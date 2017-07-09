using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web;
using System.Web.SessionState;
using Assignment6.Models.DAL;
using Assignment6.Models.BO;

namespace Assignment6.Controllers
{
    public class ValuesController : ApiController
    {
        // GET api/values
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }
        //[HttpPost]
        //public object insert([FromBody]User u)
        //{

        //    Object data = null;
        //    int result = UserDAL.insert(u);
        //    bool val = false;
        //    if (result == 0)
        //    {
        //        val = true;
        //        //Session["login"] = u.Login;
        //    }
        //    else if (result == -1)
        //        val = false;
        //    data = new
        //    {

        //        res = val,
        //    };
        //    return data;

        //}
        //[HttpPost]
        //public object validate([FromBody]User u)
        //{

        //    Object data = null;
        //    var session = HttpContext.Current.Session;
        //    User result = LoginHistoryDAL.insert(u);
        //    //if (result == 0)
        //    //    session.Add("login" , u.Login);
        //    data = new
        //    {   res = result
        //    };
        //    return data;
        //}
        [HttpPost]
        public object generateCode([FromBody] User user)
        {
            Object data = null;
            int result = UserDAL.generateCode(user.Login);

            data = new
            {
                res = result
            };
            return data;
        }
        [HttpPost]
        public object updatePassword([FromBody] User user)
        {
            Object data = null;
            int result = UserDAL.updatePassword(user.Login , user.Password);

            data = new
            {
                res = result
            };
            return data;
        }
        [HttpGet]
        public object validateCode(string email , string codeForReset)
        {
            Object data = null;
            int result = UserDAL.validateCode(email , codeForReset);
             data = new
            {
                res = result
            };
             return data;
                
        }
    }
}