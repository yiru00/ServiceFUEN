using Azure.Core;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ServiceFUEN.Models.DTOs;
using ServiceFUEN.Models.EFModels;
using System.IO.Pipelines;
using System.Reflection.Metadata.Ecma335;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace ServiceFUEN.Controllers
{
    [EnableCors("AllowAny")]
    [ApiController]
    public class PhotoController : Controller
    {
        private readonly ProjectFUENContext _dbContext;
        public PhotoController(ProjectFUENContext dbContext)
        {
            _dbContext = dbContext;
        }

		[Authorize]
		[Route("api/Photo/Create")]
        [HttpPost]
        public void Create([FromForm] CreatePhotoDTO dto)
        {
			//得登入的id
			var claim = User.Claims.ToArray();
			var userId = claim.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value;
			var memberId = int.Parse(userId.ToString());

			// 將Photo儲存進Project的Images資料夾中
			string path = System.Environment.CurrentDirectory + "/wwwroot/Images/";
            string extension = Path.GetExtension(dto.File.FileName);
            string fileName = Guid.NewGuid().ToString("N");
            string fullName = fileName + extension;
            string fullPath = Path.Combine(path, fullName);
            using (var stream = System.IO.File.Create(fullPath))
            {
                dto.File.CopyTo(stream);
            }

            // Photo加入DB
            var photo = dto.DtoToEntity();
            photo.Author = memberId;
			photo.Source = fullName;

            _dbContext.Photos.Add(photo);
            _dbContext.SaveChanges();
        }

        [Route("api/Photo/GetProfile")]
        [HttpGet]
        public CommunityMemberDTO GetProfile(int memberId)
        {
            // 取得某人的個人檔案
            var member = _dbContext.Members.FirstOrDefault(x => x.Id == memberId);
			var profile = new CommunityMemberDTO() 
			{ 
				Id = member.Id,
				Name = member.NickName,
				Source = member.PhotoSticker,
				About = member.About
			};
            return profile;
        }

		[Route("api/Photo/AllPhotos")]
		[HttpGet]
		public IEnumerable<ShowPhotoDTO> AllPhotos(int memberId)
		{
            //得登入的id
            var claim = User.Claims.ToArray();
            var claimData = claim.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);

            var PhotosContext = _dbContext.Photos.Include(x => x.AuthorNavigation).Include(x => x.OthersCollections).ToList();

			IEnumerable<ShowPhotoDTO> photos = PhotosContext
				.Where(x => x.Author == memberId)
				.OrderByDescending(x => x.UploadTime)
				.Select(x => new ShowPhotoDTO()
				{
					Id = x.Id,
					Source = x.Source,
					Title = x.Title,
					Camera = x.Camera,
					Author = x.AuthorNavigation.NickName,
					AuthorPhotoSticker = x.AuthorNavigation.PhotoSticker,
					AuthorId = x.AuthorNavigation.Id
				});
			if (claimData == null)
            {
                photos = photos.Select(x =>
                {
                    x.IsCollection = false;
                    return x;
                });
			}
            else
            {
                var loginMemberId = int.Parse(claimData.Value.ToString());

                photos = photos.Select(x =>
				{
                    x.IsCollection = PhotosContext.FirstOrDefault(y => y.Id == x.Id).OthersCollections.Any(o => o.MemberId == loginMemberId);
					return x;
				});
			}
			return photos;
		}

        [Authorize]
        [Route("api/Photo/CollectionPhoto")]
        [HttpPost]
        public IEnumerable<ShowPhotoDTO> CollectionPhoto(CommunityMPIdDTO member)
        {
			//得登入的id
			var claim = User.Claims.ToArray();
			var userId = claim.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value;
			var memberId = int.Parse(userId.ToString());

            if(member.Id != memberId) return null;

			return _dbContext.OthersCollections
                .Include(x => x.Photo)
                .ThenInclude(x => x.AuthorNavigation)
                .Where(x => x.MemberId == member.Id)
                .OrderByDescending(x => x.AddTime)
                .Select(x => new ShowPhotoDTO()
                {
                    Id = x.Photo.Id,
                    Source = x.Photo.Source,
                    Title = x.Photo.Title,
                    IsCollection = true,
                    Camera = x.Photo.Camera,
                    Author = x.Photo.AuthorNavigation.NickName,
                    AuthorPhotoSticker = x.Photo.AuthorNavigation.PhotoSticker,
                    AuthorId = x.Photo.AuthorNavigation.Id
                });
		}
		[Route("api/Photo/EditPhoto")]
		[HttpPatch]
		public void EditPhoto(EditPhotoDTO dto)
		{
			var photo = _dbContext.Photos.FirstOrDefault(x => x.Id == dto.PhotoId);
			if (photo != null)
			{
                photo.Title = dto.Title;
                photo.Camera = dto.Camera;
				_dbContext.SaveChanges();
			}
		}

		[Route("api/Photo/DeletePhoto")]
        [HttpDelete]
        public void DeletePhoto(int photoId)
        {
            var photo = _dbContext.Photos.FirstOrDefault(x => x.Id == photoId);
            if (photo != null)
            {
                _dbContext.Photos.Remove(photo);
                _dbContext.SaveChanges();
            }
        }
	}
}
