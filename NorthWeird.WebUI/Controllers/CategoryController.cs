using System.IO;
using Microsoft.AspNetCore.Mvc;
using NorthWeird.Application.Interfaces;
using System.Threading.Tasks;
using NorthWeird.Domain.Entities;
using NorthWeird.WebUI.Filters;
using NorthWeird.WebUI.ViewModel;

namespace NorthWeird.WebUI.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICategoryData _categoryData;

        public CategoryController(ICategoryData categoryData)
        {
            _categoryData = categoryData;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _categoryData.GetAllAsync());
        }

        [TypeFilter(typeof(LoggingActionFilterAttribute), Arguments = new object[] { true })]
        public async Task<IActionResult> GetCategoryImage(int id)
        {
            var image = await _categoryData.GetImageByCategoryIdAsync(id);

            return File(image, "image/jpeg");
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var model = await _categoryData.GetAsync(id);
            return model == null ? View("CategoryNotFound", id) : View(new EditCategoryViewModel{Category = model});
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditCategoryViewModel editCategory)
        {
            var files = HttpContext.Request.Form.Files;
            if (ModelState.IsValid)
            {
                var categoryToUpdate = new Category {CategoryId = editCategory.Category.CategoryId};
                using (var ms = new MemoryStream())
                {
                    await editCategory.FormFile.CopyToAsync(ms);
                    categoryToUpdate.Picture = ms.GetBuffer();
                }


                var newCategory = await _categoryData.UpdateImageAsync(categoryToUpdate);

                return RedirectToAction(nameof(Index));
            }

            return View();
        }
    }
}
