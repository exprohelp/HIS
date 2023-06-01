using HISWebApi.Models;
using HISWebApi.Repository.IPD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using static HISWebApi.Models.IPDBO;

namespace HISWebApi.Controllers
{
    [RoutePrefix("api/IPDDoctor")]
    public class IPDDoctorController : ApiController
    {
        private IPDDoctor repository = new IPDDoctor();

        [HttpPost]
        [Route("IPD_DoctorQueries")]
        public HttpResponseMessage IPD_DoctorQuerries([FromBody]IPDInfo objBO)
        {
            dataSet response = repository.IPD_DoctorQueries(objBO);
            return Request.CreateResponse(HttpStatusCode.OK, response);
        }
        [HttpPost]
        [Route("InsertDischargeTemplate")]
        public HttpResponseMessage InsertDischargeTemplate([FromBody]DischargeTemplateInfo objBO)
        {
            string response = repository.InsertDischargeTemplate(objBO);
            return Request.CreateResponse(HttpStatusCode.OK, response);
        }
        [HttpPost]
        [Route("IPD_InsertDischargeReportInfo")]
        public HttpResponseMessage IPD_InsertDischargeReportInfo([FromBody]DischargeReportInfo objBO)
        {
            string response = repository.IPD_InsertDischargeReportInfo(objBO);
            return Request.CreateResponse(HttpStatusCode.OK, response);
        }
    }
}
