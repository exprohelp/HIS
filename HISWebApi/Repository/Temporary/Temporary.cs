using HISWebApi.Models;
using HISWebApi.Repository.IPD;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace HISWebApi.Repository.Temporary
{
	public class Temporary
	{
        HISDBLayer objhisLayer = new HISDBLayer();

        public dataSet Coupon_ReferralBookletQueries(TempReferralBO objBO)
		{
			dataSet dsObj = new dataSet();
			using (SqlConnection con = new SqlConnection(GlobalConfig.ConStr_Hospital))
			{
				using (SqlCommand cmd = new SqlCommand("pCoupon_ReferralBookletQueries", con))
				{
					cmd.CommandType = CommandType.StoredProcedure;
					cmd.CommandTimeout = 2500;
					cmd.Parameters.Add("@ref_code", SqlDbType.VarChar, 10).Value = objBO.ref_code;
					cmd.Parameters.Add("@BookletNo", SqlDbType.VarChar, 7).Value = objBO.BookletNo;
					cmd.Parameters.Add("@CouponNo", SqlDbType.VarChar, 50).Value = objBO.CouponNo;				
					cmd.Parameters.Add("@from", SqlDbType.DateTime, 15).Value = objBO.from;
					cmd.Parameters.Add("@to", SqlDbType.DateTime, 15).Value = objBO.to;				
					cmd.Parameters.Add("@prm1", SqlDbType.VarChar, 50).Value = objBO.prm1;	
					cmd.Parameters.Add("@Logic", SqlDbType.VarChar, 100).Value = objBO.Logic;				
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
		public string Coupon_ReferralBookletInsertUpdate(TempReferralBO objBO)
		{
			string processInfo = string.Empty;
			using (SqlConnection con = new SqlConnection(GlobalConfig.ConStr_Hospital))
			{
				con.Open();
				using (SqlCommand cmd = new SqlCommand("pCoupon_ReferralBookletInsertUpdate", con))
				{
					cmd.CommandType = CommandType.StoredProcedure;
					cmd.CommandTimeout = 2500;
					cmd.Parameters.Add("@ref_code", SqlDbType.VarChar, 10).Value = objBO.ref_code;
					cmd.Parameters.Add("@BookletNo", SqlDbType.VarChar, 50).Value = objBO.BookletNo;
					cmd.Parameters.Add("@CouponNo", SqlDbType.VarChar, 10).Value = objBO.CouponNo;
					cmd.Parameters.Add("@UHID", SqlDbType.VarChar, 20).Value = objBO.UHID;
					cmd.Parameters.Add("@login_id", SqlDbType.VarChar, 10).Value = objBO.login_id;
					cmd.Parameters.Add("@prm1", SqlDbType.VarChar, 50).Value = objBO.prm1;					
					cmd.Parameters.Add("@Logic", SqlDbType.VarChar,100).Value = objBO.Logic;
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


        public dataSet GetPatientDetailByUHID(TempReferralBO objBO)
        {
            dataSet dsObj = new dataSet();
            dsObj = objhisLayer.GetPatientDetailByUHID(objBO.UHID);
            return dsObj;
        }

        public dataSet CovidCertificateQueries(TempReferralBO objBO)
        {
            dataSet dsObj = new dataSet();
            using (SqlConnection con = new SqlConnection(GlobalConfig.ConStr_Hospital))
            {
                //pOnline_CovidQueries
                using (SqlCommand cmd = new SqlCommand("pCovidCertificateQueries", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandTimeout = 2500;
                    cmd.Parameters.Add("@PatientId", SqlDbType.VarChar, 10).Value = objBO.PatientId;
                    cmd.Parameters.Add("@DoctorId", SqlDbType.VarChar, 7).Value = objBO.DoctorId;
                    cmd.Parameters.Add("@from", SqlDbType.VarChar, 30).Value = objBO.fromdate;
                    cmd.Parameters.Add("@to", SqlDbType.DateTime, 30).Value = objBO.todate;
                    cmd.Parameters.Add("@prm_1", SqlDbType.VarChar, 50).Value = objBO.prm1;
                    cmd.Parameters.Add("@prm_2", SqlDbType.VarChar, 50).Value = objBO.prm2;
                    cmd.Parameters.Add("@login_id", SqlDbType.VarChar, 50).Value = objBO.login_id;
                    cmd.Parameters.Add("@Logic", SqlDbType.VarChar, 100).Value = objBO.Logic;
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

        public string CovidCertificateInsertUpdate(TempReferralBO objBO)
        {
            string processInfo = string.Empty;
            using (SqlConnection con = new SqlConnection(GlobalConfig.ConStr_Hospital))
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand("pCovidCertificateInsertUpdate", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandTimeout = 2500;
                    cmd.Parameters.Add("@autoid", SqlDbType.VarChar, 15).Value = objBO.autoid;
                    cmd.Parameters.Add("@hospid", SqlDbType.VarChar, 15).Value = objBO.hospid;
                    cmd.Parameters.Add("@UHID", SqlDbType.VarChar, 20).Value = objBO.UHID;
                    cmd.Parameters.Add("@mobileno", SqlDbType.VarChar, 10).Value = objBO.Mobile;
                    cmd.Parameters.Add("@patientname", SqlDbType.VarChar, 100).Value = objBO.PatientName;
                    cmd.Parameters.Add("@phycertpath", SqlDbType.VarChar, 300).Value = objBO.PhyPath;
                    cmd.Parameters.Add("@vircertpath", SqlDbType.VarChar, 300).Value = objBO.VirPath;
                    cmd.Parameters.Add("@login_id", SqlDbType.VarChar, 15).Value = objBO.login_id;
                    cmd.Parameters.Add("@Logic", SqlDbType.VarChar, 100).Value = objBO.Logic;
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


    }
}