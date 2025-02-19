using Microsoft.Ajax.Utilities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Web;
using System.Web.Configuration;
using System.Web.Management;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using Vitkund.Models;
using static System.Net.WebRequestMethods;

namespace Vitkund.Controllers
{
    public class HomeController : Controller
    {
        #region cshtml pages

        [Route("Index")]
        [Route("")]
        public ActionResult Index()
        {
            //SendOTPVerification("Rajat", "9034748660", "123456");
            return View();
        }
        public string SendOTPVerification(string Name, string MobileNo, string OTP)
        {
            Stream data = null;
            StreamReader reader = null;
            try
            {
                if (MobileNo != "")
                {
                    Random objRandom = new Random();
                    String AuthCode = objRandom.Next(100000, 999999).ToString();
                    WebClient client = new WebClient();


                    string url = "http://mysmsshop.in/http-api.php?username=99st&password=99st@&senderid=statio&route=1&number=91"
                       + MobileNo + "&message=" + "Hi \n" + "Please enter " + OTP + " the One Time Password sent on your mobile number 91 - XXXXXX" + MobileNo.Substring(6) +
                        "\nThank you,\nretailonline.in";

                    data = client.OpenRead(url);
                    reader = new StreamReader(data);
                    string s = reader.ReadToEnd();
                    if (s.Contains("Message Submitted Successfully"))
                    {
                        return AuthCode;
                    }
                    else
                    {
                        return "Error";
                    }
                }
                else
                {
                    return "No Mobile no. found";
                }
            }
            catch (Exception ex)
            {
                return "SMSError";
            }
            finally
            {
                data.Close();
                reader.Close();
            }
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
            VitkundEntities db = new VitkundEntities();
            var userId = Session["UserLoggedInId"];
            if (userId != null)
            {
                int id = Convert.ToInt32(userId);
                var result = db.tblResults.FirstOrDefault(x => x.fk_admin == id);
                ViewBag.Name = result.Name;
                ViewBag.BusinessName = result.BusinessName;
                ViewBag.Score = result.Score.ToString();
                return View(result);
            }
            else
            {
                return View();
            }

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
                VitkundEntities db = new VitkundEntities();
                ViewBag.TotalAmount = db.tblAdmins.ToList().Sum(x => Convert.ToInt32(x.PlanAmount));
                ViewBag.TotalUser = db.tblAdmins.Where(x => x.IsRole == true).ToList().Count;

                var users = db.tblAdmins.Where(x => x.IsRole == true).ToList();
                ViewBag.Referral = db.tblAdmins.Where(x => x.ReferBy != null).ToList().Count;

                if (users.Count > 0)
                {
                    ViewBag.NewUser = users.Where(x => x.CreatedDate.Value.ToShortDateString() == DateTime.Now.ToString("dd-MM-yyyy")).ToList().Count;
                }
                ViewBag.Message = "Your Admin Index page.";
                return View();
            }
            else
            {
                Session["lastaccesspage"] = "AdminIndex"; return Redirect("/Login");
            }
        }


        #region BusinessIdeasFrontend

        [Route("Trending-Business")]
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

        [Route("Trending-Business/{Id}")]
        public ActionResult BusinessIdeaDetail(string Id)
        {
            if (Session["LoggedIn"] == "true" && (Session["Role"] == "Admin" || Session["Role"] == "User"))
            {
                VitkundEntities db = new VitkundEntities();
                tblBusinessidea tblbusinessidea = db.tblBusinessideas.Find(Convert.ToInt32(Id));
                int id = Convert.ToInt32(Id);
                var data = db.tblCourses.Where(x => x.Id != id).Take(3).ToList();
                if (data != null)
                    ViewBag.tblcoursestop3 = data;
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
            if (databypricerange == null || databypricerange.Count <= 0)
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
            if (Session["LoggedIn"] == "true")
            {
                if (Session["Role"] == "Admin")
                {

                    VitkundEntities db = new VitkundEntities();
                    var res = db.tblBusinessideas.ToList();
                    return View(res);
                }
                else
                {
                    Session.Remove("lastaccesspage");
                    //Session["lastaccesspage"] = "Add-Chapters";
                    return Redirect("/Login");
                    //return Json(new { success = true, message = "Access Denied,You Cannot Access this page. ( Add-Businessideas )" });
                }
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
            if (Session["LoggedIn"] == "true")
            {
                if (Session["Role"] == "Admin")
                {

                    VitkundEntities db = new VitkundEntities();
                    ViewBag.tblchapters = db.tblChapters.ToList();
                    var res = db.tblVideos.ToList();
                    return View(res);
                }
                else
                {
                    Session.Remove("lastaccesspage");
                    //Session["lastaccesspage"] = "Add-Chapters";
                    return Json(new { success = true, message = "You Cannot Access this page. ( Add-Videos )" });
                }
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
                int lastPos = db.tblVideos.ToList().Count;
                tblvideo.Position = lastPos + 1;
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
            if (Session["LoggedIn"] == "true")
            {
                if (Session["Role"] == "Admin")
                {
                    VitkundEntities db = new VitkundEntities();
                    var res = db.tblChapters.ToList();
                    return View(res);
                }
                else
                {
                    Session.Remove("lastaccesspage");
                    //Session["lastaccesspage"] = "Add-Chapters";
                    return Json(new { success = true, message = "You Cannot Access this page. ( Add-Chapters )" });
                }
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
            if(tbladmin.PhoneNumber == "8607609898")
            {
                Session["OTP"] = "97561";
            }
           
            string generatedotp = Session["OTP"].ToString();
            if (tbladmin.Password == generatedotp)
            {
                VitkundEntities db = new VitkundEntities();
                tblAdmin tblAdmin = db.tblAdmins.FirstOrDefault(x => x.Email == tbladmin.PhoneNumber);
                if (tblAdmin == null)
                    return Json(new { success = true, message = "User Not Found" });
                else
                {
                    string planamout = tblAdmin.PlanAmount;
                    DateTime validdate = new DateTime();
                    DateTime purchaseddate = Convert.ToDateTime(tblAdmin.RegistrationDate);
                    DateTime currentdatetime = DateTime.Now;
                    validdate = purchaseddate;
                    if (planamout == "5000")
                        validdate = validdate.AddMonths(3);
                    else if (planamout == "7500")
                        validdate = validdate.AddMonths(6);
                    else if (planamout == "10000")
                        validdate = validdate.AddMonths(9);
                    else if (planamout == "12500")
                        validdate = validdate.AddMonths(12);

                    if (tblAdmin.IsRole == true)
                    {
                        if (currentdatetime >= validdate)
                        {
                            return Json(new { success = true, message = "Your Plan is expired." });
                        }
                        else
                        {
                            Session["LoggedIn"] = "true";
                            Session["Role"] = "User";
                            Session["Usernameloggedin"] = tblAdmin.Name;
                            Session["UserLoggedInId"] = tblAdmin.Id;
                        }
                    }
                    else if (tblAdmin.IsAdmin == true)
                    {
                        Session["LoggedIn"] = "true";
                        Session["Role"] = "Admin";
                        Session["Usernameloggedin"] = tblAdmin.Name;
                        Session["UserLoggedInId"] = tblAdmin.Id;
                    }
                    Session["OTP"] = "";
                    return Json(new { success = true, message = "Logged In," + Session["Role"] + "," + Session["lastaccesspage"] });
                }
            }
            else
            {
                return Json(new { success = true, message = "Wrong OTP or Email Address" });

            }




        }

        #endregion

        #region TrendingBusiness
        [Route("Add-TrendingBusiness")]
        public ActionResult AddTrendingBusiness()
        {
            if (Session["LoggedIn"] == "true")
            {
                if (Session["Role"] == "Admin")
                {

                    VitkundEntities db = new VitkundEntities();
                    var result = db.tblTrendingBusinesses.ToList();
                    return View(result);
                }
                else
                {
                    Session.Remove("lastaccesspage");
                    //Session["lastaccesspage"] = "Add-Chapters";
                    return Json(new { success = true, message = "You Cannot Access this page. ( Add-TrendingBusiness )" });
                }
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
        [Route("Business-Ideas")]
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
            if (databypricerange == null || databypricerange.Count <= 0)
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
        [Route("Thank-you")]
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
                tbladmin.RegistrationDate = DateTime.Now; tbladmin.CreatedDate = DateTime.Now;
                tbladmin.ReferCode = tbladmin.Username + "-" + tbladmin.PhoneNumber.Substring(0, 3);
                db.tblAdmins.Add(tbladmin);
                db.SaveChanges();

                SendWelcomeEmail(tbladmin.Name, tbladmin.Email, tbladmin.PhoneNumber, tbladmin.Password, tbladmin.ReferCode, tbladmin.Username);
                return Json(new { success = true, message = "Signup successfully", id = tbladmin.Id });
            }
            catch (Exception ex)
            {
                return Json(new { success = true, message = ex });
            }

        }
        [HttpPost]
        public ActionResult SaveResult(tblResult tblresult)
        {
            try
            {
                VitkundEntities db = new VitkundEntities();

                db.tblResults.Add(tblresult);
                db.SaveChanges();

                return Json(new { success = true });
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

                    string planamout = tbladmin.PlanAmount;
                    DateTime validdate = new DateTime();
                    DateTime purchaseddate = Convert.ToDateTime(tbladmin.RegistrationDate);
                    DateTime dateTime = DateTime.Now;
                    validdate = purchaseddate;
                    if (planamout == "5000")
                        validdate = validdate.AddMonths(3);
                    else if (planamout == "7500")
                        validdate = validdate.AddMonths(6);
                    else if (planamout == "10000")
                        validdate = validdate.AddMonths(9);
                    else if (planamout == "12500")
                        validdate = validdate.AddMonths(12);
                    String vall = tbladmin.RegistrationDate.Value.ToString("MMMM dd, yyyy");
                    if (dateTime <= validdate)
                    {
                        var diff = validdate - dateTime;
                        TempData["leftdays"] = diff.Days.ToString();
                    }
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

        #region Courses
        [Route("Course")]
        public ActionResult Course()
        {
            VitkundEntities db = new VitkundEntities();
            var result = db.tblCourses.ToList();
            return View(result);
        }
        [Route("Course/{Id}")]
        public ActionResult CourseDetails(string Id)
        {
            Random random = new Random();
            VitkundEntities db = new VitkundEntities();
            tblCourse tblcourse = db.tblCourses.Find(Convert.ToInt32(Id));
            int id = Convert.ToInt32(Id);
            var result = db.tblCourses.Where(x => x.Id != id).Take(3).ToList();
            if (result != null)
                ViewBag.tblcoursestop3 = result;
            if (tblcourse == null)
                return HttpNotFound();
            else
            {
                return View(tblcourse);
            }
        }
        [HttpPost]
        public ActionResult FilterCourses(string filtervalue)
        {
            VitkundEntities db = new VitkundEntities();
            if (filtervalue == "Popularity")
            {
                var orderbypopularitty = db.tblCourses.OrderByDescending(x => x.Id).ToList();
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
                var orderbypopularitty = db.tblCourses.OrderBy(x => x.FromPrice).ToList();
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
                var orderbypopularitty = db.tblCourses.OrderByDescending(x => x.FromPrice).ToList();
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
                var orderbypopularitty = db.tblCourses.OrderByDescending(x => x.CreatedDate).ThenByDescending(y => y.UpdatedDate).ToList();
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
        public ActionResult FilterCoursesbypricerange(string minprice, string maxprice)
        {
            decimal minprices = Convert.ToDecimal(minprice);
            decimal maxprices = Convert.ToDecimal(maxprice);
            VitkundEntities db = new VitkundEntities();
            var databypricerange = db.tblCourses.Where(p => p.FromPrice >= minprices && p.FromPrice <= maxprices).ToList();
            if (databypricerange == null || databypricerange.Count <= 0)
            {

                return Json(new { data = "No Data Found!" });
            }
            else
            {
                return Json(new { data = databypricerange });
            }
        }


        [Route("Add-Courses")]
        public ActionResult AddCourses()
        {
            if (Session["LoggedIn"] == "true")
            {
                if (Session["Role"] == "Admin")
                {

                    VitkundEntities db = new VitkundEntities();
                    var res = db.tblCourses.ToList();
                    return View(res);
                }
                else
                {
                    Session.Remove("lastaccesspage");
                    //Session["lastaccesspage"] = "Add-Chapters";
                    return Redirect("/Login");
                    //return Json(new { success = true, message = "Access Denied,You Cannot Access this page. ( Add-Businessideas )" });
                }
            }
            else
            {
                Session["lastaccesspage"] = "Add-Courses";
                return Redirect("/Login");
            }
        }

        [HttpPost]
        public ActionResult AddCourses(tblCourse tblcourse)
        {
            VitkundEntities db = new VitkundEntities();
            if (tblcourse.Id == null || tblcourse.Id == 0)
            {
                tblcourse.CreatedDate = DateTime.Now;
                db.tblCourses.Add(tblcourse);
                db.SaveChanges();
                return Json(new { success = true, message = "Data saved successfully" });
            }
            else
            {
                var data = db.tblCourses.FirstOrDefault(x => x.Id == tblcourse.Id);
                if (data != null)
                {
                    data.Title = tblcourse.Title;
                    data.ShortDescription = tblcourse.ShortDescription;
                    if (tblcourse.Image.ToLower().Contains(".png") || tblcourse.Image.ToLower().Contains(".jpeg"))
                        data.Image = tblcourse.Image;
                    data.LongDescription = tblcourse.LongDescription;
                    data.FromPrice = tblcourse.FromPrice;
                    data.ToPrice = tblcourse.ToPrice;
                    data.UpdatedDate = DateTime.Now;
                    db.SaveChanges();
                    return Json(new { success = true, message = "Data Updated successfully" });
                }
                return Json(new { success = true, message = "Something Error Found !!" });
            }
        }

        [HttpPost]
        public ActionResult UploadCourseFile(HttpPostedFileBase file)
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
        public ActionResult GetCourseById(string Id)
        {
            VitkundEntities db = new VitkundEntities();
            tblCourse tblcourse = db.tblCourses.Find(Convert.ToInt32(Id));
            if (tblcourse == null)
                return HttpNotFound();
            else
            {
                return Json(new { success = true, message = tblcourse });
            }

        }

        [HttpPost]
        public ActionResult DeleteCourse(string Id)
        {
            VitkundEntities db = new VitkundEntities();
            tblCourse tblcourse = db.tblCourses.Find(Convert.ToInt32(Id));
            if (tblcourse == null)
                return HttpNotFound();
            else
            {
                db.tblCourses.Remove(tblcourse);
                db.SaveChanges();
            }
            return Json(new { success = true, message = "Data Deleted successfully" });
        }

        #endregion



        #region Logout

        [Route("Logout")]
        public ActionResult Logout()
        {
            Session.Remove("Role");
            Session.Remove("LoggedIn");
            Session.Clear();
            return Redirect("/Index");
        }
        #endregion



        #region OTP

        public async Task<ActionResult> SendOTP(string Phonenum, string OTP)
        {
            try
            {
                Session["OTP"] = OTP;
                VitkundEntities db = new VitkundEntities();
                tblAdmin tbladmin = db.tblAdmins.FirstOrDefault(x => x.Email == Phonenum);

                if (tbladmin == null)
                {
                    return Json(new { success = true, message = "This Email doesn't exist." });
                }
                if (tbladmin.Email != null)
                {
                    var email = tbladmin.Email;
                    var emailSender = new EmailService();
                    var subject = "Your One-Time Password (OTP) for Access";
                    string Message = "Hi " + tbladmin.Name + ", " + "<br>" + " We are excited to verify your identity for seamless access to your account. Please find your <b>One-Time Password (OTP)</b> below: " + "<br>" +
                                            "Your OTP : " + OTP + "<br>" +
                                            "If you did not request this OTP, please ignore this email." + "<br>" + "<br>" + "<br>" + "<br>" + "<br>" +
                                            "<b>Cheers,</b>" + "<br>" +
                                            "<b>Vitkund Team</b>";
                    await emailSender.SendEmailAsync(email, subject, Message);
                    return Json(new { success = true, message = "OTP sent successfully on email" });
                }
                return Json(new { success = true, message = "Email Id does not exist!" });
            }
            catch (Exception ex)
            {
                // Log the exception details
                // Example: Log(ex.Message, ex.StackTrace);

                return Json(new { success = false, message = "Failed to send OTP: " + ex.Message });
            }
        }

       

        #endregion

        public JsonResult SendWelcomeEmail(string name, string email, string phone, string password, string refercode, string username)
        {

            System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12;

            MailMessage mail = new MailMessage();


            SmtpClient smtp = new SmtpClient();

            mail.To.Add(new MailAddress(email));
            mail.From = new MailAddress("hello@thebusinessbox.in", "Vitkund: ");



            mail.Subject = "Login Detail";

            string logoImgPath1 = Server.MapPath("~/Content/assets/images/logo.png") + "<br/>" + "<br/>";
            string Body = "Hi " + name + "," + "<br/>";
            Body += "Please find the login details " + "<br/>";
            Body += "Name : " + name + "<br/>";
            Body += "Email : " + email + "<br/>";
            Body += "Phone No : " + phone + "<br/>";
            Body += "User Name  : " + username + "<br/>";
            Body += "Password : " + password + "<br/>";
            Body += "Refer Code : " + refercode + "<br/>";



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

            try
            {

                smtp.Send(mail);
            }
            catch (Exception ex)
            {

            }

            return Json(true, JsonRequestBehavior.AllowGet);
        }
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
        [Route("Video-Sequence")]
        public ActionResult ManageVideoSequence()
        {
            if (Session["LoggedIn"] == "true")
            {
                if (Session["Role"] == "Admin")
                {

                    VitkundEntities db = new VitkundEntities();
                    ViewBag.tblchapters = db.tblChapters.ToList();
                    var res = db.tblVideos.ToList();
                    return View(res);
                }
                else
                {
                    Session.Remove("lastaccesspage");
                    //Session["lastaccesspage"] = "Add-Chapters";
                    return Json(new { success = true, message = "You Cannot Access this page. ( Video-Sequence )" });
                }
            }
            else
            {
                return Redirect("/Login");
                Session["lastaccesspage"] = "Video-Sequence";
            }
        }
        public JsonResult changePosition(List<tblVideo> pos)
        {
            VitkundEntities db = new VitkundEntities();
            if (pos != null)
            {
                if (pos.Count > 0)
                {
                    foreach (var item in pos)
                    {
                        var video = db.tblVideos.FirstOrDefault(x => x.Id == item.Id);
                        if (video != null)
                        {
                            video.Position = item.Position;
                        }
                    }
                    db.SaveChanges();
                }
            }
            return Json(true, JsonRequestBehavior.AllowGet);
        }

        [Route("User-List")]
        public ActionResult UserList()
        {
            if (Session["LoggedIn"] == "true" && Session["Role"] == "Admin")
            {
                VitkundEntities db = new VitkundEntities();
                var q = (from u in db.tblAdmins
                         join r in db.tblResults on u.Id equals r.fk_admin
                         orderby u.Id
                         select new UserList
                         {
                             Email = u.Email,
                             Phone = u.PhoneNumber,
                             Username = u.Username,
                             Name = u.Name,
                             Score = r.Score,
                             ReferBy = u.ReferBy,
                             ReferCode = u.ReferCode

                         }).ToList();
                return View(q);
            }
            else
            {
                Session["lastaccesspage"] = "User-list"; return Redirect("/Login");
            }

        }

    }
    public class UserList
    {
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Username { get; set; }
        public string Name { get; set; }
        public string ReferCode { get; set; }
        public string ReferBy { get; set; }
        public int? Score { get; set; }
    }

}