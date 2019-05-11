using AutoMapper;
using NorthWeird.Application.Models;
using NorthWeird.Domain.Entities;

namespace NorthWeird.Application.Mapping
{
    public class ProductMappingProfile : Profile
    {
        public ProductMappingProfile()
        {
            CreateMap<Product, ProductDto>()
                .ForMember(p=>p.CategoryName, opt => opt.MapFrom(product=>product.Category.CategoryName))
                .ForMember(p=>p.SupplierName, opt => opt.MapFrom(product=>product.Supplier.CompanyName))
                .ReverseMap();
        }
    }
}
