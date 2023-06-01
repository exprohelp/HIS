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

namespace MISWebApi.Repository.Accounts
{
    public class Account
    {
        public PaymentInfo[] BindOnloadData([FromBody] ipAccounts ipAcc)
        {
            List<PaymentInfo> prpArr = new List<PaymentInfo>();
            string qry = string.Empty;
            try
            {
                using (SqlConnection con = new SqlConnection(GlobalConfig.ConStr_Hospital))
                {
                    using (SqlCommand cmd = new SqlCommand("pAC_GetVoucherSummary", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandTimeout = 2500;
                        cmd.Parameters.Add("@unit_id", SqlDbType.VarChar, 50).Value = ipAcc.UnitId;
                        cmd.Parameters.Add("@voucher_no", SqlDbType.VarChar, 50).Value = ipAcc.VoucherNo;
                        cmd.Parameters.Add("@vch_type", SqlDbType.VarChar, 50).Value = ipAcc.VchType;
                        cmd.Parameters.Add("@vch_date", SqlDbType.DateTime).Value=DateTime.Now.ToString("yyyy/MM/dd");
                        cmd.Parameters.Add("@login_id", SqlDbType.VarChar, 50).Value = ipAcc.LoginId;
                        cmd.Parameters.Add("@Logic", SqlDbType.VarChar, 50).Value = ipAcc.Logic;
                        try
                        {
                            con.Open();
                            DataSet ds = new DataSet();
                            SqlDataAdapter da = new SqlDataAdapter(cmd);
                            da.Fill(ds);
                            foreach (DataRow dr in ds.Tables[0].Rows)
                            {
                                PaymentInfo en = new PaymentInfo();
                                en.PayTo_Code = dr["payto"].ToString();
                                en.PayBy_Code = dr["payby"].ToString();
                                en.DrCrAmount = Convert.ToDecimal(dr["amount"]).ToString("0.##");
                                en.Narration = dr["narration"].ToString();
                                prpArr.Add(en);
                            }
                            con.Close();

                        }
                        catch (SqlException sqlEx)
                        {
                        }
                        finally { con.Close(); }
                   
                    }
                }
                return prpArr.ToArray();
            }
            catch (Exception ex)
            {
                return prpArr.ToArray();
            }
        }
        public PayToPayByInfo[] Bind_PayTo_PayBy([FromBody] ipAccounts ipAcc)
        {
            List<PayToPayByInfo> prpArr = new List<PayToPayByInfo>();
            string qry = string.Empty;
            try
            {
                using (SqlConnection con = new SqlConnection(GlobalConfig.ConStr_Hospital))
                {
                    using (SqlCommand cmd = new SqlCommand("pAC_GetLedgerInfoForEntry", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandTimeout = 2500;
                        cmd.Parameters.Add("@unit_id", SqlDbType.VarChar, 50).Value = ipAcc.UnitId;
                        cmd.Parameters.Add("@vch_type", SqlDbType.VarChar, 50).Value = ipAcc.VchType;
                        cmd.Parameters.Add("@ledgerfor", SqlDbType.VarChar).Value = ipAcc.Prm1;
                        try
                        {
                            con.Open();
                            DataSet ds = new DataSet();
                            SqlDataAdapter da = new SqlDataAdapter(cmd);
                            da.Fill(ds);
                            foreach (DataRow dr in ds.Tables[0].Rows)
                            {
                                PayToPayByInfo en = new PayToPayByInfo();
                                en.Code = dr["ld_code"].ToString();
                                en.Name = dr["ld_name"].ToString();
                                prpArr.Add(en);
                            }
                            con.Close();

                        }
                        catch (SqlException sqlEx)
                        {
                        }
                        finally { con.Close(); }

                    }
                }
                return prpArr.ToArray();
            }
            catch (Exception ex)
            {
                return prpArr.ToArray();
            }
        }
        public PaymentInfo[] SubmitPayment([FromBody] PaymentInfo objPay)
        {
            List<PaymentInfo> prpArr = new List<PaymentInfo>();
            string qry = string.Empty;
            try
            {
                using (SqlConnection con = new SqlConnection(GlobalConfig.ConStr_Hospital))
                {
                    using (SqlCommand cmd = new SqlCommand("pAC_VoucherPosting", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandTimeout = 2500;
                        cmd.Parameters.Add("@unit_id", SqlDbType.VarChar, 10).Value = objPay.UnitId;
                        cmd.Parameters.Add("@vch_type", SqlDbType.VarChar, 10).Value = objPay.VchType;
                        cmd.Parameters.Add("@vch_date", SqlDbType.DateTime).Value = objPay.VchDate;
                        cmd.Parameters.Add("@cr_Ledger", SqlDbType.VarChar, 100).Value = objPay.PayBy_Code;
                        cmd.Parameters.Add("@dr_ledger", SqlDbType.VarChar, 100).Value = objPay.PayTo_Code;
                        cmd.Parameters.Add("@amount", SqlDbType.BigInt).Value = objPay.DrCrAmount;
                        cmd.Parameters.Add("@narration", SqlDbType.VarChar, 150).Value =objPay.Narration;
                        cmd.Parameters.Add("@login_id", SqlDbType.VarChar, 10).Value = objPay.LoginId;
                        cmd.Parameters.Add("@logic", SqlDbType.VarChar, 50).Value = objPay.Logic;
                        cmd.Parameters.Add("@genFrom", SqlDbType.VarChar, 10).Value = objPay.GenFrom;
                        cmd.Parameters.Add("@division", SqlDbType.VarChar, 15).Value =objPay.Division;
                        cmd.Parameters.Add("@voucher_no", SqlDbType.VarChar, 16).Value = objPay.VoucherNo;
                        cmd.Parameters["@voucher_no"].Direction = ParameterDirection.InputOutput;
                        try
                        {
                            con.Open();
                            DataSet ds = new DataSet();
                            SqlDataAdapter da = new SqlDataAdapter(cmd);
                            da.Fill(ds);
                            foreach (DataRow dr in ds.Tables[0].Rows)
                            {
                                PaymentInfo en = new PaymentInfo();
                                en.PayTo_Code = dr["payto"].ToString();
                                en.PayBy_Code = dr["payby"].ToString();
                                en.DrCrAmount = dr["amount"].ToString();
                                en.Narration = dr["narration"].ToString();
                                prpArr.Add(en);
                            }
                            con.Close();
                        }
                        catch (SqlException sqlEx)
                        {

                        }
                        finally { con.Close(); }

                    }
                }
                return prpArr.ToArray();
            }
            catch (Exception ex)
            {
                return prpArr.ToArray();
            }
        }
        public dataSet SearchLedger(ipAccounts ipAcc)
        {
            string qry = string.Empty;
            dataSet dsObj = new dataSet();
            try
            {
                using (SqlConnection con = new SqlConnection(GlobalConfig.ConStr_Hospital))
                {
                    using (SqlCommand cmd = new SqlCommand("pAc_LedgerQueries", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandTimeout = 2500;
                        cmd.Parameters.Add("@hosp_id", SqlDbType.VarChar, 100).Value =ipAcc.UnitId;
                        cmd.Parameters.Add("@ledgerId", SqlDbType.VarChar, 100).Value = ipAcc.ledgerId;
                        cmd.Parameters.Add("@prm_1", SqlDbType.VarChar, 50).Value = ipAcc.Prm1;
                        cmd.Parameters.Add("@from", SqlDbType.VarChar, 50).Value = ipAcc.From;
                        cmd.Parameters.Add("@to", SqlDbType.VarChar, 50).Value = ipAcc.To;
                        cmd.Parameters.Add("@logic", SqlDbType.VarChar, 50).Value = ipAcc.Logic;
                        cmd.Parameters.Add("@login_id", SqlDbType.VarChar, 10).Value = ipAcc.LoginId;
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
                            dsObj.Msg =sqlEx.Message;
                        }
                        finally { con.Close(); }

                    }
                }
                return dsObj;
            }
            catch (Exception ex)
            {
                return dsObj;
            }
        }
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
                    cmd.Parameters.Add("@ledgerId", SqlDbType.VarChar,25).Value = ipAcc.ledgerId;
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
        public string  LedgerBooksInfo(ipAccounts ipAcc)
        {
            dataSet ds = AC_ViewLedgerInfo(ipAcc);

            string _result = string.Empty;
            StringBuilder sb = new StringBuilder();
            sb.Append("<table class='table table-bordered'>");
            decimal debitAmt = 0;
            decimal CreditAmt = 0;
            decimal opening_debit = 0;
            decimal opening_credit = 0;

            decimal closing_debit = 0;
            decimal closing_credit = 0;

            if (ds.ResultSet.Tables.Count > 0 && ds.ResultSet.Tables[0].Rows.Count > 0)
            {
                string temp = string.Empty;
                var openingList = ds.ResultSet.Tables[0].AsEnumerable().Where(x => x.Field<string>("vch_type").ToUpper() == "Opening").Select(y => new { opening_debit = y.Field<decimal>("debitAmt"), opening_credit = y.Field<decimal>("CreditAmt") });
                if (openingList.Count() > 0)
                {
                    opening_debit = openingList.First().opening_debit;
                    opening_credit = openingList.First().opening_credit;
                }
                foreach (DataRow dr in ds.ResultSet.Tables[0].Rows)
                {
                    if (dr["vch_type"].ToString() != "Opening")
                    {
                        sb.Append("<tr style='font-size:10pt'>");
                        sb.Append("<td style='text-align:left;padding:1px; margin:1px;width:5%'><input type='button' id='" + dr["voucher_no"].ToString() + "' value='Detail' class='btn btn-info btn-block' onclick='showVoucherDetail(this.id);' /></td>");
                        sb.Append("<td style='text-align:left;padding:1px; margin:1px;width:10%'> " + dr["vch_type"].ToString() + " </td>");
                        sb.Append("<td style='text-align:left;padding:1px; margin:1px;width:10%'> " + Convert.ToDateTime(dr["vch_date"]).ToString("dd-MM-yyyy") + " </td>");
                        sb.Append("<td style='text-align:left;padding:1px; margin:1px;width:55%'> <b style='color:green;font-size:11px'> " + dr["Particular"].ToString() + "</b><br/><i style='color:black;font-size:10px'>" + dr["narration"].ToString() + " </i> </td>");
                        sb.Append("<td style='text-align:right;padding:1px; margin:1px;width:10%'>  " + Convert.ToDecimal(dr["debitAmt"]).ToString() + " </td>");
                        sb.Append("<td style='text-align:right;padding:1px; margin:1px;width:10%'>  " + Convert.ToDecimal(dr["CreditAmt"]).ToString() + " </td>");
                        sb.Append("</tr>");
                        debitAmt = debitAmt + Convert.ToDecimal(dr["debitAmt"]);
                        CreditAmt = CreditAmt + Convert.ToDecimal(dr["CreditAmt"]);
                    }
                }
                sb.Append("</table>");
            }
            if ((opening_debit + debitAmt) < (opening_credit + CreditAmt))
            {
                closing_credit = (opening_credit + CreditAmt) - (opening_debit + debitAmt);
                closing_debit = 0;
            }
            if ((opening_debit + debitAmt) > (opening_credit + CreditAmt))
            {
                closing_debit = (opening_debit + debitAmt) - (opening_credit + CreditAmt);
                closing_credit = 0;
            }
            return sb.ToString().Replace("#", ".") + "#" + opening_debit.ToString("0") + "#" + opening_credit.ToString("0") + "#" + debitAmt.ToString("0") + "#" + CreditAmt.ToString("0") + "#" + closing_debit.ToString("0") + "#" + closing_credit.ToString("0");
        }
        public dataSet LedgerBooksDataSet(ipAccounts ipAcc)
        {
            dataSet ds = AC_ViewLedgerInfo(ipAcc);
            return ds;
        }
        public string  LedgerBooksDetailOfVoucher(ipAccounts ipAcc)
        {
            dataSet ds = AC_ViewLedgerInfo(ipAcc);
            string _result = string.Empty;
            StringBuilder sb = new StringBuilder();
            sb.Append("<table class='table table-bordered'>");
            sb.Append("<tr style='font-size:10pt' >");
            sb.Append("<td style='text-align:left;padding:1px; margin:1px;width:10%'>Type</td>");
            sb.Append("<td style='text-align:left;padding:1px; margin:1px;width:10%'>Date</td>");
            sb.Append("<td style='text-align:left;padding:1px; margin:1px;width:50%'>Ledger and Narration</td>");
            sb.Append("<td style='text-align:right;padding:1px; margin:1px;width:10%'>Debit</td>");
            sb.Append("<td style='text-align:right;padding:1px; margin:1px;width:10%'>Credit</td>");
            sb.Append("</tr>");
            if (ds.ResultSet.Tables.Count > 0 && ds.ResultSet.Tables[0].Rows.Count > 0)
            {

                foreach (DataRow dr in ds.ResultSet.Tables[0].Rows)
                {
                    sb.Append("<tr style='font-size:10pt' >");
                    sb.Append("<td style='text-align:left;padding:1px; margin:1px;width:10%'> " + dr["vch_type"].ToString() + " </td>");
                    sb.Append("<td style='text-align:left;padding:1px; margin:1px;width:10%'> " + Convert.ToDateTime(dr["vch_date"]).ToString("dd-MM-yyyy") + " </td>");
                    sb.Append("<td style='text-align:left;padding:1px; margin:1px;width:50%'> <b style='color:green;font-size:11px'> " + dr["Particular"].ToString() + "</b><br/><i style='color:black;font-size:10px'>" + dr["narration"].ToString() + " </i> </td>");
                    sb.Append("<td style='text-align:right;padding:1px; margin:1px;width:10%'>  " + Convert.ToDecimal(dr["debitAmt"]).ToString() + " </td>");
                    sb.Append("<td style='text-align:right;padding:1px; margin:1px;width:10%'>  " + Convert.ToDecimal(dr["CreditAmt"]).ToString() + " </td>");
                    sb.Append("</tr>");
                }
                sb.Append("</table>");
            }
            return sb.ToString();
        }
    }
}