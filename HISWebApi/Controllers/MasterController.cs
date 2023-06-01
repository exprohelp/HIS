using HISWebApi.Models;
using HISWebApi.Repository.Masters;
using HISWebApi.Repository.MenuMaster;
using HISWebApi.Repository.Utilities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using static MISWebApi.Models.MenuMaster;


namespace HISWebApi.Controllers
{
    [RoutePrefix("api/master")]
    public class MasterController : ApiController
    {
        DoctorMaster repository = new DoctorMaster();
        SpecializationMaster repospec = new SpecializationMaster();
        DegreeMaster repodeg = new DegreeMaster();
        DepartmentMaster repodep = new DepartmentMaster();
        CreateItems repitem = new CreateItems();
        CategorySubcategory repcatsubcat = new CategorySubcategory();
        FollowupMarking repfollowmark = new FollowupMarking();
        PanelMaster reppnlmst = new PanelMaster();
        StateDistrict repstdist = new StateDistrict();
        ReferralMaster repobj = new ReferralMaster();
        private CPOE repositoryCPOE = new CPOE();
        #region DoctorMaster
        [HttpPost]
        [Route("DoctorMasterQueries")]
        public HttpResponseMessage MasterQueries([FromBody] DoctorMastersBO objBO)
        {
            dataSet ds = repository.DoctorMasterQueries(objBO);
            return Request.CreateResponse(HttpStatusCode.OK, ds);
        }
        [HttpPost]
        [Route("mInsertUpdateDoctor")]
        public HttpResponseMessage InsertUpdateDoctor([FromBody] DoctorMastersBO objBO)
        {
            string Result = repository.mInsertUpdateDoctor(objBO);
            return Request.CreateResponse(HttpStatusCode.OK, Result);
        }
        [HttpPost]
        [Route("InsertUpdateDoctor")]
        public async Task<HttpResponseMessage> InsertUpdateDoctor()
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
                string IsPhotoUploaded = string.Empty;
                string outFileName = string.Empty;
                string vertual_path = string.Empty;
                var filesReadToProvider = await Request.Content.ReadAsMultipartAsync();
                //Json String of object  ipUploadDocument to be send at first or 0 index parameter   
                var json = await filesReadToProvider.Contents[0].ReadAsStringAsync();
                DoctorMastersBO obj = JsonConvert.DeserializeObject<DoctorMastersBO>(json);
                //Image to be send at second or 1 index parameter  
                ss.Message = repository.mInsertUpdateDoctor(obj);
                response = Request.CreateResponse(HttpStatusCode.OK, ss);
                obj.doctorId = ss.Message.Split('|')[1];
                byte[] fileBytes = await filesReadToProvider.Contents[1].ReadAsByteArrayAsync();
                if (fileBytes.Length > 20)
                {
                    obj.ImageName = obj.doctorId + ".jpg";
                    IsPhotoUploaded = UploadClass.UploadDoctorImage(out outFileName, out vertual_path, fileBytes, obj.ImageName);
                    obj.virtual_path = vertual_path;
                }
                //Update Photo
                if (IsPhotoUploaded.Contains("Success"))
                {
                    obj.Logic = "UpdateDoctorPhoto";
                    repository.mInsertUpdateDoctor(obj);
                }
            }
            catch (Exception ex)
            {
                ss.Status = 0;
                ss.Message = ex.Message;
                response = Request.CreateResponse(HttpStatusCode.OK, ss);
            }
            return response;
            //string doc_location = string.Empty;
            //string result = UploadClass.UploadPrescription(out doc_location, obj.imageByte, obj.ImageName);
            //return Request.CreateResponse(HttpStatusCode.OK, result);
        }
        [HttpPost]
        [Route("mInsertUpdateDoctorTimeSlot")]
        public HttpResponseMessage InsertUpdateDoctorTimeSlot([FromBody] DoctorsSlot objBO)
        {
            string Result = repository.InsertUpdateDoctorTimeSlot(objBO);
            return Request.CreateResponse(HttpStatusCode.OK, Result);
        }
        [HttpPost]
        [Route("mInsertDoctorLeave")]
        public HttpResponseMessage InsertDoctorLeave([FromBody] List<DoctorLeave> objBO)
        {
            string Result = repository.InsertDoctorLeave(objBO);
            return Request.CreateResponse(HttpStatusCode.OK, Result);
        }
        [HttpPost]
        [Route("mDeleteDoctorSchedule")]
        public HttpResponseMessage DeleteDoctorSchedule([FromBody] DoctorsSlot objBO)
        {
            string Result = repository.DeleteDoctorSchedule(objBO);
            return Request.CreateResponse(HttpStatusCode.OK, Result);
        }

        [HttpPost]
        [Route("mInsertDoctorRateList")]
        public HttpResponseMessage InsertDoctorRateList([FromBody] DoctorRateList objBO)
        {
            string Result = repository.InsertDoctorRateList(objBO);
            return Request.CreateResponse(HttpStatusCode.OK, Result);
        }

        [HttpPost]
        [Route("DoctorInsertSchedule")]
        public HttpResponseMessage DoctorInsertSchedule([FromBody] DoctorLeave objBO)
        {
            string Result = repository.DoctorInsertSchedule(objBO);
            return Request.CreateResponse(HttpStatusCode.OK, Result);
        }
        #endregion

        #region SpecializationMaster
        [HttpPost]
        [Route("mInsertUpdateSpecialization")]
        public HttpResponseMessage InsertUpdateSpecialization([FromBody] SpecializationBO objBO)
        {
            string Result = repospec.InsertUpdateSpecialization(objBO);
            return Request.CreateResponse(HttpStatusCode.OK, Result);
        }
        #endregion

        #region DegreeMaster
        [HttpPost]
        [Route("mInsertUpdateDegree")]
        public HttpResponseMessage InsertUpdateDegree([FromBody] DegreeBO objBO)
        {
            string Result = repodeg.mInsertUpdateDegree(objBO);
            return Request.CreateResponse(HttpStatusCode.OK, Result);
        }
        #endregion

        #region DepartmentMaster
        [HttpPost]
        [Route("mInsertUpdateDepartment")]
        public HttpResponseMessage InsertUpdateDepartment([FromBody] DepartmentBO objBO)
        {
            string Result = repodep.mInsertUpdateDepartment(objBO);
            return Request.CreateResponse(HttpStatusCode.OK, Result);
        }
        [HttpPost]
        [Route("SaveUpdateNotification")]
        public async Task<HttpResponseMessage> SaveUpdateNotification()
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
                var filesReadToProvider = await Request.Content.ReadAsMultipartAsync();
                //Json String of object  ipUploadDocument to be send at first or 0 index parameter   
                var json = await filesReadToProvider.Contents[0].ReadAsStringAsync();
                NotificationBO obj = JsonConvert.DeserializeObject<NotificationBO>(json);
                //Image to be send at second or 1 index parameter  
                ss.Message = repodep.SaveUpdateNotification(obj);
                byte[] fileBytes = await filesReadToProvider.Contents[1].ReadAsByteArrayAsync();
                string autoId = ss.Message.Split('|')[1];
                if (ss.Message.Contains("Success"))
                {
                    if (fileBytes.Length > 20)
                    {

                        //string ImageName = filesReadToProvider.Contents[1].Headers.ContentDisposition.FileName.Replace("\"", "");
                        string ImageName = autoId + ".jpg";
                        ss.Message = UploadClass.UploadNotificationImage(out outFileName, out vertual_path, fileBytes, ImageName);
                        obj.ImagePath = vertual_path;
                    }
                }
                if (obj.ImagePath.Length > 5)
                {
                    obj.AutoId = Convert.ToInt32(autoId);
                    obj.Logic = "UpdateImage";
                    repodep.SaveUpdateNotification(obj);
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

        #region CreateItem
        [HttpPost]
        [Route("mLabItemQueries")]
        public HttpResponseMessage mLabItemQueries([FromBody] CreateItem objBO)
        {
            dataSet ds = repitem.ItemQueries(objBO);
            return Request.CreateResponse(HttpStatusCode.OK, ds);
        }

        [HttpPost]
        [Route("mLabItemInsertUpdate")]
        public HttpResponseMessage mLabItemInsertUpdate([FromBody] CreateItem objBO)
        {
            string result = repitem.ItemInsertUpdate(objBO);
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }

        [HttpPost]
        [Route("mExportExcel")]
        public HttpResponseMessage mExportExcel([FromBody] CreateItem objBO)
        {
            dataSet ds = repitem.ItemQueries(objBO);
            HISWebApi.Repository.Utilities.ExcelGenerator obj = new Repository.Utilities.ExcelGenerator();
            return obj.GetExcelFile(ds.ResultSet);
        }

        #endregion

        #region Category SubCategory Master
        [HttpPost]
        [Route("mCategorySubCategoryQueries")]
        public HttpResponseMessage CategorySubCategoryQueries([FromBody] CategorySubCategoryBO objBO)
        {
            dataSet ds = repcatsubcat.mCategorySubCategoryQueries(objBO);
            return Request.CreateResponse(HttpStatusCode.OK, ds);
        }

        [HttpPost]
        [Route("mCategorySubCategoryInsertUpdate")]
        public HttpResponseMessage mcategoryInsertUpdate([FromBody] CategorySubCategoryBO objBO)
        {
            string result = repcatsubcat.CategorySubCategoryInsertUpdate(objBO);
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }
        #endregion

        #region Panel FollowUp

        [HttpPost]
        [Route("mPanelQueries")]
        public HttpResponseMessage mPanelQueries([FromBody] FollowupMarkingBO objBO)
        {
            dataSet ds = repfollowmark.mPanelQueries(objBO);
            return Request.CreateResponse(HttpStatusCode.OK, ds);
        }

        [HttpPost]
        [Route("mPanelFollowupInsertUpdate")]
        public HttpResponseMessage mPanelFollowupInsertUpdate([FromBody] FollowupMarkingBO objBO)
        {
            string result = repfollowmark.mPanelFollowupInsertUpdate(objBO);
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }

        [HttpPost]
        [Route("mInsertUpdatePanelMaster")]
        public HttpResponseMessage mInsertUpdatePanelMaster([FromBody] PanelMasterBO objBO)
        {
            string result = reppnlmst.mInsertUpdatePanelMaster(objBO);
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }



        #endregion

        #region ReferralMaster

        [HttpPost]
        [Route("mReferralQueries")]
        public HttpResponseMessage mReferralQueries([FromBody] ReferralMasterBO objBO)
        {
            if (objBO.ReportType == "Excel")
            {
                dataSet ds = repobj.mReferralQueries(objBO);
                ExcelGenerator obj = new ExcelGenerator();
                return obj.GetExcelFile(ds.ResultSet);
            }
            else
            {
                dataSet ds = repobj.mReferralQueries(objBO);
                return Request.CreateResponse(HttpStatusCode.OK, ds);
            }
        }
        [HttpPost]
        [Route("mReferralInsertUpdate")]
        public HttpResponseMessage mReferralInsertUpdate([FromBody] ReferralMasterBO objBO)
        {
            string result = repobj.mReferralInsertUpdate(objBO);
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }


        #endregion

        #region StateDistrict      
        [HttpPost]
        [Route("mStateDistrictQueries")]
        public HttpResponseMessage mStateDistrictQueries([FromBody] StateDistrictBO objBO)
        {
            dataSet ds = repstdist.mStateDistrictQueries(objBO);
            return Request.CreateResponse(HttpStatusCode.OK, ds);
        }

        [HttpPost]
        [Route("mStateDistrictInsertUpdate")]
        public HttpResponseMessage mStateDistrictInsertUpdate([FromBody] StateDistrictBO objBO)
        {
            string result = repstdist.mStateDistrictInsertUpdate(objBO);
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }
        #endregion
        #region Ambulance Master
        AmbulanceMaster repositoryAmbulance = new AmbulanceMaster();
        [HttpPost]
        [Route("Ambulance_MasterQueries")]
        public HttpResponseMessage Ambulance_MasterQueries([FromBody] ipAmbulance objBO)
        {
            dataSet ds = repositoryAmbulance.Ambulance_MasterQueries(objBO);
            return Request.CreateResponse(HttpStatusCode.OK, ds);
        }
        [HttpPost]
        [Route("Ambulance_InsertUpdateMaster")]
        public HttpResponseMessage Ambulance_InsertUpdateMaster([FromBody] AmbulanceMastersInfo objBO)
        {
            string result = repositoryAmbulance.Ambulance_InsertUpdateMaster(objBO);
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }
        [HttpPost]
        [Route("InsertAmbulanceDoc")]
        public async Task<HttpResponseMessage> InsertAmbulanceDoc()
        {
            HttpResponseMessage response = new HttpResponseMessage();
            AmbulanceMastersInfo objBO = new AmbulanceMastersInfo();
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
                string virtual_path = string.Empty;
                var filesReadToProvider = await Request.Content.ReadAsMultipartAsync();
                //Json String of object  ipUploadDocument to be send at first or 0 index parameter   
                var json = await filesReadToProvider.Contents[0].ReadAsStringAsync();
                AmbulanceMastersInfo obj = JsonConvert.DeserializeObject<AmbulanceMastersInfo>(json);
                //Image to be send at second or 1 index parameter  
                byte[] fileBytes = await filesReadToProvider.Contents[1].ReadAsByteArrayAsync();
                string FileName = filesReadToProvider.Contents[1].Headers.ContentDisposition.FileName;
                if (fileBytes.Length > 10)
                {
                    obj.ImageName = FileName.Substring((FileName.Length) - 5).Replace("\"", "");
                    obj.ImageName = obj.AmbulanceId + "_" + obj.DocType + FileName.Substring((FileName.Length) - 5).Replace("\"", "");
                    ss.Message = UploadClass.UploadAmbulanceDoc(out outFileName, out virtual_path, fileBytes, obj.ImageName);
                    response = Request.CreateResponse(HttpStatusCode.OK, ss);

                    obj.FilePath = virtual_path;
                    obj.Logic = "InsertAmbulanceDoc";
                    ss.Message = repositoryAmbulance.Ambulance_InsertUpdateMaster(obj);
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
        #endregion Ambulance Master
        #region CPOEMaster
        #region CPOEMaster
        [HttpPost]
        [Route("CPOE_MasterQueries")]
        public HttpResponseMessage CPOE_MasterQueries([FromBody] CPOETemplateItemsBO objBO)
        {
            dataSet ds = repositoryCPOE.CPOE_MasterQueries(objBO);
            return Request.CreateResponse(HttpStatusCode.OK, ds);
        }
        [HttpPost]
        [Route("CPOE_OPTHMasterQueries")]
        public HttpResponseMessage CPOE_OPTHMasterQueries([FromBody] CPOETemplateItemsBO objBO)
        {
            dataSet ds = repositoryCPOE.CPOE_OPTHMasterQueries(objBO);
            return Request.CreateResponse(HttpStatusCode.OK, ds);
        }
        [HttpPost]
        [Route("CPOE_OPTHQueries")]
        public HttpResponseMessage CPOE_OPTHQueries([FromBody] CPOETemplateItemsBO objBO)
        {
            dataSet ds = repositoryCPOE.CPOE_OPTHQueries(objBO);
            return Request.CreateResponse(HttpStatusCode.OK, ds);
        }
        [HttpPost]
        [Route("CPOE_InsertUpdateMaster")]
        public HttpResponseMessage CPOE_InsertUpdateMaster([FromBody] CPOETemplateItemsBO objBO)
        {
            string Result = repositoryCPOE.CPOE_InsertUpdateMaster(objBO);
            return Request.CreateResponse(HttpStatusCode.OK, Result);
        }
        [HttpPost]
        [Route("CPOE_OPTHInsertUpdateMaster")]
        public HttpResponseMessage CPOE_OPTHInsertUpdateMaster([FromBody] OPTHTemplateItemsBO objBO)
        {
            string Result = repositoryCPOE.CPOE_OPTHInsertUpdateMaster(objBO);
            return Request.CreateResponse(HttpStatusCode.OK, Result);
        }
        #endregion CPOEMaster
        #endregion CPOEMaster
        private MenuMaster repositoryMenuMaster = new MenuMaster();
        [HttpPost]
        [Route("MenuMasterQueries")]
        public HttpResponseMessage MenuMasterQueries([FromBody] GroupItems objBO)
        {
            dataSet ds = repositoryMenuMaster.MenuMasterQueries(objBO);
            return Request.CreateResponse(HttpStatusCode.OK, ds);
        }
        [HttpPost]
        [Route("InsertModifyMenuMaster")]
        public HttpResponseMessage InsertModifyMenuMaster([FromBody] GroupMaster objBO)
        {
            string result = repositoryMenuMaster.InsertModifyMenuMaster(objBO);
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }
        [HttpPost]
        [Route("AllotMenuToGroup")]
        public HttpResponseMessage AllotMenuToGroup(List<GroupItems> objBO)
        {
            string Result = repositoryMenuMaster.AllotMenuToGroup(objBO);
            return Request.CreateResponse(HttpStatusCode.OK, Result);
        }
        [HttpPost]
        [Route("AllotedMenu")]
        public HttpResponseMessage AllotedMenu([FromBody] GroupItems objBO)
        {
            dataSet ds = repositoryMenuMaster.MenuMasterQueries(objBO);
            StringBuilder b = new StringBuilder();
            b.Clear();
            DataTable dt = new DataTable();
            dt = ds.ResultSet.Tables[0];
            var Role = (from m in dt.AsEnumerable() select m["roleName"]).Distinct().ToList();
            var MainMenu = (from m in dt.AsEnumerable() select m["MenuName"]).Distinct().ToList();
            foreach (var r in Role)
            {
                b.Append("<div class='panel-heading mt-5' id='Panel" + r.ToString().Split('|')[1] + "'>" +
                    "<h4 class='panel-title'>" +
                    "<label class=''><input type='checkbox' checked='' class='parent' data-role='" + r.ToString().Split('|')[1] + "'></label>" +
                    "&nbsp;<a data-toggle='collapse' data-parent='#accordion' href='#" + r.ToString().Split('|')[1] + "' aria-expanded='false' class='collapsed'>" + r.ToString().Split('|')[0] + "</a>" +
                    "</h4>" +
                    "</div>" +
                    "<div id='" + r.ToString().Split('|')[1] + "' class='panel-collapse collapse' aria-expanded='false' style='height: 0px'>" +
                    "<div class='panel-body'><ul id='ul" + r.ToString().Split('|')[1] + "'>");
                foreach (var mm in (from m in dt.AsEnumerable() where (m.Field<string>("role_id") == r.ToString().Split('|')[1]) select m["MenuName"]).Distinct().ToList())
                {
                    b.Append("<li data-menuid='" + mm.ToString().Split('|')[1] + "'><label><input checked='' type='checkbox' data-menuid='" + mm.ToString().Split('|')[1] + "'>" + mm.ToString().Split('|')[0] + "</label>");
                    b.Append("<ul style='list-style:none' id='ulSubMenu" + mm.ToString().Split('|')[1] + "'>");
                    foreach (var sm in (from m in dt.AsEnumerable() where (m.Field<string>("menu_id") == mm.ToString().Split('|')[1]) select m["SubMenuName"]).Distinct().ToList())
                    {
                        b.Append("<li data-smid='" + sm.ToString().Split('|')[1] + "'><label><input checked='' type='checkbox' data-submenuid='" + sm.ToString().Split('|')[1] + "'>" + sm.ToString().Split('|')[0] + "</label></li>");
                    }
                    b.Append("</ul></li>");
                }

                b.Append("</ul></div></div>");
            }
            return Request.CreateResponse(HttpStatusCode.OK, b.ToString());
        }
        [HttpPost]
        [Route("GetAllotedMenuInfo")]
        public HttpResponseMessage GetAllotedMenuInfo([FromBody] GroupItems objBO)
        {
            dataSet ds = repositoryMenuMaster.MenuMasterQueries(objBO);
            StringBuilder b = new StringBuilder();
            b.Clear();
            DataTable dt = new DataTable();
            dt = ds.ResultSet.Tables[0];
            var Emp = (from m in dt.AsEnumerable() select m["emp_name"]).Distinct().ToList();
            var Role = (from m in dt.AsEnumerable() select m["roleName"]).Distinct().ToList();
            var MainMenu = (from m in dt.AsEnumerable() select m["MenuName"]).Distinct().ToList();
            foreach (var em in Emp)
            {
                b.Append("<div class='panel-heading mt-5' id='Panel" + em.ToString().Split('|')[1] + "'>" +
                    "<h4 class='panel-title'>" +
                    "<label class=''><input checked='' type='checkbox' class='parent' data-role='" + em.ToString().Split('|')[1] + "'></label>" +
                    "&nbsp;<a data-toggle='collapse' data-parent='#accordion' href='#" + em.ToString().Split('|')[1] + "' aria-expanded='false' class='collapsed'>" + em.ToString().Split('|')[0] + "</a>" +
                    "</h4>" +
                    "</div>" +
                    "<div id='" + em.ToString().Split('|')[1] + "' class='panel-collapse collapse' aria-expanded='false' style='height: 0px'>" +
                    "<div class='panel-body'>");
                foreach (var r in Role)
                {
                    b.Append("<div class='panel-heading mt-5' id='Panel" + r.ToString().Split('|')[1] + "'>" +
                        "<h4 class='panel-title'>" +
                        "<label class=''><input checked='' type='checkbox' class='parent' data-role='" + r.ToString().Split('|')[1] + "'></label>" +
                        "&nbsp;<a data-toggle='collapse' data-parent='#accordion' href='#" + r.ToString().Split('|')[1] + "' aria-expanded='false' class='collapsed'>" + r.ToString().Split('|')[0] + "</a>" +
                        "</h4>" +
                        "</div>" +
                        "<div id='" + r.ToString().Split('|')[1] + "' class='panel-collapse collapse in' aria-expanded='true'>" +
                        "<div class='panel-body'><ul id='ul" + r.ToString().Split('|')[1] + "'>");
                    foreach (var mm in (from m in dt.AsEnumerable() where (m.Field<string>("role_id") == r.ToString().Split('|')[1]) select m["MenuName"]).Distinct().ToList())
                    {
                        b.Append("<li data-menuid='" + mm.ToString().Split('|')[1] + "'><label><input checked='' type='checkbox' data-menuid='" + mm.ToString().Split('|')[1] + "'>" + mm.ToString().Split('|')[0] + "</label>");
                        b.Append("<ul style='list-style:none' id='ulSubMenu" + mm.ToString().Split('|')[1] + "'>");
                        foreach (var sm in (from m in dt.AsEnumerable() where (m.Field<string>("menu_id") == mm.ToString().Split('|')[1]) select m["SubMenuName"]).Distinct().ToList())
                        {
                            b.Append("<li data-smid='" + sm.ToString().Split('|')[1] + "'><label><input checked='' type='checkbox' data-submenuid='" + sm.ToString().Split('|')[1] + "'>" + sm.ToString().Split('|')[0] + "</label></li>");
                        }
                        b.Append("</ul></li>");
                    }

                    b.Append("</ul></div></div>");
                }
                b.Append("</div></div>");
            }
            return Request.CreateResponse(HttpStatusCode.OK, b.ToString());
        }
    }
}
