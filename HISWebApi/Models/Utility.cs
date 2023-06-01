using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HISWebApi.Models
{
    public class ipUploadPDF
    {
        public string FileName;
        public string FileByteArray;
        public string Logic { get; set; }
    }
    public class SubmitStatus
    {
        public int Status { get; set; }
        public string Message { get; set; }
        public string AppintmentId { get; set; }
        public string purchId { get; set; }
        public string virtual_path { get; set; }
    }
    public class ipPanelRates
    {
        public int PanelId { get; set; }
        public string RateListId { get; set; }
        public string ItemID { get; set; }
        public string Rate { get; set; }

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
    public class HealthCardInfo
    {
        public string labcode { get; set; }
        public string card_no { get; set; }
        public string member_id { get; set; }
        public string m_type { get; set; }
        public string cust_name { get; set; }
        public string gender { get; set; }
        public string dob { get; set; }
        public string mobileno { get; set; }
        public string UHID { get; set; }
        public string area { get; set; }
        public string Locality { get; set; }
        public string district { get; set; }
        public string state { get; set; }
        public string email { get; set; }
        public string pin { get; set; }
        public string prm_1 { get; set; }
        public string logic { get; set; }
        public string login_id { get; set; }
    }
    public class CouponQueries : ipOnlineAppQry
    {
        public string unit_id { get; set; }
        public string CouponType { get; set; }
        public string TokenNo { get; set; }
        public string BookingId { get; set; }
        public string prm_1 { get; set; }
        public string login_id { get; set; }
        public string LabCode { get; set; }
        public string mobileNo { get; set; }
        public string Logic { get; set; }

    }
    public class CouponLogBO : ipOnlineAppQry
    {
        public string labcode { get; set; }
        public string Mobile { get; set; }
        public string PatientName { get; set; }
        public string CouponCode { get; set; }
    }
}