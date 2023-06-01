using HISWebApi.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace HISWebApi.Repository.WebPortal
{
    public class WebPortalDBLayer
    {
        ItDoseProxy.MobileAppSoapClient itdosehisProxy = new ItDoseProxy.MobileAppSoapClient();
        public string GetDoctorTimingSlot(ipWebPortal objBO)
        {
           return  itdosehisProxy.LoadTimeSlot(objBO.DoctorId, objBO.AppDate);
        }
        public dataSet GetDiagnosticTestAnalysis(ipWebPortal objBO)
        {
            string processInfo = string.Empty;
            DataSet ds = new DataSet();
            string qry = "CALL DoctorTimingSlot('" + objBO.DoctorId + "','" + objBO.AppDate + "'," + 1 + ")";
            return ExecuteDataset(qry);
        }
        public dataSet ExecuteDataset(string commandText)
        {
            MySqlConnection con = new MySqlConnection(GlobalConfig.ConStr_HISByItDose);
            dataSet dsObj = new dataSet();
            DataSet ds = new DataSet();
            MySqlCommand cmd = new MySqlCommand(commandText, con);
            cmd.CommandType = CommandType.Text;
            cmd.CommandTimeout = 2500;
            try
            {
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                da.Fill(ds);
                dsObj.ResultSet = ds;
                dsObj.Msg = "Success";
            }
            catch (Exception ex)
            {
                dsObj.ResultSet = null;
                dsObj.Msg = ex.Message;
            }
            return dsObj;
        }
        public string ExecuteScalar(string qry)
        {
            string processInfo = string.Empty;
            MySqlConnection mysqlcon = new MySqlConnection(GlobalConfig.ConStr_HISByItDose);
            MySqlCommand mysqlcmd = new MySqlCommand(qry, mysqlcon);
            try
            {
                mysqlcon.Open();
                mysqlcmd = new MySqlCommand(qry, mysqlcon);
                mysqlcmd.CommandType = CommandType.Text;
                mysqlcmd.CommandTimeout = 2500;
                if (mysqlcmd.ExecuteNonQuery() > 0)
                    processInfo = "Success";
            }
            catch (Exception ex) { }
            finally { mysqlcon.Close(); };
            return processInfo;
        }
        public dataSet SaveGenerateAppointment(ipWebPortal objBO)
        {
            string SqlCmd = string.Empty;
              SqlCmd += "INSERT INTO app_appointment (PatientID, DoctorID, AppointmentDate, StartTime, Discription, IsRegistred,centreID) VALUES ( '" + objBO.uhid + "', '" + objBO.DoctorId + "', '" + objBO.AppDate + "', '" + objBO.StartTime + "', '" + objBO.Desc + "', '" + objBO.IsReg + "','" + objBO.centerId + "');SELECT @@identity as AppId;";
            return ExecuteDataset(SqlCmd);
        }

        public dataSet DirectAppointment(ipWebPortal objBO)
        {
            string SqlCmd = string.Empty;
            SqlCmd = "INSERT INTO app_patient_master (Title, PatientName, Age, Mobile, Email, CountryID, cityID, House_No, Gender,PFirstName,PLastName, StateID, DistrictID) " +
                "VALUES ('" + objBO.Title + "','" + objBO.PatientName + "','" + objBO.Age + "','" + objBO.Mobile + "','" + objBO.Email + "','" + objBO.CountryID + "','" + objBO.cityID + "','" +objBO.House_No + "','" +objBO.Gender + "','" + objBO.PFirstName + "','" + objBO.PLastName + "','" + objBO.StateID + "','" + objBO.DistrictID + "');SELECT @@identity as AppId;";
            return ExecuteDataset(SqlCmd);
        }

        public dataSet CheckPatientExistence(ipWebPortal objBO)
        {
            
            string qry = "SELECT Title,PFirstName,pLastName,age,gender,mobile,CONCAT(house_no,' '+street_name) address FROM patient_master WHERE patient_id = '" + objBO.uhid + "'";
            return ExecuteDataset(qry);
        }
        public dataSet ExistAppoitment(ipWebPortal objBO)
        {
            string qry = string.Empty;
             qry= "SELECT StartTime FROM app_appointment WHERE AppointmentDate = '" + objBO.AppDate + "' AND DoctorID='" + objBO.DoctorId + "'";
            return ExecuteDataset(qry);
        }
    }
}