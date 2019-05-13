using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using NorthWeird.Application.Interfaces;
using NorthWeird.Application.Models;
using NorthWeird.Persistence;
using NorthWeird.Infrastructure.Image;

namespace NorthWeird.Application.Services
{
    public class SqlCategoryData : ICategoryData
    {
        private readonly NorthWeirdDbContext _context;
        private readonly IMapper _mapper;

        public SqlCategoryData(NorthWeirdDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<IEnumerable<CategoryDto>> GetAllAsync()
        {
            var resultCategory = await _context.Categories.ToListAsync(CancellationToken.None);
            return _mapper.Map<IEnumerable<CategoryDto>>(resultCategory);
        }

        public async Task<byte[]> GetImageByCategoryIdAsync(int categoryId)
        {
            var category = await _context.Categories.SingleOrDefaultAsync(c => c.CategoryId == categoryId);

            return ImageHelper.IsValidImage(category.Picture)
                ? category.Picture
                : ImageHelper.FixBrokenImage(category.Picture);
        }

        public async Task<CategoryDto> GetAsync(int id)
        {
            var resultCategory = await _context.Categories.SingleOrDefaultAsync(c => c.CategoryId == id, CancellationToken.None);
            return _mapper.Map<CategoryDto>(resultCategory);
        }

        public async Task<CategoryDto> UpdateImageAsync(CategoryDto category)
        {
            var categoryToUpdate =
                await _context.Categories.SingleOrDefaultAsync(c => c.CategoryId == category.CategoryId);

            categoryToUpdate.Picture = category.Picture;

            await _context.SaveChangesAsync(CancellationToken.None);
            return _mapper.Map<CategoryDto>(categoryToUpdate);
        }
    }
}
