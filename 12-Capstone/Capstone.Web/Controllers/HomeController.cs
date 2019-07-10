using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Capstone.Web.Models;
using Capstone.Web.DAL;

namespace Capstone.Web.Controllers
{
    public class HomeController : Controller
    {
        private IParkDAO parkDAO;
        public HomeController(IParkDAO parkDAO)
        {
            this.parkDAO = parkDAO;
        }
        public IActionResult Index()
        {
            IList<Park> parks = parkDAO.GetAllParks();
            return View(parks);
        }
        public IActionResult Detail(string parkCode)
        {
            Park park = parkDAO.GetParkById(parkCode);
            return View(park);
        }
        public IActionResult Forecast(string parkCode)
        {
            IList<ParkWeather> weather = parkDAO.GetWeather(parkCode);
            return View(weather);
        }

      

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
