using HIS.Repository;
using HISWebApi.Models;
using MediSoftTech_HIS.App_Start;
using System;
using System.Data;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace MediSoftTech_HIS.Areas.Lab.Controllers
{
    public class PrintController : Controller
    {

        //GET:Lab/Print
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
            StringBuilder b = new StringBuilder();
            StringBuilder h = new StringBuilder();
            StringBuilder f = new StringBuilder();
            StringBuilder common = new StringBuilder();
            int deptCounter = 0;
            var ObsReport = ds.Tables[2].AsEnumerable().Select(y => new
            {
                SubCatName = y.Field<string>("SubCatName"),
                TestName = y.Field<string>("TestName"),
                report_content = y.Field<string>("report_content"),
                r_type = y.Field<string>("r_type"),
            }).GroupBy(x => x.SubCatName).ToList();
            foreach (var report in ObsReport)
            {
                deptCounter++;
                if (deptCounter > 1)
                {
                    b.Append("<h3 style='page-break-after: always;'><br><br></h3>");
                }

                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        b.Append("<div style='width:100%;float:left;margin-top:-12px;padding:8px'>");
                        string chandanLogo = HttpContext.Server.MapPath(@"/Content/logo/logo.png");
                        b.Append("<div style='text-align:left;width:32%;float:left'>");
                        b.Append("<img src=" + chandanLogo + " style='width:120px;margin-top:5px;' />");
                        b.Append("</div>");
                        b.Append("<div style='text-align:left;width:auto;float:left;width:43%;'>");
                        b.Append("<h2 style='font-weight:bold;margin:0'>" + dr["Hospital_Name"].ToString() + "</h2>");
                        b.Append("<span style='text-align:left;'>" + dr["Full_Address"].ToString() + "</span><br/>");
                        b.Append("<span style='text-align:left;'><b>Landline No : </b>" + dr["LandlineNo"].ToString() + "</span><br/>");
                        //b.Append("<span style='text-align:left;'><b>Email ID : </b>" + dr["EmailID"].ToString() + "</span><br/>");
                        b.Append("<span style='text-align:left;'><b>CIN No: " + dr["cin_no"].ToString() + "</b><br/><b>GSTIN : " + dr["gst_no"].ToString() + "</b></span><br/>");
                        b.Append("</div>");
                        b.Append("<div style='text-align:left;width:25%;float:left'>");
                        b.Append("<img src=" + chandanLogo + " style='width:120px;' />");
                        b.Append("</div>");
                        b.Append("</div>");
                        b.Append("<hr/>");
                    }
                }

                if (ds.Tables.Count > 0 && ds.Tables[1].Rows.Count > 0)
                {
                    foreach (DataRow dr in ds.Tables[1].Rows)
                    {
                        b.Append("<table style='width:100%;font-size:14px;text-align:left;background:#ececec;'>");
                        b.Append("<tr>");
                        b.Append("<td><b>Patient Name</b></td>");
                        b.Append("<td><b>:</b></td>");
                        b.Append("<td><b>" + dr["patient_name"].ToString() + "</b></td>");
                        b.Append("<td colspan='4'>&nbsp;</td>");
                        b.Append("<td><b>Registered On</b></td>");
                        b.Append("<td><b>:</b></td>");
                        b.Append("<td>" + dr["RegDate"].ToString() + "</td>");
                        b.Append("</tr>");
                        b.Append("<tr>");
                        b.Append("<td><b>Age/Gender</b></td>");
                        b.Append("<td><b>:</b></td>");
                        b.Append("<td>" + dr["ageInfo"].ToString() + "</td>");
                        b.Append("<td colspan='4'>&nbsp;</td>");
                        b.Append("<td><b>Collected</b></td>");
                        b.Append("<td><b>:</b></td>");
                        b.Append("<td>" + dr["RegDate"].ToString() + "</td>");
                        b.Append("</tr>");
                        b.Append("<tr>");
                        b.Append("<td><b>UHID/MR NO</b></td>");
                        b.Append("<td><b>:</b></td>");
                        b.Append("<td>" + dr["UHID"].ToString() + "</td>");
                        b.Append("<td colspan='4'>&nbsp;</td>");
                        b.Append("<td><b>Received</b></td>");
                        b.Append("<td><b>:</b></td>");
                        b.Append("<td>" + dr["RegDate"].ToString() + "</td>");
                        b.Append("</tr>");
                        b.Append("<tr>");
                        b.Append("<td><b>Visit ID</b></td>");
                        b.Append("<td><b>:</b></td>");
                        b.Append("<td>" + dr["VisitNo"].ToString() + "</td>");
                        b.Append("<td colspan='4'>&nbsp;</td>");
                        b.Append("<td><b>Reported</b></td>");
                        b.Append("<td><b>:</b></td>");
                        b.Append("<td>" + dr["RegDate"].ToString() + "</td>");
                        b.Append("</tr>");
                        b.Append("<tr>");
                        b.Append("<td><b>Ref Doctor</b></td>");
                        b.Append("<td><b>:</b></td>");
                        b.Append("<td>" + dr["ref_name"].ToString() + "</td>");
                        b.Append("<td colspan='4'>&nbsp;</td>");
                        b.Append("<td><b>Status</b></td>");
                        b.Append("<td><b>:</b></td>");
                        b.Append("<td>" + dr["VisitNo"].ToString() + "</td>");
                        b.Append("</tr>");
                        b.Append("</table>");
                    }
                }
                if (report.First().r_type != "Text")
                {
                    b.Append("<table border='0' style='width:100%;font-size:13px;border-collapse: collapse;margin-top:10px;'>");
                    b.Append("<table border='0' style='width:100%;font-size:13px;border-collapse: collapse;margin-top:10px;'>");
                    b.Append("<tr>");
                    b.Append("<th style='text-align:left;padding-left:4px;'>Test Name</th>");
                    b.Append("<th style='text-align:left;padding-right:4px;'>Result</th>");
                    b.Append("<th style='text-align:left;padding-right:4px;'>Unit</th>");
                    b.Append("<th style='text-align:left;padding-right:4px;'>Bio. Ref. Interval</th>");
                    b.Append("<th style='text-align:left;padding-right:4px;'>Method</th>");
                    b.Append("</tr>");
                }
                var ObsTest = ds.Tables[2].AsEnumerable().Where(z => z.Field<string>("SubCatName") == report.First().SubCatName).Select(y => new
                {
                    AutoTestId = y.Field<Int64>("AutoTestId"),
                    ObsCount = y.Field<Int64>("ObsCount"),
                    TestName = y.Field<string>("TestName"),
                }).GroupBy(x => x.AutoTestId).ToList();
                if (ObsTest.Count > 0)
                {
                    foreach (var dr in ObsTest)
                    {
                        Int64 AutoTestId = dr.First().AutoTestId;
                        Int64 ObsCount = dr.First().ObsCount;
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
                            method_name = y.Field<string>("method_name")
                        }).ToList();
                        b.Append("<h3 style='text-align:center;margin:0'>" + report.First().SubCatName + " REPORTS</h3>");

                        b.Append("<h4 style='background:#ddd;;padding-left:4px;margin-bottom:2px;'>" + report.First().TestName + "</h4>");
                        if (ObsCount == 1)
                        {
                            foreach (var obj1 in ObsDetail)
                            {
                                b.Append("<tr>");
                                b.Append("<td style='text-align:left;padding-left:4px;'>" + obj1.ObservationName + "</td>");
                                b.Append("<td style='text-align:left;padding-left:4px;'>" + obj1.reading + "</td>");
                                b.Append("<td style='text-align:left;padding-left:4px;'>" + obj1.result_unit + "</td>");
                                b.Append("<td style='text-align:left;padding-left:4px;'>" + obj1.RefRange + "</td>");
                                b.Append("<td style='text-align:left;padding-left:4px;'>" + obj1.method_name + "</td>");
                                b.Append("</tr>");
                            }
                        }
                        else
                        {
                            foreach (var obj1 in ObsDetail)
                            {
                                if (temp != obj1.HeaderName)
                                {
                                    b.Append("<tr>");
                                    b.Append("<td colspan='5' style='font-size:14px;text-decoration:underline;padding-left:4px;'><b>" + obj1.HeaderName + "</b></td>");
                                    b.Append("</tr>");
                                    temp = obj1.HeaderName;
                                }
                                b.Append("<tr>");
                                b.Append("<td style='text-align:left;padding-left:4px;'>" + obj1.ObservationName + "</td>");
                                b.Append("<td style='text-align:left;padding-left:4px;'>" + obj1.reading + "</td>");
                                b.Append("<td style='text-align:left;padding-left:4px;'>" + obj1.result_unit + "</td>");
                                b.Append("<td style='text-align:left;padding-left:4px;'>" + obj1.RefRange + "</td>");
                                b.Append("<td style='text-align:left;padding-left:4px;'>" + obj1.method_name + "</td>");
                                b.Append("</tr>");
                            }
                        }
                    }
                }
                b.Append("</table>");
                if (report.First().r_type == "Text")
                {
                    b.Append(report.First().report_content);
                }
            }
            f.Append("<div style='width:100%;float:left;margin-top:5px;zoom:1.5'>");
            f.Append("<div style='width:50%;float:right;margin-top:85px;text-align:center'>");
            f.Append("<hr/>Authorized Signature");
            f.Append("</div>");
            f.Append("</div>");
            f.Append("<div style='width:100%;float:left'><br/>");
            f.Append("<p><hr style='margin-top:-14px;margin-bottom:-14px;border:1px solid #000'></p>");
            var Barcode = BarcodeGenerator.GenerateBarCode("123213", 40, 70);
            f.Append("<img src='" + Barcode + "' style='width:100px;margin-top:5px;' />");
            f.Append("<p><hr style='margin-top:-14px;margin-bottom:-14px;border:1px solid #000'></p>");
            f.Append("</div>");

            pdfConverter.Header_Enabled = false;
            pdfConverter.Footer_Enabled = true;
            pdfConverter.Footer_Hight = 135;
            pdfConverter.Header_Hight = 70;
            pdfConverter.PageMarginLeft = 10;
            pdfConverter.PageMarginRight = 10;
            pdfConverter.PageMarginBottom = 10;
            pdfConverter.PageMarginTop = 10;
            pdfConverter.PageMarginTop = 10;
            pdfConverter.PageName = "A4";
            pdfConverter.PageOrientation = "Portrait";
            return pdfConverter.ConvertToPdf(h.ToString(), b.ToString(), f.ToString(), "Print-Invoice.pdf");
        }
    }
}