
using HISWebApi.Models;
using HISWebApi.Repository.Lab;
using HISWebApi.Repository.Lab.Observation;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace HISWebApi.Controllers
{
    [RoutePrefix("api/sample")]
    public class SampleController : ApiController
    {
        private InvestigationMaster investigationMaster = new InvestigationMaster();
        private Sample sampleRepository = new Sample();
        private LabObservation observationRepository = new LabObservation();



        [HttpPost]
        [Route("Lab_SampleCollectionQueries")]
        public HttpResponseMessage Lab_SampleCollectionQueries([FromBody] SampleCollection objBO)
        {
            dataSet ds = sampleRepository.Lab_SampleCollectionQueries(objBO);
            return Request.CreateResponse(HttpStatusCode.OK, ds);
        }
        [HttpPost]
        [Route("Lab_SampleCollection")]
        public HttpResponseMessage Lab_SampleCollection([FromBody]List<SampleCollection> objBO)
        {
            string ds = sampleRepository.Lab_SampleCollection(objBO);
            return Request.CreateResponse(HttpStatusCode.OK, ds);
        }
        [HttpPost]
        [Route("LabReporting_Queries")]
        public HttpResponseMessage LabReporting_Queries([FromBody]LabReporting objBO)
        {
            dataSet ds = sampleRepository.LabReporting_Queries(objBO);
            return Request.CreateResponse(HttpStatusCode.OK, ds);
        }
        // [HttpPost]
        //[Route("LabTransportTime")]
        // public HttpResponseMessage LabTransportTime([FromBody]LabTransportTime objBO)
        // {
        //     string result = observationRepository.LabTransportTime(objBO);
        //     return Request.CreateResponse(HttpStatusCode.OK, result);
        // }
        [Route("LabInsertUpdateOutSource")]
        public HttpResponseMessage LabInsertUpdateOutSource([FromBody]LabOutSource objBO)
        {
            string result = investigationMaster.LabInsertUpdateOutSource(objBO);
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }
        [Route("LabTestProcessingTime")]
        public HttpResponseMessage LabTestProcessingTime([FromBody]LabObservations objBO)
        {
            dataSet ds = observationRepository.LabObservationQueries(objBO);
            return Request.CreateResponse(HttpStatusCode.OK, ds);
        }
        [HttpPost]
        [Route("Lab_ResultEntry")]
        public HttpResponseMessage Lab_ResultEntry([FromBody]List<LabTestResultEntry> objBO)
        {
            string result = sampleRepository.Lab_ResultEntry(objBO);
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }
    }
}
