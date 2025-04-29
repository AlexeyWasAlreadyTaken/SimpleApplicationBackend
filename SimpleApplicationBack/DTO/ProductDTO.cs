using SimpleApplicationBack.Models;

namespace SimpleApplicationBack.DTO
{
    public class ProductDTO
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? FullDescription { get; set; }
        public int? PictureNumber { get; set; }
        public string? PictureName { get; set; }
        public Guid? TypeId { get; set; }
        public Guid? ColorId { get; set; }
        public string? ColorCode { get; set; }
        public string? TypeName { get; set; }

    }
}
