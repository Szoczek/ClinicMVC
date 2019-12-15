using Clinic.Settings.Implementations;

namespace Clinic.Settings
{
    public class SettingsManager
    {
        public ClinicDatabaseSettings DatabaseSettings;
        public SettingsManager(ClinicDatabaseSettings databaseSettings)
        {
            this.DatabaseSettings = databaseSettings;
        }
    }
}
