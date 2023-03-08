using System;
using System.Collections.Generic;

namespace ServiceFUEN.Models.EFModels;

public partial class Coupon
{
    public int Id { get; set; }

    public string Code { get; set; } = null!;

    public string Name { get; set; } = null!;

    public decimal Discount { get; set; }

    public int LeastCost { get; set; }

    public int Count { get; set; }

    public virtual ICollection<OrderDetail> OrderDetails { get; } = new List<OrderDetail>();

    public virtual ICollection<Member> Members { get; } = new List<Member>();
}
