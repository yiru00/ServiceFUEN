using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ServiceFUEN.Models.EFModels;
using ServiceFUEN.Models.ViewModels;

namespace ServiceFUEN.Controllers
{
    
    
    [EnableCors("AllowAny")]
    public class ActivtiyEnrollController : Controller
    {
        private readonly ProjectFUENContext _context;

        public ActivtiyEnrollController(ProjectFUENContext context)
        {
            _context = context;
        }

        // Post: api/ActivtiyEnroll/Enroll
        //報名活動
        [HttpPost]
        [Route("api/ActivtiyEnroll/Enroll")]
        public bool Enroll(int memberId,int activityId)
        {
            //該會員是否報名過
            //該會員是否通過實名認證
            //活動是否存在
            //活動是否截止
            //把參數填到vm=>轉成entity=>存到資料庫
            //回傳{報名結果:true/false,訊息:"報名成功"/"失敗原因...."}
           //_context.Add(activityMember);
           //_context.SaveChangesAsync();
            return true; 
        }
    }
}
