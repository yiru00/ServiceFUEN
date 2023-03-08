using System;
using System.Collections.Generic;

namespace ServiceFUEN.Models.EFModels;

public partial class Instructor
{
    public int Id { get; set; }

    public string InstructorName { get; set; } = null!;

    public string ResumePhoto { get; set; } = null!;

    public string Description { get; set; } = null!;

    public virtual ICollection<Activity> Activities { get; } = new List<Activity>();
}
