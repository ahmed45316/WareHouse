using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace WareHouse.Mvc.Models
{
    public class InvoiceVm
    {
        
        public long? Id { get; set; }
      
       // public InvoicType InvoicType { get; set; }
        public long InvoicType { get; set; }
        public string InvoicTypeName { get; set; }
        [Required(ErrorMessage = "*")]
        public DateTime InvoiceDateTime { get; set; }
        [Required(ErrorMessage = "*")]
        public long CustomerId { get; set; }
        [DisplayName("Customer Name")]
        public string CustomerName { get; set; }
        [Required(ErrorMessage = "*")]
        public string InvoiceNumber { get; set; }
        public decimal InvoiceTotal { get; set; }
        public List<InvoiceDetailVm> InvoiceDetails { get; set; }
    }
}
