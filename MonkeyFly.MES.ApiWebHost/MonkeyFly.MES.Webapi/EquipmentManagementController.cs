using MonkeyFly.Core;
using MonkeyFly.MES.BasicService;
using MonkeyFly.MES.FilterAttributes;
using MonkeyFly.MES.Services;
using Newtonsoft.Json.Linq;
using System.Web.Http;
using System.Web.Http.Cors;

namespace MonkeyFly.MES.Webapi
{
    /// <summary>
    /// 设备管理控制器
    /// </summary>
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class EquipmentManagementController : ApiController
    {
        /// <summary>
        /// EquipmentManagementAPI
        /// </summary>
        [HttpGet]
        public void EquipmentManagementAPI() { }

        #region EMS00001设备主档
        /// <summary>
        /// 设备主档列表
        /// SAM 2017年5月22日11:09:10
        /// </summary>
        /// <param name="Token">授权码</param>
        /// <param name="Code">代号</param>
        /// <param name="Status">状态</param>
        /// <param name="page">页码</param>
        /// <param name="rows">行数</param>
        /// <returns></returns>
        [HttpGet]
        [Authenticate]
        public object Ems00001GetList(string Token, string Code = null, string Status = null, int page = 1, int rows = 10)
        {
            DataLogerService.writeURL(Token, "Code:" + Code + "--->Status:" + Status);
            UtilBussinessService.WriteLog(UtilBussinessService.detoken(Token), "设备主档", null, "查看", "Code:" + Code + "--->Status:" + Status);
            return EMBussinessService.Ems00001GetList(Token, Code, Status, page, rows);
        }

        /// <summary>
        /// 保存设备主档
        /// SAM 2017年5月22日14:41:30
        /// </summary>
        /// <param name="request">JSON数据</param>
        /// <returns></returns>
        [HttpPost]
        [Authenticate]
        public object Ems00001Save(JObject request)
        {
            DataLogerService.writeURL(request.Value<string>("Token"), request.ToString());
            UtilBussinessService.WriteLog(UtilBussinessService.detoken(request.Value<string>("Token")), "设备主档", null, "保存", request.ToString());
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
                deleted = EMBussinessService.Ems00001delete(request.Value<string>("Token"), request.Value<JArray>("deleted"));
            if (isInsert)
                Insert = EMBussinessService.Ems00001insert(request.Value<string>("Token"), request.Value<JArray>("inserted"));
            if (isUpdate)
                Update = EMBussinessService.Ems00001update(request.Value<string>("Token"), request.Value<JArray>("updated"));

            return new { inserted = Insert, updated = Update, deleted = deleted };
        }

        /// <summary>
        /// 删除设备
        /// SAM 2017年8月14日23:19:01
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Authenticate]
        public object Ems00001Delete(JObject request)
        {
            DataLogerService.writeURL(request.Value<string>("Token"), request.ToString());
            UtilBussinessService.WriteLog(UtilBussinessService.detoken(request.Value<string>("Token")), "设备主档", null, "删除", request.ToString());
            return EMBussinessService.Ems00001Delete(request);
        }



        /// <summary>
        /// 获取正常的设备列表（用于设备管理另外两个页签）
        /// SAM 2017年5月22日22:27:24
        /// </summary>
        /// <param name="Token">授权码</param>
        /// <param name="Code">代号</param>
        /// <returns></returns>
        [HttpGet]
        [Authenticate]
        public object Ems00001List(string Token, string Code = null)
        {
            DataLogerService.writeURL(Token, "Code:" + Code);
            return EMBussinessService.Ems00001List(Token, Code);
        }

        /// <summary>
        /// 获取一个设备对应的设备项目列表
        /// SAM 2017年5月22日22:34:51
        /// </summary>
        /// <param name="Token">授权码</param>
        /// <param name="EquipmentID"></param>
        /// <param name="page">页码</param>
        /// <param name="rows">行数</param>
        /// <returns></returns>
        [HttpGet]
        [Authenticate]
        public object Ems00001GetProjectList(string Token, string EquipmentID, int page = 1, int rows = 10)
        {
            DataLogerService.writeURL(Token, "EquipmentID:" + EquipmentID);
            UtilBussinessService.WriteLog(UtilBussinessService.detoken(Token), "设备主档", null, "查看", "EquipmentID:" + EquipmentID);
            return EMBussinessService.Ems00001GetProjectList(Token, EquipmentID, page, rows);
        }

        /// <summary>
        /// 获取一个设备对应的设备项目列表(正常的)
        /// </summary>
        /// <param name="Token">授权码</param>
        /// <param name="EquipmentID"></param>
        /// <param name="page">页码</param>
        /// <param name="rows">行数</param>
        [HttpGet]
        [Authenticate]
        public object EquipmentProjectList(string Token, string EquipmentID, int page = 1, int rows = 10)
        {
            DataLogerService.writeURL(Token, "EquipmentID:" + EquipmentID);
            return EMBussinessService.EquipmentProjectList(Token, EquipmentID, page, rows);
        }

        /// <summary>
        /// 保存设备项目
        /// SAM 2017年5月22日22:53:12
        /// </summary>
        /// <param name="request">JSON数据</param>
        /// <returns></returns>
        [HttpPost]
        [Authenticate]
        public object Ems00001ProjectSave(JObject request)
        {
            DataLogerService.writeURL(request.Value<string>("Token"), request.ToString());
            UtilBussinessService.WriteLog(UtilBussinessService.detoken(request.Value<string>("Token")), "设备主档", null, "保存", request.ToString());
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
                deleted = EMBussinessService.Ems00001Projectdelete(request.Value<string>("Token"), request.Value<JArray>("deleted"));
            if (isInsert)
                Insert = EMBussinessService.Ems00001Projectinsert(request.Value<string>("Token"), request.Value<JArray>("inserted"));
            if (isUpdate)
                Update = EMBussinessService.Ems00001Projectupdate(request.Value<string>("Token"), request.Value<JArray>("updated"));

            return new { inserted = Insert, updated = Update, deleted = deleted };
        }

        /// <summary>
        /// 删除设备项目
        /// SAM 2017年8月14日23:21:52
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Authenticate]
        public object Ems00001ProjectDelete(JObject request)
        {
            DataLogerService.writeURL(request.Value<string>("Token"), request.ToString());
            UtilBussinessService.WriteLog(UtilBussinessService.detoken(request.Value<string>("Token")), "设备主档", null, "删除", request.ToString());
            return EMBussinessService.Ems00001ProjectDelete(request);
        }

        /// <summary>
        /// 获取一个设备对应的图样列表
        /// SAM 2017年5月23日09:40:46
        /// </summary>
        /// <param name="Token">授权码</param>
        /// <param name="EquipmentID"></param>
        /// <param name="page">页码</param>
        /// <param name="rows">行数</param>
        /// <returns></returns>
        [HttpGet]
        [Authenticate]
        public object Ems00001GetPatternList(string Token, string EquipmentID, int page = 1, int rows = 10)
        {
            DataLogerService.writeURL(Token, "AttachmentID:" + EquipmentID);
            UtilBussinessService.WriteLog(UtilBussinessService.detoken(Token), "设备主档", null, "查看", "EquipmentID:" + EquipmentID);
            return EMBussinessService.Ems00001GetPatternList(Token, EquipmentID, page, rows);
        }

        /// <summary>
        /// 设备图样的删除
        /// SAM 2017年5月23日10:04:04
        /// </summary>
        /// <param name="request">JSON数据</param>
        /// <returns></returns>
        [HttpPost]
        [Authenticate]
        public object Ems00001PatternDelete(JObject request)
        {
            string Token = request.Value<string>("Token");
            DataLogerService.writeURL(Token, request.ToString());
            UtilBussinessService.WriteLog(UtilBussinessService.detoken(Token), " 设备主档", null, "新增", request.ToString());
            return EMBussinessService.Ems00001PatternDelete(Token, request);
        }

        /// <summary>
        /// 获取机况设定
        /// SAM 2017年7月31日17:00:53
        /// </summary>
        /// <param name="Token"></param>
        /// <param name="Code"></param>
        /// <param name="Name"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <returns></returns>
        [HttpGet]
        [Authenticate]
        public object Ems00001GetConditionList(string Token, string Code = null, string Name = null, int page = 1, int rows = 10)
        {
            return EMBussinessService.Ems00001GetConditionList(Token, Code, Name, page, rows);
        }

        /// <summary>
        /// 机况设定保存
        /// SAM 2017年7月31日17:01:453
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Authenticate]
        public object Ems00001ConditionSave(JObject request)
        {
            DataLogerService.writeURL(request.Value<string>("Token"), request.ToString());
            //bool isDelete = false;
            //bool isInsert = false;
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
                        //case "deleted": isDelete = true; break;
                        //case "inserted": isInsert = true; break;
                        case "updated": isUpdate = true; break;
                    }
                }
            }
            if (isUpdate)
                Update = EMBussinessService.Ems00001ConditionUpdate(request.Value<string>("Token"), (JArray)request.Value<JArray>("updated"));

            return new { inserted = Insert, updated = Update, deleted = deleted };
        }


        #endregion

        #region EMS00002设备巡检项目主档

        /// <summary>
        /// 获取正常的类别为M的设备列表（Ems00002获取列表）
        /// MOUSE 2017年8月1日16:12:18
        /// </summary>
        /// <param name="Token">授权码</param>
        /// <param name="Code">代号</param>
        /// <returns></returns>
        [HttpGet]
        [Authenticate]
        public object Ems00002List(string Token, string Code = null)
        {
            DataLogerService.writeURL(Token, "Code:" + Code);
            return EMBussinessService.Ems00002List(Token, Code);
        }

        /// <summary>
        /// 获取一个设备对应的巡检项目列表
        /// SAM 2017年5月23日15:37:53
        /// </summary>
        /// <param name="Token">授权码</param>
        /// <param name="EquipmentID"></param>
        /// <param name="page">页码</param>
        /// <param name="rows">行数</param>
        /// <returns></returns>
        [HttpGet]
        [Authenticate]
        public object Ems00002GetList(string Token, string EquipmentID, int page = 1, int rows = 10)
        {
            DataLogerService.writeURL(Token, "EquipmentID:" + EquipmentID);
            UtilBussinessService.WriteLog(UtilBussinessService.detoken(Token), "设备巡检项目主档", null, "查看", "EquipmentID:" + EquipmentID);
            return EMBussinessService.Ems00002GetList(Token, EquipmentID, page, rows);
        }

        /// <summary>
        /// 设备巡检项目保存
        /// SAM 2017年5月23日15:55:44
        /// </summary>
        /// <param name="request">JSON数据</param>
        /// <returns></returns>
        [HttpPost]
        [Authenticate]
        public object Ems00002Save(JObject request)
        {
            DataLogerService.writeURL(request.Value<string>("Token"), request.ToString());
            UtilBussinessService.WriteLog(UtilBussinessService.detoken(request.Value<string>("Token")), "设备巡检项目主档", null, "保存", request.ToString());
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
                deleted = EMBussinessService.Ems00002delete(request.Value<string>("Token"), request.Value<JArray>("deleted"));
            if (isInsert)
                Insert = EMBussinessService.Ems00002insert(request.Value<string>("Token"), request.Value<JArray>("inserted"));
            if (isUpdate)
                Update = EMBussinessService.Ems00002update(request.Value<string>("Token"), request.Value<JArray>("updated"));

            return new { inserted = Insert, updated = Update, deleted = deleted };
        }

        /// <summary>
        /// 设备巡检项目删除
        /// SAM 2017年6月1日10:07:31
        /// </summary>
        /// <param name="request">JSON数据</param>
        /// <returns></returns>
        [HttpPost]
        [Authenticate]
        public object Ems00002Delete(JObject request)
        {
            DataLogerService.writeURL(request.Value<string>("Token"), request.ToString());
            UtilBussinessService.WriteLog(UtilBussinessService.detoken(request.Value<string>("Token")), "设备巡检项目主档", null, "删除", request.ToString());
            return EMBussinessService.Ems00002Delete(request);
        }

        /// <summary>
        /// 根据设备流水号获取巡检项目列表
        /// MOUSE 2017年7月31日15:35:31
        /// </summary>
        /// <param name="Token"></param>
        /// <param name="EquipmentID"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <returns></returns>
        [HttpGet]
        [Authenticate]
        public object Ems00002GetProjectList(string Token, string EquipmentID, int page = 1, int rows = 10)
        {
            DataLogerService.writeURL(Token, "EquipmentID:" + EquipmentID);
            UtilBussinessService.WriteLog(UtilBussinessService.detoken(Token), "设备巡检项目主档", null, "查看", "EquipmentID:" + EquipmentID);
            return EMBussinessService.Ems00002GetProjectList(Token, EquipmentID, page, rows);
        }

        /// <summary>
        /// 根据设备流水号获取巡检项目（不分页）
        /// MOUSE 2017年7月31日15:03:36
        /// </summary>
        /// <param name="Token"></param>
        /// <param name="EquipmentID"></param>
        /// <returns></returns>
        [HttpGet]
        [Authenticate]
        public object Ems00002ProjectList(string Token, string EquipmentID)
        {
            DataLogerService.writeURL(Token, "EquipmentID:" + EquipmentID);
            return EMBussinessService.Ems00002ProjectList(Token, EquipmentID);
        }

        /// <summary>
        /// 根据设备流水号获取不属于他的巡检项目（不分页）
        /// MOUSE 2017年7月31日16:30:36
        /// </summary>
        /// <param name="Token">授权码</param>
        /// <param name="EquipmentID"></param>
        /// <returns></returns>
        [HttpGet]
        [Authenticate]
        public object Ems00002NoProjectList(string Token, string EquipmentID)
        {
            DataLogerService.writeURL(Token, "EquipmentID:" + EquipmentID);
            return EMBussinessService.Ems00002NoProjectList(Token, EquipmentID);
        }

        /// <summary>
        /// 保存设备的巡检项目
        /// MOUSE 2017年7月31日17:21:54
        /// </summary>
        /// <param name="request">JSON数据</param>
        /// <returns></returns>
        [HttpPost]
        [Authenticate]
        public object Ems00002ProjectSave(JObject request)
        {
            DataLogerService.writeURL(request.Value<string>("Token"), request.ToString());
            UtilBussinessService.WriteLog(UtilBussinessService.detoken(request.Value<string>("Token")), "设备巡检项目主档", null, "保存", request.ToString());
            return EMBussinessService.Ems00002ProjectSave(request);
        }

        #endregion

        #region EMS00003设备巡检维护
        /// <summary>
        /// 设备巡检维护表头列表
        /// SAM 2017年6月8日11:12:20
        /// </summary>
        /// <param name="Token"></param>
        /// <param name="Code"></param>
        /// <param name="StartDate"></param>
        /// <param name="EndDate"></param>
        /// <returns></returns>
        [HttpGet]
        [Authenticate]
        public object EMS00003GetList(string Token, string Code = null, string StartDate = null, string EndDate = null)
        {
            DataLogerService.writeURL(Token, "Code:" + Code + "--->StartDate:" + StartDate + "--->EndDate:" + EndDate);
            UtilBussinessService.WriteLog(UtilBussinessService.detoken(Token), "设备巡检维护", null, "查看", "Code:" + Code + "--->StartDate:" + StartDate + "--->EndDate:" + EndDate);
            return EMBussinessService.EMS00003GetList(Token, Code, StartDate, EndDate);
        }

        /// <summary>
        /// 设备巡检维护表头保存
        /// SAM 2017年6月8日14:57:16
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Authenticate]
        public object Ems00003Save(JObject request)
        {
            DataLogerService.writeURL(request.Value<string>("Token"), request.ToString());
            UtilBussinessService.WriteLog(UtilBussinessService.detoken(request.Value<string>("Token")), "设备巡检维护", null, "保存", request.ToString());

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
            //    deleted = EMBussinessService.Ems00003delete(request.Value<string>("Token"), request.Value<JArray>("deleted"));
            if (isInsert)
                Insert = EMBussinessService.Ems00003insert(request.Value<string>("Token"), request.Value<JArray>("inserted"));
            if (isUpdate)
                Update = EMBussinessService.Ems00003update(request.Value<string>("Token"), request.Value<JArray>("updated"));

            return new { inserted = Insert, updated = Update, deleted = deleted };
        }

        /// <summary>
        /// 设备巡检维护表头删除
        /// Tom 2017年7月28日15:04:47
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Authenticate]
        public object Ems0003Delete(JObject request)
        {
            return EMBussinessService.Ems0003Delete(request);
        }

        /// <summary>
        /// 新增设备巡检维护
        /// SAM 2017年6月8日17:13:42
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Authenticate]
        public object Ems00003Add(JObject request)
        {
            DataLogerService.writeURL(request.Value<string>("Token"), request.ToString());
            UtilBussinessService.WriteLog(UtilBussinessService.detoken(request.Value<string>("Token")), "设备巡检维护", null, "新增", request.ToString());
            return EMBussinessService.Ems00003Add(request);
        }

        ///// <summary>
        ///// 巡检单号获取
        ///// SAM 2017年7月10日14:36:42
        ///// </summary>
        ///// <param name="Token"></param>
        ///// <returns></returns>
        //[HttpGet]
        //[Authenticate]
        //public string Ems00003GetAutoNumber(string Token)
        //{
        //    DataLogerService.writeURL(Token, null);
        //    return EMBussinessService.Ems00003GetAutoNumber(Token);
        //}
        /// <summary>
        /// 巡检单号获取
        /// SAM 2017年8月3日23:43:48  
        /// 因上面逻辑未完成，故现在做出修正。
        /// </summary>
        /// <param name="Token"></param>
        /// <returns></returns>
        [HttpGet]
        [Authenticate]
        public object Ems00003GetAutoNumber(string Token)
        {
            DataLogerService.writeURL(Token, null);
            return EMBussinessService.Ems00003GetAutoNumber(Token);
        }

        /// <summary>
        /// 根据设备巡检维护的表头获取明细
        /// SAM 2017年6月8日15:57:13
        /// </summary>
        /// <param name="Token"></param>
        /// <param name="EquipmentInspectionRecordID"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <returns></returns>
        [HttpGet]
        [Authenticate]
        public object EMS00003GetDetailList(string Token, string EquipmentInspectionRecordID, int page = 1, int rows = 10)
        {
            DataLogerService.writeURL(Token, "EquipmentInspectionRecordID:" + EquipmentInspectionRecordID);
            UtilBussinessService.WriteLog(UtilBussinessService.detoken(Token), "设备巡检维护", null, "查看", "EquipmentInspectionRecordID:" + EquipmentInspectionRecordID);
            return EMBussinessService.EMS00003GetDetailList(Token, EquipmentInspectionRecordID, page, rows);
        }

        /// <summary>
        /// 根据设备巡检维护的表头获取明细列表
        /// SAM 2017年10月24日17:59:00
        /// 现值要做是否存在与区间内的判定
        /// </summary>
        /// <param name="Token"></param>
        /// <param name="EquipmentInspectionRecordID"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <returns></returns>
        [HttpGet]
        [Authenticate]
        public object EMS00003GetDetailListV1(string Token, string EquipmentInspectionRecordID, int page = 1, int rows = 10)
        {
            DataLogerService.writeURL(Token, "EquipmentInspectionRecordID:" + EquipmentInspectionRecordID);
            UtilBussinessService.WriteLog(UtilBussinessService.detoken(Token), "设备巡检维护", null, "查看", "EquipmentInspectionRecordID:" + EquipmentInspectionRecordID);
            return EMBussinessService.EMS00003GetDetailListV1(Token, EquipmentInspectionRecordID, page, rows);
        }

        /// <summary>
        /// 设备巡检维护明细的保存
        /// SAM 2017年6月8日16:04:59
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Authenticate]
        public object Ems00003DetailSave(JObject request)
        {
            DataLogerService.writeURL(request.Value<string>("Token"), request.ToString());
            UtilBussinessService.WriteLog(UtilBussinessService.detoken(request.Value<string>("Token")), "设备巡检维护", null, "保存", request.ToString());
        
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
            //    deleted = EMBussinessService.Ems00003Detaildelete(request.Value<string>("Token"), request.Value<JArray>("deleted"));
            if (isInsert)
                Insert = EMBussinessService.Ems00003Detailinsert(request.Value<string>("Token"), request.Value<JArray>("inserted"));
            if (isUpdate)
                Update = EMBussinessService.Ems00003Detailupdate(request.Value<string>("Token"), request.Value<JArray>("updated"));

            return new { inserted = Insert, updated = Update, deleted = deleted };
        }

        /// <summary>
        /// 设备巡检维护明细删除
        /// Tom 2017年7月28日15:04:47
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Authenticate]
        public object Ems0003DetailDelete(JObject request)
        {
            return EMBussinessService.Ems0003DetailDelete(request);
        }
        #endregion

        #region EMS00004设备叫修单维护
        /// <summary>
        /// 获取设备叫修单的列表
        /// SAM 2017年5月24日14:17:29
        /// </summary>
        /// <param name="Token">授权码</param>
        /// <param name="Code">代号</param>
        /// <param name="Status">状态</param>
        /// <param name="page">页码</param>
        /// <param name="rows">行数</param>
        /// <returns></returns>
        [HttpGet]
        [Authenticate]
        public object Ems00004GetList(string Token, string Code = null, string Status = null, int page = 1, int rows = 10)
        {
            DataLogerService.writeURL(Token, "Code:" + Code + "--->Status:" + Status);
            UtilBussinessService.WriteLog(UtilBussinessService.detoken(Token), "设备叫修单", null, "查看", "Code:" + Code + "--->Status:" + Status);
            return EMBussinessService.Ems00004GetList(Token, Code, Status, page, rows);
        }

        /// <summary>
        /// 新增设备叫修单
        /// SAM 2017年6月2日14:35:33
        /// </summary>
        /// <param name="request">JSON数据</param>
        /// <returns></returns>
        [HttpPost]
        [Authenticate]
        public object Ems00004Add(JObject request)
        {
            DataLogerService.writeURL(request.Value<string>("Token"), request.ToString());
            UtilBussinessService.WriteLog(UtilBussinessService.detoken(request.Value<string>("Token")), "设备叫修单", null, "新增", request.ToString());
            return EMBussinessService.Ems00004Add(request);
        }

        /// <summary>
        /// 获取单据预设子元挑选下拉框
        /// SAM 2017年8月3日23:28:28
        /// </summary>
        /// <param name="Token"></param>
        /// <returns></returns>
        [HttpGet]
        [Authenticate]
        public object Ems00004GetTypeList(string Token)
        {
            DataLogerService.writeURL(Token, null);
            return UtilBussinessService.GetTypeList(UtilBussinessService.detoken(Token), Framework.SystemID + "0201213000038");
        }

        /// <summary>
        /// 检查设备是否已存在叫修单
        /// SAM 2017年6月5日18:26:20
        /// </summary>
        /// <param name="Token">授权码</param>
        /// <param name="EquipmentID"></param>
        /// <returns></returns>
        [HttpGet]
        [Authenticate]
        public bool CheckEquipment(string Token, string EquipmentID)
        {
            DataLogerService.writeURL(Token, "EquipmentID:" + EquipmentID);
            return EMBussinessService.CheckEquipment(Token, EquipmentID);
        }

        /// <summary>
        /// 设备叫修单保存
        /// SAm 2017年5月29日13:18:50
        /// </summary>
        /// <param name="request">JSON数据</param>
        /// <returns></returns>
        [HttpPost]
        [Authenticate]
        public object Ems00004Save(JObject request)
        {
            DataLogerService.writeURL(request.Value<string>("Token"), request.ToString());
            UtilBussinessService.WriteLog(UtilBussinessService.detoken(request.Value<string>("Token")), "设备叫修单", null, "保存", request.ToString());
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
                deleted = EMBussinessService.Ems00004delete(request.Value<string>("Token"), request.Value<JArray>("deleted"));
            if (isInsert)
                Insert = EMBussinessService.Ems00004insert(request.Value<string>("Token"), request.Value<JArray>("inserted"));
            if (isUpdate)
                Update = EMBussinessService.Ems00004update(request.Value<string>("Token"), request.Value<JArray>("updated"));

            return new { inserted = Insert, updated = Update, deleted = deleted };
        }

        /// <summary>
        /// 根据叫修单获取他的叫修原因
        /// SAM 2017年5月24日14:45:54
        /// </summary>
        /// <param name="Token">授权码</param>
        /// <param name="CalledRepairOrderID"></param>
        /// <param name="page">页码</param>
        /// <param name="rows">行数</param>
        /// <returns></returns>
        [HttpGet]
        [Authenticate]
        public object Ems00004GetReasonList(string Token, string CalledRepairOrderID, int page = 1, int rows = 10)
        {
            DataLogerService.writeURL(Token, "CalledRepairOrderID:" + CalledRepairOrderID);
            UtilBussinessService.WriteLog(UtilBussinessService.detoken(Token), "设备叫修单", null, "查看", "CalledRepairOrderID:" + CalledRepairOrderID);
            return EMBussinessService.Ems00004GetReasonList(Token, CalledRepairOrderID, page, rows);
        }

        /// <summary>
        /// 叫修原因的保存
        /// SAM 2017年5月29日15:50:50
        /// </summary>
        /// <param name="request">JSON数据</param>
        /// <returns></returns>
        [HttpPost]
        [Authenticate]
        public object Ems00004ReasonSave(JObject request)
        {
            DataLogerService.writeURL(request.Value<string>("Token"), request.ToString());
            UtilBussinessService.WriteLog(UtilBussinessService.detoken(request.Value<string>("Token")), "设备叫修单叫修原因", null, "保存", request.ToString());
            //bool isDelete = false;
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
                        //case "deleted": isDelete = true; break;
                        case "inserted": isInsert = true; break;
                        case "updated": isUpdate = true; break;
                    }
                }
            }

            if (isInsert)
                Insert = EMBussinessService.Ems00004Reasoninsert(request.Value<string>("Token"), request.Value<JArray>("inserted"));
            if (isUpdate)
                Update = EMBussinessService.Ems00004Reasonupdate(request.Value<string>("Token"), request.Value<JArray>("updated"));

            return new { inserted = Insert, updated = Update, deleted = deleted };
        }

        /// <summary>
        /// 叫修原因的删除单个
        /// Jack 2017年8月7日16:05:11
        /// </summary>
        /// <param name="request">JSON数据</param>
        /// <returns></returns>
        [HttpPost]
        [Authenticate]
        public object Ems00004ReasonDelete(JObject request)
        {
            DataLogerService.writeURL(request.Value<string>("Token"), request.ToString());
            UtilBussinessService.WriteLog(UtilBussinessService.detoken(request.Value<string>("Token")), "叫修原因的删除单个", null, "删除", request.ToString());
            return EMBussinessService.Ems00004Reasondelete(request.Value<string>("Token"), request);
        }
        #endregion

        #region EMS00005设备维修作业
        /// <summary>
        /// 设备维修作业的列表
        /// SAM 2017年5月29日23:08:40
        /// </summary>
        /// <param name="Token">授权码</param>
        /// <param name="Code">代号</param>
        /// <param name="page">页码</param>
        /// <param name="rows">行数</param>
        /// <returns></returns>
        [HttpGet]
        [Authenticate]
        public object Ems00005GetList(string Token, string Code = null, int page = 1, int rows = 10)
        {
            DataLogerService.writeURL(Token, "Code:" + Code);
            UtilBussinessService.WriteLog(UtilBussinessService.detoken(Token), "设备维修作业", null, "查看", "Code:" + Code);
            return EMBussinessService.Ems00005GetList(Token, Code, page, rows);
        }

        /// <summary>
        /// 设备维修作业的保存
        /// SAM 2017年5月29日23:14:06
        /// </summary>
        /// <param name="request">JSON数据</param>
        /// <returns></returns>
        [HttpPost]
        [Authenticate]
        public object Ems00005Save(JObject request)
        {
            DataLogerService.writeURL(request.Value<string>("Token"), request.ToString());
            UtilBussinessService.WriteLog(UtilBussinessService.detoken(request.Value<string>("Token")), "设备维修作业", null, "保存", request.ToString());

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
                Update = EMBussinessService.Ems00005update(request.Value<string>("Token"), request.Value<JArray>("updated"));

            return new { inserted = Insert, updated = Update, deleted = deleted };
        }



        /// <summary>
        /// 获取维修记录
        /// SAM 2017年5月29日23:19:47
        /// </summary>
        /// <param name="Token">授权码</param>
        /// <param name="CalledRepairOrderID"></param>
        /// <param name="page">页码</param>
        /// <param name="rows">行数</param>
        /// <returns></returns>
        [HttpGet]
        [Authenticate]
        public object Ems00005GetServiceList(string Token, string CalledRepairOrderID, int page = 1, int rows = 10)
        {
            DataLogerService.writeURL(Token, "CalledRepairOrderID:" + CalledRepairOrderID);
            UtilBussinessService.WriteLog(UtilBussinessService.detoken(Token), "维修记录", null, "查看", "CalledRepairOrderID:" + CalledRepairOrderID);
            return EMBussinessService.Ems00005GetServiceList(Token, CalledRepairOrderID, page, rows);
        }

        /// <summary>
        /// 维修记录表头的保存
        /// SAM 2017年5月29日23:27:09
        /// </summary>
        /// <param name="request">JSON数据</param>
        /// <returns></returns>
        [HttpPost]
        [Authenticate]
        public object Ems00005ServiceSave(JObject request)
        {
            DataLogerService.writeURL(request.Value<string>("Token"), request.ToString());
            UtilBussinessService.WriteLog(UtilBussinessService.detoken(request.Value<string>("Token")), "设备维修作业", null, "保存", request.ToString());
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
                deleted = EMBussinessService.Ems00005Servicedelete(request.Value<string>("Token"), request.Value<JArray>("deleted"));
            if (isInsert)
                Insert = EMBussinessService.Ems00005Serviceinsert(request.Value<string>("Token"), request.Value<JArray>("inserted"));
            if (isUpdate)
                Update = EMBussinessService.Ems00005Serviceupdate(request.Value<string>("Token"), request.Value<JArray>("updated"));

            return new { inserted = Insert, updated = Update, deleted = deleted };
        }

        /// <summary>
        /// 维修记录表头的单个删除
        /// SAM 2017年5月29日23:27:09
        /// </summary>
        /// <param name="request">JSON数据</param>
        /// <returns></returns>
        [HttpPost]
        [Authenticate]
        public object Ems00005ServiceDelete(JObject request)
        {
            DataLogerService.writeURL(request.Value<string>("Token"), request.ToString());
            UtilBussinessService.WriteLog(UtilBussinessService.detoken(request.Value<string>("Token")), "维修记录表头", null, "删除", request.ToString());
            return EMBussinessService.Ems00005ServiceDelete(request);
        }

        /// <summary>
        /// 维修记录明细的列表
        /// SAM 2017年5月29日23:28:24
        /// </summary>
        /// <param name="Token">授权码</param>
        /// <param name="ServiceReasonLogID"></param>
        /// <param name="page">页码</param>
        /// <param name="rows">行数</param>
        /// <returns></returns>
        [HttpGet]
        [Authenticate]
        public object Ems00005GetServiceDetailsList(string Token, string ServiceReasonLogID, int page = 1, int rows = 10)
        {
            DataLogerService.writeURL(Token, "CalledRepairOrderID:" + ServiceReasonLogID);
            UtilBussinessService.WriteLog(UtilBussinessService.detoken(Token), "维修记录", null, "查看", "ServiceReasonLogID:" + ServiceReasonLogID);
            return EMBussinessService.Ems00005GetServiceDetailsList(Token, ServiceReasonLogID, page, rows);
        }

        /// <summary>
        /// 维修记录明细的保存
        /// SAM 2017年5月29日23:28:47
        /// </summary>
        /// <param name="request">JSON数据</param>
        /// <returns></returns>
        [HttpPost]
        [Authenticate]
        public object Ems00005ServiceDetailsSave(JObject request)
        {
            DataLogerService.writeURL(request.Value<string>("Token"), request.ToString());
            UtilBussinessService.WriteLog(UtilBussinessService.detoken(request.Value<string>("Token")), "设备维修作业", null, "保存", request.ToString());
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
                deleted = EMBussinessService.Ems00005ServiceDetailsdelete(request.Value<string>("Token"), request.Value<JArray>("deleted"));
            if (isInsert)
                Insert = EMBussinessService.Ems00005ServiceDetailsinsert(request.Value<string>("Token"), request.Value<JArray>("inserted"));
            if (isUpdate)
                Update = EMBussinessService.Ems00005ServicDetailseupdate(request.Value<string>("Token"), request.Value<JArray>("updated"));

            return new { inserted = Insert, updated = Update, deleted = deleted };
        }

        /// <summary>
        /// 维修记录明细的单个删除
        /// SAM 2017年5月29日23:27:09
        /// </summary>
        /// <param name="request">JSON数据</param>
        /// <returns></returns>
        [HttpPost]
        [Authenticate]
        public object Ems00005ServiceDetailDelete(JObject request)
        {
            DataLogerService.writeURL(request.Value<string>("Token"), request.ToString());
            UtilBussinessService.WriteLog(UtilBussinessService.detoken(request.Value<string>("Token")), "维修记录明细", null, "删除", request.ToString());
            return EMBussinessService.Ems00005ServiceDetailDelete(request);
        }

        /// <summary>
        /// 开始明细
        /// SAM 2017年6月2日11:52:37
        /// </summary>
        /// <param name="request">JSON数据</param>
        /// <returns></returns>
        [HttpPost]
        [Authenticate]
        public object Ems00005DetailsStart(JObject request)
        {
            DataLogerService.writeURL(request.Value<string>("Token"), request.ToString());
            UtilBussinessService.WriteLog(UtilBussinessService.detoken(request.Value<string>("Token")), "维修记录明细", null, "开始", request.ToString());
            return EMBussinessService.Ems00005DetailsStart(request);
        }

        /// <summary>
        /// 结束明细
        /// SAM 2017年6月2日11:52:51
        /// </summary>
        /// <param name="request">JSON数据</param>
        /// <returns></returns>
        [HttpPost]
        [Authenticate]
        public object Ems00005DetailsEnd(JObject request)
        {
            DataLogerService.writeURL(request.Value<string>("Token"), request.ToString());
            UtilBussinessService.WriteLog(UtilBussinessService.detoken(request.Value<string>("Token")), "维修记录明细", null, "结束", request.ToString());
            return EMBussinessService.Ems00005DetailsEnd(request);
        }

        /// <summary>
        /// 设备维修作业开始维修
        /// MOUSE 2017年7月26日17:01:05
        /// </summary>
        /// <param name="request">JSON数据</param>
        /// <returns></returns>
        [HttpPost]
        [Authenticate]
        public object Ems0005RepairStart(JObject request)
        {
            DataLogerService.writeURL(request.Value<string>("Token"), request.ToString());
            UtilBussinessService.WriteLog(UtilBussinessService.detoken(request.Value<string>("Token")), "设备维修作业开始维修", null, "开始", request.ToString());
            return EMBussinessService.Ems0005RepairStart(request);
        }

        /// <summary>
        /// 设备维修作业结束维修
        /// MOUSE 2017年7月26日18:20:57
        /// </summary>
        /// <param name="request">JSON数据</param>
        /// <returns></returns>
        [HttpPost]
        [Authenticate]
        public object Ems0005RepairEnd(JObject request)
        {
            DataLogerService.writeURL(request.Value<string>("Token"), request.ToString());
            UtilBussinessService.WriteLog(UtilBussinessService.detoken(request.Value<string>("Token")), "设备维修作业结束维修", null, "结束", request.ToString());
            return EMBussinessService.Ems0005RepairEnd(request);
        }



        #endregion

        #region EMS00006设备叫修结案处理
        /// <summary>
        /// 设备叫修结案处理列表
        /// SAM 2017年6月5日14:43:21
        /// </summary>
        /// <param name="Token">授权码</param>
        /// <param name="Code">代号</param>
        /// <param name="Status">状态</param>
        /// <param name="page">页码</param>
        /// <param name="rows">行数</param>
        /// <returns></returns>
        [HttpGet]
        [Authenticate]
        public object Ems00006GetList(string Token, string Code = null, string Status = null, int page = 1, int rows = 10)
        {
            DataLogerService.writeURL(Token, "Code:" + Code + "---->Status:" + Status);
            UtilBussinessService.WriteLog(UtilBussinessService.detoken(Token), "设备叫修结案处理", null, "查看", "Code:" + Code + "---->Status:" + Status);
            return EMBussinessService.Ems00006GetList(Token, Code, Status, page, rows);
        }

        /// <summary>
        /// 设备叫修结案处理保存
        /// SAM 2017年6月5日16:31:51
        /// </summary>
        /// <param name="request">JSON数据</param>
        /// <returns></returns>
        [HttpPost]
        [Authenticate]
        public object Ems00006Save(JObject request)
        {
            DataLogerService.writeURL(request.Value<string>("Token"), request.ToString());
            UtilBussinessService.WriteLog(UtilBussinessService.detoken(request.Value<string>("Token")), "设备叫修结案处理", null, "保存", request.ToString());
          
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
                Update = EMBussinessService.Ems00006update(request.Value<string>("Token"), request.Value<JArray>("updated"));

            return new { inserted = Insert, updated = Update, deleted = deleted };
        }
        #endregion

        #region EMS00007維修原因統計分析
        /// <summary>
        /// 維修原因統計分析-原因码列表
        /// SAM 2017年8月3日11:45:05
        /// </summary>
        /// <param name="Token"></param>
        /// <param name="StartReasonCode"></param>
        /// <param name="EndReasonCode"></param>
        /// <param name="StartDate"></param>
        /// <param name="EndDate"></param>
        /// <param name="StartEquipmentCode"></param>
        /// <param name="EndEquipmentCode"></param>
        /// <param name="Type"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <returns></returns>
        [HttpGet]
        [Authenticate]
        public object Ems00007GetReasonList(string Token, string StartReasonCode = null, string EndReasonCode = null,
             string StartDate = null, string EndDate = null,
              string StartEquipmentCode = null, string EndEquipmentCode = null,
               string Type = null,
            int page = 1, int rows = 10)
        {
            DataLogerService.writeURL(Token, null);
            UtilBussinessService.WriteLog(UtilBussinessService.detoken(Token), "设备叫修结案处理", null, "查看", null);
            return EMBussinessService.Ems00007GetReasonList(Token, StartReasonCode, EndReasonCode, StartDate, EndDate, StartEquipmentCode, EndEquipmentCode, Type, page, rows);
        }

        /// <summary>
        /// 維修原因統計分析-原因码列表(不分页，用于园饼图)
        /// SAM 2017年8月3日16:06:36
        /// </summary>
        /// <param name="Token"></param>
        /// <param name="StartReasonCode"></param>
        /// <param name="EndReasonCode"></param>
        /// <param name="StartDate"></param>
        /// <param name="EndDate"></param>
        /// <param name="StartEquipmentCode"></param>
        /// <param name="EndEquipmentCode"></param>
        /// <param name="Type"></param>
        /// <returns></returns>
        [HttpGet]
        [Authenticate]
        public object Ems00007GetReason(string Token, string StartReasonCode = null, string EndReasonCode = null,
             string StartDate = null, string EndDate = null, string StartEquipmentCode = null, string EndEquipmentCode = null,
               string Type = null)
        {
            DataLogerService.writeURL(Token, null);
            UtilBussinessService.WriteLog(UtilBussinessService.detoken(Token), "设备叫修结案处理", null, "查看", null);
            return EMBussinessService.Ems00007GetReason(Token, StartReasonCode, EndReasonCode, StartDate, EndDate, StartEquipmentCode, EndEquipmentCode, Type);
        }

        /// <summary>
        /// 根据原因码获取查询条件内的叫修单明细列表
        /// SAM 2017年8月3日15:37:27
        /// </summary>
        /// <param name="Token"></param>
        /// <param name="ReasonID"></param>
        /// <param name="StartDate"></param>
        /// <param name="EndDate"></param>
        /// <param name="StartEquipmentCode"></param>
        /// <param name="EndEquipmentCode"></param>
        /// <param name="Type"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <returns></returns>
        [HttpGet]
        [Authenticate]
        public object Ems00007GetReasonDetailList(string Token, string ReasonID,
            string StartDate = null, string EndDate = null,
             string StartEquipmentCode = null, string EndEquipmentCode = null,
              string Type = null,
           int page = 1, int rows = 10)
        {
            DataLogerService.writeURL(Token, null);
            UtilBussinessService.WriteLog(UtilBussinessService.detoken(Token), "设备叫修结案处理", null, "查看", null);
            return EMBussinessService.Ems00007GetReasonDetailList(Token, ReasonID, StartDate, EndDate, StartEquipmentCode, EndEquipmentCode, Type, page, rows);
        }

        /// <summary>
        /// 維修原因統計分析-设备列表
        /// SAM 2017年8月3日15:50:51
        /// </summary>
        /// <param name="Token"></param>
        /// <param name="StartDate"></param>
        /// <param name="EndDate"></param>
        /// <param name="StartEquipmentCode"></param>
        /// <param name="EndEquipmentCode"></param>
        /// <param name="Type"></param>
        /// <returns></returns>
        [HttpGet]
        [Authenticate]
        public object Ems00007GetEquipmentList(string Token,
            string StartDate = null, string EndDate = null,
             string StartEquipmentCode = null, string EndEquipmentCode = null,
              string Type = null)
        {
            DataLogerService.writeURL(Token, null);
            UtilBussinessService.WriteLog(UtilBussinessService.detoken(Token), "设备叫修结案处理", null, "查看", null);
            return EMBussinessService.Ems00007GetEquipmentList(Token, StartDate, EndDate, StartEquipmentCode, EndEquipmentCode, Type);
        }

        /// <summary>
        ///  維修原因統計分析-设备叫修单明细列表
        ///  SAM 2017年8月3日16:01:17
        /// </summary>
        /// <param name="Token"></param>
        /// <param name="EquipmentID"></param>
        /// <param name="StartDate"></param>
        /// <param name="EndDate"></param>
        /// <param name="StartEquipmentCode"></param>
        /// <param name="EndEquipmentCode"></param>
        /// <param name="Type"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <returns></returns>
        [HttpGet]
        [Authenticate]
        public object Ems00007GetEquipmentDetailList(string Token, string EquipmentID,
        string StartDate = null, string EndDate = null,
        string StartEquipmentCode = null, string EndEquipmentCode = null,
        string Type = null, int page = 1, int rows = 10)
        {
            DataLogerService.writeURL(Token, null);
            UtilBussinessService.WriteLog(UtilBussinessService.detoken(Token), "设备叫修结案处理", null, "查看", null);
            return EMBussinessService.Ems00007GetEquipmentDetailList(Token, EquipmentID, StartDate, EndDate, StartEquipmentCode, EndEquipmentCode, Type, page, rows);
        }

        #endregion

        #region EMS00008设备保养清单设定
        /// <summary>
        /// 保养项目主档列表
        /// SAM 2017年7月5日14:24:47
        /// </summary>
        /// <param name="Token"></param>
        /// <param name="Code"></param>
        /// <param name="Name"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <returns></returns>
        [HttpGet]
        [Authenticate]
        public object Ems00008GetProjectList(string Token, string Code = null, string Name = null, int page = 1, int rows = 10)
        {
            DataLogerService.writeURL(Token, "Code:" + Code + "--->Name:" + Name);
            UtilBussinessService.WriteLog(UtilBussinessService.detoken(Token), " 保养项目主档", null, "查看", "Code:" + Code + "--->Name:" + Name);
            return EMBussinessService.Ems00008GetProjectList(Token, Code, Name, page, rows);
        }

        /// <summary>
        /// 保养项目的保存
        /// SAM 2017年7月5日14:34:58
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Authenticate]
        public object Ems00008ProjectSave(JObject request)
        {
            DataLogerService.writeURL(request.Value<string>("Token"), request.ToString());
            UtilBussinessService.WriteLog(UtilBussinessService.detoken(request.Value<string>("Token")), " 保养项目主档", null, "保存", request.ToString());
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
                deleted = EMBussinessService.Ems00008Projectdelete(request.Value<string>("Token"), request.Value<JArray>("deleted"));
            if (isInsert)
                Insert = EMBussinessService.Ems00008Projectinsert(request.Value<string>("Token"), request.Value<JArray>("inserted"));
            if (isUpdate)
                Update = EMBussinessService.Ems00008Projectupdate(request.Value<string>("Token"), request.Value<JArray>("updated"));

            return new { inserted = Insert, updated = Update, deleted = deleted };
        }

        /// <summary>
        /// 获取保养类型列表
        /// SAM 2017年7月5日14:41:24
        /// </summary>
        /// <param name="Token"></param>
        /// <param name="Code"></param>
        /// <param name="Name"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <returns></returns>
        [HttpGet]
        [Authenticate]
        public object Ems00008GetTypeList(string Token, string Code = null, string Name = null, int page = 1, int rows = 10)
        {
            DataLogerService.writeURL(Token, "Code:" + Code + "--->Name:" + Name);
            UtilBussinessService.WriteLog(UtilBussinessService.detoken(Token), "保养类型主档", null, "查看", "Code:" + Code + "--->Name:" + Name);
            return EMBussinessService.Ems00008GetTypeList(Token, Code, Name, page, rows);
        }

        /// <summary>
        /// 保养类型保存
        /// SAM 2017年7月5日14:57:14
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Authenticate]
        public object Ems00008TypeSave(JObject request)
        {
            DataLogerService.writeURL(request.Value<string>("Token"), request.ToString());
            UtilBussinessService.WriteLog(UtilBussinessService.detoken(request.Value<string>("Token")), " 保养类型主档", null, "保存", request.ToString());
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
                deleted = EMBussinessService.Ems00008Typedelete(request.Value<string>("Token"), request.Value<JArray>("deleted"));
            if (isInsert)
                Insert = IPBussinessService.Parameterinsert(request.Value<string>("Token"), request.Value<JArray>("inserted"), Framework.SystemID + "0191213000023");
            if (isUpdate)
                Update = EMBussinessService.Ems00008Typeupdate(request.Value<string>("Token"), request.Value<JArray>("updated"));

            return new { inserted = Insert, updated = Update, deleted = deleted };
        }

        /// <summary>
        /// 设备保养清单设定列表
        /// SAM 2017年7月5日14:58:12
        /// </summary>
        /// <param name="Token"></param>
        /// <param name="Code"></param>
        /// <param name="Name"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <returns></returns>
        [HttpGet]
        [Authenticate]
        public object Ems00008GetList(string Token, string Code = null, string Name = null, int page = 1, int rows = 10)
        {
            DataLogerService.writeURL(Token, "Code:" + Code + "--->Name:" + Name);
            UtilBussinessService.WriteLog(UtilBussinessService.detoken(Token), "保养类型主档", null, "查看", "Code:" + Code + "--->Name:" + Name);
            return EMBussinessService.Ems00008GetList(Token, Code, Name, page, rows);
        }

        /// <summary>
        /// 设备保养清单设定保存
        /// SAM 2017年7月5日15:02:00
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Authenticate]
        public object Ems00008Save(JObject request)
        {
            DataLogerService.writeURL(request.Value<string>("Token"), request.ToString());
            UtilBussinessService.WriteLog(UtilBussinessService.detoken(request.Value<string>("Token")), " 保养类型主档", null, "保存", request.ToString());
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
                deleted = EMBussinessService.Ems00008delete(request.Value<string>("Token"), request.Value<JArray>("deleted"));
            if (isInsert)
                Insert = EMBussinessService.Ems00008insert(request.Value<string>("Token"), request.Value<JArray>("inserted"));
            if (isUpdate)
                Update = EMBussinessService.Ems00008update(request.Value<string>("Token"), request.Value<JArray>("updated"));

            return new { inserted = Insert, updated = Update, deleted = deleted };
        }

        /// <summary>
        /// 设备设定列表
        /// SAM 2017年7月5日15:15:00
        /// </summary>
        /// <param name="Token"></param>
        /// <param name="EquipmentMaintenanceListID"></param>
        /// <param name="Code"></param>
        /// <param name="Name"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <returns></returns>
        [HttpGet]
        [Authenticate]
        public object Ems00008GetDeviceSettingList(string Token, string EquipmentMaintenanceListID, string Code = null, string Name = null, int page = 1, int rows = 10)
        {
            DataLogerService.writeURL(Token, "EquipmentMaintenanceListID:" + EquipmentMaintenanceListID);
            UtilBussinessService.WriteLog(UtilBussinessService.detoken(Token), "保养类型主档", null, "查看", "EquipmentMaintenanceListID:" + EquipmentMaintenanceListID);
            return EMBussinessService.Ems00008GetDeviceSettingList(Token, EquipmentMaintenanceListID, Code, Name, page, rows);
        }

        /// <summary>
        /// 保養清單:設備開窗-已選擇資料
        /// SAM 2017年7月14日15:22:41
        /// </summary>
        /// <param name="Token"></param>
        /// <param name="EquipmentMaintenanceListID"></param>
        /// <param name="Code"></param>
        /// <param name="Name"></param>
        /// <returns></returns>
        [HttpGet]
        [Authenticate]
        public object Ems00008DeviceSettingList(string Token, string EquipmentMaintenanceListID, string Code = null, string Name = null)
        {
            DataLogerService.writeURL(Token, "EquipmentMaintenanceListID:" + EquipmentMaintenanceListID);
            UtilBussinessService.WriteLog(UtilBussinessService.detoken(Token), "保养类型主档", null, "查看", "EquipmentMaintenanceListID:" + EquipmentMaintenanceListID);
            return EMBussinessService.Ems00008DeviceSettingList(Token, EquipmentMaintenanceListID, Code, Name);
        }


        /// <summary>
        /// 保養清單:設備開窗-未選擇資料
        /// SAM 2017年7月14日15:03:04
        /// </summary>
        /// <param name="Token"></param>
        /// <param name="EquipmentMaintenanceListID"></param>
        /// <param name="Code"></param>
        /// <param name="Name"></param>
        /// <returns></returns>
        [HttpGet]
        [Authenticate]
        public object EMS00008ListDeviceAdd(string Token, string EquipmentMaintenanceListID, string Code = null, string Name = null)
        {
            DataLogerService.writeURL(Token, "EquipmentMaintenanceListID:" + EquipmentMaintenanceListID);
            UtilBussinessService.WriteLog(UtilBussinessService.detoken(Token), "保养类型主档", null, "查看", "EquipmentMaintenanceListID:" + EquipmentMaintenanceListID);
            return EMBussinessService.EMS00008ListDeviceAdd(Token, EquipmentMaintenanceListID, Code, Name);
        }

        /// <summary>
        /// 保养清单-设备的保存
        /// SAM 2017年7月14日15:30:58
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Authenticate]
        public object EMS00008ListDeviceAddSave(JObject request)
        {
            DataLogerService.writeURL(request.Value<string>("Token"), request.ToString());
            UtilBussinessService.WriteLog(UtilBussinessService.detoken(request.Value<string>("Token")), "清单明细保存", null, "保存", request.ToString());
            return EMBussinessService.EMS00008ListDeviceAddSave(request);
        }




        /// <summary>
        /// 设备设定保存
        /// SAM 2017年7月5日15:27:18
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Authenticate]
        public object Ems00008DeviceSettingSave(JObject request)
        {
            DataLogerService.writeURL(request.Value<string>("Token"), request.ToString());
            UtilBussinessService.WriteLog(UtilBussinessService.detoken(request.Value<string>("Token")), " 保养类型主档", null, "保存", request.ToString());
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
                deleted = EMBussinessService.Ems00008Detaildelete(request.Value<string>("Token"), request.Value<JArray>("deleted"));
            if (isInsert)
                Insert = EMBussinessService.Ems00008Detailinsert(request.Value<string>("Token"), request.Value<JArray>("inserted"), 2);

            return new { inserted = Insert, updated = Update, deleted = deleted };
        }

        /// <summary>
        /// 获取清单明细列表
        /// SAM 2017年7月5日15:39:25
        /// </summary>
        /// <param name="Token"></param>
        /// <param name="EquipmentMaintenanceListID"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <returns></returns>
        [HttpGet]
        [Authenticate]
        public object Ems00008GetDetailList(string Token, string EquipmentMaintenanceListID, int page = 1, int rows = 10)
        {
            DataLogerService.writeURL(Token, "EquipmentMaintenanceListID:" + EquipmentMaintenanceListID);
            UtilBussinessService.WriteLog(UtilBussinessService.detoken(Token), "保养类型主档", null, "查看", "EquipmentMaintenanceListID:" + EquipmentMaintenanceListID);
            return EMBussinessService.Ems00008GetDetailList(Token, EquipmentMaintenanceListID, page, rows);
        }

        /// <summary>
        /// 获取清单明细列表（不分页）
        /// SAM 2017年7月14日11:41:26
        /// </summary>
        /// <param name="Token"></param>
        /// <param name="EquipmentMaintenanceListID"></param>
        /// <returns></returns>
        [HttpGet]
        [Authenticate]
        public object Ems00008DetailList(string Token, string EquipmentMaintenanceListID)
        {
            DataLogerService.writeURL(Token, "EquipmentMaintenanceListID:" + EquipmentMaintenanceListID);
            UtilBussinessService.WriteLog(UtilBussinessService.detoken(Token), "保养类型主档", null, "查看", "EquipmentMaintenanceListID:" + EquipmentMaintenanceListID);
            return EMBussinessService.Ems00008DetailList(Token, EquipmentMaintenanceListID);
        }



        /// <summary>
        /// 获取指定清单未设定的保养项目列表
        /// SAM 2017年7月14日11:22:59
        /// </summary>
        /// <param name="Token"></param>
        /// <param name="EquipmentMaintenanceListID"></param>
        /// <param name="Code"></param>
        /// <param name="Name"></param>
        /// <returns></returns>
        [HttpGet]
        [Authenticate]
        public object EMS00008ListDetailAdd(string Token, string EquipmentMaintenanceListID, string Code = null, string Name = null)
        {
            DataLogerService.writeURL(Token, "EquipmentMaintenanceListID:" + EquipmentMaintenanceListID);
            UtilBussinessService.WriteLog(UtilBussinessService.detoken(Token), "保养类型主档", null, "查看", "EquipmentMaintenanceListID:" + EquipmentMaintenanceListID);
            return EMBussinessService.EMS00008ListDetailAdd(Token, EquipmentMaintenanceListID, Code, Name);
        }

        /// <summary>
        /// 清单明细保存
        /// SAM 2017年7月14日11:40:16
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Authenticate]
        public object EMS00008ListDetailSave(JObject request)
        {
            DataLogerService.writeURL(request.Value<string>("Token"), request.ToString());
            UtilBussinessService.WriteLog(UtilBussinessService.detoken(request.Value<string>("Token")), "清单明细保存", null, "保存", request.ToString());
            return EMBussinessService.EMS00008ListDetailSave(request);
        }

        /// <summary>
        /// 清单明细保存
        /// SAM 2017年7月5日15:27:18
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Authenticate]
        public object Ems00008DetailSave(JObject request)
        {
            DataLogerService.writeURL(request.Value<string>("Token"), request.ToString());
            UtilBussinessService.WriteLog(UtilBussinessService.detoken(request.Value<string>("Token")), " 保养类型主档", null, "保存", request.ToString());
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
                deleted = EMBussinessService.Ems00008Detaildelete(request.Value<string>("Token"), request.Value<JArray>("deleted"));
            if (isInsert)
                Insert = EMBussinessService.Ems00008Detailinsert(request.Value<string>("Token"), request.Value<JArray>("inserted"), 1);

            return new { inserted = Insert, updated = Update, deleted = deleted };
        }

        /// <summary>
        ///  保養類型删除
        ///  SAM 2017年7月19日11:07:59
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Authenticate]
        public object Ems00008TypeDelete(JObject request)
        {
            DataLogerService.writeURL(request.Value<string>("Token"), request.ToString());
            return EMBussinessService.Ems00008TypeDelete(request);
        }

        /// <summary>
        /// 保養項目删除
        /// SAM 2017年7月19日11:08:20
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Authenticate]
        public object Ems00008ProjectDelete(JObject request)
        {
            DataLogerService.writeURL(request.Value<string>("Token"), request.ToString());
            return EMBussinessService.Ems00008ProjectDelete(request);
        }

        /// <summary>
        /// 保养清单删除
        /// SAM 2017年7月19日11:08:38
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Authenticate]
        public object Ems00008Delete(JObject request)
        {
            DataLogerService.writeURL(request.Value<string>("Token"), request.ToString());
            return EMBussinessService.Ems00008Delete(request);
        }


        #endregion

        #region EMS00009设备保养工单维护
        /// <summary>
        ///  获取保养单主列表
        ///  SAM 2017年7月9日09:39:19
        /// </summary>
        /// <param name="Token"></param>
        /// <param name="Type">保养类型 流水号</param>
        /// <param name="Status">状态，多个，用逗号隔开</param>
        /// <param name="EquipmentID">设备流水号</param>
        /// <param name="UserID">保养人员流水号</param>
        /// <param name="StartCode">起始保养工单</param>
        /// <param name="EndCode">结束保养工单</param>
        /// <param name="StartDate">起始保养日期</param>
        /// <param name="EndDate">结束保养日期</param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <returns></returns>
        [HttpGet]
        [Authenticate]
        public object Ems00009GetList(string Token, string Type = null, string Status = null, string EquipmentID = null, string UserID = null, string StartCode = null, string EndCode = null, string StartDate = null, string EndDate = null, int page = 1, int rows = 10)
        {
            DataLogerService.writeURL(Token, null);
            UtilBussinessService.WriteLog(UtilBussinessService.detoken(Token), "保养工单维护", null, "查看", null);
            return EMBussinessService.Ems00009GetList(Token, Type, Status, EquipmentID, UserID, StartCode, EndCode, StartDate, EndDate, page, rows);
        }

        /// <summary>
        /// 获取单据预设子元挑选下拉框
        /// SAM 2017年8月3日23:28:28
        /// </summary>
        /// <param name="Token"></param>
        /// <returns></returns>
        [HttpGet]
        [Authenticate]
        public object Ems00009GetTypeList(string Token)
        {
            DataLogerService.writeURL(Token, null);
            return UtilBussinessService.GetTypeList(UtilBussinessService.detoken(Token), Framework.SystemID + "0201213000039");
        }

        /// <summary>
        /// 保养工单的新增
        /// SAM 2017年7月9日09:47:32
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Authenticate]
        public object Ems00009Add(JObject request)
        {
            DataLogerService.writeURL(request.Value<string>("Token"), request.ToString());
            UtilBussinessService.WriteLog(UtilBussinessService.detoken(request.Value<string>("Token")), "保养工单维护", null, "新增", request.ToString());
            return EMBussinessService.Ems00009Add(request);
        }


        /// <summary>
        /// 根据保养类型获取保养清单
        /// SAM 2017年7月9日16:22:49
        /// </summary>
        /// <param name="Token"></param>
        /// <param name="Type">保养类型流水号</param>
        /// <returns></returns>
        [HttpGet]
        [Authenticate]
        public object Ems00009GetEquMaiList(string Token, string Type)
        {
            DataLogerService.writeURL(Token, null);
            return EMBussinessService.Ems00009GetEquMaiList(Token, Type);
        }

        /// <summary>
        /// 根据保养清单获取设备设定列表
        /// SAM 2017年7月9日16:27:22
        /// </summary>
        /// <param name="Token"></param>
        /// <param name="EquipmentMaintenanceListID">保养清单流水号</param>
        /// <returns></returns>
        [HttpGet]
        [Authenticate]
        public object Ems00009GetEquMaiDetailList(string Token, string EquipmentMaintenanceListID)
        {
            DataLogerService.writeURL(Token, null);
            return EMBussinessService.Ems00009GetEquMaiDetailList(Token, EquipmentMaintenanceListID);
        }

        /// <summary>
        /// 根据保养清单获取设备设定列表，同时根据保养工单将保养工单已存在的点亮
        /// SAM 2017年7月31日23:22:22
        /// </summary>
        /// <param name="Token"></param>
        /// <param name="MaintenanceOrderID"></param>
        /// <param name="EquipmentMaintenanceListID"></param>
        /// <returns></returns>
        [HttpGet]
        [Authenticate]
        public object Ems00009GetEquDetailList(string Token, string MaintenanceOrderID, string EquipmentMaintenanceListID)
        {
            DataLogerService.writeURL(Token, null);
            return EMBussinessService.Ems00009GetEquDetailList(Token, MaintenanceOrderID, EquipmentMaintenanceListID);
        }

        /// <summary>
        /// 保养工单的更新
        /// SAM 2017年7月9日09:50:25
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Authenticate]
        public object Ems00009Update(JObject request)
        {
            DataLogerService.writeURL(request.Value<string>("Token"), request.ToString());
            UtilBussinessService.WriteLog(UtilBussinessService.detoken(request.Value<string>("Token")), "保养工单维护", null, "更新", request.ToString());
            return EMBussinessService.Ems00009Update(request);
        }

        /// <summary>
        /// 保养工单的删除
        /// SAM 2017年7月9日09:50:35
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Authenticate]
        public object Ems00009Delete(JObject request)
        {
            DataLogerService.writeURL(request.Value<string>("Token"), request.ToString());
            UtilBussinessService.WriteLog(UtilBussinessService.detoken(request.Value<string>("Token")), "保养工单维护", null, "删除", request.ToString());
            return EMBussinessService.Ems00009Delete(request);
        }

        /// <summary>
        /// 保养工单核发
        /// SAM 2017年7月9日16:33:06
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Authenticate]
        public object Ems00009OP(JObject request)
        {
            DataLogerService.writeURL(request.Value<string>("Token"), request.ToString());
            UtilBussinessService.WriteLog(UtilBussinessService.detoken(request.Value<string>("Token")), "保养工单维护", null, "核发", request.ToString());
            return EMBussinessService.Ems00009OP(request);
        }

        /// <summary>
        /// 保养工单作废
        /// SAM 2017年7月9日16:33:26
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Authenticate]
        public object Ems00009CA(JObject request)
        {
            DataLogerService.writeURL(request.Value<string>("Token"), request.ToString());
            UtilBussinessService.WriteLog(UtilBussinessService.detoken(request.Value<string>("Token")), "保养工单维护", null, "作废", request.ToString());
            return EMBussinessService.Ems00009CA(request);
        }

        /// <summary>
        /// 根据保养工单获取他的设备保养明细列表
        /// SAM 2017年7月9日10:01:10
        /// </summary>
        /// <param name="Token"></param>
        /// <param name="MaintenanceOrderID"></param>
        /// <param name="EquipmentID"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <returns></returns>
        [HttpGet]
        [Authenticate]
        public object Ems00009GetDetailList(string Token, string MaintenanceOrderID, string EquipmentID = null, int page = 1, int rows = 10)
        {
            DataLogerService.writeURL(Token, null);
            UtilBussinessService.WriteLog(UtilBussinessService.detoken(Token), "保养工单维护", null, "查看", null);
            return EMBussinessService.Ems00009GetDetailList(Token, MaintenanceOrderID, EquipmentID, page, rows);
        }

        //7.设备明细弹窗
        //PopUp/EMSGetEquMaiEquipmentList

        /// <summary>
        /// 保养工单设备明细的新增
        /// SAM 2017年7月9日10:03:16
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Authenticate]
        public object Ems00009DetailAdd(JObject request)
        {
            DataLogerService.writeURL(request.Value<string>("Token"), request.ToString());
            UtilBussinessService.WriteLog(UtilBussinessService.detoken(request.Value<string>("Token")), "保养工单维护", null, "新增", request.ToString());
            return EMBussinessService.Ems00009DetailAdd(request);
        }

        /// <summary>
        /// 保养工单设备明细的删除
        /// SAM 2017年7月9日10:03:25
        /// </summary>
        /// <param name = "request" ></param>
        [HttpPost]
        [Authenticate]
        public object Ems00009DetailDelete(JObject request)
        {
            DataLogerService.writeURL(request.Value<string>("Token"), request.ToString());
            UtilBussinessService.WriteLog(UtilBussinessService.detoken(request.Value<string>("Token")), "保养工单维护", null, "删除", request.ToString());
            return EMBussinessService.Ems00009DetailDelete(request);
        }

        /// <summary>
        /// 根据保养工单设备获取保养项目列表
        /// SAM 2017年7月9日10:07:04
        /// </summary>
        /// <param name="Token"></param>
        /// <param name="MaiOrderEquipmentID">保养工单-设备设定流水号</param>
        /// <param name="Code">项目代号，关键字查询</param>
        /// <param name="Name">说明，关键字查询</param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <returns></returns>
        [HttpGet]
        [Authenticate]
        public object Ems00009GetProjectList(string Token, string MaiOrderEquipmentID, string Code = null, string Name = null, int page = 1, int rows = 10)
        {
            DataLogerService.writeURL(Token, null);
            UtilBussinessService.WriteLog(UtilBussinessService.detoken(Token), "保养工单维护", null, "查看", null);
            return EMBussinessService.Ems00009GetProjectList(Token, MaiOrderEquipmentID, Code, Name, page, rows);
        }

        //11.保养项目弹窗
        //PopUp/EMSGetEquMaiProjectList

        /// <summary>
        /// 保养项目新增
        /// SAM 2017年7月9日10:08:27
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Authenticate]
        public object Ems00009ProjectAdd(JObject request)
        {
            DataLogerService.writeURL(request.Value<string>("Token"), request.ToString());
            UtilBussinessService.WriteLog(UtilBussinessService.detoken(request.Value<string>("Token")), "保养工单维护", null, "新增", request.ToString());
            return EMBussinessService.Ems00009ProjectAdd(request);
        }

        /// <summary>
        /// 保养项目删除
        /// SAM 2017年7月9日10:08:20
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Authenticate]
        public object Ems00009ProjectDelete(JObject request)
        {
            DataLogerService.writeURL(request.Value<string>("Token"), request.ToString());
            UtilBussinessService.WriteLog(UtilBussinessService.detoken(request.Value<string>("Token")), "保养工单维护", null, "删除", request.ToString());
            return EMBussinessService.Ems00009ProjectDelete(request);
        }
        #endregion

        #region EMS00010設備保養資料維護
        /// <summary>
        /// 設備保養資料主列表（保养单与明细一起显示，以明细为主）
        /// SAM 2017年7月9日11:45:31
        /// </summary>
        /// <param name="Token"></param>
        /// <param name="TypeCode">保养类型代号</param>
        /// <param name="Status">状态，多个，用逗号隔开</param>
        /// <param name="EquipmentCode">设备代号</param>
        /// <param name="UserCode">保养人代号</param>
        /// <param name="StartCode">起始保养工单</param>
        /// <param name="EndCode">结束保养工单</param>
        /// <param name="StartDate">起始保养日期</param>
        /// <param name="EndDate">结束保养日期</param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <returns></returns>
        [HttpGet]
        [Authenticate]
        public object Ems00010GetList(string Token, string TypeCode = null, string Status = null, string EquipmentCode = null, string UserCode = null, string StartCode = null, string EndCode = null, string StartDate = null, string EndDate = null, int page = 1, int rows = 10)
        {
            DataLogerService.writeURL(Token, null);
            UtilBussinessService.WriteLog(UtilBussinessService.detoken(Token), "設備保養資料維護", null, "查看", null);
            return EMBussinessService.Ems00010GetList(Token, TypeCode, Status, EquipmentCode, UserCode, StartCode, EndCode, StartDate, EndDate, page, rows);
        }

        //2.获取明细列表（这里表面上叫做明细，实际上是保养项目）
        //  EquipmentManagement/Ems00009GetProjectList

        //3.保养项目弹窗
        //   PopUp/EMSGetEquMaiProjectList

        //4.保养项目新增 （多条用逗号隔开）
        //  EquipmentManagement/Ems00009ProjectAdd

        //5.保养项目删除 
        //  EquipmentManagement/Ems00009ProjectDelete

        /// <summary>
        /// 保养项目的保存
        /// SAM 2017年7月9日17:16:40
        /// </summary>
        /// <param name="request">JSON数据</param>
        /// <returns></returns>
        [HttpPost]
        [Authenticate]
        public object Ems00010ProjectSave(JObject request)
        {
            DataLogerService.writeURL(request.Value<string>("Token"), request.ToString());
            UtilBussinessService.WriteLog(UtilBussinessService.detoken(request.Value<string>("Token")), "設備保養資料維護", null, "保存", request.ToString());
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
                Update = EMBussinessService.Ems00010Projectupdate(request.Value<string>("Token"), request.Value<JArray>("updated"));

            return new { inserted = Insert, updated = Update, deleted = deleted };
        }

        /// <summary>
        /// 开始保养
        /// SAM 2017年7月9日17:20:48
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Authenticate]
        public object Ems00010Start(JObject request)
        {
            DataLogerService.writeURL(request.Value<string>("Token"), request.ToString());
            UtilBussinessService.WriteLog(UtilBussinessService.detoken(request.Value<string>("Token")), "設備保養資料維護", null, "开始", request.ToString());
            return EMBussinessService.Ems00010Start(request);
        }

        /// <summary>
        /// 结束保养
        /// SAM 2017年7月9日17:21:05
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Authenticate]
        public object Ems00010End(JObject request)
        {
            DataLogerService.writeURL(request.Value<string>("Token"), request.ToString());
            UtilBussinessService.WriteLog(UtilBussinessService.detoken(request.Value<string>("Token")), "設備保養資料維護", null, "开始", request.ToString());
            return EMBussinessService.Ems00010End(request);
        }
        #endregion

        #region EMS00011設備保養結案與還原
        /// <summary>
        /// 設備保養結案與還原-主列表
        /// SAM 2017年7月9日17:59:29
        /// </summary>
        /// <param name="Token"></param>
        /// <param name="Type"></param>
        /// <param name="Status"></param>
        /// <param name="EquipmentID"></param>
        /// <param name="UserID"></param>
        /// <param name="StartNo"></param>
        /// <param name="EndNo"></param>
        /// <param name="StartDate"></param>
        /// <param name="EndDate"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <returns></returns>
        [HttpGet]
        [Authenticate]
        public object Ems00011GetList(string Token, string Type = null, string Status = null, string EquipmentID = null, string UserID = null, string StartNo = null, string EndNo = null, string StartDate = null, string EndDate = null, int page = 1, int rows = 10)
        {
            DataLogerService.writeURL(Token, null);
            UtilBussinessService.WriteLog(UtilBussinessService.detoken(Token), "設備保養結案與還原", null, "查看", null);
            return EMBussinessService.Ems00011GetList(Token, Type, Status, EquipmentID, UserID, StartNo, EndNo, StartDate, EndDate, page, rows);
        }

        /// <summary>
        /// 保养单设备明细列表
        /// SAM 2017年7月9日17:33:38
        /// </summary>
        /// <param name="Token"></param>
        /// <param name="MaintenanceOrderID"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <returns></returns>
        [HttpGet]
        [Authenticate]
        public object Ems00011DetailGetList(string Token, string MaintenanceOrderID, int page = 1, int rows = 10)
        {
            DataLogerService.writeURL(Token, null);
            UtilBussinessService.WriteLog(UtilBussinessService.detoken(Token), "保养工单维护", null, "查看", null);
            return EMBussinessService.Ems00011GetDetailList(Token, MaintenanceOrderID, page, rows);
        }

        /// <summary>
        /// 保养项目列表
        /// SAM 2017年7月9日17:34:11
        /// </summary>
        /// <param name="Token"></param>
        /// <param name="MaiOrderEquipmentID"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <returns></returns>
        [HttpGet]
        [Authenticate]
        public object Ems00011MTItemGetList(string Token, string MaiOrderEquipmentID, int page = 1, int rows = 10)
        {
            DataLogerService.writeURL(Token, null);
            UtilBussinessService.WriteLog(UtilBussinessService.detoken(Token), "保养工单维护", null, "查看", null);
            return EMBussinessService.Ems00011GetProjectList(Token, MaiOrderEquipmentID, page, rows);
        }

        /// <summary>
        /// 保养设备结案
        /// SAM 2017年7月9日17:40:35
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Authenticate]
        public object Ems00011CloseCase(JObject request)
        {
            DataLogerService.writeURL(request.Value<string>("Token"), request.ToString());
            UtilBussinessService.WriteLog(UtilBussinessService.detoken(request.Value<string>("Token")), "設備保養資料維護", null, "结案", request.ToString());
            return EMBussinessService.Ems00011CloseCase(request);
        }

        /// <summary>
        /// 保养设备还原
        /// SAM 2017年7月9日17:41:33
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Authenticate]
        public object Ems00011Restore(JObject request)
        {
            DataLogerService.writeURL(request.Value<string>("Token"), request.ToString());
            UtilBussinessService.WriteLog(UtilBussinessService.detoken(request.Value<string>("Token")), "設備保養資料維護", null, "还原", request.ToString());
            return EMBussinessService.Ems00011Restore(request);
        }
        #endregion
    }
}
