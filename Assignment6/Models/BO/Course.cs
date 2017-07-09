using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Assignment6.Models.BO
{
    public class Course
    {
        public String CourseCode { get ;set ;}
        public String CourseTitle { get; set; }
        public int CreditHours { get; set; }
        public int InstructorId { get; set; } 
    }
}