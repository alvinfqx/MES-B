using MonkeyFly.Core;
using MonkeyFly.MES.BasicService;
using MonkeyFly.MES.Models;
using MonkeyFly.MES.ModelServices;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;

namespace MonkeyFly.MES.Services
{
    public class QMBussinessService
    {
        #region QCS00001抽样检验设定
        /// <summary>
        /// 抽样检验设定主列表
        /// SAM 2017年6月5日10:41:53
        /// </summary>
        /// <param name="Token">授权码</param>
        /// <param name="Type"></param>
        /// <param name="page">页码</param>
        /// <param name="rows">行数</param>
        /// <returns></returns>
        public static object Qcs00001GetList(string token, string Type, int page, int rows)
        {
            int count = 0;
            return UtilBussinessService.getPaginationModel(QCS_CheckTestSettingService.Qcs00001GetList(Type, page, rows, ref count), count);
        }

        /// <summary>
        /// 新增抽样检验设定
        /// SAM 2017年6月5日11:02:29
        /// </summary>
        /// <param name="Token">授权码</param>
        /// <param name="jArray"></param>
        /// <returns></returns>
        public static object Qcs00001insert(string Token, JArray jArray)
        {
            int success = 0;
            int fail = 0;
            string failIDs = null;
            string msg = null;
            string userid = UtilBussinessService.detoken(Token);
            JObject data = null;
            QCS_CheckTestSetting model = null;
            for (int i = 0; i < jArray.Count; i++)
            {
                data = (JObject)jArray[i];
                if (!QCS_CheckTestSettingService.Check("*", data.Value<string>("InspectionLevel"), data.Value<string>("InspectionMethod"), data.Value<string>("AQL"), null))
                {
                    model = new QCS_CheckTestSetting();
                    model.CheckTestSettingID = UniversalService.GetSerialNumber("QCS_CheckTestSetting");
                    model.InspectionStandard = "*";
                    model.InspectionLevel = data.Value<string>("InspectionLevel");
                    model.InspectionMethod = data.Value<string>("InspectionMethod");
                    model.AQL = data.Value<string>("AQL");
                    model.Status = data.Value<string>("Status");
                    model.Comments = data.Value<string>("Comments");
                    if (QCS_CheckTestSettingService.insert(userid, model))
                        success++;
                    else
                    {
                        failIDs = UtilBussinessService.str(failIDs, data.Value<string>("CheckTestSettingID"));
                        fail++;
                    }
                }
                else
                {
                    failIDs = UtilBussinessService.str(failIDs, data.Value<string>("CheckTestSettingID"));
                    msg = UtilBussinessService.str(msg, data.Value<string>("InspectionStandard") + "已存在");
                    fail++;
                }
            }
            return new { success = success, fail = fail, failIDs = failIDs, msg = msg };
        }

        /// <summary>
        /// 删除抽样检验设定
        /// SAM 2017年6月5日11:02:38
        /// </summary>
        /// <param name="Token">授权码</param>
        /// <param name="jArray"></param>
        /// <returns></returns>
        public static object Qcs00001delete(string Token, JArray jArray)
        {
            int success = 0;
            int fail = 0;
            string failIDs = null;
            string msg = null;
            string userid = UtilBussinessService.detoken(Token);
            JObject data = null;
            QCS_CheckTestSetting model = null;
            for (int i = 0; i < jArray.Count; i++)
            {
                data = (JObject)jArray[i];
                model = QCS_CheckTestSettingService.get(data.Value<string>("CheckTestSettingID"));
                //if (!SYS_ResourceDetailsService.CheckEquipment(data.Value<string>("EquipmentID")))
                //{
                model.Status = Framework.SystemID + "0201213000003";
                if (QCS_CheckTestSettingService.update(userid, model))
                    success++;
                else
                {
                    failIDs = UtilBussinessService.str(failIDs, data.Value<string>("CheckTestSettingID"));
                    fail++;
                }
                //}
                //else
                //{
                //    failIDs = UtilBussinessService.str(failIDs, data.Value<string>("EquipmentID"));
                //    msg = UtilBussinessService.str(failIDs, model.Code + "已使用，不能删除");
                //    fail++;
                //}

            }
            return new { success = success, fail = fail, failIDs = failIDs, msg = msg };
        }

        /// <summary>
        /// 更新抽样检验设定
        /// SAM 2017年6月5日11:02:483
        /// </summary>
        /// <param name="Token">授权码</param>
        /// <param name="jArray"></param>
        /// <returns></returns>
        public static object Qcs00001update(string Token, JArray jArray)
        {
            int success = 0;
            int fail = 0;
            string failIDs = null;
            string msg = null;
            string userid = UtilBussinessService.detoken(Token);
            JObject data = null;
            QCS_CheckTestSetting model = null;
            for (int i = 0; i < jArray.Count; i++)
            {
                data = (JObject)jArray[i];
                model = QCS_CheckTestSettingService.get(data.Value<string>("CheckTestSettingID"));
                model.InspectionLevel = data.Value<string>("InspectionLevel");
                model.InspectionMethod = data.Value<string>("InspectionMethod");
                model.AQL = data.Value<string>("AQL");
                model.Status = data.Value<string>("Status");
                model.Comments = data.Value<string>("Comments");
                if (!QCS_CheckTestSettingService.Check(model.InspectionStandard, data.Value<string>("InspectionLevel"), data.Value<string>("InspectionMethod"), data.Value<string>("AQL"), data.Value<string>("CheckTestSettingID")))
                {
                    if (QCS_CheckTestSettingService.update(userid, model))
                        success++;
                    else
                    {
                        failIDs = UtilBussinessService.str(failIDs, data.Value<string>("CheckTestSettingID"));
                        fail++;
                    }
                }
                else
                {
                    failIDs = UtilBussinessService.str(failIDs, data.Value<string>("CheckTestSettingID"));
                    msg = UtilBussinessService.str(msg, "资料重复");
                    fail++;
                }


            }
            return new { success = success, fail = fail, failIDs = failIDs, msg = msg };
        }

        /// <summary>
        /// 根据抽样检验设定获取他的明细
        /// SAM 2017年6月5日11:21:47
        /// </summary>
        /// <param name="Token">授权码</param>
        /// <param name="CheckTestSettingID"></param>
        /// <param name="page">页码</param>
        /// <param name="rows">行数</param>
        /// <returns></returns>
        public static object Qcs00001GetDetailsList(string token, string CheckTestSettingID)
        {
            return QCS_CheckTestSettingDetailsService.Qcs00001GetDetailsList(CheckTestSettingID);
        }

        /// <summary>
        /// 新增抽样检验设定明细
        /// SAM 2017年6月5日11:32:34
        /// </summary>
        /// <param name="Token">授权码</param>
        /// <param name="jArray"></param>
        /// <returns></returns>
        public static object Qcs00001Detailsinsert(string Token, JArray jArray)
        {
            int success = 0;
            int fail = 0;
            string failIDs = null;
            string msg = null;
            string userid = UtilBussinessService.detoken(Token);
            JObject data = null;
            QCS_CheckTestSettingDetails model = null;
            for (int i = 0; i < jArray.Count; i++)
            {
                data = (JObject)jArray[i];

                if (string.IsNullOrWhiteSpace(data.Value<string>("Sequence")))
                {
                    failIDs = UtilBussinessService.str(failIDs, data.Value<string>("CTSDID"));
                    msg = UtilBussinessService.str(msg, "序号不能为空");
                    fail++;
                    continue;
                }

                model = new QCS_CheckTestSettingDetails();
                model.CTSDID = UniversalService.GetSerialNumber("QCS_CheckTestSettingDetails");
                model.CheckTestSettingID = data.Value<string>("CheckTestSettingID");
                model.Sequence = data.Value<int>("Sequence");
                model.StartBatch = data.Value<decimal>("StartBatch");
                model.EndBatch = data.Value<decimal>("EndBatch");
                model.SamplingQuantity = data.Value<decimal>("SamplingQuantity");
                model.AcQuantity = data.Value<decimal>("AcQuantity");
                model.ReQuantity = data.Value<decimal>("ReQuantity");
                model.Status = Framework.SystemID + "0201213000001";
                model.Comments = data.Value<string>("Comments");
                if (!QCS_CheckTestSettingDetailsService.CheckSequence(data.Value<string>("Sequence"), data.Value<string>("CheckTestSettingID"), null))
                {
                    if (QCS_CheckTestSettingDetailsService.insert(userid, model))
                        success++;
                    else
                    {
                        failIDs = UtilBussinessService.str(failIDs, data.Value<string>("CTSDID"));
                        fail++;
                    }
                }
                else
                {
                    failIDs = UtilBussinessService.str(failIDs, data.Value<string>("CTSDID"));
                    msg = UtilBussinessService.str(msg, "序号：" + data.Value<string>("Sequence") + "已存在");
                    fail++;
                }
            }
            return new { success = success, fail = fail, failIDs = failIDs, msg = msg };
        }

        /// <summary>
        /// 删除抽样检验设定明细
        /// SAM 2017年6月5日11:32:34
        /// </summary>
        /// <param name="Token">授权码</param>
        /// <param name="jArray"></param>
        /// <returns></returns>
        public static object Qcs00001Detailsdelete(string Token, JArray jArray)
        {
            int success = 0;
            int fail = 0;
            string failIDs = null;
            string msg = null;
            string userid = UtilBussinessService.detoken(Token);
            JObject data = null;
            QCS_CheckTestSettingDetails model = null;
            for (int i = 0; i < jArray.Count; i++)
            {
                data = (JObject)jArray[i];
                model = QCS_CheckTestSettingDetailsService.get(data.Value<string>("CTSDID"));
                //if (!SYS_ResourceDetailsService.CheckEquipment(data.Value<string>("EquipmentID")))
                //{
                model.Status = Framework.SystemID + "0201213000003";
                if (QCS_CheckTestSettingDetailsService.update(userid, model))
                    success++;
                else
                {
                    failIDs = UtilBussinessService.str(failIDs, data.Value<string>("CTSDID"));
                    fail++;
                }
                //}
                //else
                //{
                //    failIDs = UtilBussinessService.str(failIDs, data.Value<string>("EquipmentID"));
                //    msg = UtilBussinessService.str(failIDs, model.Code + "已使用，不能删除");
                //    fail++;
                //}

            }
            return new { success = success, fail = fail, failIDs = failIDs, msg = msg };
        }

        /// <summary>
        /// 更新抽样检验设定明细
        /// SAM 2017年6月5日11:32:47
        /// </summary>
        /// <param name="Token">授权码</param>
        /// <param name="jArray"></param>
        /// <returns></returns>
        public static object Qcs00001Detailsupdate(string Token, JArray jArray)
        {
            int success = 0;
            int fail = 0;
            string failIDs = null;
            string msg = null;
            string userid = UtilBussinessService.detoken(Token);
            JObject data = null;
            QCS_CheckTestSettingDetails model = null;
            for (int i = 0; i < jArray.Count; i++)
            {
                data = (JObject)jArray[i];
                model = QCS_CheckTestSettingDetailsService.get(data.Value<string>("CTSDID"));
                model.StartBatch = data.Value<decimal>("StartBatch");
                model.EndBatch = data.Value<decimal>("EndBatch");
                model.SamplingQuantity = data.Value<decimal>("SamplingQuantity");
                model.AcQuantity = data.Value<decimal>("AcQuantity");
                model.ReQuantity = data.Value<decimal>("ReQuantity");
                model.Comments = data.Value<string>("Comments");
                if (QCS_CheckTestSettingDetailsService.update(userid, model))
                    success++;
                else
                {
                    failIDs = UtilBussinessService.str(failIDs, data.Value<string>("CTSDID"));
                    fail++;
                }

            }
            return new { success = success, fail = fail, failIDs = failIDs, msg = msg };
        }

        /// <summary>
        /// 单个抽样检验设定删除
        /// Mouse 2017年7月26日11:39:33
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public static object Qcs00001Delete(JObject request)
        {
            string Token = request.Value<string>("Token");
            string userid = UtilBussinessService.detoken(Token);
            string CheckTestSettingID = request.Value<string>("CheckTestSettingID");

            QCS_CheckTestSetting model = QCS_CheckTestSettingService.get(CheckTestSettingID);
            if (model == null)
                return new { status = "410", msg = "删除失败！抽样检验设定。" };

            model.Status = Framework.SystemID + "0201213000003";

            if (QCS_CheckTestSettingService.update(userid, model))
                return new { status = "200", msg = "删除成功！" };
            else
                return new { status = "410", msg = "删除失败！" };
        }

        #endregion

        #region QCS00002檢驗項目類別維護
        /// <summary>
        /// 获取检验项目列表
        /// SAM 2017年6月9日10:48:56
        /// </summary>
        /// <param name="token"></param>
        /// <param name="code"></param>
        /// <param name="status"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <returns></returns>
        public static object Qcs00002GetProjectList(string token, string code, string status, int page, int rows)
        {
            int count = 0;
            return UtilBussinessService.getPaginationModel(QCS_InspectionProjectService.Qcs00002GetProjectList(code, status, page, rows, ref count), count);
        }

        /// <summary>
        /// 检验项目的删除
        /// SAM 2017年9月28日10:36:35
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public static object Qcs00002ProjectDelete(JObject request)
        {
            string Token = request.Value<string>("Token");
            string userid = UtilBussinessService.detoken(Token);
            string InspectionProjectID = request.Value<string>("InspectionProjectID");
            QCS_InspectionProject model = QCS_InspectionProjectService.get(InspectionProjectID);
            if (model == null)
                return new { status = "410", msg = "删除失败！不存在的检验项目！" };

            //如果检验项目已经存在QCS00004的设定中，则无法删除
            if (QCS_StaInsSpeSettingService.CheckQcs00002Project(InspectionProjectID))
                return new { status = "410", msg = "删除失败！此检验项目已存在被使用！" };

            //如果检验项目已经存在QCS00005，7,8的明细中，则无法删除
            if (QCS_InspectionDocumentDetailsService.CheckQcs00002Project(InspectionProjectID))
                return new { status = "410", msg = "删除失败！此检验项目已存在被使用！" };

            model.Status = Framework.SystemID + "0201213000003";
            if (QCS_InspectionProjectService.update(userid, model))
                return new { status = "200", msg = "删除成功！" };
            else
                return new { status = "410", msg = "删除失败！" };
        }

        /// <summary>
        /// 新增检验项目
        /// SAM 2017年6月9日10:49:35
        /// </summary>
        /// <param name="Token"></param>
        /// <param name="jArray"></param>
        /// <returns></returns>
        public static object Qcs00002Projectinsert(string Token, JArray jArray)
        {
            int success = 0;
            int fail = 0;
            string failIDs = null;
            string msg = null;
            string userid = UtilBussinessService.detoken(Token);
            JObject data = null;
            QCS_InspectionProject model = null;
            for (int i = 0; i < jArray.Count; i++)
            {
                data = (JObject)jArray[i];
                if (!QCS_InspectionProjectService.Check(data.Value<string>("Code"), null))
                {
                    model = new QCS_InspectionProject();
                    model.InspectionProjectID = UniversalService.GetSerialNumber("QCS_InspectionProject");
                    model.Code = data.Value<string>("Code");
                    model.Name = data.Value<string>("Name");
                    model.InspectionStandard = data.Value<string>("InspectionStandard");
                    model.InspectionLevel = data.Value<string>("InspectionLevel");
                    model.Disadvantages = data.Value<string>("Disadvantages");
                    model.Attribute = data.Value<string>("Attribute");
                    model.Status = data.Value<string>("Status");
                    model.Comments = data.Value<string>("Comments");
                    if (QCS_InspectionProjectService.insert(userid, model))
                        success++;
                    else
                    {
                        failIDs = UtilBussinessService.str(failIDs, data.Value<string>("InspectionProjectID"));
                        fail++;
                    }
                }
                else
                {
                    failIDs = UtilBussinessService.str(failIDs, data.Value<string>("InspectionProjectID"));
                    msg = UtilBussinessService.str(msg, data.Value<string>("Code") + "已存在");
                    fail++;
                }
            }
            return new { success = success, fail = fail, failIDs = failIDs, msg = msg };
        }

        /// <summary>
        /// 删除检验项目
        /// SAN 2017年6月9日10:49:46
        /// </summary>
        /// <param name="Token"></param>
        /// <param name="jArray"></param>
        /// <returns></returns>
        public static object Qcs00002Projectdelete(string Token, JArray jArray)
        {
            int success = 0;
            int fail = 0;
            string failIDs = null;
            string msg = null;
            string userid = UtilBussinessService.detoken(Token);
            JObject data = null;
            QCS_InspectionProject model = null;
            for (int i = 0; i < jArray.Count; i++)
            {
                data = (JObject)jArray[i];
                model = QCS_InspectionProjectService.get(data.Value<string>("InspectionProjectID"));
                model.Status = Framework.SystemID + "0201213000003";
                if (QCS_InspectionProjectService.update(userid, model))
                    success++;
                else
                {
                    failIDs = UtilBussinessService.str(failIDs, data.Value<string>("InspectionProjectID"));
                    fail++;
                }

            }
            return new { success = success, fail = fail, failIDs = failIDs, msg = msg };
        }

        /// <summary>
        /// 更新检验项目
        /// SAM 2017年6月9日10:50:00
        /// </summary>
        /// <param name="Token"></param>
        /// <param name="jArray"></param>
        /// <returns></returns>
        public static object Qcs00002Projectupdate(string Token, JArray jArray)
        {
            int success = 0;
            int fail = 0;
            string failIDs = null;
            string msg = null;
            string userid = UtilBussinessService.detoken(Token);
            JObject data = null;
            QCS_InspectionProject model = null;
            for (int i = 0; i < jArray.Count; i++)
            {
                data = (JObject)jArray[i];
                model = QCS_InspectionProjectService.get(data.Value<string>("InspectionProjectID"));
                model.InspectionStandard = data.Value<string>("InspectionStandard");
                model.InspectionLevel = data.Value<string>("InspectionLevel");
                model.Disadvantages = data.Value<string>("Disadvantages");
                model.Attribute = data.Value<string>("Attribute");
                model.Status = data.Value<string>("Status");
                model.Comments = data.Value<string>("Comments");
                model.Name = data.Value<string>("Name");
                if (QCS_InspectionProjectService.UpdateV2(userid, model))
                    success++;
                else
                {
                    failIDs = UtilBussinessService.str(failIDs, data.Value<string>("CheckTestSettingID"));
                    fail++;
                }


            }
            return new { success = success, fail = fail, failIDs = failIDs, msg = msg };
        }

        /// <summary>
        /// 检验类别列表
        /// SAM 2017年6月9日10:50:12
        /// </summary>
        /// <param name="token"></param>
        /// <param name="code"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <returns></returns>
        public static object Qcs00002GetTypeList(string token, string code, int page, int rows)
        {
            int count = 0;
            return UtilBussinessService.getPaginationModel(SYS_ParameterService.GeneralGetList("019121300001E", code, null, page, rows, ref count), count);
        }

        /// <summary>
        /// 新增检验类别
        /// SAM 2017年6月9日11:42:19
        /// </summary>
        /// <param name="Token"></param>
        /// <param name="jArray"></param>
        /// <returns></returns>
        public static object Qcs00002Typeinsert(string Token, JArray jArray)
        {
            int success = 0;
            int fail = 0;
            string failIDs = null;
            string msg = null;
            string userid = UtilBussinessService.detoken(Token);
            JObject data = null;
            SYS_Parameters model = null;
            string Type = Framework.SystemID + "019121300001E";
            QCS_SamplingSetting SModel = null;
            DateTime Now = DateTime.Now;
            DateTime UTCNow = DateTime.UtcNow;
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
                    model.IsEnable = 1; //状态       
                    model.Sequence = 0;
                    model.UsingType = 0;

                    if (SYS_ParameterService.insert(userid, model))
                    {
                        IList<Hashtable> MethodList = SYS_ParameterService.GetLists(Framework.SystemID + "019121300001B");
                        IList<Hashtable> DisadvantagesList = SYS_ParameterService.GetLists(Framework.SystemID + "019121300001D");
                        foreach (Hashtable MItem in MethodList)
                        {
                            foreach (Hashtable DItem in DisadvantagesList)
                            {
                                SModel = new QCS_SamplingSetting();
                                SModel.SamplingSettingID = UniversalService.GetSerialNumber("QCS_SamplingSetting");
                                SModel.CategoryID = model.ParameterID;
                                SModel.InspectionMethod = MItem["value"].ToString();
                                SModel.Disadvantages = DItem["value"].ToString();
                                SModel.Status = Framework.SystemID + "0201213000001";
                                SModel.CreateTime = UTCNow;
                                SModel.CreateLocalTime = Now;
                                QCS_SamplingSettingService.insert(userid, SModel);
                            }
                        }
                        success++;
                    }
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
        /// 检验类别删除
        /// SAM 2017年10月17日16:09:13
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public static object Qcs00002TypeDelete(JObject request)
        {
            string Token = request.Value<string>("Token");
            string userid = UtilBussinessService.detoken(Token);
            string ParameterID = request.Value<string>("ParameterID");
            SYS_Parameters model = SYS_ParameterService.get(ParameterID);
            if (model == null)
                return new { status = "410", msg = "删除失败！不存在的检验类别！" };

            //如果检验类别已存在与检验单据明细档中，则无法删除
            if (QCS_StaInsSpeSettingService.CheckQcs00002Type(ParameterID))
                return new { status = "410", msg = model.Code + "代号已使用，不得删除" };

            ////如果检验项目已经存在QCS00005，7,8的明细中，则无法删除
            if (QCS_InspectionDocumentDetailsService.CheckQcs00002Type(ParameterID))
                return new { status = "410", msg = model.Code + "代号已使用，不得删除" };

            model.IsEnable = 2;

            if (SYS_ParameterService.update(userid, model))
            {
                QCS_SamplingSettingService.Delete(userid, ParameterID);
                return new { status = "200", msg = "删除成功！" };
            }
            else
                return new { status = "410", msg = "删除失败！" };
        }

        /// <summary>
        /// 检验类别的更新
        /// SAM 2017年10月17日16:12:53
        /// </summary>
        /// <param name="Token"></param>
        /// <param name="jArray"></param>
        /// <returns></returns>
        public static object Qcs00002Typeupdate(string Token, JArray jArray)
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
                if (model == null)
                {
                    failIDs = UtilBussinessService.str(failIDs, data.Value<string>("ParameterID"));
                    msg = UtilBussinessService.str(msg, "不存在的检验类别");
                    fail++;
                }
                model.IsEnable = data.Value<int>("IsEnable");
                model.Comments = data.Value<string>("Comments");
                model.Name = data.Value<string>("Name");
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
        /// 检验类别的抽检设定列表
        /// SAM 2017年6月9日10:50:47
        /// </summary>
        /// <param name="token"></param>
        /// <param name="TypeID"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <returns></returns>
        public static object Qcs00002GetTypeDetailsList(string token, string TypeID)
        {
            return QCS_SamplingSettingService.Qcs00002GetTypeDetailsList(TypeID);
        }

        /// <summary>
        /// 更新抽检设定
        /// SAM 2017年6月9日10:51:28
        /// </summary>
        /// <param name="Token"></param>
        /// <param name="jArray"></param>
        /// <returns></returns>
        public static object Qcs00002TypeDetailsupdate(string Token, JArray jArray)
        {
            int success = 0;
            int fail = 0;
            string failIDs = null;
            string msg = null;
            string userid = UtilBussinessService.detoken(Token);
            JObject data = null;
            DateTime Now = DateTime.Now;
            DateTime UTCNow = DateTime.UtcNow;
            QCS_SamplingSetting model = null;
            for (int i = 0; i < jArray.Count; i++)
            {
                data = (JObject)jArray[i];
                model = QCS_SamplingSettingService.get(data.Value<string>("CategoryID"), data.Value<string>("InspectionMethod"), Framework.SystemID + "0201213000077");
                model.AQL = data.Value<string>("One");
                model.ModifiedLocalTime = Now;
                model.ModifiedTime = UTCNow;
                model.Modifier = userid;
                QCS_SamplingSettingService.update(userid, model);
                model = QCS_SamplingSettingService.get(data.Value<string>("CategoryID"), data.Value<string>("InspectionMethod"), Framework.SystemID + "0201213000078");
                model.AQL = data.Value<string>("Two");
                model.ModifiedLocalTime = Now;
                model.ModifiedTime = UTCNow;
                model.Modifier = userid;
                QCS_SamplingSettingService.update(userid, model);
                model = QCS_SamplingSettingService.get(data.Value<string>("CategoryID"), data.Value<string>("InspectionMethod"), Framework.SystemID + "0201213000079");
                model.AQL = data.Value<string>("Three");
                model.ModifiedLocalTime = Now;
                model.ModifiedTime = UTCNow;
                model.Modifier = userid;
                QCS_SamplingSettingService.update(userid, model);
                model = QCS_SamplingSettingService.get(data.Value<string>("CategoryID"), data.Value<string>("InspectionMethod"), Framework.SystemID + "020121300007A");
                model.AQL = data.Value<string>("Four");
                model.ModifiedLocalTime = Now;
                model.ModifiedTime = UTCNow;
                model.Modifier = userid;
                QCS_SamplingSettingService.update(userid, model);
            }
            return new { success = success, fail = fail, failIDs = failIDs, msg = msg };
        }

        #endregion

        #region QCS00003检验群组码
        /// <summary>
        /// 检验群组码列表
        /// SAM 2017年5月26日11:42:00
        /// </summary>
        /// <param name="Token">授权码</param>
        /// <param name="Code">代号</param>
        /// <param name="Status">状态</param>
        /// <param name="page">页码</param>
        /// <param name="rows">行数</param>
        /// <returns></returns>
        public static object Qcs00003GetList(string token, string code, string status, int page, int rows)
        {
            int count = 0;
            return UtilBussinessService.getPaginationModel(SYS_ParameterService.GeneralGetList("0191213000018", code, status, page, rows, ref count), count);
        }

        /// <summary>
        /// 根据检验群组码获取他的料品设定
        /// SAM 2017年5月26日11:49:47
        /// </summary>
        /// <param name="Token">授权码</param>
        /// <param name="GroupID"></param>
        /// <param name="page">页码</param>
        /// <param name="rows">行数</param>
        /// <returns></returns>
        public static object Qcs00003GetDetailsList(string token, string GroupID, int page, int rows)
        {
            int count = 0;
            return UtilBussinessService.getPaginationModel(QCS_GroupItemService.Qcs00003GetDetailsList(GroupID, page, rows, ref count), count);
        }

        /// <summary>
        /// 根据检验群组码获取他的料品设定（不分页）
        /// SAM 2017年5月26日11:50:00
        /// </summary>
        /// <param name="Token">授权码</param>
        /// <param name="GroupID"></param>
        /// <returns></returns>
        public static object Qcs00003DetailsList(string token, string GroupID)
        {
            return QCS_GroupItemService.Qcs00003DetailsList(GroupID);
        }

        /// <summary>
        /// 根据检验群组码获取不属于他的料品列表
        /// SAM 2017年5月26日11:50:14
        /// </summary>
        /// <param name="Token">授权码</param>
        /// <param name="GroupID"></param>
        /// <returns></returns>
        public static object Qcs00003ItemList(string token, string GroupID, string StartCode, string EndCode, int page, int rows)
        {
            int count = 0;
            IList<Hashtable> result = SYS_ItemsService.Qcs00003ItemList(GroupID, StartCode, EndCode, page, rows, ref count);
            return UtilBussinessService.getPaginationModel(result, count);
        }


        /// <summary>
        /// 保存检验群组码的明细
        /// SAM 2017年5月26日11:48:19
        /// </summary>
        /// <param name="request">JSON数据</param>
        /// <returns></returns>
        public static object Qcs00003DetailsSave(JObject request)
        {
            string Token = request.Value<string>("Token");
            string userid = UtilBussinessService.detoken(Token);
            string GroupID = request.Value<string>("GroupID");
            JArray List = request.Value<JArray>("data");

            if (string.IsNullOrWhiteSpace(GroupID))
                return new { status = "410", msg = "GroupID为空!" };

            JObject data = null;
            Hashtable AddModel = null;
            QCS_GroupItem model = null;
            string New = null;
            List<Hashtable> Add = new List<Hashtable>();
            for (int i = 0; i < List.Count; i++) //将新增的和已存在的分开
            {
                data = (JObject)List[i];
                AddModel = new Hashtable();
                if (string.IsNullOrWhiteSpace(data.Value<string>("GroupItemID")))
                {
                    AddModel["ItemID"] = data.Value<string>("ItemID");
                    AddModel["GroupID"] = GroupID;
                    Add.Add(AddModel);
                }
                else
                {
                    if (New == null)
                        New = data.Value<string>("GroupItemID");
                    else
                        New = New + "','" + data.Value<string>("GroupItemID");
                }
            }
            QCS_GroupItemService.Delete(userid, New, GroupID);

            foreach (Hashtable item in Add) //循环添加新增的
            {
                model = new QCS_GroupItem();
                model.GroupItemID = UniversalService.GetSerialNumber("QCS_GroupItem");
                model.GroupID = GroupID;
                model.ItemID = item["ItemID"].ToString();
                model.Status = Framework.SystemID + "0201213000001";
                if (!QCS_GroupItemService.Check(model.ItemID, model.GroupID))
                    QCS_GroupItemService.insert(userid, model);
            }

            return new { status = "200", msg = "保存成功!" };
        }

        /// <summary>
        /// 根据检验群组码获取他的料品设定（不分页）
        /// SAM 2017年5月26日11:50:00
        /// </summary>
        /// <param name="Token">授权码</param>
        /// <param name="GroupID"></param>
        /// <returns></returns>
        public static object Qcs00003DetailsListV2(string token, string GroupID)
        {
            return SYS_ItemsService.Qcs00003DetailsListV2(GroupID);
        }

        /// <summary>
        /// 获取所有的未分配检验群组码的料品列表
        /// SAM 2017年10月18日10:52:15
        /// </summary>
        /// <param name="token"></param>
        /// <param name="StartCode"></param>
        /// <param name="EndCode"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <returns></returns>
        public static object Qcs00003ItemListV2(string token, string StartCode, string EndCode, int page, int rows)
        {
            int count = 0;
            IList<Hashtable> result = SYS_ItemsService.Qcs00003ItemListV2(StartCode, EndCode, page, rows, ref count);
            return UtilBussinessService.getPaginationModel(result, count);
        }

        /// <summary>
        /// 保存检验群组码的明细
        /// Sam 2017年10月18日11:17:37 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public static object Qcs00003DetailsSaveV2(JObject request)
        {
            string Token = request.Value<string>("Token");
            string userid = UtilBussinessService.detoken(Token);
            string GroupID = request.Value<string>("GroupID");
            JArray List = request.Value<JArray>("data");

            if (string.IsNullOrWhiteSpace(GroupID))
                return new { status = "410", msg = "GroupID为空!" };

            JObject data = null;
            string Items = null;
            for (int i = 0; i < List.Count; i++)
            {
                data = (JObject)List[i];
                if (Items == null)
                    Items = data.Value<string>("ItemID");
                else
                    Items = Items + "','" + data.Value<string>("ItemID");
            }

            //根据检验群码流水号+料品流水号集合，为集合内的所有料品都更新上检验群码号。
            SYS_ItemsService.Qcs00003UpdateAdd(userid, Items, GroupID);
            //根据检验群码流水号+料品流水号集合，隶属于这个检验群妈组，但是却不在集合内的料品，其检验群组码将设置成null
            SYS_ItemsService.Qcs00003UpdateDelete(userid, Items, GroupID);

            return new { status = "200", msg = "保存成功!" };
        }



        #endregion

        #region QCS00004标准检验规范设定
        /// <summary>
        /// 标准检验规范设定-料品页签列表
        /// SAM 2017年6月15日17:09:07
        /// </summary>
        /// <param name="token"></param>
        /// <param name="Code"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <returns></returns>
        public static object Qcs00004GetItemList(string token, string Code, int page, int rows)
        {
            int count = 0;
            return UtilBussinessService.getPaginationModel(SYS_ItemsService.Qcs00004GetItemList(Code, page, rows, ref count), count);
        }

        /// <summary>
        ///  标准检验规范设定-检验群码页签列表
        ///  SAM 2017年6月16日10:25:29
        /// </summary>
        /// <param name="token"></param>
        /// <param name="Code"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <returns></returns>
        public static object Qcs00004GetGroupList(string token, string Code, int page, int rows)
        {
            int count = 0;
            return UtilBussinessService.getPaginationModel(SYS_ParameterService.Qcs00004GetGroupList(Code, page, rows, ref count), count);
        }

        /// <summary>
        /// Qcs00004弹窗表头列表
        /// SAM 2017年6月16日10:26:16
        /// </summary>
        /// <param name="token"></param>
        /// <param name="StaInsSpeSettingID"></param>
        /// <param name="InspectionType"></param>
        /// <param name="Code"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <returns></returns>
        public static object Qcs00004GetHeaderList(string token, string PartID, string SettingType, string InspectionType, string Code, int page, int rows)
        {
            IList<Hashtable> result = null;
            int count = 0;
            int Seq = 1;
            if (SettingType == (Framework.SystemID + "020121300007B"))//料品
            {
                result = QCS_StaInsSpeSettingService.Qcs00004GetHeaderList(PartID, null, Framework.SystemID + InspectionType, Code, page, rows, ref count);
                foreach (Hashtable item in result)
                {
                    item["ID"] = Seq;
                    Seq++;
                }
                return UtilBussinessService.getPaginationModel(result, count);
            }
            else if (SettingType == (Framework.SystemID + "020121300007C"))//检验群组码
            {
                result = QCS_StaInsSpeSettingService.Qcs00004GetHeaderList(null, PartID, Framework.SystemID + InspectionType, Code, page, rows, ref count);
                foreach (Hashtable item in result)
                {
                    item["ID"] = Seq;
                    Seq++;
                }
                return UtilBussinessService.getPaginationModel(result, count);
            }
            else
                return null;
        }

        /// <summary>
        /// 弹窗表头更新
        /// SAM 2017年7月6日10:54:08
        /// </summary>
        /// <param name="Token"></param>
        /// <param name="jArray"></param>
        /// <returns></returns>
        public static object Qcs00004Headerupdate(string Token, JArray jArray)
        {
            int success = 0;
            int fail = 0;
            string failIDs = null;
            string msg = null;
            string userid = UtilBussinessService.detoken(Token);
            JObject data = null;
            SFC_ItemProcess Processmodel = null;
            SFC_ItemOperation Operationmodel = null;
            for (int i = 0; i < jArray.Count; i++)
            {
                data = (JObject)jArray[i];
                if (!string.IsNullOrWhiteSpace(data.Value<string>("ItemOperationID")))
                {
                    Operationmodel = SFC_ItemOperationService.get(data.Value<string>("ItemOperationID"));
                    if (Operationmodel == null)
                    {
                        failIDs = UtilBussinessService.str(failIDs, data.Value<string>("ItemProcessID"));
                        msg = UtilBussinessService.str(msg, data.Value<string>("Sequence") + "工序流水号错误");
                        fail++;
                        continue;
                    }
                    if (data.Value<string>("InspectionType") == Framework.SystemID + "020121300007E")
                        Operationmodel.IsIP = data.Value<bool>("IsIP");
                    else if (data.Value<string>("InspectionType") == Framework.SystemID + "0201213000080")
                        Operationmodel.IsFPI = data.Value<bool>("IsFPI");
                    else if (data.Value<string>("InspectionType") == Framework.SystemID + "0201213000081")
                        Operationmodel.IsOSI = data.Value<bool>("IsOSI");

                    if (!string.IsNullOrWhiteSpace(data.Value<string>("GroupID")))
                        Operationmodel.InspectionGroupID = data.Value<string>("GroupID");
                    if (SFC_ItemOperationService.update(userid, Operationmodel))
                        success++;
                    else
                    {
                        failIDs = UtilBussinessService.str(failIDs, data.Value<string>("ItemProcessID"));
                        fail++;
                    }
                }
                else
                {
                    Processmodel = SFC_ItemProcessService.get(data.Value<string>("ItemProcessID"));
                    if (Processmodel == null)
                    {
                        failIDs = UtilBussinessService.str(failIDs, data.Value<string>("ItemProcessID"));
                        msg = UtilBussinessService.str(msg, data.Value<string>("Sequence") + "制程流水号错误");
                        fail++;
                        continue;
                    }
                    if (data.Value<string>("InspectionType") == Framework.SystemID + "020121300007E")
                        Processmodel.IsIP = data.Value<bool>("IsIP");
                    else if (data.Value<string>("InspectionType") == Framework.SystemID + "0201213000080")
                        Processmodel.IsFPI = data.Value<bool>("IsFPI");
                    else if (data.Value<string>("InspectionType") == Framework.SystemID + "0201213000081")
                        Processmodel.IsOSI = data.Value<bool>("IsOSI");
                    if (!string.IsNullOrWhiteSpace(data.Value<string>("GroupID")))
                        Processmodel.InspectionGroupID = data.Value<string>("GroupID");
                    if (SFC_ItemProcessService.update(userid, Processmodel))
                        success++;
                    else
                    {
                        failIDs = UtilBussinessService.str(failIDs, data.Value<string>("ItemProcessID"));
                        fail++;
                    }
                }

            }
            return new { success = success, fail = fail, failIDs = failIDs, msg = msg };
        }


        /// <summary>
        /// QCS弹窗明细列表
        /// SAM 2017年6月16日10:27:48
        /// </summary>
        /// <param name="token"></param>
        /// <param name="SISSPDetailID"></param>
        /// <param name="Code"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <returns></returns>
        public static object Qcs00004GetDetailsList(string token, string SettingType, string PartID, string InspectionType, string ProcessID, string OperationID, string Code, int page, int rows)
        {
            int count = 0;

            return UtilBussinessService.getPaginationModel(QCS_StaInsSpeSettingService.Qcs00004GetDetailsList(SettingType, PartID, InspectionType, ProcessID, OperationID, Code, page, rows, ref count), count);
        }

        /// <summary>
        /// 明细的新增
        /// SAM 2017年7月6日11:40:10
        /// </summary>
        /// <param name="Token"></param>
        /// <param name="jArray"></param>
        /// <returns></returns>
        public static object Qcs00004Detailinsert(string Token, JArray jArray)
        {
            int success = 0;
            int fail = 0;
            string failIDs = null;
            string msg = null;
            string userid = UtilBussinessService.detoken(Token);
            JObject data = null;
            QCS_StaInsSpeSetting model = null;
            for (int i = 0; i < jArray.Count; i++)
            {
                data = (JObject)jArray[i];
                //if (QCS_CheckTestSettingService.Check("*", data.Value<string>("InspectionLevel"), data.Value<string>("InspectionMethod"), data.Value<string>("AQL"), null))
                //{
                //    failIDs = UtilBussinessService.str(failIDs, data.Value<string>("CheckTestSettingID"));
                //    msg = UtilBussinessService.str(msg, data.Value<string>("InspectionStandard") + "已存在");
                //    fail++;
                //    continue;
                //}
                model = new QCS_StaInsSpeSetting();
                model.StaInsSpeSettingID = UniversalService.GetSerialNumber("QCS_StaInsSpeSetting");
                model.PartID = data.Value<string>("PartID");
                model.SettingType = data.Value<string>("SettingType");
                model.InspectionType = data.Value<string>("InspectionType");
                model.ProcessID = data.Value<string>("ProcessID");
                model.OperationID = data.Value<string>("OperationID");
                model.Sequence = data.Value<int>("Sequence");
                model.CategoryID = data.Value<string>("CategoryID");
                model.InspectionMethod = data.Value<string>("InspectionMethod");
                model.InspectionDay = data.Value<int>("InspectionDay");
                model.InspectionStandard = data.Value<string>("InspectionStandard");
                model.InspectionProjectID = data.Value<string>("InspectionProjectID");

                model.Attribute = data.Value<string>("Attribute");
                model.Status = Framework.SystemID + "0201213000001";
                model.Comments = data.Value<string>("Comments");
                model.AQL = QCS_SamplingSettingService.getAQL(model.CategoryID, model.InspectionMethod, data.Value<string>("Disadvantages"));
                if (QCS_StaInsSpeSettingService.insert(userid, model))
                    success++;
                else
                {
                    failIDs = UtilBussinessService.str(failIDs, data.Value<string>("StaInsSpeSettingID"));
                    fail++;
                }

            }
            return new { success = success, fail = fail, failIDs = failIDs, msg = msg };
        }


        /// <summary>
        /// 明细的删除
        /// SAM 2017年7月6日11:44:54
        /// </summary>
        /// <param name="Token"></param>
        /// <param name="jArray"></param>
        /// <returns></returns>
        public static object Qcs00004Detaildelete(string Token, JArray jArray)
        {
            int success = 0;
            int fail = 0;
            string failIDs = null;
            string msg = null;
            string userid = UtilBussinessService.detoken(Token);
            JObject data = null;
            QCS_StaInsSpeSetting model = null;
            for (int i = 0; i < jArray.Count; i++)
            {
                data = (JObject)jArray[i];
                model = QCS_StaInsSpeSettingService.get(data.Value<string>("StaInsSpeSettingID"));

                model.Status = Framework.SystemID + "0201213000003";
                if (QCS_StaInsSpeSettingService.update(userid, model))
                    success++;
                else
                {
                    failIDs = UtilBussinessService.str(failIDs, data.Value<string>("StaInsSpeSettingID"));
                    fail++;
                }

            }
            return new { success = success, fail = fail, failIDs = failIDs, msg = msg };
        }

        /// <summary>
        /// Qcs00004明细单条删除
        /// Mouse 2017年10月11日15:04:25
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public static object Qcs00004DetailDelete(JObject request)
        {
            string userid = UtilBussinessService.detoken(request.Value<string>("Token"));
            QCS_StaInsSpeSetting model = QCS_StaInsSpeSettingService.get(request.Value<string>("StaInsSpeSettingID"));
            if (model == null)
            {
                return new { Status = "400", msg = "检验明细流水号为空！" };
            }
            model.Status = Framework.SystemID + "0201213000003";
            if (QCS_StaInsSpeSettingService.update(userid, model))
                return new { Status = "200", msg = "删除成功！" };
            else
                return new { Status = "400", msg = "删除失败！" };
        }

        /// <summary>
        /// 明细的更新
        /// SAM 2017年7月6日11:45:22
        /// </summary>
        /// <param name="Token"></param>
        /// <param name="jArray"></param>
        /// <returns></returns>
        public static object Qcs00004Detailupdate(string Token, JArray jArray)
        {
            int success = 0;
            int fail = 0;
            string failIDs = null;
            string msg = null;
            string userid = UtilBussinessService.detoken(Token);
            JObject data = null;
            QCS_StaInsSpeSetting model = null;
            for (int i = 0; i < jArray.Count; i++)
            {
                data = (JObject)jArray[i];
                model = QCS_StaInsSpeSettingService.get(data.Value<string>("StaInsSpeSettingID"));
                model.Sequence = data.Value<int>("Sequence");
                model.InspectionDay = data.Value<int>("InspectionDay");
                model.InspectionStandard = data.Value<string>("InspectionStandard");
                model.Attribute = data.Value<string>("Attribute");
                model.AQL = QCS_SamplingSettingService.getAQL(model.CategoryID, model.InspectionMethod, data.Value<string>("Disadvantages"));
                model.Comments = data.Value<string>("Comments");
                if (QCS_StaInsSpeSettingService.update(userid, model))
                    success++;
                else
                {
                    failIDs = UtilBussinessService.str(failIDs, data.Value<string>("StaInsSpeSettingID"));
                    fail++;
                }
            }
            return new { success = success, fail = fail, failIDs = failIDs, msg = msg };
        }

        /// <summary>
        /// 检验群码专属表头列表
        /// Sam 2017年10月19日11:54:03
        /// </summary>
        /// <param name="token"></param>
        /// <param name="PartID"></param>
        /// <param name="InspectionType"></param>
        /// <param name="Code"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <returns></returns>
        public static object Qcs00004GroupGetHeaderList(string token, string GroupID, string InspectionType, int page, int rows)
        {
            IList<Hashtable> result = null;
            int count = 0;
            result = QCS_StaInsSpeSettingService.Qcs00004GroupGetHeaderList(GroupID, InspectionType, page, rows, ref count);
            Hashtable CreateDetail = null;
            Hashtable ModifiedDetail = null;

            string FieldName = null;
            if (InspectionType == Framework.SystemID + "020121300007E")
                FieldName = "IsIP";
            else if (InspectionType == Framework.SystemID + "0201213000080")
                FieldName = "IsFPI";
            else if (InspectionType == Framework.SystemID + "0201213000081")
                FieldName = "IsOSI";

            for (int i = 0; i < result.Count; i++)
            {
                result[i]["ID"] = i + 1;
                result[i]["IsInspection"] = true;
                if (string.IsNullOrWhiteSpace(result[i]["OperationID"].ToString()))
                {
                    if (SFC_ItemProcessService.Qcs04CheckYorN(result[i]["ProcessID"].ToString(), GroupID, FieldName, 0))
                    {
                        if (!SFC_ItemProcessService.Qcs04CheckYorN(result[i]["ProcessID"].ToString(), GroupID, FieldName, 1))
                            result[i]["IsInspection"] = false;
                    }
                }
                else
                {
                    if (SFC_ItemOperationService.Qcs04CheckYorN(result[i]["OperationID"].ToString(), result[i]["ProcessID"].ToString(), GroupID, FieldName, 0))
                    {
                        if (!SFC_ItemOperationService.Qcs04CheckYorN(result[i]["OperationID"].ToString(), result[i]["ProcessID"].ToString(), GroupID, FieldName, 1))
                            result[i]["IsInspection"] = false;
                    }
                }
                CreateDetail = QCS_StaInsSpeSettingService.Qcs00004GetCreate(GroupID, InspectionType, result[i]["ProcessID"].ToString(), result[i]["OperationID"].ToString());
                if (CreateDetail != null)
                {

                    result[i]["Creator"] = CreateDetail["Creator"];
                    result[i]["CreateTime"] = CreateDetail["CreateTime"];
                    ModifiedDetail = QCS_StaInsSpeSettingService.Qcs00004GetModified(GroupID, InspectionType, result[i]["ProcessID"].ToString(), result[i]["OperationID"].ToString());
                    if (ModifiedDetail != null)
                    {
                        result[i]["Modifier"] = CreateDetail["Modifier"];
                        result[i]["ModifiedTime"] = CreateDetail["ModifiedTime"];
                    }
                }
                else
                {
                    result[i]["IsInspection"] = null;
                }
            }
            return UtilBussinessService.getPaginationModel(result, count);
        }


        /// <summary>
        /// 检验群码专属表头保存
        /// SAM 2017年10月19日15:36:42
        /// </summary>
        /// <param name="Token"></param>
        /// <param name="jArray"></param>
        /// <returns></returns>
        public static object Qcs00004GroupHeaderupdate(string Token, JArray jArray)
        {
            int success = 0;
            int fail = 0;
            string failIDs = null;
            string msg = null;
            string userid = UtilBussinessService.detoken(Token);
            JObject data = null;
            SYS_Parameters ProModel = null;
            SYS_Parameters OpeModel = null;
            SYS_Parameters GroupModel = null;
            bool IsInspection = true;
            for (int i = 0; i < jArray.Count; i++)
            {
                data = (JObject)jArray[i];

                if (string.IsNullOrWhiteSpace(data.Value<string>("IsInspection")))
                {
                    failIDs = UtilBussinessService.str(failIDs, data.Value<string>("ID"));
                    msg = UtilBussinessService.str(msg, data.Value<string>("Sequence") + "是否应检栏位不能为空");
                    fail++;
                    continue;
                }
                try
                {
                    IsInspection = data.Value<bool>("IsInspection");
                }
                catch
                {
                    failIDs = UtilBussinessService.str(failIDs, data.Value<string>("ID"));
                    msg = UtilBussinessService.str(msg, data.Value<string>("Sequence") + "是否应检栏位格式错误");
                    fail++;
                    continue;
                }

                ProModel = SYS_ParameterService.get(data.Value<string>("ProcessID"));
                if (ProModel == null)
                {
                    failIDs = UtilBussinessService.str(failIDs, data.Value<string>("ID"));
                    msg = UtilBussinessService.str(msg, data.Value<string>("Sequence") + "制程流水号错误");
                    fail++;
                    continue;
                }

                GroupModel = SYS_ParameterService.get(data.Value<string>("GroupID"));
                if (GroupModel == null)
                {
                    failIDs = UtilBussinessService.str(failIDs, data.Value<string>("ID"));
                    msg = UtilBussinessService.str(msg, data.Value<string>("Sequence") + "群组流水号错误");
                    fail++;
                    continue;
                }

                string FieldName = null;
                if (data.Value<string>("InspectionType") == Framework.SystemID + "020121300007E")
                    FieldName = "IsIP";
                else if (data.Value<string>("InspectionType") == Framework.SystemID + "0201213000080")
                    FieldName = "IsFPI";
                else if (data.Value<string>("InspectionType") == Framework.SystemID + "0201213000081")
                    FieldName = "IsOSI";

                if (!string.IsNullOrWhiteSpace(data.Value<string>("OperationID")))
                {
                    OpeModel = SYS_ParameterService.get(data.Value<string>("OperationID"));
                    if (OpeModel == null)
                    {
                        failIDs = UtilBussinessService.str(failIDs, data.Value<string>("ID"));
                        msg = UtilBussinessService.str(msg, data.Value<string>("Sequence") + "工序流水号错误");
                        fail++;
                        continue;
                    }
                    if (SFC_ItemOperationService.Qcs04Update(userid, OpeModel.ParameterID, ProModel.ParameterID, GroupModel.ParameterID, FieldName, IsInspection))
                        success++;
                    else
                    {
                        failIDs = UtilBussinessService.str(failIDs, data.Value<string>("ID"));
                        fail++;
                    }
                }
                else
                {
                    if (SFC_ItemProcessService.Qcs04Update(userid, ProModel.ParameterID, GroupModel.ParameterID, FieldName, IsInspection))
                        success++;
                    else
                    {
                        failIDs = UtilBussinessService.str(failIDs, data.Value<string>("ID"));
                        fail++;
                    }
                }
            }
            return new { success = success, fail = fail, failIDs = failIDs, msg = msg };
        }
        #endregion

        #region QCS00005制程检验维护
        ///<summary>
        ///制程检验维护列表
        ///Joint 2017年7月3日16:52:39
        ///</summary>
        ///<param name="Token">授权码</param>
        ///<param name="InspectionNo">检验单号</param>
        ///<param name="Status">状态</param>
        ///<param name="page">页码</param>
        ///<param name="rows">行数</param>
        ///<returns></returns>

        public static object Qcs00005GetList(string Token, string InspectionNo, string Status, int page, int rows)
        {
            int count = 0;
            return UtilBussinessService.getPaginationModel(QCS_InspectionDocumentService.Qcs00005GetList(InspectionNo, Status, page, rows, ref count), count);
        }

        /// <summary>
        /// 制程检验单新增
        /// Joint
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static object Qcs00005Add(JObject data)
        {
            string Token = data.Value<string>("Token");
            string userid = UtilBussinessService.detoken(Token);
            string SysID = Framework.SystemID;
            int success = 0, fill = 0;
            string fillmsg = null;
            string fillmsg2 = null;
            decimal sum = 0;
            try
            {

                if (string.IsNullOrWhiteSpace(data.Value<string>("InspectionNo")))
                    return new { status = "410", msg = "单据编号不能为空！" };
                /*
                //判定方式，先查找对应检验项目判定方式是什么
                MES_Parameter model2 = new MES_Parameter();//创建Inf00020实体
                model2.ParameterID = "100391101213000003";//赋值对应的检验项目判定方式流水号
                model2 = MES_ParameterService.get(model2.ParameterID);//获取对应实体
                MES_Parameter model3 = new MES_Parameter();//品质判定方式
                model3.ParameterID = "100391101213000002";
                model3 = MES_ParameterService.get(model3.ParameterID);
                */

                /*SAM 2017年9月9日23:16:28  调整*/
                MES_Parameter model2 = MES_ParameterService.get(SysID + "1101213000003");//获取对应检验项目判定方式实体
                MES_Parameter model3 = MES_ParameterService.get(SysID + "1101213000002");//获取对应品质判定方式实体

                if (model2 == null)
                    return new { status = "410", msg = "不存在检验项目判定方式的设定，请优先设定" };

                if (model3 == null)
                    return new { status = "410", msg = "不存在品质判定方式的设定，请优先设定" };

                QCS_InspectionDocument model = new QCS_InspectionDocument();
                model.InspectionDocumentID = UniversalService.GetSerialNumber("QCS_InspectionDocument");
                model.InspectionNo = data.Value<string>("InspectionNo");
                if (!string.IsNullOrWhiteSpace(data.Value<string>("DocumentDate")))
                    model.DocumentDate = data.Value<DateTime>("DocumentDate");
                model.InspectionMethod = data.Value<string>("InspectionMethod");
                model.CompletionOrderID = data.Value<string>("CompletionOrderID");
                model.ItemID = data.Value<string>("ItemID");
                model.TaskDispatchID = data.Value<string>("TaskDispatchID");
                if (!string.IsNullOrWhiteSpace(data.Value<string>("InspectionDate")))
                    model.InspectionDate = data.Value<DateTime>("InspectionDate");
                model.InspectionUserID = data.Value<string>("InspectionUserID");
                model.FinQuantity = data.Value<decimal>("FinQuantity");

                if (!string.IsNullOrWhiteSpace(data.Value<string>("InspectionQuantity")) || data.Value<decimal>("InspectionQuantity") < 0)
                {
                    model.InspectionQuantity = data.Value<decimal>("InspectionQuantity");//检验数量？
                }
                else
                {
                    return new { status = "410", msg = "新增失败！检验数量不可为空也不可小于0！" };
                }

                model.ScrappedQuantity = data.Value<decimal>("ScrappedQuantity");//报废数量
                model.NGquantity = data.Value<decimal>("NGquantity");//NG数量
                model.OKQuantity = data.Value<decimal>("OKQuantity");//OK数量
                sum = model.ScrappedQuantity + model.NGquantity + model.OKQuantity;//三个数量加起来是否等于检验数量
                if (sum != model.InspectionQuantity)
                {
                    return new { status = "410", msg = "新增失败！OK数量+NG数量+报废数量不等于检验数量！" };
                }

                model.InspectionFlag = data.Value<bool>("InspectionFlag");
                model.Status = data.Value<string>("Status");
                model.Comments = data.Value<string>("Comments");

                if (model3.Value != Framework.SystemID + "02012130000B1")//如果品质判定方式不为自动，那么由表头带入判定
                {
                    model.QualityControlDecision = data.Value<string>("QcDecision");
                }
                else
                {
                    model.QualityControlDecision = Framework.SystemID + "0201213000091";//自动判定时，默认为允收
                }

                //通过完工单号拿到对应的制程与工序流水号
                SFC_TaskDispatch TdModel = SFC_TaskDispatchService.get(model.TaskDispatchID);
                if (TdModel == null)
                {
                    return new { Status = "400", msg = "对应任务单号不存在！" };
                }
                string AutoNumberID = data.Value<string>("DocumentAutoNumberID");
                //查询所新增的检验单号是否已存在
                while (QCS_InspectionDocumentService.CheckInspectionNo(model.InspectionNo))
                {
                    AutoNumberID = null;
                    model.InspectionNo = UtilBussinessService.GetDocumentAutoNumber(userid, data.Value<string>("DocumentID"), model.InspectionDate.ToString(), ref AutoNumberID);
                }
                if (QCS_InspectionDocumentService.insert(userid, model))
                {
                    UtilBussinessService.UpdateDocumentAutoNumber(userid, AutoNumberID);
                    //如果检验单据成功添加那么根据对应的料品号与检验种类获取明细设定列表，规格表说明不足新添加条件，工序与制程
                    List<QCS_StaInsSpeSetting> StaInsSpeSetting = QCS_StaInsSpeSettingService.GetListByItemID(model.ItemID, model.InspectionMethod, TdModel.ProcessID, TdModel.OperationID);
                    if (StaInsSpeSetting == null)
                        return new { Status = "400", msg = "表头新增成功，但明细新增失败(没有对应的Qcs00004数据)" };
                    //检验明细新增
                    foreach (QCS_StaInsSpeSetting Sta in StaInsSpeSetting)
                    {
                        QCS_InspectionProject Sampling = QCS_InspectionProjectService.get(Sta.InspectionProjectID);
                        if (Sampling == null)
                            return new { Status = "400", msg = "表头新增成功，但明细新增失败(没有对应的检验项目数据)" };
                        QCS_InspectionDocumentDetails DetailsModel = new QCS_InspectionDocumentDetails();
                        DetailsModel.InspectionDocumentDetailID = UniversalService.GetSerialNumber("QCS_InspectionDocumentDetails");
                        DetailsModel.InspectionDocumentID = model.InspectionDocumentID;
                        DetailsModel.Sequence = Sta.Sequence;//排序
                        DetailsModel.InspectionStandard = Sta.InspectionStandard;//检验标准
                        DetailsModel.InspectionItemID = Sta.InspectionProjectID;//检验项目流水号用于获取项目代号与说明
                        DetailsModel.InspectionMethod = Sta.InspectionType;//检验种类
                        DetailsModel.InspectionClassID = Sta.CategoryID;//检验类别
                        DetailsModel.InspectionMethodID = Sta.InspectionMethod;//检验方式
                        DetailsModel.InspectionLevelID = Sampling.InspectionLevel;//检验水准
                        DetailsModel.InspectionFaultID = Sampling.Disadvantages;//缺点等级
                        DetailsModel.Status = Framework.SystemID + "0201213000001";
                        DetailsModel.Aql = Sta.AQL;
                        DetailsModel.AttributeType = Sta.Attribute;
                        //当没有AQL值默认为0
                        if (Sta.AQL == null)
                        {
                            DetailsModel.SampleQuantity = 0;
                            DetailsModel.AcQuantity = 0;
                            DetailsModel.ReQuantity = 0;
                        }
                        //当QCS000001对应的数据没有时，AC,RE,抽样数量设置为0
                        else
                        {
                            QCS_CheckTestSetting Check = QCS_CheckTestSettingService.getByAql(DetailsModel.InspectionLevelID, DetailsModel.InspectionMethodID, Sta.AQL);//获取抽检检验设定实体
                            if (Check == null)
                            {
                                fill++;
                                fillmsg = "部分新增失败，QCS000001没有与检验方式，检验水平，AQL对应的数据，AC,RE，抽样数量将默认为0";
                                DetailsModel.SampleQuantity = 0;
                                DetailsModel.AcQuantity = 0;
                                DetailsModel.ReQuantity = 0;
                            }
                            else
                            {
                                //当QCS000001对应的明细数据没有时，AC,RE,抽样数量设置为0
                                QCS_CheckTestSettingDetails CheckDetail = QCS_CheckTestSettingDetailsService.getCTSDetails(Check.CheckTestSettingID);//获取抽检检验设定明细实体
                                if (CheckDetail == null)
                                {
                                    fill++;
                                    fillmsg2 = "有部分QCS000001明细没有与检验方式，检验水平，AQL对应的明细数据，AC,RE，抽样数量将默认为0";
                                    DetailsModel.SampleQuantity = 0;
                                    DetailsModel.AcQuantity = 0;
                                    DetailsModel.ReQuantity = 0;
                                }
                                //decimal p = model.InspectionQuantity / CheckDetail.EndBatch;//检验数量自动计算？检验数量除以结束批量？
                                //DetailsModel.SampleQuantity = p * CheckDetail.SamplingQuantity;
                                //DetailsModel.AcQuantity = p * CheckDetail.AcQuantity;
                                //DetailsModel.ReQuantity = p * CheckDetail.ReQuantity;
                                //不确定P的计算方式，暂时注释掉
                                else
                                {
                                    DetailsModel.SampleQuantity = CheckDetail.SamplingQuantity;
                                    DetailsModel.AcQuantity = CheckDetail.AcQuantity;
                                    DetailsModel.ReQuantity = CheckDetail.ReQuantity;
                                }
                            }
                        }
                        DetailsModel.NGquantity = 0;//不良数？

                        if (model2.Value == Framework.SystemID + "02012130000B3")//查看系统设置的判定方式
                        {
                            if (DetailsModel.NGquantity >= DetailsModel.ReQuantity)
                                DetailsModel.QualityControlDecision = Framework.SystemID + "0201213000090";
                            else
                                DetailsModel.QualityControlDecision = Framework.SystemID + "0201213000091";
                        }
                        else
                        {
                            DetailsModel.QualityControlDecision = "人工判定";
                        }
                        if (model3.Value == Framework.SystemID + "02012130000B1" && DetailsModel.QualityControlDecision == Framework.SystemID + "0201213000090")
                        {//品质判定为自动且新增的明细判定为拒收
                            model.QualityControlDecision = Framework.SystemID + "0201213000090";//假如系统判定方式为自动，明细有一个为拒收，那么表头也为拒收

                            QCS_InspectionDocumentService.update(userid, model);//更新表头数据
                        }

                        QCS_InspectionDocumentDetailsService.insert(userid, DetailsModel);
                        success++;

                    }


                    //return new { status = "200", msg = "新增成功！" + fillmsg + fillmsg2 + "失败数量为：" + fill, success, fill };
                    return new { status = "200", msg = "新增成功！" };
                }
                else
                    return new { status = "410", msg = "新增失败！" };
            }
            catch (Exception ex)
            {
                DataLogerService.writeerrlog(ex);
                return new { status = "410", msg = "表头新增成功！但明细为空！" };
            }
        }

        /// <summary>
        /// 新增制程检验单
        /// Sam 2017年10月19日11:40:02
        /// 调整了检验单明细的获取逻辑，优先判断群码再料品
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static object Qcs00005AddV1(JObject data)
        {
            string Token = data.Value<string>("Token");
            string userid = UtilBussinessService.detoken(Token);
            string SysID = Framework.SystemID;
            int success = 0, fill = 0;
            string fillmsg = null;
            string fillmsg2 = null;
            decimal sum = 0;
            try
            {
                if (string.IsNullOrWhiteSpace(data.Value<string>("InspectionNo")))
                    return new { status = "410", msg = "单据编号不能为空！" };
                /*
                //判定方式，先查找对应检验项目判定方式是什么
                MES_Parameter model2 = new MES_Parameter();//创建Inf00020实体
                model2.ParameterID = "100391101213000003";//赋值对应的检验项目判定方式流水号
                model2 = MES_ParameterService.get(model2.ParameterID);//获取对应实体
                MES_Parameter model3 = new MES_Parameter();//品质判定方式
                model3.ParameterID = "100391101213000002";
                model3 = MES_ParameterService.get(model3.ParameterID);
                */

                /*SAM 2017年9月9日23:16:28  调整*/
                MES_Parameter model2 = MES_ParameterService.get(SysID + "1101213000003");//获取对应检验项目判定方式实体
                MES_Parameter model3 = MES_ParameterService.get(SysID + "1101213000002");//获取对应品质判定方式实体

                if (model2 == null)
                    return new { status = "410", msg = "不存在检验项目判定方式的设定，请优先设定" };

                if (model3 == null)
                    return new { status = "410", msg = "不存在品质判定方式的设定，请优先设定" };

                QCS_InspectionDocument model = new QCS_InspectionDocument();
                model.InspectionDocumentID = UniversalService.GetSerialNumber("QCS_InspectionDocument");
                model.InspectionNo = data.Value<string>("InspectionNo");
                if (!string.IsNullOrWhiteSpace(data.Value<string>("DocumentDate")))
                    model.DocumentDate = data.Value<DateTime>("DocumentDate");
                model.InspectionMethod = data.Value<string>("InspectionMethod");
                model.CompletionOrderID = data.Value<string>("CompletionOrderID");
                model.ItemID = data.Value<string>("ItemID");
                model.TaskDispatchID = data.Value<string>("TaskDispatchID");
                if (!string.IsNullOrWhiteSpace(data.Value<string>("InspectionDate")))
                    model.InspectionDate = data.Value<DateTime>("InspectionDate");
                model.InspectionUserID = data.Value<string>("InspectionUserID");
                model.FinQuantity = data.Value<decimal>("FinQuantity");

                if (!string.IsNullOrWhiteSpace(data.Value<string>("InspectionQuantity")) || data.Value<decimal>("InspectionQuantity") < 0)
                {
                    model.InspectionQuantity = data.Value<decimal>("InspectionQuantity");//检验数量？
                }
                else
                {
                    return new { status = "410", msg = "新增失败！检验数量不可为空也不可小于0！" };
                }

                model.ScrappedQuantity = data.Value<decimal>("ScrappedQuantity");//报废数量
                model.NGquantity = data.Value<decimal>("NGquantity");//NG数量
                model.OKQuantity = data.Value<decimal>("OKQuantity");//OK数量
                sum = model.ScrappedQuantity + model.NGquantity + model.OKQuantity;//三个数量加起来是否等于检验数量
                if (sum != model.InspectionQuantity)
                {
                    return new { status = "410", msg = "新增失败！OK数量+NG数量+报废数量不等于检验数量！" };
                }

                model.InspectionFlag = data.Value<bool>("InspectionFlag");
                model.Status = data.Value<string>("Status");
                model.Comments = data.Value<string>("Comments");

                if (model3.Value != Framework.SystemID + "02012130000B1")//如果品质判定方式不为自动，那么由表头带入判定
                {
                    model.QualityControlDecision = data.Value<string>("QcDecision");
                }
                else
                {
                    model.QualityControlDecision = Framework.SystemID + "0201213000091";//自动判定时，默认为允收
                }

                //通过完工单号拿到对应的制程与工序流水号
                SFC_TaskDispatch TdModel = SFC_TaskDispatchService.get(model.TaskDispatchID);
                if (TdModel == null)
                {
                    return new { Status = "400", msg = "对应任务单号不存在！" };
                }
                string AutoNumberID = data.Value<string>("DocumentAutoNumberID");
                //查询所新增的检验单号是否已存在
                while (QCS_InspectionDocumentService.CheckInspectionNo(model.InspectionNo))
                {
                    AutoNumberID = null;
                    model.InspectionNo = UtilBussinessService.GetDocumentAutoNumber(userid, data.Value<string>("DocumentID"), model.InspectionDate.ToString(), ref AutoNumberID);
                }
                if (QCS_InspectionDocumentService.insert(userid, model))
                {
                    UtilBussinessService.UpdateDocumentAutoNumber(userid, AutoNumberID);
                    //如果检验单据成功添加那么根据对应的料品号与检验种类获取明细设定列表，规格表说明不足新添加条件，工序与制程

                    SYS_Items ItemModel = SYS_ItemsService.get(model.ItemID);
                    if (ItemModel == null)
                        return new { Status = "400", msg = "对应的料品代号错误" };
                    List<QCS_StaInsSpeSetting> StaInsSpeSetting = null;
                    //如果存在检验码，则拿去检验码的明细
                    if (!string.IsNullOrWhiteSpace(ItemModel.GroupID))
                        StaInsSpeSetting = QCS_StaInsSpeSettingService.GetListByPartID(ItemModel.GroupID, model.InspectionMethod, Framework.SystemID + "020121300007C", TdModel.ProcessID, TdModel.OperationID);
                    //如果不存在检验码或者说检验码并没有对应明细
                    if (StaInsSpeSetting == null)
                        StaInsSpeSetting = QCS_StaInsSpeSettingService.GetListByPartID(model.ItemID, model.InspectionMethod, Framework.SystemID + "020121300007B", TdModel.ProcessID, TdModel.OperationID);

                    if (StaInsSpeSetting == null)
                        return new { Status = "400", msg = "表头新增成功，但明细新增失败(没有对应的Qcs00004数据)" };
                    //检验明细新增
                    foreach (QCS_StaInsSpeSetting Sta in StaInsSpeSetting)
                    {
                        QCS_InspectionProject Sampling = QCS_InspectionProjectService.get(Sta.InspectionProjectID);
                        if (Sampling == null)
                            return new { Status = "400", msg = "表头新增成功，但明细新增失败(没有对应的检验项目数据)" };
                        QCS_InspectionDocumentDetails DetailsModel = new QCS_InspectionDocumentDetails();
                        DetailsModel.InspectionDocumentDetailID = UniversalService.GetSerialNumber("QCS_InspectionDocumentDetails");
                        DetailsModel.InspectionDocumentID = model.InspectionDocumentID;
                        DetailsModel.Sequence = Sta.Sequence;//排序
                        DetailsModel.InspectionStandard = Sta.InspectionStandard;//检验标准
                        DetailsModel.InspectionItemID = Sta.InspectionProjectID;//检验项目流水号用于获取项目代号与说明
                        DetailsModel.InspectionMethod = Sta.InspectionType;//检验种类
                        DetailsModel.InspectionClassID = Sta.CategoryID;//检验类别
                        DetailsModel.InspectionMethodID = Sta.InspectionMethod;//检验方式
                        DetailsModel.InspectionLevelID = Sampling.InspectionLevel;//检验水准
                        DetailsModel.InspectionFaultID = Sampling.Disadvantages;//缺点等级
                        DetailsModel.Status = Framework.SystemID + "0201213000001";
                        DetailsModel.Aql = Sta.AQL;
                        DetailsModel.AttributeType = Sta.Attribute;
                        //当没有AQL值默认为0
                        //TODO AQL没值就不用生成明细了吧？
                        if (Sta.AQL == null)
                        {
                            DetailsModel.SampleQuantity = 0;
                            DetailsModel.AcQuantity = 0;
                            DetailsModel.ReQuantity = 0;
                        }
                        //当QCS000001对应的数据没有时，AC,RE,抽样数量设置为0
                        else
                        {
                            QCS_CheckTestSetting Check = QCS_CheckTestSettingService.getByAql(DetailsModel.InspectionLevelID, DetailsModel.InspectionMethodID, Sta.AQL);//获取抽检检验设定实体
                            if (Check == null)
                            {
                                fill++;
                                fillmsg = "部分新增失败，QCS000001没有与检验方式，检验水平，AQL对应的数据，AC,RE，抽样数量将默认为0";
                                DetailsModel.SampleQuantity = 0;
                                DetailsModel.AcQuantity = 0;
                                DetailsModel.ReQuantity = 0;
                            }
                            else
                            {
                                //当QCS000001对应的明细数据没有时，AC,RE,抽样数量设置为0

                                /*需要根据任务单的分派数量，找到对应的区间，获取数据*/
                                QCS_CheckTestSettingDetails CheckDetail = QCS_CheckTestSettingDetailsService.GetDetails(Check.CheckTestSettingID, TdModel.DispatchQuantity);//获取抽检检验设定明细实体
                                if (CheckDetail == null)
                                {
                                    fill++;
                                    fillmsg2 = "有部分QCS000001明细没有与检验方式，检验水平，AQL对应的明细数据，AC,RE，抽样数量将默认为0";
                                    DetailsModel.SampleQuantity = 0;
                                    DetailsModel.AcQuantity = 0;
                                    DetailsModel.ReQuantity = 0;
                                }
                                //decimal p = model.InspectionQuantity / CheckDetail.EndBatch;//检验数量自动计算？检验数量除以结束批量？
                                //DetailsModel.SampleQuantity = p * CheckDetail.SamplingQuantity;
                                //DetailsModel.AcQuantity = p * CheckDetail.AcQuantity;
                                //DetailsModel.ReQuantity = p * CheckDetail.ReQuantity;
                                //不确定P的计算方式，暂时注释掉
                                else
                                {
                                    DetailsModel.SampleQuantity = CheckDetail.SamplingQuantity;
                                    DetailsModel.AcQuantity = CheckDetail.AcQuantity;
                                    DetailsModel.ReQuantity = CheckDetail.ReQuantity;
                                }
                            }
                        }
                        DetailsModel.NGquantity = 0;//不良数？

                        if (model2.Value == Framework.SystemID + "02012130000B3")//查看系统设置的判定方式
                        {
                            if (DetailsModel.NGquantity >= DetailsModel.ReQuantity)
                                DetailsModel.QualityControlDecision = Framework.SystemID + "0201213000090";
                            else
                                DetailsModel.QualityControlDecision = Framework.SystemID + "0201213000091";
                        }
                        else
                        {
                            DetailsModel.QualityControlDecision = "人工判定";
                        }
                        if (model3.Value == Framework.SystemID + "02012130000B1" && DetailsModel.QualityControlDecision == Framework.SystemID + "0201213000090")
                        {//品质判定为自动且新增的明细判定为拒收
                            model.QualityControlDecision = Framework.SystemID + "0201213000090";//假如系统判定方式为自动，明细有一个为拒收，那么表头也为拒收

                            QCS_InspectionDocumentService.update(userid, model);//更新表头数据
                        }

                        QCS_InspectionDocumentDetailsService.insert(userid, DetailsModel);
                        success++;

                    }
                    return new { status = "200", msg = "新增成功！" };
                }
                else
                    return new { status = "410", msg = "新增失败！" };
            }
            catch (Exception ex)
            {
                DataLogerService.writeerrlog(ex);
                return new { status = "410", msg = "表头新增成功！但明细为空！" };
            }
        }

        /// <summary>
        /// 新增制程检验单V2版本
        /// Sam 2017年10月23日17:34:48
        /// 在V1的版本上调整了部分逻辑。在寻找抽样检验设定资料时，加多了分派量是否在范围内的判断
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static object Qcs00005AddV2(JObject data)
        {
            string Token = data.Value<string>("Token");
            string userid = UtilBussinessService.detoken(Token);
            string SysID = Framework.SystemID;
            decimal sum = 0;
            try
            {
                if (string.IsNullOrWhiteSpace(data.Value<string>("InspectionNo")))
                    return new { status = "410", msg = "新增失败！检验单号不能为空！" };

                //判定方式，先查找对应检验项目判定方式是什么
                /*SAM 2017年9月9日23:16:28  调整*/
                MES_Parameter model2 = MES_ParameterService.get(SysID + "1101213000003");//获取对应检验项目判定方式实体
                MES_Parameter model3 = MES_ParameterService.get(SysID + "1101213000002");//获取对应品质判定方式实体

                if (model2 == null)
                    return new { status = "410", msg = "新增失败！不存在检验项目判定方式的设定，请优先设定" };

                if (model3 == null)
                    return new { status = "410", msg = "新增失败！不存在品质判定方式的设定，请优先设定" };

                QCS_InspectionDocument model = new QCS_InspectionDocument();
                model.InspectionDocumentID = UniversalService.GetSerialNumber("QCS_InspectionDocument");
                model.InspectionNo = data.Value<string>("InspectionNo");
                if (!string.IsNullOrWhiteSpace(data.Value<string>("DocumentDate")))
                    model.DocumentDate = data.Value<DateTime>("DocumentDate");
                model.InspectionMethod = data.Value<string>("InspectionMethod");
                model.CompletionOrderID = data.Value<string>("CompletionOrderID");
                model.ItemID = data.Value<string>("ItemID");
                model.TaskDispatchID = data.Value<string>("TaskDispatchID");
                if (!string.IsNullOrWhiteSpace(data.Value<string>("InspectionDate")))
                    model.InspectionDate = data.Value<DateTime>("InspectionDate");
                model.InspectionUserID = data.Value<string>("InspectionUserID");
                model.FinQuantity = data.Value<decimal>("FinQuantity");

                if (string.IsNullOrWhiteSpace(data.Value<string>("InspectionQuantity")))
                    return new { status = "410", msg = "新增失败！检验数量不能为空！" };

                if (data.Value<decimal>("InspectionQuantity") < 0)
                    return new { status = "410", msg = "新增失败！检验数量不不可小于0！" };

                model.InspectionQuantity = data.Value<decimal>("InspectionQuantity");//检验数量
                model.ScrappedQuantity = data.Value<decimal>("ScrappedQuantity");//报废数量
                model.NGquantity = data.Value<decimal>("NGquantity");//NG数量
                model.OKQuantity = data.Value<decimal>("OKQuantity");//OK数量
                sum = model.ScrappedQuantity + model.NGquantity + model.OKQuantity;//三个数量加起来是否等于检验数量
                if (sum != model.InspectionQuantity)
                {
                    return new { status = "410", msg = "新增失败！OK数量+NG数量+报废数量不等于检验数量！" };
                }

                model.InspectionFlag = data.Value<bool>("InspectionFlag");
                model.Status = data.Value<string>("Status");
                model.Comments = data.Value<string>("Comments");

                if (model3.Value != Framework.SystemID + "02012130000B1")//如果品质判定方式不为自动，那么由表头带入判定
                {
                    model.QualityControlDecision = data.Value<string>("QcDecision");
                }
                else
                {
                    model.QualityControlDecision = Framework.SystemID + "0201213000091";//自动判定时，默认为允收
                }

                //通过任务单号获取对应的任务单实体
                SFC_TaskDispatch TdModel = SFC_TaskDispatchService.get(model.TaskDispatchID);
                if (TdModel == null)
                {
                    return new { Status = "400", msg = "对应任务单信息不存在！" };
                }
                string AutoNumberID = data.Value<string>("DocumentAutoNumberID");
                //查询所新增的检验单号是否已存在
                while (QCS_InspectionDocumentService.CheckInspectionNo(model.InspectionNo))
                {
                    AutoNumberID = null;
                    model.InspectionNo = UtilBussinessService.GetDocumentAutoNumber(userid, data.Value<string>("DocumentID"), model.InspectionDate.ToString(), ref AutoNumberID);
                }
                if (QCS_InspectionDocumentService.insert(userid, model))
                {
                    UtilBussinessService.UpdateDocumentAutoNumber(userid, AutoNumberID);
                    //如果检验单据成功添加那么根据对应的料品号与检验种类获取明细设定列表，规格表说明不足新添加条件，工序与制程

                    SYS_Items ItemModel = SYS_ItemsService.get(model.ItemID);
                    if (ItemModel == null)
                        return new { Status = "400", msg = "对应的料品代号错误" };

                    List<QCS_StaInsSpeSetting> StaInsSpeSetting = null;
                    //如果存在检验码，则拿去检验码的明细
                    if (!string.IsNullOrWhiteSpace(ItemModel.GroupID))
                        StaInsSpeSetting = QCS_StaInsSpeSettingService.GetListByPartID(ItemModel.GroupID, model.InspectionMethod, Framework.SystemID + "020121300007C", TdModel.ProcessID, TdModel.OperationID);
                    //如果不存在检验码或者说检验码并没有对应明细
                    if (StaInsSpeSetting == null)
                        StaInsSpeSetting = QCS_StaInsSpeSettingService.GetListByPartID(model.ItemID, model.InspectionMethod, Framework.SystemID + "020121300007B", TdModel.ProcessID, TdModel.OperationID);

                    if (StaInsSpeSetting == null)
                        return new { Status = "400", msg = "表头新增成功，但明细新增失败(没有对应的Qcs00004数据)" };
                    //检验明细新增
                    foreach (QCS_StaInsSpeSetting Sta in StaInsSpeSetting)
                    {
                        QCS_InspectionProject Sampling = QCS_InspectionProjectService.get(Sta.InspectionProjectID);
                        if (Sampling == null)
                            return new { Status = "400", msg = "表头新增成功，但明细新增失败(没有对应的检验项目数据)" };
                        QCS_InspectionDocumentDetails DetailsModel = new QCS_InspectionDocumentDetails();
                        DetailsModel.InspectionDocumentDetailID = UniversalService.GetSerialNumber("QCS_InspectionDocumentDetails");
                        DetailsModel.InspectionDocumentID = model.InspectionDocumentID;
                        DetailsModel.Sequence = Sta.Sequence;//排序
                        DetailsModel.InspectionStandard = Sta.InspectionStandard;//检验标准
                        DetailsModel.InspectionItemID = Sta.InspectionProjectID;//检验项目流水号用于获取项目代号与说明
                        DetailsModel.InspectionMethod = Sta.InspectionType;//检验种类
                        DetailsModel.InspectionClassID = Sta.CategoryID;//检验类别
                        DetailsModel.InspectionMethodID = Sta.InspectionMethod;//检验方式
                        DetailsModel.InspectionLevelID = Sampling.InspectionLevel;//检验水准
                        DetailsModel.InspectionFaultID = Sampling.Disadvantages;//缺点等级
                        DetailsModel.Status = Framework.SystemID + "0201213000001";
                        DetailsModel.Aql = Sta.AQL;
                        DetailsModel.AttributeType = Sta.Attribute;
                        //当没有AQL值默认为0
                        //TODO AQL没值就不用生成明细了吧？
                        if (Sta.AQL == null)
                        {
                            DetailsModel.SampleQuantity = 0;
                            DetailsModel.AcQuantity = 0;
                            DetailsModel.ReQuantity = 0;
                        }
                        //当QCS000001对应的数据没有时，AC,RE,抽样数量设置为0
                        else
                        {
                            QCS_CheckTestSetting Check = QCS_CheckTestSettingService.getByAql(DetailsModel.InspectionLevelID, DetailsModel.InspectionMethodID, Sta.AQL);//获取抽检检验设定实体
                            if (Check == null)
                            {
                                //TODO
                                //fill++;
                                //fillmsg = "部分新增失败，QCS000001没有与检验方式，检验水平，AQL对应的数据，AC,RE，抽样数量将默认为0";
                                DetailsModel.SampleQuantity = 0;
                                DetailsModel.AcQuantity = 0;
                                DetailsModel.ReQuantity = 0;
                            }
                            else
                            {
                                //当QCS000001对应的明细数据没有时，AC,RE,抽样数量设置为0

                                /*需要根据任务单的分派数量，找到对应的区间，获取数据*/
                                //QCS_CheckTestSettingDetails CheckDetail = QCS_CheckTestSettingDetailsService.getCTSDetails(Check.CheckTestSettingID);//获取抽检检验设定明细实体
                                QCS_CheckTestSettingDetails CheckDetail = QCS_CheckTestSettingDetailsService.GetDetails(Check.CheckTestSettingID, TdModel.DispatchQuantity);//获取抽检检验设定明细实体
                                if (CheckDetail == null)
                                {
                                    //TODO
                                    //fill++;
                                    //fillmsg2 = "有部分QCS000001明细没有与检验方式，检验水平，AQL对应的明细数据，AC,RE，抽样数量将默认为0";
                                    DetailsModel.SampleQuantity = 0;
                                    DetailsModel.AcQuantity = 0;
                                    DetailsModel.ReQuantity = 0;
                                }
                                //decimal p = model.InspectionQuantity / CheckDetail.EndBatch;//检验数量自动计算？检验数量除以结束批量？
                                //DetailsModel.SampleQuantity = p * CheckDetail.SamplingQuantity;
                                //DetailsModel.AcQuantity = p * CheckDetail.AcQuantity;
                                //DetailsModel.ReQuantity = p * CheckDetail.ReQuantity;
                                //不确定P的计算方式，暂时注释掉
                                else
                                {
                                    DetailsModel.SampleQuantity = CheckDetail.SamplingQuantity;
                                    DetailsModel.AcQuantity = CheckDetail.AcQuantity;
                                    DetailsModel.ReQuantity = CheckDetail.ReQuantity;
                                }
                            }
                        }
                        DetailsModel.NGquantity = 0;//不良数？

                        if (model2.Value == Framework.SystemID + "02012130000B3")//查看系统设置的判定方式
                        {
                            if (DetailsModel.NGquantity >= DetailsModel.ReQuantity)
                                DetailsModel.QualityControlDecision = Framework.SystemID + "0201213000090";
                            else
                                DetailsModel.QualityControlDecision = Framework.SystemID + "0201213000091";
                        }
                        else
                        {
                            DetailsModel.QualityControlDecision = "人工判定";
                        }
                        if (model3.Value == Framework.SystemID + "02012130000B1" && DetailsModel.QualityControlDecision == Framework.SystemID + "0201213000090")
                        {//品质判定为自动且新增的明细判定为拒收
                            model.QualityControlDecision = Framework.SystemID + "0201213000090";//假如系统判定方式为自动，明细有一个为拒收，那么表头也为拒收

                            QCS_InspectionDocumentService.update(userid, model);//更新表头数据
                        }

                        QCS_InspectionDocumentDetailsService.insert(userid, DetailsModel);
                    }
                    return new { status = "200", msg = "新增成功！" };
                }
                else
                    return new { status = "410", msg = "新增失败！" };
            }
            catch (Exception ex)
            {
                DataLogerService.writeerrlog(ex);
                return new { status = "410", msg = "表头新增成功！但明细为空！" };
            }
        }

        /// <summary>
        /// 新增制程检验单V3版本
        /// Sam 2017年10月23日17:34:48
        /// 在V2的版本上调整了部分代码，以及修改了一个bug:明细的获取错误。
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static object Qcs00005AddV3(JObject data)
        {
            string Token = data.Value<string>("Token");
            string userid = UtilBussinessService.detoken(Token);
            string SysID = Framework.SystemID;
            decimal sum = 0;
            try
            {
                if (string.IsNullOrWhiteSpace(data.Value<string>("InspectionNo")))
                    return new { status = "410", msg = "新增失败！检验单号不能为空！" };

                //判定方式，先查找对应检验项目判定方式是什么
                /*SAM 2017年9月9日23:16:28  调整*/
                MES_Parameter model2 = MES_ParameterService.get(SysID + "1101213000003");//获取对应检验项目判定方式实体
                MES_Parameter model3 = MES_ParameterService.get(SysID + "1101213000002");//获取对应品质判定方式实体

                if (model2 == null)
                    return new { status = "410", msg = "新增失败！不存在检验项目判定方式的设定，请优先设定" };

                if (model3 == null)
                    return new { status = "410", msg = "新增失败！不存在品质判定方式的设定，请优先设定" };

                QCS_InspectionDocument model = new QCS_InspectionDocument();
                model.InspectionDocumentID = UniversalService.GetSerialNumber("QCS_InspectionDocument");
                model.InspectionNo = data.Value<string>("InspectionNo");
                if (!string.IsNullOrWhiteSpace(data.Value<string>("DocumentDate")))
                    model.DocumentDate = data.Value<DateTime>("DocumentDate");
                model.InspectionMethod = data.Value<string>("InspectionMethod");
                model.CompletionOrderID = data.Value<string>("CompletionOrderID");
                model.ItemID = data.Value<string>("ItemID");
                model.TaskDispatchID = data.Value<string>("TaskDispatchID");
                if (!string.IsNullOrWhiteSpace(data.Value<string>("InspectionDate")))
                    model.InspectionDate = data.Value<DateTime>("InspectionDate");
                model.InspectionUserID = data.Value<string>("InspectionUserID");
                model.FinQuantity = data.Value<decimal>("FinQuantity");

                if (string.IsNullOrWhiteSpace(data.Value<string>("InspectionQuantity")))
                    return new { status = "410", msg = "新增失败！检验数量不能为空！" };

                if (data.Value<decimal>("InspectionQuantity") < 0)
                    return new { status = "410", msg = "新增失败！检验数量不不可小于0！" };

                model.InspectionQuantity = data.Value<decimal>("InspectionQuantity");//检验数量
                model.ScrappedQuantity = data.Value<decimal>("ScrappedQuantity");//报废数量
                model.NGquantity = data.Value<decimal>("NGquantity");//NG数量
                model.OKQuantity = data.Value<decimal>("OKQuantity");//OK数量
                sum = model.ScrappedQuantity + model.NGquantity + model.OKQuantity;//三个数量加起来是否等于检验数量
                if (sum != model.InspectionQuantity)
                {
                    return new { status = "410", msg = "新增失败！OK数量+NG数量+报废数量不等于检验数量！" };
                }

                model.InspectionFlag = data.Value<bool>("InspectionFlag");
                model.Status = data.Value<string>("Status");
                model.Comments = data.Value<string>("Comments");

                if (model3.Value != Framework.SystemID + "02012130000B1")//如果品质判定方式不为自动，那么由表头带入判定
                {
                    model.QualityControlDecision = Framework.SystemID + "0201213000091";//需求修改默认为允收
                }
                else
                {
                    model.QualityControlDecision = Framework.SystemID + "0201213000091";//自动判定时，默认为允收
                }

                //通过任务单号获取对应的任务单实体
                SFC_TaskDispatch TdModel = SFC_TaskDispatchService.get(model.TaskDispatchID);
                if (TdModel == null)
                {
                    return new { status = "410", msg = "对应任务单信息不存在！" };
                }
                string AutoNumberID = data.Value<string>("DocumentAutoNumberID");

                /*当保存时,如果检验单号已存在，则自动获取下一检验单号*/
                /*SAM 2017年10月27日16:36:36*/
                /*
                 * 因为存在一个情况是检验单号新增进去了但是并没有及时更新流水，造成了无限死循环。
                 * 所以目前做了一个机制，循环计数，如果循环超过5次，就更新一次流水,然后重置循环次数
                 */
                int Seq = 1;
                while (QCS_InspectionDocumentService.CheckInspectionNo(model.InspectionNo))
                {
                    if (Seq == 5)
                    {
                        UtilBussinessService.UpdateDocumentAutoNumber(userid, AutoNumberID);
                        Seq = 1;
                    }
                    else
                    {
                        AutoNumberID = null;
                        model.InspectionNo = UtilBussinessService.GetDocumentAutoNumber(userid, data.Value<string>("DocumentID"), model.InspectionDate.ToString(), ref AutoNumberID);
                        Seq++;
                    }
                }

                if (QCS_InspectionDocumentService.insert(userid, model))
                {
                    UtilBussinessService.UpdateDocumentAutoNumber(userid, AutoNumberID);
                    //如果检验单据成功添加那么根据对应的料品号与检验种类获取明细设定列表，规格表说明不足新添加条件，工序与制程

                    SYS_Items ItemModel = SYS_ItemsService.get(model.ItemID);
                    if (ItemModel == null)
                        return new { status = "400", msg = "对应的料品代号错误" };

                    List<QCS_StaInsSpeSetting> StaInsSpeSetting = null;
                    //如果存在检验码，则拿去检验码的明细
                    if (!string.IsNullOrWhiteSpace(ItemModel.GroupID))
                        StaInsSpeSetting = QCS_StaInsSpeSettingService.GetListByPartID(ItemModel.GroupID, model.InspectionMethod, Framework.SystemID + "020121300007C", TdModel.ProcessID, TdModel.OperationID);
                    //如果不存在检验码或者说检验码并没有对应明细
                    if (StaInsSpeSetting == null)
                        StaInsSpeSetting = QCS_StaInsSpeSettingService.GetListByPartID(model.ItemID, model.InspectionMethod, Framework.SystemID + "020121300007B", TdModel.ProcessID, TdModel.OperationID);

                    if (StaInsSpeSetting == null)
                        return new { status = "400", msg = "表头新增成功，但明细新增失败(没有对应的Qcs00004数据)" };
                    //检验明细新增
                    foreach (QCS_StaInsSpeSetting Sta in StaInsSpeSetting)
                    {
                        QCS_InspectionProject Sampling = QCS_InspectionProjectService.get(Sta.InspectionProjectID);
                        if (Sampling == null)
                            return new { status = "400", msg = "表头新增成功，但明细新增失败(没有对应的检验项目数据)" };
                        QCS_InspectionDocumentDetails DetailsModel = new QCS_InspectionDocumentDetails();
                        DetailsModel.InspectionDocumentDetailID = UniversalService.GetSerialNumber("QCS_InspectionDocumentDetails");
                        DetailsModel.InspectionDocumentID = model.InspectionDocumentID;
                        DetailsModel.Sequence = Sta.Sequence;//排序
                        DetailsModel.InspectionStandard = Sta.InspectionStandard;//检验标准
                        DetailsModel.InspectionItemID = Sta.InspectionProjectID;//检验项目流水号用于获取项目代号与说明
                        DetailsModel.InspectionMethod = Sta.InspectionType;//检验种类
                        DetailsModel.InspectionClassID = Sta.CategoryID;//检验类别
                        DetailsModel.InspectionMethodID = Sta.InspectionMethod;//检验方式
                        DetailsModel.InspectionLevelID = Sampling.InspectionLevel;//检验水准
                        DetailsModel.InspectionFaultID = Sampling.Disadvantages;//缺点等级
                        DetailsModel.Status = Framework.SystemID + "0201213000001";
                        DetailsModel.Aql = Sta.AQL;
                        DetailsModel.AttributeType = Sta.Attribute;
                        //当没有AQL值默认为0
                        //TODO AQL没值就不用生成明细了吧？
                        if (Sta.AQL == null)
                        {
                            DetailsModel.SampleQuantity = 0;
                            DetailsModel.AcQuantity = 0;
                            DetailsModel.ReQuantity = 0;
                        }
                        //当QCS000001对应的数据没有时，AC,RE,抽样数量设置为0
                        else
                        {
                            QCS_CheckTestSetting Check = QCS_CheckTestSettingService.getByAql(DetailsModel.InspectionLevelID, DetailsModel.InspectionMethodID, Sta.AQL);//获取抽检检验设定实体
                            if (Check == null)
                            {
                                DetailsModel.SampleQuantity = 0;
                                DetailsModel.AcQuantity = 0;
                                DetailsModel.ReQuantity = 0;
                            }
                            else
                            {
                                //当QCS000001对应的明细数据没有时，AC,RE,抽样数量设置为0
                                /*需要根据任务单的分派数量，找到对应的区间，获取数据*/
                                QCS_CheckTestSettingDetails CheckDetail = QCS_CheckTestSettingDetailsService.GetDetails(Check.CheckTestSettingID, TdModel.DispatchQuantity);//获取抽检检验设定明细实体
                                if (CheckDetail == null)
                                {
                                    DetailsModel.SampleQuantity = 0;
                                    DetailsModel.AcQuantity = 0;
                                    DetailsModel.ReQuantity = 0;
                                }
                                else
                                {
                                    DetailsModel.SampleQuantity = CheckDetail.SamplingQuantity;
                                    DetailsModel.AcQuantity = CheckDetail.AcQuantity;
                                    DetailsModel.ReQuantity = CheckDetail.ReQuantity;
                                }
                            }
                        }
                        DetailsModel.NGquantity = 0;//不良数？

                        if (model2.Value == Framework.SystemID + "02012130000B3")//查看系统设置的判定方式
                        {
                            if (DetailsModel.NGquantity >= DetailsModel.ReQuantity)
                                DetailsModel.QualityControlDecision = Framework.SystemID + "0201213000090";
                            else
                                DetailsModel.QualityControlDecision = Framework.SystemID + "0201213000091";
                        }
                        else
                        {
                            DetailsModel.QualityControlDecision = Framework.SystemID + "0201213000091";//人工判定时默认为允收
                        }
                        if (model3.Value == Framework.SystemID + "02012130000B1" && DetailsModel.QualityControlDecision == Framework.SystemID + "0201213000090")
                        {//品质判定为自动且新增的明细判定为拒收
                            model.QualityControlDecision = Framework.SystemID + "0201213000090";//假如系统判定方式为自动，明细有一个为拒收，那么表头也为拒收

                            QCS_InspectionDocumentService.update(userid, model);//更新表头数据
                        }

                        QCS_InspectionDocumentDetailsService.insert(userid, DetailsModel);
                    }
                    return new { status = "200", msg = "新增成功！" };
                }
                else
                    return new { status = "410", msg = "新增失败！" };
            }
            catch (Exception ex)
            {
                DataLogerService.writeerrlog(ex);
                return new { status = "410", msg = "表头新增成功！但明细为空！" };
            }
        }

        /// <summary>
        /// 制程检验单表头—更新
        /// Joint
        /// </summary>
        /// <param name="Token"></param>
        /// <param name="jArray"></param>
        /// <returns></returns>
        public static object Qcs00005update(string Token, JArray jArray)
        {
            int success = 0;
            int fail = 0;
            string failIDs = null;
            string msg = null;
            string userid = UtilBussinessService.detoken(Token);
            JObject data = null;
            QCS_InspectionDocument model = null;
            string SysId = Framework.SystemID;
            for (int i = 0; i < jArray.Count; i++)
            {

                data = (JObject)jArray[i];
                model = QCS_InspectionDocumentService.get(data.Value<string>("InspectionDocumentID"));
                if (model.Status == SysId + "020121300008E" || model.Status == SysId + "020121300008F")//状态为CA确认,CL作废时不可新增修改
                {

                    failIDs = UtilBussinessService.str(failIDs, data.Value<string>("InspectionDocumentID"));
                    msg = UtilBussinessService.str(msg, model.InspectionNo + "状态为CA确认或CL作废,不能修改");
                    fail++;
                    continue;
                }
                model.InspectionDocumentID = data.Value<string>("InspectionDocumentID");
                if (!string.IsNullOrWhiteSpace(data.Value<string>("InspectionDate")))
                    model.DocumentDate = data.Value<DateTime>("DocumentDate");
                model.InspectionMethod = data.Value<string>("InspectionMethod");
                model.CompletionOrderID = data.Value<string>("FinishID");
                model.ItemID = data.Value<string>("ItemID");
                model.TaskDispatchID = data.Value<string>("TaskDispatchID");
                if (!string.IsNullOrWhiteSpace(data.Value<string>("InspectionDate")))
                    model.InspectionDate = data.Value<DateTime>("InspectionDate");
                model.InspectionUserID = data.Value<string>("InspectionUserID");
                model.QualityControlDecision = data.Value<string>("QcDecision");
                //防止前端传空值报错，为空时默认为0
                if (string.IsNullOrWhiteSpace(data.Value<string>("FinQuantity")))
                {
                    model.FinQuantity = 0;
                }
                else
                {
                    model.FinQuantity = data.Value<decimal>("FinQuantity");
                }
                //防止前端传空值报错，为空时默认为0
                if (string.IsNullOrWhiteSpace(data.Value<string>("InspectionQuantity")))
                {
                    model.InspectionQuantity = 0;
                }
                else
                {
                    model.InspectionQuantity = data.Value<decimal>("InspectionQuantity");
                }
                //防止前端传空值报错，为空时默认为0
                if (string.IsNullOrWhiteSpace(data.Value<string>("ScrappedQuantity")))
                {
                    model.ScrappedQuantity = 0;
                }
                else
                {
                    model.ScrappedQuantity = data.Value<decimal>("ScrappedQuantity");
                }
                //防止前端传空值报错，为空时默认为0
                if (string.IsNullOrWhiteSpace(data.Value<string>("NGquantity")))
                {
                    model.NGquantity = 0;
                }
                else
                {
                    model.NGquantity = data.Value<decimal>("NGquantity");
                }
                //防止前端传空值报错，为空时默认为0
                if (string.IsNullOrWhiteSpace(data.Value<string>("OKQuantity")))
                {
                    model.OKQuantity = 0;
                }
                else
                {
                    model.OKQuantity = data.Value<decimal>("OKQuantity");
                }
                model.InspectionFlag = data.Value<bool>("InspectionFlag");
                model.Status = data.Value<string>("Status");
                model.Comments = data.Value<string>("Comments");
                if (QCS_InspectionDocumentService.update(userid, model))
                    success++;
                else
                {
                    failIDs = UtilBussinessService.str(failIDs, data.Value<string>("InspectionDocumentID"));
                    fail++;
                }

            }
            return new { success = success, fail = fail, failIDs = failIDs, msg = msg };
        }


        /// <summary>
        /// QCS00005 制程检验更新
        /// Alvin 2017年9月11日14:46:44
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public static object Qcs00005UpdateByOne(JObject request)
        {
            string Token = request.Value<string>("Token");//获取传进来的token
            string userid = UtilBussinessService.detoken(Token);//获取登录人的流水号
            string SysId = Framework.SystemID;//系统代号
            QCS_InspectionDocument model = null;//初始化model
            try
            {
                if (string.IsNullOrWhiteSpace(request.Value<string>("InspectionDocumentID")))
                {
                    return new { status = "410", msg = "检验单据流水号不能为空！" };
                }

                model = QCS_InspectionDocumentService.get(request.Value<string>("InspectionDocumentID"));
                if (model.Status == SysId + "020121300008E" || model.Status == SysId + "020121300008F")//状态为CA确认,CL作废时不可修改
                {
                    return new { status = "410", msg = "状态为CA确认或CL作废,不能修改！" };
                }

                model.InspectionDocumentID = request.Value<string>("InspectionDocumentID");

                if (!string.IsNullOrWhiteSpace(request.Value<string>("DocumentDate")))
                    model.DocumentDate = request.Value<DateTime>("DocumentDate");//单据日期

                if (!string.IsNullOrWhiteSpace(request.Value<string>("InspectionDate")))
                    model.InspectionDate = request.Value<DateTime>("InspectionDate");//检验日期

                model.InspectionUserID = request.Value<string>("InspectionUserID");//检验人员
                model.QualityControlDecision = request.Value<string>("QcDecision");//品質判定
                model.InspectionQuantity = request.Value<decimal>("InspectionQuantity");//检验数量
                model.ScrappedQuantity = request.Value<decimal>("ScrappedQuantity");//報廢數量
                model.NGquantity = request.Value<decimal>("NGquantity");//NG數量
                model.InspectionFlag = request.Value<bool>("InspectionFlag");//检验注记
                model.Comments = request.Value<string>("Comments");//備註

                if (QCS_InspectionDocumentService.updateByOne(userid, model))
                {
                    return new { status = "200", msg = "修改成功！" };
                }
                else
                {
                    return new { status = "410", msg = "修改失败！" };
                }
            }
            catch (Exception ex)
            {
                DataLogerService.writeerrlog(ex);
                return new { status = "410", msg = "修改失败！" + ex.ToString() };
            }
        }



        /// <summary>
        /// 制程检验单明细表
        /// Joint
        /// </summary>
        /// <param name="Token"></param>
        /// <param name="InspectionDocumentID"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <returns></returns>
        public static object Qcs00005GetDetailsList(string Token, string InspectionDocumentID, int page, int rows)
        {
            int count = 0;
            return UtilBussinessService.getPaginationModel(QCS_InspectionDocumentDetailsService.Qcs00005GetDetailsList(InspectionDocumentID, page, rows, ref count), count);
        }


        /// <summary>
        /// 制程检验单明细更新
        /// Joint
        /// </summary>
        /// <param name="Token"></param>
        /// <param name="jArray"></param>
        /// <returns></returns>
        public static object Qcs00005DetailUpdate(string Token, JArray jArray)
        {
            int success = 0;
            int fail = 0;
            string failIDs = null;
            string msg = null;
            string userid = UtilBussinessService.detoken(Token);
            JObject data = null;
            QCS_InspectionDocumentDetails model = null;
            QCS_InspectionDocument Headermodel = null;
            string SysId = Framework.SystemID;
            for (int i = 0; i < jArray.Count; i++)
            {
                data = (JObject)jArray[i];
                Headermodel = QCS_InspectionDocumentService.get(data.Value<string>("InspectionDocumentID"));
                if (Headermodel == null)
                {
                    failIDs = UtilBussinessService.str(failIDs, data.Value<string>("InspectionDocumentID"));
                    msg = UtilBussinessService.str(msg, data.Value<string>("Sequence") + "的表头流水号为空");
                    fail++;
                    continue;
                }
                if (Headermodel.ItemID == null)
                {
                    failIDs = UtilBussinessService.str(failIDs, data.Value<string>("InspectionDocumentID"));
                    msg = UtilBussinessService.str(msg, data.Value<string>("Sequence") + data.Value<string>("InspectionDocumentID") + "的料品流水号为空");
                    fail++;
                    continue;
                }
                if (Headermodel.Status == SysId + "020121300008E" || Headermodel.Status == SysId + "020121300008F")//状态为CA确认,CL作废时不可新增修改
                {

                    failIDs = UtilBussinessService.str(failIDs, data.Value<string>("InspectionNo"));
                    msg = UtilBussinessService.str(msg, Headermodel.InspectionNo + "状态为CA确认或CL作废,不能修改");
                    fail++;
                    continue;
                }
                model = new QCS_InspectionDocumentDetails();
                model.InspectionDocumentDetailID = data.Value<string>("InspectionDocumentDetailID");
                model.InspectionDocumentID = data.Value<string>("InspectionDocumentID");
                model.Sequence = string.IsNullOrWhiteSpace(data.Value<string>("Sequence")) ? 0 : data.Value<int>("Sequence");
                model.InspectionStandard = data.Value<string>("InspectionStandard");
                model.InspectionMethod = data.Value<string>("InspectionMethod");
                model.InspectionClassID = data.Value<string>("InspectionClassID");
                model.InspectionMethodID = data.Value<string>("InspectionMethodID");
                model.InspectionItemID = data.Value<string>("InspectionItemID");
                model.InspectionLevelID = data.Value<string>("InspectionLevelID");
                model.InspectionFaultID = data.Value<string>("InspectionFaultID");
                model.SampleQuantity = data.Value<decimal>("SampleQuantity");
                model.Aql = data.Value<string>("Aql");
                model.Status = data.Value<string>("Status");
                model.AcQuantity = data.Value<decimal>("AcQuantity");
                model.ReQuantity = data.Value<decimal>("ReQuantity");
                model.NGquantity = data.Value<decimal>("NGquantity");
                model.Attribute = data.Value<string>("Attribute");
                model.QualityControlDecision = data.Value<string>("QcDecision");
                MES_Parameter model2 = MES_ParameterService.get(Framework.SystemID + "1101213000003");//获取对应检验项目判定方式实体
                if (model2.Value == Framework.SystemID + "02012130000B3")//如果明细判定方式为自动
                {
                    if (model.NGquantity >= model.ReQuantity)//当明细修改时，不良数大于等于re数，将明细判定改为拒收
                    {
                        model.QualityControlDecision = Framework.SystemID + "0201213000090";
                    }
                    else//小于则改为允收
                    {
                        model.QualityControlDecision = Framework.SystemID + "0201213000091";
                    }
                }
                QCS_InspectionDocument QCS_ID = new QCS_InspectionDocument();
                QCS_ID = QCS_InspectionDocumentService.get(model.InspectionDocumentID);
                MES_Parameter model3 = new MES_Parameter();//品质判定方式
                model3.ParameterID = Framework.SystemID + "1101213000002";
                model3 = MES_ParameterService.get(model3.ParameterID);
                if (model3.Value == Framework.SystemID + "02012130000B1")//如果表头品质判定方式为自动
                {
                    if (model.QualityControlDecision == Framework.SystemID + "0201213000090")//当明细更改为拒收，那么获取表头实体并修改表头验收字段
                    {
                        QCS_ID.QualityControlDecision = Framework.SystemID + "0201213000090";//明细为拒收，表头也更新为拒收
                        QCS_InspectionDocumentService.update(userid, QCS_ID);
                    }
                }
                model.Comments = data.Value<string>("Comments");
                if (QCS_InspectionDocumentDetailsService.update(userid, model))
                {
                    success++;
                    if (model3.Value == Framework.SystemID + "02012130000B1")//如果表头品质判定方式为自动
                    {
                        bool result = true;//默认明细所有结果都为允收
                        IList<Hashtable> DetailArray = QCS_InspectionDocumentDetailsService.GetAllDetail(model.InspectionDocumentID);//获取该表头下的所有明细
                        for (int j = 0; j < DetailArray.Count; j++)
                        {
                            QCS_InspectionDocumentDetails IDmodel = QCS_InspectionDocumentDetailsService.get(DetailArray[j]["InspectionDocumentDetailID"].ToString());
                            if (IDmodel.QualityControlDecision == Framework.SystemID + "0201213000090")
                            {
                                result = false;//当有一个明细结果为拒收时，将result改为false
                            }
                        }
                        if (result == true)//当所有的明细结果都为允收时，将表头该为允收
                        {
                            QCS_InspectionDocument IDmodel = QCS_InspectionDocumentService.get(model.InspectionDocumentID);
                            IDmodel.QualityControlDecision = Framework.SystemID + "0201213000091";
                            QCS_InspectionDocumentService.update(userid, IDmodel);
                        }
                    }
                }
                else
                {
                    failIDs = UtilBussinessService.str(failIDs, data.Value<string>("InspectionDocumentDetailID"));
                    fail++;
                }

            }
            return new { success = success, fail = fail, failIDs = failIDs, msg = msg };
        }



        /// <summary>
        /// 制程检验单明细更新（移动端）
        /// Alvin 2017年9月11日15:14:56
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public static object Qcs00005DetailUpdateByOne(JObject request)
        {
            string Token = request.Value<string>("Token");//获取传进来的token
            string userid = UtilBussinessService.detoken(Token);//获取登录人的流水号
            string SysId = Framework.SystemID;//系统代号
            QCS_InspectionDocumentDetails model = null;
            QCS_InspectionDocument Headermodel = null;
            try
            {
                Headermodel = QCS_InspectionDocumentService.get(request.Value<string>("InspectionDocumentID"));
                if (string.IsNullOrWhiteSpace(request.Value<string>("InspectionDocumentID")))
                {
                    return new { status = "410", msg = "检验单据流水号不能为空！" };
                }
                if (Headermodel.ItemID == null)
                {
                    return new { status = "410", msg = "该检验单据的料品流水号为空！" };
                }

                if (Headermodel.Status == SysId + "020121300008E" || Headermodel.Status == SysId + "020121300008F")//状态为CA确认,CL作废时不可修改
                {
                    return new { status = "410", msg = "状态为CA确认或CL作废,不能修改！" };
                }
                model = new QCS_InspectionDocumentDetails();
                if (string.IsNullOrWhiteSpace(request.Value<string>("InspectionDocumentDetailID")))
                {
                    return new { status = "410", msg = "检验单据明细流水号不能为空！" };
                }
                model.InspectionDocumentDetailID = request.Value<string>("InspectionDocumentDetailID");
                model.InspectionDocumentID = request.Value<string>("InspectionDocumentID");
                model.NGquantity = request.Value<decimal>("NGquantity");
                model.Attribute = request.Value<string>("Attribute");
                model.QualityControlDecision = request.Value<string>("QcDecision");
                model.Comments = request.Value<string>("Comments");
                MES_Parameter model3 = new MES_Parameter();//品质判定方式
                model3.ParameterID = Framework.SystemID + "1101213000002";
                model3 = MES_ParameterService.get(model3.ParameterID);
                if (model3.Value == Framework.SystemID + "02012130000B1")//如果表头品质判定方式为自动
                {
                    if (model.QualityControlDecision == Framework.SystemID + "0201213000090")//当明细更改为拒收，那么获取表头实体并修改表头验收字段
                    {
                        QCS_InspectionDocument QCS_ID = new QCS_InspectionDocument();
                        QCS_ID = QCS_InspectionDocumentService.get(model.InspectionDocumentID);
                        QCS_ID.QualityControlDecision = Framework.SystemID + "0201213000090";//明细为拒收，表头也更新为拒收
                        QCS_InspectionDocumentService.update(userid, QCS_ID);
                    }
                }

                if (QCS_InspectionDocumentDetailsService.Qcs00005DetailUpdate(userid, model))
                {
                    return new { status = "200", msg = "修改明细成功！" };
                }
                else
                {
                    return new { status = "410", msg = "修改明细失败！" };
                }
            }
            catch (Exception ex)
            {
                DataLogerService.writeerrlog(ex);
                return new { status = "410", msg = "修改明细失败！" + ex.ToString() };
            }
        }


        /// <summary>
        /// 制程检验单明细原因码列表
        /// Joint
        /// </summary>
        /// <param name="Token"></param>
        /// <param name="InspectionDocumentID"></param>
        /// <param name="InspectionDocumentDetailID"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <returns></returns>
        public static object Qcs00005GetReasonList(string Token, string InspectionDocumentID, string InspectionDocumentDetailID, int page, int rows)
        {
            int count = 0;
            return UtilBussinessService.getPaginationModel(QCS_InspectionDocumentReasonService.Qcs00005GetReasonList(InspectionDocumentID, InspectionDocumentDetailID, page, rows, ref count), count);
        }

        /// <summary>
        /// 制程检验原因码新增
        /// Joint
        /// </summary>
        /// <param name="Token"></param>
        /// <param name="jArray"></param>
        /// <returns></returns>
        public static object Qcs00005ReasonInsert(string Token, JArray jArray)
        {
            int success = 0;
            int fail = 0;
            string failIDs = null;
            string msg = null;
            string userid = UtilBussinessService.detoken(Token);
            JObject data = null;
            QCS_InspectionDocumentReason model = new QCS_InspectionDocumentReason();
            QCS_InspectionDocumentDetails Headermodel = null;
            for (int i = 0; i < jArray.Count; i++)
            {
                data = (JObject)jArray[i];
                Headermodel = QCS_InspectionDocumentDetailsService.get(data.Value<string>("InspectionDocumentDetailID"));
                if (Headermodel == null)
                {
                    failIDs = UtilBussinessService.str(failIDs, data.Value<string>("InspectionDocumentDetailID"));
                    msg = UtilBussinessService.str(msg, data.Value<string>("Sequence") + "的明细流水号为空");
                    fail++;
                }
                model.InspectionDocumentReasonID = UniversalService.GetSerialNumber("QCS_InspectionDocumentReason");
                model.InspectionDocumentID = data.Value<string>("InspectionDocumentID");
                model.InspectionDocumentDetailID = data.Value<string>("InspectionDocumentDetailID");
                if (!string.IsNullOrWhiteSpace(data.Value<string>("Sequence")))
                    model.Sequence = data.Value<int>("Sequence");
                if (string.IsNullOrWhiteSpace(data.Value<string>("ReasonID")))
                {
                    return new { Status = "400", msg = "保存失败，原因为空！" };
                }
                model.ReasonID = data.Value<string>("ReasonID");
                model.Comments = data.Value<string>("Comments");
                model.Status = Framework.SystemID + "0201213000001";
                if (QCS_InspectionDocumentReasonService.insert(userid, model))
                    success++;
                else
                {
                    failIDs = UtilBussinessService.str(failIDs, data.Value<string>("InspectionDocumentReasonID"));
                    fail++;
                }
            }
            return new { success = success, fail = fail, failIDs = failIDs, msg = msg };
        }



        /// <summary>
        /// 制程检验原因码新增--移动端
        /// Alvin 2017年9月11日16:33:52
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public static object Qcs00005ReasonAddByOne(JObject request)
        {
            string userid = UtilBussinessService.detoken(request.Value<string>("Token"));
            QCS_InspectionDocumentReason model = new QCS_InspectionDocumentReason();
            QCS_InspectionDocumentDetails Headermodel = null;
            try
            {
                Headermodel = QCS_InspectionDocumentDetailsService.get(request.Value<string>("InspectionDocumentDetailID"));
                if (Headermodel == null)
                {
                    return new { status = "410", msg = "检验单据明细流水号不能为空！" };
                }
                model.InspectionDocumentReasonID = UniversalService.GetSerialNumber("QCS_InspectionDocumentReason");
                model.InspectionDocumentID = request.Value<string>("InspectionDocumentID");
                model.InspectionDocumentDetailID = request.Value<string>("InspectionDocumentDetailID");
                if (!string.IsNullOrWhiteSpace(request.Value<string>("Sequence")))
                    model.Sequence = request.Value<int>("Sequence");
                if (string.IsNullOrWhiteSpace(request.Value<string>("ReasonID")))
                {
                    return new { Status = "400", msg = "保存失败，原因为空！" };
                }
                model.ReasonID = request.Value<string>("ReasonID");
                model.Comments = request.Value<string>("Comments");
                model.Status = Framework.SystemID + "0201213000001";
                if (QCS_InspectionDocumentReasonService.insert(userid, model))
                {
                    return new { status = "200", msg = "原因码新增成功！" };
                }
                else
                {
                    return new { status = "410", msg = "原因码新增失败！" };
                }
            }
            catch (Exception ex)
            {
                DataLogerService.writeerrlog(ex);
                return new { status = "410", msg = "原因码新增失败！" + ex.ToString() };

            }
        }



        /// <summary>
        /// 制程检验原因吗更新
        /// Joint
        /// </summary>
        /// <param name="Token"></param>
        /// <param name="jArray"></param>
        /// <returns></returns>
        public static object Qcs00005ReasonUpdate(string Token, JArray jArray)
        {
            int success = 0;
            int fail = 0;
            string failIDs = null;
            string msg = null;
            string userid = UtilBussinessService.detoken(Token);
            JObject data = null;
            QCS_InspectionDocumentReason model = null;
            QCS_InspectionDocumentDetails Headermodel = null;
            for (int i = 0; i < jArray.Count; i++)
            {
                data = (JObject)jArray[i];
                Headermodel = QCS_InspectionDocumentDetailsService.get(data.Value<string>("InspectionDocumentDetailID"));
                if (Headermodel == null)
                {
                    failIDs = UtilBussinessService.str(failIDs, data.Value<string>("InspectionDocumentDetailID"));
                    msg = UtilBussinessService.str(msg, data.Value<string>("Sequence") + "的明细流水号为空");
                    fail++;
                }
                model = QCS_InspectionDocumentReasonService.get(data.Value<string>("InspectionDocumentReasonID"));
                if (model == null)
                {
                    failIDs = UtilBussinessService.str(failIDs, data.Value<string>("InspectionDocumentReasonID"));
                    msg = UtilBussinessService.str(msg, data.Value<string>("Sequence") + "的原因码流水号为空");
                    fail++;
                }
                if (!string.IsNullOrWhiteSpace(data.Value<string>("Sequence")))
                    model.Sequence = data.Value<int>("Sequence");
                model.ReasonID = data.Value<string>("ReasonID");
                model.Comments = data.Value<string>("Comments");
                if (QCS_InspectionDocumentReasonService.update(userid, model))
                    success++;
                else
                {
                    failIDs = UtilBussinessService.str(failIDs, data.Value<string>("InspectionDocumentReasonID"));
                    fail++;
                }
            }
            return new { success = success, fail = fail, failIDs = failIDs, msg = msg };
        }


        /// <summary>
        /// 原因码修改
        /// Alvin 2017年9月11日16:45:54
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public static object Qcs00005ReasonEditByOne(JObject request)
        {
            string userid = UtilBussinessService.detoken(request.Value<string>("Token"));
            QCS_InspectionDocumentReason model = null;
            QCS_InspectionDocumentDetails Headermodel = null;
            try
            {
                Headermodel = QCS_InspectionDocumentDetailsService.get(request.Value<string>("InspectionDocumentDetailID"));
                if (Headermodel == null)
                {
                    return new { status = "410", msg = "检验单据明细流水号不能为空！" };
                }
                model = QCS_InspectionDocumentReasonService.get(request.Value<string>("InspectionDocumentReasonID"));
                if (model == null)
                {
                    return new { status = "410", msg = "原因码流水号不能为空！" };
                }
                if (!string.IsNullOrWhiteSpace(request.Value<string>("Sequence")))
                    model.Sequence = request.Value<int>("Sequence");
                model.ReasonID = request.Value<string>("ReasonID");
                model.Comments = request.Value<string>("Comments");
                if (QCS_InspectionDocumentReasonService.update(userid, model))
                {
                    return new { status = "200", msg = "原因码修改成功！" };
                }
                else
                {
                    return new { status = "410", msg = "原因码修改失败！" };
                }
            }
            catch (Exception ex)
            {
                DataLogerService.writeerrlog(ex);
                return new { status = "410", msg = "原因码修改失败！" + ex.ToString() };
            }
        }



        /// <summary>
        /// 制程检验原因码删除
        /// Joint
        /// </summary>
        /// <param name="Token"></param>
        /// <param name="jArray"></param>
        /// <returns></returns>
        public static object Qcs00005ReasonDelete(string Token, JArray jArray)
        {
            int success = 0;
            int fail = 0;
            string failIDs = null;
            string msg = null;
            string userid = UtilBussinessService.detoken(Token);
            JObject data = null;
            QCS_InspectionDocumentReason model = null;
            QCS_InspectionDocumentDetails Headermodel = null;
            for (int i = 0; i < jArray.Count; i++)
            {
                data = (JObject)jArray[i];
                Headermodel = QCS_InspectionDocumentDetailsService.get(data.Value<string>("InspectionDocumentDetailID"));
                if (Headermodel == null)
                {
                    failIDs = UtilBussinessService.str(failIDs, data.Value<string>("InspectionDocumentDetailID"));
                    msg = UtilBussinessService.str(msg, data.Value<string>("Sequence") + "的明细流水号为空");
                    fail++;
                }
                model = QCS_InspectionDocumentReasonService.get(data.Value<string>("InspectionDocumentReasonID"));
                if (model == null)
                {
                    failIDs = UtilBussinessService.str(failIDs, data.Value<string>("InspectionDocumentReasonID"));
                    msg = UtilBussinessService.str(msg, data.Value<string>("Sequence") + "的原因码流水号为空");
                    fail++;
                }
                model.Status = Framework.SystemID + "0201213000003";
                if (QCS_InspectionDocumentReasonService.update(userid, model))
                    success++;
                else
                {
                    failIDs = UtilBussinessService.str(failIDs, data.Value<string>("InspectionDocumentReasonID"));
                    fail++;
                }
            }
            return new { success = success, fail = fail, failIDs = failIDs, msg = msg };
        }

        /// <summary>
        /// 制程检验原因码删除(移动端)
        /// Alvin 2017年9月11日16:59:53
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public static object Qcs00005ReasonDeleteByOne(JObject request)
        {
            string userid = UtilBussinessService.detoken(request.Value<string>("Token"));
            QCS_InspectionDocumentReason model = null;
            QCS_InspectionDocumentDetails Headermodel = null;
            try
            {
                Headermodel = QCS_InspectionDocumentDetailsService.get(request.Value<string>("InspectionDocumentDetailID"));
                if (Headermodel == null)
                {
                    return new { status = "410", msg = "检验单据明细流水号不能为空！" };
                }
                model = QCS_InspectionDocumentReasonService.get(request.Value<string>("InspectionDocumentReasonID"));
                if (model == null)
                {
                    return new { status = "410", msg = "原因码流水号不能为空！" };
                }
                model.Status = Framework.SystemID + "0201213000003";
                if (QCS_InspectionDocumentReasonService.update(userid, model))
                {
                    return new { status = "200", msg = "原因码删除成功！" };
                }
                else
                {
                    return new { status = "410", msg = "原因码删除失败！" };
                }
            }
            catch (Exception ex)
            {
                DataLogerService.writeerrlog(ex);
                return new { status = "410", msg = "原因码删除失败！" + ex.ToString() };
            }
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
        /// <returns></returns>
        public static object Qcs00005GetRemarkList(string Token, string InspectionDocumentID, string InspectionDocumentDetailID, int page, int rows)
        {
            int count = 0;
            return UtilBussinessService.getPaginationModel(QCS_InspectionDocumentRemarkService.Qcs00005GetRemarkList(InspectionDocumentID, InspectionDocumentDetailID, page, rows, ref count), count);
        }

        /// <summary>
        /// 检验结果说明新增
        /// Joint
        /// </summary>
        /// <param name="Token"></param>
        /// <param name="jArray"></param>
        /// <returns></returns>
        public static object Qcs00005RemarkInsert(string Token, JArray jArray)
        {
            int success = 0;
            int fail = 0;
            string failIDs = null;
            string msg = null;
            string userid = UtilBussinessService.detoken(Token);
            JObject data = null;
            QCS_InspectionDocumentRemark model = new QCS_InspectionDocumentRemark();
            QCS_InspectionDocumentDetails Headermodel = null;
            for (int i = 0; i < jArray.Count; i++)
            {
                data = (JObject)jArray[i];
                Headermodel = QCS_InspectionDocumentDetailsService.get(data.Value<string>("InspectionDocumentDetailID"));
                if (Headermodel == null)
                {
                    failIDs = UtilBussinessService.str(failIDs, data.Value<string>("InspectionDocumentDetailID"));
                    msg = UtilBussinessService.str(msg, data.Value<string>("Sequence") + "的明细流水号为空");
                    fail++;
                }
                model.InspectionDocumentRemarkID = UniversalService.GetSerialNumber("QCS_InspectionDocumentRemark");
                model.InspectionDocumentDetailID = data.Value<string>("InspectionDocumentDetailID");
                model.InspectionDocumentID = data.Value<string>("InspectionDocumentID");
                if (!string.IsNullOrWhiteSpace(data.Value<string>("Sequence")))
                    model.Sequence = data.Value<int>("Sequence");
                model.Remark = data.Value<string>("Remark");
                model.Status = Framework.SystemID + "0201213000001";
                if (QCS_InspectionDocumentRemarkService.insert(userid, model))
                    success++;
                else
                {
                    failIDs = UtilBussinessService.str(failIDs, data.Value<string>("InspectionDocumentRemarkID"));
                    fail++;
                }
            }
            return new { success = success, fail = fail, failIDs = failIDs, msg = msg };
        }

        /// <summary>
        /// 检验结果说明新增(移动端)
        /// Alvin 2017年9月11日17:09:54
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public static object Qcs00005RemarkAddByOne(JObject request)
        {
            string userid = UtilBussinessService.detoken(request.Value<string>("Token"));
            QCS_InspectionDocumentRemark model = new QCS_InspectionDocumentRemark();
            QCS_InspectionDocumentDetails Headermodel = null;
            try
            {
                Headermodel = QCS_InspectionDocumentDetailsService.get(request.Value<string>("InspectionDocumentDetailID"));
                if (Headermodel == null)
                {
                    return new { status = "410", msg = "检验单据明细流水号不能为空！" };
                }
                model.InspectionDocumentRemarkID = UniversalService.GetSerialNumber("QCS_InspectionDocumentRemark");
                model.InspectionDocumentDetailID = request.Value<string>("InspectionDocumentDetailID");
                model.InspectionDocumentID = request.Value<string>("InspectionDocumentID");
                if (!string.IsNullOrWhiteSpace(request.Value<string>("Sequence")))
                    model.Sequence = request.Value<int>("Sequence");
                model.Remark = request.Value<string>("Remark");
                model.Status = Framework.SystemID + "0201213000001";
                if (QCS_InspectionDocumentRemarkService.insert(userid, model))
                {
                    return new { status = "200", msg = "结果说明新增成功！" };
                }
                else
                {
                    return new { status = "410", msg = "结果说明新增失败！" };
                }

            }
            catch (Exception ex)
            {
                DataLogerService.writeerrlog(ex);
                return new { status = "410", msg = "结果说明新增失败！" + ex.ToString() };
            }
        }

        /// <summary>
        /// 检验结果说明更新
        /// Joint
        /// </summary>
        /// <param name="Token"></param>
        /// <param name="jArray"></param>
        /// <returns></returns>
        public static object Qcs00005RemarkUpdate(string Token, JArray jArray)
        {
            int success = 0;
            int fail = 0;
            string failIDs = null;
            string msg = null;
            string userid = UtilBussinessService.detoken(Token);
            JObject data = null;
            QCS_InspectionDocumentRemark model = null;
            QCS_InspectionDocumentDetails Headermodel = null;
            for (int i = 0; i < jArray.Count; i++)
            {
                data = (JObject)jArray[i];
                Headermodel = QCS_InspectionDocumentDetailsService.get(data.Value<string>("InspectionDocumentDetailID"));
                if (Headermodel == null)
                {
                    failIDs = UtilBussinessService.str(failIDs, data.Value<string>("InspectionDocumentDetailID"));
                    msg = UtilBussinessService.str(msg, data.Value<string>("Sequence") + "的明细流水号为空");
                    fail++;
                }
                model = QCS_InspectionDocumentRemarkService.get(data.Value<String>("InspectionDocumentRemarkID"));
                if (model == null)
                {
                    failIDs = UtilBussinessService.str(failIDs, data.Value<string>("InspectionDocumentRemarkID"));
                    msg = UtilBussinessService.str(msg, data.Value<string>("Sequence") + "的结果说明流水号为空");
                    fail++;
                }
                if (!string.IsNullOrWhiteSpace(data.Value<string>("Sequence")))
                    model.Sequence = data.Value<int>("Sequence");
                model.Remark = data.Value<string>("Remark");
                if (QCS_InspectionDocumentRemarkService.update(userid, model))
                    success++;
                else
                {
                    failIDs = UtilBussinessService.str(failIDs, data.Value<string>("InspectionDocumentRemarkID"));
                    fail++;
                }
            }
            return new { success = success, fail = fail, failIDs = failIDs, msg = msg };
        }


        /// <summary>
        /// 检验结果说明修改(移动端)
        /// Alvin 2017年9月11日17:09:54
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public static object Qcs00005RemarkEditByOne(JObject request)
        {
            string userid = UtilBussinessService.detoken(request.Value<string>("Token"));
            QCS_InspectionDocumentRemark model = null;
            QCS_InspectionDocumentDetails Headermodel = null;
            try
            {
                Headermodel = QCS_InspectionDocumentDetailsService.get(request.Value<string>("InspectionDocumentDetailID"));
                if (Headermodel == null)
                {
                    return new { status = "410", msg = "检验单据明细流水号不能为空！" };
                }
                model = QCS_InspectionDocumentRemarkService.get(request.Value<String>("InspectionDocumentRemarkID"));
                if (model == null)
                {
                    return new { status = "410", msg = "检验单据结果说明流水号为空！" };
                }
                if (!string.IsNullOrWhiteSpace(request.Value<string>("Sequence")))
                    model.Sequence = request.Value<int>("Sequence");
                model.Remark = request.Value<string>("Remark");
                if (QCS_InspectionDocumentRemarkService.update(userid, model))
                {
                    return new { status = "200", msg = "结果说明修改成功！" };
                }
                else
                {
                    return new { status = "410", msg = "结果说明修改失败！" };
                }

            }
            catch (Exception ex)
            {
                DataLogerService.writeerrlog(ex);
                return new { status = "410", msg = "结果说明修改失败！" + ex.ToString() };
            }
        }



        /// <summary>
        /// 检验结果说明删除
        /// Joint
        /// </summary>
        /// <param name="Token"></param>
        /// <param name="jArray"></param>
        /// <returns></returns>
        public static object Qcs00005RemarkDelete(string Token, JArray jArray)
        {
            int success = 0;
            int fail = 0;
            string failIDs = null;
            string msg = null;
            string userid = UtilBussinessService.detoken(Token);
            JObject data = null;
            QCS_InspectionDocumentRemark model = null;
            QCS_InspectionDocumentDetails Headermodel = null;
            for (int i = 0; i < jArray.Count; i++)
            {
                data = (JObject)jArray[i];
                Headermodel = QCS_InspectionDocumentDetailsService.get(data.Value<string>("InspectionDocumentDetailID"));
                if (Headermodel == null)
                {
                    failIDs = UtilBussinessService.str(failIDs, data.Value<string>("InspectionDocumentDetailID"));
                    msg = UtilBussinessService.str(msg, data.Value<string>("Sequence") + "的表头流水号为空");
                    fail++;
                }
                model = QCS_InspectionDocumentRemarkService.get(data.Value<String>("InspectionDocumentRemarkID"));
                if (model == null)
                {
                    failIDs = UtilBussinessService.str(failIDs, data.Value<string>("InspectionDocumentRemarkID"));
                    msg = UtilBussinessService.str(msg, data.Value<string>("Sequence") + "的结果说明流水号为空");
                    fail++;
                }
                model.Status = Framework.SystemID + "0201213000003";
                if (QCS_InspectionDocumentRemarkService.update(userid, model))
                    success++;
                else
                {
                    failIDs = UtilBussinessService.str(failIDs, data.Value<string>("InspectionDocumentDetailID"));
                    fail++;
                }
            }
            return new { success = success, fail = fail, failIDs = failIDs, msg = msg };
        }


        /// <summary>
        /// 检验结果说明删除(移动端)
        /// Alvin 2017年9月11日17:23:37
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public static object Qcs00005RemarkDeleteByOne(JObject request)
        {
            string userid = UtilBussinessService.detoken(request.Value<string>("Token"));
            QCS_InspectionDocumentRemark model = null;
            QCS_InspectionDocumentDetails Headermodel = null;
            try
            {
                Headermodel = QCS_InspectionDocumentDetailsService.get(request.Value<string>("InspectionDocumentDetailID"));
                if (Headermodel == null)
                {
                    return new { status = "410", msg = "检验单据明细流水号不能为空！" };
                }
                model = QCS_InspectionDocumentRemarkService.get(request.Value<String>("InspectionDocumentRemarkID"));
                if (model == null)
                {
                    return new { status = "410", msg = "检验单据结果说明流水号为空！" };
                }
                model.Status = Framework.SystemID + "0201213000003";
                if (QCS_InspectionDocumentRemarkService.update(userid, model))
                {
                    return new { status = "200", msg = "结果说明删除成功！" };
                }
                else
                {
                    return new { status = "410", msg = "结果说明删除失败！" };
                }

            }
            catch (Exception ex)
            {
                DataLogerService.writeerrlog(ex);
                return new { status = "410", msg = "结果说明删除失败！" + ex.ToString() };
            }
        }

        #endregion

        #region QCS00006制程检验确认
        /// <summary>
        ///  制程检验确认-主查詢
        ///  SAM 2017年7月9日20:30:23
        /// </summary>
        /// <param name="token"></param>
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
        public static object Qcs00006GetList(string token, string InspectionType, string UserID, string SDate, string EDate, string StartInspectionNo, string EndInspectionNo, string StartRCNo, string EndRCNo, string StartPart, string EndPart, int page, int rows)
        {
            int count = 0;
            IList<Hashtable> result = QCS_InspectionDocumentService.Qcs00006GetList(InspectionType, UserID, SDate, EDate, StartInspectionNo, EndInspectionNo, StartRCNo, EndRCNo, StartPart, EndPart, page, rows, ref count);
            return UtilBussinessService.getPaginationModel(result, count);
        }

        /// <summary>
        /// 制程检验确认-檢驗明細
        /// SAM 2017年7月9日21:19:11
        /// </summary>
        /// <param name="token"></param>
        /// <param name="InspectionDocumentID"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <returns></returns>
        public static object QCS00006GetDetailList(string token, string InspectionDocumentID, int page, int rows)
        {
            int count = 0;
            IList<Hashtable> result = QCS_InspectionDocumentDetailsService.QCS00006GetDetailList(InspectionDocumentID, page, rows, ref count);
            return UtilBussinessService.getPaginationModel(result, count);
        }

        /// <summary>
        /// 檢驗明細-不良原因
        /// SAM 2017年7月9日21:41:43
        /// </summary>
        /// <param name="token"></param>
        /// <param name="InspectionDocumentDetailID"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <returns></returns>
        public static object QCS00006DetailReason(string token, string InspectionDocumentDetailID, int page, int rows)
        {
            int count = 0;
            IList<Hashtable> result = QCS_InspectionDocumentReasonService.QCS00006DetailReason(InspectionDocumentDetailID, page, rows, ref count);
            return UtilBussinessService.getPaginationModel(result, count);
        }

        /// <summary>
        /// 檢驗明細-檢驗結果
        /// SAM 2017年7月9日21:41:52
        /// </summary>
        /// <param name="token"></param>
        /// <param name="InspectionDocumentDetailID"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <returns></returns>
        public static object QCS00006DetailResult(string token, string InspectionDocumentDetailID, int page, int rows)
        {
            int count = 0;
            IList<Hashtable> result = QCS_InspectionDocumentRemarkService.QCS00006DetailResult(InspectionDocumentDetailID, page, rows, ref count);
            return UtilBussinessService.getPaginationModel(result, count);
        }

        /// <summary>
        /// 制程检验确认-檢驗單確認
        /// SAM 2017年7月9日21:47:07
        /// Mouse 2017年10月11日15:53:34 修改产生完工调整单
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public static object QCS00006Confirm(JObject request)
        {
            string Token = request.Value<string>("Token");
            string userid = UtilBussinessService.detoken(Token);

            QCS_InspectionDocument model = QCS_InspectionDocumentService.get(request.Value<string>("InspectionDocumentID"));
            if (model == null)
                return new { status = "410", msg = "流水号错误！" };

            if (model.Status != Framework.SystemID + "020121300008D")
                return new { status = "410", msg = "工单并非处于开单状态！" };

            if (model.InspectionMethod == Framework.SystemID + "0201213000080" || model.InspectionMethod == Framework.SystemID + "0201213000081")
            {
                model.Status = Framework.SystemID + "020121300008E";
                if (QCS_InspectionDocumentService.update(userid, model))
                {
                    if(model.InspectionMethod == Framework.SystemID + "0201213000080" && model.QualityControlDecision==Framework.SystemID+ "0201213000091")
                    {//当检验单种类为首件时，且品质判定为允收将该任务单的首件品质判定改为Y、1、true
                        SFC_TaskDispatch TDmodel = SFC_TaskDispatchService.get(model.TaskDispatchID);
                        TDmodel.FPIPass = true;
                        SFC_TaskDispatchService.update(userid, TDmodel);
                    }
                        return new { status = "200", msg = "確認成功！" };
                }
                else
                    return new { status = "410", msg = "確認失败！" };
            }
            else if (model.InspectionMethod == Framework.SystemID + "020121300007E")
            {
                if (!model.InspectionFlag)
                {
                    if (string.IsNullOrWhiteSpace(model.QualityControlDecision))
                        return new { status = "410", msg = model.InspectionNo + "檢驗單據未品質判定，不得確認！" };
                }

                model.Status = Framework.SystemID + "020121300008E";
                if (QCS_InspectionDocumentService.update(userid, model))
                {
                    //c.可移轉量須自動產生製程移轉單。邏輯請參考MES規格02-制程移轉作業。
                    //d.報廢量、反修量的加總需產生完工調整單，扣除檢驗製程的完工量。產生的完工調整單請參考檢驗單確認自動產生完工調整單邏輯)。
                    //    完工調整單之完工調整量 = 負數(報廢量 + 反修量)
                    //    完工調整單之報廢量 = 檢驗單之報廢量
                    //    完工調整單之反修量 = 檢驗單之反修量
                    //    完工調整單之單號等同檢驗單號


                    //SFC_CompletionOrder Commodel = new SFC_CompletionOrder();
                    //Commodel.CompletionOrderID = UniversalService.GetSerialNumber("SFC_CompletionOrder");
                    //Commodel.CompletionNo = model.InspectionNo;
                    //Commodel.Date = DateTime.Now;
                    //Commodel.TaskDispatchID = Complention.TaskDispatchID;
                    //Commodel.FabricatedMotherID = Complention.FabricatedMotherID;
                    //Commodel.FabMoProcessID = Complention.FabMoProcessID;
                    //Commodel.FabMoOperationID = Complention.FabMoOperationID;
                    //Commodel.ItemID = Complention.ItemID;
                    //Commodel.ProcessID = Complention.ProcessID;
                    //Commodel.OperationID = Complention.OperationID;
                    //Commodel.FinProQuantity = 0;
                    //Commodel.ScrappedQuantity = 0;
                    //Commodel.DifferenceQuantity = 0;
                    //Commodel.RepairQuantity = 0;


                    //model.Status = Framework.SystemID + "0201213000029";
                    //model.Type = Framework.SystemID + "02012130000A2";
                    //model.Comments = request.Value<string>("Comments");
                    //model.OriginalCompletionOrderID = request.Value<string>("OriginalCompletionOrderID");
                    //model.DTSID = request.Value<string>("DTSID");

                    return new { status = "200", msg = "確認成功！" };
                }
                else
                    return new { status = "410", msg = "確認失败！" };
            }
            else
                return new { status = "410", msg = "未定义的檢驗種類！" };
        }

        /// <summary>
        /// 制程检验确认-产生已确认完工调整单
        /// Mouse 2017年10月11日18:03:07
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public static object QCS00006ConfirmV710(JObject request)
        {
            string Token = request.Value<string>("Token");
            string userid = UtilBussinessService.detoken(Token);

            QCS_InspectionDocument model = QCS_InspectionDocumentService.get(request.Value<string>("InspectionDocumentID"));
            if (model == null)
                return new { status = "410", msg = "流水号错误！" };

            if (model.Status != Framework.SystemID + "020121300008D")
                return new { status = "410", msg = "工单并非处于开单状态！" };

            if (model.InspectionMethod == Framework.SystemID + "0201213000080" || model.InspectionMethod == Framework.SystemID + "0201213000081")
            {
                model.Status = Framework.SystemID + "020121300008E";
                if (QCS_InspectionDocumentService.update(userid, model))
                    return new { status = "200", msg = "確認成功！" };
                else
                    return new { status = "410", msg = "確認失败！" };
            }
            else if (model.InspectionMethod == Framework.SystemID + "020121300007E")
            {
                if (!model.InspectionFlag)
                {
                    if (string.IsNullOrWhiteSpace(model.QualityControlDecision))
                        return new { status = "410", msg = model.InspectionNo + "檢驗單據未品質判定，不得確認！" };
                }

                model.Status = Framework.SystemID + "020121300008E";
                if (QCS_InspectionDocumentService.update(userid, model))
                {
                    //c.可移轉量須自動產生製程移轉單。邏輯請參考MES規格02-制程移轉作業。
                    //d.報廢量、反修量的加總需產生完工調整單，扣除檢驗製程的完工量。產生的完工調整單請參考檢驗單確認自動產生完工調整單邏輯)。
                    //    完工調整單之完工調整量 = 負數(報廢量 + 反修量)
                    //    完工調整單之報廢量 = 檢驗單之報廢量
                    //    完工調整單之反修量 = 檢驗單之反修量
                    //    完工調整單之單號等同檢驗單號


                    //SFC_CompletionOrder Commodel = new SFC_CompletionOrder();
                    //Commodel.CompletionOrderID = UniversalService.GetSerialNumber("SFC_CompletionOrder");
                    //Commodel.CompletionNo = model.InspectionNo;
                    //Commodel.Date = DateTime.Now;
                    //Commodel.TaskDispatchID = Complention.TaskDispatchID;
                    //Commodel.FabricatedMotherID = Complention.FabricatedMotherID;
                    //Commodel.FabMoProcessID = Complention.FabMoProcessID;
                    //Commodel.FabMoOperationID = Complention.FabMoOperationID;
                    //Commodel.ItemID = Complention.ItemID;
                    //Commodel.ProcessID = Complention.ProcessID;
                    //Commodel.OperationID = Complention.OperationID;
                    //Commodel.FinProQuantity = 0;
                    //Commodel.ScrappedQuantity = 0;
                    //Commodel.DifferenceQuantity = 0;
                    //Commodel.RepairQuantity = 0;


                    //model.Status = Framework.SystemID + "0201213000029";
                    //model.Type = Framework.SystemID + "02012130000A2";
                    //model.Comments = request.Value<string>("Comments");
                    //model.OriginalCompletionOrderID = request.Value<string>("OriginalCompletionOrderID");
                    //model.DTSID = request.Value<string>("DTSID");
                    if (model.InspectionMethod == Framework.SystemID + "020121300007E")//如果是检验种类为制程检验，则产生完工调整单
                    {
                        SFC_CompletionOrder Complention = SFC_CompletionOrderService.get(model.CompletionOrderID);
                        if (Complention == null)
                            return new { status = "200", msg = "原完工单信息不存在！" };
                        if (model.ScrappedQuantity > 0 || model.NGquantity > 0)//如果该制程检验报废或NG大于0.
                        {
                            string AutoNumberID = null;
                            SFC_CompletionOrder COmodel = new SFC_CompletionOrder();
                            COmodel.CompletionOrderID = UniversalService.GetSerialNumber("SFC_CompletionOrder");
                            IList<Hashtable> Type = SYS_DocumentTypeSettingService.GetTypeList(userid, Framework.SystemID + "02012130000AA");
                            if (Type.Count != 0)
                                COmodel.CompletionNo = UtilBussinessService.GetDocumentAutoNumber(userid, Type[0]["value"].ToString(), null, ref AutoNumberID);
                            COmodel.Date = DateTime.Now;
                            COmodel.TaskDispatchID = Complention.TaskDispatchID;
                            COmodel.FabricatedMotherID = Complention.FabricatedMotherID;
                            COmodel.FabMoProcessID = Complention.FabMoProcessID;
                            COmodel.FabMoOperationID = Complention.FabMoOperationID;
                            COmodel.ItemID = Complention.ItemID;
                            COmodel.ProcessID = Complention.ProcessID;
                            COmodel.OperationID = Complention.OperationID;
                            COmodel.Status = Framework.SystemID + "020121300002A";//产生的调整单状态为结案
                            COmodel.Type = Framework.SystemID + "02012130000A1";
                            COmodel.OriginalCompletionOrderID = model.CompletionOrderID;
                            //1	完工數量、報廢量、差異量、寫入
                            //2	寫入RC任務單分派資料檔完工數(累加)及制令製程(工序)檔完工量(累加) 
                            //3	寫入RC任務單分派資料檔報廢量(累加)及制令製程(工序)檔報廢量(累加)
                            //4	寫入RC任務單分派資料檔差異量(累加)及製令製程(工序)檔差異量(累加) 
                            //5	寫入RC任務單分派資料檔返修量(累加)及製令製程(工序)檔返修量(累加) 
                            //6	RC任務單單如有工序,制令制程工序檔如是最後一道工序(制程2工序6),將此工序之相關數量寫入該製令制程檔(制程2),其他(制程2工序1~5)不用寫入制程檔(制程2)
                            //7	當RC任務單分派資料檔完工量大於等於分派量時，RC任務單分派單之狀態同時改成CL狀態。
                            //8	當製令製程工序檔的完工量大於等於生產量時，制程工序狀態之狀態同時改為CL狀態。
                            SFC_TaskDispatch TaskModel = SFC_TaskDispatchService.get(COmodel.TaskDispatchID);//任务单
                            SFC_FabMoOperation OperationModel = SFC_FabMoOperationService.get(COmodel.FabMoOperationID);//工序
                            SFC_FabMoProcess ProcessModel = SFC_FabMoProcessService.get(COmodel.FabMoProcessID);//制程
                            TaskModel.FinishQuantity += COmodel.FinProQuantity;
                            TaskModel.ScrapQuantity += COmodel.ScrappedQuantity;
                            TaskModel.DiffQuantity += COmodel.DifferenceQuantity;
                            TaskModel.RepairQuantity += COmodel.RepairQuantity;

                            if (TaskModel.FinishQuantity >= TaskModel.DispatchQuantity)
                                TaskModel.Status = Framework.SystemID + "020121300008B";

                            SFC_TaskDispatchService.update(userid, TaskModel);

                            if (OperationModel == null)
                            {
                                ProcessModel.FinProQuantity += COmodel.FinProQuantity;
                                ProcessModel.ScrappedQuantity += COmodel.ScrappedQuantity;
                                ProcessModel.DifferenceQuantity += COmodel.DifferenceQuantity;
                                ProcessModel.RepairQuantity += COmodel.RepairQuantity;
                                if (ProcessModel.FinProQuantity >= ProcessModel.Quantity)
                                    ProcessModel.Status = Framework.SystemID + "020121300002A";

                                SFC_FabMoProcessService.update(userid, ProcessModel);
                            }
                            else
                            {

                                OperationModel.FinProQuantity += COmodel.FinProQuantity;
                                OperationModel.ScrappedQuantity += COmodel.ScrappedQuantity;
                                OperationModel.DifferenceQuantity += COmodel.DifferenceQuantity;
                                OperationModel.RepairQuantity += COmodel.RepairQuantity;

                                if (OperationModel.FinProQuantity >= OperationModel.Quantity)
                                    OperationModel.Status = Framework.SystemID + "020121300002A";

                                SFC_FabMoOperationService.update(userid, OperationModel);

                                SFC_FabMoOperationRelationship ship = SFC_FabMoOperationRelationshipService.getByFMOperation(OperationModel.FabMoOperationID);
                                if (ship != null && ship.IsLastOperation)
                                {

                                    ProcessModel.FinProQuantity += COmodel.FinProQuantity;
                                    ProcessModel.ScrappedQuantity += COmodel.ScrappedQuantity;
                                    ProcessModel.DifferenceQuantity += COmodel.DifferenceQuantity;
                                    ProcessModel.RepairQuantity += COmodel.RepairQuantity;

                                    if (ProcessModel.FinProQuantity >= ProcessModel.Quantity)
                                        ProcessModel.Status = Framework.SystemID + "020121300002A";

                                    SFC_FabMoProcessService.update(userid, ProcessModel);
                                }
                            }
                            //执行完毕
                            if (SFC_CompletionOrderService.insert(userid, COmodel))
                            {
                                UtilBussinessService.UpdateDocumentAutoNumber(userid, AutoNumberID);
                                return new { status = "200", msg = "确认成功并生成调整单！" };
                            }
                            else
                                return new { status = "410", msg = "确认成功但调整单生成失败！" };
                        }
                    }
                    return new { status = "200", msg = "確認成功！" };
                }
                else
                    return new { status = "410", msg = "確認失败！" };
            }
            else
                return new { status = "410", msg = "未定义的檢驗種類！" };
        }

        /// <summary>
        /// 制程检验确认-檢驗單作廢
        /// SAM 2017年7月9日21:56:51
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public static object QCS00006Cancel(JObject request)
        {
            string Token = request.Value<string>("Token");
            string userid = UtilBussinessService.detoken(Token);

            QCS_InspectionDocument model = QCS_InspectionDocumentService.get(request.Value<string>("InspectionDocumentID"));
            if (model == null)
                return new { status = "410", msg = "流水号错误！" };

            if (model.Status != Framework.SystemID + "020121300008D")
                return new { status = "410", msg = "工单并非处于开单状态！" };


            if (model.InspectionMethod == Framework.SystemID + "0201213000080" || model.InspectionMethod == Framework.SystemID + "0201213000081")
            {
                model.Status = Framework.SystemID + "020121300008F";
                if (QCS_InspectionDocumentService.update(userid, model))
                    return new { status = "200", msg = "作廢成功！" };
                else
                    return new { status = "410", msg = "作廢失败！" };
            }
            else if (model.InspectionMethod == Framework.SystemID + "020121300007E")
            {

                model.Status = Framework.SystemID + "020121300008F";
                //b.需扣除製程完工回報檔之檢驗數量。(依照檢驗單之完工單串回完工回報檔扣除檢驗數量)
                if (QCS_InspectionDocumentService.update(userid, model))
                    return new { status = "200", msg = "作廢成功！" };
                else
                    return new { status = "410", msg = "作廢失败！" };
            }
            else
                return new { status = "410", msg = "未定义的檢驗種類！" };
        }
        #endregion

        #region QCS00007制程首件检验维护
        /// <summary>
        /// 获取制程检验单列表
        /// Joint
        /// </summary>
        /// <param name="Token"></param>
        /// <param name="InspectionNo"></param>
        /// <param name="Status"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <returns></returns>
        public static object Qcs00007GetList(string Token, string InspectionNo, string Status, int page, int rows)
        {
            int count = 0;
            return UtilBussinessService.getPaginationModel(QCS_InspectionDocumentService.Qcs00007GetList(InspectionNo, Status, page, rows, ref count), count);
        }

        /// <summary>
        /// 新增--新增时明细新增尚有错误先注释掉
        /// Joint
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static object Qcs00007Add(JObject data)
        {
            string Token = data.Value<string>("Token");
            string userid = UtilBussinessService.detoken(Token);
            string SysID = Framework.SystemID;
            int success = 0, fill = 0;
            string fillmsg = null;
            string fillmsg2 = null;
            decimal sum = 0;
            try
            {

                if (string.IsNullOrWhiteSpace(data.Value<string>("InspectionNo")))
                    return new { status = "410", msg = "单据编号不能为空！" };
                /*
                //判定方式，先查找对应检验项目判定方式是什么
                MES_Parameter model2 = new MES_Parameter();//创建Inf00020实体
                model2.ParameterID = "100391101213000003";//赋值对应的检验项目判定方式流水号
                model2 = MES_ParameterService.get(model2.ParameterID);//获取对应实体
                MES_Parameter model3 = new MES_Parameter();//品质判定方式
                model3.ParameterID = "100391101213000002";
                model3 = MES_ParameterService.get(model3.ParameterID);
                */

                /*SAM 2017年9月9日23:16:28  调整*/
                MES_Parameter model2 = MES_ParameterService.get(SysID + "1101213000003");//获取对应检验项目判定方式实体
                MES_Parameter model3 = MES_ParameterService.get(SysID + "1101213000002");//获取对应品质判定方式实体

                if (model2 == null)
                    return new { status = "410", msg = "不存在检验项目判定方式的设定，请优先设定" };

                if (model3 == null)
                    return new { status = "410", msg = "不存在品质判定方式的设定，请优先设定" };

                QCS_InspectionDocument model = new QCS_InspectionDocument();
                model.InspectionDocumentID = UniversalService.GetSerialNumber("QCS_InspectionDocument");
                model.InspectionNo = data.Value<string>("InspectionNo");
                if (!string.IsNullOrWhiteSpace(data.Value<string>("DocumentDate")))
                    model.DocumentDate = data.Value<DateTime>("DocumentDate");
                model.InspectionMethod = data.Value<string>("InspectionMethod");
                model.CompletionOrderID = data.Value<string>("CompletionOrderID");
                model.ItemID = data.Value<string>("ItemID");
                model.TaskDispatchID = data.Value<string>("TaskDispatchID");
                if (!string.IsNullOrWhiteSpace(data.Value<string>("InspectionDate")))
                    model.InspectionDate = data.Value<DateTime>("InspectionDate");
                model.InspectionUserID = data.Value<string>("InspectionUserID");
                model.FinQuantity = data.Value<decimal>("FinQuantity");

                if (!string.IsNullOrWhiteSpace(data.Value<string>("InspectionQuantity")) || data.Value<decimal>("InspectionQuantity") < 0)
                {
                    model.InspectionQuantity = data.Value<decimal>("InspectionQuantity");//检验数量？
                }
                else
                {
                    return new { status = "410", msg = "新增失败！检验数量不可为空也不可小于0！" };
                }

                model.ScrappedQuantity = data.Value<decimal>("ScrappedQuantity");//报废数量
                model.NGquantity = data.Value<decimal>("NGquantity");//NG数量
                model.OKQuantity = data.Value<decimal>("OKQuantity");//OK数量
                sum = model.ScrappedQuantity + model.NGquantity + model.OKQuantity;//三个数量加起来是否等于检验数量
                if (sum != model.InspectionQuantity)
                {
                    return new { status = "410", msg = "新增失败！OK数量+NG数量+报废数量不等于检验数量！" };
                }

                model.InspectionFlag = data.Value<bool>("InspectionFlag");
                model.Status = data.Value<string>("Status");
                model.Comments = data.Value<string>("Comments");

                if (model3.Value != Framework.SystemID + "02012130000B1")//如果品质判定方式不为自动，那么由表头带入判定
                {
                    model.QualityControlDecision = data.Value<string>("QcDecision");
                }
                else
                {
                    model.QualityControlDecision = Framework.SystemID + "0201213000091";//自动判定时，默认为允收
                }

                //通过完工单号拿到对应的制程与工序流水号
                SFC_TaskDispatch TdModel = SFC_TaskDispatchService.get(model.TaskDispatchID);
                if (TdModel == null)
                {
                    return new { Status = "400", msg = "对应任务单号不存在！" };
                }
                string AutoNumberID = data.Value<string>("DocumentAutoNumberID");
                //查询所新增的检验单号是否已存在
                while (QCS_InspectionDocumentService.CheckInspectionNo(model.InspectionNo))
                {
                    AutoNumberID = null;
                    model.InspectionNo = UtilBussinessService.GetDocumentAutoNumber(userid, data.Value<string>("DocumentID"), model.InspectionDate.ToString(), ref AutoNumberID);
                }

                if (QCS_InspectionDocumentService.insert(userid, model))
                {
                    UtilBussinessService.UpdateDocumentAutoNumber(userid, AutoNumberID);
                    //如果检验单据成功添加那么根据对应的料品号与检验种类获取明细设定列表，规格表说明不足新添加条件，工序与制程
                    List<QCS_StaInsSpeSetting> StaInsSpeSetting = QCS_StaInsSpeSettingService.GetListByItemID(model.ItemID, model.InspectionMethod, TdModel.ProcessID, TdModel.OperationID);
                    if (StaInsSpeSetting == null)
                        return new { Status = "400", msg = "表头新增成功，但明细新增失败(没有对应的Qcs00004数据)" };
                    //检验明细新增
                    foreach (QCS_StaInsSpeSetting Sta in StaInsSpeSetting)
                    {
                        QCS_InspectionProject Sampling = QCS_InspectionProjectService.get(Sta.InspectionProjectID);
                        if (Sampling == null)
                            return new { Status = "400", msg = "表头新增成功，但明细新增失败(没有对应的检验项目数据)" };
                        QCS_InspectionDocumentDetails DetailsModel = new QCS_InspectionDocumentDetails();
                        DetailsModel.InspectionDocumentDetailID = UniversalService.GetSerialNumber("QCS_InspectionDocumentDetails");
                        DetailsModel.InspectionDocumentID = model.InspectionDocumentID;
                        DetailsModel.Sequence = Sta.Sequence;//排序
                        DetailsModel.InspectionStandard = Sta.InspectionStandard;//检验标准
                        DetailsModel.InspectionItemID = Sta.InspectionProjectID;//检验项目流水号用于获取项目代号与说明
                        DetailsModel.InspectionMethod = Sta.InspectionType;//检验种类
                        DetailsModel.InspectionClassID = Sta.CategoryID;//检验类别
                        DetailsModel.InspectionMethodID = Sta.InspectionMethod;//检验方式
                        DetailsModel.InspectionLevelID = Sampling.InspectionLevel;//检验水准
                        DetailsModel.InspectionFaultID = Sampling.Disadvantages;//缺点等级
                        DetailsModel.Status = Framework.SystemID + "0201213000001";
                        DetailsModel.Aql = Sta.AQL;
                        DetailsModel.AttributeType = Sta.Attribute;
                        //当没有AQL值默认为0
                        if (Sta.AQL == null)
                        {
                            DetailsModel.SampleQuantity = 0;
                            DetailsModel.AcQuantity = 0;
                            DetailsModel.ReQuantity = 0;
                        }
                        //当QCS000001对应的数据没有时，AC,RE,抽样数量设置为0
                        else
                        {
                            QCS_CheckTestSetting Check = QCS_CheckTestSettingService.getByAql(DetailsModel.InspectionLevelID, DetailsModel.InspectionMethodID, Sta.AQL);//获取抽检检验设定实体
                            if (Check == null)
                            {
                                fill++;
                                fillmsg = "部分新增失败，QCS000001没有与检验方式，检验水平，AQL对应的数据，AC,RE，抽样数量将默认为0";
                                DetailsModel.SampleQuantity = 0;
                                DetailsModel.AcQuantity = 0;
                                DetailsModel.ReQuantity = 0;
                            }
                            else
                            {
                                //当QCS000001对应的明细数据没有时，AC,RE,抽样数量设置为0
                                QCS_CheckTestSettingDetails CheckDetail = QCS_CheckTestSettingDetailsService.getCTSDetails(Check.CheckTestSettingID);//获取抽检检验设定明细实体
                                if (CheckDetail == null)
                                {
                                    fill++;
                                    fillmsg2 = "有部分QCS000001明细没有与检验方式，检验水平，AQL对应的明细数据，AC,RE，抽样数量将默认为0";
                                    DetailsModel.SampleQuantity = 0;
                                    DetailsModel.AcQuantity = 0;
                                    DetailsModel.ReQuantity = 0;
                                }
                                //decimal p = model.InspectionQuantity / CheckDetail.EndBatch;//检验数量自动计算？检验数量除以结束批量？
                                //DetailsModel.SampleQuantity = p * CheckDetail.SamplingQuantity;
                                //DetailsModel.AcQuantity = p * CheckDetail.AcQuantity;
                                //DetailsModel.ReQuantity = p * CheckDetail.ReQuantity;
                                //不确定P的计算方式，暂时注释掉
                                else
                                {
                                    DetailsModel.SampleQuantity = CheckDetail.SamplingQuantity;
                                    DetailsModel.AcQuantity = CheckDetail.AcQuantity;
                                    DetailsModel.ReQuantity = CheckDetail.ReQuantity;
                                }
                            }
                        }
                        DetailsModel.NGquantity = 0;//不良数？

                        if (model2.Value == Framework.SystemID + "02012130000B3")//查看系统设置的判定方式
                        {
                            if (DetailsModel.NGquantity >= DetailsModel.ReQuantity)
                                DetailsModel.QualityControlDecision = Framework.SystemID + "0201213000090";
                            else
                                DetailsModel.QualityControlDecision = Framework.SystemID + "0201213000091";
                        }
                        else
                        {
                            DetailsModel.QualityControlDecision = "人工判定";
                        }
                        if (model3.Value == Framework.SystemID + "02012130000B1" && DetailsModel.QualityControlDecision == Framework.SystemID + "0201213000090")
                        {//品质判定为自动且新增的明细判定为拒收
                            model.QualityControlDecision = Framework.SystemID + "0201213000090";//假如系统判定方式为自动，明细有一个为拒收，那么表头也为拒收

                            QCS_InspectionDocumentService.update(userid, model);//更新表头数据
                        }

                        QCS_InspectionDocumentDetailsService.insert(userid, DetailsModel);
                        success++;

                    }


                    //return new { status = "200", msg = "新增成功！" + fillmsg + fillmsg2 + "失败数量为：" + fill, success, fill };
                    return new { status = "200", msg = "新增成功！" };
                }
                else
                    return new { status = "410", msg = "新增失败！" };
            }
            catch (Exception ex)
            {
                DataLogerService.writeerrlog(ex);
                return new { status = "410", msg = "表头新增成功！但明细为空！" };
            }
        }

        /// <summary>
        /// 新增制程检验单
        /// Sam 2017年10月19日11:41:43
        /// 调整了检验单明细的获取逻辑，优先判断群码再料品
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static object Qcs00007AddV1(JObject data)
        {
            string Token = data.Value<string>("Token");
            string userid = UtilBussinessService.detoken(Token);
            string SysID = Framework.SystemID;
            int success = 0, fill = 0;
            string fillmsg = null;
            string fillmsg2 = null;
            decimal sum = 0;
            try
            {

                if (string.IsNullOrWhiteSpace(data.Value<string>("InspectionNo")))
                    return new { status = "410", msg = "单据编号不能为空！" };
                /*
                //判定方式，先查找对应检验项目判定方式是什么
                MES_Parameter model2 = new MES_Parameter();//创建Inf00020实体
                model2.ParameterID = "100391101213000003";//赋值对应的检验项目判定方式流水号
                model2 = MES_ParameterService.get(model2.ParameterID);//获取对应实体
                MES_Parameter model3 = new MES_Parameter();//品质判定方式
                model3.ParameterID = "100391101213000002";
                model3 = MES_ParameterService.get(model3.ParameterID);
                */

                /*SAM 2017年9月9日23:16:28  调整*/
                MES_Parameter model2 = MES_ParameterService.get(SysID + "1101213000003");//获取对应检验项目判定方式实体
                MES_Parameter model3 = MES_ParameterService.get(SysID + "1101213000002");//获取对应品质判定方式实体

                if (model2 == null)
                    return new { status = "410", msg = "不存在检验项目判定方式的设定，请优先设定" };

                if (model3 == null)
                    return new { status = "410", msg = "不存在品质判定方式的设定，请优先设定" };

                QCS_InspectionDocument model = new QCS_InspectionDocument();
                model.InspectionDocumentID = UniversalService.GetSerialNumber("QCS_InspectionDocument");
                model.InspectionNo = data.Value<string>("InspectionNo");
                if (!string.IsNullOrWhiteSpace(data.Value<string>("DocumentDate")))
                    model.DocumentDate = data.Value<DateTime>("DocumentDate");
                model.InspectionMethod = data.Value<string>("InspectionMethod");
                model.CompletionOrderID = data.Value<string>("CompletionOrderID");
                model.ItemID = data.Value<string>("ItemID");
                model.TaskDispatchID = data.Value<string>("TaskDispatchID");
                if (!string.IsNullOrWhiteSpace(data.Value<string>("InspectionDate")))
                    model.InspectionDate = data.Value<DateTime>("InspectionDate");
                model.InspectionUserID = data.Value<string>("InspectionUserID");
                model.FinQuantity = data.Value<decimal>("FinQuantity");

                if (!string.IsNullOrWhiteSpace(data.Value<string>("InspectionQuantity")) || data.Value<decimal>("InspectionQuantity") < 0)
                {
                    model.InspectionQuantity = data.Value<decimal>("InspectionQuantity");//检验数量？
                }
                else
                {
                    return new { status = "410", msg = "新增失败！检验数量不可为空也不可小于0！" };
                }

                model.ScrappedQuantity = data.Value<decimal>("ScrappedQuantity");//报废数量
                model.NGquantity = data.Value<decimal>("NGquantity");//NG数量
                model.OKQuantity = data.Value<decimal>("OKQuantity");//OK数量
                sum = model.ScrappedQuantity + model.NGquantity + model.OKQuantity;//三个数量加起来是否等于检验数量
                if (sum != model.InspectionQuantity)
                {
                    return new { status = "410", msg = "新增失败！OK数量+NG数量+报废数量不等于检验数量！" };
                }

                model.InspectionFlag = data.Value<bool>("InspectionFlag");
                model.Status = data.Value<string>("Status");
                model.Comments = data.Value<string>("Comments");

                if (model3.Value != Framework.SystemID + "02012130000B1")//如果品质判定方式不为自动，那么由表头带入判定
                {
                    model.QualityControlDecision = data.Value<string>("QcDecision");
                }
                else
                {
                    model.QualityControlDecision = Framework.SystemID + "0201213000091";//自动判定时，默认为允收
                }

                //通过完工单号拿到对应的制程与工序流水号
                SFC_TaskDispatch TdModel = SFC_TaskDispatchService.get(model.TaskDispatchID);
                if (TdModel == null)
                {
                    return new { Status = "400", msg = "对应任务单号不存在！" };
                }
                string AutoNumberID = data.Value<string>("DocumentAutoNumberID");
                //查询所新增的检验单号是否已存在
                while (QCS_InspectionDocumentService.CheckInspectionNo(model.InspectionNo))
                {
                    AutoNumberID = null;
                    model.InspectionNo = UtilBussinessService.GetDocumentAutoNumber(userid, data.Value<string>("DocumentID"), model.InspectionDate.ToString(), ref AutoNumberID);
                }
                if (QCS_InspectionDocumentService.insert(userid, model))
                {
                    UtilBussinessService.UpdateDocumentAutoNumber(userid, AutoNumberID);
                    //如果检验单据成功添加那么根据对应的料品号与检验种类获取明细设定列表，规格表说明不足新添加条件，工序与制程
                    SYS_Items ItemModel = SYS_ItemsService.get(model.ItemID);
                    if (ItemModel == null)
                        return new { Status = "400", msg = "对应的料品代号错误" };
                    List<QCS_StaInsSpeSetting> StaInsSpeSetting = null;

                    if (!string.IsNullOrWhiteSpace(ItemModel.GroupID))
                        StaInsSpeSetting = QCS_StaInsSpeSettingService.GetListByItemID(ItemModel.GroupID, model.InspectionMethod, TdModel.ProcessID, TdModel.OperationID);
                    //如果不存在检验码或者说检验码并没有对应明细
                    if (StaInsSpeSetting == null)
                        StaInsSpeSetting = QCS_StaInsSpeSettingService.GetListByItemID(model.ItemID, model.InspectionMethod, TdModel.ProcessID, TdModel.OperationID);

                    if (StaInsSpeSetting == null)
                        return new { Status = "400", msg = "表头新增成功，但明细新增失败(没有对应的Qcs00004数据)" };
                    //检验明细新增
                    foreach (QCS_StaInsSpeSetting Sta in StaInsSpeSetting)
                    {
                        QCS_InspectionProject Sampling = QCS_InspectionProjectService.get(Sta.InspectionProjectID);
                        if (Sampling == null)
                            return new { Status = "400", msg = "表头新增成功，但明细新增失败(没有对应的检验项目数据)" };
                        QCS_InspectionDocumentDetails DetailsModel = new QCS_InspectionDocumentDetails();
                        DetailsModel.InspectionDocumentDetailID = UniversalService.GetSerialNumber("QCS_InspectionDocumentDetails");
                        DetailsModel.InspectionDocumentID = model.InspectionDocumentID;
                        DetailsModel.Sequence = Sta.Sequence;//排序
                        DetailsModel.InspectionStandard = Sta.InspectionStandard;//检验标准
                        DetailsModel.InspectionItemID = Sta.InspectionProjectID;//检验项目流水号用于获取项目代号与说明
                        DetailsModel.InspectionMethod = Sta.InspectionType;//检验种类
                        DetailsModel.InspectionClassID = Sta.CategoryID;//检验类别
                        DetailsModel.InspectionMethodID = Sta.InspectionMethod;//检验方式
                        DetailsModel.InspectionLevelID = Sampling.InspectionLevel;//检验水准
                        DetailsModel.InspectionFaultID = Sampling.Disadvantages;//缺点等级
                        DetailsModel.Status = Framework.SystemID + "0201213000001";
                        DetailsModel.Aql = Sta.AQL;
                        DetailsModel.AttributeType = Sta.Attribute;
                        //当没有AQL值默认为0
                        if (Sta.AQL == null)
                        {
                            DetailsModel.SampleQuantity = 0;
                            DetailsModel.AcQuantity = 0;
                            DetailsModel.ReQuantity = 0;
                        }
                        //当QCS000001对应的数据没有时，AC,RE,抽样数量设置为0
                        else
                        {
                            QCS_CheckTestSetting Check = QCS_CheckTestSettingService.getByAql(DetailsModel.InspectionLevelID, DetailsModel.InspectionMethodID, Sta.AQL);//获取抽检检验设定实体
                            if (Check == null)
                            {
                                fill++;
                                fillmsg = "部分新增失败，QCS000001没有与检验方式，检验水平，AQL对应的数据，AC,RE，抽样数量将默认为0";
                                DetailsModel.SampleQuantity = 0;
                                DetailsModel.AcQuantity = 0;
                                DetailsModel.ReQuantity = 0;
                            }
                            else
                            {
                                //当QCS000001对应的明细数据没有时，AC,RE,抽样数量设置为0
                                QCS_CheckTestSettingDetails CheckDetail = QCS_CheckTestSettingDetailsService.getCTSDetails(Check.CheckTestSettingID);//获取抽检检验设定明细实体
                                if (CheckDetail == null)
                                {
                                    fill++;
                                    fillmsg2 = "有部分QCS000001明细没有与检验方式，检验水平，AQL对应的明细数据，AC,RE，抽样数量将默认为0";
                                    DetailsModel.SampleQuantity = 0;
                                    DetailsModel.AcQuantity = 0;
                                    DetailsModel.ReQuantity = 0;
                                }
                                //decimal p = model.InspectionQuantity / CheckDetail.EndBatch;//检验数量自动计算？检验数量除以结束批量？
                                //DetailsModel.SampleQuantity = p * CheckDetail.SamplingQuantity;
                                //DetailsModel.AcQuantity = p * CheckDetail.AcQuantity;
                                //DetailsModel.ReQuantity = p * CheckDetail.ReQuantity;
                                //不确定P的计算方式，暂时注释掉
                                else
                                {
                                    DetailsModel.SampleQuantity = CheckDetail.SamplingQuantity;
                                    DetailsModel.AcQuantity = CheckDetail.AcQuantity;
                                    DetailsModel.ReQuantity = CheckDetail.ReQuantity;
                                }
                            }
                        }
                        DetailsModel.NGquantity = 0;//不良数？

                        if (model2.Value == Framework.SystemID + "02012130000B3")//查看系统设置的判定方式
                        {
                            if (DetailsModel.NGquantity >= DetailsModel.ReQuantity)
                                DetailsModel.QualityControlDecision = Framework.SystemID + "0201213000090";
                            else
                                DetailsModel.QualityControlDecision = Framework.SystemID + "0201213000091";
                        }
                        else
                        {
                            DetailsModel.QualityControlDecision = "人工判定";
                        }
                        if (model3.Value == Framework.SystemID + "02012130000B1" && DetailsModel.QualityControlDecision == Framework.SystemID + "0201213000090")
                        {//品质判定为自动且新增的明细判定为拒收
                            model.QualityControlDecision = Framework.SystemID + "0201213000090";//假如系统判定方式为自动，明细有一个为拒收，那么表头也为拒收

                            QCS_InspectionDocumentService.update(userid, model);//更新表头数据
                        }

                        QCS_InspectionDocumentDetailsService.insert(userid, DetailsModel);
                        success++;

                    }


                    //return new { status = "200", msg = "新增成功！" + fillmsg + fillmsg2 + "失败数量为：" + fill, success, fill };
                    return new { status = "200", msg = "新增成功！" };
                }
                else
                    return new { status = "410", msg = "新增失败！" };
            }
            catch (Exception ex)
            {
                DataLogerService.writeerrlog(ex);
                return new { status = "410", msg = "表头新增成功！但明细为空！" };
            }
        }

        /// <summary>
        /// 新增制程检验单
        /// SAM 2017年10月23日17:41:23
        ///  在V1的版本上调整了部分逻辑。在寻找抽样检验设定资料时，加多了分派量是否在范围内的判断
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static object Qcs00007AddV2(JObject data)
        {
            string Token = data.Value<string>("Token");
            string userid = UtilBussinessService.detoken(Token);
            string SysID = Framework.SystemID;
            decimal sum = 0;
            try
            {

                if (string.IsNullOrWhiteSpace(data.Value<string>("InspectionNo")))
                    return new { status = "410", msg = "单据编号不能为空！" };

                //判定方式，先查找对应检验项目判定方式是什么
                /*SAM 2017年9月9日23:16:28  调整*/
                MES_Parameter model2 = MES_ParameterService.get(SysID + "1101213000003");//获取对应检验项目判定方式实体
                MES_Parameter model3 = MES_ParameterService.get(SysID + "1101213000002");//获取对应品质判定方式实体

                if (model2 == null)
                    return new { status = "410", msg = "不存在检验项目判定方式的设定，请优先设定" };

                if (model3 == null)
                    return new { status = "410", msg = "不存在品质判定方式的设定，请优先设定" };

                QCS_InspectionDocument model = new QCS_InspectionDocument();
                model.InspectionDocumentID = UniversalService.GetSerialNumber("QCS_InspectionDocument");
                model.InspectionNo = data.Value<string>("InspectionNo");
                if (!string.IsNullOrWhiteSpace(data.Value<string>("DocumentDate")))
                    model.DocumentDate = data.Value<DateTime>("DocumentDate");
                model.InspectionMethod = data.Value<string>("InspectionMethod");
                model.CompletionOrderID = data.Value<string>("CompletionOrderID");
                model.ItemID = data.Value<string>("ItemID");
                model.TaskDispatchID = data.Value<string>("TaskDispatchID");
                if (!string.IsNullOrWhiteSpace(data.Value<string>("InspectionDate")))
                    model.InspectionDate = data.Value<DateTime>("InspectionDate");
                model.InspectionUserID = data.Value<string>("InspectionUserID");
                model.FinQuantity = data.Value<decimal>("FinQuantity");

                if (string.IsNullOrWhiteSpace(data.Value<string>("InspectionQuantity")))
                    return new { status = "410", msg = "新增失败！检验数量不能为空！" };

                if (data.Value<decimal>("InspectionQuantity") < 0)
                    return new { status = "410", msg = "新增失败！检验数量不不可小于0！" };

                model.InspectionQuantity = data.Value<decimal>("InspectionQuantity");//检验数量
                model.ScrappedQuantity = data.Value<decimal>("ScrappedQuantity");//报废数量
                model.NGquantity = data.Value<decimal>("NGquantity");//NG数量
                model.OKQuantity = data.Value<decimal>("OKQuantity");//OK数量
                sum = model.ScrappedQuantity + model.NGquantity + model.OKQuantity;//三个数量加起来是否等于检验数量
                if (sum != model.InspectionQuantity)
                {
                    return new { status = "410", msg = "新增失败！OK数量+NG数量+报废数量不等于检验数量！" };
                }

                model.InspectionFlag = data.Value<bool>("InspectionFlag");
                model.Status = data.Value<string>("Status");
                model.Comments = data.Value<string>("Comments");

                if (model3.Value != Framework.SystemID + "02012130000B1")//如果品质判定方式不为自动，那么由表头带入判定
                {
                    model.QualityControlDecision = data.Value<string>("QcDecision");
                }
                else
                {
                    model.QualityControlDecision = Framework.SystemID + "0201213000091";//自动判定时，默认为允收
                }

                //通过完工单号拿到对应的制程与工序流水号
                SFC_TaskDispatch TdModel = SFC_TaskDispatchService.get(model.TaskDispatchID);
                if (TdModel == null)
                {
                    return new { Status = "400", msg = "对应任务单号不存在！" };
                }
                string AutoNumberID = data.Value<string>("DocumentAutoNumberID");
                //查询所新增的检验单号是否已存在
                while (QCS_InspectionDocumentService.CheckInspectionNo(model.InspectionNo))
                {
                    AutoNumberID = null;
                    model.InspectionNo = UtilBussinessService.GetDocumentAutoNumber(userid, data.Value<string>("DocumentID"), model.InspectionDate.ToString(), ref AutoNumberID);
                }
                if (QCS_InspectionDocumentService.insert(userid, model))
                {
                    UtilBussinessService.UpdateDocumentAutoNumber(userid, AutoNumberID);
                    //如果检验单据成功添加那么根据对应的料品号与检验种类获取明细设定列表，规格表说明不足新添加条件，工序与制程
                    SYS_Items ItemModel = SYS_ItemsService.get(model.ItemID);
                    if (ItemModel == null)
                        return new { Status = "400", msg = "对应的料品代号错误" };
                    List<QCS_StaInsSpeSetting> StaInsSpeSetting = null;

                    if (!string.IsNullOrWhiteSpace(ItemModel.GroupID))
                        StaInsSpeSetting = QCS_StaInsSpeSettingService.GetListByItemID(ItemModel.GroupID, model.InspectionMethod, TdModel.ProcessID, TdModel.OperationID);
                    //如果不存在检验码或者说检验码并没有对应明细
                    if (StaInsSpeSetting == null)
                        StaInsSpeSetting = QCS_StaInsSpeSettingService.GetListByItemID(model.ItemID, model.InspectionMethod, TdModel.ProcessID, TdModel.OperationID);

                    if (StaInsSpeSetting == null)
                        return new { Status = "400", msg = "表头新增成功，但明细新增失败(没有对应的Qcs00004数据)" };
                    //检验明细新增
                    foreach (QCS_StaInsSpeSetting Sta in StaInsSpeSetting)
                    {
                        QCS_InspectionProject Sampling = QCS_InspectionProjectService.get(Sta.InspectionProjectID);
                        if (Sampling == null)
                            return new { Status = "400", msg = "表头新增成功，但明细新增失败(没有对应的检验项目数据)" };
                        QCS_InspectionDocumentDetails DetailsModel = new QCS_InspectionDocumentDetails();
                        DetailsModel.InspectionDocumentDetailID = UniversalService.GetSerialNumber("QCS_InspectionDocumentDetails");
                        DetailsModel.InspectionDocumentID = model.InspectionDocumentID;
                        DetailsModel.Sequence = Sta.Sequence;//排序
                        DetailsModel.InspectionStandard = Sta.InspectionStandard;//检验标准
                        DetailsModel.InspectionItemID = Sta.InspectionProjectID;//检验项目流水号用于获取项目代号与说明
                        DetailsModel.InspectionMethod = Sta.InspectionType;//检验种类
                        DetailsModel.InspectionClassID = Sta.CategoryID;//检验类别
                        DetailsModel.InspectionMethodID = Sta.InspectionMethod;//检验方式
                        DetailsModel.InspectionLevelID = Sampling.InspectionLevel;//检验水准
                        DetailsModel.InspectionFaultID = Sampling.Disadvantages;//缺点等级
                        DetailsModel.Status = Framework.SystemID + "0201213000001";
                        DetailsModel.Aql = Sta.AQL;
                        DetailsModel.AttributeType = Sta.Attribute;
                        //当没有AQL值默认为0
                        if (Sta.AQL == null)
                        {
                            DetailsModel.SampleQuantity = 0;
                            DetailsModel.AcQuantity = 0;
                            DetailsModel.ReQuantity = 0;
                        }
                        //当QCS000001对应的数据没有时，AC,RE,抽样数量设置为0
                        else
                        {
                            QCS_CheckTestSetting Check = QCS_CheckTestSettingService.getByAql(DetailsModel.InspectionLevelID, DetailsModel.InspectionMethodID, Sta.AQL);//获取抽检检验设定实体
                            if (Check == null)
                            {
                                //fill++;
                                //fillmsg = "部分新增失败，QCS000001没有与检验方式，检验水平，AQL对应的数据，AC,RE，抽样数量将默认为0";
                                DetailsModel.SampleQuantity = 0;
                                DetailsModel.AcQuantity = 0;
                                DetailsModel.ReQuantity = 0;
                            }
                            else
                            {
                                //当QCS000001对应的明细数据没有时，AC,RE,抽样数量设置为0
                                QCS_CheckTestSettingDetails CheckDetail = QCS_CheckTestSettingDetailsService.GetDetails(Check.CheckTestSettingID, TdModel.DispatchQuantity);//获取抽检检验设定明细实体
                                //QCS_CheckTestSettingDetails CheckDetail = QCS_CheckTestSettingDetailsService.getCTSDetails(Check.CheckTestSettingID);//获取抽检检验设定明细实体
                                if (CheckDetail == null)
                                {
                                    //fill++;
                                    //fillmsg2 = "有部分QCS000001明细没有与检验方式，检验水平，AQL对应的明细数据，AC,RE，抽样数量将默认为0";
                                    DetailsModel.SampleQuantity = 0;
                                    DetailsModel.AcQuantity = 0;
                                    DetailsModel.ReQuantity = 0;
                                }
                                //decimal p = model.InspectionQuantity / CheckDetail.EndBatch;//检验数量自动计算？检验数量除以结束批量？
                                //DetailsModel.SampleQuantity = p * CheckDetail.SamplingQuantity;
                                //DetailsModel.AcQuantity = p * CheckDetail.AcQuantity;
                                //DetailsModel.ReQuantity = p * CheckDetail.ReQuantity;
                                //不确定P的计算方式，暂时注释掉
                                else
                                {
                                    DetailsModel.SampleQuantity = CheckDetail.SamplingQuantity;
                                    DetailsModel.AcQuantity = CheckDetail.AcQuantity;
                                    DetailsModel.ReQuantity = CheckDetail.ReQuantity;
                                }
                            }
                        }
                        DetailsModel.NGquantity = 0;//不良数？

                        if (model2.Value == Framework.SystemID + "02012130000B3")//查看系统设置的判定方式
                        {
                            if (DetailsModel.NGquantity >= DetailsModel.ReQuantity)
                                DetailsModel.QualityControlDecision = Framework.SystemID + "0201213000090";
                            else
                                DetailsModel.QualityControlDecision = Framework.SystemID + "0201213000091";
                        }
                        else
                        {
                            DetailsModel.QualityControlDecision = "人工判定";
                        }
                        if (model3.Value == Framework.SystemID + "02012130000B1" && DetailsModel.QualityControlDecision == Framework.SystemID + "0201213000090")
                        {//品质判定为自动且新增的明细判定为拒收
                            model.QualityControlDecision = Framework.SystemID + "0201213000090";//假如系统判定方式为自动，明细有一个为拒收，那么表头也为拒收

                            QCS_InspectionDocumentService.update(userid, model);//更新表头数据
                        }

                        QCS_InspectionDocumentDetailsService.insert(userid, DetailsModel);
                    }


                    //return new { status = "200", msg = "新增成功！" + fillmsg + fillmsg2 + "失败数量为：" + fill, success, fill };
                    return new { status = "200", msg = "新增成功！" };
                }
                else
                    return new { status = "410", msg = "新增失败！" };
            }
            catch (Exception ex)
            {
                DataLogerService.writeerrlog(ex);
                return new { status = "410", msg = "表头新增成功！但明细为空！" };
            }
        }


        /// <summary>
        /// 新增制程检验单
        /// SAM 2017年10月23日17:41:23
        /// 在V2的版本上，调整了代码，修正了一些bug.
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static object Qcs00007AddV3(JObject data)
        {
            string Token = data.Value<string>("Token");
            string userid = UtilBussinessService.detoken(Token);
            string SysID = Framework.SystemID;
            decimal sum = 0;
            try
            {

                if (string.IsNullOrWhiteSpace(data.Value<string>("InspectionNo")))
                    return new { status = "410", msg = "单据编号不能为空！" };

                //判定方式，先查找对应检验项目判定方式是什么
                /*SAM 2017年9月9日23:16:28  调整*/
                MES_Parameter model2 = MES_ParameterService.get(SysID + "1101213000003");//获取对应检验项目判定方式实体
                MES_Parameter model3 = MES_ParameterService.get(SysID + "1101213000002");//获取对应品质判定方式实体

                if (model2 == null)
                    return new { status = "410", msg = "不存在检验项目判定方式的设定，请优先设定" };

                if (model3 == null)
                    return new { status = "410", msg = "不存在品质判定方式的设定，请优先设定" };

                QCS_InspectionDocument model = new QCS_InspectionDocument();
                model.InspectionDocumentID = UniversalService.GetSerialNumber("QCS_InspectionDocument");
                model.InspectionNo = data.Value<string>("InspectionNo");
                if (!string.IsNullOrWhiteSpace(data.Value<string>("DocumentDate")))
                    model.DocumentDate = data.Value<DateTime>("DocumentDate");
                model.InspectionMethod = data.Value<string>("InspectionMethod");
                model.CompletionOrderID = data.Value<string>("CompletionOrderID");
                model.ItemID = data.Value<string>("ItemID");
                model.TaskDispatchID = data.Value<string>("TaskDispatchID");
                if (!string.IsNullOrWhiteSpace(data.Value<string>("InspectionDate")))
                    model.InspectionDate = data.Value<DateTime>("InspectionDate");
                model.InspectionUserID = data.Value<string>("InspectionUserID");
                model.FinQuantity = data.Value<decimal>("FinQuantity");

                if (string.IsNullOrWhiteSpace(data.Value<string>("InspectionQuantity")))
                    return new { status = "410", msg = "新增失败！检验数量不能为空！" };

                if (data.Value<decimal>("InspectionQuantity") < 0)
                    return new { status = "410", msg = "新增失败！检验数量不不可小于0！" };

                model.InspectionQuantity = data.Value<decimal>("InspectionQuantity");//检验数量
                model.ScrappedQuantity = data.Value<decimal>("ScrappedQuantity");//报废数量
                model.NGquantity = data.Value<decimal>("NGquantity");//NG数量
                model.OKQuantity = data.Value<decimal>("OKQuantity");//OK数量
                sum = model.ScrappedQuantity + model.NGquantity + model.OKQuantity;//三个数量加起来是否等于检验数量
                if (sum != model.InspectionQuantity)
                {
                    return new { status = "410", msg = "新增失败！OK数量+NG数量+报废数量不等于检验数量！" };
                }

                model.InspectionFlag = data.Value<bool>("InspectionFlag");
                model.Status = data.Value<string>("Status");
                model.Comments = data.Value<string>("Comments");

                if (model3.Value != Framework.SystemID + "02012130000B1")//如果品质判定方式不为自动，那么由表头带入判定
                {
                    model.QualityControlDecision = Framework.SystemID + "0201213000091";//需求修改默认为允收
                }
                else
                {
                    model.QualityControlDecision = Framework.SystemID + "0201213000091";//自动判定时，默认为允收
                }

                //通过完工单号拿到对应的制程与工序流水号
                SFC_TaskDispatch TdModel = SFC_TaskDispatchService.get(model.TaskDispatchID);
                if (TdModel == null)
                {
                    return new { status = "410", msg = "对应任务单号不存在！" };
                }
                string AutoNumberID = data.Value<string>("DocumentAutoNumberID");
                //查询所新增的检验单号是否已存在
                //while (QCS_InspectionDocumentService.CheckInspectionNo(model.InspectionNo))
                //{
                //    AutoNumberID = null;
                //    model.InspectionNo = UtilBussinessService.GetDocumentAutoNumber(userid, data.Value<string>("DocumentID"), model.InspectionDate.ToString(), ref AutoNumberID);
                //}

                /*当保存时,如果检验单号已存在，则自动获取下一检验单号*/
                /*SAM 2017年10月27日16:36:36*/
                /*
                 * 因为存在一个情况是检验单号新增进去了但是并没有及时更新流水，造成了无限死循环。
                 * 所以目前做了一个机制，循环计数，如果循环超过5次，就更新一次流水,然后重置循环次数
                 */
                int Seq = 1;
                while (QCS_InspectionDocumentService.CheckInspectionNo(model.InspectionNo))
                {
                    if (Seq == 5)
                    {
                        UtilBussinessService.UpdateDocumentAutoNumber(userid, AutoNumberID);
                        Seq = 1;
                    }
                    else
                    {
                        AutoNumberID = null;
                        model.InspectionNo = UtilBussinessService.GetDocumentAutoNumber(userid, data.Value<string>("DocumentID"), model.InspectionDate.ToString(), ref AutoNumberID);
                        Seq++;
                    }
                }

                if (QCS_InspectionDocumentService.insert(userid, model))
                {
                    UtilBussinessService.UpdateDocumentAutoNumber(userid, AutoNumberID);
                    //如果检验单据成功添加那么根据对应的料品号与检验种类获取明细设定列表，规格表说明不足新添加条件，工序与制程
                    SYS_Items ItemModel = SYS_ItemsService.get(model.ItemID);
                    if (ItemModel == null)
                        return new { status = "400", msg = "对应的料品代号错误" };

                    List<QCS_StaInsSpeSetting> StaInsSpeSetting = null;
                    //如果存在检验码，则拿去检验码的明细
                    if (!string.IsNullOrWhiteSpace(ItemModel.GroupID))
                        StaInsSpeSetting = QCS_StaInsSpeSettingService.GetListByPartID(ItemModel.GroupID, model.InspectionMethod, Framework.SystemID + "020121300007C", TdModel.ProcessID, TdModel.OperationID);
                    //如果不存在检验码或者说检验码并没有对应明细
                    if (StaInsSpeSetting == null)
                        StaInsSpeSetting = QCS_StaInsSpeSettingService.GetListByPartID(model.ItemID, model.InspectionMethod, Framework.SystemID + "020121300007B", TdModel.ProcessID, TdModel.OperationID);

                    if (StaInsSpeSetting == null)
                        return new { status = "400", msg = "表头新增成功，但明细新增失败(没有对应的Qcs00004数据)" };
                    //检验明细新增
                    foreach (QCS_StaInsSpeSetting Sta in StaInsSpeSetting)
                    {
                        QCS_InspectionProject Sampling = QCS_InspectionProjectService.get(Sta.InspectionProjectID);
                        if (Sampling == null)
                            return new { status = "400", msg = "表头新增成功，但明细新增失败(没有对应的检验项目数据)" };
                        QCS_InspectionDocumentDetails DetailsModel = new QCS_InspectionDocumentDetails();
                        DetailsModel.InspectionDocumentDetailID = UniversalService.GetSerialNumber("QCS_InspectionDocumentDetails");
                        DetailsModel.InspectionDocumentID = model.InspectionDocumentID;
                        DetailsModel.Sequence = Sta.Sequence;//排序
                        DetailsModel.InspectionStandard = Sta.InspectionStandard;//检验标准
                        DetailsModel.InspectionItemID = Sta.InspectionProjectID;//检验项目流水号用于获取项目代号与说明
                        DetailsModel.InspectionMethod = Sta.InspectionType;//检验种类
                        DetailsModel.InspectionClassID = Sta.CategoryID;//检验类别
                        DetailsModel.InspectionMethodID = Sta.InspectionMethod;//检验方式
                        DetailsModel.InspectionLevelID = Sampling.InspectionLevel;//检验水准
                        DetailsModel.InspectionFaultID = Sampling.Disadvantages;//缺点等级
                        DetailsModel.Status = Framework.SystemID + "0201213000001";
                        DetailsModel.Aql = Sta.AQL;
                        DetailsModel.AttributeType = Sta.Attribute;
                        //当没有AQL值默认为0
                        if (Sta.AQL == null)
                        {
                            DetailsModel.SampleQuantity = 0;
                            DetailsModel.AcQuantity = 0;
                            DetailsModel.ReQuantity = 0;
                        }
                        //当QCS000001对应的数据没有时，AC,RE,抽样数量设置为0
                        else
                        {
                            QCS_CheckTestSetting Check = QCS_CheckTestSettingService.getByAql(DetailsModel.InspectionLevelID, DetailsModel.InspectionMethodID, Sta.AQL);//获取抽检检验设定实体
                            if (Check == null)
                            {
                                //fill++;
                                //fillmsg = "部分新增失败，QCS000001没有与检验方式，检验水平，AQL对应的数据，AC,RE，抽样数量将默认为0";
                                DetailsModel.SampleQuantity = 0;
                                DetailsModel.AcQuantity = 0;
                                DetailsModel.ReQuantity = 0;
                            }
                            else
                            {
                                //当QCS000001对应的明细数据没有时，AC,RE,抽样数量设置为0
                                QCS_CheckTestSettingDetails CheckDetail = QCS_CheckTestSettingDetailsService.GetDetails(Check.CheckTestSettingID, TdModel.DispatchQuantity);//获取抽检检验设定明细实体
                                if (CheckDetail == null)
                                {
                                    DetailsModel.SampleQuantity = 0;
                                    DetailsModel.AcQuantity = 0;
                                    DetailsModel.ReQuantity = 0;
                                }                               
                                else
                                {
                                    DetailsModel.SampleQuantity = CheckDetail.SamplingQuantity;
                                    DetailsModel.AcQuantity = CheckDetail.AcQuantity;
                                    DetailsModel.ReQuantity = CheckDetail.ReQuantity;
                                }
                            }
                        }
                        DetailsModel.NGquantity = 0;//不良数？

                        if (model2.Value == Framework.SystemID + "02012130000B3")//查看系统设置的判定方式
                        {
                            if (DetailsModel.NGquantity >= DetailsModel.ReQuantity)
                                DetailsModel.QualityControlDecision = Framework.SystemID + "0201213000090";
                            else
                                DetailsModel.QualityControlDecision = Framework.SystemID + "0201213000091";
                        }
                        else
                        {
                            DetailsModel.QualityControlDecision = Framework.SystemID + "0201213000091";//人工判定时默认为允收
                        }
                        if (model3.Value == Framework.SystemID + "02012130000B1" && DetailsModel.QualityControlDecision == Framework.SystemID + "0201213000090")
                        {//品质判定为自动且新增的明细判定为拒收
                            model.QualityControlDecision = Framework.SystemID + "0201213000090";//假如系统判定方式为自动，明细有一个为拒收，那么表头也为拒收

                            QCS_InspectionDocumentService.update(userid, model);//更新表头数据
                        }

                        QCS_InspectionDocumentDetailsService.insert(userid, DetailsModel);
                    }
                    return new { status = "200", msg = "新增成功！" };
                }
                else
                    return new { status = "410", msg = "新增失败！" };
            }
            catch (Exception ex)
            {
                DataLogerService.writeerrlog(ex);
                return new { status = "410", msg = "表头新增成功！但明细为空！" };
            }
        }

        /// <summary>
        /// 保存
        /// Joint
        /// </summary>
        /// <param name="Token"></param>
        /// <param name="jArray"></param>
        /// <returns></returns>
        public static object Qcs00007update(string Token, JArray jArray)
        {
            int success = 0;
            int fail = 0;
            string failIDs = null;
            string msg = null;
            string userid = UtilBussinessService.detoken(Token);
            JObject data = null;
            QCS_InspectionDocument model = null;
            string SysId = Framework.SystemID;
            for (int i = 0; i < jArray.Count; i++)
            {

                data = (JObject)jArray[i];
                model = QCS_InspectionDocumentService.get(data.Value<string>("InspectionDocumentID"));
                if (model.Status == SysId + "020121300008E" || model.Status == SysId + "020121300008F")//状态为CA确认,CL作废时不可新增修改
                {

                    failIDs = UtilBussinessService.str(failIDs, data.Value<string>("InspectionDocumentID"));
                    msg = UtilBussinessService.str(msg, model.InspectionNo + "状态为CA确认或CL作废,不能修改");
                    fail++;
                    continue;
                }
                model.InspectionDocumentID = data.Value<string>("InspectionDocumentID");
                if (!string.IsNullOrWhiteSpace(data.Value<string>("InspectionDate")))
                    model.DocumentDate = data.Value<DateTime>("DocumentDate");
                model.InspectionMethod = data.Value<string>("InspectionMethod");
                model.CompletionOrderID = data.Value<string>("FinishID");
                model.ItemID = data.Value<string>("ItemID");
                model.TaskDispatchID = data.Value<string>("TaskDispatchID");
                if (!string.IsNullOrWhiteSpace(data.Value<string>("InspectionDate")))
                    model.InspectionDate = data.Value<DateTime>("InspectionDate");
                model.InspectionUserID = data.Value<string>("InspectionUserID");
                model.QualityControlDecision = data.Value<string>("QcDecision");
                //防止前端传空值报错，为空时默认为0
                if (string.IsNullOrWhiteSpace(data.Value<string>("FinQuantity")))
                {
                    model.FinQuantity = 0;
                }
                else
                {
                    model.FinQuantity = data.Value<decimal>("FinQuantity");
                }
                //防止前端传空值报错，为空时默认为0
                if (string.IsNullOrWhiteSpace(data.Value<string>("InspectionQuantity")))
                {
                    model.InspectionQuantity = 0;
                }
                else
                {
                    model.InspectionQuantity = data.Value<decimal>("InspectionQuantity");
                }
                //防止前端传空值报错，为空时默认为0
                if (string.IsNullOrWhiteSpace(data.Value<string>("ScrappedQuantity")))
                {
                    model.ScrappedQuantity = 0;
                }
                else
                {
                    model.ScrappedQuantity = data.Value<decimal>("ScrappedQuantity");
                }
                //防止前端传空值报错，为空时默认为0
                if (string.IsNullOrWhiteSpace(data.Value<string>("NGquantity")))
                {
                    model.NGquantity = 0;
                }
                else
                {
                    model.NGquantity = data.Value<decimal>("NGquantity");
                }
                //防止前端传空值报错，为空时默认为0
                if (string.IsNullOrWhiteSpace(data.Value<string>("OKQuantity")))
                {
                    model.OKQuantity = 0;
                }
                else
                {
                    model.OKQuantity = data.Value<decimal>("OKQuantity");
                }
                model.InspectionFlag = data.Value<bool>("InspectionFlag");
                model.Status = data.Value<string>("Status");
                model.Comments = data.Value<string>("Comments");
                if (QCS_InspectionDocumentService.update(userid, model))
                    success++;
                else
                {
                    failIDs = UtilBussinessService.str(failIDs, data.Value<string>("InspectionDocumentID"));
                    fail++;
                }

            }
            return new { success = success, fail = fail, failIDs = failIDs, msg = msg };
        }

        /// <summary>
        /// 获取检验明细列表
        /// Joint
        /// </summary>
        /// <param name="Token"></param>
        /// <param name="InspectionNo"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <returns></returns>
        public static object Qcs00007GetDetailsList(string Token, string InspectionNo, int page, int rows)
        {
            int count = 0;
            return UtilBussinessService.getPaginationModel(QCS_InspectionDocumentDetailsService.Qcs00005GetDetailsList(InspectionNo, page, rows, ref count), count);
        }

        /// <summary>
        /// 检验单明细更新
        /// Joint
        /// </summary>
        /// <param name="Token"></param>
        /// <param name="jArray"></param>
        /// <returns></returns>
        public static object Qcs00007DetailUpdate(string Token, JArray jArray)
        {
            int success = 0;
            int fail = 0;
            string failIDs = null;
            string msg = null;
            string userid = UtilBussinessService.detoken(Token);
            JObject data = null;
            QCS_InspectionDocumentDetails model = null;
            QCS_InspectionDocument Headermodel = null;
            string SysId = Framework.SystemID;
            for (int i = 0; i < jArray.Count; i++)
            {
                data = (JObject)jArray[i];
                Headermodel = QCS_InspectionDocumentService.get(data.Value<string>("InspectionDocumentID"));
                if (Headermodel == null)
                {
                    failIDs = UtilBussinessService.str(failIDs, data.Value<string>("InspectionDocumentID"));
                    msg = UtilBussinessService.str(msg, data.Value<string>("Sequence") + "的表头流水号为空");
                    fail++;
                    continue;
                }
                if (Headermodel.ItemID == null)
                {
                    failIDs = UtilBussinessService.str(failIDs, data.Value<string>("InspectionDocumentID"));
                    msg = UtilBussinessService.str(msg, data.Value<string>("Sequence") + data.Value<string>("InspectionDocumentID") + "的料品流水号为空");
                    fail++;
                    continue;
                }
                if (Headermodel.Status == SysId + "020121300008E" || Headermodel.Status == SysId + "020121300008F")//状态为CA确认,CL作废时不可新增修改
                {

                    failIDs = UtilBussinessService.str(failIDs, data.Value<string>("InspectionNo"));
                    msg = UtilBussinessService.str(msg, Headermodel.InspectionNo + "状态为CA确认或CL作废,不能修改");
                    fail++;
                    continue;
                }
                model = new QCS_InspectionDocumentDetails();
                model.InspectionDocumentDetailID = data.Value<string>("InspectionDocumentDetailID");
                model.InspectionDocumentID = data.Value<string>("InspectionDocumentID");
                model.Sequence = string.IsNullOrWhiteSpace(data.Value<string>("Sequence")) ? 0 : data.Value<int>("Sequence");
                model.InspectionStandard = data.Value<string>("InspectionStandard");
                model.InspectionMethod = data.Value<string>("InspectionMethod");
                model.InspectionClassID = data.Value<string>("InspectionClassID");
                model.InspectionMethodID = data.Value<string>("InspectionMethodID");
                model.InspectionItemID = data.Value<string>("InspectionItemID");
                model.InspectionLevelID = data.Value<string>("InspectionLevelID");
                model.InspectionFaultID = data.Value<string>("InspectionFaultID");
                model.SampleQuantity = data.Value<decimal>("SampleQuantity");
                model.Aql = data.Value<string>("Aql");
                model.Status = data.Value<string>("Status");
                model.AcQuantity = data.Value<decimal>("AcQuantity");
                model.ReQuantity = data.Value<decimal>("ReQuantity");
                model.NGquantity = data.Value<decimal>("NGquantity");
                model.Attribute = data.Value<string>("Attribute");
                model.QualityControlDecision = data.Value<string>("QcDecision");
                MES_Parameter model2 = MES_ParameterService.get(Framework.SystemID + "1101213000003");//获取对应检验项目判定方式实体
                if (model2.Value == Framework.SystemID + "02012130000B3")//如果明细判定方式为自动
                {
                    if (model.NGquantity >= model.ReQuantity)//当明细修改时，不良数大于等于re数，将明细判定改为拒收
                    {
                        model.QualityControlDecision = Framework.SystemID + "0201213000090";
                    }
                    else
                    {
                        model.QualityControlDecision = Framework.SystemID + "0201213000091";
                    }
                }
                QCS_InspectionDocument QCS_ID = new QCS_InspectionDocument();
                QCS_ID = QCS_InspectionDocumentService.get(model.InspectionDocumentID);
                MES_Parameter model3 = new MES_Parameter();//品质判定方式
                model3.ParameterID = Framework.SystemID + "1101213000002";
                model3 = MES_ParameterService.get(model3.ParameterID);
                if (model3.Value == Framework.SystemID + "02012130000B1")//如果表头品质判定方式为自动
                {
                    if (model.QualityControlDecision == Framework.SystemID + "0201213000090")//当明细更改为拒收，那么获取表头实体并修改表头验收字段
                    {
                        QCS_ID.QualityControlDecision = Framework.SystemID + "0201213000090";//明细为拒收，表头也更新为拒收
                        QCS_InspectionDocumentService.update(userid, QCS_ID);
                    }
                }
                model.Comments = data.Value<string>("Comments");
                if (QCS_InspectionDocumentDetailsService.update(userid, model))
                {
                    success++;
                    if (model3.Value == Framework.SystemID + "02012130000B1")//如果表头品质判定方式为自动
                    {
                        bool result = true;//默认明细所有结果都为允收
                        IList<Hashtable> DetailArray = QCS_InspectionDocumentDetailsService.GetAllDetail(model.InspectionDocumentID);//获取该表头下的所有明细
                        for (int j = 0; j < DetailArray.Count; j++)
                        {
                            QCS_InspectionDocumentDetails IDmodel = QCS_InspectionDocumentDetailsService.get(DetailArray[j]["InspectionDocumentDetailID"].ToString());
                            if (IDmodel.QualityControlDecision == Framework.SystemID + "0201213000090")
                            {
                                result = false;//当有一个明细结果为拒收时，将result改为false
                            }
                        }
                        if (result == true)//当所有的明细结果都为允收时，将表头该为允收
                        {
                            QCS_InspectionDocument IDmodel = QCS_InspectionDocumentService.get(model.InspectionDocumentID);
                            IDmodel.QualityControlDecision = Framework.SystemID + "0201213000091";
                            QCS_InspectionDocumentService.update(userid, IDmodel);
                        }
                    }
                }
                else
                {
                    failIDs = UtilBussinessService.str(failIDs, data.Value<string>("InspectionDocumentDetailID"));
                    fail++;
                }

            }
            return new { success = success, fail = fail, failIDs = failIDs, msg = msg };
        }

        /// <summary>
        /// 检验单明细更新--移动端
        /// Alvin 2017年9月12日14:48:26
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public static object Qcs00007DetailUpdateByOne(JObject request)
        {
            string Token = request.Value<string>("Token");//获取传进来的token
            string userid = UtilBussinessService.detoken(Token);//获取登录人的流水号
            string SysId = Framework.SystemID;//系统代号
            QCS_InspectionDocumentDetails model = null;
            QCS_InspectionDocument Headermodel = null;
            try
            {
                Headermodel = QCS_InspectionDocumentService.get(request.Value<string>("InspectionDocumentID"));
                if (string.IsNullOrWhiteSpace(request.Value<string>("InspectionDocumentID")))
                {
                    return new { status = "410", msg = "检验单据流水号不能为空！" };
                }
                if (Headermodel.ItemID == null)
                {
                    return new { status = "410", msg = "该检验单据的料品流水号为空！" };
                }

                if (Headermodel.Status == SysId + "020121300008E" || Headermodel.Status == SysId + "020121300008F")//状态为CA确认,CL作废时不可修改
                {
                    return new { status = "410", msg = "状态为CA确认或CL作废,不能修改！" };
                }
                model = new QCS_InspectionDocumentDetails();
                if (string.IsNullOrWhiteSpace(request.Value<string>("InspectionDocumentDetailID")))
                {
                    return new { status = "410", msg = "检验单据明细流水号不能为空！" };
                }
                model.InspectionDocumentDetailID = request.Value<string>("InspectionDocumentDetailID");
                model.InspectionDocumentID = request.Value<string>("InspectionDocumentID");
                model.NGquantity = request.Value<decimal>("NGquantity");
                model.Attribute = request.Value<string>("Attribute");
                model.QualityControlDecision = request.Value<string>("QcDecision");
                model.Comments = request.Value<string>("Comments");
                MES_Parameter model3 = new MES_Parameter();//品质判定方式
                model3.ParameterID = Framework.SystemID + "1101213000002";
                model3 = MES_ParameterService.get(model3.ParameterID);
                if (model3.Value == Framework.SystemID + "02012130000B1")//如果表头品质判定方式为自动
                {
                    if (model.QualityControlDecision == Framework.SystemID + "0201213000090")//当明细更改为拒收，那么获取表头实体并修改表头验收字段
                    {
                        QCS_InspectionDocument QCS_ID = new QCS_InspectionDocument();
                        QCS_ID = QCS_InspectionDocumentService.get(model.InspectionDocumentID);
                        QCS_ID.QualityControlDecision = Framework.SystemID + "0201213000090";//明细为拒收，表头也更新为拒收
                        QCS_InspectionDocumentService.update(userid, QCS_ID);
                    }
                }

                if (QCS_InspectionDocumentDetailsService.Qcs00005DetailUpdate(userid, model))
                {
                    return new { status = "200", msg = "修改明细成功！" };
                }
                else
                {
                    return new { status = "410", msg = "修改明细失败！" };
                }
            }
            catch (Exception ex)
            {
                DataLogerService.writeerrlog(ex);
                return new { status = "410", msg = "修改明细失败！" + ex.ToString() };
            }
        }


        /// <summary>
        /// 制程检验单明细原因码列表
        /// Joint
        /// </summary>
        /// <param name="Token"></param>
        /// <param name="InspectionDocumentID"></param>
        /// <param name="InspectionDocumentDetailID"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <returns></returns>
        public static object Qcs00007GetReasonList(string Token, string InspectionDocumentID, string InspectionDocumentDetailID, int page, int rows)
        {
            int count = 0;
            return UtilBussinessService.getPaginationModel(QCS_InspectionDocumentReasonService.Qcs00005GetReasonList(InspectionDocumentID, InspectionDocumentDetailID, page, rows, ref count), count);
        }

        /// <summary>
        /// 制程检验原因码新增
        /// Joint
        /// </summary>
        /// <param name="Token"></param>
        /// <param name="jArray"></param>
        /// <returns></returns>
        public static object Qcs00007ReasonInsert(string Token, JArray jArray)
        {
            int success = 0;
            int fail = 0;
            string failIDs = null;
            string msg = null;
            string userid = UtilBussinessService.detoken(Token);
            JObject data = null;
            QCS_InspectionDocumentReason model = new QCS_InspectionDocumentReason();
            QCS_InspectionDocumentDetails Headermodel = null;
            for (int i = 0; i < jArray.Count; i++)
            {
                data = (JObject)jArray[i];
                Headermodel = QCS_InspectionDocumentDetailsService.get(data.Value<string>("InspectionDocumentDetailID"));
                if (Headermodel == null)
                {
                    failIDs = UtilBussinessService.str(failIDs, data.Value<string>("InspectionDocumentDetailID"));
                    msg = UtilBussinessService.str(msg, data.Value<string>("Sequence") + "的明细流水号为空");
                    fail++;
                }
                model.InspectionDocumentReasonID = UniversalService.GetSerialNumber("QCS_InspectionDocumentReason");
                model.InspectionDocumentID = data.Value<string>("InspectionDocumentID");
                model.InspectionDocumentDetailID = data.Value<string>("InspectionDocumentDetailID");
                if (!string.IsNullOrWhiteSpace(data.Value<string>("Sequence")))
                    model.Sequence = data.Value<int>("Sequence");
                if (string.IsNullOrWhiteSpace(data.Value<string>("ReasonID")))
                {
                    return new { Status = "400", msg = "保存失败，原因为空！" };
                }
                model.ReasonID = data.Value<string>("ReasonID");
                model.Comments = data.Value<string>("Comments");
                model.Status = Framework.SystemID + "0201213000001";
                if (QCS_InspectionDocumentReasonService.insert(userid, model))
                    success++;
                else
                {
                    failIDs = UtilBussinessService.str(failIDs, data.Value<string>("InspectionDocumentReasonID"));
                    fail++;
                }
            }
            return new { success = success, fail = fail, failIDs = failIDs, msg = msg };
        }

        /// <summary>
        /// 制程检验原因码更新
        /// Joint
        /// </summary>
        /// <param name="Token"></param>
        /// <param name="jArray"></param>
        /// <returns></returns>
        public static object Qcs00007ReasonUpdate(string Token, JArray jArray)
        {
            int success = 0;
            int fail = 0;
            string failIDs = null;
            string msg = null;
            string userid = UtilBussinessService.detoken(Token);
            JObject data = null;
            QCS_InspectionDocumentReason model = null;
            QCS_InspectionDocumentDetails Headermodel = null;
            for (int i = 0; i < jArray.Count; i++)
            {
                data = (JObject)jArray[i];
                Headermodel = QCS_InspectionDocumentDetailsService.get(data.Value<string>("InspectionDocumentDetailID"));
                if (Headermodel == null)
                {
                    failIDs = UtilBussinessService.str(failIDs, data.Value<string>("InspectionDocumentDetailID"));
                    msg = UtilBussinessService.str(msg, data.Value<string>("Sequence") + "的明细流水号为空");
                    fail++;
                }
                model = QCS_InspectionDocumentReasonService.get(data.Value<string>("InspectionDocumentReasonID"));
                if (Headermodel == null)
                {
                    failIDs = UtilBussinessService.str(failIDs, data.Value<string>("InspectionDocumentReasonID"));
                    msg = UtilBussinessService.str(msg, data.Value<string>("Sequence") + "的原因码流水号为空");
                    fail++;
                }
                if (!string.IsNullOrWhiteSpace(data.Value<string>("Sequence")))
                    model.Sequence = data.Value<int>("Sequence");
                model.ReasonID = data.Value<string>("ReasonID");
                model.Comments = data.Value<string>("Comments");
                if (QCS_InspectionDocumentReasonService.update(userid, model))
                    success++;
                else
                {
                    failIDs = UtilBussinessService.str(failIDs, data.Value<string>("InspectionDocumentReasonID"));
                    fail++;
                }
            }
            return new { success = success, fail = fail, failIDs = failIDs, msg = msg };
        }

        /// <summary>
        /// 制程检验原因码删除
        /// Joint
        /// </summary>
        /// <param name="Token"></param>
        /// <param name="jArray"></param>
        /// <returns></returns>
        public static object Qcs00007ReasonDelete(string Token, JArray jArray)
        {
            int success = 0;
            int fail = 0;
            string failIDs = null;
            string msg = null;
            string userid = UtilBussinessService.detoken(Token);
            JObject data = null;
            QCS_InspectionDocumentReason model = null;
            QCS_InspectionDocumentDetails Headermodel = null;
            for (int i = 0; i < jArray.Count; i++)
            {
                data = (JObject)jArray[i];
                Headermodel = QCS_InspectionDocumentDetailsService.get(data.Value<string>("InspectionDocumentDetailID"));
                if (Headermodel == null)
                {
                    failIDs = UtilBussinessService.str(failIDs, data.Value<string>("InspectionDocumentDetailID"));
                    msg = UtilBussinessService.str(msg, data.Value<string>("Sequence") + "的明细流水号为空");
                    fail++;
                }
                model = QCS_InspectionDocumentReasonService.get(data.Value<string>("InspectionDocumentReasonID"));
                if (model == null)
                {
                    failIDs = UtilBussinessService.str(failIDs, data.Value<string>("InspectionDocumentReasonID"));
                    msg = UtilBussinessService.str(msg, data.Value<string>("Sequence") + "的原因码流水号为空");
                    fail++;
                }
                model.Status = Framework.SystemID + "0201213000003";
                if (QCS_InspectionDocumentReasonService.update(userid, model))
                    success++;
                else
                {
                    failIDs = UtilBussinessService.str(failIDs, data.Value<string>("InspectionDocumentReasonID"));
                    fail++;
                }
            }
            return new { success = success, fail = fail, failIDs = failIDs, msg = msg };
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
        /// <returns></returns>
        public static object Qcs00007GetRemarkList(string Token, string InspectionDocumentID, string InspectionDocumentDetailID, int page, int rows)
        {
            int count = 0;
            return UtilBussinessService.getPaginationModel(QCS_InspectionDocumentRemarkService.Qcs00005GetRemarkList(InspectionDocumentID, InspectionDocumentDetailID, page, rows, ref count), count);
        }

        /// <summary>
        /// 检验结果说明新增
        /// Joint
        /// </summary>
        /// <param name="Token"></param>
        /// <param name="jArray"></param>
        /// <returns></returns>
        public static object Qcs00007RemarkInsert(string Token, JArray jArray)
        {
            int success = 0;
            int fail = 0;
            string failIDs = null;
            string msg = null;
            string userid = UtilBussinessService.detoken(Token);
            JObject data = null;
            QCS_InspectionDocumentRemark model = new QCS_InspectionDocumentRemark();
            QCS_InspectionDocumentDetails Headermodel = null;
            for (int i = 0; i < jArray.Count; i++)
            {
                data = (JObject)jArray[i];
                Headermodel = QCS_InspectionDocumentDetailsService.get(data.Value<string>("InspectionDocumentDetailID"));
                if (Headermodel == null)
                {
                    failIDs = UtilBussinessService.str(failIDs, data.Value<string>("InspectionDocumentDetailID"));
                    msg = UtilBussinessService.str(msg, data.Value<string>("Sequence") + "的明细流水号为空");
                    fail++;
                }
                model.InspectionDocumentRemarkID = UniversalService.GetSerialNumber("QCS_InspectionDocumentRemark");
                model.InspectionDocumentDetailID = data.Value<string>("InspectionDocumentDetailID");
                model.InspectionDocumentID = data.Value<string>("InspectionDocumentID");
                if (!string.IsNullOrWhiteSpace(data.Value<string>("Sequence")))
                    model.Sequence = data.Value<int>("Sequence");
                model.Remark = data.Value<string>("Remark");
                model.Status = Framework.SystemID + "0201213000001";
                if (QCS_InspectionDocumentRemarkService.insert(userid, model))
                    success++;
                else
                {
                    failIDs = UtilBussinessService.str(failIDs, data.Value<string>("InspectionDocumentRemarkID"));
                    fail++;
                }
            }
            return new { success = success, fail = fail, failIDs = failIDs, msg = msg };
        }

        /// <summary>
        /// 检验结果说明更新
        /// Joint
        /// </summary>
        /// <param name="Token"></param>
        /// <param name="jArray"></param>
        /// <returns></returns>
        public static object Qcs00007RemarkUpdate(string Token, JArray jArray)
        {
            int success = 0;
            int fail = 0;
            string failIDs = null;
            string msg = null;
            string userid = UtilBussinessService.detoken(Token);
            JObject data = null;
            QCS_InspectionDocumentRemark model = null;
            QCS_InspectionDocumentDetails Headermodel = null;
            for (int i = 0; i < jArray.Count; i++)
            {
                data = (JObject)jArray[i];
                Headermodel = QCS_InspectionDocumentDetailsService.get(data.Value<string>("InspectionDocumentDetailID"));
                if (Headermodel == null)
                {
                    failIDs = UtilBussinessService.str(failIDs, data.Value<string>("InspectionDocumentDetailID"));
                    msg = UtilBussinessService.str(msg, data.Value<string>("Sequence") + "的明细流水号为空");
                    fail++;
                }
                model = QCS_InspectionDocumentRemarkService.get(data.Value<String>("InspectionDocumentRemarkID"));
                if (model == null)
                {
                    failIDs = UtilBussinessService.str(failIDs, data.Value<string>("InspectionDocumentRemarkID"));
                    msg = UtilBussinessService.str(msg, data.Value<string>("Sequence") + "的结果说明流水号为空");
                    fail++;
                }
                if (!string.IsNullOrWhiteSpace(data.Value<string>("Sequence")))
                    model.Sequence = data.Value<int>("Sequence");
                model.Remark = data.Value<string>("Remark");
                if (QCS_InspectionDocumentRemarkService.update(userid, model))
                    success++;
                else
                {
                    failIDs = UtilBussinessService.str(failIDs, data.Value<string>("InspectionDocumentRemarkID"));
                    fail++;
                }
            }
            return new { success = success, fail = fail, failIDs = failIDs, msg = msg };
        }

        /// <summary>
        /// 检验结果说明删除
        /// Joint
        /// </summary>
        /// <param name="Token"></param>
        /// <param name="jArray"></param>
        /// <returns></returns>
        public static object Qcs00007RemarkDelete(string Token, JArray jArray)
        {
            int success = 0;
            int fail = 0;
            string failIDs = null;
            string msg = null;
            string userid = UtilBussinessService.detoken(Token);
            JObject data = null;
            QCS_InspectionDocumentRemark model = null;
            QCS_InspectionDocumentDetails Headermodel = null;
            for (int i = 0; i < jArray.Count; i++)
            {
                data = (JObject)jArray[i];
                Headermodel = QCS_InspectionDocumentDetailsService.get(data.Value<string>("InspectionDocumentDetailID"));
                if (Headermodel == null)
                {
                    failIDs = UtilBussinessService.str(failIDs, data.Value<string>("InspectionDocumentDetailID"));
                    msg = UtilBussinessService.str(msg, data.Value<string>("Sequence") + "的表头流水号为空");
                    fail++;
                }
                model = QCS_InspectionDocumentRemarkService.get(data.Value<String>("InspectionDocumentRemarkID"));
                if (model == null)
                {
                    failIDs = UtilBussinessService.str(failIDs, data.Value<string>("InspectionDocumentRemarkID"));
                    msg = UtilBussinessService.str(msg, data.Value<string>("Sequence") + "的结果说明流水号为空");
                    fail++;
                }
                model.Status = Framework.SystemID + "0201213000003";
                if (QCS_InspectionDocumentRemarkService.update(userid, model))
                    success++;
                else
                {
                    failIDs = UtilBussinessService.str(failIDs, data.Value<string>("InspectionDocumentRemarkID"));
                    fail++;
                }
            }
            return new { success = success, fail = fail, failIDs = failIDs, msg = msg };
        }


        /// <summary>
        /// QCS00007开窗更新--安卓
        /// Alvin  2017年9月11日14:17:49
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public static object Qcs00007UpdateByOne(JObject request)
        {
            string Token = request.Value<string>("Token");//获取传进来的token
            string userid = UtilBussinessService.detoken(Token);//获取登录人的流水号
            string SysId = Framework.SystemID;//系统代号
            QCS_InspectionDocument model = null;//初始化model
            try
            {
                if (string.IsNullOrWhiteSpace(request.Value<string>("InspectionDocumentID")))
                {
                    return new { status = "410", msg = "检验单据流水号不能为空！" };
                }

                model = QCS_InspectionDocumentService.get(request.Value<string>("InspectionDocumentID"));
                if (model.Status == SysId + "020121300008E" || model.Status == SysId + "020121300008F")//状态为CA确认,CL作废时不可修改
                {
                    return new { status = "410", msg = "状态为CA确认或CL作废,不能修改！" };
                }

                model.InspectionDocumentID = request.Value<string>("InspectionDocumentID");
                if (!string.IsNullOrWhiteSpace(request.Value<string>("DocumentDate")))
                    model.DocumentDate = request.Value<DateTime>("DocumentDate");
                model.TaskDispatchID = request.Value<string>("TaskDispatchID");
                if (!string.IsNullOrWhiteSpace(request.Value<string>("InspectionDate")))
                    model.InspectionDate = request.Value<DateTime>("InspectionDate");
                model.InspectionUserID = request.Value<string>("InspectionUserID");
                model.QualityControlDecision = request.Value<string>("QcDecision");
                model.InspectionQuantity = request.Value<decimal>("InspectionQuantity");
                model.ScrappedQuantity = request.Value<decimal>("ScrappedQuantity");
                model.NGquantity = request.Value<decimal>("NGquantity");
                model.InspectionFlag = request.Value<bool>("InspectionFlag");
                model.Comments = request.Value<string>("Comments");

                if (QCS_InspectionDocumentService.Qcs00007UpdateByOne(userid, model))
                {
                    return new { status = "200", msg = "修改成功！" };
                }
                else
                {
                    return new { status = "410", msg = "修改失败！" };
                }
            }
            catch (Exception ex)
            {
                DataLogerService.writeerrlog(ex);
                return new { status = "410", msg = "修改失败！" + ex.ToString() };
            }
        }

        /// <summary>
        /// QCS00007原因码新增
        /// Alvin 2017年9月12日15:04:28
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public static object Qcs00007ReasonAddByOne(JObject request)
        {
            string userid = UtilBussinessService.detoken(request.Value<string>("Token"));
            QCS_InspectionDocumentReason model = new QCS_InspectionDocumentReason();
            QCS_InspectionDocumentDetails Headermodel = null;
            try
            {
                Headermodel = QCS_InspectionDocumentDetailsService.get(request.Value<string>("InspectionDocumentDetailID"));
                if (Headermodel == null)
                {
                    return new { status = "410", msg = "检验首件检验的明细流水号不能为空！" };
                }
                model.InspectionDocumentReasonID = UniversalService.GetSerialNumber("QCS_InspectionDocumentReason");
                model.InspectionDocumentID = request.Value<string>("InspectionDocumentID");
                model.InspectionDocumentDetailID = request.Value<string>("InspectionDocumentDetailID");
                if (!string.IsNullOrWhiteSpace(request.Value<string>("Sequence")))
                    model.Sequence = request.Value<int>("Sequence");
                if (string.IsNullOrWhiteSpace(request.Value<string>("ReasonID")))
                {
                    return new { Status = "400", msg = "保存失败，原因为空！" };
                }
                model.ReasonID = request.Value<string>("ReasonID");
                model.Comments = request.Value<string>("Comments");
                model.Status = Framework.SystemID + "0201213000001";
                if (QCS_InspectionDocumentReasonService.insert(userid, model))
                {
                    return new { status = "200", msg = "原因码新增成功！" };
                }
                else
                {
                    return new { status = "410", msg = "原因码新增失败！" };
                }
            }
            catch (Exception ex)
            {
                DataLogerService.writeerrlog(ex);
                return new { status = "410", msg = "原因码新增失败！" + ex.ToString() };

            }
        }

        /// <summary>
        /// QCS00007原因码更新
        /// Alvin 2017年9月12日15:04:28
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public static object Qcs00007ReasonEditByOne(JObject request)
        {
            string userid = UtilBussinessService.detoken(request.Value<string>("Token"));
            QCS_InspectionDocumentReason model = null;
            QCS_InspectionDocumentDetails Headermodel = null;
            try
            {
                Headermodel = QCS_InspectionDocumentDetailsService.get(request.Value<string>("InspectionDocumentDetailID"));
                if (Headermodel == null)
                {
                    return new { status = "410", msg = "检验首件检验的明细流水号不能为空！" };
                }
                model = QCS_InspectionDocumentReasonService.get(request.Value<string>("InspectionDocumentReasonID"));
                if (model == null)
                {
                    return new { status = "410", msg = "原因码流水号不能为空！" };
                }
                if (!string.IsNullOrWhiteSpace(request.Value<string>("Sequence")))
                    model.Sequence = request.Value<int>("Sequence");
                model.ReasonID = request.Value<string>("ReasonID");//参数表获取
                model.Comments = request.Value<string>("Comments");
                if (QCS_InspectionDocumentReasonService.update(userid, model))
                {
                    return new { status = "200", msg = "原因码修改成功！" };
                }
                else
                {
                    return new { status = "410", msg = "原因码修改失败！" };
                }
            }
            catch (Exception ex)
            {
                DataLogerService.writeerrlog(ex);
                return new { status = "410", msg = "原因码修改失败！" + ex.ToString() };
            }
        }

        /// <summary>
        /// QCS00007原因码删除
        /// Alvin 2017年9月12日15:04:28
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public static object Qcs00007ReasonDeleteByOne(JObject request)
        {
            string userid = UtilBussinessService.detoken(request.Value<string>("Token"));
            QCS_InspectionDocumentReason model = null;
            QCS_InspectionDocumentDetails Headermodel = null;
            try
            {
                Headermodel = QCS_InspectionDocumentDetailsService.get(request.Value<string>("InspectionDocumentDetailID"));
                if (Headermodel == null)
                {
                    return new { status = "410", msg = "检验首件的明细流水号不能为空！" };
                }
                model = QCS_InspectionDocumentReasonService.get(request.Value<string>("InspectionDocumentReasonID"));
                if (model == null)
                {
                    return new { status = "410", msg = "原因码流水号不能为空！" };
                }
                model.Status = Framework.SystemID + "0201213000003";
                if (QCS_InspectionDocumentReasonService.update(userid, model))
                {
                    return new { status = "200", msg = "原因码删除成功！" };
                }
                else
                {
                    return new { status = "410", msg = "原因码删除失败！" };
                }
            }
            catch (Exception ex)
            {
                DataLogerService.writeerrlog(ex);
                return new { status = "410", msg = "原因码删除失败！" + ex.ToString() };
            }
        }

        /// <summary>
        /// QCS00007 制程首件检验结果说明新增（移动端）
        /// Alvin  2017年9月12日15:21:13
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public static object Qcs00007RemarkAddByOne(JObject request)
        {
            string userid = UtilBussinessService.detoken(request.Value<string>("Token"));
            QCS_InspectionDocumentRemark model = new QCS_InspectionDocumentRemark();
            QCS_InspectionDocumentDetails Headermodel = null;
            try
            {
                Headermodel = QCS_InspectionDocumentDetailsService.get(request.Value<string>("InspectionDocumentDetailID"));
                if (Headermodel == null)
                {
                    return new { status = "410", msg = "制程首件检验明细流水号不能为空！" };
                }
                model.InspectionDocumentRemarkID = UniversalService.GetSerialNumber("QCS_InspectionDocumentRemark");
                model.InspectionDocumentDetailID = request.Value<string>("InspectionDocumentDetailID");
                model.InspectionDocumentID = request.Value<string>("InspectionDocumentID");
                if (!string.IsNullOrWhiteSpace(request.Value<string>("Sequence")))
                    model.Sequence = request.Value<int>("Sequence");
                model.Remark = request.Value<string>("Remark");
                model.Status = Framework.SystemID + "0201213000001";
                if (QCS_InspectionDocumentRemarkService.insert(userid, model))
                {
                    return new { status = "200", msg = "结果说明新增成功！" };
                }
                else
                {
                    return new { status = "410", msg = "结果说明新增失败！" };
                }

            }
            catch (Exception ex)
            {
                DataLogerService.writeerrlog(ex);
                return new { status = "410", msg = "结果说明新增失败！" + ex.ToString() };
            }
        }

        /// <summary>
        /// QCS00007 制程首件检验结果说明更新（移动端）
        /// Alvin  2017年9月12日15:21:13
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public static object Qcs00007RemarkEditByOne(JObject request)
        {
            string userid = UtilBussinessService.detoken(request.Value<string>("Token"));
            QCS_InspectionDocumentRemark model = null;
            QCS_InspectionDocumentDetails Headermodel = null;
            try
            {
                Headermodel = QCS_InspectionDocumentDetailsService.get(request.Value<string>("InspectionDocumentDetailID"));
                if (Headermodel == null)
                {
                    return new { status = "410", msg = "制程首件检验明细流水号不能为空！" };
                }
                model = QCS_InspectionDocumentRemarkService.get(request.Value<String>("InspectionDocumentRemarkID"));
                if (model == null)
                {
                    return new { status = "410", msg = "制程首件检验结果说明流水号为空！" };
                }
                if (!string.IsNullOrWhiteSpace(request.Value<string>("Sequence")))
                    model.Sequence = request.Value<int>("Sequence");
                model.Remark = request.Value<string>("Remark");
                if (QCS_InspectionDocumentRemarkService.update(userid, model))
                {
                    return new { status = "200", msg = "结果说明修改成功！" };
                }
                else
                {
                    return new { status = "410", msg = "结果说明修改失败！" };
                }

            }
            catch (Exception ex)
            {
                DataLogerService.writeerrlog(ex);
                return new { status = "410", msg = "结果说明修改失败！" + ex.ToString() };
            }
        }

        /// <summary>
        /// QCS00007 制程首件检验结果说明删除（移动端）
        /// Alvin  2017年9月12日15:21:13
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public static object Qcs00007RemarkDeleteByOne(JObject request)
        {
            string userid = UtilBussinessService.detoken(request.Value<string>("Token"));
            QCS_InspectionDocumentRemark model = null;
            QCS_InspectionDocumentDetails Headermodel = null;
            try
            {
                Headermodel = QCS_InspectionDocumentDetailsService.get(request.Value<string>("InspectionDocumentDetailID"));
                if (Headermodel == null)
                {
                    return new { status = "410", msg = "制程首件检验明细流水号不能为空！" };
                }
                model = QCS_InspectionDocumentRemarkService.get(request.Value<String>("InspectionDocumentRemarkID"));
                if (model == null)
                {
                    return new { status = "410", msg = "制程首件检验结果说明流水号为空！" };
                }
                model.Status = Framework.SystemID + "0201213000003";
                if (QCS_InspectionDocumentRemarkService.update(userid, model))
                {
                    return new { status = "200", msg = "结果说明删除成功！" };
                }
                else
                {
                    return new { status = "410", msg = "结果说明删除失败！" };
                }

            }
            catch (Exception ex)
            {
                DataLogerService.writeerrlog(ex);
                return new { status = "410", msg = "结果说明删除失败！" + ex.ToString() };
            }
        }


        #endregion

        #region QCS00008制程巡检检验维护
        /// <summary>
        /// 获取制程检验单列表
        /// Joint
        /// </summary>
        /// <param name="Token"></param>
        /// <param name="InspectionNo"></param>
        /// <param name="Status"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <returns></returns>
        public static object Qcs00008GetList(string Token, string InspectionNo, string Status, int page, int rows)
        {
            int count = 0;
            return UtilBussinessService.getPaginationModel(QCS_InspectionDocumentService.Qcs00008GetList(InspectionNo, Status, page, rows, ref count), count);
        }

        /// <summary>
        /// 制程巡检检验新增
        /// Joint
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static object Qcs00008Add(JObject data)
        {
            string Token = data.Value<string>("Token");
            string userid = UtilBussinessService.detoken(Token);
            string SysID = Framework.SystemID;
            int success = 0, fill = 0;
            string fillmsg = null;
            string fillmsg2 = null;
            decimal sum = 0;
            try
            {

                if (string.IsNullOrWhiteSpace(data.Value<string>("InspectionNo")))
                    return new { status = "410", msg = "单据编号不能为空！" };
                /*
                //判定方式，先查找对应检验项目判定方式是什么
                MES_Parameter model2 = new MES_Parameter();//创建Inf00020实体
                model2.ParameterID = "100391101213000003";//赋值对应的检验项目判定方式流水号
                model2 = MES_ParameterService.get(model2.ParameterID);//获取对应实体
                MES_Parameter model3 = new MES_Parameter();//品质判定方式
                model3.ParameterID = "100391101213000002";
                model3 = MES_ParameterService.get(model3.ParameterID);
                */

                /*SAM 2017年9月9日23:16:28  调整*/
                MES_Parameter model2 = MES_ParameterService.get(SysID + "1101213000003");//获取对应检验项目判定方式实体
                MES_Parameter model3 = MES_ParameterService.get(SysID + "1101213000002");//获取对应品质判定方式实体

                if (model2 == null)
                    return new { status = "410", msg = "不存在检验项目判定方式的设定，请优先设定" };

                if (model3 == null)
                    return new { status = "410", msg = "不存在品质判定方式的设定，请优先设定" };

                QCS_InspectionDocument model = new QCS_InspectionDocument();
                model.InspectionDocumentID = UniversalService.GetSerialNumber("QCS_InspectionDocument");
                model.InspectionNo = data.Value<string>("InspectionNo");
                if (!string.IsNullOrWhiteSpace(data.Value<string>("DocumentDate")))
                    model.DocumentDate = data.Value<DateTime>("DocumentDate");
                model.InspectionMethod = data.Value<string>("InspectionMethod");
                model.CompletionOrderID = data.Value<string>("CompletionOrderID");
                model.ItemID = data.Value<string>("ItemID");
                model.TaskDispatchID = data.Value<string>("TaskDispatchID");
                if (!string.IsNullOrWhiteSpace(data.Value<string>("InspectionDate")))
                    model.InspectionDate = data.Value<DateTime>("InspectionDate");
                model.InspectionUserID = data.Value<string>("InspectionUserID");
                model.FinQuantity = data.Value<decimal>("FinQuantity");

                if (!string.IsNullOrWhiteSpace(data.Value<string>("InspectionQuantity")) || data.Value<decimal>("InspectionQuantity") < 0)
                {
                    model.InspectionQuantity = data.Value<decimal>("InspectionQuantity");//检验数量？
                }
                else
                {
                    return new { status = "410", msg = "新增失败！检验数量不可为空也不可小于0！" };
                }

                model.ScrappedQuantity = data.Value<decimal>("ScrappedQuantity");//报废数量
                model.NGquantity = data.Value<decimal>("NGquantity");//NG数量
                model.OKQuantity = data.Value<decimal>("OKQuantity");//OK数量
                sum = model.ScrappedQuantity + model.NGquantity + model.OKQuantity;//三个数量加起来是否等于检验数量
                if (sum != model.InspectionQuantity)
                {
                    return new { status = "410", msg = "新增失败！OK数量+NG数量+报废数量不等于检验数量！" };
                }

                model.InspectionFlag = data.Value<bool>("InspectionFlag");
                model.Status = data.Value<string>("Status");
                model.Comments = data.Value<string>("Comments");

                if (model3.Value != Framework.SystemID + "02012130000B1")//如果品质判定方式不为自动，那么由表头带入判定
                {
                    model.QualityControlDecision = data.Value<string>("QcDecision");
                }
                else
                {
                    model.QualityControlDecision = Framework.SystemID + "0201213000091";//自动判定时，默认为允收
                }

                //通过完工单号拿到对应的制程与工序流水号
                SFC_TaskDispatch TdModel = SFC_TaskDispatchService.get(model.TaskDispatchID);
                if (TdModel == null)
                {
                    return new { Status = "400", msg = "对应任务单号不存在！" };
                }
                string AutoNumberID = data.Value<string>("DocumentAutoNumberID");
                //查询所新增的检验单号是否已存在
                while (QCS_InspectionDocumentService.CheckInspectionNo(model.InspectionNo))
                {
                    AutoNumberID = null;
                    model.InspectionNo = UtilBussinessService.GetDocumentAutoNumber(userid, data.Value<string>("DocumentID"), model.InspectionDate.ToString(), ref AutoNumberID);
                }
                if (QCS_InspectionDocumentService.insert(userid, model))
                {
                    UtilBussinessService.UpdateDocumentAutoNumber(userid, AutoNumberID);
                    //如果检验单据成功添加那么根据对应的料品号与检验种类获取明细设定列表，规格表说明不足新添加条件，工序与制程
                    List<QCS_StaInsSpeSetting> StaInsSpeSetting = QCS_StaInsSpeSettingService.GetListByItemID(model.ItemID, model.InspectionMethod, TdModel.ProcessID, TdModel.OperationID);
                    if (StaInsSpeSetting == null)
                        return new { Status = "400", msg = "表头新增成功，但明细新增失败(没有对应的Qcs00004数据)" };
                    //检验明细新增
                    foreach (QCS_StaInsSpeSetting Sta in StaInsSpeSetting)
                    {
                        QCS_InspectionProject Sampling = QCS_InspectionProjectService.get(Sta.InspectionProjectID);
                        if (Sampling == null)
                            return new { Status = "400", msg = "表头新增成功，但明细新增失败(没有对应的检验项目数据)" };
                        QCS_InspectionDocumentDetails DetailsModel = new QCS_InspectionDocumentDetails();
                        DetailsModel.InspectionDocumentDetailID = UniversalService.GetSerialNumber("QCS_InspectionDocumentDetails");
                        DetailsModel.InspectionDocumentID = model.InspectionDocumentID;
                        DetailsModel.Sequence = Sta.Sequence;//排序
                        DetailsModel.InspectionStandard = Sta.InspectionStandard;//检验标准
                        DetailsModel.InspectionItemID = Sta.InspectionProjectID;//检验项目流水号用于获取项目代号与说明
                        DetailsModel.InspectionMethod = Sta.InspectionType;//检验种类
                        DetailsModel.InspectionClassID = Sta.CategoryID;//检验类别
                        DetailsModel.InspectionMethodID = Sta.InspectionMethod;//检验方式
                        DetailsModel.InspectionLevelID = Sampling.InspectionLevel;//检验水准
                        DetailsModel.InspectionFaultID = Sampling.Disadvantages;//缺点等级
                        DetailsModel.Status = Framework.SystemID + "0201213000001";
                        DetailsModel.Aql = Sta.AQL;
                        DetailsModel.AttributeType = Sta.Attribute;
                        //当没有AQL值默认为0
                        if (Sta.AQL == null)
                        {
                            DetailsModel.SampleQuantity = 0;
                            DetailsModel.AcQuantity = 0;
                            DetailsModel.ReQuantity = 0;
                        }
                        //当QCS000001对应的数据没有时，AC,RE,抽样数量设置为0
                        else
                        {
                            QCS_CheckTestSetting Check = QCS_CheckTestSettingService.getByAql(DetailsModel.InspectionLevelID, DetailsModel.InspectionMethodID, Sta.AQL);//获取抽检检验设定实体
                            if (Check == null)
                            {
                                fill++;
                                fillmsg = "部分新增失败，QCS000001没有与检验方式，检验水平，AQL对应的数据，AC,RE，抽样数量将默认为0";
                                DetailsModel.SampleQuantity = 0;
                                DetailsModel.AcQuantity = 0;
                                DetailsModel.ReQuantity = 0;
                            }
                            else
                            {
                                //当QCS000001对应的明细数据没有时，AC,RE,抽样数量设置为0
                                QCS_CheckTestSettingDetails CheckDetail = QCS_CheckTestSettingDetailsService.getCTSDetails(Check.CheckTestSettingID);//获取抽检检验设定明细实体
                                if (CheckDetail == null)
                                {
                                    fill++;
                                    fillmsg2 = "有部分QCS000001明细没有与检验方式，检验水平，AQL对应的明细数据，AC,RE，抽样数量将默认为0";
                                    DetailsModel.SampleQuantity = 0;
                                    DetailsModel.AcQuantity = 0;
                                    DetailsModel.ReQuantity = 0;
                                }
                                //decimal p = model.InspectionQuantity / CheckDetail.EndBatch;//检验数量自动计算？检验数量除以结束批量？
                                //DetailsModel.SampleQuantity = p * CheckDetail.SamplingQuantity;
                                //DetailsModel.AcQuantity = p * CheckDetail.AcQuantity;
                                //DetailsModel.ReQuantity = p * CheckDetail.ReQuantity;
                                //不确定P的计算方式，暂时注释掉
                                else
                                {
                                    DetailsModel.SampleQuantity = CheckDetail.SamplingQuantity;
                                    DetailsModel.AcQuantity = CheckDetail.AcQuantity;
                                    DetailsModel.ReQuantity = CheckDetail.ReQuantity;
                                }
                            }
                        }
                        DetailsModel.NGquantity = 0;//不良数？

                        if (model2.Value == Framework.SystemID + "02012130000B3")//查看系统设置的判定方式
                        {
                            if (DetailsModel.NGquantity >= DetailsModel.ReQuantity)
                                DetailsModel.QualityControlDecision = Framework.SystemID + "0201213000090";
                            else
                                DetailsModel.QualityControlDecision = Framework.SystemID + "0201213000091";
                        }
                        else
                        {
                            DetailsModel.QualityControlDecision = "人工判定";
                        }
                        if (model3.Value == Framework.SystemID + "02012130000B1" && DetailsModel.QualityControlDecision == Framework.SystemID + "0201213000090")
                        {//品质判定为自动且新增的明细判定为拒收
                            model.QualityControlDecision = Framework.SystemID + "0201213000090";//假如系统判定方式为自动，明细有一个为拒收，那么表头也为拒收

                            QCS_InspectionDocumentService.update(userid, model);//更新表头数据
                        }

                        QCS_InspectionDocumentDetailsService.insert(userid, DetailsModel);
                        success++;

                    }


                    //return new { status = "200", msg = "新增成功！" + fillmsg + fillmsg2 + "失败数量为：" + fill, success, fill };
                    return new { status = "200", msg = "新增成功！" };
                }
                else
                    return new { status = "410", msg = "新增失败！" };
            }
            catch (Exception ex)
            {
                DataLogerService.writeerrlog(ex);
                return new { status = "410", msg = "表头新增成功！但明细为空！" };
            }
        }

        /// <summary>
        /// 新增制程巡检单
        /// SAM 2017年10月19日11:43:12
        /// 调整了检验单明细的获取逻辑，优先判断群码再料品
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static object Qcs00008AddV1(JObject data)
        {
            string Token = data.Value<string>("Token");
            string userid = UtilBussinessService.detoken(Token);
            string SysID = Framework.SystemID;
            int success = 0, fill = 0;
            string fillmsg = null;
            string fillmsg2 = null;
            decimal sum = 0;
            try
            {

                if (string.IsNullOrWhiteSpace(data.Value<string>("InspectionNo")))
                    return new { status = "410", msg = "单据编号不能为空！" };
                /*
                //判定方式，先查找对应检验项目判定方式是什么
                MES_Parameter model2 = new MES_Parameter();//创建Inf00020实体
                model2.ParameterID = "100391101213000003";//赋值对应的检验项目判定方式流水号
                model2 = MES_ParameterService.get(model2.ParameterID);//获取对应实体
                MES_Parameter model3 = new MES_Parameter();//品质判定方式
                model3.ParameterID = "100391101213000002";
                model3 = MES_ParameterService.get(model3.ParameterID);
                */

                /*SAM 2017年9月9日23:16:28  调整*/
                MES_Parameter model2 = MES_ParameterService.get(SysID + "1101213000003");//获取对应检验项目判定方式实体
                MES_Parameter model3 = MES_ParameterService.get(SysID + "1101213000002");//获取对应品质判定方式实体

                if (model2 == null)
                    return new { status = "410", msg = "不存在检验项目判定方式的设定，请优先设定" };

                if (model3 == null)
                    return new { status = "410", msg = "不存在品质判定方式的设定，请优先设定" };

                QCS_InspectionDocument model = new QCS_InspectionDocument();
                model.InspectionDocumentID = UniversalService.GetSerialNumber("QCS_InspectionDocument");
                model.InspectionNo = data.Value<string>("InspectionNo");
                if (!string.IsNullOrWhiteSpace(data.Value<string>("DocumentDate")))
                    model.DocumentDate = data.Value<DateTime>("DocumentDate");
                model.InspectionMethod = data.Value<string>("InspectionMethod");
                model.CompletionOrderID = data.Value<string>("CompletionOrderID");
                model.ItemID = data.Value<string>("ItemID");
                model.TaskDispatchID = data.Value<string>("TaskDispatchID");
                if (!string.IsNullOrWhiteSpace(data.Value<string>("InspectionDate")))
                    model.InspectionDate = data.Value<DateTime>("InspectionDate");
                model.InspectionUserID = data.Value<string>("InspectionUserID");
                model.FinQuantity = data.Value<decimal>("FinQuantity");

                if (!string.IsNullOrWhiteSpace(data.Value<string>("InspectionQuantity")) || data.Value<decimal>("InspectionQuantity") < 0)
                {
                    model.InspectionQuantity = data.Value<decimal>("InspectionQuantity");//检验数量？
                }
                else
                {
                    return new { status = "410", msg = "新增失败！检验数量不可为空也不可小于0！" };
                }

                model.ScrappedQuantity = data.Value<decimal>("ScrappedQuantity");//报废数量
                model.NGquantity = data.Value<decimal>("NGquantity");//NG数量
                model.OKQuantity = data.Value<decimal>("OKQuantity");//OK数量
                sum = model.ScrappedQuantity + model.NGquantity + model.OKQuantity;//三个数量加起来是否等于检验数量
                if (sum != model.InspectionQuantity)
                {
                    return new { status = "410", msg = "新增失败！OK数量+NG数量+报废数量不等于检验数量！" };
                }

                model.InspectionFlag = data.Value<bool>("InspectionFlag");
                model.Status = data.Value<string>("Status");
                model.Comments = data.Value<string>("Comments");

                if (model3.Value != Framework.SystemID + "02012130000B1")//如果品质判定方式不为自动，那么由表头带入判定
                {
                    model.QualityControlDecision = data.Value<string>("QcDecision");
                }
                else
                {
                    model.QualityControlDecision = Framework.SystemID + "0201213000091";//自动判定时，默认为允收
                }

                //通过完工单号拿到对应的制程与工序流水号
                SFC_TaskDispatch TdModel = SFC_TaskDispatchService.get(model.TaskDispatchID);
                if (TdModel == null)
                {
                    return new { Status = "400", msg = "对应任务单号不存在！" };
                }
                string AutoNumberID = data.Value<string>("DocumentAutoNumberID");
                //查询所新增的检验单号是否已存在
                while (QCS_InspectionDocumentService.CheckInspectionNo(model.InspectionNo))
                {
                    AutoNumberID = null;
                    model.InspectionNo = UtilBussinessService.GetDocumentAutoNumber(userid, data.Value<string>("DocumentID"), model.InspectionDate.ToString(), ref AutoNumberID);
                }
                if (QCS_InspectionDocumentService.insert(userid, model))
                {
                    UtilBussinessService.UpdateDocumentAutoNumber(userid, AutoNumberID);
                    //如果检验单据成功添加那么根据对应的料品号与检验种类获取明细设定列表，规格表说明不足新添加条件，工序与制程
                    SYS_Items ItemModel = SYS_ItemsService.get(model.ItemID);
                    if (ItemModel == null)
                        return new { Status = "400", msg = "对应的料品代号错误" };
                    List<QCS_StaInsSpeSetting> StaInsSpeSetting = null;

                    if (!string.IsNullOrWhiteSpace(ItemModel.GroupID))
                        StaInsSpeSetting = QCS_StaInsSpeSettingService.GetListByItemID(ItemModel.GroupID, model.InspectionMethod, TdModel.ProcessID, TdModel.OperationID);
                    //如果不存在检验码或者说检验码并没有对应明细
                    if (StaInsSpeSetting == null)
                        StaInsSpeSetting = QCS_StaInsSpeSettingService.GetListByItemID(model.ItemID, model.InspectionMethod, TdModel.ProcessID, TdModel.OperationID);

                    if (StaInsSpeSetting == null)
                        return new { Status = "400", msg = "表头新增成功，但明细新增失败(没有对应的Qcs00004数据)" };
                    //检验明细新增
                    foreach (QCS_StaInsSpeSetting Sta in StaInsSpeSetting)
                    {
                        QCS_InspectionProject Sampling = QCS_InspectionProjectService.get(Sta.InspectionProjectID);
                        if (Sampling == null)
                            return new { Status = "400", msg = "表头新增成功，但明细新增失败(没有对应的检验项目数据)" };
                        QCS_InspectionDocumentDetails DetailsModel = new QCS_InspectionDocumentDetails();
                        DetailsModel.InspectionDocumentDetailID = UniversalService.GetSerialNumber("QCS_InspectionDocumentDetails");
                        DetailsModel.InspectionDocumentID = model.InspectionDocumentID;
                        DetailsModel.Sequence = Sta.Sequence;//排序
                        DetailsModel.InspectionStandard = Sta.InspectionStandard;//检验标准
                        DetailsModel.InspectionItemID = Sta.InspectionProjectID;//检验项目流水号用于获取项目代号与说明
                        DetailsModel.InspectionMethod = Sta.InspectionType;//检验种类
                        DetailsModel.InspectionClassID = Sta.CategoryID;//检验类别
                        DetailsModel.InspectionMethodID = Sta.InspectionMethod;//检验方式
                        DetailsModel.InspectionLevelID = Sampling.InspectionLevel;//检验水准
                        DetailsModel.InspectionFaultID = Sampling.Disadvantages;//缺点等级
                        DetailsModel.Status = Framework.SystemID + "0201213000001";
                        DetailsModel.Aql = Sta.AQL;
                        DetailsModel.AttributeType = Sta.Attribute;
                        //当没有AQL值默认为0
                        if (Sta.AQL == null)
                        {
                            DetailsModel.SampleQuantity = 0;
                            DetailsModel.AcQuantity = 0;
                            DetailsModel.ReQuantity = 0;
                        }
                        //当QCS000001对应的数据没有时，AC,RE,抽样数量设置为0
                        else
                        {
                            QCS_CheckTestSetting Check = QCS_CheckTestSettingService.getByAql(DetailsModel.InspectionLevelID, DetailsModel.InspectionMethodID, Sta.AQL);//获取抽检检验设定实体
                            if (Check == null)
                            {
                                fill++;
                                fillmsg = "部分新增失败，QCS000001没有与检验方式，检验水平，AQL对应的数据，AC,RE，抽样数量将默认为0";
                                DetailsModel.SampleQuantity = 0;
                                DetailsModel.AcQuantity = 0;
                                DetailsModel.ReQuantity = 0;
                            }
                            else
                            {
                                //当QCS000001对应的明细数据没有时，AC,RE,抽样数量设置为0
                                QCS_CheckTestSettingDetails CheckDetail = QCS_CheckTestSettingDetailsService.getCTSDetails(Check.CheckTestSettingID);//获取抽检检验设定明细实体
                                if (CheckDetail == null)
                                {
                                    fill++;
                                    fillmsg2 = "有部分QCS000001明细没有与检验方式，检验水平，AQL对应的明细数据，AC,RE，抽样数量将默认为0";
                                    DetailsModel.SampleQuantity = 0;
                                    DetailsModel.AcQuantity = 0;
                                    DetailsModel.ReQuantity = 0;
                                }
                                //decimal p = model.InspectionQuantity / CheckDetail.EndBatch;//检验数量自动计算？检验数量除以结束批量？
                                //DetailsModel.SampleQuantity = p * CheckDetail.SamplingQuantity;
                                //DetailsModel.AcQuantity = p * CheckDetail.AcQuantity;
                                //DetailsModel.ReQuantity = p * CheckDetail.ReQuantity;
                                //不确定P的计算方式，暂时注释掉
                                else
                                {
                                    DetailsModel.SampleQuantity = CheckDetail.SamplingQuantity;
                                    DetailsModel.AcQuantity = CheckDetail.AcQuantity;
                                    DetailsModel.ReQuantity = CheckDetail.ReQuantity;
                                }
                            }
                        }
                        DetailsModel.NGquantity = 0;//不良数？

                        if (model2.Value == Framework.SystemID + "02012130000B3")//查看系统设置的判定方式
                        {
                            if (DetailsModel.NGquantity >= DetailsModel.ReQuantity)
                                DetailsModel.QualityControlDecision = Framework.SystemID + "0201213000090";
                            else
                                DetailsModel.QualityControlDecision = Framework.SystemID + "0201213000091";
                        }
                        else
                        {
                            DetailsModel.QualityControlDecision = "人工判定";
                        }
                        if (model3.Value == Framework.SystemID + "02012130000B1" && DetailsModel.QualityControlDecision == Framework.SystemID + "0201213000090")
                        {//品质判定为自动且新增的明细判定为拒收
                            model.QualityControlDecision = Framework.SystemID + "0201213000090";//假如系统判定方式为自动，明细有一个为拒收，那么表头也为拒收

                            QCS_InspectionDocumentService.update(userid, model);//更新表头数据
                        }

                        QCS_InspectionDocumentDetailsService.insert(userid, DetailsModel);
                        success++;

                    }


                    //return new { status = "200", msg = "新增成功！" + fillmsg + fillmsg2 + "失败数量为：" + fill, success, fill };
                    return new { status = "200", msg = "新增成功！" };
                }
                else
                    return new { status = "410", msg = "新增失败！" };
            }
            catch (Exception ex)
            {
                DataLogerService.writeerrlog(ex);
                return new { status = "410", msg = "表头新增成功！但明细为空！" };
            }
        }

        /// <summary>
        /// 新增制程巡检单
        /// SAM 2017年10月23日17:39:05
        /// 在V1的版本上调整了部分逻辑。在寻找抽样检验设定资料时，加多了分派量是否在范围内的判断
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static object Qcs00008AddV2(JObject data)
        {
            string Token = data.Value<string>("Token");
            string userid = UtilBussinessService.detoken(Token);
            string SysID = Framework.SystemID;
            decimal sum = 0;
            try
            {

                if (string.IsNullOrWhiteSpace(data.Value<string>("InspectionNo")))
                    return new { status = "410", msg = "单据编号不能为空！" };

                //判定方式，先查找对应检验项目判定方式是什么
                /*SAM 2017年9月9日23:16:28  调整*/
                MES_Parameter model2 = MES_ParameterService.get(SysID + "1101213000003");//获取对应检验项目判定方式实体
                MES_Parameter model3 = MES_ParameterService.get(SysID + "1101213000002");//获取对应品质判定方式实体

                if (model2 == null)
                    return new { status = "410", msg = "不存在检验项目判定方式的设定，请优先设定" };

                if (model3 == null)
                    return new { status = "410", msg = "不存在品质判定方式的设定，请优先设定" };

                QCS_InspectionDocument model = new QCS_InspectionDocument();
                model.InspectionDocumentID = UniversalService.GetSerialNumber("QCS_InspectionDocument");
                model.InspectionNo = data.Value<string>("InspectionNo");
                if (!string.IsNullOrWhiteSpace(data.Value<string>("DocumentDate")))
                    model.DocumentDate = data.Value<DateTime>("DocumentDate");
                model.InspectionMethod = data.Value<string>("InspectionMethod");
                model.CompletionOrderID = data.Value<string>("CompletionOrderID");
                model.ItemID = data.Value<string>("ItemID");
                model.TaskDispatchID = data.Value<string>("TaskDispatchID");
                if (!string.IsNullOrWhiteSpace(data.Value<string>("InspectionDate")))
                    model.InspectionDate = data.Value<DateTime>("InspectionDate");
                model.InspectionUserID = data.Value<string>("InspectionUserID");
                model.FinQuantity = data.Value<decimal>("FinQuantity");

                if (string.IsNullOrWhiteSpace(data.Value<string>("InspectionQuantity")))
                    return new { status = "410", msg = "新增失败！检验数量不能为空！" };

                if (data.Value<decimal>("InspectionQuantity") < 0)
                    return new { status = "410", msg = "新增失败！检验数量不不可小于0！" };

                model.InspectionQuantity = data.Value<decimal>("InspectionQuantity");//检验数量
                model.ScrappedQuantity = data.Value<decimal>("ScrappedQuantity");//报废数量
                model.NGquantity = data.Value<decimal>("NGquantity");//NG数量
                model.OKQuantity = data.Value<decimal>("OKQuantity");//OK数量
                sum = model.ScrappedQuantity + model.NGquantity + model.OKQuantity;//三个数量加起来是否等于检验数量
                if (sum != model.InspectionQuantity)
                {
                    return new { status = "410", msg = "新增失败！OK数量+NG数量+报废数量不等于检验数量！" };
                }

                model.InspectionFlag = data.Value<bool>("InspectionFlag");
                model.Status = data.Value<string>("Status");
                model.Comments = data.Value<string>("Comments");

                if (model3.Value != Framework.SystemID + "02012130000B1")//如果品质判定方式不为自动，那么由表头带入判定
                {
                    model.QualityControlDecision = data.Value<string>("QcDecision");
                }
                else
                {
                    model.QualityControlDecision = Framework.SystemID + "0201213000091";//自动判定时，默认为允收
                }

                //通过完工单号拿到对应的制程与工序流水号
                SFC_TaskDispatch TdModel = SFC_TaskDispatchService.get(model.TaskDispatchID);
                if (TdModel == null)
                {
                    return new { Status = "400", msg = "对应任务单号不存在！" };
                }
                string AutoNumberID = data.Value<string>("DocumentAutoNumberID");
                //查询所新增的检验单号是否已存在
                while (QCS_InspectionDocumentService.CheckInspectionNo(model.InspectionNo))
                {
                    AutoNumberID = null;
                    model.InspectionNo = UtilBussinessService.GetDocumentAutoNumber(userid, data.Value<string>("DocumentID"), model.InspectionDate.ToString(), ref AutoNumberID);
                }
                if (QCS_InspectionDocumentService.insert(userid, model))
                {
                    UtilBussinessService.UpdateDocumentAutoNumber(userid, AutoNumberID);
                    //如果检验单据成功添加那么根据对应的料品号与检验种类获取明细设定列表，规格表说明不足新添加条件，工序与制程
                    SYS_Items ItemModel = SYS_ItemsService.get(model.ItemID);
                    if (ItemModel == null)
                        return new { Status = "400", msg = "对应的料品代号错误" };
                    List<QCS_StaInsSpeSetting> StaInsSpeSetting = null;

                    if (!string.IsNullOrWhiteSpace(ItemModel.GroupID))
                        StaInsSpeSetting = QCS_StaInsSpeSettingService.GetListByItemID(ItemModel.GroupID, model.InspectionMethod, TdModel.ProcessID, TdModel.OperationID);
                    //如果不存在检验码或者说检验码并没有对应明细
                    if (StaInsSpeSetting == null)
                        StaInsSpeSetting = QCS_StaInsSpeSettingService.GetListByItemID(model.ItemID, model.InspectionMethod, TdModel.ProcessID, TdModel.OperationID);

                    if (StaInsSpeSetting == null)
                        return new { Status = "400", msg = "表头新增成功，但明细新增失败(没有对应的Qcs00004数据)" };
                    //检验明细新增
                    foreach (QCS_StaInsSpeSetting Sta in StaInsSpeSetting)
                    {
                        QCS_InspectionProject Sampling = QCS_InspectionProjectService.get(Sta.InspectionProjectID);
                        if (Sampling == null)
                            return new { Status = "400", msg = "表头新增成功，但明细新增失败(没有对应的检验项目数据)" };
                        QCS_InspectionDocumentDetails DetailsModel = new QCS_InspectionDocumentDetails();
                        DetailsModel.InspectionDocumentDetailID = UniversalService.GetSerialNumber("QCS_InspectionDocumentDetails");
                        DetailsModel.InspectionDocumentID = model.InspectionDocumentID;
                        DetailsModel.Sequence = Sta.Sequence;//排序
                        DetailsModel.InspectionStandard = Sta.InspectionStandard;//检验标准
                        DetailsModel.InspectionItemID = Sta.InspectionProjectID;//检验项目流水号用于获取项目代号与说明
                        DetailsModel.InspectionMethod = Sta.InspectionType;//检验种类
                        DetailsModel.InspectionClassID = Sta.CategoryID;//检验类别
                        DetailsModel.InspectionMethodID = Sta.InspectionMethod;//检验方式
                        DetailsModel.InspectionLevelID = Sampling.InspectionLevel;//检验水准
                        DetailsModel.InspectionFaultID = Sampling.Disadvantages;//缺点等级
                        DetailsModel.Status = Framework.SystemID + "0201213000001";
                        DetailsModel.Aql = Sta.AQL;
                        DetailsModel.AttributeType = Sta.Attribute;
                        //当没有AQL值默认为0
                        if (Sta.AQL == null)
                        {
                            DetailsModel.SampleQuantity = 0;
                            DetailsModel.AcQuantity = 0;
                            DetailsModel.ReQuantity = 0;
                        }
                        //当QCS000001对应的数据没有时，AC,RE,抽样数量设置为0
                        else
                        {
                            QCS_CheckTestSetting Check = QCS_CheckTestSettingService.getByAql(DetailsModel.InspectionLevelID, DetailsModel.InspectionMethodID, Sta.AQL);//获取抽检检验设定实体
                            if (Check == null)
                            {
                                //fill++;
                                //fillmsg = "部分新增失败，QCS000001没有与检验方式，检验水平，AQL对应的数据，AC,RE，抽样数量将默认为0";
                                DetailsModel.SampleQuantity = 0;
                                DetailsModel.AcQuantity = 0;
                                DetailsModel.ReQuantity = 0;
                            }
                            else
                            {
                                //当QCS000001对应的明细数据没有时，AC,RE,抽样数量设置为0
                                QCS_CheckTestSettingDetails CheckDetail = QCS_CheckTestSettingDetailsService.GetDetails(Check.CheckTestSettingID, TdModel.DispatchQuantity);//获取抽检检验设定明细实体
                                //QCS_CheckTestSettingDetails CheckDetail = QCS_CheckTestSettingDetailsService.getCTSDetails(Check.CheckTestSettingID);//获取抽检检验设定明细实体
                                if (CheckDetail == null)
                                {
                                    DetailsModel.SampleQuantity = 0;
                                    DetailsModel.AcQuantity = 0;
                                    DetailsModel.ReQuantity = 0;
                                }
                                //不确定P的计算方式，暂时注释掉
                                else
                                {
                                    DetailsModel.SampleQuantity = CheckDetail.SamplingQuantity;
                                    DetailsModel.AcQuantity = CheckDetail.AcQuantity;
                                    DetailsModel.ReQuantity = CheckDetail.ReQuantity;
                                }
                            }
                        }
                        DetailsModel.NGquantity = 0;//不良数？

                        if (model2.Value == Framework.SystemID + "02012130000B3")//查看系统设置的判定方式
                        {
                            if (DetailsModel.NGquantity >= DetailsModel.ReQuantity)
                                DetailsModel.QualityControlDecision = Framework.SystemID + "0201213000090";
                            else
                                DetailsModel.QualityControlDecision = Framework.SystemID + "0201213000091";
                        }
                        else
                        {
                            DetailsModel.QualityControlDecision = "人工判定";
                        }
                        if (model3.Value == Framework.SystemID + "02012130000B1" && DetailsModel.QualityControlDecision == Framework.SystemID + "0201213000090")
                        {//品质判定为自动且新增的明细判定为拒收
                            model.QualityControlDecision = Framework.SystemID + "0201213000090";//假如系统判定方式为自动，明细有一个为拒收，那么表头也为拒收

                            QCS_InspectionDocumentService.update(userid, model);//更新表头数据
                        }

                        QCS_InspectionDocumentDetailsService.insert(userid, DetailsModel);
                    }


                    //return new { status = "200", msg = "新增成功！" + fillmsg + fillmsg2 + "失败数量为：" + fill, success, fill };
                    return new { status = "200", msg = "新增成功！" };
                }
                else
                    return new { status = "410", msg = "新增失败！" };
            }
            catch (Exception ex)
            {
                DataLogerService.writeerrlog(ex);
                return new { status = "410", msg = "表头新增成功！但明细为空！" };
            }
        }

        /// <summary>
        /// 新增制程巡检单
        /// SAM 2017年10月23日17:39:05
        /// 在V2的版本上，调整了一些代码，修正了bug
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static object Qcs00008AddV3(JObject data)
        {
            string Token = data.Value<string>("Token");
            string userid = UtilBussinessService.detoken(Token);
            string SysID = Framework.SystemID;
            decimal sum = 0;
            try
            {

                if (string.IsNullOrWhiteSpace(data.Value<string>("InspectionNo")))
                    return new { status = "410", msg = "单据编号不能为空！" };

                //判定方式，先查找对应检验项目判定方式是什么
                /*SAM 2017年9月9日23:16:28  调整*/
                MES_Parameter model2 = MES_ParameterService.get(SysID + "1101213000003");//获取对应检验项目判定方式实体
                MES_Parameter model3 = MES_ParameterService.get(SysID + "1101213000002");//获取对应品质判定方式实体

                if (model2 == null)
                    return new { status = "410", msg = "不存在检验项目判定方式的设定，请优先设定" };

                if (model3 == null)
                    return new { status = "410", msg = "不存在品质判定方式的设定，请优先设定" };

                QCS_InspectionDocument model = new QCS_InspectionDocument();
                model.InspectionDocumentID = UniversalService.GetSerialNumber("QCS_InspectionDocument");
                model.InspectionNo = data.Value<string>("InspectionNo");
                if (!string.IsNullOrWhiteSpace(data.Value<string>("DocumentDate")))
                    model.DocumentDate = data.Value<DateTime>("DocumentDate");
                model.InspectionMethod = data.Value<string>("InspectionMethod");
                model.CompletionOrderID = data.Value<string>("CompletionOrderID");
                model.ItemID = data.Value<string>("ItemID");
                model.TaskDispatchID = data.Value<string>("TaskDispatchID");
                if (!string.IsNullOrWhiteSpace(data.Value<string>("InspectionDate")))
                    model.InspectionDate = data.Value<DateTime>("InspectionDate");
                model.InspectionUserID = data.Value<string>("InspectionUserID");
                model.FinQuantity = data.Value<decimal>("FinQuantity");

                if (string.IsNullOrWhiteSpace(data.Value<string>("InspectionQuantity")))
                    return new { status = "410", msg = "新增失败！检验数量不能为空！" };

                if (data.Value<decimal>("InspectionQuantity") < 0)
                    return new { status = "410", msg = "新增失败！检验数量不不可小于0！" };

                model.InspectionQuantity = data.Value<decimal>("InspectionQuantity");//检验数量
                model.ScrappedQuantity = data.Value<decimal>("ScrappedQuantity");//报废数量
                model.NGquantity = data.Value<decimal>("NGquantity");//NG数量
                model.OKQuantity = data.Value<decimal>("OKQuantity");//OK数量
                sum = model.ScrappedQuantity + model.NGquantity + model.OKQuantity;//三个数量加起来是否等于检验数量
                if (sum != model.InspectionQuantity)
                {
                    return new { status = "410", msg = "新增失败！OK数量+NG数量+报废数量不等于检验数量！" };
                }

                model.InspectionFlag = data.Value<bool>("InspectionFlag");
                model.Status = data.Value<string>("Status");
                model.Comments = data.Value<string>("Comments");

                if (model3.Value != Framework.SystemID + "02012130000B1")//如果品质判定方式不为自动，那么由表头带入判定
                {
                    model.QualityControlDecision = Framework.SystemID + "0201213000091";//需求修改默认为允收
                }
                else
                {
                    model.QualityControlDecision = Framework.SystemID + "0201213000091";//自动判定时，默认为允收
                }

                //通过完工单号拿到对应的制程与工序流水号
                SFC_TaskDispatch TdModel = SFC_TaskDispatchService.get(model.TaskDispatchID);
                if (TdModel == null)
                {
                    return new { status = "410", msg = "对应任务单号不存在！" };
                }
                string AutoNumberID = data.Value<string>("DocumentAutoNumberID");
                //查询所新增的检验单号是否已存在
                //while (QCS_InspectionDocumentService.CheckInspectionNo(model.InspectionNo))
                //{
                //    AutoNumberID = null;
                //    model.InspectionNo = UtilBussinessService.GetDocumentAutoNumber(userid, data.Value<string>("DocumentID"), model.InspectionDate.ToString(), ref AutoNumberID);
                //}

                /*当保存时,如果检验单号已存在，则自动获取下一检验单号*/
                /*SAM 2017年10月27日16:36:36*/
                /*
                 * 因为存在一个情况是检验单号新增进去了但是并没有及时更新流水，造成了无限死循环。
                 * 所以目前做了一个机制，循环计数，如果循环超过5次，就更新一次流水,然后重置循环次数
                 */
                int Seq = 1;
                while (QCS_InspectionDocumentService.CheckInspectionNo(model.InspectionNo))
                {
                    if (Seq == 5)
                    {
                        UtilBussinessService.UpdateDocumentAutoNumber(userid, AutoNumberID);
                        Seq = 1;
                    }
                    else
                    {
                        AutoNumberID = null;
                        model.InspectionNo = UtilBussinessService.GetDocumentAutoNumber(userid, data.Value<string>("DocumentID"), model.InspectionDate.ToString(), ref AutoNumberID);
                        Seq++;
                    }
                }
                if (QCS_InspectionDocumentService.insert(userid, model))
                {
                    UtilBussinessService.UpdateDocumentAutoNumber(userid, AutoNumberID);
                    //如果检验单据成功添加那么根据对应的料品号与检验种类获取明细设定列表，规格表说明不足新添加条件，工序与制程
                    SYS_Items ItemModel = SYS_ItemsService.get(model.ItemID);
                    if (ItemModel == null)
                        return new { status = "400", msg = "对应的料品代号错误" };

                    List<QCS_StaInsSpeSetting> StaInsSpeSetting = null;
                    //如果存在检验码，则拿去检验码的明细
                    if (!string.IsNullOrWhiteSpace(ItemModel.GroupID))
                        StaInsSpeSetting = QCS_StaInsSpeSettingService.GetListByPartID(ItemModel.GroupID, model.InspectionMethod, Framework.SystemID + "020121300007C", TdModel.ProcessID, TdModel.OperationID);
                    //如果不存在检验码或者说检验码并没有对应明细
                    if (StaInsSpeSetting == null)
                        StaInsSpeSetting = QCS_StaInsSpeSettingService.GetListByPartID(model.ItemID, model.InspectionMethod, Framework.SystemID + "020121300007B", TdModel.ProcessID, TdModel.OperationID);

                    if (StaInsSpeSetting == null)
                        return new { status = "400", msg = "表头新增成功，但明细新增失败(没有对应的Qcs00004数据)" };
                    //检验明细新增
                    foreach (QCS_StaInsSpeSetting Sta in StaInsSpeSetting)
                    {
                        QCS_InspectionProject Sampling = QCS_InspectionProjectService.get(Sta.InspectionProjectID);
                        if (Sampling == null)
                            return new { status = "400", msg = "表头新增成功，但明细新增失败(没有对应的检验项目数据)" };
                        QCS_InspectionDocumentDetails DetailsModel = new QCS_InspectionDocumentDetails();
                        DetailsModel.InspectionDocumentDetailID = UniversalService.GetSerialNumber("QCS_InspectionDocumentDetails");
                        DetailsModel.InspectionDocumentID = model.InspectionDocumentID;
                        DetailsModel.Sequence = Sta.Sequence;//排序
                        DetailsModel.InspectionStandard = Sta.InspectionStandard;//检验标准
                        DetailsModel.InspectionItemID = Sta.InspectionProjectID;//检验项目流水号用于获取项目代号与说明
                        DetailsModel.InspectionMethod = Sta.InspectionType;//检验种类
                        DetailsModel.InspectionClassID = Sta.CategoryID;//检验类别
                        DetailsModel.InspectionMethodID = Sta.InspectionMethod;//检验方式
                        DetailsModel.InspectionLevelID = Sampling.InspectionLevel;//检验水准
                        DetailsModel.InspectionFaultID = Sampling.Disadvantages;//缺点等级
                        DetailsModel.Status = Framework.SystemID + "0201213000001";
                        DetailsModel.Aql = Sta.AQL;
                        DetailsModel.AttributeType = Sta.Attribute;
                        //当没有AQL值默认为0
                        if (Sta.AQL == null)
                        {
                            DetailsModel.SampleQuantity = 0;
                            DetailsModel.AcQuantity = 0;
                            DetailsModel.ReQuantity = 0;
                        }
                        //当QCS000001对应的数据没有时，AC,RE,抽样数量设置为0
                        else
                        {
                            QCS_CheckTestSetting Check = QCS_CheckTestSettingService.getByAql(DetailsModel.InspectionLevelID, DetailsModel.InspectionMethodID, Sta.AQL);//获取抽检检验设定实体
                            if (Check == null)
                            {
                                DetailsModel.SampleQuantity = 0;
                                DetailsModel.AcQuantity = 0;
                                DetailsModel.ReQuantity = 0;
                            }
                            else
                            {
                                //当QCS000001对应的明细数据没有时，AC,RE,抽样数量设置为0
                                QCS_CheckTestSettingDetails CheckDetail = QCS_CheckTestSettingDetailsService.GetDetails(Check.CheckTestSettingID, TdModel.DispatchQuantity);//获取抽检检验设定明细实体
                                if (CheckDetail == null)
                                {
                                    DetailsModel.SampleQuantity = 0;
                                    DetailsModel.AcQuantity = 0;
                                    DetailsModel.ReQuantity = 0;
                                }
                                else
                                {
                                    DetailsModel.SampleQuantity = CheckDetail.SamplingQuantity;
                                    DetailsModel.AcQuantity = CheckDetail.AcQuantity;
                                    DetailsModel.ReQuantity = CheckDetail.ReQuantity;
                                }
                            }
                        }
                        DetailsModel.NGquantity = 0;//不良数？

                        if (model2.Value == Framework.SystemID + "02012130000B3")//查看系统设置的判定方式
                        {
                            if (DetailsModel.NGquantity >= DetailsModel.ReQuantity)
                                DetailsModel.QualityControlDecision = Framework.SystemID + "0201213000090";
                            else
                                DetailsModel.QualityControlDecision = Framework.SystemID + "0201213000091";
                        }
                        else
                        {
                            DetailsModel.QualityControlDecision = Framework.SystemID + "0201213000091";//人工判定时默认为允收
                        }
                        if (model3.Value == Framework.SystemID + "02012130000B1" && DetailsModel.QualityControlDecision == Framework.SystemID + "0201213000090")
                        {//品质判定为自动且新增的明细判定为拒收
                            model.QualityControlDecision = Framework.SystemID + "0201213000090";//假如系统判定方式为自动，明细有一个为拒收，那么表头也为拒收

                            QCS_InspectionDocumentService.update(userid, model);//更新表头数据
                        }

                        QCS_InspectionDocumentDetailsService.insert(userid, DetailsModel);
                    }
                    //return new { status = "200", msg = "新增成功！" + fillmsg + fillmsg2 + "失败数量为：" + fill, success, fill };
                    return new { status = "200", msg = "新增成功！" };
                }
                else
                    return new { status = "410", msg = "新增失败！" };
            }
            catch (Exception ex)
            {
                DataLogerService.writeerrlog(ex);
                return new { status = "410", msg = "表头新增成功！但明细为空！" };
            }
        }

        /// <summary>
        /// 制程巡检检验表头—更新
        /// Joint
        /// </summary>
        /// <param name="Token"></param>
        /// <param name="jArray"></param>
        /// <returns></returns>
        public static object Qcs00008update(string Token, JArray jArray)
        {
            int success = 0;
            int fail = 0;
            string failIDs = null;
            string msg = null;
            string userid = UtilBussinessService.detoken(Token);
            JObject data = null;
            QCS_InspectionDocument model = null;
            string SysId = Framework.SystemID;
            for (int i = 0; i < jArray.Count; i++)
            {

                data = (JObject)jArray[i];
                model = QCS_InspectionDocumentService.get(data.Value<string>("InspectionDocumentID"));
                if (model.Status == SysId + "020121300008E" || model.Status == SysId + "020121300008F")//状态为CA确认,CL作废时不可新增修改
                {

                    failIDs = UtilBussinessService.str(failIDs, data.Value<string>("InspectionDocumentID"));
                    msg = UtilBussinessService.str(msg, model.InspectionNo + "状态为CA确认或CL作废,不能修改");
                    fail++;
                    continue;
                }
                model.InspectionDocumentID = data.Value<string>("InspectionDocumentID");
                if (!string.IsNullOrWhiteSpace(data.Value<string>("InspectionDate")))
                    model.DocumentDate = data.Value<DateTime>("DocumentDate");
                model.InspectionMethod = data.Value<string>("InspectionMethod");
                model.CompletionOrderID = data.Value<string>("FinishID");
                model.ItemID = data.Value<string>("ItemID");
                model.TaskDispatchID = data.Value<string>("TaskDispatchID");
                if (!string.IsNullOrWhiteSpace(data.Value<string>("InspectionDate")))
                    model.InspectionDate = data.Value<DateTime>("InspectionDate");
                model.InspectionUserID = data.Value<string>("InspectionUserID");
                model.QualityControlDecision = data.Value<string>("QcDecision");

                //防止前端传空值报错，为空时默认为0
                if (string.IsNullOrWhiteSpace(data.Value<string>("FinQuantity")))
                {
                    model.FinQuantity = 0;
                }
                else
                {
                    model.FinQuantity = data.Value<decimal>("FinQuantity");
                }
                //防止前端传空值报错，为空时默认为0
                if (string.IsNullOrWhiteSpace(data.Value<string>("InspectionQuantity")))
                {
                    model.InspectionQuantity = 0;
                }
                else
                {
                    model.InspectionQuantity = data.Value<decimal>("InspectionQuantity");
                }
                //防止前端传空值报错，为空时默认为0
                if (string.IsNullOrWhiteSpace(data.Value<string>("ScrappedQuantity")))
                {
                    model.ScrappedQuantity = 0;
                }
                else
                {
                    model.ScrappedQuantity = data.Value<decimal>("ScrappedQuantity");
                }
                //防止前端传空值报错，为空时默认为0
                if (string.IsNullOrWhiteSpace(data.Value<string>("NGquantity")))
                {
                    model.NGquantity = 0;
                }
                else
                {
                    model.NGquantity = data.Value<decimal>("NGquantity");
                }
                //防止前端传空值报错，为空时默认为0
                if (string.IsNullOrWhiteSpace(data.Value<string>("OKQuantity")))
                {
                    model.OKQuantity = 0;
                }
                else
                {
                    model.OKQuantity = data.Value<decimal>("OKQuantity");
                }

                model.InspectionFlag = data.Value<bool>("InspectionFlag");
                model.Status = data.Value<string>("Status");
                model.Comments = data.Value<string>("Comments");
                if (QCS_InspectionDocumentService.update(userid, model))
                    success++;
                else
                {
                    failIDs = UtilBussinessService.str(failIDs, data.Value<string>("InspectionDocumentID"));
                    fail++;
                }

            }
            return new { success = success, fail = fail, failIDs = failIDs, msg = msg };
        }

        /// <summary>
        /// 制程巡检检验维护表头修改（移动端）
        /// Alvin 2017年9月12日16:07:38
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public static object Qcs00008UpdateByOne(JObject request)
        {
            string Token = request.Value<string>("Token");//获取传进来的token
            string userid = UtilBussinessService.detoken(Token);//获取登录人的流水号
            string SysId = Framework.SystemID;//系统代号
            QCS_InspectionDocument model = null;//初始化model
            try
            {
                if (string.IsNullOrWhiteSpace(request.Value<string>("InspectionDocumentID")))
                {
                    return new { status = "410", msg = "检验单据流水号不能为空！" };
                }

                model = QCS_InspectionDocumentService.get(request.Value<string>("InspectionDocumentID"));
                if (model.Status == SysId + "020121300008E" || model.Status == SysId + "020121300008F")//状态为CA确认,CL作废时不可修改
                {
                    return new { status = "410", msg = "状态为CA确认或CL作废,不能修改！" };
                }

                model.InspectionDocumentID = request.Value<string>("InspectionDocumentID");//流水号
                if (!string.IsNullOrWhiteSpace(request.Value<string>("DocumentDate")))
                    model.DocumentDate = request.Value<DateTime>("DocumentDate");//單據日期               
                model.QualityControlDecision = request.Value<string>("QcDecision");//品質判定，值得获取字段 QcDecision            
                model.ScrappedQuantity = request.Value<decimal>("ScrappedQuantity");//报废量
                model.NGquantity = request.Value<decimal>("NGquantity");//NG量              
                model.Comments = request.Value<string>("Comments");

                if (QCS_InspectionDocumentService.Qcs00008UpdateByOne(userid, model))
                {
                    return new { status = "200", msg = "修改成功！" };
                }
                else
                {
                    return new { status = "410", msg = "修改失败！" };
                }
            }
            catch (Exception ex)
            {
                DataLogerService.writeerrlog(ex);
                return new { status = "410", msg = "修改失败！" + ex.ToString() };
            }

        }



        /// <summary>
        /// 获取检验明细列表
        /// Joint
        /// </summary>
        /// <param name="Token"></param>
        /// <param name="InspectionNo"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <returns></returns>
        public static object Qcs00008GetDetailsList(string Token, string InspectionNo, int page, int rows)
        {
            int count = 0;
            return UtilBussinessService.getPaginationModel(QCS_InspectionDocumentDetailsService.Qcs00005GetDetailsList(InspectionNo, page, rows, ref count), count);
        }

        /// <summary>
        /// 检验单明细更新
        /// Joint
        /// </summary>
        /// <param name="Token"></param>
        /// <param name="jArray"></param>
        /// <returns></returns>
        public static object Qcs00008DetailUpdate(string Token, JArray jArray)
        {
            int success = 0;
            int fail = 0;
            string failIDs = null;
            string msg = null;
            string userid = UtilBussinessService.detoken(Token);
            JObject data = null;
            QCS_InspectionDocumentDetails model = null;
            QCS_InspectionDocument Headermodel = null;
            string SysId = Framework.SystemID;
            for (int i = 0; i < jArray.Count; i++)
            {
                data = (JObject)jArray[i];
                Headermodel = QCS_InspectionDocumentService.get(data.Value<string>("InspectionDocumentID"));
                if (Headermodel == null)
                {
                    failIDs = UtilBussinessService.str(failIDs, data.Value<string>("InspectionDocumentID"));
                    msg = UtilBussinessService.str(msg, data.Value<string>("Sequence") + "的表头流水号为空");
                    fail++;
                    continue;
                }
                if (Headermodel.ItemID == null)
                {
                    failIDs = UtilBussinessService.str(failIDs, data.Value<string>("InspectionDocumentID"));
                    msg = UtilBussinessService.str(msg, data.Value<string>("Sequence") + data.Value<string>("InspectionDocumentID") + "的料品流水号为空");
                    fail++;
                    continue;
                }
                if (Headermodel.Status == SysId + "020121300008E" || Headermodel.Status == SysId + "020121300008F")//状态为CA确认,CL作废时不可新增修改
                {

                    failIDs = UtilBussinessService.str(failIDs, data.Value<string>("InspectionNo"));
                    msg = UtilBussinessService.str(msg, Headermodel.InspectionNo + "状态为CA确认或CL作废,不能修改");
                    fail++;
                    continue;
                }
                model = new QCS_InspectionDocumentDetails();
                model.InspectionDocumentDetailID = data.Value<string>("InspectionDocumentDetailID");
                model.InspectionDocumentID = data.Value<string>("InspectionDocumentID");
                model.Sequence = string.IsNullOrWhiteSpace(data.Value<string>("Sequence")) ? 0 : data.Value<int>("Sequence");
                model.InspectionStandard = data.Value<string>("InspectionStandard");
                model.InspectionMethod = data.Value<string>("InspectionMethod");
                model.InspectionClassID = data.Value<string>("InspectionClassID");
                model.InspectionMethodID = data.Value<string>("InspectionMethodID");
                model.InspectionItemID = data.Value<string>("InspectionItemID");
                model.InspectionLevelID = data.Value<string>("InspectionLevelID");
                model.InspectionFaultID = data.Value<string>("InspectionFaultID");
                model.SampleQuantity = data.Value<decimal>("SampleQuantity");
                model.Aql = data.Value<string>("Aql");
                model.Status = data.Value<string>("Status");
                model.AcQuantity = data.Value<decimal>("AcQuantity");
                model.ReQuantity = data.Value<decimal>("ReQuantity");
                model.NGquantity = data.Value<decimal>("NGquantity");
                model.Attribute = data.Value<string>("Attribute");
                model.QualityControlDecision = data.Value<string>("QcDecision");
                MES_Parameter model2 = MES_ParameterService.get(Framework.SystemID + "1101213000003");//获取对应检验项目判定方式实体
                if (model2.Value == Framework.SystemID + "02012130000B3")//如果明细判定方式为自动
                {
                    if (model.NGquantity >= model.ReQuantity)//当明细修改时，不良数大于等于re数，将明细判定改为拒收
                    {
                        model.QualityControlDecision = Framework.SystemID + "0201213000090";
                    }
                    else
                    {
                        model.QualityControlDecision = Framework.SystemID + "0201213000091";
                    }
                }
                QCS_InspectionDocument QCS_ID = new QCS_InspectionDocument();
                QCS_ID = QCS_InspectionDocumentService.get(model.InspectionDocumentID);
                MES_Parameter model3 = new MES_Parameter();//品质判定方式
                model3.ParameterID = Framework.SystemID + "1101213000002";
                model3 = MES_ParameterService.get(model3.ParameterID);
                if (model3.Value == Framework.SystemID + "02012130000B1")//如果表头品质判定方式为自动
                {
                    if (model.QualityControlDecision == Framework.SystemID + "0201213000090")//当明细更改为拒收，那么获取表头实体并修改表头验收字段
                    {
                        QCS_ID.QualityControlDecision = Framework.SystemID + "0201213000090";//明细为拒收，表头也更新为拒收
                        QCS_InspectionDocumentService.update(userid, QCS_ID);
                    }
                }
                model.Comments = data.Value<string>("Comments");
                if (QCS_InspectionDocumentDetailsService.update(userid, model))
                {
                    success++;
                    if (model3.Value == Framework.SystemID + "02012130000B1")//如果表头品质判定方式为自动
                    {
                        bool result = true;//默认明细所有结果都为允收
                        IList<Hashtable> DetailArray = QCS_InspectionDocumentDetailsService.GetAllDetail(model.InspectionDocumentID);//获取该表头下的所有明细
                        for (int j = 0; j < DetailArray.Count; j++)
                        {
                            QCS_InspectionDocumentDetails IDmodel = QCS_InspectionDocumentDetailsService.get(DetailArray[j]["InspectionDocumentDetailID"].ToString());
                            if (IDmodel.QualityControlDecision == Framework.SystemID + "0201213000090")
                            {
                                result = false;//当有一个明细结果为拒收时，将result改为false
                            }
                        }
                        if (result == true)//当所有的明细结果都为允收时，将表头该为允收
                        {
                            QCS_InspectionDocument IDmodel = QCS_InspectionDocumentService.get(model.InspectionDocumentID);
                            IDmodel.QualityControlDecision = Framework.SystemID + "0201213000091";
                            QCS_InspectionDocumentService.update(userid, IDmodel);
                        }
                    }
                }
                else
                {
                    failIDs = UtilBussinessService.str(failIDs, data.Value<string>("InspectionDocumentDetailID"));
                    fail++;
                }

            }
            return new { success = success, fail = fail, failIDs = failIDs, msg = msg };
        }

        /// <summary>
        /// 制程巡检--明细更新（移动端）
        /// Alvin 2017年9月14日16:22:45
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public static object Qcs00008DetailUpdateByOne(JObject request)
        {
            string Token = request.Value<string>("Token");//获取传进来的token
            string userid = UtilBussinessService.detoken(Token);//获取登录人的流水号
            string SysId = Framework.SystemID;//系统代号
            QCS_InspectionDocumentDetails model = null;
            QCS_InspectionDocument Headermodel = null;
            try
            {
                Headermodel = QCS_InspectionDocumentService.get(request.Value<string>("InspectionDocumentID"));
                if (string.IsNullOrWhiteSpace(request.Value<string>("InspectionDocumentID")))
                {
                    return new { status = "410", msg = "检验单据流水号不能为空！" };
                }
                if (Headermodel.ItemID == null)
                {
                    return new { status = "410", msg = "该检验单据的料品流水号为空！" };
                }

                //状态为CA确认,CL作废时不可修改
                if (Headermodel.Status == SysId + "020121300008E" || Headermodel.Status == SysId + "020121300008F")
                {
                    return new { status = "410", msg = "状态为CA确认或CL作废,不能修改！" };
                }
                model = new QCS_InspectionDocumentDetails();
                if (string.IsNullOrWhiteSpace(request.Value<string>("InspectionDocumentDetailID")))
                {
                    return new { status = "410", msg = "检验单据明细流水号不能为空！" };
                }
                model.InspectionDocumentDetailID = request.Value<string>("InspectionDocumentDetailID");
                model.InspectionDocumentID = request.Value<string>("InspectionDocumentID");
                model.Attribute = request.Value<string>("Attribute");
                model.QualityControlDecision = request.Value<string>("QcDecision");
                model.Comments = request.Value<string>("Comments");
                MES_Parameter model3 = new MES_Parameter();//品质判定方式
                model3.ParameterID = Framework.SystemID + "1101213000002";
                model3 = MES_ParameterService.get(model3.ParameterID);
                if (model3.Value == Framework.SystemID + "02012130000B1")//如果表头品质判定方式为自动
                {
                    if (model.QualityControlDecision == Framework.SystemID + "0201213000090")//当明细更改为拒收，那么获取表头实体并修改表头验收字段
                    {
                        QCS_InspectionDocument QCS_ID = new QCS_InspectionDocument();
                        QCS_ID = QCS_InspectionDocumentService.get(model.InspectionDocumentID);
                        QCS_ID.QualityControlDecision = Framework.SystemID + "0201213000090";//明细为拒收，表头也更新为拒收
                        QCS_InspectionDocumentService.update(userid, QCS_ID);
                    }
                }

                if (QCS_InspectionDocumentDetailsService.Qcs00008DetailUpdate(userid, model))
                {
                    return new { status = "200", msg = "修改明细成功！" };
                }
                else
                {
                    return new { status = "410", msg = "修改明细失败！" };
                }
            }
            catch (Exception ex)
            {
                DataLogerService.writeerrlog(ex);
                return new { status = "410", msg = "修改明细失败！" + ex.ToString() };
            }
        }


        /// <summary>
        /// 制程检验单明细原因码列表
        /// Joint
        /// </summary>
        /// <param name="Token"></param>
        /// <param name="InspectionDocumentID"></param>
        /// <param name="InspectionDocumentDetailID"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <returns></returns>
        public static object Qcs00008GetReasonList(string Token, string InspectionDocumentID, string InspectionDocumentDetailID, int page, int rows)
        {
            int count = 0;
            return UtilBussinessService.getPaginationModel(QCS_InspectionDocumentReasonService.Qcs00005GetReasonList(InspectionDocumentID, InspectionDocumentDetailID, page, rows, ref count), count);
        }

        /// <summary>
        /// 制程检验原因码新增
        /// Joint
        /// </summary>
        /// <param name="Token"></param>
        /// <param name="jArray"></param>
        /// <returns></returns>
        public static object Qcs00008ReasonInsert(string Token, JArray jArray)
        {
            int success = 0;
            int fail = 0;
            string failIDs = null;
            string msg = null;
            string userid = UtilBussinessService.detoken(Token);
            JObject data = null;
            QCS_InspectionDocumentReason model = new QCS_InspectionDocumentReason();
            QCS_InspectionDocumentDetails Headermodel = null;
            for (int i = 0; i < jArray.Count; i++)
            {
                data = (JObject)jArray[i];
                Headermodel = QCS_InspectionDocumentDetailsService.get(data.Value<string>("InspectionDocumentDetailID"));
                if (Headermodel == null)
                {
                    failIDs = UtilBussinessService.str(failIDs, data.Value<string>("InspectionDocumentDetailID"));
                    msg = UtilBussinessService.str(msg, data.Value<string>("Sequence") + "的明细流水号为空");
                    fail++;
                }
                model.InspectionDocumentReasonID = UniversalService.GetSerialNumber("QCS_InspectionDocumentReason");
                model.InspectionDocumentID = data.Value<string>("InspectionDocumentID");
                model.InspectionDocumentDetailID = data.Value<string>("InspectionDocumentDetailID");
                if (!string.IsNullOrWhiteSpace(data.Value<string>("Sequence")))
                    model.Sequence = data.Value<int>("Sequence");
                if (string.IsNullOrWhiteSpace(data.Value<string>("ReasonID")))
                {
                    return new { Status = "400", msg = "保存失败，原因为空！" };
                }
                model.ReasonID = data.Value<string>("ReasonID");
                model.Comments = data.Value<string>("Comments");
                model.Status = Framework.SystemID + "0201213000001";
                if (QCS_InspectionDocumentReasonService.insert(userid, model))
                    success++;
                else
                {
                    failIDs = UtilBussinessService.str(failIDs, data.Value<string>("InspectionDocumentReasonID"));
                    fail++;
                }
            }
            return new { success = success, fail = fail, failIDs = failIDs, msg = msg };
        }

        /// <summary>
        /// 制程检验原因码更新
        /// Joint
        /// </summary>
        /// <param name="Token"></param>
        /// <param name="jArray"></param>
        /// <returns></returns>
        public static object Qcs00008ReasonUpdate(string Token, JArray jArray)
        {
            int success = 0;
            int fail = 0;
            string failIDs = null;
            string msg = null;
            string userid = UtilBussinessService.detoken(Token);
            JObject data = null;
            QCS_InspectionDocumentReason model = null;
            QCS_InspectionDocumentDetails Headermodel = null;
            for (int i = 0; i < jArray.Count; i++)
            {
                data = (JObject)jArray[i];
                Headermodel = QCS_InspectionDocumentDetailsService.get(data.Value<string>("InspectionDocumentDetailID"));
                if (Headermodel == null)
                {
                    failIDs = UtilBussinessService.str(failIDs, data.Value<string>("InspectionDocumentDetailID"));
                    msg = UtilBussinessService.str(msg, data.Value<string>("Sequence") + "的明细流水号为空");
                    fail++;
                }
                model = QCS_InspectionDocumentReasonService.get(data.Value<string>("InspectionDocumentReasonID"));
                if (Headermodel == null)
                {
                    failIDs = UtilBussinessService.str(failIDs, data.Value<string>("InspectionDocumentReasonID"));
                    msg = UtilBussinessService.str(msg, data.Value<string>("Sequence") + "的原因码流水号为空");
                    fail++;
                }
                if (!string.IsNullOrWhiteSpace(data.Value<string>("Sequence")))
                    model.Sequence = data.Value<int>("Sequence");
                model.ReasonID = data.Value<string>("ReasonID");
                model.Comments = data.Value<string>("Comments");
                if (QCS_InspectionDocumentReasonService.update(userid, model))
                    success++;
                else
                {
                    failIDs = UtilBussinessService.str(failIDs, data.Value<string>("InspectionDocumentReasonID"));
                    fail++;
                }
            }
            return new { success = success, fail = fail, failIDs = failIDs, msg = msg };
        }

        /// <summary>
        /// 制程检验原因码删除
        /// Joint
        /// </summary>
        /// <param name="Token"></param>
        /// <param name="jArray"></param>
        /// <returns></returns>
        public static object Qcs00008ReasonDelete(string Token, JArray jArray)
        {
            int success = 0;
            int fail = 0;
            string failIDs = null;
            string msg = null;
            string userid = UtilBussinessService.detoken(Token);
            JObject data = null;
            QCS_InspectionDocumentReason model = null;
            QCS_InspectionDocumentDetails Headermodel = null;
            for (int i = 0; i < jArray.Count; i++)
            {
                data = (JObject)jArray[i];
                Headermodel = QCS_InspectionDocumentDetailsService.get(data.Value<string>("InspectionDocumentDetailID"));
                if (Headermodel == null)
                {
                    failIDs = UtilBussinessService.str(failIDs, data.Value<string>("InspectionDocumentDetailID"));
                    msg = UtilBussinessService.str(msg, data.Value<string>("Sequence") + "的明细流水号为空");
                    fail++;
                }
                model = QCS_InspectionDocumentReasonService.get(data.Value<string>("InspectionDocumentReasonID"));
                if (model == null)
                {
                    failIDs = UtilBussinessService.str(failIDs, data.Value<string>("InspectionDocumentReasonID"));
                    msg = UtilBussinessService.str(msg, data.Value<string>("Sequence") + "的原因码流水号为空");
                    fail++;
                }
                model.Status = Framework.SystemID + "0201213000003";
                if (QCS_InspectionDocumentReasonService.update(userid, model))
                    success++;
                else
                {
                    failIDs = UtilBussinessService.str(failIDs, data.Value<string>("InspectionDocumentReasonID"));
                    fail++;
                }
            }
            return new { success = success, fail = fail, failIDs = failIDs, msg = msg };
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
        /// <returns></returns>
        public static object Qcs00008GetRemarkList(string Token, string InspectionDocumentID, string InspectionDocumentDetailID, int page, int rows)
        {
            int count = 0;
            return UtilBussinessService.getPaginationModel(QCS_InspectionDocumentRemarkService.Qcs00005GetRemarkList(InspectionDocumentID, InspectionDocumentDetailID, page, rows, ref count), count);
        }

        /// <summary>
        /// 检验结果说明新增
        /// Joint
        /// </summary>
        /// <param name="Token"></param>
        /// <param name="jArray"></param>
        /// <returns></returns>
        public static object Qcs00008RemarkInsert(string Token, JArray jArray)
        {
            int success = 0;
            int fail = 0;
            string failIDs = null;
            string msg = null;
            string userid = UtilBussinessService.detoken(Token);
            JObject data = null;
            QCS_InspectionDocumentRemark model = new QCS_InspectionDocumentRemark();
            QCS_InspectionDocumentDetails Headermodel = null;
            for (int i = 0; i < jArray.Count; i++)
            {
                data = (JObject)jArray[i];
                Headermodel = QCS_InspectionDocumentDetailsService.get(data.Value<string>("InspectionDocumentDetailID"));
                if (Headermodel == null)
                {
                    failIDs = UtilBussinessService.str(failIDs, data.Value<string>("InspectionDocumentDetailID"));
                    msg = UtilBussinessService.str(msg, data.Value<string>("Sequence") + "的明细流水号为空");
                    fail++;
                }
                model.InspectionDocumentRemarkID = UniversalService.GetSerialNumber("QCS_InspectionDocumentRemark");
                model.InspectionDocumentDetailID = data.Value<string>("InspectionDocumentDetailID");
                model.InspectionDocumentID = data.Value<string>("InspectionDocumentID");
                if (!string.IsNullOrWhiteSpace(data.Value<string>("Sequence")))
                    model.Sequence = data.Value<int>("Sequence");
                model.Remark = data.Value<string>("Remark");
                model.Status = Framework.SystemID + "0201213000001";
                if (QCS_InspectionDocumentRemarkService.insert(userid, model))
                    success++;
                else
                {
                    failIDs = UtilBussinessService.str(failIDs, data.Value<string>("InspectionDocumentRemarkID"));
                    fail++;
                }
            }
            return new { success = success, fail = fail, failIDs = failIDs, msg = msg };
        }

        /// <summary>
        /// 检验结果说明更新
        /// Joint
        /// </summary>
        /// <param name="Token"></param>
        /// <param name="jArray"></param>
        /// <returns></returns>
        public static object Qcs00008RemarkUpdate(string Token, JArray jArray)
        {
            int success = 0;
            int fail = 0;
            string failIDs = null;
            string msg = null;
            string userid = UtilBussinessService.detoken(Token);
            JObject data = null;
            QCS_InspectionDocumentRemark model = null;
            QCS_InspectionDocumentDetails Headermodel = null;
            for (int i = 0; i < jArray.Count; i++)
            {
                data = (JObject)jArray[i];
                Headermodel = QCS_InspectionDocumentDetailsService.get(data.Value<string>("InspectionDocumentDetailID"));
                if (Headermodel == null)
                {
                    failIDs = UtilBussinessService.str(failIDs, data.Value<string>("InspectionDocumentDetailID"));
                    msg = UtilBussinessService.str(msg, data.Value<string>("Sequence") + "的明细流水号为空");
                    fail++;
                }
                model = QCS_InspectionDocumentRemarkService.get(data.Value<String>("InspectionDocumentRemarkID"));
                if (model == null)
                {
                    failIDs = UtilBussinessService.str(failIDs, data.Value<string>("InspectionDocumentRemarkID"));
                    msg = UtilBussinessService.str(msg, data.Value<string>("Sequence") + "的结果说明流水号为空");
                    fail++;
                }
                if (!string.IsNullOrWhiteSpace(data.Value<string>("Sequence")))
                    model.Sequence = data.Value<int>("Sequence");
                model.Remark = data.Value<string>("Remark");
                if (QCS_InspectionDocumentRemarkService.update(userid, model))
                    success++;
                else
                {
                    failIDs = UtilBussinessService.str(failIDs, data.Value<string>("InspectionDocumentRemarkID"));
                    fail++;
                }
            }
            return new { success = success, fail = fail, failIDs = failIDs, msg = msg };
        }

        /// <summary>
        /// 检验结果说明删除
        /// Joint
        /// </summary>
        /// <param name="Token"></param>
        /// <param name="jArray"></param>
        /// <returns></returns>
        public static object Qcs00008RemarkDelete(string Token, JArray jArray)
        {
            int success = 0;
            int fail = 0;
            string failIDs = null;
            string msg = null;
            string userid = UtilBussinessService.detoken(Token);
            JObject data = null;
            QCS_InspectionDocumentRemark model = null;
            QCS_InspectionDocumentDetails Headermodel = null;
            for (int i = 0; i < jArray.Count; i++)
            {
                data = (JObject)jArray[i];
                Headermodel = QCS_InspectionDocumentDetailsService.get(data.Value<string>("InspectionDocumentDetailID"));
                if (Headermodel == null)
                {
                    failIDs = UtilBussinessService.str(failIDs, data.Value<string>("InspectionDocumentDetailID"));
                    msg = UtilBussinessService.str(msg, data.Value<string>("Sequence") + "的表头流水号为空");
                    fail++;
                }
                model = QCS_InspectionDocumentRemarkService.get(data.Value<String>("InspectionDocumentRemarkID"));
                if (model == null)
                {
                    failIDs = UtilBussinessService.str(failIDs, data.Value<string>("InspectionDocumentRemarkID"));
                    msg = UtilBussinessService.str(msg, data.Value<string>("Sequence") + "的结果说明流水号为空");
                    fail++;
                }
                model.Status = Framework.SystemID + "0201213000003";
                if (QCS_InspectionDocumentRemarkService.update(userid, model))
                    success++;
                else
                {
                    failIDs = UtilBussinessService.str(failIDs, data.Value<string>("InspectionDocumentRemarkID"));
                    fail++;
                }
            }
            return new { success = success, fail = fail, failIDs = failIDs, msg = msg };
        }
        #endregion

        #region QCS00009客诉单维护
        /// <summary>
        /// 客诉单表头列表
        /// SAM 2017年6月14日16:58:45
        /// </summary>
        /// <param name="token"></param>
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
        public static object Qcs00009GetList(string token, string StartCode, string EndCode, string StartDate, string EndDate, string StartCustCode, string EndCustCode, string Status, string StartOrderCode, string EndOrderCode, int page, int rows)
        {
            int count = 0;
            IList<Hashtable> result = QCS_ComplaintService.Qcs00009GetList(StartCode, EndCode, StartDate, EndDate, StartCustCode, EndCustCode, Status, StartOrderCode, EndOrderCode, page, rows, ref count);
            return UtilBussinessService.getPaginationModel(result, count);
        }


        /// <summary>
        /// 客诉单的新增
        /// SAM 2017年6月15日09:24:10
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static object Qcs00009Add(JObject data)
        {
            string Token = data.Value<string>("Token");
            string userid = UtilBussinessService.detoken(Token);

            try
            {
                QCS_Complaint model = new QCS_Complaint();
                model.ComplaintID = UniversalService.GetSerialNumber("QCS_Complaint");
                model.Code = data.Value<string>("Code");
                model.Date = data.Value<DateTime>("Date");
                model.CustomerID = data.Value<string>("CustomerID");
                model.CustomerName = data.Value<string>("CustomerName");
                model.Comments = data.Value<string>("Comments");
                model.Complaintor = data.Value<string>("Complaintor");
                model.ApplicantID = data.Value<string>("ApplicantID");
                model.Status = Framework.SystemID + "0201213000028";

                //while (EMS_CalledRepairOrderService.CheckCode(model.Code, null))
                //{
                //    model.Code = UtilBussinessService.GetAutoNumber(userid, "QCS");
                //}

                if (QCS_ComplaintService.insert(userid, model))
                {
                    //UtilBussinessService.updateAutoNumber(userid, "QCS", model.Code);
                    UtilBussinessService.UpdateDocumentAutoNumber(userid, data.Value<string>("DocumentAutoNumberID"));
                    return new { status = "200", msg = "新增成功！" };
                }
                else
                    return new { status = "410", msg = "新增失败！" };
            }
            catch (Exception ex)
            {
                DataLogerService.writeerrlog(ex);
                return new { status = "410", msg = "新增失败！" + ex.ToString() };
            }
        }

        /// <summary>
        /// 客诉单单一删除
        /// SAM 2017年8月22日16:00:14
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static object Qcs00009Delete(JObject data)
        {
            string Token = data.Value<string>("Token");
            string userid = UtilBussinessService.detoken(Token);

            try
            {
                QCS_Complaint model = QCS_ComplaintService.get(data.Value<string>("ComplaintID"));
                if (model.Status != Framework.SystemID + "0201213000028")
                    return new { status = "410", msg = model.Code + "处于非立单状态,不能删除！" };

                model.Status = Framework.SystemID + "0201213000003";
                if (QCS_ComplaintService.update(userid, model))
                    return new { status = "200", msg = "删除成功！" };
                else
                    return new { status = "410", msg = "删除失败！" };
            }
            catch (Exception ex)
            {
                DataLogerService.writeerrlog(ex);
                return new { status = "410", msg = "删除失败！" + ex.ToString() };
            }
        }


        /// <summary>
        /// 客诉单的删除
        /// SAM 2017年6月15日09:27:27
        /// </summary>
        /// <param name="Token"></param>
        /// <param name="jArray"></param>
        /// <returns></returns>
        public static object Qcs00009delete(string Token, JArray jArray)
        {
            int success = 0;
            int fail = 0;
            string failIDs = null;
            string msg = null;
            string userid = UtilBussinessService.detoken(Token);
            JObject data = null;
            QCS_Complaint model = null;
            for (int i = 0; i < jArray.Count; i++)
            {
                data = (JObject)jArray[i];
                model = QCS_ComplaintService.get(data.Value<string>("ComplaintID"));

                if (model.Status == Framework.SystemID + "0201213000028")
                {
                    model.Status = Framework.SystemID + "0201213000003";
                    if (QCS_ComplaintService.update(userid, model))
                        success++;
                    else
                    {
                        failIDs = UtilBussinessService.str(failIDs, data.Value<string>("ComplaintID"));
                        fail++;
                    }
                }
                else
                {
                    failIDs = UtilBussinessService.str(failIDs, data.Value<string>("ComplaintID"));
                    msg = UtilBussinessService.str(msg, model.Code + "处于非立单状态,不能删除");
                    fail++;
                }


            }
            return new { success = success, fail = fail, failIDs = failIDs, msg = msg };
        }

        /// <summary>
        /// 更新客诉单
        /// SAM 2017年6月15日09:28:12
        /// </summary>
        /// <param name="Token"></param>
        /// <param name="jArray"></param>
        /// <returns></returns>
        public static object Qcs00009update(string Token, JArray jArray)
        {
            int success = 0;
            int fail = 0;
            string failIDs = null;
            string msg = null;
            string SysID = Framework.SystemID;
            string userid = UtilBussinessService.detoken(Token);
            JObject data = null;
            QCS_Complaint model = null;
            for (int i = 0; i < jArray.Count; i++)
            {
                data = (JObject)jArray[i];
                model = QCS_ComplaintService.get(data.Value<string>("ComplaintID"));
                if (model.Status == SysID + "0201213000029" || model.Status == SysID + "020121300002A" || model.Status == SysID + "020121300002B")//狀態為核发OP,註銷CA、結案CL時，全部不可修改
                {
                    failIDs = UtilBussinessService.str(failIDs, data.Value<string>("ComplaintID"));
                    msg = UtilBussinessService.str(msg, model.Code + "的状态为非立单状态,不能修改");
                    fail++;
                    continue; //跳过循环
                }

                if (data.Value<string>("Status") == SysID + "020121300002A")
                {
                    failIDs = UtilBussinessService.str(failIDs, data.Value<string>("ComplaintID"));
                    msg = UtilBussinessService.str(msg, model.Code + "不能修改为结案状态");
                    fail++;
                    continue; //跳过循环
                }
                model.CustomerID = data.Value<string>("CustomerID");
                model.CustomerName = data.Value<string>("CustomerName");
                model.Comments = data.Value<string>("Comments");
                model.Complaintor = data.Value<string>("Complaintor");
                model.ApplicantID = data.Value<string>("ApplicantID");
                model.Status = data.Value<string>("Status");
                if (QCS_ComplaintService.update(userid, model))
                {
                    //同步更新明细
                    QCS_ComplaintDetailsService.updateStatus(data.Value<string>("ComplaintID"), data.Value<string>("Status"));
                    success++;
                }
                else
                {
                    failIDs = UtilBussinessService.str(failIDs, data.Value<string>("ComplaintID"));
                    fail++;
                }
            }
            return new { success = success, fail = fail, failIDs = failIDs, msg = msg };
        }

        /// <summary>
        /// 客诉单的明细列表
        /// SAM 2017年6月14日17:51:22
        /// </summary>
        /// <param name="token"></param>
        /// <param name="ComplaintID"></param>
        /// <param name="StartOrderCode"></param>
        /// <param name="EndOrderCode"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <returns></returns>
        public static object Qcs00009GetDetailsList(string token, string ComplaintID, string StartOrderCode, string EndOrderCode, int page, int rows)
        {
            int count = 0;
            IList<Hashtable> result = QCS_ComplaintDetailsService.Qcs00009GetDetailsList(ComplaintID, StartOrderCode, EndOrderCode, page, rows, ref count);
            return UtilBussinessService.getPaginationModel(result, count);
        }

        /// <summary>
        /// 客诉单明细的新增
        /// SAM 2017年6月15日09:38:37
        /// </summary>
        /// <param name="Token"></param>
        /// <param name="jArray"></param>
        /// <returns></returns>
        public static object Qcs00009Detailinsert(string Token, JArray jArray)
        {
            int success = 0;
            int fail = 0;
            string failIDs = null;
            string msg = null;
            string userid = UtilBussinessService.detoken(Token);
            JObject data = null;
            QCS_ComplaintDetails model = null;
            QCS_Complaint Headermodel = null;
            decimal Quantity = 0;
            for (int i = 0; i < jArray.Count; i++)
            {

                data = (JObject)jArray[i];
                Headermodel = QCS_ComplaintService.get(data.Value<string>("ComplaintID"));
                if (Headermodel == null)
                {
                    failIDs = UtilBussinessService.str(failIDs, data.Value<string>("ComplaintDetailID"));
                    msg = UtilBussinessService.str(msg, data.Value<string>("Sequence") + "的表头流水号为空");
                    fail++;
                }
                else if (Headermodel.Status != Framework.SystemID + "0201213000028")
                {
                    failIDs = UtilBussinessService.str(failIDs, data.Value<string>("ComplaintDetailID"));
                    msg = UtilBussinessService.str(msg, data.Value<string>("Sequence") + "客诉单为非立单状态，不能新增数据！");
                    fail++;
                }

                try
                {
                    Quantity = data.Value<decimal>("Quantity");
                }
                catch
                {
                    failIDs = UtilBussinessService.str(failIDs, data.Value<string>("ComplaintDetailID"));
                    msg = UtilBussinessService.str(msg, data.Value<string>("Sequence") + "的客訴數量格式错误");
                    fail++;
                }
                model = new QCS_ComplaintDetails();
                model.ComplaintDetailID = UniversalService.GetSerialNumber("QCS_ComplaintDetails");
                model.ComplaintID = data.Value<string>("ComplaintID");
                model.Sequence = string.IsNullOrWhiteSpace(data.Value<string>("Sequence")) ? 0 : data.Value<int>("Sequence");
                model.ItemID = data.Value<string>("ItemID");
                model.BatchNumber = data.Value<string>("BatchNumber");
                model.ShipperNo = data.Value<string>("ShipperNo");
                model.OrderNo = data.Value<string>("OrderNo");
                model.Quantity = Quantity;
                model.Description = data.Value<string>("Description");
                model.Status = Framework.SystemID + "0201213000028";
                model.Comments = data.Value<string>("Comments");

                if (QCS_ComplaintDetailsService.insert(userid, model))
                    success++;
                else
                {
                    failIDs = UtilBussinessService.str(failIDs, data.Value<string>("ComplaintDetailID"));
                    fail++;
                }
            }
            return new { success = success, fail = fail, failIDs = failIDs, msg = msg };
        }

        /// <summary>
        /// 客诉单明细单一删除
        /// SAM 2017年8月22日16:00:32
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static object Qcs00009DetailDelete(JObject data)
        {
            string Token = data.Value<string>("Token");
            string userid = UtilBussinessService.detoken(Token);

            try
            {
                QCS_ComplaintDetails model = QCS_ComplaintDetailsService.get(data.Value<string>("ComplaintDetailID"));
                if (model == null)
                    return new { status = "410", msg = "不存在的明细信息！" };

                QCS_Complaint Headermodel = QCS_ComplaintService.get(model.ComplaintID);
                if (Headermodel.Status != Framework.SystemID + "0201213000028")
                    return new { status = "410", msg = "客诉单为非立单状态，不能删除数据！" };

                model.Status = Framework.SystemID + "0201213000003";
                if (QCS_ComplaintDetailsService.update(userid, model))
                    return new { status = "200", msg = "删除成功！" };
                else
                    return new { status = "410", msg = "删除失败！" };
            }
            catch (Exception ex)
            {
                DataLogerService.writeerrlog(ex);
                return new { status = "410", msg = "删除失败！" + ex.ToString() };
            }
        }


        /// <summary>
        /// 客诉单明细的删除
        /// SAM 2017年6月15日09:43:46
        /// </summary>
        /// <param name="Token"></param>
        /// <param name="jArray"></param>
        /// <returns></returns>
        public static object Qcs00009Detaildelete(string Token, JArray jArray)
        {
            int success = 0;
            int fail = 0;
            string failIDs = null;
            string msg = null;
            string userid = UtilBussinessService.detoken(Token);
            JObject data = null;
            QCS_ComplaintDetails model = null;
            QCS_Complaint Headermodel = null;
            for (int i = 0; i < jArray.Count; i++)
            {
                data = (JObject)jArray[i];
                Headermodel = QCS_ComplaintService.get(data.Value<string>("ComplaintID"));
                if (Headermodel == null)
                {
                    failIDs = UtilBussinessService.str(failIDs, data.Value<string>("ComplaintDetailID"));
                    msg = UtilBussinessService.str(msg, data.Value<string>("Sequence") + "的表头流水号为空");
                    fail++;
                }
                else if (Headermodel.Status != Framework.SystemID + "0201213000028")
                {
                    failIDs = UtilBussinessService.str(failIDs, data.Value<string>("ComplaintDetailID"));
                    msg = UtilBussinessService.str(msg, data.Value<string>("Sequence") + "客诉单为非立单状态，不能删除数据！");
                    fail++;
                }

                model = QCS_ComplaintDetailsService.get(data.Value<string>("ComplaintDetailID"));
                model.Status = Framework.SystemID + "0201213000003";
                if (QCS_ComplaintDetailsService.update(userid, model))
                    success++;
                else
                {
                    failIDs = UtilBussinessService.str(failIDs, data.Value<string>("ComplaintDetailID"));
                    fail++;
                }
            }
            return new { success = success, fail = fail, failIDs = failIDs, msg = msg };
        }

        /// <summary>
        /// 客诉单明细的更新
        /// SAM 2017年6月15日09:48:55
        /// </summary>
        /// <param name="Token"></param>
        /// <param name="jArray"></param>
        /// <returns></returns>
        public static object Qcs00009Detailupdate(string Token, JArray jArray)
        {
            int success = 0;
            int fail = 0;
            string failIDs = null;
            string msg = null;
            string userid = UtilBussinessService.detoken(Token);
            JObject data = null;
            QCS_ComplaintDetails model = null;
            QCS_Complaint Headermodel = null;
            decimal Quantity = 0;
            for (int i = 0; i < jArray.Count; i++)
            {
                data = (JObject)jArray[i];
                Headermodel = QCS_ComplaintService.get(data.Value<string>("ComplaintID"));
                if (Headermodel == null)
                {
                    failIDs = UtilBussinessService.str(failIDs, data.Value<string>("ComplaintDetailID"));
                    msg = UtilBussinessService.str(msg, data.Value<string>("Sequence") + "的表头流水号为空");
                    fail++;
                }
                else if (Headermodel.Status != Framework.SystemID + "0201213000028")
                {
                    failIDs = UtilBussinessService.str(failIDs, data.Value<string>("ComplaintDetailID"));
                    msg = UtilBussinessService.str(msg, data.Value<string>("Sequence") + "客诉单为非立单状态，不能新增数据！");
                    fail++;
                }

                try
                {
                    Quantity = data.Value<decimal>("Quantity");
                }
                catch
                {
                    failIDs = UtilBussinessService.str(failIDs, data.Value<string>("ComplaintDetailID"));
                    msg = UtilBussinessService.str(msg, data.Value<string>("Sequence") + "的客訴數量格式错误");
                    fail++;
                }

                model = QCS_ComplaintDetailsService.get(data.Value<string>("ComplaintDetailID"));
                model.ItemID = data.Value<string>("ItemID");
                model.BatchNumber = data.Value<string>("BatchNumber");
                model.ShipperNo = data.Value<string>("ShipperNo");
                model.OrderNo = data.Value<string>("OrderNo");
                model.Quantity = Quantity;
                model.Description = data.Value<string>("Description");
                model.Comments = data.Value<string>("Comments");
                if (QCS_ComplaintDetailsService.update(userid, model))
                    success++;
                else
                {
                    failIDs = UtilBussinessService.str(failIDs, data.Value<string>("ComplaintDetailID"));
                    fail++;
                }

            }
            return new { success = success, fail = fail, failIDs = failIDs, msg = msg };
        }

        /// <summary>
        /// 获取附件列表
        /// SAM 2017年6月15日16:24:43
        /// </summary>
        /// <param name="token"></param>
        /// <param name="ComplaintDetailID"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <returns></returns>
        public static object Qcs00009GetAttachmentList(string token, string ComplaintDetailID, int page, int rows)
        {
            int count = 0;
            IList<Hashtable> result = SYS_AttachmentsService.Qcs00009GetAttachmentList(ComplaintDetailID, page, rows, ref count);
            return UtilBussinessService.getPaginationModel(result, count);
        }



        /// <summary>
        /// 附件的删除
        /// SAM 2017年6月21日17:30:52
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static object Qcs00009FileDelete(JObject data)
        {
            string Token = data.Value<string>("Token");
            string userid = UtilBussinessService.detoken(Token);

            try
            {
                SYS_Attachments model = SYS_AttachmentsService.get(data.Value<string>("AttachmentID"));
                if (model == null)
                    return new { status = "410", msg = "删除失败，流水号错误！" };

                model.Status = Framework.SystemID + "0201213000003";

                if (SYS_AttachmentsService.update(userid, model))
                    return new { status = "200", msg = "删除成功！" };
                else
                    return new { status = "410", msg = "删除失败！" };
            }
            catch (Exception ex)
            {
                DataLogerService.writeerrlog(ex);
                return new { status = "410", msg = "删除失败！" + ex.ToString() };
            }
        }



        #endregion

        #region QCS000010客诉分析与改善
        /// <summary>
        /// 客诉分析与改善列表
        /// SAM 2017年6月15日11:11:48
        /// </summary>
        /// <param name="token"></param>
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
        public static object Qcs00010GetList(string token, string StartCode, string EndCode, string StartDate, string EndDate, string StartCustCode, string EndCustCode, string StartOrderCode, string EndOrderCode, string Status, int page, int rows)
        {
            int count = 0;
            IList<Hashtable> result = QCS_ComplaintService.Qcs00010GetList(StartCode, EndCode, StartDate, EndDate, StartCustCode, EndCustCode, StartOrderCode, EndOrderCode, Status, page, rows, ref count);
            return UtilBussinessService.getPaginationModel(result, count);
        }

        /// <summary>
        /// 获取客诉原因列表
        /// SAM 2017年6月15日11:36:44
        /// </summary>
        /// <param name="token"></param>
        /// <param name="ComplaintDetailID"></param>
        /// <param name="GroupCode"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <returns></returns>
        public static object Qcs00010GetReasonList(string token, string ComplaintDetailID, string GroupCode, int page, int rows)
        {
            int count = 0;
            IList<Hashtable> result = QCS_ComplaintReasonService.Qcs00010GetReasonList(ComplaintDetailID, GroupCode, page, rows, ref count);
            return UtilBussinessService.getPaginationModel(result, count);
        }


        /// <summary>
        /// 客诉原因新增
        /// SAM 2017年6月15日12:01:53
        /// </summary>
        /// <param name="Token"></param>
        /// <param name="jArray"></param>
        /// <returns></returns>
        public static object Qcs00010Reasoninsert(string Token, JArray jArray)
        {
            int success = 0;
            int fail = 0;
            string failIDs = null;
            string msg = null;
            string userid = UtilBussinessService.detoken(Token);
            JObject data = null;
            QCS_ComplaintReason model = null;
            QCS_ComplaintDetails Detailmodel = null;
            decimal Quantity = 0;
            for (int i = 0; i < jArray.Count; i++)
            {
                data = (JObject)jArray[i];
                Detailmodel = QCS_ComplaintDetailsService.get(data.Value<string>("ComplaintDetailID"));
                if (Detailmodel == null)
                {
                    failIDs = UtilBussinessService.str(failIDs, data.Value<string>("ComplaintReasonID"));
                    msg = UtilBussinessService.str(msg, data.Value<string>("Sequence") + "的客诉单明细流水号为空");
                    fail++;
                }
                else if (Detailmodel.Status != Framework.SystemID + "0201213000029")
                {
                    failIDs = UtilBussinessService.str(failIDs, data.Value<string>("ComplaintReasonID"));
                    msg = UtilBussinessService.str(msg, data.Value<string>("Sequence") + "客诉单为非核准状态，不能新增客诉原因！");
                    fail++;
                }

                try
                {
                    Quantity = data.Value<decimal>("Quantity");
                }
                catch
                {
                    failIDs = UtilBussinessService.str(failIDs, data.Value<string>("ComplaintReasonID"));
                    msg = UtilBussinessService.str(msg, data.Value<string>("Sequence") + "的客訴數量格式错误");
                    fail++;
                }

                if (string.IsNullOrWhiteSpace(data.Value<string>("ReasonID")))
                {
                    failIDs = UtilBussinessService.str(failIDs, data.Value<string>("ComplaintReasonID"));
                    msg = UtilBussinessService.str(msg, data.Value<string>("Sequence") + "原因码为空");
                    fail++;
                }

                model = new QCS_ComplaintReason();
                model.ComplaintReasonID = UniversalService.GetSerialNumber("QCS_ComplaintReason");
                model.ComplaintID = Detailmodel.ComplaintID;
                model.ComplaintDetailID = data.Value<string>("ComplaintDetailID");
                model.Sequence = string.IsNullOrWhiteSpace(data.Value<string>("Sequence")) ? 0 : data.Value<int>("Sequence");
                model.ReasonGroupID = data.Value<string>("ReasonGroupID");
                model.ReasonID = data.Value<string>("ReasonID");
                model.Quantity = Quantity;
                model.Status = Framework.SystemID + "0201213000001";
                model.Comments = data.Value<string>("Comments");
                if (QCS_ComplaintReasonService.insert(userid, model))
                    success++;
                else
                {
                    failIDs = UtilBussinessService.str(failIDs, data.Value<string>("ComplaintReasonID"));
                    fail++;
                }
            }
            return new { success = success, fail = fail, failIDs = failIDs, msg = msg };
        }

        /// <summary>
        /// 客诉原因删除
        /// SAM 2017年6月15日12:02:12
        /// </summary>
        /// <param name="Token"></param>
        /// <param name="jArray"></param>
        /// <returns></returns>
        public static object Qcs00010Reasondelete(string Token, JArray jArray)
        {
            int success = 0;
            int fail = 0;
            string failIDs = null;
            string msg = null;
            string userid = UtilBussinessService.detoken(Token);
            JObject data = null;
            QCS_ComplaintReason model = null;
            QCS_ComplaintDetails Detailmodel = null;
            for (int i = 0; i < jArray.Count; i++)
            {
                data = (JObject)jArray[i];
                Detailmodel = QCS_ComplaintDetailsService.get(data.Value<string>("ComplaintDetailID"));
                if (Detailmodel == null)
                {
                    failIDs = UtilBussinessService.str(failIDs, data.Value<string>("ComplaintReasonID"));
                    msg = UtilBussinessService.str(msg, data.Value<string>("Sequence") + "的客诉单明细流水号为空");
                    fail++;
                }
                else if (Detailmodel.Status != Framework.SystemID + "0201213000029")
                {
                    failIDs = UtilBussinessService.str(failIDs, data.Value<string>("ComplaintReasonID"));
                    msg = UtilBussinessService.str(msg, data.Value<string>("Sequence") + "客诉单为非核准状态，不能删除客诉原因！");
                    fail++;
                }

                model = QCS_ComplaintReasonService.get(data.Value<string>("ComplaintReasonID"));
                model.Status = Framework.SystemID + "0201213000003";
                if (QCS_ComplaintReasonService.update(userid, model))
                    success++;
                else
                {
                    failIDs = UtilBussinessService.str(failIDs, data.Value<string>("ComplaintReasonID"));
                    fail++;
                }
            }
            return new { success = success, fail = fail, failIDs = failIDs, msg = msg };
        }

        /// <summary>
        /// 客诉原因更新
        /// SAM 2017年6月15日12:02:18
        /// </summary>
        /// <param name="Token"></param>
        /// <param name="jArray"></param>
        /// <returns></returns>
        public static object Qcs00010Reasonupdate(string Token, JArray jArray)
        {
            int success = 0;
            int fail = 0;
            string failIDs = null;
            string msg = null;
            string userid = UtilBussinessService.detoken(Token);
            JObject data = null;
            QCS_ComplaintReason model = null;
            QCS_ComplaintDetails Detailmodel = null;
            decimal Quantity = 0;
            for (int i = 0; i < jArray.Count; i++)
            {
                data = (JObject)jArray[i];
                Detailmodel = QCS_ComplaintDetailsService.get(data.Value<string>("ComplaintDetailID"));
                if (Detailmodel == null)
                {
                    failIDs = UtilBussinessService.str(failIDs, data.Value<string>("ComplaintReasonID"));
                    msg = UtilBussinessService.str(msg, data.Value<string>("Sequence") + "的客诉单明细流水号为空");
                    fail++;
                }
                else if (Detailmodel.Status != Framework.SystemID + "0201213000029")
                {
                    failIDs = UtilBussinessService.str(failIDs, data.Value<string>("ComplaintReasonID"));
                    msg = UtilBussinessService.str(msg, data.Value<string>("Sequence") + "客诉单为非核准状态，不能新增客诉原因！");
                    fail++;
                }

                try
                {
                    Quantity = data.Value<decimal>("Quantity");
                }
                catch
                {
                    failIDs = UtilBussinessService.str(failIDs, data.Value<string>("ComplaintReasonID"));
                    msg = UtilBussinessService.str(msg, data.Value<string>("Sequence") + "的客訴數量格式错误");
                    fail++;
                }

                if (string.IsNullOrWhiteSpace(data.Value<string>("ReasonID")))
                {
                    failIDs = UtilBussinessService.str(failIDs, data.Value<string>("ComplaintReasonID"));
                    msg = UtilBussinessService.str(msg, data.Value<string>("Sequence") + "原因码为空");
                    fail++;
                }

                model = QCS_ComplaintReasonService.get(data.Value<string>("ComplaintReasonID"));
                model.ReasonGroupID = data.Value<string>("ReasonGroupID");
                model.ReasonID = data.Value<string>("ReasonID");
                model.Quantity = Quantity;
                model.Comments = data.Value<string>("Comments");
                if (QCS_ComplaintReasonService.update(userid, model))
                    success++;
                else
                {
                    failIDs = UtilBussinessService.str(failIDs, data.Value<string>("ComplaintReasonID"));
                    fail++;
                }

            }
            return new { success = success, fail = fail, failIDs = failIDs, msg = msg };
        }

        /// <summary>
        /// 客訴單处理对策列表
        /// SAM 2017年6月15日14:13:52
        /// </summary>
        /// <param name="token"></param>
        /// <param name="ComplaintDetailID"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <returns></returns>
        public static object Qcs00010GetHandleList(string token, string ComplaintDetailID, int page, int rows)
        {
            int count = 0;
            IList<Hashtable> result = QCS_ComplaintHandleService.Qcs00010GetHandleList(ComplaintDetailID, page, rows, ref count);
            return UtilBussinessService.getPaginationModel(result, count);
        }

        /// <summary>
        /// 客訴單处理对策新增
        /// SAM 2017年6月27日13:57:55
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static object Qcs000010HandleAdd(JObject data)
        {
            string Token = data.Value<string>("Token");
            string userid = UtilBussinessService.detoken(Token);

            QCS_ComplaintHandle model = null;
            QCS_ComplaintDetails Detailmodel = null;

            try
            {
                Detailmodel = QCS_ComplaintDetailsService.get(data.Value<string>("ComplaintDetailID"));
                if (Detailmodel == null)
                    return new { status = "410", msg = "客诉明细流水号不存在！" };
                else if (Detailmodel.Status != Framework.SystemID + "0201213000029")
                    return new { status = "410", msg = "客诉单为非核准状态，不能新增处理对策！" };

                model = new QCS_ComplaintHandle();
                model.ComplaintHandleID = UniversalService.GetSerialNumber("QCS_ComplaintHandle");
                model.ComplaintID = Detailmodel.ComplaintID;
                model.ComplaintDetailID = data.Value<string>("ComplaintDetailID");
                model.Sequence = string.IsNullOrWhiteSpace(data.Value<string>("Sequence")) ? 0 : data.Value<int>("Sequence");
                model.Method = data.Value<string>("Method");
                model.Status = Framework.SystemID + "0201213000001";
                model.Comments = data.Value<string>("Comments");

                if (QCS_ComplaintHandleService.insert(userid, model))
                    return new { status = "200", msg = "新增成功！" };
                else
                    return new { status = "410", msg = "新增失败！" };
            }
            catch (Exception ex)
            {
                DataLogerService.writeerrlog(ex);
                return new { status = "410", msg = "新增失败！" + ex.ToString() };
            }
        }

        /// <summary>
        /// 更新客訴單处理对策
        /// SAM 2017年6月27日14:02:28
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static object Qcs000010HandleUpdate(JObject data)
        {
            string Token = data.Value<string>("Token");
            string userid = UtilBussinessService.detoken(Token);

            QCS_ComplaintHandle model = null;
            QCS_ComplaintDetails Detailmodel = null;

            try
            {
                Detailmodel = QCS_ComplaintDetailsService.get(data.Value<string>("ComplaintDetailID"));
                if (Detailmodel == null)
                    return new { status = "410", msg = "客诉明细流水号不存在！" };
                else if (Detailmodel.Status != Framework.SystemID + "0201213000029")
                    return new { status = "410", msg = "客诉单为非核准状态，不能更新处理对策！" };

                model = QCS_ComplaintHandleService.get(data.Value<string>("ComplaintHandleID"));
                model.Method = data.Value<string>("Method");
                if (QCS_ComplaintHandleService.update(userid, model))
                    return new { status = "200", msg = "修改成功！" };
                else
                    return new { status = "410", msg = "修改失败！" };
            }
            catch (Exception ex)
            {
                DataLogerService.writeerrlog(ex);
                return new { status = "410", msg = "修改失败！" + ex.ToString() };
            }
        }


        /// <summary>
        /// 处理对策新增
        /// SAM 2017年6月15日14:18:46
        /// </summary>
        /// <param name="Token"></param>
        /// <param name="jArray"></param>
        /// <returns></returns>
        public static object Qcs00010Handleinsert(string Token, JArray jArray)
        {
            int success = 0;
            int fail = 0;
            string failIDs = null;
            string msg = null;
            string userid = UtilBussinessService.detoken(Token);
            JObject data = null;
            QCS_ComplaintHandle model = null;
            QCS_ComplaintDetails Detailmodel = null;
            for (int i = 0; i < jArray.Count; i++)
            {
                data = (JObject)jArray[i];
                Detailmodel = QCS_ComplaintDetailsService.get(data.Value<string>("ComplaintDetailID"));
                if (Detailmodel == null)
                {
                    failIDs = UtilBussinessService.str(failIDs, data.Value<string>("ComplaintHandleID"));
                    msg = UtilBussinessService.str(msg, data.Value<string>("Sequence") + "的客诉单明细流水号为空");
                    fail++;
                }
                else if (Detailmodel.Status != Framework.SystemID + "0201213000029")
                {
                    failIDs = UtilBussinessService.str(failIDs, data.Value<string>("ComplaintHandleID"));
                    msg = UtilBussinessService.str(msg, data.Value<string>("Sequence") + "客诉单为非核准状态，不能新增处理对策！");
                    fail++;
                }

                model = new QCS_ComplaintHandle();
                model.ComplaintHandleID = UniversalService.GetSerialNumber("QCS_ComplaintHandle");
                model.ComplaintID = Detailmodel.ComplaintID;
                model.ComplaintDetailID = data.Value<string>("ComplaintDetailID");
                model.Sequence = string.IsNullOrWhiteSpace(data.Value<string>("Sequence")) ? 0 : data.Value<int>("Sequence");
                model.Method = data.Value<string>("Method");
                model.Status = Framework.SystemID + "0201213000001";
                model.Comments = data.Value<string>("Comments");
                if (QCS_ComplaintHandleService.insert(userid, model))
                    success++;
                else
                {
                    failIDs = UtilBussinessService.str(failIDs, data.Value<string>("ComplaintHandleID"));
                    fail++;
                }
            }
            return new { success = success, fail = fail, failIDs = failIDs, msg = msg };
        }

        /// <summary>
        /// 处理对策删除
        /// SAM 2017年6月15日14:18:53
        /// </summary>
        /// <param name="Token"></param>
        /// <param name="jArray"></param>
        /// <returns></returns>
        public static object Qcs00010Handledelete(string Token, JArray jArray)
        {
            int success = 0;
            int fail = 0;
            string failIDs = null;
            string msg = null;
            string userid = UtilBussinessService.detoken(Token);
            JObject data = null;
            QCS_ComplaintHandle model = null;
            QCS_ComplaintDetails Detailmodel = null;
            for (int i = 0; i < jArray.Count; i++)
            {
                data = (JObject)jArray[i];
                Detailmodel = QCS_ComplaintDetailsService.get(data.Value<string>("ComplaintDetailID"));
                if (Detailmodel == null)
                {
                    failIDs = UtilBussinessService.str(failIDs, data.Value<string>("ComplaintHandleID"));
                    msg = UtilBussinessService.str(msg, data.Value<string>("Sequence") + "的客诉单明细流水号为空");
                    fail++;
                }
                else if (Detailmodel.Status != Framework.SystemID + "0201213000029")
                {
                    failIDs = UtilBussinessService.str(failIDs, data.Value<string>("ComplaintHandleID"));
                    msg = UtilBussinessService.str(msg, data.Value<string>("Sequence") + "客诉单为非核准状态，不能删除处理对策！");
                    fail++;
                }

                model = QCS_ComplaintHandleService.get(data.Value<string>("ComplaintHandleID"));
                model.Status = Framework.SystemID + "0201213000003";
                if (QCS_ComplaintHandleService.update(userid, model))
                    success++;
                else
                {
                    failIDs = UtilBussinessService.str(failIDs, data.Value<string>("ComplaintHandleID"));
                    fail++;
                }
            }
            return new { success = success, fail = fail, failIDs = failIDs, msg = msg };
        }

        /// <summary>
        /// 处理对策更新
        /// SAM 2017年6月15日14:19:12
        /// </summary>
        /// <param name="Token"></param>
        /// <param name="jArray"></param>
        /// <returns></returns>
        public static object Qcs00010Handleupdate(string Token, JArray jArray)
        {
            int success = 0;
            int fail = 0;
            string failIDs = null;
            string msg = null;
            string userid = UtilBussinessService.detoken(Token);
            JObject data = null;
            QCS_ComplaintHandle model = null;
            QCS_ComplaintDetails Detailmodel = null;
            for (int i = 0; i < jArray.Count; i++)
            {
                data = (JObject)jArray[i];
                Detailmodel = QCS_ComplaintDetailsService.get(data.Value<string>("ComplaintDetailID"));
                if (Detailmodel == null)
                {
                    failIDs = UtilBussinessService.str(failIDs, data.Value<string>("ComplaintHandleID"));
                    msg = UtilBussinessService.str(msg, data.Value<string>("Sequence") + "的客诉单明细流水号为空");
                    fail++;
                }
                else if (Detailmodel.Status != Framework.SystemID + "0201213000029")
                {
                    failIDs = UtilBussinessService.str(failIDs, data.Value<string>("ComplaintHandleID"));
                    msg = UtilBussinessService.str(msg, data.Value<string>("Sequence") + "客诉单为非核准状态，不能更新处理对策！");
                    fail++;
                }

                model = QCS_ComplaintHandleService.get(data.Value<string>("ComplaintHandleID"));
                model.Method = data.Value<string>("Method");
                if (QCS_ComplaintHandleService.update(userid, model))
                    success++;
                else
                {
                    failIDs = UtilBussinessService.str(failIDs, data.Value<string>("ComplaintHandleID"));
                    fail++;
                }

            }
            return new { success = success, fail = fail, failIDs = failIDs, msg = msg };
        }

        /// <summary>
        /// 处理对策获取附件列表
        /// SAM 2017年6月22日14:18:52
        /// </summary>
        /// <param name="token"></param>
        /// <param name="ComplaintDetailID"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <returns></returns>
        public static object Qcs00010GetAttachmentList(string token, string ComplaintDetailID, int page, int rows)
        {
            int count = 0;
            IList<Hashtable> result = SYS_AttachmentsService.Qcs00010GetAttachmentList(ComplaintDetailID, page, rows, ref count);
            return UtilBussinessService.getPaginationModel(result, count);
        }


        /// <summary>
        /// 处理对策附件的删除
        /// SAM 2017年6月21日17:30:52
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static object Qcs00010FileDelete(JObject data)
        {
            string Token = data.Value<string>("Token");
            string userid = UtilBussinessService.detoken(Token);

            try
            {
                SYS_Attachments model = SYS_AttachmentsService.get(data.Value<string>("AttachmentID"));
                if (model == null)
                    return new { status = "410", msg = "删除失败，流水号错误！" };

                model.Status = Framework.SystemID + "0201213000003";

                if (SYS_AttachmentsService.update(userid, model))
                    return new { status = "200", msg = "删除成功！" };
                else
                    return new { status = "410", msg = "删除失败！" };
            }
            catch (Exception ex)
            {
                DataLogerService.writeerrlog(ex);
                return new { status = "410", msg = "删除失败！" + ex.ToString() };
            }
        }



        #endregion

        #region QCS000011客诉单状态变更
        /// <summary>
        /// 客诉单状态变更列表
        /// SAM 2017年6月15日15:40:23
        /// </summary>
        /// <param name="token"></param>
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
        public static object Qcs00011GetList(string token, string StartCode, string EndCode, string StartDate, string EndDate, string StartCustCode, string EndCustCode, string StartOrderCode, string EndOrderCode, string Status, int page, int rows)
        {
            int count = 0;
            IList<Hashtable> result = QCS_ComplaintService.Qcs00011GetList(StartCode, EndCode, StartDate, EndDate, StartCustCode, EndCustCode, StartOrderCode, EndOrderCode, Status, page, rows, ref count);
            return UtilBussinessService.getPaginationModel(result, count);
        }

        /// <summary>
        /// 结案
        /// SAM 2017年6月15日16:00:16
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public static object Qcs00011CL(JObject data)
        {
            string Token = data.Value<string>("Token");
            string userid = UtilBussinessService.detoken(Token);

            QCS_ComplaintDetails model = null;
            model = QCS_ComplaintDetailsService.get(data.Value<string>("ComplaintDetailID"));
            if (model == null)
                return new { status = "410", msg = "流水号错误，不存在的流水号！" };

            if (model.Status != Framework.SystemID + "0201213000029")
                return new { status = "410", msg = "记录处于非核发状态，不能结案！" };

            model.Status = Framework.SystemID + "020121300002A";

            if (QCS_ComplaintDetailsService.update(userid, model))
            {
                if (!QCS_ComplaintDetailsService.CheckStatus(model.ComplaintID, model.Status))
                    QCS_ComplaintService.updateStatus(model.ComplaintID, model.Status);
                return new { status = "200", msg = "结案成功！" };
            }
            else
                return new { status = "410", msg = "结案失败！" };
        }

        /// <summary>
        /// 还原
        /// SAM 2017年6月15日16:00:26
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public static object Qcs00011OP(JObject data)
        {
            string Token = data.Value<string>("Token");
            string userid = UtilBussinessService.detoken(Token);

            QCS_ComplaintDetails model = null;
            model = QCS_ComplaintDetailsService.get(data.Value<string>("ComplaintDetailID"));
            if (model == null)
                return new { status = "410", msg = "流水号错误，不存在的流水号！" };

            if (model.Status != Framework.SystemID + "020121300002A")
                return new { status = "410", msg = "记录处于非结案状态，不能还原！" };

            model.Status = Framework.SystemID + "0201213000029";

            if (QCS_ComplaintDetailsService.update(userid, model))
            {
                QCS_ComplaintService.updateStatus(model.ComplaintID, model.Status);
                return new { status = "200", msg = "还原成功！" };
            }
            else
                return new { status = "410", msg = "还原失败！" };
        }

        /// <summary>
        /// 注销
        /// SAM 2017年6月15日16:00:34
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public static object Qcs00011CA(JObject data)
        {
            string Token = data.Value<string>("Token");
            string userid = UtilBussinessService.detoken(Token);

            QCS_ComplaintDetails model = null;
            model = QCS_ComplaintDetailsService.get(data.Value<string>("ComplaintDetailID"));
            if (model == null)
                return new { status = "410", msg = "流水号错误，不存在的流水号！" };

            if (model.Status != Framework.SystemID + "0201213000029" && model.Status != Framework.SystemID + "0201213000028")
                return new { status = "410", msg = "记录处于非立单或核发状态，不能注销！" };

            model.Status = Framework.SystemID + "020121300002B";

            if (QCS_ComplaintDetailsService.update(userid, model))
            {
                if (!QCS_ComplaintDetailsService.CheckStatus(model.ComplaintID, model.Status))
                    QCS_ComplaintService.updateStatus(model.ComplaintID, model.Status);
                return new { status = "200", msg = "注销成功！" };
            }
            else
                return new { status = "410", msg = "注销失败！" };
        }
        #endregion
    }
}
