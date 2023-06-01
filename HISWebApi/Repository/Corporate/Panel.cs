using HISWebApi.Repository.IPD;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Net;
using System.Net.Sockets;
using static HISWebApi.Models.Corporate;

namespace HISWebApi.Repository.Corporate
{
    public class Panel
    {
        HISDBLayer his_layer = new HISDBLayer();

        public Models.dataSet GetPanel()
        {
            string _result = string.Empty;
            return his_layer.GetPanel();
        }
        public dataSet PanelQuerie(PanelInfo objBO)
        {
            dataSet dsObj = new dataSet();

            using (SqlConnection con = new SqlConnection(GlobalConfig.ConStr_Hospital))
            {
                using (SqlCommand cmd = new SqlCommand("pPanelQueries", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandTimeout = 2500;               
                    cmd.Parameters.Add("@PanelId", SqlDbType.VarChar, 20).Value = objBO.PanelId;
                    cmd.Parameters.Add("@UHID", SqlDbType.VarChar, 20).Value = objBO.UHID;
                    cmd.Parameters.Add("@from", SqlDbType.Date).Value = objBO.from;
                    cmd.Parameters.Add("@to", SqlDbType.Date).Value = objBO.to;
                    cmd.Parameters.Add("@Prm1", SqlDbType.VarChar, 100).Value = objBO.Prm1;
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
        public string InsertFundManagement(FundManagementInfo objBO)
        {
            string processInfo = string.Empty;
            using (SqlConnection con = new SqlConnection(GlobalConfig.ConStr_Hospital))
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand("pInsertFundManagement", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandTimeout = 2500;
                    cmd.Parameters.Add("@AutoId", SqlDbType.VarChar, 10).Value = objBO.AutoId;                  
                    cmd.Parameters.Add("@PanelId", SqlDbType.VarChar, 10).Value = objBO.PanelId;                  
                    cmd.Parameters.Add("@Amount", SqlDbType.Decimal).Value = objBO.Amount;
                    cmd.Parameters.Add("@RefDetail", SqlDbType.VarChar, 500).Value = objBO.RefDetail;
                    cmd.Parameters.Add("@RefNo", SqlDbType.VarChar, 20).Value = objBO.RefNo;
                    cmd.Parameters.Add("@UHID", SqlDbType.VarChar, 20).Value = objBO.UHID;
                    cmd.Parameters.Add("@UTRNo", SqlDbType.VarChar, 20).Value = objBO.UTRNo;
                    cmd.Parameters.Add("@UTRDate", SqlDbType.Date).Value = objBO.UTRDate;
                    cmd.Parameters.Add("@TnxType", SqlDbType.VarChar, 20).Value = objBO.TnxType;
                    cmd.Parameters.Add("@Prm1", SqlDbType.VarChar, 100).Value = objBO.Prm1;
                    cmd.Parameters.Add("@Prm2", SqlDbType.VarChar, 100).Value = objBO.Prm2;
                    cmd.Parameters.Add("@login_id", SqlDbType.VarChar, 10).Value = objBO.login_id;                  
                    cmd.Parameters.Add("@Logic", SqlDbType.VarChar, 50).Value = objBO.Logic;
                    cmd.Parameters.Add("@result", SqlDbType.VarChar, 200).Value = "";
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
                    return processInfo;
                }
            }
        }
        public dataSet PanelQueries(PanelItemExcludeBO objBO)
        {
            dataSet dsObj = new dataSet();

            using (SqlConnection con = new SqlConnection(GlobalConfig.ConStr_ChandanPharmacyLive))
            {
                using (SqlCommand cmd = new SqlCommand("pcorp_PanelQueries", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandTimeout = 2500;
                    cmd.Parameters.Add("@hosp_id", SqlDbType.VarChar, 10).Value = objBO.hosp_id;
                    cmd.Parameters.Add("@autoId", SqlDbType.Int, 10).Value = objBO.auto_id;
                    cmd.Parameters.Add("@panel_id", SqlDbType.VarChar, 10).Value = objBO.panel_id;
                    cmd.Parameters.Add("@item_id", SqlDbType.VarChar, 10).Value = objBO.item_id;
                    cmd.Parameters.Add("@login_id", SqlDbType.VarChar, 10).Value = objBO.login_id;
                    cmd.Parameters.Add("@IsActive", SqlDbType.Int, 1).Value = objBO.isActive;
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
        public string ExcludeItemForPanel(PanelItemExcludeBO objBO)
        {
            string processInfo = string.Empty;
            using (SqlConnection con = new SqlConnection(GlobalConfig.ConStr_ChandanPharmacyLive))
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand("pCorp_ExcludeItemForPanel", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandTimeout = 2500;
                    cmd.Parameters.Add("@hosp_id", SqlDbType.VarChar, 10).Value = objBO.hosp_id;
                    cmd.Parameters.Add("@autoId", SqlDbType.Int, 10).Value = objBO.auto_id;
                    cmd.Parameters.Add("@panel_id", SqlDbType.VarChar, 10).Value = objBO.panel_id;
                    cmd.Parameters.Add("@item_id", SqlDbType.VarChar, 10).Value = objBO.item_id;
                    cmd.Parameters.Add("@login_id", SqlDbType.VarChar, 10).Value = objBO.login_id;
                    cmd.Parameters.Add("@IsActive", SqlDbType.Int, 1).Value = objBO.isActive;
                    cmd.Parameters.Add("@Logic", SqlDbType.VarChar, 50).Value = objBO.Logic;
                    cmd.Parameters.Add("@result", SqlDbType.VarChar, 200).Value = "";
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
                    return processInfo;
                }
            }
        }
        public string PanelLink(PanelLinkBO objBO)
        {
            string processInfo = string.Empty;
            using (SqlConnection con = new SqlConnection(GlobalConfig.ConStr_ChandanPharmacyLive))
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand("pCorp_PanelLink", con))
                {
                    try
                    {
                        cmd.Parameters.Clear();
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandTimeout = 2500;
                        cmd.Parameters.Add("@auto_id", SqlDbType.Int, 10).Value = objBO.auto_id;
                        cmd.Parameters.Add("@master_panel_id", SqlDbType.VarChar, 10).Value = objBO.master_panel_id;
                        cmd.Parameters.Add("@link_panel_id", SqlDbType.VarChar, 10).Value = objBO.link_panel_id;
                        cmd.Parameters.Add("@login_id", SqlDbType.VarChar, 10).Value = objBO.login_id;
                        cmd.Parameters.Add("@active_flag", SqlDbType.VarChar, 1).Value = objBO.active_flag;
                        cmd.Parameters.Add("@Logic", SqlDbType.VarChar, 50).Value = objBO.Logic;
                        cmd.Parameters.Add("@result", SqlDbType.VarChar, 200).Value = "";
                        cmd.Parameters["@result"].Direction = ParameterDirection.InputOutput;
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
        public string PanelLinkUpdateStatus(PanelLinkBO objBO)
        {
            string processInfo = string.Empty;
            using (SqlConnection con = new SqlConnection(GlobalConfig.ConStr_ChandanPharmacyLive))
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand("pCorp_PanelLink", con))
                {
                    try
                    {
                        cmd.Parameters.Clear();
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandTimeout = 2500;
                        cmd.Parameters.Add("@auto_id", SqlDbType.Int, 10).Value = objBO.auto_id;
                        cmd.Parameters.Add("@active_flag", SqlDbType.VarChar, 1).Value = objBO.active_flag;
                        cmd.Parameters.Add("@Logic", SqlDbType.VarChar, 50).Value = objBO.Logic;
                        cmd.Parameters.Add("@result", SqlDbType.VarChar, 200).Value = "";
                        cmd.Parameters["@result"].Direction = ParameterDirection.InputOutput;
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
        //public string GetLocalIPAddress()
        //{
        //    var host = Dns.GetHostEntry(Dns.GetHostName());
        //    foreach (var ip in host.AddressList)
        //    {
        //        if (ip.AddressFamily == AddressFamily.InterNetwork)
        //        {
        //            return ip.ToString();
        //        }
        //    }
        //    throw new Exception("No network adapters with an IPv4 address in the system!");
        //}
    }
}