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
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Drawing;
using Microsoft.AspNetCore.Hosting;

namespace JuliRennen.Controllers
{
    public class RouteController : Controller
    {
        private JuliRennenContext _context;
        private IWebHostEnvironment Environment;


        public RouteController(JuliRennenContext context, IWebHostEnvironment _environment)
        {
            _context = context;
            Environment = _environment;
        }
        // 
        // GET: RunRoute/
        //Returns route data to map
        [HttpPost]
        public JsonResult GetRoute()
        {
            //Use context to get data from DB and send as JSON
            
            return Json(_context.Route);
        }

        [HttpPost]
        public JsonResult GetSpecificRoute(string data)
        {

            foreach (Route route in _context.Route)
            {
                if(route.Name == data)
                {
                    return Json(route);
                }
            }
            return Json("No Route found");
        }




        [HttpPost]
        public ActionResult acceptRoute([FromForm] string Photo, [FromForm] string Name, [FromForm] string Distance, [FromForm] string GPSyStart, [FromForm] string GPSyEnd, [FromForm] string GPSxStart, [FromForm] string GPSxEnd, [FromForm] string FileLoc)
        {
            
            Route NewRoute = new Route();


            var id = HttpContext.Session.GetInt32("UserID");
            var s = _context.User.Find(id);

            if (_context.Route.Any(o => o.Name == Name)){
                var result = _context.Route.SingleOrDefault((o => o.Name == Name));
                result.Name = Name;
                result.Distance = Double.Parse(Distance, CultureInfo.InvariantCulture);
                result.GPSyStart = Double.Parse(GPSyStart, CultureInfo.InvariantCulture);
                result.GPSyEnd = Double.Parse(GPSyEnd,CultureInfo.InvariantCulture);
                result.GPSxStart = Double.Parse(GPSxStart, CultureInfo.InvariantCulture);
                result.GPSxEnd = Double.Parse(GPSxEnd, CultureInfo.InvariantCulture);
                result.User = s;
                result.Photo = Photo;
                _context.SaveChanges();
                ViewBag.Message = _context.Route;
                return View("SeeRoutesRedirect", ViewBag.Message);
            }
           
            NewRoute.Name = Name;
            NewRoute.Distance = Double.Parse(Distance, CultureInfo.InvariantCulture);
            NewRoute.GPSyStart = Double.Parse(GPSyStart, CultureInfo.InvariantCulture);
            NewRoute.GPSyEnd = Double.Parse(GPSyEnd, CultureInfo.InvariantCulture);
            NewRoute.GPSxStart = Double.Parse(GPSxStart, CultureInfo.InvariantCulture);
            NewRoute.GPSxEnd =  Double.Parse(GPSxEnd, CultureInfo.InvariantCulture);
            NewRoute.User = s;
            NewRoute.Photo = Photo; 
            _context.Route.Add(NewRoute);
            _context.SaveChanges();
            ViewBag.Message = _context.Route;
            return View("SeeRoutesRedirect", ViewBag.Message);
        }


        [HttpPost]
        public ActionResult Index([FromForm] string RouteName, [FromForm] string PhotoData, [FromForm] string fileName, [FromForm] string Distance, [FromForm] string GPSyStart, [FromForm] string GPSyEnd, [FromForm] string GPSxStart, [FromForm] string GPSxEnd)
        {
            Route NewRoute = new Route();
            if(RouteName != null) 
            { NewRoute.Name = RouteName; } 
            else
            { RouteName = " "; }
            NewRoute.Name = RouteName;
            NewRoute.Distance = Convert.ToDouble(Distance);
            NewRoute.GPSyStart = Convert.ToDouble(GPSyStart);
            NewRoute.GPSyEnd = Convert.ToDouble(GPSyEnd);
            NewRoute.GPSxStart = Convert.ToDouble(GPSxStart);
            NewRoute.GPSxEnd = Convert.ToDouble(GPSxEnd);
            string filepath = ".";
            if (PhotoData != null && fileName != null)
            {
                string base64 = PhotoData.Substring(PhotoData.LastIndexOf(',') + 1);
                // Image image = MakeImage(base64);
                string upload = Path.Combine("wwwroot", "images");
                filepath = Path.Combine(upload, fileName);
                filepath = Path.Combine(upload, fileName);
                byte[] newBytes = Convert.FromBase64String(base64);
                System.IO.File.WriteAllBytes(filepath, newBytes);

            }
            NewRoute.Photo = filepath;
            ViewBag.Message = NewRoute;
            TempData["Photo"] = filepath;
            TempData["RouteName"] = RouteName;
            return View();
        }

        public void MakeImage(string base64)
        {
            base64 = HttpUtility.UrlDecode(base64);
           // byte[] imageBytes = Convert.TryFromBase64String(base64);
            
            /* using (var ms = new MemoryStream(imageBytes, 0, imageBytes.Length))
             {
                 Image image = Image.FromStream(ms, true);
                 return image;
             }*/
        }
      

        [Authorize]
        public ActionResult WelcomeUser()
        {
            return View();
        }

        [Authorize]
        public ActionResult SeeRoutes()
        {
            ViewBag.Message = _context.Route;
            return View();
        }

        [Authorize]
        public ActionResult SeeRoutesRedirect(dynamic data)
        {
            ViewBag.Message = data;
            return View("SeeRoutes");
        }

    }
}


