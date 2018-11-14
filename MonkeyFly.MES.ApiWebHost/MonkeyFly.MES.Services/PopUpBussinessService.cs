using MonkeyFly.Core;
using MonkeyFly.MES.ModelServices;
using System.Collections;
using System.Collections.Generic;
using System;

namespace MonkeyFly.MES.Services
{
    public class PopUpBussinessService
    {
        /// <summary>
        /// 账户弹窗
        /// SAM 2017年4月28日14:44:33
        /// </summary>
        /// <param name="Token">授权码</param>
        /// <param name="Code">代号</param>
        /// <param name="page">页码</param>
        /// <param name="rows">行数</param>
        /// <returns></returns>
        public static object GetUserList(string token,string Account, string code, string UserName, string StartCode, string EndCode, int page, int rows)
        {
            int count = 0;
            return UtilBussinessService.getPaginationModel(SYS_MESUserService.GetUserList(Account,code, UserName,StartCode, EndCode, page, rows, ref count), count);
        }

        /// <summary>
        /// 获取分类的弹窗
        /// SAM 2017年5月2日15:22:03
        /// </summary>
        /// <param name="Token">授权码</param>
        /// <param name="type"></param>
        /// <param name="Code">代号</param>
        /// <param name="page">页码</param>
        /// <param name="rows">行数</param>
        /// <returns></returns>
        public static object getClassList(string Token, string type, string Code, int page, int rows)
        {
            int Count = 0;
            return UtilBussinessService.getPaginationModel(SYS_ParameterService.getClassList(type, Code, page, rows, ref Count), Count);
        }

        /// <summary>
        /// 获取存在于参数表的数据
        /// SAM 2017年5月2日14:59:45
        /// </summary>
        /// <param name="Token">授权码</param>
        /// <param name="type"></param>
        /// <param name="Code">代号</param>
        /// <param name="page">页码</param>
        /// <param name="rows">行数</param>
        /// <returns></returns>
        public static object getParameterList(string Token, string type, string Code, string Name,int page, int rows)
        {
            int Count = 0;
            return UtilBussinessService.getPaginationModel(SYS_ParameterService.getParameterList(Framework.SystemID + "0191213" + type, Code, Name, page, rows, ref Count), Count);
        }

        /// <summary>
        /// 厂别的弹窗
        /// SAM 2017年5月4日11:35:51
        /// </summary>
        /// <param name="Token">授权码</param>
        /// <param name="Code">代号</param>
        /// <param name="page">页码</param>
        /// <param name="rows">行数</param>
        /// <returns></returns>
        public static object GetPlantList(string token, string code, int page, int rows)
        {
            int count = 0;
            return UtilBussinessService.getPaginationModel(SYS_OrganizationService.GetPlantList(code, page, rows, ref count), count);
        }

        /// <summary>
        /// 厂区的弹窗
        /// SAM 2017年5月24日14:58:49
        /// </summary>
        /// <param name="Token">授权码</param>
        /// <param name="Code">代号</param>
        /// <param name="page">页码</param>
        /// <param name="rows">行数</param>
        /// <returns></returns>
        public static object GetPlantAreaList(string token, string code, int page, int rows)
        {
            int count = 0;
            return UtilBussinessService.getPaginationModel(SYS_PlantAreaService.GetPlantAreaList(code, page, rows, ref count), count);
        }

        /// <summary>
        /// 获取仓库弹窗列表
        /// Tom 2017年6月29日01点23分
        /// </summary>
        /// <param name="Token"></param>
        /// <param name="Code"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <returns></returns>
        public static object GetWarehouseList(string token, string code, int page, int rows)
        {
            int count = 0;
            return UtilBussinessService.getPaginationModel(SYS_OrganizationService.GetWarehouseList(code, page, rows, ref count), count);
        }

        /// <summary>
        /// 部门的弹窗
        /// SAM 2017年5月19日16:07:08
        /// </summary>
        /// <param name="Token">授权码</param>
        /// <param name="Code">代号</param>
        /// <param name="page">页码</param>
        /// <param name="rows">行数</param>
        /// <returns></returns>
        public static object GetDeptList(string token, string Plant, string code, int page, int rows)
        {
            int count = 0;
            return UtilBussinessService.getPaginationModel(SYS_OrganizationService.GetDeptList(code, Plant, page, rows, ref count), count);
        }

        /// <summary>
        /// 感知器的弹窗
        /// SAM 2017年5月24日15:07:43
        /// </summary>
        /// <param name="Token">授权码</param>
        /// <param name="Code">代号</param>
        /// <param name="page">页码</param>
        /// <param name="rows">行数</param>
        /// <returns></returns>
        public static object GetSensorList(string token, string code, int page, int rows)
        {
            int count = 0;
            return UtilBussinessService.getPaginationModel(IOT_SensorService.GetSensorList(code, page, rows, ref count), count);
        }

        /// <summary>
        /// 获取厂商的弹窗
        /// SAM 2017年5月24日16:27:07
        /// </summary>
        /// <param name="Token">授权码</param>
        /// <param name="Code">代号</param>
        /// <param name="page">页码</param>
        /// <param name="rows">行数</param>
        /// <returns></returns>
        public static object GetManufacturerList(string token, string code,string Name, int page, int rows)
        {
            int count = 0;
            return UtilBussinessService.getPaginationModel(SYS_ManufacturersService.GetManufacturerList(code, Name, page, rows, ref count), count);
        }

        /// <summary>
        /// 获取客户的弹窗
        /// SAM 2017年6月21日16:40:40
        /// </summary>
        /// <param name="token"></param>
        /// <param name="code"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <returns></returns>
        public static object GetCustomerList(string token, string code, int page, int rows)
        {
            int count = 0;
            return UtilBussinessService.getPaginationModel(SYS_CustomersService.GetCustomerList(code, page, rows, ref count), count);
        }

        /// <summary>
        /// 获取项目的弹窗
        /// SAM 2017年5月25日17:10:08
        /// </summary>
        /// <param name="Token">授权码</param>
        /// <param name="Code">代号</param>
        /// <param name="page">页码</param>
        /// <param name="rows">行数</param>
        /// <returns></returns>
        public static object GetProjectList(string token, string code, int page, int rows)
        {
            int count = 0;
            return UtilBussinessService.getPaginationModel(SYS_ProjectsService.GetProjectList(code, page, rows, ref count), count);
        }

        /// <summary>
        /// 获取行事历的弹窗
        /// SAM 2017年5月26日09:37:00
        /// </summary>
        /// <param name="Token">授权码</param>
        /// <param name="Code">代号</param>
        /// <param name="page">页码</param>
        /// <param name="rows">行数</param>
        /// <returns></returns>
        public static object GetCalendarList(string token, string code, int page, int rows)
        {
            int count = 0;
            return UtilBussinessService.getPaginationModel(SYS_CalendarService.GetCalendarList(code, page, rows, ref count), count);
        }

        /// <summary>
        /// 设备的弹窗
        /// SAM 2017年5月26日14:21:35
        /// </summary>
        /// <param name="Token">授权码</param>
        /// <param name="StartCode"></param>
        /// <param name="EndCode"></param>
        /// <param name="page">页码</param>
        /// <param name="rows">行数</param>
        /// <returns></returns>
        public static object GetEquipmentList(string token, string Code, string StartCode, string EndCode, string Category, string Name, int page, int rows)
        {
            int count = 0;
            return UtilBussinessService.getPaginationModel(EMS_EquipmentService.GetEquipmentList(Code, StartCode, EndCode, Category, Name, page, rows, ref count), count);
        }

        /// <summary>
        /// 获取设备项目的弹窗
        /// SAM 2017年5月27日11:03:48
        /// </summary>
        /// <param name="Token">授权码</param>
        /// <param name="EquipmentID"></param>
        /// <param name="page">页码</param>
        /// <param name="rows">行数</param>
        /// <returns></returns>
        public static object GetEquipmentProjectList(string token, string EquipmentID, string Code, int page, int rows)
        {
            int count = 0;
            return UtilBussinessService.getPaginationModel(EMS_EquipmentProjectService.EquipmentProjectList(EquipmentID, Code, page, rows, ref count), count);
        }

        /// <summary>
        /// 获取设备巡检项目的弹窗
        /// SAM 2017年6月8日15:08:32
        /// </summary>
        /// <param name="token"></param>
        /// <param name="Code"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <returns></returns>
        public static object GetEquipmentInspectionProjectList(string token, string Code, int page, int rows)
        {
            int count = 0;
            return UtilBussinessService.getPaginationModel(EMS_EquipmentInspectionProjectService.GetEquipmentInspectionProjectList(Code, page, rows, ref count), count);
        }

        /// <summary>
        /// 获取料品的弹窗
        /// SAM 2017年6月8日15:08:08
        /// </summary>
        /// <param name="token"></param>
        /// <param name="Code"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <returns></returns>
        public static object GetItemList(string token, string Code, string Name,int page, int rows)
        {
            int count = 0;
            return UtilBussinessService.getPaginationModel(SYS_ItemsService.GetItemList(Code, Name,page, rows, ref count), count);
        }

        /// <summary>
        /// SFC专用获取料品的弹窗（排除供應型態为4（採購件）的）
        /// SAM 2017年7月23日00:08:36
        /// </summary>
        /// <param name="token"></param>
        /// <param name="Code"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <returns></returns>
        public static object SfcGetItemList(string token, string Type,string Code, string StartCode, string EndCode, int page, int rows)
        {
            int count = 0;
            return UtilBussinessService.getPaginationModel(SYS_ItemsService.SfcGetItemList(Code, Type, StartCode, EndCode, page, rows, ref count), count);
        }

        /// <summary>
        /// SFC专用获取料品的弹窗（排除供應型態为4（採購件）的）
        /// Mouse 2017年10月23日16:37:19  起始不区分大小写
        /// </summary>
        /// <param name="token"></param>
        /// <param name="Code"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <returns></returns>
        public static object Sfc00023GetItemList(string token, string Type, string Code, string StartCode, string EndCode, int page, int rows)
        {
            int count = 0;
            return UtilBussinessService.getPaginationModel(SYS_ItemsService.Sfc00023GetItemList(Code, Type, StartCode, EndCode, page, rows, ref count), count);
        }

        /// <summary>
        /// 获取资源的弹窗
        /// SAm 2017年5月27日16:13:48
        /// </summary>
        /// <param name="Token">授权码</param>
        /// <param name="Code">代号</param>
        /// <param name="page">页码</param>
        /// <param name="rows">行数</param>
        /// <returns></returns>
        public static object GetResourceList(string token, string Code, int page, int rows)
        {
            int count = 0;
            return UtilBussinessService.getPaginationModel(SYS_ResourcesService.GetResourceList(Code, page, rows, ref count), count);
        }

        /// <summary>
        /// 获取原因码的弹窗
        /// SAM 2017年5月29日23:35:26
        /// </summary>
        /// <param name="Token">授权码</param>
        /// <param name="type"></param>
        /// <param name="Code">代号</param>
        /// <param name="page">页码</param>
        /// <param name="rows">行数</param>
        /// <returns></returns>
        public static object getReasonList(string Token, string type,string GroupID, string Code, string GroupDescription, string Description, int page, int rows)
        {
            int Count = 0;
            return UtilBussinessService.getPaginationModel(SYS_ParameterService.getReasonList(type, GroupID, Code, GroupDescription, Description, page, rows, ref Count), Count);
        }

        /// <summary>
        /// 获取班别的弹窗
        /// SAM 2017年6月14日09:38:42
        /// </summary>
        /// <param name="token"></param>
        /// <param name="code"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <returns></returns>
        public static object GetSYSClassList(string token, string code, int page, int rows)
        {
            int count = 0;
            return UtilBussinessService.getPaginationModel(SYS_ClassService.GetClassList(code, page, rows, ref count), count);
        }



        /// <summary>
        /// 工作中心的弹窗
        /// SAM 2017年6月20日14:48:59
        /// </summary>
        /// <param name="token"></param>
        /// <param name="ProcessID"></param>
        /// <param name="code"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <returns></returns>
        public static object GetWorkCenterList(string token, string ProcessID, string code, int page, int rows)
        {
            int count = 0;
            IList<Hashtable> result = SYS_WorkCenterService.GetWorkCenterList(ProcessID, code, page, rows, ref count);
            return UtilBussinessService.getPaginationModel(result, count);
        }


        /// <summary>
        /// 制品制程的料品专属弹窗
        /// SAM 2017年6月21日09:30:16
        /// </summary>
        /// <param name="token"></param>
        /// <param name="StartCode"></param>
        /// <param name="EndCode"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <returns></returns>
        public static object Sfc00001GetItemList(string token, string StartCode, string EndCode, int page, int rows)
        {
            int count = 0;
            return UtilBussinessService.getPaginationModel(SYS_ItemsService.Sfc00001GetItemList(StartCode, EndCode, page, rows, ref count), count);
        }


        /// <summary>
        /// 制品制程的资源群组专属开窗
        /// SAM 2017年6月21日09:52:30
        /// </summary>
        /// <param name="token"></param>
        /// <param name="WorkCenterID"></param>
        /// <param name="GroupCode"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <returns></returns>
        public static object Sfc00001GetGroupList(string token, string WorkCenterID, string GroupCode, string Type, int page, int rows)
        {
            int count = 0;
            return UtilBussinessService.getPaginationModel(SYS_ResourcesService.Sfc00001GetGroupList(WorkCenterID, GroupCode, Type, page, rows, ref count), count);
        }


        /// <summary>
        /// 客诉单的开窗
        /// SAM 2017年6月21日17:49:21
        /// </summary>
        /// <param name="token"></param>
        /// <param name="Code"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <returns></returns>
        public static object GetComplaintList(string token, string Code, int page, int rows)
        {
            int count = 0;
            return UtilBussinessService.getPaginationModel(QCS_ComplaintService.GetComplaintList(Code, page, rows, ref count), count);
        }

        ///<summary>
        ///開窗讀取製令母件表
        ///Joint 2017年6月27日11:23:10
        ///</summary>
        ///<param name="Token">授权码</param>
        ///<param name="MoNo">制令单号</param>
        ///<param name="page">页码</param>
        ///<param name"rows">行数</param>

        public static object Sfc00003GetFabricatedMother(string Token, string MoNo, int page, int rows)
        {
            int count = 0;
            return UniversalService.getPaginationModel(SFC_FabricatedMotherService.Sfc00003GetFabricatedMother(MoNo, page, rows, ref count), count);
        }

        ///<summary>
        ///開窗讀取制品制程表
        ///Joint 2017年6月27日14:34:53
        ///</summary>
        ///<param name="Token">授权码</param>
        ///<param name="ItemID">料品流水号</param>
        ///<param name="page">页码</param>
        ///<param name"rows">行数</param>
        public static object Sfc00003GetItemProcessList(string Token, string Code, int page, int rows)
        {
            int count = 0;
            return UniversalService.getPaginationModel(SFC_ItemProcessService.Sfc00003GetItemProcessList(Code, page, rows, ref count), count);
        }

        /// <summary>
        /// 制品工序的专属弹窗
        /// SAM 2017年6月27日16:50:32
        /// </summary>
        /// <param name="token"></param>
        /// <param name="ProcessID"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <returns></returns>
        public static object Sfc00001GetOperationList(string token, string ProcessID, string Code, int page, int rows)
        {
            int count = 0;
            return UtilBussinessService.getPaginationModel(SYS_ParameterService.Sfc00001GetOperationList(ProcessID, Code, page, rows, ref count), count);
        }

        /// <summary>
        /// 制品制程关系专属弹窗
        /// SAM 2017年6月27日22:13:22
        /// </summary>
        /// <param name="Token"></param>
        /// <param name="ItemID"></param>
        /// <param name="code"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <returns></returns>
        public static object Sfc00001GetItemProcessList(string Token, string ItemID, string code, int page, int rows)
        {
            int count = 0;
            return UniversalService.getPaginationModel(SFC_ItemProcessService.Sfc00001GetItemProcessList(ItemID, code, page, rows, ref count), count);
        }


        /// <summary>
        /// 制品制程工序专属开窗
        /// SAM 2017年6月27日22:21:35
        /// </summary>
        /// <param name="Token"></param>
        /// <param name="ItemProcessID"></param>
        /// <param name="code"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <returns></returns>
        public static object Sfc00001GetProcessOperationList(string Token, string ItemProcessID, string code, int page, int rows)
        {
            int count = 0;
            return UniversalService.getPaginationModel(SFC_ItemOperationService.Sfc00001GetProcessOperationList(ItemProcessID, code, page, rows, ref count), count);
        }

        /// <summary>
        /// 完工回报主档读取
        /// </summary>
        /// <param name="Token"></param>
        /// <param name="CompletionNo"></param>
        /// <param name="Code"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <returns></returns>
        public static object GetFinishList(string Token, string CompletionNo, string Code, int page, int rows)
        {
            int count = 0;
            return UniversalService.getPaginationModel(SFC_CompletionOrderService.GetFinishList(CompletionNo, Code, page, rows, ref count), count);
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
        public static object GetFinishListV1(string Token, string CompletionNo, string Code, int page, int rows)
        {
            int count = 0;
            return UniversalService.getPaginationModel(SFC_CompletionOrderService.GetFinishListV1(CompletionNo, Code, page, rows, ref count), count);
        }

        /// <summary>
        /// QCS00004获取检验项目的下拉框
        /// SAM 2017年7月6日15:10:46
        /// </summary>
        /// <param name="Token"></param>
        /// <returns></returns>
        public static IList<Hashtable> QCS00004GetInspectionProjectList(string Token)
        {
            return QCS_InspectionProjectService.QCS00004GetInspectionProjectList();
        }

        /// <summary>
        /// 不存在于工单和清单中的设备弹窗
        /// SAM 2017年7月9日16:49:01
        /// </summary>
        /// <param name="Token"></param>
        /// <param name="MaintenanceOrderID"></param>
        /// <param name="EquipmentMaintenanceListID"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <returns></returns>
        public static IList<Hashtable> EMSGetEquMaiEquipmentList(string Token, string MaintenanceOrderID, string EquipmentMaintenanceListID, string Code, string Name)
        {
            return EMS_EquipmentService.EMSGetEquMaiEquipmentList(MaintenanceOrderID, EquipmentMaintenanceListID, Code, Name);
        }

        /// <summary>
        /// 不存在于保养工单的保养项目列表
        /// SAM 2017年7月9日17:00:43
        /// </summary>
        /// <param name="Token"></param>
        /// <param name="MaintenanceOrderID"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <returns></returns>
        public static IList<Hashtable> EMSGetEquMaiProjectList(string Token, string MaintenanceOrderID)
        {
            return SYS_ParameterService.EMSGetEquMaiProjectList(MaintenanceOrderID);
        }

        /// <summary>
        /// 检验单的开窗
        /// SAM 2017年7月9日20:48:10
        /// </summary>
        /// <param name="Token"></param>
        /// <param name="code"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <returns></returns>
        public static object QCSInspectionDocumentList(string Token, string code, int page, int rows)
        {
            int count = 0;
            return UniversalService.getPaginationModel(QCS_InspectionDocumentService.QCSInspectionDocumentList(code, page, rows, ref count), count);
        }

        /// <summary>
        /// 任务单的开窗
        /// SAM 2017年9月6日17:15:59
        /// </summary>
        /// <param name="Token"></param>
        /// <param name="TaskNo"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <returns></returns>
        public static object TaskDispatchList(string Token, string TaskNo, int page, int rows)
        {
            int count = 0;
            return UniversalService.getPaginationModel(SFC_TaskDispatchService.TaskDispatchList(TaskNo, page, rows, ref count), count);
        }



        /// <summary>
        /// 任务单的开窗
        /// SAM 2017年7月9日20:59:17
        /// Mouse  2017年9月4日17:54:45 修改
        /// </summary>
        /// <param name="Token"></param>
        /// <param name="TaskNo"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <returns></returns>
        public static object Qcs00007TaskDispatchList(string Token, string TaskNo, int page, int rows)
        {
            int count = 0;
            return UniversalService.getPaginationModel(SFC_TaskDispatchService.Qcs00007TaskDispatchList(TaskNo, page, rows, ref count), count);
        }

        /// <summary>
        /// 任务单的开窗
        /// Mouse 2017年11月1日09:49:13 需求修改
        /// </summary>
        /// <param name="Token"></param>
        /// <param name="TaskNo"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <returns></returns>
        public static object Qcs00007TaskDispatchListV1(string Token, string TaskNo, string ProcessCode, string ProcessName, int page, int rows)
        {
            int count = 0;
            return UniversalService.getPaginationModel(SFC_TaskDispatchService.Qcs00007TaskDispatchListV1(TaskNo, ProcessCode, ProcessName, page, rows, ref count), count);
        }

        /// <summary>
        /// 新的任务单的开窗
        /// MOUSE 2017年7月9日20:59:17
        /// </summary>
        /// <param name="Token"></param>
        /// <param name="TaskNo"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <returns></returns>
        public static object Qcs00008TaskDispatchList(string Token, string TaskNo, int page, int rows)
        {
            int count = 0;
            return UniversalService.getPaginationModel(SFC_TaskDispatchService.Qcs00008TaskDispatchList(TaskNo, page, rows, ref count), count);
        }

        /// <summary>
        /// Sfc00004获取资源明细的专属弹窗
        /// SAM 2017年7月10日17:04:41
        /// </summary>
        /// <param name="Token"></param>
        /// <param name="Code"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <returns></returns>
        public static object Sfc00004GetResourceDetailList(string Token, string Code, string FabricatedProcessID, string FabMoOperationID, int page, int rows)
        {
            int count = 0;
            IList<Hashtable> result = null;
            if (string.IsNullOrWhiteSpace(Code))
                result = SYS_ResourceDetailsService.Sfc00004GetResourceDetailList(FabricatedProcessID, FabMoOperationID, page,rows,ref count);
            else if (Code == "L")
                result = SYS_ResourceDetailsService.Sfc00004GetLResourceDetailList(FabricatedProcessID, FabMoOperationID, page, rows, ref count);
            else if (Code == "M")
                result = SYS_ResourceDetailsService.Sfc00004GetMResourceDetailList(FabricatedProcessID, FabMoOperationID, page, rows, ref count);
            return UniversalService.getPaginationModel(result, count);
        }


        /// <summary>
        /// Ems00009的检修人弹窗
        /// SAM 2017年7月12日14:42:38
        /// </summary>
        /// <param name="token"></param>
        /// <param name="OrganizationCode"></param>
        /// <param name="OrganizationName"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <returns></returns>
        public static object Ems00009GetUserList(string token, string OrganizationCode, string OrganizationName, int page, int rows)
        {
            int count = 0;
            return UtilBussinessService.getPaginationModel(SYS_MESUserService.Ems00009GetUserList(OrganizationCode, OrganizationName, page, rows, ref count), count);
        }


        /// <summary>
        /// 制令工单-根据制令制程获取下属工序弹窗
        /// SAM 2017年7月14日11:49:42
        /// </summary>
        /// <param name="token"></param>
        /// <param name="FabMoProcessID"></param>
        /// <param name="Code"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <returns></returns>
        public static object SfcGetFabMoOperationList(string token, string FabMoProcessID, string Code, int page, int rows)
        {
            int count = 0;
            return UtilBussinessService.getPaginationModel(SFC_FabMoOperationService.SfcGetFabMoOperationList(FabMoProcessID, Code, page, rows, ref count), count);
        }


        /// <summary>
        /// 制令工单-获取制令单下的制程弹窗
        /// SAM 2017年7月14日15:52:17
        /// </summary>
        /// <param name="token"></param>
        /// <param name="FabricatedMotherID"></param>
        /// <param name="Code"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <returns></returns>
        public static object SfcGetFabMoProcessList(string token, string FabricatedMotherID, string Code, int page, int rows)
        {
            int count = 0;
            return UtilBussinessService.getPaginationModel(SFC_FabMoProcessService.SfcGetFabMoProcessList(FabricatedMotherID, Code, page, rows, ref count), count);
        }

        /// <summary>
        /// 获取资源类别弹窗列表
        /// Tom 2017年7月24日16:25:56
        /// </summary>
        /// <param name="token"></param>
        /// <param name="code"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <returns></returns>
        public static object SfcGetResourceClassList(string token, string code, int page, int rows)
        {
            int count = 0;
            return UtilBussinessService.getPaginationModel(SYS_ParameterService.Inf00015ClassList(code, "1", page, rows, ref count), count);
        }

        /// <summary>
        /// 根据资源类别获取资源
        /// Tom 2017年7月24日17:27:42 
        /// </summary>
        /// <param name="token"></param>
        /// <param name="classID"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <returns></returns>
        public static object SfcGetResourceByClass(string token, string TaskDispatchID, string classID, int page, int rows)
        {
            int count = 0;
            return UtilBussinessService.getPaginationModel(SFC_TaskDispatchResourceService.SfcGetResourceByClass(TaskDispatchID, classID, page, rows, ref count), count);
        }

        /// <summary>
        /// 制令单的弹窗
        /// SAM 2017年7月27日14:02:15
        /// </summary>
        /// <param name="Token"></param>
        /// <param name="MoNo"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <returns></returns>
        public static object Sfc00010GetFabricatedMother(string Token, string MoNo, int page, int rows)
        {
            int count = 0;
            return UniversalService.getPaginationModel(SFC_FabricatedMotherService.Sfc00010GetFabricatedMother(MoNo, page, rows, ref count), count);
        }

        /// <summary>
        /// Sfc02的制令单开窗
        /// SAM 2017年8月30日17:00:021
        /// </summary>
        /// <param name="Token"></param>
        /// <param name="MoNo"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <returns></returns>
        public static object Sfc00002GetFabMother(string Token, string MoNo,string ItemName, int page, int rows)
        {
            int count = 0;
            return UniversalService.getPaginationModel(SFC_FabricatedMotherService.Sfc00002GetFabricatedMother(MoNo, ItemName, page, rows, ref count), count);
        }
        
        /// <summary>
        /// Sfc02的制令单开窗
        /// Mouse 2017年9月26日14:48:01
        /// </summary>
        /// <param name="Token"></param>
        /// <param name="MoNo"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <returns></returns>
        public static object Sfc00020GetFabricatedMother(string Token, string MoNo,string ItemName, int page, int rows)
        {
            int count = 0;
            return UniversalService.getPaginationModel(SFC_FabricatedMotherService.Sfc00020GetFabricatedMother(MoNo,ItemName, page, rows, ref count), count);
        }
        /// <summary>
        /// 设备巡检维护任务单弹窗
        /// Tom 2017年7月28日17:40:38
        /// </summary>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <returns></returns>
        public static object Ems0003GetTaskDispatchList(string EquipmentID,int page, int rows)
        {
            int count = 0;
            return UtilBussinessService.getPaginationModel(SFC_TaskDispatchService.Ems0003GetTaskDispatchList(EquipmentID,page, rows, ref count), count);
        }
        /// <summary>
        /// Iot00003设备弹窗
        /// Mouse 2017年9月6日18:04:08
        /// </summary>
        /// <param name="EquipmentID"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <returns></returns>
        public static object Iot00003GetEquipment(string EquipmentID,int page ,int rows)
        {
            int count = 0;
            return UtilBussinessService.getPaginationModel(EMS_EquipmentService.Iot00003GetEquipment(EquipmentID, page, rows, ref count), count);
        }

        /// <summary>
        /// Iot00003设备弹窗
        /// Mouse 2017年9月6日18:04:08
        /// </summary>
        /// <param name="EquipmentID"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <returns></returns>
        public static object Iot00003GetEquipmentV1(string EquipmentID, string Code, int page, int rows)
        {
            int count = 0;
            return UtilBussinessService.getPaginationModel(EMS_EquipmentService.Iot00003GetEquipmentV1(EquipmentID, Code, page, rows, ref count), count);
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
        public static IList<Hashtable> GetLanguageList(string ObjectID)
        {
            return SYS_ParameterService.GetLanguageList(ObjectID);
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
        public static object Sfc00017GetItemList(string token, string Code, string Name, int page, int rows)
        {
            int count = 0;
            return UtilBussinessService.getPaginationModel(SYS_ItemsService.Sfc00017GetItemList(Code, Name, page, rows, ref count), count);
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
        public static object Sfc00017GetFabMoList(string Token, string MoNo, string ItemName, int page, int rows)
        {
            int count = 0;
            return UniversalService.getPaginationModel(SFC_FabricatedMotherService.Sfc00017GetFabMoList(MoNo, ItemName, page, rows, ref count), count);
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
        public static object SfcGetFabMoList(string Token, string MoNo,string ItemName, int page, int rows)
        {
            int count = 0;
            return UniversalService.getPaginationModel(SFC_FabricatedMotherService.SfcGetFabMoList(MoNo, ItemName, page, rows, ref count), count);
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
        public static object SfcGetCLFabMoList(string Token, string MoNo, int page, int rows)
        {
            int count = 0;
            IList<Hashtable> result = SFC_FabricatedMotherService.SfcStatusGetFabMoList(Framework.SystemID + "020121300002A", MoNo, page, rows, ref count);
            return UniversalService.getPaginationModel(result, count);
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
        public static object SfcItemList(string Token, string Code, int page, int rows)
        {
            int count = 0;
            IList<Hashtable> result = SYS_ItemsService.SfcItemList(Code, page, rows, ref count);
            return UniversalService.getPaginationModel(result, count);
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
        public static object SfcWorkCenterList(string Token, string Code, int page, int rows)
        {
            int count = 0;
            IList<Hashtable> result = SYS_WorkCenterService.SfcWorkCenterList(Code, page, rows, ref count);
            return UniversalService.getPaginationModel(result, count);
        }

        /// <summary>
        /// 获取制程的列表
        /// Sam 2017年10月30日14:35:29
        /// Code为单个起始区间查询，不包括本身
        /// Name为单个起始区间查询，不包括本身
        /// </summary>
        /// <param name="Token"></param>
        /// <param name="Code"></param>
        /// <param name="Name"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <returns></returns>
        public static object SfcProcessList(string Token, string Code, string Name,int page, int rows)
        {
            int count = 0;
            IList<Hashtable> result = SYS_ParameterService.SfcProcessList(Code, Name, page, rows, ref count);
            return UniversalService.getPaginationModel(result, count);
        }
    }
}
