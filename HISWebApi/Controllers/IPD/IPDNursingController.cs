using HISWebApi.Models;
using HISWebApi.Repository.IPD;
using HISWebApi.Repository.IPD.Nursing;
using HISWebApi.Repository.Utilities;
using System.Collections.Generic;
using System.Data;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using static HISWebApi.Models.IPDBO;

namespace HISWebApi.Controllers
{
    [RoutePrefix("api/IPDNursing")]
    public class IPDNursingController : ApiController
    {
        private MedicineIndent repository = new MedicineIndent();
        private IPD repositoryIPD = new IPD();
        private HISDBLayer hisDB = new HISDBLayer();

        #region IPD
        [HttpPost]
        [Route("IPD_ClinicalSafetyRoundQuesries")]
        public HttpResponseMessage IPD_ClinicalSafetyRoundQuesries([FromBody]IPDBO.FeedbackInfo objBO)
        {
            dataSet response = repositoryIPD.IPD_ClinicalSafetyRoundQuesries(objBO);
            return Request.CreateResponse(HttpStatusCode.OK, response);
        }
        [HttpPost]
        [Route("IPD_InsertClinicalSafetyRound")]
        public HttpResponseMessage IPD_InsertClinicalSafetyRound([FromBody]IPDBO.SafetyRoundInfo objBO)
        {
            string result = repositoryIPD.IPD_InsertClinicalSafetyRound(objBO);
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }
        [HttpPost]
        [Route("IPD_FeedbackQuesries")]
        public HttpResponseMessage IPD_FeedbackQuesries([FromBody]IPDBO.FeedbackInfo objBO)
        {
            dataSet response = repositoryIPD.IPD_FeedbackQuesries(objBO);
            return Request.CreateResponse(HttpStatusCode.OK, response);
        }
        [HttpPost]
        [Route("IPD_InsertFeedback")]
        public HttpResponseMessage IPD_InsertFeedback([FromBody]IPDBO.FeedbackInfo objBO)
        {
            string result = repositoryIPD.IPD_InsertFeedback(objBO);
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }
        [HttpGet]
        [Route("GetAdmittedIPDPatient")]
        public HttpResponseMessage GetAdmittedIPDPatient()
        {
            dataSet response = repository.GetAdmittedIPDPatient();
            return Request.CreateResponse(HttpStatusCode.OK, response);
        }
        [HttpPost]
        [Route("PatientListForFeedBack")]
        public HttpResponseMessage PatientListForFeedBack([FromBody] IPDBO.FeedbackInfo objBO)
        {
            DataSet response = repository.PatientListForFeedBack(objBO);
            return Request.CreateResponse(HttpStatusCode.OK, response);
        }

        [HttpPost]
        [Route("GetOPDEvery10PatientList")]
        public HttpResponseMessage GetOPDEvery10PatientList(HospitalBO obj)
        {
            DataSet response = hisDB.GetOPDEvery10PatientList(obj);
            return Request.CreateResponse(HttpStatusCode.OK, response);
        }
        [HttpGet]
        [Route("GetPatientDetailByIPDNo")]
        public HttpResponseMessage GetPatientDetailByIPDNo(string IPDNo)
        {
            dataSet response = repository.GetPatientDetailByIPDNo(IPDNo);
            return Request.CreateResponse(HttpStatusCode.OK, response);
        }

        [HttpGet]
        [Route("GetDoctor")]
        public HttpResponseMessage GetDoctor()
        {
            dataSet response = repository.GetDoctor();
            return Request.CreateResponse(HttpStatusCode.OK, response);
        }
        [HttpPost]
        [Route("SearchMedicine")]
        public HttpResponseMessage SearchMedicine([FromBody]IPDBO.MedicineBO objBO)
        {
            dataSet response = repository.SearchMedicine(objBO);
            return Request.CreateResponse(HttpStatusCode.OK, response);
        }
        [HttpPost]
        [Route("Insert_IPOPIndent")]
        public HttpResponseMessage Insert_IPOPIndent(List<IPDBO.IPOPIndentBO> objBO)
        {
            string Result = repository.Insert_IPOPIndent(objBO);
            return Request.CreateResponse(HttpStatusCode.OK, Result);
        }
        [HttpPost]
        [Route("IPOP_IndentQueries")]
        public HttpResponseMessage IPOP_IndentQueries([FromBody]IPDBO.ipIndentReport objBO)
        {
            dataSet response = repository.IPOP_IndentQueries(objBO);
            return Request.CreateResponse(HttpStatusCode.OK, response);
        }
        [HttpPost]
        [Route("InPatientIndentReport")]
        public HttpResponseMessage InPatientIndentReport([FromBody]IPDBO.ipIndentReport objBO)
        {
            dataSet response = repository.InPatientIndentReport(objBO);
            return Request.CreateResponse(HttpStatusCode.OK, response);
        }
        [HttpGet]
        [Route("GetNurseDetails")]
        public HttpResponseMessage GetNurseDetails()
        {
            dataSet response = repository.GetNurseDetails();
            return Request.CreateResponse(HttpStatusCode.OK, response);
        }
        [HttpPost]
        [Route("GetKitInformation")]
        public HttpResponseMessage GetKitInformation(IPDBO.KitInfoBO objBO)
        {
            dataSet response = repository.GetKitInformation(objBO);
            return Request.CreateResponse(HttpStatusCode.OK, response);
        }
        [HttpPost]
        [Route("HandOverTakeOverInsert")]
        public HttpResponseMessage HandOverTakeOverInsert([FromBody]List<IPDBO.ipHandoverInfo> objBO)
        {
            string ds = repository.HandOverTakeOverInsert(objBO);
            return Request.CreateResponse(HttpStatusCode.OK, ds);
        }
        [HttpPost]
        [Route("PatientFeedBackOTP")]
        public HttpResponseMessage PatientFeedBackOTP(HospitalBO obj)
        {
            string otpvalue = OTPGenerator.PatientFeedBackOTP(obj);
            if (!string.IsNullOrEmpty(otpvalue))
            {
                return Request.CreateResponse(HttpStatusCode.OK, otpvalue);
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.OK, "Otp not found");
            }
        }
        [HttpPost]
        [Route("IPD_RegistrationQueries")]
        public HttpResponseMessage IPD_RegistrationQueries([FromBody]IPDInfo objBO)
        {
            dataSet response = repositoryIPD.IPD_RegistrationQueries(objBO);
            return Request.CreateResponse(HttpStatusCode.OK, response);
        }
        [HttpPost]
        [Route("IPD_RegistrationInsertUpdate")]
        public HttpResponseMessage IPD_RegistrationInsertUpdate([FromBody]IPDRegistration obj)
        {
            string ds = repositoryIPD.IPD_RegistrationInsertUpdate(obj.objPatient, obj.objBooking);
            return Request.CreateResponse(HttpStatusCode.OK, ds);
        }
        [HttpPost]
        [Route("IPD_TakeAdvance")]
        public HttpResponseMessage IPD_TakeAdvance([FromBody]IPDTakeAdvance obj)
        {
            string ds = repositoryIPD.IPD_TakeAdvance(obj.objBooking, obj.objPayment);
            return Request.CreateResponse(HttpStatusCode.OK, ds);
        }
        #endregion           
    }
}
