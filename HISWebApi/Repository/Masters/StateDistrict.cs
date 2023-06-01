using HISWebApi.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace HISWebApi.Repository.Masters
{
    public class StateDistrict
    {
        public dataSet mStateDistrictQueries(StateDistrictBO objBO)
        {
            dataSet dsObj = new dataSet();
            using (SqlConnection con = new SqlConnection(GlobalConfig.ConStr_Hospital))
            {
                using (SqlCommand cmd = new SqlCommand("pm_StateDistrictMasterQueries", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandTimeout = 2500;
                    cmd.Parameters.Add("@autoid", SqlDbType.Int).Value = objBO.autoid;
                    cmd.Parameters.Add("@hosp_id", SqlDbType.VarChar, 10).Value = objBO.hosp_id;
                    cmd.Parameters.Add("@CountryId", SqlDbType.VarChar, 20).Value = objBO.CountryId;
                    cmd.Parameters.Add("@StateCode", SqlDbType.VarChar, 20).Value = objBO.StateCode;
                    cmd.Parameters.Add("@DistrictCode", SqlDbType.VarChar, 20).Value = objBO.DistrictCode;                    
                    cmd.Parameters.Add("@prm_1", SqlDbType.VarChar, 100).Value = objBO.prm_1;
                    cmd.Parameters.Add("@prm_2", SqlDbType.VarChar, 100).Value = objBO.prm_2;
                    cmd.Parameters.Add("@Logic", SqlDbType.VarChar, 50).Value = objBO.Logic;
                    cmd.Parameters.Add("@login_id", SqlDbType.VarChar, 10).Value = objBO.login_id;
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

        public string mStateDistrictInsertUpdate(StateDistrictBO objBO)
        {
            dataSet dsObj = new dataSet();
            string processInfo = string.Empty;
            using (SqlConnection con = new SqlConnection(GlobalConfig.ConStr_Hospital))
            {
                using (SqlCommand cmd = new SqlCommand("pm_StateDistrictInsertUpdate", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandTimeout = 2500;
                    cmd.Parameters.Add("@autoid", SqlDbType.VarChar, 15).Value = objBO.autoid;
                    cmd.Parameters.Add("@hosp_id", SqlDbType.VarChar, 15).Value = objBO.hosp_id;
                    cmd.Parameters.Add("@CountryId", SqlDbType.VarChar, 15).Value = objBO.CountryId;
                    cmd.Parameters.Add("@StateCode", SqlDbType.VarChar, 15).Value = objBO.StateCode;
                    cmd.Parameters.Add("@StateName", SqlDbType.VarChar, 15).Value = objBO.StateName;
                    cmd.Parameters.Add("@DistrictCode", SqlDbType.VarChar, 20).Value = objBO.DistrictCode;
                    cmd.Parameters.Add("@DistrictName", SqlDbType.VarChar, 200).Value = objBO.DistrictName;                   
                    cmd.Parameters.Add("@login_id", SqlDbType.VarChar, 15).Value = objBO.login_id;
                    cmd.Parameters.Add("@Logic", SqlDbType.VarChar, 50).Value = objBO.Logic;
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