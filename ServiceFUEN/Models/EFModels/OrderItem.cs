using System;
using System.Collections.Generic;

namespace ServiceFUEN.Models.EFModels;

public partial class OrderItem
{
    public int OrderId { get; set; }

    public int ProductId { get; set; }

    public string ProductName { get; set; } = null!;

    public int ProductPrice { get; set; }

    public int ProductNumber { get; set; }

    public virtual OrderDetail Order { get; set; } = null!;

    public virtual Product Product { get; set; } = null!;
}
