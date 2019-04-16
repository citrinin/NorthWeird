using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Query.Internal;
using NorthWeird.Application.Interfaces;
using NorthWeird.Domain.Entities;

namespace NorthWeird.WebApi.Controllers
{
    [Route("api/products")]
    public class ProductsController : Controller
    {
        private readonly IProductData _productData;

        public ProductsController(IProductData productData)
        {
            _productData = productData;
        }

        [HttpGet("")]
        public async Task<IActionResult> Get()
        {
            return Ok(await _productData.GetAllAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id, bool includeCategory = false)
        {
            try
            {
                var product = includeCategory ? await _productData.GetWithCategoryAsync(id) : await _productData.GetAsync(id);

                if (product == null)
                {
                    return NotFound($"Product {id} was not found");
                }

                return Ok(product);
            }
            catch
            {
                
            }

            return BadRequest();
        }
    }
}