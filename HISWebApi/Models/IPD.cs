using System;
using System.Collections.Generic;
using System.Data;

namespace HISWebApi.Models
{
    public class IPDBO
    {


        public class ipDashBoard
        {
            public string unit_id { get; set; }
            public string LoginId { get; set; }
            public string Password { get; set; }
            public string from { get; set; }
            public string to { get; set; }
            public string Prm1 { get; set; }
            public string Prm2 { get; set; }
            public string Prm3 { get; set; }
            public string login_id { get; set; }
            public string Logic { get; set; }
        }
        public class SafetyRoundInfo : ipDashBoard
        {
            public string IPDNo { get; set; }
            public string PatientName { get; set; }
            public string Age { get; set; }
            public string AgeType { get; set; }
            public string Gender { get; set; }
            public string AdmittedDoctorId { get; set; }
            public string AdmittDoctorName { get; set; }
            public string FloorName { get; set; }

            public string BedNo { get; set; }
            public string CheckupType { get; set; }
            public string CheckupStatus { get; set; }
            public int QuestId { get; set; }
            public string Remark { get; set; }
            public string attendantName { get; set; }
        }
        public class RoomShiftInfo : ipDashBoard
        {
            public string IPDNo { get; set; }
            public string DoctorId { get; set; }
            public string RoomBedId { get; set; }
            public string RoomBillingCategory { get; set; }
            public string RoomChangeDateTime { get; set; }
        }
        public class BloodRequisition {
            public BloodRequisitionInfo bloodRequisitionInfo { get; set; }
            public List<ComponentInfo> componentInfo { get; set; }
        }
        public class BloodRequisitionInfo : ipDashBoard
        {
            public string UHID { get; set; }
            public string IPOPNo { get; set; }
            public string PatientName { get; set; }
            public string IPOPType { get; set; }
            public string IndentNo { get; set; }
            public string ItemId { get; set; }
            public string ComponentId { get; set; }
            public int IndentQty { get; set; }
            public string IndentBy { get; set; }
            public string TnxId { get; set; }
            public string BloodGroupId { get; set; }
            public string BloodTestedBy { get; set; }
            public DateTime BloodTestedDate { get; set; }
            public string Diagnosis { get; set; }
            public string TransfusionIndicator { get; set; }
            public string HbPerc { get; set; }
            public char PreviousTransfusion { get; set; }
            public string DonorNo { get; set; }
            public string AboRH { get; set; }
            public DateTime dateTransfuse { get; set; }
            public string ReactionAny { get; set; }
            public char IsPregnant { get; set; }
            public char haemolytic_disease { get; set; }
            public char miscarriage { get; set; }
            public string ReqType { get; set; }
            public string ReqType_Remark1 { get; set; }
            public string ReqType_Remark2 { get; set; }
            public string ReqByDoctorId { get; set; }
        }
        public class ComponentInfo
        {
            public string ComponentID { get; set; }
            public string ComponentName { get; set; }
            public int Qty { get; set; }
        }
        public class IPDInfo : ipDashBoard
        {
            public string hosp_id { get; set; }
            public string UHID { get; set; }
            public string IPDNo { get; set; }
            public string Floor { get; set; }
            public string PanelId { get; set; }
            public string DoctorId { get; set; }
            public string CPT_Code { get; set; }
            public string RoomBillingCategory { get; set; }
            public string SearchType { get; set; }
            public string ItemIds { get; set; }
            public int Qty { get; set; }
        }
        public class DischargeReportInfo : ipDashBoard
        {
            public string UHID { get; set; }
            public string IPDNo { get; set; }
            public string TemplateId { get; set; }
            public string TemplateName { get; set; }
            public string TemplateContent { get; set; }
        }
        public class DischargeTemplateInfo : ipDashBoard
        {
            public int AutoId { get; set; }
            public string UHID { get; set; }
            public string IPDNo { get; set; }
            public string HeaderId { get; set; }
            public string HeaderName { get; set; }
            public string TemplateId { get; set; }
            public string TemplateName { get; set; }
        }
        public class ipTPApproval
        {
            public int AutoId { get; set; }
            public string IPDNo { get; set; }
            public string ApprovalDate { get; set; }
            public string ApprovalType { get; set; }
            public decimal ApprovalAmount { get; set; }
            public decimal CoPayAmount { get; set; }
            public decimal Discount { get; set; }
            public string ClaimNo { get; set; }
            public string Remark { get; set; }
            public string login_id { get; set; }
            public string FileExtension { get; set; }
            public string Logic { get; set; }
        }
        public class BBIndentInfo : ipDashBoard
        {
            public string hosp_id { get; set; }
            public string IPOPNo { get; set; }
            public string IPOPType { get; set; }
            public string IndentNo { get; set; }
            public List<BBComponentInfo> componentInfo { get; set; }
        }
        public class BBComponentInfo
        {
            public string ItemId { get; set; }
            public string ComponentID { get; set; }
            public int IndentQty { get; set; }
        }
        public class DischargeStatusInfo : ipDashBoard
        {
            public string BillNo { get; set; }
            public string IPDNo { get; set; }
            public DateTime discharge_Init_date { get; set; }
            public DateTime DischargeDate { get; set; }
            public DateTime DeathDateTime { get; set; }
            public string InitCallBy { get; set; }
            public string DischargeType { get; set; }
            public string BillingType { get; set; }
            public string Remark { get; set; }
            public string CauseOfDeath { get; set; }
            public string TypeOfDeath { get; set; }
            public decimal ApprAmount { get; set; }
            public decimal CoPayAmount { get; set; }
            public decimal DiscountAmount { get; set; }
        }
        public class FeedbackInfo : ipDashBoard
        {
            public string IPDNo { get; set; }
            public string PatientName { get; set; }
            public string Age { get; set; }
            public string AgeType { get; set; }
            public string Gender { get; set; }
            public string AdmittedDoctorId { get; set; }
            public string AdmittDoctorName { get; set; }
            public string FloorName { get; set; }
            public string AdmitDate { get; set; }
            public string OverallFeedback { get; set; }
            public decimal OverallRating { get; set; }
            public char IsCompleted { get; set; }
            public string FeedbackType { get; set; }
            public int QuestId { get; set; }
            public string Feedback { get; set; }
            public decimal Rating { get; set; }
            public string Qno { get; set; }
            public string QInfo { get; set; }
            public string NurseId { get; set; }
        }
        public class ipOverDueAmount
        {
            public string IPDNo { get; set; }
            public string LoginId { get; set; }
            public string LoginName { get; set; }
            public string Logic { get; set; }
            public string from { get; set; }
            public string to { get; set; }
            public string Prm1 { get; set; }
            public string Remark { get; set; }
            public decimal MinAlertAmount { get; set; }
            public int RepeatCallMinute { get; set; }

            public string OutPutType { get; set; }
        }
        public class KitInfoBO
        {
            public string unit_id { get; set; }
            public string Kit_id { get; set; }
            public string kit_name { get; set; }
            public string item_id { get; set; }
            public int qty { get; set; }
            public string login_id { get; set; }
            public char isActive { get; set; }
            public string Logic { get; set; }

        }
        public class MedicineBO
        {
            public string searchKey { get; set; }
            public string searchType { get; set; }
            public string PanelId { get; set; }
        }
        public class IPOPIndentBO
        {
            public string HospId { get; set; }
            public string pt_name { get; set; }
            public string ipop_no { get; set; }
            public string gen_from { get; set; }
            public string dept_name { get; set; }
            public string room_no { get; set; }
            public string bed_no { get; set; }
            public string doctor_id { get; set; }
            public string doctor_name { get; set; }
            public string item_id { get; set; }
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
        public class SalesBillInfo
        {
            public string unit_id { get; set; }
            public string Bill_No { get; set; }
            public string login_id { get; set; }
            public string login_name { get; set; }
        }
        public class dataSetPharmacy
        {
            public string Msg { get; set; }
            public DataSet result { get; set; }
        }
        public class ipIndentReport
        {
            public string HospId { get; set; }
            public string IPOPNoList { get; set; }
            public string indent_no { get; set; }
            public string prm_1 { get; set; }
            public string prm_2 { get; set; }
            public string login_id { get; set; }
            public string Logic { get; set; }

        }
        public class ipFeedBack
        {
            public string IPDOPNo { get; set; }
            public string OutPutType { get; set; }
            public string prm_1 { get; set; }
            public string prm_2 { get; set; }
            public string from { get; set; }
            public string to { get; set; }
            public string empCode { get; set; }
            public string Logic { get; set; }
        }
        public class ipIPDAudit
        {
            public string IPDNo { get; set; }
            public string OutPutType { get; set; }
            public string prm_1 { get; set; }
            public string prm_2 { get; set; }
            public string from { get; set; }
            public string to { get; set; }
            public string empCode { get; set; }
            public string Logic { get; set; }
        }
        public class ipMISReport
        {
            public string DoctorId { get; set; }
            public string tnxType { get; set; }
            public string from { get; set; }
            public string to { get; set; }
            public string CatId { get; set; }
            public string SubCatId { get; set; }
            public string OutPutType { get; set; }
            public string Logic { get; set; }

        }
        public class ipHandoverInfo : ipDashBoard
        {
            public string IpdNo { get; set; }
            public string PatientName { get; set; }
            public string Age { get; set; }
            public string Gender { get; set; }
            public string DoctorId { get; set; }
            public string DoctorName { get; set; }
            public string AdmitDate { get; set; }
            public string FloorName { get; set; }
            public string StaffCode { get; set; }
            public string ShiftName { get; set; }
            public string prm_1 { get; set; }
            public string prm_2 { get; set; }
            public string prm_3 { get; set; }
        }
    }
}