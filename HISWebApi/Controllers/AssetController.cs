using HISWebApi.Models;
using HISWebApi.Repository.Asset;
using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace HISWebApi.Controllers
{
    [RoutePrefix("api/Asset")]
    public class AssetController : ApiController
    {
        private Asset repositoryAsset = new Asset();
        [HttpPost]
        [Route("Hosp_AssetMovementQueries")]
        public HttpResponseMessage Hosp_AssetMovementQueries([FromBody] ipAsset objBO)
        {
            dataSet ds = repositoryAsset.Hosp_AssetMovementQueries(objBO);
            return Request.CreateResponse(HttpStatusCode.OK, ds);
        }
        [HttpPost]
        [Route("HospitalComplaints")]
        public HttpResponseMessage HospitalComplaints([FromBody] AssetsBO objBO)
        {
            dataSet ds = repositoryAsset.HospitalComplaints(objBO);
            return Request.CreateResponse(HttpStatusCode.OK, ds);
        }
        [HttpPost]
        [Route("Hospital_AssetMovement")]
        public HttpResponseMessage Hospital_AssetMovement([FromBody] ipAssetMovement objBO)
        {
            string result = repositoryAsset.Hospital_AssetMovement(objBO);
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }
        [HttpPost]
        [Route("AssetsQueries")]
        public HttpResponseMessage AssetsQueries([FromBody] AssetsBO objBO)
        {
            dataSet ds = repositoryAsset.AssetsQueries(objBO);
            return Request.CreateResponse(HttpStatusCode.OK, ds);
        }
        [HttpPost]
        [Route("InsertModifyAssets")]
        public HttpResponseMessage Hospital_AssetMovement([FromBody] AssetsBO objBO)
        {
            string result = repositoryAsset.InsertModifyAssets(objBO);
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }

        [HttpPost]
        [Route("insertIntoComplaints")]
        public HttpResponseMessage insertIntoComplaints(AssetsComplaintsBO objBO)
        {
            string Result = "";
            try
            {
                Result = repositoryAsset.insertIntoComplaints(objBO);
                if (Result != "")
                {
                    return Request.CreateResponse(HttpStatusCode.OK, Result);
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound, Result);
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpPost]
        [Route("closeComplaint")]
        public HttpResponseMessage closeComplaint(AssetsComplaintsBO objBO)
        {
            string Result = "";
            Result = repositoryAsset.closeComplaint(objBO);
            return Request.CreateResponse(HttpStatusCode.OK, Result);
        }
        [HttpPost]
        [Route("insertComplaintCommunication")]
        public HttpResponseMessage insertComplaintCommunication(AssetsComplaintsBO objBO)
        {
            string Result = "";
            Result = repositoryAsset.insertComplaintCommunication(objBO);
            return Request.CreateResponse(HttpStatusCode.OK, Result);
        }
        [HttpPost]
        [Route("AssetUnderHeadExcel")]
        public HttpResponseMessage AssetUnderHeadExcel([FromBody] ipAsset objBO)
        {
            dataSet ds = repositoryAsset.Hosp_AssetMovementQueries(objBO);
            HISWebApi.Repository.Utilities.ExcelGenerator obj = new Repository.Utilities.ExcelGenerator();
            return obj.GetExcelFile(ds.ResultSet);
        }

        [HttpPost]
        [Route("MovementReportExcel")]
        public HttpResponseMessage MovementReportExcel([FromBody] ipAsset objBO)
        {
            dataSet ds = repositoryAsset.Hosp_AssetMovementQueries(objBO);
            HISWebApi.Repository.Utilities.ExcelGenerator obj = new Repository.Utilities.ExcelGenerator();
            return obj.GetExcelFile(ds.ResultSet);
        }
    }
}
