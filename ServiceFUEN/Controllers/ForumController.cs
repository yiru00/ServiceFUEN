﻿using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ServiceFUEN.Models.DTOs;
using ServiceFUEN.Models.EFModels;
using ServiceFUEN.Models.ViewModels;
using System.Diagnostics;
using System.Text.RegularExpressions;

namespace ServiceFUEN.Controllers
{
    [EnableCors("AllowAny")]
    [ApiController]
    public class ForumController : Controller
    {
        private readonly ProjectFUENContext _context;

        public ForumController(ProjectFUENContext context)
        {
            _context = context;
        }

        //顯示文章留言
        [HttpGet]
        [Route("api/Article/ArticleComment")]
        public IEnumerable<MessageVM> ArticleComment(int messageId)
        {
            var projectFUENContext = _context.Messages
                .Where(a => a.Id == messageId)
                .Select(a => new MessageVM()
                {
                    Id= a.Id,
                    Content = a.Content,
                    Time = a.Time,
                    MemberId = a.MemberId,
                    ArticleId = a.Article.Id,
                    NickName = a.Member.NickName,
                });

            return projectFUENContext;
        }

        //顯示選取的文章內容
        [HttpGet]
        [Route("api/Article/ArticleDetails")]
        public IEnumerable<ArticleListVM> ArticleDetails(int ArticleId)
        {
            var projectFUENContext = _context.Articles
                .Where(a => a.Id == ArticleId)
                .Select(a => new ArticleListVM()
                {
                    ArticleId = a.Id,
                    Title = a.Title,
                    Time = a.Time,
                    MemberId = a.MemberId,
                    NickName = a.Member.NickName,
                    ForumId = a.ForumId,
                    ForumName = a.Forum.Name,
                });

            return projectFUENContext;
        }


        // 全部最新文章(依照新到舊時間)
        [HttpGet]
        [Route("api/Article/LatestArticle")]
        public IEnumerable<ArticleListVM> LatestArticle()
        {
            var projectFUENContext = _context.Articles
               .Include(a => a.Member).Include(a => a.Forum).Include(a => a.Messages).OrderByDescending(a=>a.Time).Select(a => new ArticleListVM
               {
                   ArticleId = a.Id,
                   MemberId = a.MemberId,
                   Title = a.Title,
                   ForumId = a.ForumId,
                   Time = a.Time,
                   NickName = a.Member.NickName,
                   ForumName = a.Forum.Name,
               })
          .ToList();
            return projectFUENContext;

        }

        //某看板最新文章(依照新到舊時間)
        [HttpGet]
        [Route("api/Article/ForumLatestArticle")]
        public IEnumerable<ArticleListVM> ForumLatestArticle(int ForumId)
        {
            var projectFUENContext = _context.Articles
               .Include(a => a.Member).Include(a => a.Forum).Include(a => a.Messages).Where(a => a.ForumId == ForumId).OrderByDescending(a => a.Time).Select(a => new ArticleListVM
               {
                   ArticleId = a.Id,
                   MemberId = a.MemberId,
                   Title = a.Title,
                   ForumId = a.ForumId,
                   Time = a.Time,
                   NickName = a.Member.NickName,
                   ForumName = a.Forum.Name,
               })
          .ToList();
            return projectFUENContext;

        }

        // 全部最熱門文章(依照留言數)
        [HttpGet]
        [Route("api/Article/PopularArticle")]
        public IEnumerable<ArticleListVM> PopularArticle()
        {
            var projectFUENContext = _context.Articles
                .Include(a => a.Member).Include(a => a.Forum).Include(a => a.Messages).OrderByDescending(x => x.Messages.Count).Select(a => new ArticleListVM
                {
                    ArticleId = a.Id,
                    MemberId= a.MemberId,
                    Title= a.Title,
                    ForumId= a.ForumId,
                    Time = a.Time,
                    NickName = a.Member.NickName,
                    ForumName = a.Forum.Name,
                })
           .ToList();
            return projectFUENContext;
        }

        //某看板最熱門文章(依照留言數)
        [HttpGet]
        [Route("api/Article/ForumPopularArticle")]
        public IEnumerable<ArticleListVM> ForumPopularArticle(int ForumId)
        {
            var projectFUENContext = _context.Articles
                .Include(a => a.Member).Include(a => a.Forum).Include(a => a.Messages).Where(a => a.ForumId==ForumId).OrderByDescending(x => x.Messages.Count).Select(a => new ArticleListVM
                {
                    ArticleId = a.Id,
                    MemberId = a.MemberId,
                    Title = a.Title,
                    ForumId = a.ForumId,
                    Time = a.Time,
                    NickName = a.Member.NickName,
                    ForumName = a.Forum.Name,
                })
           .ToList();
            return projectFUENContext;
        }

        // 找某看板的文章
        [HttpGet]
        [Route("api/Article/ForumArticle")]
        public IEnumerable<ArticleListVM> ForumArticle(int ForumId)
        {
            var projectFUENContext = _context.Articles
                .Include(a => a.Member).Include(a => a.Forum).Include(a => a.Messages).Where(a=>a.ForumId==ForumId).OrderByDescending(a=>a.Time).Select(a => new ArticleListVM
                {
                    ArticleId = a.Id,
                    MemberId = a.MemberId,
                    Title = a.Title,
                    ForumId = a.ForumId,
                    Time = a.Time,
                    NickName = a.Member.NickName,
                    ForumName = a.Forum.Name,
                })
           .ToList();
            return projectFUENContext;
        }
        
        //Search文章標題
        [HttpGet]
        [Route("api/Article/Search")]
        public IEnumerable<ArticleListVM> Search(string? title)
        {
            IEnumerable<Article> projectFUENContext = _context.Articles.Include(a => a.Member).Include(a => a.Forum).Include(a => a.Messages);
            if (!string.IsNullOrEmpty(title))
            {
                projectFUENContext =
                projectFUENContext.Where(a => a.Title.Contains(title));
            }
            var result = projectFUENContext.Select(a => new ArticleListVM()
            {
                ArticleId = a.Id,
                MemberId = a.MemberId,
                Title = a.Title,
                ForumId = a.ForumId,
                Time = a.Time,
                NickName = a.Member.NickName,
                ForumName = a.Forum.Name,
            });
            return result;
        }

        //全部看板
        [HttpGet]
        [Route("api/Forum/ForumAll")]
        public IEnumerable<ForumAllVM> ForumAll()
        {
            var projectFUENContext = _context.Forums.Select(a => new ForumAllVM()
            {
                Id = a.Id,
                Name = a.Name,
            });

            return projectFUENContext;
        }

        //看板詳細
        [HttpGet]
        [Route("api/Forum/ForumDetails")]
        public IEnumerable<ForumDetailVM> ForumDetails(int ForumId)
        {
            var projectFUENContext = _context.Forums
                .Where(a => a.Id == ForumId)
                .Select(a => new ForumDetailVM()
                    {
                        Id = a.Id,
                        Name = a.Name,
                        About = a.About,
                        CoverPhoto = a.CoverPhoto,
                    });

            return projectFUENContext;
        }


        //新增留言(完成不確定)
        //沒有驗證文章存不存在
        [HttpPost]
        [Route("api/Message/CreateComment")]
        public void CreateComment(MessageDto msg)
        {
            Message message = msg.VMToEntity();
            _context.Messages.Add(message);
            _context.SaveChanges();
        }

        //新增文章
        [HttpPost]
        [Route("api/Article/CreateArticle")]
        public void CreateArticle([FromForm]CreateArticleVM articleVM)
        {
            List<ArticlePhoto> photos = new List<ArticlePhoto>();

            foreach (var file in articleVM.Files)
            {
                // 移進資料夾
                string path = System.Environment.CurrentDirectory + "/Images/";
                string extension = Path.GetExtension(file.FileName);
                string fileName = Guid.NewGuid().ToString("N");
                string fullName = fileName + extension;
                string fullPath = Path.Combine(path, fullName);
                using (var stream = System.IO.File.Create(fullPath))
                {
                    file.CopyTo(stream);
                }

                // GUID檔名加進List
                ArticlePhoto photo = new ArticlePhoto()
                {
                    Photo = fullName,
                };
                photos.Add(photo);
            }

            Article article = new Article();
            article.MemberId = articleVM.MemberId;
            article.Title = articleVM.Title;
            article.Content = articleVM.Content;
            article.ArticlePhotos = photos;
            article.ForumId = articleVM.ForumId;
            //foreach(string item in articleVM.Photos)
            //{
 
            //    //article.ArticlePhotos.Add(photo); 一樣
            //}
            //article.ArticlePhotos = photos;

            _context.Articles.Add(article);
            _context.SaveChanges();
        }

       


        //修改文章
        [HttpPut]
        [Route("api/Article/EditArticle/{id}")]
        public void EditArticle(int id,[FromForm] CreateArticleVM articleVM)
        {

            List<ArticlePhoto> photos = new List<ArticlePhoto>();

            foreach (var file in articleVM.Files)
            {
                // 移進資料夾
                string path = System.Environment.CurrentDirectory + "/Images/";
                string extension = Path.GetExtension(file.FileName);
                string fileName = Guid.NewGuid().ToString("N");
                string fullName = fileName + extension;
                string fullPath = Path.Combine(path, fullName);
                using (var stream = System.IO.File.Create(fullPath))
                {
                    file.CopyTo(stream);
                }

                // GUID檔名加進List
                ArticlePhoto photo = new ArticlePhoto()
                {
                    Photo = fullName,
                };
                photos.Add(photo);
            }

            var article = _context.Articles.FirstOrDefault(a => a.Id == id);
            article.MemberId = articleVM.MemberId;
            article.Title = articleVM.Title;
            article.Content = articleVM.Content;
            article.ForumId = articleVM.ForumId;
            article.ArticlePhotos = photos;

            _context.Articles.Update(article);
            _context.SaveChanges();
        }
        //刪除文章
        [HttpDelete]
        [Route("api/Article/DeleteArticle")]
        public void DeleteArticle(int articleid)
        {
            var article = _context.Articles.FirstOrDefault(a => a.Id == articleid);
            _context.Articles.Remove(article);
            _context.SaveChanges();
        }

        //刪除文章留言
        [HttpDelete]
        [Route("api/Message/DeleteComment")]
        public void DeleteComment(int messageid)
        {
            var message = _context.Messages.FirstOrDefault(a => a.Id == messageid);
            _context.Messages.Remove(message);
            _context.SaveChanges();
        }

        
    }
}
