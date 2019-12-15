using Clinic.Settings.Abstract;

namespace Clinic.Settings.Implementations
{
    public class ClinicDatabaseSettings : IClinicDatabaseSettings
    {
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }
}
