using System.Threading.Tasks;

namespace Services.Services
{
    public interface IDataService
    {
        void GeneratePatients(int length);
        void GenerateDoctors(int length);
        void GenerateVisits(int length);
    }
}
