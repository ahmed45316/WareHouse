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
    public class CustomersController : Controller
    {
        private readonly IRestsharpContainer _restsharpContainer;
        public CustomersController(IRestsharpContainer restsharpContainer)
        {
            _restsharpContainer = restsharpContainer;
        }
        public async Task<IActionResult> Index()
        {
            var customers = await _restsharpContainer.SendRequest<IEnumerable<CustomerVm>>("Customers/GetAll", Method.GET);
            return View(customers);
        }
        public IActionResult Create()
        {
            return View();
        }
        //POST - CREATE
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CustomerVm obj)
        {
            if (ModelState.IsValid)
            {
                await _restsharpContainer.SendRequest<long>("Customers/Add", Method.POST, obj);
                return RedirectToAction("Index");
            }
            return View(obj);

        }
        public async Task<IActionResult> Edit(int? id)
        {
            var customer = await GetCustomer(id);
            if (customer == null) return NotFound();
            return View(customer);
        }

        //POST - EDIT
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(CustomerVm obj)
        {
            if (ModelState.IsValid)
            {
                await _restsharpContainer.SendRequest("Customers/Edit", Method.PUT, obj);
                return RedirectToAction("Index");
            }
            return View(obj);

        }
        //GET - DELETE
        public async Task<IActionResult> Delete(int? id)
        {
            var customer = await GetCustomer(id);
            if (customer == null) return NotFound();
            return View(customer);
        }

        //POST - DELETE
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeletePost(int? id)
        {
            await _restsharpContainer.SendRequest($"Customers/Delete/{id}", Method.DELETE);
            return RedirectToAction("Index");
        }

        private async Task<CustomerVm> GetCustomer(int? id)
        {
            if (id == null || id == 0) return null;
            return await _restsharpContainer.SendRequest<CustomerVm>($"Customers/Get/{id}", Method.GET);
        }
    }
}
