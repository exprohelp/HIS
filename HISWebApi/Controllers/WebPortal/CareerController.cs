using HISWebApi.Models;
using HISWebApi.Repository.Utilities;
using HISWebApi.Repository.WebPortal;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace HISWebApi.Controllers.WebPortal
{
   [RoutePrefix("api/Career")]
    public class CareerController : ApiController
    {
        private Career repositoryCareer = new Career();     
        [HttpPost]
        [Route("CareerInfo")]
        public HttpResponseMessage CareerInfo([FromBody]CarrierInfo objBO)
        {
            dataSet ds = repositoryCareer.CareerInfo(objBO);
            return Request.CreateResponse(HttpStatusCode.OK, ds);
        }
        [HttpPost]
        [Route("WebAwardQueries")]
        public HttpResponseMessage GetWebAwardInfo([FromBody]AwardInfo objBO)
        {
            dataSet ds = repositoryCareer.GetWebAwardInfo(objBO);
            return Request.CreateResponse(HttpStatusCode.OK, ds);
        }
        [HttpPost]
        [Route("UploadWebAwardDocument")]
        public async Task<HttpResponseMessage> UploadWebAwardDocument()
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
                string result = string.Empty;
                string outFileName = string.Empty;
                // string vertual_path = string.Empty;
                var filesReadToProvider = await Request.Content.ReadAsMultipartAsync();
                //Json String of object  ipUploadDocument to be send at first or 0 index parameter   
                var json = await filesReadToProvider.Contents[0].ReadAsStringAsync();
                AwardInfo obj = JsonConvert.DeserializeObject<AwardInfo>(json);
                //Image to be send at second or 1 index parameter  
                result = repositoryCareer.InsertWebAward(obj);
                byte[] fileBytes = await filesReadToProvider.Contents[1].ReadAsByteArrayAsync();
                if (fileBytes.Length > 0 && result.Contains("Success"))
                {
                    obj.ImageName = result.Split('|')[1] + ".jpg";
                    ss.Message = UploadClass.UploadWebAwardDocument(out outFileName, fileBytes, obj.ImageName);
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
