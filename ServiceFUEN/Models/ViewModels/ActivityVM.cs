using System;

namespace ServiceFUEN.Models.ViewModels
{
    public class ActivityVM
	{
        public int ActivityId { get; set; }
        public string CoverImage { get; set; }
        public string ActivityName { get; set; }
        public string Address { get; set; }
        public string CategoryName { get; set; }
        public DateTime GatheringTime { get; set; }
        public int NumOfEnrolment { get; set; }
        public int NumOfCollections { get; set; }
        public decimal EnrolmentRate { get; set; }

    }
}

