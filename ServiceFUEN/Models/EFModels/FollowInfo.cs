﻿using System;
using System.Collections.Generic;

namespace ServiceFUEN.Models.EFModels;

public partial class FollowInfo
{
    public int Follower { get; set; }

    public int Following { get; set; }

    public DateTime FollowTime { get; set; }

    public virtual Member FollowerNavigation { get; set; } = null!;

    public virtual Member FollowingNavigation { get; set; } = null!;
}
