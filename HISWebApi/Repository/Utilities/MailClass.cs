using HISWebApi.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Web;

namespace HISWebApi.Repository.Utilities
{
    public class MailClass
    {
        public static string SendMailToHRFromContactQuery(PatientFeedbackInfo obj)
        {
            string result = string.Empty;
            MailMessage mail = new MailMessage();
            try
            {
                mail.From = new MailAddress("chl.hr@chandan.co.in");
                mail.To.Add(obj.MailTo);
                mail.Subject = "Patient Query (Chandan Hospital)";
                StringBuilder sb = new StringBuilder();
                sb.Append("<b>Subject : "+ obj.Subject + " </b> <br/><br/>");
                sb.Append("Description : " + obj.Subject + "<br/><br/>");
                sb.Append("Patient Email : " + obj.EmailId+"<br />Mobile : " + obj.MobileNo);
                if (obj.physical_path.Length>10)
                {
                    sb.Append("<br /><br/> Please Find the Attachment-");
                    mail.Attachments.Add(new Attachment(obj.physical_path));
                }
                mail.Body = sb.ToString();
                mail.IsBodyHtml = true;

                SmtpClient smtp = new SmtpClient("smtp.zoho.in",587);
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtp.Credentials = new System.Net.NetworkCredential("chl.hr@chandan.co.in", "Newhr@123");
                smtp.EnableSsl = true;
                smtp.Send(mail);
                result = "success";
            }
            catch (Exception ex)
            {
                result = ex.Message;
            }
            finally
            {
                mail.Dispose();
            }
            return result;
        }
        public static string SendMailToHRFromCarrier(CarrierInfo obj)
        {
            string result = string.Empty;
            string Body = string.Empty;
            MailMessage mail = new MailMessage();
            try
            {
                Attachment att = new Attachment(new MemoryStream(obj.fileBytes),obj.ImageName);
                mail.To.Add("ajeet@chandan");//chl.hr@chandan.co.in,
                mail.From = new MailAddress("chl.hr@chandan.co.in");

                mail.Subject = "Career (Chandan Hospital) | CV ";
                Body = obj.JobInformation + "<br/>" + "Please Find the Attachment <br>Candidate Emailid - </br>" + obj.Email + "<br/>";
                mail.Body = Body;
                mail.Attachments.Add(att);
                mail.IsBodyHtml = true;

                SmtpClient smtp = new SmtpClient("smtp.zoho.in", 587);
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtp.Credentials = new System.Net.NetworkCredential("chl.hr@chandan.co.in", "Newhr@123");
                smtp.EnableSsl = true;
                smtp.Send(mail);
                result = "Success";
            }
            catch (Exception ex)
            {
                result = ex.Message;
            }
            finally
            {
                mail.Dispose();
            }
            return result;
        }
    }
}