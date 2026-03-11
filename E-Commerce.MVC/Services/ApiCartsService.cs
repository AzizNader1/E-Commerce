using E_Commerce.MVC.DTOs.CartDTOs;
using E_Commerce.MVC.DTOs.CartItemDTOs;
using E_Commerce.MVC.DTOs.ProductDTOs;
using E_Commerce.MVC.DTOs.UserDTOs;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace E_Commerce.MVC.Services
{
    public class ApiCartsService : IApiCartsService
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public ApiCartsService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        private static JsonSerializerOptions CreateJsonOptions()
        {
            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            options.Converters.Add(new JsonStringEnumConverter());
            return options;
        }

        public async Task<CartShoppingDto> GetCartByUserNameAsync(string userName)
        {
            var client = _httpClientFactory.CreateClient("ECommerceApi");
            var response = await client.GetAsync($"Carts/GetAllCartsByUserName/{userName}");

            if (!response.IsSuccessStatusCode)
                return null;

            var content = await response.Content.ReadAsStringAsync();
            var carts = JsonSerializer.Deserialize<List<CartDto>>(content, CreateJsonOptions());

            // Return the first cart or null
            var wantedCart = carts?.FirstOrDefault();
            if (wantedCart!.CartItems.Count() == 0)
                return new CartShoppingDto
                {
                    CartId = wantedCart.CartId,
                    UserId = wantedCart.UserId,
                    UserName = userName,
                    CartItems = new List<CartWithProductDto>()
                };

            var productsIds = new List<int>();
            foreach (var item in wantedCart.CartItems)
            {
                productsIds.Add(item.ProductId);
            }

            var products = new List<ProductDto>();
            foreach (var id in productsIds)
            {
                var productResponse = await client.GetAsync($"Products/GetProductById/{id}");

                if (!productResponse.IsSuccessStatusCode)
                    return new CartShoppingDto
                    {
                        CartId = wantedCart.CartId,
                        UserId = wantedCart.UserId,
                        UserName = userName,
                        CartItems = new List<CartWithProductDto>()
                    };
                var productContent = await productResponse.Content.ReadAsStringAsync();
                products.Add(JsonSerializer.Deserialize<ProductDto>(productContent, CreateJsonOptions())!);
            }

            var cartItemsWithProducts = new List<CartWithProductDto>();
            foreach (var product in products)
            {
                var cartItem = wantedCart.CartItems.FirstOrDefault(ci => ci.ProductId == product.ProductId);
                if (cartItem != null)
                {
                    cartItemsWithProducts.Add(new CartWithProductDto
                    {
                        CartId = cartItem.CartId,
                        CartItemId = cartItem.CartItemId,
                        ProductId = cartItem.ProductId,
                        Quantity = cartItem.Quantity,
                        ProductName = product.ProductName,
                        ProductPrice = product.ProductPrice,
                        ProductImage = product.ProductImage,
                        ProductDescription = product.ProductDescription,
                        ProductImageContentType = product.ProductImageContentType,
                        ProductStockQuantity = product.ProductStockQuantity
                    });
                }
            }
            return new CartShoppingDto
            {
                CartId = wantedCart?.CartId ?? 0,
                UserId = wantedCart?.UserId ?? 0,
                UserName = userName,
                CartItems = cartItemsWithProducts ?? new List<CartWithProductDto>()
            };
        }

        public async Task<CartShoppingDto> GetCartByIdAsync(int cartId)
        {
            var client = _httpClientFactory.CreateClient("ECommerceApi");
            var response = await client.GetAsync($"Carts/GetCartById/{cartId}");

            if (!response.IsSuccessStatusCode)
                return null;

            var content = await response.Content.ReadAsStringAsync();
            var cartDto = JsonSerializer.Deserialize<CartDto>(content, CreateJsonOptions());
            if (cartDto!.CartItems.Count() == 0)
                return new CartShoppingDto
                {
                    CartId = cartDto.CartId,
                    UserId = cartDto.UserId,
                    UserName = "",
                    CartItems = new List<CartWithProductDto>()
                };

            var productsIds = new List<int>();
            foreach (var item in cartDto.CartItems)
            {
                productsIds.Add(item.ProductId);
            }

            var products = new List<ProductDto>();
            foreach (var id in productsIds)
            {
                var productResponse = await client.GetAsync($"Products/GetProductById/{id}");

                if (!productResponse.IsSuccessStatusCode)
                    return new CartShoppingDto
                    {
                        CartId = cartDto.CartId,
                        UserId = cartDto.UserId,
                        UserName = "",
                        CartItems = new List<CartWithProductDto>()
                    };
                var productContent = await productResponse.Content.ReadAsStringAsync();
                products.Add(JsonSerializer.Deserialize<ProductDto>(productContent, CreateJsonOptions())!);
            }

            var cartItemsWithProducts = new List<CartWithProductDto>();
            foreach (var product in products)
            {
                var cartItem = cartDto.CartItems.FirstOrDefault(ci => ci.ProductId == product.ProductId);
                if (cartItem != null)
                {
                    cartItemsWithProducts.Add(new CartWithProductDto
                    {
                        CartItemId = cartItem.CartItemId,
                        ProductId = cartItem.ProductId,
                        Quantity = cartItem.Quantity,
                        ProductName = product.ProductName,
                        ProductPrice = product.ProductPrice,
                        ProductImage = product.ProductImage,
                        ProductDescription = product.ProductDescription,
                        ProductImageContentType = product.ProductImageContentType,
                        ProductStockQuantity = product.ProductStockQuantity,
                    });
                }
            }
            return new CartShoppingDto
            {
                CartId = cartDto?.CartId ?? 0,
                UserId = cartDto?.UserId ?? 0,
                UserName = "",
                CartItems = cartItemsWithProducts ?? new List<CartWithProductDto>()
            };
        }

        public async Task<CartWithProductDto> AddCartItemAsync(CreateCartItemDto dto)
        {
            var client = _httpClientFactory.CreateClient("ECommerceApi");
            var response = await client.PostAsJsonAsync("CartItems/AddCartItem", dto);

            if (!response.IsSuccessStatusCode)
                return null;

            // After adding, get the updated cart items to return the new item with product details
            var itemsResponse = await client.GetAsync($"CartItems/GetCartItemsByCartId/{dto.CartId}");
            if (itemsResponse.IsSuccessStatusCode)
            {
                var content = await itemsResponse.Content.ReadAsStringAsync();
                var items = JsonSerializer.Deserialize<List<CartItemDto>>(content, CreateJsonOptions());
                // Return the newly added item (last one with matching product)
                var justAddedItems = items?.LastOrDefault(i => i.ProductId == dto.ProductId);
                if (justAddedItems != null)
                {
                    var productResponse = await client.GetAsync($"Products/GetProductById/{justAddedItems.ProductId}");
                    if (productResponse.IsSuccessStatusCode)
                    {
                        var productContent = await productResponse.Content.ReadAsStringAsync();
                        var product = JsonSerializer.Deserialize<ProductDto>(productContent, CreateJsonOptions());
                        return new CartWithProductDto
                        {
                            CartId = dto.CartId,
                            CartItemId = justAddedItems.CartItemId,
                            ProductId = justAddedItems.ProductId,
                            Quantity = justAddedItems.Quantity,
                            ProductName = product!.ProductName,
                            ProductPrice = product.ProductPrice,
                            ProductImage = product.ProductImage,
                            ProductDescription = product.ProductDescription,
                            ProductImageContentType = product.ProductImageContentType,
                            ProductStockQuantity = product.ProductStockQuantity,
                        };
                    }
                }
            }

            return new CartWithProductDto { CartId = dto.CartId, ProductId = dto.ProductId, Quantity = dto.Quantity };
        }

        public async Task<CartWithProductDto> UpdateCartItemAsync(CartItemDto dto)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> DeleteCartItemAsync(int cartItemId)
        {
            var client = _httpClientFactory.CreateClient("ECommerceApi");
            var response = await client.DeleteAsync($"CartItems/DeleteCartItem/{cartItemId}");

            return response.IsSuccessStatusCode;
        }

        public async Task<bool> ClearCartAsync(int cartId)
        {
            var client = _httpClientFactory.CreateClient("ECommerceApi");

            // Get all cart items
            var response = await client.GetAsync($"CartItems/GetCartItemsByCartId/{cartId}");
            if (!response.IsSuccessStatusCode)
                return false;

            var content = await response.Content.ReadAsStringAsync();
            var items = JsonSerializer.Deserialize<List<CartItemDto>>(content, CreateJsonOptions());

            // Delete each item
            if (items != null)
            {
                foreach (var item in items)
                {
                    await client.DeleteAsync($"CartItems/DeleteCartItem/{item.CartItemId}");
                }
            }

            return true;
        }

        public async Task<bool> GetCartByUserName(string userName)
        {
            var client = _httpClientFactory.CreateClient("ECommerceApi");

            var userResponse = await client.GetAsync($"Users/GetAllUsers");
            var userContent = await userResponse.Content.ReadAsStringAsync();
            var users = JsonSerializer.Deserialize<List<UserDto>>(userContent, CreateJsonOptions());
            var wantedUser = users?.Where(a => a.UserName == userName).FirstOrDefault();

            var response = await client.GetAsync($"Carts/GetAllCarts");
            var content = await response.Content.ReadAsStringAsync();
            var carts = JsonSerializer.Deserialize<List<CartDto>>(content, CreateJsonOptions());

            if (!carts!.Any() || carts!.Where(c => c.UserId == wantedUser!.UserId).FirstOrDefault() == null)
            {
                var createCartResponse = await client.PostAsJsonAsync("Carts/AddCart", new CreateCartDto { UserId = wantedUser!.UserId });
                if (!createCartResponse.IsSuccessStatusCode)
                    return false;
            }
            return true;
        }
    }
}
