using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AstrolPOSAPI.Infrastructure
{
    public static class IServiceCollectionExtensions
    {
        public static void AddInfrastructureLayer(this IServiceCollection services)
        {
            services.AddServices();
        }

        private static void AddServices(this IServiceCollection services)
        {
            //services
            //    .AddTransient<IEmailService, EmailService>();
            
            // NoSeries service for auto-generating codes
            services.AddTransient<AstrolPOSAPI.Application.Interfaces.Services.INoSeriesService, 
                Services.NoSeriesService>();
        }
    }
}
