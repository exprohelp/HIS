using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Text.RegularExpressions;
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;
using System.Text;
using RestSharp;
using Newtonsoft.Json.Linq;

namespace HISWebApi.Repository.Utilities
{
   public class SmsClass
    {
        public string SendSms(string MobileNoByComaSeperated, string msg)
        {
            String responseFromServer = string.Empty;
            try
            {
                string url ="https://http.myvfirst.com/smpp/sendsms?username=chndnotphtp&password=chn2130O&to=" + MobileNoByComaSeperated + "&from=chandn&text=" + msg + "";
                //string url = "http://www.myvaluefirst.com/smpp/sendsms?username=chndnotphtp&password=chn2130O&to=" + MobileNoByComaSeperated + "&from=chandn&text=" + msg + "";
                //string url = "https://push.sanketik.net//api/push?accesskey=8YeoMztmrqYWFZCNM7SUM6jWNkPWN7&to=" + MobileNoByComaSeperated + "&text=" + msg + "&from=CHCARE";
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
            catch (Exception ex) {
                responseFromServer = ex.Message;
            }
            return responseFromServer;
        }
        public string Shorten(string longUrl)
        {
            string API_KEY = "c4b1e2497fd30c27b2ec87d71a4c0089e44480e6";
            string API_URL = "https://api-ssl.bit.ly/v4";

                var client = new RestClient(API_URL);
                    var request = new RestRequest("shorten");
                    request.AddHeader("Authorization", $"Bearer {API_KEY}");
                    var param = new Dictionary<string, string> {
                { "long_url", longUrl }
            };
                    request.AddJsonBody(param);
                    var response = client.Post(request);
                    string content = response.Content;
                    // WriteLine(content);
                    JObject d = JObject.Parse(content);
                    var result = (string)d["id"];
                    return result;
                }
    }
}