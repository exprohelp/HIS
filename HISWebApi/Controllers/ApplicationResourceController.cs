using HISWebApi.Models;
using HISWebApi.Repository.Inventory;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace HISWebApi.Controllers
{
    [RoutePrefix("api/ApplicationResource")]
    public class ApplicationResourceController : ApiController
    {
        #region Authentication
        private Authentication repositoryAuthentication = new Authentication();
        [HttpPost]        
        [Route("AuthenticationQueries")]
        public HttpResponseMessage AuthenticationQueries([FromBody] ipAuthentication objBO)
        {
            dataSet ds = repositoryAuthentication.AuthenticationQueries(objBO);
            return Request.CreateResponse(HttpStatusCode.OK, ds);
        }
        #endregion Authentication

        #region Master
        private Master repositoryMaster = new Master();
        [HttpPost]
        [Route("MasterQueries")]
        public HttpResponseMessage MenuQueries([FromBody]MasterDetails objBO)
        {
            dataSet ds = repositoryMaster.MasterQueries(objBO);
            return Request.CreateResponse(HttpStatusCode.OK, ds);
        }
        [HttpPost]
        [Route("InsertModifyMasterDetails")]
        public HttpResponseMessage InsertModifyMasterDetails([FromBody]MasterDetails objBO)
        {
            string Result = "";
            try
            {
                Result = repositoryMaster.InsertModifyMasterDetails(objBO);
                if (Result != "")
                {
                    return Request.CreateResponse(HttpStatusCode.OK, Result);
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound, Result);
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
        #endregion Master

        #region Menu
        private Menus repositoryMenu = new Menus();
        [HttpPost]
        [Route("MenuQueries")]
        public HttpResponseMessage MenuQueries([FromBody]RoleMasterBO objBO)
        {
            dataSet ds = repositoryMenu.MenuQueries(objBO);
            return Request.CreateResponse(HttpStatusCode.OK, ds);
        }
        [HttpPost]
        [Route("AssignUnitToEmp")]
        public HttpResponseMessage AssignUnitToEmp(List<RoleMasterBO> objBO)
        {
            string Result = "";
            try
            {
                Result = repositoryMenu.AssignUnitToEmp(objBO);
                if (Result != "")
                {
                    return Request.CreateResponse(HttpStatusCode.OK, Result);
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound, Result);
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
        [HttpPost]
        [Route("InsertDeleteAllotMenu")]
        public HttpResponseMessage InsertDeleteAllotMenu([FromBody]RoleMasterBO objBO)
        {
            string Result = "";
            try
            {
                Result = repositoryMenu.InsertDeleteAllotMenu(objBO);
                if (Result != "")
                {
                    return Request.CreateResponse(HttpStatusCode.OK, Result);
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound, Result);
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpPost]
        [Route("AllotSubMenuToEmployee")]
        public HttpResponseMessage AllotSubMenuToEmployee(List<RoleMasterBO> objBO)
        {
            string Result = "";
            try
            {
                Result = repositoryMenu.AllotSubMenuToEmployee(objBO);
                if (Result != "")
                {
                    return Request.CreateResponse(HttpStatusCode.OK, Result);
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound, Result);
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
        [HttpPost]
        [Route("GetMenuSubmenu")]
        public HttpResponseMessage Menu([FromBody]MenuBO objBO)
        {
            List<MenuBO> list = repositoryMenu.GetMenuByRoll(objBO);
            return Request.CreateResponse(HttpStatusCode.OK, list);
        }
        [HttpPost]
        [Route("GetAllotedMenuByEmpCode")]
        public HttpResponseMessage GetAllotedMenuByEmpCode([FromBody]MenuBO objBO)
        {
            List<MenuBO> list = repositoryMenu.GetAllotedMenuByEmpCode(objBO);
            return Request.CreateResponse(HttpStatusCode.OK, list);
        }
        [HttpPost]
        [Route("GetUnitDivisionByEmpCode")]
        public HttpResponseMessage GetUnitDivisionByEmpCode([FromBody]DivisionBO objBO)
        {
            List<DivisionBO> list = repositoryMenu.GetUnitDivisionByEmpCode(objBO);
            return Request.CreateResponse(HttpStatusCode.OK, list);
        }
        #endregion Menu

    }
}
