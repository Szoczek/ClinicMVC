using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Clinic.Settings.Implementations;
using Clinic.Settings.Abstract;
using Clinic.Settings;

namespace Clinic.Resolver
{
    public class SettingsRegistrator
    {
        public static void RegisterSettings(IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<ClinicDatabaseSettings>(
                configuration.GetSection(nameof(ClinicDatabaseSettings)));

            services.AddSingleton<IClinicDatabaseSettings, ClinicDatabaseSettings>();
            services.AddSingleton<SettingsManager>();
        }
    }
}
