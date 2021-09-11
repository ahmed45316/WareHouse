using System;
using System.Threading.Tasks;
using WareHouse.Common.Abstraction.Repository;

namespace WareHouse.Common.Abstraction.UnitOfWork
{
    public interface IUnitOfWork : IDisposable 
    {
        IRepository<TB> GetRepository<TB>() where TB : class;
        Task<int> SaveChanges();
    }
}
