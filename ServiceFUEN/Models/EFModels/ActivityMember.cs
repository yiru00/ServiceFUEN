using System;
using System.Collections.Generic;

namespace ServiceFUEN.Models.EFModels;

public partial class ActivityMember
{
    public int Id { get; set; }

    public int ActivityId { get; set; }

    public int MemberId { get; set; }

    public DateTime DateJoined { get; set; }

    public virtual Activity Activity { get; set; } = null!;

    public virtual Member Member { get; set; } = null!;
}
