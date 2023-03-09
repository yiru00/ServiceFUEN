namespace ServiceFUEN.Models.DTOs
{
	public class TopPhotoResultDTO
	{
		public TopPhotoResultDTO() 
		{
			this.PhotoSrc = new List<TopPhotoSrc>();
			this.PhotoId = new List<int?>();
			this.PhotoViews = new List<int?>();
			this.PhotoTitle = new List<string?>();
		}
		public List<TopPhotoSrc> PhotoSrc { get; set; }
		public List<int?> PhotoId { get; set; }
		public List<int?> PhotoViews { get; set; }
		public List<string?> PhotoTitle { get; set; }

	}
}
