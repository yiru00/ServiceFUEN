using ServiceFUEN.Models.EFModels;
using System.Diagnostics.CodeAnalysis;

namespace ServiceFUEN.Models.DTOs
{
    public class PhotoDTO
    {
        public int Id { get; set; }
        public IFormFile File { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int Author { get; set; }
        public int? ISO { get; set; }
        public string? Pixel { get; set; }
        public string? Aperture { get; set; }
        public string? Shutter { get; set; }
        public string? Camera { get; set; }
        public string? Negative { get; set; }
        public string Location { get; set; }
        public DateTime ShootingTime { get; set; }
        public bool IsCollection { get; set; }
        public DateTime? CollectionTime { get; set; }
        public IEnumerable<int> AlbumIds { get; set; }
    }

    public static partial class  ExtensionMethods
    {
        public static Photo DtoToEntity(this PhotoDTO dto)
        {
            // File的Name沒有Assign to Photo
            return new Photo()
            {
                Id = dto.Id,
                Title = dto.Title,
                Description = dto.Description,
                Author = dto.Author,
                ISO = dto.ISO,
                Pixel = dto.Pixel,
                Aperture= dto.Aperture,
                Shutter= dto.Shutter,
                Camera= dto.Camera,
                Negative= dto.Negative,
                Location= dto.Location,
                ShootingTime= dto.ShootingTime,
                IsCollection = dto.IsCollection,
                CollectionTime = dto.CollectionTime,
            };
        }
    }
}
