using E_Commerce.MVC.DTOs.CategoryDTOs;
using E_Commerce.MVC.DTOs.ProductDTOs;
using E_Commerce.MVC.DTOs.UserDTOs;
using E_Commerce.MVC.Services;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.MVC.Controllers
{
    public class AdminController : Controller
    {
        private readonly IApiProductsService _productsService;
        private readonly IApiCategoriesService _categoriesService;
        private readonly IApiUsersService _usersService;

        public AdminController(
            IApiProductsService productsService,
            IApiCategoriesService categoriesService,
            IApiUsersService usersService)
        {
            _productsService = productsService;
            _categoriesService = categoriesService;
            _usersService = usersService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            if (HttpContext.Session.GetString("UserRole") != "Admin")
            {
                return Unauthorized("this page that you want to access is authorized only to admins");
            }
            return View();
        }

        // ───── Products ─────

        [HttpGet]
        public async Task<IActionResult> Products()
        {
            if (HttpContext.Session.GetString("UserRole") != "Admin")
            {
                return Unauthorized("this page that you want to access is authorized only to admins");
            }
            var products = await _productsService.GetAllProductsAsync();
            return View(products);
        }

        [HttpGet]
        public async Task<IActionResult> CreateProduct()
        {
            if (HttpContext.Session.GetString("UserRole") != "Admin")
            {
                return Unauthorized("this page that you want to access is authorized only to admins");
            }
            ViewBag.Categories = await _categoriesService.GetAllCategoriesAsync();
            return View(new CreateProductDto());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateProduct(CreateProductDto dto, IFormFile? productImage)
        {
            if (HttpContext.Session.GetString("UserRole") != "Admin")
            {
                return Unauthorized("this page that you want to access is authorized only to admins");
            }
            if (!ModelState.IsValid)
            {
                ViewBag.Categories = await _categoriesService.GetAllCategoriesAsync();
                return View(dto);
            }

            var created = await _productsService.CreateProductAsync(dto, productImage);
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
            if (HttpContext.Session.GetString("UserRole") != "Admin")
            {
                return Unauthorized("this page that you want to access is authorized only to admins");
            }

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
                CategoryId = product.CategoryId,
                UpdateImage = false
            };

            ViewBag.Categories = await _categoriesService.GetAllCategoriesAsync();
            ViewBag.HasExistingImage = product.IsProductHasImage;
            return View(updateDto);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditProduct(UpdateProductDto dto, IFormFile? productImage)
        {
            if (HttpContext.Session.GetString("UserRole") != "Admin")
            {
                return Unauthorized("this page that you want to access is authorized only to admins");
            }

            if (!ModelState.IsValid)
            {
                ViewBag.Categories = await _categoriesService.GetAllCategoriesAsync();
                return View(dto);
            }

            // If a new image is uploaded, set the flag
            dto.UpdateImage = productImage != null;

            var updated = await _productsService.UpdateProductAsync(dto, productImage);
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
            if (HttpContext.Session.GetString("UserRole") != "Admin")
            {
                return Unauthorized("this page that you want to access is authorized only to admins");
            }

            var ok = await _productsService.DeleteProductAsync(id);
            TempData[ok ? "SuccessMessage" : "ErrorMessage"] =
                ok ? "Product deleted successfully." : "Failed to delete product.";

            return RedirectToAction(nameof(Products));
        }

        // ───── Categories ─────

        [HttpGet]
        public async Task<IActionResult> Categories()
        {
            if (HttpContext.Session.GetString("UserRole") != "Admin")
            {
                return Unauthorized("this page that you want to access is authorized only to admins");
            }

            var categories = await _categoriesService.GetAllCategoriesAsync();
            return View(categories);
        }

        [HttpGet]
        public IActionResult CreateCategory()
        {
            if (HttpContext.Session.GetString("UserRole") != "Admin")
            {
                return Unauthorized("this page that you want to access is authorized only to admins");
            }

            return View(new CreateCategoryDto());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateCategory(CreateCategoryDto dto)
        {
            if (HttpContext.Session.GetString("UserRole") != "Admin")
            {
                return Unauthorized("this page that you want to access is authorized only to admins");
            }

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
            if (HttpContext.Session.GetString("UserRole") != "Admin")
            {
                return Unauthorized("this page that you want to access is authorized only to admins");
            }

            var category = await _categoriesService.GetCategoryByIdAsync(id);
            if (category == null)
                return NotFound();

            return View(category);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditCategory(CategoryDto dto)
        {
            if (HttpContext.Session.GetString("UserRole") != "Admin")
            {
                return Unauthorized("this page that you want to access is authorized only to admins");
            }

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
            if (HttpContext.Session.GetString("UserRole") != "Admin")
            {
                return Unauthorized("this page that you want to access is authorized only to admins");
            }

            var ok = await _categoriesService.DeleteCategoryAsync(id);
            TempData[ok ? "SuccessMessage" : "ErrorMessage"] =
                ok ? "Category deleted successfully." : "Failed to delete category.";

            return RedirectToAction(nameof(Categories));
        }

        // ───── Users ─────

        [HttpGet]
        public async Task<IActionResult> Users()
        {
            if (HttpContext.Session.GetString("UserRole") != "Admin")
            {
                return Unauthorized("this page that you want to access is authorized only to admins");
            }

            var users = await _usersService.GetAllUsersAsync();
            return View(users);
        }

        [HttpGet]
        public async Task<IActionResult> EditUser(int id)
        {
            if (HttpContext.Session.GetString("UserRole") != "Admin")
            {
                return Unauthorized("this page that you want to access is authorized only to admins");
            }

            var user = await _usersService.GetUserByIdAsync(id);
            if (user == null)
                return NotFound();

            // Clear the password field for security
            user.UserPassword = string.Empty;
            return View(user);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditUser(UserDto dto)
        {
            if (HttpContext.Session.GetString("UserRole") != "Admin")
            {
                return Unauthorized("this page that you want to access is authorized only to admins");
            }

            if (!ModelState.IsValid)
                return View(dto);

            // If password is empty, get the existing user to preserve the password
            if (string.IsNullOrEmpty(dto.UserPassword))
            {
                var existingUser = await _usersService.GetUserByIdAsync(dto.UserId);
                if (existingUser != null)
                {
                    dto.UserPassword = existingUser.UserPassword;
                }
            }

            var result = await _usersService.UpdateUserAsync(dto);
            if (!result.IsAuthenticated)
            {
                ViewBag.ErrorMessage = result.ErrorMessage ?? "Failed to update user. Please try again.";
                dto.UserPassword = string.Empty; // Clear password for security
                return View(dto);
            }

            TempData["SuccessMessage"] = "User updated successfully.";
            return RedirectToAction(nameof(Users));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteUser(int id)
        {
            if (HttpContext.Session.GetString("UserRole") != "Admin")
            {
                return Unauthorized("this page that you want to access is authorized only to admins");
            }

            var ok = await _usersService.DeleteUserAsync(id);
            TempData[ok ? "SuccessMessage" : "ErrorMessage"] =
                ok ? "User deleted successfully." : "Failed to delete user.";

            return RedirectToAction(nameof(Users));
        }
    }
}
