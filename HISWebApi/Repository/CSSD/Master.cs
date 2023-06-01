using HISWebApi.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using static HISWebApi.Models.CSSD;

namespace HISWebApi.Repository.CSSD
{
	public class Master
	{
		public dataSet CSSD_MasterQueries(ipCSSD objBO)
		{
			dataSet dsObj = new dataSet();
			using (SqlConnection con = new SqlConnection(GlobalConfig.ConnectionString))
			{
				using (SqlCommand cmd = new SqlCommand("pCSSD_MasterQueries", con))
				{
					cmd.CommandType = CommandType.StoredProcedure;
					cmd.CommandTimeout = 2500;
					cmd.Parameters.Add("@ItemId", SqlDbType.VarChar, 10).Value = objBO.ItemId;				
					cmd.Parameters.Add("@Prm_1", SqlDbType.VarChar, 50).Value = objBO.Prm_1;
					cmd.Parameters.Add("@Prm_2", SqlDbType.VarChar, 50).Value = objBO.Prm_2;
					cmd.Parameters.Add("@login_id", SqlDbType.VarChar, 10).Value = objBO.login_id;				
					cmd.Parameters.Add("@Logic", SqlDbType.VarChar, 50).Value = objBO.Logic;				
					try
					{
						con.Open();
						DataSet ds = new DataSet();
						SqlDataAdapter da = new SqlDataAdapter(cmd);
						da.Fill(ds);
						dsObj.ResultSet = ds;
						dsObj.Msg = "Success";
						con.Close();
					}
					catch (SqlException sqlEx)
					{
						dsObj.ResultSet = null;
						dsObj.Msg = sqlEx.Message;
					}
					finally { con.Close(); }
					return dsObj;
				}
			}
		}
		public string CSSD_InsertUpdateItemMaster(ItemInfo objBO)
		{
			dataSet dsObj = new dataSet();
			string processInfo = string.Empty;
			using (SqlConnection con = new SqlConnection(GlobalConfig.ConnectionString))
			{
				using (SqlCommand cmd = new SqlCommand("pCSSD_InsertUpdateItemMaster", con))
				{
					cmd.CommandType = CommandType.StoredProcedure;
					cmd.CommandTimeout = 2500;
					cmd.Parameters.Add("@hosp_id", SqlDbType.VarChar,10).Value = objBO.hosp_id;
					cmd.Parameters.Add("@ItemId", SqlDbType.VarChar, 10).Value = objBO.ItemId;
					cmd.Parameters.Add("@ItemType", SqlDbType.VarChar, 10).Value = objBO.ItemType;
					cmd.Parameters.Add("@ItemName", SqlDbType.VarChar, 200).Value = objBO.ItemName;
					cmd.Parameters.Add("@SterileType", SqlDbType.VarChar, 50).Value = objBO.SterileType;
					cmd.Parameters.Add("@wh_ItemId", SqlDbType.VarChar, 10).Value = objBO.wh_ItemId;
					cmd.Parameters.Add("@ItemCategory", SqlDbType.VarChar, 50).Value = objBO.ItemCategory;
					cmd.Parameters.Add("@IsActive", SqlDbType.Int, 1).Value = objBO.IsActive;
					cmd.Parameters.Add("@login_id", SqlDbType.VarChar, 10).Value = objBO.login_id;
					cmd.Parameters.Add("@Logic", SqlDbType.VarChar, 50).Value = objBO.Logic;
					cmd.Parameters.Add("@result", SqlDbType.VarChar, 100).Value = "";
					cmd.Parameters["@result"].Direction = ParameterDirection.InputOutput;
					try
					{
						con.Open();
						cmd.ExecuteNonQuery();
						processInfo = (string)cmd.Parameters["@result"].Value.ToString();
						con.Close();
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
		public string CSSD_InsertUpdateSetMaster(SetMasterInfo objBO)
		{
			dataSet dsObj = new dataSet();
			string processInfo = string.Empty;
			using (SqlConnection con = new SqlConnection(GlobalConfig.ConnectionString))
			{
				using (SqlCommand cmd = new SqlCommand("pCSSD_InsertUpdateSetMaster", con))
				{
					cmd.CommandType = CommandType.StoredProcedure;
					cmd.CommandTimeout = 2500;
					cmd.Parameters.Add("@AutoId", SqlDbType.Int, 10).Value = objBO.AutoId;
					cmd.Parameters.Add("@DeptName", SqlDbType.VarChar, 100).Value = objBO.DeptName;
					cmd.Parameters.Add("@ItemId", SqlDbType.VarChar, 10).Value = objBO.ItemId;
					cmd.Parameters.Add("@SetId", SqlDbType.VarChar, 10).Value = objBO.SetId;
					cmd.Parameters.Add("@SetName", SqlDbType.VarChar, 200).Value = objBO.SetName;
					cmd.Parameters.Add("@ItemType", SqlDbType.VarChar, 10).Value = objBO.ItemType;
					cmd.Parameters.Add("@expiryDays", SqlDbType.Int, 3).Value = objBO.expiryDays;
					cmd.Parameters.Add("@nos", SqlDbType.Int, 10).Value = objBO.nos;
					cmd.Parameters.Add("@description", SqlDbType.VarChar, 500).Value = objBO.description;					
					cmd.Parameters.Add("@IsActive", SqlDbType.Int, 1).Value = objBO.IsActive;
					cmd.Parameters.Add("@login_id", SqlDbType.VarChar, 10).Value = objBO.login_id;
					cmd.Parameters.Add("@Logic", SqlDbType.VarChar, 50).Value = objBO.Logic;
					cmd.Parameters.Add("@result", SqlDbType.VarChar, 100).Value = "";
					cmd.Parameters["@result"].Direction = ParameterDirection.InputOutput;
					try
					{
						con.Open();
						cmd.ExecuteNonQuery();
						processInfo = (string)cmd.Parameters["@result"].Value.ToString();
						con.Close();
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
	}
}