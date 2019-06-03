using Microsoft.AspNetCore.Mvc;
using Moq;
using NorthWeird.Application.Interfaces;
using NorthWeird.WebUI.Controllers;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NorthWeird.Application.Models;
using Xunit;

namespace NorthWeird.WebUI.Tests.Controllers
{
    public class ProductControllerTests
    {
        private readonly IProductData _productService;
        private readonly ICategoryData _categoryService;
        private readonly ISupplierData _supplierService;
        private readonly List<ProductDto> _productList;

        public ProductControllerTests()
        {
            _productList = new List<ProductDto>
            {
                new ProductDto {ProductId = 1, ProductName = "Frutella", CategoryId = 1, SupplierId = 1},
                new ProductDto {ProductId = 2, ProductName = "Mars", CategoryId = 2, SupplierId = 2},
                new ProductDto {ProductId = 3, ProductName = "Onion", CategoryId = 3, SupplierId = 3}
            };

            var mockProduct = new Mock<IProductData>();
            mockProduct
                .Setup(service => service.GetAllAsync())
                .ReturnsAsync(_productList);

            mockProduct
                .Setup(service => service.AddAsync(It.IsAny<ProductDto>()))
                .Callback((ProductDto product) => _productList.Add(product))
                .Returns((ProductDto product) => Task.FromResult(product));

            mockProduct
                .Setup(service => service.GetAsync(It.IsAny<int>()))
                .Returns((int id) => Task.FromResult(_productList.Find(p => p.ProductId == id)));

            mockProduct
                .Setup(service => service.UpdateAsync(It.IsAny<ProductDto>()))
                .Returns((ProductDto product) =>
                {
                    var productToUpdate = _productList.Find(p => p.ProductId == product.ProductId);
                    productToUpdate.ProductName = product.ProductName;
                    return Task.FromResult(productToUpdate);
                });

            _productService = mockProduct.Object;

            var mockSupplier = new Mock<ISupplierData>();
            mockSupplier.Setup(service => service.GetAllAsync())
                .ReturnsAsync(new List<SupplierDto>());

            _supplierService = mockSupplier.Object;

            var mockCategory = new Mock<ICategoryData>();
            mockCategory.Setup(service => service.GetAllAsync())
                .ReturnsAsync(new List<CategoryDto>());

            _categoryService = mockCategory.Object;
        }

        [Fact]
        public async Task Index_WithoutParams_ReturnsViewResultWithListOfProducts()
        {
            var controller = new ProductController(_productService, _categoryService, _supplierService);

            var result = await controller.Index();
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<IEnumerable<ProductDto>>(viewResult.ViewData.Model);
            Assert.Equal(3, model.Count());
        }

        [Fact]
        public async Task Create_WithoutParams_ReturnsViewResultWithoutModel()
        {
            var controller = new ProductController(_productService, _categoryService, _supplierService);

            var result = await controller.Create();
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = viewResult.ViewData.Model;
            Assert.Null(model);
        }

        [Fact]
        public async Task Create_WithModelParam_AddsModelAndRedirectsToIndexAction()
        {
            var controller = new ProductController(_productService, _categoryService, _supplierService);
            var productToAdd = new ProductDto { ProductName = "Lososij", ProductId = 0 };

            var result = await controller.Create(productToAdd);

            Assert.Equal(4, _productList.Count);
            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal(nameof(controller.Index), redirectToActionResult.ActionName);
        }

        [Fact]
        public async Task Edit_WithIdParam_ReturnsViewResultWithModel()
        {
            var controller = new ProductController(_productService, _categoryService, _supplierService);
            var modelId = 2;
            var result = await controller.Edit(modelId);
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<ProductDto>(viewResult.ViewData.Model);

            Assert.Equal(modelId, model.ProductId);
        }

        [Fact]
        public async Task Edit_WithIncorrectIdParam_ReturnsViewResultNotFoundProduct()
        {
            var controller = new ProductController(_productService, _categoryService, _supplierService);
            var modelId = 400;
            var result = await controller.Edit(modelId);
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<int>(viewResult.ViewData.Model);

            Assert.Equal("ProductNotFound", viewResult.ViewName);
            Assert.Equal(modelId, model);
        }

        [Fact]
        public async Task Edit_WithModelParam_AddsModelAndRedirectsToIndexAction()
        {
            var controller = new ProductController(_productService, _categoryService, _supplierService);
            var productToUpdate = new ProductDto { ProductName = "Lososij", ProductId = 2 };

            var result = await controller.Edit(productToUpdate);

            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal(nameof(controller.Index), redirectToActionResult.ActionName);

            var updatedProduct = _productList.Find(p => p.ProductId == productToUpdate.ProductId);

            Assert.Equal(productToUpdate.ProductName, updatedProduct.ProductName);
        }
    }
}
