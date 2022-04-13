using System.ComponentModel.DataAnnotations;

namespace UdemyIdentity.ViewModels
{

    public class PasswordResetByAdminViewModel
    {
        public string UserId { get; set; }

        [Display(Name = "Yeni şifre")]
        public string NewPassword { get; set; }
    }
}
