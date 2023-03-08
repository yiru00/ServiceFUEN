using System;
using System.Collections.Generic;

namespace ServiceFUEN.Models.EFModels;

public partial class Answer
{
    public int Id { get; set; }

    public string Content { get; set; } = null!;

    public DateTime DateCreated { get; set; }

    public int QuestionId { get; set; }

    public virtual Question Question { get; set; } = null!;
}
