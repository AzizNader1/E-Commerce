using E_Commerce.API.DTOs.OrderDTOs;
using E_Commerce.API.DTOs.OrderItemDTOs;
using E_Commerce.API.Models;
using E_Commerce.API.UnitOfWork;

namespace E_Commerce.API.Services
{
    /// <summary>
    /// This class represents the service layer for managing orders in an e-commerce application. It implements the IOrderService interface and provides methods for adding, deleting, retrieving, and updating orders. The service interacts with the database through a Unit of Work (UOW) pattern to perform operations on order data. Each method includes validation checks to ensure that the input data is valid and that the necessary related entities (such as users and order items) exist in the database. The service is responsible for handling all business logic related to orders, ensuring that the application functions correctly when users create or modify their orders.
    /// </summary>
    public class OrderService : IOrderService
    {
        private readonly UOW _uow;
        public OrderService(UOW uow)
        {
            _uow = uow;
        }

        public void AddOrder(CreateOrderDto createOrderDto)
        {
            if (createOrderDto == null)
                throw new ArgumentNullException(nameof(createOrderDto), "the data of the order can not be lefted as empty");

            if (createOrderDto.UserId <= 0)
                throw new ArgumentNullException(nameof(createOrderDto.UserId), "invalid data entried");

            _uow.OrderRepository.AddModel(new Order
            {
                UserId = createOrderDto.UserId,
                OrderDate = createOrderDto.OrderDate,
                TotalAmount = createOrderDto.TotalAmount,
                Status = createOrderDto.Status
            });
        }

        public void DeleteOrder(int orderId)
        {
            if (orderId <= 0)
                throw new ArgumentNullException(nameof(orderId), "invalid data entried");

            if (_uow.OrderRepository.GetModelById(orderId) == null)
                throw new ArgumentNullException("there is no orders exists for that id");

            var orderItems = _uow.OrderItemRepository.GetAllModels().Where(o => o.OrderId == orderId).ToList();
            foreach (var item in orderItems)
            {
                _uow.OrderItemRepository.DeleteModel(item.OrderItemId);
            }

            _uow.OrderRepository.DeleteModel(orderId);
        }

        public List<OrderDto> GetAllOrders()
        {
            var orders = _uow.OrderRepository.GetAllModels();
            if (orders == null || orders.Count == 0)
                throw new ArgumentNullException(nameof(orders), "there is no data exists in the database");

            var orderItemsDto = new List<OrderItemDto>();
            var orderItems = _uow.OrderItemRepository.GetAllModels();
            foreach (var item in orderItems)
            {
                orderItemsDto.Add(new OrderItemDto
                {
                    OrderItemId = item.OrderItemId,
                    OrderId = item.OrderId,
                    ProductId = item.ProductId,
                    Quantity = item.Quantity,
                    UnitPrice = item.UnitPrice
                });
            }

            var orderDtos = new List<OrderDto>();

            foreach (var order in orders)
            {
                orderDtos.Add(new OrderDto
                {
                    OrderId = order.OrderId,
                    UserId = order.UserId,
                    OrderDate = order.OrderDate,
                    TotalAmount = order.TotalAmount,
                    Status = order.Status,
                    OrderItems = orderItemsDto.Where(o => o.OrderId == order.OrderId).ToList()
                });
            }
            return orderDtos;
        }

        public List<OrderDto> GetAllOrdersByUserId(int userId)
        {
            if (userId <= 0)
                throw new ArgumentNullException(nameof(userId), "invalid data entried");

            var user = _uow.UserRepository.GetModelById(userId);
            if (user == null)
                throw new ArgumentNullException(nameof(user), "there is no data exists for that id");

            var orders = _uow.OrderRepository.GetAllModels();
            if (orders == null || orders.Count == 0)
                throw new ArgumentNullException(nameof(orders), "there is no data exists in the database");

            if (!orders.Any(o => o.UserId == userId))
                throw new ArgumentNullException("there is no order related to this user");

            var orderItemsDto = new List<OrderItemDto>();
            var orderItems = _uow.OrderItemRepository.GetAllModels();
            foreach (var item in orderItems)
            {
                orderItemsDto.Add(new OrderItemDto
                {
                    OrderItemId = item.OrderItemId,
                    OrderId = item.OrderId,
                    ProductId = item.ProductId,
                    Quantity = item.Quantity,
                    UnitPrice = item.UnitPrice
                });
            }

            var orderDtos = new List<OrderDto>();

            foreach (var order in orders)
            {
                if (order.UserId == userId)
                {
                    orderDtos.Add(new OrderDto
                    {
                        OrderId = order.OrderId,
                        UserId = order.UserId,
                        OrderDate = order.OrderDate,
                        TotalAmount = order.TotalAmount,
                        Status = order.Status,
                        OrderItems = orderItemsDto.Where(o => o.OrderId == order.OrderId).ToList()
                    });
                }
            }
            return orderDtos;

        }

        public List<OrderDto> GetAllOrdersByUserName(string userName)
        {
            if (userName == null || userName == "" || userName == default)
                throw new ArgumentNullException(nameof(userName), "invalid data entried");

            var user = _uow.UserRepository.GetAllModels().FirstOrDefault(u => u.UserName == userName);
            if (user == null)
                throw new ArgumentNullException(nameof(user), "there is no data exists for that id");

            var orders = _uow.OrderRepository.GetAllModels();
            if (orders == null || orders.Count == 0)
                throw new ArgumentNullException(nameof(orders), "there is no data exists in the database");

            if (!orders.Any(o => o.UserId == user.UserId))
                throw new ArgumentNullException("there is no orders related to this user");

            var orderItemsDto = new List<OrderItemDto>();
            var orderItems = _uow.OrderItemRepository.GetAllModels();
            foreach (var item in orderItems)
            {
                orderItemsDto.Add(new OrderItemDto
                {
                    OrderItemId = item.OrderItemId,
                    OrderId = item.OrderId,
                    ProductId = item.ProductId,
                    Quantity = item.Quantity,
                    UnitPrice = item.UnitPrice
                });
            }

            var orderDtos = new List<OrderDto>();

            foreach (var order in orders)
            {
                if (order.UserId == user.UserId)
                {
                    orderDtos.Add(new OrderDto
                    {
                        OrderId = order.OrderId,
                        UserId = order.UserId,
                        OrderDate = order.OrderDate,
                        TotalAmount = order.TotalAmount,
                        Status = order.Status,
                        OrderItems = orderItemsDto
                    });
                }
            }
            return orderDtos;
        }

        public OrderDto GetOrderById(int orderId)
        {
            if (orderId <= 0)
                throw new ArgumentNullException(nameof(orderId), "invalid data entried");

            var selectedOrder = _uow.OrderRepository.GetModelById(orderId);
            if (selectedOrder == null)
                throw new ArgumentNullException(nameof(selectedOrder), "there is no data exists for that id");

            var orderItemsDto = new List<OrderItemDto>();
            var orderItems = _uow.OrderItemRepository.GetAllModels().Where(o => o.OrderId == orderId);
            if (orderItems == null)
                throw new ArgumentNullException(nameof(orderItems), "there is no order items inside this order number");

            foreach (var item in orderItems)
            {
                orderItemsDto.Add(new OrderItemDto
                {
                    OrderItemId = item.OrderItemId,
                    OrderId = item.OrderId,
                    ProductId = item.ProductId,
                    Quantity = item.Quantity,
                    UnitPrice = item.UnitPrice
                });
            }

            return new OrderDto
            {
                OrderId = selectedOrder.OrderId,
                UserId = selectedOrder.UserId,
                OrderDate = selectedOrder.OrderDate,
                TotalAmount = selectedOrder.TotalAmount,
                Status = selectedOrder.Status,
                OrderItems = orderItemsDto
            };
        }

        public void UpdateOrder(OrderDto orderDto)
        {
            if (orderDto == null)
                throw new ArgumentNullException(nameof(orderDto), "invalid data entried");

            var selectedOrder = _uow.OrderRepository.GetModelById(orderDto.OrderId);
            if (selectedOrder == null)
                throw new ArgumentNullException(nameof(selectedOrder), "there is no orders exists for that id");

            selectedOrder.Status = orderDto.Status;

            _uow.OrderRepository.UpdateModel(selectedOrder);
        }
    }
}
