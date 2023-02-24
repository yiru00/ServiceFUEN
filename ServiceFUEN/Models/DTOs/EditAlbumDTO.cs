namespace ServiceFUEN.Models.DTOs
{
	public class EditAlbumDTO
	{
		//public int AlbumId { get; set; }
		//public string? AlbumName { get; set; }
		//public string CoverImg { get; set; }

		public AlbumDTO AlbumDTO { get; set; }
		public int[]? PhotoId { get; set; }
	}
}
