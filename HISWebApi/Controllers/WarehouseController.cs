using HISWebApi.Models;
using HISWebApi.Repository.Warehouse;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using static HISWebApi.Models.Warehouse;

namespace HISWebApi.Controllers
{
	[RoutePrefix("api/warehouse")]
	public class WarehouseController : ApiController
	{
        private Audit  audit_repository = new Audit();
        private Master repository = new Master();
		private Indent repoIndent = new Indent();
        private PharmacyStore pharStore = new PharmacyStore();
        private Consumption repoconsumption = new Consumption();
        #region Master
        [HttpPost]
        [Route("wh_VendorApprove")]
        public HttpResponseMessage wh_VendorApprove([FromBody] VendorApprove objBO)
        {
            string Result = repository.wh_VendorApprove(objBO);
            return Request.CreateResponse(HttpStatusCode.OK, Result);
        }
        [HttpPost]
		[Route("DeleteWharehouseDepartment")]
		public HttpResponseMessage DeleteWharehouseDepartment([FromBody] WhDepartmentBO objBO)
		{
			string Result = repository.DeleteWharehouseDepartment(objBO);
			return Request.CreateResponse(HttpStatusCode.OK, Result);
		}
		[HttpPost]
		[Route("DeleteWharehouseCart")]
		public HttpResponseMessage DeleteWharehouseCart([FromBody] WhCartBO objBO)
		{
			string Result = repository.DeleteWharehouseCart(objBO);
			return Request.CreateResponse(HttpStatusCode.OK, Result);
		}

		[HttpPost]
		[Route("MasterQueries")]
		public HttpResponseMessage MasterQueries([FromBody] ManufacturerBO objBO)
		{
			dataSet ds = repository.MasterQueries(objBO);
			return Request.CreateResponse(HttpStatusCode.OK, ds);
		}
		[HttpPost]
		[Route("InsertUpdateManufacturer")]
		public HttpResponseMessage InsertUpdateManufacturer([FromBody] ManufacturerBO objBO)
		{
			string Result = repository.InsertUpdateManufacturer(objBO);
			return Request.CreateResponse(HttpStatusCode.OK, Result);
		}
		[HttpPost]
		[Route("InsertUpdateCategory")]
		public HttpResponseMessage InsertUpdateCategory([FromBody] CategoryBO objBO)
		{
			string Result = repository.InsertUpdateCategory(objBO);
			return Request.CreateResponse(HttpStatusCode.OK, Result);
		}
		[HttpPost]
		[Route("InsertUpdateItemMaster")]
		public HttpResponseMessage InsertUpdateItemMaster([FromBody] ItemMasterBO objBO)
		{
			string Result = repository.InsertUpdateItemMaster(objBO);
			return Request.CreateResponse(HttpStatusCode.OK, Result);
		}
		[HttpPost]
		[Route("InsertUpdatePackType")]
		public HttpResponseMessage InsertUpdatePackType([FromBody] PackTypeBO objBO)
		{
			string Result = repository.InsertUpdatePackType(objBO);
			return Request.CreateResponse(HttpStatusCode.OK, Result);
		}
		[HttpPost]
		[Route("InsertUpdateVendorMaster")]
		public HttpResponseMessage InsertUpdateVendorMaster([FromBody] VendorMasterBO objBO)
		{
			string Result = repository.InsertUpdateVendorMaster(objBO);
			return Request.CreateResponse(HttpStatusCode.OK, Result);
		}

		[HttpPost]
		[Route("InsertUpdateWHDepartment")]
		public HttpResponseMessage InsertUpdateWHDepartment([FromBody] WhDepartmentBO objBO)
		{
			string Result = repository.InsertUpdateWhDepartment(objBO);
			return Request.CreateResponse(HttpStatusCode.OK, Result);
		}
		[HttpPost]
		[Route("wh_LinkPackManufacturerToItem")]
		public HttpResponseMessage wh_LinkPackManufacturerToItem([FromBody] ItemMasterBO objBO)
		{
			string Result = repository.wh_LinkPackManufacturerToItem(objBO);
			return Request.CreateResponse(HttpStatusCode.OK, Result);
		}
		[HttpPost]
		[Route("InsertUpdateEmployeeCartLink")]
		public HttpResponseMessage InsertUpdateEmpAssignToCart([FromBody] WhEmpAssignEmpToCart objBO)
		{
			string Result = repository.InsertUpdateEmpAssignToCart(objBO);
			return Request.CreateResponse(HttpStatusCode.OK, Result);
		}
		#endregion Master

		#region PurchaseEntry
		private Purchase repositoryPurchase = new Purchase();

		[HttpPost]
		[Route("wh_GoodsReturnQueries")]
		public HttpResponseMessage wh_GoodsReturnQueries([FromBody] GoodsReturnQueriesBO objBO)
		{
			dataSet ds = repositoryPurchase.wh_GoodsReturnQueries(objBO);
			return Request.CreateResponse(HttpStatusCode.OK, ds);
		}
		[HttpPost]
		[Route("PurchaseQuery")]
		public HttpResponseMessage PurchaseQuery([FromBody] PurchaseQueriesBO objBO)
		{
			dataSet ds = repositoryPurchase.PurchaseQuery(objBO);
			return Request.CreateResponse(HttpStatusCode.OK, ds);
		}
		[HttpPost]
		[Route("PurchaseInsert")]
		public HttpResponseMessage InsertPurchase([FromBody] PurchaseBO objBO)
		{
			string result = repositoryPurchase.PurchaseInsert(objBO);
			return Request.CreateResponse(HttpStatusCode.OK, result);
		}
        [HttpPost]
        [Route("wh_PurchaseDiascountReprocess")]
        public HttpResponseMessage wh_PurchaseDiascountReprocess([FromBody] DiascountReprocessBO objBO)
        {
            string result = repositoryPurchase.wh_PurchaseDiascountReprocess(objBO);
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }
        [HttpPost]
		[Route("PurchasePosting")]
		public HttpResponseMessage PurchasePosting([FromBody] PurchaseBO objBO)
		{
			string result = repositoryPurchase.PurchasePosting(objBO);
			return Request.CreateResponse(HttpStatusCode.OK, result);
		}
		[HttpPost]
		[Route("PurchaseDelete")]
		public HttpResponseMessage DeletePurchase([FromBody] PurchaseBO objBO)
		{
			string result = repositoryPurchase.PurchaseDelete(objBO);
			return Request.CreateResponse(HttpStatusCode.OK, result);
		}
		[HttpPost]
		[Route("wh_PurchaseImportStock")]
		public HttpResponseMessage wh_PurchaseImportStock([FromBody] PurchaseBO objBO)
		{
			string result = repositoryPurchase.wh_PurchaseImportStock(objBO);
			return Request.CreateResponse(HttpStatusCode.OK, result);
		}
		[HttpPost]
		[Route("wh_PurchaseReturnInsert")]
		public HttpResponseMessage wh_PurchaseReturnInsert([FromBody] GoodsReturnQueriesBO objBO)
		{
			string result = repositoryPurchase.wh_PurchaseReturnInsert(objBO);
			return Request.CreateResponse(HttpStatusCode.OK, result);
		}
        [HttpPost]
        [Route("UploadPurchaseDocument")]
        public async Task<HttpResponseMessage> UploadPurchaseDocument()
        {
            HttpResponseMessage response = new HttpResponseMessage();
            Models.SubmitStatus ss = new Models.SubmitStatus();
            if (!Request.Content.IsMimeMultipartContent())
            {
                ss.Status = 0;
                ss.Message = "This is not multipart content";
                response = Request.CreateResponse(HttpStatusCode.UnsupportedMediaType, "This is not multipart content");
            }
            try
            {
                var filesReadToProvider = await Request.Content.ReadAsMultipartAsync();
                //Json String of object  ipUploadPurchaseDocument to be send at first or 0 index parameter   
                var json = await filesReadToProvider.Contents[0].ReadAsStringAsync();
                Models.ipDocumentInfo obj = JsonConvert.DeserializeObject<Models.ipDocumentInfo>(json);
                //Image to be send at second or 1 index parameter  
                byte[] fileBytes = await filesReadToProvider.Contents[1].ReadAsByteArrayAsync();
                ss = repositoryPurchase.UploadPurchaseDocument(obj.TranId, obj.ImageType, fileBytes, obj.login_id);
                response = Request.CreateResponse(HttpStatusCode.OK, ss);
            }
            catch (Exception ex)
            {
                ss.Status = 0;
                ss.Message = ex.Message;
                response = Request.CreateResponse(HttpStatusCode.OK, ss);
            }
            return response;
        }
        [HttpPost]
        [Route("wh_purchaseOrder_Query")]
        public HttpResponseMessage wh_purchaseOrder_Query([FromBody] PurchaseBO objBO)
        {
            if (objBO.Logic == "ViewOrder")
            {
                dataSet ds = repositoryPurchase.wh_purchaseOrder_Query(objBO);
                HISWebApi.Repository.Utilities.ExcelGenerator obj = new Repository.Utilities.ExcelGenerator();
                return obj.GetExcelFile(ds.ResultSet);
            }
            else
            {
                dataSet ds = repositoryPurchase.wh_purchaseOrder_Query(objBO);
                return Request.CreateResponse(HttpStatusCode.OK, ds);
            }
        }

        [HttpPost]
        [Route("wh_purchaseOrder_Process")]
        public HttpResponseMessage wh_purchaseOrder_Process([FromBody] PurchaseBO objBO)
        {
            string result = repositoryPurchase.wh_purchaseOrder_Process(objBO);
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }
        #endregion PurchaseEntry
        #region Warehouse INDENT 

        [HttpPost]
		[Route("wh_IndentCartQueries")]
		public HttpResponseMessage wh_IndentCartQueries([FromBody] IdentBO objBO)
		{
			dataSet ds = repoIndent.wh_IndentCartQueries(objBO);
			return Request.CreateResponse(HttpStatusCode.OK, ds);
		}

		[HttpPost]
		[Route("wh_IndentProcessQueries")]
		public HttpResponseMessage wh_IndentProcessQueries([FromBody] IdentBO objBO)
		{
			dataSet ds = repoIndent.IndentProcessQueries(objBO);
			return Request.CreateResponse(HttpStatusCode.OK, ds);
		}

		[HttpPost]
		[Route("wh_IndentStockQueries")]
		public HttpResponseMessage wh_IndentStockQueries([FromBody] IdentStockBO objBO)
		{
			dataSet ds = repoIndent.IndentStockQueries(objBO);
			return Request.CreateResponse(HttpStatusCode.OK, ds);
		}

		[HttpPost]
		[Route("DownloadStockReport")]
		public HttpResponseMessage DownloadStockReport([FromBody] IdentStockBO objBO)
		{
			dataSet ds = repoIndent.IndentStockQueries(objBO);
			HISWebApi.Repository.Utilities.ExcelGenerator obj = new Repository.Utilities.ExcelGenerator();
			return obj.GetExcelFile(ds.ResultSet);
		}

		[HttpPost]
		[Route("wh_DispatchedQuery")]
		public HttpResponseMessage wh_DispatchedQuery([FromBody] DispatchOrderBo objBO)
		{
			dataSet ds = repoIndent.DispatchedQuery(objBO);
			return Request.CreateResponse(HttpStatusCode.OK, ds);
		}

		[HttpPost]
		[Route("wh_InsertUpdateIndent")]
		public HttpResponseMessage wh_InsertUpdateIndent([FromBody] IdentBO objBO)
		{
			string Result = repoIndent.wh_InsertUpdateIndent(objBO);
			return Request.CreateResponse(HttpStatusCode.OK, Result);
		}
        [HttpPost]
        [Route("wh_IndentReplaceAndUpdateQty")]
        public HttpResponseMessage wh_IndentReplaceAndUpdateQty(IndentReplacement objBO)
        {
            dataSet Result = repoIndent.wh_IndentReplaceAndUpdateQty(objBO);
            return Request.CreateResponse(HttpStatusCode.OK, Result);
        }

        [HttpPost]
		[Route("wh_DispatchStock")]
		public HttpResponseMessage DispatchStock([FromBody] DispatchOrderBo objBO)
		{
			string Result = repoIndent.DispatchStock(objBO);
			return Request.CreateResponse(HttpStatusCode.OK, Result);
		}

		[HttpPost]
		[Route("wh_CompleteDispatch")]
		public HttpResponseMessage wh_CompleteDispatch([FromBody] DispatchOrderBo objBO)
		{
			string Result = repoIndent.CompleteDispatch(objBO);
			return Request.CreateResponse(HttpStatusCode.OK, Result);
		}

		[HttpPost]
		[Route("wh_DeleteDispatch")]
		public HttpResponseMessage wh_DeleteDispatch([FromBody] DispatchOrderBo objBO)
		{
			dataSet Result = repoIndent.DeleteDispatch(objBO);
			return Request.CreateResponse(HttpStatusCode.OK, Result);
		}
        [HttpPost]
        [Route("wh_DeleteDispatchWithoutIndent")]
        public HttpResponseMessage DeleteDispatchWithoutIndent([FromBody] DispatchOrderBo objBO)
        {
            string Result = repoIndent.DeleteDispatchWithoutIndent(objBO);
            return Request.CreateResponse(HttpStatusCode.OK, Result);
        }

        [HttpPost]
		[Route("wh_PrintDispatchedDetails")]
		public HttpResponseMessage wh_PrintDispatchedDetails([FromBody] DispatchOrderBo objBO)
		{
			dataSet ds = repoIndent.DispatchedQuery(objBO);
			return Request.CreateResponse(HttpStatusCode.OK, ds);
		}

		[HttpPost]
		[Route("wh_PrintDispatchedByLotNo")]
		public HttpResponseMessage wh_PrintDispatchedByLotNo([FromBody] DispatchOrderBo objBO)
		{
			dataSet ds = repoIndent.DispatchedQuery(objBO);
			return Request.CreateResponse(HttpStatusCode.OK, ds);
		}
		[HttpPost]
		[Route("wh_PendingTransaction")]
		public HttpResponseMessage wh_PendingTransaction([FromBody] DispatchOrderBo objBO)
		{
			dataSet ds = repoIndent.DispatchedQuery(objBO);
			return Request.CreateResponse(HttpStatusCode.OK, ds);
		}

		[HttpPost]
		[Route("wh_TransactionDetails")]
		public HttpResponseMessage wh_TransactionDetails([FromBody]DispatchOrderBo objBO)
		{
			dataSet ds = repoIndent.DispatchedQuery(objBO);
			return Request.CreateResponse(HttpStatusCode.OK, ds);
		}
		[HttpPost]
		[Route("InsertUpdateWHCart")]
		public HttpResponseMessage InsertUpdateWHCart([FromBody] WhCartBO objBO)
		{
			string Result = repository.InsertUpdateWhCart(objBO);
			return Request.CreateResponse(HttpStatusCode.OK, Result);
		}
		[HttpPost]
		[Route("wh_ReceiveProduct")]
		public HttpResponseMessage wh_ReceiveProduct([FromBody]DispatchOrderBo objBO)
		{
			string Result = repoIndent.ReceiveProduct(objBO);
			return Request.CreateResponse(HttpStatusCode.OK, Result);
		}
		#endregion
		#region Consumption
		[HttpPost]
		[Route("wh_ConsumptionQueries")]
		public HttpResponseMessage wh_ConsumptionQueries([FromBody] ConsumptionBO objBO)
		{
			dataSet ds = repoconsumption.ConsumptionQueries(objBO);
			return Request.CreateResponse(HttpStatusCode.OK, ds);
		}
		[HttpPost]
		[Route("wh_InsertUpdateConsumption")]
		public HttpResponseMessage wh_InsertUpdateConsumption([FromBody] ConsumptionBO objBO)
		{
			string Result = repoconsumption.InsertUpdateConsumption(objBO);
			return Request.CreateResponse(HttpStatusCode.OK, Result);
		}
        [HttpPost]
        [Route("Hospital_GeneralStoreQueries")]
        public HttpResponseMessage Hospital_GeneralStoreQueries([FromBody] GeneralStoreQueries objBO)
        {
            dataSet ds =  pharStore.Hospital_GeneralStoreQueries(objBO);
            return Request.CreateResponse(HttpStatusCode.OK, ds);
        }
        [HttpPost]
        [Route("wh_ImportFromPharmacy")]
        public HttpResponseMessage wh_ImportFromPharmacy([FromBody]GeneralStoreQueries objBO)
        {
            string Result = repoIndent.wh_ImportFromPharmacy(objBO);
            return Request.CreateResponse(HttpStatusCode.OK, Result);
        }

        [HttpPost]
        [Route("wh_ConsumeStockByPatient")]
        public HttpResponseMessage wh_ConsumeStockByPatient(List<ipPatientConsume> objBO)
        {
            string Result = repoconsumption.wh_ConsumeStockByPatient(objBO);
            return Request.CreateResponse(HttpStatusCode.OK, Result);
        }

        //[Route("wh_ConsumeStockByPatient")]
        //public HttpResponseMessage wh_ConsumeStockByPatient([FromBody] ConsumeStockByPatient objBO)
        //{
        //    string Result = repoconsumption.wh_ConsumeStockByPatient(objBO.objConsume, objBO.objStock);
        //    return Request.CreateResponse(HttpStatusCode.OK, Result);
        //}
        [HttpPost]
        [Route("wh_IndentQueries")]
        public HttpResponseMessage wh_IndentQueries([FromBody] IdentBO objBO)
        {
            dataSet ds = repoIndent.IndentQueries(objBO);
            return Request.CreateResponse(HttpStatusCode.OK, ds);
        }

        #endregion
        #region Audit Block

        [HttpPost]
        [Route("wh_AuditQueries")]
        public HttpResponseMessage wh_AuditQueries([FromBody] AuditBO objBO)
        {
            dataSet ds = audit_repository.wh_AuditQueries(objBO);
            return Request.CreateResponse(HttpStatusCode.OK, ds);
        }
        [HttpPost]
        [Route("wh_AuditCompletion")]
        public HttpResponseMessage wh_AuditCompletion([FromBody] AuditInfoBO objBO)
        {
            string result = audit_repository.wh_AuditCompletion(objBO);
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }
        [HttpPost]
        [Route("DownloadAuditReport")]
        public HttpResponseMessage DownloadAuditReport([FromBody] AuditBO objBO)
        {
            dataSet ds = audit_repository.wh_AuditQueries(objBO);
            HISWebApi.Repository.Utilities.ExcelGenerator obj = new Repository.Utilities.ExcelGenerator();
            return obj.GetExcelFile(ds.ResultSet);
        }

        [HttpPost]
        [Route("wh_InsertAuditMaster")]
        public HttpResponseMessage wh_InsertAuditMaster([FromBody] AuditMasterBO objBO)
        {
            string result = audit_repository.wh_InsertAuditMaster(objBO);
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }

        [HttpPost]
        [Route("wh_InsertAuditInfo")]
        public HttpResponseMessage wh_InsertAuditInfo([FromBody] AuditInfoBO objBO)
        {
            string result = audit_repository.wh_InsertAuditInfo(objBO);
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }
        #endregion
        #region Reports
        Report repositoryReport = new Report();
        [HttpPost]
        [Route("wh_ReportQueries")]
        public HttpResponseMessage wh_PendingTransaction([FromBody] ReportBO objBO)
        {
            if (objBO.Logic == "StockIssueReport")
            {
                dataSet ds = repositoryReport.wh_ReportQueries(objBO);
                HISWebApi.Repository.Utilities.ExcelGenerator obj = new Repository.Utilities.ExcelGenerator();
                return obj.GetExcelFile(ds.ResultSet);
            }
            else
            {
                dataSet ds = repositoryReport.wh_ReportQueries(objBO);
                return Request.CreateResponse(HttpStatusCode.OK, ds);
            }
        }
        [HttpPost]
        [Route("wh_MiscellaneousReportQueries")]
        public HttpResponseMessage wh_MiscellaneousReportQueries([FromBody] ReportBO objBO)
        {
            dataSet ds = repositoryReport.wh_MiscellaneousReportQueries(objBO);
            return Request.CreateResponse(HttpStatusCode.OK, ds);
        }
        [HttpPost]
        [Route("wh_PurchaseReportQueries")]
        public HttpResponseMessage wh_PurchaseReportQueries([FromBody] ReportBO objBO)
        {
            if (objBO.ReportType == "XL")
            {
                dataSet ds = repositoryReport.wh_PurchaseReportQueries(objBO);
                HISWebApi.Repository.Utilities.ExcelGenerator obj = new Repository.Utilities.ExcelGenerator();
                return obj.GetExcelFile(ds.ResultSet);
            }
            else
            {
                dataSet ds = repositoryReport.wh_PurchaseReportQueries(objBO);
                return Request.CreateResponse(HttpStatusCode.OK, ds);
            }
        }
        #endregion Reports
        //Receive Stock from Diagnostic
        #region ReceiveStockDiagnostic		
        [HttpPost]
        [Route("Unit_TransferQueries")]
        public HttpResponseMessage Unit_TransferQueries([FromBody] TransferQueries objBO)
        {
            dataSet ds = repoIndent.Unit_TransferQueries(objBO);
            return Request.CreateResponse(HttpStatusCode.OK, ds);
        }
        [HttpPost]
        [Route("wh_ImportFromDiagnosticWarehouse")]
        public HttpResponseMessage wh_ImportFromDiagnosticWarehouse([FromBody] TransferQueries objBO)
        {
            string result = repoIndent.wh_ImportFromDiagnosticWarehouse(objBO);
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }
        #endregion ReceiveStockDiagnostic
    }
}
