using HISWebApi.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace HISWebApi.Repository.OPD
{
	public class Prescription
	{
		public dataSet CPOE_PrescriptionAdviceQueries(PrescriptionBO objBO)
		{
			dataSet dsObj = new dataSet();
			using (SqlConnection con = new SqlConnection(GlobalConfig.ConStr_Hospital))
			{
				using (SqlCommand cmd = new SqlCommand("pCPOE_PrescriptionAdviceQueries", con))
				{
					cmd.CommandType = CommandType.StoredProcedure;
					cmd.CommandTimeout = 2500;
					cmd.Parameters.Add("@app_no", SqlDbType.VarChar, 20).Value = objBO.app_no;
					cmd.Parameters.Add("@DeptId", SqlDbType.VarChar, 10).Value = objBO.DeptId;
					cmd.Parameters.Add("@TemplateType", SqlDbType.VarChar, 50).Value = objBO.TemplateType;
					cmd.Parameters.Add("@FilterType", SqlDbType.VarChar, 50).Value = objBO.FilterType;
					cmd.Parameters.Add("@TemplateId", SqlDbType.VarChar, 10).Value = objBO.TemplateId;
					cmd.Parameters.Add("@DoctorId", SqlDbType.VarChar, 10).Value = objBO.DoctorId;
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
		public string CPOE_InsertUpdateAdviceProcess(AdviceProcessBO objBO)
		{
			string processInfo = string.Empty;
			using (SqlConnection con = new SqlConnection(GlobalConfig.ConStr_Hospital))
			{
				con.Open();
				using (SqlCommand cmd = new SqlCommand("pCPOE_InsertUpdateAdviceProcess", con))
				{
					cmd.CommandType = CommandType.StoredProcedure;
					cmd.CommandTimeout = 2500;
					cmd.Parameters.Add("@UHID", SqlDbType.VarChar, 10).Value = objBO.UHID;
					cmd.Parameters.Add("@TemplateType", SqlDbType.VarChar, 10).Value = objBO.TemplateType;
					cmd.Parameters.Add("@TemplateId", SqlDbType.VarChar, 10).Value = objBO.TemplateId;
					cmd.Parameters.Add("@TemplateName", SqlDbType.VarChar, 100).Value = objBO.TemplateName;
					cmd.Parameters.Add("@Description", SqlDbType.VarChar, 100).Value = objBO.Description;
					cmd.Parameters.Add("@ItemId", SqlDbType.VarChar, 10).Value = objBO.ItemId;
					cmd.Parameters.Add("@app_no", SqlDbType.VarChar, 10).Value = objBO.app_no;
					cmd.Parameters.Add("@DoctorId", SqlDbType.VarChar, 10).Value = objBO.DoctorId;
					cmd.Parameters.Add("@DeptId", SqlDbType.VarChar, 10).Value = objBO.DeptId;
					cmd.Parameters.Add("@DoctorId_Trf", SqlDbType.VarChar, 10).Value = objBO.DoctorId_Trf;
					cmd.Parameters.Add("@caseType", SqlDbType.VarChar, 50).Value = objBO.caseType;
					cmd.Parameters.Add("@consultType", SqlDbType.VarChar, 50).Value = objBO.consultType;
					cmd.Parameters.Add("@doctor_diagnosis", SqlDbType.VarChar, 100).Value = objBO.doctor_diagnosis;
					cmd.Parameters.Add("@doctor_remark", SqlDbType.VarChar, 100).Value = objBO.doctor_remark;
					cmd.Parameters.Add("@prm1", SqlDbType.VarChar, 100).Value = objBO.prm_1;
					cmd.Parameters.Add("@IsFav", SqlDbType.Bit, 1).Value = objBO.IsFav;
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
		public string CPOE_InsertVitalSign(VitalSignBO objBO)
		{
			string processInfo = string.Empty;
			using (SqlConnection con = new SqlConnection(GlobalConfig.ConStr_Hospital))
			{
				con.Open();
				using (SqlCommand cmd = new SqlCommand("pCPOE_InsertVitalSign", con))
				{
					cmd.CommandType = CommandType.StoredProcedure;
					cmd.CommandTimeout = 2500;
					cmd.Parameters.Add("@RefNo", SqlDbType.VarChar, 18).Value = objBO.RefNo;
					cmd.Parameters.Add("@UHID", SqlDbType.VarChar, 18).Value = objBO.UHID;
					cmd.Parameters.Add("@DoctorId", SqlDbType.VarChar, 10).Value = objBO.DoctorId;
					cmd.Parameters.Add("@EntrySource", SqlDbType.VarChar, 18).Value = objBO.EntrySource;
					cmd.Parameters.Add("@BP_Sys", SqlDbType.Decimal, 18).Value = objBO.BP_Sys;
					cmd.Parameters.Add("@BP_Dys", SqlDbType.Decimal, 18).Value = objBO.BP_Dys;
					cmd.Parameters.Add("@Pulse", SqlDbType.Decimal, 18).Value = objBO.Pulse;
					cmd.Parameters.Add("@Resp", SqlDbType.Decimal, 18).Value = objBO.Resp;
					cmd.Parameters.Add("@Temprarture", SqlDbType.Decimal, 18).Value = objBO.Temprarture;
					cmd.Parameters.Add("@HT", SqlDbType.Decimal, 18).Value = objBO.HT;
					cmd.Parameters.Add("@WT", SqlDbType.Decimal, 18).Value = objBO.WT;
					cmd.Parameters.Add("@ArmSpan", SqlDbType.Decimal, 18).Value = objBO.ArmSpan;
					cmd.Parameters.Add("@SittingHeight", SqlDbType.Decimal, 18).Value = objBO.SittingHeight;
					cmd.Parameters.Add("@IBW", SqlDbType.Decimal, 18).Value = objBO.IBW;
					cmd.Parameters.Add("@SPO2", SqlDbType.Decimal, 18).Value = objBO.SPO2;
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
		public string CPOE_InsertPrescribedItems(ipPrescription objBO, List<PrescribedItems> objItems, List<PrescribedMedicine> objMedicine)
		{
			string processInfo = string.Empty;
			DataTable dt = new DataTable();
			dt.Columns.Add("TemplateId", typeof(string));
			dt.Columns.Add("ItemId", typeof(string));
			dt.Columns.Add("ItemName", typeof(string));
			dt.Columns.Add("Remark", typeof(string));
			foreach (PrescribedItems obj in objItems)
			{
				DataRow dr = dt.NewRow();
				dr["TemplateId"] = obj.TemplateId;
				dr["ItemId"] = obj.ItemId;
				dr["ItemName"] = obj.ItemName;
				dr["Remark"] = obj.Remark;
				dt.Rows.Add(dr);
			}
			DataTable dt2 = new DataTable();
			dt2.Columns.Add("app_no", typeof(string));
			dt2.Columns.Add("DoctorId", typeof(string));
			dt2.Columns.Add("Item_id", typeof(string));
			dt2.Columns.Add("Item_name", typeof(string));
			dt2.Columns.Add("med_dose", typeof(string));
			dt2.Columns.Add("med_times", typeof(int));
			dt2.Columns.Add("med_duration", typeof(int));
			dt2.Columns.Add("med_intake", typeof(string));
			dt2.Columns.Add("med_route", typeof(string));
			dt2.Columns.Add("qty", typeof(decimal));
			dt2.Columns.Add("remark", typeof(string));
			foreach (PrescribedMedicine obj in objMedicine)
			{
				DataRow dr = dt2.NewRow();
				dr["app_no"] = obj.app_no;
				dr["DoctorId"] = obj.DoctorId;
				dr["Item_id"] = obj.Item_id; 
				dr["Item_name"] = obj.Item_name;
				dr["med_dose"] = obj.med_dose;
				dr["med_times"] = obj.med_times;
				dr["med_duration"] = obj.med_duration;
				dr["med_intake"] = obj.med_intake;
				dr["med_route"] = obj.med_route;
				dr["qty"] = obj.qty;
				dr["remark"] = obj.remark;
				dt2.Rows.Add(dr);
			}
			using (SqlConnection con = new SqlConnection(GlobalConfig.ConStr_Hospital))
			{
				con.Open();
				using (SqlCommand cmd = new SqlCommand("pCPOE_InsertPrescribedItems", con))
				{
					cmd.CommandType = CommandType.StoredProcedure;
					cmd.CommandTimeout = 2500;
					cmd.Parameters.Add("@DoctorId", SqlDbType.VarChar, 20).Value = objBO.DoctorId;
					cmd.Parameters.Add("@AppNo", SqlDbType.VarChar, 20).Value = objBO.app_no;
					cmd.Parameters.AddWithValue("UDT_PrescribedItems", dt);
					cmd.Parameters.AddWithValue("@UDT_PrescribedMedicine", dt2);                  
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
				}
			}
			return processInfo;
		}
		public string CPOE_InsertMedicineTemplateInfo(MedicineTemplateInfo objBO)
		{
			string processInfo = string.Empty;
			using (SqlConnection con = new SqlConnection(GlobalConfig.ConStr_Hospital))
			{
				con.Open();
				using (SqlCommand cmd = new SqlCommand("pCPOE_InsertMedicineTemplateInfo", con))
				{
					cmd.CommandType = CommandType.StoredProcedure;
					cmd.CommandTimeout = 2500;
					cmd.Parameters.Add("@med_TemplateId", SqlDbType.VarChar, 10).Value = objBO.med_TemplateId;
					cmd.Parameters.Add("@DoctorId", SqlDbType.VarChar, 10).Value = objBO.DoctorId;
					cmd.Parameters.Add("@Item_id", SqlDbType.VarChar, 10).Value = objBO.Item_id;
					cmd.Parameters.Add("@Item_name", SqlDbType.VarChar, 100).Value = objBO.Item_name;
					cmd.Parameters.Add("@med_dose", SqlDbType.VarChar, 200).Value = objBO.med_dose;
					cmd.Parameters.Add("@med_times", SqlDbType.Int, 10).Value = objBO.med_times;
					cmd.Parameters.Add("@med_duration", SqlDbType.Int, 10).Value = objBO.med_duration;
					cmd.Parameters.Add("@med_intake", SqlDbType.VarChar, 100).Value = objBO.med_intake;
					cmd.Parameters.Add("@med_route", SqlDbType.VarChar, 100).Value = objBO.med_route;
					cmd.Parameters.Add("@qty", SqlDbType.Decimal, 20).Value = objBO.qty;
					cmd.Parameters.Add("@remark", SqlDbType.VarChar, 200).Value = objBO.remark;
					cmd.Parameters.Add("@login_id", SqlDbType.VarChar, 10).Value = objBO.login_id;
					cmd.Parameters.Add("@IsFav", SqlDbType.Bit, 1).Value = objBO.IsFav;					
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