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
                //Route = "/Activity/" + source.Id,
                Address = source.Address,
                CategoryId = source.CategoryId,
                CategoryName = source.Category.CategoryName,
                Description = source.Description,
                GatheringTime = source.GatheringTime.ToString("yyyy-MM-dd HH:mm"),
                Deadline = source.Deadline.ToString("yyyy-MM-dd HH:mm"),
                DateOfCreated = source.DateOfCreated.ToString("yyyy-MM-dd HH:mm"),
                NumOfEnrolment = source.ActivityMembers.Count,
                MemberLimit = source.MemberLimit,
                NumOfCollections = source.ActivityCollections.Count,
                EnrolmentRate = source.ActivityMembers.Count * 100 / source.MemberLimit ,
                InstructorName = source.Instructor.InstructorName,
                InstructorResumePhoto = source.Instructor.ResumePhoto,
               
            };
        }

        public static ActivityResVM ToActivityResVM(this Activity source)
        {
            return new ActivityResVM
            {
                ActivityId = source.Id,
                //route="/Activity/"+ source.Id,
                CoverImage = source.CoverImage,
                ActivityName = source.ActivityName,
                Address = source.Address,
                City=source.Address.Substring(0,3),
                CategoryId = source.CategoryId,
                CategoryName = source.Category.CategoryName,
                Description = source.Description,
                GatheringTime = source.GatheringTime.ToString("yyyy-MM-dd HH:mm"),
                Deadline = source.Deadline.ToString("yyyy-MM-dd HH:mm"),
                DateOfCreated = source.DateOfCreated.ToString("yyyy-MM-dd HH:mm"),
                NumOfEnrolment = source.ActivityMembers.Count,
                MemberLimit = source.MemberLimit,
                NumOfCollections = source.ActivityCollections.Count,
                EnrolmentRate = source.ActivityMembers.Count * 100 / source.MemberLimit,
                InstructorName = source.Instructor.InstructorName,
                InstructorResumePhoto = source.Instructor.ResumePhoto,
                statusId = 3,
                message = "可收藏",
                UnSaveId = 0,
        
            };
        }

        public static ActivityDetailsVM ToActivityDetailsVM(this Activity? source)
        {
            if (source==null)
            {
                return new ActivityDetailsVM
                {
                    ActivityId = null,
                    CoverImage = null,
                    ActivityName = null,
                    Recommendation = null,
                    Address = null,
                    MemberLimit = null,
                    ActivityDescription = null,
                    GatheringTime = null,
                    Deadline = null,
                    DateOfCreated = null,
                    NumOfEnrolment = null,
                    NumOfCollection = null,
                    InstructorId = null,
                    InstructorName = null,
                    ResumePhoto = null,
                    InstructorDescription = null,
                    CategoryId = null,
                    DisplayOrder = null,
                    CategoryName = null
                };
            }
            else
            {
                return new ActivityDetailsVM
                {
                    ActivityId = source.Id,
                    CoverImage = source.CoverImage,
                    ActivityName = source.ActivityName,
                    Recommendation = source.Recommendation,
                    Address = source.Address,
                    MemberLimit = source.MemberLimit,
                    ActivityDescription = source.Description,
                    GatheringTime = source.GatheringTime.ToString("yyyy-MM-dd HH:mm"),
                    Deadline = source.Deadline.ToString("yyyy-MM-dd HH:mm"),
                    DateOfCreated = source.DateOfCreated.ToString("yyyy-MM-dd HH:mm"),
                    NumOfEnrolment = source.ActivityMembers.Count,
                    NumOfCollection = source.ActivityCollections.Count,
                    InstructorId = source.InstructorId,
                    InstructorName = source.Instructor.InstructorName,
                    ResumePhoto = source.Instructor.ResumePhoto,
                    InstructorDescription = source.Instructor.Description,
                    CategoryId = source.CategoryId,
                    DisplayOrder = source.Category.DisplayOrder,
                    CategoryName = source.Category.CategoryName
                };
            }
            
        }
    }
}

