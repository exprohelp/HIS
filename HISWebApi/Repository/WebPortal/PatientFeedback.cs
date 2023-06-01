using HISWebApi.Models;
using HISWebApi.Repository.Utilities;
using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Net.Mail;

namespace HISWebApi.Repository.WebPortal
{
    public class PatientFeedback
    {
        public dataSet web_ContactUsQueries(ipPatientFeedback obj)
        {
            dataSet dsObj = new dataSet();
            using (SqlConnection con = new SqlConnection(GlobalConfig.ConStr_Hospital))
            {
                using (SqlCommand cmd = new SqlCommand("pweb_ContactUsQueries",con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandTimeout = 2500;
                    cmd.Parameters.AddWithValue("@Auto_Id", "0");
                    cmd.Parameters.AddWithValue("@DeptName", obj.DeptName);
                    cmd.Parameters.AddWithValue("@Designation ", obj.Designation);
                    cmd.Parameters.AddWithValue("@EmailId", obj.EmailId);
                    cmd.Parameters.AddWithValue("@EmpName", obj.EmpName);
                    cmd.Parameters.AddWithValue("@MobileNo", obj.MobileNo);
                    cmd.Parameters.AddWithValue("@Logic", obj.Logic);
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
        public string Web_InsertPatientFeedback(PatientFeedbackInfo objBO)
        {
            string processInfo = string.Empty;
            using (SqlConnection con = new SqlConnection(GlobalConfig.ConStr_Hospital))
            {
                using (SqlCommand cmd = new SqlCommand("Web_PatientFeedback", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandTimeout = 2500;
                    cmd.Parameters.Add("@Auto_Id", SqlDbType.VarChar, 20).Value = objBO.Auto_Id;
                    cmd.Parameters.Add("@QueryType", SqlDbType.VarChar, 20).Value = objBO.QueryType;
                    cmd.Parameters.Add("@EmailId", SqlDbType.VarChar, 20).Value = objBO.EmailId;
                    cmd.Parameters.Add("@MobileNo", SqlDbType.VarChar, 20).Value = objBO.MobileNo;
                    cmd.Parameters.Add("@Subject", SqlDbType.VarChar, 100).Value = objBO.Subject;
                    cmd.Parameters.Add("@Message", SqlDbType.VarChar, 100).Value = objBO.Message;
                    cmd.Parameters.Add("@Attachment", SqlDbType.VarChar, 100).Value = objBO.Attachment;
                    cmd.Parameters.Add("@virtual_path", SqlDbType.VarChar, 100).Value = objBO.virtual_path;
                    cmd.Parameters.Add("@physical_path", SqlDbType.VarChar, 100).Value = objBO.physical_path;
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
                }
                return processInfo;
            }
        }
        public dataSet PatientQueries(PatientFeedbackInfo objBO)
        {
            dataSet dsObj = new dataSet();
            using (SqlConnection con = new SqlConnection(GlobalConfig.ConStr_Hospital))
            {
                using (SqlCommand cmd = new SqlCommand("Web_PatientFeedbackQueries", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandTimeout = 2500;
                    cmd.Parameters.Add("@Auto_Id", SqlDbType.Int).Value = objBO.Auto_Id;
                    cmd.Parameters.Add("@QueryType", SqlDbType.VarChar, 10).Value = objBO.QueryType;
                    cmd.Parameters.Add("@EmailId", SqlDbType.VarChar, 20).Value = objBO.EmailId;
                    cmd.Parameters.Add("@MobileNo", SqlDbType.VarChar, 10).Value = objBO.MobileNo;
                    cmd.Parameters.Add("@Subject", SqlDbType.VarChar, 10).Value = objBO.Subject;
                    cmd.Parameters.Add("@Attachment", SqlDbType.VarChar, 10).Value = objBO.Attachment;
                    cmd.Parameters.Add("@Message", SqlDbType.DateTime, 10).Value = objBO.Message;
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
    }
}