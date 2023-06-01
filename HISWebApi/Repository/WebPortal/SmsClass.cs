using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;

namespace HISWebApi.Repository.WebPortal
{
    public class SmsClass
    {
        public string SendSms(string MobileNoByComaSeperated, string msg)
        {
            String responseFromServer = string.Empty;
            string url = "";
            string Provider = "ValueFirst";
            try
            {

                if (Provider == "ValueFirst")
                    url = "https://http.myvfirst.com/smpp/sendsms?username=chndnotphtp&password=chn2130O&to=" + MobileNoByComaSeperated + "&from=chandn&text=" + msg + "";
                else
                    url = "https://http.myvfirst.com/smpp/sendsms?username=chndnotphtp&password=chn2130O&to=" + MobileNoByComaSeperated + "&from=chandn&text=" + msg + "";

                WebRequest request = WebRequest.Create(url);
                request.Method = "GET";
                //request.ContentLength = sURL.Length;
                request.Credentials = CredentialCache.DefaultCredentials;
                HttpWebResponse response1 = (HttpWebResponse)request.GetResponse();
                Stream dataStream = response1.GetResponseStream();
                StreamReader reader = new StreamReader(dataStream);
                responseFromServer = reader.ReadToEnd();
                reader.Close();
                dataStream.Close();
                response1.Close();
            }
            catch (Exception ex)
            {
                responseFromServer = ex.Message;
            }
            return responseFromServer;
        }

    }
}