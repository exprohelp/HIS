using HISWebApi.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using static HISWebApi.Models.IPDBO;

namespace HISWebApi.Repository.IPD.MobileApp
{
    public class DoctorApp
    {
        HISDBLayer his_layer = new HISDBLayer();
        Commission.Commission objComm = new Commission.Commission();
        public dataSet GetAdmittedIPDPatientByDoctor(string DoctorId)
        {
            string _result = string.Empty;
            return his_layer.GetAdmittedIPDPatientByDoctor(DoctorId);
        }
        public dataSet HIS_PatientQOfDoctor(string DoctorId)
        {
            string _result = string.Empty;
            return his_layer.HIS_PatientQOfDoctor(DoctorId);
        }
        public dataSet HIS_GetPatientListByMobileOrUHID(string DoctorId)
        {
            string _result = string.Empty;
            return his_layer.HIS_GetPatientListByMobileOrUHID(DoctorId);
        }
        public dataSet HIS_DashboardQueries(ReferralCommissionBO objBO)
        {
            string _result = string.Empty;
            return objComm.HIS_DashboardQueries(objBO);
        }

        public dataSet Doctor_AppQuerries(ipDoctorApp objBO)
        {
            dataSet objDS = new dataSet();
            string t = GlobalConfig.ConStr_Hospital;
            using (SqlConnection con = new SqlConnection(GlobalConfig.ConStr_Hospital))
            {
                using (SqlCommand cmd = new SqlCommand("pDoctor_AppQuerries", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandTimeout = 2500;
                    cmd.Parameters.Add("@emp_code", SqlDbType.VarChar, 10).Value = objBO.emp_code;
                    cmd.Parameters.Add("@password", SqlDbType.VarChar, 20).Value = objBO.Password;
                    cmd.Parameters.Add("@DoctorId", SqlDbType.VarChar, 10).Value = objBO.DoctorId;
                    cmd.Parameters.Add("@from", SqlDbType.Date).Value = objBO.from;
                    cmd.Parameters.Add("@to", SqlDbType.Date).Value = objBO.to;
                    cmd.Parameters.Add("@Prm1", SqlDbType.VarChar, 50).Value = objBO.Prm1;
                    cmd.Parameters.Add("@Prm2", SqlDbType.VarChar, 50).Value = objBO.Prm2;
                    cmd.Parameters.Add("@Logic", SqlDbType.VarChar, 50).Value = objBO.Logic;
                    try
                    {
                        con.Open();
                        DataSet ds = new DataSet();
                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        da.Fill(ds);
                        objDS.Msg = "Success";
                        objDS.ResultSet = ds;
                        con.Close();
                    }
                    catch (Exception sqlEx)
                    {
                        objDS.ResultSet = null;
                        objDS.Msg = sqlEx.Message;
                    }
                    finally { con.Close(); }
                    return objDS;
                }
            }            
        }
        public dataSet IPD_ReferenceAndDischargeQueries(ipReference objBO)
        {
            dataSet objDS = new dataSet();
            using (SqlConnection con = new SqlConnection(GlobalConfig.ConStr_Hospital))
            {
                using (SqlCommand cmd = new SqlCommand("pIPD_ReferenceAndDischargeQueries", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandTimeout = 2500;
                    cmd.Parameters.Add("@DoctorId",SqlDbType.VarChar, 10).Value = objBO.DoctorId;
                    cmd.Parameters.Add("@IPDNo",SqlDbType.VarChar, 10).Value = objBO.IPDNo;
                    cmd.Parameters.Add("@from",SqlDbType.Date).Value = objBO.from;
                    cmd.Parameters.Add("@to",SqlDbType.Date).Value = objBO.to;
                    cmd.Parameters.Add("@Prm1",SqlDbType.VarChar, 50).Value = objBO.Prm1;
                    cmd.Parameters.Add("@Logic",SqlDbType.VarChar, 50).Value = objBO.Logic;
                    try
                    {
                        con.Open();
                        DataSet ds = new DataSet();
                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        da.Fill(ds);
                        objDS.Msg = "Success";
                        objDS.ResultSet = ds;
                        con.Close();
                    }
                    catch (Exception sqlEx)
                    {
                        objDS.ResultSet = null;
                        objDS.Msg = sqlEx.Message;
                    }
                    finally { con.Close(); }
                    return objDS;
                }
            }
        }
        public string  IPD_ReferenceInsertUpdate(ipReference objBO)
        {
            string processInfo = string.Empty;
            using (SqlConnection con = new SqlConnection(GlobalConfig.ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("pIPD_ReferenceInsertUpdate", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandTimeout = 2500;
                    cmd.Parameters.Add("@hosp_id", SqlDbType.VarChar, 10).Value = objBO.HospId;
                    cmd.Parameters.Add("@RefId", SqlDbType.VarChar, 10).Value = objBO.RefId;
                    cmd.Parameters.Add("@UHID", SqlDbType.VarChar, 20).Value = objBO.UHID;
                    cmd.Parameters.Add("@IPDNo", SqlDbType.VarChar, 20).Value = objBO.IPDNo;
                    cmd.Parameters.Add("@PatientName", SqlDbType.VarChar, 100).Value = objBO.PatientName;
                    cmd.Parameters.Add("@RoomName", SqlDbType.VarChar, 100).Value = objBO.RoomName;
                    cmd.Parameters.Add("@RefBy", SqlDbType.VarChar, 10).Value = objBO.RefBy;
                    cmd.Parameters.Add("@RefTo", SqlDbType.VarChar, 10).Value = objBO.RefTo;
                    cmd.Parameters.Add("@RefType", SqlDbType.VarChar, 30).Value = objBO.RefType;
                    cmd.Parameters.Add("@RefRemark", SqlDbType.VarChar, 500).Value = objBO.RefRemark;
                    cmd.Parameters.Add("@Observation", SqlDbType.VarChar, 500).Value = objBO.Observation;
                    cmd.Parameters.Add("@RefStatus", SqlDbType.VarChar, 30).Value = objBO.RefStatus;
                    cmd.Parameters.Add("@smsText", SqlDbType.VarChar, 500).Value = objBO.SmsText;
                    cmd.Parameters.Add("@login_id", SqlDbType.VarChar, 10).Value = objBO.login_id;
                    cmd.Parameters.Add("@Logic", SqlDbType.VarChar, 20).Value = objBO.Logic;
                    cmd.Parameters.Add("@result", SqlDbType.VarChar, 50).Value = "";
                    cmd.Parameters["@result"].Direction = ParameterDirection.InputOutput;
                    try
                    {
                        con.Open();
                        cmd.ExecuteNonQuery();
                        processInfo = (string)cmd.Parameters["@result"].Value.ToString().Split('|')[0];
                        string RefId =(string)cmd.Parameters["@result"].Value.ToString().Split('|')[1];
                        if(processInfo.Contains("Success"))
                        {
                            ipReference obj = new ipReference();
                            obj.Logic = "RefNotifyDetail";
                            obj.Prm1 = RefId;
                            dataSet ds = IPD_ReferenceAndDischargeQueries(obj);
                            DataRow dr = ds.ResultSet.Tables[0].Rows[0];
                            if (dr["smsTo"].ToString().Length == 10)
                            {
                                string sms = "Dear Doctor,\n" + dr["RefByName"].ToString() + " has sent a reference, details as follows:\nIPD No. " + dr["IPdNo"].ToString() + ",\nName : " + dr["PatientName"].ToString() + "\nLocation : " + dr["RoomName"].ToString() + "\n\n-Team Chandan";
                                Utilities.SmsClass smObj = new Utilities.SmsClass();
                                string response= smObj.SendSms(dr["smsTo"].ToString(), sms);
                                //string response = smObj.SendSms("9838003146", sms);
                                string qry_sms = "insert into online_doctorAppSmsLog(appointmentId, mobile_no, sms, login_id, response_message) ";
                                qry_sms += "values('" + RefId + "','" + dr["smsTo"].ToString() + "', '" + sms + "', '-','" + response + "')";
                                DBManager.QueryExecute(qry_sms, GlobalConfig.ConStr_Hospital);
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
        public string  DoctorShareByDoctorId(ReferralCommissionBO objBO)
        {
            string _result = string.Empty;
            dataSet ds = objComm.HIS_DashboardQueries(objBO);
            StringBuilder sb = new StringBuilder();
            if (ds.ResultSet.Tables.Count > 0 && ds.ResultSet.Tables[0].Rows.Count > 0)
            {
                sb.Append("<table class='table table-bordered'>");
                string str = "Share Of Month : " + ds.ResultSet.Tables[0].Rows[0]["month_name"].ToString();
                sb.Append("<caption  style='text-align:center;' >" + str + "</caption>");
                sb.Append("<tr>");
                sb.Append("<td style='text-align:left;font-size:14px'>TnxType</th>");
                sb.Append("<td style='text-align:Right;font-size:14px'>Amount</th>");
                sb.Append("</tr>");

                string temp = string.Empty;
                decimal Total = 0;
                decimal non_emergency_amt = 0;
                decimal GTotal = 0;
                decimal Actual_share = 0;
                decimal min_gurantee = 0;
                decimal emergency_ser_share = 0;
                int count = 0;
                min_gurantee = Convert.ToDecimal(ds.ResultSet.Tables[0].Rows[0]["min_gurantee"]);
                foreach (DataRow dr in ds.ResultSet.Tables[0].Rows)
                {
                    if (temp != dr["TnxType"].ToString())
                    {
                        if (count > 0)
                        {
                            sb.Append("<tr>");
                            sb.Append("<td style='width:60%;text-align:Right;padding-top:2px;padding-bottom:2px;font-size:12px;'><b>Total : </b> </td>");
                            sb.Append("<td style='width:40%;text-align:Right;padding-top:2px;padding-bottom:2px;font-size:14px;'><b> " + Total.ToString() + "</b> </td>");
                            sb.Append("</tr> ");
                            Total = 0;
                        }
                        sb.Append("<tr>");
                        sb.Append("<td colspan='2' style='width:10%;text-align:left;padding-top:2px;padding-bottom:2px;font-size:13px;background-color:#8d8dd0;color:white'><b>" + dr["TnxType"].ToString() + "</b> </td>");
                        sb.Append("</tr> ");
                        temp = dr["TnxType"].ToString();
                    }
                    if (dr["CategoryName"].ToString() != "Emergency-Services")
                        non_emergency_amt = non_emergency_amt + Convert.ToDecimal(dr["final_share"]);

                    if (dr["CategoryName"].ToString() == "Emergency-Services")
                        emergency_ser_share = emergency_ser_share + Convert.ToDecimal(dr["final_share"]);

                    sb.Append("<tr>");
                    sb.Append("<td style='width:60%;text-align:left;padding-top:2px;padding-bottom:2px;font-size:13px;'>" + dr["CategoryName"].ToString() + "</td>");
                    sb.Append("<td style='width:40%;text-align:Right;padding-top:2px;padding-bottom:2px;font-size:13px;'><b> " + dr["final_share"].ToString() + "</b> </td>");
                    sb.Append("</tr> ");
                    Total = Total + Convert.ToInt32(dr["final_share"]);
                    GTotal = GTotal + Convert.ToInt32(dr["final_share"]);
                    count++;
                }
                sb.Append("<tr>");
                sb.Append("<td style='width:60%;text-align:Right;padding-top:2px;padding-bottom:2px;font-size:12px;'><b>Total : </b> </td>");
                sb.Append("<td style='width:40%;text-align:Right;padding-top:2px;padding-bottom:2px;font-size:14px;'><b> " + Total.ToString() + "</b> </td>");
                sb.Append("</tr> ");

                if (non_emergency_amt < min_gurantee)
                    Actual_share = min_gurantee + emergency_ser_share;
                else
                    Actual_share = GTotal;

                sb.Append("<tr>");
                sb.Append("<td style='width:60%;text-align:Right;padding-top:2px;padding-bottom:2px;font-size:14px;'><b>Grand Total : </b> </td>");
                sb.Append("<td style='width:40%;text-align:Right;padding-top:2px;padding-bottom:2px;font-size:14px;'><b>" + GTotal.ToString() + "</b> </td>");
                sb.Append("</tr> ");

                sb.Append("<tr>");
                sb.Append("<td style='width:70%;text-align:Right;padding-top:2px;padding-bottom:2px;font-size:11px;;background-color:#efdda8'><b>" + "M.G. : [" + min_gurantee.ToString("0") + "]" + "  Payable </b> </td>");
                sb.Append("<td style='width:30%;text-align:Right;padding-top:2px;padding-bottom:2px;font-size:13px;;background-color:#efdda8'><b> " + Actual_share.ToString("0") + "</b> </td>");
                sb.Append("</tr> ");

                sb.Append("</table>");
            }
            string months = string.Empty;
            string sale = string.Empty;
            if (ds.ResultSet.Tables.Count > 0 && ds.ResultSet.Tables[1].Rows.Count > 0)
            {
                foreach (DataRow dr in ds.ResultSet.Tables[1].Rows)
                {
                    months = months + dr["mth"].ToString() + ",";
                    sale = sale + dr["share"].ToString() + ",";
                }
                months = months.Substring(0, months.Length - 1);
                sale = sale.Substring(0, sale.Length - 1);
            }
            return sb.ToString() + "|" + months.ToString() + "|" + sale.ToString();
        }
        public string IPD_OTBookingInsertUpdate(OTBookingInfo objBO)
        {
            string processInfo = string.Empty;
            using (SqlConnection con = new SqlConnection(GlobalConfig.ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("pIPD_OTBookingInsertUpdate", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandTimeout = 2500;
                    cmd.Parameters.Add("@AutoId", SqlDbType.VarChar, 10).Value = objBO.AutoId;
                    cmd.Parameters.Add("@OTCartId", SqlDbType.VarChar, 10).Value = objBO.OTCartId;
                    cmd.Parameters.Add("@DoctorId", SqlDbType.VarChar, 20).Value = objBO.DoctorId;
                    cmd.Parameters.Add("@UHID", SqlDbType.VarChar, 20).Value = objBO.UHID;
                    cmd.Parameters.Add("@patientName", SqlDbType.VarChar, 20).Value = objBO.patientName;
                    cmd.Parameters.Add("@patientMobile", SqlDbType.VarChar, 100).Value = objBO.patientMobile;
                    cmd.Parameters.Add("@bookingDate", SqlDbType.Date, 12).Value = objBO.bookingDate;
                    cmd.Parameters.Add("@bookFromTime", SqlDbType.VarChar, 10).Value = objBO.bookFromTime;
                    cmd.Parameters.Add("@bookToTime", SqlDbType.VarChar, 10).Value = objBO.bookToTime;
                    cmd.Parameters.Add("@Prm1", SqlDbType.VarChar, 50).Value = objBO.Prm1;
                    cmd.Parameters.Add("@Remark", SqlDbType.VarChar, 500).Value = objBO.Remark;
                    cmd.Parameters.Add("@ItemRequired", SqlDbType.VarChar, 500).Value = objBO.ItemRequired;
                    cmd.Parameters.Add("@bookBy", SqlDbType.VarChar, 10).Value = objBO.bookBy;
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
        public string Conference_BookingInsertUpdate(ConferenceBookingInfo objBO)
        {
            string processInfo = string.Empty;
            using (SqlConnection con = new SqlConnection(GlobalConfig.ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("pConference_BookingInsertUpdate", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandTimeout = 2500;
                    cmd.Parameters.Add("@ConfId", SqlDbType.VarChar, 10).Value = objBO.ConfId;
                    cmd.Parameters.Add("@BookingType", SqlDbType.VarChar, 20).Value = objBO.BookingType;
                    cmd.Parameters.Add("@bookingDate", SqlDbType.Date, 12).Value = objBO.bookingDate;
                    cmd.Parameters.Add("@bookFromTime", SqlDbType.VarChar, 10).Value = objBO.bookFromTime;
                    cmd.Parameters.Add("@bookToTime", SqlDbType.VarChar, 10).Value = objBO.bookToTime;
                    cmd.Parameters.Add("@Subject", SqlDbType.VarChar, 50).Value = objBO.Subject;
                    cmd.Parameters.Add("@Remark", SqlDbType.VarChar, 500).Value = objBO.Remark;
                    cmd.Parameters.Add("@Prm1", SqlDbType.VarChar, 500).Value = objBO.Prm1;
                    cmd.Parameters.Add("@bookBy", SqlDbType.VarChar, 10).Value = objBO.bookBy;
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
                    catch (Exception sqlEx)
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