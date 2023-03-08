using System;
using System.Collections.Generic;

namespace ServiceFUEN.Models.EFModels;

public partial class Tag
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public DateTime CreatedTime { get; set; }

    public virtual ICollection<Photo> Photos { get; } = new List<Photo>();
}
