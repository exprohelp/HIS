using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace HISWebApi.Models
{

    public class EdpMaster
    {
        public Nullable<int> ID { get; set; }
        public string Logic { get; set; }
        public string Result { get; set; }
        public bool Flag { get; set; }
        public bool IsActive { get; set; }
        public string login_id { get; set; }
        public string warehouseCartId { get; set; }
        public string prm_1 { get; set; }
        public string FileType { get; set; }
        public string prm_2 { get; set; }
        public string hosp_id { get; set; }
        public DateTime? CreatedOn { get; set; }
    }

    public class PanelRateItemBO:EdpMaster
    {
        public string autoId { get; set; }        
        public string PanelId { get; set; }
        public string DoctorId { get; set; }
        public string RateListId { get; set; }
        public string ItemId { get; set; }
        public string ItemName { get; set; }
        public string ItemCode { get; set; }
        public string catid { get; set; }
        public string subcatid { get; set; }
        public decimal rate { get; set; }
        public string RateItemList { get; set; }
        public string RoomBillCategory { get; set; }
        public string Srno { get; set; }
        public string ExcelFlag { get; set; }
        public DataTable RateItemListTable { get; set; }


    }                 
                      
}                     
                      