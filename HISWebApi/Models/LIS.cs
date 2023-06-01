using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace HISWebApi.Models
{
    public class LISBO
    {
        public class ipLIS
        {
            public Nullable<int> ID { get; set; }
            public string Logic { get; set; }
            public string Result { get; set; }
            public int auto_id { get; set; }
            public bool Flag { get; set; }
            public bool IsActive { get; set; }
            public string login_id { get; set; }
            public DateTime? CreatedOn { get; set; }
        }
        public class dataSet
        {
            public string Msg { get; set; }
            public DataSet ResultSet { get; set; }
        }
        public class PatientBO : ipLIS
        {
            public string visit_no { get; set; }
            public string section { get; set; }
            public string from { get; set; }
            public string to { get; set; }
            public string Prm1 { get; set; }
        }
    }
}