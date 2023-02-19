using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServiceFUEN.Models.DTOs;
using ServiceFUEN.Models.EFModels;

namespace ServiceFUEN.Controllers
{
    [EnableCors("AllowAny")]
    [ApiController]
    public class CommentController : Controller
    {
		private readonly ProjectFUENContext _dbContext;
		public CommentController(ProjectFUENContext dbContext)
		{
			_dbContext = dbContext;
		}

		[Route("api/Comment/Create")]
        [HttpPost]
        public void Create(CreateCommentDTO dto)
        {
            Comment comment = dto.DtoToEntity();
            _dbContext.Comments.Add(comment);
            _dbContext.SaveChanges();
        }

        [Route("api/Comment/Edit")]
        [HttpPut]
        public void Edit(int id, string content)
        {
            var comment = _dbContext.Comments.FirstOrDefault(c => c.Id == id);
            comment.Content = content;

            _dbContext.SaveChanges();
        }

        [Route("api/Comment/Delete")]
        [HttpDelete]
        public void Delete(int id)
        {
			var comment = _dbContext.Comments.FirstOrDefault(c => c.Id == id);
            _dbContext.Comments.Remove(comment);
            _dbContext.SaveChanges();
		}
    }
}
