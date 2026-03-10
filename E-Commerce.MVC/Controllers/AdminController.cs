using E_Commerce.MVC.DTOs.CategoryDTOs;
using E_Commerce.MVC.DTOs.ProductDTOs;
using E_Commerce.MVC.Services;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.MVC.Controllers
{
    public class AdminController : Controller
    {
        private readonly IApiProductsService _productsService;
        private readonly IApiCategoriesService _categoriesService;

        public AdminController(IApiProductsService productsService, IApiCategoriesService categoriesService)
        {
            _productsService = productsService;
            _categoriesService = categoriesService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        // ───── Products ─────

        [HttpGet]
        public async Task<IActionResult> Products()
        {
            var products = await _productsService.GetAllProductsAsync();
            return View(products);
        }

        [HttpGet]
        public async Task<IActionResult> CreateProduct()
        {
            ViewBag.Categories = await _categoriesService.GetAllCategoriesAsync();
            return View(new CreateProductDto());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateProduct(CreateProductDto dto)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Categories = await _categoriesService.GetAllCategoriesAsync();
                return View(dto);
            }

            var created = await _productsService.CreateProductAsync(dto);
            if (created == null)
            {
                ViewBag.ErrorMessage = "Failed to create product. Please try again.";
                ViewBag.Categories = await _categoriesService.GetAllCategoriesAsync();
                return View(dto);
            }

            TempData["SuccessMessage"] = "Product created successfully.";
            return RedirectToAction(nameof(Products));
        }

        [HttpGet]
        public async Task<IActionResult> EditProduct(int id)
        {
            var product = await _productsService.GetProductByIdAsync(id);
            if (product == null)
                return NotFound();

            var updateDto = new UpdateProductDto
            {
                ProductId = product.ProductId,
                ProductName = product.ProductName,
                ProductDescription = product.ProductDescription,
                ProductPrice = product.ProductPrice,
                ProductStockQuantity = product.ProductStockQuantity,
                CategoryId = product.CategoryId
            };

            ViewBag.Categories = await _categoriesService.GetAllCategoriesAsync();
            return View(updateDto);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditProduct(UpdateProductDto dto)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Categories = await _categoriesService.GetAllCategoriesAsync();
                return View(dto);
            }

            var updated = await _productsService.UpdateProductAsync(dto);
            if (updated == null)
            {
                ViewBag.ErrorMessage = "Failed to update product. Please try again.";
                ViewBag.Categories = await _categoriesService.GetAllCategoriesAsync();
                return View(dto);
            }

            TempData["SuccessMessage"] = "Product updated successfully.";
            return RedirectToAction(nameof(Products));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var ok = await _productsService.DeleteProductAsync(id);
            TempData[ok ? "SuccessMessage" : "ErrorMessage"] =
                ok ? "Product deleted successfully." : "Failed to delete product.";

            return RedirectToAction(nameof(Products));
        }

        // ───── Categories ─────

        [HttpGet]
        public async Task<IActionResult> Categories()
        {
            var categories = await _categoriesService.GetAllCategoriesAsync();
            return View(categories);
        }

        [HttpGet]
        public IActionResult CreateCategory()
        {
            return View(new CreateCategoryDto());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateCategory(CreateCategoryDto dto)
        {
            if (!ModelState.IsValid)
                return View(dto);

            var ok = await _categoriesService.CreateCategoryAsync(dto);
            if (!ok)
            {
                ViewBag.ErrorMessage = "Failed to create category. Please try again.";
                return View(dto);
            }

            TempData["SuccessMessage"] = "Category created successfully.";
            return RedirectToAction(nameof(Categories));
        }

        [HttpGet]
        public async Task<IActionResult> EditCategory(int id)
        {
            var category = await _categoriesService.GetCategoryByIdAsync(id);
            if (category == null)
                return NotFound();

            return View(category);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditCategory(CategoryDto dto)
        {
            if (!ModelState.IsValid)
                return View(dto);

            var ok = await _categoriesService.UpdateCategoryAsync(dto);
            if (!ok)
            {
                ViewBag.ErrorMessage = "Failed to update category. Please try again.";
                return View(dto);
            }

            TempData["SuccessMessage"] = "Category updated successfully.";
            return RedirectToAction(nameof(Categories));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            var ok = await _categoriesService.DeleteCategoryAsync(id);
            TempData[ok ? "SuccessMessage" : "ErrorMessage"] =
                ok ? "Category deleted successfully." : "Failed to delete category.";

            return RedirectToAction(nameof(Categories));
        }
    }
}

