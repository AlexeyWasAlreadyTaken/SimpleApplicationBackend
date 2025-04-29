using System;
using System.Collections.Generic;

namespace SimpleApplicationBack.Models;

public partial class OrderProduct
{
    public Guid Id { get; set; }

    public Guid? OrderId { get; set; }

    public Guid? ProductId { get; set; }

    public virtual Order? Order { get; set; }

    public virtual Product? Product { get; set; }
}
