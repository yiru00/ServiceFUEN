using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ServiceFUEN.Models.DTOs;
using ServiceFUEN.Models.EFModels;
using System.Reflection.Metadata.Ecma335;

namespace ServiceFUEN.Controllers
{
	[EnableCors("AllowAny")]
	[ApiController]
	public class PhotoController : Controller
	{
		private readonly ProjectFUENContext _dbContext;
		public PhotoController(ProjectFUENContext dbContext) {
			_dbContext = dbContext;
		}

		[HttpGet]
		[Route("api/Photo/GetMemberPhotos")]
		public IEnumerable<PhotoSrcDTO> GetMemberPhotos(int memberId) {
			//撈某人已公開(非典藏)的照片
			var photos = _dbContext.Photos.Include(p => p.AuthorNavigation)
				.Where(p => p.AuthorNavigation.Id == memberId && p.IsCollection == false)
				.Select(p => new PhotoSrcDTO
				{
					PhotoId = p.Id,
					PhotoSrc = p.Source,
				});
			return photos;
		}

		[HttpGet]
		[Route("api/Photo/CommunityPage")]
		public IEnumerable<PhotoSrcDTO> CommunityPage(int tagId)
		{
			//社群主頁-取得某tag的照片們
			//一個tag對應多張照片，Where(撈多筆).First(撈一筆)所以可以用.Photos，
			var photos = _dbContext.Tags.Include(t => t.Photos)
				.FirstOrDefault(t => t.Id == tagId).Photos
				.Select(t => new PhotoSrcDTO
				{
					PhotoId = t.Id,
					PhotoSrc = t.Source,
				});

			return photos;
		}

	}
}
