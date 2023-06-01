using HISWebApi.Models;
using HISWebApi.Repository;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using static HISWebApi.Models.IPDBO;

namespace HISWebApi.Repository.IPD
{
    public class HISDBLayer
    {
        public dataSet GetPatientInfoByUHID(string uhid)
        {
            string processInfo = string.Empty;
            DataSet ds = new DataSet();
            string qry = string.Empty;
            qry += " SELECT Patient_id UHID,PName PatientName,House_no,Mobile mobileNo,'ITDOSE' As StartDate, dob,Gender,district District,'ITDOSE' sourceData FROM PATIENT_MASTER WHERE Patient_ID='" + uhid + "'";
            return ExecuteDataset(qry);
        }
        public dataSet HIS_OPDCall(HospitalBO ipapp)
        {
            string processInfo = string.Empty;
            DataSet ds = new DataSet();
            string qry = string.Empty;
            qry += " SELECT op.ID,PatientName, Age, pm.Mobile,AppointmentDate,StartTime,Discription,dm.Name DoctorName,iscancel,CancelDateTime,CancelReason,CallStatus FROM app_appointment op ";
            qry += " INNER JOIN app_patient_master pm ON op.PatientId = pm.ID ";
            qry += " INNER JOIN doctor_master dm ON dm.Doctor_id = op.Doctorid ";
            qry += " WHERE appointmentDate BETWEEN '" + ipapp.from + "' AND '" + ipapp.to + "' AND LENGTH(PatientName)> 2  ";
            return ExecuteDataset(qry);
        }
        public string HIS_OPDCancelAndCallDone(HospitalBO ipapp)
        {
            string processInfo = string.Empty;
            DataSet ds = new DataSet();
            string qry = string.Empty;
            if (ipapp.Logic == "Cancel")
                qry = " UPDATE app_appointment SET IsCancel = 1, CancelDateTime = NOW() WHERE ID = " + ipapp.ID + " ";
            else if (ipapp.Logic == "CallDone")
                qry += " UPDATE app_appointment SET CallStatus = 'Done', CallDate = NOW(), CallBy = '" + ipapp.login_id + "' WHERE ID = " + ipapp.ID + " ";
            return ExecuteScalar(qry);
        }
        public dataSet GetAdmittedIPDPatient()
        {
            string processInfo = string.Empty;
            DataSet ds = new DataSet();
            string qry = "call sp_admittedIPDPatient";            
            return ExecuteDataset(qry);
        }
        //public dataSet GetOPDEvery10PatientList(HospitalBO ipapp)
        //{
        //    string processInfo = string.Empty;
        //    DataSet ds = new DataSet();
        //    string qry = "call sp_CallOverDuePatient ('','','" + ipapp.from + "','" + ipapp.Logic + "') ";
        //    return ExecuteDataset(qry);
        //}
        public DataSet GetOPDEvery10PatientList(HospitalBO ipapp)
        {
            DataSet dsReport = new DataSet();
            string _result = string.Empty;
            string qry = "call sp_CallOverDuePatient ('','','" + ipapp.from + "','" + ipapp.Logic + "') ";
            dataSet ds = ExecuteDataset(qry);
            string IpdNoList = string.Empty;
            foreach (DataRow dr in ds.ResultSet.Tables[0].Rows)
            {
                IpdNoList = IpdNoList + dr["App_id"].ToString() + ",";
            }

            IPD obj = new IPD();
            FeedbackInfo objBO = new FeedbackInfo();
            objBO.Prm3 = IpdNoList;
            objBO.Logic = "OPDFeedBackTaken";

            DataTable dt1 = ds.ResultSet.Tables[0];
            dt1.TableName = "Table1";

            dataSet ds2 = obj.IPD_FeedbackQuesries(objBO);
            DataTable dt2 = ds2.ResultSet.Tables[0];
            dt2.TableName = "Table2";

            dsReport.Tables.Add(dt1.Copy()); //iTDOSE data
            dsReport.Tables.Add(dt2.Copy());
            return dsReport;
        }

        public dataSet GetAdmittedIPDPatientByDoctor(string DoctorId)
        {
            string processInfo = string.Empty;
            DataSet ds = new DataSet();
            string qry = "call sp_admittedIPDPatientByDoctor ('"+ DoctorId + "') ";
            return ExecuteDataset(qry);
        }
        public dataSet GetOverDuePatientList(ipOverDueAmount ipoverdue )
        {
            string processInfo = string.Empty;
            DataSet ds = new DataSet();
            string qry = "CALL sp_CallOverDuePatient ('"+ ipoverdue.IPDNo + "','" + ipoverdue.LoginId + "','" + ipoverdue.Prm1 + "','"+ ipoverdue.Logic + "') ";
            return ExecuteDataset(qry);
        }
        public dataSet HIS_PatientQOfDoctor(string DoctorId)
        {
            string processInfo = string.Empty;
            DataSet ds = new DataSet();
            string qry = string.Empty;
            qry += " SELECT UPPER(CONCAT(app.`Title`,' ',app.`Pname`))PName,app.`AppNo`,app.`IsAbsent`,'0' AS 'IsCurrent',IFNULL(IsCall, 0) IsCall FROM `appointment` app  ";
            qry += " WHERE DATE=CURRENT_DATE() AND IFNULL(IsCall,0) IN (1,0)  AND P_Out=0  AND flag=0 AND TemperatureRoom=1 AND Doctor_ID='" + DoctorId + "'  ";
            return ExecuteDataset(qry);
        }
        public dataSet HIS_GetPatientListByMobileOrUHID(string Prm1)
        {
            string processInfo = string.Empty;
            DataSet ds = new DataSet();
            string qry = string.Empty;
            qry += " SELECT Patient_ID,PName,Mobile  FROM PATIENT_MASTER WHERE (mobile='"+ Prm1 + "' OR Patient_ID='"+ Prm1 + "'); ";
            return ExecuteDataset(qry);
        }
        public string CallOverDueInsertUpdate(ipOverDueAmount ipoverdue)
        {
            DataSet ds = new DataSet();
            string processInfo = string.Empty;
            MySqlConnection con = new MySqlConnection(GlobalConfig.ConStr_HISByItDose);
            MySqlCommand cmd = new MySqlCommand("sp_CallOverDueInsertUpdate", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandTimeout = 2500;
            cmd.Parameters.Add("_IpdNo", MySqlDbType.VarChar, 20).Value = ipoverdue.IPDNo;
            cmd.Parameters.Add("_Remark", MySqlDbType.VarChar, 30).Value = ipoverdue.Remark;
            cmd.Parameters.Add("_LoginName", MySqlDbType.VarChar, 20).Value = ipoverdue.LoginName;
            cmd.Parameters.Add("_Prm1", MySqlDbType.VarChar, 100).Value = ipoverdue.Prm1;
            cmd.Parameters.Add("_MinAlertAmount", MySqlDbType.Decimal).Value = ipoverdue.MinAlertAmount;
            cmd.Parameters.Add("_RepeatCallMinute", MySqlDbType.Int64).Value = ipoverdue.RepeatCallMinute;
            cmd.Parameters.Add("_Logic", MySqlDbType.VarChar, 100).Value = ipoverdue.Logic;
            cmd.Parameters.Add("_Result", MySqlDbType.VarChar, 100).Value = "";
            cmd.Parameters["_Result"].Direction = ParameterDirection.InputOutput;
            try
            {
                con.Open();
                cmd.ExecuteNonQuery();
                processInfo = (string)cmd.Parameters["_Result"].Value.ToString();
            }
            catch (Exception ex) { processInfo = ex.Message; }
            finally { con.Close(); }
            return processInfo;
        }
        public string InsertPatientMasterItdose(out string processInfo, PatientMasterBO objBO)
        {
            string UHIDNo = string.Empty;
            using (MySqlConnection con = new MySqlConnection(GlobalConfig.ConStr_HISByItDose))
            {
                con.Open();
                MySqlParameter paramTnxID = new MySqlParameter();
                paramTnxID.ParameterName = "@PatientID";
                paramTnxID.MySqlDbType = MySqlDbType.VarChar;
                paramTnxID.Size = 50;
                paramTnxID.Direction = ParameterDirection.Output;

                using (MySqlCommand cmd = new MySqlCommand("Insert_patient", con))
                {

                    cmd.CommandTimeout = 2500;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Loc", "L");
                    cmd.Parameters.AddWithValue("@Hosp", "CH01");
                    cmd.Parameters.AddWithValue("@Title", objBO.Title);
                    cmd.Parameters.AddWithValue("@Pname", objBO.patient_name);
                    cmd.Parameters.AddWithValue("@PFirstName", objBO.FirstName);
                    cmd.Parameters.AddWithValue("@PLastName", objBO.LastName);
                    cmd.Parameters.AddWithValue("@House_No", objBO.address);
                    cmd.Parameters.AddWithValue("@Street_Name", "");
                    cmd.Parameters.AddWithValue("@Locality", "");
                    cmd.Parameters.AddWithValue("@City", objBO.district);
                    cmd.Parameters.AddWithValue("@Pincode", "");
                    cmd.Parameters.AddWithValue("@Phone", "");
                    cmd.Parameters.AddWithValue("@Mobile", objBO.mobile_no);
                    cmd.Parameters.AddWithValue("@Email", objBO.email);
                    cmd.Parameters.AddWithValue("@DOB", objBO.dob);
                    cmd.Parameters.AddWithValue("@Age", objBO.dob);
                    cmd.Parameters.AddWithValue("@Relation", objBO.relation_of);
                    cmd.Parameters.AddWithValue("@RelationName", objBO.relation_name);
                    cmd.Parameters.AddWithValue("@TimeOfBirth", "-");
                    cmd.Parameters.AddWithValue("@PlaceOfBirth", "");
                    cmd.Parameters.AddWithValue("@IdentificationMark", "");
                    cmd.Parameters.AddWithValue("@BloodGroup", "");
                    cmd.Parameters.AddWithValue("@EmergencyPhone", "");
                    cmd.Parameters.AddWithValue("@Gender", objBO.gender);
                    cmd.Parameters.AddWithValue("@MaritalStatus", objBO.marital_status);
                    cmd.Parameters.AddWithValue("@DateEnrolled", System.DateTime.Now);
                    cmd.Parameters.AddWithValue("@FeesPaid", 0);
                    cmd.Parameters.AddWithValue("@HospitalOfEnroll_ID", "");
                    cmd.Parameters.AddWithValue("@ExtractDate", "1900-01-01");
                    cmd.Parameters.AddWithValue("@Active", 1);
                    cmd.Parameters.AddWithValue("@UserName", "");
                    cmd.Parameters.AddWithValue("@PASSWORD", "");
                    cmd.Parameters.AddWithValue("@CardPaid", "");
                    cmd.Parameters.AddWithValue("@State", objBO.state);
                    cmd.Parameters.AddWithValue("@Country", objBO.country);
                    cmd.Parameters.AddWithValue("@Passport_No", "");
                    cmd.Parameters.AddWithValue("@Passport_IssueDate", "1900-01-01");
                    cmd.Parameters.AddWithValue("@Patient_Category", "-");
                    cmd.Parameters.AddWithValue("@Remark", "-");
                    cmd.Parameters.AddWithValue("@Weight", "-");
                    cmd.Parameters.AddWithValue("@ResidentialAddress", "");
                    cmd.Parameters.AddWithValue("@ReligiousAffiliation", "");
                    cmd.Parameters.AddWithValue("@LanguageSpoken", "");
                    cmd.Parameters.AddWithValue("@Occupation", "");
                    cmd.Parameters.AddWithValue("@Employer", "");
                    cmd.Parameters.AddWithValue("@EmergencyNotify", "");
                    cmd.Parameters.AddWithValue("@EmergencyRelationShip", "");
                    cmd.Parameters.AddWithValue("@EmergencyAddress", "");
                    cmd.Parameters.AddWithValue("@EmergencyPhoneNo", "");
                    cmd.Parameters.AddWithValue("@RegisterBy", objBO.login_id);
                    cmd.Parameters.AddWithValue("@PatientType", "1");
                    cmd.Parameters.AddWithValue("@Ethnicity", "");
                    cmd.Parameters.AddWithValue("@Card_ID", 0);
                    cmd.Parameters.AddWithValue("@Place", "");
                    cmd.Parameters.AddWithValue("@Taluka", "");
                    cmd.Parameters.AddWithValue("@LandMark", "");
                    cmd.Parameters.AddWithValue("@District", objBO.district);
                    cmd.Parameters.AddWithValue("@Panel_ID", 0);
                    cmd.Parameters.AddWithValue("@AdharCardNo", "");
                    cmd.Parameters.AddWithValue("@CentreID", 1);
                    cmd.Parameters.AddWithValue("@Hospital_ID", "HOS/070920/00001");
                    cmd.Parameters.AddWithValue("@IPAddress", "");
                    cmd.Parameters.AddWithValue("@CountryID", 14);
                    cmd.Parameters.AddWithValue("@DistrictID", 110);
                    cmd.Parameters.AddWithValue("@CityID", 0);
                    cmd.Parameters.AddWithValue("@TalukaID", 0);
                    cmd.Parameters.AddWithValue("@HospPatientType", "-");
                    cmd.Parameters.AddWithValue("@OldPatient_ID", "");
                    cmd.Parameters.AddWithValue("@StateID", 34);
                    cmd.Parameters.AddWithValue("@IsRegistrationApply", 0);
                    cmd.Parameters.AddWithValue("@Source", "");
                    cmd.Parameters.AddWithValue("@Religion", "");
                    cmd.Parameters.AddWithValue("@NextRegistrationDate", System.DateTime.Now.AddYears(1));
                    cmd.Parameters.Add(paramTnxID);
                    try
                    {
                        UHIDNo = cmd.ExecuteScalar().ToString(); // ds.Tables[0].Rows[0]["PatientID"].ToString();
                        processInfo = "Success";
                    }
                    catch (Exception sqlEx)
                    {
                        processInfo = "Error Found   : " + sqlEx.Message;
                        UHIDNo = "Erro";
                    }
                    finally { con.Close(); }
                    return UHIDNo;
                }
            }
        }
        public dataSet GetDoctor()
        {
            string processInfo = string.Empty;
            DataSet ds = new DataSet();
            string qry = "SELECT Doctor_id,NAME DoctorName FROM doctor_master WHERE isactive=1 ORDER BY DoctorName";
            return ExecuteDataset(qry);
        }
        public dataSet GetDiagnosticTestAnalysis(ipIPDAudit objBO)
        {
            string processInfo = string.Empty;
            DataSet ds = new DataSet();
            string qry = "call sp_TestAnalysisInIPD ('" + objBO.IPDNo + "')";
            return ExecuteDataset(qry);
        }
        public dataSet LabBookingOfPatient(string PatientId)
        {
            string qry = string.Empty;
            qry += "SELECT lt.LedgerTransactionNo,DATE_FORMAT(lt.Date,'%d-%m-%Y') Date ,REPLACE(REPLACE(lt.LedgerTransactionNo, 'LOSHHI', '1'), 'LSHHI', '2') LabNo,plo.Test_ID, ";
            qry += "im.Name TestName, IF(plo.Approved = 0, 'Pending', 'Ready')ReportStatus,plo.IsOutSource FROM f_ledgertransaction lt ";
            qry += "INNER JOIN patient_labinvestigation_opd plo ON plo.LedgerTransactionNo = lt.LedgerTransactionNo ";
            qry += "INNER JOIN investigation_master im ON im.Investigation_Id = plo.Investigation_ID ";
            qry += "INNER JOIN patient_master pm ON pm.Patient_ID = plo.Patient_ID ";
            qry += "WHERE lt.IsCancel = 0 AND lt.TypeOfTnx IN('OPD-BILLING','OPD-LAB','OPD-Package') 	AND pm.Patient_ID = '" + PatientId + "' ";
            qry += "ORDER BY lt.LedgerTransactionNo DESC ";
            return ExecuteDataset(qry);
        }
        public dataSet HISCustomerList(string MobileNo)
        {
            string qry = string.Empty;
            qry += "SELECT pm.Patient_ID,pm.PName FROM f_ledgerTransaction lt INNER JOIN patient_master pm ON pm.`Patient_ID`= lt.`Patient_ID` ";
            qry += "WHERE pm.`Mobile`= '" + MobileNo + "' GROUP BY pm.Patient_ID,pm.PName ";
            return ExecuteDataset(qry);
        }
        public dataSet GetPanel()
        {
            string processInfo = string.Empty;
            DataSet ds = new DataSet();
            string qry = "SELECT Panel_id,company_name FROM f_panel_master ORDER BY company_name";
            return ExecuteDataset(qry);
        }
        public string ExecuteScalar(string qry)
        {
            string processInfo = string.Empty;
            MySqlConnection con = new MySqlConnection(GlobalConfig.ConStr_HISByItDose);
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
        public dataSet GetPatientDetailByIPDNo(string IPDNo)
        {
            string processInfo = string.Empty;
            DataSet ds = new DataSet();

            string qry = "  SELECT PNAME, Gender, Company_name, ich.`Consultant_Name`,ich.`DateOfAdmit`,ich.`DateOfDischarge` ";
            qry += " FROM patient_medical_history pip INNER JOIN patient_master pm ON pip.`Patient_ID`= pm.Patient_ID AND TYPE = 'IPD' ";
            qry += " INNER JOIN f_panel_master fpm ON fpm.panel_id = pip.`Panel_ID` ";
            qry += " LEFT OUTER JOIN IPD_CASE_HISTORY ICH ON ICH.Transaction_ID = PIP.Transaction_ID ";
            qry += " WHERE pip.ID =" + IPDNo + "";
  
            return ExecuteDataset(qry);
        }
        public dataSet GetPatientDetailByUHID(string UHID)
        {
            string processInfo = string.Empty;
            DataSet ds = new DataSet();
            //string qry = " SELECT PNAME, Gender FROM  patient_master WHERE Patient_ID='" + UHID + "' ";
            string qry = " SELECT Patient_ID, PNAME, Mobile, RelationName, Gender FROM  patient_master WHERE Patient_ID='" + UHID + "' "; //For CovidCertificate
            return ExecuteDataset(qry);
        }
        public void Sync_IPDDatafromHISToChandan(string IPDNo,string RefCode,string loginId)
        {
            string result = string.Empty;
            string processInfo = string.Empty;
            DataSet ds = new DataSet();
            string qry = string.Empty;
            qry += "select ltd.*,cm.CategoryID,cm.NAME CategoryName,sc.SubCategoryId,sc.NAME SubCatName,ltd.ItemId,fim.TypeName ItemName,CONCAT(dm.title,' ',dm.NAME) doctorName,pm. Company_Name PanelName,CONCAT(dr.title,' ',dr.NAME) ref_name from  ";
            qry += "( ";
            qry += " SELECT 'IPD' TnxType,pmh.ID IPOPNo,ltd.LedgerTnxId UniqId,ltd.EntryDate,adj.billDate,ltd.LedgerTransactionNo,ltd.Transaction_ID,ltd.Doctor_Id,ltd.ItemId,IFNULL(Rate,0) Rate,IFNULL((ltd.Rate*ltd.Quantity),0) GrossAmt,IFNULL(DiscAmt,0) DiscAmt ,  ";
            qry += "IFNULL(ROUND(ltd.Amount-(ltd.Amount * ROUND(IFNULL(adj.DiscountOnBill,0)*100/IFNULL((SELECT SUM(Amount) FROM f_ledgertnxdetail  WHERE transaction_ID=ltd.transaction_ID AND isverified<>2),0),4)*.01)),0) Amount,IFNULL(Quantity,0) Quantity  ";
            qry += " ,ISPackage,adj.PanelID,adj.Patient_ID,Pm.PName,pm.Mobile,pm.Age,pm.City,IFNULL(adj.TotalBilledAmt,0)TotalBilledAmt ,IFNULL(adj.DiscountOnBill,0) DiscountOnBill,IFNULL(ltd.DiscountPercentage,0) DiscountPercentage,adj.BillNo,pmh.referedById,ltd.TypeOfTnx  ";
            qry += " FROM f_ledgertnxdetail ltd INNER JOIN f_ipdadjustment adj ON adj.transaction_ID = ltd.Transaction_ID  ";
            qry += " INNER JOIN patient_medical_history pmh ON pmh.transaction_ID = ltd.Transaction_ID  ";
            qry += " INNER JOIN patient_master PM ON pm.Patient_ID = adj.Patient_ID  ";
            qry += " WHERE ltd.isverified = 1 AND adj.CentreID IN (1) AND pmh.ID=" + IPDNo + " ";
            qry += " ) ltd LEFT OUTER JOIN f_itemmaster fim ON fim.ItemID=ltd.ItemId  ";
            qry += "LEFT OUTER JOIN f_subcategorymaster sc ON fim.SubCategoryID=sc.SubCategoryID  ";
            qry += "LEFT OUTER JOIN f_categorymaster cm  ON cm.CategoryID = sc.CategoryID  ";
            qry += "LEFT OUTER JOIN doctor_master  dm ON dm.Doctor_Id = ltd.Doctor_Id  ";
            qry += "LEFT OUTER JOIN f_panel_master pm ON pm.panel_ID = ltd.PanelID ";
            qry += "LEFT OUTER JOIN doctor_referal dr ON dr.Doctor_ID=ltd.Doctor_Id";
            try
            {
                DBManager.QueryExecute("delete from HIS_RawData where IPOPNo='" + IPDNo + "' ", "ITDoseData");
                ds = ExecuteDataset(qry).ResultSet;
                if(ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    SqlConnection sqlcon =new SqlConnection(GlobalConfig.ConStr_HISByItDose);
                    try
                    {
                        sqlcon.Open();
                        foreach (DataRow dr in ds.Tables[0].Rows)
                        {
                            result = Insert_Modify_HIS_RawData(sqlcon, dr["TnxType"].ToString(), dr["UniqId"].ToString(), dr["IPOPNo"].ToString(), dr["entryDate"].ToString(), dr["billDate"].ToString(), dr["LedgerTransactionNo"].ToString(), dr["Transaction_ID"].ToString(), dr["Doctor_Id"].ToString(), dr["CategoryID"].ToString(), dr["CategoryName"].ToString(), dr["SubCategoryId"].ToString(), dr["SubCatName"].ToString(), dr["ItemId"].ToString(), dr["ItemName"].ToString(), Convert.ToDecimal(dr["Rate"]), Convert.ToDecimal(dr["GrossAmt"]), Convert.ToDecimal(dr["DiscAmt"]), Convert.ToDecimal(dr["Amount"]), Convert.ToDecimal(dr["Quantity"]), Convert.ToDecimal(dr["TotalBilledAmt"]), Convert.ToDecimal(dr["DiscountOnBill"]), Convert.ToDecimal(dr["DiscountPercentage"]), dr["BillNo"].ToString(), dr["referedById"].ToString(), dr["ref_name"].ToString(), dr["ISPackage"].ToString(), dr["PanelID"].ToString(), dr["Patient_ID"].ToString(), dr["PName"].ToString(), dr["Mobile"].ToString(), dr["Age"].ToString(), dr["City"].ToString(), dr["doctorName"].ToString(), dr["PanelName"].ToString(), dr["TypeOfTnx"].ToString(), "Re-Generate");
                        }
                        HIS_CalculateReferralCommission(sqlcon,IPDNo,loginId,"Calculate");
                    }
                    catch (Exception ex) { }
                    finally { sqlcon.Close(); };
                }
            }
            catch (Exception ex) { }
        }
        public string Insert_Modify_HIS_RawData(SqlConnection con, string TnxType, string UniqId, string IPOPNo, string entrydate, string billDate, string LedgerTransactionNo, string Transaction_ID, string Doctor_Id, string CategoryID, string CategoryName, string SubCategoryId, string SubCatName, string ItemId, string ItemName, decimal Rate, decimal GrossAmt, decimal DiscAmt, decimal Amount, decimal Quantity, decimal TotalBilledAmt, decimal DiscountOnBill, decimal DiscountPercentage, string BillNo, string referedById, string referedByName, string ISPackage, string PanelID, string Patient_ID, string PName, string Mobile, string Age, string City, string doctorName, string PanelName, string TypeOfTnx, string Logic)
        {
            string processInfo = string.Empty;
            SqlCommand cmd = new SqlCommand("Insert_Modify_HIS_RawData", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandTimeout = 2500;
            cmd.Parameters.Add("@TnxType", SqlDbType.VarChar, 30).Value = TnxType;
            cmd.Parameters.Add("@UniqId", SqlDbType.VarChar, 30).Value = UniqId;
            cmd.Parameters.Add("@IPOPNo", SqlDbType.VarChar, 30).Value = IPOPNo;
            cmd.Parameters.Add("@entrydate", SqlDbType.DateTime).Value = entrydate;
            cmd.Parameters.Add("@billDate", SqlDbType.DateTime).Value = billDate;
            cmd.Parameters.Add("@LedgerTransactionNo", SqlDbType.VarChar, 30).Value = LedgerTransactionNo;
            cmd.Parameters.Add("@Transaction_ID", SqlDbType.VarChar, 30).Value = Transaction_ID;
            cmd.Parameters.Add("@Doctor_Id", SqlDbType.VarChar, 30).Value = Doctor_Id;
            cmd.Parameters.Add("@CategoryID", SqlDbType.VarChar, 50).Value = CategoryID;
            cmd.Parameters.Add("@CategoryName", SqlDbType.VarChar, 200).Value = CategoryName;
            cmd.Parameters.Add("@SubCategoryId", SqlDbType.VarChar, 50).Value = SubCategoryId;
            cmd.Parameters.Add("@SubCatName", SqlDbType.VarChar, 300).Value = SubCatName;
            cmd.Parameters.Add("@ItemId", SqlDbType.VarChar, 50).Value = ItemId;
            cmd.Parameters.Add("@ItemName", SqlDbType.VarChar, 200).Value = ItemName;
            cmd.Parameters.Add("@Rate", SqlDbType.Decimal).Value = Rate;
            cmd.Parameters.Add("@GrossAmt", SqlDbType.Decimal).Value = GrossAmt;
            cmd.Parameters.Add("@DiscAmt", SqlDbType.Decimal).Value = DiscAmt;
            cmd.Parameters.Add("@Amount", SqlDbType.Decimal).Value = Amount;
            cmd.Parameters.Add("@Quantity", SqlDbType.Decimal).Value = Quantity;
            cmd.Parameters.Add("@TotalBilledAmt", SqlDbType.Decimal).Value = TotalBilledAmt;
            cmd.Parameters.Add("@DiscountOnBill", SqlDbType.Decimal).Value = DiscountOnBill;
            cmd.Parameters.Add("@DiscountPercentage", SqlDbType.Decimal).Value = DiscountPercentage;
            cmd.Parameters.Add("@BillNo", SqlDbType.VarChar).Value = BillNo;
            cmd.Parameters.Add("@referedById", SqlDbType.VarChar).Value = referedById;
            cmd.Parameters.Add("@referedByName", SqlDbType.VarChar).Value = referedByName;
            cmd.Parameters.Add("@ISPackage", SqlDbType.VarChar).Value = ISPackage;
            cmd.Parameters.Add("@PanelID", SqlDbType.VarChar, 30).Value = PanelID;
            cmd.Parameters.Add("@Patient_ID", SqlDbType.VarChar, 510).Value = Patient_ID;
            cmd.Parameters.Add("@PName", SqlDbType.VarChar, 510).Value = PName;
            cmd.Parameters.Add("@Mobile", SqlDbType.VarChar, 30).Value = Mobile;
            cmd.Parameters.Add("@Age", SqlDbType.VarChar, 30).Value = Age;
            cmd.Parameters.Add("@City", SqlDbType.VarChar, 100).Value = City;
            cmd.Parameters.Add("@doctorName", SqlDbType.VarChar, 100).Value = doctorName;
            cmd.Parameters.Add("@PanelName", SqlDbType.VarChar, 100).Value = PanelName;
            cmd.Parameters.Add("@TypeOfTnx", SqlDbType.VarChar, 100).Value = TypeOfTnx;
            cmd.Parameters.Add("@Logic", SqlDbType.VarChar, 20).Value = Logic;
            cmd.Parameters.Add("@result", SqlDbType.VarChar, 50).Value = "";
            cmd.Parameters["@result"].Direction = ParameterDirection.InputOutput;
            cmd.ExecuteNonQuery();
            processInfo = (string)cmd.Parameters["@result"].Value.ToString();
            return processInfo;
        }
        public string HIS_CalculateReferralCommission(SqlConnection con, string IPDNo,string LoginId,string Logic)
        {
            string processInfo = string.Empty;
            SqlCommand cmd = new SqlCommand("pHIS_CalculateReferralCommission", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandTimeout = 2500;
            cmd.Parameters.Add("@IPDNo", SqlDbType.VarChar,20).Value = IPDNo;
            cmd.Parameters.Add("@LoginId", SqlDbType.VarChar, 30).Value = LoginId;
            cmd.Parameters.Add("@Logic", SqlDbType.VarChar, 20).Value = Logic;
            cmd.Parameters.Add("@result", SqlDbType.VarChar, 100).Value = "";
            cmd.Parameters["@result"].Direction = ParameterDirection.InputOutput;
            cmd.ExecuteNonQuery();
            processInfo = (string)cmd.Parameters["@result"].Value.ToString();
            return processInfo;
        }

    }
}