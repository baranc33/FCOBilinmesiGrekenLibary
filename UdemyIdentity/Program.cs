var builder = WebApplication.CreateBuilder(args);
builder.Services.AddMvc();

var app = builder.Build();
app.UseDeveloperExceptionPage();// developer hata mesajlar�
app.UseStatusCodePages();// �zellikle bir content d�nmiyen sayfalarda hata i�eri�i
app.UseStaticFiles();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
