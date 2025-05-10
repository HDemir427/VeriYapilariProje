using OrderManagementSystem.Data.Entity;
using OrderManagementSystem.Data.Dto;
using OrderManagementSystem.Utilities;

namespace OrderManagementSystem.Services
{
    public class OrderService : IOrderService
    {
        private readonly CustomQueue<Order> _orderQueue;
        private readonly CustomHashTable<int, Order> _orders;

        public OrderService(CustomQueue<Order> orderQueue, CustomHashTable<int, Order> orders)
        {
            _orderQueue = orderQueue;
            _orders = orders;
        }

        public Order CreateOrder(OrderDto orderDto)
        {
            var order = new Order
            {
                UserId = orderDto.UserId,
                TotalAmount = orderDto.OrderItems.Sum(item => item.UnitPrice * item.Quantity),
                Status = "Pending",
                OrderDate = DateTime.UtcNow,
                OrderItems = orderDto.OrderItems
            };

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