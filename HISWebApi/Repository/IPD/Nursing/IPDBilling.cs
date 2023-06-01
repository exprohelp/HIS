using HISWebApi.Models;
using HISWebApi.Repository.Utilities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using static HISWebApi.Models.IPDBO;

namespace HISWebApi.Repository.IPD
{
    public class IPDBilling
    {
        public dataSet IPD_BillingQuerries(IPDInfo objBO)
        {
            dataSet dsObj = new dataSet();
            using (SqlConnection con = new SqlConnection(GlobalConfig.ConStr_Hospital))
            {
                using (SqlCommand cmd = new SqlCommand("pIPD_BillingQuerries", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandTimeout = 2500;
                    cmd.Parameters.Add("@hosp_id", SqlDbType.VarChar, 10).Value = objBO.hosp_id;
                    cmd.Parameters.Add("@UHID", SqlDbType.VarChar, 20).Value = objBO.UHID;
                    cmd.Parameters.Add("@IPDNo", SqlDbType.VarChar, 11).Value = objBO.IPDNo;
                    cmd.Parameters.Add("@DoctorId", SqlDbType.VarChar, 15).Value = objBO.DoctorId;
                    cmd.Parameters.Add("@Floor", SqlDbType.VarChar, 50).Value = objBO.Floor;
                    cmd.Parameters.Add("@PanelId", SqlDbType.VarChar, 50).Value = objBO.PanelId;
                    cmd.Parameters.Add("@from", SqlDbType.Date).Value = objBO.from;
                    cmd.Parameters.Add("@to", SqlDbType.Date).Value = objBO.to;
                    cmd.Parameters.Add("@Prm1", SqlDbType.VarChar, 500).Value = objBO.Prm1;
                    cmd.Parameters.Add("@Prm2", SqlDbType.VarChar, 500).Value = objBO.Prm2;
                    cmd.Parameters.Add("@login_id", SqlDbType.VarChar, 20).Value = objBO.login_id;
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
        public string  IPD_BillingInsertModifyItems(ServiceBookingBO objBooking, List<ServiceItems> objRateList)
        {
            string processInfo = string.Empty;
            string processInfo1 = string.Empty;
            if (objRateList.Count > 0)
            {
                DataTable dt = new DataTable();
                dt.Columns.Add("AutoId", typeof(Int64));
                dt.Columns.Add("TnxId", typeof(string));
                dt.Columns.Add("RateListId", typeof(string));
                dt.Columns.Add("RateListName", typeof(string));
                dt.Columns.Add("CatId", typeof(string));
                dt.Columns.Add("ItemId", typeof(string));
                dt.Columns.Add("ItemSection", typeof(string));
                dt.Columns.Add("IsPackage", typeof(int));
                dt.Columns.Add("IsRateEditable", typeof(char));
                dt.Columns.Add("IsPatientPayable", typeof(char));
                dt.Columns.Add("IsDiscountable", typeof(char));
                dt.Columns.Add("mrp_rate", typeof(decimal));
                dt.Columns.Add("panel_rate", typeof(decimal));
                dt.Columns.Add("panel_discount", typeof(decimal));
                dt.Columns.Add("qty", typeof(int));            
                dt.Columns.Add("adl_disc_amount", typeof(decimal));
                dt.Columns.Add("adl_disc_perc", typeof(decimal));
                dt.Columns.Add("net_amount", typeof(decimal));
                dt.Columns.Add("IsUrgent", typeof(char));
                dt.Columns.Add("Remark", typeof(string));
                foreach (ServiceItems objRate in objRateList)
                {
                    DataRow dr = dt.NewRow();
                    dr["AutoId"] = objRate.AutoId;
                    dr["TnxId"] = objRate.TnxId;
                    dr["RateListId"] = objRate.RateListId;
                    dr["RateListName"] = objRate.RateListName;
                    dr["CatId"] = objRate.CatId;
                    dr["ItemId"] = objRate.ItemId;
                    dr["ItemSection"] = objRate.ItemSection;
                    dr["IsPackage"] = objRate.IsPackage;
                    dr["IsRateEditable"] = objRate.IsRateEditable;
                    dr["IsPatientPayable"] = objRate.IsPatientPayable;
                    dr["IsDiscountable"] = objRate.IsDiscountable;
                    dr["mrp_rate"] = objRate.mrp_rate;
                    dr["panel_rate"] = objRate.panel_rate;
                    dr["panel_discount"] = objRate.panel_discount;
                    dr["qty"] = objRate.qty;                 
                    dr["adl_disc_amount"] = objRate.adl_disc_amount;
                    dr["adl_disc_perc"] = objRate.adl_disc_perc;
                    dr["net_amount"] = objRate.net_amount;
                    dr["IsUrgent"] = objRate.IsUrgent;
                    dr["Remark"] = objRate.Remark;
                    dt.Rows.Add(dr);
                }
                using (SqlConnection con = new SqlConnection(GlobalConfig.ConStr_Hospital))
                {
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand("pIPD_BillingInsertModifyItems", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandTimeout = 2500;
                        cmd.Parameters.Add("@HospId", SqlDbType.VarChar, 10).Value = objBooking.hosp_id;
                        cmd.Parameters.Add("@IPDNo", SqlDbType.VarChar, 20).Value = objBooking.IPDNo;
                        cmd.Parameters.Add("@DoctorId", SqlDbType.VarChar, 10).Value = objBooking.DoctorId;
                        cmd.Parameters.Add("@ipAddress", SqlDbType.VarChar, 30).Value = objBooking.ipAddress;
                        cmd.Parameters.Add("@login_id", SqlDbType.VarChar, 10).Value = objBooking.login_id;
                        cmd.Parameters.Add("@Logic", SqlDbType.VarChar, 50).Value = objBooking.Logic;
                        cmd.Parameters.AddWithValue("UDT_TestAndProcedureItems", dt);
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
            }
            return processInfo;
        }
        public dataSet IPD_CalculateAndInsertSurguryAmount(ServiceBookingBO objBooking, List<ServiceItems> objRateList)
        {
            dataSet dsObj = new dataSet();
            string processInfo = string.Empty;
            string processInfo1 = string.Empty;
            if (objRateList.Count > 0)
            {
                DataTable dt = new DataTable();
                dt.Columns.Add("AutoId", typeof(Int64));
                dt.Columns.Add("TnxId", typeof(string));
                dt.Columns.Add("RateListId", typeof(string));
                dt.Columns.Add("RateListName", typeof(string));
                dt.Columns.Add("CatId", typeof(string));
                dt.Columns.Add("ItemId", typeof(string));
                dt.Columns.Add("ItemSection", typeof(string));
                dt.Columns.Add("IsPackage", typeof(int));
                dt.Columns.Add("IsRateEditable", typeof(char));
                dt.Columns.Add("IsPatientPayable", typeof(char));
                dt.Columns.Add("IsDiscountable", typeof(char));
                dt.Columns.Add("mrp_rate", typeof(decimal));
                dt.Columns.Add("panel_rate", typeof(decimal));
                dt.Columns.Add("panel_discount", typeof(decimal));
                dt.Columns.Add("qty", typeof(int));
                dt.Columns.Add("adl_disc_amount", typeof(decimal));
                dt.Columns.Add("adl_disc_perc", typeof(decimal));
                dt.Columns.Add("net_amount", typeof(decimal));
                dt.Columns.Add("IsUrgent", typeof(char));
                dt.Columns.Add("Remark", typeof(string));
                foreach (ServiceItems objRate in objRateList)
                {
                    DataRow dr = dt.NewRow();
                    dr["AutoId"] = objRate.AutoId;
                    dr["TnxId"] = objRate.TnxId;
                    dr["RateListId"] = objRate.RateListId;
                    dr["RateListName"] = objRate.RateListName;
                    dr["CatId"] = objRate.CatId;
                    dr["ItemId"] = objRate.ItemId;
                    dr["ItemSection"] = objRate.ItemSection;
                    dr["IsPackage"] = objRate.IsPackage;
                    dr["IsRateEditable"] = objRate.IsRateEditable;
                    dr["IsPatientPayable"] = objRate.IsPatientPayable;
                    dr["IsDiscountable"] = objRate.IsDiscountable;
                    dr["mrp_rate"] = objRate.mrp_rate;
                    dr["panel_rate"] = objRate.panel_rate;
                    dr["panel_discount"] = objRate.panel_discount;
                    dr["qty"] = objRate.qty;
                    dr["adl_disc_amount"] = objRate.adl_disc_amount;
                    dr["adl_disc_perc"] = objRate.adl_disc_perc;
                    dr["net_amount"] = objRate.net_amount;
                    dr["IsUrgent"] = objRate.IsUrgent;
                    dr["Remark"] = objRate.Remark;
                    dt.Rows.Add(dr);
                }
                using (SqlConnection con = new SqlConnection(GlobalConfig.ConStr_Hospital))
                {                   
                    using (SqlCommand cmd = new SqlCommand("pIPD_CalculateAndInsertSurguryAmount", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandTimeout = 2500;
                        cmd.Parameters.Add("@HospId", SqlDbType.VarChar, 10).Value = objBooking.hosp_id;
                        cmd.Parameters.Add("@DoctorId", SqlDbType.VarChar, 10).Value = objBooking.DoctorId;
                        cmd.Parameters.Add("@IPDNo", SqlDbType.VarChar, 20).Value = objBooking.IPDNo;
                        cmd.Parameters.Add("@IPAddress", SqlDbType.VarChar, 30).Value = objBooking.ipAddress;
                        cmd.Parameters.Add("@login_id", SqlDbType.VarChar, 10).Value = objBooking.login_id;
                        cmd.Parameters.Add("@Surgery_amt", SqlDbType.Decimal).Value = objBooking.Surgery_amt;
                        cmd.Parameters.Add("@Surgeon_amt", SqlDbType.Decimal).Value = objBooking.Surgeon_amt;
                        cmd.Parameters.Add("@Logic", SqlDbType.VarChar, 50).Value = objBooking.Logic;
                        cmd.Parameters.AddWithValue("UDT_TestAndProcedureItems", dt);
                        cmd.Parameters.Add("@result", SqlDbType.VarChar, 100).Value = "";
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
                            dsObj.ResultSet = null;
                            dsObj.Msg = sqlEx.Message;
                        }
                        finally { con.Close(); }                       
                    }
                }
            }
            return dsObj;
        }
        public string  IPD_TPApprovalEntry(ipTPApproval objBO,byte[] ImageBytes)
        {
            string processInfo = string.Empty;
            using(SqlConnection con = new SqlConnection(GlobalConfig.ConStr_Hospital))
            {
                using (SqlCommand cmd = new SqlCommand("pIPD_TPApprovalEntry", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandTimeout = 2500;
                        cmd.Parameters.Add("@auto_id", SqlDbType.Int).Value = objBO.AutoId;
                        cmd.Parameters.Add("@IPDNo", SqlDbType.VarChar, 30).Value = objBO.IPDNo;
                        cmd.Parameters.Add("@ApprovalDate", SqlDbType.VarChar, 30).Value = objBO.ApprovalDate;
                        cmd.Parameters.Add("@ApprovalType", SqlDbType.VarChar, 30).Value = objBO.ApprovalType;
                        cmd.Parameters.Add("@ClaimNo", SqlDbType.VarChar, 30).Value = objBO.ClaimNo;
                        cmd.Parameters.Add("@ApprovalAmount", SqlDbType.Decimal).Value = objBO.ApprovalAmount;
                        cmd.Parameters.Add("@CoPayAmount", SqlDbType.Decimal).Value = objBO.CoPayAmount;
                        cmd.Parameters.Add("@Discount", SqlDbType.Decimal).Value = objBO.Discount;
                        cmd.Parameters.Add("@Remark", SqlDbType.VarChar, 30).Value = objBO.Remark;
                        cmd.Parameters.Add("@doc_path", SqlDbType.VarChar, 150).Value = "";
                        cmd.Parameters.Add("@login_id", SqlDbType.VarChar, 50).Value = objBO.login_id;
                        cmd.Parameters.Add("@Logic", SqlDbType.VarChar, 50).Value = objBO.Logic;
                        cmd.Parameters.Add("@result", SqlDbType.VarChar, 50).Value = "";
                        cmd.Parameters["@result"].Direction = ParameterDirection.InputOutput;
                        try
                        {
                            con.Open();
                            cmd.ExecuteNonQuery();
                            processInfo = (string)cmd.Parameters["@result"].Value.ToString();
                            if(cmd.Parameters["@result"].Value.ToString().Contains("|"))
                            {
                                processInfo = (string)cmd.Parameters["@result"].Value.ToString().Split('|')[0];
                                string AutoId = (string)cmd.Parameters["@result"].Value.ToString().Split('|')[1];
                                string FileName = objBO.IPDNo + "_" + AutoId + "." + objBO.FileExtension;
                                if (ImageBytes.Length > 20)
                                {
                                    string FilePath = "";
                                    UploadClass.IPD_TPAAdvanceDocument(out FilePath, ImageBytes, FileName);
                                    string qry = "update IPD_TPAApproval set doc_path = '" + FilePath + "' where auto_id = " + AutoId + ";";
                                    DBManager.QueryExecute(qry, GlobalConfig.ConStr_Hospital);
                                }
                            }
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
        public string  IPD_TPApprovalRejectEntry(ipTPApproval objBO)
        {
            string processInfo = string.Empty;
            using (SqlConnection con = new SqlConnection(GlobalConfig.ConStr_Hospital))
            {
                using (SqlCommand cmd = new SqlCommand("pIPD_TPApprovalEntry", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandTimeout = 2500;
                    cmd.Parameters.Add("@auto_id", SqlDbType.Int).Value = objBO.AutoId;
                    cmd.Parameters.Add("@IPDNo", SqlDbType.VarChar, 30).Value = objBO.IPDNo;
                    cmd.Parameters.Add("@ApprovalDate", SqlDbType.VarChar, 30).Value = objBO.ApprovalDate;
                    cmd.Parameters.Add("@ApprovalType", SqlDbType.VarChar, 30).Value = objBO.ApprovalType;
                    cmd.Parameters.Add("@ClaimNo", SqlDbType.VarChar, 30).Value = objBO.ClaimNo;
                    cmd.Parameters.Add("@ApprovalAmount", SqlDbType.Decimal).Value = objBO.ApprovalAmount;
                    cmd.Parameters.Add("@CoPayAmount", SqlDbType.Decimal).Value = objBO.CoPayAmount;
                    cmd.Parameters.Add("@Remark", SqlDbType.VarChar, 30).Value = objBO.Remark;
                    cmd.Parameters.Add("@doc_path", SqlDbType.VarChar, 150).Value = "";
                    cmd.Parameters.Add("@login_id", SqlDbType.VarChar, 50).Value = objBO.login_id;
                    cmd.Parameters.Add("@Logic", SqlDbType.VarChar, 50).Value = objBO.Logic;
                    cmd.Parameters.Add("@result", SqlDbType.VarChar, 50).Value = "";
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
            }
        }
    }
}