using System;
using System.Collections.Generic;

namespace ServiceFUEN.Models.EFModels;

public partial class OwnCollection
{
    public int MemberId { get; set; }

    public int PhotoId { get; set; }

    public DateTime AddTime { get; set; }

    public virtual Member Member { get; set; } = null!;

    public virtual Photo Photo { get; set; } = null!;
}
