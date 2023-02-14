using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ServiceFUEN.Models.EFModels;
using ServiceFUEN.Models.ViewModels;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ServiceFUEN.Controllers
{

    [EnableCors("AllowAny")]
    //[Route("api/[controller]")]
   // [ApiController]
    public class ActivityController : Controller
    {
        private readonly ProjectFUENContext _context;

        public ActivityController(ProjectFUENContext context)
        {
            _context = context;
        }

        // GET: api/Activity/NewActivity
        [HttpGet]
        [Route("api/Activity/NewActivity")]
        public IEnumerable<ActivityVM> NewActivity()
        {
            var projectFUENContext = _context.Activities.Include(a=>a.Category).Include(a => a.ActivityMembers).Include(a => a.ActivityCollections).OrderBy(a=>a.DateOfCreated).Select(a=>a.toActivityVM());

            return projectFUENContext.ToList();
        }

        // GET api/Activity/PopularActivity
        [HttpGet]
        [Route("api/Activity/PopularActivity")]
        public ActivityVM PopularActivity()
        {
            
        }

        // GET api/Activity/Search
        [HttpGet]
        [Route("api/Activity/Search")]
        public ActivityVM Search(string activity,string address,DateTime time)
        {
            
        }

        // GET api/Activity/Details
        [HttpGet]
        [Route("api/Activity/Details")]
        public ActivityCategoryVM Details(int activityId)
        {
            
        }

        // GET api/Activity/Details
        [HttpGet]
        [Route("api/Activity/Category")]
        public ActivityCategoryVM Category()
        {
            
        }
    }
}

