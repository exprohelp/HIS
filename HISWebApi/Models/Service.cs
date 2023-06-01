using System;
using System.Collections.Generic;

namespace HISWebApi.Models
{
    public class ipService
    {
        public string AppointmentId { get; set; }
        public string hosp_id { get; set; }
        public string prm_1 { get; set; }
        public string prm_2 { get; set; }
        public string from { get; set; }
        public string CatId { get; set; }
        public string SubCatId { get; set; }
        public string RateListId { get; set; }
        public DateTime cr_date { get; set; }
        public string to { get; set; }
        public bool IsActive { get; set; }
        public string login_id { get; set; }
        public string Logic { get; set; }
    }
    public class ServiceBO : ipAppointment
    {
        public string visit_purpose { get; set; }
        public string SourceName { get; set; }
        public string state { get; set; }
        public string c_city { get; set; }
        public string c_address { get; set; }
        public string mobile_no { get; set; }
        public string prm_1 { get; set; }
        public string degree { get; set; }
        public string ref_name { get; set; }
        public string ref_code { get; set; }
    }
    public class ServiceQueries : ipService
    {
        public string SearcKey { get; set; }
        public string SearchValue { get; set; }
        public string Hosp_Id { get; set; }
        public string UHID { get; set; }
        public string DoctorId { get; set; }
        public string DeptId { get; set; }
        public string PanelId { get; set; }
        public string CouponCode { get; set; }
        public string ItemId { get; set; }
        public string logic { get; set; }
        public string CPT_Code { get; set; }
        public string SearchType { get; set; }
    }
    public class ServiceRefund : PatientMasterBO
    {
        public string VisitNo { get; set; }
        public string CancelReason { get; set; }
        public string ItemIdList { get; set; }
        public decimal amount { get; set; }
        public string PayMode { get; set; }
        public string IPAddress { get; set; }
    }
    public class ServiceItems : ipService
    {
        public int AutoId { get; set; }
        public string TnxId { get; set; }
        public string RateListName { get; set; }
        public string ItemId { get; set; }
        public string ItemSection { get; set; }
        public string CatId { get; set; }
        public int IsPackage { get; set; }
        public decimal mrp_rate { get; set; }
        public decimal Rate { get; set; }
        public decimal panel_discount { get; set; }
        public decimal panel_rate { get; set; }
        public char IsRateEditable { get; set; }
        public char IsPatientPayable { get; set; }
        public char IsDiscountable { get; set; }
        public int qty { get; set; }
        public decimal adl_disc_amount { get; set; }
        public decimal adl_disc_perc { get; set; }
        public decimal net_amount { get; set; }
        public char IsUrgent { get; set; }
        public string Remark { get; set; }
    }

    public class serviceBooking
    {
        public PatientMasterBO objPatient { get; set; }
        public ServiceBookingBO objBooking { get; set; }
        public List<ServiceItems> objRateList { get; set; }
        public List<PaymentBO> objPayment { get; set; }
    }
    public class ServiceBookingBO : PatientMasterBO
    {
        public string IsNewPatient { get; set; }
        public string DoctorId { get; set; }
        public string AppDate { get; set; }
        public string IPDNo { get; set; }
        public string AppInTime { get; set; }
        public string AppOutTime { get; set; }
        public string ItemId { get; set; }
        public decimal Surgery_amt { get; set; }
        public decimal Surgeon_amt { get; set; }
        public string visitType { get; set; }
        public string visitSource { get; set; }
        public string visit_purpose { get; set; }
        public string GrossAmount { get; set; }
        public string discount { get; set; }
        public string discount_remark { get; set; }
        public string discountType { get; set; }
        public string discountBy { get; set; }
        public string IDProofType { get; set; }
        public string IDProofNO { get; set; }
        public int ageInDays { get; set; }
        public decimal coPayAmount { get; set; }
        public string ipAddress { get; set; }
        public string refDoctorCode { get; set; }
        public string PanelId { get; set; }
        public string RateList_Id { get; set; }
        public bool IsConfirmed { get; set; }
        public string GenFrom { get; set; }
        public string online_app_no { get; set; }
        public string BillingRole { get; set; }
    }
}