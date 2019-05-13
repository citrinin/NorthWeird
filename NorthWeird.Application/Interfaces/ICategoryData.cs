using System.Collections.Generic;
using System.Threading.Tasks;
using NorthWeird.Application.Models;

namespace NorthWeird.Application.Interfaces
{
    public interface ICategoryData
    {
       Task<IEnumerable<CategoryDto>> GetAllAsync();

        Task<byte[]> GetImageByCategoryIdAsync(int categoryId);

        Task<CategoryDto> GetAsync(int id);

        Task<CategoryDto> UpdateImageAsync(CategoryDto category);
    }
}
