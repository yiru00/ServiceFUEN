using System;
using System.Collections.Generic;

namespace ServiceFUEN.Models.EFModels;

public partial class OrderDetail
{
    public int Id { get; set; }

    public int MemberId { get; set; }

    public DateTime OrderDate { get; set; }

    public string Address { get; set; } = null!;

    public int State { get; set; }

    public int Amount { get; set; }

    public string PaymentId { get; set; } = null!;

    public int? UsedCoupon { get; set; }

    public virtual Member Member { get; set; } = null!;

    public virtual ICollection<OrderItem> OrderItems { get; } = new List<OrderItem>();

    public virtual Coupon? UsedCouponNavigation { get; set; }
}
