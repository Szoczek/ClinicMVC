using Clinic.Database.Models;
using System.Threading.Tasks;

namespace Clinic.Database.Abstract
{
    public interface IUnitOfWork
    {
        IRepository<About> AboutRepository { get; }
        IRepository<User> UserRepository { get; }
        IRepository<Visit> VisitRepository { get; }
        void Save();
        Task SaveAsync();
    }
}
