using AutoMapper;
using WareHouse.Common.Abstraction.UnitOfWork;

namespace WareHouse.Service.Services.Base
{
    public class BaseServices : IBaseServices
    {
        protected readonly IUnitOfWork UniteOfWork;
        protected readonly IMapper Mapper;
        public BaseServices(IUnitOfWork uniteOfWork, IMapper mapper)
        {
            UniteOfWork = uniteOfWork;
            Mapper = mapper;
        }
    }
}
