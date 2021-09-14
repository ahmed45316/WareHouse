using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WareHouse.Common.Dto;
using WareHouse.Common.Parameters;
using WareHouse.Service.Services.Category;

namespace WareHouse.API.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryServices _services;

        public CategoryController(ICategoryServices services)
        {
            _services = services;
        }
        [HttpPost]
        public async Task<IActionResult> AddAsync(AddCategoryDto model)
        {
            return Created(Uri.UriSchemeHttp, await _services.AddAsync(model));
        }
        [HttpPut]
        public async Task<IActionResult> EditAsync(EditCategoryDto model)
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
        public async Task<IActionResult> FindAsync(CategoryPredicate predicate)
        {
            return Ok(await _services.FindAsync(predicate));
        }
    }
}
