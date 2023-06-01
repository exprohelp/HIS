using HISWebApi.Models;
using HISWebApi.Repository.OPD;
using HISWebApi.Repository.Utilities;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace HISWebApi.Controllers
{
	[RoutePrefix("api/Service")]
	public class ServiceController : ApiController
    {
		private Service repository = new Service();
		//Service Booking
		[HttpPost]
		[Route("Opd_ServiceQueries")]
		public HttpResponseMessage Opd_ServiceQueries([FromBody] ServiceQueries objBO)
		{
			dataSet ds = repository.Opd_ServiceQueries(objBO);
			return Request.CreateResponse(HttpStatusCode.OK, ds);
		}
		[HttpPost]
		[Route("Opd_ServiceRates")]
		public HttpResponseMessage Opd_ServiceRates([FromBody] ServiceQueries objBO)
		{
			dataSet ds = repository.Opd_ServiceRates(objBO);
			return Request.CreateResponse(HttpStatusCode.OK, ds);
		}
		[HttpPost]
		[Route("Opd_ServiceBooking")]
		public HttpResponseMessage Opd_ServiceBooking([FromBody]serviceBooking objBO)
		{
			string result = repository.Opd_ServiceBooking(objBO.objPatient, objBO.objBooking, objBO.objRateList, objBO.objPayment);
			return Request.CreateResponse(HttpStatusCode.OK, result);
		}
		[HttpPost]
		[Route("Opd_ServiceRefund")]
		public HttpResponseMessage Opd_ServiceRefund([FromBody]ServiceRefund objBO)
		{
			string result = repository.Opd_ServiceRefund(objBO);
			return Request.CreateResponse(HttpStatusCode.OK, result);
		}
	}
}
