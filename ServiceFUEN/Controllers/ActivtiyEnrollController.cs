using System.Linq;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ServiceFUEN.Models.EFModels;
using ServiceFUEN.Models.Infrastructures.ExtensionMethods;
using ServiceFUEN.Models.ViewModels;

namespace ServiceFUEN.Controllers
{
    
    
    [EnableCors("AllowAny")]
    [ApiController]
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
        public  EnrollResVM Enroll(EnrollReqVM enrollReq)
        {
            //取得要求資料
            int memberId = enrollReq.MemberId;
            int activityId = enrollReq.ActivityId;

            //準備回傳資料
            EnrollResVM enrollRes = new EnrollResVM();
            enrollRes.result = false;

            var member = _context.Members.Find(memberId);
            var activity = _context.Activities.Find(activityId);
            //會員是否存在
            if (member != null)
            {

                //活動是否存在？
                if (activity != null)//存在
                {

                    //會員是否通過實名驗證（抓手機有沒有填就好）
                    if (member.Mobile != null) //有填
                    {
                        //該會員是否報名過？
                        var isEnrolled = _context.ActivityMembers.Where(a => a.ActivityId == activityId).FirstOrDefault(a => a.MemberId == memberId);
                        if (isEnrolled == null) //沒報名過（在此活動中的參加名單無此會員）
                        {
                            //活動是否截止？
                            if (activity.Deadline > DateTime.Now)//未截止（活動截止日大於現在）
                            {

                                //報名人數
                                int numOfEnrolment = _context.Activities.Include(a => a.ActivityMembers).FirstOrDefault(a => a.Id == activityId).ActivityMembers.Count;

                                //人數限制
                                int memberLimit = _context.Activities.FirstOrDefault(a => a.Id == activityId).MemberLimit;

                                //活動是否額滿?
                                if (memberLimit > numOfEnrolment)//未額滿（限制人數>報名人數）
                                {
                                   //報名(新增一筆活動成員)
                                    _context.ActivityMembers.Add(enrollReq.ToActivityMemberEntity());
                                    _context.SaveChanges();
                                    enrollRes.message = "報名成功";
                                    enrollRes.result = true;
                                    
                                }
                                else //已額滿
                                {
                                    enrollRes.message = "報名的活動已額滿";
                                }
                            }
                            else //截止
                            {
                                enrollRes.message = "報名的活動已截止";
                            }
                        }//報名過
                        else
                        {
                            enrollRes.message = "會員已報名過";
                        }
                    }
                    else //沒驗證
                    {
                        enrollRes.message = "會員未通過實名驗證";
                    }

                }
                else //活動不存在
                {
                    enrollRes.message = "報名的活動不存在";
                }

            }
            else
            {
                enrollRes.message = "會員不存在";
            }

            //找到該會員在該活動的報名ID(之後拿來刪除用)
            int activityMemberId = _context.ActivityMembers.Where(a => a.ActivityId == activityId).Where(a => a.MemberId == memberId).ToList()[0].Id;
            enrollRes.deleteId = activityMemberId;
            return enrollRes; 
        }

        [HttpDelete]
        [Route("api/ActivtiyEnroll/CancelEnroll")]
        public EnrollResVM CancelEnroll(int activityMemberId)
        {
            return new EnrollResVM();
        }
    
    }
}
