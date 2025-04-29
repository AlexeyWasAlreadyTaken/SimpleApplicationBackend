using Microsoft.EntityFrameworkCore;
using SimpleApplicationBack.Models;

namespace SimpleApplicationBack.Interfaces
{
    public interface IProductRepository
    {
        ICollection<Product> GetProducts();
        public Product GetProduct(Guid id);
        public Product GetProduct(string name);
        public ICollection<Color> GetColors();
        public ICollection<Models.Type> GetTypes();
        public bool ProductExists(Guid id);
        public bool ColorExists(Guid? id);
        public bool TypeExists(Guid? id);
        public string GetProductColor(Guid? id);
        public string GetProductType(Guid? id);
        public bool CreateProduct(Product product);
        public bool UpdateProduct(Product product);
        public bool DeleteProduct(Product product);
        public bool Save();
    }
}
