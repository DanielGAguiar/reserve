using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Reserve_Covid.Models;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Reserve_Covid.Controllers
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
            var client = new RestClient("https://api.covid19api.com/summary");
            client.Timeout = -1;
            var request = new RestRequest(Method.GET);
            IRestResponse response = client.Execute(request);

            Paises ListaPaises = JsonConvert.DeserializeObject<Paises>(response.Content);

            int posicao = 1;

            ListaPaises.Countries = ListaPaises.Countries.OrderByDescending(o => o.TotalAtivos).Take(10).ToList();

            ListaPaises.Countries = ListaPaises.Countries.Select(s => new Pais()
            {
                Country = s.Country,
                Posicao = posicao++,                
                TotalAtived = s.TotalAtivos
            }).ToList();


            return View(ListaPaises.Countries);
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
