namespace ServiceFUEN.Models.ViewModels
{
    public class ArticleListVM
    {
        public int ArticleId { get; set; }
        public string Title { get; set; }
        public DateTime Time { get; set; }
        public int MemberId { get; set; }
        public string NickName { get; set; }
        public int ForumId { get; set; }
        public string ForumName { get; set; }


    }
}
