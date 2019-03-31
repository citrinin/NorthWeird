using System.Collections.Generic;
using System.Threading.Tasks;
using NorthWeird.Domain.Entities;

namespace NorthWeird.Application.Interfaces
{
    public interface ICategoryData
    {
       Task<IEnumerable<Category>> GetAllAsync();
    }
}
