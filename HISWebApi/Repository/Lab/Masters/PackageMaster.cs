using HISWebApi.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace HISWebApi.Repository.Lab.Masters
{
    public class PackageMaster
    {
        public dataSet PackageMasterQueries(LabPackage objBO)
        {
            dataSet dsObj = new dataSet();
            using (SqlConnection con = new SqlConnection(GlobalConfig.ConStr_Hospital))
            {
                using (SqlCommand cmd = new SqlCommand("pm_PackageQueries", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandTimeout = 2500;
                    cmd.Parameters.Add("@hosp_id", SqlDbType.VarChar, 10).Value = objBO.hosp_id;
                    cmd.Parameters.Add("@CatId", SqlDbType.VarChar, 20).Value = objBO.catid;
                    cmd.Parameters.Add("@SubCatId", SqlDbType.VarChar, 20).Value = objBO.subcateid;
                    cmd.Parameters.Add("@PackageId", SqlDbType.VarChar, 20).Value = objBO.packageId;
                    cmd.Parameters.Add("@itemId", SqlDbType.VarChar, 20).Value = objBO.itemid;                    
                    cmd.Parameters.Add("@prm_1", SqlDbType.VarChar, 100).Value = objBO.prm_1;
                    cmd.Parameters.Add("@prm_2", SqlDbType.VarChar, 100).Value = objBO.prm_2;
                    cmd.Parameters.Add("@Logic", SqlDbType.VarChar, 50).Value = objBO.Logic;
                    cmd.Parameters.Add("@login_id", SqlDbType.VarChar, 10).Value = objBO.login_id;
                    cmd.Parameters.Add("@warehouseCartId", SqlDbType.VarChar, 7).Value = objBO.warehouseCartId;
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

        public string PackageInsertUpdate(LabPackage objBO)
        {
            dataSet dsObj = new dataSet();
            string processInfo = string.Empty;
            using (SqlConnection con = new SqlConnection(GlobalConfig.ConStr_Hospital))
            {               
                using (SqlCommand cmd = new SqlCommand("pm_PackageInsertUpdate", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandTimeout = 2500;
                    cmd.Parameters.Add("@CatId", SqlDbType.VarChar, 10).Value = objBO.catid;
                    cmd.Parameters.Add("@SubCatId", SqlDbType.VarChar, 10).Value = objBO.subcateid;
                    cmd.Parameters.Add("@hosp_id", SqlDbType.VarChar, 20).Value = objBO.hosp_id;
                    cmd.Parameters.Add("@PackageType", SqlDbType.VarChar, 20).Value = objBO.packagetype;
                    cmd.Parameters.Add("@PackageName", SqlDbType.VarChar, 150).Value = objBO.packagename;
                    cmd.Parameters.Add("@PackageId", SqlDbType.VarChar, 150).Value = objBO.packageId;
                    cmd.Parameters.Add("@bookFor", SqlDbType.VarChar, 10).Value = objBO.bookFor;
                    cmd.Parameters.Add("@Description", SqlDbType.VarChar, 10).Value = objBO.description;
                    cmd.Parameters.Add("@login_id", SqlDbType.VarChar, 10).Value = objBO.login_id;
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

        public string PackageLinkItems(LabPackage objBO)
        {
            dataSet dsObj = new dataSet();
            string processInfo = string.Empty;
            using (SqlConnection con = new SqlConnection(GlobalConfig.ConStr_Hospital))
            {
                using (SqlCommand cmd = new SqlCommand("pm_PackageLinkItems", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandTimeout = 2500;
                    cmd.Parameters.Add("@hosp_id", SqlDbType.VarChar, 20).Value = objBO.hosp_id;
                    cmd.Parameters.Add("@type", SqlDbType.VarChar, 20).Value = objBO.type;
                    cmd.Parameters.Add("@packageId", SqlDbType.VarChar, 20).Value = objBO.packageId;                    
                    cmd.Parameters.Add("@itemId", SqlDbType.VarChar, 20).Value = objBO.itemid;
                    cmd.Parameters.Add("@innerpackageId", SqlDbType.VarChar, 20).Value = objBO.innerpackageid;                    
                    cmd.Parameters.Add("@packageInvesttype", SqlDbType.VarChar, 20).Value = objBO.packageInvesttype;
                    cmd.Parameters.Add("@login_id", SqlDbType.VarChar, 10).Value = objBO.login_id;                   
                    cmd.Parameters.Add("@result", SqlDbType.VarChar, 50).Value = "";
                    cmd.Parameters["@result"].Direction = ParameterDirection.InputOutput;
                    cmd.Parameters.Add("@Logic", SqlDbType.VarChar, 50).Value = objBO.Logic;
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

        public string DeletePackageLinkItems(LabPackage objBO)
        {
            dataSet dsObj = new dataSet();
            string processInfo = string.Empty;
            using (SqlConnection con = new SqlConnection(GlobalConfig.ConStr_Hospital))
            {
                using (SqlCommand cmd = new SqlCommand("pm_PackageLinkItems", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandTimeout = 2500;
                    cmd.Parameters.Add("@hosp_id", SqlDbType.VarChar, 20).Value = objBO.hosp_id;
                    cmd.Parameters.Add("@type", SqlDbType.VarChar, 20).Value = objBO.type;                    
                    cmd.Parameters.Add("@packageId", SqlDbType.VarChar, 20).Value = objBO.packageId;
                    cmd.Parameters.Add("@itemId", SqlDbType.VarChar, 20).Value = objBO.itemid;
                    cmd.Parameters.Add("@innerpackageId", SqlDbType.VarChar, 20).Value = objBO.innerpackageid;                    
                    cmd.Parameters.Add("@packageInvesttype", SqlDbType.VarChar, 20).Value = objBO.packageInvesttype;
                    cmd.Parameters.Add("@login_id", SqlDbType.VarChar, 10).Value = objBO.login_id;
                    cmd.Parameters.Add("@result", SqlDbType.VarChar, 50).Value = "";
                    cmd.Parameters["@result"].Direction = ParameterDirection.InputOutput;
                    cmd.Parameters.Add("@Logic", SqlDbType.VarChar, 50).Value = objBO.Logic;
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

        //
    }
}