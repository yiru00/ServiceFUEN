using System.Security.Principal;

namespace ServiceFUEN.Models.DTOs
{
    public class ShowPhotoDTO
    {
        public int Id { get; set; }
        public string Source { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int? ISO { get; set; }
        public string? Pixel { get; set; }
        public string? Aperture { get; set; }
        public string? Shutter { get; set; }
        public string? Camera { get; set; }
        public string? Negative { get; set; }
        public string Location { get; set; }
        public DateTime ShootingTime { get; set; }
        public DateTime UploadTime { get; set; }
        public MemberDTO Author { get; set; }
        public bool IsCollection { get; set; }
        public IEnumerable<CommentDTO> Comments { get; set; }
        public IEnumerable<TagDTO> Tags { get; set; }
        public int Views { get; set; }
    }
}
