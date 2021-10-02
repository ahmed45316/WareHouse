using System.Collections.Generic;
using System.Threading.Tasks;
using WareHouse.Common.Core;
using WareHouse.Common.Dto;
using WareHouse.Common.Parameters;
using WareHouse.Service.Services.Base;

namespace WareHouse.Service.Services.Category
{
    public interface ICategoryServices : IBaseServices
    {
        Task<Result<long>> AddAsync(AddCategoryDto model);
        Task EditAsync(EditCategoryDto model);
        Task DeleteAsync(long id);
        Task<GetCategoryDto> GetByIdAsync(long id);
        Task<IEnumerable<GetCategoryDto>> GetAllAsync();
        Task<IEnumerable<GetCategoryDto>> FindAsync(CategoryPredicate predicate);

    }
}
