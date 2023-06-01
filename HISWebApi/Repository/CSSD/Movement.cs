using HISWebApi.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using static HISWebApi.Models.CSSD;

namespace HISWebApi.Repository.CSSD
{
	public class Movement
	{
		public dataSet CSSD_MovementQueries(ipCSSD objBO)
		{
			dataSet objDS = new dataSet();
			using (SqlConnection con = new SqlConnection(GlobalConfig.ConStr_Hospital))
			{
				using (SqlCommand cmd = new SqlCommand("pCSSD_MovementQueries", con))
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
						objDS.Msg = "Success";
						objDS.ResultSet = ds;
						con.Close();
					}
					catch (Exception sqlEx)
					{
						objDS.ResultSet = null;
						objDS.Msg = sqlEx.Message;
					}
					finally { con.Close(); }
					return objDS;
				}
			}
		}
		public string CSSD_InsertUpdateMovement(MovementInfo objBO)
		{
			string processInfo = string.Empty;
			using (SqlConnection con = new SqlConnection(GlobalConfig.ConnectionString))
			{
				using (SqlCommand cmd = new SqlCommand("pCSSD_InsertUpdateMovement", con))
				{
					cmd.CommandType = CommandType.StoredProcedure;
					cmd.CommandTimeout = 2500;
					cmd.Parameters.Add("@AutoId", SqlDbType.Int, 5).Value = objBO.AutoId;
					cmd.Parameters.Add("@CartdId", SqlDbType.VarChar, 10).Value = objBO.CartdId;
					cmd.Parameters.Add("@IndentNo", SqlDbType.VarChar, 16).Value = objBO.IndentNo;
					cmd.Parameters.Add("@BatchNo", SqlDbType.VarChar, 20).Value = objBO.BatchNo;
					cmd.Parameters.Add("@SetId", SqlDbType.VarChar, 10).Value = objBO.SetId;
					cmd.Parameters.Add("@ItemId", SqlDbType.VarChar, 10).Value = objBO.ItemId;
					cmd.Parameters.Add("@qty", SqlDbType.Int, 5).Value = objBO.qty;
					cmd.Parameters.Add("@Remark", SqlDbType.VarChar, 100).Value = objBO.Remark;
					cmd.Parameters.Add("@Prm1", SqlDbType.VarChar, 50).Value = objBO.Prm_1;
					cmd.Parameters.Add("@Prm2", SqlDbType.VarChar, 50).Value = objBO.Prm_2;
					cmd.Parameters.Add("@login_id", SqlDbType.VarChar, 10).Value = objBO.login_id;
					cmd.Parameters.Add("@ExpDate", SqlDbType.DateTime, 12).Value = objBO.expDate;
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
		public string CSSD_ItemDispatchAndReceive(List<MovementInfo> objBO)
		{
			string hosp_id = string.Empty;
			string Prm_1 = string.Empty;
			string Prm_2 = string.Empty;
			string trn_no = string.Empty;
			string IPDNo = string.Empty;
			string login_id = string.Empty;
			string Logic = string.Empty;
			string ProcessInfo = string.Empty;
			if (objBO.Count > 0)
			{
				DataTable dt = new DataTable();
				dt.Columns.Add("trn_no", typeof(string));
				dt.Columns.Add("from_cart", typeof(string));
				dt.Columns.Add("to_cart", typeof(string));
				dt.Columns.Add("SetId", typeof(string));
				dt.Columns.Add("batch_no", typeof(string));
				dt.Columns.Add("exp_date", typeof(DateTime));
				dt.Columns.Add("SetKey", typeof(string));
				dt.Columns.Add("Qty", typeof(int));
				dt.Columns.Add("ItemType", typeof(string));
				dt.Columns.Add("remark", typeof(string));
				foreach (MovementInfo obj in objBO)
				{
					trn_no = obj.TrnNo;
					IPDNo = obj.IPDNo;
					hosp_id = obj.hosp_id;
					Prm_1 = obj.Prm_1;
					Prm_2 = obj.Prm_2;
					login_id = obj.login_id;
					Logic = obj.Logic;
					DataRow dr = dt.NewRow();
					dr["trn_no"] = obj.TrnNo;
					dr["from_cart"] = obj.FromCart;
					dr["to_cart"] = obj.ToCart;
					dr["SetId"] = obj.SetId;
					dr["batch_no"] = obj.BatchNo;
					dr["exp_date"] = obj.expDate;
					dr["SetKey"] = obj.SetKey;
					dr["Qty"] = obj.qty;
					dr["ItemType"] = obj.ItemType;
					dr["remark"] = obj.Remark;
					dt.Rows.Add(dr);
				}
				using (SqlConnection con = new SqlConnection(GlobalConfig.ConStr_Hospital))
				{
					using (SqlCommand cmd = new SqlCommand("pCSSD_ItemDispatchAndReceive", con))
					{
						cmd.CommandType = CommandType.StoredProcedure;
						cmd.CommandTimeout = 2500;
						cmd.Parameters.AddWithValue("udt_CSSDSetMovement", dt);
						cmd.Parameters.Add("@hosp_id", SqlDbType.VarChar, 10).Value = hosp_id;
						cmd.Parameters.Add("@trn_no", SqlDbType.VarChar, 20).Value = trn_no;
						cmd.Parameters.Add("@IPDNo", SqlDbType.VarChar, 20).Value = IPDNo;
						cmd.Parameters.Add("@Prm_1", SqlDbType.VarChar,100).Value = Prm_1;
						cmd.Parameters.Add("@Prm_2", SqlDbType.VarChar,100).Value = Prm_2;
						cmd.Parameters.Add("@login_id", SqlDbType.VarChar, 10).Value = login_id;
						cmd.Parameters.Add("@Logic", SqlDbType.VarChar, 50).Value = Logic;
						cmd.Parameters.Add("@result", SqlDbType.VarChar, 50).Value = "";
						cmd.Parameters["@result"].Direction = ParameterDirection.InputOutput;
						try
						{
							con.Open();
							cmd.ExecuteNonQuery();
							ProcessInfo = (string)cmd.Parameters["@result"].Value.ToString();
							con.Close();
						}
						catch (SqlException ex)
						{
							ProcessInfo = ex.Message;
						}
						finally { con.Close(); }
					}
				}
			}
			return ProcessInfo;
		}
	}
}