namespace ServiceFUEN.Models.DTOs
{
	public class EditAlbumDTO
	{
		public int AlbumId { get; set; }
		public string? AlbumName { get; set; }
		public int[]? PhotoId { get; set; }
	}
}
