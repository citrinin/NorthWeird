using System.Collections.Generic;
using System.Threading.Tasks;
using NorthWeird.Domain.Entities;

namespace NorthWeird.Application.Interfaces
{
    public interface ISupplierData
    {
        Task<IEnumerable<Supplier>> GetAll();
    }
}
