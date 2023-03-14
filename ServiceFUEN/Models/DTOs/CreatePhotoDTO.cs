using ServiceFUEN.Models.EFModels;

namespace ServiceFUEN.Models.DTOs
{
    public class CreatePhotoDTO
    {
        public int Id { get; set; }
        public IFormFile File { get; set; }
        //public int Author { get; set; }
        public string Title { get; set; }
        public string Camera { get; set; }
   }

    public static partial class ExtensionMethods
    {
        public static Photo DtoToEntity(this CreatePhotoDTO dto)
        {
            // File的Name沒有Assign to Photo
            return new Photo()
            {
                Id = dto.Id,
                Title = dto.Title,
                Camera = dto.Camera,
                //Author = dto.Author
            };
        }
    }
}
