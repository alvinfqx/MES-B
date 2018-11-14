using MonkeyFly.Core;
using MonkeyFly.MES.BasicService;
using MonkeyFly.MES.FilterAttributes;
using MonkeyFly.MES.Services;
using Newtonsoft.Json.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Web.Http;
using System.Web.Http.Cors;

namespace MonkeyFly.MES.Webapi
{
    /// <summary>
    /// 智能制造
    /// SAM 2017年6月12日17:24:59
    /// </summary>
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class IntelligentManufacturingController : ApiController
    {
        /// <summary>
        /// IntelligentManufacturingAPI
        /// </summary>
        [HttpGet]
        public void IntelligentManufacturingAPI() { }

        #region SFC00001制品制程资料维护
        /// <summary>
        /// 制品列表
        /// SAM 2017年6月19日14:52:39
        /// </summary>
        /// <param name="Token"></param>
        /// <param name="Type">供应形态</param>
        /// <param name="StartCode">开始代号</param>
        /// <param name="EndCode">结束代号</param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <returns></returns>
        [HttpGet]
        [Authenticate]
        public object Sfc00001GetList(string Token, string Type = null, string StartCode = null, string EndCode = null, int page = 1, int rows = 10)
        {
            DataLogerService.writeURL(Token, "StartCode:" + StartCode + "--->EndCode:" + EndCode + "--->Type:" + Type);
            UtilBussinessService.WriteLog(UtilBussinessService.detoken(Token), "制品制程资料", null, "查看", "StartCode:" + StartCode + "--->EndCode:" + EndCode + "--->Type:" + Type);
            return IMBussinessService.Sfc00001GetList(Token, Type, StartCode, EndCode, page, rows);
        }

        #region 制品制程
        /// <summary>
        /// 制程明细列表
        /// SAM 2017年6月20日14:14:32
        /// </summary>
        /// <param name="Token"></param>
        /// <param name="ItemID"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <returns></returns>
        [HttpGet]
        [Authenticate]
        public object Sfc00001GetDetailList(string Token, string ItemID, int page = 1, int rows = 10)
        {
            DataLogerService.writeURL(Token, "ItemID:" + ItemID);
            UtilBussinessService.WriteLog(UtilBussinessService.detoken(Token), "制品制程资料", null, "查看", "ItemID:" + ItemID);
            return IMBussinessService.Sfc00001GetDetailList(Token, ItemID, page, rows);
        }

        /// <summary>
        /// 制品制程的删除
        /// SAM 2017年8月30日21:54:34
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Authenticate]
        public object Sfc00001Delete(JObject request)
        {
            DataLogerService.writeURL(request.Value<string>("Token"), request.ToString());
            UtilBussinessService.WriteLog(UtilBussinessService.detoken(request.Value<string>("Token")), "制品制程资料", null, "删除", request.ToString());
            return IMBussinessService.Sfc00001Delete(request);
        }

        /// <summary>
        /// 制程明细保存
        /// SAM 2017年6月20日14:19:34
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Authenticate]
        public object Sfc00001Save(JObject request)
        {
            DataLogerService.writeURL(request.Value<string>("Token"), request.ToString());
            UtilBussinessService.WriteLog(UtilBussinessService.detoken(request.Value<string>("Token")), "制品制程资料", null, "保存", request.ToString());
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
                deleted = IMBussinessService.Sfc00001delete(request.Value<string>("Token"), request.Value<JArray>("deleted"));
            if (isInsert)
                Insert = IMBussinessService.Sfc00001insert(request.Value<string>("Token"), request.Value<JArray>("inserted"));
            if (isUpdate)
                Update = IMBussinessService.Sfc00001update(request.Value<string>("Token"), request.Value<JArray>("updated"));

            return new { inserted = Insert, updated = Update, deleted = deleted };
        }
        #endregion

        #region BOM和资源
        /// <summary>
        /// 制品制程BOM和资源中左边树的显示
        /// SAM 2017年7月27日02:23:503
        /// </summary>
        /// <param name="Token"></param>
        /// <param name="ItemID"></param>
        /// <returns></returns>
        [HttpGet]
        [Authenticate]
        public object Sfc00001GetTreeList(string Token, string ItemID)
        {
            DataLogerService.writeURL(Token, "ItemID:" + ItemID);
            UtilBussinessService.WriteLog(UtilBussinessService.detoken(Token), "", null, "查看", "ItemID:" + ItemID);
            return IMBussinessService.Sfc00001GetTreeList(Token, ItemID);
        }

        /// <summary>
        /// 根据制品制程流水号或者制品制程工序流水号获取BOM(分页)
        /// MOUSE 2017年8月1日10:56:36
        /// </summary>
        /// <param name="Token"></param>
        /// <param name="ItemProcessID"></param>制品制程流水号
        /// <param name="ItemOperationID"></param>制品工序流水号
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <returns></returns>
        [HttpGet]
        [Authenticate]
        public object Sfc00001BomList(string Token, string ItemProcessID, string ItemOperationID = null, int page = 1, int rows = 10)
        {
            DataLogerService.writeURL(Token, null);
            UtilBussinessService.WriteLog(UtilBussinessService.detoken(Token), "", null, "查看", "ItemProcessID:" + ItemProcessID);
            return IMBussinessService.Sfc00001BomList(Token, ItemProcessID, ItemOperationID, page, rows);
        }


        /// <summary>
        /// 根据制品制程流水号或者制品制程工序流水号获取BOM(不分页)
        /// SAM 2017年7月27日02:25:10
        /// </summary>
        /// <param name="Token"></param>
        /// <param name="ItemProcessID"></param>
        /// <param name="ItemOperationID"></param>
        /// <returns></returns>
        [HttpGet]
        [Authenticate]
        public object Sfc00001GetBomList(string Token, string ItemProcessID, string ItemOperationID = null)
        {
            DataLogerService.writeURL(Token, null);
            UtilBussinessService.WriteLog(UtilBussinessService.detoken(Token), "", null, "查看", "ItemProcessID:" + ItemProcessID);
            return IMBussinessService.Sfc00001GetBomList(Token, ItemProcessID, ItemOperationID);
        }

        /// <summary>
        /// 根据制品制程流水号或者制品制程工序流水号获取不存在的Bom(不分页)
        /// SAM 2017年7月27日02:25:13
        /// MOUSE 2017年8月2日16:18:56 修改为分页
        /// </summary>
        /// <param name="Token"></param>
        /// <param name="FabMoProcessID"></param>
        /// <param name="FabMoOperationID"></param>
        /// 2017年8月2日16:31:21
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <returns></returns>
        [HttpGet]
        [Authenticate]
        public object Sfc00001GetNoBomList(string Token, string FabMoProcessID, string FabMoOperationID = null, int page = 1, int rows = 10)
        {
            DataLogerService.writeURL(Token, null);
            UtilBussinessService.WriteLog(UtilBussinessService.detoken(Token), "", null, "查看", "FabMoOperationID:" + FabMoOperationID);
            return IMBussinessService.Sfc00001GetNoBomList(Token, FabMoProcessID, FabMoOperationID, page, rows);
        }

        /// <summary>
        /// 制品制程添加BOM
        /// SAM 2017年7月27日02:25:38
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Authenticate]
        public object Sfc00001BomAdd(JObject request)
        {
            DataLogerService.writeURL(request.Value<string>("Token"), request.ToString());
            return IMBussinessService.Sfc00001BomAdd(request);
        }

        /// <summary>
        /// 制品制程删除BOM
        /// SAM 2017年7月27日02:25:42
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Authenticate]
        public object Sfc00001BomDelete(JObject request)
        {
            DataLogerService.writeURL(request.Value<string>("Token"), request.ToString());
            return IMBussinessService.Sfc00001BomDelete(request);
        }

        /// <summary>
        /// 制品制程BOM的保存
        /// SAM 2017年7月27日02:25:45
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Authenticate]
        public object Sfc00001BomSave(JObject request)
        {
            DataLogerService.writeURL(request.Value<string>("Token"), request.ToString());
            UtilBussinessService.WriteLog(UtilBussinessService.detoken(request.Value<string>("Token")), "制品制程BOM", null, "保存", request.ToString());
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
                Update = IMBussinessService.Sfc00001Bomupdate(request.Value<string>("Token"), request.Value<JArray>("updated"));

            return new { inserted = Insert, updated = Update, deleted = deleted };
        }

        /// <summary>
        /// 根据制品制程流水号或者制品制程工序流水号获取资源(分页)
        /// MOUSE 2017年8月1日15:09:24
        /// </summary>
        /// <param name="Token"></param>
        /// <param name="ItemProcessID"></param>制品制程流水号
        /// <param name="ItemOperationID"></param>制品工序流水号
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <returns></returns>
        [HttpGet]
        [Authenticate]
        public object Sfc00001BOMResourceList(string Token, string ItemProcessID, string ItemOperationID = null, int page = 1, int rows = 10)
        {
            DataLogerService.writeURL(Token, null);
            UtilBussinessService.WriteLog(UtilBussinessService.detoken(Token), "", null, "查看", "ItemProcessID:" + ItemProcessID);
            return IMBussinessService.Sfc00001BOMResourceList(Token, ItemProcessID, ItemOperationID, page, rows);
        }

        /// <summary>
        /// 根据制程流水号或者制程工序流水号获取资源(不分页)
        /// SAM 2017年7月27日12:06:32
        /// </summary>
        /// <param name="Token"></param>
        /// <param name="ItemProcessID"></param>
        /// <param name="ItemOperationID"></param>
        /// <returns></returns>
        [HttpGet]
        [Authenticate]
        public object Sfc00001GetBOMResourceList(string Token, string ItemProcessID, string ItemOperationID = null)
        {
            DataLogerService.writeURL(Token, null);
            UtilBussinessService.WriteLog(UtilBussinessService.detoken(Token), "", null, "查看", "ItemOperationID:" + ItemOperationID);
            return IMBussinessService.Sfc00001BOMGetResourceList(Token, ItemProcessID, ItemOperationID);
        }

        /// <summary>
        /// 根据制程流水号或者制程工序流水号获取不存在的资源(不分页)
        /// SAM 2017年7月27日12:07:55
        /// MOUSE 2017年8月2日16:32:29 改为分页
        /// </summary>
        /// <param name="Token"></param>
        /// <param name="WorkCenterID"></param>
        /// <param name="ItemProcessID"></param>
        /// <param name="ItemOperationID"></param>
        /// 2017年8月2日16:34:24
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <returns></returns>
        [HttpGet]
        [Authenticate]
        public object Sfc00001GetNoResourceList(string Token, string WorkCenterID, string ItemProcessID, string ItemOperationID = null, int page = 1, int rows = 10)
        {
            DataLogerService.writeURL(Token, null);
            UtilBussinessService.WriteLog(UtilBussinessService.detoken(Token), "", null, "查看", "ItemOperationID:" + ItemOperationID);
            return IMBussinessService.Sfc00001GetNoResourceList(Token, WorkCenterID, ItemProcessID, ItemOperationID, page, rows);
        }

        /// <summary>
        /// 添加资源
        /// SAM 2017年7月27日12:07:50
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Authenticate]
        public object Sfc00001ResourceAdd(JObject request)
        {
            DataLogerService.writeURL(request.Value<string>("Token"), request.ToString());
            return IMBussinessService.Sfc00001ResourceAdd(request);
        }

        /// <summary>
        /// 删除资源
        /// SAM 2017年7月27日12:07:58
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Authenticate]
        public object Sfc00001ResourceDelete(JObject request)
        {
            DataLogerService.writeURL(request.Value<string>("Token"), request.ToString());
            return IMBussinessService.Sfc00001ResourceDelete(request);
        }

        /// <summary>
        /// 资源的保存
        /// SAM 2017年7月27日12:08:01
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Authenticate]
        public object Sfc00001BOMResourceSave(JObject request)
        {
            DataLogerService.writeURL(request.Value<string>("Token"), request.ToString());
            UtilBussinessService.WriteLog(UtilBussinessService.detoken(request.Value<string>("Token")), "制品制程用料", null, "保存", request.ToString());

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
                Update = IMBussinessService.Sfc00001BOMResourceupdate(request.Value<string>("Token"), request.Value<JArray>("updated"));

            return new { inserted = Insert, updated = Update, deleted = deleted };
        }
        #endregion

        #region 製品用料
        /// <summary>
        /// 製品用料列表
        /// SAM 2017年6月20日15:06:44
        /// </summary>
        /// <param name="Token"></param>
        /// <param name="ItemProcessID"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <returns></returns>
        [HttpGet]
        [Authenticate]
        public object Sfc00001GetMaterialList(string Token, string ItemProcessID, int page = 1, int rows = 10)
        {
            DataLogerService.writeURL(Token, "ItemProcessID:" + ItemProcessID);
            UtilBussinessService.WriteLog(UtilBussinessService.detoken(Token), "製品用料", null, "查看", "ItemProcessID:" + ItemProcessID);
            return IMBussinessService.Sfc00001GetMaterialList(Token, ItemProcessID, page, rows);
        }

        /// <summary>
        /// 製品用料的删除
        /// SAM 2017年8月30日21:57:05
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Authenticate]
        public object Sfc00001MaterialDelete(JObject request)
        {
            DataLogerService.writeURL(request.Value<string>("Token"), request.ToString());
            UtilBussinessService.WriteLog(UtilBussinessService.detoken(request.Value<string>("Token")), "制品制程资料", null, "删除", request.ToString());
            return IMBussinessService.Sfc00001MaterialDelete(request);
        }

        /// <summary>
        /// 制品制程用料的保存
        /// SAM 2017年6月20日15:07:28
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Authenticate]
        public object Sfc00001MaterialSave(JObject request)
        {
            DataLogerService.writeURL(request.Value<string>("Token"), request.ToString());
            UtilBussinessService.WriteLog(UtilBussinessService.detoken(request.Value<string>("Token")), "制品制程用料", null, "保存", request.ToString());
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
                deleted = IMBussinessService.Sfc00001Materialdelete(request.Value<string>("Token"), request.Value<JArray>("deleted"));
            if (isInsert)
                Insert = IMBussinessService.Sfc00001Materialinsert(request.Value<string>("Token"), request.Value<JArray>("inserted"));
            if (isUpdate)
                Update = IMBussinessService.Sfc00001Materialupdate(request.Value<string>("Token"), request.Value<JArray>("updated"));

            return new { inserted = Insert, updated = Update, deleted = deleted };
        }
        #endregion

        #region 制品制程/制品制程工序资源
        /// <summary>
        /// 制品制程/制品制程工序的资源删除
        /// SAM 2017年8月30日22:00:14
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Authenticate]
        public object Sfc01ResourceDelete(JObject request)
        {
            DataLogerService.writeURL(request.Value<string>("Token"), request.ToString());
            UtilBussinessService.WriteLog(UtilBussinessService.detoken(request.Value<string>("Token")), "制品制程资料", null, "删除", request.ToString());
            return IMBussinessService.Sfc01ResourceDelete(request);
        }

        /// <summary>
        /// 製程資源列表-设备
        /// SAM 2017年6月21日10:28:48
        /// </summary>
        /// <param name="Token"></param>
        /// <param name="ItemProcessID"></param>
        /// <returns></returns>
        [HttpGet]
        [Authenticate]
        public object Sfc00001GetMResourceList(string Token, string ItemProcessID)
        {
            DataLogerService.writeURL(Token, "ItemProcessID:" + ItemProcessID);
            UtilBussinessService.WriteLog(UtilBussinessService.detoken(Token), "製程資源", null, "查看", "ItemProcessID:" + ItemProcessID);
            return IMBussinessService.Sfc00001GetResourceList(Token, ItemProcessID, "0201213000084");
        }

        /// <summary>
        /// 製程資源保存-设备
        /// SAM 2017年6月21日10:31:53
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Authenticate]
        public object Sfc00001MResourceSave(JObject request)
        {
            DataLogerService.writeURL(request.Value<string>("Token"), request.ToString());
            UtilBussinessService.WriteLog(UtilBussinessService.detoken(request.Value<string>("Token")), "制品制程用料", null, "保存", request.ToString());
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
                deleted = IMBussinessService.Sfc00001ProcessResourcedelete(request.Value<string>("Token"), request.Value<JArray>("deleted"));
            if (isInsert)
                Insert = IMBussinessService.Sfc00001ProcessResourceinsert(request.Value<string>("Token"), request.Value<JArray>("inserted"), "0201213000084");
            if (isUpdate)
                Update = IMBussinessService.Sfc00001ProcessResourceupdate(request.Value<string>("Token"), request.Value<JArray>("updated"), "0201213000084");

            return new { inserted = Insert, updated = Update, deleted = deleted };
        }

        /// <summary>
        /// 製程資源列表-人工
        /// SAM 2017年6月21日10:28:48
        /// </summary>
        /// <param name="Token"></param>
        /// <param name="ItemProcessID"></param>
        /// <returns></returns>
        [HttpGet]
        [Authenticate]
        public object Sfc00001GetLResourceList(string Token, string ItemProcessID)
        {
            DataLogerService.writeURL(Token, "ItemProcessID:" + ItemProcessID);
            UtilBussinessService.WriteLog(UtilBussinessService.detoken(Token), "製程資源", null, "查看", "ItemProcessID:" + ItemProcessID);
            return IMBussinessService.Sfc00001GetResourceList(Token, ItemProcessID, "0201213000085");
        }

        /// <summary>
        /// 製程資源保存-人工
        /// SAM 2017年6月21日10:31:53
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Authenticate]
        public object Sfc00001LResourceSave(JObject request)
        {
            DataLogerService.writeURL(request.Value<string>("Token"), request.ToString());
            UtilBussinessService.WriteLog(UtilBussinessService.detoken(request.Value<string>("Token")), "制品制程用料", null, "保存", request.ToString());
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
                deleted = IMBussinessService.Sfc00001ProcessResourcedelete(request.Value<string>("Token"), request.Value<JArray>("deleted"));
            if (isInsert)
                Insert = IMBussinessService.Sfc00001ProcessResourceinsert(request.Value<string>("Token"), request.Value<JArray>("inserted"), "0201213000085");
            if (isUpdate)
                Update = IMBussinessService.Sfc00001ProcessResourceupdate(request.Value<string>("Token"), request.Value<JArray>("updated"), "0201213000085");

            return new { inserted = Insert, updated = Update, deleted = deleted };
        }

        /// <summary>
        /// 製程資源列表-其他
        /// SAM 2017年6月22日15:12:16
        /// </summary>
        /// <param name="Token"></param>
        /// <param name="ItemProcessID"></param>
        /// <returns></returns>
        [HttpGet]
        [Authenticate]
        public object Sfc00001GetResourceList(string Token, string ItemProcessID)
        {
            DataLogerService.writeURL(Token, "ItemProcessID:" + ItemProcessID);
            UtilBussinessService.WriteLog(UtilBussinessService.detoken(Token), "製程資源", null, "查看", "ItemProcessID:" + ItemProcessID);
            return IMBussinessService.Sfc00001GetResourceList(Token, ItemProcessID, "0201213000086");
        }

        /// <summary>
        /// 製程資源保存-其他
        /// SAM 2017年6月22日15:12:16
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Authenticate]
        public object Sfc00001ResourceSave(JObject request)
        {
            DataLogerService.writeURL(request.Value<string>("Token"), request.ToString());
            UtilBussinessService.WriteLog(UtilBussinessService.detoken(request.Value<string>("Token")), "制品制程用料", null, "保存", request.ToString());
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
                deleted = IMBussinessService.Sfc00001ProcessResourcedelete(request.Value<string>("Token"), request.Value<JArray>("deleted"));
            if (isInsert)
                Insert = IMBussinessService.Sfc00001ProcessResourceinsert(request.Value<string>("Token"), request.Value<JArray>("inserted"), "0201213000086");
            if (isUpdate)
                Update = IMBussinessService.Sfc00001ProcessResourceupdate(request.Value<string>("Token"), request.Value<JArray>("updated"), "0201213000086");

            return new { inserted = Insert, updated = Update, deleted = deleted };
        }

        /// <summary>
        /// 根据制品制程资源获取明细
        /// SAM 2017年6月21日10:42:26
        /// </summary>
        /// <param name="Token"></param>
        /// <param name="ResourceID"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <returns></returns>
        [HttpGet]
        [Authenticate]
        public object Sfc00001IPRDetailList(string Token, string ResourceID, int page = 1, int rows = 10)
        {
            DataLogerService.writeURL(Token, "ResourceID:" + ResourceID);
            UtilBussinessService.WriteLog(UtilBussinessService.detoken(Token), "製品製程資源", null, "查看", "ItemProcessID:" + ResourceID);
            return IMBussinessService.Sfc00001IPRDetailList(Token, ResourceID, page, rows);
        }

        #endregion

        #region 制品制程获取替代制程
        /// <summary>
        /// 根据制品制程获取替代制程列表
        /// SAM 2017年6月22日16:00:46
        /// </summary>
        /// <param name="Token"></param>
        /// <param name="ItemProcessID"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <returns></returns>
        [HttpGet]
        [Authenticate]
        public object Sfc00001GetLAlternativeRelationList(string Token, string ItemProcessID, int page = 1, int rows = 10)
        {
            DataLogerService.writeURL(Token, "ItemProcessID:" + ItemProcessID);
            UtilBussinessService.WriteLog(UtilBussinessService.detoken(Token), "製程資源", null, "查看", "ItemProcessID:" + ItemProcessID);
            return IMBussinessService.Sfc00001GetLAlternativeRelationList(Token, ItemProcessID, page, rows);
        }


        /// <summary>
        /// 保存替代制程
        /// SAM 2017年6月22日16:22:23
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Authenticate]
        public object Sfc00001AlternativeRelationSave(JObject request)
        {
            DataLogerService.writeURL(request.Value<string>("Token"), request.ToString());
            UtilBussinessService.WriteLog(UtilBussinessService.detoken(request.Value<string>("Token")), "制品制程用料", null, "保存", request.ToString());
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
                deleted = IMBussinessService.Sfc00001ARdelete(request.Value<string>("Token"), request.Value<JArray>("deleted"));
            if (isInsert)
                Insert = IMBussinessService.Sfc00001ARinsert(request.Value<string>("Token"), request.Value<JArray>("inserted"));
            if (isUpdate)
                Update = IMBussinessService.Sfc00001ARupdate(request.Value<string>("Token"), request.Value<JArray>("updated"));

            return new { inserted = Insert, updated = Update, deleted = deleted };
        }

        /// <summary>
        /// 替代制程的删除
        /// SAM 2017年8月30日22:02:11
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Authenticate]
        public object Sfc01AlternativeRelationDelete(JObject request)
        {
            DataLogerService.writeURL(request.Value<string>("Token"), request.ToString());
            UtilBussinessService.WriteLog(UtilBussinessService.detoken(request.Value<string>("Token")), "制品制程资料", null, "删除", request.ToString());
            return IMBussinessService.Sfc01AlternativeRelationDelete(request);
        }
        #endregion

        #region 制品制程工序
        /// <summary>
        /// 根据制品制程获取制品工序列表
        /// SAM 2017年6月22日16:45:46
        /// </summary>
        /// <param name="Token"></param>
        /// <param name="ItemProcessID"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <returns></returns>
        [HttpGet]
        [Authenticate]
        public object Sfc00001GetItemOperationList(string Token, string ItemProcessID, int page = 1, int rows = 10)
        {
            DataLogerService.writeURL(Token, "ItemProcessID:" + ItemProcessID);
            UtilBussinessService.WriteLog(UtilBussinessService.detoken(Token), "製程資源", null, "查看", "ItemProcessID:" + ItemProcessID);
            return IMBussinessService.Sfc00001GetItemOperationList(Token, ItemProcessID, page, rows);
        }

        /// <summary>
        /// 制品工序保存
        /// SAM 2017年6月22日17:40:05
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Authenticate]
        public object Sfc00001ItemOperationSave(JObject request)
        {
            DataLogerService.writeURL(request.Value<string>("Token"), request.ToString());
            UtilBussinessService.WriteLog(UtilBussinessService.detoken(request.Value<string>("Token")), "制品制程用料", null, "保存", request.ToString());
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
                deleted = IMBussinessService.Sfc00001ItemOperationdelete(request.Value<string>("Token"), request.Value<JArray>("deleted"));
            if (isInsert)
                Insert = IMBussinessService.Sfc00001ItemOperationinsert(request.Value<string>("Token"), request.Value<JArray>("inserted"));
            if (isUpdate)
                Update = IMBussinessService.Sfc00001ItemOperationupdate(request.Value<string>("Token"), request.Value<JArray>("updated"));

            return new { inserted = Insert, updated = Update, deleted = deleted };
        }

        /// <summary>
        /// 制程工序的删除
        /// SAM 2017年8月30日22:03:55
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Authenticate]
        public object Sfc00001ItemOperationDelete(JObject request)
        {
            DataLogerService.writeURL(request.Value<string>("Token"), request.ToString());
            UtilBussinessService.WriteLog(UtilBussinessService.detoken(request.Value<string>("Token")), "制品制程资料", null, "删除", request.ToString());
            return IMBussinessService.Sfc00001ItemOperationDelete(request);
        }
        #endregion

        #region 製品工序用料
        /// <summary>
        /// 製品工序用料列表
        /// SAM 2017年6月22日18:16:24
        /// </summary>
        /// <param name="Token"></param>
        /// <param name="ItemOperationID"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <returns></returns>
        [HttpGet]
        [Authenticate]
        public object Sfc00001GetOperationMaterialList(string Token, string ItemOperationID, int page = 1, int rows = 10)
        {
            DataLogerService.writeURL(Token, "ItemProcessID:" + ItemOperationID);
            UtilBussinessService.WriteLog(UtilBussinessService.detoken(Token), "製品工序用料", null, "查看", "ItemProcessID:" + ItemOperationID);
            return IMBussinessService.Sfc00001GetOperationMaterialList(Token, ItemOperationID, page, rows);
        }

        /// <summary>
        /// 製品工序用料的删除
        /// SAM 2017年9月23日15:34:01
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Authenticate]
        public object Sfc00001OperationMaterialDelete(JObject request)
        {
            DataLogerService.writeURL(request.Value<string>("Token"), request.ToString());
            UtilBussinessService.WriteLog(UtilBussinessService.detoken(request.Value<string>("Token")), "製品工序用料", null, "删除", request.ToString());
            return IMBussinessService.Sfc00001MaterialDelete(request);
        }

        /// <summary>
        /// 製品工序用料的保存
        /// SAM 2017年6月20日15:07:28
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Authenticate]
        public object Sfc00001OperationMaterialSave(JObject request)
        {
            DataLogerService.writeURL(request.Value<string>("Token"), request.ToString());
            UtilBussinessService.WriteLog(UtilBussinessService.detoken(request.Value<string>("Token")), "制品制程用料", null, "保存", request.ToString());
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
                deleted = IMBussinessService.Sfc00001OperationMaterialdelete(request.Value<string>("Token"), request.Value<JArray>("deleted"));
            if (isInsert)
                Insert = IMBussinessService.Sfc00001OperationMaterialinsert(request.Value<string>("Token"), request.Value<JArray>("inserted"));
            if (isUpdate)
                Update = IMBussinessService.Sfc00001OperationMaterialupdate(request.Value<string>("Token"), request.Value<JArray>("updated"));

            return new { inserted = Insert, updated = Update, deleted = deleted };
        }
        #endregion

        #region 製程工序資源
        /// <summary>
        /// 製程工序資源列表-设备
        /// SAM 2017年6月21日10:28:48
        /// </summary>
        /// <param name="Token"></param>
        /// <param name="ItemOperationID"></param>
        /// <returns></returns>
        [HttpGet]
        [Authenticate]
        public object Sfc00001GetMOperationResourceList(string Token, string ItemOperationID)
        {
            DataLogerService.writeURL(Token, "ItemOperationID:" + ItemOperationID);
            UtilBussinessService.WriteLog(UtilBussinessService.detoken(Token), "製程資源", null, "查看", "ItemProcessID:" + ItemOperationID);
            return IMBussinessService.Sfc00001GetOperationResourceList(Token, ItemOperationID, "0201213000084");
        }

        /// <summary>
        /// 製程工序資源保存-设备
        /// SAM 2017年6月21日10:31:53
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Authenticate]
        public object Sfc00001MOperationResourceSave(JObject request)
        {
            DataLogerService.writeURL(request.Value<string>("Token"), request.ToString());
            UtilBussinessService.WriteLog(UtilBussinessService.detoken(request.Value<string>("Token")), "制品制程用料", null, "保存", request.ToString());
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
                deleted = IMBussinessService.Sfc00001ProcessResourcedelete(request.Value<string>("Token"), request.Value<JArray>("deleted"));
            if (isInsert)
                Insert = IMBussinessService.Sfc00001ProcessResourceinsert(request.Value<string>("Token"), request.Value<JArray>("inserted"), "0201213000084");
            if (isUpdate)
                Update = IMBussinessService.Sfc00001ProcessResourceupdate(request.Value<string>("Token"), request.Value<JArray>("updated"), "0201213000084");

            return new { inserted = Insert, updated = Update, deleted = deleted };
        }

        /// <summary>
        /// 製程工序資源列表-人工
        /// SAM 2017年6月21日10:28:48
        /// </summary>
        /// <param name="Token"></param>
        /// <param name="ItemOperationID"></param>
        /// <returns></returns>
        [HttpGet]
        [Authenticate]
        public object Sfc00001GetLOperationResourceList(string Token, string ItemOperationID)
        {
            DataLogerService.writeURL(Token, "ItemOperationID:" + ItemOperationID);
            UtilBussinessService.WriteLog(UtilBussinessService.detoken(Token), "製程資源", null, "查看", "ItemOperationID:" + ItemOperationID);
            return IMBussinessService.Sfc00001GetOperationResourceList(Token, ItemOperationID, "0201213000085");
        }

        /// <summary>
        /// 製程工序資源保存-人工
        /// SAM 2017年6月21日10:31:53
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Authenticate]
        public object Sfc00001LOperationResourceSave(JObject request)
        {
            DataLogerService.writeURL(request.Value<string>("Token"), request.ToString());
            UtilBussinessService.WriteLog(UtilBussinessService.detoken(request.Value<string>("Token")), "制品制程用料", null, "保存", request.ToString());
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
                deleted = IMBussinessService.Sfc00001ProcessResourcedelete(request.Value<string>("Token"), request.Value<JArray>("deleted"));
            if (isInsert)
                Insert = IMBussinessService.Sfc00001ProcessResourceinsert(request.Value<string>("Token"), request.Value<JArray>("inserted"), "0201213000085");
            if (isUpdate)
                Update = IMBussinessService.Sfc00001ProcessResourceupdate(request.Value<string>("Token"), request.Value<JArray>("updated"), "0201213000085");

            return new { inserted = Insert, updated = Update, deleted = deleted };
        }

        /// <summary>
        /// 製程工序工序資源列表-其他
        /// SAM 2017年6月22日15:12:16
        /// </summary>
        /// <param name="Token"></param>
        /// <param name="ItemOperationID"></param>
        /// <returns></returns>
        [HttpGet]
        [Authenticate]
        public object Sfc00001GetOperationResourceList(string Token, string ItemOperationID)
        {
            DataLogerService.writeURL(Token, "ItemProcessID:" + ItemOperationID);
            UtilBussinessService.WriteLog(UtilBussinessService.detoken(Token), "製程資源", null, "查看", "ItemOperationID:" + ItemOperationID);
            return IMBussinessService.Sfc00001GetOperationResourceList(Token, ItemOperationID, "0201213000086");
        }

        /// <summary>
        /// 製程工序資源保存-其他
        /// SAM 2017年6月22日15:12:16
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Authenticate]
        public object Sfc00001OperationResourceSave(JObject request)
        {
            DataLogerService.writeURL(request.Value<string>("Token"), request.ToString());
            UtilBussinessService.WriteLog(UtilBussinessService.detoken(request.Value<string>("Token")), "制品制程用料", null, "保存", request.ToString());
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
                deleted = IMBussinessService.Sfc00001ProcessResourcedelete(request.Value<string>("Token"), request.Value<JArray>("deleted"));
            if (isInsert)
                Insert = IMBussinessService.Sfc00001ProcessResourceinsert(request.Value<string>("Token"), request.Value<JArray>("inserted"), "0201213000086");
            if (isUpdate)
                Update = IMBussinessService.Sfc00001ProcessResourceupdate(request.Value<string>("Token"), request.Value<JArray>("updated"), "0201213000086");

            return new { inserted = Insert, updated = Update, deleted = deleted };
        }
        #endregion

        #region 制品制程关系
        /// <summary>
        /// 获取制品的制程关系列表（分页）
        /// SAM 2017年6月23日10:48:52
        /// </summary>
        /// <param name="Token"></param>
        /// <param name="ItemID"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <returns></returns>
        [HttpGet]
        [Authenticate]
        public object Sfc00001GetProcessRelationShipList(string Token, string ItemID, int page = 1, int rows = 10)
        {
            DataLogerService.writeURL(Token, "ItemID:" + ItemID);
            UtilBussinessService.WriteLog(UtilBussinessService.detoken(Token), "制程关系", null, "查看", "ItemID:" + ItemID);
            return IMBussinessService.Sfc00001GetProcessRelationShipList(Token, ItemID, page, rows);
        }

        /// <summary>
        /// 制程关系的删除
        /// SAM 2017年8月30日22:05:25
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Authenticate]
        public object Sfc00001ProcessRelationShipDelete(JObject request)
        {
            DataLogerService.writeURL(request.Value<string>("Token"), request.ToString());
            UtilBussinessService.WriteLog(UtilBussinessService.detoken(request.Value<string>("Token")), "制程关系", null, "删除", request.ToString());
            return IMBussinessService.Sfc00001ProcessRelationShipDelete(request);
        }

        /// <summary>
        /// 制程关系保存
        /// SAM 2017年6月23日11:09:04
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Authenticate]
        public object Sfc00001ProcessRelationShipSave(JObject request)
        {
            DataLogerService.writeURL(request.Value<string>("Token"), request.ToString());
            UtilBussinessService.WriteLog(UtilBussinessService.detoken(request.Value<string>("Token")), "制程关系", null, "保存", request.ToString());
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
                deleted = IMBussinessService.Sfc00001PRSdelete(request.Value<string>("Token"), request.Value<JArray>("deleted"));
            if (isInsert)
                Insert = IMBussinessService.Sfc00001PRSinsert(request.Value<string>("Token"), request.Value<JArray>("inserted"));
            if (isUpdate)
                Update = IMBussinessService.Sfc00001PRSupdate(request.Value<string>("Token"), request.Value<JArray>("updated"));

            return new { inserted = Insert, updated = Update, deleted = deleted };
        }

        /// <summary>
        /// 单个保存制程关系
        /// SAM 2017年6月23日11:39:12
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Authenticate]
        public object ProcessRelationShipSave(JObject request)
        {
            DataLogerService.writeURL(request.Value<string>("Token"), request.ToString());
            UtilBussinessService.WriteLog(UtilBussinessService.detoken(request.Value<string>("Token")), "制程关系", null, "结案", request.ToString());
            return IMBussinessService.ProcessRelationShipSave(request);
        }

        /// <summary>
        /// 获取制品的制程关系列表(不分页)
        /// SAM 2017年6月23日11:05:01
        /// </summary>
        /// <param name="Token"></param>
        /// <param name="ItemID"></param>
        /// <returns></returns>
        [HttpGet]
        [Authenticate]
        public IList<Hashtable> Sfc00001GetProcessRelationShipListNoPage(string Token, string ItemID)
        {
            DataLogerService.writeURL(Token, "ItemID:" + ItemID);
            UtilBussinessService.WriteLog(UtilBussinessService.detoken(Token), "制程关系", null, "查看", "ItemID:" + ItemID);
            return IMBussinessService.Sfc00001GetProcessRelationShipListNoPage(Token, ItemID);
        }

        /// <summary>
        /// 判断制程关系是否存在错误
        /// SAM 2017年6月29日14:21:16
        /// 1.判断是否存在两个最终制程
        /// 2.判断是否存在没有设定制程关系的制程
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Authenticate]
        public object CheckProcessRelationShip(JObject request)
        {
            DataLogerService.writeURL(request.Value<string>("Token"), request.ToString());
            UtilBussinessService.WriteLog(UtilBussinessService.detoken(request.Value<string>("Token")), "制程关系", null, "检查", request.ToString());
            return IMBussinessService.CheckProcessRelationShip(request);
        }
        #endregion

        #region 制程工序关系
        /// <summary>
        /// 获取制程的工序关系列表（分页）
        /// SAM 2017年6月23日10:48:52
        /// </summary>
        /// <param name="Token"></param>
        /// <param name="ItemProcessID"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <returns></returns>
        [HttpGet]
        [Authenticate]
        public object Sfc00001GetOperationRelationShipList(string Token, string ItemProcessID, int page = 1, int rows = 10)
        {
            DataLogerService.writeURL(Token, "ItemID:" + ItemProcessID);
            UtilBussinessService.WriteLog(UtilBussinessService.detoken(Token), "制程工序关系", null, "查看", "ItemProcessID:" + ItemProcessID);
            return IMBussinessService.Sfc00001GetOperationRelationShipList(Token, ItemProcessID, page, rows);
        }


        /// <summary>
        /// 判断制品制程工序是否存在错误
        /// SAM 2017年6月29日14:21:16
        /// 1.判断是否存在两个最终工序
        /// 2.判断是否存在没有设定工序关系的工序
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Authenticate]
        public object CheckProcessOperationRelationShip(JObject request)
        {
            DataLogerService.writeURL(request.Value<string>("Token"), request.ToString());
            UtilBussinessService.WriteLog(UtilBussinessService.detoken(request.Value<string>("Token")), "制程工序关系", null, "检查", request.ToString());
            return IMBussinessService.CheckProcessOperationRelationShip(request);
        }


        /// <summary>
        /// 工序关系保存
        /// SAM 2017年6月23日15:30:31
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Authenticate]
        public object Sfc00001OperationRelationShipSave(JObject request)
        {
            DataLogerService.writeURL(request.Value<string>("Token"), request.ToString());
            UtilBussinessService.WriteLog(UtilBussinessService.detoken(request.Value<string>("Token")), "制程工序关系", null, "保存", request.ToString());
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
                deleted = IMBussinessService.Sfc00001ORSdelete(request.Value<string>("Token"), request.Value<JArray>("deleted"));
            if (isInsert)
                Insert = IMBussinessService.Sfc00001ORSinsert(request.Value<string>("Token"), request.Value<JArray>("inserted"));
            if (isUpdate)
                Update = IMBussinessService.Sfc00001ORSupdate(request.Value<string>("Token"), request.Value<JArray>("updated"));


            return new { inserted = Insert, updated = Update, deleted = deleted };
        }

        /// <summary>
        /// 工序关系的删除
        /// SAM 2017年8月30日22:06:55
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Authenticate]
        public object Sfc00001OperationRelationShipDelete(JObject request)
        {
            DataLogerService.writeURL(request.Value<string>("Token"), request.ToString());
            UtilBussinessService.WriteLog(UtilBussinessService.detoken(request.Value<string>("Token")), "制程工序关系", null, "删除", request.ToString());
            return IMBussinessService.Sfc00001OperationRelationShipDelete(request);
        }

        /// <summary>
        /// 单个保存工序关系
        /// SAM 2017年6月23日11:39:12
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Authenticate]
        public object ProcessOperationShipSave(JObject request)
        {
            DataLogerService.writeURL(request.Value<string>("Token"), request.ToString());
            UtilBussinessService.WriteLog(UtilBussinessService.detoken(request.Value<string>("Token")), "制程工序关系", null, "保存", request.ToString());
            return IMBussinessService.OperationRelationShipSave(request);
        }

        /// <summary>
        /// 获取制程的工序关系列表(不分页)
        /// SAM 2017年6月23日11:05:01
        /// </summary>
        /// <param name="Token"></param>
        /// <param name="ItemProcessID"></param>
        /// <returns></returns>
        [HttpGet]
        [Authenticate]
        public IList<Hashtable> Sfc00001GetOperationRelationShipListNoPage(string Token, string ItemProcessID)
        {
            DataLogerService.writeURL(Token, "ItemProcessID:" + ItemProcessID);
            UtilBussinessService.WriteLog(UtilBussinessService.detoken(Token), "制程工序关系", null, "查看", "ItemProcessID:" + ItemProcessID);
            return IMBussinessService.Sfc00001GetOperationRelationShipListNoPage(Token, ItemProcessID);
        }
        #endregion
        #endregion

        #region SFC00002制令单维护
        #region 主界面
        /// <summary>
        /// 获取生管列表
        /// Tom 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Authenticate]
        public object Sfc00002GetProductManagerList(string Token, string Account = null, int page = 1, int rows = 10)
        {
            return IMBussinessService.Sfc00002GetProductManagerList(Account, page, rows);
        }

        /// <summary>
        /// 根据用户获取单据类别列表
        /// Tom
        /// </summary>
        /// <param name="Token"></param>
        /// <param name="MFCUserID"></param>
        /// <returns></returns>
        [HttpGet]
        [Authenticate]
        public object Sfc00002GetBillTypeList(string Token, string MFCUserID)
        {
            return IMBussinessService.Sfc00002GetBillTypeList(Token, MFCUserID);
        }

        /// <summary>
        /// 获取料品开窗数据
        /// Tom 2017年6月27日20点23分
        /// </summary>
        /// <param name="Token"></param>
        /// <param name="Code"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <returns></returns>
        [HttpGet]
        [Authenticate]
        public object Sfc00002GetItemsList(string Token, string Code = null, int page = 1, int rows = 10)
        {
            return IMBussinessService.Sfc00002GetItemsList(Code, page, rows);
        }

        /// <summary>
        /// 获取制令单数据
        /// Tom
        /// </summary>
        /// <param name="Token"></param>
        /// <param name="ID"></param>
        /// <returns></returns>
        [HttpGet]
        [Authenticate]
        public object Sfc00002GetFabricatedMother(string Token, string ID)
        {
            return IMBussinessService.Sfc00002GetFabricatedMother(ID);
        }


        /// <summary>
        /// 获取用户列表
        /// Tom 2017年6月28日19点32
        /// </summary>
        /// <param name="Token"></param>
        /// <param name="Account"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <returns></returns>
        [HttpGet]
        [Authenticate]
        public object Sfc00002GetUserList(string Token, string Account = null, int page = 1, int rows = 10)
        {
            return IMBussinessService.Sfc00002GetUserList(Account, page, rows);
        }

        /// <summary>
        /// 获取客户列表
        /// Tom 2017年6月29日00点34分
        /// </summary>
        /// <param name="Token"></param>
        /// <param name="Code"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <returns></returns>
        [HttpGet]
        [Authenticate]
        public object Sfc00002GetCustomerList(string Token, string Code = null, int page = 1, int rows = 10)
        {
            return IMBussinessService.Sfc00002GetCustomerList(Code, page, rows);
        }

        /// <summary>
        /// 制令单根据料品获取批号
        /// SAM 2017年9月19日15:43:05
        /// </summary>
        /// <param name="Token"></param>
        /// <param name="ItemID"></param>
        /// <param name="Date"></param>
        /// <returns></returns>
        [HttpGet]
        [Authenticate]
        public object Sfc00002GetLotNumber(string Token, string ItemID, string Date = null)
        {
            DataLogerService.writeURL(Token, "ItemID:" + ItemID);
            return IMBussinessService.Sfc00002GetLotNumber(Token, ItemID, Date);
        }

        /// <summary>
        /// 添加制令单
        /// Tom 2017年6月29日00点07分
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Authenticate]
        public object Sfc00002AddFabricatedMother(JObject request)
        {
            DataLogerService.writeURL(request.Value<string>("Token"), request.ToString());
            return IMBussinessService.Sfc00002AddFabricatedMother(request);
        }

        /// <summary>
        /// 修改制令单
        /// Tom 2017年6月29日19点03分
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Authenticate]
        public object Sfc00002UpdateFabricatedMother(JObject request)
        {
            return IMBussinessService.Sfc00002UpdateFabricatedMother(request);
        }

        /// <summary>
        /// 获取制令单列表
        /// Tom 2017年6月29日04点06分
        /// </summary>
        /// <param name="Token"></param>
        /// <param name="MoNo">制令单号，格式：代号</param>
        /// <param name="StartItemCode">开始制品代号，格式：代号</param>
        /// <param name="EndItemCode">结束制品代号，格式：代号</param>
        /// <param name="StartFabMoCode">开始制令单号，格式：代号</param>
        /// <param name="EndFabMoCode">结束制令单号，格式：代号</param>
        /// <param name="CustCode">客户代号，格式：代号</param>
        /// <param name="MESUserCode">业务员代号，格式：代号</param>
        /// <param name="ControlUser">管制员，格式：代号</param>
        /// <param name="Status">状态，格式：流水号,流水号</param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <returns></returns>
        [HttpGet]
        [Authenticate]
        public object Sfc00002FabricatedMotherList(string Token, string MoNo = null,
            string StartItemCode = null, string EndItemCode = null,
            string StartFabMoCode = null, string EndFabMoCode = null,
            string CustCode = null, string MESUserCode = null,
            string ControlUser = null, string Status = null,
            int page = 1, int rows = 10)
        {
            DataLogerService.writeURL(Token, null);
            return IMBussinessService.Sfc00002FabricatedMotherList(Token, MoNo, StartItemCode, EndItemCode,
                StartFabMoCode, EndFabMoCode, CustCode, MESUserCode, ControlUser, Status, page, rows);
        }

        /// <summary>
        /// 核发制令单
        /// SAM 2017年7月13日09:55:46
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Authenticate]
        public object Sfc00002Approve(JObject request)
        {
            DataLogerService.writeURL(request.Value<string>("Token"), request.ToString());
            return IMBussinessService.Sfc00002Approve(request);
        }

        /// <summary>
        /// 制令单作废
        /// SAM 2017年7月13日09:55:54
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Authenticate]
        public object Sfc00002Invalid(JObject request)
        {
            DataLogerService.writeURL(request.Value<string>("Token"), request.ToString());
            return IMBussinessService.Sfc00002Invalid(request);
        }

        /// <summary>
        /// 制令单结案
        /// SAM 2017年7月13日10:07:37
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Authenticate]
        public object Sfc00002Closed(JObject request)
        {
            DataLogerService.writeURL(request.Value<string>("Token"), request.ToString());
            return IMBussinessService.Sfc00002Closed(request);
        }

        /// <summary>
        /// 制令单还原
        /// SAM 2017年7月13日10:08:09
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Authenticate]
        public object Sfc00002Reduction(JObject request)
        {
            DataLogerService.writeURL(request.Value<string>("Token"), request.ToString());
            return IMBussinessService.Sfc00002Reduction(request);
        }
        #endregion

        #region BOM&资源
        /// <summary>
        /// BOM和资源中左边树的显示
        /// SAM 2017年7月15日10:37:44
        /// </summary>
        /// <param name="Token"></param>
        /// <param name="FabricatedMotherID"></param>
        /// <returns></returns>
        [HttpGet]
        [Authenticate]
        public object Sfc00002GetTreeList(string Token, string FabricatedMotherID)
        {
            DataLogerService.writeURL(Token, "FabricatedMotherID:" + FabricatedMotherID);
            UtilBussinessService.WriteLog(UtilBussinessService.detoken(Token), "", null, "查看", "FabricatedMotherID:" + FabricatedMotherID);
            return IMBussinessService.Sfc00002GetTreeList(Token, FabricatedMotherID);
        }

        /// <summary>
        /// 根据指令制程流水号或者指令制程工序流水号获取BOM(不分页)
        /// SAM 2017年7月15日12:32:24
        /// </summary>
        /// <param name="Token"></param>
        /// <param name="FabMoProcessID"></param>
        /// <param name="FabMoOperationID"></param>
        /// <returns></returns>
        [HttpGet]
        [Authenticate]
        public object Sfc00002GetBomList(string Token, string FabMoProcessID, string FabMoOperationID = null)
        {
            DataLogerService.writeURL(Token, null);
            UtilBussinessService.WriteLog(UtilBussinessService.detoken(Token), "", null, "查看", "FabMoOperationID:" + FabMoOperationID);
            return IMBussinessService.Sfc00002GetBomList(Token, FabMoProcessID, FabMoOperationID);
        }

        /// <summary>
        /// 根据指令制程流水号或者指令制程工序流水号获取不存在的Bom
        /// SAM 2017年7月15日12:48:21
        /// </summary>
        /// <param name="Token"></param>
        /// <param name="FabMoProcessID"></param>
        /// <param name="FabMoOperationID"></param>
        ///  Alvin  2017年8月10日09:56:36
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <returns></returns>
        [HttpGet]
        [Authenticate]
        public object Sfc00002GetNoBomList(string Token, string FabMoProcessID, string FabMoOperationID = null, int page = 1, int rows = 10)
        {
            DataLogerService.writeURL(Token, null);
            UtilBussinessService.WriteLog(UtilBussinessService.detoken(Token), "", null, "查看", "FabMoOperationID:" + FabMoOperationID);
            return IMBussinessService.Sfc00002GetNoBomList(Token, FabMoProcessID, FabMoOperationID, page, rows);
        }

        /// <summary>
        /// 添加BOM
        /// SAM 2017年7月15日17:52:18
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Authenticate]
        public object Sfc00002BomAdd(JObject request)
        {
            DataLogerService.writeURL(request.Value<string>("Token"), request.ToString());
            return IMBussinessService.Sfc00002BomAdd(request);
        }

        /// <summary>
        /// 删除BOM
        /// SAM 2017年7月15日17:52:10
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Authenticate]
        public object Sfc00002BomDelete(JObject request)
        {
            DataLogerService.writeURL(request.Value<string>("Token"), request.ToString());
            return IMBussinessService.Sfc00002BomDelete(request);
        }

        /// <summary>
        /// BOM的保存
        /// SAM 2017年7月15日12:54:12
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Authenticate]
        public object Sfc00002BomSave(JObject request)
        {
            DataLogerService.writeURL(request.Value<string>("Token"), request.ToString());
            UtilBussinessService.WriteLog(UtilBussinessService.detoken(request.Value<string>("Token")), "制品制程用料", null, "保存", request.ToString());

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
                Update = IMBussinessService.Sfc00002Bomupdate(request.Value<string>("Token"), request.Value<JArray>("updated"));

            return new { inserted = Insert, updated = Update, deleted = deleted };
        }

        /// <summary>
        /// 根据指令制程流水号或者指令制程工序流水号获取资源(不分页)
        /// </summary>
        /// <param name="Token"></param>
        /// <param name="FabMoProcessID"></param>
        /// <param name="FabMoOperationID"></param>
        /// <returns></returns>
        [HttpGet]
        [Authenticate]
        public object Sfc00002GetResourceList(string Token, string FabMoProcessID, string FabMoOperationID = null)
        {
            DataLogerService.writeURL(Token, null);
            UtilBussinessService.WriteLog(UtilBussinessService.detoken(Token), "", null, "查看", "FabMoOperationID:" + FabMoOperationID);
            return IMBussinessService.Sfc00002GetResourceList(Token, FabMoProcessID, FabMoOperationID);
        }

        /// <summary>
        /// 根据指令制程流水号或者指令制程工序流水号获取不存在的资源(不分页)
        /// SAM 2017年7月15日13:07:38
        /// </summary>
        /// <param name="Token"></param>
        /// <param name="FabMoProcessID"></param>
        /// <param name="FabMoOperationID"></param>
        /// <param name="WorkCenterID"></param>
        /// Alvin 2017年8月10日10:20:30
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <returns></returns>
        [HttpGet]
        [Authenticate]
        public object Sfc00002GetNoResourceList(string Token, string WorkCenterID, string FabMoProcessID, string FabMoOperationID = null, int page = 1, int rows = 10)
        {
            DataLogerService.writeURL(Token, null);
            UtilBussinessService.WriteLog(UtilBussinessService.detoken(Token), "", null, "查看", "FabMoOperationID:" + FabMoOperationID);
            return IMBussinessService.Sfc00002GetNoResourceList(Token, WorkCenterID, FabMoProcessID, FabMoOperationID, page, rows);
        }

        /// <summary>
        /// 添加资源
        /// SAM 2017年7月15日17:52:18
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Authenticate]
        public object Sfc00002ResourceAdd(JObject request)
        {
            DataLogerService.writeURL(request.Value<string>("Token"), request.ToString());
            return IMBussinessService.Sfc00002ResourceAdd(request);
        }

        /// <summary>
        /// 删除资源
        /// SAM 2017年7月15日17:52:10
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Authenticate]
        public object Sfc00002ResourceDelete(JObject request)
        {
            DataLogerService.writeURL(request.Value<string>("Token"), request.ToString());
            return IMBussinessService.Sfc00002ResourceDelete(request);
        }

        /// <summary>
        /// 资源的保存
        /// SAM 2017年7月15日13:07:58
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Authenticate]
        public object Sfc00002ResourceSave(JObject request)
        {
            DataLogerService.writeURL(request.Value<string>("Token"), request.ToString());
            UtilBussinessService.WriteLog(UtilBussinessService.detoken(request.Value<string>("Token")), "制品制程用料", null, "保存", request.ToString());

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
                Update = IMBussinessService.Sfc00002Resourceupdate(request.Value<string>("Token"), request.Value<JArray>("updated"));

            return new { inserted = Insert, updated = Update, deleted = deleted };
        }
        #endregion

        /// <summary>
        /// 获取制令的制程关系列表（分页）
        /// SAM 2017年7月13日10:15:28
        /// </summary>
        /// <param name="Token"></param>
        /// <param name="FabricatedMotherID">制令单流水号</param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <returns></returns>
        [HttpGet]
        [Authenticate]
        public object Sfc00002GetFabMoRelShipList(string Token, string FabricatedMotherID, int page = 1, int rows = 10)
        {
            DataLogerService.writeURL(Token, "FabricatedMotherID:" + FabricatedMotherID);
            UtilBussinessService.WriteLog(UtilBussinessService.detoken(Token), "", null, "查看", "FabricatedMotherID:" + FabricatedMotherID);
            return IMBussinessService.Sfc00002GetFabMoRelShipList(Token, FabricatedMotherID, page, rows);
        }

        /// <summary>
        /// 制令制程关系保存
        /// SAM 2017年7月13日10:15:28
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Authenticate]
        public object Sfc00002FabMoRelShipSave(JObject request)
        {
            DataLogerService.writeURL(request.Value<string>("Token"), request.ToString());
            UtilBussinessService.WriteLog(UtilBussinessService.detoken(request.Value<string>("Token")), "制品制程用料", null, "保存", request.ToString());

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
                Insert = IMBussinessService.Sfc00002FabMoRelShipinsert(request.Value<string>("Token"), request.Value<JArray>("inserted"));
            if (isUpdate)
                Update = IMBussinessService.Sfc00002FabMoRelShipupdate(request.Value<string>("Token"), request.Value<JArray>("updated"));

            return new { inserted = Insert, updated = Update, deleted = deleted };
        }

        /// <summary>
        /// 单个制令制程关系
        /// SAM 2017年7月13日10:24:19 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Authenticate]
        public object FabMoRelShipSave(JObject request)
        {
            DataLogerService.writeURL(request.Value<string>("Token"), request.ToString());
            UtilBussinessService.WriteLog(UtilBussinessService.detoken(request.Value<string>("Token")), "製程資源", null, "结案", request.ToString());
            return IMBussinessService.FabMoRelShipSave(request);
        }

        /// <summary>
        /// 获取制令的制程关系列表(不分页)
        /// SAM 2017年6月23日11:05:01
        /// </summary>
        /// <param name="Token"></param>
        /// <param name="FabricatedMotherID"></param>
        /// <returns></returns>
        [HttpGet]
        [Authenticate]
        public IList<Hashtable> Sfc00002GetFabMoRelShipNoPage(string Token, string FabricatedMotherID)
        {
            DataLogerService.writeURL(Token, "FabricatedMotherID:" + FabricatedMotherID);
            UtilBussinessService.WriteLog(UtilBussinessService.detoken(Token), "製程資源", null, "查看", "FabricatedMotherID:" + FabricatedMotherID);
            return IMBussinessService.Sfc00002GetFabMoRelShipNoPage(Token, FabricatedMotherID);
        }

        /// <summary>
        /// 制令制程关系删除
        /// SAM 2017年7月14日15:47:44
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Authenticate]
        public object FabMoRelShipDelete(JObject request)
        {
            DataLogerService.writeURL(request.Value<string>("Token"), request.ToString());
            return IMBussinessService.FabMoRelShipDelete(request);
        }

        /// <summary>
        /// 获取制令制程列表
        /// </summary>
        /// <param name="Token"></param>
        /// <param name="FabricatedMotherID"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <returns></returns>
        [HttpGet]
        [Authenticate]
        public object Sfc00002FabricatedProcessList(string Token, string FabricatedMotherID, int page = 1, int rows = 10)
        {
            DataLogerService.writeURL(Token, "FabricatedMotherID:" + FabricatedMotherID);
            UtilBussinessService.WriteLog(UtilBussinessService.detoken(Token), "制令制程", null, "查看", "FabricatedMotherID:" + FabricatedMotherID);
            return IMBussinessService.Sfc00002FabricatedProcessList(Token, FabricatedMotherID, page, rows);
        }

        /// <summary>
        /// 保存制令制程
        /// Tom 2017年6月29日09点44分
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Authenticate]
        public object Sfc00002SaveFabricatedProcess(JObject request)
        {
            DataLogerService.writeURL(request.Value<string>("Token"), request.ToString());
            return IMBussinessService.Sfc00002SaveFabricatedProcess(request);
        }

        /// <summary>
        /// 删除制令制程
        /// SAM 2017年7月13日11:00:51
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Authenticate]
        public object Sfc00002FabMoProcessDelete(JObject request)
        {
            DataLogerService.writeURL(request.Value<string>("Token"), request.ToString());
            return IMBussinessService.Sfc00002FabMoProcessDelete(request);
        }

        /// <summary>
        /// 获取制令制程用料列表
        /// Tom 2017年6月29日16点19分
        /// </summary>
        /// <param name="Token"></param>
        /// <param name="FabMoProcessID"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <returns></returns>
        [HttpGet]
        [Authenticate]
        public object Sfc00002GetFabMoItem(string Token, string FabMoProcessID, int page = 1, int rows = 10)
        {
            DataLogerService.writeURL(Token, "FabMoProcessID:" + FabMoProcessID);
            UtilBussinessService.WriteLog(UtilBussinessService.detoken(Token), "制令制程用料", null, "查看", "FabMoProcessID:" + FabMoProcessID);
            return IMBussinessService.Sfc00002GetFabMoItem(Token, FabMoProcessID, page, rows);
        }

        /// <summary>
        /// 保存制令用料
        /// Tom 2017年6月30日09点24分
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Authenticate]
        public object Sfc00002SaveFabMoItem(JObject request)
        {
            DataLogerService.writeURL(request.Value<string>("Token"), request.ToString());
            return IMBussinessService.Sfc00002SaveFabricatedItem(request);
        }

        /// <summary>
        /// 删除制令用料
        /// SAM 2017年7月13日11:04:28
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Authenticate]
        public object Sfc00002FabMoItemDelete(JObject request)
        {
            DataLogerService.writeURL(request.Value<string>("Token"), request.ToString());
            return IMBussinessService.Sfc00002FabMoItemDelete(request);
        }

        /// <summary>
        /// 获取制令设备资源
        /// Tom 2017年7月3日15点38分
        /// </summary>
        /// <param name="Token"></param>
        /// <param name="FabMoProcessID"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <returns></returns>
        [HttpGet]
        [Authenticate]
        public object Sfc00002GetFabMoResourceEquipment(string Token, string FabMoProcessID, int page = 1, int rows = 10)
        {
            return IMBussinessService.Sfc00002GetFabricatedResourceEquipment(Token, FabMoProcessID, page, rows);
        }

        /// <summary>
        /// 获取制令人工资源
        /// Tom 2017年7月3日15点38分
        /// </summary>
        /// <param name="Token"></param>
        /// <param name="FabMoProcessID"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <returns></returns>
        [HttpGet]
        [Authenticate]
        public object Sfc00002GetFabMoResourceUser(string Token, string FabMoProcessID, int page = 1, int rows = 10)
        {
            return IMBussinessService.Sfc00002GetFabricatedResourceUser(Token, FabMoProcessID, page, rows);
        }

        /// <summary>
        /// 获取制令其他资源
        /// Tom 2017年7月3日15点38分
        /// </summary>
        /// <param name="Token"></param>
        /// <param name="FabMoProcessID"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <returns></returns>
        [HttpGet]
        [Authenticate]
        public object Sfc00002GetFabMoResourceOther(string Token, string FabMoProcessID, int page = 1, int rows = 10)
        {
            return IMBussinessService.Sfc00002GetFabricatedResourceOther(Token, FabMoProcessID, page, rows);
        }

        /// <summary>
        /// 保存制令资源
        /// SAM 2017年8月31日11:11:03
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Authenticate]
        public object Sfc00002SaveFabMoResource(JObject request)
        {
            DataLogerService.writeURL(request.Value<string>("Token"), request.ToString());
            UtilBussinessService.WriteLog(UtilBussinessService.detoken(request.Value<string>("Token")), "制品制程用料", null, "保存", request.ToString());

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
                Insert = IMBussinessService.Sfc02FabResourceinsert(request.Value<string>("Token"), request.Value<JArray>("inserted"));
            if (isUpdate)
                Update = IMBussinessService.Sfc02FabResourceupdate(request.Value<string>("Token"), request.Value<JArray>("updated"));

            return new { inserted = Insert, updated = Update, deleted = deleted };
        }


        /// <summary>
        /// 制令制程资源的删除
        /// SAM 2017年7月13日11:12:28
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Authenticate]
        public object Sfc00002FabMoResourceDelete(JObject request)
        {
            DataLogerService.writeURL(request.Value<string>("Token"), request.ToString());
            return IMBussinessService.Sfc00002FabMoResourceDelete(request);
        }

        /// <summary>
        /// 根据制令制程流水号获取制令制程信息
        /// SAM 2017年7月17日17:03:17
        /// </summary>
        /// <param name="Token"></param>
        /// <param name="FabMoProcessID"></param>
        /// <returns></returns>
        [HttpGet]
        [Authenticate]
        public Hashtable Sfc00002GetFabMoProcess(string Token, string FabMoProcessID)
        {
            DataLogerService.writeURL(Token, "FabMoProcessID:" + FabMoProcessID);
            return IMBussinessService.Sfc00002GetFabMoProcess(FabMoProcessID);
        }

        /// <summary>
        /// 根据制令制程工序流水号获取制令制程工序信息
        /// SAM 2017年7月17日18:27:07
        /// </summary>
        /// <param name="Token"></param>
        /// <param name="FabMoOperationID"></param>
        /// <returns></returns>
        [HttpGet]
        [Authenticate]
        public Hashtable Sfc00002GetFabMoOperation(string Token, string FabMoOperationID)
        {
            DataLogerService.writeURL(Token, "FabMoOperationID:" + FabMoOperationID);
            return IMBussinessService.Sfc00002GetFabMoOperation(FabMoOperationID);
        }


        /// <summary>
        /// 获取一个制令制程的拆解替代列表
        /// Tom 2017年7月5日09点49分
        /// </summary>
        /// <param name="Token"></param>
        /// <param name="FabMoProcessID"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <returns></returns>
        [HttpGet]
        [Authenticate]
        public object Sfc00002GetFabMoSplitList(string Token, string FabMoProcessID, int page = 1, int rows = 10)
        {
            DataLogerService.writeURL(Token, null);
            return IMBussinessService.Sfc00002GetFabMoSplitList(Token, FabMoProcessID, page, rows);
        }

        /// <summary>
        /// 保存拆单替代数据
        /// SAM 2017年7月14日10:09:51
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Authenticate]
        public object Sfc00002FabMoProcessSplitSave(JObject request)
        {
            DataLogerService.writeURL(request.Value<string>("Token"), request.ToString());
            UtilBussinessService.WriteLog(UtilBussinessService.detoken(request.Value<string>("Token")), "制令制程工序", null, "保存", request.ToString());
            bool isInsert = false;
            bool isUpdate = false;

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
                Insert = IMBussinessService.Sfc00002FabMoProcessSplitinsert(request.Value<string>("Token"), request.Value<JArray>("inserted"));
            if (isUpdate)
                Update = IMBussinessService.Sfc00002FabMoProcessSplitupdate(request.Value<string>("Token"), request.Value<JArray>("updated"));

            return new { inserted = Insert, updated = Update };
        }

        /// <summary>
        /// 获取一个制令制程的替代关系列表（弹窗）
        /// 根据制品和制程
        /// SAM 2017年7月13日14:08:12
        /// </summary>
        /// <param name="Token"></param>
        /// <param name="ProcessID"></param>
        /// <param name="ItemID"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <returns></returns>
        [HttpGet]
        [Authenticate]
        public object Sfc00002GetFabMoAltRelShipList(string Token, string ItemID, string ProcessID, int page = 1, int rows = 10)
        {
            DataLogerService.writeURL(Token, null);
            return IMBussinessService.Sfc00002GetFabMoAltRelShipList(Token, ItemID, ProcessID, page, rows);
        }

        /// <summary>
        /// 根据制令制程获取他下面的制令制程工序列表
        /// SAM 2017年7月13日16:21:26
        /// </summary>
        /// <param name="Token"></param>
        /// <param name="FabMoProcessID"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <returns></returns>
        [HttpGet]
        [Authenticate]
        public object Sfc00002FabMoOperationList(string Token, string FabMoProcessID, int page = 1, int rows = 10)
        {
            DataLogerService.writeURL(Token, "FabMoProcessID:" + FabMoProcessID);
            UtilBussinessService.WriteLog(UtilBussinessService.detoken(Token), "制令制程", null, "查看", "FabMoProcessID:" + FabMoProcessID);
            return IMBussinessService.Sfc00002FabMoOperationList(Token, FabMoProcessID, page, rows);
        }


        /// <summary>
        /// 制令制程工序保存
        /// SAM 2017年7月13日17:13:53
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Authenticate]
        public object Sfc00002FabMoOperationSave(JObject request)
        {
            DataLogerService.writeURL(request.Value<string>("Token"), request.ToString());
            UtilBussinessService.WriteLog(UtilBussinessService.detoken(request.Value<string>("Token")), "制令制程工序", null, "保存", request.ToString());
            bool isInsert = false;
            bool isUpdate = false;

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
                Insert = IMBussinessService.Sfc00002FabMoOperationinsert(request.Value<string>("Token"), request.Value<JArray>("inserted"));
            if (isUpdate)
                Update = IMBussinessService.Sfc00002FabMoOperationupdate(request.Value<string>("Token"), request.Value<JArray>("updated"));

            return new { inserted = Insert, updated = Update };
        }

        /// <summary>
        /// 制令制程工序的删除
        /// SAM 2017年7月13日17:30:22
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Authenticate]
        public object Sfc00002FabMoOperationDelete(JObject request)
        {
            DataLogerService.writeURL(request.Value<string>("Token"), request.ToString());
            return IMBussinessService.Sfc00002FabMoOperationDelete(request);
        }



        /// <summary>
        /// 获取制程的工序关系列表（分页）
        /// SAM 2017年7月13日17:34:34
        /// </summary>
        /// <param name="Token"></param>
        /// <param name="FabMoProcessID">制程流水号</param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <returns></returns>
        [HttpGet]
        [Authenticate]
        public object Sfc00002GetFabMoOperationRelShipList(string Token, string FabMoProcessID, int page = 1, int rows = 10)
        {
            DataLogerService.writeURL(Token, "FabMoProcessID:" + FabMoProcessID);
            UtilBussinessService.WriteLog(UtilBussinessService.detoken(Token), "", null, "查看", "FabMoProcessID:" + FabMoProcessID);
            return IMBussinessService.Sfc00002GetFabMoOperationRelShipList(Token, FabMoProcessID, page, rows);
        }


        /// <summary>
        /// 制令制程工序关系保存
        /// SAM 2017年7月13日17:33:27
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Authenticate]
        public object Sfc00002FabMoOperationRelShipSave(JObject request)
        {
            DataLogerService.writeURL(request.Value<string>("Token"), request.ToString());
            UtilBussinessService.WriteLog(UtilBussinessService.detoken(request.Value<string>("Token")), "制品制程用料", null, "保存", request.ToString());

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
                Insert = IMBussinessService.Sfc00002FabMoOperationRelShipinsert(request.Value<string>("Token"), request.Value<JArray>("inserted"));
            if (isUpdate)
                Update = IMBussinessService.Sfc00002FabMoOperationRelShipupdate(request.Value<string>("Token"), request.Value<JArray>("updated"));

            return new { inserted = Insert, updated = Update, deleted = deleted };
        }

        /// <summary>
        /// 单个制令制程工序关系
        /// SAM 2017年7月13日10:24:19 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Authenticate]
        public object FabMoOperationRelShipSave(JObject request)
        {
            DataLogerService.writeURL(request.Value<string>("Token"), request.ToString());
            UtilBussinessService.WriteLog(UtilBussinessService.detoken(request.Value<string>("Token")), "製程資源", null, "结案", request.ToString());
            return IMBussinessService.FabMoOperationRelShipSave(request);
        }

        /// <summary>
        /// 获取制程的工序关系列表(不分页)
        /// SAM 2017年7月13日17:34:52
        /// </summary>
        /// <param name="Token"></param>
        /// <param name="FabMoProcessID">制令制程流水号</param>
        /// <returns></returns>
        [HttpGet]
        [Authenticate]
        public IList<Hashtable> Sfc00002GetFabMoOperationRelShipNoPage(string Token, string FabMoProcessID)
        {
            DataLogerService.writeURL(Token, "ItemID:" + FabMoProcessID);
            UtilBussinessService.WriteLog(UtilBussinessService.detoken(Token), "製程資源", null, "查看", "FabMoProcessID:" + FabMoProcessID);
            return IMBussinessService.Sfc00002GetFabMoOperationRelShipNoPage(Token, FabMoProcessID);
        }

        /// <summary>
        /// 制令制程工序关系删除
        /// SAM 2017年7月14日15:49:29
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Authenticate]
        public object FabMoOperationRelShipDelete(JObject request)
        {
            DataLogerService.writeURL(request.Value<string>("Token"), request.ToString());
            return IMBussinessService.FabMoOperationRelShipDelete(request);
        }

        /// <summary>
        /// 判断制程工序关系是否存在错误
        /// SAM 2017年9月10日23:32:57
        /// 判断是否存在没有设定制程关系的制程
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Authenticate]
        public object CheckFabMoOpRelationShip(JObject request)
        {
            DataLogerService.writeURL(request.Value<string>("Token"), request.ToString());
            UtilBussinessService.WriteLog(UtilBussinessService.detoken(request.Value<string>("Token")), "制令製程工序关系", null, "检查", request.ToString());
            return IMBussinessService.CheckFabMoOpRelationShip(request);
        }

        /// <summary>
        /// 判断制令制程关系是否存在错误
        /// SAM 2017年9月10日23:33:00
        /// 判断是否存在没有设定制令制程关系的制令制程
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Authenticate]
        public object CheckFabMoProRelationShip(JObject request)
        {
            DataLogerService.writeURL(request.Value<string>("Token"), request.ToString());
            UtilBussinessService.WriteLog(UtilBussinessService.detoken(request.Value<string>("Token")), "制令製程关系", null, "检查", request.ToString());
            return IMBussinessService.CheckFabMoProRelationShip(request);
        }

        /// <summary>
        /// 获取工序的用料列表
        /// SAM 2017年7月13日17:48:23
        /// </summary>
        /// <param name="Token"></param>
        /// <param name="FabMoOperationID"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <returns></returns>
        [HttpGet]
        [Authenticate]
        public object Sfc00002GetFabMoOperationItem(string Token, string FabMoOperationID, int page = 1, int rows = 10)
        {
            DataLogerService.writeURL(Token, "FabMoOperationID:" + FabMoOperationID);
            UtilBussinessService.WriteLog(UtilBussinessService.detoken(Token), "制令制程工序用料", null, "查看", "FabMoProcessID:" + FabMoOperationID);
            return IMBussinessService.Sfc00002GetFabMoOperationItem(Token, FabMoOperationID, page, rows);
        }


        /// <summary>
        /// 保存工序用料
        /// SAM 2017年7月13日20:18:11
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Authenticate]
        public object Sfc00002FabMoOperationItemSave(JObject request)
        {
            DataLogerService.writeURL(request.Value<string>("Token"), request.ToString());
            UtilBussinessService.WriteLog(UtilBussinessService.detoken(request.Value<string>("Token")), "制品制程用料", null, "保存", request.ToString());

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
                Insert = IMBussinessService.Sfc00002FabMoOperationIteminsert(request.Value<string>("Token"), request.Value<JArray>("inserted"));
            if (isUpdate)
                Update = IMBussinessService.Sfc00002FabMoOperationItemupdate(request.Value<string>("Token"), request.Value<JArray>("updated"));

            return new { inserted = Insert, updated = Update, deleted = deleted };
        }


        /// <summary>
        /// 工序用料的删除
        /// SAM 2017年7月13日20:28:01
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Authenticate]
        public object Sfc00002FabMoOperationItemDelete(JObject request)
        {
            DataLogerService.writeURL(request.Value<string>("Token"), request.ToString());
            return IMBussinessService.Sfc00002FabMoOperationItemDelete(request);
        }

        /// <summary>
        /// 制令制程工序资源-设备
        /// SAM 2017年7月13日20:30:27
        /// </summary>
        /// <param name="Token"></param>
        /// <param name="FabMoOperationID"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <returns></returns>
        [HttpGet]
        [Authenticate]
        public object Sfc00002GetFabMoOperationMResource(string Token, string FabMoOperationID, int page = 1, int rows = 10)
        {
            DataLogerService.writeURL(Token, FabMoOperationID);
            return IMBussinessService.Sfc00002GetFabricatedOperationResource(Token, Framework.SystemID + "0201213000084", FabMoOperationID, page, rows);
        }

        /// <summary>
        /// 制令制程工序资源-人工
        /// Tom 2017年7月3日15点38分
        /// </summary>
        /// <param name="Token"></param>
        /// <param name="FabMoOperationID"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <returns></returns>
        [HttpGet]
        [Authenticate]
        public object Sfc00002GetFabMoOperationLResource(string Token, string FabMoOperationID, int page = 1, int rows = 10)
        {
            return IMBussinessService.Sfc00002GetFabricatedOperationResource(Token, Framework.SystemID + "0201213000085", FabMoOperationID, page, rows);
        }

        /// <summary>
        /// 制令制程工序资源-其他
        /// SAM 2017年7月13日20:31:32
        /// </summary>
        /// <param name="Token"></param>
        /// <param name="FabMoOperationID"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <returns></returns>
        [HttpGet]
        [Authenticate]
        public object Sfc00002GetFabMoOperationResource(string Token, string FabMoOperationID, int page = 1, int rows = 10)
        {
            return IMBussinessService.Sfc00002GetFabricatedOperationResource(Token, Framework.SystemID + "0201213000086", FabMoOperationID, page, rows);
        }




        #endregion

        #region SFC00003制令单拆单作业
        ///<summary>
        ///制令单列表
        ///Joint 2017年6月22日17:24:32
        ///</summary>
        ///<param name="Token">授权码</param>
        ///<param name="StartItemCode"></param>
        ///<param name="EndItemCode"></param>
        ///<param name="StartMoNo"></param>
        ///<param name="EndMoNo"></param>
        ///<param name="page">页码</param>
        ///<param name="rows">行数</param>
        [HttpGet]
        [Authenticate]

        public object Sfc00003GetList(string Token, string StartItemCode = null, string EndItemCode = null, string StartMoNo = null, string EndMoNo = null, int page = 1, int rows = 10)
        {
            DataLogerService.writeURL(Token, "StartItemCode:" + StartItemCode + "EndItemCode:" + EndItemCode + "StartMoNo:" + StartMoNo + "EndMoNo:" + EndMoNo);
            UtilBussinessService.WriteLog(UtilBussinessService.detoken(Token), "制令单资料", null, "查看", "StartItemCode:" + StartItemCode + "EndItemCode:" + EndItemCode + "StartMoNo:" + StartMoNo + "EndMoNo:" + EndMoNo);
            return IMBussinessService.Sfc00003GetList(Token, StartItemCode, EndItemCode, StartMoNo, EndMoNo, page, rows);
        }
        /// <summary>
        /// 拆单保存
        /// joint
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public object Sfc00003DetailSave(JObject request)
        {
            DataLogerService.writeURL(request.Value<string>("Token"), request.ToString());
            UtilBussinessService.WriteLog(UtilBussinessService.detoken(request.Value<string>("Token")), "制令单资料", null, "拆单处理保存", request.ToString());
            object Insert = null;
            Insert = IMBussinessService.Sfc00003DetailSave(request.Value<string>("Token"), request.Value<string>("FabricatedMotherID"), request.Value<JArray>("inserted"));
            return new { inserted = Insert };
        }
        #endregion

        #region SFC00004任务单分派与维护
        /// <summary>
        /// 任务单分派与维护主列表
        /// SAM 2017年6月26日17:58:53
        /// </summary>
        /// <param name="Token"></param>
        /// <param name="StartWorkCenterCode"></param>
        /// <param name="EndWorkCenterCode"></param>
        /// <param name="StartProcessCode"></param>
        /// <param name="EndProcessCode"></param>
        /// <param name="StartFabMoCode"></param>
        /// <param name="EndFabMoCode"></param>
        /// <param name="StartDate"></param>
        /// <param name="EndDate"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <returns></returns>
        [HttpGet]
        [Authenticate]
        public object Sfc00004GetList(string Token,
           string StartWorkCenterCode = null, string EndWorkCenterCode = null,
           string StartProcessCode = null, string EndProcessCode = null,
           string StartFabMoCode = null, string EndFabMoCode = null,
           string StartDate = null, string EndDate = null, int page = 1, int rows = 10)
        {
            DataLogerService.writeURL(Token, null);
            UtilBussinessService.WriteLog(UtilBussinessService.detoken(Token), "RC分派与维护", null, "查看", null);
            return IMBussinessService.Sfc00004GetList(Token, StartWorkCenterCode, EndWorkCenterCode,
            StartProcessCode, EndProcessCode, StartFabMoCode, EndFabMoCode, StartDate, EndDate, page, rows);
        }

        /// <summary>
        /// 任务单分派列表
        /// SAM 2017年6月29日16:55:23
        /// </summary>
        /// <param name="Token"></param>
        /// <param name="FabMoOperationID"></param>
        /// <param name="FabMoProcessID"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <returns></returns>
        [HttpGet]
        [Authenticate]
        public object Sfc00004GetDispatchList(string Token, string FabMoOperationID = null, string FabMoProcessID = null, int page = 1, int rows = 10)
        {
            DataLogerService.writeURL(Token, null);
            UtilBussinessService.WriteLog(UtilBussinessService.detoken(Token), "RC分派与维护", null, "查看", null);
            return IMBussinessService.Sfc00004GetDispatchList(Token, FabMoOperationID, FabMoProcessID, page, rows);
        }

        /// <summary>
        /// 任务单分派保存
        /// SAM 2017年7月3日09:56:11
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Authenticate]
        public object Sfc00004Save(JObject request)
        {
            DataLogerService.writeURL(request.Value<string>("Token"), request.ToString());
            UtilBussinessService.WriteLog(UtilBussinessService.detoken(request.Value<string>("Token")), "RC分派与维护", null, "保存", request.ToString());

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
                Insert = IMBussinessService.Sfc00004insert(request.Value<string>("Token"), request.Value<JArray>("inserted"));
            if (isUpdate)
                Update = IMBussinessService.Sfc00004update(request.Value<string>("Token"), request.Value<JArray>("updated"));

            return new { inserted = Insert, updated = Update, deleted = deleted };
        }

        /// <summary>
        /// 单个任务单分派保存--安卓用
        /// Mouse 2017年9月26日09:44:41 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Authenticate]
        public object Sfc00004InsertByAndroid(JObject request)
        {
            DataLogerService.writeURL(request.Value<string>("Token"), request.ToString());
            UtilBussinessService.WriteLog(UtilBussinessService.detoken(request.Value<string>("Token")), "RC分派与维护", null, "新增", request.ToString());
            return IMBussinessService.Sfc00004InsertByAndroid(request);
        }

        /// <summary>
        /// 单个任务单分派修改--安卓用
        /// Mouse 2017年9月26日09:44:41 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Authenticate]
        public object Sfc00004UpdateByAndroid(JObject request)
        {
            DataLogerService.writeURL(request.Value<string>("Token"), request.ToString());
            UtilBussinessService.WriteLog(UtilBussinessService.detoken(request.Value<string>("Token")), "RC分派与维护", null, "修改", request.ToString());
            return IMBussinessService.Sfc00004UpdateByAndroid(request);
        }

        /// <summary>
        /// 判断分派量是否合格
        /// SAM 2017年7月6日15:23:41
        /// true表示超过  false表示不超过
        /// </summary>
        /// <param name="Token"></param>
        /// <param name="DispatchQuantity"></param>
        /// <param name="FabMoProcessID"></param>
        /// <param name="FabMoOperationID"></param>
        /// <param name="TaskDispatchID"></param>
        /// <returns></returns>
        [HttpGet]
        [Authenticate]
        public string Sfc00004CheckDispatchQuantity(string Token, string DispatchQuantity, string FabMoProcessID, string FabMoOperationID = null, string TaskDispatchID = null)
        {
            DataLogerService.writeURL(Token, null);
            return IMBussinessService.Sfc00004CheckDispatchQuantity(Token, DispatchQuantity, FabMoProcessID, FabMoOperationID, TaskDispatchID);
        }

        /// <summary>
        /// 判断是否能够转作废
        /// SAM 2017年7月6日15:41:25
        /// true表示可以，False表示不可以
        /// </summary>
        /// <param name="Token"></param>
        /// <param name="FabMoProcessID"></param>
        /// <param name="FabMoOperationID"></param>
        /// <returns></returns>
        [HttpGet]
        [Authenticate]
        public bool Sfc00004CheckStatus(string Token, string FabMoProcessID, string FabMoOperationID = null)
        {
            DataLogerService.writeURL(Token, null);
            return IMBussinessService.Sfc00004CheckStatus(Token, FabMoProcessID, FabMoOperationID);
        }

        /// <summary>
        /// 根据任务单号获取他的信息
        /// SAM 2017年7月6日16:24:28
        /// </summary>
        /// <param name="Token"></param>
        /// <param name="TaskDispatchID"></param>
        /// <returns></returns>
        [HttpGet]
        [Authenticate]
        public Hashtable Sfc00004GetTask(string Token, string TaskDispatchID)
        {
            DataLogerService.writeURL(Token, "TaskDispatchID" + TaskDispatchID);
            return IMBussinessService.Sfc00004GetTask(Token, TaskDispatchID);
        }

        /// <summary>
        /// 根据任务单获取他的明细列表
        /// SAM 2017年7月10日17:34:41
        /// </summary>
        /// <param name="Token"></param>
        /// <param name="TaskDispatchID"></param>
        /// <returns></returns>
        [HttpGet]
        [Authenticate]
        public List<Hashtable> Sfc00004GetResource(string Token, string TaskDispatchID)
        {
            DataLogerService.writeURL(Token, "TaskDispatchID" + TaskDispatchID);
            return IMBussinessService.Sfc00004GetResource(Token, TaskDispatchID);
        }

        /// <summary>
        /// 任务单资源明细列表
        /// SAM 2017年7月3日14:44:59
        /// </summary>
        /// <param name="Token"></param>
        /// <param name="TaskDispatchID"></param>
        /// <param name="Type">对应模块，人工，机器，其他</param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <returns></returns>
        [HttpGet]
        [Authenticate]
        public object Sfc00004GetResourceList(string Token, string TaskDispatchID, string Type, int page = 1, int rows = 10)
        {
            DataLogerService.writeURL(Token, null);
            UtilBussinessService.WriteLog(UtilBussinessService.detoken(Token), "任务分派与维护", null, "查看", null);
            return IMBussinessService.Sfc00004GetResourceList(Token, TaskDispatchID, Type, page, rows);
        }

        /// <summary>
        /// 任务单资源明细保存
        /// SAM 2017年7月5日16:53:28
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Authenticate]
        public object Sfc00004ResourceSave(JObject request)
        {
            DataLogerService.writeURL(request.Value<string>("Token"), request.ToString());
            UtilBussinessService.WriteLog(UtilBussinessService.detoken(request.Value<string>("Token")), " 任务单资源明细", null, "保存", request.ToString());

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
                Insert = IMBussinessService.Sfc00004Resourceinsert(request.Value<string>("Token"), request.Value<JArray>("inserted"));
            if (isUpdate)
                Update = IMBussinessService.Sfc00004Resourceupdate(request.Value<string>("Token"), request.Value<JArray>("updated"));

            return new { inserted = Insert, updated = Update, deleted = deleted };
        }

        /// <summary>
        /// 单个任务单资源明细保存--安卓用
        /// Mouse 2017年9月26日09:44:41 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Authenticate]
        public object Sfc00004ResourceInsertByAndroid(JObject request)
        {
            DataLogerService.writeURL(request.Value<string>("Token"), request.ToString());
            UtilBussinessService.WriteLog(UtilBussinessService.detoken(request.Value<string>("Token")), "任务单资源明细", null, "新增", request.ToString());
            return IMBussinessService.Sfc00004ResourceInsertByAndroid(request);
        }

        /// <summary>
        /// 单个任务单资源明细修改--安卓用
        /// Mouse 2017年9月26日09:44:41 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Authenticate]
        public object Sfc00004ResourceUpdateByAndroid(JObject request)
        {
            DataLogerService.writeURL(request.Value<string>("Token"), request.ToString());
            UtilBussinessService.WriteLog(UtilBussinessService.detoken(request.Value<string>("Token")), "任务单资源明细", null, "修改", request.ToString());
            return IMBussinessService.Sfc00004ResourceUpdateByAndroid(request);
        }

        /// <summary>
        /// 任务单资源明细的删除
        /// SAM 2017年7月10日16:58:00
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Authenticate]
        public object Sfc00004ResourceDelete(JObject request)
        {
            DataLogerService.writeURL(request.Value<string>("Token"), request.ToString());
            UtilBussinessService.WriteLog(UtilBussinessService.detoken(request.Value<string>("Token")), "任务单", null, "删除", request.ToString());
            return IMBussinessService.Sfc00004ResourceDelete(request);
        }

        /// <summary>
        /// 根据制令制程流水号获取制令制程信息
        /// SAM 2017年10月24日11:33:04
        /// SFC04表头专用，已分派量=制造量（工序）-已分派量（所有非删除，非作废的任务单）+差异量（所有非删除，非作废的任务单）
        /// </summary>
        /// <param name="Token"></param>
        /// <param name="FabMoProcessID"></param>
        /// <returns></returns>
        [HttpGet]
        [Authenticate]
        public Hashtable Sfc00004GetFabMoProcess(string Token, string FabMoProcessID)
        {
            DataLogerService.writeURL(Token, "FabMoProcessID:" + FabMoProcessID);
            return IMBussinessService.Sfc00004GetFabMoProcess(FabMoProcessID);
        }


        /// <summary>
        /// 根据制令制程工序流水号获取制令制程工序信息
        /// SAM 2017年10月24日11:33:04
        /// SFC04表头专用，已分派量=制造量（工序）-已分派量（所有非删除，非作废的任务单）+差异量（所有非删除，非作废的任务单）
        /// </summary>
        /// <param name="Token"></param>
        /// <param name="FabMoOperationID"></param>
        /// <returns></returns>
        [HttpGet]
        [Authenticate]
        public Hashtable Sfc00004GetFabMoOperation(string Token, string FabMoOperationID)
        {
            DataLogerService.writeURL(Token, "FabMoOperationID:" + FabMoOperationID);
            return IMBussinessService.Sfc00004GetFabMoOperation(FabMoOperationID);
        }


        /// <summary>
        /// 判断分派量是否合格
        /// SAM 2017年10月24日11:56:48
        /// V1版本：分派量的计算公式中加入了差异量，所以可分派量需要重新计算
        /// </summary>
        /// <param name="Token"></param>
        /// <param name="DispatchQuantity"></param>
        /// <param name="FabMoProcessID"></param>
        /// <param name="FabMoOperationID"></param>
        /// <param name="TaskDispatchID"></param>
        /// <returns></returns>
        [HttpGet]
        [Authenticate]
        public string Sfc00004CheckDispatchQuantityV1(string Token, string DispatchQuantity, string FabMoProcessID, string FabMoOperationID = null, string TaskDispatchID = null)
        {
            DataLogerService.writeURL(Token, null);
            return IMBussinessService.Sfc00004CheckDispatchQuantityV1(Token, DispatchQuantity, FabMoProcessID, FabMoOperationID, TaskDispatchID);
        }
        #endregion

        #region SFC00005RUN CARD发派处理
        #endregion

        #region SFC00006工作站作业维护
        /// <summary>
        /// 获取工作站作业列表
        /// Tom 2017年7月17日15点57分
        /// </summary>
        /// <param name="Token"></param>
        /// <param name="TaskNo"></param>
        /// <param name="MoNo"></param>
        /// <param name="WorkcenterCode"></param>
        /// <param name="ProcessCode"></param>
        /// <param name="OperationCode"></param>
        /// <param name="ClassCode"></param>
        /// <param name="StartDate"></param>
        /// <param name="EndDate"></param>
        /// <param name="Status"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <returns></returns>
        [HttpGet]
        [Authenticate]
        public object Sfc00006GetList(string Token, string TaskNo = null,
            string MoNo = null, string WorkcenterCode = null, string ProcessCode = null,
            string OperationCode = null, string ClassCode = null,
            string StartDate = null, string EndDate = null, string Status = null, int page = 1, int rows = 10)
        {
            DataLogerService.writeURL(Token, null);
            UtilBussinessService.WriteLog(UtilBussinessService.detoken(Token), "工作站作业维护", null, "查看", null);
            return IMBussinessService.Sfc00006GetList(Token, TaskNo, MoNo, WorkcenterCode, ProcessCode, OperationCode,
                ClassCode, StartDate, EndDate, Status, page, rows);
        }

        /// <summary>
        /// 获取工作站作业列表
        /// SAM 2017年10月24日09:27:32 
        /// 在原来的基础上，加多了班别字段。代号-名称
        /// </summary>
        /// <param name="Token"></param>
        /// <param name="TaskNo"></param>
        /// <param name="MoNo"></param>
        /// <param name="WorkcenterCode"></param>
        /// <param name="ProcessCode"></param>
        /// <param name="OperationCode"></param>
        /// <param name="ClassCode"></param>
        /// <param name="StartDate"></param>
        /// <param name="EndDate"></param>
        /// <param name="Status"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <returns></returns>
        [HttpGet]
        [Authenticate]
        public object Sfc00006GetListV1(string Token, string TaskNo = null,
         string MoNo = null, string WorkcenterCode = null, string ProcessCode = null,
         string OperationCode = null, string ClassCode = null,
         string StartDate = null, string EndDate = null, string Status = null, int page = 1, int rows = 10)
        {
            DataLogerService.writeURL(Token, null);
            UtilBussinessService.WriteLog(UtilBussinessService.detoken(Token), "工作站作业维护", null, "查看", null);
            return IMBussinessService.Sfc00006GetListV1(Token, TaskNo, MoNo, WorkcenterCode, ProcessCode, OperationCode,
                ClassCode, StartDate, EndDate, Status, page, rows);
        }

        /// <summary>
        /// 获取工作站作业列表
        /// SAM 2017年10月26日19:15:04
        /// 在V1的基础上，加多了进站人和出站人的显示
        /// </summary>
        /// <param name="Token"></param>
        /// <param name="TaskNo"></param>
        /// <param name="MoNo"></param>
        /// <param name="WorkcenterCode"></param>
        /// <param name="ProcessCode"></param>
        /// <param name="OperationCode"></param>
        /// <param name="ClassCode"></param>
        /// <param name="StartDate"></param>
        /// <param name="EndDate"></param>
        /// <param name="Status"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <returns></returns>
        [HttpGet]
        [Authenticate]
        public object Sfc00006GetListV2(string Token, string TaskNo = null,
        string MoNo = null, string WorkcenterCode = null, string ProcessCode = null,
        string OperationCode = null, string ClassCode = null,
        string StartDate = null, string EndDate = null, string Status = null, int page = 1, int rows = 10)
        {
            DataLogerService.writeURL(Token, null);
            UtilBussinessService.WriteLog(UtilBussinessService.detoken(Token), "工作站作业维护", null, "查看", null);
            return IMBussinessService.Sfc00006GetListV2(Token, TaskNo, MoNo, WorkcenterCode, ProcessCode, OperationCode,
                ClassCode, StartDate, EndDate, Status, page, rows);
        }

        /// <summary>
        /// 用料视窗列表
        /// Joint 2017年7月21日09:38:38
        /// </summary>
        /// <param name="Token"></param>
        /// <param name="TaskDispatchID"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <returns></returns>
        [HttpGet]
        [Authenticate]
        public object Sfc00006GetItemList(string Token, string TaskDispatchID, int page = 1, int rows = 10)
        {
            DataLogerService.writeURL(Token, null);
            UtilBussinessService.WriteLog(UtilBussinessService.detoken(Token), "用料明细", null, "查看", null);
            return IMBussinessService.Sfc00006GetItemList(Token, TaskDispatchID, page, rows);
        }

        /// <summary>
        /// 获取资源异常列表
        /// Tom 2017年7月20日15点20分
        /// </summary>
        /// <param name="Token"></param>
        /// <param name="TaskDispatchID"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <returns></returns>
        [HttpGet]
        [Authenticate]
        public object Sfc00006GetExceptionList(string Token, string TaskDispatchID, int page = 1, int rows = 10)
        {
            DataLogerService.writeURL(Token, null);
            UtilBussinessService.WriteLog(UtilBussinessService.detoken(Token), "工作站异常", null, "查看", null);
            return IMBussinessService.Sfc00006GetExceptionList(Token, TaskDispatchID, page, rows);
        }

        /// <summary>
        /// 保存资源异常
        /// Tom 2017年7月24日09:56:36
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Authenticate]
        public object Sfc00006SaveException(JObject request)
        {
            return IMBussinessService.Sfc00006SaveException(request);
        }

        /// <summary>
        /// 删除工作中心资源异常
        /// Tom 2017年7月24日10:56:42
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Authenticate]
        public object Sfc00006ExceptionDelete(JObject request)
        {
            return IMBussinessService.Sfc00006ExceptionDelete(request);
        }

        /// <summary>
        /// 进站
        /// Tom 2017年7月24日15:17:19
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Authenticate]
        public object Sfc00006Arrival(JObject request)
        {
            return IMBussinessService.Sfc00006Arrival(request);
        }

        /// <summary>
        /// 进站
        /// SAM 2017年10月26日21:45:59
        /// 增加进站人的记录
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Authenticate]
        public object Sfc00006ArrivalV1(JObject request)
        {
            DataLogerService.writeURL(request.Value<string>("Token"), null);
            UtilBussinessService.WriteLog(UtilBussinessService.detoken(request.Value<string>("Token")), "工作站", null, "进站", null);
            return IMBussinessService.Sfc00006ArrivalV1(request);
        }

        /// <summary>
        /// 出站
        /// SAM 2017年10月26日21:47:06
        /// 增加出站人的记录
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Authenticate]
        public object Sfc00006OutboundV1(JObject request)
        {
            DataLogerService.writeURL(request.Value<string>("Token"), null);
            UtilBussinessService.WriteLog(UtilBussinessService.detoken(request.Value<string>("Token")), "工作站", null, "出站", null);
            return IMBussinessService.Sfc00006OutboundV1(request);
        }


        /// <summary>
        /// 出站
        /// Tom 2017年7月24日15:39:40
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Authenticate]
        public object Sfc00006Outbound(JObject request)
        {
            return IMBussinessService.Sfc00006Outbound(request);
        }

        /// <summary>
        /// 异常暂停
        /// Tom 2017年7月25日14:11:22
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Authenticate]
        public object Sfc00006ExceptionStop(JObject request)
        {
            return IMBussinessService.Sfc00006ExceptionStop(request);
        }

        /// <summary>
        /// 异常解除
        /// Tom 2017年7月25日15:13:41
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Authenticate]
        public object Sfc00006ExceptionRelieve(JObject request)
        {
            DataLogerService.writeURL(request.Value<string>("Token"), request.ToString());
            return IMBussinessService.Sfc00006ExceptionRelieve(request);
        }

        /// <summary>
        /// 完工新增
        /// SAM 2017年8月22日15:44:42
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Authenticate]
        public object Sfc00006CompletionAdd(JObject request)
        {
            DataLogerService.writeURL(request.Value<string>("Token"), request.ToString());
            UtilBussinessService.WriteLog(UtilBussinessService.detoken(request.Value<string>("Token")), "工作站", null, "新增", request.ToString());
            return IMBussinessService.Sfc00006CompletionAdd(request);
        }

        /// <summary>
        /// 检测对应的任务单可否报工
        /// Sam 2017年11月1日14:43:30
        /// true 可报工 false 不可报告
        /// </summary>
        /// <param name="Token"></param>
        /// <param name="TaskDispatchID"></param>
        /// <returns></returns>
        [HttpGet]
        [Authenticate]
        public bool Sfc00006CheckCompletion(string Token, string TaskDispatchID)
        {
            DataLogerService.writeURL(Token, "TaskDispatchID:"+ TaskDispatchID);
            return IMBussinessService.Sfc00006CheckCompletion(Token,TaskDispatchID);
        }

        #endregion

        #region SFC00007完工回报作业

        /// <summary>
        /// 完工單回報作業-列表
        /// SAM 2017年7月18日17:06:09
        /// </summary>
        /// <param name="Token"></param>
        /// <param name="TaskDispatchID"></param>
        /// <param name="FinishNo">完工单，格式：单号</param>
        /// <param name="Date">完工日期</param>
        /// <param name="WorkCenter">工作中心，格式：代号</param>
        /// <param name="Process">制程，格式：代号</param>
        /// <param name="FabricatedMother">制令单号，格式：代号</param>
        /// <param name="Status">状态，格式：流水号，流水号</param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <returns></returns>
        [HttpGet]
        [Authenticate]
        public object SFC00007GetList(string Token, string TaskDispatchID = null, string FinishNo = null, string Date = null,
            string WorkCenter = null, string Process = null, string FabricatedMother = null, string Status = null, int page = 1, int rows = 10)
        {
            DataLogerService.writeURL(Token, "FinishNo:" + FinishNo + "--->Date:" + Date + "--->WorkCenter:" + WorkCenter + "--->Process:" + Process + "--->FabricatedMother:" + FabricatedMother + "--->Status:" + Status);
            UtilBussinessService.WriteLog(UtilBussinessService.detoken(Token), "完工單回報作業", null, "查看", "FinishNo:" + FinishNo + "--->Date:" + Date + "--->WorkCenter:" + WorkCenter + "--->Process:" + Process + "--->FabricatedMother:" + FabricatedMother + "--->Status:" + Status);
            return IMBussinessService.SFC00007GetList(Token, TaskDispatchID, FinishNo, Date, WorkCenter, Process, FabricatedMother, Status, page, rows);
        }

        /// <summary>
        /// 根据任务单流水号获取完工单
        /// Tom 2017年7月26日15点09分
        /// </summary>
        /// <param name="Token"></param>
        /// <param name="TaskDispatchID"></param>
        /// <returns></returns>
        [HttpGet]
        [Authenticate]
        public object Sfc00007GetByTaskDispatchID(string Token, string TaskDispatchID)
        {
            DataLogerService.writeURL(Token, "TaskDispatchID:" + TaskDispatchID);
            return IMBussinessService.Sfc00007GetByTaskDispatchID(Token, TaskDispatchID);
        }

        /// <summary>
        /// 获取当前登录用户的类别设定
        /// SAM 2017年7月28日14:49:04
        /// </summary>
        /// <param name="Token"></param>
        /// <returns></returns>
        [HttpGet]
        [Authenticate]
        public object Sfc00007GetBillTypeList(string Token)
        {
            DataLogerService.writeURL(Token, null);
            return IMBussinessService.Sfc00007GetTypeList(Token);
        }

        /// <summary>
        /// 完工單回報作業-任務單號開窗（状态不为CL和CA）
        /// </summary>
        /// <param name="Token"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <returns></returns>
        [HttpGet]
        [Authenticate]
        public object Sfc00007GetTaskList(string Token, string TaskCode = null, string ItemCode = null, int page = 1, int rows = 10)
        {
            DataLogerService.writeURL(Token, null);
            UtilBussinessService.WriteLog(UtilBussinessService.detoken(Token), "完工單回報作業", null, "查看", null);
            return IMBussinessService.Sfc00007GetTaskList(Token, TaskCode, ItemCode, page, rows);
        }

        /// <summary>
        /// 完工單回報作業-任務單號開窗（状态不为CL和CA）
        /// SAM 2017年10月24日11:20:11
        /// V1版本，加多班别字段显示，然后因为界面并无查询条件，移除查询条件
        /// 只查询 首检为N 或者 首检为Y并且首檢品質判定为Y 的任务单
        /// </summary>
        /// <param name="Token"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <returns></returns>
        [HttpGet]
        [Authenticate]
        public object Sfc00007GetTaskListV1(string Token, int page = 1, int rows = 10)
        {
            DataLogerService.writeURL(Token, null);
            UtilBussinessService.WriteLog(UtilBussinessService.detoken(Token), "完工單回報作業", null, "查看", null);
            return IMBussinessService.Sfc00007GetTaskListV1(Token, page, rows);
        }


        /// <summary>
        /// 完工单拿号
        /// SAM 2017年7月19日17:01:14
        /// </summary>
        /// <param name="Token"></param>
        /// <param name="DTSID"></param>
        /// <param name="Date"></param>
        /// <returns></returns>
        [HttpGet]
        [Authenticate]
        public object Sfc00007GetAutoNumber(string Token, string DTSID, string Date)
        {
            DataLogerService.writeURL(Token, null);
            return IMBussinessService.Sfc00007GetAutoNumber(Token, DTSID, Date);
        }

        /// <summary>
        /// 根据任务单流水号获取下一站制程（工序）
        /// SAM 2017年8月21日14:56:50
        /// </summary>
        /// <param name="Token"></param>
        /// <param name="TaskDispatchID"></param>
        /// <returns></returns>
        [HttpGet]
        [Authenticate]
        public object Sfc00007GetNextList(string Token, string TaskDispatchID)
        {
            DataLogerService.writeURL(Token, null);
            return IMBussinessService.Sfc00007GetNextList(TaskDispatchID);
        }

        /// <summary>
        /// 完工单的新增
        /// SAM 2017年7月20日11:13:45
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Authenticate]
        public object Sfc00007Add(JObject request)
        {
            DataLogerService.writeURL(request.Value<string>("Token"), request.ToString());
            UtilBussinessService.WriteLog(UtilBussinessService.detoken(request.Value<string>("Token")), "保养工单维护", null, "新增", request.ToString());
            return IMBussinessService.Sfc00007Add(request);
        }

        /// <summary>
        /// 完工单的删除
        /// SAM 2017年7月20日11:23:50
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Authenticate]
        public object Sfc00007Delete(JObject request)
        {
            DataLogerService.writeURL(request.Value<string>("Token"), request.ToString());
            return IMBussinessService.Sfc00007Delete(request);
        }

        /// <summary>
        /// 完工单的保存
        /// SAM 2017年7月20日11:14:22
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Authenticate]
        public object Sfc00007Save(JObject request)
        {
            DataLogerService.writeURL(request.Value<string>("Token"), request.ToString());
            UtilBussinessService.WriteLog(UtilBussinessService.detoken(request.Value<string>("Token")), " 完工單回報作業", null, "保存", request.ToString());

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
                Insert = IMBussinessService.Sfc00007insert(request.Value<string>("Token"), request.Value<JArray>("inserted"));
            if (isUpdate)
                Update = IMBussinessService.Sfc00007update(request.Value<string>("Token"), request.Value<JArray>("updated"));

            return new { inserted = Insert, updated = Update, deleted = deleted };
        }

        /// <summary>
        /// 完工單回報作業-資源報工列表
        /// SAM 2017年7月19日17:32:56
        /// </summary>
        /// <param name="Token"></param>
        /// <param name="CompletionOrderID"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <returns></returns>
        [HttpGet]
        [Authenticate]
        public object Sfc00007GetResourceReportingList(string Token, string CompletionOrderID, int page = 1, int rows = 10)
        {
            DataLogerService.writeURL(Token, "CompletionOrderID:" + CompletionOrderID);
            UtilBussinessService.WriteLog(UtilBussinessService.detoken(Token), "完工單回報作業", null, "查看", "CompletionOrderID:" + CompletionOrderID);
            return IMBussinessService.Sfc00007GetResourceReportingList(CompletionOrderID, page, rows);
        }

        /// <summary>
        /// 完工單回報作業-資源報工设备/人工开窗
        /// SAM 2017年7月20日10:34:48
        /// </summary>
        /// <param name="Token"></param>
        /// <param name="TaskDispatchID"></param>
        /// <param name="ResourceType"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <returns></returns>
        [HttpGet]
        [Authenticate]
        public object GetMachineOrManList(string Token, string TaskDispatchID, string ResourceType, int page = 1, int rows = 10)
        {
            DataLogerService.writeURL(Token, "TaskDispatchID" + TaskDispatchID + "-->ResourceType" + ResourceType);
            return IMBussinessService.GetMachineOrManList(TaskDispatchID, ResourceType, page, rows);
        }

        /// <summary>
        /// 完工單回報作業-資源報工保存
        /// SAM 2017年7月19日23:38:07
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Authenticate]
        public object Sfc00007ResourceReportingSave(JObject request)
        {
            DataLogerService.writeURL(request.Value<string>("Token"), request.ToString());
            UtilBussinessService.WriteLog(UtilBussinessService.detoken(request.Value<string>("Token")), " 完工單回報作業", null, "保存", request.ToString());

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
                Insert = IMBussinessService.Sfc00007ResourceReportinginsert(request.Value<string>("Token"), request.Value<JArray>("inserted"));
            if (isUpdate)
                Update = IMBussinessService.Sfc00007ResourceReportingupdate(request.Value<string>("Token"), request.Value<JArray>("updated"));

            return new { inserted = Insert, updated = Update, deleted = deleted };
        }

        /// <summary>
        ///  完工單回報作業-資源報工删除
        ///  SAM 2017年7月19日23:39:50
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Authenticate]
        public object Sfc00007ResourceReportingDelete(JObject request)
        {
            DataLogerService.writeURL(request.Value<string>("Token"), request.ToString());
            return IMBussinessService.Sfc00007ResourceReportingDelete(request);
        }

        /// <summary>
        /// 完工單回報作業-異常數量列表
        /// SAM 2017年7月19日17:50:16
        /// </summary>
        /// <param name="Token"></param>
        /// <param name="CompletionOrderID"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <returns></returns>
        [HttpGet]
        [Authenticate]
        public object Sfc00007GetUnusualQtyList(string Token, string CompletionOrderID, int page = 1, int rows = 10)
        {
            DataLogerService.writeURL(Token, "CompletionOrderID:" + CompletionOrderID);
            UtilBussinessService.WriteLog(UtilBussinessService.detoken(Token), "完工單回報作業", null, "查看", "CompletionOrderID:" + CompletionOrderID);
            return IMBussinessService.Sfc00007GetUnusualQtyList(CompletionOrderID, page, rows);
        }

        /// <summary>
        ///  完工單回報作業-異常數量保存
        ///  SAM 2017年7月19日23:38:27
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Authenticate]
        public object Sfc00007UnusualQtySave(JObject request)
        {
            DataLogerService.writeURL(request.Value<string>("Token"), request.ToString());
            UtilBussinessService.WriteLog(UtilBussinessService.detoken(request.Value<string>("Token")), " 完工單回報作業", null, "保存", request.ToString());

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
                Insert = IMBussinessService.Sfc00007UnusualQtyinsert(request.Value<string>("Token"), request.Value<JArray>("inserted"));
            if (isUpdate)
                Update = IMBussinessService.Sfc00007UnusualQtyupdate(request.Value<string>("Token"), request.Value<JArray>("updated"));

            return new { inserted = Insert, updated = Update, deleted = deleted };
        }

        /// <summary>
        ///  完工單回報作業-異常數量删除
        ///  SAM 2017年7月19日23:39:50
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Authenticate]
        public object Sfc00007UnusualQtyDelete(JObject request)
        {
            DataLogerService.writeURL(request.Value<string>("Token"), request.ToString());
            return IMBussinessService.Sfc00007UnusualQtyDelete(request);
        }

        /// <summary>
        /// 完工單回報作業-無效工時列表
        /// SAM 2017年7月19日17:51:01
        /// </summary>
        /// <param name="Token"></param>
        /// <param name="CompletionOrderID"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <returns></returns>
        [HttpGet]
        [Authenticate]
        public object Sfc00007GetUnusualHourList(string Token, string CompletionOrderID, int page = 1, int rows = 10)
        {
            DataLogerService.writeURL(Token, "CompletionOrderID:" + CompletionOrderID);
            UtilBussinessService.WriteLog(UtilBussinessService.detoken(Token), "完工單回報作業", null, "查看", "CompletionOrderID:" + CompletionOrderID);
            return IMBussinessService.Sfc00007GetUnusualHourList(CompletionOrderID, page, rows);
        }


        /// <summary>
        /// 完工單回報作業-無效工時保存
        /// SAM 2017年7月19日23:38:57
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Authenticate]
        public object Sfc00007UnusualHourSave(JObject request)
        {
            DataLogerService.writeURL(request.Value<string>("Token"), request.ToString());
            UtilBussinessService.WriteLog(UtilBussinessService.detoken(request.Value<string>("Token")), " 完工單回報作業", null, "保存", request.ToString());

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
                Insert = IMBussinessService.Sfc00007UnusualHourinsert(request.Value<string>("Token"), request.Value<JArray>("inserted"));
            if (isUpdate)
                Update = IMBussinessService.Sfc00007UnusualHourupdate(request.Value<string>("Token"), request.Value<JArray>("updated"));

            return new { inserted = Insert, updated = Update, deleted = deleted };
        }

        /// <summary>
        ///  完工單回報作業-無效工時删除
        ///  SAM 2017年7月19日23:39:50
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Authenticate]
        public object Sfc00007UnusualHourDelete(JObject request)
        {
            DataLogerService.writeURL(request.Value<string>("Token"), request.ToString());
            return IMBussinessService.Sfc00007UnusualHourDelete(request);
        }

        /// <summary>
        /// 完工單回報作業-批號列表
        /// SAM 2017年7月19日18:02:01
        /// </summary>
        /// <param name="Token"></param>
        /// <param name="CompletionOrderID"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <returns></returns>
        [HttpGet]
        [Authenticate]
        public object Sfc00007GetLotList(string Token, string CompletionOrderID, int page = 1, int rows = 10)
        {
            DataLogerService.writeURL(Token, "CompletionOrderID:" + CompletionOrderID);
            UtilBussinessService.WriteLog(UtilBussinessService.detoken(Token), "完工單回報作業", null, "查看", "CompletionOrderID:" + CompletionOrderID);
            return IMBussinessService.Sfc00007GetLotList(CompletionOrderID, page, rows);
        }

        /// <summary>
        /// 完工單回報作業-批號屬性—取號开窗
        /// SAM 2017年7月20日14:28:46
        /// </summary>
        /// <param name="Token"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <returns></returns>
        [HttpGet]
        [Authenticate]
        public object Sfc00007GetLotAutoNumber(string Token, int page = 1, int rows = 10)
        {
            DataLogerService.writeURL(Token, null);
            UtilBussinessService.WriteLog(UtilBussinessService.detoken(Token), "完工單回報作業", null, "查看", null);
            return IMBussinessService.Sfc00007GetLotAutoNumber(Token, page, rows);
        }

        /// <summary>
        /// 完工單回報作業-批號保存
        /// SAM 2017年7月19日23:39:14
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Authenticate]
        public object Sfc00007LotSave(JObject request)
        {
            DataLogerService.writeURL(request.Value<string>("Token"), request.ToString());
            UtilBussinessService.WriteLog(UtilBussinessService.detoken(request.Value<string>("Token")), " 完工單回報作業", null, "保存", request.ToString());

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
                Insert = IMBussinessService.Sfc00007Lotinsert(request.Value<string>("Token"), request.Value<JArray>("inserted"));
            if (isUpdate)
                Update = IMBussinessService.Sfc00007Lotupdate(request.Value<string>("Token"), request.Value<JArray>("updated"));

            return new { inserted = Insert, updated = Update, deleted = deleted };
        }

        /// <summary>
        ///  完工單回報作業-批號删除
        ///  SAM 2017年7月19日23:39:50
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Authenticate]
        public object Sfc00007LotDelete(JObject request)
        {
            DataLogerService.writeURL(request.Value<string>("Token"), request.ToString());
            return IMBussinessService.Sfc00007LotDelete(request);
        }

        /// <summary>
        /// 完工單回報作業-批號-屬性列表
        /// SAM 2017年7月20日00:42:41
        /// </summary>
        /// <param name="Token"></param>
        /// <param name="BatchAttributeID"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <returns></returns>
        [HttpGet]
        [Authenticate]
        public object Sfc00007GetLotAttributeList(string Token, string BatchAttributeID, int page = 1, int rows = 10)
        {
            DataLogerService.writeURL(Token, "CompletionOrderID:" + BatchAttributeID);
            UtilBussinessService.WriteLog(UtilBussinessService.detoken(Token), "完工單回報作業", null, "查看", "BatchAttributeID:" + BatchAttributeID);
            return IMBussinessService.Sfc00007GetLotAttributeList(Token, BatchAttributeID, page, rows);
        }

        /// <summary>
        /// 根据料品属性获取料品资料值
        /// SAM 2017年7月20日15:32:23
        /// </summary>
        /// <param name="Token"></param>
        /// <param name="AttributeID"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <returns></returns>
        [HttpGet]
        [Authenticate]
        public object Sfc00007GetAttributeLList(string Token, string AttributeID, int page = 1, int rows = 10)
        {
            DataLogerService.writeURL(Token, null);
            UtilBussinessService.WriteLog(UtilBussinessService.detoken(Token), "完工單回報作業", null, "查看", null);
            return IMBussinessService.Sfc00007GetAttributeLList(Token, AttributeID, page, rows);
        }

        /// <summary>
        /// 完工單回報作業-批號-屬性保存
        /// SAM 2017年7月20日00:43:44
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Authenticate]
        public object Sfc00007LotAttributeSave(JObject request)
        {
            DataLogerService.writeURL(request.Value<string>("Token"), request.ToString());
            UtilBussinessService.WriteLog(UtilBussinessService.detoken(request.Value<string>("Token")), " 完工單回報作業", null, "保存", request.ToString());

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
                Update = IMBussinessService.Sfc00007LotAttributeupdate(request.Value<string>("Token"), request.Value<JArray>("updated"));

            return new { inserted = Insert, updated = Update, deleted = deleted };
        }

        /// <summary>
        /// 完工單回報作業-批號屬性—檢查批號
        /// SAM 2017年7月21日09:59:40
        /// </summary>
        /// <param name="Token"></param>
        /// <param name="TaskID"></param>
        /// <param name="LotNo"></param>
        /// <returns></returns>
        [HttpGet]
        [Authenticate]
        public object Sfc00007LotNoChecks(string Token, string TaskID, string LotNo)
        {
            DataLogerService.writeURL(Token, TaskID + "-" + LotNo);
            UtilBussinessService.WriteLog(UtilBussinessService.detoken(Token), "完工單回報作業", null, "檢查", null);
            return IMBussinessService.Sfc00007LotNoChecks(Token, TaskID, LotNo);
        }

        /// <summary>
        /// 完工單回報作業-完工單—確認按鍵
        /// SAM 2017年7月21日10:14:11
        /// TODO 逻辑过难
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Authenticate]
        public object Sfc00007CompeletedConfirm(JObject request)
        {
            DataLogerService.writeURL(request.Value<string>("Token"), request.ToString());
            UtilBussinessService.WriteLog(UtilBussinessService.detoken(request.Value<string>("Token")), "完工單回報作業", null, "確認", request.ToString());
            return IMBussinessService.Sfc00007CompeletedConfirm(request);
        }

        /// <summary>
        /// 判断报废量，差异量，返修量是否可编辑
        /// SAM 2017年7月26日16:39:21
        /// true代表可编辑 
        /// </summary>
        /// <param name="Token"></param>
        /// <param name="CompletionOrderID"></param>
        /// <returns></returns>
        [HttpGet]
        [Authenticate]
        public object Sfc00007CheckQuantity(string Token, string CompletionOrderID = null)
        {
            DataLogerService.writeURL(Token, CompletionOrderID);
            UtilBussinessService.WriteLog(UtilBussinessService.detoken(Token), "完工單回報作業", null, "檢查", null);
            return IMBussinessService.Sfc00007CheckQuantity(Token, CompletionOrderID);
        }

        /// <summary>
        /// 完工单根据料品获取批号
        /// SAM 2017年7月28日10:56:35
        /// </summary>
        /// <param name="Token"></param>
        /// <param name="ItemID"></param>
        /// <returns></returns>
        [HttpGet]
        [Authenticate]
        public object Sfc00007GetLotNumber(string Token, string ItemID)
        {
            DataLogerService.writeURL(Token, ItemID);
            return IMBussinessService.Sfc00007GetLotNumber(Token, ItemID);
        }

        /// <summary>
        /// 判断累計報工量(數量)是否任務單分派量
        /// SAM 2017年8月23日18:34:10
        /// </summary>
        /// <param name="Token"></param>
        /// <param name="TaskDispatchID"></param>
        /// <param name="FinProQuantity"></param>
        /// <returns></returns>
        [HttpGet]
        [Authenticate]
        public bool Sfc00007CheckFinProQuantity(string Token, string TaskDispatchID, string FinProQuantity)
        {
            DataLogerService.writeURL(Token, null);
            return IMBussinessService.Sfc00007CheckFinProQuantity(Token, TaskDispatchID, FinProQuantity);
        }

        /// <summary>
        /// 根据完工单流水号获取资源报工工时
        /// SAM 2017年9月13日14:50:25
        /// </summary>
        /// <param name="Token"></param>
        /// <param name="CompletionOrderID"></param>
        /// <returns></returns>
        [HttpGet]
        [Authenticate]
        public string Sfc00007GetReportingHour(string Token, string CompletionOrderID)
        {
            DataLogerService.writeURL(Token, null);
            return IMBussinessService.Sfc00007GetReportingHour(Token, CompletionOrderID);
        }


        #endregion

        #region SFC00008完工调整作业
        /// <summary>
        /// 查询完工调整作业表
        /// SAM 2017年7月20日16:19:02
        /// </summary>
        /// <param name="Token"></param>
        /// <param name="AdjustCode"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <returns></returns>
        [HttpGet]
        [Authenticate]
        public object Sfc00008GetList(string Token, string AdjustCode = null, int page = 1, int rows = 10)
        {
            DataLogerService.writeURL(Token, "AdjustCode:" + AdjustCode);
            UtilBussinessService.WriteLog(UtilBussinessService.detoken(Token), "完工调整作業", null, "查看", "AdjustCode:" + AdjustCode);
            return IMBussinessService.SFC00008GetList(Token, AdjustCode, page, rows);
        }

        /// <summary>
        /// 调整单获取单据类别列表
        /// SAM 2017年8月1日11:58:21
        /// </summary>
        /// <param name="Token"></param>
        /// <returns></returns>
        [HttpGet]
        [Authenticate]
        public object Sfc00008GetTypeList(string Token)
        {
            return IMBussinessService.Sfc00008GetTypeList(Token);
        }

        /// <summary>
        /// 调整单拿号
        /// SAM 2017年8月1日12:00:55
        /// </summary>
        /// <param name="Token"></param>
        /// <param name="DTSID"></param>
        /// <param name="Date"></param>
        /// <returns></returns>
        [HttpGet]
        [Authenticate]
        public object Sfc00008GetAutoNumber(string Token, string DTSID, string Date)
        {
            DataLogerService.writeURL(Token, null);
            return IMBussinessService.Sfc00008GetAutoNumber(Token, DTSID, Date);
        }

        /// <summary>
        /// 原完工单号的开窗查询
        /// SAM 2017年7月20日16:28:27
        /// </summary>
        /// <param name="Token"></param>
        /// <param name="OldCompletedNo"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <returns></returns>
        [HttpGet]
        [Authenticate]
        public object Sfc00008GetOldCompletedNo(string Token, string OldCompletedNo = null, int page = 1, int rows = 10)
        {
            DataLogerService.writeURL(Token, "OldCompletedNo:" + OldCompletedNo);
            UtilBussinessService.WriteLog(UtilBussinessService.detoken(Token), "完工调整作業", null, "查看", "OldCompletedNo:" + OldCompletedNo);
            return IMBussinessService.Sfc00008GetOldCompletedNo(Token, OldCompletedNo, page, rows);
        }

        /// <summary>
        /// 调整单的新增
        /// SAM 2017年7月20日17:46:44
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Authenticate]
        public object Sfc00008Add(JObject request)
        {
            DataLogerService.writeURL(request.Value<string>("Token"), request.ToString());
            UtilBussinessService.WriteLog(UtilBussinessService.detoken(request.Value<string>("Token")), "完工调整作業", null, "新增", request.ToString());
            return IMBussinessService.Sfc00008Add(request);
        }

        /// <summary>
        /// 调整单的确认
        /// SAM 2017年7月25日15:59:40
        /// TODO 逻辑未完成
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Authenticate]
        public object Sfc00008Confirm(JObject request)
        {
            DataLogerService.writeURL(request.Value<string>("Token"), request.ToString());
            UtilBussinessService.WriteLog(UtilBussinessService.detoken(request.Value<string>("Token")), "完工调整作業", null, "新增", request.ToString());
            return IMBussinessService.Sfc00008Confirm(request);
        }

        /// <summary>
        /// 调整单的保存
        /// SAM 2017年7月26日18:24:23
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Authenticate]
        public object Sfc00008Save(JObject request)
        {
            DataLogerService.writeURL(request.Value<string>("Token"), request.ToString());
            UtilBussinessService.WriteLog(UtilBussinessService.detoken(request.Value<string>("Token")), " 完工调整作業", null, "保存", request.ToString());

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
                Update = IMBussinessService.Sfc00008update(request.Value<string>("Token"), request.Value<JArray>("updated"));

            return new { inserted = Insert, updated = Update, deleted = deleted };
        }

        /// <summary>
        /// 调整单的删除
        /// SAM 2017年7月26日18:28:51
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Authenticate]
        public object Sfc00008Delete(JObject request)
        {
            DataLogerService.writeURL(request.Value<string>("Token"), request.ToString());
            return IMBussinessService.Sfc00007Delete(request);
        }
        #endregion

        #region SFC00010制程完工状况分析（制令）
        /// <summary>
        /// 制程完工状况分析（制令）-主列表
        /// SAM 2017年7月23日01:07:56
        /// </summary>
        /// <param name="Token"></param>
        /// <param name="StartWorkCenterCode"></param>
        /// <param name="EndWorkCenterCode"></param>
        /// <param name="StartFabMoCode"></param>
        /// <param name="EndFabMoCode"></param>
        /// <param name="StartDate"></param>
        /// <param name="EndDate"></param>
        /// <param name="StartCustCode"></param>
        /// <param name="EndCustCode"></param>
        /// <param name="StartMESUserCode"></param>
        /// <param name="EndMESUserCode"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <returns></returns>
        [HttpGet]
        [Authenticate]
        public object Sfc00010GetList(string Token, string StartWorkCenterCode = null, string EndWorkCenterCode = null,
           string StartFabMoCode = null, string EndFabMoCode = null,
           string StartDate = null, string EndDate = null,
           string StartCustCode = null, string EndCustCode = null,
           string StartMESUserCode = null, string EndMESUserCode = null,
            int page = 1, int rows = 10)
        {
            DataLogerService.writeURL(Token, null);
            UtilBussinessService.WriteLog(UtilBussinessService.detoken(Token), "制程完工状况分析（制令）", null, "查看", null);
            return IMBussinessService.Sfc00010GetList(Token, StartWorkCenterCode, EndWorkCenterCode,
                 StartFabMoCode, EndFabMoCode, StartDate, EndDate,
                  StartCustCode, EndCustCode, StartMESUserCode, EndMESUserCode, page, rows);
        }

        /// <summary>
        /// 制程完工状况分析（制令）-制程列表
        /// SAM 2017年7月23日01:28:01
        /// </summary>
        /// <param name="Token"></param>
        /// <param name="FabricatedMotherID"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <returns></returns>
        [HttpGet]
        [Authenticate]
        public object Sfc00010GetProcessList(string Token, string FabricatedMotherID, int page = 1, int rows = 10)
        {
            DataLogerService.writeURL(Token, null);
            UtilBussinessService.WriteLog(UtilBussinessService.detoken(Token), "制程完工状况分析", null, "查看", null);
            return IMBussinessService.Sfc00010GetProcessList(Token, FabricatedMotherID, page, rows);
        }

        /// <summary>
        /// 制程完工状况分析（制令）-工序列表
        /// SAM 2017年7月23日01:32:18
        /// </summary>
        /// <param name="Token"></param>
        /// <param name="FabMoProcessID"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <returns></returns>
        [HttpGet]
        [Authenticate]
        public object Sfc00010GetOperationList(string Token, string FabMoProcessID, int page = 1, int rows = 10)
        {
            DataLogerService.writeURL(Token, null);
            UtilBussinessService.WriteLog(UtilBussinessService.detoken(Token), "制程完工状况分析", null, "查看", null);
            return IMBussinessService.Sfc00011GetOperationList(Token, FabMoProcessID, page, rows);
        }


        #endregion

        #region SFC00011制程完工状况分析（工作中心）
        /// <summary>
        /// 制程完工状况分析-主列表
        /// SAM 2017年7月23日00:45:23
        /// </summary>
        /// <param name="Token"></param>
        /// <param name="StartWorkCenterCode"></param>
        /// <param name="EndWorkCenterCode"></param>
        /// <param name="StartFabMoCode"></param>
        /// <param name="EndFabMoCode"></param>
        /// <param name="StartDate"></param>
        /// <param name="EndDate"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <returns></returns>
        [HttpGet]
        [Authenticate]
        public object Sfc00011GetList(string Token, string StartWorkCenterCode = null, string EndWorkCenterCode = null,
           string StartFabMoCode = null, string EndFabMoCode = null,
           string StartDate = null, string EndDate = null, int page = 1, int rows = 10)
        {
            DataLogerService.writeURL(Token, null);
            UtilBussinessService.WriteLog(UtilBussinessService.detoken(Token), "制程完工状况分析", null, "查看", null);
            return IMBussinessService.Sfc00011GetList(Token,
                StartWorkCenterCode, EndWorkCenterCode, StartFabMoCode, EndFabMoCode, StartDate, EndDate, page, rows);
        }

        /// <summary>
        /// 制程完工状况分析-主列表
        /// SAM 2017年7月23日00:45:23
        /// 需求新增，加两个字段
        /// Mouse 2017年11月15日17:58:43
        /// </summary>
        /// <param name="Token"></param>
        /// <param name="StartWorkCenterCode"></param>
        /// <param name="EndWorkCenterCode"></param>
        /// <param name="StartFabMoCode"></param>
        /// <param name="EndFabMoCode"></param>
        /// <param name="StartDate"></param>
        /// <param name="EndDate"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <returns></returns>
        [HttpGet]
        [Authenticate]
        public object Sfc00011GetListV1(string Token, string StartWorkCenterCode = null, string EndWorkCenterCode = null,
           string StartFabMoCode = null, string EndFabMoCode = null,
           string StartDate = null, string EndDate = null, int page = 1, int rows = 10)
        {
            DataLogerService.writeURL(Token, null);
            UtilBussinessService.WriteLog(UtilBussinessService.detoken(Token), "制程完工状况分析", null, "查看", null);
            return IMBussinessService.Sfc00011GetListV1(Token,
                StartWorkCenterCode, EndWorkCenterCode, StartFabMoCode, EndFabMoCode, StartDate, EndDate, page, rows);
        }


        /// <summary>
        /// 制程完工状况分析-工序列表
        /// SAM 2017年7月23日00:58:55
        /// </summary>
        /// <param name="Token"></param>
        /// <param name="FabMoProcessID"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <returns></returns>
        [HttpGet]
        [Authenticate]
        public object Sfc00011GetOperationList(string Token, string FabMoProcessID, int page = 1, int rows = 10)
        {
            DataLogerService.writeURL(Token, null);
            UtilBussinessService.WriteLog(UtilBussinessService.detoken(Token), "制程完工状况分析", null, "查看", null);
            return IMBussinessService.Sfc00011GetOperationList(Token, FabMoProcessID, page, rows);
        }


        #endregion

        #region SFC00012制程工时分析
        /// <summary>
        /// 制程工时分析-主列表
        /// SAM 2017年7月23日00:15:29
        /// </summary>
        /// <param name="Token"></param>
        /// <param name="StartProcessCode"></param>
        /// <param name="EndProcessCode"></param>
        /// <param name="StartWorkCenterCode"></param>
        /// <param name="EndWorkCenterCode"></param>
        /// <param name="StartFabMoCode"></param>
        /// <param name="EndFabMoCode"></param>
        /// <param name="StartItemCode"></param>
        /// <param name="EndItemCode"></param>
        /// <param name="StartDate"></param>
        /// <param name="EndDate"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <returns></returns>
        [HttpGet]
        [Authenticate]
        public object Sfc00012GetList(string Token, string StartProcessCode = null, string EndProcessCode = null,
            string StartWorkCenterCode = null, string EndWorkCenterCode = null,
            string StartFabMoCode = null, string EndFabMoCode = null,
            string StartItemCode = null, string EndItemCode = null,
            string StartDate = null, string EndDate = null, int page = 1, int rows = 10)
        {
            DataLogerService.writeURL(Token, null);
            UtilBussinessService.WriteLog(UtilBussinessService.detoken(Token), "制程工时分析", null, "查看", null);
            return IMBussinessService.Sfc00012GetList(Token, StartProcessCode, EndProcessCode,
                StartWorkCenterCode, EndWorkCenterCode,
                 StartFabMoCode, EndFabMoCode,
                StartItemCode, EndItemCode, StartDate, EndDate, page, rows);
        }

        /// <summary>
        /// 制程工时分析-工序列表
        /// SAM 2017年7月23日01:48:11
        /// </summary>
        /// <param name="Token"></param>
        /// <param name="FabMoProcessID"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <returns></returns>
        [HttpGet]
        [Authenticate]
        public object Sfc00012GetOperationList(string Token, string FabMoProcessID, int page = 1, int rows = 10)
        {
            DataLogerService.writeURL(Token, null);
            UtilBussinessService.WriteLog(UtilBussinessService.detoken(Token), "制程完工状况分析", null, "查看", null);
            return IMBussinessService.Sfc00012GetOperationList(Token, FabMoProcessID, page, rows);
        }


        #endregion

        #region SFC00013制程完工异常分析
        /// <summary>
        /// 制程完工异常分析-主列表
        /// SAM 2017年7月9日23:15:02
        /// </summary>
        /// <param name="Token"></param>
        /// <param name="StartProcessCode">起始制程代号</param>
        /// <param name="EndProcessCode">结束制程代号</param>
        /// <param name="StartItemCode">起始料品代号</param>
        /// <param name="EndItemCode">结束料品代号</param>
        /// <param name="StartDate">起始完工日期</param>
        /// <param name="EndDate">结束完工日期</param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <returns></returns>
        [HttpGet]
        [Authenticate]
        public object Sfc00013GetList(string Token, string StartProcessCode = null, string EndProcessCode = null,
            string StartItemCode = null, string EndItemCode = null,
            string StartDate = null, string EndDate = null, int page = 1, int rows = 10)
        {
            DataLogerService.writeURL(Token, null);
            UtilBussinessService.WriteLog(UtilBussinessService.detoken(Token), "制程完工异常分析", null, "查看", null);
            return IMBussinessService.Sfc00013GetList(Token, StartProcessCode, EndProcessCode, StartItemCode, EndItemCode, StartDate, EndDate, page, rows);
        }

        /// <summary>
        /// 制程完工异常分析-異常明細(根据料品流水号+制程流水号)
        /// SAM 2017年7月9日23:35:53
        /// </summary>
        /// <param name="Token"></param>
        /// <param name="ItemID"></param>
        /// <param name="ProcessID"></param>
        /// <param name="StartDate"></param>
        /// <param name="EndDate"></param>
        /// <returns></returns>
        [HttpGet]
        [Authenticate]
        public IList<Hashtable> Sfc00013GetDetailList(string Token, string ItemID, string ProcessID, string StartDate = null, string EndDate = null)
        {
            DataLogerService.writeURL(Token, null);
            UtilBussinessService.WriteLog(UtilBussinessService.detoken(Token), "制程完工异常分析", null, "查看", null);
            return IMBussinessService.Sfc00013GetDetailList(Token, ItemID, ProcessID, StartDate, EndDate);
        }
        #endregion

        #region SFC00014人工时统计分析
        /// <summary>
        /// 人工时统计分析列表
        /// SAM 2017年7月9日23:22:16
        /// </summary>
        /// <param name="Token"></param>
        /// <param name="StartWorkCenterCode">起始工作中心代号</param>
        /// <param name="EndWorkCenterCode">结束工作中心代号</param>
        /// <param name="StartUserCode">起始员工代号</param>
        /// <param name="EndUserCode">结束员工代号</param>
        /// <param name="StartDate">起始完工日期</param>
        /// <param name="EndDate">结束完工日期</param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <returns></returns>
        [HttpGet]
        [Authenticate]
        public object Sfc00014GetList(string Token,
            string StartWorkCenterCode = null, string EndWorkCenterCode = null,
           string StartUserCode = null, string EndUserCode = null,
           string StartDate = null, string EndDate = null, int page = 1, int rows = 10)
        {
            DataLogerService.writeURL(Token, null);
            UtilBussinessService.WriteLog(UtilBussinessService.detoken(Token), "人工时统计分析", null, "查看", null);
            return IMBussinessService.Sfc00014GetList(Token, StartWorkCenterCode, EndWorkCenterCode, StartUserCode, EndUserCode, StartDate, EndDate, page, rows);
        }

        #endregion

        #region SFC00015机器工时统计分析
        /// <summary>
        /// 机器工时统计分析
        /// SAM 2017年7月9日23:25:01
        /// </summary>
        /// <param name="Token"></param>
        /// <param name="StartWorkCenterCode">起始工作中心代号</param>
        /// <param name="EndWorkCenterCode">结束工作中心代号</param>
        /// <param name="StartEquipmentCode">开始设备代号</param>
        /// <param name="EndEquipmentCode">结束设备代号</param>
        /// <param name="StartDate">起始完工日期</param>
        /// <param name="EndDate">结束完工日期</param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <returns></returns>
        [HttpGet]
        [Authenticate]
        public object Sfc00015GetList(string Token, string StartWorkCenterCode = null, string EndWorkCenterCode = null,
          string StartEquipmentCode = null, string EndEquipmentCode = null,
          string StartDate = null, string EndDate = null, int page = 1, int rows = 10)
        {
            DataLogerService.writeURL(Token, null);
            UtilBussinessService.WriteLog(UtilBussinessService.detoken(Token), "制程完工异常分析", null, "查看", null);
            return IMBussinessService.Sfc00015GetList(Token, StartWorkCenterCode, EndWorkCenterCode, StartEquipmentCode, EndEquipmentCode, StartDate, EndDate, page, rows);
        }
        #endregion

        #region SFC00016无效工时原因分析
        /// <summary>
        /// 无效工时原因分析
        /// SAM 2017年7月9日23:28:25
        /// </summary>
        /// <param name="Token"></param>
        /// <param name="StartWorkCenterCode">起始工作中心代号</param>
        /// <param name="EndWorkCenterCode">结束工作中心代号</param>
        /// <param name="StartDate">起始完工日期</param>
        /// <param name="EndDate">结束完工日期</param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <returns></returns>
        [HttpGet]
        [Authenticate]
        public object Sfc00016GetList(string Token, string StartWorkCenterCode = null, string EndWorkCenterCode = null,
          string StartDate = null, string EndDate = null, int page = 1, int rows = 10)
        {
            DataLogerService.writeURL(Token, null);
            UtilBussinessService.WriteLog(UtilBussinessService.detoken(Token), "制程完工异常分析", null, "查看", null);
            return IMBussinessService.Sfc00016GetList(Token, StartWorkCenterCode, EndWorkCenterCode, StartDate, EndDate, page, rows);
        }

        /// <summary>
        /// 根据工作中心获取人工無效原因明細
        /// SAM 2017年7月9日23:32:37
        /// </summary>
        /// <param name="Token"></param>
        /// <param name="WorkCenterID"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <returns></returns>
        [HttpGet]
        [Authenticate]
        public object Sfc00016GetLDetailList(string Token, string WorkCenterID, int page = 1, int rows = 10)
        {
            DataLogerService.writeURL(Token, null);
            UtilBussinessService.WriteLog(UtilBussinessService.detoken(Token), "无效工时原因分析", null, "查看", null);
            return IMBussinessService.Sfc00016GetLDetailList(Token, WorkCenterID, page, rows);
        }

        /// <summary>
        /// 据工作中心获取机器無效原因明細
        /// SAM 2017年7月9日23:33:00
        /// </summary>
        /// <param name="Token"></param>
        /// <param name="WorkCenterID"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <returns></returns>
        [HttpGet]
        [Authenticate]
        public object Sfc00016GetMDetailList(string Token, string WorkCenterID, int page = 1, int rows = 10)
        {
            DataLogerService.writeURL(Token, null);
            UtilBussinessService.WriteLog(UtilBussinessService.detoken(Token), "无效工时原因分析", null, "查看", null);
            return IMBussinessService.Sfc00016GetMDetailList(Token, WorkCenterID, page, rows);
        }
        #endregion

        #region SFC00017制令直通率分析
        /// <summary>
        /// 製令直通率分析
        /// SAM 2017年9月3日21:23:18
        /// </summary>
        /// <param name="Token"></param>
        /// <param name="StartFabMoCode"></param>
        /// <param name="EndFabMoCode"></param>
        /// <param name="StartItemCode"></param>
        /// <param name="EndItemCode"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <returns></returns>
        [HttpGet]
        [Authenticate]
        public object Sfc00017GetList(string Token, string StartFabMoCode = null, string EndFabMoCode = null,
        string StartItemCode = null, string EndItemCode = null, int page = 1, int rows = 10)
        {
            DataLogerService.writeURL(Token, null);
            UtilBussinessService.WriteLog(UtilBussinessService.detoken(Token), "制令直通率分析", null, "查看", null);
            return IMBussinessService.Sfc00017GetList(Token, StartFabMoCode, EndFabMoCode, StartItemCode, EndItemCode, page, rows);
        }

        /// <summary>
        /// 制令直通率分析-制程明细列表
        /// SAM 2017年9月3日21:24:53
        /// </summary>
        /// <param name="Token"></param>
        /// <param name="FabricatedMotherID"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <returns></returns>
        [HttpGet]
        [Authenticate]
        public object Sfc00017GetDetailList(string Token, string FabricatedMotherID, int page = 1, int rows = 10)
        {
            DataLogerService.writeURL(Token, null);
            UtilBussinessService.WriteLog(UtilBussinessService.detoken(Token), "制令直通率分析-制程明细", null, "查看", null);
            return IMBussinessService.Sfc00017GetDetailList(Token, FabricatedMotherID, page, rows);
        }
        #endregion

        #region SFC00018制令用料耗用分析

        /// <summary>
        /// 制令用料耗用分析
        /// SAM 2017年9月3日21:34:45
        /// </summary>
        /// <param name="Token"></param>
        /// <param name="StartFabMoCode"></param>
        /// <param name="EndFabMoCode"></param>
        /// <param name="StartDate"></param>
        /// <param name="EndDate"></param>
        /// <param name="Status"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <returns></returns>
        [HttpGet]
        [Authenticate]
        public object Sfc00018GetList(string Token, string StartFabMoCode = null, string EndFabMoCode = null,
        string StartDate = null, string EndDate = null, string Status = null, int page = 1, int rows = 10)
        {
            DataLogerService.writeURL(Token, null);
            UtilBussinessService.WriteLog(UtilBussinessService.detoken(Token), "制令用料耗用分析", null, "查看", null);
            return IMBussinessService.Sfc00018GetList(Token, StartFabMoCode, EndFabMoCode, StartDate, EndDate, Status, page, rows);
        }
        #endregion

        #region SFC00019制品生产工时分析
        /// <summary>
        /// 製品生產工時分析-制品页签
        /// SAM 2017年9月3日22:11:30
        /// </summary>
        /// <param name="Token"></param>
        /// <param name="StartItemCode"></param>
        /// <param name="EndItemCode"></param>
        /// <param name="StartFabMoCode"></param>
        /// <param name="EndFabMoCode"></param>
        /// <param name="StartDate"></param>
        /// <param name="EndDate"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <returns></returns>
        [HttpGet]
        [Authenticate]
        public object Sfc00019ItemGetList(string Token,
             string StartItemCode = null, string EndItemCode = null,
            string StartFabMoCode = null, string EndFabMoCode = null,
       string StartDate = null, string EndDate = null, int page = 1, int rows = 10)
        {
            DataLogerService.writeURL(Token, null);
            UtilBussinessService.WriteLog(UtilBussinessService.detoken(Token), "制令用料耗用分析", null, "查看", null);
            return IMBussinessService.Sfc00019ItemGetList(Token, StartItemCode, EndItemCode, StartFabMoCode, EndFabMoCode, StartDate, EndDate, page, rows);
        }

        /// <summary>
        /// 製品生產工時分析-工作中心页签
        /// SAM 2017年9月3日22:11:52
        /// </summary>
        /// <param name="Token"></param>
        /// <param name="StartWorkCenterCode"></param>
        /// <param name="EndWorkCenterCode"></param>
        /// <param name="StartItemCode"></param>
        /// <param name="EndItemCode"></param>
        /// <param name="StartFabMoCode"></param>
        /// <param name="EndFabMoCode"></param>
        /// <param name="StartDate"></param>
        /// <param name="EndDate"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <returns></returns>
        [HttpGet]
        [Authenticate]
        public object Sfc00019WorkCenterGetList(string Token,
            string StartWorkCenterCode = null, string EndWorkCenterCode = null,
             string StartItemCode = null, string EndItemCode = null,
            string StartFabMoCode = null, string EndFabMoCode = null,
       string StartDate = null, string EndDate = null, int page = 1, int rows = 10)
        {
            DataLogerService.writeURL(Token, null);
            UtilBussinessService.WriteLog(UtilBussinessService.detoken(Token), "制令用料耗用分析", null, "查看", null);
            return IMBussinessService.Sfc00019WorkCenterGetList(Token, StartWorkCenterCode, EndWorkCenterCode,
                StartWorkCenterCode, EndWorkCenterCode,
                StartFabMoCode, EndFabMoCode, StartDate, EndDate, page, rows);
        }
        #endregion

        #region SFC00020制令生产计划表
        /// <summary>
        /// SFC20列表
        /// Mouse 2017-9-26 15:22:54
        /// </summary>
        /// <param name="Token"></param>
        /// <param name="MoNoStar"></param>
        /// <param name="MoNoEnd"></param>
        /// <param name="StartDate"></param>
        /// <param name="FinishDate"></param>
        /// <param name="Status"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <returns></returns>
        [HttpGet]
        [Authenticate]
        public object sfc00020GetList(string Token, string MoNoStar = null, string MoNoEnd = null, string StartDate = null, string FinishDate = null, string Status = null, int page = 1, int rows = 10)
        {
            DataLogerService.writeURL(Token, null);
            UtilBussinessService.WriteLog(UtilBussinessService.detoken(Token), "制令生产计划表", null, "查看", null);
            return IMBussinessService.sfc00020GetList(Token, MoNoStar, MoNoEnd, StartDate, FinishDate, Status, page, rows);
        }
        #endregion

        #region SFC00021製程生產計畫差異
        /// <summary>
        /// 製程生產計畫差異-列表
        /// Sam 2017年9月28日10:13:45
        /// </summary>
        /// <param name="Token"></param>
        /// <param name="StartProcessCode">起始製程代號</param>
        /// <param name="EndProcessCode">結束製程代號</param>
        /// <param name="StartDate">起始製令製程開工日</param>
        /// <param name="EndDate">結束製令製程開工日</param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <returns></returns>
        [HttpGet]
        [Authenticate]
        public object Sfc00021GetList(string Token, string StartProcessCode = null, string EndProcessCode = null,
            string StartDate = null, string EndDate = null, int page = 1, int rows = 10)
        {
            DataLogerService.writeURL(Token, null);
            UtilBussinessService.WriteLog(UtilBussinessService.detoken(Token), "製程生產計畫差異", null, "查看", null);
            return IMBussinessService.Sfc00021GetList(Token, StartProcessCode, EndProcessCode, StartDate, EndDate, page, rows);
        }
        #endregion

        #region SFC000022制品不良统计分析
        /// <summary>
        /// 制品不良统计分析-原因统计页签列表
        /// SAM 2017年10月10日16:01:35
        /// </summary>
        /// <param name="Token"></param>
        /// <param name="StartItemCode">起始料号</param>
        /// <param name="EndItemCode">结束料号</param>
        /// <param name="StartDate">起始日期</param>
        /// <param name="EndDate">结束日期</param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <returns></returns>
        [HttpGet]
        [Authenticate]
        public object Sfc00022GetReasonList(string Token, string StartItemCode = null, string EndItemCode = null,
            string StartDate = null, string EndDate = null, int page = 1, int rows = 10)
        {
            DataLogerService.writeURL(Token, null);
            UtilBussinessService.WriteLog(UtilBussinessService.detoken(Token), "制品不良统计分析", null, "查看", null);
            return IMBussinessService.Sfc00022GetReasonList(Token, StartItemCode, EndItemCode, StartDate, EndDate, page, rows);
        }

        /// <summary>
        /// 制品不良统计分析-原因统计页签-明细列表
        /// SAM 2017年10月11日09:29:59
        /// </summary>
        /// <param name="Token"></param>
        /// <param name="ItemID">料号流水号</param>
        /// <returns></returns>
        [HttpGet]
        [Authenticate]
        public object Sfc00022GetReasonDetailList(string Token, string ItemID)
        {
            DataLogerService.writeURL(Token, null);
            UtilBussinessService.WriteLog(UtilBussinessService.detoken(Token), "制品不良统计分析", null, "查看", null);
            return IMBussinessService.Sfc00022GetReasonDetailList(Token, ItemID);
        }

        /// <summary>
        /// 制品不良统计分析-料品統計页签列表
        /// Sam 2017年10月11日09:56:19
        /// </summary>
        /// <param name="Token"></param>
        /// <param name="StartItemCode">起始料号</param>
        /// <param name="EndItemCode">结束料号</param>
        /// <param name="StartDate">起始日期</param>
        /// <param name="EndDate">结束日期</param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <returns></returns>
        [HttpGet]
        [Authenticate]
        public object Sfc00022GetItemList(string Token, string StartItemCode = null, string EndItemCode = null,
          string StartDate = null, string EndDate = null, int page = 1, int rows = 10)
        {
            DataLogerService.writeURL(Token, null);
            UtilBussinessService.WriteLog(UtilBussinessService.detoken(Token), "制品不良统计分析", null, "查看", null);
            return IMBussinessService.Sfc00022GetItemList(Token, StartItemCode, EndItemCode, StartDate, EndDate, page, rows);
        }

        /// <summary>
        /// 制品不良统计分析-料品統計页签-明细列表
        /// Sam 2017年10月11日14:14:10
        /// </summary>
        /// <param name="Token"></param>
        /// <param name="ItemID"></param>
        /// <param name="StartDate"></param>
        /// <param name="EndDate"></param>
        /// <param name="Interval">年：4，月：7，日：10</param>
        /// <returns></returns>
        [HttpGet]
        [Authenticate]
        public object Sfc00022GetItemDetailList(string Token, string ItemID, string StartDate = null, string EndDate=null,int Interval=4)
        {
            DataLogerService.writeURL(Token, null);
            UtilBussinessService.WriteLog(UtilBussinessService.detoken(Token), "制品不良统计分析", null, "查看", null);
            return IMBussinessService.Sfc00022GetItemDetailList(Token, ItemID, StartDate, EndDate, Interval);
        }


        #endregion

        #region SFC00023完工批号明细表
        /// <summary>
        /// SFC00023列表获取
        /// </summary>
        /// <param name="Token"></param>
        /// <param name="BatchNo"></param>
        /// <param name="ItemStar"></param>
        /// <param name="ItemEnd"></param>
        /// <param name="DateStar"></param>
        /// <param name="DateEnd"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <returns></returns>
        [HttpGet]
        [Authenticate]
        public object sfc00023GetList(string Token, string BatchNo = null, string ItemStar = null, string ItemEnd = null, string DateStar = null, string DateEnd = null, int page = 1, int rows = 10)
        {
            DataLogerService.writeURL(Token, null);
            UtilBussinessService.WriteLog(UtilBussinessService.detoken(Token), "完工批号明细表", null, "查看", null);
            return IMBussinessService.sfc00023GetList(Token, BatchNo, ItemStar, ItemEnd, DateStar, DateEnd, page, rows);
        }
        #endregion
    }
}
