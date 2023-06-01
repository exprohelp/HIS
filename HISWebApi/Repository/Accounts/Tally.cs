using HISWebApi.Models;
using HISWebApi.Repository;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Web;
using System.Web.Http;

namespace HISWebApi.Repository.Accounts
{
	public class Tally
	{
        TallyServerProxy.TallyAdapterServiceClient tsp = new TallyServerProxy.TallyAdapterServiceClient();
		public dataSet AC_ViewLedgerInfo(ipAccounts ipAcc)
		{
			dataSet dsObj = new dataSet();
			using (SqlConnection con = new SqlConnection(GlobalConfig.ConStr_Hospital))
			{
				using (SqlCommand cmd = new SqlCommand("pAC_ViewLedgerInfo", con))
				{
					cmd.CommandType = CommandType.StoredProcedure;
					cmd.CommandTimeout = 2500;
					cmd.Parameters.Add("@hosp_id", SqlDbType.VarChar, 200).Value = ipAcc.UnitId;
					cmd.Parameters.Add("@ledgerId", SqlDbType.VarChar, 25).Value = ipAcc.ledgerId;
					cmd.Parameters.Add("@From", SqlDbType.DateTime).Value = ipAcc.From;
					cmd.Parameters.Add("@to", SqlDbType.DateTime).Value = ipAcc.To;
					cmd.Parameters.Add("@login_id", SqlDbType.VarChar, 15).Value = ipAcc.LoginId;
					cmd.Parameters.Add("@voucher_no", SqlDbType.VarChar, 16).Value = ipAcc.VoucherNo;
					cmd.Parameters.Add("@Logic", SqlDbType.VarChar, 50).Value = ipAcc.Logic;
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
		public string VoucherForSync(ipAccounts ipAcc)
		{
            string result = string.Empty;
            using (SqlConnection con = new SqlConnection(GlobalConfig.ConStr_Hospital))
			{
				using (SqlCommand cmd = new SqlCommand("pAC_ViewLedgerInfo", con))
				{
					cmd.CommandType = CommandType.StoredProcedure;
					cmd.CommandTimeout = 2500;
					cmd.Parameters.Add("@hosp_id", SqlDbType.VarChar, 200).Value = ipAcc.UnitId;
					cmd.Parameters.Add("@ledgerId", SqlDbType.VarChar, 25).Value = ipAcc.ledgerId;
					cmd.Parameters.Add("@From", SqlDbType.DateTime).Value = ipAcc.From;
					cmd.Parameters.Add("@to", SqlDbType.DateTime).Value = ipAcc.To;
					cmd.Parameters.Add("@login_id", SqlDbType.VarChar, 15).Value = ipAcc.LoginId;
					cmd.Parameters.Add("@voucher_no", SqlDbType.VarChar, 16).Value = ipAcc.VoucherNo;
					cmd.Parameters.Add("@Logic", SqlDbType.VarChar, 50).Value = ipAcc.Logic;
					try
					{
						con.Open();
						DataSet ds = new DataSet();
						SqlDataAdapter da = new SqlDataAdapter(cmd);
						da.Fill(ds);
                        List<TallyServerProxy.VoucherDataVertical> vList = new List<TallyServerProxy.VoucherDataVertical>();
                        if(ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                        {
                            string vchType = string.Empty;
                            foreach (DataRow dr in ds.Tables[0].Rows)
                            {
                                TallyServerProxy.VoucherDataVertical l = new TallyServerProxy.VoucherDataVertical();
                                l.vchDate = dr["vchdate"].ToString();
                                l.vchType = dr["vch_type"].ToString();
                                l.VoucherNo = dr["voucher_no"].ToString();
                                l.billNo = dr["billNo"].ToString();
                                l.billType = dr["BillType"].ToString();
                                l.Company = dr["NameInTally"].ToString();
                                l.Narration = dr["narration"].ToString().Replace("&", "&amp;");
                                l.ledgername = dr["ledgername"].ToString().Replace("&", "&amp;");
                                l.Amount = Convert.ToDecimal(dr["amount"]);
                                l.SeqNo = Convert.ToInt16(dr["SeqNo"]);
                                vchType= dr["vch_type"].ToString(); 
                                vList.Add(l);
                            }
                            result=tsp.Tally_VoucherEntryNew(vList, vchType);
                            if(result.Contains("Success"))
                            {
                                string qry = "update Ac_Vouchers set IsTallySync='Y' where voucher_no='"+ ipAcc.VoucherNo + "';";
                                DBManager.QueryExecute(qry,GlobalConfig.ConStr_Hospital);
                            }
                        }
       
						con.Close();
					}
					catch (SqlException sqlEx)
					{
                        result = sqlEx.Message;

                    }
					finally { con.Close(); }
					return result;
				}
			}
		}
	}
}