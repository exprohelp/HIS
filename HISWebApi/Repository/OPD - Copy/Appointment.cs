using HISWebApi.Models;
using HISWebApi.Repository.Patient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace HISWebApi.Repository.OPD
{
	public class Appointment
	{
		public dataSet Referral_Search(ReferralSearch objBO)
		{
			dataSet dsObj = new dataSet();
			using (SqlConnection con = new SqlConnection(GlobalConfig.ConStr_Hospital))
			{
				using (SqlCommand cmd = new SqlCommand("pReferral_Search", con))
				{
					cmd.CommandType = CommandType.StoredProcedure;
					cmd.CommandTimeout = 2500;
					cmd.Parameters.Add("@hosp_id", SqlDbType.VarChar, 10).Value = objBO.hosp_id;
					cmd.Parameters.Add("@referral_id", SqlDbType.VarChar, 10).Value = objBO.referral_id;
					cmd.Parameters.Add("@referral_name", SqlDbType.VarChar, 50).Value = objBO.referral_name;
					cmd.Parameters.Add("@prm_1", SqlDbType.VarChar, 50).Value = objBO.prm_1;
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

		public dataSet Opd_AppointmentQueries(AppointmentQueries objBO)
		{
			dataSet dsObj = new dataSet();
			using (SqlConnection con = new SqlConnection(GlobalConfig.ConStr_Hospital))
			{
				using (SqlCommand cmd = new SqlCommand("pOpd_AppointmentQueries", con))
				{
					cmd.CommandType = CommandType.StoredProcedure;
					cmd.CommandTimeout = 2500;
					cmd.Parameters.Add("@SearcKey", SqlDbType.VarChar, 50).Value = objBO.SearcKey;
					cmd.Parameters.Add("@SearchValue", SqlDbType.VarChar, 100).Value = objBO.SearchValue;
					cmd.Parameters.Add("@Hosp_Id", SqlDbType.VarChar, 10).Value = objBO.Hosp_Id;
					cmd.Parameters.Add("@UHID", SqlDbType.VarChar, 20).Value = objBO.UHID;
					cmd.Parameters.Add("@AppointmentId", SqlDbType.VarChar, 20).Value = objBO.AppointmentId;
					cmd.Parameters.Add("@DoctorId", SqlDbType.VarChar, 10).Value = objBO.DoctorId;
					cmd.Parameters.Add("@DeptId", SqlDbType.VarChar, 10).Value = objBO.DeptId;
					cmd.Parameters.Add("@PanelId", SqlDbType.VarChar, 10).Value = objBO.PanelId;
					cmd.Parameters.Add("@ItemId", SqlDbType.VarChar, 15).Value = objBO.ItemId;
					cmd.Parameters.Add("@prm_1", SqlDbType.VarChar).Value = objBO.prm_1;
					cmd.Parameters.Add("@from", SqlDbType.DateTime, 20).Value = objBO.from;
					cmd.Parameters.Add("@to", SqlDbType.DateTime, 20).Value = objBO.to;
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
		public dataSet Opd_AppointmentSearch(AppointmentQueries objBO)
		{
			dataSet dsObj = new dataSet();
			using (SqlConnection con = new SqlConnection(GlobalConfig.ConStr_Hospital))
			{
				using (SqlCommand cmd = new SqlCommand("pOpd_AppointmentSearch", con))
				{
					cmd.CommandType = CommandType.StoredProcedure;
					cmd.CommandTimeout = 2500;
					cmd.Parameters.Add("@SearcKey", SqlDbType.VarChar, 50).Value = objBO.SearcKey;
					cmd.Parameters.Add("@SearchValue", SqlDbType.VarChar, 100).Value = objBO.SearchValue;
					cmd.Parameters.Add("@Hosp_Id", SqlDbType.VarChar, 10).Value = objBO.Hosp_Id;
					cmd.Parameters.Add("@UHID", SqlDbType.VarChar, 20).Value = objBO.UHID;
					cmd.Parameters.Add("@AppointmentId", SqlDbType.VarChar, 20).Value = objBO.AppointmentId;
					cmd.Parameters.Add("@DoctorId", SqlDbType.VarChar, 10).Value = objBO.DoctorId;
					cmd.Parameters.Add("@DeptId", SqlDbType.VarChar, 10).Value = objBO.DeptId;
					cmd.Parameters.Add("@prm_1", SqlDbType.VarChar).Value = objBO.prm_1;
					cmd.Parameters.Add("@from", SqlDbType.DateTime, 20).Value = objBO.from;
					cmd.Parameters.Add("@to", SqlDbType.DateTime, 20).Value = objBO.to;
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
		public dataSet Patient_MasterQueries(PatientMasterQueries objBO)
		{
			dataSet dsObj = new dataSet();
			using (SqlConnection con = new SqlConnection(GlobalConfig.ConStr_Hospital))
			{
				using (SqlCommand cmd = new SqlCommand("pPatient_MasterQueries", con))
				{
					cmd.CommandType = CommandType.StoredProcedure;
					cmd.CommandTimeout = 2500;
					cmd.Parameters.Add("@Hosp_Id", SqlDbType.VarChar, 20).Value = objBO.hosp_id;
					cmd.Parameters.Add("@UHID", SqlDbType.VarChar, 20).Value = objBO.UHID;
					cmd.Parameters.Add("@MobileNo", SqlDbType.VarChar, 10).Value = objBO.MobileNo;
					cmd.Parameters.Add("@SearcKey", SqlDbType.VarChar, 50).Value = objBO.SearcKey;
					cmd.Parameters.Add("@SearchValue", SqlDbType.VarChar, 100).Value = objBO.SearchValue;
					cmd.Parameters.Add("@from", SqlDbType.DateTime, 20).Value = objBO.from;
					cmd.Parameters.Add("@to", SqlDbType.DateTime, 20).Value = objBO.to;
					cmd.Parameters.Add("@prm_1", SqlDbType.VarChar, 20).Value = objBO.prm_1;
					cmd.Parameters.Add("@prm_2", SqlDbType.VarChar, 20).Value = objBO.prm_2;
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
		public string Opd_InsertAppointmentAssets(AppointmentBO objBO)
		{
			string processInfo = string.Empty;
			using (SqlConnection con = new SqlConnection(GlobalConfig.ConStr_Hospital))
			{
				con.Open();
				using (SqlCommand cmd = new SqlCommand("pOpd_InsertAppointmentAssets", con))
				{
					cmd.CommandType = CommandType.StoredProcedure;
					cmd.CommandTimeout = 2500;
					cmd.Parameters.Add("@ref_code", SqlDbType.VarChar, 10).Value = objBO.ref_code;
					cmd.Parameters.Add("@ref_name", SqlDbType.VarChar, 50).Value = objBO.ref_name;
					cmd.Parameters.Add("@visit_purpose", SqlDbType.VarChar, 50).Value = objBO.visit_purpose;
					cmd.Parameters.Add("@degree", SqlDbType.VarChar, 50).Value = objBO.degree;
					cmd.Parameters.Add("@mobile_no", SqlDbType.VarChar, 10).Value = objBO.mobile_no;
					cmd.Parameters.Add("@prm_1", SqlDbType.VarChar, 100).Value = objBO.prm_1;
					cmd.Parameters.Add("@c_address", SqlDbType.VarChar, 100).Value = objBO.c_address;
					cmd.Parameters.Add("@c_city", SqlDbType.VarChar, 50).Value = objBO.c_city;
					cmd.Parameters.Add("@state", SqlDbType.VarChar, 50).Value = objBO.state;
					cmd.Parameters.Add("@hosp_id", SqlDbType.VarChar, 10).Value = objBO.hosp_id;
					cmd.Parameters.Add("@SourceName", SqlDbType.NVarChar, 100).Value = objBO.SourceName;
					cmd.Parameters.Add("@IsActive", SqlDbType.NVarChar, 8).Value = objBO.IsActive;
					cmd.Parameters.Add("@login_id", SqlDbType.NVarChar).Value = objBO.login_id;
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
		public string Opd_AppointmentBooking(PatientMasterBO objPatient, AppointmentBookingBO objBooking, List<PaymentBO> objPayment)
		{
			PatientMaster pm = new PatientMaster();
			string processInfo = string.Empty;
			string processInfo1 = string.Empty;
			if (objBooking.visitType.Contains("Walk-In"))
			{
				string UHID = pm.InsertPatientMaster(out processInfo1, objPatient);
				objBooking.UHID = UHID;
			}
			else
			{
				processInfo1 = "Success";
			}
			if (processInfo1.Contains("Success"))
			{
				DataTable dt = new DataTable();
				dt.Columns.Add("ReceiptNo", typeof(string));
				dt.Columns.Add("PayMode", typeof(string));
				dt.Columns.Add("CardNo", typeof(string));
				dt.Columns.Add("BankName", typeof(string));
				dt.Columns.Add("RefNo", typeof(string));
				dt.Columns.Add("MachineId", typeof(string));
				dt.Columns.Add("MachineName", typeof(string));
				dt.Columns.Add("Amount", typeof(decimal));
				dt.Columns.Add("OnlPaymentId", typeof(string));
				dt.Columns.Add("OnlPayStatus", typeof(string));
				dt.Columns.Add("OnlPayResponse", typeof(string));
				dt.Columns.Add("OnlPaymentDate", typeof(DateTime));
				dt.Columns.Add("login_id", typeof(string));
				foreach (PaymentBO obj in objPayment)
				{
					DataRow dr = dt.NewRow();
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
					dt.Rows.Add(dr);
				}
				using (SqlConnection con = new SqlConnection(GlobalConfig.ConStr_Hospital))
				{
					con.Open();
					using (SqlCommand cmd = new SqlCommand("pOpd_AppointmentBooking", con))
					{
						TimeSpan AppInTime = TimeSpan.Parse(objBooking.AppInTime);
						TimeSpan AppOutTime = TimeSpan.Parse(objBooking.AppOutTime);
						cmd.CommandType = CommandType.StoredProcedure;
						cmd.CommandTimeout = 2500;
						cmd.Parameters.Add("@hosp_id", SqlDbType.VarChar, 10).Value = objBooking.hosp_id;
						cmd.Parameters.Add("@UHID", SqlDbType.VarChar, 20).Value = objBooking.UHID;
						cmd.Parameters.Add("@IsNewPatient", SqlDbType.VarChar, 20).Value = objBooking.IsNewPatient;
						cmd.Parameters.Add("@patient_name", SqlDbType.VarChar, 100).Value = objBooking.patient_name;
						cmd.Parameters.Add("@email", SqlDbType.VarChar, 50).Value = objBooking.email;
						cmd.Parameters.Add("@age", SqlDbType.VarChar, 20).Value = objBooking.age;
						cmd.Parameters.Add("@ageType", SqlDbType.VarChar, 20).Value = objBooking.ageType;
						cmd.Parameters.Add("@gender", SqlDbType.VarChar, 30).Value = objBooking.gender;
						cmd.Parameters.Add("@mobile_no", SqlDbType.VarChar, 10).Value = objBooking.mobile_no;
						cmd.Parameters.Add("@DoctorId", SqlDbType.VarChar, 10).Value = objBooking.DoctorId;
						cmd.Parameters.Add("@AppDate", SqlDbType.DateTime, 20).Value = objBooking.AppDate;
						cmd.Parameters.Add("@AppInTime", SqlDbType.Time, 20).Value = AppInTime;
						cmd.Parameters.Add("@AppOutTime", SqlDbType.Time, 30).Value = AppOutTime;
						cmd.Parameters.Add("@ItemId", SqlDbType.VarChar, 20).Value = objBooking.ItemId;
						cmd.Parameters.Add("@visitType", SqlDbType.VarChar, 50).Value = objBooking.visitType;
						cmd.Parameters.Add("@visitSource", SqlDbType.VarChar, 100).Value = objBooking.visitSource;
						cmd.Parameters.Add("@visit_purpose", SqlDbType.VarChar, 500).Value = objBooking.visit_purpose;
						cmd.Parameters.Add("@GrossAmount", SqlDbType.Decimal, 20).Value = objBooking.GrossAmount;
						cmd.Parameters.Add("@discount", SqlDbType.Decimal, 50).Value = objBooking.discount;
						cmd.Parameters.Add("@discount_remark", SqlDbType.VarChar, 200).Value = objBooking.discount_remark;
						cmd.Parameters.Add("@discountType", SqlDbType.VarChar, 100).Value = objBooking.discountType;
						cmd.Parameters.Add("@discountBy", SqlDbType.VarChar, 50).Value = objBooking.discountBy;
						cmd.Parameters.Add("@ipAddress", SqlDbType.VarChar, 30).Value = objBooking.ipAddress;
						cmd.Parameters.Add("@login_id", SqlDbType.VarChar, 20).Value = objBooking.login_id;
						cmd.Parameters.Add("@refDoctorCode", SqlDbType.VarChar, 20).Value = objBooking.refDoctorCode;
						cmd.Parameters.Add("@PanelId", SqlDbType.VarChar, 50).Value = objBooking.PanelId;
						cmd.Parameters.Add("@RateList_Id", SqlDbType.VarChar, 20).Value = objBooking.RateList_Id;
						cmd.Parameters.Add("@IsConfirmed", SqlDbType.Bit).Value = objBooking.IsConfirmed;
						cmd.Parameters.Add("@GenFrom", SqlDbType.VarChar, 30).Value = objBooking.GenFrom;
						cmd.Parameters.Add("@online_app_no", SqlDbType.VarChar, 16).Value = objBooking.online_app_no;
						cmd.Parameters.Add("@Logic", SqlDbType.VarChar, 50).Value = objBooking.Logic;
						cmd.Parameters.AddWithValue("udt_Receipt", dt);
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
		public string Opd_AppointmentCancellation(AppointmentCancellationBO objBO)
		{
			string processInfo = string.Empty;
			using (SqlConnection con = new SqlConnection(GlobalConfig.ConStr_Hospital))
			{
				con.Open();
				using (SqlCommand cmd = new SqlCommand("pOpd_AppointmentCancellation", con))
				{
					cmd.CommandType = CommandType.StoredProcedure;
					cmd.CommandTimeout = 2500;
					cmd.Parameters.Add("@hosp_id", SqlDbType.VarChar, 10).Value = objBO.hosp_id;
					cmd.Parameters.Add("@oldTnxId", SqlDbType.VarChar, 20).Value = objBO.oldTnxId;
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
		public string Opd_AppointmentUpdate(AppointmentReschedule objBO)
		{
			string processInfo = string.Empty;
			using (SqlConnection con = new SqlConnection(GlobalConfig.ConStr_Hospital))
			{
				con.Open();
				using (SqlCommand cmd = new SqlCommand("pOpd_AppointmentUpdate", con))
				{
					TimeSpan AppInTime = TimeSpan.Parse(objBO.AppInTime);
					TimeSpan AppOutTime = TimeSpan.Parse(objBO.AppOutTime);
					cmd.CommandType = CommandType.StoredProcedure;
					cmd.CommandTimeout = 2500;
					cmd.Parameters.Add("@UHID", SqlDbType.VarChar, 20).Value = objBO.UHID;
					cmd.Parameters.Add("@mobile_no", SqlDbType.VarChar, 10).Value = objBO.mobile_no;
					cmd.Parameters.Add("@DoctorId", SqlDbType.VarChar, 10).Value = objBO.DoctorId;
					cmd.Parameters.Add("@app_no", SqlDbType.VarChar, 30).Value = objBO.app_no;
					cmd.Parameters.Add("@AppDate", SqlDbType.Date, 10).Value = objBO.AppDate;
					cmd.Parameters.Add("@AppInTime", SqlDbType.Time, 10).Value = objBO.AppInTime;
					cmd.Parameters.Add("@AppOutTime", SqlDbType.Time, 10).Value = objBO.AppOutTime;
					cmd.Parameters.Add("@prm_1", SqlDbType.VarChar, 50).Value = objBO.prm_1;
					cmd.Parameters.Add("@Prm_2", SqlDbType.VarChar, 50).Value = objBO.Prm_2;
					cmd.Parameters.Add("@PanelId", SqlDbType.VarChar, 20).Value = objBO.PanelId;
					cmd.Parameters.Add("@login_id", SqlDbType.VarChar, 10).Value = objBO.login_id;
					cmd.Parameters.Add("@Logic", SqlDbType.VarChar, 50).Value = objBO.Logic;
					cmd.Parameters.Add("@result", SqlDbType.VarChar, 50).Value = "";
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