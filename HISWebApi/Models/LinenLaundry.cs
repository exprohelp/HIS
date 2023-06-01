using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HISWebApi.Models
{
    public class ipLinenLaundry
    {
        public int AutoId { get; set; }
        public string hosp_id { get; set; }
        public string CartId { get; set; }
        public DateTime from { get; set; }
        public DateTime To { get; set; }
        public string IndentNo { get; set; }
        public string FloorName { get; set; }
        public string WardId { get; set; }
        public string NSID { get; set; }
        public string RoomId { get; set; }
        public string ItemId { get; set; }
        public string Prm1 { get; set; }
        public string Prm2 { get; set; }
        public int IsActive { get; set; }
        public string login_id { get; set; }
        public string Logic { get; set; }
    }
    public class RequestMaster : ipLinenLaundry
    {
        public string NursingId { get; set; }
        public int TotalRooms { get; set; }
        public int TotalBeds { get; set; }
        public int ItemStock { get; set; }
        public int InUse { get; set; }
        public int ReadyToUse { get; set; }
        public int TotalReq { get; set; }
        public int InTransit { get; set; }
    }
    public class TransferLinen
    {
        public string lot_no { get; set; }
        public string ItemId { get; set; }
        public string trf_remark { get; set; }
        public int qty { get; set; }
        public int damage_qty { get; set; }
        public string trf_to { get; set; }
        public string trf_from { get; set; }
        public string login_id { get; set; }
        public string IndentNo { get; set; }
        public string AutoId { get; set; }
        public string Logic { get; set; }
    }
    public class ipLinen
    {
        public string HospId { get; set; }
        public string CartId { get; set; }
        public string TranId { get; set; }
        public string ItemId { get; set; }
        public string trf_from { get; set; }
        public string OutPutType { get; set; }
        public string Prm1 { get; set; }
        public string Prm2 { get; set; }
        public string login_id { get; set; }
        public string Logic { get; set; }
    }
    public class IndentInfo : ipLinen
    {
        public int AutoId { get; set; }
        public string IndentNo { get; set; }
        public int qty { get; set; }
        public string Remark { get; set; }
        public DateTime expDate { get; set; }
    }
    public class LaundryReceive
    {
        public string hosp_id { get; set; }
        public string CartId { get; set; }
        public string item_id { get; set; }
        public string from { get; set; }
        public string to { get; set; }
        public string transid { get; set; }
        public string IssueTo { get; set; }
        public string transtype { get; set; }
        public string compid { get; set; }
        public string frmcart { get; set; }
        public string tocart { get; set; }
        public string masterkeyid { get; set; }
        public string qty { get; set; }
        public int StockQuantity { get; set; }
        public string remark { get; set; }
        public string login_id { get; set; }
        public string Logic { get; set; }
    }
}