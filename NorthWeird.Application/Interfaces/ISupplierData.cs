using System.Collections.Generic;
using System.Threading.Tasks;
using NorthWeird.Application.Models;

namespace NorthWeird.Application.Interfaces
{
    public interface ISupplierData
    {
        Task<IEnumerable<SupplierDto>> GetAllAsync();
    }
}