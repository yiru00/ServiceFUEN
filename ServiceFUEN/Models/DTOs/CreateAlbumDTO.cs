namespace ServiceFUEN.Models.DTOs
{
	public class CreateAlbumDTO
	{
		public string? AlbumName { get; set; }
		public string CoverImg { get; set; }
		public int MemberId { get; set; }
		public int[]? PhotoId { get; set; }
	}
}
