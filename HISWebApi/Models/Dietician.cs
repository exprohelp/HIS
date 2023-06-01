using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HISWebApi.Models
{
    public class ipDietician
    {
        public int AutoId { get; set; }
        public string hosp_id { get; set; }
        public string UHID { get; set; }
        public string IPDNo { get; set; }
        public string from { get; set; }
        public string to { get; set; }
        public string OutPutType { get; set; }
        public string Prm1 { get; set; }
        public string Prm2 { get; set; }
        public string login_id { get; set; }
        public string Logic { get; set; }
    }
    public class DietMasterBO
    {
        public int Autoid { get; set; }
        public int qty { get; set; }
        public string DietTypeId { get; set; }
        public string DietId { get; set; }
        public string DietName { get; set; }
        public string DietTypeName { get; set; }
        public string ItemId { get; set; }
        public string ItemName { get; set; }
        public string NutritionName { get; set; }
        public string MeasuringName { get; set; }
        public string Measuring { get; set; }
        public string CategoryName { get; set; }
        public decimal NutritionValue { get; set; }
        public decimal CalorieCount { get; set; }
        public string DietRoute { get; set; }
        public string login_id { get; set; }
        public DateTime date { get; set; }
        public TimeSpan servefrom { get; set; }
        public TimeSpan serveto { get; set; }
        public char IsActive { get; set; }
        public string Logic { get; set; }
        public string prm1 { get; set; }
    }
    public class DietScheduleInfo : ipDietician
    {
        public string IPDNo { get; set; }
        public string FloorName { get; set; }
        public string RoomNo { get; set; }
        public string BedNo { get; set; }
        public string ServingDate { get; set; }
        public string DietCategory { get; set; }
        public string DietId { get; set; }
        public string ItemId { get; set; }
        public string MedicalProcedure { get; set; }
        public string Remark { get; set; }
        public decimal qty { get; set; }
    }
    public class DietPatientInfo : ipDietician
    {
        public string AdmitDate { get; set; }
        public string PatientName { get; set; }
        public decimal Age { get; set; }
        public string ageType { get; set; }
        public string Gender { get; set; }
        public decimal Weight { get; set; }
        public decimal Height { get; set; }
        public string DoctorId { get; set; }
        public string DoctorName { get; set; }
        public string DietCategory { get; set; }
        public string FloorName { get; set; }
        public string RoomType { get; set; }
        public string RoomNo { get; set; }
        public string BedNo { get; set; }
        public string IsDischarged { get; set; }
        public string DischargeDateTime { get; set; }
        public string MedicalProcedure { get; set; }
        public string ProcedureDate { get; set; }
        public string PreExistingDisease { get; set; }
        public string DietTypeId { get; set; }
    }
}