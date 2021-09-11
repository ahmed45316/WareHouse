using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WareHouse.Common.Dto;
using WareHouse.Common.Parameters;
using WareHouse.Service.Services.Customer;

namespace WareHouse.API.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly ICustomerServices _services;

        public CustomersController(ICustomerServices services)
        {
            _services = services;
        }
        [HttpPost]
        public async Task<IActionResult> AddAsync(AddCustomerDto model)
        {
            return Created(Uri.UriSchemeHttp, await _services.AddAsync(model));
        }
        [HttpPut]
        public async Task<IActionResult> EditAsync(EditCustomerDto model)
        {
            await _services.EditAsync(model);
            return Accepted();
        }
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
        public async Task<IActionResult> GetAllAsync()
        {
            return Ok(await _services.GetAllAsync());
        }
        [HttpPost]
        public async Task<IActionResult> FindAsync(CustomerPredicate predicate)
        {
            return Ok(await _services.FindAsync(predicate));
        }
    }
}
