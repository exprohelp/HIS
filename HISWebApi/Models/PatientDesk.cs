using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HISWebApi.Models
{
    public class ipPatientDesk
    {
        public string RequestId { get; set; }
        public string UHID { get; set; }
        public string IPDNo { get; set; }
        public string DriverId { get; set; }
        public string AmbulanceId { get; set; }
        public string from { get; set; }
        public string to { get; set; }
        public string Prm1 { get; set; }
        public string Prm2 { get; set; }
        public string login_id { get; set; }
        public string Logic { get; set; }
    }
    public class EmergencyDesk : ipPatientDesk
    {
        public string PatientName { get; set; }
        public string EntryDate { get; set; }
        public string RequestBy { get; set; }
        public string RequestType { get; set; }
        public string RequestNote { get; set; }
        public string District { get; set; }
        public string LandMark { get; set; }
        public string PatientAddress { get; set; }
        public string PatientGeoLat { get; set; }
        public string PatientGeoLong { get; set; }
        public string MobileNo { get; set; }
        public string RequestTakenBy { get; set; }
        public string PickupDate { get; set; }
        public string PickupTime { get; set; }
        public string AmbulanceTrackingLink { get; set; }
        public string AllotedDriverId { get; set; }
    }
   
       
}