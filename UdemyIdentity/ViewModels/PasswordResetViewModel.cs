using System.ComponentModel.DataAnnotations;

namespace UdemyIdentity.ViewModels
{
    public class PasswordResetViewModel
    {
        [Display(Name ="Email Adresi")]
        [Required(ErrorMessage ="Email Alanı Zorunlduur")]
        [EmailAddress]
        public string Email { get; set; }
    }
}
