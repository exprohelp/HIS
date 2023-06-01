using HISWebApi.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace HISWebApi.Repository.Masters
{
	public class CPOE
	{
        public dataSet CPOE_MasterQueries(CPOETemplateItemsBO objBO)
        {
            dataSet dsObj = new dataSet();
            using (SqlConnection con = new SqlConnection(GlobalConfig.ConStr_Hospital))
            {
                using (SqlCommand cmd = new SqlCommand("pCPOE_MasterQueries", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandTimeout = 2500;
                    cmd.Parameters.Add("@TemplateType", SqlDbType.VarChar, 10).Value = objBO.TemplateType;
                    cmd.Parameters.Add("@DoctorId", SqlDbType.VarChar, 10).Value = objBO.DoctorId;
                    cmd.Parameters.Add("@TemplateId", SqlDbType.VarChar, 10).Value = objBO.TemplateId;
                    cmd.Parameters.Add("@ItemId", SqlDbType.VarChar, 10).Value = objBO.ItemId;
                    cmd.Parameters.Add("@ItemName", SqlDbType.VarChar, 100).Value = objBO.ItemName;
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
        public dataSet CPOE_OPTHMasterQueries(CPOETemplateItemsBO objBO)
        {
            dataSet dsObj = new dataSet();
            using (SqlConnection con = new SqlConnection(GlobalConfig.ConStr_Hospital))
            {
                using (SqlCommand cmd = new SqlCommand("pCPOE_OPTHMasterQueries", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandTimeout = 2500;
                    cmd.Parameters.Add("@TemplateType", SqlDbType.VarChar, 10).Value = objBO.TemplateType;
                    cmd.Parameters.Add("@DoctorId", SqlDbType.VarChar, 10).Value = objBO.DoctorId;
                    cmd.Parameters.Add("@TemplateId", SqlDbType.VarChar, 10).Value = objBO.TemplateId;
                    cmd.Parameters.Add("@ItemId", SqlDbType.VarChar, 10).Value = objBO.ItemId;
                    cmd.Parameters.Add("@ItemName", SqlDbType.VarChar, 100).Value = objBO.ItemName;
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
        public dataSet CPOE_OPTHQueries(CPOETemplateItemsBO objBO)
        {
            dataSet dsObj = new dataSet();
            using (SqlConnection con = new SqlConnection(GlobalConfig.ConStr_Hospital))
            {
                using (SqlCommand cmd = new SqlCommand("pCPOE_OPTHQueries", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandTimeout = 2500;
                    cmd.Parameters.Add("@app_no", SqlDbType.VarChar, 20).Value = objBO.app_no;
                    cmd.Parameters.Add("@TemplateType", SqlDbType.VarChar, 10).Value = objBO.TemplateType;
                    cmd.Parameters.Add("@DoctorId", SqlDbType.VarChar, 10).Value = objBO.DoctorId;
                    cmd.Parameters.Add("@TemplateId", SqlDbType.VarChar, 10).Value = objBO.TemplateId;
                    cmd.Parameters.Add("@ItemId", SqlDbType.VarChar, 10).Value = objBO.ItemId;
                    cmd.Parameters.Add("@ItemName", SqlDbType.VarChar, 100).Value = objBO.ItemName;
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
        public string CPOE_InsertUpdateMaster(CPOETemplateItemsBO objBO)
        {
            string processInfo = string.Empty;
            using (SqlConnection con = new SqlConnection(GlobalConfig.ConStr_Hospital))
            {
                using (SqlCommand cmd = new SqlCommand("pCPOE_InsertUpdateMaster", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandTimeout = 2500;
                    cmd.Parameters.Add("@TemplateType", SqlDbType.VarChar, 10).Value = objBO.TemplateType;
                    cmd.Parameters.Add("@DoctorId", SqlDbType.VarChar, 10).Value = objBO.DoctorId;
                    cmd.Parameters.Add("@TemplateId", SqlDbType.VarChar, 10).Value = objBO.TemplateId;
                    cmd.Parameters.Add("@GroupName", SqlDbType.VarChar, 100).Value = objBO.GroupName;
                    cmd.Parameters.Add("@ItemId", SqlDbType.VarChar, 10).Value = objBO.ItemId;
                    cmd.Parameters.Add("@ItemName", SqlDbType.VarChar, 100).Value = objBO.ItemName;
                    cmd.Parameters.Add("@login_id", SqlDbType.VarChar, 10).Value = objBO.login_id;
                    cmd.Parameters.Add("@Prm1", SqlDbType.VarChar, 100).Value = objBO.prm_1;
                    cmd.Parameters.Add("@Prm2", SqlDbType.VarChar, 100).Value = objBO.prm_2;
                    cmd.Parameters.Add("@IsFavourite", SqlDbType.Bit, 1).Value = objBO.IsFavourite;
                    cmd.Parameters.Add("@IsActive", SqlDbType.Bit, 1).Value = objBO.IsActive;
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
        public string CPOE_OPTHInsertUpdateMaster(OPTHTemplateItemsBO objBO)
        {
            string processInfo = string.Empty;
            using (SqlConnection con = new SqlConnection(GlobalConfig.ConStr_Hospital))
            {
                using (SqlCommand cmd = new SqlCommand("pCPOE_OPTHInsertUpdateMaster", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandTimeout = 2500;
                    cmd.Parameters.Add("@TemplateType", SqlDbType.VarChar, 10).Value = objBO.TemplateType;
                    cmd.Parameters.Add("@DoctorId", SqlDbType.VarChar, 10).Value = objBO.DoctorId;
                    cmd.Parameters.Add("@TemplateId", SqlDbType.VarChar, 10).Value = objBO.TemplateId;
                    cmd.Parameters.Add("@GroupType", SqlDbType.VarChar, 100).Value = objBO.GroupType;
                    cmd.Parameters.Add("@GroupName", SqlDbType.VarChar, 100).Value = objBO.GroupName;
                    cmd.Parameters.Add("@ItemId", SqlDbType.VarChar, 10).Value = objBO.ItemId;
                    cmd.Parameters.Add("@ItemName", SqlDbType.VarChar, 100).Value = objBO.ItemName;
                    cmd.Parameters.Add("@login_id", SqlDbType.VarChar, 10).Value = objBO.login_id;
                    cmd.Parameters.Add("@Prm1", SqlDbType.VarChar, 100).Value = objBO.prm_1;
                    cmd.Parameters.Add("@Prm2", SqlDbType.VarChar, 100).Value = objBO.prm_2;
                    cmd.Parameters.Add("@IsActive", SqlDbType.Bit, 1).Value = objBO.IsActive;
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