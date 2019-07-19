using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Capstone.Web.DAL;
using Capstone.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Capstone.Web.Controllers
{
    public class SurveyController : Controller
    {
        private ISurveyDAO dao;
        private IParkDAO pdao;

        public SurveyController(ISurveyDAO dao, IParkDAO pdao)
        {
            this.dao = dao;
            this.pdao = pdao;
        }

        [HttpGet]
        public IActionResult Index()
        {
            Dictionary<string, string> namesAndCodes = GetParkCodesAndNames();
            IList<SelectListItem> items = ParkSelectListItems(namesAndCodes);
            ViewBag.NamesAndCodes = items;
            ViewBag.Activity = activitySelectListItems;
            ViewBag.States = states;
            Survey survey = new Survey();
            return View(survey);
        }


        [HttpGet]
        public IActionResult RankedResults()
        {
            IList<Park> parks = pdao.GetRankedParks();
            return View(parks);
        }

        [HttpPost]
        public IActionResult RankedResults(Survey survey)
        {
            if (!ModelState.IsValid)
            {
                Dictionary<string, string> namesAndCodes = GetParkCodesAndNames();
                IList<SelectListItem> items = ParkSelectListItems(namesAndCodes);
                ViewBag.NamesAndCodes = items;
                ViewBag.Activity = activitySelectListItems;
                ViewBag.States = states;
                return View("Index", survey);
            }

            dao.SubmitSurvey(survey);
            IList<Park> parks = pdao.GetRankedParks();
            return View(parks);
        }

        public Dictionary<string, string> GetParkCodesAndNames()
        {
            IList<Park> parks = pdao.GetAllParks();

            Dictionary<string, string> namesAndCodes = new Dictionary<string, string>();

            foreach (Park park in parks)
            {
                namesAndCodes[park.ParkName] = park.ParkCode;
            }

            return namesAndCodes;
        }

        public static IList<SelectListItem> ParkSelectListItems(Dictionary<string, string> parks)
        {
            IList<SelectListItem> items = new List<SelectListItem>();

            items.Add(new SelectListItem() { Text = "Select", Selected = true, Disabled = true });

            foreach (KeyValuePair<string, string> kvp in parks)
            {
                items.Add(new SelectListItem() { Text = kvp.Key, Value = kvp.Value });
            }

            return items;
        }

        private static IList<SelectListItem> activitySelectListItems = new List<SelectListItem>()
        {
            new SelectListItem() { Text = "Select", Selected = true, Disabled = true },
            new SelectListItem() { Text = "inactive" },
            new SelectListItem() { Text = "sedentary" },
            new SelectListItem() { Text = "active" },
            new SelectListItem() { Text = "extremely active" }
        };

        private static IList<SelectListItem> states = new List<SelectListItem>()
        {
            new SelectListItem() { Text = "Select", Selected = true, Disabled = true},
            new SelectListItem() {Text="Alabama", Value="AL"},
            new SelectListItem() { Text="Alaska", Value="AK"},
            new SelectListItem() { Text="Arizona", Value="AZ"},
            new SelectListItem() { Text="Arkansas", Value="AR"},
            new SelectListItem() { Text="California", Value="CA"},
            new SelectListItem() { Text="Colorado", Value="CO"},
            new SelectListItem() { Text="Connecticut", Value="CT"},
            new SelectListItem() { Text="District of Columbia", Value="DC"},
            new SelectListItem() { Text="Delaware", Value="DE"},
            new SelectListItem() { Text="Florida", Value="FL"},
            new SelectListItem() { Text="Georgia", Value="GA"},
            new SelectListItem() { Text="Hawaii", Value="HI"},
            new SelectListItem() { Text="Idaho", Value="ID"},
            new SelectListItem() { Text="Illinois", Value="IL"},
            new SelectListItem() { Text="Indiana", Value="IN"},
            new SelectListItem() { Text="Iowa", Value="IA"},
            new SelectListItem() { Text="Kansas", Value="KS"},
            new SelectListItem() { Text="Kentucky", Value="KY"},
            new SelectListItem() { Text="Louisiana", Value="LA"},
            new SelectListItem() { Text="Maine", Value="ME"},
            new SelectListItem() { Text="Maryland", Value="MD"},
            new SelectListItem() { Text="Massachusetts", Value="MA"},
            new SelectListItem() { Text="Michigan", Value="MI"},
            new SelectListItem() { Text="Minnesota", Value="MN"},
            new SelectListItem() { Text="Mississippi", Value="MS"},
            new SelectListItem() { Text="Missouri", Value="MO"},
            new SelectListItem() { Text="Montana", Value="MT"},
            new SelectListItem() { Text="Nebraska", Value="NE"},
            new SelectListItem() { Text="Nevada", Value="NV"},
            new SelectListItem() { Text="New Hampshire", Value="NH"},
            new SelectListItem() { Text="New Jersey", Value="NJ"},
            new SelectListItem() { Text="New Mexico", Value="NM"},
            new SelectListItem() { Text="New York", Value="NY"},
            new SelectListItem() { Text="North Carolina", Value="NC"},
            new SelectListItem() { Text="North Dakota", Value="ND"},
            new SelectListItem() { Text="Ohio", Value="OH"},
            new SelectListItem() { Text="Oklahoma", Value="OK"},
            new SelectListItem() { Text="Oregon", Value="OR"},
            new SelectListItem() { Text="Pennsylvania", Value="PA"},
            new SelectListItem() { Text="Rhode Island", Value="RI"},
            new SelectListItem() { Text="South Carolina", Value="SC"},
            new SelectListItem() { Text="South Dakota", Value="SD"},
            new SelectListItem() { Text="Tennessee", Value="TN"},
            new SelectListItem() { Text="Texas", Value="TX"},
            new SelectListItem() { Text="Utah", Value="UT"},
            new SelectListItem() { Text="Vermont", Value="VT"},
            new SelectListItem() { Text="Virginia", Value="VA"},
            new SelectListItem() { Text="Washington", Value="WA"},
            new SelectListItem() { Text="West Virginia", Value="WV"},
            new SelectListItem() { Text="Wisconsin", Value="WI"},
            new SelectListItem() { Text="Wyoming", Value="WY"}
        };
    }
}