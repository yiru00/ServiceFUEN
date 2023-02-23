using ServiceFUEN.Models.EFModels;

namespace ServiceFUEN.Models.ViewModels
{
    public class ShoppingCartVM
    {
        public int MemberId { get; set; }
        public int State { get; set; }
        public CartProduct[] CartProducts { get; set; }
    }

    public class CartProduct
    {
        public int Id { get; set; }
        public int Qty { get; set; }
        public string Name { get; set; }
    }
}
