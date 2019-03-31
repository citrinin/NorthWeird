using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Configuration;
using NorthWeird.Application.Tests.Infrastructure;
using NorthWeird.Persistence;
using NUnit.Framework;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NorthWeird.Application.Services;
using NorthWeird.Domain.Entities;

namespace NorthWeird.Application.Tests.Services
{
    public class SqlProductDataTest
    {
        private NorthWeirdDbContext _context;
        private IConfiguration _configuration;

        [SetUp]
        public void Setup()
        {
            _context = NorthWeirdDbContextFactory.Create();
        }

        [TearDown]
        public void Cleanup()
        {
            NorthWeirdDbContextFactory.Destroy(_context);
        }

        [TestCase(2, 2)]
        [TestCase(4, 3)]
        [TestCase(0, 3)]
        public async Task GetAllAsync_ShouldReturnSpecifiedAmountOfRecords(int productsPerPage, int resultProductNumber)
        {
            _configuration = new ConfigurationBuilder()
                .AddInMemoryCollection(new[]
                {
                    new KeyValuePair<string, string>("ModelSettings:ProductsPerPage", productsPerPage.ToString())
                })
                .Build();
            var service = new SqlProductData(_context, _configuration);

            var products = await service.GetAllAsync();
            Assert.AreEqual(resultProductNumber, products.Count());
        }

        [Test]
        public async Task AddAsyncTest_ShouldContainNewProduct()
        {
            _configuration = new ConfigurationBuilder().Build();
            var options = new DbContextOptionsBuilder<NorthWeirdDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .EnableSensitiveDataLogging()
                .Options;
            using (var context = new NorthWeirdDbContext(options))
            {
                var service = new SqlProductData(context, _configuration);
                var productToAdd = new Product { ProductName = "Lososij", ProductId = 4 };
                await service.AddAsync(productToAdd);
            }

            using (var context = new NorthWeirdDbContext(options))
            {
                var product = context.Products.First();

                Assert.AreEqual(1, context.Products.Count());
                Assert.AreEqual(4, product.ProductId);
                Assert.AreEqual("Lososij", product.ProductName);
            }
        }

        // Is it correct way to test AddAsync method?

        //[Test]
        //public async Task AddAsyncTest2_ProductAdded()
        //{
        //    var productName = "Lososij";

        //    _configuration = new ConfigurationBuilder().Build();
        //    var service = new SqlProductData(_context, _configuration);
        //    var productToAdd = new Product { ProductName = productName, ProductId = 4 };
        //    await service.AddAsync(productToAdd);

        //    var product = _context.Products.First(p => p.ProductName == productName);

        //    Assert.AreEqual(4, _context.Products.Count());
        //    Assert.AreEqual(4, product.ProductId);
        //    Assert.AreEqual("Lososij", product.ProductName);
        //}

        [Test]
        public async Task UpdateAsyncTest_ShouldChangeProductNameValue()
        {
            _configuration = new ConfigurationBuilder().Build();
            var options = new DbContextOptionsBuilder<NorthWeirdDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .EnableSensitiveDataLogging()
                .Options;
            using (var context = new NorthWeirdDbContext(options))
            {
                var service = new SqlProductData(context, _configuration);
                var productToAdd = new Product { ProductName = "Molochko", ProductId = 1 };
                await service.AddAsync(productToAdd);
            }

            using (var context = new NorthWeirdDbContext(options))
            {
                var product = context.Products.First();

                Assert.AreEqual(1, context.Products.Count());
                Assert.AreEqual(1, product.ProductId);
                Assert.AreEqual("Molochko", product.ProductName);
            }

            using (var context = new NorthWeirdDbContext(options))
            {
                var service = new SqlProductData(context, _configuration);
                var productToUpdate = new Product { ProductName = "Lososij", ProductId = 1 };
                await service.UpdateAsync(productToUpdate);
            }

            using (var context = new NorthWeirdDbContext(options))
            {
                var product = context.Products.First();

                Assert.AreEqual(1, context.Products.Count());
                Assert.AreEqual(1, product.ProductId);
                Assert.AreEqual("Lososij", product.ProductName);
            }
        }

        [TestCase(1, "Frutella")]
        [TestCase(2, "Mars")]
        [TestCase(3, "Onion")]
        [TestCase(4, null)]
        public async Task GetAsyncTest_ShouldReturnProductByIdWithSpecifiedProductName(int productId, string productName)
        {
            _configuration = new ConfigurationBuilder().Build();
            var service = new SqlProductData(_context, _configuration);

            var product = await service.GetAsync(productId);
            Assert.AreEqual(productName, product?.ProductName);
        }
    }
}
