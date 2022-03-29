var builder = WebApplication.CreateBuilder(args);
builder.Services.AddMvc();

var app = builder.Build();
app.UseDeveloperExceptionPage();// developer hata mesajlarý
app.UseStatusCodePages();// özellikle bir content dönmiyen sayfalarda hata içeriði
app.UseStaticFiles();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
