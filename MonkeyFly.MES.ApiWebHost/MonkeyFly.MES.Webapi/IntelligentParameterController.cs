using MonkeyFly.Core;
using MonkeyFly.MES.BasicService;
using MonkeyFly.MES.FilterAttributes;
using MonkeyFly.MES.Services;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Web.Http;
using System.Web.Http.Cors;

namespace MonkeyFly.MES.Webapi
{
    /// <summary>
    /// 智能参数控制器
    /// </summary>
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class IntelligentParameterController : ApiController
    {
        /// <summary>
        /// IntelligentParameterAPI
        /// </summary>
        [HttpGet]
        public void IntelligentParameterAPI() { }

        #region Inf00001厂别厂区主档
        /// <summary>
        /// 获取厂别的列表
        /// SAM 2017年4月26日15:19:42
        /// </summary>
        /// <param name="Token">授权码</param>
        /// <param name="Code">代号</param>
        /// <param name="page">页码</param>
        /// <param name="rows">行数</param>
        /// <returns></returns>
        [HttpGet]
        [Authenticate]
        public object Inf00001getPlantList(string Token, string Code = null, int page = 1, int rows = 10)
        {
            DataLogerService.writeURL(Token, "Code:" + Code);
            UtilBussinessService.WriteLog(UtilBussinessService.detoken(Token), "厂别主档", null, "查看", "Code:" + Code);
            return IPBussinessService.Inf00001getPlantList(Token, Code, page, rows);
        }

        /// <summary>
        /// 保存厂别
        /// SAM 2017年4月26日15:23:31
        /// </summary>
        /// <param name="request">JSON数据</param>
        /// <returns></returns>
        [HttpPost]
        [Authenticate]
        public object Inf00001PlantSave(JObject request)
        {
            DataLogerService.writeURL(request.Value<string>("Token"), request.ToString());
            UtilBussinessService.WriteLog(UtilBussinessService.detoken(request.Value<string>("Token")), "厂别主档", null, "保存", request.ToString());
            bool isDelete = false;
            bool isInsert = false;
            bool isUpdate = false;

            object deleted = null;
            object Insert = null;
            object Update = null;

            foreach (JProperty i in request.Children())
            {

                if (i.Name != "Token" && ((JArray)i.Value).Count > 0)
                {
                    switch (i.Name)
                    {
                        case "deleted": isDelete = true; break;
                        case "inserted": isInsert = true; break;
                        case "updated": isUpdate = true; break;
                    }
                }
            }

            if (isDelete)
                deleted = IPBussinessService.Plantdelete(request.Value<string>("Token"), request.Value<JArray>("deleted"));
            if (isInsert)
                Insert = IPBussinessService.Plantinsert(request.Value<string>("Token"), request.Value<JArray>("inserted"));
            if (isUpdate)
                Update = IPBussinessService.Plantupdate(request.Value<string>("Token"), request.Value<JArray>("updated"));

            return new { inserted = Insert, updated = Update, deleted = deleted };
        }

        /// <summary>
        /// 删除厂别
        /// SAM 2017年7月20日22:45:55
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Authenticate]
        public object Inf00001PlantDelete(JObject request)
        {
            DataLogerService.writeURL(request.Value<string>("Token"), request.ToString());
            UtilBussinessService.WriteLog(UtilBussinessService.detoken(request.Value<string>("Token")), "厂别厂区主档", null, "删除", request.ToString());
            return IPBussinessService.Inf00001PlantDelete(request);
        }


        /// <summary>
        /// 获取指定厂别的语序
        /// SAM 2017年4月26日15:49:46
        /// </summary>
        /// <param name="Token">授权码</param>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Authenticate]
        public object Inf00001GetPlantLanguageList(string Token, string id)
        {
            DataLogerService.writeURL(Token, "id:" + id);
            UtilBussinessService.WriteLog(UtilBussinessService.detoken(Token), "厂别主档语序", null, "查看", null);
            return IPBussinessService.Inf00001GetPlantLanguageList(id);
        }

        /// <summary>
        /// 保存厂别语序
        /// SAM 2017年4月26日15:54:13
        /// </summary>
        /// <param name="request">JSON数据</param>
        /// <returns></returns>
        [HttpPost]
        [Authenticate]
        public object Inf00001PlantLanguageSave(JObject request)
        {
            DataLogerService.writeURL(request.Value<string>("Token"), request.ToString());
            UtilBussinessService.WriteLog(UtilBussinessService.detoken(request.Value<string>("Token")), "厂别主档语序", null, "保存", request.ToString());
            bool isDelete = false;
            bool isInsert = false;
            bool isUpdate = false;

            object deleted = null;
            object Insert = null;
            object Update = null;

            foreach (JProperty i in request.Children())
            {

                if (i.Name != "Token" && ((JArray)i.Value).Count > 0)
                {
                    switch (i.Name)
                    {
                        case "deleted": isDelete = true; break;
                        case "inserted": isInsert = true; break;
                        case "updated": isUpdate = true; break;
                    }
                }
            }

            if (isDelete)
                deleted = IPBussinessService.Inf00001PlantLanguagedelete((JArray)request.Value<JArray>("deleted"));
            if (isInsert)
                Insert = IPBussinessService.Inf00001PlantLanguageinsert(request.Value<string>("Token"), (JArray)request.Value<JArray>("inserted"));
            if (isUpdate)
                Update = IPBussinessService.Inf00001PlantLanguageupdate(request.Value<string>("Token"), (JArray)request.Value<JArray>("updated"));


            return new { inserted = Insert, updated = Update, deleted = deleted };
        }


        /// <summary>
        /// 获取厂区列表
        /// SAM 2017年4月26日15:36:32
        /// </summary>
        /// <param name="Token">授权码</param>
        /// <param name="Code">代号</param>
        /// <param name="page">页码</param>
        /// <param name="rows">行数</param>
        /// <returns></returns>
        [HttpGet]
        [Authenticate]
        public object Inf00001getPlantAreaList(string Token, string Code = null, int page = 1, int rows = 10)
        {
            DataLogerService.writeURL(Token, "Code:" + Code);
            UtilBussinessService.WriteLog(UtilBussinessService.detoken(Token), "厂区主档", null, "查看", "Code:" + Code);
            return IPBussinessService.Inf00001getPlantAreaList(Token, Code, page, rows);
        }



        /// <summary>
        /// 保存厂区的操作
        /// SAM 2017年4月26日14:43:45
        /// </summary>
        /// <param name="request">JSON数据</param>
        /// <returns></returns>
        [HttpPost]
        [Authenticate]
        public object Inf00001PlantAreaSave(JObject request)
        {
            DataLogerService.writeURL(request.Value<string>("Token"), request.ToString());
            UtilBussinessService.WriteLog(UtilBussinessService.detoken(request.Value<string>("Token")), "厂区主档", null, "保存", request.ToString());
            bool isDelete = false;
            bool isInsert = false;
            bool isUpdate = false;

            object deleted = null;
            object Insert = null;
            object Update = null;

            foreach (JProperty i in request.Children())
            {

                if (i.Name != "Token" && ((JArray)i.Value).Count > 0)
                {
                    switch (i.Name)
                    {
                        case "deleted": isDelete = true; break;
                        case "inserted": isInsert = true; break;
                        case "updated": isUpdate = true; break;
                    }
                }
            }

            if (isDelete)
                deleted = IPBussinessService.PlantAreadelete(request.Value<string>("Token"), request.Value<JArray>("deleted"));
            if (isInsert)
                Insert = IPBussinessService.PlantAreainsert(request.Value<string>("Token"), request.Value<JArray>("inserted"));
            if (isUpdate)
                Update = IPBussinessService.PlantAreaupdate(request.Value<string>("Token"), request.Value<JArray>("updated"));

            return new { inserted = Insert, updated = Update, deleted = deleted };
        }


        /// <summary>
        /// 获取指定厂区的语序
        /// SAM 2017年4月26日15:49:46
        /// </summary>
        /// <param name="Token">授权码</param>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Authenticate]
        public object Inf00001GetPlantAreaLanguageList(string Token, string id)
        {
            DataLogerService.writeURL(Token, "id:" + id);
            UtilBussinessService.WriteLog(UtilBussinessService.detoken(Token), "厂区主档语序", null, "查看", null);
            return IPBussinessService.Inf00001GetPlantAreaLanguageList(id);
        }

        /// <summary>
        /// 保存厂区语序
        /// SAM 2017年4月26日15:54:13
        /// </summary>
        /// <param name="request">JSON数据</param>
        /// <returns></returns>
        [HttpPost]
        [Authenticate]
        public object Inf00001PlantAreaLanguageSave(JObject request)
        {
            DataLogerService.writeURL(request.Value<string>("Token"), request.ToString());
            UtilBussinessService.WriteLog(UtilBussinessService.detoken(request.Value<string>("Token")), "厂区主档语序", null, "保存", request.ToString());
            bool isDelete = false;
            bool isInsert = false;
            bool isUpdate = false;

            object deleted = null;
            object Insert = null;
            object Update = null;

            foreach (JProperty i in request.Children())
            {

                if (i.Name != "Token" && ((JArray)i.Value).Count > 0)
                {
                    switch (i.Name)
                    {
                        case "deleted": isDelete = true; break;
                        case "inserted": isInsert = true; break;
                        case "updated": isUpdate = true; break;
                    }
                }
            }

            if (isDelete)
                deleted = IPBussinessService.Inf00001PlantAreaLanguagedelete((JArray)request.Value<JArray>("deleted"));
            if (isInsert)
                Insert = IPBussinessService.Inf00001PlantAreaLanguageinsert(request.Value<string>("Token"), (JArray)request.Value<JArray>("inserted"));
            if (isUpdate)
                Update = IPBussinessService.Inf00001PlantAreaLanguageupdate(request.Value<string>("Token"), (JArray)request.Value<JArray>("updated"));


            return new { inserted = Insert, updated = Update, deleted = deleted };
        }

        #endregion

        #region Inf00002个人设定

        /// <summary>
        /// 账号管理列表--修改版
        /// MOUSE 2017年8月2日17:47:31
        /// </summary>
        /// <param name="Token">授权码</param>
        /// <param name="OrganizationID"></param>
        /// <param name="Account">账号</param>
        /// <param name="UserName">姓名</param>
        /// <param name="page">页码</param>
        /// <param name="rows">行数</param>
        /// <returns></returns>
        [HttpGet]
        [Authenticate]
        public object Inf00002GetList(string Token, string OrganizationID = null, string Account = null, string UserName = null, int page = 1, int rows = 10)
        {
            DataLogerService.writeURL(Token, "Account:" + Account + "-->UserName:" + UserName + "-->OrganizationID:" + OrganizationID);
            UtilBussinessService.WriteLog(UtilBussinessService.detoken(Token), "个人设定主档", null, "查看", "Account:" + Account + "-->UserName:" + UserName + "-->OrganizationID:" + OrganizationID);
            return IPBussinessService.Inf00002GetList(Token, OrganizationID, Account, UserName, page, rows);
        }

        /// <summary>
        /// 获取当前登录用户的个人信息-修改版
        /// MOUSE 2017年8月4日15:14:54
        /// </summary>
        /// <param name="Token"></param>
        /// <returns></returns>
        [HttpGet]
        [Authenticate]
        public Hashtable Inf00002GetUser(string Token)
        {
            DataLogerService.writeURL(Token, null);
            return IPBussinessService.Inf00002GetUser(Token);
        }

        /// <summary>
        /// 个人设定保存
        /// MOUSE 2017年8月4日16:49:00
        /// </summary>
        /// <param name="request">JSON数据</param>
        /// <returns></returns>
        [HttpPost]
        [Authenticate]
        public object Inf00002UserSave(JObject request)
        {
            string Token = request.Value<string>("Token");
            DataLogerService.writeURL(Token, request.ToString());
            UtilBussinessService.WriteLog(UtilBussinessService.detoken(Token), " 个人设定主档", null, "保存", request.ToString());
            return IPBussinessService.Inf00002UserSave(Token, request);
        }

        /// <summary>
        /// 修复密码
        /// Tom 2017年4月27日11:13:52
        /// </summary>
        /// <param name="request">JSON数据</param>
        /// <returns></returns>
        [HttpPost]
        [Authenticate]
        public object Inf00002ResetPassword(JObject request)
        {
            DataLogerService.writeURL(request.Value<string>("Token"), request.ToString());
            return IPBussinessService.Inf00002ResetPassword(request);
        }

        /// <summary>
        /// 获取角色的语序
        /// Tom 2017年4月27日16:44:02
        /// </summary>
        /// <param name="Token">授权码</param>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Authenticate]
        public object Inf00004GetPlantLanguageList(string Token, string id)
        {
            DataLogerService.writeURL(Token, "id:" + id);
            return IPBussinessService.Inf00004GetPlantLanguageList(id);
        }

        /// <summary>
        /// 保存角色语序
        /// Tom 2017年4月27日16:44:20
        /// </summary>
        /// <param name="request">JSON数据</param>
        /// <returns></returns>
        [HttpPost]
        [Authenticate]
        public object Inf00004PlantLanguageSave(JObject request)
        {
            DataLogerService.writeURL(request.Value<string>("Token"), request.ToString());
            bool isDelete = false;
            bool isInsert = false;
            bool isUpdate = false;

            object deleted = null;
            object Insert = null;
            object Update = null;

            foreach (JProperty i in request.Children())
            {

                if (i.Name != "Token" && ((JArray)i.Value).Count > 0)
                {
                    switch (i.Name)
                    {
                        case "deleted": isDelete = true; break;
                        case "inserted": isInsert = true; break;
                        case "updated": isUpdate = true; break;
                    }
                }
            }

            if (isDelete)
                deleted = IPBussinessService.Inf00004Languagedelete((JArray)request.Value<JArray>("deleted"));
            if (isInsert)
                Insert = IPBussinessService.Inf00004Languageinsert(request.Value<string>("Token"), (JArray)request.Value<JArray>("inserted"));
            if (isUpdate)
                Update = IPBussinessService.Inf00004Languageupdate(request.Value<string>("Token"), (JArray)request.Value<JArray>("updated"));


            return new { inserted = Insert, updated = Update, deleted = deleted };
        }
        #endregion

        #region Inf00003账户主档
        /// <summary>
        /// 账号管理列表
        /// SAM 2017年5月4日14:32:36
        /// </summary>
        /// <param name="Token">授权码</param>
        /// <param name="OrganizationID"></param>
        /// <param name="Code">代号</param>
        /// <param name="Status">状态</param>
        /// <param name="page">页码</param>
        /// <param name="rows">行数</param>
        /// <returns></returns>
        [HttpGet]
        [Authenticate]
        public object Inf00003GetList(string Token, string OrganizationID = null, string Code = null, string Status = null, int page = 1, int rows = 10)
        {
            DataLogerService.writeURL(Token, "Code:" + Code + "-->Status:" + Status + "-->OrganizationID:" + OrganizationID);
            UtilBussinessService.WriteLog(UtilBussinessService.detoken(Token), "账号管理主档", null, "查看", "Code:" + Code + "-->Status:" + Status + "-->OrganizationID:" + OrganizationID);
            return IPBussinessService.Inf00003GetList(Token, OrganizationID, Code, Status, page, rows);
        }

        /// <summary>
        /// 账号管理列表议题修改
        /// Mouse 2017年11月1日14:28:55增加筛选条件
        /// </summary>
        /// <param name="Token"></param>
        /// <param name="OrganizationID"></param>
        /// <param name="Code"></param>
        /// <param name="Status"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <param name="Account"></param>
        /// <param name="UserName"></param>
        /// <param name="DeptID">下拉框部门流水号</param>
        /// <returns></returns>
        [HttpGet]
        [Authenticate]
        public object Inf00003GetListV1(string Token, string OrganizationID = null, string Code = null, string Status = null,string Account = null, string UserName = null, string DeptID = null, int page = 1, int rows = 10)
        {
            DataLogerService.writeURL(Token, "Code:" + Code + "-->Status:" + Status + "-->OrganizationID:" + OrganizationID);
            UtilBussinessService.WriteLog(UtilBussinessService.detoken(Token), "账号管理主档", null, "查看", "Code:" + Code + "-->Status:" + Status + "-->OrganizationID:" + OrganizationID);
            return IPBussinessService.Inf00003GetListV1(Token, OrganizationID, Code, Status, Account, UserName, DeptID, page, rows);
        }

        /// <summary>
        /// 获取厂别的列表  删
        /// SAM 2017年4月26日15:19:42  删
        /// </summary>
        /// <param name="Token">授权码</param>
        /// <param name="Code">代号</param>
        /// <param name="page">页码</param>
        /// <param name="rows">行数</param>
        /// <returns></returns>
        [HttpGet]
        [Authenticate]
        public object Inf00001getPlantListLook(string Token, string Code = null, int page = 1, int rows = 10)
        {
            DataLogerService.writeURL(Token, "Code:" + Code);
            UtilBussinessService.WriteLog(UtilBussinessService.detoken(Token), "厂别主档", null, "查看", "Code:" + Code);
            return IPBussinessService.Inf00001getPlantList(Token, Code, page, rows);
        }

        /// <summary>
        /// 账号的新增
        /// SAM 2017年5月9日17:16:06
        /// </summary>
        /// <param name="request">JSON数据</param>
        /// <returns></returns>
        [HttpPost]
        [Authenticate]
        public object Inf00003Add(JObject request)
        {
            string Token = request.Value<string>("Token");
            DataLogerService.writeURL(Token, request.ToString());
            UtilBussinessService.WriteLog(UtilBussinessService.detoken(Token), " 账号管理主档", null, "新增", request.ToString());
            return IPBussinessService.Inf00003Add(Token, request);
        }

        /// <summary>
        /// 账号的更新
        /// SAM 2017年5月9日17:16:38
        /// </summary>
        /// <param name="request">JSON数据</param>
        /// <returns></returns>
        [HttpPost]
        [Authenticate]
        public object Inf00003Update(JObject request)
        {
            string Token = request.Value<string>("Token");
            DataLogerService.writeURL(Token, request.ToString());
            UtilBussinessService.WriteLog(UtilBussinessService.detoken(Token), " 账号管理主档", null, "编辑", request.ToString());
            return IPBussinessService.Inf00003Update(Token, request);
        }

        /// <summary>
        /// 帐号的删除
        /// SAM 2017年7月30日23:39:44
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Authenticate]
        public object Inf00003Delete(JObject request)
        {
            string Token = request.Value<string>("Token");
            DataLogerService.writeURL(Token, request.ToString());
            UtilBussinessService.WriteLog(UtilBussinessService.detoken(Token), " 账号管理主档", null, "删除", request.ToString());
            return IPBussinessService.Inf00003Delete(Token, request);
        }

        /// <summary>
        /// 账号管理-获取所有有厂别的部门
        /// SAM 2017年7月4日10:57:59
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Authenticate]
        public IList<Hashtable> Inf00003GetOrganization(string Token)
        {
            return IPBussinessService.Inf00003GetOrganization(Token);
        }

        /// <summary>
        /// 账号的保存
        /// SAM 2017年5月4日15:22:48
        /// </summary>
        /// <param name="request">JSON数据</param>
        /// <returns></returns>
        [HttpPost]
        [Authenticate]
        public object Inf00003Save(JObject request)
        {
            DataLogerService.writeURL(request.Value<string>("Token"), request.ToString());
            UtilBussinessService.WriteLog(UtilBussinessService.detoken(request.Value<string>("Token")), "账号管理主档", null, "保存", request.ToString());
            bool isDelete = false;
            bool isInsert = false;
            bool isUpdate = false;

            object deleted = null;
            object Insert = null;
            object Update = null;

            foreach (JProperty i in request.Children())
            {

                if (i.Name != "Token" && ((JArray)i.Value).Count > 0)
                {
                    switch (i.Name)
                    {
                        case "deleted": isDelete = true; break;
                        case "inserted": isInsert = true; break;
                        case "updated": isUpdate = true; break;
                    }
                }
            }

            if (isDelete)
                deleted = IPBussinessService.Inf00003delete(request.Value<string>("Token"), request.Value<JArray>("deleted"));
            if (isInsert)
                Insert = IPBussinessService.Inf00003insert(request.Value<string>("Token"), request.Value<JArray>("inserted"));
            if (isUpdate)
                Update = IPBussinessService.Inf00003update(request.Value<string>("Token"), request.Value<JArray>("updated"));

            return new { inserted = Insert, updated = Update, deleted = deleted };
        }

        /// <summary>
        /// 获取组织结构
        /// SAM 2017年5月25日16:42:19
        /// </summary>
        /// <param name="Token">授权码</param>
        /// <returns></returns>
        [HttpGet]
        [Authenticate]
        public IList<Hashtable> GetOrganization(string Token)
        {
            DataLogerService.writeURL(Token, null);
            return IPBussinessService.GetOrganization(Token);
        }

        #endregion

        #region  Inf00005部门主档
        /// <summary>
        /// 部门主档列表
        /// SAM 2017年5月18日23:26:30
        /// </summary>
        /// <param name="Token">授权码</param>
        /// <param name="Code">代号</param>
        /// <param name="Status">状态</param>
        /// <param name="page">页码</param>
        /// <param name="rows">行数</param>
        /// <returns></returns>
        [HttpGet]
        [Authenticate]
        public object Inf00005GetList(string Token, string Code = null, string Status = null, int page = 1, int rows = 10)
        {
            DataLogerService.writeURL(Token, "Code:" + Code + "--->Code:" + Code);
            UtilBussinessService.WriteLog(UtilBussinessService.detoken(Token), "部门主档", null, "查看", "Code:" + Code + "--->Code:" + Code);
            return IPBussinessService.Inf00005GetList(Token, Code, Status, page, rows);
        }

        /// <summary>
        /// 部门主档保存
        /// SAM 2017年5月18日23:27:23
        /// </summary>
        /// <param name="request">JSON数据</param>
        /// <returns></returns>
        [HttpPost]
        [Authenticate]
        public object Inf00005Save(JObject request)
        {
            DataLogerService.writeURL(request.Value<string>("Token"), request.ToString());
            UtilBussinessService.WriteLog(UtilBussinessService.detoken(request.Value<string>("Token")), "部门主档", null, "保存", request.ToString());
            bool isDelete = false;
            bool isInsert = false;
            bool isUpdate = false;

            object deleted = null;
            object Insert = null;
            object Update = null;

            foreach (JProperty i in request.Children())
            {

                if (i.Name != "Token" && ((JArray)i.Value).Count > 0)
                {
                    switch (i.Name)
                    {
                        case "deleted": isDelete = true; break;
                        case "inserted": isInsert = true; break;
                        case "updated": isUpdate = true; break;
                    }
                }
            }

            if (isDelete)
                deleted = IPBussinessService.Inf00005delete(request.Value<string>("Token"), request.Value<JArray>("deleted"));
            if (isInsert)
                Insert = IPBussinessService.Inf00005insert(request.Value<string>("Token"), request.Value<JArray>("inserted"));
            if (isUpdate)
                Update = IPBussinessService.Inf00005update(request.Value<string>("Token"), request.Value<JArray>("updated"));

            return new { inserted = Insert, updated = Update, deleted = deleted };
        }

        /// <summary>
        /// 根据厂别获取他下面所有正常的部门
        /// SAM 2017年5月19日10:54:56
        /// </summary>
        /// <param name="Token">授权码</param>
        /// <param name="PlantCode"></param>
        /// <returns></returns>
        [HttpGet]
        [Authenticate]
        public object Inf00005GetDeptList(string Token, string PlantCode)
        {
            DataLogerService.writeURL(Token, "PlantCode:" + PlantCode);
            UtilBussinessService.WriteLog(UtilBussinessService.detoken(Token), "部门主档", null, "查看", "PlantID:" + PlantCode);
            return IPBussinessService.Inf00005GetDeptList(Token, PlantCode);
        }

        /// <summary>
        /// 部门的单一删除
        /// SAM 2017年7月25日11:06:12
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Authenticate]
        public object Inf00005Delete(JObject request)
        {
            DataLogerService.writeURL(request.Value<string>("Token"), request.ToString());
            UtilBussinessService.WriteLog(UtilBussinessService.detoken(request.Value<string>("Token")), "部门主档", null, "删除", request.ToString());
            return IPBussinessService.Inf00005Delete(request);
        }



        /// <summary>
        /// 根据部门流水号获取班别列表
        /// SAM 2017年7月25日11:23:44
        /// </summary>
        /// <param name="Token"></param>
        /// <param name="OrganizationID"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <returns></returns>
        [HttpGet]
        [Authenticate]
        public object Inf00005GetClassList(string Token, string OrganizationID, int page = 1, int rows = 10)
        {
            DataLogerService.writeURL(Token, "OrganizationID:" + OrganizationID);
            UtilBussinessService.WriteLog(UtilBussinessService.detoken(Token), "部门主档", null, "查看", "OrganizationID:" + OrganizationID);
            return IPBussinessService.Inf00005GetClassList(Token, OrganizationID, page, rows);
        }


        /// <summary>
        /// 根据部门流水号获取班别（不分页）
        /// SAM 2017年7月25日11:29:37
        /// </summary>
        /// <param name="Token"></param>
        /// <param name="OrganizationID"></param>
        /// <returns></returns>
        [HttpGet]
        [Authenticate]
        public object Inf00005ClassList(string Token, string OrganizationID)
        {
            DataLogerService.writeURL(Token, "OrganizationID:" + OrganizationID);
            return IPBussinessService.Inf00005ClassList(Token, OrganizationID);
        }

        /// <summary>
        /// 根据部门流水号获取不属于他的班别（不分页）
        /// SAM 2017年5月25日14:23:48
        /// </summary>
        /// <param name="Token">授权码</param>
        /// <param name="OrganizationID"></param>
        /// <returns></returns>
        [HttpGet]
        [Authenticate]
        public object Inf00005NoClassList(string Token, string OrganizationID)
        {
            DataLogerService.writeURL(Token, "OrganizationID:" + OrganizationID);
            return IPBussinessService.Inf00005NoClassList(Token, OrganizationID);
        }

        /// <summary>
        /// 保存部门的班别列表
        /// SAM 2017年5月25日14:39:38
        /// </summary>
        /// <param name="request">JSON数据</param>
        /// <returns></returns>
        [HttpPost]
        [Authenticate]
        public object Inf00005ClassSave(JObject request)
        {
            DataLogerService.writeURL(request.Value<string>("Token"), request.ToString());
            UtilBussinessService.WriteLog(UtilBussinessService.detoken(request.Value<string>("Token")), "部门主档", null, "保存", request.ToString());
            return IPBussinessService.Inf00005ClassSave(request);
        }



        #endregion

        #region Inf00007客户主档
        /// <summary>
        /// 获取客户主档列表
        /// SAM 2017年4月27日10:09:16
        /// </summary>
        /// <param name="Token">授权码</param>
        /// <param name="Code">代号</param>
        /// <param name="Name"></param>
        /// <param name="page">页码</param>
        /// <param name="rows">行数</param>
        /// <returns></returns>
        [HttpGet]
        [Authenticate]
        public object Inf00007getList(string Token, string Code = null, string Name = null, int page = 1, int rows = 10)
        {
            DataLogerService.writeURL(Token, "Code:" + Code + "--->Name:" + Name);
            UtilBussinessService.WriteLog(UtilBussinessService.detoken(Token), "客户主档", null, "查看", "Code:" + Code + "--->Name:" + Name);
            return IPBussinessService.Inf00007getList(Token, Code, Name, page, rows);
        }

        /// <summary>
        /// 保存客户主档操作
        /// SAM 2017年4月27日10:19:40
        /// </summary>
        /// <param name="request">JSON数据</param>
        /// <returns></returns>
        [HttpPost]
        [Authenticate]
        public object Inf00007Save(JObject request)
        {
            DataLogerService.writeURL(request.Value<string>("Token"), request.ToString());
            UtilBussinessService.WriteLog(UtilBussinessService.detoken(request.Value<string>("Token")), "客户主档", null, "保存", request.ToString());

            bool isInsert = false;
            bool isUpdate = false;

            object deleted = null;
            object Insert = null;
            object Update = null;

            foreach (JProperty i in request.Children())
            {

                if (i.Name != "Token" && ((JArray)i.Value).Count > 0)
                {
                    switch (i.Name)
                    {

                        case "inserted": isInsert = true; break;
                        case "updated": isUpdate = true; break;
                    }
                }
            }
            if (isInsert)
                Insert = IPBussinessService.Inf00007insert(request.Value<string>("Token"), request.Value<JArray>("inserted"));
            if (isUpdate)
                Update = IPBussinessService.Inf00007update(request.Value<string>("Token"), request.Value<JArray>("updated"));

            return new { inserted = Insert, updated = Update, deleted = deleted };
        }

        /// <summary>
        /// 客户的删除
        /// SAM 2017年7月27日21:43:08
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Authenticate]
        public object Inf00007Delete(JObject request)
        {
            DataLogerService.writeURL(request.Value<string>("Token"), request.ToString());
            UtilBussinessService.WriteLog(UtilBussinessService.detoken(request.Value<string>("Token")), "客户主档", null, "删除", request.ToString());
            return IPBussinessService.Inf00007Delete(request);
        }

        /// <summary>
        /// 获取客户主档的语序
        /// SAM 2017年4月27日10:27:50
        /// </summary>
        /// <param name="Token">授权码</param>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Authenticate]
        public object Inf00007GetLanguageList(string Token, string id)
        {
            DataLogerService.writeURL(Token, "id:" + id);
            UtilBussinessService.WriteLog(UtilBussinessService.detoken(Token), "客户主档语序", null, "查看", null);
            return IPBussinessService.Inf00007GetLanguageList(id);
        }

        /// <summary>
        /// 保存客户语序
        /// SAM 2017年4月27日10:29:03
        /// </summary>
        /// <param name="request">JSON数据</param>
        /// <returns></returns>
        [HttpPost]
        [Authenticate]
        public object Inf00007LanguageSave(JObject request)
        {
            DataLogerService.writeURL(request.Value<string>("Token"), request.ToString());
            UtilBussinessService.WriteLog(UtilBussinessService.detoken(request.Value<string>("Token")), "客户主档语序", null, "保存", request.ToString());
            bool isDelete = false;
            bool isInsert = false;
            bool isUpdate = false;

            object deleted = null;
            object Insert = null;
            object Update = null;

            foreach (JProperty i in request.Children())
            {

                if (i.Name != "Token" && ((JArray)i.Value).Count > 0)
                {
                    switch (i.Name)
                    {
                        case "deleted": isDelete = true; break;
                        case "inserted": isInsert = true; break;
                        case "updated": isUpdate = true; break;
                    }
                }
            }

            if (isDelete)
                deleted = IPBussinessService.Inf00007Languagedelete((JArray)request.Value<JArray>("deleted"));
            if (isInsert)
                Insert = IPBussinessService.Inf00007Languageinsert(request.Value<string>("Token"), (JArray)request.Value<JArray>("inserted"));
            if (isUpdate)
                Update = IPBussinessService.Inf00007Languageupdate(request.Value<string>("Token"), (JArray)request.Value<JArray>("updated"));


            return new { inserted = Insert, updated = Update, deleted = deleted };
        }



        #endregion

        #region Inf00008厂商主档
        /// <summary>
        /// 获取厂商主档列表
        /// SAM 2017年4月27日11:29:27
        /// </summary>
        /// <param name="Token">授权码</param>
        /// <param name="Code">代号</param>
        /// <param name="Type"></param>
        /// <param name="page">页码</param>
        /// <param name="rows">行数</param>
        /// <returns></returns>
        [HttpGet]
        [Authenticate]
        public object Inf00008getList(string Token, string Code = null, string Type = null, int page = 1, int rows = 10)
        {
            DataLogerService.writeURL(Token, "Code:" + Code + "--->Type:" + Type);
            UtilBussinessService.WriteLog(UtilBussinessService.detoken(Token), "厂商主档", null, "查看", "Code:" + Code + "--->Type:" + Type);
            return IPBussinessService.Inf00008getList(Token, Code, Type, page, rows);
        }

        /// <summary>
        /// 保存厂商主档操作
        /// SAM 2017年4月27日11:29:30
        /// </summary>
        /// <param name="request">JSON数据</param>
        /// <returns></returns>
        [HttpPost]
        [Authenticate]
        public object Inf00008Save(JObject request)
        {
            DataLogerService.writeURL(request.Value<string>("Token"), request.ToString());
            UtilBussinessService.WriteLog(UtilBussinessService.detoken(request.Value<string>("Token")), "厂商主档", null, "保存", request.ToString());
            bool isDelete = false;
            bool isInsert = false;
            bool isUpdate = false;

            object deleted = null;
            object Insert = null;
            object Update = null;

            foreach (JProperty i in request.Children())
            {

                if (i.Name != "Token" && ((JArray)i.Value).Count > 0)
                {
                    switch (i.Name)
                    {
                        case "deleted": isDelete = true; break;
                        case "inserted": isInsert = true; break;
                        case "updated": isUpdate = true; break;
                    }
                }
            }

            if (isDelete)
                deleted = IPBussinessService.Inf00008delete(request.Value<string>("Token"), request.Value<JArray>("deleted"));
            if (isInsert)
                Insert = IPBussinessService.Inf00008insert(request.Value<string>("Token"), request.Value<JArray>("inserted"));
            if (isUpdate)
                Update = IPBussinessService.Inf00008update(request.Value<string>("Token"), request.Value<JArray>("updated"));

            return new { inserted = Insert, updated = Update, deleted = deleted };
        }


        /// <summary>
        /// 获取厂商主档的语序
        /// SAM 2017-4-27 11:29:342
        /// </summary>
        /// <param name="Token">授权码</param>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Authenticate]
        public object Inf00008GetLanguageList(string Token, string id)
        {
            DataLogerService.writeURL(Token, "id:" + id);
            UtilBussinessService.WriteLog(UtilBussinessService.detoken(Token), "厂商主档语序", null, "查看", "id:" + id);
            return IPBussinessService.Inf00008GetLanguageList(id);
        }

        /// <summary>
        /// 保存厂商语序
        /// SAM 2017年4月27日11:29:39
        /// </summary>
        /// <param name="request">JSON数据</param>
        /// <returns></returns>
        [HttpPost]
        [Authenticate]
        public object Inf00008LanguageSave(JObject request)
        {
            DataLogerService.writeURL(request.Value<string>("Token"), request.ToString());
            UtilBussinessService.WriteLog(UtilBussinessService.detoken(request.Value<string>("Token")), "厂商主档语序", null, "保存", request.ToString());
            bool isDelete = false;
            bool isInsert = false;
            bool isUpdate = false;

            object deleted = null;
            object Insert = null;
            object Update = null;

            foreach (JProperty i in request.Children())
            {

                if (i.Name != "Token" && ((JArray)i.Value).Count > 0)
                {
                    switch (i.Name)
                    {
                        case "deleted": isDelete = true; break;
                        case "inserted": isInsert = true; break;
                        case "updated": isUpdate = true; break;
                    }
                }
            }

            if (isDelete)
                deleted = IPBussinessService.Inf00008Languagedelete((JArray)request.Value<JArray>("deleted"));
            if (isInsert)
                Insert = IPBussinessService.Inf00008Languageinsert(request.Value<string>("Token"), (JArray)request.Value<JArray>("inserted"));
            if (isUpdate)
                Update = IPBussinessService.Inf00008Languageupdate(request.Value<string>("Token"), (JArray)request.Value<JArray>("updated"));


            return new { inserted = Insert, updated = Update, deleted = deleted };
        }

        /// <summary>
        /// 删除单个厂商功能
        /// Mouse 2017年7月24日11:05:34
        /// </summary>
        /// <param name="request">JSON数据</param>
        /// <returns></returns>
        [HttpPost]
        [Authenticate]
        public object Inf00008ManufacturerDelete(JObject request)
        {
            DataLogerService.writeURL(request.Value<string>("Token"), request.ToString());
            UtilBussinessService.WriteLog(UtilBussinessService.detoken(request.Value<string>("Token")), "删除单个厂商", null, "删除", request.ToString());
            return IPBussinessService.Inf00008ManufacturerDelete(request);
        }
        #endregion

        #region Inf00009分类主档
        /// <summary>
        /// 获取分类群组码列表
        /// Tom 2017年4月28日14:01:04
        /// </summary>
        /// <param name="Token">授权码</param>
        /// <param name="Code">代号</param>
        /// <param name="Status">状态</param>
        /// <param name="page">页码</param>
        /// <param name="rows">行数</param>
        /// <returns></returns>
        [HttpGet]
        [Authenticate]
        public object Inf00009GroupCodeList(string Token, string Code = null, int? Status = null, int page = 1, int rows = 10)
        {
            UtilBussinessService.WriteLog(UtilBussinessService.detoken(Token), "分类群组码主档", null, "查看", "Code:" + Code + "<-->Status:" + Status);
            return IPBussinessService.Inf00009GroupCodeList(Token, Code, Status, page, rows);
        }

        /// <summary>
        /// 分类群组码保存
        /// Tom 2017年4月28日16:40:04
        /// </summary>
        /// <param name="request">JSON数据</param>
        /// <returns></returns>
        [HttpPost]
        [Authenticate]
        public object Inf00009GroupCodeSave(JObject request)
        {
            DataLogerService.writeURL(request.Value<string>("Token"), request.ToString());
            UtilBussinessService.WriteLog(UtilBussinessService.detoken(request.Value<string>("Token")), "分类群组码主档", null, "保存", request.ToString());
            bool isDelete = false;
            bool isInsert = false;
            bool isUpdate = false;

            object deleted = null;
            object Insert = null;
            object Update = null;

            foreach (JProperty i in request.Children())
            {
                if (i.Name != "Token" && ((JArray)i.Value).Count > 0)
                {
                    switch (i.Name)
                    {
                        case "deleted": isDelete = true; break;
                        case "inserted": isInsert = true; break;
                        case "updated": isUpdate = true; break;
                    }
                }
            }

            string Token = request.Value<string>("Token");
            string userID = UtilBussinessService.detoken(Token);
            if (isDelete)
                deleted = IPBussinessService.Inf00009GroupCodeDelete(userID, (JArray)request.Value<JArray>("deleted"));
            if (isInsert)
                Insert = IPBussinessService.Inf00009GroupCodeInsert(userID, (JArray)request.Value<JArray>("inserted"));
            if (isUpdate)
                Update = IPBussinessService.Inf00009GroupCodeUpdate(userID, (JArray)request.Value<JArray>("updated"));

            return new { inserted = Insert, updated = Update, deleted = deleted };
        }

        /// <summary>
        /// 分类群组码删除
        /// Joint 2017年7月24日16:49:48
        /// </summary>
        /// <param name="request"></param>
        [HttpPost]
        [Authenticate]
        public object Inf00009GroupCodeDeleted(JObject request)
        {
            DataLogerService.writeURL(request.Value<string>("Token"), request.ToString());
            UtilBussinessService.WriteLog(UtilBussinessService.detoken(request.Value<string>("Token")), "分类群组码主档", null, "删除", request.ToString());
            return IPBussinessService.Inf00009GroupCodeDeleted(request);
        }

        /// <summary>
        /// 分类列表
        /// Tom 2017年5月2日17:45:17
        /// </summary>
        /// <param name="Token">授权码</param>
        /// <param name="Code">代号</param>
        /// <param name="UseCode"></param>
        /// <param name="Status"></param>
        /// <param name="page">页码</param>
        /// <param name="rows">行数</param>
        /// <returns></returns>
        [HttpGet]
        [Authenticate]
        public object Inf00009ClassList(string Token, string UseCode = null, string Code = null, int? Status = null, int page = 1, int rows = 10)
        {
            UtilBussinessService.WriteLog(UtilBussinessService.detoken(Token), "分类主档", null, "查看", "Code:" + Code + "<-->Status:" + Status);
            return IPBussinessService.Inf00009ClassList(Token, UseCode, Code, Status, page, rows);
        }

        /// <summary>
        /// 分类保存
        /// Tom 2017年4月28日16:40:04
        /// </summary>
        /// <param name="request">JSON数据</param>
        /// <returns></returns>
        [HttpPost]
        [Authenticate]
        public object Inf00009ClassSave(JObject request)
        {
            DataLogerService.writeURL(request.Value<string>("Token"), request.ToString());
            UtilBussinessService.WriteLog(UtilBussinessService.detoken(request.Value<string>("Token")), "分类主档", null, "保存", request.ToString());
            bool isDelete = false;
            bool isInsert = false;
            bool isUpdate = false;

            object deleted = null;
            object Insert = null;
            object Update = null;

            foreach (JProperty i in request.Children())
            {
                if (i.Name != "Token" && ((JArray)i.Value).Count > 0)
                {
                    switch (i.Name)
                    {
                        case "deleted": isDelete = true; break;
                        case "inserted": isInsert = true; break;
                        case "updated": isUpdate = true; break;
                    }
                }
            }

            string Token = request.Value<string>("Token");
            string userID = UtilBussinessService.detoken(Token);
            if (isDelete)
                deleted = IPBussinessService.Inf00009ClassDelete(userID, (JArray)request.Value<JArray>("deleted"));
            if (isInsert)
                Insert = IPBussinessService.Inf00009ClassInsert(userID, (JArray)request.Value<JArray>("inserted"));
            if (isUpdate)
                Update = IPBussinessService.Inf00009ClassUpdate(userID, (JArray)request.Value<JArray>("updated"));

            return new { inserted = Insert, updated = Update, deleted = deleted };
        }

        /// <summary>
        /// 单一删除分类代号
        /// Joint 2017年7月24日17:45:32
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Authenticate]
        public object Inf00009ClassDeleted(JObject request)
        {
            DataLogerService.writeURL(request.Value<string>("Token"), request.ToString());
            UtilBussinessService.WriteLog(UtilBussinessService.detoken(request.Value<string>("Token")), "分类代号主档", null, "删除", request.ToString());
            return IPBussinessService.Inf00009ClassDeleted(request);
        }
        #endregion

        #region Inf00010料品主档
        /// <summary>
        /// 料品主档列表
        /// SAM 2017年5月16日14:50:06
        /// </summary>
        /// <param name="Token">授权码</param>
        /// <param name="Code">代号</param>
        /// <param name="Status">状态</param>
        /// <param name="page">页码</param>
        /// <param name="rows">行数</param>
        /// <returns></returns>
        [HttpGet]
        [Authenticate]
        public object Inf00010GetList(string Token, string Code = null, string Status = null, int page = 1, int rows = 10)
        {
            DataLogerService.writeURL(Token, "Code:" + Code + "--->Status:" + Status);
            UtilBussinessService.WriteLog(UtilBussinessService.detoken(Token), "料品主档", null, "查看", "Code:" + Code + "--->Status:" + Status);
            return IPBussinessService.Inf00010GetList(Token, Code, Status, page, rows);
        }

        /// <summary>
        /// 料品主档保存
        /// SAM 2017年5月16日14:56:20
        /// </summary>
        /// <param name="request">JSON数据</param>
        /// <returns></returns>
        [HttpPost]
        [Authenticate]
        public object Inf00010Save(JObject request)
        {
            DataLogerService.writeURL(request.Value<string>("Token"), request.ToString());
            UtilBussinessService.WriteLog(UtilBussinessService.detoken(request.Value<string>("Token")), "料品主档", null, "保存", request.ToString());
            bool isDelete = false;
            bool isInsert = false;
            bool isUpdate = false;

            object deleted = null;
            object Insert = null;
            object Update = null;

            foreach (JProperty i in request.Children())
            {

                if (i.Name != "Token" && ((JArray)i.Value).Count > 0)
                {
                    switch (i.Name)
                    {
                        case "deleted": isDelete = true; break;
                        case "inserted": isInsert = true; break;
                        case "updated": isUpdate = true; break;
                    }
                }
            }

            if (isDelete)
                deleted = IPBussinessService.Inf00010delete(request.Value<string>("Token"), request.Value<JArray>("deleted"));
            if (isInsert)
                Insert = IPBussinessService.Inf00010insert(request.Value<string>("Token"), request.Value<JArray>("inserted"));
            if (isUpdate)
                Update = IPBussinessService.Inf00010update(request.Value<string>("Token"), request.Value<JArray>("updated"));

            return new { inserted = Insert, updated = Update, deleted = deleted };
        }

        /// <summary>
        /// 料品的单一删除
        /// SAM 2017年7月24日17:44:54
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Authenticate]
        public object Inf00010Delete(JObject request)
        {
            DataLogerService.writeURL(request.Value<string>("Token"), request.ToString());
            UtilBussinessService.WriteLog(UtilBussinessService.detoken(request.Value<string>("Token")), "料品主档", null, "删除", request.ToString());
            return IPBussinessService.Inf00010Delete(request);
        }


        /// <summary>
        /// 获取料品的属性列表
        /// SAM 2017年5月16日16:30:28
        /// </summary>
        /// <param name="Token">授权码</param>
        /// <param name="ItemID"></param>
        /// <param name="page">页码</param>
        /// <param name="rows">行数</param>
        /// <returns></returns>
        [HttpGet]
        [Authenticate]
        public object Inf00010AttributeGetList(string Token, string ItemID, int page = 1, int rows = 10)
        {
            DataLogerService.writeURL(Token, "ItemID:" + ItemID);
            UtilBussinessService.WriteLog(UtilBussinessService.detoken(Token), "料品主档", null, "查看", "ItemID:" + ItemID);
            return IPBussinessService.Inf00010AttributeGetList(Token, ItemID, page, rows);
        }

        /// <summary>
        /// 料品的属性明细保存
        /// SAM 2017年5月16日16:41:44
        /// </summary>
        /// <param name="request">JSON数据</param>
        /// <returns></returns>
        [HttpPost]
        [Authenticate]
        public object Inf00010AttributeSave(JObject request)
        {
            DataLogerService.writeURL(request.Value<string>("Token"), request.ToString());
            UtilBussinessService.WriteLog(UtilBussinessService.detoken(request.Value<string>("Token")), "料品主档", null, "保存", request.ToString());
            bool isDelete = false;
            bool isInsert = false;
            bool isUpdate = false;

            object deleted = null;
            object Insert = null;
            object Update = null;

            foreach (JProperty i in request.Children())
            {

                if (i.Name != "Token" && ((JArray)i.Value).Count > 0)
                {
                    switch (i.Name)
                    {
                        case "deleted": isDelete = true; break;
                        case "inserted": isInsert = true; break;
                        case "updated": isUpdate = true; break;
                    }
                }
            }

            if (isDelete)
                deleted = IPBussinessService.Inf00010Attributedelete(request.Value<string>("Token"), request.Value<JArray>("deleted"));
            if (isInsert)
                Insert = IPBussinessService.Inf00010Attributeinsert(request.Value<string>("Token"), request.Value<JArray>("inserted"));
            if (isUpdate)
                Update = IPBussinessService.Inf00010Attributeupdate(request.Value<string>("Token"), request.Value<JArray>("updated"));

            return new { inserted = Insert, updated = Update, deleted = deleted };
        }

        /// <summary>
        /// 获取料品语序
        /// SAM 2017年5月17日10:00:49
        /// </summary>
        /// <param name="Token">授权码</param>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Authenticate]
        public IList<Hashtable> Inf00010GetLanguageList(string Token, string id)
        {
            DataLogerService.writeURL(Token, "id:" + id);
            UtilBussinessService.WriteLog(UtilBussinessService.detoken(Token), "语序", null, "查看", null);
            return IPBussinessService.Inf00010GetLanguageList(id);
        }

        /// <summary>
        /// 保存料品语序
        /// SAM 2017年5月17日10:00:49
        /// </summary>
        /// <param name="request">JSON数据</param>
        /// <returns></returns>
        [HttpPost]
        [Authenticate]
        public object Inf00010LanguageSave(JObject request)
        {
            DataLogerService.writeURL(request.Value<string>("Token"), request.ToString());
            UtilBussinessService.WriteLog(UtilBussinessService.detoken(request.Value<string>("Token")), "语序", null, "保存", request.ToString());
            bool isDelete = false;
            bool isInsert = false;
            bool isUpdate = false;

            object deleted = null;
            object Insert = null;
            object Update = null;

            foreach (JProperty i in request.Children())
            {

                if (i.Name != "Token" && ((JArray)i.Value).Count > 0)
                {
                    switch (i.Name)
                    {
                        case "deleted": isDelete = true; break;
                        case "inserted": isInsert = true; break;
                        case "updated": isUpdate = true; break;
                    }
                }
            }

            if (isDelete)
                deleted = IPBussinessService.Languagedelete(request.Value<JArray>("deleted"));
            if (isInsert)
                Insert = IPBussinessService.Inf00010Languageinsert(request.Value<string>("Token"), request.Value<JArray>("inserted"));
            if (isUpdate)
                Update = IPBussinessService.Inf00010Languageupdate(request.Value<string>("Token"), request.Value<JArray>("updated"));


            return new { inserted = Insert, updated = Update, deleted = deleted };
        }

        #endregion

        #region Inf00011单位主档
        /// <summary>
        /// 获取单位列表
        /// Tom 2017-05-02 17:22:23
        /// </summary>
        /// <param name="Token">授权码</param>
        /// <param name="Code">代号</param>
        /// <param name="page">页码</param>
        /// <param name="rows">行数</param>
        /// <returns></returns>
        [HttpGet]
        [Authenticate]
        public object Inf00011List(string Token, string Code = null, int page = 1, int rows = 10)
        {
            UtilBussinessService.WriteLog(UtilBussinessService.detoken(Token), "单位", null, "查看", "Code:" + Code);
            return IPBussinessService.Inf00011List(Token, Code, page, rows);
        }

        /// <summary>
        /// 单位保存
        /// Tom 2017年4月28日16:40:04
        /// </summary>
        /// <param name="request">JSON数据</param>
        /// <returns></returns>
        [HttpPost]
        [Authenticate]
        public object Inf00011Save(JObject request)
        {
            UtilBussinessService.WriteLog(UtilBussinessService.detoken(request.Value<string>("Token")), "单位", null, "保存", request.ToString());
            DataLogerService.writeURL(request.Value<string>("Token"), request.ToString());
            bool isDelete = false;
            bool isInsert = false;
            bool isUpdate = false;

            object deleted = null;
            object Insert = null;
            object Update = null;

            foreach (JProperty i in request.Children())
            {
                if (i.Name != "Token" && ((JArray)i.Value).Count > 0)
                {
                    switch (i.Name)
                    {
                        case "deleted": isDelete = true; break;
                        case "inserted": isInsert = true; break;
                        case "updated": isUpdate = true; break;
                    }
                }
            }

            string Token = request.Value<string>("Token");
            string userID = UtilBussinessService.detoken(Token);
            if (isDelete)
                deleted = IPBussinessService.Inf00011Delete(userID, (JArray)request.Value<JArray>("deleted"));
            if (isInsert)
                Insert = IPBussinessService.Inf00011Insert(userID, (JArray)request.Value<JArray>("inserted"));
            if (isUpdate)
                Update = IPBussinessService.Inf00011Update(userID, (JArray)request.Value<JArray>("updated"));

            return new { inserted = Insert, updated = Update, deleted = deleted };
        }

        /// <summary>
        /// 单位的删除
        /// SAM 2017年7月24日17:26:353
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Authenticate]
        public object Inf00011Delete(JObject request)
        {
            DataLogerService.writeURL(request.Value<string>("Token"), request.ToString());
            UtilBussinessService.WriteLog(UtilBussinessService.detoken(request.Value<string>("Token")), "单位主档", null, "删除", request.ToString());
            return IPBussinessService.Inf00011Delete(request);
        }
        #endregion

        #region Inf00012仓库主档

        /// <summary>
        /// 获取仓库列表
        /// SAM 2017年5月4日11:02:03
        /// </summary>
        /// <param name="Token">授权码</param>
        /// <param name="Code">代号</param>
        /// <param name="Status">状态</param>
        /// <param name="page">页码</param>
        /// <param name="rows">行数</param>
        /// <returns></returns>
        [HttpGet]
        [Authenticate]
        public object Inf00012GetList(string Token, string Code = null, string Status = null, int page = 1, int rows = 10)
        {
            DataLogerService.writeURL(Token, "Code:" + Code + "-->Status:" + Status);
            UtilBussinessService.WriteLog(UtilBussinessService.detoken(Token), "仓库主档", null, "查看", "Code:" + Code + "-->Status:" + Status);
            return IPBussinessService.Inf00012GetList(Token, Code, Status, page, rows);
        }

        /// <summary>
        /// 保存仓库
        /// SAM 2017年5月4日11:02:15
        /// </summary>
        /// <param name="request">JSON数据</param>
        /// <returns></returns>
        [HttpPost]
        [Authenticate]
        public object Inf00012Save(JObject request)
        {
            DataLogerService.writeURL(request.Value<string>("Token"), request.ToString());
            UtilBussinessService.WriteLog(UtilBussinessService.detoken(request.Value<string>("Token")), "仓库主档", null, "保存", request.ToString());
            bool isDelete = false;
            bool isInsert = false;
            bool isUpdate = false;

            object deleted = null;
            object Insert = null;
            object Update = null;

            foreach (JProperty i in request.Children())
            {

                if (i.Name != "Token" && ((JArray)i.Value).Count > 0)
                {
                    switch (i.Name)
                    {
                        case "deleted": isDelete = true; break;
                        case "inserted": isInsert = true; break;
                        case "updated": isUpdate = true; break;
                    }
                }
            }

            if (isDelete)
                deleted = IPBussinessService.Inf00012delete(request.Value<string>("Token"), request.Value<JArray>("deleted"));
            if (isInsert)
                Insert = IPBussinessService.Inf00012insert(request.Value<string>("Token"), request.Value<JArray>("inserted"));
            if (isUpdate)
                Update = IPBussinessService.Inf00012update(request.Value<string>("Token"), request.Value<JArray>("updated"));

            return new { inserted = Insert, updated = Update, deleted = deleted };
        }

        /// <summary>
        /// 仓库的单一删除
        /// SAM 2017年7月24日10:24:57
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Authenticate]
        public object Inf00012Delete(JObject request)
        {
            DataLogerService.writeURL(request.Value<string>("Token"), request.ToString());
            UtilBussinessService.WriteLog(UtilBussinessService.detoken(request.Value<string>("Token")), "仓库主档", null, "删除", request.ToString());
            return IPBussinessService.Inf00012Delete(request);
        }
        #endregion

        #region Inf00013班别主档
        /// <summary>
        /// 获取班别列表
        /// Tom 2017年5月5日15:05:34
        /// </summary>
        /// <param name="Token">授权码</param>
        /// <param name="Code">代号</param>
        /// <param name="Status">状态</param>
        /// <param name="page">页码</param>
        /// <param name="rows">行数</param>
        /// <returns></returns>
        [HttpGet]
        [Authenticate]
        public object Inf00013List(string Token, string Code = null, string Status = null, int page = 1, int rows = 10)
        {
            DataLogerService.writeURL(Token, "Code:" + Code + "-->Status:" + Status);
            UtilBussinessService.WriteLog(UtilBussinessService.detoken(Token), "班别主档", null, "查看", "Code:" + Code + "-->Status:" + Status);
            return IPBussinessService.Inf00013List(Token, Code, Status, page, rows);
        }

        /// <summary>
        /// 班别保存
        /// Tom 2017年5月5日15:29:25
        /// </summary>
        /// <param name="request">JSON数据</param>
        /// <returns></returns>
        [HttpPost]
        [Authenticate]
        public object Inf00013Save(JObject request)
        {
            DataLogerService.writeURL(request.Value<string>("Token"), request.ToString());
            UtilBussinessService.WriteLog(UtilBussinessService.detoken(request.Value<string>("Token")), "班别主档", null, "保存", request.ToString());
            bool isDelete = false;
            bool isInsert = false;
            bool isUpdate = false;
            object deleted = null;
            object Insert = null;
            object Update = null;
            foreach (JProperty i in request.Children())
            {
                if (i.Name != "Token" && ((JArray)i.Value).Count > 0)
                {
                    switch (i.Name)
                    {
                        case "deleted": isDelete = true; break;
                        case "inserted": isInsert = true; break;
                        case "updated": isUpdate = true; break;
                    }
                }
            }
            if (isDelete)
                deleted = IPBussinessService.Inf00013Delete(request.Value<string>("Token"), (JArray)request.Value<JArray>("deleted"));
            if (isInsert)
                Insert = IPBussinessService.Inf00013Insert(request.Value<string>("Token"), (JArray)request.Value<JArray>("inserted"));
            if (isUpdate)
                Update = IPBussinessService.Inf00013Update(request.Value<string>("Token"), (JArray)request.Value<JArray>("updated"));

            return new { inserted = Insert, updated = Update, deleted = deleted };
        }

        /// <summary>
        /// 班别的删除
        /// Joint 2017年7月24日16:23:41
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Authenticate]
        public object Inf00013delect(JObject request)
        {
            DataLogerService.writeURL(request.Value<String>("Token"), request.ToString());
            UtilBussinessService.WriteLog(UtilBussinessService.detoken(request.Value<string>("Token")), "班别主档", null, "删除", request.ToString());
            return IPBussinessService.Inf00013delect(request);
        }
        #endregion

        #region Inf00014行事历主档
        /// <summary>
        /// 行事历的列表
        /// SAM 2017年5月8日17:46:28
        /// </summary>
        /// <param name="Token">授权码</param>
        /// <param name="Code">代号</param>
        /// <param name="page">页码</param>
        /// <param name="rows">行数</param>
        /// <returns></returns>
        [HttpGet]
        [Authenticate]
        public object Inf00014GetList(string Token, string Code = null, int page = 1, int rows = 10)
        {
            DataLogerService.writeURL(Token, "Code:" + Code);
            UtilBussinessService.WriteLog(UtilBussinessService.detoken(Token), " 行事历主档", null, "查看", "Code:" + Code);
            return IPBussinessService.Inf00014GetList(Token, Code, page, rows);
        }

        /// <summary>
        /// 行事历表头的保存（新增以及编辑）
        /// SAM 2017年7月23日22:30:44
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Authenticate]
        public object Inf00014Save(JObject request)
        {
            DataLogerService.writeURL(request.Value<string>("Token"), request.ToString());
            UtilBussinessService.WriteLog(UtilBussinessService.detoken(request.Value<string>("Token")), " 行事历", null, "保存", request.ToString());

            bool isInsert = false;
            bool isUpdate = false;

            object deleted = null;
            object Insert = null;
            object Update = null;

            foreach (JProperty i in request.Children())
            {

                if (i.Name != "Token" && ((JArray)i.Value).Count > 0)
                {
                    switch (i.Name)
                    {

                        case "inserted": isInsert = true; break;
                        case "updated": isUpdate = true; break;
                    }
                }
            }

            if (isInsert)
                Insert = IPBussinessService.Inf00014insert(request.Value<string>("Token"), request.Value<JArray>("inserted"));
            if (isUpdate)
                Update = IPBussinessService.Inf00014update(request.Value<string>("Token"), request.Value<JArray>("updated"));

            return new { inserted = Insert, updated = Update, deleted = deleted };
        }


        /// <summary>
        /// 行事历表头的删除
        /// SAM 2017年5月17日15:16:27
        /// </summary>
        /// <param name="request">JSON数据</param>
        /// <returns></returns>
        [HttpPost]
        [Authenticate]
        public object Inf00014Delete(JObject request)
        {
            string Token = request.Value<string>("Token");
            DataLogerService.writeURL(Token, request.ToString());
            UtilBussinessService.WriteLog(UtilBussinessService.detoken(Token), " 行事历", null, "删除", request.ToString());
            return IPBussinessService.Inf00014Delete(Token, request);
        }

        /// <summary>
        /// 行事历表头的编辑
        /// SAM 2017年5月17日15:34:20
        /// </summary>
        /// <param name="request">JSON数据</param>
        /// <returns></returns>
        [HttpPost]
        [Authenticate]
        public object Inf00014Update(JObject request)
        {
            string Token = request.Value<string>("Token");
            DataLogerService.writeURL(Token, request.ToString());
            UtilBussinessService.WriteLog(UtilBussinessService.detoken(Token), " 行事历", null, "编辑", request.ToString());
            return IPBussinessService.Inf00014Update(Token, request);
        }

        /// <summary>
        /// 根据行事历流水号获取明细
        /// SAM 2017年5月8日17:58:31
        /// </summary>
        /// <param name="Token">授权码</param>
        /// <param name="CalendarID"></param>
        /// <returns></returns>
        [HttpGet]
        [Authenticate]
        public object Inf00014GetDetailsList(string Token, string CalendarID)
        {
            DataLogerService.writeURL(Token, "CalendarID:" + CalendarID);
            UtilBussinessService.WriteLog(UtilBussinessService.detoken(Token), " 行事历明细", null, "查看", "CalendarID:" + CalendarID);
            return IPBussinessService.Inf00014GetDetailsList(Token, CalendarID);
        }

        /// <summary>
        /// 根据行事历和年月获取那个月的具体信息
        /// SAM 2017年5月9日09:29:39
        /// </summary>
        /// <param name="Token">授权码</param>
        /// <param name="CalendarID"></param>
        /// <param name="Month"></param>
        /// <returns></returns>
        [HttpGet]
        [Authenticate]
        public object Inf00014GetMonthList(string Token, string CalendarID, string Month)
        {
            DataLogerService.writeURL(Token, "CalendarID:" + CalendarID + "--->Month:" + Month);
            UtilBussinessService.WriteLog(UtilBussinessService.detoken(Token), " 行事历明细", null, "查看", "CalendarID:" + CalendarID);
            return IPBussinessService.Inf00014GetMonthList(Token, CalendarID, Month);
        }

        ///// <summary>
        ///// 行事历添加
        ///// SAM 2017年5月9日13:57:45
        ///// </summary>
        ///// <param name="request">JSON数据</param>
        ///// <returns></returns>
        //[HttpPost]
        //[Authenticate]
        //public object Inf00014Add(JObject request)
        //{
        //    string Token = request.Value<string>("Token");
        //    DataLogerService.writeURL(Token, request.ToString());
        //    UtilBussinessService.WriteLog(UtilBussinessService.detoken(Token), " 行事历", null, "新增", request.ToString());
        //    return IPBussinessService.Inf00014Add(Token, request);
        //}

        ///// <summary>
        ///// 判断指定时间内是否存在行事历期间
        ///// 存在返回true,不存在返回false
        ///// SAM 2017年6月13日14:16:29
        ///// </summary>
        ///// <param name="request"></param>
        ///// <returns></returns>
        //[HttpPost]
        //[Authenticate]
        //public object Inf00014Check(JObject request)
        //{
        //    string Token = request.Value<string>("Token");
        //    DataLogerService.writeURL(Token, request.ToString());
        //    return IPBussinessService.Inf00014Check(Token, request);
        //}

        /// <summary>
        /// 更新行事历
        /// SAM 2017年5月9日13:58:31
        /// </summary>
        /// <param name="request">JSON数据</param>
        /// <returns></returns>
        [HttpPost]
        [Authenticate]
        public object Inf00014UpdateDetails(JObject request)
        {
            string Token = request.Value<string>("Token");
            DataLogerService.writeURL(Token, request.ToString());
            UtilBussinessService.WriteLog(UtilBussinessService.detoken(Token), " 行事历", null, "更新", request.ToString());
            return IPBussinessService.Inf00014UpdateDetails(Token, request);
        }


        /// <summary>
        /// 行事历期间维护
        /// SAM 2017年7月23日22:51:09
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Authenticate]
        public object Inf00014PeriodUpdate(JObject request)
        {
            string Token = request.Value<string>("Token");
            DataLogerService.writeURL(Token, request.ToString());
            UtilBussinessService.WriteLog(UtilBussinessService.detoken(Token), " 行事历", null, "期间维护", request.ToString());
            return IPBussinessService.Inf00014PeriodUpdate(Token, request);
        }

        /// <summary>
        /// 判断是否已存在主行事历
        /// SAM 2017年7月28日11:51:48
        /// true代表存在，false代表不存在
        /// </summary>
        /// <param name="Token"></param>
        /// <returns></returns>
        [HttpGet]
        [Authenticate]
        public object Inf00014CheckIfdefault(string Token)
        {
            DataLogerService.writeURL(Token, null);
            return IPBussinessService.Inf00014CheckIfdefault(Token);
        }

        #endregion

        #region Inf00015资源主档
        /// <summary>
        /// 资源类别列表
        /// SAM 2017年5月12日10:11:28
        /// </summary>
        /// <param name="Token">授权码</param>
        /// <param name="Code">代号</param>
        /// <param name="Status">状态</param>
        /// <param name="page">页码</param>
        /// <param name="rows">行数</param>
        /// <returns></returns>
        [HttpGet]
        [Authenticate]
        public object Inf00015GetClassList(string Token, string Code = null, string Status = null, int page = 1, int rows = 10)
        {
            DataLogerService.writeURL(Token, "Code:" + Code);
            UtilBussinessService.WriteLog(UtilBussinessService.detoken(Token), "资源主档", null, "查看", "Code:" + Code + "--->Status:" + Status);
            return IPBussinessService.Inf00015ClassList(Token, Code, Status, page, rows);
        }

        /// <summary>
        /// 资源类别保存
        /// SAM 2017年5月12日10:38:33
        /// </summary>
        /// <param name="request">JSON数据</param>
        /// <returns></returns>
        [HttpPost]
        [Authenticate]
        public object Inf00015ClassSave(JObject request)
        {
            DataLogerService.writeURL(request.Value<string>("Token"), request.ToString());
            UtilBussinessService.WriteLog(UtilBussinessService.detoken(request.Value<string>("Token")), "资源主档", null, "保存", request.ToString());

            bool isInsert = false;
            bool isUpdate = false;

            object deleted = null;
            object Insert = null;
            object Update = null;

            foreach (JProperty i in request.Children())
            {

                if (i.Name != "Token" && ((JArray)i.Value).Count > 0)
                {
                    switch (i.Name)
                    {

                        case "inserted": isInsert = true; break;
                        case "updated": isUpdate = true; break;
                    }
                }
            }

            if (isInsert)
                Insert = IPBussinessService.Parameterinsert(request.Value<string>("Token"), request.Value<JArray>("inserted"), Framework.SystemID + "0191213000013");
            if (isUpdate)
                Update = IPBussinessService.Parameterupdate(request.Value<string>("Token"), request.Value<JArray>("updated"));

            return new { inserted = Insert, updated = Update, deleted = deleted };
        }

        /// <summary>
        /// 资源类别的单一删除
        /// SAM 2017年7月27日22:07:28
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Authenticate]
        public object Inf00015ClassDelete(JObject request)
        {
            string Token = request.Value<string>("Token");
            DataLogerService.writeURL(Token, request.ToString());
            UtilBussinessService.WriteLog(UtilBussinessService.detoken(Token), " 资源类别", null, "删除", request.ToString());
            return IPBussinessService.Inf00015ClassDelete(Token, request);
        }

        /// <summary>
        /// 资源群组列表
        /// SAM 2017年5月12日10:11:28
        /// </summary>
        /// <param name="Token">授权码</param>
        /// <param name="Code">代号</param>
        /// <param name="Status">状态</param>
        /// <param name="page">页码</param>
        /// <param name="rows">行数</param>
        /// <returns></returns>
        [HttpGet]
        [Authenticate]
        public object Inf00015GetGroupList(string Token, string Code = null, string Status = null, int page = 1, int rows = 10)
        {
            DataLogerService.writeURL(Token, "Code:" + Code);
            UtilBussinessService.WriteLog(UtilBussinessService.detoken(Token), "资源主档", null, "查看", "Code:" + Code + "--->Status:" + Status);
            return IPBussinessService.Inf00015GroupList(Token, Code, Status, page, rows);
        }

        /// <summary>
        /// 资源群组保存
        /// SAM 2017年5月12日10:38:33
        /// </summary>
        /// <param name="request">JSON数据</param>
        /// <returns></returns>
        [HttpPost]
        [Authenticate]
        public object Inf00015GroupSave(JObject request)
        {
            DataLogerService.writeURL(request.Value<string>("Token"), request.ToString());
            UtilBussinessService.WriteLog(UtilBussinessService.detoken(request.Value<string>("Token")), "资源主档", null, "保存", request.ToString());

            bool isInsert = false;
            bool isUpdate = false;

            object deleted = null;
            object Insert = null;
            object Update = null;

            foreach (JProperty i in request.Children())
            {

                if (i.Name != "Token" && ((JArray)i.Value).Count > 0)
                {
                    switch (i.Name)
                    {

                        case "inserted": isInsert = true; break;
                        case "updated": isUpdate = true; break;
                    }
                }
            }

            if (isInsert)
                Insert = IPBussinessService.Parameterinsert(request.Value<string>("Token"), request.Value<JArray>("inserted"), Framework.SystemID + "0191213000014");
            if (isUpdate)
                Update = IPBussinessService.Parameterupdate(request.Value<string>("Token"), request.Value<JArray>("updated"));

            return new { inserted = Insert, updated = Update, deleted = deleted };
        }

        /// <summary>
        /// 资源群组的单一删除
        /// SAM 2017年7月27日22:15:23
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Authenticate]
        public object Inf00015GroupDelete(JObject request)
        {
            string Token = request.Value<string>("Token");
            DataLogerService.writeURL(Token, request.ToString());
            UtilBussinessService.WriteLog(UtilBussinessService.detoken(Token), " 资源群组", null, "删除", request.ToString());
            return IPBussinessService.Inf00015GroupDelete(Token, request);
        }

        /// <summary>
        /// 资源列表
        /// SAM 2017年5月12日11:12:28
        /// </summary>
        /// <param name="Token">授权码</param>
        /// <param name="Code">代号</param>
        /// <param name="Status">状态</param>
        /// <param name="page">页码</param>
        /// <param name="rows">行数</param>
        /// <returns></returns>
        [HttpGet]
        [Authenticate]
        public object Inf00015GetList(string Token, string Code = null, string Status = null, int page = 1, int rows = 10)
        {
            DataLogerService.writeURL(Token, "Code:" + Code);
            UtilBussinessService.WriteLog(UtilBussinessService.detoken(Token), "资源主档", null, "查看", "Code:" + Code + "--->Status:" + Status);
            return IPBussinessService.Inf00015GetList(Token, Code, Status, page, rows);
        }

        /// <summary>
        /// 资源的单一删除
        /// SAM 2017年7月27日22:15:53
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Authenticate]
        public object Inf00015Delete(JObject request)
        {
            string Token = request.Value<string>("Token");
            DataLogerService.writeURL(Token, request.ToString());
            UtilBussinessService.WriteLog(UtilBussinessService.detoken(Token), " 资源", null, "删除", request.ToString());
            return IPBussinessService.Inf00015Delete(Token, request);
        }


        /// <summary>
        /// 资源的保存
        /// SAM 2017年5月12日11:12:56
        /// </summary>
        /// <param name="request">JSON数据</param>
        /// <returns></returns>
        [HttpPost]
        [Authenticate]
        public object Inf00015Save(JObject request)
        {
            DataLogerService.writeURL(request.Value<string>("Token"), request.ToString());
            UtilBussinessService.WriteLog(UtilBussinessService.detoken(request.Value<string>("Token")), "资源主档", null, "保存", request.ToString());

            bool isInsert = false;
            bool isUpdate = false;
            object deleted = null;
            object Insert = null;
            object Update = null;

            foreach (JProperty i in request.Children())
            {

                if (i.Name != "Token" && ((JArray)i.Value).Count > 0)
                {
                    switch (i.Name)
                    {

                        case "inserted": isInsert = true; break;
                        case "updated": isUpdate = true; break;
                    }
                }
            }

            if (isInsert)
                Insert = IPBussinessService.Inf00015insert(request.Value<string>("Token"), request.Value<JArray>("inserted"));
            if (isUpdate)
                Update = IPBussinessService.Inf00015update(request.Value<string>("Token"), request.Value<JArray>("updated"));

            return new { inserted = Insert, updated = Update, deleted = deleted };
        }

        /// <summary>
        /// 获取资源的明细列表
        /// SAM 2017年5月12日11:15:46
        /// </summary>
        /// <param name="Token">授权码</param>
        /// <param name="ResourceID"></param>
        /// <param name="page">页码</param>
        /// <param name="rows">行数</param>
        /// <returns></returns>
        [HttpGet]
        [Authenticate]
        public object Inf00015GetDetailsList(string Token, string ResourceID, int page = 1, int rows = 10)
        {
            DataLogerService.writeURL(Token, "ResourceID:" + ResourceID);
            UtilBussinessService.WriteLog(UtilBussinessService.detoken(Token), "资源主档", null, "查看", "ResourceID:" + ResourceID);
            return IPBussinessService.Inf00015GetDetailsList(Token, ResourceID, page, rows);
        }

        /// <summary>
        /// 保存资源的明细
        /// SAM 2017年5月12日11:16:21
        /// </summary>
        /// <param name="request">JSON数据</param>
        /// <returns></returns>
        [HttpPost]
        [Authenticate]
        public object Inf00015DetailsSave(JObject request)
        {
            DataLogerService.writeURL(request.Value<string>("Token"), request.ToString());
            UtilBussinessService.WriteLog(UtilBussinessService.detoken(request.Value<string>("Token")), "资源主档", null, "保存", request.ToString());
            bool isDelete = false;
            bool isInsert = false;

            object deleted = null;
            object Insert = null;
            object Update = null;

            foreach (JProperty i in request.Children())
            {

                if (i.Name != "Token" && ((JArray)i.Value).Count > 0)
                {
                    switch (i.Name)
                    {
                        case "deleted": isDelete = true; break;
                        case "inserted": isInsert = true; break;
                    }
                }
            }

            if (isDelete)
                deleted = IPBussinessService.Inf00015Detailsdelete(request.Value<string>("Token"), request.Value<JArray>("deleted"));
            if (isInsert)
                Insert = IPBussinessService.Inf00015Detailsinsert(request.Value<string>("Token"), request.Value<JArray>("inserted"));

            return new { inserted = Insert, updated = Update, deleted = deleted };
        }



        /// <summary>
        /// 根据资源流水号获取资源明细（不分页）
        /// SAM 2017年7月30日22:23:06
        /// </summary>
        /// <param name="Token"></param>
        /// <param name="ResourceID"></param>
        /// <returns></returns>
        [HttpGet]
        [Authenticate]
        public object Inf00015DetailList(string Token, string ResourceID)
        {
            DataLogerService.writeURL(Token, "ResourceID:" + ResourceID);
            return IPBussinessService.Inf00015DetailList(Token, ResourceID);
        }

        /// <summary>
        /// 根据资源流水号获取不属于他的资源明细（不分页）
        /// SAM 2017年7月30日22:23:06
        /// </summary>
        /// <param name="Token">授权码</param>
        /// <param name="ResourceID"></param>
        /// <returns></returns>
        [HttpGet]
        [Authenticate]
        public object Inf00015NoDetailList(string Token, string ResourceID)
        {
            DataLogerService.writeURL(Token, "ResourceID:" + ResourceID);
            return IPBussinessService.Inf00015NoDetailList(Token, ResourceID);
        }

        /// <summary>
        /// 根据资源流水号获取不属于他的资源明细列表（不分页）
        /// SAM 2017年10月26日21:59:17 
        /// 在原来的版本上，加多了一个代号的查询。
        /// </summary>
        /// <param name="Token"></param>
        /// <param name="ResourceID"></param>
        /// <param name="Code"></param>
        /// <returns></returns>
        [HttpGet]
        [Authenticate]
        public object Inf00015NoDetailListV1(string Token, string ResourceID,string Code =null)
        {
            DataLogerService.writeURL(Token, "ResourceID:" + ResourceID);
            return IPBussinessService.Inf00015NoDetailListV1(Token, ResourceID, Code);
        }

        /// <summary>
        /// 保存资源明细设定
        /// SAM 2017年7月30日22:23:06
        /// </summary>
        /// <param name="request">JSON数据</param>
        /// <returns></returns>
        [HttpPost]
        [Authenticate]
        public object Inf00015DetailSave(JObject request)
        {
            DataLogerService.writeURL(request.Value<string>("Token"), request.ToString());
            UtilBussinessService.WriteLog(UtilBussinessService.detoken(request.Value<string>("Token")), "资源明细设定", null, "保存", request.ToString());
            return IPBussinessService.Inf00015DetailSave(request);
        }


        #endregion

        #region Inf00016單據自動編號主档
        /// <summary>
        /// 获取單據種別的列表
        /// SAM 2017年5月29日23:45:15
        /// </summary>
        /// <param name="Token">授权码</param>
        /// <param name="Code">代号</param>
        /// <param name="Status">状态</param>
        /// <param name="page">页码</param>
        /// <param name="rows">行数</param>
        /// <returns></returns>
        [HttpGet]
        [Authenticate]
        public object Inf00016GetTypeList(string Token, string Code = null, string Status = null, int page = 1, int rows = 10)
        {
            DataLogerService.writeURL(Token, "Code:" + Code + "--->Status:" + Status);
            UtilBussinessService.WriteLog(UtilBussinessService.detoken(Token), "單據種別", null, "查看", "Code:" + Code + "--->Status:" + Status);
            return IPBussinessService.Inf00016GetTypeList(Token, Code, Status, page, rows);
        }

        /// <summary>
        /// 单据类别设定表
        /// SAM 2017年6月1日15:23:34
        /// </summary>
        /// <param name="Token">授权码</param>
        /// <param name="TypeCode"></param>
        /// <param name="Code">代号</param>
        /// <param name="Status">状态</param>
        /// <param name="page">页码</param>
        /// <param name="rows">行数</param>
        /// <returns></returns>
        [HttpGet]
        [Authenticate]
        public object Inf00016GetList(string Token, string TypeCode = null, string Code = null, string Status = null, int page = 1, int rows = 10)
        {
            DataLogerService.writeURL(Token, "Code:" + Code + "--->Status:" + Status + "--->TypeCode:" + TypeCode);
            UtilBussinessService.WriteLog(UtilBussinessService.detoken(Token), "单据类别设定", null, "查看", "Code:" + Code + "--->Status:" + Status + "--->TypeCode:" + TypeCode);
            return IPBussinessService.Inf00016GetList(Token, TypeCode, Code, Status, page, rows);
        }

        /// <summary>
        /// 单据类别设定表保存
        /// SAM 2017年6月1日15:35:00
        /// </summary>
        /// <param name="request">JSON数据</param>
        /// <returns></returns>
        [HttpPost]
        [Authenticate]
        public object Inf00016Save(JObject request)
        {
            DataLogerService.writeURL(request.Value<string>("Token"), request.ToString());
            UtilBussinessService.WriteLog(UtilBussinessService.detoken(request.Value<string>("Token")), "单据类别设定", null, "保存", request.ToString());

            bool isInsert = false;
            bool isUpdate = false;

            object deleted = null;
            object Insert = null;
            object Update = null;

            foreach (JProperty i in request.Children())
            {

                if (i.Name != "Token" && ((JArray)i.Value).Count > 0)
                {
                    switch (i.Name)
                    {

                        case "inserted": isInsert = true; break;
                        case "updated": isUpdate = true; break;
                    }
                }
            }

            if (isInsert)
                Insert = IPBussinessService.Inf00016insert(request.Value<string>("Token"), request.Value<JArray>("inserted"));
            if (isUpdate)
                Update = IPBussinessService.Inf00016update(request.Value<string>("Token"), request.Value<JArray>("updated"));

            return new { inserted = Insert, updated = Update, deleted = deleted };
        }

        /// <summary>
        /// 单据类别设定表删除
        /// Tom 2017年7月31日10:06:22
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Authenticate]
        public object Inf00016Delete(JObject request)
        {
            return IPBussinessService.Inf00016Delete(request);
        }

        /// <summary>
        /// 根据类别流水号获取单据状况
        /// SAM 2017年6月1日16:03:02
        /// </summary>
        /// <param name="Token">授权码</param>
        /// <param name="DTSID"></param>
        /// <param name="page">页码</param>
        /// <param name="rows">行数</param>
        /// <returns></returns>
        [HttpGet]
        [Authenticate]
        public object Inf00016GetAutoNumberList(string Token, string DTSID, int page = 1, int rows = 10)
        {
            DataLogerService.writeURL(Token, "DTSID:" + DTSID);
            UtilBussinessService.WriteLog(UtilBussinessService.detoken(Token), "单据类别设定", null, "查看", "DTSID:" + DTSID);
            return IPBussinessService.Inf00016GetAutoNumberList(Token, DTSID, page, rows);
        }


        /// <summary>
        /// 根据类别流水号获取权限列表
        /// SAM 2017年6月1日16:23:27
        /// </summary>
        /// <param name="Token">授权码</param>
        /// <param name="DTSID"></param>
        /// <param name="page">页码</param>
        /// <param name="rows">行数</param>
        /// <returns></returns>
        [HttpGet]
        [Authenticate]
        public object Inf00016GetAuthorityList(string Token, string DTSID, int page = 1, int rows = 10)
        {
            DataLogerService.writeURL(Token, "DTSID:" + DTSID);
            UtilBussinessService.WriteLog(UtilBussinessService.detoken(Token), "单据类别设定", null, "查看", "DTSID:" + DTSID);
            return IPBussinessService.Inf00016GetAuthorityList(Token, DTSID, page, rows);
        }

        /// <summary>
        /// 根据类别流水号获取权限（不分页）
        /// SAM 2017年6月1日16:38:17
        /// </summary>
        /// <param name="Token">授权码</param>
        /// <param name="DTSID"></param>
        /// <returns></returns>
        [HttpGet]
        [Authenticate]
        public object Inf00016AuthorityList(string Token, string DTSID)
        {
            DataLogerService.writeURL(Token, "DTSID:" + DTSID);
            UtilBussinessService.WriteLog(UtilBussinessService.detoken(Token), "单据类别设定", null, "查看", "DTSID:" + DTSID);
            return IPBussinessService.Inf00016AuthorityList(Token, DTSID);
        }

        /// <summary>
        /// 根据类别代号获取不归属于他的数据
        /// SAM 2017年6月1日16:47:12
        /// </summary>
        /// <param name="Token">授权码</param>
        /// <param name="DTSID"></param>
        /// <param name="Code"></param>
        /// <returns></returns>
        [HttpGet]
        [Authenticate]
        public object Inf00016GetNotAuthorityList(string Token, string DTSID, string Code = null)
        {
            DataLogerService.writeURL(Token, "DTSID:" + DTSID);
            UtilBussinessService.WriteLog(UtilBussinessService.detoken(Token), "单据类别设定", null, "查看", "DTSID:" + DTSID);
            return IPBussinessService.Inf00016GetNotAuthorityList(Token, DTSID, Code);
        }

        /// <summary>
        /// 保存权限控制
        /// SAM 2017年6月1日16:55:38
        /// </summary>
        /// <param name="request">JSON数据</param>
        /// <returns></returns>
        [HttpPost]
        [Authenticate]
        public object Inf00016AuthoritySave(JObject request)
        {
            DataLogerService.writeURL(request.Value<string>("Token"), request.ToString());
            UtilBussinessService.WriteLog(UtilBussinessService.detoken(request.Value<string>("Token")), "单据类别设定", null, "保存", request.ToString());
            return IPBussinessService.Inf00016AuthoritySave(request);
        }

        #endregion

        #region Inf00017原因码主档
        /// <summary>
        /// 原因群码列表
        /// SAM 2017年5月11日09:59:331
        /// </summary>
        /// <param name="Token">授权码</param>
        /// <param name="Code">代号</param>
        /// <param name="Status">状态</param>
        /// <param name="page">页码</param>
        /// <param name="rows">行数</param>
        /// <returns></returns>
        [HttpGet]
        [Authenticate]
        public object Inf00017GetGroupList(string Token, string Code = null, string Status = null, int page = 1, int rows = 10)
        {
            DataLogerService.writeURL(Token, "Code:" + Code);
            UtilBussinessService.WriteLog(UtilBussinessService.detoken(Token), "原因群码主档", null, "查看", "Code:" + Code + "--->Status:" + Status);
            return IPBussinessService.Inf00017GetGroupList(Token, Code, Status, page, rows);
        }

        /// <summary>
        /// 原因群码保存
        /// SAM 2017年5月11日10:01:22
        /// </summary>
        /// <param name="request">JSON数据</param>
        /// <returns></returns>
        [HttpPost]
        [Authenticate]
        public object Inf00017GroupSave(JObject request)
        {
            DataLogerService.writeURL(request.Value<string>("Token"), request.ToString());
            UtilBussinessService.WriteLog(UtilBussinessService.detoken(request.Value<string>("Token")), "原因码群码主档", null, "保存", request.ToString());
            bool isDelete = false;
            bool isInsert = false;
            bool isUpdate = false;

            object deleted = null;
            object Insert = null;
            object Update = null;

            foreach (JProperty i in request.Children())
            {

                if (i.Name != "Token" && ((JArray)i.Value).Count > 0)
                {
                    switch (i.Name)
                    {
                        case "deleted": isDelete = true; break;
                        case "inserted": isInsert = true; break;
                        case "updated": isUpdate = true; break;
                    }
                }
            }

            if (isDelete)
                deleted = IPBussinessService.Inf00017Groupdelete(request.Value<string>("Token"), request.Value<JArray>("deleted"));
            if (isInsert)
                Insert = IPBussinessService.Parameterinsert(request.Value<string>("Token"), request.Value<JArray>("inserted"), Framework.SystemID + "0191213000011");
            if (isUpdate)
                Update = IPBussinessService.Parameterupdate(request.Value<string>("Token"), request.Value<JArray>("updated"));

            return new { inserted = Insert, updated = Update, deleted = deleted };
        }

        /// <summary>
        /// 单个原因群码删除
        /// Mouse 2017年7月25日15:14:24
        /// </summary>
        /// <param name="request"></param>
        [HttpPost]
        [Authenticate]
        public object Inf00017GroupCodeDeleted(JObject request)
        {
            DataLogerService.writeURL(request.Value<string>("Token"), request.ToString());
            UtilBussinessService.WriteLog(UtilBussinessService.detoken(request.Value<string>("Token")), "原因码群码主档", null, "删除", request.ToString());
            return IPBussinessService.Inf00017GroupCodeDeleted(request);
        }

        /// <summary>
        /// 获取原因码列表
        /// SAM 2017年5月11日10:03:38
        /// </summary>
        /// <param name="Token">授权码</param>
        /// <param name="Code">代号</param>
        /// <param name="Status">状态</param>
        /// <param name="page">页码</param>
        /// <param name="rows">行数</param>
        /// <returns></returns>
        [HttpGet]
        [Authenticate]
        public object Inf00017GetList(string Token, string Code = null, string Status = null, int page = 1, int rows = 10)
        {
            DataLogerService.writeURL(Token, "Code:" + Code);
            UtilBussinessService.WriteLog(UtilBussinessService.detoken(Token), "原因码主档", null, "查看", "Code:" + Code + "--->Status:" + Status);
            return IPBussinessService.Inf00017GetList(Token, Code, Status, page, rows);
        }

        /// <summary>
        /// 原因码保存
        /// SAM 2017年5月11日10:04:48
        /// </summary>
        /// <param name="request">JSON数据</param>
        /// <returns></returns>
        [HttpPost]
        [Authenticate]
        public object Inf00017Save(JObject request)
        {
            DataLogerService.writeURL(request.Value<string>("Token"), request.ToString());
            UtilBussinessService.WriteLog(UtilBussinessService.detoken(request.Value<string>("Token")), "原因码主档", null, "保存", request.ToString());
            bool isDelete = false;
            bool isInsert = false;
            bool isUpdate = false;

            object deleted = null;
            object Insert = null;
            object Update = null;

            foreach (JProperty i in request.Children())
            {

                if (i.Name != "Token" && ((JArray)i.Value).Count > 0)
                {
                    switch (i.Name)
                    {
                        case "deleted": isDelete = true; break;
                        case "inserted": isInsert = true; break;
                        case "updated": isUpdate = true; break;
                    }
                }
            }

            if (isDelete)
                deleted = IPBussinessService.Parameterdelete(request.Value<string>("Token"), request.Value<JArray>("deleted"));
            if (isInsert)
                Insert = IPBussinessService.Inf00017insert(request.Value<string>("Token"), request.Value<JArray>("inserted"));
            if (isUpdate)
                Update = IPBussinessService.Inf00017update(request.Value<string>("Token"), request.Value<JArray>("updated"));

            return new { inserted = Insert, updated = Update, deleted = deleted };
        }
        /// <summary>
        /// 单一原因码代码删除
        /// Mouse 2017年7月25日14:57:06
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        //[HttpPost]
        //[Authenticate]
        public object Inf00017Deleted(JObject request)
        {
            DataLogerService.writeURL(request.Value<string>("Token"), request.ToString());
            UtilBussinessService.WriteLog(UtilBussinessService.detoken(request.Value<string>("Token")), "原因码主档", null, "删除", request.ToString());
            return IPBussinessService.Inf00017Deleted(request);
        }
        #endregion

        #region Inf00018工作中心主档
        /// <summary>
        /// 获取工序列表
        /// SAM 2017年5月24日16:25:00
        /// </summary>
        /// <param name="Token">授权码</param>
        /// <param name="Code">代号</param>
        /// <param name="Status">状态</param>
        /// <param name="page">页码</param>
        /// <param name="rows">行数</param>
        /// <returns></returns>
        [HttpGet]
        [Authenticate]
        public object Inf00018GetOperationList(string Token, string Code = null, string Status = null, int page = 1, int rows = 10)
        {
            DataLogerService.writeURL(Token, "Code:" + Code);
            UtilBussinessService.WriteLog(UtilBussinessService.detoken(Token), "工序主档", null, "查看", "Code:" + Code + "--->Status:" + Status);
            return IPBussinessService.Inf00018GetOperationList(Token, Code, Status, page, rows);
        }

        /// <summary>
        /// 工序的单一删除
        /// Joint 2017年7月26日10:49:49
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Authenticate]
        public object Inf00018OperationDelete(JObject request)
        {
            DataLogerService.writeURL(request.Value<string>("Token"), request.ToString());
            UtilBussinessService.WriteLog(UtilBussinessService.detoken(request.Value<string>("Token")), "工序主档", null, "删除", request.ToString());
            return IPBussinessService.Inf00018OperationDelete(request);
        }

        /// <summary>
        /// 工序的保存
        /// SAM 2017年5月24日17:23:38
        /// </summary>
        /// <param name="request">JSON数据</param>
        /// <returns></returns>
        [HttpPost]
        [Authenticate]
        public object Inf00018OperationSave(JObject request)
        {
            DataLogerService.writeURL(request.Value<string>("Token"), request.ToString());
            UtilBussinessService.WriteLog(UtilBussinessService.detoken(request.Value<string>("Token")), "工序主档", null, "保存", request.ToString());
            bool isDelete = false;
            bool isInsert = false;
            bool isUpdate = false;

            object deleted = null;
            object Insert = null;
            object Update = null;

            foreach (JProperty i in request.Children())
            {

                if (i.Name != "Token" && ((JArray)i.Value).Count > 0)
                {
                    switch (i.Name)
                    {
                        case "deleted": isDelete = true; break;
                        case "inserted": isInsert = true; break;
                        case "updated": isUpdate = true; break;
                    }
                }
            }

            if (isDelete)
                deleted = IPBussinessService.Inf00018Operationdelete(request.Value<string>("Token"), request.Value<JArray>("deleted"));
            if (isInsert)
                Insert = IPBussinessService.Parameterinsert(request.Value<string>("Token"), request.Value<JArray>("inserted"), Framework.SystemID + "0191213000016");
            if (isUpdate)
                Update = IPBussinessService.Inf00018Operationupdate(request.Value<string>("Token"), request.Value<JArray>("updated"));

            return new { inserted = Insert, updated = Update, deleted = deleted };
        }

        /// <summary>
        /// 根据工序流水号获取制程(不分页)
        /// Joint 2017年7月26日14:40:16
        /// </summary>
        /// <param name="Token"></param>
        /// <param name="OperationID"></param>
        /// <returns></returns>
        [HttpGet]
        [Authenticate]
        public object Inf00018GetNoPageProcessList(string Token, string OperationID)
        {
            DataLogerService.writeURL(Token, "OperationID:" + OperationID);
            return IPBussinessService.Inf00018GetProcessList(Token, OperationID);
        }

        /// <summary>
        /// 根据工序流水号获取不属于他的制程(不分页)
        /// Joint 2017年7月26日16:00:57
        /// </summary>
        /// <param name="Token"></param>
        /// <param name="OperationID"></param>
        /// <returns></returns>
        [HttpGet]
        [Authenticate]
        public object Inf00018GetNoProcessList(string Token, string OperationID)
        {
            DataLogerService.writeURL(Token, "OperationID:" + OperationID);
            return IPBussinessService.Inf00018GetNoProcessList(Token, OperationID);
        }

        /// <summary>
        /// 保存工序的制程列表
        /// Joint 2017年7月26日16:38:13
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Authenticate]
        public object Inf00018OperationProcessSave(JObject request)
        {
            DataLogerService.writeURL(request.Value<string>("Token"), request.ToString());
            UtilBussinessService.WriteLog(UtilBussinessService.detoken(request.Value<string>("Token")), "工序主档", null, "保存", request.ToString());
            return IPBussinessService.Inf00018OperationProcessSave(request);
        }

        /// <summary>
        /// 获取制程列表
        /// SAM 2017年5月24日16:26:05
        /// </summary>
        /// <param name="Token">授权码</param>
        /// <param name="Code">代号</param>
        /// <param name="Status">状态</param>
        /// <param name="page">页码</param>
        /// <param name="rows">行数</param>
        /// <returns></returns>
        [HttpGet]
        [Authenticate]
        public object Inf00018GetProcessList(string Token, string Code = null, string Status = null, int page = 1, int rows = 10)
        {
            DataLogerService.writeURL(Token, "Code:" + Code);
            UtilBussinessService.WriteLog(UtilBussinessService.detoken(Token), "制程主档", null, "查看", "Code:" + Code + "--->Status:" + Status);
            return IPBussinessService.Inf00018GetProcessList(Token, Code, Status, page, rows);
        }

        /// <summary>
        /// 制程的单一删除
        /// Joint 2017年7月28日11:16:54
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Authenticate]
        public object Inf00018ProcessDeleted(JObject request)
        {
            DataLogerService.writeURL(request.Value<string>("Token"), request.ToString());
            UtilBussinessService.WriteLog(UtilBussinessService.detoken(request.Value<string>("Token")), "制程主档", null, "删除", request.ToString());
            return IPBussinessService.Inf00018ProcessDeleted(request);
        }
        /// <summary>
        /// 制程的保存
        /// SAM 2017年5月24日17:24:06
        /// </summary>
        /// <param name="request">JSON数据</param>
        /// <returns></returns>
        [HttpPost]
        [Authenticate]
        public object Inf00018ProcessSave(JObject request)
        {
            DataLogerService.writeURL(request.Value<string>("Token"), request.ToString());
            UtilBussinessService.WriteLog(UtilBussinessService.detoken(request.Value<string>("Token")), "工序主档", null, "保存", request.ToString());
            bool isDelete = false;
            bool isInsert = false;
            bool isUpdate = false;

            object deleted = null;
            object Insert = null;
            object Update = null;

            foreach (JProperty i in request.Children())
            {

                if (i.Name != "Token" && ((JArray)i.Value).Count > 0)
                {
                    switch (i.Name)
                    {
                        case "deleted": isDelete = true; break;
                        case "inserted": isInsert = true; break;
                        case "updated": isUpdate = true; break;
                    }
                }
            }

            if (isDelete)
                deleted = IPBussinessService.Inf00018Processdelete(request.Value<string>("Token"), request.Value<JArray>("deleted"));
            if (isInsert)
                Insert = IPBussinessService.Inf00018Processinsert(request.Value<string>("Token"), request.Value<JArray>("inserted"));
            if (isUpdate)
                Update = IPBussinessService.Inf00018Processupdate(request.Value<string>("Token"), request.Value<JArray>("updated"));

            return new { inserted = Insert, updated = Update, deleted = deleted };
        }

        /// <summary>
        /// 根据制程流水号获取工序(不分页)
        /// Joint 2017年7月27日14:49:36
        /// </summary>
        /// <param name="Token"></param>
        /// <param name="ProcessID"></param>
        /// <returns></returns>
        [HttpGet]
        [Authenticate]
        public object Inf00018GetOperationList(string Token, string ProcessID)
        {
            DataLogerService.writeURL(Token, "ProcessID:" + ProcessID);
            return IPBussinessService.Inf00018GetOperationList(Token, ProcessID);
        }

        /// <summary>
        /// 根据制程流水号获取不属于他的工序(不分页)
        /// Joint 2017年7月27日14:50:07
        /// </summary>
        /// <param name="Token"></param>
        /// <param name="ProcessID"></param>
        /// <returns></returns>
        [HttpGet]
        [Authenticate]
        public object Inf00018GetNoOperationList(string Token, string ProcessID)
        {
            DataLogerService.writeURL(Token, "ProcessID:" + ProcessID);
            return IPBussinessService.Inf00018GetNoOperationList(Token, ProcessID);
        }

        /// <summary>
        /// 保存制程的工序列表
        /// Joint 2017年7月27日14:50:11
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Authenticate]
        public object Inf00018MasterProcessOperationSave(JObject request)
        {
            DataLogerService.writeURL(request.Value<string>("Token"), request.ToString());
            UtilBussinessService.WriteLog(UtilBussinessService.detoken(request.Value<string>("Token")), "制程主档", null, "保存", request.ToString());
            return IPBussinessService.Inf00018MasterProcessOperationSave(request);
        }

        /// <summary>
        /// 根据制程获取制程的工序
        /// SAM 2017年5月24日17:27:27
        /// </summary>
        /// <param name="Token">授权码</param>
        /// <param name="ProcessID"></param>
        /// <param name="page">页码</param>
        /// <param name="rows">行数</param>
        /// <returns></returns>
        [HttpGet]
        [Authenticate]
        public object Inf00018ProcessGetOperationList(string Token, string ProcessID, int page = 1, int rows = 10)
        {
            DataLogerService.writeURL(Token, "ProcessID:" + ProcessID);
            UtilBussinessService.WriteLog(UtilBussinessService.detoken(Token), "制程工序", null, "查看", "ProcessID:" + ProcessID);
            return IPBussinessService.Inf00018ProcessGetOperationList(Token, ProcessID, page, rows);
        }

        /// <summary>
        /// 制程工序的保存
        /// SAM 2017年5月24日17:43:56
        /// </summary>
        /// <param name="request">JSON数据</param>
        /// <returns></returns>
        [HttpPost]
        [Authenticate]
        public object Inf00018ProcessOperationSave(JObject request)
        {
            DataLogerService.writeURL(request.Value<string>("Token"), request.ToString());
            UtilBussinessService.WriteLog(UtilBussinessService.detoken(request.Value<string>("Token")), "工序主档", null, "保存", request.ToString());
            bool isDelete = false;
            bool isInsert = false;
            bool isUpdate = false;

            object deleted = null;
            object Insert = null;
            object Update = null;

            foreach (JProperty i in request.Children())
            {

                if (i.Name != "Token" && ((JArray)i.Value).Count > 0)
                {
                    switch (i.Name)
                    {
                        case "deleted": isDelete = true; break;
                        case "inserted": isInsert = true; break;
                        case "updated": isUpdate = true; break;
                    }
                }
            }

            if (isDelete)
                deleted = IPBussinessService.Inf00018ProcessOperationdelete(request.Value<string>("Token"), request.Value<JArray>("deleted"));
            if (isInsert)
                Insert = IPBussinessService.Inf00018ProcessOperationinsert(request.Value<string>("Token"), request.Value<JArray>("inserted"));
            if (isUpdate)
                Update = IPBussinessService.Inf00018ProcessOperationupdate(request.Value<string>("Token"), request.Value<JArray>("updated"));

            return new { inserted = Insert, updated = Update, deleted = deleted };
        }

        /// <summary>
        /// 根据制程获取工作中心列表（不分页）
        /// Joint 2017年7月27日15:25:13
        /// </summary>
        /// <param name="Token"></param>
        /// <param name="ProcessID"></param>
        /// <returns></returns>
        [HttpGet]
        [Authenticate]
        public object Inf00018GetWorkCenterProcessList(string Token, string ProcessID)
        {
            DataLogerService.writeURL(Token, "ProcessID:" + ProcessID);
            return IPBussinessService.Inf00018GetWorkCenterProcessList(Token, ProcessID);
        }

        /// <summary>
        /// 根据制程获取不属于他的工作中心列表(不分页)
        /// Joint 2017年7月27日16:17:05
        /// </summary>
        /// <param name="Token"></param>
        /// <param name="ProcessID"></param>
        /// <returns></returns>
        [HttpGet]
        [Authenticate]
        public object Inf00018GetNoWorkCenterProcessList(string Token, string ProcessID)
        {
            DataLogerService.writeURL(Token, "ProcessID:" + ProcessID);
            return IPBussinessService.Inf00018GetNoWorkCenterProcessList(Token, ProcessID);
        }

        /// <summary>
        /// 保存制程的工作中心列表
        /// Joint 2017年7月27日16:28:35
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Authenticate]
        public object Inf00018ProcessWorkCenterSave(JObject request)
        {
            DataLogerService.writeURL(request.Value<string>("Token"), request.ToString());
            UtilBussinessService.WriteLog(UtilBussinessService.detoken(request.Value<string>("Token")), "制程主档", null, "保存", request.ToString());
            return IPBussinessService.Inf00018ProcessWorkCenterSave(request);
        }
        /// <summary>
        /// 获取工作中心的列表
        /// SAM 2017年5月24日17:51:23
        /// </summary>
        /// <param name="Token">授权码</param>
        /// <param name="Code">代号</param>
        /// <param name="Status">状态</param>
        /// <param name="page">页码</param>
        /// <param name="rows">行数</param>
        /// <returns></returns>
        [HttpGet]
        [Authenticate]
        public object Inf00018GetWorkCenterList(string Token, string Code = null, string Status = null, int page = 1, int rows = 10)
        {
            DataLogerService.writeURL(Token, "Code:" + Code);
            UtilBussinessService.WriteLog(UtilBussinessService.detoken(Token), "工作中心主档", null, "查看", "Code:" + Code + "--->Status:" + Status);
            return IPBussinessService.Inf00018GetWorkCenterList(Token, Code, Status, page, rows);
        }

        /// <summary>
        /// 工作中心的单一删除
        /// Joint 2017年7月28日14:18:10
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Authenticate]
        public object Inf00018WorkCenterDeleted(JObject request)
        {
            DataLogerService.writeURL(request.Value<string>("Token"), request.ToString());
            UtilBussinessService.WriteLog(UtilBussinessService.detoken(request.Value<string>("Token")), "工作中心主档", null, "删除", request.ToString());
            return IPBussinessService.Inf00018WorkCenterDeleted(request);
        }

        /// <summary>
        /// 工作中心的保存
        /// SAM 2017年5月25日09:05:03
        /// </summary>
        /// <param name="request">JSON数据</param>
        /// <returns></returns>
        [HttpPost]
        [Authenticate]
        public object Inf00018WorkCenterSave(JObject request)
        {
            DataLogerService.writeURL(request.Value<string>("Token"), request.ToString());
            UtilBussinessService.WriteLog(UtilBussinessService.detoken(request.Value<string>("Token")), "工作中心主档", null, "保存", request.ToString());
            bool isDelete = false;
            bool isInsert = false;
            bool isUpdate = false;

            object deleted = null;
            object Insert = null;
            object Update = null;

            foreach (JProperty i in request.Children())
            {

                if (i.Name != "Token" && ((JArray)i.Value).Count > 0)
                {
                    switch (i.Name)
                    {
                        case "deleted": isDelete = true; break;
                        case "inserted": isInsert = true; break;
                        case "updated": isUpdate = true; break;
                    }
                }
            }

            if (isDelete)
                deleted = IPBussinessService.Inf00018WorkCenterdelete(request.Value<string>("Token"), request.Value<JArray>("deleted"));
            if (isInsert)
                Insert = IPBussinessService.Inf00018WorkCenterinsert(request.Value<string>("Token"), request.Value<JArray>("inserted"));
            if (isUpdate)
                Update = IPBussinessService.Inf00018WorkCenterupdate(request.Value<string>("Token"), request.Value<JArray>("updated"));

            return new { inserted = Insert, updated = Update, deleted = deleted };
        }

        /// <summary>
        /// 根据工作中心获取制程列表
        /// SAM 2017年5月25日09:18:31
        /// </summary>
        /// <param name="Token">授权码</param>
        /// <param name="WorkCenterID"></param>
        /// <param name="page">页码</param>
        /// <param name="rows">行数</param>
        /// <returns></returns>
        [HttpGet]
        [Authenticate]
        public object Inf00018WorkCenterGetProcessList(string Token, string WorkCenterID, int page = 1, int rows = 10)
        {
            DataLogerService.writeURL(Token, "WorkCenterID:" + WorkCenterID);
            UtilBussinessService.WriteLog(UtilBussinessService.detoken(Token), "工作中心制程", null, "查看", "WorkCenterID:" + WorkCenterID);
            return IPBussinessService.Inf00018WorkCenterGetProcessList(Token, WorkCenterID, page, rows);
        }

        /// <summary>
        /// 工作中心制程的保存
        /// SAM 2017年5月25日09:19:06
        /// </summary>
        /// <param name="request">JSON数据</param>
        /// <returns></returns>
        [HttpPost]
        [Authenticate]
        public object Inf00018WorkCenterProcessSave(JObject request)
        {
            DataLogerService.writeURL(request.Value<string>("Token"), request.ToString());
            UtilBussinessService.WriteLog(UtilBussinessService.detoken(request.Value<string>("Token")), "工作中心主档", null, "保存", request.ToString());
            bool isDelete = false;
            bool isInsert = false;
            bool isUpdate = false;

            object deleted = null;
            object Insert = null;
            object Update = null;

            foreach (JProperty i in request.Children())
            {

                if (i.Name != "Token" && ((JArray)i.Value).Count > 0)
                {
                    switch (i.Name)
                    {
                        case "deleted": isDelete = true; break;
                        case "inserted": isInsert = true; break;
                        case "updated": isUpdate = true; break;
                    }
                }
            }

            if (isDelete)
                deleted = IPBussinessService.Inf00018WorkCenterProcessdelete(request.Value<string>("Token"), request.Value<JArray>("deleted"));
            if (isInsert)
                Insert = IPBussinessService.Inf00018WorkCenterProcessinsert(request.Value<string>("Token"), request.Value<JArray>("inserted"));
            if (isUpdate)
                Update = IPBussinessService.Inf00018WorkCenterProcessupdate(request.Value<string>("Token"), request.Value<JArray>("updated"));

            return new { inserted = Insert, updated = Update, deleted = deleted };
        }

        /// <summary>
        /// 根据工作中心获取资源列表
        /// SAM 2017年5月25日09:19:44
        /// </summary>
        /// <param name="Token">授权码</param>
        /// <param name="WorkCenterID"></param>
        /// <param name="page">页码</param>
        /// <param name="rows">行数</param>
        /// <returns></returns>
        [HttpGet]
        [Authenticate]
        public object Inf00018WorkCenterGetResourcesList(string Token, string WorkCenterID, int page = 1, int rows = 10)
        {
            DataLogerService.writeURL(Token, "WorkCenterID:" + WorkCenterID);
            UtilBussinessService.WriteLog(UtilBussinessService.detoken(Token), "工作中心资源", null, "查看", "WorkCenterID:" + WorkCenterID);
            return IPBussinessService.Inf00018WorkCenterGetResourcesList(Token, WorkCenterID, page, rows);
        }

        /// <summary>
        /// 工作中心资源的保存
        /// SAM 2017年5月25日09:20:17
        /// </summary>
        /// <param name="request">JSON数据</param>
        /// <returns></returns>
        [HttpPost]
        [Authenticate]
        public object Inf00018WorkCenterResourcesSave(JObject request)
        {
            DataLogerService.writeURL(request.Value<string>("Token"), request.ToString());
            UtilBussinessService.WriteLog(UtilBussinessService.detoken(request.Value<string>("Token")), "工作中心资源", null, "保存", request.ToString());
            bool isDelete = false;
            bool isInsert = false;
            bool isUpdate = false;

            object deleted = null;
            object Insert = null;
            object Update = null;

            foreach (JProperty i in request.Children())
            {

                if (i.Name != "Token" && ((JArray)i.Value).Count > 0)
                {
                    switch (i.Name)
                    {
                        case "deleted": isDelete = true; break;
                        case "inserted": isInsert = true; break;
                        case "updated": isUpdate = true; break;
                    }
                }
            }

            if (isDelete)
                deleted = IPBussinessService.Inf00018WorkCenterResourcesdelete(request.Value<string>("Token"), request.Value<JArray>("deleted"));
            if (isInsert)
                Insert = IPBussinessService.Inf00018WorkCenterResourcesinsert(request.Value<string>("Token"), request.Value<JArray>("inserted"));
            if (isUpdate)
                Update = IPBussinessService.Inf00018WorkCenterResourcesupdate(request.Value<string>("Token"), request.Value<JArray>("updated"));

            return new { inserted = Insert, updated = Update, deleted = deleted };
        }



        /// <summary>
        /// 根据工作中心获取制程列表（不分页）
        /// Joint 2017年7月28日16:49:38
        /// </summary>
        /// <param name="Token"></param>
        /// <param name="WorkCenterID"></param>
        /// <returns></returns>
        [HttpGet]
        [Authenticate]
        public object Inf00018GetProcessWorkCenterList(string Token, string WorkCenterID)
        {
            DataLogerService.writeURL(Token, "WorkCenterID:" + WorkCenterID);
            return IPBussinessService.Inf00018GetProcessWorkCenterList(Token, WorkCenterID);
        }

        /// <summary>
        /// 根据工作中心获取不属于他的制程列表(不分页)
        /// Joint 2017年7月28日16:49:43
        /// </summary>
        /// <param name="Token"></param>
        /// <param name="WorkCenterID"></param>
        /// <returns></returns>
        [HttpGet]
        [Authenticate]
        public object Inf00018GetNoProcessWorkCenterList(string Token, string WorkCenterID)
        {
            DataLogerService.writeURL(Token, "WorkCenterID:" + WorkCenterID);
            return IPBussinessService.Inf00018GetNoProcessWorkCenterList(Token, WorkCenterID);
        }

        /// <summary>
        /// 保存工作中心的制程列表
        /// Joint 2017年7月28日16:50:02
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>  
        [HttpPost]
        [Authenticate]
        public object Inf00018MasterWorkCenterProcessSave(JObject request)
        {
            DataLogerService.writeURL(request.Value<string>("Token"), request.ToString());
            UtilBussinessService.WriteLog(UtilBussinessService.detoken(request.Value<string>("Token")), "制程主档", null, "保存", request.ToString());
            return IPBussinessService.Inf00018MasterWorkCenterProcessSave(request);
        }

        /// <summary>
        /// 工作中心的制程列表移除-判断
        /// Joint 2017年7月28日18:04:51
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Authenticate]
        public object Inf00018MasterWorkCenterProcessDelete(JObject request)
        {
            DataLogerService.writeURL(request.Value<string>("Token"), request.ToString());
            UtilBussinessService.WriteLog(UtilBussinessService.detoken(request.Value<string>("Token")), "制程主档", null, "检验", request.ToString());
            return IPBussinessService.Inf00018MasterWorkCenterProcessDelete(request);
        }

        /// <summary>
        /// 工作中心的制程列表移除
        /// Joint 2017年7月31日09:31:33
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Authenticate]
        public object Inf00018MasterWorkCenterProcessDeleted(JObject request)
        {
            DataLogerService.writeURL(request.Value<string>("Token"), request.ToString());
            UtilBussinessService.WriteLog(UtilBussinessService.detoken(request.Value<string>("Token")), "制程主档", null, "移除", request.ToString());
            return IPBussinessService.Inf00018MasterWorkCenterProcessDeleted(request);
        }

        /// <summary>
        /// 根据工作中心获取资源列表（不分页）
        /// Joint 2017年7月31日11:05:48
        /// </summary>
        /// <param name="Token"></param>
        /// <param name="WorkCenterID"></param>
        /// <returns></returns>
        [HttpGet]
        [Authenticate]
        public object Inf00018GetWorkCenterResourceList(string Token, string WorkCenterID)
        {
            DataLogerService.writeURL(Token, "WorkCenterID:" + WorkCenterID);
            return IPBussinessService.Inf00018GetWorkCenterResourceList(Token, WorkCenterID);
        }

        /// <summary>
        /// 根据工作中心获取不属于它的资源列表（不分页）
        /// Joint 2017年7月31日11:57:04
        /// </summary>
        /// <param name="Token"></param>
        /// <param name="WorkCenterID"></param>
        /// <returns></returns>
        [HttpGet]
        [Authenticate]
        public object Inf00018GetNoWorkCenterResourceList(string Token, string WorkCenterID)
        {
            DataLogerService.writeURL(Token, "WorkCenterID:" + WorkCenterID);
            return IPBussinessService.Inf00018GetNoWorkCenterResourceList(Token, WorkCenterID);
        }

        /// <summary>
        /// 工作中心资源保存
        /// Joint 2017年7月31日14:53:31
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Authenticate]
        public object Inf00018WorkCenterResourceSave(JObject request)
        {
            DataLogerService.writeURL(request.Value<string>("Token"), request.ToString());
            UtilBussinessService.WriteLog(UtilBussinessService.detoken(request.Value<string>("Token")), "资源主档", null, "保存", request.ToString());
            return IPBussinessService.Inf00018WorkCenterResourceSave(request);
        }

        /// <summary>
        /// 制程下拉框
        /// Joint 2017年7月31日15:39:53
        /// </summary>
        /// <param name="Token"></param>
        /// <param name="WorkCenterID"></param>
        /// <returns></returns>
        [HttpGet]
        [Authenticate]
        public object Inf00018WorkCenterResourceProcess(string Token, string WorkCenterID)
        {
            DataLogerService.writeURL(Token, "WorkCenterID:" + WorkCenterID);
            return IPBussinessService.Inf00018WorkCenterResourceProcess(Token, WorkCenterID);
        }

        /// <summary>
        /// 工序下拉框
        /// Joint 2017年7月31日18:05:47
        /// </summary>
        /// <param name="Token"></param>
        /// <param name="ProcessID"></param>
        /// <returns></returns>
        [HttpGet]
        [Authenticate]
        public object Inf00018WorkCenterResourceOperation(string Token, string ProcessID)
        {
            DataLogerService.writeURL(Token, "ProcessID:" + ProcessID);
            return IPBussinessService.Inf00018WorkCenterResourceOperation(Token, ProcessID);
        }
        #endregion

        #region Inf00019项目主档

        /// <summary>
        /// 项目列表
        /// Tom 2017年5月9日11:54:19
        /// </summary>
        /// <param name="Token"></param>
        /// <param name="Code"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <returns></returns>
        [HttpGet]
        [Authenticate]
        public object Inf00019List(string Token, string Code = null, int page = 1, int rows = 10)
        {
            return IPBussinessService.Inf00019List(Token, Code, page, rows);
        }

        /// <summary>
        /// 项目保存
        /// Tom 2017年5月9日11:52:27
        /// </summary>
        /// <param name="request">JSON数据</param>
        /// <returns></returns>
        [HttpPost]
        [Authenticate]
        public object Inf00019Save(JObject request)
        {
            DataLogerService.writeURL(request.Value<string>("Token"), request.ToString());
            bool isDelete = false;
            bool isInsert = false;
            bool isUpdate = false;
            object deleted = null;
            object Insert = null;
            object Update = null;
            foreach (JProperty i in request.Children())
            {
                if (i.Name != "Token" && ((JArray)i.Value).Count > 0)
                {
                    switch (i.Name)
                    {
                        case "deleted": isDelete = true; break;
                        case "inserted": isInsert = true; break;
                        case "updated": isUpdate = true; break;
                    }
                }
            }
            if (isDelete)
                deleted = IPBussinessService.Inf00019Delete(request.Value<string>("Token"), (JArray)request.Value<JArray>("deleted"));
            if (isInsert)
                Insert = IPBussinessService.Inf00019Insert(request.Value<string>("Token"), (JArray)request.Value<JArray>("inserted"));
            if (isUpdate)
                Update = IPBussinessService.Inf00019Update(request.Value<string>("Token"), (JArray)request.Value<JArray>("updated"));

            return new { inserted = Insert, updated = Update, deleted = deleted };
        }

        /// <summary>
        /// 项目的单一删除
        /// SAM 2017年7月24日10:04:54
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Authenticate]
        public object Inf00019Delete(JObject request)
        {
            DataLogerService.writeURL(request.Value<string>("Token"), request.ToString());
            UtilBussinessService.WriteLog(UtilBussinessService.detoken(request.Value<string>("Token")), "项目主档", null, "删除", request.ToString());
            return IPBussinessService.Inf00019Delete(request);
        }

        #endregion

        #region Inf00020系统参数（MES）
        /// <summary>
        /// 系统参数列表
        /// SAM 2017年7月31日00:06:51
        /// </summary>
        /// <param name="Token"></param>
        /// <param name="Module"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <returns></returns>
        [HttpGet]
        [Authenticate]
        public object Inf00020GetList(string Token, string Module = null, int page = 1, int rows = 10)
        {
            return IPBussinessService.Inf00020GetList(Token, Module, page, rows);
        }

        /// <summary>
        /// 系统参数-保存
        /// SAM 2017年7月31日00:09:30
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Authenticate]
        public object Inf00020Save(JObject request)
        {
            DataLogerService.writeURL(request.Value<string>("Token"), request.ToString());

            bool isUpdate = false;
            object deleted = null;
            object Insert = null;
            object Update = null;
            foreach (JProperty i in request.Children())
            {
                if (i.Name != "Token" && ((JArray)i.Value).Count > 0)
                {
                    switch (i.Name)
                    {

                        case "updated": isUpdate = true; break;
                    }
                }
            }
            if (isUpdate)
                Update = IPBussinessService.Inf00020Update(request.Value<string>("Token"), (JArray)request.Value<JArray>("updated"));

            return new { inserted = Insert, updated = Update, deleted = deleted };
        }

        /// <summary>
        /// 系统参数-获取语序列表
        /// SAM 2017年8月24日10:50:50
        /// </summary>
        /// <param name="Token"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Authenticate]
        public object Inf00020GetLanguageList(string Token, string id)
        {
            DataLogerService.writeURL(Token, "id:" + id);
            UtilBussinessService.WriteLog(UtilBussinessService.detoken(Token), "语序", null, "查看", null);
            return IPBussinessService.Inf00020GetLanguageList(id);
        }

        /// <summary>
        /// 系统参数-语序的保存
        /// SAM 2017年8月24日10:54:07
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Authenticate]
        public object Inf00020LanguageSave(JObject request)
        {
            DataLogerService.writeURL(request.Value<string>("Token"), request.ToString());
            UtilBussinessService.WriteLog(UtilBussinessService.detoken(request.Value<string>("Token")), "语序", null, "保存", request.ToString());
            bool isDelete = false;
            bool isInsert = false;
            bool isUpdate = false;

            object deleted = null;
            object Insert = null;
            object Update = null;

            foreach (JProperty i in request.Children())
            {

                if (i.Name != "Token" && ((JArray)i.Value).Count > 0)
                {
                    switch (i.Name)
                    {
                        case "deleted": isDelete = true; break;
                        case "inserted": isInsert = true; break;
                        case "updated": isUpdate = true; break;
                    }
                }
            }

            if (isDelete)
                deleted = IPBussinessService.Languagedelete(request.Value<JArray>("deleted"));
            if (isInsert)
                Insert = IPBussinessService.Inf00020Languageinsert(request.Value<string>("Token"), request.Value<JArray>("inserted"));
            if (isUpdate)
                Update = IPBussinessService.Inf00020Languageupdate(request.Value<string>("Token"), request.Value<JArray>("updated"));


            return new { inserted = Insert, updated = Update, deleted = deleted };
        }

        /// <summary>
        /// 获取系统的品质判定
        /// Mouse 2017年8月28日10:08:00
        /// </summary>
        /// <param name="Token"></param>
        /// <returns></returns>
        [HttpGet]
        [Authenticate]
        public object Inf00020GetValue(string Token)
        {
            DataLogerService.writeURL(Token, null);
            UtilBussinessService.WriteLog(UtilBussinessService.detoken(Token), "系统参数主档", null, "查看", null);
            return IPBussinessService.Inf00020GetValue(Token);
        }
        #endregion

        #region Inf00021制程替代群组主档
        /// <summary>
        /// 制程替代群组列表
        /// SAM 2017年5月23日09:28:25
        /// </summary>
        /// <param name="Token">授权码</param>
        /// <param name="Code">代号</param>
        /// <param name="Status">状态</param>
        /// <param name="page">页码</param>
        /// <param name="rows">行数</param>
        /// <returns></returns>
        [HttpGet]
        [Authenticate]
        public object Inf00021GetList(string Token, string Code = null, string Status = null, int page = 1, int rows = 10)
        {
            DataLogerService.writeURL(Token, "Code:" + Code);
            UtilBussinessService.WriteLog(UtilBussinessService.detoken(Token), "制程替代群组主档", null, "查看", "Code:" + Code + "--->Status:" + Status);
            return IPBussinessService.Inf00021GetList(Token, Code, Status, page, rows);
        }

        /// <summary>
        /// 制程替代群组保存
        /// SAM 2017年5月12日10:12:03
        /// </summary>
        /// <param name="request">JSON数据</param>
        /// <returns></returns>
        [HttpPost]
        [Authenticate]
        public object Inf00021Save(JObject request)
        {
            DataLogerService.writeURL(request.Value<string>("Token"), request.ToString());
            UtilBussinessService.WriteLog(UtilBussinessService.detoken(request.Value<string>("Token")), "制程替代群组主档", null, "保存", request.ToString());

            bool isInsert = false;
            bool isUpdate = false;

            object deleted = null;
            object Insert = null;
            object Update = null;

            foreach (JProperty i in request.Children())
            {

                if (i.Name != "Token" && ((JArray)i.Value).Count > 0)
                {
                    switch (i.Name)
                    {

                        case "inserted": isInsert = true; break;
                        case "updated": isUpdate = true; break;
                    }
                }
            }

            //if (isDelete)
            //    deleted = IPBussinessService.Inf00021delete(request.Value<string>("Token"), request.Value<JArray>("deleted"));
            if (isInsert)
                Insert = IPBussinessService.Parameterinsert(request.Value<string>("Token"), request.Value<JArray>("inserted"), Framework.SystemID + "0191213000015");
            if (isUpdate)
                Update = IPBussinessService.Parameterupdate(request.Value<string>("Token"), request.Value<JArray>("updated"));

            return new { inserted = Insert, updated = Update, deleted = deleted };
        }

        /// <summary>
        /// 删除制程替代群组
        /// Tom 2017年7月27日17:47:26
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Authenticate]
        public object Inf00021Delete(JObject request)
        {
            return IPBussinessService.Inf00021Delete(request);
        }

        /// <summary>
        /// 根据替代群组获取他的替代制程
        /// SAM 2017年5月25日14:21:48
        /// </summary>
        /// <param name="Token">授权码</param>
        /// <param name="GroupID"></param>
        /// <param name="page">页码</param>
        /// <param name="rows">行数</param>
        /// <returns></returns>
        [HttpGet]
        [Authenticate]
        public object Inf00021GetDetailsList(string Token, string GroupID, int page = 1, int rows = 10)
        {
            DataLogerService.writeURL(Token, "GroupID:" + GroupID);
            UtilBussinessService.WriteLog(UtilBussinessService.detoken(Token), "制程替代群组明细", null, "查看", "GroupID:" + GroupID);
            return IPBussinessService.Inf00021GetDetailsList(Token, GroupID, page, rows);
        }

        /// <summary>
        /// 根据替代群组获取他的替代制程（不分页）
        /// SAM 2017年5月25日14:17:04
        /// </summary>
        /// <param name="Token">授权码</param>
        /// <param name="GroupID"></param>
        /// <returns></returns>
        [HttpGet]
        [Authenticate]
        public object Inf00021DetailsList(string Token, string GroupID)
        {
            DataLogerService.writeURL(Token, "GroupID:" + GroupID);
            return IPBussinessService.Inf00021DetailsList(Token, GroupID);
        }

        /// <summary>
        /// 根据替代群组获取不属于他的制程列表
        /// SAM 2017年5月25日14:23:48
        /// </summary>
        /// <param name="Token">授权码</param>
        /// <param name="GroupID"></param>
        /// <param name="Code"></param>
        /// <returns></returns>
        [HttpGet]
        [Authenticate]
        public object Inf00021ProcessList(string Token, string GroupID, string Code)
        {
            DataLogerService.writeURL(Token, "GroupID:" + GroupID);
            return IPBussinessService.Inf00021ProcessList(Token, GroupID, Code);
        }

        /// <summary>
        /// 保存替代群组的明细
        /// SAM 2017年5月25日14:39:38
        /// </summary>
        /// <param name="request">JSON数据</param>
        /// <returns></returns>
        [HttpPost]
        [Authenticate]
        public object Inf00021DetailsSave(JObject request)
        {
            DataLogerService.writeURL(request.Value<string>("Token"), request.ToString());
            UtilBussinessService.WriteLog(UtilBussinessService.detoken(request.Value<string>("Token")), "制程替代群组主档", null, "保存", request.ToString());
            return IPBussinessService.Inf00021DetailsSave(request);
        }
        #endregion

        #region Inf00023批号自动编号主档
        /// <summary>
        /// 批号自动编号设定列表
        /// SAM 2017年5月17日15:42:39
        /// </summary>
        /// <param name="Token">授权码</param>
        /// <param name="Code">代号</param>
        /// <param name="page">页码</param>
        /// <param name="rows">行数</param>
        /// <returns></returns>
        [HttpGet]
        [Authenticate]
        public object Inf00023GetList(string Token, string Code = null, int page = 1, int rows = 10)
        {
            DataLogerService.writeURL(Token, "Code:" + Code);
            UtilBussinessService.WriteLog(UtilBussinessService.detoken(Token), "批号自动编号主档", null, "查看", "Code:" + Code);
            return IPBussinessService.Inf00023GetList(Token, Code, page, rows);
        }

        /// <summary>
        /// 获取正常批号自动编号列表
        /// Tom 2017年6月23日17点42分
        /// </summary>
        /// <param name="Token"></param>
        /// <param name="Code"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <returns></returns>
        [HttpGet]
        [Authenticate]
        public object Inf00023GetVaildList(string Token, string Code = null, int page = 1, int rows = 10)
        {
            DataLogerService.writeURL(Token, "Code:" + Code);
            UtilBussinessService.WriteLog(UtilBussinessService.detoken(Token), "批号自动编号主档", null, "查看", "Code:" + Code);
            return IPBussinessService.Inf00023GetVaildList(Token, Code, page, rows);
        }

        /// <summary>
        /// 批号自动编号设定保存
        /// SAM 2017年5月17日15:53:38
        /// </summary>
        /// <param name="request">JSON数据</param>
        /// <returns></returns>
        [HttpPost]
        [Authenticate]
        public object Inf00023Save(JObject request)
        {
            DataLogerService.writeURL(request.Value<string>("Token"), request.ToString());
            UtilBussinessService.WriteLog(UtilBussinessService.detoken(request.Value<string>("Token")), "批号自动编号主档", null, "保存", request.ToString());
            bool isDelete = false;
            bool isInsert = false;
            bool isUpdate = false;

            object deleted = null;
            object Insert = null;
            object Update = null;

            foreach (JProperty i in request.Children())
            {

                if (i.Name != "Token" && ((JArray)i.Value).Count > 0)
                {
                    switch (i.Name)
                    {
                        case "deleted": isDelete = true; break;
                        case "inserted": isInsert = true; break;
                        case "updated": isUpdate = true; break;
                    }
                }
            }

            if (isDelete)
                deleted = IPBussinessService.Inf00023delete(request.Value<string>("Token"), request.Value<JArray>("deleted"));
            if (isInsert)
                Insert = IPBussinessService.Inf00023insert(request.Value<string>("Token"), request.Value<JArray>("inserted"));
            if (isUpdate)
                Update = IPBussinessService.Inf00023update(request.Value<string>("Token"), request.Value<JArray>("updated"));

            return new { inserted = Insert, updated = Update, deleted = deleted };
        }

        /// <summary>
        /// 批号自动编号设定的删除
        /// SAM 2017年7月23日23:21:48
        /// TODO 未完成
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Authenticate]
        public object Inf00023Delete(JObject request)
        {
            string Token = request.Value<string>("Token");
            DataLogerService.writeURL(Token, request.ToString());
            UtilBussinessService.WriteLog(UtilBussinessService.detoken(Token), " 批号自动编号设定", null, "删除", request.ToString());
            return IPBussinessService.Inf00023Delete(Token, request);
        }


        /// <summary>
        /// 批號紀錄列表
        /// SAM 2017年5月17日16:00:31
        /// </summary>
        /// <param name="Token">授权码</param>
        /// <param name="AutoNumberID"></param>
        /// <param name="page">页码</param>
        /// <param name="rows">行数</param>
        /// <returns></returns>
        [HttpGet]
        [Authenticate]
        public object Inf00023RecordGetList(string Token, string AutoNumberID, int page = 1, int rows = 10)
        {
            DataLogerService.writeURL(Token, "AutoNumberID:" + AutoNumberID);
            UtilBussinessService.WriteLog(UtilBussinessService.detoken(Token), "批号自动编号设定", null, "查看", "AutoNumberID:" + AutoNumberID);
            return IPBussinessService.Inf00023RecordGetList(Token, AutoNumberID, page, rows);
        }

        /// <summary>
        /// 获取批号记录明细
        /// SAM 2017年7月27日02:07:21
        /// TODO
        /// </summary>
        /// <param name="Token"></param>
        /// <param name="AutoNumberRecordID"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <returns></returns>
        [HttpGet]
        [Authenticate]
        public object Inf00023RecordDetailList(string Token, string AutoNumberRecordID, int page = 1, int rows = 10)
        {
            DataLogerService.writeURL(Token, "Code:" + AutoNumberRecordID);
            UtilBussinessService.WriteLog(UtilBussinessService.detoken(Token), "批号自动编号主档", null, "查看", "Code:" + AutoNumberRecordID);
            return IPBussinessService.Inf00023RecordDetailList(Token, AutoNumberRecordID, page, rows);
        }

        #endregion

        #region Inf00024料品属性主档
        /// <summary>
        /// 料品属性列表
        /// SAM 2017年5月10日10:06:32
        /// </summary>
        /// <param name="Token">授权码</param>
        /// <param name="Code">代号</param>
        /// <param name="page">页码</param>
        /// <param name="rows">行数</param>
        /// <returns></returns>
        [HttpGet]
        [Authenticate]
        public object Inf00024GetList(string Token, string Code = null, int page = 1, int rows = 10)
        {
            DataLogerService.writeURL(Token, "Code:" + Code);
            UtilBussinessService.WriteLog(UtilBussinessService.detoken(Token), "料品属性主档", null, "查看", "Code:" + Code);
            return IPBussinessService.Inf00024GetList(Token, Code, page, rows);
        }

        /// <summary>
        /// 料品属性的保存
        /// SAM 2017年5月10日10:10:18
        /// </summary>
        /// <param name="request">JSON数据</param>
        /// <returns></returns>
        [HttpPost]
        [Authenticate]
        public object Inf00024Save(JObject request)
        {
            DataLogerService.writeURL(request.Value<string>("Token"), request.ToString());
            UtilBussinessService.WriteLog(UtilBussinessService.detoken(request.Value<string>("Token")), "料品属性主档", null, "保存", request.ToString());
            bool isDelete = false;
            bool isInsert = false;
            bool isUpdate = false;

            object deleted = null;
            object Insert = null;
            object Update = null;

            foreach (JProperty i in request.Children())
            {

                if (i.Name != "Token" && ((JArray)i.Value).Count > 0)
                {
                    switch (i.Name)
                    {
                        case "deleted": isDelete = true; break;
                        case "inserted": isInsert = true; break;
                        case "updated": isUpdate = true; break;
                    }
                }
            }

            if (isDelete)
                deleted = IPBussinessService.Inf00024delete(request.Value<string>("Token"), request.Value<JArray>("deleted"));
            if (isInsert)
                Insert = IPBussinessService.Inf00024insert(request.Value<string>("Token"), request.Value<JArray>("inserted"));
            if (isUpdate)
                Update = IPBussinessService.Inf00024update(request.Value<string>("Token"), request.Value<JArray>("updated"));

            return new { inserted = Insert, updated = Update, deleted = deleted };
        }

        /// <summary>
        /// 料品属性的单一删除
        /// SAM 2017年7月24日10:13:36
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Authenticate]
        public object Inf00024Delete(JObject request)
        {
            DataLogerService.writeURL(request.Value<string>("Token"), request.ToString());
            UtilBussinessService.WriteLog(UtilBussinessService.detoken(request.Value<string>("Token")), "料品属性", null, "删除", request.ToString());
            return IPBussinessService.Inf00024Delete(request);
        }

        /// <summary>
        /// 获取属性的资料值列表
        /// SAM 2017年5月10日10:54:30
        /// </summary>
        /// <param name="Token">授权码</param>
        /// <param name="ParameterID"></param>
        /// <param name="page">页码</param>
        /// <param name="rows">行数</param>
        /// <returns></returns>
        [HttpGet]
        [Authenticate]
        public object Inf00024DetailsGetList(string Token, string ParameterID, int page = 1, int rows = 10)
        {
            DataLogerService.writeURL(Token, "ParameterID:" + ParameterID);
            UtilBussinessService.WriteLog(UtilBussinessService.detoken(Token), "料品属性主档", null, "查看", "ParameterID:" + ParameterID);
            return IPBussinessService.Inf00024DetailsGetList(Token, ParameterID, page, rows);
        }

        /// <summary>
        /// 资料值保存
        /// SAM 2017年5月10日17:44:39
        /// </summary>
        /// <param name="request">JSON数据</param>
        /// <returns></returns>
        [HttpPost]
        [Authenticate]
        public object Inf00024DetailsSave(JObject request)
        {
            DataLogerService.writeURL(request.Value<string>("Token"), request.ToString());
            UtilBussinessService.WriteLog(UtilBussinessService.detoken(request.Value<string>("Token")), "料品属性主档", null, "保存", request.ToString());
            bool isDelete = false;
            bool isInsert = false;
            bool isUpdate = false;

            object deleted = null;
            object Insert = null;
            object Update = null;

            foreach (JProperty i in request.Children())
            {

                if (i.Name != "Token" && ((JArray)i.Value).Count > 0)
                {
                    switch (i.Name)
                    {
                        case "deleted": isDelete = true; break;
                        case "inserted": isInsert = true; break;
                        case "updated": isUpdate = true; break;
                    }
                }
            }

            if (isDelete)
                deleted = IPBussinessService.Inf00024Detailsdelete(request.Value<string>("Token"), request.Value<JArray>("deleted"));
            if (isInsert)
                Insert = IPBussinessService.Inf00024Detailsinsert(request.Value<string>("Token"), request.Value<JArray>("inserted"));
            if (isUpdate)
                Update = IPBussinessService.Inf00024Detailsupdate(request.Value<string>("Token"), request.Value<JArray>("updated"));

            return new { inserted = Insert, updated = Update, deleted = deleted };
        }
        #endregion

        #region trn00001資料拋轉參數設定
        /// <summary>
        /// 資料拋轉參數設定列表
        /// SAM 2017年7月5日10:02:59
        /// </summary>
        /// <param name="Token"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <returns></returns>
        [HttpGet]
        [Authenticate]
        public object Trn00001GetList(string Token, int page = 1, int rows = 10)
        {
            DataLogerService.writeURL(Token, null);
            UtilBussinessService.WriteLog(UtilBussinessService.detoken(Token), "資料拋轉參數設定", null, "查看", null);
            return IPBussinessService.Trn00001GetList(Token, page, rows);
        }

        /// <summary>
        /// 資料拋轉參數設定-保存
        /// SAM 2017年7月5日10:23:02
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Authenticate]
        public object Trn00001Save(JObject request)
        {
            DataLogerService.writeURL(request.Value<string>("Token"), request.ToString());
            UtilBussinessService.WriteLog(UtilBussinessService.detoken(request.Value<string>("Token")), "資料拋轉參數設定", null, "保存", request.ToString());

            bool isInsert = false;
            bool isUpdate = false;

            object deleted = null;
            object Insert = null;
            object Update = null;

            foreach (JProperty i in request.Children())
            {

                if (i.Name != "Token" && ((JArray)i.Value).Count > 0)
                {
                    switch (i.Name)
                    {

                        case "inserted": isInsert = true; break;
                        case "updated": isUpdate = true; break;
                    }
                }
            }
            if (isInsert)
                Insert = IPBussinessService.Trn00001insert(request.Value<string>("Token"), request.Value<JArray>("inserted"));
            if (isUpdate)
                Update = IPBussinessService.Trn00001update(request.Value<string>("Token"), request.Value<JArray>("updated"));

            return new { inserted = Insert, updated = Update, deleted = deleted };
        }


        #endregion

        #region Trn00002 MES资料转入处理
        /// <summary>
        /// MES资料转入处理-列表查询
        /// SAM 2017年8月31日15:32:51
        /// </summary>
        /// <param name="Token"></param>
        /// <param name="StartDate"></param>
        /// <param name="EndDate"></param>
        /// <param name="StartCode"></param>
        /// <param name="EndCode"></param>
        /// <param name="Type"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <returns></returns>
        [HttpGet]
        [Authenticate]
        public object Trn00002GetList(string Token, string StartDate = null, string EndDate = null, string StartCode = null, string EndCode = null, string Type = null, int page = 1, int rows = 10)
        {
            DataLogerService.writeURL(Token, null);
            UtilBussinessService.WriteLog(UtilBussinessService.detoken(Token), "MES资料转入处理", null, "查看", null);
            return IPBussinessService.Trn00002GetList(Token, StartDate, EndDate, StartCode, EndCode, Type, page, rows);
        }
        #endregion

        #region Lan00000语序主档
        /// <summary>
        /// 获取语序列表
        /// SAM 2017年5月3日10:37:51
        /// </summary>
        /// <param name="Token">授权码</param>
        /// <param name="Code">代号</param>
        /// <param name="page">页码</param>
        /// <param name="rows">行数</param>
        /// <returns></returns>
        [HttpGet]
        [Authenticate]
        public object Lan00000GetList(string Token, string Code = null, int page = 1, int rows = 10)
        {
            DataLogerService.writeURL(Token, "Code:" + Code);
            UtilBussinessService.WriteLog(UtilBussinessService.detoken(Token), "语序主档", null, "查看", "Code:" + Code);
            return IPBussinessService.Lan00000GetList(Token, Code, page, rows);
        }

        /// <summary>
        /// 保存语序操作
        /// SAM 2017年5月3日10:37:51
        /// </summary>
        /// <param name="request">JSON数据</param>
        /// <returns></returns>
        [HttpPost]
        [Authenticate]
        public object Lan00000Save(JObject request)
        {
            DataLogerService.writeURL(request.Value<string>("Token"), request.ToString());
            UtilBussinessService.WriteLog(UtilBussinessService.detoken(request.Value<string>("Token")), "语序主档", null, "保存", request.ToString());
            bool isDelete = false;
            bool isInsert = false;
            bool isUpdate = false;

            object deleted = null;
            object Insert = null;
            object Update = null;

            foreach (JProperty i in request.Children())
            {

                if (i.Name != "Token" && ((JArray)i.Value).Count > 0)
                {
                    switch (i.Name)
                    {
                        case "deleted": isDelete = true; break;
                        case "inserted": isInsert = true; break;
                        case "updated": isUpdate = true; break;
                    }
                }
            }

            if (isDelete)
                deleted = IPBussinessService.Lan00000delete(request.Value<string>("Token"), request.Value<JArray>("deleted"));
            if (isInsert)
                Insert = IPBussinessService.Lan00000insert(request.Value<string>("Token"), request.Value<JArray>("inserted"));
            if (isUpdate)
                Update = IPBussinessService.Lan00000update(request.Value<string>("Token"), request.Value<JArray>("updated"));

            return new { inserted = Insert, updated = Update, deleted = deleted };
        }

        #endregion

        #region SCP00004系统操作

        /// <summary>
        /// 系统操作查询
        /// Tom 2017年5月12日09:36:26
        /// </summary>
        /// <param name="Token"></param>
        /// <param name="StartTime"></param>
        /// <param name="EndTime"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <returns></returns>
        [HttpGet]
        [Authenticate]
        public object SCP00004List(string Token, DateTime? StartTime = null, DateTime? EndTime = null, int page = 1, int rows = 10)
        {
            return IPBussinessService.SCP00004List(Token, StartTime, EndTime, page, rows);
        }
        #endregion

        #region USE00001用途别主档
        /// <summary>
        /// 用途别列表
        /// SAM 2017年5月12日10:11:28
        /// </summary>
        /// <param name="Token">授权码</param>
        /// <param name="Code">代号</param>
        /// <param name="Status">状态</param>
        /// <param name="page">页码</param>
        /// <param name="rows">行数</param>
        /// <returns></returns>
        [HttpGet]
        [Authenticate]
        public object USE00001GetList(string Token, string Code = null, string Status = null, int page = 1, int rows = 10)
        {
            DataLogerService.writeURL(Token, "Code:" + Code);
            UtilBussinessService.WriteLog(UtilBussinessService.detoken(Token), "用途别主档", null, "查看", "Code:" + Code + "--->Status:" + Status);
            return IPBussinessService.USE00001List(Token, Code, Status, page, rows);
        }

        /// <summary>
        /// 用途别保存
        /// SAM 2017年5月12日10:12:03
        /// </summary>
        /// <param name="request">JSON数据</param>
        /// <returns></returns>
        [HttpPost]
        [Authenticate]
        public object USE00001Save(JObject request)
        {
            DataLogerService.writeURL(request.Value<string>("Token"), request.ToString());
            UtilBussinessService.WriteLog(UtilBussinessService.detoken(request.Value<string>("Token")), "用途别主档", null, "保存", request.ToString());
            bool isDelete = false;
            bool isInsert = false;
            bool isUpdate = false;

            object deleted = null;
            object Insert = null;
            object Update = null;

            foreach (JProperty i in request.Children())
            {

                if (i.Name != "Token" && ((JArray)i.Value).Count > 0)
                {
                    switch (i.Name)
                    {
                        case "deleted": isDelete = true; break;
                        case "inserted": isInsert = true; break;
                        case "updated": isUpdate = true; break;
                    }
                }
            }

            if (isDelete)
                deleted = IPBussinessService.USE00001delete(request.Value<string>("Token"), request.Value<JArray>("deleted"));
            if (isInsert)
                Insert = IPBussinessService.Parameterinsert(request.Value<string>("Token"), request.Value<JArray>("inserted"), Framework.SystemID + "019121300000D");
            if (isUpdate)
                Update = IPBussinessService.Parameterupdate(request.Value<string>("Token"), request.Value<JArray>("updated"));

            return new { inserted = Insert, updated = Update, deleted = deleted };
        }

        /// <summary>
        /// 用途别的单一删除
        /// SAM 2017年7月28日12:03:08
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Authenticate]
        public object USE00001Delete(JObject request)
        {
            DataLogerService.writeURL(request.Value<string>("Token"), request.ToString());
            UtilBussinessService.WriteLog(UtilBussinessService.detoken(request.Value<string>("Token")), "用途别", null, "删除", request.ToString());
            return IPBussinessService.USE00001Delete(request);
        }
        #endregion

        #region 通用函数
        /// <summary>
        /// 获取语序列表
        /// SAM 2017年4月28日10:16:56
        /// </summary>
        /// <param name="Token">授权码</param>
        /// <param name="name"></param>
        /// <param name="page">页码</param>
        /// <param name="rows">行数</param>
        /// <returns></returns>
        [HttpGet]
        [Authenticate]
        public object GetLanguageList(string Token, string name = null, int page = 1, int rows = 10)
        {
            DataLogerService.writeURL(Token, "name:" + name);
            return IPBussinessService.GetLanguageList(Token, name, page, rows);
        }

        /// <summary>
        /// 获取语序（Name和Comments双语序）
        /// SAM 2017年5月4日11:03:39
        /// </summary>
        /// <param name="Token">授权码</param>
        /// <param name="id"></param>
        /// <param name="TableID"></param>
        /// <returns></returns>
        [HttpGet]
        [Authenticate]
        public object NCGetLanguageList(string Token, string id, string TableID)
        {
            DataLogerService.writeURL(Token, "id:" + id);
            UtilBussinessService.WriteLog(UtilBussinessService.detoken(Token), "语序", null, "查看", null);
            return IPBussinessService.NCGetLanguageList(id, TableID);
        }

        /// <summary>
        /// 保存语序（Name和Comments双语序）
        /// SAM 2017年5月8日15:47:11
        /// </summary>
        /// <param name="request">JSON数据</param>
        /// <returns></returns>
        [HttpPost]
        [Authenticate]
        public object LanguageSave(JObject request)
        {
            DataLogerService.writeURL(request.Value<string>("Token"), request.ToString());
            UtilBussinessService.WriteLog(UtilBussinessService.detoken(request.Value<string>("Token")), "语序", null, "保存", request.ToString());
            bool isDelete = false;
            bool isInsert = false;
            bool isUpdate = false;

            object deleted = null;
            object Insert = null;
            object Update = null;

            foreach (JProperty i in request.Children())
            {

                if (i.Name != "Token" && ((JArray)i.Value).Count > 0)
                {
                    switch (i.Name)
                    {
                        case "deleted": isDelete = true; break;
                        case "inserted": isInsert = true; break;
                        case "updated": isUpdate = true; break;
                    }
                }
            }

            if (isDelete)
                deleted = IPBussinessService.Languagedelete(request.Value<JArray>("deleted"));
            if (isInsert)
                Insert = IPBussinessService.NCLanguageinsert(request.Value<string>("Token"), request.Value<JArray>("inserted"));
            if (isUpdate)
                Update = IPBussinessService.NCLanguageupdate(request.Value<string>("Token"), request.Value<JArray>("updated"));


            return new { inserted = Insert, updated = Update, deleted = deleted };
        }

        /// <summary>
        /// 获取语序（Code,Name和Comments三语序）
        /// </summary>
        /// <param name="Token">授权码</param>
        /// <param name="id"></param>
        /// <param name="TableID"></param>
        /// <returns></returns>
        [HttpGet]
        [Authenticate]
        public object CNCGetLanguageList(string Token, string id, string TableID)
        {
            DataLogerService.writeURL(Token, "id:" + id);
            UtilBussinessService.WriteLog(UtilBussinessService.detoken(Token), "语序", null, "查看", null);
            return IPBussinessService.CNCGetLanguageList(id, TableID);
        }

        /// <summary>
        /// 保存语序（Code,Name和Comments三语序）
        /// SAM 2017年5月8日15:51:26
        /// </summary>
        /// <param name="request">JSON数据</param>
        /// <returns></returns>
        [HttpPost]
        [Authenticate]
        public object CNCLanguageSave(JObject request)
        {
            DataLogerService.writeURL(request.Value<string>("Token"), request.ToString());
            UtilBussinessService.WriteLog(UtilBussinessService.detoken(request.Value<string>("Token")), "语序", null, "保存", request.ToString());
            bool isDelete = false;
            bool isInsert = false;
            bool isUpdate = false;

            object deleted = null;
            object Insert = null;
            object Update = null;

            foreach (JProperty i in request.Children())
            {

                if (i.Name != "Token" && ((JArray)i.Value).Count > 0)
                {
                    switch (i.Name)
                    {
                        case "deleted": isDelete = true; break;
                        case "inserted": isInsert = true; break;
                        case "updated": isUpdate = true; break;
                    }
                }
            }

            if (isDelete)
                deleted = IPBussinessService.Languagedelete(request.Value<JArray>("deleted"));
            if (isInsert)
                Insert = IPBussinessService.CNCLanguageinsert(request.Value<string>("Token"), request.Value<JArray>("inserted"));
            if (isUpdate)
                Update = IPBussinessService.CNCLanguageupdate(request.Value<string>("Token"), request.Value<JArray>("updated"));


            return new { inserted = Insert, updated = Update, deleted = deleted };
        }
        #endregion
    }
}
