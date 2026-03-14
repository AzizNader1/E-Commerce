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
                            OrderItemQuantity = product.ProductStockQuantity,
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
                            OrderItemQuantity = product.ProductStockQuantity,
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
                            OrderItemQuantity = product.ProductStockQuantity,
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
                            OrderItemQuantity = product.ProductStockQuantity,
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

        public async Task<OrderProductDto?> CreateOrderAsync(CheckoutViewModel checkoutViewModel)
        {
            var client = _httpClientFactory.CreateClient("ECommerceApi");
            var dto = new CreateOrderDto
            {
                UserId = checkoutViewModel.User!.UserId,
                OrderDate = DateTime.UtcNow,
                Status = OrderStatus.Pending,
                TotalAmount = checkoutViewModel.TotalPrice
            };
            var response = await client.PostAsJsonAsync("Orders/AddOrder", dto);

            if (!response.IsSuccessStatusCode)
                return null;

            // get the id of the order that just created to add the items which related to it

            var ordersResponse = await client.GetAsync("Orders/GetAllOrders");
            if (!ordersResponse.IsSuccessStatusCode)
                return null;

            var ordersContent = await ordersResponse.Content.ReadAsStringAsync();
            var orders = JsonSerializer.Deserialize<List<OrderDto>>(ordersContent, CreateJsonOptions()) ?? new List<OrderDto>();
            var createdOrder = orders.LastOrDefault(o => o.UserId == checkoutViewModel.User.UserId);

            // add order items to the order

            var orderItemDto = new CreateOrderItemDto
            {
                OrderId = createdOrder!.OrderId,
                ProductId = checkoutViewModel.Product!.ProductId,
                OrderItemQuantity = checkoutViewModel.OrderQuantity,
                UnitPrice = checkoutViewModel.Product.ProductPrice
            };

            var orderItemResponse = await client.PostAsJsonAsync("OrderItems/AddOrderItem", orderItemDto);
            if (!orderItemResponse.IsSuccessStatusCode)
            {
                var deleteResponse = await client.DeleteAsync($"Orders/DeleteOrder/{createdOrder.OrderId}");
                return null;
            }

            // get the id of the order item that just created to add the items which related to it
            var orderItemsResponse = await client.GetAsync("OrderItems/GetAllOrderItems");
            if (!orderItemsResponse.IsSuccessStatusCode)
                return null;

            var orderItemsContent = await orderItemsResponse.Content.ReadAsStringAsync();
            var orderItems = JsonSerializer.Deserialize<List<OrderItemDto>>(orderItemsContent, CreateJsonOptions()) ?? new List<OrderItemDto>();
            var createdOrderItem = orderItems.LastOrDefault(oi => oi.OrderId == createdOrder.OrderId && oi.ProductId == checkoutViewModel.Product.ProductId);

            // update product quantity in stock
            var updateQuantityResponse = await client.PutAsync(
                    $"Products/UpdateProductQuantity?productId={checkoutViewModel.Product.ProductId}&quantity={checkoutViewModel.OrderQuantity}", null);

            if (!updateQuantityResponse.IsSuccessStatusCode)
            {
                var deleteResponse = await client.DeleteAsync($"Orders/DeleteOrder/{createdOrder.OrderId}");
                var deleteOrderItemResponse = await client.DeleteAsync($"OrderItems/DeleteOrderItem/{createdOrderItem!.OrderItemId}");
                return null;
            }

            var updateProductContent = await updateQuantityResponse.Content.ReadAsStringAsync();
            var updateProductResult = JsonSerializer.Deserialize<ProductDto>(updateProductContent, CreateJsonOptions());


            return new OrderProductDto
            {
                OrderDate = DateTime.UtcNow,
                Status = OrderStatus.Pending,
                TotalAmount = checkoutViewModel.TotalPrice,
                OrderId = createdOrder.OrderId,
                UserId = checkoutViewModel.User.UserId
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
            var updateDto = new OrderDto
            {
                OrderId = orderId,
                UserId = order.UserId,
                OrderDate = order.OrderDate,
                TotalAmount = order.TotalAmount,
                Status = status,
                OrderItems = order.OrderItems.Select(oi => new OrderItemDto
                {
                    OrderItemId = oi.OrderItemId,
                    OrderId = oi.OrderId,
                    ProductId = oi.ProductId,
                    Quantity = oi.OrderItemQuantity,
                    UnitPrice = oi.UnitPrice
                }).ToList()
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
