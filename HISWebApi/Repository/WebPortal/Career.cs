using HISWebApi.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace HISWebApi.Repository.WebPortal
{
    public class Career
    {        
        public dataSet CareerInfo(CarrierInfo objBO)
        {
            dataSet dsObj = new dataSet();
            using (SqlConnection con = new SqlConnection(GlobalConfig.ConStr_Hospital))
            {
                using (SqlCommand cmd = new SqlCommand("pCareer_Insert", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandTimeout = 2500;
                    cmd.Parameters.Add("@Id", SqlDbType.Int).Value = objBO.Id;
                    cmd.Parameters.Add("@Job_For", SqlDbType.VarChar, 20).Value = objBO.JobInformation;
                    cmd.Parameters.Add("@Job_Description", SqlDbType.VarChar, 20).Value = objBO.Job_Description;
                    cmd.Parameters.Add("@Desired_Profile", SqlDbType.VarChar, 10).Value = objBO.Desired_Profile;
                    cmd.Parameters.Add("@Other_Details", SqlDbType.VarChar, 10).Value = objBO.Other_Details;
                    cmd.Parameters.Add("@No_Of_Openings", SqlDbType.Int).Value = objBO.No_Of_Openings;
                    cmd.Parameters.Add("@IsActive", SqlDbType.VarChar, 100).Value = objBO.IsActive;
                    cmd.Parameters.Add("@login_id", SqlDbType.VarChar, 100).Value = objBO.login_id;
                    cmd.Parameters.Add("@Logic", SqlDbType.VarChar, 20).Value = objBO.Logic;
                    cmd.Parameters.Add("@result", SqlDbType.VarChar, 500).Value = "";
                    cmd.Parameters["@result"].Direction = ParameterDirection.InputOutput;
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
        public string InsertWebAward(AwardInfo objBO)
        {
            string processInfo = string.Empty;
            using (SqlConnection con = new SqlConnection(GlobalConfig.ConStr_Hospital))
            {
                using (SqlCommand cmd = new SqlCommand("pInsertWeb_Award", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandTimeout = 2500;
                    cmd.Parameters.Add("@AutoId", SqlDbType.BigInt).Value = objBO.Auto_Id;
                    cmd.Parameters.Add("@AwardName", SqlDbType.VarChar, 200).Value = objBO.AwardName;
                    cmd.Parameters.Add("@FilePath", SqlDbType.VarChar, 100).Value = objBO.FilePath;
                    cmd.Parameters.Add("@IsActive", SqlDbType.Char, 1).Value = objBO.IsActive;
                    cmd.Parameters.Add("@login_id", SqlDbType.VarChar, 20).Value = objBO.login_id;
                    cmd.Parameters.Add("@Logic", SqlDbType.VarChar, 50).Value = objBO.Logic;
                    cmd.Parameters.Add("@result", SqlDbType.VarChar, 100).Value = "";
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
        public dataSet GetWebAwardInfo(AwardInfo objBO)
        {
            dataSet dsObj = new dataSet();
            using (SqlConnection con = new SqlConnection(GlobalConfig.ConStr_Hospital))
            {
                using (SqlCommand cmd = new SqlCommand("pWeb_AwardQueries", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandTimeout = 2500;
                    cmd.Parameters.Add("@AutoId", SqlDbType.BigInt).Value = objBO.Auto_Id;
                    cmd.Parameters.Add("@AwardName", SqlDbType.VarChar, 200).Value = objBO.AwardName;
                    cmd.Parameters.Add("@FilePath", SqlDbType.VarChar, 100).Value = objBO.FilePath;
                    cmd.Parameters.Add("@IsActive", SqlDbType.Char, 1).Value = objBO.IsActive;
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
    }
}