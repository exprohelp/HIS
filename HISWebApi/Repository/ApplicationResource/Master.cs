using HISWebApi.Models;
using System.Data;
using System.Data.SqlClient;

namespace HISWebApi.Repository.Inventory
{
    public class Master
    {
        public dataSet MasterQueries(MasterDetails objBO)
        {
            string processInfo = string.Empty;
            dataSet dsObj = new dataSet();
            using (SqlConnection con = new SqlConnection(GlobalConfig.ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("pMenu_MasterQueries", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandTimeout = 2500;
                    cmd.Parameters.AddWithValue("@hosp_id", objBO.LoginId);
                    cmd.Parameters.AddWithValue("@role_id", objBO.RoleId);
                    cmd.Parameters.AddWithValue("@role_name", objBO.RoleName);
                    cmd.Parameters.AddWithValue("@menu_id", objBO.MenuId);
                    cmd.Parameters.AddWithValue("@menu_name", objBO.MenuName);
                    cmd.Parameters.AddWithValue("@sub_menu_id", objBO.SubMenuId);
                    cmd.Parameters.AddWithValue("@sub_menu_name", objBO.SubMenuName);
                    cmd.Parameters.AddWithValue("@sub_menu_link", objBO.SubMenuUrl);
                    cmd.Parameters.AddWithValue("@sequence_no", objBO.SequenceNo);
                    cmd.Parameters.AddWithValue("@used_for", objBO.UsedFor);
                    cmd.Parameters.AddWithValue("@menu_seq", objBO.MenuSequence);
                    cmd.Parameters.AddWithValue("@state_code", objBO.StateCode);
                    cmd.Parameters.AddWithValue("@state_name", objBO.StateName);
                    cmd.Parameters.AddWithValue("@city_code", objBO.CityCode);
                    cmd.Parameters.AddWithValue("@city_name", objBO.CityName);
                    cmd.Parameters.AddWithValue("@desig_code", objBO.DesigCode);
                    cmd.Parameters.AddWithValue("@desig_name", objBO.DesigName);
                    cmd.Parameters.AddWithValue("@emp_id", objBO.emp_id);
                    cmd.Parameters.AddWithValue("@emp_code", objBO.EmployeeCode);
                    cmd.Parameters.AddWithValue("@title", objBO.Title);
                    cmd.Parameters.AddWithValue("@emp_name", objBO.EmployeeName);
                    cmd.Parameters.AddWithValue("@gender", objBO.Gender);
                    cmd.Parameters.AddWithValue("@marital_status", objBO.MaritalStatus);
                    cmd.Parameters.AddWithValue("@mobile_no", objBO.MobileNo);
                    cmd.Parameters.AddWithValue("@loc_address", objBO.LocAddress);
                    cmd.Parameters.AddWithValue("@loc_locality", objBO.LocLocality);
                    cmd.Parameters.AddWithValue("@loc_city", objBO.LocCity);
                    cmd.Parameters.AddWithValue("@loc_state", objBO.LocState);
                    cmd.Parameters.AddWithValue("@perm_address", objBO.PerAddress);
                    cmd.Parameters.AddWithValue("@perm_locality", objBO.PerLocality);
                    cmd.Parameters.AddWithValue("@perm_city", objBO.PerCity);
                    cmd.Parameters.AddWithValue("@perm_state", objBO.PerState);
                    cmd.Parameters.AddWithValue("@emp_father_name", objBO.FatherName);
                    cmd.Parameters.AddWithValue("@emp_mother_name", objBO.MotherName);
                    cmd.Parameters.AddWithValue("@husband_name", objBO.HusbandWife);
                    cmd.Parameters.AddWithValue("@qualification", objBO.Qualification);
                    cmd.Parameters.AddWithValue("@experience", objBO.Experience);
                    cmd.Parameters.AddWithValue("@designation", objBO.Designation);
                    cmd.Parameters.AddWithValue("@blood_group", objBO.BloodGroup);
                    cmd.Parameters.AddWithValue("@aadhar_no", objBO.AadharNo);
                    cmd.Parameters.AddWithValue("@user_code", objBO.UserCode);
                    cmd.Parameters.AddWithValue("@user_type", objBO.UserType);
                    cmd.Parameters.AddWithValue("@netpassword", objBO.Password);
                    cmd.Parameters.AddWithValue("@flag", objBO.Flag);
                    cmd.Parameters.AddWithValue("@login_id", objBO.Loginid);
                    cmd.Parameters.AddWithValue("@Logic", objBO.Logic);
                    cmd.Parameters.Add("@result", SqlDbType.VarChar, 500).Value = "";
                    cmd.Parameters["@result"].Direction = ParameterDirection.InputOutput;
                    try
                    {
                        con.Open();
                        DataSet ds = new DataSet();
                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        da.Fill(ds);
                        dsObj.ResultSet = ds;
                        dsObj.Msg = (string)cmd.Parameters["@result"].Value.ToString();
                    }
                    catch (SqlException sqlEx)
                    {
                        dsObj.ResultSet = null;
                        dsObj.Msg = "Error Found   : " + sqlEx.Message;
                    }
                    finally { con.Close(); }
                    return dsObj;
                }
            }
        }
        public string InsertModifyMasterDetails(MasterDetails objBO)
        {
            string processInfo = string.Empty;
            using (SqlConnection con = new SqlConnection(GlobalConfig.ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("pMenu_InsertModifyMasterDetails", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandTimeout = 2500;
                    cmd.Parameters.AddWithValue("@ID", objBO.ID);
                    cmd.Parameters.AddWithValue("@hosp_id", objBO.LoginId);
                    cmd.Parameters.AddWithValue("@role_id", objBO.RoleId);
                    cmd.Parameters.AddWithValue("@role_name", objBO.RoleName);
                    cmd.Parameters.AddWithValue("@menu_id", objBO.MenuId);
                    cmd.Parameters.AddWithValue("@menu_name", objBO.MenuName);
                    cmd.Parameters.AddWithValue("@sub_menu_id", objBO.SubMenuId);
                    cmd.Parameters.AddWithValue("@sub_menu_name", objBO.SubMenuName);
                    cmd.Parameters.AddWithValue("@sub_menu_link", objBO.SubMenuUrl);
                    cmd.Parameters.AddWithValue("@sequence_no", objBO.SequenceNo);
                    cmd.Parameters.AddWithValue("@used_for", objBO.UsedFor);
                    cmd.Parameters.AddWithValue("@menu_seq", objBO.MenuSequence);
                    cmd.Parameters.AddWithValue("@state_code", objBO.StateCode);
                    cmd.Parameters.AddWithValue("@state_name", objBO.StateName);
                    cmd.Parameters.AddWithValue("@city_code", objBO.CityCode);
                    cmd.Parameters.AddWithValue("@city_name", objBO.CityName);
                    cmd.Parameters.AddWithValue("@desig_code", objBO.DesigCode);
                    cmd.Parameters.AddWithValue("@desig_name", objBO.DesigName);
                    cmd.Parameters.AddWithValue("@emp_id", objBO.emp_id);
                    cmd.Parameters.AddWithValue("@emp_code", objBO.EmployeeCode);
                    cmd.Parameters.AddWithValue("@title", objBO.Title);
                    cmd.Parameters.AddWithValue("@emp_name", objBO.EmployeeName);
                    cmd.Parameters.AddWithValue("@gender", objBO.Gender);
                    cmd.Parameters.AddWithValue("@marital_status", objBO.MaritalStatus);
                    cmd.Parameters.AddWithValue("@d_o_b", objBO.DOB);
                    cmd.Parameters.AddWithValue("@mobile_no", objBO.MobileNo);
                    cmd.Parameters.AddWithValue("@loc_address", objBO.LocAddress);
                    cmd.Parameters.AddWithValue("@loc_locality", objBO.LocLocality);
                    cmd.Parameters.AddWithValue("@loc_city", objBO.LocCity);
                    cmd.Parameters.AddWithValue("@loc_state", objBO.LocState);
                    cmd.Parameters.AddWithValue("@perm_address", objBO.PerAddress);
                    cmd.Parameters.AddWithValue("@perm_locality", objBO.PerLocality);
                    cmd.Parameters.AddWithValue("@perm_city", objBO.PerCity);
                    cmd.Parameters.AddWithValue("@perm_state", objBO.PerState);
                    cmd.Parameters.AddWithValue("@emp_father_name", objBO.FatherName);
                    cmd.Parameters.AddWithValue("@emp_mother_name", objBO.MotherName);
                    cmd.Parameters.AddWithValue("@husband_name", objBO.HusbandWife);
                    cmd.Parameters.AddWithValue("@qualification", objBO.Qualification);
                    cmd.Parameters.AddWithValue("@experience", objBO.Experience);
                    cmd.Parameters.AddWithValue("@designation", objBO.Designation);
                    cmd.Parameters.AddWithValue("@blood_group", objBO.BloodGroup);
                    cmd.Parameters.AddWithValue("@aadhar_no", objBO.AadharNo);
                    cmd.Parameters.AddWithValue("@user_code", objBO.UserCode);
                    cmd.Parameters.AddWithValue("@user_type", objBO.UserType);
                    cmd.Parameters.AddWithValue("@netpassword", objBO.Password);
                    cmd.Parameters.AddWithValue("@cr_date", objBO.CreatedOn);
                    cmd.Parameters.AddWithValue("@flag", objBO.Flag);
                    cmd.Parameters.AddWithValue("@login_id", objBO.Loginid);
                    cmd.Parameters.AddWithValue("@Logic", objBO.Logic);
                    cmd.Parameters.Add("@result", SqlDbType.VarChar, 100).Value = "";
                    cmd.Parameters["@result"].Direction = ParameterDirection.InputOutput;
                    try
                    {
                        con.Open();
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