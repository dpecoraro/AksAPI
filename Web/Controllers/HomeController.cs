using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Web.Models;

namespace Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            try
            {

                var httpClient = new HttpClient();

                var req = await httpClient.GetAsync("http://aksapi/WeatherForecast");

                if (req.IsSuccessStatusCode)
                {
                    var response = await req.Content.ReadAsStringAsync();

                    var forecast = JsonConvert.DeserializeObject<List<WeatherForecast>>(response);

                    var viewModel = new WeatherForecastViewModel { Forecasts = forecast };

                    return View(viewModel);
                }

                return View(new WeatherForecastViewModel { Forecasts = new List<WeatherForecast>() });
            }
            catch (Exception ex)
            {
                return View(new WeatherForecastViewModel { Forecasts = new List<WeatherForecast>() });
            }
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
