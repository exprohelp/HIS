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
    [RoutePrefix("api/Appoinment")]
    public class AppoinmentController : ApiController
    {
        private Appoinment repositoryAppoinment = new Appoinment();
        private WebPortalDBLayer webPortalDBLayer = new WebPortalDBLayer();
        private SmsDataLayer repositorySmsDataLayer = new SmsDataLayer();
        private ItDoseProxy.MobileAppSoapClient itdose = new ItDoseProxy.MobileAppSoapClient();

        [HttpPost]
        [Route("GetDiagnosticTestAnalysis")]
        public HttpResponseMessage GetDiagnosticTestAnalysis([FromBody]ipWebPortal objBO)
        {
            dataSet ds = repositoryAppoinment.GetDiagnosticTestAnalysis(objBO);
            return Request.CreateResponse(HttpStatusCode.OK, ds);
        }
        [HttpPost]
        [Route("GetDoctorTimingSlot")]
        public HttpResponseMessage GetDoctorTimingSlot(ipWebPortal objBO)
        {
           string result= webPortalDBLayer.GetDoctorTimingSlot(objBO);
           return Request.CreateResponse(HttpStatusCode.OK, result);
        }
        [HttpPost]
        [Route("SaveGenerateAppointment")]
        public HttpResponseMessage SaveGenerateAppointment([FromBody]ipWebPortal objBO)
        {
            dataSet ds = repositoryAppoinment.SaveGenerateAppointment(objBO);
            return Request.CreateResponse(HttpStatusCode.OK, ds);
        }
        //[HttpPost]
        //[Route("DirectAppointment")]
        //public HttpResponseMessage DirectAppointment([FromBody]ipWebPortal objBO)
        //{
        //    dataSet ds = repositoryAppoinment.DirectAppointment(objBO);
        //    return Request.CreateResponse(HttpStatusCode.OK, ds);
        //}
        [HttpPost]
        [Route("DirectAppointment")]
        public HttpResponseMessage bookOPD([FromBody] ipWebPortal objBO)
        {
            string result = itdose.GenerateDirectAppointment(objBO.Title, objBO.PFirstName, objBO.PLastName, objBO.Age, objBO.Gender, objBO.Mobile, objBO.Address, objBO.DoctorId, objBO.AppDate, objBO.StartTime, objBO.OpdRemark);
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }
        [HttpPost]
        [Route("SendSmsToPatient")]
        public HttpResponseMessage SendSmsToPatient([FromBody]ipSMS objBO)
        {
            string ds = repositorySmsDataLayer.SendSmsToPatient(objBO);


            return Request.CreateResponse(HttpStatusCode.OK, ds);
        }
        [HttpPost]
        [Route("SendSmsToEmployee")]
        public HttpResponseMessage SendSmsToEmployee([FromBody]ipSMS objBO)
        {
            string ds = repositorySmsDataLayer.SendSmsToEmployee(objBO);
            return Request.CreateResponse(HttpStatusCode.OK, ds);
        }
        [HttpPost]
        [Route("InsertSmsLog")]
        public HttpResponseMessage InsertSmsLog([FromBody] PatientSmsInfo objBO)
        {
            string result = repositoryAppoinment.InsertSmsLog(objBO);
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }
        #region AppBooking
        [HttpPost]
        [Route("OnlineInsertDoctorAppointment")]
        public HttpResponseMessage OnlineInsertDoctorAppointment([FromBody] DoctorAppointmentInfo objBO)
        {
            string result = repositoryAppoinment.OnlineInsertDoctorAppointment(objBO);
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }
        [HttpPost]
        [Route("OnlineBookingNotification")]
        public HttpResponseMessage OnlineBookingNotification([FromBody] DoctorAppointmentInfo objBO)
        {
            dataSet ds = repositoryAppoinment.OnlineBookingNotification(objBO);
            return Request.CreateResponse(HttpStatusCode.OK, ds);
        }
        #endregion
        //Carrier Section
        [HttpPost]
        [Route("SendMailToHRFromCarrier")]
        public async Task<HttpResponseMessage> SendMailToHRFromCarrier()
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
                CarrierInfo obj = JsonConvert.DeserializeObject<CarrierInfo>(json);
                //Image to be send at second or 1 index parameter  
                byte[] fileBytes = await filesReadToProvider.Contents[1].ReadAsByteArrayAsync();
                string ImageName = filesReadToProvider.Contents[1].Headers.ContentDisposition.FileName;
                obj.fileBytes = fileBytes;
                obj.ImageName = ImageName;
                MailClass.SendMailToHRFromCarrier(obj);
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
    }
}
