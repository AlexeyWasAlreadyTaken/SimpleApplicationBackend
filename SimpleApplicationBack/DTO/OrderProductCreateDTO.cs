namespace SimpleApplicationBack.DTO
{
    public class OrderProductCreateDTO
    {
        public Guid Id { get; set; }
        public Guid ProductId { get; set; }
        public Guid OrderId { get; set; }
    }
}
