using OrderManagementSystem.Data.Entity;

namespace OrderManagementSystem.Services
{
    public interface IOrderService
    {
        Order CreateOrder(OrderDto orderDto);
        Order GetOrderById(int orderId);
        void UpdateOrderStatus(int orderId, string status);
        List<Order> GetOrdersByUser(int userId);
    }
}