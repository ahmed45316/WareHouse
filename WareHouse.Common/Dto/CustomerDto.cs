namespace WareHouse.Common.Dto
{
    public class GetCustomerDto
    {
        public long Id { get; set; }
        public string CustomerName { get; set; }
        public string CustomerPhone { get; set; }
    }
    public class EditCustomerDto
    {
        public long Id { get; set; }
        public string CustomerName { get; set; }
        public string CustomerPhone { get; set; }
    }
    public class AddCustomerDto
    {
        public string CustomerName { get; set; }
        public string CustomerPhone { get; set; }
    }
}
