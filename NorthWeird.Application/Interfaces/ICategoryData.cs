using System.Collections.Generic;
using System.Threading.Tasks;
using NorthWeird.Domain.Entities;

namespace NorthWeird.Application.Interfaces
{
    public interface ICategoryData
    {
       Task<IEnumerable<Category>> GetAllAsync();

        Task<byte[]> GetImageByCategoryIdAsync(int categoryId);

        Task<Category> GetAsync(int id);

        Task<Category> UpdateImageAsync(Category category);
    }
}
