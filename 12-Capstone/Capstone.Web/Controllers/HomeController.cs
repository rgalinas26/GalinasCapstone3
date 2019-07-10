using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Capstone.Web.Models;
using Capstone.Web.DAL;
using Microsoft.AspNetCore.Http;

namespace Capstone.Web.Controllers
{
    public class HomeController : Controller
    {
        private const string UNIT_KEY = "F";
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
        public IActionResult Forecast(string parkCode, string unit)
        {
            // get unit from session
            // add unit parameter
            IList<ParkWeather> weather = parkDAO.GetWeather(parkCode, unit);
            return View(weather);
        }
        private string GetPreferredUnit()
        {
            return HttpContext.Session.GetString(UNIT_KEY) ?? "";
        }

        private void SetPreferredUnit(string unit)
        {
            if (cuisine == null)
            {
                ClearPreferredCuisine();
            }
            else
            {
                HttpContext.Session.SetString(UNIT_KEY, cuisine);
            }
        }
        private void ClearPreferredCuisine()
        {
            HttpContext.Session.Remove(UNIT_KEY);
            //This will remove everything in Session while the above will only remove things stored at CUISINE_KEY
            //HttpContext.Session.Clear();
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
