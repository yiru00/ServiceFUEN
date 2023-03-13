using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ServiceFUEN.Models.DTOs;
using ServiceFUEN.Models.EFModels;
using System.Collections.Generic;

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
			//改memberId=1
			var album = _dbContext.AlbumItems
				.Include(a => a.Photo)
				.ThenInclude(a => a.AuthorNavigation)
				.Include(a => a.Album)
				.OrderByDescending(a => a.AddTime)
				.Where(a => a.AlbumId == albumId).Select(a => new AlbumPhotoDTO
				{
					Id = a.Photo.Id,
					Source = a.Photo.Source,
					Title = a.Photo.Title,
					Camera = a.Photo.Camera,
					Author = a.Photo.AuthorNavigation.NickName,
					AuthorId = a.Photo.AuthorNavigation.Id,
					AuthorPhotoSticker = a.Photo.AuthorNavigation.PhotoSticker,
					IsCollection = _dbContext.Photos
					.FirstOrDefault(x => x.Id == a.PhotoId)
					.OthersCollections.Any(o => o.MemberId == 1),
					AlbumId = a.AlbumId,
					AlbumName = a.Album.Name,
				});

			return album;
		}

		[HttpPost]
		[Route("api/Album/CreateAlbum")]
		public int CreateAlbum(CreateAlbumDTO albumDTO)
		{
			//創建相簿，傳入相簿名稱、封面照片、陣列PhotoID
			// 回傳num 會是 n+1= add相簿1筆，相片n筆
			int num = 0;
			try
			{
				Album album = new Album();
				album.MemberId = albumDTO.MemberId;
				album.Name = albumDTO.AlbumName;
				album.CoverImage = _dbContext.Photos.FirstOrDefault(x => x.Id == albumDTO.PhotoId[0]).Source;

				List<AlbumItem> albumItem = new List<AlbumItem>();
				foreach(int item in albumDTO.PhotoId)
				{
					AlbumItem albumPhotos = new AlbumItem { PhotoId = item };
					albumItem.Add(albumPhotos);
				}
				album.AlbumItems = albumItem;

				_dbContext.Albums.Add(album);
				num = _dbContext.SaveChanges();
			}
			catch{ num = 0; }

			return num;
		}

		[HttpPut]
		[Route("api/Album/EditAlbum")]
		public int EditAlbum(EditAlbumDTO albumDTO)
		{
			int num = 0;

			try { 

				var album = _dbContext.Albums.Include(a => a.AlbumItems).FirstOrDefault(a => a.Id == albumDTO.AlbumId);
				if (album == null) return num;

				album.Name = albumDTO.AlbumName;
				album.CoverImage = _dbContext.Photos.FirstOrDefault(x => x.Id == albumDTO.PhotoId[0]).Source;

				List<AlbumItem> albumItem = new List<AlbumItem>();
				foreach (var item in albumDTO.PhotoId)
				{
					AlbumItem albumPhotos = new AlbumItem { PhotoId = item };
					albumItem.Add(albumPhotos);
				}

				album.AlbumItems = albumItem;

				_dbContext.Albums.Update(album);
				num = _dbContext.SaveChanges();
			}
			catch { num = 0; }
			
			return num;
		}

		[HttpDelete]
		[Route("api/Album/DeleteAlbumPhoto")]
		public void DeleteAlbumPhoto(int PhotoId,int AlbumId)
		{
			var albumPhoto = _dbContext.AlbumItems
				.FirstOrDefault(x => x.AlbumId == AlbumId && x.PhotoId == PhotoId);
			_dbContext.Remove(albumPhoto);
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
