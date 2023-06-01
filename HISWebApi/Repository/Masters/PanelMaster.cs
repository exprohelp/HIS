using HISWebApi.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace HISWebApi.Repository.Masters
{
    public class PanelMaster
    {
        public string mInsertUpdatePanelMaster(PanelMasterBO objBO)
        {
            dataSet dsObj = new dataSet();
            string processInfo = string.Empty;           
            using (SqlConnection con = new SqlConnection(GlobalConfig.ConStr_Hospital))
            {
                using (SqlCommand cmd = new SqlCommand("pm_PanelMasterInsertUpdate", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandTimeout = 2500;
                    cmd.Parameters.Add("@hospid", SqlDbType.VarChar, 20).Value = objBO.HospId;
                    cmd.Parameters.Add("@panelid", SqlDbType.VarChar, 20).Value = objBO.PanelId;
                    cmd.Parameters.Add("@panelcode", SqlDbType.VarChar, 20).Value = objBO.PanelCode;
                    cmd.Parameters.Add("@panelname", SqlDbType.VarChar, 300).Value = objBO.PanelName;
                    cmd.Parameters.Add("@paneltype", SqlDbType.VarChar, 50).Value = objBO.PanelType;
                    cmd.Parameters.Add("@iscredit", SqlDbType.VarChar, 15).Value = objBO.IsCredit;
                    cmd.Parameters.Add("@panelgroup", SqlDbType.VarChar, 15).Value = objBO.PanelGroup;
                    cmd.Parameters.Add("@panelgroupid", SqlDbType.VarChar, 15).Value = objBO.PanelGroupId;
                    cmd.Parameters.Add("@add1", SqlDbType.VarChar, 300).Value = objBO.Address1;
                    cmd.Parameters.Add("@add2", SqlDbType.VarChar, 300).Value = objBO.Address2;
                    cmd.Parameters.Add("@email", SqlDbType.VarChar, 100).Value = objBO.Email;
                    cmd.Parameters.Add("@phone", SqlDbType.VarChar, 20).Value = objBO.PhoneNo;
                    cmd.Parameters.Add("@mobile", SqlDbType.VarChar, 10).Value = objBO.ContactNo;
                    cmd.Parameters.Add("@contactperson", SqlDbType.VarChar, 100).Value = objBO.ContactPerson;
                    cmd.Parameters.Add("@faxno", SqlDbType.VarChar, 20).Value = objBO.Fax;

                    cmd.Parameters.Add("@datefrom", SqlDbType.Date).Value = objBO.ValidFrom;
                    cmd.Parameters.Add("@dateto", SqlDbType.Date).Value = objBO.ValidTo;
                    cmd.Parameters.Add("@paymentmode", SqlDbType.VarChar, 30).Value = objBO.PaymentMode;
                    cmd.Parameters.Add("@refcodeipd", SqlDbType.VarChar, 30).Value = objBO.ReferRateIpd;
                    cmd.Parameters.Add("@refcodeopd", SqlDbType.VarChar, 30).Value = objBO.ReferRateOpd;
                    cmd.Parameters.Add("@ratetypeipdopd", SqlDbType.VarChar, 30).Value = objBO.RateTypeIpdOpd;
                    cmd.Parameters.Add("@hiderate", SqlDbType.VarChar, 1).Value = objBO.HideRate;
                    cmd.Parameters.Add("@showprintout", SqlDbType.VarChar, 1).Value = objBO.ShowPrintOut;

                    cmd.Parameters.Add("@copaymenton", SqlDbType.BigInt).Value = objBO.CoPaymentOn;
                    cmd.Parameters.Add("@copaymentpercentage", SqlDbType.Decimal).Value = objBO.CoPaymentIn;
                    cmd.Parameters.Add("@currency", SqlDbType.VarChar, 20).Value = objBO.FollowCuurency;
                    cmd.Parameters.Add("@ispartialpay", SqlDbType.BigInt).Value = objBO.AllowPartialPay;
                    cmd.Parameters.Add("@creditlimit", SqlDbType.VarChar, 100).Value = objBO.CreditLimit;                    

                    cmd.Parameters.Add("@login_id", SqlDbType.VarChar, 15).Value = objBO.login_id;
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
        
    }
}