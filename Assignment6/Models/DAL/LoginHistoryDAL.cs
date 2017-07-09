using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Assignment6.Models.BO;
using System.Data.SqlClient;
using System.Data;
using System.Web.SessionState;
using System.Web;
using System.Web.Mvc;


namespace Assignment6.Models.DAL
{
    public class LoginHistoryDAL : ParentDAL
    {

        static public User insert(User user)
        {
            connect();
            user.Password = UserDAL.getHashSha256(user.Password);
            sqlCommand = new SqlCommand("insert_in_login_history_after_validating_user", sqlConnection);
            sqlCommand.Parameters.AddWithValue("@Email", user.Login);
            sqlCommand.Parameters.AddWithValue("@Password", user.Password);
            sqlCommand.CommandType = CommandType.StoredProcedure;
            SqlParameter returnParameter = sqlCommand.Parameters.Add("@retVal", SqlDbType.Int);
            returnParameter.Direction = ParameterDirection.ReturnValue;
            SqlDataReader dataReader = sqlCommand.ExecuteReader();
            User u = null ;
            if (dataReader.Read())
            {
                u = new User();
                u.Login=dataReader[0].ToString();
                u.Name = dataReader[1].ToString();
                u.Password = dataReader[2].ToString();
                u.Type = dataReader[3].ToString();
                u.PictureLink = dataReader[4].ToString();
                u.UserId =  Convert.ToInt32( dataReader[5]);
              
            }
            //int retVal = Convert.ToInt32(sqlCommand.Parameters["@retVal"].Value);
            return u;
          
        }
       
    }
}