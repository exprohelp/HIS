using ClosedXML.Excel;
using HISWebApi.Models;
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
    public class ExcelGenerator
    {
        public HttpResponseMessage GetExcelFile(DataSet ds)
        {
            HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
            try
            {
                var ms = new MemoryStream();
                using (ClosedXML.Excel.XLWorkbook wb = new ClosedXML.Excel.XLWorkbook())
                {
                    if (ds.Tables.Count > 0)
                    {
                        int ct = 1;
                        foreach (DataTable dt in ds.Tables)
                        {
                            wb.Worksheets.Add(ds.Tables[ct-1], "Sheet" + ct.ToString());
                            ct++;
                        }
                    }
                    else { wb.Worksheets.Add(ds.Tables[0], "Sheet1"); }

                    wb.SaveAs(ms);
                    ms.Seek(0, SeekOrigin.Begin);
                    response = new HttpResponseMessage(HttpStatusCode.OK);
                    response.Content = new StreamContent(ms);
                    response.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment");
                    response.Content.Headers.ContentDisposition.FileName = "test.xlsx";
                    response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
                    response.Content.Headers.ContentLength = ms.Length;
                    ms.Seek(0, SeekOrigin.Begin);
                }
            }
            catch (Exception ex)
            {
            }
            return response;
        }
        public DataTable GetDataTableByExcel(out string _result, ipPanelRates pr)
        {
            DataTable dt = new DataTable();
            _result = "";
            using (XLWorkbook workbook = new XLWorkbook("d:\\JanPool.xlsx"))
            {
                IXLWorksheet worksheet = workbook.Worksheet(1);
                bool FirstRow = true;
                //Range for reading the cells based on the last cell used.  
                string readRange = "1:1";
                foreach (IXLRow row in worksheet.RowsUsed())
                {
                    //If Reading the First Row (used) then add them as column name  
                    if (FirstRow)
                    {
                        //Checking the Last cellused for column generation in datatable  
                        readRange = string.Format("{0}:{1}", 1, row.LastCellUsed().Address.ColumnNumber);
                        foreach (IXLCell cell in row.Cells(readRange))
                        {
                            dt.Columns.Add(cell.Value.ToString());
                        }
                        FirstRow = false;
                    }
                    else
                    {
                        //Adding a Row in datatable  
                        dt.Rows.Add();
                        int cellIndex = 0;
                        //Updating the values of datatable  
                        foreach (IXLCell cell in row.Cells(readRange))
                        {
                            dt.Rows[dt.Rows.Count - 1][cellIndex] = cell.Value.ToString();
                            cellIndex++;
                        }
                    }
                    _result = "";
                }
                //If no data in Excel file  
                if (FirstRow)
                {
                    _result = "Empty Excel File!";
                }
            }
            return dt;
        }
        public HttpResponseMessage GetPDFFile()
        {
            HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
            try
            {
                byte[] data = System.IO.File.ReadAllBytes("d:\\m1.pdf");
                var ms = new MemoryStream(data);
                ms.Seek(0, SeekOrigin.Begin);
                response = new HttpResponseMessage(HttpStatusCode.OK);
                response.Content = new StreamContent(ms);
                response.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment");
                response.Content.Headers.ContentDisposition.FileName = "test.pdf";
                response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/pdf");
                response.Content.Headers.ContentLength = ms.Length;
                ms.Seek(0, SeekOrigin.Begin);

            }
            catch (Exception ex)
            {
            }
            return response;
        }
    }
}