using HISWebApi.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace HISWebApi.Repository.WebPortal
{
    public class Doctors
    {
        public dataSet Web_DoctorQueries(ipDoctors obj)
        {
            dataSet dsObj = new dataSet();
            using (SqlConnection con = new SqlConnection(GlobalConfig.ConStr_Hospital))
            {
                using (SqlCommand cmd = new SqlCommand("pWeb_DoctorInfoQueries",con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandTimeout = 2500;                   
                    cmd.Parameters.AddWithValue("@HospId", obj.hosp_id);
                    cmd.Parameters.AddWithValue("@DeptId", obj.DeptId);
                    cmd.Parameters.AddWithValue("@DoctorId ",obj.DoctorId);                
                    cmd.Parameters.AddWithValue("@Prm1", obj.Prm1);
                    cmd.Parameters.AddWithValue("@prm2", obj.prm2);
                    cmd.Parameters.AddWithValue("@login_id", obj.login_id);
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
        public dataSet Web_DepartmentQueries(ipDoctors obj)
        {
            dataSet dsObj = new dataSet();
            using (SqlConnection con = new SqlConnection(GlobalConfig.ConStr_Hospital))
            {
                using (SqlCommand cmd = new SqlCommand("pWeb_DepartmentQueries", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandTimeout = 2500;
                    cmd.Parameters.AddWithValue("@HospId", obj.hosp_id);
                    cmd.Parameters.AddWithValue("@DeptId", obj.DeptId);
                    cmd.Parameters.AddWithValue("@DoctorId ", obj.DoctorId);
                    cmd.Parameters.AddWithValue("@Prm1", obj.Prm1);
                    cmd.Parameters.AddWithValue("@prm2", obj.prm2);
                    cmd.Parameters.AddWithValue("@login_id", obj.login_id);
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
        public string InsertDepartmentFeatures(ipDoctors objBO)
        {
            string processInfo = string.Empty;
            using (SqlConnection con = new SqlConnection(GlobalConfig.ConStr_Hospital))
            {
                using (SqlCommand cmd = new SqlCommand("pWeb_InsertUpdateDepartmentMaster", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandTimeout = 2500;
                    cmd.Parameters.Add("@srno", SqlDbType.BigInt).Value = objBO.srno;
                    cmd.Parameters.Add("@autoid", SqlDbType.BigInt).Value = objBO.AutoId;
                    cmd.Parameters.Add("@Prm1", SqlDbType.VarChar, 500).Value = objBO.Prm1;
                    cmd.Parameters.Add("@DeptId", SqlDbType.VarChar, 25).Value = objBO.DeptId;
                    cmd.Parameters.Add("@DeptName", SqlDbType.VarChar, 255).Value = objBO.DeptName;
                    cmd.Parameters.Add("@DeptShortName", SqlDbType.VarChar, 200).Value = objBO.DeptShortName;
                    cmd.Parameters.Add("@TagName", SqlDbType.VarChar, 50).Value = objBO.TagName;
                    cmd.Parameters.Add("@Description", SqlDbType.VarChar).Value = objBO.Description;
                    cmd.Parameters.Add("@MediaLink", SqlDbType.VarChar, 500).Value = objBO.MediaLink;
                    cmd.Parameters.Add("@PhysicalPath", SqlDbType.VarChar, 300).Value = objBO.PhyPath;
                    cmd.Parameters.Add("@ImgPath", SqlDbType.VarChar, 300).Value = objBO.ImgPath;
                    cmd.Parameters.Add("@ImgPostion", SqlDbType.VarChar, 10).Value = objBO.ImgPostion;
                    cmd.Parameters.Add("@MediaType", SqlDbType.VarChar, 25).Value = objBO.MediaType;
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
                }
                return processInfo;
            }
        }
        public string InsertUpdateViedoLibrary(ViedoLibraryInfo objBO)
        {
            string processInfo = string.Empty;
            using (SqlConnection con = new SqlConnection(GlobalConfig.ConStr_Hospital))
            {
                using (SqlCommand cmd = new SqlCommand("pweb_InsertUpdateviedoLibrary", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandTimeout = 2500;
                    cmd.Parameters.Add("@AutoId", SqlDbType.BigInt).Value = objBO.AutoId;
                    cmd.Parameters.Add("@Seq_no", SqlDbType.BigInt).Value = objBO.seq_no;
                    cmd.Parameters.Add("@Title", SqlDbType.VarChar, 200).Value = objBO.title;
                    cmd.Parameters.Add("@Link", SqlDbType.VarChar, 100).Value = objBO.link;
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
                        processInfo = "Error Found : " + sqlEx.Message;
                    }
                    finally { con.Close(); }
                }
                return processInfo;
            }
        }
        public dataSet ViedoLibraryQueries(ViedoLibraryInfo objBO)
        {
            dataSet dsObj = new dataSet();

            using (SqlConnection con = new SqlConnection(GlobalConfig.ConStr_Hospital))
            {
                using (SqlCommand cmd = new SqlCommand("pweb_ViedoLibraryQueries", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandTimeout = 2500;
                    cmd.Parameters.Add("@AutoId", SqlDbType.BigInt).Value = objBO.AutoId;
                    cmd.Parameters.Add("@seq_no", SqlDbType.BigInt).Value = objBO.seq_no;
                    cmd.Parameters.Add("@title", SqlDbType.VarChar, 100).Value = objBO.title;
                    cmd.Parameters.Add("@link", SqlDbType.VarChar, 100).Value = objBO.link;
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
    }

}