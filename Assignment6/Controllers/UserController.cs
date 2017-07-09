using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.SessionState;
using Assignment6.Models.BO;

using Assignment6.Models.DAL;

//using Assignment6.Models.DAL;
namespace Assignment6.Controllers
{
    public class UserController : Controller
    {
        //
        // GET: /User/

        //public ActionResult Index()
        //{
        //    return View();
        //}
        //[HttpPost]
        //public JsonResult insert(User u)
        //{

        //    Object data = null;
        //    int result = UserDAL.insert(u);
        //    bool val = false;
        //    if (result == 0)
        //        val = true;
        //    else if (result == -1)
        //        val = false;
        //    data = new
        //    {

        //        res = val,
        //        url = Url.Content("~/Account/Register")

        //    };
        //    return Json(data, JsonRequestBehavior.AllowGet);
        //}
    [HttpGet]
    public ActionResult validate(string email, string codeForReset)
    {
        Object data = null;
        var result = UserDAL.validateCode(email , codeForReset);
        if (result == 0)
        {
            Session["valid"] = "true";
            return View("EnterNewPassword");
        }
        else
        {
            return Redirect("~/Home/Error");
        }
    }
    [HttpPost]
    public JsonResult validateLogin(User u)
    {

        Object data = null;
        bool val = false;
        String r="null";
        User result = LoginHistoryDAL.insert(u);
        if (result != null)
        {
            Session.Add("user", result);
            Session.Add("userType" , result.Type);
            ViewBag.type = result.Type;
            val = true;
            r = result.Type;
            r= r.Trim();
              
        }
        data = new
        {
            res = r
        };
        return Json(data, JsonRequestBehavior.AllowGet);
    }

    public ActionResult abc()
    {
        return View("EnterNewPassword");
    }


    [HttpPost]
    public JsonResult insert(User u)
    {
        String r = "null";

        Object data = null;
        User result = UserDAL.insert(u);
        bool val = false;
        Session.Add("user", result);
       
        val = true;
        r = result.Type;
        r = r.Trim();
        data = new
        {
            res = r
        };
        return Json(data, JsonRequestBehavior.AllowGet);
       

    }
    }

}
