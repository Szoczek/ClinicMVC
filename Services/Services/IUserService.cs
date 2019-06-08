using Database.Models;
using System.Collections;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Services.Implementations
{
    public interface IUserService
    {
        Task<User> Register(User user);
        Task<ClaimsPrincipal> Authenticate(User user);
        Task<User> Login(string login, string password);
        Task<User> GetById(string id);
        Task<User> PatchUser(User user);
        Task<IEnumerable<User>> GetDoctors();
        Task<IEnumerable<User>> GetPatients();
    }
}
