using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using HISWebApi.Models;

namespace HISWebApi.Repository.Masters
{
    public class DepartmentMaster
    {
        public string SaveUpdateNotification(NotificationBO obj)
        {
            dataSet dsObj = new dataSet();
            string processInfo = string.Empty;
            using (SqlConnection con = new SqlConnection(GlobalConfig.ConStr_MobileAppDb))
            {
                using (SqlCommand cmd = new SqlCommand("pMob_Insert_Modify_NotificationMaster", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandTimeout = 2500;
                    cmd.Parameters.Add("@autoid", SqlDbType.Int).Value = obj.AutoId;
                    cmd.Parameters.Add("@Title", SqlDbType.VarChar).Value = obj.Title;
                    cmd.Parameters.Add("@Description", SqlDbType.VarChar).Value = obj.Description;
                    cmd.Parameters.Add("@login_id", SqlDbType.VarChar, 10).Value = obj.login_id;
                    cmd.Parameters.Add("@CurNotification", SqlDbType.Int).Value = obj.CurNotification;
                    cmd.Parameters.Add("@isActive", SqlDbType.Bit).Value = obj.IsActive;
                    cmd.Parameters.Add("@ValidFrom", SqlDbType.VarChar).Value = obj.ValidFrom;
                    cmd.Parameters.Add("@ValidTo", SqlDbType.VarChar).Value = obj.ValidTo;
                    cmd.Parameters.Add("@ImagePath", SqlDbType.VarChar).Value = obj.ImagePath;
                    cmd.Parameters.Add("@Logic", SqlDbType.VarChar).Value = obj.Logic;
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
        public string mInsertUpdateDepartment(DepartmentBO obj)
        {
            dataSet dsObj = new dataSet();
            string processInfo = string.Empty;
            using (SqlConnection con = new SqlConnection(GlobalConfig.ConStr_Hospital))
            {
                using (SqlCommand cmd = new SqlCommand("pm_tinymaster", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandTimeout = 2500;
                    cmd.Parameters.Add("@hospid", SqlDbType.VarChar, 16).Value = obj.hosp_id;
                    cmd.Parameters.Add("@prm_1", SqlDbType.VarChar, 100).Value = obj.DeptName;
                    cmd.Parameters.Add("@prm_2", SqlDbType.VarChar, 500).Value = obj.DeptDesc;
                    cmd.Parameters.Add("@loginid", SqlDbType.VarChar, 15).Value = obj.login_id;
                    cmd.Parameters.Add("@tagname", SqlDbType.VarChar, 50).Value = obj.Tagname;
                    cmd.Parameters.Add("@logic", SqlDbType.VarChar, 15).Value = obj.Logic;
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