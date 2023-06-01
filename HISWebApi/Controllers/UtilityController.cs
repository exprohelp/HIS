using HISWebApi.Models;
using HISWebApi.Repository;
using HISWebApi.Repository.IPD;
using HISWebApi.Repository.Utilities;
using iTextSharp.text;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.pdf;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace HISWebApi.Controllers
{
    public class UtilityController : ApiController
    {
        UploadClass repository = new UploadClass();
        Member repositoryMember = new Member();
        HISDBLayer hisdblayer = new HISDBLayer();
        [HttpPost]
        [Route("api/Utility/SetHeaderandFooterold")]
        public  HttpResponseMessage SetHeaderandFooterold()
        {
            HttpResponseMessage response = new HttpResponseMessage();
            string _message = string.Empty;
            string appid = string.Empty;

            if(!Request.Content.IsMimeMultipartContent())
            {
                response = Request.CreateResponse(HttpStatusCode.UnsupportedMediaType, "This is not multipart content");
            }
            try
            {
                var filesReadToProvider = Request.Content.ReadAsMultipartAsync().Result;
                //Json String of object  ipUploadPrescription to be send at first or 0 index parameter   
                var AppintmentId = filesReadToProvider.Contents[0].ReadAsStringAsync().Result;
                //ipUploadPDF obj = JsonConvert.DeserializeObject<ipUploadPDF>(json);
                //Image to be send at second or 1 index parameter  
                byte[] fileBytes=filesReadToProvider.Contents[1].ReadAsByteArrayAsync().Result;
                string _result = string.Empty;
                HISWebApi.Repository.Utilities.PdfGenerator obj = new Repository.Utilities.PdfGenerator();
                string filePath = "I:\\Hospital\\PrescriptionByHospital\\" + AppintmentId + ".pdf";

                //DBManager.QueryExecute("execute Insert_Error 'file','Passed' ", GlobalConfig.ConStr_Hospital);

                response = obj.SetHeaderandFooter(out _result,fileBytes, filePath);
                if(_result.Contains("Success"))
                {
                    appid = AppintmentId;
                    string vertual_path = "http://exprohelp.com/HospDoc/PrescriptionByHospital/" + AppintmentId + ".pdf";
                    HISWebApi.Repository.Hospital.OnlineAppointment op = new Repository.Hospital.OnlineAppointment();
                    _message = op.SaveOnlinePrescription(AppintmentId, vertual_path, filePath);
                    if (_message.Contains("Success"))
                    {
                        _message = "Success";
                        response = Request.CreateResponse(HttpStatusCode.OK, _message);
                    }
                    else
                        response = Request.CreateResponse(HttpStatusCode.OK, _message);
                }
                else
                {
                    response= Request.CreateResponse(HttpStatusCode.OK, _result);
                }
            }
            catch (Exception ex)
            {
                response = Request.CreateResponse(HttpStatusCode.OK,ex.Message);
                DBManager.QueryExecute("execute Insert_Error '" + appid + "','" + ex.Message + "' ", GlobalConfig.ConStr_Hospital);
            }
            return response;
        }
        [HttpPost]
        [Route("api/Utility/SetHeaderandFooter")]
        public async Task<HttpResponseMessage> SetHeaderandFooter()
        {
            HttpResponseMessage response = new HttpResponseMessage();
            string _message = string.Empty;
            string appid = string.Empty;
            // Check if the request contains multipart/form-data.
            if (!Request.Content.IsMimeMultipartContent())
            {
                response = Request.CreateResponse(HttpStatusCode.UnsupportedMediaType, "File Type problem");
            }
            string root = HttpContext.Current.Server.MapPath("~/App_Data");
            var provider = new MultipartMemoryStreamProvider();
            try
            {
                // Read the form data.
                await Request.Content.ReadAsMultipartAsync(provider);
                var AppintmentId = await provider.Contents[0].ReadAsStringAsync();
                byte[] fileBytes =await provider.Contents[1].ReadAsByteArrayAsync();
                string _result = string.Empty;
                HISWebApi.Repository.Utilities.PdfGenerator obj = new Repository.Utilities.PdfGenerator();
                string filePath = "I:\\Hospital\\PrescriptionByHospital\\" + AppintmentId + ".pdf";
                response = obj.SetHeaderandFooter(out _result, fileBytes, filePath);
                if (_result.Contains("Success"))
                {
                    string vertual_path = "http://exprohelp.com/HospDoc/PrescriptionByHospital/" + AppintmentId + ".pdf";
                    HISWebApi.Repository.Hospital.OnlineAppointment op = new Repository.Hospital.OnlineAppointment();
                    _message = op.SaveOnlinePrescription(AppintmentId, vertual_path, filePath);
                    if (_message.Contains("Success"))
                    {
                        _message = "Success";
                        response = Request.CreateResponse(HttpStatusCode.OK, _message);
                    }
                    else
                        response = Request.CreateResponse(HttpStatusCode.OK, _message);
                }
                else
                {
                    response = Request.CreateResponse(HttpStatusCode.OK, _result);
                }
            }
            catch (System.Exception ex)
            {
                response = Request.CreateResponse(HttpStatusCode.OK, ex.Message);
                DBManager.QueryExecute("execute Insert_Error '" + appid + "','" + ex.Message + "' ", GlobalConfig.ConStr_Hospital);
            }
            return response;
        }

        [HttpPost]
        [Route("api/Utility/UploadDocument")]
        public async Task<HttpResponseMessage> UploadPurchaseDocument()
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
                var filesReadToProvider = await Request.Content.ReadAsMultipartAsync();
                //Json String of object  ipUploadDocument to be send at first or 0 index parameter   
                var json = await filesReadToProvider.Contents[0].ReadAsStringAsync();
                ipDocumentInfo obj = JsonConvert.DeserializeObject<ipDocumentInfo>(json);
                //Image to be send at second or 1 index parameter  
                byte[] fileBytes = await filesReadToProvider.Contents[1].ReadAsByteArrayAsync();
                ss = repository.UploadDocument(obj.TrnType, obj.TranId, obj.ImageType, fileBytes, obj.login_id);
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

        //Member
        [HttpPost]
        [Route("api/Utility/Online_SentOtp")]
        public HttpResponseMessage Online_SentOtp([FromBody] HospitalBO objBO)
        {
            string result = repositoryMember.Online_SentOtp(objBO);
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }
        [HttpPost]
        [Route("api/Utility/GetCardUserProfileQueries")]
        public HttpResponseMessage GetCardUserProfileQueries([FromBody] HealthCardInfo ipapp)
        {
            dataSet ds = repositoryMember.GetCardUserProfileQueries(ipapp);
            return Request.CreateResponse(HttpStatusCode.OK, ds);
        }
        [HttpPost]
        [Route("api/Utility/CouponQueries")]
        public HttpResponseMessage CouponQueries([FromBody] CouponQueries ipapp)
        {
            dataSet ds = repositoryMember.CouponQueries(ipapp);
            return Request.CreateResponse(HttpStatusCode.OK, ds);
        }
        [HttpPost]
        [Route("api/Utility/Insert_CardUserProfile")]
        public HttpResponseMessage Insert_CardUserProfile([FromBody] HealthCardInfo ipapp)
        {
            string result = repositoryMember.Insert_CardUserProfile(ipapp);
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }
        [HttpPost]
        [Route("api/Utility/InsertMobileAppCoupons")]
        public HttpResponseMessage InsertMobileAppCoupons([FromBody] CouponLogBO objBO)
        {
            string result = repositoryMember.InsertMobileAppCoupons(objBO);
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }
        [HttpPost]
        [Route("api/Utility/SendCoupon")]
        public HttpResponseMessage SendCoupon([FromBody] CouponQueries objBO)
        {
            string result = repositoryMember.SendCoupon(objBO);
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }
        //[HttpPost]
        //[Route("api/Utility/Sync_IPDDatafromHISToChandan")]
        //public HttpResponseMessage Sync_IPDDatafromHISToChandan([FromBody] ipCommission objBO)
        //{
        //    string result = hisdblayer.Sync_IPDDatafromHISToChandan(objBO.IPDNo,objBO.RefCode,objBO.login_id);
        //    return Request.CreateResponse(HttpStatusCode.OK, result);
        //}

    }
}
