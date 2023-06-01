using HISWebApi.Models;
using HISWebApi.Repository.Commission;
using HISWebApi.Repository.IPD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace HISWebApi.Controllers
{
	[RoutePrefix("api/commission")]
    public class CommissionController : ApiController
	{
		HISDBLayer hisdblayer = new HISDBLayer();
		Commission repository = new Commission();
		
		[HttpPost,Route("GetPatientDetailByIPDNo")]
		public HttpResponseMessage GetPatientDetailByIPDNo([FromBody]string IPDNo)
		{
			dataSet response = repository.GetPatientDetailByIPDNo(IPDNo);
			return Request.CreateResponse(HttpStatusCode.OK, response);
		}
        [HttpPost, Route("GetPatientDetailByUHID")]
        public HttpResponseMessage GetPatientDetailByUHID([FromBody]string uhid)
        {
            dataSet response = repository.GetPatientDetailByUHID(uhid);
            return Request.CreateResponse(HttpStatusCode.OK, response);
        }
        [HttpPost,Route("HIS_ShareQueries")]
		public HttpResponseMessage HIS_ShareQueries([FromBody] ReferralCommissionBO objBO)
		{
            if (objBO.ReportType == "Excel")
            {
                dataSet ds = repository.HIS_ShareQueries(objBO);
                HISWebApi.Repository.Utilities.ExcelGenerator obj = new Repository.Utilities.ExcelGenerator();
                return obj.GetExcelFile(ds.ResultSet);
            }
            else
            {
                dataSet ds = repository.HIS_ShareQueries(objBO);
                return Request.CreateResponse(HttpStatusCode.OK, ds);
            }

       
		}	
		[HttpPost]
		[Route("HIS_InsertReferralcommission")]
		public HttpResponseMessage HIS_InsertReferralcommission([FromBody] ReferralCommissionBO objBO)
		{
			string Result = repository.HIS_InsertReferralcommission(objBO);
			return Request.CreateResponse(HttpStatusCode.OK, Result);
		}		
		[HttpPost,Route("Sync_IPDDatafromHISToChandan")]
		public HttpResponseMessage Sync_IPDDatafromHISToChandan([FromBody] ipCommission objBO)
		{
			string result = repository.Sync_IPDDatafromHISToChandan(objBO.IPDNo, objBO.RefCode, objBO.login_id,Convert.ToDecimal(objBO.commisionPerc));
			return Request.CreateResponse(HttpStatusCode.OK, result);
		}
	}
}
