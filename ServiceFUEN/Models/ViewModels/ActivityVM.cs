using System;

namespace ServiceFUEN.Models.ViewModels
{
    public class ActivityVM
	{
        public int ActivityId { get; set; }
        public string CoverImage { get; set; }
        //public string Route { get; set; }
        public string ActivityName { get; set; }
        public string Address { get; set; }
        public int? CategoryId { get; set; }
        public string CategoryName { get; set; }
        public string Description { get; set; }
        public string GatheringTime { get; set; }
        public string Deadline { get; set; }
        public string DateOfCreated { get; set; }
        public int NumOfEnrolment { get; set; }
        public int MemberLimit { get; set; }
        public int NumOfCollections { get; set; }
        public decimal EnrolmentRate { get; set; }

        public string InstructorName { get; set; }
        public string InstructorResumePhoto { get; set; }

    }
}

