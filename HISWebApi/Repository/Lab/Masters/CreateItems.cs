using HISWebApi.Models;
using System.Data;
using System.Data.SqlClient;

namespace HISWebApi.Repository.Lab.Masters
{
    public class CreateItems
    {
        public dataSet LabItemQueries(CreateItem objBO)
        {
            dataSet dsObj = new dataSet();
            using (SqlConnection con = new SqlConnection(GlobalConfig.ConStr_Hospital))
            {
                using (SqlCommand cmd = new SqlCommand("pm_LabItemQueries", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandTimeout = 2500;
                    cmd.Parameters.Add("@autoid", SqlDbType.Int).Value = objBO.autoid;
                    cmd.Parameters.Add("@hosp_id", SqlDbType.VarChar, 10).Value = objBO.hosp_id;
                    cmd.Parameters.Add("@CatId", SqlDbType.VarChar, 20).Value = objBO.catid;
                    cmd.Parameters.Add("@SubCatId", SqlDbType.VarChar, 20).Value = objBO.subcatid;
                    cmd.Parameters.Add("@ItemId", SqlDbType.VarChar, 20).Value = objBO.itemid;                    
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

        public string LabItemInsertUpdate(CreateItem objBO)
        {
            dataSet dsObj = new dataSet();
            string processInfo = string.Empty;
            using (SqlConnection con = new SqlConnection(GlobalConfig.ConStr_Hospital))
            {
                using (SqlCommand cmd = new SqlCommand("pm_LabItemInsertUpdate", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandTimeout = 2500;
                    cmd.Parameters.Add("@autoid", SqlDbType.VarChar, 20).Value = objBO.autoid;
                    cmd.Parameters.Add("@subcatid", SqlDbType.VarChar, 20).Value = objBO.subcatid;
                    cmd.Parameters.Add("@itemtype", SqlDbType.VarChar, 35).Value = objBO.itemtype;
                    cmd.Parameters.Add("@cptcode", SqlDbType.VarChar, 50).Value = objBO.cptcodes;
                    cmd.Parameters.Add("@itemname", SqlDbType.VarChar, 100).Value = objBO.itemname;
                    cmd.Parameters.Add("@isshareable", SqlDbType.VarChar, 1).Value = objBO.isshareble;
                    cmd.Parameters.Add("@isdiscountable", SqlDbType.VarChar, 1).Value = objBO.isdiscountable;                    
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