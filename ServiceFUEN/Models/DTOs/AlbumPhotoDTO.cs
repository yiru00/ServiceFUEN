namespace ServiceFUEN.Models.DTOs
{
    public class AlbumPhotoDTO
    {
        //public int PhotoId { get; set; }
        //public string PhotoSrc { get; set; }
        public PhotoSrcDTO PhotoSrcDTO { get; set; }
        public string CoverImg { get; set; }
		public string AlbumName { get; set;}
        public int AlbumId { get; set;} 
    }
}