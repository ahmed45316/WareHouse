using System.Collections.Generic;
using System.Threading.Tasks;
using WareHouse.Common.Dto;
using WareHouse.Common.Enum;
using WareHouse.Common.Parameters;
using WareHouse.Service.Services.Base;

namespace WareHouse.Service.Services.Invoice
{
    public interface IInvoiceServices : IBaseServices
    {
        Task<long> AddAsync(AddInvoiceDto model);
        //Task EditAsync(EditInvoiceDto model);
        Task DeleteAsync(long id);
        Task<GetInvoiceDto> GetByIdAsync(long id);
        Task<GetInvoiceDto> GetByInvoicNumberAsync(string invoicNumber, InvoicType invoicType);
        Task<IEnumerable<GetInvoiceDto>> GetAllAsync();
        Task<IEnumerable<GetInvoiceDto>> FindAsync(InvoicePredicate predicate);

    }
}
