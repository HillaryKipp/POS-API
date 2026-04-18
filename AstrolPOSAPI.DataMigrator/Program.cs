using System.Collections;
using System.Reflection;
using AstrolPOSAPI.Application.Interfaces.Services;
using AstrolPOSAPI.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Configuration;
using Npgsql;

AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

var configuration = BuildConfiguration(args);

var sourceConnectionString = configuration["source"]
    ?? configuration["MIGRATION_SOURCE_CONNECTION_STRING"]
    ?? configuration.GetConnectionString("DefaultConnection");

var targetConnectionString = configuration["target"]
    ?? configuration["MIGRATION_TARGET_CONNECTION_STRING"]
    ?? configuration["RENDER_POSTGRES_CONNECTION_STRING"];

if (string.IsNullOrWhiteSpace(sourceConnectionString))
{
    throw new InvalidOperationException("Missing source connection string. Set MIGRATION_SOURCE_CONNECTION_STRING or configure ConnectionStrings:DefaultConnection.");
}

if (string.IsNullOrWhiteSpace(targetConnectionString))
{
    throw new InvalidOperationException("Missing target connection string. Set MIGRATION_TARGET_CONNECTION_STRING or pass --target.");
}

sourceConnectionString = NormalizeConnectionString(sourceConnectionString);
targetConnectionString = NormalizeConnectionString(targetConnectionString);

await using var sourceDb = new AppDbContext(CreateOptions(sourceConnectionString), new NullCurrentUserService())
{
    DisableAuditTracking = true
};

await using var targetDb = new AppDbContext(CreateOptions(targetConnectionString), new NullCurrentUserService())
{
    DisableAuditTracking = true
};

Console.WriteLine("Ensuring target schema exists...");
await targetDb.Database.EnsureCreatedAsync();

var orderedEntityTypes = GetMigrationOrder(sourceDb.Model);
var totalRows = 0;

foreach (var entityType in orderedEntityTypes)
{
    var clrType = entityType.ClrType;
    if (clrType == null)
    {
        continue;
    }

    var sourceRows = await LoadEntitiesAsync(sourceDb, clrType);
    if (sourceRows.Count == 0)
    {
        continue;
    }

    Console.WriteLine($"Copying {entityType.GetTableName() ?? clrType.Name} ({sourceRows.Count} rows)...");

    targetDb.ChangeTracker.AutoDetectChangesEnabled = false;
    targetDb.AddRange(sourceRows);
    await targetDb.SaveChangesAsync();
    targetDb.ChangeTracker.Clear();

    totalRows += sourceRows.Count;
}

Console.WriteLine($"Migration complete. {totalRows} rows copied.");

static IConfigurationRoot BuildConfiguration(string[] args)
{
    var basePath = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, "..", "..", "..", "..", "Astrol_POS_API"));

    return new ConfigurationBuilder()
        .SetBasePath(basePath)
        .AddJsonFile("appsettings.json", optional: true)
        .AddJsonFile("appsettings.Development.json", optional: true)
        .AddEnvironmentVariables()
        .AddCommandLine(args)
        .Build();
}

static DbContextOptions<AppDbContext> CreateOptions(string connectionString)
{
    var builder = new DbContextOptionsBuilder<AppDbContext>();

    if (IsPostgresConnectionString(connectionString))
    {
        builder.UseNpgsql(connectionString);
    }
    else
    {
        builder.UseSqlServer(connectionString);
    }

    return builder.Options;
}

static bool IsPostgresConnectionString(string connectionString)
{
    return connectionString.Contains("Host=", StringComparison.OrdinalIgnoreCase)
        || connectionString.StartsWith("postgresql://", StringComparison.OrdinalIgnoreCase)
        || connectionString.StartsWith("postgres://", StringComparison.OrdinalIgnoreCase);
}

static string NormalizeConnectionString(string connectionString)
{
    if (!connectionString.StartsWith("postgresql://", StringComparison.OrdinalIgnoreCase)
        && !connectionString.StartsWith("postgres://", StringComparison.OrdinalIgnoreCase))
    {
        return connectionString;
    }

    var uri = new Uri(connectionString);
    var userInfo = uri.UserInfo.Split(':', 2);
    var databaseName = uri.AbsolutePath.Trim('/');

    var builder = new NpgsqlConnectionStringBuilder
    {
        Host = uri.Host,
        Port = uri.Port > 0 ? uri.Port : 5432,
        Username = Uri.UnescapeDataString(userInfo[0]),
        Password = userInfo.Length > 1 ? Uri.UnescapeDataString(userInfo[1]) : string.Empty,
        Database = databaseName,
        SslMode = SslMode.Require
    };

    return builder.ConnectionString;
}

static List<IEntityType> GetMigrationOrder(IModel model)
{
    var entityTypes = model.GetEntityTypes()
        .Where(entityType => !entityType.IsOwned())
        .Where(entityType => entityType.FindPrimaryKey() != null)
        .Where(entityType => entityType.ClrType != null)
        .Where(entityType => entityType.GetTableName() != null)
        .ToList();

    var dependencies = entityTypes.ToDictionary(
        entityType => entityType,
        entityType => entityType.GetForeignKeys()
            .Select(foreignKey => foreignKey.PrincipalEntityType)
            .Where(principal => principal != entityType && entityTypes.Contains(principal))
            .ToHashSet());

    var ordered = new List<IEntityType>();
    var queue = new Queue<IEntityType>(dependencies.Where(pair => pair.Value.Count == 0).Select(pair => pair.Key).OrderBy(entityType => entityType.GetTableName()));

    while (queue.Count > 0)
    {
        var current = queue.Dequeue();
        if (!ordered.Contains(current))
        {
            ordered.Add(current);
        }

        foreach (var dependency in dependencies)
        {
            if (!dependency.Value.Remove(current) || dependency.Value.Count != 0)
            {
                continue;
            }

            if (!ordered.Contains(dependency.Key))
            {
                queue.Enqueue(dependency.Key);
            }
        }
    }

    foreach (var remaining in entityTypes.Where(entityType => !ordered.Contains(entityType)).OrderBy(entityType => entityType.GetTableName()))
    {
        ordered.Add(remaining);
    }

    return ordered;
}

static async Task<List<object>> LoadEntitiesAsync(DbContext dbContext, Type clrType)
{
    var query = BuildQuery(dbContext, clrType);
    var list = await InvokeToListAsync(query, clrType);

    return list.Cast<object>().ToList();
}

static IQueryable BuildQuery(DbContext dbContext, Type clrType)
{
    var setMethod = typeof(DbContext).GetMethods(BindingFlags.Instance | BindingFlags.Public)
        .Single(method => method.Name == nameof(DbContext.Set) && method.IsGenericMethodDefinition && method.GetParameters().Length == 0)
        .MakeGenericMethod(clrType);

    var query = (IQueryable)setMethod.Invoke(dbContext, null)!;
    query = ApplyQueryableExtension(nameof(EntityFrameworkQueryableExtensions.IgnoreQueryFilters), clrType, query);
    query = ApplyQueryableExtension(nameof(EntityFrameworkQueryableExtensions.AsNoTracking), clrType, query);

    return query;
}

static IQueryable ApplyQueryableExtension(string methodName, Type clrType, IQueryable source)
{
    var method = typeof(EntityFrameworkQueryableExtensions).GetMethods(BindingFlags.Public | BindingFlags.Static)
        .Where(candidate => candidate.Name == methodName)
        .Where(candidate => candidate.IsGenericMethodDefinition)
        .Single(candidate => candidate.GetParameters().Length == 1)
        .MakeGenericMethod(clrType);

    return (IQueryable)method.Invoke(null, new object[] { source })!;
}

static async Task<IList> InvokeToListAsync(IQueryable query, Type clrType)
{
    var method = typeof(EntityFrameworkQueryableExtensions).GetMethods(BindingFlags.Public | BindingFlags.Static)
        .Where(candidate => candidate.Name == nameof(EntityFrameworkQueryableExtensions.ToListAsync))
        .Where(candidate => candidate.IsGenericMethodDefinition)
        .Single(candidate =>
        {
            var parameters = candidate.GetParameters();
            return parameters.Length == 2 && parameters[1].ParameterType == typeof(CancellationToken);
        })
        .MakeGenericMethod(clrType);

    var task = (Task)method.Invoke(null, new object[] { query, CancellationToken.None })!;
    await task.ConfigureAwait(false);

    return (IList)task.GetType().GetProperty("Result")!.GetValue(task)!;
}

sealed class NullCurrentUserService : ICurrentUserService
{
    public string? UserId => null;
}