using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using UdemyIdentity.Models;
using UdemyIdentity.ViewModels;

namespace UdemyIdentity.Controllers
{
    public class HomeController : Controller
    {
        private UserManager<AppUser> userManager { get; }

        public HomeController(UserManager<AppUser> userManager)
        {
            this.userManager = userManager;
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
                    PhoneNumber = model.PhoneNumber
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
    }
}
