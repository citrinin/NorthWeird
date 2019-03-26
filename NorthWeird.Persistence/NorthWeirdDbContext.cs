using Microsoft.EntityFrameworkCore;
using NorthWeird.Domain.Entities;

namespace NorthWeird.Persistence
{
    public class NorthWeirdDbContext: DbContext
    {
        public NorthWeirdDbContext(DbContextOptions<NorthWeirdDbContext> options)
        : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }

        public DbSet<Category> Categories { get; set; }

        public DbSet<Supplier> Suppliers { get; set; }
    }
}
