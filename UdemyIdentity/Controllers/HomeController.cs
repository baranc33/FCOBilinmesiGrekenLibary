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



        [HttpGet]
        public IActionResult Login(string ReturnUrl)
        {
            TempData["ReturnUrl"] = ReturnUrl;
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
                    if (await userManager.IsLockedOutAsync(user)) // true dönerse kitlidir
                    {
                        ModelState.AddModelError("", "Hesabınız Bir Süreliğine kitlenmiştir bir süre sonra tekrar deneyiniz");
                    }
                    else
                    {

                        // önce kayıtlı coockileri siliyoruz
                        await signInManager.SignOutAsync();

                        // 1. true/false program.cs te belirttiğimiz coockie ömrünü aktif eder
                        // 2. true/false başarısız girişlerde kulanıcı kitleme
                        Microsoft.AspNetCore.Identity.SignInResult result = await signInManager.PasswordSignInAsync(user, model.Password, model.RememberMe, false);

                        //if (result.IsNotAllowed)//kullanıcı kitliyken doğru giriş yaparsa
                        //if (result.Succeeded)// işlem başarılımı
                        //if (result.IsLockedOut)//kullanıcı kitlimi değilmi
                        //if (result.RequiresTwoFactor)// iki faktörlü koruma açıkmı?
                        if (result.Succeeded)
                        {
                            await userManager.ResetAccessFailedCountAsync(user);
                            if (TempData["ReturnUrl"] != null)
                            {
                                return Redirect(TempData["ReturnUrl"].ToString());
                            }
                            return RedirectToAction("Index", "Member");
                        }
                        else
                        {

                            await userManager.AccessFailedAsync(user);// hhatalı girişi 1 arttır
                            int fail = await userManager.GetAccessFailedCountAsync(user);// hatalı girişleri getir

                            if (fail == 3)
                            {// 3 başarısız giriş yapmak
                                await userManager.SetLockoutEndDateAsync(user, new DateTimeOffset(DateTime.Now.AddMinutes(5)));
                                ModelState.AddModelError("", "Hesabınız 5 dk boyunca kitlendi.");
                            }
                            // hatalı giriş mesajı
                            ModelState.AddModelError("", $"{fail} Hatalı Giriş Yaptınız");

                        }
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

        public IActionResult ResetPassword()
        {
            TempData["durum"] = null;
            return View();
        }

        [HttpPost]
        public IActionResult ResetPassword(PasswordResetViewModel passwordResetViewModel)
        {
            if (TempData["durum"] == null)
            {
                AppUser user = userManager.FindByEmailAsync(passwordResetViewModel.Email).Result;
                //  kullanıcı doğru varmı bakıyoruz
                if (user != null)
                {
                    //user manager sınıfından bir token oluşturuyoruz
                    // bu tokenı maile ile yollicazki doğrulama işleminde doğru mail adresinden 
                    // dönüş olduğunu onaylamak için bu tokenı oluşturmak için program.cs te service eklemeliyiz
                    // ıdentity altına .AddDefaultTokenProviders();
                    //
                    string passwordResetToken = userManager.GeneratePasswordResetTokenAsync(user).Result;

                    // bir link oluşturcaz mailde tıkladığında gideceği yer
                    string passwordResetLink = Url.Action("ResetPasswordConfirm", "Home", new
                    {
                        userId = user.Id,
                        token = passwordResetToken
                    }, HttpContext.Request.Scheme);

                    //  www.bıdıbıdı.com/Home/ResetPasswordConfirm?userId=sdjfsjf&token=dfjkdjfdjf

                    Helper.PasswordReset.PasswordResetSendEmail(passwordResetLink, user.Email);

                    // mail gönderildimi diğe bilgi ekliyoruz
                    ViewBag.status = "success";
                    TempData["durum"] = true.ToString();
                }
                else
                {
                    // email hatalı ise
                    ModelState.AddModelError("", "Sistemde kayıtlı email adresi bulunamamıştır.");
                }
                return View(passwordResetViewModel);
            }
            else
            {
                return RedirectToAction("ResetPassword");
            }
        }


    }
}
