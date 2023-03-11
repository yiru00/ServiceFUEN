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

		//[Route("api/Collection/OwnCollections")]
		//[HttpPost]
		//public IEnumerable<PhotoSrcDTO> OwnCollections([FromBody]int memberId)
		//{
		//	//[FromBody]data直接寫 1 2 3 不用任何格式 contentType一樣要application/json
		//	//查看自己收藏自己的照片
		//	var ownCollections = _dbContext.Photos
		//		.Where(p => p.Author == memberId && p.IsCollection == true)
		//		.OrderByDescending(p => p.CollectionTime)
		//		.Select(p => new PhotoSrcDTO
		//		{
		//			PhotoId = p.Id,
		//			PhotoSrc = p.Source
		//		});

		//	return ownCollections;
		//}

		[Route("api/Collection/OthersCollection")]
		[HttpPost]
		public IEnumerable<PhotoSrcDTO> OthersCollection([FromBody]int memberId)
		{
			//查看自己珍藏他人的照片
			var othersCollection = _dbContext.OthersCollections
				.Where(c => c.MemberId == memberId)
				.Include(c => c.Photo)
				.OrderByDescending(c => c.AddTime)
				.Select(c => new PhotoSrcDTO
				{
					PhotoId = c.Photo.Id,
					PhotoSrc = c.Photo.Source
				});

			return othersCollection;
		}

		//[Route("api/Collection/Collect")]
		//[HttpPut]
		//public void Collect(int photoId, int memberId)
		//{
		//	var photo = _dbContext.Photos.FirstOrDefault(p => p.Id == photoId);

		//	// 判斷是否是典藏
		//	if (photo.Author == memberId)
		//	{
		//		if (photo.IsCollection)
		//		{
		//			photo.IsCollection = false;
		//			photo.CollectionTime = null;
		//		}
		//		else
		//		{
		//			photo.IsCollection = true;
		//			photo.CollectionTime = DateTime.Now;
		//		}

		//	}
		//	else
		//	{
		//		var collection = _dbContext.OthersCollections.FirstOrDefault(p => p.MemberId == memberId && p.PhotoId == photoId);
		//		if (collection != null) _dbContext.OthersCollections.Remove(collection);
		//		else
		//		{
		//			OthersCollection otherCollection = new OthersCollection()
		//			{
		//				MemberId = memberId,
		//				PhotoId = photoId,
		//			};
		//			_dbContext.OthersCollections.Add(otherCollection);
		//		}
		//	}
		//	_dbContext.SaveChanges();
		//}
	}

	
}
