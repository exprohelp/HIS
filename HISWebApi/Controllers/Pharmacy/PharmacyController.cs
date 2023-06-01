using HISWebApi.Models;
using HISWebApi.Repository.Pharmacy;
using HISWebApi.Repository.Utilities;
using System.Data;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace HISWebApi.Controllers
{
    [RoutePrefix("api/Pharmacy")]
    public class PharmacyController : ApiController
    {
        Pharmacy repository = new Pharmacy();
        [HttpPost]
        [Route("Pharmacy_Queries")]
        public HttpResponseMessage Pharmacy_Queries(ipPharmacyInfo objBO)
        {
            if (objBO.ReportType == "Excel")
            {
                dataSet response = repository.Pharmacy_Queries(objBO);
                ExcelGenerator obj = new ExcelGenerator();
                return obj.GetExcelFile(response.ResultSet);
            }
            else
            {
                dataSet response = repository.Pharmacy_Queries(objBO);
                return Request.CreateResponse(HttpStatusCode.OK, response);
            }
        }
        [HttpPost]
        [Route("Hospital_Queries")]
        public HttpResponseMessage Hospital_Queries(ipPharmacyInfo objBO)
        {
            if (objBO.ReportType == "Excel")
            {
                dataSet response = repository.Hospital_Queries(objBO);
                ExcelGenerator obj = new ExcelGenerator();
                return obj.GetExcelFile(response.ResultSet);

            }
            else
            {
                dataSet response = repository.Hospital_Queries(objBO);
                return Request.CreateResponse(HttpStatusCode.OK, response);
            }
        }
        [HttpPost]
        [Route("Hospital_VerifyIPDBill")]
        public HttpResponseMessage Hospital_VerifyIPDBill(ipPharmacyInfo objBO)
        {           
            string response = repository.Hospital_VerifyIPDBill(objBO);
            return Request.CreateResponse(HttpStatusCode.OK, response);
        }
        [HttpPost]
        [Route("Hospital_BillPushReport")]
        public HttpResponseMessage Hospital_BillPushReport([FromBody]ipPharmacyInfo objBO)
        {
            dataSet ds = repository.Hospital_BillPushReport(objBO);
            return Request.CreateResponse(HttpStatusCode.OK, ds);
        }
    }
}
