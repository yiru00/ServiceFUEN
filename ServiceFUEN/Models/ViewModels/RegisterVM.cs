using ServiceFUEN.Models.DTOs;
using System.ComponentModel.DataAnnotations;

namespace ServiceFUEN.Models.ViewModels
{
	public class RegisterVM
	{
		[Display(Name = "帳號")]
		[Required]
		public string EmailAccount { get; set; }

		[Required]
		[StringLength(12)]
		[DataType(DataType.Password)]
		public string EncryptedPassword { get; set; }

		[Required]
		[StringLength(12)]
		[Compare(nameof(EncryptedPassword))]
		[DataType(DataType.Password)]
		public string ConfirmEncryptedPassword { get; set; }
		public string NickName { get; set; }
	}
	public static class RegisterVMExts
	{
		public static RegisterDTO ToRequestDto(this RegisterVM source)
		{
			return new RegisterDTO
			{
				EmailAccount = source.EmailAccount,
				EncryptedPassword = source.EncryptedPassword,
				NickName = source.NickName,
			};
		}
	}
}
