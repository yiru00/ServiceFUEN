﻿using ServiceFUEN.Models.EFModels;

namespace ServiceFUEN.Models.ViewModels
{
    public static partial class ActivityExts
    {
        public static ActivityCategoryVM toActivityCategoryVM(this ActivityCategory source)
        {
            return new ActivityCategoryVM
            {
                Id=source.Id,
                CategoryName=source.CategoryName,
                DisplayOrder=source.DisplayOrder
            };
        }
    }
}

