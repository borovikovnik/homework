using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using DotNetOpenAuth.AspNet;
using Microsoft.Web.WebPages.OAuth;
using WebMatrix.WebData;
using jpggram.Filters;
using jpggram.Models;
using System.IO;

namespace jpggram.Controllers
{
    [Authorize]
    [InitializeSimpleMembership]
    public class PhotosController : Controller
    {
        UsersContext db = new UsersContext();

        public ActionResult Index()
        {
            ViewBag.Photos = db.Photos
                               .Where(c => c.UserID == WebSecurity.CurrentUserId)
                               .OrderByDescending(c => c.PhotoId);
            return View();
        }

        public ActionResult Delete(int id)
        {
            Photo photo = db.Photos.Find(id);

            if (photo.UserID == WebSecurity.CurrentUserId)
            {
                db.Photos.Remove(db.Photos.Find(id));
                db.SaveChanges();
            }
            else
            {
                ViewBag.Error = "Иллюминати что-то сломали!";
            }

            return RedirectToAction("/");
        }

        public ActionResult FileUpload(HttpPostedFileBase file)
        {
            if (file != null)
            {
                string pic = System.IO.Path.GetFileName(file.FileName);
                string path = System.IO.Path.Combine(
                                       Server.MapPath("~/images/profile"), pic);
                file.SaveAs(path);

                using (MemoryStream ms = new MemoryStream())
                {
                    file.InputStream.CopyTo(ms);
                    byte[] array = ms.GetBuffer();
                }

                Photo photo = new Photo();
                photo.UserID = WebSecurity.CurrentUserId;
                photo.Date = DateTime.Now;
                photo.Url = "/images/profile/" + file.FileName;
                db.Photos.Add(photo);
                db.SaveChanges();
            }
            return RedirectToAction("Index", "Photos");
        }

    }
}