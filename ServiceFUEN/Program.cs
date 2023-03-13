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

app.UseCookiePolicy();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.UseStaticFiles();

app.UseStaticFiles();

app.Run();
