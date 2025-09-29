using EntityLayer.Concrete;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using System.Threading.Tasks;
using Traversal.Areas.Member.Models;

namespace Traversal.Areas.Member.Controllers
{
    [Area("Member")]
    [Route("Member/[controller]/[action]")]
    public class ProfileController : Controller
    {
        private readonly UserManager<AppUser> _userManager;

        public ProfileController(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var values = await _userManager.FindByNameAsync(User.Identity.Name);
            UserEditViewModel userEditViewModel = new UserEditViewModel();
            userEditViewModel.name = values.Name;
            userEditViewModel.surname = values.Surname;
            userEditViewModel.phonenumber = values.PhoneNumber;
            userEditViewModel.mail = values.Email;
            return View(userEditViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Index(UserEditViewModel p)
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name); // Şu an sistemde oturum açmış olan kullanıcıyı User.Identity.Name (kullanıcı adı) ile bulur.
            if (p.Image != null)
            {
                var resource = Directory.GetCurrentDirectory();  // Uygulamanın çalıştığı klasör
                var extension = Path.GetExtension(p.Image.FileName);  // Dosya uzantısını al (.jpg, .png vs.)
                var imagename = Guid.NewGuid() + extension;  // Benzersiz bir dosya adı üret
                var savelocation = resource + "/wwwroot/userimages/" + imagename;  // Kaydedileceği tam yol
                var stream = new FileStream(savelocation, FileMode.Create);  // Dosya için akış aç
                await p.Image.CopyToAsync(stream);  // Dosyayı belirtilen klasöre kopyala
                user.ImageUrl = imagename;  // Kullanıcıya görsel adını ata
            }
            user.Name = p.name;
            user.Surname = p.surname;
            user.PasswordHash = _userManager.PasswordHasher.HashPassword(user, p.password);
            var result = await _userManager.UpdateAsync(user);
            if (result.Succeeded)
            {
                return RedirectToAction("SignIn", "Login");
            }
            return View();
        }
    }
}
