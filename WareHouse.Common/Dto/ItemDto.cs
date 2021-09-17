using System;
using System.Collections.Generic;
using System.Text;

namespace WareHouse.Common.Dto
{
   
    public class GetItemDto
    {
        public long Id { get; set; }
        public string ItemName { get; set; }
        public long CategoryId { get; set; }
        public virtual GetCategoryDto Category { get; set; }
        public string CategoryName { get; set; }
    }
    public class EditItemDto
    {
        public long Id { get; set; }
        public string ItemName { get; set; }
        public long CategoryId { get; set; }

    }
    public class AddItemDto
    {
        public string ItemName { get; set; }
        public long CategoryId { get; set; }

    }
}
