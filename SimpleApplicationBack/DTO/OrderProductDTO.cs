namespace SimpleApplicationBack.DTO
{
    public class OrderProductDTO
    {
        public Guid Id { get; set; }
        public Guid? ProductId { get; set; }
        public ProductDTO? Product { get; set; }
    }
}
