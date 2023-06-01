using HISWebApi.Models;
using HISWebApi.Repository.BloodBank;
using HISWebApi.Repository.Utilities;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web.Http;
using static HISWebApi.Models.BloodBank;

namespace HISWebApi.Controllers
{
    [RoutePrefix("api/BloodBank")]
    public class BloodBankController : ApiController
    {
        private DonorInfo repository = new DonorInfo();

        [HttpPost]
        [Route("BB_SelectQueries")]
        public HttpResponseMessage BB_SelectQueries([FromBody] SelectQueriesInfo objBO)
        {
            if (objBO.OutPutType == "Excel")
            {
                dataSet ds = repository.BB_SelectQueries(objBO);
                HISWebApi.Repository.Utilities.ExcelGenerator obj = new Repository.Utilities.ExcelGenerator();
                return obj.GetExcelFile(ds.ResultSet);
            }
            else
            {
                dataSet ds = repository.BB_SelectQueries(objBO);
                return Request.CreateResponse(HttpStatusCode.OK, ds);
            }
        }
        [HttpPost]
        [Route("BB_SDPQueries")]
        public HttpResponseMessage BB_SDPQueries([FromBody] SelectQueriesInfo objBO)
        {
            if (objBO.OutPutType == "Excel")
            {
                dataSet ds = repository.BB_SDPQueries(objBO);
                ExcelGenerator obj = new ExcelGenerator();
                return obj.GetExcelFile(ds.ResultSet);
            }
            else
            {
                dataSet ds = repository.BB_SelectQueries(objBO);
                return Request.CreateResponse(HttpStatusCode.OK, ds);
            }
        }
        [HttpPost]
        [Route("BB_StockAndDashBoard_Queries")]
        public HttpResponseMessage BB_StockAndDashBoard_Queries([FromBody] SelectQueriesInfo objBO)
        {
            dataSet ds = repository.BB_StockAndDashBoard_Queries(objBO);
            return Request.CreateResponse(HttpStatusCode.OK, ds);
        }

        [HttpPost]
        [Route("BB_InsertModifyDonorInfo")]
        public HttpResponseMessage BB_InsertModifyDonorInfo([FromBody]DonorDetail obj)
        {
            string result = repository.BB_InsertModifyDonorInfo(obj);
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }
        [HttpPost]
        [Route("BB_InsertModifyCollectionInfo")]
        public HttpResponseMessage BB_InsertModifyCollectionInfo([FromBody]BloodCollection obj)
        {
            string result = repository.BB_InsertModifyCollectionInfo(obj);
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }

        [HttpPost]
        [Route("BB_InsertModifyGroupingInfo")]
        public HttpResponseMessage BB_InsertModifyGroupingInfo([FromBody]BloodGrouping obj)
        {
            string result = repository.BB_InsertModifyGroupingInfo(obj);
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }
        [HttpPost]
        [Route("BB_InsertModifyBloodScreening")]
        public HttpResponseMessage BB_InsertModifyBloodScreening([FromBody]BloodScreeningDetail objBO)
        {
            string result = repository.BB_InsertModifyBloodScreening(objBO);
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }
        [HttpPost]
        [Route("BB_InsertModifyComponentCreation")]
        public HttpResponseMessage BB_InsertModifyComponentCreation([FromBody]ComponentDetail objBO)
        {
            string result = repository.BB_InsertModifyComponentCreation(objBO);
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }
        [HttpPost]
        [Route("BB_Insert_ModifyBloodIssue")]
        public HttpResponseMessage BB_Insert_ModifyBloodIssue([FromBody]BloodIssueInfo objBO)
        {
            string result = repository.BB_Insert_ModifyBloodIssue(objBO);
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }
        [HttpPost]
        [Route("BB_ApproveDonation")]
        public HttpResponseMessage BB_ApproveDonation([FromBody]ApproveDonationInfo objBO)
        {
            string result = repository.BB_ApproveDonation(objBO);
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }
        [HttpPost]
        [Route("SaveDonorInfo")]
        public async Task<HttpResponseMessage> UploadQueryDocument()
        {
            HttpResponseMessage response = new HttpResponseMessage();
            SubmitStatus ss = new SubmitStatus();
            if (!Request.Content.IsMimeMultipartContent())
            {
                ss.Status = 0;
                ss.Message = "This is not multipart content";
                response = Request.CreateResponse(HttpStatusCode.UnsupportedMediaType, "This is not multipart content");
            }
            try
            {
                string donorid = string.Empty;
                string outFileName = string.Empty;
                string vertual_path = string.Empty;
                byte[] fileBytes = null;
                var filesReadToProvider = await Request.Content.ReadAsMultipartAsync();
                var json = await filesReadToProvider.Contents[0].ReadAsStringAsync();
                DonorDetail obj = JsonConvert.DeserializeObject<DonorDetail>(json);
                //Json String of object  ipUploadDocument to be send at first or 0 index parameter  
                Regex regex = new Regex(@"^[\w/\:.-]+;base64,");
                if (obj.photo_path == "Y")
                {
                    string base64File = regex.Replace(obj.Base64String, string.Empty);
                    fileBytes = Convert.FromBase64String(base64File);
                }

                ss.Message = repository.BB_InsertModifyDonorInfo(obj);
                if (ss.Message.Contains("Success"))
                {
                    donorid = ss.Message.Split('|')[1];
                    if (obj.photo_path == "Y" && fileBytes != null)
                    {
                        obj.ImageName = donorid + ".jpeg";
                        ss.Message = UploadClass.UploadBloodBankDonorPhoto(out vertual_path, fileBytes, obj.ImageName);
                    }
                }
                response = Request.CreateResponse(HttpStatusCode.OK, ss);
            }
            catch (Exception ex)
            {
                ss.Status = 0;
                ss.Message = ex.Message;
                response = Request.CreateResponse(HttpStatusCode.OK, ss);
            }
            return response;
        }

    }
}
