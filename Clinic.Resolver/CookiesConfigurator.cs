using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

namespace Clinic.Resolver
{
    public class CookiesConfigurator
    {
        public static CookiePolicyOptions ConfigureCookies()
        {
            return new CookiePolicyOptions()
            {
                CheckConsentNeeded = context => true,
                MinimumSameSitePolicy = SameSiteMode.None
            };
        }
    }
}
