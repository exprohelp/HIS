using HISWebApi.Models;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace HISWebApi.Repository.EDP
{
    public class PanelRateItem
    {
        public dataSet PanelRateQueries(PanelRateItemBO objBO)
        {
            dataSet dsObj = new dataSet();
            using (SqlConnection con = new SqlConnection(GlobalConfig.ConStr_Hospital))
            {
                using (SqlCommand cmd = new SqlCommand("pPanel_RateQueries", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandTimeout = 2500;
                    cmd.Parameters.Add("@HospId", SqlDbType.VarChar, 15).Value = objBO.hosp_id;
                    cmd.Parameters.Add("@PanelId", SqlDbType.VarChar, 20).Value = objBO.PanelId;
                    cmd.Parameters.Add("@RateListId", SqlDbType.VarChar, 20).Value = objBO.RateListId;
                    cmd.Parameters.Add("@DoctorId", SqlDbType.VarChar, 20).Value = objBO.DoctorId;
                    cmd.Parameters.Add("@CatId", SqlDbType.VarChar, 20).Value = objBO.catid;
                    cmd.Parameters.Add("@Subcatid", SqlDbType.VarChar, 20).Value = objBO.subcatid;
                    cmd.Parameters.Add("@Itemid", SqlDbType.VarChar, 20).Value = objBO.ItemId;
                    cmd.Parameters.Add("@prm_1", SqlDbType.VarChar, 50).Value = objBO.prm_1;
                    cmd.Parameters.Add("@Logic", SqlDbType.VarChar, 50).Value = objBO.Logic;
                    cmd.Parameters.Add("@login_id", SqlDbType.VarChar, 10).Value = objBO.login_id;
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
        public string PanelItemRateInsertUpdate(List<PanelRateItemBO> objBO)
        {
            dataSet dsObj = new dataSet();
            string processInfo = string.Empty;
            string hosp_id = string.Empty;
            string login_id = string.Empty;
            string Logic = string.Empty;
            DataTable dt = new DataTable();
            dt.Columns.Add("RateListId", typeof(string));
            dt.Columns.Add("ItemId", typeof(string));
            dt.Columns.Add("Rate", typeof(decimal));
            dt.Columns.Add("ExtItemCode", typeof(string));
            dt.Columns.Add("ExtItemName", typeof(string));
            dt.Columns.Add("RoomBillingCategory", typeof(string));
            foreach (PanelRateItemBO obj in objBO)
            {
                hosp_id = obj.hosp_id;
                login_id = obj.login_id;
                Logic = obj.Logic;
                DataRow dr = dt.NewRow();
                dr["RateListId"] = obj.RateListId;
                dr["ItemId"] = obj.ItemId;
                dr["Rate"] = obj.rate;
                dr["ExtItemCode"] = obj.ItemCode;
                dr["ExtItemName"] = obj.ItemName;
                dr["RoomBillingCategory"] = obj.RoomBillCategory;
                dt.Rows.Add(dr);
            }
            using (SqlConnection con = new SqlConnection(GlobalConfig.ConStr_Hospital))
            {
                using (SqlCommand cmd = new SqlCommand("pPanelItemRateInsertUpdate", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandTimeout = 2500;
                    cmd.Parameters.AddWithValue("@PanelRateItemdt", dt);
                    cmd.Parameters.Add("@Hosp_Id", SqlDbType.VarChar, 15).Value = hosp_id;
                    cmd.Parameters.Add("@login_id", SqlDbType.VarChar, 15).Value = login_id;
                    cmd.Parameters.Add("@Logic", SqlDbType.VarChar, 50).Value = Logic;
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
        //public string PanelItemRateInsertUpdate(PanelRateItemBO objBO)
        //{
        //    dataSet dsObj = new dataSet();
        //    string processInfo = string.Empty;
        //    DataTable RateItemListTable = JsonConvert.DeserializeObject<DataTable>(objBO.RateItemList);            
        //    //var duplicates = RateItemListTable.AsEnumerable().GroupBy(r => r[0]).Where(gr => gr.Count() > 1).ToList();           
        //    if (RateItemListTable.Rows.Count > 0)
        //    {
        //        objBO.RateItemListTable = RateItemListTable;
        //    }
        //    using (SqlConnection con = new SqlConnection(GlobalConfig.ConStr_Hospital))
        //    {
        //        using (SqlCommand cmd = new SqlCommand("pPanelItemRateInsertUpdate", con))
        //        {
        //            cmd.CommandType = CommandType.StoredProcedure;
        //            cmd.CommandTimeout = 2500;
        //            cmd.Parameters.AddWithValue("@PanelRateItemdt", RateItemListTable);
        //            cmd.Parameters.Add("@Hosp_Id", SqlDbType.VarChar, 15).Value = objBO.hosp_id;
        //            cmd.Parameters.Add("@login_id", SqlDbType.VarChar, 15).Value = objBO.login_id;
        //            cmd.Parameters.Add("@Logic", SqlDbType.VarChar, 50).Value = objBO.Logic;
        //            cmd.Parameters.Add("@result", SqlDbType.VarChar, 50).Value = "";
        //            cmd.Parameters["@result"].Direction = ParameterDirection.InputOutput;
        //            try
        //            {
        //                con.Open();
        //                cmd.ExecuteNonQuery();
        //                processInfo = (string)cmd.Parameters["@result"].Value.ToString();
        //                con.Close();
        //            }
        //            catch (SqlException sqlEx)
        //            {
        //                processInfo = "Error Found   : " + sqlEx.Message;
        //            }
        //            finally { con.Close(); }
        //            return processInfo;
        //        }
        //    }
        //}
    }
}