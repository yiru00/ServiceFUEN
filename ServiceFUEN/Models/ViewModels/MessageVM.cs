namespace ServiceFUEN.Models.ViewModels
{
    public class MessageVM
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public string Time { get; set; }
        public int MemberId { get; set; }
        public int ArticleId { get; set; }
        public string NickName { get; set; }

    }
}
