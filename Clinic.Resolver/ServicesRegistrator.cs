using Microsoft.Extensions.DependencyInjection;
using Clinic.Services.Abstract;
using Clinic.Services.Implementations;

namespace Clinic.Resolver
{
    public class ServicesRegistrator
    {
        public static void RegisterServices(IServiceCollection services)
        {
            services.AddSingleton<IFakeDataService, FakeDataService>();
            services.AddSingleton<IUserService, UserService>();
            services.AddSingleton<IVisitService, VisitService>();
        }
    }
}
