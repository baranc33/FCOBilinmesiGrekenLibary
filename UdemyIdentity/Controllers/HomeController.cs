using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using UdemyIdentity.Models;
using UdemyIdentity.ViewModels;
 

namespace UdemyIdentity.Controllers
{
    public class HomeController : Controller
    {
        public UserManager<AppUser> userManager { get; }
        public SignInManager<AppUser> signInManager { get; }
        public HomeController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult SignUp()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> SignUp(UserViewModel model)
        {
            if (ModelState.IsValid)
            {
                AppUser user = new AppUser()
                {
                    UserName = model.UserName,
                    Email = model.Email,
                    PhoneNumber = model.PhoneNumber,
                    Picture = model.Password
                };

                IdentityResult result = await userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    return RedirectToAction("Login");
                }
                else
                {
                    foreach (var item in result.Errors)
                    {
                        // eğer en başta key gönderirsek gönderdiğimiz key ilgili inputun altında görünür
                        // biz genel hata olarak gösterecez
                        ModelState.AddModelError(string.Empty, item.Description);

                    }
                }

            }
            return View(model);
        }




        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                AppUser user = await userManager.FindByEmailAsync(model.Email);
                if (user != null)
                {
                    // önce kayıtlı coockileri siliyoruz
                    await signInManager.SignOutAsync();

                    // 1. true/false program.cs te belirttiğimiz coockie ömrünü aktif eder
                    // 2. true/false başarısız girişlerde kulanıcı kitleme
Microsoft.AspNetCore.Identity.SignInResult result = await signInManager.PasswordSignInAsync(user, model.Password, false, false);

                    //if (result.IsNotAllowed)//kullanıcı kitliyken doğru giriş yaparsa
                    //if (result.Succeeded)// işlem başarılımı
                    //if (result.IsLockedOut)//kullanıcı kitlimi değilmi
                    //if (result.RequiresTwoFactor)// iki faktörlü koruma açıkmı?
                    if (result.Succeeded)
                    {
                        return RedirectToAction("Index","Member");
                    }



                }
                else
                {
                    // kullanıcı yoksa Email inputu hedefli bir mesaj yolliyalım
                    ModelState.AddModelError(nameof(LoginViewModel.Email), "Geçersiz kullanıcı adı veya  şifresi");
                }
            }






            return View(model);
        }
    }
}
