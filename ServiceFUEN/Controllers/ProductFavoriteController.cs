using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ServiceFUEN.Models.EFModels;
using ServiceFUEN.Models.ViewModels;
using System.Linq;

namespace ServiceFUEN.Controllers
{
    [EnableCors("AllowAny")]
    [ApiController]
    public class ProductFavoriteController : Controller
    {
        private readonly ProjectFUENContext _context;

        public ProductFavoriteController(ProjectFUENContext context)
        {
            _context = context;
        }
        // Post api/Favorites/ProductFavorites
        //會員收藏商品
        [HttpPost]
        [Route("api/Favorites/ProductFavorites")]
        public favoriteVM ProductFavorites(int memberId, int productId)
        {
            var result = new favoriteVM();
            result.deleteId= 0;

            var member = _context.Members.FirstOrDefault(m => m.Id == memberId);
            if (member == null) {
                result.reply = "沒有此會員";
                result.upshot = false;
                return result;

            }
            var product = _context.Products.FirstOrDefault(m => m.Id == productId);

            if(product == null) {
                result.reply = "沒有此商品";
                result.upshot = false;
                return result;
            }
            var fav = new Favorite()
            {
                MemberId = memberId,
                ProductId = productId
            };

            _context.Favorites.Add(fav);
            _context.SaveChanges();

            result.reply = "收藏成功";
            result.upshot = true;
           result.deleteId=  _context.Favorites.Where(x=>x.ProductId== productId).FirstOrDefault(x=>x.MemberId==memberId).Id ;
            return result;

        }

        // Delete api/Favorites/UnFavorites
        //會員取消收藏商品
        //待完成
        [HttpDelete]
        [Route("api/favorites/unfavorites/{deleteId}")]
        public bool unfavorite(int deleteId)

        {
            var favorite = _context.Favorites.Find(deleteId);
            if (favorite == null)
            {
                return false;
            }
            _context.Favorites.Remove(favorite);
            _context.SaveChanges();
            return true;

        }

        // Get api/Favorites/FavoritesAll
        //會員查看已收藏商品
        //待完成

        [HttpGet]
        [Route("api/Favorites/FavoritesAll")]
        public List<ProFavoriteVM> FavoritesAll(int memberId)
        {
            var ProFavoriteVM = _context.Favorites
            
                .Include(x => x.Product)
                .Where(x => x.MemberId==memberId)
                .Select(x => x.ToProFavoriteVM())
            .ToList();

            foreach (ProFavoriteVM vm in ProFavoriteVM)
            {
                var photo = _context.ProductPhotos.Where(x => x.ProductId == vm.Id).Select(x => x.Source).Where(x => x.Substring(0, 2) == "01").ToList()[0];
                vm.Source = photo;
            }

            
            return ProFavoriteVM;

        }

        [HttpGet]
        [Route("api/Favorites/FavoritesStatus")]
        public favoriteVM FavoritesStatus(int memberId ,int productId)
        {
            favoriteVM  result = new favoriteVM();
         
            var favorited = _context.Favorites.Where(x => x.MemberId == memberId).FirstOrDefault(x => x.ProductId == productId);
            if (memberId ==0 || favorited == null)
            {
                result.deleteId = 0;
                result.reply = "沒收藏過";
                result.upshot = false;
                return result;
            }
            result.deleteId = favorited.Id;
            result.reply = "已收藏過";
            result.upshot = true;
            return result;

        }
    }
}
