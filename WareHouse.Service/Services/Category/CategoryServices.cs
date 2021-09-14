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

namespace WareHouse.Service.Services.Category
{
    public class CategoryServices : BaseServices, ICategoryServices
    {
        public CategoryServices(IUnitOfWork uniteOfWork, IMapper mapper) : base(uniteOfWork, mapper)
        {
        }


        public async Task<long> AddAsync(AddCategoryDto model)
        {
            var Category = Mapper.Map<Entity.Domain.Category>(model);
            var res = UniteOfWork.GetRepository<Entity.Domain.Category>().Add(Category);
            await UniteOfWork.SaveChanges();
            return res.Id;
        }

        public async Task EditAsync(EditCategoryDto model)
        {
            var oldCategory = await UniteOfWork.GetRepository<Entity.Domain.Category>().GetAsync(model.Id);
            var newCategory = Mapper.Map(model, oldCategory);
            UniteOfWork.GetRepository<Entity.Domain.Category>().Update(newCategory);
            await UniteOfWork.SaveChanges();
        }

        public async Task DeleteAsync(long id)
        {
            var oldCategory = await UniteOfWork.GetRepository<Entity.Domain.Category>().GetAsync(id);
            UniteOfWork.GetRepository<Entity.Domain.Category>().Remove(oldCategory);
            await UniteOfWork.SaveChanges();
        }

        public async Task<GetCategoryDto> GetByIdAsync(long id)
        {
            var Category = await UniteOfWork.GetRepository<Entity.Domain.Category>().GetAsync(id);
            return Mapper.Map<GetCategoryDto>(Category);
        }

        public async Task<IEnumerable<GetCategoryDto>> GetAllAsync()
        {
            var Categorys = await UniteOfWork.GetRepository<Entity.Domain.Category>().GetAllAsync();
            return Mapper.Map<IEnumerable<GetCategoryDto>>(Categorys);
        }

        public async Task<IEnumerable<GetCategoryDto>> FindAsync(CategoryPredicate predicate)
        {
            var Categorys = await UniteOfWork.GetRepository<Entity.Domain.Category>().FindAsync(PredicateBuilderFunction(predicate));
            return Mapper.Map<IEnumerable<GetCategoryDto>>(Categorys);
        }
        private static Expression<Func<Entity.Domain.Category, bool>> PredicateBuilderFunction(CategoryPredicate categoryPredicate)
        {
            var predicate = PredicateBuilder.New<Entity.Domain.Category>(true);
            if (categoryPredicate.Id != null)
            {
                predicate = predicate.And(x => x.Id == categoryPredicate.Id);
            }
            if (!string.IsNullOrWhiteSpace(categoryPredicate.Name))
            {
                predicate = predicate.And(x => x.CategoryName.ToLower().Contains(categoryPredicate.Name.ToLower()));
            }

            return predicate;
        }
    }
}
