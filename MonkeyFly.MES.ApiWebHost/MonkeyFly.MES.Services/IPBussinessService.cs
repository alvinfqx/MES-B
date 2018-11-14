using MonkeyFly.Core;
using MonkeyFly.MES.BasicService;
using MonkeyFly.MES.Models;
using MonkeyFly.MES.ModelServices;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MonkeyFly.MES.Services
{
    /// <summary>
    /// 智能参数控制器的业务逻辑层
    /// SAM 2017年4月26日10:02:46
    /// </summary>
    public class IPBussinessService
    {
        #region Inf00001厂别厂区主档
        /// <summary>
        /// 获取厂别的列表
        /// SAM 2017年4月26日15:19:54
        /// </summary>
        /// <param name="Token">授权码</param>
        /// <param name="Code">代号</param>
        /// <param name="page">页码</param>
        /// <param name="rows">行数</param>
        /// <returns></returns>
        public static object Inf00001getPlantList(string token, string Code, int page, int rows)
        {
            int count = 0;
            IList<Hashtable> result = SYS_OrganizationService.Inf00001getPlantList(Code, page, rows, ref count);
            return UtilBussinessService.getPaginationModel(result, count);
        }

        /// <summary>
        /// 删除厂别
        /// SAM 2017年7月20日22:46:42
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public static object Inf00001PlantDelete(JObject request)
        {
            string Token = request.Value<string>("Token");
            string userid = UtilBussinessService.detoken(Token);
            string OrganizationID = request.Value<string>("OrganizationID");
            SYS_Organization model = SYS_OrganizationService.get(OrganizationID);
            if (model == null)
                return new { status = "410", msg = "删除失败！不存在的厂别信息" };
            if (!SYS_PlantAreaService.CheckPlant(OrganizationID) && !SYS_OrganizationService.Inf00001Check(OrganizationID)
                && !SYS_OrganizationService.Inf00012Check(OrganizationID))
            {
                model.Status = 2;
                if (SYS_OrganizationService.update(userid, model))
                    return new { status = "200", msg = "删除成功！" };
                else
                    return new { status = "410", msg = "删除失败！" };
            }
            else
                return new { status = "410", msg = model.Name + "已使用，不能删除" };
        }


        /// <summary>
        /// 厂别删除
        /// 2017年4月26日15:24:09
        /// </summary>
        /// <param name="Token">授权码</param>
        /// <param name="jArray"></param>
        /// <returns></returns>
        public static object Plantdelete(string Token, JArray jArray)
        {
            int success = 0;
            int fail = 0;
            string failIDs = null;
            string msg = null;
            string userid = UtilBussinessService.detoken(Token);
            JObject data = null;
            SYS_Organization model = null;
            for (int i = 0; i < jArray.Count; i++)
            {
                data = (JObject)jArray[i];
                model = SYS_OrganizationService.get(data.Value<string>("OrganizationID"));
                if (!SYS_PlantAreaService.CheckPlant(data.Value<string>("OrganizationID"))
                    && !SYS_OrganizationService.Inf00001Check(data.Value<string>("OrganizationID"))
                    && !SYS_OrganizationService.Inf00012Check(data.Value<string>("OrganizationID"))
                    )
                {
                    model.Status = 2;
                    if (SYS_OrganizationService.update(userid, model))
                        success++;
                    else
                    {
                        failIDs = UtilBussinessService.str(failIDs, data.Value<string>("OrganizationID"));
                        fail++;
                    }
                }
                else
                {
                    failIDs = UtilBussinessService.str(failIDs, data.Value<string>("OrganizationID"));
                    msg = UtilBussinessService.str(msg, model.Code + "已使用，不能删除");
                    fail++;
                }

            }
            return new { success = success, fail = fail, failIDs = failIDs, msg = msg };
        }

        /// <summary>
        /// 厂别添加
        /// SAM 2017年4月26日14:49:09
        /// </summary>
        /// <param name="Token">授权码</param>
        /// <param name="jArray"></param>
        /// <returns></returns>
        public static object Plantinsert(string Token, JArray jArray)
        {
            int success = 0;
            int fail = 0;
            string failIDs = null;
            string msg = null;
            string userid = UtilBussinessService.detoken(Token);
            JObject data = null;
            SYS_Organization model = null;
            for (int i = 0; i < jArray.Count; i++)
            {
                data = (JObject)jArray[i];
                if (!SYS_OrganizationService.CheckCode(data.Value<string>("Code"), Framework.SystemID + "020121300001E", null))
                {
                    model = new SYS_Organization();
                    model.OrganizationID = UniversalService.GetSerialNumber("SYS_Organization");
                    model.ParentOrganizationID = "0";
                    model.Code = data.Value<string>("Code");
                    model.Name = data.Value<string>("Name");
                    model.Type = Framework.SystemID + "020121300001E";
                    model.Status = data.Value<int>("Status");
                    model.Sequence = string.IsNullOrWhiteSpace(data.Value<string>("Sequence")) ? 0 : data.Value<int>("Sequence");
                    model.Comments = data.Value<string>("Comments");
                    if (SYS_OrganizationService.insert(userid, model))
                        success++;
                    else
                    {
                        failIDs = UtilBussinessService.str(failIDs, data.Value<string>("OrganizationID"));
                        fail++;
                    }
                }
                else
                {
                    failIDs = UtilBussinessService.str(failIDs, data.Value<string>("OrganizationID"));
                    msg = UtilBussinessService.str(msg, data.Value<string>("Code") + "已存在");
                    fail++;
                }
            }
            return new { success = success, fail = fail, failIDs = failIDs, msg = msg };
        }

        /// <summary>
        /// 厂别更新
        /// SAM 2017年4月26日15:34:03
        /// </summary>
        /// <param name="Token">授权码</param>
        /// <param name="jArray"></param>
        /// <returns></returns>
        public static object Plantupdate(string Token, JArray jArray)
        {
            int success = 0;
            int fail = 0;
            string failIDs = null;
            string msg = null;
            string userid = UtilBussinessService.detoken(Token);
            JObject data = null;
            SYS_Organization model = null;
            for (int i = 0; i < jArray.Count; i++)
            {
                data = (JObject)jArray[i];
                if (!SYS_OrganizationService.CheckCode(data.Value<string>("Code"), Framework.SystemID + "020121300001E", data.Value<string>("OrganizationID")))
                {
                    model = SYS_OrganizationService.get(data.Value<string>("OrganizationID"));
                    model.Name = data.Value<string>("Name");
                    model.Comments = data.Value<string>("Comments");
                    model.Status = data.Value<int>("Status");
                    if (SYS_OrganizationService.update(userid, model))
                        success++;
                    else
                    {
                        failIDs = UtilBussinessService.str(failIDs, data.Value<string>("OrganizationID"));
                        fail++;
                    }
                }
                else
                {
                    failIDs = UtilBussinessService.str(failIDs, data.Value<string>("OrganizationID"));
                    msg = UtilBussinessService.str(msg, data.Value<string>("Code") + "已存在");
                    fail++;
                }

            }
            return new { success = success, fail = fail, failIDs = failIDs, msg = msg };
        }


        /// <summary>
        /// 获取指定厂别的语序
        /// SAM 2017年4月26日15:51:11
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static object Inf00001GetPlantLanguageList(string id)
        {
            return SYS_LanguageLibService.Inf00001GetPlantLanguageList(id);
        }

        /// <summary>
        /// 添加厂别语序
        /// SAM  2017年4月26日15:56:04
        /// </summary>
        /// <param name="Token">授权码</param>
        /// <param name="jArray"></param>
        /// <returns></returns>
        public static object Inf00001PlantLanguageinsert(string Token, JArray jArray)
        {
            int success = 0;
            int fail = 0;
            string failIDs = null;
            string msg = null;
            string userid = UtilBussinessService.detoken(Token);
            JObject request = null;
            for (int i = 0; i < jArray.Count; i++)
            {
                request = (JObject)jArray[i];
                if (!SYS_LanguageLibService.checkLanguage(request.Value<string>("RowID"), "Name", request.Value<string>("LanguageCode"), null))
                {
                    SYS_LanguageLib lan = new SYS_LanguageLib();
                    lan.LanguageLibID = UniversalService.GetSerialNumber("SYS_LanguageLib");
                    lan.TableID = "5";
                    lan.RowID = request.Value<string>("RowID");
                    lan.Field = "Name";
                    lan.OriginalLanguage = request.Value<string>("OriginalLanguage");
                    lan.OriginalContent = request.Value<string>("OriginalContent");
                    lan.LanguageCode = request.Value<string>("LanguageCode");
                    lan.LanguageContentOne = request.Value<string>("Name");
                    lan.IsDefault = request.Value<bool>("IsDefault");
                    if (SYS_LanguageLibService.insert(userid, lan))
                    {
                        lan.LanguageLibID = UniversalService.GetSerialNumber("SYS_LanguageLib");
                        lan.Field = "Comments";
                        lan.LanguageCode = request.Value<string>("LanguageCode");
                        lan.LanguageContentOne = request.Value<string>("Comments");
                        SYS_LanguageLibService.insert(userid, lan);
                        success++;
                    }
                    else
                    {
                        failIDs = UtilBussinessService.str(failIDs, request.Value<string>("LanguageLibID"));
                        fail++;
                    }
                }
                else
                {
                    failIDs = UtilBussinessService.str(failIDs, request.Value<string>("LanguageLibID"));
                    msg = UtilBussinessService.str(msg, "语序已存在");
                    fail++;
                }
            }

            return new { success = success, fail = fail, failIDs = failIDs };
        }

        /// <summary>
        /// 更新厂别语序
        /// SAM 2017年1月17日09:42:23
        /// </summary>
        /// <param name="Token">授权码</param>
        /// <param name="jArray"></param>
        /// <returns></returns>
        public static object Inf00001PlantLanguageupdate(string Token, JArray jArray)
        {
            int success = 0;
            int fail = 0;
            string failIDs = null;
            string msg = null;
            string userid = UtilBussinessService.detoken(Token);
            JObject request = null;
            try
            {
                for (int i = 0; i < jArray.Count; i++)
                {
                    request = (JObject)jArray[i];
                    if (!SYS_LanguageLibService.checkLanguage(request.Value<string>("RowID"), "Name", request.Value<string>("LanguageCode"), request.Value<String>("LanguageLibID")))
                    {
                        bool IsDefault = request.Value<bool>("IsDefault");
                        try
                        {
                            SYS_LanguageLibService.UpdateLanguage(userid, request.Value<string>("Name"), request.Value<string>("LanguageCode"), IsDefault, request.Value<string>("RowID"), "Name");
                            SYS_LanguageLibService.UpdateLanguage(userid, request.Value<string>("Comments"), request.Value<string>("LanguageCode"), IsDefault, request.Value<string>("RowID"), "Comments");
                            success++;
                        }
                        catch (Exception ex)
                        {
                            DataLogerService.writeerrlog(ex);
                            failIDs = UtilBussinessService.str(failIDs, request.Value<string>("LanguageLibID"));
                            fail++;
                        }
                    }
                    else
                    {
                        failIDs = UtilBussinessService.str(failIDs, request.Value<string>("LanguageLibID"));
                        msg = UtilBussinessService.str(msg, "语序已存在");
                        fail++;
                    }
                }
            }
            catch (Exception ex)
            {
                DataLogerService.writeerrlog(ex);
            }
            return new { success = success, fail = fail, failIDs = failIDs };
        }

        /// <summary>
        /// 删除厂别语序
        /// SAM 2017年1月17日09:39:52
        /// </summary>
        /// <param name="jArray"></param>
        /// <returns></returns>
        public static object Inf00001PlantLanguagedelete(JArray jArray)
        {
            int success = 0;
            int fail = 0;
            string failIDs = null;
            JObject request = null;
            for (int i = 0; i < jArray.Count; i++)
            {
                request = (JObject)jArray[i];

                string LanguageCode = request.Value<string>("LanguageCode");
                string rowid = request.Value<string>("RowID");
                if (SYS_LanguageLibService.DeleteLanguage(LanguageCode, rowid))
                    success++;
                else
                {
                    failIDs = UtilBussinessService.str(failIDs, request.Value<string>("LanguageLibID"));
                    fail++;
                }
            }
            return new { success = success, fail = fail, failIDs = failIDs };
        }

        /// <summary>
        /// 获取厂区列表
        /// SAM 2017年4月26日15:36:53
        /// </summary>
        /// <param name="Token">授权码</param>
        /// <param name="Code">代号</param>
        /// <param name="page">页码</param>
        /// <param name="rows">行数</param>
        /// <returns></returns>
        public static object Inf00001getPlantAreaList(string token, string Code, int page, int rows)
        {
            int count = 0;
            return UtilBussinessService.getPaginationModel(SYS_PlantAreaService.Inf00001getPlantAreaList(Code, page, rows, ref count), count);
        }

        /// <summary>
        /// 厂区删除
        /// 2017年4月26日14:46:30
        /// </summary>
        /// <param name="Token">授权码</param>
        /// <param name="jArray"></param>
        /// <returns></returns>
        public static object PlantAreadelete(string Token, JArray jArray)
        {
            int success = 0;
            int fail = 0;
            string failIDs = null;
            string msg = null;
            string userid = UtilBussinessService.detoken(Token);
            JObject data = null;
            SYS_PlantArea model = null;
            for (int i = 0; i < jArray.Count; i++)
            {
                data = (JObject)jArray[i];
                model = SYS_PlantAreaService.get(data.Value<string>("PlantAreaID"));
                model.Status = Framework.SystemID + "0201213000003";
                if (SYS_PlantAreaService.update(userid, model))
                    success++;
                else
                {
                    failIDs = UtilBussinessService.str(failIDs, data.Value<string>("MaterialStructureID"));
                    fail++;
                }

            }
            return new { success = success, fail = fail, failIDs = failIDs, msg = msg };
        }


        /// <summary>
        /// 厂区添加
        /// SAM 2017年4月26日14:49:09
        /// </summary>
        /// <param name="Token">授权码</param>
        /// <param name="jArray"></param>
        /// <returns></returns>
        public static object PlantAreainsert(string Token, JArray jArray)
        {
            int success = 0;
            int fail = 0;
            string failIDs = null;
            string msg = null;
            string userid = UtilBussinessService.detoken(Token);
            JObject data = null;
            SYS_PlantArea model = null;
            for (int i = 0; i < jArray.Count; i++)
            {
                data = (JObject)jArray[i];
                if (!SYS_PlantAreaService.CheckCode(data.Value<string>("Code"), null))
                {
                    model = new SYS_PlantArea();
                    model.PlantAreaID = UniversalService.GetSerialNumber("SYS_PlantArea");
                    model.Code = data.Value<string>("Code");
                    model.Name = data.Value<string>("Name");
                    model.PlantID = data.Value<string>("PlantID");
                    model.Status = data.Value<string>("Status");
                    model.Comments = data.Value<string>("Comments");
                    if (SYS_PlantAreaService.insert(userid, model))
                        success++;
                    else
                    {
                        failIDs = UtilBussinessService.str(failIDs, data.Value<string>("PlantAreaID"));
                        fail++;
                    }
                }
                else
                {
                    failIDs = UtilBussinessService.str(failIDs, data.Value<string>("PlantAreaID"));
                    msg = UtilBussinessService.str(msg, data.Value<string>("Code") + "已存在");
                    fail++;
                }
            }
            return new { success = success, fail = fail, failIDs = failIDs, msg = msg };
        }

        /// <summary>
        /// 厂区更新
        /// SAM 2017年3月14日17:37:43
        /// </summary>
        /// <param name="Token">授权码</param>
        /// <param name="jArray"></param>
        /// <returns></returns>
        public static object PlantAreaupdate(string Token, JArray jArray)
        {
            int success = 0;
            int fail = 0;
            string failIDs = null;
            string msg = null;
            string userid = UtilBussinessService.detoken(Token);
            JObject data = null;
            SYS_PlantArea model = null;
            for (int i = 0; i < jArray.Count; i++)
            {
                data = (JObject)jArray[i];
                if (!SYS_PlantAreaService.CheckCode(data.Value<string>("Code"), data.Value<string>("PlantAreaID")))
                {
                    model = SYS_PlantAreaService.get(data.Value<string>("PlantAreaID"));
                    model.Status = data.Value<string>("Status");
                    model.PlantID = data.Value<string>("PlantID");
                    model.Name = data.Value<string>("Name");
                    model.Comments = data.Value<string>("Comments");
                    if (SYS_PlantAreaService.update(userid, model))
                        success++;
                    else
                    {
                        failIDs = UtilBussinessService.str(failIDs, data.Value<string>("PlantAreaID"));
                        fail++;
                    }
                }
                else
                {
                    failIDs = UtilBussinessService.str(failIDs, data.Value<string>("PlantAreaID"));
                    msg = UtilBussinessService.str(msg, data.Value<string>("Code") + "已存在");
                    fail++;
                }

            }
            return new { success = success, fail = fail, failIDs = failIDs, msg = msg };
        }



        /// <summary>
        /// 获取指定厂别的语序
        /// SAM 2017年4月26日15:51:11
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static object Inf00001GetPlantAreaLanguageList(string id)
        {
            return SYS_LanguageLibService.Inf00001GetPlantAreaLanguageList(id);
        }

        /// <summary>
        /// 添加厂区语序
        /// SAM  2017年4月26日15:56:04
        /// </summary>
        /// <param name="Token">授权码</param>
        /// <param name="jArray"></param>
        /// <returns></returns>
        public static object Inf00001PlantAreaLanguageinsert(string Token, JArray jArray)
        {
            int success = 0;
            int fail = 0;
            string failIDs = null;
            string msg = null;
            string userid = UtilBussinessService.detoken(Token);
            JObject request = null;
            for (int i = 0; i < jArray.Count; i++)
            {
                request = (JObject)jArray[i];
                if (!SYS_LanguageLibService.checkLanguage(request.Value<string>("RowID"), "Name", request.Value<string>("LanguageCode"), null))
                {
                    SYS_LanguageLib lan = new SYS_LanguageLib();
                    lan.LanguageLibID = UniversalService.GetSerialNumber("SYS_LanguageLib");
                    lan.TableID = "30";
                    lan.RowID = request.Value<string>("RowID");
                    lan.Field = "Name";
                    lan.OriginalLanguage = request.Value<string>("OriginalLanguage");
                    lan.OriginalContent = request.Value<string>("OriginalContent");
                    lan.LanguageCode = request.Value<string>("LanguageCode");
                    lan.LanguageContentOne = request.Value<string>("Name");
                    lan.IsDefault = request.Value<bool>("IsDefault");
                    if (SYS_LanguageLibService.insert(userid, lan))
                    {
                        lan.LanguageLibID = UniversalService.GetSerialNumber("SYS_LanguageLib");
                        lan.Field = "Comments";
                        lan.LanguageCode = request.Value<string>("LanguageCode");
                        lan.LanguageContentOne = request.Value<string>("Comments");
                        SYS_LanguageLibService.insert(userid, lan);
                        success++;
                    }
                    else
                    {
                        failIDs = UtilBussinessService.str(failIDs, request.Value<string>("LanguageLibID"));
                        fail++;
                    }
                }
                else
                {
                    failIDs = UtilBussinessService.str(failIDs, request.Value<string>("LanguageLibID"));
                    msg = UtilBussinessService.str(msg, "语序已存在");
                    fail++;
                }
            }

            return new { success = success, fail = fail, failIDs = failIDs };
        }

        /// <summary>
        /// 更新厂区语序
        /// SAM 2017年1月17日09:42:23
        /// </summary>
        /// <param name="Token">授权码</param>
        /// <param name="jArray"></param>
        /// <returns></returns>
        public static object Inf00001PlantAreaLanguageupdate(string Token, JArray jArray)
        {
            int success = 0;
            int fail = 0;
            string failIDs = null;
            string msg = null;
            string userid = UtilBussinessService.detoken(Token);
            JObject request = null;
            try
            {
                for (int i = 0; i < jArray.Count; i++)
                {
                    request = (JObject)jArray[i];
                    if (!SYS_LanguageLibService.checkLanguage(request.Value<string>("RowID"), "Name", request.Value<string>("LanguageCode"), request.Value<String>("LanguageLibID")))
                    {
                        bool IsDefault = request.Value<bool>("IsDefault");
                        try
                        {
                            SYS_LanguageLibService.UpdateLanguage(userid, request.Value<string>("Name"), request.Value<string>("LanguageCode"), IsDefault, request.Value<string>("RowID"), "Name");
                            SYS_LanguageLibService.UpdateLanguage(userid, request.Value<string>("Comments"), request.Value<string>("LanguageCode"), IsDefault, request.Value<string>("RowID"), "Comments");
                            success++;
                        }
                        catch (Exception ex)
                        {
                            DataLogerService.writeerrlog(ex);
                            failIDs = UtilBussinessService.str(failIDs, request.Value<string>("LanguageLibID"));
                            fail++;
                        }
                    }
                    else
                    {
                        failIDs = UtilBussinessService.str(failIDs, request.Value<string>("LanguageLibID"));
                        msg = UtilBussinessService.str(msg, "语序已存在");
                        fail++;
                    }
                }
            }
            catch (Exception ex)
            {
                DataLogerService.writeerrlog(ex);
            }
            return new { success = success, fail = fail, failIDs = failIDs };
        }

        /// <summary>
        /// 删除厂区语序
        /// SAM 2017年1月17日09:39:52
        /// </summary>
        /// <param name="jArray"></param>
        /// <returns></returns>
        public static object Inf00001PlantAreaLanguagedelete(JArray jArray)
        {
            int success = 0;
            int fail = 0;
            string failIDs = null;
            JObject request = null;
            for (int i = 0; i < jArray.Count; i++)
            {
                request = (JObject)jArray[i];

                string LanguageCode = request.Value<string>("LanguageCode");
                string rowid = request.Value<string>("RowID");
                if (SYS_LanguageLibService.DeleteLanguage(LanguageCode, rowid))
                    success++;
                else
                {
                    failIDs = UtilBussinessService.str(failIDs, request.Value<string>("LanguageLibID"));
                    fail++;
                }
            }
            return new { success = success, fail = fail, failIDs = failIDs };
        }

        #endregion

        #region Inf00002个人设定

        /// <summary>
        /// 账号管理列表--用于个人设定的修改版
        /// MOUSE 2017年8月2日17:53:06
        /// 由于需求要求不能跨厂别查询，当初始时，即OrganizationID为空时，
        /// 去根据登录用户查询他所在部门所属厂别下的所有部门，默认只查这一部分的用户以及没有归属任何厂别的用户
        /// </summary>
        /// <param name="Token">授权码</param>
        /// <param name="Account">账号</param>
        /// <param name="UserName">姓名</param>
        /// <param name="page">页码</param>
        /// <param name="rows">行数</param>
        /// <returns></returns>
        public static object Inf00002GetList(string token, string OrganizationID, string Account, string UserName, int page, int rows)
        {
            int count = 0;
            string userid = UtilBussinessService.detoken(token);
            //string Default = null;
            //if (string.IsNullOrWhiteSpace(OrganizationID))
            //{
            //    string Organization = SYS_OrganizationService.getUserOrg(userid);
            //    if (Organization == null)
            //        Default = null;
            //    SYS_Organization Org = SYS_OrganizationService.get(Organization);
            //    if (Org == null)
            //        Default = null;
            //    SYS_Organization Plant = SYS_OrganizationService.get(Org.PlantID);
            //    if (Plant == null)
            //        Default = null;
            //    IList<Hashtable> orgs = SYS_OrganizationService.Inf00005GetDeptList(Plant.Code);
            //    Default = string.Join("','", orgs.Select(s => s["OrganizationID"].ToString()).ToList());
            //}
            return UtilBussinessService.getPaginationModel(SYS_MESUserService.Inf00002GetList(OrganizationID, Account, UserName, page, rows, ref count), count);
        }

        /// <summary>
        /// 获取当前登录用户的个人信息-修改版
        /// MOUSE 2017年8月4日15:15:08
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public static Hashtable Inf00002GetUser(string Token)
        {
            string userid = UtilBussinessService.detoken(Token);
            return SYS_MESUserService.Inf00002GetUser(userid);
        }

        /// <summary>
        /// 个人设定保存
        /// MOUSE 2017年8月4日16:49:29
        /// </summary>
        /// <param name="Token">授权码</param>
        /// <param name="jArray"></param>
        /// <returns></returns>
        public static object Inf00002UserSave(string Token, JObject request)
        {
            string userId = UtilBussinessService.detoken(Token);

            SYS_MESUsers model = SYS_MESUserService.get(request.Value<string>("MESUserID"));

            if (model == null)
                return new { status = "410", msg = "流水号错误!" };
            model.EnglishName = request.Value<string>("EnglishName");
            model.Language = request.Value<int>("Language");
            model.Email = request.Value<string>("Email");
            if (SYS_MESUserService.update(userId, model))
            {
                return new { status = "200", msg = "编辑成功!" };
            }
            else
                return new { status = "410", msg = "编辑失败!" };
        }
        /// <summary>
        /// 修复密码
        /// Tom
        /// </summary>
        /// <param name="request">JSON数据</param>
        /// <returns></returns>
        public static object Inf00002ResetPassword(JObject request)
        {

            string NewPassword = request.Value<string>("NewPassword");
            string ComfirmPassword = request.Value<string>("ComfirmPassword");
            string Token = request.Value<string>("Token");
            string userID = UniversalService.detoken(Token);
            if (string.IsNullOrWhiteSpace(NewPassword))
            {
                return new { status = "400", msg = "新密码不能为空" };
            }
            if (NewPassword != ComfirmPassword)
            {
                return new { status = "400", msg = "确认密码与新密码不匹配" };
            }
            if (SYS_MESUserService.ResetPassword(userID, NewPassword))
            {
                return new { status = "200", msg = "修改成功" };
            }
            else
            {
                return new { status = "400", msg = "修改失败" };
            }
        }
        #endregion

        #region Inf00003账号管理主档
        /// <summary>
        /// 账号管理列表
        /// SAM 2017年5月4日14:33:34
        /// 修改：2017年6月6日21:40:50
        /// 由于需求要求不能跨厂别查询，当初始时，即OrganizationID为空时，
        /// 去根据登录用户查询他所在部门所属厂别下的所有部门，默认只查这一部分的用户以及没有归属任何厂别的用户
        /// 
        /// 修改：2017年8月2日16:29:13 SAM
        /// 由于需求更改要求初始默认显示所有数据，以上修改作废
        /// </summary>
        /// <param name="Token">授权码</param>
        /// <param name="Code">代号</param>
        /// <param name="Status">状态</param>
        /// <param name="page">页码</param>
        /// <param name="rows">行数</param>
        /// <returns></returns>
        public static object Inf00003GetList(string token, string OrganizationID, string Code, string Status, int page, int rows)
        {
            int count = 0;
            //string userid = UtilBussinessService.detoken(token);
            //string Default = null;
            //if (string.IsNullOrWhiteSpace(OrganizationID))
            //{
            //    string Organization = SYS_OrganizationService.getUserOrg(userid);
            //    if (Organization == null)
            //        Default = null;
            //    SYS_Organization Org = SYS_OrganizationService.get(Organization);
            //    if (Org == null)
            //        Default = null;
            //    SYS_Organization Plant = SYS_OrganizationService.get(Org.PlantID);
            //    if (Plant == null)
            //        Default = null;
            //    IList<Hashtable> orgs = SYS_OrganizationService.Inf00005GetDeptList(Plant.Code);
            //    Default = string.Join("','", orgs.Select(s => s["OrganizationID"].ToString()).ToList());
            //}
            return UtilBussinessService.getPaginationModel(SYS_MESUserService.Inf00003GetList(OrganizationID, Code, Status, page, rows, ref count), count);
        }

        /// <summary>
        /// 议题修改 账号管理列表
        /// Mouse 2017年11月1日14:56:28
        /// </summary>
        /// <param name="token"></param>
        /// <param name="OrganizationID"></param>
        /// <param name="Code"></param>
        /// <param name="Status"></param>
        /// <param name="Account"></param>
        /// <param name="UserName"></param>
        /// <param name="OrganizationId"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <returns></returns>
        public static object Inf00003GetListV1(string token, string OrganizationID, string Code, string Status, string Account, string UserName, string DeptID, int page, int rows)
        {
            int count = 0;
            return UtilBussinessService.getPaginationModel(SYS_MESUserService.Inf00003GetListV1(OrganizationID, Code, Status, Account, UserName, DeptID, page, rows, ref count), count);
        }

        /// <summary>
        /// 账号添加
        /// </summary>
        /// <param name="Token">授权码</param>
        /// <param name="request">JSON数据</param>
        /// <returns></returns>
        public static object Inf00003Add(string Token, JObject request)
        {
            string userId = UtilBussinessService.detoken(Token);

            if (string.IsNullOrWhiteSpace(request.Value<string>("Account")) || string.IsNullOrWhiteSpace(request.Value<string>("UserName")))
                return new { status = "410", msg = "帐号和名称不能为空！" };


            if (SYS_MESUserService.CheckCode(request.Value<string>("Account"), null, null, null))
                return new { status = "410", msg = "帐号重复!" };

            if (!string.IsNullOrWhiteSpace(request.Value<string>("Emplno")))
            {
                if (SYS_MESUserService.CheckCode(null, request.Value<string>("Emplno"), null, null))
                    return new { status = "410", msg = "工号重复!" };
            }

            if (!string.IsNullOrWhiteSpace(request.Value<string>("CardCode")))
            {
                if (SYS_MESUserService.CheckCode(null, null, request.Value<string>("CardCode"), null))
                    return new { status = "410", msg = "卡号重复!" };
            }

            SYS_MESUsers model = new SYS_MESUsers();
            model.MESUserID = UniversalService.GetSerialNumber("SYS_MESUsers");
            model.Account = request.Value<string>("Account");
            model.Password = "3d9188577cc9bfe9291ac66b5cc872b7";
            model.UserType = 10;
            model.Status = request.Value<int>("Status");
            model.UserName = request.Value<string>("UserName");
            model.EnglishName = request.Value<string>("EnglishName");
            model.Emplno = request.Value<string>("Emplno");
            model.CardCode = request.Value<string>("CardCode");
            model.Sex = request.Value<bool>("Sex");
            model.Email = request.Value<string>("Email");
            model.Brith = request.Value<string>("Brith");
            model.IDcard = request.Value<string>("IDcard");
            model.InTime = request.Value<string>("InTime");
            model.Type = request.Value<string>("Type");
            model.Mobile = request.Value<string>("Mobile");
            model.Sequence = string.IsNullOrWhiteSpace(request.Value<string>("Sequence")) ? 0 : request.Value<int>("Sequence");
            model.Comments = request.Value<string>("Comments");
            if (SYS_MESUserService.insert(userId, model))
            {
                //根据前段是否传进来判断是否需要再次添加用户和组织的映射
                if (!string.IsNullOrWhiteSpace(request.Value<string>("OrganizationID")))
                    SYS_UserOrganizationMappingService.add(userId, model.MESUserID, request.Value<string>("OrganizationID"));

                return new { status = "200", msg = "新增成功!" };
            }
            else
                return new { status = "410", msg = "新增失败!" };


        }

        /// <summary>
        /// 账号的更新
        /// SAM 2017年5月9日17:17:38
        /// </summary>
        /// <param name="Token">授权码</param>
        /// <param name="request">JSON数据</param>
        /// <returns></returns>
        public static object Inf00003Update(string Token, JObject request)
        {
            string userId = UtilBussinessService.detoken(Token);

            if (string.IsNullOrWhiteSpace(request.Value<string>("UserName")))
                return new { status = "410", msg = "名称不能为空！" };

            //if (SYS_MESUserService.CheckCode(request.Value<string>("Account"), null, null, request.Value<string>("MESUserID")))
            //    return new { status = "410", msg = "账号重复!" };

            if (!string.IsNullOrWhiteSpace(request.Value<string>("Emplno")))
            {
                if (SYS_MESUserService.CheckCode(null, request.Value<string>("Emplno"), null, request.Value<string>("MESUserID")))
                    return new { status = "410", msg = "工号重复!" };
            }

            if (!string.IsNullOrWhiteSpace(request.Value<string>("CardCode")))
            {
                if (SYS_MESUserService.CheckCode(null, null, request.Value<string>("CardCode"), request.Value<string>("MESUserID")))
                    return new { status = "410", msg = "卡号重复!" };
            }

            SYS_MESUsers model = SYS_MESUserService.get(request.Value<string>("MESUserID"));

            if (model == null)
                return new { status = "410", msg = "流水号错误!" };

            //model.Account = request.Value<string>("Account");
            model.Status = request.Value<int>("Status");
            model.UserName = request.Value<string>("UserName");
            model.EnglishName = request.Value<string>("EnglishName");
            model.Emplno = request.Value<string>("Emplno");
            model.CardCode = request.Value<string>("CardCode");
            model.Sex = request.Value<bool>("Sex");
            model.Email = request.Value<string>("Email");
            model.Brith = request.Value<string>("Brith");
            model.IDcard = request.Value<string>("IDcard");
            model.InTime = request.Value<string>("InTime");
            model.Type = request.Value<string>("Type");
            model.Mobile = request.Value<string>("Mobile");
            model.Sequence = string.IsNullOrWhiteSpace(request.Value<string>("Sequence")) ? 0 : request.Value<int>("Sequence");
            model.Comments = request.Value<string>("Comments");
            if (SYS_MESUserService.update(userId, model))
            {
                try
                {
                    //删除用户和组织的映射
                    SYS_UserOrganizationMappingService.Delete(model.MESUserID);
                    //根据前段是否传进来判断是否需要再次添加用户和组织的映射
                    if (!string.IsNullOrWhiteSpace(request.Value<string>("OrganizationID")))
                        SYS_UserOrganizationMappingService.add(userId, model.MESUserID, request.Value<string>("OrganizationID"));
                }
                catch (Exception ex)
                {
                    DataLogerService.writeerrlog(ex);
                }
                return new { status = "200", msg = "编辑成功!" };
            }
            else
                return new { status = "410", msg = "编辑失败!" };
        }

        /// <summary>
        /// 帐号的删除
        /// SAM 2017年7月30日23:40:04
        /// </summary>
        /// <param name="Token"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        public static object Inf00003Delete(string Token, JObject request)
        {
            string userId = UtilBussinessService.detoken(Token);
            SYS_MESUsers model = SYS_MESUserService.get(request.Value<string>("MESUserID"));

            if (model == null)
                return new { status = "410", msg = "帐号信息不存在!" };

            if (SYS_DocumentAuthorityService.CheckUser(model.MESUserID))// 单据部门员工权限表
                return new { status = "410", msg = model.Emplno + "代号已使用，不得删除" };

            if (SYS_MESUserService.CheckRoleUser(model.MESUserID))// 角色成员表 INFO_ROLE_USER
                return new { status = "410", msg = model.Emplno + "代号已使用，不得删除" };

            if (SYS_CustomersService.CheckUser(model.MESUserID))// 客户主档 INFO_CUSTOMER
                return new { status = "410", msg = model.Emplno + "代号已使用，不得删除" };

            if (SYS_ManufacturersService.CheckUser(model.MESUserID))//  厂商主档 INFO_VENDOR
                return new { status = "410", msg = model.Emplno + "代号已使用，不得删除" };

            model.Status = 2;
            if (SYS_MESUserService.update(userId, model))
            {
                SYS_UserOrganizationMappingService.Delete(model.MESUserID);
                return new { status = "200", msg = "删除成功!" };
            }
            else
                return new { status = "410", msg = "删除失败!" };
        }

        /// <summary>
        ///  账号管理-获取所有有厂别的部门
        ///  SAM 2017年7月4日11:00:59
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public static IList<Hashtable> Inf00003GetOrganization(string token)
        {
            return SYS_OrganizationService.Inf00003GetOrganization();
        }

        /// <summary>
        /// 账号删除
        /// SAM 2017年5月4日14:45:32
        /// </summary>
        /// <param name="Token">授权码</param>
        /// <param name="jArray"></param>
        /// <returns></returns>
        public static object Inf00003delete(string Token, JArray jArray)
        {
            int success = 0;
            int fail = 0;
            string failIDs = null;
            string msg = null;
            string userid = UtilBussinessService.detoken(Token);
            JObject data = null;
            SYS_MESUsers model = null;
            for (int i = 0; i < jArray.Count; i++)
            {
                data = (JObject)jArray[i];
                //if (!SYS_PlantAreaService.CheckPlant(data.Value<string>("OrganizationID")))
                //{
                model = SYS_MESUserService.get(data.Value<string>("MESUserID"));
                model.Status = 2;
                if (SYS_MESUserService.update(userid, model))
                    success++;
                else
                {
                    failIDs = UtilBussinessService.str(failIDs, data.Value<string>("MESUserID"));
                    fail++;
                }
                //}
                //else
                //{
                //    failIDs = UtilBussinessService.str(failIDs, data.Value<string>("OrganizationID"));
                //    msg = UtilBussinessService.str(failIDs, "");
                //    fail++;
                //}
            }
            return new { success = success, fail = fail, failIDs = failIDs, msg = msg };
        }

        /// <summary>
        /// 账号添加
        /// SAM 2017年5月4日14:45:37
        /// </summary>
        /// <param name="Token">授权码</param>
        /// <param name="jArray"></param>
        /// <returns></returns>
        public static object Inf00003insert(string Token, JArray jArray)
        {
            int success = 0;
            int fail = 0;
            string failIDs = null;
            string msg = null;
            string userid = UtilBussinessService.detoken(Token);
            JObject data = null;
            SYS_MESUsers model = null;
            for (int i = 0; i < jArray.Count; i++)
            {
                data = (JObject)jArray[i];
                if (!SYS_MESUserService.CheckCode(data.Value<string>("Account"), null, null, null))
                {
                    if (!SYS_MESUserService.CheckCode(null, data.Value<string>("Emplno"), null, null))
                    {
                        if (!SYS_MESUserService.CheckCode(null, null, data.Value<string>("CardCode"), null))
                        {
                            model = new SYS_MESUsers();
                            model.MESUserID = UniversalService.GetSerialNumber("SYS_MESUsers");
                            model.Account = data.Value<string>("Account");
                            model.Password = "3d9188577cc9bfe9291ac66b5cc872b7";
                            model.UserType = 10;
                            model.Status = data.Value<int>("Status");
                            model.UserName = data.Value<string>("UserName");
                            model.EnglishName = data.Value<string>("EnglishName");
                            model.Emplno = data.Value<string>("Emplno");
                            model.CardCode = data.Value<string>("CardCode");
                            model.Sex = data.Value<bool>("Sex");
                            model.Email = data.Value<string>("Email");
                            model.Brith = data.Value<string>("Brith");
                            model.IDcard = data.Value<string>("IDcard");
                            model.InTime = data.Value<string>("InTime");
                            model.Type = data.Value<string>("Type");
                            model.Sequence = string.IsNullOrWhiteSpace(data.Value<string>("Sequence")) ? 0 : data.Value<int>("Sequence");
                            model.Comments = data.Value<string>("Comments");
                            if (SYS_MESUserService.insert(userid, model))
                                success++;
                            else
                            {
                                failIDs = UtilBussinessService.str(failIDs, data.Value<string>("MESUserID"));
                                fail++;
                            }
                        }
                        else
                        {
                            failIDs = UtilBussinessService.str(failIDs, data.Value<string>("MESUserID"));
                            msg = UtilBussinessService.str(msg, "卡号重复");
                            fail++;
                        }
                    }
                    else
                    {
                        failIDs = UtilBussinessService.str(failIDs, data.Value<string>("MESUserID"));
                        msg = UtilBussinessService.str(msg, "工号重复");
                        fail++;
                    }
                }
                else
                {
                    failIDs = UtilBussinessService.str(failIDs, data.Value<string>("MESUserID"));
                    msg = UtilBussinessService.str(msg, "账号重复");
                    fail++;
                }
            }
            return new { success = success, fail = fail, failIDs = failIDs, msg = msg };
        }

        /// <summary>
        /// 账号更新
        /// SAM 2017年5月4日14:45:42
        /// </summary>
        /// <param name="Token">授权码</param>
        /// <param name="jArray"></param>
        /// <returns></returns>
        public static object Inf00003update(string Token, JArray jArray)
        {
            int success = 0;
            int fail = 0;
            string failIDs = null;
            string msg = null;
            string userid = UtilBussinessService.detoken(Token);
            JObject data = null;
            SYS_MESUsers model = null;
            for (int i = 0; i < jArray.Count; i++)
            {
                data = (JObject)jArray[i];
                if (!SYS_MESUserService.CheckCode(null, data.Value<string>("Emplno"), null, data.Value<string>("MESUserID")))
                {
                    if (!SYS_MESUserService.CheckCode(null, null, data.Value<string>("CardCode"), data.Value<string>("MESUserID")))
                    {
                        model = SYS_MESUserService.get(data.Value<string>("MESUserID"));
                        model.Status = data.Value<int>("Status");
                        model.UserName = data.Value<string>("UserName");
                        model.EnglishName = data.Value<string>("EnglishName");
                        model.Emplno = data.Value<string>("Emplno");
                        model.CardCode = data.Value<string>("CardCode");
                        model.Sex = data.Value<bool>("Sex");
                        model.Email = data.Value<string>("Email");
                        model.Brith = data.Value<string>("Brith");
                        model.IDcard = data.Value<string>("IDcard");
                        model.Brith = data.Value<string>("Brith");
                        model.InTime = data.Value<string>("InTime");
                        model.Type = data.Value<string>("Type");
                        model.Sequence = string.IsNullOrWhiteSpace(data.Value<string>("Sequence")) ? 0 : data.Value<int>("Sequence");
                        model.Comments = data.Value<string>("Comments");
                        if (SYS_MESUserService.insert(userid, model))
                            success++;
                        else
                        {
                            failIDs = UtilBussinessService.str(failIDs, data.Value<string>("MESUserID"));
                            fail++;
                        }
                    }
                    else
                    {
                        failIDs = UtilBussinessService.str(failIDs, data.Value<string>("MESUserID"));
                        msg = UtilBussinessService.str(msg, "卡号重复");
                        fail++;
                    }
                }
                else
                {
                    failIDs = UtilBussinessService.str(failIDs, data.Value<string>("MESUserID"));
                    msg = UtilBussinessService.str(msg, "工号重复");
                    fail++;
                }
            }
            return new { success = success, fail = fail, failIDs = failIDs, msg = msg };
        }


        /// <summary>
        /// 获取组织结构
        /// SAM 2017年5月25日16:42:53
        /// </summary>
        /// <param name="Token">授权码</param>
        /// <returns></returns>
        public static IList<Hashtable> GetOrganization(string token)
        {
            string userid = UtilBussinessService.detoken(token);
            string Organization = SYS_OrganizationService.getUserOrg(userid);
            if (Organization == null)
                return null;

            SYS_Organization Org = SYS_OrganizationService.get(Organization);
            if (Org == null)
                return null;

            SYS_Organization Plant = SYS_OrganizationService.get(Org.PlantID);
            if (Plant == null)
                return null;

            return SYS_OrganizationService.Inf00005GetDeptList(Plant.Code);
        }


        #endregion

        #region Inf00004角色主档
        /// <summary>
        /// 获取角色语系
        /// Tom 2017年4月27日16:45:20
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static object Inf00004GetPlantLanguageList(string id)
        {
            return SYS_LanguageLibService.Inf00004GetPlantAreaLanguageList(id);
        }


        /// <summary>
        /// 添加角色语序
        /// Tom  2017年4月26日15:56:04
        /// </summary>
        /// <param name="Token">授权码</param>
        /// <param name="jArray"></param>
        /// <returns></returns>
        public static object Inf00004Languageinsert(string Token, JArray jArray)
        {
            int success = 0;
            int fail = 0;
            string failIDs = null;
            string msg = null;
            string userid = UtilBussinessService.detoken(Token);
            JObject request = null;
            for (int i = 0; i < jArray.Count; i++)
            {
                request = (JObject)jArray[i];
                if (!SYS_LanguageLibService.checkLanguage(request.Value<string>("RoleID"), "Name", request.Value<string>("LanguageCode"), null))
                {
                    SYS_LanguageLib lan = new SYS_LanguageLib();
                    lan.LanguageLibID = UniversalService.GetSerialNumber("SYS_LanguageLib");
                    lan.TableID = "6";
                    lan.RowID = request.Value<string>("RoleID");
                    lan.Field = "Name";
                    lan.OriginalLanguage = request.Value<string>("OriginalLanguage");
                    lan.OriginalContent = request.Value<string>("OriginalContent");
                    lan.LanguageCode = request.Value<string>("LanguageCode");
                    lan.LanguageContentOne = request.Value<string>("Name");
                    lan.IsDefault = request.Value<bool>("IsDefault");
                    if (SYS_LanguageLibService.insert(userid, lan))
                    {
                        lan.LanguageLibID = UniversalService.GetSerialNumber("SYS_LanguageLib");
                        lan.Field = "Description";
                        lan.LanguageCode = request.Value<string>("LanguageCode");
                        lan.LanguageContentOne = request.Value<string>("Description");
                        SYS_LanguageLibService.insert(userid, lan);
                        success++;
                    }
                    else
                    {
                        failIDs = UtilBussinessService.str(failIDs, request.Value<string>("LanguageLibID"));
                        fail++;
                    }
                }
                else
                {
                    failIDs = UtilBussinessService.str(failIDs, request.Value<string>("LanguageLibID"));
                    msg = UtilBussinessService.str(msg, "已存在语序");
                    fail++;
                }
            }

            return new { success = success, fail = fail, failIDs = failIDs };
        }

        /// <summary>
        /// 更新角色语序
        /// Tom 2017年1月17日09:42:23
        /// </summary>
        /// <param name="Token">授权码</param>
        /// <param name="jArray"></param>
        /// <returns></returns>
        public static object Inf00004Languageupdate(string Token, JArray jArray)
        {
            int success = 0;
            int fail = 0;
            string failIDs = null;
            string msg = null;
            string userid = UtilBussinessService.detoken(Token);
            JObject request = null;
            try
            {
                for (int i = 0; i < jArray.Count; i++)
                {
                    request = (JObject)jArray[i];
                    if (!SYS_LanguageLibService.checkLanguage(request.Value<string>("RoleID"), "Name", request.Value<string>("LanguageCode"), request.Value<String>("LanguageLibID")))
                    {
                        bool IsDefault = request.Value<bool>("IsDefault");
                        try
                        {
                            SYS_LanguageLibService.UpdateLanguage(userid, request.Value<string>("Name"), request.Value<string>("LanguageCode"), IsDefault, request.Value<string>("RoleID"), "Name");
                            SYS_LanguageLibService.UpdateLanguage(userid, request.Value<string>("Description"), request.Value<string>("LanguageCode"), IsDefault, request.Value<string>("RoleID"), "Description");
                            success++;
                        }
                        catch (Exception ex)
                        {
                            DataLogerService.writeerrlog(ex);
                            failIDs = UtilBussinessService.str(failIDs, request.Value<string>("LanguageLibID"));
                            fail++;
                        }
                    }
                    else
                    {
                        failIDs = UtilBussinessService.str(failIDs, request.Value<string>("LanguageLibID"));
                        msg = UtilBussinessService.str(msg, "已存在语序");
                        fail++;
                    }
                }
            }
            catch (Exception ex)
            {
                DataLogerService.writeerrlog(ex);
            }
            return new { success = success, fail = fail, failIDs = failIDs };
        }

        /// <summary>
        /// 删除角色语序
        /// Tom 2017年1月17日09:39:52
        /// </summary>
        /// <param name="jArray"></param>
        /// <returns></returns>
        public static object Inf00004Languagedelete(JArray jArray)
        {
            int success = 0;
            int fail = 0;
            string failIDs = null;
            JObject request = null;
            for (int i = 0; i < jArray.Count; i++)
            {
                request = (JObject)jArray[i];

                string LanguageCode = request.Value<string>("LanguageCode");
                string rowid = request.Value<string>("rowid");
                if (SYS_LanguageLibService.DeleteLanguage(LanguageCode, rowid))
                    success++;
                else
                {
                    failIDs = UtilBussinessService.str(failIDs, request.Value<string>("LanguageLibID"));
                    fail++;
                }
            }
            return new { success = success, fail = fail, failIDs = failIDs };
        }

        /// <summary>
        /// 获取语序列表
        /// SAM 2017年4月28日10:17:42
        /// </summary>
        /// <param name="Token">授权码</param>
        /// <param name="name"></param>
        /// <param name="page">页码</param>
        /// <param name="rows">行数</param>
        /// <returns></returns>
        public static object GetLanguageList(string token, string name, int page, int rows)
        {
            int count = 0;
            return UtilBussinessService.getPaginationModel(SYS_ParameterService.GetLanguageList(name, page, rows, ref count), count);
        }
        #endregion

        #region Inf00005部门主档
        /// <summary>
        /// 部门主档列表
        /// SAM 2017年5月18日23:29:35
        /// </summary>
        /// <param name="Token">授权码</param>
        /// <param name="Code">代号</param>
        /// <param name="Status">状态</param>
        /// <param name="page">页码</param>
        /// <param name="rows">行数</param>
        /// <returns></returns>
        public static object Inf00005GetList(string token, string code, string status, int page, int rows)
        {
            int count = 0;
            return UtilBussinessService.getPaginationModel(SYS_OrganizationService.Inf00005GetList(code, status, page, rows, ref count), count);
        }

        /// <summary>
        ///  部门的单一删除
        ///  SAM 2017年7月25日11:07:46
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public static object Inf00005Delete(JObject request)
        {
            string Token = request.Value<string>("Token");
            string userid = UtilBussinessService.detoken(Token);

            SYS_Organization model = SYS_OrganizationService.get(request.Value<string>("OrganizationID"));
            if (model == null)
                return new { status = "410", msg = "不存在的部门信息！" };

            if (SYS_OrganizationService.CheckParent(model.OrganizationID))
                return new { status = "410", msg = model.Code + "已使用，无法删除！" };

            if (SYS_WorkCenterService.CheckOrganization(model.OrganizationID))
                return new { status = "410", msg = model.Code + "已使用，无法删除！" };

            if (EMS_EquipmentService.CheckOrganization(model.OrganizationID))
                return new { status = "410", msg = model.Code + "已使用，无法删除！" };

            model.Status = 2;

            if (SYS_OrganizationService.update(userid, model))
            {
                //根据部门删除部门-班别信息
                //SYS_OrganizationClassService.deleteByDept(model.OrganizationID);
                return new { status = "200", msg = "删除成功！" };
            }
            else
                return new { status = "410", msg = "删除失败！" };
        }


        /// <summary>
        /// 部门删除
        /// SAM 2017年5月18日23:30:21
        /// </summary>
        /// <param name="Token">授权码</param>
        /// <param name="jArray"></param>
        /// <returns></returns>
        public static object Inf00005delete(string Token, JArray jArray)
        {
            int success = 0;
            int fail = 0;
            string failIDs = null;
            string msg = null;
            string userid = UtilBussinessService.detoken(Token);
            JObject data = null;
            SYS_Organization model = null;
            for (int i = 0; i < jArray.Count; i++)
            {
                data = (JObject)jArray[i];
                model = SYS_OrganizationService.get(data.Value<string>("OrganizationID"));
                if (!SYS_OrganizationService.CheckParent(data.Value<string>("OrganizationID")) && !SYS_OrganizationService.CheckUserMapping(data.Value<string>("OrganizationID")) && !SYS_OrganizationService.CheckRoleMapping(data.Value<string>("OrganizationID")))
                {
                    model.Status = 2;
                    if (SYS_OrganizationService.update(userid, model))
                        success++;
                    else
                    {
                        failIDs = UtilBussinessService.str(failIDs, data.Value<string>("OrganizationID"));
                        fail++;
                    }
                }
                else
                {
                    failIDs = UtilBussinessService.str(failIDs, data.Value<string>("OrganizationID"));
                    msg = UtilBussinessService.str(failIDs, model.Code + "已使用，不能删除");
                    fail++;
                }

            }
            return new { success = success, fail = fail, failIDs = failIDs, msg = msg };
        }

        /// <summary>
        /// 部门新增
        /// SAM 2017年5月18日23:30:28
        /// </summary>
        /// <param name="Token">授权码</param>
        /// <param name="jArray"></param>
        /// <returns></returns>
        public static object Inf00005insert(string Token, JArray jArray)
        {
            int success = 0;
            int fail = 0;
            string failIDs = null;
            string msg = null;
            string userid = UtilBussinessService.detoken(Token);
            JObject data = null;
            SYS_Organization model = null;
            bool IfTop = true;
            for (int i = 0; i < jArray.Count; i++)
            {
                data = (JObject)jArray[i];
                IfTop = true;
                if (!SYS_OrganizationService.CheckCode(data.Value<string>("Code"), data.Value<string>("Type"), null))
                {
                    model = new SYS_Organization();
                    model.OrganizationID = UniversalService.GetSerialNumber("SYS_Organization");
                    model.Code = data.Value<string>("Code");
                    model.Name = data.Value<string>("Name");
                    model.Type = data.Value<string>("Type");
                    model.Status = data.Value<int>("Status");
                    model.Sequence = string.IsNullOrWhiteSpace(data.Value<string>("Sequence")) ? 0 : data.Value<int>("Sequence");
                    model.PlantID = data.Value<string>("PlantID");
                    model.IfTop = data.Value<bool>("IfTop");
                    if (model.IfTop)
                        model.ParentOrganizationID = model.OrganizationID;
                    else
                        model.ParentOrganizationID = data.Value<string>("ParentOrganizationID");
                    model.Comments = data.Value<string>("Comments");
                    if (!string.IsNullOrWhiteSpace(model.PlantID))
                    {
                        if (model.IfTop)
                        {
                            if (SYS_OrganizationService.CheckIfTop(model.PlantID, null))
                                IfTop = false;
                        }
                    }

                    if (IfTop)
                    {
                        if (SYS_OrganizationService.insert(userid, model))
                            success++;
                        else
                        {
                            failIDs = UtilBussinessService.str(failIDs, data.Value<string>("OrganizationID"));
                            fail++;
                        }
                    }
                    else
                    {
                        failIDs = UtilBussinessService.str(failIDs, data.Value<string>("OrganizationID"));
                        msg = UtilBussinessService.str(msg, "归属厂别已存在最上层部门");
                        fail++;
                    }
                }
                else
                {
                    failIDs = UtilBussinessService.str(failIDs, data.Value<string>("OrganizationID"));
                    msg = UtilBussinessService.str(msg, data.Value<string>("Code") + "已存在");
                    fail++;
                }
            }
            return new { success = success, fail = fail, failIDs = failIDs, msg = msg };
        }

        /// <summary>
        /// 部门更新
        /// SAM 2017年5月18日23:30:39
        /// </summary>
        /// <param name="Token">授权码</param>
        /// <param name="jArray"></param>
        /// <returns></returns>
        public static object Inf00005update(string Token, JArray jArray)
        {
            int success = 0;
            int fail = 0;
            string failIDs = null;
            string msg = null;
            string userid = UtilBussinessService.detoken(Token);
            JObject data = null;
            SYS_Organization model = null;
            bool IfTop = true;
            for (int i = 0; i < jArray.Count; i++)
            {
                data = (JObject)jArray[i];
                IfTop = true;
                model = SYS_OrganizationService.get(data.Value<string>("OrganizationID"));
                model.Name = data.Value<string>("Name");
                model.Comments = data.Value<string>("Comments");
                model.Status = data.Value<int>("Status");
                model.PlantID = data.Value<string>("PlantID");
                model.IfTop = data.Value<bool>("IfTop");
                if (model.IfTop)
                    model.ParentOrganizationID = model.OrganizationID;
                else
                    model.ParentOrganizationID = data.Value<string>("ParentOrganizationID");

                if (!string.IsNullOrWhiteSpace(model.PlantID))
                {
                    if (model.IfTop)
                    {
                        if (SYS_OrganizationService.CheckIfTop(model.PlantID, data.Value<string>("OrganizationID")))
                            IfTop = false;
                    }
                }

                if (model.Status == 0)
                {
                    if (!SYS_OrganizationService.CheckParent(data.Value<string>("OrganizationID")) && !SYS_OrganizationService.CheckUserMapping(data.Value<string>("OrganizationID")) && !SYS_OrganizationService.CheckRoleMapping(data.Value<string>("OrganizationID")))
                    {
                        if (IfTop)
                        {
                            if (SYS_OrganizationService.update(userid, model))
                                success++;
                            else
                            {
                                failIDs = UtilBussinessService.str(failIDs, data.Value<string>("OrganizationID"));
                                fail++;
                            }
                        }
                        else
                        {
                            failIDs = UtilBussinessService.str(failIDs, data.Value<string>("OrganizationID"));
                            msg = UtilBussinessService.str(msg, "归属厂别已存在最上层部门");
                            fail++;
                        }
                    }
                    else
                    {
                        failIDs = UtilBussinessService.str(failIDs, data.Value<string>("OrganizationID"));
                        msg = UtilBussinessService.str(failIDs, model.Code + "已使用，不能作废");
                        fail++;
                    }
                }
                else
                {
                    if (IfTop)
                    {
                        if (SYS_OrganizationService.update(userid, model))
                            success++;
                        else
                        {
                            failIDs = UtilBussinessService.str(failIDs, data.Value<string>("OrganizationID"));
                            fail++;
                        }
                    }
                    else
                    {
                        failIDs = UtilBussinessService.str(failIDs, data.Value<string>("OrganizationID"));
                        msg = UtilBussinessService.str(msg, "归属厂别已存在最上层部门");
                        fail++;
                    }
                }


            }
            return new { success = success, fail = fail, failIDs = failIDs, msg = msg };
        }

        /// <summary>
        /// 根据厂别获取他下面所有正常的部门
        /// SAM 2017年5月19日10:49:51
        /// </summary>
        /// <param name="Token">授权码</param>
        /// <param name="PlantID"></param>
        /// <returns></returns>
        public static object Inf00005GetDeptList(string token, string PlantCode)
        {
            return SYS_OrganizationService.Inf00005GetDeptList(PlantCode);
        }

        /// <summary>
        /// 根据部门流水号获取班别列表
        /// SAM 2017年7月25日11:33:08
        /// </summary>
        /// <param name="token"></param>
        /// <param name="OrganizationID"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <returns></returns>
        public static object Inf00005GetClassList(string token, string OrganizationID, int page, int rows)
        {
            int count = 0;
            return UtilBussinessService.getPaginationModel(SYS_OrganizationClassService.Inf00005GetClassList(OrganizationID, page, rows, ref count), count);
        }

        /// <summary>
        /// 根据部门流水号获取班别（不分页）
        /// SAM 2017年7月25日11:33:17
        /// </summary>
        /// <param name="token"></param>
        /// <param name="OrganizationID"></param>
        /// <returns></returns>
        public static object Inf00005ClassList(string token, string OrganizationID)
        {
            return SYS_OrganizationClassService.Inf00005ClassList(OrganizationID);
        }

        /// <summary>
        /// 根据部门流水号获取不属于他的班别（不分页）
        /// SAM 2017年7月25日11:33:31
        /// </summary>
        /// <param name="token"></param>
        /// <param name="OrganizationID"></param>
        /// <returns></returns>
        public static object Inf00005NoClassList(string token, string OrganizationID)
        {
            return SYS_ClassService.Inf00005NoClassList(OrganizationID);
        }

        /// <summary>
        /// 保存部门的班别列表
        /// SAM 2017年7月25日11:33:42
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public static object Inf00005ClassSave(JObject request)
        {
            string Token = request.Value<string>("Token");
            string userid = UtilBussinessService.detoken(Token);
            string OrganizationID = request.Value<string>("OrganizationID");
            JArray List = request.Value<JArray>("data");

            if (string.IsNullOrWhiteSpace(OrganizationID))
                return new { status = "410", msg = "OrganizationID为空!" };

            JObject data = null;
            Hashtable AddModel = null;
            SYS_OrganizationClass model = null;
            string New = null;
            List<Hashtable> Add = new List<Hashtable>();
            for (int i = 0; i < List.Count; i++) //将新增的和已存在的分开
            {
                data = (JObject)List[i];
                AddModel = new Hashtable();
                if (string.IsNullOrWhiteSpace(data.Value<string>("OrganizationClassID")))
                {
                    AddModel["ClassID"] = data.Value<string>("ClassID");
                    AddModel["OrganizationID"] = OrganizationID;
                    Add.Add(AddModel);
                }
                else
                {
                    if (New == null)
                        New = data.Value<string>("OrganizationClassID");
                    else
                        New = New + "','" + data.Value<string>("OrganizationClassID");
                }
            }
            SYS_OrganizationClassService.Delete(userid, New, OrganizationID);

            foreach (Hashtable item in Add) //循环添加新增的
            {
                model = new SYS_OrganizationClass();
                model.OrganizationClassID = UniversalService.GetSerialNumber("SYS_OrganizationClass");
                model.OrganizationID = OrganizationID;
                model.ClassID = item["ClassID"].ToString();
                model.Status = Framework.SystemID + "0201213000001";
                if (!SYS_OrganizationClassService.CheckClass(model.ClassID, model.OrganizationID))
                    SYS_OrganizationClassService.insert(userid, model);
            }

            return new { status = "200", msg = "保存成功！" };
        }


        #endregion

        #region Inf00007客户主档
        /// <summary>
        /// 获取客户主档列表
        /// SAM 2017年4月27日10:09:22
        /// </summary>
        /// <param name="Token">授权码</param>
        /// <param name="Code">代号</param>
        /// <param name="Name"></param>
        /// <param name="page">页码</param>
        /// <param name="rows">行数</param>
        /// <returns></returns>
        public static object Inf00007getList(string token, string Code, string Name, int page, int rows)
        {
            int count = 0;
            return UtilBussinessService.getPaginationModel(SYS_CustomersService.Inf00007getList(Code, Name, page, rows, ref count), count);
        }

        /// <summary>
        /// 客户添加
        /// SAM 2017年4月26日14:49:09
        /// </summary>
        /// <param name="Token">授权码</param>
        /// <param name="jArray"></param>
        /// <returns></returns>
        public static object Inf00007insert(string Token, JArray jArray)
        {
            int success = 0;
            int fail = 0;
            string failIDs = null;
            string msg = null;
            string userid = UtilBussinessService.detoken(Token);
            JObject data = null;
            SYS_Customers model = null;
            for (int i = 0; i < jArray.Count; i++)
            {
                data = (JObject)jArray[i];
                if (!SYS_CustomersService.CheckCode(data.Value<string>("Code"), null))
                {
                    model = new SYS_Customers();
                    model.CustomerID = UniversalService.GetSerialNumber("SYS_Customers");
                    model.Code = data.Value<string>("Code");
                    model.Name = data.Value<string>("Name");
                    model.Contacts = data.Value<string>("Contacts");
                    model.Email = data.Value<string>("Email");
                    model.MESUserID = data.Value<string>("MESUserID");
                    model.ClassOne = data.Value<string>("ClassOne");
                    model.ClassTwo = data.Value<string>("ClassTwo");
                    model.Status = data.Value<string>("Status");
                    model.Comments = data.Value<string>("Comments");
                    if (SYS_CustomersService.insert(userid, model))
                        success++;
                    else
                    {
                        failIDs = UtilBussinessService.str(failIDs, data.Value<string>("CustomerID"));
                        fail++;
                    }
                }
                else
                {
                    failIDs = UtilBussinessService.str(failIDs, data.Value<string>("CustomerID"));
                    msg = UtilBussinessService.str(msg, data.Value<string>("Code") + "已存在");
                    fail++;
                }
            }
            return new { success = success, fail = fail, failIDs = failIDs, msg = msg };
        }

        /// <summary>
        /// 客户更新
        /// SAM 2017年3月14日17:37:43
        /// </summary>
        /// <param name="Token">授权码</param>
        /// <param name="jArray"></param>
        /// <returns></returns>
        public static object Inf00007update(string Token, JArray jArray)
        {
            int success = 0;
            int fail = 0;
            string failIDs = null;
            string msg = null;
            string userid = UtilBussinessService.detoken(Token);
            JObject data = null;
            SYS_Customers model = null;
            for (int i = 0; i < jArray.Count; i++)
            {
                data = (JObject)jArray[i];
                model = SYS_CustomersService.get(data.Value<string>("CustomerID"));
                model.Name = data.Value<string>("Name");
                model.Contacts = data.Value<string>("Contacts");
                model.Email = data.Value<string>("Email");
                model.MESUserID = data.Value<string>("MESUserID");
                model.ClassOne = data.Value<string>("ClassOne");
                model.ClassTwo = data.Value<string>("ClassTwo");
                model.Status = data.Value<string>("Status");
                model.Comments = data.Value<string>("Comments");
                if (SYS_CustomersService.update(userid, model))
                    success++;
                else
                {
                    failIDs = UtilBussinessService.str(failIDs, data.Value<string>("CustomerID"));
                    fail++;
                }


            }
            return new { success = success, fail = fail, failIDs = failIDs, msg = msg };
        }

        /// <summary>
        /// 客户的删除
        /// SAM 2017年7月27日21:43:50
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public static object Inf00007Delete(JObject request)
        {
            string Token = request.Value<string>("Token");
            string userid = UtilBussinessService.detoken(Token);
            string CustomerID = request.Value<string>("CustomerID");
            SYS_Customers model = SYS_CustomersService.get(CustomerID);
            if (model == null)
                return new { status = "410", msg = "删除失败！不存在的客户信息" };

            if (SFC_FabricatedMotherService.CheckCust(CustomerID))
                return new { status = "410", msg = model.Code + "已使用,无法删除！" };

            model.Status = Framework.SystemID + "0201213000003";
            if (SYS_CustomersService.update(userid, model))
                return new { status = "200", msg = "删除成功！" };
            else
                return new { status = "410", msg = "删除失败！" };
        }

        /// <summary>
        /// 获取指定客户的语序
        /// SAM 2017年4月27日10:29:43
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static object Inf00007GetLanguageList(string id)
        {
            return SYS_LanguageLibService.Inf00007GetLanguageList(id);
        }

        /// <summary>
        /// 添加客户语序
        /// SAM 2017年4月27日10:32:40
        /// </summary>
        /// <param name="Token">授权码</param>
        /// <param name="jArray"></param>
        /// <returns></returns>
        public static object Inf00007Languageinsert(string Token, JArray jArray)
        {
            int success = 0;
            int fail = 0;
            string failIDs = null;
            string msg = null;
            string userid = UtilBussinessService.detoken(Token);
            JObject request = null;
            for (int i = 0; i < jArray.Count; i++)
            {
                request = (JObject)jArray[i];
                if (!SYS_LanguageLibService.checkLanguage(request.Value<string>("RowID"), "Code", request.Value<string>("LanguageCode"), null))
                {
                    SYS_LanguageLib lan = new SYS_LanguageLib();
                    lan.LanguageLibID = UniversalService.GetSerialNumber("SYS_LanguageLib");
                    lan.TableID = "31";
                    lan.RowID = request.Value<string>("RowID");
                    lan.Field = "Code";
                    lan.OriginalLanguage = request.Value<string>("OriginalLanguage");
                    lan.OriginalContent = request.Value<string>("OriginalContent");
                    lan.LanguageCode = request.Value<string>("LanguageCode");
                    lan.LanguageContentOne = request.Value<string>("Code");
                    lan.IsDefault = request.Value<bool>("IsDefault");
                    if (SYS_LanguageLibService.insert(userid, lan))
                    {
                        lan.LanguageLibID = UniversalService.GetSerialNumber("SYS_LanguageLib");
                        lan.Field = "Name";
                        lan.LanguageCode = request.Value<string>("LanguageCode");
                        lan.LanguageContentOne = request.Value<string>("Name");
                        SYS_LanguageLibService.insert(userid, lan);
                        lan.LanguageLibID = UniversalService.GetSerialNumber("SYS_LanguageLib");
                        lan.Field = "Comments";
                        lan.LanguageCode = request.Value<string>("LanguageCode");
                        lan.LanguageContentOne = request.Value<string>("Comments");
                        SYS_LanguageLibService.insert(userid, lan);
                        success++;
                    }
                    else
                    {
                        failIDs = UtilBussinessService.str(failIDs, request.Value<string>("LanguageLibID"));
                        fail++;
                    }
                }
                else
                {
                    failIDs = UtilBussinessService.str(failIDs, request.Value<string>("LanguageLibID"));
                    msg = UtilBussinessService.str(msg, "语序已存在");
                    fail++;
                }
            }

            return new { success = success, fail = fail, failIDs = failIDs };
        }

        /// <summary>
        /// 更新客户语序
        /// SAM 2017年4月27日10:32:40
        /// </summary>
        /// <param name="Token">授权码</param>
        /// <param name="jArray"></param>
        /// <returns></returns>
        public static object Inf00007Languageupdate(string Token, JArray jArray)
        {
            int success = 0;
            int fail = 0;
            string failIDs = null;
            string msg = null;
            string userid = UtilBussinessService.detoken(Token);
            JObject request = null;
            try
            {
                for (int i = 0; i < jArray.Count; i++)
                {
                    request = (JObject)jArray[i];
                    if (!SYS_LanguageLibService.checkLanguage(request.Value<string>("RowID"), "Code", request.Value<string>("LanguageCode"), request.Value<String>("LanguageLibID")))
                    {
                        bool IsDefault = request.Value<bool>("IsDefault");
                        try
                        {
                            SYS_LanguageLibService.UpdateLanguage(userid, request.Value<string>("Code"), request.Value<string>("LanguageCode"), IsDefault, request.Value<string>("RowID"), "Code");
                            SYS_LanguageLibService.UpdateLanguage(userid, request.Value<string>("Name"), request.Value<string>("LanguageCode"), IsDefault, request.Value<string>("RowID"), "Name");
                            SYS_LanguageLibService.UpdateLanguage(userid, request.Value<string>("Comments"), request.Value<string>("LanguageCode"), IsDefault, request.Value<string>("RowID"), "Comments");
                            success++;
                        }
                        catch (Exception ex)
                        {
                            DataLogerService.writeerrlog(ex);
                            failIDs = UtilBussinessService.str(failIDs, request.Value<string>("LanguageLibID"));
                            fail++;
                        }
                    }
                    else
                    {
                        failIDs = UtilBussinessService.str(failIDs, request.Value<string>("LanguageLibID"));
                        msg = UtilBussinessService.str(msg, "语序已存在");
                        fail++;
                    }
                }
            }
            catch (Exception ex)
            {
                DataLogerService.writeerrlog(ex);
            }
            return new { success = success, fail = fail, failIDs = failIDs };
        }

        /// <summary>
        /// 删除客户语序
        /// SAM 2017年4月27日10:32:40
        /// </summary>
        /// <param name="jArray"></param>
        /// <returns></returns>
        public static object Inf00007Languagedelete(JArray jArray)
        {
            int success = 0;
            int fail = 0;
            string failIDs = null;
            JObject request = null;
            for (int i = 0; i < jArray.Count; i++)
            {
                request = (JObject)jArray[i];

                string LanguageCode = request.Value<string>("LanguageCode");
                string rowid = request.Value<string>("RowID");
                if (SYS_LanguageLibService.DeleteLanguage(LanguageCode, rowid))
                    success++;
                else
                {
                    failIDs = UtilBussinessService.str(failIDs, request.Value<string>("LanguageLibID"));
                    fail++;
                }
            }
            return new { success = success, fail = fail, failIDs = failIDs };
        }

        #endregion

        #region Inf00008厂商主档

        /// <summary>
        /// 获取厂商主档列表
        /// SAM 2017年4月27日11:43:15
        /// </summary>
        /// <param name="Token">授权码</param>
        /// <param name="Code">代号</param>
        /// <param name="Name"></param>
        /// <param name="page">页码</param>
        /// <param name="rows">行数</param>
        /// <returns></returns>
        public static object Inf00008getList(string token, string Code, string Name, int page, int rows)
        {
            int count = 0;
            return UtilBussinessService.getPaginationModel(SYS_ManufacturersService.Inf00008getList(Code, Name, page, rows, ref count), count);
        }

        /// <summary>
        /// 厂商删除
        /// SAM 2017年4月27日11:43:12
        /// </summary>
        /// <param name="Token">授权码</param>
        /// <param name="jArray"></param>
        /// <returns></returns>
        public static object Inf00008delete(string Token, JArray jArray)
        {
            int success = 0;
            int fail = 0;
            string failIDs = null;
            string msg = null;
            string userid = UtilBussinessService.detoken(Token);
            JObject data = null;
            SYS_Manufacturers model = null;
            for (int i = 0; i < jArray.Count; i++)
            {
                data = (JObject)jArray[i];
                model = SYS_ManufacturersService.get(data.Value<string>("ManufacturerID"));
                model.Status = Framework.SystemID + "0201213000003";
                if (SYS_ManufacturersService.update(userid, model))
                    success++;
                else
                {
                    failIDs = UtilBussinessService.str(failIDs, data.Value<string>("ManufacturerID"));
                    fail++;
                }

            }
            return new { success = success, fail = fail, failIDs = failIDs, msg = msg };
        }


        /// <summary>
        /// 厂商添加
        /// SAM 2017年4月27日11:37:41
        /// </summary>
        /// <param name="Token">授权码</param>
        /// <param name="jArray"></param>
        /// <returns></returns>
        public static object Inf00008insert(string Token, JArray jArray)
        {
            int success = 0;
            int fail = 0;
            string failIDs = null;
            string msg = null;
            string userid = UtilBussinessService.detoken(Token);
            JObject data = null;
            SYS_Manufacturers model = null;
            for (int i = 0; i < jArray.Count; i++)
            {
                data = (JObject)jArray[i];
                if (!SYS_ManufacturersService.CheckCode(data.Value<string>("Code"), null))
                {
                    model = new SYS_Manufacturers();
                    model.ManufacturerID = UniversalService.GetSerialNumber("SYS_Manufacturers");
                    model.Type = data.Value<string>("Type");
                    model.Code = data.Value<string>("Code");
                    model.Name = data.Value<string>("Name");
                    model.Contacts = data.Value<string>("Contacts");
                    model.Email = data.Value<string>("Email");
                    model.MESUserID = data.Value<string>("MESUserID");
                    model.ClassOne = data.Value<string>("ClassOne");
                    model.ClassTwo = data.Value<string>("ClassTwo");
                    model.Status = data.Value<string>("Status");
                    model.Comments = data.Value<string>("Comments");
                    if (SYS_ManufacturersService.insert(userid, model))
                        success++;
                    else
                    {
                        failIDs = UtilBussinessService.str(failIDs, data.Value<string>("ManufacturerID"));
                        fail++;
                    }
                }
                else
                {
                    failIDs = UtilBussinessService.str(failIDs, data.Value<string>("ManufacturerID"));
                    msg = UtilBussinessService.str(msg, data.Value<string>("Code") + "已存在");
                    fail++;
                }
            }
            return new { success = success, fail = fail, failIDs = failIDs, msg = msg };
        }

        /// <summary>
        /// 厂商更新
        /// SAM 2017年4月27日11:35:05
        /// </summary>
        /// <param name="Token">授权码</param>
        /// <param name="jArray"></param>
        /// <returns></returns>
        public static object Inf00008update(string Token, JArray jArray)
        {
            int success = 0;
            int fail = 0;
            string failIDs = null;
            string msg = null;
            string userid = UtilBussinessService.detoken(Token);
            JObject data = null;
            SYS_Manufacturers model = null;
            for (int i = 0; i < jArray.Count; i++)
            {
                data = (JObject)jArray[i];
                model = SYS_ManufacturersService.get(data.Value<string>("ManufacturerID"));

                model.Contacts = data.Value<string>("Contacts");
                model.Email = data.Value<string>("Email");
                model.MESUserID = data.Value<string>("MESUserID");
                model.ClassOne = data.Value<string>("ClassOne");
                model.ClassTwo = data.Value<string>("ClassTwo");
                model.Status = data.Value<string>("Status");

                model.Name = data.Value<string>("Name");
                model.Comments = data.Value<string>("Comments");
                if (SYS_ManufacturersService.update(userid, model))
                    success++;
                else
                {
                    failIDs = UtilBussinessService.str(failIDs, data.Value<string>("ManufacturerID"));
                    fail++;
                }


            }
            return new { success = success, fail = fail, failIDs = failIDs, msg = msg };
        }

        /// <summary>
        /// 获取指定厂商的语序
        /// SAM 2017年4月27日10:29:43
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static object Inf00008GetLanguageList(string id)
        {
            return SYS_LanguageLibService.Inf00008GetLanguageList(id);
        }

        /// <summary>
        /// 添加厂商语序
        /// SAM 2017年4月27日10:32:40
        /// </summary>
        /// <param name="Token">授权码</param>
        /// <param name="jArray"></param>
        /// <returns></returns>
        public static object Inf00008Languageinsert(string Token, JArray jArray)
        {
            int success = 0;
            int fail = 0;
            string failIDs = null;
            string msg = null;
            string userid = UtilBussinessService.detoken(Token);
            JObject request = null;
            for (int i = 0; i < jArray.Count; i++)
            {
                request = (JObject)jArray[i];
                if (!SYS_LanguageLibService.checkLanguage(request.Value<string>("RowID"), "Code", request.Value<string>("LanguageCode"), null))
                {
                    SYS_LanguageLib lan = new SYS_LanguageLib();
                    lan.LanguageLibID = UniversalService.GetSerialNumber("SYS_LanguageLib");
                    lan.TableID = "32";
                    lan.RowID = request.Value<string>("RowID");
                    lan.Field = "Code";
                    lan.OriginalLanguage = request.Value<string>("OriginalLanguage");
                    lan.OriginalContent = request.Value<string>("OriginalContent");
                    lan.LanguageCode = request.Value<string>("LanguageCode");
                    lan.LanguageContentOne = request.Value<string>("Code");
                    lan.IsDefault = request.Value<bool>("IsDefault");
                    if (SYS_LanguageLibService.insert(userid, lan))
                    {
                        lan.LanguageLibID = UniversalService.GetSerialNumber("SYS_LanguageLib");
                        lan.Field = "Name";
                        lan.LanguageCode = request.Value<string>("LanguageCode");
                        lan.LanguageContentOne = request.Value<string>("Name");
                        SYS_LanguageLibService.insert(userid, lan);
                        lan.LanguageLibID = UniversalService.GetSerialNumber("SYS_LanguageLib");
                        lan.Field = "Comments";
                        lan.LanguageCode = request.Value<string>("LanguageCode");
                        lan.LanguageContentOne = request.Value<string>("Comments");
                        SYS_LanguageLibService.insert(userid, lan);
                        success++;
                    }
                    else
                    {
                        failIDs = UtilBussinessService.str(failIDs, request.Value<string>("LanguageLibID"));
                        fail++;
                    }
                }
                else
                {
                    failIDs = UtilBussinessService.str(failIDs, request.Value<string>("LanguageLibID"));
                    msg = UtilBussinessService.str(msg, "语序已存在");
                    fail++;
                }
            }

            return new { success = success, fail = fail, failIDs = failIDs };
        }

        /// <summary>
        /// 更新厂商语序
        /// SAM 2017年4月27日11:33:51
        /// </summary>
        /// <param name="Token">授权码</param>
        /// <param name="jArray"></param>
        /// <returns></returns>
        public static object Inf00008Languageupdate(string Token, JArray jArray)
        {
            int success = 0;
            int fail = 0;
            string failIDs = null;
            string msg = null;
            string userid = UtilBussinessService.detoken(Token);
            JObject request = null;
            try
            {
                for (int i = 0; i < jArray.Count; i++)
                {
                    request = (JObject)jArray[i];
                    if (!SYS_LanguageLibService.checkLanguage(request.Value<string>("RowID"), "Code", request.Value<string>("LanguageCode"), request.Value<String>("LanguageLibID")))
                    {
                        bool IsDefault = request.Value<bool>("IsDefault");
                        try
                        {
                            SYS_LanguageLibService.UpdateLanguage(userid, request.Value<string>("Code"), request.Value<string>("LanguageCode"), IsDefault, request.Value<string>("RowID"), "Code");
                            SYS_LanguageLibService.UpdateLanguage(userid, request.Value<string>("Name"), request.Value<string>("LanguageCode"), IsDefault, request.Value<string>("RowID"), "Name");
                            SYS_LanguageLibService.UpdateLanguage(userid, request.Value<string>("Comments"), request.Value<string>("LanguageCode"), IsDefault, request.Value<string>("RowID"), "Comments");
                            success++;
                        }
                        catch (Exception ex)
                        {
                            DataLogerService.writeerrlog(ex);
                            failIDs = UtilBussinessService.str(failIDs, request.Value<string>("LanguageLibID"));
                            fail++;
                        }
                    }
                    else
                    {
                        failIDs = UtilBussinessService.str(failIDs, request.Value<string>("LanguageLibID"));
                        msg = UtilBussinessService.str(msg, "语序已存在");
                        fail++;
                    }
                }
            }
            catch (Exception ex)
            {
                DataLogerService.writeerrlog(ex);
            }
            return new { success = success, fail = fail, failIDs = failIDs };
        }

        /// <summary>
        /// 删除厂商语序
        /// SAM 2017年4月27日11:32:45
        /// </summary>
        /// <param name="jArray"></param>
        /// <returns></returns>
        public static object Inf00008Languagedelete(JArray jArray)
        {
            int success = 0;
            int fail = 0;
            string failIDs = null;
            JObject request = null;
            for (int i = 0; i < jArray.Count; i++)
            {
                request = (JObject)jArray[i];

                string LanguageCode = request.Value<string>("LanguageCode");
                string rowid = request.Value<string>("RowID");
                if (SYS_LanguageLibService.DeleteLanguage(LanguageCode, rowid))
                    success++;
                else
                {
                    failIDs = UtilBussinessService.str(failIDs, request.Value<string>("LanguageLibID"));
                    fail++;
                }
            }
            return new { success = success, fail = fail, failIDs = failIDs };
        }

        /// <summary>
        /// 删除单个厂商功能
        /// Mouse 2017年7月24日11:49:01
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public static object Inf00008ManufacturerDelete(JObject request)
        {
            string Token = request.Value<string>("Token");
            string userid = UtilBussinessService.detoken(Token);
            string ManufacturerID = request.Value<string>("ManufacturerID");

            SYS_Manufacturers model = SYS_ManufacturersService.get(ManufacturerID);
            if (model == null)
                return new { status = "410", msg = "删除失败！不存在的厂别信息" };

            model.Status = Framework.SystemID + "0201213000003";

            if (SYS_ManufacturersService.update(userid, model))
                return new { status = "200", msg = "删除成功！" };
            else
                return new { status = "410", msg = "删除失败！" };
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
        public static object Inf00009GroupCodeList(string token, string code, int? status, int page, int rows)
        {
            int count = 0;
            return UtilBussinessService.getPaginationModel(SYS_ParameterService.Inf00009GroupCodeList(code, status, page, rows, ref count), count);
        }

        /// <summary>
        /// 更新分类群组码
        /// Tom 2017年4月28日15:48:59
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="jArray"></param>
        /// <returns></returns>
        public static object Inf00009GroupCodeUpdate(string userID, JArray jArray)
        {
            int success = 0;
            int fail = 0;
            string failIDs = null;
            string msg = null;
            string userid = userID;
            JObject data = null;
            SYS_Parameters par = null;
            for (int i = 0; i < jArray.Count; i++)
            {
                data = (JObject)jArray[i];

                par = SYS_ParameterService.get(data.Value<string>("ParameterID"));
                par.Code = data.Value<string>("Code");
                par.Name = data.Value<string>("Name");
                par.IsEnable = data.Value<int>("Status"); //状态
                par.Comments = data.Value<string>("Comments");
                if (SYS_ParameterService.update(userid, par))
                    success++;
                else
                {
                    failIDs = UtilBussinessService.str(failIDs, par.ParameterID);
                    fail++;
                }

            }
            return new { success = success, fail = fail, failIDs = failIDs, msg = msg };
        }

        /// <summary>
        /// 单一删除分类群组码
        /// Joint 2017年7月24日17:14:09
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public static object Inf00009GroupCodeDeleted(JObject request)
        {
            string Token = request.Value<string>("Token");
            string userid = UtilBussinessService.detoken(Token);
            SYS_Parameters model = SYS_ParameterService.get(request.Value<string>("ParameterID"));
            if (model == null)
                return new { status = "410", msg = "不存在的分类群组码" };
            if (SYS_ParameterService.inf00009Check(request.Value<string>("ParameterID")))
                return new { status = "410", msg = "代號已使用，不得刪除！" };
            if (SYS_ParameterService.delete(userid, request.Value<string>("ParameterID")))
                return new { status = "200", msg = "删除成功！" };
            else
                return new { status = "410", msg = "删除失败！" };

        }
        /// <summary>
        /// 添加分类群组码
        /// Tom 2017-04-28 15:49:04
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="jArray"></param>
        /// <returns></returns>
        public static object Inf00009GroupCodeInsert(string userID, JArray jArray)
        {
            int success = 0;
            int fail = 0;
            string failIDs = null;
            string msg = null;
            string userid = userID;
            string typeID = Framework.SystemID + "019121300000E";
            JObject data = null;
            SYS_Parameters par = null;
            for (int i = 0; i < jArray.Count; i++)
            {
                data = (JObject)jArray[i];
                if (!SYS_ParameterService.CheckParameter(data.Value<string>("Code"), null, null, typeID, null))
                {
                    par = new SYS_Parameters();
                    par.ParameterID = UniversalService.GetSerialNumber("SYS_Parameters");
                    par.ParameterTypeID = typeID;
                    par.Code = data.Value<string>("Code");  //代号
                    par.Name = data.Value<string>("Name");//说明
                    par.Comments = data.Value<string>("Comments");
                    par.IsEnable = data.Value<int>("Status"); //状态
                    par.IsDefault = false;
                    par.Sequence = 0;//暂时后台定死0
                    par.UsingType = 0;//这个字段暂未知有何用处
                    if (SYS_ParameterService.insert(userid, par))
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
        /// 删除分类群组码
        /// Tom 2017年4月28日15:49:30
        /// </summary>
        /// <param name="jArray"></param>
        /// <returns></returns>
        public static object Inf00009GroupCodeDelete(string userID, JArray jArray)
        {
            int success = 0;
            int fail = 0;
            string failIDs = null;
            JObject data = null;
            string msg = null;
            for (int i = 0; i < jArray.Count; i++)
            {
                data = (JObject)jArray[i];

                if (!SYS_ParameterService.inf00009Check(data.Value<string>("ParameterID")))
                {
                    if (SYS_ParameterService.delete(userID, data.Value<string>("ParameterID")))
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
                    msg = UtilBussinessService.str(msg, data.Value<string>("Code") + "代号已使用，不能删除");
                    fail++;
                }

            }
            return new { success = success, fail = fail, failIDs = failIDs, msg = msg };
        }

        /// <summary>
        /// 获取分类列表
        /// Tom 2017年5月2日17:57:26
        /// </summary>
        /// <param name="Token">授权码</param>
        /// <param name="Code">代号</param>
        /// <param name="Status">状态</param>
        /// <param name="page">页码</param>
        /// <param name="rows">行数</param>
        /// <returns></returns>
        public static object Inf00009ClassList(string token, string useCode, string code, int? status, int page, int rows)
        {
            int count = 0;
            return UtilBussinessService.getPaginationModel(SYS_ParameterService.Inf00009ClassList(useCode, code, status, page, rows, ref count), count);
        }

        /// <summary>
        /// 更新分类
        /// Tom 2017年5月2日17:57:48
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="jArray"></param>
        /// <returns></returns>
        public static object Inf00009ClassUpdate(string userID, JArray jArray)
        {
            int success = 0;
            int fail = 0;
            string failIDs = null;
            string msg = null;
            string userid = userID;
            JObject data = null;
            SYS_Parameters par = null;
            for (int i = 0; i < jArray.Count; i++)
            {
                data = (JObject)jArray[i];
                if (!SYS_ParameterService.CheckParameter(data.Value<string>("Code"), null, null, Framework.SystemID + "019121300000B", data.Value<string>("ParameterID")))
                {
                    par = SYS_ParameterService.get(data.Value<string>("ParameterID"));
                    par.Code = data.Value<string>("Code");  //代号
                    par.Name = data.Value<string>("Description");
                    par.DescriptionOne = data.Value<string>("GroupParameterID");// 群组
                    par.Description = data.Value<string>("UseParameterID");
                    par.Comments = data.Value<string>("Comments");
                    par.IsEnable = data.Value<int>("Status"); //状态
                    if (SYS_ParameterService.update(userid, par))
                        success++;
                    else
                    {
                        failIDs = UtilBussinessService.str(failIDs, par.ParameterID);
                        fail++;
                    }
                }
                else
                {
                    failIDs = UtilBussinessService.str(failIDs, par.ParameterID);
                    msg = UtilBussinessService.str(msg, data.Value<string>("Code") + "已存在");
                    fail++;
                }
            }
            return new { success = success, fail = fail, failIDs = failIDs, msg = msg };
        }

        /// <summary>
        /// 添加分类
        /// Tom 2017年5月2日17:58:01
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="jArray"></param>
        /// <returns></returns>
        public static object Inf00009ClassInsert(string userID, JArray jArray)
        {
            int success = 0;
            int fail = 0;
            string failIDs = null;
            string msg = null;
            string userid = userID;
            string typeID = Framework.SystemID + "019121300000B";
            JObject data = null;
            SYS_Parameters par = null;
            for (int i = 0; i < jArray.Count; i++)
            {
                data = (JObject)jArray[i];
                if (!SYS_ParameterService.CheckParameter(data.Value<string>("Code"), null, null, typeID, null))
                {
                    par = new SYS_Parameters();
                    par.ParameterID = UniversalService.GetSerialNumber("SYS_Parameters");
                    par.ParameterTypeID = typeID;
                    par.Code = data.Value<string>("Code");  //代号
                    par.Name = data.Value<string>("Description");
                    par.DescriptionOne = data.Value<string>("GroupParameterID");// 群组
                    par.Description = data.Value<string>("UseParameterID");
                    par.Comments = data.Value<string>("Comments");
                    par.IsEnable = data.Value<int>("Status"); //状态
                    par.IsDefault = false;
                    par.Sequence = 0;//暂时后台定死0
                    par.UsingType = 0;//这个字段暂未知有何用处
                    if (SYS_ParameterService.insert(userid, par))
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
        /// 删除分类
        /// Tom 2017年5月2日17:58:16
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="jArray"></param>
        /// <returns></returns>
        public static object Inf00009ClassDelete(string userID, JArray jArray)
        {
            int success = 0;
            int fail = 0;
            string failIDs = null;
            string msg = null;
            JObject data = null;
            for (int i = 0; i < jArray.Count; i++)
            {
                data = (JObject)jArray[i];
                if (SYS_ItemsService.CheckClass(data.Value<string>("ParameterID")))
                {
                    failIDs = UtilBussinessService.str(failIDs, data.Value<string>("ParameterID"));
                    msg = UtilBussinessService.str(msg, data.Value<string>("Code") + "已使用,不能删除！");
                    fail++;
                }
                else if (SYS_ParameterService.delete(userID, data.Value<string>("ParameterID")))
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
        /// 单一删除分类代号
        /// Joint 2017年7月24日17:45:39
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public static object Inf00009ClassDeleted(JObject request)
        {
            string Token = request.Value<string>("Token");
            string userid = UtilBussinessService.detoken(Token);
            SYS_Parameters model = SYS_ParameterService.get(request.Value<string>("ParameterID"));
            if (model == null)
                return new { status = "410", msg = "不存在的分类群组码" };
            if (SYS_ItemsService.CheckClass(request.Value<string>("ParameterID")))
                return new { status = "410", msg = "已使用，无法删除！" };
            if (EMS_EquipmentService.get(request.Value<string>("ParameterID")) != null)
                return new { status = "410", msg = "已使用，无法删除！" };
            if (SYS_ParameterService.delete(userid, request.Value<string>("ParameterID")))
                return new { status = "200", msg = "删除成功！" };
            else
                return new { status = "410", msg = "删除失败！" };

        }
        #endregion

        #region Inf00010料品主档
        /// <summary>
        /// 料品主档列表
        /// SAM 2017年5月16日14:53:24
        /// </summary>
        /// <param name="Token">授权码</param>
        /// <param name="Code">代号</param>
        /// <param name="Status">状态</param>
        /// <param name="page">页码</param>
        /// <param name="rows">行数</param>
        /// <returns></returns>
        public static object Inf00010GetList(string token, string code, string Status, int page, int rows)
        {
            int count = 0;
            return UtilBussinessService.getPaginationModel(SYS_ItemsService.Inf00010GetList(code, Status, page, rows, ref count), count);
        }

        /// <summary>
        /// 新增料品
        /// SAM 2017年5月16日15:36:05
        /// </summary>
        /// <param name="Token">授权码</param>
        /// <param name="jArray"></param>
        /// <returns></returns>
        public static object Inf00010insert(string Token, JArray jArray)
        {
            int success = 0;
            int fail = 0;
            string failIDs = null;
            string msg = null;
            string userid = UtilBussinessService.detoken(Token);
            JObject data = null;
            SYS_Items model = null;
            bool IsClass = true;
            for (int i = 0; i < jArray.Count; i++)
            {
                data = (JObject)jArray[i];
                List<string> ClassList = new List<string>();
                if (!string.IsNullOrWhiteSpace(data.Value<string>("ClassOne")))
                    ClassList.Add(data.Value<string>("ClassOne"));
                if (!string.IsNullOrWhiteSpace(data.Value<string>("ClassTwo")))
                    ClassList.Add(data.Value<string>("ClassTwo"));
                if (!string.IsNullOrWhiteSpace(data.Value<string>("ClassThree")))
                    ClassList.Add(data.Value<string>("ClassThree"));
                if (!string.IsNullOrWhiteSpace(data.Value<string>("ClassFour")))
                    ClassList.Add(data.Value<string>("ClassFour"));
                if (!string.IsNullOrWhiteSpace(data.Value<string>("ClassFive")))
                    ClassList.Add(data.Value<string>("ClassFive"));
                int count = ClassList.Count;
                if (count != 0)
                {
                    int NCount = ClassList.Distinct().ToList().Count;
                    if (count != NCount)
                        IsClass = false;
                }
                if (IsClass)
                {
                    if (!SYS_ItemsService.CheckCode(data.Value<string>("Code"), null))
                    {
                        model = new SYS_Items();
                        model.ItemID = UniversalService.GetSerialNumber("SYS_Items");
                        model.Code = data.Value<string>("Code");
                        model.Name = data.Value<string>("Name");
                        model.Specification = data.Value<string>("Specification");
                        model.Status = data.Value<string>("Status");
                        model.Unit = data.Value<string>("Unit");
                        model.ClassOne = data.Value<string>("ClassOne");
                        model.ClassTwo = data.Value<string>("ClassTwo");
                        model.ClassThree = data.Value<string>("ClassThree");
                        model.ClassFour = data.Value<string>("ClassFour");
                        model.ClassFive = data.Value<string>("ClassFive");
                        model.AuxUnit = data.Value<string>("AuxUnit");
                        model.AuxUnitRatio = data.Value<decimal>("AuxUnitRatio");
                        model.IsCutMantissa = data.Value<bool>("IsCutMantissa");
                        model.CutMantissa = data.Value<string>("CutMantissa");
                        model.Type = data.Value<string>("Type");
                        model.Drawing = data.Value<string>("Drawing");
                        model.PartSource = data.Value<string>("PartSource");
                        model.BarCord = data.Value<string>("BarCord");
                        model.GroupID = data.Value<string>("GroupID");
                        model.Lot = data.Value<bool>("Lot");
                        model.OverRate = data.Value<decimal>("OverRate");
                        model.Comments = data.Value<string>("Comments");
                        model.SerialPart = data.Value<bool>("SerialPart");
                        model.KeyPart = data.Value<bool>("KeyPart");
                        model.LotMethod = data.Value<string>("LotMethod");
                        model.LotClassID = data.Value<string>("LotClassID");

                        if (SYS_ItemsService.insert(userid, model))
                            success++;
                        else
                        {
                            failIDs = UtilBussinessService.str(failIDs, data.Value<string>("ItemID"));
                            fail++;
                        }
                    }
                    else
                    {
                        failIDs = UtilBussinessService.str(failIDs, data.Value<string>("ItemID"));
                        msg = UtilBussinessService.str(msg, data.Value<string>("Code") + "已存在");
                        fail++;
                    }
                }
                else
                {
                    failIDs = UtilBussinessService.str(failIDs, data.Value<string>("ItemID"));
                    msg = UtilBussinessService.str(msg, "分类资料重复");
                    fail++;
                }
            }
            return new { success = success, fail = fail, failIDs = failIDs, msg = msg };
        }

        /// <summary>
        /// 删除
        /// SAM 2017年5月16日15:41:55 
        /// </summary>
        /// <param name="Token">授权码</param>
        /// <param name="jArray"></param>
        /// <returns></returns>
        public static object Inf00010delete(string Token, JArray jArray)
        {
            int success = 0;
            int fail = 0;
            string failIDs = null;
            string msg = null;
            string userid = UtilBussinessService.detoken(Token);
            JObject data = null;
            SYS_Items model = null;
            for (int i = 0; i < jArray.Count; i++)
            {
                data = (JObject)jArray[i];
                model = SYS_ItemsService.get(data.Value<string>("ItemID"));
                model.Status = Framework.SystemID + "0201213000003";
                if (SYS_ItemsService.update(userid, model))
                    success++;
                else
                {
                    failIDs = UtilBussinessService.str(failIDs, data.Value<string>("ItemID"));
                    fail++;
                }
            }
            return new { success = success, fail = fail, failIDs = failIDs, msg = msg };
        }

        /// <summary>
        /// 更新
        /// SAM 2017年5月16日15:43:36
        /// </summary>
        /// <param name="Token">授权码</param>
        /// <param name="jArray"></param>
        /// <returns></returns>
        public static object Inf00010update(string Token, JArray jArray)
        {
            int success = 0;
            int fail = 0;
            string failIDs = null;
            string msg = null;
            string userid = UtilBussinessService.detoken(Token);
            JObject data = null;
            SYS_Items model = null;
            bool IsClass = true;
            for (int i = 0; i < jArray.Count; i++)
            {
                data = (JObject)jArray[i];
                List<string> ClassList = new List<string>();
                if (!string.IsNullOrWhiteSpace(data.Value<string>("ClassOne")))
                    ClassList.Add(data.Value<string>("ClassOne"));
                if (!string.IsNullOrWhiteSpace(data.Value<string>("ClassTwo")))
                    ClassList.Add(data.Value<string>("ClassTwo"));
                if (!string.IsNullOrWhiteSpace(data.Value<string>("ClassThree")))
                    ClassList.Add(data.Value<string>("ClassThree"));
                if (!string.IsNullOrWhiteSpace(data.Value<string>("ClassFour")))
                    ClassList.Add(data.Value<string>("ClassFour"));
                if (!string.IsNullOrWhiteSpace(data.Value<string>("ClassFive")))
                    ClassList.Add(data.Value<string>("ClassFive"));
                int count = ClassList.Count;
                if (count != 0)
                {
                    int NCount = ClassList.Distinct().ToList().Count;
                    if (count != NCount)
                        IsClass = false;
                }
                if (IsClass)
                {
                    model = SYS_ItemsService.get(data.Value<string>("ItemID"));
                    model.Name = data.Value<string>("Name");
                    model.Specification = data.Value<string>("Specification");
                    model.Status = data.Value<string>("Status");
                    model.Unit = data.Value<string>("Unit");
                    model.ClassOne = data.Value<string>("ClassOne");
                    model.ClassTwo = data.Value<string>("ClassTwo");
                    model.ClassThree = data.Value<string>("ClassThree");
                    model.ClassFour = data.Value<string>("ClassFour");
                    model.ClassFive = data.Value<string>("ClassFive");
                    model.AuxUnit = data.Value<string>("AuxUnit");
                    model.AuxUnitRatio = data.Value<decimal>("AuxUnitRatio");
                    model.IsCutMantissa = data.Value<bool>("IsCutMantissa");
                    model.CutMantissa = data.Value<string>("CutMantissa");
                    model.Type = data.Value<string>("Type");
                    model.Drawing = data.Value<string>("Drawing");
                    model.PartSource = data.Value<string>("PartSource");
                    model.BarCord = data.Value<string>("BarCord");
                    model.GroupID = data.Value<string>("GroupID");
                    model.Lot = data.Value<bool>("Lot");
                    model.OverRate = data.Value<decimal>("OverRate");
                    model.SerialPart = data.Value<bool>("SerialPart");
                    model.KeyPart = data.Value<bool>("KeyPart");
                    model.LotMethod = data.Value<string>("LotMethod");
                    model.LotClassID = data.Value<string>("LotClassID");
                    model.Comments = data.Value<string>("Comments");
                    if (SYS_ItemsService.update(userid, model))
                        success++;
                    else
                    {
                        failIDs = UtilBussinessService.str(failIDs, data.Value<string>("ItemID"));
                        fail++;
                    }
                }
                else
                {
                    failIDs = UtilBussinessService.str(failIDs, data.Value<string>("ItemID"));
                    msg = UtilBussinessService.str(msg, "分类资料重复");
                    fail++;
                }

            }
            return new { success = success, fail = fail, failIDs = failIDs, msg = msg };
        }

        /// <summary>
        /// 料品的单一删除
        /// SAM 2017年7月24日17:45:41
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public static object Inf00010Delete(JObject request)
        {
            string Token = request.Value<string>("Token");
            string userid = UtilBussinessService.detoken(Token);

            SYS_Items model = SYS_ItemsService.get(request.Value<string>("ItemID"));
            if (model == null)
                return new { status = "410", msg = "不存在的料品信息！" };

            //if (SYS_ItemAttributesService.CheckAttribute(model.ParameterID))
            //    return new { status = "410", msg = model.Code + "已使用，无法删除！" };

            //if (SFC_BatchAttributeDetailsService.CheckAttribute(model.ParameterID))
            //    return new { status = "410", msg = model.Code + "已使用，无法删除！" };

            model.Status = Framework.SystemID + "0201213000003";

            if (SYS_ItemsService.update(userid, model))
                return new { status = "200", msg = "删除成功！" };
            else
                return new { status = "410", msg = "删除失败！" };
        }


        /// <summary>
        /// 获取料品的属性列表
        /// SAM 2017年5月16日16:31:05
        /// </summary>
        /// <param name="Token">授权码</param>
        /// <param name="ItemID"></param>
        /// <param name="page">页码</param>
        /// <param name="rows">行数</param>
        /// <returns></returns>
        public static object Inf00010AttributeGetList(string token, string ItemID, int page, int rows)
        {
            int count = 0;
            return UtilBussinessService.getPaginationModel(SYS_ItemAttributesService.Inf00010AttributeGetList(ItemID, page, rows, ref count), count);
        }


        /// <summary>
        /// 添加料品的属性
        /// SAM 2017年5月16日16:35:53
        /// </summary>
        /// <param name="Token">授权码</param>
        /// <param name="jArray"></param>
        /// <returns></returns>
        public static object Inf00010Attributeinsert(string Token, JArray jArray)
        {
            int success = 0;
            int fail = 0;
            string failIDs = null;
            string msg = null;
            string userid = UtilBussinessService.detoken(Token);
            JObject data = null;
            SYS_ItemAttributes model = null;
            for (int i = 0; i < jArray.Count; i++)
            {
                data = (JObject)jArray[i];

                if (!string.IsNullOrWhiteSpace(data.Value<string>("ItemID")))
                {
                    if (!SYS_ItemAttributesService.Check(data.Value<string>("AttributeID"), data.Value<string>("ItemID")))
                    {
                        model = new SYS_ItemAttributes();
                        model.ItemAttributeID = UniversalService.GetSerialNumber("SYS_ItemAttributes");
                        model.ItemID = data.Value<string>("ItemID");
                        model.AttributeID = data.Value<string>("AttributeID");
                        model.Status = Framework.SystemID + "0201213000001";
                        model.Comments = data.Value<string>("Comments");
                        if (SYS_ItemAttributesService.insert(userid, model))
                            success++;
                        else
                        {
                            failIDs = UtilBussinessService.str(failIDs, data.Value<string>("ItemAttributeID"));
                            fail++;
                        }
                    }
                    else
                    {
                        failIDs = UtilBussinessService.str(failIDs, data.Value<string>("ItemAttributeID"));
                        msg = UtilBussinessService.str(msg, "资料重复");
                        fail++;
                    }
                }
                else
                {
                    failIDs = UtilBussinessService.str(failIDs, data.Value<string>("ItemAttributeID"));
                    msg = UtilBussinessService.str(msg, "料品流水号为空");
                    fail++;
                }
            }
            return new { success = success, fail = fail, failIDs = failIDs, msg = msg };
        }

        /// <summary>
        /// 删除料品的属性
        /// SAM 2017年5月16日15:41:55 
        /// </summary>
        /// <param name="Token">授权码</param>
        /// <param name="jArray"></param>
        /// <returns></returns>
        public static object Inf00010Attributedelete(string Token, JArray jArray)
        {
            int success = 0;
            int fail = 0;
            string failIDs = null;
            string msg = null;
            string userid = UtilBussinessService.detoken(Token);
            JObject data = null;
            SYS_ItemAttributes model = null;
            for (int i = 0; i < jArray.Count; i++)
            {
                data = (JObject)jArray[i];
                model = SYS_ItemAttributesService.get(data.Value<string>("ItemAttributeID"));
                model.Status = Framework.SystemID + "0201213000003";
                if (SYS_ItemAttributesService.update(userid, model))
                    success++;
                else
                {
                    failIDs = UtilBussinessService.str(failIDs, data.Value<string>("ItemAttributeID"));
                    fail++;
                }
            }
            return new { success = success, fail = fail, failIDs = failIDs, msg = msg };
        }

        /// <summary>
        /// 更新料品的属性
        /// SAM 2017年5月16日15:43:36
        /// </summary>
        /// <param name="Token">授权码</param>
        /// <param name="jArray"></param>
        /// <returns></returns>
        public static object Inf00010Attributeupdate(string Token, JArray jArray)
        {
            int success = 0;
            int fail = 0;
            string failIDs = null;
            string msg = null;
            string userid = UtilBussinessService.detoken(Token);
            JObject data = null;
            SYS_ItemAttributes model = null;

            for (int i = 0; i < jArray.Count; i++)
            {
                data = (JObject)jArray[i];
                model = SYS_ItemAttributesService.get(data.Value<string>("ItemAttributeID"));
                model.Comments = data.Value<string>("Comments");
                if (SYS_ItemAttributesService.update(userid, model))
                    success++;
                else
                {
                    failIDs = UtilBussinessService.str(failIDs, data.Value<string>("ItemAttributeID"));
                    fail++;
                }


            }
            return new { success = success, fail = fail, failIDs = failIDs, msg = msg };
        }


        /// <summary>
        ///  获取料品语序
        ///  SAM 2017年5月17日10:02:33
        /// </summary>
        /// <param name="id"></param>
        /// <param name="TableID"></param>
        /// <returns></returns>
        public static IList<Hashtable> Inf00010GetLanguageList(string id)
        {
            return SYS_LanguageLibService.Inf00010GetLanguageList(id);
        }

        /// <summary>
        /// 添加料品语序
        /// SAM 2017年5月17日10:03:52
        /// </summary>
        /// <param name="Token">授权码</param>
        /// <param name="jArray"></param>
        /// <returns></returns>
        public static object Inf00010Languageinsert(string Token, JArray jArray)
        {
            int success = 0;
            int fail = 0;
            string failIDs = null;
            string msg = null;
            string userid = UtilBussinessService.detoken(Token);
            JObject request = null;
            for (int i = 0; i < jArray.Count; i++)
            {
                request = (JObject)jArray[i];
                if (!SYS_LanguageLibService.checkLanguage(request.Value<string>("RowID"), "Code", request.Value<string>("LanguageCode"), null))
                {
                    SYS_LanguageLib lan = new SYS_LanguageLib();
                    lan.LanguageLibID = UniversalService.GetSerialNumber("SYS_LanguageLib");
                    lan.TableID = "39";
                    lan.RowID = request.Value<string>("RowID");
                    lan.Field = "Code";
                    lan.OriginalLanguage = request.Value<string>("OriginalLanguage");
                    lan.OriginalContent = request.Value<string>("OriginalContent");
                    lan.LanguageCode = request.Value<string>("LanguageCode");
                    lan.LanguageContentOne = request.Value<string>("Code");
                    lan.IsDefault = request.Value<bool>("IsDefault");
                    if (SYS_LanguageLibService.insert(userid, lan))
                    {
                        lan.LanguageLibID = UniversalService.GetSerialNumber("SYS_LanguageLib");
                        lan.Field = "Name";
                        lan.LanguageCode = request.Value<string>("LanguageCode");
                        lan.LanguageContentOne = request.Value<string>("Name");
                        SYS_LanguageLibService.insert(userid, lan);
                        lan.LanguageLibID = UniversalService.GetSerialNumber("SYS_LanguageLib");
                        lan.Field = "Specification";
                        lan.LanguageCode = request.Value<string>("LanguageCode");
                        lan.LanguageContentOne = request.Value<string>("Specification");
                        SYS_LanguageLibService.insert(userid, lan);
                        lan.LanguageLibID = UniversalService.GetSerialNumber("SYS_LanguageLib");
                        lan.Field = "Comments";
                        lan.LanguageCode = request.Value<string>("LanguageCode");
                        lan.LanguageContentOne = request.Value<string>("Comments");
                        SYS_LanguageLibService.insert(userid, lan);
                        success++;
                    }
                    else
                    {
                        failIDs = UtilBussinessService.str(failIDs, request.Value<string>("LanguageLibID"));
                        fail++;
                    }
                }
                else
                {
                    failIDs = UtilBussinessService.str(failIDs, request.Value<string>("LanguageLibID"));
                    msg = UtilBussinessService.str(msg, "已存在语序");
                    fail++;
                }

            }

            return new { success = success, fail = fail, failIDs = failIDs, msg = msg };
        }

        /// <summary>
        /// 更新料品语序
        /// SAM 2017年5月17日10:06:58
        /// </summary>
        /// <param name="Token">授权码</param>
        /// <param name="jArray"></param>
        /// <returns></returns>
        public static object Inf00010Languageupdate(string Token, JArray jArray)
        {
            int success = 0;
            int fail = 0;
            string failIDs = null;
            string msg = null;
            string userid = UtilBussinessService.detoken(Token);
            JObject request = null;
            try
            {
                for (int i = 0; i < jArray.Count; i++)
                {
                    request = (JObject)jArray[i];
                    if (!SYS_LanguageLibService.checkLanguage(request.Value<string>("RowID"), "Code", request.Value<string>("LanguageCode"), request.Value<string>("LanguageLibID")))
                    {
                        bool IsDefault = request.Value<bool>("IsDefault");
                        try
                        {
                            SYS_LanguageLibService.UpdateLanguage(userid, request.Value<string>("Code"), request.Value<string>("LanguageCode"), IsDefault, request.Value<string>("RowID"), "Code");
                            SYS_LanguageLibService.UpdateLanguage(userid, request.Value<string>("Name"), request.Value<string>("LanguageCode"), IsDefault, request.Value<string>("RowID"), "Name");
                            SYS_LanguageLibService.UpdateLanguage(userid, request.Value<string>("Specification"), request.Value<string>("LanguageCode"), IsDefault, request.Value<string>("RowID"), "Specification");
                            SYS_LanguageLibService.UpdateLanguage(userid, request.Value<string>("Comments"), request.Value<string>("LanguageCode"), IsDefault, request.Value<string>("RowID"), "Comments");
                            success++;
                        }
                        catch (Exception ex)
                        {
                            DataLogerService.writeerrlog(ex);
                            failIDs = UtilBussinessService.str(failIDs, request.Value<string>("LanguageLibID"));
                            fail++;
                        }
                    }
                    else
                    {
                        failIDs = UtilBussinessService.str(failIDs, request.Value<string>("LanguageLibID"));
                        msg = UtilBussinessService.str(msg, "已存在语序");
                        fail++;
                    }
                }
            }
            catch (Exception ex)
            {
                DataLogerService.writeerrlog(ex);
            }
            return new { success = success, fail = fail, failIDs = failIDs, msg = msg };
        }
        #endregion

        #region Inf00011单位主档
        /// <summary>
        /// 获取单位列表
        /// Tom 2017-05-02 17:22:03
        /// </summary>
        /// <param name="Token">授权码</param>
        /// <param name="Code">代号</param>
        /// <param name="page">页码</param>
        /// <param name="rows">行数</param>
        /// <returns></returns>
        public static object Inf00011List(string token, string code, int page, int rows)
        {
            int count = 0;
            return UtilBussinessService.getPaginationModel(SYS_ParameterService.Inf00011List(code, page, rows, ref count), count);
        }

        /// <summary>
        /// 删除单位
        /// Tom 2017年5月2日17:34:35
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="jArray"></param>
        /// <returns></returns>
        public static object Inf00011Delete(string userID, JArray jArray)
        {
            int success = 0;
            int fail = 0;
            string failIDs = null;
            JObject data = null;
            string msg = null;
            for (int i = 0; i < jArray.Count; i++)
            {
                data = (JObject)jArray[i];
                if (!SYS_ItemsService.CheckUnit(data.Value<string>("ParameterID")))
                {
                    if (SYS_ParameterService.delete(userID, data.Value<string>("ParameterID")))
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
                    msg = UtilBussinessService.str(msg, data.Value<string>("Code") + "已使用,不能删除！");
                    fail++;
                }
            }
            return new { success = success, fail = fail, failIDs = failIDs, msg = msg };
        }

        /// <summary>
        /// 添加单位
        /// Tom 2017年5月2日17:34:47
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="jArray"></param>
        /// <returns></returns>
        public static object Inf00011Insert(string userID, JArray jArray)
        {
            int success = 0;
            int fail = 0;
            string failIDs = null;
            string msg = null;
            string userid = userID;
            string typeID = Framework.SystemID + "019121300000C";
            JObject data = null;
            SYS_Parameters par = null;
            for (int i = 0; i < jArray.Count; i++)
            {
                data = (JObject)jArray[i];
                if (!SYS_ParameterService.CheckParameter(data.Value<string>("Code"), null, null, typeID, null))
                {
                    par = new SYS_Parameters();
                    par.ParameterID = UniversalService.GetSerialNumber("SYS_Parameters");
                    par.ParameterTypeID = typeID;
                    par.Code = data.Value<string>("Code");  //代号
                    par.Name = data.Value<string>("Name");//说明
                    par.Comments = data.Value<string>("Comments");
                    par.IsEnable = 1; //状态
                    par.IsDefault = false;
                    par.Sequence = 0;//暂时后台定死0
                    par.UsingType = 0;//这个字段暂未知有何用处
                    par.IsSystem = true;
                    if (SYS_ParameterService.insert(userid, par))
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
        /// 更新单位
        /// Tom 2017年5月2日17:35:03
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="jArray"></param>
        /// <returns></returns>
        public static object Inf00011Update(string userID, JArray jArray)
        {
            int success = 0;
            int fail = 0;
            string failIDs = null;
            string msg = null;
            string userid = userID;
            JObject data = null;
            SYS_Parameters par = null;
            for (int i = 0; i < jArray.Count; i++)
            {
                data = (JObject)jArray[i];

                par = new SYS_Parameters();
                par = SYS_ParameterService.get(data.Value<string>("ParameterID"));
                par.Name = data.Value<string>("Name");//说明
                par.Comments = data.Value<string>("Comments");
                par.IsEnable = data.Value<int>("Status"); //状态
                if (SYS_ParameterService.update(userid, par))
                    success++;
                else
                {
                    failIDs = UtilBussinessService.str(failIDs, par.ParameterID);
                    fail++;
                }

            }
            return new { success = success, fail = fail, failIDs = failIDs, msg = msg };
        }

        /// <summary>
        /// 单位的删除
        /// SAM 2017年7月24日17:27:27
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public static object Inf00011Delete(JObject request)
        {
            string Token = request.Value<string>("Token");
            string userid = UtilBussinessService.detoken(Token);

            SYS_Parameters model = SYS_ParameterService.get(request.Value<string>("ParameterID"));
            if (model == null)
                return new { status = "410", msg = "不存在的单位信息！" };

            if (SYS_ItemsService.CheckUnit(request.Value<string>("ParameterID")))
                return new { status = "410", msg = model.Code + "已使用，无法删除！" };

                //if (SFC_BatchAttributeDetailsService.CheckAttribute(model.ParameterID))
                //    return new { status = "410", msg = model.Code + "已使用，无法删除！" };
                model.IsEnable = 2;

            if (SYS_ParameterService.update(userid, model))
                return new { status = "200", msg = "删除成功！" };
            else
                return new { status = "410", msg = "删除失败！" };
        }
        #endregion

        #region Inf00012仓库主档
        /// <summary>
        /// 获取仓库的列表
        /// SAM 2017年5月4日11:05:44
        /// </summary>
        /// <param name="Token">授权码</param>
        /// <param name="Code">代号</param>
        /// <param name="page">页码</param>
        /// <param name="rows">行数</param>
        /// <returns></returns>
        public static object Inf00012GetList(string token, string Code, string Status, int page, int rows)
        {
            int count = 0;
            return UtilBussinessService.getPaginationModel(SYS_OrganizationService.Inf00012GetList(Code, Status, page, rows, ref count), count);
        }

        /// <summary>
        /// 仓库删除
        /// SAM 2017年5月4日11:17:26
        /// </summary>
        /// <param name="Token">授权码</param>
        /// <param name="jArray"></param>
        /// <returns></returns>
        public static object Inf00012delete(string Token, JArray jArray)
        {
            int success = 0;
            int fail = 0;
            string failIDs = null;
            string msg = null;
            string userid = UtilBussinessService.detoken(Token);
            JObject data = null;
            SYS_Organization model = null;
            for (int i = 0; i < jArray.Count; i++)
            {
                data = (JObject)jArray[i];
                //if (!SYS_PlantAreaService.CheckPlant(data.Value<string>("OrganizationID")))
                //{
                model = SYS_OrganizationService.get(data.Value<string>("OrganizationID"));
                model.Status = 2;
                if (SYS_OrganizationService.update(userid, model))
                    success++;
                else
                {
                    failIDs = UtilBussinessService.str(failIDs, data.Value<string>("OrganizationID"));
                    fail++;
                }
                //}
                //else
                //{
                //    failIDs = UtilBussinessService.str(failIDs, data.Value<string>("OrganizationID"));
                //    msg = UtilBussinessService.str(failIDs, Model.Code + "xxx已使用，不能删除");
                //    fail++;
                //}
            }
            return new { success = success, fail = fail, failIDs = failIDs, msg = msg };
        }

        /// <summary>
        /// 仓库添加
        /// SAM 2017年5月4日11:17:29
        /// </summary>
        /// <param name="Token">授权码</param>
        /// <param name="jArray"></param>
        /// <returns></returns>
        public static object Inf00012insert(string Token, JArray jArray)
        {
            int success = 0;
            int fail = 0;
            string failIDs = null;
            string msg = null;
            string userid = UtilBussinessService.detoken(Token);
            JObject data = null;
            SYS_Organization model = null;
            for (int i = 0; i < jArray.Count; i++)
            {
                data = (JObject)jArray[i];
                if (!SYS_OrganizationService.CheckCode(data.Value<string>("Code"), Framework.SystemID + "020121300001F", null))
                {
                    model = new SYS_Organization();
                    model.OrganizationID = UniversalService.GetSerialNumber("SYS_Organization");
                    model.ParentOrganizationID = data.Value<string>("ParentOrganizationID");
                    model.Code = data.Value<string>("Code");
                    model.Name = data.Value<string>("Name");
                    model.Type = Framework.SystemID + "020121300001F";
                    model.Status = data.Value<int>("Status");
                    model.Sequence = string.IsNullOrWhiteSpace(data.Value<string>("Sequence")) ? 0 : data.Value<int>("Sequence");
                    model.Comments = data.Value<string>("Comments");
                    if (SYS_OrganizationService.insert(userid, model))
                        success++;
                    else
                    {
                        failIDs = UtilBussinessService.str(failIDs, data.Value<string>("OrganizationID"));
                        fail++;
                    }
                }
                else
                {
                    failIDs = UtilBussinessService.str(failIDs, data.Value<string>("OrganizationID"));
                    msg = UtilBussinessService.str(msg, data.Value<string>("Code") + "已存在");
                    fail++;
                }
            }
            return new { success = success, fail = fail, failIDs = failIDs, msg = msg };
        }

        /// <summary>
        /// 仓库更新
        /// SAM 2017年4月26日15:34:03
        /// </summary>
        /// <param name="Token">授权码</param>
        /// <param name="jArray"></param>
        /// <returns></returns>
        public static object Inf00012update(string Token, JArray jArray)
        {
            int success = 0;
            int fail = 0;
            string failIDs = null;
            string msg = null;
            string userid = UtilBussinessService.detoken(Token);
            JObject data = null;
            SYS_Organization model = null;
            for (int i = 0; i < jArray.Count; i++)
            {
                data = (JObject)jArray[i];
                if (!SYS_OrganizationService.CheckCode(data.Value<string>("Code"), Framework.SystemID + "020121300001F", data.Value<string>("OrganizationID")))
                {
                    model = SYS_OrganizationService.get(data.Value<string>("OrganizationID"));
                    model.ParentOrganizationID = data.Value<string>("ParentOrganizationID");
                    model.Status = data.Value<int>("Status");
                    model.Name = data.Value<string>("Name");
                    model.Comments = data.Value<string>("Comments");
                    if (SYS_OrganizationService.update(userid, model))
                        success++;
                    else
                    {
                        failIDs = UtilBussinessService.str(failIDs, data.Value<string>("OrganizationID"));
                        fail++;
                    }
                }
                else
                {
                    failIDs = UtilBussinessService.str(failIDs, data.Value<string>("OrganizationID"));
                    msg = UtilBussinessService.str(msg, data.Value<string>("Code") + "已存在");
                    fail++;
                }

            }
            return new { success = success, fail = fail, failIDs = failIDs, msg = msg };
        }

        /// <summary>
        /// 仓库的单一删除
        /// SAM 2017年7月24日10:25:36
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public static object Inf00012Delete(JObject request)
        {
            string Token = request.Value<string>("Token");
            string userid = UtilBussinessService.detoken(Token);

            SYS_Organization model = SYS_OrganizationService.get(request.Value<string>("OrganizationID"));
            if (model == null)
                return new { status = "410", msg = "不存在的仓库信息！" };

            //if (SYS_ItemAttributesService.CheckAttribute(model.ParameterID))
            //    return new { status = "410", msg = model.Code + "已使用，无法删除！" };

            //if (SFC_BatchAttributeDetailsService.CheckAttribute(model.ParameterID))
            //    return new { status = "410", msg = model.Code + "已使用，无法删除！" };

            model.Status = 2;

            if (SYS_OrganizationService.update(userid, model))
                return new { status = "200", msg = "删除成功！" };
            else
                return new { status = "410", msg = "删除失败！" };
        }
        #endregion

        #region Inf00013班别主档
        /// <summary>
        /// 获取班别列表
        /// Tom 2017年5月5日15:05:52
        /// </summary>
        /// <param name="Token">授权码</param>
        /// <param name="Code">代号</param>
        /// <param name="Status">状态</param>
        /// <param name="page">页码</param>
        /// <param name="rows">行数</param>
        /// <returns></returns>
        public static object Inf00013List(string token, string code, string status, int page, int rows)
        {
            int count = 0;
            return UniversalService.getPaginationModel(
                SYS_ClassService.GetPage(code, status, page, rows, ref count), count);
        }

        /// <summary>
        /// 班别添加
        /// Tom 2017年5月5日15:29:42
        /// </summary>
        /// <param name="Token">授权码</param>
        /// <param name="jArray"></param>
        /// <returns></returns>
        public static object Inf00013Insert(string Token, JArray jArray)
        {
            int success = 0;
            int fail = 0;
            string failIDs = null;
            string msg = null;
            string userid = UtilBussinessService.detoken(Token);
            JObject data = null;
            SYS_Class Model = null;
            for (int i = 0; i < jArray.Count; i++)
            {
                data = (JObject)jArray[i];
                Model = new SYS_Class();
                Model.ClassID = UniversalService.GetSerialNumber("SYS_Class");
                Model.Code = data.Value<string>("Code");
                Model.Name = data.Value<string>("Name");
                Model.CrossDay = data.Value<bool>("CrossDay");
                Model.OnTime = data.Value<string>("OnTime");
                Model.OffTime = data.Value<string>("OffTime");
                Model.OffHour = data.Value<decimal>("OffHour");
                Model.WorkHour = data.Value<decimal>("WorkHour");
                Model.Comments = data.Value<string>("Comments");
                Model.Status = data.Value<string>("Status");
                if (SYS_ClassService.ChecCode(Model.Code))
                {
                    failIDs = UtilBussinessService.str(failIDs, data.Value<string>("Code"));
                    msg = UtilBussinessService.str(msg, "资料重复");
                    fail++;
                }
                else if (SYS_ClassService.insert(userid, Model))
                    success++;
                else
                {
                    failIDs = UtilBussinessService.str(failIDs, data.Value<string>("ClassID"));
                    fail++;
                }
            }
            return new { success = success, fail = fail, failIDs = failIDs, msg = msg };
        }

        /// <summary>
        /// 班别更新
        /// Tom 2017年5月5日15:30:01
        /// </summary>
        /// <param name="Token">授权码</param>
        /// <param name="jArray"></param>
        /// <returns></returns>
        public static object Inf00013Update(string Token, JArray jArray)
        {
            int success = 0;
            int fail = 0;
            string failIDs = null;
            string msg = null;
            string userid = UtilBussinessService.detoken(Token);
            JObject data = null;
            for (int i = 0; i < jArray.Count; i++)
            {
                data = (JObject)jArray[i];
                SYS_Class Model = null;
                Model = new SYS_Class();
                Model.ClassID = data.Value<string>("ClassID");
                Model.Code = data.Value<string>("Code");
                Model.Name = data.Value<string>("Name");
                Model.CrossDay = data.Value<bool>("CrossDay");
                Model.OnTime = data.Value<string>("OnTime");
                Model.OffTime = data.Value<string>("OffTime");
                Model.OffHour = data.Value<decimal>("OffHour");
                Model.WorkHour = data.Value<decimal>("WorkHour");
                Model.Status = data.Value<string>("Status");
                Model.Comments = data.Value<string>("Comments");
                if (SYS_ClassService.ChecCode(Model.Code, Model.ClassID))
                {
                    failIDs = UtilBussinessService.str(failIDs, data.Value<string>("Code"));
                    msg = UtilBussinessService.str(msg, "资料重复");
                    fail++;
                }
                else if (SYS_ClassService.update(userid, Model))
                    success++;
                else
                {
                    failIDs = UtilBussinessService.str(failIDs, data.Value<string>("ClassID"));
                    fail++;
                }
            }
            return new { success = success, fail = fail, failIDs = failIDs, msg = msg };
        }

        /// <summary>
        /// 班别删除
        /// Tom 2017年5月5日15:30:13
        /// </summary>
        /// <param name="Token">授权码</param>
        /// <param name="jArray"></param>
        /// <returns></returns>
        public static object Inf00013Delete(string Token, JArray jArray)
        {
            int success = 0;
            int fail = 0;
            string failIDs = null;
            string msg = null;
            string userid = UtilBussinessService.detoken(Token);
            JObject data = null;
            SYS_Class Model = null;
            for (int i = 0; i < jArray.Count; i++)
            {
                data = (JObject)jArray[i];
                Model = SYS_ClassService.get(data.Value<string>("ClassID"));
                if (Model == null)
                {
                    continue;
                }

                Model.Status = Framework.SystemID + "0201213000003";
                if (SYS_ClassService.update(userid, Model))
                    success++;
                else
                {
                    failIDs = UtilBussinessService.str(failIDs, data.Value<string>("ClassID"));
                    fail++;
                }
            }
            return new { success = success, fail = fail, failIDs = failIDs, msg = msg };
        }

        /// <summary>
        /// 单一删除 
        /// Joint 2017年7月24日16:57:52
        /// </summary>
        /// <param name="requst"></param>
        /// <returns></returns>
        public static object Inf00013delect(JObject requst)
        {
            string Token = requst.Value<string>("Token");
            string userid = UtilBussinessService.detoken(Token);
            SYS_Class model = SYS_ClassService.get(requst.Value<string>("ClassID"));
            if (model == null)
                return new { status = "410", msg = "不存在的班别信息！" };
            if (SFC_TaskDispatchService.Check(model.ClassID) != null)
                return new { status = "410", msg = model.Code + "已使用，无法删除！" };
            if (SYS_OrganizationClassService.CheckClass(requst.Value<string>("ClassID"), null))
                return new { status = "410", msg = model.Code + "已使用，无法删除！" };
            model.Status = Framework.SystemID + "0201213000003";
            if (SYS_ClassService.update(userid, model))
                return new { status = "200", msg = "删除成功！" };
            else
                return new { status = "410", msg = "删除失败！" };
        }
        #endregion

        #region Inf00014行事历主档
        /// <summary>
        /// 行事历的列表
        /// SAM 2017年5月8日17:46:55
        /// </summary>
        /// <param name="Token">授权码</param>
        /// <param name="Code">代号</param>
        /// <param name="page">页码</param>
        /// <param name="rows">行数</param>
        /// <returns></returns>
        public static object Inf00014GetList(string token, string Code, int page, int rows)
        {
            int count = 0;
            return UtilBussinessService.getPaginationModel(SYS_CalendarService.Inf00014GetList(Code, page, rows, ref count), count);
        }

        /// <summary>
        /// 行事历的新增
        /// SAM 2017年7月23日22:31:44
        /// </summary>
        /// <param name="Token"></param>
        /// <param name="jArray"></param>
        /// <returns></returns>
        public static object Inf00014insert(string Token, JArray jArray)
        {
            int success = 0;
            int fail = 0;
            string failIDs = null;
            string msg = null;
            string userid = UtilBussinessService.detoken(Token);
            JObject data = null;
            SYS_Calendar model = null;
            for (int i = 0; i < jArray.Count; i++)
            {
                data = (JObject)jArray[i];
                if (SYS_CalendarService.CheckCode(data.Value<string>("Code"), null))
                {
                    failIDs = UtilBussinessService.str(failIDs, data.Value<string>("CalendarID"));
                    msg = UtilBussinessService.str(msg, "[" + data.Value<string>("Code") + "]代号已存在！");
                    fail++;
                    continue;
                }
                model = new SYS_Calendar();
                model.CalendarID = UniversalService.GetSerialNumber("SYS_Calendar");
                model.Code = data.Value<string>("Code");
                model.Name = data.Value<string>("Name");
                model.Ifdefault = data.Value<bool>("Ifdefault");
                model.Status = data.Value<string>("Status");
                model.Comments = data.Value<string>("Comments");

                if (model.Ifdefault)
                {
                    if (SYS_CalendarService.CheckDefault(null))
                    {
                        failIDs = UtilBussinessService.str(failIDs, data.Value<string>("CalendarID"));
                        msg = UtilBussinessService.str(msg, "已存在主行事历");
                        fail++;
                        continue;
                    }
                }

                if (SYS_CalendarService.insert(userid, model))
                    success++;
                else
                {
                    failIDs = UtilBussinessService.str(failIDs, data.Value<string>("CalendarID"));
                    fail++;
                }

            }
            return new { success = success, fail = fail, failIDs = failIDs, msg = msg };
        }


        /// <summary>
        /// 行事历的编辑
        /// SAM 2017年7月23日22:32:07
        /// </summary>
        /// <param name="Token"></param>
        /// <param name="jArray"></param>
        /// <returns></returns>
        public static object Inf00014update(string Token, JArray jArray)
        {
            int success = 0;
            int fail = 0;
            string failIDs = null;
            string msg = null;
            string userid = UtilBussinessService.detoken(Token);
            JObject data = null;
            SYS_Calendar model = null;
            for (int i = 0; i < jArray.Count; i++)
            {
                data = (JObject)jArray[i];
                model = SYS_CalendarService.get(data.Value<string>("CalendarID"));
                model.Name = data.Value<string>("Name");
                model.Ifdefault = data.Value<bool>("Ifdefault");
                model.Status = data.Value<string>("Status");
                model.Comments = data.Value<string>("Comments");

                if (model.Ifdefault)
                {
                    if (SYS_CalendarService.CheckDefault(model.CalendarID))
                    {
                        failIDs = UtilBussinessService.str(failIDs, data.Value<string>("CalendarID"));
                        msg = UtilBussinessService.str(msg, "已存在主行事历");
                        fail++;
                        continue;
                    }
                }

                if (SYS_CalendarService.update(userid, model))
                    success++;
                else
                {
                    failIDs = UtilBussinessService.str(failIDs, data.Value<string>("CalendarID"));
                    fail++;
                }

            }
            return new { success = success, fail = fail, failIDs = failIDs, msg = msg };
        }



        /// <summary>
        /// 行事历的更新
        /// SAM 2017年5月17日11:38:26
        /// </summary>
        /// <param name="Token">授权码</param>
        /// <param name="jArray"></param>
        /// <returns></returns>
        public static object Inf00014Update(string Token, JObject request)
        {
            string userid = UtilBussinessService.detoken(Token);
            SYS_Calendar Model = SYS_CalendarService.get(request.Value<string>("CalendarID"));
            if (Model == null)
                return new { status = "410", msg = "流水号错误！" };

            Model.Ifdefault = request.Value<bool>("Ifdefault");
            Model.Status = request.Value<string>("Status");
            if (SYS_CalendarService.update(userid, Model))
            {
                //更新明细状态为删除
                SYS_CalendarDetailsService.updateByCalendar(userid, Model.CalendarID, Model.Status);
                return new { status = "200", msg = "编辑成功!" };
            }
            else
                return new { status = "410", msg = "编辑失败!" };

        }

        /// <summary>
        /// 行事历的删除
        /// SAM 2017年5月17日11:40:14
        /// </summary>
        /// <param name="Token">授权码</param>
        /// <param name="jArray"></param>
        /// <returns></returns>
        public static object Inf00014Delete(string Token, JObject request)
        {

            string userid = UtilBussinessService.detoken(Token);
            SYS_Calendar Model = SYS_CalendarService.get(request.Value<string>("CalendarID"));
            if (Model == null)
                return new { status = "410", msg = "流水号错误！" };

            if (SYS_WorkCenterService.inf00014Check(request.Value<string>("CalendarID")))
                return new { status = "410", msg = Model.Code + "已使用，不能删除" };

            if (SYS_CalendarDetailsService.inf00014Check(request.Value<string>("CalendarID")))
                return new { status = "410", msg = Model.Code + "已使用，不能删除" };


            Model.Status = Framework.SystemID + "0201213000003";
            if (SYS_CalendarService.update(userid, Model))
            {
                ////更新明细状态为删除
                //SYS_CalendarDetailsService.updateByCalendar(userid, Model.CalendarID, Model.Status);
                return new { status = "200", msg = "删除成功!" };
            }
            else
                return new { status = "410", msg = "删除失败!" };
        }

        /// <summary>
        /// 根据行事历流水号获取明细
        /// SAM 2017年5月8日17:59:23
        /// </summary>
        /// <param name="Token">授权码</param>
        /// <param name="CalendarID"></param>
        /// <param name="page">页码</param>
        /// <param name="rows">行数</param>
        /// <returns></returns>
        public static object Inf00014GetDetailsList(string token, string CalendarID)
        {
            return SYS_CalendarDetailsService.Inf00014GetDetailsList(CalendarID);
        }

        /// <summary>
        /// 根据行事历和年月获取那个月的具体信息
        /// SAM 2017年5月9日09:31:06
        /// </summary>
        /// <param name="Token">授权码</param>
        /// <param name="FactoryID"></param>
        /// <param name="Month"></param>
        /// <returns></returns>
        public static object Inf00014GetMonthList(string token, string CalendarID, string Month)
        {
            IList<Hashtable> lists = SYS_CalendarDetailsService.Inf00014GetMonthList(CalendarID, Month);
            int year = int.Parse(Month.Split('-')[0].ToString());
            int month = int.Parse(Month.Split('-')[1].ToString());
            string week = UtilBussinessService.GetDayByWeek(UtilBussinessService.GetDateTimeMonthFirstDay(year, month).DayOfWeek.ToString());//获取这个月的第一天是星期几
            string num = DateTime.DaysInMonth(year, month).ToString();//获取这年这月的共有多少天
            string day = null;
            foreach (Hashtable item in lists)
            {
                if (day == null)
                    day = "|" + int.Parse(item["Day"].ToString()).ToString() + "|:" + item["Wkhour"].ToString();
                else
                    day += ";|" + int.Parse(item["Day"].ToString()).ToString() + "|:" + item["Wkhour"].ToString();
            }

            return new { day = day, year = year, month = month, week = week, num = num };
        }

        ///// <summary>
        ///// 添加
        ///// SAM 2017年5月9日11:38:44
        ///// </summary>
        ///// <param name="Token">授权码</param>
        ///// <param name="request">JSON数据</param>
        ///// <returns></returns>
        //public static object Inf00014Add(string Token, JObject request)
        //{
        //    try
        //    {
        //        string userId = UtilBussinessService.detoken(Token);
        //        string Code = request.Value<string>("Code");
        //        if (!SYS_CalendarService.CheckCode(Code, null))
        //        {
        //            bool Ifdefault = request.Value<bool>("Ifdefault"); //厂别主行事历
        //            bool Iscover = request.Value<bool>("Iscover"); //是否覆盖             
        //            int startYear = int.Parse(request.Value<string>("startDate").Split('-')[0]);
        //            int startMonth = int.Parse(request.Value<string>("startDate").Split('-')[1]);
        //            int endYear = int.Parse(request.Value<string>("endDate").Split('-')[0]);
        //            int endMonth = int.Parse(request.Value<string>("endDate").Split('-')[1]);
        //            string MON = string.IsNullOrWhiteSpace(request.Value<string>("MON")) ? "0" : request.Value<string>("MON");
        //            string TUE = string.IsNullOrWhiteSpace(request.Value<string>("TUE")) ? "0" : request.Value<string>("TUE");
        //            string WED = string.IsNullOrWhiteSpace(request.Value<string>("WED")) ? "0" : request.Value<string>("WED");
        //            string THU = string.IsNullOrWhiteSpace(request.Value<string>("THU")) ? "0" : request.Value<string>("THU");
        //            string FRI = string.IsNullOrWhiteSpace(request.Value<string>("FRI")) ? "0" : request.Value<string>("FRI");
        //            string SAT = string.IsNullOrWhiteSpace(request.Value<string>("SAT")) ? "0" : request.Value<string>("SAT");
        //            string SUN = string.IsNullOrWhiteSpace(request.Value<string>("SUN")) ? "0" : request.Value<string>("SUN");
        //            SYS_Calendar Calendar = new SYS_Calendar();
        //            Calendar.CalendarID = UniversalService.GetSerialNumber("SYS_Calendar");
        //            Calendar.Ifdefault = Ifdefault;
        //            Calendar.Code = Code;
        //            Calendar.Name = request.Value<string>("Name");
        //            Calendar.Status = Framework.SystemID + "0201213000001";
        //            Calendar.Comments = request.Value<string>("Comments");
        //            SYS_CalendarDetails Detail = new SYS_CalendarDetails();
        //            Detail.CalendarDetailID = UniversalService.GetSerialNumber("SYS_CalendarDetails");
        //            Detail.CalendarID = Calendar.CalendarID;
        //            string sql = null;
        //            if (SYS_CalendarService.insert(userId, Calendar))
        //            {
        //                if (startYear == endYear)
        //                {
        //                    for (; startMonth <= endMonth; startMonth++)
        //                    {
        //                        int Month_number = DateTime.DaysInMonth(startYear, startMonth);
        //                        for (int i = 1; i <= Month_number; i++)
        //                        {
        //                            int week = UtilBussinessService.Whether_Weekend(startYear, startMonth, i);
        //                            switch (week)
        //                            {
        //                                case 0:
        //                                    Detail.Wkhour = decimal.Parse(MON);
        //                                    break;
        //                                case 1:
        //                                    Detail.Wkhour = decimal.Parse(TUE);
        //                                    break;
        //                                case 2:
        //                                    Detail.Wkhour = decimal.Parse(WED);
        //                                    break;
        //                                case 3:
        //                                    Detail.Wkhour = decimal.Parse(THU);
        //                                    break;
        //                                case 4:
        //                                    Detail.Wkhour = decimal.Parse(FRI);
        //                                    break;
        //                                case 5:
        //                                    Detail.Wkhour = decimal.Parse(SAT);
        //                                    break;
        //                                case 6:
        //                                    Detail.Wkhour = decimal.Parse(SUN);
        //                                    break;
        //                            }
        //                            Detail.Yeardate = DateTime.Parse(startYear.ToString() + "-" + startMonth.ToString().PadLeft(2, '0') + "-" + i.ToString().PadLeft(2, '0'));
        //                           if (SYS_CalendarDetailsService.CheckYeardate(Detail.Yeardate.ToString(),CalendarID))
        //                            {
        //                                if (Iscover)
        //                                {
        //                                    SYS_CalendarDetailsService.UpdateWkhour(userId, Detail.Yeardate.ToString(), Detail.Wkhour.ToString());
        //                                }
        //                                Detail.CalendarDetailID = UniversalService.GetSerialNumber("SYS_CalendarDetails");
        //                                sql += SYS_CalendarDetailsService.insertSQL(userId, Detail);

        //                            }
        //                            else
        //                            {
        //                                Detail.CalendarDetailID = UniversalService.GetSerialNumber("SYS_CalendarDetails");
        //                                sql += SYS_CalendarDetailsService.insertSQL(userId, Detail);
        //                            }
        //                        }
        //                    }
        //                    if (!string.IsNullOrWhiteSpace(sql))
        //                    {
        //                        if (UtilBussinessService.RunSQL(sql))
        //                            return new { status = "200", msg = "添加成功!" };
        //                        else
        //                            return new { status = "410", msg = "添加失败!" };
        //                    }
        //                    return new { status = "411", msg = "已存在" + startYear.ToString() + "年" + startMonth.ToString() + "月-" + endYear.ToString() + "年" + endMonth.ToString() + "月的行事历期间" };
        //                }
        //                else
        //                {
        //                    int Year = startYear;
        //                    for (; startYear <= endYear; startYear++)
        //                    {
        //                        if (startYear == Year)
        //                        {
        //                            for (; startMonth <= 12; startMonth++)
        //                            {
        //                                int Month_number = DateTime.DaysInMonth(startYear, startMonth);
        //                                for (int i = 1; i <= Month_number; i++)
        //                                {
        //                                    int week = UtilBussinessService.Whether_Weekend(startYear, startMonth, i);
        //                                    switch (week)
        //                                    {
        //                                        case 0:
        //                                            Detail.Wkhour = decimal.Parse(MON);
        //                                            break;
        //                                        case 1:
        //                                            Detail.Wkhour = decimal.Parse(TUE);
        //                                            break;
        //                                        case 2:
        //                                            Detail.Wkhour = decimal.Parse(WED);
        //                                            break;
        //                                        case 3:
        //                                            Detail.Wkhour = decimal.Parse(THU);
        //                                            break;
        //                                        case 4:
        //                                            Detail.Wkhour = decimal.Parse(FRI);
        //                                            break;
        //                                        case 5:
        //                                            Detail.Wkhour = decimal.Parse(SAT);
        //                                            break;
        //                                        case 6:
        //                                            Detail.Wkhour = decimal.Parse(SUN);
        //                                            break;
        //                                    }
        //                                    Detail.Yeardate = DateTime.Parse(startYear.ToString() + "-" + startMonth.ToString().PadLeft(2, '0') + "-" + i.ToString().PadLeft(2, '0'));
        //                                   if (SYS_CalendarDetailsService.CheckYeardate(Detail.Yeardate.ToString(),CalendarID))
        //                                    {
        //                                        if (Iscover)//如果需要覆盖，则将之前的改成新的
        //                                        {
        //                                            SYS_CalendarDetailsService.UpdateWkhour(userId, Detail.Yeardate.ToString(), Detail.Wkhour.ToString());
        //                                        }
        //                                        Detail.CalendarDetailID = UniversalService.GetSerialNumber("SYS_CalendarDetails");
        //                                        sql += SYS_CalendarDetailsService.insertSQL(userId, Detail);

        //                                    }
        //                                    else
        //                                    {
        //                                        Detail.CalendarDetailID = UniversalService.GetSerialNumber("SYS_CalendarDetails");
        //                                        sql += SYS_CalendarDetailsService.insertSQL(userId, Detail);
        //                                    }
        //                                }
        //                            }
        //                        }
        //                        else if (startYear == endYear)
        //                        {
        //                            for (int x = 1; x <= endMonth; x++)
        //                            {
        //                                int Month_number = DateTime.DaysInMonth(startYear, x);
        //                                for (int i = 1; i <= Month_number; i++)
        //                                {
        //                                    int week = UtilBussinessService.Whether_Weekend(startYear, x, i);
        //                                    switch (week)
        //                                    {
        //                                        case 0:
        //                                            Detail.Wkhour = decimal.Parse(MON);
        //                                            break;
        //                                        case 1:
        //                                            Detail.Wkhour = decimal.Parse(TUE);
        //                                            break;
        //                                        case 2:
        //                                            Detail.Wkhour = decimal.Parse(WED);
        //                                            break;
        //                                        case 3:
        //                                            Detail.Wkhour = decimal.Parse(THU);
        //                                            break;
        //                                        case 4:
        //                                            Detail.Wkhour = decimal.Parse(FRI);
        //                                            break;
        //                                        case 5:
        //                                            Detail.Wkhour = decimal.Parse(SAT);
        //                                            break;
        //                                        case 6:
        //                                            Detail.Wkhour = decimal.Parse(SUN);
        //                                            break;
        //                                    }
        //                                    Detail.Yeardate = DateTime.Parse(startYear.ToString() + "-" + x.ToString().PadLeft(2, '0') + "-" + i.ToString().PadLeft(2, '0'));
        //                                   if (SYS_CalendarDetailsService.CheckYeardate(Detail.Yeardate.ToString(),CalendarID))
        //                                    {
        //                                        if (Iscover)
        //                                        {
        //                                            SYS_CalendarDetailsService.UpdateWkhour(userId, Detail.Yeardate.ToString(), Detail.Wkhour.ToString());
        //                                        }
        //                                        Detail.CalendarDetailID = UniversalService.GetSerialNumber("SYS_CalendarDetails");
        //                                        sql += SYS_CalendarDetailsService.insertSQL(userId, Detail);
        //                                    }
        //                                    else
        //                                    {
        //                                        Detail.CalendarDetailID = UniversalService.GetSerialNumber("SYS_CalendarDetails");
        //                                        sql += SYS_CalendarDetailsService.insertSQL(userId, Detail);
        //                                    }
        //                                }
        //                            }
        //                        }
        //                        else
        //                        {
        //                            for (int x = 1; x <= 12; x++)
        //                            {
        //                                int Month_number = DateTime.DaysInMonth(startYear, x);
        //                                for (int i = 1; i <= Month_number; i++)
        //                                {
        //                                    int week = UtilBussinessService.Whether_Weekend(startYear, x, i);
        //                                    switch (week)
        //                                    {
        //                                        case 0:
        //                                            Detail.Wkhour = decimal.Parse(MON);
        //                                            break;
        //                                        case 1:
        //                                            Detail.Wkhour = decimal.Parse(TUE);
        //                                            break;
        //                                        case 2:
        //                                            Detail.Wkhour = decimal.Parse(WED);
        //                                            break;
        //                                        case 3:
        //                                            Detail.Wkhour = decimal.Parse(THU);
        //                                            break;
        //                                        case 4:
        //                                            Detail.Wkhour = decimal.Parse(FRI);
        //                                            break;
        //                                        case 5:
        //                                            Detail.Wkhour = decimal.Parse(SAT);
        //                                            break;
        //                                        case 6:
        //                                            Detail.Wkhour = decimal.Parse(SUN);
        //                                            break;
        //                                    }
        //                                    Detail.Yeardate = DateTime.Parse(startYear.ToString() + "-" + x.ToString().PadLeft(2, '0') + "-" + i.ToString().PadLeft(2, '0'));
        //                                   if (SYS_CalendarDetailsService.CheckYeardate(Detail.Yeardate.ToString(),CalendarID))
        //                                    {
        //                                        if (Iscover)
        //                                        {
        //                                            SYS_CalendarDetailsService.UpdateWkhour(userId, Detail.Yeardate.ToString(), Detail.Wkhour.ToString());
        //                                        }
        //                                        Detail.CalendarDetailID = UniversalService.GetSerialNumber("SYS_CalendarDetails");
        //                                        sql += SYS_CalendarDetailsService.insertSQL(userId, Detail);

        //                                    }
        //                                    else
        //                                    {
        //                                        Detail.CalendarDetailID = UniversalService.GetSerialNumber("SYS_CalendarDetails");
        //                                        sql += SYS_CalendarDetailsService.insertSQL(userId, Detail);
        //                                    }
        //                                }
        //                            }
        //                        }
        //                    }
        //                    if (!string.IsNullOrWhiteSpace(sql))
        //                    {
        //                        if (UtilBussinessService.RunSQL(sql))
        //                            return new { status = "200", msg = "添加成功!" };
        //                        else
        //                            return new { status = "410", msg = "添加失败!" };
        //                    }
        //                    return new { status = "411", msg = "已存在" + startYear.ToString() + "年" + startMonth.ToString() + "月-" + endYear.ToString() + "年" + endMonth.ToString() + "月的行事历期间" };
        //                }
        //            }
        //            else
        //                return new { status = "410", msg = "添加失败!添加行事历表头失败！" };
        //        }
        //        return new { status = "441", msg = "添加失败!已存在行事历代码!" };
        //    }
        //    catch (Exception ex)
        //    {
        //        return new { status = "441", msg = ex.ToString() };
        //    }
        //}

        ///// <summary>
        ///// 判断指定时间内是否存在行事历期间
        ///// SAM 2017年6月13日15:06:20
        ///// </summary>
        ///// <param name="Token"></param>
        ///// <param name="request"></param>
        ///// <returns></returns>
        //public static bool Inf00014Check(string Token, JObject request)
        //{
        //    string userId = UtilBussinessService.detoken(Token);
        //    int startYear = int.Parse(request.Value<string>("startDate").Split('-')[0]);
        //    int startMonth = int.Parse(request.Value<string>("startDate").Split('-')[1]);
        //    int endYear = int.Parse(request.Value<string>("endDate").Split('-')[0]);
        //    int endMonth = int.Parse(request.Value<string>("endDate").Split('-')[1]);

        //    if (startYear == endYear)
        //    {
        //        for (; startMonth <= endMonth; startMonth++)
        //        {
        //            int Month_number = DateTime.DaysInMonth(startYear, startMonth);
        //            for (int i = 1; i <= Month_number; i++)
        //            {
        //                if (SYS_CalendarDetailsService.CheckYeardate(DateTime.Parse(startYear.ToString() + "-" + startMonth.ToString().PadLeft(2, '0') + "-" + i.ToString().PadLeft(2, '0')).ToString()))
        //                    return true;
        //            }
        //        }
        //        return false;
        //    }
        //    else
        //    {
        //        int Year = startYear;
        //        for (; startYear <= endYear; startYear++)
        //        {
        //            if (startYear == Year)
        //            {
        //                for (; startMonth <= 12; startMonth++)
        //                {
        //                    int Month_number = DateTime.DaysInMonth(startYear, startMonth);
        //                    for (int i = 1; i <= Month_number; i++)
        //                    {
        //                        if (SYS_CalendarDetailsService.CheckYeardate(DateTime.Parse(startYear.ToString() + "-" + startMonth.ToString().PadLeft(2, '0') + "-" + i.ToString().PadLeft(2, '0')).ToString()))
        //                            return true;
        //                    }
        //                }
        //            }
        //            else if (startYear == endYear)
        //            {
        //                for (int x = 1; x <= endMonth; x++)
        //                {
        //                    int Month_number = DateTime.DaysInMonth(startYear, x);
        //                    for (int i = 1; i <= Month_number; i++)
        //                    {
        //                        if (SYS_CalendarDetailsService.CheckYeardate(DateTime.Parse(startYear.ToString() + "-" + x.ToString().PadLeft(2, '0') + "-" + i.ToString().PadLeft(2, '0')).ToString()))
        //                            return true;
        //                    }
        //                }
        //            }
        //            else
        //            {
        //                for (int x = 1; x <= 12; x++)
        //                {
        //                    int Month_number = DateTime.DaysInMonth(startYear, x);
        //                    for (int i = 1; i <= Month_number; i++)
        //                    {
        //                        if (SYS_CalendarDetailsService.CheckYeardate(DateTime.Parse(startYear.ToString() + "-" + x.ToString().PadLeft(2, '0') + "-" + i.ToString().PadLeft(2, '0')).ToString()))
        //                            return true;
        //                    }
        //                }
        //            }
        //        }
        //        return false;
        //    }

        //}

        /// <summary>
        /// 更新
        /// SAM 2017年5月9日13:59:16
        /// </summary>
        /// <param name="Token">授权码</param>
        /// <param name="request">JSON数据</param>
        /// <returns></returns>
        public static object Inf00014UpdateDetails(string Token, JObject request)
        {
            string userId = UtilBussinessService.detoken(Token);
            string Date = request.Value<string>("Date");
            string CalendarID = request.Value<string>("CalendarID");
            string[] str = request.Value<string>("Days").Split(',');
            string sql = null;
            foreach (string item in str)
            {
                sql += SYS_CalendarDetailsService.updateSQL(userId, Date + "-" + item.Split(':')[0].ToString().PadLeft(2, '0'), CalendarID, String.IsNullOrWhiteSpace(item.Split(':')[1]) ? "0" : item.Split(':')[1]);
            }

            if (UtilBussinessService.RunSQL(sql))
                return new { status = "200", msg = "修改成功!" };
            else
                return new { status = "410", msg = "修改失败!" };
        }

        /// <summary>
        /// 行事历期间维护
        /// SAM 2017年7月23日22:51:49
        /// </summary>
        /// <param name="Token"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        public static object Inf00014PeriodUpdate(string Token, JObject request)
        {
            try
            {
                string userId = UtilBussinessService.detoken(Token);
                string CalendarID = request.Value<string>("CalendarID");

                SYS_Calendar Calendar = SYS_CalendarService.get(CalendarID);
                int startYear = int.Parse(request.Value<string>("startDate").Split('-')[0]);
                int startMonth = int.Parse(request.Value<string>("startDate").Split('-')[1]);
                int endYear = int.Parse(request.Value<string>("endDate").Split('-')[0]);
                int endMonth = int.Parse(request.Value<string>("endDate").Split('-')[1]);
                string MON = request.Value<string>("MON");
                string TUE = request.Value<string>("TUE");
                string WED = request.Value<string>("WED");
                string THU = request.Value<string>("THU");
                string FRI = request.Value<string>("FRI");
                string SAT = request.Value<string>("SAT");
                string SUN = request.Value<string>("SUN");

                SYS_CalendarDetails Detail = new SYS_CalendarDetails();
                Detail.CalendarDetailID = UniversalService.GetSerialNumber("SYS_CalendarDetails");
                Detail.CalendarID = Calendar.CalendarID;
                string sql = null;
                bool IsUpdate = true;
                if (startYear == endYear)
                {
                    for (; startMonth <= endMonth; startMonth++)
                    {
                        int Month_number = DateTime.DaysInMonth(startYear, startMonth);
                        for (int i = 1; i <= Month_number; i++)
                        {
                            IsUpdate = true;
                            int week = UtilBussinessService.Whether_Weekend(startYear, startMonth, i);
                            switch (week)
                            {
                                case 0:
                                    if (string.IsNullOrWhiteSpace(MON))
                                        IsUpdate = false;
                                    else
                                        Detail.Wkhour = decimal.Parse(MON);
                                    break;
                                case 1:
                                    if (string.IsNullOrWhiteSpace(TUE))
                                        IsUpdate = false;
                                    else
                                        Detail.Wkhour = decimal.Parse(TUE);
                                    break;
                                case 2:
                                    if (string.IsNullOrWhiteSpace(WED))
                                        IsUpdate = false;
                                    else
                                        Detail.Wkhour = decimal.Parse(WED);
                                    break;
                                case 3:
                                    if (string.IsNullOrWhiteSpace(THU))
                                        IsUpdate = false;
                                    else
                                        Detail.Wkhour = decimal.Parse(THU);
                                    break;
                                case 4:
                                    if (string.IsNullOrWhiteSpace(FRI))
                                        IsUpdate = false;
                                    else
                                        Detail.Wkhour = decimal.Parse(FRI);
                                    break;
                                case 5:
                                    if (string.IsNullOrWhiteSpace(SAT))
                                        IsUpdate = false;
                                    else
                                        Detail.Wkhour = decimal.Parse(SAT);
                                    break;
                                case 6:
                                    if (string.IsNullOrWhiteSpace(SUN))
                                        IsUpdate = false;
                                    else
                                        Detail.Wkhour = decimal.Parse(SUN);
                                    break;
                            }
                            if (!IsUpdate)
                                Detail.Wkhour = 0;
                            Detail.Yeardate = DateTime.Parse(startYear.ToString() + "-" + startMonth.ToString().PadLeft(2, '0') + "-" + i.ToString().PadLeft(2, '0'));
                            if (SYS_CalendarDetailsService.CheckYeardate(Detail.Yeardate.ToString(), CalendarID))
                            {
                                if (IsUpdate)
                                    SYS_CalendarDetailsService.UpdateWkhour(userId, Detail.Yeardate.ToString(), Detail.Wkhour.ToString(), CalendarID);
                            }
                            else
                            {
                                Detail.CalendarDetailID = UniversalService.GetSerialNumber("SYS_CalendarDetails");
                                sql += SYS_CalendarDetailsService.insertSQL(userId, Detail);
                            }
                        }
                    }
                    if (!string.IsNullOrWhiteSpace(sql))
                    {
                        if (UtilBussinessService.RunSQL(sql))
                            return new { status = "200", msg = "保存成功!" };
                        else
                            return new { status = "410", msg = "保存失败!" };
                    }
                    return new { status = "200", msg = "保存成功!" };
                }
                else
                {
                    int Year = startYear;
                    for (; startYear <= endYear; startYear++)
                    {
                        if (startYear == Year)
                        {
                            for (; startMonth <= 12; startMonth++)
                            {
                                int Month_number = DateTime.DaysInMonth(startYear, startMonth);
                                for (int i = 1; i <= Month_number; i++)
                                {
                                    IsUpdate = true; //当工时为空时，默认不改
                                    int week = UtilBussinessService.Whether_Weekend(startYear, startMonth, i);
                                    switch (week)
                                    {
                                        case 0:
                                            if (string.IsNullOrWhiteSpace(MON))
                                                IsUpdate = false;
                                            else
                                                Detail.Wkhour = decimal.Parse(MON);
                                            break;
                                        case 1:
                                            if (string.IsNullOrWhiteSpace(TUE))
                                                IsUpdate = false;
                                            else
                                                Detail.Wkhour = decimal.Parse(TUE);
                                            break;
                                        case 2:
                                            if (string.IsNullOrWhiteSpace(WED))
                                                IsUpdate = false;
                                            else
                                                Detail.Wkhour = decimal.Parse(WED);
                                            break;
                                        case 3:
                                            if (string.IsNullOrWhiteSpace(THU))
                                                IsUpdate = false;
                                            else
                                                Detail.Wkhour = decimal.Parse(THU);
                                            break;
                                        case 4:
                                            if (string.IsNullOrWhiteSpace(FRI))
                                                IsUpdate = false;
                                            else
                                                Detail.Wkhour = decimal.Parse(FRI);
                                            break;
                                        case 5:
                                            if (string.IsNullOrWhiteSpace(SAT))
                                                IsUpdate = false;
                                            else
                                                Detail.Wkhour = decimal.Parse(SAT);
                                            break;
                                        case 6:
                                            if (string.IsNullOrWhiteSpace(SUN))
                                                IsUpdate = false;
                                            else
                                                Detail.Wkhour = decimal.Parse(SUN);
                                            break;
                                    }
                                    if (!IsUpdate)
                                        Detail.Wkhour = 0;
                                    Detail.Yeardate = DateTime.Parse(startYear.ToString() + "-" + startMonth.ToString().PadLeft(2, '0') + "-" + i.ToString().PadLeft(2, '0'));
                                    if (SYS_CalendarDetailsService.CheckYeardate(Detail.Yeardate.ToString(), CalendarID))
                                    {
                                        if (IsUpdate)
                                            SYS_CalendarDetailsService.UpdateWkhour(userId, Detail.Yeardate.ToString(), Detail.Wkhour.ToString(), CalendarID);
                                    }
                                    else
                                    {
                                        Detail.CalendarDetailID = UniversalService.GetSerialNumber("SYS_CalendarDetails");
                                        sql += SYS_CalendarDetailsService.insertSQL(userId, Detail);
                                    }
                                }
                            }
                        }
                        else if (startYear == endYear)
                        {
                            for (int x = 1; x <= endMonth; x++)
                            {
                                int Month_number = DateTime.DaysInMonth(startYear, x);
                                for (int i = 1; i <= Month_number; i++)
                                {
                                    IsUpdate = true;
                                    int week = UtilBussinessService.Whether_Weekend(startYear, x, i);
                                    switch (week)
                                    {
                                        case 0:
                                            if (string.IsNullOrWhiteSpace(MON))
                                                IsUpdate = false;
                                            else
                                                Detail.Wkhour = decimal.Parse(MON);
                                            break;
                                        case 1:
                                            if (string.IsNullOrWhiteSpace(TUE))
                                                IsUpdate = false;
                                            else
                                                Detail.Wkhour = decimal.Parse(TUE);
                                            break;
                                        case 2:
                                            if (string.IsNullOrWhiteSpace(WED))
                                                IsUpdate = false;
                                            else
                                                Detail.Wkhour = decimal.Parse(WED);
                                            break;
                                        case 3:
                                            if (string.IsNullOrWhiteSpace(THU))
                                                IsUpdate = false;
                                            else
                                                Detail.Wkhour = decimal.Parse(THU);
                                            break;
                                        case 4:
                                            if (string.IsNullOrWhiteSpace(FRI))
                                                IsUpdate = false;
                                            else
                                                Detail.Wkhour = decimal.Parse(FRI);
                                            break;
                                        case 5:
                                            if (string.IsNullOrWhiteSpace(SAT))
                                                IsUpdate = false;
                                            else
                                                Detail.Wkhour = decimal.Parse(SAT);
                                            break;
                                        case 6:
                                            if (string.IsNullOrWhiteSpace(SUN))
                                                IsUpdate = false;
                                            else
                                                Detail.Wkhour = decimal.Parse(SUN);
                                            break;
                                    }
                                    if (!IsUpdate)
                                        Detail.Wkhour = 0;
                                    Detail.Yeardate = DateTime.Parse(startYear.ToString() + "-" + x.ToString().PadLeft(2, '0') + "-" + i.ToString().PadLeft(2, '0'));
                                    if (SYS_CalendarDetailsService.CheckYeardate(Detail.Yeardate.ToString(), CalendarID))
                                    {
                                        if (IsUpdate)
                                            SYS_CalendarDetailsService.UpdateWkhour(userId, Detail.Yeardate.ToString(), Detail.Wkhour.ToString(), CalendarID);
                                    }
                                    else
                                    {
                                        Detail.CalendarDetailID = UniversalService.GetSerialNumber("SYS_CalendarDetails");
                                        sql += SYS_CalendarDetailsService.insertSQL(userId, Detail);
                                    }
                                }
                            }
                        }
                        else
                        {
                            for (int x = 1; x <= 12; x++)
                            {
                                int Month_number = DateTime.DaysInMonth(startYear, x);
                                for (int i = 1; i <= Month_number; i++)
                                {
                                    IsUpdate = true;
                                    int week = UtilBussinessService.Whether_Weekend(startYear, x, i);
                                    switch (week)
                                    {
                                        case 0:
                                            if (string.IsNullOrWhiteSpace(MON))
                                                IsUpdate = false;
                                            else
                                                Detail.Wkhour = decimal.Parse(MON);
                                            break;
                                        case 1:
                                            if (string.IsNullOrWhiteSpace(TUE))
                                                IsUpdate = false;
                                            else
                                                Detail.Wkhour = decimal.Parse(TUE);
                                            break;
                                        case 2:
                                            if (string.IsNullOrWhiteSpace(WED))
                                                IsUpdate = false;
                                            else
                                                Detail.Wkhour = decimal.Parse(WED);
                                            break;
                                        case 3:
                                            if (string.IsNullOrWhiteSpace(THU))
                                                IsUpdate = false;
                                            else
                                                Detail.Wkhour = decimal.Parse(THU);
                                            break;
                                        case 4:
                                            if (string.IsNullOrWhiteSpace(FRI))
                                                IsUpdate = false;
                                            else
                                                Detail.Wkhour = decimal.Parse(FRI);
                                            break;
                                        case 5:
                                            if (string.IsNullOrWhiteSpace(SAT))
                                                IsUpdate = false;
                                            else
                                                Detail.Wkhour = decimal.Parse(SAT);
                                            break;
                                        case 6:
                                            if (string.IsNullOrWhiteSpace(SUN))
                                                IsUpdate = false;
                                            else
                                                Detail.Wkhour = decimal.Parse(SUN);
                                            break;
                                    }
                                    if (!IsUpdate)
                                        Detail.Wkhour = 0;
                                    Detail.Yeardate = DateTime.Parse(startYear.ToString() + "-" + x.ToString().PadLeft(2, '0') + "-" + i.ToString().PadLeft(2, '0'));
                                    if (SYS_CalendarDetailsService.CheckYeardate(Detail.Yeardate.ToString(), CalendarID))
                                    {
                                        if (IsUpdate)
                                            SYS_CalendarDetailsService.UpdateWkhour(userId, Detail.Yeardate.ToString(), Detail.Wkhour.ToString(), CalendarID);
                                    }
                                    else
                                    {
                                        Detail.CalendarDetailID = UniversalService.GetSerialNumber("SYS_CalendarDetails");
                                        sql += SYS_CalendarDetailsService.insertSQL(userId, Detail);
                                    }
                                }
                            }
                        }
                    }
                    if (!string.IsNullOrWhiteSpace(sql))
                    {
                        if (UtilBussinessService.RunSQL(sql))
                            return new { status = "200", msg = "保存成功!" };
                        else
                            return new { status = "410", msg = "保存失败!" };
                    }
                    return new { status = "200", msg = "保存成功!" };
                }

            }
            catch (Exception ex)
            {
                return new { status = "441", msg = ex.ToString() };
            }
        }

        /// <summary>
        /// 判断是否已存在主行事历
        /// SAM 2017年7月28日11:53:16
        /// </summary>
        /// <param name="Token"></param>
        /// <returns></returns>
        public static bool Inf00014CheckIfdefault(string Token)
        {
            return SYS_CalendarService.Inf00014CheckIfdefault();
        }


        #endregion

        #region Inf00015资源主档
        /// <summary>
        /// 资源类别列表
        /// SAM 2017年5月12日10:39:05
        /// </summary>
        /// <param name="Token">授权码</param>
        /// <param name="Code">代号</param>
        /// <param name="Status">状态</param>
        /// <param name="page">页码</param>
        /// <param name="rows">行数</param>
        /// <returns></returns>
        public static object Inf00015ClassList(string token, string code, string Status, int page, int rows)
        {
            int count = 0;
            return UtilBussinessService.getPaginationModel(SYS_ParameterService.Inf00015ClassList(code, Status, page, rows, ref count), count);
        }

        /// <summary>
        /// 资源类别的单一删除
        /// SAM 2017年7月27日22:08:44
        /// </summary>
        /// <param name="Token"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        public static object Inf00015ClassDelete(string Token, JObject request)
        {
            string userid = UtilBussinessService.detoken(Token);
            SYS_Parameters model = SYS_ParameterService.get(request.Value<string>("ParameterID"));
            if (model == null)
                return new { status = "410", msg = "不存在的资源类别信息！" };

            if (SYS_ResourcesService.CheckClass(request.Value<string>("ParameterID")))
                return new { status = "410", msg = model.Code + "已使用，不能删除" };

            if(!model.IsSystem)
                return new { status = "410", msg = model.Code + "为系统预设数据，不能删除" };

            model.IsEnable = 2;
            if (SYS_ParameterService.update(userid, model))
                return new { status = "200", msg = "删除成功!" };
            else
                return new { status = "410", msg = "删除失败!" };
        }

        /// <summary>
        /// 资源群组列表
        /// SAM 2017年5月12日10:59:31
        /// </summary>
        /// <param name="Token">授权码</param>
        /// <param name="Code">代号</param>
        /// <param name="Status">状态</param>
        /// <param name="page">页码</param>
        /// <param name="rows">行数</param>
        /// <returns></returns>
        public static object Inf00015GroupList(string token, string code, string Status, int page, int rows)
        {
            int count = 0;
            return UtilBussinessService.getPaginationModel(SYS_ParameterService.Inf00015GroupList(code, Status, page, rows, ref count), count);
        }

        /// <summary>
        /// 资源群组的单一删除
        /// SAM 2017年7月27日22:11:56
        /// </summary>
        /// <param name="Token"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        public static object Inf00015GroupDelete(string Token, JObject request)
        {
            string userid = UtilBussinessService.detoken(Token);
            SYS_Parameters model = SYS_ParameterService.get(request.Value<string>("ParameterID"));
            if (model == null)
                return new { status = "410", msg = "流水号错误！" };

            if (SYS_ResourcesService.CheckGroup(request.Value<string>("ParameterID")))
                return new { status = "410", msg = model.Code + "已使用，不能删除" };

            model.IsEnable = 2;
            if (SYS_ParameterService.update(userid, model))
                return new { status = "200", msg = "删除成功!" };
            else
                return new { status = "410", msg = "删除失败!" };
        }

        /// <summary>
        /// 资源列表
        /// SAM 2017年5月12日11:18:30
        /// </summary>
        /// <param name="Token">授权码</param>
        /// <param name="Code">代号</param>
        /// <param name="Status">状态</param>
        /// <param name="page">页码</param>
        /// <param name="rows">行数</param>
        /// <returns></returns>
        public static object Inf00015GetList(string token, string code, string Status, int page, int rows)
        {
            int count = 0;
            return UtilBussinessService.getPaginationModel(SYS_ResourcesService.Inf00015GetList(code, Status, page, rows, ref count), count);
        }

        /// <summary>
        /// 新增资源
        /// SAM 2017年5月12日11:42:25
        /// </summary>
        /// <param name="Token">授权码</param>
        /// <param name="jArray"></param>
        /// <param name="Type"></param>
        /// <returns></returns>
        public static object Inf00015insert(string Token, JArray jArray)
        {
            int success = 0;
            int fail = 0;
            string failIDs = null;
            string msg = null;
            string userid = UtilBussinessService.detoken(Token);
            JObject data = null;
            SYS_Resources model = null;
            for (int i = 0; i < jArray.Count; i++)
            {
                data = (JObject)jArray[i];
                if (!SYS_ResourcesService.CheckCode(data.Value<string>("Code"), null))
                {
                    model = new SYS_Resources();
                    model.ResourceID = UniversalService.GetSerialNumber("SYS_Resources");
                    model.Code = data.Value<string>("Code");  //代号
                    model.Description = data.Value<string>("Description");//说明
                    model.Comments = data.Value<string>("Comments");
                    model.Status = data.Value<string>("Status"); //状态       
                    model.ClassID = data.Value<string>("ClassID");
                    model.GroupID = data.Value<string>("GroupID");
                    model.Quantity = data.Value<decimal>("Quantity");
                    if (SYS_ResourcesService.insert(userid, model))
                        success++;
                    else
                    {
                        failIDs = UtilBussinessService.str(failIDs, data.Value<string>("ResourceID"));
                        fail++;
                    }
                }
                else
                {
                    failIDs = UtilBussinessService.str(failIDs, data.Value<string>("ResourceID"));
                    msg = UtilBussinessService.str(msg, data.Value<string>("Code") + "已存在");
                    fail++;
                }

            }
            return new { success = success, fail = fail, failIDs = failIDs, msg = msg };
        }

        /// <summary>
        /// 资源的单一删除
        /// SAM 2017年7月27日22:13:13
        /// </summary>
        /// <param name="Token"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        public static object Inf00015Delete(string Token, JObject request)
        {
            string userid = UtilBussinessService.detoken(Token);
            string ResourceID = request.Value<string>("ResourceID");
            SYS_Resources model = SYS_ResourcesService.get(ResourceID);
            if (model == null)
                return new { status = "410", msg = "不存在的资源信息！" };

            if (SYS_WorkCenterResourcesService.CheckResource(ResourceID))
                return new { status = "410", msg = model.Code + "已使用，不能删除" };
            if (SFC_FabMoResourceService.CheckResource(ResourceID))                  // MOUSE 2017年7月28日16:14:26 增加筛选条件
                return new { status = "410", msg = model.Code + "已使用，不能删除" };
            model.Status = Framework.SystemID + "0201213000003";
            if (SYS_ResourcesService.update(userid, model))
                return new { status = "200", msg = "删除成功!" };
            else
                return new { status = "410", msg = "删除失败!" };
        }

        /// <summary>
        /// 更新资源
        /// SAM 2017年5月12日11:42:47
        /// </summary>
        /// <param name="Token">授权码</param>
        /// <param name="jArray"></param>
        /// <returns></returns>
        public static object Inf00015update(string Token, JArray jArray)
        {
            int success = 0;
            int fail = 0;
            string failIDs = null;
            string msg = null;
            string userid = UtilBussinessService.detoken(Token);
            JObject data = null;
            SYS_Resources model = null;
            for (int i = 0; i < jArray.Count; i++)
            {
                data = (JObject)jArray[i];
                model = SYS_ResourcesService.get(data.Value<string>("ResourceID"));
                model.Status = data.Value<string>("Status"); //状态
                model.GroupID = data.Value<string>("GroupID");
                model.Quantity = data.Value<decimal>("Quantity");
                model.Description = data.Value<string>("Description");//说明
                model.Comments = data.Value<string>("Comments");//备注
                if (SYS_ResourcesService.update(userid, model))
                    success++;
                else
                {
                    failIDs = UtilBussinessService.str(failIDs, data.Value<string>("ResourceID"));
                    fail++;
                }

            }
            return new { success = success, fail = fail, failIDs = failIDs, msg = msg };
        }

        /// <summary>
        /// 资源明细列表
        /// SAM 2017年5月12日11:18:30
        /// </summary>
        /// <param name="Token">授权码</param>
        /// <param name="Code">代号</param>
        /// <param name="Status">状态</param>
        /// <param name="page">页码</param>
        /// <param name="rows">行数</param>
        /// <returns></returns>
        public static object Inf00015GetDetailsList(string token, string ResourceID, int page, int rows)
        {
            int count = 0;
            SYS_Resources Model = SYS_ResourcesService.get(ResourceID);
            if (Model == null)
                return null;
            SYS_Parameters parModel = SYS_ParameterService.get(Model.ClassID);
            if (parModel == null)
                return null;
            if (parModel.Code.Contains("L"))
                return UtilBussinessService.getPaginationModel(SYS_ResourceDetailsService.Inf00015GetLDetailsList(ResourceID, page, rows, ref count), count);
            else
                return UtilBussinessService.getPaginationModel(SYS_ResourceDetailsService.Inf00015GetDetailsList(ResourceID, page, rows, ref count), count);
        }


        /// <summary>
        /// 新增资源明细
        /// SAM 2017年5月12日11:42:25
        /// </summary>
        /// <param name="Token">授权码</param>
        /// <param name="jArray"></param>
        /// <param name="Type"></param>
        /// <returns></returns>
        public static object Inf00015Detailsinsert(string Token, JArray jArray)
        {
            int success = 0;
            int fail = 0;
            string failIDs = null;
            string msg = null;
            string userid = UtilBussinessService.detoken(Token);
            JObject data = null;
            SYS_ResourceDetails model = null;
            for (int i = 0; i < jArray.Count; i++)
            {
                data = (JObject)jArray[i];
                if (!SYS_ResourceDetailsService.CheckDetail(data.Value<string>("DetailID"), data.Value<string>("ResourceID"), null))
                {
                    model = new SYS_ResourceDetails();
                    model.ResourceDetailID = UniversalService.GetSerialNumber("SYS_ResourceDetails");
                    model.ResourceID = data.Value<string>("ResourceID");
                    model.DetailID = data.Value<string>("DetailID");
                    model.Comments = data.Value<string>("Comments");
                    model.Status = Framework.SystemID + "0201213000001";
                    if (SYS_ResourceDetailsService.insert(userid, model))
                        success++;
                    else
                    {
                        failIDs = UtilBussinessService.str(failIDs, data.Value<string>("ResourceDetailID"));
                        fail++;
                    }
                }
                else
                {
                    failIDs = UtilBussinessService.str(failIDs, data.Value<string>("ResourceDetailID"));
                    msg = UtilBussinessService.str(msg, data.Value<string>("Code") + "已存在");
                    fail++;
                }

            }
            return new { success = success, fail = fail, failIDs = failIDs, msg = msg };
        }

        /// <summary>
        /// 删除资源明细
        /// SAM 2017年5月12日11:42:39
        /// </summary>
        /// <param name="Token">授权码</param>
        /// <param name="jArray"></param>
        /// <returns></returns>
        public static object Inf00015Detailsdelete(string Token, JArray jArray)
        {
            int success = 0;
            int fail = 0;
            string failIDs = null;
            string msg = null;
            string userid = UtilBussinessService.detoken(Token);
            JObject data = null;
            SYS_ResourceDetails model = null;
            for (int i = 0; i < jArray.Count; i++)
            {
                data = (JObject)jArray[i];
                model = SYS_ResourceDetailsService.get(data.Value<string>("ResourceDetailID"));
                model.Status = Framework.SystemID + "0201213000003";
                if (SYS_ResourceDetailsService.update(userid, model))
                    success++;
                else
                {
                    failIDs = UtilBussinessService.str(failIDs, data.Value<string>("ResourceDetailID"));
                    fail++;
                }
            }
            return new { success = success, fail = fail, failIDs = failIDs, msg = msg };
        }

        /// <summary>
        /// 根据资源流水号获取资源明细（不分页）
        /// SAM 2017年7月30日22:24:53
        /// </summary>
        /// <param name="token"></param>
        /// <param name="ResourceID"></param>
        /// <returns></returns>
        public static IList<Hashtable> Inf00015DetailList(string token, string ResourceID)
        {
            IList<Hashtable> result = null;
            SYS_Resources model = SYS_ResourcesService.get(ResourceID);
            if (model == null)
                return result;
            SYS_Parameters ParModel = SYS_ParameterService.get(model.ClassID);
            if (ParModel == null)
                return result;

            if (ParModel.Code == "L")
            {
                result = SYS_ResourceDetailsService.Inf00015LDetailList(ResourceID);
                foreach (Hashtable item in result)
                {
                    item["Status"] = UtilBussinessService.GetStatus(item["Status"].ToString());
                }
            }
            else
                result = SYS_ResourceDetailsService.Inf00015MDetailList(ResourceID);

            return result;
        }

        /// <summary>
        /// 根据资源流水号获取不属于他的资源明细（不分页）
        /// SAM 2017年7月30日23:02:06
        /// </summary>
        /// <param name="token"></param>
        /// <param name="ResourceID"></param>
        /// <returns></returns>
        public static IList<Hashtable> Inf00015NoDetailList(string token, string ResourceID)
        {
            IList<Hashtable> result = null;
            SYS_Resources model = SYS_ResourcesService.get(ResourceID);
            if (model == null)
                return result;
            SYS_Parameters ParModel = SYS_ParameterService.get(model.ClassID);
            if (ParModel == null)
                return result;

            if (ParModel.Code == "L")
            {
                result = SYS_ResourceDetailsService.Inf00015NoLDetailList(ResourceID);
                foreach (Hashtable item in result)
                {
                    item["Status"] = UtilBussinessService.GetStatus(item["Status"].ToString());
                }
            }
            else
                result = SYS_ResourceDetailsService.Inf00015NoMDetailList(ResourceID);

            return result;

        }

        /// <summary>
        /// 根据资源流水号获取不属于他的资源明细列表（不分页）
        /// SAM 2017年10月26日21:59:17 
        /// 在原来的版本上，加多了一个代号的查询。
        /// </summary>
        /// <param name="token"></param>
        /// <param name="ResourceID"></param>
        /// <param name="Code"></param>
        /// <returns></returns>
        public static IList<Hashtable> Inf00015NoDetailListV1(string token, string ResourceID,string Code)
        {
            IList<Hashtable> result = null;
            SYS_Resources model = SYS_ResourcesService.get(ResourceID);
            if (model == null)
                return result;
            SYS_Parameters ParModel = SYS_ParameterService.get(model.ClassID);
            if (ParModel == null)
                return result;

            if (ParModel.Code == "L")
            {
                result = SYS_ResourceDetailsService.Inf00015NoLDetailListV1(ResourceID, Code);
                foreach (Hashtable item in result)
                {
                    item["Status"] = UtilBussinessService.GetStatus(item["Status"].ToString());
                }
            }
            else
                result = SYS_ResourceDetailsService.Inf00015NoMDetailListV1(ResourceID, Code);

            return result;

        }

        /// <summary>
        /// 保存资源明细
        /// SAM 2017年7月30日23:02:06
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public static object Inf00015DetailSave(JObject request)
        {
            string Token = request.Value<string>("Token");
            string userid = UtilBussinessService.detoken(Token);
            string ResourceID = request.Value<string>("ResourceID");
            JArray List = request.Value<JArray>("data");

            if (string.IsNullOrWhiteSpace(ResourceID))
                return new { status = "410", msg = "ResourceID为空!" };

            JObject data = null;
            Hashtable AddModel = null;
            SYS_ResourceDetails model = null;
            string New = null;
            List<Hashtable> Add = new List<Hashtable>();
            for (int i = 0; i < List.Count; i++) //将新增的和已存在的分开
            {
                data = (JObject)List[i];
                AddModel = new Hashtable();
                if (string.IsNullOrWhiteSpace(data.Value<string>("ResourceDetailID")))
                {
                    AddModel["DetailID"] = data.Value<string>("DetailID");
                    AddModel["ResourceID"] = ResourceID;
                    Add.Add(AddModel);
                }
                else
                {
                    if (New == null)
                        New = data.Value<string>("ResourceDetailID");
                    else
                        New = New + "','" + data.Value<string>("ResourceDetailID");
                }
            }
            SYS_ResourceDetailsService.Delete(userid, New, ResourceID);

            foreach (Hashtable item in Add) //循环添加新增的
            {
                model = new SYS_ResourceDetails();
                model.ResourceDetailID = UniversalService.GetSerialNumber("SYS_ResourceDetails");
                model.ResourceID = ResourceID;
                model.DetailID = item["DetailID"].ToString();
                model.Status = Framework.SystemID + "0201213000001";
                if (!SYS_ResourceDetailsService.CheckDetail(model.DetailID, model.ResourceID))
                    SYS_ResourceDetailsService.insert(userid, model);
            }

            return new { status = "200", msg = "保存成功！" };
        }





        #endregion

        #region Inf00016單據自動編號維護
        /// <summary>
        /// 获取單據種別的列表
        /// SAM 2017年5月29日23:46:38
        /// </summary>
        /// <param name="Token">授权码</param>
        /// <param name="Code">代号</param>
        /// <param name="Status">状态</param>
        /// <param name="page">页码</param>
        /// <param name="rows">行数</param>
        /// <returns></returns>
        public static object Inf00016GetTypeList(string token, string code, string Status, int page, int rows)
        {
            int count = 0;
            return UtilBussinessService.getPaginationModel(SYS_ParameterService.GeneralGetList("0191213000019", code, Status, page, rows, ref count), count);
        }

        /// <summary>
        /// 单据类别设定表
        /// SAM 2017年6月1日15:24:34
        /// </summary>
        /// <param name="Token">授权码</param>
        /// <param name="TypeCode"></param>
        /// <param name="Code">代号</param>
        /// <param name="Status">状态</param>
        /// <param name="page">页码</param>
        /// <param name="rows">行数</param>
        /// <returns></returns>
        public static object Inf00016GetList(string token, string TypeCode, string code, string Status, int page, int rows)
        {
            int count = 0;
            return UtilBussinessService.getPaginationModel(SYS_DocumentTypeSettingService.Inf00016GetList(TypeCode, code, Status, page, rows, ref count), count);
        }

        /// <summary>
        /// 单据类别设定新增
        /// SAM 2017年6月1日15:38:19
        /// </summary>
        /// <param name="Token">授权码</param>
        /// <param name="jArray"></param>
        /// <returns></returns>
        public static object Inf00016insert(string Token, JArray jArray)
        {
            int success = 0;
            int fail = 0;
            string failIDs = null;
            string msg = null;
            string userid = UtilBussinessService.detoken(Token);
            JObject data = null;
            SYS_DocumentTypeSetting model = null;
            for (int i = 0; i < jArray.Count; i++)
            {
                data = (JObject)jArray[i];
                model = new SYS_DocumentTypeSetting();
                model.DTSID = UniversalService.GetSerialNumber("SYS_DocumentTypeSetting");
                model.Code = data.Value<string>("Code");
                model.Name = data.Value<string>("Name");
                model.Status = data.Value<string>("Status");
                model.TypeID = data.Value<string>("TypeID");
                model.IfDefault = data.Value<bool>("IfDefault");
                model.GiveWay = data.Value<string>("GiveWay");
                model.YearLength = data.Value<int>("YearLength");
                model.MonthLength = data.Value<int>("MonthLength");
                model.DateLength = data.Value<int>("DateLength");
                model.Attribute = data.Value<bool>("Attribute");
                model.CodeLength = data.Value<int>("CodeLength");
                model.YearType = "0201213000033";
                model.Comments = data.Value<string>("Comments");
                model.Length = model.Code.Length + model.YearLength + model.MonthLength + model.DateLength + model.CodeLength;
                if (!SYS_DocumentTypeSettingService.CheckCode(data.Value<string>("Code"), null))
                {

                    if (SYS_DocumentTypeSettingService.insert(userid, model))
                        success++;
                    else
                    {
                        failIDs = UtilBussinessService.str(failIDs, data.Value<string>("DTSID"));
                        fail++;
                    }
                }
                else
                {
                    failIDs = UtilBussinessService.str(failIDs, data.Value<string>("DTSID"));
                    msg = UtilBussinessService.str(msg, data.Value<string>("Code") + "已存在");
                    fail++;
                }

            }
            return new { success = success, fail = fail, failIDs = failIDs, msg = msg };
        }

        /// <summary>
        /// 单据类别设定删除
        /// SAM 2017年6月1日15:54:35
        /// </summary>
        /// <param name="Token">授权码</param>
        /// <param name="jArray"></param>
        /// <returns></returns>
        public static object Inf00016delete(string Token, JArray jArray)
        {
            int success = 0;
            int fail = 0;
            string failIDs = null;
            string msg = null;
            string userid = UtilBussinessService.detoken(Token);
            JObject data = null;
            SYS_DocumentTypeSetting model = null;
            for (int i = 0; i < jArray.Count; i++)
            {
                data = (JObject)jArray[i];
                model = SYS_DocumentTypeSettingService.get(data.Value<string>("DTSID"));

                if (!SYS_DocumentAutoNumberService.Check(data.Value<string>("DTSID")))
                {
                    model.Status = Framework.SystemID + "0201213000003";
                    if (SYS_DocumentTypeSettingService.update(userid, model))
                        success++;
                    else
                    {
                        failIDs = UtilBussinessService.str(failIDs, data.Value<string>("DTSID"));
                        fail++;
                    }
                }
                else
                {
                    failIDs = UtilBussinessService.str(failIDs, data.Value<string>("DTSID"));
                    msg = UtilBussinessService.str(msg, model.Code + "已使用，不能删除");
                    fail++;
                }
            }
            return new { success = success, fail = fail, failIDs = failIDs, msg = msg };
        }

        /// <summary>
        /// 单据类别设定表删除
        /// Tom 2017年7月31日10:06:22
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public static object Inf00016Delete(JObject request)
        {
            string Token = request.Value<string>("Token");
            string userid = UtilBussinessService.detoken(Token);
            SYS_DocumentTypeSetting model = SYS_DocumentTypeSettingService.get(request.Value<string>("DTSID"));

            if (!SYS_DocumentAutoNumberService.Check(request.Value<string>("DTSID")))
            {
                model.Status = Framework.SystemID + "0201213000003";
                if (SYS_DocumentTypeSettingService.update(userid, model))
                {
                    return new { status = "200", msg = "删除成功" };
                }
                else
                {
                    return new { status = "400", msg = "删除失败" };
                }
            }
            else
            {
                return new { status = "400", msg = "已使用，不能删除" };
            }
        }

        /// <summary>
        /// 单据类别设定更新
        /// SAM 2017年6月1日15:54:55
        /// </summary>
        /// <param name="Token">授权码</param>
        /// <param name="jArray"></param>
        /// <returns></returns>
        public static object Inf00016update(string Token, JArray jArray)
        {
            int success = 0;
            int fail = 0;
            string failIDs = null;
            string msg = null;
            string userid = UtilBussinessService.detoken(Token);
            JObject data = null;
            SYS_DocumentTypeSetting model = null;
            for (int i = 0; i < jArray.Count; i++)
            {
                data = (JObject)jArray[i];
                model = SYS_DocumentTypeSettingService.get(data.Value<string>("DTSID"));
                model.Name = data.Value<string>("Name");
                model.Status = data.Value<string>("Status");
                model.TypeID = data.Value<string>("TypeID");
                model.IfDefault = data.Value<bool>("IfDefault");
                model.YearLength = data.Value<int>("YearLength");
                model.MonthLength = data.Value<int>("MonthLength");
                model.DateLength = data.Value<int>("DateLength");
                model.Attribute = data.Value<bool>("Attribute");
                model.CodeLength = data.Value<int>("CodeLength");
                model.YearType = data.Value<string>("YearType");
                model.Comments = data.Value<string>("Comments");
                model.Length = model.Code.Length + model.YearLength + model.MonthLength + model.DateLength + model.CodeLength;
                if (SYS_DocumentTypeSettingService.update(userid, model))
                    success++;
                else
                {
                    failIDs = UtilBussinessService.str(failIDs, data.Value<string>("DTSID"));
                    fail++;
                }

            }
            return new { success = success, fail = fail, failIDs = failIDs, msg = msg };
        }

        /// <summary>
        /// 根据类别流水号获取单据状况
        /// SAM 2017年6月1日16:04:17
        /// </summary>
        /// <param name="Token">授权码</param>
        /// <param name="DTSID"></param>
        /// <param name="page">页码</param>
        /// <param name="rows">行数</param>
        /// <returns></returns>
        public static object Inf00016GetAutoNumberList(string token, string DTSID, int page, int rows)
        {
            int count = 0;
            return UtilBussinessService.getPaginationModel(SYS_DocumentAutoNumberService.Inf00016GetAutoNumberList(DTSID, page, rows, ref count), count);
        }

        /// <summary>
        /// 根据类别流水号获取权限列表
        /// SAM 2017年6月1日16:23:59
        /// </summary>
        /// <param name="Token">授权码</param>
        /// <param name="DTSID"></param>
        /// <param name="page">页码</param>
        /// <param name="rows">行数</param>
        /// <returns></returns>
        public static object Inf00016GetAuthorityList(string token, string DTSID, int page, int rows)
        {
            SYS_DocumentTypeSetting model = SYS_DocumentTypeSettingService.get(DTSID);
            if (model == null)
                return null;
            int count = 0;
            if (model.Attribute)
                return UtilBussinessService.getPaginationModel(SYS_DocumentAuthorityService.Inf00016GetTAuthorityList(DTSID, page, rows, ref count), count);
            else
                return UtilBussinessService.getPaginationModel(SYS_DocumentAuthorityService.Inf00016GetFAuthorityList(DTSID, page, rows, ref count), count);
        }

        /// <summary>
        ///  根据类别流水号获取权限（不分页）
        ///  SAM 2017年6月1日16:41:39
        /// </summary>
        /// <param name="Token">授权码</param>
        /// <param name="DTSID"></param>
        /// <returns></returns>
        public static object Inf00016AuthorityList(string token, string DTSID)
        {
            SYS_DocumentTypeSetting model = SYS_DocumentTypeSettingService.get(DTSID);
            if (model == null)
                return null;
            if (model.Attribute)
                return SYS_DocumentAuthorityService.Inf00016GetTAuthorityList(DTSID);
            else
                return SYS_DocumentAuthorityService.Inf00016GetFAuthorityList(DTSID);
        }


        /// <summary>
        /// 根据类别代号获取不归属于他的数据
        /// SAM 2017年6月1日16:47:41
        /// </summary>
        /// <param name="Token">授权码</param>
        /// <param name="DTSID"></param>
        /// <returns></returns>
        public static object Inf00016GetNotAuthorityList(string token, string DTSID, string Code)
        {
            SYS_DocumentTypeSetting model = SYS_DocumentTypeSettingService.get(DTSID);
            if (model == null)
                return null;
            if (model.Attribute)
                return SYS_MESUserService.Inf00016GetNotAuthorityList(DTSID, Code);
            else
                return SYS_OrganizationService.Inf00016GetNotAuthorityList(DTSID, Code);
        }

        /// <summary>
        /// 保存权限控制
        /// SAM 2017年6月1日17:02:02
        /// </summary>
        /// <param name="request">JSON数据</param>
        /// <returns></returns>
        public static object Inf00016AuthoritySave(JObject request)
        {
            string Token = request.Value<string>("Token");
            string userid = UtilBussinessService.detoken(Token);
            string ClassID = request.Value<string>("ClassID");
            JArray List = request.Value<JArray>("data");

            SYS_DocumentTypeSetting Dmodel = SYS_DocumentTypeSettingService.get(ClassID);
            if (Dmodel == null)
                return new { status = "410", msg = "ClassID为空!" };

            JObject data = null;
            Hashtable AddModel = null;
            SYS_DocumentAuthority model = null;
            string New = null;
            List<Hashtable> Add = new List<Hashtable>();
            for (int i = 0; i < List.Count; i++) //将新增的和已存在的分开
            {
                data = (JObject)List[i];
                AddModel = new Hashtable();
                if (string.IsNullOrWhiteSpace(data.Value<string>("DocumentAuthorityID")))
                {
                    AddModel["AuthorityID"] = data.Value<string>("AuthorityID");
                    AddModel["ClassID"] = ClassID;
                    Add.Add(AddModel);
                }
                else
                {
                    if (New == null)
                        New = data.Value<string>("DocumentAuthorityID");
                    else
                        New = New + "','" + data.Value<string>("DocumentAuthorityID");
                }
            }
            SYS_DocumentAuthorityService.Delete(userid, New, ClassID);

            foreach (Hashtable item in Add) //循环添加新增的
            {
                model = new SYS_DocumentAuthority();
                model.DocumentAuthorityID = UniversalService.GetSerialNumber("SYS_DocumentAuthority");
                model.ClassID = ClassID;
                model.Attribute = Dmodel.Attribute;
                model.AuthorityID = item["AuthorityID"].ToString();
                model.Status = Framework.SystemID + "0201213000001";
                if (!SYS_DocumentAuthorityService.Check(model.AuthorityID, model.ClassID))
                    SYS_DocumentAuthorityService.insert(userid, model);
            }

            return new { status = "200", msg = "保存成功!" };
        }

        #endregion

        #region Inf00017原因码主档
        /// <summary>
        /// 原因群码列表
        /// SAM 2017年5月11日10:06:38
        /// </summary>
        /// <param name="Token">授权码</param>
        /// <param name="Code">代号</param>
        /// <param name="Status">状态</param>
        /// <param name="page">页码</param>
        /// <param name="rows">行数</param>
        /// <returns></returns>
        public static object Inf00017GetGroupList(string token, string code, string Status, int page, int rows)
        {
            int count = 0;
            return UtilBussinessService.getPaginationModel(SYS_ParameterService.Inf00017GetGroupList(code, Status, page, rows, ref count), count);
        }

        /// <summary>
        /// 删除原因群码
        /// SAM 2017年5月12日11:20:33
        /// </summary>
        /// <param name="Token">授权码</param>
        /// <param name="jArray"></param>
        /// <returns></returns>
        public static object Inf00017Groupdelete(string Token, JArray jArray)
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
                if (!SYS_ParameterService.Inf00017CheckGroup(data.Value<string>("ParameterID")))
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
        /// 单一删除原因群码代码
        /// Mouse 2017年7月25日15:40:02
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public static object Inf00017GroupCodeDeleted(JObject request)
        {
            string Token = request.Value<string>("Token");
            string userid = UtilBussinessService.detoken(Token);

            SYS_Parameters model = SYS_ParameterService.get(request.Value<string>("ParameterID"));
            if (model == null)
                return new { status = "410", msg = "不存在的原因群码" };

            if (SYS_ParameterService.Inf00017CheckGroup(model.ParameterID))
                return new { status = "410", msg = "已使用，无法删除！" };

            if (SYS_ParameterService.delete(userid, request.Value<string>("ParameterID")))
                return new { status = "200", msg = "删除成功！" };
            else
                return new { status = "410", msg = "删除失败！" };
        }

        /// <summary>
        /// 获取原因码列表
        /// SAM 2017年5月11日10:10:17
        /// </summary>
        /// <param name="Token">授权码</param>
        /// <param name="Code">代号</param>
        /// <param name="Status">状态</param>
        /// <param name="page">页码</param>
        /// <param name="rows">行数</param>
        /// <returns></returns>
        public static object Inf00017GetList(string token, string code, string Status, int page, int rows)
        {
            int count = 0;
            return UtilBussinessService.getPaginationModel(SYS_ParameterService.Inf00017GetList(code, Status, page, rows, ref count), count);
        }

        /// <summary>
        /// 添加原因码
        /// SAM 2017年5月11日10:10:31
        /// </summary>
        /// <param name="Token">授权码</param>
        /// <param name="jArray"></param>
        /// <returns></returns>
        public static object Inf00017insert(string Token, JArray jArray)
        {
            int success = 0;
            int fail = 0;
            string failIDs = null;
            string msg = null;
            string userid = UtilBussinessService.detoken(Token);
            JObject data = null;
            SYS_Parameters model = null;
            string Type = Framework.SystemID + "0191213000012";
            for (int i = 0; i < jArray.Count; i++)
            {
                data = (JObject)jArray[i];
                if (!SYS_ParameterService.CheckParameter(data.Value<string>("Code"), null, null, Type, null))
                {
                    model = new SYS_Parameters();
                    model.ParameterID = UniversalService.GetSerialNumber("SYS_Parameters");
                    model.ParameterTypeID = Type;
                    model.Code = data.Value<string>("Code");//代号
                    model.Name = data.Value<string>("Name");//名称
                    model.Comments = data.Value<string>("Comments");//备注
                    model.IsEnable = data.Value<int>("IsEnable"); //状态
                    model.Description = data.Value<string>("Description"); //用途
                    model.DescriptionOne = data.Value<string>("DescriptionOne"); //原因群码
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
        /// 编辑原因码
        /// SAM 2017年5月11日10:13:14
        /// </summary>
        /// <param name="Token">授权码</param>
        /// <param name="jArray"></param>
        /// <returns></returns>
        public static object Inf00017update(string Token, JArray jArray)
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
                model.IsEnable = data.Value<int>("IsEnable"); //状态
                model.Name = data.Value<string>("Name");//说明
                model.Comments = data.Value<string>("Comments");//备注
                model.DescriptionOne = data.Value<string>("DescriptionOne"); //原因群码
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
        /// 单一原因码代码删除
        /// Mouse 2017年7月25日16:08:26
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public static object Inf00017Deleted(JObject request)
        {
            string Token = request.Value<string>("Token");
            string userid = UtilBussinessService.detoken(Token);
            SYS_Parameters model = SYS_ParameterService.get(request.Value<string>("ParameterID"));
            if (model == null)
                return new { status = "410", msg = "不存在的原因码代码" };
            if (SYS_ParameterService.Inf00017Check(request.Value<string>("ParameterID")))
                return new { status = "410", msg = "删除失败！被其他主档使用" };
            if (SYS_ParameterService.delete(userid, request.Value<string>("ParameterID")))
                return new { status = "200", msg = "删除成功！" };
            else
                return new { status = "410", msg = "删除失败！" };

        }
        #endregion

        #region Inf00018工作中心主档
        /// <summary>
        /// 获取工序列表
        /// SAM 2017年5月24日16:25:11
        /// </summary>
        /// <param name="Token">授权码</param>
        /// <param name="Code">代号</param>
        /// <param name="Status">状态</param>
        /// <param name="page">页码</param>
        /// <param name="rows">行数</param>
        /// <returns></returns>
        public static object Inf00018GetOperationList(string token, string code, string Status, int page, int rows)
        {
            int count = 0;
            return UtilBussinessService.getPaginationModel(SYS_ParameterService.GeneralGetList("0191213000016", code, Status, page, rows, ref count), count);
        }

        /// <summary>
        /// 工序的单一删除
        /// Joint 2017年7月26日10:56:57
        /// </summary>
        /// <returns></returns>
        public static object Inf00018OperationDelete(JObject request)
        {
            string Token = request.Value<string>("Token");
            string userid = UtilBussinessService.detoken(Token);
            SYS_Parameters model = SYS_ParameterService.get(request.Value<string>("OperationID"));
            if (model == null)
                return new { status = "410", msg = "不存在的工序信息！" };
            if (SFC_ItemOperationService.checkOperation(request.Value<string>("OperationID")) != null)
                return new { status = "410", msg = "代号已使用，不得删除或作废！" };
            if (SFC_FabMoOperationService.checkOperation(request.Value<string>("OperationID")) != null)
                return new { status = "410", msg = "代号已使用，不得删除或作废！" };
            if (SYS_ParameterService.delete(userid, request.Value<string>("OperationID")))
            {
                SYS_ProcessOperationService.DeleteOperation(userid, request.Value<string>("OperationID"));
                return new { status = "200", msg = "删除成功！" };
            }
            else
                return new { status = "410", msg = "删除失败！" };
        }
        /// <summary>
        /// 删除工序
        /// SAM 2017年5月23日09:31:22
        /// </summary>
        /// <param name="Token">授权码</param>
        /// <param name="jArray"></param>
        /// <returns></returns>
        public static object Inf00018Operationdelete(string Token, JArray jArray)
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
                if (!SYS_ProcessOperationService.CheckOperation(data.Value<string>("ParameterID")))
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
        /// 更新工序
        /// SAM 2017年5月24日16:33:05
        /// </summary>
        /// <param name="Token">授权码</param>
        /// <param name="jArray"></param>
        /// <returns></returns>
        public static object Inf00018Operationupdate(string Token, JArray jArray)
        {
            int success = 0;
            int fail = 0;
            string failIDs = null;
            string msg = null;
            string userid = UtilBussinessService.detoken(Token);
            JObject data = null;
            SYS_Parameters model = null;
            bool IsVoid = true;
            for (int i = 0; i < jArray.Count; i++)
            {
                data = (JObject)jArray[i];
                model = SYS_ParameterService.get(data.Value<string>("ParameterID"));
                if (data.Value<int>("IsEnable") == 0) //如果是要作废
                {
                    if (SYS_ProcessOperationService.CheckOperation(data.Value<string>("ParameterID")))//判断是否在制程工序中存在
                    {
                        IsVoid = false;//是的话，不能作废
                    }
                }
                if (IsVoid)
                {
                    model.IsEnable = data.Value<int>("IsEnable"); //状态
                    model.Name = data.Value<string>("Name"); //说明
                    model.Comments = data.Value<string>("Comments");  //备注
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
                    msg = UtilBussinessService.str(msg, model.Code + "已使用，不能作废");
                    fail++;
                }

            }
            return new { success = success, fail = fail, failIDs = failIDs, msg = msg };
        }

        /// <summary>
        /// 根据工序流水号获取制程(不分页)
        /// Joint 2017年7月26日15:29:17
        /// </summary>
        /// <param name="Token"></param>
        /// <param name="OperationID"></param>
        /// <returns></returns>
        public static object Inf00018GetProcessList(string Token, string OperationID)
        {
            return SYS_ProcessOperationService.Inf00018GetProcessList(OperationID);
        }
        /// <summary>
        /// 根据工序流水号获取不属于他的制程(不分页)
        /// Joint 2017年7月26日16:01:13
        /// </summary>
        /// <param name="Token"></param>
        /// <param name="Code"></param>
        /// <param name="Name"></param>
        /// <returns></returns>
        public static object Inf00018GetNoProcessList(string Token, string OperationID)
        {
            return SYS_ParameterService.Inf00018GetNoProcessList(OperationID);
        }

        /// <summary>
        /// 保存工序的制程列表
        /// Joint 2017年7月26日16:44:48
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public static object Inf00018OperationProcessSave(JObject request)
        {
            string Token = request.Value<string>("Token");
            string userid = UtilBussinessService.detoken(Token);
            string OperationID = request.Value<string>("OperationID");
            JArray List = request.Value<JArray>("data");
            if (string.IsNullOrWhiteSpace(OperationID))
                return new { status = "410", msg = "OperationID为空!" };
            JObject data = null;
            Hashtable AddModel = null;
            SYS_ProcessOperation model = null;
            string New = null;
            List<Hashtable> Add = new List<Hashtable>();
            for (int i = 0; i < List.Count; i++)//将新增的和已存在的分开
            {
                data = (JObject)List[i];
                AddModel = new Hashtable();
                if (string.IsNullOrWhiteSpace(data.Value<string>("ProcessOperationID")))
                {
                    AddModel["ProcessID"] = data.Value<string>("ProcessID");
                    AddModel["OperationID"] = data.Value<string>("OperationID");
                    Add.Add(AddModel);
                }
                else
                {
                    if (New == null)
                        New = data.Value<string>("ProcessOperationID");
                    else
                        New = New + "','" + data.Value<string>("ProcessOperationID");
                }
            }
            SYS_ProcessOperationService.DeleteOperation(userid, New, OperationID);
            foreach (Hashtable item in Add)//循环添加新增的
            {
                model = new SYS_ProcessOperation();
                model.ProcessOperationID = UniversalService.GetSerialNumber("SYS_ProcessOperation");
                model.OperationID = OperationID;
                model.ProcessID = item["ProcessID"].ToString();
                model.Status = Framework.SystemID + "0201213000001";
                if (!SYS_ProcessOperationService.CheckProcess(model.ProcessID, model.OperationID))
                    SYS_ProcessOperationService.insert(userid, model);
            }
            return new { status = "200", msg = "保存成功！" };
        }

        /// <summary>
        /// 获取制程列表
        /// SAM 2017年5月24日16:30:48
        /// </summary>
        /// <param name="Token">授权码</param>
        /// <param name="Code">代号</param>
        /// <param name="Status">状态</param>
        /// <param name="page">页码</param>
        /// <param name="rows">行数</param>
        /// <returns></returns>
        public static object Inf00018GetProcessList(string token, string code, string Status, int page, int rows)
        {
            int count = 0;
            return UtilBussinessService.getPaginationModel(SYS_ParameterService.GeneralGetList("0191213000017", code, Status, page, rows, ref count), count);
        }

        /// <summary>
        /// 制程的单一删除
        /// Joint 2017年7月27日14:38:32
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public static object Inf00018ProcessDeleted(JObject request)
        {
            string Token = request.Value<string>("Token");
            string userid = UtilBussinessService.detoken(Token);
            SYS_Parameters model = SYS_ParameterService.get(request.Value<string>("ProcessID"));
            if (model == null)
                return new { status = "410", msg = "不存在的制程！" };
            if (SFC_ItemProcessService.CheckProcess(request.Value<string>("ProcessID")))
                return new { status = "410", msg = "代号已使用，不得删除或作废！" };
            if (SYS_AlternativeGroupDetailsService.CheckProcess(request.Value<string>("ProcessID")))
                return new { status = "410", msg = "代号已使用，不得删除或作废！" };
            if (SFC_FabMoProcessService.CheckProcess(request.Value<string>("ProcessID")))
                return new { status = "410", msg = "代号已使用，不得删除或作废！" };
            if (SYS_ParameterService.delete(userid, request.Value<string>("ProcessID")))
            {
                SYS_ProcessOperationService.DeleteProcess(userid, request.Value<string>("ProcessID"));
                SYS_WorkCenterProcessService.DeleteProcess(userid, request.Value<string>("ProcessID"));
                return new { status = "200", msg = "删除成功！" };
            }
            else
                return new { status = "410", msg = "删除失败！" };
        }

        /// <summary>
        /// 删除制程
        /// SAM 2017年5月24日16:31:40
        /// </summary>
        /// <param name="Token">授权码</param>
        /// <param name="jArray"></param>
        /// <returns></returns>
        public static object Inf00018Processdelete(string Token, JArray jArray)
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
                if (!SYS_WorkCenterProcessService.CheckProcess(data.Value<string>("ParameterID")))
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
        /// 新增制程
        /// SAM 2017年5月24日16:32:57
        /// </summary>
        /// <param name="Token">授权码</param>
        /// <param name="jArray"></param>
        /// <returns></returns>
        public static object Inf00018Processinsert(string Token, JArray jArray)
        {
            int success = 0;
            int fail = 0;
            string failIDs = null;
            string msg = null;
            string userid = UtilBussinessService.detoken(Token);
            JObject data = null;
            SYS_Parameters model = null;
            string Type = Framework.SystemID + "0191213000017";
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
                    model.IsEnable = data.Value<int>("IsEnable"); //状态       
                    model.IsDefault = data.Value<bool>("IsDefault"); //启用工序
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
        /// 更新制程
        /// SAM 2017年5月24日16:33:05
        /// </summary>
        /// <param name="Token">授权码</param>
        /// <param name="jArray"></param>
        /// <returns></returns>
        public static object Inf00018Processupdate(string Token, JArray jArray)
        {
            int success = 0;
            int fail = 0;
            string failIDs = null;
            string msg = null;
            string userid = UtilBussinessService.detoken(Token);
            JObject data = null;
            SYS_Parameters model = null;
            bool IsVoid = true;
            for (int i = 0; i < jArray.Count; i++)
            {
                data = (JObject)jArray[i];
                model = SYS_ParameterService.get(data.Value<string>("ParameterID"));
                if (data.Value<int>("IsEnable") == 0) //如果是要作废
                {
                    if (SYS_WorkCenterProcessService.CheckProcess(data.Value<string>("ParameterID")))//判断是否在工作中心制程中存在
                    {
                        IsVoid = false;//是的话，不能作废
                    }
                }

                if (IsVoid)
                {
                    model.Name = data.Value<string>("Name");//说明
                    model.Comments = data.Value<string>("Comments");
                    model.IsEnable = data.Value<int>("IsEnable"); //状态       
                    model.IsDefault = data.Value<bool>("IsDefault"); //启用工序
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
                    msg = UtilBussinessService.str(msg, model.Code + "已使用，不能作废");
                    fail++;
                }

            }
            return new { success = success, fail = fail, failIDs = failIDs, msg = msg };
        }

        /// <summary>
        /// 根据工序流水号获取制程(不分页)
        /// Joint 2017年7月27日15:02:36
        /// </summary>
        /// <param name="Token"></param>
        /// <param name="ProcessID"></param>
        /// <returns></returns>
        public static object Inf00018GetOperationList(string Token, string ProcessID)
        {
            return SYS_ProcessOperationService.Inf00018GetOperationList(ProcessID);
        }
        /// <summary>
        /// 根据工序流水号获取不属于他的制程(不分页)
        /// Joint 2017年7月27日15:02:30
        /// </summary>
        /// <param name="Token"></param>
        /// <param name="ProcessID"></param>
        /// <returns></returns>
        public static object Inf00018GetNoOperationList(string Token, string ProcessID)
        {
            return SYS_ParameterService.Inf00018GetNoOperationList(ProcessID);
        }

        /// <summary>
        /// 保存制程的工序列表
        /// Joint 2017年7月27日15:02:44
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public static object Inf00018MasterProcessOperationSave(JObject request)
        {
            string Token = request.Value<string>("Token");
            string userid = UtilBussinessService.detoken(Token);
            string ProcessID = request.Value<string>("ProcessID");
            JArray List = request.Value<JArray>("data");
            if (string.IsNullOrWhiteSpace(ProcessID))
                return new { status = "410", msg = "ProcessID为空!" };
            JObject data = null;
            Hashtable AddModel = null;
            SYS_ProcessOperation model = null;
            string New = null;
            List<Hashtable> Add = new List<Hashtable>();
            for (int i = 0; i < List.Count; i++)//将新增的和已存在的分开
            {
                data = (JObject)List[i];
                AddModel = new Hashtable();
                if (string.IsNullOrWhiteSpace(data.Value<string>("ProcessOperationID")))
                {
                    AddModel["ProcessID"] = data.Value<string>("ProcessID");
                    AddModel["OperationID"] = data.Value<string>("OperationID");
                    Add.Add(AddModel);
                }
                else
                {
                    if (New == null)
                        New = data.Value<string>("ProcessOperationID");
                    else
                        New = New + "','" + data.Value<string>("ProcessOperationID");
                }
            }
            SYS_ProcessOperationService.DeleteProcess(userid, New, ProcessID);
            foreach (Hashtable item in Add)//循环添加新增的
            {
                model = new SYS_ProcessOperation();
                model.ProcessOperationID = UniversalService.GetSerialNumber("SYS_ProcessOperation");
                model.ProcessID = ProcessID;
                model.OperationID = item["OperationID"].ToString();
                model.Status = Framework.SystemID + "0201213000001";
                if (!SYS_ProcessOperationService.CheckProcess(model.ProcessID, model.OperationID))
                    SYS_ProcessOperationService.insert(userid, model);
            }
            return new { status = "200", msg = "保存成功！" };
        }

        /// <summary>
        /// 根据制程获取制程的工序
        /// SAM 2017年5月24日17:28:59
        /// </summary>
        /// <param name="Token">授权码</param>
        /// <param name="ProcessID"></param>
        /// <param name="page">页码</param>
        /// <param name="rows">行数</param>
        /// <returns></returns>
        public static object Inf00018ProcessGetOperationList(string token, string ProcessID, int page, int rows)
        {
            int count = 0;
            return UtilBussinessService.getPaginationModel(SYS_ProcessOperationService.Inf00018ProcessGetOperationList(ProcessID, page, rows, ref count), count);
        }

        /// <summary>
        /// 删除制程工序
        /// SAM 2017年5月24日17:46:16
        /// </summary>
        /// <param name="Token">授权码</param>
        /// <param name="jArray"></param>
        /// <returns></returns>
        public static object Inf00018ProcessOperationdelete(string Token, JArray jArray)
        {
            int success = 0;
            int fail = 0;
            string failIDs = null;
            string msg = null;
            string userid = UtilBussinessService.detoken(Token);
            JObject data = null;
            SYS_ProcessOperation model = null;
            for (int i = 0; i < jArray.Count; i++)
            {
                data = (JObject)jArray[i];
                model = SYS_ProcessOperationService.get(data.Value<string>("ProcessOperationID"));

                model.Status = Framework.SystemID + "0201213000003";
                if (SYS_ProcessOperationService.update(userid, model))
                    success++;
                else
                {
                    failIDs = UtilBussinessService.str(failIDs, data.Value<string>("ProcessOperationID"));
                    fail++;
                }

            }
            return new { success = success, fail = fail, failIDs = failIDs, msg = msg };
        }

        /// <summary>
        /// 新增制程工序
        /// SAM 2017年5月24日17:47:21
        /// </summary>
        /// <param name="Token">授权码</param>
        /// <param name="jArray"></param>
        /// <returns></returns>
        public static object Inf00018ProcessOperationinsert(string Token, JArray jArray)
        {
            int success = 0;
            int fail = 0;
            string failIDs = null;
            string msg = null;
            string userid = UtilBussinessService.detoken(Token);
            JObject data = null;
            SYS_ProcessOperation model = null;
            for (int i = 0; i < jArray.Count; i++)
            {
                data = (JObject)jArray[i];
                if (!SYS_ProcessOperationService.Check(data.Value<string>("OperationID"), data.Value<string>("ProcessID"), null))
                {
                    model = new SYS_ProcessOperation();
                    model.ProcessOperationID = UniversalService.GetSerialNumber("SYS_ProcessOperation");
                    model.ProcessID = data.Value<string>("ProcessID");
                    model.OperationID = data.Value<string>("OperationID");
                    model.Status = Framework.SystemID + "0201213000001";
                    if (SYS_ProcessOperationService.insert(userid, model))
                        success++;
                    else
                    {
                        failIDs = UtilBussinessService.str(failIDs, data.Value<string>("ProcessOperationID"));
                        fail++;
                    }
                }
                else
                {
                    failIDs = UtilBussinessService.str(failIDs, data.Value<string>("ProcessOperationID"));
                    msg = UtilBussinessService.str(msg, "资料重复");
                    fail++;
                }

            }
            return new { success = success, fail = fail, failIDs = failIDs, msg = msg };
        }

        /// <summary>
        /// 更新制程工序
        /// SAM 2017年5月24日16:33:05
        /// </summary>
        /// <param name="Token">授权码</param>
        /// <param name="jArray"></param>
        /// <returns></returns>
        public static object Inf00018ProcessOperationupdate(string Token, JArray jArray)
        {
            int success = 0;
            int fail = 0;
            string failIDs = null;
            string msg = null;
            string userid = UtilBussinessService.detoken(Token);
            JObject data = null;
            SYS_ProcessOperation model = null;
            for (int i = 0; i < jArray.Count; i++)
            {
                data = (JObject)jArray[i];
                model = SYS_ProcessOperationService.get(data.Value<string>("ProcessOperationID"));
                if (!SYS_ProcessOperationService.Check(data.Value<string>("OperationID"), data.Value<string>("ProcessID"), data.Value<string>("ProcessOperationID")))
                {
                    model.OperationID = data.Value<string>("OperationID");
                    if (SYS_ProcessOperationService.update(userid, model))
                        success++;
                    else
                    {
                        failIDs = UtilBussinessService.str(failIDs, data.Value<string>("ProcessOperationID"));
                        fail++;
                    }
                }
                else
                {
                    failIDs = UtilBussinessService.str(failIDs, data.Value<string>("ProcessOperationID"));
                    msg = UtilBussinessService.str(msg, "资料重复");
                    fail++;
                }

            }
            return new { success = success, fail = fail, failIDs = failIDs, msg = msg };
        }

        /// <summary>
        /// 根据制程获取工作中心列表（不分页）
        /// Joint 2017年7月27日15:40:09
        /// </summary>
        /// <param name="Token"></param>
        /// <param name="ProcessID"></param>
        /// <returns></returns>
        public static object Inf00018GetWorkCenterProcessList(string Token, string ProcessID)
        {
            return SYS_WorkCenterProcessService.Inf00018GetWorkCenterProcessList(ProcessID);
        }

        /// <summary>
        /// 根据制程获取不属于他的工作中心列表(不分页)
        /// Joint 2017年7月27日16:26:08
        /// </summary>
        /// <param name="Token"></param>
        /// <param name="ProcessID"></param>
        /// <returns></returns>
        public static object Inf00018GetNoWorkCenterProcessList(string Token, string ProcessID)
        {
            return SYS_WorkCenterService.Inf00018GetNoWorkCenterProcessList(ProcessID);
        }

        /// <summary>
        /// 保存制程的工作中心列表
        /// Joint 2017年7月27日16:33:49
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public static object Inf00018ProcessWorkCenterSave(JObject request)
        {
            string Token = request.Value<string>("Token");
            string userid = UtilBussinessService.detoken(Token);
            string ProcessID = request.Value<string>("ProcessID");
            JArray List = request.Value<JArray>("data");
            if (string.IsNullOrWhiteSpace(ProcessID))
                return new { status = "410", msg = "ProcessID为空!" };
            JObject data = null;
            Hashtable AddModel = null;
            SYS_WorkCenterProcess model = null;
            string New = null;
            List<Hashtable> Add = new List<Hashtable>();
            for (int i = 0; i < List.Count; i++) //将新增的和已存在的分开
            {
                data = (JObject)List[i];
                AddModel = new Hashtable();
                if (string.IsNullOrWhiteSpace(data.Value<string>("WorkCenterProcessID")))
                {
                    AddModel["WorkCenterID"] = data.Value<string>("WorkCenterID");
                    AddModel["ProcessID"] = data.Value<string>("ProcessID");
                    Add.Add(AddModel);
                }
                else
                {
                    if (New == null)
                        New = data.Value<string>("WorkCenterProcessID");
                    else
                        New = New + "','" + data.Value<string>("WorkCenterProcessID");
                }
            }
            SYS_WorkCenterProcessService.Delete(userid, New, ProcessID);
            foreach (Hashtable item in Add)//循环添加新增的
            {
                model = new SYS_WorkCenterProcess();
                model.WorkCenterProcessID = UniversalService.GetSerialNumber("SYS_WorkCenterProcess");
                model.ProcessID = ProcessID;
                model.WorkCenterID = item["WorkCenterID"].ToString();
                model.Status = Framework.SystemID + "0201213000001";
                if (!SYS_WorkCenterProcessService.Check(model.ProcessID, model.WorkCenterID, null))
                    SYS_WorkCenterProcessService.insert(userid, model);
            }
            return new { status = "200", msg = "保存成功！" };
        }
        /// <summary>
        /// 获取工作中心的列表
        /// SAM 2017年5月24日17:52:09
        /// </summary>
        /// <param name="Token">授权码</param>
        /// <param name="Code">代号</param>
        /// <param name="Status">状态</param>
        /// <param name="page">页码</param>
        /// <param name="rows">行数</param>
        /// <returns></returns>
        public static object Inf00018GetWorkCenterList(string token, string code, string Status, int page, int rows)
        {
            int count = 0;
            return UtilBussinessService.getPaginationModel(SYS_WorkCenterService.Inf00018GetWorkCenterList(code, Status, page, rows, ref count), count);
        }

        /// <summary>
        /// 工作中心的单一删除
        /// Joint 2017年7月28日14:20:35
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public static object Inf00018WorkCenterDeleted(JObject request)
        {
            string Token = request.Value<string>("Token");
            string userid = UtilBussinessService.detoken(Token);
            SYS_WorkCenter model = SYS_WorkCenterService.get(request.Value<string>("WorkCenterID"));
            if (model == null)
                return new { status = "410", msg = "不存在的制程！" };
            if (SFC_ItemProcessService.CheckWorkCenter(request.Value<string>("WorkCenterID")))
                return new { status = "410", msg = "代号已使用，不得删除或作废" };
            if (SFC_FabMoProcessService.CheckWorkCenter(request.Value<string>("WorkCenterID")))
                return new { status = "410", msg = "代号已使用，不得删除或作废" };
            model.Status = Framework.SystemID + "0201213000003";
            if (SYS_WorkCenterService.update(userid, model))
            {
                SYS_WorkCenterProcessService.DeleteWorkCenter(userid, request.Value<string>("WorkCenterID"));
                SYS_WorkCenterResourcesService.DeleteWorkCenter(userid, request.Value<string>("WorkCenterID"));
                return new { status = "200", msg = "删除成功！" };
            }
            else
                return new { status = "410", msg = "删除失败！" };
        }


        /// <summary>
        /// 删除工作中心
        /// SAM 2017年5月25日09:13:18
        /// </summary>
        /// <param name="Token">授权码</param>
        /// <param name="jArray"></param>
        /// <returns></returns>
        public static object Inf00018WorkCenterdelete(string Token, JArray jArray)
        {
            int success = 0;
            int fail = 0;
            string failIDs = null;
            string msg = null;
            string userid = UtilBussinessService.detoken(Token);
            JObject data = null;
            SYS_WorkCenter model = null;
            for (int i = 0; i < jArray.Count; i++)
            {
                data = (JObject)jArray[i];
                model = SYS_WorkCenterService.get(data.Value<string>("WorkCenterID"));
                if (!SYS_WorkCenterResourcesService.CheckWorkCenter(data.Value<string>("WorkCenterID")))
                {
                    model.Status = Framework.SystemID + "0201213000003";
                    if (SYS_WorkCenterService.update(userid, model))
                        success++;
                    else
                    {
                        failIDs = UtilBussinessService.str(failIDs, data.Value<string>("WorkCenterID"));
                        fail++;
                    }
                }
                else
                {
                    failIDs = UtilBussinessService.str(failIDs, data.Value<string>("WorkCenterID"));
                    msg = UtilBussinessService.str(msg, model.Code + "已使用，不能删除");
                    fail++;
                }

            }
            return new { success = success, fail = fail, failIDs = failIDs, msg = msg };
        }

        /// <summary>
        /// 新增工作中心
        /// SAM 2017年5月25日09:13:30
        /// </summary>
        /// <param name="Token">授权码</param>
        /// <param name="jArray"></param>
        /// <returns></returns>
        public static object Inf00018WorkCenterinsert(string Token, JArray jArray)
        {
            int success = 0;
            int fail = 0;
            string failIDs = null;
            string msg = null;
            string userid = UtilBussinessService.detoken(Token);
            JObject data = null;
            SYS_WorkCenter model = null;
            for (int i = 0; i < jArray.Count; i++)
            {
                data = (JObject)jArray[i];
                if (!SYS_WorkCenterService.CheckCode(data.Value<string>("Code"), null))
                {
                    model = new SYS_WorkCenter();
                    model.WorkCenterID = UniversalService.GetSerialNumber("SYS_WorkCenter");
                    model.Code = data.Value<string>("Code");
                    model.Name = data.Value<string>("Name");
                    model.CalendarID = data.Value<string>("CalendarID");
                    model.InoutMark = data.Value<string>("InoutMark");
                    model.DepartmentID = data.Value<string>("DepartmentID");
                    model.ResourceReport = data.Value<bool>("ResourceReport");
                    model.IsClass = data.Value<bool>("EnableShift");
                    model.DispatchMode = data.Value<string>("DispatchMode");
                    model.Status = data.Value<string>("Status");
                    model.Comments = data.Value<string>("Comments");
                    if (SYS_WorkCenterService.insert(userid, model))
                        success++;
                    else
                    {
                        failIDs = UtilBussinessService.str(failIDs, data.Value<string>("WorkCenterID"));
                        fail++;
                    }
                }
                else
                {
                    failIDs = UtilBussinessService.str(failIDs, data.Value<string>("WorkCenterID"));
                    msg = UtilBussinessService.str(msg, data.Value<string>("Code") + "已存在");
                    fail++;
                }

            }
            return new { success = success, fail = fail, failIDs = failIDs, msg = msg };
        }

        /// <summary>
        /// 更新工作中心
        /// SAM 2017年5月25日09:15:13
        /// </summary>
        /// <param name="Token">授权码</param>
        /// <param name="jArray"></param>
        /// <returns></returns>
        public static object Inf00018WorkCenterupdate(string Token, JArray jArray)
        {
            int success = 0;
            int fail = 0;
            string failIDs = null;
            string msg = null;
            string userid = UtilBussinessService.detoken(Token);
            JObject data = null;
            SYS_WorkCenter model = null;
            bool update = true;
            for (int i = 0; i < jArray.Count; i++)
            {
                data = (JObject)jArray[i];
                model = SYS_WorkCenterService.get(data.Value<string>("WorkCenterID"));
                if (!SYS_WorkCenterService.CheckCode(data.Value<string>("Code"), data.Value<string>("WorkCenterID")))
                {
                    if (data.Value<string>("Status") == Framework.SystemID + "0201213000002")
                    {
                        if (SYS_WorkCenterResourcesService.CheckWorkCenter(data.Value<string>("WorkCenterID")))
                            update = false;
                    }
                    if (update)
                    {
                        model.Name = data.Value<string>("Name");
                        model.CalendarID = data.Value<string>("CalendarID");
                        model.InoutMark = data.Value<string>("InoutMark");
                        model.DepartmentID = data.Value<string>("DepartmentID");
                        model.ResourceReport = data.Value<bool>("ResourceReport");
                        model.IsClass = data.Value<bool>("EnableShift");
                        model.DispatchMode = data.Value<string>("DispatchMode");
                        model.Status = data.Value<string>("Status");
                        model.Comments = data.Value<string>("Comments");
                        if (SYS_WorkCenterService.update(userid, model))
                            success++;
                        else
                        {
                            failIDs = UtilBussinessService.str(failIDs, data.Value<string>("WorkCenterID"));
                            fail++;
                        }
                    }
                    else
                    {
                        failIDs = UtilBussinessService.str(failIDs, data.Value<string>("WorkCenterID"));
                        msg = UtilBussinessService.str(msg, data.Value<string>("Code") + "已使用,不能作废");
                        fail++;
                    }
                }
                else
                {
                    failIDs = UtilBussinessService.str(failIDs, data.Value<string>("WorkCenterID"));
                    msg = UtilBussinessService.str(msg, data.Value<string>("Code") + "已存在");
                    fail++;
                }

            }
            return new { success = success, fail = fail, failIDs = failIDs, msg = msg };
        }

        /// <summary>
        /// 根据工作中心获取制程列表
        /// SAM 2017年5月25日09:21:17
        /// </summary>
        /// <param name="Token">授权码</param>
        /// <param name="WorkCenterID"></param>
        /// <param name="page">页码</param>
        /// <param name="rows">行数</param>
        /// <returns></returns>
        public static object Inf00018WorkCenterGetProcessList(string token, string WorkCenterID, int page, int rows)
        {
            int count = 0;
            return UtilBussinessService.getPaginationModel(SYS_WorkCenterProcessService.Inf00018WorkCenterGetProcessList(WorkCenterID, page, rows, ref count), count);
        }

        /// <summary>
        /// 删除工作中心制程
        /// SAM 2017年5月25日09:22:07
        /// </summary>
        /// <param name="Token">授权码</param>
        /// <param name="jArray"></param>
        /// <returns></returns>
        public static object Inf00018WorkCenterProcessdelete(string Token, JArray jArray)
        {
            int success = 0;
            int fail = 0;
            string failIDs = null;
            string msg = null;
            string userid = UtilBussinessService.detoken(Token);
            JObject data = null;
            SYS_WorkCenterProcess model = null;
            for (int i = 0; i < jArray.Count; i++)
            {
                data = (JObject)jArray[i];
                model = SYS_WorkCenterProcessService.get(data.Value<string>("WorkCenterProcessID"));

                model.Status = Framework.SystemID + "0201213000003";
                if (SYS_WorkCenterProcessService.update(userid, model))
                    success++;
                else
                {
                    failIDs = UtilBussinessService.str(failIDs, data.Value<string>("WorkCenterProcessID"));
                    fail++;
                }

            }
            return new { success = success, fail = fail, failIDs = failIDs, msg = msg };
        }

        /// <summary>
        /// 新增工作中心制程
        /// SAM 2017年5月25日09:22:20
        /// </summary>
        /// <param name="Token">授权码</param>
        /// <param name="jArray"></param>
        /// <returns></returns>
        public static object Inf00018WorkCenterProcessinsert(string Token, JArray jArray)
        {
            int success = 0;
            int fail = 0;
            string failIDs = null;
            string msg = null;
            string userid = UtilBussinessService.detoken(Token);
            JObject data = null;
            SYS_WorkCenterProcess model = null;
            for (int i = 0; i < jArray.Count; i++)
            {
                data = (JObject)jArray[i];
                if (!SYS_WorkCenterProcessService.Check(data.Value<string>("ProcessID"), data.Value<string>("WorkCenterID"), null))
                {
                    model = new SYS_WorkCenterProcess();
                    model.WorkCenterProcessID = UniversalService.GetSerialNumber("SYS_WorkCenterProcess");
                    model.ProcessID = data.Value<string>("ProcessID");
                    model.WorkCenterID = data.Value<string>("WorkCenterID");
                    model.Status = Framework.SystemID + "0201213000001";
                    if (SYS_WorkCenterProcessService.insert(userid, model))
                        success++;
                    else
                    {
                        failIDs = UtilBussinessService.str(failIDs, data.Value<string>("WorkCenterProcessID"));
                        fail++;
                    }
                }
                else
                {
                    failIDs = UtilBussinessService.str(failIDs, data.Value<string>("WorkCenterProcessID"));
                    msg = UtilBussinessService.str(msg, "资料重复");
                    fail++;
                }

            }
            return new { success = success, fail = fail, failIDs = failIDs, msg = msg };
        }

        /// <summary>
        /// 更新工作中心制程
        /// SAM 2017年5月25日09:22:36
        /// </summary>
        /// <param name="Token">授权码</param>
        /// <param name="jArray"></param>
        /// <returns></returns>
        public static object Inf00018WorkCenterProcessupdate(string Token, JArray jArray)
        {
            int success = 0;
            int fail = 0;
            string failIDs = null;
            string msg = null;
            string userid = UtilBussinessService.detoken(Token);
            JObject data = null;
            SYS_WorkCenterProcess model = null;
            for (int i = 0; i < jArray.Count; i++)
            {
                data = (JObject)jArray[i];
                model = SYS_WorkCenterProcessService.get(data.Value<string>("WorkCenterProcessID"));
                if (!SYS_WorkCenterProcessService.Check(data.Value<string>("ProcessID"), data.Value<string>("WorkCenterID"), data.Value<string>("WorkCenterProcessID")))
                {
                    model.ProcessID = data.Value<string>("ProcessID");
                    if (SYS_WorkCenterProcessService.update(userid, model))
                        success++;
                    else
                    {
                        failIDs = UtilBussinessService.str(failIDs, data.Value<string>("WorkCenterProcessID"));
                        fail++;
                    }
                }
                else
                {
                    failIDs = UtilBussinessService.str(failIDs, data.Value<string>("WorkCenterProcessID"));
                    msg = UtilBussinessService.str(msg, "资料重复");
                    fail++;
                }

            }
            return new { success = success, fail = fail, failIDs = failIDs, msg = msg };
        }



        /// <summary>
        /// 根据工作中心获取资源列表
        /// SAM 2017年5月25日09:23:30
        /// </summary>
        /// <param name="Token">授权码</param>
        /// <param name="WorkCenterID"></param>
        /// <param name="page">页码</param>
        /// <param name="rows">行数</param>
        /// <returns></returns>
        public static object Inf00018WorkCenterGetResourcesList(string token, string WorkCenterID, int page, int rows)
        {
            int count = 0;
            return UtilBussinessService.getPaginationModel(SYS_WorkCenterResourcesService.Inf00018WorkCenterGetResourcesList(WorkCenterID, page, rows, ref count), count);
        }

        /// <summary>
        /// 删除工作中心资源
        /// SAM 2017年5月25日09:22:07
        /// </summary>
        /// <param name="Token">授权码</param>
        /// <param name="jArray"></param>
        /// <returns></returns>
        public static object Inf00018WorkCenterResourcesdelete(string Token, JArray jArray)
        {
            int success = 0;
            int fail = 0;
            string failIDs = null;
            string msg = null;
            string userid = UtilBussinessService.detoken(Token);
            JObject data = null;
            SYS_WorkCenterResources model = null;
            for (int i = 0; i < jArray.Count; i++)
            {
                data = (JObject)jArray[i];
                model = SYS_WorkCenterResourcesService.get(data.Value<string>("WorkCenterResourcesID"));

                model.Status = Framework.SystemID + "0201213000003";
                if (SYS_WorkCenterResourcesService.update(userid, model))
                    success++;
                else
                {
                    failIDs = UtilBussinessService.str(failIDs, data.Value<string>("WorkCenterResourcesID"));
                    fail++;
                }

            }
            return new { success = success, fail = fail, failIDs = failIDs, msg = msg };
        }

        /// <summary>
        /// 新增工作中心资源
        /// SAM 2017年5月25日09:22:20
        /// </summary>
        /// <param name="Token">授权码</param>
        /// <param name="jArray"></param>
        /// <returns></returns>
        public static object Inf00018WorkCenterResourcesinsert(string Token, JArray jArray)
        {
            int success = 0;
            int fail = 0;
            string failIDs = null;
            string msg = null;
            string userid = UtilBussinessService.detoken(Token);
            JObject data = null;
            SYS_WorkCenterResources model = null;
            for (int i = 0; i < jArray.Count; i++)
            {
                data = (JObject)jArray[i];
                if (!SYS_WorkCenterResourcesService.Check(data.Value<string>("ResourceID"), data.Value<string>("WorkCenterID"), null))
                {
                    model = new SYS_WorkCenterResources();
                    model.WorkCenterResourcesID = UniversalService.GetSerialNumber("SYS_ProcessOperation");
                    model.ResourceID = data.Value<string>("ResourceID");
                    model.WorkCenterID = data.Value<string>("WorkCenterID");
                    model.Status = Framework.SystemID + "0201213000001";
                    if (SYS_WorkCenterResourcesService.insert(userid, model))
                        success++;
                    else
                    {
                        failIDs = UtilBussinessService.str(failIDs, data.Value<string>("WorkCenterResourcesID"));
                        fail++;
                    }
                }
                else
                {
                    failIDs = UtilBussinessService.str(failIDs, data.Value<string>("WorkCenterResourcesID"));
                    msg = UtilBussinessService.str(msg, "资料重复");
                    fail++;
                }

            }
            return new { success = success, fail = fail, failIDs = failIDs, msg = msg };
        }

        /// <summary>
        /// 更新工作中心资源
        /// SAM 2017年5月25日09:22:36
        /// </summary>
        /// <param name="Token">授权码</param>
        /// <param name="jArray"></param>
        /// <returns></returns>
        public static object Inf00018WorkCenterResourcesupdate(string Token, JArray jArray)
        {
            int success = 0;
            int fail = 0;
            string failIDs = null;
            string msg = null;
            string userid = UtilBussinessService.detoken(Token);
            JObject data = null;
            SYS_WorkCenterResources model = null;
            for (int i = 0; i < jArray.Count; i++)
            {
                data = (JObject)jArray[i];
                model = SYS_WorkCenterResourcesService.get(data.Value<string>("WorkCenterResourcesID"));
                if (!SYS_WorkCenterResourcesService.Check(data.Value<string>("ResourceID"), data.Value<string>("WorkCenterID"), data.Value<string>("WorkCenterResourcesID")))
                {
                    model.ResourceID = data.Value<string>("ResourceID");
                    if (SYS_WorkCenterResourcesService.update(userid, model))
                        success++;
                    else
                    {
                        failIDs = UtilBussinessService.str(failIDs, data.Value<string>("WorkCenterResourcesID"));
                        fail++;
                    }
                }
                else
                {
                    failIDs = UtilBussinessService.str(failIDs, data.Value<string>("WorkCenterResourcesID"));
                    msg = UtilBussinessService.str(msg, "资料重复");
                    fail++;
                }

            }
            return new { success = success, fail = fail, failIDs = failIDs, msg = msg };
        }


        /// <summary>
        /// 根据工作中心获取制程列表（不分页）
        /// Joint 2017年7月28日16:55:06
        /// </summary>
        /// <param name="Token"></param>
        /// <param name="WorkCenterID"></param>
        /// <returns></returns>
        public static object Inf00018GetProcessWorkCenterList(string Token, string WorkCenterID)
        {
            return SYS_WorkCenterProcessService.Inf00018GetProcessWorkCenterList(WorkCenterID);
        }

        /// <summary>
        /// 根据工作中心获取不属于他的制程列表(不分页)
        /// Joint 2017年7月28日16:55:13
        /// </summary>
        /// <param name="Token"></param>
        /// <param name="WorkCenterID"></param>
        /// <returns></returns>
        public static object Inf00018GetNoProcessWorkCenterList(string Token, string WorkCenterID)
        {
            return SYS_ParameterService.Inf00018GetNoProcessWorkCenterList(WorkCenterID);
        }

        /// <summary>
        /// 保存制程的工作中心列表
        /// Joint 2017年7月28日16:55:18
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public static object Inf00018MasterWorkCenterProcessSave(JObject request)
        {
            string Token = request.Value<string>("Token");
            string userid = UtilBussinessService.detoken(Token);
            string WorkCenterID = request.Value<string>("WorkCenterID");
            JArray List = request.Value<JArray>("data");
            if (string.IsNullOrWhiteSpace(WorkCenterID))
                return new { status = "410", msg = "WorkCenterID为空!" };
            JObject data = null;
            Hashtable AddModel = null;
            SYS_WorkCenterProcess model = null;
            string New = null;
            List<Hashtable> Add = new List<Hashtable>();
            for (int i = 0; i < List.Count; i++) //将新增的和已存在的分开
            {
                data = (JObject)List[i];
                AddModel = new Hashtable();
                if (string.IsNullOrWhiteSpace(data.Value<string>("WorkCenterProcessID")))
                {
                    AddModel["WorkCenterID"] = data.Value<string>("WorkCenterID");
                    AddModel["ProcessID"] = data.Value<string>("ProcessID");
                    Add.Add(AddModel);
                }
                else
                {
                    if (New == null)
                        New = data.Value<string>("WorkCenterProcessID");
                    else
                        New = New + "','" + data.Value<string>("WorkCenterProcessID");
                }
            }
            SYS_WorkCenterProcessService.Deleted(userid, New, WorkCenterID);
            foreach (Hashtable item in Add)//循环添加新增的
            {
                model = new SYS_WorkCenterProcess();
                model.WorkCenterProcessID = UniversalService.GetSerialNumber("SYS_WorkCenterProcess");
                model.ProcessID = item["ProcessID"].ToString();
                model.WorkCenterID = WorkCenterID;
                model.Status = Framework.SystemID + "0201213000001";
                if (!SYS_WorkCenterProcessService.Check(model.ProcessID, model.WorkCenterID, null))
                    SYS_WorkCenterProcessService.insert(userid, model);
            }
            return new { status = "200", msg = "保存成功！" };
        }

        /// <summary>
        /// 工作中心的制程列表移除-判断
        /// Joint 2017年7月28日18:06:10
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public static object Inf00018MasterWorkCenterProcessDelete(JObject request)
        {
            string process = null;
            string Token = request.Value<string>("Token");
            string userid = UtilBussinessService.detoken(Token);
            foreach (JObject item in request.Value<JArray>("data"))
            {
                string ProcessID =item.Value<string>("ProcessID");
                string ProcessName = item.Value<string>("ProcessDescription");
                string WorkCenterID = item.Value<string>("WorkCenterID");
                if (string.IsNullOrWhiteSpace(WorkCenterID))
                    return new { status = "410", msg = "WorkCenterID为空!" };
                if (string.IsNullOrWhiteSpace(ProcessID))
                    return new { status = "410", msg = "ProcessID为空!" };
                if (SYS_WorkCenterResourcesService.CheckProcess(WorkCenterID, ProcessID) != null)
                    process = UtilBussinessService.str(process, ProcessName);
                else
                    return new { status = "200", msg = "没有用到以上制程流水号！" };
            }
            return new { status = "430", msg = "确定要移除" + process + "制程！" };      
        }

        /// <summary>
        /// 工作中心的制程列表移除
        /// Joint 2017年7月31日10:57:24
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public static object Inf00018MasterWorkCenterProcessDeleted(JObject request)
        {
            string Token = request.Value<string>("Token");
            string userid = UtilBussinessService.detoken(Token);
            string ProcessID = request.Value<string>("ProcessID");
            string WorkCenterID = request.Value<string>("WorkCenterID");
            bool Check = request.Value<bool>("Check");
            SYS_WorkCenterResources model = null;
            List<SYS_WorkCenterResources> WorkCenterModel = SYS_WorkCenterResourcesService.CheckProcess(WorkCenterID, ProcessID);

            if (Check == true)
            {
                foreach (SYS_WorkCenterResources item in WorkCenterModel)
                {
                    model.WorkCenterResourcesID = item.WorkCenterResourcesID;
                    model.Status = Framework.SystemID + "0201213000003";
                    if (SYS_WorkCenterResourcesService.update(userid, model))
                        return new { status = "200", msg = "删除成功！" };
                }
            }
            else
            {
                foreach (SYS_WorkCenterResources item in WorkCenterModel)
                {
                    model.WorkCenterResourcesID = item.WorkCenterResourcesID;
                    model.ProcessID = null;
                    if (SYS_WorkCenterResourcesService.update(userid, model))
                        return new { status = "200", msg = "修改成功！" };
                }
            }
            return new { status = "410", msg = "删除或修改失败！" };
        }

        /// <summary>
        /// 根据工作中心获取资源列表（不分页）
        /// Joint 2017年7月31日11:05:54
        /// </summary>
        /// <param name="Token"></param>
        /// <param name="WorkCenterID"></param>
        /// <returns></returns>
        public static object Inf00018GetWorkCenterResourceList(string Token, string WorkCenterID)
        {
            return SYS_WorkCenterResourcesService.Inf00018GetWorkCenterResourceList(WorkCenterID);
        }

        /// <summary>
        /// 根据工作中心获取不属于它的资源列表（不分页）
        /// Joint 2017年7月31日11:57:31
        /// </summary>
        /// <param name="Token"></param>
        /// <param name="WorkCenterID"></param>
        /// <returns></returns>
        public static object Inf00018GetNoWorkCenterResourceList(string Token, string WorkCenterID)
        {
            return SYS_ResourcesService.Inf00018GetNoWorkCenterResourceList(WorkCenterID);
        }

        /// <summary>
        /// 工作中心资源保存
        /// Joint 2017年7月31日15:13:48
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public static object Inf00018WorkCenterResourceSave(JObject request)
        {
            string Token = request.Value<string>("Token");
            string userid = UtilBussinessService.detoken(Token);
            string WorkCenterID = request.Value<string>("WorkCenterID");
            JArray List = request.Value<JArray>("data");
            if (string.IsNullOrWhiteSpace(WorkCenterID))
                return new { status = "410", msg = "WorkCenterID为空!" };
            JObject data = null;
            Hashtable AddModel = null;
            SYS_WorkCenterResources model = null;
            string New = null;
            List<Hashtable> Add = new List<Hashtable>();
            for (int i = 0; i < List.Count; i++) //将新增的和已存在的分开
            {
                data = (JObject)List[i];
                AddModel = new Hashtable();
                if (string.IsNullOrWhiteSpace(data.Value<string>("WorkCenterResourceID")))
                {
                    AddModel["WorkCenterID"] = data.Value<string>("WorkCenterID");
                    AddModel["ResourceID"] = data.Value<string>("ResourceID");
                    AddModel["ProcessID"] = data.Value<string>("ProcessID");
                    AddModel["OperationID"] = data.Value<string>("OperationID");
                    AddModel["IfMain"] = data.Value<string>("IfMain");
                    Add.Add(AddModel);
                }
                else
                {
                    if (New == null)
                        New = data.Value<string>("WorkCenterResourceID");
                    else
                        New = New + "','" + data.Value<string>("WorkCenterResourceID");
                }
            }
            SYS_WorkCenterResourcesService.Delete(userid, New, WorkCenterID);
            foreach (Hashtable item in Add)//循环添加新增的
            {
                model = new SYS_WorkCenterResources();
                model.WorkCenterResourcesID = UniversalService.GetSerialNumber("SYS_WorkCenterResources");
                model.ResourceID = item["ResourceID"].ToString();
                model.WorkCenterID = WorkCenterID;
                model.IfMain = item["IfMain"].ToString() == "1"?true:false;
                if (item["ProcessID"] != null)
                    model.ProcessID = item["ProcessID"].ToString();
                if (item["OperationID"]!=null) {
                    model.OperationID = item["OperationID"].ToString();
                }
                
                model.Status = Framework.SystemID + "0201213000001";
                if (!SYS_WorkCenterResourcesService.Check(model.ResourceID, model.WorkCenterID, null))
                    SYS_WorkCenterResourcesService.insert(userid, model);
            }
            return new { status = "200", msg = "保存成功！" };
        }

        /// <summary>
        /// 制程下拉框
        /// Joint 2017年7月31日15:42:19
        /// </summary>
        /// <param name="Token"></param>
        /// <param name="WorkCenterID"></param>
        /// <returns></returns>
        public static object Inf00018WorkCenterResourceProcess(string Token, string WorkCenterID)
        {
            return SYS_WorkCenterProcessService.Inf00018GetProcessWorkCenterList(WorkCenterID);
        }

        /// <summary>
        /// 工序下拉框
        /// Joint 2017年7月31日18:07:49
        /// </summary>
        /// <param name="Token"></param>
        /// <param name="ProcessID"></param>
        /// <returns></returns>
        public static object Inf00018WorkCenterResourceOperation(string Token, string ProcessID)
        {
            return SYS_ProcessOperationService.Inf00018GetOperationList(ProcessID);
        }

        #endregion

        #region Inf00019项目主档
        /// <summary>
        /// 项目列表
        /// Tom 2017年5月9日11:55:12
        /// </summary>
        /// <param name="Token">授权码</param>
        /// <param name="Code">代号</param>
        /// <returns></returns>
        public static object Inf00019List(string token, string code, int page = 1, int rows = 10)
        {
            int count = 0;
            return UniversalService.getPaginationModel(
                SYS_ProjectsService.GetPage(code, page, rows, ref count), count);
        }

        /// <summary>
        /// 项目主档新增
        /// Tom 2017年5月9日11:49:08
        /// </summary>
        /// <param name="Token">授权码</param>
        /// <param name="jArray"></param>
        /// <returns></returns>
        public static object Inf00019Insert(string Token, JArray jArray)
        {
            int success = 0;
            int fail = 0;
            string failIDs = null;
            string msg = null;
            string userid = UtilBussinessService.detoken(Token);
            JObject data = null;
            SYS_Projects Model = null;
            for (int i = 0; i < jArray.Count; i++)
            {
                data = (JObject)jArray[i];
                Model = new SYS_Projects();
                Model.Description = data.Value<string>("Description");
                Model.Code = data.Value<string>("Code");
                Model.Attribute = data.Value<string>("Attribute");
                Model.Status = data.Value<string>("Status");
                Model.Comments = data.Value<string>("Comments");
                Model.ProjectID = UniversalService.GetSerialNumber("SYS_Projects");
                if (SYS_ProjectsService.ChecCode(Model.Code))
                {
                    failIDs = UtilBussinessService.str(failIDs, data.Value<string>("ProjectID"));
                    msg = UtilBussinessService.str(msg, data.Value<string>("Code") + "已存在");
                    fail++;
                }
                else if (SYS_ProjectsService.insert(userid, Model))
                    success++;
                else
                {
                    failIDs = UtilBussinessService.str(failIDs, data.Value<string>("ProjectID"));
                    fail++;
                }
            }
            return new { success = success, fail = fail, failIDs = failIDs, msg = msg };
        }
        /// <summary>
        /// 项目主档更新
        /// Tom 2017年5月9日11:49:24
        /// </summary>
        /// <param name="Token">授权码</param>
        /// <param name="jArray"></param>
        /// <returns></returns>
        public static object Inf00019Update(string Token, JArray jArray)
        {
            int success = 0;
            int fail = 0;
            string failIDs = null;
            string msg = null;
            string userid = UtilBussinessService.detoken(Token);
            JObject data = null;
            SYS_Projects Model = null;
            for (int i = 0; i < jArray.Count; i++)
            {
                data = (JObject)jArray[i];
                Model = SYS_ProjectsService.get(data.Value<string>("ProjectID"));
                Model.Description = data.Value<string>("Description");
                Model.Attribute = data.Value<string>("Attribute");
                Model.Status = data.Value<string>("Status");
                Model.Comments = data.Value<string>("Comments");
                Model.Code = data.Value<string>("Code");
                if (SYS_ProjectsService.ChecCode(Model.Code, Model.ProjectID))
                {
                    failIDs = UtilBussinessService.str(failIDs, data.Value<string>("ProjectID"));
                    msg = UtilBussinessService.str(msg, data.Value<string>("Code") + "已存在");
                    fail++;
                }
                else if (SYS_ProjectsService.update(userid, Model))
                    success++;
                else
                {
                    failIDs = UtilBussinessService.str(failIDs, data.Value<string>("ProjectID"));
                    fail++;
                }
            }
            return new { success = success, fail = fail, failIDs = failIDs, msg = msg };
        }
        /// <summary>
        /// 项目主档删除
        /// Tom 2017年5月9日11:49:46
        /// </summary>
        /// <param name="Token">授权码</param>
        /// <param name="jArray"></param>
        /// <returns></returns>
        public static object Inf00019Delete(string Token, JArray jArray)
        {
            int success = 0;
            int fail = 0;
            string failIDs = null;
            string msg = null;
            string userid = UtilBussinessService.detoken(Token);
            JObject data = null;
            SYS_Projects Model = null;
            for (int i = 0; i < jArray.Count; i++)
            {
                data = (JObject)jArray[i];
                Model = SYS_ProjectsService.get(data.Value<string>("ProjectID"));
                Model.Status = Framework.SystemID + "0201213000003";
                if (SYS_ProjectsService.update(userid, Model))
                    success++;
                else
                {
                    failIDs = UtilBussinessService.str(failIDs, Model.ProjectID);
                    fail++;
                }
            }
            return new { success = success, fail = fail, failIDs = failIDs, msg = msg };
        }

        /// <summary>
        /// 项目的单一删除
        /// SAM 2017年7月24日10:05:50
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public static object Inf00019Delete(JObject request)
        {
            string Token = request.Value<string>("Token");
            string userid = UtilBussinessService.detoken(Token);

            SYS_Projects model = SYS_ProjectsService.get(request.Value<string>("ProjectID"));
            if (model == null)
                return new { status = "410", msg = "不存在的项目信息！" };

            if (EMS_EquipmentProjectService.CheckProject(model.ProjectID))
                return new { status = "410", msg = model.Code + "已使用，无法删除！" };

            model.Status = Framework.SystemID + "0201213000003";

            if (SYS_ProjectsService.update(userid, model))
                return new { status = "200", msg = "删除成功！" };
            else
                return new { status = "410", msg = "删除失败！" };
        }

        #endregion

        #region Inf00020系统参数（MES）

        /// <summary>
        /// 系统参数列表
        /// SAM 2017年7月31日00:09:06
        /// </summary>
        /// <param name="token"></param>
        /// <param name="Module"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <returns></returns>
        public static object Inf00020GetList(string token, string Module, int page = 1, int rows = 10)
        {
            int count = 0;
            IList<Hashtable> result = MES_ParameterService.Inf00020GetList(Module, page, rows, ref count);
            foreach (Hashtable item in result)
            {
                //根据语序及参数流水号获取语序设定，不存在不替换，存在则替换
                string ModuleName = SYS_LanguageLibService.GetLan(item["Module"].ToString(), "Name", 20);
                if (!string.IsNullOrWhiteSpace(ModuleName))
                    item["ModuleValue"] = item["ModuleCode"].ToString() +"_"+ ModuleName;

                string Setting = SYS_LanguageLibService.GetLan(item["Setting"].ToString(), "Name", 20);
                if (!string.IsNullOrWhiteSpace(Setting))
                    item["SettingName"] = Setting;

                string Name = SYS_LanguageLibService.GetLan(item["ParameterID"].ToString(), "Name", 110);
                if (!string.IsNullOrWhiteSpace(Name))
                {
                    item["Name"] = Name;
                }
                string Comments = SYS_LanguageLibService.GetLan(item["ParameterID"].ToString(), "Comments", 110);
                if (!string.IsNullOrWhiteSpace(Comments))
                {
                    item["Comments"] = Comments;
                }
                //if (item["Setting"].ToString() == Framework.SystemID + "02012130000A9")
                //{
                //    string Value = SYS_LanguageLibService.GetLan(item["ParameterID"].ToString(), "Value", 110);
                //    if (!string.IsNullOrWhiteSpace(Value))
                //    {
                //        item["Value"] = Value;
                //    }
                //} else
                if (item["Setting"].ToString() == Framework.SystemID + "02012130000A8")
                {
                    IList<Hashtable> Option = UtilBussinessService.GetParameters(token, item["Option"].ToString());
                    item["Option"] = "[";
                    foreach (Hashtable table in Option)
                    {
                        item["Option"] += "{";
                        foreach (DictionaryEntry de in table)
                        {
                            item["Option"] += "\""+ de.Key+"\":\""+ de.Value+"\",";
                        }
                        item["Option"] = item["Option"].ToString().TrimEnd(',');
                        item["Option"] += "},";
                    }
                    item["Option"] = item["Option"].ToString().TrimEnd(',');
                    item["Option"] += "]";
                }
            }
            return UniversalService.getPaginationModel(result, count);
        }

        /// <summary>
        /// 系统参数-修改
        /// SAM2017年7月31日00:09:48
        /// </summary>
        /// <param name="Token"></param>
        /// <param name="jArray"></param>
        /// <returns></returns>
        public static object Inf00020Update(string Token, JArray jArray)
        {
            int success = 0;
            int fail = 0;
            string failIDs = null;
            string msg = null;
            string userid = UtilBussinessService.detoken(Token);
            JObject data = null;
            MES_Parameter Model = null;
            SYS_LanguageLib LibModel = null;
            for (int i = 0; i < jArray.Count; i++)
            {
                data = (JObject)jArray[i];
                Model = MES_ParameterService.get(data.Value<string>("ParameterID"));
                Model.Value = data.Value<string>("Value");
                Model.Comments = data.Value<string>("Comments");
                if (MES_ParameterService.update(userid, Model))
                {
                    LibModel = SYS_LanguageLibService.Get(data.Value<string>("ParameterID"), "Comments", 110);
                    if (LibModel != null)
                    {
                        LibModel.LanguageContentOne = data.Value<string>("Comments");
                        SYS_LanguageLibService.update(userid, LibModel);
                    }
                    success++;
                }
                else
                {
                    failIDs = UtilBussinessService.str(failIDs, data.Value<string>("ProjectID"));
                    fail++;
                }
            }
            return new { success = success, fail = fail, failIDs = failIDs, msg = msg };
        }

        /// <summary>
        /// 系统参数-获取语序列表
        /// SAM 2017年8月24日10:51:32
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static object Inf00020GetLanguageList(string id)
        {
            return SYS_LanguageLibService.Inf00020GetLanguageList(id);
        }

        /// <summary>
        /// 系统参数-语序的新增
        /// SAM 2017年8月24日10:54:59
        /// </summary>
        /// <param name="Token"></param>
        /// <param name="jArray"></param>
        /// <returns></returns>
        public static object Inf00020Languageinsert(string Token, JArray jArray)
        {
            int success = 0;
            int fail = 0;
            string failIDs = null;
            string msg = null;
            string userid = UtilBussinessService.detoken(Token);
            JObject request = null;
            for (int i = 0; i < jArray.Count; i++)
            {
                request = (JObject)jArray[i];

                    if (!SYS_LanguageLibService.checkLanguage(request.Value<string>("RowID"), "Name", request.Value<string>("LanguageCode"), null))
                    {
                        SYS_LanguageLib lan = new SYS_LanguageLib();
                        lan.LanguageLibID = UniversalService.GetSerialNumber("SYS_LanguageLib");
                        lan.TableID ="110";
                        lan.RowID = request.Value<string>("RowID");
                        lan.Field = "Name";
                        lan.OriginalLanguage = request.Value<string>("OriginalLanguage");
                        lan.OriginalContent = request.Value<string>("OriginalContent");
                        lan.LanguageCode = request.Value<string>("LanguageCode");
                        lan.LanguageContentOne = request.Value<string>("Name");
                        lan.IsDefault = request.Value<bool>("IsDefault");
                        if (SYS_LanguageLibService.insert(userid, lan))
                        {
                            lan.LanguageLibID = UniversalService.GetSerialNumber("SYS_LanguageLib");
                            lan.Field = "Value";
                            lan.LanguageCode = request.Value<string>("LanguageCode");
                            lan.LanguageContentOne = request.Value<string>("Value");
                            SYS_LanguageLibService.insert(userid, lan);
                            lan.LanguageLibID = UniversalService.GetSerialNumber("SYS_LanguageLib");
                            lan.Field = "Comments";
                            lan.LanguageCode = request.Value<string>("LanguageCode");
                            lan.LanguageContentOne = request.Value<string>("Comments");
                            SYS_LanguageLibService.insert(userid, lan);
                            success++;
                        }
                        else
                        {
                            failIDs = UtilBussinessService.str(failIDs, request.Value<string>("LanguageLibID"));
                            fail++;
                        }
                    }
                    else
                    {
                        failIDs = UtilBussinessService.str(failIDs, request.Value<string>("LanguageLibID"));
                        msg = UtilBussinessService.str(msg, "已存在语序");
                        fail++;
                    }
               
            }

            return new { success = success, fail = fail, failIDs = failIDs, msg = msg };
        }

        /// <summary>
        /// 系统参数-语序的更新
        /// SAM 2017年8月24日10:55:06
        /// </summary>
        /// <param name="Token"></param>
        /// <param name="jArray"></param>
        /// <returns></returns>
        public static object Inf00020Languageupdate(string Token, JArray jArray)
        {
            int success = 0;
            int fail = 0;
            string failIDs = null;
            string msg = null;
            string userid = UtilBussinessService.detoken(Token);
            JObject request = null;
            try
            {
                for (int i = 0; i < jArray.Count; i++)
                {
                    request = (JObject)jArray[i];
                    if (!SYS_LanguageLibService.checkLanguage(request.Value<string>("RowID"), "Name", request.Value<string>("LanguageCode"), request.Value<string>("LanguageLibID")))
                    {
                        bool IsDefault = request.Value<bool>("IsDefault");
                        try
                        {
                            SYS_LanguageLibService.UpdateLanguage(userid, request.Value<string>("Name"), request.Value<string>("LanguageCode"), IsDefault, request.Value<string>("RowID"), "Name");
                            SYS_LanguageLibService.UpdateLanguage(userid, request.Value<string>("Value"), request.Value<string>("LanguageCode"), IsDefault, request.Value<string>("RowID"), "Value");
                            SYS_LanguageLibService.UpdateLanguage(userid, request.Value<string>("Comments"), request.Value<string>("LanguageCode"), IsDefault, request.Value<string>("RowID"), "Comments");
                            success++;
                        }
                        catch (Exception ex)
                        {
                            DataLogerService.writeerrlog(ex);
                            failIDs = UtilBussinessService.str(failIDs, request.Value<string>("LanguageLibID"));
                            fail++;
                        }
                    }
                    else
                    {
                        failIDs = UtilBussinessService.str(failIDs, request.Value<string>("LanguageLibID"));
                        msg = UtilBussinessService.str(msg, "已存在语序");
                        fail++;
                    }
                }
            }
            catch (Exception ex)
            {
                DataLogerService.writeerrlog(ex);
            }
            return new { success = success, fail = fail, failIDs = failIDs, msg = msg };
        }

        /// <summary>
        /// 获取系统的品质判定方式
        /// Mouse 2017年8月28日10:08:28
        /// </summary>
        /// <param name="Token"></param>
        /// <returns></returns>

        public static object Inf00020GetValue(string Token)
        {
            string userID = UtilBussinessService.detoken(Token);
            MES_Parameter model= MES_ParameterService.get(Framework.SystemID+"1101213000002");//获取对应的品质判定的实体
            return model.Value;//返回品质判定流水号
        }

        #endregion

        #region Inf00021制程替代群组主档
        /// <summary>
        /// 制程替代群组列表
        /// SAM 2017年5月23日09:29:02
        /// </summary>
        /// <param name="Token">授权码</param>
        /// <param name="Code">代号</param>
        /// <param name="Status">状态</param>
        /// <param name="page">页码</param>
        /// <param name="rows">行数</param>
        /// <returns></returns>
        public static object Inf00021GetList(string token, string code, string Status, int page, int rows)
        {
            int count = 0;
            return UtilBussinessService.getPaginationModel(SYS_ParameterService.Inf00021GetList(code, Status, page, rows, ref count), count);
        }

        /// <summary>
        /// 删除替代群组
        /// SAM 2017年5月23日09:31:22
        /// </summary>
        /// <param name="Token">授权码</param>
        /// <param name="jArray"></param>
        /// <returns></returns>
        public static object Inf00021delete(string Token, JArray jArray)
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
                if (!SYS_AlternativeGroupDetailsService.Check(data.Value<string>("ParameterID")))
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
        /// 根据替代群组获取他的替代制程
        /// SAM 2017年5月25日14:22:57
        /// </summary>
        /// <param name="Token">授权码</param>
        /// <param name="GroupID"></param>
        /// <param name="page">页码</param>
        /// <param name="rows">行数</param>
        /// <returns></returns>
        public static object Inf00021GetDetailsList(string token, string GroupID, int page, int rows)
        {
            int count = 0;
            return UtilBussinessService.getPaginationModel(SYS_AlternativeGroupDetailsService.Inf00021GetDetailsList(GroupID, page, rows, ref count), count);
        }


        /// <summary>
        /// 根据替代群组获取他的替代制程
        /// SAM 2017年5月25日14:18:07
        /// </summary>
        /// <param name="Token">授权码</param>
        /// <param name="GroupID"></param>
        /// <returns></returns>
        public static object Inf00021DetailsList(string token, string GroupID)
        {
            return SYS_AlternativeGroupDetailsService.Inf00021DetailsList(GroupID);
        }

        /// <summary>
        /// 根据替代群组获取不属于他的制程列表
        /// SAM 2017年5月25日14:25:33
        /// </summary>
        /// <param name="Token">授权码</param>
        /// <param name="GroupID"></param>
        /// <returns></returns>
        public static object Inf00021ProcessList(string token, string GroupID, string Code)
        {
            return SYS_ParameterService.Inf00021ProcessList(GroupID, Code);
        }

        /// <summary>
        /// 保存替代群组的明细
        /// SAM 2017年5月26日11:48:19
        /// </summary>
        /// <param name="request">JSON数据</param>
        /// <returns></returns>
        public static object Inf00021DetailsSave(JObject request)
        {
            string Token = request.Value<string>("Token");
            string userid = UtilBussinessService.detoken(Token);
            string GroupID = request.Value<string>("GroupID");
            JArray List = request.Value<JArray>("data");

            if (string.IsNullOrWhiteSpace(GroupID))
                return new { status = "410", msg = "GroupID为空!" };

            JObject data = null;
            Hashtable AddModel = null;
            SYS_AlternativeGroupDetails model = null;
            //  IList<Hashtable> Old = SYS_AlternativeGroupDetailsService.Inf00021DetailsList(GroupID);
            string New = null;
            List<Hashtable> Add = new List<Hashtable>();
            for (int i = 0; i < List.Count; i++) //将新增的和已存在的分开
            {
                data = (JObject)List[i];
                AddModel = new Hashtable();
                if (string.IsNullOrWhiteSpace(data.Value<string>("AGDetailID")))
                {
                    AddModel["DetailID"] = data.Value<string>("DetailID");
                    AddModel["GroupID"] = GroupID;
                    Add.Add(AddModel);
                }
                else
                {
                    if (New == null)
                        New = data.Value<string>("AGDetailID");
                    else
                        New = New + "','" + data.Value<string>("AGDetailID");
                }
            }
            SYS_AlternativeGroupDetailsService.Delete(userid, New, GroupID);

            foreach (Hashtable item in Add) //循环添加新增的
            {
                model = new SYS_AlternativeGroupDetails();
                model.AGDetailID = UniversalService.GetSerialNumber("SYS_AlternativeGroupDetails");
                model.GroupID = GroupID;
                model.DetailID = item["DetailID"].ToString();
                model.Status = Framework.SystemID + "0201213000001";
                if (!SYS_AlternativeGroupDetailsService.Check(model.DetailID, model.GroupID))
                    SYS_AlternativeGroupDetailsService.insert(userid, model);
            }

            return new { status = "200", msg = "保存成功！" };
        }

        /// <summary>
        /// 删除制程替代群组
        /// Tom 2017年7月27日17:47:26
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public static object Inf00021Delete(JObject request)
        {
            try
            {
                string Token = request.Value<string>("Token");
                string userID = UniversalService.detoken(Token);

                SYS_Parameters model = SYS_ParameterService.get(request.Value<string>("ParameterID"));
                if (model == null)
                    return new { status = "410", msg = "不存在该制程替代群组信息！" };

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

        #endregion

        #region Inf00023批号自动编号主档
        /// <summary>
        /// 批号自动编号设定列表
        /// SAM 2017年5月17日15:42:51
        /// </summary>
        /// <param name="Token">授权码</param>
        /// <param name="Code">代号</param>
        /// <param name="page">页码</param>
        /// <param name="rows">行数</param>
        /// <returns></returns>
        public static object Inf00023GetList(string token, string code, int page, int rows)
        {
            int count = 0;
            return UtilBussinessService.getPaginationModel(SYS_AutoNumberService.Inf00023GetList(code, page, rows, ref count), count);
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
        public static object Inf00023GetVaildList(string token, string code, int page, int rows)
        {
            int count = 0;
            return UtilBussinessService.getPaginationModel(SYS_AutoNumberService.Inf00023GetVaildList(code, page, rows, ref count), count);
        }


        /// <summary>
        /// 批号自动编号设定新增
        /// SAM 2017年5月17日15:54:22
        /// </summary>
        /// <param name="Token">授权码</param>
        /// <param name="jArray"></param>
        /// <returns></returns>
        public static object Inf00023insert(string Token, JArray jArray)
        {
            int success = 0;
            int fail = 0;
            string failIDs = null;
            string msg = null;
            string userid = UtilBussinessService.detoken(Token);
            JObject data = null;
            SYS_AutoNumber model = null;
            for (int i = 0; i < jArray.Count; i++)
            {
                data = (JObject)jArray[i];

                if (SYS_AutoNumberService.CheckCode(data.Value<string>("Code"), null, null))
                {

                    failIDs = UtilBussinessService.str(failIDs, data.Value<string>("AutoNumberID"));
                    msg = UtilBussinessService.str(msg, data.Value<string>("Code") + "已存在");
                    fail++;
                    continue;
                }
                if (SYS_AutoNumberService.CheckCode(null, data.Value<string>("Description"), null))
                {
                    failIDs = UtilBussinessService.str(failIDs, data.Value<string>("AutoNumberID"));
                    msg = UtilBussinessService.str(msg, data.Value<string>("Description") + "已存在");
                    fail++;
                    continue;
                }
                model = new SYS_AutoNumber();
                model.AutoNumberID = UniversalService.GetSerialNumber("SYS_AutoNumber");
                model.Code = data.Value<string>("Code");
                model.Description = data.Value<string>("Description");
                model.DefaultCharacter = data.Value<string>("DefaultCharacter");
                model.YearLength = data.Value<int>("YearLength");
                model.MonthLength = data.Value<int>("MonthLength");
                model.DateLength = data.Value<int>("DateLength");
                model.NumLength = data.Value<int>("NumLength");
                model.Length = model.DefaultCharacter.Length + model.YearLength + model.MonthLength + model.DateLength + model.NumLength;
                model.Status = data.Value<string>("Status");
                model.Comments = data.Value<string>("Comments");

                if (SYS_AutoNumberService.insert(userid, model))
                    success++;
                else
                {
                    failIDs = UtilBussinessService.str(failIDs, data.Value<string>("AutoNumberID"));
                    fail++;
                }


            }
            return new { success = success, fail = fail, failIDs = failIDs, msg = msg };
        }

        /// <summary>
        /// 删除自动编号设定
        /// SAM 2017年5月17日15:57:35
        /// </summary>
        /// <param name="Token">授权码</param>
        /// <param name="jArray"></param>
        /// <returns></returns>
        public static object Inf00023delete(string Token, JArray jArray)
        {
            int success = 0;
            int fail = 0;
            string failIDs = null;
            string msg = null;
            string userid = UtilBussinessService.detoken(Token);
            JObject data = null;
            SYS_AutoNumber model = null;
            for (int i = 0; i < jArray.Count; i++)
            {
                data = (JObject)jArray[i];
                model = SYS_AutoNumberService.get(data.Value<string>("AutoNumberID"));
                if (model != null)
                {
                    model.Status = Framework.SystemID + "0201213000003";
                    if (SYS_AutoNumberService.update(userid, model))
                        success++;
                    else
                    {
                        failIDs = UtilBussinessService.str(failIDs, data.Value<string>("AutoNumberID"));
                        fail++;
                    }
                }
                else
                {
                    failIDs = UtilBussinessService.str(failIDs, data.Value<string>("AutoNumberID"));
                    msg = UtilBussinessService.str(msg, "流水号错误");
                    fail++;
                }
            }
            return new { success = success, fail = fail, failIDs = failIDs, msg = msg };
        }

        /// <summary>
        /// 更新自动编号设定
        /// SAM  2017年5月17日15:57:38
        /// </summary>
        /// <param name="Token">授权码</param>
        /// <param name="jArray"></param>
        /// <returns></returns>
        public static object Inf00023update(string Token, JArray jArray)
        {
            int success = 0;
            int fail = 0;
            string failIDs = null;
            string msg = null;
            string userid = UtilBussinessService.detoken(Token);
            JObject data = null;
            SYS_AutoNumber model = null;
            for (int i = 0; i < jArray.Count; i++)
            {
                data = (JObject)jArray[i];
                model = SYS_AutoNumberService.get(data.Value<string>("AutoNumberID"));
                if (model != null)
                {
                    model.DefaultCharacter = data.Value<string>("DefaultCharacter");
                    model.YearLength = data.Value<int>("YearLength");
                    model.MonthLength = data.Value<int>("MonthLength");
                    model.DateLength = data.Value<int>("DateLength");
                    model.NumLength = data.Value<int>("NumLength");
                    model.Status = data.Value<string>("Status");
                    model.Length = model.DefaultCharacter.Length + model.YearLength + model.MonthLength + model.DateLength + model.NumLength;
                    if (SYS_AutoNumberService.update(userid, model))
                        success++;
                    else
                    {
                        failIDs = UtilBussinessService.str(failIDs, data.Value<string>("AutoNumberID"));
                        fail++;
                    }
                }
                else
                {
                    failIDs = UtilBussinessService.str(failIDs, data.Value<string>("AutoNumberID"));
                    msg = UtilBussinessService.str(msg, "流水号错误");
                    fail++;
                }

            }
            return new { success = success, fail = fail, failIDs = failIDs, msg = msg };
        }

        /// <summary>
        /// 批號紀錄列表
        /// SAM 2017年5月17日16:01:07
        /// </summary>
        /// <param name="Token">授权码</param>
        /// <param name="AutoNumberID"></param>
        /// <param name="page">页码</param>
        /// <param name="rows">行数</param>
        /// <returns></returns>
        public static object Inf00023RecordGetList(string token, string AutoNumberID, int page, int rows)
        {
            int count = 0;
            return UtilBussinessService.getPaginationModel(SYS_AutoNumberRecordService.Inf00023RecordGetList(AutoNumberID, page, rows, ref count), count);
        }

        /// <summary>
        /// 批号自动编号设定的删除
        /// SAM 2017年7月26日16:54:45
        /// </summary>
        /// <param name="Token"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        public static object Inf00023Delete(string Token, JObject request)
        {

            string userid = UtilBussinessService.detoken(Token);
            SYS_AutoNumber Model = SYS_AutoNumberService.get(request.Value<string>("AutoNumberID"));
            if (Model == null)
                return new { status = "410", msg = "流水号错误！" };

            //if (!SYS_ClassService.inf00014Check(request.Value<string>("CalendarID")))
            //{
            Model.Status = Framework.SystemID + "0201213000003";
            if (SYS_AutoNumberService.update(userid, Model))
                return new { status = "200", msg = "删除成功!" };
            else
                return new { status = "410", msg = "删除失败!" };
            //}
            //else
            //    return new { status = "410", msg = "已使用，不能删除" };
        }

        /// <summary>
        /// 获取批号记录明细
        /// SAM 2017年7月27日02:43:25
        /// </summary>
        /// <param name="token"></param>
        /// <param name="AutoNumberRecordID"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <returns></returns>
        public static object Inf00023RecordDetailList(string token, string AutoNumberRecordID, int page, int rows)
        {
            int count = 0;
            return UtilBussinessService.getPaginationModel(SFC_BatchAttributeService.Inf00023RecordDetailList(AutoNumberRecordID, page, rows, ref count), count);
        }
        #endregion

        #region Inf00024料品属性主档
        /// <summary>
        /// 料品属性列表
        /// SAM 2017年5月10日10:06:51
        /// </summary>
        /// <param name="Token">授权码</param>
        /// <param name="Code">代号</param>
        /// <param name="page">页码</param>
        /// <param name="rows">行数</param>
        /// <returns></returns>
        public static object Inf00024GetList(string token, string code, int page, int rows)
        {
            int count = 0;
            return UtilBussinessService.getPaginationModel(SYS_ParameterService.Inf00024GetList(code, page, rows, ref count), count);
        }

        /// <summary>
        /// 删除料品属性
        /// SAM 2017年5月10日10:33:39
        /// </summary>
        /// <param name="Token">授权码</param>
        /// <param name="jArray"></param>
        /// <returns></returns>
        public static object Inf00024delete(string Token, JArray jArray)
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
                model.IsEnable = 2;
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
        /// 添加料品属性
        /// SAM 2017年5月10日10:11:24
        /// </summary>
        /// <param name="Token">授权码</param>
        /// <param name="jArray"></param>
        /// <returns></returns>
        public static object Inf00024insert(string Token, JArray jArray)
        {
            int success = 0;
            int fail = 0;
            string failIDs = null;
            string msg = null;
            string userid = UtilBussinessService.detoken(Token);
            JObject data = null;
            SYS_Parameters model = null;
            string Type = Framework.SystemID + "019121300000F";
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
                    model.IsDefault = data.Value<bool>("IsDefault"); //是否手写
                    model.Comments = data.Value<string>("Comments");
                    model.IsEnable = data.Value<int>("IsEnable"); //状态       
                    model.Sequence = 0;
                    model.UsingType = 0;
                    model.IsSystem = true;
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
        /// 编辑料品属性
        /// SAM 2017年5月10日10:32:45
        /// </summary>
        /// <param name="Token">授权码</param>
        /// <param name="jArray"></param>
        /// <returns></returns>
        public static object Inf00024update(string Token, JArray jArray)
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
                model.IsEnable = data.Value<int>("IsEnable");//状态
                model.IsDefault = data.Value<bool>("IsDefault"); //是否手写
                model.Comments = data.Value<string>("Comments");
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
        /// 料品属性的单一删除
        /// SAM 2017年7月24日10:14:37
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public static object Inf00024Delete(JObject request)
        {
            string Token = request.Value<string>("Token");
            string userid = UtilBussinessService.detoken(Token);

            SYS_Parameters model = SYS_ParameterService.get(request.Value<string>("ParameterID"));
            if (model == null)
                return new { status = "410", msg = "不存在的料品属性信息！" };

            if (SYS_ItemAttributesService.CheckAttribute(model.ParameterID))
                return new { status = "410", msg = model.Code + "已使用，无法删除！" };

            if (SFC_BatchAttributeDetailsService.CheckAttribute(model.ParameterID))
                return new { status = "410", msg = model.Code + "已使用，无法删除！" };

            model.IsEnable = 2;

            if (SYS_ParameterService.update(userid, model))
                return new { status = "200", msg = "删除成功！" };
            else
                return new { status = "410", msg = "删除失败！" };
        }



        /// <summary>
        /// 获取属性的资料值列表
        /// SAM 2017年5月10日10:55:29
        /// </summary>
        /// <param name="Token">授权码</param>
        /// <param name="ParameterID"></param>
        /// <param name="page">页码</param>
        /// <param name="rows">行数</param>
        /// <returns></returns>
        public static object Inf00024DetailsGetList(string token, string ParameterID, int page, int rows)
        {
            int count = 0;
            return UtilBussinessService.getPaginationModel(SYS_ParameterService.Inf00024DetailsGetList(ParameterID, page, rows, ref count), count);
        }

        /// <summary>
        /// 删除料品属性资料值
        /// SAM 2017年5月10日10:59:03
        /// </summary>
        /// <param name="Token">授权码</param>
        /// <param name="jArray"></param>
        /// <returns></returns>
        public static object Inf00024Detailsdelete(string Token, JArray jArray)
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
                model.IsEnable = 2;
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
        /// 添加料品属性资料值
        /// SAM 2017年5月10日10:58:58
        /// </summary>
        /// <param name="Token">授权码</param>
        /// <param name="jArray"></param>
        /// <returns></returns>
        public static object Inf00024Detailsinsert(string Token, JArray jArray)
        {
            int success = 0;
            int fail = 0;
            string failIDs = null;
            string msg = null;
            string userid = UtilBussinessService.detoken(Token);
            JObject data = null;
            SYS_Parameters model = null;
            string Type = Framework.SystemID + "0191213000010";
            for (int i = 0; i < jArray.Count; i++)
            {
                data = (JObject)jArray[i];
                model = new SYS_Parameters();
                model.ParameterID = UniversalService.GetSerialNumber("SYS_Parameters");
                model.ParameterTypeID = Type;
                model.ParentParameterID = data.Value<string>("ParentParameterID");
                model.Name = data.Value<string>("Name");//资料值
                model.Comments = data.Value<string>("Comments");//备注
                model.IsEnable = 1; //状态       
                model.Sequence = 0;
                model.UsingType = 0;
                model.IsSystem = true;
                if (SYS_ParameterService.insert(userid, model))
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
        /// 编辑料品属性资料值
        /// SAM 2017年5月10日11:00:44
        /// </summary>
        /// <param name="Token">授权码</param>
        /// <param name="jArray"></param>
        /// <returns></returns>
        public static object Inf00024Detailsupdate(string Token, JArray jArray)
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
                model.Comments = data.Value<string>("Comments"); //备注
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

        #endregion

        #region USE00001用途别主档
        /// <summary>
        /// 用途别列表
        /// SAM 2017年5月12日10:13:36
        /// </summary>
        /// <param name="Token">授权码</param>
        /// <param name="Code">代号</param>
        /// <param name="Status">状态</param>
        /// <param name="page">页码</param>
        /// <param name="rows">行数</param>
        /// <returns></returns>
        public static object USE00001List(string token, string code, string Status, int page, int rows)
        {
            int count = 0;
            return UtilBussinessService.getPaginationModel(SYS_ParameterService.USE00001List(code, Status, page, rows, ref count), count);
        }

        /// <summary>
        /// 删除用途
        /// SAM 2017年5月12日11:27:31
        /// </summary>
        /// <param name="Token">授权码</param>
        /// <param name="jArray"></param>
        /// <returns></returns>
        public static object USE00001delete(string Token, JArray jArray)
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
        /// 用途别的单一删除
        /// SAM 2017年7月28日12:04:02
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public static object USE00001Delete(JObject request)
        {
            string Token = request.Value<string>("Token");
            string userid = UtilBussinessService.detoken(Token);

            SYS_Parameters model = SYS_ParameterService.get(request.Value<string>("ParameterID"));
            if (model == null)
                return new { status = "410", msg = "不存在的用途信息！" };

            //if (EMS_EquipmentProjectService.CheckProject(model.ProjectID))
            //    return new { status = "410", msg = model.Code + "已使用，无法删除！" };

            model.IsEnable = 2;

            if (SYS_ParameterService.update(userid, model))
                return new { status = "200", msg = "删除成功！" };
            else
                return new { status = "410", msg = "删除失败！" };
        }
        #endregion

        #region SCP00004系统操作
        /// <summary>
        /// 系统操作列表
        /// Tom 2017年5月12日09:38:06
        /// </summary>
        /// <param name="Token">授权码</param>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <returns></returns>
        public static object SCP00004List(string token, DateTime? startTime, DateTime? endTime, int page, int rows)
        {
            int count = 0;
            return UniversalService.getPaginationModel(
                        SYS_MFCLogService.GetPage(
                            startTime, endTime, page, rows, ref count),
                        count);
        }
        #endregion

        #region Trn00001資料拋轉參數設定列表
        /// <summary>
        /// 資料拋轉參數設定列表
        /// SAM 2017年7月5日10:03:07
        /// </summary>
        /// <param name="token"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <returns></returns>
        public static object Trn00001GetList(string token, int page, int rows)
        {
            int count = 0;
            Hashtable ControlParameter = null;
            IList<Hashtable> result = SYS_ParameterService.Trn00001GetList(page, rows, ref count);
            foreach (Hashtable item in result)
            {
                ControlParameter = SYS_ControlParameterService.Trn00001Get(item["ParameterID"].ToString());
                if (ControlParameter != null)
                {
                    item["System"] = ControlParameter["System"];
                    item["Dbms"] = ControlParameter["Dbms"];
                    item["DBName"] = ControlParameter["DBName"];
                    item["DBUser"] = ControlParameter["DBUser"];
                    //item["DBPassword"] = HttpUtility.UrlDecode(ControlParameter["DBPassword"].ToString(), System.Text.Encoding.GetEncoding("GB2312"));
                    item["DBPassword"] = null;
                    item["RetriveTime"] = ControlParameter["RetriveTime"];
                    item["Remark"] = ControlParameter["Remark"];
                    item["StartExchange"] = ControlParameter["StartExchange"];
                }
            }
            return UtilBussinessService.getPaginationModel(result, count);
        }

        /// <summary>
        /// 資料拋轉參數設定-新增
        /// SAM 2017年7月5日10:25:42
        /// </summary>
        /// <param name="Token"></param>
        /// <param name="jArray"></param>
        /// <returns></returns>
        public static object Trn00001insert(string Token, JArray jArray)
        {
            int success = 0;
            int fail = 0;
            string failIDs = null;
            string msg = null;
            string userid = UtilBussinessService.detoken(Token);
            JObject data = null;
            SYS_Parameters par = null;
            string sql = null;
            for (int i = 0; i < jArray.Count; i++)
            {
                data = (JObject)jArray[i];
                par = new SYS_Parameters();
                par.ParameterID = UniversalService.GetSerialNumber("SYS_Parameters");
                par.ParameterTypeID = Framework.SystemID + "0191213000020";
                par.IsEnable = 1;
                par.IsDefault = false;
                par.Sequence = 0;//暂时后台定死0
                par.UsingType = 0;//这个字段暂未知有何用处
                if (SYS_ParameterService.insert(userid, par))
                {
                    SYS_ControlParameters conpar = new SYS_ControlParameters();
                    conpar.Name = par.ParameterID;
                    conpar.PageNumber = "TRN00001";
                    conpar.IsEnable = true;
                    conpar.IsDefault = true;
                    conpar.Sequence = 1;
                    conpar.ParameterTypeID = par.ParameterTypeID;
                    sql = null;
                    sql += SYS_ControlParameterService.insertSQL(userid, conpar, UniversalService.GetSerialNumber("SYS_ControlParameters"), "System", data.Value<string>("System"), 3);
                    sql += SYS_ControlParameterService.insertSQL(userid, conpar, UniversalService.GetSerialNumber("SYS_ControlParameters"), "Dbms", data.Value<string>("Dbms"), 3);
                    sql += SYS_ControlParameterService.insertSQL(userid, conpar, UniversalService.GetSerialNumber("SYS_ControlParameters"), "DBName", data.Value<string>("DBName"), 3);
                    sql += SYS_ControlParameterService.insertSQL(userid, conpar, UniversalService.GetSerialNumber("SYS_ControlParameters"), "DBUser", data.Value<string>("DBUser"), 3);
                    sql += SYS_ControlParameterService.insertSQL(userid, conpar, UniversalService.GetSerialNumber("SYS_ControlParameters"), "DBPassword", HttpUtility.UrlEncode(data.Value<string>("DBPassword"), System.Text.Encoding.GetEncoding("GB2312")), 3);
                    sql += SYS_ControlParameterService.insertSQL(userid, conpar, UniversalService.GetSerialNumber("SYS_ControlParameters"), "RetriveTime", data.Value<string>("RetriveTime"), 4);
                    sql += SYS_ControlParameterService.insertSQL(userid, conpar, UniversalService.GetSerialNumber("SYS_ControlParameters"), "Remark", data.Value<string>("Remark"), 3);
                    sql += SYS_ControlParameterService.insertSQL(userid, conpar, UniversalService.GetSerialNumber("SYS_ControlParameters"), "StartExchange", data.Value<string>("StartExchange"), 0);

                    SYS_ControlParameterService.RunSQL(sql);//如果执行失败，怎么办？
                    success++;
                }
                else
                {
                    failIDs = UtilBussinessService.str(failIDs, data.Value<string>("ParameterID"));
                    fail++;
                }

            }
            return new { success = success, fail = fail, failIDs = failIDs, msg = msg };
        }

        /// <summary>
        /// 資料拋轉參數設定-更新
        /// SAM 2017年7月5日10:23:52
        /// </summary>
        /// <param name="Token"></param>
        /// <param name="jArray"></param>
        /// <returns></returns>
        public static object Trn00001update(string Token, JArray jArray)
        {
            int success = 0;
            int fail = 0;
            string failIDs = null;
            string msg = null;
            string userid = UtilBussinessService.detoken(Token);
            JObject data = null;
            String sql = null;
            for (int i = 0; i < jArray.Count; i++)
            {
                data = (JObject)jArray[i];
                sql = null;

                sql += SYS_ControlParameterService.updateSQL(userid, data.Value<string>("ParameterID"), "System", data.Value<string>("System"));
                sql += SYS_ControlParameterService.updateSQL(userid, data.Value<string>("ParameterID"), "DBUser", data.Value<string>("DBUser"));
                sql += SYS_ControlParameterService.updateSQL(userid, data.Value<string>("ParameterID"), "Dbms", data.Value<string>("Dbms"));
                sql += SYS_ControlParameterService.updateSQL(userid, data.Value<string>("ParameterID"), "DBName", data.Value<string>("DBName"));
                if (!String.IsNullOrWhiteSpace(data.Value<string>("DBPassword")))
                    sql += SYS_ControlParameterService.updateSQL(userid, data.Value<string>("ParameterID"), "DBPassword", HttpUtility.UrlEncode(data.Value<string>("DBPassword"), System.Text.Encoding.GetEncoding("UTF-8")));
                sql += SYS_ControlParameterService.updateSQL(userid, data.Value<string>("ParameterID"), "RetriveTime", data.Value<string>("RetriveTime"));
                sql += SYS_ControlParameterService.updateSQL(userid, data.Value<string>("ParameterID"), "Remark", data.Value<string>("Remark"));
                sql += SYS_ControlParameterService.updateSQL(userid, data.Value<string>("ParameterID"), "StartExchange", data.Value<string>("StartExchange"));

                if (SYS_ControlParameterService.RunSQL(sql))
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

        #region Trn00002 MES资料转入处理
        /// <summary>
        /// MES资料转入处理-列表查询
        /// SAM 2017年8月31日15:39:08
        /// </summary>
        /// <param name="token"></param>
        /// <param name="StartDate"></param>
        /// <param name="EndDate"></param>
        /// <param name="StartCode"></param>
        /// <param name="EndCode"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <returns></returns>
        public static object Trn00002GetList(string token, string StartDate, string EndDate, string StartCode, string EndCode, string Type,int page, int rows)
        {
            int count = 0;
            return UtilBussinessService.getPaginationModel(TRAN_TRANSFER_LOGService.Trn00002GetList(StartDate, EndDate, StartCode, EndCode, Type, page, rows, ref count), count);
        }
        #endregion

        #region Lan00000语序主档
        /// <summary>
        /// 获取语序主档列表
        /// SAM 2017年5月3日10:38:42
        /// </summary>
        /// <param name="Token">授权码</param>
        /// <param name="Code">代号</param>
        /// <param name="Name"></param>
        /// <param name="page">页码</param>
        /// <param name="rows">行数</param>
        /// <returns></returns>
        public static object Lan00000GetList(string token, string Code, int page, int rows)
        {
            int count = 0;
            return UtilBussinessService.getPaginationModel(SYS_ParameterService.Lan00000GetList(Code, page, rows, ref count), count);
        }

        /// <summary>
        /// 语序删除
        /// SAM 2017年5月3日10:39:21
        /// </summary>
        /// <param name="Token">授权码</param>
        /// <param name="jArray"></param>
        /// <returns></returns>
        public static object Lan00000delete(string Token, JArray jArray)
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
                if (!SYS_LanguageLibService.CheckLanguage(data.Value<string>("ParameterID")))
                {

                    Model.IsEnable = 2;
                    if (SYS_ParameterService.update(userid, Model))
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
                    msg = UtilBussinessService.str(msg, Model.Code + "已使用，不能删除");
                    fail++;
                }

            }
            return new { success = success, fail = fail, failIDs = failIDs, msg = msg };
        }

        /// <summary>
        /// 语序添加
        /// SAM 2017年5月3日10:39:21
        /// </summary>
        /// <param name="Token">授权码</param>
        /// <param name="jArray"></param>
        /// <returns></returns>
        public static object Lan00000insert(string Token, JArray jArray)
        {
            int success = 0;
            int fail = 0;
            string failIDs = null;
            string msg = null;
            string userid = UtilBussinessService.detoken(Token);
            JObject data = null;
            SYS_Parameters model = null;
            string typeID = Framework.SystemID + "019121300000A";
            for (int i = 0; i < jArray.Count; i++)
            {
                data = (JObject)jArray[i];
                if (!SYS_ParameterService.CheckParameter(data.Value<string>("Code"), null, null, typeID, null))
                {
                    model = new SYS_Parameters();
                    model.ParameterID = UniversalService.GetSerialNumber("SYS_Parameters");
                    model.ParameterTypeID = typeID;
                    model.Code = data.Value<string>("Code");  //代号
                    model.Name = data.Value<string>("Name");//说明
                    model.Comments = data.Value<string>("Comments");//备注
                    model.IsEnable = data.Value<int>("IsEnable"); //状态
                    model.IsDefault = false;
                    model.Sequence = 0;//暂时后台定死0
                    model.UsingType = 0;//这个字段暂未知有何用处
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
        /// 语序更新
        /// SAM 2017年5月3日10:39:21
        /// </summary>
        /// <param name="Token">授权码</param>
        /// <param name="jArray"></param>
        /// <returns></returns>
        public static object Lan00000update(string Token, JArray jArray)
        {
            int success = 0;
            int fail = 0;
            string failIDs = null;
            string msg = null;
            string userid = UtilBussinessService.detoken(Token);
            JObject data = null;
            SYS_Parameters model = null;
            string typeID = Framework.SystemID + "019121300000A";
            for (int i = 0; i < jArray.Count; i++)
            {
                data = (JObject)jArray[i];
                if (!SYS_ParameterService.CheckParameter(data.Value<string>("Code"), null, null, typeID, data.Value<string>("ParameterID")))
                {
                    model = SYS_ParameterService.get(data.Value<string>("ParameterID"));
                    model.Name = data.Value<string>("Name");//说明
                    model.Comments = data.Value<string>("Comments");//备注
                    model.IsEnable = data.Value<int>("IsEnable"); //状态
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
                    msg = UtilBussinessService.str(msg, model.Code + "已存在");
                    fail++;
                }
            }
            return new { success = success, fail = fail, failIDs = failIDs, msg = msg };
        }


        #endregion

        #region 通用函数
        /// <summary>
        ///  获取语序（Name和Comments）
        ///  SAM 2017年5月4日11:32:48
        /// </summary>
        /// <param name="id"></param>
        /// <param name="TableID"></param>
        /// <returns></returns>
        public static object NCGetLanguageList(string id, string TableID)
        {
            return SYS_LanguageLibService.NCGetLanguageList(id, TableID);
        }

        /// <summary>
        /// 添加语序(Name和Comments)
        /// SAM  2017年5月4日11:26:34
        /// </summary>
        /// <param name="Token">授权码</param>
        /// <param name="jArray"></param>
        /// <returns></returns>
        public static object NCLanguageinsert(string Token, JArray jArray)
        {
            int success = 0;
            int fail = 0;
            string failIDs = null;
            string msg = null;
            string userid = UtilBussinessService.detoken(Token);
            JObject request = null;
            for (int i = 0; i < jArray.Count; i++)
            {
                request = (JObject)jArray[i];
                if (!string.IsNullOrWhiteSpace(request.Value<string>("TableID")))
                {
                    if (!SYS_LanguageLibService.checkLanguage(request.Value<string>("RowID"), "Name", request.Value<string>("LanguageCode"), null))
                    {
                        SYS_LanguageLib lan = new SYS_LanguageLib();
                        lan.LanguageLibID = UniversalService.GetSerialNumber("SYS_LanguageLib");
                        lan.TableID = request.Value<string>("TableID");
                        lan.RowID = request.Value<string>("RowID");
                        lan.Field = "Name";
                        lan.OriginalLanguage = request.Value<string>("OriginalLanguage");
                        lan.OriginalContent = request.Value<string>("OriginalContent");
                        lan.LanguageCode = request.Value<string>("LanguageCode");
                        lan.LanguageContentOne = request.Value<string>("Name");
                        lan.IsDefault = request.Value<bool>("IsDefault");
                        if (SYS_LanguageLibService.insert(userid, lan))
                        {
                            lan.LanguageLibID = UniversalService.GetSerialNumber("SYS_LanguageLib");
                            lan.Field = "Comments";
                            lan.LanguageCode = request.Value<string>("LanguageCode");
                            lan.LanguageContentOne = request.Value<string>("Comments");
                            SYS_LanguageLibService.insert(userid, lan);
                            success++;
                        }
                        else
                        {
                            failIDs = UtilBussinessService.str(failIDs, request.Value<string>("LanguageLibID"));
                            fail++;
                        }
                    }
                    else
                    {
                        failIDs = UtilBussinessService.str(failIDs, request.Value<string>("LanguageLibID"));
                        msg = UtilBussinessService.str(msg, "已存在语序");
                        fail++;
                    }
                }
                else
                {
                    failIDs = UtilBussinessService.str(failIDs, request.Value<string>("LanguageLibID"));
                    msg = UtilBussinessService.str(msg, "TableID为空");
                    fail++;
                }
            }

            return new { success = success, fail = fail, failIDs = failIDs, msg = msg };
        }

        /// <summary>
        /// 更新双语序(Name和Comments)
        /// SAM 2017年1月17日09:42:23
        /// </summary>
        /// <param name="Token">授权码</param>
        /// <param name="jArray"></param>
        /// <returns></returns>
        public static object NCLanguageupdate(string Token, JArray jArray)
        {
            int success = 0;
            int fail = 0;
            string failIDs = null;
            string msg = null;
            string userid = UtilBussinessService.detoken(Token);
            JObject request = null;
            try
            {
                for (int i = 0; i < jArray.Count; i++)
                {
                    request = (JObject)jArray[i];
                    if (!SYS_LanguageLibService.checkLanguage(request.Value<string>("RowID"), "Name", request.Value<string>("LanguageCode"), request.Value<string>("LanguageLibID")))
                    {
                        bool IsDefault = request.Value<bool>("IsDefault");
                        try
                        {
                            SYS_LanguageLibService.UpdateLanguage(userid, request.Value<string>("Name"), request.Value<string>("LanguageCode"), IsDefault, request.Value<string>("RowID"), "Name");
                            SYS_LanguageLibService.UpdateLanguage(userid, request.Value<string>("Comments"), request.Value<string>("LanguageCode"), IsDefault, request.Value<string>("RowID"), "Comments");
                            success++;
                        }
                        catch (Exception ex)
                        {
                            DataLogerService.writeerrlog(ex);
                            failIDs = UtilBussinessService.str(failIDs, request.Value<string>("LanguageLibID"));
                            fail++;
                        }
                    }
                    else
                    {
                        failIDs = UtilBussinessService.str(failIDs, request.Value<string>("LanguageLibID"));
                        msg = UtilBussinessService.str(msg, "已存在语序");
                        fail++;
                    }
                }
            }
            catch (Exception ex)
            {
                DataLogerService.writeerrlog(ex);
            }
            return new { success = success, fail = fail, failIDs = failIDs, msg = msg };
        }

        /// <summary>
        /// 删除语序
        /// SAM 2017年5月4日11:24:30
        /// </summary>
        /// <param name="jArray"></param>
        /// <returns></returns>
        public static object Languagedelete(JArray jArray)
        {
            int success = 0;
            int fail = 0;
            string failIDs = null;
            JObject request = null;
            for (int i = 0; i < jArray.Count; i++)
            {
                request = (JObject)jArray[i];

                string LanguageCode = request.Value<string>("LanguageCode");
                string rowid = request.Value<string>("RowID");
                if (SYS_LanguageLibService.DeleteLanguage(LanguageCode, rowid))
                    success++;
                else
                {
                    failIDs = UtilBussinessService.str(failIDs, request.Value<string>("LanguageLibID"));
                    fail++;
                }
            }
            return new { success = success, fail = fail, failIDs = failIDs };
        }

        /// <summary>
        ///  获取语序（Code,Name和Comments三语序）
        ///  SAM 2017年5月4日11:32:48
        /// </summary>
        /// <param name="id"></param>
        /// <param name="TableID"></param>
        /// <returns></returns>
        public static object CNCGetLanguageList(string id, string TableID)
        {
            return SYS_LanguageLibService.CNCGetLanguageList(id, TableID);
        }

        /// <summary>
        /// 添加语序(Code,Name和Comments)
        /// SAM 2017年5月8日15:50:50
        /// </summary>
        /// <param name="Token">授权码</param>
        /// <param name="jArray"></param>
        /// <returns></returns>
        public static object CNCLanguageinsert(string Token, JArray jArray)
        {
            int success = 0;
            int fail = 0;
            string failIDs = null;
            string msg = null;
            string userid = UtilBussinessService.detoken(Token);
            JObject request = null;
            for (int i = 0; i < jArray.Count; i++)
            {
                request = (JObject)jArray[i];
                if (!string.IsNullOrWhiteSpace(request.Value<string>("TableID")))
                {
                    if (!SYS_LanguageLibService.checkLanguage(request.Value<string>("RowID"), "Code", request.Value<string>("LanguageCode"), null))
                    {
                        SYS_LanguageLib lan = new SYS_LanguageLib();
                        lan.LanguageLibID = UniversalService.GetSerialNumber("SYS_LanguageLib");
                        lan.TableID = request.Value<string>("TableID");
                        lan.RowID = request.Value<string>("RowID");
                        lan.Field = "Code";
                        lan.OriginalLanguage = request.Value<string>("OriginalLanguage");
                        lan.OriginalContent = request.Value<string>("OriginalContent");
                        lan.LanguageCode = request.Value<string>("LanguageCode");
                        lan.LanguageContentOne = request.Value<string>("Code");
                        lan.IsDefault = request.Value<bool>("IsDefault");
                        if (SYS_LanguageLibService.insert(userid, lan))
                        {
                            lan.LanguageLibID = UniversalService.GetSerialNumber("SYS_LanguageLib");
                            lan.Field = "Name";
                            lan.LanguageCode = request.Value<string>("LanguageCode");
                            lan.LanguageContentOne = request.Value<string>("Name");
                            SYS_LanguageLibService.insert(userid, lan);
                            lan.LanguageLibID = UniversalService.GetSerialNumber("SYS_LanguageLib");
                            lan.Field = "Comments";
                            lan.LanguageCode = request.Value<string>("LanguageCode");
                            lan.LanguageContentOne = request.Value<string>("Comments");
                            SYS_LanguageLibService.insert(userid, lan);
                            success++;
                        }
                        else
                        {
                            failIDs = UtilBussinessService.str(failIDs, request.Value<string>("LanguageLibID"));
                            fail++;
                        }
                    }
                    else
                    {
                        failIDs = UtilBussinessService.str(failIDs, request.Value<string>("LanguageLibID"));
                        msg = UtilBussinessService.str(msg, "已存在语序");
                        fail++;
                    }
                }
                else
                {
                    failIDs = UtilBussinessService.str(failIDs, request.Value<string>("LanguageLibID"));
                    msg = UtilBussinessService.str(msg, "TableID为空");
                    fail++;
                }
            }

            return new { success = success, fail = fail, failIDs = failIDs, msg = msg };
        }

        /// <summary>
        /// 更新三语序(Code,Name和Comments)
        /// SAM 2017年5月8日15:51:51
        /// </summary>
        /// <param name="Token">授权码</param>
        /// <param name="jArray"></param>
        /// <returns></returns>
        public static object CNCLanguageupdate(string Token, JArray jArray)
        {
            int success = 0;
            int fail = 0;
            string failIDs = null;
            string msg = null;
            string userid = UtilBussinessService.detoken(Token);
            JObject request = null;
            try
            {
                for (int i = 0; i < jArray.Count; i++)
                {
                    request = (JObject)jArray[i];
                    if (!SYS_LanguageLibService.checkLanguage(request.Value<string>("RowID"), "Code", request.Value<string>("LanguageCode"), request.Value<string>("LanguageLibID")))
                    {
                        bool IsDefault = request.Value<bool>("IsDefault");
                        try
                        {
                            SYS_LanguageLibService.UpdateLanguage(userid, request.Value<string>("Code"), request.Value<string>("LanguageCode"), IsDefault, request.Value<string>("RowID"), "Code");
                            SYS_LanguageLibService.UpdateLanguage(userid, request.Value<string>("Name"), request.Value<string>("LanguageCode"), IsDefault, request.Value<string>("RowID"), "Name");
                            SYS_LanguageLibService.UpdateLanguage(userid, request.Value<string>("Comments"), request.Value<string>("LanguageCode"), IsDefault, request.Value<string>("RowID"), "Comments");
                            success++;
                        }
                        catch (Exception ex)
                        {
                            DataLogerService.writeerrlog(ex);
                            failIDs = UtilBussinessService.str(failIDs, request.Value<string>("LanguageLibID"));
                            fail++;
                        }
                    }
                    else
                    {
                        failIDs = UtilBussinessService.str(failIDs, request.Value<string>("LanguageLibID"));
                        msg = UtilBussinessService.str(msg, "已存在语序");
                        fail++;
                    }
                }
            }
            catch (Exception ex)
            {
                DataLogerService.writeerrlog(ex);
            }
            return new { success = success, fail = fail, failIDs = failIDs, msg = msg };
        }

        /// <summary>
        /// 通用新增参数函数（Code,Name,Comments,IsEnable）
        /// </summary>
        /// <param name="Token">授权码</param>
        /// <param name="jArray"></param>
        /// <returns></returns>
        public static object Parameterinsert(string Token, JArray jArray, string Type)
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
                if (!SYS_ParameterService.CheckParameter(data.Value<string>("Code"), null, null, Type, null))
                {
                    model = new SYS_Parameters();
                    model.ParameterID = UniversalService.GetSerialNumber("SYS_Parameters");
                    model.ParameterTypeID = Type;
                    model.Code = data.Value<string>("Code");  //代号
                    model.Name = data.Value<string>("Name");//说明
                    model.Comments = data.Value<string>("Comments");
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
        /// 通用删除参数函数
        /// SAM 2017年5月11日10:02:16
        /// </summary>
        /// <param name="Token">授权码</param>
        /// <param name="jArray"></param>
        /// <returns></returns>
        public static object Parameterdelete(string Token, JArray jArray)
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
                model.IsEnable = 2;
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
        /// 通用更新参数函数(只更新Status)
        /// </summary>
        /// <param name="Token">授权码</param>
        /// <param name="jArray"></param>
        /// <returns></returns>
        public static object Parameterupdate(string Token, JArray jArray)
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
                model.IsEnable = data.Value<int>("IsEnable"); //状态
                model.Comments = data.Value<string>("Comments");//备注
                model.Name = data.Value<string>("Name");//说明
                model.Code = data.Value<string>("Code");//原因群码群码
                model.DescriptionOne = data.Value<string>("DescriptionOne");//原因码群码
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

        #endregion
    }
}