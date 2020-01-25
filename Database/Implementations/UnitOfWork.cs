using Clinic.Database.Abstract;
using Clinic.Database.Models;
using System;
using System.Threading.Tasks;

namespace Clinic.Database.Implementations
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly MongoDbContext _context;
        private IRepository<User> _usrRepo;
        public IRepository<About> AboutRepository => new Repository<About>(_context);
        public IRepository<User> UserRepository => _usrRepo ?? (_usrRepo = new Repository<User>(_context));
        public IRepository<Visit> VisitRepository => new Repository<Visit>(_context);
        public UnitOfWork(MongoDbContext context)
        {
            _context = context;
        }


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
