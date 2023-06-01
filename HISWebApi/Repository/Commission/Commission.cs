using HISWebApi.Models;
using HISWebApi.Repository.IPD;
using System;
using System.Data;
using System.Data.SqlClient;

namespace HISWebApi.Repository.Commission
{
	public class Commission
	{
		HISDBLayer his_layer = new HISDBLayer();

        public object DbManager { get; private set; }

        public dataSet GetPatientDetailByIPDNo(string IPDNo)
        {
            string _result = string.Empty;
            return his_layer.GetPatientDetailByIPDNo(IPDNo);
        }
        public dataSet GetPatientDetailByUHID(string uhid)
		{
			string _result = string.Empty;
			return his_layer.GetPatientDetailByUHID(uhid);
		}
		public dataSet HIS_ShareQueries(ReferralCommissionBO objBO)
		{
			dataSet dsObj = new dataSet();
			using (SqlConnection con = new SqlConnection(GlobalConfig.ConStr_ItdoseDataByChandan))
			{
				using (SqlCommand cmd = new SqlCommand("pHIS_ShareQueries", con))
				{
					cmd.CommandType = CommandType.StoredProcedure;
					cmd.CommandTimeout = 2500;
					cmd.Parameters.Add("@trn_type", SqlDbType.VarChar, 10).Value = objBO.trn_type;
					cmd.Parameters.Add("@from", SqlDbType.VarChar, 10).Value = objBO.from;
					cmd.Parameters.Add("@to", SqlDbType.VarChar, 10).Value = objBO.to;
					cmd.Parameters.Add("@prm_1", SqlDbType.VarChar, 50).Value = objBO.prm_1;
					cmd.Parameters.Add("@prm_2", SqlDbType.VarChar, 50).Value = objBO.prm_2;
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
		public string HIS_InsertReferralcommission(ReferralCommissionBO objBO)
		{
			string processInfo = string.Empty;
			using (SqlConnection con = new SqlConnection(GlobalConfig.ConStr_ItdoseDataByChandan))
			{
				con.Open();
				using (SqlCommand cmd = new SqlCommand("pHIS_InsertReferralcommission", con))
				{
					cmd.CommandType = CommandType.StoredProcedure;
					cmd.CommandTimeout = 2500;
					cmd.Parameters.Add("@IPDNo", SqlDbType.VarChar, 10).Value = objBO.IPDNo;
					cmd.Parameters.Add("@ref_code", SqlDbType.VarChar, 10).Value = objBO.ref_code;
					cmd.Parameters.Add("@patient_name", SqlDbType.VarChar, 100).Value = objBO.patient_name;
					cmd.Parameters.Add("@gender", SqlDbType.VarChar, 20).Value = objBO.gender;
					cmd.Parameters.Add("@commission_Perc", SqlDbType.Decimal, 18).Value = objBO.commission_Perc;
					cmd.Parameters.Add("@commission_amount", SqlDbType.Decimal, 18).Value = objBO.commission_amount;
					cmd.Parameters.Add("@login_id", SqlDbType.VarChar, 10).Value = objBO.login_id;
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
        public dataSet HIS_DashboardQueries(ReferralCommissionBO objBO)
        {
            dataSet dsObj = new dataSet();
            using (SqlConnection con = new SqlConnection(GlobalConfig.ConStr_ItdoseDataByChandan))
            {
                using (SqlCommand cmd = new SqlCommand("pHIS_DashboardQueries", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandTimeout = 2500;
                    cmd.Parameters.Add("@unit_id", SqlDbType.VarChar, 10).Value = "";
                    cmd.Parameters.Add("@from", SqlDbType.DateTime).Value = objBO.from;
                    cmd.Parameters.Add("@to", SqlDbType.DateTime).Value = objBO.to;
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
        public string Sync_IPDDatafromHISToChandan(string IPDNo, string RefCode, string loginId,decimal commissionPerc)
		{
			string result = string.Empty;
			string processInfo = string.Empty;
           	ReferralCommissionBO t = new ReferralCommissionBO();
			t.prm_1 = IPDNo;
			t.Logic = "CheckFinalShareAvailability";
            t.prm_2 = loginId;
			dataSet dsChk = HIS_ShareQueries(t);
			if(dsChk.ResultSet.Tables.Count > 0 && dsChk.ResultSet.Tables[0].Rows.Count > 0)
			{
                if(dsChk.ResultSet.Tables[0].Rows[0]["allow_process"].ToString() == "1")
                {
                    if (dsChk.ResultSet.Tables[0].Rows[0]["shareCount"].ToString() == "0")
                    {
                        DataSet ds = new DataSet();
                        string qry = string.Empty;
                        qry += "select ltd.*,cm.CategoryID,cm.NAME CategoryName,sc.SubCategoryId,sc.NAME SubCatName,ltd.ItemId,fim.TypeName ItemName,CONCAT(dm.title,' ',dm.NAME) doctorName,pm. Company_Name PanelName,CONCAT(dr.title,' ',dr.NAME) ref_name from  ";
                        qry += "( ";
                        qry += " SELECT 'IPD' TnxType,pmh.ID IPOPNo,ltd.LedgerTnxId UniqId,ltd.EntryDate,adj.billDate,ltd.LedgerTransactionNo,ltd.Transaction_ID,ltd.Doctor_Id,ltd.ItemId,IFNULL(Rate,0) Rate,IFNULL((ltd.Rate*ltd.Quantity),0) GrossAmt,IFNULL(DiscAmt,0) DiscAmt ,  ";
                        qry += "IFNULL(ROUND(ltd.Amount-(ltd.Amount * ROUND(IFNULL(adj.DiscountOnBill,0)*100/IFNULL((SELECT SUM(Amount) FROM f_ledgertnxdetail  WHERE transaction_ID=ltd.transaction_ID AND isverified<>2),0),4)*.01)),0) Amount,IFNULL(Quantity,0) Quantity  ";
                        qry += " ,ISPackage,adj.PanelID,adj.Patient_ID,Pm.PName,pm.Mobile,pm.Age,pm.City,IFNULL(adj.TotalBilledAmt,0)TotalBilledAmt ,IFNULL(adj.DiscountOnBill,0) DiscountOnBill,IFNULL(ltd.DiscountPercentage,0) DiscountPercentage,adj.BillNo,pmh.referedById,ltd.TypeOfTnx  ";
                        qry += " FROM f_ledgertnxdetail ltd INNER JOIN f_ipdadjustment adj ON adj.transaction_ID = ltd.Transaction_ID  ";
                        qry += " INNER JOIN patient_medical_history pmh ON pmh.transaction_ID = ltd.Transaction_ID  ";
                        qry += " INNER JOIN patient_master PM ON pm.Patient_ID = adj.Patient_ID  ";
                        qry += " WHERE ltd.isverified = 1 AND adj.CentreID IN (1) AND pmh.ID=" + IPDNo + " ";
                        qry += " ) ltd LEFT OUTER JOIN f_itemmaster fim ON fim.ItemID=ltd.ItemId  ";
                        qry += "LEFT OUTER JOIN f_subcategorymaster sc ON fim.SubCategoryID=sc.SubCategoryID  ";
                        qry += "LEFT OUTER JOIN f_categorymaster cm  ON cm.CategoryID = sc.CategoryID  ";
                        qry += "LEFT OUTER JOIN doctor_master  dm ON dm.Doctor_Id = ltd.Doctor_Id  ";
                        qry += "LEFT OUTER JOIN f_panel_master pm ON pm.panel_ID = ltd.PanelID ";
                        qry += "LEFT OUTER JOIN doctor_referal dr ON dr.Doctor_ID=ltd.Doctor_Id";
                        try
                        {
                            ReferralCommissionBO d = new ReferralCommissionBO();
                            d.prm_1 = IPDNo;
                            d.Logic = "DeleteIPDDetail";
                            HIS_ShareQueries(d);
                            //DbManager.QueryExecute("delete from HIS_RawData where IPOPNo='" + IPDNo + "' ", "ITDoseData");
                         
                            ds = his_layer.ExecuteDataset(qry).ResultSet;
                            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                            {
                                SqlConnection sqlcon = new SqlConnection(GlobalConfig.ConStr_ItdoseDataByChandan);
                                try
                                {
                                    sqlcon.Open();
                                    foreach (DataRow dr in ds.Tables[0].Rows)
                                    {
                                        result = Insert_Modify_HIS_RawData(sqlcon, dr["TnxType"].ToString(), dr["UniqId"].ToString(), dr["IPOPNo"].ToString(), dr["entryDate"].ToString(), dr["billDate"].ToString(), dr["LedgerTransactionNo"].ToString(), dr["Transaction_ID"].ToString(), dr["Doctor_Id"].ToString(), dr["CategoryID"].ToString(), dr["CategoryName"].ToString(), dr["SubCategoryId"].ToString(), dr["SubCatName"].ToString(), dr["ItemId"].ToString(), dr["ItemName"].ToString(), Convert.ToDecimal(dr["Rate"]), Convert.ToDecimal(dr["GrossAmt"]), Convert.ToDecimal(dr["DiscAmt"]), Convert.ToDecimal(dr["Amount"]), Convert.ToDecimal(dr["Quantity"]), Convert.ToDecimal(dr["TotalBilledAmt"]), Convert.ToDecimal(dr["DiscountOnBill"]), Convert.ToDecimal(dr["DiscountPercentage"]), dr["BillNo"].ToString(), dr["referedById"].ToString(), dr["ref_name"].ToString(), dr["ISPackage"].ToString(), dr["PanelID"].ToString(), dr["Patient_ID"].ToString(), dr["PName"].ToString(), dr["Mobile"].ToString(), dr["Age"].ToString(), dr["City"].ToString(), dr["doctorName"].ToString(), dr["PanelName"].ToString(), dr["TypeOfTnx"].ToString(), "Re-Generate");
                                    }
                                    HIS_CalculateReferralCommission(sqlcon, IPDNo, loginId, "Calculate",commissionPerc);
                                    processInfo = "Successfully Processed";
                                }
                                catch (Exception ex) { }
                                finally { sqlcon.Close(); };
                            }
                        }
                        catch (Exception ex) { processInfo = ex.Message; }

                    }
                    else
                    {
                        processInfo = "Hospital doctor share is calculated talk to Administrator";
                    }
                }
                else
                {
                    processInfo = "You are not authorised or it's paid ";
                }
            }
			return processInfo;
		}
		public string Insert_Modify_HIS_RawData(SqlConnection con, string TnxType, string UniqId, string IPOPNo, string entrydate, string billDate, string LedgerTransactionNo, string Transaction_ID, string Doctor_Id, string CategoryID, string CategoryName, string SubCategoryId, string SubCatName, string ItemId, string ItemName, decimal Rate, decimal GrossAmt, decimal DiscAmt, decimal Amount, decimal Quantity, decimal TotalBilledAmt, decimal DiscountOnBill, decimal DiscountPercentage, string BillNo, string referedById, string referedByName, string ISPackage, string PanelID, string Patient_ID, string PName, string Mobile, string Age, string City, string doctorName, string PanelName, string TypeOfTnx, string Logic)
		{
			string processInfo = string.Empty;
			SqlCommand cmd = new SqlCommand("Insert_Modify_HIS_RawData", con);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.CommandTimeout = 2500;
			cmd.Parameters.Add("@TnxType", SqlDbType.VarChar, 30).Value = TnxType;
			cmd.Parameters.Add("@UniqId", SqlDbType.VarChar, 30).Value = UniqId;
			cmd.Parameters.Add("@IPOPNo", SqlDbType.VarChar, 30).Value = IPOPNo;
			cmd.Parameters.Add("@entrydate", SqlDbType.DateTime).Value = entrydate;
			cmd.Parameters.Add("@billDate", SqlDbType.DateTime).Value = billDate;
			cmd.Parameters.Add("@LedgerTransactionNo", SqlDbType.VarChar, 30).Value = LedgerTransactionNo;
			cmd.Parameters.Add("@Transaction_ID", SqlDbType.VarChar, 30).Value = Transaction_ID;
			cmd.Parameters.Add("@Doctor_Id", SqlDbType.VarChar, 30).Value = Doctor_Id;
			cmd.Parameters.Add("@CategoryID", SqlDbType.VarChar, 50).Value = CategoryID;
			cmd.Parameters.Add("@CategoryName", SqlDbType.VarChar, 200).Value = CategoryName;
			cmd.Parameters.Add("@SubCategoryId", SqlDbType.VarChar, 50).Value = SubCategoryId;
			cmd.Parameters.Add("@SubCatName", SqlDbType.VarChar, 300).Value = SubCatName;
			cmd.Parameters.Add("@ItemId", SqlDbType.VarChar, 50).Value = ItemId;
			cmd.Parameters.Add("@ItemName", SqlDbType.VarChar, 200).Value = ItemName;
			cmd.Parameters.Add("@Rate", SqlDbType.Decimal).Value = Rate;
			cmd.Parameters.Add("@GrossAmt", SqlDbType.Decimal).Value = GrossAmt;
			cmd.Parameters.Add("@DiscAmt", SqlDbType.Decimal).Value = DiscAmt;
			cmd.Parameters.Add("@Amount", SqlDbType.Decimal).Value = Amount;
			cmd.Parameters.Add("@Quantity", SqlDbType.Decimal).Value = Quantity;
			cmd.Parameters.Add("@TotalBilledAmt", SqlDbType.Decimal).Value = TotalBilledAmt;
			cmd.Parameters.Add("@DiscountOnBill", SqlDbType.Decimal).Value = DiscountOnBill;
			cmd.Parameters.Add("@DiscountPercentage", SqlDbType.Decimal).Value = DiscountPercentage;
			cmd.Parameters.Add("@BillNo", SqlDbType.VarChar).Value = BillNo;
			cmd.Parameters.Add("@referedById", SqlDbType.VarChar).Value = referedById;
			cmd.Parameters.Add("@referedByName", SqlDbType.VarChar).Value = referedByName;
			cmd.Parameters.Add("@ISPackage", SqlDbType.VarChar).Value = ISPackage;
			cmd.Parameters.Add("@PanelID", SqlDbType.VarChar, 30).Value = PanelID;
			cmd.Parameters.Add("@Patient_ID", SqlDbType.VarChar, 510).Value = Patient_ID;
			cmd.Parameters.Add("@PName", SqlDbType.VarChar, 510).Value = PName;
			cmd.Parameters.Add("@Mobile", SqlDbType.VarChar, 30).Value = Mobile;
			cmd.Parameters.Add("@Age", SqlDbType.VarChar, 30).Value = Age;
			cmd.Parameters.Add("@City", SqlDbType.VarChar, 100).Value = City;
			cmd.Parameters.Add("@doctorName", SqlDbType.VarChar, 100).Value = doctorName;
			cmd.Parameters.Add("@PanelName", SqlDbType.VarChar, 100).Value = PanelName;
			cmd.Parameters.Add("@TypeOfTnx", SqlDbType.VarChar, 100).Value = TypeOfTnx;
			cmd.Parameters.Add("@Logic", SqlDbType.VarChar, 20).Value = Logic;
			cmd.Parameters.Add("@result", SqlDbType.VarChar, 50).Value = "";
			cmd.Parameters["@result"].Direction = ParameterDirection.InputOutput;
			cmd.ExecuteNonQuery();
			processInfo = (string)cmd.Parameters["@result"].Value.ToString();
			return processInfo;
		}
		public string HIS_CalculateReferralCommission(SqlConnection con, string IPDNo, string LoginId, string Logic,decimal commissionPerc)
		{
			string processInfo = string.Empty;
			SqlCommand cmd = new SqlCommand("pHIS_CalculateReferralCommission", con);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.CommandTimeout = 2500;
			cmd.Parameters.Add("@IPDNo", SqlDbType.VarChar, 20).Value = IPDNo;
			cmd.Parameters.Add("@LoginId", SqlDbType.VarChar, 30).Value = LoginId;
			cmd.Parameters.Add("@Logic", SqlDbType.VarChar, 20).Value = Logic;
            cmd.Parameters.Add("@commPerc", SqlDbType.Decimal).Value = commissionPerc;
            cmd.Parameters.Add("@result", SqlDbType.VarChar, 100).Value = "";
			cmd.Parameters["@result"].Direction = ParameterDirection.InputOutput;
			cmd.ExecuteNonQuery();
			processInfo = (string)cmd.Parameters["@result"].Value.ToString();
			return processInfo;
		}
	}
}