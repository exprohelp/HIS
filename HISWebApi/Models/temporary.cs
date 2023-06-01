using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HISWebApi.Models
{
	public class TempReferralBO
	{
        public int AutiId { get; set; }
        public int autoid { get; set; }
        public string hospid { get; set; }
        public string ref_code { get; set; }
        public string BookletNo { get; set; }
        public string CouponNo { get; set; }
        public string UHID { get; set; }
        public DateTime from { get; set; }
        public DateTime to { get; set; }
        public string fromdate { get; set; }
        public string todate { get; set; }
        public string PatientName { get; set; }
        public string PatientId { get; set; }
        public string DoctorId { get; set; }
        public string Mobile { get; set; }
        public string VirPath { get; set; }
        public string PhyPath { get; set; }
        public string login_id { get; set; }
        public string prm1 { get; set; }
        public string prm2 { get; set; }
        public string Logic { get; set; }
    }
}