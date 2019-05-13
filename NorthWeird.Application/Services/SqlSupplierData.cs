using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using NorthWeird.Application.Interfaces;
using NorthWeird.Application.Models;
using NorthWeird.Persistence;

namespace NorthWeird.Application.Services
{
    public class SqlSupplierData: ISupplierData
    {
        private readonly NorthWeirdDbContext _context;
        private readonly IMapper _mapper;

        public SqlSupplierData(NorthWeirdDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<IEnumerable<SupplierDto>> GetAllAsync()
        {
            var resultSuppliers = await _context.Suppliers.ToListAsync(CancellationToken.None);
            return _mapper.Map<IEnumerable<SupplierDto>>(resultSuppliers);
        }
    }
}
