using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Assignment6.Models.BO;
using Assignment6.Models.DAL;
using System.IO;


namespace Assignment6.Controllers
{
    public class StudentHomeController : Controller
    {
        //
        // GET: /StudentHome/

        public ActionResult Index()
        {
            User u = (User)Session["user"];
          
            List<Course> list = StudentCourseDAL.getAllStudentCourses(u.UserId);
            return View("index", list);
        }
           [HttpPost]
        public ActionResult insertStudentCourse(string courseId)
        {
            User u = (User)Session["user"];
            StudentCourseDAL.insert(u.UserId , courseId);
            List<Course> list = StudentCourseDAL.getAllStudentCourses(u.UserId);
            return View("index" , list);
        }
           [HttpGet]
           public ActionResult Assignments(string id)
           {
               //ViewData["course"] = id;
               // ViewBag.course = id;


               Session["course"] = id;
               List<Assignment> list = AssignmentDAL.getCourseAssignments(id);
               return View("Assignments", list);
           }

           public ActionResult submitAssignment(Submission sub)
           {
               //Session["assignment"] = id;
               //User u = (User)Session["user"];
               //Submission sub = SubmissionDAL.getSubmissionOfSepeificSubmiiterAndAssignment(id, u.UserId);

               int id =  (int)Session["assignment"] ;
               User u = (User)Session["user"];

                 Submission s = SubmissionDAL.getSubmissionOfSepeificSubmiiterAndAssignment(id , u.UserId);
                 if (s.Id == 0)
                 {

                     //User u = (User)Session["user"];
                     sub.SubmitterId = u.UserId;
                     sub.DisplayName = u.Name;
                     //  Assignment a = (Assignment)Session["assignment"]);
                     sub.AssignmentId = Convert.ToInt32(Session["assignment"].ToString());


                     var uniqueness = "";
                     var fileSavePath = "";
                     if (Request.Files["Link"] != null)
                     {
                         var file = Request.Files["Link"];
                         if (file.FileName != "")
                         {
                             var ext = System.IO.Path.GetExtension(file.FileName);
                             uniqueness = Guid.NewGuid().ToString() + ext;
                             var rootPath = Server.MapPath("~/Submissions");
                             fileSavePath = System.IO.Path.Combine(rootPath, uniqueness);
                             file.SaveAs(fileSavePath);
                         }
                         sub.Link = fileSavePath;

                     }
                     SubmissionDAL.insert(sub);
                 }
              
             //  Session.Add( sub.Id.ToString() , "ok");

               return View("submissionSubmitted");
           }

           public ActionResult submissionSubmitted()
           {
               return View();
           }

   public ActionResult AssignmentSubmission(int id)
           {
               Session["assignment"] = id;
               User u =(User) Session["user"];
               Submission sub = SubmissionDAL.getSubmissionOfSepeificSubmiiterAndAssignment(id , u.UserId);
               if (sub.Id == 0)
                   return View("submitAssignment");
             //  Session.Add(sub.Id.ToString(), "ok");
               else

               return View("submissionSubmitted");
           }
            [HttpGet]
           public ActionResult Download()
           {
               int id = (int)Session["assignment"];
             
               Assignment ass = AssignmentDAL.getSpecificAssignment(id);
               string fullName = ass.ProblemLink;
               string extension = getExtension(fullName);
               byte[] fileBytes = GetFile(fullName);
               string fileName = ass.DisplayName+"."+extension;
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
        [HttpGet]
           public ActionResult LogOut()
           {
               Session.Clear();
               return View("~/Views/Home/Login.cshtml");
           }

        //
        // GET: /StudentHome/Details/5

        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /StudentHome/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /StudentHome/Create

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
        // GET: /StudentHome/Edit/5

        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /StudentHome/Edit/5

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
        // GET: /StudentHome/Delete/5

        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /StudentHome/Delete/5

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
