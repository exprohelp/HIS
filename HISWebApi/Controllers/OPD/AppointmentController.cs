using HISWebApi.Models;
using HISWebApi.Repository.IPD;
using HISWebApi.Repository.OPD;
using HISWebApi.Repository.Utilities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web.Http;

namespace HISWebApi.Controllers
{
	[RoutePrefix("api/Appointment")]
	public class AppointmentController : ApiController
	{
		private Appointment repository = new Appointment();
		private Vaccination repositoryVaccination = new Vaccination();
		private LISDBLayer lisdblayer = new LISDBLayer();

		[HttpPost]
		[Route("Base64")]
		public HttpResponseMessage Base64([FromBody] AppointmentQueries objBO)
		{
			ExcelGenerator obj = new ExcelGenerator();
			return obj.GetPDFFile();
		}
		[HttpPost]
		[Route("Referral_Search")]
		public HttpResponseMessage Referral_Search([FromBody] ReferralSearch objBO)
		{
			dataSet ds = repository.Referral_Search(objBO);
			return Request.CreateResponse(HttpStatusCode.OK, ds);
		}
        [HttpPost]
        [Route("ITDosePatientInfo")]
        public HttpResponseMessage ITDosePatientInfo([FromBody] LISBO.PatientBO objBO)
        {
            dataSet ds = lisdblayer.ITDosePatientInfo(objBO);
            return Request.CreateResponse(HttpStatusCode.OK, ds);
        }
        [HttpPost]
        [Route("Opd_ExaminationRoomQueries")]
        public HttpResponseMessage Opd_ExaminationRoomQueries([FromBody] AppointmentQueries objBO)
        {
            dataSet ds = repository.Opd_ExaminationRoomQueries(objBO);
            return Request.CreateResponse(HttpStatusCode.OK, ds);
        }
        [HttpPost]
		[Route("Opd_AppointmentSearch")]
		public HttpResponseMessage Opd_AppointmentSearch([FromBody] AppointmentQueries objBO)
		{
			dataSet ds = repository.Opd_AppointmentSearch(objBO);
			return Request.CreateResponse(HttpStatusCode.OK, ds);
		}
		[HttpPost]
		[Route("Opd_AppointmentQueries")]
		public HttpResponseMessage Opd_AppointmentQueries([FromBody] AppointmentQueries objBO)
		{
			dataSet ds = repository.Opd_AppointmentQueries(objBO);
			return Request.CreateResponse(HttpStatusCode.OK, ds);
		}     
        [HttpPost]
		[Route("Patient_MasterQueries")]
		public HttpResponseMessage Patient_MasterQueries([FromBody] PatientMasterQueries objBO)
		{
			dataSet ds = repository.Patient_MasterQueries(objBO);
			return Request.CreateResponse(HttpStatusCode.OK, ds);
		}
		[HttpPost]
		[Route("Opd_InsertAppointmentAssets")]
		public HttpResponseMessage Opd_InsertAppointmentAssets([FromBody] AppointmentBO objBO)
		{
			string result = repository.Opd_InsertAppointmentAssets(objBO);
			return Request.CreateResponse(HttpStatusCode.OK, result);
		}
		[HttpPost]
		[Route("Opd_AppointmentBooking")]
		public HttpResponseMessage Opd_AppointmentBooking([FromBody]OpdBooking obj)
		{
			string result = repository.Opd_AppointmentBooking(obj.objPatient, obj.objBooking, obj.objPayment);
			return Request.CreateResponse(HttpStatusCode.OK, result);
		}
        [HttpPost]
        [Route("Opd_AppointmentBooking1")]
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
                OpdBooking obj = JsonConvert.DeserializeObject<OpdBooking>(json);
                //Json String of object  ipUploadDocument to be send at first or 0 index parameter  
                Regex regex = new Regex(@"^[\w/\:.-]+;base64,");
                if (obj.photo_path == "Y")
                {
                    string base64File = regex.Replace(obj.Base64String, string.Empty);
                    fileBytes = Convert.FromBase64String(base64File);
                }

                ss.Message = repository.Opd_AppointmentBooking(obj.objPatient, obj.objBooking, obj.objPayment);
                if (ss.Message.Contains("Success"))
                {
                    donorid = ss.Message.Split('|')[1];
                    if (obj.photo_path == "Y" && fileBytes != null)
                    {
                        obj.ImageName = donorid + ".jpeg";
                        string UploadMsg = UploadClass.UploadPatientImage(out vertual_path, fileBytes, obj.ImageName);
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
		[Route("Opd_AppointmentCancellation")]
		public HttpResponseMessage Opd_AppointmentCancellation([FromBody]AppointmentCancellationBO objBO)
		{
			string result = repository.Opd_AppointmentCancellation(objBO);
			return Request.CreateResponse(HttpStatusCode.OK, result);
		}
		[HttpPost]
		[Route("Opd_AppointmentUpdate")]
		public HttpResponseMessage Opd_AppointmentUpdate([FromBody]AppointmentReschedule objBO)
		{
			string result = repository.Opd_AppointmentUpdate(objBO);
			return Request.CreateResponse(HttpStatusCode.OK, result);
		}
        [HttpPost]
        [Route("Opd_InOutMarking")]
        public HttpResponseMessage Opd_InOutMarking([FromBody]OpdInOutMarking objBO)
        {
            string result = repository.Opd_InOutMarking(objBO);
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }
        [HttpPost]
        [Route("Opd_DisplayTVQueries")]
        public HttpResponseMessage Opd_DisplayTVQueries([FromBody] ipAppointment objBO)
        {
            dataSet ds = repository.Opd_DisplayTVQueries(objBO);
            return Request.CreateResponse(HttpStatusCode.OK, ds);
        }
        #region Vaccine
        [HttpPost]
        [Route("OPD_VaccineQueries")]
        public HttpResponseMessage OPD_VaccineQueries([FromBody] ipVaccine objBO)
        {
            dataSet ds = repositoryVaccination.OPD_VaccineQueries(objBO);
            return Request.CreateResponse(HttpStatusCode.OK, ds);
        }
        [HttpPost]
        [Route("OPD_InsertVaccinationInfo")]
        public HttpResponseMessage OPD_InsertVaccinationInfo([FromBody]List<VaccinationInfo> objBO)
        {
            string result = repositoryVaccination.OPD_InsertVaccinationInfo(objBO);
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }
        [HttpPost]
        [Route("OPD_VaccinationInsertUpdate")]
        public HttpResponseMessage OPD_VaccinationInsertUpdate([FromBody]VaccinationInfo objBO)
        {
            string result = repositoryVaccination.OPD_VaccinationInsertUpdate(objBO);
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }
        #endregion Vaccine
    }
}
