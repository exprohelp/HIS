using HISWebApi.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace HISWebApi.Repository.PatientDesk
{
	public class Patient
	{
		public dataSet AmbulanceAndEmergencyQueries(ipPatientDesk ip)
		{
			dataSet dsObj = new dataSet();
			using (SqlConnection con = new SqlConnection(GlobalConfig.ConStr_Hospital))
			{					
				using (SqlCommand cmd = new SqlCommand("pAmbulanceAndEmergencyQueries", con))
				{					
					cmd.CommandType = CommandType.StoredProcedure;
					cmd.CommandTimeout = 2500;
					cmd.Parameters.Add("@RequestId", SqlDbType.VarChar, 10).Value = ip.RequestId;
					cmd.Parameters.Add("@DriverId", SqlDbType.VarChar, 10).Value = ip.DriverId;
					cmd.Parameters.Add("@AmbulanceId", SqlDbType.VarChar, 10).Value = ip.AmbulanceId;
					cmd.Parameters.Add("@from", SqlDbType.VarChar, 10).Value = ip.from;
					cmd.Parameters.Add("@to", SqlDbType.VarChar, 10).Value = ip.to;
					cmd.Parameters.Add("@Prm1", SqlDbType.VarChar,50).Value = ip.Prm1;
					cmd.Parameters.Add("@Prm2", SqlDbType.VarChar, 50).Value = ip.Prm2;				
					cmd.Parameters.Add("@Logic", SqlDbType.VarChar, 50).Value = ip.Logic;
					try{
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
		public string AmbulanceAndEmergencyRequest(EmergencyDesk objBO)
		{
			dataSet dsObj = new dataSet();
			string processInfo = string.Empty;
			using (SqlConnection con = new SqlConnection(GlobalConfig.ConStr_Hospital))
			{
				using (SqlCommand cmd = new SqlCommand("pAmbulanceAndEmergencyRequest", con))
				{
					cmd.CommandType = CommandType.StoredProcedure;
					cmd.CommandTimeout = 2500;

                    cmd.Parameters.Add("@RequestId", SqlDbType.VarChar, 10).Value = objBO.RequestId;
                    cmd.Parameters.Add("@UHID", SqlDbType.VarChar, 20).Value = objBO.UHID;
                    cmd.Parameters.Add("@IPDNo", SqlDbType.VarChar, 20).Value = objBO.IPDNo;
                    cmd.Parameters.Add("@PatientName", SqlDbType.VarChar, 50).Value = objBO.PatientName;
                    cmd.Parameters.Add("@RequestBy", SqlDbType.VarChar, 30).Value = objBO.RequestBy;
                    cmd.Parameters.Add("@RequestType", SqlDbType.VarChar, 50).Value = objBO.RequestType;
                    cmd.Parameters.Add("@RequestNote", SqlDbType.VarChar, 500).Value = objBO.RequestNote;
                    cmd.Parameters.Add("@District", SqlDbType.VarChar, 50).Value = objBO.District;
                    cmd.Parameters.Add("@LandMark", SqlDbType.VarChar, 100).Value = objBO.LandMark;
                    cmd.Parameters.Add("@PatientAddress", SqlDbType.VarChar, 300).Value = objBO.PatientAddress;
                    cmd.Parameters.Add("@PatientGeoLat", SqlDbType.VarChar, 50).Value = objBO.PatientGeoLat;
                    cmd.Parameters.Add("@PatientGeoLong", SqlDbType.VarChar, 50).Value = objBO.PatientGeoLong;
                    cmd.Parameters.Add("@MobileNo", SqlDbType.VarChar, 20).Value = objBO.MobileNo;
                    cmd.Parameters.Add("@RequestTakenBy", SqlDbType.VarChar, 20).Value = objBO.RequestTakenBy;
                    cmd.Parameters.Add("@PickupDate", SqlDbType.DateTime).Value = string.IsNullOrEmpty(objBO.PickupDate) ? null : objBO.PickupDate;
                    cmd.Parameters.Add("@PickupTime", SqlDbType.DateTime).Value = string.IsNullOrEmpty(objBO.PickupTime) ? null : objBO.PickupTime; ;
                    cmd.Parameters.Add("@AmbulanceTrackingLink", SqlDbType.VarChar, 200).Value = objBO.AmbulanceTrackingLink;
                    cmd.Parameters.Add("@AmbulanceId", SqlDbType.VarChar, 20).Value = objBO.AmbulanceId;
                    cmd.Parameters.Add("@AllotedDriverId", SqlDbType.VarChar, 20).Value = objBO.AllotedDriverId;
                    cmd.Parameters.Add("@Prm1", SqlDbType.VarChar).Value = objBO.Prm1;
                    cmd.Parameters.Add("@Prm2", SqlDbType.VarChar).Value = objBO.Prm2;
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
		public string InsertSmsLog(PatientSmsInfo objBO)
		{
			string processInfo = string.Empty;
			using (SqlConnection con = new SqlConnection(GlobalConfig.ConStr_Hospital))
			{
				using (SqlCommand cmd = new SqlCommand("InsertSmsLog", con))
				{
					cmd.CommandType = CommandType.StoredProcedure;
					cmd.CommandTimeout = 2500;
					cmd.Parameters.Add("@AppointmentId", SqlDbType.VarChar, 5).Value = objBO.AppointmentId;
					cmd.Parameters.Add("@MobileNo", SqlDbType.VarChar, 15).Value = objBO.MobileNo;
					cmd.Parameters.Add("@Sms", SqlDbType.VarChar, 50).Value = objBO.Sms;
					cmd.Parameters.Add("@SmsResponse", SqlDbType.VarChar, 80).Value = objBO.SmsResponse;
					cmd.Parameters.Add("@EntrySource", SqlDbType.VarChar, 50).Value = objBO.EntrySource;
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
	}
}