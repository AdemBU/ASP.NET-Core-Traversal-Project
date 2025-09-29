using DocumentFormat.OpenXml.Office2010.ExcelAc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Unicode;
using System.Threading.Tasks;
using Traversal.Areas.Admin.Models;

namespace Traversal.Areas.Admin.Controllers
{
    [AllowAnonymous]
    [Area("Admin")]
    public class VisitorApiController : Controller
    {
        // bu sınıf api olmayacak, bu sınıf api'yi confin edicek yani tüketicek

        private readonly IHttpClientFactory _httpClientFactory; //HttpClient üretmek için kullanılan bir yapı.

        public VisitorApiController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        //DeserializeObject, Json formatında olan string'i bir object'e çevirir.
        //SerializeObject ise bir object'i Json türünde bir string degere çevirir.

        // DeserializeObject: Listeleme ve veri getirme 
        // SerializeObject: Ekleme ve güncelleme

        public async Task<IActionResult> Index()  //listeleme
        {
            var client = _httpClientFactory.CreateClient();  //yeni bir HttpClient nesnesi oluşturur.
            var responseMessage = await client.GetAsync("http://localhost:3794/api/Visitor");  // GetAsync → API’den veri çeker.
            //Gelen Cevabı Kontrol Etme
            if (responseMessage.IsSuccessStatusCode)  //API’den 200 OK gibi başarılı yanıt geldi mi kontrol ediyor.
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();  //API’nin döndürdüğü JSON verisini string olarak alıyor.
                var values = JsonConvert.DeserializeObject<List<VisitorViewModel>>(jsonData); //JSON’u C# model listesine dönüştürüyor (VisitorViewModel tipine).
                return View(values);
            }
            return View();
        }

        [HttpGet]
        public IActionResult AddVisitor()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddVisitor(VisitorViewModel p)
        {
            var client = _httpClientFactory.CreateClient(); // API’ye bu client ile istek atılacak.
            var jsonData = JsonConvert.SerializeObject(p);  //(C# nesnesi) JSON string haline çevriliyor.
            StringContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");
            //API’ye göndereceğimiz gövde (body) hazırlanıyor. İçerik: JSON(jsonData)  Kodlama: UTF-8  İçerik tipi: application/json
            var responseMessage = await client.PostAsync("http://localhost:3794/api/Visitor", content); //PostAsync → API’ye POST isteği atar.
            if (responseMessage.IsSuccessStatusCode)
            {
                //API’den başarılı yanıt geldiyse (200 OK, 201 Created gibi), kullanıcı Index action’ına yönlendirilir.
                return RedirectToAction("Index");
            }
            return View();
        }

        public async Task<IActionResult> DeleteVisitor(int id)
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.DeleteAsync($"http://localhost:3794/api/Visitor/{id}");
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> UpdateVisitor(int id)
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync($"http://localhost:3794/api/Visitor/{id}");
            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<VisitorViewModel>(jsonData);
                return View(values);
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> UpdateVisitor(VisitorViewModel p)
        {
            var client = _httpClientFactory.CreateClient();
            var jsonData = JsonConvert.SerializeObject(p);
            StringContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var responseMessage = await client.PutAsync("http://localhost:3794/api/Visitor", content);
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View();
        }
    }
}
