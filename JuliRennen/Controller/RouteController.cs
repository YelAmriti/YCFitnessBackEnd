﻿using Microsoft.AspNetCore.Mvc;
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
        public ActionResult acceptRoute([FromForm] string Photo, [FromForm] string Name, [FromForm] string Distance, [FromForm] string GPSyStart, [FromForm] string GPSyEnd, [FromForm] string GPSxStart, [FromForm] string GPSxEnd, [FromForm] string FileLoc)
        {
            Route NewRoute = new Route();

            NewRoute.Name = Name;
            NewRoute.Distance = Convert.ToDouble(Distance);
            NewRoute.GPSyStart = Convert.ToDouble(GPSyStart);
            NewRoute.GPSyEnd = Convert.ToDouble(GPSyEnd);
            NewRoute.GPSxStart = Convert.ToDouble(GPSxStart);
            NewRoute.GPSxEnd = Convert.ToDouble(GPSxEnd);
            var id = HttpContext.Session.GetInt32("UserID");
            User s = _context.User.Find(id);
            NewRoute.User = s;
            Photo = "../" + Photo.Substring(Photo.LastIndexOf('t') + 1);
            NewRoute.Photo = Photo; 

            _context.Route.Add(NewRoute);
            _context.SaveChanges();
            ViewBag.Message = _context.Route;
            return View("SeeRoutesRedirect", ViewBag.Message);
        }


        [HttpPost]
        public ActionResult Index([FromForm] string PhotoData, [FromForm] string fileName, [FromForm] string Distance, [FromForm] string GPSyStart, [FromForm] string GPSyEnd, [FromForm] string GPSxStart, [FromForm] string GPSxEnd)
        {
            Route NewRoute = new Route();
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


