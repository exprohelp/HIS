using HISWebApi.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace HISWebApi.Repository.WebPortal
{
    public class Appoinment
    {        
        WebPortalDBLayer web_layer = new WebPortalDBLayer();
        public dataSet GetDiagnosticTestAnalysis(ipWebPortal objBO)
        {
            string _result = string.Empty;
            return web_layer.GetDiagnosticTestAnalysis(objBO);
        }
        public dataSet SaveGenerateAppointment(ipWebPortal objBO)
        {
            string _result = string.Empty;
            return web_layer.SaveGenerateAppointment(objBO);
        }
        public dataSet DirectAppointment(ipWebPortal objBO)
        {
            string _result = string.Empty;
            return web_layer.DirectAppointment(objBO);
        }
        public string InsertSmsLog(PatientSmsInfo objBO)
        {
            string processInfo = string.Empty;
            using (SqlConnection con = new SqlConnection(GlobalConfig.ConStr_Hospital))
            {
                using (SqlCommand cmd = new SqlCommand("InsertSmsLog", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandTimeout = 2500;
                    cmd.Parameters.Add("@AppointmentId", SqlDbType.VarChar, 5).Value = objBO.AppointmentId;
                    cmd.Parameters.Add("@MobileNo", SqlDbType.VarChar, 15).Value = objBO.MobileNo;
                    cmd.Parameters.Add("@Sms", SqlDbType.VarChar, 50).Value = objBO.Sms;
                    cmd.Parameters.Add("@SmsResponse", SqlDbType.VarChar, 80).Value = objBO.SmsResponse;
                    cmd.Parameters.Add("@EntrySource", SqlDbType.VarChar, 50).Value = objBO.EntrySource;
                    cmd.Parameters.Add("@login_id", SqlDbType.VarChar, 10).Value = objBO.login_id;
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

        #region AppBooking
        public string OnlineInsertDoctorAppointment(DoctorAppointmentInfo objBO)
        {
            string processInfo = string.Empty;
            using (SqlConnection con = new SqlConnection(GlobalConfig.ConStr_Hospital))
            {
                using (SqlCommand cmd = new SqlCommand("pOnline_InsertDoctorAppointment", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandTimeout = 2500;
                    cmd.Parameters.Add("@CardNo", SqlDbType.VarChar, 20).Value = "-";
                    cmd.Parameters.Add("@UHID", SqlDbType.VarChar, 20).Value = "-";
                    cmd.Parameters.Add("@mobile_no", SqlDbType.VarChar, 10).Value = objBO.MobileNo;
                    cmd.Parameters.Add("@email_id", SqlDbType.VarChar, 50).Value = objBO.EmailId;
                    cmd.Parameters.Add("@patient_name", SqlDbType.VarChar, 100).Value = objBO.PatientName;
                    cmd.Parameters.Add("@gender", SqlDbType.VarChar, 10).Value = objBO.Gender;
                    cmd.Parameters.Add("@age", SqlDbType.Int).Value = objBO.Age;
                    cmd.Parameters.Add("@dob", SqlDbType.Date).Value = objBO.dob;
                    cmd.Parameters.Add("@agetype", SqlDbType.VarChar).Value = objBO.AgeType;
                    cmd.Parameters.Add("@state", SqlDbType.VarChar, 100).Value = objBO.state;
                    cmd.Parameters.Add("@city", SqlDbType.VarChar, 100).Value = objBO.City;
                    cmd.Parameters.Add("@locality", SqlDbType.VarChar, 200).Value = objBO.Locality;
                    cmd.Parameters.Add("@pt_address", SqlDbType.VarChar, 200).Value = objBO.PtAddress;
                    cmd.Parameters.Add("@appointment_reason", SqlDbType.VarChar, 1000).Value = objBO.AppointmentReason;
                    cmd.Parameters.Add("@Logic", SqlDbType.VarChar, 20).Value = "Insert";
                    cmd.Parameters.Add("@result", SqlDbType.VarChar, 500).Value = "";
                    cmd.Parameters["@result"].Direction = ParameterDirection.InputOutput;
                    cmd.Parameters.Add("@AppointmentId", SqlDbType.VarChar, 16).Value = "";
                    cmd.Parameters["@AppointmentId"].Direction = ParameterDirection.InputOutput;
                    try
                    {
                        con.Open();
                        cmd.ExecuteNonQuery();
                        processInfo = (string)cmd.Parameters["@result"].Value.ToString();
                        string AppointmentId = (string)cmd.Parameters["@AppointmentId"].Value.ToString();
                        con.Close();
                        if (processInfo.Contains("Success"))
                        {
                            DoctorAppointmentInfo ip = new DoctorAppointmentInfo();
                            objBO.UnitId = "-";
                            objBO.AppointmentId = AppointmentId;
                            objBO.login_id = "-";
                            objBO.Logic = "BookingConfirmation";
                            OnlineBookingNotification(objBO);
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
        public dataSet OnlineBookingNotification(DoctorAppointmentInfo objBO)
        {
            dataSet dsObj = new dataSet();
            using (SqlConnection con = new SqlConnection(GlobalConfig.ConStr_Hospital))
            {
                using (SqlCommand cmd = new SqlCommand("pOnline_doctorAppQueries", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandTimeout = 2500;
                    cmd.Parameters.Add("@unit_id", SqlDbType.VarChar, 10).Value = "CH01";
                    cmd.Parameters.Add("@appointment_id", SqlDbType.VarChar, 16).Value = objBO.AppointmentId;
                    cmd.Parameters.Add("@from", SqlDbType.DateTime).Value = objBO.From;
                    cmd.Parameters.Add("@to", SqlDbType.DateTime).Value = objBO.To;
                    cmd.Parameters.Add("@prm_1", SqlDbType.VarChar, 50).Value = "-";
                    cmd.Parameters.Add("@login_id", SqlDbType.VarChar, 50).Value = objBO.login_id;
                    cmd.Parameters.Add("@Logic", SqlDbType.VarChar, 200).Value = "BookingDetail";
                    try
                    {
                        con.Open();
                        DataSet ds = new DataSet();
                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        da.Fill(ds);
                        dsObj.ResultSet = ds;
                        dsObj.Msg = "Success";
                        con.Close();
                        if (objBO.Logic == "BookingConfirmation")
                        {
                            SmsDataLayer smsDataLayer = new SmsDataLayer();
                            string mobile_no = ds.Tables[0].Rows[0]["mobile_no"].ToString();
                            string AppointmentId = ds.Tables[0].Rows[0]["AppointmentId"].ToString();
                            ipSMS patientSms = new ipSMS();
                            patientSms.AppId = AppointmentId;
                            patientSms.MobileNo = mobile_no;

                            ipSMS empSms = new ipSMS();
                            empSms.AppId = AppointmentId;
                            empSms.MobileNo = "7311170955";
                            smsDataLayer.OnlineConsultationPatientSms(patientSms);
                            smsDataLayer.OnlineConsultationSendSmsToEmp(empSms);
                        }
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
        #endregion

    }
}
