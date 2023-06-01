using HISWebApi.Models;
using HISWebApi.Repository.Utilities;
using HISWebApi.Repository.WebPortal;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace HISWebApi.Controllers.WebPortal
{
    [RoutePrefix("api/PatientFeedback")]
    public class PatientFeedbackController : ApiController
    {
        private PatientFeedback repositoryPatient = new PatientFeedback();
        private SmsDataLayer repositorySmsDataLayer = new SmsDataLayer();

        [HttpPost]
        [Route("web_ContactUsQueries")]
        public HttpResponseMessage web_ContactUsQueries([FromBody] ipPatientFeedback objBO)
        {
            dataSet ds = repositoryPatient.web_ContactUsQueries(objBO);
            return Request.CreateResponse(HttpStatusCode.OK, ds);
        }
        [HttpPost]
        [Route("Web_InsertPatientFeedback")]
        public HttpResponseMessage Web_InsertPatientFeedback([FromBody]PatientFeedbackInfo objBO)
        {
            string result = repositoryPatient.Web_InsertPatientFeedback(objBO);
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }

        [HttpPost, Route("GetPatientQueries")]
        public HttpResponseMessage PatientQueries([FromBody] PatientFeedbackInfo objBO)
        {
            dataSet ds = repositoryPatient.PatientQueries(objBO);
            return Request.CreateResponse(HttpStatusCode.OK, ds);
        }
        [HttpPost]
        [Route("UploadQueryDocument")]
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
                string outFileName = string.Empty;
                string vertual_path = string.Empty;
                var filesReadToProvider = await Request.Content.ReadAsMultipartAsync();
                //Json String of object  ipUploadDocument to be send at first or 0 index parameter   
                var json = await filesReadToProvider.Contents[0].ReadAsStringAsync();
                PatientFeedbackInfo obj = JsonConvert.DeserializeObject<PatientFeedbackInfo>(json);
                //Image to be send at second or 1 index parameter  

                byte[] fileBytes = await filesReadToProvider.Contents[1].ReadAsByteArrayAsync();
                if (fileBytes.Length > 0)
                {
                    ss.Message = UploadClass.UploadQueryDocument(out outFileName, out vertual_path, fileBytes, obj.ImageName);
                    ss.virtual_path = vertual_path;
                }
                obj.virtual_path = vertual_path;
                obj.physical_path = outFileName;
                ss.Message = repositoryPatient.Web_InsertPatientFeedback(obj);
                MailClass.SendMailToHRFromContactQuery(obj);
                response = Request.CreateResponse(HttpStatusCode.OK, ss);
            }
            catch (Exception ex)
            {
                ss.Status = 0;
                ss.Message = ex.Message;
                response = Request.CreateResponse(HttpStatusCode.OK, ss);
            }
            return response;
            //string doc_location = string.Empty;
            //string result = UploadClass.UploadPrescription(out doc_location, obj.imageByte, obj.ImageName);
            //return Request.CreateResponse(HttpStatusCode.OK, result);
        }

        //Patient Login
        [HttpPost]
        [Route("GetOtp")]
        public HttpResponseMessage GetOtp()
        {
            string otpvalue = OTPGenerator.GenerateRandomOTP(4);
            if (!string.IsNullOrEmpty(otpvalue))
            {
                return Request.CreateResponse(HttpStatusCode.OK, otpvalue);
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.OK, "Otp not found");
            }
        }
        [HttpPost]
        [Route("PatientLoginOTP")]
        public HttpResponseMessage PatientLoginOTP([FromBody] ipSMS objBO)
        {
            string smsresponse = repositorySmsDataLayer.PatientLoginOTP(objBO);
            return Request.CreateResponse(HttpStatusCode.OK, smsresponse);
        }
    }
}
