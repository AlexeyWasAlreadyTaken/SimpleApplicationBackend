using Microsoft.EntityFrameworkCore;
using SimpleApplicationBack.Data;
using SimpleApplicationBack.Interfaces;
using SimpleApplicationBack.Models;

namespace SimpleApplicationBack.Repository
{
    public class OrderRepository : IOrderRepository
    {
        private readonly SimpleApplicationDataBaseContext _context;
        public OrderRepository(SimpleApplicationDataBaseContext context)
        {
            _context = context;
        }
        public ICollection<Order> GetOrders()
        {

            return _context.Orders.Include(o => o.OrderProducts).ThenInclude(op => op.Product).ToList();
        }
        public ICollection<OrderProduct> GetOrdersProducts(Guid orderId)
        {
            return _context.OrderProducts.Where(o => o.OrderId == orderId).OrderBy(p => p.ProductId).ToList();
        }
        public Order GetOrder(Guid id)
        {
            return _context.Orders.Where(p => p.Id == id).FirstOrDefault();
        }
        public Order GetOrder(int number)
        {
            return _context.Orders.Where(p => p.Number == number).FirstOrDefault();
        }
        public bool OrderExists(Guid id)
        {
            return _context.Orders.Any(p => p.Id == id);
        }
        public bool CreateOrder(Order order)
        {
            _context.Add(order);
            return Save();
        }
        public bool UpdateOrder(Order order)
        {
            _context.Update(order);
            return Save();
        }
        public bool DeleteOrder(Order order)
        {
            _context.Remove(order);
            return Save();
        }
        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

    }
}
