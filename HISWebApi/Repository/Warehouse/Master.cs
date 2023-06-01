using HISWebApi.Models;
using System.Data;
using System.Data.SqlClient;
using static HISWebApi.Models.Warehouse;

namespace HISWebApi.Repository.Warehouse
{
    public class Master
    {
        public dataSet MasterQueries(ManufacturerBO objBO)
        {
            dataSet dsObj = new dataSet();
            using (SqlConnection con = new SqlConnection(GlobalConfig.ConStr_Hospital))
            {
                using (SqlCommand cmd = new SqlCommand("pwh_MasterQueries", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandTimeout = 2500;
                    cmd.Parameters.Add("@hosp_id", SqlDbType.VarChar, 10).Value = objBO.hosp_id;
                    cmd.Parameters.Add("@mfd_id", SqlDbType.VarChar, 7).Value = objBO.mfd_id;
                    cmd.Parameters.Add("@vendor_id", SqlDbType.VarChar, 10).Value = objBO.vendor_id;
                    cmd.Parameters.Add("@mfd_name", SqlDbType.VarChar, 50).Value = objBO.mfd_name;
					cmd.Parameters.Add("@MainCategory", SqlDbType.VarChar, 50).Value = objBO.MainCategory;
					cmd.Parameters.Add("@item_name", SqlDbType.VarChar, 50).Value = objBO.prm_1;
                    cmd.Parameters.Add("@prm_1", SqlDbType.VarChar, 50).Value = objBO.prm_1;
                    cmd.Parameters.Add("@contact_no", SqlDbType.VarChar, 12).Value = objBO.contact_no;
                    cmd.Parameters.Add("@address", SqlDbType.VarChar, 100).Value = objBO.address;
                    cmd.Parameters.Add("@country_id", SqlDbType.Int).Value = objBO.country_id;
                    cmd.Parameters.Add("@state_code", SqlDbType.Int).Value = objBO.state_id;               
                    cmd.Parameters.Add("@remark", SqlDbType.VarChar, 100).Value = objBO.remark;
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
        public string InsertUpdateManufacturer(ManufacturerBO objBO)
        {
            string processInfo = string.Empty;
            using (SqlConnection con = new SqlConnection(GlobalConfig.ConStr_Hospital))
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand("pwh_InsertUpdateManufacturer", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandTimeout = 2500;
                    cmd.Parameters.Add("@hosp_id", SqlDbType.VarChar, 10).Value = objBO.hosp_id;
                    cmd.Parameters.Add("@mfd_id", SqlDbType.VarChar, 7).Value = objBO.mfd_id;
                    cmd.Parameters.Add("@mfd_name", SqlDbType.VarChar, 50).Value = objBO.mfd_name;
                    cmd.Parameters.Add("@contact_no", SqlDbType.VarChar, 12).Value = objBO.contact_no;
                    cmd.Parameters.Add("@address", SqlDbType.VarChar, 100).Value = objBO.address;
                    cmd.Parameters.Add("@remark", SqlDbType.VarChar, 100).Value = objBO.remark;
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
        public string InsertUpdateCategory(CategoryBO objBO)
        {
            string processInfo = string.Empty;
            using (SqlConnection con = new SqlConnection(GlobalConfig.ConStr_Hospital))
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand("pwh_InsertUpdateCategory", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandTimeout = 2500;
                    cmd.Parameters.Add("@hosp_id", SqlDbType.VarChar, 10).Value = objBO.hosp_id;
                    cmd.Parameters.Add("@CategoryId", SqlDbType.VarChar, 7).Value = objBO.CategoryId;
                    cmd.Parameters.Add("@MainCategoryName", SqlDbType.VarChar, 50).Value = objBO.MainCategory;
                    cmd.Parameters.Add("@CategoryName", SqlDbType.VarChar, 50).Value = objBO.CategoryName;
                    cmd.Parameters.Add("@login_id", SqlDbType.VarChar, 10).Value = objBO.login_id;
                    cmd.Parameters.Add("@IsActive", SqlDbType.Bit, 10).Value = objBO.IsActive;
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
        public string InsertUpdateItemMaster(ItemMasterBO objBO)
        {
            string processInfo = string.Empty;
            using (SqlConnection con = new SqlConnection(GlobalConfig.ConStr_Hospital))
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand("pwh_InsertUpdateItemMaster", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandTimeout = 2500;
                    cmd.Parameters.Add("@hosp_id", SqlDbType.VarChar, 10).Value = objBO.hosp_id;
                    cmd.Parameters.Add("@hsn", SqlDbType.VarChar, 8).Value = objBO.hsn;             
                    cmd.Parameters.Add("@item_type", SqlDbType.VarChar, 10).Value = objBO.ItemType;
                    cmd.Parameters.Add("@item_id", SqlDbType.VarChar, 10).Value = objBO.ItemId;
                    cmd.Parameters.Add("@item_name", SqlDbType.VarChar, 200).Value = objBO.ItemName;
					cmd.Parameters.Add("@purchase_flag", SqlDbType.VarChar, 10).Value = objBO.purchase_flag;
			        cmd.Parameters.Add("@CategoryId", SqlDbType.VarChar, 30).Value = objBO.CategoryId;
                    cmd.Parameters.Add("@rol", SqlDbType.Int).Value = objBO.rol;
                    cmd.Parameters.Add("@MOQ", SqlDbType.Int).Value = objBO.MOQ;
                    cmd.Parameters.Add("@remark", SqlDbType.VarChar, 200).Value = objBO.Remark;
                    cmd.Parameters.Add("@status_flag", SqlDbType.Char, 1).Value = objBO.StatusFlag;
                    cmd.Parameters.Add("@login_id", SqlDbType.VarChar, 10).Value = objBO.login_id;
                    cmd.Parameters.Add("@ShelfNo", SqlDbType.VarChar, 20).Value = objBO.ShelfNo;
                    cmd.Parameters.Add("@Logic", SqlDbType.VarChar, 50).Value = objBO.Logic;
                    cmd.Parameters.Add("@result", SqlDbType.VarChar, 100).Value = "";
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
        public string InsertUpdatePackType(PackTypeBO objBO)
        {
            string processInfo = string.Empty;
            using (SqlConnection con = new SqlConnection(GlobalConfig.ConStr_Hospital))
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand("pwh_InsertUpdatePackType", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandTimeout = 2500;
                    cmd.Parameters.Add("@hosp_id", SqlDbType.VarChar, 10).Value = objBO.hosp_id;
                    cmd.Parameters.Add("@autoid", SqlDbType.Int).Value = objBO.autoid;
                    cmd.Parameters.Add("@pack_type", SqlDbType.VarChar, 50).Value = objBO.pack_type;
                    cmd.Parameters.Add("@pack_qty", SqlDbType.VarChar, 10).Value = objBO.pack_qty;
                    cmd.Parameters.Add("@login_id", SqlDbType.VarChar, 10).Value = objBO.login_id;                
                    cmd.Parameters.Add("@IsActive", SqlDbType.Bit, 10).Value = objBO.IsActive;
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
		public string InsertUpdateWhCart(WhCartBO objBO)
		{
			string processInfo = string.Empty;
			using (SqlConnection con = new SqlConnection(GlobalConfig.ConStr_Hospital))
			{
				con.Open();
				using (SqlCommand cmd = new SqlCommand("pwh_InsertUpdateCart", con))
				{
					cmd.CommandType = CommandType.StoredProcedure;
					cmd.CommandTimeout = 2500;
					cmd.Parameters.Add("@autoId", SqlDbType.BigInt).Value = objBO.autoId == 0 ? 0 : objBO.autoId;
					cmd.Parameters.Add("@hospId", SqlDbType.VarChar, 15).Value = objBO.hosp_id;
					cmd.Parameters.Add("@DeptId", SqlDbType.VarChar, 50).Value = objBO.DeptId;
					cmd.Parameters.Add("@Carttype", SqlDbType.VarChar, 50).Value = objBO.CartType;
					cmd.Parameters.Add("@CartName ", SqlDbType.VarChar, 50).Value = objBO.CartName;
					cmd.Parameters.Add("@LoginId", SqlDbType.VarChar, 15).Value = objBO.login_id;
					cmd.Parameters.Add("@Logic", SqlDbType.VarChar, 50).Value = objBO.Logic;
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
					return processInfo;
				}
			}
		}
		public string InsertUpdateVendorMaster(VendorMasterBO objBO)
        {
            string processInfo = string.Empty;
            using (SqlConnection con = new SqlConnection(GlobalConfig.ConStr_Hospital))
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand("pwh_InsertUpdateVendorMaster", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandTimeout = 2500;
                    cmd.Parameters.Add("@vendor_id", SqlDbType.VarChar, 10).Value = objBO.vendor_id;
                    cmd.Parameters.Add("@hosp_id", SqlDbType.VarChar, 8).Value = objBO.hosp_id;
                    cmd.Parameters.Add("@vendor_name", SqlDbType.VarChar, 50).Value = objBO.vendor_name;
                    cmd.Parameters.Add("@contact_person", SqlDbType.VarChar, 50).Value = objBO.contact_person;
                    cmd.Parameters.Add("@address1", SqlDbType.VarChar, 50).Value = objBO.address1;
                    cmd.Parameters.Add("@address2", SqlDbType.VarChar, 50).Value = objBO.address2;
                    cmd.Parameters.Add("@address3", SqlDbType.VarChar, 50).Value = objBO.address3;
                    cmd.Parameters.Add("@city", SqlDbType.VarChar, 50).Value = objBO.city;
                    cmd.Parameters.Add("@state", SqlDbType.VarChar, 50).Value = objBO.state;
                    cmd.Parameters.Add("@country", SqlDbType.VarChar, 50).Value = objBO.country;
                    cmd.Parameters.Add("@pin", SqlDbType.VarChar, 8).Value = objBO.pin;
                    cmd.Parameters.Add("@ledgerid", SqlDbType.VarChar, 16).Value = objBO.ledgerid;
                    cmd.Parameters.Add("@contact_no", SqlDbType.VarChar, 20).Value = objBO.contact_no;
                    cmd.Parameters.Add("@email", SqlDbType.VarChar, 50).Value = objBO.email;
                    cmd.Parameters.Add("@payment_mode", SqlDbType.VarChar, 20).Value = objBO.payment_mode;
                    cmd.Parameters.Add("@payment_days", SqlDbType.Int, 5).Value = objBO.payment_days;
                    cmd.Parameters.Add("@gst_no", SqlDbType.VarChar, 30).Value = objBO.gst_no;
                    cmd.Parameters.Add("@drug_lic_no", SqlDbType.VarChar, 30).Value = objBO.drug_lic_no;
                    cmd.Parameters.Add("@notes", SqlDbType.VarChar, 250).Value = objBO.notes;
                    cmd.Parameters.Add("@login_id", SqlDbType.VarChar, 10).Value = objBO.login_id;
                    cmd.Parameters.Add("@active_flag", SqlDbType.VarChar, 1).Value = objBO.active_flag;
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
        public string InsertUpdateWhDepartment(WhDepartmentBO objBO)
        {
            string processInfo = string.Empty;
            using (SqlConnection con = new SqlConnection(GlobalConfig.ConStr_Hospital))
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand("pwh_InsertUpdateDepartment", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandTimeout = 2500;
                    cmd.Parameters.Add("@autoId", SqlDbType.BigInt).Value = objBO.autoId == 0 ? 0 : objBO.autoId;
                    cmd.Parameters.Add("@hospId", SqlDbType.VarChar, 15).Value = objBO.hosp_id;
                    cmd.Parameters.Add("@DeptName", SqlDbType.VarChar, 100).Value = objBO.DeptName;
                    cmd.Parameters.Add("@LoginId", SqlDbType.VarChar, 15).Value = objBO.login_id;
                    cmd.Parameters.Add("@Logic", SqlDbType.VarChar, 50).Value = objBO.Logic;
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
                    return processInfo;
                }
            }
        }
		public string wh_LinkPackManufacturerToItem(ItemMasterBO objBO)
		{
			string processInfo = string.Empty;
			using (SqlConnection con = new SqlConnection(GlobalConfig.ConStr_Hospital))
			{
				con.Open();
				using (SqlCommand cmd = new SqlCommand("pwh_LinkPackManufacturerToItem", con))
				{
					cmd.CommandType = CommandType.StoredProcedure;
					cmd.CommandTimeout = 2500;
					cmd.Parameters.Add("@ItemId", SqlDbType.VarChar, 10).Value = objBO.ItemId;
					cmd.Parameters.Add("@pack_id", SqlDbType.VarChar, 7).Value = objBO.pack_id;
					cmd.Parameters.Add("@mfd_id", SqlDbType.VarChar, 50).Value = objBO.MfdId;				
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
		public string InsertUpdateEmpAssignToCart(WhEmpAssignEmpToCart objBO)
		{
			string processInfo = string.Empty;
			using (SqlConnection con = new SqlConnection(GlobalConfig.ConStr_Hospital))
			{
				con.Open();			
				using (SqlCommand cmd = new SqlCommand("pwh_CartLinkToEmployee", con))
				{
					cmd.CommandType = CommandType.StoredProcedure;
					cmd.CommandTimeout = 2500;
					cmd.Parameters.Add("@autoId", SqlDbType.BigInt).Value = objBO.autoId == 0 ? 0 : objBO.autoId;
					cmd.Parameters.Add("@hospId", SqlDbType.VarChar, 15).Value = objBO.hosp_id;
					cmd.Parameters.Add("@EmpCode", SqlDbType.VarChar, 15).Value = objBO.EmpCode;
					cmd.Parameters.Add("@cartIds", SqlDbType.VarChar).Value = objBO.CartIds;
					cmd.Parameters.Add("@LoginId", SqlDbType.VarChar, 15).Value = objBO.login_id;
					cmd.Parameters.Add("@Logic", SqlDbType.VarChar, 50).Value = objBO.Logic;
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
					return processInfo;
				}
			}
		}
		public string DeleteWharehouseDepartment(WhDepartmentBO objBO)
		{
			string processInfo = string.Empty;
			using (SqlConnection con = new SqlConnection(GlobalConfig.ConStr_Hospital))
			{
				con.Open();
				using (SqlCommand cmd = new SqlCommand("pwh_InsertUpdateDepartment", con))
				{
					cmd.CommandType = CommandType.StoredProcedure;
					cmd.CommandTimeout = 2500;
					cmd.Parameters.Add("@autoId", SqlDbType.BigInt).Value = objBO.autoId == 0 ? 0 : objBO.autoId;
					cmd.Parameters.Add("@hospId", SqlDbType.VarChar, 8).Value = objBO.hosp_id;
					cmd.Parameters.Add("@DeptName", SqlDbType.VarChar, 8).Value = objBO.DeptName;
					cmd.Parameters.Add("@LoginId", SqlDbType.VarChar, 15).Value = objBO.login_id;
					cmd.Parameters.Add("@Logic", SqlDbType.VarChar, 50).Value = objBO.Logic;
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
					return processInfo;
				}
			}
		}		
		public string DeleteWharehouseCart(WhCartBO objBO)
		{
			string processInfo = string.Empty;
			using (SqlConnection con = new SqlConnection(GlobalConfig.ConStr_Hospital))
			{
				con.Open();
				using (SqlCommand cmd = new SqlCommand("pwh_InsertUpdateCart", con))
				{
					cmd.CommandType = CommandType.StoredProcedure;
					cmd.CommandTimeout = 2500;
					cmd.Parameters.Add("@autoId", SqlDbType.BigInt).Value = objBO.autoId == 0 ? 0 : objBO.autoId;
					cmd.Parameters.Add("@hospId", SqlDbType.VarChar, 8).Value = objBO.hosp_id;
					cmd.Parameters.Add("@DeptId", SqlDbType.VarChar, 8).Value = objBO.DeptId;
					cmd.Parameters.Add("@Carttype", SqlDbType.VarChar, 8).Value = objBO.CartType;
					cmd.Parameters.Add("@CartName", SqlDbType.VarChar, 8).Value = objBO.CartName;
					cmd.Parameters.Add("@LoginId", SqlDbType.VarChar, 15).Value = objBO.login_id;
					cmd.Parameters.Add("@Logic", SqlDbType.VarChar, 50).Value = objBO.Logic;
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
					return processInfo;
				}
			}
		}
        public string wh_VendorApprove(VendorApprove objBO)
        {
            string processInfo = string.Empty;
            using (SqlConnection con = new SqlConnection(GlobalConfig.ConStr_Hospital))
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand("pwh_VendorApprove", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandTimeout = 2500;
                    cmd.Parameters.Add("@ledgerID", SqlDbType.VarChar, 16).Value = objBO.ledgerID;
                    cmd.Parameters.Add("@vendor_id", SqlDbType.VarChar, 10).Value = objBO.vendor_id;
                    cmd.Parameters.Add("@vendor_name", SqlDbType.VarChar, 100).Value = objBO.vendor_name;
                    cmd.Parameters.Add("@login_id", SqlDbType.VarChar, 100).Value = objBO.login_id;
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
    }
}