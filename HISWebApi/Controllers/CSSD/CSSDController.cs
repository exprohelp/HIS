using HISWebApi.Models;
using HISWebApi.Repository.CSSD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using static HISWebApi.Models.CSSD;

namespace HISWebApi.Controllers.CSSD
{
	[RoutePrefix("api/cssd")]
    public class CSSDController : ApiController
    {
		Master masterRepository = new Master();
		Movement movementRepository = new Movement();

		[HttpPost,Route("CSSD_MasterQueries")]
		public HttpResponseMessage CSSD_MasterQueries([FromBody] ipCSSD objBO)
		{
			dataSet ds = masterRepository.CSSD_MasterQueries(objBO);
			return Request.CreateResponse(HttpStatusCode.OK, ds);
		}

		[HttpPost, Route("CSSD_InsertUpdateItemMaster")]
		public HttpResponseMessage CSSD_InsertUpdateItemMaster([FromBody] ItemInfo objBO)
		{
			string result = masterRepository.CSSD_InsertUpdateItemMaster(objBO);
			return Request.CreateResponse(HttpStatusCode.OK, result);
		}
		[HttpPost, Route("CSSD_InsertUpdateSetMaster")]
		public HttpResponseMessage CSSD_InsertUpdateSetMaster([FromBody] SetMasterInfo objBO)
		{
			string result = masterRepository.CSSD_InsertUpdateSetMaster(objBO);
			return Request.CreateResponse(HttpStatusCode.OK, result);
		}

		//Movement
		[HttpPost,Route("CSSD_MovementQueries")]
		public HttpResponseMessage CSSD_MovementQueries([FromBody] ipCSSD objBO)
		{
			dataSet ds = movementRepository.CSSD_MovementQueries(objBO);
			return Request.CreateResponse(HttpStatusCode.OK, ds);
		}
		[HttpPost, Route("CSSD_InsertUpdateMovement")]
		public HttpResponseMessage CSSD_InsertUpdateMovement([FromBody] MovementInfo objBO)
		{
			string result = movementRepository.CSSD_InsertUpdateMovement(objBO);
			return Request.CreateResponse(HttpStatusCode.OK, result);
		}
		[HttpPost, Route("CSSD_ItemDispatchAndReceive")]
		public HttpResponseMessage CSSD_ItemDispatchAndReceive([FromBody] List<MovementInfo> objBO)
		{
			string result = movementRepository.CSSD_ItemDispatchAndReceive(objBO);
			return Request.CreateResponse(HttpStatusCode.OK, result);
		}
	}
}
