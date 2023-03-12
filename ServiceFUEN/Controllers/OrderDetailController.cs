﻿using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServiceFUEN.Models.EFModels;
using ServiceFUEN.Models.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace ServiceFUEN.Controllers
{
    [Route("api/[controller]")]
    [EnableCors("AllowAny")]
    [ApiController]
    public class OrderDetailController : ControllerBase
    {
        private readonly ProjectFUENContext _context;


        public OrderDetailController(ProjectFUENContext context)
        {
            _context = context;
        }

        [HttpGet("GetMemberOrder")]
        [AllowAnonymous]
         public async Task<IActionResult> GetMemberOrder(int memberid)
        {
            //var orderid = _context.OrderDetails.Where(x => x.MemberId == memberid).Select(x => x.Id).FirstOrDefault();
            //var qq = _context.OrderDetails.Where(x => x.MemberId == memberid);
            var memberorder = _context.OrderDetails.Where(x => x.MemberId == memberid).OrderByDescending(x=>x.OrderDate).ToList();

            return Ok(memberorder); 
  
        }

        [HttpGet("GetOrderID")]
        [AllowAnonymous]
        public async Task<IActionResult> GetOrderID(int orderid)
        {
            //var orderid = _context.OrderDetails.Where(x => x.MemberId == memberid).Select(x => x.Id).FirstOrDefault();
            //var qq = _context.OrderDetails.Where(x => x.MemberId == memberid);
            var memberorderitems = _context.OrderItems.Where(x => x.OrderId == orderid);

            return Ok(memberorderitems);

        }


    }
}
