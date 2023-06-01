using HISWebApi.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace HISWebApi.Repository.Masters
{
    public class ReferralMaster
    {
        public dataSet mReferralQueries(ReferralMasterBO objBO)
        {
            dataSet dsObj = new dataSet();
            using (SqlConnection con = new SqlConnection(GlobalConfig.ConStr_Hospital))
            {
                using (SqlCommand cmd = new SqlCommand("pReferralQueries", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandTimeout = 2500;
                    cmd.Parameters.Add("@HospId", SqlDbType.VarChar, 15).Value = objBO.HospId;
                    cmd.Parameters.Add("@RefCode", SqlDbType.VarChar, 20).Value = objBO.RefCode == "" ? null : objBO.RefCode;
                    cmd.Parameters.Add("@EmpName", SqlDbType.VarChar, 50).Value = objBO.EmpName;
                    cmd.Parameters.Add("@Prm1", SqlDbType.VarChar, 50).Value = objBO.prm_1;
                    cmd.Parameters.Add("@Prm2", SqlDbType.VarChar, 50).Value = objBO.prm_2;
                    cmd.Parameters.Add("@Logic", SqlDbType.VarChar, 50).Value = objBO.Logic;
                    cmd.Parameters.Add("@login_id", SqlDbType.VarChar, 15).Value = objBO.login_id;
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

        public string mReferralInsertUpdate(ReferralMasterBO objBO)
        {
            dataSet dsObj = new dataSet();
            string processInfo = string.Empty;            
            using(SqlConnection con = new SqlConnection(GlobalConfig.ConStr_Hospital))
            {
                using (SqlCommand cmd = new SqlCommand("pReferralInsertUpdate", con))
                { 
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandTimeout = 2500;
                    cmd.Parameters.Add("@business_type", SqlDbType.VarChar, 30).Value = objBO.BusinessType;
                    cmd.Parameters.Add("@ref_code", SqlDbType.VarChar, 20).Value = objBO.RefCode;
                    cmd.Parameters.Add("@ref_name", SqlDbType.VarChar, 100).Value = objBO.Ref_Name;
                    cmd.Parameters.Add("@degree", SqlDbType.VarChar, 100).Value = objBO.Degree;
                    cmd.Parameters.Add("@speciality", SqlDbType.VarChar, 50).Value = objBO.Speciality;
                    cmd.Parameters.Add("@d_o_b", SqlDbType.VarChar, 15).Value = objBO.Dob == "1900/01/01" ? null : objBO.Dob;
                    cmd.Parameters.Add("@c_name", SqlDbType.VarChar, 100).Value = objBO.Clinicname;
                    cmd.Parameters.Add("@c_address", SqlDbType.VarChar, 300).Value = objBO.Clinicaddress;
                    cmd.Parameters.Add("@c_locality", SqlDbType.VarChar, 50).Value = objBO.Cliniclocality;
                    cmd.Parameters.Add("@state", SqlDbType.VarChar, 50).Value = objBO.State;
                    cmd.Parameters.Add("@c_city", SqlDbType.VarChar, 50).Value = objBO.City;
                    cmd.Parameters.Add("@Prm1", SqlDbType.VarChar, 500).Value = objBO.prm_1;
                    cmd.Parameters.Add("@c_phone", SqlDbType.VarChar, 20).Value = objBO.Phone;
                    cmd.Parameters.Add("@mobile_no", SqlDbType.VarChar, 20).Value = objBO.Mobile_no;
                    cmd.Parameters.Add("@email", SqlDbType.VarChar, 100).Value = objBO.Email;
                    cmd.Parameters.Add("@ProCode", SqlDbType.VarChar, 20).Value = objBO.EmpCode;
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