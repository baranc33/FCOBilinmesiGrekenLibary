using System.Net.Mail;
using System.Net;

namespace UdemyIdentity.Helper
{
    public static class PasswordReset
    {


        public static void PasswordResetSendEmail(string link, string email)
        {

            var fromAddress = new MailAddress("docenthesapla@gmail.com", "From Name");
            var toAddress = new MailAddress(email, "To Name");
            const string subject = "test";
            const string body = "Hey now!!";

            var smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                Credentials = new NetworkCredential("docenthesapla@gmail.com", "hesaplaDocent33"),
            Timeout = 20000
            };
            using (var message = new MailMessage(fromAddress, toAddress)
            {
                Subject = subject,
                Body = body
            })
            {
                smtp.Send(message);
            }
        }
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


        //public static void PasswordResetSendEmail(string link, string email)
        //{
        //    //Gelen bilgileri string değişkenlere attım.
        //    //string mailadress = email;

        //    //try bloğu içerinde yazdım sorun çıkar ise çözüm üreteyim diye.
        //    try
        //    {
        //        //MailMessage kütüphanesinden bir instance oluşturuyoruz.
        //        MailMessage mail = new MailMessage();
        //        //Mesaj içeriğinde html ifadelere izin veriyoruz.
        //        mail.IsBodyHtml = true;
        //        //Bu kısım mail'in kime gideceğidir.Kendi adresimi yazdım.
        //        mail.To.Add(email);
        //        //Burası ise kimin göndereceğidir.Kim gönderecek?
        //        mail.From = new MailAddress(email);
        //        //Gelen mailin konusu
        //        mail.Subject = $"www.DocentHesapla.com::Şifre sıfırlama";
        //        mail.Body = "<h2>Şifrenizi yenilemek için lütfen aşağıdaki linke tıklayınız.</h2><hr/>";
        //        mail.Body += $"<a href='{link}'>şifre yenileme linki</a>";
        //        //Gelen mailin içeriği
        //        mail.IsBodyHtml = true;
        //        //Bu kısımda smtp classında instance oluşturuyoruz.
        //        SmtpClient smtp = new SmtpClient();
        //        //Burada maili gönderen kişinin mail adresi ve şifresi alınıyor.
        //        smtp.Credentials = new NetworkCredential("DocentHesapla@gmail.com", "hesaplaDocent33");
        //        //Hangi portu kullanacağımızı yazıyoruz.
        //        smtp.Port = 587;
        //        //Hangi mail adresini kullanacağızı seçiyoruz.
        //        smtp.Host = "smtp.gmail.com";
        //        //Ssl güvenlik protokolünü aktifleştiriyoruz.
        //        smtp.EnableSsl = true;
        //        //Maili gönderiyoruz.
        //        smtp.Send(mail);
        //        //return Json(true, JsonRequestBehavior.AllowGet);
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }

        //}
    }
}
