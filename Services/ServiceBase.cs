using Clinic.Repositories.Abstract;

namespace Clinic.Services
{
    public class ServiceBase
    {
        protected IUnitOfWork _unitOfWork;

        public ServiceBase(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
    }
}
