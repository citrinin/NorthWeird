using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mime;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NorthWeird.Application.Interfaces;
using NorthWeird.Domain.Entities;
using NorthWeird.Persistence;
using NorthWeird.Infrastructure.Image;

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

        public async Task<byte[]> GetImageByCategoryIdAsync(int categoryId)
        {
            var category = await _context.Categories.SingleOrDefaultAsync(c => c.CategoryId == categoryId);

            return ImageHelper.IsValidImage(category.Picture)
                ? category.Picture
                : ImageHelper.FixBrokenImage(category.Picture);
        }

        public async Task<Category> GetAsync(int id)
        {
            return await _context.Categories.SingleOrDefaultAsync(c => c.CategoryId == id, CancellationToken.None);

        }

        public async Task<Category> UpdateImageAsync(Category category)
        {
            var categoryToUpdate =
                await _context.Categories.SingleOrDefaultAsync(c => c.CategoryId == category.CategoryId);

            categoryToUpdate.Picture = category.Picture;

            await _context.SaveChangesAsync(CancellationToken.None);
            return categoryToUpdate;
        }
    }
}
