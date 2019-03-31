using Microsoft.AspNetCore.Mvc;
using NorthWeird.Application.Interfaces;
using System.Threading.Tasks;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace NorthWeird.WebUI.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICategoryData _categoryData;

        public CategoryController(ICategoryData categoryData)
        {
            _categoryData = categoryData;
        }
        // GET: /<controller>/
        public async Task<IActionResult> Index()
        {
            return View(await _categoryData.GetAllAsync());
        }
    }
}
