using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using WareHouse.Entity.Domain.Base;

namespace WareHouse.Entity.Domain
{
    public class Category : BaseDomain
    {
        [StringLength(50)]
        public string CategoryName { get; set; }
        public virtual ICollection<Item> Items { get; set; } = new Collection<Item>();
    }
}
