using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using AstrolPOSAPI.Domain.Common;
using AstrolPOSAPI.Domain.Entities.Audit;
using AstrolPOSAPI.Domain.Entities.Core;
using AstrolPOSAPI.Domain.Entities.Identity;
using AstrolPOSAPI.Domain.Entities.POS;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AstrolPOSAPI.Persistence.Contexts
{
    public class AppDbContext(DbContextOptions<AppDbContext> options) : IdentityDbContext<AppUser, AppRole, string>(options)
    {

        public DbSet<AuditLog> AuditLogs => Set<AuditLog>();
        public DbSet<Company> Companies => Set<Company>();
        public DbSet<Store> Stores => Set<Store>();
        public DbSet<StoreType> StoreTypes => Set<StoreType>();
        public DbSet<NoSeries> NoSeries => Set<NoSeries>();
        public DbSet<DrawerGroup> DrawerGroups => Set<DrawerGroup>();
        public DbSet<Terminal> Terminals => Set<Terminal>();
        public DbSet<DefaultScreen> DefaultScreens => Set<DefaultScreen>();
        public DbSet<Drawer> Drawers => Set<Drawer>();
        public DbSet<AssignedDrawer> AssignedDrawers => Set<AssignedDrawer>();
        public DbSet<TouchScreen> TouchScreens => Set<TouchScreen>();
        public DbSet<TouchScreenButton> TouchScreenButtons => Set<TouchScreenButton>();

        // Identity/Auth related

        public DbSet<Permission> Permissions => Set<Permission>();
        public DbSet<Domain.Entities.Identity.OTP> OTPs => Set<Domain.Entities.Identity.OTP>();
        public DbSet<AstrolPOSAPI.Domain.Entities.Identity.UserStore> UserStores => Set<AstrolPOSAPI.Domain.Entities.Identity.UserStore>();
        public DbSet<AstrolPOSAPI.Domain.Entities.Core.GeneralSettings> GeneralSettings => Set<AstrolPOSAPI.Domain.Entities.Core.GeneralSettings>();

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Company>(e =>
            {
                e.Property(p => p.Code).IsRequired().HasMaxLength(32);
                e.Property(p => p.Name).IsRequired().HasMaxLength(256);
                e.HasIndex(p => p.Code).IsUnique();
            });

            builder.Entity<StoreType>(e =>
            {
                e.Property(p => p.Code).IsRequired().HasMaxLength(32);
                e.Property(p => p.Description).IsRequired().HasMaxLength(256);
                e.HasIndex(p => p.Code).IsUnique();
            });

            builder.Entity<NoSeries>(e =>
            {
                e.Property(p => p.Code).IsRequired().HasMaxLength(32);
                e.Property(p => p.Description).IsRequired().HasMaxLength(128);
            });

            builder.Entity<DrawerGroup>(e =>
            {
                e.Property(p => p.Code).IsRequired().HasMaxLength(32);
                e.Property(p => p.Name).IsRequired().HasMaxLength(128);
                e.Property(p => p.Description).IsRequired().HasMaxLength(128);
                e.Property(p => p.CompanyId).IsRequired();
                e.Property(p => p.StoreOfOperationId).IsRequired();

                e.HasOne(dg => dg.Company)
                    .WithMany()
                    .HasForeignKey(dg => dg.CompanyId)
                    .OnDelete(DeleteBehavior.Restrict);

                e.HasOne(dg => dg.StoreOfOperation)
                    .WithMany()
                    .HasForeignKey(dg => dg.StoreOfOperationId)
                    .OnDelete(DeleteBehavior.Restrict);

                e.HasQueryFilter(dg => dg.DeletedDate == null);
            });

            builder.Entity<Terminal>(e =>
            {
                e.Property(p => p.Code).IsRequired().HasMaxLength(32);
                e.Property(p => p.Description).IsRequired().HasMaxLength(128);
                e.Property(p => p.CompanyId).IsRequired();
                e.Property(p => p.StoreOfOperationId).IsRequired();

                e.HasOne(t => t.Company)
                    .WithMany()
                    .HasForeignKey(t => t.CompanyId)
                    .OnDelete(DeleteBehavior.Restrict);

                e.HasOne(t => t.StoreOfOperation)
                    .WithMany()
                    .HasForeignKey(t => t.StoreOfOperationId)
                    .OnDelete(DeleteBehavior.Restrict);

                e.HasQueryFilter(t => t.DeletedDate == null);
            });

            builder.Entity<DefaultScreen>(e =>
            {
                e.Property(p => p.Code).IsRequired().HasMaxLength(32);
                e.Property(p => p.Description).IsRequired().HasMaxLength(128);
                e.Property(p => p.CompanyId).IsRequired();
                e.Property(p => p.StoreOfOperationId).IsRequired();

                e.HasOne(ds => ds.Company)
                    .WithMany()
                    .HasForeignKey(ds => ds.CompanyId)
                    .OnDelete(DeleteBehavior.Restrict);

                e.HasOne(ds => ds.StoreOfOperation)
                    .WithMany()
                    .HasForeignKey(ds => ds.StoreOfOperationId)
                    .OnDelete(DeleteBehavior.Restrict);

                e.HasQueryFilter(ds => ds.DeletedDate == null);
            });

            builder.Entity<Drawer>(e =>
            {
                e.Property(p => p.Status).HasConversion<int>();

                // Foreign Keys

                e.Property(p => p.CompanyId).IsRequired();
                e.Property(p => p.StoreOfOperationId).IsRequired();
                e.Property(p => p.DrawerGroupId).IsRequired();
                e.Property(p => p.TerminalId).IsRequired();

                // Relationships
                e.HasOne(d => d.Company)
                    .WithMany()
                    .HasForeignKey(d => d.CompanyId)
                    .OnDelete(DeleteBehavior.Restrict);

                e.HasOne(d => d.StoreOfOperation)
                    .WithMany()
                    .HasForeignKey(d => d.StoreOfOperationId)
                    .OnDelete(DeleteBehavior.Restrict);

                e.HasOne(d => d.DrawerGroup)
                    .WithMany()
                    .HasForeignKey(d => d.DrawerGroupId)
                    .OnDelete(DeleteBehavior.Restrict);

                e.HasOne(d => d.Terminal)
                    .WithMany()
                    .HasForeignKey(d => d.TerminalId)
                    .OnDelete(DeleteBehavior.Restrict);

                e.HasOne(d => d.DefaultScreen)
                    .WithMany()
                    .HasForeignKey(d => d.DefaultScreenId)
                    .OnDelete(DeleteBehavior.Restrict);

                e.HasQueryFilter(d => d.DeletedDate == null);
                e.HasIndex(d => d.StoreOfOperationId);
            });

            builder.Entity<AssignedDrawer>(e =>
            {
                e.Property(p => p.OpenCash).HasColumnType("decimal(18,2)");

                // Foreign Keys

                e.Property(p => p.CompanyId).IsRequired();
                e.Property(p => p.StoreOfOperationId).IsRequired();
                e.Property(p => p.DrawerId).IsRequired();
                e.Property(p => p.UserId).IsRequired();

                // Relationships
                e.HasOne(ad => ad.Company)
                    .WithMany()
                    .HasForeignKey(ad => ad.CompanyId)
                    .OnDelete(DeleteBehavior.Restrict);

                e.HasOne(ad => ad.StoreOfOperation)
                    .WithMany()
                    .HasForeignKey(ad => ad.StoreOfOperationId)
                    .OnDelete(DeleteBehavior.Restrict);

                e.HasOne(ad => ad.Drawer)
                    .WithMany()
                    .HasForeignKey(ad => ad.DrawerId)
                    .OnDelete(DeleteBehavior.Restrict);

                e.HasOne(ad => ad.User)
                    .WithMany()
                    .HasForeignKey(ad => ad.UserId)
                    .OnDelete(DeleteBehavior.Restrict);

                e.HasOne(ad => ad.DefaultScreen)
                    .WithMany()
                    .HasForeignKey(ad => ad.DefaultScreenId)
                    .OnDelete(DeleteBehavior.Restrict);

                e.HasQueryFilter(ad => ad.DeletedDate == null);
                e.HasIndex(ad => ad.StoreOfOperationId);
                e.HasIndex(ad => ad.UserId);
                e.HasIndex(ad => ad.DrawerId);
            });

            builder.Entity<TouchScreen>(e =>
            {
                e.Property(p => p.ScreenName).IsRequired().HasMaxLength(100);
                e.Property(p => p.Description).HasMaxLength(500);
                e.Property(p => p.CompanyId).IsRequired();
                e.Property(p => p.StoreOfOperationId).IsRequired();

                e.HasOne(ts => ts.Company)
                    .WithMany()
                    .HasForeignKey(ts => ts.CompanyId)
                    .OnDelete(DeleteBehavior.Restrict);

                e.HasOne(ts => ts.StoreOfOperation)
                    .WithMany()
                    .HasForeignKey(ts => ts.StoreOfOperationId)
                    .OnDelete(DeleteBehavior.Restrict);

                e.HasMany(ts => ts.Buttons)
                    .WithOne(b => b.TouchScreen)
                    .HasForeignKey(b => b.TouchScreenId)
                    .OnDelete(DeleteBehavior.Cascade);

                e.HasQueryFilter(ts => ts.DeletedDate == null);
                e.HasIndex(ts => ts.StoreOfOperationId);
            });

            builder.Entity<TouchScreenButton>(e =>
            {
                e.Property(p => p.ItemName).IsRequired().HasMaxLength(100);
                e.Property(p => p.BackgroundColor).IsRequired().HasMaxLength(7);
                e.Property(p => p.TextColor).IsRequired().HasMaxLength(7);
                e.Property(p => p.ImageUrl).HasMaxLength(512);
                e.Property(p => p.CompanyId).IsRequired();
                e.Property(p => p.StoreOfOperationId).IsRequired();
                e.Property(p => p.TouchScreenId).IsRequired();

                // Enum conversions
                e.Property(p => p.ButtonType).HasConversion<int>();
                e.Property(p => p.Shape).HasConversion<int>();

                e.HasOne(b => b.Company)
                    .WithMany()
                    .HasForeignKey(b => b.CompanyId)
                    .OnDelete(DeleteBehavior.Restrict);

                e.HasOne(b => b.StoreOfOperation)
                    .WithMany()
                    .HasForeignKey(b => b.StoreOfOperationId)
                    .OnDelete(DeleteBehavior.Restrict);

                e.HasOne(b => b.TouchScreen)
                    .WithMany(ts => ts.Buttons)
                    .HasForeignKey(b => b.TouchScreenId)
                    .OnDelete(DeleteBehavior.Cascade);

                e.HasQueryFilter(b => b.DeletedDate == null);
                e.HasIndex(b => b.TouchScreenId);
                e.HasIndex(b => b.StoreOfOperationId);
                e.HasIndex(b => new { b.Row, b.Column });
            });

            builder.Entity<AuditLog>(e =>
            {
                e.Property(p => p.TableName).IsRequired().HasMaxLength(256);
                e.Property(p => p.Action).IsRequired().HasMaxLength(32);
            });

            builder.Entity<AppUser>(e =>
            {
                e.Property(p => p.EmpNo).HasMaxLength(32);
                e.Property(p => p.Name).HasMaxLength(256);
                e.Property(p => p.NationalID).HasMaxLength(64);

                // Relationships

                e.HasOne(u => u.Company)
                    .WithMany()
                    .HasForeignKey(u => u.CompanyId)
                    .OnDelete(DeleteBehavior.Restrict);


                e.HasOne(u => u.StoreOfOperation)
                    .WithMany()
                    .HasForeignKey(u => u.StoreOfOperationId)
                    .OnDelete(DeleteBehavior.Restrict);

                // Indexes for performance

                e.HasIndex(u => u.CompanyId);
                e.HasIndex(u => u.StoreOfOperationId);
            });

            // UserStore (junction table for many-to-many User-Store relationship)
            builder.Entity<AstrolPOSAPI.Domain.Entities.Identity.UserStore>(e =>
            {
                // Composite primary key
                e.HasKey(us => new { us.UserId, us.StoreId });

                // Relationships
                e.HasOne(us => us.User)
                    .WithMany(u => u.UserStores)
                    .HasForeignKey(us => us.UserId)
                    .OnDelete(DeleteBehavior.Cascade);

                e.HasOne(us => us.Store)
                    .WithMany()
                    .HasForeignKey(us => us.StoreId)
                    .OnDelete(DeleteBehavior.Restrict);

                // Indexes
                e.HasIndex(us => us.UserId);
                e.HasIndex(us => us.StoreId);
                e.HasIndex(us => us.IsPrimary);
            });

            // Store entity configuration

            builder.Entity<Store>(e =>
            {
                e.Property(p => p.Code).IsRequired().HasMaxLength(32);
                e.Property(p => p.Name).IsRequired().HasMaxLength(256);
                e.Property(p => p.CompanyId).IsRequired();


                e.HasOne(s => s.Company)
                    .WithMany(c => c.Stores)
                    .HasForeignKey(s => s.CompanyId)
                    .OnDelete(DeleteBehavior.Cascade);


                e.HasOne(s => s.StoreType)
                    .WithMany()
                    .HasForeignKey(s => s.StoreTypeId)
                    .OnDelete(DeleteBehavior.Restrict);

                // Query filter for soft deletes

                e.HasQueryFilter(s => s.DeletedDate == null);

                // Indexes

                e.HasIndex(s => s.CompanyId);
                e.HasIndex(s => s.Code);
            });

            // Permission entity configuration

            builder.Entity<Permission>(e =>
            {
                e.Property(p => p.ResourceName).IsRequired().HasMaxLength(128);


                e.HasOne(p => p.User)
                    .WithMany()
                    .HasForeignKey(p => p.UserId)
                    .OnDelete(DeleteBehavior.Cascade);


                e.HasOne(p => p.Role)
                    .WithMany()
                    .HasForeignKey(p => p.RoleId)
                    .OnDelete(DeleteBehavior.Cascade);

                // Query filter for soft deletes

                e.HasQueryFilter(p => p.DeletedDate == null);

                // Indexes

                e.HasIndex(p => new { p.UserId, p.ResourceName });
                e.HasIndex(p => new { p.RoleId, p.ResourceName });
            });

            // OTP entity configuration

            builder.Entity<Domain.Entities.Identity.OTP>(e =>
            {
                e.Property(p => p.PhoneNumber).IsRequired().HasMaxLength(20);
                e.Property(p => p.OTPCode).IsRequired().HasMaxLength(10);
                e.Property(p => p.Purpose).IsRequired().HasMaxLength(64);


                e.HasOne(o => o.User)
                    .WithMany()
                    .HasForeignKey(o => o.UserId)
                    .OnDelete(DeleteBehavior.Cascade);

                // Indexes for quick lookups

                e.HasIndex(o => new { o.UserId, o.Purpose, o.IsVerified });
                e.HasIndex(o => o.ExpiresAt);
            });

            // GeneralSettings entity configuration
            builder.Entity<AstrolPOSAPI.Domain.Entities.Core.GeneralSettings>(e =>
            {
                e.Property(p => p.CompanyId).IsRequired();
                e.Property(p => p.LogoUrl).HasMaxLength(512);
                e.Property(p => p.CompanyName).HasMaxLength(256);
                e.Property(p => p.CompanySlogan).HasMaxLength(512);


                e.Property(p => p.PrimaryColor).HasMaxLength(20);
                e.Property(p => p.SecondaryColor).HasMaxLength(20);
                e.Property(p => p.TertiaryColor).HasMaxLength(20);
                e.Property(p => p.AccentColor).HasMaxLength(20);
                e.Property(p => p.BackgroundColor).HasMaxLength(20);
                e.Property(p => p.BackgroundImageUrl).HasMaxLength(512);


                e.Property(p => p.Currency).HasMaxLength(10);
                e.Property(p => p.CurrencySymbol).HasMaxLength(5);
                e.Property(p => p.DateFormat).HasMaxLength(20);
                e.Property(p => p.TimeFormat).HasMaxLength(5);
                e.Property(p => p.Timezone).HasMaxLength(100);


                e.Property(p => p.TaxNumber).HasMaxLength(50);
                e.Property(p => p.ReceiptFooter).HasMaxLength(500);
                e.Property(p => p.SupportEmail).HasMaxLength(256);
                e.Property(p => p.SupportPhone).HasMaxLength(20);

                // One-to-one relationship with Company
                e.HasOne(gs => gs.Company)
                    .WithOne()
                    .HasForeignKey<AstrolPOSAPI.Domain.Entities.Core.GeneralSettings>(gs => gs.CompanyId)
                    .OnDelete(DeleteBehavior.Cascade);

                // Query filter for soft deletes
                e.HasQueryFilter((AstrolPOSAPI.Domain.Entities.Core.GeneralSettings gs) => gs.DeletedDate == null);

                // Index on CompanyId for quick lookups
                e.HasIndex(gs => gs.CompanyId).IsUnique();
            });
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var now = DateTimeOffset.UtcNow;

            // ---------------------------
            // 1. FIX: Materialize entries before looping
            // ---------------------------
            var entries = ChangeTracker
                .Entries<BaseAuditableEntity>()
                .ToList();

            foreach (var entry in entries)
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.CreatedDate = now.DateTime;
                        break;
                    case EntityState.Modified:
                        entry.Entity.UpdatedDate = now.DateTime;
                        break;
                    case EntityState.Deleted:
                        entry.Entity.DeletedDate = now.DateTime;
                        break;
                }
            }

            // ---------------------------
            // 2. FIX: Materialize audit entries before looping
            // ---------------------------
            var auditEntries = ChangeTracker.Entries()
                .Where(e => e.State == EntityState.Added
                            || e.State == EntityState.Modified
                            || e.State == EntityState.Deleted)
                .Where(e => e.Entity is not AuditLog)
                .ToList();

            foreach (var e in auditEntries)
            {
                var log = new AuditLog
                {
                    TableName = e.Metadata.GetTableName() ?? e.Entity.GetType().Name,
                    Action = e.State.ToString(),
                    OccurredAt = now,
                };

                // Primary Key
                var key = e.Properties
                    .Where(p => p.Metadata.IsPrimaryKey())
                    .ToDictionary(p => p.Metadata.Name, p => p.CurrentValue);

                log.KeyValues = JsonSerializer.Serialize(key);

                // OLD VALUES
                if (e.State == EntityState.Modified || e.State == EntityState.Deleted)
                {
                    var oldVals = e.Properties
                        .ToDictionary(p => p.Metadata.Name, p => p.OriginalValue);
                    log.OldValues = JsonSerializer.Serialize(oldVals);
                }

                // NEW VALUES
                if (e.State == EntityState.Added || e.State == EntityState.Modified)
                {
                    var newVals = e.Properties
                        .ToDictionary(p => p.Metadata.Name, p => p.CurrentValue);
                    log.NewValues = JsonSerializer.Serialize(newVals);
                }

                // THIS modifies ChangeTracker → MUST run AFTER .ToList()
                AuditLogs.Add(log);
            }

            return await base.SaveChangesAsync(cancellationToken);
        }


    }
}
