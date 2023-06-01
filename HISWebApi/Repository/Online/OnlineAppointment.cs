using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Http;
using HISWebApi.Models;
using HISWebApi.Repository;
using HISWebApi.Repository.IPD;
using Newtonsoft.Json;
using RestSharp;
namespace HISWebApi.Repository.Hospital
{
    public class OnlineAppointment
    {
        HISDBLayer hisDBLayer = new HISDBLayer();
        public dataSet HIS_OPDCall(HospitalBO ipapp)
        {
            return hisDBLayer.HIS_OPDCall(ipapp);
        }
        public string HIS_OPDCancelAndCallDone(HospitalBO ipapp)
        {
            return hisDBLayer.HIS_OPDCancelAndCallDone(ipapp);
        }
        public dataSet DoctorAppQueries(ipOnlineAppQry ipapp)
        {
            dataSet dsObj = new dataSet();
            using(SqlConnection con = new SqlConnection(GlobalConfig.ConStr_Hospital))
            {
                using (SqlCommand cmd = new SqlCommand("pOnline_doctorAppQueries", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandTimeout = 2500;
                    cmd.Parameters.Add("@unit_id", SqlDbType.VarChar,10).Value = ipapp.unit_id;
                    cmd.Parameters.Add("@appointment_id", SqlDbType.VarChar,16).Value = ipapp.appointment_id;
                    cmd.Parameters.Add("@from", SqlDbType.DateTime).Value = ipapp.fromdate;
                    cmd.Parameters.Add("@to", SqlDbType.DateTime).Value = ipapp.todate;
                    cmd.Parameters.Add("@prm_1", SqlDbType.VarChar, 50).Value = ipapp.prm_1;
                    cmd.Parameters.Add("@login_id", SqlDbType.VarChar, 50).Value = ipapp.login_id;
                    cmd.Parameters.Add("@Logic", SqlDbType.VarChar, 200).Value = ipapp.Logic;
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
        public dataSet AppointmentConfirmation(ipOnlineConfirmation ip)
        {
            dataSet dsResult = new dataSet();
            using (SqlConnection con = new SqlConnection(GlobalConfig.ConStr_Hospital))
            {
                con.Open();
                SqlTransaction tran = con.BeginTransaction();
                using (SqlCommand cmd = new SqlCommand("pOnline_AppointmentConfirmation", con))
                {
                    cmd.Transaction = tran;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandTimeout = 2500;
                    cmd.Parameters.Add("@AppointmentId", SqlDbType.VarChar, 16).Value = ip.appointment_id;
                    cmd.Parameters.Add("@doctorid", SqlDbType.VarChar, 20).Value = ip.DoctorId;
                    cmd.Parameters.Add("@appointment_datetime", SqlDbType.DateTime).Value = ip.appointment_datetime;
                    cmd.Parameters.Add("@AppType", SqlDbType.VarChar, 10).Value = ip.AppType;
                    cmd.Parameters.Add("@Confirm_remark", SqlDbType.VarChar, 200).Value = ip.Confirm_remark;
                    cmd.Parameters.Add("@payment_link", SqlDbType.VarChar).Value = ip.payment_link;
                    cmd.Parameters.Add("@meeting_link", SqlDbType.VarChar).Value = ip.meeting_link;
                    cmd.Parameters.Add("@login_id", SqlDbType.VarChar, 10).Value = ip.login_id;
                    cmd.Parameters.Add("@Logic", SqlDbType.VarChar, 50).Value = ip.Logic;
                    cmd.Parameters.Add("@result", SqlDbType.VarChar, 100).Value = "";
                    cmd.Parameters["@result"].Direction = ParameterDirection.InputOutput;
                    try
                    {
                        cmd.ExecuteNonQuery();
                        string processInfo = (string)cmd.Parameters["@result"].Value.ToString();
                        if (processInfo.Contains("Success"))
                        {
                            tran.Commit();
                            dsResult.ResultSet = null;
                            dsResult.Msg = processInfo;
                            if (ip.Logic == "TakeConfirmation")
                            {
                                ipOnlineAppQry onl = new ipOnlineAppQry();
                                onl.appointment_id = ip.appointment_id;
                                onl.unit_id = ip.unit_id;
                                onl.fromdate = "1900/01/01";
                                onl.todate = "1900/01/01";
                                onl.prm_1 = "-";
                                onl.login_id = ip.login_id;
                                onl.Logic = "SendPaymentLink";
                                string t = BookingNotification(onl);
                                if(!t.Contains("Success"))
                                dsResult.Msg = t;
                            }
                        }
                        else
                        {
                            tran.Rollback();
                            dsResult.ResultSet = null;
                            dsResult.Msg = processInfo;
                        }
                    }
                    catch (SqlException sqlEx)
                    {
                        tran.Rollback();
                        dsResult.ResultSet = null;
                        dsResult.Msg = sqlEx.Message;
                    }
                    finally { con.Close(); }

                }
            }
            return dsResult;
        }
        public dataSet Online_MarkPayment(ipOnlinePayment ip)
        {
            dataSet dsResult = new dataSet();
            using (SqlConnection con = new SqlConnection(GlobalConfig.ConStr_Hospital))
            {
                con.Open();
                SqlTransaction tran = con.BeginTransaction();
                using (SqlCommand cmd = new SqlCommand("pOnline_MarkPayment", con))
                {
                    cmd.Transaction = tran;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandTimeout = 2500;
                    cmd.Parameters.Add("@appointment_id", SqlDbType.VarChar, 16).Value = ip.appointment_id;
                    cmd.Parameters.Add("@pay_responseKey", SqlDbType.VarChar, 100).Value = ip.pay_responseKey;
                    cmd.Parameters.Add("@paymentId", SqlDbType.VarChar, 50).Value = ip.paymentId;
                    cmd.Parameters.Add("@payTnxId", SqlDbType.VarChar, 50).Value = ip.pay_responseTnxId;
                    cmd.Parameters.Add("@payStatus", SqlDbType.VarChar, 50).Value = ip.payStatus;
                    cmd.Parameters.Add("@payAmount", SqlDbType.Decimal).Value = ip.payAmount;
                    cmd.Parameters.Add("@login_id", SqlDbType.VarChar, 10).Value = "-";
                    cmd.Parameters.Add("@Logic", SqlDbType.VarChar, 50).Value = ip.Logic;
                    cmd.Parameters.Add("@result", SqlDbType.VarChar, 100).Value = "";
                    cmd.Parameters["@result"].Direction = ParameterDirection.InputOutput;
                    try
                    {
                        cmd.ExecuteNonQuery();
                        string processInfo = (string)cmd.Parameters["@result"].Value.ToString();
                        if (processInfo.Contains("Success"))
                        {
                            tran.Commit();
                            dsResult.ResultSet = null;
                            dsResult.Msg = processInfo;

                            Utilities.SmsClass smsService = new Utilities.SmsClass();
                            string sms = "Payment done of booking id :" + ip.appointment_id + ". Please take care. Chandan Hospital";
                            string response = smsService.SendSms(CareContacts.PackageCareNo, sms);
                            string qry_sms1 = "insert into online_doctorAppSmsLog(appointmentId, mobile_no, sms, login_id, response_message) ";
                            qry_sms1 += "values('" + ip.appointment_id + "','"+ CareContacts.PackageCareNo + "', '" + sms + "', '-','" + response + "')";
                            DBManager.QueryExecute(qry_sms1, GlobalConfig.ConStr_Hospital);
                        }
                        else
                        {
                            tran.Rollback();
                            dsResult.ResultSet = null;
                            dsResult.Msg = "Roll back";
                        }
                    }
                    catch (SqlException sqlEx)
                    {
                        tran.Rollback();
                        dsResult.ResultSet = null;
                        dsResult.Msg = sqlEx.Message;
                    }
                    finally { con.Close(); }

                }
            }
            return dsResult;
        }
        public string  BookingNotification(ipOnlineAppQry ipapp)
        {
            string result = string.Empty; ;
            using (SqlConnection con = new SqlConnection(GlobalConfig.ConStr_Hospital))
            {
                using (SqlCommand cmd = new SqlCommand("pOnline_doctorAppQueries", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandTimeout = 2500;
                    cmd.Parameters.Add("@unit_id", SqlDbType.VarChar, 10).Value = "CH01";
                    cmd.Parameters.Add("@appointment_id", SqlDbType.VarChar, 16).Value = ipapp.appointment_id;
                    cmd.Parameters.Add("@from", SqlDbType.DateTime).Value = ipapp.fromdate;
                    cmd.Parameters.Add("@to", SqlDbType.DateTime).Value = ipapp.todate;
                    cmd.Parameters.Add("@prm_1", SqlDbType.VarChar, 50).Value = "-";
                    cmd.Parameters.Add("@login_id", SqlDbType.VarChar, 50).Value = ipapp.login_id;
                    cmd.Parameters.Add("@Logic", SqlDbType.VarChar, 200).Value = "BookingDetail";
                    try
                    {
                        con.Open();
                        DataSet ds = new DataSet();
                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        da.Fill(ds);
                        con.Close();
                        if (ipapp.Logic == "BookingConfirmation")
                        {
                            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                            {
                                try
                                {
                                    string mobile_no = ds.Tables[0].Rows[0]["mobile_no"].ToString();
                                    string AppointmentId = ds.Tables[0].Rows[0]["AppointmentId"].ToString();
                                    Utilities.SmsClass smsService = new Utilities.SmsClass();

                                    string sms = "Dear Patient, Your Appointment Id is " + AppointmentId + ". soon our Executive will call you. Team Chandan";
                                    string response = smsService.SendSms(mobile_no, sms);

                                    string qry_sms = "insert into online_doctorAppSmsLog(appointmentId, mobile_no, sms, login_id, response_message) ";
                                    qry_sms += "values('" + AppointmentId + "','" + mobile_no + "', '" + sms + "', '-','" + response + "')";
                                    DBManager.QueryExecute(qry_sms, GlobalConfig.ConStr_Hospital);

                                    string sms1 = "New Online doctor appointment booking  at Chandan Hospital with booking id : " + AppointmentId + ". Please take care.";
                                    string response1 = smsService.SendSms("7311170955", sms1);
                               
                                    string qry_sms1 = "insert into online_doctorAppSmsLog(appointmentId, mobile_no, sms, login_id, response_message) ";
                                    qry_sms1 += "values('" + AppointmentId + "','7311170955', '" + sms1 + "', '-','" + response1 + "')";
                                    DBManager.QueryExecute(qry_sms1, GlobalConfig.ConStr_Hospital);

                                    result = "Successfully Sent";
                                }
                                catch (Exception ex)
                                {
                                    result = ex.Message;
                                }
                            }
                        }
                        if (ipapp.Logic == "SendPaymentLink")
                        {
                            if(ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                            {
                                try
                                {
                                    string mobile_no = ds.Tables[0].Rows[0]["mobile_no"].ToString();
                                    string AppointmentId = ds.Tables[0].Rows[0]["AppointmentId"].ToString();
                                    string app_datetime = ds.Tables[0].Rows[0]["app_date"].ToString();

                                    string sms = "Your App. date : " + app_datetime + " and ref.No " + AppointmentId + ". Pay. http://exprohelp.com/ChandanMIS/Hospital/Online/doPayment?appid=" + AppointmentId + " Chandan Hospital, Lucknow.";

                                    Utilities.SmsClass smsService = new Utilities.SmsClass();
                                    string response=smsService.SendSms(mobile_no, sms);
                                    string qry="update online_doctorAppointment set payment_linkStatus='" + response + "' where AppointmentId='" + AppointmentId + "' ";
                                    DBManager.QueryExecute(qry, GlobalConfig.ConStr_Hospital);
                              
                                    string qry_sms = "insert into online_doctorAppSmsLog(appointmentId, mobile_no, sms, login_id, response_message) ";
                                    qry_sms += "values('"+ AppointmentId + "','" + mobile_no + "', '" + sms + "', '-','" + response + "')";
                                    DBManager.QueryExecute(qry_sms, GlobalConfig.ConStr_Hospital);
                                    result = "Successfully Sent";
                                }
                                catch (Exception ex) { result = ex.Message; }
                              
                            }
                           
                        }
                        if (ipapp.Logic == "SendMeetingLink")
                        {
                            if(ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                            {
                                try
                                {
                                    string mobile_no = ds.Tables[0].Rows[0]["mobile_no"].ToString();
                                    string meetingLink = ipapp.prm_1;
                                    Utilities.SmsClass smsService = new Utilities.SmsClass();
                                    string sms = "Your Meeting Link is :" + meetingLink;
                                    string response = smsService.SendSms(mobile_no, sms);
                                    string qry = "update online_doctorAppointment set meeting_link='" + meetingLink + "' where AppointmentId='" + ipapp.appointment_id + "' ";
                                    DBManager.QueryExecute(qry, GlobalConfig.ConStr_Hospital);
                                    string qry_sms = "insert into online_doctorAppSmsLog(appointmentId, mobile_no, sms, login_id, response_message) ";
                                    qry_sms += "values('" + ipapp.appointment_id + "','" + mobile_no + "', '" + sms + "', '-','" + response + "')";
                                    DBManager.QueryExecute(qry_sms, GlobalConfig.ConStr_Hospital);
                                    result = "Successfully Sent";

                                }
                                catch (Exception ex) { result = ex.Message; }
                            }
                        }
                        if (ipapp.Logic == "SendPrescription")
                        {
                            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                            {
                                try
                                {
                                    string mobile_no = ds.Tables[0].Rows[0]["mobile_no"].ToString();
                                    string app_datetime = ds.Tables[0].Rows[0]["app_date"].ToString();
                                    string PrescriptionLink = ds.Tables[0].Rows[0]["PrescriptionPath"].ToString();
                                    string meetingLink = ipapp.prm_1;
                                    Utilities.SmsClass smsService = new Utilities.SmsClass();
                                    string sms = "Your Prescription Link at Chandon Hospital for appointment date :" + app_datetime + " is :" + PrescriptionLink;
                                    //string response = "test";
                                    string response = smsService.SendSms(mobile_no, sms);
                                    string qry = "update online_doctorAppointment set IsPrescriptionAttached=1 where AppointmentId='" + ipapp.appointment_id + "' ";
                                    DBManager.QueryExecute(qry, GlobalConfig.ConStr_Hospital);
                                    string qry_sms = "insert into online_doctorAppSmsLog(appointmentId, mobile_no, sms, login_id, response_message) ";
                                    qry_sms += "values('" + ipapp.appointment_id + "','" + mobile_no + "', '" + sms + "', '-','" + response + "')";
                                    DBManager.QueryExecute(qry_sms, GlobalConfig.ConStr_Hospital);
                                    result = "Successfully Sent";
                                } catch (Exception ex) { result = ex.Message; }
                            }

                        }

                    }
                    catch (SqlException sqlEx)
                    {
                        result = sqlEx.Message;
                    }
                    finally { con.Close(); }
                    return result;
                }
            }
        }
        public dataSet CallWebApiPayUMoney(string methodRoute, ipTransaction obj)
        {
            dataSet ds = new dataSet();
            var client = new RestClient("https://www.payumoney.com/payment/op/getPaymentResponse?merchantKey=E3Ghpv8l&merchantTransactionIds=" + obj.merchantTransactionIds + "");
            client.Timeout = -1;
            var request = new RestRequest(Method.POST);
            request.AddHeader("authorization", "uFROiqo2VnqSsIUCNji3+xRpiqM9ciu0gd+kuIuA9lc=");
            IRestResponse response = client.Execute(request);
            ipPayUResponse payUResponse = JsonConvert.DeserializeObject<ipPayUResponse>(response.Content, new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore,
                MissingMemberHandling = MissingMemberHandling.Ignore,
                Formatting = Formatting.None,
                DateFormatHandling = DateFormatHandling.IsoDateFormat,
                FloatParseHandling = FloatParseHandling.Decimal
            });
            DataTable dt = new DataTable();
            dt.Columns.Add("AppointmentId", typeof(string));
            dt.Columns.Add("paymentId", typeof(string));
            dt.Columns.Add("status", typeof(string));
            dt.Columns.Add("addedon", typeof(string));
            dt.Columns.Add("amount", typeof(string));
            dt.Columns.Add("mode", typeof(string));
            dt.Columns.Add("cmdType", typeof(string));
            if (payUResponse.result != null)
            {

                var t = payUResponse.result.AsEnumerable().Where(x => x.postBackParam.status == "success");
                int ct = 1;
                string SyspayStatus = "-";
                DataSet ds1 = DBManager.GetQueryResult("select payStatus from online_doctorAppointment where appointmentid='"+ obj.merchantTransactionIds + "' ",GlobalConfig.ConStr_Hospital);
                if(ds1.Tables.Count > 0 && ds1.Tables[0].Rows.Count > 0)
                {
                    SyspayStatus = ds1.Tables[0].Rows[0]["payStatus"].ToString();
                }
                foreach (PayUResult pr in payUResponse.result)
                {
                    PayUBody pbd = pr.postBackParam;
                    DataRow dr = dt.NewRow();
                    dr["AppointmentId"] = obj.merchantTransactionIds;
                    dr["paymentId"] = pbd.mihpayid;
                    dr["status"] = pbd.status;
                    dr["addedon"] = pbd.addedon;
                    dr["mode"] = pbd.mode;
                    dr["amount"] = pbd.amount;
                    if (pbd.status == "success" && t.Count() == 1)
                    {
                        if(SyspayStatus =="success")
                            dr["cmdType"] ="Payment Marked";
                        else
                            dr["cmdType"] ="Update";
                    }
                    if (pbd.status == "success" && t.Count() > 1)
                    {
                        if (ct == 1)
                        {
                            if (SyspayStatus == "success")
                                dr["cmdType"] = "Payment Marked";
                            else
                                dr["cmdType"] = "Update";
                        }
                        else
                        {
                            dr["cmdType"] = "Refund";
                        }
                        ct++;
                    }
                    dt.Rows.Add(dr);
                }
                DataSet ds2 = new DataSet();
                ds2.Tables.Add(dt);
                ds.ResultSet = ds2;
                ds.Msg = "Found";
            }
            else
            {
                ds.ResultSet = null;
                ds.Msg = "No Payment Fount";
            }
            return ds;
        }
        public string  UpdatePaymentStatus(ipUpdatePayStatus ip)
        {
            string result = string.Empty;
            if(ip.command == "Update")
            {
                using (SqlConnection con = new SqlConnection(GlobalConfig.ConStr_Hospital))
                {
                    con.Open();
                    SqlTransaction tran = con.BeginTransaction();
                    using (SqlCommand cmd = new SqlCommand("pOnline_doctorFeePayment", con))
                    {
                        string[] t = ip.StrValues.Split('|');
                        cmd.Transaction = tran;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandTimeout = 2500;
                        cmd.Parameters.Add("@appointment_id", SqlDbType.VarChar, 16).Value = ip.AppointmentId;
                        cmd.Parameters.Add("@pay_responseKey", SqlDbType.VarChar, 100).Value = "-";
                        cmd.Parameters.Add("@paymentId", SqlDbType.VarChar, 50).Value = t[0];
                        cmd.Parameters.Add("@payTnxId", SqlDbType.VarChar, 50).Value = ip.AppointmentId;
                        cmd.Parameters.Add("@payStatus", SqlDbType.VarChar, 50).Value = t[1];
                        cmd.Parameters.Add("@payAmount", SqlDbType.Decimal).Value =Convert.ToDecimal(t[2]);
                        cmd.Parameters.Add("@login_id", SqlDbType.VarChar, 10).Value = ip.loginId;
                        cmd.Parameters.Add("@Logic", SqlDbType.VarChar, 50).Value = ip.Logic;
                        cmd.Parameters.Add("@result", SqlDbType.VarChar, 100).Value = "";
                        cmd.Parameters["@result"].Direction = ParameterDirection.InputOutput;
                        try
                        {
                            cmd.ExecuteNonQuery();
                            string processInfo = (string)cmd.Parameters["@result"].Value.ToString();
                            if (processInfo.Contains("Success"))
                            {
                                tran.Commit();
                                result = processInfo;
                            }
                            else
                            {
                                result = "Roll back";
                            }
                        }
                        catch (SqlException sqlEx)
                        {
                            tran.Rollback();
                            result = sqlEx.Message;
                        }
                        finally { con.Close(); }

                    }
                }
            }
            if (ip.command == "Refund")
            {
                try
                {
                    string[] t = ip.StrValues.Split('|');
                    var client = new RestClient("https://www.payumoney.com/treasury/merchant/refundPayment?merchantKey=E3Ghpv8l&paymentId=" + t[0] + "&refundAmount=" + t[2] + "");
                    client.Timeout = -1;
                    var request = new RestRequest(Method.POST);
                    request.AddHeader("authorization", "uFROiqo2VnqSsIUCNji3+xRpiqM9ciu0gd+kuIuA9lc=");
                    IRestResponse response = client.Execute(request);
                    ipPayRefundResponse payURefundResponse = JsonConvert.DeserializeObject<ipPayRefundResponse>(response.Content, new JsonSerializerSettings
                    {
                        NullValueHandling = NullValueHandling.Ignore,
                        MissingMemberHandling = MissingMemberHandling.Ignore,
                        Formatting = Formatting.None,
                        DateFormatHandling = DateFormatHandling.IsoDateFormat,
                        FloatParseHandling = FloatParseHandling.Decimal
                    });
                    result = payURefundResponse.message;
                }
                catch (Exception ex) { result = ex.Message; }
            }
            return result;
        }
        public string  SaveOnlinePrescription(string AppointmentId, string virtual_path,string physical_path)
        {
            string processInfo = string.Empty;
            using (SqlConnection con = new SqlConnection(GlobalConfig.ConStr_Hospital))
            {
                using (SqlCommand cmd = new SqlCommand("pInsert_OnlineAppointmentPrescription", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandTimeout = 2500;
                    cmd.Parameters.Add("@AppointmentId", SqlDbType.VarChar, 20).Value = AppointmentId;
                    cmd.Parameters.Add("@virtual_path", SqlDbType.VarChar, 150).Value = virtual_path;
                    cmd.Parameters.Add("@physical_path", SqlDbType.VarChar, 150).Value = physical_path;
                    cmd.Parameters.Add("@DoctorName", SqlDbType.VarChar, 100).Value = "-";
                    cmd.Parameters.Add("@docSource", SqlDbType.VarChar, 50).Value = "ByHospital";
                    cmd.Parameters.Add("@Logic", SqlDbType.VarChar, 20).Value = "Insert";
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
                        processInfo = sqlEx.Message;
                    }
                    finally { con.Close(); }
                }
                return processInfo;
            }
        }
        public List<TransactionDetail> CallWebApiPayUMoneyByDate(out string ProcessInfo, ipTransaction obj)
        {
            List<TransactionDetail> trnLists = new List<TransactionDetail>();
            try
            {
                byte[] hash;
                string d = "DCqJFDrL" + "|" + "get_Transaction_Details" + "|" + obj.from + "|" + "8sIJ4ogpyz";
                var datab = Encoding.UTF8.GetBytes(d);
                using (SHA512 shaM = new SHA512Managed())
                {
                    hash = shaM.ComputeHash(datab);
                }
                string hasString = GetStringFromHash(hash);
                var client = new RestClient("https://info.payu.in/merchant/postservice?form=2");
                client.Timeout = -1;
                var request = new RestRequest(Method.POST);
                request.AddHeader("Accept", "application/json");
                request.AddHeader("Content-Type", "application/x-www-form-urlencoded");
                request.AddParameter("key", "DCqJFDrL");
                request.AddParameter("command", "get_Transaction_Details");
                request.AddParameter("var1", obj.from);
                request.AddParameter("var2", obj.to);
                request.AddParameter("hash", hasString);
                IRestResponse response = client.Execute(request);

                iPayUPaymentList payUResponse = JsonConvert.DeserializeObject<iPayUPaymentList>(response.Content, new JsonSerializerSettings
                {
                    NullValueHandling = NullValueHandling.Ignore,
                    MissingMemberHandling = MissingMemberHandling.Ignore,
                    Formatting = Formatting.None,
                    DateFormatHandling = DateFormatHandling.IsoDateFormat,
                    FloatParseHandling = FloatParseHandling.Decimal
                });
                trnLists = payUResponse.Transaction_details;
                ProcessInfo = "Success";
            }
            catch (Exception ex)
            {
                ProcessInfo = ex.Message;
            }
            return trnLists;
        }
        private static string GetStringFromHash(byte[] hash)
        {
            StringBuilder result = new StringBuilder();
            for (int i = 0; i < hash.Length; i++)
            {
                result.Append(hash[i].ToString("X2").ToLower());
            }
            return result.ToString();
        }
    }
}