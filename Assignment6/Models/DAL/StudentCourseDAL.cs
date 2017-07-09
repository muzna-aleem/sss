using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;
using Assignment6.Models.BO;
namespace Assignment6.Models.DAL
{
    public class StudentCourseDAL : ParentDAL
    {
        public static int insert(int studentId , string courseId)
        {
            connect();
            sqlCommand = new SqlCommand("insert into StudentCourse (StudentId , CourseId ) Values(@StudentId , @CourseId)" , sqlConnection);
            sqlCommand.Parameters.AddWithValue("@StudentId" , studentId);
            sqlCommand.Parameters.AddWithValue("@CourseId", courseId);
            return sqlCommand.ExecuteNonQuery();
        }
        public static List<Course> getAllStudentCourses(int StId)
        {
            connect();
            string query = "Select * from Course inner join StudentCourse on CourseCode like CourseId where StudentID = @StudentID  ";
            sqlCommand = new SqlCommand(query, sqlConnection);

            sqlCommand.Parameters.AddWithValue("@StudentID", StId);
            //  sqlCommand.CommandType = CommandType.;


            IDataReader dataReader = sqlCommand.ExecuteReader();
            List<Course> list = new List<Course>();

            while (dataReader.Read())
            {
                Course course = new Course();
                course.CourseCode = dataReader[0].ToString();
                course.CourseTitle = dataReader[1].ToString();
                course.CreditHours = (int)dataReader[2];
                course.InstructorId = (int)dataReader[3]; 
                list.Add(course);

            }
            return list;
        }
    }
}