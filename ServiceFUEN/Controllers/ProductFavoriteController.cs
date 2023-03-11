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

        //[HttpPost]
        //[Route("api/ProductSave/SaveFav")]
        ////前台顯示活動收藏的四種可能情況(不存在/已舉辦/可收藏/已收藏(若沒傳memberId（=0）進來就不會走到這步判斷))
        //public  SaveStatus(int memberId, int productId)
        //{
        //    //取得要求資料
        //    int memberId = saveReq.MemberId;
        //    int activityId = saveReq.ActivityId;


        //    var member = _context.Members.Find(memberId);
        //    var activity = _context.Activities.Find(activityId);

        //    //準備回傳資料
        //    SaveStatusResVM saveStatusRes = new SaveStatusResVM();

        //    //活動是否存在？
        //    if (activity != null)//存在
        //    {
        //        saveStatusRes.ActivityName = activity.ActivityName;
        //        //活動是否舉辦
        //        if (activity.GatheringTime > DateTime.Now)//未舉辦
        //        {

        //            saveStatusRes.statusId = 3;
        //            saveStatusRes.message = "可收藏";
        //            saveStatusRes.activityId = activityId;

        //            //該會員是否收藏過？
        //            var isSaved = _context.ActivityCollections.Where(a => a.ActivityId == activityId).FirstOrDefault(a => a.UserId == memberId);
        //            if (member != null && isSaved != null) //會員有存在 且收藏過（在此活動中的收藏名單有此會員）
        //            {
        //                saveStatusRes.statusId = 4;
        //                saveStatusRes.message = "已收藏過";
        //                saveStatusRes.UnSaveId = isSaved.Id;
        //            }
        //        }
        //        else
        //        {
        //            saveStatusRes.statusId = 2;
        //            saveStatusRes.message = "活動已舉辦";

        //            //該會員是否收藏過？
        //            var isSaved = _context.ActivityCollections.Where(a => a.ActivityId == activityId).FirstOrDefault(a => a.UserId == memberId);
        //            if (member != null && isSaved != null) //會員有存在 且收藏過（在此活動中的收藏名單有此會員）
        //            {
        //                saveStatusRes.statusId = 5;
        //                saveStatusRes.message = "活動已舉辦且已收藏過";
        //                saveStatusRes.UnSaveId = isSaved.Id;
        //            }
        //        }



        //    }
        //    else //活動不存在
        //    {
        //        saveStatusRes.statusId = 1;
        //        saveStatusRes.message = "沒有此活動";
        //    }
        //    return saveStatusRes;
        //}
    }
}
