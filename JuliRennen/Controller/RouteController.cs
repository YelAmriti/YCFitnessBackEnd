using Microsoft.AspNetCore.Mvc;
using JuliRennen.Models;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Authorization;
using System.Globalization;
using JuliRennen.Data;
using Route = JuliRennen.Models.Route;

namespace JuliRennen.Controllers
{
    public class RouteController : Controller
    {
        private JuliRennenContext _context;

        public RouteController(JuliRennenContext context)
        {
            _context = context;
        }
        // 
        // GET: RunRoute/
        //Returns route data to map
        [HttpPost]
        public JsonResult returnRoute(string userID)
        {
            //Use context to get data from DB and send as JSON
            Route run = new Route();
            run.Name = "Arel";
            run.ID = 2;
            run.GPSxStart = 52.45;
            run.GPSxEnd = 53.45;
            run.GPSyStart = 43.34;
            run.GPSyEnd = 34.32;
            run.Photo = "Hi";
            return Json(run);
        }

        // 
        // GET: /RunRoute/Welcome/ 
        /*
        [HttpPost]
        public void acceptRoute(string data)
        {
            dynamic results = JsonConvert.DeserializeObject(data);
            Route route = new Route();
            route.ID = 0;
            route.Name = "User Chosen";
            route.Photo = results.Photo;
            route.Distance = Convert.ToDouble(results.Distance);
            route.GPSxStart = Convert.ToDouble(results.GPSxStart);
            route.GPSxEnd = Convert.ToDouble(results.GPSxEnd);
            route.GPSyStart = Convert.ToDouble(results.GPSyStart);
            route.GPSyEnd = Convert.ToDouble(results.GPSyEnd);

            _context.Add(route);
            _context.SaveChanges();

        }*/

        [HttpPost]
        public string acceptRoute([FromForm] string Name, [FromForm] string Distance, [FromForm] string GPSyStart, [FromForm] string GPSyEnd, [FromForm] string GPSxStart, [FromForm] string GPSxEnd)
        {
            Route NewRoute = new Route();
            NewRoute.Name = Name;
            NewRoute.Photo = "Yes";
            NewRoute.Distance = Convert.ToDouble(Distance);
            NewRoute.GPSyStart = Convert.ToDouble(GPSyStart);
            NewRoute.GPSyEnd = Convert.ToDouble(GPSyEnd);
            NewRoute.GPSxStart = Convert.ToDouble(GPSxStart);
            NewRoute.GPSxEnd = Convert.ToDouble(GPSxEnd);

            _context.Add(NewRoute);
            _context.SaveChanges();
            return "Hello I'm Done";
        }

        [HttpPost]
        public ActionResult Index([FromForm] string Distance, [FromForm] string GPSyStart, [FromForm] string GPSyEnd, [FromForm] string GPSxStart, [FromForm] string GPSxEnd)
        {
            Route NewRoute = new Route();
            NewRoute.Distance = Convert.ToDouble(Distance);
            NewRoute.GPSyStart = Convert.ToDouble(GPSyStart);
            NewRoute.GPSyEnd = Convert.ToDouble(GPSyEnd);
            NewRoute.GPSxStart = Convert.ToDouble(GPSxStart);
            NewRoute.GPSxEnd = Convert.ToDouble(GPSxEnd);
            ViewBag.Message = NewRoute;
            return View();
        }

/*
        public ActionResult Create(Route userRoute)
        {
            return View("~/Pages/Routes/Create.cshtml", userRoute);
        }*/
    }
}


