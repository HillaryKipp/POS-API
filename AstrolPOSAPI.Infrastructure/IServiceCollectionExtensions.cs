using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;

namespace AstrolPOSAPI.Infrastructure
{
    public static class IServiceCollectionExtensions
    {
        public static void AddInfrastructureLayer(this IServiceCollection services, Microsoft.Extensions.Configuration.IConfiguration configuration)
        {
            services.AddServices(configuration);
        }

        private static void AddServices(this IServiceCollection services, Microsoft.Extensions.Configuration.IConfiguration configuration)
        {
            //services
            //    .AddTransient<IEmailService, EmailService>();

            // NoSeries service for auto-generating codes
            services.AddTransient<AstrolPOSAPI.Application.Interfaces.Services.INoSeriesService,
                Services.NoSeriesService>();

            services.AddTransient<AstrolPOSAPI.Application.Interfaces.Infrastructure.ISmsSender,
                Services.ExpressSmsSender>();

            var httpClientBuilder = services.AddHttpClient("ExpressSMS", client =>
            {
                var baseUrl = configuration["ExpressSMS:BaseUrl"] ?? "https://api.expresssms.com/";
                client.BaseAddress = new Uri(baseUrl);
            });

            // Bypass SSL validation if configured (useful for APIs with certificate issues)
            var bypassSsl = configuration.GetValue<bool>("ExpressSMS:BypassSSL", true); // Default true for compatibility
            if (bypassSsl)
            {
                httpClientBuilder.ConfigurePrimaryHttpMessageHandler(() => new HttpClientHandler
                {
                    ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => true
                });
            }
        }
    }
}
