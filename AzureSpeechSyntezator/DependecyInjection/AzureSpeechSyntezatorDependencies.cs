using AzureSpeechSyntezator.Services;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzureSpeechSyntezator.DependecyInjection
{
    public static class AzureSpeechSyntezatorDependencies
    {
        public static void AddServices(IServiceCollection services)
        {
            services.AddScoped<ConfigurationService>();
            services.AddScoped<SpeechSyntezator>();
            services.AddScoped<AudioService>();
        }
    }
}