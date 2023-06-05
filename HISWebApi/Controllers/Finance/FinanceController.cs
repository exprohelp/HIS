using HISWebApi.Models;
using HISWebApi.Repository.Finance;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace HISWebApi.Controllers
{
    [RoutePrefix("api/Finance")]
    public class FinanceController : ApiController
    {
        private Finance repository = new Finance();

        [HttpPost]
        [Route("Financial_Queries")]
        public HttpResponseMessage Financial_Queries([FromBody] ipFinance objBO)
        {
            dataSet ds = repository.Financial_Queries(objBO);
            return Request.CreateResponse(HttpStatusCode.OK, ds);
        }
    }
}
