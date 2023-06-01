using HISWebApi.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace HISWebApi.Repository.Lab.Masters
{
    public class LabDoctorSignature
    {
        public dataSet LabDoctorSignatureQueries(LabDrSignature objBO)
        {
            dataSet dsObj = new dataSet();
            using (SqlConnection con = new SqlConnection(GlobalConfig.ConStr_Hospital))
            {
                using (SqlCommand cmd = new SqlCommand("pm_LabDoctorSignatureQueries", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandTimeout = 2500;
                    cmd.Parameters.Add("@signid", SqlDbType.VarChar, 10).Value = objBO.signid;
                    cmd.Parameters.Add("@doctorid", SqlDbType.VarChar, 20).Value = objBO.doctorid;
                    cmd.Parameters.Add("@deptid", SqlDbType.VarChar, 20).Value = objBO.deptid;
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
        public string DoctorSignatireInsertUpdate(LabDrSignature objBO)
        {
            dataSet dsObj = new dataSet();
            string processInfo = string.Empty;
            using (SqlConnection con = new SqlConnection(GlobalConfig.ConStr_Hospital))
            {
                using (SqlCommand cmd = new SqlCommand("pm_LabDoctorSignatureInsertUpdate", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandTimeout = 2500;
                    cmd.Parameters.Add("@SignId", SqlDbType.VarChar, 20).Value = objBO.signid;
                    cmd.Parameters.Add("@doctorid", SqlDbType.VarChar, 20).Value = objBO.doctorid;
                    cmd.Parameters.Add("@doctorname", SqlDbType.VarChar, 100).Value = objBO.doctorname;
                    cmd.Parameters.Add("@degree", SqlDbType.VarChar,300).Value = objBO.degree;
                    cmd.Parameters.Add("@signvirpath", SqlDbType.VarChar,255).Value = objBO.signvirtualpath;
                    cmd.Parameters.Add("@signphypath", SqlDbType.VarChar,255).Value = objBO.signphysicalpath;
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