using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HISWebApi.Models
{
    public class OTSchedule
    {
        public string OTId { get; set; }
        public string DoctorId { get; set; }
        public string IPDNo { get; set; }
        public string from { get; set; }
        public string to { get; set; }
        public string Prm1 { get; set; }
        public string Prm2 { get; set; }
        public string Logic { get; set; }       
    }
}