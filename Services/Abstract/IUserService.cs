using Clinic.Database.Models;
using Clinic.Database.Models.ExternalTypes;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Clinic.Services.Abstract
{
    public interface IUserService
    {
        Task<User> Register(User user);
        Task<ClaimsPrincipal> Authenticate(User user);
        Task<User> Login(string login, string password);
        Task<User> GetById(string id);
        Task<User> GetById(Guid id);
        Task<User> PatchUser(User user);
        Task<IEnumerable<User>> GetDoctors();
        Task<IEnumerable<User>> GetPatients();
        Task<Dictionary<string, string>> GetDoctorsForSpecialityExcludingDoctor(Specialties speiciality, User doctor = null);
    }
}
