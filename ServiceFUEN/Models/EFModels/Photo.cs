using System;
using System.Collections.Generic;

namespace ServiceFUEN.Models.EFModels;

public partial class Photo
{
    public int Id { get; set; }

    public string Source { get; set; } = null!;

    public string Title { get; set; } = null!;

    public string Description { get; set; } = null!;

    public int Author { get; set; }

    public int? Iso { get; set; }

    public string? Pixel { get; set; }

    public string? Aperture { get; set; }

    public string? Shutter { get; set; }

    public string? Camera { get; set; }

    public string? Negative { get; set; }

    public string Location { get; set; } = null!;

    public DateTime ShootingTime { get; set; }

    public DateTime UploadTime { get; set; }

    public virtual ICollection<AlbumItem> AlbumItems { get; } = new List<AlbumItem>();

    public virtual Member AuthorNavigation { get; set; } = null!;

    public virtual ICollection<Comment> Comments { get; } = new List<Comment>();

    public virtual ICollection<OthersCollection> OthersCollections { get; } = new List<OthersCollection>();

    public virtual ICollection<OwnCollection> OwnCollections { get; } = new List<OwnCollection>();

    public virtual ICollection<PhotoReport> PhotoReports { get; } = new List<PhotoReport>();

    public virtual ICollection<View> Views { get; } = new List<View>();

    public virtual ICollection<Tag> Tags { get; } = new List<Tag>();
}
