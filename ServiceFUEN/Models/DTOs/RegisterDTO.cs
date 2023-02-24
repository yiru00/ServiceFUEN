using System.ComponentModel.DataAnnotations;

namespace ServiceFUEN.Models.DTOs
{
	public class RegisterDTO
	{
		[Display(Name = "帳號")]
		[Required]
		[EmailAddress]
		public string EmailAccount { get; set; }

		[Display(Name = "密碼")]
		[Required]
		[StringLength(12)]
		[DataType(DataType.Password)]
		public string EncryptedPassword { get; set; }

		[Display(Name = "重複確認密碼")]
		[Required]
		[StringLength(12)]
		[Compare(nameof(EncryptedPassword))]
		[DataType(DataType.Password)]
		public string ConfirmEncryptedPassword { get; set; }
		public string NickName { get; set; }


	}
}
