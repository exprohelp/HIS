using HISWebApi.Models;
using System;
using System.Data;
using System.Data.SqlClient;
using static HISWebApi.Models.Warehouse;

namespace HISWebApi.Repository.Warehouse
{
	public class Report
	{
		public dataSet wh_ReportQueries(ReportBO objBO)
		{
			dataSet dsObj = new dataSet();
			using (SqlConnection con = new SqlConnection(GlobalConfig.ConStr_Hospital))
			{
				using (SqlCommand cmd = new SqlCommand("pwh_ReportQueries", con))
				{
					cmd.CommandType = CommandType.StoredProcedure;
					cmd.CommandTimeout = 2500;
					cmd.Parameters.Add("@from_cart", SqlDbType.VarChar, 50).Value = objBO.from_cart;
					cmd.Parameters.Add("@to_cart", SqlDbType.VarChar, 50).Value = objBO.to_cart;
					cmd.Parameters.Add("@from", SqlDbType.DateTime, 20).Value = objBO.from;
					cmd.Parameters.Add("@to", SqlDbType.DateTime, 20).Value = objBO.to;
					cmd.Parameters.Add("@ItemId", SqlDbType.VarChar, 200).Value = objBO.ItemId;
					cmd.Parameters.Add("@ReportType", SqlDbType.VarChar, 50).Value = objBO.ReportType;
					cmd.Parameters.Add("@prm_1", SqlDbType.VarChar, 200).Value = objBO.prm_1;
					cmd.Parameters.Add("@login_id", SqlDbType.VarChar, 200).Value = objBO.login_id;
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
		public dataSet wh_MiscellaneousReportQueries(ReportBO objBO)
		{
			dataSet dsObj = new dataSet();
			using (SqlConnection con = new SqlConnection(GlobalConfig.ConStr_Hospital))
			{
				using (SqlCommand cmd = new SqlCommand("pwh_MiscellaneousReportQueries", con))
				{
					cmd.CommandType = CommandType.StoredProcedure;
					cmd.CommandTimeout = 2500;
					cmd.Parameters.Add("@prm_1", SqlDbType.VarChar, 50).Value = objBO.prm_1;
					cmd.Parameters.Add("@login_id", SqlDbType.VarChar, 50).Value = objBO.login_id;
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
		public dataSet wh_PurchaseReportQueries(ReportBO objBO)
		{
			dataSet dsObj = new dataSet();
			using (SqlConnection con = new SqlConnection(GlobalConfig.ConStr_Hospital))
			{
				using (SqlCommand cmd = new SqlCommand(objBO.ProcName, con))
				{
					cmd.CommandType = CommandType.StoredProcedure;
					cmd.CommandTimeout = 2500;
					cmd.Parameters.Add("@from", SqlDbType.DateTime, 15).Value = objBO.from;
					cmd.Parameters.Add("@to", SqlDbType.DateTime, 15).Value = objBO.to;
					cmd.Parameters.Add("@CartId", SqlDbType.VarChar, 20).Value = objBO.CartId;
					cmd.Parameters.Add("@CategoryID", SqlDbType.VarChar, 20).Value = objBO.Category;
					cmd.Parameters.Add("@vendorID", SqlDbType.VarChar,50).Value = objBO.vendorID;
					cmd.Parameters.Add("@ItemIds", SqlDbType.VarChar, 500).Value = objBO.ItemIds;
					cmd.Parameters.Add("@prm_1", SqlDbType.VarChar, 50).Value = objBO.prm_1;									
					cmd.Parameters.Add("@login_id", SqlDbType.VarChar, 15).Value = objBO.login_id;
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
      
    }
}