namespace E_Commerce.MVC.DTOs.CartDTOs
{
    public class CartWithProductDto
    {
        public int CartItemId { get; set; }
        public int CartId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }

        // Product details for display
        public string? ProductName { get; set; }
        public string? ProductDescription { get; set; }
        public decimal ProductPrice { get; set; }
        public string? ProductImageContentType { get; set; }
        public byte[]? ProductImage { get; set; }
        public int ProductStockQuantity { get; set; }
        public string? CategoryName { get; set; }

        // Computed properties
        public decimal TotalPrice => ProductPrice * Quantity;

        public string? ImageSrc
        {
            get
            {
                if (ProductImage != null && ProductImage.Length > 0 && !string.IsNullOrEmpty(ProductImageContentType))
                {
                    return $"data:{ProductImageContentType};base64,{Convert.ToBase64String(ProductImage)}";
                }
                return "https://placehold.co/100x100/1e1b4b/a855f7?text=No+Image";
            }
        }
    }
}
