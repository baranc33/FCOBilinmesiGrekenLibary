using Microsoft.AspNetCore.Identity;

namespace UdemyIdentity.CustomValidaton
{

    public class CustomIdentityErrorDescriber : IdentityErrorDescriber
    {
        // default user name mesajını bulduk ve ezerrek istediğimiz cümleyi yazalım
        public override IdentityError InvalidUserName(string userName)
        {
            return new IdentityError()
            {
                Code = "InvalidUserName",
                Description = $"Bu {userName} Geçersizdir"

            };

        }


        // birde mail için yapalım
        public override IdentityError DuplicateEmail(string email)
        {
            return new IdentityError()
            {
                Code = "InvalidEmail",
                Description = $"Bu {email} Kullanılmaktadır"

            };
        }

        // password uzunluğu
        public override IdentityError PasswordTooShort(int length)
        {
            return new IdentityError()
            {
                Code = "InvalidPasswordToShor",
                Description = $"şifreniz en Az {length} Karakterli olmalıdır"// length i start uptan çekiyor

            };
        }



    }

}
