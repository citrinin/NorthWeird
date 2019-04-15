using Microsoft.AspNetCore.Mvc;

namespace NorthWeird.WebApi.Controllers
{
    [Route("api/products")]
    public class ProductsController : Controller
    {
        [HttpGet("")]
        public IActionResult Get()
        {
            return Ok(new { Name = "Kate", Surname = "Kuzkina" });
        }
    }
}