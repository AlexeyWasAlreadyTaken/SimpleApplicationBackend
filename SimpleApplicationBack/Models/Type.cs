using System;
using System.Collections.Generic;

namespace SimpleApplicationBack.Models;

public partial class Type
{
    public Guid Id { get; set; }

    public string? Name { get; set; }

    public int? Code { get; set; }

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}
