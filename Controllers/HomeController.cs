using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using VCubWatcher.Models;

namespace VCubWatcher.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Carte()
        {
            var station = GetBikeStationListFromApi();
            return View(station);
        }
        public IActionResult Favoris()
        {
            return View();
        }
        public IActionResult ListeStations()
        {
            var station = GetBikeStationListFromApi();
            return View(station);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        private static readonly HttpClient client = new HttpClient();
        private static List<BikeStation> GetBikeStationListFromApi()
        {
            var stringTask = client.GetStringAsync("https://api.alexandredubois.com/vcub-backend/vcub.php");
            var myJsonResponse = stringTask.Result;
            var result = JsonConvert.DeserializeObject<List<BikeStation>>(myJsonResponse);
            return result;
        }
    }
}
