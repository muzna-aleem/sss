using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Assignment6.Models.BO
{
    public class User
    {
        //private int UserID;
        //private string Login, Password, CodeForReset;

        public int UserId { get; set;} 
        public string Login {get; set;}
        public string Password { get; set; }
        public string CodeForReset { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string PictureLink { get; set; }
    }
}