﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace ServiceFUEN.Models.EFModels
{
    public partial class OrderDetail
    {
        public OrderDetail()
        {
            OrderItems = new HashSet<OrderItem>();
        }

        public int Id { get; set; }
        public int MemberId { get; set; }
        public DateTime OrderDate { get; set; }
        public string Address { get; set; }
        public int State { get; set; }
        public int Total { get; set; }
        public string PaymentId { get; set; }
        public int? UsedCoupon { get; set; }

        public virtual Member Member { get; set; }
        public virtual Coupon UsedCouponNavigation { get; set; }
        public virtual ICollection<OrderItem> OrderItems { get; set; }
    }
}