using Microsoft.Ajax.Utilities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.IO;
using System.Linq;
using System.Net.Mail;
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
            Session.Remove("Role");
            Session.Remove("LoggedIn");
            Session.Remove("Usernameloggedin");
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

        [Route("Privacy-Policy")]
        public ActionResult privacypolicy()
        {
            ViewBag.Message = "Privacy Policy";

            return View();
        }

        [Route("Terms-and-Condition")]
        public ActionResult termsandcondition()
        {
            ViewBag.Message = "Terms and Condition";

            return View();
        }

        [Route("Refund-Policy")]
        public ActionResult refundpolicy()
        {
            ViewBag.Message = "Refund Policy";

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

        [Route("Videos")]
        public ActionResult Videos()
        {
            if (Session["LoggedIn"] == "true" && (Session["Role"] == "Admin" || Session["Role"] == "User"))
            {

                VitkundEntities db = new VitkundEntities();
                ViewBag.tblchapters = db.tblChapters.ToList();
                ViewBag.Message = "Your contact page.";
                var res = db.tblVideos.ToList();
                return View(res);
            }
            else
            {
                Session["lastaccesspage"] = "Videos";
                return Redirect("/Login");
            }
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
            if (Session["LoggedIn"] == "true" && Session["Role"] == "Admin")
            {

                ViewBag.Message = "Your Admin Index page.";
                return View();
            }
            else
            {
                Session["lastaccesspage"] = "AdminIndex"; return Redirect("/Login");
            }
        }


        #region BusinessIdeasFrontend

        [Route("Business-ideas")]
        public ActionResult BusinessIdeas()
        {
            if (Session["LoggedIn"] == "true" && (Session["Role"] == "Admin" || Session["Role"] == "User"))
            {
                
                ViewBag.Message = "Your contact page.";
                VitkundEntities db = new VitkundEntities();
                var businessIdeas = db.tblBusinessideas.ToList();
                return View(businessIdeas);
            }
            else
            {
                Session["lastaccesspage"] = "Business-Ideas";
                return Redirect("/Login");
            }
        }

        [Route("Business-Ideas/{Id}")]
        public ActionResult BusinessIdeaDetail(string Id)
        {
            if (Session["LoggedIn"] == "true" && (Session["Role"] == "Admin" || Session["Role"] == "User"))
            {
                VitkundEntities db = new VitkundEntities();
                tblBusinessidea tblbusinessidea = db.tblBusinessideas.Find(Convert.ToInt32(Id));
                if (tblbusinessidea == null)
                    return HttpNotFound();
                else
                {
                    return View(tblbusinessidea);
                }
            }
            else
                return Redirect("/Login");
        }
        [HttpPost]
        public ActionResult FilterBusinessIdeas(string filtervalue)
        {
            VitkundEntities db = new VitkundEntities();
            if (filtervalue == "Popularity")
            {
                var orderbypopularitty = db.tblBusinessideas.OrderByDescending(x => x.Id).ToList();
                if (string.IsNullOrEmpty(orderbypopularitty.ToString()))
                {

                    return Json(new { data = "No Data Found!" });
                }
                else
                {
                    return Json(new { data = orderbypopularitty });
                }
            }
            else if (filtervalue == "Price: low to high")
            {
                var orderbypopularitty = db.tblBusinessideas.OrderBy(x => x.fromPrice).ToList();
                if (string.IsNullOrEmpty(orderbypopularitty.ToString()))
                {

                    return Json(new { data = "No Data Found!" });
                }
                else
                {
                    return Json(new { data = orderbypopularitty });
                }
            }
            else if (filtervalue == "Price: high to low")
            {
                var orderbypopularitty = db.tblBusinessideas.OrderByDescending(x => x.fromPrice).ToList();
                if (string.IsNullOrEmpty(orderbypopularitty.ToString()))
                {

                    return Json(new { data = "No Data Found!" });
                }
                else
                {
                    return Json(new { data = orderbypopularitty });
                }
            }
            else if (filtervalue == "Latest")
            {
                var orderbypopularitty = db.tblBusinessideas.OrderByDescending(x => x.createDate).ThenByDescending(y => y.UpdateDate).ToList();
                if (string.IsNullOrEmpty(orderbypopularitty.ToString()))
                {

                    return Json(new { data = "No Data Found!" });
                }
                else
                {
                    return Json(new { data = orderbypopularitty });
                }
            }
            else
            {
                return Json(new { data = "No Data Found!" });
            }
        }
        [HttpPost]
        public ActionResult FilterBusinessIdeasbypricerange(string minprice, string maxprice)
        {
            decimal minprices = Convert.ToDecimal(minprice);
            decimal maxprices = Convert.ToDecimal(maxprice);
            VitkundEntities db = new VitkundEntities();
            var databypricerange = db.tblBusinessideas.Where(p => p.fromPrice >= minprices && p.fromPrice <= maxprices).ToList();
            if (databypricerange == null)
            {

                return Json(new { data = "No Data Found!" });
            }
            else
            {
                return Json(new { data = databypricerange });
            }
        }

        #endregion

        #endregion



        #region AddBusinessIdeas
        [Route("Add-Businessideas")]
        public ActionResult AddBusinessIdeas()
        {
            if (Session["LoggedIn"] == "true" && Session["Role"] == "Admin")
            {

                VitkundEntities db = new VitkundEntities();
                var res = db.tblBusinessideas.ToList();
                return View(res);
            }
            else
            {
                Session["lastaccesspage"] = "Add-BusinessIdeas";
                return Redirect("/Login");
            }
        }

        [HttpPost]
        public ActionResult AddBusinessIdeas(tblBusinessidea tblBusinessidea)
        {
            VitkundEntities db = new VitkundEntities();
            if (tblBusinessidea.Id == null || tblBusinessidea.Id == 0)
            {
                tblBusinessidea.createDate = DateTime.Now;
                db.tblBusinessideas.Add(tblBusinessidea);
                db.SaveChanges();
                return Json(new { success = true, message = "Data saved successfully" });
            }
            else
            {
                var data = db.tblBusinessideas.FirstOrDefault(x => x.Id == tblBusinessidea.Id);
                if (data != null)
                {
                    data.Title = tblBusinessidea.Title;
                    data.ShortDescription = tblBusinessidea.ShortDescription;
                    if (tblBusinessidea.Image.ToLower().Contains(".png") || tblBusinessidea.Image.ToLower().Contains(".jpeg"))
                        data.Image = tblBusinessidea.Image;
                    data.LongDescription = tblBusinessidea.LongDescription;
                    data.fromPrice = tblBusinessidea.fromPrice;
                    data.toPrice = tblBusinessidea.toPrice;
                    data.UpdateDate = DateTime.Now;
                    db.SaveChanges();
                    return Json(new { success = true, message = "Data Updated successfully" });
                }
                return Json(new { success = true, message = "Something Error Found !!" });
            }
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
        public ActionResult GetBusinessIdeaById(string Id)
        {
            VitkundEntities db = new VitkundEntities();
            tblBusinessidea tblbusinessidea = db.tblBusinessideas.Find(Convert.ToInt32(Id));
            if (tblbusinessidea == null)
                return HttpNotFound();
            else
            {
                return Json(new { success = true, message = tblbusinessidea });
            }

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
            if (Session["LoggedIn"] == "true" && Session["Role"] == "Admin")
            {

                VitkundEntities db = new VitkundEntities();
                ViewBag.tblchapters = db.tblChapters.ToList();
                var res = db.tblVideos.ToList();
                return View(res);
            }
            else
            {
                return Redirect("/Login");
                Session["lastaccesspage"] = "Add-Videos";
            }
        }
        [HttpPost]
        public ActionResult AddVideos(tblVideo tblvideo)
        {
            VitkundEntities db = new VitkundEntities();
            if (tblvideo.Id == null || tblvideo.Id == 0)
            {
                db.tblVideos.Add(tblvideo);
                db.SaveChanges();
                return Json(new { success = true, message = "Data saved successfully" });
            }
            else
            {
                var data = db.tblVideos.FirstOrDefault(x => x.Id == tblvideo.Id);
                if (data != null)
                {
                    data.FileTitle = tblvideo.FileTitle;
                    if (tblvideo.VideoFile.ToLower().Contains(".mp4"))
                        data.VideoFile = tblvideo.VideoFile;
                    if (tblvideo.VideoImage.ToLower().Contains(".png") || tblvideo.VideoImage.ToLower().Contains(".jpeg"))
                        data.VideoImage = tblvideo.VideoImage;
                    data.fK_Chapter = tblvideo.fK_Chapter;
                    db.SaveChanges();
                    return Json(new { success = true, message = "Data Updated successfully" });
                }

                return Json(new { success = true, message = "Something Error Found !!" });
            }

        }
        [HttpPost]
        public ActionResult GetVideosFile(string Id)
        {
            VitkundEntities db = new VitkundEntities();
            tblVideo tblvideo = db.tblVideos.Find(Convert.ToInt32(Id));
            if (tblvideo == null)
                return HttpNotFound();
            else
            {
                return Json(new { success = true, message = tblvideo });
            }

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
                    file1.SaveAs(filePath);
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
            if (Session["LoggedIn"] == "true" && Session["Role"] == "Admin")
            {

                VitkundEntities db = new VitkundEntities();
                var res = db.tblChapters.ToList();
                return View(res);
            }
            else
            {
                Session["lastaccesspage"] = "Add-Chapters"; return Redirect("/Login");
            }
        }
        [HttpPost]
        public ActionResult AddChapters(tblChapter tblchapter)
        {
            VitkundEntities db = new VitkundEntities();
            if (tblchapter.Id == null || tblchapter.Id == 0)
            {
                db.tblChapters.Add(tblchapter);
                db.SaveChanges();
                return Json(new { success = true, message = "Data saved successfully" });
            }
            else
            {
                var data = db.tblChapters.FirstOrDefault(x => x.Id == tblchapter.Id);
                if (data != null)
                {
                    data.Name = tblchapter.Name;
                    db.SaveChanges();
                    return Json(new { success = true, message = "Data Updated successfully" });
                }

                return Json(new { success = true, message = "Something Error Found !!" });
            }
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


        #region Admin
        [HttpPost]
        public ActionResult LoginAdmin(tblAdmin tbladmin)
        {
            VitkundEntities db = new VitkundEntities();
            tblAdmin tblAdmin = db.tblAdmins.FirstOrDefault(x => x.Username == tbladmin.Username && x.Password == tbladmin.Password);
            if (tblAdmin == null)
                return Json(new { success = true, message = "User Not Found" });
            else
            {
                if (tblAdmin.IsRole == true)
                {
                    Session["LoggedIn"] = "true";
                    Session["Role"] = "User";
                    Session["Usernameloggedin"] = tblAdmin.Name;
                    Session["UserLoggedInId"] = tblAdmin.Id;
                }
                else if (tblAdmin.IsAdmin == true)
                {
                    Session["LoggedIn"] = "true";
                    Session["Role"] = "Admin";
                    Session["Usernameloggedin"] = tbladmin.Name;
                    Session["UserLoggedInId"] = tblAdmin.Id;
                }
                return Json(new { success = true, message = "Logged In," + Session["Role"] +","+ Session["lastaccesspage"] });
            }


        }

        #endregion

        #region TrendingBusiness
        [Route("Add-TrendingBusiness")]
        public ActionResult AddTrendingBusiness()
        {
            if (Session["LoggedIn"] == "true" && Session["Role"] == "Admin")
            {

                VitkundEntities db = new VitkundEntities();
                var result = db.tblTrendingBusinesses.ToList();
                return View(result);
            }
            else
            {
                Session["lastaccesspage"] = "Add-TrendingBusiness"; return Redirect("/Login");
            }
        }

        [HttpPost]
        public ActionResult AddTrendingBusiness(tblTrendingBusiness tblTrendingBusiness)
        {
            VitkundEntities db = new VitkundEntities();
            if (tblTrendingBusiness.Id == null || tblTrendingBusiness.Id == 0)
            {
                db.tblTrendingBusinesses.Add(tblTrendingBusiness);
                db.SaveChanges();
                return Json(new { success = true, message = "Data saved successfully" });
            }
            else
            {
                var data = db.tblTrendingBusinesses.FirstOrDefault(x => x.Id == tblTrendingBusiness.Id);
                if (data != null)
                {
                    data.BusinessName = tblTrendingBusiness.BusinessName;
                    data.FromPrice = tblTrendingBusiness.FromPrice;
                    data.ToPrice = tblTrendingBusiness.ToPrice;
                    if (!string.IsNullOrEmpty(tblTrendingBusiness.VideoLink))
                        data.VideoLink = tblTrendingBusiness.VideoLink;
                    db.SaveChanges();
                    return Json(new { success = true, message = "Data Updated successfully" });
                }

                return Json(new { success = true, message = "Something Error Found !!" });
            }
        }
        [HttpPost]
        public ActionResult GetTrendingBusinessById(string Id)
        {
            VitkundEntities db = new VitkundEntities();
            tblTrendingBusiness tblTrendingBusiness = db.tblTrendingBusinesses.Find(Convert.ToInt32(Id));
            if (tblTrendingBusiness == null)
                return HttpNotFound();
            else
            {
                return Json(new { success = true, message = tblTrendingBusiness });
            }
        }
        [HttpPost]
        public ActionResult DeleteTrendingBusiness(string Id)
        {
            VitkundEntities db = new VitkundEntities();
            tblTrendingBusiness tblTrendingBusiness = db.tblTrendingBusinesses.Find(Convert.ToInt32(Id));
            if (tblTrendingBusiness == null)
                return HttpNotFound();
            else
            {
                db.tblTrendingBusinesses.Remove(tblTrendingBusiness);
                db.SaveChanges();
            }
            return Json(new { success = true, message = "Data Deleted successfully" });
        }

        //FrontEnd
        [Route("Trending-Business")]
        public ActionResult TrendingBusiness()
        {
            if (Session["LoggedIn"] == "true" && (Session["Role"] == "Admin" || Session["Role"] == "User"))
            {
                
                VitkundEntities db = new VitkundEntities();
                var trendingbusiness = db.tblTrendingBusinesses.ToList();
                return View(trendingbusiness);
            }
            else
            {
                Session["lastaccesspage"] = "Trending-Business";
                return Redirect("/Login");
            }
        }
        [HttpPost]
        public ActionResult FilterTrendingBusinessbypricerange(string minprice, string maxprice)
        {
            decimal minprices = Convert.ToDecimal(minprice);
            decimal maxprices = Convert.ToDecimal(maxprice);
            VitkundEntities db = new VitkundEntities();
            var databypricerange = db.tblTrendingBusinesses.Where(p => p.FromPrice >= minprices && p.FromPrice <= maxprices).ToList();
            if (databypricerange == null)
            {

                return Json(new { data = "No Data Found!" });
            }
            else
            {
                return Json(new { data = databypricerange });
            }
        }
        [HttpPost]
        public ActionResult FilterTrendingBusiness(string filtervalue)
        {
            VitkundEntities db = new VitkundEntities();
            if (filtervalue == "Popularity")
            {
                var orderbypopularitty = db.tblTrendingBusinesses.OrderByDescending(x => x.Id).ToList();
                if (string.IsNullOrEmpty(orderbypopularitty.ToString()))
                {

                    return Json(new { data = "No Data Found!" });
                }
                else
                {
                    return Json(new { data = orderbypopularitty });
                }
            }
            else if (filtervalue == "Price: low to high")
            {
                var orderbypopularitty = db.tblTrendingBusinesses.OrderBy(x => x.FromPrice).ToList();
                if (string.IsNullOrEmpty(orderbypopularitty.ToString()))
                {

                    return Json(new { data = "No Data Found!" });
                }
                else
                {
                    return Json(new { data = orderbypopularitty });
                }
            }
            else if (filtervalue == "Price: high to low")
            {
                var orderbypopularitty = db.tblTrendingBusinesses.OrderByDescending(x => x.FromPrice).ToList();
                if (string.IsNullOrEmpty(orderbypopularitty.ToString()))
                {

                    return Json(new { data = "No Data Found!" });
                }
                else
                {
                    return Json(new { data = orderbypopularitty });
                }
            }
            else if (filtervalue == "Latest")
            {
                var orderbypopularitty = db.tblTrendingBusinesses.OrderByDescending(x => x.CreateDate).ThenByDescending(y => y.UpdatedDate).ToList();
                if (string.IsNullOrEmpty(orderbypopularitty.ToString()))
                {

                    return Json(new { data = "No Data Found!" });
                }
                else
                {
                    return Json(new { data = orderbypopularitty });
                }
            }
            else
            {
                return Json(new { data = "No Data Found!" });
            }
        }
        //FrontEnd
        #endregion

        #region Signup
        [Route("Signup")]
        public ActionResult Signup()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddSignupDetails(tblAdmin tbladmin)
        {
            try
            {
                VitkundEntities db = new VitkundEntities();
                tbladmin.IsRole = true;
                tbladmin.RegistrationDate = DateTime.Now;
                db.tblAdmins.Add(tbladmin);
                db.SaveChanges();
                return Json(new { success = true, message = "Signup successfully" });
            }
            catch (Exception ex)
            {
                return Json(new { success = true, message = ex });
            }

        }
        #endregion

        #region myprofile
        [Route("profile")]
        public ActionResult myaccount()
        {
            var id = Session["UserLoggedInId"];
            if (id != null)
            {

                VitkundEntities db = new VitkundEntities();
                tblAdmin tbladmin = new tblAdmin();
                tbladmin = db.tblAdmins.Find(Convert.ToInt32(id));
                if (tbladmin.RegistrationDate.HasValue)
                {
                    String vall = tbladmin.RegistrationDate.Value.ToString("MMMM dd, yyyy");
                }
                return View(tbladmin);
            }
            else
            {
                Session["lastaccesspage"] = "profile"; return Redirect("/Login");
            }

        }
        [HttpPost]
        public ActionResult UpdateProfileDetails(tblAdmin tbladmin)
        {
            VitkundEntities db = new VitkundEntities();
            if (tbladmin.Id != null)
            {
                var data = db.tblAdmins.FirstOrDefault(x => x.Id == tbladmin.Id);
                if (data != null)
                {
                    data.Name = tbladmin.Name;
                    data.FatherName = tbladmin.FatherName;
                    data.City = tbladmin.City;
                    data.State = tbladmin.State;
                    data.PhoneNumber = tbladmin.PhoneNumber;
                    data.Username = tbladmin.Username;
                    db.SaveChanges();
                    return Json(new { success = true, message = "Data Updated successfully" });
                }

                return Json(new { success = true, message = "Something Error Found !!" });
            }
            else
            {
                return Json(new { success = true, message = "Something Error Found !!" });
            }
        }
        [HttpPost]
        public ActionResult UpdatePassword(int Id, string CurrentPassword, string NewPassword)
        {
            VitkundEntities db = new VitkundEntities();
            if (Id != null)
            {
                var data = db.tblAdmins.FirstOrDefault(x => x.Id == Id);
                if (data.Password == CurrentPassword)
                {
                    data.Password = NewPassword;
                    db.SaveChanges();
                    return Json(new { success = true, message = "Password Updated successfully" });
                }
                else
                {
                    return Json(new { success = true, message = "Current Password Doesn't match!!" });
                }
            }
            else
            {
                return Json(new { success = true, message = "Something went wrong!!" });
            }
        }
        #endregion

        #region Logout

        //[HttpPost]
        //public ActionResult Logout()
        //{
        //    Session.Remove("Role");
        //    Session.Remove("LoggedIn");
        //    return Redirect("/Login");
        //}
        #endregion

        public JsonResult SendContactEmail(string name, string email, string phone, string message)
        {

            System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12;

            MailMessage mail = new MailMessage();

            ArrayList list_emails = new ArrayList();
            list_emails.Add(ConfigurationManager.AppSettings["ToMail"].ToString());
            list_emails.Add(ConfigurationManager.AppSettings["ToMailNew"].ToString());

            SmtpClient smtp = new SmtpClient();
            foreach (string email_to in list_emails)
            {
                mail.To.Add(new MailAddress(email_to));
                mail.From = new MailAddress("hello@thebusinessbox.in", "Vitkund: ");



                mail.Subject = "New Contact Form Enquiry";

                string logoImgPath1 = Server.MapPath("~/Content/assets/images/logo.png") + "<br/>" + "<br/>";
                string Body = "Hi Team," + "<br/>";
                Body += "We received one new enquiry. " + "<br/>";
                Body += "Name : " + name + "<br/>";
                Body += "Email : " + email + "<br/>";
                Body += "Phone No : " + phone + "<br/>";
                Body += "Message : " + message + "<br/>";



                Body += "<b>Regards</b> " + "<br/>";
                Body += "<b>Team Vitkund</b> " + "<br/>";
                mail.Body = Body;


                mail.IsBodyHtml = true;
                smtp.Host = "smtpout.secureserver.net";
                //smtp.Port = 80;
                smtp.Port = 80;
                //smtp.Port = 587;
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new System.Net.NetworkCredential("hello@thebusinessbox.in", "Webasp@12"); // Enter seders User name and password                                                                                           //smtp.Credentials = new System.Net.NetworkCredential("oknkapil@gmail.com", "1234@Abcd");
                smtp.EnableSsl = false;
            }
            try
            {

                smtp.Send(mail);
            }
            catch (Exception ex)
            {

            }

            return Json(true, JsonRequestBehavior.AllowGet);
        }

    }
}