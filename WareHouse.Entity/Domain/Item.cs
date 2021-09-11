using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WareHouse.Entity.Domain.Base;

namespace WareHouse.Entity.Domain
{
    public class Item : BaseDomain
    {
        [StringLength(100)]
        public string ItemName { get; set; }
        public long CategoryId { get; set; }
        [ForeignKey(nameof(CategoryId))]
        public virtual Category Category { get; set; }
        public virtual ICollection<InvoiceDetail> InvoiceDetails { get; set; }
    }
}
