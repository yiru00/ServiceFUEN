using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ServiceFUEN.Models.DTOs;
using ServiceFUEN.Models.EFModels;
using System.IO.Pipelines;
using System.Reflection.Metadata.Ecma335;

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
        public void Create([FromForm]PhotoDTO dto)
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
                foreach(int item in dto.AlbumIds)
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
    }
}
