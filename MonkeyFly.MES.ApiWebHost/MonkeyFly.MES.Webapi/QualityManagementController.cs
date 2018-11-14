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
    /// 品质管理
    /// </summary>
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class QualityManagementController : ApiController
    {
        /// <summary>
        /// QualityManagementAPI
        /// </summary>
        [HttpGet]
        public void QualityManagementAPI() { }

        #region QCS00001抽样检验设定
        /// <summary>
        /// 抽样检验设定主列表
        /// SAM 2017年6月5日10:39:01
        /// </summary>
        /// <param name="Token">授权码</param>
        /// <param name="Type"></param>
        /// <param name="page">页码</param>
        /// <param name="rows">行数</param>
        /// <returns></returns>
        [HttpGet]
        [Authenticate]
        public object Qcs00001GetList(string Token, string Type = null, int page = 1, int rows = 10)
        {
            DataLogerService.writeURL(Token, "Type:" + Type);
            UtilBussinessService.WriteLog(UtilBussinessService.detoken(Token), "抽样检验设定维护", null, "查看", "Type:" + Type);
            return QMBussinessService.Qcs00001GetList(Token, Type, page, rows);
        }

        /// <summary>
        /// 抽样检验设定保存
        /// SAM 2017年6月5日10:40:28
        /// </summary>
        /// <param name="request">JSON数据</param>
        /// <returns></returns>
        [HttpPost]
        [Authenticate]
        public object Qcs00001Save(JObject request)
        {
            DataLogerService.writeURL(request.Value<string>("Token"), request.ToString());
            UtilBussinessService.WriteLog(UtilBussinessService.detoken(request.Value<string>("Token")), "检验群组码主档", null, "保存", request.ToString());
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
                deleted = QMBussinessService.Qcs00001delete(request.Value<string>("Token"), request.Value<JArray>("deleted"));
            if (isInsert)
                Insert = QMBussinessService.Qcs00001insert(request.Value<string>("Token"), request.Value<JArray>("inserted"));
            if (isUpdate)
                Update = QMBussinessService.Qcs00001update(request.Value<string>("Token"), request.Value<JArray>("updated"));

            return new { inserted = Insert, updated = Update, deleted = deleted };
        }

        /// <summary>
        /// 根据抽样检验设定获取他的明细
        /// SAM 2017年6月5日11:18:21
        /// </summary>
        /// <param name="Token">授权码</param>
        /// <param name="CheckTestSettingID"></param>
        /// <returns></returns>
        [HttpGet]
        [Authenticate]
        public object Qcs00001GetDetailsList(string Token, string CheckTestSettingID)
        {
            DataLogerService.writeURL(Token, "CheckTestSettingID:" + CheckTestSettingID);
            UtilBussinessService.WriteLog(UtilBussinessService.detoken(Token), "抽样检验设定维护", null, "查看", "CheckTestSettingID:" + CheckTestSettingID);
            return QMBussinessService.Qcs00001GetDetailsList(Token, CheckTestSettingID);
        }

        /// <summary>
        /// 抽样检验设定明细的保存
        /// SAM 2017年6月5日11:31:48
        /// </summary>
        /// <param name="request">JSON数据</param>
        /// <returns></returns>
        [HttpPost]
        [Authenticate]
        public object Qcs00001DetailsSave(JObject request)
        {
            DataLogerService.writeURL(request.Value<string>("Token"), request.ToString());
            UtilBussinessService.WriteLog(UtilBussinessService.detoken(request.Value<string>("Token")), "检验群组码主档", null, "保存", request.ToString());
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
                deleted = QMBussinessService.Qcs00001Detailsdelete(request.Value<string>("Token"), request.Value<JArray>("deleted"));
            if (isInsert)
                Insert = QMBussinessService.Qcs00001Detailsinsert(request.Value<string>("Token"), request.Value<JArray>("inserted"));
            if (isUpdate)
                Update = QMBussinessService.Qcs00001Detailsupdate(request.Value<string>("Token"), request.Value<JArray>("updated"));

            return new { inserted = Insert, updated = Update, deleted = deleted };
        }

        /// <summary>
        /// 单个抽样检验设定删除
        /// Mouse 2017年7月26日11:28:25
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Authenticate]
        public object Qcs00001Delete(JObject request)
        {
            DataLogerService.writeURL(request.Value<string>("Token"), request.ToString());
            UtilBussinessService.WriteLog(UtilBussinessService.detoken(request.Value<string>("Token")), "抽样检验设定维护", null, "删除", request.ToString());
            return QMBussinessService.Qcs00001Delete(request);
        }

        #endregion

        #region QCS00002檢驗項目類別維護
        /// <summary>
        /// 获取检验项目列表
        /// SAM 2017年6月9日10:28:17
        /// </summary>
        /// <param name="Token"></param>
        /// <param name="Code"></param>
        /// <param name="Status"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <returns></returns>
        [HttpGet]
        [Authenticate]
        public object Qcs00002GetProjectList(string Token, string Code = null, string Status = null, int page = 1, int rows = 10)
        {
            DataLogerService.writeURL(Token, "Code:" + Code + "--->Status:" + Status);
            UtilBussinessService.WriteLog(UtilBussinessService.detoken(Token), "检验项目", null, "查看", "Code:" + Code + "--->Status:" + Status);
            return QMBussinessService.Qcs00002GetProjectList(Token, Code, Status, page, rows);
        }

        /// <summary>
        /// 检验项目的保存
        /// SAM 2017年6月9日10:35:33
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Authenticate]
        public object Qcs00002ProjectSave(JObject request)
        {
            DataLogerService.writeURL(request.Value<string>("Token"), request.ToString());
            UtilBussinessService.WriteLog(UtilBussinessService.detoken(request.Value<string>("Token")), "检验项目", null, "保存", request.ToString());
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
                deleted = QMBussinessService.Qcs00002Projectdelete(request.Value<string>("Token"), request.Value<JArray>("deleted"));
            if (isInsert)
                Insert = QMBussinessService.Qcs00002Projectinsert(request.Value<string>("Token"), request.Value<JArray>("inserted"));
            if (isUpdate)
                Update = QMBussinessService.Qcs00002Projectupdate(request.Value<string>("Token"), request.Value<JArray>("updated"));

            return new { inserted = Insert, updated = Update, deleted = deleted };
        }

        /// <summary>
        /// 检验项目的删除
        /// SAM 2017年9月28日10:35:35
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Authenticate]
        public object Qcs00002ProjectDelete(JObject request)
        {
            DataLogerService.writeURL(request.Value<string>("Token"), request.ToString());
            UtilBussinessService.WriteLog(UtilBussinessService.detoken(request.Value<string>("Token")), "检验项目", null, "删除", request.ToString());
            return QMBussinessService.Qcs00002ProjectDelete(request);
        }

        /// <summary>
        /// 检验类别列表
        /// SAM 2017年6月9日10:36:35
        /// </summary>
        /// <param name="Token"></param>
        /// <param name="Code"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <returns></returns>
        [HttpGet]
        [Authenticate]
        public object Qcs00002GetTypeList(string Token, string Code = null, int page = 1, int rows = 10)
        {
            DataLogerService.writeURL(Token, "Code:" + Code);
            UtilBussinessService.WriteLog(UtilBussinessService.detoken(Token), "检验类别", null, "查看", "Code:" + Code);
            return QMBussinessService.Qcs00002GetTypeList(Token, Code, page, rows);
        }

        /// <summary>
        /// 检验类别保存
        /// SAM 2017年6月9日10:37:42
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Authenticate]
        public object Qcs00002TypeSave(JObject request)
        {
            DataLogerService.writeURL(request.Value<string>("Token"), request.ToString());
            UtilBussinessService.WriteLog(UtilBussinessService.detoken(request.Value<string>("Token")), "检验类别", null, "保存", request.ToString());
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
                Insert = QMBussinessService.Qcs00002Typeinsert(request.Value<string>("Token"), request.Value<JArray>("inserted"));
            if (isUpdate)
                Update = QMBussinessService.Qcs00002Typeupdate(request.Value<string>("Token"), request.Value<JArray>("updated"));

            return new { inserted = Insert, updated = Update, deleted = deleted };
        }

        /// <summary>
        /// 检验类别删除
        /// SAM 2017年10月17日16:08:34
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Authenticate]
        public object Qcs00002TypeDelete(JObject request)
        {
            DataLogerService.writeURL(request.Value<string>("Token"), request.ToString());
            UtilBussinessService.WriteLog(UtilBussinessService.detoken(request.Value<string>("Token")), "检验类别", null, "删除", request.ToString());
            return QMBussinessService.Qcs00002TypeDelete(request);
        }

        /// <summary>
        /// 检验类别的抽检设定列表
        /// SAM 2017-6-9 10:41:152
        /// </summary>
        /// <param name="Token"></param>
        /// <param name="TypeID"></param>
        /// <returns></returns>
        [HttpGet]
        [Authenticate]
        public object Qcs00002GetTypeDetailsList(string Token, string TypeID)
        {
            DataLogerService.writeURL(Token, "TypeID:" + TypeID);
            UtilBussinessService.WriteLog(UtilBussinessService.detoken(Token), "抽检设定", null, "查看", "TypeID:" + TypeID);
            return QMBussinessService.Qcs00002GetTypeDetailsList(Token, TypeID);
        }

        /// <summary>
        /// 检验类别的抽检设定保存
        /// SAM 2017年6月9日10:41:333
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Authenticate]
        public object Qcs00002TypeDetailsSave(JObject request)
        {
            DataLogerService.writeURL(request.Value<string>("Token"), request.ToString());
            UtilBussinessService.WriteLog(UtilBussinessService.detoken(request.Value<string>("Token")), "抽检设定", null, "保存", request.ToString());
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
                Update = QMBussinessService.Qcs00002TypeDetailsupdate(request.Value<string>("Token"), request.Value<JArray>("updated"));

            return new { inserted = Insert, updated = Update, deleted = deleted };
        }
        #endregion

        #region QCS00003检验群组码
        /// <summary>
        /// 检验群组码列表
        /// SAM 2017年5月26日11:41:52
        /// </summary>
        /// <param name="Token">授权码</param>
        /// <param name="Code">代号</param>
        /// <param name="Status">状态</param>
        /// <param name="page">页码</param>
        /// <param name="rows">行数</param>
        /// <returns></returns>
        [HttpGet]
        [Authenticate]
        public object Qcs00003GetList(string Token, string Code = null, string Status = null, int page = 1, int rows = 10)
        {
            DataLogerService.writeURL(Token, "Code:" + Code + "--->Status:" + Status);
            UtilBussinessService.WriteLog(UtilBussinessService.detoken(Token), "检验群组码主档", null, "查看", "Code:" + Code + "--->Status:" + Status);
            return QMBussinessService.Qcs00003GetList(Token, Code, Status, page, rows);
        }

        /// <summary>
        /// 检验群组码的保存
        /// SAM 2017年5月26日11:44:09
        /// </summary>
        /// <param name="request">JSON数据</param>
        /// <returns></returns>
        [HttpPost]
        [Authenticate]
        public object Qcs00003Save(JObject request)
        {
            DataLogerService.writeURL(request.Value<string>("Token"), request.ToString());
            UtilBussinessService.WriteLog(UtilBussinessService.detoken(request.Value<string>("Token")), "检验群组码主档", null, "保存", request.ToString());

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
            //    deleted = IPBussinessService.Inf00018Operationdelete(request.Value<string>("Token"), request.Value<JArray>("deleted"));
            if (isInsert)
                Insert = IPBussinessService.Parameterinsert(request.Value<string>("Token"), request.Value<JArray>("inserted"), Framework.SystemID + "0191213000018");
            if (isUpdate)
                Update = IPBussinessService.Parameterupdate(request.Value<string>("Token"), request.Value<JArray>("updated"));

            return new { inserted = Insert, updated = Update, deleted = deleted };
        }


        /// <summary>
        /// 根据检验群组码获取他的料品设定
        /// SAM 2017年5月26日11:46:34
        /// </summary>
        /// <param name="Token">授权码</param>
        /// <param name="GroupID"></param>
        /// <param name="page">页码</param>
        /// <param name="rows">行数</param>
        /// <returns></returns>
        [HttpGet]
        [Authenticate]
        public object Qcs00003GetDetailsList(string Token, string GroupID, int page = 1, int rows = 10)
        {
            DataLogerService.writeURL(Token, "GroupID:" + GroupID);
            UtilBussinessService.WriteLog(UtilBussinessService.detoken(Token), "检验群组码明细", null, "查看", "GroupID:" + GroupID);
            return QMBussinessService.Qcs00003GetDetailsList(Token, GroupID, page, rows);
        }

        /// <summary>
        /// 根据检验群组码获取他的料品设定（不分页）
        /// SAM 2017年5月25日14:17:04
        /// </summary>
        /// <param name="Token">授权码</param>
        /// <param name="GroupID"></param>
        /// <returns></returns>
        [HttpGet]
        [Authenticate]
        public object Qcs00003DetailsList(string Token, string GroupID)
        {
            DataLogerService.writeURL(Token, "GroupID:" + GroupID);
            return QMBussinessService.Qcs00003DetailsList(Token, GroupID);
        }


        /// <summary>
        /// 根据检验群组码获取不属于他的料品列表
        /// SAM 2017年5月25日14:23:48
        /// 
        /// SAM 2017年8月29日09:46:54
        /// 添加分页
        /// </summary>
        /// <param name="Token"></param>
        /// <param name="GroupID"></param>
        /// <param name="StartCode"></param>
        /// <param name="EndCode"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <returns></returns>
        [HttpGet]
        [Authenticate]
        public object Qcs00003ItemList(string Token, string GroupID, string StartCode = null, string EndCode = null, int page = 1, int rows = 10)
        {
            DataLogerService.writeURL(Token, "GroupID:" + GroupID);
            return QMBussinessService.Qcs00003ItemList(Token, GroupID, StartCode, EndCode, page, rows);
        }

        /// <summary>
        /// 保存检验群组码的明细
        /// SAM 2017年5月25日14:39:38
        /// </summary>
        /// <param name="request">JSON数据</param>
        /// <returns></returns>
        [HttpPost]
        [Authenticate]
        public object Qcs00003DetailsSave(JObject request)
        {
            DataLogerService.writeURL(request.Value<string>("Token"), request.ToString());
            UtilBussinessService.WriteLog(UtilBussinessService.detoken(request.Value<string>("Token")), "检验群组码明细", null, "保存", request.ToString());
            return QMBussinessService.Qcs00003DetailsSave(request);
        }

        /// <summary>
        /// 获取所有的未分配检验群组码的料品列表
        /// SAM 2017年10月18日10:52:15
        /// </summary>
        /// <param name="Token"></param>
        /// <param name="StartCode"></param>
        /// <param name="EndCode"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <returns></returns>
        [HttpGet]
        [Authenticate]
        public object Qcs00003ItemListV2(string Token, string StartCode = null, string EndCode = null, int page = 1, int rows = 10)
        {
            DataLogerService.writeURL(Token, "StartCode:" + StartCode + "EndCode:" + EndCode);
            return QMBussinessService.Qcs00003ItemListV2(Token, StartCode, EndCode, page, rows);
        }

        /// <summary>
        /// 根据检验群组码获取他的料品设定（不分页）
        /// SAM 2017年5月25日14:17:04
        /// </summary>
        /// <param name="Token">授权码</param>
        /// <param name="GroupID"></param>
        /// <returns></returns>
        [HttpGet]
        [Authenticate]
        public object Qcs00003DetailsListV2(string Token, string GroupID)
        {
            DataLogerService.writeURL(Token, "GroupID:" + GroupID);
            return QMBussinessService.Qcs00003DetailsListV2(Token, GroupID);
        }

        /// <summary>
        /// 保存检验群组码的明细
        /// Sam 2017年10月18日11:17:37 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Authenticate]
        public object Qcs00003DetailsSaveV2(JObject request)
        {
            DataLogerService.writeURL(request.Value<string>("Token"), request.ToString());
            UtilBussinessService.WriteLog(UtilBussinessService.detoken(request.Value<string>("Token")), "检验群组码明细", null, "保存", request.ToString());
            return QMBussinessService.Qcs00003DetailsSaveV2(request);
        }
        #endregion

        #region QCS00004标准检验规范设定
        /// <summary>
        /// 标准检验规范设定-料品页签列表
        /// SAM 2017年6月15日17:08:55
        /// </summary>
        /// <param name="Token"></param>
        /// <param name="Code"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <returns></returns>
        [HttpGet]
        [Authenticate]
        public object Qcs00004GetItemList(string Token, string Code = null, int page = 1, int rows = 10)
        {
            DataLogerService.writeURL(Token, "Code:" + Code);
            UtilBussinessService.WriteLog(UtilBussinessService.detoken(Token), "标准检验规范设定", null, "查看", "Code:" + Code);
            return QMBussinessService.Qcs00004GetItemList(Token, Code, page, rows);
        }

        /// <summary>
        /// 标准检验规范设定-检验群码页签列表
        /// SAM 2017年6月15日17:35:13
        /// TODO
        /// </summary>
        /// <param name="Token"></param>
        /// <param name="Code"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <returns></returns>
        [HttpGet]
        [Authenticate]
        public object Qcs00004GetGroupItemList(string Token, string Code = null, int page = 1, int rows = 10)
        {
            DataLogerService.writeURL(Token, "Code:" + Code);
            UtilBussinessService.WriteLog(UtilBussinessService.detoken(Token), "标准检验规范设定", null, "查看", "Code:" + Code);
            return QMBussinessService.Qcs00004GetGroupList(Token, Code, page, rows);
        }

        /// <summary>
        ///  Qcs00004弹窗表头列表
        ///  SAM 2017年6月16日10:16:10
        /// </summary>
        /// <param name="Token"></param>
        /// <param name="PartID"></param>
        /// <param name="SettingType"></param>
        /// <param name="InspectionType"></param>
        /// <param name="Code"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <returns></returns>
        [HttpGet]
        [Authenticate]
        public object Qcs00004GetHeaderList(string Token, string PartID, string SettingType, string InspectionType, string Code = null, int page = 1, int rows = 10)
        {
            DataLogerService.writeURL(Token, "PartID:" + PartID);
            UtilBussinessService.WriteLog(UtilBussinessService.detoken(Token), "标准检验规范设定", null, "查看", "ItemID:" + PartID);
            return QMBussinessService.Qcs00004GetHeaderList(Token, PartID, SettingType, InspectionType, Code, page, rows);
        }

        /// <summary>
        /// 检验群码专属表头列表
        /// Sam 2017年10月19日11:54:03
        /// </summary>
        /// <param name="Token"></param>
        /// <param name="PartID"></param>
        /// <param name="InspectionType"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <returns></returns>
        [HttpGet]
        [Authenticate]
        public object Qcs00004GroupGetHeaderList(string Token, string PartID, string InspectionType,  int page = 1, int rows = 10)
        {
            DataLogerService.writeURL(Token, "PartID:" + PartID);
            UtilBussinessService.WriteLog(UtilBussinessService.detoken(Token), "标准检验规范设定-检验群码页签", null, "查看", "PartID:" + PartID);
            return QMBussinessService.Qcs00004GroupGetHeaderList(Token, PartID, InspectionType, page, rows);
        }

        /// <summary>
        /// 检验群码专属表头保存
        /// Sam 2017年10月19日15:29:59
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public object Qcs00004GroupHeaderSave(JObject request)
        {
            DataLogerService.writeURL(request.Value<string>("Token"), request.ToString());
            UtilBussinessService.WriteLog(UtilBussinessService.detoken(request.Value<string>("Token")), "标准检验规范设定-检验群码页签", null, "保存", request.ToString());

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
                Update = QMBussinessService.Qcs00004GroupHeaderupdate(request.Value<string>("Token"), request.Value<JArray>("updated"));

            return new { inserted = Insert, updated = Update, deleted = deleted };
        }


        /// <summary>
        /// 弹窗表头保存
        /// SAM 2017年7月6日10:53:16
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Authenticate]
        public object Qcs00004HeaderSave(JObject request)
        {
            DataLogerService.writeURL(request.Value<string>("Token"), request.ToString());
            UtilBussinessService.WriteLog(UtilBussinessService.detoken(request.Value<string>("Token")), "标准检验规范设定", null, "保存", request.ToString());

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
                Update = QMBussinessService.Qcs00004Headerupdate(request.Value<string>("Token"), request.Value<JArray>("updated"));

            return new { inserted = Insert, updated = Update, deleted = deleted };
        }

        /// <summary>
        /// QCS弹窗明细列表
        /// SAM 2017年7月6日11:39:42
        /// </summary>
        /// <param name="Token"></param>
        /// <param name="SettingType"></param>
        /// <param name="PartID"></param>
        /// <param name="InspectionType"></param>
        /// <param name="ProcessID"></param>
        /// <param name="OperationID"></param>
        /// <param name="Code"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <returns></returns>
        [HttpGet]
        [Authenticate]
        public object Qcs00004GetDetailsList(string Token, string SettingType, string PartID, string InspectionType, string ProcessID, string OperationID = null, string Code = null, int page = 1, int rows = 10)
        {
            DataLogerService.writeURL(Token, "Code:" + Code);
            UtilBussinessService.WriteLog(UtilBussinessService.detoken(Token), "标准检验规范设定", null, "查看", "Code:" + Code);
            return QMBussinessService.Qcs00004GetDetailsList(Token, SettingType, PartID, InspectionType, ProcessID, OperationID, Code, page, rows);
        }

        /// <summary>
        /// 明细的保存
        /// SAM 2017年7月6日11:39:49
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Authenticate]
        public object Qcs00004DetailSave(JObject request)
        {
            DataLogerService.writeURL(request.Value<string>("Token"), request.ToString());
            UtilBussinessService.WriteLog(UtilBussinessService.detoken(request.Value<string>("Token")), "标准检验规范设定", null, "保存", request.ToString());
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
                deleted = QMBussinessService.Qcs00004Detaildelete(request.Value<string>("Token"), request.Value<JArray>("deleted"));
            if (isInsert)
                Insert = QMBussinessService.Qcs00004Detailinsert(request.Value<string>("Token"), request.Value<JArray>("inserted"));
            if (isUpdate)
                Update = QMBussinessService.Qcs00004Detailupdate(request.Value<string>("Token"), request.Value<JArray>("updated"));

            return new { inserted = Insert, updated = Update, deleted = deleted };
        }

        /// <summary>
        /// Qcs00004明细的单条删除
        /// Mouse 2017年10月11日15:02:59
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Authenticate]
        public object Qcs00004DetailDelete(JObject request)
        {
            DataLogerService.writeURL(request.Value<string>("Token"), request.ToString());
            UtilBussinessService.WriteLog(UtilBussinessService.detoken(request.Value<string>("Token")), "标准检验规范设定", null, "删除", request.ToString());
            return QMBussinessService.Qcs00004DetailDelete(request);
        }

        #endregion

        #region QCS00005制程检验维护
        ///<summary>
        ///制程检验维护列表
        ///Joint 2017年7月3日16:52:20
        ///</summary>
        ///<param name="Token">授权码</param>
        ///<param name="InspectionNo">检验单号</param>
        ///<param name="Status">状态</param>
        ///<param name="page">页码</param>
        ///<param name="rows">行数</param>
        [HttpGet]
        [Authenticate]
        public object Qcs00005GetList(string Token, string InspectionNo = null, string Status = null, int page = 1, int rows = 10)
        {
            DataLogerService.writeURL(Token, "InspectionNo:" + InspectionNo);
            UtilBussinessService.WriteLog(UtilBussinessService.detoken(Token), "制程检验单", null, "查看", "InspectionNo:" + InspectionNo);
            return QMBussinessService.Qcs00005GetList(Token, InspectionNo, Status, page, rows);
        }

        /// <summary>
        /// QCS05获取单据预设子元列表
        /// SAM 2017年7月30日21:28:10
        /// </summary>
        /// <param name="Token"></param>
        /// <returns></returns>
        [HttpGet]
        [Authenticate]
        public object Qcs00005GetTypeList(string Token)
        {
            DataLogerService.writeURL(Token, null);
            return UtilBussinessService.GetTypeList(UtilBussinessService.detoken(Token), Framework.SystemID + "020121300003B");
        }

        /// <summary>
        /// QCS05获取单据编号
        /// Joint 2017年8月1日10:10:33
        /// </summary>
        /// <param name="Token"></param>
        /// <param name="Value"></param>
        /// <param name="Date"></param>
        /// <returns></returns>
        [HttpGet]
        [Authenticate]
        public object Qcs00005GetAutoNumber(string Token, string Value, string Date)
        {
            DataLogerService.writeURL(Token, "Value:" + Value);
            return UtilBussinessService.GetDocumentAutoNumber(UtilBussinessService.detoken(Token), Value, Date);
        }

        /// <summary>
        /// 制程检验表头新增
        /// Joint
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Authenticate]
        public object Qcs00005Add(JObject request)
        {
            DataLogerService.writeURL(request.Value<string>("Token"), request.ToString());
            UtilBussinessService.WriteLog(UtilBussinessService.detoken(request.Value<string>("Token")), "制程检验维护", null, "新增", request.ToString());
            return QMBussinessService.Qcs00005Add(request);
        }

        /// <summary>
        /// 制程检验单新增
        /// SAM 2017年10月19日11:44:28
        /// 调整了检验单明细的获取逻辑，优先判断群码再料品
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Authenticate]
        public object Qcs00005AddV1(JObject request)
        {
            DataLogerService.writeURL(request.Value<string>("Token"), request.ToString());
            UtilBussinessService.WriteLog(UtilBussinessService.detoken(request.Value<string>("Token")), "制程检验维护", null, "新增", request.ToString());
            return QMBussinessService.Qcs00005AddV1(request);
        }

        /// <summary>
        /// 新增制程检验单V2版本
        /// Sam 2017年10月23日17:34:48
        /// 在V1的版本上调整了部分逻辑。在寻找抽样检验设定资料时，加多了分派量是否在范围内的判断
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Authenticate]
        public object Qcs00005AddV2(JObject request)
        {
            DataLogerService.writeURL(request.Value<string>("Token"), request.ToString());
            UtilBussinessService.WriteLog(UtilBussinessService.detoken(request.Value<string>("Token")), "制程检验维护", null, "新增", request.ToString());
            return QMBussinessService.Qcs00005AddV2(request);
        }

        /// <summary>
        /// 新增制程检验单V3版本
        /// Sam 2017年10月23日17:34:48
        /// 在V2的版本上调整了部分代码，以及修改了一个bug:明细的获取错误。
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Authenticate]
        public object Qcs00005AddV3(JObject request)
        {
            DataLogerService.writeURL(request.Value<string>("Token"), request.ToString());
            UtilBussinessService.WriteLog(UtilBussinessService.detoken(request.Value<string>("Token")), "制程检验维护", null, "新增", request.ToString());
            return QMBussinessService.Qcs00005AddV3(request);
        }


        /// <summary>
        /// 制程检验表头修改
        /// Alvin  2017年9月6日14:58:19
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Authenticate]
        public object Qcs00005UpdateByOne(JObject request)
        {
            DataLogerService.writeURL(request.Value<string>("Token"), request.ToString());
            UtilBussinessService.WriteLog(UtilBussinessService.detoken(request.Value<string>("Token")), "制程检验维护", null, "修改", request.ToString());
            return QMBussinessService.Qcs00005UpdateByOne(request);
        }

        /// <summary>
        /// 制程检验表头保存
        /// Joint
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Authenticate]
        public object Qcs00005Save(JObject request)
        {
            DataLogerService.writeURL(request.Value<string>("Token"), request.ToString());
            UtilBussinessService.WriteLog(UtilBussinessService.detoken(request.Value<string>("Token")), "制程检验维护", null, "保存", request.ToString());
            bool isUpdate = false;
            object Update = null;
            foreach (JProperty i in request.Children())
            {
                if (i.Name != "Token" && ((JArray)i.Value).Count > 0)
                {
                    switch (i.Name)
                    {
                        case "updated": isUpdate = true;
                            break;
                    }
                }
            }
            if (isUpdate)
                Update = QMBussinessService.Qcs00005update(request.Value<string>("Token"), request.Value<JArray>("updated"));

            return new { updated = Update };
        }

        /// <summary>
        /// 制程检验明细资料
        /// Joint
        /// </summary>
        /// <param name="Token"></param>
        /// <param name="InspectionDocumentID"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        [HttpGet]
        [Authenticate]
        public object Qcs00005GetDetailsList(string Token, string InspectionDocumentID = null, int page = 1, int rows = 10)
        {
            DataLogerService.writeURL(Token, "InspectionDocumentID:" + InspectionDocumentID);
            UtilBussinessService.WriteLog(UtilBussinessService.detoken(Token), "制程检验单明细", null, "查看", "InspectionDocumentID:" + InspectionDocumentID);
            return QMBussinessService.Qcs00005GetDetailsList(Token, InspectionDocumentID, page, rows);
        }

        /// <summary>
        /// 制程检验明细-保存
        /// Joint
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Authenticate]
        public object Qcs00005DetailSave(JObject request)
        {
            DataLogerService.writeURL(request.Value<string>("Token"), request.ToString());
            UtilBussinessService.WriteLog(UtilBussinessService.detoken(request.Value<string>("Token")), "制程检验单", null, "保存", request.ToString());
            bool isUpdate = false;


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
                Update = QMBussinessService.Qcs00005DetailUpdate(request.Value<string>("Token"), request.Value<JArray>("updated"));

            return new { updated = Update};
        }

        /// <summary>
        /// 制程检验明细-单条更新（移动端）
        /// Alvin 2017年9月11日15:11:02
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Authenticate]
        public object Qcs00005DetailUpdateByOne(JObject request)
        {
            DataLogerService.writeURL(request.Value<string>("Token"), request.ToString());
            UtilBussinessService.WriteLog(UtilBussinessService.detoken(request.Value<string>("Token")), "制程检验单", null, "更新", request.ToString());
            return QMBussinessService.Qcs00005DetailUpdateByOne(request);
        }

        /// <summary>
        /// 制程检验明细原因码资料
        /// Joint
        /// </summary>
        /// <param name="Token"></param>
        /// <param name="InspectionDocumentID"></param>
        /// <param name="InspectionDocumentDetailID"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <returns></returns>
        [HttpGet]
        [Authenticate]
        public object Qcs00005GetReasonList(string Token, string InspectionDocumentID = null, string InspectionDocumentDetailID = null, int page = 1, int rows = 10)
        {
            DataLogerService.writeURL(Token, "InspectionDocumentID:" + InspectionDocumentID);
            UtilBussinessService.WriteLog(UtilBussinessService.detoken(Token), "制程检验单明细原因码", null, "查看", "InspectionDocumentID:" + InspectionDocumentID);
            return QMBussinessService.Qcs00005GetReasonList(Token, InspectionDocumentID, InspectionDocumentDetailID, page, rows);
        }

        /// <summary>
        /// 原因码保存
        /// Joint
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Authenticate]
        public object Qcs00005ReasonSave(JObject request)
        {
            DataLogerService.writeURL(request.Value<string>("Token"), request.ToString());
            UtilBussinessService.WriteLog(UtilBussinessService.detoken(request.Value<string>("Token")), "制程检验单原因码", null, "保存", request.ToString());
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
                deleted = QMBussinessService.Qcs00005ReasonDelete(request.Value<string>("Token"), (JArray)request.Value<JArray>("deleted"));
            if (isInsert)
                Insert = QMBussinessService.Qcs00005ReasonInsert(request.Value<string>("Token"), (JArray)request.Value<JArray>("inserted"));
            if (isUpdate)
                Update = QMBussinessService.Qcs00005ReasonUpdate(request.Value<string>("Token"), (JArray)request.Value<JArray>("updated"));

            return new { inserted = Insert, updated = Update, deleted = deleted };
        }


        /// <summary>
        /// 原因码新增
        /// Alvin  2017年9月11日16:31:13
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Authenticate]
        public object Qcs00005ReasonAddByOne(JObject request)
        {
            DataLogerService.writeURL(request.Value<string>("Token"), request.ToString());
            UtilBussinessService.WriteLog(UtilBussinessService.detoken(request.Value<string>("Token")), "制程检验单原因码", null, "新增", request.ToString());
            return QMBussinessService.Qcs00005ReasonAddByOne(request);
        }

        /// <summary>
        /// 原因码修改
        /// Alvin  2017年9月11日16:31:13
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Authenticate]
        public object Qcs00005ReasonEditByOne(JObject request)
        {
            DataLogerService.writeURL(request.Value<string>("Token"), request.ToString());
            UtilBussinessService.WriteLog(UtilBussinessService.detoken(request.Value<string>("Token")), "制程检验单原因码", null, "修改", request.ToString());
            return QMBussinessService.Qcs00005ReasonEditByOne(request);
        }

        /// <summary>
        /// 原因码删除
        /// Alvin  2017年9月11日16:31:13
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Authenticate]
        public object Qcs00005ReasonDeleteByOne(JObject request)
        {
            DataLogerService.writeURL(request.Value<string>("Token"), request.ToString());
            UtilBussinessService.WriteLog(UtilBussinessService.detoken(request.Value<string>("Token")), "制程检验单原因码", null, "删除", request.ToString());
            return QMBussinessService.Qcs00005ReasonDeleteByOne(request);
        }

        /// <summary>
        /// 制程检验明细-检验结果说明
        /// Joint
        /// </summary>
        /// <param name="Token"></param>
        /// <param name="InspectionDocumentID"></param>
        /// <param name="InspectionDocumentDetailID"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        [HttpGet]
        [Authenticate]
        public object Qcs00005GetRemarkList(string Token, string InspectionDocumentID = null, string InspectionDocumentDetailID = null, int page = 1, int rows = 10)
        {
            DataLogerService.writeURL(Token, "InspectionDocumentID:" + InspectionDocumentID);
            UtilBussinessService.WriteLog(UtilBussinessService.detoken(Token), "制程检验单明细检验结果说明", null, "查看", "InspectionDocumentID:" + InspectionDocumentID);
            return QMBussinessService.Qcs00005GetRemarkList(Token, InspectionDocumentID, InspectionDocumentDetailID, page, rows);
        }

        /// <summary>
        /// 检验结果说明保存
        /// Joint
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Authenticate]
        public object Qcs00005RemarkSave(JObject request)
        {
            DataLogerService.writeURL(request.Value<string>("Token"), request.ToString());
            UtilBussinessService.WriteLog(UtilBussinessService.detoken(request.Value<string>("Token")), "制程检验单检明细验结果说明", null, "保存", request.ToString());
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
                deleted = QMBussinessService.Qcs00005RemarkDelete(request.Value<string>("Token"), (JArray)request.Value<JArray>("deleted"));
            if (isInsert)
                Insert = QMBussinessService.Qcs00005RemarkInsert(request.Value<string>("Token"), (JArray)request.Value<JArray>("inserted"));
            if (isUpdate)
                Update = QMBussinessService.Qcs00005RemarkUpdate(request.Value<string>("Token"), (JArray)request.Value<JArray>("updated"));

            return new { inserted = Insert, updated = Update, deleted = deleted };
        }


        /// <summary>
        /// 检验结果说明新增(移动端)
        /// Alvin 2017年9月11日17:07:53
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Authenticate]
        public object Qcs00005RemarkAddByOne(JObject request)
        {
            DataLogerService.writeURL(request.Value<string>("Token"), request.ToString());
            UtilBussinessService.WriteLog(UtilBussinessService.detoken(request.Value<string>("Token")), "制程检验单检明细验结果说明", null, "新增", request.ToString());
            return QMBussinessService.Qcs00005RemarkAddByOne(request);

        }

        /// <summary>
        /// 检验结果说明修改(移动端)
        /// Alvin 2017年9月11日17:07:53
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Authenticate]
        public object Qcs00005RemarkEditByOne(JObject request)
        {
            DataLogerService.writeURL(request.Value<string>("Token"), request.ToString());
            UtilBussinessService.WriteLog(UtilBussinessService.detoken(request.Value<string>("Token")), "制程检验单检明细验结果说明", null, "修改", request.ToString());
            return QMBussinessService.Qcs00005RemarkEditByOne(request);

        }


        /// <summary>
        /// 检验结果说明删除(移动端)
        /// Alvin 2017年9月11日17:07:53
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Authenticate] 
        public object Qcs00005RemarkDeleteByOne(JObject request)
        {
            DataLogerService.writeURL(request.Value<string>("Token"), request.ToString());
            UtilBussinessService.WriteLog(UtilBussinessService.detoken(request.Value<string>("Token")), "制程检验单检明细验结果说明", null, "删除", request.ToString());
            return QMBussinessService.Qcs00005RemarkDeleteByOne(request);
        }

        #endregion

        #region QCS00006制程检验确认
        /// <summary>
        /// 制程检验确认-主查詢
        /// SAM 2017年7月9日20:12:27
        /// </summary>
        /// <param name="Token"></param>
        /// <param name="InspectionType"></param>
        /// <param name="UserID"></param>
        /// <param name="SDate"></param>
        /// <param name="EDate"></param>
        /// <param name="StartInspectionNo"></param>
        /// <param name="EndInspectionNo"></param>
        /// <param name="StartRCNo"></param>
        /// <param name="EndRCNo"></param>
        /// <param name="StartPart"></param>
        /// <param name="EndPart"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <returns></returns>
        [HttpGet]
        [Authenticate]
        public object Qcs00006GetList(string Token, string InspectionType = null, string UserID = null,
            string SDate = null, string EDate = null, string StartInspectionNo = null, string EndInspectionNo = null,
            string StartRCNo = null, string EndRCNo = null, string StartPart = null, string EndPart = null,
             int page = 1, int rows = 10)
        {
            DataLogerService.writeURL(Token, null);
            UtilBussinessService.WriteLog(UtilBussinessService.detoken(Token), "制程检验确认", null, "查看", null);
            return QMBussinessService.Qcs00006GetList(Token, InspectionType, UserID, SDate, EDate, StartInspectionNo, EndInspectionNo,
            StartRCNo, EndRCNo, StartPart, EndPart, page, rows);
        }

        /// <summary>
        ///  制程检验确认-檢驗明細
        ///  SAM 2017年7月9日21:20:54
        /// </summary>
        /// <param name="Token"></param>
        /// <param name="InspectionDocumentID"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <returns></returns>
        [HttpGet]
        [Authenticate]
        public object QCS00006GetDetailList(string Token, string InspectionDocumentID, int page = 1, int rows = 10)
        {
            DataLogerService.writeURL(Token, null);
            UtilBussinessService.WriteLog(UtilBussinessService.detoken(Token), "制程检验确认-檢驗明細", null, "查看", null);
            return QMBussinessService.QCS00006GetDetailList(Token, InspectionDocumentID, page, rows);
        }

        /// <summary>
        /// 檢驗明細-不良原因
        /// SAM 2017年7月9日21:38:18
        /// </summary>
        /// <param name="Token"></param>
        /// <param name="InspectionDocumentDetailID"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <returns></returns>
        [HttpGet]
        [Authenticate]
        public object QCS00006DetailReason(string Token, string InspectionDocumentDetailID, int page = 1, int rows = 10)
        {
            DataLogerService.writeURL(Token, null);
            UtilBussinessService.WriteLog(UtilBussinessService.detoken(Token), "檢驗明細-不良原因", null, "查看", null);
            return QMBussinessService.QCS00006DetailReason(Token, InspectionDocumentDetailID, page, rows);
        }

        /// <summary>
        /// 檢驗明細-檢驗結果
        /// SAM 2017年7月9日21:40:00
        /// </summary>
        /// <param name="Token"></param>
        /// <param name="InspectionDocumentDetailID"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <returns></returns>
        [HttpGet]
        [Authenticate]
        public object QCS00006DetailResult(string Token, string InspectionDocumentDetailID, int page = 1, int rows = 10)
        {
            DataLogerService.writeURL(Token, null);
            UtilBussinessService.WriteLog(UtilBussinessService.detoken(Token), "檢驗明細-檢驗結果", null, "查看", null);
            return QMBussinessService.QCS00006DetailResult(Token, InspectionDocumentDetailID, page, rows);
        }

        /// <summary>
        /// 制程检验确认-檢驗單確認
        /// SAM 2017年7月9日21:46:30
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Authenticate]
        public object QCS00006Confirm(JObject request)
        {
            DataLogerService.writeURL(request.Value<string>("Token"), request.ToString());
            UtilBussinessService.WriteLog(UtilBussinessService.detoken(request.Value<string>("Token")), "制程检验确认-檢驗單確認", null, "確認", request.ToString());
            return QMBussinessService.QCS00006Confirm(request);
        }
        /// <summary>
        /// 制程检验确认-产生已确认完工调整单
        /// Mouse 2017年10月11日18:03:07
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Authenticate]
        public object QCS00006ConfirmV710(JObject request)
        {
            DataLogerService.writeURL(request.Value<string>("Token"), request.ToString());
            UtilBussinessService.WriteLog(UtilBussinessService.detoken(request.Value<string>("Token")), "制程检验确认-檢驗單確認", null, "確認", request.ToString());
            return QMBussinessService.QCS00006ConfirmV710(request);
        }

        /// <summary>
        /// 制程检验确认-檢驗單作廢
        /// SAM 2017年7月9日21:56:24
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Authenticate]
        public object QCS00006Cancel(JObject request)
        {
            DataLogerService.writeURL(request.Value<string>("Token"), request.ToString());
            UtilBussinessService.WriteLog(UtilBussinessService.detoken(request.Value<string>("Token")), "制程检验确认-檢驗單作廢", null, "作廢", request.ToString());
            return QMBussinessService.QCS00006Cancel(request);
        }

        #endregion

        #region QCS00007制程首件检验维护
        /// <summary>
        /// 制程首件检验维护列表
        /// Joint 
        /// </summary>
        /// <param name="Token"></param>
        /// <param name="InspectionNo"></param>
        /// <param name="Status"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        [HttpGet]
        [Authenticate]
        public object Qcs00007GetList(string Token, string InspectionNo = null, string Status = null, int page = 1, int rows = 10)
        {
            DataLogerService.writeURL(Token, "InspectionNo:" + InspectionNo);
            UtilBussinessService.WriteLog(UtilBussinessService.detoken(Token), "制程检验单", null, "查看", "InspectionNo:" + InspectionNo);
            return QMBussinessService.Qcs00007GetList(Token, InspectionNo, Status, page, rows);
        }

        /// <summary>
        /// QCS07获取单据预设子元列表
        /// SAM 2017年7月30日21:28:10
        /// </summary>
        /// <param name="Token"></param>
        /// <returns></returns>
        [HttpGet]
        [Authenticate]
        public object Qcs00007GetTypeList(string Token)
        {
            DataLogerService.writeURL(Token, null);

            return UtilBussinessService.GetTypeList(UtilBussinessService.detoken(Token), Framework.SystemID + "020121300003C");
        }

        /// <summary>
        /// Qcs07获取单据编号
        /// Joint 2017年8月1日10:02:06
        /// </summary>
        /// <param name="Token"></param>
        /// <param name="Value">单据类别流水号</param>
        /// <param name="Date">单据日期</param>
        /// <returns></returns>
        [HttpGet]
        [Authenticate]
        public object Qcs00007GetAutoNumber(string Token, string Value, string Date)
        {
            DataLogerService.writeURL(Token, "Value:" + Value);
            return UtilBussinessService.GetDocumentAutoNumber(UtilBussinessService.detoken(Token), Value, Date);
        }

        /// <summary>
        /// 表头新增
        /// Joint
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Authenticate]
        public object Qcs00007Add(JObject request)
        {
            DataLogerService.writeURL(request.Value<string>("Token"), request.ToString());
            UtilBussinessService.WriteLog(UtilBussinessService.detoken(request.Value<string>("Token")), "制程首件检验维护", null, "新增", request.ToString());
            return QMBussinessService.Qcs00007Add(request);
        }

        /// <summary>
        /// 首件单新增
        /// SAM 2017年10月19日11:45:07
        /// 调整了检验单明细的获取逻辑，优先判断群码再料品
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Authenticate]
        public object Qcs00007AddV1(JObject request)
        {
            DataLogerService.writeURL(request.Value<string>("Token"), request.ToString());
            UtilBussinessService.WriteLog(UtilBussinessService.detoken(request.Value<string>("Token")), "制程首件检验维护", null, "新增", request.ToString());
            return QMBussinessService.Qcs00007AddV1(request);
        }

        /// <summary>
        /// 新增制程检验单
        /// SAM 2017年10月23日17:41:23
        ///  在V1的版本上调整了部分逻辑。在寻找抽样检验设定资料时，加多了分派量是否在范围内的判断
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Authenticate]
        public object Qcs00007AddV2(JObject request)
        {
            DataLogerService.writeURL(request.Value<string>("Token"), request.ToString());
            UtilBussinessService.WriteLog(UtilBussinessService.detoken(request.Value<string>("Token")), "制程首件检验维护", null, "新增", request.ToString());
            return QMBussinessService.Qcs00007AddV2(request);
        }

        /// <summary>
        /// 新增制程检验单
        /// SAM 2017年10月25日01:46:12
        /// 在V2的版本上，调整了代码，修正了一些bug.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Authenticate]
        public object Qcs00007AddV3(JObject request)
        {
            DataLogerService.writeURL(request.Value<string>("Token"), request.ToString());
            UtilBussinessService.WriteLog(UtilBussinessService.detoken(request.Value<string>("Token")), "制程首件检验维护", null, "新增", request.ToString());
            return QMBussinessService.Qcs00007AddV3(request);
        }

        /// <summary>
        /// 表头保存
        /// Joint
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Authenticate]
        public object Qcs00007Save(JObject request)
        {
            DataLogerService.writeURL(request.Value<string>("Token"), request.ToString());
            UtilBussinessService.WriteLog(UtilBussinessService.detoken(request.Value<string>("Token")), "制程首件检验维护", null, "保存", request.ToString());
            bool isUpdate = false;
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
                Update = QMBussinessService.Qcs00007update(request.Value<string>("Token"), request.Value<JArray>("updated"));

            return new { updated = Update };
        }

        /// <summary>
        /// 单条数据更新--安卓
        /// Alvin  2017年9月11日14:13:51
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Authenticate]
        public object Qcs00007UpdateByOne(JObject request)
        {
            DataLogerService.writeURL(request.Value<string>("Token"), request.ToString());
            UtilBussinessService.WriteLog(UtilBussinessService.detoken(request.Value<string>("Token")), "制程首件检验维护", null, "更新", request.ToString());
            return QMBussinessService.Qcs00007UpdateByOne(request);           
        }


        /// <summary>
        /// 检验明细列表
        /// Joint
        /// </summary>
        /// <param name="Token"></param>
        /// <param name="InspectionDocumentID"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        [HttpGet]
        [Authenticate]
        public object Qcs00007GetDetailsList(string Token, string InspectionDocumentID = null, int page = 1, int rows = 10)
        {
            DataLogerService.writeURL(Token, "InspectionDocumentID:" + InspectionDocumentID);
            UtilBussinessService.WriteLog(UtilBussinessService.detoken(Token), "制程检验单明细", null, "查看", "InspectionDocumentID:" + InspectionDocumentID);
            return QMBussinessService.Qcs00007GetDetailsList(Token, InspectionDocumentID, page, rows);
        }


        /// <summary>
        /// 检验明细保存
        /// Joint
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Authenticate]
        public object Qcs00007DetailsListSave(JObject request)
        {
            DataLogerService.writeURL(request.Value<string>("Token"), request.ToString());
            UtilBussinessService.WriteLog(UtilBussinessService.detoken(request.Value<string>("Token")), "制程检验单", null, "保存", request.ToString());

            bool isUpdate = false;


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
                Update = QMBussinessService.Qcs00007DetailUpdate(request.Value<string>("Token"), request.Value<JArray>("updated"));

            return new { updated = Update, inserted = Insert };
        }


        /// <summary>
        /// 制程检验明细-单条更新（移动端）
        /// Alvin 2017年9月11日15:11:02
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Authenticate]
        public object Qcs00007DetailUpdateByOne(JObject request)
        {
            DataLogerService.writeURL(request.Value<string>("Token"), request.ToString());
            UtilBussinessService.WriteLog(UtilBussinessService.detoken(request.Value<string>("Token")), "制程首件检验维护", null, "更新", request.ToString());
            return QMBussinessService.Qcs00007DetailUpdateByOne(request);
        }

        /// <summary>
        /// 制程检验明细原因码资料
        /// Joint
        /// </summary>
        /// <param name="Token"></param>
        /// <param name="InspectionDocumentID"></param>
        /// <param name="InspectionDocumentDetailID"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <returns></returns>
        [HttpGet]
        [Authenticate]
        public object Qcs00007GetReasonList(string Token, string InspectionDocumentID = null, string InspectionDocumentDetailID = null, int page = 1, int rows = 10)
        {
            DataLogerService.writeURL(Token, "InspectionDocumentID:" + InspectionDocumentID);
            UtilBussinessService.WriteLog(UtilBussinessService.detoken(Token), "制程检验单明细原因码", null, "查看", "InspectionDocumentID:" + InspectionDocumentID);
            return QMBussinessService.Qcs00007GetReasonList(Token, InspectionDocumentID, InspectionDocumentDetailID, page, rows);
        }

        /// <summary>
        /// 原因码保存
        /// Joint
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Authenticate]
        public object Qcs00007ReasonSave(JObject request)
        {
            DataLogerService.writeURL(request.Value<string>("Token"), request.ToString());
            UtilBussinessService.WriteLog(UtilBussinessService.detoken(request.Value<string>("Token")), "制程检验单原因码", null, "保存", request.ToString());
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
                deleted = QMBussinessService.Qcs00007ReasonDelete(request.Value<string>("Token"), (JArray)request.Value<JArray>("deleted"));
            if (isInsert)
                Insert = QMBussinessService.Qcs00007ReasonInsert(request.Value<string>("Token"), (JArray)request.Value<JArray>("inserted"));
            if (isUpdate)
                Update = QMBussinessService.Qcs00007ReasonUpdate(request.Value<string>("Token"), (JArray)request.Value<JArray>("updated"));

            return new { inserted = Insert, updated = Update, deleted = deleted };
        }

        /// <summary>
        /// Qcs00007原因码新增
        /// Alvin  2017年9月12日15:01:36
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Authenticate]
        public object Qcs00007ReasonAddByOne(JObject request)
        {
            DataLogerService.writeURL(request.Value<string>("Token"), request.ToString());
            UtilBussinessService.WriteLog(UtilBussinessService.detoken(request.Value<string>("Token")), "制程首件检验维护原因码", null, "新增", request.ToString());
            return QMBussinessService.Qcs00007ReasonAddByOne(request);
        }

        /// <summary>
        /// Qcs00007原因码修改
        /// Alvin  2017年9月12日15:01:41
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Authenticate]
        public object Qcs00007ReasonEditByOne(JObject request)
        {
            DataLogerService.writeURL(request.Value<string>("Token"), request.ToString());
            UtilBussinessService.WriteLog(UtilBussinessService.detoken(request.Value<string>("Token")), "制程首件检验维护原因码", null, "修改", request.ToString());
            return QMBussinessService.Qcs00007ReasonEditByOne(request);
        }

        /// <summary>
        /// Qcs00007原因码删除
        /// Alvin  2017年9月12日15:01:45
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Authenticate]
        public object Qcs00007ReasonDeleteByOne(JObject request)
        {
            DataLogerService.writeURL(request.Value<string>("Token"), request.ToString());
            UtilBussinessService.WriteLog(UtilBussinessService.detoken(request.Value<string>("Token")), "制程检验单原因码", null, "删除", request.ToString());
            return QMBussinessService.Qcs00007ReasonDeleteByOne(request);
        }

        /// <summary>
        /// 制程检验明细-检验结果说明
        /// Joint
        /// </summary>
        /// <param name="Token"></param>
        /// <param name="InspectionDocumentID"></param>
        /// <param name="InspectionDocumentDetailID"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        [HttpGet]
        [Authenticate]
        public object Qcs00007GetRemarkList(string Token, string InspectionDocumentID = null, string InspectionDocumentDetailID = null, int page = 1, int rows = 10)
        {
            DataLogerService.writeURL(Token, "InspectionDocumentID:" + InspectionDocumentID);
            UtilBussinessService.WriteLog(UtilBussinessService.detoken(Token), "制程检验单明细检验结果说明", null, "查看", "InspectionDocumentID:" + InspectionDocumentID);
            return QMBussinessService.Qcs00007GetRemarkList(Token, InspectionDocumentID, InspectionDocumentDetailID, page, rows);
        }

        /// <summary>
        /// 检验结果说明保存
        /// Joint
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Authenticate]
        public object Qcs00007RemarkSave(JObject request)
        {
            DataLogerService.writeURL(request.Value<string>("Token"), request.ToString());
            UtilBussinessService.WriteLog(UtilBussinessService.detoken(request.Value<string>("Token")), "制程检验单检明细验结果说明", null, "保存", request.ToString());
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
                deleted = QMBussinessService.Qcs00007ReasonDelete(request.Value<string>("Token"), (JArray)request.Value<JArray>("deleted"));
            if (isInsert)
                Insert = QMBussinessService.Qcs00007ReasonInsert(request.Value<string>("Token"), (JArray)request.Value<JArray>("inserted"));
            if (isUpdate)
                Update = QMBussinessService.Qcs00007ReasonUpdate(request.Value<string>("Token"), (JArray)request.Value<JArray>("updated"));

            return new { inserted = Insert, updated = Update, deleted = deleted };
        }


        /// <summary>
        /// QCS00007检验结果说明新增(移动端)
        /// Alvin 2017年9月12日15:19:21
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Authenticate]
        public object Qcs00007RemarkAddByOne(JObject request)
        {
            DataLogerService.writeURL(request.Value<string>("Token"), request.ToString());
            UtilBussinessService.WriteLog(UtilBussinessService.detoken(request.Value<string>("Token")), "制程首件维护检验明细验结果说明", null, "新增", request.ToString());
            return QMBussinessService.Qcs00007RemarkAddByOne(request);

        }

        /// <summary>
        /// QCS00007检验结果说明修改(移动端)
        /// Alvin 2017年9月12日15:19:25
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Authenticate]
        public object Qcs00007RemarkEditByOne(JObject request)
        {
            DataLogerService.writeURL(request.Value<string>("Token"), request.ToString());
            UtilBussinessService.WriteLog(UtilBussinessService.detoken(request.Value<string>("Token")), "制程首件维护检验明细验结果说明", null, "修改", request.ToString());
            return QMBussinessService.Qcs00007RemarkEditByOne(request);

        }


        /// <summary>
        /// QCS00007检验结果说明删除(移动端)
        /// Alvin 2017年9月12日15:19:29
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Authenticate]
        public object Qcs00007RemarkDeleteByOne(JObject request)
        {
            DataLogerService.writeURL(request.Value<string>("Token"), request.ToString());
            UtilBussinessService.WriteLog(UtilBussinessService.detoken(request.Value<string>("Token")), "制程首件维护检验明细验结果说明", null, "删除", request.ToString());
            return QMBussinessService.Qcs00007RemarkDeleteByOne(request);
        }
        #endregion

        #region QCS00008制程巡检检验维护
        /// <summary>
        /// 制程巡检检验维护列表
        /// Joint
        /// </summary>
        /// <param name="Token"></param>
        /// <param name="InspectionNo"></param>
        /// <param name="Status"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        [HttpGet]
        [Authenticate]
        public object Qcs00008GetList(string Token, string InspectionNo = null, string Status = null, int page = 1, int rows = 10)
        {
            DataLogerService.writeURL(Token, "InspectionNo:" + InspectionNo);
            UtilBussinessService.WriteLog(UtilBussinessService.detoken(Token), "制程检验单", null, "查看", "InspectionNo:" + InspectionNo);
            return QMBussinessService.Qcs00008GetList(Token, InspectionNo, Status, page, rows);
        }

        /// <summary>
        /// QCS08获取单据预设子元列表
        /// SAM 2017年7月30日21:28:10
        /// </summary>
        /// <param name="Token"></param>
        /// <returns></returns>
        [HttpGet]
        [Authenticate]
        public object Qcs00008GetTypeList(string Token)
        {
            DataLogerService.writeURL(Token, null);
            return UtilBussinessService.GetTypeList(UtilBussinessService.detoken(Token), Framework.SystemID + "020121300003D");
        }

        /// <summary>
        /// Qcs08获取单据编号
        /// Joint 2017年8月1日10:15:09
        /// </summary>
        /// <param name="Token"></param>
        /// <param name="Value"></param>
        /// <param name="Date"></param>
        /// <returns></returns>
        [HttpGet]
        [Authenticate]
        public object Qcs00008GetAutoNumber(string Token, string Value, string Date)
        {
            DataLogerService.writeURL(Token, "Value:" + Value);
            return UtilBussinessService.GetDocumentAutoNumber(UtilBussinessService.detoken(Token), Value, Date);
        }

        /// <summary>
        /// 制程检验表头新增
        /// Joint
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Authenticate]
        public object Qcs00008Add(JObject request)
        {
            DataLogerService.writeURL(request.Value<string>("Token"), request.ToString());
            UtilBussinessService.WriteLog(UtilBussinessService.detoken(request.Value<string>("Token")), "制程巡检检验维护", null, "新增", request.ToString());
            return QMBussinessService.Qcs00008Add(request);
        }

        /// <summary>
        /// 新增制程巡检单
        /// SAM 2017年10月19日11:43:12
        /// 调整了检验单明细的获取逻辑，优先判断群码再料品
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Authenticate]
        public object Qcs00008AddV1(JObject request)
        {
            DataLogerService.writeURL(request.Value<string>("Token"), request.ToString());
            UtilBussinessService.WriteLog(UtilBussinessService.detoken(request.Value<string>("Token")), "制程巡检检验维护", null, "新增", request.ToString());
            return QMBussinessService.Qcs00008AddV1(request);
        }

        /// <summary>
        /// 新增制程巡检单
        /// SAM 2017年10月23日17:39:05
        /// 在V1的版本上调整了部分逻辑。在寻找抽样检验设定资料时，加多了分派量是否在范围内的判断
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Authenticate]
        public object Qcs00008AddV2(JObject request)
        {
            DataLogerService.writeURL(request.Value<string>("Token"), request.ToString());
            UtilBussinessService.WriteLog(UtilBussinessService.detoken(request.Value<string>("Token")), "制程巡检检验维护", null, "新增", request.ToString());
            return QMBussinessService.Qcs00008AddV2(request);
        }

        /// <summary>
        /// 新增制程巡检单
        /// SAM 2017年10月23日17:39:05
        /// 在V2的版本上，调整了一些代码，修正了bug
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Authenticate]
        public object Qcs00008AddV3(JObject request)
        {
            DataLogerService.writeURL(request.Value<string>("Token"), request.ToString());
            UtilBussinessService.WriteLog(UtilBussinessService.detoken(request.Value<string>("Token")), "制程巡检检验维护", null, "新增", request.ToString());
            return QMBussinessService.Qcs00008AddV3(request);
        }

        /// <summary>
        /// 制程检验表头保存
        /// Joint
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Authenticate]
        public object Qcs00008Save(JObject request)
        {
            DataLogerService.writeURL(request.Value<string>("Token"), request.ToString());
            UtilBussinessService.WriteLog(UtilBussinessService.detoken(request.Value<string>("Token")), "制程巡检检验维护", null, "保存", request.ToString());
            bool isUpdate = false;
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
                Update = QMBussinessService.Qcs00008update(request.Value<string>("Token"), request.Value<JArray>("updated"));

            return new { updated = Update };

           
        }

        /// <summary>
        /// 制程检验表头修改（移动端）
        /// Alvin 2017年9月12日16:07:38
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Authenticate]
        public object Qcs00008UpdateByOne(JObject request)
        {           
            DataLogerService.writeURL(request.Value<string>("Token"), request.ToString());
            UtilBussinessService.WriteLog(UtilBussinessService.detoken(request.Value<string>("Token")), "制程巡检检验维护", null, "更新", request.ToString());
            return QMBussinessService.Qcs00008UpdateByOne(request);
        }

        /// <summary>
        /// 检验明细列表
        /// Joint
        /// </summary>
        /// <param name="Token"></param>
        /// <param name="InspectionDocumentID"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        [HttpGet]
        [Authenticate]
        public object Qcs00008GetDetailsList(string Token, string InspectionDocumentID = null, int page = 1, int rows = 10)
        {
            DataLogerService.writeURL(Token, "InspectionDocumentID:" + InspectionDocumentID);
            UtilBussinessService.WriteLog(UtilBussinessService.detoken(Token), "制程检验单明细", null, "查看", "InspectionDocumentID:" + InspectionDocumentID);
            return QMBussinessService.Qcs00008GetDetailsList(Token, InspectionDocumentID, page, rows);
        }

        /// <summary>
        /// 检验明细保存
        /// Joint
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Authenticate]
        public object Qcs00008DetailsListSave(JObject request)
        {
            DataLogerService.writeURL(request.Value<string>("Token"), request.ToString());
            UtilBussinessService.WriteLog(UtilBussinessService.detoken(request.Value<string>("Token")), "制程检验单", null, "保存", request.ToString());
            bool isUpdate = false;

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
                Update = QMBussinessService.Qcs00008DetailUpdate(request.Value<string>("Token"), request.Value<JArray>("updated"));

            return new { updated = Update, inserted = Insert };
        }

        /// <summary>
        /// 制程检验明细原因码资料
        /// Joint
        /// </summary>
        /// <param name="Token"></param>
        /// <param name="InspectionDocumentID"></param>
        /// <param name="InspectionDocumentDetailID"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <returns></returns>
        [HttpGet]
        [Authenticate]
        public object Qcs00008GetReasonList(string Token, string InspectionDocumentID = null, string InspectionDocumentDetailID = null, int page = 1, int rows = 10)
        {
            DataLogerService.writeURL(Token, "InspectionDocumentID:" + InspectionDocumentID);
            UtilBussinessService.WriteLog(UtilBussinessService.detoken(Token), "制程检验单明细原因码", null, "查看", "InspectionDocumentID:" + InspectionDocumentID);
            return QMBussinessService.Qcs00008GetReasonList(Token, InspectionDocumentID, InspectionDocumentDetailID, page, rows);
        }

        /// <summary>
        /// 原因码保存
        /// Joint
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Authenticate]
        public object Qcs00008ReasonSave(JObject request)
        {
            DataLogerService.writeURL(request.Value<string>("Token"), request.ToString());
            UtilBussinessService.WriteLog(UtilBussinessService.detoken(request.Value<string>("Token")), "制程检验单原因码", null, "保存", request.ToString());
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
                deleted = QMBussinessService.Qcs00008ReasonDelete(request.Value<string>("Token"), (JArray)request.Value<JArray>("deleted"));
            if (isInsert)
                Insert = QMBussinessService.Qcs00008ReasonInsert(request.Value<string>("Token"), (JArray)request.Value<JArray>("inserted"));
            if (isUpdate)
                Update = QMBussinessService.Qcs00008ReasonUpdate(request.Value<string>("Token"), (JArray)request.Value<JArray>("updated"));

            return new { inserted = Insert, updated = Update, deleted = deleted };
        }

        /// <summary>
        /// 制程检验明细-检验结果说明
        /// Joint
        /// </summary>
        /// <param name="Token"></param>
        /// <param name="InspectionDocumentID"></param>
        /// <param name="InspectionDocumentDetailID"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        [HttpGet]
        [Authenticate]
        public object Qcs00008GetRemarkList(string Token, string InspectionDocumentID = null, string InspectionDocumentDetailID = null, int page = 1, int rows = 10)
        {
            DataLogerService.writeURL(Token, "InspectionDocumentID:" + InspectionDocumentID);
            UtilBussinessService.WriteLog(UtilBussinessService.detoken(Token), "制程检验单明细检验结果说明", null, "查看", "InspectionDocumentID:" + InspectionDocumentID);
            return QMBussinessService.Qcs00008GetRemarkList(Token, InspectionDocumentID, InspectionDocumentDetailID, page, rows);
        }

        /// <summary>
        /// 检验结果说明保存
        /// Joint
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Authenticate]
        public object Qcs00008RemarkSave(JObject request)
        {
            DataLogerService.writeURL(request.Value<string>("Token"), request.ToString());
            UtilBussinessService.WriteLog(UtilBussinessService.detoken(request.Value<string>("Token")), "制程检验单检明细验结果说明", null, "保存", request.ToString());
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
                deleted = QMBussinessService.Qcs00008ReasonDelete(request.Value<string>("Token"), (JArray)request.Value<JArray>("deleted"));
            if (isInsert)
                Insert = QMBussinessService.Qcs00008ReasonInsert(request.Value<string>("Token"), (JArray)request.Value<JArray>("inserted"));
            if (isUpdate)
                Update = QMBussinessService.Qcs00008ReasonUpdate(request.Value<string>("Token"), (JArray)request.Value<JArray>("updated"));

            return new { inserted = Insert, updated = Update, deleted = deleted };
        }



        /// <summary>
        /// 制程巡检维护检验明细-单条更新（移动端）
        /// Alvin 2017年9月14日16:16:24
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Authenticate]
        public object Qcs00008DetailUpdateByOne(JObject request)
        {
            DataLogerService.writeURL(request.Value<string>("Token"), request.ToString());
            UtilBussinessService.WriteLog(UtilBussinessService.detoken(request.Value<string>("Token")), "制程巡检维护检验明细", null, "更新", request.ToString());
            return QMBussinessService.Qcs00008DetailUpdateByOne(request);
        }


        /// <summary>
        /// 制程巡检维护原因码新增
        /// Alvin  2017年9月14日16:16:51
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Authenticate]
        public object Qcs00008ReasonAddByOne(JObject request)
        {
            DataLogerService.writeURL(request.Value<string>("Token"), request.ToString());
            UtilBussinessService.WriteLog(UtilBussinessService.detoken(request.Value<string>("Token")), "制程巡检维护单原因码", null, "新增", request.ToString());
            return QMBussinessService.Qcs00005ReasonAddByOne(request);
        }

        /// <summary>
        /// 制程巡检维护原因码修改
        /// Alvin  2017年9月14日16:17:20
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Authenticate]
        public object Qcs00008ReasonEditByOne(JObject request)
        {
            DataLogerService.writeURL(request.Value<string>("Token"), request.ToString());
            UtilBussinessService.WriteLog(UtilBussinessService.detoken(request.Value<string>("Token")), " 制程巡检维护原因码", null, "修改", request.ToString());
            return QMBussinessService.Qcs00005ReasonEditByOne(request);
        }

        /// <summary>
        /// 制程巡检维护原因码删除
        /// Alvin  2017年9月14日16:17:54
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Authenticate]
        public object Qcs00008ReasonDeleteByOne(JObject request)
        {
            DataLogerService.writeURL(request.Value<string>("Token"), request.ToString());
            UtilBussinessService.WriteLog(UtilBussinessService.detoken(request.Value<string>("Token")), "制程巡检维护原因码", null, "删除", request.ToString());
            return QMBussinessService.Qcs00005ReasonDeleteByOne(request);
        }




        /// <summary>
        /// 制程巡检维护检验结果说明新增(移动端)
        /// Alvin 2017年9月14日16:18:19
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Authenticate]
        public object Qcs00008RemarkAddByOne(JObject request)
        {
            DataLogerService.writeURL(request.Value<string>("Token"), request.ToString());
            UtilBussinessService.WriteLog(UtilBussinessService.detoken(request.Value<string>("Token")), "制程巡检维护明细检验结果说明", null, "新增", request.ToString());
            return QMBussinessService.Qcs00005RemarkAddByOne(request);

        }

        /// <summary>
        /// 制程巡检维护--检验结果说明修改(移动端)
        /// Alvin 2017年9月11日17:07:53
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Authenticate]
        public object Qcs00008RemarkEditByOne(JObject request)
        {
            DataLogerService.writeURL(request.Value<string>("Token"), request.ToString());
            UtilBussinessService.WriteLog(UtilBussinessService.detoken(request.Value<string>("Token")), "制程巡检维护明细验结果说明", null, "修改", request.ToString());
            return QMBussinessService.Qcs00005RemarkEditByOne(request);

        }


        /// <summary>
        /// 制程巡检维护--检验结果说明删除(移动端)
        /// Alvin 2017年9月14日16:19:32
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Authenticate]
        public object Qcs00008RemarkDeleteByOne(JObject request)
        {
            DataLogerService.writeURL(request.Value<string>("Token"), request.ToString());
            UtilBussinessService.WriteLog(UtilBussinessService.detoken(request.Value<string>("Token")), "制程巡检维护结果说明", null, "删除", request.ToString());
            return QMBussinessService.Qcs00005RemarkDeleteByOne(request);
        }
        #endregion

        #region QCS00009客诉单维护
        /// <summary>
        /// 客诉单表头列表
        /// SAM 2017年7月3日11:32:54
        /// </summary>
        /// <param name="Token"></param>
        /// <param name="StartCode"></param>
        /// <param name="EndCode"></param>
        /// <param name="StartDate"></param>
        /// <param name="EndDate"></param>
        /// <param name="StartCustCode"></param>
        /// <param name="EndCustCode"></param>
        /// <param name="Status"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <returns></returns>
        [HttpGet]
        [Authenticate]
        public object Qcs00009GetList(string Token, string StartCode = null, string EndCode = null, string StartDate = null,
          string EndDate = null, string StartCustCode = null, string EndCustCode = null, string Status = null,
          string StartOrderCode = null, string EndOrderCode = null,
            int page = 1, int rows = 10)
        {
            DataLogerService.writeURL(Token, "StartCode:" + StartCode + "EndCode:" + EndCode + "StartDate:" + StartDate + "EndDate:" + EndDate + "StartCustCode:" + StartCustCode + "EndCustCode:" + EndCustCode + "Status:" + Status);
            UtilBussinessService.WriteLog(UtilBussinessService.detoken(Token), "客诉单维护", null, "查看", "StartCode:" + StartCode + "EndCode:" + EndCode + "StartDate:" + StartDate + "EndDate:" + EndDate + "StartCustCode:" + StartCustCode + "EndCustCode:" + EndCustCode + "Status:" + Status);
            return QMBussinessService.Qcs00009GetList(Token, StartCode, EndCode, StartDate, EndDate, StartCustCode, EndCustCode, Status, StartOrderCode, EndOrderCode, page, rows);
        }

        /// <summary>
        /// 单据种别的获取
        /// SAM 2017年8月3日10:02:42
        /// </summary>
        /// <param name="Token"></param>
        /// <param name="MESUserID">申请人流水号</param>
        /// <returns></returns>
        [HttpGet]
        [Authenticate]
        public object Qcs00009GetTypeList(string Token, string MESUserID)
        {
            DataLogerService.writeURL(Token, null);
            return UtilBussinessService.GetTypeList(MESUserID, Framework.SystemID + "020121300003E");
        }

        /// <summary>
        /// 客诉单的新增
        /// SAM 2017年6月15日08:54:42
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Authenticate]
        public object Qcs00009Add(JObject request)
        {
            DataLogerService.writeURL(request.Value<string>("Token"), request.ToString());
            UtilBussinessService.WriteLog(UtilBussinessService.detoken(request.Value<string>("Token")), "客诉单维护", null, "新增", request.ToString());
            return QMBussinessService.Qcs00009Add(request);
        }

        /// <summary>
        /// 客诉单单一删除
        /// SAM 2017年8月22日15:56:06
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Authenticate]
        public object Qcs00009Delete(JObject request)
        {
            DataLogerService.writeURL(request.Value<string>("Token"), request.ToString());
            UtilBussinessService.WriteLog(UtilBussinessService.detoken(request.Value<string>("Token")), "客诉单维护", null, "新增", request.ToString());
            return QMBussinessService.Qcs00009Delete(request);
        }


        /// <summary>
        /// 客诉单表头的保存
        /// SAM 2017年6月15日08:53:18
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Authenticate]
        public object Qcs00009Save(JObject request)
        {
            DataLogerService.writeURL(request.Value<string>("Token"), request.ToString());
            UtilBussinessService.WriteLog(UtilBussinessService.detoken(request.Value<string>("Token")), "检验群组码主档", null, "保存", request.ToString());
            bool isDelete = false;
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

                        case "updated": isUpdate = true; break;
                    }
                }
            }

            if (isDelete)
                deleted = QMBussinessService.Qcs00009delete(request.Value<string>("Token"), request.Value<JArray>("deleted"));

            if (isUpdate)
                Update = QMBussinessService.Qcs00009update(request.Value<string>("Token"), request.Value<JArray>("updated"));

            return new { inserted = Insert, updated = Update, deleted = deleted };
        }

        /// <summary>
        /// 客诉单的明细列表
        /// SAM 2017年6月14日17:46:52
        /// </summary>
        /// <param name="Token"></param>
        /// <param name="ComplaintID"></param>
        /// <param name="StartOrderCode"></param>
        /// <param name="EndOrderCode"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <returns></returns>
        [HttpGet]
        [Authenticate]
        public object Qcs00009GetDetailsList(string Token, string ComplaintID, string StartOrderCode = null, string EndOrderCode = null, int page = 1, int rows = 10)
        {
            DataLogerService.writeURL(Token, "ComplaintID:" + ComplaintID + "StartOrderCode:" + StartOrderCode + "EndOrderCode:" + EndOrderCode);
            UtilBussinessService.WriteLog(UtilBussinessService.detoken(Token), "客诉单维护", null, "查看", "ComplaintID:" + ComplaintID + "StartOrderCode:" + StartOrderCode + "EndOrderCode:" + EndOrderCode);
            return QMBussinessService.Qcs00009GetDetailsList(Token, ComplaintID, StartOrderCode, EndOrderCode, page, rows);
        }


        /// <summary>
        /// 客诉单明细单一删除
        /// SAM 2017年8月22日15:59:54
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Authenticate]
        public object Qcs00009DetailDelete(JObject request)
        {
            DataLogerService.writeURL(request.Value<string>("Token"), request.ToString());
            UtilBussinessService.WriteLog(UtilBussinessService.detoken(request.Value<string>("Token")), "客诉单维护", null, "删除", request.ToString());
            return QMBussinessService.Qcs00009DetailDelete(request);
        }


        /// <summary>
        /// 客诉单明细的保存
        /// SAM 2017年6月15日11:56:50
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Authenticate]
        public object Qcs00009DetailSave(JObject request)
        {
            DataLogerService.writeURL(request.Value<string>("Token"), request.ToString());
            UtilBussinessService.WriteLog(UtilBussinessService.detoken(request.Value<string>("Token")), "客诉单维护", null, "保存", request.ToString());
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
                deleted = QMBussinessService.Qcs00009Detaildelete(request.Value<string>("Token"), request.Value<JArray>("deleted"));
            if (isInsert)
                Insert = QMBussinessService.Qcs00009Detailinsert(request.Value<string>("Token"), request.Value<JArray>("inserted"));
            if (isUpdate)
                Update = QMBussinessService.Qcs00009Detailupdate(request.Value<string>("Token"), request.Value<JArray>("updated"));

            return new { inserted = Insert, updated = Update, deleted = deleted };
        }

        /// <summary>
        /// 获取附件列表
        /// SAM 2017年6月15日16:24:12
        /// </summary>
        /// <param name="Token"></param>
        /// <param name="ComplaintDetailID"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <returns></returns>
        [HttpGet]
        [Authenticate]
        public object Qcs00009GetAttachmentList(string Token, string ComplaintDetailID, int page = 1, int rows = 10)
        {
            DataLogerService.writeURL(Token, "ComplaintDetailID:" + ComplaintDetailID);
            UtilBussinessService.WriteLog(UtilBussinessService.detoken(Token), "訴單處理對策附件", null, "查看", "ComplaintDetailID:" + ComplaintDetailID);
            return QMBussinessService.Qcs00009GetAttachmentList(Token, ComplaintDetailID, page, rows);
        }

        /// <summary>
        /// 附件的删除
        /// SAM 2017年6月21日17:27:59
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Authenticate]
        public object Qcs00009FileDelete(JObject request)
        {
            DataLogerService.writeURL(request.Value<string>("Token"), request.ToString());
            UtilBussinessService.WriteLog(UtilBussinessService.detoken(request.Value<string>("Token")), "客诉单维护", null, "删除", request.ToString());
            return QMBussinessService.Qcs00009FileDelete(request);
        }



        #endregion

        #region QCS000010客诉分析与改善

        /// <summary>
        /// 客诉分析与改善列表
        /// SAM 2017年6月15日10:59:29
        /// </summary>
        /// <param name="Token"></param>
        /// <param name="StartCode"></param>
        /// <param name="EndCode"></param>
        /// <param name="StartDate"></param>
        /// <param name="EndDate"></param>
        /// <param name="StartCustCode"></param>
        /// <param name="EndCustCode"></param>
        /// <param name="StartOrderCode"></param>
        /// <param name="EndOrderCode"></param>
        /// <param name="Status"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <returns></returns>
        [HttpGet]
        [Authenticate]
        public object Qcs00010GetList(string Token,
            string StartCode = null, string EndCode = null, string StartDate = null, string EndDate = null,
            string StartCustCode = null, string EndCustCode = null, string StartOrderCode = null,
            string EndOrderCode = null, string Status = null,
           int page = 1, int rows = 10)
        {
            DataLogerService.writeURL(Token, "StartCode:" + StartCode + "EndCode:" + EndCode + "StartDate:" + StartDate + "EndDate:" + EndDate + "StartCustCode:" + StartCustCode + "EndCustCode:" + EndCustCode + "Status:" + Status);
            UtilBussinessService.WriteLog(UtilBussinessService.detoken(Token), "客诉分析与改善", null, "查看", "StartCode:" + StartCode + "EndCode:" + EndCode + "StartDate:" + StartDate + "EndDate:" + EndDate + "StartCustCode:" + StartCustCode + "EndCustCode:" + EndCustCode + "Status:" + Status);
            return QMBussinessService.Qcs00010GetList(Token, StartCode, EndCode, StartDate, EndDate, StartCustCode, EndCustCode, StartOrderCode, EndOrderCode, Status, page, rows);
        }

        /// <summary>
        /// 获取客诉原因列表
        /// SAM 2017年6月15日11:31:36
        /// </summary>
        /// <param name="Token"></param>
        /// <param name="ComplaintDetailID"></param>
        /// <param name="GroupCode"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <returns></returns>
        [HttpGet]
        [Authenticate]
        public object Qcs00010GetReasonList(string Token, string ComplaintDetailID, string GroupCode = null, int page = 1, int rows = 10)
        {
            DataLogerService.writeURL(Token, "ComplaintDetailID:" + ComplaintDetailID + "GroupCode:" + GroupCode);
            UtilBussinessService.WriteLog(UtilBussinessService.detoken(Token), "客訴單原因", null, "查看", "ComplaintDetailID:" + ComplaintDetailID + "GroupCode:" + GroupCode);
            return QMBussinessService.Qcs00010GetReasonList(Token, ComplaintDetailID, GroupCode, page, rows);
        }

        /// <summary>
        /// 客诉原因的保存
        /// SAM 2017年6月15日12:00:09
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Authenticate]
        public object Qcs00010ReasonSave(JObject request)
        {
            DataLogerService.writeURL(request.Value<string>("Token"), request.ToString());
            UtilBussinessService.WriteLog(UtilBussinessService.detoken(request.Value<string>("Token")), "客訴單原因", null, "保存", request.ToString());
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
                deleted = QMBussinessService.Qcs00010Reasondelete(request.Value<string>("Token"), request.Value<JArray>("deleted"));
            if (isInsert)
                Insert = QMBussinessService.Qcs00010Reasoninsert(request.Value<string>("Token"), request.Value<JArray>("inserted"));
            if (isUpdate)
                Update = QMBussinessService.Qcs00010Reasonupdate(request.Value<string>("Token"), request.Value<JArray>("updated"));

            return new { inserted = Insert, updated = Update, deleted = deleted };
        }

        /// <summary>
        /// 客訴單处理对策列表
        /// SAM 2017年6月15日14:13:08
        /// </summary>
        /// <param name="Token"></param>
        /// <param name="ComplaintDetailID"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <returns></returns>
        [HttpGet]
        [Authenticate]
        public object Qcs00010GetHandleList(string Token, string ComplaintDetailID, int page = 1, int rows = 10)
        {
            DataLogerService.writeURL(Token, "ComplaintDetailID:" + ComplaintDetailID);
            UtilBussinessService.WriteLog(UtilBussinessService.detoken(Token), "客訴單处理对策", null, "查看", "ComplaintDetailID:" + ComplaintDetailID);
            return QMBussinessService.Qcs00010GetHandleList(Token, ComplaintDetailID, page, rows);
        }

        /// <summary>
        ///  客訴單处理对策新增
        ///  SAM 2017年6月27日13:57:05
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Authenticate]
        public object Qcs00010HandleAdd(JObject request)
        {
            DataLogerService.writeURL(request.Value<string>("Token"), request.ToString());
            UtilBussinessService.WriteLog(UtilBussinessService.detoken(request.Value<string>("Token")), "客訴單处理对策", null, "新增", request.ToString());
            return QMBussinessService.Qcs000010HandleAdd(request);
        }

        /// <summary>
        /// 更新
        /// SAM 2017年6月27日14:12:18
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Authenticate]
        public object Qcs000010HandleUpdate(JObject request)
        {
            DataLogerService.writeURL(request.Value<string>("Token"), request.ToString());
            UtilBussinessService.WriteLog(UtilBussinessService.detoken(request.Value<string>("Token")), "客訴單处理对策", null, "更新", request.ToString());
            return QMBussinessService.Qcs000010HandleUpdate(request);
        }

        /// <summary>
        /// 客诉单处理对策保存
        /// SAM 2017年6月15日15:54:43
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Authenticate]
        public object Qcs00010HandleSave(JObject request)
        {
            DataLogerService.writeURL(request.Value<string>("Token"), request.ToString());
            UtilBussinessService.WriteLog(UtilBussinessService.detoken(request.Value<string>("Token")), "客訴單原因", null, "保存", request.ToString());
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
                deleted = QMBussinessService.Qcs00010Handledelete(request.Value<string>("Token"), request.Value<JArray>("deleted"));
            if (isInsert)
                Insert = QMBussinessService.Qcs00010Handleinsert(request.Value<string>("Token"), request.Value<JArray>("inserted"));
            if (isUpdate)
                Update = QMBussinessService.Qcs00010Handleupdate(request.Value<string>("Token"), request.Value<JArray>("updated"));

            return new { inserted = Insert, updated = Update, deleted = deleted };
        }

        /// <summary>
        /// 处理对策获取附件列表
        /// SAM 2017年6月22日14:14:46
        /// </summary>
        /// <param name="Token"></param>
        /// <param name="ComplaintDetailID"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <returns></returns>
        [HttpGet]
        [Authenticate]
        public object Qcs00010GetAttachmentList(string Token, string ComplaintDetailID, int page = 1, int rows = 10)
        {
            DataLogerService.writeURL(Token, "ComplaintDetailID:" + ComplaintDetailID);
            UtilBussinessService.WriteLog(UtilBussinessService.detoken(Token), "訴單處理對策附件", null, "查看", "ComplaintDetailID:" + ComplaintDetailID);
            return QMBussinessService.Qcs00010GetAttachmentList(Token, ComplaintDetailID, page, rows);
        }

        /// <summary>
        /// 處理對策附件的删除
        /// SAM 2017年6月22日14:14:59
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Authenticate]
        public object Qcs00010FileDelete(JObject request)
        {
            DataLogerService.writeURL(request.Value<string>("Token"), request.ToString());
            UtilBussinessService.WriteLog(UtilBussinessService.detoken(request.Value<string>("Token")), "訴單處理對策附件", null, "删除", request.ToString());
            return QMBussinessService.Qcs00010FileDelete(request);
        }


        #endregion

        #region QCS000011客诉单状态变更

        /// <summary>
        /// 客诉单状态变更列表
        /// SAM 2017年6月15日15:36:51
        /// </summary>
        /// <param name="Token"></param>
        /// <param name="StartCode"></param>
        /// <param name="EndCode"></param>
        /// <param name="StartDate"></param>
        /// <param name="EndDate"></param>
        /// <param name="StartCustCode"></param>
        /// <param name="EndCustCode"></param>
        /// <param name="StartOrderCode"></param>
        /// <param name="EndOrderCode"></param>
        /// <param name="Status"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <returns></returns>
        [HttpGet]
        [Authenticate]
        public object Qcs00011GetList(string Token, string StartCode = null, string EndCode = null, string StartDate = null,
         string EndDate = null, string StartCustCode = null, string EndCustCode = null, string StartOrderCode = null, string EndOrderCode = null, string Status = null,
           int page = 1, int rows = 10)
        {
            DataLogerService.writeURL(Token, "StartCode:" + StartCode + "EndCode:" + EndCode + "StartDate:" + StartDate + "EndDate:" + EndDate + "StartCustCode:" + StartCustCode + "EndCustCode:" + EndCustCode + "StartOrderCode:" + StartOrderCode + "EndOrderCode:" + EndOrderCode + "Status:" + Status);
            UtilBussinessService.WriteLog(UtilBussinessService.detoken(Token), "客诉单状态变更", null, "查看", "StartCode:" + StartCode + "EndCode:" + EndCode + "StartDate:" + StartDate + "EndDate:" + EndDate + "StartCustCode:" + StartCustCode + "EndCustCode:" + EndCustCode + "StartOrderCode:" + StartOrderCode + "EndOrderCode:" + EndOrderCode + "Status:" + Status);
            return QMBussinessService.Qcs00011GetList(Token, StartCode, EndCode, StartDate, EndDate, StartCustCode, EndCustCode, StartOrderCode, EndOrderCode, Status, page, rows);
        }

        /// <summary>
        /// 结案
        /// SAM 2017年6月15日15:57:58
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Authenticate]
        public object Qcs00011CL(JObject request)
        {
            DataLogerService.writeURL(request.Value<string>("Token"), request.ToString());
            UtilBussinessService.WriteLog(UtilBussinessService.detoken(request.Value<string>("Token")), "客诉单状态变更", null, "结案", request.ToString());
            return QMBussinessService.Qcs00011CL(request);
        }

        /// <summary>
        /// 还原
        /// SAM 2017年6月15日15:58:22
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Authenticate]
        public object Qcs00011OP(JObject request)
        {
            DataLogerService.writeURL(request.Value<string>("Token"), request.ToString());
            UtilBussinessService.WriteLog(UtilBussinessService.detoken(request.Value<string>("Token")), "客诉单状态变更", null, "还原", request.ToString());
            return QMBussinessService.Qcs00011OP(request);
        }

        /// <summary>
        /// 注销
        /// SAM 2017年6月15日15:58:44
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Authenticate]
        public object Qcs00011CA(JObject request)
        {
            DataLogerService.writeURL(request.Value<string>("Token"), request.ToString());
            UtilBussinessService.WriteLog(UtilBussinessService.detoken(request.Value<string>("Token")), "客诉单状态变更", null, "注销", request.ToString());
            return QMBussinessService.Qcs00011CA(request);
        }

        #endregion
    }
}
