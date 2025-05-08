using System.Net.WebSockets;
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
            return _context.Orders.Where(p => p.Id == id).Include(o => o.OrderProducts).ThenInclude(op => op.Product).FirstOrDefault();
        }
        public Order GetOrder(int number)
        {
            return _context.Orders.Where(p => p.Number == number).Include(o => o.OrderProducts).ThenInclude(op => op.Product).FirstOrDefault();
        }
        public bool OrderExists(Guid id)
        {
            return _context.Orders.Any(p => p.Id == id);
        }
        public bool CreateOrder(Order order)
        {
            order.Id = Guid.NewGuid();
            List<OrderProduct> opl = new List<OrderProduct>();

            foreach (var op in order.OrderProducts)
            {
                op.Id = Guid.NewGuid();
                op.OrderId = order.Id;
                opl.Add(op);
            }

            //_context.Add(opl);
            //Save();
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
        public Order GetOrderWithOrderProducts(Guid orderId)
        {
            return _context.Orders.Include(o => o.OrderProducts).FirstOrDefault(o => o.Id == orderId);
        }
        public bool DeleteOrderProduct(OrderProduct op)
        {
            _context.OrderProducts.Remove(op);
            return Save();
        }

    }
}
