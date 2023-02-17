using ServiceFUEN.Models.EFModels;

namespace ServiceFUEN.Models.ViewModels
{
    public static partial class ActivityExts
    {
        public static ActivityVM ToActivityVM(this Activity source)
        {
            return new ActivityVM
            {
                ActivityId = source.Id,
                CoverImage = source.CoverImage,
                ActivityName = source.ActivityName,
                Address = source.Address,
                CategoryName = source.Category.CategoryName,
                GatheringTime = source.GatheringTime,
                Deadline=source.Deadline,
                DateOfCreated=source.DateOfCreated,
                NumOfEnrolment = source.ActivityMembers.Count,
                NumOfCollections = source.ActivityCollections.Count,
                EnrolmentRate = source.ActivityMembers.Count / source.MemberLimit
            };
        }

        //public static ActivityDetailsVM ToActivityDetailsVM(this Activity? source)
        //{
        //    if (source==null)
        //    {
        //        return new ActivityDetailsVM
        //        {
        //            ActivityId = null,
        //            CoverImage = null,
        //            ActivityName = null,
        //            Recommendation = null,
        //            Address = null,
        //            MemberLimit = null,
        //            ActivityDescription = null,
        //            GatheringTime = null,
        //            Deadline = null,
        //            DateOfCreated = null,
        //            NumOfEnrolment = null,
        //            NumOfCollection = null,
        //            InstructorId = null,
        //            InstructorName = null,
        //            ResumePhoto = null,
        //            InstructorDescription = null,
        //            CategoryId = null,
        //            DisplayOrder = null,
        //            CategoryName = null
        //        };
        //    }
        //    else
        //    {
        //        return new ActivityDetailsVM
        //        {
        //            ActivityId = source.Id,
        //            CoverImage = source.CoverImage,
        //            ActivityName = source.ActivityName,
        //            Recommendation = source.Recommendation,
        //            Address = source.Address,
        //            MemberLimit = source.MemberLimit,
        //            ActivityDescription = source.Description,
        //            GatheringTime = source.GatheringTime,
        //            Deadline = source.Deadline,
        //            DateOfCreated = source.DateOfCreated,
        //            NumOfEnrolment = source.ActivityMembers.Count,
        //            NumOfCollection = source.ActivityCollections.Count,
        //            InstructorId = source.InstructorId,
        //            InstructorName = source.Instructor.InstructorName,
        //            ResumePhoto = source.Instructor.ResumePhoto,
        //            InstructorDescription = source.Instructor.Description,
        //            CategoryId = source.CategoryId,
        //            DisplayOrder = source.Category.DisplayOrder,
        //            CategoryName = source.Category.CategoryName
        //        };
        //   }
            
        //}
    }
}

