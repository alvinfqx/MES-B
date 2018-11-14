using MonkeyFly.MES.BasicService;
using MonkeyFly.MES.FilterAttributes;
using MonkeyFly.MES.Services;
using System.Collections;
using System.Collections.Generic;
using System.Web.Http;
using System.Web.Http.Cors;

namespace MonkeyFly.MES.Webapi
{
    /// <summary>
    /// 弹窗专属控制器
    /// SAM 2017年6月6日21:46:46
    /// </summary>
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class PopUpController : ApiController
    {
        /// <summary>
        /// PopUpAPI
        /// </summary>
        [HttpGet]
        public void PopUpAPI() { }

        /// <summary>
        /// 账户的弹窗
        /// SAM 2017年4月28日14:43:17
        /// </summary>
        /// <param name="Token"></param>
        /// <param name="Account"></param>
        /// <param name="Code"></param>
        /// <param name="UserName"></param>
        /// <param name="StartCode"></param>
        /// <param name="EndCode"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <returns></returns>
        [HttpGet]
        [Authenticate]
        public object GetUserList(string Token, string Account = null, string Code = null, string UserName = null, string StartCode = null, string EndCode = null, int page = 1, int rows = 10)
        {
            DataLogerService.writeURL(Token, "Code:" + Code);
            return PopUpBussinessService.GetUserList(Token, Account, Code, UserName, StartCode, EndCode, page, rows);
        }

        /// <summary>
        /// 获取分类的弹窗
        /// SAM 2017年5月2日15:21:53
        /// </summary>
        /// <param name="Token">授权码</param>
        /// <param name="Type">用途的Code</param>
        /// <param name="Code">代号</param>
        /// <param name="page">页码</param>
        /// <param name="rows">行数</param>
        /// <returns></returns>
        [HttpGet]
        [Authenticate]
        public object getClassList(string Token, string Type = null, string Code = null, int page = 1, int rows = 10)
        {
            DataLogerService.writeURL(Token, null);
            return PopUpBussinessService.getClassList(Token, Type, Code, page, rows);
        }

        /// <summary>
        /// 获取存在于参数表的数据
        /// SAM 2017年5月2日14:56:54
        /// </summary>
        /// <param name="Token">授权码</param>
        /// <param name="typeID">XXXXX1</param>
        /// <param name="Name"></param>
        /// <param name="Code">代号</param>
        /// <param name="page">页码</param>
        /// <param name="rows">行数</param>
        /// <returns></returns>
        [HttpGet]
        [Authenticate]
        public object getParameterList(string Token, string typeID, string Code = null, string Name = null, int page = 1, int rows = 10)
        {
            DataLogerService.writeURL(Token, null);
            return PopUpBussinessService.getParameterList(Token, typeID, Code, Name, page, rows);
        }

        /// <summary>
        /// 厂别的弹窗
        /// SAM 2017年5月4日11:34:52
        /// </summary>
        /// <param name="Token">授权码</param>
        /// <param name="Code">代号</param>
        /// <param name="page">页码</param>
        /// <param name="rows">行数</param>
        /// <returns></returns>
        [HttpGet]
        [Authenticate]
        public object GetPlantList(string Token, string Code = null, int page = 1, int rows = 10)
        {
            DataLogerService.writeURL(Token, "Code:" + Code);
            return PopUpBussinessService.GetPlantList(Token, Code, page, rows);
        }

        /// <summary>
        /// 仓库的弹窗
        /// Tom 2017年6月29日01点23分
        /// </summary>
        /// <param name="Token"></param>
        /// <param name="Code"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <returns></returns>
        [HttpGet]
        [Authenticate]
        public object GetWarehouseList(string Token, string Code = null, int page = 1, int rows = 10)
        {
            DataLogerService.writeURL(Token, "Code:" + Code);
            return PopUpBussinessService.GetWarehouseList(Token, Code, page, rows);
        }

        /// <summary>
        /// 厂区的弹窗
        /// SAM 2017年5月24日14:58:04
        /// </summary>
        /// <param name="Token">授权码</param>
        /// <param name="Code">代号</param>
        /// <param name="page">页码</param>
        /// <param name="rows">行数</param>
        /// <returns></returns>
        [HttpGet]
        [Authenticate]
        public object GetPlantAreaList(string Token, string Code = null, int page = 1, int rows = 10)
        {
            DataLogerService.writeURL(Token, "Code:" + Code);
            return PopUpBussinessService.GetPlantAreaList(Token, Code, page, rows);
        }

        /// <summary>
        /// 部门的弹窗
        /// SAM 2017年5月19日16:06:00
        /// </summary>
        /// <param name="Token">授权码</param>
        /// <param name="Plant">厂别</param>
        /// <param name="Code">代号</param>
        /// <param name="page">页码</param>
        /// <param name="rows">行数</param>
        /// <returns></returns>
        [HttpGet]
        [Authenticate]
        public object GetDeptList(string Token, string Plant = null, string Code = null, int page = 1, int rows = 10)
        {
            DataLogerService.writeURL(Token, "Code:" + Code);
            return PopUpBussinessService.GetDeptList(Token, Plant, Code, page, rows);
        }

        /// <summary>
        /// 感知器的弹窗
        /// SAM 2017年5月24日15:07:17
        /// </summary>
        /// <param name="Token">授权码</param>
        /// <param name="Code">代号</param>
        /// <param name="page">页码</param>
        /// <param name="rows">行数</param>
        /// <returns></returns>
        [HttpGet]
        [Authenticate]
        public object GetSensorList(string Token, string Code = null, int page = 1, int rows = 10)
        {
            DataLogerService.writeURL(Token, "Code:" + Code);
            return PopUpBussinessService.GetSensorList(Token, Code, page, rows);
        }

        /// <summary>
        /// 获取厂商的弹窗
        /// SAM 2017年5月24日16:26:41
        /// </summary>
        /// <param name="Token">授权码</param>
        /// <param name="Code">代号</param>
        /// <param name="Name">名称</param>
        /// <param name="page">页码</param>
        /// <param name="rows">行数</param>
        /// <returns></returns>
        [HttpGet]
        [Authenticate]
        public object GetManufacturerList(string Token, string Code = null, string Name = null, int page = 1, int rows = 10)
        {
            DataLogerService.writeURL(Token, "Code:" + Code);
            return PopUpBussinessService.GetManufacturerList(Token, Code, Name, page, rows);
        }

        /// <summary>
        /// 获取客户的弹窗
        /// SAM 2017年6月21日16:40:07
        /// </summary>
        /// <param name="Token"></param>
        /// <param name="Code"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <returns></returns>
        [HttpGet]
        [Authenticate]
        public object GetCustomerList(string Token, string Code = null, int page = 1, int rows = 10)
        {
            DataLogerService.writeURL(Token, "Code:" + Code);
            return PopUpBussinessService.GetCustomerList(Token, Code, page, rows);
        }

        /// <summary>
        /// 获取项目的弹窗
        /// SAM 2017年5月25日17:09:53
        /// </summary>
        /// <param name="Token">授权码</param>
        /// <param name="Code">代号</param>
        /// <param name="page">页码</param>
        /// <param name="rows">行数</param>
        /// <returns></returns>
        [HttpGet]
        [Authenticate]
        public object GetProjectList(string Token, string Code = null, int page = 1, int rows = 10)
        {
            DataLogerService.writeURL(Token, "Code:" + Code);
            return PopUpBussinessService.GetProjectList(Token, Code, page, rows);
        }

        /// <summary>
        /// 获取行事历的弹窗
        /// SAM 2017年5月26日09:34:30
        /// </summary>
        /// <param name="Token">授权码</param>
        /// <param name="Code">代号</param>
        /// <param name="page">页码</param>
        /// <param name="rows">行数</param>
        /// <returns></returns>
        [HttpGet]
        [Authenticate]
        public object GetCalendarList(string Token, string Code = null, int page = 1, int rows = 10)
        {
            DataLogerService.writeURL(Token, "Code:" + Code);
            return PopUpBussinessService.GetCalendarList(Token, Code, page, rows);
        }

        /// <summary>
        /// 设备的弹窗
        /// SAM 2017年6月1日15:18:53
        /// </summary>
        /// <param name="Token">授权码</param>
        /// <param name="Code">代号</param>
        /// <param name="Category">资源类别</param>
        /// <param name="Name"></param>
        /// <param name="StartCode"></param>
        /// <param name="EndCode"></param>
        /// <param name="page">页码</param>
        /// <param name="rows">行数</param>
        /// <returns></returns>
        [HttpGet]
        [Authenticate]
        public object GetEquipmentList(string Token, string Code = null, string StartCode = null, string EndCode = null, string Category = null, string Name = null, int page = 1, int rows = 10)
        {
            DataLogerService.writeURL(Token, "Code:" + Code);
            return PopUpBussinessService.GetEquipmentList(Token, Code, StartCode, EndCode, Category, Name, page, rows);
        }

        /// <summary>
        /// 设备项目的弹窗
        /// SAM 2017年5月27日11:02:59
        /// </summary>
        /// <param name="Token">授权码</param>
        /// <param name="EquipmentID">设备的流水号</param>
        /// <param name="Code"></param>
        /// <param name="page">页码</param>
        /// <param name="rows">行数</param>
        /// <returns></returns>
        [HttpGet]
        [Authenticate]
        public object GetEquipmentProjectList(string Token, string EquipmentID, string Code = null, int page = 1, int rows = 10)
        {
            DataLogerService.writeURL(Token, "Code:" + Code);
            return PopUpBussinessService.GetEquipmentProjectList(Token, EquipmentID, Code, page, rows);
        }

        /// <summary>
        /// 设备巡检项目的弹窗
        /// SAM 2017年6月8日15:07:47
        /// </summary>
        /// <param name="Token"></param>
        /// <param name="Code"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <returns></returns>
        [HttpGet]
        [Authenticate]
        public object GetEquipmentInspectionProjectList(string Token, string Code = null, int page = 1, int rows = 10)
        {
            DataLogerService.writeURL(Token, "Code:" + Code);
            return PopUpBussinessService.GetEquipmentInspectionProjectList(Token, Code, page, rows);
        }

        /// <summary>
        /// 料品的弹窗
        /// SAM 2017年5月27日14:01:39
        /// </summary>
        /// <param name="Token">授权码</param>
        /// <param name="Code">代号</param>
        /// <param name="Name">料品名称</param>
        /// <param name="page">页码</param>
        /// <param name="rows">行数</param>
        /// <returns></returns>
        [HttpGet]
        [Authenticate]
        public object GetItemList(string Token, string Code = null, string Name=null,int page = 1, int rows = 10)
        {
            DataLogerService.writeURL(Token, "Code:" + Code+ "Name:"+ Name);
            return PopUpBussinessService.GetItemList(Token, Code, Name, page, rows);
        }

        /// <summary>
        /// SFC专用获取料品的弹窗（排除供應型態为4（採購件）的）
        /// SAM 2017年7月23日00:11:41
        /// </summary>
        /// <param name="Token"></param>
        /// <param name="Code"></param>
        /// <param name="Type"></param>
        /// <param name="StartCode"></param>
        /// <param name="EndCode"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <returns></returns>
        [HttpGet]
        [Authenticate]
        public object SfcGetItemList(string Token, string Type = null, string Code = null, string StartCode = null, string EndCode = null, int page = 1, int rows = 10)
        {
            DataLogerService.writeURL(Token, "Code:" + Code);
            return PopUpBussinessService.SfcGetItemList(Token, Type, Code, StartCode, EndCode, page, rows);
        }

        /// <summary>
        /// SFC专用获取料品的弹窗（排除供應型態为4（採購件）的）
        /// Mouse 2017年10月23日16:37:19  起始不区分大小写
        /// </summary>
        /// <param name="Token"></param>
        /// <param name="Code"></param>
        /// <param name="Type"></param>
        /// <param name="StartCode"></param>
        /// <param name="EndCode"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <returns></returns>
        [HttpGet]
        [Authenticate]
        public object Sfc00023GetItemList(string Token, string Type = null, string Code = null, string StartCode = null, string EndCode = null, int page = 1, int rows = 10)
        {
            DataLogerService.writeURL(Token, "Code:" + Code);
            return PopUpBussinessService.Sfc00023GetItemList(Token, Type, Code, StartCode, EndCode, page, rows);
        }


        /// <summary>
        /// 资源的弹窗
        /// SAM 2017年5月27日16:13:21
        /// </summary>
        /// <param name="Token">授权码</param>
        /// <param name="Code">代号</param>
        /// <param name="page">页码</param>
        /// <param name="rows">行数</param>
        /// <returns></returns>
        [HttpGet]
        [Authenticate]
        public object GetResourceList(string Token, string Code = null, int page = 1, int rows = 10)
        {
            DataLogerService.writeURL(Token, "Code:" + Code);
            return PopUpBussinessService.GetResourceList(Token, Code, page, rows);
        }

        /// <summary>
        /// 原因码的弹窗
        /// SAM 2017年5月29日23:35:01
        /// </summary>
        /// <param name="Token">授权码</param>
        /// <param name="Type">用途别</param>
        /// <param name="Code">原因代号</param>
        /// <param name="GroupID">群组流水号</param>
        /// <param name="GroupDescription">群组说明</param>
        /// <param name="Description">原因说明</param>
        /// <param name="page">页码</param>
        /// <param name="rows">行数</param>
        /// <returns></returns>
        [HttpGet]
        [Authenticate]
        public object getReasonList(string Token, string Type = null, string GroupID = null, string Code = null, string GroupDescription = null, string Description = null, int page = 1, int rows = 10)
        {
            DataLogerService.writeURL(Token, null);
            return PopUpBussinessService.getReasonList(Token, Type, GroupID, Code, GroupDescription, Description, page, rows);
        }

        /// <summary>
        /// 班别的弹窗
        /// SAM 2017年6月14日09:38:07
        /// </summary>
        /// <param name="Token"></param>
        /// <param name="Code"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <returns></returns>
        [HttpGet]
        [Authenticate]
        public object GetSYSClassList(string Token, string Code = null, int page = 1, int rows = 10)
        {
            DataLogerService.writeURL(Token, "Code:" + Code);
            return PopUpBussinessService.GetSYSClassList(Token, Code, page, rows);
        }

        /// <summary>
        /// 工作中心的弹窗
        /// SAM 2017年6月20日14:47:55
        /// </summary>
        /// <param name="Token"></param>
        /// <param name="ProcessID">制程流水号</param>
        /// <param name="Code">工作中心代号</param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <returns></returns>
        [HttpGet]
        [Authenticate]
        public object GetWorkCenterList(string Token, string ProcessID = null, string Code = null, int page = 1, int rows = 10)
        {
            DataLogerService.writeURL(Token, "Code:" + Code + "-->ProcessID:" + ProcessID);
            return PopUpBussinessService.GetWorkCenterList(Token, ProcessID, Code, page, rows);
        }

        /// <summary>
        /// 制品制程的料品专属弹窗
        /// SAM 2017年6月21日09:29:28
        /// </summary>
        /// <param name="Token"></param>
        /// <param name="StartCode"></param>
        /// <param name="EndCode"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <returns></returns>
        [HttpGet]
        [Authenticate]
        public object Sfc00001GetItemList(string Token, string StartCode = null, string EndCode = null, int page = 1, int rows = 10)
        {
            DataLogerService.writeURL(Token, "StartCode:" + StartCode + "-->EndCode:" + EndCode);
            return PopUpBussinessService.Sfc00001GetItemList(Token, StartCode, EndCode, page, rows);
        }


        /// <summary>
        /// 制品制程的资源群组专属开窗（设备）
        /// （表面上叫做资源群组的开窗，实际上，最重要的还是资源。。。所以猪脚是资源流水号ResourceID ）
        /// SAM 2017年6月21日09:48:18
        /// </summary>
        /// <param name="Token"></param>
        /// <param name="WorkCenterID"></param>
        /// <param name="GroupCode"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <returns></returns>
        [HttpGet]
        [Authenticate]
        public object Sfc00001GetMGroupList(string Token, string WorkCenterID, string GroupCode = null, int page = 1, int rows = 10)
        {
            DataLogerService.writeURL(Token, "WorkCenterID:" + WorkCenterID + "-->GroupCode:" + GroupCode);
            return PopUpBussinessService.Sfc00001GetGroupList(Token, WorkCenterID, GroupCode, "M", page, rows);
        }

        /// <summary>
        /// 制品制程的资源群组专属开窗（人工）
        /// （表面上叫做资源群组的开窗，实际上，最重要的还是资源。。。所以猪脚是资源流水号ResourceID ）
        /// SAM 2017年6月21日09:48:18
        /// </summary>
        /// <param name="Token"></param>
        /// <param name="WorkCenterID"></param>
        /// <param name="GroupCode"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <returns></returns>
        [HttpGet]
        [Authenticate]
        public object Sfc00001GetLGroupList(string Token, string WorkCenterID, string GroupCode = null, int page = 1, int rows = 10)
        {
            DataLogerService.writeURL(Token, "WorkCenterID:" + WorkCenterID + "-->GroupCode:" + GroupCode);
            return PopUpBussinessService.Sfc00001GetGroupList(Token, WorkCenterID, GroupCode, "L", page, rows);
        }


        /// <summary>
        /// 制品制程的资源群组专属开窗（不是人工也不是设备）
        /// （表面上叫做资源群组的开窗，实际上，最重要的还是资源。。。所以猪脚是资源流水号ResourceID ）
        /// SAM 2017年6月21日09:48:18
        /// </summary>
        /// <param name="Token"></param>
        /// <param name="WorkCenterID"></param>
        /// <param name="GroupCode"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <returns></returns>
        [HttpGet]
        [Authenticate]
        public object Sfc00001GetGroupList(string Token, string WorkCenterID, string GroupCode = null, int page = 1, int rows = 10)
        {
            DataLogerService.writeURL(Token, "WorkCenterID:" + WorkCenterID + "-->GroupCode:" + GroupCode);
            return PopUpBussinessService.Sfc00001GetGroupList(Token, WorkCenterID, GroupCode, null, page, rows);
        }


        /// <summary>
        /// 客诉单的开窗
        /// SAM 2017年6月21日17:48:45
        /// </summary>
        /// <param name="Token"></param>
        /// <param name="Code"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <returns></returns>
        [HttpGet]
        [Authenticate]
        public object GetComplaintList(string Token, string Code = null, int page = 1, int rows = 10)
        {
            DataLogerService.writeURL(Token, "Code:" + Code);
            return PopUpBussinessService.GetComplaintList(Token, Code, page, rows);
        }

        ///<summary>
        ///開窗讀取製令母件表
        ///Joint 2017年6月27日10:55:13
        ///</summary>
        ///<param name="Token">授权码</param>
        ///<param name="MoNo">制令单号</param>
        ///<param name="page">页码</param>
        ///<param name="rows">行数</param>
        [HttpGet]
        [Authenticate]

        public object Sfc00003GetFabricatedMother(string Token, string MoNo = null, int page = 1, int rows = 10)
        {
            DataLogerService.writeURL(Token, "MoNo:" + MoNo);
            return PopUpBussinessService.Sfc00003GetFabricatedMother(Token, MoNo, page, rows);
        }

        ///<summary>
        ///開窗讀取制品制程表
        ///Joint 2017年6月27日10:55:13
        ///</summary>
        ///<param name="Token">授权码</param>
        ///<param name="Code">料品号</param>
        ///<param name="page">页码</param>
        ///<param name="rows">行数</param>
        [HttpGet]
        [Authenticate]

        public object Sfc00003GetItemProcessList(string Token, string Code = null, int page = 1, int rows = 10)
        {
            DataLogerService.writeURL(Token, "Code:" + Code);
            return PopUpBussinessService.Sfc00003GetItemProcessList(Token, Code, page, rows);
        }

        /// <summary>
        /// 制品工序的专属弹窗
        /// SAM 2017年6月27日16:48:173
        /// </summary>
        /// <param name="Token"></param>
        /// <param name="ProcessID"></param>
        /// <param name="Code"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <returns></returns>
        [HttpGet]
        [Authenticate]
        public object Sfc00001GetOperationList(string Token, string ProcessID, string Code = null, int page = 1, int rows = 10)
        {
            DataLogerService.writeURL(Token, "ProcessID:" + ProcessID);
            return PopUpBussinessService.Sfc00001GetOperationList(Token, ProcessID, Code, page, rows);
        }

        /// <summary>
        /// 制品制程关系专属弹窗
        /// SAM 2017年6月27日22:12:44
        /// </summary>
        /// <param name="Token"></param>
        /// <param name="ItemID"></param>
        /// <param name="Code"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <returns></returns>
        [HttpGet]
        [Authenticate]

        public object Sfc00001GetItemProcessList(string Token, string ItemID, string Code = null, int page = 1, int rows = 10)
        {
            DataLogerService.writeURL(Token, "ItemID:" + ItemID);
            return PopUpBussinessService.Sfc00001GetItemProcessList(Token, ItemID, Code, page, rows);
        }

        /// <summary>
        /// 制品制程工序专属开窗
        /// SAM 2017年6月27日22:18:08
        /// </summary>
        /// <param name="Token"></param>
        /// <param name="ItemProcessID"></param>
        /// <param name="Code"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <returns></returns>
        [HttpGet]
        [Authenticate]
        public object Sfc00001GetProcessOperationList(string Token, string ItemProcessID, string Code = null, int page = 1, int rows = 10)
        {
            DataLogerService.writeURL(Token, "ItemProcessID:" + ItemProcessID);
            return PopUpBussinessService.Sfc00001GetProcessOperationList(Token, ItemProcessID, Code, page, rows);
        }

        /// <summary>
        /// QCS00004获取检验项目的下拉框
        /// SAM 2017年7月6日15:09:08
        /// </summary>
        /// <param name="Token"></param>
        /// <returns></returns>
        [HttpGet]
        [Authenticate]
        public IList<Hashtable> QCS00004GetInspectionProjectList(string Token)
        {
            DataLogerService.writeURL(Token, null);
            return PopUpBussinessService.QCS00004GetInspectionProjectList(Token);
        }



        /// <summary>
        /// 完工回报主档读取
        /// Joint
        /// </summary>
        /// <param name="Token"></param>
        /// <param name="CompletionNo"></param>
        /// <param name="Code"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <returns></returns>
        [HttpGet]
        [Authenticate]
        public object GetFinishList(string Token, string CompletionNo = null, string Code = null, int page = 1, int rows = 10)
        {
            DataLogerService.writeURL(Token, "CompletionNo:" + CompletionNo);
            return PopUpBussinessService.GetFinishList(Token, CompletionNo, Code, page, rows);
        }

        /// <summary>
        /// QCS00005完工单弹窗--需求新增
        /// Mouse 2017年11月9日14:46:26
        /// 去掉状态为开单的，
        /// 去掉状态为确认且允收的
        /// </summary>
        /// <param name="Token"></param>
        /// <param name="CompletionNo"></param>
        /// <param name="Code"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <returns></returns>
        [HttpGet]
        [Authenticate]
        public object GetFinishListV1(string Token, string CompletionNo = null, string Code = null, int page = 1, int rows = 10)
        {
            DataLogerService.writeURL(Token, "CompletionNo:" + CompletionNo);
            return PopUpBussinessService.GetFinishListV1(Token, CompletionNo, Code, page, rows);
        }

        /// <summary>
        /// 不存在于工单和清单中的设备弹窗
        /// SAM 2017年7月9日16:47:18
        /// </summary>
        /// <param name="Token"></param>
        /// <param name="MaintenanceOrderID"></param>
        /// <param name="EquipmentMaintenanceListID"></param>
        /// <param name="Code"></param>
        /// <param name="Name"></param>
        /// <returns></returns>
        [HttpGet]
        [Authenticate]
        public IList<Hashtable> EMSGetEquMaiEquipmentList(string Token, string MaintenanceOrderID,
            string EquipmentMaintenanceListID = null, string Code = null, string Name = null)
        {
            DataLogerService.writeURL(Token, null);
            return PopUpBussinessService.EMSGetEquMaiEquipmentList(Token, MaintenanceOrderID, EquipmentMaintenanceListID, Code, Name);
        }

        /// <summary>
        /// 不存在于保养工单的保养项目列表
        /// SAM 2017年7月9日17:00:16
        /// </summary>
        /// <param name="Token"></param>
        /// <param name="MaintenanceOrderID"></param>
        /// <returns></returns>
        [HttpGet]
        [Authenticate]
        public IList<Hashtable> EMSGetEquMaiProjectList(string Token, string MaintenanceOrderID)
        {
            DataLogerService.writeURL(Token, null);
            return PopUpBussinessService.EMSGetEquMaiProjectList(Token, MaintenanceOrderID);
        }

        /// <summary>
        /// 检验单的开窗
        /// SAM 2017年7月9日20:47:211
        /// </summary>
        /// <param name="Token"></param>
        /// <param name="InspectionNo"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <returns></returns>
        [HttpGet]
        [Authenticate]
        public object QCSInspectionDocumentList(string Token, string InspectionNo = null, int page = 1, int rows = 10)
        {
            DataLogerService.writeURL(Token, "InspectionNo:" + InspectionNo);
            return PopUpBussinessService.QCSInspectionDocumentList(Token, InspectionNo, page, rows);
        }

        /// <summary>
        /// 任务单的开窗
        /// SAM 2017年9月6日17:16:18
        /// </summary>
        /// <param name="Token"></param>
        /// <param name="TaskNo"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <returns></returns>
        [HttpGet]
        [Authenticate]
        public object TaskDispatchList(string Token, string TaskNo = null, int page = 1, int rows = 10)
        {
            DataLogerService.writeURL(Token, "TaskNo:" + TaskNo);
            return PopUpBussinessService.TaskDispatchList(Token, TaskNo, page, rows);
        }


        /// <summary>
        /// 任务单的开窗
        /// SAM 2017年7月9日20:58:52
        /// Mouse 2017年9月4日17:54:12 修改
        /// </summary>
        /// <param name="Token"></param>
        /// <param name="TaskNo"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <returns></returns>
        [HttpGet]
        [Authenticate]
        public object Qcs00007TaskDispatchList(string Token, string TaskNo = null, int page = 1, int rows = 10)
        {
            DataLogerService.writeURL(Token, "TaskNo:" + TaskNo);
            return PopUpBussinessService.Qcs00007TaskDispatchList(Token, TaskNo, page, rows);
        }

        /// <summary>
        /// 任务单的开窗
        /// Mouse 2017年11月1日09:48:15 需求修改
        /// </summary>
        /// <param name="Token"></param>
        /// <param name="TaskNo"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <param name="ProcessCode"></param>
        /// <param name="ProcessName"></param>
        /// <returns></returns>
        [HttpGet]
        [Authenticate]
        public object Qcs00007TaskDispatchListV1(string Token, string TaskNo = null, string ProcessCode = null, string ProcessName = null, int page = 1, int rows = 10)
        {
            DataLogerService.writeURL(Token, "TaskNo:" + TaskNo);
            return PopUpBussinessService.Qcs00007TaskDispatchListV1(Token, TaskNo, ProcessCode, ProcessName, page, rows);
        }


        /// <summary>
        /// 新的任务单的开窗
        /// MOUSE 2017年7月9日20:58:52
        /// </summary>
        /// <param name="Token"></param>
        /// <param name="TaskNo"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <returns></returns>
        [HttpGet]
        [Authenticate]
        public object Qcs00008TaskDispatchList(string Token, string TaskNo = null, int page = 1, int rows = 10)
        {
            DataLogerService.writeURL(Token, "TaskNo:" + TaskNo);
            return PopUpBussinessService.Qcs00008TaskDispatchList(Token, TaskNo, page, rows);
        }

        /// <summary>
        /// Sfc00004获取资源明细的专属弹窗
        /// SAM 2017年7月10日17:03:29
        /// </summary>
        /// <param name="Token"></param>
        /// <param name="Code">类别代号，L代表人工，M代表机器，为空即代表不是机器也不是人工</param>
        /// <param name="page"></param>
        /// <param name="FabMoProcessID"></param>
        /// <param name="FabMoOperationID"></param>
        /// <param name="rows"></param>
        /// <returns></returns>
        [HttpGet]
        [Authenticate]
        public object Sfc00004GetResourceDetailList(string Token, string Code, string FabMoProcessID, string FabMoOperationID = null, int page = 1, int rows = 10)
        {
            DataLogerService.writeURL(Token, "Code:" + Code);
            return PopUpBussinessService.Sfc00004GetResourceDetailList(Token, Code, FabMoProcessID, FabMoOperationID, page, rows);
        }

        /// <summary>
        /// Ems00009的检修人弹窗
        /// SAM 2017年7月12日14:41:08
        /// </summary>
        /// <param name="Token"></param>
        /// <param name="OrganizationCode"></param>
        /// <param name="OrganizationName"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <returns></returns>
        [HttpGet]
        [Authenticate]
        public object Ems00009GetUserList(string Token, string OrganizationCode = null, string OrganizationName = null, int page = 1, int rows = 10)
        {
            DataLogerService.writeURL(Token, "OrganizationCode:" + OrganizationCode);
            return PopUpBussinessService.Ems00009GetUserList(Token, OrganizationCode, OrganizationName, page, rows);
        }


        /// <summary>
        /// 制令工单-根据制令制程获取下属工序弹窗
        /// SAM 2017年7月14日11:48:01
        /// </summary>
        /// <param name="Token"></param>
        /// <param name="FabMoProcessID"></param>
        /// <param name="Code"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <returns></returns>
        [HttpGet]
        [Authenticate]
        public object SfcGetFabMoOperationList(string Token, string FabMoProcessID, string Code = null, int page = 1, int rows = 10)
        {
            DataLogerService.writeURL(Token, "FabMoProcessID:" + FabMoProcessID);
            return PopUpBussinessService.SfcGetFabMoOperationList(Token, FabMoProcessID, Code, page, rows);
        }

        /// <summary>
        /// 制令工单-获取制令单下的制程弹窗
        /// SAM 2017年7月14日15:51:08
        /// </summary>
        /// <param name="Token"></param>
        /// <param name="FabricatedMotherID"></param>
        /// <param name="Code"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <returns></returns>
        [HttpGet]
        [Authenticate]
        public object SfcGetFabMoProcessList(string Token, string FabricatedMotherID, string Code = null, int page = 1, int rows = 10)
        {
            DataLogerService.writeURL(Token, "FabricatedMotherID:" + FabricatedMotherID);
            return PopUpBussinessService.SfcGetFabMoProcessList(Token, FabricatedMotherID, Code, page, rows);
        }

        /// <summary>
        /// 获取资源类别弹窗列表
        /// Tom 2017年7月24日16:25:56
        /// </summary>
        /// <param name="Token"></param>
        /// <param name="Code"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <returns></returns>
        [HttpGet]
        [Authenticate]
        public object SfcGetResourceClassList(string Token, string Code = null, int page = 1, int rows = 10)
        {
            return PopUpBussinessService.SfcGetResourceClassList(Token, Code, page, rows);
        }

        /// <summary>
        /// 根据资源类别获取资源
        /// Tom 2017年7月24日17:27:42
        /// </summary>
        /// <param name="Token"></param>
        /// <param name="TaskDispatchID"></param>
        /// <param name="ClassID"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <returns></returns>
        [HttpGet]
        [Authenticate]
        public object SfcGetResourceByClass(string Token, string TaskDispatchID, string ClassID, int page = 1, int rows = 10)
        {
            return PopUpBussinessService.SfcGetResourceByClass(Token, TaskDispatchID, ClassID, page, rows);
        }

        /// <summary>
        /// 制令单的弹窗
        /// SAM 2017年7月25日14:01:50
        /// </summary>
        /// <param name="Token"></param>
        /// <param name="MoNo"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <returns></returns>
        [HttpGet]
        [Authenticate]
        public object Sfc00010GetFabricatedMother(string Token, string MoNo = null, int page = 1, int rows = 10)
        {
            DataLogerService.writeURL(Token, "MoNo:" + MoNo);
            return PopUpBussinessService.Sfc00010GetFabricatedMother(Token, MoNo, page, rows);
        }

        /// <summary>
        /// Sfc02的制令单开窗
        /// SAM 2017年8月30日16:59:23
        /// </summary>
        /// <param name="Token"></param>
        /// <param name="MoNo"></param>
        /// <param name="ItemName">料品品名</param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <returns></returns>
        [HttpGet]
        [Authenticate]
        public object Sfc00002GetFabricatedMother(string Token, string MoNo = null, string ItemName=null,int page = 1, int rows = 10)
        {
            DataLogerService.writeURL(Token, "MoNo:" + MoNo+ "ItemName" + ItemName);
            return PopUpBussinessService.Sfc00002GetFabMother(Token, MoNo, ItemName, page, rows);
        }

        /// <summary>
        /// Sfc20的制令单表头开窗
        /// Mouse 2017年9月26日14:47:46
        /// </summary>
        /// <param name="Token"></param>
        /// <param name="MoNo"></param>
        /// <param name="ItemName"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <returns></returns>
        [HttpGet]
        [Authenticate]
        public object Sfc00020GetFabricatedMother(string Token, string MoNo = null,string ItemName=null, int page = 1, int rows = 10)
        {
            DataLogerService.writeURL(Token, "MoNo:" + MoNo);
            return PopUpBussinessService.Sfc00020GetFabricatedMother(Token, MoNo,ItemName, page, rows);
        }

        /// <summary>
        /// 设备巡检维护任务单弹窗
        /// Tom 2017年7月28日17:40:38
        /// 
        /// 設備巡檢,意指直接到現場查看機器設備,順便要紀錄正在生產的工單
        /// 故在程式設計上,不會各自挑設備和任務單
        /// 挑了設備,要挑的任務單會是這台設備正在生產的任務單
        /// 故,任務單開窗會過濾條件為資源使用這台設備的任務單
        /// 任務單狀態一定是在IN
        /// </summary>
        /// <param name="Token"></param>
        /// <param name="EquipmentID">设备流水号</param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <returns></returns>
        [HttpGet]
        [Authenticate]
        public object Ems0003GetTaskDispatchList(string Token, string EquipmentID, int page = 1, int rows = 10)
        {
            return PopUpBussinessService.Ems0003GetTaskDispatchList(EquipmentID, page, rows);
        }

        /// <summary>
        /// Iot00003设备弹窗
        /// Mouse 2017年9月6日18:04:20
        /// </summary>
        /// <param name="Token"></param>
        /// <param name="EquipmentID"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <returns></returns>
        [HttpGet]
        [Authenticate]
        public object Iot00003GetEquipment(string Token, string EquipmentID = null, int page = 1, int rows = 10)
        {
            DataLogerService.writeURL(Token, "EquipmentID" + EquipmentID);
            return PopUpBussinessService.Iot00003GetEquipment(EquipmentID, page, rows);
        }

        /// <summary>
        /// Iot00003设备弹窗V1修改
        /// Mouse 2017年9月6日18:04:20
        /// </summary>
        /// <param name="Token"></param>
        /// <param name="EquipmentID"></param>
        /// <param name="Code"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <returns></returns>
        [HttpGet]
        [Authenticate]
        public object Iot00003GetEquipmentV1(string Token, string EquipmentID = null, string Code = null, int page = 1, int rows = 10)
        {
            DataLogerService.writeURL(Token, "EquipmentID" + EquipmentID);
            return PopUpBussinessService.Iot00003GetEquipmentV1(EquipmentID, Code, page, rows);
        }

        /// <summary>
        /// 获取语序下拉框
        /// Sam 2017年9月28日14:19:14
        /// ObjectID字段为所选中需要做语序的记录的流水号
        /// 用于过滤掉已经设置好的语序
        /// </summary>
        /// <param name="Token"></param>
        /// <param name="ObjectID"></param>
        /// <returns></returns>
        [HttpGet]
        [Authenticate]
        public IList<Hashtable> GetLanguageList(string Token, string ObjectID)
        {
            DataLogerService.writeURL(Token, "ObjectID" + ObjectID);
            return PopUpBussinessService.GetLanguageList(ObjectID);
        }

        /// <summary>
        /// Sfc00017的料品弹窗
        /// SAM 2017年10月11日17:40:16
        /// </summary>
        /// <param name="Token"></param>
        /// <param name="Code"></param>
        /// <param name="Name"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <returns></returns>
        [HttpGet]
        [Authenticate]
        public object Sfc00017GetItemList(string Token, string Code = null, string Name = null, int page = 1, int rows = 10)
        {
            DataLogerService.writeURL(Token, "Code:" + Code + "Name:" + Name);
            return PopUpBussinessService.Sfc00017GetItemList(Token, Code, Name, page, rows);
        }

        /// <summary>
        /// sfc00017的制令单号弹窗
        /// SAM 2017年10月11日17:40:28
        /// </summary>
        /// <param name="Token"></param>
        /// <param name="MoNo"></param>
        /// <param name="ItemName"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <returns></returns>
        [HttpGet]
        [Authenticate]
        public object Sfc00017GetFabMoList(string Token, string MoNo = null, string ItemName = null, int page = 1, int rows = 10)
        {
            DataLogerService.writeURL(Token, "MoNo:" + MoNo + "ItemName" + ItemName);
            return PopUpBussinessService.Sfc00017GetFabMoList(Token, MoNo, ItemName, page, rows);
        }

        /// <summary>
        /// 获取制令单列表
        /// SAM 2017年10月20日08:45:51 
        /// 状态为ca/作废 的数据不抓取
        /// MoNo为单个其实区间查询
        /// </summary>
        /// <param name="Token"></param>
        /// <param name="MoNo"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <returns></returns>
        [HttpGet]
        [Authenticate]
        public object SfcGetFabMoList(string Token, string MoNo = null,string ItemName=null, int page = 1, int rows = 10)
        {
            DataLogerService.writeURL(Token, "MoNo:" + MoNo);
            return PopUpBussinessService.SfcGetFabMoList(Token, MoNo, ItemName, page, rows);
        }

        /// <summary>
        /// 获取制令单列表
        /// SAM 2017年10月20日15:40:03
        /// 状态为CL/结案 的数据
        /// MoNo为单个起始区间查询，不包括本身
        /// </summary>
        /// <param name="Token"></param>
        /// <param name="MoNo"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <returns></returns>
        [HttpGet]
        [Authenticate]
        public object SfcGetCLFabMoList(string Token, string MoNo = null, int page = 1, int rows = 10)
        {
            DataLogerService.writeURL(Token, "MoNo:" + MoNo);
            return PopUpBussinessService.SfcGetCLFabMoList(Token, MoNo, page, rows);
        }

        /// <summary>
        /// 获取料品列表
        /// SAM 2017年10月20日15:38:10
        /// 供应型态为采购件的料号不抓取
        /// Code为单个起始区间查询，不包括本身
        /// </summary>
        /// <param name="Token"></param>
        /// <param name="Code"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <returns></returns>
        [HttpGet]
        [Authenticate]
        public object SfcItemList(string Token, string Code = null, int page = 1, int rows = 10)
        {
            DataLogerService.writeURL(Token, "Code:" + Code);
            return PopUpBussinessService.SfcItemList(Token, Code, page, rows);
        }

        /// <summary>
        /// 获取工作中心列表
        /// SAM 2017年10月20日15:38:10
        /// Code为单个起始区间查询，不包括本身
        /// </summary>
        /// <param name="Token"></param>
        /// <param name="Code"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <returns></returns>
        [HttpGet]
        [Authenticate]
        public object SfcWorkCenterList(string Token, string Code = null, int page = 1, int rows = 10)
        {
            DataLogerService.writeURL(Token, "Code:" + Code);
            return PopUpBussinessService.SfcWorkCenterList(Token, Code, page, rows);
        }

        /// <summary>
        /// 获取制程的列表
        /// Sam 2017年10月30日14:35:29
        /// Code为单个起始区间查询，不包括本身
        /// Name关键字的模糊查询
        /// </summary>
        /// <param name="Token"></param>
        /// <param name="Code"></param>
        /// <param name="Name"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <returns></returns>
        [HttpGet]
        [Authenticate]
        public object SfcProcessList(string Token, string Code = null, string Name=null,int page = 1, int rows = 10)
        {
            DataLogerService.writeURL(Token, "Code:" + Code+ "Name:" + Code);
            return PopUpBussinessService.SfcProcessList(Token, Code, Name,page, rows);
        }

    }
}
