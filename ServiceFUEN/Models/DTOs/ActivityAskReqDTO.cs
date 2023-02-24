using System;
namespace ServiceFUEN.Models.DTOs
{
	public class ActivityAskReqDTO
	{
		public int MemberId { get; set; }
		public int ActivityId { get; set; }
		public string content { get; set; }
	}
}

