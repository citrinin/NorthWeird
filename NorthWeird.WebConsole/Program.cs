using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using NorthWeird.Domain.Entities;

namespace NorthWeird.WebConsole
{
    class Program
    {
        static readonly HttpClient client = new HttpClient();

        static void Main(string[] args)
        {
            RunAsync().GetAwaiter().GetResult();
        }

        static async Task RunAsync()
        {
            client.BaseAddress = new Uri("http://localhost:52370/");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var products = await GetAllProductsAsync();
            Console.WriteLine("-------------Product list-------------");
            foreach (var product in products)
            {
                ShowProduct(product);
            }

            var categories = await GetAllCategoriesAsync();
            Console.WriteLine("-------------Category list-------------");
            foreach (var category in categories)
            {
                ShowCategory(category);
            }

        }

        static void ShowProduct(Product product)
        {
            Console.WriteLine($"Name: {product.ProductName}\t Category: {product.Category.CategoryName}\t Price: {product.UnitPrice}");
        }

        static void ShowCategory(Category category)
        {
            Console.WriteLine($"Name: {category.CategoryName}\t Description: {category.Description}");
        }

        static async Task<List<Product>> GetAllProductsAsync()
        {
            List<Product> products = null;
            var response = await client.GetAsync("api/products");
            if (response.IsSuccessStatusCode)
            {
                products = await response.Content.ReadAsAsync<List<Product>>();
            }

            return products;
        }

        static async Task<List<Category>> GetAllCategoriesAsync()
        {
            List<Category> categories = null;
            var response = await client.GetAsync("api/categories");
            if (response.IsSuccessStatusCode)
            {
                categories = await response.Content.ReadAsAsync<List<Category>>();
            }

            return categories;
        }
    }
}
