using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using Assignment6.Models.BO;
using Assignment6.Models.Utilities;
using System.Web;
using System.Web.Mvc;
using System.Security.Cryptography;
using System.Text;
using System.Web.Configuration;


namespace Assignment6.Models.DAL
{
    
   public class UserDAL : ParentDAL
    {
      static internal string getHashSha256(string text)
       {
           byte[] bytes = Encoding.Unicode.GetBytes(text);
           SHA256Managed hashstring = new SHA256Managed();
           byte[] hash = hashstring.ComputeHash(bytes);
           string hashString = string.Empty;
           foreach (byte x in hash)
           {
               hashString += String.Format("{0:x2}", x);
           }
           return hashString;
       }
        static public User insert(User user)
        {
            connect();
            user.Password = getHashSha256(user.Password);
            sqlCommand = new SqlCommand("insert_after_validating" , sqlConnection);
            sqlCommand.Parameters.AddWithValue("@Email" , user.Login);
            sqlCommand.Parameters.AddWithValue("@Password", user.Password);
            sqlCommand.Parameters.AddWithValue("@Name", user.Name);
            sqlCommand.Parameters.AddWithValue("@Type", user.Type);
            sqlCommand.CommandType = CommandType.StoredProcedure;
            SqlParameter returnParameter = sqlCommand.Parameters.Add("@retVal", SqlDbType.Int);
            returnParameter.Direction = ParameterDirection.ReturnValue;
            sqlCommand.ExecuteNonQuery();
            int retVal = Convert.ToInt32( sqlCommand.Parameters["@retVal"].Value);
            if (retVal == 0)
            {
                User u = UserDAL.getUser(user.Login);
                return u;
            }
            else
                return null;
        }
        static public int generateCode(string login) //returns 0 when succussfully code sent, -1 when email does not exists , 1 when code not sent due to internet issue
        {
            connect();
            sqlCommand = new SqlCommand("generate_code", sqlConnection);
            sqlCommand.Parameters.AddWithValue("@Email", login);
            
            sqlCommand.CommandType = CommandType.StoredProcedure;
            SqlParameter returnParameter1 = sqlCommand.Parameters.Add("@retVal1", SqlDbType.Int);
            returnParameter1.Direction = ParameterDirection.ReturnValue;
            SqlParameter returnParameter2 = sqlCommand.Parameters.Add("@retVal", SqlDbType.NVarChar);
            returnParameter1.Direction = ParameterDirection.ReturnValue;
            returnParameter2.Direction = ParameterDirection.ReturnValue;
            IDataReader reader =  sqlCommand.ExecuteReader();
            User u = new User();
            int codeSentStatus=1;
       
            if (reader.Read())
            {
                u.CodeForReset = reader[0].ToString();
                u.Login = reader[1].ToString();
               // string path = Url.Content("http://localhost:29325/Home/ForgotPassword");
                string path = "http://bsef14m041.apphb.com/Home/validate?email=" + u.Login + "&codeForReset=" + u.CodeForReset;
                string body = path;
                //string senderLogin = WebConfigurationManager.AppSettings["senderLogin"];
                string senderPassword = System.Configuration.ConfigurationManager.AppSettings["senderPassword"].ToString();
                string senderLogin = System.Configuration.ConfigurationManager.AppSettings["senderLogin"].ToString();

              
                codeSentStatus = EmailHandler.SendEmail( u.Login ,"Password Recovery Details" , body , senderLogin , senderPassword );
            }

            int retVal = Convert.ToInt32(sqlCommand.Parameters["@retVal1"].Value);
            if(retVal == 0)
                return codeSentStatus;
            else
            //string r = (sqlCommand.Parameters["@retVal"].Value).ToString();
            return retVal;
        
        }


        static public int validateCode( string login , string code)
        {
            connect();
            sqlCommand = new SqlCommand("validate_code", sqlConnection);
            sqlCommand.Parameters.AddWithValue("@Email", login);
            sqlCommand.Parameters.AddWithValue("@CodeForReset", code);
            sqlCommand.CommandType = CommandType.StoredProcedure;
            SqlParameter returnParameter = sqlCommand.Parameters.Add("@retVal", SqlDbType.Int);
            returnParameter.Direction = ParameterDirection.ReturnValue;
            sqlCommand.ExecuteNonQuery();
            int retVal = Convert.ToInt32(sqlCommand.Parameters["@retVal"].Value);

            return retVal;
        }
        static public User getUser(string email)
        {
            connect();
            sqlCommand = new SqlCommand("Select * from dbo.[User] where Email like @Email", sqlConnection);
            sqlCommand.Parameters.AddWithValue("@Email", email);
            SqlDataReader d = sqlCommand.ExecuteReader();
            User u = new User();
            if (d.Read())
            {
                u.Login = d[0].ToString();
                u.Name = d[1].ToString();
                u.Password = d[2].ToString();
                u.Type = d[3].ToString();
                u.UserId =(int) d[5];
                u.CodeForReset = d[4].ToString();



            }
            return u;
        }

        static public int updatePassword (string login, string password)
        {
            connect();
            password = getHashSha256(password);
            sqlCommand = new SqlCommand("update_password", sqlConnection);
            sqlCommand.Parameters.AddWithValue("@Email", login);
            sqlCommand.Parameters.AddWithValue("@Password", password);
            sqlCommand.CommandType = CommandType.StoredProcedure;
            
           
           int retVal = Convert.ToInt32(sqlCommand.ExecuteNonQuery());

            return retVal;
        }

    }
}