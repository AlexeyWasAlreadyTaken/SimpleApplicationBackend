using SimpleApplicationBack.Models;

namespace SimpleApplicationBack.DTO
{
    public class OrderDTO
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? Phone { get; set; }
        public string? Comment { get; set; }
        public int? Number { get; set; }
        public DateTime? Date { get; set; }

        public List<OrderProduct> OrderProducts { get; set; }
    }
}
