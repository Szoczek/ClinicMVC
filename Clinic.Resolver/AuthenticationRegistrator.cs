using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace Clinic.Resolver
{
    public class AuthenticationRegistrator
    {
        private const string LOGIN_PATH = "/User/Login";
        private const string LOGOUT_PATH = "/User/Logout";
        private const string COOKIE_NAME = "ClinicLoginCookie";
        public static void RegisterAuthentication(IServiceCollection services)
        {
            services.AddAuthentication(options =>
            {
                options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            }).AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, options =>
            {
                options.LoginPath = LOGIN_PATH;
                options.LogoutPath = LOGOUT_PATH;
                options.AccessDeniedPath = CookieAuthenticationDefaults.AccessDeniedPath;
                options.Cookie = new CookieBuilder
                {
                    Name = COOKIE_NAME
                };
            });
        }
    }
}
