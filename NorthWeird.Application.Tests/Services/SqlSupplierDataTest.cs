using System.Linq;
using System.Threading.Tasks;
using NorthWeird.Application.Services;
using NorthWeird.Application.Tests.Infrastructure;
using NorthWeird.Persistence;
using NUnit.Framework;

namespace NorthWeird.Application.Tests.Services
{
    public class SqlSupplierDataTest
    {
        private NorthWeirdDbContext _context;

        [SetUp]
        public void Setup()
        {
            _context = NorthWeirdDbContextFactory.Create();
        }

        [TearDown]
        public void Cleanup()
        {
            NorthWeirdDbContextFactory.Destroy(_context);
        }

        [Test]
        public async Task GetAllAsync_ShouldReturnAllRecords()
        {
            var service = new SqlSupplierData(_context);
            var suppliers = await service.GetAllAsync();
            Assert.AreEqual(3, suppliers.Count());
        }
    }
}
