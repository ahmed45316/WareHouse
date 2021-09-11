using System;

namespace WareHouse.Entity.Domain.Base
{
    public class BaseDomain
    {
        public long Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public long CreatedBy { get; set; }
    }
}
