using System;
using System.Collections.Generic;

namespace ServiceFUEN.Models.EFModels;

public partial class Comment
{
    public int Id { get; set; }

    public string Content { get; set; } = null!;

    public DateTime CommentTime { get; set; }

    public int PhotoId { get; set; }

    public int MemberId { get; set; }

    public virtual ICollection<CommentReport> CommentReports { get; } = new List<CommentReport>();

    public virtual Member Member { get; set; } = null!;

    public virtual Photo Photo { get; set; } = null!;
}
