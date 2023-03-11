using MailKit;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using ServiceFUEN.Models.DTOs;
using ServiceFUEN.Models.EFModels;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var ProjectFUENconnectionString = builder.Configuration.GetConnectionString("azure") ?? throw new InvalidOperationException("Connection string 'azure' not found.");
builder.Services.AddDbContext<ProjectFUENContext>(options =>
    options.UseSqlServer(ProjectFUENconnectionString));

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, options => options.LoginPath = "/Login/Index");

builder.Services.AddControllers();

string MyAllowOrigins = "AllowAny";
builder.Services.AddCors(options => {
    options.AddPolicy(
            name: MyAllowOrigins,
            policy => policy.WithOrigins("*")
            .WithHeaders("*")
            .WithMethods("*"));
});

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidIssuer = "flower",
            ValidateAudience = true,
            ValidAudience = "my",
            ValidateLifetime = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("ASDZXASDHAUISDHASDOHAHSDUAHDS"))
        };
    });

//builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(option =>
//{
//	//未登入時會自動導到這個網址
//	option.LoginPath = new PathString("/api/Login/NoLogin");
//});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddHttpContextAccessor();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseStaticFiles();


app.UseCors();
app.UseHttpsRedirection();

app.UseDefaultFiles();
app.Use(async (context, next) =>
{
    await next();
    // 判斷是否要存取網頁，而不是發api請求
    if (context.Response.StatusCode == 404 &&  // 該資源不存在
        !System.IO.Path.HasExtension(context.Request.Path.Value) && // 網址最後沒有帶副檔名
        !context.Request.Path.Value.StartsWith("/api")) // 網址不是/api開頭
    {
        context.Request.Path = "/index.html"; // 將網址改成index.html
        context.Response.StatusCode = 200; // 並將HTTP狀態碼設200成功
        await next();
    }
});
app.UseStaticFiles();

app.UseCookiePolicy();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
