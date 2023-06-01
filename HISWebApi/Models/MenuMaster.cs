namespace MISWebApi.Models
{
	public class MenuMaster
	{
		public class GroupMaster
		{
			public int AutoId { get; set; }
			public string GroupId { get; set; }
			public string GroupName { get; set; }
			public string GroupFor { get; set; }
			public string GroupItemId { get; set; }
			public int IsActive { get; set; }
			public string Logic { get; set; }
		}
		public class GroupItems
		{
			public int AutoId { get; set; }
			public string GroupId { get; set; }
			public string GroupName { get; set; }
			public string GroupFor { get; set; }
			public string GroupItemId { get; set; }
			public string Prm1 { get; set; }
			public string Prm2 { get; set; }
			public int IsActive { get; set; }
			public string Logic { get; set; }
		}
	}
}