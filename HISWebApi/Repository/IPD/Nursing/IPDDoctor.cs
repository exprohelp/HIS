using HISWebApi.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using static HISWebApi.Models.IPDBO;

namespace HISWebApi.Repository.IPD
{
    public class IPDDoctor
    {
        public dataSet IPD_DoctorQueries(IPDInfo objBO)
        {
            dataSet dsObj = new dataSet();
            using (SqlConnection con = new SqlConnection(GlobalConfig.ConStr_Hospital))
            {
                using (SqlCommand cmd = new SqlCommand("pIPD_DoctorQueries", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandTimeout = 2500;
                    cmd.Parameters.Add("@hosp_id", SqlDbType.VarChar, 10).Value = objBO.hosp_id;
                    cmd.Parameters.Add("@UHID", SqlDbType.VarChar, 20).Value = objBO.UHID;
                    cmd.Parameters.Add("@IPDNo", SqlDbType.VarChar, 11).Value = objBO.IPDNo;
                    cmd.Parameters.Add("@Prm1", SqlDbType.VarChar, 500).Value = objBO.Prm1;
                    cmd.Parameters.Add("@Prm2", SqlDbType.VarChar, 500).Value = objBO.Prm2;
                    cmd.Parameters.Add("@from", SqlDbType.Date).Value = objBO.from;
                    cmd.Parameters.Add("@to", SqlDbType.Date).Value = objBO.to;                
                    cmd.Parameters.Add("@login_id", SqlDbType.VarChar, 20).Value = objBO.login_id;
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
        public string InsertDischargeTemplate(DischargeTemplateInfo objBO)
        {
            string processInfo = string.Empty;
            using (SqlConnection con = new SqlConnection(GlobalConfig.ConStr_Hospital))
            {
                using (SqlCommand cmd = new SqlCommand("pm_InsertDischargeTemplate", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandTimeout = 2500;
                    cmd.Parameters.Add("@AutoId", SqlDbType.Int).Value = objBO.AutoId;
                    cmd.Parameters.Add("@UHID", SqlDbType.VarChar, 30).Value = objBO.UHID;
                    cmd.Parameters.Add("@IPDNo", SqlDbType.VarChar, 30).Value = objBO.IPDNo;
                    cmd.Parameters.Add("@HeaderId", SqlDbType.VarChar, 20).Value = objBO.HeaderId;
                    cmd.Parameters.Add("@HeaderName", SqlDbType.VarChar, 100).Value = objBO.HeaderName;
                    cmd.Parameters.Add("@TemplateId", SqlDbType.VarChar, 20).Value = objBO.TemplateId;
                    cmd.Parameters.Add("@TemplateName", SqlDbType.VarChar, 100).Value = objBO.TemplateName;
                    cmd.Parameters.Add("@Prm1", SqlDbType.VarChar, 100).Value = objBO.Prm1;
                    cmd.Parameters.Add("@Prm2", SqlDbType.VarChar, 100).Value = objBO.Prm2;                    
                    cmd.Parameters.Add("@login_id", SqlDbType.VarChar, 50).Value = objBO.login_id;
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
        public string IPD_InsertDischargeReportInfo(DischargeReportInfo objBO)
        {
            string processInfo = string.Empty;
            using (SqlConnection con = new SqlConnection(GlobalConfig.ConStr_Hospital))
            {
                using (SqlCommand cmd = new SqlCommand("pIPD_InsertDischargeReportInfo", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandTimeout = 2500;                             
                    cmd.Parameters.Add("@IPDNo", SqlDbType.VarChar, 30).Value = objBO.IPDNo;                 
                    cmd.Parameters.Add("@TemplateId", SqlDbType.VarChar, 20).Value = objBO.TemplateId;
                    cmd.Parameters.Add("@TemplateName", SqlDbType.VarChar, 100).Value = objBO.TemplateName;
                    cmd.Parameters.Add("@TemplateContent", SqlDbType.NVarChar).Value = objBO.TemplateContent;
                    cmd.Parameters.Add("@Prm1", SqlDbType.VarChar, 100).Value = objBO.Prm1;
                    cmd.Parameters.Add("@Prm2", SqlDbType.VarChar, 100).Value = objBO.Prm2;
                    cmd.Parameters.Add("@login_id", SqlDbType.VarChar, 50).Value = objBO.login_id;
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