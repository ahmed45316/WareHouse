using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;
using WareHouse.Common.Abstraction.Repository;
using WareHouse.Common.Abstraction.UnitOfWork;

namespace WareHouse.DataAccess.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork 
    {
        private DbContext _context;
        private readonly IServiceProvider _serviceProvider;
        public UnitOfWork(DbContext context, IServiceProvider serviceProvider)
        {
            _context = context;
            _serviceProvider = serviceProvider;
        }
        public IRepository<TB> GetRepository<TB>() where TB : class => _serviceProvider.GetRequiredService<IRepository<TB>>();
        public async Task<int> SaveChanges()
        {
            return await _context.SaveChangesAsync();
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        protected virtual void Dispose(bool disposing)
        {
            if (!disposing)
            {
                return;
            }

            if (_context == null)
            {
                return;
            }

            _context.Dispose();
            _context = null;
        }
    }
}