using System;
using System.Collections.Generic;

namespace ServiceFUEN.Models.EFModels;

public partial class CommentReport
{
    public int Id { get; set; }

    public int? Reporter { get; set; }

    public int CommentId { get; set; }

    public DateTime ReportTime { get; set; }

    public virtual Comment Comment { get; set; } = null!;

    public virtual Member? ReporterNavigation { get; set; }
}
