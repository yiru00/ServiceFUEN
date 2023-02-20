namespace ServiceFUEN.Models.ViewModels
{
    public class ProductSearchVM
    {
        //public int Id { get; set; }
        public string? Name { get; set; }
        public int CategoryId { get; set; }
        public int BrandId { get; set; }

        public int Price { get; set; }

        public List<string> Source { get; set; }
    }
}
