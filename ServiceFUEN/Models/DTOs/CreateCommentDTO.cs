using ServiceFUEN.Models.EFModels;

namespace ServiceFUEN.Models.DTOs
{
	public class CreateCommentDTO
	{
		public int Id { get; set; }
		public string Content { get; set; }
		public int MemberId { get; set; }
		public int PhotoId { get; set; }
	}

	public static partial class ExtensionMethods
	{
		public static Comment DtoToEntity(this CreateCommentDTO source)
		{
			return new Comment()
			{
				Id = source.Id,
				Content = source.Content,
				MemberId = source.MemberId,
				PhotoId = source.PhotoId,
			};
		}
	}
}
