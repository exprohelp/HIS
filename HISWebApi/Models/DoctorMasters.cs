using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace HISWebApi.Models
{
    public class DoctorMasters
    {
        public class ipMasters
        {
            public Nullable<int> ID { get; set; }
            public string Logic { get; set; }
            public string Tagname { get; set; }
            public string Result { get; set; }
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

        public class DoctorMastersBO : ipMasters
        {
            public string autoId { get; set; }
            public string hosp_id { get; set; }            
            public string doctorId { get; set; }
            public string prm_1 { get; set; }
            public string prm_2 { get; set; }
            public string DeptId { get; set; }            
            public string status { get; set; }
            public string Emrgavail { get; set; }
            public string docshare { get; set; }
            public string onlineAppoint { get; set; }
            public string IsTokenReq { get; set; }
            public string title { get; set; }
            public string doctorname { get; set; }
            public string doctype { get; set; }
            public string phone { get; set; }
            public string mobile { get; set; }
            public string address { get; set; }
            public string specialization { get; set; }            
            public string degree { get; set; }
            public string gender { get; set; }
            public string imaregno { get; set; }
            public string regdate { get; set; }
            public string feefreq { get; set; }
            public string floorno { get; set; }
            public string roomno { get; set; }
            public string drstatus { get; set; } 
            public string patientduration { get; set; }
        }

        public class SpecializationBO : ipMasters
        {
            public string hosp_id { get; set; }
            public string SpecId { get; set; }
            public string SpecName { get; set; }
            public string SpecDesc { get; set; }

        }
        public class DepartmentBO : ipMasters
        {
            public string hosp_id { get; set; }
            public string DeptId { get; set; }
            public string DeptName { get; set; }
            public string DeptDesc { get; set; }
            
        }
        public class DegreeBO : ipMasters
        {
            public string hosp_id { get; set; }
            public string DegId { get; set; }
            public string DegName { get; set; }
            public string DegDesc { get; set; }
        }

        public class DoctorsSlot : ipMasters
        {
            public string autoid { get; set; }
            public string hosp_id { get; set; }
            public string doctorId { get; set; }            
            public string StartTime { get; set; }
            public string EndTime { get; set; }
            public string ShiftName { get; set; }
            public string PatientLimit { get; set; }
            public string Daysvalues { get; set; }
            public string Datevalue { get; set; }
            public string DegDesc { get; set; }
        }

        public class DoctorLeave : ipMasters
        {
            public string autoid { get; set; }
            public string doctorId { get; set; }
            public string inputdate { get; set; }
            public string StartTime { get; set; }
            public string EndTime { get; set; }
            public string Fromdate { get; set; }
            public string ToDate { get; set; }
        }

        public class DoctorRateList : ipMasters
        {
            public string hosp_id { get; set; }
            public string ids { get; set; }
            public string typeval { get; set; }
            public string Rates { get; set; }
            public string lstarrvalues { get; set; }
            public string doctorId { get; set; }
        }

    }
}