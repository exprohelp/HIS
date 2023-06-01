using HISWebApi.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using static HISWebApi.Models.Warehouse;

namespace HISWebApi.Repository.LinenLaundry
{
    public class TransferAndReceiving
    {
        #region LINEN Transfer Receiving
        public dataSet LL_AuditQry(ipLinenLaundry objBO)
        {
            dataSet dsObj = new dataSet();
            string processInfo = string.Empty;
            using (SqlConnection con = new SqlConnection(GlobalConfig.ConStr_Hospital))
            {
                using (SqlCommand cmd = new SqlCommand("pLL_AuditQry", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandTimeout = 2500;
                    cmd.Parameters.Add("@CartId", SqlDbType.VarChar, 10).Value = objBO.CartId;
                    cmd.Parameters.Add("@from", SqlDbType.Date).Value = objBO.from;
                    cmd.Parameters.Add("@To", SqlDbType.Date).Value = objBO.To;
                    cmd.Parameters.Add("@Prm1", SqlDbType.VarChar, 100).Value = objBO.Prm1;
                    cmd.Parameters.Add("@Prm2", SqlDbType.VarChar, 100).Value = objBO.Prm2;
                    cmd.Parameters.Add("@IndentNo", SqlDbType.VarChar, 20).Value = objBO.IndentNo;
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
        public dataSet LL_TransferAndReceiveQueries(ipLinen objBO)
        {
            dataSet dsObj = new dataSet();
            using (SqlConnection con = new SqlConnection(GlobalConfig.ConStr_Hospital))
            {
                using (SqlCommand cmd = new SqlCommand("pLL_TransferAndReceiveQueries", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandTimeout = 2500;
                    cmd.Parameters.Add("@hosp_id", SqlDbType.VarChar, 10).Value = objBO.HospId;
                    cmd.Parameters.Add("@cart_id", SqlDbType.VarChar, 10).Value = objBO.CartId;
                    cmd.Parameters.Add("@TranId", SqlDbType.VarChar, 18).Value = objBO.TranId;
                    cmd.Parameters.Add("@trf_from", SqlDbType.VarChar, 12).Value = objBO.trf_from;
                    cmd.Parameters.Add("@item_id", SqlDbType.VarChar, 12).Value = objBO.ItemId;
                    cmd.Parameters.Add("@prm_1", SqlDbType.VarChar, 100).Value = objBO.Prm1;
                    cmd.Parameters.Add("@prm_2", SqlDbType.VarChar, 100).Value = objBO.Prm2;
                    cmd.Parameters.Add("@Logic", SqlDbType.VarChar, 50).Value = objBO.Logic;
                    cmd.Parameters.Add("@login_id", SqlDbType.VarChar, 10).Value = objBO.login_id;
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
        public string LL_TransferStock(TransferLinen objBO)
        {
            dataSet dsObj = new dataSet();
            string processInfo = string.Empty;
            using (SqlConnection con = new SqlConnection(GlobalConfig.ConStr_Hospital))
            {
                using (SqlCommand cmd = new SqlCommand("pLL_TransferStock", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandTimeout = 2500;
                    cmd.Parameters.Add("@lot_no", SqlDbType.VarChar, 16).Value = objBO.lot_no;
                    cmd.Parameters.Add("@itemId", SqlDbType.VarChar, 16).Value = objBO.ItemId;
                    cmd.Parameters.Add("@trf_remark", SqlDbType.VarChar, 500).Value = objBO.trf_remark;
                    cmd.Parameters.Add("@qty", SqlDbType.Int).Value = objBO.qty;
                    cmd.Parameters.Add("@damage_qty", SqlDbType.Int).Value = objBO.damage_qty;
                    cmd.Parameters.Add("@trf_to", SqlDbType.VarChar, 10).Value = objBO.trf_to;
                    cmd.Parameters.Add("@trf_from", SqlDbType.VarChar, 10).Value = objBO.trf_from;
                    cmd.Parameters.Add("@login_id", SqlDbType.VarChar, 10).Value = objBO.login_id;
                    cmd.Parameters.Add("@IndentNo", SqlDbType.VarChar, 20).Value = objBO.IndentNo;
                    cmd.Parameters.Add("@Logic", SqlDbType.VarChar, 50).Value = objBO.Logic;
                    cmd.Parameters.Add("@AutoId", SqlDbType.BigInt).Value = Convert.ToInt32(objBO.AutoId);
                    cmd.Parameters.Add("@result", SqlDbType.VarChar, 50).Value = "";
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
        public string LL_TransferComplete(TransferLinen objBO)
        {
            dataSet dsObj = new dataSet();
            string processInfo = string.Empty;
            using (SqlConnection con = new SqlConnection(GlobalConfig.ConStr_Hospital))
            {
                using (SqlCommand cmd = new SqlCommand("pLL_TransferComplete", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandTimeout = 2500;
                    cmd.Parameters.Add("@TranId", SqlDbType.VarChar, 20).Value = objBO.lot_no;
                    cmd.Parameters.Add("@Logic", SqlDbType.VarChar, 30).Value = objBO.Logic;
                    cmd.Parameters.Add("@login_id", SqlDbType.VarChar, 10).Value = objBO.login_id;
                    cmd.Parameters.Add("@result", SqlDbType.VarChar, 50).Value = "";
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
        public string LL_ReceiveInStock(TransferLinen objBO)
        {
            string processInfo = string.Empty;
            using (SqlConnection con = new SqlConnection(GlobalConfig.ConStr_Hospital))
            {
                using (SqlCommand cmd = new SqlCommand("pLL_ReceiveInStock", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandTimeout = 2500;
                    cmd.Parameters.Add("@TranId", SqlDbType.VarChar, 18).Value = objBO.lot_no;
                    cmd.Parameters.Add("@Logic", SqlDbType.VarChar, 100).Value = objBO.Logic;
                    cmd.Parameters.Add("@login_id", SqlDbType.VarChar, 16).Value = objBO.login_id;
                    cmd.Parameters.Add("@result", SqlDbType.VarChar, 50).Value = "";
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
        public string LL_WashingToLaundryReceiving(LaundryReceive objBO)
        {
            string processInfo = string.Empty;
            using (SqlConnection con = new SqlConnection(GlobalConfig.ConStr_Hospital))
            {
                using (SqlCommand cmd = new SqlCommand("pLL_WashingToLaundryReceiving", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandTimeout = 2500;
                    cmd.Parameters.Add("@TranId", SqlDbType.VarChar, 18).Value = objBO.transid;
                    cmd.Parameters.Add("@ItemId", SqlDbType.VarChar, 16).Value = objBO.item_id;
                    cmd.Parameters.Add("@qty", SqlDbType.Int, 10).Value = objBO.qty;
                    cmd.Parameters.Add("@login_id", SqlDbType.VarChar, 16).Value = objBO.login_id;
                    cmd.Parameters.Add("@hosp_id", SqlDbType.VarChar, 16).Value = objBO.hosp_id;
                    cmd.Parameters.Add("@Logic", SqlDbType.VarChar, 100).Value = objBO.Logic;
                    cmd.Parameters.Add("@result", SqlDbType.VarChar, 50).Value = "";
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
        public string LL_WashingLaundaryMovement(LaundryReceive objBO)
        {
            string processInfo = string.Empty;
            using (SqlConnection con = new SqlConnection(GlobalConfig.ConStr_Hospital))
            {
                using (SqlCommand cmd = new SqlCommand("pLL_WashingLaundaryMovement", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandTimeout = 2500;
                    cmd.Parameters.Add("@TranId", SqlDbType.VarChar, 18).Value = objBO.transid;
                    cmd.Parameters.Add("@ItemId", SqlDbType.VarChar, 16).Value = objBO.item_id;
                    cmd.Parameters.Add("@qty", SqlDbType.Int, 10).Value = objBO.qty;
                    cmd.Parameters.Add("@login_id", SqlDbType.VarChar, 16).Value = objBO.login_id;
                    cmd.Parameters.Add("@hosp_id", SqlDbType.VarChar, 16).Value = objBO.hosp_id;
                    cmd.Parameters.Add("@Logic", SqlDbType.VarChar, 100).Value = objBO.Logic;
                    cmd.Parameters.Add("@result", SqlDbType.VarChar, 50).Value = "";
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
        public string LL_AllPendingReceiving(List<LaundryReceive> objBO)
        {
            string processInfo = string.Empty;
            string login_id = string.Empty;
            string hosp_id = string.Empty;
            if (objBO.Count > 0)
            {
                DataTable dt = new DataTable();
                dt.Columns.Add("trn_no", typeof(string));
                dt.Columns.Add("item_id", typeof(string));
                dt.Columns.Add("fromCartId", typeof(string));
                dt.Columns.Add("toCartId", typeof(string));
                dt.Columns.Add("master_key_id", typeof(string));
                dt.Columns.Add("qty", typeof(int));
                dt.Columns.Add("remark", typeof(string));
                foreach (LaundryReceive obj in objBO)
                {
                    hosp_id = obj.hosp_id;
                    login_id = obj.login_id;
                    DataRow dr = dt.NewRow();
                    dr["trn_no"] = obj.transid;
                    dr["item_id"] = obj.item_id;
                    dr["fromCartId"] = obj.frmcart;
                    dr["toCartId"] = obj.tocart;
                    dr["master_key_id"] = "";
                    dr["qty"] = obj.qty;
                    dr["remark"] = "";
                    dt.Rows.Add(dr);
                }
                using (SqlConnection con = new SqlConnection(GlobalConfig.ConStr_Hospital))
                {
                    using (SqlCommand cmd = new SqlCommand("pLL_AllPendingReceiving", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandTimeout = 2500;
                        cmd.Parameters.Add("@hosp_id", SqlDbType.VarChar, 10).Value = hosp_id;
                        cmd.Parameters.Add("@login_id", SqlDbType.VarChar, 10).Value = login_id;
                        cmd.Parameters.AddWithValue("wh_udtStockTran", dt);
                        cmd.Parameters.Add("@result", SqlDbType.VarChar, 50).Value = "";
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
                    }
                }
            }
            return processInfo;
        }
        public string LL_LinenDistribution(List<LaundryReceive> objBO)
        {
            var processInfo = string.Empty;
            var hosp_id = string.Empty;
            var login_id = string.Empty;
            if (objBO.Count > 0)
            {
                DataTable dt = new DataTable();
                dt.Columns.Add("trn_no", typeof(string));
                dt.Columns.Add("item_id", typeof(string));
                dt.Columns.Add("fromCartId", typeof(string));
                dt.Columns.Add("toCartId", typeof(string));
                dt.Columns.Add("master_key_Id", typeof(string));
                dt.Columns.Add("qty", typeof(int));
                dt.Columns.Add("remark", typeof(string));
                foreach (LaundryReceive obj in objBO)
                {
                    hosp_id = obj.hosp_id;
                    login_id = obj.login_id;
                    DataRow dr = dt.NewRow();
                    dr["trn_no"] = obj.transid;
                    dr["item_id"] = obj.item_id;
                    dr["fromCartId"] = obj.frmcart;
                    dr["toCartId"] = obj.tocart;
                    dr["master_key_Id"] = "-";
                    dr["qty"] = obj.qty;
                    dr["remark"] = "-";
                    dt.Rows.Add(dr);
                }
                using (SqlConnection con = new SqlConnection(GlobalConfig.ConnectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("pLL_LinenDistribution", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandTimeout = 2500;
                        cmd.Parameters.Add("@hosp_id", SqlDbType.VarChar, 10).Value = hosp_id;
                        cmd.Parameters.AddWithValue("wh_udtStockTran", dt);
                        cmd.Parameters.Add("@login_id", SqlDbType.VarChar, 10).Value = login_id;
                        cmd.Parameters.Add("@Logic", SqlDbType.VarChar, 30).Value = "Distribute";
                        cmd.Parameters.Add("@result", SqlDbType.VarChar, 50).Value = "";
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
                            processInfo = "Error Found : " + sqlEx.Message;
                        }
                        finally { con.Close(); }
                    }
                }
            }
            return processInfo;
        }
        public string LL_IndentInsert(IndentInfo objBO)
        {
            string processInfo = string.Empty;
            using (SqlConnection con = new SqlConnection(GlobalConfig.ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("pLL_IndentInsert", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandTimeout = 2500;
                    cmd.Parameters.Add("@AutoId", SqlDbType.Int, 5).Value = objBO.AutoId;
                    cmd.Parameters.Add("@CartdId", SqlDbType.VarChar, 10).Value = objBO.CartId;
                    cmd.Parameters.Add("@IndentNo", SqlDbType.VarChar, 16).Value = objBO.IndentNo;
                    cmd.Parameters.Add("@ItemId", SqlDbType.VarChar, 10).Value = objBO.ItemId;
                    cmd.Parameters.Add("@qty", SqlDbType.Int, 5).Value = objBO.qty;
                    cmd.Parameters.Add("@Remark", SqlDbType.VarChar, 100).Value = objBO.Remark;
                    cmd.Parameters.Add("@Prm1", SqlDbType.VarChar, 50).Value = objBO.Prm1;
                    cmd.Parameters.Add("@Prm2", SqlDbType.VarChar, 50).Value = objBO.Prm2;
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
        public dataSet LL_IndentProcess_Queries(IdentBO objBO)
        {
            dataSet dsObj = new dataSet();
            using (SqlConnection con = new SqlConnection(GlobalConfig.ConStr_Hospital))
            {
                using (SqlCommand cmd = new SqlCommand("pLL_IndentProcess_Queries", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandTimeout = 2500;
                    cmd.Parameters.Add("@hosp_id", SqlDbType.VarChar, 10).Value = objBO.hosp_id;
                    cmd.Parameters.Add("@cart_id", SqlDbType.VarChar, 10).Value = objBO.CartId;
                    cmd.Parameters.Add("@IndentNo", SqlDbType.VarChar, 50).Value = objBO.indent_no;
                    cmd.Parameters.Add("@item_id", SqlDbType.VarChar, 12).Value = objBO.item_id;
                    cmd.Parameters.Add("@from", SqlDbType.VarChar, 100).Value = objBO.from;
                    cmd.Parameters.Add("@to", SqlDbType.VarChar, 100).Value = objBO.to;
                    cmd.Parameters.Add("@prm_1", SqlDbType.VarChar, 100).Value = objBO.prm_1;
                    cmd.Parameters.Add("@Logic", SqlDbType.VarChar, 50).Value = objBO.Logic;
                    cmd.Parameters.Add("@login_id", SqlDbType.VarChar, 10).Value = objBO.login_id;
                    cmd.Parameters.Add("@warehouseCartId", SqlDbType.VarChar, 10).Value = objBO.warehouseCartId;

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
        #endregion
    }
}


