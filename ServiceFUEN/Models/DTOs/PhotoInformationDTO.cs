namespace ServiceFUEN.Models.DTOs
{
    public class PhotoInformationDTO
    {
        public int? ISO { get; set; }
        public string? Pixel { get; set; }
        public string? Aperture { get; set; }
        public string? Shutter { get; set; }
        public string? Camera { get; set; }
        public string? Negative { get; set; }
        public string Location { get; set; }
        public DateTime ShootingTime { get; set; }
    }
}
