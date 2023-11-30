using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vitkund.Models;

namespace Vitkund.Controllers
{
    public class HomeController : Controller
    {
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
           var businessIdeas=  db.tblBusinessideas.ToList();
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

    }
}