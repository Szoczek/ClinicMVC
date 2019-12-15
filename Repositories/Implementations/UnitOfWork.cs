using Clinic.Database.Models;
using Clinic.Repositories.Abstract;
using System;

namespace Clinic.Repositories.Implementations
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        public IRepository<About> AboutRepository => throw new NotImplementedException();

        public IRepository<User> UserRepository => throw new NotImplementedException();

        public IRepository<Patient> PatientRepository => throw new NotImplementedException();

        public IRepository<Doctor> DoctorRepository => throw new NotImplementedException();

        public IRepository<Visit> VisitRepository => throw new NotImplementedException();

        public IRepository<Contract> ContractRepository => throw new NotImplementedException();

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public void Save()
        {
            throw new NotImplementedException();
        }
    }
}
