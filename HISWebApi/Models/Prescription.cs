using System;
using System.Collections.Generic;

namespace HISWebApi.Models
{
    public class ipPrescription
    {
        public Nullable<int> ID { get; set; }
        public string UHID { get; set; }
        public string app_no { get; set; }
        public string DoctorId { get; set; }
        public string ItemId { get; set; }
        public string ItemName { get; set; }
        public string Logic { get; set; }
        public string Result { get; set; }
        public string TemplateId { get; set; }
        public bool Flag { get; set; }
        public bool IsActive { get; set; }
        public string login_id { get; set; }
        public string warehouseCartId { get; set; }
        public string prm_1 { get; set; }
        public string prm_2 { get; set; }
        public DateTime? CreatedOn { get; set; }
    }
    public class PrescriptionBO : ipPrescription
    {
        public string TemplateType { get; set; }
        public string FilterType { get; set; }
        public string DeptId { get; set; }
        public string DoctorId_Trf { get; set; }
        public string caseType { get; set; }
        public string consultType { get; set; }
        public string doctor_diagnosis { get; set; }
        public string doctor_remark { get; set; }
    }
    public class AdviceProcessBO : ipPrescription
    {

        public string TemplateType { get; set; }
        public string TemplateName { get; set; }
        public string Description { get; set; }
        public string ItemId { get; set; }
        public string DeptId { get; set; }
        public string DoctorId_Trf { get; set; }
        public string caseType { get; set; }
        public string consultType { get; set; }
        public string doctor_diagnosis { get; set; }
        public string doctor_remark { get; set; }
        public bool IsFav { get; set; }
    }
    public class VitalSignBO : ipPrescription
    {
        public string RefNo { get; set; }
        public string EntrySource { get; set; }
        public decimal BP_Sys { get; set; }
        public decimal BP_Dys { get; set; }
        public decimal Pulse { get; set; }
        public decimal Resp { get; set; }
        public decimal Temprarture { get; set; }
        public decimal HT { get; set; }
        public decimal WT { get; set; }
        public decimal ArmSpan { get; set; }
        public decimal SittingHeight { get; set; }
        public decimal IBW { get; set; }
        public decimal SPO2 { get; set; }
    }
    public class PrescribedItems
    {
        public string TemplateId { get; set; }
        public string Duration { get; set; }
        public string EyesInfo { get; set; }
        public string ItemId { get; set; }
        public string ItemName { get; set; }
        public string Remark { get; set; }
    }
    public class MedicineTemplateInfo : ipPrescription
    {
        public int AutoId { get; set; }
        public string med_TemplateId { get; set; }
        public string Item_id { get; set; }
        public string Item_name { get; set; }
        public string med_dose { get; set; }
        public string med_times { get; set; }
        public string med_duration { get; set; }
        public string med_intake { get; set; }
        public string med_route { get; set; }
        public string qty { get; set; }
        public string remark { get; set; }
        public bool IsFav { get; set; }
    }
    public class VisualAcuityInto : ipPrescription
    {
        public string left_unaided { get; set; }
        public string right_unaided { get; set; }
        public string left_withPinHole { get; set; }
        public string right_withPinHole { get; set; }
        public string left_withPrvGlass { get; set; }
        public string right_withPrvGlass { get; set; }
        public string left_PrvSpectPowerDist { get; set; }
        public string right_PrvSpectPowerDist { get; set; }
        public string left_PrvSpectPowerNear { get; set; }
        public string right_PrvSpectPowerNear { get; set; }
        public string PosteriorRatinaSegment { get; set; }
        public string EyeLens { get; set; }
        public string left_MCT { get; set; }
        public string right_MCT { get; set; }
        public string left_AT { get; set; }
        public string right_AT { get; set; }
        public string left_Gonioscopy { get; set; }
        public string right_Gonioscopy { get; set; }
    }
    public class SpecInto : ipPrescription
    {
        public int AutoId { get; set; }
        public string Item_id { get; set; }
        public string SpecType { get; set; }
        public string left_Sph { get; set; }
        public string left_Cyl { get; set; }
        public string left_Axis { get; set; }
        public string left_VA { get; set; }
        public string right_Sph { get; set; }
        public string right_Cyl { get; set; }
        public string right_Axis { get; set; }
        public string right_VA { get; set; }
        public string Const { get; set; }
        public string Bifocal { get; set; }
        public string Monofocal { get; set; }
        public string PhotoSun { get; set; }
        public string ARC { get; set; }
        public string Progressive { get; set; }
        public string Near { get; set; }
    }
    public class PrescribedMedicine : ipPrescription
    {
        public string Item_id { get; set; }
        public string Item_name { get; set; }
        public string med_dose { get; set; }
        public int med_times { get; set; }
        public int med_duration { get; set; }
        public string med_intake { get; set; }
        public string EyesInfo { get; set; }
        public string med_route { get; set; }
        public decimal qty { get; set; }
        public string remark { get; set; }
    }
    public class PrescribedItemsBO
    {
        public List<PrescribedItems> objItems { get; set; }
        public List<PrescribedMedicine> objMedicine { get; set; }
        public ipPrescription ipPrescription { get; set; }
    }
}