using HISWebApi.Models;
using HISWebApi.Repository.IPD;
using System;
using System.Data;
using System.Data.SqlClient;

namespace HISWebApi.Repository.Pharmacy
{
    public class Pharmacy
    {
        private HISDBLayer hisDB = new HISDBLayer();
        
        public dataSet HOTO_Queries(Hoto p)
        {
            string processInfo = string.Empty;
            dataSet dsObj = new dataSet();
            SqlConnection con = new SqlConnection(GlobalConfig.ConStr_ChandanPharmacyLive);
            SqlCommand cmd = new SqlCommand("pHOTO_Queries", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandTimeout = 2500;
            cmd.Parameters.Add("@unit_id", SqlDbType.VarChar, 10).Value = p.unit_id;
            cmd.Parameters.Add("@loginId", SqlDbType.VarChar, 15).Value = p.login_id;
            cmd.Parameters.Add("@Logic", SqlDbType.VarChar, 50).Value = p.logic;
            cmd.Parameters.Add("@shiftID", SqlDbType.VarChar, 20).Value = p.shiftID;
            cmd.Parameters.Add("@prm_1", SqlDbType.VarChar, 50).Value = p.prm_1;
            cmd.Parameters.Add("@prm_2", SqlDbType.VarChar, 50).Value = p.prm_2;
            cmd.Parameters.Add("@result", SqlDbType.VarChar, 200).Value = "N/A";
            cmd.Parameters["@result"].Direction = ParameterDirection.InputOutput;
            try
            {
                con.Open();
                DataSet ds = new DataSet();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                dsObj.ResultSet = ds;
                dsObj.Msg = (string)cmd.Parameters["@result"].Value.ToString();
                con.Close();           
            }
            catch (SqlException sqlEx)
            {
                dsObj.Msg = sqlEx.ToString();
            }
            finally { con.Close(); }
            return dsObj;
        }
        public string Insert_Modify_AC_Collection_Info(Hoto p)
        {
            string processInfo = string.Empty;
            DataSet ds = new DataSet();
            SqlConnection con = new SqlConnection(GlobalConfig.ConStr_ChandanPharmacyLive);
            SqlCommand cmd = new SqlCommand("Insert_Modify_AC_Collection_Info", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandTimeout = 2500;
            cmd.Parameters.Add("@autoId", SqlDbType.NVarChar, 20).Value = p.autoId;
            cmd.Parameters.Add("@unit_id", SqlDbType.NVarChar, 20).Value = p.unit_id;
            cmd.Parameters.Add("@tnxDate", SqlDbType.Date, 10).Value = p.tnxDate;
            cmd.Parameters.Add("@tnxBy", SqlDbType.NVarChar, 20).Value = p.tnxBy;
            cmd.Parameters.Add("@tnxTo", SqlDbType.NVarChar, 20).Value = p.tnxTo;
            cmd.Parameters.Add("@amount", SqlDbType.Decimal).Value = p.amount;
            cmd.Parameters.Add("@logic", SqlDbType.NVarChar, 50).Value = p.logic;
            cmd.Parameters.Add("@shiftID", SqlDbType.NVarChar, 20).Value = p.shiftID;
            cmd.Parameters.Add("@prm_1", SqlDbType.NVarChar, 50).Value = p.prm_1;
            cmd.Parameters.Add("@prm_2", SqlDbType.NVarChar, 50).Value = p.prm_2;
            cmd.Parameters.Add("@login_id", SqlDbType.NVarChar, 16).Value = p.login_id;
            cmd.Parameters.Add("@result", SqlDbType.NVarChar, 50).Value = "N/A";
            cmd.Parameters["@result"].Direction = ParameterDirection.InputOutput;
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
        public dataSet Hospital_BillPushReport(ipPharmacyInfo ip)
        {
            if (ip.Logic == "HIS:PushDataSummary" && ip.prm_1!= "ByIPDNo")
            {
                string IPOPNos = string.Empty;
                dataSet ds = hisDB.GetAdmittedIPDPatient();
                foreach (DataRow dr in ds.ResultSet.Tables[0].Rows)
                {
                    IPOPNos += dr["IPDNO"].ToString() + "|";
                }
                ip.IPDNos = IPOPNos;
            }           
            dataSet dsObj = new dataSet();
            using (SqlConnection con = new SqlConnection(GlobalConfig.ConStr_ChandanPharmacyLive))
            {
                using (SqlCommand cmd = new SqlCommand("pHospital_BillPushReport", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandTimeout = 2500;
                    cmd.Parameters.Add("@IPDNos", SqlDbType.VarChar, 5000).Value = ip.IPDNos;
                    cmd.Parameters.Add("@from", SqlDbType.Date).Value = ip.from;
                    cmd.Parameters.Add("@to", SqlDbType.Date).Value = ip.to;
                    cmd.Parameters.Add("@sale_inv_no", SqlDbType.VarChar, 20).Value = ip.sale_inv_no;                 
                    cmd.Parameters.Add("@Logic", SqlDbType.VarChar, 50).Value = ip.Logic;
                    try
                    {
                        con.Open();
                        DataSet ds1 = new DataSet();
                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        da.Fill(ds1);
                        dsObj.ResultSet = ds1;
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
        public dataSet Pharmacy_Queries(ipPharmacyInfo ip)
        {
            dataSet dsObj = new dataSet();
            using (SqlConnection con = new SqlConnection(GlobalConfig.ConStr_Hospital))
            {
                using (SqlCommand cmd = new SqlCommand("pPharmacy_Queries", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandTimeout = 2500;
                    cmd.Parameters.Add("@IPDNo", SqlDbType.VarChar, 10).Value = ip.IPDNo;
                    cmd.Parameters.Add("@from", SqlDbType.DateTime).Value = ip.from;
                    cmd.Parameters.Add("@to", SqlDbType.DateTime).Value = ip.to;
                    cmd.Parameters.Add("@Prm1", SqlDbType.VarChar, 100).Value = ip.prm_1;
                    cmd.Parameters.Add("@Prm2", SqlDbType.VarChar, 100).Value = ip.prm_2;                    
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
        public dataSet Hospital_Queries(ipPharmacyInfo ip)
        {
            dataSet dsObj = new dataSet();
            using (SqlConnection con = new SqlConnection(GlobalConfig.ConStr_ChandanPharmacyLive))
            {
                using (SqlCommand cmd = new SqlCommand("pHospital_Queries", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandTimeout = 2500;
                    cmd.Parameters.Add("@unit_id", SqlDbType.VarChar, 10).Value = ip.unit_id;
                    cmd.Parameters.Add("@logic", SqlDbType.VarChar, 50).Value = ip.Logic;
                    cmd.Parameters.Add("@uhid", SqlDbType.VarChar, 20).Value = ip.uhid;
                    cmd.Parameters.Add("@prm_1", SqlDbType.VarChar, 50).Value = ip.prm_1;
                    cmd.Parameters.Add("@prm_2", SqlDbType.VarChar, 50).Value = ip.prm_2;
                    cmd.Parameters.Add("@prm_3", SqlDbType.VarChar, 50).Value = ip.prm_3;
                    cmd.Parameters.Add("@login_id", SqlDbType.VarChar, 50).Value = ip.login_id;
                    try
                    {
                        con.Open();
                        DataSet ds = new DataSet();
                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        da.Fill(ds);
                        dsObj.ResultSet = ds;
                        dsObj.Msg = "Success";
                        con.Close();
                        //if(ip.Logic.Contains("IPDSalebyIPDNo") && ds.Tables[0].Rows.Count > 0)
                        //{
                        //  ip.Logic = "InsertBillInfo";
                        //  Pharmacy_InsertBillInfo(ds.Tables[0], ip);
                        //}
                        //else
                        //{
                        //  dsObj.Msg = "Record Not Found.";
                        //}
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
        public string  Hospital_VerifyIPDBill(ipPharmacyInfo obj)
        {           
            string processInfo = string.Empty;
            using (SqlConnection con = new SqlConnection(GlobalConfig.ConStr_ChandanPharmacyLive))
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand("pHospital_VerifyIPDBill", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandTimeout = 2500;                  
                    cmd.Parameters.Add("@IPDNo", SqlDbType.VarChar, 20).Value = obj.IPDNo;
                    cmd.Parameters.Add("@SaleInvNos", SqlDbType.VarChar, 5000).Value = obj.SaleInvNos;
                    cmd.Parameters.Add("@prm_1", SqlDbType.VarChar, 50).Value = obj.prm_1;
                    cmd.Parameters.Add("@login_id", SqlDbType.VarChar, 50).Value = obj.login_id;
                    cmd.Parameters.Add("@Logic", SqlDbType.VarChar, 50).Value = obj.Logic;
                    cmd.Parameters.Add("@result", SqlDbType.VarChar, 100).Value = "";
                    cmd.Parameters["@result"].Direction = ParameterDirection.InputOutput;
                    try
                    {
                        cmd.ExecuteNonQuery();
                        processInfo = (string)cmd.Parameters["@result"].Value.ToString();
                    }
                    catch (Exception sqlEx)
                    {
                        processInfo = "Error Found   : " + sqlEx.Message;
                    }
                    finally { con.Close(); }
                }
            }
            return processInfo;
        }
    }
}