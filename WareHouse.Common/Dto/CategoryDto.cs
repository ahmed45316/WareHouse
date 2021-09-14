using System;
using System.Collections.Generic;
using System.Text;

namespace WareHouse.Common.Dto
{
    public class GetCategoryDto
    {
        public long Id { get; set; }
        public string CategoryName { get; set; }
        
    }
    public class EditCategoryDto
    {
        public long Id { get; set; }
        public string CategoryName { get; set; }
       
    }
    public class AddCategoryDto
    {
        public string CategoryName { get; set; }
     
    }
}

