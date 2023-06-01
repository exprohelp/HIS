using HISWebApi.Models;
using HISWebApi.Repository.OT;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace HISWebApi.Controllers
{
    [RoutePrefix("api/OT")]
    public class OTController : ApiController
    {
        private OT OTrepo = new OT();

        [HttpPost,Route("OTScheduleQueries")]
        public HttpResponseMessage OTScheduleQueries([FromBody]OTSchedule objBO)
        {
            dataSet ds = OTrepo.OTScheduleQueries(objBO);
            return Request.CreateResponse(HttpStatusCode.OK, ds);
        }

    }
}
