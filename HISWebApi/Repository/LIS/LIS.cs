using HISWebApi.Models;
using HISWebApi.Repository.IPD;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace HISWebApi.Repository.LIS
{
    public class LIS
    {
        LISDBLayer lis_layer = new LISDBLayer();

        public dataSet GetPatientDetails(LISBO.PatientBO objBO)
        {
            string _result = string.Empty;
            return lis_layer.GetPatientDetails(objBO);
        }
        public string GetAssignedReport(LISBO.PatientBO objBO)
        {
            string _result = string.Empty;
            return lis_layer.GetAssignedReport(objBO);
        }
        public string UpdateRemark(LISBO.PatientBO objBO)
        {
            string _result = string.Empty;
            return lis_layer.UpdateRemark(objBO);
        }
    }
}