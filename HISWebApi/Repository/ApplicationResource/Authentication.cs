using HISWebApi.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace HISWebApi.Repository.Inventory
{
    public class Authentication
    {     
        public dataSet AuthenticationQueries(ipAuthentication ip)
        {
            dataSet dsObj = new dataSet();
            using (SqlConnection con = new SqlConnection(GlobalConfig.ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("pAuthentication_Queries", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandTimeout = 2500;
                    cmd.Parameters.Add("@unit_id", SqlDbType.VarChar, 10).Value = ip.unit_id;
                    cmd.Parameters.Add("@LoginId", SqlDbType.VarChar, 16).Value = ip.LoginId;
                    cmd.Parameters.Add("@password", SqlDbType.VarChar).Value = ip.Password;
                    cmd.Parameters.Add("@role_id", SqlDbType.VarChar).Value = ip.role_id;
                    cmd.Parameters.Add("@menu_id", SqlDbType.VarChar, 50).Value = ip.menu_id;
                    cmd.Parameters.Add("@sub_menu_id", SqlDbType.VarChar, 50).Value = ip.sub_menu_id;
                    cmd.Parameters.Add("@Logic", SqlDbType.VarChar, 200).Value = ip.Logic;
                    try
                    {
                        con.Open();
                        DataSet ds = new DataSet();
                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        da.Fill(ds);
                        dsObj.Msg = "-";
                        if (ip.Logic=="Authenticate")
                        {
                            if(ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                                dsObj.Msg = "Success";
                            else
                                dsObj.Msg = "Failed To Authenticate";
                        }
                        dsObj.ResultSet = ds;
                    }
                    catch (SqlException sqlEx)
                    {
                        dsObj.ResultSet = null;
                        dsObj.Msg = "Error Found   : " + sqlEx.Message;
                    }
                    finally { con.Close(); }
                    return dsObj;
                }
            }
        }
    }
}

  