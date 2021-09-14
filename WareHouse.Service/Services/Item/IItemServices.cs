using System.Collections.Generic;
using System.Threading.Tasks;
using WareHouse.Common.Dto;
using WareHouse.Common.Parameters;
using WareHouse.Service.Services.Base;

namespace WareHouse.Service.Services.Item
{
    public interface IItemServices : IBaseServices
    {
        Task<long> AddAsync(AddItemDto model);
        Task EditAsync(EditItemDto model);
        Task DeleteAsync(long id);
        Task<GetItemDto> GetByIdAsync(long id);
        Task<IEnumerable<GetItemDto>> GetAllAsync();
        Task<IEnumerable<GetItemDto>> FindAsync(ItemPredicate predicate);

    }
}
