using System;
using System.Collections.Generic;
using System.Text;

namespace WareHouse.Common.Dto
{
    public class GetInvoiceDto
    {
        public long Id { get; set; }
        public DateTime InvoiceDateTime { get; set; }
        public long CustomerId { get; set; }
        public string InvoiceNumber { get; set; }
    }
    public class EditInvoiceDto
    {
        public long Id { get; set; }
        public DateTime InvoiceDateTime { get; set; }
        public long CustomerId { get; set; }
        public string InvoiceNumber { get; set; }
    }
    public class AddInvoiceDto
    {
        public DateTime InvoiceDateTime { get; set; }
        public long CustomerId { get; set; }
        public string InvoiceNumber { get; set; }
    }
}
