using HISWebApi.Models;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace HISWebApi.Repository.Lab
{
    public class Sample
    {
        public dataSet LabReporting_Queries(LabReporting objBO)
        {
            dataSet dsObj = new dataSet();
            using (SqlConnection con = new SqlConnection(GlobalConfig.ConStr_Hospital))
            {
                using (SqlCommand cmd = new SqlCommand("pLab_ReportingQueries", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandTimeout = 2500;
                    cmd.Parameters.Add("@LabCode", SqlDbType.VarChar, 10).Value = objBO.LabCode;
                    cmd.Parameters.Add("@IpOpType", SqlDbType.VarChar, 50).Value = objBO.IpOpType;
                    cmd.Parameters.Add("@ReportStatus", SqlDbType.VarChar, 50).Value = objBO.ReportStatus;
                    cmd.Parameters.Add("@SubCat", SqlDbType.VarChar, 100).Value = objBO.SubCat;
                    cmd.Parameters.Add("@VisitNo", SqlDbType.VarChar, 20).Value = objBO.VisitNo;
                    cmd.Parameters.Add("@BarccodeNo", SqlDbType.VarChar, 15).Value = objBO.BarccodeNo;
                    cmd.Parameters.Add("@TestCategory", SqlDbType.VarChar, 50).Value = objBO.TestCategory;
                    cmd.Parameters.Add("@AutoTestId", SqlDbType.Int).Value = objBO.AutoTestId;
                    cmd.Parameters.Add("@TestCode", SqlDbType.VarChar, 10).Value = objBO.TestCode;
                    cmd.Parameters.Add("@from", SqlDbType.Date).Value = objBO.from;
                    cmd.Parameters.Add("@to", SqlDbType.Date).Value = objBO.to;
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
        public dataSet Lab_SampleCollectionQueries(SampleCollection objBO)
        {
            dataSet dsObj = new dataSet();
            using (SqlConnection con = new SqlConnection(GlobalConfig.ConStr_Hospital))
            {
                using (SqlCommand cmd = new SqlCommand("pLab_SampleCollectionQueries", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandTimeout = 2500;
                    cmd.Parameters.Add("@hosp_id", SqlDbType.VarChar, 10).Value = objBO.hosp_id;
                    cmd.Parameters.Add("@VisitNo", SqlDbType.VarChar, 20).Value = objBO.VisitNo;
                    cmd.Parameters.Add("@BarcodeNo", SqlDbType.VarChar, 10).Value = objBO.BarcodeNo;
                    cmd.Parameters.Add("@SampleCode", SqlDbType.VarChar, 10).Value = objBO.SampleCode;
                    cmd.Parameters.Add("@TestCode", SqlDbType.VarChar, 10).Value = objBO.TestCode;
                    cmd.Parameters.Add("@from", SqlDbType.Date, 12).Value = objBO.from;
                    cmd.Parameters.Add("@to", SqlDbType.Date, 12).Value = objBO.to;
                    cmd.Parameters.Add("@Prm1", SqlDbType.VarChar, 50).Value = objBO.Prm1;
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
        public dataSet SampleDispatchQueries(SampleDispatchInfo objBO)
        {
            dataSet dsObj = new dataSet();
            using (SqlConnection con = new SqlConnection(GlobalConfig.ConStr_Hospital))
            {
                using (SqlCommand cmd = new SqlCommand("pLab_SampleDispatchQueries", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandTimeout = 2500;
                    cmd.Parameters.Add("@hosp_id", SqlDbType.VarChar, 10).Value = objBO.hosp_id;
                    cmd.Parameters.Add("@VisitNo", SqlDbType.VarChar, 20).Value = objBO.VisitNo;
                    cmd.Parameters.Add("@BarcodeNo", SqlDbType.VarChar, 10).Value = objBO.BarcodeNo;
                    cmd.Parameters.Add("@DispatchLabCode", SqlDbType.VarChar, 10).Value = objBO.DispatchLabCode;
                    cmd.Parameters.Add("@TestCode", SqlDbType.VarChar, 10).Value = objBO.TestCode;
                    cmd.Parameters.Add("@from", SqlDbType.DateTime, 10).Value = objBO.from;
                    cmd.Parameters.Add("@to", SqlDbType.DateTime, 10).Value = objBO.to;
                    cmd.Parameters.Add("@Prm1", SqlDbType.VarChar, 50).Value = objBO.Prm1;
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
        public string Lab_ResultEntry(List<LabTestResultEntry> objBO)
        {
            dataSet dsObj = new dataSet();
            string processInfo = string.Empty;
            var VisitNo = string.Empty;
            var dispatchLab = string.Empty;
            var SubCat = string.Empty;
            var DoctorSignId = string.Empty;
            var EntrySaveType = string.Empty;
            var login_id = string.Empty;
            var Logic = string.Empty;
            if (objBO.Count > 0)
            {
                DataTable dt = new DataTable();
                dt.Columns.Add("AutoTestId", typeof(int));
                dt.Columns.Add("TestCode", typeof(string));
                dt.Columns.Add("ObservationId", typeof(string));
                dt.Columns.Add("ab_flag", typeof(char));
                dt.Columns.Add("read_1", typeof(string));
                dt.Columns.Add("read_2", typeof(string));
                dt.Columns.Add("test_comment", typeof(string));
                dt.Columns.Add("min_value", typeof(float));
                dt.Columns.Add("max_value", typeof(float));
                dt.Columns.Add("nr_range", typeof(string));
                dt.Columns.Add("result_unit", typeof(string));
                dt.Columns.Add("method_name", typeof(string));
                dt.Columns.Add("r_type", typeof(string));
                dt.Columns.Add("report_text_content", typeof(string));
                foreach (LabTestResultEntry obj in objBO)
                {
                    VisitNo = obj.VisitNo;
                    dispatchLab = obj.dispatchLab;
                    SubCat = obj.SubCat;
                    DoctorSignId = obj.DoctorSignId;
                    login_id = obj.login_id;
                    EntrySaveType = obj.EntrySaveType;
                    Logic = obj.Logic;
                    DataRow dr = dt.NewRow();
                    dr["AutoTestId"] = obj.AutoTestId;
                    dr["TestCode"] = obj.TestCode;
                    dr["ObservationId"] = obj.ObservationId;
                    dr["ab_flag"] = obj.ab_flag;
                    dr["read_1"] = obj.read_1;
                    dr["read_2"] = obj.read_2;
                    dr["test_comment"] = obj.test_comment;
                    dr["min_value"] = obj.min_value;
                    dr["max_value"] = obj.max_value;
                    dr["nr_range"] = obj.nr_range;
                    dr["result_unit"] = obj.result_unit;
                    dr["method_name"] = obj.method_name;
                    dr["r_type"] = obj.r_type;
                    dr["report_text_content"] = obj.report_text_content;
                    dt.Rows.Add(dr);
                }
                using (SqlConnection con = new SqlConnection(GlobalConfig.ConStr_Hospital))
                {
                    using (SqlCommand cmd = new SqlCommand("pLab_ResultEntry", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandTimeout = 2500;
                        cmd.Parameters.Add("@VisitNo", SqlDbType.VarChar, 20).Value = VisitNo;
                        cmd.Parameters.Add("@dispatchLab", SqlDbType.VarChar, 20).Value = dispatchLab;
                        cmd.Parameters.Add("@SubCat", SqlDbType.VarChar, 100).Value = SubCat;
                        cmd.Parameters.AddWithValue("UDT_LabTestResultEntry", dt);
                        cmd.Parameters.Add("@LoginId", SqlDbType.VarChar, 20).Value = login_id;
                        cmd.Parameters.Add("@DoctorSignId", SqlDbType.VarChar, 20).Value = DoctorSignId;
                        cmd.Parameters.Add("@EntrySaveType", SqlDbType.VarChar, 50).Value = EntrySaveType;
                        cmd.Parameters.Add("@Logic", SqlDbType.VarChar, 50).Value = Logic;
                        cmd.Parameters.Add("@result", SqlDbType.VarChar, 100).Value = "";
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
            return processInfo;
        }
        public string Lab_SampleCollection(List<SampleCollection> objBO)
        {
            dataSet dsObj = new dataSet();
            string processInfo = string.Empty;
            var VisitNo = string.Empty;
            var login_id = string.Empty;
            if (objBO.Count > 0)
            {
                DataTable dt = new DataTable();
                dt.Columns.Add("AutotestId", typeof(int));
                dt.Columns.Add("testcode", typeof(string));
                dt.Columns.Add("sampleCode", typeof(string));
                dt.Columns.Add("BarcodeNo", typeof(string));
                dt.Columns.Add("VialQty", typeof(int));
                foreach (SampleCollection obj in objBO)
                {
                    VisitNo = obj.VisitNo;
                    login_id = obj.login_id;
                    DataRow dr = dt.NewRow();
                    dr["AutotestId"] = obj.AutotestId;
                    dr["testcode"] = obj.TestCode;
                    dr["sampleCode"] = obj.SampleCode;
                    dr["BarcodeNo"] = obj.BarcodeNo;
                    dr["VialQty"] = obj.VialQty;
                    dt.Rows.Add(dr);
                }
                using (SqlConnection con = new SqlConnection(GlobalConfig.ConStr_Hospital))
                {
                    using (SqlCommand cmd = new SqlCommand("pLab_SampleCollection", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandTimeout = 2500;
                        cmd.Parameters.Add("@VisitNo", SqlDbType.VarChar, 20).Value = VisitNo;
                        cmd.Parameters.AddWithValue("UDT_SampleCollectedData", dt);
                        cmd.Parameters.Add("@login_id", SqlDbType.VarChar, 10).Value = login_id;
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
            return processInfo;
        }

        public string Lab_SampleDispatchAndReceive(List<LabSampleInfo> objBO)
        {
            string processInfo = string.Empty;
            string deliveryBoyid = string.Empty;
            string LabCode = string.Empty;
            string hosp_id = string.Empty;
            string login_id = string.Empty;
            string Logic = string.Empty;
            if (objBO.Count > 0)
            {
                DataTable dt = new DataTable();
                dt.Columns.Add("AutoTestId", typeof(int));
                dt.Columns.Add("VisitNo", typeof(string));
                dt.Columns.Add("testcode", typeof(string));
                dt.Columns.Add("LabCode", typeof(string));
                dt.Columns.Add("DispatchLab", typeof(string));
                dt.Columns.Add("DispatchNo", typeof(string));
                dt.Columns.Add("BarcodeNo", typeof(string));
                dt.Columns.Add("login_id", typeof(string));
                foreach (LabSampleInfo obj in objBO)
                {
                    LabCode = obj.LabCode;
                    deliveryBoyid = obj.deliveryBoyid;
                    hosp_id = obj.hosp_id;
                    login_id = obj.login_id;
                    Logic = obj.Logic;
                    DataRow dr = dt.NewRow();
                    dr["AutoTestId"] = obj.AutoTestId;
                    dr["VisitNo"] = obj.VisitNo;
                    dr["testcode"] = obj.testcode;
                    dr["LabCode"] = obj.LabCode;
                    dr["DispatchLab"] = obj.DispatchLab;
                    dr["DispatchNo"] = obj.DispatchNo;
                    dr["BarcodeNo"] = obj.BarcodeNo;
                    dr["login_id"] = obj.login_id;
                    dt.Rows.Add(dr);
                }
                using (SqlConnection con = new SqlConnection(GlobalConfig.ConStr_Hospital))
                {
                    using (SqlCommand cmd = new SqlCommand("pLab_SampleDispatchAndReceive", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandTimeout = 2500;
                        cmd.Parameters.Add("@LabCode", SqlDbType.VarChar, 10).Value = LabCode;
                        cmd.Parameters.Add("@deliveryBoyid", SqlDbType.VarChar, 20).Value = deliveryBoyid;
                        cmd.Parameters.AddWithValue("UDT_SampleDataInputs", dt);
                        cmd.Parameters.Add("@loginId", SqlDbType.VarChar, 10).Value = login_id;
                        cmd.Parameters.Add("@Logic", SqlDbType.VarChar, 50).Value = Logic;
                        cmd.Parameters.Add("@result", SqlDbType.VarChar, 100).Value = "";
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
            return processInfo;
        }


        public dataSet SampleLabReceivingQueries(SampleDispatchInfo objBO)
        {
            dataSet dsObj = new dataSet();
            using (SqlConnection con = new SqlConnection(GlobalConfig.ConStr_Hospital))
            {
                using (SqlCommand cmd = new SqlCommand("pLab_SampleLabReceivingQueries", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandTimeout = 2500;
                    cmd.Parameters.Add("@hosp_id", SqlDbType.VarChar, 10).Value = objBO.hosp_id;
                    cmd.Parameters.Add("@VisitNo", SqlDbType.VarChar, 20).Value = objBO.VisitNo;
                    cmd.Parameters.Add("@BarcodeNo", SqlDbType.VarChar, 10).Value = objBO.BarcodeNo;
                    cmd.Parameters.Add("@SubCatId", SqlDbType.VarChar, 10).Value = objBO.SubCatId;
                    cmd.Parameters.Add("@DispatchNo", SqlDbType.VarChar, 20).Value = objBO.DispatchNo;
                    cmd.Parameters.Add("@TestCode", SqlDbType.VarChar, 10).Value = objBO.TestCode;
                    cmd.Parameters.Add("@from", SqlDbType.DateTime, 10).Value = objBO.from;
                    cmd.Parameters.Add("@to", SqlDbType.DateTime, 10).Value = objBO.to;
                    cmd.Parameters.Add("@Prm1", SqlDbType.VarChar, 50).Value = objBO.Prm1;
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

        public string LabSampleReceiveInLab(List<LabReceiveInfo> objBO)
        {
            string processInfo = string.Empty;
            string deliveryBoyid = string.Empty;
            string LabCode = string.Empty;
            string BarcodeNo = string.Empty;
            string hosp_id = string.Empty;
            string login_id = string.Empty;
            string Logic = string.Empty;
            if (objBO.Count > 0)
            {
                DataTable dt = new DataTable();
                dt.Columns.Add("AutoTestId", typeof(int));
                dt.Columns.Add("VisitNo", typeof(string));
                dt.Columns.Add("testcode", typeof(string));
                dt.Columns.Add("LabCode", typeof(string));
                dt.Columns.Add("DispatchLab", typeof(string));
                dt.Columns.Add("DispatchNo", typeof(string));
                dt.Columns.Add("BarcodeNo", typeof(string));
                dt.Columns.Add("login_id", typeof(string));
                foreach (LabReceiveInfo obj in objBO)
                {
                    LabCode = obj.LabCode;
                    BarcodeNo = obj.BarcodeNo;
                    deliveryBoyid = obj.deliveryBoyid;
                    hosp_id = obj.hosp_id;
                    login_id = obj.login_id;
                    Logic = obj.Logic;
                    DataRow dr = dt.NewRow();
                    dr["AutoTestId"] = obj.AutoTestId;
                    dr["VisitNo"] = obj.VisitNo;
                    dr["testcode"] = obj.testcode;
                    dr["LabCode"] = obj.LabCode;
                    dr["DispatchLab"] = obj.DispatchLab;
                    dr["DispatchNo"] = obj.DispatchNo;
                    dr["BarcodeNo"] = obj.BarcodeNo;
                    dr["login_id"] = obj.login_id;
                    dt.Rows.Add(dr);
                }
                using (SqlConnection con = new SqlConnection(GlobalConfig.ConStr_Hospital))
                {
                    using (SqlCommand cmd = new SqlCommand("pLab_SampleReceiveInLab", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandTimeout = 2500;
                        cmd.Parameters.Add("@LabCode", SqlDbType.VarChar, 10).Value = LabCode;
                        cmd.Parameters.AddWithValue("UDT_SampleDataInputs", dt);
                        cmd.Parameters.Add("@deliveryBoyid", SqlDbType.VarChar, 20).Value = deliveryBoyid;
                        cmd.Parameters.Add("@BarcodeNo", SqlDbType.VarChar, 20).Value = BarcodeNo;
                        cmd.Parameters.Add("@loginId", SqlDbType.VarChar, 10).Value = login_id;
                        cmd.Parameters.Add("@Logic", SqlDbType.VarChar, 50).Value = Logic;
                        cmd.Parameters.Add("@result", SqlDbType.VarChar, 100).Value = "";
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
            return processInfo;
        }

    }
}