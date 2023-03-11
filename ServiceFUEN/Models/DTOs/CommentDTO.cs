namespace ServiceFUEN.Models.DTOs
{
    public class CommentDTO
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public DateTime CommentTime { get; set; }
        public CommunityMemberDTO Author { get; set; }
    }
}
