using HISWebApi.Models;
using HISWebApi.Repository.OPD;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace HISWebApi.Controllers
{
	[RoutePrefix("api/Prescription")]
	public class PrescriptionController : ApiController
	{
		private Prescription repositoryPrescription = new Prescription();  

		[HttpPost]
		[Route("CPOE_PrescriptionAdviceQueries")]
		public HttpResponseMessage CPOE_PrescriptionAdviceQueries([FromBody] PrescriptionBO objBO)
		{
			dataSet ds = repositoryPrescription.CPOE_PrescriptionAdviceQueries(objBO);
			return Request.CreateResponse(HttpStatusCode.OK, ds);
		}
		[HttpPost]
		[Route("CPOE_InsertUpdateAdviceProcess")]
		public HttpResponseMessage CPOE_InsertUpdateAdviceProcess([FromBody] AdviceProcessBO objBO)
		{
			string result = repositoryPrescription.CPOE_InsertUpdateAdviceProcess(objBO);
			return Request.CreateResponse(HttpStatusCode.OK, result);
		}
		[HttpPost]
		[Route("CPOE_InsertVitalSign")]
		public HttpResponseMessage CPOE_InsertVitalSign([FromBody] VitalSignBO objBO)
		{
			string result = repositoryPrescription.CPOE_InsertVitalSign(objBO);
			return Request.CreateResponse(HttpStatusCode.OK, result);
		}
		[HttpPost]
		[Route("CPOE_InsertPrescribedItems")]
		public HttpResponseMessage CPOE_InsertPrescribedItems([FromBody] PrescribedItemsBO objBO)
		{
			string result = repositoryPrescription.CPOE_InsertPrescribedItems(objBO.ipPrescription,objBO.objItems,objBO.objMedicine);
			return Request.CreateResponse(HttpStatusCode.OK, result);
		}
		[HttpPost]
		[Route("CPOE_InsertMedicineTemplateInfo")]
		public HttpResponseMessage CPOE_InsertMedicineTemplateInfo([FromBody] MedicineTemplateInfo objBO)
		{
			string result = repositoryPrescription.CPOE_InsertMedicineTemplateInfo(objBO);
			return Request.CreateResponse(HttpStatusCode.OK, result);
		}
	}
}
