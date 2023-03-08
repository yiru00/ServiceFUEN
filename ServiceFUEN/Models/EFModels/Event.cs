using System;
using System.Collections.Generic;

namespace ServiceFUEN.Models.EFModels;

public partial class Event
{
    public int Id { get; set; }

    public string EventName { get; set; } = null!;

    public string Photo { get; set; } = null!;

    public DateTime StartDate { get; set; }

    public DateTime EndDate { get; set; }

    public virtual ICollection<Product> Products { get; } = new List<Product>();
}
