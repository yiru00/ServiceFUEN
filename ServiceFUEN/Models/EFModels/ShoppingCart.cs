﻿using System;
using System.Collections.Generic;

namespace ServiceFUEN.Models.EFModels;

public partial class ShoppingCart
{
    public int MemberId { get; set; }

    public int ProductId { get; set; }

    public int Number { get; set; }

    public virtual Member Member { get; set; } = null!;

    public virtual Product Product { get; set; } = null!;
}
