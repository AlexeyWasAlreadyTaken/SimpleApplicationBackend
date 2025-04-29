using System;
using System.Collections.Generic;

namespace SimpleApplicationBack.Models;

public partial class Order
{
    public Guid Id { get; set; }

    public string? Name { get; set; }

    public string? Phone { get; set; }

    public string? Comment { get; set; }

    public int? Number { get; set; }

    public DateTime? Date { get; set; }

    public virtual ICollection<OrderProduct> OrderProducts { get; set; } = new List<OrderProduct>();
}
