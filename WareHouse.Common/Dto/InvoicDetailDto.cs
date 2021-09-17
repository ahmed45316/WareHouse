namespace WareHouse.Common.Dto
{
    public class GetInvoiceDetailDto
    {
        public long ItemId { get; set; }
        public string ItemName { get; set; }
        public decimal Quantity { get; set; }
        public decimal Price { get; set; }
        public decimal TotalPrice { get; set; }
        public long InvoicId { get; set; }
    }
    public class EditInvoiceDetailDto
    {
        public long Id { get; set; }
        public long ItemId { get; set; }
        public decimal Quantity { get; set; }
        public decimal Price { get; set; }
        public decimal TotalPrice { get; set; }
        public long InvoicId { get; set; }
    }
    public class AddInvoiceDetailDto
    {
        public long ItemId { get; set; }
        public decimal Quantity { get; set; }
        public decimal Price { get; set; }
        public decimal TotalPrice { get; set; }
        public long InvoicId { get; set; }
    }
}
