using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HISWebApi.Models
{
	public class ipCommission
	{
		public string from { get; set; }
		public string to { get; set; }
		public string prm_1 { get; set; }
		public string IPDNo { get; set; }
		public string RefCode { get; set; }
        public string commisionPerc { get; set; }
        public string prm_2 { get; set; }
		public string login_id { get; set; }
        public string ReportType { get; set; }
        public string Logic { get; set; }
	}
	public class ReferralCommissionBO: ipCommission
	{
		public string IPDNo { get; set; }
		public string ref_code { get; set; }
		public string patient_name { get; set; }
		public string gender { get; set; }
		public decimal commission_Perc { get; set; }
		public decimal commission_amount { get; set; }
		public string trn_type { get; set; }

	}
}