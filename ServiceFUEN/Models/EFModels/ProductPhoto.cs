using System;
using System.Collections.Generic;

namespace ServiceFUEN.Models.EFModels;

public partial class ProductPhoto
{
    public int Id { get; set; }

    public int ProductId { get; set; }

    public string Source { get; set; } = null!;

    public virtual Product Product { get; set; } = null!;
}
