using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HISWebApi.Models
{
    public class BloodBank
    {
        public class DonorDetail : Screeing
        {
            public int Autoid { get; set; }
            public Nullable<DateTime> Dob { get; set; }
            public DateTime Blood_Donatedate { get; set; }
            public DateTime Dtentry { get; set; }
            public string BloodGroupId { get; set; }
            public int Isrevisit { get; set; }
            public int Tested_Bloodgroup_Id { get; set; }
            public string Aadharno { get; set; }
            public string City { get; set; }
            public string Contactno { get; set; }
            public string Country { get; set; }
            public string Dfirstname { get; set; }
            public string Dlastname { get; set; }
            public string Donor_Address { get; set; }
            public string Donor_Id { get; set; }
            public string Donorname { get; set; }
            public string Dtbirth { get; set; }
            public string Email { get; set; }
            public string Gender { get; set; }
            public string Blood_Pressure { get; set; }
            public string Base64String { get; set; }
            public string ImageName { get; set; }
            public string photo_path { get; set; }
            public string MediaLink { get; set; }
            public string Remark { get; set; }
            public string Hosp_Id { get; set; }
            public string Pincode { get; set; }
            public string Relation { get; set; }
            public string Relative_Name { get; set; }
            public string Title { get; set; }
            public string isFit { get; set; }
            public string BloodDonate { get; set; }

            public List<donorAnswer> objDonorAnswers { get; set; }
        }

        public class bb_donation_history : Screeing
        {
            public int Autoid { get; set; }
            public DateTime Collecteddate { get; set; }
            public DateTime Entrydate { get; set; }
            public DateTime Dtentry { get; set; }
            public int Isactive { get; set; }
            public int Iscomponent { get; set; }
            public int Isdonated { get; set; }
            public int Isfit { get; set; }
            public int Isgrouped { get; set; }

            public int Isscreened { get; set; }
            public int Istested { get; set; }
            public string Blood_Donate { get; set; }
            public string Bloodcollection_Id { get; set; }
            public string Collectedby { get; set; }
            public string Collectedremarks { get; set; }
            public string Donor_Id { get; set; }

            public string Haemoglobin { get; set; }
            public string Hbsag { get; set; }
            public string Hcv { get; set; }
            public string Hiv12 { get; set; }
            public string Hosp_Id { get; set; }
            public string Irgantibodies { get; set; }
            public string Mp { get; set; }
            public string Platelets { get; set; }
            public string Remarks { get; set; }
            public string Uhid { get; set; }
            public string Uselocation { get; set; }
            public string Vdrl { get; set; }
            public string Visit_Id { get; set; }
        }
        public class Screeing : AddEditBy
        {
            public string uhid { get; set; }
            public string Ipdno { get; set; }
            public string Bagtype { get; set; }
            public string Bbtubeno { get; set; }
            public string DonationType { get; set; }
            public string DonorType { get; set; }
            public int Isphlebotomy { get; set; }
            public string Platelet { get; set; }
            public string Pulse { get; set; }
            public string Hb { get; set; }
            public string Temprature { get; set; }
            public string Gpe { get; set; }
            public string Blood_Pressure { get; set; }
            public string Height { get; set; }
            public string Weight { get; set; }
            public string Volume { get; set; }
            public string Logic { get; set; }
        }


        public class donorAnswer
        {
            public string QuestId { get; set; }
            public string Answer { get; set; }
            public string Remarks { get; set; }
        }

        public class AddEditBy
        {
            public string Entryby { get; set; }
            public string updatedby { get; set; }
        }
        public class BloodIssueInfo
        {
            public int AutoId { get; set; }
            public string hosp_id { get; set; }
            public string Stock_ID { get; set; }
            public string ItemID { get; set; }
            public string IPOPNo { get; set; }
            public string IPOPType { get; set; }
            public string IndentNo { get; set; }
            public int Quantity { get; set; }
            public string Prm1 { get; set; }
            public string Prm2 { get; set; }
            public string login_id { get; set; }
            public string Logic { get; set; }
        }
        public class ComponentDetail
        {
            public string hosp_id { get; set; }
            public string donor_id { get; set; }
            public string visit_id { get; set; }
            public string BloodCollection_Id { get; set; }
            public string BagType { get; set; }
            public string BBTubeNo { get; set; }
            public string BloodGroup { get; set; }
            public string login_id { get; set; }
            public string Logic { get; set; }
            public List<ComponentInfo> componentInfo { get; set; }
        }
        public class ComponentInfo
        {
            public string ComponentID { get; set; }
            public string ComponentName { get; set; }
            public int Qty { get; set; }
            public string PackSize { get; set; }
            public int IsComponent { get; set; }
            public DateTime EntryDate { get; set; }
            public DateTime ExpiryDate { get; set; }

        }
        public class BloodScreeningDetail
        {
            public string hosp_id { get; set; }
            public string donor_id { get; set; }
            public string visit_id { get; set; }
            public string screening_id { get; set; }
            public string login_id { get; set; }
            public string Logic { get; set; }
            public List<ScreeningValue> ScreeningValue { get; set; }
        }
        public class ScreeningValue
        {
            public string BloodCollection_Id { get; set; }
            public string TestId { get; set; }
            public string Method { get; set; }
            public decimal resultValue { get; set; }
            public string Result { get; set; }
        }
        public class BloodCollection
        {
            public string Hosp_Id { get; set; }
            public string uhid { get; set; }
            public string Donor_id { get; set; }
            public string Visit_ID { get; set; }
            public string BloodCollection_Id { get; set; }
            public string BagType { get; set; }
            public string TubeNo { get; set; }
            public string Volume { get; set; }
            public string CollectionRemark { get; set; }
            public string CollectionBy { get; set; }
            public string Isdonated { get; set; }
            public string Phlebotomy { get; set; }
            public string IsShocked { get; set; }
            public string login_id { get; set; }
            public string Logic { get; set; }
        }

        public class BloodGrouping
        {
            public string hosp_id { get; set; }
            public string donor_id { get; set; }
            public string visit_id { get; set; }
            public string bb_Grouping_Id { get; set; }
            public string BloodCollection_Id { get; set; }
            public string ScreenedBG { get; set; }
            public string AntiA { get; set; }
            public string AntiB { get; set; }
            public string AntiAB { get; set; }
            public string RH { get; set; }
            public string BloodTested { get; set; }
            public string Remark { get; set; }
            public string CreatedBy { get; set; }
            public string ACell { get; set; }
            public string BCell { get; set; }
            public string OCell { get; set; }
            public string BloodGroupAlloted { get; set; }
            public string IsMotherSample { get; set; }
            public string IsMType { get; set; }
            public string IsMTested { get; set; }
            public string LedgerType { get; set; }
            public string BGTested { get; set; }
            public string MotherBG { get; set; }
            public string Logic { get; set; }
        }
        public class SelectQueriesInfo
        {
            public string hosp_id { get; set; }
            public string DonorId { get; set; }
            public string VisitId { get; set; }
            public string BloodGroup { get; set; }
            public int ExpireInDays { get; set; }

            public DateTime from { get; set; }
            public DateTime to { get; set; }
            public string IndentNo { get; set; }
            public string Prm1 { get; set; }
            public string Prm2 { get; set; }
            public string Prm3 { get; set; }
            public string login_id { get; set; }
            public string OutPutType { get; set; }

            public string Logic { get; set; }
        }
        public class ApproveDonationInfo
        {
            public string hosp_id { get; set; }
            public string donor_id { get; set; }
            public string visit_id { get; set; }
            public string Stock_Id { get; set; }
            public string BloodCollectionId { get; set; }
            public string Prm1 { get; set; }
            public string Prm2 { get; set; }
            public string Prm3 { get; set; }
            public string login_id { get; set; }
            public string Logic { get; set; }
        }
    }
}