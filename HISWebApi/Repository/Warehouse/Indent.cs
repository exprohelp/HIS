using HISWebApi.Models;
using Newtonsoft.Json;
using System.Data;
using System.Data.SqlClient;
using static HISWebApi.Models.Warehouse;

namespace HISWebApi.Repository.Warehouse
{
    public class Indent
    {
        #region Warehouse Indent Entry
        public dataSet IndentQueries(IdentBO objBO)
        {
            dataSet dsObj = new dataSet();
            using (SqlConnection con = new SqlConnection(GlobalConfig.ConStr_Hospital))
            {
                using (SqlCommand cmd = new SqlCommand("pwh_IndentCartQueries", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandTimeout = 2500;
                    cmd.Parameters.Add("@hosp_id", SqlDbType.VarChar, 10).Value = objBO.hosp_id;
                    cmd.Parameters.Add("@CartId", SqlDbType.VarChar, 7).Value = objBO.CartId;
                    //cmd.Parameters.Add("@indent_no", SqlDbType.VarChar, 50).Value = objBO.indent_no;
                    cmd.Parameters.Add("@item_id", SqlDbType.VarChar, 12).Value = objBO.item_id;
                    //cmd.Parameters.Add("@from", SqlDbType.VarChar, 100).Value = objBO.from;
                    //cmd.Parameters.Add("@to", SqlDbType.VarChar, 100).Value = objBO.to;
                    cmd.Parameters.Add("@prm_1", SqlDbType.VarChar, 100).Value = objBO.prm_1;
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
        public dataSet wh_IndentCartQueries(IdentBO objBO)
        {
            dataSet dsObj = new dataSet();
            using (SqlConnection con = new SqlConnection(GlobalConfig.ConStr_Hospital))
            {
                using (SqlCommand cmd = new SqlCommand("pwh_IndentCartQueries", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandTimeout = 2500;
                    cmd.Parameters.Add("@hosp_id", SqlDbType.VarChar, 10).Value = objBO.hosp_id;
                    cmd.Parameters.Add("@CartId", SqlDbType.VarChar, 7).Value = objBO.CartId;
                    cmd.Parameters.Add("@indent_no", SqlDbType.VarChar, 50).Value = objBO.indent_no;
                    cmd.Parameters.Add("@item_id", SqlDbType.VarChar, 20).Value = objBO.item_id;
                    cmd.Parameters.Add("@from", SqlDbType.VarChar, 20).Value = objBO.from;
                    cmd.Parameters.Add("@to", SqlDbType.VarChar, 20).Value = objBO.to;
                    cmd.Parameters.Add("@prm_1", SqlDbType.VarChar, 100).Value = objBO.prm_1;
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
        public string  wh_InsertUpdateIndent(IdentBO objBO)
        {
            dataSet dsObj = new dataSet();
            string processInfo = string.Empty;
            using (SqlConnection con = new SqlConnection(GlobalConfig.ConStr_Hospital))
            {
                using (SqlCommand cmd = new SqlCommand("pwh_InsertUpdateIndent", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandTimeout = 2500;
                    cmd.Parameters.Add("@autoid", SqlDbType.VarChar, 10).Value = objBO.autoId;
                    cmd.Parameters.Add("@hosp_id", SqlDbType.VarChar, 10).Value = objBO.hosp_id;
                    cmd.Parameters.Add("@CartId", SqlDbType.VarChar, 7).Value = objBO.CartId;
                    cmd.Parameters.Add("@indent_no", SqlDbType.VarChar, 50).Value = objBO.indent_no;
                    cmd.Parameters.Add("@item_id", SqlDbType.VarChar, 50).Value = objBO.item_id;
                    cmd.Parameters.Add("@order_qty", SqlDbType.VarChar, 50).Value = objBO.order_qty;
                    cmd.Parameters.Add("@verify_qty", SqlDbType.VarChar, 50).Value = objBO.verifyqty;
                    cmd.Parameters.Add("@order_by", SqlDbType.VarChar, 50).Value = objBO.order_by;
                    cmd.Parameters.Add("@Logic", SqlDbType.VarChar, 50).Value = objBO.Logic;
                    cmd.Parameters.Add("@verify_by", SqlDbType.VarChar, 50).Value = objBO.veify_by;
                    cmd.Parameters.Add("@user_remark", SqlDbType.VarChar, 200).Value = objBO.user_remark;
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
       
        #endregion

        #region WAREHOUSE INDENT PROCESS QUERY
        public dataSet IndentProcessQueries(IdentBO objBO)
        {
            dataSet dsObj = new dataSet();
            using (SqlConnection con = new SqlConnection(GlobalConfig.ConStr_Hospital))
            {
                using (SqlCommand cmd = new SqlCommand("pwh_IndentProcess_Queries", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandTimeout = 2500;
                    cmd.Parameters.Add("@hosp_id", SqlDbType.VarChar, 10).Value = objBO.hosp_id;
                    cmd.Parameters.Add("@cart_id", SqlDbType.VarChar, 10).Value = objBO.CartId;
                    cmd.Parameters.Add("@indent_no", SqlDbType.VarChar, 50).Value = objBO.indent_no;
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
        public dataSet DispatchedQuery(DispatchOrderBo objBO)
        {
            dataSet dsObj = new dataSet();
            using (SqlConnection con = new SqlConnection(GlobalConfig.ConStr_Hospital))
            {
                using (SqlCommand cmd = new SqlCommand("pwh_DispatchQuery", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandTimeout = 2500;
                    cmd.Parameters.Add("@lot_no", SqlDbType.VarChar, 30).Value = objBO.lot_no;
                    cmd.Parameters.Add("@indentNo", SqlDbType.VarChar, 30).Value = objBO.indent_no;
                    cmd.Parameters.Add("@item_id", SqlDbType.VarChar, 30).Value = objBO.item_id;
                    cmd.Parameters.Add("@FromDate", SqlDbType.VarChar, 15).Value = objBO.FromDate;
                    cmd.Parameters.Add("@ToDate", SqlDbType.VarChar, 15).Value = objBO.ToDate;
                    cmd.Parameters.Add("@CartId", SqlDbType.VarChar, 15).Value = objBO.CartId;
                    cmd.Parameters.Add("@login_id", SqlDbType.VarChar, 15).Value = objBO.login_id;
                    cmd.Parameters.Add("@Logic", SqlDbType.VarChar, 50).Value = objBO.Logic;
                    cmd.Parameters.Add("@warehouseCartId", SqlDbType.VarChar, 20).Value = objBO.warehouseCartId;
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
        public dataSet wh_IndentReplaceAndUpdateQty(IndentReplacement objBO)
        {
            string processInfo = string.Empty;
            dataSet dsObj = new dataSet();
            using (SqlConnection con = new SqlConnection(GlobalConfig.ConStr_Hospital))
            {
                using (SqlCommand cmd = new SqlCommand("pwh_IndentReplaceAndUpdateQty", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandTimeout = 2500;
                    cmd.Parameters.Add("@Indent_no", SqlDbType.VarChar, 18).Value = objBO.Indent_no;
                    cmd.Parameters.Add("@Item_id", SqlDbType.VarChar, 10).Value = objBO.Item_id;
                    cmd.Parameters.Add("@verifyQty", SqlDbType.Int).Value = objBO.verifyQty;
                    cmd.Parameters.Add("@remark", SqlDbType.VarChar, 200).Value = objBO.remark;
                    cmd.Parameters.Add("@NewItemId", SqlDbType.VarChar, 10).Value = objBO.NewItemId;
                    cmd.Parameters.Add("@loginid", SqlDbType.VarChar, 10).Value = objBO.loginid;
                    cmd.Parameters.Add("@Logic", SqlDbType.VarChar, 50).Value = objBO.Logic;
                    cmd.Parameters.Add("@warehouseCartId", SqlDbType.VarChar, 50).Value = objBO.warehouseCartId;
                    cmd.Parameters.Add("@result", SqlDbType.VarChar, 100).Value = "";
                    cmd.Parameters["@result"].Direction = ParameterDirection.InputOutput;
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

        #region WAREHOUSE STOCKQUERY

        public dataSet IndentStockQueries(IdentStockBO objBO)
        {
            dataSet dsObj = new dataSet();
            using (SqlConnection con = new SqlConnection(GlobalConfig.ConStr_Hospital))
            {
                using (SqlCommand cmd = new SqlCommand("pwh_StockQueries", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandTimeout = 2500;
                    cmd.Parameters.Add("@hosp_id", SqlDbType.VarChar, 10).Value = objBO.hosp_id;
                    cmd.Parameters.Add("@cart_id", SqlDbType.VarChar, 7).Value = objBO.CartId;  
                    cmd.Parameters.Add("@item_id", SqlDbType.VarChar, 12).Value = objBO.item_id;
                    cmd.Parameters.Add("@prm_1", SqlDbType.VarChar, 100).Value = objBO.prm_1;
                    cmd.Parameters.Add("@Logic", SqlDbType.VarChar, 50).Value = objBO.Logic;
                    cmd.Parameters.Add("@login_id", SqlDbType.VarChar, 10).Value = objBO.login_id;
					cmd.Parameters.Add("@warehouseCartId", SqlDbType.VarChar, 7).Value = objBO.warehouseCartId;
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
        public string DispatchStock(DispatchOrderBo objBO)
        {
            dataSet dsObj = new dataSet();
            string processInfo = string.Empty;
            using (SqlConnection con = new SqlConnection(GlobalConfig.ConStr_Hospital))
            {
                using (SqlCommand cmd = new SqlCommand("pwh_DispatchProduct", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandTimeout = 2500;
                    cmd.Parameters.Add("@lot_no", SqlDbType.VarChar, 16).Value = objBO.lot_no;
                    cmd.Parameters.Add("@master_key_id", SqlDbType.VarChar, 20).Value = objBO.master_key_id;
                    cmd.Parameters.Add("@qty", SqlDbType.Int).Value = objBO.qty;
                    cmd.Parameters.Add("@trf_to", SqlDbType.VarChar, 10).Value = objBO.trf_to;
                    cmd.Parameters.Add("@mfd_id", SqlDbType.VarChar, 10).Value = objBO.mfd_id;
                    cmd.Parameters.Add("@comp_code", SqlDbType.VarChar, 10).Value = objBO.comp_code;
                    cmd.Parameters.Add("@trf_from", SqlDbType.VarChar, 10).Value = objBO.trf_from;
                    cmd.Parameters.Add("@cr_by", SqlDbType.VarChar, 10).Value = objBO.login_id;
                    cmd.Parameters.Add("@Indent_no", SqlDbType.VarChar, 20).Value = objBO.indent_no;
                    cmd.Parameters.Add("@Indent_autoId", SqlDbType.Int).Value = objBO.indent_autoId;                    
                    cmd.Parameters.Add("@Logic", SqlDbType.VarChar, 50).Value = objBO.Logic;
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
        public string CompleteDispatch(DispatchOrderBo objBO)
        {
            dataSet dsObj = new dataSet();
            string processInfo = string.Empty;
            using (SqlConnection con = new SqlConnection(GlobalConfig.ConStr_Hospital))
            {
                using (SqlCommand cmd = new SqlCommand("pwh_DispatchComplete", con))
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
        public dataSet DeleteDispatch(DispatchOrderBo objBO)
        {
            dataSet dsObj = new dataSet();
            string processInfo = string.Empty;
            using (SqlConnection con = new SqlConnection(GlobalConfig.ConStr_Hospital))
            {
                using (SqlCommand cmd = new SqlCommand("pwh_DispatchDelete", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandTimeout = 2500;

                    cmd.Parameters.Add("@auto_id", SqlDbType.VarChar, 16).Value = objBO.auto_id;
                    cmd.Parameters.Add("@itemid", SqlDbType.VarChar, 20).Value = objBO.item_id;
                    cmd.Parameters.Add("@indentno", SqlDbType.VarChar, 30).Value = objBO.indent_no;
                    cmd.Parameters.Add("@fromIndent", SqlDbType.VarChar, 16).Value = objBO.fromIndent;
                    cmd.Parameters.Add("@cart_id", SqlDbType.VarChar, 16).Value = objBO.CartId;
                    cmd.Parameters.Add("@warehouseCartId", SqlDbType.VarChar, 20).Value = objBO.warehouseCartId;
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
        public string  DeleteDispatchWithoutIndent(DispatchOrderBo objBO)
        {
            dataSet dsObj = new dataSet();
            string processInfo = string.Empty;
            using (SqlConnection con = new SqlConnection(GlobalConfig.ConStr_Hospital))
            {
                using (SqlCommand cmd = new SqlCommand("pwh_DispatchDelete", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandTimeout = 2500;

                    cmd.Parameters.Add("@auto_id", SqlDbType.VarChar, 16).Value = objBO.auto_id;
                    cmd.Parameters.Add("@fromIndent", SqlDbType.VarChar, 16).Value = objBO.fromIndent;
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
        public string  ReceiveProduct(DispatchOrderBo objBO)
        {            
            string processInfo = string.Empty;
            using (SqlConnection con = new SqlConnection(GlobalConfig.ConStr_Hospital))
            {
                using (SqlCommand cmd = new SqlCommand("pwh_DispatchReceive", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandTimeout = 2500;

                    cmd.Parameters.Add("@TranId", SqlDbType.VarChar, 16).Value = objBO.lot_no;
                    cmd.Parameters.Add("@Logic", SqlDbType.VarChar, 16).Value = objBO.Logic;
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
        public string  wh_ImportFromPharmacy(GeneralStoreQueries objBO)
        {
            PharmacyStore pharStore = new PharmacyStore();
            string processInfo = string.Empty;
            string result = string.Empty;
            dataSet ds = pharStore.Hospital_GeneralStoreQueries(objBO);
            using (SqlConnection con = new SqlConnection(GlobalConfig.ConStr_Hospital))
            {
                using (SqlCommand cmd = new SqlCommand("pwh_ImportFromPharmacy", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandTimeout = 2500;
                    cmd.Parameters.Add("@TransferId", SqlDbType.VarChar, 20).Value = objBO.TransferId;
                    cmd.Parameters.AddWithValue("@UDT_RecvdByPharmacy", ds.ResultSet.Tables[0]);
                    cmd.Parameters.Add("@login_id", SqlDbType.VarChar, 15).Value = objBO.login_id;
                    cmd.Parameters.Add("@Logic", SqlDbType.VarChar, 50).Value = objBO.WHLogic;
                    cmd.Parameters.Add("@result", SqlDbType.VarChar, 50).Value = "";
                    cmd.Parameters["@result"].Direction = ParameterDirection.InputOutput;
                    try
                    {
                        con.Open();
                        cmd.ExecuteNonQuery();
                        processInfo =(string)cmd.Parameters["@result"].Value.ToString();
                        con.Close();
                        pharStore.Receive_TrfId_InStock(out result, "MS-H0049", objBO.TransferId, objBO.login_id, "N/A");
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
        #endregion
        //Receive Stock from Diagnostic
        public dataSet Unit_TransferQueries(TransferQueries objBO)
        {
            dataSet dsObj = new dataSet();
            using (SqlConnection con = new SqlConnection(GlobalConfig.ConStr_eIMData))
            {
                using (SqlCommand cmd = new SqlCommand("pUnit_TransferQueries", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandTimeout = 2500;
                    cmd.Parameters.Add("@unit_id", SqlDbType.VarChar, 10).Value = objBO.unit_id;
                    cmd.Parameters.Add("@TransferId", SqlDbType.VarChar, 20).Value = objBO.TransferId;
                    cmd.Parameters.Add("@item_id", SqlDbType.VarChar, 20).Value = objBO.item_id;
                    cmd.Parameters.Add("@prm_1", SqlDbType.VarChar, 100).Value = objBO.prm_1;
                    cmd.Parameters.Add("@from", SqlDbType.DateTime, 22).Value = objBO.from;
                    cmd.Parameters.Add("@to", SqlDbType.DateTime, 22).Value = objBO.to;
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
        public string wh_ImportFromDiagnosticWarehouse(TransferQueries objBO)
        {
            string processInfo = string.Empty;
            using (SqlConnection con = new SqlConnection(GlobalConfig.ConStr_Hospital))
            {
                using (SqlCommand cmd = new SqlCommand("pwh_ImportFromDiagnosticWarehouse", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandTimeout = 2500;
                    cmd.Parameters.Add("@unit_id", SqlDbType.VarChar, 10).Value = objBO.unit_id;
                    cmd.Parameters.Add("@TransferId", SqlDbType.VarChar, 50).Value = objBO.TransferId;
                    cmd.Parameters.Add("@login_id", SqlDbType.VarChar, 15).Value = objBO.login_id;
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
    }
}





/***  Till Not in Use   ***/
//public string VerifyIndent(IdentVerifyBO objBO)
//{
//    string processInfo = string.Empty;
//    DataTable dt = (DataTable)JsonConvert.DeserializeObject(objBO.VeriftDataTable, (typeof(DataTable)));
//    using (SqlConnection con = new SqlConnection(GlobalConfig.ConStr_Hospital))
//    {
//        using (SqlCommand cmd = new SqlCommand("pwh_IndentVerification", con))
//        {
//            cmd.CommandType = CommandType.StoredProcedure;
//            cmd.CommandTimeout = 2500;
//            cmd.Parameters.AddWithValue("@wh_IndentVerifyUDT", dt);
//            cmd.Parameters.Add("@Logic", SqlDbType.VarChar, 50).Value = objBO.Logic;
//            cmd.Parameters.Add("@login_id", SqlDbType.VarChar, 10).Value = objBO.login_id;
//            cmd.Parameters.Add("@result", SqlDbType.VarChar, 50).Value = "";
//            cmd.Parameters["@result"].Direction = ParameterDirection.InputOutput;
//            try
//            {
//                con.Open();
//                cmd.ExecuteNonQuery();
//                processInfo = (string)cmd.Parameters["@result"].Value.ToString();
//                con.Close();
//            }
//            catch (SqlException sqlEx)
//            {
//                processInfo = "Error Found   : " + sqlEx.Message;
//            }
//            finally { con.Close(); }
//            return processInfo;
//        }
//    }
//}
/***  Till Not in Use   ***/
