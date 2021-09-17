using AutoMapper;
using WareHouse.Common.Dto;
using WareHouse.Entity.Domain;

namespace WareHouse.Service.Mapping
{
    public class WareHouseMapping : Profile
    {
        public WareHouseMapping()
        {
            MapCategoryProfile();
            MapCustomerProfile();
            MapItemProfile();
            MapInvoiceProfile();
            MapInvoiceDetailProfile();
        }
        private void MapCustomerProfile()
        {
            CreateMap<GetCustomerDto, Customer>().ReverseMap();
            //.ForMember(dest=>dest.CustomerName,opt=>opt.MapFrom(src=>src.CustomerName));
            CreateMap<AddCustomerDto, Customer>().ReverseMap();
            CreateMap<EditCustomerDto, Customer>().ReverseMap();
        }
        private void MapCategoryProfile()
        {
            CreateMap<GetCategoryDto, Category>().ReverseMap();
            //.ForMember(dest=>dest.CustomerName,opt=>opt.MapFrom(src=>src.CustomerName));
            CreateMap<AddCategoryDto, Category>().ReverseMap();
            CreateMap<EditCategoryDto, Category>().ReverseMap();
        }
        private void MapItemProfile()
        {
            CreateMap<GetItemDto, Item>().ReverseMap()
            .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category == null ? null : src.Category.CategoryName));
            CreateMap<AddItemDto, Item>().ReverseMap();
            CreateMap<EditItemDto, Item>().ReverseMap();
        }
        private void MapInvoiceProfile()
        {
            CreateMap<GetInvoiceDto, Invoice>().ReverseMap();
            //.ForMember(dest=>dest.CustomerName,opt=>opt.MapFrom(src=>src.CustomerName));
            CreateMap<AddInvoiceDto, Invoice>().ReverseMap();
            CreateMap<EditInvoiceDto, Invoice>().ReverseMap();
        }
        private void MapInvoiceDetailProfile()
        {
            CreateMap<GetInvoiceDetailDto, InvoiceDetail>().ReverseMap()
            .ForMember(dest => dest.ItemName, opt => opt.MapFrom(src => src.Item == null ? null : src.Item.ItemName));
            CreateMap<AddInvoiceDetailDto, InvoiceDetail>().ReverseMap();
            CreateMap<EditInvoiceDetailDto, InvoiceDetail>().ReverseMap();
        }
    }
}
