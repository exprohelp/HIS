using HiQPdf;
using HISWebApi.Models;
using MediSoftTech_HIS.App_Start;
using System;
using System.Data;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace MediSoftTech_HIS.Areas.Lab.Repository
{
    public class GenReport
    {
        dataSet dsResult = new dataSet();
        string _Deptname = string.Empty;
        public FileResult PrintLabReport(string visitNo)
        {
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
            dsResult = APIProxy.CallWebApiMethod("sample/LabReporting_Queries", obj);

            var DeptList = dsResult.ResultSet.Tables[2].AsEnumerable().Select(y => new
            {
                SubCatName = y.Field<string>("SubCatName")
            }).ToList().GroupBy(x => x.SubCatName);
            PdfDocument repDocument = new PdfDocument();
            repDocument.SerialNumber = "g8vq0tPn‐5c/q4fHi‐8fq7rbOj‐sqO3o7uy‐t6Owsq2y‐sa26urq6";
            foreach (var dept in DeptList)
            {
                _Deptname = dept.First().SubCatName;
                //PdfDocument doc1 = new PdfDocument();
                PdfPage page1 = repDocument.AddPage(PdfPageSize.A4, new PdfDocumentMargins(10), PdfPageOrientation.Portrait);
                string HtmlBody = GetBodyHTML(_Deptname, dsResult.ResultSet);

                PdfHtml htmlBody = new PdfHtml(HtmlBody, null);
                htmlBody.BrowserWidth = 800;
                htmlBody.RenderWebFonts = true;
                htmlBody.PageCreatingEvent += new PdfPageCreatingDelegate(htmlToPdfConverter_PageCreatingEvent);
                htmlBody.FontEmbedding = false;
                htmlBody.ImagesCutAllowed = false;
                //htmlBody.FitDestHeight = true;
                page1.Layout(htmlBody);
                //Setting Footer at last page Document
                SetFooter(repDocument.Pages[repDocument.Pages.Count - 1]);

            }
            byte[] pdfdata = repDocument.WriteToMemory();
            FileResult fileResult = new FileContentResult(pdfdata, "application/pdf");
            return fileResult;
        }
        public void htmlToPdfConverter_PageCreatingEvent(PdfPageCreatingParams eventParams)
        {
            PdfPage page1 = eventParams.PdfPage;
            //Set Header
            SetHeader(page1);
        }
        private void SetHeader(PdfPage pdfPage)
        {
            if (pdfPage != null)
            {
                pdfPage.CreateHeaderCanvas(180);
                string StrhtmlHeader = GetHeaderHTML(_Deptname, dsResult.ResultSet);
                PdfHtml headerHtml = new PdfHtml(0, 0, StrhtmlHeader, null);
                pdfPage.Header.Layout(headerHtml);
            }
        }
        private void SetFooter(PdfPage pdfPage)
        {
            if (pdfPage != null)
            {
                pdfPage.CreateFooterCanvas(100);
                string StrhtmlFooter = GetFooterHTML(_Deptname, dsResult.ResultSet);
                PdfHtml footerHtml = new PdfHtml(0, 0, StrhtmlFooter, null);
                pdfPage.Footer.Layout(footerHtml);
            }
        }
        private string GetBodyHTML(string DepartmentName, DataSet ds)
        {
            string _result = string.Empty;
            string temp = "-";
            StringBuilder b = new StringBuilder();
            var ObsTest = ds.Tables[3].AsEnumerable().Where(z => z.Field<string>("SubCatName") == DepartmentName).Select(y => new
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
                var tempCatName = "";
                foreach (var dr in ObsTest)
                {
                    if (tempCatName != dr.r_type)
                    {
                        if (dr.r_type != "Text")
                        {
                            b.Append("<table border='0' style='width:100%;font-size:15px; border-collapse: collapse;margin-top:-10px;'>");
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
                    var ObsDetail = ds.Tables[4].AsEnumerable().Where(x => x.Field<Int64>("AutoTestId") == AutoTestId).Select(y => new
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
                            b.Append("<td style='width:10%;text-align:left;padding-left:4px;font-size:12px'>" + obj1.method_name + "</td>");
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

            }
            b.Append("</table>");
            string t = b.ToString();
            return b.ToString();
        }
        private string GetHeaderHTML(string DepartmentName, DataSet ds)
        {
            StringBuilder h = new StringBuilder();
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    h.Append("<div style='width:1200px;font-size:25px;float:left;margin-top:-12px;padding:8px;font-family:calibri'>");
                    string chandanLogo = "/Content/logo/logo.png"; // HttpContext.Server.MapPath(@"/Content/logo/logo.png");
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
                //DataRow[] drList = ds.Tables[1].Select("SubCatName = " + DepartmentName);
                DataRow[] drList = ds.Tables[1].Select(string.Format("SubCatName ='{0}' ", DepartmentName));
                foreach (DataRow dr in drList)
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
                    h.Append("<td>" + dr["sample_collect_date"].ToString() + "</td>");
                    h.Append("</tr>");
                    h.Append("<tr>");
                    h.Append("<td><b>UHID/MR NO</b></td>");
                    h.Append("<td><b>:</b></td>");
                    h.Append("<td>" + dr["UHID"].ToString() + "</td>");
                    h.Append("<td colspan='4'>&nbsp;</td>");
                    h.Append("<td><b>Received</b></td>");
                    h.Append("<td><b>:</b></td>");
                    h.Append("<td>" + dr["LabReceivedDate"].ToString() + "</td>");
                    h.Append("</tr>");
                    h.Append("<tr>");
                    h.Append("<td><b>Visit ID</b></td>");
                    h.Append("<td><b>:</b></td>");
                    h.Append("<td>" + dr["VisitNo"].ToString() + "</td>");
                    h.Append("<td colspan='4'>&nbsp;</td>");
                    h.Append("<td><b>Reported</b></td>");
                    h.Append("<td><b>:</b></td>");
                    h.Append("<td>" + dr["ApprovedDate"].ToString() + "</td>");
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

            h.Append("<hr/>");
            h.Append("<h3 style='text-align:center;margin-top:5px;'>" + DepartmentName + " REPORTS</h3>");
            return h.ToString();
        }
        private string GetFooterHTML(string DepartmentName, DataSet ds)
        {
            string _result = string.Empty;
            string temp = "-";
            StringBuilder f = new StringBuilder();
            var SignInfo = ds.Tables[2].AsEnumerable().Where(z => z.Field<string>("SubCatName") == DepartmentName).Select(y => new
            {
                SubCatName = y.Field<string>("SubCatName"),
                ReportApprDoctor = y.Field<string>("ReportApprDoctor"),
                degree = y.Field<string>("degree"),
                SignVirtualPath = y.Field<string>("SignVirtualPath"),
            }).ToList();
            f.Append("<div style='width:100%;float:right;text-align:center;margin-bottom:5px'>");
            f.Append("<table style='width:1200px;font-size:22px;font-family:calibri;text-align:center; border-collapse: collapse;border: 1px solid black;'>");
            f.Append("<tr>");
            foreach (var obj1 in SignInfo)
            {
                if (SignInfo.Count == 1)
                {
                    f.Append("<td style='width:400px;'></td>");
                    f.Append("<td style='width:400px;'></td>");
                    f.Append("<td style='width:400px;'>");
                    f.Append("<img src='http://192.168.0.11/HospDoc/" + obj1.SignVirtualPath + "' style='height:60px'><br>");
                    f.Append("Dr. " + obj1.ReportApprDoctor + "<br>" + obj1.degree + "");
                    f.Append("</td>");
                }
                else
                {
                    f.Append("<td style='width:400px;'>");
                    f.Append("<img src='http://192.168.0.11/HospDoc/" + obj1.SignVirtualPath + "' style='height:60px'><br>");
                    f.Append("Dr. " + obj1.ReportApprDoctor + "<br>" + obj1.degree + "");
                    f.Append("</td>");
                }
               
            }
            f.Append("</tr>");
            f.Append("</table>");
            return f.ToString();
        }
    }
}