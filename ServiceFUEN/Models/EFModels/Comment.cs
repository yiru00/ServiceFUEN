﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace ServiceFUEN.Models.EFModels
{
    public partial class Comment
    {
        public Comment()
        {
            CommentReports = new HashSet<CommentReport>();
        }

        public int Id { get; set; }
        public string Content { get; set; }
        public DateTime CommentTime { get; set; }
        public int PhotoId { get; set; }
        public int MemberId { get; set; }

        public virtual Member Member { get; set; }
        public virtual Photo Photo { get; set; }

        public virtual ICollection<CommentReport> CommentReports { get; set; }
    }
}