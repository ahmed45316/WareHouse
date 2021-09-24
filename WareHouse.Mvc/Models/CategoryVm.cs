using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace WareHouse.Mvc.Models
{
    public class CategoryVm
    {
        public long Id { get; set; }
        [DisplayName("Category name")]
        [Required(ErrorMessage ="*")]
        public string CategoryName { get; set; }
    }
}
