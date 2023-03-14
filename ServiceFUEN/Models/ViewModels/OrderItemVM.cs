using MailKit.Search;
using ServiceFUEN.Models.EFModels;

namespace ServiceFUEN.Models.ViewModels
{
    public class OrderItemVM
	{

        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public int ProductPrice { get; set; }
        public int ProductNumber { get; set; }

        public string source { get; set; }

       


}


    public static class toordervm 
    {
        public static OrderItemVM toorvm(this OrderItem souce)
        {
            return new OrderItemVM
			{
                OrderId = souce.OrderId,
                ProductId = souce.ProductId,
                ProductName = souce.ProductName,
                ProductPrice = souce.ProductPrice,
                ProductNumber = souce.ProductNumber,
                source = null,

            };
			



		}



        
    }
     
}
