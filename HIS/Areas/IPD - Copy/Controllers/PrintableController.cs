using HIS.Repository;
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
			b.Append("<table style='background:#f5f5f5;width:100%;font-size:14px;text-align:left;border:0px solid #dcdcdc;margin-bottom:-15px'>");
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

                    //b.Append("<tr>");
                    //b.Append("<th style='text-align:right;padding-left:5px;' colspan='9'>Amount of Tax Subject to Reverse Charge</th>");
                    //b.Append("<td style='font-size:12px;text-align:right;padding-right:2px' colspan='2'> " + Convert.ToDecimal(dr1["cgst"]).ToString(".00") + "</td>");
                    //b.Append("<td style='font-size:12px;text-align:right;padding-right:2px' colspan='2'> " + Convert.ToDecimal(dr1["sgst"]).ToString(".00") + "</td>");
                    //b.Append("</tr>");
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

                    f.Append("<tr>");
                    f.Append("<td colspan='3' style='width:12%;text-align:right;'><b> (Amount Inclusive of GST Rs. "+ Convert.ToDecimal(dr1["tax"]).ToString(".00") + ")</b></td>");
                    f.Append("</tr>");

                    f.Append("</table>");
                    f.Append("<b style='text-align:center;font-size:15px;'>Received</b>&nbsp;&nbsp;");
                    f.Append("<b style='min-width:95px;text-align:right;border:1px solid #000;padding:1px 3px;float:right;font-size:15px'> " + Convert.ToDecimal(dr1["received"]).ToString(".00") + "</b>");
                    f.Append("</div>");
                    f.Append("<div style='width:45%;float:left;'>");
                    f.Append("<img src=" + BarcodeGenerator.GenerateBarCode(dr1["barcode"].ToString(), 400, 70)  + " style='float:right' />");
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
                b.Append("<span style='width:30%;font-size:16px;'><b>DATE : " +Convert.ToDateTime(date).ToString("dd-MM-yyyy") + "</span>");
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
                        if(ds.Tables[0].Rows[i][j].ToString()=="0") 
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


    }
}