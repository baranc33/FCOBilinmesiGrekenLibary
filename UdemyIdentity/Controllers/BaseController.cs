using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using UdemyIdentity.Models;

namespace UdemyIdentity.Controllers
{
    public class BaseController : Controller
    {
        protected UserManager<AppUser> userManager { get; }
        protected SignInManager<AppUser> signInManager { get; }

        protected RoleManager<AppRole> roleManager { get; }

        // kullanııcıyı bulma
        protected AppUser CurrentUser => userManager.FindByNameAsync(User.Identity.Name).Result;

        public BaseController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, RoleManager<AppRole> roleManager = null)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.roleManager = roleManager;
        }

        // model statete eror ekleme
        public void AddModelError(IdentityResult result)
        {
            foreach (var item in result.Errors)
            {   // eğer en başta key gönderirsek gönderdiğimiz key ilgili inputun altında görünür
                        // biz genel hata olarak gösterecez
                        
                if (item.Description == "Invalid token.")
                {
                    ModelState.AddModelError("", "Link Daha önce kullanılmıştır.");
                }
                else
                {
                    ModelState.AddModelError("", item.Description);
                }
            }
        }
    }
}
