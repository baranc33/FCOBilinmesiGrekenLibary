using Microsoft.AspNetCore.Identity;
using UdemyIdentity.Models;

namespace UdemyIdentity.CustomValidaton
{
    public class CustomPasswordValidator : IPasswordValidator<AppUser>
    {
        public Task<IdentityResult> ValidateAsync(UserManager<AppUser>
            manager, AppUser user, string password)
        {// implemente ettikten sonra üstteki kod hazır geliyor
            // bize bundan sonrasını işlemek kalıyor
            List<IdentityError> errors = new List<IdentityError>();
            // kendimize Bi error örneği oluşturalımki bu sınıfı kullanabileleim

            //şimdi sorularımızı sorup doğrulayalım
            // kullanıcı ismini şifreye koymasın
            if (password.ToLower().Contains(user.UserName.ToLower()))
            {// burda Code kısmı ID olarak düşünebiliriz çağırıp kullanmak için.
                errors.Add(new IdentityError()
                {
                    Code = "PasswordContainsUserName",
                    Description = "Şifre Kullanıcı Adı içermez"
                });
            }
            // burası sadece 1234 ü etkiler bütün ardaşık sayıları engellemez 
            //o yüzden fazla bu işlemi 3 - 5 parçada yapmalıyız
            if (password.ToLower().Contains("1111"))
            {
                errors.Add(new IdentityError()
                {
                    Code = "PasswordContains1111",
                    Description = "Şifre bu kadar Basit olamaz"
                });
            }
            // şartları hayal gücüne bağlı olarak arttırıla bilir



            if (errors.Count == 0)// hata yoksa
            {
                return Task.FromResult(IdentityResult.Success); // başarılı
            }
            else
            {
                return Task.FromResult(IdentityResult.Failed(errors.ToArray()));
            }

        }

    }

}
