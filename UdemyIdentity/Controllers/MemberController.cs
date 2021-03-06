using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using UdemyIdentity.Enums;
using UdemyIdentity.Models;
using UdemyIdentity.ViewModels;
namespace UdemyIdentity.Controllers
{
    [Authorize]
    public class MemberController : BaseController
    {
        public MemberController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager) : base(userManager, signInManager)
        {
        }

        public IActionResult Index()
        {

            AppUser user = CurrentUser;
            // mapster kütüphanesi yükledik core olanı değil
            // direk böyle dönüştürünce aynı isime eşleştiriyor.
            UserViewModel userViewModel = user.Adapt<UserViewModel>();
            return View(userViewModel);
        }

        [HttpGet]
        public IActionResult UserEdit()
        {
            AppUser user = CurrentUser;

            UserViewModel userViewModel = user.Adapt<UserViewModel>();
            // enum içersine sayı değilde bir enumdan yazı gönderiyorum
            ViewBag.Gender = new SelectList(Enum.GetNames(typeof(Gender)));

            return View(userViewModel);
        }

        [HttpPost]// burda model ve resim alıyoruz
        public async Task<IActionResult> UserEdit(UserViewModel userViewModel, IFormFile userPicture)
        {// şifre gerekli olmadığından model stattenden çıkarıyorum
            ModelState.Remove("Password");
            // kişi resim yüklemek zorunda olmadığından dolayı bunuda çıkarıyorum
            ModelState.Remove("userPicture");

            // gelen veriyi enumdan değiştiriyorum
            ViewBag.Gender = new SelectList(Enum.GetNames(typeof(Gender)));
            if (ModelState.IsValid)
            {// kendi bilgisini güncelliceği için kendi bilgelerinden getiriyoruz
                AppUser user = CurrentUser;
                string oldPictrueName = user.Picture;
                // 1 telefon numarası 1 kişi tarafından kullanılmasını istiyorsak ona göre bir sorgu hazirlicaz
                // öncelikle kullanıcının telefon numarasını getiriyoruz
                string phone = userManager.GetPhoneNumberAsync(user).Result;
                // değiştirilimişmi numarası diye bakıyoruz
                if (phone != userViewModel.PhoneNumber)
                {// yeni telefon numarası daha önce kullanılmışmsa hata ekleyip gönderiyoruz
                    if (userManager.Users.Any(u => u.PhoneNumber == userViewModel.PhoneNumber))
                    {
                        ModelState.AddModelError("", "Bu telefon numarası başka üye tarafından kullanılmaktadır.");
                        return View(userViewModel);
                    }
                }
                // bir resim yüklenmişmi ? aynı zamanda isiminide kontrol ediyoruz
               
                if (userPicture != null && userPicture.Length > 0)
                {// bir path ismi oluşturuyoruz
                    var fileName = Guid.NewGuid().ToString() + Path.GetExtension(userPicture.FileName);

                    var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/UserPicture", fileName);

                    using (var stream = new FileStream(path, FileMode.Create))
                    {
                        await userPicture.CopyToAsync(stream);

                        user.Picture = "/UserPicture/" + fileName;
                    }
                    // burdan eski resmi silcem 13. indexten alıyorumki user picture yazısını iptal edeyim
                    if (oldPictrueName!=null && oldPictrueName.Length>5)
                    {// hiç resmi yoksa diye kontrol ediyorum
                        var deletePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/UserPicture", oldPictrueName.Substring(13));
                        FileInfo fi =new FileInfo(deletePath);
                        System.IO.File.Delete(deletePath);
                        fi.Delete();
                    }
                }
                // değiştircem bilgileri tek tek seçiyorum
                user.UserName = userViewModel.UserName;
                user.Email = userViewModel.Email;
                user.PhoneNumber = userViewModel.PhoneNumber;
                user.City = userViewModel.City;
                user.BirthDay = userViewModel.BirthDay;
                user.Gender = (int)userViewModel.Gender;
                // update işlemi
                IdentityResult result = await userManager.UpdateAsync(user);
                // başarılıysa securtiystamp güncellemesi için sigout ve signin yaptırıyorum
                if (result.Succeeded)
                {
                    await userManager.UpdateSecurityStampAsync(user);
                    await signInManager.SignOutAsync();
                    await signInManager.SignInAsync(user, true);

                    ViewBag.success = "true";
                }
                else
                {// hataları tek tek dönüyorum
                    AddModelError(result);
                }
            }

            return View(userViewModel);
        }

        public IActionResult PasswordChange()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> PasswordChange(PasswordChangeViewModel passwordChangeViewModel)
        {
            // model statele hata varmı bakalım tekrar ve şifreler açısından
            if (ModelState.IsValid)
            {
                AppUser user = CurrentUser;

                // eski şifre doğrumu diye kontrol ediyoruz
                bool exist = userManager.CheckPasswordAsync(user, passwordChangeViewModel.PasswordOld).Result;

                if (exist)
                {
                    IdentityResult result = userManager.ChangePasswordAsync(user, passwordChangeViewModel.PasswordOld, passwordChangeViewModel.PasswordNew).Result;

                    if (result.Succeeded)
                    {
                        // şifre güncellendiyse securityStampi değiştirelim
                        await userManager.UpdateSecurityStampAsync(user);
                        // çıkış yaptıralım coockieden
                        await signInManager.SignOutAsync();
                        // tekrardan giriş yaptaralım
                        await signInManager.PasswordSignInAsync(user, passwordChangeViewModel.PasswordNew, true, false);
                        // mesaj göstermek için bir data oluşturalım
                        ViewBag.success = "true";
                    }
                    else
                    {
                        AddModelError(result);
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Eski şifreniz yanlış");
                }
            }

            return View(passwordChangeViewModel);
        }

        public void Logout()
        {
            signInManager.SignOutAsync();
        }

        //public IActionResult LogOut()
        //{
        //    signInManager.SignOutAsync();
        //    return RedirectToAction("Index", "Home");
        //}
       
   public IActionResult AccessDenied(string ReturnUrl)
   {
       if (ReturnUrl.ToLower().Contains("violencegage"))
       {
           ViewBag.message = "Erişmeye çalıştığınız sayfa şiddet videoları içerdiğinden dolayı 15 yaşında büyük olmanız gerekmektedir";
       }
       else if (ReturnUrl.ToLower().Contains("ankarapage"))
       {
           ViewBag.message = "Bu sayfaya sadece şehir alanı ankara olan kullanıcılar erişebilir";
       }
       else if (ReturnUrl.ToLower().Contains("exchange"))
       {
           ViewBag.message = "30 günlük ücretsiz deneme hakkınız sona ermiştir.";
       }
       else
       {
           ViewBag.message = "Bu sayfaya erişim izniniz yoktur. Erişim izni almak için site yöneticisiyle görüşünüz";
       }

       return View();
   }
        /*      [Authorize(Roles = "manager,admin")]
          public IActionResult Manager()
          {
              return View();
          }

          [Authorize(Roles = "editor,admin")]
          public IActionResult Editor()
          {
              return View();
          }

          [Authorize(Policy = "AnkaraPolicy")]
          public IActionResult AnkaraPage()
          {
              return View();
          }

          [Authorize(Policy = "ViolencePolicy")]
          public IActionResult ViolencePage()
          {
              return View();
          }

          public async Task<IActionResult> ExchangeRedirect()
          {
              bool result = User.HasClaim(x => x.Type == "ExpireDateExchange");

              if (!result)
              {
                  Claim ExpireDateExchange = new Claim("ExpireDateExchange", DateTime.Now.AddDays(30).Date.ToShortDateString(), ClaimValueTypes.String, "Internal");

                  await userManager.AddClaimAsync(CurrentUser, ExpireDateExchange);

                  await signInManager.SignOutAsync();
                  await signInManager.SignInAsync(CurrentUser, true);
              }

              return RedirectToAction("Exchange");
          }

          [Authorize(Policy = "ExchangePolicy")]
          public IActionResult Exchange()
          {
              return View();
          }*/
    }
}
