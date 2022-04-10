using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using UdemyIdentity.CustomValidaton;
using UdemyIdentity.Models;



var builder = WebApplication.CreateBuilder(args);


string con = builder.Configuration["ConnectionStrings:DefaultConnectionString"];
builder.Services.AddDbContext<AppIdentityDbContext>(opts =>
{
    opts.UseSqlServer(con);
});// entity framework db ba�lant�s�


// �dentity Ba�lant��s�
builder.Services.AddIdentity<AppUser, AppRole>(opt =>
{
    opt.User.RequireUniqueEmail = true;//evet tek mail olmal�
    opt.User.AllowedUserNameCharacters = "�i��������abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._";

    opt.Password.RequiredLength = 4;//minimun de�er
    opt.Password.RequireLowercase = false;
    opt.Password.RequireUppercase = false;
    opt.Password.RequireNonAlphanumeric = false;
    opt.Password.RequireDigit = false;

}).AddEntityFrameworkStores<AppIdentityDbContext>().AddPasswordValidator<CustomPasswordValidator>()
.AddUserValidator<CustomUserValidator>().AddErrorDescriber<CustomIdentityErrorDescriber>()
.AddDefaultTokenProviders();




CookieBuilder cookieBuilder = new CookieBuilder();
cookieBuilder.Name = "MyBlog";
cookieBuilder.HttpOnly = false;
cookieBuilder.SameSite = SameSiteMode.Lax;
cookieBuilder.SecurePolicy = CookieSecurePolicy.SameAsRequest;


builder.Services.ConfigureApplicationCookie(opts =>
{
    opts.LoginPath = new PathString("/Home/Login");
    opts.LogoutPath = new PathString("/Member/Logout");
    opts.Cookie = cookieBuilder;
    opts.SlidingExpiration = true;
    opts.ExpireTimeSpan = System.TimeSpan.FromDays(60);
    opts.AccessDeniedPath = new PathString("/Member/AccessDenied");
});





builder.Services.AddMvc();
// Endpoint routing does not support hatas� i�in
// builder.Services.AddMvc(option => option.EnableEndpointRouting = false); 





var app = builder.Build();
app.UseDeveloperExceptionPage();// developer hata mesajlar�
app.UseStatusCodePages();// �zellikle bir view d�nmiyen sayfalarda hata i�eri�i yazar


//now handle other requests (default, static files, mvc actions, ...)
app.UseStaticFiles();





app.UseAuthentication();
app.UseAuthorization();// derste hoca bunu yazmiyor ama yazmay�nca hata veriyor
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");










app.Run();
