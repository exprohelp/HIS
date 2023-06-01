using static HISWebApi.Models.Warehouse;
using Newtonsoft.Json;
using System.Data;
using System.Data.SqlClient;
using System;
using HISWebApi.Models;

namespace HISWebApi.Repository.Warehouse
{
    public class PharmacyStore
    {
        public dataSet Hospital_GeneralStoreQueries(Models.Warehouse.GeneralStoreQueries objBO)
        {
            dataSet dsObj = new dataSet();
            using (SqlConnection con = new SqlConnection(GlobalConfig.ConStr_ChandanPharmacyLive))
            {
                using (SqlCommand cmd = new SqlCommand("pHospital_GeneralStoreQueries", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandTimeout = 2500;
                    cmd.Parameters.Add("@unit_id", SqlDbType.VarChar, 10).Value = objBO.unit_id;
                    cmd.Parameters.Add("@TransferId", SqlDbType.VarChar, 20).Value = objBO.TransferId;
                    cmd.Parameters.Add("@prm_1", SqlDbType.VarChar, 100).Value = objBO.prm_1;
                    cmd.Parameters.Add("@from", SqlDbType.DateTime, 22).Value = objBO.from;
                    cmd.Parameters.Add("@login_id", SqlDbType.VarChar, 10).Value = objBO.login_id;
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

        public string Receive_TrfId_InStock(out string processInfo, string unit_id, string trf_id, string login_id, string voucher_no)
        {
            using (SqlConnection con = new SqlConnection(GlobalConfig.ConStr_ChandanPharmacyLive))
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand("pReceive_TrfId_InStock", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandTimeout = 2500;
                    cmd.Parameters.Add("@unit_id", SqlDbType.VarChar, 10).Value = unit_id;
                    cmd.Parameters.Add("@trf_id", SqlDbType.VarChar, 16).Value = trf_id;
                    cmd.Parameters.Add("@login_id", SqlDbType.VarChar, 10).Value = login_id;
                    cmd.Parameters.Add("@voucher_no", SqlDbType.VarChar, 16).Value = voucher_no;
                    cmd.Parameters["@voucher_no"].Direction = ParameterDirection.InputOutput;
                    try
                    {
                        cmd.ExecuteNonQuery();
                        processInfo = (string)cmd.Parameters["@result"].Value.ToString();
                    }
                    catch (Exception sqlEx)
                    {
                        processInfo = "Error Found   : " + sqlEx.Message;
                    }
                    finally { con.Close(); }
                }
            }
            return processInfo;
        }
       
    }
}