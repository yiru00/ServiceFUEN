using ServiceFUEN.Models.EFModels;

namespace ServiceFUEN.Models.ViewModels
{
    public class OrderVM
    {

        public OrderDetail? orderDetail { get; set; }






        public class OrderItem
        {
            public int OrderId { get; set; }
            public int ProductId { get; set; }
            public string ProductName { get; set; }
            public int ProductPrice { get; set; }
            public int ProductNumber { get; set; }

        }

        public class OrderDetail
        {
            public int Id { get; set; }
            public int MemberId { get; set; }
            public DateTime OrderDate { get; set; }
            public string Address { get; set; }
            public int State { get; set; }
            public int Total { get; set; }
            public string PaymentId { get; set; }
            public int? UsedCoupon { get; set; }

        }

    }
}
