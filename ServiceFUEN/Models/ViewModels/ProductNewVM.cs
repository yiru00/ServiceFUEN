namespace ServiceFUEN.Models.ViewModels
{
    public class ProductNewVM
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public int Price { get; set; }
        public DateTime ReleaseDate { get; set; }

        public string Source { get; set; }
    }
}
