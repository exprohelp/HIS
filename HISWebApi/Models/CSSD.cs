using System;

namespace HISWebApi.Models
{
	public class CSSD
	{
		public class ipCSSD
		{
			public int AutoId { get; set; }
			public int ID { get; set; }
			public string hosp_id { get; set; }
			public string ItemId { get; set; }
			public string ItemName { get; set; }
			public string CartdId { get; set; }
			public string IndentNo { get; set; }
			public string Prm_1 { get; set; }
			public string Prm_2 { get; set; }
			public string login_id { get; set; }
			public int qty { get; set; }
			public int IsActive { get; set; }
			public string Logic { get; set; }
		}
		
		public class ItemInfo:ipCSSD
		{					
			public string ItemType { get; set; }
			public string SterileType { get; set; }
			public string wh_ItemId { get; set; }
			public string ItemCategory { get; set; }			
		}
		public class SetMasterInfo : ipCSSD
		{
			public string DeptName { get; set; }
			public string SetId { get; set; }
			public string SetName { get; set; }
			public string ItemType { get; set; }
			public int expiryDays { get; set; }
			public int nos { get; set; }
			public string description { get; set; }			
		}
		public class MovementInfo : ipCSSD
		{
			public string TrnNo { get; set; }
			public string IPDNo { get; set; }
			public string FromCart { get; set; }
			public string ToCart { get; set; }
			public string SetId { get; set; }			
			public string BatchNo { get; set; }			
			public DateTime expDate { get; set; }						
			public string SetKey { get; set; }
			public string ItemType { get; set; }
			public string Remark { get; set; }
		}
	}
}