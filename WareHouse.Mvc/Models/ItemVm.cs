using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using WareHouse.Common.Dto;

namespace WareHouse.Mvc.Models
{
    public class ItemVm
    {
        public long Id { get; set; }
        [Required(ErrorMessage = "*")]
        [DisplayName("Item Name")]
        public string ItemName { get; set; }
        [Required(ErrorMessage = "*")]
        public long CategoryId { get; set; }
       public string CategoryName { get; set; }
    }
}
