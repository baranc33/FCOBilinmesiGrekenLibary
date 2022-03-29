using Microsoft.AspNetCore.Identity;

namespace UdemyIdentity.Models
{
    public class AppUser : IdentityUser
    {
        public int City { get; set; }
        public string Picture { get; set; }

    }
}
