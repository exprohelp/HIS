using HISWebApi.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace HISWebApi.Repository.Inventory
{
    public class Menus
    {
        public dataSet MenuQueries(RoleMasterBO objBO)
        {
            string processInfo = string.Empty;
            dataSet dsObj = new dataSet();
            using (SqlConnection con = new SqlConnection(GlobalConfig.ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("pMenu_MenuQueries", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandTimeout = 2500;
                    cmd.Parameters.AddWithValue("@division", objBO.Division);
                    cmd.Parameters.AddWithValue("@unit_id", objBO.UnitId);
                    cmd.Parameters.AddWithValue("@emp_code", objBO.EmpCode);
                    cmd.Parameters.AddWithValue("@role_id", objBO.role_id);
                    cmd.Parameters.AddWithValue("@prm_1", objBO.Prm1);
                    cmd.Parameters.AddWithValue("@ID", objBO.ID);
                    cmd.Parameters.AddWithValue("@sub_menu_id", objBO.SubMenuId);
                    cmd.Parameters.AddWithValue("@login_id", objBO.login_id);
                    cmd.Parameters.AddWithValue("@Logic", objBO.Logic);
                    cmd.Parameters.Add("@result", SqlDbType.VarChar, 50).Value = "";
                    cmd.Parameters["@result"].Direction = ParameterDirection.InputOutput;
                    try
                    {
                        con.Open();
                        DataSet ds = new DataSet();
                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        da.Fill(ds);
                        dsObj.ResultSet = ds;
                        dsObj.Msg = "Success";
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
        public string InsertDeleteAllotMenu(RoleMasterBO objBO)
        {
            string processInfo = string.Empty;
            using (SqlConnection con = new SqlConnection(GlobalConfig.ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("pMenu_InsertModifyMenu", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandTimeout = 2500;
                    cmd.Parameters.AddWithValue("@unit_id", objBO.UnitId);
                    cmd.Parameters.AddWithValue("@emp_code", objBO.EmpCode);
                    cmd.Parameters.AddWithValue("@role_id", objBO.role_id);
                    cmd.Parameters.AddWithValue("@sub_menu_id", objBO.SubMenuId);
                    cmd.Parameters.AddWithValue("@Delete_sub_menu_id", objBO.SubMenuId);
                    cmd.Parameters.AddWithValue("@prm_1", objBO.Prm1);
                    cmd.Parameters.AddWithValue("@ID", objBO.ID);
                    cmd.Parameters.AddWithValue("@login_id", objBO.login_id);
                    cmd.Parameters.AddWithValue("@Logic", objBO.Logic);
                    cmd.Parameters.Add("@result", SqlDbType.VarChar, 50).Value = "";
                    cmd.Parameters["@result"].Direction = ParameterDirection.InputOutput;
                    try
                    {
                        con.Open();
                        int i = cmd.ExecuteNonQuery();
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
        public string AllotSubMenuToEmployee(List<RoleMasterBO> objBO)
        {
            string processInfo = string.Empty;
            if (objBO.Count > 0)
            {
                try
                {
                    DataTable dt = new DataTable();
                    dt.Columns.Add("emp_code", typeof(string));
                    dt.Columns.Add("sub_menu_id", typeof(string));
                    foreach (RoleMasterBO obj in objBO)
                    {
                        DataRow dr = dt.NewRow();
                        dr["emp_code"] = obj.EmpCode;
                        dr["sub_menu_id"] = obj.SubMenuId;
                        dt.Rows.Add(dr);
                    }
                    using (SqlConnection con = new SqlConnection(GlobalConfig.ConnectionString))
                    {
                        using (SqlCommand cmd = new SqlCommand("pMenu_InsertModifyMenu", con))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.CommandTimeout = 2500;
                            cmd.Parameters.AddWithValue("@AllotMenu", dt);
                            cmd.Parameters.AddWithValue("@Logic", "AllotSubMenuToEmployee");
                            cmd.Parameters.Add("@result", SqlDbType.VarChar, 50).Value = "";
                            cmd.Parameters["@result"].Direction = ParameterDirection.InputOutput;
                            try
                            {
                                con.Open();
                                int i = cmd.ExecuteNonQuery();
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
                catch (Exception ex) { processInfo = ex.Message; }
            }
            return processInfo;
        }
        public string AssignUnitToEmp(List<RoleMasterBO> objBO)
        {
            string processInfo = string.Empty;
            if (objBO.Count > 0)
            {
                try
                {
                    DataTable dt = new DataTable();
                    dt.Columns.Add("emp_code", typeof(string));
                    dt.Columns.Add("unit_id", typeof(string));
                    dt.Columns.Add("login_id", typeof(string));
                    foreach (RoleMasterBO obj in objBO)
                    {
                        DataRow dr = dt.NewRow();
                        dr["emp_code"] = obj.EmpCode;
                        dr["unit_id"] = obj.UnitId;
                        dr["login_id"] = obj.login_id;
                        dt.Rows.Add(dr);
                    }
                    using (SqlConnection con = new SqlConnection(GlobalConfig.ConnectionString))
                    {
                        using (SqlCommand cmd = new SqlCommand("pMenu_InsertModifyMenu", con))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.CommandTimeout = 2500;
                            cmd.Parameters.AddWithValue("@UnitTable", dt);
                            cmd.Parameters.AddWithValue("@Logic", "InsertUnit");
                            cmd.Parameters.Add("@result", SqlDbType.VarChar, 50).Value = "";
                            cmd.Parameters["@result"].Direction = ParameterDirection.InputOutput;
                            try
                            {
                                con.Open();
                                int i = cmd.ExecuteNonQuery();
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
                catch (Exception ex) { processInfo = ex.Message; }
            }
            return processInfo;
        }
        //Menu And Sub Menu Before Allotment
        public List<MenuBO> GetMenuByRoll(MenuBO objBO)
        {
            List<MenuBO> list = new List<MenuBO>();
            Connection con = new Connection();
            con.Open();
            con.cmd.CommandText = "pMenu_MenuQueries";
            con.cmd.CommandType = CommandType.StoredProcedure;
            con.cmd.Parameters.AddWithValue("@role_id", (object)objBO.RoleId ?? DBNull.Value);
            con.cmd.Parameters.AddWithValue("@Logic", objBO.Logic);
            con.cmd.Parameters.Add("@result", SqlDbType.VarChar, 50).Value = "";
            con.cmd.Parameters["@result"].Direction = ParameterDirection.InputOutput;
            SqlDataReader dr = con.cmd.ExecuteReader();
            while (dr.Read())
            {
                var menu = new MenuBO();
                menu.MenuId = dr["menu_id"].ToString();
                menu.MenuName = dr["menu_name"].ToString();
                menu.SubMenu = GetSubMenuByMenu(objBO.RoleId, menu.MenuId);
                list.Add(menu);
            }
            return list;
        }
        public List<SubMenuBO> GetSubMenuByMenu(string RoleId, string MenuId)
        {
            List<SubMenuBO> list = new List<SubMenuBO>();
            Connection con = new Connection();
            con.Open();
            con.cmd.CommandText = "pMenu_MenuQueries";
            con.cmd.CommandType = CommandType.StoredProcedure;
            con.cmd.Parameters.AddWithValue("@role_id", RoleId);
            con.cmd.Parameters.AddWithValue("@menu_id", MenuId);
            con.cmd.Parameters.AddWithValue("@Logic", "GetSubMenuByMenuAndRoll");
            con.cmd.Parameters.Add("@result", SqlDbType.VarChar, 50).Value = "";
            con.cmd.Parameters["@result"].Direction = ParameterDirection.InputOutput;
            SqlDataReader dr = con.cmd.ExecuteReader();
            while (dr.Read())
            {
                var subMenu = new SubMenuBO();
                subMenu.MenuId = dr["menu_id"].ToString();
                subMenu.SubMenuId = dr["sub_menu_id"].ToString();
                subMenu.SubMenuName = dr["sub_menu_name"].ToString();
                subMenu.SubMenuLink = dr["sub_menu_link"].ToString();
                list.Add(subMenu);
            }
            return list;
        }
        //Menu And Sub Menu After Allotment
        public List<MenuBO> GetAllotedMenuByEmpCode(MenuBO objBO)
        {
            List<MenuBO> list = new List<MenuBO>();
            Connection con = new Connection();
            con.Open();
            con.cmd.CommandText = "pMenu_MenuQueries";
            con.cmd.CommandType = CommandType.StoredProcedure;
            con.cmd.Parameters.AddWithValue("@role_id", objBO.RoleId);
            con.cmd.Parameters.AddWithValue("@emp_code", objBO.EmpCode);
            con.cmd.Parameters.AddWithValue("@Logic", "GetAllotedMenuByEmpCode");
            con.cmd.Parameters.Add("@result", SqlDbType.VarChar, 50).Value = "";
            con.cmd.Parameters["@result"].Direction = ParameterDirection.InputOutput;
            SqlDataReader dr = con.cmd.ExecuteReader();
            while (dr.Read())
            {
                var menu = new MenuBO();
                menu.MenuId = dr["menu_id"].ToString();
                menu.MenuName = dr["menu_name"].ToString();
                menu.EmpCode = dr["emp_code"].ToString();
                menu.SubMenu = GetAllotedSubMenuByEmpCode(menu.MenuId, objBO.EmpCode, objBO.RoleId);
                list.Add(menu);
            }
            return list;
        }
        public List<SubMenuBO> GetAllotedSubMenuByEmpCode(string MenuId, string EmpCode, string RoleId)
        {
            List<SubMenuBO> list = new List<SubMenuBO>();
            Connection con = new Connection();
            con.Open();
            con.cmd.CommandText = "pMenu_MenuQueries";
            con.cmd.CommandType = CommandType.StoredProcedure;
            con.cmd.Parameters.AddWithValue("@role_id", RoleId);
            con.cmd.Parameters.AddWithValue("@menu_id", MenuId);
            con.cmd.Parameters.AddWithValue("@emp_code", EmpCode);
            con.cmd.Parameters.AddWithValue("@Logic", "GetAllotedSubMenuByEmpCode");
            con.cmd.Parameters.Add("@result", SqlDbType.VarChar, 50).Value = "";
            con.cmd.Parameters["@result"].Direction = ParameterDirection.InputOutput;
            SqlDataReader dr = con.cmd.ExecuteReader();
            while (dr.Read())
            {
                var subMenu = new SubMenuBO();
                subMenu.MenuId = dr["menu_id"].ToString();
                subMenu.SubMenuId = dr["sub_menu_id"].ToString();
                subMenu.SubMenuName = dr["sub_menu_name"].ToString();
                subMenu.SubMenuLink = dr["sub_menu_link"].ToString();
                subMenu.EmpCode = dr["emp_code"].ToString();
                list.Add(subMenu);
            }
            return list;
        }

        //Get Unit And Division After Allotment
        public List<DivisionBO> GetUnitDivisionByEmpCode(DivisionBO objBO)
        {
            List<DivisionBO> list = new List<DivisionBO>();
            Connection con = new Connection();
            con.Open();
            con.cmd.CommandText = "pMenu_MenuQueries";
            con.cmd.CommandType = CommandType.StoredProcedure;           
            con.cmd.Parameters.AddWithValue("@emp_code", objBO.EmpCode);
            con.cmd.Parameters.AddWithValue("@Logic", "GetUnitDivisionByEmpCode");
            con.cmd.Parameters.Add("@result", SqlDbType.VarChar, 50).Value = "";
            con.cmd.Parameters["@result"].Direction = ParameterDirection.InputOutput;
            SqlDataReader dr = con.cmd.ExecuteReader();
            while (dr.Read())
            {
                var div = new DivisionBO();
                div.Division = dr["division"].ToString();               
                div.Unit = GetUnitByEmpCodeAndDivision(objBO.EmpCode,div.Division);
                list.Add(div);
            }
            return list;
        }
        public List<UnitBO> GetUnitByEmpCodeAndDivision(string EmpCode, string Division)
        {
            List<UnitBO> list = new List<UnitBO>();
            Connection con = new Connection();
            con.Open();
            con.cmd.CommandText = "pMenu_MenuQueries";
            con.cmd.CommandType = CommandType.StoredProcedure;
            con.cmd.Parameters.AddWithValue("@division", Division);           
            con.cmd.Parameters.AddWithValue("@emp_code", EmpCode);
            con.cmd.Parameters.AddWithValue("@Logic", "GetUnitByEmpCodeAndDivision");
            con.cmd.Parameters.Add("@result", SqlDbType.VarChar, 50).Value = "";
            con.cmd.Parameters["@result"].Direction = ParameterDirection.InputOutput;
            SqlDataReader dr = con.cmd.ExecuteReader();
            while (dr.Read())
            {
                var unit = new UnitBO();
                unit.ID = Convert.ToInt32(dr["ID"]);
                unit.UnitId = dr["unit_id"].ToString();
                unit.UnitName = dr["unit_name"].ToString();
                unit.Division = dr["Division"].ToString();
                unit.DefaultUnit = dr["default_unit"].ToString();
                list.Add(unit);
            }
            return list;
        }
    }
}