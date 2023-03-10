using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServiceFUEN.Models.DTOs;
using ServiceFUEN.Models.EFModels;

namespace ServiceFUEN.Controllers
{
    [EnableCors("AllowAny")]
    [ApiController]
    public class ProfileController : Controller
    {
        private readonly ProjectFUENContext _dbContext;
        public ProfileController(ProjectFUENContext dbContext)
        {
            _dbContext = dbContext;
        }

        [Route("api/Profile/GetAllMember")]
        public IEnumerable<CommunityMemberDTO> GetAllMember(string searchInput)
        {
            IEnumerable<Member> members = _dbContext.Members;

            if (!string.IsNullOrEmpty(searchInput)) 
            { 
                members = members.Where(x => x.NickName.Contains(searchInput));
            }

            return members.Select(x => new CommunityMemberDTO()
            {
                Id = x.Id,
                Name = x.NickName,
                Source = x.PhotoSticker,
                About= x.About,
            });
        }
    }
}
