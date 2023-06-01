using HISWebApi.Models;
using HISWebApi.Repository.LinenLaundry;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using static HISWebApi.Models.Warehouse;

namespace HISWebApi.Controllers
{
	[RoutePrefix("api/LinenLaundry")]
	public class LinenLaundryController : ApiController
	{
		private Master repositoryMaster = new Master();
		private TransferAndReceiving rep_Transfer = new TransferAndReceiving();

        [HttpPost, Route("LL_AuditQry")]
        public HttpResponseMessage LL_AuditQry([FromBody] ipLinenLaundry objBO)
        {
            dataSet ds = rep_Transfer.LL_AuditQry(objBO);
            return Request.CreateResponse(HttpStatusCode.OK, ds);
        }
        [HttpPost, Route("LL_MasterQueries")]
		public HttpResponseMessage LL_MasterQueries([FromBody] ipLinenLaundry objBO)
		{
			dataSet ds = repositoryMaster.LL_MasterQueries(objBO);
			return Request.CreateResponse(HttpStatusCode.OK, ds);
		}
		[HttpPost, Route("LL_InsertUpdateMaster")]
		public HttpResponseMessage LL_InsertUpdateMaster([FromBody] RequestMaster objBO)
		{
			string result = repositoryMaster.LL_InsertUpdateMaster(objBO);
			return Request.CreateResponse(HttpStatusCode.OK, result);
		}
		[HttpPost, Route("LL_TransferAndReceiveQueries")]
		public HttpResponseMessage LL_TransferAndReceiveQueries(ipLinen objBO)
		{
			if (objBO.OutPutType == "Excel")
			{
				dataSet ds = rep_Transfer.LL_TransferAndReceiveQueries(objBO);
				HISWebApi.Repository.Utilities.ExcelGenerator obj = new Repository.Utilities.ExcelGenerator();
				return obj.GetExcelFile(ds.ResultSet);
			}
			else
			{
				dataSet ds = rep_Transfer.LL_TransferAndReceiveQueries(objBO);
				return Request.CreateResponse(HttpStatusCode.OK, ds);
			}
		}
		[HttpPost, Route("LL_TransferStock")]
		public HttpResponseMessage LL_TransferStock(TransferLinen objBO)
		{
			string result = rep_Transfer.LL_TransferStock(objBO);
			return Request.CreateResponse(HttpStatusCode.OK, result);
		}
		[HttpPost, Route("LL_TransferComplete")]
		public HttpResponseMessage LL_TransferComplete(TransferLinen objBO)
		{
			string result = rep_Transfer.LL_TransferComplete(objBO);
			return Request.CreateResponse(HttpStatusCode.OK, result);
		}
		[HttpPost, Route("LL_ReceiveInStock")]
		public HttpResponseMessage LL_ReceiveInStock(TransferLinen objBO)
		{
			string result = rep_Transfer.LL_ReceiveInStock(objBO);
			return Request.CreateResponse(HttpStatusCode.OK, result);
		}
		[HttpPost, Route("LL_AllPendingReceiving")]
		public HttpResponseMessage LL_AllPendingReceiving([FromBody] List<LaundryReceive> objBO)
		{
			string result = rep_Transfer.LL_AllPendingReceiving(objBO);
			return Request.CreateResponse(HttpStatusCode.OK, result);
		}
		[HttpPost, Route("LL_WashingToLaundryReceiving")]
		public HttpResponseMessage LL_WashingToLaundryReceiving([FromBody] LaundryReceive objBO)
		{
			string result = rep_Transfer.LL_WashingToLaundryReceiving(objBO);
			return Request.CreateResponse(HttpStatusCode.OK, result);
		}
		[HttpPost, Route("LL_WashingLaundaryMovement")]
		public HttpResponseMessage LL_WashingLaundaryMovement([FromBody] LaundryReceive objBO)
		{
			string result = rep_Transfer.LL_WashingLaundaryMovement(objBO);
			return Request.CreateResponse(HttpStatusCode.OK, result);
		}
		[HttpPost, Route("LL_LinenDistribution")]
		public HttpResponseMessage LL_LinenDistribution([FromBody]List<LaundryReceive> objBO)
		{
			string result = rep_Transfer.LL_LinenDistribution(objBO);
			return Request.CreateResponse(HttpStatusCode.OK, result);
		}
		[HttpPost, Route("LL_IndentInsert")]
		public HttpResponseMessage LL_IndentInsert([FromBody]IndentInfo objBO)
		{
			string result = rep_Transfer.LL_IndentInsert(objBO);
			return Request.CreateResponse(HttpStatusCode.OK, result);
		}
        [HttpPost, Route("LL_IndentProcess_Queries")]
        public HttpResponseMessage LL_IndentProcess_Queries([FromBody]IdentBO objBO)
        {
            dataSet result = rep_Transfer.LL_IndentProcess_Queries(objBO);
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }
    }
}
