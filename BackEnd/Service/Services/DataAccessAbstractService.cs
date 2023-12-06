using Common.Interfaces.Service;
using Common.Repository;

namespace Service.Services
{
    public abstract class DataAccessAbstractService
    {
        protected readonly IUnitOfWork UoW;

        public DataAccessAbstractService(IUnitOfWork unitOfWork)
        {
            UoW = unitOfWork;
        }
    }
}