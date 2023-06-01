using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HISWebApi.Models
{
    public class ipAsset
    {
        public string unit_id { get; set; }
        public string eq_no { get; set; }
        public string DeptId { get; set; }
        public string from { get; set; }
        public string to { get; set; }
        public string prm_1 { get; set; }
        public string login_id { get; set; }
        public string ComplCode { get; set; }
        public string Remarks { get; set; }
        public string Logic { get; set; }
    }
    public class ipAssetMovement :ipAsset
    {
        public string SectionCode { get; set; }
        public string Remark { get; set; }
    }
    public class AssetsBO : ipAsset
    {
        public string eqNo { get; set; }
        public string DeptCode { get; set; }
        public string DeptName { get; set; }
        public string DeptHead { get; set; }
        public string SectionCode { get; set; }
        public string SectionName { get; set; }
    }
    public class AssetsComplaintsBO : ipAsset
    {
        public string EmpCode { get; set; }
        public string UnitId { get; set; }
        public string EqNo { get; set; }
        public string Complaint { get; set; }
        public string StatusOk { get; set; }
        public string ClientStatus { get; set; }
    }
}