using System.Collections.Generic;
using System.Threading.Tasks;
using NorthWeird.Domain.Entities;

namespace NorthWeird.Application.Interfaces
{
    public interface IProductData
    {
        Task<IEnumerable<Product>> GetAllAsync();

        Task<Product> AddAsync(Product product);

        Task<Product> GetAsync(int id);

        Task<Product> GetWithCategoryAsync(int id);

        Task<Product> UpdateAsync(Product product);

        Task<IEnumerable<Product>> GetPageAsync(int itemsPerPage, int pageNumber);

        Task DeleteAsync(Product productToDelete);
    }
}
