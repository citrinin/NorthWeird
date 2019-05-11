using AutoMapper;
using NorthWeird.Application.Models;
using NorthWeird.Domain.Entities;

namespace NorthWeird.Application.Mapping
{
    public class CategoryMappingProfile : Profile
    {
        public CategoryMappingProfile()
        {
            CreateMap<Category, CategoryDto>().ReverseMap();
        }
    }
}
