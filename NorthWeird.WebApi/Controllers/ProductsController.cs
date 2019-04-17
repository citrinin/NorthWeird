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

        [HttpGet("{id}", Name = "ProductGet")]
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

        [HttpPost]
        public async Task<IActionResult> Post([FromBody]Product product)
        {
            try
            {
                await _productData.AddAsync(product);
                var newUri = Url.Link("ProductGet",new {id = product.ProductId});
                return Created(newUri, product);
            }
            catch (Exception)
            {

            }

            return BadRequest();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody]Product product)
        {
            try
            {
                //var oldProduct = await _productData.GetAsync(id);
                //if (oldProduct == null)
                //{
                //    return NotFound($"Could not find a product with id {id}");
                //}

                await _productData.UpdateAsync(product);
                return Ok(product);
            }
            catch (Exception)
            {

            }

            return BadRequest("Couldn't update product");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var oldProduct = await _productData.GetAsync(id);
                if (oldProduct == null)
                {
                    return NotFound($"Could not find a product with id {id}");
                }

                await _productData.DeleteAsync(oldProduct);
                return Ok();
            }
            catch (Exception)
            {

            }

            return BadRequest("Couldn't update product");
        }
    }
}