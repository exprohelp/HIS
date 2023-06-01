using HISWebApi.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace HISWebApi.Repository.EDP
{
    public class PanelRateItemLink
    {
        public string PanelRateListLinkInsertUpdate(PanelRateItemBO objBO)
        {
            dataSet dsObj = new dataSet();
            string processInfo = string.Empty;           
            using (SqlConnection con = new SqlConnection(GlobalConfig.ConStr_Hospital))
            {
                using (SqlCommand cmd = new SqlCommand("pPanelRateListLink", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandTimeout = 2500;                    
                    cmd.Parameters.Add("@Hosp_Id", SqlDbType.VarChar, 15).Value = objBO.hosp_id;
                    cmd.Parameters.Add("@PanelId", SqlDbType.VarChar, 15).Value = objBO.PanelId;
                    cmd.Parameters.Add("@RateListId", SqlDbType.VarChar, 15).Value = objBO.RateListId;
                    cmd.Parameters.Add("@Srno", SqlDbType.VarChar, 15).Value = objBO.Srno;
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