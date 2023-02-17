using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ServiceFUEN.Models.EFModels;
using ServiceFUEN.Models.ViewModels;
using System.Diagnostics;

namespace ServiceFUEN.Controllers
{
    [EnableCors("AllowAny")]
    [ApiController]
    public class ProductController : Controller
    {
        private readonly ProjectFUENContext _context;
        public ProductController(ProjectFUENContext context)
        {
            _context = context;
        }
        [HttpGet]
        [Route("api/Product/DetailProducts")]
        public IEnumerable<ProductDetailVM> DetailProducts()
        {
            var projectFUENContext = _context.Products
            //.Include(x=>x.ProductPhotos)
            .Select(p => p.ToProductDetailVM());
            return projectFUENContext.ToList();
          

        }
        [HttpGet]
        [Route("api/Activity/New")]
        public IEnumerable<ActivityVM> New()
        {
            var projectFUENContext = _context.Activities
                .Include(a => a.Category)
                .Include(a => a.ActivityMembers)
                .Include(a => a.ActivityCollections)
                .Where(a => a.GatheringTime > DateTime.Now)
                .OrderByDescending(a => a.DateOfCreated).Select(a => a.ToActivityVM());

            return projectFUENContext.ToList();
        }

        //[HttpGet]
        //[Route("api/Product/AllProduct")]
        //public IEnumerable<ProductExts> AllProduct(int id) 
        //{
        //    var projectFUENContext = _context.Products
        //        .Select(p => new ProductVM
        //        {
        //            Id = p.Id,
        //            Name = p.Name,
        //            Price = p.Price,
        //            ProductPhotos = p.ProductPhotos,
        //            ProductSpec = p.ProductSpec,
        //            ReleaseDate = p.ReleaseDate,
        //            ManufactorDate = p.ManufactorDate,
        //            Inventory = p.Inventory
        //        }).ToList();
        //    return projectFUENContext;
        //}
    }
}
