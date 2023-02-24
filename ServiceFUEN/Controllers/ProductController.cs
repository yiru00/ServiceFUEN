using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ServiceFUEN.Models.EFModels;
using ServiceFUEN.Models.ViewModels;
using System.Diagnostics;
using System.Linq;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using static Microsoft.Extensions.Logging.EventSource.LoggingEventSource;

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
        // Get api/Product/DetailProducts
        //取得商品所有資訊
        [HttpGet]
        [Route("api/Product/DetailProducts")]
        public IEnumerable<ProductDetailVM> DetailProducts(int id)
        {
            var projectFUENContext = _context.Products

           .Include(x => x.ProductPhotos)
           .Where(x => x.Id == id)
            .Select(p => p.ToProductDetailVM());
            return projectFUENContext.ToList();
        }
        // Get api/Product/AllProducts
        //取得商品圖、品名、價格
        [HttpGet]
        [Route("api/Product/AllProducts")]
        public IEnumerable<ProductAllVM> AllProducts()
        {
            var projectFUENContext = _context.Products

           .Include(x => x.ProductPhotos)
            .Select(p => p.ToProductAllVM());
            return projectFUENContext.ToList();
        }
        // Get api/Product/NewProducts
        //取得新上架前十樣商品的商品圖、品名、價格
        [HttpGet]
        [Route("api/Product/NewProducts")]
        public IEnumerable<ProductNewVM> NewProducts()
        {
            var projectFUENContext = _context.Products

           .Include(x => x.ProductPhotos)
            .Select(p => p.ToProductNewVM()).ToList()
            .OrderByDescending(p => p.ReleaseDate).Take(10);
            return projectFUENContext;
        }

        // Get api/Category/Categorylist
        //類別下拉選單
        [HttpGet]
        [Route("api/Category/Categorylist")]
        public IEnumerable<CategoryVM> Categorylist()
        {
            var projectFUENContext = _context.Categories
             .Select(p => p.ToCategoryVM());
            return projectFUENContext.ToList();
        }

        // Get api/Category/Categorylist
        //類別下拉選單
        [HttpGet]
        [Route("api/Brands/Brandlist")]
        public IEnumerable<BrandVM> Brandlist()
        {
            var projectFUENContext = _context.Brands
             .Select(p => p.ToBrandVM());
            return projectFUENContext.ToList();
        }

        // Get api/Product/Search
        //依搜尋條件搜尋商品，包含商品圖、品名、價格
        [HttpGet]
       [Route("api/Product/Search")]

        public IEnumerable<ProductSearchDTO> Search(string? name, int? categoryId, int? brandId)
        {
            IEnumerable<Product> projectFUENContext = _context.Products;

            if (!string.IsNullOrEmpty(name))
            {
                projectFUENContext =
                projectFUENContext.Where(p => p.Name.ToLower().Contains(name.ToLower()));
            }
            if (categoryId != null && categoryId != 0)
            {
                projectFUENContext =
               projectFUENContext.Where(a => a.CategoryId == categoryId);

            }
            if (brandId != null && brandId != 0)
            {
                projectFUENContext =
               projectFUENContext.Where(a => a.BrandId == brandId);
            }

            return projectFUENContext.Select(p => p.ToProductSearchDTO()).ToList();
        }
      
        // Get api/Event/Eventphotos
        //顯示活動圖（首頁輪播圖要用）
        [HttpGet]
        [Route("api/Event/Eventphotos")]
        public IEnumerable<EventVM> Eventphotos()
        {
            var projectFUENContext = _context.Events
                .Select(e => e.ToEventVM());
            return projectFUENContext.ToList();
        }


    }
}
