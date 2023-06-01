using HISWebApi.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace HISWebApi.Repository.Dietician
{
    public class Diet
    {
        public dataSet DietmasterQueries(DietMasterBO objBO)
        {
            dataSet dsObj = new dataSet();
            using (SqlConnection con = new SqlConnection(GlobalConfig.ConStr_Hospital))
            {
                using (SqlCommand cmd = new SqlCommand("pdiet_MasterQueries", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandTimeout = 2500;
                    cmd.Parameters.Add("@AutoId", SqlDbType.VarChar, 10).Value = objBO.Autoid;
                    cmd.Parameters.Add("@DietTypeId", SqlDbType.VarChar, 10).Value = objBO.DietTypeId;
                    cmd.Parameters.Add("@DietId", SqlDbType.VarChar, 10).Value = objBO.DietId;
                    cmd.Parameters.Add("@ItemId", SqlDbType.VarChar, 10).Value = objBO.ItemId;
                    cmd.Parameters.Add("@prm1", SqlDbType.VarChar, 100).Value = objBO.prm1;
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
        public dataSet diet_DiticianQueries(ipDietician objBO)
        {
            dataSet dsObj = new dataSet();
            using (SqlConnection con = new SqlConnection(GlobalConfig.ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("pdiet_DiticianQueries", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandTimeout = 2500;
                    cmd.Parameters.Add("@IPDNo", SqlDbType.VarChar, 10).Value = objBO.IPDNo;
                    cmd.Parameters.Add("@from", SqlDbType.Date).Value = objBO.from;
                    cmd.Parameters.Add("@to", SqlDbType.Date).Value = objBO.to;
                    cmd.Parameters.Add("@Prm1", SqlDbType.VarChar, 300).Value = objBO.Prm1;
                    cmd.Parameters.Add("@Prm2", SqlDbType.VarChar, 100).Value = objBO.Prm2;
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
        public string diet_InsertUpdateDiet(DietPatientInfo objBO)
        {
            string processInfo = string.Empty;
            using (SqlConnection con = new SqlConnection(GlobalConfig.ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("pdiet_InsertUpdateDiet", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandTimeout = 2500;
                    cmd.Parameters.Add("@UHID", SqlDbType.VarChar, 20).Value = objBO.UHID;
                    cmd.Parameters.Add("@IPDNo", SqlDbType.VarChar, 10).Value = objBO.IPDNo;
                    cmd.Parameters.Add("@AdmitDate", SqlDbType.DateTime).Value = objBO.AdmitDate;
                    cmd.Parameters.Add("@PatientName", SqlDbType.VarChar, 100).Value = objBO.PatientName;
                    cmd.Parameters.Add("@Age", SqlDbType.Decimal).Value = objBO.Age;
                    cmd.Parameters.Add("@ageType", SqlDbType.VarChar, 10).Value = objBO.ageType;
                    cmd.Parameters.Add("@Gender", SqlDbType.VarChar, 10).Value = objBO.Gender;
                    cmd.Parameters.Add("@Weight", SqlDbType.Decimal).Value = objBO.Weight;
                    cmd.Parameters.Add("@Height", SqlDbType.Decimal).Value = objBO.Height;
                    cmd.Parameters.Add("@DoctorId", SqlDbType.VarChar, 10).Value = objBO.DoctorId;
                    cmd.Parameters.Add("@DoctorName", SqlDbType.VarChar, 100).Value = objBO.DoctorName;
                    cmd.Parameters.Add("@FloorName", SqlDbType.VarChar, 50).Value = objBO.FloorName;
                    cmd.Parameters.Add("@RoomType", SqlDbType.VarChar, 50).Value = objBO.RoomType;
                    cmd.Parameters.Add("@RoomNo", SqlDbType.VarChar, 10).Value = objBO.RoomNo;
                    cmd.Parameters.Add("@BedNo", SqlDbType.VarChar, 10).Value = objBO.BedNo;
                    cmd.Parameters.Add("@MedicalProcedure", SqlDbType.VarChar, 200).Value = objBO.MedicalProcedure;
                    cmd.Parameters.Add("@PreExistingDisease", SqlDbType.VarChar, 500).Value = objBO.PreExistingDisease;
                    cmd.Parameters.Add("@ProcedureDate", SqlDbType.VarChar,10).Value = objBO.ProcedureDate;
                    cmd.Parameters.Add("@DietTypeId", SqlDbType.VarChar, 10).Value = objBO.DietTypeId;
                    cmd.Parameters.Add("@login_id", SqlDbType.VarChar, 10).Value = objBO.login_id;
                    cmd.Parameters.Add("@Logic", SqlDbType.VarChar, 50).Value = objBO.Logic;
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
        public string diet_LockOrFreeze(DietPatientInfo objBO)
        {
            string processInfo = string.Empty;
            using (SqlConnection con = new SqlConnection(GlobalConfig.ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("pdiet_LockOrFreeze", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandTimeout = 2500;
                    cmd.Parameters.Add("@IPDNo", SqlDbType.VarChar, 20).Value = objBO.IPDNo;
                    cmd.Parameters.Add("@FloorName", SqlDbType.VarChar, 50).Value = objBO.FloorName;
                    cmd.Parameters.Add("@DietCategory", SqlDbType.VarChar, 50).Value = objBO.DietCategory;
                    cmd.Parameters.Add("@Prm1", SqlDbType.VarChar, 50000).Value = objBO.Prm1;
                    cmd.Parameters.Add("@Prm2", SqlDbType.VarChar, 50000).Value = objBO.Prm2;
                    cmd.Parameters.Add("@login_id", SqlDbType.VarChar, 10).Value = objBO.login_id;
                    cmd.Parameters.Add("@Logic", SqlDbType.VarChar, 50).Value = objBO.Logic;
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
        public string DietmasterInsertUpdate(DietMasterBO objBO)
        {
            dataSet dsObj = new dataSet();
            string processInfo = string.Empty;
            using (SqlConnection con = new SqlConnection(GlobalConfig.ConStr_Hospital))
            {
                using (SqlCommand cmd = new SqlCommand("pdiet_InsertUpdateMaster", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandTimeout = 2500;
                    cmd.Parameters.Add("@AutoId", SqlDbType.Int).Value = objBO.Autoid;
                    cmd.Parameters.Add("@DietTypeId", SqlDbType.VarChar, 50).Value = objBO.DietTypeId;
                    cmd.Parameters.Add("@ItemId", SqlDbType.VarChar, 10).Value = objBO.ItemId;
                    cmd.Parameters.Add("@ItemName", SqlDbType.VarChar, 100).Value = objBO.ItemName;
                    cmd.Parameters.Add("@DietId", SqlDbType.VarChar, 10).Value = objBO.DietId;
                    cmd.Parameters.Add("@qty", SqlDbType.Int).Value = objBO.qty;
                    cmd.Parameters.Add("@DietName", SqlDbType.VarChar, 100).Value = objBO.DietName;
                    cmd.Parameters.Add("@DietRoute", SqlDbType.VarChar, 50).Value = objBO.DietRoute;
                    cmd.Parameters.Add("@DietTypeName", SqlDbType.VarChar, 50).Value = objBO.DietTypeName;
                    cmd.Parameters.Add("@NutritionName", SqlDbType.VarChar, 50).Value = objBO.NutritionName;
                    cmd.Parameters.Add("@Measuring", SqlDbType.VarChar, 100).Value = objBO.Measuring;
                    cmd.Parameters.Add("@NutritionValue", SqlDbType.Decimal).Value = objBO.NutritionValue;
                    cmd.Parameters.Add("@CalorieCount", SqlDbType.Decimal).Value = objBO.CalorieCount;
                    cmd.Parameters.Add("@MeasuringName", SqlDbType.VarChar, 50).Value = objBO.MeasuringName;
                    cmd.Parameters.Add("@CategoryName", SqlDbType.VarChar, 50).Value = objBO.CategoryName;
                    cmd.Parameters.Add("@ServeFrom", SqlDbType.Time).Value = objBO.servefrom;
                    cmd.Parameters.Add("@ServeTo", SqlDbType.Time).Value = objBO.serveto;
                    cmd.Parameters.Add("@cr_date", SqlDbType.DateTime).Value = objBO.date;
                    cmd.Parameters.Add("@IsActive", SqlDbType.Char).Value = objBO.IsActive;
                    cmd.Parameters.Add("@login_id", SqlDbType.VarChar, 10).Value = objBO.login_id;
                    cmd.Parameters.Add("@Logic", SqlDbType.VarChar, 50).Value = objBO.Logic;
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
        public string diet_InsertDietSchedule(List<DietScheduleInfo> objBO)
        {
            string processInfo = string.Empty;
            string Logic = string.Empty;
            string login_id = string.Empty;
            string Remark = string.Empty;
            DataTable dt = new DataTable();
            if (objBO.Count > 0)
            {
                dt.Columns.Add("IPDNo", typeof(string));
                dt.Columns.Add("FloorName", typeof(string));
                dt.Columns.Add("RoomNo", typeof(string));
                dt.Columns.Add("BedNo", typeof(string));
                dt.Columns.Add("ServingDate", typeof(string));
                dt.Columns.Add("DietCategory", typeof(string));
                dt.Columns.Add("DietId", typeof(string));
                dt.Columns.Add("ItemId", typeof(string));
                dt.Columns.Add("qty", typeof(decimal));
                dt.Columns.Add("medicalProcedure", typeof(string));
                foreach (DietScheduleInfo obj in objBO)
                {
                    var t = dt.AsEnumerable().Where(x => x.Field<string>("ItemId") == obj.ItemId);
                    if (t.Count() == 0)
                    {
                        Remark = obj.Remark;
                        login_id = obj.login_id;
                        Logic = obj.Logic;
                        DataRow dr = dt.NewRow();
                        dr["IPDNo"] = obj.IPDNo;
                        dr["FloorName"] = obj.FloorName;
                        dr["RoomNo"] = obj.RoomNo;
                        dr["BedNo"] = obj.BedNo;
                        dr["ServingDate"] = obj.ServingDate;
                        dr["DietCategory"] = obj.DietCategory;
                        dr["DietId"] = obj.DietId;
                        dr["ItemId"] = obj.ItemId;
                        dr["qty"] = obj.qty;
                        dr["medicalProcedure"] = obj.MedicalProcedure;
                        dt.Rows.Add(dr);
                    }
                }
            }
            using (SqlConnection con = new SqlConnection(GlobalConfig.ConStr_Hospital))
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand("pdiet_InsertDietSchedule", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandTimeout = 2500;
                    cmd.Parameters.AddWithValue("@udt_DietSchedule", dt);
                    cmd.Parameters.Add("@Remark", SqlDbType.VarChar).Value = Remark;
                    cmd.Parameters.Add("@login_id", SqlDbType.VarChar, 20).Value = login_id;
                    cmd.Parameters.Add("@Logic", SqlDbType.VarChar, 50).Value = Logic;
                    cmd.Parameters.Add("@result", SqlDbType.VarChar, 50).Value = "";
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
                }
            }
            return processInfo;
        }
    }
}