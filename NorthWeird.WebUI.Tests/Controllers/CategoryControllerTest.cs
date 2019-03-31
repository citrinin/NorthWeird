using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NorthWeird.Application.Interfaces;
using NorthWeird.Domain.Entities;
using Xunit;
using NorthWeird.WebUI.Controllers;

namespace NorthWeird.WebUI.Tests.Controllers
{
    public class CategoryControllerTest
    {
        private readonly ICategoryData _categoryService;

        public CategoryControllerTest()
        {
            var mockCategory = new Mock<ICategoryData>();
            mockCategory.Setup(service => service.GetAllAsync())
                .ReturnsAsync(new List<Category>
                {
                    new Category { CategoryId = 1, CategoryName = "Fish" },
                    new Category { CategoryId = 2, CategoryName = "Meat" },
                    new Category { CategoryId = 3, CategoryName = "Vegetables" }
                });

            _categoryService = mockCategory.Object;
        }

        [Fact]
        public async Task Index_WithoutParams_ReturnsViewResultWithListOfCategories()
        {
            var controller = new CategoryController(_categoryService);

            var result = await controller.Index();

            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<IEnumerable<Category>>(viewResult.ViewData.Model);
            Assert.Equal(3, model.Count());
        }
    }
}
