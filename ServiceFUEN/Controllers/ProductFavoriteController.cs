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
        public void ProductFavorites(int memberId, int productId)
        {
            var member = _context.Members.FirstOrDefault(m => m.Id == memberId);
            var product = _context.Products.FirstOrDefault(m => m.Id == productId);

            favoriteVM fav = new favoriteVM();
            fav.upshot = false;

            if (member != null)
            {
                if (product != null)
                {
                    var ismember = _context.Members.Where(m => m.Id == memberId);
                    var isproduct = _context.Products.Where(p => p.Id == productId);
                    if (ismember != null && isproduct != null)
                    {
                        member.Products.Add(product);

                        _context.SaveChanges();
                        fav.reply = "收藏成功";
                        fav.upshot = true;
                    }
                    else
                    {
                        fav.reply = "商品已收藏過";
                    }
                }
                else
                {
                    fav.reply = "商品不存在";
                }
            }
            fav.reply = "會員不存在";

        }
        //// Post api/Favorites/ProductFavorites
        ////收藏商品
        //[HttpGet]
        //[Route("api/Favorites/ProductFavorites")]
        //public void ProductFavorites(int memberId, int productId)
        //{
        //    var member = _context.Members.FirstOrDefault(m => m.Id == memberId);
        //    var product = _context.Products.FirstOrDefault(m => m.Id == productId);




        //    member.Products.Add(product);

        //    _context.SaveChanges();

        //}
        // Delete api/Favorites/UnFavorites
        //會員取消收藏商品
        //待完成
        [HttpDelete]
        [Route("api/Favorites/UnFavorites")]
        public void UnFavorite(int memberId, int productId)
        {
 
            var member = _context.Members.Include(p => p.Products).FirstOrDefault(m => m.Id == memberId);

            var product = member.Products.FirstOrDefault(p => p.Id == productId);

            member.Products.Remove(product);
            _context.SaveChanges();

        }

        // Get api/Favorites/FavoritesAll
        //會員查看已收藏商品
        //待完成

        [HttpGet]
        [Route("api/Favorites/FavoritesAll")]
        public void FavoritesAll(int memberId)
        {
            var member = _context.Members
                .Include(p => p.Products)
                .FirstOrDefault(m => m.Id == memberId);
            

           member.Products.ToList();
           
        }
    }
}
