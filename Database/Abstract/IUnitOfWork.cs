using Clinic.Database.Models;
using System.Threading.Tasks;

namespace Clinic.Database.Abstract
{
    public interface IUnitOfWork
    {
        IRepository<About> AboutRepository { get; }
        IRepository<User> UserRepository { get; }
        IRepository<Patient> PatientRepository { get; }
        IRepository<Doctor> DoctorRepository { get; }
        IRepository<Visit> VisitRepository { get; }
        IRepository<Contract> ContractRepository { get; }
        void Save();
        Task SaveAsync();
    }
}
