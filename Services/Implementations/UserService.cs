using System;
using System.Security.Claims;
using System.Threading.Tasks;
using Database;
using Database.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace Services.Implementations
{
    public class UserService : IUserService
    {
        private readonly DataContext _dataContext;
        public UserService(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<ClaimsPrincipal> Authenticate(User user)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString(), CookieAuthenticationDefaults.AuthenticationScheme),
                new Claim(ClaimTypes.Email, user.Login, CookieAuthenticationDefaults.AuthenticationScheme),
                new Claim(ClaimTypes.Name, $"{user.FirstName} {user.LastName}", CookieAuthenticationDefaults.AuthenticationScheme),
                new Claim(ClaimTypes.Role, user.IsAdmin ? "Administrator" : "User", CookieAuthenticationDefaults.AuthenticationScheme)
            };

            var userIdentity = new ClaimsIdentity(claims,
                CookieAuthenticationDefaults.AuthenticationScheme,
                ClaimTypes.Name,
                ClaimTypes.Role);

            var principal = new ClaimsPrincipal(userIdentity);

            return await Task.FromResult(principal);
        }

        public async Task<User> Get(string id)
        {
            Guid userId = Guid.Parse(id);
            User user = await _dataContext.GetCollection<User>()
                .AsQueryable().FirstOrDefaultAsync(x => x.Id.Equals(userId));

            return user;
        }

        public async Task<User> Login(string login, string password)
        {
            var user = await _dataContext
                .GetCollection<User>()
                .AsQueryable()
                .FirstOrDefaultAsync(x => x.Login.Equals(login) && BCrypt.Net.BCrypt.Verify(password, x.Password));

            if (user == null)
                throw new Exception();

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
