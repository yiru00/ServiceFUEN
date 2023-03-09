using System.Security.Principal;

namespace ServiceFUEN.Models.DTOs
{
    public class ShowPhotoDTO
    {
        public int Id { get; set; }
        public string Source { get; set; }
        public string Title { get; set; }
        public bool IsCollection { get; set; }
        public string Camrea { get; set; }
        public CommunityMemberDTO Author { get; set; }
        
    }
}
