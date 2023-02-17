using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ServiceFUEN.Models.DTOs;
using ServiceFUEN.Models.EFModels;
using ServiceFUEN.Models.Infrastructures.ExtensionMethods;
using ServiceFUEN.Models.ViewModels;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ServiceFUEN.Controllers
{
    [EnableCors("AllowAny")]
    [ApiController]
    public class ActivityQnAController : Controller
    {
        private readonly ProjectFUENContext _context;

        public ActivityQnAController(ProjectFUENContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("api/ActivityQnA/Get")]
        public IEnumerable<QnAResVM> Get(int activityId)
        {
            var projectFUENContext = _context.Questions
                .Include(a => a.Answers)
                .Include(a => a.Member).Where(a=>a.ActivityId==activityId)
                .Select(a => a.ToQnAResVM()).ToList()
                .OrderByDescending(a => a.QDateCreated);
            return projectFUENContext.ToList();
        }

        [HttpPost]
        [Route("api/ActivityQnA/Ask")]
        public ActivityAskResVM Ask(ActivityAskReqDTO activityAskReq)
        {

            //取得值
            int memberId = activityAskReq.MemberId;
            int activityId = activityAskReq.ActivityId;
            string content = activityAskReq.content;

            //回傳結果
            ActivityAskResVM activityAskRes = new ActivityAskResVM();
            activityAskRes.result = false;

            var member = _context.Members.Find(memberId);
            var activity = _context.Activities.Find(activityId);

            //活動是否存在
            if (activity!=null)//存在
            {
                //判斷活動是否已舉辦
                if (activity.GatheringTime>DateTime.Now) //未舉辦
                {
                    //是否有會員（不用驗證）
                    if (member != null)
                    {
                        //發問
                        _context.Questions.Add(activityAskReq.ToQuestionEntity());
                        _context.SaveChanges();
                        activityAskRes.message = "發問成功";
                        activityAskRes.result = true;

                    }
                    else
                    {
                        activityAskRes.message = "未登入會員";
                    }
                }
                else
                {
                    activityAskRes.message = "活動已舉辦";
                }

            }
            else //不存在
            {
                activityAskRes.message = "活動不存在";
            }
            return activityAskRes;
        }

        [HttpDelete]
        [Route("api/ActivityQnA/DeleteQ")]
        public DeleteQResVM DeleteQ(int questionId)
        {
            var deleteQRes = new DeleteQResVM();
            deleteQRes.result = false;
            var question = _context.Questions.Find(questionId);

            //判斷是否有報名資料
            if (question != null)//有該資料
            {
                _context.Questions.Remove(question);
                _context.SaveChanges();
                deleteQRes.result = true;
                deleteQRes.message = "取消發問成功";
            }
            else
            {
                deleteQRes.message = "找不到此問題";
            }
            return deleteQRes;
        }
    }
}

