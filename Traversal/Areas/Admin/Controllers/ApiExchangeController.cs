using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System;
using Microsoft.AspNetCore.Authorization;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using DocumentFormat.OpenXml.Office2010.ExcelAc;
using Traversal.Areas.Admin.Models;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Traversal.Areas.Admin.Controllers
{
    [Area("Admin")]
    [AllowAnonymous]
    public class ApiExchangeController : Controller
    {
        public async Task<IActionResult> Index()
        {
            List<BookingExchangeViewModel2> bookingExchangeViewModel = new List<BookingExchangeViewModel2>();
            var client = new HttpClient();
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri("https://booking-com.p.rapidapi.com/v1/metadata/exchange-rates?currency=TRY&locale=en-gb"),
                Headers =
    {
        { "x-rapidapi-key", "e8a5c01167mshb2bcfd2a2797ec0p10a723jsnca477cd2d01b" },
        { "x-rapidapi-host", "booking-com.p.rapidapi.com" },
    },
            };
            using (var response = await client.SendAsync(request))
            {
                response.EnsureSuccessStatusCode();
                var body = await response.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<BookingExchangeViewModel2>(body);
                return View(values.exchange_rates);
            }
        }
    }
}
