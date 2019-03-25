using System.Collections.Generic;
using System.Threading.Tasks;
using NorthWeird.Domain.Entities;

namespace NorthWeird.Application.Interfaces
{
    public interface IProductData
    {
        Task<IEnumerable<Product>> GetAll();

        Task<Product> Add(Product product);

        Task<Product> Get(int id);

        Task<Product> Update(Product product);
    }
}
