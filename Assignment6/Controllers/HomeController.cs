using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Assignment6.Models.DAL;
using Assignment6.Models.BO;

namespace Assignment6.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
           // ViewBag.Message = "Modify this template to jump-start your ASP.NET MVC application.";
          //int x = UserDAL.insert("abc" , "xyz");
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your app description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public ActionResult Home()
        {
            if (Session["user"] == null)
            {
                return Redirect("~/Home/Login");
            }
           
            ViewBag.Message = "Home Page";
            
            return View();
        }
        public ActionResult Login()
        {
            ViewBag.Message = "Login";
            return View();
        }
        public ActionResult ForgotPassword()
        {
            ViewBag.Message = "Forgot Password";
            return View();
        }
        [HttpGet]
        public ActionResult validate(string email, string codeForReset)
        {
            Object data = null;
            var result = UserDAL.validateCode(email, codeForReset);
            if (result == 0)
            {
                ViewData["login"] = email;
                return View("EnterNewPassword");
            }
            else
                return View("Error");
          
        }
        public ActionResult EnterNewPassword()
        {
            if (Session["valid"] != null)
            {
                return View();
            }
            else
               return View("Error");
        }
        public ActionResult Error()
        {
            return View();
        }


    }
}
