using System.Collections.Generic;
using System.Threading.Tasks;
using WareHouse.Common.Dto;
using WareHouse.Common.Parameters;
using WareHouse.Service.Services.Base;

namespace WareHouse.Service.Services.Customer
{
    public interface ICustomerServices : IBaseServices
    {
        Task<long> AddAsync(AddCustomerDto model);
        Task EditAsync(EditCustomerDto model);
        Task DeleteAsync(long id);
        Task<GetCustomerDto> GetByIdAsync(long id);
        Task<IEnumerable<GetCustomerDto>> GetAllAsync();
        Task<IEnumerable<GetCustomerDto>> FindAsync(CustomerPredicate predicate);

    }
}
