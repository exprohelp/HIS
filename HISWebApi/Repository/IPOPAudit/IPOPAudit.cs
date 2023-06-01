using HISWebApi.Models;
using HISWebApi.Repository.IPD;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using static HISWebApi.Models.IPDBO;

namespace HISWebApi.Repository.IPOPAudit
{
	public class IPOPAudit
	{
		HISDBLayer his_layer = new HISDBLayer();
		public dataSet DiagnosticTestAnalysis(ipIPDAudit objBO)
		{
			string _result = string.Empty;
			return his_layer.GetDiagnosticTestAnalysis(objBO);
		}
        public dataSet MIS_Report(ipMISReport objBO)
        {
            dataSet dsObj = new dataSet();
            using (SqlConnection con = new SqlConnection(GlobalConfig.ConStr_ItdoseDataByChandan))
            {
                using (SqlCommand cmd = new SqlCommand("pMIS_Report", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandTimeout = 2500;
                    cmd.Parameters.Add("@DoctorId", SqlDbType.VarChar, 20).Value = objBO.DoctorId;
                    cmd.Parameters.Add("@tnxType", SqlDbType.VarChar, 50).Value = objBO.tnxType;
                    cmd.Parameters.Add("@from", SqlDbType.DateTime).Value = objBO.from;
                    cmd.Parameters.Add("@to", SqlDbType.DateTime).Value = objBO.to;
                    cmd.Parameters.Add("@CatId", SqlDbType.VarChar, 200).Value = objBO.CatId;
                    cmd.Parameters.Add("@SubCatId", SqlDbType.VarChar, 200).Value = objBO.SubCatId;
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
        public dataSet IPD_MedicineAudit(ipIPDAudit objBO)
		{
			dataSet dsObj = new dataSet();
			using (SqlConnection con = new SqlConnection(GlobalConfig.ConStr_ChandanPharmacyLive))
			{
				using (SqlCommand cmd = new SqlCommand("pIPD_MedicineAudit", con))
				{
					cmd.CommandType = CommandType.StoredProcedure;
					cmd.CommandTimeout = 2500;
					cmd.Parameters.Add("@IPDNo", SqlDbType.VarChar, 20).Value = objBO.IPDNo;					
					cmd.Parameters.Add("@from", SqlDbType.DateTime, 16).Value = objBO.from;					
					cmd.Parameters.Add("@to", SqlDbType.DateTime, 16).Value = objBO.to;					
					cmd.Parameters.Add("@prm_1", SqlDbType.VarChar, 16).Value = objBO.prm_1;					
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
        public dataSet FeedbackReport_Quesries(ipFeedBack objBO)
        {
            dataSet dsObj = new dataSet();
            using (SqlConnection con = new SqlConnection(GlobalConfig.ConStr_Hospital))
            {
                using (SqlCommand cmd = new SqlCommand("pFeedbackReport_Quesries", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandTimeout = 2500;
                    cmd.Parameters.Add("@Prm1", SqlDbType.VarChar, 100).Value = objBO.prm_1;
                    cmd.Parameters.Add("@Prm2", SqlDbType.VarChar, 100).Value = objBO.prm_2;
                    cmd.Parameters.Add("@empCode", SqlDbType.VarChar, 100).Value = objBO.empCode;
                    cmd.Parameters.Add("@from", SqlDbType.DateTime, 16).Value = objBO.from;
                    cmd.Parameters.Add("@to", SqlDbType.DateTime, 16).Value = objBO.to;
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
    }
}