using System.Threading.Tasks;

namespace Clinic.Services.Abstract
{
    public interface IFakeDataService
    {
        Task GeneratePatientsAsync(int length);
        Task GenerateDoctorsAsync(int length);
        Task GenerateVisitsAsync(int length);
    }
}
