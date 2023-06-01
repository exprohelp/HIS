using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace HISWebApi.Models
{
    public class Corporate
    {
        public class ipCorporate
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
        public class FundManagementInfo : ipCorporate
        {
            public int AutoId { get; set; }
            public string PanelId { get; set; }
            public DateTime trn_date { get; set; }
            public Decimal Amount { get; set; }
            public string RefDetail { get; set; }
            public string RefNo { get; set; }
            public string UHID { get; set; }
            public string UTRNo { get; set; }
            public DateTime UTRDate { get; set; }
            public string TnxType { get; set; }
            public string Prm1 { get; set; }
            public string Prm2 { get; set; }
        }
        public class PanelInfo : ipCorporate
        {
            public string PanelId { get; set; }
            public string UHID { get; set; }
            public DateTime from { get; set; }
            public DateTime to { get; set; }
            public string Prm1 { get; set; }
            public string Prm2 { get; set; }
        }
        public class PanelItemExcludeBO : ipCorporate
        {
            public string hosp_id { get; set; }
            public string pie_id { get; set; }
            public string panel_id { get; set; }
            public string item_id { get; set; }
            public int isActive { get; set; }
        }
        public class PanelLinkBO : ipCorporate
        {
            public string master_panel_id { get; set; }
            public string link_panel_id { get; set; }
            public string active_flag { get; set; }
        }
    }
}