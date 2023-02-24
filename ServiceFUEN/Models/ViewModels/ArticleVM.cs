namespace ServiceFUEN.Models.ViewModels
{
    public class ArticleVM
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime Time { get; set; }
        public int MemberId { get; set; }
        public int ForumId { get; set; }

    }
}
