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
    public class ActivityRecordController : Controller
    {
        private readonly ProjectFUENContext _context;

        public ActivityRecordController(ProjectFUENContext context)
        {
            _context = context;
        }

        //會員已報名未舉辦的活動 按報名時間大到小
        [HttpPost]
        [Route("api/ActivityRecord/Enrolled")]
        public IEnumerable<EnrollRecordResVM> Enrolled(RecordReqDTO req) {

            int memberId = req.memberId;

            var projectFUENContext = _context.ActivityMembers
                .Include(a=>a.Activity)
                .Where(a=>a.MemberId==memberId)
                .Where(a => a.Activity.GatheringTime > DateTime.Now)
                .OrderBy(a => a.DateJoined).Select(a => a.ToEnrollRecordResVM());

            return projectFUENContext.ToList();
        }

        //會員已參加的活動（已報名且已舉辦）
        [HttpPost]
        [Route("api/ActivityRecord/Joined")]
        public IEnumerable<EnrollRecordResVM> Joined(RecordReqDTO req) {

            int memberId = req.memberId;

            var projectFUENContext = _context.ActivityMembers
                .Include(a => a.Activity)
                .Where(a => a.MemberId == memberId)
                .Where(a => a.Activity.GatheringTime < DateTime.Now)
                .OrderBy(a => a.DateJoined).Select(a => a.ToEnrollRecordResVM());

            return projectFUENContext.ToList();

        }


        //會員已收藏的未舉辦活動
        [HttpPost]
        [Route("api/ActivityRecord/Saved")]
        public IEnumerable<SaveRecordResVM> Saved(RecordReqDTO req) {

            int memberId = req.memberId;

            var projectFUENContext = _context.ActivityCollections
                .Include(a => a.Activity)
                .Where(a => a.UserId == memberId)
                .Where(a => a.Activity.GatheringTime > DateTime.Now)
                .OrderBy(a => a.DateCreated).Select(a => a.ToSaveRecordResVM());

            return projectFUENContext.ToList();
        }
    }
}

