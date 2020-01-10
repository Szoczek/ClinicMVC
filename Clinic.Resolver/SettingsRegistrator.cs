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
            services.AddSingleton<IClinicDatabaseSettings, ClinicDatabaseSettings>();
            services.Configure<IClinicDatabaseSettings>(
                configuration.GetSection(nameof(ClinicDatabaseSettings)));

            services.AddSingleton<SettingsManager>();
        }
    }
}
