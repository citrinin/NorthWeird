using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using NorthWeird.Application.Interfaces;
using NorthWeird.Domain.Entities;
using NorthWeird.Persistence;

namespace NorthWeird.Application.Services
{
    public class SqlProductData: IProductData
    {
        private readonly IConfiguration _configuration;
        private readonly NorthWeirdDbContext _context;

        public SqlProductData(NorthWeirdDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            var itemsToRead = _configuration.GetValue("ModelSettings:ProductsPerPage", 0);
            var products = _context.Products.Include(p => p.Category)
                .Include(p => p.Supplier);

            return await (itemsToRead == 0 ? products : products.Take(itemsToRead)).ToListAsync(CancellationToken.None);
        }

        public async Task<IEnumerable<Product>> GetPageAsync(int itemsPerPage, int pageNumber)
        {
            if (itemsPerPage <=0 || pageNumber<= 0)
            {
                throw new ArgumentException("Incorrect page number");
            }

            var products = _context.Products.Include(p => p.Category)
                .Include(p => p.Supplier);

            return await products.Skip(itemsPerPage * pageNumber).Take(itemsPerPage).ToListAsync(CancellationToken.None);
        }

        public async Task<Product> AddAsync(Product product)
        {
             _context.Products.Add(product);
            await _context.SaveChangesAsync(CancellationToken.None);
            return product;
        }

        public async Task<Product> GetAsync(int id)
        {
            return await _context.Products.SingleOrDefaultAsync(p => p.ProductId == id, CancellationToken.None);
        }

        public async Task<Product> UpdateAsync(Product product)
        {
            _context.Attach(product).State = EntityState.Modified;

            await _context.SaveChangesAsync(CancellationToken.None);
            return product;
        }

        public Task<Product> GetWithCategoryAsync(int id)
        {
            return _context.Products
                .Include(p => p.Category)
                .SingleOrDefaultAsync(p => p.ProductId == id, CancellationToken.None);
        }

        public async Task DeleteAsync(Product productToDelete)
        {
            _context.Products.Remove(productToDelete);
            await _context.SaveChangesAsync(CancellationToken.None);
        }
    }
}
