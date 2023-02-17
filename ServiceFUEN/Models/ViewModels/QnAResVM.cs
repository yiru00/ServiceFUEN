using System;
namespace ServiceFUEN.Models.ViewModels
{
	public class QnAResVM
	{
        public string NickName { get; set; }
        public string PhotoSticker { get; set; }

        public int activityId { get; set; }
        public int QId { get; set; }
        public string QContent { get; set; }
        public DateTime QDateCreated { get; set; }
        public int MemberId { get; set; }

        
        public int? AId { get; set; }
        public string? AContent { get; set; }
        public DateTime? ADateCreated { get; set; }
    }
}

