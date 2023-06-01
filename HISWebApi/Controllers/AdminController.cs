using HISWebApi.Models;
using HISWebApi.Repository.Admin;
using HISWebApi.Repository.Hospital;
using HISWebApi.Repository.Online;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace HISWebApi.Controllers
{
    //test
   public class AdminController : ApiController
    {
        private PanelRates repositoryOnline = new PanelRates();
        [HttpPost]
        [Route("api/Admin/UploadPanelRates")]
        public HttpResponseMessage UploadPanelRates([FromBody] ipPanelRates ipapp)
        {
            string result=repositoryOnline.UploadPanelRates(ipapp);
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }
     
      
    }
}
