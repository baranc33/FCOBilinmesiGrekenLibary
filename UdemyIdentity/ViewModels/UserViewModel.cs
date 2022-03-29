using System.ComponentModel.DataAnnotations;

namespace UdemyIdentity.ViewModels
{
    public class UserViewModel
    {
        [Required(ErrorMessage ="Kullanıcı Adı Gereklidir")]
        [Display(Name ="Kullanıcı Adı :")]
        public string UserName{ get; set; }

        [Display(Name = "Tel No:")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "Email Adresi Gereklidir")]
        [Display(Name = "Email Adresi:")]
        [EmailAddress(ErrorMessage ="Hatalı Mail Girişi")]
        public string Email { get; set; }
        
        [Required(ErrorMessage ="Şifre Zorunludur")]
        [Display(Name = "Şifre :")]
        [DataType(DataType.Password)]// emailde bu sekilde yazılabilir
        public string Password { get; set; }

    }
}
