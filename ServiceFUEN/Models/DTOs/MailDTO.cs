using ServiceFUEN.Models.EFModels;

namespace ServiceFUEN.Models.DTOs
{
	public class MailDTO
	{
		public int Id { get; set; }
		public string EmailAccount { get; set; }
		public string EncryptedPassword { get; set; }
		public string RealName { get; set; }
		public string ConfirmCode { get; set; }

	}
	public static class MailEx
	{
		public static MailDTO MailUser(this Member source)
		{
			return new MailDTO()
			{
				Id = source.Id,
				EmailAccount = source.EmailAccount,
				RealName = source.RealName,

				EncryptedPassword = source.EncryptedPassword,
				ConfirmCode = source.ConfirmCode,
			};
		}
	}
}
