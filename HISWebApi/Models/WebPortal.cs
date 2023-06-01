using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HISWebApi.Models
{
    public class ipWebPortal
    {
        public int Id { get; set; }
        public string DoctorId { get; set; }
        public string DoctorName { get; set; }
        public string AppId { get; set; }
        public string AppDate { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string Logic { get; set; }
        public string login_id { get; set; }
        public string uhid { get; set; }
        public string StartTime { get; set; }
        public string Desc { get; set; }
        public string IsReg { get; set; }
        public string centerId { get; set; }
        public string Title { get; set; }
        public string PatientName { get; set; }
        public string Age { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }
        public string CountryID { get; set; }
        public string cityID { get; set; }
        public string House_No { get; set; }
        public string Gender { get; set; }
        public string PFirstName { get; set; }  
        public string PLastName { get; set; }
        public string StateID { get; set; }
        public string DistrictID { get; set; }
        public string ProcessInfo { get; set; }
        public string Address { get; set; }
        public string OpdRemark { get; set; }
    }
    public class ipSMS : ipWebPortal
    {
        public string MobileNo { get; set; }
        public string Otp { get; set; }
        public string Sms { get; set; }
        public string RequestId { get; set; }
        public string PatientName { get; set; }
        public string AmbulanceMobileNumber { get; set; }
        public string PickupDateTime { get; set; }
        public string AmbulanceNumber { get; set; }
    }

    public class NotificationMaster : ipWebPortal
    {
        public int City_Id { get; set; }
        public string City_Name { get; set; }
        public string TokenNo { get; set; }
    }
    public class AwardInfo
    {
        public string Auto_Id { get; set; }
        public string AwardName { get; set; }
        public string FilePath { get; set; }
        public int IsActive { get; set; }
        public string login_id { get; set; }
        public string date { get; set; }
        public string Logic { get; set; }
        public string virtual_path { get; set; }
        public string physical_path { get; set; }
        public string ImageType { get; set; }
        public string ImageName { get; set; }
        public byte[] ImageByte { get; set; }
        public string Message { get; set; }


    }
    public class PatientSmsInfo : ipWebPortal
    {
        public string AppointmentId { get; set; }
        public string MobileNo { get; set; }
        public string Sms { get; set; }
        public string SmsResponse { get; set; }
        public string EntrySource { get; set; }
       
    }
    public class CarrierInfo : ipWebPortal
    {
        public string physical_path { get; set; }
        public string JobInformation { get; set; }       
        public string Job_Description { get; set; }       
        public byte[] fileBytes { get; set; }       
        public string MobileNo { get; set; }       
        public string ImageName { get; set; }       
        public string Desired_Profile { get; set; }  
        public string Other_Details { get; set; }  
        public string No_Of_Openings { get; set; }  
        public string IsActive { get; set; }  
     
    }
    public class ipDoctors
    {

       public string hosp_id { get; set; }
        public string DeptId { get; set; }
        public string DoctorId { get; set; }
        public string Prm1 { get; set; }
        public string prm2 { get; set; }
        public string login_id { get; set; }
        public string Logic { get; set; }
        public string IsActive { get; set; }
        public int AutoId { get; set; }
        public int srno { get; set; }
        public string DeptName { get; set; }
        public string DeptShortName { get; set; }
        public string TagName { get; set; }
        public string virtual_path { get; set; }
        public string physical_path { get; set; }
        public string Description { get; set; }
        public string MediaLink { get; set; }
        public string PhyPath { get; set; }
        public string CreatedBy { get; set; }
        public string MediaType { get; set; }
        public string ImgPath { get; set; }
        public string ImgPostion { get; set; }
        //  public string Prm1 { get; set; }     
        public string ImageType { get; set; }
        public string ImageName { get; set; }
        public byte[] ImageByte { get; set; }

    }
    public class ipPatientFeedback
    {

        public int Auto_Id { get; set; }
        public string Logic { get; set; }
        public string Result { get; set; }
        public bool Flag { get; set; }
        public bool IsActive { get; set; }
        public string DeptName { get; set; }
        public string EmpId { get; set; }
        public string EmpName { get; set; }
        public string Designation { get; set; }
        public string EmailId { get; set; }
        public string MobileNo { get; set; }
        public string hosp_id { get; set; }
        public string login_id { get; set; }
        public string prm_1 { get; set; }
        public string prm_2 { get; set; }
        public DateTime? CreatedOn { get; set; }
    }
    public class PatientFeedbackInfo : ipPatientFeedback
    {

        public string QueryType { get; set; }
        public string MailTo { get; set; }
        public string Subject { get; set; }
        public string Attachment { get; set; }
        public string virtual_path { get; set; }
        public string physical_path { get; set; }
        public string ImageType { get; set; }
        public string ImageName { get; set; }
        public byte[] ImageByte { get; set; }
        public string Message { get; set; }

    }
    public class ViedoLibraryInfo : ipPatientFeedback
    {
        public int AutoId { get; set; }
        public string title { get; set; }
        public string link { get; set; }
        public int seq_no { get; set; }
    }
}
