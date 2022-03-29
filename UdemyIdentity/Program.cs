using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using UdemyIdentity.Models;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddMvc();

string con= builder.Configuration["ConnectionStrings:DefaultConnectionString"];
builder.Services.AddDbContext<AppIdentityDbContext>(opts =>
{
    opts.UseSqlServer(con);
});// entity framework db ba�lant�s�

// �dentity Ba�lant��s�
builder.Services.AddIdentity<AppUser,AppRole>().AddEntityFrameworkStores<AppIdentityDbContext>();



var app = builder.Build();
app.UseDeveloperExceptionPage();// developer hata mesajlar�
app.UseStatusCodePages();// �zellikle bir content d�nmiyen sayfalarda hata i�eri�i
app.UseStaticFiles();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
