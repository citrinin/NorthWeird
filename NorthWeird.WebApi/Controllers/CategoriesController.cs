using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NorthWeird.Application.Interfaces;

namespace NorthWeird.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryData _categoryData;

        public CategoriesController(ICategoryData categoryData)
        {
            _categoryData = categoryData;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _categoryData.GetAllAsync());
        }
    }
}