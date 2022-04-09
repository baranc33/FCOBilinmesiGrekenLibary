using System.Net.Mail;
using System.Net;

namespace UdemyIdentity.Helper
{
    public static class PasswordReset
    {

        public static void PasswordResetSendEmail(string link, string email)
        {
            // mail göndere bilmek önce mail açıkken güvenliğini izin vermeliyiz
            //https://myaccount.google.com/lesssecureapps?pli=1&rapt=AEjHL4NdWDCEVH_yZNeXLR5Ca9EaqYQ3lgEYU5tyIERrr49koSOULHvyneHuKOsV2nSfPj0zkSoesKO5LOunNMzbJCgxSOSG2Q
         
            try
            {
                //MailMessage kütüphanesinden bir instance oluşturuyoruz.
                MailMessage mail = new MailMessage();
                //Mesaj içeriğinde html ifadelere izin veriyoruz.
                mail.IsBodyHtml = true;
                //Bu kısım mail'in kime gideceğidir.Kendi adresimi yazdım.
                mail.To.Add(email);
                //Burası ise kimin göndereceğidir.Kim gönderecek?
                mail.From = new MailAddress("docenthesapla@gmail.com", "Doçent Hesaplama");
                //Gelen mailin konusu
                mail.Subject = "Şifre Sıfırlama isteği";
                //mail.Body += "<h4 class='text-center'>Eğer Bu isteği siz göndermediyseniz ciddiye almayınız.</h4><hr>";
                //mail.Body += "<div class='col-md-6 offset-3 bg-primary text-white mt-5'><p class='text-warning'></p>";
                mail.Body = "<center><h1> Akademik Hesaplamalar sitemize şifre yenilenme isteğinde bulunuldu </h1></center>";
                mail.Body += "<center style='border-radius: 40px;padding-top:30px;padding-bottom:30px;margin-left: 10%;margin-right: 10%;background-color: rgba(29, 26, 191, 0.834);'>";
                mail.Body += "<h2 style='color:white'> Saygı Değer Kullanıcımız </h2><hr><div>";
                mail.Body += $"<p style='color:white'> Az önce wwww.akademikhesaplamalar,net sitesimize.  Mail adresiz adına şifre Sıfırlama isteğinde bulunuldu</p>";
                mail.Body += "<p style='color:yellow' > Adres size ait değilse veya işlem tarafınızca gerçekleştirilmediyse dikkate almayınız </p>";
                    mail.Body += $"<a style='color:red;text-decoration:none;font-size:20px;' href='{link}' > Şifrenizi Yenilemek için tıklayınız.</a></div> </center> ";
                mail.Body += $"<p> Bu istek sürekli tekrar ediyor ve sizin değilse sitemizin iletişim kısmından bize bildiriniz.</p>";
                mail.Body += "<a href='www.akademikhesaplamalar.net' style='text-decoration:none;'> wwww.akademikhesaplamalar.net >></a>";
                mail.IsBodyHtml = true;
                // smptp clientiını host firmamızdan öğreneceğiz
                SmtpClient smtp = new SmtpClient();
                //Burada maili gönderen kişinin mail adresi ve şifresi alınıyor.
                smtp.Credentials = new System.Net.NetworkCredential("docenthesapla@gmail.com", "hesaplaDocent33");
                //Hangi portu kullanacağımızı yazıyoruz.
                smtp.Port = 587;
                //Hangi mail adresini kullanacağızı seçiyoruz.
                smtp.Host = "smtp.gmail.com";
                //Ssl güvenlik protokolünü aktifleştiriyoruz.
                smtp.EnableSsl = true;
                //Maili gönderiyoruz.
                smtp.Send(mail);

            }
            catch (Exception)
            {
                throw;
            }


        }



        //public static void PasswordResetSendEmail(string link, string email)
        //{
        //// mail göndere bilmek önce mail açıkken güvenliğini izin vermeliyiz
        ////https://myaccount.google.com/lesssecureapps?pli=1&rapt=AEjHL4NdWDCEVH_yZNeXLR5Ca9EaqYQ3lgEYU5tyIERrr49koSOULHvyneHuKOsV2nSfPj0zkSoesKO5LOunNMzbJCgxSOSG2Q

        //    try
        //    {
        //        //MailMessage kütüphanesinden bir instance oluşturuyoruz.
        //        MailMessage mail = new MailMessage();
        //        //Mesaj içeriğinde html ifadelere izin veriyoruz.
        //        mail.IsBodyHtml = true;
        //        //Bu kısım mail'in kime gideceğidir.Kendi adresimi yazdım.
        //        mail.To.Add(email);
        //        //Burası ise kimin göndereceğidir.Kim gönderecek?
        //        mail.From = new MailAddress("docenthesapla@gmail.com","Doçent Hesaplama");
        //        //Gelen mailin konusu
        //        mail.Subject = "Şifre Sıfırlama isteği";
        //        mail.Body = "<h2>Doçentlik Puan Hesaplama sitemize Şifre yenilenmesine dair bir istek atıkldı</h2>";
        //        mail.Body += "<h4>Eğer Bu isteği siz göndermediyseniz ciddiye almayınız</h4><hr/>";
        //        mail.Body += $"<a style='color:red;text-decoration:none; ' href='{link}'>Şifrenizi Yenilemek için tıklayınız.</a>";
        //        mail.IsBodyHtml = true;
        //        // smptp clientiını host firmamızdan öğreneceğiz
        //        SmtpClient smtp = new SmtpClient();
        //        //Burada maili gönderen kişinin mail adresi ve şifresi alınıyor.
        //        smtp.Credentials = new System.Net.NetworkCredential("docenthesapla@gmail.com", "hesaplaDocent33");
        //        //Hangi portu kullanacağımızı yazıyoruz.
        //        smtp.Port = 587;
        //        //Hangi mail adresini kullanacağızı seçiyoruz.
        //        smtp.Host = "smtp.gmail.com";
        //        //Ssl güvenlik protokolünü aktifleştiriyoruz.
        //        smtp.EnableSsl = true;
        //        //Maili gönderiyoruz.
        //        smtp.Send(mail);

        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }


        //}







        //public static void PasswordResetSendEmail(string link, string email)
        //{
        //    MailMessage mail = new MailMessage();

        //    // smptp clientiını host firmamızdan öğreneceğiz
        //    SmtpClient smtpClient = new SmtpClient("smtp.gmail.com");
        //    //aldığımız mail adresi
        //    mail.From = new MailAddress("DocentHesapla@outlook.com");
        //    mail.To.Add(email);// burası bir list birden fazla göndermeyede yarar

        //    mail.Subject = $"www.DocentHesapla.com::Şifre sıfırlama";
        //    mail.Body = "<h2>Şifrenizi yenilemek için lütfen aşağıdaki linke tıklayınız.</h2><hr/>";
        //    mail.Body += $"<a href='{link}'>şifre yenileme linki</a>";
        //    mail.IsBodyHtml = true;
        //    smtpClient.Port = 587;
        //    smtpClient.Credentials = new System.Net.NetworkCredential("docenthesapla@outlook.com", "hesaplaDocent33");

        //    smtpClient.Send(mail);

        //}

    }
}
