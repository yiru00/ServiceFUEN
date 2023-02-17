using ServiceFUEN.Models.EFModels;

namespace ServiceFUEN.Models.DTOs
{
    public class PhotoDTO
    {
        public int Id { get; set; }
        public string Source { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int Author { get; set; }
        public int? ISO { get; set; }
        public string Pixel { get; set; }
        public string Aperture { get; set; }
        public string Shutter { get; set; }
        public string Camera { get; set; }
        public string Negative { get; set; }
        public string Location { get; set; }
        public DateTime ShootingTime { get; set; }
        public DateTime UploadTime { get; set; }
        public bool IsCollection { get; set; }
        public DateTime? CollectionTime { get; set; }
    }

    public static class  ExtensionMethods
    {
        public static Photo DtoToEntity(this PhotoDTO photo)
        {
            return new Photo()
            {
                Id = photo.Id,
                Source = photo.Source,
                Title = photo.Title,
                Description = photo.Description,
                Author = photo.Author,
                ISO = photo.ISO,
                Pixel = photo.Pixel,
                Aperture = photo.Aperture,
                Shutter = photo.Shutter,
                Camera = photo.Camera,
                Negative = photo.Negative,
                Location = photo.Location,
                ShootingTime = photo.ShootingTime,
                UploadTime = photo.UploadTime,
                IsCollection = photo.IsCollection,
                CollectionTime = photo.CollectionTime,
            };
        }
    }
}
