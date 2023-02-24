namespace ServiceFUEN.Models.DTOs
{
	public class DateResultDTO
	{
		public DateResultDTO() 
		{
			this.DateViews = new List<int>();
			this.Date = new List<string>();
		}
		public List<int> DateViews { get; set; }
		public List<string> Date { get; set; }
	}
}
