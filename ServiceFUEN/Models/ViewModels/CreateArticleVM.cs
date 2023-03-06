namespace ServiceFUEN.Models.ViewModels
{
    public class CreateArticleVM
    {
        public IEnumerable<IFormFile> Files { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public int MemberId { get; set; }
        public int ForumId { get; set; }
    }
}
