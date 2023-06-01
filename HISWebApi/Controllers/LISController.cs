using HISWebApi.Models;
using HISWebApi.Repository.LIS;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace HISWebApi.Controllers
{
    [RoutePrefix("api/temporary")]
    public class LISController : ApiController
    {
        public LIS repository = new LIS();

        [HttpPost]
        [Route("GetPatient")]
        public HttpResponseMessage GetPatientDetails(LISBO.PatientBO objBO)
        {
            Models.dataSet ds = repository.GetPatientDetails(objBO);
            return Request.CreateResponse(HttpStatusCode.OK, ds);
        }
        [HttpPost]
        [Route("GetAssignedReport")]
        public HttpResponseMessage GetAssignedReport(LISBO.PatientBO objBO)
        {
            string str = repository.GetAssignedReport(objBO);
            return Request.CreateResponse(HttpStatusCode.OK, str);
        }
        [HttpPost]
        [Route("UpdateRemark")]
        public HttpResponseMessage UpdateRemark(LISBO.PatientBO objBO)
        {
            string result = repository.UpdateRemark(objBO);
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }
    }
}
