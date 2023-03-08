using System;
using System.Collections.Generic;

namespace ServiceFUEN.Models.EFModels;

public partial class Album
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public int MemberId { get; set; }

    public DateTime CreatedTime { get; set; }

    public virtual ICollection<AlbumItem> AlbumItems { get; } = new List<AlbumItem>();

    public virtual Member Member { get; set; } = null!;
}
