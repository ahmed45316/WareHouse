using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AutoMapper;
using LinqKit;
using WareHouse.Common.Abstraction.UnitOfWork;
using WareHouse.Common.Dto;
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
            var invoice = Mapper.Map<Entity.Domain.Invoice>(model);
            var res = UniteOfWork.GetRepository<Entity.Domain.Invoice>().Add(invoice);
            await UniteOfWork.SaveChanges();
            return res.Id;
        }

        public async Task EditAsync(EditInvoiceDto model)
        {
            var oldInvoice = await UniteOfWork.GetRepository<Entity.Domain.Invoice>().GetAsync(model.Id);
            var newInvoice = Mapper.Map(model, oldInvoice);
            UniteOfWork.GetRepository<Entity.Domain.Invoice>().Update(newInvoice);
            await UniteOfWork.SaveChanges();
        }

        public async Task DeleteAsync(long id)
        {
            var oldInvoice = await UniteOfWork.GetRepository<Entity.Domain.Invoice>().GetAsync(id);
            UniteOfWork.GetRepository<Entity.Domain.Invoice>().Remove(oldInvoice);
            await UniteOfWork.SaveChanges();
        }

        public async Task<GetInvoiceDto> GetByIdAsync(long id)
        {
            var Invoice = await UniteOfWork.GetRepository<Entity.Domain.Invoice>().GetAsync(id);
            return Mapper.Map<GetInvoiceDto>(Invoice);
        }

        public async Task<IEnumerable<GetInvoiceDto>> GetAllAsync()
        {
            var Invoices = await UniteOfWork.GetRepository<Entity.Domain.Invoice>().GetAllAsync();
            return Mapper.Map<IEnumerable<GetInvoiceDto>>(Invoices);
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
    }
}
