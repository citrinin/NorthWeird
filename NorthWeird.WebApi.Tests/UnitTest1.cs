using System;
using NorthWeird.AutoGen;
using Xunit;

namespace NorthWeird.WebApi.Tests
{
    public class UnitTest1
    {
        [Fact]
        public async void Test1()
        {
            var xz = new NorthWeirdAPI();

            var result = await xz.ProductGetWithHttpMessagesAsync(1);

            var r = await result.Response.Content.ReadAsStringAsync();
            Console.WriteLine(r);
            Assert.True(result.Response.IsSuccessStatusCode);
        }
    }
}
