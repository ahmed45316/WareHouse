using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WareHouse.Entity.Domain.Base;

namespace WareHouse.Entity.Domain
{
    public class Customer : BaseDomain
    {
        [StringLength(100)]
        public string CustomerName { get; set; }
        [Column(TypeName = "varchar(20)")]
        public string CustomerPhone { get; set; }
        public virtual ICollection<Invoice> Invoices { get; set; }
    }
}
