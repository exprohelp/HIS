using HISWebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace HISWebApi.Repository.WebPortal
{
    public class SmsDataLayer
    {
        SmsClass sms = new SmsClass();
        private Appoinment repositoryAppoinment = new Appoinment();

        public string PatientLoginOTP(ipSMS objBO)
        {
            string msg = objBO.Otp + " is your chandan member verification code";
            string smsresponse = sms.SendSms(objBO.MobileNo, msg);
            if (smsresponse == "Sent")
            {
                PatientSmsInfo obj = new PatientSmsInfo();
                obj.AppointmentId = objBO.AppId;
                obj.MobileNo = objBO.MobileNo;
                obj.Sms = msg;
                obj.SmsResponse = smsresponse;
                obj.EntrySource = "WebPortal:PatientLoginSms";
                obj.login_id = "-";
                obj.Logic = "InsertSmsLog";
                repositoryAppoinment.InsertSmsLog(obj);
            }
            if (!string.IsNullOrEmpty(smsresponse))
            {
                return smsresponse;
            }
            else
            {
                return "SMS not send, please try again";
            }
        }
        public string SendSmsToPatient(ipSMS objBO)
        {
            string msg = "Dear " + objBO.PatientName + " your appointment for" + objBO.DoctorName + "is confirmed on " + objBO.AppDate +
                "at Chandan Hospital appointment Id " + "A" + objBO.AppId + "For any query call us at 0522-6666666";

            string smsresponse = sms.SendSms(objBO.MobileNo, msg);
            if (smsresponse == "Sent")
            {
                PatientSmsInfo obj = new PatientSmsInfo();
                obj.AppointmentId = objBO.AppId;
                obj.MobileNo = objBO.MobileNo;
                obj.Sms = msg;
                obj.SmsResponse = smsresponse;
                obj.EntrySource = "OPD:PatientSms";
                obj.login_id = "-";
                obj.Logic = "InsertSmsLog";
                repositoryAppoinment.InsertSmsLog(obj);
            }
            if (!string.IsNullOrEmpty(smsresponse))
            {
                return smsresponse;
            }
            else
            {
                return "SMS not send, please try again";
            }
        }
        public string SendSmsToEmployee(ipSMS objBO)
        {
            string msg = "You got new booking for Patient from Chandan web Portal with App Id " + "A" + objBO.AppId + " for Dated " + objBO.AppDate + "";

            string smsresponse = sms.SendSms(objBO.MobileNo, msg);
            if (smsresponse == "Sent")
            {
                PatientSmsInfo obj = new PatientSmsInfo();
                obj.AppointmentId = objBO.AppId;
                obj.MobileNo = objBO.MobileNo;
                obj.Sms = msg;
                obj.SmsResponse = smsresponse;
                obj.EntrySource = "OPD:EmployeeSms";
                obj.login_id = "-";
                obj.Logic = "InsertSmsLog";
                repositoryAppoinment.InsertSmsLog(obj);
            }
            if (!string.IsNullOrEmpty(smsresponse))
            {
                return smsresponse;
            }
            else
            {
                return "SMS not send, please try again";
            }
        }
        public string OnlineConsultationPatientSms(ipSMS objBO)
        {
            string msg = "Dear Patient, Your Appointment Id is " + objBO.AppId + ". soon our Hospital Executive will call you.";
            string smsresponse = sms.SendSms(objBO.MobileNo, msg);
            if (smsresponse == "Sent")
            {
                PatientSmsInfo obj = new PatientSmsInfo();
                obj.AppointmentId = objBO.AppId;
                obj.MobileNo = objBO.MobileNo;
                obj.Sms = msg;
                obj.SmsResponse = smsresponse;
                obj.EntrySource = "OnlineConsultationBooking:PatientSms";
                obj.login_id = "-";
                obj.Logic = "InsertSmsLog";
                repositoryAppoinment.InsertSmsLog(obj);
            }
            if (!string.IsNullOrEmpty(smsresponse))
            {
                return smsresponse;
            }
            else
            {
                return "SMS not send, please try again";
            }
        }
        public string OnlineConsultationSendSmsToEmp(ipSMS objBO)
        {
            string msg = "New Online doctor appointment booking with booking id : " + objBO.AppId + ". Please take care.";            
            string smsresponse = sms.SendSms(objBO.MobileNo, msg);
            if (smsresponse == "Sent")
            {
                PatientSmsInfo obj = new PatientSmsInfo();
                obj.AppointmentId = objBO.AppId;
                obj.MobileNo = objBO.MobileNo;
                obj.Sms = msg;
                obj.SmsResponse = smsresponse;
                obj.EntrySource = "OnlineConsultationBooking:EmployeeSms";
                obj.login_id = "-";
                obj.Logic = "InsertSmsLog";
                repositoryAppoinment.InsertSmsLog(obj);
            }
            if (!string.IsNullOrEmpty(smsresponse))
            {
                return smsresponse;
            }
            else
            {
                return "SMS not send, please try again";
            }
        }
    }
}