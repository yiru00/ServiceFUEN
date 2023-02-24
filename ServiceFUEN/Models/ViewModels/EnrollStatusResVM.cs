using System;

namespace ServiceFUEN.Models.ViewModels
{
    public class EnrollStatusResVM
    {

        public string ActivityName { get; set; }
        public int statusId { get; set; }
        public string message { get; set; }
        public int deleteId { get; set; }

        //statusId=1 message="沒有此活動"
        //statusId=2 message="已截止"
        //statusId=3 message="已額滿"
        //statusId=4 message="可報名"
        //statusId=5 message="已報名"

    }
}

