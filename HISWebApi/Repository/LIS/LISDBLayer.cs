using HISWebApi.Models;
using MySql.Data.MySqlClient;
using System;
using System.Data;

namespace HISWebApi.Repository.IPD
{
    public class LISDBLayer
    {
        public dataSet ITDosePatientInfo(LISBO.PatientBO objBO)
        {
            string processInfo = string.Empty;
            DataSet ds = new DataSet();
            string qry = "select top 10 *from patient_master";
            return ExecuteDataset(qry);
        }
        public string UpdateRemark(LISBO.PatientBO objBO)
        {
            string processInfo = string.Empty;
            DataSet ds = new DataSet();
            string qry = "UPDATE f_ledgertransaction SET   customerSlab='"+objBO.section+ "'  WHERE LedgerTransactionNo='" + objBO.visit_no + "' ";
            return ExecuteScalar(qry);
        }
        public dataSet GetPatientDetails(LISBO.PatientBO objBO)
        {
            string processInfo = string.Empty;
            DataSet ds = new DataSet();
            string qry = "";
            if (objBO.Logic == "Pending")
            {
                qry = " SELECT (CASE WHEN plo.CentreIDSession=2109 THEN 'CHL Diagnostic' ELSE 'HOSPITAL' END) Unit_Name,fl.LedgerTransactionNo,CONCAT(pm.title, pm.`PName`)PatientName,fl.Gender,fl.Age,SUM(Amount) amount,customerSlab";
                qry += " FROM patient_labinvestigation_opd plo INNER JOIN f_ledgertransaction fl ON plo.LedgerTransactionNo = fl.LedgerTransactionNo";
                qry += " INNER JOIN patient_master pm ON pm.Patient_ID = fl.Patient_ID";
                qry += " INNER JOIN f_itemmaster fim ON fim.Type_id = plo.`Investigation_ID` ";
                qry += " INNER JOIN centre_master cm ON cm.centreId = plo.CentreIDSession";

                if (objBO.Prm1=="ALL")
                qry += " WHERE plo.Investigation_ID in ( 15551,15586)  AND (plo.CentreIDSession = 2109 or plo.CentreIDSession = 5 )";
                if (objBO.Prm1!= "ALL")
                qry += " WHERE plo.Investigation_ID in ( 15551,15586)  AND (plo.CentreIDSession = " + objBO.Prm1 + " or plo.CentreIDSession = 5 )";

                qry += " AND fl.Date >= '" + objBO.from.Replace("/", "-") + " 00:00:00' AND fl.Date <= '" + objBO.from.Replace("/", "-") + " 23:59:59'  AND plo.IsRefund = 0 AND amount>= 0";
                qry += " AND(plo.IsPackage = 0 OR  plo.investigation_id = '') AND LENGTH(IFNULL(customerSlab,''))<2 ";
                qry += " GROUP BY (CASE WHEN plo.CentreIDSession=2109 THEN 'CHL Diagnostic' ELSE 'HOSPITAL' END),fl.LedgerTransactionNo,pm.title,pm.`PName`,fl.Gender,fl.Age,customerSlab order by fl.Date desc";
            }
            else
            {
                qry = " SELECT (CASE WHEN plo.CentreIDSession=2109 THEN 'CHL Diagnostic' ELSE 'HOSPITAL' END) Unit_Name,fl.LedgerTransactionNo,CONCAT(pm.title, pm.`PName`)PatientName,fl.Gender,fl.Age,SUM(Amount) amount,customerSlab";
                qry += " FROM patient_labinvestigation_opd plo INNER JOIN f_ledgertransaction fl ON plo.LedgerTransactionNo = fl.LedgerTransactionNo";
                qry += " INNER JOIN patient_master pm ON pm.Patient_ID = fl.Patient_ID";
                qry += " INNER JOIN f_itemmaster fim ON fim.Type_id = plo.`Investigation_ID` ";
                qry += " INNER JOIN centre_master cm ON cm.centreId = plo.CentreIDSession";

                if (objBO.Prm1 == "ALL")
                    qry += " WHERE plo.Investigation_ID in ( 15551,15586)  AND (plo.CentreIDSession = 2109 or plo.CentreIDSession = 5 )";
                if (objBO.Prm1 != "ALL")
                    qry += " WHERE plo.Investigation_ID in ( 15551,15586)  AND (plo.CentreIDSession = " + objBO.Prm1 + " or plo.CentreIDSession = 5 )";

                qry += " AND fl.Date >= '" + objBO.from.Replace("/", "-") + " 00:00:00' AND fl.Date <= '" + objBO.from.Replace("/", "-") + " 23:59:59'  AND plo.IsRefund = 0 AND amount>= 0";
                qry += " AND(plo.IsPackage = 0 OR  plo.investigation_id = '') AND LENGTH(IFNULL(customerSlab,''))>2 ";
                qry += " GROUP BY (CASE WHEN plo.CentreIDSession=2109 THEN 'CHL Diagnostic' ELSE 'HOSPITAL' END),fl.LedgerTransactionNo,pm.title,pm.`PName`,fl.Gender,fl.Age,customerSlab order by fl.Date desc";

            }
            return ExecuteDataset(qry);
        }
        public string  GetAssignedReport(LISBO.PatientBO objBO)
        {
            string processInfo = string.Empty;
            string qry = "";
            qry = " SELECT (CASE WHEN plo.CentreIDSession=2109 THEN 'CHL Diagnostic' ELSE 'HOSPITAL' END) Unit_Name,plo.LedgerTransactionID,fl.LedgerTransactionNo,CONCAT(pm.title, pm.`PName`)PatientName,fl.Gender,fl.Age,SUM(Amount) amount,customerSlab,plo.Test_ID,plo.InvestigationName,IF(plo.Approved=0,'Pending','Ready')ReportStatus";
            qry += " FROM patient_labinvestigation_opd plo INNER JOIN f_ledgertransaction fl ON plo.LedgerTransactionNo = fl.LedgerTransactionNo";
            qry += " INNER JOIN patient_master pm ON pm.Patient_ID = fl.Patient_ID";
            qry += " INNER JOIN f_itemmaster fim ON fim.Type_id = plo.`Investigation_ID` ";
            qry += " INNER JOIN centre_master cm ON cm.centreId = plo.CentreIDSession";
            if (objBO.Prm1 == "ALL")
            qry += " WHERE plo.Investigation_ID in ( 15551,15586)  AND (plo.CentreIDSession = 2109 or plo.CentreIDSession = 5 )";
            if (objBO.Prm1 != "ALL")
            qry += " WHERE plo.Investigation_ID in ( 15551,15586)  AND (plo.CentreIDSession = " + objBO.Prm1 + " or plo.CentreIDSession = 5 )";
            qry += " AND fl.Date >= '" + objBO.from.Replace("/", "-") + " 00:00:00' AND fl.Date <= '" + objBO.from.Replace("/", "-") + " 23:59:59'  AND plo.IsRefund = 0 AND amount>= 0";
            qry += " AND(plo.IsPackage = 0 OR  plo.investigation_id = '') AND LENGTH(IFNULL(customerSlab,''))>2 ";
            qry += " GROUP BY plo.CentreIDSession,plo.LedgerTransactionID,fl.LedgerTransactionNo,pm.title,pm.`PName`,fl.Gender,fl.Age,customerSlab,plo.Test_ID,plo.InvestigationName order by customerSlab";
            dataSet ds= ExecuteDataset(qry);
            string str = "";
            string temp = "";
            foreach (DataRow dr in ds.ResultSet.Tables[0].Rows)
            {
                if (temp != dr["customerSlab"].ToString())
                {
                    str += "<tr>";
                    str += "<td colspan='3' style='background-color:#dff3ff;'>" + dr["customerSlab"].ToString() + "</td>";
                    str += "<tr>";
                    temp = dr["customerSlab"].ToString();
                }
                str += "<tr>";
                str += "<td >" + dr["Unit_Name"].ToString() + "</td>";
                str += "<td >" + dr["LedgerTransactionNo"].ToString() + "</td>";
                str += "<td >" + dr["PatientName"].ToString()  + "</td>";
                str += "<td >" + dr["Gender"].ToString()+ "</td>";
                str += "<td >" + dr["Age"].ToString()+ "</td>";
                str += "<td >" + dr["amount"].ToString()  + "</td>";
                str += "<td >" + dr["InvestigationName"].ToString()  + "</td>";
                str += "<td >" + dr["ReportStatus"].ToString()  + "</td>";
                if (dr["ReportStatus"].ToString() == "Ready")
                {
                    string link=  "http://chandan.online/Chandan/OnlineLabReports.aspx?LedgertransactionID=" + Common.Encrypt(dr["LedgerTransactionID"].ToString()) + "";
                    str += "<td><a target='_blank' href='" + link + "' class='btn btn-success' download='download'>Download</a></td>";
                }
                else
                {
                    str += "<td></td>";
                }
                str += "<tr>";

            }
            return str;
        }


        public string ExecuteScalar(string qry)
        {
            string processInfo = string.Empty;
            MySqlConnection con = new MySqlConnection(GlobalConfig.ConStr_LISByItDose);
            MySqlCommand cmd = new MySqlCommand(qry, con);
            cmd.CommandType = CommandType.Text;
            cmd.CommandTimeout = 2500;
            try
            {
                con.Open();
                if (cmd.ExecuteNonQuery() > 0)
                {
                    processInfo = "Successfully Saved";
                }
                else
                {
                    processInfo = "Not Saved";
                }
            }
            catch (MySqlException MySqlEx)
            {
                if (!MySqlEx.ToString().Contains("Violation of PRIMARY KEY"))
                    processInfo = "Error Found   : " + MySqlEx.Message;
                else
                    processInfo = " ";
            }
            finally { con.Close(); }
            return processInfo;
        }
        public dataSet ExecuteDataset(string commandText)
        {
            MySqlConnection con = new MySqlConnection(GlobalConfig.ConStr_LISByItDose);
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
    }
}