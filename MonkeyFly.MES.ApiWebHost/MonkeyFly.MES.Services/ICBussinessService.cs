using MonkeyFly.Core;
using MonkeyFly.MES.Models;
using MonkeyFly.MES.ModelServices;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonkeyFly.MES.Services
{
    public class ICBussinessService
    {
        #region IOT00001感知器主档
        /// <summary>
        /// 感知器列表
        /// SAM 2017年5月23日12:03:19
        /// </summary>
        /// <param name="Token">授权码</param>
        /// <param name="Code">代号</param>
        /// <param name="Status">状态</param>
        /// <param name="page">页码</param>
        /// <param name="rows">行数</param>
        /// <returns></returns>
        public static object lot00001GetList(string token, string code, string status, int page, int rows)
        {
            int count = 0;
            IList<Hashtable> result = IOT_SensorService.lot00001GetList(code, status, page, rows, ref count);
            return UtilBussinessService.getPaginationModel(result, count);
        }

        /// <summary>
        /// 添加感知器
        /// SAM 2017年5月23日12:19:30
        /// </summary>
        /// <param name="Token">授权码</param>
        /// <param name="jArray"></param>
        /// <returns></returns>
        public static object lot00001insert(string Token, JArray jArray)
        {
            int success = 0;
            int fail = 0;
            string failIDs = null;
            string msg = null;
            string userid = UtilBussinessService.detoken(Token);
            JObject data = null;
            IOT_Sensor model = null;
            for (int i = 0; i < jArray.Count; i++)
            {
                data = (JObject)jArray[i];
                if (!IOT_SensorService.CheckCode(data.Value<string>("Code"), null))
                {
                    model = new IOT_Sensor();
                    model.SensorID = UniversalService.GetSerialNumber("IOT_Sensor");
                    model.Code = data.Value<string>("Code");
                    model.Name = data.Value<string>("Name");
                    model.Status = data.Value<string>("Status");
                    model.Comments = data.Value<string>("Comments");
                    if (!string.IsNullOrWhiteSpace(data.Value<string>("EnabledDate")))
                        model.EnabledDate = data.Value<DateTime>("EnabledDate");
                    if (!string.IsNullOrWhiteSpace(data.Value<string>("FailureDate")))
                        model.FailureDate = data.Value<DateTime>("FailureDate");
                    model.Brand = data.Value<string>("Brand");
                    model.Type = data.Value<string>("Type");
                    model.ManufacturerID = data.Value<string>("ManufacturerID");
                    model.IsWarning = data.Value<bool>("IsWarning");
                    if (!string.IsNullOrWhiteSpace(data.Value<string>("MaxAlarmTime")))
                        model.MaxAlarmTime = data.Value<int>("MaxAlarmTime");
                    if (!string.IsNullOrWhiteSpace(data.Value<string>("MinAlarmTime")))
                        model.MinAlarmTime = data.Value<int>("MinAlarmTime");
                    if (!string.IsNullOrWhiteSpace(data.Value<string>("MaxValue")))
                        model.MaxValue = data.Value<decimal>("MaxValue");
                    if (!string.IsNullOrWhiteSpace(data.Value<string>("MinValue")))
                        model.MinValue = data.Value<decimal>("MinValue");
                    if (IOT_SensorService.insert(userid, model))
                        success++;
                    else
                    {
                        failIDs = UtilBussinessService.str(failIDs, data.Value<string>("SensorID"));
                        fail++;
                    }
                }
                else
                {
                    failIDs = UtilBussinessService.str(failIDs, data.Value<string>("SensorID"));
                    msg = UtilBussinessService.str(msg, data.Value<string>("Code") + "已存在");
                    fail++;
                }
            }
            return new { success = success, fail = fail, failIDs = failIDs, msg = msg };
        }

        /// <summary>
        /// 删除感知器
        /// SAM 2017年5月23日12:23:42
        /// </summary>
        /// <param name="Token">授权码</param>
        /// <param name="jArray"></param>
        /// <returns></returns>
        public static object lot00001delete(string Token, JArray jArray)
        {
            int success = 0;
            int fail = 0;
            string failIDs = null;
            string msg = null;
            string userid = UtilBussinessService.detoken(Token);
            JObject data = null;
            IOT_Sensor model = null;
            for (int i = 0; i < jArray.Count; i++)
            {
                data = (JObject)jArray[i];
                model = IOT_SensorService.get(data.Value<string>("SensorID"));
                if (!EMS_EquipmentProjectService.CheckSensor(data.Value<string>("SensorID")))
                {
                    model.Status = Framework.SystemID + "0201213000003";
                    if (IOT_SensorService.update(userid, model))
                        success++;
                    else
                    {
                        failIDs = UtilBussinessService.str(failIDs, data.Value<string>("SensorID"));
                        fail++;
                    }
                }
                else
                {
                    failIDs = UtilBussinessService.str(failIDs, data.Value<string>("SensorID"));
                    msg = UtilBussinessService.str(failIDs, model.Code + "已使用，不能删除");
                    fail++;
                }

            }
            return new { success = success, fail = fail, failIDs = failIDs, msg = msg };
        }

        /// <summary>
        /// 更新感知器
        /// SAM 2017年5月23日12:24:45
        /// </summary>
        /// <param name="Token">授权码</param>
        /// <param name="jArray"></param>
        /// <returns></returns>
        public static object lot00001update(string Token, JArray jArray)
        {
            int success = 0;
            int fail = 0;
            string failIDs = null;
            string msg = null;
            string userid = UtilBussinessService.detoken(Token);
            JObject data = null;
            IOT_Sensor model = null;
            for (int i = 0; i < jArray.Count; i++)
            {
                data = (JObject)jArray[i];
                model = IOT_SensorService.get(data.Value<string>("SensorID"));
                model.Status = data.Value<string>("Status");
                if (!string.IsNullOrWhiteSpace(data.Value<string>("EnabledDate")))
                    model.EnabledDate = data.Value<DateTime>("EnabledDate");
                else
                    model.EnabledDate = null;
                if (!string.IsNullOrWhiteSpace(data.Value<string>("FailureDate")))
                    model.FailureDate = data.Value<DateTime>("FailureDate");
                else
                    model.FailureDate = null;
                model.Brand = data.Value<string>("Brand");
                model.Type = data.Value<string>("Type");
                model.ManufacturerID = data.Value<string>("ManufacturerID");
                model.IsWarning = data.Value<bool>("IsWarning");
                if (!string.IsNullOrWhiteSpace(data.Value<string>("MaxAlarmTime")))
                    model.MaxAlarmTime = data.Value<int>("MaxAlarmTime");
                else
                    model.MaxAlarmTime = null;
                if (!string.IsNullOrWhiteSpace(data.Value<string>("MinAlarmTime")))
                    model.MinAlarmTime = data.Value<int>("MinAlarmTime");
                else
                    model.MinAlarmTime = null;
                if (IOT_SensorService.update(userid, model))
                    success++;
                else
                {
                    failIDs = UtilBussinessService.str(failIDs, data.Value<string>("SensorID"));
                    fail++;
                }


            }
            return new { success = success, fail = fail, failIDs = failIDs, msg = msg };
        }

        #endregion

        #region Iot00002
        /// <summary>
        /// 设备管理获取列表
        /// Alvin 2017年9月4日17:36:03
        /// </summary>
        /// <param name="token"></param>
        /// <param name="plantCode"></param>
        /// <param name="plantAreaCode"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <returns></returns>
        public static object Iot00002GetList(string token, string plantCode, string plantAreaCode )
        {
            //int count = 0;
            IList<Hashtable> result = EMS_EquipmentService.Iot00002GetList(token, plantCode, plantAreaCode);
            return result;
        }
        #endregion

        #region IOT00003机台设备监控
        /// <summary>
        /// 机台设备监控 设备列表
        /// Mouse 2017年9月6日11:28:19
        /// </summary>
        /// <param name="Token"></param>
        /// <param name="Code"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <returns></returns>
        public static object Iot00003GetEquipmentList(string Token,string EquipmentID, int page,int rows)
        {
            int count = 0;
            IList<Hashtable> result = EMS_EquipmentService.Iot00003GetEquipmentList(EquipmentID, page, rows, ref count);
            return UtilBussinessService.getPaginationModel(result, count);
        }

        /// <summary>
        /// 机台设备监控 设备列表
        /// Mouse 2017年9月6日11:28:19
        /// 2017年11月10日10:49:02
        /// </summary>
        /// <param name="Token"></param>
        /// <param name="Code"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <returns></returns>
        public static object Iot00003GetEquipmentListV1(string Token, string EquipmentID, int page, int rows)
        {
            int count = 0;
            IList<Hashtable> result = EMS_EquipmentService.Iot00003GetEquipmentListV1(EquipmentID, page, rows, ref count);
            return UtilBussinessService.getPaginationModel(result, count);
        }

        /// <summary>
        /// Iot00003工作中心按钮
        /// </summary>
        /// <param name="Token"></param>
        /// <param name="EquipmentID"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <returns></returns>
        public static object Iot00003GetCenter(string Token,string EquipmentID,int page,int rows)
        {
            int count = 0;
            return EMS_EquipmentService.Iot00003GetCenter(EquipmentID, page, rows, ref count);
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
        public static object Iot00003GetCenterProcess(string Token,string WorkCenterID,int page,int rows)
        {
            int count = 0;
            return SYS_WorkCenterProcessService.Iot00003GetCenterProcess(WorkCenterID, page, rows, ref count);
        }

        /// <summary>
        /// 查询工作中心是否于制程存在关系
        /// Mouse 2017年9月12日18:13:56
        /// </summary>
        /// <param name="Token"></param>
        /// <param name="WorkCenterID"></param>
        /// <returns></returns>
        public static bool Iot00003CheckCenterProcess(string Token,string WorkCenterID)
        {
            return SYS_WorkCenterProcessService.Iot00003CheckCenterProcess(WorkCenterID);
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
        public static object Iot00003GetMomProcess(string Token, string EquipmentID, int page, int rows)
        {
            //先查找到该任务资源明细有用到该设备代号且状态为IN或OP的任务卡
            IList<SFC_TaskDispatch> Task = SFC_TaskDispatchService.GetIot00003EquipmentTask(EquipmentID);
            if (Task.Count == 0)
            {
                return new { Status = "410", msg = "任务明细资源没有用到该设备代号且状态为IN或OP任务卡！" };
            }

            //对比失败,GG
            List<Hashtable> SFCFMP = new List<Hashtable>();
            //for (int i = 0; i < Task.Count; i++)
            foreach(SFC_TaskDispatch TaskStatu in Task)
            {
                //SFC_TaskDispatch TaskStatu = Task[i];
                //任务单已发派且分派量大于0
                if (TaskStatu.IsDispatch == true && TaskStatu.DispatchQuantity > 0)
                {
                    //任务单制程工序没有资料
                    if (string.IsNullOrWhiteSpace(TaskStatu.OperationID))
                    {
                        //串联制令制程档
                        SFCFMP.Add(SFC_FabMoProcessService.Iot00003GetMomProcessNoOperation(TaskStatu.FabMoProcessID));
                    }
                    //任务单制程工序有资料
                    else
                    {
                        SFCFMP.Add(SFC_FabMoOperationService.Iot00003GetMomProcessHasOperation(TaskStatu.FabMoOperationID));
                    }
                }
                //任务单未分派
                else
                {
                    //获取设备的资源明细ID后连接制令资源找到制令制程，与制令制程工序
                    SFC_FabMoResource FMRmodel = SFC_FabMoResourceService.Iot00003GetFabMoResource(EquipmentID);
                    //资源表中的制令制程工序流水号为空
                    if (string.IsNullOrWhiteSpace(FMRmodel.FabMoOperationID))
                    {
                        SFCFMP.Add(SFC_FabMoProcessService.Iot00003GetMomProcessNoOperation(FMRmodel.FabMoProcessID));
                    }
                    //资源表中的制令制程工序流水号不为空
                    else
                    {
                        SFCFMP.Add(SFC_FabMoOperationService.Iot00003GetMomProcessHasOperation(FMRmodel.FabMoOperationID));
                    }
                }
            }

            if (SFCFMP == null)
            {
                return new { Status = "410", msg = "未知错误" };
            }
            else
            {
                return SFCFMP;
            }
        }

        /// <summary>
        /// Iot00003设备事件按钮
        /// Mouse 2017年9月13日16:50:03
        /// </summary>
        /// <param name="Token"></param>
        /// <param name="EquipmentID"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <returns></returns>
        public static object Iot00003GetDaqEvent(string Token,string EquipmentID,int page,int rows)
        {
            int count = 0;
            return UtilBussinessService.getPaginationModel(IOT_DAQ_EVENTService.Iot00003GetDaqEvent(EquipmentID, page, rows,ref count), count);
        }
        /// <summary>
        /// Iot00003设备监控资料
        /// Mouse 2017年9月14日10:27:20
        /// </summary>
        /// <param name="Token"></param>
        /// <param name="EquipmentID"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <returns></returns>
        public static object Iot00003GetDaqValue(string Token,string EquipmentID)
        {
            
            List<Hashtable> x = new List<Hashtable>();
            IList<SYS_Projects> d = SYS_ProjectsService.Iot00003GetEquipmentProject(EquipmentID);//根据流水号获取他所有的设备项目代号
            foreach(var m in d) {
                if(IOT_DAQ_VALUEService.Iot00003GetDaqValue(EquipmentID, m.Code)!=null)//没有再DAQValue表找到设备项目代号相同的数据，则返回空
                x.Add(IOT_DAQ_VALUEService.Iot00003GetDaqValue(EquipmentID, m.Code));
            }

            return x;
        }

        /// <summary>
        /// Iot00003DAQ现值趋势图
        /// Mouse 2017年9月14日11:09:09
        /// </summary>
        /// <param name="Token"></param>
        /// <param name="EquipmentID"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <returns></returns>
        public static object Iot00003GetMeasurementQST(string Token,string EquipmentID,string Sensor)
        {
            //DateTime timeend = DateTime.Now;  测试需要因为数据问题暂时把时间设为固定改回
            DateTime timeend = DateTime.Now;
            DateTime timestar = timeend.AddMinutes(-1);
            List<Hashtable> data = new List<Hashtable>();
            List<Hashtable> Value = new List<Hashtable>();
            for (int i = 1; i <= 20; i++)
            {
                if (i != 1)
                {
                    timeend = timestar;
                    timestar = timeend.AddMinutes(-1);
                }
                data.Add(IOT_DAQ_VALUEService.Iot00003GetMeasurementQSTTime(EquipmentID, timestar, timeend, Sensor));
            }
            Value.Add(IOT_DAQ_VALUEService.Iot00003GetMeasurementQSTValue(EquipmentID, timestar, timeend, Sensor));
            return new { data =data,Value=Value};
        }
        #endregion
    }
}
