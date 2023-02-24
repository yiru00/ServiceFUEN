using ServiceFUEN.Models.EFModels;

namespace ServiceFUEN.Models.ViewModels
{
	public class ReturnVM
    {
		public string Messsage { get; set; } = "";
		public int Code { get; set; } = 0;

		public object? Data { get; set; }
	}

    public enum RetunCode
	{
		呼叫失敗 = -1,
		初始值 = 0,
		呼叫成功 = 1,
	}
}
