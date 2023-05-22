﻿using HIS.Repository;
using MediSoftTech_HIS.App_Start;
using MediSoftTech_HIS.Repository;
using System;
using System.Data;
using System.Text;
using System.Web.Mvc;
using static HISWebApi.Models.IPDBO;

namespace MediSoftTech_HIS.Areas.IPD.Controllers
{
    public class PrintController : Controller
    {
        public FileResult RequisitionSlip(string indent_no, string Logic = "IndentDetail")
        {
            PdfGenerator pdfConverter = new PdfGenerator();
            ipIndentReport obj = new ipIndentReport();
            obj.indent_no = indent_no;
            obj.Logic = Logic;
            HISWebApi.Models.dataSet dsResult = APIProxy.CallWebApiMethod("IPDNursing/IPOP_IndentQueries", obj);
            DataSet ds = dsResult.ResultSet;

            string _result = string.Empty;
            StringBuilder b = new StringBuilder();
            StringBuilder h = new StringBuilder();
            string pName = "";
            string IndentBy = "";
            string dept_name = "";
            string IndentDate = "";
            string IPDNo = "";
            string Panel = "";

            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                pName = dr["pt_name"].ToString();
                IndentBy = dr["IndentBy"].ToString();
                dept_name = dr["dept_name"].ToString();
                IndentDate = dr["cr_date"].ToString();
                IPDNo = dr["ipop_no"].ToString();
                Panel = dr["panel_name"].ToString();
            }
            b.Append("<h1 style='text-align:center;font-weight:bold;text-decoration: underline;'>Requisition Slip</h1>");
            b.Append("<table style='background:#f5f5f5;width:100%;font-size:15px;text-align:left;border:0px solid #dcdcdc;margin-bottom:-15px'>");
            b.Append("<tr>");
            b.Append("<td style='width:12%;'><b>Indent No.</b></td>");
            b.Append("<td style='width:2%;'><b>:</b></td>");
            b.Append("<td style='width:8%;text-aligh:left'>" + indent_no + "</td>");
            b.Append("<td style='width:1%;'>&nbsp;</td>");
            b.Append("<td style='width:10%;'><b>Indent By</b></td>");
            b.Append("<td style='width:2%;'><b>:</b></td>");
            b.Append("<td style='width:27%;'>" + IndentBy + "(" + dept_name + ")" + "</td>");
            b.Append("<td style='width:1%;'>&nbsp;</td>");
            b.Append("<td style='width:12%;'><b>Indent Date</b></td>");
            b.Append("<td style='width:2%;'><b>:</b></td>");
            b.Append("<td style='width:22%;'>" + IndentDate + "</td>");
            b.Append("</tr>");
            b.Append("<tr>");
            b.Append("<td style='width:10%;'><b>IPD No.</b></td>");
            b.Append("<td style='width:2%;'><b>:</b></td>");
            b.Append("<td style='width:8%;text-aligh:left'>" + IPDNo + "</td>");
            b.Append("<td style='width:1%;'>&nbsp;</td>");
            b.Append("<td style='width:14%;'><b>Patient Name</b></td>");
            b.Append("<td style='width:2%;'><b>:</b></td>");
            b.Append("<td style='width:27%;'>" + pName + "</td>");
            b.Append("<td style='width:1%;'>&nbsp;</td>");
            b.Append("<td style='width:12%;'><b>Panel</b></td>");
            b.Append("<td style='width:2%;'><b>:</b></td>");
            b.Append("<td style='width:20%;'>" + Panel + "</td>");

            b.Append("</tr>");
            b.Append("</table>");

            b.Append("<p>To,<br/>&nbsp;Chandan Hospital</p>");
            b.Append("<b>Subject : Please Issue Following Items.</b><br/>");
            b.Append("<span>Dear Sir,<br/>Kindly Arrange to Procure Following Items at the earliast for</span><br/>");
            b.Append("<br/>");
            b.Append("<table border='1' style='width:100%;font-size:11px;border-collapse: collapse'>");
            b.Append("<tr>");
            b.Append("<th style='width:40%;text-align:left;padding-left:4px;'>Item Name</th>");
            b.Append("<th style='white-space: nowrap;text-align:right;'>Qty</th>");
            b.Append("<th style='text-align:right;padding-right:4px;'>Issue Qty</th>");
            b.Append("<th style='white-space: nowrap;text-align:right'>Balance Qty</th>");
            b.Append("<th style='text-align:right;padding-right:4px;'>Type</th>");
            //b.Append("<th style='text-align:right;padding-right:4px;'>Mfd. Name</th>");
            b.Append("</tr>");
            //Body			
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    b.Append("<tr>");
                    b.Append("<td style='text-align:left;white-space: nowrap;padding-left:4px;'>" + dr["item_name"].ToString() + "</td>");
                    b.Append("<td style='text-align:right;white-space: nowrap;padding-right:4px;'>" + dr["qty"].ToString() + "</td>");
                    b.Append("<td style='text-align:right;padding-right:4px;'>" + dr["issue_qty"].ToString() + "</td>");
                    b.Append("<td style='text-align:right;white-space: nowrap;padding-right:4px;'>" + dr["balance"].ToString() + "</td>");
                    b.Append("<td style='text-align:right;padding-right:4px;white-space: nowrap'>" + dr["ReqType"] + "</td>");
                    b.Append("</tr>");
                }
            }
            b.Append("</table>");
            //b.Append("<div style='float:right;'>");
            //b.Append("<b>From</b><br/>");
            //b.Append(IndentBy + "<br/>");
            //b.Append(dept_name);
            //b.Append("</div>");

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
            return pdfConverter.ConvertToPdf(h.ToString(), b.ToString(), "-", "Print-Dispatch-Invoice.pdf");
        }
        public FileResult BloodRequisitionForm(string IndentNo)
        {
            PdfGenerator pdfConverter = new PdfGenerator();
            IPDInfo obj = new IPDInfo();
            obj.IPDNo = IndentNo;
            obj.from = "1900/01/01";
            obj.to = "1900/01/01";
            obj.Logic = "BloodRequisitionFormPrint";
            HISWebApi.Models.dataSet dsResult = APIProxy.CallWebApiMethod("IPDNursingService/IPD_PatientQueries", obj);
            DataSet ds = dsResult.ResultSet;

            string _result = string.Empty;
            StringBuilder b = new StringBuilder();
            StringBuilder h = new StringBuilder();

            string headerImageFile = HttpContext.Server.MapPath(@"~/Content/logo/logo.png");
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                b.Append("<div style='width:99.50%;float:left;padding:8px'>");//Main Div Start
                b.Append("<div style='width:100%;float:left;padding:8px'>");
                b.Append("<div style='width:30%;float:left;'>");
                b.Append("<img src=" + headerImageFile + " style='width:130px;margin-top:-15px' />");
                b.Append("</div>");
                b.Append("<div style='width:70%;float:right;'>");
                b.Append("<h2 style='font-weight:bold;margin:0;text-align:left'>BLOOD REQUISITION FORM</h2>");
                b.Append("<h3 style='margin:0;text-align:left'>Indent No : IND000023</h3>");
                b.Append("</div>");
                b.Append("</div>");
                b.Append("<hr/>");

                b.Append("<div style='width:100%;float:left;font-size:15px;padding:2px 0;line-height:18px;height:auto;border:1px solid #000;'>");

                b.Append("<div style='width:100%;float:left;'>");
                b.Append("<div style='width:38%;padding:3px;float:left;'>");
                b.Append("<label style='font-size:13px'><b>Patient Name : </b> " + dr["PatientName"].ToString() + "</label>");
                b.Append("</div>");
                b.Append("<div style='width:59%;padding:2px;float:right;'>");
                b.Append("<label><b>Hospital : </b></label>");
                b.Append("<label style='white-space:pre;padding:3px;font-size:12px'>CHANDAN HOSPITAL (Private) Reg. No. 0915700044</label>");
                b.Append("&nbsp;<label style='white-space:pre;padding:3px'><b>Other : </b> OPD</label>");
                b.Append("</div>");
                b.Append("</div>");

                b.Append("<div style='width:100%;float:left;'>");
                b.Append("<div style='width:17%;padding:3px;float:left;'>");
                b.Append("<label><b>Age : </b> " + dr["ageInfo"].ToString() + "</label>");
                b.Append("</div>");
                b.Append("<div style='width:17%;padding:3px;float:left;'>");
                b.Append("<label><b>Gender : </b> " + dr["Gender"].ToString() + "</label>");
                b.Append("</div>");
                b.Append("<div style='width:63%;padding:2px;float:right;'>");
                b.Append("<label><b>Address : </b>" + dr["address"].ToString() + "</label>");
                b.Append("</div>");
                b.Append("</div>");

                b.Append("<div style='width:100%;float:left;'>");
                b.Append("<div style='width:24%;padding:3px;float:left;'>");
                b.Append("<label><b>UHID : </b> " + dr["UHID"].ToString() + "</ label>");
                b.Append("</div>");
                b.Append("<div style='width:24%;padding:3px;float:left;'>");
                b.Append("<label><b>IPD No. : </b> " + dr["IPOPNo"].ToString() + "</label>");
                b.Append("</div>");
                b.Append("<div style='width:24%;padding:3px;float:left;'>");
                b.Append("<label><b>Ward : </b> " + dr["RoomType"].ToString() + "</label>");
                b.Append("</div>");
                b.Append("<div style='width:24%;padding:3px;float:left;'>");
                b.Append("<label><b>Bed : </b> " + dr["BedNo"].ToString() + "</label>");
                b.Append("</div>");
                b.Append("</div>");

                b.Append("</div>");

                b.Append("<div style='width:100%;float:left;font-size:15px;padding:2px 0;line-height:18px;height:auto;margin-top:15px;border:1px solid #000;'>");

                b.Append("<div style='width:100%;float:left;'>");
                b.Append("<div style='width:45%;padding:3px;float:left;'>");
                b.Append("<label><b>Consultant : </b> " + dr["Consultant"].ToString() + "</label>");
                b.Append("</div>");
                b.Append("<div style='width:52%;padding:2px;float:right;'>");
                b.Append("<label><b>Diagnosis : </b> " + dr["Diagnosis"].ToString() + "</label>");
                b.Append("</div>");
                b.Append("</div>");

                b.Append("<div style='width:100%;float:left;'>");
                b.Append("<div style='width:80%;padding:3px;float:left;'>");
                b.Append("<label><b>Indication for Transfusion : </b> " + dr["TransfusionIndicator"].ToString() + "</label>");
                b.Append("</div>");
                b.Append("<div style='width:17%;padding:2px;float:right;'>");
                b.Append("<label><b>Hb % : </b> " + dr["HbPerc"].ToString() + "</label>");
                b.Append("</div>");
                b.Append("</div>");

                b.Append("<div style='width:100%;float:left;font-size:13px'>");
                b.Append("<div style='width:20%;padding:3px;float:left;'>");
                b.Append("<label><b>Previous Transfusion : </b> " + dr["PreviousTransfusion"].ToString() + "</label>");
                b.Append("</div>");
                b.Append("<div style='width:13%;padding:2px;float:left;'>");
                b.Append("<label><b>Donor No : </b> " + dr["DonorNo"].ToString() + "</label>");
                b.Append("</div>");
                b.Append("<div style='width:13%;padding:2px;float:left;'>");
                b.Append("<label><b>ABO Rh : </b> " + dr["AboRH"].ToString() + "</label>");
                b.Append("</div>");
                b.Append("<div style='width:21%;padding:2px;float:left;'>");
                b.Append("<label><b>Date Transfused : </b>" + dr["dateTransfuse"].ToString() + "</label>");
                b.Append("</div>");
                b.Append("<div style='width:30%;padding:2px;float:right;'>");
                b.Append("<label><b>Reaction if any : </b> " + dr["ReactionAny"].ToString() + "</label>");
                b.Append("</div>");
                b.Append("</div>");

                b.Append("</div>");
                if (dr["IsPregnant"].ToString() == "Y")
                {
                    b.Append("<div style='width:100%;float:left;font-size:15px;padding:2px 0;line-height:18px;height:auto;margin-top:15px;border:1px solid #000;'>");
                    b.Append("<div style='width:100%;float:left;'>");
                    b.Append("<label style='width:100%;float:left;padding:3px;'><b>Obstetrics History for Female Patient Only</b></label>");
                    b.Append("<div style='width:15%;padding:3px;float:left;'>");
                    b.Append("<label><b>Pregnancy : </b> " + dr["IsPregnant"].ToString() + "</label>");
                    b.Append("</div>");
                    b.Append("<div style='width:40%;padding:2px;float:left;'>");
                    b.Append("<label><b>H/O of haemolytic disease in new born : </b> " + dr["haemolytic_disease"].ToString() + "</label>");
                    b.Append("</div>");
                    b.Append("<div style='width:40%;padding:2px;float:left;'>");
                    b.Append("<label><b>H/O of still birth/miscarriage : </b> " + dr["miscarriage"].ToString() + "</label>");
                    b.Append("</div>");
                    b.Append("</div>");
                    b.Append("</div>");
                }
                b.Append("<div style='width:100%;float:left;font-size:15px;padding:2px 0;line-height:18px;height:auto;margin-top:15px;border:1px solid #000;'>");
                b.Append("<div style='width:100%;float:left;'>");
                b.Append("<label style='width:100%;float:left;padding:3px;'><b>Component and Quantity</b></label>");
                b.Append("<div style='width:95%;padding:3px;float:left;display:flex'>");
                foreach (DataRow dr1 in ds.Tables[1].Rows)
                {
                    b.Append("<label style='margin-left:6px;'><b>" + dr1["AliasName"].ToString() + " :</b> " + dr1["IndentQty"].ToString() + "</label>");
                }
                b.Append("</div>");
                b.Append("</div>");
                b.Append("</div>");

                b.Append("<div style='width:100%;float:left;font-size:15px;padding:2px 0;line-height:18px;height:auto;margin-top:15px;border:1px solid #000;'>");
                b.Append("<div style='width:100%;float:left;'>");
                b.Append("<label style='width:100%;float:left;padding:3px;'><b>Requirement Time</b></label>");

                b.Append("<div style='width:20%;padding:3px;float:left;'>");
                b.Append("<label><b>" + dr["ReqType"].ToString() + "</b></label>");
                b.Append("</div>");
                b.Append("<div style='width:36%;padding:2px;float:left;'>");
                b.Append("<label>" + dr["ReqType_Remark1"].ToString() + "</label>");
                b.Append("</div>");
                b.Append("<div style='width:36%;padding:2px;float:left;'>");
                b.Append("<label><b>Supply Time : </b> " + dr["ReqType_Remark2"].ToString() + "</label>");
                b.Append("</div>");
                b.Append("</div>");

                b.Append("</div>");

                b.Append("<div style='width:100%;float:left;font-size:15px;padding:2px 0;line-height:18px;height:auto;margin-top:15px;border:1px solid #000;'>");
                b.Append("<div style='width:100%;float:left;'>");
                b.Append("<label style='width:100%;float:left;padding:3px;'>I certify that I have personally collected tha blood sample after identification of patients ID No. and name. I have expalined the necessity of blood transfusion and risks associated with it to patients/relatives and taken informed conscent. </label>");

                b.Append("<div style='width:17%;padding:3px;float:left;'>");
                b.Append("<label>Date : <b>" + dr["IndentDate"].ToString().Split(' ')[0] + "</b></label>");
                b.Append("</div>");
                b.Append("<div style='width:15%;padding:2px;float:left;'>");
                b.Append("<label>Time : <b>" + dr["IndentDate"].ToString().Split(' ')[1] + "</b></label>");
                b.Append("</div>");
                b.Append("<div style='width:35%;padding:2px;float:left;'>");
                b.Append("<label>Doctor Name : <b> " + dr["DoctorName"].ToString() + "</b></label>");
                b.Append("</div>");
                b.Append("<div style='width:27%;padding:2px;float:left;'>");
                b.Append("<label><b>Sign of Doctor: </b> <img src=" + dr["SignVirtualPath"].ToString() + " style='width:109px' /></label>");
                b.Append("</div>");
                b.Append("</div>");
                b.Append("</div>");

                b.Append("<label style='margin-top:15px;width:100%;float:left;padding:3px;text-align:center'><b>TO BE FILLED BY DEPARTMENT OF TRANSFUSION MEDICINE</b></label>");

                b.Append("<div style='width:100%;float:left;font-size:15px;padding:5px 0;line-height:18px;height:auto;margin-top:5px;border:1px solid #000;'>");
                b.Append("<div style='width:100%;float:left;font-size:14px'>");

                b.Append("<div style='width:25%;padding:2px;float:left;display:flex;margin-top:4px'>");
                b.Append("<label><b>Booking No :</b></label>");
                b.Append("<label style='padding:3px 50px;height:16px;border:1px solid #000;margin-left:2px'></label>");
                b.Append("</div>");

                b.Append("<div style='width:20%;padding:2px;float:left;display:flex;margin-top:4px'>");
                b.Append("<label><b>Date :</b></label>");
                b.Append("<label style='padding:3px 50px;height:16px;border:1px solid #000;margin-left:2px'></label>");
                b.Append("</div>");

                b.Append("<div style='width:18%;padding:2px;float:left;display:flex;margin-top:4px'>");
                b.Append("<label><b>Time :</b></label>");
                b.Append("<label style='padding:3px 40px;height:16px;border:1px solid #000;margin-left:2px'></label>");
                b.Append("</div>");

                b.Append("<div style='width:35%;padding:2px;float:left;display:flex;margin-top:4px'>");
                b.Append("<label><b>Blood Group (ABO & Rh) :</b></label>");
                b.Append("<label style='padding:3px 40px;height:16px;border:1px solid #000;margin-left:2px'></label>");
                b.Append("</div>");

                b.Append("</div>");

                b.Append("<div style='width:100%;float:left;font-size:14px'>");
                b.Append("<div style='width:97%;padding:2px;float:left;display:flex;margin-top:4px'>");
                b.Append("<label><b>Mode of Adjustment :</b></label>");
                b.Append("<label style='padding:3px 300px;height:16px;border:1px solid #000;'></label>");
                b.Append("</div>");
                b.Append("</div>");

                b.Append("<div style='width:100%;float:left;font-size:14px'>");

                b.Append("<div style='width:32%;padding:2px;float:left;display:flex;margin-top:4px'>");
                b.Append("<label><b>Issue on date :</b></label>");
                b.Append("<label style='padding:3px 50px;height:16px;border:1px solid #000;margin-left:2px'></label>");
                b.Append("</div>");

                b.Append("<div style='width:32%;padding:2px;float:left;display:flex;margin-top:4px'>");
                b.Append("<label><b>Booking No :</b></label>");
                b.Append("<label style='padding:3px 50px;height:16px;border:1px solid #000;margin-left:2px'></label>");
                b.Append("</div>");

                b.Append("<div style='width:32%;padding:2px;float:left;display:flex;margin-top:4px'>");
                b.Append("<label><b>Donor No :</b></label>");
                b.Append("<label style='padding:3px 40px;height:16px;border:1px solid #000;margin-left:2px'></label>");
                b.Append("</div>");

                b.Append("</div>");

                b.Append("<div style='width:100%;float:left;font-size:14px'>");

                b.Append("<div style='width:32%;padding:2px;float:left;display:flex;margin-top:4px'>");
                b.Append("<label><b>Amount Collected :</b></label>");
                b.Append("<label style='padding:3px 50px;height:16px;border:1px solid #000;margin-left:2px'></label>");
                b.Append("</div>");

                b.Append("<div style='width:32%;padding:2px;float:left;display:flex;margin-top:4px'>");
                b.Append("<label><b>Bill No :</b></label>");
                b.Append("<label style='padding:3px 50px;height:16px;border:1px solid #000;margin-left:2px'></label>");
                b.Append("</div>");

                b.Append("<div style='width:32%;padding:2px;float:left;display:flex;margin-top:4px'>");
                b.Append("<label><b>Bill Date :</b></label>");
                b.Append("<label style='padding:3px 40px;height:16px;border:1px solid #000;margin-left:2px'></label>");
                b.Append("</div>");

                b.Append("</div>");

                b.Append("</div>");
                b.Append("<label style='width:100%;font-size:13px;float:left;padding:3px;text-align:left;margin-top:10px'><b>NOTE : ALL FIELDS MUST BE FILLED. PLEASE WRITE N/A WHERE NOT APPLICABLE.</b></label>");

                b.Append("</div>");//Main Div Close
            }

            pdfConverter.Header_Enabled = false;
            pdfConverter.Footer_Enabled = true;
            pdfConverter.Footer_Hight = 17;
            pdfConverter.Header_Hight = 70;
            pdfConverter.PageMarginLeft = 5;
            pdfConverter.PageMarginRight = 5;
            pdfConverter.PageMarginBottom = 10;
            pdfConverter.PageMarginTop = 10;
            pdfConverter.PageMarginTop = 10;
            pdfConverter.PageName = "A4";
            pdfConverter.PageOrientation = "Portrait";
            return pdfConverter.ConvertToPdf(h.ToString(), b.ToString(), "-", "Print-Dispatch-Invoice.pdf");
        }
        public FileResult SalesBill(string InvoiceNo)
        {
            PdfGenerator pdfConverter = new PdfGenerator();
            SalesBillInfo obj = new SalesBillInfo();
            obj.unit_id = "MS-H0048";
            obj.Bill_No = InvoiceNo;
            obj.login_id = "-";
            obj.login_name = "-";
            dataSetPharmacy dsResult = APIProxy.CallPharmacyWebApiMethod("sales/billInformation", obj);
            DataSet ds = dsResult.result;

            string _result = string.Empty;
            StringBuilder h = new StringBuilder();
            StringBuilder b = new StringBuilder();
            StringBuilder f = new StringBuilder();
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr1 in ds.Tables[0].Rows)
                {
                    /*
                    h.Append("<div style='width:100%;padding:2px 4px;zoom:1.5;'>");//head Start		
                    h.Append("<table style='width:100%'>");
                    h.Append("<tr><td style='width:25%'><b>GSTIN :</b> " + dr1["gstno"].ToString() + "</td>");
                    h.Append("<td style='width:25%'><b>Drug Lic. No. : </b>" + dr1["Dl_No"].ToString() + "</td>");
                    h.Append("<td style='width:25%'><b>Food Lic. No. : </b>" + dr1["FoodLicenceNo"].ToString() + "</td>");
                    h.Append("<td style='width:25%'><b>C.I.N. : </b>" + dr1["cin_no"].ToString() + "</td></tr>");
                    h.Append("</table>");
                    h.Append("</div>");//head End
                    */

                    h.Append("<div style='width:100%;padding:2px 4px;zoom:1.5;'>");//head Start			
                    h.Append("<div style='width:25%;float:left;'>");
                    h.Append("<span style='text-align:center;font-size:13px;'><b>GSTIN :</b> <br/> " + dr1["gstno"].ToString() + "</span>");
                    h.Append("</div>");

                    h.Append("<div style='width:25%;float:right;'>");
                    h.Append("<span style='text-align:center;font-size:13px;'><b>Drug Lic. No. : </b> <br/> " + dr1["Dl_No"].ToString() + "</span>");
                    h.Append("</div>");

                    h.Append("<div style='width:25%;float:right;'>");
                    h.Append("<span style='text-align:center;font-size:13px;'><b>Food Lic. No. : </b> <br/> " + dr1["FoodLicenceNo"].ToString() + "</span>");
                    h.Append("</div>");

                    h.Append("<div style='width:25%;float:right;'>");
                    h.Append("<span style='text-align:center;font-size:13px;'><b>C.I.N. : </b> <br/> " + dr1["cin_no"].ToString() + "</span>");
                    h.Append("</div>");
                    h.Append("</div>");//head End

                    h.Append("<div style='width:100%;border:1px solid #000;border-radius:5px;zoom:1.5;'>");
                    //Header
                    h.Append("<div style='width:100%;margin-top:10px;'>");
                    h.Append("<div style='width:20%;float:left;font-size:11px;'>");
                    string headerImageFile = HttpContext.Server.MapPath(@"~/Content/logo/logo.png");
                    h.Append("<img src=" + headerImageFile + " style='width:90px;margin:20px 10px;' />");
                    h.Append("</div>");
                    h.Append("<div style='width:50%;float:left;font-size:13px;line-height:16px;'>");
                    h.Append("<h3 style='font-weight:bold;margin:0'>" + dr1["shop_name"].ToString() + "</h3>");
                    h.Append("<span style='text-align:left;'>" + dr1["comp_name"].ToString() + "</span><br/>");
                    h.Append("<span style='text-align:left;'>" + dr1["address"].ToString() + "</span><br/>");
                    h.Append("<span style='text-align:left;'>Contact No. 05226666601, 8707542569, Email:care@chandanpharmacy.com</span><br/>");
                    h.Append("</div>");
                    h.Append("<div style='width:30%;float:right;font-size:12px;line-height:16px;zoom:1.2'>");
                    h.Append("<b style='float:right;padding-right:5px;'>Sale Date : " + Convert.ToDateTime(dr1["sale_date"]).ToString("dd-MMM-yyy") + "</b><br/>");
                    h.Append("<b style='float:right;padding-right:5px;'>Sale Inv. No. : " + dr1["sale_inv_no"].ToString() + "</b><br/>");
                    h.Append("<b style='float:right;padding-right:5px;'>Health Card No. : " + dr1["card_no"].ToString() + "</b>");
                    h.Append("</div>");
                    h.Append("</div>");
                    //Information
                    h.Append("<div style='width:99%;border-top:1px solid #000;height:60px;border-bottom:1px solid #000;margin:80px 0 0 0;padding:2px 4px;zoom:1.2'>");
                    h.Append("<div style='width:40%;float:left;font-size:12px;line-height:18px'>");
                    h.Append("<span style='text-align:left;'>Details of Receiver (Billed to)</span><br/>");
                    h.Append("<b style='text-align:left;'>Name : " + dr1["pt_name"].ToString() + "</b><br/>");
                    h.Append("<span style='text-align:left;'>Prescribed By : " + dr1["ref_name"].ToString() + "</span><br/>");
                    h.Append("</div>");
                    h.Append("<div style='width:50%;float:left;font-size:13px;'>");
                    h.Append("<span style='text-align:left;'>IPD/OPD PATIENT INFORMATION</span><br/>");
                    h.Append("<table style='width:100%;font-size:13px;text-align:left;margin:0'>");
                    h.Append("<td style='width:12%;'> " + dr1["IpdInfo"].ToString() + "</td>");
                    h.Append("</tr>");
                    h.Append("</table>");
                    h.Append("</div>");
                    h.Append("<div style='width:9%;float:right;font-size:10px;line-height:22px;border:2px solid #000;border-radius:2px;text-align:center'>");
                    h.Append("<b style='padding:2px;'>Token No.</b><br/>");
                    h.Append("<span style='padding:2px;font-size:18px;'>" + dr1["token_no"].ToString() + "</span>");
                    h.Append("</div>");
                    h.Append("</div>");
                    //Items 				
                    b.Append("<div style='width:100%;'>");
                    b.Append("<table style='width:100%;font-size:13px;text-align:left;margin:0; border-collapse: collapse;' border='1' >");
                    b.Append("<tr style='background:#ddd;'>");
                    b.Append("<td style='width:1%;padding-left:5px'><b>No.</b></td>");
                    b.Append("<td style='width:30%;padding-left:5px'><b>Medicine Name </b></td>");
                    b.Append("<td style='width:5%;font-size:10px;text-align:right;padding-right:2px'><b>HSN</b></td>");
                    b.Append("<td style='width:5%;font-size:10px;text-align:right;padding-right:2px'><b>UOM</b></td>");
                    b.Append("<td style='width:8%;font-size:10px;text-align:right;padding-right:2px'><b>Unit MRP</b></td>");
                    b.Append("<td style='width:5%;font-size:10px;text-align:right;padding-right:2px'><b>Qty</b></td>");
                    b.Append("<td style='width:5%;font-size:10px;text-align:right;padding-right:2px'><b>Amount</b></td>");
                    b.Append("<td style='width:5%;font-size:10px;text-align:right;padding-right:2px'><b>Discount</b></td>");
                    b.Append("<td style='width:6%;font-size:10px;text-align:right;padding-right:2px'><b>Taxable Value</b></td>");
                    b.Append("<td style='width:5%;font-size:10px;text-align:right;padding-right:2px'><b>CGST Rate</b></td>");
                    b.Append("<td style='width:5%;font-size:10px;text-align:right;padding-right:2px'><b>CGST Amt.</b></td>");
                    b.Append("<td style='width:5%;font-size:10px;text-align:right;padding-right:2px'><b>SGST Rate</b></td>");
                    b.Append("<td style='width:5%;font-size:10px;text-align:right;padding-right:2px'><b>SGST Amt.</b></td>");
                    b.Append("</tr>");
                    //Body			
                    var count = 0;
                    if (ds.Tables.Count > 0 && ds.Tables[1].Rows.Count > 0)
                    {
                        foreach (DataRow dr in ds.Tables[1].Rows)
                        {
                            count++;
                            b.Append("<tr>");
                            b.Append("<td style='width:1%;padding-left:5px'>" + count + "</td>");
                            b.Append("<td style='width:30%;padding-left:5px'>" + dr["item_name"].ToString() + "<br> Mfd/Mkt: " + dr["mfd_name"].ToString() + ",Batch No. " + dr["batch_no"].ToString() + "<br>Expiry : " + dr["exp_date"].ToString() + "</td>");
                            b.Append("<td style='width:5%;font-size:10px;text-align:right;padding-right:2px'>" + dr["hsn"].ToString() + "</td>");
                            b.Append("<td style='width:5%;font-size:10px;text-align:right;padding-right:2px'>" + dr["uom"].ToString() + "</td>");
                            b.Append("<td style='width:8%;font-size:10px;text-align:right;padding-right:2px'>" + Convert.ToDecimal(dr["mrp"]).ToString(".00") + "</td>");
                            b.Append("<td style='width:5%;font-size:10px;text-align:right;padding-right:2px'>" + dr["sale_qty"].ToString() + "</td>");
                            b.Append("<td style='width:5%;font-size:10px;text-align:right;padding-right:2px'>" + Convert.ToDecimal(dr["amount"]).ToString(".00") + "</td>");
                            b.Append("<td style='width:5%;font-size:10px;text-align:right;padding-right:2px'>" + Convert.ToDecimal(dr["discount"]).ToString(".00") + "</td>");
                            b.Append("<td style='width:6%;font-size:10px;text-align:right;padding-right:2px'>" + Convert.ToDecimal(dr["taxvalue"]).ToString(".00") + "</td>");
                            b.Append("<td style='width:5%;font-size:10px;text-align:right;padding-right:2px'>" + Convert.ToDecimal(dr["CGST_rate"]).ToString(".0") + "</td>");
                            b.Append("<td style='width:5%;font-size:10px;text-align:right;padding-right:2px'>" + Convert.ToDecimal(dr["cgst"]).ToString(".00") + "</td>");
                            b.Append("<td style='width:5%;font-size:10px;text-align:right;padding-right:2px'>" + Convert.ToDecimal(dr["SGST_rate"]).ToString(".0") + "</td>");
                            b.Append("<td style='width:5%;font-size:10px;text-align:right;padding-right:2px'>" + Convert.ToDecimal(dr["sgst"]).ToString(".00") + "</td>");
                            b.Append("</tr>");
                        }
                    }

                    b.Append("<tr>");
                    b.Append("<th style='text-align:right;padding-left:5px;' colspan='9'>Amount of Tax Subject to Reverse Charge</th>");
                    b.Append("<td style='font-size:12px;text-align:right;padding-right:2px' colspan='2'> " + Convert.ToDecimal(dr1["cgst"]).ToString(".00") + "</td>");
                    b.Append("<td style='font-size:12px;text-align:right;padding-right:2px' colspan='2'> " + Convert.ToDecimal(dr1["sgst"]).ToString(".00") + "</td>");
                    b.Append("</tr>");
                    b.Append("</table>");
                    b.Append("</div>");
                    b.Append("</div>");//Final End										
                                       //Bottom info			
                    f.Append("<div style='width:100%;border:none;zoom:1.5;'>");
                    f.Append("<div style='width:45%;float:left;font-size:12px;line-height:10px'>");
                    f.Append("<span style='text-align:left;'>Note :</span><br/>");
                    f.Append("<b style='text-align:left;font-size:8px'>1) Price Difference Under Drug Price Control order 1970 will be refunded if any</b><br/>");
                    f.Append("<b style='text-align:left;font-size:8px'>2) Medicine without Cash Memo, Batch No & Exp. will not be taken back.</b><br/>");
                    f.Append("<b style='text-align:left;font-size:8px'>3) Please get medicines verified from prescription before use.</b><br/>");
                    f.Append("<b style='text-align:left;font-size:8px'>4) All disputes subject to LUCKNOW Jurisdiction only.</ b><br/>");
                    f.Append("</div>");
                    f.Append("<div style='width:20%;float:left;font-size:12px;line-height:13px;margin-top:3px;'>");
                    string signature = HttpContext.Server.MapPath(@"/Content/logo/logo.png");
                    f.Append("<img src='data:image/png;base64," + dr1["sign_image"].ToString() + " style='width:80px;height:18px;' /><br/>");
                    f.Append("<b style='text-align:center;font-size:15px;'>Signature</b>");
                    f.Append("</div>");
                    f.Append("<div style='width:35%;float:right;font-size:13px;'>");
                    f.Append("<table style='width:100%;font-size:13px;text-align:left;margin:4px; border-collapse: collapse;' border='1' >");
                    f.Append("<tr>");
                    f.Append("<td style='width:12%;text-align:right;'><b>Total</b></td>");
                    f.Append("<td style='width:12%;text-align:right;'><b>Discount</b></td>");
                    f.Append("<td style='width:12%;text-align:right;'><b>Payable</b></td>");
                    f.Append("</tr>");
                    f.Append("<tr>");
                    f.Append("<td style='width:12%;text-align:right;'> " + Convert.ToDecimal(dr1["total"]).ToString(".00") + "</td>");
                    f.Append("<td style='width:12%;text-align:right;'> " + Convert.ToDecimal(dr1["discount"]).ToString(".00") + "</td>");
                    f.Append("<td style='width:12%;text-align:right;'> " + Convert.ToDecimal(dr1["net"]).ToString(".00") + "</td>");
                    f.Append("</tr>");
                    f.Append("</table>");
                    f.Append("<b style='text-align:center;font-size:15px;'>Received</b>&nbsp;&nbsp;");
                    f.Append("<b style='min-width:95px;text-align:right;border:1px solid #000;padding:1px 3px;float:right;font-size:15px'> " + Convert.ToDecimal(dr1["received"]).ToString(".00") + "</b>");
                    f.Append("</div>");
                    f.Append("<div style='width:45%;float:left;'>");
                    f.Append("<img src=" + BarcodeGenerator.GenerateBarCode(dr1["barcode"].ToString(), 400, 70) + " style='float:right' />");
                    //f.Append("<img src=" + BarcodeGenerator.GenerateBarCode(dr1["barcode"].ToString(),480,60) + " style='float:right' />");
                    f.Append("</div>");
                    f.Append("<div style='width:55%;float:right;padding:20px 0'>");
                    f.Append("<span style='float:right;font-size:13px;'>In Words :" + AmountConverter.ConvertToWords(Convert.ToString(dr1["net"].ToString()).ToString()) + "</span><br><br><br>");
                    f.Append("<span style='float:right;font-size:16px;'>You have saved Rs. " + Convert.ToDecimal(dr1["discount"]).ToString(".00") + "</span>");
                    f.Append("</div>");
                    f.Append("</div>");
                    string CurrentDateTime = DateTime.Now.ToString("dd-MMM-yyy hh:mm tt");
                    f.Append("<div style='width:100%;border-top:1px solid #000;zoom:1.5;margin-top:10px'>");//Final Start	
                    f.Append("<table style='width:100%;font-size:12px;text-align:left;margin:4px; border-collapse: collapse;' border='0' >");
                    f.Append("<tr>");
                    f.Append("<td style='width:20%;text-align:center;'><b>Counter No :</b> " + dr1["counter_id"].ToString() + "</td>");
                    f.Append("<td style='width:40%;text-align:center;'><b>Bill Creation Date & Time :</b> " + CurrentDateTime + "</td>");
                    f.Append("<td style='width:40%;text-align:center;'><b>Bill Printing Time :</b> " + CurrentDateTime + "</td>");
                    f.Append("</tr>");
                    f.Append("</table>");
                    f.Append("</div>");//Footer End						
                }

            }
            pdfConverter.Header_Enabled = true;
            pdfConverter.Footer_Enabled = true;
            pdfConverter.Footer_Hight = 110;
            pdfConverter.Header_Hight = 100;
            pdfConverter.PageMarginLeft = 10;
            pdfConverter.PageMarginRight = 10;
            pdfConverter.PageMarginBottom = 10;
            pdfConverter.PageMarginTop = 10;
            pdfConverter.PageName = "A5";
            pdfConverter.PageOrientation = "Portrait";
            return pdfConverter.ConvertToPdf(h.ToString(), b.ToString(), f.ToString(), "Sales-Bill.pdf");
        }
        public FileResult ClinicalSafetyRoundReports(string date, string FloorName, string Logic = "ClinicalSafetyRoundReports")
        {
            PdfGenerator pdfConverter = new PdfGenerator();
            ipHandoverInfo obj = new ipHandoverInfo();
            obj.Prm1 = date;
            obj.Prm2 = FloorName;
            obj.Logic = Logic;
            HISWebApi.Models.dataSet dsResult = APIProxy.CallWebApiMethod("IPDNursing/IPD_ClinicalSafetyRoundQuesries", obj);
            DataSet ds = dsResult.ResultSet;
            string _result = string.Empty;
            StringBuilder b = new StringBuilder();
            StringBuilder h = new StringBuilder();
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                b.Append("<div>");
                b.Append("<span style='width:30%;font-size:16px;'><b>DATE : " + Convert.ToDateTime(date).ToString("dd-MM-yyyy") + "</span>");
                b.Append("<span style='width:70%;font-size:16px;margin-left:50px;'><b>Floor Name: " + FloorName + "</span>");
                b.Append("</div>");
                b.Append("<h2 style='background-color:#ccc;text-align:center;font-family:Verdana,sans-serif;font-weight:bold;margin-bottom:5px;'>Clinical Safety Round Report</h2>");
                b.Append("<hr/>");
                b.Append("<div>");
                b.Append("<table border='1' style='width:100%;white-space:nowrap;font-size:11px;border-collapse: collapse;text-align:center;'>");
                b.Append("<tr>");
                foreach (DataColumn dc in ds.Tables[0].Columns)
                {
                    b.Append("<th style='width:12%;background-color:#ddd;'>" + dc.ColumnName + "</td>");
                }
                b.Append("</tr>");
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    b.Append("<tr>");
                    for (int j = 0; j < ds.Tables[0].Columns.Count; j++)
                    {
                        b.Append("<th style='width:12%;'>" + ds.Tables[0].Rows[i][j].ToString() + "</td>");
                    }
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
            pdfConverter.PageOrientation = "Landscape";
            return pdfConverter.ConvertToPdf(h.ToString(), b.ToString(), "-", "Print-Dispatch-Invoice.pdf");
        }
        public FileResult PatientAllotmentReport(string date, string FloorName, string Logic = "PatientAllotmentReport")
        {
            PdfGenerator pdfConverter = new PdfGenerator();
            ipHandoverInfo obj = new ipHandoverInfo();
            obj.Prm1 = date;
            obj.Prm2 = FloorName;
            obj.Logic = Logic;
            HISWebApi.Models.dataSet dsResult = APIProxy.CallWebApiMethod("IPDNursing/IPD_ClinicalSafetyRoundQuesries", obj);
            DataSet ds = dsResult.ResultSet;
            string _result = string.Empty;
            StringBuilder b = new StringBuilder();
            StringBuilder h = new StringBuilder();
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                b.Append("<div>");
                b.Append("<span style='width:30%;font-size:16px;'><b>DATE : " + Convert.ToDateTime(date).ToString("dd-MM-yyyy") + "</span>");
                b.Append("<span style='width:70%;font-size:16px;margin-left:50px;'><b>Floor Name: " + FloorName + "</span>");
                b.Append("</div>");
                b.Append("<h2 style='background-color:#ccc;text-align:center;font-family:Verdana,sans-serif;font-weight:bold;margin-bottom:5px;'>Staff Assignment Report </h2>");
                b.Append("<hr/>");
                b.Append("<div style='width:100%;'>");
                b.Append("<table border='1' style='width:100%;white-space:nowrap;font-size:11px;border-collapse: collapse;text-align:center;'>");
                b.Append("<tr>");
                foreach (DataColumn dc in ds.Tables[0].Columns)
                {
                    b.Append("<th style='width:12%;background-color:#ddd;text-align:left'>" + dc.ColumnName + "</td>");
                }
                b.Append("</tr>");
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    b.Append("<tr>");
                    for (int j = 0; j < ds.Tables[0].Columns.Count; j++)
                    {
                        if (ds.Tables[0].Rows[i][j].ToString() == "0")
                            b.Append("<th style='width:12%;text-align:left'></td>");
                        else
                            b.Append("<th style='width:12%;text-align:left;font-size:10px'>" + ds.Tables[0].Rows[i][j].ToString() + "</td>");
                    }
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
            return pdfConverter.ConvertToPdf(h.ToString(), b.ToString(), "-", "StaffAlloted.pdf");
        }
        public FileResult IPDBillSummary(string _IPDNo, string _BillNo, string _BillPrintType)
        {
            _IPDNo = UtilityClass.decoding(_IPDNo);
            _BillNo = UtilityClass.decoding(_BillNo);

            PdfGenerator pdfConverter = new PdfGenerator();
            IPDInfo obj = new IPDInfo();
            obj.IPDNo = _IPDNo;
            obj.UHID = "-";
            obj.DoctorId = "-";
            obj.from = "1900/01/01";
            obj.to = "1900/01/01";
            obj.Prm1 = _BillNo;
            obj.Prm2 = _BillPrintType;
            obj.login_id = "-";
            obj.Logic = "BillPrinting";
            HISWebApi.Models.dataSet dsResult = APIProxy.CallWebApiMethod("IPDBilling/IPD_BillingQuerries", obj);
            DataSet ds = dsResult.ResultSet;

            string _result = string.Empty;
            StringBuilder b = new StringBuilder();
            StringBuilder h = new StringBuilder();
            string UHID = "";
            string patient_name = "";
            string BillType = "";
            string ageInfo = "";
            string AdmitDate = "";
            string IPDNo = "";
            string DoctorName = "";
            string roomFullName = "";
            string PanelName = "";
            string DischargeDate = "";
            string Department = "";
            string BillNo = "";
            string BillDate = "";
            string ContactNo = "";
            string DischargeType = "";
            string Address = "";

            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                UHID = dr["UHID"].ToString();
                BillType = dr["BillType"].ToString();
                IPDNo = dr["IPDNo"].ToString();
                patient_name = dr["patient_name"].ToString();
                ageInfo = dr["ageInfo"].ToString();
                AdmitDate = dr["AdmitDate"].ToString();
                DoctorName = dr["DoctorName"].ToString();
                roomFullName = dr["roomFullName"].ToString();
                PanelName = dr["PanelName"].ToString();
                DischargeDate = dr["DischargeDate"].ToString();
                Department = dr["Department"].ToString();
                BillNo = dr["BillNo"].ToString();
                BillDate = dr["BillDate"].ToString();
                DischargeType = dr["DischargeType"].ToString();
                ContactNo = dr["ContactNo"].ToString();
                Address = dr["Address"].ToString();
            }
            h.Append("<table style='width:2080px;padding:10px 0;background:#fff;font-size:42px;text-align:left;border:1px solid #000;margin:0 15px'>");
            //h.Append("<tr>");
            //h.Append("<td colspan='7' style='font-size:62px;text-align:center'>Final Bill Detail<hr/ style='margin:7px'></td>");
            //h.Append("</tr>");

            h.Append("<tr>");
            h.Append("<td style='width:17%;padding:3px;'><b>UHID</b></td>");
            h.Append("<td style='width:1%;'><b>:</b></td>");
            h.Append("<td style='width:32%;text-aligh:left'>" + UHID + "</td>");
            h.Append("<td style='width:1%;'>&nbsp;</td>");
            h.Append("<td style='width:20%;'><b>Bill No</b></td>");
            h.Append("<td style='width:1%;'><b>:</b></td>");
            h.Append("<td style='width:29%;'>" + BillNo + "</td>");
            h.Append("</tr>");

            h.Append("<tr>");
            h.Append("<td style='width:17%;'><b>IPD No</b></td>");
            h.Append("<td style='width:1%;'><b>:</b></td>");
            h.Append("<td style='width:32%;'>" + IPDNo + "</td>");
            h.Append("<td style='width:1%;'>&nbsp;</td>");
            h.Append("<td style='width:20%;'><b>Bill Date</b></td>");
            h.Append("<td style='width:1%;'><b>:</b></td>");
            h.Append("<td style='width:29%;'>" + BillDate + "</td>");
            h.Append("</tr>");
            h.Append("<tr>");
            h.Append("<td style='width:17%;'><b>Patient Name</b></td>");
            h.Append("<td style='width:1%;'><b>:</b></td>");
            h.Append("<td style='width:32%;'>" + patient_name + "</td>");
            h.Append("<td style='width:1%;'>&nbsp;</td>");
            h.Append("<td style='width:20%;'><b>Panel</b></td>");
            h.Append("<td style='width:1%;'><b>:</b></td>");
            h.Append("<td style='width:29%;'>" + PanelName + "</td>");
            h.Append("</tr>");
            if (_BillPrintType == "BillTypeCategorywiseOnly")
                b.Append("<h2 style='text-align:center;font-weight:bold;text-decoration: underline;'>" + BillType + " Summary Bill Detail</h2>");

            if (_BillPrintType == "ItemWise")
                b.Append("<h2 style='text-align:center;font-weight:bold;text-decoration: underline;'>" + BillType + " Item wise Bill Detail</h2>");

            if (_BillPrintType == "IncludingPackagedItem")
                b.Append("<h2 style='text-align:center;font-weight:bold;text-decoration: underline;'>" + BillType + " Package Breakup Detail</h2>");

            b.Append("<table style='padding:10px 0;background:#fff;width:100%;font-size:15px;text-align:left;border:1px solid #000;margin-bottom:-15px;margin-top:0'>");
            b.Append("<tr>");
            b.Append("<td style='width:17%;padding:3px;'><b>UHID</b></td>");
            b.Append("<td style='width:1%;'><b>:</b></td>");
            b.Append("<td style='width:32%;text-aligh:left'>" + UHID + "</td>");
            b.Append("<td style='width:1%;'>&nbsp;</td>");
            b.Append("<td style='width:20%;'><b>Bill No</b></td>");
            b.Append("<td style='width:1%;'><b>:</b></td>");
            b.Append("<td style='width:29%;'>" + BillNo + "</td>");
            b.Append("</tr>");

            b.Append("<tr>");
            b.Append("<td style='width:17%;'><b>IPD No</b></td>");
            b.Append("<td style='width:1%;'><b>:</b></td>");
            b.Append("<td style='width:32%;'>" + IPDNo + "</td>");
            b.Append("<td style='width:1%;'>&nbsp;</td>");
            b.Append("<td style='width:20%;'><b>Bill Date</b></td>");
            b.Append("<td style='width:1%;'><b>:</b></td>");
            b.Append("<td style='width:29%;'>" + BillDate + "</td>");
            b.Append("</tr>");
            b.Append("<tr>");
            b.Append("<td style='width:17%;'><b>Patient Name</b></td>");
            b.Append("<td style='width:1%;'><b>:</b></td>");
            b.Append("<td style='width:32%;'>" + patient_name + "</td>");
            b.Append("<td style='width:1%;'>&nbsp;</td>");
            b.Append("<td style='width:20%;'><b>Panel</b></td>");
            b.Append("<td style='width:1%;'><b>:</b></td>");
            b.Append("<td style='width:29%;'>" + PanelName + "</td>");
            b.Append("</tr>");

            b.Append("<tr>");
            b.Append("<td style='width:17%;padding:3px;'><b>Age</b></td>");
            b.Append("<td style='width:1%;'><b>:</b></td>");
            b.Append("<td style='width:32%;text-aligh:left'>" + ageInfo + "</td>");
            b.Append("<td style='width:1%;'>&nbsp;</td>");
            b.Append("<td style='width:20%;'><b>Doctor Name</b></td>");
            b.Append("<td style='width:1%;'><b>:</b></td>");
            b.Append("<td style='width:29%;'>" + DoctorName + "</td>");
            b.Append("</tr>");

            b.Append("<tr>");
            b.Append("<td style='width:17%;'><b>Contact No </b></td>");
            b.Append("<td style='width:1%;'><b>:</b></td>");
            b.Append("<td style='width:32%;'>" + ContactNo + "</td>");
            b.Append("<td style='width:1%;'>&nbsp;</td>");
            b.Append("<td style='width:20%;'><b>Discharge Type</b></td>");
            b.Append("<td style='width:1%;'><b>:</b></td>");
            b.Append("<td style='width:29%;'>" + DischargeType + "</td>");
            b.Append("</tr>");

            b.Append("<tr>");
            b.Append("<td style='width:17%;'><b>Address</b></td>");
            b.Append("<td style='width:1%;'><b>:</b></td>");
            b.Append("<td style='width:32%;'>" + Address + "</td>");
            b.Append("<td style='width:1%;'>&nbsp;</td>");
            b.Append("<td style='width:20%;'><b>Admission Date Time</b></td>");
            b.Append("<td style='width:1%;'><b>:</b></td>");
            b.Append("<td style='width:29%;'>" + AdmitDate + "</td>");
            b.Append("</tr>");

            b.Append("<tr>");
            b.Append("<td style='width:17%;'><b></b></td>");
            b.Append("<td style='width:1%;'><b></b></td>");
            b.Append("<td style='width:32%;'></td>");
            b.Append("<td style='width:1%;'>&nbsp;</td>");
            b.Append("<td style='width:20%;'><b>Discharge Date Time</b></td>");
            b.Append("<td style='width:1%;'><b>:</b></td>");
            b.Append("<td style='width:29%;'>" + DischargeDate + "</td>");
            b.Append("</tr>");
            b.Append("</table>");
            if (_BillPrintType != "IncludingPackagedItem")
            {
                b.Append("<br/><label style='padding:2px 5px;margin-top:20px'><b>Category Wise Details</b></label>");
                b.Append("<table border='1' style='width:100%;font-size:12px;border-collapse: collapse'>");
                b.Append("<tr style='background:#ddd'>");
                b.Append("<th style='width:40%;text-align:left;padding-left:4px;'>Cat Name</th>");
                b.Append("<th style='white-space: nowrap;text-align:right;'>Item Count</th>");
                b.Append("<th style='text-align:right;padding-right:4px;'>Gross Amount</th>");
                b.Append("<th style='white-space: nowrap;text-align:right'>Discount</th>");
                b.Append("<th style='white-space: nowrap;text-align:right'>Net Amount</th>");
                b.Append("</tr>");
                if (ds.Tables.Count > 0 && ds.Tables[1].Rows.Count > 0)
                {
                    foreach (DataRow dr in ds.Tables[1].Rows)
                    {
                        b.Append("<tr>");
                        b.Append("<td style='text-align:left;white-space: nowrap;padding-left:4px;'>" + dr["CatName"].ToString() + "</td>");
                        b.Append("<td style='text-align:right;white-space: nowrap;padding-right:4px;'>" + dr["ItemCount"].ToString() + "</td>");
                        b.Append("<td style='text-align:right;padding-right:4px;'>" + dr["GrossAmount"].ToString() + "</td>");
                        b.Append("<td style='text-align:right;white-space: nowrap;padding-right:4px;'>" + dr["Discount"].ToString() + "</td>");
                        b.Append("<td style='text-align:right;white-space: nowrap;padding-right:4px;'>" + dr["NetAmount"].ToString() + "</td>");
                        b.Append("</tr>");
                    }
                }
                b.Append("</table>");
            }
            if (_BillPrintType == "ItemWise")
            {
                b.Append("<br/><label style='padding:2px 5px;margin-top:20px'><b>Item Wise Details</b></label>");
                b.Append("<table border='0' style='width:100%;font-size:12px;border-collapse: collapse'>");
                b.Append("<tr style='background:#ddd'>");
                b.Append("<th style='width:5%;text-align:left;padding-left:4px;'>S.N</th>");
                b.Append("<th style='width:12%;text-align:left;padding-left:4px;'>Tnx Date</th>");
                b.Append("<th style='width:46%;text-align:left;padding-left:4px;'>Item Name</th>");
                b.Append("<th style='width:3%;white-space: nowrap;text-align:right;'>Qty</th>");
                b.Append("<th style='width:12%;text-align:right;padding-right:4px;'>Rate</th>");
                b.Append("<th style='width:11%;white-space: nowrap;text-align:right'>Discount</th>");
                b.Append("<th style='width:16%;white-space: nowrap;text-align:right'>Amount</th>");
                b.Append("</tr>");
                string temp = string.Empty;
                string temp2 = string.Empty;
                string temp3 = string.Empty;
                int CounItem = 1;
                int CountSurgeryItem = 1;
                decimal tAmount = 0;
                decimal tSurgeryAmt = 0;
                if (ds.Tables.Count > 0 && ds.Tables[2].Rows.Count > 0)
                {
                    foreach (DataRow dr in ds.Tables[2].Rows)
                    {
                        if (temp != dr["SubCatNameDisplayName"].ToString())
                        {
                            if (CounItem != 1)
                            {
                                b.Append("<tr style='background:#fff'>");
                                b.Append("<td colspan='6' style='font-size:11px;text-align:right;white-space: nowrap;padding:5px 4px;'><b>Category Wise Total<b></td>");
                                b.Append("<td  style='font-size:11px;text-align:right;white-space: nowrap;padding:5px 4px;'><b>" + Convert.ToDecimal(tAmount).ToString(".00") + "<b></td>");
                                b.Append("</tr>");
                                tAmount = 0;
                            }
                            b.Append("<tr style='background:#fff'>");
                            b.Append("<td colspan='7' style='font-size:13px;text-decoration:underline;text-align:left;white-space: nowrap;padding:5px 4px;'><b>" + dr["SubCatNameDisplayName"].ToString().ToUpper() + "<b></td>");
                            b.Append("</tr>");
                            temp = dr["SubCatNameDisplayName"].ToString();
                        }
                        if (dr["SurgeryName"].ToString().Length > 2 && temp3 != dr["SurgeryName"].ToString())
                        {
                            if (CountSurgeryItem != 1)
                            {
                                b.Append("<tr style='background:#fff'>");
                                b.Append("<td colspan='6' style='font-size:11px;text-align:right;white-space: nowrap;padding:5px 4px;'><b>Surgery Wise Total<b></td>");
                                b.Append("<td  style='font-size:11px;text-align:right;white-space: nowrap;padding:5px 4px;'><b>" + Convert.ToDecimal(tSurgeryAmt).ToString(".00") + "<b></td>");
                                b.Append("</tr>");
                                tSurgeryAmt = 0;
                            }
                            b.Append("<tr style='background:#fff'>");
                            b.Append("<td colspan='7' style='font-size:11px;text-align:left;white-space: nowrap;padding:5px 4px;'><b>" + dr["SurgeryName"].ToString() + "<b></td>");
                            b.Append("</tr>");
                            temp3 = dr["SurgeryName"].ToString();
                            tSurgeryAmt = 0;
                            CountSurgeryItem++;
                        }
                        b.Append("<tr>");
                        b.Append("<td style='text-align:left;white-space: nowrap;padding-left:4px;width:5%'>" + (CounItem++).ToString() + "</td>");
                        b.Append("<td style='text-align:left;white-space: nowrap;padding-left:4px;'>" + dr["TnxDate"].ToString() + "</td>");
                        b.Append("<td style='text-align:left;white-space: nowrap;padding-left:4px;'>" + dr["ItemName"].ToString() + "</td>");
                        b.Append("<td style='text-align:right;white-space: nowrap;padding-right:4px;'>" + dr["Qty"].ToString() + "</td>");
                        b.Append("<td style='text-align:right;padding-right:4px;'>" + dr["panel_rate"].ToString() + "</td>");
                        b.Append("<td style='text-align:right;white-space: nowrap;padding-right:4px;'>" + dr["discount"].ToString() + "</td>");
                        b.Append("<td style='text-align:right;white-space: nowrap;padding-right:4px;'>" + dr["amount"].ToString() + "</td>");
                        b.Append("</tr>");
                        tAmount = tAmount + Convert.ToDecimal(dr["amount"]);
                        tSurgeryAmt = tSurgeryAmt + Convert.ToDecimal(dr["amount"]);

                    }
                }
                if (CountSurgeryItem > 1)
                {
                    b.Append("<tr style='background:#fff'>");
                    b.Append("<td colspan='6' style='font-size:11px;text-align:right;white-space: nowrap;padding:5px 4px;'><b>Surgery Wise Total<b></td>");
                    b.Append("<td  style='font-size:11px;text-decoration:underline;text-align:right;white-space: nowrap;padding:5px 4px;'><b>" + Convert.ToDecimal(tSurgeryAmt).ToString(".00") + "<b></td>");
                    b.Append("</tr>");
                }
                if (tAmount > 0)
                {
                    b.Append("<tr style='background:#fff'>");
                    b.Append("<td colspan='6' style='font-size:11px;text-align:right;white-space: nowrap;padding:5px 4px;'><b>Category Wise Total<b></td>");
                    b.Append("<td  style='font-size:11px;text-decoration:underline;text-align:right;white-space: nowrap;padding:5px 4px;'><b>" + Convert.ToDecimal(tAmount).ToString(".00") + "<b></td>");
                    b.Append("</tr>");
                }

                b.Append("</table>");
            }
            if (_BillPrintType == "DateWise")
            {
                b.Append("<br/><label style='padding:2px 5px;margin-top:20px'><b>Item Wise Details</b></label>");
                b.Append("<table border='0' style='width:100%;font-size:12px;border-collapse: collapse'>");
                b.Append("<tr style='background:#ddd'>");
                b.Append("<th style='width:5%;text-align:left;padding-left:4px;'>S.N</th>");
                b.Append("<th style='width:12%;text-align:left;padding-left:4px;'>Tnx Date</th>");
                b.Append("<th style='width:46%;text-align:left;padding-left:4px;'>Item Name</th>");
                b.Append("<th style='width:3%;white-space: nowrap;text-align:right;'>Qty</th>");
                b.Append("<th style='width:12%;text-align:right;padding-right:4px;'>Rate</th>");
                b.Append("<th style='width:11%;white-space: nowrap;text-align:right'>Discount</th>");
                b.Append("<th style='width:16%;white-space: nowrap;text-align:right'>Amount</th>");
                b.Append("</tr>");
                string temp = string.Empty;
                string tempDateWise = string.Empty;
                string temp3 = string.Empty;
                int CounItem = 1;
                int CountSurgeryItem = 1;
                int CountDateWise = 1;
                decimal tAmount = 0;
                decimal tSurgeryAmt = 0;
                decimal tDateWiseAmount = 0;
                if (ds.Tables.Count > 0 && ds.Tables[2].Rows.Count > 0)
                {
                    foreach (DataRow dr in ds.Tables[2].Rows)
                    {
                        if (tempDateWise != dr["TnxDate"].ToString())
                        {
                            if (CountDateWise != 0)
                            {
                                b.Append("<tr style='background:#fff'>");
                                b.Append("<td colspan='6' style='font-size:11px;text-align:right;white-space: nowrap;padding:5px 4px;'><b>Date Wise Total<b></td>");
                                b.Append("<td  style='font-size:11px;text-align:right;white-space: nowrap;padding:5px 4px;'><b>" + Convert.ToDecimal(tDateWiseAmount).ToString(".00") + "<b></td>");
                                b.Append("</tr>");
                                tDateWiseAmount = 0;
                            }
                            b.Append("<tr style='background:#fff'>");
                            b.Append("<td colspan='7' style='font-size:13px;text-decoration:underline;text-align:left;white-space: nowrap;padding:5px 4px;'><b>" + dr["TnxDate"].ToString().ToUpper() + "<b></td>");
                            b.Append("</tr>");
                            tempDateWise = dr["TnxDate"].ToString();
                        }
                        if (temp != dr["SubCatNameDisplayName"].ToString())
                        {
                            if (CounItem != 1)
                            {
                                b.Append("<tr style='background:#fff'>");
                                b.Append("<td colspan='6' style='font-size:11px;text-align:right;white-space: nowrap;padding:5px 4px;'><b>Category Wise Total<b></td>");
                                b.Append("<td  style='font-size:11px;text-align:right;white-space: nowrap;padding:5px 4px;'><b>" + Convert.ToDecimal(tAmount).ToString(".00") + "<b></td>");
                                b.Append("</tr>");
                                tAmount = 0;
                            }
                            b.Append("<tr style='background:#fff'>");
                            b.Append("<td colspan='7' style='font-size:13px;text-decoration:underline;text-align:left;white-space: nowrap;padding:5px 4px;'><b>" + dr["SubCatNameDisplayName"].ToString().ToUpper() + "<b></td>");
                            b.Append("</tr>");
                            temp = dr["SubCatNameDisplayName"].ToString();
                        }
                        if (dr["SurgeryName"].ToString().Length > 2 && temp3 != dr["SurgeryName"].ToString())
                        {
                            if (CountSurgeryItem != 1)
                            {
                                b.Append("<tr style='background:#fff'>");
                                b.Append("<td colspan='6' style='font-size:11px;text-align:right;white-space: nowrap;padding:5px 4px;'><b>Surgery Wise Total<b></td>");
                                b.Append("<td  style='font-size:11px;text-align:right;white-space: nowrap;padding:5px 4px;'><b>" + Convert.ToDecimal(tSurgeryAmt).ToString(".00") + "<b></td>");
                                b.Append("</tr>");
                                tSurgeryAmt = 0;
                            }
                            b.Append("<tr style='background:#fff'>");
                            b.Append("<td colspan='7' style='font-size:11px;text-align:left;white-space: nowrap;padding:5px 4px;'><b>" + dr["SurgeryName"].ToString() + "<b></td>");
                            b.Append("</tr>");
                            temp3 = dr["SurgeryName"].ToString();
                            tSurgeryAmt = 0;
                            CountSurgeryItem++;
                        }
                        b.Append("<tr>");
                        b.Append("<td style='text-align:left;white-space: nowrap;padding-left:4px;width:5%'>" + (CounItem++).ToString() + "</td>");
                        b.Append("<td style='text-align:left;white-space: nowrap;padding-left:4px;'>" + dr["TnxDate"].ToString() + "</td>");
                        b.Append("<td style='text-align:left;white-space: nowrap;padding-left:4px;'>" + dr["ItemName"].ToString() + "</td>");
                        b.Append("<td style='text-align:right;white-space: nowrap;padding-right:4px;'>" + dr["Qty"].ToString() + "</td>");
                        b.Append("<td style='text-align:right;padding-right:4px;'>" + dr["panel_rate"].ToString() + "</td>");
                        b.Append("<td style='text-align:right;white-space: nowrap;padding-right:4px;'>" + dr["discount"].ToString() + "</td>");
                        b.Append("<td style='text-align:right;white-space: nowrap;padding-right:4px;'>" + dr["amount"].ToString() + "</td>");
                        b.Append("</tr>");
                        tAmount = tAmount + Convert.ToDecimal(dr["amount"]);
                        tSurgeryAmt = tSurgeryAmt + Convert.ToDecimal(dr["amount"]);
                        tDateWiseAmount = tDateWiseAmount + Convert.ToDecimal(dr["amount"]);
                        CountDateWise++;
                    }
                }
                b.Append("<tr style='background:#fff'>");
                b.Append("<td colspan='6' style='font-size:11px;text-align:right;white-space: nowrap;padding:5px 4px;'><b>Date Wise Total<b></td>");
                b.Append("<td  style='font-size:11px;text-align:right;white-space: nowrap;padding:5px 4px;'><b>" + Convert.ToDecimal(tDateWiseAmount).ToString(".00") + "<b></td>");
                b.Append("</tr>");

                if (CountSurgeryItem > 1)
                {
                    b.Append("<tr style='background:#fff'>");
                    b.Append("<td colspan='6' style='font-size:11px;text-align:right;white-space: nowrap;padding:5px 4px;'><b>Surgery Wise Total<b></td>");
                    b.Append("<td  style='font-size:11px;text-decoration:underline;text-align:right;white-space: nowrap;padding:5px 4px;'><b>" + Convert.ToDecimal(tSurgeryAmt).ToString(".00") + "<b></td>");
                    b.Append("</tr>");
                }
                if (tAmount > 0)
                {
                    b.Append("<tr style='background:#fff'>");
                    b.Append("<td colspan='6' style='font-size:11px;text-align:right;white-space: nowrap;padding:5px 4px;'><b>Category Wise Total<b></td>");
                    b.Append("<td  style='font-size:11px;text-decoration:underline;text-align:right;white-space: nowrap;padding:5px 4px;'><b>" + Convert.ToDecimal(tAmount).ToString(".00") + "<b></td>");
                    b.Append("</tr>");
                }

                b.Append("</table>");
            }
            if (_BillPrintType == "IncludingPackagedItem")
            {
                b.Append("<br/><label style='padding:2px 5px;margin-top:20px'><b>Item Wise Details</b></label>");
                b.Append("<table border='0' style='width:100%;font-size:12px;border-collapse: collapse'>");
                b.Append("<tr style='background:#ddd'>");
                b.Append("<th style='width:5%;text-align:left;padding-left:4px;'>S.N.</th>");
                b.Append("<th style='width:10%;text-align:left;padding-left:4px;'>Tnx Date</th>");
                b.Append("<th style='width:20%;text-align:left;padding-left:4px;'>Sub Category Name</th>");
                b.Append("<th style='width:45%;text-align:left;padding-left:4px;'>Item Name</th>");
                b.Append("<th style='width:5%;white-space: nowrap;text-align:right;'>Qty</th>");
                b.Append("<th style='width:15%;white-space: nowrap;text-align:right'>Amount</th>");
                b.Append("</tr>");
                string temp = string.Empty;
                string temp2 = string.Empty;
                string temp3 = string.Empty;
                int CounItem = 1;
                decimal tAmount = 0;
                decimal PackageAmount = 0;
                if (ds.Tables.Count > 0 && ds.Tables[5].Rows.Count > 0)
                {
                    foreach (DataRow dr in ds.Tables[5].Rows)
                    {
                        if (temp != dr["PackageName"].ToString())
                        {
                            if (CounItem != 1)
                            {
                                b.Append("<tr style='background:#fff'>");
                                b.Append("<td colspan='6' style='text-align:right;white-space: nowrap;padding-right:4px;hight:15px'></td>");
                                b.Append("</tr>");

                                b.Append("<tr style='background:#fff'>");
                                b.Append("<td colspan='4' style='text-align:right;white-space: nowrap;padding-right:4px;width:5%;font-size:13px;'><b>Item wise Total<b></td>");
                                b.Append("<td style='text-align:center;white-space: nowrap;padding-left:4px;width:5%;font-size:13px;'>:<b></td>");
                                b.Append("<td style='text-align:right;white-space: nowrap;padding-right:4px;width:15%;font-size:13px;'><b>" + Convert.ToDecimal(tAmount).ToString(".00") + "<b></td>");
                                b.Append("</tr>");

                                b.Append("<tr style='background:#fff'>");
                                b.Append("<td colspan='4' style='text-align:right;white-space: nowrap;padding-left:4px;width:5%;font-size:13px;'><b>Package Discount Amount<b></td>");
                                b.Append("<td style='text-align:center;white-space: nowrap;padding-left:4px;width:5%;font-size:13px;'>:<b></td>");
                                b.Append("<td style='text-align:right;white-space: nowrap;padding-right:4px;width:15%;font-size:13px;'><b>" + Convert.ToDecimal(tAmount - PackageAmount).ToString(".00") + "<b></td>");
                                b.Append("</tr>");

                                b.Append("<tr style='background:#fff'>");
                                b.Append("<td colspan='4' style='text-align:right;white-space: nowrap;padding-left:4px;width:5%;font-size:13px;'><b>Net Package Amount<b></td>");
                                b.Append("<td style='text-align:center;white-space: nowrap;padding-left:4px;width:5%;font-size:13px;'>:<b></td>");
                                b.Append("<td style='text-align:right;white-space: nowrap;padding-right:4px;width:15%;font-size:13px;'><b>" + PackageAmount.ToString(".00") + "<b></td>");
                                b.Append("</tr>");

                                tAmount = 0;
                            }
                            b.Append("<tr style='background:#fff'>");
                            b.Append("<td colspan='6' style='text-align:right;white-space: nowrap;padding-right:4px;hight:15px'><hr/></td>");
                            b.Append("</tr>");

                            b.Append("<tr style='background:#fff'>");
                            b.Append("<td colspan='6' style='font-size:13px;text-decoration:underline;text-align:left;white-space: nowrap;padding:5px 4px;'><b>" + dr["PackageName"].ToString().ToUpper() + "<b></td>");
                            b.Append("</tr>");
                            temp = dr["PackageName"].ToString();
                            PackageAmount = Convert.ToDecimal(dr["PackageAmount"]);
                        }
                        b.Append("<tr>");
                        b.Append("<td style='text-align:left;white-space: nowrap;padding-left:4px;width:5%'>" + (CounItem++).ToString() + "</td>");
                        b.Append("<td style='text-align:left;white-space: nowrap;padding-left:4px;width:10%'>" + dr["TnxDate"].ToString() + "</td>");
                        b.Append("<td style='text-align:left;white-space: nowrap;padding-left:4px;width:20%'>" + dr["SubCatNameDisplayName"].ToString() + "</td>");
                        b.Append("<td style='text-align:left;white-space: nowrap;padding-left:4px;width:45%'>" + dr["ItemName"].ToString() + "</td>");
                        b.Append("<td style='text-align:right;white-space: nowrap;padding-right:4px;width:5%'>" + dr["Qty"].ToString() + "</td>");
                        b.Append("<td style='text-align:right;white-space: nowrap;padding-right:4px;width:15%'>" + Convert.ToDecimal(dr["amount"]).ToString(".00") + "</td>");
                        b.Append("</tr>");
                        tAmount = tAmount + Convert.ToDecimal(dr["amount"]);
                    }
                }
                b.Append("<tr style='background:#fff'>");
                b.Append("<td colspan='6' style='text-align:right;white-space: nowrap;padding-right:4px;hight:15px'><hr/></td>");
                b.Append("</tr>");

                b.Append("<tr style='background:#fff'>");
                b.Append("<td colspan='4' style='text-align:right;white-space: nowrap;padding-right:4px;width:5%;font-size:13px;'><b>Item wise Total<b></td>");
                b.Append("<td style='text-align:center;white-space: nowrap;padding-left:4px;width:5%;font-size:13px;'>:<b></td>");
                b.Append("<td style='text-align:right;white-space: nowrap;padding-right:4px;width:15%;font-size:13px;'><b>" + Convert.ToDecimal(tAmount).ToString(".00") + "<b></td>");
                b.Append("</tr>");

                b.Append("<tr style='background:#fff'>");
                b.Append("<td colspan='4' style='text-align:right;white-space: nowrap;padding-left:4px;width:5%;font-size:13px;'><b>Package Discount Amount<b></td>");
                b.Append("<td style='text-align:center;white-space: nowrap;padding-left:4px;width:5%;font-size:13px;'>:<b></td>");
                b.Append("<td style='text-align:right;white-space: nowrap;padding-right:4px;width:15%;font-size:13px;'><b>" + Convert.ToDecimal(tAmount - PackageAmount).ToString(".00") + "<b></td>");
                b.Append("</tr>");

                b.Append("<tr style='background:#fff'>");
                b.Append("<td colspan='4' style='text-align:right;white-space: nowrap;padding-left:4px;width:5%;font-size:13px;'><b>Net Package Amount<b></td>");
                b.Append("<td style='text-align:center;white-space: nowrap;padding-left:4px;width:5%;font-size:13px;'>:<b></td>");
                b.Append("<td style='text-align:right;white-space: nowrap;padding-right:4px;width:15%;font-size:13px;'><b>" + PackageAmount.ToString(".00") + "<b></td>");
                b.Append("</tr>");


                b.Append("</table>");



            }
            if (_BillPrintType != "IncludingPackagedItem")
            {
                double GrossAmount = 0;
                double PanelDiscount = 0;
                double AdlDiscount = 0;
                double TotalDiscount = 0;
                double NetAmount = 0;
                double ReceivedAmount = 0;
                double PanelApprovedAmount = 0;
                double BalanceAmount = 0;
                foreach (DataRow dr in ds.Tables[4].Rows)
                {
                    GrossAmount = Convert.ToDouble(dr["TotalAmount"].ToString());
                    PanelDiscount = Convert.ToDouble(dr["PanelDiscount"].ToString());
                    AdlDiscount = Convert.ToDouble(dr["AdlDiscount"].ToString());
                    TotalDiscount = Convert.ToDouble(dr["TotalDiscount"].ToString());
                    NetAmount = Convert.ToDouble(dr["NetAmount"].ToString());
                    ReceivedAmount = Convert.ToDouble(dr["Received"].ToString());
                    PanelApprovedAmount = Convert.ToDouble(dr["PanelApprovedAmount"].ToString());
                    BalanceAmount = Convert.ToDouble(dr["BalanceAmount"].ToString());

                }
                //Bottom info			
                b.Append("<div style='width:100%;float:left;margin-top:5px'>");
                b.Append("<hr/>");
                b.Append("<div style='width:60%;float:left'>");
                b.Append("<label style='padding:2px 5px;'><b>Advance/Return Details</b></label>");
                b.Append("<table border='1' style='width:100%;font-size:12px;border-collapse: collapse;margin-top:10px;'>");
                b.Append("<tr>");
                //b.Append("<th style='width:1%;text-align:left;padding-left:4px;'>S.No.</th>");
                b.Append("<th style='text-align:center;padding-left:4px;'>Receipt No</th>");
                b.Append("<th style='text-align:center;padding-left:4px;'>Receipt Date</th>");
                b.Append("<th style='text-align:right;padding-right:4px;'>Amount</th>");
                b.Append("<th style='text-align:center;padding-right:4px;'>Pay Mode</th>");
                b.Append("</tr>");
                int Count = 0;
                if (ds.Tables.Count > 0 && ds.Tables[3].Rows.Count > 0)
                {
                    foreach (DataRow dr in ds.Tables[3].Rows)
                    {
                        Count++;
                        b.Append("<tr>");
                        //  b.Append("<td style='padding-left:4px;'>" + Count + "</td>");
                        b.Append("<td style='text-align:center;white-space: nowrap;padding-left:4px;'>" + dr["ReceiptNo"].ToString() + "</td>");
                        b.Append("<td style='text-align:center;white-space: nowrap;padding-left:4px;'>" + dr["receiptDate"].ToString() + "</td>");
                        b.Append("<td style='text-align:right;padding-right:4px;'>" + Convert.ToDecimal(dr["Amount"]).ToString("0.00") + "</td>");
                        b.Append("<td style='text-align:center;white-space: nowrap;padding-left:4px;'>" + dr["PayMode"].ToString() + "</td>");
                        b.Append("</tr>");
                    }
                }
                b.Append("</table>");
                b.Append("</div>");
                b.Append("<div style='width:40%;float:right'>");
                b.Append("<table style='font-size:12px;float:right' border='0' cellspacing='0'>");

                b.Append("<tr style='font-size:13px'>");
                b.Append("<td colspan='2' style='padding:3px 0;width:55%;text-align:left'><b>Net Amount (a-(b+c)) </b></td>");
                b.Append("<td style='padding:3px 0;width:10%;text-align:center;border-bottom:1px solid #000'><b> : </b></td>");
                b.Append("<td style='padding:3px 0;width:10%;text-align:center;border-bottom:1px solid #000'><b> Rs. </b></td>");
                b.Append("<td style='padding:3px 0;width:25%;text-align:right;white-space: nowrap;border-bottom:1px solid #000'><b>" + NetAmount.ToString("0.00") + "</b></td>");
                b.Append("</tr>");

                b.Append("<tr style='font-size:13px'>");
                b.Append("<td colspan='2' style='width:55%;text-align:left'><b>Gross Amount(a)</b></td>");
                b.Append("<td style='width:10%;text-align:center'><b> : </b></td>");
                b.Append("<td style='width:10%;text-align:center'><b> Rs. </b></td>");
                b.Append("<td style='width:25%;text-align:right;white-space: nowrap;'><b>" + GrossAmount.ToString("0.00") + "</b></td>");
                b.Append("</tr>");
                b.Append("<tr style='font-size:13px'>");
                b.Append("<td colspan='2' style='width:55%;text-align:left'><b>Panel Discount(b)</b></td>");
                b.Append("<td style='width:10%;text-align:center'><b> : </b></td>");
                b.Append("<td style='width:10%;text-align:center'><b> Rs. </b></td>");
                b.Append("<td style='width:25%;text-align:right;white-space: nowrap;'><b>" + PanelDiscount.ToString("0.00") + "</b></td>");
                b.Append("</tr>");
                b.Append("<tr style='font-size:13px;'>");
                b.Append("<td colspan='2' style='padding:3px 0;width:55%;text-align:left;'><b>Adl. Discount(c)</b></td>");
                b.Append("<td style='padding:3px 0;width:10%;text-align:center;border-bottom:1px solid #000'><b> : </b></td>");
                b.Append("<td style='padding:3px 0;width:10%;text-align:center;border-bottom:1px solid #000'><b> Rs. </b></td>");
                b.Append("<td style='padding:3px 0;width:25%;text-align:right;white-space: nowrap;border-bottom:1px solid #000'><b>" + AdlDiscount.ToString("0.00") + "</b></td>");
                b.Append("</tr>");
                b.Append("<tr style='font-size:13px'>");
                b.Append("<td colspan='2' style='padding:3px 0;width:55%;text-align:left'><b>Total Discount(b+c) </b></td>");
                b.Append("<td style='padding:3px 0;width:10%;text-align:center'><b> : </b></td>");
                b.Append("<td style='padding:3px 0;width:10%;text-align:center'><b> Rs. </b></td>");
                b.Append("<td style='padding:3px 0;width:25%;text-align:right;white-space: nowrap;'><b>" + TotalDiscount.ToString("0.00") + "</b></td>");
                b.Append("</tr>");


                b.Append("<tr style='font-size:13px'>");
                b.Append("<td colspan='2' style='padding:3px 0;width:55%;text-align:left'><b>Received Amount </b></td>");
                b.Append("<td style='padding:3px 0;width:10%;text-align:center'><b> : </b></td>");
                b.Append("<td style='padding:3px 0;width:10%;text-align:center'><b> Rs. </b></td>");
                b.Append("<td style='padding:3px 0;width:25%;text-align:right;white-space: nowrap;'><b>" + ReceivedAmount.ToString("0.00") + "</b></td>");
                b.Append("</tr>");

                if (PanelApprovedAmount > 0)
                {
                    b.Append("<tr style='font-size:13px'>");
                    b.Append("<td colspan='2' style='padding:3px 0;width:55%;text-align:left'><b>Panel Approval Amount </b></td>");
                    b.Append("<td style='padding:3px 0;width:10%;text-align:center'><b> : </b></td>");
                    b.Append("<td style='padding:3px 0;width:10%;text-align:center'><b> Rs. </b></td>");
                    b.Append("<td style='padding:3px 0;width:25%;text-align:right;white-space: nowrap;'><b>" + PanelApprovedAmount.ToString("0.00") + "</b></td>");
                    b.Append("</tr>");
                }

                b.Append("<tr style='font-size:13px'>");
                b.Append("<td colspan='2' style='padding:3px 0;width:55%;text-align:left'><b>Balance Amount </b></td>");
                b.Append("<td style='padding:3px 0;width:10%;text-align:center'><b> : </b></td>");
                b.Append("<td style='padding:3px 0;width:10%;text-align:center'><b> Rs. </b></td>");
                b.Append("<td style='padding:3px 0;width:25%;text-align:right;white-space: nowrap;'><b>" + BalanceAmount.ToString("0.00") + "</b></td>");
                b.Append("</tr>");


                b.Append("</table>");
                b.Append("</div>");
                b.Append("</div>");
            }

            pdfConverter.Header_Enabled = true;
            pdfConverter.Footer_Enabled = true;
            pdfConverter.Footer_Hight = 17;
            pdfConverter.Header_Hight = 70;
            pdfConverter.PageMarginLeft = 20;
            pdfConverter.PageMarginRight = 15;
            pdfConverter.PageMarginBottom = 10;
            pdfConverter.HeaderSource = "IPDBill";
            pdfConverter.PageMarginTop = 10;
            pdfConverter.PageMarginTop = 10;
            pdfConverter.PageName = "A4";
            pdfConverter.PageOrientation = "Portrait";
            return pdfConverter.ConvertToPdf(h.ToString(), b.ToString(), "-", "Print-Dispatch-Invoice.pdf");
        }
        public FileResult IPDDischargeReport(string _IPDNo)
        {
            PdfGenerator pdfConverter = new PdfGenerator();
            IPDInfo obj = new IPDInfo();
            obj.IPDNo = _IPDNo;
            obj.UHID = "-";
            obj.DoctorId = "-";
            obj.from = "1900/01/01";
            obj.to = "1900/01/01";
            obj.Prm1 = "-";
            obj.Prm2 = "-";
            obj.login_id = "-";
            obj.Logic = "DischargeReport";
            HISWebApi.Models.dataSet dsResult = APIProxy.CallWebApiMethod("IPDBilling/IPD_BillingQuerries", obj);
            DataSet ds = dsResult.ResultSet;

            string _result = string.Empty;
            StringBuilder b = new StringBuilder();
            StringBuilder h = new StringBuilder();
            string UHID = "";
            string patient_name = "";
            string ageInfo = "";
            string AdmitDate = "";
            string IPDNo = "";
            string DoctorName = "";
            string roomFullName = "";
            string PanelName = "";
            string DischargeDate = "";
            string Department = "";
            string BillNo = "";
            string BillDate = "";
            string ContactNo = "";
            string DischargeType = "";
            string Address = "";

            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                UHID = dr["UHID"].ToString();
                IPDNo = dr["IPDNo"].ToString();
                patient_name = dr["patient_name"].ToString();
                ageInfo = dr["ageInfo"].ToString();
                AdmitDate = dr["AdmitDate"].ToString();
                DoctorName = dr["DoctorName"].ToString();
                roomFullName = dr["roomFullName"].ToString();
                PanelName = dr["PanelName"].ToString();
                DischargeDate = dr["DischargeDate"].ToString();
                Department = dr["Department"].ToString();
                BillNo = dr["BillNo"].ToString();
                BillDate = dr["BillDate"].ToString();
                DischargeType = dr["DischargeType"].ToString();
                ContactNo = dr["ContactNo"].ToString();
                Address = dr["Address"].ToString();
            }
            h.Append("<table style='width:2080px;padding:10px 0;background:#fff;font-size:42px;text-align:left;border:1px solid #000;margin:0 15px'>");
            //h.Append("<tr>");
            //h.Append("<td colspan='7' style='font-size:62px;text-align:center'>Final Bill Detail<hr/ style='margin:7px'></td>");
            //h.Append("</tr>");
            h.Append("<tr>");
            h.Append("<td style='width:17%;padding:3px;'><b>UHID</b></td>");
            h.Append("<td style='width:1%;'><b>:</b></td>");
            h.Append("<td style='width:32%;text-aligh:left'>" + UHID + "</td>");
            h.Append("<td style='width:1%;'>&nbsp;</td>");
            h.Append("<td style='width:20%;'><b>Bill No</b></td>");
            h.Append("<td style='width:1%;'><b>:</b></td>");
            h.Append("<td style='width:29%;'>" + BillNo + "</td>");
            h.Append("</tr>");

            h.Append("<tr>");
            h.Append("<td style='width:17%;'><b>IPD No</b></td>");
            h.Append("<td style='width:1%;'><b>:</b></td>");
            h.Append("<td style='width:32%;'>" + IPDNo + "</td>");
            h.Append("<td style='width:1%;'>&nbsp;</td>");
            h.Append("<td style='width:20%;'><b>Bill Date</b></td>");
            h.Append("<td style='width:1%;'><b>:</b></td>");
            h.Append("<td style='width:29%;'>" + BillDate + "</td>");
            h.Append("</tr>");
            h.Append("<tr>");
            h.Append("<td style='width:17%;'><b>Patient Name</b></td>");
            h.Append("<td style='width:1%;'><b>:</b></td>");
            h.Append("<td style='width:32%;'>" + patient_name + "</td>");
            h.Append("<td style='width:1%;'>&nbsp;</td>");
            h.Append("<td style='width:20%;'><b>Panel</b></td>");
            h.Append("<td style='width:1%;'><b>:</b></td>");
            h.Append("<td style='width:29%;'>" + PanelName + "</td>");
            h.Append("</tr>");

            b.Append("<h2 style='text-align:center;font-weight:bold;text-decoration: underline;'>IPD Discharge Report</h2>");
            b.Append("<table style='padding:10px 0;background:#fff;width:100%;font-size:15px;text-align:left;border:1px solid #000;margin-bottom:-15px;margin-top:0'>");
            b.Append("<tr>");
            b.Append("<td style='width:17%;padding:3px;'><b>UHID</b></td>");
            b.Append("<td style='width:1%;'><b>:</b></td>");
            b.Append("<td style='width:32%;text-aligh:left'>" + UHID + "</td>");
            b.Append("<td style='width:1%;'>&nbsp;</td>");
            b.Append("<td style='width:20%;'><b>Bill No</b></td>");
            b.Append("<td style='width:1%;'><b>:</b></td>");
            b.Append("<td style='width:29%;'>" + BillNo + "</td>");
            b.Append("</tr>");

            b.Append("<tr>");
            b.Append("<td style='width:17%;'><b>IPD No</b></td>");
            b.Append("<td style='width:1%;'><b>:</b></td>");
            b.Append("<td style='width:32%;'>" + IPDNo + "</td>");
            b.Append("<td style='width:1%;'>&nbsp;</td>");
            b.Append("<td style='width:20%;'><b>Bill Date</b></td>");
            b.Append("<td style='width:1%;'><b>:</b></td>");
            b.Append("<td style='width:29%;'>" + BillDate + "</td>");
            b.Append("</tr>");
            b.Append("<tr>");
            b.Append("<td style='width:17%;'><b>Patient Name</b></td>");
            b.Append("<td style='width:1%;'><b>:</b></td>");
            b.Append("<td style='width:32%;'>" + patient_name + "</td>");
            b.Append("<td style='width:1%;'>&nbsp;</td>");
            b.Append("<td style='width:20%;'><b>Panel</b></td>");
            b.Append("<td style='width:1%;'><b>:</b></td>");
            b.Append("<td style='width:29%;'>" + PanelName + "</td>");
            b.Append("</tr>");

            b.Append("<tr>");
            b.Append("<td style='width:17%;padding:3px;'><b>Age</b></td>");
            b.Append("<td style='width:1%;'><b>:</b></td>");
            b.Append("<td style='width:32%;text-aligh:left'>" + ageInfo + "</td>");
            b.Append("<td style='width:1%;'>&nbsp;</td>");
            b.Append("<td style='width:20%;'><b>Doctor Name</b></td>");
            b.Append("<td style='width:1%;'><b>:</b></td>");
            b.Append("<td style='width:29%;'>" + DoctorName + "</td>");
            b.Append("</tr>");

            b.Append("<tr>");
            b.Append("<td style='width:17%;'><b>Contact No </b></td>");
            b.Append("<td style='width:1%;'><b>:</b></td>");
            b.Append("<td style='width:32%;'>" + ContactNo + "</td>");
            b.Append("<td style='width:1%;'>&nbsp;</td>");
            b.Append("<td style='width:20%;'><b>Discharge Type</b></td>");
            b.Append("<td style='width:1%;'><b>:</b></td>");
            b.Append("<td style='width:29%;'>" + DischargeType + "</td>");
            b.Append("</tr>");

            b.Append("<tr>");
            b.Append("<td style='width:17%;'><b>Address</b></td>");
            b.Append("<td style='width:1%;'><b>:</b></td>");
            b.Append("<td style='width:32%;'>" + Address + "</td>");
            b.Append("<td style='width:1%;'>&nbsp;</td>");
            b.Append("<td style='width:20%;'><b>Admission Date Time</b></td>");
            b.Append("<td style='width:1%;'><b>:</b></td>");
            b.Append("<td style='width:29%;'>" + AdmitDate + "</td>");
            b.Append("</tr>");

            b.Append("<tr>");
            b.Append("<td style='width:17%;'><b></b></td>");
            b.Append("<td style='width:1%;'><b></b></td>");
            b.Append("<td style='width:32%;'></td>");
            b.Append("<td style='width:1%;'>&nbsp;</td>");
            b.Append("<td style='width:20%;'><b>Discharge Date Time</b></td>");
            b.Append("<td style='width:1%;'><b>:</b></td>");
            b.Append("<td style='width:29%;'>" + DischargeDate + "</td>");
            b.Append("</tr>");
            b.Append("</table></br/>");

            if (ds.Tables.Count > 0 && ds.Tables[1].Rows.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[1].Rows)
                {
                    b.Append("<div style='width:100%;margin-top:10px'>");
                    b.Append("<label style='padding:2px 5px;margin-top:20px'><b>" + dr["HeaderName"].ToString() + "</b><hr/ style='margin:0;'></label>");
                    b.Append(dr["template_content"].ToString());
                    b.Append("</div>");
                }
            }
            b.Append("</table>");

            pdfConverter.Header_Enabled = true;
            pdfConverter.Footer_Enabled = true;
            pdfConverter.Footer_Hight = 17;
            pdfConverter.Header_Hight = 70;
            pdfConverter.PageMarginLeft = 20;
            pdfConverter.PageMarginRight = 15;
            pdfConverter.PageMarginBottom = 10;
            pdfConverter.HeaderSource = "IPDDischargeReport";
            pdfConverter.PageMarginTop = 10;
            pdfConverter.PageMarginTop = 10;
            pdfConverter.PageName = "A4";
            pdfConverter.PageOrientation = "Portrait";
            return pdfConverter.ConvertToPdf(h.ToString(), b.ToString(), "-", "Print-Dispatch-Invoice.pdf");
        }

    }
}