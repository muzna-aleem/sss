using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Assignment6.Models.BO
{
    public class Submission
    {
        public int Id { get; set; }
        public string Link { get; set; }
        public string DisplayName { get; set; }
        public int AssignmentId { get; set; }
        public String SubmissionTime { get; set; }
     
             public int SubmitterId { get; set; }
    }
}