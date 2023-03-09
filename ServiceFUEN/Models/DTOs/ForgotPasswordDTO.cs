using System.ComponentModel.DataAnnotations;

namespace ServiceFUEN.Models.DTOs
{
	public class ForgotPasswordDTO
	{
		public string RealName { get; set; }
		public string EmailAccount { get; set; }
		public string Mobile { get; set; }
		
	}
}
