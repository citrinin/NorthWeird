using System;
using System.Threading;
using NorthWeird.WebApiTests;
using System.Net.Http;
using Newtonsoft.Json;
using NorthWeird.WebApiTests.Models;
using Xunit;

namespace NorthWeird.WebApi.Tests
{
    public class UnitTest1
    {
        [Fact]
        public async void Test1()
        {
            var northWeirdApi = new NorthWeirdAPI();
            var productId = 1;

            var result = await northWeirdApi.Products.GET1WithHttpMessagesAsync(productId);

            var productJson = await result.Response.Content.ReadAsStringAsync();

            var product = JsonConvert.DeserializeObject<ProductDto>(productJson);

            Assert.True(result.Response.IsSuccessStatusCode);
            Assert.Equal(productId, product.ProductId);
            Assert.Contains("chai", product.ProductName.ToLower());
        }
    }
}
