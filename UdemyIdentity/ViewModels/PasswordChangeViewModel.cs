using System.ComponentModel.DataAnnotations;

namespace UdemyIdentity.ViewModels
{
    public class PasswordChangeViewModel
    {
        [Display(Name ="Eski şifreniz")]
        [Required(ErrorMessage ="Eski Şifreniz Zorunludur")]
        [DataType(DataType.Password)]
        [MinLength(4,ErrorMessage ="Şifreniz En az 4 karakterli olmalıdır")]
        public string  PasswordOld{ get; set; }

        [Display(Name = "Yeni şifreniz")]
        [Required(ErrorMessage = "Yeni Şifreniz Zorunludur")]
        [DataType(DataType.Password)]
        [MinLength(4, ErrorMessage = "Şifreniz En az 4 karakterli olmalıdır")]
        public string  PasswordNew { get; set; }


        [Display(Name = "Yeni şifre tekrarı")]
        [Required(ErrorMessage = "Tekrar şifre onay Zorunludur")]
        [MinLength(4, ErrorMessage = "Şifreniz En az 4 karakterli olmalıdır")]
        [Compare("PasswordNew",ErrorMessage ="Şifreniz Uyuşmuyor")]
        public string PasswordConfirm { get; set; }
    }
}
