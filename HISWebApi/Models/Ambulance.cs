using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HISWebApi.Models
{
    public class ipAmbulance
    {
        public string RequestId { get; set; }
        public string DriverId { get; set; }
        public string from { get; set; }
        public string to { get; set; }
        public string Prm1 { get; set; }
        public string Prm2 { get; set; }
        public string login_id { get; set; }
        public string Logic { get; set; }
        public string IsActive { get; set; }
        public string AmbulanceId { get; set; }
        
    }
    public class ipAmbulanceTrack
    {
        public string AmbulanceId { get; set; }
        public string UserPsw { get; set; }
        public string RequestId { get; set; }
        public string Latitdude { get; set; }
        public string Longitude { get; set; }
        public string Prm1 { get; set; }
        public string Logic { get; set; }
    }

    public class AmbulanceResponse
    {
        public String RequestId;
        public String IPDNo;
        public String Requestby;
        public String RequestType;
        public String PatientName;
        public String RequestNote;
        public String PatientAddress;
        public String Latitude;
        public String Longitude;
        public String MobileNo;
        public String PickupDate;
        public String PickupTime;
        public String AmbulanceId;
    }
    public class AmbulanceMastersInfo:ipAmbulance
	{
		public string DriverName { get; set; }
		public string AmbulanceName { get; set; }
		public string AmbulanceNumber { get; set; }
		public string DriverMobileNo { get; set; }
		public string AmbulanceMobileNo { get; set; }
		public string DocType { get; set; }
		public string FilePath { get; set; }
		public string FitnessDoc { get; set; }
		public string PollutionDoc { get; set; }
		public string RegistrationDoc { get; set; }
		public string InsuranceDoc { get; set; }
		public string ImageName { get; set; }
	}
}