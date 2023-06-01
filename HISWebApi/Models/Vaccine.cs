using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HISWebApi.Models
{
    public class ipVaccine
    {
        public string UHID { get; set; }
        public string app_no { get; set; }
        public string DoctorId { get; set; }
        public string Template { get; set; }
        public int VaccineId { get; set; }
        public string dob { get; set; }
        public string from { get; set; }
        public string to { get; set; }
        public string Prm1 { get; set; }
        public string Prm2 { get; set; }
        public string login_id { get; set; }
        public string Logic { get; set; }
    }
    public class VaccinationInfo : ipVaccine
    {
        public string IPDNo { get; set; }
        public string UHID { get; set; }
        public string PatientName { get; set; }
        public string Gender { get; set; }
        public DateTime? DOB { get; set; }
        public DateTime? StartDate { get; set; }
        public string AttendantName { get; set; }
        public string mobileNo { get; set; }
        public string TemplateName { get; set; }
        public int VaccineId { get; set; }      
        public string VaccineName { get; set; }
        public string PharSaleInvNo { get; set; }
        public int ScheduleDay { get; set; }
        public DateTime? ScheduleDate { get; set; }
        public DateTime? PlanedFromDate { get; set; }
        public DateTime? PlanedToDate { get; set; }
        public string PlanedBy { get; set; }
        public DateTime? VaccinationDate { get; set; }
        public string District { get; set; }
        public string Address { get; set; }
        public string VaccineGivenBy { get; set; }
        public DateTime? VaccinationTime { get; set; }
    }
}