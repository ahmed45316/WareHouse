using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WareHouse.Mvc.Models;
using WareHouse.Mvc.RestSharp;

namespace WareHouse.Mvc.Controllers
{
    public class InvoicesController : Controller
    {
        private readonly IRestsharpContainer _restsharpContainer;
        public InvoicesController(IRestsharpContainer restsharpContainer)
        {
            _restsharpContainer = restsharpContainer;
        }
        public async Task<IActionResult> Index()
        {
            var invoices = await _restsharpContainer.SendRequest<IEnumerable<InvoiceVm>>("Invoice/GetAll", Method.GET);
            return View(invoices);
        }
        public async Task<IActionResult> Create()
        {
            await GetAllCustomers();
            return View();
        }
        //POST - CREATE
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(InvoiceVm obj)
        {
            if (ModelState.IsValid)
            {
                await _restsharpContainer.SendRequest<long>("Invoice/Add", Method.POST, obj);
                return RedirectToAction("Index");
            }
            return View(obj);

        }
        public async Task<IActionResult> Edit(int? id)
        {
            await GetAllCustomers();
            var invoice = await GetInvoice(id);
            if (invoice == null) return NotFound();
            return View(invoice);
        }

        //POST - EDIT
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(InvoiceVm obj)
        {
            if (ModelState.IsValid)
            {
                await _restsharpContainer.SendRequest("Invoice/Edit", Method.PUT, obj);
                return RedirectToAction("Index");
            }
            return View(obj);

        }
        //GET - DELETE
        public async Task<IActionResult> Delete(int? id)
        {
            var invoice = await GetInvoice(id);
            if (invoice == null) return NotFound();
            return View(invoice);
        }

        //POST - DELETE
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeletePost(int? id)
        {
            await _restsharpContainer.SendRequest($"Invoice/Delete/{id}", Method.DELETE);
            return RedirectToAction("Index");
        }

        private async Task<InvoiceVm> GetInvoice(int? id)
        {
            if (id == null || id == 0) return null;
            return await _restsharpContainer.SendRequest<InvoiceVm>($"Invoice/Get/{id}", Method.GET);
        }
        private async Task GetAllCustomers()
        {
            var list = await _restsharpContainer.SendRequest<IEnumerable<CustomerVm>>("Customers/GetAll", Method.GET);
            ViewBag.ListOfCustomers = new SelectList(list, "Id", "CustomerName");
        }
    }
}
