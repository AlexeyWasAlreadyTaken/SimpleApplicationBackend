using SimpleApplicationBack.Models;

namespace SimpleApplicationBack.Interfaces
{
    public interface IOrderRepository
    {
        public ICollection<Order> GetOrders();
        public ICollection<OrderProduct> GetOrdersProducts(Guid orderId);
        public Order GetOrder(Guid id);
        public Order GetOrder(int number);
        public bool OrderExists(Guid id);
        public bool CreateOrder(Order order);
        public bool UpdateOrder(Order order);
        public bool DeleteOrder(Order order);
        public bool Save();
    }
}
