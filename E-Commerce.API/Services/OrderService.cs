using E_Commerce.API.Repositories;
using E_Commerce.API.UnitOfWork;
using E_Commerce.API.Models;
using E_Commerce.API.DTOs.OrderDTOs;

namespace E_Commerce.API.Services
{
    public class OrderService : IOrderService
    {
        private readonly UOW _uow;
        public OrderService(UOW uow)
        {
            _uow = uow;
        }

        public void AddOrderAsync(CreateOrderDto createOrderDto)
        {
            if(createOrderDto == null)
                throw new ArgumentNullException(nameof(createOrderDto),"the data of the order can not be lefted as empty");
            
            _uow.OrderRepository.AddAsync(new Order { 
                UserId = createOrderDto.UserId,
                OrderDate = createOrderDto.OrderDate,
                TotalAmount = createOrderDto.TotalAmount,
                Status = createOrderDto.Status                
            });
        }

        public void DeleteOrderAsync(int produtId)
        {
            if(produtId == null || produtId == 0)
                throw new ArgumentNullException(nameof(produtId), "invalid data entried");
            
            Order selectedOrder = _uow.OrderRepository.GetByIdAsync(produtId);
            if(selectedOrder == null)
                throw new ArgumentNullException(nameof(selectedOrder),"there is no data exists for that id");
            
            _uow.OrderRepository.DeleteAsync(produtId);
        }

        public List<OrderDto> GetAllOrdersAsync()
        {
            List<Order> orders = _uow.OrderRepository.GetAllAsync();
            if(orders == null || orders.Count == 0)
                throw new ArgumentNullException(nameof(orders), "there is no data exists in the database");

            List<OrderDto> orderDtos = new List<OrderDto>();
            
            foreach(var order in orders)
            {
                orderDtos.Add(new OrderDto
                {
                    OrderId = order.OrderId,
                    UserId = order.UserId,
                    OrderDate = order.OrderDate,
                    TotalAmount = order.TotalAmount,
                    Status = order.Status
                });
            }
            return orderDtos;
        }

        public List<OrderDto> GetAllOrdersByUserId(int userId)
        {
            if(userId == null || userId == 0)
                throw new ArgumentNullException(nameof(userId), "invalid data entried");
            
            User user = _uow.UserRepository.GetByIdAsync(userId);
            if(user == null)
                throw new ArgumentNullException(nameof(user),"there is no data exists for that id");
           
            List<Order> orders = _uow.OrderRepository.GetAllAsync();
            if(orders == null || orders.Count == 0)
                throw new ArgumentNullException(nameof(orders), "there is no data exists in the database");

            List<OrderDto> orderDtos = new List<OrderDto>();
            
            foreach(var order in orders)
            {
                if(order.UserId == userId)
                {
                    orderDtos.Add(new OrderDto
                    {
                        OrderId = order.OrderId,
                        UserId = order.UserId,
                        OrderDate = order.OrderDate,
                        TotalAmount = order.TotalAmount,
                        Status = order.Status
                    });
                }
            }
            return orderDtos;

        }

        public List<OrderDto> GetAllOrdersByUserName(string userName)
        {
            if (userName == null)
                throw new ArgumentNullException(nameof(userName), "invalid data entried");
           
            List<User> users = _uow.UserRepository.GetAllAsync();
            if(users == null || users.Count == 0)
                throw new ArgumentNullException(nameof(users),"there is no data exists in the database");
            User user = users.FirstOrDefault(u => u.Username == userName);
            
            List<Order> orders = _uow.OrderRepository.GetAllAsync();
            if (orders == null || orders.Count == 0)
                throw new ArgumentNullException(nameof(orders), "there is no data exists in the database");

            List<OrderDto> orderDtos = new List<OrderDto>();
            
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
                        Status = order.Status
                    });
                }
            }
            return orderDtos;
        }

        public OrderDto GetOrderByIdAsync(int orderId)
        {
            if(orderId == null || orderId == 0)
                throw new ArgumentNullException(nameof(orderId), "invalid data entried");
            
            Order selectedOrder = _uow.OrderRepository.GetByIdAsync(orderId);
            if(selectedOrder == null)
                throw new ArgumentNullException(nameof(selectedOrder),"there is no data exists for that id");
           
            return new OrderDto{
                OrderId = selectedOrder.OrderId,
                UserId = selectedOrder.UserId,
                OrderDate = selectedOrder.OrderDate,
                TotalAmount = selectedOrder.TotalAmount,
                Status = selectedOrder.Status
            };
        }

        public void UpdateOrderAsync(OrderDto orderDto)
        {
            if(orderDto == null)
                throw new ArgumentNullException(nameof(orderDto), "invalid data entried");
            
            Order selectedOrder = _uow.OrderRepository.GetByIdAsync(orderDto.OrderId);
            if(selectedOrder == null)
               throw new ArgumentNullException(nameof(selectedOrder),"there is no orders exists for that id");
            
            _uow.OrderRepository.UpdateAsync(new Order
            {
               OrderDate = orderDto.OrderDate,
               TotalAmount = orderDto.TotalAmount,
               Status = orderDto.Status
            });
        }
    }
}
