using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using NorthWeird.Application.Interfaces;
using NorthWeird.Application.Models;
using NorthWeird.Domain.Entities;
using NorthWeird.Persistence;

namespace NorthWeird.Application.Services
{
    public class SqlProductData: IProductData
    {
        private readonly IConfiguration _configuration;
        private readonly NorthWeirdDbContext _context;
        private readonly IMapper _mapper;

        public SqlProductData(
            NorthWeirdDbContext context,
            IConfiguration configuration,
            IMapper mapper
            )
        {
            _context = context;
            _configuration = configuration;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ProductDto>> GetAllAsync()
        {
            var itemsToRead = _configuration.GetValue("ModelSettings:ProductsPerPage", 0);
            var products = _context.Products
                .Include(p => p.Category)
                .Include(p => p.Supplier);

            var resultProducts = await (itemsToRead == 0 ? products : products.Take(itemsToRead))
                .ToListAsync(CancellationToken.None);

            return _mapper.Map<IEnumerable<ProductDto>>(resultProducts);
        }

        public async Task<IEnumerable<ProductDto>> GetPageAsync(int itemsPerPage, int pageNumber)
        {
            if (itemsPerPage <=0 || pageNumber<= 0)
            {
                throw new ArgumentException("Incorrect page number");
            }

            var products = _context.Products
                .Include(p => p.Category)
                .Include(p => p.Supplier);

            var resultProducts = await products
                .Skip(itemsPerPage * pageNumber)
                .Take(itemsPerPage)
                .ToListAsync(CancellationToken.None);

            return _mapper.Map<IEnumerable<ProductDto>>(resultProducts);
        }

        public async Task<ProductDto> AddAsync(ProductDto productToAdd)
        {
            var product = _mapper.Map<Product>(productToAdd);
             _context.Products.Add(product);
            await _context.SaveChangesAsync(CancellationToken.None);
            return _mapper.Map<ProductDto>(product);
        }

        public async Task<ProductDto> GetAsync(int id)
        {
            var resultProduct = await _context.Products.SingleOrDefaultAsync(p => p.ProductId == id, CancellationToken.None);
            return _mapper.Map<ProductDto>(resultProduct);
        }

        public async Task<ProductDto> UpdateAsync(ProductDto productToUpdate)
        {
            var oldProduct = await _context.Products.SingleOrDefaultAsync(p => p.ProductId == productToUpdate.ProductId, CancellationToken.None);
            if (oldProduct == null)
            {
                throw new NullReferenceException("old product is null");
            }

            var product = _mapper.Map(productToUpdate, oldProduct);

            await _context.SaveChangesAsync(CancellationToken.None);
            return _mapper.Map<ProductDto>(product);
        }

        public async Task DeleteAsync(ProductDto productToDelete)
        {
            _context.Products.Remove(_mapper.Map<Product>(productToDelete));
            await _context.SaveChangesAsync(CancellationToken.None);
        }
    }
}
