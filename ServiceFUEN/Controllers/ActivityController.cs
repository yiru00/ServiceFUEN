using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ServiceFUEN.Models.DTOs;
using ServiceFUEN.Models.EFModels;
using ServiceFUEN.Models.ViewModels;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ServiceFUEN.Controllers
{

    [EnableCors("AllowAny")]
    [ApiController]
    public class ActivityController : Controller
    {
        private readonly ProjectFUENContext _context;

        public ActivityController(ProjectFUENContext context)
        {
            _context = context;
        }

        // GET: api/Activity/New
        //取得所有未舉辦的活動（集合日期大於現在），並按照活動建立日期大到小排序
        [HttpGet]
        [Route("api/Activity/New")]
        public IEnumerable<ActivityVM> New()
        {
            var projectFUENContext = _context.Activities
                .Include(a => a.Category)
                .Include(a => a.ActivityMembers)
                .Include(a=>a.ActivityCollections)
                .Include(a=>a.Instructor)
                .Where(a=>a.GatheringTime>DateTime.Now)
                .OrderByDescending(a=>a.DateOfCreated).Select(a=>a.ToActivityVM());

            return projectFUENContext.ToList();
        }

        // GET api/Activity/Popular
        //取得所有未舉辦的活動（集合日期大於現在），並按照報名率大到小、收藏數排序大到小
        [HttpGet]
        [Route("api/Activity/Popular")]
        public IEnumerable<ActivityVM> Popular()
        {

            var projectFUENContext = _context.Activities
                .Include(a => a.Category)
                .Include(a => a.ActivityMembers)
                .Include(a => a.ActivityCollections)
                 .Include(a => a.Instructor)
                .Where(a => a.GatheringTime > DateTime.Now)
                .Select(a => a.ToActivityVM()).ToList() //IQueryable查詢字串的條件在ToList()後才會到資料庫撈
                .OrderByDescending(a => a.EnrolmentRate).ThenByDescending(a => a.NumOfCollections); //orderby是IEnumerable的擴充方法//前面不ToList() orderby就會是IQueryable的擴充方法=>查不到vm裡自訂的欄位
            return projectFUENContext.ToList();

        }

        // GET api/Activity/WillBeHeld
        //取得所有即將舉辦的（未舉辦）活動  集合日期小到大
        [HttpGet]
        [Route("api/Activity/WillBeHeld")]
        public IEnumerable<ActivityVM> WillBeHeld()
        {

            var projectFUENContext = _context.Activities
                .Include(a => a.Category)
                .Include(a => a.ActivityMembers)
                .Include(a => a.ActivityCollections)
                 .Include(a => a.Instructor)
                .Where(a => a.GatheringTime > DateTime.Now)
                .OrderBy(a => a.GatheringTime)
                .Select(a => a.ToActivityVM());
                
            return projectFUENContext.ToList();

        }

        // post api/Activity/SameCategory
        //取得某分類未截止活動，按收藏數大到小排
        [HttpPost]
        [Route("api/Activity/SameCategory")]
        public IEnumerable<ActivityResVM> SameCategory(SameCategoryReqDTO sameCategoryReq)
        {
            int memberId = sameCategoryReq.memberId;
            int categoryId = sameCategoryReq.categoryId;
            int activityId = sameCategoryReq.activityId;
            IEnumerable<ActivityResVM> activityResVM = new List<ActivityResVM>();

            var projectFUENContext = _context.Activities
                .Include(a => a.Category)
                .Include(a => a.ActivityMembers)
                .Include(a => a.ActivityCollections)
                 .Include(a => a.Instructor)
                .Where(a => a.Deadline > DateTime.Now)
                .Where(a => a.CategoryId == categoryId)
                .Where(a=>a.Id!=activityId)
                .Select(a => a.ToActivityResVM()).ToList()
                .OrderByDescending(a=>a.NumOfCollections);

            activityResVM = projectFUENContext;
            foreach (ActivityResVM vm in activityResVM)
            {
                var isSaved = _context.ActivityCollections.Where(a => a.ActivityId == vm.ActivityId).FirstOrDefault(a => a.UserId == memberId);
                if (memberId != 0 && isSaved != null) //有會員 且收藏過（在此活動中的收藏名單有此會員）
                {
                    vm.statusId = 4;
                    vm.message = "已收藏過";
                    vm.UnSaveId = isSaved.Id;
                }
            }

            return projectFUENContext.ToList();

        }
        // GET api/Activity/Search
        //依照搜尋條件取得所有未舉辦的活動 按活動集合日期小到大排
        [HttpGet]
        [Route("api/Activity/Search")]
        public IEnumerable<ActivityResVM> Search(string? activityName,int? categoryId,string? address,DateTime? time,int memberId)
        {
            var now = DateTime.Now;
            if (time!=null&&time>now)//沒傳入日期或傳入日期小於當下 一律以當下為準
            {
                now = (DateTime)time;
            }
            IEnumerable<ActivityResVM> activityResVM = new List<ActivityResVM>();

            var projectFUENContext = _context.Activities
                .Include(a => a.Category)
                .Include(a => a.ActivityMembers)
                .Include(a => a.ActivityCollections)
                 .Include(a => a.Instructor)
                .Where(a => a.GatheringTime > now); //前端預設一定是大於今天（now）
            

            if (!string.IsNullOrEmpty(activityName))
            {
                projectFUENContext=
                projectFUENContext.Where(a => a.ActivityName.Contains(activityName));
            }
            if (!string.IsNullOrEmpty(address))
            {
                projectFUENContext =
               projectFUENContext.Where(a => a.Address.Contains(address));
            }
            if (categoryId !=null&&categoryId!=0)//數字沒值預設是0
            {
                projectFUENContext =
               projectFUENContext.Where(a => a.CategoryId==categoryId);
            }

            activityResVM = projectFUENContext.OrderBy(a => a.GatheringTime).Select(a => a.ToActivityResVM()).ToList();
            

            foreach(ActivityResVM vm in activityResVM)
            {
                var isSaved = _context.ActivityCollections.Where(a => a.ActivityId == vm.ActivityId).FirstOrDefault(a => a.UserId == memberId);
                if (memberId !=0 && isSaved != null) //有會員 且收藏過（在此活動中的收藏名單有此會員）
                {
                    vm.statusId = 4;
                    vm.message = "已收藏過";
                    vm.UnSaveId = isSaved.Id;
                 
                }
            }

            return activityResVM;
            //return projectFUENContext.Select(a => a.ToActivityVM()).ToList().OrderBy(a => a.GatheringTime);
        }

        //GET api/Activity/Details
        //搜尋某個活動//找不到會回傳所有欄位都是null
        [HttpGet]
        [Route("api/Activity/Details")]
         public ActivityDetailsVM Details(int activityId,int categoryId)
        {
            var projectFUENContext = _context.Activities
                .Include(a => a.Category)
                .Include(a => a.ActivityMembers)
                .Include(a => a.Instructor)
                .Include(a => a.ActivityCollections)
                .FirstOrDefault(a => a.Id == activityId).ToActivityDetailsVM();
            return projectFUENContext;
        }

        // GET api/Activity/Category
        //取得活動類型分類，照自訂順序排列
        [HttpGet]
        [Route("api/Activity/Category")]
        public IEnumerable<ActivityCategoryVM> Category()
        {
            var projectFUENContext = _context.ActivityCategories.OrderBy(a=>a.DisplayOrder).Select(a=>a.ToActivityCategoryVM());
            return projectFUENContext.ToList();
        }
    }
}

