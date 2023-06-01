using HISWebApi.Models;
using MISWebApi.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using static MISWebApi.Models.MenuMaster;

namespace HISWebApi.Repository.MenuMaster
{
	public class MenuMaster
	{
		public dataSet MenuMasterQueries(GroupItems objBO)
		{
			string processInfo = string.Empty;
			dataSet dsObj = new dataSet();
			using (SqlConnection con = new SqlConnection(GlobalConfig.ConnectionString))
			{
				using (SqlCommand cmd = new SqlCommand("pHIS_MenuMasterQueries", con))
				{
					cmd.CommandType = CommandType.StoredProcedure;
					cmd.CommandTimeout = 2500;
					cmd.Parameters.Add("@GroupId", SqlDbType.VarChar, 20).Value = objBO.GroupId;
					cmd.Parameters.Add("@GroupName", SqlDbType.VarChar, 50).Value = objBO.GroupName;
					cmd.Parameters.Add("@GroupFor", SqlDbType.VarChar, 50).Value = objBO.GroupFor;
					cmd.Parameters.Add("@GroupItem", SqlDbType.VarChar, 50).Value = objBO.GroupItemId;
					cmd.Parameters.Add("@Prm1", SqlDbType.VarChar, 50).Value = objBO.Prm1;
					cmd.Parameters.Add("@Prm2", SqlDbType.VarChar, 50).Value = objBO.Prm2;
					cmd.Parameters.Add("@IsActive", SqlDbType.Int, 1).Value = objBO.IsActive;
					cmd.Parameters.Add("@Logic", SqlDbType.VarChar, 50).Value = objBO.Logic;
					try
					{
						con.Open();
						DataSet ds = new DataSet();
						SqlDataAdapter da = new SqlDataAdapter(cmd);
						da.Fill(ds);
						dsObj.ResultSet = ds;						
					}
					catch (SqlException sqlEx)
					{
						dsObj.ResultSet = null;
						dsObj.Msg = "Error Found   : " + sqlEx.Message;
					}
					finally { con.Close(); }
					return dsObj;
				}
			}
		}
		public string InsertModifyMenuMaster(GroupMaster objBO)
		{
			string processInfo = string.Empty;
			using (SqlConnection con = new SqlConnection(GlobalConfig.ConnectionString))
			{
				using (SqlCommand cmd = new SqlCommand("pHIS_InsertModifyMenuMaster", con))
				{
					cmd.CommandType = CommandType.StoredProcedure;
					cmd.CommandTimeout = 2500;
					cmd.Parameters.Add("@AutoId", SqlDbType.Int, 20).Value = objBO.AutoId;					
					cmd.Parameters.Add("@GroupId", SqlDbType.VarChar, 20).Value = objBO.GroupId;					
					cmd.Parameters.Add("@GroupName", SqlDbType.VarChar, 50).Value = objBO.GroupName;					
					cmd.Parameters.Add("@GroupFor", SqlDbType.VarChar, 50).Value = objBO.GroupFor;					
					cmd.Parameters.Add("@GroupItemId", SqlDbType.VarChar,20).Value = objBO.GroupItemId;													
					cmd.Parameters.Add("@DeleteMenu", SqlDbType.VarChar,20).Value = objBO.GroupItemId;					
					cmd.Parameters.Add("@IsActive", SqlDbType.Int, 1).Value = objBO.IsActive;					
					cmd.Parameters.Add("@Logic", SqlDbType.VarChar, 50).Value = objBO.Logic;					
					cmd.Parameters.Add("@result", SqlDbType.VarChar, 100).Value = "";
					cmd.Parameters["@result"].Direction = ParameterDirection.InputOutput;
					try
					{
						con.Open();
						cmd.ExecuteNonQuery();
						processInfo = (string)cmd.Parameters["@result"].Value.ToString();
					}
					catch (SqlException sqlEx)
					{
						processInfo = "Error Found   : " + sqlEx.Message;
					}
					finally { con.Close(); }
					return processInfo;
				}
			}
		}
		public string AllotMenuToGroup(List<GroupItems> objBO)
		{
			string processInfo = string.Empty;
			if (objBO.Count > 0)
			{
				try
				{
					DataTable dt = new DataTable();
					dt.Columns.Add("GroupId", typeof(string));
					dt.Columns.Add("GroupItemId", typeof(string));
					foreach (GroupItems obj in objBO)
					{
						DataRow dr = dt.NewRow();
						dr["GroupId"] = obj.GroupId;
						dr["GroupItemId"] = obj.GroupItemId;
						dt.Rows.Add(dr);
					}
					using (SqlConnection con = new SqlConnection(GlobalConfig.ConnectionString))
					{
						using (SqlCommand cmd = new SqlCommand("pHIS_InsertModifyMenuMaster", con))
						{
							cmd.CommandType = CommandType.StoredProcedure;
							cmd.CommandTimeout = 2500;
							cmd.Parameters.AddWithValue("@GroupMenuAllot", dt);
							cmd.Parameters.AddWithValue("@Logic", "GroupMenuAllot");
							cmd.Parameters.Add("@result", SqlDbType.VarChar, 50).Value = "";
							cmd.Parameters["@result"].Direction = ParameterDirection.InputOutput;
							try
							{
								con.Open();
								int i = cmd.ExecuteNonQuery();
								processInfo = (string)cmd.Parameters["@result"].Value.ToString();
							}
							catch (SqlException sqlEx)
							{
								processInfo = "Error Found   : " + sqlEx.Message;
							}
							finally { con.Close(); }
							return processInfo;
						}
					}
				}
				catch (Exception ex) { processInfo = ex.Message; }
			}
			return processInfo;
		}
	}
}