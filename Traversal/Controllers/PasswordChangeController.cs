using EntityLayer.Concrete;
using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MimeKit;
using System.Threading.Tasks;
using Traversal.Models;

namespace Traversal.Controllers
{
    [AllowAnonymous]
    public class PasswordChangeController : Controller
    {
        private readonly UserManager<AppUser> _userManager;

        public PasswordChangeController(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        [HttpGet]
        public IActionResult ForgetPassword()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ForgetPassword(ForgetPasswordViewModel forgetPasswordViewModel)
        {
            var user = await _userManager.FindByEmailAsync(forgetPasswordViewModel.Mail);
            string passwordResetToken = await _userManager.GeneratePasswordResetTokenAsync(user);
            var passwordResetTokenLink = Url.Action("ResetPassword", "PasswordChange", new
            {
                userId = user.Id,
                token = passwordResetToken
            }, HttpContext.Request.Scheme);

            //MimeMessage: E-postanın içeriğini ve başlıklarını temsil eder.
            MimeMessage mimeMessage = new MimeMessage();  //Gönderilecek e-postayı temsil eden nesne oluşturulur.

            //MailboxAddress: E-posta adreslerini tanımlar (gönderen ve alıcı).

            // Mail gönderecek olan kişinin adres işlemi
            MailboxAddress mailboxAddressFrom = new MailboxAddress("Adem", "devadmck99@gmail.com");
            mimeMessage.From.Add(mailboxAddressFrom);

            // Mail alacak olan kişinin adres işlemleri
            MailboxAddress mailboxAddressTo = new MailboxAddress("User", forgetPasswordViewModel.Mail);
            mimeMessage.To.Add(mailboxAddressTo);

            //BodyBuilder: E-posta içeriğini (body) oluşturur.
            var bodyBuilder = new BodyBuilder();
            bodyBuilder.TextBody = passwordResetTokenLink;
            mimeMessage.Body = bodyBuilder.ToMessageBody();
            //Subject: E-postanın konusu, modelden (mailRequest) geliyor.
            mimeMessage.Subject = "Şifre Değişiklik Talebi";

            //SmtpClient: SMTP protokolü üzerinden e-posta göndermek için kullanılır (MailKit kütüphanesine aittir).
            SmtpClient client = new SmtpClient();
            client.Connect("smtp.gmail.com", 587, false);  //smtp.gmail.com, 587: Gmail’in SMTP sunucusu ve TLS için portu.
            client.Authenticate("devadmck99@gmail.com", "mmwv ewuv stqu mvra");
            client.Send(mimeMessage);
            client.Disconnect(true); //Disconnect: SMTP bağlantısı güvenli şekilde kapatılır.

            return View();
        }

        [HttpGet]
        public IActionResult ResetPassword(string userid, string token)
        {
            TempData["userid"] = userid;
            TempData["token"] = token;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ResetPassword(ResetPasswordViewModel resetPasswordViewModel)
        {
            var userid = TempData["userid"];
            var token = TempData["token"];
            if (userid == null || token == null)
            {
                //hata mesajı
            }
            var user = await _userManager.FindByIdAsync(userid.ToString());
            var result = await _userManager.ResetPasswordAsync(user, token.ToString(), resetPasswordViewModel.Password);
            if (result.Succeeded)
            {
                return RedirectToAction("SignIn", "Login");
            }

            return View();
        }
    }
}
