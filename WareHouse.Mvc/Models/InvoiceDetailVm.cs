namespace WareHouse.Mvc.Models
{
    public class InvoiceDetailVm
    {
        public long? Id { get; set; }
        public long CategoryId { get; set; }
        public long ItemId { get; set; }
        public decimal Quantity { get; set; }
        public decimal Price { get; set; }
        public decimal TotalPrice { get; set; }
        public long? InvoicId { get; set; }
    }
}
