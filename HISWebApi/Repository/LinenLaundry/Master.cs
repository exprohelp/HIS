using HISWebApi.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using static HISWebApi.Models.Warehouse;

namespace HISWebApi.Repository.LinenLaundry
{
    public class Master
    {
        public dataSet LL_MasterQueries(ipLinenLaundry objBO)
        {
            dataSet dsObj = new dataSet();

            using (SqlConnection con = new SqlConnection(GlobalConfig.ConStr_Hospital))
            {
                using (SqlCommand cmd = new SqlCommand("pLL_MasterQueries", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandTimeout = 2500;
                    cmd.Parameters.Add("@hosp_id", SqlDbType.VarChar, 10).Value = objBO.hosp_id;
                    cmd.Parameters.Add("@FloorName", SqlDbType.VarChar, 100).Value = objBO.FloorName;
                    cmd.Parameters.Add("@NSID", SqlDbType.VarChar, 20).Value = objBO.NSID;
                    cmd.Parameters.Add("@RoomId", SqlDbType.VarChar, 15).Value = objBO.RoomId;
                    cmd.Parameters.Add("@ItemId", SqlDbType.VarChar, 15).Value = objBO.ItemId;
                    cmd.Parameters.Add("@Prm1", SqlDbType.VarChar, 50).Value = objBO.Prm1;
                    cmd.Parameters.Add("@login_id", SqlDbType.VarChar, 50).Value = objBO.login_id;
                    cmd.Parameters.Add("@Logic", SqlDbType.VarChar, 50).Value = objBO.Logic;
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
        public string  LL_InsertUpdateMaster(RequestMaster objBO)
        {
            string processInfo = string.Empty;
            using (SqlConnection con = new SqlConnection(GlobalConfig.ConStr_Hospital))
            {
                using (SqlCommand cmd = new SqlCommand("pLL_InsertUpdateMaster", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandTimeout = 2500;
                    cmd.Parameters.Add("@AutoId", SqlDbType.Int, 10).Value = objBO.AutoId;
                    cmd.Parameters.Add("@hosp_id", SqlDbType.VarChar, 10).Value = objBO.hosp_id;
                    cmd.Parameters.Add("@NursingId", SqlDbType.VarChar, 10).Value = objBO.NursingId;
                    cmd.Parameters.Add("@RoomId", SqlDbType.VarChar, 10).Value = objBO.RoomId;
                    cmd.Parameters.Add("@TotalBeds", SqlDbType.Int, 10).Value = objBO.TotalBeds;
                    cmd.Parameters.Add("@ItemId", SqlDbType.VarChar, 10).Value = objBO.ItemId;
                    cmd.Parameters.Add("@ItemStock", SqlDbType.Int, 10).Value = objBO.ItemStock;
                    cmd.Parameters.Add("@InUse", SqlDbType.Int, 10).Value = objBO.InUse;
                    cmd.Parameters.Add("@ReadyToUse", SqlDbType.Int, 10).Value = objBO.ReadyToUse;
                    cmd.Parameters.Add("@InTransit", SqlDbType.Int, 10).Value = objBO.InTransit;
                    cmd.Parameters.Add("@TotalReq", SqlDbType.Int, 10).Value = objBO.TotalReq;
                    cmd.Parameters.Add("@IsActive", SqlDbType.Int, 1).Value = objBO.IsActive;
                    cmd.Parameters.Add("@login_id", SqlDbType.VarChar, 10).Value = objBO.login_id;
                    cmd.Parameters.Add("@Logic", SqlDbType.VarChar, 50).Value = objBO.Logic;
                    cmd.Parameters.Add("@result", SqlDbType.VarChar, 100).Value = "";
                    cmd.Parameters["@result"].Direction = ParameterDirection.InputOutput;
                    try
                    {
                        con.Open();
                        cmd.ExecuteNonQuery();
                        processInfo = (string)cmd.Parameters["@result"].Value.ToString();
                    }
                    catch (SqlException sqlEx) { processInfo = "Error Found   : " + sqlEx.Message; }
                    finally { con.Close(); }
                    return processInfo;
                }
            }
        }
  
    }
}