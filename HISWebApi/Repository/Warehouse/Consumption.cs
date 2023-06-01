using HISWebApi.Models;
using HISWebApi.Repository.Utilities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using static HISWebApi.Models.Warehouse;

namespace HISWebApi.Repository.Warehouse
{
    public class Consumption
    {
        public dataSet ConsumptionQueries(ConsumptionBO objBO)
        {
            dataSet dsObj = new dataSet();
            using (SqlConnection con = new SqlConnection(GlobalConfig.ConStr_Hospital))
            {
                using (SqlCommand cmd = new SqlCommand("pwh_ConsumptionQuery", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandTimeout = 2500;
                    cmd.Parameters.Add("@hosp_id", SqlDbType.VarChar, 10).Value = objBO.hosp_id;
                    cmd.Parameters.Add("@CartId", SqlDbType.VarChar, 7).Value = objBO.CartId;                    
                    cmd.Parameters.Add("@item_id", SqlDbType.VarChar, 12).Value = objBO.item_id;
                    cmd.Parameters.Add("@from", SqlDbType.VarChar, 100).Value = objBO.from;
                    cmd.Parameters.Add("@to", SqlDbType.VarChar, 100).Value = objBO.to;
                    cmd.Parameters.Add("@prm_1", SqlDbType.VarChar, 100).Value = objBO.prm_1;
                    cmd.Parameters.Add("@prm_2", SqlDbType.VarChar, 100).Value = objBO.prm_2;                    
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
        public string InsertUpdateConsumption(ConsumptionBO objBO)
        {
            dataSet dsObj = new dataSet();
            string processInfo = string.Empty;
            using (SqlConnection con = new SqlConnection(GlobalConfig.ConStr_Hospital))
            {
                using (SqlCommand cmd = new SqlCommand("pwh_ConsumptionInsertUpdate", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandTimeout = 2500;
                    cmd.Parameters.Add("@hosp_id", SqlDbType.VarChar, 10).Value = objBO.hosp_id;
                    cmd.Parameters.Add("@compid", SqlDbType.VarChar, 10).Value = objBO.compid;                    
                    cmd.Parameters.Add("@transId", SqlDbType.VarChar, 25).Value = objBO.transid;
                    cmd.Parameters.Add("@trntype", SqlDbType.VarChar, 20).Value = objBO.transtype;
                    cmd.Parameters.Add("@frmcartid", SqlDbType.VarChar, 25).Value = objBO.frmcart;
                    cmd.Parameters.Add("@tocartid", SqlDbType.VarChar, 25).Value = objBO.tocart;
                    cmd.Parameters.Add("@itemid", SqlDbType.VarChar, 20).Value = objBO.item_id;
                    cmd.Parameters.Add("@IssueTo", SqlDbType.VarChar, 20).Value = objBO.IssueTo;
                    cmd.Parameters.Add("@masterkeyid", SqlDbType.VarChar, 25).Value = objBO.masterkeyid;                    
                    cmd.Parameters.Add("@qty", SqlDbType.VarChar, 10).Value = objBO.qty;
                    cmd.Parameters.Add("@loginid", SqlDbType.VarChar, 20).Value = objBO.login_id;
                    cmd.Parameters.Add("@logic", SqlDbType.VarChar, 30).Value = objBO.Logic;
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
        public string wh_ConsumeStockByPatient(List<ipPatientConsume> objList)
        {
            string processInfo = string.Empty;
            DataTable dt = new DataTable();
            dt.Columns.Add("HospId", typeof(string));
            dt.Columns.Add("pt_name", typeof(string));
            dt.Columns.Add("ipop_no", typeof(string));
            dt.Columns.Add("gen_from", typeof(string));
            dt.Columns.Add("dept_name", typeof(string));
            dt.Columns.Add("room_no", typeof(string));
            dt.Columns.Add("bed_no", typeof(string));
            dt.Columns.Add("doctor_id", typeof(string));
            dt.Columns.Add("doctor_name", typeof(string));
            dt.Columns.Add("from_cart", typeof(string));
            dt.Columns.Add("item_id", typeof(string));
            dt.Columns.Add("master_key_id", typeof(string));
            dt.Columns.Add("item_name", typeof(string));
            dt.Columns.Add("qty", typeof(Int32));
            dt.Columns.Add("login_id", typeof(string));
            dt.Columns.Add("UHID", typeof(string));
            dt.Columns.Add("pay_type", typeof(string));
            dt.Columns.Add("PanelId", typeof(string));
            dt.Columns.Add("panel_name", typeof(string));
            dt.Columns.Add("ReqBy", typeof(string));
            dt.Columns.Add("ReqType", typeof(string));
            dt.Columns.Add("ProductSaleType", typeof(string));
            foreach (ipPatientConsume r in objList)
            {
                DataRow dr = dt.NewRow();
                dr["HospId"]=r.HospId;
                dr["pt_name"] = r.pt_name;
                dr["ipop_no"] = r.ipop_no;
                dr["gen_from"] = r.gen_from;
                dr["dept_name"] = r.dept_name;
                dr["room_no"] = r.room_no;
                dr["bed_no"] = r.bed_no;
                dr["doctor_id"] = r.doctor_id;
                dr["doctor_name"] = r.doctor_name;
                dr["from_cart"] = r.from_cart;
                dr["item_id"] = r.item_id;
                dr["master_key_id"] = r.master_key_id;
                dr["item_name"] = r.item_name;
                dr["qty"] = r.qty;
                dr["login_id"] = r.login_id;
                dr["UHID"] = r.UHID;
                dr["pay_type"] = r.pay_type;
                dr["PanelId"] = r.PanelId;
                dr["panel_name"] = r.panel_name;
                dr["ReqBy"] = r.ReqBy;
                dr["ReqType"] = r.ReqType;
                dr["ProductSaleType"] = r.ProductSaleType;
                dt.Rows.Add(dr); 
            }
            using (SqlConnection con = new SqlConnection(GlobalConfig.ConStr_Hospital))
            {
                using (SqlCommand cmd = new SqlCommand("pwh_ConsumeStockByPatient", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandTimeout = 2500;
                    cmd.Parameters.AddWithValue("@udt_PatientIndent",dt);
                    cmd.Parameters.Add("@result", SqlDbType.VarChar, 50).Value = "";
                    cmd.Parameters["@result"].Direction = ParameterDirection.InputOutput;
                    try
                    {
                        con.Open();
                        cmd.ExecuteNonQuery();
                        processInfo = cmd.Parameters["@result"].Value.ToString();
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


        public string wh_ConsumeStockByPatient(ConsumeStockBO objConsume, List<ConsumptionBO> objStock)
        {
            string processInfo = string.Empty;
            if (objStock.Count > 0)
            {
                DataTable dt = new DataTable();
                dt.Columns.Add("trn_no", typeof(string));
                dt.Columns.Add("item_id", typeof(string));
                dt.Columns.Add("fromCartId", typeof(string));
                dt.Columns.Add("toCartId", typeof(string));
                dt.Columns.Add("master_key_Id", typeof(string));
                dt.Columns.Add("qty", typeof(int));
                dt.Columns.Add("remark", typeof(string));
                foreach (ConsumptionBO obj in objStock)
                {
                    DataRow dr = dt.NewRow();
                    dr["trn_no"] = "-";
                    dr["item_id"] = obj.item_id;
                    dr["fromCartId"] = obj.frmcart;
                    dr["toCartId"] = obj.tocart;
                    dr["master_key_Id"] = obj.masterkeyid;
                    dr["qty"] = obj.StockQuantity;
                    dr["remark"] = obj.remark;
                    dt.Rows.Add(dr);
                }
                using (SqlConnection con = new SqlConnection(GlobalConfig.ConStr_Hospital))
                {
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand("pwh_ConsumeStockByPatient", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandTimeout = 2500;
                        cmd.Parameters.Add("@hosp_id", SqlDbType.VarChar, 10).Value = objConsume.hosp_id;
                        cmd.Parameters.Add("@UHID", SqlDbType.VarChar, 20).Value = objConsume.UHID;
                        cmd.Parameters.Add("@IPDNo", SqlDbType.VarChar, 20).Value = objConsume.IPDNo;
                        cmd.Parameters.Add("@patientName", SqlDbType.VarChar, 200).Value = objConsume.patientName;
                        cmd.Parameters.AddWithValue("wh_udtStockTran", dt);
                        cmd.Parameters.Add("@login_id", SqlDbType.VarChar, 20).Value = objConsume.login_id;
                        cmd.Parameters.Add("@Logic", SqlDbType.VarChar, 30).Value = objConsume.Logic;
                        cmd.Parameters.Add("@result", SqlDbType.VarChar, 100).Value = "";
                        cmd.Parameters["@result"].Direction = ParameterDirection.InputOutput;
                        try
                        {
                            cmd.ExecuteNonQuery();
                            processInfo = (string)cmd.Parameters["@result"].Value.ToString();
                        }
                        catch (Exception sqlEx)
                        {
                            processInfo = "Error Found   : " + sqlEx.Message;
                        }
                        finally { con.Close(); }
                    }
                }
            }
            return processInfo;
        }
    }
}