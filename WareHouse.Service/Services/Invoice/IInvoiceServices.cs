using System.Collections.Generic;
using System.Threading.Tasks;
using WareHouse.Common.Dto;
using WareHouse.Common.Parameters;
using WareHouse.Service.Services.Base;

namespace WareHouse.Service.Services.Invoice
{
    public interface IInvoiceServices : IBaseServices
    {
        Task<long> AddAsync(AddInvoiceDto model);
        Task EditAsync(EditInvoiceDto model);
        Task DeleteAsync(long id);
        Task<GetInvoiceDto> GetByIdAsync(long id);
        Task<IEnumerable<GetInvoiceDto>> GetAllAsync();
        Task<IEnumerable<GetInvoiceDto>> FindAsync(InvoicePredicate predicate);

    }
}
