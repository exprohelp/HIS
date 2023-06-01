using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace HISWebApi.Repository.Inventory
{
    public class Connection
    {
        public SqlConnection con = new SqlConnection();
        public SqlCommand cmd;
        public SqlDataAdapter da;
        public Connection()
        {
            con.ConnectionString = ConfigurationManager.ConnectionStrings["Connection"].ToString();
            cmd = new SqlCommand("", con);
            da = new SqlDataAdapter("", con);
            da.SelectCommand = new SqlCommand("", con);
            da.InsertCommand = new SqlCommand("", con);
            da.UpdateCommand = new SqlCommand("", con);
            da.DeleteCommand = new SqlCommand("", con);
        }
        public void Open()
        {
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }
        }
        public void Close()
        {
            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }
        }
    }
}
