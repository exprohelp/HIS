using System;
using System.Data;

namespace HISWebApi.Models
{
    public class pmMasterLogic
    {
        public Nullable<int> ID { get; set; }
        public string Logic { get; set; }
        public string Result { get; set; }
        public string app_no { get; set; }
        public bool Flag { get; set; }
        public bool IsActive { get; set; }
        public string login_id { get; set; }
        public bool IsFavourite { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string warehouseCartId { get; set; }
        public string prm_1 { get; set; }
        public string prm_2 { get; set; }
        public string Tagname { get; set; }
    }
    public class MasterRole : pmMasterLogic
    {
        public string RoleId { get; set; }
        public string RoleName { get; set; }       
    }
    public class dataSet
    {
        public string Msg { get; set; }
        public DataSet ResultSet { get; set; }      
    }

    public class MasterMainMenu : pmMasterLogic
    {
        public string MenuId { get; set; }
        public string RoleId { get; set; }
        public string MenuName { get; set; }
    }
    public class MasterSubMenu : pmMasterLogic
    {
        public int AutoId { get; set; }
        public string SubMenuId { get; set; }
        public string SubMenuName { get; set; }
        public string SubMenuUrl { get; set; }
        public string MenuId { get; set; }
        public string MenuName { get; set; }
        public bool DisableFlag { get; set; }
        public string Loginid { get; set; }
        public int SequenceNo { get; set; }
        public string UsedFor { get; set; }
        public int MenuSequence { get; set; }   
    }
    public class MasterEmployee : pmMasterLogic
    {
        public int emp_id { get; set; }
        public string EmployeeCode { get; set; }      
        public string Title { get; set; }
        public string EmployeeName { get; set; }
        public string Gender { get; set; }
        public string MaritalStatus { get; set; }
        public DateTime DOB { get; set; }
        public string MobileNo { get; set; }
        public string LocAddress { get; set; }
        public string LocLocality { get; set; }
        public string LocCity { get; set; }
        public string LocState { get; set; }
        public string PerAddress { get; set; }
        public string PerLocality { get; set; }
        public string PerCity { get; set; }
        public string PerState { get; set; }
        public string FatherName { get; set; }
        public string MotherName { get; set; }
        public string HusbandWife { get; set; }
        public string Qualification { get; set; }
        public string Experience { get; set; }
        public string Designation { get; set; }
        public string BloodGroup { get; set; }
        public string AadharNo { get; set; }
        public string UserCode { get; set; }
        public string UserType { get; set; }
        public string Password { get; set; }
        public string LoginId { get; set; }      
    }
    public class MasterState : pmMasterLogic
    {       
        public string StateCode { get; set; }
        public string StateName { get; set; }             
    }
    public class MasterCity : pmMasterLogic
    {
        public string CityCode { get; set; }
        public string CityName { get; set; }
        public string StateCode { get; set; }
    }
    public class MasterDesignation: pmMasterLogic
    {
        public string DesigCode { get; set; }
        public string DesigName { get; set; }       
    }
    public class MasterDetails: pmMasterLogic
    {
        public string RoleId { get; set; }
        public string RoleName { get; set; }
        public string MenuId { get; set; }  
        public string MenuName { get; set; }
        public string DesigCode { get; set; }
        public string DesigName { get; set; }
        public string CityCode { get; set; }
        public string CityName { get; set; }
        public string StateCode { get; set; }
        public string StateName { get; set; }
        public int AutoId { get; set; }
        public string SubMenuId { get; set; }
        public string SubMenuName { get; set; }
        public string SubMenuUrl { get; set; }
        public bool DisableFlag { get; set; }
        public string Loginid { get; set; }
        public int SequenceNo { get; set; }
        public string UsedFor { get; set; }
        public int MenuSequence { get; set; }
        public int emp_id { get; set; }
        public string EmployeeCode { get; set; }
        public string Title { get; set; }
        public string EmployeeName { get; set; }
        public string Gender { get; set; }
        public string MaritalStatus { get; set; }
        public DateTime? DOB { get; set; }
        public string MobileNo { get; set; }
        public string LocAddress { get; set; }
        public string LocLocality { get; set; }
        public string LocCity { get; set; }
        public string LocState { get; set; }
        public string PerAddress { get; set; }
        public string PerLocality { get; set; }
        public string PerCity { get; set; }
        public string PerState { get; set; }
        public string FatherName { get; set; }
        public string MotherName { get; set; }
        public string HusbandWife { get; set; }
        public string Qualification { get; set; }
        public string Experience { get; set; }
        public string Designation { get; set; }
        public string BloodGroup { get; set; }
        public string AadharNo { get; set; }
        public string UserCode { get; set; }
        public string UserType { get; set; }
        public string Password { get; set; }
        public string LoginId { get; set; }
    }

    public class DoctorMastersBO : pmMasterLogic
    {
        public string autoId { get; set; }
        public string hosp_id { get; set; }
        public string emp_code { get; set; }
        public string doctorId { get; set; }
        public string HISDoctorId { get; set; }
        public string DeptId { get; set; }
        public string status { get; set; }
        public string Emrgavail { get; set; }
        public string docshare { get; set; }
        public string onlineAppoint { get; set; }
        public string IsTokenReq { get; set; }
        public string title { get; set; }
        public string Prefix { get; set; }
        public string doctorname { get; set; }
        public string doctype { get; set; }
        public string phone { get; set; }
        public string mobile { get; set; }
        public string address { get; set; }
        public string specialization { get; set; }
        public string Designation { get; set; }
        public string degree { get; set; }
        public string degree2 { get; set; }
        public string gender { get; set; }
        public string imaregno { get; set; }
        public string regdate { get; set; }
        public string ImageName { get; set; }
        public string TagName { get; set; }
        public string Description { get; set; }
        public string virtual_path { get; set; }
        public string feefreq { get; set; }
        public string floorno { get; set; }
        public string roomno { get; set; }
        public string drstatus { get; set; }
        public string IsDirector { get; set; }
        public string IsOPD { get; set; }
        public string IsFreeOPD { get; set; }
        public string IsProfile { get; set; }
        public string IsExamRoomByPass { get; set; }
        public string patientduration { get; set; }
    }
    public class SpecializationBO : pmMasterLogic
    {
        public string hosp_id { get; set; }
        public string SpecId { get; set; }
        public string SpecName { get; set; }
        public string SpecDesc { get; set; }

    }
    public class DepartmentBO : pmMasterLogic
    {
        public string hosp_id { get; set; }
        public string DeptId { get; set; }
        public string DeptName { get; set; }
        public string DeptDesc { get; set; }

    }
    public class DegreeBO : pmMasterLogic
    {
        public string hosp_id { get; set; }
        public string DegId { get; set; }
        public string DegName { get; set; }
        public string DegDesc { get; set; }
    }
    public class DoctorsSlot : pmMasterLogic
    {
        public string autoid { get; set; }
        public string hosp_id { get; set; }
        public string doctorId { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
        public string ShiftName { get; set; }
        public string PatientLimit { get; set; }
        public string Daysvalues { get; set; }
        public string Datevalue { get; set; }
        public string DegDesc { get; set; }
    }
    public class DoctorLeave : pmMasterLogic
    {
        public string autoid { get; set; }
        public string doctorId { get; set; }
        public string inputdate { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
        public string Fromdate { get; set; }
        public string ToDate { get; set; }
    }
    public class DoctorRateList : pmMasterLogic
    {
        public string hosp_id { get; set; }
        public string ids { get; set; }
        public string typeval { get; set; }
        public string Rates { get; set; }
        public string lstarrvalues { get; set; }
        public string doctorId { get; set; }
    }

    public class CreateItem : pmMasterLogic
    {
        public string hosp_id { get; set; }
        public int autoid { get; set; }
        public string catid { get; set; }
        public string subcatid { get; set; }
        public string itemid { get; set; }
        public string itemname { get; set; }
        public string cptcodes { get; set; }
        public string isdiscountable { get; set; }
        public int isshareble { get; set; }
        public string itemtype { get; set; }

    }
    public class CategorySubCategoryBO : pmMasterLogic
    {
        public string hosp_id { get; set; }
        public int autoid { get; set; }
        public string catid { get; set; }        
        public string catname { get; set; }
        public string catdesc { get; set; }
        public string catabbr { get; set; }
        public string subcatid { get; set; }
        public string subcatname { get; set; }
        public string subcatdesc { get; set; }
        public string subcatabbr { get; set; }
    }
    public class FollowupMarkingBO : pmMasterLogic
    {
        public string HospId { get; set; }
        public int AutoId { get; set; }
        public string PanelId { get; set; }
        public string DoctorId { get; set; }
        public string DeptId { get; set; }        
        public string ItemId { get; set; }
        public string FollowpDays { get; set; }
        public string FollowpDetails { get; set; }
        public DataTable FollowupTable { get; set; }
    }

    public class PanelMasterBO: pmMasterLogic
    {
        public string HospId { get; set; }
        public int AutoId { get; set; }
        public string PanelId { get; set; }
        public string PanelCode { get; set; }
        public string PanelName { get; set; }
        public string PanelGroup{ get; set; }
        public string PanelGroupId { get; set; }
        public string PanelType { get; set; }
        public int IsCredit { get; set; }

        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string ContactPerson { get; set; }
        public string ContactNo { get; set; }
        public string PhoneNo { get; set; }
        public string Email { get; set; }
        public string Fax { get; set; }

        public string PaymentMode { get; set; }
        public string ValidFrom { get; set; }
        public string ValidTo { get; set; }

        public string ReferRateIpd { get; set; }
        public string ReferRateOpd { get; set; }
        public string CreditLimit { get; set; }
        public string RateTypeIpdOpd { get; set; }
        public string RatePalenId { get; set; }

        public string ShowPrintOut { get; set; }
        public string HideRate { get; set; }
        public long CoPaymentOn { get; set; }
        public string CoPaymentIn { get; set; }
        public string FollowCuurency { get; set; }
        public string AllowPartialPay { get; set; }
             

    }
    public class ReferralMasterBO : pmMasterLogic
    {
        public string HospId { get; set; }
        public string RefCode { get; set; }
        public string EmpName { get; set; }
        public string EmpCode { get; set; }
        public string BusinessType { get; set; }
        public string Ref_Name { get; set; }
        public string Degree { get; set; }
        public string Speciality { get; set; }
        public string Dob { get; set; }
        public string Clinicname { get; set; }
        public string Clinicaddress { get; set; }
        public string Cliniclocality { get; set; }
        public string State { get; set; }
        public string City { get; set; }
        public string ReportType { get; set; }
        public string Phone { get; set; }
        public string Mobile_no { get; set; }
        public string Email { get; set; }
        public string ProCode { get; set; }

    }
    public class StateDistrictBO : pmMasterLogic
    {
        public string hosp_id { get; set; }
        public int autoid { get; set; }
        public string CountryId { get; set; }
        public string CountryName { get; set; }
        public string StateCode { get; set; }
        public string StateName { get; set; }
        public string DistrictCode { get; set; }
        public string DistrictName { get; set; }        
    }
    public class NotificationBO
    {
        public int AutoId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string login_id { get; set; }
        public string CurNotification { get; set; }
        public string ValidFrom { get; set; }
        public string ValidTo { get; set; }
        public string ImagePath { get; set; }
        public string IsActive { get; set; }
        public string ImageName { get; set; }
        public byte[] ImageByte { get; set; }
        public string Logic { get; set; }
    }
    public class CPOETemplateItemsBO : pmMasterLogic
    {
        public string TemplateType { get; set; }
        public string GroupName { get; set; }
        public string DoctorId { get; set; }
        public string TemplateId { get; set; }
        public string ItemId { get; set; }
        public string ItemName { get; set; }
    }
    public class OPTHTemplateItemsBO : pmMasterLogic
    {
        public string TemplateType { get; set; }
        public string GroupType { get; set; }
        public string GroupName { get; set; }
        public string DoctorId { get; set; }
        public string TemplateId { get; set; }
        public string ItemId { get; set; }
        public string ItemName { get; set; }
    }
}