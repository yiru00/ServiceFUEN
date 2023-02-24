using ServiceFUEN.Models.EFModels;

namespace ServiceFUEN.Models.DTOs
{
    public class MessageDto
    {
        public string Content { get; set; }
        public int MemberId { get; set; }
        public int ArticleId { get; set; }

    }

    public static partial class ExtensionMethods
    {
        public static Message VMToEntity(this MessageDto source)
        {
            return new Message()
            {
                Content = source.Content,
                MemberId = source.MemberId,
                ArticleId = source.ArticleId,
            };
        }
    }

}
