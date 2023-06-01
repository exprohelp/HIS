using HISWebApi.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;

namespace HISWebApi.Repository.Utilities
{
    public static class OTPGenerator
    {
        //Example  
        //string[] saAllowedCharacters = { "1", "2", "3", "4", "5", "6", "7", "8", "9", "0" };
        //string sRandomOTP = = GenerateRandomOTP(8, saAllowedCharacters);
        public static string GenerateRandomOTP(int iOTPLength)
        {
            string[] saAllowedCharacters = { "1", "2", "3", "4", "5", "6", "7", "8", "9", "0" };
            string sOTP = String.Empty;
            string sTempChars = String.Empty;
            Random rand = new Random();
            for (int i = 0; i < iOTPLength; i++)
            {

                int p = rand.Next(0, saAllowedCharacters.Length);

                sTempChars = saAllowedCharacters[rand.Next(0, saAllowedCharacters.Length)];

                sOTP += sTempChars;

            }

            return sOTP;

        }
        public static string PatientFeedBackOTP(HospitalBO objBO)
        {
            string result = string.Empty;
            try
            {
                string mobile_no = objBO.Mobile;
                string OTP = Utilities.OTPGenerator.GenerateRandomOTP(4);
                string sms = "Your Feedback Verification Code is "+ OTP + ".Team Chandan";
                //string sms = "Your Feedback Verification Code is 3445. Team Chandan";
                
                Utilities.SmsClass smsService = new Utilities.SmsClass();
                string responsemessage = smsService.SendSms(mobile_no, sms);
                result = OTP;
                try
                {
                    string qry = "insert into online_doctorAppSmsLog(appointmentId, mobile_no, sms, login_id, response_message) ";
                    qry += "values('-', '" + mobile_no + "', '" + sms + "', '-','" + responsemessage + "')";
                    DBManager.QueryExecute(qry, GlobalConfig.ConStr_Hospital);
                }
                catch (Exception ex) { }

            }
            catch (Exception ex) { result = ex.Message; }
            return result;
        }

    }

}