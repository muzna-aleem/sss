using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Assignment6.Models.BO;
using System.Data.SqlClient;
using System.Data;

namespace Assignment6.Models.DAL
{
    public class AssignmentDAL : ParentDAL
    {
        public static int insert(Assignment assignment)
        {
            connect();
            //DateTime dt = Convert.ToDateTime(assignment.Deadline);
            string query = "insert into Assignment (Deadline, AssignmentTitle , DisplayName ,ProblemLink ,  DifficultyLevel , Marks , Format , CourseCode ) Values(@Deadline, @AssignmentTitle , @DisplayName , @ProblemLink ,  @DifficultyLevel , @Marks , @Format , @CourseCode)";
            sqlCommand = new SqlCommand(query , sqlConnection);
            sqlCommand.Parameters.AddWithValue("@Deadline" , assignment.Deadline);
            sqlCommand.Parameters.AddWithValue("@AssignmentTitle", assignment.Title);
            sqlCommand.Parameters.AddWithValue("@DisplayName", assignment.DisplayName);
            sqlCommand.Parameters.AddWithValue("@ProblemLink", assignment.ProblemLink);
            sqlCommand.Parameters.AddWithValue("@DifficultyLevel", assignment.DifficultyLevel);
            sqlCommand.Parameters.AddWithValue("@Marks", assignment.Marks);
            sqlCommand.Parameters.AddWithValue("@Format", assignment.Format);
            sqlCommand.Parameters.AddWithValue("@CourseCode", assignment.CourseCode);
            return sqlCommand.ExecuteNonQuery();
        }
        public static List<Assignment> getCourseAssignments(string courseCode)
        {
            connect();
            sqlCommand = new SqlCommand("select * from Assignment where CourseCode like @CourseCode" , sqlConnection);
            sqlCommand.Parameters.AddWithValue("@CourseCode" , courseCode);
           SqlDataReader dt = sqlCommand.ExecuteReader();
           List<Assignment> list = new List<Assignment>();
         
            while(dt.Read())
            {
                Assignment ass = new Assignment();
                ass.AssignmentId = (int)dt[0];
                ass.Deadline = dt[1].ToString();
                ass.Title = dt[2].ToString();
                ass.DisplayName = dt[3].ToString();
                ass.ProblemLink = dt[4].ToString();
                ass.DifficultyLevel = dt[5].ToString();
                ass.Marks =(int) dt[6];
                ass.Format = dt[7].ToString();
                ass.CourseCode = dt[8].ToString();


                list.Add(ass);

            }
            return list;
        }

        public static Assignment getSpecificAssignment (int assId)
        {
              connect();
              sqlCommand = new SqlCommand("select * from Assignment where AssignmentId = @AssignmentId ", sqlConnection);
              sqlCommand.Parameters.AddWithValue("@AssignmentId", assId);

           SqlDataReader dt = sqlCommand.ExecuteReader();
           Assignment ass = new Assignment();
            if(dt.Read())
            {
                 ass = new Assignment();
                ass.AssignmentId = (int)dt[0];
                ass.Deadline = dt[1].ToString();
                ass.Title = dt[2].ToString();
                ass.DisplayName = dt[3].ToString();
                ass.ProblemLink = dt[4].ToString();
                ass.DifficultyLevel = dt[5].ToString();
                ass.Marks =(int) dt[6];
                ass.Format = dt[7].ToString();
                ass.CourseCode = dt[8].ToString();


               

            }
            return ass;
        }

    }
}