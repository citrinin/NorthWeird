using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NorthWeird.Application.Interfaces;
using NorthWeird.Domain.Entities;
using NorthWeird.Persistence;

namespace NorthWeird.Application.Services
{
    public class SqlSupplierData: ISupplierData
    {
        private readonly NorthWeirdDbContext _context;

        public SqlSupplierData(NorthWeirdDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Supplier>> GetAllAsync()
        {
            return await _context.Suppliers.ToListAsync(CancellationToken.None);

        }
    }
}
