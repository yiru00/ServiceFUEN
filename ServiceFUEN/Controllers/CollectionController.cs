using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ServiceFUEN.Models.DTOs;
using ServiceFUEN.Models.EFModels;
using System.Security.Claims;

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

		[Authorize]
		[Route("api/Collection/Collect")]
		[HttpPost]
		public void Collect(CommunityMPIdDTO dto)
		{
			//得登入的id
		    var claim = User.Claims.ToArray();
			var userId = claim.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value;
			var memberId = int.Parse(userId.ToString());

			var record = _dbContext.OthersCollections.FirstOrDefault(x => x.MemberId == memberId && x.PhotoId == dto.PhotoId);

			// 表示table中某人尚未收藏此照片
			if (record == null)
			{
				var collections = new OthersCollection()
				{
					MemberId = memberId,
					PhotoId = dto.PhotoId
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
