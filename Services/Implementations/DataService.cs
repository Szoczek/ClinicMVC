using Bogus;
using Database;
using Database.Models;
using Database.Models.Utilities;
using Services.Services;
using System;
using System.Linq;

namespace Services.Implementations
{
    public class DataService : IDataService
    {
        private readonly DataContext _dataContext;
        private readonly UserService _userService;
        public DataService(DataContext dataContext, UserService userService)
        {
            this._dataContext = dataContext;
            this._userService = userService;
        }

        public void GenerateDoctors(int length)
        {
            var userFaker = new Faker<User>()
                .CustomInstantiator(f => new User())
                .RuleFor(o => o.FirstName, f => f.Name.FirstName())
                .RuleFor(o => o.LastName, f => f.Name.LastName())
                .RuleFor(o => o.Login, f => f.Internet.Email())
                .RuleFor(o => o.Password, BCrypt.Net.BCrypt.HashPassword("password"))
                .RuleFor(o => o.IsAdmin, false);

            var doctorFaker = new Faker<Doctor>()
               .CustomInstantiator(f => new Doctor())
               .RuleFor(o => o.DateOfBirth, f => f.Date.Past(50, DateTime.Now.AddYears(-30)))
               .RuleFor(o => o.Speciality, f => (Specialties)f.Finance.Random.Int(0, 5));

            var contractFaker = new Faker<Contract>()
              .CustomInstantiator(f => new Contract())
              .RuleFor(o => o.StartDate, f => f.Date.Recent(200))
              .RuleFor(o => o.EndDate, f => f.Date.Soon(720))
              .RuleFor(o => o.Salary, f => Math.Round(f.Finance.Random.Decimal(3000.0M, 6000.0M), 2));

            for (int i = 0; i < length; i++)
            {
                InsertDoctor(userFaker.Generate(1)[0], doctorFaker.Generate(1)[0], contractFaker.Generate(1)[0]);
            }
        }

        public void GeneratePatients(int length)
        {
            var userFaker = new Faker<User>()
               .CustomInstantiator(f => new User())
               .RuleFor(o => o.FirstName, f => f.Name.FirstName())
               .RuleFor(o => o.LastName, f => f.Name.LastName())
               .RuleFor(o => o.Login, f => f.Internet.Email())
               .RuleFor(o => o.Password, BCrypt.Net.BCrypt.HashPassword("password"))
               .RuleFor(o => o.IsAdmin, false);

            var patientFaker = new Faker<Patient>()
               .CustomInstantiator(f => new Patient())
               .RuleFor(o => o.DateOfBirth, f => f.Date.Past(50, DateTime.Now.AddYears(-30)))
               .RuleFor(o => o.BloodType, f => (BloodTypes)f.Finance.Random.Int(0, 3));

            for (int i = 0; i < length; i++)
            {
                InsertPatient(userFaker.Generate(1)[0], patientFaker.Generate(1)[0]);
            }
        }

        public void GenerateVisits(int length)
        {
            var patients = _userService.GetPatients().Result;
            var doctors = _userService.GetDoctors().Result;

            var visitFaker = new Faker<Visit>()
               .CustomInstantiator(f => new Visit())
               .RuleFor(o => o.StartDate, f => f.Date.Recent(365))
               .RuleFor(o => o.EndDate, f => f.Date.Recent(305))
               .RuleFor(o => o.Patient, f => patients.ElementAt(f.Random.Int(0, patients.Count() - 1)))
               .RuleFor(o => o.Doctor, f => doctors.ElementAt(f.Random.Int(0, doctors.Count() - 1)))
               .RuleFor(o => o.EndDate, f => f.Date.Recent(305))
               .RuleFor(o => o.Notes, f => f.Lorem.Lines(3));

            for (int i = 0; i < length; i++)
            {
                InsertVisit(visitFaker.Generate(1)[0]);
            }
        }

        private void InsertDoctor(User user, Doctor doctor, Contract contract)
        {
            Guid id = new Guid();

            user.Id = id;
            doctor.Id = id;
            contract.Id = new Guid();
            doctor.Contract = contract;
            user.Doctor = doctor;

            _dataContext.GetCollection<User>()
                .InsertOne(user);
        }

        private void InsertPatient(User user, Patient patient)
        {
            Guid id = new Guid();

            user.Id = id;
            patient.Id = id;
            user.Patient = patient;

            _dataContext.GetCollection<User>()
                .InsertOne(user);
        }

        private void InsertVisit(Visit visit)
        {
            visit.Id = new Guid();
            _dataContext.GetCollection<Visit>()
                .InsertOne(visit);
        }
    }
}
