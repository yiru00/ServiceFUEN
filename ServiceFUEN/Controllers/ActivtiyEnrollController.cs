using System.Linq;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ServiceFUEN.Models.DTOs;
using ServiceFUEN.Models.EFModels;
using ServiceFUEN.Models.Infrastructures.ExtensionMethods;
using ServiceFUEN.Models.Services;
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
        //會員報名活動
        [HttpPost]
        [Route("api/ActivtiyEnroll/Enroll")]
        //會員報名功能
        public async Task<EnrollResVM> EnrollAsync(EnrollReqDTO enrollReq)
        {
            //取得要求資料
            int memberId = enrollReq.MemberId;
            int activityId = enrollReq.ActivityId;

            //準備回傳資料
            EnrollResVM enrollRes = new EnrollResVM();
            enrollRes.result = false;
            enrollRes.deleteId = 0;

            var member = _context.Members.Find(memberId);
            var activity = _context.Activities.Find(activityId);
            //會員是否存在
            if (member != null)
            {

                //活動是否存在？
                if (activity != null)//存在
                {

                    //會員是否通過實名驗證（抓手機、姓名有沒有填）
                    if (member.Mobile != null&&member.RealName!=null) //有填
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
                                    //enrollRes.memberRealName = member.RealName;
                                    //enrollRes.mobile = member.Mobile;

                                    int activityMemberId = _context.ActivityMembers.Where(a => a.ActivityId == activityId).FirstOrDefault(a => a.MemberId == memberId).Id;
                                    enrollRes.deleteId = activityMemberId;

                                    //寄信
                                    var emailService = new EmailService();
                                    await emailService.SendEmailAsync(member.EmailAccount, "活動報名成功！",
                                        $"<p>{member.RealName}您好：</p>"+
                                         $"<p>您已成功報名「{activity.ActivityName}」活動</p>" +
                                        $"<p>集合時間：{activity.GatheringTime.ToString("yyyy-MM-dd HH:mm")}</p>"+
                                        $"<p>集合地點：{activity.Address}</p>"+
                                        $"<a href='http://localhost:5173/Activity/{activity.Id}'>更多資訊</a>"
                                        );

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

            return enrollRes;
        }

        [HttpDelete]
        [Route("api/ActivtiyEnroll/CancelEnroll/{activityMemberId}")]
        public CancelEnrollResVM CancelEnroll(int activityMemberId)
        {
            var enrollRes = new CancelEnrollResVM();
            enrollRes.result = false;
            var activityMember = _context.ActivityMembers.Find(activityMemberId);
            //判斷是否有報名資料
            if (activityMember != null)//有該資料
            {
                _context.ActivityMembers.Remove(activityMember);
                _context.SaveChanges();
                enrollRes.result = true;
                enrollRes.message = "取消成功";
            }
            else
            {
                enrollRes.message = "會員未報名該活動";
            }
            return enrollRes;
        }

        [HttpPost]
        [Route("api/ActivityEnroll/EnrollStatus")]
        //前台顯示活動的四種可能情況(已截止/已額滿/可報名/已報名(若沒傳memberId（=0）進來就不會走到這步判斷))
        public EnrollStatusResVM EnrollStatus(EnrollReqDTO enrollReq)
        {
            //取得要求資料
            int memberId = enrollReq.MemberId;
            int activityId = enrollReq.ActivityId;


            var member = _context.Members.Find(memberId);
            var activity = _context.Activities.Find(activityId);

            //準備回傳資料
            EnrollStatusResVM enrollStatusRes = new EnrollStatusResVM();

            //活動是否存在？
            if (activity != null)//存在
            {
                enrollStatusRes.ActivityName = activity.ActivityName;
                //活動是否截止？
                if (activity.Deadline > DateTime.Now)//未截止（活動截止日大於現在）
                {
                    //該會員是否報名過？
                    var isEnrolled = _context.ActivityMembers.Where(a => a.ActivityId == activityId).FirstOrDefault(a => a.MemberId == memberId);
                    if (member != null && isEnrolled != null) //會員有存在 且報名過（在此活動中的參加名單有此會員）
                    {
                        enrollStatusRes.statusId = 5;
                        enrollStatusRes.message = "已報名";
                        enrollStatusRes.deleteId = isEnrolled.Id;

                    }
                    else
                    {
                        //報名人數
                        int numOfEnrolment = _context.Activities.Include(a => a.ActivityMembers).FirstOrDefault(a => a.Id == activityId).ActivityMembers.Count;

                        //人數限制
                        int memberLimit = _context.Activities.FirstOrDefault(a => a.Id == activityId).MemberLimit;

                        //活動是否額滿?
                        if (memberLimit > numOfEnrolment)//未額滿（限制人數>報名人數）
                        {
                            enrollStatusRes.statusId = 4;
                            enrollStatusRes.message = "可報名";


                        }
                        else //已額滿
                        {
                            enrollStatusRes.statusId = 3;
                            enrollStatusRes.message = "已額滿";
                        }
                    }

                    
                }
                else //截止
                {
                    enrollStatusRes.statusId = 2;
                    enrollStatusRes.message = "已截止";
                }

            }

            else //活動不存在
            {
                enrollStatusRes.statusId = 1;
                enrollStatusRes.message = "沒有此活動";
            }
            return enrollStatusRes;
        }

    }
}
