using ExcelDataReader;
using HISWebApi.Models;
using HISWebApi.Repository.EDP;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace HISWebApi.Controllers
{
    [RoutePrefix("api/EDP")]
    public class EDPController : ApiController
    {
        PanelRateItem repoPanelRateItem = new PanelRateItem();
        PanelRateItemLink repoPanelRateItemLink = new PanelRateItemLink();

        [HttpPost, Route("PanelRateQueries")]
        public HttpResponseMessage PanelRateQueries([FromBody] PanelRateItemBO objBO)
        {
            if (objBO.FileType== "Excel")
            {
                dataSet ds = repoPanelRateItem.PanelRateQueries(objBO);
                Repository.Utilities.ExcelGenerator obj = new Repository.Utilities.ExcelGenerator();
                return obj.GetExcelFile(ds.ResultSet);
            }
            else
            {
                dataSet ds = repoPanelRateItem.PanelRateQueries(objBO);
                return Request.CreateResponse(HttpStatusCode.OK, ds);
            }           
        }

        [HttpPost, Route("PanelItemRateInsertUpdate")]
        public HttpResponseMessage PanelItemRateInsertUpdate([FromBody] List<PanelRateItemBO> objBO)
        {
            string result = repoPanelRateItem.PanelItemRateInsertUpdate(objBO);
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }      
        [HttpPost, Route("PanelRateListLinkInsertUpdate")]
        public HttpResponseMessage PanelRateListLinkInsertUpdate([FromBody] PanelRateItemBO objBO)
        {
            string result = repoPanelRateItemLink.PanelRateListLinkInsertUpdate(objBO);
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }      
        [Route("ReadFile")]
        [HttpPost]
        public string ReadFile()
        {
            try
            {
                #region Variable Declaration
                string message = "";
                HttpResponseMessage ResponseMessage = null;
                var httpRequest = HttpContext.Current.Request;
                DataSet dsexcelRecords = new DataSet();
                IExcelDataReader reader = null;
                HttpPostedFile Inputfile = null;

                var LoginId = HttpContext.Current.Request.Params["LoginId"];
                var HospId = HttpContext.Current.Request.Params["HospId"];
                var Logic = HttpContext.Current.Request.Params["Logic"];

                Stream FileStream = null;
                #endregion

                if (httpRequest.Files.Count > 0)
                {
                    Inputfile = httpRequest.Files[0];
                    FileStream = Inputfile.InputStream;

                    if (Inputfile != null && FileStream != null)
                    {
                        if (Inputfile.FileName.EndsWith(".xls"))
                            reader = ExcelReaderFactory.CreateBinaryReader(FileStream);
                        else if (Inputfile.FileName.EndsWith(".xlsx"))
                            reader = ExcelReaderFactory.CreateOpenXmlReader(FileStream);
                        else
                            message = "The file format is not supported.";

                        dsexcelRecords = reader.AsDataSet();
                        reader.Close();

                        if (dsexcelRecords != null && dsexcelRecords.Tables.Count > 0)
                        {
                            DataTable dtStudentRecords = dsexcelRecords.Tables[0];
                             List<PanelRateItemBO> objBO=new List<PanelRateItemBO>();
                            for (int i = 1; i < dtStudentRecords.Rows.Count; i++)
                            {

                                PanelRateItemBO obj = new PanelRateItemBO();
                                obj.RateListId = dtStudentRecords.Rows[i][2].ToString();
                                obj.RoomBillCategory = dtStudentRecords.Rows[i][3].ToString();
                                obj.ItemId = dtStudentRecords.Rows[i][4].ToString();
                                obj.rate = Convert.ToDecimal(dtStudentRecords.Rows[i][6]);                                
                                obj.login_id = LoginId;
                                obj.hosp_id = HospId;
                                obj.Logic = Logic;
                                objBO.Add(obj);
                            }
                            message = repoPanelRateItem.PanelItemRateInsertUpdate(objBO);
                        }
                        else
                            message = "Selected file is empty.";
                    }
                    else
                        message = "Invalid File.";
                }

                return message;                
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
