using Azure;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.JSInterop.Infrastructure;
using ServiceFUEN.Models.DTOs;
using ServiceFUEN.Models.EFModels;
using ServiceFUEN.Models.ViewModels;
using System.Security.Claims;
using MimeKit;
using MailKit.Net.Smtp;
using MailKit.Security;
using System.Reflection.Metadata.Ecma335;

namespace ServiceFUEN.Controllers
{




	[EnableCors("AllowAny")]
	[ApiController]
	public class MembersController : Controller
	{

		private readonly ProjectFUENContext _context;
		public MembersController(ProjectFUENContext context)
		{
			_context = context;

		}


		[HttpPost]
		[Route("api/Members/Signup")]
		public string Signup(RegisterDTO dto)
		{

			Member check = _context.Members.SingleOrDefault(x => x.EmailAccount == dto.EmailAccount);
			if (check != null)
			{
				return "已被使用";
			}


			Member member = new Member()
			{
				EmailAccount = dto.EmailAccount,
				EncryptedPassword = dto.EncryptedPassword,
				//RealName = null,
				//NickName = nickname,
				//BirthOfDate = null,
				//Mobile = null,
				//Address = null,
				//PhotoSticker = null,
				//About = null,
				IsConfirmed = false, //預設是未確認的會員
				ConfirmCode = Guid.NewGuid().ToString("N"),
				IsInBlackList = false//預設黑名單:否
			};
			_context.Members.Add(member);
			_context.SaveChanges();

			return "註冊成功";
			//todo-註冊成功需寄出激活連結


		}

		[HttpPost]
		[Route("api/Members/Login")]
		public string Login([FromForm]string account, [FromForm] string password)
		{
			var user = (from a in _context.Members
						where a.EmailAccount == account
						&& a.EncryptedPassword == password
						select a).SingleOrDefault();

			if (user == null)
			{
				return "帳號密碼錯誤";
			}
			else
			{
				//todo驗證
			}

			var claims = new List<Claim>
				{
					new Claim(ClaimTypes.Name, user.EmailAccount),
					new Claim("FullName", user.NickName),
                   // new Claim(ClaimTypes.Role, "Administrator")
                };


			var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
			HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));

			return "登入";
		}




		/// <summary>
		/// 寄信-還需要寄出連結跟寄件者
		/// </summary>
		[HttpPost]
		[Route("api/Members/SendEmail")]
		public void SendEmail()
		{
			var message = new MimeMessage();
			message.From.Add(new MailboxAddress("123", "shirtyingplan@gmail.com"));
			message.To.Add(new MailboxAddress("456", "sky8227055@hotmail.com"));
			message.Subject = "123";

			BodyBuilder body = new BodyBuilder();
			body.HtmlBody = "<p>哭阿<p>";
			
			message.Body = body.ToMessageBody();


			Send(message);
		}

		/// <summary>
		/// Gmail資訊
		/// </summary>
		/// <param name="message"></param>
		private void Send(MimeMessage message)
		{
			using (var client = new SmtpClient())
			{
				client.Connect("smtp.gmail.com", 587, SecureSocketOptions.StartTls);
				client.Authenticate("shirtyingplan@gmail.com", "iskdjbmbuzbcylth");
				client.Send(message);
				client.Disconnect(true);
			}
		}


		/// <summary>
		/// 激活
		/// </summary>
		/// <param name="memberId"></param>
		/// <param name="confirmCode"></param>
		/// <returns></returns>
		[HttpGet]
		[Route("api/Members/ActiveRegister")]
		public string ActiveRegister(int memberId, string confirmCode)
		{
			
			Member entity = _context.Members.SingleOrDefault(x => x.Id == memberId);
			if (entity == null) return null;

			Member result = new Member
			{
				Id = entity.Id,
				EmailAccount = entity.EmailAccount,
				EncryptedPassword = entity.EncryptedPassword,
				IsConfirmed = entity.IsConfirmed,
				ConfirmCode = entity.ConfirmCode
			};

			if (string.Compare(result.ConfirmCode, confirmCode) != 0) return "失敗";
						
			var member = _context.Members.Find(memberId);
			member.IsConfirmed = true;
			member.ConfirmCode = null;
			_context.SaveChanges();

			return "激活成功";
		}

		[HttpGet]
		public bool IsExist(string account)
		{
			var member = _context.Members.SingleOrDefault(x => x.EmailAccount == account);
			if (member == null)
			{
			return true;
			}
			return false;

		}


		[HttpGet]
		[Route("api/Members/Profile")]
		public ProfileDTO Profile(int id)
		{
			var member = _context.Members.FirstOrDefault(x=>x.Id ==id);
			ProfileDTO dto = new ProfileDTO()
			{
				EmailAccount = member.EmailAccount,
				RealName = member.RealName,
				NickName = member.NickName,
				BirthOfDate = member.BirthOfDate,
				Mobile = member.Mobile,
				Address = member.Address,
				PhotoSticker = member.PhotoSticker,
				About = member.About,
			};		
			return dto;
		}
	}
}