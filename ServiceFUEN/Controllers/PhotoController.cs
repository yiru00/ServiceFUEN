using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ServiceFUEN.Models.DTOs;
using ServiceFUEN.Models.EFModels;
using System.IO.Pipelines;

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
        [HttpGet]
        public string Create(PhotoDTO dto)
        {
            Photo photo = dto.DtoToEntity();
            _dbContext.Photos.Add(photo);
            _dbContext.SaveChanges();
            return "成功";
        }

        [Route("api/Photo/Edit")]
        [HttpPut]
        public string Edit(PhotoDTO dto)
        {
            Photo photo = _dbContext.Photos.SingleOrDefault(p => p.Id == dto.Id);

            photo.Source = dto.Source;
            photo.Title = dto.Title;
            photo.Description = dto.Description;
            photo.Author = dto.Author;
            photo.ISO = dto.ISO;
            photo.Pixel = dto.Pixel;
            photo.Aperture = dto.Aperture;
            photo.Shutter = dto.Shutter;
            photo.Camera = dto.Camera;
            photo.Negative = dto.Negative;
            photo.Location = dto.Location;
            photo.ShootingTime = dto.ShootingTime;
            photo.UploadTime = dto.UploadTime;
            photo.IsCollection = dto.IsCollection;
            photo.CollectionTime = dto.CollectionTime;

            _dbContext.SaveChanges();
            return "成功";
        }

        [Route("api/Photo/Delete")]
        [HttpDelete]
        public string Delete(int id)
        {
            Photo photo = _dbContext.Photos.FirstOrDefault(x => x.Id == id);
            _dbContext.Photos.Remove(photo);
            _dbContext.SaveChanges();
            return "成功";
        }

        [Route("api/Photo/Collect")]
        [HttpPut]
        public string Collect(int id)
        {
            Photo photo = _dbContext.Photos.FirstOrDefault(x => x.Id == id);

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

            _dbContext.SaveChanges();

            return "成功";
        }
    }
}
