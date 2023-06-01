using HISWebApi.Models;
using HISWebApi.Repository.Patient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;


namespace HISWebApi.Repository.OPD
{
	public class Service
	{
		//Service Bookimng	
		public dataSet Opd_ServiceQueries(ServiceQueries objBO)
		{
			dataSet dsObj = new dataSet();
			using (SqlConnection con = new SqlConnection(GlobalConfig.ConStr_Hospital))
			{
				using (SqlCommand cmd = new SqlCommand("pOpd_ServiceQueries", con))
				{
					cmd.CommandType = CommandType.StoredProcedure;
					cmd.CommandTimeout = 2500;
					cmd.Parameters.Add("@hosp_id", SqlDbType.VarChar, 10).Value = objBO.hosp_id;
					cmd.Parameters.Add("@UHID", SqlDbType.VarChar, 20).Value = objBO.UHID;
					cmd.Parameters.Add("@doctorId", SqlDbType.VarChar, 10).Value = objBO.DoctorId;
					cmd.Parameters.Add("@PanelId", SqlDbType.VarChar, 50).Value = objBO.PanelId;
					cmd.Parameters.Add("@SearchKey", SqlDbType.VarChar, 100).Value = objBO.SearcKey;
					cmd.Parameters.Add("@CatId", SqlDbType.VarChar, 10).Value = objBO.CatId;
					cmd.Parameters.Add("@SubCatId", SqlDbType.VarChar, 100).Value = objBO.SubCatId;
					cmd.Parameters.Add("@SearchValue", SqlDbType.VarChar, 100).Value = objBO.SearchValue;
					cmd.Parameters.Add("@CouponCode", SqlDbType.VarChar, 50).Value = objBO.CouponCode;
					cmd.Parameters.Add("@ItemId", SqlDbType.VarChar, 15).Value = objBO.ItemId;
					cmd.Parameters.Add("@prm_1", SqlDbType.VarChar).Value = objBO.prm_1;
					cmd.Parameters.Add("@from", SqlDbType.DateTime, 20).Value = objBO.from;
					cmd.Parameters.Add("@to", SqlDbType.DateTime, 20).Value = objBO.to;
					cmd.Parameters.Add("@ItemIds", SqlDbType.VarChar, 5000).Value = objBO.ItemId;
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
		public dataSet Opd_ServiceRates(ServiceQueries objBO)
		{
			dataSet dsObj = new dataSet();
			using (SqlConnection con = new SqlConnection(GlobalConfig.ConStr_Hospital))
			{
				using (SqlCommand cmd = new SqlCommand("pOpd_ServiceRates", con))
				{
					cmd.CommandType = CommandType.StoredProcedure;
					cmd.CommandTimeout = 2500;
					cmd.Parameters.Add("@hosp_id", SqlDbType.VarChar, 10).Value = objBO.hosp_id;
					cmd.Parameters.Add("@UHID", SqlDbType.VarChar, 20).Value = objBO.UHID;
					cmd.Parameters.Add("@doctorId", SqlDbType.VarChar, 10).Value = objBO.DoctorId;
					cmd.Parameters.Add("@PanelId", SqlDbType.VarChar, 50).Value = objBO.PanelId;
					cmd.Parameters.Add("@ItemIds", SqlDbType.VarChar, 5000).Value = objBO.ItemId;
					cmd.Parameters.Add("@login_id", SqlDbType.VarChar, 10).Value = objBO.login_id;
					cmd.Parameters.Add("@logic", SqlDbType.VarChar, 30).Value = objBO.logic;
					cmd.Parameters.Add("@CPT_Code", SqlDbType.VarChar, 10).Value = objBO.CPT_Code;
					cmd.Parameters.Add("@SearchType", SqlDbType.VarChar, 50).Value = objBO.SearchType;
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
		public string Opd_ServiceBooking(PatientMasterBO objPatient, ServiceBookingBO objBooking, List<ServiceItems> objRateList, List<PaymentBO> objPayment)
		{
			PatientMaster pm = new PatientMaster();
			string processInfo = string.Empty;
			string processInfo1 = string.Empty;
			string UHID = pm.InsertPatientMaster(out processInfo1, objPatient);
	    	objBooking.UHID = UHID;
			if (processInfo1.Contains("Success"))
			{
				DataTable dt1 = new DataTable();
				dt1.Columns.Add("RateListId", typeof(string));
				dt1.Columns.Add("RateListName", typeof(string));
				dt1.Columns.Add("ItemId", typeof(string));
				dt1.Columns.Add("ItemSection", typeof(string));
				dt1.Columns.Add("IsPackage", typeof(int));
				dt1.Columns.Add("Rate", typeof(decimal));
				dt1.Columns.Add("panel_discount", typeof(decimal));
				dt1.Columns.Add("panel_rate", typeof(decimal));
				dt1.Columns.Add("qty", typeof(int));
				dt1.Columns.Add("adl_disc_amount", typeof(decimal));
				dt1.Columns.Add("adl_disc_perc", typeof(decimal));
				dt1.Columns.Add("net_amount", typeof(decimal));
				dt1.Columns.Add("IsUrgent", typeof(char));
				foreach (ServiceItems objRate in objRateList)
				{
					DataRow dr = dt1.NewRow();
					dr["RateListId"] = objRate.RateListId;
					dr["RateListName"] = objRate.RateListName;
					dr["ItemId"] = objRate.ItemId;
					dr["ItemSection"] = objRate.ItemSection;
					dr["IsPackage"] = objRate.IsPackage;
					dr["Rate"] = objRate.Rate;
					dr["panel_discount"] = objRate.panel_discount;
					dr["panel_rate"] = objRate.panel_rate;
					dr["qty"] = objRate.qty;
					dr["adl_disc_amount"] = objRate.adl_disc_amount;
					dr["adl_disc_perc"] = objRate.adl_disc_perc;
					dr["net_amount"] = objRate.net_amount;
					dr["IsUrgent"] = objRate.IsUrgent;
					dt1.Rows.Add(dr);
				}
				if (processInfo1.Contains("Success"))
				{
					DataTable dt2 = new DataTable();
					dt2.Columns.Add("ReceiptNo", typeof(string));
					dt2.Columns.Add("PayMode", typeof(string));
					dt2.Columns.Add("CardNo", typeof(string));
					dt2.Columns.Add("BankName", typeof(string));
					dt2.Columns.Add("RefNo", typeof(string));
					dt2.Columns.Add("MachineId", typeof(string));
					dt2.Columns.Add("MachineName", typeof(string));
					dt2.Columns.Add("Amount", typeof(decimal));
					dt2.Columns.Add("OnlPaymentId", typeof(string));
					dt2.Columns.Add("OnlPayStatus", typeof(string));
					dt2.Columns.Add("OnlPayResponse", typeof(string));
					dt2.Columns.Add("OnlPaymentDate", typeof(DateTime));
					dt2.Columns.Add("login_id", typeof(string));
					foreach (PaymentBO obj in objPayment)
					{
						DataRow dr = dt2.NewRow();
						dr["ReceiptNo"] = obj.ReceiptNo;
						dr["PayMode"] = obj.PayMode;
						dr["CardNo"] = obj.CardNo;
						dr["BankName"] = obj.BankName;
						dr["RefNo"] = obj.RefNo;
						dr["MachineId"] = obj.MachineId;
						dr["MachineName"] = obj.MachineName;
						dr["Amount"] = obj.Amount;
						dr["OnlPaymentId"] = obj.OnlPaymentId;
						dr["OnlPayStatus"] = obj.OnlPayStatus;
						dr["OnlPayResponse"] = obj.OnlPayResponse;
						dr["OnlPaymentDate"] = obj.OnlPaymentDate;
						dr["login_id"] = obj.login_id;
						dt2.Rows.Add(dr);
					}
					using (SqlConnection con = new SqlConnection(GlobalConfig.ConStr_Hospital))
					{
						con.Open();
						using (SqlCommand cmd = new SqlCommand("pOpd_ServiceBooking", con))
						{
							cmd.CommandType = CommandType.StoredProcedure;
							cmd.CommandTimeout = 2500;
							cmd.Parameters.Add("@hosp_id", SqlDbType.VarChar, 10).Value = objBooking.hosp_id;
							cmd.Parameters.Add("@UHID", SqlDbType.VarChar, 20).Value = objBooking.UHID;
							cmd.Parameters.Add("@IsNewPatient", SqlDbType.VarChar, 20).Value = objBooking.IsNewPatient;
							cmd.Parameters.Add("@patient_name", SqlDbType.VarChar, 100).Value = objBooking.patient_name;
							cmd.Parameters.Add("@email", SqlDbType.VarChar, 30).Value = objBooking.email_id;
							cmd.Parameters.Add("@age", SqlDbType.Decimal, 20).Value = objBooking.age;
							cmd.Parameters.Add("@ageType", SqlDbType.VarChar, 20).Value = objBooking.ageType;
							cmd.Parameters.Add("@gender", SqlDbType.VarChar, 20).Value = objBooking.gender;
							cmd.Parameters.Add("@dob", SqlDbType.Date, 10).Value = objBooking.dob;
							cmd.Parameters.Add("@ageInDays", SqlDbType.Int, 10).Value = objBooking.ageInDays;
							cmd.Parameters.Add("@mobile_no", SqlDbType.VarChar, 10).Value = objBooking.mobile_no;
							cmd.Parameters.Add("@IDProofType", SqlDbType.VarChar, 50).Value = objBooking.IDProofType;
							cmd.Parameters.Add("@IDProofNO", SqlDbType.VarChar, 50).Value = objBooking.IDProofNO;
							cmd.Parameters.Add("@DoctorId", SqlDbType.VarChar, 10).Value = objBooking.DoctorId;
							cmd.Parameters.Add("@visitSource", SqlDbType.VarChar, 100).Value = objBooking.visitSource;
							cmd.Parameters.Add("@GrossAmount", SqlDbType.Decimal, 20).Value = objBooking.GrossAmount;
							cmd.Parameters.Add("@discount", SqlDbType.Decimal, 20).Value = objBooking.discount;
							cmd.Parameters.Add("@coPayAmount", SqlDbType.Decimal, 20).Value = objBooking.coPayAmount;
							cmd.Parameters.Add("@discountBy", SqlDbType.VarChar, 50).Value = objBooking.discountBy;
							cmd.Parameters.Add("@discountType", SqlDbType.VarChar, 100).Value = objBooking.discountType;
							cmd.Parameters.Add("@discount_remark", SqlDbType.VarChar, 200).Value = objBooking.discount_remark;
							cmd.Parameters.Add("@login_id", SqlDbType.VarChar, 20).Value = objBooking.login_id;
							cmd.Parameters.Add("@refDoctorCode", SqlDbType.VarChar, 20).Value = objBooking.refDoctorCode;
							cmd.Parameters.Add("@PanelId", SqlDbType.VarChar, 50).Value = objBooking.PanelId;
							cmd.Parameters.Add("@GenFrom", SqlDbType.VarChar, 30).Value = objBooking.GenFrom;
							cmd.Parameters.Add("@ipAddress", SqlDbType.VarChar, 30).Value = objBooking.ipAddress;
							cmd.Parameters.Add("@Logic", SqlDbType.VarChar, 20).Value = objBooking.Logic;
							cmd.Parameters.AddWithValue("UDT_ServiceItems", dt1);
							cmd.Parameters.AddWithValue("udt_Receipt", dt2);
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
			}
			return processInfo;
		}
		public string Opd_ServiceRefund(ServiceRefund objBO)
		{
			string processInfo = string.Empty;
			using (SqlConnection con = new SqlConnection(GlobalConfig.ConStr_Hospital))
			{
				con.Open();
				using (SqlCommand cmd = new SqlCommand("pOpd_ServiceRefund", con))
				{
					cmd.CommandType = CommandType.StoredProcedure;
					cmd.CommandTimeout = 2500;
					cmd.Parameters.Add("@hosp_id", SqlDbType.VarChar, 10).Value = objBO.hosp_id;
					cmd.Parameters.Add("@VisitNo", SqlDbType.VarChar, 20).Value = objBO.VisitNo;
					cmd.Parameters.Add("@CancelReason", SqlDbType.VarChar, 200).Value = objBO.CancelReason;
					cmd.Parameters.Add("@ItemIdList", SqlDbType.VarChar, 200).Value = objBO.ItemIdList;
					cmd.Parameters.Add("@amount", SqlDbType.Decimal, 20).Value = objBO.amount;
					cmd.Parameters.Add("@PayMode", SqlDbType.VarChar, 40).Value = objBO.PayMode;
					cmd.Parameters.Add("@IPAddress", SqlDbType.VarChar, 25).Value = objBO.IPAddress;
					cmd.Parameters.Add("@login_id", SqlDbType.VarChar, 10).Value = objBO.login_id;
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
	}
}