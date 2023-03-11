using System.ComponentModel.DataAnnotations;

namespace ServiceFUEN.Models.DTOs
{
	public class MemberDTO
	{
		public int Id { get; set; }

		[EmailAddress]
		public string EmailAccount { get; set; }
		public string EncryptedPassword { get; set; }
		public string? RealName { get; set; }
		public string NickName { get; set; }
		public DateTime? BirthOfDate { get; set; }
		public string? Mobile { get; set; }
		public string? Address { get; set; }
		public string? PhotoSticker { get; set; }
		public string? About { get; set; }
		public string? ConfirmCode { get; set; }
		public bool IsConfirmed { get; set; }
		public bool IsInBlackList { get; set; }
	}
}
