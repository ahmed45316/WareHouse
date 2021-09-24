using Microsoft.AspNetCore.Mvc;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WareHouse.Mvc.Models;
using WareHouse.Mvc.RestSharp;

namespace WareHouse.Mvc.Controllers
{
    public class CategoriesController : Controller
    {
        private readonly IRestsharpContainer _restsharpContainer;
        public CategoriesController(IRestsharpContainer restsharpContainer)
        {
            _restsharpContainer = restsharpContainer;
        }
        public async Task<IActionResult> Index()
        {
            var categries = await _restsharpContainer.SendRequest<IEnumerable<CategoryVm>>("Category/GetAll", Method.GET);
            return View(categries);
        }
        public IActionResult Create()
        {
            return View();
        }
        //POST - CREATE
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CategoryVm obj)
        {
            if (ModelState.IsValid)
            {
                await _restsharpContainer.SendRequest<long>("Category/Add", Method.POST, obj);
                return RedirectToAction("Index");
            }
            return View(obj);

        }
        public async Task<IActionResult> Edit(int? id)
        {
            var category = await GetCategory(id);
            if (category == null) return NotFound();
            return View(category);
        }

        //POST - EDIT
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(CategoryVm obj)
        {
            if (ModelState.IsValid)
            {
                await _restsharpContainer.SendRequest("Category/Edit", Method.PUT, obj);
                return RedirectToAction("Index");
            }
            return View(obj);

        }
        //GET - DELETE
        public async Task<IActionResult> Delete(int? id)
        {
            var category = await GetCategory(id);
            if (category == null) return NotFound();
            return View(category);
        }

        //POST - DELETE
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeletePost(int? id)
        {
            await _restsharpContainer.SendRequest($"Category/Delete/{id}", Method.DELETE);
            return RedirectToAction("Index");
        }

        private async Task<CategoryVm> GetCategory(int? id)
        {
            if (id == null || id == 0) return null;
            return await _restsharpContainer.SendRequest<CategoryVm>($"Category/Get/{id}", Method.GET);
        }
    }
}
