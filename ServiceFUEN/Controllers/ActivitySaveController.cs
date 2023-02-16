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
    public class ActivitySaveController : Controller
    {
        private readonly ProjectFUENContext _context;

        public ActivitySaveController(ProjectFUENContext context)
        {
            _context = context;
        }

        // Post: api/ActivtiyEnroll/Save
        //會員收藏活動（不需實名制）
        [HttpPost]
        [Route("api/ActivitySave/Save")]
        //會員收藏功能
        public SaveResVM Save(SaveReqDTO saveReq)
        {
            //取得要求資料
            int memberId = saveReq.MemberId;
            int activityId = saveReq.ActivityId;

            //準備回傳資料
            SaveResVM saveRes = new SaveResVM();
            saveRes.result = false;

            var member = _context.Members.Find(memberId);
            var activity = _context.Activities.Find(activityId);

            //會員是否存在
            if (member != null)
            {

                //活動是否存在？
                if (activity != null)//存在
                {
                    //該會員是否收藏過？
                    var isSaveed = _context.ActivityCollections.Where(a => a.ActivityId == activityId).FirstOrDefault(a => a.UserId == memberId);
                    if (isSaveed == null) //沒收藏過（在此活動中的收藏名單無此會員）
                    {
                        //活動是否舉辦？
                        if (activity.GatheringTime > DateTime.Now)//未舉辦（活動舉辦日大於現在）
                        {
                            //報名(新增一筆活動成員)
                            _context.ActivityCollections.Add(saveReq.ToActivityCollectionEntity());
                            _context.SaveChanges();
                            saveRes.message = "收藏成功";
                            saveRes.result = true;

                        }
                        else
                        {
                            saveRes.message = "活動已舉辦";
                        
                        }

                    }//收藏過
                    else
                    {
                        saveRes.message = "會員已收藏過";
                    }


                }
                else //活動不存在
                {
                    saveRes.message = "收藏的活動不存在";
                }

            }
            else
            {
                saveRes.message = "會員不存在";
            }

            return saveRes;
        }

        [HttpDelete]
        [Route("api/ActivitySave/UnSave")]
        public UnSaveResVM UnSave(int activityCollectionId)
        {
            var unsaveRes = new UnSaveResVM();
            unsaveRes.result = false;
            var activityCollection = _context.ActivityCollections.Find(activityCollectionId);

            //判斷是否有報名資料
            if (activityCollection != null)//有該資料
            {
                _context.ActivityCollections.Remove(activityCollection);
                _context.SaveChanges();
                unsaveRes.result = true;
                unsaveRes.message = "取消收藏成功";
            }
            else
            {
                unsaveRes.message = "會員未收藏該活動";
            }
            return unsaveRes;
        }

        [HttpPost]
        [Route("api/ActivitySave/SaveStatus")]
        //前台顯示活動收藏的四種可能情況(不存在/已舉辦/可收藏/已收藏(若沒傳memberId（=0）進來就不會走到這步判斷))
        public SaveStatusResVM SaveStatus(SaveReqDTO saveReq)
        {
            //取得要求資料
            int memberId = saveReq.MemberId;
            int activityId = saveReq.ActivityId;


            var member = _context.Members.Find(memberId);
            var activity = _context.Activities.Find(activityId);

            //準備回傳資料
            SaveStatusResVM saveStatusRes = new SaveStatusResVM();

            //活動是否存在？
            if (activity != null)//存在
            {
                saveStatusRes.ActivityName = activity.ActivityName;
                //活動是否舉辦
                if (activity.GatheringTime > DateTime.Now)//未舉辦
                {

                    saveStatusRes.statusId = 3;
                    saveStatusRes.message = "可收藏";

                    //該會員是否收藏過？
                    var isSaved = _context.ActivityCollections.Where(a => a.ActivityId == activityId).FirstOrDefault(a => a.UserId == memberId);
                    if (member != null && isSaved != null) //會員有存在 且收藏過（在此活動中的收藏名單有此會員）
                    {
                        saveStatusRes.statusId = 4;
                        saveStatusRes.message = "已收藏過";
                        saveStatusRes.UnSaveId = isSaved.Id;
                    }
                }
                else
                {
                    saveStatusRes.statusId = 2;
                    saveStatusRes.message = "活動已舉辦";
                }

                   
                
            }
            else //活動不存在
            {
                saveStatusRes.statusId = 1;
                saveStatusRes.message = "沒有此活動";
            }
            return saveStatusRes;
        }
    }
}

