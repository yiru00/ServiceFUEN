using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ServiceFUEN.Models.DTOs;
using ServiceFUEN.Models.EFModels;

namespace ServiceFUEN.Controllers
{
	[EnableCors("AllowAny")]
	[ApiController]
	public class CollectionController : Controller
	{
		private readonly ProjectFUENContext _dbContext;
		public CollectionController(ProjectFUENContext dbContext)
		{
			_dbContext = dbContext;
		}

		[Route("api/Collection/Collect")]
		[HttpPost]
		public void Collect(int memberId, int photoId)
		{
			var record = _dbContext.OthersCollections.FirstOrDefault(x => x.MemberId == memberId && x.PhotoId == photoId);

			// 表示table中某人尚未收藏此照片
			if (record == null)
			{
				var collections = new OthersCollection()
				{
					MemberId = memberId,
					PhotoId = photoId
				};
				_dbContext.OthersCollections.Add(collections);
			}
			// 表示某人以收藏此照片
			else
			{
				_dbContext.OthersCollections.Remove(record);
			}
			_dbContext.SaveChanges();
		}
	}
}
