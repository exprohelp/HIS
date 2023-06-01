using HISWebApi.Models;

using System.Data;
using System.Data.SqlClient;

namespace HISWebApi.Repository.Asset
{
    public class Asset
    {
        public dataSet Hosp_AssetMovementQueries(ipAsset ip)
        {
            dataSet dsObj = new dataSet();
            using (SqlConnection con = new SqlConnection(GlobalConfig.ConStr_Assets))
            {
                using (SqlCommand cmd = new SqlCommand("pHosp_AssetMovementQueries", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandTimeout = 2500;
                    cmd.Parameters.Add("@unit_id", SqlDbType.VarChar, 10).Value = ip.unit_id;
                    cmd.Parameters.Add("@eq_no", SqlDbType.VarChar, 16).Value = ip.eq_no;
                    cmd.Parameters.Add("@DeptId", SqlDbType.VarChar, 16).Value = ip.DeptId;
                    cmd.Parameters.Add("@from", SqlDbType.VarChar).Value = ip.from;
                    cmd.Parameters.Add("@to", SqlDbType.VarChar).Value = ip.to;
                    cmd.Parameters.Add("@prm_1", SqlDbType.VarChar, 16).Value = ip.prm_1;
                    cmd.Parameters.Add("@login_id", SqlDbType.VarChar, 16).Value = ip.login_id;
                    cmd.Parameters.Add("@Logic", SqlDbType.VarChar, 50).Value = ip.Logic;
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
        public string Hospital_AssetMovement(ipAssetMovement ip)
        {
            string processInfo = string.Empty;
            using (SqlConnection con = new SqlConnection(GlobalConfig.ConStr_Assets))
            {
                con.Open();
                SqlTransaction tran = con.BeginTransaction();
                using (SqlCommand cmd = new SqlCommand("pHospital_AssetMovement", con))
                {
                    cmd.Transaction = tran;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandTimeout = 2500;
                    cmd.Parameters.Add("@unit_id", SqlDbType.VarChar, 10).Value = ip.unit_id;
                    cmd.Parameters.Add("@eq_no", SqlDbType.VarChar, 16).Value = ip.eq_no;
                    cmd.Parameters.Add("@SectionCode", SqlDbType.VarChar, 10).Value = ip.SectionCode;
                    cmd.Parameters.Add("@trf_remark", SqlDbType.VarChar, 200).Value = ip.Remark;
                    cmd.Parameters.Add("@login_id", SqlDbType.VarChar, 10).Value = ip.login_id;
                    cmd.Parameters.Add("@Logic", SqlDbType.VarChar, 20).Value = ip.Logic;
                    cmd.Parameters.Add("@result", SqlDbType.VarChar, 200).Value = "";
                    cmd.Parameters["@result"].Direction = ParameterDirection.InputOutput;
                    try
                    {
                        cmd.ExecuteNonQuery();
                        processInfo = (string)cmd.Parameters["@result"].Value.ToString();
                        if (processInfo.Contains("Success"))
                        {
                            tran.Commit();
                        }
                        else
                        {
                            tran.Rollback();
                        }
                    }
                    catch (SqlException sqlEx)
                    {
                        tran.Rollback();
                        processInfo = "Error Found   : " + sqlEx.Message;
                    }
                    finally { con.Close(); }
                    return processInfo;
                }
            }
        }
        public dataSet AssetsQueries(AssetsBO objBO)
        {
            dataSet dsObj = new dataSet();
            using (SqlConnection con = new SqlConnection(GlobalConfig.ConStr_Assets))
            {
                using (SqlCommand cmd = new SqlCommand("pHosp_AssetsQueries", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandTimeout = 2500;
                    cmd.Parameters.Add("@unit_id", SqlDbType.VarChar, 10).Value = objBO.unit_id;
                    cmd.Parameters.Add("@DeptCode", SqlDbType.VarChar, 10).Value = objBO.DeptCode;
                    cmd.Parameters.Add("@DeptName", SqlDbType.VarChar, 100).Value = objBO.DeptName;
                    cmd.Parameters.Add("@DeptHead", SqlDbType.VarChar, 10).Value = objBO.DeptHead;
                    cmd.Parameters.Add("@SectionCode", SqlDbType.VarChar, 10).Value = objBO.SectionCode;
                    cmd.Parameters.Add("@SectionName", SqlDbType.VarChar, 100).Value = objBO.SectionName;
                    cmd.Parameters.Add("@EqNo", SqlDbType.VarChar, 100).Value = objBO.eq_no;
                    cmd.Parameters.Add("@ComplCode", SqlDbType.VarChar, 100).Value = objBO.ComplCode;
                    cmd.Parameters.Add("@login_id", SqlDbType.VarChar, 20).Value = objBO.login_id;
                    cmd.Parameters.Add("@Logic", SqlDbType.VarChar, 100).Value = objBO.Logic;
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
        public dataSet HospitalComplaints(AssetsBO objBO)
        {
            dataSet dsObj = new dataSet();
            using (SqlConnection con = new SqlConnection(GlobalConfig.ConStr_Assets))
            {
                using (SqlCommand cmd = new SqlCommand("pHospital_Complaints", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandTimeout = 2500;
                    cmd.Parameters.Add("@eqNo", SqlDbType.VarChar, 20).Value = objBO.eqNo;
                    cmd.Parameters.Add("@compl_code", SqlDbType.VarChar, 20).Value = objBO.ComplCode;
                    cmd.Parameters.Add("@prm_1", SqlDbType.VarChar, 50).Value = objBO.prm_1;
                    cmd.Parameters.Add("@from", SqlDbType.DateTime).Value = objBO.from;
                    cmd.Parameters.Add("@to", SqlDbType.DateTime).Value = objBO.to;
                    cmd.Parameters.Add("@login_id", SqlDbType.VarChar, 10).Value = objBO.login_id;
                    cmd.Parameters.Add("@Logic", SqlDbType.VarChar, 100).Value = objBO.Logic;
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
        public string InsertModifyAssets(AssetsBO objBO)
        {
            string processInfo = string.Empty;
            using (SqlConnection con = new SqlConnection(GlobalConfig.ConStr_Assets))
            {
                con.Open();
                SqlTransaction tran = con.BeginTransaction();
                using (SqlCommand cmd = new SqlCommand("pHosp_InsertModifyAssets", con))
                {
                    cmd.Transaction = tran;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandTimeout = 2500;
                    cmd.Parameters.Add("@unit_id", SqlDbType.VarChar, 10).Value = objBO.unit_id;
                    cmd.Parameters.Add("@DeptCode", SqlDbType.VarChar, 10).Value = objBO.DeptCode;
                    cmd.Parameters.Add("@DeptName", SqlDbType.VarChar, 100).Value = objBO.DeptName;
                    cmd.Parameters.Add("@DeptHead", SqlDbType.VarChar, 10).Value = objBO.DeptHead;
                    cmd.Parameters.Add("@SectionCode", SqlDbType.VarChar, 10).Value = objBO.SectionCode;
                    cmd.Parameters.Add("@SectionName", SqlDbType.VarChar, 100).Value = objBO.SectionName;
                    cmd.Parameters.Add("@login_id", SqlDbType.VarChar, 20).Value = objBO.login_id;
                    cmd.Parameters.Add("@Logic", SqlDbType.VarChar, 100).Value = objBO.Logic;
                    cmd.Parameters.Add("@result", SqlDbType.VarChar, 500).Value = "";
                    cmd.Parameters["@result"].Direction = ParameterDirection.InputOutput;
                    try
                    {
                        int i = cmd.ExecuteNonQuery();
                        processInfo = (string)cmd.Parameters["@result"].Value.ToString();
                        if (processInfo.Contains("Success"))
                        {
                            tran.Commit();
                        }
                        else
                        {
                            tran.Rollback();
                        }
                    }
                    catch (SqlException sqlEx)
                    {
                        tran.Rollback();
                        processInfo = "Error Found   : " + sqlEx.Message;
                    }
                    finally { con.Close(); }
                    return processInfo;
                }
            }
        }
        public string insertIntoComplaints(AssetsComplaintsBO objBO)
        {
            string processInfo = string.Empty;
            using (SqlConnection con = new SqlConnection(GlobalConfig.ConStr_Assets))
            {
                using (SqlCommand cmd = new SqlCommand("pInsEquipComplaints", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandTimeout = 2500;
                    cmd.Parameters.Add("@emp_code", SqlDbType.VarChar, 10).Value = objBO.EmpCode;
                    cmd.Parameters.Add("@unit_id", SqlDbType.VarChar, 10).Value = objBO.UnitId;
                    cmd.Parameters.Add("@eq_no", SqlDbType.VarChar, 16).Value = objBO.EqNo;
                    cmd.Parameters.Add("@Complaint", SqlDbType.VarChar, 200).Value = objBO.Complaint;
                    cmd.Parameters.Add("@status_ok", SqlDbType.VarChar, 1).Value = "N";
                    cmd.Parameters.Add("@Client_status", SqlDbType.VarChar, 15).Value = "Pending";
                    cmd.Parameters.Add("@result", SqlDbType.VarChar, 100).Value = "outPut";
                    cmd.Parameters["@result"].Direction = ParameterDirection.Output;
                    try
                    {
                        con.Open();
                        cmd.ExecuteNonQuery();
                        processInfo = (string)cmd.Parameters["@result"].Value.ToString();
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

        public string insertComplaintCommunication(AssetsComplaintsBO objBO)
        {
            string processInfo = string.Empty;
            using (SqlConnection con = new SqlConnection(GlobalConfig.ConStr_Assets))
            {
                using (SqlCommand cmd = new SqlCommand("pInsIntoCommunication", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandTimeout = 2500;
                    cmd.Parameters.Add("@complCode", SqlDbType.VarChar, 10).Value = objBO.ComplCode;
                    cmd.Parameters.Add("@loginId", SqlDbType.VarChar, 10).Value =objBO.login_id;
                    cmd.Parameters.Add("@remarks", SqlDbType.VarChar, 200).Value = objBO.Remarks;
                    try {
                        con.Open();
                        cmd.ExecuteNonQuery();
                    }
                    catch (SqlException sqlEx) { }
                    finally { con.Close(); }
                    return "Successfully Attached";
                }
            }
        }
        public string closeComplaint(AssetsComplaintsBO objBO)
        {
            string processInfo = string.Empty;
            using (SqlConnection con = new SqlConnection(GlobalConfig.ConStr_Assets))
            {
                using (SqlCommand cmd = new SqlCommand("pCloseComplaint", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandTimeout = 2500;
                    cmd.Parameters.Add("@unitId", SqlDbType.VarChar, 10).Value = objBO.UnitId;
                    cmd.Parameters.Add("@eq_No", SqlDbType.VarChar, 16).Value = objBO.EqNo;
                    try
                    {
                        con.Open();
                        cmd.ExecuteNonQuery();
                    }
                    catch (SqlException sqlEx) { }
                    finally { con.Close(); }
                    return "Successfully Attached";
                }
            }
        }

    }
}