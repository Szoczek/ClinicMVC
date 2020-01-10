using Clinic.Settings.Abstract;
using Microsoft.Extensions.Configuration;

namespace Clinic.Settings.Implementations
{
    public class ClinicDatabaseSettings : IClinicDatabaseSettings
    {
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }

        public ClinicDatabaseSettings(IConfiguration configuration)
        {
            ConnectionString = configuration.GetSection(nameof(ClinicDatabaseSettings)).GetSection("ConnectionString").Value;
            DatabaseName = configuration.GetSection(nameof(ClinicDatabaseSettings)).GetSection("DatabaseName").Value;
        }
    }
}
