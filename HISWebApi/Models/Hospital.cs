using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HISWebApi.Models
{
    public class HospitalBO
    {
        public int ID { get; set; }
        public string CardNo { get; set; }
        public string UHID { get; set; }
        public string DoctorId { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }
        public string PatientName { get; set; }
        public string from { get; set; }
        public string to { get; set; }
        public int Age { get; set; }
        public string AgeType { get; set; }
        public string City { get; set; }
        public string Locality { get; set; }
        public string login_id { get; set; }
        public string Address { get; set; }
        public string AppomtReason { get; set; }
        public string Logic { get; set; }
        public string Prm1 { get; set; }
        public string Result { get; set; }
    }
   
    public class ChartAndTable
    {
        public string TableHtml;
        public List<VitalChart> datachart;
    }
    public class ipOnlineAppQry
    {
        public string unit_id { get; set; }
        public string appointment_id { get; set; }
        public string fromdate { get; set; }
        public string todate { get; set; }
        public string prm_1 { get; set; }
        public string login_id { get; set; }
        public string Logic { get; set; }
    }
    public class iPayUPaymentList
    {
        public int status { get; set; }
        public string msg { get; set; }
        public List<TransactionDetail> Transaction_details { get; set; }
    }

    public class TransactionDetail
    {
        public string id { get; set; }
        public string status { get; set; }
        public string key { get; set; }
        public string merchantname { get; set; }
        public string txnid { get; set; }
        public string firstname { get; set; }
        public object lastname { get; set; }
        public string addedon { get; set; }
        public string bank_name { get; set; }
        public string payment_gateway { get; set; }
        public string phone { get; set; }
        public string email { get; set; }
        public string transaction_fee { get; set; }
        public string amount { get; set; }
        public string discount { get; set; }
        public string additional_charges { get; set; }
        public string productinfo { get; set; }
        public string error_code { get; set; }
        public string bank_ref_no { get; set; }
        public string ibibo_code { get; set; }
        public string mode { get; set; }
        public string ip { get; set; }
        public object card_no { get; set; }
        public object cardtype { get; set; }
        public string offer_key { get; set; }
        public object field0 { get; set; }
        public object field1 { get; set; }
        public string field2 { get; set; }
        public string field3 { get; set; }
        public object field4 { get; set; }
        public string field5 { get; set; }
        public string field6 { get; set; }
        public string field7 { get; set; }
        public object field8 { get; set; }
        public string field9 { get; set; }
        public string udf1 { get; set; }
        public string udf2 { get; set; }
        public string udf3 { get; set; }
        public string udf4 { get; set; }
        public string udf5 { get; set; }
        public string address2 { get; set; }
        public string city { get; set; }
        public string zipcode { get; set; }
        public string pg_mid { get; set; }
        public object issuing_bank { get; set; }
        public object offer_type { get; set; }
        public object failure_reason { get; set; }
        public string mer_service_fee { get; set; }
        public string mer_service_tax { get; set; }
    }
    public class ipPackageQueries
    {
        public string unit_id { get; set; }
        public string DoctorId { get; set; }
        public string PatientId { get; set; }
        public string UHID { get; set; }
        public string fromdate { get; set; }
        public string todate { get; set; }
        public string prm_1 { get; set; }
        public string read_date { get; set; }
        public string remark { get; set; }
        public string login_id { get; set; }
        public string Logic { get; set; }
    }
    public class CovidServices
    {
        public string patient_id { get; set; }
        public string item_id { get; set; }
        public DateTime proc_date { get; set; }
        public string proc_by { get; set; }
        public DateTime sch_date { get; set; }
        public string remark { get; set; }
        public string service_status { get; set; }
        public string Logic { get; set; }
    }
    public class VitalChart
    {
        public string day { get; set; }
        public string bp_Sys_high { get; set; }
        public string bp_dias_low { get; set; }
        public string temprature { get; set; }
        public string SPO2 { get; set; }
        public string pulse { get; set; }
    }
    public class ipTransaction
    {
        public string merchantKey { get; set; }
        public string merchantTransactionIds { get; set; }
        public string from { get; set; }
        public string to { get; set; }
        public int count { get; set; }
    }
    public class ipUpdatePayStatus
    {
        public string command { get; set; }
        public string StrValues { get; set; }
        public string AppointmentId { get; set; }
        public string loginId { get; set; }
        public string Logic { get; set; }
    }
    public class ipUploadPrescription
    {
        public string HospitalId { get; set; }
        public string TokenNo { get; set; }
        public string AppointmentId { get; set; }
        public string DoctorName { get; set; }
        public string ImageName { get; set; }
        public string ImageType { get; set; }
    }

    public class ipPayUResponse
    {
        public int status { get; set; }
        public string message { get; set; }
        public List<PayUResult> result { get; set; }
    }
    public class ipPayRefundResponse
    {
        public int errorCode { get; set; }
        public string message { get; set; }
        public string result { get; set; }
    }
    public class PayUResult
    {
        public string merchantTransactionId { get; set; }
        public decimal transactionAmount { get; set; }
        public decimal merchantServiceFee { get; set; }
        public decimal merchantServiceTax { get; set; }
        public PayUBody postBackParam { get; set; }
    }
    public class PayUBody
    {
        public string status { get; set; }
        public string txnid { get; set; }
        public string udf5 { get; set; }
        public string paymentId { get; set; }
        public string mode { get; set; }
        public string amount { get; set; }
        public string mihpayid { get; set; }
        public string addedon { get; set; }
        public string payuMoneyId { get; set; }
    }
    public class ipOnlinePayment
    {
        public string appointment_id { get; set; }
        public string pay_responseTnxId { get; set; }
        public string pay_responseKey { get; set; }
        public string paymentId { get; set; }
        public string payStatus { get; set; }
        public decimal payAmount { get; set; }
        public string Logic { get; set; }
    }
    public class ipOnlineConfirmation: ipOnlineAppQry
    {
        public string DoctorId { get; set; }
        public string appointment_datetime { get; set; }
        public string Confirm_remark { get; set; }
        public string payment_link { get; set; }
        public string meeting_link { get; set; }
        public string AppType { get; set; }
    }
    public class ipPackageOnlineConfirmation : ipPackageQueries
    {
        public string DoctorId { get; set; }
        public string appointment_datetime { get; set; }
        public string Confirm_remark { get; set; }
        public string payment_link { get; set; }
        public string meeting_link { get; set; }
        public string AppType { get; set; }
    }

}