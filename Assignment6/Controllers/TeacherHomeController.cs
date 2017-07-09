using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Assignment6.Models.BO;
using Assignment6.Models.DAL;

namespace Assignment6.Controllers
{
    public class TeacherHomeController : Controller
    {
        //
        // GET: /Teacher/


        public ActionResult Index()
        {
            User u = (User)Session["user"];
            List<Course> list = CourseDAL.getAllTeacherCourses(u.UserId);
            return View(list);
        }

        [HttpGet]
        public ActionResult Assignments(string id)
        {
            //ViewData["course"] = id;
           // ViewBag.course = id;


            Session["course"] = id;
            List<Assignment> list = AssignmentDAL.getCourseAssignments(id);
            return View("Assignments" , list);
        }
        [HttpGet]
        public ActionResult LogOut()
        {
            Session.Clear();
            return View("~/Views/Home/Login.cshtml");
        }
        public ActionResult CreateNewAssignment()
        {
          
            return View("CreateNewAssignment");
        }

        [HttpGet]
        public ActionResult AssignmentDetails(int id)
        {
            List<Submission> list = SubmissionDAL.getAssignmentSubmissions(id);
            Session["assignment"] = id;
            return View(list);
        }
        //string AssTitle , string AssName , string AssLevel , string AssMarks , string AssFormat , string AssDeadline
        [HttpPost]
        public ActionResult saveNewAssignment( Assignment a  )
        {
            var uniqueness = "";
            var fileSavePath="";
            if (Request.Files["ProblemLink"] != null)
            {
                var file = Request.Files["ProblemLink"];
                if (file.FileName != "")
                {
                    var ext = System.IO.Path.GetExtension(file.FileName);
                    uniqueness = Guid.NewGuid().ToString()+ext;
                    var rootPath = Server.MapPath("~/Assignments");
                    fileSavePath = System.IO.Path.Combine(rootPath , uniqueness );
                    file.SaveAs(fileSavePath);
                }
                a.ProblemLink= fileSavePath;
                a.CourseCode = Session["course"].ToString();
                AssignmentDAL.insert(a);
            }
            String id = Session["course"].ToString();
            List<Assignment> list = AssignmentDAL.getCourseAssignments(id);
            return View("Assignments", list);
           // return View("Assignments");
        }
       
        [HttpPost]
        public ActionResult insertCourse( Course course)
        {

           Object data = null;
           User u =  (User)Session["user"];
           course.InstructorId = u.UserId;
           int r = CourseDAL.insert(course);

         
 

           List<Course> list = CourseDAL.getAllTeacherCourses(u.UserId);
           return View("Index", list);

          // return Json(data, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult Download(int id)
        {

            Session.Add("submission" , id);
            Submission sub = SubmissionDAL.getSpecificSubmission(id);
            string fullName = sub.Link;
            string extension = getExtension(fullName);
            byte[] fileBytes = GetFile(fullName);
            string fileName = sub.DisplayName+"."+extension;
            return File(
                fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, fileName);
        }
        string getExtension(string fileName)
        {
            String[] str = fileName.Split('.');
            return str[str.Length - 1];
        }

        byte[] GetFile(string s)
        {
            System.IO.FileStream fs = System.IO.File.OpenRead(s);
            byte[] data = new byte[fs.Length];
            int br = fs.Read(data, 0, data.Length);
            if (br != fs.Length)
                throw new System.IO.IOException(s);
            return data;
        }
        
        //
        //  GET: /Teacher/Details/5

        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /Teacher/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Teacher/Create

        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Teacher/Edit/5

        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /Teacher/Edit/5

        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Teacher/Delete/5

        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /Teacher/Delete/5

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
