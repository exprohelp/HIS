using HISWebApi.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace HISWebApi.Repository.Patient
{
    public class PatientMaster
    {
        public string InsertPatientMaster(out string processInfo, PatientMasterBO objBO)
        {
            string UHIDNo = string.Empty;
            IPD.HISDBLayer hisdbLayer = new IPD.HISDBLayer();
            string itdoseresult = "";
            if (objBO.UHID.ToUpper() == "NEW" && objBO.Logic == "ItdoseNewEntry")
            {
                UHIDNo = hisdbLayer.InsertPatientMasterItdose(out itdoseresult, objBO);
                if (!itdoseresult.Contains("Success"))
                {
                    processInfo = itdoseresult;
                    return itdoseresult;
                }
                objBO.UHID = UHIDNo;
            }
            else
            {
                objBO.Logic = "OldEntry";
            }
            using (SqlConnection con = new SqlConnection(GlobalConfig.ConStr_Hospital))
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand("pPatient_MasterInsert", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandTimeout = 2500;
                    cmd.Parameters.Add("@hosp_id", SqlDbType.VarChar, 10).Value = objBO.hosp_id;
                    cmd.Parameters.Add("@barcodeno", SqlDbType.VarChar, 50).Value = objBO.barcodeno;
                    cmd.Parameters.Add("@Title", SqlDbType.VarChar, 20).Value = objBO.Title;
                    cmd.Parameters.Add("@FirstName", SqlDbType.VarChar, 50).Value = objBO.FirstName;
                    cmd.Parameters.Add("@LastName", SqlDbType.VarChar, 50).Value = objBO.LastName;
                    cmd.Parameters.Add("@patient_name", SqlDbType.VarChar, 60).Value = objBO.patient_name;
                    cmd.Parameters.Add("@gender", SqlDbType.VarChar, 10).Value = objBO.gender;
                    cmd.Parameters.Add("@dob", SqlDbType.DateTime, 15).Value = objBO.dob;
                    cmd.Parameters.Add("@age", SqlDbType.Int).Value = objBO.age;
                    cmd.Parameters.Add("@age_type", SqlDbType.VarChar, 10).Value = objBO.ageType;
                    cmd.Parameters.Add("@mobile_no", SqlDbType.VarChar, 20).Value = objBO.mobile_no;
                    cmd.Parameters.Add("@email_id", SqlDbType.VarChar, 50).Value = objBO.email_id;
                    cmd.Parameters.Add("@marital_status", SqlDbType.VarChar, 20).Value = objBO.marital_status;
                    cmd.Parameters.Add("@religion", SqlDbType.VarChar, 20).Value = objBO.religion;
                    cmd.Parameters.Add("@relation_of", SqlDbType.VarChar, 30).Value = objBO.relation_of;
                    cmd.Parameters.Add("@relation_name", SqlDbType.VarChar, 50).Value = objBO.relation_name;
                    cmd.Parameters.Add("@relation_phone", SqlDbType.VarChar, 25).Value = objBO.relation_phone;
                    cmd.Parameters.Add("@country", SqlDbType.VarChar, 50).Value = objBO.country;
                    cmd.Parameters.Add("@state", SqlDbType.VarChar, 50).Value = objBO.state;
                    cmd.Parameters.Add("@district", SqlDbType.VarChar, 50).Value = objBO.district;
                    cmd.Parameters.Add("@locality", SqlDbType.VarChar, 50).Value = objBO.locality;
                    cmd.Parameters.Add("@village", SqlDbType.VarChar, 50).Value = objBO.village;
                    cmd.Parameters.Add("@address", SqlDbType.VarChar, 200).Value = objBO.address;
                    cmd.Parameters.Add("@idType", SqlDbType.VarChar, 50).Value = objBO.idType;
                    cmd.Parameters.Add("@IDNo", SqlDbType.VarChar, 50).Value = objBO.IDNo;
                    cmd.Parameters.Add("@CardNo", SqlDbType.VarChar, 20).Value = objBO.CardNo;
                    cmd.Parameters.Add("@member_id", SqlDbType.VarChar, 20).Value = objBO.member_id;
                    cmd.Parameters.Add("@login_id", SqlDbType.VarChar, 15).Value = objBO.login_id;
                    cmd.Parameters.Add("@Logic", SqlDbType.VarChar, 50).Value = objBO.Logic;
                    cmd.Parameters.Add("@result", SqlDbType.VarChar, 100).Value = "";
                    cmd.Parameters["@result"].Direction = ParameterDirection.InputOutput;
                    cmd.Parameters.Add("@UHID", SqlDbType.VarChar, 20).Value = objBO.UHID;
                    cmd.Parameters["@UHID"].Direction = ParameterDirection.InputOutput;
                    try
                    {
                        cmd.ExecuteNonQuery();
                        processInfo = (string)cmd.Parameters["@result"].Value.ToString();
                        UHIDNo = (string)cmd.Parameters["@UHID"].Value.ToString();
                    }
                    catch (Exception sqlEx)
                    {
                        processInfo = "Error Found   : " + sqlEx.Message;
                    }
                    finally { con.Close(); }
                    return UHIDNo;
                }
            }
        }

    }
}