using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace HISWebApi
{
    public static class DBManager
    {
        public static string QueryExecute(string qry,string connectionString)
        {
            string result = string.Empty;
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand(qry, con))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandTimeout = 2500;
                    try
                    {
                        if(cmd.ExecuteNonQuery() > 0)
                          result = "Success";
                        else
                          result = "Failed";
                    }
                    catch (SqlException sqlEx)
                    {
                        result = sqlEx.Message;
                    }
                    finally { con.Close(); }

                }
            }
            return result;
        }
        public static DataSet GetQueryResult(string qry, string connectionString)
        {
            DataSet ds = new DataSet();
            string result = string.Empty;
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand(qry, con))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandTimeout = 2500;
                    try
                    {
                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        da.Fill(ds);
                    }
                    catch (SqlException sqlEx)
                    {
                        result = sqlEx.Message;
                    }
                    finally { con.Close(); }
                }
            }
            return ds;
        }
    }
}