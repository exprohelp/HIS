using HISWebApi.Models;
using HISWebApi.Repository.PatientDesk;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace HISWebApi.Controllers
{
    public class PatientDeskController : ApiController
    {
        private AmbulanceDriverApp repEmergecnyService = new AmbulanceDriverApp();

        [HttpPost]
        [Route("api/PatientDesk/AmbulanceAndEmergencyQueries")]
        public HttpResponseMessage AmbulanceAndEmergencyQueries([FromBody] ipAmbulance ipapp)
        {
            List<AmbulanceResponse> result = repEmergecnyService.AmbulanceAndEmergencyQueries(ipapp);
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }
        [HttpPost]
        [Route("api/PatientDesk/AmbulanceTrackingPatient")]
        public HttpResponseMessage AmbulanceTrackingPatient([FromBody] ipAmbulanceTrack ipapp)
        {
            string result = repEmergecnyService.AmbulanceTrackingPatient(ipapp);
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }
    }
}
