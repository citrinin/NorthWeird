using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NorthWeird.Application.Interfaces;

namespace NorthWeird.WebApi.Controllers
{
    /// <inheritdoc />
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryData _categoryData;

        /// <inheritdoc />
        public CategoriesController(ICategoryData categoryData)
        {
            _categoryData = categoryData;
        }

        /// <summary>
        /// Retrieves a category list
        /// </summary>
        /// <returns></returns>
        [HttpGet("")]
        public async Task<IActionResult> Get()
        {
            return Ok(await _categoryData.GetAllAsync());
        }
    }
}