using HISWebApi.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace HISWebApi.Repository.WebPortal
{
    public class WebMaster
    {
        public dataSet GetNotificationMaster(NotificationMaster obj)
        {
            dataSet dsObj = new dataSet();
            using (SqlConnection con = new SqlConnection(GlobalConfig.ConStr_MobileAppDb))
            {
                using (SqlCommand cmd = new SqlCommand("pMob_GetNotificationMaster", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandTimeout = 2500;
                    cmd.Parameters.AddWithValue("@City_Id", obj.City_Id);
                    cmd.Parameters.AddWithValue("@City_Name", obj.City_Name);
                    cmd.Parameters.AddWithValue("@logic ", obj.Logic);
                   cmd.Parameters.AddWithValue("@TokenNo", obj.TokenNo);               
                    try
                    {
                        con.Open();
                        DataSet ds = new DataSet();
                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        da.Fill(ds);
                        dsObj.ResultSet = ds;
                        dsObj.Msg = "Success";
                        con.Close();
                    }
                    catch (SqlException sqlEx)
                    {
                        dsObj.ResultSet = null;
                        dsObj.Msg = sqlEx.Message;
                    }
                    finally { con.Close(); }
                    return dsObj;
                }
            }
        }
    }
}