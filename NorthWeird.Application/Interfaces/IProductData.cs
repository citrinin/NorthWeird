using System.Collections.Generic;
using System.Threading.Tasks;
using NorthWeird.Application.Models;
using NorthWeird.Domain.Entities;

namespace NorthWeird.Application.Interfaces
{
    public interface IProductData
    {
        Task<IEnumerable<ProductDto>> GetAllAsync();

        Task<ProductDto> AddAsync(Product product);

        Task<ProductDto> GetAsync(int id);

        Task<ProductDto> GetWithCategoryAsync(int id);

        Task<ProductDto> UpdateAsync(Product product);

        Task<IEnumerable<ProductDto>> GetPageAsync(int itemsPerPage, int pageNumber);

        Task DeleteAsync(Product productToDelete);
    }
}
