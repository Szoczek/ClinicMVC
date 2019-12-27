using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Clinic.Database.Implementations;
using Clinic.Database.Models;
using Clinic.Database.Models.ExternalTypes;
using Clinic.Services.Abstract;
using Clinic.Utils;
using Clinic.Utils.Extensions;
using Microsoft.AspNetCore.Authentication.Cookies;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace Clinic.Services.Implementations
{
    public class UserService : ServiceBase, IUserService
    {
        public UserService(UnitOfWork uow) : base(uow) { }
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
            return await GetById(userId);
        }
        public async Task<User> GetById(Guid id)
        {
            return await _unitOfWork.UserRepository.GetById(id);
        }
        public async Task<IEnumerable<User>> GetDoctors()
        {
            var users = await _unitOfWork.UserRepository.GetAll();
            return users.Where(x => x.Doctor != null).ToList();
        }
        public async Task<Dictionary<string, string>> GetDoctorsForSpecialityExcludingDoctor(Specialties speciality, User doctor = null)
        {
            var doctors = new Dictionary<string, string>();
            var doctorsTmp = await _unitOfWork.UserRepository.GetAll();

            doctorsTmp = doctorsTmp
            .Where(x => x.Doctor != null && x.Doctor.Speciality == speciality)
            .ToList();

            if (doctor != null)
                doctorsTmp = doctorsTmp.Where(x => x.Id != doctor.Id).ToList();

            doctorsTmp.ForEach(x => doctors.Add(x.Id.ToString(), x.GetFullName()));
            return doctors;
        }
        public async Task<IEnumerable<User>> GetPatients()
        {
            var users = await _unitOfWork.UserRepository.GetAll();
            return users.Where(x => x.Patient != null).ToList();
        }
        public async Task<User> Login(string login, string password)
        {
            var users = await _unitOfWork.UserRepository.GetAll();
            users = users.Where(x => x.Login.Equals(login)).ToList();

            var user = users
                .FirstOrDefault(x => BCrypt.Net.BCrypt.Verify(password, x.Password.ToString()));

            if (user == null)
                throw new Exception("Invalid login or password");

            return user;
        }
        public async Task<User> PatchUser(User user)
        {
            var tmp = await GetById(user.Id);

            if (user.Password != null)
                user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);
            else
                user.Password = tmp.Password;

            if (tmp.Doctor != null)
                user.Doctor.Contract = tmp.Doctor.Contract;

            await _unitOfWork.UserRepository.Update(user);
            return await GetById(user.Id);
        }
        public async Task<User> Register(User user)
        {
            user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);
            await _unitOfWork.UserRepository.Create(user);

            return await GetById(user.Id);
        }
    }
}
