using HIS.Repository;
using MediSoftTech_HIS.App_Start;
using System;
using System.Data;
using System.Text;
using System.Web.Mvc;
using static HISWebApi.Models.IPDBO;

namespace MediSoftTech_HIS.Areas.IPOPAudit.Controllers
{
	public class PrintController : Controller
	{
		// GET: IPOPAudit/Print
		public FileResult MedicineBillReport(string IPDNo, string pt_Name, string UHID, string Logic = "IPD:BillReport")
		{
            PdfGenerator pdfConverter = new PdfGenerator();
			ipIPDAudit obj = new ipIPDAudit();
			obj.IPDNo = IPDNo;
			obj.Logic = Logic;
			HISWebApi.Models.dataSet dsResult = APIProxy.CallWebApiMethod("IPOPAudit/IPD_MedicineAudit", obj);

			DataSet ds = dsResult.ResultSet;
			string _result = string.Empty;
			StringBuilder b = new StringBuilder();
			StringBuilder h = new StringBuilder();
			if (ds.Tables.Count > 0 && ds.Tables[1].Rows.Count > 0)
			{
				foreach (DataRow dr in ds.Tables[1].Rows)
				{
					h.Append("<h1 style='text-align:center'>Medical Store Bill Summary</h1>");
					h.Append("<table style='width:100%;font-size:20px;text-align:center;border:1px solid #dcdcdc;'>");
					h.Append("<tr>");
					h.Append("<td><b>UHID No : </b></td>");
					h.Append("<td>" + dr["uhid"].ToString() + "</td>");
					h.Append("<td><b>Patient Name : </b></td>");
					h.Append("<td>" + dr["pt_name"].ToString() + "</td>");
					h.Append("<td><b>IPD No : </b></td>");
					h.Append("<td>" + IPDNo + "</td>");
					h.Append("</tr>");
					h.Append("</table>");
				}
			}

			b.Append("<table style='width:100%;font-size:11px;border:1px solid #ddd;border-collapse: collapse;text-align:left;font-family:'Open Sans','Helvetica Neue', Helvetica, Arial, sans-serif'>");
			if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
			{
				string temp = string.Empty;
				int id = 1;
				decimal total = 0;
				decimal grandTotal = 0;
				int counter = 1;
				string str2 = string.Empty;
				foreach (DataRow dr in ds.Tables[0].Rows)
				{
					id++;
					grandTotal += Convert.ToDecimal(dr["amount"]);
					if (temp != dr["sale_inv_no"].ToString())
					{
						id = 1;
						if (counter > 1)
						{
							b.Append("<tr>");
							b.Append("<td style='height:10px' colspan='7'>&nbsp;</td>");
							b.Append("</tr>");
							b.Append("<tr style='background-color:#d3ecff;font-weight:bold;margin:25px 0px !important'>");
							b.Append("<td style='border:1px solid #dddddd;text-align:right' colspan='6'>Total :</td>");
							b.Append("<td style='border:1px solid #dddddd;text-align:right'>" + Convert.ToDecimal(total).ToString(".00") + "</td>");
							b.Append("</tr>");
							total = 0;
						}
						string str = "Sale Invoice No : " + dr["sale_inv_no"].ToString() + "  Indent No : " + dr["indentNo"].ToString() + " Indent Date : " + dr["sale_date"].ToString().Substring(0, 10);
						b.Append("<tr style='background-color:#dcdcdc;padding:20px;font-size:16px;'>");
						b.Append("<td colspan='7'>" + str + "</td>");
						b.Append("</tr>");
						b.Append("<tr>");
						b.Append("<th style='border:1px solid #dddddd;'>ID</th>");
						b.Append("<th style='border:1px solid #dddddd;'>Name Of Product</th>");
						b.Append("<th style='border:1px solid #dddddd;'>Batch No</th>");
						b.Append("<th style='border:1px solid #dddddd;'>Exp. Date</th>");
						b.Append("<th style='border:1px solid #dddddd;text-align:right'>USR</th>");
						b.Append("<th style='border:1px solid #dddddd;text-align:right'>Quantity</th>");
						b.Append("<th style='border:1px solid #dddddd;text-align:right'>Amount</th>");
						b.Append("</tr>");
						temp = dr["sale_inv_no"].ToString();
					}
					b.Append("<tr>");
					b.Append("<td style='border:1px solid #dddddd;'>" + id + "</td>");
					b.Append("<td style='border:1px solid #dddddd;'>" + dr["item_name"].ToString() + "</td>");
					b.Append("<td style='border:1px solid #dddddd;'>" + dr["batch_no"].ToString() + "</td>");
					b.Append("<td style='border:1px solid #dddddd;'>" + dr["exp_date"].ToString().Substring(0, 10) + "</td>");
					b.Append("<td style='border:1px solid #dddddd;text-align:right'>" + Convert.ToDecimal(dr["usr"]).ToString(".00") + "</td>");
					b.Append("<td style='border:1px solid #dddddd;text-align:right'>" + dr["sale_qty"].ToString() + "</td>");
					b.Append("<td style='border:1px solid #dddddd;text-align:right'>" + Convert.ToDecimal(dr["amount"]).ToString(".00") + "</td>");
					b.Append("</tr>");
					//b.Append("<tr><td>&nbsp;</td></tr>");
					total += Convert.ToDecimal(dr["amount"]);
					counter++;
				}
				b.Append("<tr>");
				b.Append("<td style='height:11px' colspan='7'>&nbsp;</td>");
				b.Append("</tr>");
				b.Append("<tr style='background-color:#d3ecff;font-weight:bold;margin:25px 0px !important;font-size:18px'>");
				b.Append("<td style='border:1px solid #dddddd;text-align:right;' colspan='6'>Grand Total :</td>");
				b.Append("<td style='border:1px solid #dddddd;text-align:right;'>" + Convert.ToDecimal(grandTotal).ToString(".00") + "</td>");
				b.Append("</tr>");
			}
			b.Append("</table>");
			pdfConverter.Header_Enabled = true;
			pdfConverter.Header_Hight = 70;
			pdfConverter.PageMarginLeft = 10;
			pdfConverter.PageMarginRight = 10;
			pdfConverter.PageMarginBottom = 10;
			pdfConverter.PageMarginTop = 10;
			pdfConverter.PageMarginTop = 10;
            pdfConverter.PageName = "A4";
            pdfConverter.PageOrientation = "Portrait";
            
            return pdfConverter.ConvertToPdf(h.ToString(), b.ToString(), "-", "MedicineBillReport.pdf");
		}
        public FileResult SaleItemReport(string IPDNo)
        {
            PdfGenerator pdfConverter = new PdfGenerator();
            ipIPDAudit obj = new ipIPDAudit();
            obj.IPDNo = IPDNo;
            obj.Logic = "IPD:SaleItemReport";
            HISWebApi.Models.dataSet dsResult = APIProxy.CallWebApiMethod("IPOPAudit/IPD_MedicineAudit", obj);

            DataSet ds = dsResult.ResultSet;
            string _result = string.Empty;
            StringBuilder b = new StringBuilder();
            StringBuilder h = new StringBuilder();
            h.Append("<h1 style='text-align:center'>Sale Items report</h1>");
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {               
                foreach (DataRow dr in ds.Tables[0].Rows)
                {                  
                    h.Append("<table style='margin:0px;width:100%;font-size:20px;border:1px solid #dcdcdc;'>");
                    h.Append("<tr>");
                    h.Append("<td><b>UHID No : </b></td>");
                    h.Append("<td style='text-align:left'>" + dr["uhid"].ToString() + "</td>");
                    h.Append("<td><b>Patient Name : </b></td>");
                    h.Append("<td style='text-align:left'>" + dr["pt_name"].ToString() + "</td>");
                    h.Append("<td><b>IPD No : </b></td>");
                    h.Append("<td style='text-align:left'>" + IPDNo + "</td>");
                    h.Append("</tr>");
                    h.Append("</table>");
                }
            }
            b.Append("<h1 style='margin:0px;font-size:19px;text-align:center'>Summary Bill</h1>");
            b.Append("<table border='0' style='margin:0px;width:100%;font-size:11px;border-collapse: collapse;text-align:left;font-family:'Open Sans','Helvetica Neue', Helvetica, Arial, sans-serif'>");
            b.Append("<tr>");
            b.Append("<th style='width:70%;padding-left:5px;'>Sale Inv No</th>");
            b.Append("<th style='padding-left:5px;text-align:right'>Total</th>");
            b.Append("<th style='padding-left:5px;text-align:right'>Discount</th>");
            b.Append("<th style='padding-left:5px;text-align:right'>Net</th>");
            b.Append("</tr>");
            if (ds.Tables.Count > 0 && ds.Tables[1].Rows.Count > 0)
            {                

                string temp = string.Empty;
                string saleInvNo = string.Empty;
                int id = 1;
                decimal total = 0;
                decimal grandTotal = 0;
                int counter = 1;
                string str2 = string.Empty;
                foreach (DataRow dr in ds.Tables[1].Rows)
                {
                    id++;
                    grandTotal += Convert.ToDecimal(dr["net"]);
                    if (temp != dr["sale_date"].ToString())
                    {
                        id = 1;
                        if (counter > 1)
                        {
                            b.Append("<tr>");
                            b.Append("<td style='height:1px' colspan='4'>&nbsp;</td>");
                            b.Append("</tr>");
                            b.Append("<tr style='background-color:#fff;font-weight:bold;'>");
                            b.Append("<td style='border-top:1px solid #000;border-bottom:1px solid #000;text-align:right' colspan='3'>Total :</td>");
                            b.Append("<td style='border-top:1px solid #000;border-bottom:1px solid #000;text-align:right'>" + Convert.ToDecimal(total).ToString(".00") + "</td>");
                            b.Append("</tr>");
                            b.Append("<tr>");
                            b.Append("<td style='height:1px' colspan='4'>&nbsp;</td>");
                            b.Append("</tr>");
                            total = 0;
                        }
                        string str = "Sale Date : " + dr["sale_date"].ToString().Substring(0, 10);
                        b.Append("<tr style='background-color:#dcdcdc;padding:20px;font-size:13px;'>");
                        b.Append("<td colspan='4'>" + str + "</td>");
                        b.Append("</tr>");                     
                        temp = dr["sale_date"].ToString();
                    }                  
                    b.Append("<tr>");                  
                    b.Append("<td style='padding-left:5px;'>" + dr["sale_inv_no"].ToString() + "</td>");  
                    b.Append("<td style='padding-left:5px;text-align:right'>" + Convert.ToDecimal(dr["total"]).ToString(".00") + "</td>");
                    b.Append("<td style='padding-left:5px;text-align:right'>" + Convert.ToDecimal(dr["discount"]).ToString(".00") + "</td>");
                    b.Append("<td style='padding-left:5px;text-align:right'>" + Convert.ToDecimal(dr["net"]).ToString(".00") + "</td>");
                    b.Append("</tr>");
                    //b.Append("<tr><td>&nbsp;</td></tr>");
                    total += Convert.ToDecimal(dr["net"]);
                    counter++;
                }
                b.Append("<tr>");
                b.Append("<td style='height:1px' colspan='7'>&nbsp;</td>");
                b.Append("</tr>");
                b.Append("<tr style='background-color:#fff;font-weight:bold;margin:15px 0px !important'>");
                b.Append("<td style='border-top:1px solid #000;border-bottom:1px solid #000;text-align:right' colspan='3'>Total :</td>");
                b.Append("<td style='border-top:1px solid #000;border-bottom:1px solid #000;text-align:right'>" + Convert.ToDecimal(total).ToString(".00") + "</td>");
                b.Append("</tr>");
                b.Append("<tr>");
                b.Append("<td style='height:1px' colspan='7'>&nbsp;</td>");
                b.Append("</tr>");
                b.Append("<tr style='background-color:#fff;font-weight:bold;margin:25px 0px !important;font-size:14px'>");
                b.Append("<td style='border-top:1px solid #000;border-bottom:1px solid #000;text-align:right;' colspan='3'>Grand Total :</td>");
                b.Append("<td style='border-top:1px solid #000;border-bottom:1px solid #000;text-align:right;'>" + Convert.ToDecimal(grandTotal).ToString(".00") + "</td>");
                b.Append("</tr>");
            }
            b.Append("</table>");
            //decare
            b.Append("<h3 style='page-break-after: always;'></h3>");

            b.Append("<h1 style='margin:0;font-size:19px;text-align:center'>Detail Bill</h1>");
            b.Append("<table border='0' style='margin:0;width:100%;font-size:11px;border-collapse: collapse;text-align:left;font-family:'Open Sans','Helvetica Neue', Helvetica, Arial, sans-serif'>");
            if (ds.Tables.Count > 0 && ds.Tables[2].Rows.Count > 0)
            {
                string temp = string.Empty;
                string saleInvNo = string.Empty;
                int id = 1;
                decimal total = 0;
                decimal grandTotal = 0;
                int counter = 1;
                string str2 = string.Empty;
                foreach (DataRow dr in ds.Tables[2].Rows)
                {
                    id++;
                    grandTotal += Convert.ToDecimal(dr["amount"]);
                    if (temp != dr["sale_date"].ToString())
                    {
                        id = 1;
                        if (counter > 1)
                        {
                            b.Append("<tr>");
                            b.Append("<td style='height:2px' colspan='7'>&nbsp;</td>");
                            b.Append("</tr>");

                            b.Append("<tr style='background-color:#fff;font-weight:bold;'>");
                            b.Append("<td style='border-top:1px solid #000;border-bottom:1px solid #000;text-align:right' colspan='7'>Total :</td>");
                            b.Append("<td style='border-top:1px solid #000;border-bottom:1px solid #000;text-align:right'>" + Convert.ToDecimal(total).ToString(".00") + "</td>");
                            b.Append("</tr>");
                            b.Append("<tr>");
                            b.Append("<td style='height:2px' colspan='7'>&nbsp;</td>");
                            b.Append("</tr>");
                            total = 0;
                        }
                        string str = "Sale Date : " + dr["sale_date"].ToString().Substring(0, 10);
                        b.Append("<tr style='background-color:#dcdcdc;padding:20px;font-size:13px;'>");
                        b.Append("<td colspan='8'>" + str + "</td>");
                        b.Append("</tr>");
                        b.Append("<tr>");
                        b.Append("<th style='padding-left:5px;'>ID</th>");                  
                        b.Append("<th style='padding-left:5px;'>Name Of Product</th>");
                        b.Append("<th style='padding-left:5px;white-space:pre'>Batch No</th>");
                        b.Append("<th style='padding-left:5px;'>Exp. Date</th>");
                        b.Append("<th style='margin-left:5px;padding-left:5px;text-align:right'>MRP</th>");
                        b.Append("<th style='margin-left:5px;padding-left:5px;text-align:right'>USR</th>");
                        b.Append("<th style='margin-left:5px;padding-left:5px;text-align:right'>Qty</th>");
                        b.Append("<th style='margin-left:5px;padding-left:5px;text-align:right'>Amount</th>");
                        b.Append("</tr>");
                        temp = dr["sale_date"].ToString();
                    }
                    if (saleInvNo != dr["sale_inv_no"].ToString())
                    {
                        string str = "Sale Inv No : " + dr["sale_inv_no"].ToString();
                        b.Append("<tr>");
                        b.Append("<td colspan='8' style='border-bottom:1px solid #ddd;padding-top:8px'><b>" + str + "</b></td>");
                        b.Append("</tr>");
                        saleInvNo = dr["sale_inv_no"].ToString();
                    }
                    b.Append("<tr>");
                    b.Append("<td style='padding-left:5px;'>" + id + "</td>");                  
                    b.Append("<td style='padding-left:5px;'>" + dr["item_name"].ToString() + "</td>");
                    b.Append("<td style='padding-left:5px;'>" + dr["batch_no"].ToString() + "</td>");
                    b.Append("<td style='padding-left:5px;'>" + dr["exp_date"].ToString().Substring(0, 10) + "</td>");
                    b.Append("<td style='margin-left:5px;padding-left:5px;text-align:right'>" + Convert.ToDecimal(dr["mrp"]).ToString(".00") + "</td>");
                    b.Append("<td style='margin-left:5px;padding-left:5px;text-align:right'>" + Convert.ToDecimal(dr["usr"]).ToString(".00") + "</td>");
                    b.Append("<td style='margin-left:5px;padding-left:5px;text-align:right'>" + dr["sale_qty"].ToString() + "</td>");
                    b.Append("<td style='margin-left:5px;padding-left:5px;text-align:right'>" + Convert.ToDecimal(dr["amount"]).ToString(".00") + "</td>");
                    b.Append("</tr>");
                    //b.Append("<tr><td>&nbsp;</td></tr>");
                    total += Convert.ToDecimal(dr["amount"]);
                    counter++;
                }
                b.Append("<tr>");
                b.Append("<td style='height:1px' colspan='7'>&nbsp;</td>");
                b.Append("</tr>");

                b.Append("<tr style='background-color:#fff;font-weight:bold;'>");
                b.Append("<td style='border-top:1px solid #000;border-bottom:1px solid #000;text-align:right' colspan='7'>Total :</td>");
                b.Append("<td style='border-top:1px solid #000;border-bottom:1px solid #000;text-align:right'>" + Convert.ToDecimal(total).ToString(".00") + "</td>");
                b.Append("</tr>");

                b.Append("<tr>");
                b.Append("<td style='height:1px' colspan='7'>&nbsp;</td>");
                b.Append("</tr>");

                b.Append("<tr style='background-color:#fff;font-weight:bold;margin:25px 0px !important;font-size:14px'>");
                b.Append("<td style='border-top:1px solid #000;border-bottom:1px solid #000;text-align:right;' colspan='7'>Grand Total :</td>");
                b.Append("<td style='border-top:1px solid #000;border-bottom:1px solid #000;text-align:right;'>" + Convert.ToDecimal(grandTotal).ToString(".00") + "</td>");
                b.Append("</tr>");
            }
            b.Append("</table>"); 
            //decare
            pdfConverter.Header_Enabled = true;
            pdfConverter.Header_Hight = 50;
            pdfConverter.PageMarginLeft = 10;
            pdfConverter.PageMarginRight = 10;
            pdfConverter.PageMarginBottom = 10;
            pdfConverter.PageMarginTop = 10;
            pdfConverter.Footer_Enabled = true;
            pdfConverter.Footer_Hight = 20;
            pdfConverter.PageMarginTop = 10;
            pdfConverter.PageName = "A4";
            pdfConverter.PageOrientation = "Portrait";

            return pdfConverter.ConvertToPdf(h.ToString(), b.ToString(), "-", "MedicineBillReport.pdf");
        }
    }
}