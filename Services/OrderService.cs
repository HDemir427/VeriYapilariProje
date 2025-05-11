using AutoMapper;
using OrderManagementSystem.Data.Entity;
using OrderManagementSystem.Dto;
using OrderManagementSystem.Utilities;

namespace OrderManagementSystem.Services
{
    public class OrderService : IOrderService
    {
        private readonly IMapper _mapper;
        private readonly CustomHashTable<int, Order> _orders;
        private readonly CustomQueue<Order> _orderQueue;

        public OrderService(
            IMapper mapper,
            CustomHashTable<int, Order> orders,
            CustomQueue<Order> orderQueue)
        {
            _mapper = mapper;
            _orders = orders;
            _orderQueue = orderQueue;
        }

        public Order CreateOrder(OrderDto orderDto)
        {

            var order = _mapper.Map<Order>(orderDto);

            order.Status = "Pending";
            order.OrderDate = DateTime.UtcNow;

            _orders.Add(order.OrderId, order);
            _orderQueue.Enqueue(order);

            return order;
        }

        public Order GetOrderById(int orderId) => _orders.Get(orderId);

        public void UpdateOrderStatus(int orderId, string status)
        {
            var order = _orders.Get(orderId);
            order.Status = status;
            _orders.Add(orderId, order); 
        }

        public List<Order> GetOrdersByUser(int userId)
            => _orders.GetAll().Where(o => o.UserId == userId).ToList();
    }
}
