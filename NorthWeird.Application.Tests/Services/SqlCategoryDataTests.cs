using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using NorthWeird.Application.Mapping;
using NorthWeird.Application.Services;
using NorthWeird.Application.Tests.Infrastructure;
using NorthWeird.Persistence;
using NUnit.Framework;

namespace NorthWeird.Application.Tests.Services
{
    public class SqlCategoryDataTests
    {
        private NorthWeirdDbContext _context;
        private IMapper _mapper;

        [SetUp]
        public void Setup()
        {
            _context = NorthWeirdDbContextFactory.Create();
            _mapper = new MapperConfiguration(c=>c.AddMaps(typeof(ProductMappingProfile))).CreateMapper();
        }

        [TearDown]
        public void Cleanup()
        {
            NorthWeirdDbContextFactory.Destroy(_context);
        }

        [Test]
        public async Task GetAllAsync_ShouldReturnAllRecords()
        {
            var service = new SqlCategoryData(_context, _mapper);
            var categories = await service.GetAllAsync();
            Assert.AreEqual(3, categories.Count());
        }
    }
}