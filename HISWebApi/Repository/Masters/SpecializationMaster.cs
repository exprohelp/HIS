using System.Data;
using System.Data.SqlClient;
using HISWebApi.Models;

namespace HISWebApi.Repository.Masters
{
    public class SpecializationMaster
    {
        public string InsertUpdateSpecialization(SpecializationBO obj)
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
                    cmd.Parameters.Add("@prm_1", SqlDbType.VarChar, 100).Value = obj.SpecName;
                    cmd.Parameters.Add("@prm_2", SqlDbType.VarChar, 500).Value = obj.SpecDesc;                    
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