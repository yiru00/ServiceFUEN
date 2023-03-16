using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ServiceFUEN.Models.DTOs;
using ServiceFUEN.Models.EFModels;
using System.Collections.Generic;
using System.Security.Claims;

namespace ServiceFUEN.Controllers
{
	[EnableCors("AllowAny")]
	[ApiController]
	public class AlbumController : Controller
	{
		private readonly ProjectFUENContext _dbContext;
		public AlbumController(ProjectFUENContext dbContext)
		{
			_dbContext = dbContext;
		}

		[HttpGet]
		[Route("api/Album/GetAlbums")]
		public IEnumerable<AlbumDTO> GetAlbums(int memberId)
		{

			var albums = _dbContext.Albums.Where(a => a.MemberId == memberId)
				.OrderByDescending(x => x.CreatedTime)
				.Select(a => new AlbumDTO
				{
					AlbumId = a.Id,
					AlbumName = a.Name,
					CoverImg = a.CoverImage
				});

			return albums;
		}

		[HttpGet]
		[Route("api/Album/AlbumPhotos")]
		public IEnumerable<AlbumPhotoDTO> AlbumPhotos(int albumId)
		{
			//撈出某相簿的相簿名稱、封面照片及照片們

			//得登入的id
			var claim = User.Claims.ToArray();
			var claimData = claim.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
			var PhotosContext = _dbContext.AlbumItems
				.Include(a => a.Photo)
				.ThenInclude(a => a.AuthorNavigation)
				.Include(a => a.Album).Where(a => a.AlbumId == albumId).ToList();

			if (PhotosContext.Count() == 0) {
				var resultAlbum = _dbContext.Albums.FirstOrDefault(x => x.Id == albumId);

				return new List<AlbumPhotoDTO>()
				{
					new AlbumPhotoDTO
					{
						AlbumId=resultAlbum.Id,
						AlbumName=resultAlbum.Name,
					}
				};
			};

			IEnumerable<AlbumPhotoDTO> album = PhotosContext
				.OrderByDescending(a => a.AddTime)
				.Select(a => new AlbumPhotoDTO
				{
					Id = a.Photo.Id,
					Source = a.Photo.Source,
					Title = a.Photo.Title,
					Camera = a.Photo.Camera,
					Author = a.Photo.AuthorNavigation.NickName,
					AuthorId = a.Photo.AuthorNavigation.Id,
					AuthorPhotoSticker = a.Photo.AuthorNavigation.PhotoSticker,
					AlbumId = a.AlbumId,
					AlbumName = a.Album.Name,
				});

			if (claimData == null)
			{
				album = album.Select(x =>
				{
					x.IsCollection = false;
					return x;
				});
			}
			else
			{
				var loginMemberId = int.Parse(claimData.Value.ToString());

				album = album.Select(x =>
				{
					x.IsCollection = _dbContext.Photos.Include(a => a.OthersCollections).FirstOrDefault(y => y.Id == x.Id).OthersCollections.Any(o => o.MemberId == loginMemberId);
					return x;
				});
			}

			return album;
		}
		[Authorize]
		[HttpPost]
		[Route("api/Album/CreateAlbum")]
		public void CreateAlbum(CreateAlbumDTO albumDTO)
		{
			//創建相簿，傳入相簿名稱、封面照片、陣列PhotoID
			//得登入的id
			var claim = User.Claims.ToArray();
			var userId = claim.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value;
			var memberId = int.Parse(userId.ToString());

			Album album = new Album();
			album.MemberId = memberId;
			album.Name = albumDTO.AlbumName;
			int[] emptyArray = {};
			if (albumDTO.PhotoId.Length==0) album.CoverImage = "defaultAlbum.jpg";
			else{ album.CoverImage = _dbContext.Photos.FirstOrDefault(x => x.Id == albumDTO.PhotoId[0]).Source; }
			
			List<AlbumItem> albumItem = new List<AlbumItem>();
			foreach(int item in albumDTO.PhotoId)
			{
				AlbumItem albumPhotos = new AlbumItem { PhotoId = item };
				albumItem.Add(albumPhotos);
			}
			album.AlbumItems = albumItem;

			_dbContext.Albums.Add(album);
			_dbContext.SaveChanges();
		}

		[HttpPut]
		[Route("api/Album/EditAlbum")]
		public void EditAlbum(EditAlbumDTO albumDTO)
		{ 

			var album = _dbContext.Albums.Include(a => a.AlbumItems).ToList().FirstOrDefault(a => a.Id == albumDTO.AlbumId);

			album.Name = albumDTO.AlbumName;
			if (albumDTO.PhotoId.Length == 0) album.CoverImage = "defaultAlbum.jpg";
			else { album.CoverImage = _dbContext.Photos.FirstOrDefault(x => x.Id == albumDTO.PhotoId[0]).Source; }

			List<AlbumItem> albumItem = new List<AlbumItem>();
			foreach (var item in albumDTO.PhotoId)
			{
				AlbumItem albumPhotos = new AlbumItem { PhotoId = item };
				albumItem.Add(albumPhotos);
			}

			album.AlbumItems = albumItem;

			_dbContext.Albums.Update(album);
			_dbContext.SaveChanges();

		}

		[HttpDelete]
		[Route("api/Album/DeleteAlbumPhoto")]
		public void DeleteAlbumPhoto(int PhotoId,int AlbumId)
		{
			var albumPhoto = _dbContext.AlbumItems
				.Include(x=>x.Album)
				.FirstOrDefault(x => x.AlbumId == AlbumId && x.PhotoId == PhotoId);

			_dbContext.Remove(albumPhoto);


			var existPhotos = _dbContext.AlbumItems.Where(x => x.AlbumId == AlbumId).Count();

			if ( existPhotos == 1 )
			{
				albumPhoto.Album.CoverImage = "defaultAlbum.jpg";
			};

			_dbContext.SaveChanges();
		}

		[HttpDelete]
		[Route("api/Album/DeleteAlbum")]
		public void DeleteAlbum(int albumid)
		{
			var album = _dbContext.Albums.FirstOrDefault(a => a.Id == albumid);
			_dbContext.Remove(album);
			_dbContext.SaveChanges();
		}
	}
}
