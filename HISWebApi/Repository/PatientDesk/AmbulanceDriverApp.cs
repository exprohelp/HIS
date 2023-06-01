using HISWebApi.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
namespace HISWebApi.Repository.PatientDesk
{
    public class AmbulanceDriverApp
    {
        public List<AmbulanceResponse> AmbulanceAndEmergencyQueries(ipAmbulance objBO)
        {
            AmbulanceResponse obj = new AmbulanceResponse();
            List<AmbulanceResponse> list = new List<AmbulanceResponse>();
            using (SqlConnection con = new SqlConnection(GlobalConfig.ConStr_Hospital))
            {
                using (SqlCommand cmd = new SqlCommand("pAmbulanceAndEmergencyQueries", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandTimeout = 2500;
                    cmd.Parameters.Add("@RequestId", SqlDbType.VarChar, 10).Value = objBO.RequestId;
                    cmd.Parameters.Add("@DriverId", SqlDbType.VarChar, 10).Value = objBO.DriverId;
                    cmd.Parameters.Add("@AmbulanceId", SqlDbType.VarChar,10).Value = objBO.AmbulanceId;
                    cmd.Parameters.Add("@from", SqlDbType.VarChar).Value = objBO.from;
                    cmd.Parameters.Add("@to", SqlDbType.VarChar).Value = objBO.to;
                    cmd.Parameters.Add("@Prm1", SqlDbType.VarChar, 50).Value = objBO.Prm1;
                    cmd.Parameters.Add("@Prm2", SqlDbType.VarChar, 50).Value = objBO.Prm2;
                    cmd.Parameters.Add("@Logic", SqlDbType.VarChar, 50).Value = objBO.Logic;
                    try
                    {
                        con.Open();
                        DataSet ds = new DataSet();
                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        da.Fill(ds);
                        con.Close();
                        foreach (DataRow dr in ds.Tables[0].Rows)
                        {
                            obj = new AmbulanceResponse();
                            obj.RequestId = dr["RequestId"].ToString();
                            obj.IPDNo = dr["IPDNo"].ToString();
                            obj.Requestby = dr["Requestby"].ToString();
                            obj.RequestType = dr["RequestType"].ToString();
                            obj.PatientName = dr["PatientName"].ToString();
                            obj.RequestNote = dr["RequestNote"].ToString();
                            obj.PatientAddress = dr["PatientAddress"].ToString();
                            obj.Latitude = dr["Latitude"].ToString();
                            obj.Longitude = dr["Longitude"].ToString();
                            obj.MobileNo = dr["MobileNo"].ToString();
                            obj.PickupDate = dr["PickupDate"].ToString();
                            obj.PickupTime = dr["PickupTime"].ToString();
                            obj.AmbulanceId = dr["AmbulanceId"].ToString();

                            list.Add(obj);
                        }
                    }
                    catch (SqlException sqlEx)
                    {
                    }
                    finally { con.Close(); }
                    return list;
                }
            }
        }

        public String AmbulanceTrackingPatient(ipAmbulanceTrack obj)
        {
            string processInfo = "";
            using (SqlConnection con = new SqlConnection(GlobalConfig.ConStr_Hospital))
            {
                using (SqlCommand cmd = new SqlCommand("pAmbulance_TrackingPatient", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandTimeout = 2500;
                    cmd.Parameters.Add("@AmbulanceId", SqlDbType.VarChar, 10).Value = obj.AmbulanceId;
                    cmd.Parameters.Add("@UserPsw", SqlDbType.VarChar, 20).Value = obj.UserPsw;
                    cmd.Parameters.Add("@RequestId", SqlDbType.VarChar, 30).Value = obj.RequestId;
                    cmd.Parameters.Add("@Latitdude", SqlDbType.VarChar, 50).Value = obj.Latitdude;
                    cmd.Parameters.Add("@Longitude", SqlDbType.VarChar, 50).Value = obj.Longitude;
                    cmd.Parameters.Add("@Prm1", SqlDbType.VarChar, 100).Value = obj.Prm1;
                    cmd.Parameters.Add("@Logic", SqlDbType.VarChar, 50).Value = obj.Logic;
                    cmd.Parameters.Add("@result", SqlDbType.VarChar, 200).Value = "";
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

    }
}