using System;
using System.Collections.Generic;

namespace ServiceFUEN.Models.EFModels;

public partial class Activity
{
    public int Id { get; set; }

    public string CoverImage { get; set; } = null!;

    public string ActivityName { get; set; } = null!;

    public string Recommendation { get; set; } = null!;

    public string Address { get; set; } = null!;

    public int MemberLimit { get; set; }

    public string Description { get; set; } = null!;

    public DateTime GatheringTime { get; set; }

    public DateTime Deadline { get; set; }

    public DateTime DateOfCreated { get; set; }

    public int? InstructorId { get; set; }

    public int? CategoryId { get; set; }

    public virtual ICollection<ActivityCollection> ActivityCollections { get; } = new List<ActivityCollection>();

    public virtual ICollection<ActivityMember> ActivityMembers { get; } = new List<ActivityMember>();

    public virtual ActivityCategory? Category { get; set; }

    public virtual Instructor? Instructor { get; set; }

    public virtual ICollection<Question> Questions { get; } = new List<Question>();
}
