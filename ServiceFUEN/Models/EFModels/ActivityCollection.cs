using System;
using System.Collections.Generic;

namespace ServiceFUEN.Models.EFModels;

public partial class ActivityCollection
{
    public int Id { get; set; }

    public int ActivityId { get; set; }

    public int UserId { get; set; }

    public DateTime DateCreated { get; set; }

    public virtual Activity Activity { get; set; } = null!;

    public virtual Member User { get; set; } = null!;
}
