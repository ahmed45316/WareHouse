using Microsoft.EntityFrameworkCore;
using WareHouse.DataAccess.Configuration;
using WareHouse.DataAccess.DataInitializer;
using WareHouse.Entity.Domain;

namespace WareHouse.DataAccess.Context
{
    public class WareHouseContext : DbContext
    {
        private readonly IDataInitializer _dataInitializer;
        public WareHouseContext(DbContextOptions options, IDataInitializer dataInitializer) : base(options)
        {
            _dataInitializer = dataInitializer;
        }

        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Item> Items { get; set; }
        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<Invoice> Invoices { get; set; }
        public virtual DbSet<InvoiceDetail> InvoiceDetails { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            #region Configuration
            modelBuilder.ApplyConfiguration(new CategoryConfig());
            #endregion

            #region Seed
            modelBuilder.Entity<Category>().HasData(_dataInitializer.SeedCategories());
            modelBuilder.Entity<Customer>().HasData(_dataInitializer.SeedCustomers());
            #endregion
            base.OnModelCreating(modelBuilder);
        }
    }
}
