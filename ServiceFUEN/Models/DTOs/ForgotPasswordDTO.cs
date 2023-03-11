using System.ComponentModel.DataAnnotations;

namespace ServiceFUEN.Models.DTOs
{
	public class ForgotPasswordDTO
	{
		public int Id { get; set; }

		[Required]
		[EmailAddress]
		public string EmailAccount { get; set; }
		public string RealName { get; set; }


	}
}
