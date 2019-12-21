using Clinic.Database.Abstract;
using Clinic.Database.Models;
using System;
using System.Threading.Tasks;

namespace Clinic.Database.Implementations
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly MongoDbContext _context;
        public UnitOfWork(MongoDbContext context)
        {
            _context = context;
        }
        public IRepository<About> AboutRepository => AboutRepository ?? new Repository<About>(_context);
        public IRepository<User> UserRepository => UserRepository ?? new Repository<User>(_context);
        public IRepository<Patient> PatientRepository => PatientRepository ?? new Repository<Patient>(_context);
        public IRepository<Doctor> DoctorRepository => DoctorRepository ?? new Repository<Doctor>(_context);
        public IRepository<Visit> VisitRepository => VisitRepository ?? new Repository<Visit>(_context);
        public IRepository<Contract> ContractRepository => ContractRepository ?? new Repository<Contract>(_context);

        public async void Dispose()
        {
            await SaveAsync();
            _context.Dispose();
        }

        public void Save()
        {
            _context.CommitTransaction();
        }

        public async Task SaveAsync()
        {
            await _context.CommitTransactionAsync();
        }
    }
}
