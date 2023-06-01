using HISWebApi.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using static HISWebApi.Models.IPDBO;

namespace HISWebApi.Repository.IPD
{
    public class IPDNursingServiceRep
    {
        public dataSet IPD_PatientQueries(IPDInfo objBO)
        {
            dataSet dsObj = new dataSet();
            using (SqlConnection con = new SqlConnection(GlobalConfig.ConStr_Hospital))
            {
                using (SqlCommand cmd = new SqlCommand("pIPD_PatientQueries", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandTimeout = 2500;
                    cmd.Parameters.Add("@hosp_id", SqlDbType.VarChar, 10).Value = objBO.hosp_id;
                    cmd.Parameters.Add("@UHID", SqlDbType.VarChar, 20).Value = objBO.UHID;
                    cmd.Parameters.Add("@IPDNo", SqlDbType.VarChar, 11).Value = objBO.IPDNo;
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
        public dataSet pIPD_ItemsRate(IPDInfo objBO)
        {
            dataSet dsObj = new dataSet();
            using (SqlConnection con = new SqlConnection(GlobalConfig.ConStr_Hospital))
            {
                using (SqlCommand cmd = new SqlCommand("pIPD_ItemsRate", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandTimeout = 2500;
                    cmd.Parameters.Add("@hosp_id", SqlDbType.VarChar, 10).Value = objBO.hosp_id;
                    cmd.Parameters.Add("@IPDNo", SqlDbType.VarChar, 20).Value = objBO.IPDNo;
                    cmd.Parameters.Add("@UHID", SqlDbType.VarChar, 20).Value = objBO.UHID;
                    cmd.Parameters.Add("@doctorId", SqlDbType.VarChar, 10).Value = objBO.DoctorId;
                    cmd.Parameters.Add("@PanelId", SqlDbType.VarChar, 50).Value = objBO.PanelId;
                    cmd.Parameters.Add("@CPT_Code", SqlDbType.VarChar, 10).Value = objBO.CPT_Code;
                    cmd.Parameters.Add("@RoomBillingCategory", SqlDbType.VarChar, 50).Value = objBO.RoomBillingCategory;
                    cmd.Parameters.Add("@SearchType", SqlDbType.VarChar, 50).Value = objBO.SearchType;
                    cmd.Parameters.Add("@ItemIds", SqlDbType.VarChar, 5000).Value = objBO.ItemIds;
                    cmd.Parameters.Add("@Qty", SqlDbType.Int).Value = objBO.Qty;
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
        public string IPD_InsertBloodRequisition(BloodRequisitionInfo objBO, List<ComponentInfo> objRateList)
        {
            string processInfo = string.Empty;           
            if (objRateList.Count > 0)
            {
                DataTable dt = new DataTable();
                dt.Columns.Add("ComponentID", typeof(string));
                dt.Columns.Add("ComponentName", typeof(string));
                dt.Columns.Add("Qty", typeof(int));
                foreach (ComponentInfo objRate in objRateList)
                {
                    DataRow dr = dt.NewRow();
                    dr["ComponentID"] = objRate.ComponentID;
                    dr["ComponentName"] = objRate.ComponentName;
                    dr["Qty"] = objRate.Qty;                  
                    dt.Rows.Add(dr);
                }
                using (SqlConnection con = new SqlConnection(GlobalConfig.ConStr_Hospital))
                {
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand("pIPD_InsertBloodRequisition", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandTimeout = 2500;
                        cmd.Parameters.Add("@UHID", SqlDbType.VarChar, 20).Value = objBO.UHID;
                        cmd.Parameters.Add("@IPOPNo", SqlDbType.VarChar, 20).Value = objBO.IPOPNo;
                        cmd.Parameters.Add("@PatientName", SqlDbType.VarChar, 100).Value = objBO.PatientName;
                        cmd.Parameters.Add("@IPOPType", SqlDbType.VarChar, 50).Value = objBO.IPOPType;                      
                        cmd.Parameters.Add("@IndentNo", SqlDbType.VarChar, 20).Value = objBO.IndentNo;                    
                        cmd.Parameters.Add("@ItemId", SqlDbType.VarChar, 20).Value = objBO.ItemId;                    
                        cmd.Parameters.Add("@ComponentId", SqlDbType.VarChar, 20).Value = objBO.ComponentId;                    
                        cmd.Parameters.Add("@IndentQty", SqlDbType.Int).Value = objBO.IndentQty;                    
                        cmd.Parameters.Add("@IndentBy", SqlDbType.VarChar,10).Value = objBO.IndentBy;                    
                        cmd.Parameters.Add("@TnxId", SqlDbType.VarChar,20).Value = objBO.TnxId;                    
                        cmd.Parameters.Add("@BloodGroupId", SqlDbType.VarChar,20).Value = objBO.BloodGroupId;                    
                        cmd.Parameters.Add("@BloodTestedBy", SqlDbType.VarChar,20).Value = objBO.BloodTestedBy;                    
                        cmd.Parameters.Add("@BloodTestedDate", SqlDbType.DateTime).Value = objBO.BloodTestedDate;                    
                        cmd.Parameters.Add("@Diagnosis", SqlDbType.VarChar,300).Value = objBO.Diagnosis;                    
                        cmd.Parameters.Add("@TransfusionIndicator", SqlDbType.VarChar,100).Value = objBO.TransfusionIndicator;                    
                        cmd.Parameters.Add("@HbPerc", SqlDbType.VarChar,10).Value = objBO.HbPerc;                    
                        cmd.Parameters.Add("@PreviousTransfusion", SqlDbType.Char,1).Value = objBO.PreviousTransfusion;                    
                        cmd.Parameters.Add("@DonorNo", SqlDbType.VarChar,20).Value = objBO.DonorNo;                    
                        cmd.Parameters.Add("@AboRH", SqlDbType.VarChar,20).Value = objBO.AboRH;                    
                        cmd.Parameters.Add("@dateTransfuse", SqlDbType.Date).Value = objBO.dateTransfuse;                    
                        cmd.Parameters.Add("@ReactionAny", SqlDbType.VarChar,200).Value = objBO.ReactionAny;                    
                        cmd.Parameters.Add("@IsPregnant", SqlDbType.Char,1).Value = objBO.IsPregnant;                    
                        cmd.Parameters.Add("@haemolytic_disease", SqlDbType.Char,1).Value = objBO.haemolytic_disease;                    
                        cmd.Parameters.Add("@miscarriage", SqlDbType.Char,1).Value = objBO.miscarriage;                    
                        cmd.Parameters.Add("@ReqType", SqlDbType.VarChar,30).Value = objBO.ReqType;                    
                        cmd.Parameters.Add("@ReqType_Remark1", SqlDbType.VarChar,50).Value = objBO.ReqType_Remark1;                    
                        cmd.Parameters.Add("@ReqType_Remark2", SqlDbType.VarChar,50).Value = objBO.ReqType_Remark2;                    
                        cmd.Parameters.Add("@ReqByDoctorId", SqlDbType.VarChar,10).Value = objBO.ReqByDoctorId;                    
                        cmd.Parameters.AddWithValue("udt_BloodRequisitionComponent", dt);
                        cmd.Parameters.Add("@login_id", SqlDbType.VarChar, 10).Value = objBO.login_id;
                        cmd.Parameters.Add("@Logic", SqlDbType.VarChar, 50).Value = objBO.Logic;
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
        public string IPD_NursingItemInsert(ServiceBookingBO objBooking, List<ServiceItems> objRateList)
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
                    using (SqlCommand cmd = new SqlCommand("pIPD_NursingItemInsert", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandTimeout = 2500;
                        cmd.Parameters.Add("@HospId", SqlDbType.VarChar, 10).Value = objBooking.hosp_id;
                        cmd.Parameters.Add("@IPDNo", SqlDbType.VarChar, 20).Value = objBooking.IPDNo;
                        cmd.Parameters.Add("@DoctorId", SqlDbType.VarChar, 10).Value = objBooking.DoctorId;
                        cmd.Parameters.Add("@ipAddress", SqlDbType.VarChar, 30).Value = objBooking.ipAddress;
                        cmd.Parameters.Add("@login_id", SqlDbType.VarChar, 10).Value = objBooking.login_id;
                        cmd.Parameters.Add("@BillingRole", SqlDbType.VarChar, 10).Value = objBooking.BillingRole;
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
        public string IPD_RoomAndDoctorShift(RoomShiftInfo objBO)
        {
            dataSet dsObj = new dataSet();
            string processInfo = string.Empty;
            using (SqlConnection con = new SqlConnection(GlobalConfig.ConStr_Hospital))
            {
                using (SqlCommand cmd = new SqlCommand("pIPD_RoomAndDoctorShift", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandTimeout = 2500;
                    cmd.Parameters.Add("@IPDNo", SqlDbType.VarChar, 11).Value = objBO.IPDNo;
                    cmd.Parameters.Add("@DoctorId", SqlDbType.VarChar, 20).Value = objBO.DoctorId;
                    cmd.Parameters.Add("@RoomBedId", SqlDbType.VarChar, 20).Value = objBO.RoomBedId;
                    cmd.Parameters.Add("@RoomBillingCategory", SqlDbType.VarChar, 50).Value = objBO.RoomBillingCategory;
                    cmd.Parameters.Add("@Prm1", SqlDbType.VarChar, 100).Value = objBO.Prm1;
                    cmd.Parameters.Add("@Prm2", SqlDbType.VarChar, 100).Value = objBO.Prm2;
                    cmd.Parameters.Add("@RoomChangeDateTime", SqlDbType.DateTime, 20).Value = objBO.RoomChangeDateTime;
                    cmd.Parameters.Add("@login_id", SqlDbType.VarChar, 20).Value = objBO.login_id;
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
        public string IPD_InsertDischargeStatus(DischargeStatusInfo objBO)
        {
            string processInfo = string.Empty;
            using (SqlConnection con = new SqlConnection(GlobalConfig.ConStr_Hospital))
            {
                using (SqlCommand cmd = new SqlCommand("pIPD_InsertDischargeStatus", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandTimeout = 2500;
                    cmd.Parameters.Add("@IPDNo", SqlDbType.VarChar, 30).Value = objBO.IPDNo;
                    cmd.Parameters.Add("@discharge_Init_date", SqlDbType.DateTime).Value = objBO.discharge_Init_date;
                    cmd.Parameters.Add("@InitCallBy", SqlDbType.VarChar, 50).Value = objBO.InitCallBy;
                    cmd.Parameters.Add("@DischargeType", SqlDbType.VarChar, 20).Value = objBO.DischargeType;
                    cmd.Parameters.Add("@DischargeDate", SqlDbType.DateTime).Value = objBO.DischargeDate;
                    cmd.Parameters.Add("@DeathDateTime", SqlDbType.DateTime).Value = objBO.DeathDateTime;
                    cmd.Parameters.Add("@CauseOfDeath", SqlDbType.VarChar, 500).Value = objBO.CauseOfDeath;
                    cmd.Parameters.Add("@TypeOfDeath", SqlDbType.VarChar, 100).Value = objBO.TypeOfDeath;
                    cmd.Parameters.Add("@Remark", SqlDbType.VarChar, 500).Value = objBO.Remark;
                    cmd.Parameters.Add("@login_id", SqlDbType.VarChar, 20).Value = objBO.login_id;
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
        public string IPD_GenerateBill(DischargeStatusInfo objBO)
        {
            string processInfo = string.Empty;
            using (SqlConnection con = new SqlConnection(GlobalConfig.ConStr_Hospital))
            {
                using (SqlCommand cmd = new SqlCommand("pIPD_BillGeneration", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandTimeout = 2500;
                    cmd.Parameters.Add("@BillNo", SqlDbType.VarChar, 30).Value = objBO.BillNo;
                    cmd.Parameters.Add("@IPDNo", SqlDbType.VarChar, 30).Value = objBO.IPDNo;
                    cmd.Parameters.Add("@BillingType", SqlDbType.VarChar, 30).Value = objBO.BillingType;
                    cmd.Parameters.Add("@ApprAmount", SqlDbType.Decimal).Value = objBO.ApprAmount;
                    cmd.Parameters.Add("@CoPayAmount", SqlDbType.Decimal).Value = objBO.CoPayAmount;
                    cmd.Parameters.Add("@DiscountAmount", SqlDbType.Decimal).Value = objBO.DiscountAmount;
                    cmd.Parameters.Add("@Remark", SqlDbType.VarChar, 500).Value = objBO.Remark;
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
                    return processInfo;
                }
            }
        }

        public string BB_InsertModifyIndentInfo(BBIndentInfo objBO)
        {
            string processInfo = string.Empty;
            if (objBO.componentInfo.Count > 0)
            {
                DataTable dt = new DataTable();
                dt.Columns.Add("ItemId", typeof(string));
                dt.Columns.Add("ComponentID", typeof(string));
                dt.Columns.Add("IndentQty", typeof(int));
                foreach (BBComponentInfo objValue in objBO.componentInfo)
                {
                    DataRow dr = dt.NewRow();
                    dr["ItemId"] = objValue.ItemId;
                    dr["ComponentID"] = objValue.ComponentID;
                    dr["IndentQty"] = objValue.IndentQty;
                    dt.Rows.Add(dr);
                }
                using (SqlConnection con = new SqlConnection(GlobalConfig.ConStr_Hospital))
                {
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand("BB_Insert_Modify_bb_Indent_Info", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandTimeout = 2500;
                        cmd.Parameters.Add("@hosp_id", SqlDbType.VarChar, 20).Value = objBO.hosp_id;
                        cmd.Parameters.Add("@IPOPNo", SqlDbType.VarChar, 20).Value = objBO.IPOPNo;
                        cmd.Parameters.Add("@IPOPType", SqlDbType.VarChar, 50).Value = objBO.IPOPType;
                        cmd.Parameters.Add("@IndentNo", SqlDbType.VarChar, 20).Value = objBO.IndentNo;
                        cmd.Parameters.AddWithValue("udt_BB_Indent_Info", dt);
                        cmd.Parameters.Add("@Prm1", SqlDbType.VarChar, 100).Value = objBO.Prm1;
                        cmd.Parameters.Add("@Prm2", SqlDbType.VarChar, 100).Value = objBO.Prm2;
                        cmd.Parameters.Add("@login_id", SqlDbType.VarChar, 20).Value = objBO.login_id;
                        cmd.Parameters.Add("@Logic", SqlDbType.VarChar, 50).Value = objBO.Logic;
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
    }
}