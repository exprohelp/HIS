using HISWebApi.Models;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace HISWebApi.Repository.Online
{
    public class PackageBooking
    {
        public dataSet HISCustomerList(ipPackageQueries ipapp)
        {
            HISWebApi.Repository.IPD.HISDBLayer obj = new IPD.HISDBLayer();
            return obj.HISCustomerList(ipapp.prm_1);
        }
        public dataSet PackageQueries(ipPackageQueries ipapp)
        {
            dataSet dsObj = new dataSet();
            using (SqlConnection con = new SqlConnection(GlobalConfig.ConStr_Hospital))
            {
                using (SqlCommand cmd = new SqlCommand("pOnline_PackageQueries", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandTimeout = 2500;
                    cmd.Parameters.Add("@PatientId", SqlDbType.VarChar, 16).Value = ipapp.PatientId;
                    cmd.Parameters.Add("@DoctorId", SqlDbType.VarChar).Value = ipapp.DoctorId;
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
                        if (ipapp.Logic=="SendCMEPaymentLink")
                        {
                            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                            {
                                string result = string.Empty;
                                try
                                {
                                    string mobile_no = ds.Tables[0].Rows[0]["MobileNo"].ToString();
                                    string meetingLink = ipapp.prm_1;
                                    Utilities.SmsClass smsService = new Utilities.SmsClass();
                                    string sms = "Dear " + ds.Tables[0].Rows[0]["ParticepantName"].ToString() + " , Kindly pay Registration Fee of Rs. " + ds.Tables[0].Rows[0]["Amount"].ToString() + "for Module 3 of "+ ds.Tables[0].Rows[0]["CMEInfo"].ToString() + ". Click on Link https://exprohelp.com/his/Online/Online/CMEPayment?pid=" + ds.Tables[0].Rows[0]["AutoId"].ToString() + "\r\n -Team Chandan";
                                    string response = smsService.SendSms(mobile_no, sms);

                                    string qry_sms = "insert into online_doctorAppSmsLog(appointmentId, mobile_no, sms, login_id, response_message) ";
                                    qry_sms += "values('" + ds.Tables[0].Rows[0]["AutoId"].ToString() + "','" + mobile_no + "', '" + sms + "', '-','" + response + "')";
                                    DBManager.QueryExecute(qry_sms, GlobalConfig.ConStr_Hospital);

                                }
                                catch (Exception ex) { result = ex.Message; }
                            }

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
        public dataSet PackageConfirmation(ipPackageOnlineConfirmation ip)
        {
            dataSet dsResult = new dataSet();
            using (SqlConnection con = new SqlConnection(GlobalConfig.ConStr_Hospital))
            {
                con.Open();
                SqlTransaction tran = con.BeginTransaction();
                using (SqlCommand cmd = new SqlCommand("pOnline_PackageConfirmation", con))
                {
                    cmd.Transaction = tran;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandTimeout = 2500;
                    cmd.Parameters.Add("@PatientId", SqlDbType.VarChar, 16).Value = ip.PatientId;
                    cmd.Parameters.Add("@doctorid", SqlDbType.VarChar, 20).Value = ip.DoctorId;
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
                                ipPackageQueries onl = new ipPackageQueries();
                                onl.PatientId = ip.PatientId;
                                onl.DoctorId = ip.DoctorId;
                                onl.prm_1 = "-";
                                onl.login_id = ip.login_id;
                                onl.Logic = "SendPaymentLink";
                                string t = BookingNotification(onl);
                                if (!t.Contains("Success"))
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
        public string  BookingNotification(ipPackageQueries ipapp)
        {
            string result = string.Empty;
            using (SqlConnection con = new SqlConnection(GlobalConfig.ConStr_Hospital))
            {
                using (SqlCommand cmd = new SqlCommand("pOnline_PackageQueries", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandTimeout = 2500;
                    cmd.Parameters.Add("@PatientId", SqlDbType.VarChar, 16).Value = ipapp.PatientId;
                    cmd.Parameters.Add("@DoctorId", SqlDbType.VarChar).Value = ipapp.DoctorId;
                    cmd.Parameters.Add("@from", SqlDbType.DateTime).Value = ipapp.fromdate;
                    cmd.Parameters.Add("@to", SqlDbType.DateTime).Value = ipapp.todate;
                    cmd.Parameters.Add("@prm_1", SqlDbType.VarChar, 50).Value = ipapp.prm_1;
                    cmd.Parameters.Add("@login_id", SqlDbType.VarChar, 50).Value = ipapp.login_id;
                    cmd.Parameters.Add("@Logic", SqlDbType.VarChar, 200).Value = "BookingDetail";
                    try
                    {
                        con.Open();
                        DataSet ds = new DataSet();
                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        da.Fill(ds);
                        if (ipapp.Logic == "SendPaymentLink")
                        {
                            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                            {
                                try
                                {
                                    string mobile_no = ds.Tables[0].Rows[0]["mobile_no"].ToString();
                                    string PatientId = ds.Tables[0].Rows[0]["PatientId"].ToString();
                                    string sms = "Your Package is Booked with Ref.No " + PatientId + ". at Chandan Hospital Kindly make the payment. http://exprohelp.com/his/Online/Online/doPaymentPackage?ptId=" + PatientId + "";
                                    Utilities.SmsClass smsService = new Utilities.SmsClass();
                                    string response = smsService.SendSms(mobile_no, sms);
                                    response = "Covid : " + response;
                                    string qry_sms = "insert into online_doctorAppSmsLog(appointmentId, mobile_no, sms, login_id, response_message) ";
                                    qry_sms += "values('" + PatientId + "','" + mobile_no + "', '" + sms + "', '-','" + response + "')";
                                    DBManager.QueryExecute(qry_sms, GlobalConfig.ConStr_Hospital);
                                    result = "Successfully Sent";
                                }
                                catch (Exception ex) { result = ex.Message; }

                            }

                        }
                        if (ipapp.Logic == "SendAppLoginLink")
                        {
                            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                            {
                                try
                                {
                                    string mobile_no = ds.Tables[0].Rows[0]["mobile_no"].ToString();
                                    string meetingLink = ipapp.prm_1;
                                    Utilities.SmsClass smsService = new Utilities.SmsClass();
                                    string sms = "Chandan Covid Care App link is  http://exprohelp.com/covidapp/index.html. Your User ID is " + ds.Tables[0].Rows[0]["PatientId"].ToString() + " and Password is " + ds.Tables[0].Rows[0]["user_psw"].ToString();
                                    string response = smsService.SendSms(mobile_no, sms);
                                    string qry_sms = "insert into online_doctorAppSmsLog(appointmentId, mobile_no, sms, login_id, response_message) ";
                                    qry_sms += "values('" + ipapp.PatientId + "','" + mobile_no + "', '" + sms + "', '-','" + response + "')";
                                    DBManager.QueryExecute(qry_sms, GlobalConfig.ConStr_Hospital);
                                    result = "Successfully Sent";

                                }
                                catch (Exception ex) { result = ex.Message; }
                            }

                        }
                    }
                    catch (SqlException sqlEx)
                    {
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
                DataSet ds1 = DBManager.GetQueryResult("select payStatus from online_doctorAppointment where appointmentid='" + obj.merchantTransactionIds + "' ", GlobalConfig.ConStr_Hospital);
                if (ds1.Tables.Count > 0 && ds1.Tables[0].Rows.Count > 0)
                {
                    SyspayStatus = ds1.Tables[0].Rows[0]["payStatus"].ToString();
                }
                foreach (PayUResult pr in payUResponse.result)
                {
                    PayUBody pbd = pr.postBackParam;
                    DataRow dr = dt.NewRow();
                    dr["AppointmentId"] = obj.merchantTransactionIds;
                    dr["paymentId"] = pbd.paymentId;
                    dr["status"] = pbd.status;
                    dr["addedon"] = pbd.addedon;
                    dr["mode"] = pbd.mode;
                    dr["amount"] = pbd.amount;
                    if (pbd.status == "success" && t.Count() == 1)
                    {
                        if (SyspayStatus == "success")
                            dr["cmdType"] = "Payment Marked";
                        else
                            dr["cmdType"] = "Update";
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
        public ChartAndTable VitalReports(ipPackageQueries ipapp)
        {
            ChartAndTable ch = new ChartAndTable();
            StringBuilder sb = new StringBuilder();
            StringBuilder sbBody = new StringBuilder();
            dataSet ds = PackageQueries(ipapp);
            string abrv = "BPH->BP Sys Higher, BPL->BP Dias Low, Tmp->Temprature, BL->Breathelessness, CT->Chest Tightness, CP->Chest Pain, COU->Cough, PHE->Phelgm,BLK->Blackout, SL->Swelling Legs, LOS->Loss Of Smells; DIZ->Dizziness";
            sb.Append("<div class='well'>" + abrv + "</div>");
            var dates = ds.ResultSet.Tables[0].AsEnumerable().Select(x => new { read_date = x.Field<string>("read_date"), PatientId = x.Field<string>("PatientId") }).Distinct();
            foreach (var d in dates)
            {              
                sb.Append("<div class='panel-group'>");
                sb.Append("<div class='panel panel-default'>");
                sb.Append("<div class='panel-heading'>");
                sb.Append("<h4 class='panel-title'>");
                sb.Append("<a  href='#collapse1'>" + d.read_date + "</a>");
                sb.Append("</h4>");
                sb.Append("</div>");
                sb.Append("<div >");
                var readingList = ds.ResultSet.Tables[0].AsEnumerable().Where(y => y.Field<string>("read_date") == d.read_date).Select(x => new
                {
                    read_Time = x.Field<string>("read_Time"),
                    bp_Sys_high = x.Field<int>("bp_Sys_high"),
                    bp_Sys_bold = x.Field<string>("bp_Sys_bold"),
                    bp_dias_low = x.Field<int>("bp_dias_low"),
                    bp_dias_bold = x.Field<string>("bp_dias_bold"),
                    temprature = x.Field<decimal>("temprature"),
                    temprature_bold = x.Field<string>("temprature_bold"),
                    spo2 = x.Field<int>("spo2"),
                    spo2_bold = x.Field<string>("spo2_bold"),
                    pulse_rate = x.Field<int>("pulse_rate"),
                    pulse_rate_bold = x.Field<string>("pulse_rate_bold"),
                    breathlessness = x.Field<string>("breathlessness"),
                    chest_tightness = x.Field<string>("chest_tightness"),
                    cough = x.Field<string>("cough"),
                    phelgm = x.Field<string>("phelgm"),
                    chest_pain = x.Field<string>("chest_pain"),
                    dizziness = x.Field<string>("dizziness"),
                    blackouts = x.Field<string>("blackouts"),                
                    swellingInLegs = x.Field<string>("swellingInLegs"),
                    LossOfSmells = x.Field<string>("LossOfSmells"),
                    doctor_remark = x.Field<string>("doctor_remark"),
                    patient_remark = x.Field<string>("patient_remark")
                });
                sbBody.Clear();
                sbBody.Append("<table class='table-bordered' id='report' style='width:100%'>");
                sbBody.Append("<tr>");
                sbBody.Append("<td style='text-align:center'>Time</td>");
                sbBody.Append("<td style='text-align:center'>BPH</td>");
                sbBody.Append("<td style='text-align:center'>BPL</td>");
                sbBody.Append("<td style='text-align:center'>Temp</td>");
                sbBody.Append("<td style='text-align:center'>SPO2</td>");
                sbBody.Append("<td style='text-align:center'>PULSE</td>");
                sbBody.Append("<td style='text-align:center'>BL</td>");
                sbBody.Append("<td style='text-align:center'>CT</td>");
                sbBody.Append("<td style='text-align:center'>CP</td>");
                sbBody.Append("<td style='text-align:center'>COU</td>");
                sbBody.Append("<td style='text-align:center'>PHE</td>");
                sbBody.Append("<td style='text-align:center'>BLK</td>");
                sbBody.Append("<td style='text-align:center'>SL</td>");
                sbBody.Append("<td style='text-align:center'>LOS</td>");
                sbBody.Append("<td style='text-align:center'>DIZ</td>");
                sbBody.Append("</tr>");
                string patient_remark = "";
                string doctor_remark = "";
                foreach (var r in readingList)
                {
                    sbBody.Append("<tr>");
                    sbBody.Append("<td style='text-align:center'>" + r.read_Time + "</td>");
                    sbBody.Append("<td style='text-align:center;color:"+ r.bp_Sys_bold + "'>" + r.bp_Sys_high + "</td>");
                    sbBody.Append("<td style='text-align:center;color:" + r.bp_dias_bold + "''>" + r.bp_dias_low + "</td>");
                    sbBody.Append("<td style='text-align:center;color:" + r.temprature_bold + "''>" + r.temprature + "</td>");
                    sbBody.Append("<td style='text-align:center;color:" + r.spo2_bold + "''>" + r.spo2 + "</td>");
                    sbBody.Append("<td style='text-align:center;color:" + r.pulse_rate_bold + "''>" + r.pulse_rate + "</td>");
                    sbBody.Append("<td style='text-align:center'>" + r.blackouts + "</td>");
                    sbBody.Append("<td style='text-align:center'>" + r.chest_tightness + "</td>");
                    sbBody.Append("<td style='text-align:center'>" + r.chest_pain + "</td>");
                    sbBody.Append("<td style='text-align:center'>" + r.cough + "</td>");
                    sbBody.Append("<td style='text-align:center'>" + r.phelgm + "</td>");
                    sbBody.Append("<td style='text-align:center'>" + r.blackouts + "</td>");
                    sbBody.Append("<td style='text-align:center'>" + r.swellingInLegs + "</td>");
                    sbBody.Append("<td style='text-align:center'>" + r.LossOfSmells + "</td>");
                    sbBody.Append("<td style='text-align:center'>" + r.dizziness + "</td>");
                    sbBody.Append("</tr>");
                    doctor_remark = r.doctor_remark;
                    patient_remark = r.patient_remark;
                }
                sbBody.Append("</table>");
                sbBody.Append("<div class='form -group'>");
                sbBody.Append("<label for='premark'>Patient Remark:</label>");
                sbBody.Append("<div id='premark'>" + patient_remark + "</div>");
                sbBody.Append("</div>");
                if (ipapp.prm_1 == "DoctorView")
                {
                    sbBody.Append("<div class='form -group' style='content-align:left'>");
                    sbBody.Append("<label for='txtDocRemark'>Doctor Remark:</label>");
                    sbBody.Append("<textarea class='form -control' rows='5' id='txtDocRemark' style='width:100%'>" + doctor_remark + "</textarea>");
                    sbBody.Append("<button data-patientid='" + d.PatientId + "' id='btnSaveDoctorRemark' data-read_date='" + d.read_date + "'  onclick='UpdateAndCloseVitalRemark(this)' class='btn btn-success'>Save</button>");
                    sbBody.Append("</div>");
                }
                else
                {
                    sbBody.Append("<div class='form -group'>");
                    sbBody.Append("<label for='remark'>Doctor Remark:</label>");
                    sbBody.Append("<div id='remark'>" + doctor_remark + "</div>");
                    sbBody.Append("</div>");
                }
                sb.Append("<div class='panel-body'>" + sbBody.ToString() + "</div>");
                sb.Append("</div>");
                sb.Append("</div>");
                sb.Append("</div>");
            }
            ch.TableHtml = sb.ToString();
            if (ds.ResultSet.Tables.Count > 1 && ds.ResultSet.Tables[1].Rows.Count > 0)
                ch.datachart = VitalChart(ds.ResultSet.Tables[1]);
            return ch;
        }
        public List<VitalChart> VitalChart(DataTable dt)
        {
            string _result = string.Empty;
            List<VitalChart> datalist = new List<VitalChart>();
            try
            {
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        VitalChart en = new VitalChart();
                        en.day = dr["x"].ToString();
                        en.bp_Sys_high = dr["bp_Sys_high"].ToString();
                        en.bp_dias_low = dr["bp_dias_low"].ToString();
                        en.temprature = dr["temprature"].ToString();
                        en.SPO2 = dr["SPO2"].ToString();
                        en.pulse = dr[3].ToString();
                        datalist.Add(en);
                    }
                }
                return datalist;
            }
            catch (Exception ex)
            {
                return datalist;
            }
        }
        public string LabReportsOfPatient(ipPackageQueries obj)
        {
            HISWebApi.Repository.IPD.HISDBLayer objDB = new IPD.HISDBLayer();
            dataSet result = objDB.LabBookingOfPatient(obj.UHID);
            var visitList = result.ResultSet.Tables[0].AsEnumerable().Select(x => new { LedgerTransactionNo = x.Field<string>("LedgerTransactionNo"), entry_date = x.Field<string>("date") }).Distinct();
            string str = ""; string temp = "";
            foreach (var t in visitList)
            {
                str += "<div class='card'>";
                str += "<div class='card-header' style='background-color:#dff3ff;'>";
                str += "<div class='row'>";
                //str += "<div class='col-md-12'>Vistit No : " + t.LedgerTransactionNo + "<br />Date&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; :" + t.entry_date+ "</div>";
                str += "<div class='col-md-12'>Vistit No : " + t.LedgerTransactionNo + "<br />Date&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; :" + t.entry_date + "</div>";
                str += "</div>";
                str += "</div>";
                //In Lab Report
                var visitItemListInLab = result.ResultSet.Tables[0].AsEnumerable().Where(y => y.Field<string>("LedgerTransactionNo") == t.LedgerTransactionNo && y.Field<int>("IsOutSource") == 0)
                .Select(x => new
                {
                    Test_ID = x.Field<string>("Test_ID"),
                    TestName = x.Field<string>("TestName"),
                    ReportStatus = x.Field<string>("ReportStatus"),
                    IsOutSource = x.Field<int>("IsOutSource"),
                });
                string Testids = string.Empty;
                foreach (var ti in visitItemListInLab)
                {
                    if (ti.ReportStatus.Contains("Ready"))
                        Testids = Testids + ti.Test_ID + ",";
                }
                if (Testids.Length > 5)
                {
                    str += "<div class='card-header'>";
                    str += "<table>";
                    str += "<tr>";
                    str += "<td style='width:70%;background-color:#dbd5d5;'>Hospital Lab Report</td>";
                    string InLabLocal_link = "http://192.168.4.10/his/Design/Lab/labreportnew.aspx?TestID=" + Testids + "&LabType=OPD&LabreportType=";
                    str += "<td style='width:15%'><a target='_blank' href='" + InLabLocal_link + "' class='btn btn-info'>Download Local<a/></td>";

                    string InLabCloud_link = "http://chandanpharma.com/his/Design/Lab/labreportnew.aspx?TestID=" + Testids + "&LabType=OPD&LabreportType=";
                    str += "<td style='width:15%'><a target='_blank' href='" + InLabCloud_link + "' class='btn btn-info'>Download Cloud<a/></td>";

                    str += "</tr>";
                    str += "</table>";
                    str += "</div>";
                    str += "<div class='card-body'>";
                    str += "<table class='table-bordered tab-content' style='width:60%'>";
                    foreach (var ti in visitItemListInLab)
                    {
                        str += "<tr>";
                        str += "<td style='width:70%;font-size:13px'>" + ti.TestName + "</td>";
                        str += "<td style='width:30%;font-size:13px;text-align:center'>" + ti.ReportStatus + "</td>";
                        str += "</tr>";
                    }
                    str += "</table>";
                    str += "</div>";
                }
                var visitItemListOutLab = result.ResultSet.Tables[0].AsEnumerable().Where(y => y.Field<string>("LedgerTransactionNo") == t.LedgerTransactionNo && y.Field<int>("IsOutSource") == 1)
               .Select(x => new
               {
                   Test_ID = x.Field<string>("Test_ID"),
                   TestName = x.Field<string>("TestName"),
                   ReportStatus = x.Field<string>("ReportStatus"),
                   IsOutSource = x.Field<int>("IsOutSource"),
               });
                Testids = string.Empty;
                foreach (var ti in visitItemListOutLab)
                {
                    if (ti.ReportStatus.Contains("Ready"))
                        Testids = Testids + ti.Test_ID + ",";
                }
                if (Testids.Length > 5)
                {
                    str += "<div class='card-header'>";
                    str += "<table>";
                    str += "<tr>";
                    str += "<td style='width:70%;background-color:#dbd5d5;'>Outsource Lab Report</td>";
                    string outsourceLocal_link = "http://192.168.0.21/Chandan/Design/Lab/LabReportNew_ShortSMS.aspx?HIS_LabNo=" + t.LedgerTransactionNo;
                    str += "<td style='width:15%'><a target='_blank' href='" + outsourceLocal_link + "' class='btn btn-info'>Download Local<a/></td>";

                    string outsourceCloud_link = "http://chandan.online/Chandan/Design/Lab/LabReportNew_ShortSMS.aspx?HIS_LabNo=" + t.LedgerTransactionNo;
                    str += "<td style='width:15%'><a target='_blank' href='" + outsourceCloud_link + "' class='btn btn-info'>Download Cloud<a/></td>";

                    str += "</tr>";
                    str += "</table>";
                    str += "</div>";
                    str += "<div class='card-body'>";
                    str += "<table class='table-bordered tab-content' style='width:60%'>";
                    foreach (var ti in visitItemListOutLab)
                    {
                        str += "<tr>";
                        str += "<td style='width:70%;font-size:13px'>" + ti.TestName + "</td>";
                        str += "<td style='width:30%;font-size:13px;text-align:center'>" + ti.ReportStatus + "</td>";
                        str += "</tr>";
                    }
                    str += "</table>";
                    str += "</div>";
                }

                //Out Lab Report

                str += "</div><br/>";
            }
            return str;
        }
        public string UpdateAndCloseVitalRemark(ipPackageQueries ip)
        {
            string result = string.Empty;
            using (SqlConnection con = new SqlConnection(GlobalConfig.ConStr_Hospital))
            {
                con.Open();
                SqlTransaction tran = con.BeginTransaction();
                using (SqlCommand cmd = new SqlCommand("pOnline_UpdateAndCloseVitalRemark", con))
                {
                    cmd.Transaction = tran;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandTimeout = 2500;
                    cmd.Parameters.Add("@PatientId", SqlDbType.VarChar, 16).Value = ip.PatientId;
                    cmd.Parameters.Add("@read_date", SqlDbType.VarChar, 100).Value = ip.read_date;
                    cmd.Parameters.Add("@remark", SqlDbType.VarChar, 500).Value = ip.remark;
                    cmd.Parameters.Add("@login_id", SqlDbType.VarChar, 50).Value = ip.login_id;
                    cmd.Parameters.Add("@DoctorId", SqlDbType.VarChar, 50).Value = ip.DoctorId;                   
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
            return result;
        }
        public string InsertCovidServices(CovidServices objBO)
        {
            string processInfo = string.Empty;
            using (SqlConnection con = new SqlConnection(GlobalConfig.ConStr_Hospital))
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand("pOnline_InsertCovidServices", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandTimeout = 2500;
                    cmd.Parameters.Add("@patient_id", SqlDbType.VarChar, 16).Value = objBO.patient_id;
                    cmd.Parameters.Add("@item_id", SqlDbType.VarChar, 10).Value = objBO.item_id;
                    cmd.Parameters.Add("@proc_date", SqlDbType.DateTime).Value = objBO.proc_date;
                    cmd.Parameters.Add("@proc_by", SqlDbType.VarChar, 50).Value = objBO.proc_by;
                    cmd.Parameters.Add("@remark", SqlDbType.VarChar, 500).Value = objBO.remark;
                    cmd.Parameters.Add("@service_status", SqlDbType.VarChar, 50).Value = objBO.service_status;
                    cmd.Parameters.Add("@Logic", SqlDbType.VarChar, 50).Value = objBO.Logic;
                    cmd.Parameters.Add("@result", SqlDbType.VarChar, 100).Value = "";
                    cmd.Parameters["@result"].Direction = ParameterDirection.InputOutput;
                    try
                    {
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