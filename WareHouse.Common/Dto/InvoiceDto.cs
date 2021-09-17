using System;
using System.Collections.Generic;
using WareHouse.Common.Enum;

namespace WareHouse.Common.Dto
{
    public class GetInvoiceDto
    {
        public long Id { get; set; }
        public InvoicType InvoicType { get; set; }
        public DateTime InvoiceDateTime { get; set; }
        public long CustomerId { get; set; }
        public string InvoiceNumber { get; set; }
        public List<GetInvoiceDetailDto> InvoiceDetails { get; set; }
    }
    public class EditInvoiceDto
    {
        public long Id { get; set; }
        public DateTime InvoiceDateTime { get; set; }
        public long CustomerId { get; set; }
        public string InvoiceNumber { get; set; }
        public List<EditInvoiceDetailDto> InvoiceDetails { get; set; }
    }
    public class AddInvoiceDto
    {
        public InvoicType InvoicType { get; set; }
        public DateTime InvoiceDateTime { get; set; }
        public long CustomerId { get; set; }
        public string InvoiceNumber { get; set; }
        public List<AddInvoiceDetailDto> InvoiceDetails { get; set; }
    }
}
