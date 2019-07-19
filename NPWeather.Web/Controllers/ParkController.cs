using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Capstone.Web.DAL;
using Capstone.Web.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Capstone.Web.Controllers
{
    public class ParkController : Controller
    {
        private IParkDAO dao;
        private IWeatherForecastDAO wdao;

        public ParkController(IParkDAO dao, IWeatherForecastDAO wdao)
        {
            this.dao = dao;
            this.wdao = wdao;
        }

        public IActionResult Index()
        {
            IList<Park> parks = dao.GetAllParks();
            return View(parks);
        }

        [HttpGet]
        public IActionResult Detail(string id)
        {
            Park parks = dao.GetPark(id);
            ViewBag.ForecastList = wdao.GetWeatherForecasts(id);

            if (HttpContext.Session.Keys.Contains("scale"))
            {
                ViewData["scale"] = HttpContext.Session.GetString("scale");
            }
            else
            {
                ViewData["scale"] = "Fahrenheit";
            }

            return View(parks);
        }

        public IActionResult Toggle(string id, string scale)
        {
            HttpContext.Session.SetString("scale", scale);

            return RedirectToAction("Detail", "Park", new { id = id });
        }
     
    }
}