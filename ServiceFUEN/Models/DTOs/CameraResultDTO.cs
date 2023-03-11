namespace ServiceFUEN.Models.DTOs
{
	public class CameraResultDTO
	{
		public CameraResultDTO()
		{
			this.CameraCategory = new List<string>();
			this.CameraCount = new List<int>();
		}

		public List<string> CameraCategory { get; set; }
		public List<int> CameraCount { get; set; }
	}
}
