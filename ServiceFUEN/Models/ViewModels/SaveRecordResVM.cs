using System;
namespace ServiceFUEN.Models.ViewModels
{
	public class SaveRecordResVM
    {
        public int MemberId { get; set; }
        public int ActivityId { get; set; }
        public string Route { get; set; }
        public string CoverImage { get; set; }
        public string ActivityName { get; set; }
        public string Recommendation { get; set; }
        public string Address { get; set; }
        public int MemberLimit { get; set; }
        public string Description { get; set; }
        public string GatheringTime { get; set; }
        public string Deadline { get; set; }
        public string DateOfCreated { get; set; }

        public int SaveId { get; set; }
        public string DateOfSave { get; set; }
    }
}

