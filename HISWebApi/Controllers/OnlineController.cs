using HISWebApi.Models;
using HISWebApi.Repository.Hospital;
using HISWebApi.Repository.Online;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace HISWebApi.Controllers
{
    //[EnableCors(origins: "https://chandanOnline.in", headers: "*", methods: "*")]
    public class OnlineController : ApiController
    {
        private OnlineAppointment repositoryOnline = new OnlineAppointment();
        private PackageBooking repositoryPackage = new PackageBooking();

        #region OnlineAppointment
        [HttpPost]
        [Route("api/Online/DoctorAppQueries")]
        public HttpResponseMessage DoctorAppQueries([FromBody] ipOnlineAppQry ipapp)
        {
            dataSet ds = repositoryOnline.DoctorAppQueries(ipapp);
            return Request.CreateResponse(HttpStatusCode.OK, ds);
        }

        [HttpPost]
        [Route("api/Online/BookingNotification")]
        public HttpResponseMessage BookingNotification([FromBody] ipOnlineAppQry ipapp)
        {
            string result = repositoryOnline.BookingNotification(ipapp);
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }
        [HttpPost]
        [Route("api/Online/AppointmentConfirmation")]
        public HttpResponseMessage AppointmentConfirmation([FromBody] ipOnlineConfirmation ipConfirm)
        {
            dataSet ds = repositoryOnline.AppointmentConfirmation(ipConfirm);
            return Request.CreateResponse(HttpStatusCode.OK, ds);
        }
        [HttpPost]
        [Route("api/Online/Online_MarkPayment")]
        public HttpResponseMessage Online_MarkPayment([FromBody] ipOnlinePayment ipPaymet)
        {
            dataSet ds = repositoryOnline.Online_MarkPayment(ipPaymet);
            return Request.CreateResponse(HttpStatusCode.OK, ds);
        }
        [HttpPost]
        [Route("api/Online/getPaymentResponse")]
        public HttpResponseMessage getPaymentResponse([FromBody] ipTransaction obj)
        {
            dataSet dsResult = repositoryOnline.CallWebApiPayUMoney("getPaymentResponse", obj);
            return Request.CreateResponse(HttpStatusCode.OK, dsResult);
        }
        [HttpPost]
        [Route("api/Online/UpdatePaymentStatus")]
        public HttpResponseMessage UpdatePaymentStatus([FromBody] ipUpdatePayStatus obj)
        {
            string result = repositoryOnline.UpdatePaymentStatus(obj);
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }
        #endregion OnlineAppointment

        #region PackageBooking
        [HttpPost]
        [Route("api/Online/PackageQueries")]
        public HttpResponseMessage PackageQueries([FromBody] ipPackageQueries ipapp)
        {
            dataSet ds = repositoryPackage.PackageQueries(ipapp);
            return Request.CreateResponse(HttpStatusCode.OK, ds);
        }
        [HttpPost]
        [Route("api/Online/OnlinePackageNotification")]
        public HttpResponseMessage OnlinePackageNotification([FromBody] ipPackageQueries ipapp)
        {
            string result = repositoryPackage.BookingNotification(ipapp);
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }
        [HttpPost]
        [Route("api/Online/PackageConfirmation")]
        public HttpResponseMessage PackageConfirmation([FromBody] ipPackageOnlineConfirmation ipConfirm)
        {
            dataSet ds = repositoryPackage.PackageConfirmation(ipConfirm);
            return Request.CreateResponse(HttpStatusCode.OK, ds);
        }
        [HttpPost]
        [Route("api/Online/VitalReports")]
        public HttpResponseMessage VitalReports([FromBody] ipPackageQueries obj)
        {
            ChartAndTable result = repositoryPackage.VitalReports(obj);
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }
        [HttpPost]
        [Route("api/Online/UpdateAndCloseVitalRemark")]
        public HttpResponseMessage UpdateAndCloseVitalRemark([FromBody] ipPackageQueries ipapp)
        {
            string result = repositoryPackage.UpdateAndCloseVitalRemark(ipapp);
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }
        [HttpPost]
        [Route("api/Online/HISCustomerList")]
        public HttpResponseMessage HISCustomerList([FromBody] ipPackageQueries ipapp)
        {
            dataSet result = repositoryPackage.HISCustomerList(ipapp);
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }
        [HttpPost]
        [Route("api/Online/LabReportsOfPatient")]
        public HttpResponseMessage LabReportsOfPatient(ipPackageQueries obj)
        {
            string result = repositoryPackage.LabReportsOfPatient(obj);
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }
        [HttpPost]
        [Route("api/Online/InsertCovidServices")]
        public HttpResponseMessage InsertCovidServices([FromBody] CovidServices objBO)
        {
            string result = repositoryPackage.InsertCovidServices(objBO);
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }
        #endregion PackageBooking
        [HttpPost]
        [Route("api/Online/HIS_OPDCall")]
        public HttpResponseMessage HIS_OPDCall([FromBody] HospitalBO ipapp)
        {
            dataSet ds = repositoryOnline.HIS_OPDCall(ipapp);
            return Request.CreateResponse(HttpStatusCode.OK, ds);
        }
        [HttpPost]
        [Route("api/Online/HIS_OPDCancelAndCallDone")]
        public HttpResponseMessage HIS_OPDCancelAndCallDone([FromBody] HospitalBO ipapp)
        {
            string ds = repositoryOnline.HIS_OPDCancelAndCallDone(ipapp);
            return Request.CreateResponse(HttpStatusCode.OK, ds);
        }
        [HttpPost]
        [Route("api/OnlineDiagnostic/CallWebApiPayUMoneyByDate")]
        public HttpResponseMessage CallWebApiPayUMoneyByDate([FromBody] ipTransaction obj)
        {
            string ResultInfo = string.Empty;
            List<TransactionDetail> dsResult = repositoryOnline.CallWebApiPayUMoneyByDate(out ResultInfo, obj);
            return Request.CreateResponse(HttpStatusCode.OK, dsResult);
        }
    }
}
