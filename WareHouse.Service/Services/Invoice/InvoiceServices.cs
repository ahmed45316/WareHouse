using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AutoMapper;
using LinqKit;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Update.Internal;
using WareHouse.Common.Abstraction.UnitOfWork;
using WareHouse.Common.Dto;
using WareHouse.Common.Enum;
using WareHouse.Common.Helper.Cl;
using WareHouse.Common.Parameters;
using WareHouse.Service.Services.Base;

namespace WareHouse.Service.Services.Invoice
{
    public class InvoiceServices : BaseServices, IInvoiceServices
    {
        public InvoiceServices(IUnitOfWork uniteOfWork, IMapper mapper) : base(uniteOfWork, mapper)
        {
        }


        public async Task<long> AddAsync(AddInvoiceDto model)
        {
            if (model.InvoicType == InvoicType.InvoiceSell || model.InvoicType == InvoicType.InvoiceBuyBack)
            {
                var list = (await CheckItemStock(model.InvoiceDetails.Select(q => q.ItemId))).ToList();
                var availableInvoiceDetails = model.InvoiceDetails.Where(q => q.Quantity <= list.FirstOrDefault(u => u.ItemId == q.ItemId)?.Quantity);
                if (!model.IsContinue && availableInvoiceDetails.Count() != model.InvoiceDetails.Count) return 0;
            }
            var invoice = Mapper.Map<Entity.Domain.Invoice>(model);
            var res = UniteOfWork.GetRepository<Entity.Domain.Invoice>().Add(invoice);
            await UniteOfWork.SaveChanges();
            return res.Id;
        }

        //public async Task EditAsync(EditInvoiceDto model)
        //{
        //    var oldInvoice = await UniteOfWork.GetRepository<Entity.Domain.Invoice>().GetAsync(model.Id);
        //    var newInvoice = Mapper.Map(model, oldInvoice);
        //    UniteOfWork.GetRepository<Entity.Domain.Invoice>().Update(newInvoice);
        //    await UniteOfWork.SaveChanges();
        //}

        public async Task DeleteAsync(long id)
        {
            var oldInvoice = await UniteOfWork.GetRepository<Entity.Domain.Invoice>().GetAsync(id);
            UniteOfWork.GetRepository<Entity.Domain.Invoice>().Remove(oldInvoice);
            await UniteOfWork.SaveChanges();
        }

        public async Task<GetInvoiceDto> GetByIdAsync(long id)
        {
            var invoice = await UniteOfWork.GetRepository<Entity.Domain.Invoice>().FirstOrDefaultAsync(q => q.Id == id, include: src => src.Include(q => q.InvoiceDetails));
            return Mapper.Map<GetInvoiceDto>(invoice);
        }
        public async Task<GetInvoiceDto> GetByInvoicNumberAsync(string invoicNumber, InvoicType invoicType)
        {
            var invoice = await UniteOfWork.GetRepository<Entity.Domain.Invoice>().FirstOrDefaultSelectAsync(q =>
                new GetInvoiceDto
                {
                    Id = q.Id,
                    InvoiceDetails = Mapper.Map<List<GetInvoiceDetailDto>>(q.InvoiceDetails),
                    InvoicTypeName = q.InvoicType.ToString(),
                    CustomerName = q.Customer.CustomerName,
                    InvoiceDateTime = q.InvoiceDateTime,
                    InvoiceTotal = q.InvoiceDetails.Sum(r=>r.TotalPrice)

                }, q => q.InvoiceNumber == invoicNumber && q.InvoicType == invoicType);
            return invoice;
        }

        public async Task<IEnumerable<GetInvoiceDto>> GetAllAsync()
        {
            //var Invoices = await UniteOfWork.GetRepository<Entity.Domain.Invoice>().GetAllAsync();
            //return Mapper.Map<IEnumerable<GetInvoiceDto>>(Invoices);
            var invoices = await UniteOfWork.GetRepository<Entity.Domain.Invoice>().FindSelectAsync(q =>
               new GetInvoiceDto
               {
                   Id = q.Id,
                   InvoiceDetails = Mapper.Map<List<GetInvoiceDetailDto>>(q.InvoiceDetails),
                   InvoicTypeName = q.InvoicType.ToString(),
                   CustomerName = q.Customer.CustomerName,
                   InvoiceDateTime = q.InvoiceDateTime,
                   InvoiceTotal = q.InvoiceDetails.Sum(r => r.TotalPrice)

               } );
            return invoices;
        }

        public async Task<IEnumerable<GetInvoiceDto>> FindAsync(InvoicePredicate predicate)
        {
            var Invoices = await UniteOfWork.GetRepository<Entity.Domain.Invoice>().FindAsync(PredicateBuilderFunction(predicate));
            return Mapper.Map<IEnumerable<GetInvoiceDto>>(Invoices);
        }
        private static Expression<Func<Entity.Domain.Invoice, bool>> PredicateBuilderFunction(InvoicePredicate invoicePredicate)
        {
            var predicate = PredicateBuilder.New<Entity.Domain.Invoice>(true);
            if (invoicePredicate.Id != null)
            {
                predicate = predicate.And(x => x.Id == invoicePredicate.Id);
            }
            //if (!longIsNullOrWhiteSpace(invoicePredicate.CustomerId))
            //{
            //    predicate = predicate.And(x => x.CustomerId==invoicePredicate.CustomerId);
            //}

            return predicate;
        }

        private async Task<IEnumerable<CheckItemStock>> CheckItemStock(IEnumerable<long> itemIdsList)
        {
            var items = (await UniteOfWork.GetRepository<Entity.Domain.InvoiceDetail>().FindSelectAsync(q => new
            {
                q.ItemId,
                q.Quantity,
                q.Invoice.InvoicType
            }, q => itemIdsList.Contains(q.ItemId))).ToList();

            var list = items.GroupBy(t => t.ItemId, (key, value) => new CheckItemStock
            {
                ItemId = key,
                Quantity =
                    value.Where(q => q.InvoicType == InvoicType.InvoiceBuy || q.InvoicType == InvoicType.InvoiceSellBack).Sum(t => t.Quantity) - value.Where(q => q.InvoicType == InvoicType.InvoiceSell || q.InvoicType == InvoicType.InvoiceBuyBack).Sum(t => t.Quantity)
            }).ToList();
            return list;
        }
    }
}
