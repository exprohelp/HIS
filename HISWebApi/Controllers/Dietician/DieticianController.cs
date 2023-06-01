using HISWebApi.Models;
using HISWebApi.Repository.Dietician;
using HISWebApi.Repository.IPD.Nursing;
using System.Collections.Generic;
using System.Data;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace HISWebApi.Controllers
{
    [RoutePrefix("api/Dietician")]
    public class DieticianController : ApiController
    {
        private Diet repositoryDiet = new Diet();
        private MedicineIndent repository = new MedicineIndent();

        [HttpPost]
        [Route("diet_DiticianQueries")]
        public HttpResponseMessage diet_DiticianQueries([FromBody] ipDietician objBO)
        {
            if (objBO.OutPutType == "Excel")
            {
                dataSet ds = repositoryDiet.diet_DiticianQueries(objBO);
                HISWebApi.Repository.Utilities.ExcelGenerator obj = new Repository.Utilities.ExcelGenerator();
                return obj.GetExcelFile(ds.ResultSet);
            }
            else
            {
                dataSet ds = repositoryDiet.diet_DiticianQueries(objBO);
                return Request.CreateResponse(HttpStatusCode.OK, ds);
            }
        }
        [HttpPost]
        [Route("PatientListForDietInit")]
        public HttpResponseMessage PatientListForDietInit([FromBody] ipDietician objBO)
        {
            DataSet response = repository.PatientListForDietInit(objBO);
            return Request.CreateResponse(HttpStatusCode.OK, response);
        }
        [HttpPost]
        [Route("diet_LockOrFreeze")]
        public HttpResponseMessage diet_LockOrFreeze([FromBody] DietPatientInfo objBO)
        {
            string response = repositoryDiet.diet_LockOrFreeze(objBO);
            return Request.CreateResponse(HttpStatusCode.OK, response);
        }
        [HttpPost]
        [Route("diet_InsertUpdateDiet")]
        public HttpResponseMessage diet_InsertUpdateDiet([FromBody] DietPatientInfo objBO)
        {
            string response = repositoryDiet.diet_InsertUpdateDiet(objBO);
            return Request.CreateResponse(HttpStatusCode.OK, response);
        }
        [HttpPost]
        [Route("dietmasterInsertUpdate")]
        public HttpResponseMessage DietmasterInsertUpdate([FromBody] DietMasterBO objBO)
        {
            string result = repositoryDiet.DietmasterInsertUpdate(objBO);
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }
        [HttpPost]
        [Route("dietMasterQueries")]
        public HttpResponseMessage DietmasterQueries([FromBody] DietMasterBO objBO)
        {
            dataSet ds = repositoryDiet.DietmasterQueries(objBO);
            return Request.CreateResponse(HttpStatusCode.OK, ds);
        }
        [HttpPost]
        [Route("diet_InsertDietSchedule")]
        public HttpResponseMessage diet_InsertDietSchedule([FromBody]List<DietScheduleInfo> objBO)
        {
            string result = repositoryDiet.diet_InsertDietSchedule(objBO);
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }
    }
}
