using System;
using System.Threading;
using NorthWeird.WebApiTests;
using Xunit;

namespace NorthWeird.WebApi.Tests
{
    public class UnitTest1
    {
        [Fact]
        public async void Test1()
        {
            var northWeirdApi = new NorthWeirdAPI();

            var result = await northWeirdApi.Get1WithHttpMessagesAsync();

            var r = await result.Response.Content.ReadAsStringAsync();
            Console.WriteLine(r);
            Assert.True(result.Response.IsSuccessStatusCode);
        }
    }
}
