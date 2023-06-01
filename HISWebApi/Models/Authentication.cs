using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HISWebApi.Models
{
    public class AuthenticationBO
    {
        public int ID { get; set; }
        public string LoginID { get; set; }
        public string EmpCode { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public bool IsActive { get; set; }
    }
    public class ipAuthentication
    {
        public string unit_id { get; set; }
        public string LoginId { get; set; }
        public string Password { get; set; }
        public string role_id { get; set; }
        public string menu_id { get; set; }
        public string sub_menu_id { get; set; }
        public string Prm1 { get; set; }
        public string Logic { get; set; }
    }
}