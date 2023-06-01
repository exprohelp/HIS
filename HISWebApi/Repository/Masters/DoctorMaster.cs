
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using HISWebApi.Models;

namespace HISWebApi.Repository.Masters
{
    public class DoctorMaster
    {
        public dataSet DepartmentMasterQueries(DoctorMastersBO objBO)
        {
            dataSet dsObj = new dataSet();
            using (SqlConnection con = new SqlConnection(GlobalConfig.ConStr_Hospital))
            {
                using (SqlCommand cmd = new SqlCommand("pm_DepartmentMasterQueries", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandTimeout = 2500;
                    cmd.Parameters.Add("@autoId", SqlDbType.VarChar, 10).Value = objBO.autoId;
                    cmd.Parameters.Add("@hosp_id", SqlDbType.VarChar, 10).Value = objBO.hosp_id;
                    cmd.Parameters.Add("@prm_1", SqlDbType.VarChar, 50).Value = objBO.prm_1;
                    cmd.Parameters.Add("@prm_2", SqlDbType.VarChar, 50).Value = objBO.prm_2;
                    cmd.Parameters.Add("@doctorId", SqlDbType.VarChar, 50).Value = objBO.doctorId;
                    cmd.Parameters.Add("@Logic", SqlDbType.VarChar, 50).Value = objBO.Logic;
                    cmd.Parameters.Add("@loginId", SqlDbType.VarChar, 10).Value = objBO.login_id;
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
        public dataSet DoctorMasterQueries(DoctorMastersBO objBO)
        {
            dataSet dsObj = new dataSet();
            using (SqlConnection con = new SqlConnection(GlobalConfig.ConStr_Hospital))
            {
                using (SqlCommand cmd = new SqlCommand("pm_DoctorMasterQueries", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandTimeout = 2500;
                    cmd.Parameters.Add("@autoId", SqlDbType.VarChar, 10).Value = objBO.autoId;
                    cmd.Parameters.Add("@hosp_id", SqlDbType.VarChar, 10).Value = objBO.hosp_id;
                    cmd.Parameters.Add("@prm_1", SqlDbType.VarChar, 50).Value = objBO.prm_1;
                    cmd.Parameters.Add("@prm_2", SqlDbType.VarChar, 50).Value = objBO.prm_2;
                    cmd.Parameters.Add("@doctorId", SqlDbType.VarChar, 50).Value = objBO.doctorId;
                    cmd.Parameters.Add("@Logic", SqlDbType.VarChar, 50).Value = objBO.Logic;
                    cmd.Parameters.Add("@loginId", SqlDbType.VarChar, 10).Value = objBO.login_id;
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

        public string mInsertUpdateDoctor(DoctorMastersBO obj)
        {
            dataSet dsObj = new dataSet();
            string processInfo = string.Empty;
            using (SqlConnection con = new SqlConnection(GlobalConfig.ConStr_Hospital))
            {
                using (SqlCommand cmd = new SqlCommand("pm_DoctorInsertUpdate", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandTimeout = 2500;
                    cmd.Parameters.Add("@DoctorId", SqlDbType.VarChar, 16).Value = obj.doctorId;
                    cmd.Parameters.Add("@HISDoctorId", SqlDbType.VarChar, 16).Value = obj.HISDoctorId;
                    cmd.Parameters.Add("@Hosp_Id", SqlDbType.VarChar, 16).Value = obj.hosp_id;
                    cmd.Parameters.Add("@Prefix", SqlDbType.VarChar, 50).Value = obj.Prefix;
                    cmd.Parameters.Add("@DoctorName", SqlDbType.VarChar, 50).Value = obj.doctorname;
                    cmd.Parameters.Add("@DoctorType", SqlDbType.VarChar, 25).Value = obj.doctype;
                    cmd.Parameters.Add("@landline_no", SqlDbType.VarChar, 15).Value = obj.phone;
                    cmd.Parameters.Add("@mobile_no", SqlDbType.VarChar, 10).Value = obj.mobile;
                    cmd.Parameters.Add("@addres1", SqlDbType.VarChar, 300).Value = obj.address;
                    cmd.Parameters.Add("@speciality", SqlDbType.VarChar, 100).Value = obj.specialization;
                    cmd.Parameters.Add("@DeptId", SqlDbType.VarChar, 15).Value = obj.DeptId;
                    cmd.Parameters.Add("@Designation", SqlDbType.VarChar, 100).Value = obj.Designation;
                    cmd.Parameters.Add("@degree", SqlDbType.VarChar, 100).Value = obj.degree;
                    cmd.Parameters.Add("@degree2", SqlDbType.VarChar, 500).Value = obj.degree2;
                    cmd.Parameters.Add("@gender", SqlDbType.VarChar, 10).Value = obj.gender;
                    cmd.Parameters.Add("@imaregno", SqlDbType.VarChar, 25).Value = obj.imaregno;
                    cmd.Parameters.Add("@imaregdate", SqlDbType.VarChar, 30).Value = obj.regdate;
                    cmd.Parameters.Add("@emergency_availability", SqlDbType.VarChar, 30).Value = obj.Emrgavail;
                    cmd.Parameters.Add("@emp_code", SqlDbType.VarChar, 30).Value = obj.emp_code;
                    cmd.Parameters.Add("@share_flag", SqlDbType.VarChar, 30).Value = obj.docshare;
                    cmd.Parameters.Add("@online_appointment", SqlDbType.VarChar, 30).Value = obj.onlineAppoint;
                    cmd.Parameters.Add("@IsTokenRequired", SqlDbType.VarChar, 30).Value = obj.IsTokenReq;
                    cmd.Parameters.Add("@fee_freq", SqlDbType.VarChar, 30).Value = obj.feefreq;
                    cmd.Parameters.Add("@RoomNo", SqlDbType.VarChar, 30).Value = obj.roomno;
                    cmd.Parameters.Add("@FloorName", SqlDbType.VarChar, 30).Value = obj.floorno;
                    cmd.Parameters.Add("@Isactive", SqlDbType.VarChar, 1).Value = obj.drstatus;
                    cmd.Parameters.Add("@IsDirector", SqlDbType.VarChar, 1).Value = obj.IsDirector;
                    cmd.Parameters.Add("@IsOPD", SqlDbType.VarChar, 1).Value = obj.IsOPD;
                    cmd.Parameters.Add("@IsFreeOPD", SqlDbType.VarChar, 1).Value = obj.IsFreeOPD;
                    cmd.Parameters.Add("@IsProfile", SqlDbType.VarChar, 1).Value = obj.IsProfile;
                    cmd.Parameters.Add("@IsExamRoomByPass", SqlDbType.VarChar, 1).Value = obj.IsExamRoomByPass;
                    cmd.Parameters.Add("@duration", SqlDbType.VarChar, 5).Value = obj.patientduration;
                    cmd.Parameters.Add("@VirtualPhotoPath", SqlDbType.VarChar, 100).Value = obj.virtual_path;
                    cmd.Parameters.Add("@Prm1", SqlDbType.VarChar, 50).Value = obj.prm_1;
                    cmd.Parameters.Add("@TagName", SqlDbType.VarChar, 50).Value = obj.TagName;
                    cmd.Parameters.Add("@Description", SqlDbType.VarChar).Value = obj.Description;
                    cmd.Parameters.Add("@login_id", SqlDbType.VarChar, 30).Value = obj.login_id;
                    cmd.Parameters.Add("@ipaddress", SqlDbType.VarChar, 30).Value = "";
                    cmd.Parameters.Add("@Logic", SqlDbType.VarChar, 30).Value = obj.Logic;
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

        public string InsertUpdateDoctorTimeSlot(DoctorsSlot obj)
        {
            dataSet dsObj = new dataSet();
            string processInfo = string.Empty;
            string[] days = obj.Daysvalues.Split(',');
            if (days.Length > 0)
            {
                using (SqlConnection con = new SqlConnection(GlobalConfig.ConStr_Hospital))
                {
                    for (int i = 0; i < days.Length; i++)
                    {
                        string finaldays = days[i].ToString();
                        using (SqlCommand cmd = new SqlCommand("pm_DoctorInsertSchedule", con))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.CommandTimeout = 2500;
                            cmd.Parameters.Add("@doctorId", SqlDbType.VarChar, 30).Value = obj.doctorId;
                            cmd.Parameters.Add("@starttime", SqlDbType.VarChar, 30).Value = obj.StartTime;
                            cmd.Parameters.Add("@endtime", SqlDbType.VarChar, 30).Value = obj.EndTime;
                            cmd.Parameters.Add("@shiftname", SqlDbType.VarChar, 30).Value = obj.ShiftName;
                            cmd.Parameters.Add("@patientlimit", SqlDbType.VarChar, 30).Value = obj.PatientLimit;
                            cmd.Parameters.Add("@days", SqlDbType.VarChar, 30).Value = finaldays;
                            cmd.Parameters.Add("@login_id", SqlDbType.VarChar, 30).Value = obj.login_id;
                            cmd.Parameters.Add("@Logic", SqlDbType.VarChar, 30).Value = obj.Logic;
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
                        }
                    }
                }
            }
            return processInfo;
        }

        public string InsertDoctorLeave(List<DoctorLeave> obj)
        {
            dataSet dsObj = new dataSet();
            string processInfo = string.Empty;
            string AttType = string.Empty;
            if (obj.Count > 0)
            {
                using (SqlConnection con = new SqlConnection(GlobalConfig.ConStr_Hospital))
                {
                    string emp_name = "";
                    string dates = "";
                    string dates_last = "";
                    foreach (var item in obj)
                    {
                        using (SqlCommand cmd = new SqlCommand("pm_DoctorInsertSchedule", con))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.CommandTimeout = 2500;
                            cmd.Parameters.Add("@doctorId", SqlDbType.VarChar, 30).Value = item.doctorId;
                            cmd.Parameters.Add("@inputdate", SqlDbType.VarChar, 30).Value = item.inputdate;
                            cmd.Parameters.Add("@starttime", SqlDbType.VarChar, 30).Value = item.StartTime;
                            cmd.Parameters.Add("@endtime", SqlDbType.VarChar, 30).Value = item.EndTime;
                            cmd.Parameters.Add("@login_id", SqlDbType.VarChar, 30).Value = item.login_id;
                            cmd.Parameters.Add("@Logic", SqlDbType.VarChar, 30).Value = item.Logic;
                            cmd.Parameters.Add("@result", SqlDbType.VarChar, 50).Value = "";
                            cmd.Parameters["@result"].Direction = ParameterDirection.InputOutput;
                            try
                            {
                                con.Open();
                                cmd.ExecuteNonQuery();
                                processInfo=(string)cmd.Parameters["@result"].Value.ToString().Split('|')[0];
                                emp_name=(string)cmd.Parameters["@result"].Value.ToString().Split('|')[1];
                                AttType= (string)cmd.Parameters["@result"].Value.ToString().Split('|')[2];
                                dates_last = item.inputdate;
                                if (dates=="")
                                dates = item.inputdate;
                            }
                            catch (SqlException sqlEx)
                            {
                                processInfo = "Error Found   : " + sqlEx.Message;
                            }
                            finally { con.Close(); }
                        }
                    }
                    dates = dates + " to "+ dates_last;
                    string sms = ""+ emp_name + " has requested "+ AttType + " from "+dates+". Team Chandan";
                    Utilities.SmsClass smObj = new Utilities.SmsClass();
                    string response= smObj.SendSms("8896948383",sms); //Farukh Sir
                               smObj.SendSms("8147009051",sms); //Garima

                    string qry_sms = "insert into online_doctorAppSmsLog(appointmentId, mobile_no, sms, login_id, response_message) ";
                    qry_sms += "values('LeaveRequest','8896948383', '" + sms + "', '-','" + response + "')";
                    DBManager.QueryExecute(qry_sms, GlobalConfig.ConStr_Hospital);
                }
            }
            return processInfo;

        }

        public string DeleteDoctorSchedule(DoctorsSlot obj)
        {
            dataSet dsObj = new dataSet();
            string processInfo = string.Empty;
            using (SqlConnection con = new SqlConnection(GlobalConfig.ConStr_Hospital))
            {
                using (SqlCommand cmd = new SqlCommand("pm_DoctorInsertSchedule", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandTimeout = 2500;
                    cmd.Parameters.Add("@auto_id", SqlDbType.VarChar, 30).Value = obj.autoid;
                    cmd.Parameters.Add("@doctorId", SqlDbType.VarChar, 30).Value = obj.doctorId;
                    cmd.Parameters.Add("@login_id", SqlDbType.VarChar, 30).Value = obj.login_id;
                    cmd.Parameters.Add("@Logic", SqlDbType.VarChar, 30).Value = obj.Logic;
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
                }
            }
            return processInfo;
        }

        public string InsertDoctorRateList(DoctorRateList obj)
        {
            dataSet dsObj = new dataSet();
            string processInfo = string.Empty;
            DataTable dt = JsonConvert.DeserializeObject<DataTable>(obj.lstarrvalues);
            if (dt.Rows.Count > 0)
            {
                using (SqlConnection con = new SqlConnection(GlobalConfig.ConStr_Hospital))
                {
                    using (SqlCommand cmd = new SqlCommand("pratelist_InsertDoctorVisits", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandTimeout = 2500;                        
                        cmd.Parameters.AddWithValue("@DrRateList", dt);
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
                        //@CarType CarTableType READONLY
                    }

                }
            }
            return processInfo;
        }

        public string DoctorInsertSchedule(DoctorLeave obj)        {
            dataSet dsObj = new dataSet();
            string processInfo = string.Empty;
            using (SqlConnection con = new SqlConnection(GlobalConfig.ConStr_Hospital))
            {
                using (SqlCommand cmd = new SqlCommand("pm_DoctorInsertSchedule", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandTimeout = 2500;
                    cmd.Parameters.Add("@auto_id", SqlDbType.VarChar, 30).Value = obj.autoid;
                    cmd.Parameters.Add("@doctorId", SqlDbType.VarChar, 30).Value = obj.doctorId;
                    cmd.Parameters.Add("@login_id", SqlDbType.VarChar, 30).Value = obj.login_id;
                    cmd.Parameters.Add("@Logic", SqlDbType.VarChar, 30).Value = obj.Logic;
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
                }
            }
            return processInfo;
        }
        
    }
}
