using Microsoft.EntityFrameworkCore;
using NorthWeird.Persistence;
using System;
using System.Collections.Generic;
using NorthWeird.Domain.Entities;

namespace NorthWeird.Application.Tests.Infrastructure
{
    public class NorthWeirdContextFactory
    {
        public static NorthWeirdDbContext Create()
        {
            var options = new DbContextOptionsBuilder<NorthWeirdDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
            var context = new NorthWeirdDbContext(options);

            context.Database.EnsureCreated();

            context.Suppliers.AddRange(new List<Supplier>
            {
                new Supplier { SupplierId = 1, CompanyName = "Dumpling boutique" },
                new Supplier { SupplierId = 2, CompanyName = "Washington Bread Factory №2" },
                new Supplier { SupplierId = 3, CompanyName = "Alaska fishing inс" }
            });

            context.Categories.AddRange(new List<Category>
            {
                new Category { CategoryId = 1, CategoryName = "Fish" },
                new Category { CategoryId = 2, CategoryName = "Meat" },
                new Category { CategoryId = 3, CategoryName = "Vegetables" }
            });

            context.Products.AddRange(new List<Product>
            {
                new Product { ProductId = 1, ProductName = "Frutella", CategoryId = 1, SupplierId = 1 },
                new Product { ProductId = 2, ProductName = "Mars", CategoryId = 2, SupplierId = 2 },
                new Product { ProductId = 3, ProductName = "Onion", CategoryId = 3, SupplierId = 3 }
            });

            context.SaveChanges();
            return context;
        }

        public static void Destroy(NorthWeirdDbContext context)
        {
            context.Database.EnsureDeleted();
            context.Dispose();
        }
    }
}
