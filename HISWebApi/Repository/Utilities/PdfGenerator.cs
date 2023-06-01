using HISWebApi.Models;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;

namespace HISWebApi.Repository.Utilities
{
    public class PdfGenerator
    {
        public HttpResponseMessage SetHeaderandFooter(out string result, byte[] data, string filePath)
        {
            HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
            try
            {
                PdfReader pdfReader = new PdfReader(new MemoryStream(data));
                PdfStamper pdfStamper = new PdfStamper(pdfReader, new FileStream(filePath, FileMode.Create));
                for (int pageIndex = 1; pageIndex <= pdfReader.NumberOfPages; pageIndex++)
                {
                    pdfStamper.FormFlattening = false;
                    iTextSharp.text.Rectangle pageRectangle = pdfReader.GetPageSizeWithRotation(pageIndex);
                    //header eraser
                    PdfContentByte pdfData = pdfStamper.GetOverContent(pageIndex);
                    pdfData.BeginText();
                    //Write some content
                    PdfPTable headerTable = new PdfPTable(1);
                    headerTable.WidthPercentage = 100;
                    headerTable.HorizontalAlignment = Element.ALIGN_LEFT;

                    System.Drawing.Image img_up = System.Drawing.Image.FromFile("I:\\Hospital\\PrescriptionByHospital\\header.jpg");
                    iTextSharp.text.Image img_up_eraser = iTextSharp.text.Image.GetInstance(img_up, System.Drawing.Imaging.ImageFormat.Jpeg);

                    PdfPCell cell_up_eraser = new PdfPCell(img_up_eraser);
                    cell_up_eraser.HorizontalAlignment = Element.ALIGN_LEFT;
                    cell_up_eraser.VerticalAlignment = Element.ALIGN_MIDDLE;
                    cell_up_eraser.BorderColor = iTextSharp.text.BaseColor.WHITE;
                    cell_up_eraser.BackgroundColor = iTextSharp.text.BaseColor.WHITE;
                    headerTable.AddCell(cell_up_eraser);

                    var ct = new ColumnText(pdfData);
                    ct.Alignment = Element.ALIGN_CENTER;
                    ct.SetSimpleColumn(5, 10, 590, 830);//adjust only last value to up and down images of the row
                    ct.AddElement(headerTable);
                    ct.Go();
                    pdfData.EndText();

                    //footer eraser
                    PdfContentByte pdfData1 = pdfStamper.GetOverContent(pageIndex);
                    pdfData1.BeginText();
                    PdfPTable footerTable = new PdfPTable(1);
                    footerTable.WidthPercentage = 90;
                    footerTable.HorizontalAlignment = Element.ALIGN_LEFT;

                    var ct1 = new ColumnText(pdfData1);
                    ct1.Alignment = Element.ALIGN_CENTER;
                    ct1.SetSimpleColumn(30, 10, 570, 50);//adjust only last value to up and down images of the row ,increase raise up decrease raise down
                    ct1.AddElement(footerTable);
                    string text1 = @"*This consultation is based on the input provided by the patient via online medium. *No physical examination has been performed and all patients are advised to visit a doctor for any complications.*This opinion cannot be challenged for any medico-legal requirements as agreed upon in terms and conditions on registration.*This is a digital copy and it doesn’t require physical signature.*";
                    iTextSharp.text.Font f1 = new iTextSharp.text.Font();
                    f1.Size = 8;
                    ct1.AddElement(new Paragraph(text1, f1));
                    ct1.Go();
                    pdfData1.EndText();

                    //Medicine Used
                    PdfContentByte pdfData2 = pdfStamper.GetOverContent(pageIndex);
                    pdfData2.BeginText();
                    PdfPTable medTable = new PdfPTable(1);
                    medTable.WidthPercentage = 100;
                    medTable.HorizontalAlignment = Element.ALIGN_LEFT;

                    var ct2 = new ColumnText(pdfData2);
                    ct2.Alignment = Element.ALIGN_CENTER;
                    ct2.SetSimpleColumn(45, 10, 250, 250);//adjust only last value to up and down images of the row ,increase raise up decrease raise down
                    ct2.AddElement(medTable);
                    string text = @"OD:One time a day
BD:Two times a day
TDS:Three Times a day
QID:Four times a day
HS:Night one time a day
A/D:One time alternate day
ODA/C :Morning one time a day
empty stomach
SOS:Emergency as 
and when needed";
                    iTextSharp.text.Font f = new iTextSharp.text.Font();
                    f.Size = 9;
                    ct2.AddElement(new Paragraph(text, f));
                    ct2.Go();
                    pdfData2.EndText();
                }
                pdfStamper.Close();
                response = new HttpResponseMessage(HttpStatusCode.OK);
                //response.Content = new StreamContent(new MemoryStream(data1));
                //response.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment");
                //response.Content.Headers.ContentDisposition.FileName = "test.pdf";
                //response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/pdf");
                //response.Content.Headers.ContentLength = data1.Length;
                result = "Success";
                pdfStamper.Close();
            }
            catch (Exception ex)
            {
                DBManager.QueryExecute("execute Insert_Error 'file','" + ex.Message + "' ", GlobalConfig.ConStr_Hospital);
                result = ex.Message;
            }

            return response;
        }
        public HttpResponseMessage SetHeaderandFooterOld(out string result,byte[] data,string filePath)
        {
            HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
            PdfReader pdfReader = new PdfReader(new MemoryStream(data));
            PdfStamper pdfStamper = new PdfStamper(pdfReader, new FileStream(filePath, FileMode.Create));
            try
            {
                for(int pageIndex = 1; pageIndex <= pdfReader.NumberOfPages; pageIndex++)
                {
                    pdfStamper.FormFlattening = false;
                    iTextSharp.text.Rectangle pageRectangle = pdfReader.GetPageSizeWithRotation(pageIndex);
                    //header eraser
                    PdfContentByte pdfData = pdfStamper.GetOverContent(pageIndex);
                    pdfData.BeginText();
                    //Write some content
                    PdfPTable headerTable = new PdfPTable(1);
                    headerTable.WidthPercentage = 100;
                    headerTable.HorizontalAlignment = Element.ALIGN_LEFT;

                    System.Drawing.Image img_up = System.Drawing.Image.FromFile("I:\\Hospital\\PrescriptionByHospital\\header.jpg");
                    iTextSharp.text.Image img_up_eraser = iTextSharp.text.Image.GetInstance(img_up, System.Drawing.Imaging.ImageFormat.Jpeg);

                    PdfPCell cell_up_eraser = new PdfPCell(img_up_eraser);
                    cell_up_eraser.HorizontalAlignment = Element.ALIGN_LEFT;
                    cell_up_eraser.VerticalAlignment = Element.ALIGN_MIDDLE;
                    cell_up_eraser.BorderColor = iTextSharp.text.BaseColor.WHITE;
                    cell_up_eraser.BackgroundColor = iTextSharp.text.BaseColor.WHITE;
                    headerTable.AddCell(cell_up_eraser);

                    var ct = new ColumnText(pdfData);
                    ct.Alignment = Element.ALIGN_CENTER;
                    ct.SetSimpleColumn(5, 10,590,830);//adjust only last value to up and down images of the row
                    ct.AddElement(headerTable);
                    ct.Go();
                    pdfData.EndText();

                    //footer eraser
                    PdfContentByte pdfData1 = pdfStamper.GetOverContent(pageIndex);
                    pdfData1.BeginText();
                    PdfPTable footerTable = new PdfPTable(1);
                    footerTable.WidthPercentage = 90;
                    footerTable.HorizontalAlignment = Element.ALIGN_LEFT;
               
                    var ct1 = new ColumnText(pdfData1);
                    ct1.Alignment = Element.ALIGN_CENTER;
                    ct1.SetSimpleColumn(30, 10, 570,50);//adjust only last value to up and down images of the row ,increase raise up decrease raise down
                    ct1.AddElement(footerTable);
                    string text1 = @"*This consultation is based on the input provided by the patient via online medium. *No physical examination has been performed and all patients are advised to visit a doctor for any complications.*This opinion cannot be challenged for any medico-legal requirements as agreed upon in terms and conditions on registration.*This is a digital copy and it doesn’t require physical signature.*";
                    iTextSharp.text.Font f1 = new iTextSharp.text.Font();
                    f1.Size = 8;
                    ct1.AddElement(new Paragraph(text1, f1));
                    ct1.Go();
                    pdfData1.EndText();

                    //Medicine Used
                    PdfContentByte pdfData2 = pdfStamper.GetOverContent(pageIndex);
                    pdfData2.BeginText();
                    PdfPTable medTable = new PdfPTable(1);
                    medTable.WidthPercentage = 100;
                    medTable.HorizontalAlignment = Element.ALIGN_LEFT;
                
                    var ct2 = new ColumnText(pdfData2);
                    ct2.Alignment = Element.ALIGN_CENTER;
                    ct2.SetSimpleColumn(45, 10, 250, 250);//adjust only last value to up and down images of the row ,increase raise up decrease raise down
                    ct2.AddElement(medTable);
                    string text = @"OD:One time a day
BD:Two times a day
TDS:Three Times a day
QID:Four times a day
HS:Night one time a day
A/D:One time alternate day
ODA/C :Morning one time a day
empty stomach
SOS:Emergency as 
and when needed";
                    iTextSharp.text.Font f = new iTextSharp.text.Font();
                    f.Size = 9;
                    ct2.AddElement(new Paragraph(text, f));
                    ct2.Go();
                    pdfData2.EndText();
                }
                pdfStamper.Close();
                byte[] data1 = System.IO.File.ReadAllBytes(filePath);
                response = new HttpResponseMessage(HttpStatusCode.OK);
                response.Content = new StreamContent(new MemoryStream(data1));
                response.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment");
                response.Content.Headers.ContentDisposition.FileName = "test.pdf";
                response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/pdf");
                response.Content.Headers.ContentLength = data1.Length;
                result = "Success";
            }
            catch (Exception ex)
            {
                pdfStamper.Close();
                result = ex.Message;
            }
            return response;
        }
    }
}