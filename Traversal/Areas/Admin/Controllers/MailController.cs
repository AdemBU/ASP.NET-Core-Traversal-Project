using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Mvc;
using MimeKit;
using Traversal.Models;

namespace Traversal.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class MailController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(MailRequest mailRequest)
        {
            //MimeMessage: E-postanın içeriğini ve başlıklarını temsil eder.
            MimeMessage mimeMessage = new MimeMessage();  //Gönderilecek e-postayı temsil eden nesne oluşturulur.

            //MailboxAddress: E-posta adreslerini tanımlar (gönderen ve alıcı).

            // Mail gönderecek olan kişinin adres işlemi
            MailboxAddress mailboxAddressFrom = new MailboxAddress("Adem", "devadmck99@gmail.com");
            mimeMessage.From.Add(mailboxAddressFrom);

            // Mail alacak olan kişinin adres işlemleri
            MailboxAddress mailboxAddressTo = new MailboxAddress("User", mailRequest.ReceiverMail);
            mimeMessage.To.Add(mailboxAddressTo);

            //BodyBuilder: E-posta içeriğini (body) oluşturur.
            var bodyBuilder = new BodyBuilder();
            bodyBuilder.TextBody = mailRequest.Body;
            mimeMessage.Body = bodyBuilder.ToMessageBody();
            //Subject: E-postanın konusu, modelden (mailRequest) geliyor.
            mimeMessage.Subject = mailRequest.Subject;

            //SmtpClient: SMTP protokolü üzerinden e-posta göndermek için kullanılır (MailKit kütüphanesine aittir).
            SmtpClient client = new SmtpClient();
            client.Connect("smtp.gmail.com", 587, false);  //smtp.gmail.com, 587: Gmail’in SMTP sunucusu ve TLS için portu.
            client.Authenticate("devadmck99@gmail.com", "mmwv ewuv stqu mvra");
            client.Send(mimeMessage);
            client.Disconnect(true); //Disconnect: SMTP bağlantısı güvenli şekilde kapatılır.

            return View();
        }
    }
}
