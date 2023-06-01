using HISWebApi.Models;
using HISWebApi.Repository;
using HISWebApi.Repository.Dietician;
using HISWebApi.Repository.Utilities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using static HISWebApi.Models.IPDBO;
namespace HISWebApi.Repository.IPD.Nursing
{   
    public class MedicineIndent
    {       
        HISDBLayer his_layer = new HISDBLayer();
        public dataSet GetAdmittedIPDPatient()
        {
            string _result = string.Empty;
            return his_layer.GetAdmittedIPDPatient();
        }
        public dataSet GetPatientDetailByIPDNo(string IPDNo)
        {
            string _result = string.Empty;
            return his_layer.GetPatientDetailByIPDNo(IPDNo);
        }
        public DataSet PatientListForDietInit(ipDietician ipObj)
        {
            DataSet dsReport = new DataSet();
            string _result = string.Empty;
            dataSet ds = his_layer.GetAdmittedIPDPatient();
            string IpdNoList = string.Empty;
            foreach (DataRow dr in ds.ResultSet.Tables[0].Rows)
            {
                IpdNoList = IpdNoList + dr["IPDNO"].ToString() + ",";
            }

            Diet obj = new Diet();
            ipDietician objBO = new ipDietician();        
            objBO.Logic = ipObj.Logic;

            DataTable dt1 = ds.ResultSet.Tables[0];
            dt1.TableName = "Table1";

            dataSet ds2 = obj.diet_DiticianQueries(objBO);
            DataTable dt2 = ds2.ResultSet.Tables[0];
            dt2.TableName = "Table2";

            dsReport.Tables.Add(dt1.Copy()); //iTDOSE data
            dsReport.Tables.Add(dt2.Copy());
            return dsReport;
        }
        public DataSet PatientListForFeedBack(IPDBO.FeedbackInfo ipObj)
        {
            DataSet dsReport=new DataSet();
            string _result = string.Empty;
            dataSet ds=his_layer.GetAdmittedIPDPatient();
            string IpdNoList = string.Empty;
            foreach (DataRow dr in ds.ResultSet.Tables[0].Rows)
            {
                IpdNoList = IpdNoList + dr["IPDNO"].ToString() + ",";
            }

            IPD obj = new IPD();
            FeedbackInfo objBO = new FeedbackInfo();
            objBO.Prm3 = IpdNoList;
            objBO.Logic = ipObj.Logic;

            DataTable dt1 = ds.ResultSet.Tables[0];
            dt1.TableName = "Table1";

            dataSet ds2 = obj.IPD_FeedbackQuesries(objBO);
            DataTable dt2 = ds2.ResultSet.Tables[0];
            dt2.TableName = "Table2";

            dsReport.Tables.Add(dt1.Copy()); //iTDOSE data
            dsReport.Tables.Add(dt2.Copy());
            return dsReport;
        }
        public dataSet GetDoctor()
        {
            string _result = string.Empty;
            return his_layer.GetDoctor();
        }		
		public dataSet SearchMedicine(IPDBO.MedicineBO objBO)
        {
            string processInfo = string.Empty;
            dataSet dsObj = new dataSet();
            using (SqlConnection con = new SqlConnection(GlobalConfig.ConStr_ChandanPharmacyLive))
            {
                using (SqlCommand cmd = new SqlCommand("ITDOSE_SearchMedicine", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandTimeout = 2500;
                    cmd.Parameters.AddWithValue("@searchKey", objBO.searchKey);
                    cmd.Parameters.AddWithValue("@searchType", objBO.searchType);
                    cmd.Parameters.AddWithValue("@HISPanelId", objBO.PanelId);
                    try
                    {
                        con.Open();
                        DataSet ds = new DataSet();
                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        da.Fill(ds);
                        dsObj.ResultSet = ds;                       
                    }
                    catch (SqlException sqlEx)
                    {
                        dsObj.ResultSet = null;
                        dsObj.Msg = "Error Found   : " + sqlEx.Message;
                    }
                    finally { con.Close(); }
                    return dsObj;
                }
            }
        }
        public string Insert_IPOPIndent(List<IPDBO.IPOPIndentBO> objBO)
        {
            string processInfo = string.Empty;
            DataTable dt = ListtoDataTableConverter.ToDataTable(objBO);
            using (SqlConnection con = new SqlConnection(GlobalConfig.ConStr_ChandanPharmacyLive))
            {
                using (SqlCommand cmd = new SqlCommand("pInsert_IPOPIndent_New", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandTimeout = 2500;
                    cmd.Parameters.AddWithValue("@HISIndentTable", dt);
                    cmd.Parameters.Add("@result", SqlDbType.VarChar, 50).Value = "";
                    cmd.Parameters["@result"].Direction = ParameterDirection.InputOutput;
                    try
                    {
                        con.Open();
                        cmd.ExecuteNonQuery();
                        processInfo =cmd.Parameters["@result"].Value.ToString();
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
        public dataSet InPatientIndentReport(IPDBO.ipIndentReport objBO)
        {
            HISDBLayer his = new HISDBLayer();
            string ipdList = string.Empty;
            dataSet dsPrepared = new dataSet();
            if (objBO.IPOPNoList == "ALL")
            {
                //Get In-Patient IPD List
                dataSet ds = his.GetAdmittedIPDPatient();
                foreach (DataRow dr in ds.ResultSet.Tables[0].Rows)
                {
                    ipdList = ipdList + dr["IPDNO"].ToString() + ",";
                }
                objBO.IPOPNoList = ipdList;
                objBO.Logic = "IndentReport";
                //Passing that list and get information from chandanpharmacy_live
                dsPrepared =IPOP_IndentQueries(objBO);
            }
            return dsPrepared;
        }
        public dataSet IPOP_IndentQueries(IPDBO.ipIndentReport objBO)
        {
            string processInfo = string.Empty;
            dataSet dsObj = new dataSet();
            using (SqlConnection con = new SqlConnection(GlobalConfig.ConStr_ChandanPharmacyLive))
            {
                using (SqlCommand cmd = new SqlCommand("pIPOP_IndentQueries", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandTimeout = 2500;
                    cmd.Parameters.AddWithValue("@HospId", objBO.HospId);
                    cmd.Parameters.AddWithValue("@IPOPNoList", objBO.IPOPNoList);
                    cmd.Parameters.AddWithValue("@indent_no", objBO.indent_no);
                    cmd.Parameters.AddWithValue("@prm_1", objBO.prm_1);
                    cmd.Parameters.AddWithValue("@prm_2", objBO.prm_2);
                    cmd.Parameters.AddWithValue("@login_id", objBO.login_id);
                    cmd.Parameters.AddWithValue("@Logic", objBO.Logic);
                    try
                    {
                        con.Open();
                        DataSet ds = new DataSet();
                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        da.Fill(ds);
                        dsObj.ResultSet = ds;
                    }
                    catch (SqlException sqlEx)
                    {
                        dsObj.ResultSet = null;
                        dsObj.Msg = "Error Found   : " + sqlEx.Message;
                    }
                    finally { con.Close(); }
                    return dsObj;
                }
            }
        }
        public dataSet GetNurseDetails()
        {
            string processInfo = string.Empty;
            dataSet dsObj = new dataSet();
            using (SqlConnection con = new SqlConnection(GlobalConfig.ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("pHIS_GetNurseDetails", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandTimeout = 2500;                   
                    try
                    {
                        con.Open();
                        DataSet ds = new DataSet();
                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        da.Fill(ds);
                        dsObj.ResultSet = ds;
                    }
                    catch (SqlException sqlEx)
                    {
                        dsObj.ResultSet = null;
                        dsObj.Msg = "Error Found   : " + sqlEx.Message;
                    }
                    finally { con.Close(); }
                    return dsObj;
                }
            }
        }
        public dataSet GetKitInformation(IPDBO.KitInfoBO objBO)
        {
            string processInfo = string.Empty;
            dataSet dsObj = new dataSet();
            using (SqlConnection con = new SqlConnection(GlobalConfig.ConStr_ChandanPharmacyLive))
            {
                using (SqlCommand cmd = new SqlCommand("Insert_Modify_hosp_kit_info", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandTimeout = 2500;
                    cmd.Parameters.AddWithValue("@unit_id", objBO.unit_id);
                    cmd.Parameters.AddWithValue("@Kit_id", objBO.Kit_id);
                    cmd.Parameters.AddWithValue("@kit_name", objBO.kit_name);
                    cmd.Parameters.AddWithValue("@item_id", objBO.item_id);
                    cmd.Parameters.AddWithValue("@qty", objBO.qty);
                    cmd.Parameters.AddWithValue("@login_id", objBO.login_id);
                    cmd.Parameters.AddWithValue("@isActive", objBO.isActive);
                    cmd.Parameters.AddWithValue("@Logic", objBO.Logic);
                    cmd.Parameters.Add("@result", SqlDbType.VarChar, 100).Value = "";
                    cmd.Parameters["@result"].Direction = ParameterDirection.InputOutput;
                    try
                    {
                        con.Open();
                        DataSet ds = new DataSet();
                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        da.Fill(ds);
                        dsObj.ResultSet = ds;
                    }
                    catch (SqlException sqlEx)
                    {
                        dsObj.ResultSet = null;
                        dsObj.Msg = "Error Found   : " + sqlEx.Message;
                    }
                    finally { con.Close(); }
                    return dsObj;
                }
            }
        }
        public string HandOverTakeOverInsert(List<IPDBO.ipHandoverInfo> objBO)
        {
            dataSet dsObj = new dataSet();
            string processInfo = string.Empty;
            var login_id = string.Empty;
            var logic = string.Empty;
            if (objBO.Count > 0)
            {
                DataTable dt = new DataTable();
                dt.Columns.Add("IpdNo", typeof(string));
                dt.Columns.Add("PatientName", typeof(string));
                dt.Columns.Add("Age", typeof(string));
                dt.Columns.Add("Gender", typeof(string));
                dt.Columns.Add("DoctorId", typeof(string));
                dt.Columns.Add("DoctorName", typeof(string));
                dt.Columns.Add("AdmitDate", typeof(string));
                dt.Columns.Add("FloorName", typeof(string));
                dt.Columns.Add("StaffCode", typeof(string));
                dt.Columns.Add("ShiftName", typeof(string));
                foreach (IPDBO.ipHandoverInfo obj in objBO)
                {
                    login_id = obj.login_id;
                    logic = obj.Logic;
                    DataRow dr = dt.NewRow();
                    dr["IpdNo"] = obj.IpdNo;
                    dr["PatientName"] = obj.PatientName;
                    dr["Age"] = obj.Age;
                    dr["Gender"] = obj.Gender;
                    dr["DoctorId"] = obj.DoctorId;
                    dr["DoctorName"] = obj.DoctorName;
                    dr["AdmitDate"] = obj.AdmitDate;
                    dr["FloorName"] = obj.FloorName;
                    dr["StaffCode"] = obj.StaffCode;
                    dr["ShiftName"] = obj.ShiftName;
                    dt.Rows.Add(dr);
                }
                using (SqlConnection con = new SqlConnection(GlobalConfig.ConStr_Hospital))
                {
                    using (SqlCommand cmd = new SqlCommand("pIPD_HandOverTakeOverInsert", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandTimeout = 2500;
                        cmd.Parameters.AddWithValue("@UDT_HandOverTakeOverEntry", dt);
                        cmd.Parameters.Add("@LoginId", SqlDbType.VarChar, 10).Value = login_id;
                        cmd.Parameters.Add("@Logic", SqlDbType.VarChar, 50).Value = logic;
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
                            processInfo = "Error Found:" + sqlEx.Message;
                        }
                        finally { con.Close(); }
                    }
                }
            }
            return processInfo;
        }
    }
}