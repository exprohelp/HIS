using HISWebApi.Repository.Corporate;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using static HISWebApi.Models.Corporate;

namespace HISWebApi.Controllers
{
    [RoutePrefix("api/Corporate")]
    public class CorporateController : ApiController
    {
        private Panel repository = new Panel();

        [HttpGet]
        [Route("GetPanel")]
        public HttpResponseMessage GetPanel()
        {
            Models.dataSet ds = repository.GetPanel();
            return Request.CreateResponse(HttpStatusCode.OK, ds);
        }
        [HttpPost]
        [Route("PanelQueries")]
        public HttpResponseMessage PanelQueries([FromBody] PanelItemExcludeBO objBO)
        {
            dataSet ds = repository.PanelQueries(objBO);
            return Request.CreateResponse(HttpStatusCode.OK, ds);
        }
        [HttpPost]
        [Route("ExcludeItemForPanel")]
        public HttpResponseMessage ExcludeItemForPanel([FromBody] PanelItemExcludeBO objBO)
        {
            string Result = repository.ExcludeItemForPanel(objBO);
            return Request.CreateResponse(HttpStatusCode.OK, Result);
        }
        [HttpPost]
        [Route("PanelLink")]
        public HttpResponseMessage PanelLink([FromBody] PanelLinkBO objBO)
        {
            string Result = repository.PanelLink(objBO);
            return Request.CreateResponse(HttpStatusCode.OK, Result);
        }
        [HttpPost]
        [Route("PanelLinkUpdateStatus")]
        public HttpResponseMessage PanelLinkUpdateStatus([FromBody]PanelLinkBO objBO)
        {
            string Result = repository.PanelLinkUpdateStatus(objBO);
            return Request.CreateResponse(HttpStatusCode.OK, Result);
        }
        [HttpPost]
        [Route("PanelQuerie")]
        public HttpResponseMessage PanelQuerie([FromBody]PanelInfo objBO)
        {
            dataSet Result = repository.PanelQuerie(objBO);
            return Request.CreateResponse(HttpStatusCode.OK, Result);
        }
        [HttpPost]
        [Route("InsertFundManagement")]
        public HttpResponseMessage InsertFundManagement([FromBody]FundManagementInfo objBO)
        {
            string Result = repository.InsertFundManagement(objBO);
            return Request.CreateResponse(HttpStatusCode.OK, Result);
        }
        //[HttpGet]
        //[Route("GetLocalIPAddress")]
        //public HttpResponseMessage GetLocalIPAddress()
        //{
        //    string Result = repository.GetLocalIPAddress();
        //    return Request.CreateResponse(HttpStatusCode.OK, Result);
        //}
    }
}
