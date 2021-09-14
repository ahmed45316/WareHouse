using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WareHouse.Entity.Domain.Base;

namespace WareHouse.Entity.Domain
{
    public class Invoice:BaseDomain
    {
        public DateTime InvoiceDateTime { get; set; }
        public long CustomerId { get; set; }
        [ForeignKey(nameof(CustomerId))]
        public virtual Category Customer { get; set; }
        [StringLength(25)]
        public string InvoiceNumber  { get; set; }
        public virtual ICollection<InvoiceDetail> InvoiceDetails { get; set; }
    }
}
