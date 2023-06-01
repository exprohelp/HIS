using HISWebApi.Models;
using HISWebApi.Repository.IPD;
using HISWebApi.Repository.IPOPAudit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using static HISWebApi.Models.IPDBO;

namespace HISWebApi.Controllers
{
	[RoutePrefix("api/IPOPAudit")]
	public class IPOPAuditController : ApiController
    {
		private IPOPAudit repository = new IPOPAudit();
        private HISDBLayer repHISDB = new HISDBLayer();
        [HttpPost]
		[Route("DiagnosticTestAnalysis")]
		public HttpResponseMessage DiagnosticTestAnalysis([FromBody]ipIPDAudit objBO)
		{
			if (objBO.OutPutType == "Excel")
			{
				dataSet ds = repository.DiagnosticTestAnalysis(objBO);
				HISWebApi.Repository.Utilities.ExcelGenerator obj = new Repository.Utilities.ExcelGenerator();
				return obj.GetExcelFile(ds.ResultSet);
			}
			else
			{
				dataSet response = repository.DiagnosticTestAnalysis(objBO);
				return Request.CreateResponse(HttpStatusCode.OK, response);
			}
		}
        [HttpPost]
        [Route("MIS_Report")]
        public HttpResponseMessage MIS_Report([FromBody]ipMISReport objBO)
        {
            if (objBO.OutPutType == "Excel")
            {
                dataSet ds = repository.MIS_Report(objBO);
                HISWebApi.Repository.Utilities.ExcelGenerator obj = new Repository.Utilities.ExcelGenerator();
                return obj.GetExcelFile(ds.ResultSet);
            }
            else
            {
                dataSet response = repository.MIS_Report(objBO);
                return Request.CreateResponse(HttpStatusCode.OK, response);
            }
         
        }
        [HttpPost]
		[Route("IPD_MedicineAudit")]
		public HttpResponseMessage IPD_MedicineAudit([FromBody]ipIPDAudit objBO)
		{
			if (objBO.OutPutType == "Excel")
			{
				dataSet ds = repository.IPD_MedicineAudit(objBO);
				HISWebApi.Repository.Utilities.ExcelGenerator obj = new Repository.Utilities.ExcelGenerator();
				return obj.GetExcelFile(ds.ResultSet);
			}
			else
			{
				dataSet response = repository.IPD_MedicineAudit(objBO);
				return Request.CreateResponse(HttpStatusCode.OK, response);
			}
		}
        [HttpPost]
        [Route("FeedbackReport_Quesries")]
        public HttpResponseMessage FeedbackReport_Quesries([FromBody]ipFeedBack objBO)
        {
            if(objBO.OutPutType == "Excel")
            {
                dataSet ds = repository.FeedbackReport_Quesries(objBO);
                HISWebApi.Repository.Utilities.ExcelGenerator obj = new Repository.Utilities.ExcelGenerator();
                return obj.GetExcelFile(ds.ResultSet);
            }
            else
            {
                dataSet response = repository.FeedbackReport_Quesries(objBO);
                return Request.CreateResponse(HttpStatusCode.OK, response);
            }
        }
        [HttpPost]
        [Route("GetOverDuePatientList")]
        public HttpResponseMessage GetOverDuePatientList([FromBody]ipOverDueAmount objBO)
        {
            if (objBO.OutPutType == "Excel")
            {
                dataSet ds = repHISDB.GetOverDuePatientList(objBO);
                HISWebApi.Repository.Utilities.ExcelGenerator obj = new Repository.Utilities.ExcelGenerator();
                return obj.GetExcelFile(ds.ResultSet);
            }
            else
            {
                dataSet response = repHISDB.GetOverDuePatientList(objBO);
                return Request.CreateResponse(HttpStatusCode.OK, response);
            }
        }
        [HttpPost]
        [Route("CallOverDueInsertUpdate")]
        public HttpResponseMessage CallOverDueInsertUpdate([FromBody]ipOverDueAmount objBO)
        {
             string response = repHISDB.CallOverDueInsertUpdate(objBO);
             return Request.CreateResponse(HttpStatusCode.OK,response);
        }
    }
}
