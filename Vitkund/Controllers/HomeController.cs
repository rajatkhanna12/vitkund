using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using Vitkund.Models;

namespace Vitkund.Controllers
{
    public class HomeController : Controller
    {
        #region cshtml pages

        [Route("Index")]
        [Route("")]
        public ActionResult Index()
        {
            return View();
        }

        [Route("About-us")]
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }
        [Route("Business-ideas")]
        public ActionResult BusinessIdeas()
        {
            ViewBag.Message = "Your contact page.";
            VitkundEntities db = new VitkundEntities();
            var businessIdeas = db.tblBusinessideas.ToList();
            return View(businessIdeas);
        }
        [Route("Contact-us")]
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        [Route("Course")]
        public ActionResult Course()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        [Route("Course-details")]
        public ActionResult CourseDetails()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        [Route("Faq")]
        public ActionResult Faq()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        [Route("Gallery")]
        public ActionResult Gallery()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        [Route("Login")]
        public ActionResult Login()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        [Route("Motivational-quotes")]
        public ActionResult Motivationalquotes()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        [Route("Motivational-story")]
        public ActionResult Motivationalstory()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        [Route("Multiple-choice")]
        public ActionResult Multiplechoice()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        [Route("Plan")]
        public ActionResult Plan()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        [Route("Our-services")]
        public ActionResult OurServices()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        [Route("refer-to-friend")]
        public ActionResult Refertofriend()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        [Route("Result")]
        public ActionResult Result()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        [Route("Signup")]
        public ActionResult Signup()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        [Route("Videos")]
        public ActionResult Videos()
        {
            VitkundEntities db = new VitkundEntities();
            ViewBag.tblchapters = db.tblChapters.ToList();
            ViewBag.Message = "Your contact page.";
            var res = db.tblVideos.ToList();
            return View(res);
        }
        [Route("Vitkund")]
        public ActionResult Vitkund()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        [Route("Vitkund-gaon")]
        public ActionResult Vitkundgaon()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        [Route("AdminIndex")]
        public ActionResult AdminIndex()
        {
            ViewBag.Message = "Your Admin Index page.";
            return View();
        }
        #endregion

        #region AddBusinessIdeas
        [Route("Add-Businessideas")]
        public ActionResult AddBusinessIdeas()
        {
            VitkundEntities db = new VitkundEntities();
            var res = db.tblBusinessideas.ToList();
            return View(res);
        }
        [HttpPost]
        public ActionResult AddBusinessIdeas(tblBusinessidea tblBusinessidea)
        {
            VitkundEntities db = new VitkundEntities();
            db.tblBusinessideas.Add(tblBusinessidea);
            db.SaveChanges();
            return Json(new { success = true, message = "Data saved successfully" });
        }
        [HttpPost]
        public ActionResult UploadFile(HttpPostedFileBase file)
        {
            if (file != null && file.ContentLength > 0)
            {
                var firstfilename = string.Format(@"{0}", Guid.NewGuid());// For Create random path for prevent duplicate name.
                var fileName = Path.GetFileName(file.FileName);
                var filePath = Path.Combine(Server.MapPath("~/Content/Uploads"), firstfilename + fileName);
                var imagepathforserver = firstfilename + fileName;
                file.SaveAs(filePath);
                return Content(imagepathforserver);
            }

            return Content("No file selected.");
        }
        [HttpPost]
        public ActionResult DeleteBusinessIdea(string Id)
        {
            VitkundEntities db = new VitkundEntities();
            tblBusinessidea tblbusinessidea = db.tblBusinessideas.Find(Convert.ToInt32(Id));
            if (tblbusinessidea == null)
                return HttpNotFound();
            else
            {
                db.tblBusinessideas.Remove(tblbusinessidea);
                db.SaveChanges();
            }
            return Json(new { success = true, message = "Data Deleted successfully" });
        }
        #endregion

        #region Videos
        [Route("Add-Videos")]
        public ActionResult AddVideos()
        {
            VitkundEntities db = new VitkundEntities();
            ViewBag.tblchapters = db.tblChapters.ToList();
            var res = db.tblVideos.ToList();
            return View(res);
        }
        [HttpPost]
        public ActionResult AddVideos(tblVideo tblvideo)
        {
            VitkundEntities db = new VitkundEntities();
            db.tblVideos.Add(tblvideo);
            db.SaveChanges();
            return Json(new { success = true, message = "Data saved successfully" });
        }
        [HttpPost]
        public ActionResult DeleteVideosFile(string Id)
        {
            VitkundEntities db = new VitkundEntities();
            tblVideo tblvideo = db.tblVideos.Find(Convert.ToInt32(Id));
            if (tblvideo == null)
                return HttpNotFound();
            else
            {
                db.tblVideos.Remove(tblvideo);
                db.SaveChanges();
            }
            return Json(new { success = true, message = "Data Deleted successfully" });
        }
        [HttpPost]
        public ActionResult UploadVideosFile(HttpPostedFileBase file, HttpPostedFileBase file1)
        {
            if ((file != null && file.ContentLength > 0) || (file1 != null && file1.ContentLength > 0))
            {
                var imagepathforserver = "";
                var videopathforserver = "";
                if (file != null && file.ContentLength > 0)
                {
                    var firstfilename = string.Format(@"{0}", Guid.NewGuid()); // For Create random path for prevent duplicate name.
                    var fileName = Path.GetFileName(file.FileName);
                    var filePath = Path.Combine(Server.MapPath("~/Content/Uploads"), firstfilename + fileName);
                    imagepathforserver = firstfilename + fileName;
                    file.SaveAs(filePath);
                }
                if (file1 != null && file1.ContentLength > 0)
                {
                    var firstfilename = string.Format(@"{0}", Guid.NewGuid());
                    var fileName = Path.GetFileName(file1.FileName);
                    var filePath = Path.Combine(Server.MapPath("~/Content/Uploads"), firstfilename + fileName);
                    videopathforserver = firstfilename + fileName;
                    file.SaveAs(filePath);
                }
                return Content(imagepathforserver + "," + videopathforserver); // return imagepath and video path with comma seprated.
            }

            return Content("No file selected.");
        }
        #endregion

        #region Chapters
        [Route("Add-Chapters")]
        public ActionResult AddChapters()
        {
            VitkundEntities db = new VitkundEntities();
            var res = db.tblChapters.ToList();
            return View(res);
        }
        [HttpPost]
        public ActionResult AddChapters(tblChapter tblchapter)
        {
            VitkundEntities db = new VitkundEntities();
            if (tblchapter.Id == null || tblchapter.Id==0)
            {
                db.tblChapters.Add(tblchapter);
                db.SaveChanges();
            }
            else
            {
                var data = db.tblChapters.FirstOrDefault(x => x.Id == tblchapter.Id);
                if (data != null)
                {
                    data.Name = tblchapter.Name;
                    db.SaveChanges();
                }
            }
            return Json(new { success = true, message = "Data saved successfully" });
        }
        [HttpPost]
        public ActionResult DeleteChaptersFile(string Id)
        {
            VitkundEntities db = new VitkundEntities();
            tblChapter tblchapter = db.tblChapters.Find(Convert.ToInt32(Id));
            if (tblchapter == null)
                return HttpNotFound();
            else
            {
                db.tblChapters.Remove(tblchapter);
                db.SaveChanges();
            }
            return Json(new { success = true, message = "Data Deleted successfully" });
        }
        [HttpPost]
        public ActionResult EditChaptersFile(string Id)
        {
            VitkundEntities db = new VitkundEntities();
            tblChapter tblchapter = db.tblChapters.Find(Convert.ToInt32(Id));
            if (tblchapter == null)
                return HttpNotFound();
            else
            {
                db.tblChapters.Remove(tblchapter);
                db.SaveChanges();
            }
            return Json(new { success = true, message = "Data Deleted successfully" });
        }
        #endregion
    }
}