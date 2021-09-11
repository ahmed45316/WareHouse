using System;
using System.Collections.Generic;
using WareHouse.Entity.Domain;

namespace WareHouse.DataAccess.DataInitializer
{
    public class DataInitializer : IDataInitializer
    {
        public IEnumerable<Category> SeedCategories()
        {
            return new List<Category>
            {
                new Category
                {
                    Id = 1,
                    CategoryName = "General",
                    CreatedDate = new DateTime(2021,9,11),
                    CreatedBy = 1
                }
            };
        }

        public IEnumerable<Customer> SeedCustomers()
        {
            return new List<Customer>
            {
                new Customer
                {
                    Id = 1,
                    CustomerName = "General",
                    CreatedDate = new DateTime(2021,9,11),
                    CreatedBy = 1,
                    CustomerPhone = "010"
                }
            };
        }
    }
}