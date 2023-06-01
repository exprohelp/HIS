using HISWebApi.Models;
using HISWebApi.Repository.PatientDesk;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace HISWebApi.Controllers.PatientDesk
{
	[RoutePrefix("api/Patient")]
	public class PatientController : ApiController
	{
		private SmsDataLayer repositorySmsDataLayer = new SmsDataLayer();
		private Patient repositoryPatient = new Patient();

		[HttpPost]
		[Route("AmbulanceAndEmergencyQueries")]
		public HttpResponseMessage AmbulanceAndEmergencyQueries([FromBody] ipPatientDesk objBO)
		{
			dataSet ds = repositoryPatient.AmbulanceAndEmergencyQueries(objBO);
			return Request.CreateResponse(HttpStatusCode.OK, ds);
		}
		[HttpPost]
		[Route("AmbulanceAndEmergencyRequest")]
		public HttpResponseMessage AmbulanceAndEmergencyRequest([FromBody] EmergencyDesk objBO)
		{
			string result = repositoryPatient.AmbulanceAndEmergencyRequest(objBO);
			return Request.CreateResponse(HttpStatusCode.OK, result);
		}
		[HttpPost]
		[Route("SendTrackingLink")]
		public HttpResponseMessage SendTrackingLink([FromBody] ipSMS objBO)
		{
			string result = repositorySmsDataLayer.SendTrackingLink(objBO);
			return Request.CreateResponse(HttpStatusCode.OK, result);
		}
        [HttpPost]
        [Route("InformToAmbulanceDriver")]
        public HttpResponseMessage InformToAmbulanceDriver([FromBody] EmergencyDesk objBO)
        {
            string result = repositorySmsDataLayer.InformToAmbulanceDriver(objBO);
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }
    }
}
