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

namespace WareHouse.Service.Services.Customer
{
    public class CustomerServices : BaseServices, ICustomerServices
    {
        public CustomerServices(IUnitOfWork uniteOfWork, IMapper mapper) : base(uniteOfWork, mapper)
        {
        }


        public async Task<long> AddAsync(AddCustomerDto model)
        {
            var customer = Mapper.Map<Entity.Domain.Customer>(model);
            var res = UniteOfWork.GetRepository<Entity.Domain.Customer>().Add(customer);
            await UniteOfWork.SaveChanges();
            return res.Id;
        }

        public async Task EditAsync(EditCustomerDto model)
        {
            var oldCustomer = await UniteOfWork.GetRepository<Entity.Domain.Customer>().GetAsync(model.Id);
            var newCustomer = Mapper.Map(model, oldCustomer);
            UniteOfWork.GetRepository<Entity.Domain.Customer>().Update(newCustomer);
            await UniteOfWork.SaveChanges();
        }

        public async Task DeleteAsync(long id)
        {
            var oldCustomer = await UniteOfWork.GetRepository<Entity.Domain.Customer>().GetAsync(id);
            UniteOfWork.GetRepository<Entity.Domain.Customer>().Remove(oldCustomer);
            await UniteOfWork.SaveChanges();
        }

        public async Task<GetCustomerDto> GetByIdAsync(long id)
        {
            var customer = await UniteOfWork.GetRepository<Entity.Domain.Customer>().GetAsync(id);
            return Mapper.Map<GetCustomerDto>(customer);
        }

        public async Task<IEnumerable<GetCustomerDto>> GetAllAsync()
        {
            var customers = await UniteOfWork.GetRepository<Entity.Domain.Customer>().GetAllAsync();
            return Mapper.Map<IEnumerable<GetCustomerDto>>(customers);
        }

        public async Task<IEnumerable<GetCustomerDto>> FindAsync(CustomerPredicate predicate)
        {
            var Customers = await UniteOfWork.GetRepository<Entity.Domain.Customer>().FindAsync(PredicateBuilderFunction(predicate));
            return Mapper.Map<IEnumerable<GetCustomerDto>>(Customers);
        }
        private static Expression<Func<Entity.Domain.Customer, bool>> PredicateBuilderFunction(CustomerPredicate customerPredicate)
        {
            var predicate = PredicateBuilder.New<Entity.Domain.Customer>(true);
            if (customerPredicate.Id != null)
            {
                predicate = predicate.And(x => x.Id == customerPredicate.Id);
            }
            if (!string.IsNullOrWhiteSpace(customerPredicate.Name))
            {
                predicate = predicate.And(x => x.CustomerName.ToLower().Contains(customerPredicate.Name.ToLower()));
            }
            if (!string.IsNullOrWhiteSpace(customerPredicate.Phone))
            {
                predicate = predicate.And(x => x.CustomerPhone == customerPredicate.Phone);
            }
            return predicate;
        }
    }
}
