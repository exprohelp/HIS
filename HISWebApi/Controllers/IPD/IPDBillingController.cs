using HISWebApi.Models;
using HISWebApi.Repository.IPD;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Threading.Tasks;
using static HISWebApi.Models.IPDBO;
using System.IO;

namespace HISWebApi.Controllers
{
    [RoutePrefix("api/IPDBilling")]
    public class IPDBillingController : ApiController
    {
        private IPDBilling repository = new IPDBilling();

        [HttpPost]
        [Route("IPD_BillingQuerries")]
        public HttpResponseMessage IPD_BillingQuerries([FromBody]IPDInfo objBO)
        {
            dataSet response = repository.IPD_BillingQuerries(objBO);
            return Request.CreateResponse(HttpStatusCode.OK, response);
        }
        [HttpPost]
        [Route("IPD_BillingInsertModifyItems")]
        public HttpResponseMessage IPD_BillingInsertModifyItems([FromBody]serviceBooking obj)
        {
            string response = repository.IPD_BillingInsertModifyItems(obj.objBooking, obj.objRateList);
            return Request.CreateResponse(HttpStatusCode.OK, response);
        }
        [HttpPost]
        [Route("IPD_CalculateAndInsertSurguryAmount")]
        public HttpResponseMessage IPD_CalculateAndInsertSurguryAmount([FromBody]serviceBooking obj)
        {
            dataSet response = repository.IPD_CalculateAndInsertSurguryAmount(obj.objBooking, obj.objRateList);
            return Request.CreateResponse(HttpStatusCode.OK, response);
        }
      
        [HttpPost]
        [Route("IPD_TPApprovalEntry")]
        public async Task<HttpResponseMessage> IPD_TPApprovalEntry()
        {
            string result = string.Empty;
            HttpResponseMessage response = new HttpResponseMessage();
            if(!Request.Content.IsMimeMultipartContent())
            {
                response = Request.CreateResponse(HttpStatusCode.UnsupportedMediaType, "This is not multipart content");
            }
            try
            {
                var filesReadToProvider = await Request.Content.ReadAsMultipartAsync();
                //Json String of object  ipUploadDocument to be send at first or 0 index parameter   
                var json = await filesReadToProvider.Contents[0].ReadAsStringAsync();
                ipTPApproval obj = JsonConvert.DeserializeObject<ipTPApproval>(json);
                //Image to be send at second or 1 index parameter  

                /*
                string ImageName = filesReadToProvider.Contents[1].Headers.ContentDisposition.FileName;
                string fileName = JsonConvert.DeserializeObject<string>(ImageName);
                string fileExtension = Path.GetExtension(fileName);
                */
         

                byte[] fileBytes = await filesReadToProvider.Contents[1].ReadAsByteArrayAsync();
                result = repository.IPD_TPApprovalEntry(obj, fileBytes);
                response = Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception ex)
            {
                result = ex.Message;
                response = Request.CreateResponse(HttpStatusCode.OK, result);
            }
            return response;
        }

        [HttpPost]
        [Route("IPD_TPApprovalRejectEntry")]
        public HttpResponseMessage IPD_TPApprovalRejectEntry([FromBody]ipTPApproval obj)
        {
            string response = repository.IPD_TPApprovalRejectEntry(obj);
            return Request.CreateResponse(HttpStatusCode.OK, response);
        }
    }
}
