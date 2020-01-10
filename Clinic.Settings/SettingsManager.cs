using Clinic.Settings.Abstract;

namespace Clinic.Settings
{
    public class SettingsManager
    {
        public IClinicDatabaseSettings DatabaseSettings;
        public SettingsManager(IClinicDatabaseSettings databaseSettings)
        {
            this.DatabaseSettings = databaseSettings;
        }
    }
}
