using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using jpggram.Filters;
using jpggram.Models;
using WebMatrix.WebData;

namespace jpggram.Controllers
{
    public class HomeController : Controller
    {
        UsersContext db = new UsersContext();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Contact(int? id)
        {
            ViewBag.Users = db.UserProfiles;
            ViewBag.Message = "Другие любители джипега";

                ViewBag.Photos = db.Photos
                                   .Where(c => c.UserID == id)
                                   .OrderByDescending(c => c.PhotoId);
            return View();
        }
    }
}
