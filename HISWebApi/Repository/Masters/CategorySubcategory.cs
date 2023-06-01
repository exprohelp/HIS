using HISWebApi.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace HISWebApi.Repository.Masters
{
    public class CategorySubcategory
    {
        public dataSet mCategorySubCategoryQueries(CategorySubCategoryBO objBO)
        {
            dataSet dsObj = new dataSet();
            using (SqlConnection con = new SqlConnection(GlobalConfig.ConStr_Hospital))
            {
                using (SqlCommand cmd = new SqlCommand("pm_CatSubcatMasterQueries", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandTimeout = 2500;
                    cmd.Parameters.Add("@autoid", SqlDbType.Int).Value = objBO.autoid;
                    cmd.Parameters.Add("@hosp_id", SqlDbType.VarChar, 10).Value = objBO.hosp_id;
                    cmd.Parameters.Add("@CatId", SqlDbType.VarChar, 20).Value = objBO.catid;
                    cmd.Parameters.Add("@SubCatId", SqlDbType.VarChar, 20).Value = objBO.subcatid;                    
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
        
        public string CategorySubCategoryInsertUpdate(CategorySubCategoryBO objBO)
        {
            dataSet dsObj = new dataSet();
            string processInfo = string.Empty;
            using (SqlConnection con = new SqlConnection(GlobalConfig.ConStr_Hospital))
            {
                using (SqlCommand cmd = new SqlCommand("pm_CatSubcatInsertUpdate", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandTimeout = 2500;
                    cmd.Parameters.Add("@autoid", SqlDbType.VarChar, 15).Value = objBO.autoid;
                    cmd.Parameters.Add("@hosp_id", SqlDbType.VarChar, 15).Value = objBO.hosp_id;
                    cmd.Parameters.Add("@CatId", SqlDbType.VarChar, 15).Value = objBO.catid;
                    cmd.Parameters.Add("@CatName", SqlDbType.VarChar, 100).Value = objBO.catname;
                    cmd.Parameters.Add("@CatDesc", SqlDbType.VarChar, 500).Value = objBO.catdesc;
                    cmd.Parameters.Add("@CatAbbr", SqlDbType.VarChar, 10).Value = objBO.catabbr;
                    cmd.Parameters.Add("@SubCatId", SqlDbType.VarChar, 20).Value = objBO.subcatid;
                    cmd.Parameters.Add("@SubCatName", SqlDbType.VarChar, 200).Value = objBO.subcatname;
                    cmd.Parameters.Add("@SubCatDesc", SqlDbType.VarChar, 500).Value = objBO.subcatdesc;
                    cmd.Parameters.Add("@SubCatAbbr", SqlDbType.VarChar, 10).Value = objBO.subcatabbr;                    
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