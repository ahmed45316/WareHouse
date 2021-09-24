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
    public class ItemsController : Controller
    {
        private readonly IRestsharpContainer _restsharpContainer;
        public ItemsController(IRestsharpContainer restsharpContainer)
        {
            _restsharpContainer = restsharpContainer;
        }
        public async Task<IActionResult> Index()
        {
            var items = await _restsharpContainer.SendRequest<IEnumerable<ItemVm>>("Item/GetAll", Method.GET);
            return View(items);
        }
        public IActionResult Create()
        {
            return View();
        }
        //POST - CREATE
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ItemVm obj)
        {
            if (ModelState.IsValid)
            {
                await _restsharpContainer.SendRequest<long>("Item/Add", Method.POST, obj);
                return RedirectToAction("Index");
            }
            return View(obj);

        }
        public async Task<IActionResult> Edit(int? id)
        {
            var item = await GetItem(id);
            if (item == null) return NotFound();
            return View(item);
        }

        //POST - EDIT
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(ItemVm obj)
        {
            if (ModelState.IsValid)
            {
                await _restsharpContainer.SendRequest("Item/Edit", Method.PUT, obj);
                return RedirectToAction("Index");
            }
            return View(obj);

        }
        //GET - DELETE
        public async Task<IActionResult> Delete(int? id)
        {
            var item = await GetItem(id);
            if (item == null) return NotFound();
            return View(item);
        }

        //POST - DELETE
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeletePost(int? id)
        {
            await _restsharpContainer.SendRequest($"Item/Delete/{id}", Method.DELETE);
            return RedirectToAction("Index");
        }

        private async Task<ItemVm> GetItem(int? id)
        {
            if (id == null || id == 0) return null;
            return await _restsharpContainer.SendRequest<ItemVm>($"Item/Get/{id}", Method.GET);
        }
    }
}
