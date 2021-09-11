using AutoMapper;
using WareHouse.Common.Dto;
using WareHouse.Entity.Domain;

namespace WareHouse.Service.Mapping
{
    public class WareHouseMapping : Profile
    {
        public WareHouseMapping()
        {
            MapCustomerProfile();
        }
        private void MapCustomerProfile()
        {
            CreateMap<GetCustomerDto, Customer>().ReverseMap();
            //.ForMember(dest=>dest.CustomerName,opt=>opt.MapFrom(src=>src.CustomerName));
            CreateMap<AddCustomerDto, Customer>().ReverseMap();
            CreateMap<EditCustomerDto, Customer>().ReverseMap();
        }
    }
}
