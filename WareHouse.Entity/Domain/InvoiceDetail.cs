using System.ComponentModel.DataAnnotations.Schema;
using WareHouse.Entity.Domain.Base;

namespace WareHouse.Entity.Domain
{
    public class InvoiceDetail:BaseDomain
    {
        public long ItemId { get; set; }
        [ForeignKey(nameof(ItemId))]
        public virtual Item Item { get; set; }
        [Column(TypeName = "decimal(18,3)")]
        public decimal Quantity { get; set; }
        [Column(TypeName = "decimal(18,3)")]
        public decimal Price { get; set; }
        [Column(TypeName = "decimal(18,3)")]
        public decimal TotalPrice { get; set; }
        public long InvoicId { get; set; }
        [ForeignKey(nameof(InvoicId))]
        public virtual Invoice Invoice { get; set; }
    }
}
