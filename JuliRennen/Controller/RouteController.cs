using Microsoft.AspNetCore.Mvc;
using JuliRennen.Models;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Authorization;
using System.Globalization;
using JuliRennen.Data;
using Route = JuliRennen.Models.Route;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Web;


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

        [HttpPost]
        public ActionResult acceptRoute(IFormFile Photo, [FromForm] string Name, [FromForm] string Distance, [FromForm] string GPSyStart, [FromForm] string GPSyEnd, [FromForm] string GPSxStart, [FromForm] string GPSxEnd, [FromForm] string FileLoc)
        {
            Route NewRoute = new Route();
            //Create Developer User
            User dev = new User();
            NewRoute.Name = Name;
            NewRoute.Distance = Convert.ToDouble(Distance);
            NewRoute.GPSyStart = Convert.ToDouble(GPSyStart);
            NewRoute.GPSyEnd = Convert.ToDouble(GPSyEnd);
            NewRoute.GPSxStart = Convert.ToDouble(GPSxStart);
            NewRoute.GPSxEnd = Convert.ToDouble(GPSxEnd);

            //Save Photo
            string upload = Path.Combine("wwwroot", "images");
            string filepath = Path.Combine(upload, Photo.FileName);
            using (Stream FileStream = new FileStream(filepath, FileMode.Create, FileAccess.Write))
            {
                Photo.CopyTo(FileStream);
            }
            NewRoute.Photo = filepath;
            _context.Add(NewRoute);
            _context.SaveChanges();
            return View("WelcomeUser");
        }

        [HttpPost]
        public ActionResult Index([FromForm] string Photo, [FromForm] string Distance, [FromForm] string GPSyStart, [FromForm] string GPSyEnd, [FromForm] string GPSxStart, [FromForm] string GPSxEnd)
        {
            Route NewRoute = new Route();
            NewRoute.Distance = Convert.ToDouble(Distance);
            NewRoute.GPSyStart = Convert.ToDouble(GPSyStart);
            NewRoute.GPSyEnd = Convert.ToDouble(GPSyEnd);
            NewRoute.GPSxStart = Convert.ToDouble(GPSxStart);
            NewRoute.GPSxEnd = Convert.ToDouble(GPSxEnd);
            NewRoute.Photo = Photo;
            ViewBag.Message = NewRoute;
            //ViewData["Photo"] = NewRoute.Photo;
            return View();
        }

        public ActionResult WelcomeUser()
        {
            return View();
        }

        public ActionResult SeeRoutes()
        {
            ViewBag.Message = _context.Route;
            return View();
        }

    }
}


