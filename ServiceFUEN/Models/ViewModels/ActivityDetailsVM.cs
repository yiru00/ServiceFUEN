using System;
namespace ServiceFUEN.Models.ViewModels
{
	public class ActivityDetailsVM
	{
        public int? ActivityId { get; set; }
        public string? CoverImage { get; set; }
        public string? ActivityName { get; set; }
        public string? Recommendation { get; set; }
        public string? Address { get; set; }
        public int? MemberLimit { get; set; }
        public string? ActivityDescription { get; set; }
        public string? GatheringTime { get; set; }
        public string? Deadline { get; set; }
        public string? DateOfCreated { get; set; }
        //報名人數
        public int? NumOfEnrolment { get; set; }

        //收藏數
        public int? NumOfCollection { get; set; }

        //講師
        public int? InstructorId { get; set; }
        public string? InstructorName { get; set; }
        public string? ResumePhoto { get; set; }
        public string? InstructorDescription { get; set; }

        //分類
        public int? CategoryId { get; set; }
        public int? DisplayOrder { get; set; }
        public string? CategoryName { get; set; }
    }
}

