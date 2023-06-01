using System.IO;
using System.Web.Configuration;
using System.Web.Mvc;

namespace MediSoftTech_HIS.Areas.BloodBank.Controllers
{
    public class BloodBankController : Controller
    {
        public ActionResult DonorRegistration()
        {
            //var OutputDir = Server.MapPath("~/Content/Output.pdf");
            //// Invoke the ConvertHTML method to convert the HTML code to PDF    
            //Aspose.Html.Converters.Converter.ConvertHTML(@"https://www.javatpoint.com/html-table", ".", new Aspose.Html.Saving.PdfSaveOptions(), OutputDir);            
            return View();
        }
        public ActionResult WholeBloodProcess()
        {
            return View();
        }
        public ActionResult SDPProcess()
        {
            return View();
        }
        public ActionResult ProcessApproval()
        {
            return View();
        }
        public ActionResult ComponentCreation()
        {
            return View();
        }
        public ActionResult BloodIssue()
        {
            return View();
        }
        public ActionResult IndentBloodTest()
        {
            return View();
        }
        public ActionResult IssuedIndentInfo()
        {
            return View();
        }
        public ActionResult BloodBankDashboard()
        {
            return View();
        }
        public ActionResult RegisterDonorInfo()
        {
            return View();
        }
        public ActionResult RegisterComponent()
        {
            return View();
        }
        public ActionResult UnApproveDonation()
        {
            return View();
        }
    }
}