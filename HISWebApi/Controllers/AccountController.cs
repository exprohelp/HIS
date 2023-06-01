using HISWebApi.Models;
using HISWebApi.Repository.Accounts;
using MISWebApi.Repository.Accounts;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace MISWebApi.Controllers
{
	[RoutePrefix("api/Account")]
	public class AccountController : ApiController
	{
		private Account repositoryAccount = new Account();

		[HttpPost]
		[Route("BindOnloadData")]
		public PaymentInfo[] BindOnloadData([FromBody] ipAccounts ipAcc)
		{
			return repositoryAccount.BindOnloadData(ipAcc);
		}
		[HttpPost]
		[Route("Bind_PayTo_PayBy")]
		public PayToPayByInfo[] Bind_PayTo_PayBy([FromBody] ipAccounts ipAcc)
		{
			return repositoryAccount.Bind_PayTo_PayBy(ipAcc);
		}
		[HttpPost]
		[Route("SubmitPayment")]
		public PaymentInfo[] SubmitPayment([FromBody] PaymentInfo objPay)
		{
			return repositoryAccount.SubmitPayment(objPay);
		}
		[HttpPost]
		[Route("SearchLedger")]
		public dataSet SearchLedger([FromBody] ipAccounts ipAcc)
		{
			return repositoryAccount.SearchLedger(ipAcc);
		}
		[HttpPost]
		[Route("AC_ViewLedgerInfo")]
		public HttpResponseMessage AC_ViewLedgerInfo([FromBody] ipAccounts ipAcc)
		{
            if(ipAcc.ReportType == "Excel")
            {
                dataSet ds = repositoryAccount.LedgerBooksDataSet(ipAcc);
                HISWebApi.Repository.Utilities.ExcelGenerator obj = new HISWebApi.Repository.Utilities.ExcelGenerator();
                return obj.GetExcelFile(ds.ResultSet);
            }
            else
            {
                string result = repositoryAccount.LedgerBooksInfo(ipAcc);
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
		}
		[HttpPost]
		[Route("AC_VoucherDetail")]
		public HttpResponseMessage AC_VoucherDetail([FromBody] ipAccounts ipAcc)
		{
			string result = repositoryAccount.LedgerBooksDetailOfVoucher(ipAcc);
			return Request.CreateResponse(HttpStatusCode.OK, result);
		}
		#region #Master

		private Master repositoryMaster = new Master();

		[HttpPost]
		[Route("AC_AccountMasterQueries")]
		public HttpResponseMessage AC_AccountMasterQueries([FromBody] GroupMasterBO objBO)
		{
			dataSet ds = repositoryMaster.AC_AccountMasterQueries(objBO);
			return Request.CreateResponse(HttpStatusCode.OK, ds);
		}
		[HttpPost]
		[Route("AC_CreatePrimaryGroups")]
		public HttpResponseMessage AC_CreatePrimaryGroups([FromBody] GroupMasterBO objBO)
		{
			string result = repositoryMaster.AC_CreatePrimaryGroups(objBO);
			return Request.CreateResponse(HttpStatusCode.OK, result);
		}
		[HttpPost]
		[Route("AC_CreateLedgers")]
		public HttpResponseMessage AC_CreateLedgers([FromBody] LedgerMasterBO objBO)
		{
			string result = repositoryMaster.AC_CreateLedgers(objBO);
			return Request.CreateResponse(HttpStatusCode.OK, result);
		}
        #endregion #Master
        #region #Tally

        private Tally repositoryTally = new Tally();

        [HttpPost]
        [Route("Tally_ViewLedgerInfo")]
        public HttpResponseMessage Tally_ViewLedgerInfo([FromBody] ipAccounts objBO)
        {
            dataSet ds = repositoryTally.AC_ViewLedgerInfo(objBO);
            return Request.CreateResponse(HttpStatusCode.OK, ds);
        }
        [HttpPost]
        [Route("Tally_VoucherForSync")]
        public HttpResponseMessage Tally_VoucherForSync([FromBody] ipAccounts objBO)
        {
            string result = repositoryTally.VoucherForSync(objBO);
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }
        #endregion #Tally
    }
}
