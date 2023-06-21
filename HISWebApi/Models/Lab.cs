using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace HISWebApi.Models
{
    public class ipLabMaster
    {
        public Nullable<int> ID { get; set; }
        public string Logic { get; set; }
        public string Result { get; set; }
        public bool Flag { get; set; }
        public bool IsActive { get; set; }
        public string hosp_id { get; set; }
        public string login_id { get; set; }
        public string warehouseCartId { get; set; }
        public string prm_1 { get; set; }
        public string prm_2 { get; set; }
        public DateTime? CreatedOn { get; set; }
    }
    public class LabReporting : ipLabMaster
    {
        public string LabCode { get; set; }
        public string IpOpType { get; set; }
        public string ReportStatus { get; set; }
        public string SubCat { get; set; }
        public string VisitNo { get; set; }
        public string BarccodeNo { get; set; }
        public string TestCategory { get; set; }
        public int AutoTestId { get; set; }
        public string TestCode { get; set; }
        public DateTime from { get; set; }
        public DateTime to { get; set; }
    }
    public class LabTestResultEntry : ipLabMaster {
        public string VisitNo { get; set; }
        public string dispatchLab { get; set; }
        public string SubCat { get; set; }
        public int AutoTestId { get; set; }
        public string DoctorSignId { get; set; }
        public string EntrySaveType { get; set; }
        public string TestCode { get; set; }
        public string ObservationId { get; set; }
        public char ab_flag { get; set; }
        public string read_1 { get; set; }
        public string read_2 { get; set; }
        public string test_comment { get; set; }
        public float min_value { get; set; }
        public float max_value { get; set; }
        public string nr_range { get; set; }
        public string result_unit { get; set; }
        public string method_name { get; set; }
        public string r_type { get; set; }
        public string report_text_content { get; set; }
    }
    public class SampleCollection : ipLabMaster
    {
     
        public string VisitNo { get; set; }
        public string BarcodeNo { get; set; }
        public string SampleCode { get; set; }
        public int AutotestId { get; set; }
        public int VialQty { get; set; }
        public string TestCode { get; set; }
        public string from { get; set; }
        public string to { get; set; }
        public string Prm1 { get; set; }
        public string Login_id { get; set; }

    }
    public class LabIvestigation : ipLabMaster
    {
        public string hosp_id { get; set; }
        public string catid { get; set; }
        public string subcateid { get; set; }
        public string investcode { get; set; }
        public string investname { get; set; }
        public string investreportname { get; set; }
        public string investtype { get; set; }
        public string bookfor { get; set; }
        public string repotytype { get; set; }
        public decimal rate { get; set; }
        public decimal cost { get; set; }
        public string maxtime { get; set; }
        public string sampleoption { get; set; }
        public string sampleqty { get; set; }
        public string sampleremark { get; set; }
        public string sampletemp { get; set; }
        public string samplecontainer { get; set; }
        public string sampletype { get; set; }
        public string defaultsample { get; set; }
        public string testprep { get; set; }
        public string abouttest { get; set; }
        public string samplelinkdata { get; set; }
        public string isoutsource { get; set; }
        public string incrementflag { get; set; }
        public string promotionflag { get; set; }
        public string reportflag { get; set; }
        public string ignortat { get; set; }
        public string inoutrequired { get; set; }
        public string displayinweb { get; set; }
        public string consentflag { get; set; }
        public string printsampleinreport { get; set; }
        public string text { get; set; }
        public string Logic { get; set; }
    }

    public class LabPackage : ipLabMaster
    {
        public string hosp_id { get; set; }
        public string catid { get; set; }
        public string subcateid { get; set; }
        public string packageId { get; set; }
        public string packagename { get; set; }
        public string packagetype { get; set; }
        public string bookFor { get; set; }
        public string description { get; set; }
        public string type { get; set; }
        public string packageInvesttype { get; set; }
        public string itemid { get; set; }
        public string innerpackageid { get; set; }

    }

    public class LabObservations : ipLabMaster
    {
        public string hosp_id { get; set; }
        public string catid { get; set; }
        public string subcatid { get; set; }
        public string HeaderName { get; set; }
        public string testcode { get; set; }
        public string testname { get; set; }
        public string observationid { get; set; }
        public string observationname { get; set; }
        public string methodname { get; set; }
        public int decimalplace { get; set; }
        public string interpretation { get; set; }
        public string defaultvalue { get; set; }
        public int isprintable { get; set; }
        public int isautoapprove { get; set; }
        public int isdeltarequired { get; set; }
        public int Isactive { get; set; }
        public int Isheader { get; set; }
        public int IsBold { get; set; }
        public int IsItalic { get; set; }
        public int IsResultMandatory { get; set; }
        public string valuetype { get; set; }
        public string Prm1 { get; set; }
        public string Prm2 { get; set; }
        public string testunit { get; set; }
    }
    public class LabTransportTime : ipLabMaster
    {
        public int autoid { get; set; }
        public string labcode { get; set; }
        public string dispatch_lab { get; set; }
        public string inshift { get; set; }
        public string outshift { get; set; }
        public int transport_time { get; set; }
    }
    public class LabObservationsInfo : ipLabMaster
    {
        public int autoid { get; set; }
        public string hosp_id { get; set; }
        public string testcode { get; set; }
        public string observationid { get; set; }
        public string observationname { get; set; }
        public string machineid { get; set; }
        public string gender { get; set; }
        public string prefix { get; set; }
        public float lowvalue { get; set; }
        public float highvalue { get; set; }
        public float paniclow { get; set; }
        public float panichigh { get; set; }
        public string agefrom { get; set; }
        public string ageto { get; set; }
        //public int isreflex { get; set; }
        public string displayreading { get; set; }
        public string LabCode { get; set; }
        public string methodname { get; set; }
        public string testunit { get; set; }
    }

    public class LabTemplates : ipLabMaster
    {
        public string hosp_id { get; set; }        
        public string subcatid { get; set; }
        public string testcode { get; set; }
        public string templatename { get; set; }
        public string templatecontent { get; set; }        
    }
    public class LabOutSourceInfo : ipLabMaster
    {

        public string catId { get; set; }
        public string subcatId { get; set; }
        public string investcode { get; set; }
        public int time { get; set; }
        public string testCode { get; set; }
        public string prm1 { get; set; }
        public string prm2 { get; set; }
        public string BarcodeNo { get; set; }
        public DateTime from { get; set; }
        public DateTime to { get; set; }
        public string logic { get; set; }

    }
    public class LabDefaultValue : ipLabMaster
    {
        public string autoid { get; set; }
        public string observationid { get; set; }
        public string defaultvalue { get; set; }        
    }

    public class LabDrSignature : ipLabMaster
    {
        public string signid { get; set; }
        public string deptid { get; set; }
        public string doctorname { get; set; }
        public string doctorid { get; set; }
        public string degree { get; set; }
        public string ImageName { get; set; }
        public string photo_path { get; set; }
        public string Base64String { get; set; }
        public string signvirtualpath { get; set; }
        public string signphysicalpath { get; set; }
        public HttpPostedFileBase Imagefile { get; set; }
    }
    public class LabOutSource : ipLabMaster
    {
        public string LabCode { get; set; }
        public string LabType { get; set; }
        public string LabName { get; set; }
        public string address { get; set; }
        public string cstate { get; set; }
        public string city { get; set; }
        public string contact_no { get; set; }
        public string cin { get; set; }

    }
   
    public class LabInputInfo : ipLabMaster
    {
        public string LabCode { get; set; }
        public string ToLabCode { get; set; }
        public string TestCode { get; set; }
        public string SampleCode { get; set; }        
        public int ProcessingTime { get; set; }        
    }

    public class LabSampleInfo : ipLabMaster
    {
        public int    AutoTestId { get; set; }
        public string VisitNo { get; set; }
        public string testcode { get; set; }
        public string LabCode { get; set; }
        public string DispatchLab { get; set; }
        public string DispatchNo { get; set; }
        public string BarcodeNo { get; set; }
        public string deliveryBoyid { get; set; }

    }
    public class SampleDispatchInfo : ipLabMaster
    {
       
        public string VisitNo { get; set; }
        public string BarcodeNo { get; set; }
        public string DispatchLabCode { get; set; }
        public string TestCode { get; set; }
        public DateTime  from { get; set; }
        public DateTime to { get; set; }
        public string Prm1 { get; set; }
        public string SubCatId { get; set; }
        public string DispatchNo { get; set; }
        
    }
    public class LabReceiveInfo : ipLabMaster
    {

        public string LabCode { get; set; }
        public string deliveryBoyid { get; set; }
        public string loginId { get; set; }
        public string result { get; set; }
        public string AutoTestId { get; set; }
        public string VisitNo { get; set; }
        public string testcode { get; set; }
        public string DispatchLab { get; set; }
        public string DispatchNo { get; set; }
        public string BarcodeNo { get; set; }

    }

    //mLabItemQueries


}