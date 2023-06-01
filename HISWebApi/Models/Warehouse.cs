using System;
using System.Collections.Generic;
using System.Data;

namespace HISWebApi.Models
{
    public class Warehouse
    {
        public class ipWarehouse
        {
            public Nullable<int> ID { get; set; }
            public string Logic { get; set; }
            public string Result { get; set; }
            public bool Flag { get; set; }
            public bool IsActive { get; set; }
            public string login_id { get; set; }
            public string warehouseCartId { get; set; }
            public string prm_1 { get; set; }
            public string prm_2 { get; set; }
            public DateTime? CreatedOn { get; set; }
        }
        public class ReportBO : ipWarehouse
        {
            public DateTime from { get; set; }
            public DateTime to { get; set; }
            public string ItemIds { get; set; }
            public string ReportType { get; set; }
            public string ProcName { get; set; }
            public string CartId { get; set; }
            public string Category { get; set; }
            public string from_cart { get; set; }
            public string to_cart { get; set; }
            public string ItemId { get; set; }
            public string vendorID { get; set; }
        }
        public class IdentBO : ipWarehouse
        {
            public int autoId { get; set; }
            public string hosp_id { get; set; }
            public string CartId { get; set; }
            public string indent_no { get; set; }
            public string order_by { get; set; }
            public string order_qty { get; set; }
            public string order_date { get; set; }
            public string item_id { get; set; }
            public string from { get; set; }
            public string to { get; set; }
            public string verifyqty { get; set; }
            public string veify_by { get; set; }
            public string veifyremark { get; set; }
            public string user_remark { get; set; }

        }

        public class IndentReplacement : ipWarehouse
        {
            public string CartId { get; set; }
            public string Indent_no { get; set; }
            public string Item_id { get; set; }
            public string verifyQty { get; set; }
            public string remark { get; set; }
            public string NewItemId { get; set; }
            public string loginid { get; set; }
            public string Logic { get; set; }
        }
        public class ManufacturerBO : ipWarehouse
        {
            public int auto_id { get; set; }
            public string hosp_id { get; set; }
            public string mfd_id { get; set; }
            public string vendor_id { get; set; }
            public string MainCategory { get; set; }
            public string mfd_name { get; set; }
            public string contact_no { get; set; }
            public string address { get; set; }
            public int country_id { get; set; }
            public int state_id { get; set; }
            public int city_id { get; set; }
            public string remark { get; set; }            
            public string status { get; set; }
        }
        public class CategoryBO : ipWarehouse
        {
            public int auto_id { get; set; }
            public string hosp_id { get; set; }
            public string MainCategory { get; set; }
            public string CategoryId { get; set; }
            public string CategoryName { get; set; }
        }
        public class PurchaseQueriesBO : ipWarehouse
        {
            public string purchId { get; set; }
            public string VendorCode { get; set; }
            public string item_name { get; set; }
            public DateTime? from { get; set; }
            public DateTime? to { get; set; }
        }
        public class ipUploadPDF
        {
            public string FileName;
            public string FileByteArray;
            public string Logic { get; set; }
        }

        public class ipDocumentInfo
        {
            public string HospitalId { get; set; }
            public string TranId { get; set; }
            public string TrnType { get; set; }
            public string ImageName { get; set; }
            public string ImageType { get; set; }
            public string login_id { get; set; }
        }
        public class GoodsReturnQueriesBO : ipWarehouse
        {
            public string CartId { get; set; }
            public string item_id { get; set; }
            public string vendor_id { get; set; }
            public string vendor_type { get; set; }
            public string return_id { get; set; }
            public string unit_id { get; set; }
            public string Vendor_Code { get; set; }
            public string Item_id { get; set; }
            public string From { get; set; }
            public string To { get; set; }
            public string Prm1 { get; set; }
            public string master_key_id { get; set; }
            public int Quantity { get; set; }
            public string PurchReturnId { get; set; }
        }
        public class DiascountReprocessBO : ipWarehouse
        {
            public string purch_id { get; set; }
            public string discount_amount { get; set; }
        }
        public class PackTypeBO : ipWarehouse
        {
            public int auto_id { get; set; }
            public string hosp_id { get; set; }
            public int autoid { get; set; }
            public string pack_type { get; set; }
            public string pack_qty { get; set; }
        }
        public class VendorMasterBO : ipWarehouse
        {
            public int auto_id { get; set; }
            public string vendor_id { get; set; }
            public string hosp_id { get; set; }
            public string vendor_name { get; set; }
            public string contact_person { get; set; }
            public string address1 { get; set; }
            public string address2 { get; set; }
            public string address3 { get; set; }
            public string city { get; set; }
            public string state { get; set; }
            public string country { get; set; }
            public string pin { get; set; }
            public string ledgerid { get; set; }
            public string contact_no { get; set; }
            public string email { get; set; }
            public string payment_mode { get; set; }
            public int payment_days { get; set; }
            public string gst_no { get; set; }
            public string drug_lic_no { get; set; }
            public string notes { get; set; }
            public string active_flag { get; set; }
        }
        public class ItemMasterBO : ipWarehouse
        {
            public int auto_id { get; set; }
            public string hosp_id { get; set; }
            public string hsn { get; set; }
            public string MfdId { get; set; }
            public string MfdName { get; set; }
            public string ItemType { get; set; }
            public string ItemId { get; set; }
            public string ItemName { get; set; }
            public int pack_id { get; set; }
            public string Category { get; set; }
            public string CategoryId { get; set; }
            public int rol { get; set; }
            public int MOQ { get; set; }
            public string Remark { get; set; }
            public string ShelfNo { get; set; }
            public string StatusFlag { get; set; }
            public string purchase_flag { get; set; }
        }

        public class WhDepartmentBO : ipWarehouse
        {
            public int autoId { get; set; }
            public string hosp_id { get; set; }
            public string DeptName { get; set; }
            public string DeptCode { get; set; }
        }
        public class VendorApprove : ipWarehouse
        {
            public string ledgerID { get; set; }
            public string vendor_id { get; set; }
            public string vendor_name { get; set; }
        }
        public class PurchaseBO : ipWarehouse
        {
            public int Auto_Id { get; set; }
            public string unit_id { get; set; }
            public string comp_code { get; set; }
            public string poNumber { get; set; }
            public string pur_type { get; set; }
            public string Vendor_Code { get; set; }
            public string Inv_No { get; set; }
            public DateTime Inv_Date { get; set; }
            public string eWayBillNo { get; set; }
            public string NatureOfPurchase { get; set; }
            public DateTime? eWayBillDate { get; set; }
            public string Item_id { get; set; }
            public string barcodeNo { get; set; }
            public string hsn { get; set; }
            public string mfd_id { get; set; }
            public string Batch_No { get; set; }
            public string Exp_Date { get; set; }
            public string pack_type { get; set; }
            public int pack_qty { get; set; }
            public int qty { get; set; }
            public decimal MRP { get; set; }
            public decimal trade { get; set; }
            public decimal Quantity { get; set; }
            public decimal It_Free { get; set; }
            public decimal DisPer { get; set; }
            public int Tax_id { get; set; }
            public decimal BestRate { get; set; }
            public decimal plusminus { get; set; }
            public decimal adj_amt { get; set; }
            public string credit_cash { get; set; }
            public string Remark { get; set; }
            public string CreatedBy { get; set; }
            public string Purch_id { get; set; }
        }
        public class WhCartBO : ipWarehouse
        {
            public int autoId { get; set; }
            public string hosp_id { get; set; }
            public string DeptId { get; set; }
            public string CartType { get; set; }
            public string CartName { get; set; }
        }

        public class WhEmpAssignEmpToCart : ipWarehouse
        {
            public int autoId { get; set; }
            public string hosp_id { get; set; }
            public string EmpCode { get; set; }
            public string CartIds { get; set; }
        }


        public class IdentStockBO : ipWarehouse
        {
            public string hosp_id { get; set; }
            public string CartId { get; set; }
            public string item_id { get; set; }
        }
        public class IdentVerifyBO : ipWarehouse
        {
            public string Indent_no { get; set; }
            public string item_id { get; set; }
            public int verify_qty { get; set; }
            public string verify_remark { get; set; }
            public string VeriftDataTable { get; set; }
        }
        public class GeneralStoreQueries : ipWarehouse
        {
            public string unit_id { get; set; }
            public string TransferId { get; set; }
            public string WHLogic { get; set; }

            public DateTime from { get; set; }
            public DateTime to { get; set; }
        }
        public class TransferQueries : ipWarehouse
        {
            public string unit_id { get; set; }
            public string TransferId { get; set; }
            public string item_id { get; set; }
            public DateTime from { get; set; }
            public DateTime to { get; set; }
            public string WHLogic { get; set; }
        }
        public class DispatchOrderBo : ipWarehouse
        {
            public string auto_id { get; set; }
            public string fromIndent { get; set; }
            public string FromDate { get; set; }
            public string CartId { get; set; }
            public string ToDate { get; set; }
            public string lot_no { get; set; }
            public string item_id { get; set; }

            public string master_key_id { get; set; }
            public string qty { get; set; }
            public string trf_to { get; set; }
            public string mfd_id { get; set; }
            public string comp_code { get; set; }
            public string trf_from { get; set; }
            public string indent_no { get; set; }
            public string indent_autoId { get; set; }
        }
        public class ConsumptionBO : ipWarehouse
        {
            public string hosp_id { get; set; }
            public string CartId { get; set; }
            public string item_id { get; set; }
            public string from { get; set; }
            public string to { get; set; }
            public string prm_2 { get; set; }
            public string transid { get; set; }
            public string transtype { get; set; }
            public string compid { get; set; }
            public string frmcart { get; set; }
            public string IssueTo { get; set; }
            
            public string tocart { get; set; }
            public string masterkeyid { get; set; }
            public string qty { get; set; }
            public int StockQuantity { get; set; }
            public string remark { get; set; }
        }
        public class ipPatientConsume
        {
            public string HospId { get; set; }
            public string pt_name { get; set; }
            public string ipop_no { get; set; }
            public string gen_from { get; set; }
            public string dept_name { get; set; }
            public string room_no { get; set; }
            public string from_cart { get; set; }
            public string bed_no { get; set; }
            public string doctor_id { get; set; }
            public string doctor_name { get; set; }
            public string item_id { get; set; }
            public string master_key_id { get; set; }
            public string item_name { get; set; }
            public string qty { get; set; }
            public string login_id { get; set; }
            public string UHID { get; set; }
            public string pay_type { get; set; }
            public string PanelId { get; set; }
            public string panel_name { get; set; }
            public string ReqBy { get; set; }
            public string ReqType { get; set; }
            public string ProductSaleType { get; set; }
        }

        public class ConsumeStockByPatient
        {
            public ConsumeStockBO objConsume { get; set; }
            public List<ConsumptionBO> objStock { get; set; }
        }
        public class ConsumeStockBO : ipWarehouse
        {
            public string UHID { get; set; }
            public string IPDNo { get; set; }
            public string patientName { get; set; }
            public string hosp_id { get; set; }
        }
        public class AuditBO : ipWarehouse
        {
            public string DeptId { get; set; }
            public string CartId { get; set; }
            public string ItemId { get; set; }
            public string MasterKeyId { get; set; }
            public string AuditNo { get; set; }
        }
        public class AuditMasterBO : ipWarehouse
        {
            public string CartId { get; set; }
            public string audit_no { get; set; }
            public string audit_remark { get; set; }
            public string IsOpen { get; set; }
        }

        public class AuditInfoBO : ipWarehouse
        {
            public int HospId { get; set; }
            public int AutoId { get; set; }
            public string CartId { get; set; }
            public string ItemId { get; set; }
            public string MasterKeyId { get; set; }
            public string AuditType { get; set; }
            public string AuditNo { get; set; }
            public string batchNo { get; set; }
            public DateTime exp_date { get; set; }
            public decimal mrp { get; set; }
            public Int64 CartStock { get; set; }
            
            public Int64 Qty { get; set; }
        }
    }
}