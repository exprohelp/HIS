using HISWebApi.Models;
using System.Data;
using System.Data.SqlClient;

namespace HISWebApi.Repository.Accounts
{
	public class Master
	{
		public dataSet AC_AccountMasterQueries(GroupMasterBO objBO)
		{
			dataSet dsObj = new dataSet();
			using (SqlConnection con = new SqlConnection(GlobalConfig.ConStr_Hospital))
			{
				using (SqlCommand cmd = new SqlCommand("pAC_AccountMasterQueries", con))
				{
					cmd.CommandType = CommandType.StoredProcedure;
					cmd.CommandTimeout = 2500;
					cmd.Parameters.Add("@groupID", SqlDbType.VarChar, 10).Value = objBO.groupID;		
					cmd.Parameters.Add("@under_GroupID", SqlDbType.VarChar, 50).Value = objBO.under_GroupID;
					cmd.Parameters.Add("@prm_1", SqlDbType.VarChar, 50).Value = objBO.prm_1;
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
		public string AC_CreatePrimaryGroups(GroupMasterBO objBO)
		{
			string processInfo = string.Empty;
			using (SqlConnection con = new SqlConnection(GlobalConfig.ConStr_Hospital))
			{
				con.Open();
				using (SqlCommand cmd = new SqlCommand("pAC_CreatePrimaryGroups", con))
				{
					cmd.CommandType = CommandType.StoredProcedure;
					cmd.CommandTimeout = 2500;
					cmd.Parameters.Add("@groupID", SqlDbType.VarChar, 16).Value = objBO.groupID;
					cmd.Parameters.Add("@display_name", SqlDbType.VarChar, 100).Value = objBO.display_name;
					cmd.Parameters.Add("@under_GroupID", SqlDbType.VarChar, 16).Value = objBO.under_GroupID;
					cmd.Parameters.Add("@Logic", SqlDbType.VarChar, 500).Value = objBO.Logic;
					cmd.Parameters.Add("@result", SqlDbType.VarChar, 200).Value = "";
					cmd.Parameters["@result"].Direction = ParameterDirection.InputOutput;
					try
					{
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
		public string AC_CreateLedgers(LedgerMasterBO objBO)
		{
			string processInfo = string.Empty;
			using (SqlConnection con = new SqlConnection(GlobalConfig.ConStr_Hospital))
			{
				con.Open();
				using (SqlCommand cmd = new SqlCommand("pAC_CreateLedgers", con))
				{
					cmd.CommandType = CommandType.StoredProcedure;
					cmd.CommandTimeout = 2500;
					cmd.Parameters.Add("@ledgerID", SqlDbType.VarChar, 10).Value = objBO.ledgerID;
					cmd.Parameters.Add("@unit_id", SqlDbType.VarChar, 7).Value = objBO.unit_id;
					cmd.Parameters.Add("@ledger_name", SqlDbType.VarChar, 50).Value = objBO.ledger_name;
					cmd.Parameters.Add("@subGroupID", SqlDbType.BigInt).Value = objBO.subGroupID;
					cmd.Parameters.Add("@contact_person", SqlDbType.VarChar, 100).Value = objBO.contact_person;
					cmd.Parameters.Add("@externalName", SqlDbType.VarChar, 100).Value = objBO.externalName;
					cmd.Parameters.Add("@party_address", SqlDbType.VarChar, 100).Value = objBO.party_address;
					cmd.Parameters.Add("@openAmt", SqlDbType.Decimal).Value = objBO.openAmt;
					cmd.Parameters.Add("@panNo", SqlDbType.VarChar, 100).Value = objBO.panNo;
					cmd.Parameters.Add("@gst_no", SqlDbType.VarChar, 100).Value = objBO.gst_no;
					cmd.Parameters.Add("@Logic", SqlDbType.VarChar, 50).Value = objBO.Logic;
					cmd.Parameters.Add("@result", SqlDbType.VarChar, 200).Value = "";
					cmd.Parameters["@result"].Direction = ParameterDirection.InputOutput;
					try
					{
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
	}
}