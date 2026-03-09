using E_Commerce.API.DTOs.CategoryDTOs;
using E_Commerce.API.Models;
using E_Commerce.API.UnitOfWork;

namespace E_Commerce.API.Services
{
    /// <summary>
    /// This class represents the service layer for managing categories in an e-commerce application. It implements the ICategoryService interface and provides methods for adding, deleting, retrieving, and updating categories. The service interacts with the database through a Unit of Work (UOW) pattern to perform operations on category data. Each method includes validation checks to ensure that the input data is valid and that the necessary related entities exist in the database. The service is responsible for handling all business logic related to categories, ensuring that the application functions correctly when users create or modify product categories.
    /// </summary>
    public class CategoryService : ICategoryService
    {
        private readonly UOW _uow;
        public CategoryService(UOW uow)
        {
            _uow = uow;
        }

        public void AddCategory(CreateCategoryDto createCategoryDto)
        {
            if (createCategoryDto == null)
                throw new ArgumentNullException(nameof(createCategoryDto), "the data of the category can not be empty");

            if (_uow.CategoryRepository.GetAllModels().Any(c => c.CategoryName == createCategoryDto.CategoryName))
                throw new ArgumentException("this category name is aleady exists, the category name must be unique");

            _uow.CategoryRepository.AddModel(new Category
            {
                CategoryName = createCategoryDto.CategoryName,
                CategoryDescription = createCategoryDto.CategoryDescription
            });
        }

        public void DeleteCategory(int categoryId)
        {
            if (categoryId <= 0)
                throw new ArgumentNullException(nameof(categoryId), "invalid category id");

            if (_uow.CategoryRepository.GetModelById(categoryId) == null)
                throw new ArgumentNullException("there is no category exists in the database for that id");

            _uow.CategoryRepository.DeleteModel(categoryId);
        }

        public List<CategoryDto> GetAllCategories()
        {
            var categories = _uow.CategoryRepository.GetAllModels();
            if (categories == null || categories.Count == 0)
                throw new ArgumentNullException(nameof(categories), "there is no categories exists in the database");

            var categoryDtos = new List<CategoryDto>();

            foreach (var category in categories)
            {
                categoryDtos.Add(MapModelToDto(category));
            }
            return categoryDtos;
        }

        public CategoryDto GetCategoryById(int categoryId)
        {
            if (categoryId <= 0)
                throw new ArgumentNullException(nameof(categoryId), "invalid category id");

            var selectedCategory = _uow.CategoryRepository.GetModelById(categoryId);
            if (selectedCategory == null)
                throw new ArgumentNullException(nameof(selectedCategory), "there is no category exists in the database for that id");

            return MapModelToDto(selectedCategory);
        }

        public CategoryDto GetCategoryByName(string categoryName)
        {
            if (string.IsNullOrEmpty(categoryName))
                throw new ArgumentNullException(nameof(categoryName), "the category name can not be empty");

            var selectedCategory = _uow.CategoryRepository.GetAllModels().FirstOrDefault(c => c.CategoryName.ToString() == categoryName);
            if (selectedCategory == null)
                throw new ArgumentNullException(nameof(selectedCategory), "there is no category exists in the database for that name");

            return MapModelToDto(selectedCategory);
        }

        public void UpdateCategory(CategoryDto categoryDto)
        {
            if (categoryDto == null)
                throw new ArgumentNullException(nameof(categoryDto), "the data of the category can not be empty");

            if (_uow.CategoryRepository.GetModelById(categoryDto.CategoryId) == null)
                throw new ArgumentNullException("there is no category exists in the database for that id");

            _uow.CategoryRepository.UpdateModel(new Category
            {
                CategoryName = categoryDto.CategoryName,
                CategoryDescription = categoryDto.CategoryDescription
            });
        }

        private CategoryDto MapModelToDto(Category category)
        {
            return new CategoryDto
            {
                CategoryId = category.CategoryId,
                CategoryName = category.CategoryName,
                CategoryDescription = category.CategoryDescription!
            };
        }
    }
}
