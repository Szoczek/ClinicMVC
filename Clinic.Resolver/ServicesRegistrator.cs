using Microsoft.Extensions.DependencyInjection;
using Clinic.Services.Abstract;
using Clinic.Services.Implementations;
using Clinic.Database.Abstract;
using Clinic.Database.Implementations;
using Clinic.Database;

namespace Clinic.Resolver
{
    public class ServicesRegistrator
    {
        public static void RegisterServices(IServiceCollection services)
        {
            services.AddSingleton<MongoDbContext>();
            services.AddSingleton<IUnitOfWork, UnitOfWork>();
            services.AddSingleton<IUserService, UserService>();
            services.AddSingleton<IVisitService, VisitService>();
            services.AddSingleton<IFakeDataService, FakeDataService>();
        }
    }
}
