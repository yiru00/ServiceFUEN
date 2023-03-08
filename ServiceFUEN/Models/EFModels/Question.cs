using System;
using System.Collections.Generic;

namespace ServiceFUEN.Models.EFModels;

public partial class Question
{
    public int Id { get; set; }

    public string Content { get; set; } = null!;

    public DateTime DateCreated { get; set; }

    public int ActivityId { get; set; }

    public int MemberId { get; set; }

    public bool State { get; set; }

    public virtual Activity Activity { get; set; } = null!;

    public virtual ICollection<Answer> Answers { get; } = new List<Answer>();

    public virtual Member Member { get; set; } = null!;
}
