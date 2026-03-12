using E_Commerce.MVC.DTOs.OrderDTOs;
using E_Commerce.MVC.DTOs.OrderItemDTOs;
using E_Commerce.MVC.DTOs.ProductDTOs;
using E_Commerce.MVC.Models;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace E_Commerce.MVC.Services
{
    public class ApiOrdersService : IApiOrdersService
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public ApiOrdersService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        private static JsonSerializerOptions CreateJsonOptions()
        {
            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            options.Converters.Add(new JsonStringEnumConverter());
            return options;
        }

        public async Task<List<OrderProductDto>> GetAllOrdersAsync()
        {
            var productsOfOrder = new List<OrderProductDto>();
            var itemsDataForEachProductOrder = new List<OrderWithProductsItemDto>();

            var client = _httpClientFactory.CreateClient("ECommerceApi");
            var response = await client.GetAsync("Orders/GetAllOrders");

            if (!response.IsSuccessStatusCode)
                return null;

            var content = await response.Content.ReadAsStringAsync();
            var orderDtos = JsonSerializer.Deserialize<List<OrderDto>>(content, CreateJsonOptions()) ?? new List<OrderDto>();

            var productsIds = orderDtos.SelectMany(o => o.OrderItems.Select(oi => oi.ProductId)).ToList();
            var products = new List<ProductDto>();

            foreach (var id in productsIds)
            {
                var productResponse = await client.GetAsync($"Products/GetProductById/{id}");
                if (productResponse.IsSuccessStatusCode)
                {
                    var productContent = await productResponse.Content.ReadAsStringAsync();
                    var product = JsonSerializer.Deserialize<ProductDto>(productContent, CreateJsonOptions());
                    if (product != null)
                    {
                        itemsDataForEachProductOrder.Add(new OrderWithProductsItemDto
                        {
                            OrderId = orderDtos.SelectMany(o => o.OrderItems).FirstOrDefault(oi => oi.ProductId == id)?.OrderId ?? 0,
                            ProductId = product.ProductId,
                            ProductName = product.ProductName,
                            ProductDescription = product.ProductDescription,
                            UnitPrice = product.ProductPrice,
                            Quantity = product.ProductStockQuantity,
                            ProductImage = product.ProductImage,
                            ProductImageContentType = product.ProductImageContentType
                        });
                    }
                }
            }

            foreach (var order in orderDtos)
            {
                var orderProductDto = new OrderProductDto
                {
                    OrderId = order.OrderId,
                    UserId = order.UserId,
                    OrderDate = order.OrderDate,
                    Status = order.Status,
                    TotalAmount = order.TotalAmount,
                    OrderItems = itemsDataForEachProductOrder.Where(i => i.OrderId == order.OrderId).ToList()
                };
                productsOfOrder.Add(orderProductDto);
            }

            return productsOfOrder;

        }

        public async Task<OrderProductDto?> GetOrderByIdAsync(int orderId)
        {
            var itemsDataForEachProductOrder = new List<OrderWithProductsItemDto>();

            var client = _httpClientFactory.CreateClient("ECommerceApi");
            var response = await client.GetAsync("Orders/GetOrderById");

            if (!response.IsSuccessStatusCode)
                return null;

            var content = await response.Content.ReadAsStringAsync();
            var orderDtos = JsonSerializer.Deserialize<OrderDto>(content, CreateJsonOptions()) ?? new OrderDto();

            var productsIds = orderDtos.OrderItems.Select(oi => oi.ProductId).ToList();
            var products = new List<ProductDto>();

            foreach (var id in productsIds)
            {
                var productResponse = await client.GetAsync($"Products/GetProductById/{id}");
                if (productResponse.IsSuccessStatusCode)
                {
                    var productContent = await productResponse.Content.ReadAsStringAsync();
                    var product = JsonSerializer.Deserialize<ProductDto>(productContent, CreateJsonOptions());
                    if (product != null)
                    {
                        itemsDataForEachProductOrder.Add(new OrderWithProductsItemDto
                        {
                            OrderId = orderDtos.OrderItems.FirstOrDefault(oi => oi.ProductId == id)?.OrderId ?? 0,
                            ProductId = product.ProductId,
                            ProductName = product.ProductName,
                            ProductDescription = product.ProductDescription,
                            UnitPrice = product.ProductPrice,
                            Quantity = product.ProductStockQuantity,
                            ProductImage = product.ProductImage,
                            ProductImageContentType = product.ProductImageContentType
                        });
                    }
                }
            }

            return new OrderProductDto
            {
                OrderId = orderDtos.OrderId,
                UserId = orderDtos.UserId,
                OrderDate = orderDtos.OrderDate,
                Status = orderDtos.Status,
                TotalAmount = orderDtos.TotalAmount,
                OrderItems = itemsDataForEachProductOrder
            };

        }

        public async Task<List<OrderProductDto>> GetOrdersByUserIdAsync(int userId)
        {
            var productsOfOrder = new List<OrderProductDto>();
            var itemsDataForEachProductOrder = new List<OrderWithProductsItemDto>();
            var client = _httpClientFactory.CreateClient("ECommerceApi");
            var response = await client.GetAsync($"Orders/GetAllOrdersByUserId/{userId}");

            if (!response.IsSuccessStatusCode)
                return null;

            var content = await response.Content.ReadAsStringAsync();
            var orderDtos = JsonSerializer.Deserialize<List<OrderDto>>(content, CreateJsonOptions()) ?? new List<OrderDto>();

            var productsIds = orderDtos.SelectMany(o => o.OrderItems.Select(oi => oi.ProductId)).ToList();
            var products = new List<ProductDto>();

            foreach (var id in productsIds)
            {
                var productResponse = await client.GetAsync($"Products/GetProductById/{id}");
                if (productResponse.IsSuccessStatusCode)
                {
                    var productContent = await productResponse.Content.ReadAsStringAsync();
                    var product = JsonSerializer.Deserialize<ProductDto>(productContent, CreateJsonOptions());
                    if (product != null)
                    {
                        itemsDataForEachProductOrder.Add(new OrderWithProductsItemDto
                        {
                            OrderId = orderDtos.SelectMany(o => o.OrderItems).FirstOrDefault(oi => oi.ProductId == id)?.OrderId ?? 0,
                            ProductId = product.ProductId,
                            ProductName = product.ProductName,
                            ProductDescription = product.ProductDescription,
                            UnitPrice = product.ProductPrice,
                            Quantity = product.ProductStockQuantity,
                            ProductImage = product.ProductImage,
                            ProductImageContentType = product.ProductImageContentType
                        });
                    }
                }
            }

            foreach (var order in orderDtos)
            {
                var orderProductDto = new OrderProductDto
                {
                    OrderId = order.OrderId,
                    UserId = order.UserId,
                    OrderDate = order.OrderDate,
                    Status = order.Status,
                    TotalAmount = order.TotalAmount,
                    OrderItems = itemsDataForEachProductOrder.Where(i => i.OrderId == order.OrderId).ToList()
                };
                productsOfOrder.Add(orderProductDto);
            }

            return productsOfOrder;
        }

        public async Task<List<OrderProductDto>> GetOrdersByUserNameAsync(string userName)
        {
            var productsOfOrder = new List<OrderProductDto>();
            var itemsDataForEachProductOrder = new List<OrderWithProductsItemDto>();
            var client = _httpClientFactory.CreateClient("ECommerceApi");
            var response = await client.GetAsync($"Orders/GetAllOrdersByUserName/{Uri.EscapeDataString(userName)}");

            if (!response.IsSuccessStatusCode)
                return null;

            var content = await response.Content.ReadAsStringAsync();
            var orderDtos = JsonSerializer.Deserialize<List<OrderDto>>(content, CreateJsonOptions()) ?? new List<OrderDto>();

            var productsIds = orderDtos.SelectMany(o => o.OrderItems.Select(oi => oi.ProductId)).ToList();
            var products = new List<ProductDto>();

            foreach (var id in productsIds)
            {
                var productResponse = await client.GetAsync($"Products/GetProductById/{id}");
                if (productResponse.IsSuccessStatusCode)
                {
                    var productContent = await productResponse.Content.ReadAsStringAsync();
                    var product = JsonSerializer.Deserialize<ProductDto>(productContent, CreateJsonOptions());
                    if (product != null)
                    {
                        itemsDataForEachProductOrder.Add(new OrderWithProductsItemDto
                        {
                            OrderId = orderDtos.SelectMany(o => o.OrderItems).FirstOrDefault(oi => oi.ProductId == id)?.OrderId ?? 0,
                            ProductId = product.ProductId,
                            ProductName = product.ProductName,
                            ProductDescription = product.ProductDescription,
                            UnitPrice = product.ProductPrice,
                            Quantity = product.ProductStockQuantity,
                            ProductImage = product.ProductImage,
                            ProductImageContentType = product.ProductImageContentType
                        });
                    }
                }
            }

            foreach (var order in orderDtos)
            {
                var orderProductDto = new OrderProductDto
                {
                    OrderId = order.OrderId,
                    UserId = order.UserId,
                    OrderDate = order.OrderDate,
                    Status = order.Status,
                    TotalAmount = order.TotalAmount,
                    OrderItems = itemsDataForEachProductOrder.Where(i => i.OrderId == order.OrderId).ToList()
                };
                productsOfOrder.Add(orderProductDto);
            }

            return productsOfOrder;
        }

        public async Task<OrderProductDto?> CreateOrderAsync(CreateOrderDto dto)
        {
            var client = _httpClientFactory.CreateClient("ECommerceApi");
            var response = await client.PostAsJsonAsync("Orders/AddOrder", dto);

            if (!response.IsSuccessStatusCode)
                return null;

            // Return a basic order DTO with the created info
            return new OrderProductDto
            {
                UserId = dto.UserId,
                OrderDate = dto.OrderDate,
                Status = dto.Status,
                TotalAmount = dto.TotalAmount
            };
        }

        public async Task<bool> UpdateOrderStatusAsync(int orderId, OrderStatus status)
        {
            var client = _httpClientFactory.CreateClient("ECommerceApi");

            // First get the existing order
            var order = await GetOrderByIdAsync(orderId);
            if (order == null)
                return false;

            // Update with new status - create update object matching API expectations
            var updateDto = new
            {
                OrderId = orderId,
                UserId = order.UserId,
                OrderDate = order.OrderDate,
                TotalAmount = order.TotalAmount,
                Status = status
            };

            var json = JsonSerializer.Serialize(updateDto, CreateJsonOptions());
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await client.PutAsync("Orders/UpdateOrder", content);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteOrderAsync(int orderId)
        {
            var client = _httpClientFactory.CreateClient("ECommerceApi");
            var response = await client.DeleteAsync($"Orders/DeleteOrder/{orderId}");
            return response.IsSuccessStatusCode;
        }
    }
}
