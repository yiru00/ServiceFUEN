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
	public class StatisticController : ControllerBase
	{
		private readonly ProjectFUENContext _dbContext;
		public StatisticController(ProjectFUENContext dbContext)
		{ 
			_dbContext= dbContext;
		}

		[Route("api/Statistic/TopViews")]
		[HttpPost]
		public IEnumerable<PhotoViewDTO> TopViews(int memberId)
		{
			//瀏覽次數最高的照片
			var photos = _dbContext.Views.Include(v => v.Photo)
				.Where(v => v.Photo.Author == memberId)
				.GroupBy(v => new { v.PhotoId, v.Photo.Source}).Select(v => new PhotoViewDTO
				{
					PhotoSrc = v.Key.Source,
					PhotoId = v.Key.PhotoId,
					PhotoViews = v.Count()
				}).OrderByDescending(v=>v.PhotoViews);

			return photos;
		}

		[Route("api/Statistic/DateViews")]
		[HttpPost]
		public DateResultDTO DateViews(int memberId)
		{
			//某天的總相片瀏覽次數
			var photos = _dbContext.Views.Include(v => v.Photo)
				.Where(v => v.Photo.Author == memberId && v.ViewDate >= DateTime.Today.AddDays(-7))
				.OrderByDescending(v => v.ViewDate)
				.GroupBy(v => v.ViewDate).Select(v => new DateViewDTO
				{
					Date = v.Key.Date.ToString("yyyy-MM-dd"),
					DateViews = v.Count()
				}).ToArray();

			DateResultDTO result = new DateResultDTO();

			int j = 0;
			int count = photos.Count();
			for(int i = -7; i < 1; i++)
			{
				result.Date.Add(DateTime.Today.AddDays(i).ToString("yyyy-MM-dd"));

				if (j < count && photos[j].Date == DateTime.Today.AddDays(i).ToString("yyyy-MM-dd"))
				{
					result.DateViews.Add(photos[j].DateViews);
					j++;
				}
				else { 
					result.DateViews.Add(0);
				};
			}

			return result;
		}

		[Route("api/Statistic/CameraCount")]
		[HttpPost]
		public CameraResultDTO CameraCount(int memberId)
		{
			// 某人的相機使用率
			var cameras = _dbContext.Photos.Where(v => v.Author == memberId)
				.GroupBy(v => v.Camera).Select(v => new CameraCountDTO
				{
					CameraCategory = v.Key,
					CameraCount = v.Count()
				});

			CameraResultDTO result = new CameraResultDTO();
			foreach(var camera in cameras)
			{
				if (camera.CameraCategory != null)
				{
					result.CameraCategory.Add(camera.CameraCategory);
					result.CameraCount.Add(camera.CameraCount);
				}
			}

			return result;
		}
	}
}
