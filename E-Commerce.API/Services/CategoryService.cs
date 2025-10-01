using E_Commerce.API.DTOs.CategoryDTOs;
using E_Commerce.API.Models;
using E_Commerce.API.Repositories;
using E_Commerce.API.UnitOfWork;

namespace E_Commerce.API.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly UOW _uow;
        public CategoryService(UOW uow)
        {
            _uow = uow;
        }

        public void AddCategoryAsync(CreateCategoryDto createCategoryDto)
        {
            if(createCategoryDto == null)
                throw new ArgumentNullException(nameof(createCategoryDto),"the data of the category can not be empty");
            
            _uow.CategoryRepository.AddAsync(new Category { 
                Name = createCategoryDto.Name,
                Description = createCategoryDto.Description
            });
        }

        public void DeleteCategoryAsync(int categoryId)
        {
            if(categoryId == null || categoryId == 0)
                throw new ArgumentNullException(nameof(categoryId),"invalid category id");

            Category selectedCategory = _uow.CategoryRepository.GetByIdAsync(categoryId);
            if(selectedCategory == null)
                throw new ArgumentNullException(nameof(selectedCategory),"there is no category exists in the database for that id");

            _uow.CategoryRepository.DeleteAsync(categoryId);
        }

        public List<CategoryDto> GetAllCategoriesAsync()
        {
            List<Category> categories = _uow.CategoryRepository.GetAllAsync();
            if(categories == null || categories.Count == 0)
                throw new ArgumentNullException(nameof(categories),"there is no categories exists in the database");

            List<CategoryDto> categoryDtos = new List<CategoryDto>();
           
            foreach(var category in categories)
            {
                categoryDtos.Add(new CategoryDto
                {
                    CategoryId = category.CategoryId,
                    Name = category.Name,
                    Description = category.Description
                });
            }
            return categoryDtos;
        }

        public CategoryDto GetCategoryByIdAsync(int categoryId)
        {
            if(categoryId == null || categoryId == 0)
                throw new ArgumentNullException(nameof(categoryId),"invalid category id");

            Category selectedCategory = _uow.CategoryRepository.GetByIdAsync(categoryId);
            if(selectedCategory == null)
                throw new ArgumentNullException(nameof(selectedCategory),"there is no category exists in the database for that id");

            return new CategoryDto
            {
                CategoryId = selectedCategory.CategoryId,
                Name = selectedCategory.Name,
                Description = selectedCategory.Description
            };
        }

        public void UpdateCategoryAsync(CategoryDto categoryDto)
        {
            if(categoryDto == null)
                throw new ArgumentNullException(nameof(categoryDto),"the data of the category can not be empty");

            Category selectedCategory = _uow.CategoryRepository.GetByIdAsync(categoryDto.CategoryId);
            if(selectedCategory == null)
                throw new ArgumentNullException(nameof(selectedCategory),"there is no category exists in the database for that id");

            _uow.CategoryRepository.UpdateAsync(new Category
            {
                Name = categoryDto.Name,
                Description = categoryDto.Description
            });
        }
    }
}
