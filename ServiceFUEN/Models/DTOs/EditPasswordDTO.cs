using System.ComponentModel.DataAnnotations;

namespace ServiceFUEN.Models.DTOs
{
	public class EditPasswordDTO
	{
        public string OldEncryptedPassword { get; set; }

        [Required]
		[StringLength(12)]
		[DataType(DataType.Password)]
		public string EncryptedPassword { get; set; }

		[Required]
		[StringLength(12)]
		[Compare(nameof(EncryptedPassword))]
		[DataType(DataType.Password)]
		public string ConfirmEncryptedPassword { get; set; }
	}
}
