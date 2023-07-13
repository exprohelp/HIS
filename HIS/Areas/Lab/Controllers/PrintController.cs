using HIS.Repository;
using HISWebApi.Models;
using MediSoftTech_HIS.App_Start;
using MediSoftTech_HIS.Areas.Lab.Repository;
using System;
using System.Data;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace MediSoftTech_HIS.Areas.Lab.Controllers
{
    public class PrintController : Controller
    {
        public FileResult SampleTransferSheet(string DispatchNo)
        {
            PdfGenerator pdfConverter = new PdfGenerator();
            SampleDispatchInfo obj = new SampleDispatchInfo();
            obj.from = Convert.ToDateTime("1900/01/01");
            obj.to = Convert.ToDateTime("1900/01/01");
            obj.Prm1 = DispatchNo;
            obj.Logic = "GetDispatchInfoForPrint";
            HISWebApi.Models.dataSet dsResult = APIProxy.CallWebApiMethod("Lab/SampleDispatchQueries", obj);

            DataSet ds = dsResult.ResultSet;
            StringBuilder b = new StringBuilder();
            string BranchName = string.Empty;
            string To = string.Empty;
            string DispatchDate = string.Empty;
            string DeliveryBoy = string.Empty;
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    BranchName = dr["DispatchFrom"].ToString();
                    To = dr["DispatchTo"].ToString();
                    DispatchDate = dr["sample_collect_date"].ToString();
                    DeliveryBoy = dr["DeliveryBoy"].ToString();
                }
                b.Append("<h2 style='text-align:center;font-weight:bold;margin-bottom:5px;font-family: Sans-serif; '>Sample Transfer Sheet</h2>");
                b.Append("<hr/>");
                //1st Info Table 1
                b.Append("<div style='background:#eee;padding:5px;'>");
                b.Append("<table style='font-size:12px;width:100%;font-family:Sans-serif;'>");
                b.Append("<tr>");
                b.Append("<td style='width:12%;'><b>Dispatch No</b></td>");
                b.Append("<td><b>:</b></td>");
                b.Append("<td>" + DispatchNo + "</td>");
                b.Append("<td>&nbsp;</td>");
                b.Append("<td style='width:13%;'><b>BarCode</b></td>");
                b.Append("<td><b>:</b></td>");
                b.Append("<td><i" +
                    "mg src=" + BarcodeGenerator.GenerateBarCode(DispatchNo, 300, 70) + " style='width:250px'/></td>");
                b.Append("</tr>");
                b.Append("<tr>");
                b.Append("<td style='width:12%;'><b>Branch Name</b></td>");
                b.Append("<td><b>:</b></td>");
                b.Append("<td>" + BranchName + "</td>");
                b.Append("<td>&nbsp;</td>");
                b.Append("<td style='width:13%;'><b>To</b></td>");
                b.Append("<td><b>:</b></td>");
                b.Append("<td>" + To + "</td>");
                b.Append("</tr>");
                b.Append("<tr>");
                b.Append("<td style='width:13%;'><b>Dispatch Date </td>");
                b.Append("<td><b>:</b></td>");
                b.Append("<td>" + DispatchDate + "</td>");
                b.Append("<td>&nbsp;</td>");
                b.Append("<td style='width:13%;'><b>Delivery Boy</b></td>");
                b.Append("<td><b>:</b></td>");
                b.Append("<td>" + DeliveryBoy + "</td>");
                b.Append("</tr>");
                b.Append("</table>");
                b.Append("</div>");
                //Second Info Table
                b.Append("<div style='width:100%;float:left;'>");
                b.Append("<table style='width:100%;font-size:11px; font-family: Sans-serif;text-align:left;margin-top:8px' border='0'  cellspacing='0'>");
                b.Append("<tr>");
                b.Append("<th style='white-space:none;width:15%;border:1px solid #ddd;padding-left:8px'>Visit No</th>");
                b.Append("<th style='white-space:none;width:12%;border:1px solid #ddd;padding-left:8px'>BarCode No</th>");
                b.Append("<th style='white-space:none;width:40%;border:1px solid #ddd;padding-left:8px'>Patient Name</th>");
                b.Append("<th style='white-space:none;width:16%;border:1px solid #ddd;padding-left:8px'>Reg Date</th>");
                b.Append("<th style='white-space:none;width:16%;border:1px solid #ddd;padding-left:8px'>Sample Date</th>");
                b.Append("</tr>");
                int count = 0;
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    count++;
                    b.Append("<tr>");
                    b.Append("<td style='border:1px solid #ddd;padding-left:8px'>" + dr["VisitNo"].ToString() + "</td>");
                    b.Append("<td style='border:1px solid #ddd;padding-left:8px'>" + dr["barcodeNo"].ToString() + "</td>");
                    b.Append("<td style='border:1px solid #ddd;padding-left:8px'>" + dr["patient_name"].ToString() + "</td>");
                    b.Append("<td style='border:1px solid #ddd;padding-left:8px; white-space:nowrap;'>" + dr["RegDate"].ToString() + "</td>");
                    b.Append("<td style='border:1px solid #ddd;padding-left:8px; white-space:nowrap;'>" + dr["sample_collect_date"].ToString() + "</td>");
                    b.Append("</tr>");
                    b.Append("<tr>");
                    b.Append("<td colspan='5' style='white-space:none;background:#eee; font-size:8px;border:1px solid #ddd;padding-left:8px'><b>Test Name</b> : " + dr["TestName"].ToString() + "</td>");
                    b.Append("</tr>");
                }
                b.Append("</table>");
                b.Append("</div>");
            }
            pdfConverter.Header_Enabled = false;
            pdfConverter.Footer_Enabled = true;
            pdfConverter.Footer_Hight = 17;
            pdfConverter.Header_Hight = 70;
            pdfConverter.PageMarginLeft = 10;
            pdfConverter.PageMarginRight = 10;
            pdfConverter.PageMarginBottom = 10;
            pdfConverter.PageMarginTop = 10;
            pdfConverter.PageMarginTop = 10;
            pdfConverter.PageName = "A4";
            pdfConverter.PageOrientation = "Portrait";
            return pdfConverter.ConvertToPdf("-", b.ToString(), "-", "Sample-Transfer-Sheet.pdf");
        }
        public FileResult PrintConsentForm()
        {
            PdfGenerator pdfConverter = new PdfGenerator();
            LabTemplates obj = new LabTemplates();
            obj.hosp_id = "-";
            obj.subcatid = "-";
            obj.testcode = "-";
            obj.prm_1 = "-";
            obj.prm_2 = "-";
            obj.login_id = "-";
            obj.Logic = "PrintConsentForm";
            dataSet dsResult = APIProxy.CallWebApiMethod("Lab/mLabTemplateQueries", obj);

            DataSet ds = dsResult.ResultSet;
            StringBuilder h = new StringBuilder();
            StringBuilder b = new StringBuilder();
            StringBuilder f = new StringBuilder();
            string content = string.Empty;
            string PatientName = "Nitin Srivastava";
            string Gender = "Male";
            string Age = "30 year";
            string MobileNo = "9670244590";
            string Address = "Jankipuram, Lucknow, Uttar Pradesh-229801";
            string Investigation = "Jankipuram, Lucknow, Uttar Pradesh-229801";
            b.Append("<div style='width:100%;height:1005px;font-family:calibri'>");
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    b.Append("<div style='width:100%;font-size:15px;float:left;margin-top:-12px;padding:8px;font-family:calibri'>");
                    string chandanLogo = HttpContext.Server.MapPath(@"/Content/logo/logo.png");
                    b.Append("<div style='text-align:left;width:32%;float:left'>");
                    b.Append("<img src=" + chandanLogo + " style='width:180px;margin-top:5px;' />");
                    b.Append("</div>");
                    b.Append("<div style='text-align:left;width:auto;float:left;width:43%;'>");
                    b.Append("<h2 style='font-weight:bold;margin:0'>" + dr["Hospital_Name"].ToString() + "</h2>");
                    b.Append("<span style='text-align:left;'>" + dr["Full_Address"].ToString() + "</span><br/>");
                    b.Append("<span style='text-align:left;'><b>Landline No : </b>" + dr["LandlineNo"].ToString() + "</span><br/>");
                    //b.Append("<span style='text-align:left;'><b>Email ID : </b>" + dr["EmailID"].ToString() + "</span><br/>");
                    b.Append("<span style='text-align:left;'><b>CIN No: " + dr["cin_no"].ToString() + "</b></span><br/>");
                    b.Append("</div>");
                    b.Append("<div style='text-align:left;width:25%;float:left'>");
                    b.Append("<img src=" + chandanLogo + " style='width:180px;' />");
                    b.Append("</div>");
                    b.Append("</div>");
                    b.Append("<hr/>");
                }
            }
            //if (ds.Tables.Count > 0 && ds.Tables[1].Rows.Count > 0)
            //{
            //    foreach (DataRow dr in ds.Tables[1].Rows)
            //    {
            //        h.Append("<table style='width:1200px;font-size:22px;font-family:calibri;text-align:left;background:#ececec;'>");
            //        h.Append("<tr>");
            //        h.Append("<td><b>Patient Name</b></td>");
            //        h.Append("<td><b>:</b></td>");
            //        h.Append("<td><b>" + dr["patient_name"].ToString() + "</b></td>");
            //        h.Append("<td colspan='4'>&nbsp;</td>");
            //        h.Append("<td><b>Registered On</b></td>");
            //        h.Append("<td><b>:</b></td>");
            //        h.Append("<td>" + dr["RegDate"].ToString() + "</td>");
            //        h.Append("</tr>");
            //        h.Append("<tr>");
            //        h.Append("<td><b>Age/Gender</b></td>");
            //        h.Append("<td><b>:</b></td>");
            //        h.Append("<td>" + dr["ageInfo"].ToString() + "</td>");
            //        h.Append("<td colspan='4'>&nbsp;</td>");
            //        h.Append("<td><b>Collected</b></td>");
            //        h.Append("<td><b>:</b></td>");
            //        h.Append("<td>" + dr["RegDate"].ToString() + "</td>");
            //        h.Append("</tr>");
            //        h.Append("<tr>");
            //        h.Append("<td><b>UHID/MR NO</b></td>");
            //        h.Append("<td><b>:</b></td>");
            //        h.Append("<td>" + dr["UHID"].ToString() + "</td>");
            //        h.Append("<td colspan='4'>&nbsp;</td>");
            //        h.Append("<td><b>Received</b></td>");
            //        h.Append("<td><b>:</b></td>");
            //        h.Append("<td>" + dr["RegDate"].ToString() + "</td>");
            //        h.Append("</tr>");
            //        h.Append("<tr>");
            //        h.Append("<td><b>Visit ID</b></td>");
            //        h.Append("<td><b>:</b></td>");
            //        h.Append("<td>" + dr["VisitNo"].ToString() + "</td>");
            //        h.Append("<td colspan='4'>&nbsp;</td>");
            //        h.Append("<td><b>Reported</b></td>");
            //        h.Append("<td><b>:</b></td>");
            //        h.Append("<td>" + dr["RegDate"].ToString() + "</td>");
            //        h.Append("</tr>");
            //        h.Append("<tr>");
            //        h.Append("<td><b>Ref Doctor</b></td>");
            //        h.Append("<td><b>:</b></td>");
            //        h.Append("<td>" + dr["ref_name"].ToString() + "</td>");
            //        h.Append("<td colspan='4'>&nbsp;</td>");
            //        h.Append("<td><b>Status</b></td>");
            //        h.Append("<td><b>:</b></td>");
            //        h.Append("<td>" + dr["VisitNo"].ToString() + "</td>");
            //        h.Append("</tr>");
            //        h.Append("</table>");
            //    }
            //}
            if (ds.Tables.Count > 0 && ds.Tables[1].Rows.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[1].Rows)
                {
                    content = dr["t_content"].ToString();
                    foreach (var var1 in dr["var_list"].ToString().Split(','))
                    {
                        var oldString = "{<strong>" + var1 + "</strong>}";
                        var newString = "<strong>" + var1 + "</strong>";
                        if (var1 == "PatientName")
                            content = content.Replace(oldString, "<strong>" + PatientName + "</strong>");
                        if (var1 == "Gender")
                            content = content.Replace(oldString, "<strong>" + Gender + "</strong>");
                        if (var1 == "Age")
                            content = content.Replace(oldString, "<strong>" + Age + "</strong>");
                        if (var1 == "MobileNo")
                            content = content.Replace(oldString, "<strong>" + MobileNo + "</strong>");
                        if (var1 == "Address")
                            content = content.Replace(oldString, "<strong>" + Address + "</strong>");
                    }
                    b.Append(content);
                }
            }
            b.Append("<div style='width:100%;position:absolute;bottom:5px;right:10px'>");
            b.Append("<div style='width:50%;float:left;text-align:center'>");
            b.Append("<hr style='width:60%;' />Patient Signature");
            b.Append("</div>");
            b.Append("<div style='width:50%;float:right;text-align:center'>");
            b.Append("<hr style='width:60%;'/>Authorized Signature");
            b.Append("</div>");
            b.Append("</div>");
            b.Append("</div>");
            pdfConverter.Header_Enabled = false;
            pdfConverter.Footer_Enabled = true;
            pdfConverter.Footer_Hight = 50;
            pdfConverter.Header_Hight = 70;
            pdfConverter.PageMarginLeft = 10;
            pdfConverter.PageMarginRight = 10;
            pdfConverter.PageMarginBottom = 5;
            pdfConverter.PageMarginTop = 10;         
            pdfConverter.PageName = "A5";
            pdfConverter.PageOrientation = "Portrait";
            return pdfConverter.ConvertToPdf("-", b.ToString(), f.ToString(), "ConsentForm.pdf");
        }
        public FileResult LabReport(string visitNo)
        {
            PdfGenerator pdfConverter = new PdfGenerator();
            LabReporting obj = new LabReporting();
            obj.LabCode = "-";
            obj.IpOpType = "-";
            obj.ReportStatus = "-";
            obj.SubCat = "-";
            obj.VisitNo = visitNo;
            obj.BarccodeNo = "-";
            obj.TestCategory = "-";
            obj.AutoTestId = 0;
            obj.TestCode = "-";
            obj.from = Convert.ToDateTime("1900/01/01");
            obj.to = Convert.ToDateTime("1900/01/01");
            obj.Logic = "PrintLabReport";
            dataSet dsResult = APIProxy.CallWebApiMethod("sample/LabReporting_Queries", obj);
            DataSet ds = dsResult.ResultSet;
            string _result = string.Empty;
            string temp = "-";
            string deptReport = "";
            StringBuilder b = new StringBuilder();
            StringBuilder h = new StringBuilder();
            StringBuilder f = new StringBuilder();
            StringBuilder common = new StringBuilder();
            int deptCounter = 0;
            var ObsReport = ds.Tables[2].AsEnumerable().Select(y => new
            {
                SubCatName = y.Field<string>("SubCatName")
            }).GroupBy(x => x.SubCatName).ToList();
            //  b.Append("<div style='width:100%;height:2480px;border:1px solid #ddd;position: relative'>");
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    h.Append("<div style='width:1200px;font-size:25px;float:left;margin-top:-12px;padding:8px;font-family:calibri'>");
                    string chandanLogo = HttpContext.Server.MapPath(@"/Content/logo/logo.png");
                    h.Append("<div style='text-align:left;width:32%;float:left'>");
                    h.Append("<img src=" + chandanLogo + " style='width:180px;margin-top:5px;' />");
                    h.Append("</div>");
                    h.Append("<div style='text-align:left;width:auto;float:left;width:43%;'>");
                    h.Append("<h2 style='font-weight:bold;margin:0'>" + dr["Hospital_Name"].ToString() + "</h2>");
                    h.Append("<span style='text-align:left;'>" + dr["Full_Address"].ToString() + "</span><br/>");
                    h.Append("<span style='text-align:left;'><b>Landline No : </b>" + dr["LandlineNo"].ToString() + "</span><br/>");
                    //b.Append("<span style='text-align:left;'><b>Email ID : </b>" + dr["EmailID"].ToString() + "</span><br/>");
                    h.Append("<span style='text-align:left;'><b>CIN No: " + dr["cin_no"].ToString() + "</b></span><br/>");
                    h.Append("</div>");
                    h.Append("<div style='text-align:left;width:25%;float:left'>");
                    h.Append("<img src=" + chandanLogo + " style='width:180px;' />");
                    h.Append("</div>");
                    h.Append("</div>");
                    h.Append("<hr/>");
                }
            }
            if (ds.Tables.Count > 0 && ds.Tables[1].Rows.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[1].Rows)
                {
                    h.Append("<table style='width:1200px;font-size:22px;font-family:calibri;text-align:left;background:#ececec;'>");
                    h.Append("<tr>");
                    h.Append("<td><b>Patient Name</b></td>");
                    h.Append("<td><b>:</b></td>");
                    h.Append("<td><b>" + dr["patient_name"].ToString() + "</b></td>");
                    h.Append("<td colspan='4'>&nbsp;</td>");
                    h.Append("<td><b>Registered On</b></td>");
                    h.Append("<td><b>:</b></td>");
                    h.Append("<td>" + dr["RegDate"].ToString() + "</td>");
                    h.Append("</tr>");
                    h.Append("<tr>");
                    h.Append("<td><b>Age/Gender</b></td>");
                    h.Append("<td><b>:</b></td>");
                    h.Append("<td>" + dr["ageInfo"].ToString() + "</td>");
                    h.Append("<td colspan='4'>&nbsp;</td>");
                    h.Append("<td><b>Collected</b></td>");
                    h.Append("<td><b>:</b></td>");
                    h.Append("<td>" + dr["RegDate"].ToString() + "</td>");
                    h.Append("</tr>");
                    h.Append("<tr>");
                    h.Append("<td><b>UHID/MR NO</b></td>");
                    h.Append("<td><b>:</b></td>");
                    h.Append("<td>" + dr["UHID"].ToString() + "</td>");
                    h.Append("<td colspan='4'>&nbsp;</td>");
                    h.Append("<td><b>Received</b></td>");
                    h.Append("<td><b>:</b></td>");
                    h.Append("<td>" + dr["RegDate"].ToString() + "</td>");
                    h.Append("</tr>");
                    h.Append("<tr>");
                    h.Append("<td><b>Visit ID</b></td>");
                    h.Append("<td><b>:</b></td>");
                    h.Append("<td>" + dr["VisitNo"].ToString() + "</td>");
                    h.Append("<td colspan='4'>&nbsp;</td>");
                    h.Append("<td><b>Reported</b></td>");
                    h.Append("<td><b>:</b></td>");
                    h.Append("<td>" + dr["RegDate"].ToString() + "</td>");
                    h.Append("</tr>");
                    h.Append("<tr>");
                    h.Append("<td><b>Ref Doctor</b></td>");
                    h.Append("<td><b>:</b></td>");
                    h.Append("<td>" + dr["ref_name"].ToString() + "</td>");
                    h.Append("<td colspan='4'>&nbsp;</td>");
                    h.Append("<td><b>Status</b></td>");
                    h.Append("<td><b>:</b></td>");
                    h.Append("<td>" + dr["VisitNo"].ToString() + "</td>");
                    h.Append("</tr>");
                    h.Append("</table>");
                }
            }

            var deptName = "BIOCHEMISTRY";
            foreach (var report in ObsReport)
            {
                deptReport = report.First().SubCatName;
                deptCounter++;
                if (deptCounter > 1)
                {
                    b.Append("<h3 style='page-break-after: always;'><br><br></h3>");
                }


                var ObsTest = ds.Tables[2].AsEnumerable().Where(z => z.Field<string>("SubCatName") == report.First().SubCatName).Select(y => new
                {
                    AutoTestId = y.Field<Int64>("AutoTestId"),
                    ObsCount = y.Field<Int64>("ObsCount"),
                    TestSeqNo = y.Field<Int64>("TestSeqNo"),
                    TestName = y.Field<string>("TestName"),
                    report_content = y.Field<string>("report_content"),
                    r_type = y.Field<string>("r_type"),
                    samp_type = y.Field<string>("samp_type"),
                    Interpretation = y.Field<string>("Interpretation")
                }).OrderBy(y => y.TestSeqNo).ToList();
                if (ObsTest.Count > 0)
                {

                    b.Append("<hr/>");
                    b.Append("<h3 style='text-align:center;margin-top:5px;'>" + report.First().SubCatName + " REPORTS</h3>");
                    var tempCatName = "";
                    foreach (var dr in ObsTest)
                    {
                        if (tempCatName != dr.r_type)
                        {
                            if (dr.r_type != "Text")
                            {
                                b.Append("<table border='0' style='width:100%;font-size:13px;border-collapse: collapse;margin-top:-10px;'>");
                                b.Append("<tr>");
                                b.Append("<th style='width:35%;text-align:left;padding-left:4px;'>Test Name</th>");
                                b.Append("<th style='width:18%;text-align:left;padding-right:4px;'>Result</th>");
                                b.Append("<th style='width:2%;text-align:left;padding-right:4px;'></th>");
                                b.Append("<th style='width:10%;text-align:left;padding-right:4px;'>Unit</th>");
                                b.Append("<th style='width:25%;text-align:left;padding-right:4px;'>Bio. Ref. Interval</th>");
                                b.Append("<th style='width:10%;text-align:left;padding-right:4px;'>Method</th>");
                                b.Append("</tr>");
                            }
                            tempCatName = dr.r_type;
                        }
                        Int64 AutoTestId = dr.AutoTestId;
                        Int64 ObsCount = dr.ObsCount;
                        var ObsDetail = ds.Tables[3].AsEnumerable().Where(x => x.Field<Int64>("AutoTestId") == AutoTestId).Select(y => new
                        {
                            ObservationName = y.Field<string>("ObservationName"),
                            reading = y.Field<string>("reading"),
                            ab_flag = y.Field<string>("ab_flag"),
                            result_unit = y.Field<string>("result_unit"),
                            RefRange = y.Field<string>("RefRange"),
                            ObsSeqNo = y.Field<Int64>("ObsSeqNo"),
                            sample_type = y.Field<string>("sample_type"),
                            HeaderName = y.Field<string>("HeaderName"),
                            method_name = y.Field<string>("method_name"),
                            test_comment = y.Field<string>("test_comment")

                        }).ToList();

                        if (ObsCount == 1)
                        {

                            foreach (var obj1 in ObsDetail)
                            {
                                b.Append("<tr>");
                                b.Append("<td style='width:35%;text-align:left;padding-left:4px;'>" + obj1.ObservationName + ", <span style='font-size:10px'>" + dr.samp_type + "</span>" + "</td>");
                                if (obj1.ab_flag == "L" || obj1.ab_flag == "H")
                                    b.Append("<td style='width:18%;text-align:left;padding-left:4px;'><b>" + obj1.reading + "</b></td>");
                                else
                                    b.Append("<td style='width:18%;text-align:left;padding-left:4px;'>" + obj1.reading + "</td>");


                                b.Append("<td style='width:2%;text-align:left;padding-left:4px;font-size:11px;margin-right:5px'>" + obj1.ab_flag + "</td>");
                                b.Append("<td style='width:10%;text-align:left;padding-left:4px;font-size:11px'>" + obj1.result_unit + "</td>");
                                b.Append("<td style='width:25%;text-align:left;padding-left:4px;font-size:11px'>" + obj1.RefRange + "</td>");
                                b.Append("<td style='width:10%;text-align:left;padding-left:4px;'>" + obj1.method_name + "</td>");
                                b.Append("</tr>");
                                if (!string.IsNullOrEmpty(obj1.test_comment))
                                {
                                    b.Append("<tr>");
                                    b.Append("<td colspan='6'>" + obj1.test_comment + "</td>");
                                    b.Append("</tr>");
                                }

                                if (!string.IsNullOrEmpty(dr.Interpretation))
                                {
                                    b.Append("<tr>");
                                    b.Append("<td colspan='6'><b>Test Interpretation : </b></td>");
                                    b.Append("</tr>");
                                    b.Append("<tr>");
                                    b.Append("<td colspan='6'>" + dr.Interpretation + "</td>");
                                    b.Append("</tr>");
                                }
                            }
                        }
                        else
                        {
                            b.Append("<tr>");
                            b.Append("<td colspan='6' style='padding-left:10px;line-height:10px'></td>");
                            b.Append("</tr>");
                            b.Append("<tr>");
                            b.Append("<td colspan='6' style='background:#ddd;text-align:left;padding-left:4px;'>" + dr.TestName + ", <span style='font-size:10px'>" + dr.samp_type + "</span>" + "</td>");
                            b.Append("</tr>");

                            foreach (var obj1 in ObsDetail)
                            {

                                if (temp != obj1.HeaderName)
                                {
                                    b.Append("<tr>");
                                    b.Append("<td colspan='6' style='font-size:14px;text-decoration:underline;padding-left:4px;'><b>" + obj1.HeaderName + "</b></td>");
                                    b.Append("</tr>");
                                    temp = obj1.HeaderName;
                                }

                                b.Append("<tr>");
                                b.Append("<td style='width:35%;text-align:left;padding-left:4px;'>" + obj1.ObservationName + "</td>");
                                if (obj1.ab_flag == "L" || obj1.ab_flag == "H")
                                    b.Append("<td style='width:18%;text-align:left;padding-left:4px;'><b>" + obj1.reading + "</b></td>");
                                else
                                    b.Append("<td style='width:18%;text-align:left;padding-left:4px;'>" + obj1.reading + "</td>");


                                b.Append("<td style='width:2%;text-align:left;padding-left:4px;font-size:11px;margin-right:5px'>" + obj1.ab_flag + "</td>");
                                b.Append("<td style='width:10%;text-align:left;padding-left:4px;font-size:11px'>" + obj1.result_unit + "</td>");
                                b.Append("<td style='width:25%;text-align:left;padding-left:4px;font-size:11px'>" + obj1.RefRange + "</td>");
                                b.Append("<td style='width:10%;text-align:left;padding-left:4px;'>" + obj1.method_name + "</td>");
                                b.Append("</tr>");
                                if (obj1.test_comment != null || obj1.test_comment != "")
                                {
                                    b.Append("<tr>");
                                    b.Append("<td colspan='6'>" + obj1.test_comment + "</td>");
                                    b.Append("</tr>");
                                }

                            }
                            if (ObsDetail.Count > 1)
                            {
                                if (dr.Interpretation != null || dr.Interpretation != "")
                                {
                                    b.Append("<tr>");
                                    b.Append("<td colspan='6'>" + dr.Interpretation + "</td>");
                                    b.Append("</tr>");
                                }

                                b.Append("<tr>");
                                b.Append("<td colspan='6'><hr/></td>");
                                b.Append("</tr>");
                            }

                        }
                        if (dr.r_type == "Text")
                        {
                            b.Append("<tr>");
                            b.Append("<td colspan='6' style='text-align:left;padding-left:4px;'>" + dr.TestName + "</td>");
                            b.Append("</tr>");

                            b.Append("<tr>");
                            b.Append("<td colspan='6' style='text-align:left;padding-left:4px;'>" + dr.report_content + "</td>");
                            b.Append("</tr>");
                        }
                    }
                    b.Append("</table>");
                }

                // if (deptName != report.First().SubCatName)
                // {
                //b.Append("<div style='width:100%;float:left;margin-top:5px;position:absolute;bottom:10px;right:10px'>");
                //b.Append("<div style='width:50%;float:right;margin-top:75px;text-align:center'>");
                //b.Append("<hr/>Authorized Signature");
                //b.Append("</div>");
                //b.Append("</div>");
                //  deptName = report.First().SubCatName;
                // }
            }
            f.Append("<div style='width:100%;float:right;text-align:center;margin-bottom:5px'>");
            f.Append("<table style='width:1200px;font-size:22px;font-family:calibri;text-align:center; border-collapse: collapse;border: 1px solid black;'>");
            f.Append("<tr>");
            f.Append("<td style='width:400px;'>-</td>");
            f.Append("<td style='width:400px;'>-</td>");
            f.Append("<td style='width:400px;'>");
            f.Append("<img src='http://exprohelp.com/HospDoc//Profile/Signature/DR00033_Sign.jpg' style='height:80px'><hr>");
            f.Append("Dr. Manmeet Singh<br>MBBS, MD (Pathalogy)");
            f.Append("</td>");
            f.Append("</tr>");
            f.Append("</table>");


            pdfConverter.Header_Enabled = true;
            pdfConverter.Footer_Enabled = true;
            pdfConverter.Footer_Hight = 110;
            pdfConverter.Header_Hight = 150;
            pdfConverter.PageMarginLeft = 10;
            pdfConverter.PageMarginRight = 10;
            pdfConverter.PageMarginBottom = 5;
            pdfConverter.PageMarginTop = 10;
            pdfConverter.PageName = "A4";
            pdfConverter.PageOrientation = "Portrait";
            return pdfConverter.ConvertToPdf(h.ToString(), b.ToString(), f.ToString(), "Print-Invoice.pdf");
        }
        public FileResult PrintLabReport(string visitNo, string SubCat)
        {
            GenReport rep = new GenReport();
            return rep.PrintLabReport(visitNo, SubCat);
        }
    }
}