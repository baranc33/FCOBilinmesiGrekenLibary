using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using UdemyIdentity.Models;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddMvc();

string con= builder.Configuration["ConnectionStrings:DefaultConnectionString"];
builder.Services.AddDbContext<AppIdentityDbContext>(opts =>
{
    opts.UseSqlServer(con);
});// entity framework db baðlantýsý

// ýdentity Baðlantýýsý
builder.Services.AddIdentity<AppUser,AppRole>().AddEntityFrameworkStores<AppIdentityDbContext>();



var app = builder.Build();
app.UseDeveloperExceptionPage();// developer hata mesajlarý
app.UseStatusCodePages();// özellikle bir content dönmiyen sayfalarda hata içeriði
app.UseStaticFiles();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
