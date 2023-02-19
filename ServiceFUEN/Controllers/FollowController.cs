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
    public class FollowController : ControllerBase
    {
		private readonly ProjectFUENContext _dbContext;
		public FollowController(ProjectFUENContext dbContext)
		{
			_dbContext = dbContext;
		}

		// Follower Follow Following
		[Route("api/Follow/Create")]
		[HttpGet]
		public void Create(int followerId, int followingId)
		{
			FollowInfo follow = new FollowInfo()
			{
				Follower = followerId,
				Following = followingId
			};

			_dbContext.FollowInfos.Add(follow);
			_dbContext.SaveChanges();
		}

		[Route("api/Follow/Delete")]
		[HttpDelete]
		public void Delete(int followerId, int followingId)
		{
			var follow = _dbContext.FollowInfos.FirstOrDefault(f => f.Follower == followerId && f.Following == followingId);
			_dbContext.FollowInfos.Remove(follow);
			_dbContext.SaveChanges();
		}

		[Route("api/Follow/GetAllFollower")]
		[HttpGet]
		public IEnumerable<MemberDTO> GetAllFollower(int memberId)
		{
			return _dbContext.FollowInfos
					.Include(f => f.FollowerNavigation)
					.Where(f => f.Following == memberId)
					.Select(f => new MemberDTO()
					{
						Id = f.FollowerNavigation.Id,
						Name = f.FollowerNavigation.NickName,
						Source = f.FollowerNavigation.PhotoSticker
					});
		}


		[Route("api/Follow/GetAllFollowing")]
		[HttpGet]
		public IEnumerable<MemberDTO> GetAllFollowing(int memberId)
		{
			return _dbContext.FollowInfos
					.Include(f => f.FollowingNavigation)
					.Where(f => f.Follower == memberId)
					.Select(f => new MemberDTO()
					{
						Id = f.FollowingNavigation.Id,
						Name = f.FollowingNavigation.NickName,
						Source = f.FollowingNavigation.PhotoSticker
					});
		}
	}
}
