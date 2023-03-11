using ServiceFUEN.Models.EFModels;

namespace ServiceFUEN.Models.ViewModels
{
    public class PaymentResultVM
    {
        public int ToatalBefore { get; set; }
        public int MinusAmount { get; set; }
        public OrderDetail? OrderDetail {get; set; }
    }


}
