using System;
using System.Collections.Generic;

namespace ServiceFUEN.Models.EFModels;

public partial class Product
{
    public int Id { get; set; }

    public int CategoryId { get; set; }

    public int BrandId { get; set; }

    public string Name { get; set; } = null!;

    public int Price { get; set; }

    public DateTime ManufactorDate { get; set; }

    public DateTime ReleaseDate { get; set; }

    public int Inventory { get; set; }

    public string ProductSpec { get; set; } = null!;

    public virtual Brand Brand { get; set; } = null!;

    public virtual Category Category { get; set; } = null!;

    public virtual ICollection<OrderItem> OrderItems { get; } = new List<OrderItem>();

    public virtual ICollection<ProductPhoto> ProductPhotos { get; } = new List<ProductPhoto>();

    public virtual ICollection<ShoppingCart> ShoppingCarts { get; } = new List<ShoppingCart>();

    public virtual ICollection<Event> Events { get; } = new List<Event>();

    public virtual ICollection<Member> Members { get; } = new List<Member>();
}
