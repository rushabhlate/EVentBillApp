
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using EventBillApplication.DAL;
using EventBillApplication.Models;
namespace EventBillApplication.Controllers
{
    public class LoginController : Controller
    {
        //
        // GET: /Login/
        cl_UserAuth obj = new cl_UserAuth();

        public ActionResult Index()
        {
            if (Session["LoggedUser"] != null)
            {
                return RedirectToAction("Index", "Dashboard");
            }
            else
            {
                Session.Clear();
                Session.Abandon();
                Session.RemoveAll();
                Response.Cache.SetCacheability(HttpCacheability.NoCache);
                Response.Cache.SetExpires(DateTime.UtcNow.AddMilliseconds(-1));
                Response.Cache.SetNoStore();
                FormsAuthentication.SignOut();
                LoginEntity login = new LoginEntity();
                if (Request.Cookies["Login"] != null)
                {
                    login.UserName = Request.Cookies["Login"].Values["UserName"];
                    login.Password = Request.Cookies["Login"].Values["Password"];
                    return View(login);
                }
                return View();
            }
        }



        [HttpPost]
        public ActionResult Index(LoginEntity user)
        {
            try
            {
                if (ModelState.IsValid)
                {

                    if (user.Password == "" || user.Password == null)
                    {

                        TempData["Message"] = "Please Check Password ";
                        TempData["MessageClass"] = "danger";

                        var c = new HttpCookie("Login");
                        c.Expires = DateTime.Now.AddDays(-1);
                        Response.Cookies.Add(c);


                        return RedirectToAction("Index");
                    }
                    if (user.UserName == "" || user.UserName == null)
                    {

                        TempData["Message"] = "Please Check UserName ";
                        TempData["MessageClass"] = "danger";
                        var c = new HttpCookie("Login");
                        c.Expires = DateTime.Now.AddDays(-1);
                        Response.Cookies.Add(c);

                        return RedirectToAction("Index");
                    }
                    user.Password = Helper.Helper.Encrypt(user.Password);

                    //  var records = Service.Authenticate(user);
                    var records = obj.Authenticateuser(user);
                    if (records.Id != 0)
                    {
                       
                        Session.Add("LoggedUser", records);



                        
                        return RedirectToAction("Index", "Dashboard");

                    }
                    else
                    {
                        TempData["Message"] = "Login Failed..Please Check UserName and Password ";
                        TempData["MessageClass"] = "danger";
                        return RedirectToAction("Index");
                    }

                }
                else
                {
                    TempData["Message"] = "Login Failed..Please Check UserName and Password ";
                    TempData["MessageClass"] = "danger";
                    return RedirectToAction("Index");
                }
                return View();
            }
            catch (Exception ex)
            {
                //ExceptionLogging.SendErrorToText(ex);
                return RedirectToAction("Index", "Login");
            }
        }

        public ActionResult Signout()
        {
            FormsAuthentication.SignOut();
            Session.Abandon();
            Session.RemoveAll();
            Session.Clear();
            Session.Remove("LoggedUser");

            return RedirectToAction("Index");
        }
    }
}
