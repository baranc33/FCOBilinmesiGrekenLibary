using System.ComponentModel.DataAnnotations;

namespace UdemyIdentity.ViewModels
{
    public class PasswordResetViewModel
    {
        [Display(Name ="Email Adresi")]
        [Required(ErrorMessage ="Email Alanı Zorunlduur")]
        [EmailAddress]
        public string Email { get; set; }




        [Display(Name = "Yeni Şifreniz")]
        [Required(ErrorMessage = "Şifre alanı gereklidir")]
        [DataType(DataType.Password)]
        [MinLength(4, ErrorMessage = "şifreniz en az 4 karakterli olmalıdır.")]
        public string PasswordNew { get; set; }
    }
}
