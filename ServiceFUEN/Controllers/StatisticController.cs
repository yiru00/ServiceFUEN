using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ServiceFUEN.Models.DTOs;
using ServiceFUEN.Models.EFModels;
using System.Security.Claims;
using static System.Net.WebRequestMethods;

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
		public TopPhotoResultDTO TopViews(CommunityMPIdDTO member)
		{
			//某人瀏覽次數最高的照片
			var photos = _dbContext.Views.Include(v => v.Photo)
				.Where(v => v.Photo.Author == member.Id)
				.GroupBy(v => new { v.PhotoId, v.Photo.Source,v.Photo.Title}).Select(v => new PhotoViewDTO
				{
					PhotoSrc = v.Key.Source,
					PhotoId = v.Key.PhotoId,
					PhotoTitle = v.Key.Title,
					PhotoViews = v.Count()
				}).OrderByDescending(v=>v.PhotoViews);

			TopPhotoResultDTO result = new TopPhotoResultDTO();

			List<TopPhotoSrc> src = new List<TopPhotoSrc>();

			foreach(var photo in photos)
			{
				TopPhotoSrc photoSrc = new TopPhotoSrc() 
				{ src = $"https://localhost:7259/Images/{photo.PhotoSrc}", height = 70, width = 75 };
				result.PhotoId.Add(photo.PhotoId);
				result.PhotoTitle.Add(photo.PhotoTitle);
				src.Add(photoSrc);
				result.PhotoViews.Add(photo.PhotoViews);
			}

			result.PhotoSrc = src;

			return result;
		}

		[Route("api/Statistic/DateViews")]
		[HttpPost]
		public DateResultDTO DateViews(CommunityMPIdDTO member)
		{
			//某天的總相片瀏覽次數
			var photos = _dbContext.Views.Include(v => v.Photo)
				.Where(v => v.Photo.Author == member.Id && v.ViewDate >= DateTime.Today.AddDays(-7))
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
		public CameraResultDTO CameraCount(CommunityMPIdDTO member)
		{
			// 某人的相機使用率
			var cameras = _dbContext.Photos.Where(v => v.Author == member.Id)
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

		[Route("api/Statistic/AddView")]
		[HttpPut]
		public string AddView(CommunityMPIdDTO member)
		{
			// 得登入的ID
			var claim = User.Claims.ToArray();
			var claimData = claim.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);

			if (claimData == null)
			{
				return "未登入不能算瀏覽次數";
			}

			var loginMemberId = int.Parse(claimData.Value.ToString());

			var view = _dbContext.Views.FirstOrDefault(v => v.MemberId == loginMemberId && v.PhotoId == member.PhotoId && v.ViewDate.Date == DateTime.Today);

			if (view == null)
			{
				View entity = new View()
				{
					MemberId = loginMemberId,
					PhotoId = member.PhotoId
				};

				_dbContext.Views.Add(entity);
				_dbContext.SaveChanges();

				return "瀏覽次數增加成功";
			}

			return "瀏覽次數已經存在DB";
		}
	}
}
