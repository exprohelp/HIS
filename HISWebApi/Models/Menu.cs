using System;
using System.Collections.Generic;

namespace HISWebApi.Models
{
    public class pmLogic
    {
        public Nullable<int> ID { get; set; }
        public string Logic { get; set; }
        public string Result { get; set; }
        public bool Flag { get; set; }
        public bool IsActive { get; set; }
        public string Prm1 { get; set; }
        public string UnitId { get; set; }
        public string EmpCode { get; set; }
        public string RoleId { get; set; }
        public string Division { get; set; }
        public string SubMenuId { get; set; }
        public DateTime CreatedOn { get; set; }
    }
    public class MenuBO : pmLogic
    {
        public string MenuId { get; set; }
        public string MenuName { get; set; }
        public List<SubMenuBO> SubMenu { get; set; }
    }

    public class SubMenuBO : pmLogic
    {
        public string SubMenuName { get; set; }
        public string SubMenuLink { get; set; }
        public string MenuId { get; set; }
    }
    public class DivisionBO : pmLogic
    {            
        public List<UnitBO> Unit { get; set; }
    }
    public class UnitBO:pmLogic
    {            
        public string UnitName { get; set; }
        public string DefaultUnit { get; set; }
    }
    public class RoleMasterBO : pmLogic
    {
        public int auto_id { get; set; }
        public string hosp_id { get; set; }
        public string role_id { get; set; }
        public string role_name { get; set; }
        public DateTime cr_date { get; set; }
        public string login_id { get; set; }

    }
    public class UnitrBO : pmLogic
    {
        public string emp_code { get; set; }
        public string unit_id { get; set; }
        public string login_id { get; set; }
    }
    public class MenuAllotBO : pmLogic
    {
        public string EmpCode { get; set; }
        public string SubMenuId { get; set; }
    }
}
