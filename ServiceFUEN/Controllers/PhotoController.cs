﻿using Azure.Core;
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

        [Route("api/Photo/CollectionPhoto")]
        [HttpGet]
        public IEnumerable<ShowPhotoDTO> CollectionPhoto(int memberId)
        {
            return _dbContext.OthersCollections
                .Include(x => x.Photo)
                .Where(x => x.MemberId == memberId)
                .Select(x => new ShowPhotoDTO()
                {
                    Id = x.Photo.Id,
                    Source = x.Photo.Source,
                    Title = x.Photo.Title,
                    IsCollection = true,
                    Camrea = x.Photo.Camera
                });
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
