using System;
using System.Collections.Generic;

namespace SimpleApplicationBack.Models;

public partial class Product
{
    public Guid Id { get; set; }

    public string? Name { get; set; }

    public string? Description { get; set; }

    public string? FullDescription { get; set; }

    public Guid? TypeId { get; set; }

    public Guid? ColorId { get; set; }

    public int? PictureNumber { get; set; }

    public string? PictureName { get; set; }

    public virtual Color? Color { get; set; }

    public virtual ICollection<OrderProduct> OrderProducts { get; set; } = new List<OrderProduct>();

    public virtual Type? Type { get; set; }
}
