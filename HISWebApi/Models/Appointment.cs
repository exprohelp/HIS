using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HISWebApi.Models
{
    public class ipAppointment
    {
        public string AppointmentId { get; set; }
        public string hosp_id { get; set; }
        public string prm_1 { get; set; }
        public string from { get; set; }
        public DateTime cr_date { get; set; }
        public string to { get; set; }
        public bool IsActive { get; set; }
        public string login_id { get; set; }
        public string Logic { get; set; }
        public string DoctorId { get; set; }
        public string DeptId { get; set; }
        public string prm_2 { get; set; }
        public string CatId { get; set; }
        public string SubCatId { get; set; }
        public string RateListId { get; set; }
    }
    public class AppointmentBO : ipAppointment
    {
        public string visit_purpose { get; set; }
        public string SourceName { get; set; }
        public string state { get; set; }
        public string c_city { get; set; }
        public string c_address { get; set; }
        public string mobile_no { get; set; }
        public string degree { get; set; }
        public string ref_name { get; set; }
        public string ref_code { get; set; }
    }
    public class ReferralSearch : ipAppointment
    {
        public string referral_id { get; set; }
        public string referral_name { get; set; }
    }
    public class AppointmentQueries : ipAppointment
    {
        public string Hosp_Id { get; set; }
        public string DoctorId { get; set; }
        public string DeptId { get; set; }
        public string PanelId { get; set; }
        public string ItemId { get; set; }
        public string SearcKey { get; set; }
        public string SearchValue { get; set; }
        public string UHID { get; set; }
        public string CouponCode { get; set; }
        public string logic { get; set; }
        public string CPT_Code { get; set; }
        public string SearchType { get; set; }


    }
    public class PatientMasterQueries : ipAppointment
    {
        public string UHID { get; set; }
        public string MobileNo { get; set; }
        public string SearcKey { get; set; }
        public string SearchValue { get; set; }
    }
    public class PaymentBO : ipAppointment
    {
        public string ReceiptNo { get; set; }
        public string PayMode { get; set; }
        public string CardNo { get; set; }
        public string BankName { get; set; }
        public string RefNo { get; set; }
        public string MachineId { get; set; }
        public string MachineName { get; set; }
        public string Amount { get; set; }
        public string OnlPaymentId { get; set; }
        public string OnlPayStatus { get; set; }
        public string OnlPayResponse { get; set; }
        public DateTime OnlPaymentDate { get; set; }
    }

    public class OpdBooking
    {
        public string ImageName { get; set; }
        public string photo_path { get; set; }
        public string Base64String { get; set; }
        public string extension { get; set; }
        public PatientMasterBO objPatient { get; set; }
        public AppointmentBookingBO objBooking { get; set; }
        public List<PaymentBO> objPayment { get; set; }
   
    }
    public class AppointmentBookingBO : PatientMasterBO
    {
        public string IsNewPatient { get; set; }
        public string DoctorId { get; set; }
        public string AppDate { get; set; }
        public string AppInTime { get; set; }
        public string AppOutTime { get; set; }
        public string ItemId { get; set; }
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
    }
    public class PatientMasterBO : ipAppointment
    {
        public string barcodeno { get; set; }
        public string UHID { get; set; }
        public string Title { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string patient_name { get; set; }
        public string email { get; set; }
        public string gender { get; set; }
        public string dob { get; set; }
        public int age { get; set; }
        public string ageType { get; set; }
        public string mobile_no { get; set; }
        public string email_id { get; set; }
        public string marital_status { get; set; }
        public string religion { get; set; }
        public string relation_of { get; set; }
        public string relation_name { get; set; }
        public string relation_phone { get; set; }
        public string country { get; set; }
        public string state { get; set; }
        public string district { get; set; }
        public string locality { get; set; }
        public string village { get; set; }
        public string address { get; set; }
        public string idType { get; set; }
        public string IDNo { get; set; }
        public string CardNo { get; set; }
        public string member_id { get; set; }
        public string photo_path { get; set; }
        public string IPNo { get; set; }
    }
    public class IPDTakeAdvance
    {
        public IPDRegistrationBO objBooking { get; set; }
        public List<PaymentBO> objPayment { get; set; }
    }
    public class IPDRegistration
    {
        public PatientMasterBO objPatient { get; set; }
        public IPDRegistrationBO objBooking { get; set; }
    }
    public class IPDRegistrationBO : PatientMasterBO
    {
        public string IsNewPatient { get; set; }
        public string DoctorId { get; set; }
        public string AppDate { get; set; }
        public string AppInTime { get; set; }
        public string AppOutTime { get; set; }
        public string ItemId { get; set; }
        public string visitType { get; set; }
        public string visitSource { get; set; }
        public string visit_purpose { get; set; }
        public string GrossAmount { get; set; }
        public string discount { get; set; }
        public string discount_remark { get; set; }
        public string discountType { get; set; }
        public string discountBy { get; set; }
        public string IPOPType { get; set; }
        public string ReceiptType { get; set; }
        public string IDProofType { get; set; }
        public string IDProofNO { get; set; }
        public string IPDNo { get; set; }
        public string AdmitDate { get; set; }
        public string AdmitTime { get; set; }
        public string AdmissionType { get; set; }
        public string RoomType { get; set; }
        public string RoomBedId { get; set; }
        public string roomFullName { get; set; }
        public string BedNo { get; set; }
        public string RoomBillingCategory { get; set; }
        public string RoomTypeRequest { get; set; }
        public string MLCType { get; set; }
        public string MLCNo { get; set; }
        public string StaffCode { get; set; }
        public string PatientType { get; set; }
        public string PolicyNo { get; set; }
        public string ReferencNo { get; set; }
        public string SourceName { get; set; }
        public string Remark { get; set; }
        public string RefBy { get; set; }
        public string RefName { get; set; }
        public string EntrySource { get; set; }
        public string Prm1 { get; set; }
        public string Prm2 { get; set; }
        public string AttendantName { get; set; }
        public string AttendantContactNo1 { get; set; }
        public string AttendantContactNo2 { get; set; }
        public int ageInDays { get; set; }
        public decimal coPayAmount { get; set; }
        public string ipAddress { get; set; }
        public string refDoctorCode { get; set; }
        public string PanelId { get; set; }
        public string RateList_Id { get; set; }
        public bool IsConfirmed { get; set; }
        public string GenFrom { get; set; }
        public string online_app_no { get; set; }
    }
    public class AppointmentCancellationBO : PatientMasterBO
    {
        public string oldTnxId { get; set; }
        public string CancelReason { get; set; }
        public string ItemIdList { get; set; }
        public decimal amount { get; set; }
        public string PayMode { get; set; }
        public string IPAddress { get; set; }
    }
    public class AppointmentReschedule : PatientMasterBO
    {
        public string DoctorId { get; set; }
        public string app_no { get; set; }
        public DateTime AppDate { get; set; }
        public string AppInTime { get; set; }
        public string AppOutTime { get; set; }
        public string Prm_1 { get; set; }
        public string Prm_2 { get; set; }
        public string PanelId { get; set; }
        public string result { get; set; }
    }
    public class OpdInOutMarking : ipAppointment
    {
        public string DoctorId { get; set; }
        public string BookingNo { get; set; }
        public string inputDate { get; set; }
        public string Prm1 { get; set; }
        public string LoginId { get; set; }
    }
    public class DoctorAppointmentInfo : ipAppointment
    {
        public string CardNo { get; set; }
        public string AppointmentId { get; set; }
        public string UHID { get; set; }
        public string MobileNo { get; set; }
        public string EmailId { get; set; }
        public string PatientName { get; set; }
        public string Gender { get; set; }
        public int Age { get; set; }
        public string dob { get; set; }
        public string AgeType { get; set; }
        public string state { get; set; }
        public string City { get; set; }
        public string Locality { get; set; }
        public string PtAddress { get; set; }
        public string AppointmentReason { get; set; }
        public string UnitId { get; set; }
        public string From { get; set; }
        public string To { get; set; }
        public string Prm1 { get; set; }

    }
}