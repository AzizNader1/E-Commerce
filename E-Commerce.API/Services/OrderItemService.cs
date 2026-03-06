using E_Commerce.API.DTOs.OrderItemDTOs;
using E_Commerce.API.Models;
using E_Commerce.API.Repositories;
using E_Commerce.API.UnitOfWork;

namespace E_Commerce.API.Services
{
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
                throw new ArgumentNullException(nameof(createOrderItemDto),"order item data can not be null");

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

            OrderItem selectedOrderItem = _uow.OrderItemRepository.GetModelById(orderItemId);
            if (selectedOrderItem == null)
                throw new ArgumentNullException(nameof(selectedOrderItem), "No order item found for the given ID");

            _uow.OrderItemRepository.DeleteModel(orderItemId);
        }

        public List<OrderItemDto> GetAllOrderItems()
        {
            List<OrderItem> orderItems = _uow.OrderItemRepository.GetAllModels();
            if (orderItems == null || orderItems.Count == 0)
                throw new ArgumentNullException(nameof(orderItems), "No order items found in the database");

            List<OrderItemDto> orderItemDtos = new List<OrderItemDto>();
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

            OrderItem selectedOrderItem = _uow.OrderItemRepository.GetModelById(orderItemId);
            if (selectedOrderItem == null)
                throw new ArgumentNullException(nameof(selectedOrderItem), "No order item found for the given ID");

            return new OrderItemDto { 
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

            Order order = _uow.OrderRepository.GetModelById(orderId);
            if (order == null)
                throw new ArgumentNullException(nameof(order), "No order found for the given ID");

            List<OrderItem> orderItems = _uow.OrderItemRepository.GetAllModels();
            if (orderItems == null || orderItems.Count == 0)
                throw new ArgumentNullException(nameof(orderItems), "No order items found in the database");

            List<OrderItemDto> orderItemDtos = new List<OrderItemDto>();

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

            OrderItem existingOrderItem = _uow.OrderItemRepository.GetModelById(orderItemDto.OrderItemId);
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
