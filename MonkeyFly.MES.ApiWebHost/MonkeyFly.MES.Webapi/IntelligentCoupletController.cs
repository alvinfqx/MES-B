using MonkeyFly.MES.BasicService;
using MonkeyFly.MES.FilterAttributes;
using MonkeyFly.MES.Services;
using Newtonsoft.Json.Linq;
using System.Web.Http;
using System.Web.Http.Cors;

namespace MonkeyFly.MES.Webapi
{
    /// <summary>
    /// 智能物联
    /// </summary>
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class IntelligentCoupletController : ApiController
    {
        /// <summary>
        /// IntelligentCoupletAPI
        /// </summary>
        [HttpGet]
        public void IntelligentCoupletAPI() { }

        #region IOT00001感知器主档
        /// <summary>
        /// 感知器列表
        /// SAM 2017年5月23日12:02:53
        /// </summary>
        /// <param name="Token">授权码</param>
        /// <param name="Code">代号</param>
        /// <param name="Status">状态</param>
        /// <param name="page">页码</param>
        /// <param name="rows">行数</param>
        /// <returns></returns>
        [HttpGet]
        [Authenticate]
        public object lot00001GetList(string Token, string Code = null, string Status = null, int page = 1, int rows = 10)
        {
            DataLogerService.writeURL(Token, "Code:" + Code + "--->Status:" + Status);
            UtilBussinessService.WriteLog(UtilBussinessService.detoken(Token), "感知器主档", null, "查看", "Code:" + Code + "--->Status:" + Status);
            return ICBussinessService.lot00001GetList(Token, Code, Status, page, rows);
        }

        /// <summary>
        /// 感知器的保存
        /// SAM 2017年5月23日12:18:04
        /// </summary>
        /// <param name="request">JSON数据</param>
        /// <returns></returns>
        [HttpPost]
        [Authenticate]
        public object lot00001Save(JObject request)
        {
            DataLogerService.writeURL(request.Value<string>("Token"), request.ToString());
            UtilBussinessService.WriteLog(UtilBussinessService.detoken(request.Value<string>("Token")), "感知器主档", null, "保存", request.ToString());
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
                deleted = ICBussinessService.lot00001delete(request.Value<string>("Token"), request.Value<JArray>("deleted"));
            if (isInsert)
                Insert = ICBussinessService.lot00001insert(request.Value<string>("Token"), request.Value<JArray>("inserted"));
            if (isUpdate)
                Update = ICBussinessService.lot00001update(request.Value<string>("Token"), request.Value<JArray>("updated"));

            return new { inserted = Insert, updated = Update, deleted = deleted };
        }

        #endregion

        #region IOT00002厂区设备监控
        /// <summary>
        /// 查询列表
        /// Alvin 2017年9月4日17:28:29
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Authenticate]
        public object Iot00002GetList(string Token, string PlantCode = null, string PlantAreaCode = null)
        {
            DataLogerService.writeURL(Token, "PlantCode:" + PlantCode + "PlantAreaCode:" + PlantAreaCode);
            UtilBussinessService.WriteLog(UtilBussinessService.detoken(Token), "厂区设备监控", null, "查看", "PlantCode:" + PlantCode + "--->PlantAreaCode:" + PlantAreaCode);
            return ICBussinessService.Iot00002GetList(Token, PlantCode, PlantAreaCode);
        }
        #endregion

        #region IOT00003机台设备监控

        /// <summary>
        /// 机台设备监控 设备列表
        /// Mouse 2017年9月6日11:28:29
        /// </summary>
        /// <param name="Token"></param>
        /// <param name="EquipmentID"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <returns></returns>
        [HttpGet]
        [Authenticate]
        public object Iot00003GetEquipmentList(string Token, string EquipmentID, int page = 1, int rows = 10)
        {
            DataLogerService.writeURL(Token, "EquiipmentID:" + EquipmentID);
            UtilBussinessService.WriteLog(UtilBussinessService.detoken(Token), "机台设备监控主档", null, "查询", null);
            return ICBussinessService.Iot00003GetEquipmentList(Token, EquipmentID, page, rows);
        }

        /// <summary>
        /// 机台设备监控 设备列表
        /// Mouse 2017年9月6日11:28:29
        /// </summary>
        /// <param name="Token"></param>
        /// <param name="EquipmentID"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <returns></returns>
        [HttpGet]
        [Authenticate]
        public object Iot00003GetEquipmentListV1(string Token, string EquipmentID, int page = 1, int rows = 10)
        {
            DataLogerService.writeURL(Token, "EquiipmentID:" + EquipmentID);
            UtilBussinessService.WriteLog(UtilBussinessService.detoken(Token), "机台设备监控主档", null, "查询", null);
            return ICBussinessService.Iot00003GetEquipmentListV1(Token, EquipmentID, page, rows);
        }

        /// <summary>
        /// Iot00003工作中心按钮
        /// Mouse 2017年9月12日16:51:24
        /// </summary>
        /// <param name="Token"></param>
        /// <param name="EquipmentID"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <returns></returns>
        [HttpGet]
        [Authenticate]
        public object Iot00003GetCenter(string Token, string EquipmentID, int page = 1, int rows = 10)
        {
            DataLogerService.writeURL(Token, "EquipmentID:" + EquipmentID);
            UtilBussinessService.WriteLog(UtilBussinessService.detoken(Token), "机台设备监控主档工作中心按钮", null, "查看", null);
            return ICBussinessService.Iot00003GetCenter(Token, EquipmentID, page, rows);
        }

        /// <summary>
        /// 查询工作中心是否于制程存在关系
        /// Mouse 2017年9月12日18:13:56
        /// </summary>
        /// <param name="Token"></param>
        /// <param name="WorkCenterID"></param>
        /// <returns></returns>
        [HttpGet]
        [Authenticate]
        public object Iot00003CheckCenterProcess(string Token, string WorkCenterID)
        {
            DataLogerService.writeURL(Token, "WorkCenterID:" + WorkCenterID);
            UtilBussinessService.WriteLog(UtilBussinessService.detoken(Token), "机台设备监控主档工作中心按钮", null, "查询", null);
            return ICBussinessService.Iot00003CheckCenterProcess(Token, WorkCenterID);
        }

        /// <summary>
        /// Iot00003工作中心按钮的制程按钮
        /// Mouse 2017年9月12日16:58:14
        /// </summary>
        /// <param name="Token"></param>
        /// <param name="WorkCenterID"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <returns></returns>
        [HttpGet]
        [Authenticate]
        public object Iot00003GetCenterProcess(string Token, string WorkCenterID, int page = 1, int rows = 10)
        {
            DataLogerService.writeURL(Token, "WorkCenterID:" + WorkCenterID);
            UtilBussinessService.WriteLog(UtilBussinessService.detoken(Token), "机台设备监控主档工作中心按钮之制程按钮", null, "查看", null);
            return ICBussinessService.Iot00003GetCenterProcess(Token, WorkCenterID, page, rows);
        }

        /// <summary>
        /// Iot00003制令工单按钮
        /// Mouse 2017年9月13日10:43:19
        /// </summary>
        /// <param name="Token"></param>
        /// <param name="EquipmentID"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <returns></returns>
        [HttpGet]
        [Authenticate]
        public object Iot00003GetMomProcess(string Token, string EquipmentID, int page = 1, int rows = 10)
        {
            DataLogerService.writeURL(Token, "EquipmentID:" + EquipmentID);
            UtilBussinessService.WriteLog(UtilBussinessService.detoken(Token), "机台设备监控主档之制令工单按钮", null, "查看", null);
            return ICBussinessService.Iot00003GetMomProcess(Token, EquipmentID, page, rows);
        }

        /// <summary>
        /// Iot00003设备事件按钮
        /// Mouse 2017年9月13日16:30:15
        /// </summary>
        /// <param name="Token"></param>
        /// <param name="EquipmentID"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <returns></returns>
        [HttpGet]
        [Authenticate]
        public object Iot00003GetDaqEvent(string Token, string EquipmentID, int page = 1, int rows = 10)
        {
            DataLogerService.writeURL(Token, "EquipmentID:" + EquipmentID);
            UtilBussinessService.WriteLog(UtilBussinessService.detoken(Token), "机台设备监控主档之设备事件按钮", null, "查看", null);
            return ICBussinessService.Iot00003GetDaqEvent(Token, EquipmentID, page, rows);
        }

        /// <summary>
        /// Iot00003设备监控资料
        /// Mouse 2017年9月14日10:27:24
        /// </summary>
        /// <param name="Token"></param>
        /// <param name="EquipmentID"></param>

        /// <returns></returns>
        [HttpGet]
        [Authenticate]
        public object Iot00003GetDaqValue(string Token, string EquipmentID)
        {
            DataLogerService.writeURL(Token, "EquipmentID:" + EquipmentID);
            UtilBussinessService.WriteLog(UtilBussinessService.detoken(Token), "机台监控设备主档之监控列表", null, "查看", null);
            return ICBussinessService.Iot00003GetDaqValue(Token, EquipmentID);
        }

        /// <summary>
        /// Iot00003DAQ现值趋势图 不分页
        /// Mouse 2017年9月14日11:09:09
        /// </summary>
        /// <param name="Token"></param>
        /// <param name="EquipmentID"></param>
        /// <param name="Sensor"></param>
        /// <returns></returns>
        [HttpGet]
        [Authenticate]
        public object Iot00003GetMeasurementQST(string Token, string EquipmentID, string Sensor)
        {
            DataLogerService.writeURL(Token, "EquipmentID:" + EquipmentID);
            UtilBussinessService.WriteLog(UtilBussinessService.detoken(Token), "机台监控设备主档之DAQ趋势图", null, "查看", null);
            return ICBussinessService.Iot00003GetMeasurementQST(Token, EquipmentID, Sensor);
        }

        #endregion
    }
}
