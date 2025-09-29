using System.ComponentModel.DataAnnotations;

namespace E_Commerce.API.DTOs
{
    public class CategoryDto
    {
        public int CategoryId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
