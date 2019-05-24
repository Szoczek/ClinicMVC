using Database.Models;
using Microsoft.AspNetCore.Authentication;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Services.Implementations
{
    public interface IUserService
    {
        Task<User> Register(User user);
        Task<ClaimsPrincipal> Authenticate(User user);
        Task<User> Login(string login, string password);
        Task<User> Get(string id);
    }
}
