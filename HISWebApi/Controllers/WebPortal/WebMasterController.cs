using HISWebApi.Models;
using HISWebApi.Repository.WebPortal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace HISWebApi.Controllers.WebPortal
{
    [RoutePrefix("api/WebMaster")]
    public class WebMasterController : ApiController
    {
        private WebMaster repositoryWebMaster = new WebMaster();

        [HttpPost]
        [Route("WebNotificationMaster")]
        public HttpResponseMessage WebNotificationMaster([FromBody] NotificationMaster objBO)
        {
            dataSet ds = repositoryWebMaster.GetNotificationMaster(objBO);
            return Request.CreateResponse(HttpStatusCode.OK, ds);
        }
    }
}
