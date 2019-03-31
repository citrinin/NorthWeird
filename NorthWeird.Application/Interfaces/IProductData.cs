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

        Task<Product> UpdateAsync(Product product);
    }
}
