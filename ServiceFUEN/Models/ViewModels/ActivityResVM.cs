using System;

namespace ServiceFUEN.Models.ViewModels
{
    public class ActivityResVM
	{
        public int ActivityId { get; set; }
        public string CoverImage { get; set; }
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



        //狀態用
        public int statusId { get; set; }
        public string message { get; set; }
        public int UnSaveId { get; set; }

        //statusId=1 message="沒有此活動"
        //statusId=2 message="活動已舉辦"
        //statusId=3 message="可收藏"
        //statusId=4 message="已收藏過"
        //statusId=5 message="已舉辦且已收藏過"

    }
}

