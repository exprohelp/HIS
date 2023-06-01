using HISWebApi.Models;
using System;
using System.Data;
using System.Data.SqlClient;
using static HISWebApi.Models.BloodBank;

namespace HISWebApi.Repository.BloodBank
{
    public class DonorInfo
    {
        public dataSet BB_SelectQueries(SelectQueriesInfo objBO)
        {
            dataSet dsObj = new dataSet();
            using (SqlConnection con = new SqlConnection(GlobalConfig.ConStr_Hospital))
            {
                using (SqlCommand cmd = new SqlCommand("BB_SelectQueries", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandTimeout = 2500;
                    cmd.Parameters.Add("@hosp_id", SqlDbType.VarChar, 50).Value = objBO.hosp_id;
                    cmd.Parameters.Add("@donor_id", SqlDbType.VarChar, 50).Value = objBO.DonorId;
                    cmd.Parameters.Add("@visit_id", SqlDbType.VarChar, 50).Value = objBO.VisitId;
                    cmd.Parameters.Add("@from", SqlDbType.Date).Value = objBO.from;
                    cmd.Parameters.Add("@to", SqlDbType.Date).Value = objBO.to;
                    cmd.Parameters.Add("@IndentNo", SqlDbType.VarChar, 50).Value = objBO.IndentNo;
                    cmd.Parameters.Add("@prm_1", SqlDbType.VarChar, 50).Value = objBO.Prm1;
                    cmd.Parameters.Add("@prm_2", SqlDbType.VarChar, 50).Value = objBO.Prm2;
                    cmd.Parameters.Add("@loginId", SqlDbType.VarChar, 50).Value = objBO.login_id;
                    cmd.Parameters.Add("@logic", SqlDbType.VarChar, 50).Value = objBO.Logic;
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
        public dataSet BB_SDPQueries(SelectQueriesInfo objBO)
        {
            dataSet dsObj = new dataSet();
            using (SqlConnection con = new SqlConnection(GlobalConfig.ConStr_Hospital))
            {
                using (SqlCommand cmd = new SqlCommand("BB_SDPQueries", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandTimeout = 2500;
                    cmd.Parameters.Add("@hosp_id", SqlDbType.VarChar, 50).Value = objBO.hosp_id;
                    cmd.Parameters.Add("@donor_id", SqlDbType.VarChar, 50).Value = objBO.DonorId;
                    cmd.Parameters.Add("@visit_id", SqlDbType.VarChar, 50).Value = objBO.VisitId;
                    cmd.Parameters.Add("@from", SqlDbType.Date).Value = objBO.from;
                    cmd.Parameters.Add("@to", SqlDbType.Date).Value = objBO.to;
                    cmd.Parameters.Add("@IndentNo", SqlDbType.VarChar, 50).Value = objBO.IndentNo;
                    cmd.Parameters.Add("@prm_1", SqlDbType.VarChar, 50).Value = objBO.Prm1;
                    cmd.Parameters.Add("@prm_2", SqlDbType.VarChar, 50).Value = objBO.Prm2;
                    cmd.Parameters.Add("@loginId", SqlDbType.VarChar, 50).Value = objBO.login_id;
                    cmd.Parameters.Add("@logic", SqlDbType.VarChar, 50).Value = objBO.Logic;
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
        public dataSet BB_StockAndDashBoard_Queries(SelectQueriesInfo objBO)
        {
            dataSet dsObj = new dataSet();
            using (SqlConnection con = new SqlConnection(GlobalConfig.ConStr_Hospital))
            {
                using (SqlCommand cmd = new SqlCommand("BB_StockAndDashBoard_Queries", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandTimeout = 2500;
                    cmd.Parameters.Add("@from", SqlDbType.Date).Value = objBO.from;
                    cmd.Parameters.Add("@to", SqlDbType.Date).Value = objBO.to;
                    cmd.Parameters.Add("@BloodGroup", SqlDbType.VarChar, 10).Value = objBO.BloodGroup;
                    cmd.Parameters.Add("@ExpireInDays", SqlDbType.Int).Value = objBO.ExpireInDays;
                    cmd.Parameters.Add("@Prm1", SqlDbType.VarChar, 50).Value = objBO.Prm1;
                    cmd.Parameters.Add("@Prm2", SqlDbType.VarChar, 50).Value = objBO.Prm2;
                    cmd.Parameters.Add("@loginId", SqlDbType.VarChar, 20).Value = objBO.login_id;
                    cmd.Parameters.Add("@logic", SqlDbType.VarChar, 50).Value = objBO.Logic;
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
        public string BB_InsertModifyDonorInfo(DonorDetail objBO)
        {
            string processInfo = string.Empty;
            dataSet dsObj = new dataSet();
            DataTable dt = new DataTable();
            dt.Columns.Add("QuestId", typeof(string));
            dt.Columns.Add("Answer", typeof(string));
            dt.Columns.Add("Remarks", typeof(string));
            foreach (donorAnswer obj in objBO.objDonorAnswers)
            {
                DataRow dr = dt.NewRow();
                dr["QuestId"] = obj.QuestId; dr["Answer"] = obj.Answer; dr["Remarks"] = obj.Remarks;
                dt.Rows.Add(dr);
            }
            using (SqlConnection con = new SqlConnection(GlobalConfig.ConStr_Hospital))
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand("BB_Insert_Modify_bb_donorInfo", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandTimeout = 2500;
                    cmd.Parameters.Add("@hosp_Id", SqlDbType.VarChar, 10).Value = objBO.Hosp_Id;
                    cmd.Parameters.Add("@donor_id", SqlDbType.VarChar, 50).Value = objBO.Donor_Id;
                    cmd.Parameters.Add("@Title", SqlDbType.VarChar, 10).Value = objBO.Title;
                    cmd.Parameters.Add("@Dfirstname", SqlDbType.VarChar, 50).Value = objBO.Dfirstname;
                    cmd.Parameters.Add("@Dlastname", SqlDbType.VarChar, 50).Value = objBO.Dlastname;
                    cmd.Parameters.Add("@dtBirth", SqlDbType.VarChar, 50).Value = objBO.Dtbirth;
                    cmd.Parameters.Add("@DOB", SqlDbType.DateTime).Value = objBO.Dob;
                    cmd.Parameters.Add("@Gender", SqlDbType.VarChar, 10).Value = objBO.Gender;
                    cmd.Parameters.Add("@relative_Name", SqlDbType.VarChar, 100).Value = objBO.Relative_Name;
                    cmd.Parameters.Add("@Relation", SqlDbType.VarChar, 20).Value = objBO.Relation;
                    cmd.Parameters.Add("@BloodGroupId", SqlDbType.VarChar, 3).Value = objBO.BloodGroupId;
                    cmd.Parameters.Add("@donor_Address", SqlDbType.VarChar, 500).Value = objBO.Donor_Address;
                    cmd.Parameters.Add("@Country", SqlDbType.VarChar, 100).Value = objBO.Country;
                    cmd.Parameters.Add("@City", SqlDbType.VarChar, 100).Value = objBO.City;
                    cmd.Parameters.Add("@PinCode", SqlDbType.VarChar, 100).Value = objBO.Pincode;
                    cmd.Parameters.Add("@contactNo", SqlDbType.VarChar, 100).Value = objBO.Contactno;
                    cmd.Parameters.Add("@aadharNo", SqlDbType.VarChar, 20).Value = objBO.Aadharno;
                    cmd.Parameters.Add("@Email", SqlDbType.VarChar, 200).Value = objBO.Email;
                    cmd.Parameters.Add("@BloodDonate", SqlDbType.VarChar, 20).Value = objBO.BloodDonate;
                    cmd.Parameters.Add("@EntryBy", SqlDbType.VarChar, 100).Value = objBO.Entryby;
                    cmd.Parameters.Add("@isRevisit", SqlDbType.Int).Value = objBO.Isrevisit;
                    cmd.Parameters.Add("@Blood_Pressure", SqlDbType.VarChar, 10).Value = objBO.Blood_Pressure;
                    cmd.Parameters.Add("@Weight", SqlDbType.Int).Value = objBO.Weight;
                    cmd.Parameters.Add("@Pulse", SqlDbType.Int).Value = objBO.Pulse;
                    cmd.Parameters.Add("@GPE", SqlDbType.VarChar, 50).Value = objBO.Gpe;
                    cmd.Parameters.Add("@Height", SqlDbType.Int).Value = objBO.Height;
                    cmd.Parameters.Add("@Temprature", SqlDbType.Decimal).Value = objBO.Temprature;
                    cmd.Parameters.Add("@Hb", SqlDbType.VarChar, 20).Value = objBO.Hb;
                    cmd.Parameters.Add("@isFit", SqlDbType.Char, 1).Value = objBO.isFit;
                    cmd.Parameters.Add("@DonationType", SqlDbType.VarChar, 50).Value = objBO.DonationType;
                    cmd.Parameters.Add("@DonorType", SqlDbType.VarChar, 20).Value = objBO.DonorType;
                    cmd.Parameters.Add("@uhid", SqlDbType.VarChar, 20).Value = objBO.uhid;
                    cmd.Parameters.Add("@platelet", SqlDbType.VarChar, 10).Value = objBO.Platelet;
                    cmd.Parameters.Add("@photo_path", SqlDbType.VarChar, 200).Value = objBO.photo_path;
                    cmd.Parameters.Add("@Remark", SqlDbType.VarChar, 200).Value = objBO.Remark;
                    cmd.Parameters.Add("@Logic", SqlDbType.VarChar, 20).Value = objBO.Logic;
                    cmd.Parameters.AddWithValue("donorAnswers", dt);
                    cmd.Parameters.Add("@result", SqlDbType.VarChar, 50).Value = "-";
                    cmd.Parameters["@result"].Direction = ParameterDirection.InputOutput;
                    try
                    {
                        cmd.ExecuteNonQuery();
                        processInfo = (string)cmd.Parameters["@result"].Value.ToString();

                    }
                    catch (SqlException sqlEx)
                    {
                        dsObj.ResultSet = null;
                        dsObj.Msg = sqlEx.Message;
                    }
                    finally { con.Close(); }
                    return processInfo;
                }
            }
        }
        public string BB_InsertModifyCollectionInfo(BloodCollection obj)
        {
            string processInfo = string.Empty;

            using (SqlConnection con = new SqlConnection(GlobalConfig.ConStr_Hospital))
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand("BB_Insert_Modify_bb_collection_Info", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandTimeout = 2500;
                    cmd.Parameters.Add("@hosp_id", SqlDbType.VarChar, 10).Value = obj.Hosp_Id;
                    cmd.Parameters.Add("@donor_Id", SqlDbType.VarChar, 50).Value = obj.Donor_id;
                    cmd.Parameters.Add("@Visit_ID", SqlDbType.VarChar, 50).Value = obj.Visit_ID;
                    cmd.Parameters.Add("@BloodCollection_Id", SqlDbType.VarChar, 50).Value = obj.BloodCollection_Id;
                    cmd.Parameters.Add("@BBTubeNo", SqlDbType.VarChar, 50).Value = obj.TubeNo;
                    cmd.Parameters.Add("@BagType", SqlDbType.VarChar, 50).Value = obj.BagType;
                    cmd.Parameters.Add("@Volume", SqlDbType.VarChar, 50).Value = obj.Volume;
                    cmd.Parameters.Add("@Phlebotomy", SqlDbType.VarChar, 10).Value = obj.Phlebotomy;
                    cmd.Parameters.Add("@CollectionRemark", SqlDbType.VarChar, 100).Value = obj.CollectionRemark;
                    cmd.Parameters.Add("@CollectedBy", SqlDbType.VarChar, 50).Value = obj.CollectionBy;
                    cmd.Parameters.Add("@CollectedDate", SqlDbType.DateTime).Value = DateTime.Now;
                    cmd.Parameters.Add("@Isdonated", SqlDbType.Char, 1).Value = obj.Isdonated;
                    cmd.Parameters.Add("@IsShocked", SqlDbType.Int).Value = obj.IsShocked;
                    cmd.Parameters.Add("@login_id", SqlDbType.VarChar, 20).Value = obj.login_id;
                    cmd.Parameters.Add("@Logic", SqlDbType.VarChar, 20).Value = obj.Logic;
                    cmd.Parameters.Add("@result", SqlDbType.VarChar, 50).Value = "-";
                    cmd.Parameters["@result"].Direction = ParameterDirection.InputOutput;
                    try
                    {
                        cmd.ExecuteNonQuery();
                        processInfo = (string)cmd.Parameters["@result"].Value.ToString();

                    }
                    catch (SqlException sqlEx)
                    {
                        processInfo = sqlEx.Message;
                    }
                    finally { con.Close(); }
                    return processInfo;
                }
            }
        }
        public string BB_InsertModifyGroupingInfo(BloodGrouping obj)
        {
            string processInfo = string.Empty;

            using (SqlConnection con = new SqlConnection(GlobalConfig.ConStr_Hospital))
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand("BB_Insert_Modify_bb_grouping_Info", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandTimeout = 2500;
                    cmd.Parameters.Add("@hosp_id", SqlDbType.VarChar, 50).Value = obj.hosp_id;
                    cmd.Parameters.Add("@donor_id", SqlDbType.VarChar, 50).Value = obj.donor_id;
                    cmd.Parameters.Add("@visit_id", SqlDbType.VarChar, 50).Value = obj.visit_id;
                    cmd.Parameters.Add("@BloodCollection_Id", SqlDbType.VarChar, 50).Value = obj.BloodCollection_Id;
                    cmd.Parameters.Add("@ScreenedBG", SqlDbType.VarChar, 50).Value = obj.ScreenedBG;
                    cmd.Parameters.Add("@AntiA", SqlDbType.VarChar, 10).Value = obj.AntiA;
                    cmd.Parameters.Add("@AntiB", SqlDbType.VarChar, 10).Value = obj.AntiB;
                    cmd.Parameters.Add("@AntiAB", SqlDbType.VarChar, 10).Value = obj.AntiAB;
                    cmd.Parameters.Add("@RH", SqlDbType.VarChar, 10).Value = obj.RH;
                    cmd.Parameters.Add("@BloodTested", SqlDbType.VarChar, 10).Value = obj.BloodTested;
                    cmd.Parameters.Add("@Remark", SqlDbType.VarChar, 200).Value = obj.Remark;
                    cmd.Parameters.Add("@CreatedBy", SqlDbType.VarChar, 50).Value = obj.CreatedBy;
                    cmd.Parameters.Add("@ACell", SqlDbType.VarChar, 50).Value = obj.ACell;
                    cmd.Parameters.Add("@BCell", SqlDbType.VarChar, 50).Value = obj.BCell;
                    cmd.Parameters.Add("@OCell", SqlDbType.VarChar, 50).Value = obj.OCell;
                    cmd.Parameters.Add("@BloodGroupAlloted", SqlDbType.VarChar, 50).Value = obj.BloodGroupAlloted;
                    cmd.Parameters.Add("@IsMotherSample", SqlDbType.Int).Value = obj.IsMotherSample;
                    cmd.Parameters.Add("@IsMType", SqlDbType.Int).Value = obj.IsMType;
                    cmd.Parameters.Add("@IsMTested", SqlDbType.Int).Value = obj.IsMTested;
                    cmd.Parameters.Add("@LedgerType", SqlDbType.Int).Value = obj.LedgerType;
                    cmd.Parameters.Add("@BGTested", SqlDbType.VarChar, 50).Value = obj.BGTested;
                    cmd.Parameters.Add("@MotherBG", SqlDbType.VarChar, 50).Value = obj.MotherBG;
                    cmd.Parameters.Add("@Logic", SqlDbType.VarChar, 20).Value = obj.Logic;
                    cmd.Parameters.Add("@result", SqlDbType.VarChar, 50).Value = "-";
                    cmd.Parameters["@result"].Direction = ParameterDirection.InputOutput;
                    try
                    {
                        cmd.ExecuteNonQuery();
                        processInfo = (string)cmd.Parameters["@result"].Value.ToString();

                    }
                    catch (SqlException sqlEx)
                    {
                        processInfo = sqlEx.Message;
                    }
                    finally { con.Close(); }
                    return processInfo;
                }
            }
        }
        public string BB_InsertModifyBloodScreening(BloodScreeningDetail objBO)
        {
            string processInfo = string.Empty;
            if (objBO.ScreeningValue.Count > 0)
            {
                DataTable dt = new DataTable();
                dt.Columns.Add("BloodCollection_Id", typeof(string));
                dt.Columns.Add("TestId", typeof(string));
                dt.Columns.Add("Method", typeof(string));
                dt.Columns.Add("resultValue", typeof(decimal));
                dt.Columns.Add("Result", typeof(string));
                foreach (ScreeningValue objValue in objBO.ScreeningValue)
                {
                    DataRow dr = dt.NewRow();
                    dr["BloodCollection_Id"] = objValue.BloodCollection_Id;
                    dr["TestId"] = objValue.TestId;
                    dr["Method"] = objValue.Method;
                    dr["resultValue"] = objValue.resultValue;
                    dr["Result"] = objValue.Result;
                    dt.Rows.Add(dr);
                }
                using (SqlConnection con = new SqlConnection(GlobalConfig.ConStr_Hospital))
                {
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand("BB_Insert_Modify_bb_blood_screening", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandTimeout = 2500;
                        cmd.Parameters.Add("@hosp_id", SqlDbType.VarChar, 50).Value = objBO.hosp_id;
                        cmd.Parameters.Add("@donor_id", SqlDbType.VarChar, 50).Value = objBO.donor_id;
                        cmd.Parameters.Add("@visit_id", SqlDbType.VarChar, 50).Value = objBO.visit_id;
                        cmd.Parameters.Add("@screening_id", SqlDbType.VarChar, 50).Value = objBO.screening_id;
                        cmd.Parameters.AddWithValue("UDT_bb_BloodScreening", dt);
                        cmd.Parameters.Add("@login_id", SqlDbType.VarChar, 20).Value = objBO.login_id;
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
                    }
                }
            }
            return processInfo;
        }
        public string BB_InsertModifyComponentCreation(ComponentDetail objBO)
        {
            string processInfo = string.Empty;
            if (objBO.componentInfo.Count > 0)
            {
                DataTable dt = new DataTable();
                dt.Columns.Add("ComponentID", typeof(string));
                dt.Columns.Add("ComponentName", typeof(string));
                dt.Columns.Add("Qty", typeof(int));
                dt.Columns.Add("PackSize", typeof(string));
                dt.Columns.Add("IsComponent", typeof(int));
                dt.Columns.Add("EntryDate", typeof(DateTime));
                dt.Columns.Add("ExpiryDate", typeof(DateTime));
                foreach (ComponentInfo objValue in objBO.componentInfo)
                {
                    DataRow dr = dt.NewRow();
                    dr["ComponentID"] = objValue.ComponentID;
                    dr["ComponentName"] = objValue.ComponentName;
                    dr["Qty"] = objValue.Qty;
                    dr["PackSize"] = objValue.PackSize;
                    dr["IsComponent"] = objValue.IsComponent;
                    dr["EntryDate"] = objValue.EntryDate;
                    dr["ExpiryDate"] = objValue.ExpiryDate;
                    dt.Rows.Add(dr);
                }
                using (SqlConnection con = new SqlConnection(GlobalConfig.ConStr_Hospital))
                {
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand("BB_Insert_Modify_bb_component_creation", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandTimeout = 2500;
                        cmd.Parameters.Add("@hosp_id", SqlDbType.VarChar, 50).Value = objBO.hosp_id;
                        cmd.Parameters.Add("@donor_id", SqlDbType.VarChar, 50).Value = objBO.donor_id;
                        cmd.Parameters.Add("@visit_id", SqlDbType.VarChar, 50).Value = objBO.visit_id;
                        cmd.Parameters.Add("@BloodCollection_Id", SqlDbType.VarChar, 50).Value = objBO.BloodCollection_Id;
                        cmd.Parameters.Add("@BagType", SqlDbType.VarChar, 50).Value = objBO.BagType;
                        cmd.Parameters.Add("@BBTubeNo", SqlDbType.VarChar, 50).Value = objBO.BBTubeNo;
                        cmd.Parameters.Add("@BloodGroup", SqlDbType.VarChar, 50).Value = objBO.BloodGroup;
                        cmd.Parameters.AddWithValue("udt_BB_ComponentInfo", dt);
                        cmd.Parameters.Add("@login_id", SqlDbType.VarChar, 20).Value = objBO.login_id;
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
                    }
                }
            }
            return processInfo;
        }
        public string BB_Insert_ModifyBloodIssue(BloodIssueInfo obj)
        {
            string processInfo = string.Empty;

            using (SqlConnection con = new SqlConnection(GlobalConfig.ConStr_Hospital))
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand("BB_Insert_Modify_bb_Blood_Issue", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandTimeout = 2500;
                    cmd.Parameters.Add("@AutoId", SqlDbType.Int).Value = obj.AutoId;
                    cmd.Parameters.Add("@hosp_id", SqlDbType.VarChar, 20).Value = obj.hosp_id;
                    cmd.Parameters.Add("@Stock_ID", SqlDbType.VarChar, 20).Value = obj.Stock_ID;
                    cmd.Parameters.Add("@ItemID", SqlDbType.VarChar, 20).Value = obj.ItemID;
                    cmd.Parameters.Add("@IndentNo", SqlDbType.VarChar, 20).Value = obj.IndentNo;
                    cmd.Parameters.Add("@IPOPNo", SqlDbType.VarChar, 20).Value = obj.IPOPNo;
                    cmd.Parameters.Add("@IPOPType", SqlDbType.VarChar, 50).Value = obj.IPOPType;
                    cmd.Parameters.Add("@Quantity", SqlDbType.Int).Value = obj.Quantity;
                    cmd.Parameters.Add("@Prm1", SqlDbType.VarChar, 100).Value = obj.Prm1;
                    cmd.Parameters.Add("@Prm2", SqlDbType.VarChar, 100).Value = obj.Prm2;
                    cmd.Parameters.Add("@login_id", SqlDbType.VarChar, 10).Value = obj.login_id;
                    cmd.Parameters.Add("@Logic", SqlDbType.VarChar, 50).Value = obj.Logic;
                    cmd.Parameters.Add("@result", SqlDbType.VarChar, 50).Value = "-";
                    cmd.Parameters["@result"].Direction = ParameterDirection.InputOutput;
                    try
                    {
                        cmd.ExecuteNonQuery();
                        processInfo = (string)cmd.Parameters["@result"].Value.ToString();

                    }
                    catch (SqlException sqlEx)
                    {
                        processInfo = sqlEx.Message;
                    }
                    finally { con.Close(); }
                    return processInfo;
                }
            }
        }
        public string BB_ApproveDonation(ApproveDonationInfo obj)
        {
            string processInfo = string.Empty;
            using (SqlConnection con = new SqlConnection(GlobalConfig.ConStr_Hospital))
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand("BB_ApproveDonation", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandTimeout = 2500;
                    cmd.Parameters.Add("@hosp_id", SqlDbType.VarChar, 50).Value = obj.hosp_id;
                    cmd.Parameters.Add("@donor_id", SqlDbType.VarChar, 50).Value = obj.donor_id;
                    cmd.Parameters.Add("@Stock_Id", SqlDbType.VarChar, 50).Value = obj.Stock_Id;
                    cmd.Parameters.Add("@visit_id", SqlDbType.VarChar, 50).Value = obj.visit_id;
                    cmd.Parameters.Add("@BloodCollectionId", SqlDbType.VarChar, 50).Value = obj.BloodCollectionId;
                    cmd.Parameters.Add("@Prm1", SqlDbType.VarChar, 200).Value = obj.Prm1;
                    cmd.Parameters.Add("@Prm2", SqlDbType.VarChar, 200).Value = obj.Prm2;
                    cmd.Parameters.Add("@login_id", SqlDbType.VarChar, 20).Value = obj.login_id;
                    cmd.Parameters.Add("@Logic", SqlDbType.VarChar, 50).Value = obj.Logic;
                    cmd.Parameters.Add("@result", SqlDbType.VarChar, 50).Value = "-";
                    cmd.Parameters["@result"].Direction = ParameterDirection.InputOutput;
                    try
                    {
                        cmd.ExecuteNonQuery();
                        processInfo = (string)cmd.Parameters["@result"].Value.ToString();

                    }
                    catch (SqlException sqlEx)
                    {
                        processInfo = sqlEx.Message;
                    }
                    finally { con.Close(); }
                    return processInfo;
                }
            }
        }
    }
}