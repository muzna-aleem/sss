using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;

using Assignment6.Models.BO;
namespace Assignment6.Models.DAL
{
    public class SubmissionDAL : ParentDAL
    {
        public static int insert(Submission sub)
        {
            connect();

            sqlCommand = new SqlCommand("insert into Submission (SubmissionLink , DisplayName , AssignmentId , SubmitterId) Values( @SubmissionLink , @DisplayName , @AssignmentId ,  @SubmitterId)", sqlConnection);
            //   sqlCommand.Parameters.AddWithValue("@CourseCode", course.CourseCode);
            sqlCommand.Parameters.AddWithValue("@SubmissionLink", sub.Link);
            sqlCommand.Parameters.AddWithValue("@DisplayName", sub.DisplayName);
            sqlCommand.Parameters.AddWithValue("@AssignmentId", sub.AssignmentId);
            sqlCommand.Parameters.AddWithValue("@SubmitterId", sub.SubmitterId);
            
            return sqlCommand.ExecuteNonQuery();
        }

        public static List<Submission> getAssignmentSubmissions( int assId)
        {
            connect();
            sqlCommand = new SqlCommand("select * from Submission where AssignmentId = @AssignmentId ", sqlConnection);
            sqlCommand.Parameters.AddWithValue("@AssignmentId", assId );
            SqlDataReader dt = sqlCommand.ExecuteReader();
            List<Submission> list = new List<Submission>();
         
            while (dt.Read())
            {
                Submission ass = new Submission();
                ass.Id = (int)dt[0];
                ass.Link = dt[1].ToString();
                ass.DisplayName = dt[2].ToString();
                ass.AssignmentId = (int)dt[3];
                ass.SubmissionTime = dt[4].ToString();

                list.Add(ass);

            }
            return list;
        }
        public static Submission getSpecificSubmission(int subId)
        {
            connect();
            sqlCommand = new SqlCommand("select * from Submission where SubmissionId = @SubmissionId ", sqlConnection);
            sqlCommand.Parameters.AddWithValue("@SubmissionId", subId);
            SqlDataReader dt = sqlCommand.ExecuteReader();
            Submission ass = new Submission();

            if (dt.Read())
            {
               
                ass.Id = (int)dt[0];
                ass.Link = dt[1].ToString();
                ass.DisplayName = dt[2].ToString();
                ass.AssignmentId = (int)dt[3];
                ass.SubmissionTime = dt[4].ToString();

             

            }
            return ass;
        }

        public static Submission getSubmissionOfSepeificSubmiiterAndAssignment(int assId , int submitterId)
        {
            connect();
            sqlCommand = new SqlCommand("select * from Submission where  AssignmentId = @AssignmentId  And SubmitterId = @SubmitterId", sqlConnection);
            sqlCommand.Parameters.AddWithValue("@AssignmentId", assId);
            sqlCommand.Parameters.AddWithValue("@SubmitterId", submitterId);

            SqlDataReader dt = sqlCommand.ExecuteReader();
            Submission ass = new Submission();

            if (dt.Read())
            {

                ass.Id = (int)dt[0];
                ass.Link = dt[1].ToString();
                ass.DisplayName = dt[2].ToString();
                ass.AssignmentId = (int)dt[3];
                ass.SubmissionTime = dt[4].ToString();



            }
            return ass;
        }


    }
}