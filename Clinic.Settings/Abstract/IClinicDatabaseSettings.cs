namespace Clinic.Settings.Abstract
{
    public interface IClinicDatabaseSettings
    {
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
    }
}
