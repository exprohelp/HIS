using HISWebApi.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace HISWebApi.Repository.Masters
{
    public class FollowupMarking
    {
        public dataSet mPanelQueries(FollowupMarkingBO objBO)
        {
            dataSet dsObj = new dataSet();
            using (SqlConnection con = new SqlConnection(GlobalConfig.ConStr_Hospital))
            {
                using (SqlCommand cmd = new SqlCommand("pm_PanelQueries", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandTimeout = 2500;
                    cmd.Parameters.Add("@hospid", SqlDbType.VarChar,15).Value = objBO.HospId;
                    cmd.Parameters.Add("@panelid", SqlDbType.VarChar,20).Value = objBO.PanelId;
                    cmd.Parameters.Add("@doctorid", SqlDbType.VarChar, 20).Value = objBO.DoctorId;
                    cmd.Parameters.Add("@deptid", SqlDbType.VarChar, 20).Value = objBO.DeptId;                    
                    cmd.Parameters.Add("@itemid", SqlDbType.VarChar, 20).Value = objBO.ItemId;                   
                    cmd.Parameters.Add("@prm_1", SqlDbType.VarChar, 50).Value = objBO.prm_1;
                    cmd.Parameters.Add("@prm_2", SqlDbType.VarChar, 50).Value = objBO.prm_2;
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

        public string mPanelFollowupInsertUpdate(FollowupMarkingBO objBO)
        {
            dataSet dsObj = new dataSet();
            string processInfo = string.Empty;           
            DataTable followdt = JsonConvert.DeserializeObject<DataTable>(objBO.FollowpDetails);
            var duplicates = followdt.AsEnumerable().GroupBy(r => r[0]).Where(gr => gr.Count() > 1).ToList();
            if (duplicates.Any())
            {
            }
            if (followdt.Rows.Count > 0)
            {
                objBO.FollowupTable = followdt;
            }
            using (SqlConnection con = new SqlConnection(GlobalConfig.ConStr_Hospital))
            {
                using (SqlCommand cmd = new SqlCommand("pm_PanelFollowupInsertUpdate", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandTimeout = 2500;
                    cmd.Parameters.AddWithValue("@followupdt", followdt);                   
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