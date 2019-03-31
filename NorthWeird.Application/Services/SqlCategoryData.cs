using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NorthWeird.Application.Interfaces;
using NorthWeird.Domain.Entities;
using NorthWeird.Persistence;

namespace NorthWeird.Application.Services
{
    public class SqlCategoryData : ICategoryData
    {
        private readonly NorthWeirdDbContext _context;

        public SqlCategoryData(NorthWeirdDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Category>> GetAllAsync()
        {
            return await _context.Categories.ToListAsync(CancellationToken.None);
        }
    }
}
