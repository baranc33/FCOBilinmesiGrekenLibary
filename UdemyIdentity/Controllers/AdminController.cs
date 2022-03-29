using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using UdemyIdentity.Models;

namespace UdemyIdentity.Controllers
{
    public class AdminController : Controller
    {
        private UserManager<AppUser> userManager { get; }

        public AdminController(UserManager<AppUser> userManager)
        {
            this.userManager = userManager;
        }



        public IActionResult Index()
        {
            //IQueryable qb = userManager.Users;
            // belirli adet alabiliriz
            return View(userManager.Users.ToList());
            // kullanıcı listesi Döner

        }

    }
}
