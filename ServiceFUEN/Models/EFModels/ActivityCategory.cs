using System;
using System.Collections.Generic;

namespace ServiceFUEN.Models.EFModels;

public partial class ActivityCategory
{
    public int Id { get; set; }

    public int DisplayOrder { get; set; }

    public string CategoryName { get; set; } = null!;

    public virtual ICollection<Activity> Activities { get; } = new List<Activity>();
}
