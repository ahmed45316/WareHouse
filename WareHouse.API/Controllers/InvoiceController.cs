using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using WareHouse.Common.Dto;
using WareHouse.Common.Enum;
using WareHouse.Common.Parameters;
using WareHouse.Service.Services.Invoice;

namespace WareHouse.API.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class InvoiceController : ControllerBase
    {
        private readonly IInvoiceServices _services;

        public InvoiceController(IInvoiceServices services)
        {
            _services = services;
        }
        [HttpPost]
        public async Task<IActionResult> AddAsync(AddInvoiceDto model)
        {
            return Created(Uri.UriSchemeHttp, await _services.AddAsync(model));
        }
        //[HttpPut]
        //public async Task<IActionResult> EditAsync(EditInvoiceDto model)
        //{
        //    //await _services.EditAsync(model);
        //    return Accepted();
        //}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(long id)
        {
            await _services.DeleteAsync(id);
            return Accepted();
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync(long id)
        {
            return Ok(await _services.GetByIdAsync(id));
        }
        [HttpGet]
        public async Task<IActionResult> GetAsync(string invoicNumber, InvoicType invoicType)
        {
            return Ok(await _services.GetByInvoicNumberAsync(invoicNumber, invoicType));
        }
        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            return Ok(await _services.GetAllAsync());
        }
        [HttpPost]
        public async Task<IActionResult> FindAsync(InvoicePredicate predicate)
        {
            return Ok(await _services.FindAsync(predicate));
        }
    }
}
