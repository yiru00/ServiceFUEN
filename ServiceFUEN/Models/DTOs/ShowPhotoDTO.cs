using System.Security.Principal;

namespace ServiceFUEN.Models.DTOs
{
    public class ShowPhotoDTO
    {
        public int Id { get; set; }
        public string Source { get; set; }
        public string Title { get; set; }
        public bool IsCollection { get; set; }
        public string Camera { get; set; }
        public string Author { get; set; }
		public string AuthorPhotoSticker { get; set; }
        public int AuthorId { get; set; }


    }
}
