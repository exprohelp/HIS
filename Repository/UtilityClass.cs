using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace MediSoftTech_HIS
{
    public static class UtilityClass
    {
        public static string decoding(string toEncode)
        {
            string base64Decoded;
            byte[] data = System.Convert.FromBase64String(toEncode);
            base64Decoded = System.Text.ASCIIEncoding.ASCII.GetString(data);
            return base64Decoded;
        }
    }
}