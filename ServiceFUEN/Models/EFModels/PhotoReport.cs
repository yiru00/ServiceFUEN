using System;
using System.Collections.Generic;

namespace ServiceFUEN.Models.EFModels;

public partial class PhotoReport
{
    public int Id { get; set; }

    public int? Reporter { get; set; }

    public int PhotoId { get; set; }

    public DateTime ReportTime { get; set; }

    public virtual Photo Photo { get; set; } = null!;

    public virtual Member? ReporterNavigation { get; set; }
}
