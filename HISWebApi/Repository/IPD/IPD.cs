using HISWebApi.Models;
using HISWebApi.Repository.Patient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using static HISWebApi.Models.IPDBO;

namespace HISWebApi.Repository.IPD
{
	public class IPD
	{
        
        public dataSet IPD_RegistrationQueries(IPDInfo objBO)
        {
            dataSet dsObj = new dataSet();
            using (SqlConnection con = new SqlConnection(GlobalConfig.ConStr_Hospital))
            {
                using (SqlCommand cmd = new SqlCommand("pIPD_RegistrationQueries", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandTimeout = 2500;
                    cmd.Parameters.Add("@hosp_id", SqlDbType.VarChar, 10).Value = objBO.hosp_id;
                    cmd.Parameters.Add("@UHID", SqlDbType.VarChar, 20).Value = objBO.UHID;
                    cmd.Parameters.Add("@IPDNo", SqlDbType.VarChar, 11).Value = objBO.IPDNo;
                    cmd.Parameters.Add("@DoctorId", SqlDbType.VarChar, 10).Value = objBO.DoctorId;
                    cmd.Parameters.Add("@Prm1", SqlDbType.VarChar, 500).Value = objBO.Prm1;
                    cmd.Parameters.Add("@Prm2", SqlDbType.VarChar, 500).Value = objBO.Prm2;
                    cmd.Parameters.Add("@login_id", SqlDbType.VarChar, 20).Value = objBO.login_id;
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
        public dataSet IPD_FeedbackQuesries(FeedbackInfo objBO)
		{
			dataSet dsObj = new dataSet();
			using (SqlConnection con = new SqlConnection(GlobalConfig.ConStr_Hospital))
			{
				using (SqlCommand cmd = new SqlCommand("pIPD_FeedbackQuesries", con))
				{
					cmd.CommandType = CommandType.StoredProcedure;
					cmd.CommandTimeout = 2500;
					cmd.Parameters.Add("@Prm1", SqlDbType.VarChar, 100).Value = objBO.Prm1;
					cmd.Parameters.Add("@Prm2", SqlDbType.VarChar, 100).Value = objBO.Prm2;
                    cmd.Parameters.Add("@Prm3", SqlDbType.VarChar,500).Value = objBO.Prm3;
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
		public dataSet IPD_ClinicalSafetyRoundQuesries(FeedbackInfo objBO)
		{
			dataSet dsObj = new dataSet();
			using (SqlConnection con = new SqlConnection(GlobalConfig.ConStr_Hospital))
			{
				using (SqlCommand cmd = new SqlCommand("pIPD_ClinicalSafetyRoundQuesries", con))
				{
					cmd.CommandType = CommandType.StoredProcedure;
					cmd.CommandTimeout = 2500;
					cmd.Parameters.Add("@Prm1", SqlDbType.VarChar, 100).Value = objBO.Prm1;
					cmd.Parameters.Add("@Prm2", SqlDbType.VarChar, 100).Value = objBO.Prm2;
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
		public string IPD_InsertFeedback(FeedbackInfo objBO)
		{
			dataSet dsObj = new dataSet();
			string processInfo = string.Empty;
			using (SqlConnection con = new SqlConnection(GlobalConfig.ConStr_Hospital))
			{
				using (SqlCommand cmd = new SqlCommand("pIPD_InsertFeedback", con))
				{
					cmd.CommandType = CommandType.StoredProcedure;
					cmd.CommandTimeout = 2500;					
					cmd.Parameters.Add("@IPDNo", SqlDbType.VarChar,30).Value = objBO.IPDNo;
					cmd.Parameters.Add("@PatientName", SqlDbType.VarChar,100).Value = objBO.PatientName;
					cmd.Parameters.Add("@Age", SqlDbType.VarChar,5).Value = objBO.Age;
					cmd.Parameters.Add("@AgeType", SqlDbType.VarChar, 20).Value = objBO.AgeType;
					cmd.Parameters.Add("@Gender", SqlDbType.VarChar, 10).Value = objBO.Gender;
					cmd.Parameters.Add("@AdmittedDoctorId", SqlDbType.VarChar, 15).Value = objBO.AdmittedDoctorId;
					cmd.Parameters.Add("@AdmittDoctorName", SqlDbType.VarChar, 100).Value = objBO.AdmittDoctorName;
                    cmd.Parameters.Add("@FloorName", SqlDbType.VarChar, 200).Value = objBO.FloorName;
                    cmd.Parameters.Add("@AdmitDate", SqlDbType.DateTime).Value = objBO.AdmitDate;
					cmd.Parameters.Add("@OverallFeedback", SqlDbType.VarChar,500).Value = objBO.OverallFeedback;
					cmd.Parameters.Add("@OverallRating", SqlDbType.Decimal,10).Value = objBO.OverallRating;			
					cmd.Parameters.Add("@FeedbackType", SqlDbType.VarChar, 50).Value = objBO.FeedbackType;
					cmd.Parameters.Add("@QuestId", SqlDbType.Int).Value = objBO.QuestId;
					cmd.Parameters.Add("@Feedback", SqlDbType.VarChar, 500).Value = objBO.Feedback;
					cmd.Parameters.Add("@Rating", SqlDbType.Decimal, 10).Value = objBO.Rating;
					cmd.Parameters.Add("@Prm1", SqlDbType.VarChar, 100).Value = objBO.Prm1;
					cmd.Parameters.Add("@Prm2", SqlDbType.VarChar, 100).Value = objBO.Prm2;
                    cmd.Parameters.Add("@NurseId", SqlDbType.VarChar, 20).Value = objBO.NurseId;
                    cmd.Parameters.Add("@login_id", SqlDbType.VarChar, 10).Value = objBO.login_id;
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
		public string IPD_InsertClinicalSafetyRound(SafetyRoundInfo objBO)
		{
			dataSet dsObj = new dataSet();
			string processInfo = string.Empty;
			using (SqlConnection con = new SqlConnection(GlobalConfig.ConStr_Hospital))
			{
				using (SqlCommand cmd = new SqlCommand("pIPD_InsertClinicalSafetyRound", con))
				{
					cmd.CommandType = CommandType.StoredProcedure;
					cmd.CommandTimeout = 2500;
                    cmd.Parameters.Add("@IPDNo", SqlDbType.VarChar, 30).Value = objBO.IPDNo;
                    cmd.Parameters.Add("@PatientName", SqlDbType.VarChar, 100).Value = objBO.PatientName;
                    cmd.Parameters.Add("@Age", SqlDbType.VarChar, 5).Value = objBO.Age;
                    cmd.Parameters.Add("@AgeType", SqlDbType.VarChar, 20).Value = objBO.AgeType;
                    cmd.Parameters.Add("@Gender", SqlDbType.VarChar, 10).Value = objBO.Gender;
                    cmd.Parameters.Add("@DoctorId", SqlDbType.VarChar, 15).Value = objBO.AdmittedDoctorId;
                    cmd.Parameters.Add("@DoctorName", SqlDbType.VarChar, 100).Value = objBO.AdmittDoctorName;
                    cmd.Parameters.Add("@FloorName", SqlDbType.VarChar, 100).Value = objBO.FloorName;
                    cmd.Parameters.Add("@BedNo", SqlDbType.VarChar, 20).Value = objBO.BedNo;
                    cmd.Parameters.Add("@CheckupType", SqlDbType.VarChar, 20).Value = objBO.CheckupType;				
					cmd.Parameters.Add("@QuestId", SqlDbType.Int).Value = objBO.QuestId;
					cmd.Parameters.Add("@CheckupStatus", SqlDbType.VarChar, 50).Value = objBO.CheckupStatus;		
					cmd.Parameters.Add("@Remark", SqlDbType.VarChar, 500).Value = objBO.Remark;
					cmd.Parameters.Add("@Prm1", SqlDbType.VarChar, 100).Value = objBO.Prm1;
					cmd.Parameters.Add("@Prm2", SqlDbType.VarChar, 100).Value = objBO.Prm2;
					cmd.Parameters.Add("@login_id", SqlDbType.VarChar, 10).Value = objBO.login_id;
					cmd.Parameters.Add("@attendantName", SqlDbType.VarChar, 50).Value = objBO.attendantName;
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
        public string IPD_RegistrationInsertUpdate(PatientMasterBO objPatient, IPDRegistrationBO objBooking)
        {
            PatientMaster pm = new PatientMaster();
            string processInfo = string.Empty;
            string processInfo1 = string.Empty;
            string UHID = pm.InsertPatientMaster(out processInfo1, objPatient);
            objBooking.UHID = UHID;
            if (processInfo1.Contains("Success"))
            {
                using (SqlConnection con = new SqlConnection(GlobalConfig.ConStr_Hospital))
                {
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand("pIPD_RegistrationInsertUpdate", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandTimeout = 2500;
                        cmd.Parameters.Add("@hosp_id", SqlDbType.VarChar, 10).Value = objBooking.hosp_id;
                        cmd.Parameters.Add("@UHID", SqlDbType.VarChar, 20).Value = objBooking.UHID;
                        cmd.Parameters.Add("@IsNewPatient", SqlDbType.VarChar, 20).Value = objBooking.IsNewPatient;
                        cmd.Parameters.Add("@Title", SqlDbType.VarChar, 20).Value = objBooking.Title;
                        cmd.Parameters.Add("@patient_name", SqlDbType.VarChar, 100).Value = objBooking.patient_name;
                        cmd.Parameters.Add("@email", SqlDbType.VarChar, 50).Value = objBooking.email;
                        cmd.Parameters.Add("@age", SqlDbType.VarChar, 20).Value = objBooking.age;
                        cmd.Parameters.Add("@ageType", SqlDbType.VarChar, 20).Value = objBooking.ageType;
                        cmd.Parameters.Add("@gender", SqlDbType.VarChar, 20).Value = objBooking.gender;
                        cmd.Parameters.Add("@dob", SqlDbType.Date, 10).Value = string.IsNullOrEmpty(objBooking.dob) ? null : objBooking.dob;
                        cmd.Parameters.Add("@ageInDays", SqlDbType.Int, 10).Value = objBooking.ageInDays;
                        cmd.Parameters.Add("@mobile_no", SqlDbType.VarChar, 10).Value = objBooking.mobile_no;
                        cmd.Parameters.Add("@IDProofType", SqlDbType.VarChar, 50).Value = objBooking.IDProofType;
                        cmd.Parameters.Add("@IDProofNO", SqlDbType.VarChar, 50).Value = objBooking.IDProofNO;
                        cmd.Parameters.Add("@IPDNo", SqlDbType.VarChar, 11).Value = objBooking.IPDNo;
                        cmd.Parameters.Add("@AdmitDate", SqlDbType.DateTime).Value = objBooking.AdmitDate;
                        cmd.Parameters.Add("@AdmitTime", SqlDbType.VarChar, 10).Value = objBooking.AdmitTime;
                        cmd.Parameters.Add("@AdmissionType", SqlDbType.VarChar, 100).Value = objBooking.AdmissionType;
                        cmd.Parameters.Add("@DoctorId", SqlDbType.VarChar, 10).Value = objBooking.DoctorId;
                        cmd.Parameters.Add("@PanelId", SqlDbType.VarChar, 10).Value = objBooking.PanelId;
                        cmd.Parameters.Add("@PatientType", SqlDbType.VarChar, 100).Value = objBooking.PatientType;
                        cmd.Parameters.Add("@PolicyNo", SqlDbType.VarChar, 20).Value = objBooking.PolicyNo;
                        cmd.Parameters.Add("@ReferencNo", SqlDbType.VarChar, 20).Value = objBooking.ReferencNo;
                        cmd.Parameters.Add("@SourceName", SqlDbType.VarChar, 50).Value = objBooking.SourceName;
                        cmd.Parameters.Add("@StaffCode", SqlDbType.VarChar, 20).Value = objBooking.StaffCode;
                        cmd.Parameters.Add("@RoomBedId", SqlDbType.VarChar, 10).Value = objBooking.RoomBedId;
                        cmd.Parameters.Add("@BedNo", SqlDbType.VarChar, 10).Value = objBooking.BedNo;
                        cmd.Parameters.Add("@RoomBillingCategory", SqlDbType.VarChar, 50).Value = objBooking.RoomBillingCategory;
                        cmd.Parameters.Add("@RoomTypeRequest", SqlDbType.VarChar, 50).Value = objBooking.RoomTypeRequest;
                        cmd.Parameters.Add("@MLCType", SqlDbType.VarChar, 50).Value = objBooking.MLCType;
                        cmd.Parameters.Add("@MLCNo", SqlDbType.VarChar, 50).Value = objBooking.MLCNo;
                        cmd.Parameters.Add("@Remark", SqlDbType.VarChar, 500).Value = objBooking.Remark;
                        cmd.Parameters.Add("@RefBy", SqlDbType.VarChar, 10).Value = objBooking.RefBy;
                        cmd.Parameters.Add("@RefName", SqlDbType.VarChar, 50).Value = objBooking.RefName;
                        cmd.Parameters.Add("@AttendantName", SqlDbType.VarChar, 100).Value = objBooking.AttendantName;
                        cmd.Parameters.Add("@AttendantContactNo1", SqlDbType.VarChar, 20).Value = objBooking.AttendantContactNo1;
                        cmd.Parameters.Add("@AttendantContactNo2", SqlDbType.VarChar, 20).Value = objBooking.AttendantContactNo2;
                        cmd.Parameters.Add("@login_id", SqlDbType.VarChar, 20).Value = objBooking.login_id;
                        cmd.Parameters.Add("@Logic", SqlDbType.VarChar, 50).Value = objBooking.Logic;
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
        public string IPD_TakeAdvance(IPDRegistrationBO objBooking, List<PaymentBO> objPayment)
        {
            string processInfo = string.Empty;
            if (objPayment.Count > 0)
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
                    using (SqlCommand cmd = new SqlCommand("pIPD_TakeAdvance", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandTimeout = 2500;
                        cmd.Parameters.Add("@IPOPNo", SqlDbType.VarChar, 12).Value = objBooking.IPDNo;
                        cmd.Parameters.Add("@hosp_id", SqlDbType.VarChar, 10).Value = objBooking.hosp_id;
                        cmd.Parameters.AddWithValue("udt_Receipt", dt);
                        cmd.Parameters.Add("@IPOPType", SqlDbType.VarChar, 10).Value = objBooking.IPOPType;
                        cmd.Parameters.Add("@ReceiptType", SqlDbType.VarChar, 50).Value = objBooking.ReceiptType;
                        cmd.Parameters.Add("@Prm1", SqlDbType.VarChar, 200).Value = objBooking.Prm1;
                        cmd.Parameters.Add("@Prm2", SqlDbType.VarChar, 200).Value = objBooking.Prm2;
                        cmd.Parameters.Add("@remark", SqlDbType.VarChar, 200).Value = objBooking.Remark;
                        cmd.Parameters.Add("@login_id", SqlDbType.VarChar, 20).Value = objBooking.login_id;
                        cmd.Parameters.Add("@Logic", SqlDbType.VarChar, 50).Value = objBooking.Logic;
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
            }
            return processInfo;
        }
    }
}