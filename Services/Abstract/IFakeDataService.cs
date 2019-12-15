namespace Clinic.Services.Abstract
{
    public interface IFakeDataService
    {
        void GeneratePatients(int length);
        void GenerateDoctors(int length);
        void GenerateVisits(int length);
    }
}
