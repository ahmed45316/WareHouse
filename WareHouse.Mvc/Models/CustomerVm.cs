using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace WareHouse.Mvc.Models
{
    public class CustomerVm
    {

        public long Id { get; set; }
        [DisplayName("Customer Name")]
        [Required(ErrorMessage = "*")]
        public string CustomerName { get; set; }
        [DisplayName("Customer phone")]
        public string CustomerPhone { get; set; }
    }
}
