using HISWebApi.Models;
using HISWebApi.Repository.IPD;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace HISWebApi.Repository.OPD
{
    public class Vaccination
    {
        HISDBLayer hisDB = new HISDBLayer();
        public dataSet OPD_VaccineQueries(ipVaccine objBO)
        {
            dataSet dsObj = new dataSet();
            using (SqlConnection con = new SqlConnection(GlobalConfig.ConStr_Hospital))
            {
                using (SqlCommand cmd = new SqlCommand("pOPD_VaccineQueries", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandTimeout = 2500;
                    cmd.Parameters.Add("@UHID", SqlDbType.VarChar, 30).Value = objBO.UHID;
                    cmd.Parameters.Add("@app_no", SqlDbType.VarChar, 20).Value = objBO.app_no;
                    cmd.Parameters.Add("@DoctorId", SqlDbType.VarChar, 10).Value = objBO.DoctorId;
                    cmd.Parameters.Add("@Template", SqlDbType.VarChar, 50).Value = objBO.Template;
                    cmd.Parameters.Add("@VaccineId", SqlDbType.Int).Value = objBO.VaccineId;
                    cmd.Parameters.Add("@dob", SqlDbType.Date).Value = objBO.dob;
                    cmd.Parameters.Add("@from", SqlDbType.Date).Value = objBO.from;
                    cmd.Parameters.Add("@to", SqlDbType.Date).Value = objBO.to;
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
                        if (objBO.Logic == "GetPatientInfoByUHID" && ds.Tables[0].Rows.Count == 0)
                        {
                            dsObj = hisDB.GetPatientInfoByUHID(objBO.UHID);
                        }
                        else if (objBO.Logic == "GetPatientInfoByUHID" && ds.Tables[0].Rows.Count > 0)
                        {
                            dataSet dsObj2 = new dataSet();
                            dsObj2 = hisDB.GetPatientInfoByUHID(objBO.UHID);
                            VaccinationInfo vaccinationInfo = new VaccinationInfo();
                            vaccinationInfo.UHID = objBO.UHID;
                            vaccinationInfo.Prm1 = dsObj2.ResultSet.Tables[0].Rows[0]["PatientName"].ToString();
                            vaccinationInfo.from = "1900/01/01";
                            vaccinationInfo.to = "1900/01/01";
                            vaccinationInfo.Logic = "UpdatePatientInfoByITDose";
                            OPD_VaccinationInsertUpdate(vaccinationInfo);
                        }
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
        public string OPD_InsertVaccinationInfo(List<VaccinationInfo> objBO)
        {
            string processInfo = string.Empty;
            string Logic = string.Empty;
            string login_id = string.Empty;
            DataTable dt = new DataTable();
            if (objBO.Count > 0)
            {
                dt.Columns.Add("IPDNo", typeof(string));
                dt.Columns.Add("UHID", typeof(string));
                dt.Columns.Add("PatientName", typeof(string));
                dt.Columns.Add("Gender", typeof(string));
                dt.Columns.Add("DOB", typeof(DateTime));
                dt.Columns.Add("StartDate", typeof(DateTime));
                dt.Columns.Add("AttendantName", typeof(string));
                dt.Columns.Add("mobileNo", typeof(string));
                dt.Columns.Add("TemplateName", typeof(string));
                dt.Columns.Add("VaccineId", typeof(int));
                dt.Columns.Add("VaccineName", typeof(string));
                dt.Columns.Add("ScheduleDay", typeof(int));
                dt.Columns.Add("ScheduleDate", typeof(DateTime));
                dt.Columns.Add("PlanedFromDate", typeof(DateTime));
                dt.Columns.Add("PlanedToDate", typeof(DateTime));
                dt.Columns.Add("PlanedBy", typeof(string));
                dt.Columns.Add("PharSaleInvNo", typeof(string));
                dt.Columns.Add("VaccinationDate", typeof(DateTime));
                dt.Columns.Add("District", typeof(string));
                dt.Columns.Add("Address", typeof(string));
                dt.Columns.Add("VaccineGivenBy", typeof(string));
                dt.Columns.Add("VaccinationTime", typeof(DateTime));
                foreach (VaccinationInfo obj in objBO)
                {
                    login_id = obj.login_id;
                    Logic = obj.Logic;
                    DataRow dr = dt.NewRow();
                    dr["IPDNo"] = obj.IPDNo;
                    dr["UHID"] = obj.UHID;
                    dr["PatientName"] = obj.PatientName;
                    dr["Gender"] = obj.Gender;
                    dr["DOB"] = obj.DOB;
                    dr["StartDate"] = obj.StartDate;
                    dr["AttendantName"] = obj.AttendantName;
                    dr["mobileNo"] = obj.mobileNo;
                    dr["TemplateName"] = obj.TemplateName;
                    dr["VaccineId"] = obj.VaccineId;
                    dr["VaccineName"] = obj.VaccineName;
                    dr["ScheduleDay"] = obj.ScheduleDay;
                    dr["ScheduleDate"] = obj.ScheduleDate ?? (object)DBNull.Value;
                    dr["PlanedFromDate"] = obj.PlanedFromDate ?? (object)DBNull.Value;
                    dr["PlanedToDate"] = obj.PlanedToDate ?? (object)DBNull.Value;
                    dr["PlanedBy"] = obj.PlanedBy;
                    dr["PharSaleInvNo"] = obj.PharSaleInvNo;
                    dr["VaccinationDate"] = obj.VaccinationDate ?? (object)DBNull.Value;
                    dr["District"] = obj.District;
                    dr["Address"] = obj.Address;
                    dr["VaccineGivenBy"] = obj.VaccineGivenBy;
                    dr["VaccinationTime"] = obj.VaccinationTime ?? (object)DBNull.Value;
                    dt.Rows.Add(dr);
                }
            }
            using (SqlConnection con = new SqlConnection(GlobalConfig.ConStr_Hospital))
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand("pOPD_VaccinationInfoInsert", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandTimeout = 2500;
                    cmd.Parameters.AddWithValue("@udt_VaccinationInfo", dt);
                    cmd.Parameters.Add("@Logic", SqlDbType.VarChar, 50).Value = Logic;
                    cmd.Parameters.Add("@login_id", SqlDbType.VarChar, 20).Value = login_id;
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
        public string OPD_VaccinationInsertUpdate(VaccinationInfo objBO)
        {
            string processInfo = string.Empty;
            using (SqlConnection con = new SqlConnection(GlobalConfig.ConStr_Hospital))
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand("pOPD_VaccinationInsertUpdate", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandTimeout = 2500;
                    cmd.Parameters.Add("@UHID", SqlDbType.VarChar, 30).Value = objBO.UHID;
                    cmd.Parameters.Add("@app_no", SqlDbType.VarChar, 20).Value = objBO.app_no;
                    cmd.Parameters.Add("@VaccineId", SqlDbType.Int).Value = objBO.VaccineId;
                    cmd.Parameters.Add("@from", SqlDbType.Date).Value = objBO.from;
                    cmd.Parameters.Add("@to", SqlDbType.Date).Value = objBO.to;
                    cmd.Parameters.Add("@Prm1", SqlDbType.VarChar, 100).Value = objBO.Prm1;
                    cmd.Parameters.Add("@Prm2", SqlDbType.VarChar, 100).Value = objBO.Prm2;
                    cmd.Parameters.Add("@login_id", SqlDbType.VarChar, 20).Value = objBO.login_id;
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
    }
}