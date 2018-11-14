using MonkeyFly.Core;
using MonkeyFly.MES.BasicService;
using MonkeyFly.MES.Models;
using MonkeyFly.MES.ModelServices;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace MonkeyFly.MES.Services
{
    public class EMBussinessService
    {
        #region EMS00001设备主档
        /// <summary>
        /// 设备主档列表
        /// SAM 2017年5月22日11:10:03
        /// </summary>
        /// <param name="Token">授权码</param>
        /// <param name="Code">代号</param>
        /// <param name="Status">状态</param>
        /// <param name="page">页码</param>
        /// <param name="rows">行数</param>
        /// <returns></returns>
        public static object Ems00001GetList(string token, string code, string status, int page, int rows)
        {
            int count = 0;
            IList<Hashtable> result = EMS_EquipmentService.Ems00001GetList(code, status, page, rows, ref count);
            return UtilBussinessService.getPaginationModel(result, count);
        }

        /// <summary>
        /// 添加设备
        /// SAM 2017年5月22日14:42:18
        /// </summary>
        /// <param name="Token">授权码</param>
        /// <param name="jArray"></param>
        /// <returns></returns>
        public static object Ems00001insert(string Token, JArray jArray)
        {
            int success = 0;
            int fail = 0;
            string failIDs = null;
            string msg = null;
            string userid = UtilBussinessService.detoken(Token);
            JObject data = null;
            EMS_Equipment model = null;
            for (int i = 0; i < jArray.Count; i++)
            {
                data = (JObject)jArray[i];
                if (!EMS_EquipmentService.CheckCode(data.Value<string>("Code"), null))
                {
                    model = new EMS_Equipment();
                    model.EquipmentID = UniversalService.GetSerialNumber("EMS_Equipment");
                    model.Code = data.Value<string>("Code");
                    model.Name = data.Value<string>("Name");
                    model.ResourceCategory = data.Value<string>("ResourceCategory");
                    model.PlantAreaID = data.Value<string>("PlantAreaID");
                    model.PlantID = data.Value<string>("PlantID");
                    model.FixedAssets = data.Value<string>("FixedAssets");
                    if (!String.IsNullOrWhiteSpace(data.Value<string>("PurchaseDate")))
                        model.PurchaseDate = data.Value<DateTime>("PurchaseDate");
                    model.ManufacturerID = data.Value<string>("ManufacturerID");
                    model.Model = data.Value<string>("Model");
                    model.MachineNo = data.Value<string>("MachineNo");
                    model.ClassOne = data.Value<string>("ClassOne");
                    model.ClassTwo = data.Value<string>("ClassTwo");
                    model.OrganizationID = data.Value<string>("OrganizationID");
                    model.Status = data.Value<string>("Status");
                    model.Condition = data.Value<string>("Condition");
                    if (!String.IsNullOrWhiteSpace(data.Value<string>("ExpireDate")))
                        model.ExpireDate = data.Value<DateTime>("ExpireDate");
                    if (!String.IsNullOrWhiteSpace(data.Value<string>("StdCapacity")))
                        model.StdCapacity = data.Value<decimal>("StdCapacity");
                    model.MaintenanceTime = data.Value<decimal>("MaintenanceTime");
                    model.MaintenanceNum = data.Value<decimal>("MaintenanceNum");
                    model.UsableTime = data.Value<decimal>("UsableTime");
                    model.CavityMold = data.Value<decimal>("CavityMold");
                    model.UsableTimes = data.Value<decimal>("UsableTimes");
                    model.StatisticsFlag = data.Value<bool>("StatisticsFlag");
                    model.DescriptionOne = data.Value<string>("DescriptionOne");
                    model.DescriptionTwo = data.Value<string>("DescriptionOne");
                    if (!String.IsNullOrWhiteSpace(data.Value<string>("DateOne")))
                        model.DateOne = data.Value<DateTime>("DateOne");
                    if (!String.IsNullOrWhiteSpace(data.Value<string>("DateTwo")))
                        model.DateTwo = data.Value<DateTime>("DateTwo");
                    if (!String.IsNullOrWhiteSpace(data.Value<string>("NumOne")))
                        model.NumOne = data.Value<decimal>("NumOne");
                    if (!String.IsNullOrWhiteSpace(data.Value<string>("NumTwo")))
                        model.NumTwo = data.Value<decimal>("NumTwo");
                    model.AbnormalStatus = data.Value<bool>("AbnormalStatus");
                    model.Comments = data.Value<string>("Comments");
                    model.TotalOutput = 0;
                    model.UsableTime = 0;
                    model.UsableTimes = 0;
                    model.AttachmentID = UniversalService.GetSerialNumber("SYS_Attachments");
                    if (EMS_EquipmentService.insert(userid, model))
                        success++;
                    else
                    {
                        failIDs = UtilBussinessService.str(failIDs, data.Value<string>("EquipmentID"));
                        fail++;
                    }
                }
                else
                {
                    failIDs = UtilBussinessService.str(failIDs, data.Value<string>("EquipmentID"));
                    msg = UtilBussinessService.str(msg, data.Value<string>("Code") + "已存在");
                    fail++;
                }
            }
            return new { success = success, fail = fail, failIDs = failIDs, msg = msg };
        }

        /// <summary>
        /// 删除设备
        /// SAM 2017年5月22日14:58:36
        /// </summary>
        /// <param name="Token">授权码</param>
        /// <param name="jArray"></param>
        /// <returns></returns>
        public static object Ems00001delete(string Token, JArray jArray)
        {
            int success = 0;
            int fail = 0;
            string failIDs = null;
            string msg = null;
            string userid = UtilBussinessService.detoken(Token);
            JObject data = null;
            EMS_Equipment model = null;
            for (int i = 0; i < jArray.Count; i++)
            {
                data = (JObject)jArray[i];
                model = EMS_EquipmentService.get(data.Value<string>("EquipmentID"));
                if (SYS_ResourceDetailsService.CheckEquipment(data.Value<string>("EquipmentID")))
                {
                    failIDs = UtilBussinessService.str(failIDs, data.Value<string>("EquipmentID"));
                    msg = UtilBussinessService.str(msg, model.Code + "已使用，不能删除");
                    fail++;
                    continue;
                }

                if (EMS_EquipmentProjectService.CheckEquipment(data.Value<string>("EquipmentID")))
                {
                    failIDs = UtilBussinessService.str(failIDs, data.Value<string>("EquipmentID"));
                    msg = UtilBussinessService.str(msg, model.Code + "已使用，不能删除");
                    fail++;
                    continue;
                }

                model.Status = Framework.SystemID + "0201213000003";
                if (EMS_EquipmentService.update(userid, model))
                    success++;
                else
                {
                    failIDs = UtilBussinessService.str(failIDs, data.Value<string>("EquipmentID"));
                    fail++;
                }
            }
            return new { success = success, fail = fail, failIDs = failIDs, msg = msg };
        }

        /// <summary>
        /// 更新设备
        /// SAM 2017年5月22日14:58:51
        /// </summary>
        /// <param name="Token">授权码</param>
        /// <param name="jArray"></param>
        /// <returns></returns>
        public static object Ems00001update(string Token, JArray jArray)
        {
            int success = 0;
            int fail = 0;
            string failIDs = null;
            string msg = null;
            string userid = UtilBussinessService.detoken(Token);
            JObject data = null;
            EMS_Equipment model = null;
            for (int i = 0; i < jArray.Count; i++)
            {
                data = (JObject)jArray[i];
                model = EMS_EquipmentService.get(data.Value<string>("EquipmentID"));
                model.ResourceCategory = data.Value<string>("ResourceCategory");
                model.PlantAreaID = data.Value<string>("PlantAreaID");
                model.PlantID = data.Value<string>("PlantID");
                model.FixedAssets = data.Value<string>("FixedAssets");
                if (!string.IsNullOrWhiteSpace(data.Value<string>("PurchaseDate")))
                    model.PurchaseDate = data.Value<DateTime>("PurchaseDate");
                else
                    model.PurchaseDate = null;
                model.ManufacturerID = data.Value<string>("ManufacturerID");
                model.Model = data.Value<string>("Model");
                model.MachineNo = data.Value<string>("MachineNo");
                model.ClassOne = data.Value<string>("ClassOne");
                model.ClassTwo = data.Value<string>("ClassTwo");
                model.OrganizationID = data.Value<string>("OrganizationID");
                model.Status = data.Value<string>("Status");
                model.Condition = data.Value<string>("Condition");
                if (!string.IsNullOrWhiteSpace(data.Value<string>("ExpireDate")))
                    model.ExpireDate = data.Value<DateTime>("ExpireDate");
                else
                    model.ExpireDate = null;
                if (!string.IsNullOrWhiteSpace(data.Value<string>("StdCapacity")))
                    model.StdCapacity = data.Value<decimal>("StdCapacity");
                else
                    model.StdCapacity = null;
                model.MaintenanceTime = data.Value<decimal>("MaintenanceTime");
                model.MaintenanceNum = data.Value<decimal>("MaintenanceNum");
                model.UsableTime = data.Value<decimal>("UsableTime");
                model.CavityMold = data.Value<decimal>("CavityMold");
                model.UsableTimes = data.Value<decimal>("UsableTimes");
                model.StatisticsFlag = data.Value<bool>("StatisticsFlag");
                model.DescriptionOne = data.Value<string>("DescriptionOne");
                model.DescriptionTwo = data.Value<string>("DescriptionOne");
                if (!string.IsNullOrWhiteSpace(data.Value<string>("DateOne")))
                    model.DateOne = data.Value<DateTime>("DateOne");
                else
                    model.DateOne = null;
                if (!string.IsNullOrWhiteSpace(data.Value<string>("DateTwo")))
                    model.DateTwo = data.Value<DateTime>("DateTwo");
                else
                    model.DateTwo = null;
                if (!string.IsNullOrWhiteSpace(data.Value<string>("NumOne")))
                    model.NumOne = data.Value<decimal>("NumOne");
                else
                    model.NumOne = null;
                if (!string.IsNullOrWhiteSpace(data.Value<string>("NumTwo")))
                    model.NumTwo = data.Value<decimal>("NumTwo");
                else
                    model.NumTwo = null;
                model.AbnormalStatus = data.Value<bool>("AbnormalStatus");
                if (EMS_EquipmentService.update(userid, model))
                    success++;
                else
                {
                    failIDs = UtilBussinessService.str(failIDs, data.Value<string>("EquipmentID"));
                    fail++;
                }


            }
            return new { success = success, fail = fail, failIDs = failIDs, msg = msg };
        }

        /// <summary>
        /// 删除设备
        /// SAM 2017年8月14日23:19:30
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public static object Ems00001Delete(JObject request)
        {
            string Token = request.Value<string>("Token");
            string userid = UtilBussinessService.detoken(Token);
            string EquipmentID = request.Value<string>("EquipmentID");
            EMS_Equipment model = EMS_EquipmentService.get(EquipmentID);
            if (model == null)
                return new { status = "410", msg = "删除失败！不存在的设备信息" };

            if (SYS_ResourceDetailsService.CheckEquipment(EquipmentID))
                return new { status = "410", msg = model.Code + "已使用，不能删除" };

            if (EMS_EquipmentProjectService.CheckEquipment(EquipmentID))
                return new { status = "410", msg = model.Code + "已使用，不能删除" };

            model.Status = Framework.SystemID + "0201213000003";
            if (EMS_EquipmentService.update(userid, model))
                return new { status = "200", msg = "删除成功！" };
            else
                return new { status = "410", msg = "删除失败！" };
        }


        /// <summary>
        /// 获取正常的设备列表（用于设备管理另外两个页签）
        /// SAM 2017年5月22日22:33:11
        /// </summary>
        /// <param name="Token">授权码</param>
        /// <param name="Code">代号</param>
        /// <returns></returns>
        public static object Ems00001List(string token, string code)
        {
            return EMS_EquipmentService.Ems00001List(code);
        }

        /// <summary>
        /// 获取一个设备对应的设备项目列表
        /// SAM 2017年5月22日22:35:49
        /// </summary>
        /// <param name="Token">授权码</param>
        /// <param name="Code">代号</param>
        /// <param name="page">页码</param>
        /// <param name="rows">行数</param>
        /// <returns></returns>
        public static object Ems00001GetProjectList(string token, string EquipmentID, int page, int rows)
        {
            int count = 0;
            IList<Hashtable> result = EMS_EquipmentProjectService.Ems00001GetProjectList(EquipmentID, page, rows, ref count);
            return UtilBussinessService.getPaginationModel(result, count);
        }

        /// <summary>
        /// 获取一个设备对应的设备项目列表（正常的
        /// SAM 2017年5月23日16:07:17）
        /// </summary>
        /// <param name="Token">授权码</param>
        /// <param name="EquipmentID"></param>
        /// <param name="page">页码</param>
        /// <param name="rows">行数</param>
        /// <returns></returns>
        public static object EquipmentProjectList(string token, string EquipmentID, int page, int rows)
        {
            int count = 0;
            IList<Hashtable> result = EMS_EquipmentProjectService.EquipmentProjectList(EquipmentID, null, page, rows, ref count);
            return UtilBussinessService.getPaginationModel(result, count);
        }


        /// <summary>
        /// 添加设备项目
        /// SAM 2017年5月22日22:53:43
        /// </summary>
        /// <param name="Token">授权码</param>
        /// <param name="jArray"></param>
        /// <returns></returns>
        public static object Ems00001Projectinsert(string Token, JArray jArray)
        {
            int success = 0;
            int fail = 0;
            string failIDs = null;
            string msg = null;
            string userid = UtilBussinessService.detoken(Token);
            JObject data = null;
            EMS_EquipmentProject model = null;
            for (int i = 0; i < jArray.Count; i++)
            {
                data = (JObject)jArray[i];
                if (!EMS_EquipmentProjectService.Check(data.Value<string>("SensorID"), data.Value<string>("EquipmentID"), data.Value<string>("ProjectID"), null))
                {
                    model = new EMS_EquipmentProject();
                    model.EquipmentProjectID = UniversalService.GetSerialNumber("EMS_EquipmentProject");
                    model.EquipmentID = data.Value<string>("EquipmentID");
                    model.ProjectID = data.Value<string>("ProjectID");
                    model.IfCollection = data.Value<bool>("IfCollection");
                    model.CollectionWay = data.Value<string>("CollectionWay");
                    model.SensorID = data.Value<string>("SensorID");
                    model.StandardValue = data.Value<string>("StandardValue");
                    model.MaxValue = data.Value<string>("MaxValue");
                    model.MinValue = data.Value<string>("MinValue");
                    if (!string.IsNullOrWhiteSpace(data.Value<string>("MaxAlarmTime")))
                        model.MaxAlarmTime = data.Value<int>("MaxAlarmTime");
                    if (!string.IsNullOrWhiteSpace(data.Value<string>("MinAlarmTime")))
                        model.MinAlarmTime = data.Value<int>("MinAlarmTime");
                    model.Status = Framework.SystemID + "0201213000001";
                    model.MaxAlarmValue = data.Value<string>("MaxAlarmValue");
                    model.MinAlarmValue = data.Value<string>("MinAlarmValue");
                    model.Comments = data.Value<string>("Comments");
                    if (EMS_EquipmentProjectService.insert(userid, model))
                        success++;
                    else
                    {
                        failIDs = UtilBussinessService.str(failIDs, data.Value<string>("EquipmentProjectID"));
                        fail++;
                    }
                }
                else
                {
                    failIDs = UtilBussinessService.str(failIDs, data.Value<string>("EquipmentID"));
                    msg = UtilBussinessService.str(msg, "资料重复");
                    fail++;
                }
            }
            return new { success = success, fail = fail, failIDs = failIDs, msg = msg };
        }

        /// <summary>
        /// 删除设备项目
        /// SAM 2017年5月22日14:58:36
        /// </summary>
        /// <param name="Token">授权码</param>
        /// <param name="jArray"></param>
        /// <returns></returns>
        public static object Ems00001Projectdelete(string Token, JArray jArray)
        {
            int success = 0;
            int fail = 0;
            string failIDs = null;
            string msg = null;
            string userid = UtilBussinessService.detoken(Token);
            JObject data = null;
            EMS_EquipmentProject model = null;
            for (int i = 0; i < jArray.Count; i++)
            {
                data = (JObject)jArray[i];
                model = EMS_EquipmentProjectService.get(data.Value<string>("EquipmentProjectID"));

                model.Status = Framework.SystemID + "0201213000003";
                if (EMS_EquipmentProjectService.update(userid, model))
                    success++;
                else
                {
                    failIDs = UtilBussinessService.str(failIDs, data.Value<string>("EquipmentProjectID"));
                    fail++;
                }


            }
            return new { success = success, fail = fail, failIDs = failIDs, msg = msg };
        }

        /// <summary>
        /// 更新设备项目
        /// SAM 2017年5月22日14:58:51
        /// </summary>
        /// <param name="Token">授权码</param>
        /// <param name="jArray"></param>
        /// <returns></returns>
        public static object Ems00001Projectupdate(string Token, JArray jArray)
        {
            int success = 0;
            int fail = 0;
            string failIDs = null;
            string msg = null;
            string userid = UtilBussinessService.detoken(Token);
            JObject data = null;
            EMS_EquipmentProject model = null;
            for (int i = 0; i < jArray.Count; i++)
            {
                data = (JObject)jArray[i];

                model = EMS_EquipmentProjectService.get(data.Value<string>("EquipmentProjectID"));
                if (!EMS_EquipmentProjectService.Check(data.Value<string>("SensorID"), model.EquipmentID, model.ProjectID, model.EquipmentProjectID))
                {
                    model.IfCollection = data.Value<bool>("IfCollection");
                    model.CollectionWay = data.Value<string>("CollectionWay");
                    model.SensorID = data.Value<string>("SensorID");
                    model.StandardValue = data.Value<string>("StandardValue");
                    model.MaxValue = data.Value<string>("MaxValue");
                    model.MinValue = data.Value<string>("MinValue");
                    if (!string.IsNullOrWhiteSpace(data.Value<string>("MaxAlarmTime")))
                        model.MaxAlarmTime = data.Value<int>("MaxAlarmTime");
                    else
                        model.MaxAlarmTime = null;
                    if (!string.IsNullOrWhiteSpace(data.Value<string>("MaxAlarmTime")))
                        model.MinAlarmTime = data.Value<int>("MinAlarmTime");
                    else
                        model.MinAlarmTime = null;
                    model.MaxAlarmValue = data.Value<string>("MaxAlarmValue");
                    model.MinAlarmValue = data.Value<string>("MinAlarmValue");
                    if (EMS_EquipmentProjectService.update(userid, model))
                        success++;
                    else
                    {
                        failIDs = UtilBussinessService.str(failIDs, data.Value<string>("EquipmentProjectID"));
                        fail++;
                    }
                }
                else
                {
                    failIDs = UtilBussinessService.str(failIDs, data.Value<string>("EquipmentProjectID"));
                    msg = UtilBussinessService.str(failIDs, data.Value<string>("SensorCode") + "已存在");
                    fail++;
                }
            }
            return new { success = success, fail = fail, failIDs = failIDs, msg = msg };
        }

        /// <summary>
        /// 删除设备项目
        /// SAM 2017年8月14日23:22:23
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public static object Ems00001ProjectDelete(JObject request)
        {
            string Token = request.Value<string>("Token");
            string userid = UtilBussinessService.detoken(Token);
            string EquipmentProjectID = request.Value<string>("EquipmentProjectID");
            EMS_EquipmentProject model = EMS_EquipmentProjectService.get(EquipmentProjectID);
            if (model == null)
                return new { status = "410", msg = "删除失败！不存在的设备项目信息" };

            model.Status = Framework.SystemID + "0201213000003";
            if (EMS_EquipmentProjectService.update(userid, model))
                return new { status = "200", msg = "删除成功！" };
            else
                return new { status = "410", msg = "删除失败！" };
        }

        /// <summary>
        /// 获取一个设备对应的图样列表
        /// SAM 2017年5月23日09:43:21 
        /// </summary>
        /// <param name="Token">授权码</param>
        /// <param name="EquipmentID"></param>
        /// <param name="page">页码</param>
        /// <param name="rows">行数</param>
        /// <returns></returns>
        public static object Ems00001GetPatternList(string token, string EquipmentID, int page, int rows)
        {
            int count = 0;
            IList<Hashtable> result = SYS_AttachmentsService.Ems00001GetPatternList(EquipmentID, page, rows, ref count);
            return UtilBussinessService.getPaginationModel(result, count);
        }

        /// <summary>
        /// 删除设备图样
        /// SAM 2017年5月27日15:16:51
        /// </summary>
        /// <param name="Token">授权码</param>
        /// <param name="request">JSON数据</param>
        /// <returns></returns>
        public static object Ems00001PatternDelete(string Token, JObject request)
        {
            string userId = UtilBussinessService.detoken(Token);
            string AttachmentID = request.Value<string>("AttachmentID");

            if (String.IsNullOrWhiteSpace(AttachmentID))
                return new { status = "410", msg = "缺少参数!" };

            SYS_Attachments model = SYS_AttachmentsService.get(AttachmentID);
            if (model == null)
                return new { status = "410", msg = "参数错误!" };

            model.Status = Framework.SystemID + "0201213000003";

            if (SYS_AttachmentsService.update(userId, model))
            {
                try
                {
                    string strPath = HttpContext.Current.Server.MapPath("~/");
                    System.IO.File.Delete(strPath + model.Path);
                }
                catch (Exception ex)
                {
                    DataLogerService.writeerrlog(ex);
                }
                return new { status = "200", msg = "删除成功!" };
            }
            else
                return new { status = "410", msg = "删除失败!" };
        }

        /// <summary>
        /// 获取机况设定
        /// SAM 2017年7月31日17:02:51
        /// </summary>
        /// <param name="token"></param>
        /// <param name="Code"></param>
        /// <param name="Name"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <returns></returns>
        public static object Ems00001GetConditionList(string token, string Code, string Name, int page = 1, int rows = 10)
        {
            int count = 0;
            return UniversalService.getPaginationModel(SYS_ParameterService.Ems00001GetConditionList(Code, Name, page, rows, ref count), count);
        }

        /// <summary>
        /// 机况-保存
        /// SAM 2017年7月31日17:05:33
        /// </summary>
        /// <param name="Token"></param>
        /// <param name="jArray"></param>
        /// <returns></returns>
        public static object Ems00001ConditionUpdate(string Token, JArray jArray)
        {
            int success = 0;
            int fail = 0;
            string failIDs = null;
            string msg = null;
            string userid = UtilBussinessService.detoken(Token);
            JObject data = null;
            SYS_Parameters Model = null;
            for (int i = 0; i < jArray.Count; i++)
            {
                data = (JObject)jArray[i];
                Model = SYS_ParameterService.get(data.Value<string>("ParameterID"));
                Model.Description = data.Value<string>("Description");
                Model.Comments = data.Value<string>("Comments");
                if (SYS_ParameterService.update(userid, Model))
                    success++;
                else
                {
                    failIDs = UtilBussinessService.str(failIDs, data.Value<string>("ParameterID"));
                    fail++;
                }
            }
            return new { success = success, fail = fail, failIDs = failIDs, msg = msg };
        }


        #endregion

        #region EMS00002设备巡检项目

        /// <summary>
        /// 获取正常的类别为M的设备列表（Ems00002获取列表）
        /// MOUSE 2017年8月1日16:11:52
        /// </summary>
        /// <param name="Token">授权码</param>
        /// <param name="Code">代号</param>
        /// <returns></returns>
        public static IList<Hashtable> Ems00002List(string token, string code)
        {
            IList<Hashtable> result = EMS_EquipmentService.Ems00002List(code);
            foreach (Hashtable item in result)
            {
                //根据语序及参数流水号获取语序设定，不存在不替换，存在则替换
                string Code = SYS_LanguageLibService.GetLan(item["EquipmentID"].ToString(), "Code", 45);
                if (!string.IsNullOrWhiteSpace(Code))
                {
                    item["Code"] = Code;
                }
                string Name = SYS_LanguageLibService.GetLan(item["EquipmentID"].ToString(), "Name", 45);
                if (!string.IsNullOrWhiteSpace(Code))
                {
                    item["Name"] = Name;
                }
                string Comments = SYS_LanguageLibService.GetLan(item["EquipmentID"].ToString(), "Comments", 45);
                if (!string.IsNullOrWhiteSpace(Comments))
                {
                    item["Comments"] = Comments;
                }
            }
            return result;
        }

        /// <summary>
        /// 获取一个设备对应的巡检项目列表
        /// SAM 2017年5月23日15:38:40
        /// </summary>
        /// <param name="Token">授权码</param>
        /// <param name="EquipmentID"></param>
        /// <param name="page">页码</param>
        /// <param name="rows">行数</param>
        /// <returns></returns>
        public static object Ems00002GetList(string token, string EquipmentID, int page, int rows)
        {
            int count = 0;
            IList<Hashtable> result = EMS_EquipmentInspectionProjectService.Ems00002GetList(EquipmentID, page, rows, ref count);
            foreach (Hashtable item in result)
            {
                //根据语序及参数流水号获取语序设定，不存在不替换，存在则替换
                string Description = SYS_LanguageLibService.GetLan(item["ProjectID"].ToString(), "Name", 36);
                if (!string.IsNullOrWhiteSpace(Description))
                {
                    item["ProjectDescription"] = Description;
                }
            }
            return UtilBussinessService.getPaginationModel(result, count);
        }

        /// <summary>
        /// 添加设备巡检项目
        /// SAM 2017年5月23日16:00:21
        /// </summary>
        /// <param name="Token">授权码</param>
        /// <param name="jArray"></param>
        /// <returns></returns>
        public static object Ems00002insert(string Token, JArray jArray)
        {
            int success = 0;
            int fail = 0;
            string failIDs = null;
            string msg = null;
            string userid = UtilBussinessService.detoken(Token);
            JObject data = null;
            EMS_EquipmentInspectionProject model = null;
            EMS_EquipmentProject EPmodel = null;
            for (int i = 0; i < jArray.Count; i++)
            {
                data = (JObject)jArray[i];
                if (!EMS_EquipmentInspectionProjectService.Check(data.Value<string>("EquipmentProjectID"), data.Value<string>("EquipmentID"), null))
                {
                    model = new EMS_EquipmentInspectionProject();
                    model.EIProjectID = UniversalService.GetSerialNumber("EMS_EquipmentInspectionProject");
                    model.EquipmentID = data.Value<string>("EquipmentID");
                    model.EquipmentProjectID = data.Value<string>("EquipmentProjectID");
                    EPmodel = EMS_EquipmentProjectService.get(data.Value<string>("EquipmentProjectID"));
                    model.ProjectID = EPmodel.ProjectID;
                    model.Sequence = data.Value<int>("Sequence");
                    model.Status = data.Value<string>("Status");
                    model.Comments = data.Value<string>("Comments");
                    if (EMS_EquipmentInspectionProjectService.insert(userid, model))
                        success++;
                    else
                    {
                        failIDs = UtilBussinessService.str(failIDs, data.Value<string>("EIProjectID"));
                        fail++;
                    }
                }
                else
                {
                    failIDs = UtilBussinessService.str(failIDs, data.Value<string>("EIProjectID"));
                    msg = UtilBussinessService.str(msg, "资料重复");
                    fail++;
                }
            }
            return new { success = success, fail = fail, failIDs = failIDs, msg = msg };
        }

        /// <summary>
        /// 设备巡检项目删除
        /// SAM 2017年6月1日10:07:49
        /// </summary>
        /// <param name="request">JSON数据</param>
        /// <returns></returns>
        public static object Ems00002Delete(JObject request)
        {
            string Token = request.Value<string>("Token");
            string userid = UtilBussinessService.detoken(Token);
            string EIProjectID = request.Value<string>("EIProjectID");
            EMS_EquipmentInspectionProject model = EMS_EquipmentInspectionProjectService.get(EIProjectID);
            if (model == null)
                return new { status = "410", msg = "流水号为空！" };

            model.Status = Framework.SystemID + "0201213000003";
            if (EMS_EquipmentInspectionProjectService.update(userid, model))
                return new { status = "200", msg = "删除成功！" };
            else
                return new { status = "410", msg = "删除失败！" };
        }


        /// <summary>
        /// 删除设备巡检项目
        /// SAM 2017年5月23日16:00:21
        /// </summary>
        /// <param name="Token">授权码</param>
        /// <param name="jArray"></param>
        /// <returns></returns>
        public static object Ems00002delete(string Token, JArray jArray)
        {
            int success = 0;
            int fail = 0;
            string failIDs = null;
            string msg = null;
            string userid = UtilBussinessService.detoken(Token);
            JObject data = null;
            EMS_EquipmentInspectionProject model = null;
            for (int i = 0; i < jArray.Count; i++)
            {
                data = (JObject)jArray[i];
                model = EMS_EquipmentInspectionProjectService.get(data.Value<string>("EIProjectID"));

                model.Status = Framework.SystemID + "0201213000003";
                if (EMS_EquipmentInspectionProjectService.update(userid, model))
                    success++;
                else
                {
                    failIDs = UtilBussinessService.str(failIDs, data.Value<string>("EIProjectID"));
                    fail++;
                }


            }
            return new { success = success, fail = fail, failIDs = failIDs, msg = msg };
        }

        /// <summary>
        /// 更新设备项目
        /// SAM 2017年5月23日16:02:22
        /// </summary>
        /// <param name="Token">授权码</param>
        /// <param name="jArray"></param>
        /// <returns></returns>
        public static object Ems00002update(string Token, JArray jArray)
        {
            int success = 0;
            int fail = 0;
            string failIDs = null;
            string msg = null;
            string userid = UtilBussinessService.detoken(Token);
            JObject data = null;
            EMS_EquipmentInspectionProject model = null;
            EMS_EquipmentProject EPmodel = null;
            for (int i = 0; i < jArray.Count; i++)
            {
                data = (JObject)jArray[i];

                model = EMS_EquipmentInspectionProjectService.get(data.Value<string>("EIProjectID"));
                if (!EMS_EquipmentInspectionProjectService.Check(data.Value<string>("EquipmentProjectID"), model.EquipmentID, data.Value<string>("EIProjectID")))
                {
                    model.EquipmentProjectID = data.Value<string>("EquipmentProjectID");
                    EPmodel = EMS_EquipmentProjectService.get(data.Value<string>("EquipmentProjectID"));
                    model.ProjectID = EPmodel.ProjectID;
                    model.Sequence = data.Value<int>("Sequence");
                    model.Status = data.Value<string>("Status");
                    if (EMS_EquipmentInspectionProjectService.update(userid, model))
                        success++;
                    else
                    {
                        failIDs = UtilBussinessService.str(failIDs, data.Value<string>("EIProjectID"));
                        fail++;
                    }
                }
                else
                {
                    failIDs = UtilBussinessService.str(failIDs, data.Value<string>("EIProjectID"));
                    msg = UtilBussinessService.str(failIDs, "资料重复");
                    fail++;
                }
            }
            return new { success = success, fail = fail, failIDs = failIDs, msg = msg };
        }

        /// <summary>
        /// 根据设备流水号获取巡检项目列表
        /// MOUSE 2017年7月31日15:54:15
        /// </summary>
        /// <param name="token"></param>
        /// <param name="EquipmentID"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <returns></returns>
        public static object Ems00002GetProjectList(string token, string EquipmentID, int page, int rows)
        {
            int count = 0;
            return UtilBussinessService.getPaginationModel(EMS_EquipmentInspectionProjectService.Ems00002GetProjectList(EquipmentID, page, rows, ref count), count);
        }

        /// <summary>
        /// 根据设备流水号获取巡检项目（不分页）
        /// MOUSE 2017年7月31日15:30:24
        /// </summary>
        /// <param name="token"></param>
        /// <param name="EquipmentID"></param>
        /// <returns></returns>
        public static IList<Hashtable> Ems00002ProjectList(string token, string EquipmentID)
        {
            IList<Hashtable> result = EMS_EquipmentInspectionProjectService.Ems00002ProjectList(EquipmentID);
            foreach (Hashtable item in result)
            {
                //根据语序及参数流水号获取语序设定，不存在不替换，存在则替换
                string Description = SYS_LanguageLibService.GetLan(item["ProjectID"].ToString(), "Name", 36);
                if (!string.IsNullOrWhiteSpace(Description))
                {
                    item["ProjectDescription"] = Description;
                }
            }
            return result;
        }

        /// <summary>
        /// 根据设备流水号获取不属于他的巡检项目（不分页）
        /// MOUSE 2017年7月31日15:30:24
        /// </summary>
        /// <param name="token"></param>
        /// <param name="EquipmentID"></param>
        /// <returns></returns>
        public static object Ems00002NoProjectList(string token, string EquipmentID)
        {
            return EMS_EquipmentInspectionProjectService.Ems00002NoProjectList(EquipmentID);
        }

        /// <summary>
        /// 保存设备的巡检项目
        /// SAM 2017年7月25日11:33:42
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public static object Ems00002ProjectSave(JObject request)
        {
            string Token = request.Value<string>("Token");
            string userid = UtilBussinessService.detoken(Token);
            string EquipmentID = request.Value<string>("EquipmentID");
            JArray List = request.Value<JArray>("data");

            if (string.IsNullOrWhiteSpace(EquipmentID))
                return new { status = "410", msg = "EquipmentID为空!" };
            JObject data = null;
            Hashtable AddModel = null;
            EMS_EquipmentInspectionProject model = null;
            List<Hashtable> Add = new List<Hashtable>();
            EMS_EquipmentInspectionProjectService.Delete(userid, EquipmentID);
            for (int i = 0; i < List.Count; i++) //将新增的和已存在的分开
            {
                data = (JObject)List[i];
                AddModel = new Hashtable();


                AddModel["EquipmentProjectID"] = data.Value<string>("EquipmentProjectID");
                AddModel["EquipmentID"] = EquipmentID;

                if (string.IsNullOrWhiteSpace(data.Value<string>("Sequence")))//增加判断data里的排序是否为空为空不可进行；
                    return new { status = "410", msg = "请输入排序!" };

                AddModel["Sequence"] = data.Value<string>("Sequence");
                Add.Add(AddModel);
            }

            EMS_EquipmentProject ProModel = null;
            foreach (Hashtable item in Add) //循环添加新增的
            {
                model = new EMS_EquipmentInspectionProject();
                model.EIProjectID = UniversalService.GetSerialNumber("EMS_EquipmentInspectionProject");
                model.EquipmentID = EquipmentID;
                model.EquipmentProjectID = item["EquipmentProjectID"].ToString();
                ProModel = EMS_EquipmentProjectService.get(model.EquipmentProjectID);
                if (ProModel != null)
                    model.ProjectID = ProModel.ProjectID;
                model.Status = Framework.SystemID + "0201213000001";

                if (string.IsNullOrWhiteSpace(item["Sequence"].ToString()))//增加判断data里的排序是否为空为空不可进行；
                    return new { status = "410", msg = "请输入排序!" };

                model.Sequence = int.Parse(item["Sequence"].ToString());
                if (!EMS_EquipmentInspectionProjectService.CheckProject(model.EquipmentProjectID, model.EquipmentID))
                    EMS_EquipmentInspectionProjectService.insert(userid, model);
            }

            return new { status = "200", msg = "保存成功！" };
        }

        #endregion

        #region EMS00003设备巡检维护
        /// <summary>
        /// 设备巡检维护表头列表
        /// SAM 2017年6月8日11:13:34
        /// </summary>
        /// <param name="token"></param>
        /// <param name="code"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        public static object EMS00003GetList(string token, string code, string startDate, string endDate)
        {
            IList<Hashtable> result = EMS_EquipmentInspectionRecordService.EMS00003GetList(code, startDate, endDate);
            return result;
        }

        ///// <summary>
        ///// 巡检单号获取
        ///// SAM 2017年7月10日14:37:15
        ///// </summary>
        ///// <param name="token"></param>
        ///// <returns></returns>
        //public static string Ems00003GetAutoNumber(string token)
        //{
        //    string userid = UtilBussinessService.detoken(token);
        //    return UtilBussinessService.GetAutoNumber(userid, "EMSC");
        //}

        /// <summary>
        /// 巡检单号获取（因为巡检单并无字轨挑选的设定，所以默认拿第一个单据类别取号）
        /// SAM 2017年8月3日23:40:42
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public static object Ems00003GetAutoNumber(string token)
        {
            string userid = UtilBussinessService.detoken(token);
            string AutoNumber = null;
            string DocumentAutoNumberID = null;
            string Prevchar = null;

            SYS_DocumentTypeSetting number = SYS_DocumentTypeSettingService.getByType(Framework.SystemID + "020121300003A");
            if (number == null)
                return new { AutoNumber = AutoNumber, DocumentAutoNumberID = DocumentAutoNumberID };

            DateTime Now = DateTime.Now;
            SYS_Parameters ParModel = SYS_ParameterService.get(number.GiveWay);
            if (ParModel.Code == "M")
                Prevchar = number.Code + Now.ToString("yyMM");
            else if (ParModel.Code == "Y")
                Prevchar = number.Code + Now.ToString("yy");
            else if (ParModel.Code == "D")
                Prevchar = number.Code + Now.ToString("yyMMdd");

            SYS_DocumentAutoNumber model = SYS_DocumentAutoNumberService.getByAutoNumber(number.DTSID, Prevchar);
            if (model == null)
            {
                model = new SYS_DocumentAutoNumber();
                model.DocumentAutoNumberID = UniversalService.GetSerialNumber("SYS_DocumentAutoNumber");
                model.ClassID = number.DTSID;
                model.Num = 0;
                model.DefaultCharacter = Prevchar;
                model.Attribute = number.Attribute;
                model.Status = Framework.SystemID + "0201213000001";
                DateTime now = DateTime.Now;
                SYS_DocumentAutoNumberService.insert(userid, model);
            }
            DocumentAutoNumberID = model.DocumentAutoNumberID;
            AutoNumber = model.DefaultCharacter + (model.Num + 1).ToString().PadLeft(number.CodeLength, '0');

            return new { AutoNumber = AutoNumber, DocumentAutoNumberID = DocumentAutoNumberID };
        }

        /// <summary>
        /// 设备巡检维护新增
        /// SAM 2017年6月8日15:03:32
        /// </summary>
        /// <param name="Token"></param>
        /// <param name="jArray"></param>
        /// <returns></returns>
        public static object Ems00003insert(string Token, JArray jArray)
        {
            int success = 0;
            int fail = 0;
            string failIDs = null;
            string msg = null;
            string userid = UtilBussinessService.detoken(Token);
            JObject data = null;
            EMS_EquipmentInspectionRecord model = null;
            EMS_EquipmentInspectionRecordDetails DetailModel = null;
            for (int i = 0; i < jArray.Count; i++)
            {
                data = (JObject)jArray[i];

                model = new EMS_EquipmentInspectionRecord();
                model.EquipmentInspectionRecordID = UniversalService.GetSerialNumber("EMS_EquipmentInspectionRecord");
                model.Code = data.Value<string>("Code");
                model.Date = data.Value<DateTime>("Date");
                model.EquipmentID = data.Value<string>("EquipmentID");
                model.ClassID = data.Value<string>("ClassID");
                model.MESUserID = data.Value<string>("MESUserID");
                model.TaskID = data.Value<string>("TaskID");
                model.ItemID = data.Value<string>("ItemID");
                model.Status = Framework.SystemID + "0201213000001";
                model.Comments = data.Value<string>("Comments");
                if (EMS_EquipmentInspectionRecordService.insert(userid, model))
                {
                    IList<Hashtable> EIPlist = EMS_EquipmentInspectionProjectService.GetList(model.EquipmentID);
                    int Sequence = 1;
                    foreach (Hashtable item in EIPlist)
                    {
                        DetailModel = new EMS_EquipmentInspectionRecordDetails();
                        DetailModel.EIRDID = UniversalService.GetSerialNumber("EMS_EquipmentInspectionRecordDetails");
                        DetailModel.EquipmentInspectionRecordID = model.EquipmentInspectionRecordID;
                        DetailModel.Sequence = Sequence;
                        DetailModel.EquipmentProjectID = item["EquipmentProjectID"].ToString();
                        DetailModel.ProjectID = item["ProjectID"].ToString();
                        DetailModel.IsHand = false;
                        DetailModel.Status = Framework.SystemID + "0201213000001";
                        EMS_EquipmentInspectionRecordDetailsService.insert(userid, DetailModel);
                        Sequence++;
                    }
                    UtilBussinessService.UpdateDocumentAutoNumber(userid, data.Value<string>("DocumentAutoNumberID"));
                    success++;
                }
                else
                {
                    failIDs = UtilBussinessService.str(failIDs, data.Value<string>("EquipmentInspectionRecordID"));
                    fail++;
                }
            }
            return new { success = success, fail = fail, failIDs = failIDs, msg = msg };
        }

        /// <summary>
        /// 新增设备巡检维护
        /// SAM 2017年6月8日17:14:44
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static object Ems00003Add(JObject data)
        {
            string Token = data.Value<string>("Token");
            string userid = UtilBussinessService.detoken(Token);

            EMS_Equipment Eqmodel = null;
            Eqmodel = EMS_EquipmentService.get(data.Value<string>("EquipmentID"));
            if (Eqmodel == null)
                return new { status = "410", msg = "不存在的设备代号！" };

            EMS_EquipmentInspectionRecord model = new EMS_EquipmentInspectionRecord();
            model.EquipmentInspectionRecordID = UniversalService.GetSerialNumber("EMS_EquipmentInspectionRecord");
            model.Code = data.Value<string>("Code");
            model.Date = data.Value<DateTime>("Date");
            model.EquipmentID = data.Value<string>("EquipmentID");
            model.ClassID = data.Value<string>("ClassID");
            model.MESUserID = data.Value<string>("MESUserID");
            model.TaskID = data.Value<string>("TaskID");
            model.ItemID = data.Value<string>("ItemID");
            model.Status = Framework.SystemID + "0201213000001";
            model.Comments = data.Value<string>("Comments");

            if (EMS_EquipmentInspectionRecordService.insert(userid, model))
            {
                IList<Hashtable> EIPlist = EMS_EquipmentInspectionProjectService.GetList(model.EquipmentID);
                int Sequence = 1;
                EMS_EquipmentInspectionRecordDetails DetailModel = null;
                foreach (Hashtable item in EIPlist)
                {
                    DetailModel = new EMS_EquipmentInspectionRecordDetails();
                    DetailModel.EIRDID = UniversalService.GetSerialNumber("EMS_EquipmentInspectionRecordDetails");
                    DetailModel.EquipmentInspectionRecordID = model.EquipmentInspectionRecordID;
                    DetailModel.EIProjectID = item["EIProjectID"].ToString();
                    DetailModel.Sequence = Sequence;
                    DetailModel.EquipmentProjectID = item["EquipmentProjectID"].ToString();
                    DetailModel.ProjectID = item["ProjectID"].ToString();
                    DetailModel.IsHand = false;
                    DetailModel.Status = Framework.SystemID + "0201213000001";
                    EMS_EquipmentInspectionRecordDetailsService.insert(userid, DetailModel);
                    Sequence++;
                }
                //UtilBussinessService.updateAutoNumber(userid, "EMSC", model.Code);
                UtilBussinessService.UpdateDocumentAutoNumber(userid, data.Value<string>("DocumentAutoNumberID"));
                return new { status = "200", msg = "新增成功！" };
            }
            else
                return new { status = "410", msg = "新增失败！" };
        }

        /// <summary>
        /// 设备巡检维护删除
        /// SAM 2017年6月8日15:04:02
        /// </summary>
        /// <param name="Token"></param>
        /// <param name="jArray"></param>
        /// <returns></returns>
        public static object Ems00003delete(string Token, JArray jArray)
        {
            int success = 0;
            int fail = 0;
            string failIDs = null;
            string msg = null;
            string userid = UtilBussinessService.detoken(Token);
            JObject data = null;
            EMS_EquipmentInspectionRecord model = null;
            for (int i = 0; i < jArray.Count; i++)
            {
                data = (JObject)jArray[i];
                model = EMS_EquipmentInspectionRecordService.get(data.Value<string>("EquipmentInspectionRecordID"));
                model.Status = Framework.SystemID + "0201213000003";
                if (EMS_EquipmentInspectionRecordService.update(userid, model))
                {
                    EMS_EquipmentInspectionRecordDetailsService.delete(userid, model.EquipmentInspectionRecordID);
                    success++;
                }
                else
                {
                    failIDs = UtilBussinessService.str(failIDs, data.Value<string>("EquipmentInspectionRecordID"));
                    fail++;
                }


            }
            return new { success = success, fail = fail, failIDs = failIDs, msg = msg };
        }


        /// <summary>
        /// 设备巡检维护更新
        /// SAM 2017年6月8日15:04:31
        /// </summary>
        /// <param name="Token"></param>
        /// <param name="jArray"></param>
        /// <returns></returns>
        public static object Ems00003update(string Token, JArray jArray)
        {
            int success = 0;
            int fail = 0;
            string failIDs = null;
            string msg = null;
            string userid = UtilBussinessService.detoken(Token);
            JObject data = null;
            EMS_EquipmentInspectionRecord model = null;
            for (int i = 0; i < jArray.Count; i++)
            {
                data = (JObject)jArray[i];

                model = EMS_EquipmentInspectionRecordService.get(data.Value<string>("EquipmentInspectionRecordID"));
                model.Date = data.Value<DateTime>("Date");
                model.EquipmentID = data.Value<string>("EquipmentID");
                model.ClassID = data.Value<string>("ClassID");
                model.MESUserID = data.Value<string>("MESUserID");
                model.TaskID = data.Value<string>("TaskID");
                model.ItemID = data.Value<string>("ItemID");
                model.Comments = data.Value<string>("Comments");
                if (EMS_EquipmentInspectionRecordService.update(userid, model))
                    success++;
                else
                {
                    failIDs = UtilBussinessService.str(failIDs, data.Value<string>("EquipmentInspectionRecordID"));
                    fail++;
                }

            }
            return new { success = success, fail = fail, failIDs = failIDs, msg = msg };
        }

        /// <summary>
        /// 根据设备巡检维护的表头获取明细
        /// SAM 2017年6月8日15:57:48
        /// </summary>
        /// <param name="token"></param>
        /// <param name="EquipmentInspectionRecordID"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <returns></returns>
        public static object EMS00003GetDetailList(string token, string EquipmentInspectionRecordID, int page, int rows)
        {
            IList<Hashtable> result = EMS_EquipmentInspectionRecordDetailsService.EMS00003GetDetailList(EquipmentInspectionRecordID);
            return result;
        }

        /// <summary>
        /// 根据设备巡检维护的表头获取明细列表
        /// SAM 2017年10月24日17:59:00
        /// 现值要做是否存在与区间内的判定
        /// </summary>
        /// <param name="token"></param>
        /// <param name="EquipmentInspectionRecordID"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <returns></returns>
        public static object EMS00003GetDetailListV1(string token, string EquipmentInspectionRecordID, int page, int rows)
        {
            IList<Hashtable> result = EMS_EquipmentInspectionRecordDetailsService.EMS00003GetDetailList(EquipmentInspectionRecordID);
            decimal Value = 0;
            decimal MaxValue = 0;
            decimal MinValue = 0;
            bool IsRed = false;
            for (int i = 0; i < result.Count; i++)
            {
                try
                {
                    IsRed = false;
                    if (!string.IsNullOrWhiteSpace(result[i]["Value"].ToString()))
                    {
                        Value = decimal.Parse(result[i]["Value"].ToString());
                        if (!string.IsNullOrWhiteSpace(result[i]["MaxValue"].ToString()))
                        {
                            try
                            {
                                MaxValue = decimal.Parse(result[i]["MaxValue"].ToString());
                                if (Value > MaxValue)
                                    IsRed = true;
                            }
                            catch
                            {

                            }
                        }
                        if (!string.IsNullOrWhiteSpace(result[i]["MinValue"].ToString()))
                        {
                            try
                            {
                                MinValue = decimal.Parse(result[i]["MinValue"].ToString());
                                if (Value < MinValue)
                                    IsRed = true;
                            }
                            catch
                            {

                            }
                        }
                    }
                }
                catch
                {

                }
                result[i]["IsRed"] = IsRed;
            }
            return result;
        }

        /// <summary>
        /// 设备巡检维护明细新增
        /// SAM 2017年6月8日16:19:51
        /// </summary>
        /// <param name="Token"></param>
        /// <param name="jArray"></param>
        /// <returns></returns>
        public static object Ems00003Detailinsert(string Token, JArray jArray)
        {
            int success = 0;
            int fail = 0;
            string failIDs = null;
            string msg = null;
            string userid = UtilBussinessService.detoken(Token);
            JObject data = null;
            EMS_EquipmentInspectionRecordDetails model = null;
            for (int i = 0; i < jArray.Count; i++)
            {
                data = (JObject)jArray[i];

                model = new EMS_EquipmentInspectionRecordDetails();
                model.EIRDID = UniversalService.GetSerialNumber("EMS_EquipmentInspectionRecordDetails");
                model.EquipmentInspectionRecordID = data.Value<string>("EquipmentInspectionRecordID");
                model.EquipmentProjectID = data.Value<string>("EquipmentProjectID");
                model.ProjectID = data.Value<string>("ProjectID");
                model.Sequence = string.IsNullOrWhiteSpace(data.Value<string>("Sequence")) ? 0 : data.Value<int>("Sequence");
                model.Value = data.Value<string>("Value");
                model.Status = Framework.SystemID + "0201213000001";
                model.Comments = data.Value<string>("Comments");
                if (EMS_EquipmentInspectionRecordDetailsService.insert(userid, model))
                    success++;
                else
                {
                    failIDs = UtilBussinessService.str(failIDs, data.Value<string>("EIRDID"));
                    fail++;
                }
            }
            return new { success = success, fail = fail, failIDs = failIDs, msg = msg };
        }

        /// <summary>
        /// 设备巡检维护明细删除
        /// SAM 2017年6月8日16:20:13
        /// </summary>
        /// <param name="Token"></param>
        /// <param name="jArray"></param>
        /// <returns></returns>
        public static object Ems00003Detaildelete(string Token, JArray jArray)
        {
            int success = 0;
            int fail = 0;
            string failIDs = null;
            string msg = null;
            string userid = UtilBussinessService.detoken(Token);
            JObject data = null;
            EMS_EquipmentInspectionRecordDetails model = null;
            for (int i = 0; i < jArray.Count; i++)
            {
                data = (JObject)jArray[i];
                model = EMS_EquipmentInspectionRecordDetailsService.get(data.Value<string>("EIRDID"));
                model.Status = Framework.SystemID + "0201213000003";
                if (EMS_EquipmentInspectionRecordDetailsService.update(userid, model))
                    success++;
                else
                {
                    failIDs = UtilBussinessService.str(failIDs, data.Value<string>("EIRDID"));
                    fail++;
                }


            }
            return new { success = success, fail = fail, failIDs = failIDs, msg = msg };
        }

        /// <summary>
        /// 设备巡检维护表头删除
        /// Tom 2017年7月28日15:04:47
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public static object Ems0003Delete(JObject request)
        {
            string token = request.Value<string>("Token");
            string userid = UtilBussinessService.detoken(token);
            EMS_EquipmentInspectionRecord model = EMS_EquipmentInspectionRecordService.get(request.Value<string>("EquipmentInspectionRecordID"));
            model.Status = Framework.SystemID + "0201213000003";
            if (EMS_EquipmentInspectionRecordService.update(userid, model))
            {
                EMS_EquipmentInspectionRecordDetailsService.delete(userid, model.EquipmentInspectionRecordID);
                return new { status = "200", msg = "删除成功" };
            }
            else
            {
                return new { status = "400", msg = "删除失败" };
            }
        }

        /// <summary>
        /// 设备巡检维护明细删除
        /// Tom 2017年7月28日15:04:47
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public static object Ems0003DetailDelete(JObject request)
        {
            string token = request.Value<string>("Token");
            string userid = UtilBussinessService.detoken(token);
            EMS_EquipmentInspectionRecordDetails model = EMS_EquipmentInspectionRecordDetailsService.get(request.Value<string>("EIRDID"));
            model.Status = Framework.SystemID + "0201213000003";
            if (EMS_EquipmentInspectionRecordDetailsService.update(userid, model))
            {
                return new { status = "200", msg = "删除成功" };
            }
            else
            {
                return new { status = "400", msg = "删除失败" };
            }
        }

        /// <summary>
        /// 设备巡检维护明细更新
        /// SAM 2017年6月8日16:20:53
        /// </summary>
        /// <param name="Token"></param>
        /// <param name="jArray"></param>
        /// <returns></returns>
        public static object Ems00003Detailupdate(string Token, JArray jArray)
        {
            int success = 0;
            int fail = 0;
            string failIDs = null;
            string msg = null;
            string userid = UtilBussinessService.detoken(Token);
            JObject data = null;
            EMS_EquipmentInspectionRecordDetails model = null;
            for (int i = 0; i < jArray.Count; i++)
            {
                data = (JObject)jArray[i];

                model = EMS_EquipmentInspectionRecordDetailsService.get(data.Value<string>("EIRDID"));
                model.Value = data.Value<string>("Value");
                model.Comments = data.Value<string>("Comments");
                if (EMS_EquipmentInspectionRecordDetailsService.update(userid, model))
                    success++;
                else
                {
                    failIDs = UtilBussinessService.str(failIDs, data.Value<string>("EIRDID"));
                    fail++;
                }

            }
            return new { success = success, fail = fail, failIDs = failIDs, msg = msg };
        }
        #endregion

        #region EMS00004设备叫修单维护
        /// <summary>
        /// 获取设备叫修单的列表
        /// SAM 2017年5月24日14:18:18
        /// </summary>
        /// <param name="Token">授权码</param>
        /// <param name="Code">代号</param>
        /// <param name="Status">状态</param>
        /// <param name="page">页码</param>
        /// <param name="rows">行数</param>
        /// <returns></returns>
        public static object Ems00004GetList(string token, string code, string status, int page, int rows)
        {
            int count = 0;
            IList<Hashtable> result = EMS_CalledRepairOrderService.Ems00004GetList(code, status, page, rows, ref count);
            return UtilBussinessService.getPaginationModel(result, count);
        }

        /// <summary>
        /// 新增设备叫修单
        /// SAM 2017年6月2日14:35:58
        /// </summary>
        /// <param name="request">JSON数据</param>
        /// <returns></returns>
        public static object Ems00004Add(JObject data)
        {
            string Token = data.Value<string>("Token");
            string userid = UtilBussinessService.detoken(Token);
            string SysID = Framework.SystemID;
            EMS_Equipment Eqmodel = null;
            Eqmodel = EMS_EquipmentService.get(data.Value<string>("EquipmentID"));
            if (Eqmodel == null)
                return new { status = "410", msg = "不存在的设备代号！" };

            if (Eqmodel.Condition == SysID + "0201213000021" || Eqmodel.Condition == SysID + "0201213000022" ||
                Eqmodel.Condition == SysID + "0201213000023" || Eqmodel.Condition == SysID + "0201213000024")
                return new { status = "410", msg = "此设备机况不在待机、停止或关机状态！" };

            //if(String.IsNullOrWhiteSpace(data.Value<string>("Code")))
            //    return new { status = "410", msg = "叫修单号不能为空！" };

            EMS_CalledRepairOrder model = new EMS_CalledRepairOrder();
            model.CalledRepairOrderID = UniversalService.GetSerialNumber("EMS_CalledRepairOrder");
            model.Code = data.Value<string>("Code");
            model.Date = data.Value<DateTime>("Date");
            model.EquipmentID = data.Value<string>("EquipmentID");
            model.Status = Framework.SystemID + "0201213000028";
            model.CallMESUserID = data.Value<string>("CallMESUserID");
            model.Comments = data.Value<string>("Comments");
            model.CallOrganizationID = data.Value<string>("CallOrganizationID");

            /*当保存时,如果叫修单号已存在，则自动获取下一叫修单号*/
            /*SAM 2017年9月13日16:46:44*/
            string AutoNumberID = data.Value<string>("DocumentAutoNumberID");
            while (EMS_CalledRepairOrderService.CheckCode(model.Code))
            {
                AutoNumberID = null;
                model.Code = UtilBussinessService.GetDocumentAutoNumber(userid, data.Value<string>("DocumentID"), model.Date.ToString(), ref AutoNumberID);
            }

            if (EMS_CalledRepairOrderService.insert(userid, model))
            {
                UtilBussinessService.UpdateDocumentAutoNumber(userid, AutoNumberID);
                Eqmodel.Condition = Framework.SystemID + "0201213000022";
                EMS_EquipmentService.update(userid, Eqmodel);
                return new { status = "200", msg = "新增成功！" };
            }
            else
                return new { status = "410", msg = "新增失败！" };
        }

        /// <summary>
        /// 判断制定设备是否已存在叫修或者维护
        /// SAM 2017年6月5日18:31:28
        /// </summary>
        /// <param name="Token">授权码</param>
        /// <param name="EquipmentID"></param>
        /// <returns></returns>
        public static bool CheckEquipment(string Token, string EquipmentID)
        {
            return EMS_CalledRepairOrderService.CheckEquipmentID(EquipmentID);
        }

        /// <summary>
        /// 添加设备叫修单
        /// SAM 2017年5月29日13:19:49
        /// </summary>
        /// <param name="Token">授权码</param>
        /// <param name="jArray"></param>
        /// <returns></returns>
        public static object Ems00004insert(string Token, JArray jArray)
        {
            int success = 0;
            int fail = 0;
            string failIDs = null;
            string msg = null;
            string userid = UtilBussinessService.detoken(Token);
            JObject data = null;
            EMS_CalledRepairOrder model = null;
            EMS_Equipment Eqmodel = null;
            for (int i = 0; i < jArray.Count; i++)
            {
                data = (JObject)jArray[i];
                model = new EMS_CalledRepairOrder();
                model.CalledRepairOrderID = UniversalService.GetSerialNumber("EMS_CalledRepairOrder");
                model.Code = data.Value<string>("Code");
                model.Date = data.Value<DateTime>("Date");
                model.EquipmentID = data.Value<string>("EquipmentID");
                model.Status = Framework.SystemID + "0201213000028";
                model.CallMESUserID = data.Value<string>("CallMESUserID");
                model.Comments = data.Value<string>("Comments");
                model.CallOrganizationID = data.Value<string>("CallOrganizationID");

                Eqmodel = EMS_EquipmentService.get(data.Value<string>("EquipmentID"));
                if (Eqmodel == null)
                {
                    failIDs = UtilBussinessService.str(failIDs, data.Value<string>("CalledRepairOrderID"));
                    msg = UtilBussinessService.str(msg, "不存在的设备代号");
                    fail++;
                    continue; //跳过循环
                }

                if (!EMS_EquipmentService.CheckCode(data.Value<string>("Code"), null))
                {
                    if (EMS_CalledRepairOrderService.insert(userid, model))
                    {
                        success++;
                        Eqmodel.Condition = Framework.SystemID + "0201213000022";
                        EMS_EquipmentService.update(userid, Eqmodel);
                        //UtilBussinessService.updateAutoNumber(userid, "EMSA", model.Code);
                    }
                    else
                    {
                        failIDs = UtilBussinessService.str(failIDs, data.Value<string>("CalledRepairOrderID"));
                        fail++;
                    }
                }
                else
                {
                    failIDs = UtilBussinessService.str(failIDs, data.Value<string>("CalledRepairOrderID"));
                    msg = UtilBussinessService.str(msg, data.Value<string>("Code") + "已存在");
                    fail++;
                }
            }
            return new { success = success, fail = fail, failIDs = failIDs, msg = msg };
        }

        /// <summary>
        /// 删除叫修单
        /// SAM 2017年5月29日14:20:05
        /// </summary>
        /// <param name="Token">授权码</param>
        /// <param name="jArray"></param>
        /// <returns></returns>
        public static object Ems00004delete(string Token, JArray jArray)
        {
            int success = 0;
            int fail = 0;
            string failIDs = null;
            string msg = null;
            string userid = UtilBussinessService.detoken(Token);
            JObject data = null;
            EMS_CalledRepairOrder model = null;
            for (int i = 0; i < jArray.Count; i++)
            {
                data = (JObject)jArray[i];
                model = EMS_CalledRepairOrderService.get(data.Value<string>("CalledRepairOrderID"));

                if (model.Status == Framework.SystemID + "0201213000028")
                {
                    model.Status = Framework.SystemID + "0201213000003";
                    if (EMS_CalledRepairOrderService.update(userid, model))
                    {
                        EMS_Equipment EquModel = EMS_EquipmentService.get(model.EquipmentID);
                        if (EquModel != null)
                        {
                            EquModel.Condition = Framework.SystemID + "02012130000A5";
                            EMS_EquipmentService.update(userid, EquModel);
                        }
                        success++;
                    }
                    else
                    {
                        failIDs = UtilBussinessService.str(failIDs, data.Value<string>("CalledRepairOrderID"));
                        fail++;
                    }
                }
                else
                {
                    failIDs = UtilBussinessService.str(failIDs, data.Value<string>("CalledRepairOrderID"));
                    msg = UtilBussinessService.str(msg, model.Code + "处于非立单状态,不能删除");
                    fail++;
                }


            }
            return new { success = success, fail = fail, failIDs = failIDs, msg = msg };
        }

        /// <summary>
        /// 叫修单的更新
        /// SAM 2017年5月29日14:20:41
        /// </summary>
        /// <param name="Token">授权码</param>
        /// <param name="jArray"></param>
        /// <returns></returns>
        public static object Ems00004update(string Token, JArray jArray)
        {
            int success = 0;
            int fail = 0;
            string failIDs = null;
            string msg = null;
            string SysID = Framework.SystemID;
            string userid = UtilBussinessService.detoken(Token);
            JObject data = null;
            EMS_CalledRepairOrder model = null;
            EMS_Equipment Eqmodel = null;
            for (int i = 0; i < jArray.Count; i++)
            {
                data = (JObject)jArray[i];
                model = EMS_CalledRepairOrderService.get(data.Value<string>("CalledRepairOrderID"));
                if (model.Status == SysID + "020121300002A" || model.Status == SysID + "020121300002B")//狀態為註銷CA、結案CL時，全部不可修改
                {
                    failIDs = UtilBussinessService.str(failIDs, data.Value<string>("CalledRepairOrderID"));
                    msg = UtilBussinessService.str(msg, model.Code + "的状态为注销或者结案,不能修改");
                    fail++;
                    continue; //跳过循环
                }

                Eqmodel = EMS_EquipmentService.get(data.Value<string>("EquipmentID"));
                if (Eqmodel == null)
                {
                    failIDs = UtilBussinessService.str(failIDs, data.Value<string>("CalledRepairOrderID"));
                    msg = UtilBussinessService.str(msg, "不存在的设备代号");
                    fail++;
                    continue; //跳过循环
                }

                model.Date = data.Value<DateTime>("Date");
                model.EquipmentID = data.Value<string>("EquipmentID");
                model.CallMESUserID = data.Value<string>("CallMESUserID");
                model.CallOrganizationID = data.Value<string>("CallOrganizationID");
                model.Status = data.Value<string>("Status");
                model.Comments = data.Value<string>("Comments");
                if (EMS_CalledRepairOrderService.update(userid, model))
                {
                    if (model.Status == SysID + "0201213000029") //狀態改成核准時，要將設備主檔的機況變更為維修中
                    {
                        Eqmodel.Condition = SysID + "0201213000023";
                        EMS_EquipmentService.update(userid, Eqmodel);
                    }
                    else if (model.Status == SysID + "020121300002B") //狀態改成註銷後存檔，設備主檔的機況要變更為待机中
                    {
                        Eqmodel.Condition = SysID + "02012130000A5";
                        EMS_EquipmentService.update(userid, Eqmodel);
                    }
                    success++;
                }
                else
                {
                    failIDs = UtilBussinessService.str(failIDs, data.Value<string>("CalledRepairOrderID"));
                    fail++;
                }
            }
            return new { success = success, fail = fail, failIDs = failIDs, msg = msg };
        }

        /// <summary>
        /// 根据叫修单获取他的叫修原因
        /// SAM 2017年5月24日14:46:35
        /// </summary>
        /// <param name="Token">授权码</param>
        /// <param name="CalledRepairOrderID"></param>
        /// <param name="page">页码</param>
        /// <param name="rows">行数</param>
        /// <returns></returns>
        public static object Ems00004GetReasonList(string token, string CalledRepairOrderID, int page, int rows)
        {
            int count = 0;
            IList<Hashtable> result = EMS_CalledRepairReasonService.Ems00004GetReasonList(CalledRepairOrderID, page, rows, ref count);
            return UtilBussinessService.getPaginationModel(result, count);
        }

        /// <summary>
        /// 添加叫修原因
        /// SAM 2017年5月29日15:53:04
        /// </summary>
        /// <param name="Token">授权码</param>
        /// <param name="jArray"></param>
        /// <returns></returns>
        public static object Ems00004Reasoninsert(string Token, JArray jArray)
        {
            int success = 0;
            int fail = 0;
            string failIDs = null;
            string msg = null;
            string userid = UtilBussinessService.detoken(Token);
            JObject data = null;
            string SysID = Framework.SystemID;
            EMS_CalledRepairReason model = null;
            EMS_CalledRepairOrder Ordermodel = null;
            for (int i = 0; i < jArray.Count; i++)
            {
                data = (JObject)jArray[i];

                model = new EMS_CalledRepairReason();
                model.CalledRepairReasonID = UniversalService.GetSerialNumber("EMS_CalledRepairReason");
                model.CalledRepairOrderID = data.Value<string>("CalledRepairOrderID");
                model.ReasonID = data.Value<string>("ReasonID");
                model.ReasonDescription = data.Value<string>("ReasonDescription");
                model.Comments = data.Value<string>("Comments");
                model.DealWithDescription = data.Value<string>("DealWithDescription");
                model.Status = Framework.SystemID + "0201213000001";

                Ordermodel = EMS_CalledRepairOrderService.get(data.Value<string>("CalledRepairOrderID"));
                if (Ordermodel == null)
                {
                    failIDs = UtilBussinessService.str(failIDs, data.Value<string>("CalledRepairReasonID"));
                    msg = UtilBussinessService.str(msg, "不存在的叫修单号");
                    fail++;
                    continue; //跳过循环
                }
                if (Ordermodel.Status == SysID + "020121300002A" || Ordermodel.Status == SysID + "020121300002B")//狀態為註銷CA、結案CL時，全部不可修改
                {
                    failIDs = UtilBussinessService.str(failIDs, data.Value<string>("CalledRepairOrderID"));
                    msg = UtilBussinessService.str(msg, Ordermodel.Code + "的状态为注销或者结案,不能添加叫修原因");
                    fail++;
                    continue; //跳过循环
                }

                if (string.IsNullOrWhiteSpace(data.Value<string>("ReasonID")))
                {
                    if (EMS_CalledRepairReasonService.insert(userid, model))
                        success++;
                    else
                    {
                        failIDs = UtilBussinessService.str(failIDs, data.Value<string>("CalledRepairReasonID"));
                        fail++;
                    }
                }
                else if (!EMS_CalledRepairReasonService.CheckReason(data.Value<string>("ReasonID"), data.Value<string>("CalledRepairOrderID"), null))
                {
                    if (EMS_CalledRepairReasonService.insert(userid, model))
                        success++;
                    else
                    {
                        failIDs = UtilBussinessService.str(failIDs, data.Value<string>("CalledRepairReasonID"));
                        fail++;
                    }
                }
                else
                {
                    failIDs = UtilBussinessService.str(failIDs, data.Value<string>("CalledRepairOrderID"));
                    msg = UtilBussinessService.str(msg, "资料重复");
                    fail++;
                }
            }
            return new { success = success, fail = fail, failIDs = failIDs, msg = msg };
        }

        /// <summary>
        /// 删除叫修原因
        /// SAM 2017年5月29日15:54:46
        /// 改成单一删除
        /// Jakc 2017年8月7日16:13:28
        /// </summary>
        /// <param name="Token">授权码</param>
        /// <param name="jArray"></param>
        /// <returns></returns>
        public static object Ems00004Reasondelete(string Token, JObject request)
        {
            EMS_CalledRepairOrder Ordermodel = EMS_CalledRepairOrderService.get(request.Value<string>("CalledRepairOrderID"));
            if (Ordermodel == null)
            {
                return new { status = "410", msg = "不存在的叫修单号！" };
            }
            if (Ordermodel.Status == Framework.SystemID + "020121300002A" || Ordermodel.Status == Framework.SystemID + "020121300002B")//狀態為註銷CA、結案CL時，全部不可修改
            {
                return new { status = "410", msg = Ordermodel.Code + "的状态为注销或者结案,不能删除叫修原因" };
            }
            EMS_CalledRepairReason model = EMS_CalledRepairReasonService.get(request.Value<string>("CalledRepairReasonID"));
            if (model == null)
            {
                return new { status = "410", msg = "不存在的叫修原因！" };
            }
            string userid = UtilBussinessService.detoken(Token);
            model.Status = Framework.SystemID + "0201213000003";
            if (EMS_CalledRepairReasonService.update(userid, model))
                return new { status = "200", msg = "删除成功!" };
            else
                return new { status = "410", msg = "删除失败!" };
        }

        /// <summary>
        /// 更新叫修原因
        /// SAM 2017年5月29日15:59:41
        /// </summary>
        /// <param name="Token">授权码</param>
        /// <param name="jArray"></param>
        /// <returns></returns>
        public static object Ems00004Reasonupdate(string Token, JArray jArray)
        {
            int success = 0;
            int fail = 0;
            string failIDs = null;
            string msg = null;
            string SysID = Framework.SystemID;
            string userid = UtilBussinessService.detoken(Token);
            JObject data = null;
            EMS_CalledRepairReason model = null;
            EMS_CalledRepairOrder Ordermodel = null;
            for (int i = 0; i < jArray.Count; i++)
            {
                data = (JObject)jArray[i];
                model = EMS_CalledRepairReasonService.get(data.Value<string>("CalledRepairReasonID"));

                Ordermodel = EMS_CalledRepairOrderService.get(data.Value<string>("CalledRepairOrderID"));
                if (Ordermodel == null)
                {
                    failIDs = UtilBussinessService.str(failIDs, data.Value<string>("CalledRepairReasonID"));
                    msg = UtilBussinessService.str(msg, "不存在的叫修单号");
                    fail++;
                    continue; //跳过循环
                }
                if (Ordermodel.Status == SysID + "020121300002A" || Ordermodel.Status == SysID + "020121300002B")//狀態為註銷CA、結案CL時，全部不可修改
                {
                    failIDs = UtilBussinessService.str(failIDs, data.Value<string>("CalledRepairReasonID"));
                    msg = UtilBussinessService.str(msg, Ordermodel.Code + "的状态为注销或者结案,不能修改叫修原因");
                    fail++;
                    continue; //跳过循环
                }

                model.ReasonID = data.Value<string>("ReasonID");
                model.ReasonDescription = data.Value<string>("ReasonDescription");
                model.DealWithDescription = data.Value<string>("DealWithDescription");
                model.Comments = data.Value<string>("Comments");

                if (string.IsNullOrWhiteSpace(data.Value<string>("ReasonID")))
                {
                    if (EMS_CalledRepairReasonService.update(userid, model))
                        success++;
                    else
                    {
                        failIDs = UtilBussinessService.str(failIDs, data.Value<string>("CalledRepairReasonID"));
                        fail++;
                    }
                }
                else if (!EMS_CalledRepairReasonService.CheckReason(data.Value<string>("ReasonID"), data.Value<string>("CalledRepairOrderID"), data.Value<string>("CalledRepairReasonID")))
                {
                    if (EMS_CalledRepairReasonService.update(userid, model))
                        success++;
                    else
                    {
                        failIDs = UtilBussinessService.str(failIDs, data.Value<string>("CalledRepairReasonID"));
                        fail++;
                    }
                }
                else
                {
                    failIDs = UtilBussinessService.str(failIDs, data.Value<string>("CalledRepairReasonID"));
                    msg = UtilBussinessService.str(msg, "资料重复");
                    fail++;
                }
            }
            return new { success = success, fail = fail, failIDs = failIDs, msg = msg };
        }


        #endregion

        #region EMS00005设备维修作业
        /// <summary>
        /// 设备维修作业的列表
        /// SAM 2017年5月29日23:11:52
        /// </summary>
        /// <param name="Token">授权码</param>
        /// <param name="Code">代号</param>
        /// <param name="Status">状态</param>
        /// <param name="page">页码</param>
        /// <param name="rows">行数</param>
        /// <returns></returns>
        public static object Ems00005GetList(string token, string code, int page, int rows)
        {
            int count = 0;
            IList<Hashtable> result = EMS_CalledRepairOrderService.Ems00005GetList(code, page, rows, ref count);
            return UtilBussinessService.getPaginationModel(result, count);
        }


        /// <summary>
        /// 设备维修作业更新
        /// SAM 2017年5月29日23:18:19
        /// </summary>
        /// <param name="Token">授权码</param>
        /// <param name="jArray"></param>
        /// <returns></returns>
        public static object Ems00005update(string Token, JArray jArray)
        {
            int success = 0;
            int fail = 0;
            string failIDs = null;
            string msg = null;
            string SysID = Framework.SystemID;
            string userid = UtilBussinessService.detoken(Token);
            JObject data = null;
            EMS_CalledRepairOrder model = null;
            for (int i = 0; i < jArray.Count; i++)
            {
                data = (JObject)jArray[i];
                model = EMS_CalledRepairOrderService.get(data.Value<string>("CalledRepairOrderID"));
                if (model.Status == SysID + "020121300002A" || model.Status == SysID + "020121300002B")//狀態為註銷CA、結案CL時，全部不可修改
                {
                    failIDs = UtilBussinessService.str(failIDs, data.Value<string>("CalledRepairOrderID"));
                    msg = UtilBussinessService.str(msg, model.Code + "的状态为注销或者结案,不能修改");
                    fail++;
                    continue; //跳过循环
                }

                model.MESUserID = data.Value<string>("MESUserID");
                model.InOutRepair = data.Value<string>("InOutRepair");
                model.ManufacturerID = data.Value<string>("ManufacturerID");
                if (EMS_CalledRepairOrderService.Ems00005update(userid, model))
                    success++;
                else
                {
                    failIDs = UtilBussinessService.str(failIDs, data.Value<string>("CalledRepairOrderID"));
                    fail++;
                }
            }
            return new { success = success, fail = fail, failIDs = failIDs, msg = msg };
        }



        /// <summary>
        /// 获取维修记录
        /// TODO
        /// SAM 2017年5月29日23:20:08
        /// </summary>
        /// <param name="Token">授权码</param>
        /// <param name="CalledRepairOrderID"></param>
        /// <param name="page">页码</param>
        /// <param name="rows">行数</param>
        /// <returns></returns>
        public static object Ems00005GetServiceList(string token, string CalledRepairOrderID, int page, int rows)
        {
            int count = 0;
            IList<Hashtable> result = EMS_ServiceReasonLogService.Ems00005GetServiceList(CalledRepairOrderID, page, rows, ref count);
            return UtilBussinessService.getPaginationModel(result, count);
        }

        /// <summary>
        /// 维修记录表头新增
        /// SAM 2017年6月2日11:14:01
        /// </summary>
        /// <param name="Token">授权码</param>
        /// <param name="jArray"></param>
        /// <returns></returns>
        public static object Ems00005Serviceinsert(string Token, JArray jArray)
        {
            int success = 0;
            int fail = 0;
            string failIDs = null;
            string msg = null;
            string userid = UtilBussinessService.detoken(Token);
            JObject data = null;
            string SysID = Framework.SystemID;
            EMS_ServiceReasonLog model = null;
            EMS_CalledRepairOrder Ordermodel = null;
            for (int i = 0; i < jArray.Count; i++)
            {
                data = (JObject)jArray[i];
                model = new EMS_ServiceReasonLog();
                model.ServiceReasonLogID = UniversalService.GetSerialNumber("EMS_ServiceReasonLog");
                model.CalledRepairOrderID = data.Value<string>("CalledRepairOrderID");
                model.ReasonID = data.Value<string>("ReasonID");
                model.ReasonDescription = data.Value<string>("ReasonDescription");
                model.ReasonGroupID = data.Value<string>("ReasonGroupID");
                model.Comments = data.Value<string>("Comments");
                model.Status = Framework.SystemID + "0201213000001";

                Ordermodel = EMS_CalledRepairOrderService.get(data.Value<string>("CalledRepairOrderID"));
                if (Ordermodel == null)
                {
                    failIDs = UtilBussinessService.str(failIDs, data.Value<string>("ServiceReasonLogID"));
                    msg = UtilBussinessService.str(msg, "不存在的叫修单号");
                    fail++;
                    continue; //跳过循环
                }

                if (!string.IsNullOrWhiteSpace(data.Value<string>("ReasonID")))
                {
                    if (!EMS_ServiceReasonLogService.CheckReason(data.Value<string>("ReasonID"), data.Value<string>("CalledRepairOrderID"), null))
                    {
                        if (EMS_ServiceReasonLogService.insert(userid, model))
                            success++;
                        else
                        {
                            failIDs = UtilBussinessService.str(failIDs, data.Value<string>("ServiceReasonLogID"));
                            fail++;
                        }
                    }
                    else
                    {
                        failIDs = UtilBussinessService.str(failIDs, data.Value<string>("ServiceReasonLogID"));
                        msg = UtilBussinessService.str(msg, "资料重复");
                        fail++;
                    }
                }
                else
                {
                    if (EMS_ServiceReasonLogService.insert(userid, model))
                        success++;
                    else
                    {
                        failIDs = UtilBussinessService.str(failIDs, data.Value<string>("ServiceReasonLogID"));
                        fail++;
                    }
                }
            }
            return new { success = success, fail = fail, failIDs = failIDs, msg = msg };
        }


        /// <summary>
        /// 删除维修记录表头
        /// 2017年6月2日11:18:09
        /// </summary>
        /// <param name="Token">授权码</param>
        /// <param name="jArray"></param>
        /// <returns></returns>
        public static object Ems00005Servicedelete(string Token, JArray jArray)
        {
            int success = 0;
            int fail = 0;
            string failIDs = null;
            string msg = null;
            string userid = UtilBussinessService.detoken(Token);
            JObject data = null;
            string SysID = Framework.SystemID;
            EMS_ServiceReasonLog model = null;
            EMS_CalledRepairOrder Ordermodel = null;
            for (int i = 0; i < jArray.Count; i++)
            {
                data = (JObject)jArray[i];
                model = EMS_ServiceReasonLogService.get(data.Value<string>("ServiceReasonLogID"));

                Ordermodel = EMS_CalledRepairOrderService.get(data.Value<string>("CalledRepairOrderID"));
                if (Ordermodel == null)
                {
                    failIDs = UtilBussinessService.str(failIDs, data.Value<string>("ServiceReasonLogID"));
                    msg = UtilBussinessService.str(msg, "不存在的叫修单号");
                    fail++;
                    continue; //跳过循环
                }
                if (Ordermodel.Status == SysID + "020121300002A" || Ordermodel.Status == SysID + "020121300002B")//狀態為註銷CA、結案CL時，全部不可修改
                {
                    failIDs = UtilBussinessService.str(failIDs, data.Value<string>("CalledRepairReasonID"));
                    msg = UtilBussinessService.str(msg, Ordermodel.Code + "的状态为注销或者结案,不能删除维修记录");
                    fail++;
                    continue; //跳过循环
                }
                model.Status = Framework.SystemID + "0201213000003";
                if (EMS_ServiceReasonLogService.update(userid, model))
                    success++;
                else
                {
                    failIDs = UtilBussinessService.str(failIDs, data.Value<string>("ServiceReasonLogID"));
                    fail++;
                }
            }
            return new { success = success, fail = fail, failIDs = failIDs, msg = msg };
        }

        /// <summary>
        /// 更新维修记录表头
        /// SAM 2017年6月2日11:19:03
        /// </summary>
        /// <param name="Token">授权码</param>
        /// <param name="jArray"></param>
        /// <returns></returns>
        public static object Ems00005Serviceupdate(string Token, JArray jArray)
        {
            int success = 0;
            int fail = 0;
            string failIDs = null;
            string msg = null;
            string SysID = Framework.SystemID;
            string userid = UtilBussinessService.detoken(Token);
            JObject data = null;
            EMS_ServiceReasonLog model = null;
            EMS_CalledRepairOrder Ordermodel = null;
            for (int i = 0; i < jArray.Count; i++)
            {
                data = (JObject)jArray[i];
                model = EMS_ServiceReasonLogService.get(data.Value<string>("ServiceReasonLogID"));

                Ordermodel = EMS_CalledRepairOrderService.get(data.Value<string>("CalledRepairOrderID"));
                if (Ordermodel == null)
                {
                    failIDs = UtilBussinessService.str(failIDs, data.Value<string>("ServiceReasonLogID"));
                    msg = UtilBussinessService.str(msg, "不存在的叫修单号");
                    fail++;
                    continue; //跳过循环
                }
                if (Ordermodel.Status == SysID + "020121300002A" || Ordermodel.Status == SysID + "020121300002B")//狀態為註銷CA、結案CL時，全部不可修改
                {
                    failIDs = UtilBussinessService.str(failIDs, data.Value<string>("ServiceReasonLogID"));
                    msg = UtilBussinessService.str(msg, Ordermodel.Code + "的状态为注销或者结案,不能修改维修记录");
                    fail++;
                    continue; //跳过循环
                }

                if (!EMS_ServiceReasonLogService.CheckReason(data.Value<string>("ReasonID"), data.Value<string>("CalledRepairOrderID"), data.Value<string>("ServiceReasonLogID")))
                {
                    model.ReasonID = data.Value<string>("ReasonID");
                    model.ReasonDescription = data.Value<string>("ReasonDescription");
                    model.Comments = data.Value<string>("Comments");
                    model.ReasonGroupID = data.Value<string>("ReasonGroupID");
                    if (EMS_ServiceReasonLogService.update(userid, model))
                        success++;
                    else
                    {
                        failIDs = UtilBussinessService.str(failIDs, data.Value<string>("ServiceReasonLogID"));
                        fail++;
                    }
                }
                else
                {
                    failIDs = UtilBussinessService.str(failIDs, data.Value<string>("ServiceReasonLogID"));
                    msg = UtilBussinessService.str(msg, "资料重复");
                    fail++;
                }
            }
            return new { success = success, fail = fail, failIDs = failIDs, msg = msg };
        }

        /// <summary>
        /// 维修设备表头单个删除
        /// Mouse 2017年7月28日10:52:19
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public static object Ems00005ServiceDelete(JObject request)
        {
            string Token = request.Value<string>("Token");
            string userid = UtilBussinessService.detoken(Token);
            string ServiceReasonLogID = request.Value<string>("ServiceReasonLogID");

            EMS_ServiceReasonLog model = EMS_ServiceReasonLogService.get(ServiceReasonLogID);
            if (model == null)
                return new { status = "410", msg = "删除失败！不存在的维修设备表头" };

            model.Status = Framework.SystemID + "0201213000003";

            if (EMS_ServiceReasonLogService.update(userid, model))
                return new { status = "200", msg = "删除成功！" };
            else
                return new { status = "410", msg = "删除失败！" };
        }

        /// <summary>
        /// 维修记录明细的列表
        /// SAM 2017年6月2日11:24:44
        /// </summary>
        /// <param name="Token">授权码</param>
        /// <param name="CalledRepairOrderID"></param>
        /// <param name="page">页码</param>
        /// <param name="rows">行数</param>
        /// <returns></returns>
        public static object Ems00005GetServiceDetailsList(string token, string ServiceReasonLogID, int page, int rows)
        {
            int count = 0;
            IList<Hashtable> result = EMS_ServiceReasonLogDetailsService.Ems00005GetServiceDetailsList(ServiceReasonLogID, page, rows, ref count);
            return UtilBussinessService.getPaginationModel(result, count);
        }

        /// <summary>
        /// 新增维修记录明细
        /// SAM 2017年6月2日11:39:52
        /// </summary>
        /// <param name="Token">授权码</param>
        /// <param name="jArray"></param>
        /// <returns></returns>
        public static object Ems00005ServiceDetailsinsert(string Token, JArray jArray)
        {
            int success = 0;
            int fail = 0;
            string failIDs = null;
            string msg = null;
            string userid = UtilBussinessService.detoken(Token);
            JObject data = null;
            string SysID = Framework.SystemID;
            EMS_ServiceReasonLogDetails model = null;
            for (int i = 0; i < jArray.Count; i++)
            {
                data = (JObject)jArray[i];
                model = new EMS_ServiceReasonLogDetails();
                model.SRLDID = UniversalService.GetSerialNumber("EMS_ServiceReasonLogDetails");
                model.Sequence = string.IsNullOrWhiteSpace(data.Value<string>("Sequence")) ? 0 : data.Value<int>("Sequence");
                model.ServiceReasonLogID = data.Value<string>("ServiceReasonLogID");
                model.MESUserID = data.Value<string>("MESUserID");
                model.OrganizationID = data.Value<string>("OrganizationID");
                model.ManufacturerID = data.Value<string>("ManufacturerID");
                model.IsFee = data.Value<bool>("IsFee");
                model.Description = data.Value<string>("Description");
                model.Comments = data.Value<string>("Comments");
                model.Status = Framework.SystemID + "0201213000001";
                if (EMS_ServiceReasonLogDetailsService.insert(userid, model))
                    success++;
                else
                {
                    failIDs = UtilBussinessService.str(failIDs, data.Value<string>("SRLDID"));
                    fail++;
                }

            }
            return new { success = success, fail = fail, failIDs = failIDs, msg = msg };
        }

        /// <summary>
        /// 删除维修记录明细
        /// SAM 2017年6月2日11:50:01
        /// </summary>
        /// <param name="Token">授权码</param>
        /// <param name="jArray"></param>
        /// <returns></returns>
        public static object Ems00005ServiceDetailsdelete(string Token, JArray jArray)
        {
            int success = 0;
            int fail = 0;
            string failIDs = null;
            string msg = null;
            string userid = UtilBussinessService.detoken(Token);
            JObject data = null;
            string SysID = Framework.SystemID;
            EMS_ServiceReasonLogDetails model = null;
            for (int i = 0; i < jArray.Count; i++)
            {
                data = (JObject)jArray[i];
                model = EMS_ServiceReasonLogDetailsService.get(data.Value<string>("SRLDID"));


                model.Status = Framework.SystemID + "0201213000003";
                if (EMS_ServiceReasonLogDetailsService.updateStatus(userid, model))
                    success++;
                else
                {
                    failIDs = UtilBussinessService.str(failIDs, data.Value<string>("SRLDID"));
                    fail++;
                }
            }
            return new { success = success, fail = fail, failIDs = failIDs, msg = msg };
        }

        /// <summary>
        /// 更新维修记录明细
        /// SAM 2017年6月2日11:50:41
        /// </summary>
        /// <param name="Token">授权码</param>
        /// <param name="jArray"></param>
        /// <returns></returns>
        public static object Ems00005ServicDetailseupdate(string Token, JArray jArray)
        {
            int success = 0;
            int fail = 0;
            string failIDs = null;
            string msg = null;
            string SysID = Framework.SystemID;
            string userid = UtilBussinessService.detoken(Token);
            JObject data = null;
            EMS_ServiceReasonLogDetails model = null;
            for (int i = 0; i < jArray.Count; i++)
            {
                data = (JObject)jArray[i];
                model = EMS_ServiceReasonLogDetailsService.get(data.Value<string>("SRLDID"));
                model.Sequence = string.IsNullOrWhiteSpace(data.Value<string>("Sequence")) ? 0 : data.Value<int>("Sequence");
                model.MESUserID = data.Value<string>("MESUserID");
                model.OrganizationID = data.Value<string>("OrganizationID");
                model.ManufacturerID = data.Value<string>("ManufacturerID");
                model.IsFee = data.Value<bool>("IsFee");
                model.Description = data.Value<string>("Description");
                model.Comments = data.Value<string>("Comments");
                if (string.IsNullOrWhiteSpace(data.Value<string>("StartTime")))
                    model.StartTime = null;
                else
                    model.StartTime = data.Value<DateTime>("StartTime");
                if (string.IsNullOrWhiteSpace(data.Value<string>("EndTime")))
                    model.EndTime = null;
                else
                    model.EndTime = data.Value<DateTime>("EndTime");
                if (string.IsNullOrWhiteSpace(data.Value<string>("StartTime")) || string.IsNullOrWhiteSpace(data.Value<string>("EndTime")))
                    model.Hour = 0;
                else
                {
                    TimeSpan ts = DateTime.Parse(model.EndTime.ToString()) - DateTime.Parse(model.StartTime.ToString());
                    model.Hour = Math.Round((decimal)ts.TotalMinutes / 60, 2);
                }
                if (EMS_ServiceReasonLogDetailsService.update(userid, model))
                    success++;
                else
                {
                    failIDs = UtilBussinessService.str(failIDs, data.Value<string>("SRLDID"));
                    fail++;
                }

            }
            return new { success = success, fail = fail, failIDs = failIDs, msg = msg };
        }

        /// <summary>
        /// 维修设备明细单个删除
        /// Mouse 2017年7月28日10:52:19
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public static object Ems00005ServiceDetailDelete(JObject request)
        {
            string Token = request.Value<string>("Token");
            string userid = UtilBussinessService.detoken(Token);
            string SRLDID = request.Value<string>("SRLDID");

            EMS_ServiceReasonLogDetails model = EMS_ServiceReasonLogDetailsService.get(SRLDID);

            if (model == null)
                return new { status = "410", msg = "删除失败！不存在的维修设备明细" };

            model.Status = Framework.SystemID + "0201213000003";

            if (EMS_ServiceReasonLogDetailsService.update(userid, model))
                return new { status = "200", msg = "删除成功！" };
            else
                return new { status = "410", msg = "删除失败！" };
        }

        /// <summary>
        /// 开始明细
        /// SAM 2017年6月2日11:53:55
        /// </summary>
        /// <param name="request">JSON数据</param>
        /// <returns></returns>
        public static object Ems00005DetailsStart(JObject request)
        {
            string Token = request.Value<string>("Token");
            string userid = UtilBussinessService.detoken(Token);
            string SRLDID = request.Value<string>("SRLDID");
            EMS_ServiceReasonLogDetails model = EMS_ServiceReasonLogDetailsService.get(SRLDID);
            if (model == null)
                return new { status = "410", msg = "流水号为空！" };

            model.StartTime = DateTime.Now;
            if (EMS_ServiceReasonLogDetailsService.updateStartTime(userid, model))
                return new { status = "200", msg = "开始成功！" };
            else
                return new { status = "410", msg = "开始失败！" };
        }

        /// <summary>
        /// 结束明细
        /// SAM 2017年6月2日11:54:58
        /// </summary>
        /// <param name="request">JSON数据</param>
        /// <returns></returns>
        public static object Ems00005DetailsEnd(JObject request)
        {
            string Token = request.Value<string>("Token");
            string userid = UtilBussinessService.detoken(Token);
            string SRLDID = request.Value<string>("SRLDID");
            EMS_ServiceReasonLogDetails model = EMS_ServiceReasonLogDetailsService.get(SRLDID);
            if (model == null)
                return new { status = "410", msg = "流水号为空！" };

            model.EndTime = DateTime.Now;
            if (model.StartTime != null)
            {
                TimeSpan ts = DateTime.Parse(model.EndTime.ToString()) - DateTime.Parse(model.StartTime.ToString());
                model.Hour = Math.Round((decimal)ts.TotalMinutes / 60, 2);
            }
            else
            {
                model.Hour = 0;
            }
            if (EMS_ServiceReasonLogDetailsService.updateEndTime(userid, model))
                return new { status = "200", msg = "结束成功！" };
            else
                return new { status = "410", msg = "结束失败！" };
        }

        /// <summary>
        /// 设备维修作业开始维修
        /// MOUSE 2017年7月26日17:42:41
        /// MOUSE 2017年8月3日16:57:07 修改
        /// </summary>
        /// <param name="request">JSON数据</param>
        /// <returns></returns>
        public static object Ems0005RepairStart(JObject request)
        {
            string Token = request.Value<string>("Token");
            string userid = UtilBussinessService.detoken(Token);
            string EquipmentID = request.Value<string>("EquipmentID");
            EMS_Equipment model = EMS_EquipmentService.get(EquipmentID);
            if (model == null)
                return new { status = "410", msg = "开始失败，该设备维修作业不存在！" };
            if (model.Condition == (Framework.SystemID + "02012130000A5"))
            {
                model.Condition = Framework.SystemID + "0201213000023";
                EMS_EquipmentService.update(userid, model);
                return new { status = "200", msg = "从待机状态改变为维修状态" }; 
            }
            else
            {
                return new { status = "410", msg = "此设备机况不在待机状态，不可维修" };
            }
        }

        /// <summary>
        /// 设备维修作业结束维修
        /// MOUSE 2017年7月26日18:21:05
        /// </summary>
        /// <param name="request">JSON数据</param>
        /// <returns></returns>
        public static object Ems0005RepairEnd(JObject request)
        {
            string Token = request.Value<string>("Token");
            string userid = UtilBussinessService.detoken(Token);
            string EquipmentID = request.Value<string>("EquipmentID");
            EMS_Equipment model = EMS_EquipmentService.get(EquipmentID);
            if (model == null)
                return new { status = "410", msg = "结束失败，该设备维修作业不存在！" };
            string CalledRepairOrderID = request.Value<string>("CalledRepairOrderID");

            EMS_CalledRepairOrder OrderModel = EMS_CalledRepairOrderService.get(CalledRepairOrderID);
            if (OrderModel == null)
                return new { status = "410", msg = "结束失败，不存在的叫修单信息！" };

            //判断明细是否都已经结束
            if (EMS_ServiceReasonLogDetailsService.Check(CalledRepairOrderID))
                return new { status = "410", msg = "维修处理记录明细存在未结束的明细！" };

            if (model.Condition == (Framework.SystemID + "0201213000023"))//判断是否为维修状态
            {
                model.Condition = Framework.SystemID + "02012130000A5";//把维修状态改为待机状态
                EMS_EquipmentService.update(userid, model);
                return new { status = "200", msg = "从维修状态改为停止状态" };
            }
            else
                return new { status = "410", msg = "设备并不在维修状态，无法更改" };
        }

        #endregion

        #region EMS00006设备叫修结案处理
        /// <summary>
        /// 设备叫修结案处理列表
        /// SAM 2017年6月5日14:44:00
        /// </summary>
        /// <param name="Token">授权码</param>
        /// <param name="Code">代号</param>
        /// <param name="Status">状态</param>
        /// <param name="page">页码</param>
        /// <param name="rows">行数</param>
        /// <returns></returns>
        public static object Ems00006GetList(string token, string code, string status, int page, int rows)
        {
            int count = 0;
            IList<Hashtable> result = EMS_CalledRepairOrderService.Ems00006GetList(code, status, page, rows, ref count);
            return UtilBussinessService.getPaginationModel(result, count);
        }

        /// <summary>
        /// 设备叫修结案处理的保存
        /// SAM 2017年6月5日16:32:39
        /// </summary>
        /// <param name="Token">授权码</param>
        /// <param name="jArray"></param>
        /// <returns></returns>
        public static object Ems00006update(string Token, JArray jArray)
        {
            int success = 0;
            int fail = 0;
            string failIDs = null;
            string msg = null;
            string SysID = Framework.SystemID;
            string userid = UtilBussinessService.detoken(Token);
            JObject data = null;
            EMS_CalledRepairOrder model = null;
            for (int i = 0; i < jArray.Count; i++)
            {
                data = (JObject)jArray[i];
                model = EMS_CalledRepairOrderService.get(data.Value<string>("CalledRepairOrderID"));
                if (data.Value<bool>("IsClose"))
                {
                    EMS_Equipment equipment = EMS_EquipmentService.get(model.EquipmentID);
                    if (equipment != null)
                    {
                        if (equipment.Condition != Framework.SystemID + "02012130000A5")
                        {
                            failIDs = UtilBussinessService.str(failIDs, data.Value<string>("CalledRepairOrderID"));
                            msg = UtilBussinessService.str(msg, "对应数据并没有结束维修!");
                            fail++;
                            continue;
                        }
                    }
                    //判断明细是否都已经结束
                    if (EMS_ServiceReasonLogDetailsService.Check(model.CalledRepairOrderID))
                    {
                        failIDs = UtilBussinessService.str(failIDs, data.Value<string>("CalledRepairOrderID"));
                        msg = UtilBussinessService.str(msg, "维修处理记录明细存在未结束的明细!");
                        fail++;
                        continue;
                    }
                    model.CloseDate = DateTime.Now;
                    model.CloseMESUserID = userid;
                    model.Status = Framework.SystemID + "020121300002A";
                    if (EMS_CalledRepairOrderService.Ems00006update(userid, model))
                        success++;
                    else
                    {
                        failIDs = UtilBussinessService.str(failIDs, data.Value<string>("CalledRepairOrderID"));
                        fail++;
                    }
                }
            }
            return new { success = success, fail = fail, failIDs = failIDs, msg = msg };
        }

        #endregion

        #region EMS00007維修原因統計分析
        /// <summary>
        /// 維修原因統計分析
        /// SAM 2017年8月3日11:45:17
        /// </summary>
        /// <param name="token"></param>
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
        public static object Ems00007GetReasonList(string token, string StartReasonCode, string EndReasonCode, string StartDate, string EndDate, string StartEquipmentCode, string EndEquipmentCode, string Type, int page, int rows)
        {

            int count = 0;
            IList<Hashtable> result = EMS_CalledRepairReasonService.Ems00007GetReasonList(StartReasonCode, EndReasonCode, StartDate, EndDate, StartEquipmentCode, EndEquipmentCode, Type, page, rows, ref count);
            foreach (Hashtable item in result)
            {
                item["Count"] = EMS_CalledRepairReasonService.Ems00007GetReasonCount(item["ReasonID"].ToString(), StartReasonCode, EndReasonCode, StartDate, EndDate, StartEquipmentCode, EndEquipmentCode, Type);
            }
            return result.OrderByDescending(w => w["Count"]);
        }


        /// <summary>
        /// 維修原因統計分析-原因码列表(不分页，用于园饼图)
        /// SAM 2017年8月3日16:07:34
        /// </summary>
        /// <param name="token"></param>
        /// <param name="StartReasonCode"></param>
        /// <param name="EndReasonCode"></param>
        /// <param name="StartDate"></param>
        /// <param name="EndDate"></param>
        /// <param name="StartEquipmentCode"></param>
        /// <param name="EndEquipmentCode"></param>
        /// <param name="Type"></param>
        /// <returns></returns>
        public static object Ems00007GetReason(string token, string StartReasonCode, string EndReasonCode, string StartDate, string EndDate, string StartEquipmentCode, string EndEquipmentCode, string Type)
        {
            IList<Hashtable> result = EMS_CalledRepairReasonService.Ems00007GetReason(StartReasonCode, EndReasonCode, StartDate, EndDate, StartEquipmentCode, EndEquipmentCode, Type);
            ///查询思路
            ///首先根据查询条件，分别获取再范围内的叫修单号和原因码
            ///然后
            foreach (Hashtable item in result)
            {
                item["Count"] = EMS_CalledRepairReasonService.Ems00007GetReasonCount(item["ReasonID"].ToString(), StartReasonCode, EndReasonCode, StartDate, EndDate, StartEquipmentCode, EndEquipmentCode, Type);
            }
            return result.OrderByDescending(w => w["Count"]);
        }

        /// <summary>
        /// 根据原因码获取查询条件内的叫修单明细
        /// SAM 2017年8月3日15:39:11
        /// </summary>
        /// <param name="token"></param>
        /// <param name="ReasonID"></param>
        /// <param name="StartDate"></param>
        /// <param name="EndDate"></param>
        /// <param name="StartEquipmentCode"></param>
        /// <param name="EndEquipmentCode"></param>
        /// <param name="Type"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <returns></returns>
        public static object Ems00007GetReasonDetailList(string token, string ReasonID, string StartDate, string EndDate, string StartEquipmentCode, string EndEquipmentCode, string Type, int page, int rows)
        {

            int count = 0;
            IList<Hashtable> result = EMS_CalledRepairOrderService.Ems00007GetReasonDetailList(ReasonID, StartDate, EndDate, StartEquipmentCode, EndEquipmentCode, Type, page, rows, ref count);
            return result;
        }


        /// <summary>
        ///  維修原因統計分析-设备列表
        ///  SAM 2017年8月3日15:52:16
        /// </summary>
        /// <param name="token"></param>
        /// <param name="StartDate"></param>
        /// <param name="EndDate"></param>
        /// <param name="StartEquipmentCode"></param>
        /// <param name="EndEquipmentCode"></param>
        /// <param name="Type"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <returns></returns>
        public static object Ems00007GetEquipmentList(string token, string StartDate, string EndDate, string StartEquipmentCode, string EndEquipmentCode, string Type)
        {
            IList<Hashtable> result = EMS_CalledRepairReasonService.Ems00007GetEquipmentList(StartDate, EndDate, StartEquipmentCode, EndEquipmentCode, Type);
            if (result.Count != 0)
            {

                Hashtable All = new Hashtable();
                All["Name"] = "合计";
                All["Count"] = 0;
                foreach (Hashtable item in result)
                {
                    item["Count"] = EMS_CalledRepairReasonService.Ems00007GetEquipmentCount(item["ReasonID"].ToString(), item["EquipmentID"].ToString(), StartDate, EndDate,  Type);
                    All["Count"] = int.Parse(All["Count"].ToString()) + int.Parse(item["Count"].ToString());
                }
                result = result.OrderBy(w => w["EquipmentCode"].ToString()).ThenByDescending(w => int.Parse(w["Count"].ToString())).ToList();
                string EquipmentCode = null;
                for (int i = 0; i < result.Count; i++)
                {
                    if (EquipmentCode == result[i]["EquipmentCode"].ToString())
                    {
                        result[i]["EquipmentCode"] = null;
                        result[i]["EquipmentName"] = null;
                    }
                    else
                        EquipmentCode = result[i]["EquipmentCode"].ToString();
                }
                result.Add(All);
            }
            return result;
        }

        /// <summary>
        /// 維修原因統計分析-设备叫修单明细列表
        /// SAM 2017年8月3日16:01:50
        /// </summary>
        /// <param name="token"></param>
        /// <param name="EquipmentID"></param>
        /// <param name="StartDate"></param>
        /// <param name="EndDate"></param>
        /// <param name="StartEquipmentCode"></param>
        /// <param name="EndEquipmentCode"></param>
        /// <param name="Type"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <returns></returns>
        public static object Ems00007GetEquipmentDetailList(string token, string EquipmentID, string StartDate, string EndDate, string StartEquipmentCode, string EndEquipmentCode, string Type, int page, int rows)
        {

            int count = 0;
            IList<Hashtable> result = EMS_CalledRepairOrderService.Ems00007GetEquipmentDetailList(EquipmentID, StartDate, EndDate, StartEquipmentCode, EndEquipmentCode, Type, page, rows, ref count);
            return result;
        }
        #endregion

        #region EMS0008设备保养清单设定 
        /// <summary>
        /// 保养项目主档列表
        /// SAM 2017年7月5日14:25:39
        /// </summary>
        /// <param name="token"></param>
        /// <param name="code"></param>
        /// <param name="Name"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <returns></returns>
        public static object Ems00008GetProjectList(string token, string code, string Name, int page, int rows)
        {
            int count = 0;
            IList<Hashtable> result = SYS_ParameterService.Ems00008GetProjectList(code, Name, page, rows, ref count);
            return UtilBussinessService.getPaginationModel(result, count);
        }

        /// <summary>
        /// 删除保养项目
        /// SAM 2017年7月5日14:36:37
        /// </summary>
        /// <param name="Token"></param>
        /// <param name="jArray"></param>
        /// <returns></returns>
        public static object Ems00008Projectdelete(string Token, JArray jArray)
        {
            int success = 0;
            int fail = 0;
            string failIDs = null;
            string msg = null;
            string userid = UtilBussinessService.detoken(Token);
            JObject data = null;
            SYS_Parameters model = null;
            for (int i = 0; i < jArray.Count; i++)
            {
                data = (JObject)jArray[i];
                model = SYS_ParameterService.get(data.Value<string>("ParameterID"));
                if (!SYS_ParameterService.USE00001Check(data.Value<string>("ParameterID")))
                {
                    model.IsEnable = 2;
                    if (SYS_ParameterService.update(userid, model))
                        success++;
                    else
                    {
                        failIDs = UtilBussinessService.str(failIDs, data.Value<string>("ParameterID"));
                        fail++;
                    }
                }
                else
                {
                    failIDs = UtilBussinessService.str(failIDs, data.Value<string>("ParameterID"));
                    msg = UtilBussinessService.str(msg, model.Code + "已使用，不能删除");
                    fail++;
                }
            }
            return new { success = success, fail = fail, failIDs = failIDs, msg = msg };
        }

        /// <summary>
        /// 新增保养项目
        /// SAM 2017年7月5日14:37:11
        /// </summary>
        /// <param name="Token"></param>
        /// <param name="jArray"></param>
        /// <returns></returns>
        public static object Ems00008Projectinsert(string Token, JArray jArray)
        {
            int success = 0;
            int fail = 0;
            string failIDs = null;
            string msg = null;
            string userid = UtilBussinessService.detoken(Token);
            JObject data = null;
            SYS_Parameters model = null;
            string Type = Framework.SystemID + "0191213000022";
            for (int i = 0; i < jArray.Count; i++)
            {
                data = (JObject)jArray[i];
                if (!SYS_ParameterService.CheckParameter(data.Value<string>("Code"), null, null, Type, null))
                {
                    model = new SYS_Parameters();
                    model.ParameterID = UniversalService.GetSerialNumber("SYS_Parameters");
                    model.ParameterTypeID = Type;
                    model.Code = data.Value<string>("Code");  //代号
                    model.Name = data.Value<string>("Name");//说明
                    model.Comments = data.Value<string>("Comments");
                    model.Description = data.Value<string>("Attribute");//属性
                    model.IsEnable = data.Value<int>("IsEnable"); //状态       
                    model.Sequence = 0;
                    model.UsingType = 0;
                    if (SYS_ParameterService.insert(userid, model))
                        success++;
                    else
                    {
                        failIDs = UtilBussinessService.str(failIDs, data.Value<string>("ParameterID"));
                        fail++;
                    }
                }
                else
                {
                    failIDs = UtilBussinessService.str(failIDs, data.Value<string>("ParameterID"));
                    msg = UtilBussinessService.str(msg, data.Value<string>("Code") + "已存在");
                    fail++;
                }

            }
            return new { success = success, fail = fail, failIDs = failIDs, msg = msg };
        }

        /// <summary>
        /// 更新保养项目
        /// SAM 2017年7月5日14:37:37
        /// </summary>
        /// <param name="Token"></param>
        /// <param name="jArray"></param>
        /// <returns></returns>
        public static object Ems00008Projectupdate(string Token, JArray jArray)
        {
            int success = 0;
            int fail = 0;
            string failIDs = null;
            string msg = null;
            string userid = UtilBussinessService.detoken(Token);
            JObject data = null;
            SYS_Parameters model = null;
            for (int i = 0; i < jArray.Count; i++)
            {
                data = (JObject)jArray[i];
                model = SYS_ParameterService.get(data.Value<string>("ParameterID"));
                model.Name = data.Value<string>("Name");//说明
                model.Comments = data.Value<string>("Comments");
                model.Description = data.Value<string>("Attribute");//属性
                model.IsEnable = data.Value<int>("IsEnable"); //状态       
                if (SYS_ParameterService.update(userid, model))
                    success++;
                else
                {
                    failIDs = UtilBussinessService.str(failIDs, data.Value<string>("ParameterID"));
                    fail++;
                }

            }
            return new { success = success, fail = fail, failIDs = failIDs, msg = msg };
        }


        /// <summary>
        /// 获取保养类型列表
        /// SAM 2017年7月5日14:42:42
        /// </summary>
        /// <param name="token"></param>
        /// <param name="code"></param>
        /// <param name="Name"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <returns></returns>
        public static object Ems00008GetTypeList(string token, string code, string Name, int page, int rows)
        {
            int count = 0;
            IList<Hashtable> result = SYS_ParameterService.Ems00008GetTypeList(code, Name, page, rows, ref count);
            return UtilBussinessService.getPaginationModel(result, count);
        }

        /// <summary>
        /// 删除保养类型
        /// SAM 2017年7月5日14:47:20
        /// </summary>
        /// <param name="Token"></param>
        /// <param name="jArray"></param>
        /// <returns></returns>
        public static object Ems00008Typedelete(string Token, JArray jArray)
        {
            int success = 0;
            int fail = 0;
            string failIDs = null;
            string msg = null;
            string userid = UtilBussinessService.detoken(Token);
            JObject data = null;
            SYS_Parameters model = null;
            for (int i = 0; i < jArray.Count; i++)
            {
                data = (JObject)jArray[i];
                model = SYS_ParameterService.get(data.Value<string>("ParameterID"));
                if (!SYS_ParameterService.USE00001Check(data.Value<string>("ParameterID")))
                {
                    model.IsEnable = 2;
                    if (SYS_ParameterService.update(userid, model))
                        success++;
                    else
                    {
                        failIDs = UtilBussinessService.str(failIDs, data.Value<string>("ParameterID"));
                        fail++;
                    }
                }
                else
                {
                    failIDs = UtilBussinessService.str(failIDs, data.Value<string>("ParameterID"));
                    msg = UtilBussinessService.str(msg, model.Code + "已使用，不能删除");
                    fail++;
                }
            }
            return new { success = success, fail = fail, failIDs = failIDs, msg = msg };
        }

        /// <summary>
        /// 更新保养类型
        /// SAM 2017年7月5日14:47:30
        /// </summary>
        /// <param name="Token"></param>
        /// <param name="jArray"></param>
        /// <returns></returns>
        public static object Ems00008Typeupdate(string Token, JArray jArray)
        {
            int success = 0;
            int fail = 0;
            string failIDs = null;
            string msg = null;
            string userid = UtilBussinessService.detoken(Token);
            JObject data = null;
            SYS_Parameters model = null;
            for (int i = 0; i < jArray.Count; i++)
            {
                data = (JObject)jArray[i];
                model = SYS_ParameterService.get(data.Value<string>("ParameterID"));
                model.Name = data.Value<string>("Name");//说明
                model.Comments = data.Value<string>("Comments");
                model.IsEnable = data.Value<int>("IsEnable"); //状态       
                if (SYS_ParameterService.update(userid, model))
                    success++;
                else
                {
                    failIDs = UtilBussinessService.str(failIDs, data.Value<string>("ParameterID"));
                    fail++;
                }

            }
            return new { success = success, fail = fail, failIDs = failIDs, msg = msg };
        }


        /// <summary>
        /// 设备保养清单设定列表
        /// SAM 2017年7月5日14:58:47
        /// </summary>
        /// <param name="token"></param>
        /// <param name="code"></param>
        /// <param name="Name"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <returns></returns>
        public static object Ems00008GetList(string token, string code, string Name, int page, int rows)
        {
            int count = 0;
            IList<Hashtable> result = EMS_EquipmentMaintenanceListService.Ems00008GetList(code, Name, page, rows, ref count);
            return UtilBussinessService.getPaginationModel(result, count);
        }


        /// <summary>
        /// 删除保养清单设定
        /// SAM 2017年7月5日14:36:37
        /// </summary>
        /// <param name="Token"></param>
        /// <param name="jArray"></param>
        /// <returns></returns>
        public static object Ems00008delete(string Token, JArray jArray)
        {
            int success = 0;
            int fail = 0;
            string failIDs = null;
            string msg = null;
            string userid = UtilBussinessService.detoken(Token);
            JObject data = null;
            EMS_EquipmentMaintenanceList model = null;
            for (int i = 0; i < jArray.Count; i++)
            {
                data = (JObject)jArray[i];
                model = EMS_EquipmentMaintenanceListService.get(data.Value<string>("EquipmentMaintenanceListID"));
                //if (SYS_ParameterService.USE00001Check(data.Value<string>("ParameterID")))
                //{
                //    failIDs = UtilBussinessService.str(failIDs, data.Value<string>("EquipmentMaintenanceListID"));
                //    msg = UtilBussinessService.str(msg, model.Code + "已使用，不能删除");
                //    fail++;
                //    continue;
                //}
                model.Status = Framework.SystemID + "0201213000003";
                if (EMS_EquipmentMaintenanceListService.update(userid, model))
                    success++;
                else
                {
                    failIDs = UtilBussinessService.str(failIDs, data.Value<string>("EquipmentMaintenanceListID"));
                    fail++;
                }
            }
            return new { success = success, fail = fail, failIDs = failIDs, msg = msg };
        }

        /// <summary>
        /// 新增保养清单设定
        /// SAM 2017年7月5日14:37:11
        /// </summary>
        /// <param name="Token"></param>
        /// <param name="jArray"></param>
        /// <returns></returns>
        public static object Ems00008insert(string Token, JArray jArray)
        {
            int success = 0;
            int fail = 0;
            string failIDs = null;
            string msg = null;
            string userid = UtilBussinessService.detoken(Token);
            JObject data = null;
            EMS_EquipmentMaintenanceList model = null;
            for (int i = 0; i < jArray.Count; i++)
            {
                data = (JObject)jArray[i];
                try
                {
                    if (EMS_EquipmentMaintenanceListService.CheckCode(data.Value<string>("Code"), null))
                    {
                        failIDs = UtilBussinessService.str(failIDs, data.Value<string>("EquipmentMaintenanceListID"));
                        msg = UtilBussinessService.str(msg, data.Value<string>("Code") + "已存在");
                        fail++;
                        continue;
                    }

                    model = new EMS_EquipmentMaintenanceList();
                    model.EquipmentMaintenanceListID = UniversalService.GetSerialNumber("EMS_EquipmentMaintenanceList");
                    model.Code = data.Value<string>("Code");  //代号
                    model.Name = data.Value<string>("Name");//说明
                    model.Comments = data.Value<string>("Comments");
                    model.Type = data.Value<string>("Type");//类型
                    model.Status = Framework.SystemID + "0201213000001"; //状态       
                    if (EMS_EquipmentMaintenanceListService.insert(userid, model))
                        success++;
                    else
                    {
                        failIDs = UtilBussinessService.str(failIDs, data.Value<string>("EquipmentMaintenanceListID"));
                        fail++;
                    }
                }
                catch (Exception ex)
                {
                    DataLogerService.writeerrlog(ex);
                    failIDs = UtilBussinessService.str(failIDs, data.Value<string>("EquipmentMaintenanceListID"));
                    fail++;
                }

            }
            return new { success = success, fail = fail, failIDs = failIDs, msg = msg };
        }

        /// <summary>
        /// 更新保养清单设定
        /// SAM 2017年7月5日14:37:37
        /// </summary>
        /// <param name="Token"></param>
        /// <param name="jArray"></param>
        /// <returns></returns>
        public static object Ems00008update(string Token, JArray jArray)
        {
            int success = 0;
            int fail = 0;
            string failIDs = null;
            string msg = null;
            string userid = UtilBussinessService.detoken(Token);
            JObject data = null;
            EMS_EquipmentMaintenanceList model = null;
            for (int i = 0; i < jArray.Count; i++)
            {
                data = (JObject)jArray[i];
                model = EMS_EquipmentMaintenanceListService.get(data.Value<string>("EquipmentMaintenanceListID"));
                model.Name = data.Value<string>("Name");//说明 
                if (EMS_EquipmentMaintenanceListService.update(userid, model))
                    success++;
                else
                {
                    failIDs = UtilBussinessService.str(failIDs, data.Value<string>("EquipmentMaintenanceListID"));
                    fail++;
                }

            }
            return new { success = success, fail = fail, failIDs = failIDs, msg = msg };
        }


        /// <summary>
        /// 设备设定列表
        /// SAM 2017年7月5日15:18:33
        /// </summary>
        /// <param name="token"></param>
        /// <param name="EquipmentMaintenanceListID"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <returns></returns>
        public static object Ems00008GetDeviceSettingList(string token, string EquipmentMaintenanceListID, string Code, string Name, int page, int rows)
        {
            int count = 0;
            IList<Hashtable> result = EMS_EquipmentMaintenanceListDetailsService.Ems00008GetDeviceSettingList(EquipmentMaintenanceListID, Code, Name, page, rows, ref count);
            return UtilBussinessService.getPaginationModel(result, count);
        }

        /// <summary>
        ///  保養清單:設備開窗-已選擇資料
        ///  SAM 2017年7月14日15:22:57
        /// </summary>
        /// <param name="token"></param>
        /// <param name="EquipmentMaintenanceListID"></param>
        /// <param name="Code"></param>
        /// <returns></returns>
        public static object Ems00008DeviceSettingList(string token, string EquipmentMaintenanceListID, string Code, string Name)
        {
            IList<Hashtable> result = EMS_EquipmentMaintenanceListDetailsService.Ems00008DeviceSettingList(EquipmentMaintenanceListID, Code, Name);
            return result;
        }



        /// <summary>
        ///  保養清單:設備開窗-未選擇資料
        ///  SAM 2017年7月14日15:03:42
        /// </summary>
        /// <param name="token"></param>
        /// <param name="EquipmentMaintenanceListID"></param>
        /// <param name="Code"></param>
        /// <param name="Name"></param>
        /// <returns></returns>
        public static object EMS00008ListDeviceAdd(string token, string EquipmentMaintenanceListID, string Code, string Name)
        {
            IList<Hashtable> result = EMS_EquipmentService.EMS00008ListDeviceAdd(EquipmentMaintenanceListID, Code, Name);
            return result;
        }

        /// <summary>
        /// 保养清单-设备的保存
        /// SAM 2017年7月14日15:31:37
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public static object EMS00008ListDeviceAddSave(JObject request)
        {
            string Token = request.Value<string>("Token");
            string userid = UtilBussinessService.detoken(Token);
            string EquipmentMaintenanceListID = request.Value<string>("EquipmentMaintenanceListID");
            JArray List = request.Value<JArray>("data");

            if (string.IsNullOrWhiteSpace(EquipmentMaintenanceListID))
                return new { status = "410", msg = "EquipmentMaintenanceListID为空!" };

            JObject data = null;
            Hashtable AddModel = null;
            EMS_EquipmentMaintenanceListDetails model = null;
            string New = null;
            List<Hashtable> Add = new List<Hashtable>();
            for (int i = 0; i < List.Count; i++) //将新增的和已存在的分开
            {
                data = (JObject)List[i];
                AddModel = new Hashtable();
                if (string.IsNullOrWhiteSpace(data.Value<string>("EquipmentMaintenanceListDetailID")))
                {
                    AddModel["DetailID"] = data.Value<string>("DetailID");
                    AddModel["EquipmentMaintenanceListID"] = EquipmentMaintenanceListID;
                    Add.Add(AddModel);
                }
                else
                {
                    if (New == null)
                        New = data.Value<string>("EquipmentMaintenanceListDetailID");
                    else
                        New = New + "','" + data.Value<string>("EquipmentMaintenanceListDetailID");
                }
            }
            EMS_EquipmentMaintenanceListDetailsService.Delete(userid, New, EquipmentMaintenanceListID, 2);

            foreach (Hashtable item in Add) //循环添加新增的
            {
                model = new EMS_EquipmentMaintenanceListDetails();
                model.EquipmentMaintenanceListDetailID = UniversalService.GetSerialNumber("EMS_EquipmentMaintenanceListDetails");
                model.EquipmentMaintenanceListID = EquipmentMaintenanceListID;
                model.DetailID = item["DetailID"].ToString();
                model.Type = 2;
                model.Status = Framework.SystemID + "0201213000001";
                EMS_EquipmentMaintenanceListDetailsService.insert(userid, model);
            }

            return new { status = "200", msg = "保存成功！" };
        }



        /// <summary>
        /// 删除明细/设备设定
        /// SAM 2017年7月5日14:36:37
        /// </summary>
        /// <param name="Token"></param>
        /// <param name="jArray"></param>
        /// <returns></returns>
        public static object Ems00008Detaildelete(string Token, JArray jArray)
        {
            int success = 0;
            int fail = 0;
            string failIDs = null;
            string msg = null;
            string userid = UtilBussinessService.detoken(Token);
            JObject data = null;
            EMS_EquipmentMaintenanceListDetails model = null;
            for (int i = 0; i < jArray.Count; i++)
            {
                data = (JObject)jArray[i];
                model = EMS_EquipmentMaintenanceListDetailsService.get(data.Value<string>("EquipmentMaintenanceListDetailID"));
                //if (SYS_ParameterService.USE00001Check(data.Value<string>("ParameterID")))
                //{
                //    failIDs = UtilBussinessService.str(failIDs, data.Value<string>("EquipmentMaintenanceListID"));
                //    msg = UtilBussinessService.str(msg, model.Code + "已使用，不能删除");
                //    fail++;
                //    continue;
                //}
                model.Status = Framework.SystemID + "0201213000003";
                if (EMS_EquipmentMaintenanceListDetailsService.update(userid, model))
                    success++;
                else
                {
                    failIDs = UtilBussinessService.str(failIDs, data.Value<string>("EquipmentMaintenanceListDetailID"));
                    fail++;
                }
            }
            return new { success = success, fail = fail, failIDs = failIDs, msg = msg };
        }

        /// <summary>
        /// 新增明细/设备设定
        /// SAM 2017年7月5日14:37:11
        /// </summary>
        /// <param name="Token"></param>
        /// <param name="jArray"></param>
        /// <returns></returns>
        public static object Ems00008Detailinsert(string Token, JArray jArray, int Type)
        {
            int success = 0;
            int fail = 0;
            string failIDs = null;
            string msg = null;
            string userid = UtilBussinessService.detoken(Token);
            JObject data = null;
            EMS_EquipmentMaintenanceListDetails model = null;
            for (int i = 0; i < jArray.Count; i++)
            {
                data = (JObject)jArray[i];
                if (EMS_EquipmentMaintenanceListDetailsService.CheckDetail(data.Value<string>("DetailID"), Type, data.Value<string>("EquipmentMaintenanceListID"), null))
                {
                    failIDs = UtilBussinessService.str(failIDs, data.Value<string>("EquipmentMaintenanceListDetailID"));
                    msg = UtilBussinessService.str(msg, "资料重复");
                    fail++;
                    continue;
                }

                model = new EMS_EquipmentMaintenanceListDetails();
                model.EquipmentMaintenanceListDetailID = UniversalService.GetSerialNumber("EMS_EquipmentMaintenanceListDetails");
                model.EquipmentMaintenanceListID = data.Value<string>("EquipmentMaintenanceListID");
                model.Sequence = string.IsNullOrWhiteSpace(data.Value<string>("Sequence")) ? 0 : data.Value<int>("Sequence");
                model.Type = Type;
                model.DetailID = data.Value<string>("DetailID");
                model.Comments = data.Value<string>("Comments");
                model.Status = Framework.SystemID + "0201213000001"; //状态       
                if (EMS_EquipmentMaintenanceListDetailsService.insert(userid, model))
                    success++;
                else
                {
                    failIDs = UtilBussinessService.str(failIDs, data.Value<string>("EquipmentMaintenanceListDetailID"));
                    fail++;
                }

            }
            return new { success = success, fail = fail, failIDs = failIDs, msg = msg };
        }

        /// <summary>
        /// 获取清单明细列表
        /// SAM 2017年7月5日15:39:47
        /// </summary>
        /// <param name="token"></param>
        /// <param name="EquipmentMaintenanceListID"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <returns></returns>
        public static object Ems00008GetDetailList(string token, string EquipmentMaintenanceListID, int page, int rows)
        {
            int count = 0;
            IList<Hashtable> result = EMS_EquipmentMaintenanceListDetailsService.Ems00008GetDetailList(EquipmentMaintenanceListID, page, rows, ref count);
            return UtilBussinessService.getPaginationModel(result, count);
        }

        /// <summary>
        /// 获取清单明细列表（不分页）
        /// SAM 2017年7月14日11:43:31
        /// </summary>
        /// <param name="token"></param>
        /// <param name="EquipmentMaintenanceListID"></param>
        /// <returns></returns>
        public static object Ems00008DetailList(string token, string EquipmentMaintenanceListID)
        {
            IList<Hashtable> result = EMS_EquipmentMaintenanceListDetailsService.Ems00008DetailList(EquipmentMaintenanceListID);
            return result;
        }

        /// <summary>
        /// 获取指定清单未设定的保养项目列表
        /// SAM 2017年7月14日11:24:36
        /// </summary>
        /// <param name="token"></param>
        /// <param name="EquipmentMaintenanceListID"></param>
        /// <returns></returns>
        public static object EMS00008ListDetailAdd(string token, string EquipmentMaintenanceListID, string Code, string Name)
        {
            IList<Hashtable> result = SYS_ParameterService.EMS00008ListDetailAdd(EquipmentMaintenanceListID, Code, Name);
            return result;
        }

        /// <summary>
        /// 清单明细保存
        /// SAM 2017年7月14日14:56:55
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public static object EMS00008ListDetailSave(JObject request)
        {
            string Token = request.Value<string>("Token");
            string userid = UtilBussinessService.detoken(Token);
            string EquipmentMaintenanceListID = request.Value<string>("EquipmentMaintenanceListID");
            JArray List = request.Value<JArray>("data");

            if (string.IsNullOrWhiteSpace(EquipmentMaintenanceListID))
                return new { status = "410", msg = "EquipmentMaintenanceListID为空!" };

            JObject data = null;
            Hashtable AddModel = null;
            EMS_EquipmentMaintenanceListDetails model = null;
            string New = null;
            List<Hashtable> Add = new List<Hashtable>();
            for (int i = 0; i < List.Count; i++) //将新增的和已存在的分开
            {
                data = (JObject)List[i];
                AddModel = new Hashtable();
                if (string.IsNullOrWhiteSpace(data.Value<string>("EquipmentMaintenanceListDetailID")))
                {
                    AddModel["DetailID"] = data.Value<string>("DetailID");
                    AddModel["EquipmentMaintenanceListID"] = EquipmentMaintenanceListID;
                    Add.Add(AddModel);
                }
                else
                {
                    if (New == null)
                        New = data.Value<string>("EquipmentMaintenanceListDetailID");
                    else
                        New = New + "','" + data.Value<string>("EquipmentMaintenanceListDetailID");
                }
            }
            EMS_EquipmentMaintenanceListDetailsService.Delete(userid, New, EquipmentMaintenanceListID, 1);

            foreach (Hashtable item in Add) //循环添加新增的
            {
                model = new EMS_EquipmentMaintenanceListDetails();
                model.EquipmentMaintenanceListDetailID = UniversalService.GetSerialNumber("EMS_EquipmentMaintenanceListDetails");
                model.EquipmentMaintenanceListID = EquipmentMaintenanceListID;
                model.DetailID = item["DetailID"].ToString();
                model.Type = 1;
                model.Status = Framework.SystemID + "0201213000001";
                EMS_EquipmentMaintenanceListDetailsService.insert(userid, model);
            }

            return new { status = "200", msg = "保存成功！" };
        }

        /// <summary>
        /// 保養類型删除
        /// SAM 2017年7月19日11:28:19
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public static object Ems00008TypeDelete(JObject request)
        {
            try
            {
                string Token = request.Value<string>("Token");
                string userID = UniversalService.detoken(Token);

                SYS_Parameters model = SYS_ParameterService.get(request.Value<string>("ParameterID"));
                if (model == null)
                    return new { status = "410", msg = "不存在保养类型信息！" };

                if (EMS_EquipmentMaintenanceListService.CheckType(model.ParameterID))
                    return new { status = "410", msg = model.Code + "已使用，不能删除！" };

                model.IsEnable = 2;

                if (SYS_ParameterService.update(userID, model))
                    return new { status = "200", msg = "删除成功" };
                else
                    return new { status = "410", msg = "删除失败" };
            }
            catch (Exception ex)
            {
                DataLogerService.writeerrlog(ex);
                return new { status = "410", msg = ex.Message };
            }
        }

        /// <summary>
        /// 保養項目删除
        /// SAM 2017年7月19日11:23:18
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public static object Ems00008ProjectDelete(JObject request)
        {
            try
            {
                string Token = request.Value<string>("Token");
                string userID = UniversalService.detoken(Token);

                SYS_Parameters model = SYS_ParameterService.get(request.Value<string>("ParameterID"));
                if (model == null)
                    return new { status = "410", msg = "不存在保养项目信息！" };

                if (EMS_EquipmentMaintenanceListDetailsService.CheckDetail(model.ParameterID))
                    return new { status = "410", msg = model.Code + "已使用，不能删除！" };

                if (EMS_MaiOrderProjectService.CheckMaiProject(model.ParameterID))
                    return new { status = "410", msg = model.Code + "已使用，不能删除！" };



                model.IsEnable = 2;

                if (SYS_ParameterService.update(userID, model))
                    return new { status = "200", msg = "删除成功" };
                else
                    return new { status = "410", msg = "删除失败" };
            }
            catch (Exception ex)
            {
                DataLogerService.writeerrlog(ex);
                return new { status = "410", msg = ex.Message };
            }
        }

        /// <summary>
        /// 保养清单删除
        /// SAM 2017年7月19日11:28:03
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public static object Ems00008Delete(JObject request)
        {
            try
            {
                string Token = request.Value<string>("Token");
                string userID = UniversalService.detoken(Token);

                EMS_EquipmentMaintenanceList model = EMS_EquipmentMaintenanceListService.get(request.Value<string>("EquipmentMaintenanceListID"));
                if (model == null)
                    return new { status = "410", msg = "不存在清单信息！" };

                if (EMS_MaintenanceOrderService.CheckList(model.EquipmentMaintenanceListID))
                    return new { status = "410", msg = model.Code + "清单代号已使用，不能删除！" };

                model.Status = Framework.SystemID + "0201213000003";

                if (EMS_EquipmentMaintenanceListService.update(userID, model))
                    return new { status = "200", msg = "删除成功" };
                else
                    return new { status = "410", msg = "删除失败" };
            }
            catch (Exception ex)
            {
                DataLogerService.writeerrlog(ex);
                return new { status = "410", msg = ex.Message };
            }
        }


        #endregion

        #region EMS00009设备保养工单维护
        /// <summary>
        /// 获取保养单主列表
        /// SAM 2017年7月9日10:15:49
        /// </summary>
        /// <param name="token"></param>
        /// <param name="type"></param>
        /// <param name="status"></param>
        /// <param name="equipmentID"></param>
        /// <param name="userID"></param>
        /// <param name="Code"></param>
        /// <param name="Date"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <returns></returns>
        public static object Ems00009GetList(string token, string type, string status, string equipmentID, string userID, string StartCode, string EndCode, string StartDate, string EndDate, int page, int rows)
        {
            int count = 0;
            IList<Hashtable> result = EMS_MaintenanceOrderService.Ems00009GetList(type, status, equipmentID, userID, StartCode, EndCode, StartDate, EndDate, page, rows, ref count);
            return UtilBussinessService.getPaginationModel(result, count);
        }

        /// <summary>
        /// 保养工单的新增
        /// SAM 2017年7月9日10:45:02
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public static object Ems00009Add(JObject request)
        {
            string Token = request.Value<string>("Token");
            string userid = UtilBussinessService.detoken(Token);

            EMS_MaintenanceOrder model = new EMS_MaintenanceOrder();
            model.MaintenanceOrderID = UniversalService.GetSerialNumber("EMS_MaintenanceOrder");
            model.MaintenanceNo = request.Value<string>("MaintenanceNo");
            model.Date = request.Value<DateTime>("Date");
            model.MaintenanceDate = request.Value<DateTime>("MaintenanceDate");
            model.Type = request.Value<string>("Type");
            model.EquipmentMaintenanceListID = request.Value<string>("EquipmentMaintenanceListID");
            model.OrganizationID = request.Value<string>("OrganizationID");
            model.ManufacturerID = request.Value<string>("ManufacturerID");
            model.MESUserID = request.Value<string>("MESUserID");
            model.Status = Framework.SystemID + "0201213000028";
            model.Comments = request.Value<string>("Comments");

            //while (EMS_MaintenanceOrderService.CheckCode(model.MaintenanceNo, null))
            //{
            //    model.MaintenanceNo = UtilBussinessService.GetAutoNumber(userid, "EMSB");
            //}

            if (EMS_MaintenanceOrderService.insert(userid, model))
            {
                //UtilBussinessService.updateAutoNumber(userid, "EMSB", model.MaintenanceNo);
                UtilBussinessService.UpdateDocumentAutoNumber(userid, request.Value<string>("DocumentAutoNumberID"));
                //保存保养单设备
                string EquipmentIDs = request.Value<string>("EquipmentIDs");
                EMS_MaiOrderEquipment Equmodel = null;
                if (String.IsNullOrWhiteSpace(EquipmentIDs))
                    return new { status = "200", msg = "新增成功！" };

                string[] EquipmentList = EquipmentIDs.Split(',');
                int Sequence = 1;
                foreach (string EquipmentID in EquipmentList)
                {
                    //首先，保存保养单与设备的映射
                    Equmodel = new EMS_MaiOrderEquipment();
                    Equmodel.MaiOrderEquipmentID = UniversalService.GetSerialNumber("EMS_MaiOrderEquipment");
                    Equmodel.MaintenanceOrderID = model.MaintenanceOrderID;
                    Equmodel.Sequence = Sequence;
                    Equmodel.EquipmentID = EquipmentID;
                    Equmodel.Status = Framework.SystemID + "0201213000028";
                    if (EMS_MaiOrderEquipmentService.insert(userid, Equmodel))
                    {
                        //然后保养清单ID获取项目设定
                        IList<Hashtable> ProjectList = EMS_EquipmentMaintenanceListDetailsService.Ems00009GetDetailList(model.EquipmentMaintenanceListID);
                        //最后新增项目
                        EMS_MaiOrderProject Promodel = null;
                        int ProSequence = 1;
                        foreach (Hashtable Project in ProjectList)
                        {
                            Promodel = new EMS_MaiOrderProject();
                            Promodel.MaiOrderProjectID = UniversalService.GetSerialNumber("EMS_MaiOrderProject");
                            Promodel.MaiOrderEquipmentID = Equmodel.MaiOrderEquipmentID;
                            Promodel.MaintenanceOrderID = model.MaintenanceOrderID;
                            Promodel.Sequence = ProSequence;
                            Promodel.MaiProjectID = Project["DetailID"].ToString();
                            Promodel.Attribute = Project["Attribute"].ToString();
                            Promodel.Status = Framework.SystemID + "0201213000001";
                            EMS_MaiOrderProjectService.insert(userid, Promodel);
                            ProSequence++;
                        }
                    }
                    Sequence++;
                }
                return new { status = "200", msg = "新增成功！" };
            }
            else
                return new { status = "410", msg = "新增失败！" };
        }

        /// <summary>
        /// 根据保养类型获取保养清单
        /// SAM 2017年7月9日16:24:15
        /// </summary>
        /// <param name="token"></param>
        /// <param name="Type"></param>
        /// <returns></returns>
        public static object Ems00009GetEquMaiList(string token, string Type)
        {
            IList<Hashtable> result = EMS_EquipmentMaintenanceListService.Ems00009GetEquMaiList(Type);
            return result;
        }

        /// <summary>
        /// 根据保养清单获取设备设定列表
        /// SAM 2017年7月9日16:28:24
        /// </summary>
        /// <param name="token"></param>
        /// <param name="EquipmentMaintenanceListID"></param>
        /// <returns></returns>
        public static object Ems00009GetEquMaiDetailList(string token, string EquipmentMaintenanceListID)
        {
            IList<Hashtable> result = EMS_EquipmentMaintenanceListDetailsService.Ems00009GetEquMaiDetailList(EquipmentMaintenanceListID);
            return result;
        }

        /// <summary>
        /// 据保养清单获取设备设定列表，同时根据保养工单将保养工单已存在的点亮
        /// SAM 2017年7月31日23:28:16
        /// </summary>
        /// <param name="token"></param>
        /// <param name="MaintenanceOrderID"></param>
        /// <param name="EquipmentMaintenanceListID"></param>
        /// <returns></returns>
        public static object Ems00009GetEquDetailList(string token, string MaintenanceOrderID, string EquipmentMaintenanceListID)
        {
            IList<Hashtable> result = EMS_EquipmentMaintenanceListDetailsService.Ems00009GetEquDetailList(MaintenanceOrderID, EquipmentMaintenanceListID);
            return result;
        }

        /// <summary>
        /// 保养工单的更新
        /// SAM 2017年7月9日11:29:17
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public static object Ems00009Update(JObject request)
        {
            string Token = request.Value<string>("Token");
            string userid = UtilBussinessService.detoken(Token);

            EMS_MaintenanceOrder model = EMS_MaintenanceOrderService.get(request.Value<string>("MaintenanceOrderID"));
            if (model == null)
                return new { status = "410", msg = "流水号错误！" };

            model.MaintenanceDate = request.Value<DateTime>("MaintenanceDate");
            model.ManufacturerID = request.Value<string>("ManufacturerID");
            model.MESUserID = request.Value<string>("MESUserID");
            model.Comments = request.Value<string>("Comments");

            if (EMS_MaintenanceOrderService.update(userid, model))
            {
                string EquipmentIDs = request.Value<string>("EquipmentIDs");
                EMS_MaiOrderEquipment Equmodel = null;
                if (string.IsNullOrWhiteSpace(EquipmentIDs))
                {
                    //将工单和设备的映射都删除
                    EMS_MaiOrderEquipmentService.DeleteByOrder(userid, model.MaintenanceOrderID, null);
                }
                else
                {
                    //将不在范围内的设备设定都删除
                    EMS_MaiOrderEquipmentService.DeleteByOrder(userid, model.MaintenanceOrderID, EquipmentIDs.Replace(",", "','"));

                    string[] EquipmentList = EquipmentIDs.Split(',');
                    int Sequence = 1;
                    foreach (string EquipmentID in EquipmentList)
                    {
                        if (!EMS_MaiOrderEquipmentService.CheckEquipment(EquipmentID, model.MaintenanceOrderID))
                        {
                            //首先，保存保养单与设备的映射
                            Equmodel = new EMS_MaiOrderEquipment();
                            Equmodel.MaiOrderEquipmentID = UniversalService.GetSerialNumber("EMS_MaiOrderEquipment");
                            Equmodel.MaintenanceOrderID = model.MaintenanceOrderID;
                            Equmodel.Sequence = Sequence;
                            Equmodel.EquipmentID = EquipmentID;
                            Equmodel.Status = Framework.SystemID + "0201213000028";
                            if (EMS_MaiOrderEquipmentService.insert(userid, Equmodel))
                            {
                                //然后保养清单ID获取项目设定
                                IList<Hashtable> ProjectList = EMS_EquipmentMaintenanceListDetailsService.Ems00009GetDetailList(model.EquipmentMaintenanceListID);
                                //最后新增项目
                                EMS_MaiOrderProject Promodel = null;
                                int ProSequence = 1;
                                foreach (Hashtable Project in ProjectList)
                                {
                                    Promodel = new EMS_MaiOrderProject();
                                    Promodel.MaiOrderProjectID = UniversalService.GetSerialNumber("EMS_MaiOrderProject");
                                    Promodel.MaiOrderEquipmentID = Equmodel.MaiOrderEquipmentID;
                                    Promodel.MaintenanceOrderID = model.MaintenanceOrderID;
                                    Promodel.Sequence = ProSequence;
                                    Promodel.MaiProjectID = Project["DetailID"].ToString();
                                    Promodel.Attribute = Project["Attribute"].ToString();
                                    Promodel.Status = Framework.SystemID + "0201213000001";
                                    EMS_MaiOrderProjectService.insert(userid, Promodel);
                                    ProSequence++;
                                }
                            }
                            Sequence++;
                        }
                    }
                }
                return new { status = "200", msg = "更新成功！" };
            }
            else
                return new { status = "410", msg = "更新失败！" };
        }

        /// <summary>
        /// 保养工单的删除
        /// SAM 2017年7月9日11:29:37
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public static object Ems00009Delete(JObject request)
        {
            string Token = request.Value<string>("Token");
            string userid = UtilBussinessService.detoken(Token);

            EMS_MaintenanceOrder model = EMS_MaintenanceOrderService.get(request.Value<string>("MaintenanceOrderID"));
            if (model == null)
                return new { status = "410", msg = "流水号错误！" };

            if (model.Status == Framework.SystemID + "0201213000029" || model.Status == Framework.SystemID + "020121300002A")
                return new { status = "410", msg = "工单处于核发或者结案状态！" };

            model.Status = Framework.SystemID + "0201213000003";
            if (EMS_MaintenanceOrderService.update(userid, model))
                return new { status = "200", msg = "删除成功！" };
            else
                return new { status = "410", msg = "删除失败！" };
        }

        /// <summary>
        /// 保养工单核发
        /// SAM 2017年7月9日16:34:00
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public static object Ems00009OP(JObject request)
        {
            string Token = request.Value<string>("Token");
            string userid = UtilBussinessService.detoken(Token);

            EMS_MaintenanceOrder model = EMS_MaintenanceOrderService.get(request.Value<string>("MaintenanceOrderID"));
            if (model == null)
                return new { status = "410", msg = "流水号错误！" };

            if (model.Status != Framework.SystemID + "0201213000028")
                return new { status = "410", msg = "工单处于非立单状态！" };

            model.Status = Framework.SystemID + "0201213000029";
            if (EMS_MaintenanceOrderService.update(userid, model))
            {
                //同步更新他的设备明细
                EMS_MaiOrderEquipmentService.updateStatus(userid, model.MaintenanceOrderID, model.Status);
                return new { status = "200", msg = "核发成功！" };
            }
            else
                return new { status = "410", msg = "核发失败！" };
        }

        /// <summary>
        /// 保养工单作废
        /// SAM 2017年7月25日15:27:08
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public static object Ems00009CA(JObject request)
        {
            string Token = request.Value<string>("Token");
            string userid = UtilBussinessService.detoken(Token);

            EMS_MaintenanceOrder model = EMS_MaintenanceOrderService.get(request.Value<string>("MaintenanceOrderID"));
            if (model == null)
                return new { status = "410", msg = "流水号错误！" };

            if (model.Status != Framework.SystemID + "0201213000028")
                return new { status = "410", msg = "工单处于非立单状态！" };

            model.Status = Framework.SystemID + "020121300002B";
            if (EMS_MaintenanceOrderService.update(userid, model))
            {
                //同步更新他的设备明细
                EMS_MaiOrderEquipmentService.updateStatus(userid, model.MaintenanceOrderID, model.Status);
                return new { status = "200", msg = "作废成功！" };
            }
            else
                return new { status = "410", msg = "作废失败！" };
        }

        /// <summary>
        /// 根据保养工单获取他的设备保养明细列表
        /// SAM 2017年7月9日15:46:43
        /// </summary>
        /// <param name="token"></param>
        /// <param name="maintenanceOrderID"></param>
        /// <param name="EquipmentID"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <returns></returns>
        public static object Ems00009GetDetailList(string token, string maintenanceOrderID, string EquipmentID, int page, int rows)
        {
            int count = 0;
            IList<Hashtable> result = EMS_MaiOrderEquipmentService.Ems00009GetDetailList(maintenanceOrderID, EquipmentID, page, rows, ref count);
            return UtilBussinessService.getPaginationModel(result, count);
        }




        /// <summary>
        /// 保养工单设备明细的新增
        /// SAM 2017年7月9日15:55:27
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public static object Ems00009DetailAdd(JObject request)
        {
            string Token = request.Value<string>("Token");
            string userid = UtilBussinessService.detoken(Token);

            EMS_MaintenanceOrder model = EMS_MaintenanceOrderService.get(request.Value<string>("MaintenanceOrderID"));
            if (model == null)
                return new { status = "410", msg = "表头流水号错误！" };

            string EquipmentIDs = request.Value<string>("EquipmentIDs");
            EMS_MaiOrderEquipment Equmodel = null;
            if (String.IsNullOrWhiteSpace(EquipmentIDs))
                return new { status = "410", msg = "并没有东西需要新增！" };

            string[] EquipmentList = EquipmentIDs.Split(',');
            int Sequence = 1;
            foreach (string EquipmentID in EquipmentList)
            {
                //首先，保存保养单与设备的映射
                Equmodel = new EMS_MaiOrderEquipment();
                Equmodel.MaiOrderEquipmentID = UniversalService.GetSerialNumber("EMS_MaiOrderEquipment");
                Equmodel.MaintenanceOrderID = model.MaintenanceOrderID;
                Equmodel.Sequence = Sequence;
                Equmodel.EquipmentID = EquipmentID;
                Equmodel.Status = Framework.SystemID + "0201213000028";
                if (EMS_MaiOrderEquipmentService.insert(userid, Equmodel))
                {
                    //然后保养清单ID获取项目设定
                    IList<Hashtable> ProjectList = EMS_EquipmentMaintenanceListDetailsService.Ems00009GetDetailList(model.EquipmentMaintenanceListID);
                    //最后新增项目
                    EMS_MaiOrderProject Promodel = null;
                    int ProSequence = 1;
                    foreach (Hashtable Project in ProjectList)
                    {
                        Promodel = new EMS_MaiOrderProject();
                        Promodel.MaiOrderProjectID = UniversalService.GetSerialNumber("EMS_MaiOrderProject");
                        Promodel.MaiOrderEquipmentID = Equmodel.MaiOrderEquipmentID;
                        Promodel.MaintenanceOrderID = model.MaintenanceOrderID;
                        Promodel.Sequence = ProSequence;
                        Promodel.MaiProjectID = Project["DetailID"].ToString();
                        Promodel.Attribute = Project["Attribute"].ToString();
                        Promodel.Status = Framework.SystemID + "0201213000001";
                        EMS_MaiOrderProjectService.insert(userid, Promodel);
                        ProSequence++;
                    }
                }
                Sequence++;
            }
            return new { status = "200", msg = "新增成功！" };
        }

        /// <summary>
        /// 保养工单设备明细的删除
        /// SAM 2017年7月9日15:58:36
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public static object Ems00009DetailDelete(JObject request)
        {
            string Token = request.Value<string>("Token");
            string userid = UtilBussinessService.detoken(Token);

            EMS_MaiOrderEquipment model = EMS_MaiOrderEquipmentService.get(request.Value<string>("MaiOrderEquipmentID"));
            if (model == null)
                return new { status = "410", msg = "流水号错误！" };

            if (model.Status == Framework.SystemID + "0201213000029" || model.Status == Framework.SystemID + "020121300002B")
                return new { status = "410", msg = "工单处于核发或者结案状态！" };

            model.Status = Framework.SystemID + "0201213000003";
            if (EMS_MaiOrderEquipmentService.update(userid, model))
                return new { status = "200", msg = "删除成功！" };
            else
                return new { status = "410", msg = "删除失败！" };
        }


        /// <summary>
        /// 根据保养工单设备获取保养项目列表
        /// SAM 2017年7月9日16:00:33
        /// </summary>
        /// <param name="token"></param>
        /// <param name="maiOrderEquipmentID"></param>
        /// <param name="code"></param>
        /// <param name="name"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <returns></returns>
        public static object Ems00009GetProjectList(string token, string maiOrderEquipmentID, string code, string name, int page, int rows)
        {
            int count = 0;
            IList<Hashtable> result = EMS_MaiOrderProjectService.Ems00009GetProjectList(maiOrderEquipmentID, code, name, page, rows, ref count);
            return UtilBussinessService.getPaginationModel(result, count);
        }

        /// <summary>
        /// 保养项目新增
        /// SAM 2017年7月9日16:06:46
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public static object Ems00009ProjectAdd(JObject request)
        {
            string Token = request.Value<string>("Token");
            string userid = UtilBussinessService.detoken(Token);

            EMS_MaiOrderEquipment model = EMS_MaiOrderEquipmentService.get(request.Value<string>("MaiOrderEquipmentID"));
            if (model == null)
                return new { status = "410", msg = "明细流水号错误！" };

            string Projects = request.Value<string>("Projects");
            if (string.IsNullOrWhiteSpace(Projects))
                return new { status = "410", msg = "并没有东西需要新增！" };

            string[] ProjectList = Projects.Split(',');
            int Sequence = 1;
            EMS_MaiOrderProject Promodel = null;
            SYS_Parameters Parmodel = null;
            foreach (string MaiProjectID in ProjectList)
            {
                Parmodel = SYS_ParameterService.get(MaiProjectID);
                if (Parmodel != null)
                {
                    Promodel = new EMS_MaiOrderProject();
                    Promodel.MaiOrderProjectID = UniversalService.GetSerialNumber("EMS_MaiOrderProject");
                    Promodel.MaiOrderEquipmentID = model.MaiOrderEquipmentID;
                    Promodel.MaintenanceOrderID = model.MaintenanceOrderID;
                    Promodel.Sequence = Sequence;
                    Promodel.MaiProjectID = MaiProjectID;
                    Promodel.Attribute = Parmodel.Description;
                    Promodel.Comments = Parmodel.Comments;
                    Promodel.Status = Framework.SystemID + "0201213000001";
                    EMS_MaiOrderProjectService.insert(userid, Promodel);
                    Sequence++;
                }
            }
            EMS_MaiOrderEquipment MEmodel = EMS_MaiOrderEquipmentService.get(model.MaiOrderEquipmentID);
            if (MEmodel != null)
                EMS_MaiOrderEquipmentService.update(userid, MEmodel);

            return new { status = "200", msg = "新增成功！" };
        }

        /// <summary>
        /// 保养项目删除
        /// SAM 2017年7月9日16:13:32
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public static object Ems00009ProjectDelete(JObject request)
        {
            string Token = request.Value<string>("Token");
            string userid = UtilBussinessService.detoken(Token);

            EMS_MaiOrderProject model = EMS_MaiOrderProjectService.get(request.Value<string>("MaiOrderProjectID"));
            if (model == null)
                return new { status = "410", msg = "流水号错误！" };

            EMS_MaiOrderEquipment Equmodel = EMS_MaiOrderEquipmentService.get(model.MaiOrderEquipmentID);

            if (Equmodel.Status == Framework.SystemID + "0201213000029" || Equmodel.Status == Framework.SystemID + "020121300002B")
                return new { status = "410", msg = "工单处于核发或者结案状态！" };

            model.Status = Framework.SystemID + "0201213000003";
            if (EMS_MaiOrderProjectService.update(userid, model))
            {
                EMS_MaiOrderEquipment MEmodel = EMS_MaiOrderEquipmentService.get(model.MaiOrderEquipmentID);
                if (MEmodel != null)
                    EMS_MaiOrderEquipmentService.update(userid, MEmodel);
                return new { status = "200", msg = "删除成功！" };
            }
            else
                return new { status = "410", msg = "删除失败！" };
        }


        #endregion

        #region EMS00010設備保養資料維護
        /// <summary>
        ///  設備保養資料主列表（保养单与明细一起显示，以明细为主）
        ///  SAM 2017年7月9日11:46:29
        /// </summary>
        /// <param name="token"></param>
        /// <param name="type"></param>
        /// <param name="status"></param>
        /// <param name="equipmentID"></param>
        /// <param name="userID"></param>
        /// <param name="StartCode"></param>
        /// <param name="EndCode"></param>
        /// <param name="StartDate"></param>
        /// <param name="EndDate"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <returns></returns>
        public static object Ems00010GetList(string token, string TypeCode, string status, string EquipmentCode, string UserCode, string StartCode, string EndCode, string StartDate, string EndDate, int page, int rows)
        {
            int count = 0;
            IList<Hashtable> result = EMS_MaintenanceOrderService.Ems00010GetList(TypeCode, status, EquipmentCode, UserCode, StartCode, EndCode, StartDate, EndDate, page, rows, ref count);
            return UtilBussinessService.getPaginationModel(result, count);
        }


        /// <summary>
        /// 更新保养项目
        /// SAM 2017年7月9日17:17:373
        /// </summary>
        /// <param name="Token">授权码</param>
        /// <param name="jArray"></param>
        /// <returns></returns>
        public static object Ems00010Projectupdate(string Token, JArray jArray)
        {
            int success = 0;
            int fail = 0;
            string failIDs = null;
            string msg = null;
            string userid = UtilBussinessService.detoken(Token);
            JObject data = null;
            EMS_MaiOrderProject model = null;
            for (int i = 0; i < jArray.Count; i++)
            {
                data = (JObject)jArray[i];
                model = EMS_MaiOrderProjectService.get(data.Value<string>("MaiOrderProjectID"));
                if (model == null)
                {
                    failIDs = UtilBussinessService.str(failIDs, data.Value<string>("MaiOrderProjectID"));
                    msg = UtilBussinessService.str(msg, data.Value<string>("Code") + "流水号为空");
                    fail++;
                    continue;
                }
                model.AttributeValue = data.Value<string>("AttributeValue");
                model.Comments = data.Value<string>("Comments");
                if (EMS_MaiOrderProjectService.update(userid, model))
                    success++;
                else
                {
                    failIDs = UtilBussinessService.str(failIDs, data.Value<string>("MaiOrderProjectID"));
                    msg = UtilBussinessService.str(msg, "更新失败，请联系开发人员！");
                    fail++;
                }


            }
            return new { success = success, fail = fail, failIDs = failIDs, msg = msg };
        }

        /// <summary>
        /// 开始保养
        /// SAM 2017年7月9日17:21:34
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public static object Ems00010Start(JObject request)
        {
            string Token = request.Value<string>("Token");
            string userid = UtilBussinessService.detoken(Token);

            EMS_MaiOrderEquipment model = EMS_MaiOrderEquipmentService.get(request.Value<string>("MaiOrderEquipmentID"));
            if (model == null)
                return new { status = "410", msg = "流水号错误！" };

            if (model.Status != Framework.SystemID + "0201213000029")
                return new { status = "410", msg = "工单并非处于核发状态！" };

            EMS_Equipment Equmodel = EMS_EquipmentService.get(model.EquipmentID);

            if (Equmodel.Condition == Framework.SystemID + "0201213000021" || Equmodel.Condition == Framework.SystemID + "0201213000022" || Equmodel.Condition == Framework.SystemID + "0201213000023" || Equmodel.Condition == Framework.SystemID + "0201213000024")
                return new { status = "410", msg = "此設備機況不在待機、停止或關機狀態！" };

            if (string.IsNullOrWhiteSpace(model.StartDate.ToString()))
                model.StartDate = DateTime.Now;

            if (EMS_MaiOrderEquipmentService.update(userid, model))
            {
                Equmodel.Condition = Framework.SystemID + "0201213000024";
                EMS_EquipmentService.update(userid, Equmodel);
                return new { status = "200", msg = "操作成功！" };
            }
            else
                return new { status = "410", msg = "操作失败！" };
        }

        /// <summary>
        /// 结束保养
        /// SAM 2017年7月9日17:21:241
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public static object Ems00010End(JObject request)
        {
            string Token = request.Value<string>("Token");
            string userid = UtilBussinessService.detoken(Token);

            EMS_MaiOrderEquipment model = EMS_MaiOrderEquipmentService.get(request.Value<string>("MaiOrderEquipmentID"));
            if (model == null)
                return new { status = "410", msg = "流水号错误！" };

            if (model.Status != Framework.SystemID + "0201213000029")
                return new { status = "410", msg = "工单并非处于核发状态！" };

            EMS_Equipment Equmodel = EMS_EquipmentService.get(model.EquipmentID);

            if (Equmodel.Condition != Framework.SystemID + "0201213000024")
                return new { status = "410", msg = "此設備機況不在保养狀態！" };

            model.EndDate = DateTime.Now;
            if (EMS_MaiOrderEquipmentService.update(userid, model))
            {
                Equmodel.Condition = Framework.SystemID + "0201213000025";
                EMS_EquipmentService.update(userid, Equmodel);
                return new { status = "200", msg = "操作成功！" };
            }
            else
                return new { status = "410", msg = "操作失败！" };
        }
        #endregion

        #region EMS00011設備保養結案與還原
        /// <summary>
        /// 設備保養結案與還原-主列表
        ///  SAM 2017年7月9日11:46:29
        /// </summary>
        /// <param name="token"></param>
        /// <param name="type"></param>
        /// <param name="status"></param>
        /// <param name="equipmentID"></param>
        /// <param name="userID"></param>
        /// <param name="StartCode"></param>
        /// <param name="EndCode"></param>
        /// <param name="StartDate"></param>
        /// <param name="EndDate"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <returns></returns>
        public static object Ems00011GetList(string token, string type, string status, string equipmentID, string userID, string StartCode, string EndCode, string StartDate, string EndDate, int page, int rows)
        {
            int count = 0;
            IList<Hashtable> result = EMS_MaintenanceOrderService.Ems00011GetList(type, status, equipmentID, userID, StartCode, EndCode, StartDate, EndDate, page, rows, ref count);
            //保养类型、保养清单、保养部门、保养厂商、备注、设备说明、保管部门
            foreach (Hashtable item in result)
            {
                string Type = SYS_LanguageLibService.GetLan(item["Type"].ToString(), "Name", 20);
                if (!string.IsNullOrWhiteSpace(Type))
                    item["TypeName"] = Type;

                string EquMaiListCode = SYS_LanguageLibService.GetLan(item["EquipmentMaintenanceListID"].ToString(), "Code", 105);
                if (!string.IsNullOrWhiteSpace(EquMaiListCode))
                    item["EquMaiListCode"] = EquMaiListCode;

                string OrganizationCode = SYS_LanguageLibService.GetLan(item["OrganizationID"].ToString(), "Code", 5);
                if (!string.IsNullOrWhiteSpace(OrganizationCode))
                {
                    item["OrganizationCode"] = OrganizationCode;
                    item["OrganizationName"] = SYS_LanguageLibService.GetLan(item["OrganizationID"].ToString(), "Name", 5);
                }

                string ManufacturerCode = SYS_LanguageLibService.GetLan(item["ManufacturerID"].ToString(), "Code", 32);
                if (!string.IsNullOrWhiteSpace(ManufacturerCode))
                {
                    item["ManufacturerCode"] = ManufacturerCode;
                    item["ManufacturerName"] = SYS_LanguageLibService.GetLan(item["ManufacturerID"].ToString(), "Name", 32);
                }

                string Status = SYS_LanguageLibService.GetLan(item["Status"].ToString(), "Name", 20);
                if (!string.IsNullOrWhiteSpace(Status))
                    item["StatusName"] = Status;
            }
            return UtilBussinessService.getPaginationModel(result, count);
        }

        /// <summary>
        /// 保养单设备明细列表
        /// SAM 2017年7月9日17:37:31
        /// </summary>
        /// <param name="token"></param>
        /// <param name="maintenanceOrderID"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <returns></returns>
        public static object Ems00011GetDetailList(string token, string maintenanceOrderID, int page, int rows)
        {
            int count = 0;
            IList<Hashtable> result = EMS_MaiOrderEquipmentService.Ems00009GetDetailList(maintenanceOrderID, null, page, rows, ref count);
            foreach (Hashtable item in result)
            {
                string EquipmentName = SYS_LanguageLibService.GetLan(item["EquipmentID"].ToString(), "Name", 45);
                if (!string.IsNullOrWhiteSpace(EquipmentName))
                    item["EquipmentName"] = EquipmentName;

                string EquOrganizationCode = SYS_LanguageLibService.GetLan(item["OrganizationID"].ToString(), "Code", 5);
                if (!string.IsNullOrWhiteSpace(EquOrganizationCode))
                {
                    item["EquOrganizationCode"] = EquOrganizationCode;
                    item["EquOrganizationName"] = SYS_LanguageLibService.GetLan(item["OrganizationID"].ToString(), "Name", 5);
                }

                string Status = SYS_LanguageLibService.GetLan(item["Status"].ToString(), "Name", 20);
                if (!string.IsNullOrWhiteSpace(Status))
                    item["StatusName"] = Status;
            }
            return UtilBussinessService.getPaginationModel(result, count);
        }

        /// <summary>
        /// 保养项目列表
        /// SAM 2017年7月9日17:38:08
        /// </summary>
        /// <param name="token"></param>
        /// <param name="maiOrderEquipmentID"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <returns></returns>
        public static object Ems00011GetProjectList(string token, string maiOrderEquipmentID, int page, int rows)
        {
            int count = 0;
            IList<Hashtable> result = EMS_MaiOrderProjectService.Ems00009GetProjectList(maiOrderEquipmentID, null, null, page, rows, ref count);
            return UtilBussinessService.getPaginationModel(result, count);
        }

        /// <summary>
        /// 保养设备结案
        /// SAM 2017年7月9日17:42:56
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public static object Ems00011CloseCase(JObject request)
        {
            string Token = request.Value<string>("Token");
            string userid = UtilBussinessService.detoken(Token);

            EMS_MaiOrderEquipment model = EMS_MaiOrderEquipmentService.get(request.Value<string>("MaiOrderEquipmentID"));
            if (model == null)
                return new { status = "410", msg = "流水号错误！" };

            if (model.Status != Framework.SystemID + "0201213000029")
                return new { status = "410", msg = "工单并非处于核发状态！" };

            model.Status = Framework.SystemID + "020121300002A";
            if (EMS_MaiOrderEquipmentService.update(userid, model))
            {
                EMS_Equipment Equmodel = EMS_EquipmentService.get(model.EquipmentID);
                if (Equmodel.Condition == Framework.SystemID + "0201213000024")
                {
                    Equmodel.Condition = Framework.SystemID + "0201213000025";
                    EMS_EquipmentService.update(userid, Equmodel);
                }
                if (EMS_MaiOrderEquipmentService.CheckStatus(model.Status, model.MaintenanceOrderID))
                {
                    EMS_MaintenanceOrder Mainmodel = EMS_MaintenanceOrderService.get(model.MaintenanceOrderID);
                    Mainmodel.Status = model.Status;
                    EMS_MaintenanceOrderService.update(userid, Mainmodel);
                }
                return new { status = "200", msg = "操作成功！" };
            }
            else
                return new { status = "410", msg = "操作失败！" };
        }

        /// <summary>
        /// 保养设备还原
        /// SAM 2017年7月9日17:45:37
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public static object Ems00011Restore(JObject request)
        {
            string Token = request.Value<string>("Token");
            string userid = UtilBussinessService.detoken(Token);

            EMS_MaiOrderEquipment model = EMS_MaiOrderEquipmentService.get(request.Value<string>("MaiOrderEquipmentID"));
            if (model == null)
                return new { status = "410", msg = "流水号错误！" };

            if (model.Status != Framework.SystemID + "020121300002A")
                return new { status = "410", msg = "工单并非处于结案状态！" };

            model.Status = Framework.SystemID + "0201213000029";
            if (EMS_MaiOrderEquipmentService.update(userid, model))
            {
                EMS_MaintenanceOrder Mainmodel = EMS_MaintenanceOrderService.get(model.MaintenanceOrderID);
                Mainmodel.Status = model.Status;
                EMS_MaintenanceOrderService.update(userid, Mainmodel);
                return new { status = "200", msg = "操作成功！" };
            }
            else
                return new { status = "410", msg = "操作失败！" };
        }
        #endregion
    }
}
