using HISWebApi.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace HISWebApi.Repository.Masters
{
    public class AmbulanceMaster
    {
        public dataSet Ambulance_MasterQueries(ipAmbulance objBO)
        {
            dataSet dsObj = new dataSet();
            using (SqlConnection con = new SqlConnection(GlobalConfig.ConStr_Hospital))
            {
                using (SqlCommand cmd = new SqlCommand("pAmbulance_MasterQueries", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandTimeout = 2500;
                    cmd.Parameters.Add("@DriverId", SqlDbType.VarChar, 10).Value = objBO.DriverId;
                    cmd.Parameters.Add("@AmbulanceId", SqlDbType.VarChar, 10).Value = objBO.AmbulanceId;
                    cmd.Parameters.Add("@Prm1", SqlDbType.VarChar, 100).Value = objBO.Prm1;
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
        public string Ambulance_InsertUpdateMaster(AmbulanceMastersInfo obj)
        {
            dataSet dsObj = new dataSet();
            string processInfo = string.Empty;
            using (SqlConnection con = new SqlConnection(GlobalConfig.ConStr_Hospital))
            {
                using (SqlCommand cmd = new SqlCommand("pAmbulance_InsertUpdateMaster", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandTimeout = 2500;
                    cmd.Parameters.Add("@DriverId", SqlDbType.VarChar, 10).Value = obj.DriverId;
                    cmd.Parameters.Add("@DriverName", SqlDbType.VarChar, 100).Value = obj.DriverName;
                    cmd.Parameters.Add("@DriverMobileNo", SqlDbType.VarChar, 10).Value = obj.DriverMobileNo;
                    cmd.Parameters.Add("@AmbulanceId", SqlDbType.VarChar, 10).Value = obj.AmbulanceId;
                    cmd.Parameters.Add("@AmbulanceName", SqlDbType.VarChar, 80).Value = obj.AmbulanceName;
                    cmd.Parameters.Add("@AmbulanceNumber", SqlDbType.VarChar, 50).Value = obj.AmbulanceNumber;
                    cmd.Parameters.Add("@AmbulanceMobileNo", SqlDbType.VarChar, 10).Value = obj.AmbulanceMobileNo;
                    cmd.Parameters.Add("@DocType", SqlDbType.VarChar, 100).Value = obj.DocType;
                    cmd.Parameters.Add("@FilePath", SqlDbType.VarChar, 100).Value = obj.FilePath;
                    cmd.Parameters.Add("@Prm1", SqlDbType.VarChar, 100).Value = obj.Prm1;
                    cmd.Parameters.Add("@IsActive", SqlDbType.Char, 1).Value = obj.IsActive;
                    cmd.Parameters.Add("@login_id", SqlDbType.VarChar, 20).Value = obj.login_id;
                    cmd.Parameters.Add("@Logic", SqlDbType.VarChar, 50).Value = obj.Logic;
                    cmd.Parameters.Add("@result", SqlDbType.VarChar, 50).Value = "";
                    cmd.Parameters["@result"].Direction = ParameterDirection.InputOutput;
                    try
                    {
                        con.Open();
                        cmd.ExecuteNonQuery();
                        processInfo = (string)cmd.Parameters["@result"].Value.ToString();
                        con.Close();
                    }
                    catch (SqlException sqlEx)
                    {
                        processInfo = "Error Found   : " + sqlEx.Message;
                    }
                    finally { con.Close(); }
                    return processInfo;
                }
            }
        }
    }
}