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

			var album = _dbContext.AlbumItems.Include(a => a.Photo).Include(a => a.Album)
				.Where(a => a.AlbumId == albumId).Select(a => new AlbumPhotoDTO
				{
					PhotoSrcDTO =
					new PhotoSrcDTO
					{
						PhotoId = a.Photo.Id,
						PhotoSrc = a.Photo.Source
					},
					AlbumId = a.AlbumId,
					AlbumName = a.Album.Name,
					CoverImg = a.Album.CoverImage
				});

			//var test = _dbContext.Albums.Include(a => a.AlbumItems).ThenInclude(a => a.Photo).FirstOrDefault(a => a.Id == albumId).AlbumItems
			//	.Select(a => new AlbumPhotoDTO
			//	{
			//		PhotoSrc = a.Photo.Source,
			//		PhotoId = a.Photo.Id,
			//		AlbumName = a.Album.Name,
			//		CoverImg = a.Album.CoverImage
			//	});

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
				album.CoverImage = albumDTO.CoverImg;

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

				var album = _dbContext.Albums.Include(a => a.AlbumItems).FirstOrDefault(a => a.Id == albumDTO.AlbumDTO.AlbumId);
				if (album == null) return num;

				album.Name = albumDTO.AlbumDTO.AlbumName;
				album.CoverImage = albumDTO.AlbumDTO.CoverImg;

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
		[Route("api/Album/DeleteAlbum")]
		public void DeleteAlbum(int albumid)
		{
			var album = _dbContext.Albums.FirstOrDefault(a => a.Id == albumid);
			_dbContext.Remove(album);
			_dbContext.SaveChanges();
		}
	}
}
