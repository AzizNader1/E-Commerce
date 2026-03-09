using E_Commerce.API.DTOs.OrderItemDTOs;
using E_Commerce.API.Models;
using E_Commerce.API.UnitOfWork;

namespace E_Commerce.API.Services
{
    /// <summary>
    /// This class represents the service layer for managing order items in an e-commerce application. It implements the IOrderItemService interface and provides methods for adding, deleting, retrieving, and updating order items. The service interacts with the database through a Unit of Work (UOW) pattern to perform operations on order item data. Each method includes validation checks to ensure that the input data is valid and that the necessary related entities (such as orders and products) exist in the database. The service is responsible for handling all business logic related to order items, ensuring that the application functions correctly when users create or modify their orders.
    /// </summary>
    public class OrderItemService : IOrderItemService
    {
        private readonly UOW _uow;
        public OrderItemService(UOW uow)
        {
            _uow = uow;
        }

        public void AddOrderItem(CreateOrderItemDto createOrderItemDto)
        {
            if (createOrderItemDto == null)
                throw new ArgumentNullException(nameof(createOrderItemDto), "order item data can not be null");

            if (!_uow.OrderRepository.GetAllModels().Any(o => o.OrderId == createOrderItemDto.OrderId))
                throw new ArgumentException("No order found for the given order ID", nameof(createOrderItemDto.OrderId));

            var product = _uow.ProductRepository.GetModelById(createOrderItemDto.ProductId);
            if (product == null)
                throw new ArgumentException("No product found for the given product ID", nameof(createOrderItemDto.ProductId));

            if (createOrderItemDto.Quantity <= 0)
                throw new ArgumentException("Quantity must be greater than zero", nameof(createOrderItemDto.Quantity));

            if (createOrderItemDto.Quantity > product.ProductStockQuantity)
                throw new ArgumentException("Quantity exceeds available stock", nameof(createOrderItemDto.Quantity));

            _uow.OrderItemRepository.AddModel(new OrderItem
            {
                OrderId = createOrderItemDto.OrderId,
                ProductId = createOrderItemDto.ProductId,
                Quantity = createOrderItemDto.Quantity,
                UnitPrice = createOrderItemDto.UnitPrice
            });
        }

        public void DeleteOrderItem(int orderItemId)
        {
            if (orderItemId <= 0)
                throw new ArgumentException("Invalid order item ID", nameof(orderItemId));

            var selectedOrderItem = _uow.OrderItemRepository.GetModelById(orderItemId);
            if (selectedOrderItem == null)
                throw new ArgumentNullException(nameof(selectedOrderItem), "No order item found for the given ID");

            _uow.OrderItemRepository.DeleteModel(orderItemId);
        }

        public List<OrderItemDto> GetAllOrderItems()
        {
            var orderItems = _uow.OrderItemRepository.GetAllModels();
            if (orderItems == null || orderItems.Count == 0)
                throw new ArgumentNullException(nameof(orderItems), "No order items found in the database");

            var orderItemDtos = new List<OrderItemDto>();
            foreach (var orderItem in orderItems)
            {
                orderItemDtos.Add(new OrderItemDto
                {
                    OrderItemId = orderItem.OrderItemId,
                    OrderId = orderItem.OrderId,
                    ProductId = orderItem.ProductId,
                    Quantity = orderItem.Quantity,
                    UnitPrice = orderItem.UnitPrice
                });
            }
            return orderItemDtos;
        }

        public OrderItemDto GetOrderItemById(int orderItemId)
        {
            if (orderItemId <= 0)
                throw new ArgumentException("Invalid order item ID", nameof(orderItemId));

            var selectedOrderItem = _uow.OrderItemRepository.GetModelById(orderItemId);
            if (selectedOrderItem == null)
                throw new ArgumentNullException(nameof(selectedOrderItem), "No order item found for the given ID");

            return new OrderItemDto
            {
                OrderItemId = selectedOrderItem.OrderItemId,
                OrderId = selectedOrderItem.OrderId,
                ProductId = selectedOrderItem.ProductId,
                Quantity = selectedOrderItem.Quantity,
                UnitPrice = selectedOrderItem.UnitPrice
            };
        }

        public List<OrderItemDto> GetOrderItemsByOrderId(int orderId)
        {
            if (orderId <= 0)
                throw new ArgumentException("Invalid order ID", nameof(orderId));

            var order = _uow.OrderRepository.GetModelById(orderId);
            if (order == null)
                throw new ArgumentNullException(nameof(order), "No order found for the given ID");

            var orderItems = _uow.OrderItemRepository.GetAllModels();
            if (orderItems == null || orderItems.Count == 0)
                throw new ArgumentNullException(nameof(orderItems), "No order items found in the database");

            var orderItemDtos = new List<OrderItemDto>();

            foreach (var orderItem in orderItems)
            {
                if (orderItem.OrderId == orderId)
                {
                    orderItemDtos.Add(new OrderItemDto
                    {
                        OrderItemId = orderItem.OrderItemId,
                        OrderId = orderItem.OrderId,
                        ProductId = orderItem.ProductId,
                        Quantity = orderItem.Quantity,
                        UnitPrice = orderItem.UnitPrice
                    });
                }
            }
            return orderItemDtos;
        }

        public void UpdateOrderItem(OrderItemDto orderItemDto)
        {
            if (orderItemDto == null)
                throw new ArgumentNullException(nameof(orderItemDto), "Order item data can not be null");

            var existingOrderItem = _uow.OrderItemRepository.GetModelById(orderItemDto.OrderItemId);
            if (existingOrderItem == null)
                throw new ArgumentNullException(nameof(existingOrderItem), "No order item found for the given ID");

            _uow.OrderItemRepository.UpdateModel(new OrderItem
            {
                Quantity = orderItemDto.Quantity,
                UnitPrice = orderItemDto.UnitPrice
            });
        }
    }
}
