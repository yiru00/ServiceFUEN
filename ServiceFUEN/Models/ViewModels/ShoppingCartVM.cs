using ServiceFUEN.Models.EFModels;

namespace ServiceFUEN.Models.ViewModels
{
	public class ShoppingCartVM
	{
		public int MemberId { get; set; }
		public int State { get; set; }
		public int Total { get; set; }
		public Adress Adress { get; set; }
		public CartProduct[] CartProducts { get; set; }
		public CouponData? CouponData { get; set; }
	}

	public class CartProduct
	{
		public int Id { get; set; }
		public int Qty { get; set; }
		public string Name { get; set; }

	}

	public class CouponData
	{
		public int? UsedCouponID { get; set; }
		public string? CouponCode { get; set; }

		public decimal? Discount { get; set; }
	}

	public class Adress
	{
		public string Name { get; set; }
		public string ZipCode { get; set; }
		public string County { get; set; }
		public string CountyName { get; set; }
		public string InputRegion { get; set; }
	}




}
