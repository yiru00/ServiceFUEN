using ServiceFUEN.Models.EFModels;

namespace ServiceFUEN.Models.ViewModels
{
    public class ProductDetailVM
    {
        public int Id { get; set; }
        public string CategoryName { get; set; }
        public string BrandName { get; set; }
        public string? Name { get; set; }
        public int Price { get; set; }
        public DateTime ManufactorDate { get; set; }
        public DateTime ReleaseDate { get; set; }
        public int Inventory { get; set; }
        public string? ProductSpec { get; set; }

        public List<string> Source { get; set; }

    }
}
