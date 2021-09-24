using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AutoMapper;
using LinqKit;
using Microsoft.EntityFrameworkCore;
using WareHouse.Common.Abstraction.UnitOfWork;
using WareHouse.Common.Dto;
using WareHouse.Common.Parameters;
using WareHouse.Service.Services.Base;

namespace WareHouse.Service.Services.Item
{
    public class ItemServices : BaseServices, IItemServices
    {
        public ItemServices(IUnitOfWork uniteOfWork, IMapper mapper) : base(uniteOfWork, mapper)
        {
        }


        public async Task<long> AddAsync(AddItemDto model)
        {
            var Item = Mapper.Map<Entity.Domain.Item>(model);
            var res = UniteOfWork.GetRepository<Entity.Domain.Item>().Add(Item);
            await UniteOfWork.SaveChanges();
            return res.Id;
        }

        public async Task EditAsync(EditItemDto model)
        {
            var oldItem = await UniteOfWork.GetRepository<Entity.Domain.Item>().GetAsync(model.Id);
            var newItem = Mapper.Map(model, oldItem);
            UniteOfWork.GetRepository<Entity.Domain.Item>().Update(newItem);
            await UniteOfWork.SaveChanges();
        }

        public async Task DeleteAsync(long id)
        {
            var oldItem = await UniteOfWork.GetRepository<Entity.Domain.Item>().GetAsync(id);
            UniteOfWork.GetRepository<Entity.Domain.Item>().Remove(oldItem);
            await UniteOfWork.SaveChanges();
        }

        public async Task<GetItemDto> GetByIdAsync(long id)
        {
            // var item = await UniteOfWork.GetRepository<Entity.Domain.Item>().FirstOrDefaultAsync(q => q.Id == id, include: src => src.Include(q => q.Category));
            // return Mapper.Map<GetItemDto>(item);
            var item = await UniteOfWork.GetRepository<Entity.Domain.Item>().FirstOrDefaultSelectAsync(q =>
                new GetItemDto
                {
                    CategoryId = q.CategoryId,
                    CategoryName = q.Category.CategoryName,
                    Id = q.Id,
                    ItemName = q.ItemName
                }, q => q.Id == id);
            return item;
        }

        //public async Task<IEnumerable<GetItemDto>> GetAllAsync()
        //{
        //    var Items = await UniteOfWork.GetRepository<Entity.Domain.Item>().GetAllAsync();
        //    return Mapper.Map<IEnumerable<GetItemDto>>(Items);
        //}

        public async Task<IEnumerable<GetItemDto>> GetAllAsync()
        {
            var item = await UniteOfWork.GetRepository<Entity.Domain.Item>().FindSelectAsync(q =>
                new GetItemDto
                {
                    CategoryId = q.CategoryId,
                    CategoryName = q.Category.CategoryName,
                    Id = q.Id,
                    ItemName = q.ItemName
                });
            return item;
        }

        public async Task<IEnumerable<GetItemDto>> FindAsync(ItemPredicate predicate)
        {
            var Items = await UniteOfWork.GetRepository<Entity.Domain.Item>().FindAsync(PredicateBuilderFunction(predicate));
            return Mapper.Map<IEnumerable<GetItemDto>>(Items);
        }
        private static Expression<Func<Entity.Domain.Item, bool>> PredicateBuilderFunction(ItemPredicate itemPredicate)
        {
            var predicate = PredicateBuilder.New<Entity.Domain.Item>(true);
            if (itemPredicate.Id != null)
            {
                predicate = predicate.And(x => x.Id == itemPredicate.Id);
            }
            if (!string.IsNullOrWhiteSpace(itemPredicate.Name))
            {
                predicate = predicate.And(x => x.ItemName.ToLower().Contains(itemPredicate.Name.ToLower()));
            }

            return predicate;
        }
    }
}
