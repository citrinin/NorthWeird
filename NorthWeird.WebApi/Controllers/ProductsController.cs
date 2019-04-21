using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NorthWeird.Application.Interfaces;
using NorthWeird.Domain.Entities;

namespace NorthWeird.WebApi.Controllers
{
    /// <inheritdoc />
    [Route("api/products")]
    public class ProductsController : Controller
    {
        private readonly IProductData _productData;

        /// <inheritdoc />
        public ProductsController(IProductData productData)
        {
            _productData = productData;
        }

        /// <summary>
        /// Retrieves a product list that contains full list of products
        /// </summary>
        /// <response code="200">Product list successfully obtained</response>
        /// <response code="500">Oops! Some problems with server</response>
        [HttpGet("")]
        public async Task<IActionResult> Get()
        {
            return Ok(await _productData.GetAllAsync());
        }

        /// <summary>
        /// Retrieves a specific product by unique id
        /// </summary>
        /// <param name="id">Id of the product</param>
        /// <param name="includeCategory">flag for including category into product object</param>
        /// <response code="200">Product with such id was found</response>
        /// <response code="404">Product with such id was not found</response>
        /// <response code="500">Oops! Some problems with server</response>
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

        /// <summary>
        /// Creates product from request body
        /// </summary>
        /// <param name="product">Product that needs to be created</param>
        /// <response code="200">Product was created</response>
        /// <response code="500">Oops! Some problems with server</response>
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

        /// <summary>
        /// Updates product from request body
        /// </summary>
        /// <param name="id">id of the product that needs to be updated</param>
        /// <param name="product">Product that needs to be updated</param>
        /// <response code="200">Product was updated</response>
        /// <response code="500">Oops! Some problems with server</response>
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

        /// <summary>
        /// Deletes product from request body
        /// </summary>
        /// <param name="id">id of the product that needs to be deleted</param>
        /// <response code="200">Product was deleted</response>
        /// <response code="404">Product with such id was not found</response>
        /// <response code="500">Oops! Some problems with server</response>
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