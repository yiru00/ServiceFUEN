using ServiceFUEN.Models.EFModels;

namespace ServiceFUEN.Models.ViewModels
{
    public static partial class ActivityExts
    {
        public static ActivityVM toActivityVM(this Activity source)
        {
            return new ActivityVM
            {
                ActivityId = source.Id,
                CoverImage = source.CoverImage,
                ActivityName = source.ActivityName,
                Address = source.Address,
                CategoryName = source.Category.CategoryName,
                GatheringTime = source.GatheringTime,
                NumOfEnrolment = source.ActivityMembers.Count,
                NumOfCollections = source.ActivityCollections.Count,
                EnrolmentRate = source.ActivityMembers.Count / source.MemberLimit
            };
        }
    }
}

