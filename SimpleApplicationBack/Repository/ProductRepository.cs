using SimpleApplicationBack.Data;
using SimpleApplicationBack.Interfaces;
using SimpleApplicationBack.Models;

namespace SimpleApplicationBack.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly SimpleApplicationDataBaseContext _context;
        public ProductRepository(SimpleApplicationDataBaseContext context)
        {
            _context = context;
        }

        public ICollection<Product> GetProducts() 
        {
            return _context.Products.OrderBy(p => p.Id).ToList();
        }

        public Product GetProduct(Guid id)
        {
            return _context.Products.Where(p => p.Id == id).FirstOrDefault();
        }

        public Product GetProduct(string name)
        {
            return _context.Products.Where(p => p.Name == name).FirstOrDefault();
        }

        public bool ProductExists(Guid id)
        {
            return _context.Products.Any(p => p.Id == id);
        }

        public bool ColorExists(Guid? id)
        {
            return _context.Colors.Any(c => c.Id == id);
        }
        public bool TypeExists(Guid? id)
        {
            return _context.Types.Any(t => t.Id == id);
        }
        public string GetProductColor(Guid? id) 
        {
            return _context.Colors.Where(c => c.Id == id).FirstOrDefault().Code.ToString();
        }
        public string GetProductType(Guid? id)
        {
            return _context.Types.Where(t => t.Id == id).FirstOrDefault().Name.ToString();
        }
        public ICollection<Color> GetColors() 
        {
            return _context.Colors.OrderBy(c => c.Id).ToList();
        }
        public ICollection<Models.Type> GetTypes()
        {
            return _context.Types.OrderBy(t => t.Id).ToList();
        }

        public bool CreateProduct(Product product)
        {
            _context.Add(product);
            return Save();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0? true: false;
        }

        public bool UpdateProduct(Product product)
        {
            _context.Update(product);
            return Save();
        }
        public bool DeleteProduct(Product Product) 
        {
            _context.Remove(Product);
            return Save();
        }
    }
}
