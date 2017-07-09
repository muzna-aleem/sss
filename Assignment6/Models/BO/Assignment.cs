using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Assignment6.Models.BO
{
    public class Assignment
    {
        public int AssignmentId { get; set; }
        public string Title { get; set; }
        public string Deadline { get; set; }
        public string DisplayName { get; set; }
        public string ProblemLink { get; set; }
        public string DifficultyLevel { get; set; }
        public int Marks { get; set; }
        public string Format { get; set; }
        public string Solution { get; set; }
        public string CourseCode { get; set; }
    }
}