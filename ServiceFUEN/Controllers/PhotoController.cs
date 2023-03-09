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

        [Route("api/Photo/Create")]
        [HttpPost]
        public void Create([FromForm] CreatePhotoDTO dto)
        {
            // 將Photo儲存進Project的Images資料夾中
            string path = System.Environment.CurrentDirectory + "/Images/";
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
			// 取得某人的照片
			var photos = _dbContext.Photos.Where(x => x.Author == memberId).Select(x => new ShowPhotoDTO()
			{
				Id = x.Id,
				Source = x.Source,
				Title = x.Title,
				Camrea = x.Camera,
				IsCollection = x.OthersCollections.Any(o => o.MemberId == memberId),
			});

			return photos;
		}

		// Move to HuanYu
		//[Route("api/Photo/AddView")]
		//[HttpPut]
		//public string AddView(int photoId, int memberId)
		//{
		//    var view = _dbContext.Views.FirstOrDefault(v => v.MemberId == memberId && v.PhotoId == photoId && v.ViewDate.Date == DateTime.Today);

		//    if (view == null)
		//    {
		//        View entity = new View()
		//        {
		//            MemberId = memberId,
		//            PhotoId = photoId
		//        };
		//        _dbContext.Views.Add(entity);
		//        _dbContext.SaveChanges();

		//        return "增加成功";
		//    }

		//    return "已經存在DB";
		//}
	}
}
