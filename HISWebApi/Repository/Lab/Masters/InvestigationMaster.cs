using HISWebApi.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace HISWebApi.Repository.Lab
{
    public class InvestigationMaster
    {
        public string LabInsertUpdateOutSource(LabOutSource objBO)
        {
            dataSet dsObj = new dataSet();
            string processInfo = string.Empty;
            using (SqlConnection con = new SqlConnection(GlobalConfig.ConStr_Hospital))
            {
                using (SqlCommand cmd = new SqlCommand("pm_LabInsertUpdateOutSource", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandTimeout = 2500;
                    cmd.Parameters.Add("@LabCode", SqlDbType.VarChar, 5).Value = objBO.LabCode;
                    cmd.Parameters.Add("@LabType", SqlDbType.VarChar, 15).Value = objBO.LabType;
                    cmd.Parameters.Add("@LabName", SqlDbType.VarChar, 50).Value = objBO.LabName;
                    cmd.Parameters.Add("@address", SqlDbType.VarChar, 80).Value = objBO.address;
                    cmd.Parameters.Add("@cstate", SqlDbType.VarChar, 50).Value = objBO.cstate;
                    cmd.Parameters.Add("@city", SqlDbType.VarChar, 50).Value = objBO.city;
                    cmd.Parameters.Add("@contact_no", SqlDbType.VarChar, 50).Value = objBO.contact_no;
                    cmd.Parameters.Add("@cin", SqlDbType.VarChar, 50).Value = objBO.cin;
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

        public dataSet LabInvestigationMasterQueries(LabIvestigation objBO)
        {
            dataSet dsObj = new dataSet();
            using (SqlConnection con = new SqlConnection(GlobalConfig.ConStr_Hospital))
            {
                using (SqlCommand cmd = new SqlCommand("pm_LabInvestigationQueries", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandTimeout = 2500;
                    cmd.Parameters.Add("@hosp_id", SqlDbType.VarChar, 10).Value = objBO.hosp_id;
                    cmd.Parameters.Add("@CatId", SqlDbType.VarChar, 10).Value = objBO.catid;
                    cmd.Parameters.Add("@SubCatId", SqlDbType.VarChar, 12).Value = objBO.subcateid;
                    cmd.Parameters.Add("@TestCode", SqlDbType.VarChar, 12).Value = objBO.investcode;                    
                    cmd.Parameters.Add("@prm_1", SqlDbType.VarChar, 100).Value = objBO.prm_1;
                    cmd.Parameters.Add("@prm_2", SqlDbType.VarChar, 100).Value = objBO.prm_2;
                    cmd.Parameters.Add("@Logic", SqlDbType.VarChar, 50).Value = objBO.Logic;
                    cmd.Parameters.Add("@login_id", SqlDbType.VarChar, 10).Value = objBO.login_id;
                    cmd.Parameters.Add("@warehouseCartId", SqlDbType.VarChar, 7).Value = objBO.warehouseCartId;
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

        public string InvestigationInsertUpdate(LabIvestigation objBO)
        {
            dataSet dsObj = new dataSet();
            string processInfo = string.Empty;
            using (SqlConnection con = new SqlConnection(GlobalConfig.ConStr_Hospital))
            {
                using (SqlCommand cmd = new SqlCommand("pm_LabInvestigation_InsertUpdate", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandTimeout = 2500;
                    cmd.Parameters.Add("@CatId", SqlDbType.VarChar, 10).Value = objBO.catid;
                    cmd.Parameters.Add("@SubCatId", SqlDbType.VarChar, 10).Value = objBO.subcateid;
                    cmd.Parameters.Add("@testcode", SqlDbType.VarChar, 10).Value = objBO.investcode;
                    cmd.Parameters.Add("@testType", SqlDbType.VarChar, 20).Value = objBO.investtype;
                    cmd.Parameters.Add("@TestName", SqlDbType.VarChar, 150).Value = objBO.investname;                    
                    cmd.Parameters.Add("@nameonreport", SqlDbType.VarChar, 100).Value = objBO.investreportname;
                    cmd.Parameters.Add("@bookFor", SqlDbType.VarChar, 10).Value = objBO.bookfor;
                    cmd.Parameters.Add("@sample_Links", SqlDbType.VarChar, 5000).Value = objBO.samplelinkdata;
                    cmd.Parameters.Add("@default_sample", SqlDbType.VarChar, 255).Value = objBO.defaultsample;                                     
                    cmd.Parameters.Add("@sample_option", SqlDbType.VarChar, 50).Value = objBO.sampleoption;
                    cmd.Parameters.Add("@sample_container", SqlDbType.VarChar, 100).Value = objBO.samplecontainer;
                    cmd.Parameters.Add("@sample_temp_required", SqlDbType.VarChar, 50).Value = objBO.sampletemp;
                    cmd.Parameters.Add("@sample_qty", SqlDbType.VarChar, 30).Value = objBO.sampleqty;
                    cmd.Parameters.Add("@sample_remark", SqlDbType.VarChar, 300).Value = objBO.sampleremark;                    
                    cmd.Parameters.Add("@max_time", SqlDbType.Int).Value = objBO.maxtime;
                    cmd.Parameters.Add("@about_test", SqlDbType.VarChar, 5000).Value = objBO.abouttest;
                    cmd.Parameters.Add("@test_preperation", SqlDbType.VarChar, 1000).Value = objBO.testprep;                    
                    cmd.Parameters.Add("@PrintSampleInReport", SqlDbType.Char, 1).Value = objBO.printsampleinreport;
                    cmd.Parameters.Add("@out_source", SqlDbType.Char, 1).Value = objBO.isoutsource;
                    cmd.Parameters.Add("@promo_flag", SqlDbType.Char, 1).Value = objBO.promotionflag;
                    cmd.Parameters.Add("@report_flag", SqlDbType.Char, 1).Value = objBO.reportflag;
                    cmd.Parameters.Add("@web_flag", SqlDbType.Char, 1).Value = objBO.displayinweb;
                    cmd.Parameters.Add("@consent_flag", SqlDbType.Char, 1).Value = objBO.consentflag;
                    cmd.Parameters.Add("@IgnoreTAT", SqlDbType.Char, 1).Value = objBO.ignortat;
                    cmd.Parameters.Add("@InOutRequired", SqlDbType.Char, 1).Value = objBO.inoutrequired;
                    cmd.Parameters.Add("@Increment_flag", SqlDbType.Char, 1).Value = objBO.incrementflag;                    
                    cmd.Parameters.Add("@r_type", SqlDbType.VarChar, 10).Value = objBO.repotytype;                    
                    cmd.Parameters.Add("@test_perfcost", SqlDbType.Decimal).Value = objBO.cost;
                    cmd.Parameters.Add("@offer_rate", SqlDbType.Decimal).Value = objBO.rate;
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

        public string InsertUpdateTestInterpretation(LabIvestigation objBO)
        {
            dataSet dsObj = new dataSet();
            string processInfo = string.Empty;
            using (SqlConnection con = new SqlConnection(GlobalConfig.ConStr_Hospital))
            {
                using (SqlCommand cmd = new SqlCommand("pm_LabInterpretation_InsertUpdate", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandTimeout = 2500;                    
                    cmd.Parameters.Add("@testcode", SqlDbType.VarChar, 10).Value = objBO.investcode;                   
                    cmd.Parameters.Add("@text", SqlDbType.VarChar).Value = objBO.text;                    
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

        public string DeleteTestInterpretation(LabIvestigation objBO)
        {
            dataSet dsObj = new dataSet();
            string processInfo = string.Empty;
            using (SqlConnection con = new SqlConnection(GlobalConfig.ConStr_Hospital))
            {
                using (SqlCommand cmd = new SqlCommand("pm_LabInterpretation_InsertUpdate", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandTimeout = 2500;
                    cmd.Parameters.Add("@testcode", SqlDbType.VarChar, 10).Value = objBO.investcode;
                    cmd.Parameters.Add("@text", SqlDbType.VarChar).Value = "-";
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

        public dataSet LabOutSourceQueries(LabOutSourceInfo objBO)
        {
            dataSet dsObj = new dataSet();
            using (SqlConnection con = new SqlConnection(GlobalConfig.ConStr_Hospital))
            {
                using (SqlCommand cmd = new SqlCommand("pm_LabOutSourceQuerries", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandTimeout = 2500;
                    cmd.Parameters.Add("@hosp_id", SqlDbType.VarChar, 15).Value = objBO.hosp_id;
                    cmd.Parameters.Add("@catId", SqlDbType.VarChar, 15).Value = objBO.catId;
                    cmd.Parameters.Add("@subcatId", SqlDbType.VarChar, 15).Value = objBO.subcatId;
                    cmd.Parameters.Add("@testCode", SqlDbType.VarChar, 20).Value = objBO.testCode;
                    cmd.Parameters.Add("@BarcodeNo", SqlDbType.VarChar, 20).Value = objBO.BarcodeNo;
                    cmd.Parameters.Add("@prm1", SqlDbType.VarChar, 50).Value = objBO.prm1;
                    cmd.Parameters.Add("@prm2", SqlDbType.VarChar, 50).Value = objBO.prm2;
                    cmd.Parameters.Add("@from", SqlDbType.Date).Value = objBO.from;
                    cmd.Parameters.Add("@to", SqlDbType.Date).Value = objBO.to;
                    cmd.Parameters.Add("@logic", SqlDbType.VarChar, 50).Value = objBO.logic;
                    cmd.Parameters.Add("@login_id", SqlDbType.VarChar, 15).Value = objBO.login_id;
                    cmd.Parameters.Add("@warehouseCartId", SqlDbType.VarChar, 20).Value = objBO.warehouseCartId;
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

        public string LabSetProcTimeAndPerformingLab(List<LabInputInfo> objBO)
        {
            string processInfo = string.Empty;
            string hosp_id = string.Empty;
            string login_id = string.Empty;
            string Logic = string.Empty;
            if (objBO.Count > 0)
            {
                DataTable dt = new DataTable();
                dt.Columns.Add("LabCode", typeof(string));
                dt.Columns.Add("ToLabCode", typeof(string));
                dt.Columns.Add("TestCode", typeof(string));
                dt.Columns.Add("SampleCode", typeof(string));
                dt.Columns.Add("ProcessingTime", typeof(int));
                foreach (LabInputInfo obj in objBO)
                {
                    hosp_id = obj.hosp_id;
                    login_id = obj.login_id;
                    Logic = obj.Logic;
                    DataRow dr = dt.NewRow();
                    dr["LabCode"] =obj.LabCode;
                    dr["ToLabCode"] = obj.ToLabCode;
                    dr["TestCode"] = obj.TestCode;
                    dr["SampleCode"] = obj.SampleCode;
                    dr["ProcessingTime"] = obj.ProcessingTime;
                    dt.Rows.Add(dr);
                }
                using (SqlConnection con = new SqlConnection(GlobalConfig.ConStr_Hospital))
                {
                    using (SqlCommand cmd = new SqlCommand("pm_LabSetProcTimeAndPerformingLab", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandTimeout = 2500;
                        cmd.Parameters.Add("@hosp_id", SqlDbType.VarChar, 10).Value = hosp_id;
                        cmd.Parameters.AddWithValue("udt_LabInputs", dt);
                        cmd.Parameters.Add("@login_id", SqlDbType.VarChar, 10).Value = login_id;
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