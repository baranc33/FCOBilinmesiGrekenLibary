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

            List<AppUser> users = userManager.Users.ToList();
            //IQueryable qb = userManager.Users;
            // belirli adet alabiliriz
            return View(users);
            // kullanıcı listesi Döner

        }

    }
}
