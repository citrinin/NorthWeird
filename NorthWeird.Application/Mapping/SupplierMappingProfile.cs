using AutoMapper;
using NorthWeird.Application.Models;
using NorthWeird.Domain.Entities;

namespace NorthWeird.Application.Mapping
{
    public class SupplierMappingProfile : Profile
    {
        public SupplierMappingProfile()
        {
            CreateMap<Supplier, SupplierDto>().ReverseMap();
        }
    }
}
