using HISWebApi.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;


namespace HISWebApi.Repository.Lab.Observation
{
    public class LabObservation
    {
        public dataSet LabObservationQueries(LabObservations objBO)
        {
            dataSet dsObj = new dataSet();
            using (SqlConnection con = new SqlConnection(GlobalConfig.ConStr_Hospital))
            {
                using (SqlCommand cmd = new SqlCommand("pm_LabObservationQueries", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandTimeout = 2500;
                    cmd.Parameters.Add("@hosp_id", SqlDbType.VarChar, 15).Value = objBO.hosp_id;
                    cmd.Parameters.Add("@catId", SqlDbType.VarChar, 15).Value = objBO.catid;
                    cmd.Parameters.Add("@subcatId", SqlDbType.VarChar, 15).Value = objBO.subcatid;
                    cmd.Parameters.Add("@testcode", SqlDbType.VarChar, 20).Value = objBO.testcode;
                    cmd.Parameters.Add("@prm1", SqlDbType.VarChar, 50).Value = objBO.prm_1;
                    cmd.Parameters.Add("@prm2", SqlDbType.VarChar, 50).Value = objBO.prm_2;
                    cmd.Parameters.Add("@Logic", SqlDbType.VarChar, 50).Value = objBO.Logic;
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
        
        public string ObservationInsertUpdate(LabObservations objBO)
        {
            dataSet dsObj = new dataSet();
            string processInfo = string.Empty;
            using (SqlConnection con = new SqlConnection(GlobalConfig.ConStr_Hospital))
            {
                using (SqlCommand cmd = new SqlCommand("pm_LabObservationInsertUpdate", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandTimeout = 2500;

                    cmd.Parameters.Add("@ObservationId", SqlDbType.VarChar, 15).Value = objBO.observationid;
                    cmd.Parameters.Add("@ObservationName", SqlDbType.VarChar, 100).Value = objBO.observationname;
                    cmd.Parameters.Add("@testcode", SqlDbType.VarChar, 100).Value = objBO.testcode;
                    cmd.Parameters.Add("@method_name", SqlDbType.VarChar, 20).Value = objBO.methodname;
                    cmd.Parameters.Add("@decimalPlace", SqlDbType.Int).Value = objBO.decimalplace;
                    cmd.Parameters.Add("@Interpretation", SqlDbType.VarChar, -1).Value = objBO.interpretation;
                    cmd.Parameters.Add("@default_value", SqlDbType.VarChar, 200).Value = objBO.defaultvalue;
                    cmd.Parameters.Add("@IsPrintable", SqlDbType.Bit).Value = objBO.isprintable;
                    cmd.Parameters.Add("@AutoAppove", SqlDbType.Bit).Value = objBO.isautoapprove;
                    cmd.Parameters.Add("@IsDeltaRequired", SqlDbType.Bit).Value = objBO.isdeltarequired;
                    cmd.Parameters.Add("@valuetype", SqlDbType.VarChar, 50).Value = objBO.valuetype;
                    cmd.Parameters.Add("@testunit", SqlDbType.VarChar,50).Value = objBO.testunit;
                    cmd.Parameters.Add("@IsActive", SqlDbType.Bit).Value = objBO.IsActive;
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
        public string LabInvestiObservationLink(LabObservations objBO)
        {
            dataSet dsObj = new dataSet();
            string processInfo = string.Empty;
            using (SqlConnection con = new SqlConnection(GlobalConfig.ConStr_Hospital))
            {
                using (SqlCommand cmd = new SqlCommand("pm_LabInvestiObservationLink", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandTimeout = 2500;
                    cmd.Parameters.Add("@testcode", SqlDbType.VarChar, 10).Value = objBO.testcode;
                    cmd.Parameters.Add("@ObservationId", SqlDbType.VarChar, 10).Value = objBO.observationid;                  
                    cmd.Parameters.Add("@HeaderName", SqlDbType.VarChar, 200).Value = objBO.HeaderName;
                    cmd.Parameters.Add("@IsBold", SqlDbType.Int).Value = objBO.IsBold;
                    cmd.Parameters.Add("@IsItalic", SqlDbType.Int).Value = objBO.IsItalic;
                    cmd.Parameters.Add("@IsResultMandatory", SqlDbType.Int).Value = objBO.IsResultMandatory;
                    cmd.Parameters.Add("@Prm1", SqlDbType.VarChar, 100).Value = objBO.Prm1;
                    cmd.Parameters.Add("@Prm2", SqlDbType.VarChar, 100).Value = objBO.Prm2;
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
        public string ObservationInfoInsertUpdate(LabObservationsInfo objBO)
        {
            dataSet dsObj = new dataSet();
            string processInfo = string.Empty;
            using (SqlConnection con = new SqlConnection(GlobalConfig.ConStr_Hospital))
            {
                using (SqlCommand cmd = new SqlCommand("pm_LabObservationInfo", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandTimeout = 2500;
                    cmd.Parameters.Add("@autoid", SqlDbType.VarChar, 15).Value = objBO.autoid;
                    cmd.Parameters.Add("@machine_id", SqlDbType.VarChar, 15).Value = objBO.machineid;                  
                    cmd.Parameters.Add("@ObservationId", SqlDbType.VarChar, 15).Value = objBO.observationid;
                    cmd.Parameters.Add("@Gender", SqlDbType.VarChar, 20).Value = objBO.gender;
                    cmd.Parameters.Add("@Prefix", SqlDbType.VarChar, 15).Value = objBO.prefix;
                    cmd.Parameters.Add("@low_value", SqlDbType.VarChar, 20).Value = objBO.lowvalue;
                    cmd.Parameters.Add("@high_value", SqlDbType.VarChar, 20).Value = objBO.highvalue;
                    cmd.Parameters.Add("@panic_low", SqlDbType.VarChar, 20).Value = objBO.paniclow;
                    cmd.Parameters.Add("@panic_high", SqlDbType.VarChar, 20).Value = objBO.panichigh;
                    cmd.Parameters.Add("@age_from", SqlDbType.VarChar, 20).Value = objBO.agefrom;
                    cmd.Parameters.Add("@age_to", SqlDbType.VarChar, 20).Value = objBO.ageto;
                    cmd.Parameters.Add("@display_reading", SqlDbType.VarChar).Value = objBO.displayreading;
                    cmd.Parameters.Add("@labcode", SqlDbType.VarChar, 20).Value = objBO.LabCode;
                    cmd.Parameters.Add("@methodname", SqlDbType.VarChar, 50).Value = objBO.methodname;
                    cmd.Parameters.Add("@testunit", SqlDbType.VarChar, 50).Value = objBO.testunit;
                    cmd.Parameters.Add("@login_id", SqlDbType.VarChar, 15).Value = objBO.login_id;
                    cmd.Parameters.Add("@hosp_id", SqlDbType.VarChar, 15).Value = objBO.hosp_id;
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
        public string ObservationInfoDelete(LabObservationsInfo objBO)
        {
            dataSet dsObj = new dataSet();
            string processInfo = string.Empty;
            using (SqlConnection con = new SqlConnection(GlobalConfig.ConStr_Hospital))
            {
                using (SqlCommand cmd = new SqlCommand("pm_LabObservationInfo", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandTimeout = 2500;
                    cmd.Parameters.Add("@autoid", SqlDbType.VarChar, 15).Value = objBO.autoid ;
                    cmd.Parameters.Add("@machine_id", SqlDbType.VarChar, 15).Value = objBO.machineid;
                    cmd.Parameters.Add("@ObservationId", SqlDbType.VarChar, 15).Value = objBO.observationid;
                    cmd.Parameters.Add("@Gender", SqlDbType.VarChar, 15).Value = objBO.gender;
                    cmd.Parameters.Add("@Prefix", SqlDbType.VarChar, 15).Value = objBO.prefix;
                    cmd.Parameters.Add("@low_value", SqlDbType.VarChar, 15).Value = objBO.lowvalue;
                    cmd.Parameters.Add("@high_value", SqlDbType.VarChar, 15).Value = objBO.highvalue;
                    cmd.Parameters.Add("@panic_low", SqlDbType.VarChar, 15).Value = objBO.paniclow;
                    cmd.Parameters.Add("@panic_high", SqlDbType.VarChar, 15).Value = objBO.panichigh;
                    cmd.Parameters.Add("@age_from", SqlDbType.VarChar, 15).Value = objBO.agefrom;
                    cmd.Parameters.Add("@age_to", SqlDbType.VarChar, 15).Value = objBO.ageto;
                    cmd.Parameters.Add("@display_reading", SqlDbType.VarChar, 15).Value = objBO.displayreading;
                    cmd.Parameters.Add("@login_id", SqlDbType.VarChar, 10).Value = objBO.login_id;
                    cmd.Parameters.Add("@hosp_id", SqlDbType.VarChar, 10).Value = objBO.hosp_id;
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

        public string DefaultValueInsert(LabDefaultValue objBO)
        {
            dataSet dsObj = new dataSet();
            string processInfo = string.Empty;
            using (SqlConnection con = new SqlConnection(GlobalConfig.ConStr_Hospital))
            {
                using (SqlCommand cmd = new SqlCommand("pm_LabDefaultValue", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandTimeout = 2500;
                    cmd.Parameters.Add("@autoid", SqlDbType.VarChar, 15).Value = objBO.autoid;                    
                    cmd.Parameters.Add("@observationId", SqlDbType.VarChar, 15).Value = objBO.observationid;
                    cmd.Parameters.Add("@defaultvalue", SqlDbType.VarChar, 10).Value = objBO.defaultvalue;
                    cmd.Parameters.Add("@login_id", SqlDbType.VarChar, 10).Value = objBO.login_id;                   
                    cmd.Parameters.Add("@logic", SqlDbType.VarChar, 50).Value = objBO.Logic;
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

        public string LabTransportTime(LabTransportTime objBO)
        {
            dataSet dsObj = new dataSet();
            string processInfo = string.Empty;
            using (SqlConnection con = new SqlConnection(GlobalConfig.ConStr_Hospital))
            {
                using (SqlCommand cmd = new SqlCommand("pm_LabInsertUpdateTransportTime", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandTimeout = 2500;
                    cmd.Parameters.Add("@autoid", SqlDbType.Int, 5).Value = objBO.autoid;
                    cmd.Parameters.Add("@labcode", SqlDbType.VarChar, 5).Value = objBO.labcode;
                    cmd.Parameters.Add("@dispatch_lab", SqlDbType.VarChar, 5).Value = objBO.dispatch_lab;
                    cmd.Parameters.Add("@inshift", SqlDbType.VarChar, 10).Value = objBO.inshift;
                    cmd.Parameters.Add("@outshift", SqlDbType.VarChar, 10).Value = objBO.outshift;
                    cmd.Parameters.Add("@transport_time", SqlDbType.Int, 10).Value = objBO.transport_time;
                    cmd.Parameters.Add("@login_id", SqlDbType.VarChar, 10).Value = objBO.login_id;
                    cmd.Parameters.Add("@Logic", SqlDbType.VarChar, 20).Value = objBO.Logic;
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

    }
}