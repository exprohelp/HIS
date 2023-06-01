using System;

namespace HISWebApi.Models
{
    public class ipPharmacyInfo
    {       
        public string unit_id { get; set; }
        public string SaleInvNos { get; set; }
        public string uhid { get; set; }
        public string prm_1 { get; set; }
        public string prm_2 { get; set; }
        public string prm_3 { get; set; }
        public string ReportType { get; set; }
        public string IPDNos { get; set; }
        public string sale_inv_no { get; set; }
        public string IPDNo { get; set; }
        public string from { get; set; }
        public string to { get; set; }
        public string login_id { get; set; }
        public string Logic { get; set; }
    }
    public class PharmacyBillInfo : ipPharmacyInfo
    {
        public string indent_no { get; set; }
        public string pt_name { get; set; }
        public DateTime indent_date { get; set; }
        public string SaleStatus { get; set; }
        public string IndentStatus { get; set; }
        public string sale_inv_no { get; set; }
        public DateTime sale_date { get; set; }       
        public string pay_mode { get; set; }
        public string card_no { get; set; }
        public int ItemCount { get; set; }
        public DateTime saleCount { get; set; }
        public decimal Total { get; set; }
        public decimal discount { get; set; }
        public decimal amount { get; set; }      
    }
}