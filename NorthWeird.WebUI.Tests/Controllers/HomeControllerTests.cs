using Microsoft.AspNetCore.Mvc;
using NorthWeird.WebUI.Controllers;
using Xunit;

namespace NorthWeird.WebUI.Tests.Controllers
{
    public class HomeControllerTests
    {
        [Fact]
        public void Index_WithoutParams_ReturnsViewResultWithoutModel()
        {
            var controller = new HomeController();

            var result = controller.Index();

            var viewResult = Assert.IsType<ViewResult>(result);

            Assert.Null(viewResult.ViewData.Model);
        }
    }
}
