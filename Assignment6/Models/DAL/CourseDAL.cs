using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Assignment6.Models.BO;
using System.Data.SqlClient;
using System.Data;


namespace Assignment6.Models.DAL
{
    public class CourseDAL : ParentDAL
    {
       public static int insert(Course course)
        {
            connect();
          
            sqlCommand = new SqlCommand("insert_into_Course", sqlConnection);
         //   sqlCommand.Parameters.AddWithValue("@CourseCode", course.CourseCode);
            sqlCommand.Parameters.AddWithValue("@CourseTitle", course.CourseTitle);
            sqlCommand.Parameters.AddWithValue("@CreditHours", course.CreditHours);
            sqlCommand.Parameters.AddWithValue("@InstructorId", course.InstructorId);
            sqlCommand.CommandType = CommandType.StoredProcedure;

            return sqlCommand.ExecuteNonQuery();

            //int retVal = Convert.ToInt32(sqlCommand.Parameters["@retVal"].Value);
            //return u;
        }
        public static List<Course> getAllTeacherCourses (int teacherId)
        {
            connect();
            string query = "Select * from Course where InstructorId like @InstructorId" ;
            sqlCommand = new SqlCommand(query , sqlConnection);
            //   sqlCommand.Parameters.AddWithValue("@CourseCode", course.CourseCode);
          //  sqlCommand.Parameters.AddWithValue("@CourseTitle", course.CourseTitle);
           // sqlCommand.Parameters.AddWithValue("@CreditHours", course.CreditHours);
            sqlCommand.Parameters.AddWithValue("@InstructorId", teacherId);
          //  sqlCommand.CommandType = CommandType.;

            IDataReader dataReader =  sqlCommand.ExecuteReader();
            List<Course> list = new List<Course>();
          
            while (dataReader.Read())
            {
                Course course = new Course();
                course.CourseCode = dataReader[0].ToString();
                course.CourseTitle = dataReader[1].ToString();
                course.CreditHours =  (int)dataReader[2];
                list.Add(course);

            }
            return list;
        }
    }
}