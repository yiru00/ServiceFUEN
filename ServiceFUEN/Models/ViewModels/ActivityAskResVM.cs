using System;
namespace ServiceFUEN.Models.ViewModels
{
	public class ActivityAskResVM
	{
		public bool result { get; set; }
		public string message { get; set; }
		public int qId { get; set; }
		public string nickName { get; set; }
		public string photoSticker { get; set; }
		public DateTime? qDateCreated { get; set; }
		public string qContent { get; set; }
	}
}

