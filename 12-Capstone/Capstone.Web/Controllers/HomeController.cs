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
        //private const string UNIT_KEY = "F";
        private IParkDAO parkDAO;
        private ISurveyDAO surveyDAO;
        public HomeController(IParkDAO parkDAO, ISurveyDAO surveyDAO)
        {
            this.parkDAO = parkDAO;
            this.surveyDAO = surveyDAO;
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
            // get unit from session
            string unit = GetPreferredUnit();
            // add unit parameter

            IList<ParkWeather> weather = parkDAO.GetWeather(parkCode, unit);
            return View(weather);
        }
        [HttpPost]
        public IActionResult ToggleTemp(string parkID)
        {
            string unit = GetPreferredUnit();
           
                SetPreferredUnit(unit);
            return RedirectToAction("Forecast", "Home", new { parkCode = parkID } );
        }
        [HttpGet]
        public IActionResult AddSurvey()
        {
            return View();
        }
        [HttpPost]
        public IActionResult AddSurvey(Survey survey)
        {
            if (!ModelState.IsValid)
            {
                return View(survey);
            }
            surveyDAO.AddSurvey(survey);
            return RedirectToAction("SurveyResults");
        }
        [HttpGet]
        public IActionResult SurveyResults()
        {
            IList<SurveyResultVM> surveys = surveyDAO.SurveyResults();
            return View(surveys);
        }


        #region Private Session Methods
        private string GetPreferredUnit()
        {
            string newUnit = HttpContext.Session.GetString("TempUnit");
            if(newUnit == null)
            {
                newUnit = "F";
                HttpContext.Session.SetString("TempUnit", "F");
            }
            return newUnit;
        }

        private void SetPreferredUnit(string unit)
        {
            if (unit == "F")
            {
                HttpContext.Session.SetString("TempUnit", "C");
            }
            else
            {
                HttpContext.Session.SetString("TempUnit", "F");
            }
        }
        //private void ClearPreferredCuisine()
        //{
        //    HttpContext.Session.Remove(UNIT_KEY);
        //    This will remove everything in Session while the above will only remove things stored at CUISINE_KEY
        //    HttpContext.Session.Clear();
        //}

        #endregion
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
