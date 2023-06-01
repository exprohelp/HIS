using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HISWebApi.Models
{
    public class ipAccounts
    {
        public string UnitId { get; set; }
        public string VoucherNo { get; set; }
        public string ledgerId { get; set; }
        public string ledgerName { get; set; }
        public string VchType { get; set; }
        public string Division { get; set; }
        public string Prm1 { get; set; }
        public string From { get; set; }
        public string To { get; set; }
        public string Logic { get; set; }
        public string LoginId { get; set; }
        public string ReportType { get; set; }
    }
    //public class VoucherDataVertical
    //{
    //    public string vchDate { get; set; }
    //    public string vchType { get; set; }
    //    public string VoucherNo { get; set; }
    //    public string billNo { get; set; }
    //    public string billType { get; set; }
    //    public string Narration { get; set; }
    //    public string Company { get; set; }
    //    public string ledgername { get; set; }
    //    public decimal Amount { get; set; }
    //    public int SeqNo { get; set; }
    //}
    public class LedgerInfo
    {
        public string Ledger_Id { get; set; }
        public string Ledger_Name { get; set; }
    }

    public class PayToPayByInfo
    {
        public string Code { get; set; }
        public string Name { get; set; }
    }
    public class PaymentInfo:ipAccounts
    {
        public string VchDate { get; set; }
        public string PayTo_Code { get; set; }
        public string PayBy_Code { get; set; }
        public string Narration { get; set; }
        public string DrCrAmount { get; set; }
        public string GenFrom { get; set; }

    }
    public class LedgerBookInfo
    {
        public string Seq_No { get; set; }
        public string vch_date { get; set; }
        public string voucher_no { get; set; }
        public string vch_type { get; set; }
        public string particular { get; set; }
        public string db_Amount { get; set; }
        public string cb_amount { get; set; }
        public string narration { get; set; }
        public string GenFrom { get; set; }

    }
	public class LedgerMasterBO: ipAccounts
	{
		public string ledgerID { get; set; }
		public string unit_id { get; set; }	
		public string ledger_name { get; set; }	
		public int subGroupID { get; set; }	
		public string contact_person { get; set; }	
		public string externalName { get; set; }	
		public string party_address { get; set; }	
		public Decimal openAmt { get; set; }	
		public string panNo { get; set; }	
		public string gst_no { get; set; }	
	}
	public class GroupMasterBO: ipAccounts
	{
		public string groupID { get; set; }
		public string display_name { get; set; }
		public string under_GroupID { get; set; }
		public string prm_1 { get; set; }
	}

}
