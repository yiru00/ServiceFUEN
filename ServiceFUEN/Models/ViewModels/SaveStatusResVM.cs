using System;

namespace ServiceFUEN.Models.ViewModels
{
    public class SaveStatusResVM
    {

        public string ActivityName { get; set; }
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

