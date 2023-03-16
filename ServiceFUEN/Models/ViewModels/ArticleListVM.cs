using ServiceFUEN.Models.EFModels;

namespace ServiceFUEN.Models.ViewModels
{
    public class ArticleListVM
    {
        public string ArticlePhoto { get; set; }
        public int ArticleId { get; set; }
        public string Title { get; set; }
        public string Time { get; set; }
        public int MemberId { get; set; }
        public string NickName { get; set; }
        public int ForumId { get; set; }
        public string ForumName { get; set; }

        public string Content { get; set; }
        public string PhotoSticker { get; set; }
        public IEnumerable<MessageVM> MessageComment { get; set; }

    }
}
