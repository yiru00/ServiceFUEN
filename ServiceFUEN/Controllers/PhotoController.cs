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

            // Photo加入Album
            if (!dto.IsCollection)
            {
                foreach (int item in dto.AlbumIds)
                {
                    photo.AlbumItems.Add(new AlbumItem()
                    {
                        AlbumId = item,
                    });
                }
            }

            _dbContext.Photos.Add(photo);
            _dbContext.SaveChanges();
        }

        //[Route("api/Photo/Edit")]
        //[HttpPut]
        //public void Edit()
        //{

        //}

        //[Route("api/Photo/GetInformation")]
        //PhotoInformationDTO GetInformation(IFormFile file)
        //{

        //}

        [Route("api/Photo/GetAlbums")]
        [HttpGet]
        public IEnumerable<AlbumDTO> GetAlbums()
        {
            var albums = _dbContext.Albums.Select(a => a.EntityTODto());

            return albums;
        }

        [Route("api/Photo/GetPhoto")]
        [HttpGet]
        public ShowPhotoDTO GetPhoto(int photoId, int memberId)
        {
            // Create return DTO
            ShowPhotoDTO dto = new ShowPhotoDTO();

            // Get Photo Information
            var photo = _dbContext.Photos
                .Include(p => p.AuthorNavigation)
                .Include(p => p.Comments).ThenInclude(c => c.Member)
                .Include(p => p.Tags)
                .FirstOrDefault(p => p.Id == photoId);
            dto.Id = photo.Id;
            dto.Source = photo.Source;
            dto.Title = photo.Title;
            dto.Description = photo.Description;
            dto.ISO = photo.ISO;
            dto.Pixel = photo.Pixel;
            dto.Aperture = photo.Aperture;
            dto.Shutter = photo.Shutter;
            dto.Camera = photo.Camera;
            dto.Negative = photo.Negative;
            dto.Location = photo.Location;
            dto.ShootingTime = photo.ShootingTime;
            dto.UploadTime = photo.UploadTime;

            // Get Author Information
            MemberDTO memberDTO = new MemberDTO();
            memberDTO.Id = photo.Author;
            memberDTO.Name = photo.AuthorNavigation.NickName;
            memberDTO.Source = photo.AuthorNavigation.PhotoSticker;
            dto.Author = memberDTO;

            // Determine Photo Collection
            if (photo.Author == memberId) dto.IsCollection = photo.IsCollection;
            else
            {
                var collection = _dbContext.OthersCollections.FirstOrDefault(p => p.MemberId == memberId && p.PhotoId == photoId);
                if (collection != null) dto.IsCollection = true;
                else dto.IsCollection = false;
            }

            // Comments
            dto.Comments = photo.Comments.Select(c => new CommentDTO()
            {
                Id = c.Id,
                Content = c.Content,
                CommentTime = c.CommentTime,
                Author = new MemberDTO()
                {
                    Id = c.Member.Id,
                    Source = c.Member.PhotoSticker,
                    Name = c.Member.NickName
                }
            });

            // Tags
            dto.Tags = photo.Tags.Select(t => new TagDTO()
            {
                Id = t.Id,
                Name = t.Name,
            });

            // Views
            var views = _dbContext.Views.Where(v => v.PhotoId == photoId).ToArray();
            dto.Views = views.Length;

            return dto;
        }

        [Route("api/Photo/Collect")]
        [HttpPut]
        public void Collect(int photoId, int memberId)
        {
            var photo = _dbContext.Photos.FirstOrDefault(p => p.Id == photoId);

            // 判斷是否是典藏
            if (photo.Author == memberId)
            {
                if (photo.IsCollection)
                {
                    photo.IsCollection = false;
                    photo.CollectionTime = null;
                }
                else
                {
                    photo.IsCollection = true;
                    photo.CollectionTime = DateTime.Now;
                }

            }
            else
            {
                var collection = _dbContext.OthersCollections.FirstOrDefault(p => p.MemberId == memberId && p.PhotoId == photoId);
                if (collection != null) _dbContext.OthersCollections.Remove(collection);
                else
                {
                    OthersCollection otherCollection = new OthersCollection()
                    {
                        MemberId = memberId,
                        PhotoId = photoId,
                    };
                    _dbContext.OthersCollections.Add(otherCollection);
                }
            }
            _dbContext.SaveChanges();
        }

        [Route("api/Photo/AddView")]
        [HttpPut]
        public string AddView(int photoId, int memberId)
        {
            var view = _dbContext.Views.FirstOrDefault(v => v.MemberId == memberId && v.PhotoId == photoId && v.ViewDate.Date == DateTime.Today);

            if (view == null)
            {
                View entity = new View()
                {
                    MemberId = memberId,
                    PhotoId = photoId
                };
                _dbContext.Views.Add(entity);
                _dbContext.SaveChanges();

                return "增加成功";
            }

            return "已經存在DB";
        }
    }
}
