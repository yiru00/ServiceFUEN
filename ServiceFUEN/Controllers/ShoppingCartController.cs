using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServiceFUEN.Models.EFModels;
using ServiceFUEN.Models.ViewModels;

namespace ServiceFUEN.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ShoppingCartController : ControllerBase
	{

		private readonly ProjectFUENContext _context;


		public ShoppingCartController(ProjectFUENContext context)
		{

			_context = context;

		}


		[Route("Qqq")]
		[HttpPost]
		public IEnumerable<ShoppingCartVM> Qqq(int Memberid)
		{
			var allproducts = _context.ShoppingCarts;

			var test =  allproducts.Select(x => new ShoppingCartVM {

				 MemberId =x.MemberId,
				 ProductId=x.ProductId,
			}).ToList();



			return test;



			



		}



	}
}
