using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Clinic.Database;
using Clinic.Database.Models;
using Clinic.Database.Models.ExternalTypes;
using Clinic.Services.Abstract;
using Clinic.Utils;
using Microsoft.AspNetCore.Authentication.Cookies;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace Clinic.Services.Implementations
{
    public class UserService : IUserService
    {
        private readonly DataContext _dataContext;
        public UserService(DataContext dataContext) => _dataContext = dataContext;
        public async Task<ClaimsPrincipal> Authenticate(User user)
        {

            UserRoles role = user.IsAdmin ? UserRoles.Admin
                : user.IsDoctor() ? UserRoles.Doctor
                : user.IsPatient() ? UserRoles.Patient
                : UserRoles.User;

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString(), CookieAuthenticationDefaults.AuthenticationScheme),
                new Claim(ClaimTypes.Email, user.Login, CookieAuthenticationDefaults.AuthenticationScheme),
                new Claim(ClaimTypes.Name, $"{user.FirstName} {user.LastName}", CookieAuthenticationDefaults.AuthenticationScheme),
                new Claim(ClaimTypes.Role, role.ToString(), CookieAuthenticationDefaults.AuthenticationScheme)
            };

            var userIdentity = new ClaimsIdentity(claims,
                CookieAuthenticationDefaults.AuthenticationScheme,
                ClaimTypes.Name,
                ClaimTypes.Role);

            var principal = new ClaimsPrincipal(userIdentity);

            return await Task.FromResult(principal);
        }

        public async Task<User> GetById(string id)
        {
            Guid userId = Guid.Parse(id);
            User user = await _dataContext.GetCollection<User>()
                .AsQueryable().FirstOrDefaultAsync(x => x.Id.Equals(userId));

            return user;
        }

        public async Task<User> GetById(Guid id)
        {
            User user = await _dataContext.GetCollection<User>()
                .AsQueryable().FirstOrDefaultAsync(x => x.Id.Equals(id));

            return user;
        }

        public async Task<IEnumerable<User>> GetDoctors()
        {
            return await _dataContext.GetCollection<User>()
                .AsQueryable()
                .Where(x => x.Doctor != null).ToListAsync();
        }

        public async Task<Dictionary<string, string>> GetDoctorsForSpecialityExcludingDoctor(Specialties speciality, User doctor = null)
        {
            var doctors = new Dictionary<string, string>();
            var doctorsTmp = await _dataContext.GetCollection<User>()
                .AsQueryable()
                .Where(x => x.Doctor != null && x.Doctor.Speciality == speciality)
                .ToListAsync();

            if (doctor != null)
                doctorsTmp = doctorsTmp.Where(x => x.Id != doctor.Id).ToList();

            doctorsTmp.ForEach(x => doctors.Add(x.Id.ToString(), x.GetFullName()));
            return doctors;
        }

        public async Task<IEnumerable<User>> GetPatients()
        {
            return await _dataContext.GetCollection<User>()
                .AsQueryable()
                .Where(x => x.Patient != null).ToListAsync();
        }

        public async Task<User> Login(string login, string password)
        {
            var users = await _dataContext
                .GetCollection<User>()
                .AsQueryable()
                .Where(x => x.Login.Equals(login)).ToListAsync();

            var user = users
                .FirstOrDefault(x => BCrypt.Net.BCrypt.Verify(password, x.Password.ToString()));

            if (user == null)
                throw new Exception("Invalid login or password");

            return user;
        }

        public async Task<User> PatchUser(User user)
        {

            var tmp = await _dataContext.GetCollection<User>()
                .AsQueryable()
                .FirstOrDefaultAsync(x => x.Id.Equals(user.Id));

            if (user.Password != null)
                user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);
            else
                user.Password = tmp.Password;

            if (tmp.Doctor != null)
                user.Doctor.Contract = tmp.Doctor.Contract;

            await _dataContext.GetCollection<User>()
                .FindOneAndReplaceAsync(Builders<User>.Filter.Where(x => x.Id.Equals(user.Id)), user);

            user = await _dataContext.GetCollection<User>()
                .AsQueryable()
                .FirstOrDefaultAsync(x => x.Id.Equals(user.Id));
            return user;
        }

        public async Task<User> Register(User user)
        {
            user.Id = Guid.NewGuid();
            user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);
            await _dataContext
                .GetCollection<User>().InsertOneAsync(user);

            user = await _dataContext
                .GetCollection<User>()
                .AsQueryable()
                .FirstOrDefaultAsync(x => x.Id.Equals(user.Id));
            return user;
        }
    }
}
