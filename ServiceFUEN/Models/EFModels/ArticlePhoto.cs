﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace ServiceFUEN.Models.EFModels
{
    public partial class ArticlePhoto
    {
        public int Id { get; set; }
        public string Photo { get; set; }
        public int ArticleId { get; set; }

        public virtual Article Article { get; set; }
    }
}