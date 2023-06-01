using HIS.Repository;
using HISWebApi.Models;
using MediSoftTech_HIS.App_Start;
using System.Data;
using System.Text;
using System.Web.Mvc;

namespace MediSoftTech_HIS.Areas.Dietician.Controllers
{
    public class PrintController : Controller
    {
        public FileResult GetKitchenReqSlip(string from, string to, string Category, string Floor)
        {
            BarcodeGenerator generateBarcode = new BarcodeGenerator();
            PdfGenerator pdfConverter = new PdfGenerator();
            ipDietician obj = new ipDietician();
            obj.IPDNo = "";
            obj.from = from;
            obj.to = to;
            obj.Prm1 = Category;
            obj.Prm2 = Floor;
            obj.Logic = "GetKitchenReqSlip";
            dataSet dsResult = APIProxy.CallWebApiMethod("Dietician/diet_DiticianQueries", obj);

            DataSet ds = dsResult.ResultSet;
            string _result = string.Empty;
            StringBuilder b = new StringBuilder();
            StringBuilder h = new StringBuilder();
            b.Append("<div style='width:100%;float:left;'>");
            string headerImageFile = HttpContext.Server.MapPath(@"/Content/logo/logo.png");
            b.Append("<div style='text-align:center;width:auto;'>");
            b.Append("<h3 style='font-weight:bold;text-align:center;text-decoration:underline'>Diet Requisition Slip</h3>");
            b.Append("</div>");
            b.Append("</div>");
            b.Append("<div style='background:#e7e4e4;padding:10px;border-radius:3px;border:1px solid #ccc'>");
            b.Append("<table style='width:100%;font-size:14px;text-align:left;border-collapse:collapse' border='0' >");
            b.Append("<tr>");
            b.Append("<th style='width:40px'>From</th>");
            b.Append("<th style='width:10px'>:</th>");
            b.Append("<th>" + from.Split('-')[2] + "-" + from.Split('-')[1] + "-" + from.Split('-')[0] + "</th>");
            b.Append("<th>&nbsp;</th>");
            b.Append("<th style='width:30px'>To</th>");
            b.Append("<th style='width:10px'>:</th>");
            b.Append("<th>" + to.Split('-')[2] + "-" + to.Split('-')[1] + "-" + to.Split('-')[0] + "</th>");
            b.Append("<th>&nbsp;</th>");
            b.Append("<th style='width:60px'>Category</th>");
            b.Append("<th style='width:10px'>:</th>");
            b.Append("<th>" + Category + "</th>");
            b.Append("<th>&nbsp;</th>");
            b.Append("<th style='width:40px'>Floor</th>");
            b.Append("<th style='width:10px'>:</th>");
            b.Append("<th>" + Floor + "</th>");
            b.Append("</tr>");
            b.Append("</table>");
            b.Append("</div>");
            b.Append("<table style='width:100%;font-size:10px;text-align:left;margin-top:5px;border-collapse:collapse;' border='1' >");
            b.Append("<tr>");
            b.Append("<th style='white-space:nowrap;padding-left:3px;padding-right:3px;'>IPD No</th>");
            b.Append("<th style='padding-left:3px;padding-right:3px;'>Patient Name</th>");
            b.Append("<th style='padding-left:3px;padding-right:3px;'>Room Bed Detail</th>");
            b.Append("<th style='width:20%;padding-left:3px;padding-right:3px;'>Diet Name</th>");
            b.Append("<th style='width:40%'>Item List</th>");
            b.Append("<th style='width:20%;padding-left:3px;padding-right:3px;'>remarks</th>");
            b.Append("</tr>");
            //Body			
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    b.Append("<tr>");
                    b.Append("<td style='padding-left:3px;padding-right:3px;font-size:14px'>" + dr["IPDNo"].ToString() + "</td>");
                    b.Append("<td style='padding-left:3px;padding-right:3px;'>" + dr["PatientName"].ToString() + "</td>");
                    b.Append("<td style='padding-left:3px;padding-right:3px;font-size:14px'>" + dr["RoomBedDetail"].ToString() + "</td>");
                    b.Append("<td style='padding-left:3px;padding-right:3px;'>" + dr["DietName"].ToString() + "</td>");
                    b.Append("<td style='padding-left:3px;padding-right:3px;font-size:14px'>" + dr["ItemList"].ToString() + "</td>");
                    b.Append("<td style='padding-left:3px;padding-right:3px;font-size:14px'>" + dr["remarks"].ToString() + "</td>");
                    b.Append("</tr>");
                }
            }
            b.Append("</table>");
            pdfConverter.Header_Enabled = false;
            pdfConverter.Footer_Enabled = false;
            pdfConverter.Header_Hight = 150;
            pdfConverter.PageMarginLeft = 10;
            pdfConverter.PageMarginRight = 10;
            pdfConverter.PageMarginBottom = 10;
            pdfConverter.PageMarginTop = 10;
            pdfConverter.PageMarginTop = 10;
            pdfConverter.PageName = "A4";
            pdfConverter.Browser_Width = 1150;
            pdfConverter.PageOrientation = "Landscap";
            return pdfConverter.ConvertToPdf(h.ToString(), b.ToString(), "-", "Diet_ReqSlip.pdf");
        }
        public FileResult ServiceReportSlip(string month)
        {
            BarcodeGenerator generateBarcode = new BarcodeGenerator();
            PdfGenerator pdfConverter = new PdfGenerator();
            ipDietician obj = new ipDietician();
            obj.IPDNo = "-";
            obj.from = month;
            obj.to = "1900/01/01";
            obj.Prm1 = "-";
            obj.Prm2 = "-";
            obj.Logic = "ServiceReport";
            dataSet dsResult = APIProxy.CallWebApiMethod("Dietician/diet_DiticianQueries", obj);

            DataSet ds = dsResult.ResultSet;
            string _result = string.Empty;
            StringBuilder b = new StringBuilder();
            StringBuilder h = new StringBuilder();
            b.Append("<div style='width:100%;float:left;'>");
            string chandanLogo = HttpContext.Server.MapPath(@"/Content/logo/logo.png");
            b.Append("<div style='text-align:left;width:30%;float:left'>");
            b.Append("<img src=" + chandanLogo + " style='width:170px;margin-top:5px;' />");
            b.Append("</div>");
            b.Append("<div style='text-align:left;width:auto;float:left;width:70%;'>");
            b.Append("<h2 style='font-weight:bold;margin:0;text-align:center'>CHANDAN HOSPITAL</h2>");
            b.Append("<h5 style='text-align:center;margin:0;'>(A UNIT OF CHANDAN HOSPITAL LTD)</h5>");
            b.Append("<h5 style='text-align:center;margin:0;'>HOSPITAL BUILDING, VIJAYANTKHAND,</h5>");
            b.Append("<h5 style='text-align:center;margin:0;'>GOMTINAGAR, FAIZABAD ROAD, LUCKNOW-226010</h5>");
            b.Append("<hr/>");
            b.Append("<h2 style='font-weight:bold;margin:0;text-align:center'>Indicator Capturing Format</h2>");
            b.Append("</div>");
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    b.Append("<h4 style='text-align:left;margin:0;'>Month : " + dr["rMonth"].ToString() + "</h4>");
                    b.Append("<table style='width:100%;font-size:14px;text-align:left;border-collapse:collapse;margin-top:20px' border='1'>");
                    b.Append("<tr>");
                    b.Append("<td colspan='3'></td>");
                    b.Append("<td style='padding:3px;'><b>NUMERATOR</b></td>");
                    b.Append("<td style='padding:3px;'><b>DENOMINATOR</b></td>");
                    b.Append("<td style='padding:3px;'><b>RESULT</b></td>");
                    b.Append("</tr>");
                    b.Append("<tr>");
                    b.Append("<td style='padding:3px;text-align:center;width:20%'><b>DIETICIAN-Incharge</b><br><br><br><br><br><br><br></td>");
                    b.Append("<td style='padding:3px;text-align:center'>" + dr["ch"].ToString() + "</td>");
                    b.Append("<td style='padding:3px;text-align:center'>Percentage of cases (inpatients) wherein screening for nutritional needs has been done.</td>");
                    b.Append("<td style='padding:3px;text-align:center'>"+dr["t_Numerator"].ToString()+"</td>");
                    b.Append("<td style='padding:3px;text-align:center'>"+ dr["t_Denominator"].ToString() + "</td>");
                    b.Append("<td style='padding:3px;text-align:center'>"+dr["Result"].ToString()+"%</td>");
                    b.Append("</tr>");
                    b.Append("</table>");
                }
            }           
            b.Append("<h5 style='text-align:left;margin:6px 0;'>Remark/Positive Indicators Details : Bodies below 6 month are not included. </h5>");
            b.Append("</div>");
            pdfConverter.Header_Enabled = false;
            pdfConverter.Footer_Enabled = false;
            pdfConverter.Header_Hight = 150;
            pdfConverter.PageMarginLeft = 10;
            pdfConverter.PageMarginRight = 10;
            pdfConverter.PageMarginBottom = 10;
            pdfConverter.PageMarginTop = 10;
            pdfConverter.PageMarginTop = 10;
            pdfConverter.PageName = "A4";
            pdfConverter.Browser_Width = 760;
            pdfConverter.PageOrientation = "Portrait";
            return pdfConverter.ConvertToPdf(h.ToString(), b.ToString(), "-", "Diet_ReqSlip.pdf");
        }
    }
}