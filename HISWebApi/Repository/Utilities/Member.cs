using HISWebApi.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace HISWebApi.Repository.Utilities
{
	public class Member
	{
		public string Online_SentOtp(HospitalBO objBO)
		{
			string result = string.Empty;
			try
			{
				string mobile_no = objBO.Mobile;
				string OTP = Utilities.OTPGenerator.GenerateRandomOTP(4);
				string sms = OTP + " is your chandan member verification code";
				Utilities.SmsClass smsService = new Utilities.SmsClass();
				string responsemessage = smsService.SendSms(mobile_no, sms);
				result = OTP;
				try
				{
					string qry = "insert into online_doctorAppSmsLog(appointmentId, mobile_no, sms, login_id, response_message) ";
					qry += "values('-', '" + mobile_no + "', '" + sms + "', '-','" + responsemessage + "')";
					DBManager.QueryExecute(qry, GlobalConfig.ConStr_Hospital);
				}
				catch (Exception ex) { }
			}
			catch (Exception ex) { result = ex.Message; }
			return result;
		}
		public dataSet GetCardUserProfileQueries(HealthCardInfo ipapp)
		{
			dataSet dsObj = new dataSet();
			using (SqlConnection con = new SqlConnection(GlobalConfig.ConStr_eManagement))
			{
				using (SqlCommand cmd = new SqlCommand("pGetCardUserProfileQueries", con))
				{
					cmd.CommandType = CommandType.StoredProcedure;
					cmd.CommandTimeout = 2500;
					cmd.Parameters.Add("@labcode", SqlDbType.VarChar, 10).Value = ipapp.labcode;
					cmd.Parameters.Add("@card_no", SqlDbType.VarChar, 20).Value = ipapp.card_no;
					cmd.Parameters.Add("@member_id", SqlDbType.VarChar, 25).Value = ipapp.member_id;
					cmd.Parameters.Add("@mobileno", SqlDbType.VarChar, 10).Value = ipapp.mobileno;
					cmd.Parameters.Add("@UHID", SqlDbType.VarChar, 20).Value = ipapp.UHID;
					cmd.Parameters.Add("@district", SqlDbType.VarChar, 50).Value = ipapp.district;
					cmd.Parameters.Add("@state", SqlDbType.VarChar, 50).Value = ipapp.state;
					cmd.Parameters.Add("@prm_1", SqlDbType.VarChar, 50).Value = ipapp.prm_1;
					cmd.Parameters.Add("@logic", SqlDbType.VarChar, 50).Value = ipapp.logic;
					cmd.Parameters.Add("@login_id", SqlDbType.VarChar, 10).Value = ipapp.login_id;
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
        public dataSet CouponQueries(CouponQueries ipapp)
        {
            dataSet dsObj = new dataSet();
            using (SqlConnection con = new SqlConnection(GlobalConfig.ConStr_MobileAppDb))
            {
                using (SqlCommand cmd = new SqlCommand("pMobile_CouponQueries", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandTimeout = 2500;
                    cmd.Parameters.Add("@unit_id", SqlDbType.VarChar, 10).Value = ipapp.unit_id;
                    cmd.Parameters.Add("@TokenNo", SqlDbType.VarChar, 10).Value = ipapp.TokenNo;
                    cmd.Parameters.Add("@BookingId", SqlDbType.VarChar, 20).Value = ipapp.BookingId;                 
                    cmd.Parameters.Add("@prm_1", SqlDbType.VarChar, 50).Value = ipapp.prm_1;
                    cmd.Parameters.Add("@login_id", SqlDbType.VarChar, 50).Value = ipapp.login_id;
                    cmd.Parameters.Add("@Logic", SqlDbType.VarChar, 50).Value = ipapp.Logic;                  
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
        public string Insert_CardUserProfile(HealthCardInfo ipapp)
		{
			string processInfo = string.Empty;
			using (SqlConnection con = new SqlConnection(GlobalConfig.ConStr_eManagement))
			{
				using (SqlCommand cmd = new SqlCommand("pInsert_CardUserProfile", con))
				{
					cmd.CommandType = CommandType.StoredProcedure;
					cmd.CommandTimeout = 2500;
					cmd.Parameters.Add("@labcode", SqlDbType.VarChar, 10).Value = ipapp.labcode;
					cmd.Parameters.Add("@card_no", SqlDbType.VarChar, 20).Value = ipapp.card_no;
					cmd.Parameters.Add("@member_id", SqlDbType.VarChar, 25).Value = ipapp.member_id;
					cmd.Parameters.Add("@m_type", SqlDbType.VarChar, 10).Value = ipapp.m_type;
					cmd.Parameters.Add("@cust_name", SqlDbType.VarChar, 50).Value = ipapp.cust_name;
					cmd.Parameters.Add("@gender", SqlDbType.VarChar, 10).Value = ipapp.gender;
					cmd.Parameters.Add("@dob", SqlDbType.VarChar, 10).Value = ipapp.dob;
					cmd.Parameters.Add("@mobileno", SqlDbType.VarChar, 10).Value = ipapp.mobileno;
					cmd.Parameters.Add("@UHID", SqlDbType.VarChar, 20).Value = ipapp.UHID;
					cmd.Parameters.Add("@area", SqlDbType.VarChar, 100).Value = ipapp.area;
					cmd.Parameters.Add("@Locality", SqlDbType.VarChar, 50).Value = ipapp.Locality;
					cmd.Parameters.Add("@district", SqlDbType.VarChar, 50).Value = ipapp.district;
					cmd.Parameters.Add("@state", SqlDbType.VarChar, 50).Value = ipapp.state;
					cmd.Parameters.Add("@email", SqlDbType.VarChar, 50).Value = ipapp.email;
					cmd.Parameters.Add("@pin", SqlDbType.VarChar, 20).Value = ipapp.pin;
					cmd.Parameters.Add("@logic", SqlDbType.VarChar, 50).Value = ipapp.logic;
					cmd.Parameters.Add("@login_id", SqlDbType.VarChar, 10).Value = ipapp.login_id;
					cmd.Parameters.Add("@result", SqlDbType.VarChar, 500).Value = "";
					cmd.Parameters["@result"].Direction = ParameterDirection.InputOutput;
					try
					{
						con.Open();
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
		public string InsertMobileAppCoupons(CouponLogBO objBO)
		{
			string processInfo = string.Empty;
			using (SqlConnection con = new SqlConnection(GlobalConfig.ConStr_MobileAppDb))
			{
				con.Open();
				using (SqlCommand cmd = new SqlCommand("pInsertMobileAppCoupons", con))
				{
					cmd.CommandType = CommandType.StoredProcedure;
					cmd.CommandTimeout = 2500;
					cmd.Parameters.Add("@labcode", SqlDbType.VarChar, 12).Value = objBO.labcode;
					cmd.Parameters.Add("@Mobile", SqlDbType.VarChar, 12).Value = objBO.Mobile;
					cmd.Parameters.Add("@PatientName", SqlDbType.VarChar, 100).Value = objBO.PatientName;
					cmd.Parameters.Add("@CouponCode", SqlDbType.VarChar, 50).Value = objBO.CouponCode;
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
        public string SendCoupon(CouponQueries obj)
        {
            string processInfo = "";
           if (obj.CouponType == "SendOTP")
            {
                //Implementation Pending
                try
                {
                    SmsClass sm = new SmsClass();
                    string result = string.Empty;
                    string Couponcode = Mobile_GetCoupons(out result, obj.LabCode, obj.mobileNo, obj.login_id);
                    if (result == "Success")
                    {
                        string sms = Couponcode + " is your Chandan Member Verification Code.";
                        sm.SendSms(obj.mobileNo, sms);
                        processInfo = "Coupon is Sent on the " + obj.mobileNo + " number |XXXX";
                    }
                    else
                    {
                        processInfo = result + "|XXXX";
                    }
                }
                catch (Exception ex) { processInfo = ex.Message + "|XXXXX"; }
            }
            return processInfo;
        }
        private string Mobile_GetCoupons(out string processInfo, string LabCode, string mobileNo, string login_id)
        {
            string couponCode = "";
            SqlConnection con = new SqlConnection(GlobalConfig.ConStr_MobileAppDb);
            SqlCommand cmd = new SqlCommand("pMobile_GetCoupons", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandTimeout = 2500;
            cmd.Parameters.Add("@mobileNo", SqlDbType.VarChar, 20).Value = mobileNo;
            cmd.Parameters.Add("@login_id", SqlDbType.VarChar, 50).Value = login_id;
            cmd.Parameters.Add("@ByApp", SqlDbType.VarChar, 50).Value = "WithoutApp";
            cmd.Parameters.Add("@unit_id", SqlDbType.VarChar, 50).Value = LabCode;
            cmd.Parameters.Add("@couponCode", SqlDbType.VarChar, 10).Value = "";
            cmd.Parameters["@couponCode"].Direction = ParameterDirection.InputOutput;
            cmd.Parameters.Add("@result", SqlDbType.VarChar, 100).Value = "";
            cmd.Parameters["@result"].Direction = ParameterDirection.InputOutput;
            try
            {
                con.Open();
                cmd.ExecuteNonQuery();
                couponCode = (string)cmd.Parameters["@couponCode"].Value.ToString();
                processInfo = (string)cmd.Parameters["@result"].Value.ToString();
            }
            catch (SqlException sqlEx)
            {
                processInfo = sqlEx.Message;
            }
            finally { con.Close(); }
            return couponCode;
        }
    }
}