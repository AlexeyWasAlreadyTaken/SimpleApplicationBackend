namespace SimpleApplicationBack.DTO
{
    public class OrderCreateDTO
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? Phone { get; set; }
        public string? Comment { get; set; }
        public int? Number { get; set; }
        public DateTime? Date { get; set; }

        public List<OrderProductCreateDTO> OrderProducts { get; set; }
    }
}
