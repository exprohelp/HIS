using HISWebApi.Models;
using HISWebApi.Repository.Lab;
using HISWebApi.Repository.Lab.Masters;
using HISWebApi.Repository.Lab.Observation;
using HISWebApi.Repository.Utilities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web.Http;

namespace HISWebApi.Controllers
{
    [RoutePrefix("api/Lab")]
    public class LabController : ApiController
    {
        private InvestigationMaster investRepo = new InvestigationMaster();
        private PackageMaster packRepo = new PackageMaster();
        private LabObservation labRepo = new LabObservation();
        private LabTemplate labmethodrep = new LabTemplate();
        private LabDoctorSignature labdrsign = new LabDoctorSignature();
        private Sample sampleRepo = new Sample();



        #region InvestigationMaster
        [HttpPost]
        [Route("mInvestigationQueries")]
        public HttpResponseMessage InvestigationMasterQueries([FromBody] LabIvestigation objBO)
        {
            dataSet ds = investRepo.LabInvestigationMasterQueries(objBO);
            return Request.CreateResponse(HttpStatusCode.OK, ds);
        }

        [HttpPost]
        [Route("mInvestigationInsertUpdate")]
        public HttpResponseMessage mInvestigationInsertUpdate([FromBody] LabIvestigation objBO)
        {
            string result = investRepo.InvestigationInsertUpdate(objBO);
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }

        [HttpPost]
        [Route("mInsertUpdateTestInterpretation")]
        public HttpResponseMessage mInsertUpdateTestInterpretation([FromBody] LabIvestigation objBO)
        {
            string result = investRepo.InsertUpdateTestInterpretation(objBO);
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }

        [HttpPost]
        [Route("mDeleteTestInterpretation")]
        public HttpResponseMessage mDeleteTestInterpretation([FromBody] LabIvestigation objBO)
        {
            string result = investRepo.DeleteTestInterpretation(objBO);
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }
        #endregion

        #region PackageMaster
        [HttpPost]
        [Route("mPackageQueries")]
        public HttpResponseMessage PackageMasterQueries([FromBody] LabPackage objBO)
        {
            dataSet ds = packRepo.PackageMasterQueries(objBO);
            return Request.CreateResponse(HttpStatusCode.OK, ds);
        }

        [HttpPost]
        [Route("mPackageInsertUpdate")]
        public HttpResponseMessage PackageInsertUpdate([FromBody] LabPackage objBO)
        {
            string result = packRepo.PackageInsertUpdate(objBO);
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }

        [HttpPost]
        [Route("mPackageLinkItems")]
        public HttpResponseMessage PackageLinkItems([FromBody] LabPackage objBO)
        {
            string result = packRepo.PackageLinkItems(objBO);
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }

        [HttpPost]
        [Route("DeletePackageLinkItems")]
        public HttpResponseMessage DeletePackageLinkItems([FromBody] LabPackage objBO)
        {
            string result = packRepo.DeletePackageLinkItems(objBO);
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }
        #endregion
        [HttpPost]
        [Route("LabTransportTime")]
        public HttpResponseMessage LabTransportTime([FromBody]LabTransportTime objBO)
        {
            string result = labRepo.LabTransportTime(objBO);
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }

        #region LabObservation
        [HttpPost]
        [Route("mObservationQueries")]
        public HttpResponseMessage LabObservationQueries([FromBody] LabObservations objBO)
        {
            dataSet ds = labRepo.LabObservationQueries(objBO);
            return Request.CreateResponse(HttpStatusCode.OK, ds);
        }
        [HttpPost]
        [Route("LabOutSourceQuerries")]
        public HttpResponseMessage LabOutSourceQuerries([FromBody] LabObservations objBO)
        {
            dataSet ds = labRepo.LabObservationQueries(objBO);
            return Request.CreateResponse(HttpStatusCode.OK, ds);
        }
        [HttpPost]
        [Route("mObservationInsertUpdate")]
        public HttpResponseMessage mObservationInsertUpdate([FromBody] LabObservations objBO)
        {
            string result = labRepo.ObservationInsertUpdate(objBO);
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }

        [HttpPost]
        [Route("LabInvestiObservationLink")]
        public HttpResponseMessage mObservationDelete([FromBody] LabObservations objBO)
        {
            string result = labRepo.LabInvestiObservationLink(objBO);
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }
        #endregion

        #region LabObservationInfo
        [HttpPost]
        [Route("mObservationInfoInsertUpdate")]
        public HttpResponseMessage mObservationInfoInsertUpdate([FromBody] LabObservationsInfo objBO)
        {
            string result = labRepo.ObservationInfoInsertUpdate(objBO);
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }

        [HttpPost]
        [Route("mObservationInfoDelete")]
        public HttpResponseMessage mObservationInfoDelete([FromBody] LabObservationsInfo objBO)
        {
            string result = labRepo.ObservationInfoDelete(objBO);
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }

        [HttpPost]
        [Route("mDefaultValueInsert")]
        public HttpResponseMessage mDefaultValueInsert([FromBody] LabDefaultValue objBO)
        {
            string result = labRepo.DefaultValueInsert(objBO);
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }




        #endregion

        #region LabTemplates
        [HttpPost]
        [Route("mLabTemplateQueries")]
        public HttpResponseMessage mLabTemplateQueries([FromBody] LabTemplates objBO)
        {
            dataSet ds = labmethodrep.LabTemplateQueries(objBO);
            return Request.CreateResponse(HttpStatusCode.OK, ds);
        }

        [HttpPost]
        [Route("mLabTemplateInsertUpdate")]
        public HttpResponseMessage mLabTemplateInsertUpdate([FromBody] LabTemplates objBO)
        {
            string result = labmethodrep.LabTemplateInsertUpdate(objBO);
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }


        #endregion

        #region LabDoctorSignature
        [HttpPost]
        [Route("mDoctorSignatireQueries")]
        public HttpResponseMessage mDoctorSignatireQueries([FromBody] LabDrSignature objBO)
        {
            dataSet ds = labdrsign.LabDoctorSignatureQueries(objBO);
            return Request.CreateResponse(HttpStatusCode.OK, ds);
        }
        [HttpPost]
        [Route("mDoctorSignatireInsertUpdate")]
        public HttpResponseMessage DoctorSignatireInsertUpdate([FromBody] LabDrSignature objBO)
        {
            string result = labdrsign.DoctorSignatireInsertUpdate(objBO);
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }
        [HttpPost]
        [Route("UploadDoctorSignature")]
        public async Task<HttpResponseMessage> UploadDoctorSignature()
        {
            HttpResponseMessage response = new HttpResponseMessage();
            SubmitStatus ss = new SubmitStatus();
            if (!Request.Content.IsMimeMultipartContent())
            {
                ss.Status = 0;
                ss.Message = "This is not multipart content";
                response = Request.CreateResponse(HttpStatusCode.UnsupportedMediaType, "This is not multipart content");
            }
            try
            {             
                string outFileName = string.Empty;
                string vertual_path = string.Empty;
                byte[] fileBytes = null;
                var filesReadToProvider = await Request.Content.ReadAsMultipartAsync();
                var json = await filesReadToProvider.Contents[0].ReadAsStringAsync();
                LabDrSignature obj = JsonConvert.DeserializeObject<LabDrSignature>(json);
                //Json String of object  ipUploadDocument to be send at first or 0 index parameter  
                Regex regex = new Regex(@"^[\w/\:.-]+;base64,");
                if (obj.photo_path == "Y")
                {
                    string base64File = regex.Replace(obj.Base64String, string.Empty);
                    fileBytes = Convert.FromBase64String(base64File);
                }
                obj.signvirtualpath = "/Profile/Signature/" + obj.doctorid + "_Sign.jpg";
                obj.signphysicalpath = "D:\\Profile\\Signature\\" + obj.doctorid + "_Sign.jpg";
                ss.Message = labdrsign.DoctorSignatireInsertUpdate(obj);
                if (ss.Message.Contains("Success"))
                {                   
                    if (obj.photo_path == "Y" && fileBytes != null)
                    {
                        obj.ImageName = obj.doctorid + "_Sign.jpg";
                        string UploadMsg = UploadClass.UploadDoctorSign(out vertual_path, out outFileName, fileBytes, obj.ImageName);                      
                    }
                }
                response = Request.CreateResponse(HttpStatusCode.OK, ss);
            }
            catch (Exception ex)
            {
                ss.Status = 0;
                ss.Message = ex.Message;
                response = Request.CreateResponse(HttpStatusCode.OK, ss);
            }
            return response;
        }
        #endregion

        [HttpPost, Route("LabOutSourceQueries")]
        public HttpResponseMessage LabOutSourceQueries([FromBody] LabOutSourceInfo objBO)
        {
            dataSet ds = investRepo.LabOutSourceQueries(objBO);
            return Request.CreateResponse(HttpStatusCode.OK, ds);
        }

        [HttpPost, Route("LabSetProcTimeAndPerformingLab")]
        public HttpResponseMessage LabSetProcTimeAndPerformingLab([FromBody] List<LabInputInfo> objBO)
        {
            string result = investRepo.LabSetProcTimeAndPerformingLab(objBO);
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }

        #region Sample

        [HttpPost, Route("SampleDispatchQueries")]
        public HttpResponseMessage SampleDispatchQueries([FromBody] SampleDispatchInfo objBO)
        {
            dataSet ds = sampleRepo.SampleDispatchQueries(objBO);
            return Request.CreateResponse(HttpStatusCode.OK, ds);
        }
        [HttpPost, Route("Lab_SampleDispatchAndReceive")]
        public HttpResponseMessage Lab_SampleDispatchAndReceive([FromBody] List<LabSampleInfo> objBO)
        {
            string result = sampleRepo.Lab_SampleDispatchAndReceive(objBO);
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }
        [HttpPost, Route("SampleLabReceivingQueries")]
        public HttpResponseMessage SampleLabReceivingQueries([FromBody] SampleDispatchInfo objBO)
        {
            dataSet ds = sampleRepo.SampleLabReceivingQueries(objBO);
            return Request.CreateResponse(HttpStatusCode.OK, ds);
        }
        [HttpPost, Route("LabSampleReceiveInLab")]
        public HttpResponseMessage LabSampleReceiveInLab([FromBody] List<LabReceiveInfo> objBO)
        {
            string result = sampleRepo.LabSampleReceiveInLab(objBO);
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }

        #endregion Sample
        [HttpPost]
        [Route("UploadLabReport")]
        public async Task<HttpResponseMessage> UploadLabReport()
        {
            HttpResponseMessage response = new HttpResponseMessage();
            SubmitStatus ss = new SubmitStatus();
            if (!Request.Content.IsMimeMultipartContent())
            {
                ss.Status = 0;
                ss.Message = "This is not multipart content";
                response = Request.CreateResponse(HttpStatusCode.UnsupportedMediaType, "This is not multipart content");
            }
            try
            {
                string outFileName = string.Empty;
                string vertual_path = string.Empty;
                byte[] fileBytes = null;
                var filesReadToProvider = await Request.Content.ReadAsMultipartAsync();
                var json = await filesReadToProvider.Contents[0].ReadAsStringAsync();               
                LabDrSignature obj = JsonConvert.DeserializeObject<LabDrSignature>(json);
                //Json String of object  ipUploadDocument to be send at first or 0 index parameter  
                Regex regex = new Regex(@"^[\w/\:.-]+;base64,");
                if (obj.photo_path == "Y")
                {
                    string base64File = regex.Replace(obj.Base64String, string.Empty);
                    fileBytes = Convert.FromBase64String(base64File);
                }
                obj.signvirtualpath = "/Lab/Report/" + System.DateTime.Now.Year+"/"+ obj.signid + obj.doctorname;
                obj.signphysicalpath = obj.doctorname;              
                ss.Message = labdrsign.DoctorSignatireInsertUpdate(obj);
                if (ss.Message.Contains("Success"))
                {
                    if (obj.photo_path == "Y" && fileBytes != null)
                    {
                        obj.ImageName = obj.signid + obj.doctorname;
                        string UploadMsg = UploadClass.UploadLabReport(out vertual_path, out outFileName, fileBytes, obj.ImageName);
                    }
                }
                response = Request.CreateResponse(HttpStatusCode.OK, ss);
            }
            catch (Exception ex)
            {
                ss.Status = 0;
                ss.Message = ex.Message;
                response = Request.CreateResponse(HttpStatusCode.OK, ss);
            }
            return response;
        }
    }
}
