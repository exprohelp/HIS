using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace HISWebApi.Models
{
  
        public class ipDoctorApp
        {

            public string emp_code { get; set; }
            public string Password { get; set; }
            public string DoctorId { get; set; }
            public string login_id { get; set; }
            public string from { get; set; }
            public string to { get; set; }
            public string Prm1 { get; set; }
            public string Prm2 { get; set; }
           public string ReportType { get; set; }
        
            public string Logic { get; set; }
        }
        public class ipReference
        {
            public string HospId { get; set; }
            public string RefId { get; set; }
            public string UHID { get; set; }
            public string IPDNo { get; set; }
            public string PatientName { get; set; }
            public string RoomName { get; set; }
            public string RefBy { get; set; }
            public string RefTo { get; set; }
            public string RefType { get; set; }
            public string RefRemark { get; set; }
            public string Observation { get; set; }
            public string RefStatus { get; set; }
            public string login_id { get; set; }
            public string Logic { get; set; }
            public string DoctorId { get; set; }
            public string from { get; set; }
            public string to { get; set; }
            public string Prm1 { get; set; }
            public string SmsText { get; set; }
        }
    public class OTBookingInfo : ipDoctorApp
    {
        public string AutoId { get; set; }
        public string OTCartId { get; set; }
        public string UHID { get; set; }
        public string patientName { get; set; }
        public string patientMobile { get; set; }
        public string bookingDate { get; set; }
        public string bookFromTime { get; set; }
        public string bookToTime { get; set; }
        public string Remark { get; set; }
        public string ItemRequired { get; set; }

        public string bookBy { get; set; }
    }
    public class ConferenceBookingInfo : ipDoctorApp
    {
        public string ConfId { get; set; }
        public int BookingId { get; set; }
        public string BookingType { get; set; }
        public string patientMobile { get; set; }
        public string bookingDate { get; set; }
        public string bookFromTime { get; set; }
        public string bookToTime { get; set; }
        public string Subject { get; set; }
        public string Remark { get; set; }
        public string bookBy { get; set; }
    }
}