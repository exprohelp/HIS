using HISWebApi.Models;
using HISWebApi.Repository.IPD;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using static HISWebApi.Models.IPDBO;

namespace HISWebApi.Controllers
{
    [RoutePrefix("api/IPDNursingService")]
    public class IPDNursingServiceController : ApiController
    {
        private IPDNursingServiceRep objRepNS = new IPDNursingServiceRep();

        [HttpPost]
        [Route("IPD_PatientQueries")]
        public HttpResponseMessage IPD_PatientQueries([FromBody]IPDInfo objBO)
        {
            dataSet response = objRepNS.IPD_PatientQueries(objBO);
            return Request.CreateResponse(HttpStatusCode.OK, response);
        }
        [HttpPost]
        [Route("pIPD_ItemsRate")]
        public HttpResponseMessage pIPD_ItemsRate([FromBody]IPDInfo objBO)
        {
            dataSet response = objRepNS.pIPD_ItemsRate(objBO);
            return Request.CreateResponse(HttpStatusCode.OK, response);
        }
        [HttpPost]
        [Route("IPD_NursingItemInsert")]
        public HttpResponseMessage IPD_NursingItemInsert([FromBody]serviceBooking obj)
        {
            string response = objRepNS.IPD_NursingItemInsert(obj.objBooking, obj.objRateList);
            return Request.CreateResponse(HttpStatusCode.OK, response);
        }
        [HttpPost]
        [Route("IPD_RoomAndDoctorShift")]
        public HttpResponseMessage IPD_RoomAndDoctorShift([FromBody]RoomShiftInfo obj)
        {
            string response = objRepNS.IPD_RoomAndDoctorShift(obj);
            return Request.CreateResponse(HttpStatusCode.OK, response);
        }
        [HttpPost]
        [Route("IPD_InsertDischargeStatus")]
        public HttpResponseMessage IPD_InsertDischargeStatus([FromBody]DischargeStatusInfo obj)
        {
            string response = objRepNS.IPD_InsertDischargeStatus(obj);
            return Request.CreateResponse(HttpStatusCode.OK, response);
        }
        [HttpPost]
        [Route("IPD_GenerateBill")]
        public HttpResponseMessage IPD_GenerateBill([FromBody]DischargeStatusInfo obj)
        {
            string response = objRepNS.IPD_GenerateBill(obj);
            return Request.CreateResponse(HttpStatusCode.OK, response);
        }
        [HttpPost]
        [Route("BB_InsertModifyIndentInfo")]
        public HttpResponseMessage BB_InsertModifyIndentInfo([FromBody]BBIndentInfo obj)
        {
            string response = objRepNS.BB_InsertModifyIndentInfo(obj);
            return Request.CreateResponse(HttpStatusCode.OK, response);
        }
        [HttpPost]
        [Route("IPD_InsertBloodRequisition")]
        public HttpResponseMessage IPD_InsertBloodRequisition([FromBody]BloodRequisition objBO)
        {
            string response = objRepNS.IPD_InsertBloodRequisition(objBO.bloodRequisitionInfo, objBO.componentInfo);
            return Request.CreateResponse(HttpStatusCode.OK, response);
        }
    }
}
