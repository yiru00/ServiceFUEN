using ServiceFUEN.Models.EFModels;

namespace ServiceFUEN.Models.DTOs
{
    public class AlbumDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public static partial class ExtensionMethods
    {
        public static AlbumDTO EntityTODto(this Album entity)
        {
            return new AlbumDTO()
            {
                Id = entity.Id,
                Name = entity.Name
            };
        }
    }
}
