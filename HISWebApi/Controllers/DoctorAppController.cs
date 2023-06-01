using HISWebApi.Models;
using HISWebApi.Repository.IPD.Nursing;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace HISWebApi.Controllers
{
	[RoutePrefix("api/DoctorApp")]
	public class DoctorAppController : ApiController
	{
        private HISWebApi.Repository.IPD.MobileApp.DoctorApp obj = new Repository.IPD.MobileApp.DoctorApp();
        #region IPD References
        [HttpPost]
        [Route("GetAdmittedIPDPatientByDoctor")]
        public HttpResponseMessage GetAdmittedIPDPatientByDoctor(ipDoctorApp objBO)
        {
            dataSet response = obj.GetAdmittedIPDPatientByDoctor(objBO.DoctorId);
            return Request.CreateResponse(HttpStatusCode.OK, response);
        }
        [HttpPost]
        [Route("HIS_PatientQOfDoctor")]
        public HttpResponseMessage HIS_PatientQOfDoctor(ipDoctorApp objBO)
        {
            dataSet response = obj.HIS_PatientQOfDoctor(objBO.DoctorId);
            return Request.CreateResponse(HttpStatusCode.OK, response);
        }

        [HttpPost]
        [Route("HIS_GetPatientListByMobileOrUHID")]
        public HttpResponseMessage HIS_GetPatientListByMobileOrUHID(ipDoctorApp objBO)
        {
            dataSet response = obj.HIS_GetPatientListByMobileOrUHID(objBO.Prm1);
            return Request.CreateResponse(HttpStatusCode.OK, response);
        }
        [HttpPost]
        [Route("HIS_DashboardQueries")]
        public HttpResponseMessage HIS_DashboardQueries(ReferralCommissionBO objBO)
        {
            dataSet response = obj.HIS_DashboardQueries(objBO);
            return Request.CreateResponse(HttpStatusCode.OK, response);
        }
        [HttpPost]
        [Route("DoctorShareByDoctorId")]
        public HttpResponseMessage DoctorShareByDoctorId(ReferralCommissionBO objBO)
        {
            string response = obj.DoctorShareByDoctorId(objBO);
            return Request.CreateResponse(HttpStatusCode.OK, response);
        }
        [HttpPost]
        [Route("Doctor_AppQuerries")]
        public HttpResponseMessage Doctor_AppQuerries(ipDoctorApp objBO)
        {
            if (objBO.ReportType == "Excel")
            {
                dataSet response1 = obj.Doctor_AppQuerries(objBO);
                Repository.Utilities.ExcelGenerator obj1 = new Repository.Utilities.ExcelGenerator();
                return obj1.GetExcelFile(response1.ResultSet);
            }
            else
            {
                dataSet response = obj.Doctor_AppQuerries(objBO);
                return Request.CreateResponse(HttpStatusCode.OK, response);
            }
  
        }
        [HttpPost]
        [Route("IPD_ReferenceAndDischargeQueries")]
        public HttpResponseMessage IPD_ReferenceAndDischargeQueries(ipReference objBO)
        {
            dataSet response = obj.IPD_ReferenceAndDischargeQueries(objBO);
            return Request.CreateResponse(HttpStatusCode.OK, response);
        }
        [HttpPost]
        [Route("IPD_ReferenceInsertUpdate")]
        public HttpResponseMessage IPD_ReferenceInsertUpdate(ipReference objBO)
        {
            string response = obj.IPD_ReferenceInsertUpdate(objBO);
            return Request.CreateResponse(HttpStatusCode.OK, response);
        }
        [HttpPost]
        [Route("IPD_OTBookingInsertUpdate")]
        public HttpResponseMessage IPD_OTBookingInsertUpdate(OTBookingInfo objBO)
        {
            string response = obj.IPD_OTBookingInsertUpdate(objBO);
            return Request.CreateResponse(HttpStatusCode.OK, response);
        }
        [HttpPost]
        [Route("Conference_BookingInsertUpdate")]
        public HttpResponseMessage Conference_BookingInsertUpdate(ConferenceBookingInfo objBO)
        {
            string response = obj.Conference_BookingInsertUpdate(objBO);
            return Request.CreateResponse(HttpStatusCode.OK, response);
        }
        #endregion
    }
}
