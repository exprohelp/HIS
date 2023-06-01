using HISWebApi.Models;
using HISWebApi.Repository.Temporary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace HISWebApi.Controllers
{
	[RoutePrefix("api/temp")]
	public class TemporaryController : ApiController
    {
		public Temporary repository = new Temporary();

		[HttpPost]
		[Route("Coupon_ReferralBookletQueries")]
		public HttpResponseMessage Coupon_ReferralBookletQueries([FromBody]TempReferralBO objBO)
		{
			dataSet ds = repository.Coupon_ReferralBookletQueries(objBO);
			return Request.CreateResponse(HttpStatusCode.OK, ds);
		}
		[HttpPost]
		[Route("Coupon_ReferralBookletInsertUpdate")]
		public HttpResponseMessage Coupon_ReferralBookletInsertUpdate([FromBody]TempReferralBO objBO)
		{
			string str = repository.Coupon_ReferralBookletInsertUpdate(objBO);
			return Request.CreateResponse(HttpStatusCode.OK, str);
		}

        [HttpPost]
        [Route("GetPatientDetailByUHID")]
        public HttpResponseMessage GetPatientDetailByUHID([FromBody]TempReferralBO objBO)
        {
            dataSet ds = repository.GetPatientDetailByUHID(objBO);
            return Request.CreateResponse(HttpStatusCode.OK, ds);
        }

        [HttpPost]
        [Route("CovidCertificateQueries")]
        public HttpResponseMessage CovidCertificateQueries([FromBody]TempReferralBO objBO)
        {
            dataSet ds = repository.CovidCertificateQueries(objBO);
            return Request.CreateResponse(HttpStatusCode.OK, ds);
        }

        [HttpPost]
        [Route("CovidCertificateInsertUpdate")]
        public HttpResponseMessage CovidCertificateInsertUpdate([FromBody]TempReferralBO objBO)
        {
            string str = repository.CovidCertificateInsertUpdate(objBO);
            return Request.CreateResponse(HttpStatusCode.OK, str);
        }
    }
}
