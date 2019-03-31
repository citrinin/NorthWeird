using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using NorthWeird.Application.Interfaces;
using NorthWeird.Domain.Entities;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace NorthWeird.WebUI.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductData _productData;
        private readonly ICategoryData _categoryData;
        private readonly ISupplierData _supplierData;

        public ProductController(
            IProductData productData,
            ICategoryData categoryData,
            ISupplierData supplierData)
        {
            _productData = productData;
            _categoryData = categoryData;
            _supplierData = supplierData;
        }
        // GET: /<controller>/
        public async Task<IActionResult> Index()
        {
            return View(await _productData.GetAllAsync());
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            await InitializeSelectLists();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Product model)
        {
            if (ModelState.IsValid)
            {

               var newProduct = await _productData.AddAsync(model);

                return RedirectToAction(nameof(Index));
            }
            else
            {
                await InitializeSelectLists();
                return View();
            }
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var model = await _productData.GetAsync(id);
            if (model == null)
            {
                return View("ProductNotFound", id);
            }
            await InitializeSelectLists();
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Product model)
        {
            if (ModelState.IsValid)
            {

                var newProduct = await _productData.UpdateAsync(model);

                return RedirectToAction(nameof(Index));
            }

            await InitializeSelectLists();
            return View();
        }

        private async Task InitializeSelectLists()
        {
            ViewBag.Categories = new SelectList(await _categoryData.GetAllAsync(), nameof(Category.CategoryId), nameof(Category.CategoryName));
            ViewBag.Suppliers = new SelectList(await _supplierData.GetAllAsync(), nameof(Supplier.SupplierId), nameof(Supplier.CompanyName));
        }
    }
}
