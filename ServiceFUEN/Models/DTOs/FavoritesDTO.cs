using ServiceFUEN.Models.EFModels;

namespace ServiceFUEN.Models.ViewModels
{
    public class FavoritesDTO
    {
        public virtual ICollection<Member> Members { get; set; }
        public virtual ICollection<Product> Products { get; set; }
    }
}
