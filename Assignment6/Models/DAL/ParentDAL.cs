using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;



namespace Assignment6.Models.DAL
{
    public class ParentDAL
    {
        static protected SqlConnection sqlConnection;
        static protected SqlCommand sqlCommand;
        static public void connect()
        {
            //Server=51f194fb-1707-4065-8bb1-a7ab011249bf.sqlserver.sequelizer.com;Database=db51f194fb170740658bb1a7ab011249bf;User ID=nmanrmhlgylopluc;Password=Y7GRocxxn2QywUbFbQq6YzXQZyz5AvbQHVRaWndDgLsVVZdC62E4jZpxiZZWKA5Z;
            //sqlConnection = new SqlConnection(@"Data Source=DESKTOP-L918QFA\SQLEXPRESS;Initial Catalog=Presentador;Integrated Security=True");
           sqlConnection = new SqlConnection(@"Server=51f194fb-1707-4065-8bb1-a7ab011249bf.sqlserver.sequelizer.com;Database=db51f194fb170740658bb1a7ab011249bf;User ID=nmanrmhlgylopluc;Password=Y7GRocxxn2QywUbFbQq6YzXQZyz5AvbQHVRaWndDgLsVVZdC62E4jZpxiZZWKA5Z;");
            sqlConnection.Open();
        }
        static public void disconnect()
        {
            sqlConnection.Close();
        }
    }
}