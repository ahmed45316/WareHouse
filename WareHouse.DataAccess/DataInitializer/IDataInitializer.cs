using System.Collections.Generic;
using WareHouse.Entity.Domain;

namespace WareHouse.DataAccess.DataInitializer
{
    public interface IDataInitializer
    {
        IEnumerable<Category> SeedCategories(); 
        IEnumerable<Customer> SeedCustomers();
}
}