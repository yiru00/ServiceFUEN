using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ServiceFUEN.Models.EFModels;
using ServiceFUEN.Models.Infrastructures.ExtensionMethods;
using ServiceFUEN.Models.ViewModels;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ServiceFUEN.Controllers
{
    [EnableCors("AllowAny")]
    [ApiController]
    public class QnAController : Controller
    {
        private readonly ProjectFUENContext _context;

        public QnAController(ProjectFUENContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("api/QnA/Get")]
        public IEnumerable<QnAResVM> Get(int activityId)
        {
            var projectFUENContext = _context.Questions
                .Include(a => a.Answers)
                .Include(a => a.Member).Where(a=>a.ActivityId==activityId)
                .Select(a => a.ToQnAResVM()).ToList()
                .OrderByDescending(a => a.QDateCreated);
            return projectFUENContext.ToList();
        }


    }
}

