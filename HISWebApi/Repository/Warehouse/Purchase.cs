using HISWebApi.Models;
using System;
using System.Data;
using System.Data.SqlClient;
using static HISWebApi.Models.Warehouse;

namespace HISWebApi.Repository.Warehouse
{
	public class Purchase
	{
		public dataSet wh_GoodsReturnQueries(GoodsReturnQueriesBO objBO)
{
			dataSet dsObj = new dataSet();
			using (SqlConnection con = new SqlConnection(GlobalConfig.ConStr_Hospital))
			{
				using (SqlCommand cmd = new SqlCommand("pwh_GoodsReturnQueries", con))
				{
					cmd.CommandType = CommandType.StoredProcedure;
					cmd.CommandTimeout = 2500;
					cmd.Parameters.Add("@item_id", SqlDbType.VarChar, 16).Value = objBO.item_id;
					cmd.Parameters.Add("@vendor_id", SqlDbType.VarChar, 16).Value = objBO.vendor_id;
					cmd.Parameters.Add("@vendor_type", SqlDbType.VarChar, 50).Value = objBO.vendor_type;
					cmd.Parameters.Add("@from", SqlDbType.VarChar, 15).Value = objBO.From;
					cmd.Parameters.Add("@to", SqlDbType.VarChar, 15).Value = objBO.To;
					cmd.Parameters.Add("@prm_1", SqlDbType.VarChar, 100).Value = objBO.Prm1;
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
		public dataSet PurchaseQuery(PurchaseQueriesBO objBO)
		{
			dataSet dsObj = new dataSet();
			using (SqlConnection con = new SqlConnection(GlobalConfig.ConStr_Hospital))
			{
				using (SqlCommand cmd = new SqlCommand("pwh_PurchaseQueries", con))
				{
					cmd.CommandType = CommandType.StoredProcedure;
					cmd.CommandTimeout = 2500;
					cmd.Parameters.Add("@purchId", SqlDbType.VarChar, 16).Value = objBO.purchId;
					cmd.Parameters.Add("@VendorCode", SqlDbType.VarChar, 16).Value = objBO.VendorCode;
					cmd.Parameters.Add("@item_name", SqlDbType.VarChar, 50).Value = objBO.item_name;
					cmd.Parameters.Add("@Prm1", SqlDbType.VarChar, 100).Value = objBO.prm_1;
					cmd.Parameters.Add("@from", SqlDbType.DateTime).Value = objBO.from;
					cmd.Parameters.Add("@to", SqlDbType.DateTime).Value = objBO.to;
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
		public string PurchaseInsert(PurchaseBO objBO)
		{
			string processInfo = string.Empty;
			using (SqlConnection con = new SqlConnection(GlobalConfig.ConStr_Hospital))
			{
				con.Open();
				using (SqlCommand cmd = new SqlCommand("pwh_PurchaseInsert", con))
				{
					cmd.CommandType = CommandType.StoredProcedure;
					cmd.CommandTimeout = 2500;
					cmd.Parameters.Add("@unit_id", SqlDbType.VarChar, 10).Value = objBO.unit_id;
					cmd.Parameters.Add("@comp_code", SqlDbType.VarChar, 10).Value = objBO.comp_code;
					cmd.Parameters.Add("@pur_type", SqlDbType.NVarChar, 15).Value = objBO.pur_type;
					cmd.Parameters.Add("@Vendor_Code", SqlDbType.NVarChar, 8).Value = objBO.Vendor_Code;
					cmd.Parameters.Add("@Inv_No", SqlDbType.NVarChar, 15).Value = objBO.Inv_No;
					cmd.Parameters.Add("@Inv_Date", SqlDbType.DateTime).Value = objBO.Inv_Date;
					cmd.Parameters.Add("@NatureOfPurchase", SqlDbType.NVarChar, 25).Value = objBO.NatureOfPurchase;
					cmd.Parameters.Add("@eWayBillNo", SqlDbType.NVarChar, 25).Value = objBO.eWayBillNo;
					cmd.Parameters.Add("@eWayBillDate", SqlDbType.DateTime, 30).Value = (object)objBO.eWayBillDate ?? DBNull.Value;
					cmd.Parameters.Add("@Item_id", SqlDbType.VarChar, 10).Value = objBO.Item_id;
					cmd.Parameters.Add("@barcodeNo", SqlDbType.VarChar, 50).Value = objBO.barcodeNo;
					cmd.Parameters.Add("@hsn", SqlDbType.VarChar, 8).Value = objBO.hsn;
					cmd.Parameters.Add("@mfd_id", SqlDbType.VarChar, 10).Value = objBO.mfd_id;
					cmd.Parameters.Add("@Batch_No", SqlDbType.NVarChar, 30).Value = objBO.Batch_No;
					cmd.Parameters.Add("@Exp_Date", SqlDbType.VarChar, 30).Value = (object)objBO.Exp_Date ?? DBNull.Value;
					cmd.Parameters.Add("@pack_type", SqlDbType.NVarChar, 20).Value = objBO.pack_type;
					cmd.Parameters.Add("@pack_qty", SqlDbType.Int).Value = objBO.pack_qty;
					cmd.Parameters.Add("@MRP", SqlDbType.Decimal, 10).Value = objBO.MRP;
					cmd.Parameters.Add("@trade", SqlDbType.Decimal, 10).Value = objBO.trade;
					cmd.Parameters.Add("@Quantity", SqlDbType.Decimal, 10).Value = objBO.Quantity;
					cmd.Parameters.Add("@It_Free", SqlDbType.Decimal, 10).Value = objBO.It_Free;
					cmd.Parameters.Add("@DisPer", SqlDbType.Decimal, 10).Value = objBO.DisPer;
					cmd.Parameters.Add("@Tax_id", SqlDbType.Int).Value = objBO.Tax_id;
					cmd.Parameters.Add("@BestRate", SqlDbType.Decimal, 10).Value = objBO.BestRate;
					cmd.Parameters.Add("@Remark", SqlDbType.VarChar, 200).Value = objBO.Remark;
					cmd.Parameters.Add("@CreatedBy", SqlDbType.NVarChar, 10).Value = objBO.CreatedBy;
					cmd.Parameters.Add("@Purch_id", SqlDbType.VarChar, 16).Value = objBO.Purch_id;
					cmd.Parameters["@Purch_id"].Direction = ParameterDirection.InputOutput;
					cmd.Parameters.Add("@result", SqlDbType.VarChar, 200).Value = "";
					cmd.Parameters["@result"].Direction = ParameterDirection.InputOutput;
					try
					{
						cmd.ExecuteNonQuery();
						processInfo = (string)cmd.Parameters["@result"].Value.ToString();
						if (processInfo == "Success")
						{
							processInfo = (string)cmd.Parameters["@Purch_id"].Value.ToString();
						}
					}
					catch (Exception sqlEx)
					{
						processInfo = "Error Found   : " + sqlEx.Message;
					}
					finally { con.Close(); }
					return processInfo;
				}
			}
		}
		public string PurchasePosting(PurchaseBO objBO)
		{
			string processInfo = string.Empty;
			using (SqlConnection con = new SqlConnection(GlobalConfig.ConStr_Hospital))
			{
				con.Open();
				using (SqlCommand cmd = new SqlCommand("pwh_PurchasePosting", con))
				{
					cmd.CommandType = CommandType.StoredProcedure;
					cmd.CommandTimeout = 2500;
					cmd.Parameters.Add("@purch_id", SqlDbType.VarChar, 15).Value = objBO.Purch_id;
					cmd.Parameters.Add("@adj_amt", SqlDbType.Decimal, 10).Value = objBO.adj_amt;
					cmd.Parameters.Add("@plusminus", SqlDbType.Decimal, 15).Value = objBO.plusminus;
					cmd.Parameters.Add("@credit_cash", SqlDbType.VarChar, 10).Value = objBO.credit_cash;
					cmd.Parameters.Add("@login_id", SqlDbType.VarChar, 10).Value = objBO.login_id;
					cmd.Parameters.Add("@result", SqlDbType.VarChar, 200).Value = "";
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
					return processInfo;
				}
			}
		}
		public string PurchaseDelete(PurchaseBO objBO)
		{
			string processInfo = string.Empty;
			using (SqlConnection con = new SqlConnection(GlobalConfig.ConStr_Hospital))
			{
				con.Open();
				using (SqlCommand cmd = new SqlCommand("pwh_DeletePurchase", con))
				{
					cmd.CommandType = CommandType.StoredProcedure;
					cmd.CommandTimeout = 2500;
					cmd.Parameters.Add("@Auto_Id", SqlDbType.Int, 10).Value = objBO.Auto_Id;
					cmd.Parameters.Add("@purch_Id", SqlDbType.VarChar, 16).Value = objBO.Purch_id;
					cmd.Parameters.Add("@result", SqlDbType.VarChar, 200).Value = "";
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
					return processInfo;
				}
			}
		}
		public string wh_PurchaseImportStock(PurchaseBO objBO)
		{
			string processInfo = string.Empty;
			using (SqlConnection con = new SqlConnection(GlobalConfig.ConStr_Hospital))
			{
				con.Open();
				using (SqlCommand cmd = new SqlCommand("pwh_PurchaseImportStock", con))
				{
					cmd.CommandType = CommandType.StoredProcedure;
					cmd.CommandTimeout = 2500;
					cmd.Parameters.Add("@purchase_id", SqlDbType.VarChar, 15).Value = objBO.Purch_id;
					cmd.Parameters.Add("@login_id", SqlDbType.VarChar, 10).Value = objBO.login_id;
					cmd.Parameters.Add("@warehouseCartId", SqlDbType.VarChar, 10).Value = objBO.warehouseCartId;
					cmd.Parameters.Add("@Logic", SqlDbType.VarChar, 10).Value = objBO.Logic;
					cmd.Parameters.Add("@result", SqlDbType.VarChar, 200).Value = "";
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
					return processInfo;
				}
			}
		}
		public string wh_PurchaseReturnInsert(GoodsReturnQueriesBO objBO)
		{
			string processInfo = string.Empty;
			using (SqlConnection con = new SqlConnection(GlobalConfig.ConStr_Hospital))
			{
				con.Open();
				using (SqlCommand cmd = new SqlCommand("pwh_PurchaseReturnInsert", con))
				{
					cmd.CommandType = CommandType.StoredProcedure;
					cmd.CommandTimeout = 2500;
					cmd.Parameters.Add("@CartId", SqlDbType.VarChar, 10).Value = objBO.CartId;
					cmd.Parameters.Add("@unit_id", SqlDbType.VarChar, 10).Value = objBO.unit_id;
					cmd.Parameters.Add("@Vendor_Code", SqlDbType.VarChar, 8).Value = objBO.Vendor_Code;
					cmd.Parameters.Add("@Item_id", SqlDbType.NVarChar, 10).Value = objBO.Item_id;
					cmd.Parameters.Add("@master_key_id", SqlDbType.NVarChar, 16).Value = objBO.master_key_id;
					cmd.Parameters.Add("@Quantity", SqlDbType.Int).Value = objBO.Quantity;
					cmd.Parameters.Add("@result", SqlDbType.VarChar, 200).Value = "";
					cmd.Parameters.Add("@Logic", SqlDbType.VarChar, 200).Value = objBO.Logic;
					cmd.Parameters["@result"].Direction = ParameterDirection.InputOutput;
					cmd.Parameters.Add("@PurchReturnId", SqlDbType.VarChar, 200).Value = objBO.PurchReturnId;
					cmd.Parameters["@PurchReturnId"].Direction = ParameterDirection.InputOutput;
					try
					{
						cmd.ExecuteNonQuery();
						processInfo = (string)cmd.Parameters["@result"].Value.ToString();
						if (processInfo == "Success")
						{
							processInfo = (string)cmd.Parameters["@PurchReturnId"].Value.ToString();
						}
					}
					catch (Exception sqlEx)
					{
						processInfo = "Error Found   : " + sqlEx.Message;
					}
					finally { con.Close(); }
					return processInfo;
				}
			}
		}
		public string wh_PurchaseDiascountReprocess(DiascountReprocessBO objBO)
		{
			string processInfo = string.Empty;
			using (SqlConnection con = new SqlConnection(GlobalConfig.ConStr_Hospital))
			{
				con.Open();
				using (SqlCommand cmd = new SqlCommand("pwh_PurchaseDiascountReprocess", con))
				{
					cmd.CommandType = CommandType.StoredProcedure;
					cmd.CommandTimeout = 2500;
					cmd.Parameters.Add("@purch_id", SqlDbType.VarChar, 15).Value = objBO.purch_id;
					cmd.Parameters.Add("@discount_amount", SqlDbType.Decimal, 20).Value = objBO.discount_amount;
					cmd.Parameters.Add("@login_id", SqlDbType.VarChar, 10).Value = objBO.login_id;
					cmd.Parameters.Add("@Logic", SqlDbType.VarChar, 50).Value = objBO.Logic;					
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
					return processInfo;
				}
			}
		}
        public SubmitStatus UploadPurchaseDocument(string purchId, string ImageType, byte[] ImageByte, string login_id)
        {
            string processInfo = string.Empty;
            SubmitStatus ss = new SubmitStatus();
            string virtual_path = string.Empty; string physical_path = string.Empty;
            string ImageName = purchId.Replace("/", "").Replace("-", "");
            processInfo = Utilities.UploadClass.UploadPurchaseDocument(out physical_path, out virtual_path, purchId, ImageByte, ImageName, ImageType);
            if (processInfo.Contains("Success"))
            {
                using (SqlConnection con = new SqlConnection(GlobalConfig.ConStr_Hospital))
                {
                    using (SqlCommand cmd = new SqlCommand("pwh_UploadPurchaseDocument", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandTimeout = 2500;
                        cmd.Parameters.Add("@purchId", SqlDbType.VarChar, 20).Value = purchId;
                        cmd.Parameters.Add("@virtual_path", SqlDbType.VarChar, 200).Value = virtual_path;
                        cmd.Parameters.Add("@physical_path", SqlDbType.VarChar, 200).Value = physical_path;
                        cmd.Parameters.Add("@login_id", SqlDbType.VarChar, 15).Value = login_id;
                        cmd.Parameters.Add("@Logic", SqlDbType.VarChar, 50).Value = "UploadPurchaseDocument";
                        cmd.Parameters.Add("@result", SqlDbType.VarChar, 50).Value = "";
                        cmd.Parameters["@result"].Direction = ParameterDirection.InputOutput;
                        try
                        {
                            con.Open();
                            cmd.ExecuteNonQuery();
                            processInfo = (string)cmd.Parameters["@result"].Value.ToString();
                            if (processInfo.Contains("Success"))
                            {
                                ss.Status = 1;
                                ss.Message = processInfo;
                                ss.purchId = purchId;
                            }
                        }
                        catch (SqlException sqlEx)
                        {
                            ss.Status = 0;
                            ss.purchId = purchId;
                            ss.Message = sqlEx.Message;
                        }
                        finally { con.Close(); }
                    }
                    return ss;
                }
            }
            else
            {
                ss.Status = 0;
                ss.Message = processInfo;
            }
            return ss;
        }
        public dataSet wh_purchaseOrder_Query(PurchaseBO objBO)
        {
            dataSet dsObj = new dataSet();
            using (SqlConnection con = new SqlConnection(GlobalConfig.ConStr_Hospital))
            {
                using (SqlCommand cmd = new SqlCommand("pwh_purchaseOrder_Process", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandTimeout = 2500;
                    cmd.Parameters.Add("@unit_id", SqlDbType.VarChar, 10).Value = objBO.unit_id;
                    cmd.Parameters.Add("@poNumber", SqlDbType.VarChar, 20).Value = objBO.poNumber;
                    cmd.Parameters.Add("@qty", SqlDbType.Int, 20).Value = objBO.qty;
                    cmd.Parameters.Add("@prm1", SqlDbType.VarChar, 50).Value = objBO.prm_1;
                    cmd.Parameters.Add("@loginId", SqlDbType.VarChar, 10).Value = objBO.login_id;
                    cmd.Parameters.Add("@logic", SqlDbType.VarChar, 50).Value = objBO.Logic;
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
        public string wh_purchaseOrder_Process(PurchaseBO objBO)
        {
            string processInfo = string.Empty;
            using (SqlConnection con = new SqlConnection(GlobalConfig.ConStr_Hospital))
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand("pwh_purchaseOrder_Process", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandTimeout = 2500;
                    cmd.Parameters.Add("@unit_id", SqlDbType.VarChar, 10).Value = objBO.unit_id;
                    cmd.Parameters.Add("@poNumber", SqlDbType.VarChar, 20).Value = objBO.poNumber;
                    cmd.Parameters.Add("@qty", SqlDbType.Int, 20).Value = objBO.qty;
                    cmd.Parameters.Add("@prm1", SqlDbType.VarChar, 50).Value = objBO.prm_1;
                    cmd.Parameters.Add("@prm2", SqlDbType.VarChar, 50).Value = objBO.prm_2;
                    cmd.Parameters.Add("@loginId", SqlDbType.VarChar, 10).Value = objBO.login_id;
                    cmd.Parameters.Add("@logic", SqlDbType.VarChar, 50).Value = objBO.Logic;
                    cmd.Parameters.Add("@result", SqlDbType.VarChar, 200).Value = "";
                    cmd.Parameters["@result"].Direction = ParameterDirection.InputOutput;
                    try
                    {
                        cmd.ExecuteNonQuery();
                        processInfo = (string)cmd.Parameters["@result"].Value.ToString();
                        if (processInfo == "Success")
                        {
                            processInfo = (string)cmd.Parameters["@Purch_id"].Value.ToString();
                        }
                    }
                    catch (Exception sqlEx)
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