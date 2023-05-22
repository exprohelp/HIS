﻿using System.Web.Mvc;

namespace MediSoftTech_HIS.Areas.IPD
{
    public class IPDAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "IPD";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "IPD_default",
                "IPD/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}