using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WareHouse.Common.Dto;
using WareHouse.Common.Parameters;
using WareHouse.Service.Services.Item;

namespace WareHouse.API.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class ItemController : ControllerBase
    {
        private readonly IItemServices _services;

        public ItemController(IItemServices services)
        {
            _services = services;
        }
        [HttpPost]
        public async Task<IActionResult> AddAsync(AddItemDto model)
        {
            return Created(Uri.UriSchemeHttp, await _services.AddAsync(model));
        }
        [HttpPut]
        public async Task<IActionResult> EditAsync(EditItemDto model)
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
        [HttpGet("{id}")]
        public async Task<IActionResult> GetByCategoryIdAsync(long id)
        {
            return Ok(await _services.GetByCategoryIdAsync(id));
        }
        
        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            return Ok(await _services.GetAllAsync());
        }
        [HttpPost]
        public async Task<IActionResult> FindAsync(ItemPredicate predicate)
        {
            return Ok(await _services.FindAsync(predicate));
        }
    }
}
