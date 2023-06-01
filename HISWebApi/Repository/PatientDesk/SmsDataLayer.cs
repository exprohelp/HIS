using HISWebApi.Models;
using HISWebApi.Repository.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace HISWebApi.Repository.PatientDesk
{
    public class SmsDataLayer
    {
        SmsClass sms = new SmsClass();
        private Patient repositoryPatient = new Patient();

		public string SendTrackingLink(ipSMS objBO)
		{
			SmsClass sms = new SmsClass();
            string Link= "https://chandanhospital.in/Hospital/Patient/Ta?ReqId=" + objBO.RequestId;
           // string Link = "https://exprohelp.com/his/PatientDesk/Patient/TA?ReqId=" + objBO.RequestId;
            string shortUrl = sms.Shorten(Link);
            string msg = "Dear "+ objBO.PatientName + ", ambulance by Chandan Hospital is booked, please updated your location to track the ambulance on the following link. "+ shortUrl;
			string smsresponse = sms.SendSms(objBO.MobileNo, msg);
			return smsresponse;
	    }
        public string InformToAmbulanceDriver(EmergencyDesk objBO)
        {
            string smsresponse = string.Empty;
            dataSet ds = repositoryPatient.AmbulanceAndEmergencyQueries(objBO);
            if (ds.ResultSet.Tables.Count > 0 && ds.ResultSet.Tables[0].Rows.Count > 0)
            {
                SmsClass sms = new SmsClass();
                string AmbulanceMobNumber = ds.ResultSet.Tables[0].Rows[0]["mobileNo"].ToString();
                string AmbulanceNumber = ds.ResultSet.Tables[0].Rows[0]["AmbulanceNumber"].ToString();
                string PickupDate = ds.ResultSet.Tables[0].Rows[0]["PickupDate"].ToString();
                string msg = "एंबुलेंस " + AmbulanceNumber + " बुक कर ली गई है। कृपया मोबाइल ऐप देखें। Team Chandan ";
                //string msg = "New Ambulance Request for " + AmbulanceNumber + ", Please Pick patient at " + PickupDate + " Check App.";
                smsresponse = sms.SendSms(AmbulanceMobNumber, msg);
            }
            else
            {
                smsresponse = "Ambulance or Pickup Date/Time is not set ";
            }
            return smsresponse;
        }
    }
}