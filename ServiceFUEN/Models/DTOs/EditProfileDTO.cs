using System.ComponentModel.DataAnnotations;

namespace ServiceFUEN.Models.DTOs
{
	public class EditProfileDTO
	{
		public int Id { get; set; }
		public string? RealName { get; set; }
		public string NickName { get; set; }
		public DateTime? BirthOfDate { get; set; }
		public string? Mobile { get; set; }
		public string? Address { get; set; }
		public IFormFile File { get; set; }
		public string? About { get; set; }
	}
}
