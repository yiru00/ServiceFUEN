using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServiceFUEN.Models.DTOs;
using ServiceFUEN.Models.EFModels;

namespace ServiceFUEN.Controllers
{
    [EnableCors("AllowAny")]
    [ApiController]
    public class TagController : Controller
    {
		private readonly ProjectFUENContext _dbContext;
		public TagController(ProjectFUENContext dbContext)
		{
			_dbContext = dbContext;
		}

        // 是否寫在Photo Create一起新增???
		[Route("api/Tag/Create")]
        [HttpPost]
        public void Create([FromForm]IEnumerable<string> names)
        {
            List<Tag> tags = new List<Tag>();

            foreach (var name in names)
            {
                // name是否在DB當中有重複的
                var tag = _dbContext.Tags.FirstOrDefault(t => t.Name == name);
                if(tag == null)
                {
                    Tag entity = new Tag()
                    {
                        Name = name,
                    };
                    tags.Add(entity);
                }
            }

            _dbContext.Tags.AddRange(tags);
            _dbContext.SaveChanges();
        }

        [Route("api/Tag/Search")]
        [HttpGet]
        public IEnumerable<TagDTO> Search(string input)
        {
            return _dbContext.Tags
                .Where(t => t.Name.ToLower().StartsWith(input.ToLower()))
                .Select(t => new TagDTO()
                {
                    Id = t.Id,
                    Name = t.Name,
                });
        }
    }
}
