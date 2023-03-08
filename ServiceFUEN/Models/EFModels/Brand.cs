using System;
using System.Collections.Generic;

namespace ServiceFUEN.Models.EFModels;

public partial class Brand
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<Product> Products { get; } = new List<Product>();
}
