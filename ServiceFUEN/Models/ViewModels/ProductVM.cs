using ServiceFUEN.Models.EFModels;

namespace ServiceFUEN.Models.ViewModels
{
    public class ProductDetailVM
    {
        public int Id { get; set; }
        public int CategoryId { get; set; }
        public int BrandId { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }
        public DateTime ManufactorDate { get; set; }
        public DateTime ReleaseDate { get; set; }
        public int Inventory { get; set; }
        public string ProductSpec { get; set; }
        public string Source { get; set; }

        public virtual ICollection<ProductPhoto> ProductPhotos { get; set; }
    }
}
