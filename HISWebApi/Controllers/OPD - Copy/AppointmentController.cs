using HISWebApi.Models;
using HISWebApi.Repository.OPD;
using HISWebApi.Repository.Utilities;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace HISWebApi.Controllers
{
	[RoutePrefix("api/Appointment")]
	public class AppointmentController : ApiController
	{
		private Appointment repository = new Appointment();

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
	}
}
