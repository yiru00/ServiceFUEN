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
using static System.Net.WebRequestMethods;
using Microsoft.AspNetCore.Authorization;
using System.Text;
using System.Security.Cryptography;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.VisualBasic;
using System;
using MimeKit.Utils;

namespace ServiceFUEN.Controllers
{

	[EnableCors("AllowAny")]
	[ApiController]
	public class MembersController : Controller
	{
        private IHttpContextAccessor _contextAccessor;
        private readonly ProjectFUENContext _context;
        private readonly IConfiguration _configuration;
        public MembersController(ProjectFUENContext context, IConfiguration configuration, IHttpContextAccessor contextAccessor)
        {
            _context = context;
            _configuration = configuration;
            _contextAccessor = contextAccessor;

        }
        private string salt = "@!#IUBNKLF";

		[HttpPost]
		[Route("api/Members/SignUp")]
		public string SignUp(RegisterDTO dto)
		{

			Member check = _context.Members.SingleOrDefault(x => x.EmailAccount == dto.EmailAccount);
			if (check != null)
			{
				return "無法註冊這個帳號";
			}else if(dto.EncryptedPassword.Length > 12)
			{
				return "密碼不得超過12個字";
			}

			Member member = new Member()
			{
				EmailAccount = dto.EmailAccount,
				EncryptedPassword = ToSHA256(dto.EncryptedPassword,salt),
				NickName = dto.NickName,
				IsConfirmed = false, //預設是未確認的會員
				ConfirmCode = Guid.NewGuid().ToString("N"),
				IsInBlackList = false//預設黑名單:否
			};
			_context.Members.Add(member);
			_context.SaveChanges();

			SendSignUpEmail(member.MailUser());
			return "註冊成功，請至信箱查看啟用郵件。";
			
		}
		
		public static string ToSHA256(string plainText, string salt)
			{
				// ref https://docs.microsoft.com/zh-tw/dotnet/api/system.security.cryptography.sha256?view=net-6.0
				using (SHA256 mySHA256 = SHA256.Create())
				{
					var passwordBytes = Encoding.UTF8.GetBytes(salt + plainText);
					var hash = mySHA256.ComputeHash(passwordBytes);
					StringBuilder sb = new StringBuilder();
					foreach (var b in hash)
					{
						sb.Append(b.ToString("X2"));
					}
					return sb.ToString();
				}
			}

        [HttpPost]
        [Route("api/Members/JwtLogin")]
        public string JwtLogin(string account, string password)
        {
            var user = (from a in _context.Members
                        where a.EmailAccount == account
                        && a.EncryptedPassword == ToSHA256(password , salt)
						select a).SingleOrDefault();

            if (user == null)
            {
                return "帳號密碼錯誤";
            }
            else if (user.IsConfirmed == false)
            {
                return "帳號尚未啟用，請至信箱查看。";
            }
            else if (user.IsInBlackList == true)
            {
                return "此帳戶已是黑名單";
            }
            else
            {
                //設定使用者資訊
                var claims = new List<Claim>
                {
                    new Claim(JwtRegisteredClaimNames.NameId, user.Id.ToString()),
                    new Claim(JwtRegisteredClaimNames.Email, user.EmailAccount),
                    new Claim("FullName", user.NickName),

                };
                //var role = from a in _todoContext.Roles
                //           where a.EmployeeId == user.EmployeeId
                //           select a;
                ////設定Role
                //foreach (var temp in role)
                //{
                //    claims.Add(new Claim(ClaimTypes.Role, temp.Name));
                //}

                //取出appsettings.json裡的KEY處理
                var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:KEY"]));

                //設定jwt相關資訊
                var jwt = new JwtSecurityToken
                (
                    issuer: _configuration["JWT:Issuer"],
                    audience: _configuration["JWT:Audience"],
                    claims: claims,
                    expires: DateTime.Now.AddDays(3),
                    signingCredentials: new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256)
                );

                //產生JWT Token
                var token = new JwtSecurityTokenHandler().WriteToken(jwt);

                //回傳JWT Token給認證通過的使用者
                return token;
            }
        }

		//讀取使用者Id
		[Authorize]
		[HttpGet]
        [Route("api/Members/Read")]
        public string Read()
        {

            var claim = User.Claims.ToArray();
			
            var userId = claim.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value;
			
			return userId;
        }

  //      [Authorize]
  //      [HttpDelete]
		//[Route("api/Members/Logout")]
		//public void Logout()
		//{
		//	HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
		//}

		//[HttpGet]
		//[Route("api/Members/NoLogin")]
		//public string NoLogin()
		//{
		//	return "未登入";
		//}
		


        /// <summary>
        /// 啟用的方法
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="confirmCode"></param>
        /// <returns></returns>
        [HttpGet]
		[Route("api/Members/ActiveRegister")]
		public IActionResult ActiveRegister(int Id, string confirmCode)
		{
			
			Member entity = _context.Members.SingleOrDefault(x => x.Id == Id);
			if (entity == null) return null;

			Member result = new Member
			{
				Id = entity.Id,
				EmailAccount = entity.EmailAccount,
				EncryptedPassword = entity.EncryptedPassword,
				IsConfirmed = entity.IsConfirmed,
				ConfirmCode = entity.ConfirmCode
			};

			if (string.Compare(result.ConfirmCode, confirmCode) != 0)
			{
				return NotFound();
			}

			var member = _context.Members.Find(Id);
			member.IsConfirmed = true;
			member.ConfirmCode = null;
			_context.SaveChanges();

			return Redirect("http://localhost:5173/HomeView");
		}

        
        [HttpGet]
		[Route("api/Members/IsExist")]
		public bool IsExist(string account)
		{
			var member = _context.Members.SingleOrDefault(x => x.EmailAccount == account);
			if (member == null)
			{
			return true;
			}
			return false;
		}

		
		[Authorize]
		[HttpGet]
		[Route("api/Members/Profile")]
		public ProfileDTO Profile(int id)
		{
			var member = _context.Members.SingleOrDefault(x=>x.Id ==id);
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

		[Authorize]
		[HttpPost]
		[Route("api/Members/EditProfile")]
		public string EditProfile([FromForm]EditProfileDTO source)
		{
			var claim = User.Claims.ToArray();
			var userId = claim.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value;
			var Id = int.Parse(userId.ToString());

			var member = _context.Members.SingleOrDefault(x=>x.Id== Id);
			if (member == null)
			{
				return "Fail";
			}
			string path = System.Environment.CurrentDirectory + "/wwwroot/Images/";
			string extension = Path.GetExtension(source.File.FileName);
			string fileName = Guid.NewGuid().ToString("N");
			string fullName = fileName + extension;
			string fullpath = Path.Combine(path, fullName);
			using(var stream = System.IO.File.Create(fullpath))
			{
				source.File.CopyTo(stream);
			}
			
			member.RealName = source.RealName;
			member.NickName = source.NickName;
			member.BirthOfDate = source.BirthOfDate;
			member.Mobile = source.Mobile;
			member.Address = source.Address;
			member.PhotoSticker = fullName;
			member.About = source.About;
						
			_context.SaveChanges();

			return "Update";

		}
		[Authorize]
		[HttpPost]
		[Route("api/Members/EditPassword")]
		public string EditPassword(EditPasswordDTO source)
		//原密碼-輸入兩次新密碼-儲存變更-寫入資料庫變更
		{


			var claim = User.Claims.ToArray();
			var userId = claim.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value;
			var Id = int.Parse(userId.ToString());

			var oldpassword = ToSHA256(source.OldEncryptedPassword, salt);

            var member = _context.Members.SingleOrDefault(x => x.Id == Id);
			if (oldpassword != member.EncryptedPassword || source.EncryptedPassword!=source.ConfirmEncryptedPassword)
			{
                return "密碼有誤";
            }
            else

			member.EncryptedPassword = ToSHA256(source.EncryptedPassword,salt);

			_context.SaveChanges();
			return "變更成功";
		}


		[HttpPost]
		[Route("api/Members/ForgotPassword")]
		public string ForgotPassword(ForgotPasswordDTO source)
		{
			var member = _context.Members.SingleOrDefault(x => x.RealName == source.RealName && x.EmailAccount == source.EmailAccount && x.Mobile == source.Mobile);
			if (member == null)
			{
				return "資料有誤";
			}

			else
			{
				member.EncryptedPassword = Guid.NewGuid().ToString("N").Substring(1, 7);
				SendForgotPasswordEmail(member.MailUser());

				member.EncryptedPassword = ToSHA256(member.EncryptedPassword, salt);
				_context.SaveChanges();
			}
			return "success";
		}

		/// <summary>
		/// 註冊帳號寄出啟用郵件
		/// </summary>
		private void SendSignUpEmail(MailDTO source)
		{
			var message = new MimeMessage();
			message.From.Add(new MailboxAddress("Karza!", "shirtyingplan@gmail.com"));
			message.To.Add(new MailboxAddress("New Member", source.EmailAccount));
			message.Subject = "歡迎使用Karza!";

			BodyBuilder body = new BodyBuilder();
			string url = Request.Scheme + "://" + Request.Host + $"/api/Members/ActiveRegister?Id={source.Id}&confirmCode={source.ConfirmCode}";
			body.HtmlBody = $"<p style='font-size: 25px;'>感謝您註冊我們的網站！</p> <br> <a href=\"{url}\"><span style='font-size: 25px;'>啟用帳號</span></a><br>" +
				$"<span style='font-size: 25px;'>點擊上面連結後即可登入，建議使用者登入後馬上完整個人資料，避免忘記密碼及其它功能無法使用。</span> <br>" +
				$"<img src=\"cid:logo\" alt='網站Logo' width='50%'>";
			
			var image = body.LinkedResources.Add(@"wwwroot/images/logo.jpg");
			image.ContentId = MimeUtils.GenerateMessageId();

			// 將圖片插入到HTML中
			body.HtmlBody = body.HtmlBody.Replace("cid:logo", $"cid:{image.ContentId}");

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
		/// 忘記密碼寄信
		/// </summary>
		/// <param name="source"></param>
		private void SendForgotPasswordEmail(MailDTO source)
		{
			var message = new MimeMessage();
			message.From.Add(new MailboxAddress("Karza!", "shirtyingplan@gmail.com"));
			message.To.Add(new MailboxAddress("New Member", source.EmailAccount));
			message.Subject = "忘記密碼";

			BodyBuilder body = new BodyBuilder();
			body.HtmlBody = $"<p style='font-size: 25px;'>您的新密碼:</p><p style='font-size: 25px;'>{source.EncryptedPassword}</p><br><p style='font-size: 25px;'>建議使用者登入後立即修改密碼。</p>";
			
			message.Body = body.ToMessageBody();

			Send(message);
		}
	}
}