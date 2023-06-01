using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MediSoftTech_HIS.Areas.IPD.Controllers
{
    public class IPDController : Controller
    {
        public ActionResult IPD_OverDuePatientList()
        {
            return View();
        }

    }
}