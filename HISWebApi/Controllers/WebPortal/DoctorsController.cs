
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


namespace HISWebApi.Controllers
{
    [RoutePrefix("api/Doctors")]
    public class DoctorsController : ApiController
    {
        private Doctors repositoryDoctors = new Doctors();

        [HttpPost]
        [Route("Web_DoctorQueries")]
        public HttpResponseMessage Web_DoctorQueries([FromBody] ipDoctors objBO)
        {
            dataSet ds = repositoryDoctors.Web_DoctorQueries(objBO);
            return Request.CreateResponse(HttpStatusCode.OK, ds);
        }
        [HttpPost]
        [Route("Web_DepartmentQueries")]
        public HttpResponseMessage Web_DepartmentQueries([FromBody]ipDoctors objBO)
        {
            dataSet ds = repositoryDoctors.Web_DepartmentQueries(objBO);
            return Request.CreateResponse(HttpStatusCode.OK, ds);
        }
        [HttpPost]
        [Route("InsertDepartmentFeatures")]
        public HttpResponseMessage InsertDepartmentFeatures([FromBody]ipDoctors objBO)
        {
            string result = repositoryDoctors.InsertDepartmentFeatures(objBO);
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }
        [HttpPost]
        [Route("InsertDeptFeatures")]
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
                string autoid = string.Empty;
                string outFileName = string.Empty;
                string vertual_path = string.Empty;
                var filesReadToProvider = await Request.Content.ReadAsMultipartAsync();
                var json = await filesReadToProvider.Contents[0].ReadAsStringAsync();
                ipDoctors obj = JsonConvert.DeserializeObject<ipDoctors>(json);
                //Json String of object  ipUploadDocument to be send at first or 0 index parameter                   
                ss.Message = repositoryDoctors.InsertDepartmentFeatures(obj);
                byte[] fileBytes = await filesReadToProvider.Contents[1].ReadAsByteArrayAsync();
                string ImageName = filesReadToProvider.Contents[1].Headers.ContentDisposition.FileName;
                if (ss.Message.Contains("Success"))
                {
                    autoid = ss.Message.Split('|')[1];
                    if (fileBytes.Length > 20)
                    {
                        obj.ImageName = autoid + ".jpg";
                        ss.Message = UploadClass.UploadFeatureDocument(out outFileName, out vertual_path, fileBytes, obj.ImageName);
                        obj.MediaLink = vertual_path;
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

        [HttpPost]
        [Route("InsertTreatDoc")]
        public async Task<HttpResponseMessage> UploadTreatDoc()
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
                string autoId = string.Empty;
                string outFileName = string.Empty;
                string vertual_path = string.Empty;
                var filesReadToProvider = await Request.Content.ReadAsMultipartAsync();
                var json = await filesReadToProvider.Contents[0].ReadAsStringAsync();
                ipDoctors obj = JsonConvert.DeserializeObject<ipDoctors>(json);
                ss.Message = repositoryDoctors.InsertDepartmentFeatures(obj);
                byte[] fileBytes = await filesReadToProvider.Contents[1].ReadAsByteArrayAsync();
                string ImageName = filesReadToProvider.Contents[1].Headers.ContentDisposition.FileName;
                if (ss.Message.Contains("Success"))
                {
                    autoId = ss.Message.Split('|')[1];
                    if (fileBytes.Length > 20)
                    {
                        obj.ImageName = autoId + ".jpg";
                        ss.Message = UploadClass.UploadTreatDoc(out outFileName, out vertual_path, fileBytes, obj.ImageName);
                        obj.MediaLink = vertual_path;
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

        [HttpPost]
        [Route("Web_InsertUpdateViedoLibrary")]
        public HttpResponseMessage Web_InsertPatientFeedback([FromBody]ViedoLibraryInfo objBO)
        {
            string result = repositoryDoctors.InsertUpdateViedoLibrary(objBO);
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }
        [HttpPost]
        [Route("web_ViedoLibraryQueries")]
        public HttpResponseMessage ViedoLibraryQueries([FromBody]ViedoLibraryInfo objBO)
        {
            dataSet ds = repositoryDoctors.ViedoLibraryQueries(objBO);
            return Request.CreateResponse(HttpStatusCode.OK, ds);
        }
    }

}
