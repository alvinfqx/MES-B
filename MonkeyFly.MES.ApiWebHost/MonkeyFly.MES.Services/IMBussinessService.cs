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

namespace MonkeyFly.MES.Services
{
    /// <summary>
    /// 智能制造的业务逻辑层
    /// SAM 2017年6月12日17:25:38
    /// </summary>
    public class IMBussinessService
    {
        #region SFC00001制品制程资料维护
        /// <summary>
        /// 制品主列表
        /// SAM 2017年6月19日14:54:21
        /// </summary>
        /// <param name="token"></param>
        /// <param name="startCode"></param>
        /// <param name="endCode"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <returns></returns>
        public static object Sfc00001GetList(string token, string Type, string startCode, string endCode, int page, int rows)
        {
            int count = 0;
            return UtilBussinessService.getPaginationModel(SYS_ItemsService.Sfc00001GetList(Type, startCode, endCode, page, rows, ref count), count);
        }

        #region BOM和资源
        /// <summary>
        /// BOM和资源中左边树的显示
        /// SAM 2017年7月15日10:40:17
        /// </summary>
        /// <param name="Token"></param>
        /// <param name="FabricatedMotherID"></param>
        /// <returns></returns>
        public static object Sfc00001GetTreeList(string Token, string ItemID)
        {
            List<Hashtable> result = new List<Hashtable>();
            IList<Hashtable> Process = SFC_ItemProcessService.GetBomList(ItemID);
            foreach (Hashtable item in Process)
            {
                result.Add(item);
                IList<Hashtable> Operation = SFC_ItemOperationService.GetBomList(item["ItemProcessID"].ToString());
                result.AddRange(Operation);
            }
            return result;
        }

        /// <summary>
        /// 根据制品制程流水号或者制品制程工序流水号获取BOM（分页）
        /// MOUSE 2017年8月1日11:06:23
        /// </summary>
        /// <param name="token"></param>
        /// <param name="ItemProcessID"></param>制品制程流水号
        /// <param name="ItemOperationID"></param>制品工序流水号
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <returns></returns>
        public static object Sfc00001BomList(string token, string ItemProcessID, string ItemOperationID, int page, int rows)
        {
            int count = 0;
            IList<Hashtable> result = null;
            if (string.IsNullOrWhiteSpace(ItemOperationID))
                result = SFC_ItemMaterialService.Sfc00001BomList(ItemProcessID, null, page, rows, ref count);
            else
                result = SFC_ItemMaterialService.Sfc00001BomList(null, ItemOperationID, page, rows, ref count);
            return UtilBussinessService.getPaginationModel(result, count);
        }

        /// <summary>
        /// 根据制品制程流水号或者制品制程工序流水号获取BOM(不分页)
        /// SAM 2017年7月27日02:27:25
        /// </summary>
        /// <param name="Token"></param>
        /// <param name="ItemProcessID"></param>
        /// <param name="ItemOperationID"></param>
        /// <returns></returns>
        public static object Sfc00001GetBomList(string Token, string ItemProcessID, string ItemOperationID)
        {
            IList<Hashtable> result = null;
            if (string.IsNullOrWhiteSpace(ItemOperationID))
                result = SFC_ItemMaterialService.GetItemList(ItemProcessID, null);
            else
                result = SFC_ItemMaterialService.GetItemList(null, ItemOperationID);
            return result;
        }

        /// <summary>
        /// 根据制品制程流水号或者制品制程工序流水号获取不存在的Bom(不分页)
        /// SAM 2017年7月27日02:26:56
        /// MOUSE 2017年8月2日16:24:07 修改为分页
        /// </summary>
        /// <param name="Token"></param>
        /// <param name="ItemProcessID"></param>
        /// <param name="ItemOperationID"></param>
        /// <returns></returns>
        public static object Sfc00001GetNoBomList(string Token, string ItemProcessID, string ItemOperationID, int page, int rows)
        {
            int count = 0;
            IList<Hashtable> result = null;
            if (string.IsNullOrWhiteSpace(ItemOperationID))
                result = SYS_ItemsService.Sfc00001GetNoBomList(ItemProcessID, null, page, rows, ref count);
            else
                result = SYS_ItemsService.Sfc00001GetNoBomList(null, ItemOperationID, page, rows, ref count);
            return UtilBussinessService.getPaginationModel(result, count);
        }

        /// <summary>
        /// 制品BOM的新增
        /// SAM 2017年7月27日02:26:46
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public static object Sfc00001BomAdd(JObject request)
        {
            try
            {
                string Token = request.Value<string>("Token");
                string userID = UniversalService.detoken(Token);

                string ItemProcessID = request.Value<string>("ItemProcessID");
                string ItemOperationID = request.Value<string>("ItemOperationID");
                string ItemID = request.Value<string>("ItemID");

                if (string.IsNullOrWhiteSpace(ItemID))
                    return new { status = "410", msg = "ItemID值为空" };

                String[] ItemIDs = ItemID.Split(',');
                foreach (string item in ItemIDs)
                {
                    SFC_ItemMaterial model = new SFC_ItemMaterial();
                    model.ItemMaterialID = UniversalService.GetSerialNumber("SFC_ItemMaterial");
                    if (string.IsNullOrWhiteSpace(ItemOperationID))
                        model.ItemProcessID = ItemProcessID;
                    else
                        model.ItemOperationID = ItemOperationID;
                    model.Sequence = "0";
                    model.ItemID = item;
                    model.BasicQuantity = 0;
                    model.AttritionRate = 0;
                    model.UseQuantity = 0;
                    model.Status = Framework.SystemID + "0201213000001";
                    SFC_ItemMaterialService.insert(userID, model);
                }
                return new { status = "200", msg = "新增成功" };
            }
            catch (Exception ex)
            {
                DataLogerService.writeerrlog(ex);
                return new { status = "400", msg = ex.Message };
            }
        }

        /// <summary>
        /// 制品BOM的删除
        /// SAM 2017年7月27日02:26:41
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public static object Sfc00001BomDelete(JObject request)
        {
            try
            {
                string Token = request.Value<string>("Token");
                string userID = UniversalService.detoken(Token);
                string ItemMaterialID = request.Value<string>("ItemMaterialID");

                if (string.IsNullOrWhiteSpace(ItemMaterialID))
                    return new { status = "410", msg = "ItemMaterialID值为空" };

                String[] FabMoItemIDs = ItemMaterialID.Split(',');
                foreach (string item in FabMoItemIDs)
                {
                    SFC_ItemMaterial model = SFC_ItemMaterialService.get(item);

                    model.Status = Framework.SystemID + "0201213000003";
                    SFC_ItemMaterialService.update(userID, model);
                }
                return new { status = "200", msg = "删除成功" };
            }
            catch (Exception ex)
            {
                DataLogerService.writeerrlog(ex);
                return new { status = "400", msg = ex.Message };
            }
        }

        /// <summary>
        /// 制品BOM的更新
        /// SAM 2017年7月27日02:26:29
        /// </summary>
        /// <param name="Token"></param>
        /// <param name="jArray"></param>
        /// <returns></returns>
        public static object Sfc00001Bomupdate(string Token, JArray jArray)
        {
            int success = 0;
            int fail = 0;
            string failIDs = null;
            string msg = null;
            string userid = UtilBussinessService.detoken(Token);
            JObject data = null;
            SFC_ItemMaterial model = null;
            for (int i = 0; i < jArray.Count; i++)
            {
                data = (JObject)jArray[i];
                try
                {
                    model = SFC_ItemMaterialService.get(data.Value<string>("ItemMaterialID"));
                    if (model == null)
                    {
                        failIDs = UtilBussinessService.str(failIDs, data.Value<string>("ItemMaterialID"));
                        msg = UtilBussinessService.str(msg, data.Value<string>("ItemMaterialID") + "流水号错误");
                        fail++;
                        continue;
                    }
                    model.BasicQuantity = data.Value<decimal>("BasicQuantity");
                    model.AttritionRate = data.Value<decimal>("AttritionRate");
                    model.UseQuantity = data.Value<decimal>("UseQuantity");
                    model.Comments = data.Value<string>("Comments");
                    if (SFC_ItemMaterialService.update(userid, model))
                        success++;
                    else
                    {
                        failIDs = UtilBussinessService.str(failIDs, data.Value<string>("ItemMaterialID"));
                        fail++;
                    }
                }
                catch (Exception ex)
                {
                    failIDs = UtilBussinessService.str(failIDs, data.Value<string>("FabMoItemID"));
                    msg = UtilBussinessService.str(msg, ex.ToString());
                    fail++;
                }
            }
            return new { success = success, fail = fail, failIDs = failIDs, msg = msg };
        }

        /// <summary>
        /// 根据制品制程流水号或者制品制程工序流水号获取资源（分页）
        /// MOUSE 2017年8月1日15:09:13
        /// </summary>
        /// <param name="token"></param>
        /// <param name="ItemProcessID"></param>制品制程流水号
        /// <param name="ItemOperationID"></param>制品工序流水号
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <returns></returns>
        public static object Sfc00001BOMResourceList(string token, string ItemProcessID, string ItemOperationID, int page, int rows)
        {
            int count = 0;
            IList<Hashtable> result = null;
            if (string.IsNullOrWhiteSpace(ItemOperationID))
                result = SFC_ItemResourceService.Sfc00001BOMResourceList(ItemProcessID, null, null, page, rows, ref count);
            else
                result = SFC_ItemResourceService.Sfc00001BOMResourceList(null, ItemOperationID, null, page, rows, ref count);
            return UtilBussinessService.getPaginationModel(result, count);
        }

        /// <summary>
        /// 根据制程流水号或者制程工序流水号获取资源(不分页)
        /// SAM 2017年7月27日12:09:15
        /// </summary>
        /// <param name="Token"></param>
        /// <param name="ItemProcessID"></param>
        /// <param name="ItemOperationID"></param>
        /// <returns></returns>
        public static object Sfc00001BOMGetResourceList(string Token, string ItemProcessID, string ItemOperationID)
        {
            IList<Hashtable> result = null;
            if (string.IsNullOrWhiteSpace(ItemOperationID))
                result = SFC_ItemResourceService.Sfc00001GetResourceList(ItemProcessID, null, null);
            else
                result = SFC_ItemResourceService.Sfc00001GetResourceList(null, ItemOperationID, null);
            return result;
        }


        /// <summary>
        ///根据制程流水号或者制程工序流水号获取不存在的资源(不分页)
        /// SAM  2017年7月27日12:12:43
        /// MOUSE 2017年8月2日16:35:07 修改为分页
        /// </summary>
        /// <param name="Token"></param>
        /// <param name="ItemProcessID"></param>
        /// <param name="ItemOperationID"></param>
        /// <returns></returns>
        public static object Sfc00001GetNoResourceList(string Token, string WorkCenterID, string ItemProcessID, string ItemOperationID, int page, int rows)
        {
            int count = 0;
            IList<Hashtable> result = null;
            if (string.IsNullOrWhiteSpace(ItemOperationID))
                result = SYS_ResourcesService.Sfc00001GetNoResourceList(WorkCenterID, ItemProcessID, null, page, rows, ref count);
            else
                result = SYS_ResourcesService.Sfc00001GetNoResourceList(WorkCenterID, null, ItemOperationID, page, rows, ref count);
            return UtilBussinessService.getPaginationModel(result, count);
        }

        /// <summary>
        /// 资源的移除
        /// SAM 2017年7月27日12:14:47
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public static object Sfc00001ResourceDelete(JObject request)
        {
            try
            {
                string Token = request.Value<string>("Token");
                string userID = UniversalService.detoken(Token);
                string ItemResourceID = request.Value<string>("ItemResourceID");

                if (string.IsNullOrWhiteSpace(ItemResourceID))
                    return new { status = "410", msg = "ItemResourceID值为空" };

                String[] FabMoResourceIDs = ItemResourceID.Split(',');
                foreach (string item in FabMoResourceIDs)
                {
                    SFC_ItemResourceService.delete(userID, item);
                }
                return new { status = "200", msg = "删除成功" };
            }
            catch (Exception ex)
            {
                DataLogerService.writeerrlog(ex);
                return new { status = "400", msg = ex.Message };
            }
        }

        /// <summary>
        /// 添加资源
        /// SAM 2017年7月15日18:02:18
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public static object Sfc00001ResourceAdd(JObject request)
        {
            try
            {
                string Token = request.Value<string>("Token");
                string userID = UniversalService.detoken(Token);

                string ItemProcessID = request.Value<string>("ItemProcessID");
                string ItemOperationID = request.Value<string>("ItemOperationID");
                string ClassCode = request.Value<string>("ClassCode");
                string ResourceID = request.Value<string>("ResourceID");

                if (string.IsNullOrWhiteSpace(ResourceID))
                    return new { status = "410", msg = "ResourceID值为空" };

                String[] ResourceIDs = ResourceID.Split(',');
                String[] ClassCodes = ClassCode.Split(',');
                //SYS_Resources ReModel = null;
                for (int i = 0; i < ResourceIDs.Length; i++)
                {
                    SFC_ItemResource model = new SFC_ItemResource();
                    model.ItemResourceID = UniversalService.GetSerialNumber("SFC_ItemResource");
                    if (string.IsNullOrWhiteSpace(ItemOperationID))
                        model.ItemProcessID = ItemProcessID;
                    else
                        model.ItemOperationID = ItemOperationID;
                    model.ResourceID = ResourceIDs[i];
                    model.IfMain = false;
                    model.Status = Framework.SystemID + "0201213000001";
                    if (ClassCodes[i] == "M")
                        model.Type = Framework.SystemID + "0201213000084";
                    else if (ClassCodes[i] == "L")
                        model.Type = Framework.SystemID + "0201213000085";
                    else
                        model.Type = Framework.SystemID + "0201213000086";
                    if (!SFC_ItemResourceService.Check(model.ResourceID, model.ItemProcessID, model.ItemOperationID))
                        SFC_ItemResourceService.insert(userID, model);
                }
                return new { status = "200", msg = "新增成功" };
            }
            catch (Exception ex)
            {
                DataLogerService.writeerrlog(ex);
                return new { status = "400", msg = ex.Message };
            }
        }

        /// <summary>
        /// 资源的更新
        /// SAM 2017年7月15日13:05:40
        /// </summary>
        /// <param name="Token"></param>
        /// <param name="jArray"></param>
        /// <returns></returns>
        public static object Sfc00001BOMResourceupdate(string Token, JArray jArray)
        {
            int success = 0;
            int fail = 0;
            string failIDs = null;
            string msg = null;
            string userid = UtilBussinessService.detoken(Token);
            JObject data = null;
            SFC_ItemResource model = null;
            for (int i = 0; i < jArray.Count; i++)
            {
                data = (JObject)jArray[i];
                try
                {
                    model = SFC_ItemResourceService.get(data.Value<string>("ItemResourceID"));
                    if (model == null)
                    {
                        failIDs = UtilBussinessService.str(failIDs, data.Value<string>("ItemResourceID"));
                        msg = UtilBussinessService.str(msg, data.Value<string>("ItemResourceID") + "流水号错误");
                        fail++;
                        continue;
                    }
                    model.IfMain = data.Value<bool>("IfMain");
                    model.Comments = data.Value<string>("Comments");
                    if (SFC_ItemResourceService.update(userid, model))
                        success++;
                    else
                    {
                        failIDs = UtilBussinessService.str(failIDs, data.Value<string>("ItemResourceID"));
                        fail++;
                    }
                }
                catch (Exception ex)
                {
                    failIDs = UtilBussinessService.str(failIDs, data.Value<string>("ItemResourceID"));
                    msg = UtilBussinessService.str(msg, ex.ToString());
                    fail++;
                }
            }
            return new { success = success, fail = fail, failIDs = failIDs, msg = msg };
        }



        #endregion

        #region 制品制程
        /// <summary>
        /// 制程列表
        /// SAM 2017年6月20日14:15:10
        /// </summary>
        /// <param name="token"></param>
        /// <param name="ItemID"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <returns></returns>
        public static object Sfc00001GetDetailList(string token, string ItemID, int page, int rows)
        {
            int count = 0;
            IList<Hashtable> result = SFC_ItemProcessService.Sfc00001GetDetailList(ItemID, page, rows, ref count);
            foreach (Hashtable item in result)
            {
                item["StandardHour"] = UtilBussinessService.HourConversionStr(item["StandardHour"].ToString());
                item["PrepareHour"] = UtilBussinessService.HourConversionStr(item["PrepareHour"].ToString());
            }
            return UtilBussinessService.getPaginationModel(result, count);
        }

        /// <summary>
        /// 制品制程的删除
        /// SAM 2017年8月30日21:55:16
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public static object Sfc00001Delete(JObject request)
        {
            string Token = request.Value<string>("Token");
            string userid = UtilBussinessService.detoken(Token);
            string ItemProcessID = request.Value<string>("ItemProcessID");
            SFC_ItemProcess model = SFC_ItemProcessService.get(request.Value<string>("ItemProcessID"));
            if (model == null)
                return new { status = "410", msg = "删除失败！不存在的制品制程信息" };

            model.Status = Framework.SystemID + "0201213000003";
            if (SFC_ItemProcessService.update(userid, model))
            {
                //删除所有相关的资料
                //删除制品制程用料
                SFC_ItemMaterialService.Delete(userid, model.ItemProcessID);
                //删除製品製程資源
                SFC_ItemResourceService.Delete(userid, model.ItemProcessID);
                //删除制品制程关系
                SFC_ItemProcessRelationShipService.Delete(userid, model.ItemProcessID);
                //删除制品制程替代关系
                SFC_ItemProcessAlternativeRelationShipService.Delete(userid, model.ItemProcessID);
                //删除制品制程工序
                SFC_ItemOperationService.DeleteByProcess(userid, model.ItemProcessID);
                //删除制品制程工序关系
                SFC_ProcessOperationRelationShipService.DeleteByProcess(userid, model.ItemProcessID);
                return new { status = "200", msg = "删除成功！" };
            }
            else
                return new { status = "410", msg = "删除失败！" };
        }

        /// <summary>
        /// 新增制程
        /// SAM 2017年6月20日14:21:12
        /// </summary>
        /// <param name="Token"></param>
        /// <param name="jArray"></param>
        /// <returns></returns>
        public static object Sfc00001insert(string Token, JArray jArray)
        {
            int success = 0;
            int fail = 0;
            string failIDs = null;
            string msg = null;
            string userid = UtilBussinessService.detoken(Token);
            JObject data = null;
            SFC_ItemProcess model = null;
            for (int i = 0; i < jArray.Count; i++)
            {
                data = (JObject)jArray[i];
                if (!SFC_ItemProcessService.Check(data.Value<string>("ProcessID"), data.Value<string>("ItemID"), null))
                {
                    model = new SFC_ItemProcess();
                    model.ItemProcessID = UniversalService.GetSerialNumber("SFC_ItemProcess");
                    model.ItemID = data.Value<string>("ItemID");
                    model.Sequence = data.Value<string>("Sequence");
                    model.ProcessID = data.Value<string>("ProcessID");
                    model.WorkCenterID = data.Value<string>("WorkCenterID");
                    model.AuxUnit = data.Value<string>("AuxUnit");
                    model.AuxUnitRatio = data.Value<decimal>("AuxUnitRatio");
                    model.Price = data.Value<decimal>("Price");
                    model.ResourceReport = data.Value<bool>("ResourceReport");
                    if (!string.IsNullOrWhiteSpace(data.Value<string>("StandardTime")))
                        model.StandardTime = data.Value<int>("StandardTime");
                    if (!string.IsNullOrWhiteSpace(data.Value<string>("PrepareTime")))
                        model.PrepareTime = data.Value<int>("PrepareTime");
                    model.IsIP = data.Value<bool>("IsIP");
                    model.IsFPI = data.Value<bool>("IsFPI");
                    model.IsOSI = data.Value<bool>("IsOSI");
                    model.InspectionGroupID = data.Value<string>("InspectionGroupID");
                    //model.IfRC = data.Value<bool>("IfRC");
                    model.RoutID = data.Value<string>("RoutID");
                    model.Comments = data.Value<string>("Comments");
                    model.Status = Framework.SystemID + "0201213000001";
                    if (SFC_ItemProcessService.insert(userid, model))
                        success++;
                    else
                    {
                        failIDs = UtilBussinessService.str(failIDs, data.Value<string>("ItemProcessID"));
                        fail++;
                    }
                }
                else
                {
                    failIDs = UtilBussinessService.str(failIDs, data.Value<string>("ItemProcessID"));
                    msg = UtilBussinessService.str(msg, "资料重复");
                    fail++;
                }
            }
            return new { success = success, fail = fail, failIDs = failIDs, msg = msg };
        }

        /// <summary>
        /// 制程删除
        /// SAM 2017年6月20日14:21:33
        /// </summary>
        /// <param name="Token"></param>
        /// <param name="jArray"></param>
        /// <returns></returns>
        public static object Sfc00001delete(string Token, JArray jArray)
        {
            int success = 0;
            int fail = 0;
            string failIDs = null;
            string msg = null;
            string userid = UtilBussinessService.detoken(Token);
            JObject data = null;
            SFC_ItemProcess model = null;
            for (int i = 0; i < jArray.Count; i++)
            {
                data = (JObject)jArray[i];
                model = SFC_ItemProcessService.get(data.Value<string>("ItemProcessID"));

                model.Status = Framework.SystemID + "0201213000003";
                if (SFC_ItemProcessService.update(userid, model))
                {
                    //删除所有相关的资料
                    //删除制品制程用料
                    SFC_ItemMaterialService.Delete(userid, model.ItemProcessID);
                    //删除製品製程資源
                    SFC_ItemResourceService.Delete(userid, model.ItemProcessID);
                    //删除制品制程关系
                    SFC_ItemProcessRelationShipService.Delete(userid, model.ItemProcessID);
                    //删除制品制程替代关系
                    SFC_ItemProcessAlternativeRelationShipService.Delete(userid, model.ItemProcessID);
                    //删除制品制程工序
                    SFC_ItemOperationService.DeleteByProcess(userid, model.ItemProcessID);
                    //删除制品制程工序关系
                    SFC_ProcessOperationRelationShipService.DeleteByProcess(userid, model.ItemProcessID);
                    success++;
                }
                else
                {
                    failIDs = UtilBussinessService.str(failIDs, data.Value<string>("ItemProcessID"));
                    fail++;
                }


            }
            return new { success = success, fail = fail, failIDs = failIDs, msg = msg };
        }

        /// <summary>
        /// 制程更新
        /// SAM 2017年6月20日14:21:51
        /// </summary>
        /// <param name="Token"></param>
        /// <param name="jArray"></param>
        /// <returns></returns>
        public static object Sfc00001update(string Token, JArray jArray)
        {
            int success = 0;
            int fail = 0;
            string failIDs = null;
            string msg = null;
            string userid = UtilBussinessService.detoken(Token);
            JObject data = null;
            SFC_ItemProcess model = null;
            for (int i = 0; i < jArray.Count; i++)
            {
                data = (JObject)jArray[i];

                model = SFC_ItemProcessService.get(data.Value<string>("ItemProcessID"));
                if (!SFC_ItemProcessService.Check(data.Value<string>("ProcessID"), data.Value<string>("ItemID"), data.Value<string>("ItemProcessID")))
                {
                    model.Sequence = data.Value<string>("Sequence");
                    model.ProcessID = data.Value<string>("ProcessID");
                    model.WorkCenterID = data.Value<string>("WorkCenterID");
                    model.AuxUnit = data.Value<string>("AuxUnit");
                    model.AuxUnitRatio = data.Value<decimal>("AuxUnitRatio");
                    model.Price = data.Value<decimal>("Price");
                    model.ResourceReport = data.Value<bool>("ResourceReport");
                    if (!string.IsNullOrWhiteSpace(data.Value<string>("StandardTime")))
                        model.StandardTime = data.Value<int>("StandardTime");
                    else
                        model.StandardTime = null;
                    if (!string.IsNullOrWhiteSpace(data.Value<string>("PrepareTime")))
                        model.PrepareTime = data.Value<int>("PrepareTime");
                    else
                        model.PrepareTime = null;
                    model.IsIP = data.Value<bool>("IsIP");
                    model.IsFPI = data.Value<bool>("IsFPI");
                    model.IsOSI = data.Value<bool>("IsOSI");
                    model.InspectionGroupID = data.Value<string>("InspectionGroupID");
                    //model.IfRC = data.Value<bool>("IfRC");
                    model.RoutID = data.Value<string>("RoutID");
                    model.Comments = data.Value<string>("Comments");
                    if (SFC_ItemProcessService.update(userid, model))
                        success++;
                    else
                    {
                        failIDs = UtilBussinessService.str(failIDs, data.Value<string>("ItemProcessID"));
                        fail++;
                    }
                }
                else
                {
                    failIDs = UtilBussinessService.str(failIDs, data.Value<string>("ItemProcessID"));
                    msg = UtilBussinessService.str(failIDs, "资料重复");
                    fail++;
                }
            }
            return new { success = success, fail = fail, failIDs = failIDs, msg = msg };
        }
        #endregion

        #region 制程用料
        /// <summary>
        /// 製品用料列表
        /// SAM 2017年6月21日09:39:18
        /// </summary>
        /// <param name="token"></param>
        /// <param name="ItemProcessID"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <returns></returns>
        public static object Sfc00001GetMaterialList(string token, string ItemProcessID, int page, int rows)
        {
            int count = 0;
            IList<Hashtable> result = SFC_ItemMaterialService.Sfc00001GetMaterialList(ItemProcessID, page, rows, ref count);
            return UtilBussinessService.getPaginationModel(result, count);
        }

        /// <summary>
        /// 制品制程/制品制程工序用料的删除
        /// SAM 2017年8月30日21:58:32
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public static object Sfc00001MaterialDelete(JObject request)
        {
            string Token = request.Value<string>("Token");
            string userid = UtilBussinessService.detoken(Token);
            string ItemMaterialID = request.Value<string>("ItemMaterialID");
            SFC_ItemMaterial model = SFC_ItemMaterialService.get(request.Value<string>("ItemMaterialID"));
            if (model == null)
                return new { status = "410", msg = "删除失败！不存在用料信息" };

            model.Status = Framework.SystemID + "0201213000003";
            if (SFC_ItemMaterialService.update(userid, model))
                return new { status = "200", msg = "删除成功！" };
            else
                return new { status = "410", msg = "删除失败！" };
        }

        /// <summary>
        /// 新增製品用料
        /// SAM 2017年6月22日11:46:02
        /// </summary>
        /// <param name="Token"></param>
        /// <param name="jArray"></param>
        /// <returns></returns>
        public static object Sfc00001Materialinsert(string Token, JArray jArray)
        {
            int success = 0;
            int fail = 0;
            string failIDs = null;
            string msg = null;
            string userid = UtilBussinessService.detoken(Token);
            JObject data = null;
            SFC_ItemMaterial model = null;
            for (int i = 0; i < jArray.Count; i++)
            {
                data = (JObject)jArray[i];
                if (SFC_ItemMaterialService.Check(data.Value<string>("ItemID"), data.Value<string>("ItemProcessID"), null, null))
                {
                    failIDs = UtilBussinessService.str(failIDs, data.Value<string>("ItemMaterialID"));
                    msg = UtilBussinessService.str(msg, "资料重复");
                    fail++;
                    continue;
                }

                if (string.IsNullOrWhiteSpace(data.Value<string>("ItemID")))
                {
                    failIDs = UtilBussinessService.str(failIDs, data.Value<string>("ItemMaterialID"));
                    msg = UtilBussinessService.str(msg, "料品代号错误");
                    fail++;
                    continue;
                }

                model = new SFC_ItemMaterial();
                model.ItemMaterialID = UniversalService.GetSerialNumber("SFC_ItemMaterial");
                model.ItemProcessID = data.Value<string>("ItemProcessID");
                model.Sequence = data.Value<string>("Sequence");
                model.ItemOperationID = data.Value<string>("ItemOperationID");
                model.ItemID = data.Value<string>("ItemID");
                model.BasicQuantity = data.Value<decimal>("BasicQuantity");
                model.AttritionRate = data.Value<decimal>("AttritionRate");
                model.UseQuantity = data.Value<decimal>("UseQuantity");
                //if (model.BasicQuantity == 0)
                //    model.UseQuantity = 0;
                //else if (model.AttritionRate == 0)
                //    model.UseQuantity = model.BasicQuantity;
                //else
                //    model.UseQuantity = model.BasicQuantity * (decimal)(1 + ((double)model.AttritionRate / (double)100));
                model.Comments = data.Value<string>("Comments");
                model.Status = Framework.SystemID + "0201213000001";
                if (SFC_ItemMaterialService.insert(userid, model))
                    success++;
                else
                {
                    failIDs = UtilBussinessService.str(failIDs, data.Value<string>("ItemMaterialID"));
                    fail++;
                }
            }
            return new { success = success, fail = fail, failIDs = failIDs, msg = msg };
        }

        /// <summary>
        /// 删除製品用料
        /// SAM 2017年6月22日11:44:53
        /// </summary>
        /// <param name="Token"></param>
        /// <param name="jArray"></param>
        /// <returns></returns>
        public static object Sfc00001Materialdelete(string Token, JArray jArray)
        {
            int success = 0;
            int fail = 0;
            string failIDs = null;
            string msg = null;
            string userid = UtilBussinessService.detoken(Token);
            JObject data = null;
            SFC_ItemMaterial model = null;
            for (int i = 0; i < jArray.Count; i++)
            {
                data = (JObject)jArray[i];
                model = SFC_ItemMaterialService.get(data.Value<string>("ItemMaterialID"));
                model.Status = Framework.SystemID + "0201213000003";
                if (SFC_ItemMaterialService.update(userid, model))
                    success++;
                else
                {
                    failIDs = UtilBussinessService.str(failIDs, data.Value<string>("ItemMaterialID"));
                    fail++;
                }


            }
            return new { success = success, fail = fail, failIDs = failIDs, msg = msg };
        }

        /// <summary>
        /// 更新製品用料
        /// SAM 2017年6月22日11:53:00
        /// </summary>
        /// <param name="Token"></param>
        /// <param name="jArray"></param>
        /// <returns></returns>
        public static object Sfc00001Materialupdate(string Token, JArray jArray)
        {
            int success = 0;
            int fail = 0;
            string failIDs = null;
            string msg = null;
            string userid = UtilBussinessService.detoken(Token);
            JObject data = null;
            SFC_ItemMaterial model = null;
            for (int i = 0; i < jArray.Count; i++)
            {
                data = (JObject)jArray[i];

                model = SFC_ItemMaterialService.get(data.Value<string>("ItemMaterialID"));
                if (SFC_ItemMaterialService.Check(data.Value<string>("ItemID"), data.Value<string>("ItemProcessID"), null, data.Value<string>("ItemMaterialID")))
                {
                    failIDs = UtilBussinessService.str(failIDs, data.Value<string>("ItemMaterialID"));
                    msg = UtilBussinessService.str(failIDs, "资料重复");
                    fail++;
                    continue;
                }

                if (string.IsNullOrWhiteSpace(data.Value<string>("ItemID")))
                {
                    failIDs = UtilBussinessService.str(failIDs, data.Value<string>("ItemMaterialID"));
                    msg = UtilBussinessService.str(msg, "料品代号错误");
                    fail++;
                    continue;
                }

                model.Sequence = data.Value<string>("Sequence");
                model.ItemID = data.Value<string>("ItemID");

                model.BasicQuantity = data.Value<decimal>("BasicQuantity");
                model.AttritionRate = data.Value<decimal>("AttritionRate");
                model.UseQuantity = data.Value<decimal>("UseQuantity");
                //if (model.BasicQuantity == 0)
                //    model.UseQuantity = 0;
                //else if (model.AttritionRate == 0)
                //    model.UseQuantity = model.BasicQuantity;
                //else
                //    model.UseQuantity = model.BasicQuantity * (decimal)(1 + ((double)model.AttritionRate / (double)100));
                model.Comments = data.Value<string>("Comments");
                if (SFC_ItemMaterialService.update(userid, model))
                    success++;
                else
                {
                    failIDs = UtilBussinessService.str(failIDs, data.Value<string>("ItemMaterialID"));
                    fail++;
                }

            }
            return new { success = success, fail = fail, failIDs = failIDs, msg = msg };
        }
        #endregion

        #region 制程资源
        /// <summary>
        /// 製品製程資源列表
        /// SAM 2017年6月21日10:29:33
        /// </summary>
        /// <param name="token"></param>
        /// <param name="ItemProcessID"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <returns></returns>
        public static object Sfc00001GetResourceList(string token, string ItemProcessID, string Type)
        {
            return SFC_ItemResourceService.Sfc00001GetResourceList(ItemProcessID, Type);
        }

        /// <summary>
        /// 获取工序资源列表
        /// SAM 2017年6月23日10:49:37
        /// </summary>
        /// <param name="token"></param>
        /// <param name="ItemOperationID"></param>
        /// <param name="Type"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <returns></returns>
        public static object Sfc00001GetOperationResourceList(string token, string ItemOperationID, string Type)
        {
            return SFC_ItemResourceService.Sfc00001GetOperationResourceList(ItemOperationID, Type);
        }

        /// <summary>
        /// 制品制程/制品制程工序的资料删除
        /// SAM 2017年8月30日22:01:04
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public static object Sfc01ResourceDelete(JObject request)
        {
            string Token = request.Value<string>("Token");
            string userid = UtilBussinessService.detoken(Token);
            string ItemResourceID = request.Value<string>("ItemResourceID");
            SFC_ItemResource model = SFC_ItemResourceService.get(ItemResourceID);
            if (model == null)
                return new { status = "410", msg = "删除失败！不存在资源信息" };

            model.Status = Framework.SystemID + "0201213000003";
            if (SFC_ItemResourceService.update(userid, model))
                return new { status = "200", msg = "删除成功！" };
            else
                return new { status = "410", msg = "删除失败！" };
        }

        /// <summary>
        /// 製程資源新增
        /// SAM 2017年6月22日15:21:07
        /// </summary>
        /// <param name="Token"></param>
        /// <param name="jArray"></param>
        /// <param name="Type"></param>
        /// <returns></returns>
        public static object Sfc00001ProcessResourceinsert(string Token, JArray jArray, string Type)
        {
            int success = 0;
            int fail = 0;
            string failIDs = null;
            string msg = null;
            string userid = UtilBussinessService.detoken(Token);
            Type = Framework.SystemID + Type;
            JObject data = null;
            SFC_ItemResource model = null;
            for (int i = 0; i < jArray.Count; i++)
            {
                data = (JObject)jArray[i];
                if (!SFC_ItemResourceService.Check(data.Value<string>("ResourceID"), data.Value<string>("ItemProcessID"), data.Value<string>("ItemOperationID"), Type, null))
                {
                    model = new SFC_ItemResource();
                    model.ItemResourceID = UniversalService.GetSerialNumber("SFC_ItemResource");
                    model.ItemProcessID = data.Value<string>("ItemProcessID");
                    model.ItemOperationID = data.Value<string>("ItemOperationID");
                    model.ResourceID = data.Value<string>("ResourceID");
                    model.Type = Type;
                    model.IfMain = data.Value<bool>("IfMain");
                    model.Comments = data.Value<string>("Comments");
                    model.Status = Framework.SystemID + "0201213000001";
                    if (SFC_ItemResourceService.insert(userid, model))
                        success++;
                    else
                    {
                        failIDs = UtilBussinessService.str(failIDs, data.Value<string>("ItemResourceID"));
                        fail++;
                    }
                }
                else
                {
                    failIDs = UtilBussinessService.str(failIDs, data.Value<string>("ItemResourceID"));
                    msg = UtilBussinessService.str(msg, "资料重复");
                    fail++;
                }
            }
            return new { success = success, fail = fail, failIDs = failIDs, msg = msg };
        }

        /// <summary>
        /// 制程资源删除
        /// SAM 2017年6月22日15:21:18
        /// </summary>
        /// <param name="Token"></param>
        /// <param name="jArray"></param>
        /// <returns></returns>
        public static object Sfc00001ProcessResourcedelete(string Token, JArray jArray)
        {
            int success = 0;
            int fail = 0;
            string failIDs = null;
            string msg = null;
            string userid = UtilBussinessService.detoken(Token);
            JObject data = null;
            SFC_ItemResource model = null;
            for (int i = 0; i < jArray.Count; i++)
            {
                data = (JObject)jArray[i];
                model = SFC_ItemResourceService.get(data.Value<string>("ItemResourceID"));

                model.Status = Framework.SystemID + "0201213000003";
                if (SFC_ItemResourceService.update(userid, model))
                    success++;
                else
                {
                    failIDs = UtilBussinessService.str(failIDs, data.Value<string>("ItemResourceID"));
                    fail++;
                }
            }
            return new { success = success, fail = fail, failIDs = failIDs, msg = msg };
        }

        /// <summary>
        /// 制程资源更新
        /// SAN 2017年6月22日15:21:44
        /// </summary>
        /// <param name="Token"></param>
        /// <param name="jArray"></param>
        /// <returns></returns>
        public static object Sfc00001ProcessResourceupdate(string Token, JArray jArray, string Type)
        {
            int success = 0;
            int fail = 0;
            string failIDs = null;
            string msg = null;
            string userid = UtilBussinessService.detoken(Token);
            JObject data = null;
            Type = Framework.SystemID + Type;
            SFC_ItemResource model = null;
            for (int i = 0; i < jArray.Count; i++)
            {
                data = (JObject)jArray[i];

                model = SFC_ItemResourceService.get(data.Value<string>("ItemResourceID"));
                if (!SFC_ItemResourceService.Check(data.Value<string>("ResourceID"), data.Value<string>("ItemProcessID"), data.Value<string>("ItemOperationID"), Type, data.Value<string>("ItemResourceID")))
                {
                    model.ResourceID = data.Value<string>("ResourceID");
                    model.IfMain = data.Value<bool>("IfMain");
                    model.Comments = data.Value<string>("Comments");
                    if (SFC_ItemResourceService.update(userid, model))
                        success++;
                    else
                    {
                        failIDs = UtilBussinessService.str(failIDs, data.Value<string>("ItemResourceID"));
                        fail++;
                    }
                }
                else
                {
                    failIDs = UtilBussinessService.str(failIDs, data.Value<string>("ItemResourceID"));
                    msg = UtilBussinessService.str(failIDs, "资料重复");
                    fail++;
                }
            }
            return new { success = success, fail = fail, failIDs = failIDs, msg = msg };
        }

        /// <summary>
        /// 根据制品制程资源获取明细
        /// SAM 2017年6月21日10:43:12
        /// </summary>
        /// <param name="token"></param>
        /// <param name="ItemProcessResourceID"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <returns></returns>
        public static object Sfc00001IPRDetailList(string token, string ResourceID, int page, int rows)
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
        #endregion

        #region 替代制程
        /// <summary>
        /// 根据制品制程获取替代制程列表
        /// SAM 2017年6月22日16:02:00
        /// </summary>
        /// <param name="token"></param>
        /// <param name="ItemProcessID"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <returns></returns>
        public static object Sfc00001GetLAlternativeRelationList(string token, string ItemProcessID, int page, int rows)
        {
            int count = 0;
            IList<Hashtable> result = SFC_ItemProcessAlternativeRelationShipService.Sfc00001GetLAlternativeRelationList(ItemProcessID, page, rows, ref count);
            return UtilBussinessService.getPaginationModel(result, count);
        }

        /// <summary>
        /// 替代制程新增
        /// SAM 2017年6月22日16:23:42
        /// </summary>
        /// <param name="Token"></param>
        /// <param name="jArray"></param>
        /// <param name="Type"></param>
        /// <returns></returns>
        public static object Sfc00001ARinsert(string Token, JArray jArray)
        {
            int success = 0;
            int fail = 0;
            string failIDs = null;
            string msg = null;
            string userid = UtilBussinessService.detoken(Token);
            JObject data = null;
            SFC_ItemProcessAlternativeRelationShip model = null;
            for (int i = 0; i < jArray.Count; i++)
            {
                data = (JObject)jArray[i];
                if (!SFC_ItemProcessAlternativeRelationShipService.Check(data.Value<string>("ProcessID"), data.Value<string>("ItemProcessID"), null))
                {
                    model = new SFC_ItemProcessAlternativeRelationShip();
                    model.IPARSID = UniversalService.GetSerialNumber("SFC_ItemProcessAlternativeRelationShip");
                    model.ItemProcessID = data.Value<string>("ItemProcessID");
                    model.Sequence = data.Value<string>("Sequence");
                    model.ProcessID = data.Value<string>("ProcessID");
                    model.WorkCenterID = data.Value<string>("WorkCenterID");
                    model.Unit = data.Value<string>("Unit");
                    model.UnitRatio = data.Value<decimal>("UnitRatio");
                    model.Price = data.Value<decimal>("Price");
                    model.Comments = data.Value<string>("Comments");
                    model.Status = Framework.SystemID + "0201213000001";
                    if (SFC_ItemProcessAlternativeRelationShipService.insert(userid, model))
                        success++;
                    else
                    {
                        failIDs = UtilBussinessService.str(failIDs, data.Value<string>("IPARSID"));
                        fail++;
                    }
                }
                else
                {
                    failIDs = UtilBussinessService.str(failIDs, data.Value<string>("ItemResourceID"));
                    msg = UtilBussinessService.str(msg, "资料重复");
                    fail++;
                }
            }
            return new { success = success, fail = fail, failIDs = failIDs, msg = msg };
        }

        /// <summary>
        /// 替代制程删除
        /// SAM 2017年6月22日15:21:18
        /// </summary>
        /// <param name="Token"></param>
        /// <param name="jArray"></param>
        /// <returns></returns>
        public static object Sfc00001ARdelete(string Token, JArray jArray)
        {
            int success = 0;
            int fail = 0;
            string failIDs = null;
            string msg = null;
            string userid = UtilBussinessService.detoken(Token);
            JObject data = null;
            SFC_ItemProcessAlternativeRelationShip model = null;
            for (int i = 0; i < jArray.Count; i++)
            {
                data = (JObject)jArray[i];
                model = SFC_ItemProcessAlternativeRelationShipService.get(data.Value<string>("IPARSID"));

                model.Status = Framework.SystemID + "0201213000003";
                if (SFC_ItemProcessAlternativeRelationShipService.update(userid, model))
                    success++;
                else
                {
                    failIDs = UtilBussinessService.str(failIDs, data.Value<string>("IPARSID"));
                    fail++;
                }
            }
            return new { success = success, fail = fail, failIDs = failIDs, msg = msg };
        }

        /// <summary>
        /// 替代制程的删除
        /// SAM 2017年8月30日22:02:46
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public static object Sfc01AlternativeRelationDelete(JObject request)
        {
            string Token = request.Value<string>("Token");
            string userid = UtilBussinessService.detoken(Token);
            string IPARSID = request.Value<string>("IPARSID");
            SFC_ItemProcessAlternativeRelationShip model = SFC_ItemProcessAlternativeRelationShipService.get(IPARSID);
            if (model == null)
                return new { status = "410", msg = "删除失败！不存在替代制程信息" };

            model.Status = Framework.SystemID + "0201213000003";
            if (SFC_ItemProcessAlternativeRelationShipService.update(userid, model))
                return new { status = "200", msg = "删除成功！" };
            else
                return new { status = "410", msg = "删除失败！" };
        }

        /// <summary>
        /// 替代制程更新
        /// SAN 2017年6月22日15:21:44
        /// </summary>
        /// <param name="Token"></param>
        /// <param name="jArray"></param>
        /// <returns></returns>
        public static object Sfc00001ARupdate(string Token, JArray jArray)
        {
            int success = 0;
            int fail = 0;
            string failIDs = null;
            string msg = null;
            string userid = UtilBussinessService.detoken(Token);
            JObject data = null;
            SFC_ItemProcessAlternativeRelationShip model = null;
            for (int i = 0; i < jArray.Count; i++)
            {
                data = (JObject)jArray[i];

                model = SFC_ItemProcessAlternativeRelationShipService.get(data.Value<string>("IPARSID"));
                if (!SFC_ItemProcessAlternativeRelationShipService.Check(data.Value<string>("ProcessID"), data.Value<string>("ItemProcessID"), data.Value<string>("IPARSID")))
                {
                    model.ProcessID = data.Value<string>("ProcessID");
                    model.Unit = data.Value<string>("Unit");
                    model.UnitRatio = data.Value<decimal>("UnitRatio");
                    model.Price = data.Value<decimal>("Price");
                    model.Comments = data.Value<string>("Comments");
                    if (SFC_ItemProcessAlternativeRelationShipService.update(userid, model))
                        success++;
                    else
                    {
                        failIDs = UtilBussinessService.str(failIDs, data.Value<string>("IPARSID"));
                        fail++;
                    }
                }
                else
                {
                    failIDs = UtilBussinessService.str(failIDs, data.Value<string>("IPARSID"));
                    msg = UtilBussinessService.str(failIDs, "资料重复");
                    fail++;
                }
            }
            return new { success = success, fail = fail, failIDs = failIDs, msg = msg };
        }
        #endregion

        #region 制程工序
        /// <summary>
        /// 根据制品制程获取制品工序列表
        /// SAM 2017年6月22日16:47:52
        /// </summary>
        /// <param name="token"></param>
        /// <param name="ItemProcessID"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <returns></returns>
        public static object Sfc00001GetItemOperationList(string token, string ItemProcessID, int page, int rows)
        {
            int count = 0;
            IList<Hashtable> result = SFC_ItemOperationService.Sfc00001GetItemOperationList(ItemProcessID, page, rows, ref count);
            foreach (Hashtable item in result)
            {
                if (!string.IsNullOrWhiteSpace(item["StandardHour"].ToString()))
                {
                    TimeSpan ts = new TimeSpan(0, 0, int.Parse(item["StandardHour"].ToString()));
                    item["StandardHour"] = ts.Hours.ToString().PadLeft(2, '0') + ":" + ts.Minutes.ToString().PadLeft(2, '0') + ":" + ts.Seconds.ToString().PadLeft(2, '0');
                }
                if (!string.IsNullOrWhiteSpace(item["PrepareHour"].ToString()))
                {
                    TimeSpan ts = new TimeSpan(0, 0, int.Parse(item["PrepareHour"].ToString()));
                    item["PrepareHour"] = ts.Hours.ToString().PadLeft(2, '0') + ":" + ts.Minutes.ToString().PadLeft(2, '0') + ":" + ts.Seconds.ToString().PadLeft(2, '0');
                }
            }
            return UtilBussinessService.getPaginationModel(result, count);
        }

        /// <summary>
        /// 添加制品工序保存
        /// SAm 2017年6月22日17:40:28
        /// </summary>
        /// <param name="Token"></param>
        /// <param name="jArray"></param>
        /// <returns></returns>
        public static object Sfc00001ItemOperationinsert(string Token, JArray jArray)
        {
            int success = 0;
            int fail = 0;
            string failIDs = null;
            string msg = null;
            string userid = UtilBussinessService.detoken(Token);
            JObject data = null;
            SFC_ItemOperation model = null;
            for (int i = 0; i < jArray.Count; i++)
            {
                data = (JObject)jArray[i];
                if (SFC_ItemOperationService.CheckOperation(data.Value<string>("OperationID"), data.Value<string>("ItemProcessID"), null))
                {
                    failIDs = UtilBussinessService.str(failIDs, data.Value<string>("ItemResourceID"));
                    msg = UtilBussinessService.str(msg, data.Value<string>("Operationcode") + "工序已存在");
                    fail++;
                    continue;
                }

                if (SFC_ItemOperationService.CheckSequence(data.Value<string>("Sequence"), data.Value<string>("ItemProcessID"), null))
                {
                    failIDs = UtilBussinessService.str(failIDs, data.Value<string>("ItemResourceID"));
                    msg = UtilBussinessService.str(msg, data.Value<string>("Sequence") + "序号已存在");
                    fail++;
                    continue;
                }

                model = new SFC_ItemOperation();
                model.ItemOperationID = UniversalService.GetSerialNumber("SFC_ItemOperation");
                model.ItemProcessID = data.Value<string>("ItemProcessID");
                model.Sequence = data.Value<string>("Sequence");
                model.OperationID = data.Value<string>("OperationID");
                model.ProcessID = data.Value<string>("ProcessID");
                model.WorkCenterID = data.Value<string>("WorkCenterID");
                model.Unit = data.Value<string>("Unit");
                model.UnitRatio = data.Value<decimal>("UnitRatio");
                if (!string.IsNullOrWhiteSpace(data.Value<string>("StandardTime")))
                    model.StandardTime = data.Value<int>("StandardTime");
                if (!string.IsNullOrWhiteSpace(data.Value<string>("PrepareTime")))
                    model.PrepareTime = data.Value<int>("PrepareTime");
                model.IsIP = data.Value<bool>("IsIP");
                model.IsFPI = data.Value<bool>("IsFPI");
                model.IsOSI = data.Value<bool>("IsOSI");
                model.InspectionGroupID = data.Value<string>("InspectionGroupID");
                model.Comments = data.Value<string>("Comments");
                model.Status = Framework.SystemID + "0201213000001";
                if (SFC_ItemOperationService.insert(userid, model))
                    success++;
                else
                {
                    failIDs = UtilBussinessService.str(failIDs, data.Value<string>("ItemOperationID"));
                    fail++;
                }

            }
            return new { success = success, fail = fail, failIDs = failIDs, msg = msg };
        }

        /// <summary>
        /// 制程工序的删除
        /// SAM 2017年8月30日22:04:48
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public static object Sfc00001ItemOperationDelete(JObject request)
        {
            string Token = request.Value<string>("Token");
            string userid = UtilBussinessService.detoken(Token);
            string ItemOperationID = request.Value<string>("ItemOperationID");
            SFC_ItemOperation model = SFC_ItemOperationService.get(ItemOperationID);
            if (model == null)
                return new { status = "410", msg = "删除失败！不存在制程工序信息" };

            model.Status = Framework.SystemID + "0201213000003";
            if (SFC_ItemOperationService.update(userid, model))
            {
                //删除所有相关的资料
                //删除工序用料
                SFC_ItemMaterialService.DeleteByOperation(userid, model.ItemOperationID);
                //删除工序資源
                SFC_ItemResourceService.DeleteByOperation(userid, model.ItemOperationID);
                //删除制品制程工序关系
                SFC_ProcessOperationRelationShipService.Delete(userid, model.ItemOperationID);
                return new { status = "200", msg = "删除成功！" };
            }
            else
                return new { status = "410", msg = "删除失败！" };
        }

        /// <summary>
        /// 删除制品工序保存
        /// SAM 2017年6月22日17:40:14
        /// </summary>
        /// <param name="Token"></param>
        /// <param name="jArray"></param>
        /// <returns></returns>
        public static object Sfc00001ItemOperationdelete(string Token, JArray jArray)
        {
            int success = 0;
            int fail = 0;
            string failIDs = null;
            string msg = null;
            string userid = UtilBussinessService.detoken(Token);
            JObject data = null;
            SFC_ItemOperation model = null;
            for (int i = 0; i < jArray.Count; i++)
            {
                data = (JObject)jArray[i];
                model = SFC_ItemOperationService.get(data.Value<string>("ItemOperationID"));

                model.Status = Framework.SystemID + "0201213000003";
                if (SFC_ItemOperationService.update(userid, model))
                {
                    //删除所有相关的资料
                    //删除工序用料
                    SFC_ItemMaterialService.DeleteByOperation(userid, model.ItemOperationID);
                    //删除工序資源
                    SFC_ItemResourceService.DeleteByOperation(userid, model.ItemOperationID);
                    //删除制品制程工序关系
                    SFC_ProcessOperationRelationShipService.Delete(userid, model.ItemOperationID);
                    success++;
                }
                else
                {
                    failIDs = UtilBussinessService.str(failIDs, data.Value<string>("ItemOperationID"));
                    fail++;
                }
            }
            return new { success = success, fail = fail, failIDs = failIDs, msg = msg };
        }

        /// <summary>
        /// 更新制品工序保存
        /// SAM 2017年6月22日17:40:23
        /// </summary>
        /// <param name="Token"></param>
        /// <param name="jArray"></param>
        /// <returns></returns>
        public static object Sfc00001ItemOperationupdate(string Token, JArray jArray)
        {
            int success = 0;
            int fail = 0;
            string failIDs = null;
            string msg = null;
            string userid = UtilBussinessService.detoken(Token);
            JObject data = null;
            SFC_ItemOperation model = null;
            for (int i = 0; i < jArray.Count; i++)
            {
                data = (JObject)jArray[i];

                model = SFC_ItemOperationService.get(data.Value<string>("ItemOperationID"));
                model.Unit = data.Value<string>("Unit");
                model.UnitRatio = data.Value<decimal>("UnitRatio");
                model.IsIP = data.Value<bool>("IsIP");
                model.IsFPI = data.Value<bool>("IsFPI");
                model.IsOSI = data.Value<bool>("IsOSI");
                if (!string.IsNullOrWhiteSpace(data.Value<string>("StandardTime")))
                    model.StandardTime = data.Value<int>("StandardTime");
                if (!string.IsNullOrWhiteSpace(data.Value<string>("PrepareTime")))
                    model.PrepareTime = data.Value<int>("PrepareTime");
                model.InspectionGroupID = data.Value<string>("InspectionGroupID");
                model.Comments = data.Value<string>("Comments");
                model.Status = Framework.SystemID + "0201213000001";
                if (SFC_ItemOperationService.update(userid, model))
                    success++;
                else
                {
                    failIDs = UtilBussinessService.str(failIDs, data.Value<string>("ItemOperationID"));
                    fail++;
                }
            }
            return new { success = success, fail = fail, failIDs = failIDs, msg = msg };
        }


        /// <summary>
        /// 製品工序用料列表
        /// SAM 2017年6月22日18:17:00
        /// </summary>
        /// <param name="token"></param>
        /// <param name="ItemProcessID"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <returns></returns>
        public static object Sfc00001GetOperationMaterialList(string token, string ItemOperationID, int page, int rows)
        {
            int count = 0;
            IList<Hashtable> result = SFC_ItemMaterialService.Sfc00001GetOperationMaterialList(ItemOperationID, page, rows, ref count);
            return UtilBussinessService.getPaginationModel(result, count);
        }

        /// <summary>
        /// 新增製品工序用料
        /// SAM 2017年6月22日11:46:02
        /// </summary>
        /// <param name="Token"></param>
        /// <param name="jArray"></param>
        /// <returns></returns>
        public static object Sfc00001OperationMaterialinsert(string Token, JArray jArray)
        {
            int success = 0;
            int fail = 0;
            string failIDs = null;
            string msg = null;
            string userid = UtilBussinessService.detoken(Token);
            JObject data = null;
            SFC_ItemMaterial model = null;
            for (int i = 0; i < jArray.Count; i++)
            {
                data = (JObject)jArray[i];
                if (!SFC_ItemMaterialService.OperationCheck(data.Value<string>("ItemID"), data.Value<string>("ItemOperationID"), null))
                {
                    model = new SFC_ItemMaterial();
                    model.ItemMaterialID = UniversalService.GetSerialNumber("SFC_ItemMaterial");
                    model.ItemProcessID = data.Value<string>("ItemProcessID");
                    model.Sequence = data.Value<string>("Sequence");
                    model.ItemOperationID = data.Value<string>("ItemOperationID");
                    model.ItemID = data.Value<string>("ItemID");
                    model.BasicQuantity = data.Value<decimal>("BasicQuantity");
                    model.AttritionRate = data.Value<decimal>("AttritionRate");
                    model.UseQuantity = data.Value<decimal>("UseQuantity");
                    //if (model.BasicQuantity == 0)
                    //    model.UseQuantity = 0;
                    //else if (model.AttritionRate == 0)
                    //    model.UseQuantity = model.BasicQuantity;
                    //else
                    //    model.UseQuantity = model.BasicQuantity * (decimal)(1 + ((double)model.UseQuantity / 100));
                    model.Comments = data.Value<string>("Comments");
                    model.Status = Framework.SystemID + "0201213000001";
                    if (SFC_ItemMaterialService.insert(userid, model))
                        success++;
                    else
                    {
                        failIDs = UtilBussinessService.str(failIDs, data.Value<string>("ItemMaterialID"));
                        fail++;
                    }
                }
                else
                {
                    failIDs = UtilBussinessService.str(failIDs, data.Value<string>("ItemMaterialID"));
                    msg = UtilBussinessService.str(msg, "资料重复");
                    fail++;
                }
            }
            return new { success = success, fail = fail, failIDs = failIDs, msg = msg };
        }

        /// <summary>
        /// 删除製品工序用料
        /// SAM 2017年6月22日11:44:53
        /// </summary>
        /// <param name="Token"></param>
        /// <param name="jArray"></param>
        /// <returns></returns>
        public static object Sfc00001OperationMaterialdelete(string Token, JArray jArray)
        {
            int success = 0;
            int fail = 0;
            string failIDs = null;
            string msg = null;
            string userid = UtilBussinessService.detoken(Token);
            JObject data = null;
            SFC_ItemMaterial model = null;
            for (int i = 0; i < jArray.Count; i++)
            {
                data = (JObject)jArray[i];
                model = SFC_ItemMaterialService.get(data.Value<string>("ItemMaterialID"));
                model.Status = Framework.SystemID + "0201213000003";
                if (SFC_ItemMaterialService.update(userid, model))
                    success++;
                else
                {
                    failIDs = UtilBussinessService.str(failIDs, data.Value<string>("ItemMaterialID"));
                    fail++;
                }


            }
            return new { success = success, fail = fail, failIDs = failIDs, msg = msg };
        }

        /// <summary>
        /// 更新製品工序用料
        /// SAM 2017年6月22日11:53:00
        /// </summary>
        /// <param name="Token"></param>
        /// <param name="jArray"></param>
        /// <returns></returns>
        public static object Sfc00001OperationMaterialupdate(string Token, JArray jArray)
        {
            int success = 0;
            int fail = 0;
            string failIDs = null;
            string msg = null;
            string userid = UtilBussinessService.detoken(Token);
            JObject data = null;
            SFC_ItemMaterial model = null;
            for (int i = 0; i < jArray.Count; i++)
            {
                data = (JObject)jArray[i];

                model = SFC_ItemMaterialService.get(data.Value<string>("ItemMaterialID"));
                if (!SFC_ItemMaterialService.OperationCheck(data.Value<string>("ItemID"), data.Value<string>("ItemOperationID"), data.Value<string>("ItemMaterialID")))
                {
                    model.Sequence = data.Value<string>("Sequence");
                    model.ItemID = data.Value<string>("ItemID");

                    model.BasicQuantity = data.Value<decimal>("BasicQuantity");
                    model.AttritionRate = data.Value<decimal>("AttritionRate");
                    model.UseQuantity = data.Value<decimal>("UseQuantity");
                    //if (model.BasicQuantity == 0)
                    //    model.UseQuantity = 0;
                    //else if (model.AttritionRate == 0)
                    //    model.UseQuantity = model.BasicQuantity;
                    //else
                    //    model.UseQuantity = model.BasicQuantity * (decimal)(1 + ((double)model.UseQuantity / 100));
                    model.Comments = data.Value<string>("Comments");
                    if (SFC_ItemMaterialService.update(userid, model))
                        success++;
                    else
                    {
                        failIDs = UtilBussinessService.str(failIDs, data.Value<string>("ItemMaterialID"));
                        fail++;
                    }
                }
                else
                {
                    failIDs = UtilBussinessService.str(failIDs, data.Value<string>("ItemMaterialID"));
                    msg = UtilBussinessService.str(failIDs, "资料重复");
                    fail++;
                }
            }
            return new { success = success, fail = fail, failIDs = failIDs, msg = msg };
        }

        /// <summary>
        /// 获取制品的制程关系列表(分页)
        /// SAM 2017年6月23日10:50:28
        /// </summary>
        /// <param name="token"></param>
        /// <param name="ItemID"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <returns></returns>
        public static object Sfc00001GetProcessRelationShipList(string token, string ItemID, int page, int rows)
        {
            int count = 0;
            IList<Hashtable> result = SFC_ItemProcessRelationShipService.Sfc00001GetProcessRelationShipList(ItemID, page, rows, ref count);
            return UtilBussinessService.getPaginationModel(result, count);
        }

        /// <summary>
        /// 制程关系新增
        /// SAM 2017年6月23日11:09:52
        /// </summary>
        /// <param name="Token"></param>
        /// <param name="jArray"></param>
        /// <param name="Type"></param>
        /// <returns></returns>
        public static object Sfc00001PRSinsert(string Token, JArray jArray)
        {
            int success = 0;
            int fail = 0;
            string failIDs = null;
            string msg = null;
            string userid = UtilBussinessService.detoken(Token);
            JObject data = null;
            SFC_ItemProcessRelationShip model = null;
            for (int i = 0; i < jArray.Count; i++)
            {
                data = (JObject)jArray[i];

                model = new SFC_ItemProcessRelationShip();
                model.IPRSID = UniversalService.GetSerialNumber("SFC_ItemProcessRelationShip");
                model.ItemID = data.Value<string>("ItemID");
                model.ItemProcessID = data.Value<string>("ItemProcessID");
                model.PreItemProcessID = data.Value<string>("PreItemProcessID");
                model.FinishProcess = false;
                model.IfMain = data.Value<bool>("IfMain");
                model.Comments = data.Value<string>("Comments");
                model.Status = Framework.SystemID + "0201213000001";
                if (SFC_ItemProcessRelationShipService.insert(userid, model))
                    success++;
                else
                {
                    failIDs = UtilBussinessService.str(failIDs, data.Value<string>("IPRSID"));
                    fail++;
                }
            }
            SFC_ItemProcessRelationShipService.SetFinishProcess(userid, model.ItemID);
            return new { success = success, fail = fail, failIDs = failIDs, msg = msg };
        }

        /// <summary>
        /// 制程关系删除
        /// SAM 2017年6月23日11:20:01  
        /// </summary>
        /// <param name="Token"></param>
        /// <param name="jArray"></param>
        /// <returns></returns>
        public static object Sfc00001PRSdelete(string Token, JArray jArray)
        {
            int success = 0;
            int fail = 0;
            string failIDs = null;
            string msg = null;
            string userid = UtilBussinessService.detoken(Token);
            JObject data = null;
            SFC_ItemProcessRelationShip model = null;
            for (int i = 0; i < jArray.Count; i++)
            {
                data = (JObject)jArray[i];
                model = SFC_ItemProcessRelationShipService.get(data.Value<string>("IPRSID"));

                model.Status = Framework.SystemID + "0201213000003";
                if (SFC_ItemProcessRelationShipService.update(userid, model))
                    success++;
                else
                {
                    failIDs = UtilBussinessService.str(failIDs, data.Value<string>("IPRSID"));
                    fail++;
                }
            }
            SFC_ItemProcessRelationShipService.SetFinishProcess(userid, model.ItemID);
            return new { success = success, fail = fail, failIDs = failIDs, msg = msg };
        }

        /// <summary>
        /// 制程关系的删除
        /// SAM 2017年8月30日22:05:54
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public static object Sfc00001ProcessRelationShipDelete(JObject request)
        {
            string Token = request.Value<string>("Token");
            string userid = UtilBussinessService.detoken(Token);
            string IPRSID = request.Value<string>("IPRSID");
            SFC_ItemProcessRelationShip model = SFC_ItemProcessRelationShipService.get(IPRSID);
            if (model == null)
                return new { status = "410", msg = "删除失败！不存在制程关系信息" };

            model.Status = Framework.SystemID + "0201213000003";
            if (SFC_ItemProcessRelationShipService.update(userid, model))
            {
                SFC_ItemProcessRelationShipService.SetFinishProcess(userid, model.ItemID);
                return new { status = "200", msg = "删除成功！" };
            }
            else
                return new { status = "410", msg = "删除失败！" };
        }




        /// <summary>
        /// 制程关系更新
        /// SAN 2017年6月23日15:29:401
        /// </summary>
        /// <param name="Token"></param>
        /// <param name="jArray"></param>
        /// <returns></returns>
        public static object Sfc00001PRSupdate(string Token, JArray jArray)
        {
            int success = 0;
            int fail = 0;
            string failIDs = null;
            string msg = null;
            string userid = UtilBussinessService.detoken(Token);
            JObject data = null;
            SFC_ItemProcessRelationShip model = null;
            for (int i = 0; i < jArray.Count; i++)
            {
                data = (JObject)jArray[i];

                model = SFC_ItemProcessRelationShipService.get(data.Value<string>("IPRSID"));
                model.ItemProcessID = data.Value<string>("ItemProcessID");
                model.PreItemProcessID = data.Value<string>("PreItemProcessID");
                model.IfMain = data.Value<bool>("IfMain");
                model.Comments = data.Value<string>("Comments");
                if (SFC_ItemProcessRelationShipService.update(userid, model))
                    success++;
                else
                {
                    failIDs = UtilBussinessService.str(failIDs, data.Value<string>("ItemProcessID"));
                    fail++;
                }
            }

            SFC_ItemProcessRelationShipService.SetFinishProcess(userid, model.ItemID);

            return new { success = success, fail = fail, failIDs = failIDs, msg = msg };
        }

        /// <summary>
        /// 单个保存制程关系
        /// SAM 2017年6月23日15:28:57
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static object ProcessRelationShipSave(JObject data)
        {
            string Token = data.Value<string>("Token");
            string userid = UtilBussinessService.detoken(Token);

            SFC_ItemProcessRelationShip model = null;
            model = SFC_ItemProcessRelationShipService.get(data.Value<string>("IPRSID"));
            if (model == null)
                return new { status = "410", msg = "流水号错误，不存在的流水号！" };

            model.ItemProcessID = data.Value<string>("ItemProcessID");
            model.PreItemProcessID = data.Value<string>("PreItemProcessID");
            model.IfMain = data.Value<bool>("IfMain");
            model.Comments = data.Value<string>("Comments");

            if (SFC_ItemProcessRelationShipService.update(userid, model))
            {
                SFC_ItemProcessRelationShipService.SetFinishProcess(userid, model.ItemID);
                return new { status = "200", msg = "更新成功！" };
            }
            else
                return new { status = "410", msg = "更新失败！" };
        }

        /// <summary>
        /// 获取制品的制程关系列表(不分页)
        /// SAM 2017年6月23日11:06:00
        /// </summary>
        /// <param name="token"></param>
        /// <param name="ItemID"></param>
        /// <returns></returns>
        public static IList<Hashtable> Sfc00001GetProcessRelationShipListNoPage(string token, string ItemID)
        {
            IList<Hashtable> result = SFC_ItemProcessRelationShipService.Sfc00001GetProcessRelationShipListNoPage(ItemID);
            return result;
        }

        /// <summary>
        /// 判断制程关系是否存在错误
        /// SAM 2017年6月29日14:21:16
        /// 1.判断是否存在两个以上最终制程
        /// 2.判断是否存在没有设定制程关系的制程
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static object CheckProcessRelationShip(JObject data)
        {
            string Token = data.Value<string>("Token");
            string userid = UtilBussinessService.detoken(Token);
            string ItemID = data.Value<string>("ItemID");

            if (SFC_ItemProcessService.CheckItemProcessRelationShip(ItemID))
                return new { status = "410", msg = "存在没有关系的制程！" };

            if (SFC_ItemProcessRelationShipService.CheckFinishProcess(ItemID))
                return new { status = "410", msg = "已存在最终制程！" };

            return new { status = "200", msg = "检查没问题！" };
        }

        /// <summary>
        /// 获取制程的工序关系列表（分页）
        /// SAM 2017年6月23日15:51:00
        /// </summary>
        /// <param name="token"></param>
        /// <param name="ItemProcessID"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <returns></returns>
        public static object Sfc00001GetOperationRelationShipList(string token, string ItemProcessID, int page, int rows)
        {
            int count = 0;
            IList<Hashtable> result = SFC_ProcessOperationRelationShipService.Sfc00001GetOperationRelationShipList(ItemProcessID, page, rows, ref count);
            return UtilBussinessService.getPaginationModel(result, count);
        }

        /// <summary>
        /// 判断制品制程工序是否存在错误
        /// SAM 2017年6月29日14:21:16
        /// 1.判断是否存在两个最终工序
        /// 2.判断是否存在没有设定工序关系的工序
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static object CheckProcessOperationRelationShip(JObject data)
        {
            string Token = data.Value<string>("Token");
            string userid = UtilBussinessService.detoken(Token);
            string ItemProcessID = data.Value<string>("ItemProcessID");

            if (SFC_ItemOperationService.CheckItemProcessOperationRelationShip(ItemProcessID))
                return new { status = "410", msg = "存在没有关系的工序！" };

            if (SFC_ProcessOperationRelationShipService.CheckFinishOperation(ItemProcessID))
                return new { status = "410", msg = "已存在最终工序！" };

            return new { status = "200", msg = "检查没问题！" };
        }

        /// <summary>
        /// 工序关系新增
        /// SAM 2017年6月23日11:09:52
        /// </summary>
        /// <param name="Token"></param>
        /// <param name="jArray"></param>
        /// <param name="Type"></param>
        /// <returns></returns>
        public static object Sfc00001ORSinsert(string Token, JArray jArray)
        {
            int success = 0;
            int fail = 0;
            string failIDs = null;
            string msg = null;
            string userid = UtilBussinessService.detoken(Token);
            JObject data = null;
            SFC_ProcessOperationRelationShip model = null;
            for (int i = 0; i < jArray.Count; i++)
            {
                data = (JObject)jArray[i];

                model = new SFC_ProcessOperationRelationShip();
                model.PORSID = UniversalService.GetSerialNumber("SFC_ProcessOperationRelationShip");
                model.ItemProcessID = data.Value<string>("ItemProcessID");
                model.ItemOperationID = data.Value<string>("ItemOperationID");
                model.PreItemOperationID = data.Value<string>("PreItemOperationID");
                model.FinishOperation = false;
                model.IfMain = data.Value<bool>("IfMain");
                model.Comments = data.Value<string>("Comments");
                model.Status = Framework.SystemID + "0201213000001";
                if (SFC_ProcessOperationRelationShipService.insert(userid, model))
                    success++;
                else
                {
                    failIDs = UtilBussinessService.str(failIDs, data.Value<string>("PORSID"));
                    fail++;
                }
            }
            SFC_ProcessOperationRelationShipService.CancelFinishOperation(userid, model.ItemProcessID);
            SFC_ProcessOperationRelationShipService.SetFinishOperation(userid, model.ItemProcessID);
            return new { success = success, fail = fail, failIDs = failIDs, msg = msg };
        }

        /// <summary>
        /// 工序关系删除
        /// SAM 2017年6月23日11:20:01  
        /// </summary>
        /// <param name="Token"></param>
        /// <param name="jArray"></param>
        /// <returns></returns>
        public static object Sfc00001ORSdelete(string Token, JArray jArray)
        {
            int success = 0;
            int fail = 0;
            string failIDs = null;
            string msg = null;
            string userid = UtilBussinessService.detoken(Token);
            JObject data = null;
            SFC_ProcessOperationRelationShip model = null;
            for (int i = 0; i < jArray.Count; i++)
            {
                data = (JObject)jArray[i];
                model = SFC_ProcessOperationRelationShipService.get(data.Value<string>("PORSID"));

                model.Status = Framework.SystemID + "0201213000003";
                if (SFC_ProcessOperationRelationShipService.update(userid, model))
                    success++;
                else
                {
                    failIDs = UtilBussinessService.str(failIDs, data.Value<string>("PORSID"));
                    fail++;
                }
            }
            return new { success = success, fail = fail, failIDs = failIDs, msg = msg };
        }

        /// <summary>
        /// 工序关系的删除
        /// SAM 2017年8月30日22:07:18
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public static object Sfc00001OperationRelationShipDelete(JObject request)
        {
            string Token = request.Value<string>("Token");
            string userid = UtilBussinessService.detoken(Token);
            string PORSID = request.Value<string>("PORSID");
            SFC_ProcessOperationRelationShip model = SFC_ProcessOperationRelationShipService.get(PORSID);
            if (model == null)
                return new { status = "410", msg = "删除失败！不存在工序关系信息" };

            model.Status = Framework.SystemID + "0201213000003";
            if (SFC_ProcessOperationRelationShipService.update(userid, model))
                return new { status = "200", msg = "删除成功！" };
            else
                return new { status = "410", msg = "删除失败！" };
        }

        /// <summary>
        /// 工序关系更新
        /// SAN 2017年6月23日15:29:401
        /// </summary>
        /// <param name="Token"></param>
        /// <param name="jArray"></param>
        /// <returns></returns>
        public static object Sfc00001ORSupdate(string Token, JArray jArray)
        {
            int success = 0;
            int fail = 0;
            string failIDs = null;
            string msg = null;
            string userid = UtilBussinessService.detoken(Token);
            JObject data = null;
            SFC_ProcessOperationRelationShip model = null;
            for (int i = 0; i < jArray.Count; i++)
            {
                data = (JObject)jArray[i];

                model = SFC_ProcessOperationRelationShipService.get(data.Value<string>("PORSID"));
                model.PreItemOperationID = data.Value<string>("PreItemOperationID");
                model.ItemOperationID = data.Value<string>("ItemOperationID");
                model.IfMain = data.Value<bool>("IfMain");
                model.Comments = data.Value<string>("Comments");
                if (SFC_ProcessOperationRelationShipService.update(userid, model))
                    success++;
                else
                {
                    failIDs = UtilBussinessService.str(failIDs, data.Value<string>("PORSID"));
                    fail++;
                }
            }
            SFC_ProcessOperationRelationShipService.CancelFinishOperation(userid, model.ItemProcessID);
            SFC_ProcessOperationRelationShipService.SetFinishOperation(userid, model.ItemProcessID);

            return new { success = success, fail = fail, failIDs = failIDs, msg = msg };
        }

        /// <summary>
        /// 单个保存制程关系
        /// SAM 2017年6月23日15:28:57
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static object OperationRelationShipSave(JObject data)
        {
            string Token = data.Value<string>("Token");
            string userid = UtilBussinessService.detoken(Token);

            SFC_ProcessOperationRelationShip model = null;
            model = SFC_ProcessOperationRelationShipService.get(data.Value<string>("PORSID"));
            if (model == null)
                return new { status = "410", msg = "流水号错误，不存在的流水号！" };

            model.ItemProcessID = data.Value<string>("ItemProcessID");
            model.PreItemOperationID = data.Value<string>("PreItemOperationID");
            model.IfMain = data.Value<bool>("IfMain");
            model.Comments = data.Value<string>("Comments");

            if (SFC_ProcessOperationRelationShipService.update(userid, model))
            {
                SFC_ProcessOperationRelationShipService.CancelFinishOperation(userid, model.ItemProcessID);
                SFC_ProcessOperationRelationShipService.SetFinishOperation(userid, model.ItemProcessID);
                return new { status = "410", msg = "更新成功！" };
            }
            else
                return new { status = "410", msg = "更新失败！" };
        }

        /// <summary>
        /// 获取制品的制程关系列表(不分页)
        /// SAM 2017年6月23日11:06:00
        /// </summary>
        /// <param name="token"></param>
        /// <param name="ItemID"></param>
        /// <returns></returns>
        public static IList<Hashtable> Sfc00001GetOperationRelationShipListNoPage(string token, string ItemProcessID)
        {
            IList<Hashtable> result = SFC_ProcessOperationRelationShipService.Sfc00001GetOperationRelationShipListNoPage(ItemProcessID);
            return result;
        }
        #endregion

        #endregion

        #region SFC00002制令单维护
        /// <summary>
        /// 获取生管列表
        /// Tom 2017年6月22日14点10分
        /// </summary>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <returns></returns>
        public static object Sfc00002GetProductManagerList(string Account, int page, int rows)
        {
            int count = 0;
            return UniversalService.getPaginationModel(
                SYS_MESUserService.GetProductManagerList(Account, page, rows, ref count), count);
        }

        /// <summary>
        /// 根据用户获取单据类别列表
        /// Tom
        /// </summary>
        /// <param name="Token"></param>
        /// <param name="MFCUserID"></param>
        /// <returns></returns>
        public static object Sfc00002GetBillTypeList(string token, string mFCUserID)
        {
            //return SYS_DocumentAuthorityService.GetTypeListByUser(mFCUserID);
            return SYS_DocumentTypeSettingService.GetTypeList(mFCUserID, Framework.SystemID + "0201213000035");
        }

        /// <summary>
        /// 根据流水号获取制令单数据
        /// Tom
        /// </summary>
        /// <param name="FabricatedMotherID"></param>
        /// <returns></returns>
        public static object Sfc00002GetFabricatedMother(string FabricatedMotherID)
        {
            return SFC_FabricatedMotherService.get(FabricatedMotherID);
        }

        /// <summary>
        /// 获取料品开窗数据
        /// Tom 2017年6月27日20点12分
        /// </summary>
        /// <param name="code"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <returns></returns>
        public static object Sfc00002GetItemsList(string code, int page, int rows)
        {
            int count = 0;
            return UniversalService.getPaginationModel(
                SYS_ItemsService.Inf00010GetVaildList(code, page, rows, ref count),
                count);
        }

        /// <summary>
        /// 获取用户列表
        /// Tom 2017年6月28日19点32
        /// </summary>
        /// <param name="Account"></param>
        /// <returns></returns>
        public static object Sfc00002GetUserList(string account, int page = 1, int rows = 10)
        {
            int count = 0;
            return UniversalService.getPaginationModel(
                SYS_MESUserService.Sfc00002GetList(account, page, rows, ref count),
                count);
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
        public static object Sfc00002GetCustomerList(string code, int page, int rows)
        {
            int count = 0;
            return UniversalService.getPaginationModel(
                SYS_CustomersService.GetVaildList(code, page, rows, ref count),
                count);
        }

        /// <summary>
        /// 制令单根据料品获取批号
        /// SAM 2017年9月19日16:26:55
        /// </summary>
        /// <param name="Token"></param>
        /// <param name="ItemID"></param>
        /// <param name="Date"></param>
        /// <returns></returns>
        public static object Sfc00002GetLotNumber(string Token, string ItemID, string Date)
        {
            string userid = UtilBussinessService.detoken(Token);
            SYS_Items model = SYS_ItemsService.get(ItemID);
            string BatchNumber = null;
            string Status = "200";
            string Msg = null;
            string AutoNumberRecordID = null;
            if (model == null)
            {
                return new
                {
                    Status = "400",
                    Msg = "料品流水号错误",
                    BatchNumber = BatchNumber,
                    AutoNumberRecordID = AutoNumberRecordID
                };
            }

            if (!model.Lot)
            {
                return new
                {
                    Status = Status,
                    Msg = Msg,
                    BatchNumber = BatchNumber,
                    AutoNumberRecordID = AutoNumberRecordID
                };
            }

            if (string.IsNullOrWhiteSpace(model.LotClassID))
            {
                return new
                {
                    Status = "400",
                    Msg = "料品设定批号管控，但料品并不存在批号类别设定",
                    BatchNumber = BatchNumber,
                    AutoNumberRecordID = AutoNumberRecordID
                };
            }

            SYS_AutoNumber Automodel = SYS_AutoNumberService.get(model.LotClassID);
            if (Automodel == null)
            {
                return new
                {
                    Status = "400",
                    Msg = "批号类别错误",
                    BatchNumber = BatchNumber,
                    AutoNumberRecordID = AutoNumberRecordID
                };

            }

            if (Automodel.Status != Framework.SystemID + "0201213000001")
            {
                return new
                {
                    Status = "210",
                    Msg = "批号类别已被作废",
                    BatchNumber = BatchNumber,
                    AutoNumberRecordID = AutoNumberRecordID
                };
            }

            string Prevchar = null;

            DateTime Now = DateTime.Now;
            if (!string.IsNullOrWhiteSpace(Date))
            {
                try
                {
                    Now = DateTime.Parse(Date);
                }
                catch (Exception ex)
                {
                    DataLogerService.writeerrlog(ex);
                }
            }

            string Year = Automodel.YearLength == 4 ? Now.ToString("yy") : null;
            string Month = Automodel.MonthLength == 2 ? Now.ToString("MM") : null;
            string day = Automodel.DateLength == 2 ? Now.ToString("dd") : null;

            Prevchar = Automodel.DefaultCharacter + Year + Month + day;
            try
            {
                SYS_AutoNumberRecord Recordmodel = SYS_AutoNumberRecordService.getByAutoNumber(Automodel.AutoNumberID, Prevchar);
                if (Recordmodel == null)
                {
                    Recordmodel = new SYS_AutoNumberRecord();
                    Recordmodel.AutoNumberRecordID = UniversalService.GetSerialNumber("SYS_AutoNumberRecord");
                    Recordmodel.AutoNumberID = Automodel.AutoNumberID;
                    Recordmodel.Num = 0;
                    Recordmodel.Prevchar = Prevchar;
                    Recordmodel.Status = Framework.SystemID + "0201213000001";
                    SYS_AutoNumberRecordService.insert(userid, Recordmodel);
                }
                AutoNumberRecordID = Recordmodel.AutoNumberRecordID;
                BatchNumber = Recordmodel.Prevchar + (Recordmodel.Num + 1).ToString().PadLeft(Automodel.NumLength, '0');
            }
            catch (Exception ex)
            {
                Status = "400";
                Msg = ex.ToString();
            }
            return new
            {
                Status = Status,
                Msg = Msg,
                BatchNumber = BatchNumber,
                AutoNumberRecordID = AutoNumberRecordID
            };
        }






        /// <summary>
        /// 添加制令单
        /// Tom 2017年6月29日00点07分
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public static object Sfc00002AddFabricatedMother(JObject request)
        {
            try
            {
                string Token = request.Value<string>("Token");
                string userID = UniversalService.detoken(Token);
                SFC_FabricatedMother model = request.ToObject<SFC_FabricatedMother>();
                model.FabricatedMotherID = UniversalService.GetSerialNumber("SFC_FabricatedMother");
                model.Status = Framework.SystemID + "0201213000028";

                if (string.IsNullOrWhiteSpace(model.ItemID))
                    return new { status = "400", msg = "料品流水号不能为空" };

                SYS_Items itemmodel = SYS_ItemsService.get(model.ItemID);
                if (itemmodel == null)
                    return new { status = "400", msg = "料品流水号错误" };

                string AutoNumberID = request.Value<string>("DocumentAutoNumberID");
                string AutoNumberRecordID = request.Value<string>("AutoNumberRecordID");
                /*当保存时,如果制令单号已存在，则自动获取下一制令单号*/
                /*SAM 2017年9月13日16:46:44*/
                /*
                 * 这里的计算逻辑有个弊端就是，存在一个情况是制令单新增进去了但是并没有及时更新流水，造成了无限死循环。
                 * 所以目前做了一个机制，循环计数，如果循环超过5次，就更新一次流水,然后重置循环次数
                 */
                int Seq = 1;
                while (SFC_FabricatedMotherService.Check(model.MoNo))
                {
                    if (Seq == 5)
                    {
                        UtilBussinessService.UpdateDocumentAutoNumber(userID, AutoNumberID);
                        Seq = 1;
                    }
                    else
                    {
                        AutoNumberID = null;
                        model.MoNo = UtilBussinessService.GetDocumentAutoNumber(userID, request.Value<string>("DocumentID"), model.Date.ToString(), ref AutoNumberID);
                        Seq++;
                    }
                }

                /*当保存时,如果批号已存在，则自动获取下一批号*/
                /*SAM 2017年9月13日16:46:44*/
                Seq = 1;
                if (!String.IsNullOrWhiteSpace(model.BatchNumber))
                {
                    while (SFC_FabricatedMotherService.CheckLot(model.BatchNumber))
                    {
                        if (Seq == 5)
                        {
                            UtilBussinessService.UpdateLotAutoNumber(userID, AutoNumberRecordID);
                            Seq = 1;
                        }
                        else
                        {
                            AutoNumberRecordID = null;
                            model.BatchNumber = UtilBussinessService.GetLotAutoNumber(userID, itemmodel.LotClassID, model.Date.ToString(), ref AutoNumberRecordID);
                            Seq++;
                        }
                    }
                }

                if (SFC_FabricatedMotherService.insert(userID, model))
                {
                    UtilBussinessService.UpdateDocumentAutoNumber(userID, AutoNumberID);
                    if (!String.IsNullOrWhiteSpace(model.BatchNumber))
                        UtilBussinessService.UpdateLotAutoNumber(userID, AutoNumberRecordID);
                    //指令单添加成功后
                    //需要根据料品添加制品制程
                    //然后根据制品制程分别添加制程用料，制程资源，制程关系，制程工序，制程工序用料，制程工序资源，制程工序关系
                    //根据料品获取制品制程并添加
                    List<SFC_ItemProcess> ItemProcess = SFC_ItemProcessService.GetListByItemID(model.ItemID);
                    foreach (SFC_ItemProcess Process in ItemProcess)
                    {
                        SFC_FabMoProcess Promodel = new SFC_FabMoProcess();
                        Promodel.FabMoProcessID = UniversalService.GetSerialNumber("SFC_FabMoProcess");
                        Promodel.FabricatedMotherID = model.FabricatedMotherID;
                        Promodel.Quantity = model.Quantity;
                        Promodel.AssignQuantity = 0;
                        Promodel.DifferenceQuantity = 0;
                        Promodel.Sequence = Process.Sequence;
                        Promodel.Status = model.Status;
                        Promodel.ProcessID = Process.ProcessID;
                        Promodel.WorkCenterID = Process.WorkCenterID;
                        Promodel.StartDate = model.StartDate;
                        Promodel.FinishDate = model.FinishDate;
                        Promodel.FinProQuantity = 0;
                        Promodel.OutProQuantity = 0;
                        Promodel.ScrappedQuantity = 0;
                        Promodel.DifferenceQuantity = 0;
                        Promodel.UnitID = model.UnitID;
                        Promodel.UnitRate = 1;
                        Promodel.StandardTime = Process.StandardTime * (int)model.Quantity;
                        Promodel.PrepareTime = Process.PrepareTime;
                        Promodel.SourceID = Process.ItemProcessID;//用于查询关系
                        if (SFC_FabMoProcessService.insert(userID, Promodel))
                        {
                            //根据制程获取制程用料
                            IList<SFC_ItemMaterial> ItemMaterial = SFC_ItemMaterialService.GetMaterialList(Process.ItemProcessID, null);
                            foreach (SFC_ItemMaterial item in ItemMaterial)
                            {
                                SFC_FabMoItem Matmodel = new SFC_FabMoItem();
                                Matmodel.FabMoItemID = UniversalService.GetSerialNumber("SFC_FabMoItem");
                                Matmodel.FabMoProcessID = Promodel.FabMoProcessID;
                                Matmodel.Sequence = item.Sequence.ToString();
                                Matmodel.ItemID = item.ItemID;
                                Matmodel.BaseQuantity = (int)model.Quantity * item.BasicQuantity;
                                Matmodel.AttritionRate = item.AttritionRate;
                                Matmodel.UseQuantity = item.UseQuantity * (int)model.Quantity;
                                if (itemmodel.IsCutMantissa)//是否切除尾数
                                {
                                    SYS_Parameters Parmodel = SYS_ParameterService.get(itemmodel.CutMantissa);
                                    if (Parmodel != null)
                                    {
                                        Matmodel.BaseQuantity = Math.Round(Matmodel.BaseQuantity, Parmodel.Code == "N" ? 0 : int.Parse(Parmodel.Code));
                                        Matmodel.UseQuantity = Math.Round(Matmodel.UseQuantity, Parmodel.Code == "N" ? 0 : int.Parse(Parmodel.Code));
                                    }
                                }
                                Matmodel.Status = Framework.SystemID + "0201213000001";
                                Matmodel.Comments = item.Comments;
                                SFC_FabMoItemService.insert(userID, Matmodel);
                            }
                            //根据制程获取制程资源
                            IList<SFC_ItemResource> ItemResource = SFC_ItemResourceService.GetResourceList(Process.ItemProcessID, null, null);
                            foreach (SFC_ItemResource item in ItemResource)
                            {
                                SFC_FabMoResource Resmodel = new SFC_FabMoResource();
                                Resmodel.FabMoResourceID = UniversalService.GetSerialNumber("SFC_FabMoResource");
                                Resmodel.FabMoProcessID = Promodel.FabMoProcessID;
                                Resmodel.ResourceID = item.ResourceID;
                                Resmodel.Type = item.Type;
                                Resmodel.IfMain = item.IfMain;
                                Resmodel.Status = Framework.SystemID + "0201213000001";
                                Resmodel.Comments = item.Comments;
                                SFC_FabMoResourceService.insert(userID, Resmodel);
                            }

                            //根据制程获取制程工序
                            IList<SFC_ItemOperation> ItemOperation = SFC_ItemOperationService.GetOperationList(Process.ItemProcessID);
                            foreach (SFC_ItemOperation item in ItemOperation)
                            {
                                SFC_FabMoOperation Opmodel = new SFC_FabMoOperation();
                                Opmodel.FabMoOperationID = UniversalService.GetSerialNumber("SFC_FabMoOperation");
                                Opmodel.FabricatedMotherID = Promodel.FabricatedMotherID;
                                Opmodel.FabMoProcessID = Promodel.FabMoProcessID;
                                Opmodel.OperationID = item.OperationID;
                                Opmodel.Sequence = item.Sequence;
                                Opmodel.UnitID = item.Unit;
                                Opmodel.UnitRate = item.UnitRatio;
                                Opmodel.Quantity = Promodel.Quantity;
                                Opmodel.StandardTime = item.StandardTime * (int)model.Quantity;
                                Opmodel.PrepareTime = item.PrepareTime;
                                Opmodel.StartDate = Promodel.StartDate;
                                Opmodel.FinishDate = Promodel.FinishDate;
                                Opmodel.Status = model.Status;
                                Opmodel.Comments = item.Comments;
                                Opmodel.SourceID = item.ItemOperationID;
                                SFC_FabMoOperationService.insert(userID, Opmodel);
                                //根据制程工序获取工序用料
                                IList<SFC_ItemMaterial> OpItemMaterial = SFC_ItemMaterialService.GetMaterialList(null, item.ItemOperationID);
                                foreach (SFC_ItemMaterial Opitem in OpItemMaterial)
                                {
                                    SFC_FabMoItem Matmodel = new SFC_FabMoItem();
                                    Matmodel.FabMoItemID = UniversalService.GetSerialNumber("SFC_FabMoItem");
                                    Matmodel.FabMoOperationID = Opmodel.FabMoOperationID;
                                    Matmodel.Sequence = Opitem.Sequence.ToString();
                                    Matmodel.ItemID = Opitem.ItemID;
                                    Matmodel.BaseQuantity = Opitem.BasicQuantity * (int)model.Quantity;
                                    Matmodel.AttritionRate = Opitem.AttritionRate;
                                    Matmodel.UseQuantity = Opitem.UseQuantity * (int)model.Quantity;
                                    if (itemmodel.IsCutMantissa)//是否切除尾数
                                    {
                                        SYS_Parameters Parmodel = SYS_ParameterService.get(itemmodel.CutMantissa);
                                        if (Parmodel != null)
                                        {
                                            Matmodel.BaseQuantity = Math.Round(Matmodel.BaseQuantity, Parmodel.Code == "N" ? 0 : int.Parse(Parmodel.Code));
                                            Matmodel.UseQuantity = Math.Round(Matmodel.UseQuantity, Parmodel.Code == "N" ? 0 : int.Parse(Parmodel.Code));
                                        }
                                    }
                                    Matmodel.Status = Framework.SystemID + "0201213000001";
                                    Matmodel.Comments = item.Comments;
                                    SFC_FabMoItemService.insert(userID, Matmodel);
                                }
                                //根据制程工序获取工序资源
                                IList<SFC_ItemResource> OpItemResource = SFC_ItemResourceService.GetResourceList(null, item.ItemOperationID, null);
                                foreach (SFC_ItemResource Opitem in ItemResource)
                                {
                                    SFC_FabMoResource Resmodel = new SFC_FabMoResource();
                                    Resmodel.FabMoResourceID = UniversalService.GetSerialNumber("SFC_FabMoResource");
                                    Resmodel.FabMoOperationID = Opmodel.FabMoOperationID;
                                    Resmodel.ResourceID = Opitem.ResourceID;
                                    Resmodel.Type = Opitem.Type;
                                    Resmodel.IfMain = Opitem.IfMain;
                                    Resmodel.Status = Framework.SystemID + "0201213000001";
                                    Resmodel.Comments = item.Comments;
                                    SFC_FabMoResourceService.insert(userID, Resmodel);
                                }
                            }

                            //首先根据制令制程获取刚刚添加好的制令制程工序列表
                            //然后循环工序列表，根据每一道工序对应的制品制程工序，获取设定好的关系列表
                            //循环关系列表，再新增成制令单的制程工序关系

                            IList<SFC_ProcessOperationRelationShip> OperationShip = SFC_ProcessOperationRelationShipService.GetOperationShipList(Process.ItemProcessID);
                            foreach (SFC_ProcessOperationRelationShip shipmodel in OperationShip)
                            {
                                SFC_FabMoOperation OpModel = null;
                                SFC_FabMoOperation PreOpModel = null;
                                SFC_FabMoOperationRelationship Resmodel = new SFC_FabMoOperationRelationship();
                                Resmodel.FabMoOperationRelationshipID = UniversalService.GetSerialNumber("SFC_FabMoOperationRelationship");
                                Resmodel.FabricatedMotherID = Promodel.FabricatedMotherID;
                                Resmodel.FabMoProcessID = Promodel.FabMoProcessID;
                                OpModel = SFC_FabMoOperationService.get(Resmodel.FabMoProcessID, shipmodel.ItemOperationID);

                                if (OpModel == null)
                                    continue;
                                Resmodel.FabMoOperationID = OpModel.FabMoOperationID;

                                PreOpModel = SFC_FabMoOperationService.get(Resmodel.FabMoProcessID, shipmodel.PreItemOperationID);
                                if (PreOpModel == null)
                                    continue;
                                Resmodel.PreFabMoOperationID = PreOpModel.FabMoOperationID;
                                Resmodel.IsLastOperation = shipmodel.FinishOperation;
                                Resmodel.IfMain = shipmodel.IfMain;
                                Resmodel.Status = Framework.SystemID + "0201213000001";
                                Resmodel.Comments = shipmodel.Comments;
                                SFC_FabMoOperationRelationshipService.insert(userID, Resmodel);

                            }
                        }
                    }
                    //然后循环制程列表，根据每一道制程对应的制品制程，获取设定好的制品制程关系列表
                    //循环关系列表，再新增成制令单的制程关系
                    IList<SFC_ItemProcessRelationShip> ProcessShip = SFC_ItemProcessRelationShipService.GetProcessShipList(model.ItemID);
                    foreach (SFC_ItemProcessRelationShip shipmodel in ProcessShip)
                    {
                        SFC_FabMoProcess ProModel = null;
                        SFC_FabMoProcess PreProModel = null;
                        SFC_FabMoRelationship Resmodel = new SFC_FabMoRelationship();
                        Resmodel.FabMoRelationshipID = UniversalService.GetSerialNumber("SFC_FabMoRelationship");
                        Resmodel.FabricatedMotherID = model.FabricatedMotherID;
                        ProModel = SFC_FabMoProcessService.get(model.FabricatedMotherID, shipmodel.ItemProcessID);
                        if (ProModel == null)
                            continue;
                        Resmodel.FabMoProcessID = ProModel.FabMoProcessID;

                        PreProModel = SFC_FabMoProcessService.get(model.FabricatedMotherID, shipmodel.PreItemProcessID);
                        if (PreProModel == null)
                            continue;
                        Resmodel.PreFabMoProcessID = PreProModel.FabMoProcessID;
                        Resmodel.IfLastProcess = shipmodel.FinishProcess;
                        Resmodel.IfMain = shipmodel.IfMain;
                        Resmodel.Status = Framework.SystemID + "0201213000001";
                        Resmodel.Comments = shipmodel.Comments;
                        SFC_FabMoRelationshipService.insert(userID, Resmodel);
                    }
                    //UtilBussinessService.updateAutoNumber(userID, request.Value<string>("ClassCode"), model.MoNo);                    
                    return new { status = "200", msg = "添加成功" };
                }
                return new { status = "400", msg = "添加失败" };

            }
            catch (Exception ex)
            {
                DataLogerService.writeerrlog(ex);
                return new { status = "400", msg = ex.Message };
            }
        }

        /// <summary>
        /// 获取制令单列表
        /// Tom 2017年6月29日04点06分
        /// </summary>
        /// <param name="Token"></param>
        /// <param name="MoNo"></param>
        /// <returns></returns>
        public static object Sfc00002FabricatedMotherList(string token, string MoNo,
            string StartItemCode, string EndItemCode,
            string StartFabMoCode, string EndFabMoCode,
            string CustCode, string MESUserCode,
            string ControlUser, string Status, int page = 1, int rows = 10)
        {
            int count = 0;
            return UniversalService.getPaginationModel(
                SFC_FabricatedMotherService.GetList(MoNo, StartItemCode, EndItemCode, StartFabMoCode, EndFabMoCode, CustCode, MESUserCode, ControlUser, Status, page, rows, ref count),
                count);
        }

        /// <summary>
        /// 修改制令单
        /// Tom 2017年6月29日19点03分
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public static object Sfc00002UpdateFabricatedMother(JObject request)
        {
            try
            {
                string Token = request.Value<string>("Token");
                string userID = UniversalService.detoken(Token);
                SFC_FabricatedMother model = request.ToObject<SFC_FabricatedMother>();

                if (model.Status != Framework.SystemID + "0201213000028")
                    return new { status = "200", msg = "已核发的制令单，不能编辑！" };

                if (!SFC_FabricatedMotherService.update(userID, model))
                {
                    return new { status = "400", msg = "更新失败" };
                }

                return new { status = "200", msg = "更新成功" };
            }
            catch (Exception ex)
            {
                DataLogerService.writeerrlog(ex);
                return new { status = "400", msg = ex.Message };
            }
        }


        /// <summary>
        /// 核发制令单
        /// SAM 2017年7月13日09:55:36
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public static object Sfc00002Approve(JObject request)
        {
            try
            {
                string Token = request.Value<string>("Token");
                string userID = UniversalService.detoken(Token);

                SFC_FabricatedMother model = SFC_FabricatedMotherService.get(request.Value<string>("FabricatedMotherID"));
                if (model == null)
                    return new { status = "410", msg = "不存在制令单信息！" };

                if (model.Status != Framework.SystemID + "0201213000028")
                    return new { status = "410", msg = "该制令单不是立单状态！" };


                //判断好制令单制程关系和制程工序关系是否正常




                model.Status = Framework.SystemID + "0201213000029";
                model.ApproveUserID = userID;
                model.ApproveDate = DateTime.Now;
                if (SFC_FabricatedMotherService.update(userID, model))
                {
                    //同步核发制令制程和制令制程工序
                    SFC_FabMoProcessService.updateByFabMo(userID, model.FabricatedMotherID, model.Status);
                    SFC_FabMoOperationService.updateByFabMo(userID, model.FabricatedMotherID, model.Status);
                    return new { status = "200", msg = "核发成功" };
                }
                else
                    return new { status = "410", msg = "核发失败" };
            }
            catch (Exception ex)
            {
                DataLogerService.writeerrlog(ex);
                return new { status = "400", msg = ex.Message };
            }
        }

        /// <summary>
        /// 制令单作废
        /// SAM 2017年7月13日09:56:12
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public static object Sfc00002Invalid(JObject request)
        {
            try
            {
                string Token = request.Value<string>("Token");
                string userID = UniversalService.detoken(Token);

                SFC_FabricatedMother model = SFC_FabricatedMotherService.get(request.Value<string>("FabricatedMotherID"));
                if (model == null)
                    return new { status = "410", msg = "不存在制令单信息！" };

                if (model.Status != Framework.SystemID + "0201213000028")
                    return new { status = "410", msg = "该制令单不是立单状态！" };

                model.Status = Framework.SystemID + "020121300002B";

                if (SFC_FabricatedMotherService.update(userID, model))
                {
                    //同步作废制令制程和制令制程工序
                    SFC_FabMoProcessService.updateByFabMo(userID, model.FabricatedMotherID, model.Status);
                    SFC_FabMoOperationService.updateByFabMo(userID, model.FabricatedMotherID, model.Status);
                    return new { status = "200", msg = "作废成功" };
                }
                else
                    return new { status = "410", msg = "作废失败" };
            }
            catch (Exception ex)
            {
                DataLogerService.writeerrlog(ex);
                return new { status = "400", msg = ex.Message };
            }
        }

        /// <summary>
        /// 制令单结案
        /// SAM 2017年7月13日10:07:57
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public static object Sfc00002Closed(JObject request)
        {
            try
            {
                string Token = request.Value<string>("Token");
                string userID = UniversalService.detoken(Token);

                SFC_FabricatedMother model = SFC_FabricatedMotherService.get(request.Value<string>("FabricatedMotherID"));
                if (model == null)
                    return new { status = "410", msg = "不存在制令单信息！" };

                if (model.Status != Framework.SystemID + "0201213000029")
                    return new { status = "410", msg = "该制令单不是核发状态！" };

                if (SFC_TaskDispatchService.CheckByFabMo(model.FabricatedMotherID))
                    return new { status = "410", msg = "存在立單NA,核發OP,進站IN的任务单！" };

                model.Status = Framework.SystemID + "020121300002A";

                if (SFC_FabricatedMotherService.update(userID, model))
                {
                    //同步结案制令制程和制令制程工序
                    SFC_FabMoProcessService.updateByFabMo(userID, model.FabricatedMotherID, model.Status);
                    SFC_FabMoOperationService.updateByFabMo(userID, model.FabricatedMotherID, model.Status);
                    return new { status = "200", msg = "结案成功" };
                }
                else
                    return new { status = "410", msg = "结案失败" };
            }
            catch (Exception ex)
            {
                DataLogerService.writeerrlog(ex);
                return new { status = "400", msg = ex.Message };
            }
        }

        /// <summary>
        /// 制令单还原
        /// SAM 2017年7月13日10:08:46
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public static object Sfc00002Reduction(JObject request)
        {
            try
            {
                string Token = request.Value<string>("Token");
                string userID = UniversalService.detoken(Token);

                SFC_FabricatedMother model = SFC_FabricatedMotherService.get(request.Value<string>("FabricatedMotherID"));
                if (model == null)
                    return new { status = "410", msg = "不存在制令单信息！" };

                if (model.Status != Framework.SystemID + "020121300002A")
                    return new { status = "410", msg = "该制令单不是结案状态！" };

                model.Status = Framework.SystemID + "0201213000029";

                if (SFC_FabricatedMotherService.update(userID, model))
                    return new { status = "200", msg = "还原成功" };
                else
                    return new { status = "410", msg = "还原失败" };
            }
            catch (Exception ex)
            {
                DataLogerService.writeerrlog(ex);
                return new { status = "400", msg = ex.Message };
            }
        }


        /// <summary>
        /// BOM和资源中左边树的显示
        /// SAM 2017年7月15日10:40:17
        /// </summary>
        /// <param name="Token"></param>
        /// <param name="FabricatedMotherID"></param>
        /// <returns></returns>
        public static object Sfc00002GetTreeList(string Token, string FabricatedMotherID)
        {
            List<Hashtable> result = new List<Hashtable>();
            IList<Hashtable> Process = SFC_FabMoProcessService.GetBomList(FabricatedMotherID);
            foreach (Hashtable item in Process)
            {
                result.Add(item);
                IList<Hashtable> Operation = SFC_FabMoOperationService.GetBomList(item["FabMoProcessID"].ToString());
                result.AddRange(Operation);
            }
            return result;
        }


        /// <summary>
        /// 根据指令制程流水号或者指令制程工序流水号获取BOM(不分页)
        /// SAM 2017年7月15日12:33:39
        /// </summary>
        /// <param name="Token"></param>
        /// <param name="FabricatedMotherID"></param>
        /// <returns></returns>
        public static object Sfc00002GetBomList(string Token, string FabMoProcessID, string FabMoOperationID)
        {
            IList<Hashtable> result = null;
            if (string.IsNullOrWhiteSpace(FabMoOperationID))
                result = SFC_FabMoItemService.GetItemList(FabMoProcessID, null);
            else
                result = SFC_FabMoItemService.GetItemList(null, FabMoOperationID);
            return result;
        }

        /// <summary>
        /// 根据指令制程流水号或者指令制程工序流水号获取不存在的Bom(不分页)
        /// SAM 2017年7月15日12:48:45
        /// </summary>
        /// <param name="Token"></param>
        /// <param name="FabMoProcessID"></param>
        /// <param name="FabMoOperationID"></param>
        /// <returns></returns>
        public static object Sfc00002GetNoBomList(string Token, string FabMoProcessID, string FabMoOperationID, int page, int rows)
        {
            int count = 0;
            IList<Hashtable> result = null;
            if (string.IsNullOrWhiteSpace(FabMoOperationID))
                result = SYS_ItemsService.Sfc00002GetNoBomList(FabMoProcessID, null, page, rows, ref count);
            else
                result = SYS_ItemsService.Sfc00002GetNoBomList(null, FabMoOperationID, page, rows, ref count);
            return UtilBussinessService.getPaginationModel(result, count);
        }

        /// <summary>
        /// BOM的新增
        /// SAM 2017年7月17日10:26:28
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public static object Sfc00002BomAdd(JObject request)
        {
            try
            {
                string Token = request.Value<string>("Token");
                string userID = UniversalService.detoken(Token);

                string FabMoProcessID = request.Value<string>("FabMoProcessID");
                string FabMoOperationID = request.Value<string>("FabMoOperationID");
                string ItemID = request.Value<string>("ItemID");

                if (string.IsNullOrWhiteSpace(ItemID))
                    return new { status = "410", msg = "ItemID值为空" };

                String[] ItemIDs = ItemID.Split(',');
                foreach (string item in ItemIDs)
                {
                    SFC_FabMoItem model = new SFC_FabMoItem();
                    model.FabMoItemID = UniversalService.GetSerialNumber("SFC_FabMoItem");
                    if (string.IsNullOrWhiteSpace(FabMoOperationID))
                        model.FabMoProcessID = FabMoProcessID;
                    else
                        model.FabMoOperationID = FabMoOperationID;
                    model.Sequence = "0";
                    model.ItemID = item;
                    model.BaseQuantity = 0;
                    model.AttritionRate = 0;
                    model.UseQuantity = 0;
                    model.Crityn = false;
                    model.Status = Framework.SystemID + "0201213000001";
                    if (!SFC_FabMoItemService.Check(model.ItemID, model.FabMoProcessID, model.FabMoOperationID, null))
                        SFC_FabMoItemService.insert(userID, model);
                }
                return new { status = "200", msg = "新增成功" };
            }
            catch (Exception ex)
            {
                DataLogerService.writeerrlog(ex);
                return new { status = "400", msg = ex.Message };
            }
        }

        /// <summary>
        /// BOM的删除
        /// SAM 2017年7月17日10:26:36
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public static object Sfc00002BomDelete(JObject request)
        {
            try
            {
                string Token = request.Value<string>("Token");
                string userID = UniversalService.detoken(Token);
                string FabMoItemID = request.Value<string>("FabMoItemID");

                if (string.IsNullOrWhiteSpace(FabMoItemID))
                    return new { status = "410", msg = "FabMoItemID值为空" };

                String[] FabMoItemIDs = FabMoItemID.Split(',');
                foreach (string item in FabMoItemIDs)
                {
                    SFC_FabMoItem model = SFC_FabMoItemService.get(item);

                    model.Status = Framework.SystemID + "0201213000003";
                    SFC_FabMoItemService.update(userID, model);
                }
                return new { status = "200", msg = "删除成功" };
            }
            catch (Exception ex)
            {
                DataLogerService.writeerrlog(ex);
                return new { status = "400", msg = ex.Message };
            }
        }


        /// <summary>
        /// BOM的更新
        /// SAM 2017年7月15日13:05:40
        /// </summary>
        /// <param name="Token"></param>
        /// <param name="jArray"></param>
        /// <returns></returns>
        public static object Sfc00002Bomupdate(string Token, JArray jArray)
        {
            int success = 0;
            int fail = 0;
            string failIDs = null;
            string msg = null;
            string userid = UtilBussinessService.detoken(Token);
            JObject data = null;
            SFC_FabMoItem model = null;
            for (int i = 0; i < jArray.Count; i++)
            {
                data = (JObject)jArray[i];
                try
                {
                    model = SFC_FabMoItemService.get(data.Value<string>("FabMoItemID"));
                    if (model == null)
                    {
                        failIDs = UtilBussinessService.str(failIDs, data.Value<string>("FabMoItemID"));
                        msg = UtilBussinessService.str(msg, data.Value<string>("FabMoItemID") + "流水号错误");
                        fail++;
                        continue;
                    }
                    model.BaseQuantity = data.Value<decimal>("BaseQuantity");
                    model.AttritionRate = data.Value<decimal>("AttritionRate");
                    model.UseQuantity = data.Value<decimal>("UseQuantity");
                    model.Crityn = data.Value<bool>("Crityn");
                    model.Comments = data.Value<string>("Comments");
                    model.Status = Framework.SystemID + "0201213000001";
                    if (SFC_FabMoItemService.update(userid, model))
                        success++;
                    else
                    {
                        failIDs = UtilBussinessService.str(failIDs, data.Value<string>("FabMoItemID"));
                        fail++;
                    }
                }
                catch (Exception ex)
                {
                    failIDs = UtilBussinessService.str(failIDs, data.Value<string>("FabMoItemID"));
                    msg = UtilBussinessService.str(msg, ex.ToString());
                    fail++;
                }
            }
            return new { success = success, fail = fail, failIDs = failIDs, msg = msg };
        }

        /// <summary>
        /// 根据指令制程流水号或者指令制程工序流水号获取资源(不分页)
        /// SAM 2017年7月15日13:08:32
        /// </summary>
        /// <param name="Token"></param>
        /// <param name="FabMoProcessID"></param>
        /// <param name="FabMoOperationID"></param>
        /// <returns></returns>
        public static object Sfc00002GetResourceList(string Token, string FabMoProcessID, string FabMoOperationID)
        {
            IList<Hashtable> result = null;
            if (string.IsNullOrWhiteSpace(FabMoOperationID))
                result = SFC_FabMoResourceService.Sfc00002GetResourceList(FabMoProcessID, null, null);
            else
                result = SFC_FabMoResourceService.Sfc00002GetResourceList(null, FabMoOperationID, null);
            return result;
        }


        /// <summary>
        ///根据指令制程流水号或者指令制程工序流水号获取不存在的资源(不分页)
        /// SAM 2017年7月15日12:48:45
        /// </summary>
        /// <param name="Token"></param>
        /// <param name="FabMoProcessID"></param>
        /// <param name="FabMoOperationID"></param>
        /// <returns></returns>
        public static object Sfc00002GetNoResourceList(string Token, string WorkCenterID, string FabMoProcessID, string FabMoOperationID, int page, int rows)
        {
            int count = 0;
            IList<Hashtable> result = null;
            if (string.IsNullOrWhiteSpace(FabMoOperationID))
                result = SYS_ResourcesService.Sfc00002GetNoResourceList(WorkCenterID, FabMoProcessID, null, page, rows, ref count);
            else
                result = SYS_ResourcesService.Sfc00002GetNoResourceList(WorkCenterID, null, FabMoOperationID, page, rows, ref count);
            return UtilBussinessService.getPaginationModel(result, count);
        }

        /// <summary>
        /// 资源的移除
        /// SAM 2017年7月15日18:02:51
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public static object Sfc00002ResourceDelete(JObject request)
        {
            try
            {
                string Token = request.Value<string>("Token");
                string userID = UniversalService.detoken(Token);
                string FabMoResourceID = request.Value<string>("FabMoResourceID");

                if (string.IsNullOrWhiteSpace(FabMoResourceID))
                    return new { status = "410", msg = "FabMoItemID值为空" };

                String[] FabMoResourceIDs = FabMoResourceID.Split(',');
                foreach (string item in FabMoResourceIDs)
                {
                    SFC_FabMoResourceService.delete(userID, item);
                }
                return new { status = "200", msg = "删除成功" };
            }
            catch (Exception ex)
            {
                DataLogerService.writeerrlog(ex);
                return new { status = "400", msg = ex.Message };
            }
        }

        /// <summary>
        /// 添加资源
        /// SAM 2017年7月15日18:02:18
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public static object Sfc00002ResourceAdd(JObject request)
        {
            try
            {
                string Token = request.Value<string>("Token");
                string userID = UniversalService.detoken(Token);

                string FabMoProcessID = request.Value<string>("FabMoProcessID");
                string FabMoOperationID = request.Value<string>("FabMoOperationID");
                string ClassCode = request.Value<string>("ClassCode");
                string ResourceID = request.Value<string>("ResourceID");

                if (string.IsNullOrWhiteSpace(ResourceID))
                    return new { status = "410", msg = "ResourceID值为空" };

                String[] ResourceIDs = ResourceID.Split(',');
                SYS_Resources ReModel = null;
                foreach (string item in ResourceIDs)
                {
                    SFC_FabMoResource model = new SFC_FabMoResource();
                    model.FabMoResourceID = UniversalService.GetSerialNumber("SFC_FabMoResource");
                    if (string.IsNullOrWhiteSpace(FabMoOperationID))
                        model.FabMoProcessID = FabMoProcessID;
                    else
                        model.FabMoOperationID = FabMoOperationID;
                    model.ResourceID = item;
                    ReModel = SYS_ResourcesService.get(item);
                    try
                    {
                        if (ReModel != null)
                            model.Quantity = (int)ReModel.Quantity;
                        else
                            model.Quantity = 0;
                    }
                    catch
                    {
                        model.Quantity = 0;
                    }
                    model.IfMain = false;
                    model.Status = Framework.SystemID + "0201213000001";
                    if (ClassCode == "M")
                        model.Type = Framework.SystemID + "0201213000084";
                    else if (ClassCode == "L")
                        model.Type = Framework.SystemID + "0201213000085";
                    else
                        model.Type = Framework.SystemID + "0201213000086";
                    SFC_FabMoResourceService.insert(userID, model);
                }
                return new { status = "200", msg = "新增成功" };
            }
            catch (Exception ex)
            {
                DataLogerService.writeerrlog(ex);
                return new { status = "400", msg = ex.Message };
            }
        }

        /// <summary>
        /// 资源的更新
        /// SAM 2017年7月15日13:05:40
        /// </summary>
        /// <param name="Token"></param>
        /// <param name="jArray"></param>
        /// <returns></returns>
        public static object Sfc00002Resourceupdate(string Token, JArray jArray)
        {
            int success = 0;
            int fail = 0;
            string failIDs = null;
            string msg = null;
            string userid = UtilBussinessService.detoken(Token);
            JObject data = null;
            SFC_FabMoResource model = null;
            for (int i = 0; i < jArray.Count; i++)
            {
                data = (JObject)jArray[i];
                try
                {
                    model = SFC_FabMoResourceService.get(data.Value<string>("FabMoResourceID"));
                    if (model == null)
                    {
                        failIDs = UtilBussinessService.str(failIDs, data.Value<string>("FabMoResourceID"));
                        msg = UtilBussinessService.str(msg, data.Value<string>("FabMoResourceID") + "流水号错误");
                        fail++;
                        continue;
                    }
                    model.Quantity = data.Value<int>("Quantity");
                    model.IfMain = data.Value<bool>("IfMain");
                    model.Comments = data.Value<string>("Comments");
                    if (SFC_FabMoResourceService.update(userid, model))
                        success++;
                    else
                    {
                        failIDs = UtilBussinessService.str(failIDs, data.Value<string>("FabMoResourceID"));
                        fail++;
                    }
                }
                catch (Exception ex)
                {
                    failIDs = UtilBussinessService.str(failIDs, data.Value<string>("FabMoResourceID"));
                    msg = UtilBussinessService.str(msg, ex.ToString());
                    fail++;
                }
            }
            return new { success = success, fail = fail, failIDs = failIDs, msg = msg };
        }


        /// <summary>
        /// 获取制令的制程关系列表（分页）
        /// SAM 2017年7月13日10:33:19
        /// </summary>
        /// <param name="token"></param>
        /// <param name="FabricatedMotherID"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <returns></returns>
        public static object Sfc00002GetFabMoRelShipList(string token, string FabricatedMotherID, int page, int rows)
        {
            int count = 0;
            IList<Hashtable> result = SFC_FabMoRelationshipService.Sfc00002GetFabMoRelShipList(FabricatedMotherID, page, rows, ref count);
            return UtilBussinessService.getPaginationModel(result, count);
        }


        /// <summary>
        /// 新增制令制程关系
        /// SAM 2017年7月13日10:40:11
        /// </summary>
        /// <param name="Token"></param>
        /// <param name="jArray"></param>
        /// <param name="Type"></param>
        /// <returns></returns>
        public static object Sfc00002FabMoRelShipinsert(string Token, JArray jArray)
        {
            int success = 0;
            int fail = 0;
            string failIDs = null;
            string msg = null;
            string userid = UtilBussinessService.detoken(Token);
            JObject data = null;
            SFC_FabMoRelationship model = null;
            for (int i = 0; i < jArray.Count; i++)
            {
                data = (JObject)jArray[i];

                model = new SFC_FabMoRelationship();
                model.FabMoRelationshipID = UniversalService.GetSerialNumber("SFC_FabMoRelationship");
                model.FabricatedMotherID = data.Value<string>("FabricatedMotherID");
                model.FabMoProcessID = data.Value<string>("FabMoProcessID");
                model.PreFabMoProcessID = data.Value<string>("PreFabMoProcessID");
                model.IfLastProcess = data.Value<bool>("IfLastProcess");
                model.IfMain = data.Value<bool>("IfMain");
                model.Comments = data.Value<string>("Comments");
                model.Status = Framework.SystemID + "0201213000001";
                if (SFC_FabMoRelationshipService.insert(userid, model))
                    success++;
                else
                {
                    failIDs = UtilBussinessService.str(failIDs, data.Value<string>("FabMoRelationshipID"));
                    fail++;
                }
            }
            SFC_FabMoRelationshipService.SetFinishProcess(userid, model.FabricatedMotherID);
            return new { success = success, fail = fail, failIDs = failIDs, msg = msg };
        }

        /// <summary>
        /// 更新制令制程关系
        /// SAN 2017年7月13日10:40:39
        /// </summary>
        /// <param name="Token"></param>
        /// <param name="jArray"></param>
        /// <returns></returns>
        public static object Sfc00002FabMoRelShipupdate(string Token, JArray jArray)
        {
            int success = 0;
            int fail = 0;
            string failIDs = null;
            string msg = null;
            string userid = UtilBussinessService.detoken(Token);
            JObject data = null;
            SFC_FabMoRelationship model = null;
            for (int i = 0; i < jArray.Count; i++)
            {
                data = (JObject)jArray[i];

                model = SFC_FabMoRelationshipService.get(data.Value<string>("FabMoRelationshipID"));
                model.FabMoProcessID = data.Value<string>("FabMoProcessID");
                model.PreFabMoProcessID = data.Value<string>("PreFabMoProcessID");
                model.IfMain = data.Value<bool>("IfMain");
                model.Comments = data.Value<string>("Comments");
                if (SFC_FabMoRelationshipService.update(userid, model))
                    success++;
                else
                {
                    failIDs = UtilBussinessService.str(failIDs, data.Value<string>("FabMoRelationshipID"));
                    fail++;
                }
            }
            SFC_FabMoRelationshipService.SetFinishProcess(userid, model.FabricatedMotherID);
            return new { success = success, fail = fail, failIDs = failIDs, msg = msg };
        }

        /// <summary>
        /// 单个保存制令制程关系
        /// SAM 2017年7月13日10:41:02
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static object FabMoRelShipSave(JObject data)
        {
            string Token = data.Value<string>("Token");
            string userid = UtilBussinessService.detoken(Token);

            SFC_FabMoRelationship model = null;
            model = SFC_FabMoRelationshipService.get(data.Value<string>("FabMoRelationshipID"));
            if (model == null)
                return new { status = "410", msg = "流水号错误，不存在的流水号！" };

            model.FabMoProcessID = data.Value<string>("FabMoProcessID");
            model.PreFabMoProcessID = data.Value<string>("PreFabMoProcessID");
            model.IfMain = data.Value<bool>("IfMain");
            model.Comments = data.Value<string>("Comments");

            if (SFC_FabMoRelationshipService.update(userid, model))
            {
                SFC_FabMoRelationshipService.SetFinishProcess(userid, model.FabricatedMotherID);
                return new { status = "200", msg = "更新成功！" };
            }
            else
                return new { status = "410", msg = "更新失败！" };
        }


        /// <summary>
        /// 获取制令的制程关系列表(不分页)
        /// SAM 2017年7月13日10:49:24
        /// </summary>
        /// <param name="token"></param>
        /// <param name="FabricatedMotherID"></param>
        /// <returns></returns>
        public static IList<Hashtable> Sfc00002GetFabMoRelShipNoPage(string token, string FabricatedMotherID)
        {
            IList<Hashtable> result = SFC_FabMoRelationshipService.Sfc00002GetFabMoRelShipNoPage(FabricatedMotherID);
            return result;
        }

        /// <summary>
        /// 制令制程关系删除
        /// SAM 2017年7月14日15:48:07
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public static object FabMoRelShipDelete(JObject request)
        {
            try
            {
                string Token = request.Value<string>("Token");
                string userID = UniversalService.detoken(Token);

                SFC_FabMoRelationship model = SFC_FabMoRelationshipService.get(request.Value<string>("FabMoRelationshipID"));
                if (model == null)
                    return new { status = "410", msg = "不存在的信息！" };

                model.Status = Framework.SystemID + "0201213000003";

                if (SFC_FabMoRelationshipService.update(userID, model))
                {
                    SFC_FabMoRelationshipService.SetFinishProcess(userID, model.FabricatedMotherID);
                    return new { status = "200", msg = "删除成功" };
                }
                else
                    return new { status = "200", msg = "删除成功" };
            }
            catch (Exception ex)
            {
                DataLogerService.writeerrlog(ex);
                return new { status = "400", msg = ex.Message };
            }
        }




        /// <summary>
        /// 获取制令制程列表
        /// TOM
        /// </summary>
        /// <param name="Token"></param>
        /// <param name="FabricatedMotherID"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <returns></returns>
        public static object Sfc00002FabricatedProcessList(string token, string FabricatedMotherID, int page, int rows)
        {
            int count = 0;
            return UniversalService.getPaginationModel(
                SFC_FabMoProcessService.Sfc00002FabricatedProcessList(FabricatedMotherID, page, rows, ref count),
                count);
        }

        /// <summary>
        /// 保存制令制程
        /// Tom 2017年6月29日09点44分
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public static object Sfc00002SaveFabricatedProcess(JObject request)
        {
            string Token = request.Value<string>("Token");
            string userID = UniversalService.detoken(Token);
            return UtilBussinessService.Save<SFC_FabMoProcess>(
                request, userID, m => m.FabMoProcessID, m => m.FabMoProcessID = UniversalService.GetSerialNumber("SFC_FabMoProcess"),
                SFC_FabMoProcessService.insert, SFC_FabMoProcessService.update, SFC_FabMoProcessService.delete,
                SFC_FabMoProcessService.CheckInsertArgs, "制程重复", SFC_FabMoProcessService.CheckUpdateArgs, "制程重复");
        }

        /// <summary>
        /// 删除制令制程
        /// SAM 2017年7月13日11:01:07
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static object Sfc00002FabMoProcessDelete(JObject data)
        {
            string Token = data.Value<string>("Token");
            string userid = UtilBussinessService.detoken(Token);

            SFC_FabMoProcess model = null;
            model = SFC_FabMoProcessService.get(data.Value<string>("FabMoProcessID"));
            if (model == null)
                return new { status = "410", msg = "流水号错误，不存在的流水号！" };

            model.Status = Framework.SystemID + "0201213000003";
            if (SFC_FabMoProcessService.update(userid, model))
                return new { status = "200", msg = "删除成功！" };
            else
                return new { status = "410", msg = "删除失败！" };
        }

        /// <summary>
        /// 获取制令用料列表
        /// Tom 2017年6月29日16点19分
        /// </summary>
        /// <param name="Token"></param>
        /// <param name="FabricatedMotherID"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <returns></returns>
        public static object Sfc00002GetFabMoItem(string token, string FabMoProcessID, int page, int rows)
        {
            int count = 0;
            return UniversalService.getPaginationModel(
                SFC_FabMoItemService.GetList(FabMoProcessID, page, rows, ref count),
                count);
        }

        /// <summary>
        /// 保存制令用料
        /// Tom 2017年6月30日09点24分
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public static object Sfc00002SaveFabricatedItem(JObject request)
        {

            string Token = request.Value<string>("Token");
            string userID = UniversalService.detoken(Token);
            return UtilBussinessService.Save<SFC_FabMoItem>(
                request,
                userID, m => m.FabMoItemID,
                m => m.FabMoItemID = UniversalService.GetSerialNumber("SFC_FabMoItem"),
                SFC_FabMoItemService.insert,
                SFC_FabMoItemService.update,
                SFC_FabMoItemService.delete,
                SFC_FabMoItemService.CheckInsertArgs, "用料重复", SFC_FabMoItemService.CheckUpdateArgs, "用料重复");
        }

        /// <summary>
        /// 删除制令用料
        /// SAM 2017年7月13日11:04:46
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static object Sfc00002FabMoItemDelete(JObject data)
        {
            string Token = data.Value<string>("Token");
            string userid = UtilBussinessService.detoken(Token);

            SFC_FabMoItem model = null;
            model = SFC_FabMoItemService.get(data.Value<string>("FabMoItemID"));
            if (model == null)
                return new { status = "410", msg = "流水号错误，不存在的流水号！" };

            model.Status = Framework.SystemID + "0201213000003";
            if (SFC_FabMoItemService.update(userid, model))
                return new { status = "200", msg = "删除成功！" };
            else
                return new { status = "410", msg = "删除失败！" };
        }



        /// <summary>
        /// 获取制令资源
        /// Tom 2017年7月3日15点38分
        /// </summary>
        /// <param name="Token"></param>
        /// <param name="FabMoProcessID"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <returns></returns>
        public static object Sfc00002GetFabricatedResourceEquipment(string token, string FabMoProcessID, int page, int rows)
        {
            int count = 0;
            return UniversalService.getPaginationModel(
                SFC_FabMoResourceService.GetEquipmentList(FabMoProcessID, page, rows, ref count),
                count);
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
        public static object Sfc00002GetFabricatedResourceUser(string Token, string FabMoProcessID, int page, int rows)
        {
            int count = 0;
            return UniversalService.getPaginationModel(
                SFC_FabMoResourceService.GetUserList(FabMoProcessID, page, rows, ref count),
                count);
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
        public static object Sfc00002GetFabricatedResourceOther(string Token, string FabMoProcessID, int page, int rows)
        {
            int count = 0;
            return UniversalService.getPaginationModel(
                SFC_FabMoResourceService.GetOtherList(FabMoProcessID, page, rows, ref count),
                count);
        }

        /// <summary>
        /// 保存制令资源
        /// Tom 2017年6月30日09点24分
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public static object Sfc00002SaveFabMoResource(JObject request)
        {
            string Token = request.Value<string>("Token");
            string userID = UniversalService.detoken(Token);
            return UtilBussinessService.Save<SFC_FabMoResource>(
                request,
                userID, m => m.FabMoResourceID,
                m => m.FabMoResourceID = UniversalService.GetSerialNumber("SFC_FabMoResource"),
                SFC_FabMoResourceService.insert,
                SFC_FabMoResourceService.update,
                SFC_FabMoResourceService.delete,
                SFC_FabMoResourceService.CheckInsertArgs,
                "资源重复",
                SFC_FabMoResourceService.CheckUpdateArgs,
                "资源重复");
        }

        /// <summary>
        /// 制令资源的新增
        /// SAM 2017年8月31日11:14:19
        /// </summary>
        /// <param name="Token"></param>
        /// <param name="jArray"></param>
        /// <returns></returns>
        public static object Sfc02FabResourceinsert(string Token, JArray jArray)
        {
            int success = 0;
            int fail = 0;
            string failIDs = null;
            string msg = null;
            string userid = UtilBussinessService.detoken(Token);
            JObject data = null;
            SFC_FabMoResource model = null;
            for (int i = 0; i < jArray.Count; i++)
            {
                data = (JObject)jArray[i];
                if (string.IsNullOrWhiteSpace(data.Value<string>("ResourceID")))
                {
                    failIDs = UtilBussinessService.str(failIDs, data.Value<string>("FabMoResourceID"));
                    msg = UtilBussinessService.str(msg, "资料为空");
                    fail++;
                    continue;
                }

                if (SFC_FabMoResourceService.CheckResource(data.Value<string>("ResourceID"), data.Value<string>("FabMoProcessID"),
                    data.Value<string>("FabMoOperationID"), Framework.SystemID + data.Value<string>("Type"), null))
                {
                    failIDs = UtilBussinessService.str(failIDs, data.Value<string>("FabMoResourceID"));
                    msg = UtilBussinessService.str(msg, "资料重复");
                    fail++;
                    continue;
                }
                model = new SFC_FabMoResource();
                model.FabMoResourceID = UniversalService.GetSerialNumber("SFC_FabMoResource");

                model.FabMoProcessID = data.Value<string>("FabMoProcessID");
                model.FabMoOperationID = data.Value<string>("FabMoOperationID");
                model.ResourceID = data.Value<string>("ResourceID");
                model.Quantity = string.IsNullOrWhiteSpace(data.Value<string>("Quantity")) ? 0 : data.Value<int>("Quantity");
                model.Type = Framework.SystemID + data.Value<string>("Type");
                model.IfMain = data.Value<bool>("IfMain");
                model.Comments = data.Value<string>("Comments");
                model.Status = Framework.SystemID + "0201213000001";
                if (SFC_FabMoResourceService.insert(userid, model))
                    success++;
                else
                {
                    failIDs = UtilBussinessService.str(failIDs, data.Value<string>("FabMoProcessID"));
                    fail++;
                }
            }

            return new { success = success, fail = fail, failIDs = failIDs, msg = msg };
        }

        /// <summary>
        /// 制令资源的更新
        /// SAM 2017年8月31日11:14:29
        /// </summary>
        /// <param name="Token"></param>
        /// <param name="jArray"></param>
        /// <returns></returns>
        public static object Sfc02FabResourceupdate(string Token, JArray jArray)
        {
            int success = 0;
            int fail = 0;
            string failIDs = null;
            string msg = null;
            string userid = UtilBussinessService.detoken(Token);
            JObject data = null;
            SFC_FabMoResource model = null;
            for (int i = 0; i < jArray.Count; i++)
            {
                data = (JObject)jArray[i];

                model = SFC_FabMoResourceService.get(data.Value<string>("FabMoResourceID"));
                if (model == null)
                {
                    failIDs = UtilBussinessService.str(failIDs, data.Value<string>("FabMoResourceID"));
                    msg = UtilBussinessService.str(msg, "不存在的资源设定");
                    fail++;
                    continue;
                }

                if (SFC_FabMoResourceService.CheckResource(data.Value<string>("ResourceID"), data.Value<string>("FabMoProcessID"),
                   data.Value<string>("FabMoOperationID"), Framework.SystemID + data.Value<string>("Type"), data.Value<string>("FabMoResourceID")))
                {
                    failIDs = UtilBussinessService.str(failIDs, data.Value<string>("FabMoResourceID"));
                    msg = UtilBussinessService.str(msg, "资料重复");
                    fail++;
                    continue;
                }

                model.ResourceID = data.Value<string>("ResourceID");
                model.Quantity = string.IsNullOrWhiteSpace(data.Value<string>("Quantity")) ? 0 : data.Value<int>("Quantity");
                model.IfMain = data.Value<bool>("IfMain");
                model.Comments = data.Value<string>("Comments");
                if (SFC_FabMoResourceService.update(userid, model))
                    success++;
                else
                {
                    failIDs = UtilBussinessService.str(failIDs, data.Value<string>("FabMoResourceID"));
                    fail++;
                }
            }
            return new { success = success, fail = fail, failIDs = failIDs, msg = msg };
        }





        /// <summary>
        /// 制令制程资源的删除
        /// SAM 2017年7月13日11:12:51
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static object Sfc00002FabMoResourceDelete(JObject data)
        {
            string Token = data.Value<string>("Token");
            string userid = UtilBussinessService.detoken(Token);

            SFC_FabMoResource model = null;
            model = SFC_FabMoResourceService.get(data.Value<string>("FabMoResourceID"));
            if (model == null)
                return new { status = "410", msg = "流水号错误，不存在的流水号！" };

            model.Status = Framework.SystemID + "0201213000003";
            if (SFC_FabMoResourceService.delete(userid, model))
                return new { status = "200", msg = "删除成功！" };
            else
                return new { status = "410", msg = "删除失败！" };
        }



        /// <summary>
        /// 根据制令制程流水号获取制令制程信息
        /// SAM 2017年7月17日17:03:26
        /// </summary>
        /// <param name="fabMoProcessID"></param>
        /// <returns></returns>
        public static Hashtable Sfc00002GetFabMoProcess(string fabMoProcessID)
        {
            return SFC_FabMoProcessService.Sfc00002GetFabMoProcess(fabMoProcessID);
        }

        /// <summary>
        /// 根据制令制程工序流水号获取制令制程工序信息
        /// SA< 2017年7月17日18:27:31
        /// </summary>
        /// <param name="fabMoProcessID"></param>
        /// <returns></returns>
        public static Hashtable Sfc00002GetFabMoOperation(string FabMoOperationID)
        {
            return SFC_FabMoOperationService.Sfc00002GetFabMoOperation(FabMoOperationID);
        }


        /// <summary>
        /// 获取拆解替代列表
        /// Tom 2017年7月5日09点49分
        /// </summary>
        /// <param name="Token"></param>
        /// <returns></returns>
        public static object Sfc00002GetFabMoSplitList(string token, string OriginalFabMoProcessID, int page = 1, int rows = 10)
        {
            int count = 0;
            IList<Hashtable> result = SFC_FabMoProcessService.GetSplitList(OriginalFabMoProcessID, page, rows, ref count);
            return UniversalService.getPaginationModel(result, count);
        }

        ///// <summary>
        ///// 保存拆解替代数据
        ///// Tom 2017年7月6日15点17分
        ///// </summary>
        ///// <param name="request"></param>
        ///// <returns></returns>
        //public static object Sfc00002SaveFabricatedProcessSplit(JObject request)
        //{
        //    string Token = request.Value<string>("Token");
        //    string userID = UniversalService.detoken(Token);

        //    Func<string, SFC_FabMoProcess, bool> insert = (u, m) =>
        //   {
        //       string originID = m.OriginalFabMoProcessID;
        //       string PreFabMoProcessID = m.PreFabMoProcessID;
        //       float price = m.Price;

        //       m = SFC_FabMoProcessService.get(m.FabMoProcessID);
        //       if (m == null)
        //       {
        //           return false;
        //       }

        //       m.OriginalFabMoProcessID = originID;
        //       m.Price = price;

        //       if (!SFC_FabMoProcessService.update(userID, m))
        //       {
        //           return false;
        //       }

        //       SFC_FabMoRelationship r = new SFC_FabMoRelationship();
        //       r.FabMoProcessID = m.FabMoProcessID;
        //       r.PreFabMoProcessID = PreFabMoProcessID;
        //       r.Status = Framework.SystemID + "0201213000001";
        //       r.FabMoRelationshipID = UniversalService.GetSerialNumber("SFC_FabMoRelationship");
        //       if (!SFC_FabMoRelationshipService.insert(userID, r))
        //       {
        //           return false;
        //       }

        //       // TODO: 料品处理

        //       return true;
        //   };

        //    Func<string, SFC_FabMoProcess, bool> update = (u, m) =>
        //    {
        //        string originID = m.OriginalFabMoProcessID;
        //        string PreFabMoProcessID = m.PreFabMoProcessID;
        //        float price = m.Price;

        //        m = SFC_FabMoProcessService.get(m.FabMoProcessID);
        //        if (m == null)
        //        {
        //            return false;
        //        }

        //        m.OriginalFabMoProcessID = originID;
        //        m.Price = price;

        //        if (!SFC_FabMoProcessService.update(userID, m))
        //        {
        //            return false;
        //        }

        //        if (!SFC_FabMoRelationshipService.updateByFabricatedProcessID(
        //            userID, m.FabMoProcessID, PreFabMoProcessID))
        //        {
        //            return false;
        //        }

        //        return true;
        //    };

        //    return UtilBussinessService.Save<SFC_FabMoProcess>(
        //        request,
        //        userID, m => m.FabMoProcessID,
        //        m => m.FabMoProcessID = m.FabMoProcessID,
        //        insert,
        //        update,
        //        null, SFC_FabMoProcessService.CheckSplitUpdateArgs, "制令制程关系重复",
        //        SFC_FabMoProcessService.CheckSplitUpdateArgs, "制令制程关系重复");
        //}

        /// <summary>
        /// 获取一个制令制程的替代关系列表
        /// Sam 2017年7月13日14:09:51
        /// </summary>
        /// <param name="Token"></param>
        /// <param name="OriginalFabMoProcessID"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <returns></returns>
        public static object Sfc00002GetFabMoAltRelShipList(string token, string ItemID, string ProcessID, int page, int rows)
        {
            SFC_ItemProcess model = SFC_ItemProcessService.getByItemProcess(ItemID, ProcessID);
            if (model == null)
                return null;

            int count = 0;
            return UniversalService.getPaginationModel(
                SFC_ItemProcessAlternativeRelationShipService.Sfc00002GetFabMoAltRelShipList(model.ItemProcessID, page, rows, ref count), count);
        }

        /// <summary>
        /// 拆单替代数据的新增
        /// SAM 2017年7月14日10:12:20
        /// </summary>
        /// <param name="Token"></param>
        /// <param name="jArray"></param>
        /// <returns></returns>
        public static object Sfc00002FabMoProcessSplitinsert(string Token, JArray jArray)
        {
            int success = 0;
            int fail = 0;
            string failIDs = null;
            string msg = null;
            string userid = UtilBussinessService.detoken(Token);
            JObject data = null;
            SFC_FabMoProcess model = null;
            for (int i = 0; i < jArray.Count; i++)
            {
                data = (JObject)jArray[i];
                try
                {
                    if (SFC_FabMoProcessService.CheckSplitProcessID(data.Value<string>("ProcessID"), data.Value<string>("WorkCenterID"), data.Value<string>("OriginalFabMoProcessID"), null))
                    {
                        failIDs = UtilBussinessService.str(failIDs, data.Value<string>("FabMoProcessID"));
                        msg = UtilBussinessService.str(msg, "资料重复");
                        fail++;
                        continue;
                    }
                    model = new SFC_FabMoProcess();
                    model.FabMoProcessID = UniversalService.GetSerialNumber("SFC_FabMoProcess");
                    model.FabricatedMotherID = data.Value<string>("FabricatedMotherID");
                    model.ProcessID = data.Value<string>("ProcessID");
                    model.Sequence = data.Value<string>("Sequence");
                    model.WorkCenterID = data.Value<string>("WorkCenterID");
                    model.StartDate = data.Value<DateTime>("StartDate");
                    model.FinishDate = data.Value<DateTime>("FinishDate");
                    model.Price = data.Value<int>("Price");
                    model.UnitID = data.Value<string>("UnitID");
                    model.UnitRate = data.Value<decimal>("UnitRate");
                    if (!string.IsNullOrWhiteSpace(data.Value<string>("StandardTime")))
                        model.StandardTime = data.Value<int>("StandardTime");
                    else
                        model.StandardTime = 0;
                    if (!string.IsNullOrWhiteSpace(data.Value<string>("PrepareTime")))
                        model.PrepareTime = data.Value<int>("PrepareTime");
                    else
                        model.PrepareTime = 0;
                    model.Status = data.Value<string>("Status");
                    model.Comments = data.Value<string>("Comments");
                    model.Quantity = data.Value<decimal>("Quantity");
                    model.OriginalFabMoProcessID = data.Value<string>("OriginalFabMoProcessID");
                    if (SFC_FabMoProcessService.insert(userid, model))
                    {
                        //根据原制程流水号，为新制程添加用料，资源，制程关系，工序，工序用料，工序资源，工序关系
                        //获取制程用料
                        //根据制程获取制程用料
                        IList<SFC_FabMoItem> ItemMaterial = SFC_FabMoItemService.GetFabMoItemList(model.OriginalFabMoProcessID, null);
                        foreach (SFC_FabMoItem item in ItemMaterial)
                        {
                            SFC_FabMoItem Matmodel = new SFC_FabMoItem();
                            Matmodel.FabMoItemID = UniversalService.GetSerialNumber("SFC_FabMoItem");
                            Matmodel.FabMoProcessID = model.FabMoProcessID;
                            Matmodel.Sequence = item.Sequence.ToString();
                            Matmodel.ItemID = item.ItemID;
                            Matmodel.BaseQuantity = item.BaseQuantity;
                            Matmodel.AttritionRate = item.AttritionRate;
                            Matmodel.Status = Framework.SystemID + "0201213000001";
                            Matmodel.Comments = item.Comments;
                            SFC_FabMoItemService.insert(userid, Matmodel);
                        }
                        //根据制程获取制程资源
                        IList<SFC_FabMoResource> ItemResource = SFC_FabMoResourceService.GetFabMoResourceList(model.OriginalFabMoProcessID, null);
                        foreach (SFC_FabMoResource item in ItemResource)
                        {
                            SFC_FabMoResource Resmodel = new SFC_FabMoResource();
                            Resmodel.FabMoResourceID = UniversalService.GetSerialNumber("SFC_FabMoResource");
                            Resmodel.FabMoProcessID = model.FabMoProcessID;
                            Resmodel.ResourceID = item.ResourceID;
                            Resmodel.Type = item.Type;
                            Resmodel.IfMain = item.IfMain;
                            Resmodel.Status = Framework.SystemID + "0201213000001";
                            Resmodel.Comments = item.Comments;
                            SFC_FabMoResourceService.insert(userid, Resmodel);
                        }
                        //根据制程获取制程工序
                        IList<SFC_FabMoOperation> ItemOperation = SFC_FabMoOperationService.GetFabMoOperationList(model.OriginalFabMoProcessID);
                        foreach (SFC_FabMoOperation item in ItemOperation)
                        {
                            SFC_FabMoOperation Opmodel = new SFC_FabMoOperation();
                            Opmodel.FabMoOperationID = UniversalService.GetSerialNumber("SFC_FabMoOperation");
                            Opmodel.FabricatedMotherID = model.FabricatedMotherID;
                            Opmodel.FabMoProcessID = model.FabMoProcessID;
                            Opmodel.OperationID = item.OperationID;
                            Opmodel.Sequence = item.Sequence;
                            Opmodel.UnitID = item.UnitID;
                            Opmodel.UnitRate = item.UnitRate;
                            Opmodel.StartDate = item.StartDate;
                            Opmodel.FinishDate = item.FinishDate;
                            Opmodel.Quantity = item.Quantity;
                            Opmodel.FinProQuantity = item.FinProQuantity;
                            Opmodel.OutProQuantity = item.OutProQuantity;
                            Opmodel.ScrappedQuantity = item.ScrappedQuantity;
                            Opmodel.DifferenceQuantity = item.DifferenceQuantity;
                            Opmodel.PreProQuantity = item.PreProQuantity;
                            Opmodel.AssignQuantity = item.AssignQuantity;
                            Opmodel.ResourceReport = item.ResourceReport;
                            Opmodel.StandardTime = item.StandardTime;
                            Opmodel.PrepareTime = item.PrepareTime;
                            Opmodel.Status = item.Status;
                            Opmodel.Comments = item.Comments;
                            Opmodel.SourceID = item.OperationID;
                            SFC_FabMoOperationService.insert(userid, Opmodel);
                            //根据制程工序获取工序用料
                            IList<SFC_FabMoItem> OpMaterial = SFC_FabMoItemService.GetFabMoItemList(null, item.FabMoOperationID);
                            foreach (SFC_FabMoItem Opitem in OpMaterial)
                            {
                                SFC_FabMoItem Matmodel = new SFC_FabMoItem();
                                Matmodel.FabMoItemID = UniversalService.GetSerialNumber("SFC_FabMoItem");
                                Matmodel.FabMoOperationID = item.FabMoOperationID;
                                Matmodel.Sequence = Opitem.Sequence.ToString();
                                Matmodel.ItemID = Opitem.ItemID;
                                Matmodel.BaseQuantity = Opitem.BaseQuantity;
                                Matmodel.AttritionRate = Opitem.AttritionRate;
                                Matmodel.Status = Framework.SystemID + "0201213000001";
                                Matmodel.Comments = item.Comments;
                                SFC_FabMoItemService.insert(userid, Matmodel);
                            }
                            //根据制程工序获取工序资源
                            IList<SFC_FabMoResource> OpItemResource = SFC_FabMoResourceService.GetFabMoResourceList(null, item.FabMoOperationID);
                            foreach (SFC_FabMoResource Opitem in ItemResource)
                            {
                                SFC_FabMoResource Resmodel = new SFC_FabMoResource();
                                Resmodel.FabMoResourceID = UniversalService.GetSerialNumber("SFC_FabMoResource");
                                Resmodel.FabMoOperationID = item.FabMoOperationID;
                                Resmodel.ResourceID = Opitem.ResourceID;
                                Resmodel.Type = Opitem.Type;
                                Resmodel.IfMain = Opitem.IfMain;
                                Resmodel.Status = Framework.SystemID + "0201213000001";
                                Resmodel.Comments = item.Comments;
                                SFC_FabMoResourceService.insert(userid, Resmodel);
                            }
                        }

                        IList<SFC_ProcessOperationRelationShip> OperationShip = SFC_ProcessOperationRelationShipService.GetOperationShipList(model.OriginalFabMoProcessID);
                        foreach (SFC_ProcessOperationRelationShip shipmodel in OperationShip)
                        {
                            SFC_FabMoOperation OpModel = null;
                            SFC_FabMoOperation PreOpModel = null;
                            SFC_FabMoOperationRelationship Resmodel = new SFC_FabMoOperationRelationship();
                            Resmodel.FabMoOperationRelationshipID = UniversalService.GetSerialNumber("SFC_FabMoOperationRelationship");
                            Resmodel.FabricatedMotherID = model.FabricatedMotherID;
                            Resmodel.FabMoProcessID = model.FabMoProcessID;
                            OpModel = SFC_FabMoOperationService.get(Resmodel.FabMoProcessID, shipmodel.ItemOperationID);
                            if (OpModel == null)
                                continue;
                            Resmodel.FabMoOperationID = OpModel.FabMoOperationID;

                            PreOpModel = SFC_FabMoOperationService.get(Resmodel.FabMoProcessID, shipmodel.PreItemOperationID);
                            if (PreOpModel == null)
                                continue;
                            Resmodel.PreFabMoOperationID = PreOpModel.FabMoOperationID;
                            Resmodel.IsLastOperation = shipmodel.FinishOperation;
                            Resmodel.IfMain = shipmodel.IfMain;
                            Resmodel.Status = Framework.SystemID + "0201213000001";
                            Resmodel.Comments = shipmodel.Comments;
                            SFC_FabMoOperationRelationshipService.insert(userid, Resmodel);
                        }

                        SFC_FabMoProcess Originalmodel = SFC_FabMoProcessService.get(model.OriginalFabMoProcessID);
                        if (Originalmodel != null)
                        {
                            Originalmodel.Quantity = Originalmodel.Quantity - model.Quantity;
                            Originalmodel.SeparateQuantity = Originalmodel.SeparateQuantity + model.Quantity;
                            SFC_FabMoProcessService.update(userid, Originalmodel);
                        }

                        success++;
                    }
                    else
                    {
                        failIDs = UtilBussinessService.str(failIDs, data.Value<string>("FabMoProcessID"));
                        fail++;
                    }
                }
                catch (Exception ex)
                {
                    DataLogerService.writeerrlog(ex);
                    failIDs = UtilBussinessService.str(failIDs, data.Value<string>("FabMoProcessID"));
                    msg = UtilBussinessService.str(msg, ex.ToString());
                    fail++;
                }
            }
            return new { success = success, fail = fail, failIDs = failIDs, msg = msg };
        }

        /// <summary>
        /// 拆单替代数据的更新
        /// SAM 2017年7月14日10:13:21
        /// </summary>
        /// <param name="Token"></param>
        /// <param name="jArray"></param>
        /// <returns></returns>
        public static object Sfc00002FabMoProcessSplitupdate(string Token, JArray jArray)
        {
            int success = 0;
            int fail = 0;
            string failIDs = null;
            string msg = null;
            string userid = UtilBussinessService.detoken(Token);
            JObject data = null;
            SFC_FabMoProcess model = null;
            for (int i = 0; i < jArray.Count; i++)
            {
                data = (JObject)jArray[i];
                model = SFC_FabMoProcessService.get(data.Value<string>("FabMoProcessID"));
                if (model == null)
                {
                    failIDs = UtilBussinessService.str(failIDs, data.Value<string>("FabMoProcessID"));
                    msg = UtilBussinessService.str(msg, "不存在的制程信息");
                    fail++;
                    continue;
                }

                if (SFC_FabMoProcessService.CheckSplitProcessID(data.Value<string>("ProcessID"), data.Value<string>("WorkCenterID"), data.Value<string>("OriginalFabMoProcessID"), data.Value<string>("FabMoProcessID")))
                {
                    failIDs = UtilBussinessService.str(failIDs, data.Value<string>("FabMoProcessID"));
                    msg = UtilBussinessService.str(msg, "资料重复");
                    fail++;
                    continue;
                }
                model.ProcessID = data.Value<string>("ProcessID");
                model.Sequence = data.Value<string>("Sequence");
                model.WorkCenterID = data.Value<string>("WorkCenterID");
                model.StartDate = data.Value<DateTime>("StartDate");
                model.FinishDate = data.Value<DateTime>("FinishDate");
                model.Price = data.Value<int>("Price");
                model.UnitID = data.Value<string>("UnitID");
                model.UnitRate = data.Value<Decimal>("UnitRate");
                if (!string.IsNullOrWhiteSpace(data.Value<string>("StandardTime")))
                    model.StandardTime = data.Value<int>("StandardTime");
                else
                    model.StandardTime = 0;
                if (!string.IsNullOrWhiteSpace(data.Value<string>("PrepareTime")))
                    model.PrepareTime = data.Value<int>("PrepareTime");
                else
                    model.PrepareTime = 0;
                model.Comments = data.Value<string>("Comments");
                if (SFC_FabMoProcessService.update(userid, model))
                    success++;
                else
                {
                    failIDs = UtilBussinessService.str(failIDs, data.Value<string>("FabMoProcessID"));
                    fail++;
                }
            }
            return new { success = success, fail = fail, failIDs = failIDs, msg = msg };
        }


        /// <summary>
        /// 根据制令制程获取他下面的制令制程工序列表
        /// SAM 2017年7月13日16:22:07
        /// </summary>
        /// <param name="token"></param>
        /// <param name="FabMoProcessID"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <returns></returns>
        public static object Sfc00002FabMoOperationList(string token, string FabMoProcessID, int page, int rows)
        {
            int count = 0;
            IList<Hashtable> result = SFC_FabMoOperationService.Sfc00002FabMoOperationList(FabMoProcessID, page, rows, ref count);
            return UniversalService.getPaginationModel(result, count);
        }

        /// <summary>
        /// 制令制程工序的新增
        /// SAM 2017年7月13日17:18:40
        /// </summary>
        /// <param name="Token"></param>
        /// <param name="jArray"></param>
        /// <returns></returns>
        public static object Sfc00002FabMoOperationinsert(string Token, JArray jArray)
        {
            int success = 0;
            int fail = 0;
            string failIDs = null;
            string msg = null;
            string userid = UtilBussinessService.detoken(Token);
            JObject data = null;
            SFC_FabMoOperation model = null;
            SFC_FabMoProcess FabProcess = null;
            for (int i = 0; i < jArray.Count; i++)
            {
                data = (JObject)jArray[i];
                model = new SFC_FabMoOperation();

                FabProcess = SFC_FabMoProcessService.get(data.Value<string>("FabMoProcessID"));
                if (FabProcess == null)
                {
                    failIDs = UtilBussinessService.str(failIDs, data.Value<string>("FabMoOperationID"));
                    msg = UtilBussinessService.str(msg, "不存在的制程信息");
                    fail++;
                    continue;
                }
                if (SFC_FabMoOperationService.CheckFabMoOperation(data.Value<string>("OperationID"), data.Value<string>("FabMoProcessID"), null))
                {
                    failIDs = UtilBussinessService.str(failIDs, data.Value<string>("FabMoOperationID"));
                    msg = UtilBussinessService.str(msg, "资料重复");
                    fail++;
                    continue;
                }
                model.FabMoOperationID = UniversalService.GetSerialNumber("SFC_FabMoOperation");
                model.FabricatedMotherID = FabProcess.FabricatedMotherID;
                model.FabMoProcessID = data.Value<string>("FabMoProcessID");
                model.OperationID = data.Value<string>("OperationID");
                model.Sequence = data.Value<string>("Sequence");
                model.StandardTime = string.IsNullOrWhiteSpace(data.Value<string>("StandardTime")) ? 0 : data.Value<int>("StandardTime");
                model.PrepareTime = string.IsNullOrWhiteSpace(data.Value<string>("PrepareTime")) ? 0 : data.Value<int>("PrepareTime");
                model.UnitID = data.Value<string>("UnitID");
                model.UnitRate = string.IsNullOrWhiteSpace(data.Value<string>("UnitRate")) ? 0 : data.Value<decimal>("UnitRate");
                model.ResourceReport = data.Value<bool>("ResourceReport");
                model.Status = data.Value<string>("Status");
                model.Comments = data.Value<string>("Comments");
                model.Quantity = FabProcess.Quantity;

                //计算工序的总工时，返回制程中

                if (SFC_FabMoOperationService.insert(userid, model))
                {
                    FabProcess.StandardTime = SFC_FabMoOperationService.GetStandardTimeSum(FabProcess.FabMoProcessID);
                    FabProcess.PrepareTime = SFC_FabMoOperationService.GetPrepareTimeSum(FabProcess.FabMoProcessID);
                    SFC_FabMoProcessService.update(userid, FabProcess);
                    success++;
                }
                else
                {
                    failIDs = UtilBussinessService.str(failIDs, data.Value<string>("FabMoOperationID"));
                    fail++;
                }
            }


            return new { success = success, fail = fail, failIDs = failIDs, msg = msg };
        }

        /// <summary>
        /// 制令制程工序的更新
        /// SAM 2017年7月13日17:18:20
        /// </summary>
        /// <param name="Token"></param>
        /// <param name="jArray"></param>
        /// <returns></returns>
        public static object Sfc00002FabMoOperationupdate(string Token, JArray jArray)
        {
            int success = 0;
            int fail = 0;
            string failIDs = null;
            string msg = null;
            string userid = UtilBussinessService.detoken(Token);
            JObject data = null;
            SFC_FabMoOperation model = null;
            for (int i = 0; i < jArray.Count; i++)
            {
                data = (JObject)jArray[i];
                model = SFC_FabMoOperationService.get(data.Value<string>("FabMoOperationID"));
                if (model == null)
                {
                    failIDs = UtilBussinessService.str(failIDs, data.Value<string>("FabMoOperationID"));
                    msg = UtilBussinessService.str(msg, "不存在的制程工序信息");
                    fail++;
                    continue;
                }
                if (SFC_FabMoOperationService.CheckFabMoOperation(data.Value<string>("OperationID"), data.Value<string>("FabMoProcessID"), data.Value<string>("FabMoOperationID")))
                {
                    failIDs = UtilBussinessService.str(failIDs, data.Value<string>("FabMoOperationID"));
                    msg = UtilBussinessService.str(msg, "资料重复");
                    fail++;
                    continue;
                }
                model.Sequence = data.Value<string>("Sequence");
                model.StandardTime = string.IsNullOrWhiteSpace(data.Value<string>("StandardTime")) ? 0 : data.Value<int>("StandardTime");
                model.PrepareTime = string.IsNullOrWhiteSpace(data.Value<string>("PrepareTime")) ? 0 : data.Value<int>("PrepareTime");
                model.UnitID = data.Value<string>("UnitID");
                model.UnitRate = string.IsNullOrWhiteSpace(data.Value<string>("UnitRate")) ? 0 : data.Value<Decimal>("UnitRate");
                model.ResourceReport = data.Value<bool>("ResourceReport");
                model.Status = data.Value<string>("Status");
                model.Comments = data.Value<string>("Comments");
                //计算工序的总工时，返回制程中

                if (SFC_FabMoOperationService.update(userid, model))
                {
                    SFC_FabMoProcess FabProcess = SFC_FabMoProcessService.get(model.FabMoProcessID);
                    FabProcess.StandardTime = SFC_FabMoOperationService.GetStandardTimeSum(model.FabMoProcessID);
                    FabProcess.PrepareTime = SFC_FabMoOperationService.GetPrepareTimeSum(model.FabMoProcessID);
                    SFC_FabMoProcessService.update(userid, FabProcess);
                    success++;
                }
                else
                {
                    failIDs = UtilBussinessService.str(failIDs, data.Value<string>("FabMoOperationID"));
                    fail++;
                }
            }
            return new { success = success, fail = fail, failIDs = failIDs, msg = msg };
        }

        /// <summary>
        /// 制令制程工序的删除
        /// SAM 2017年7月13日17:30:54
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static object Sfc00002FabMoOperationDelete(JObject data)
        {
            string Token = data.Value<string>("Token");
            string userid = UtilBussinessService.detoken(Token);

            SFC_FabMoOperation model = null;
            model = SFC_FabMoOperationService.get(data.Value<string>("FabMoOperationID"));
            if (model == null)
                return new { status = "410", msg = "流水号错误，不存在的流水号！" };

            model.Status = Framework.SystemID + "0201213000003";
            if (SFC_FabMoOperationService.update(userid, model))
                return new { status = "200", msg = "删除成功！" };
            else
                return new { status = "410", msg = "删除失败！" };
        }

        /// <summary>
        /// 获取制程的工序关系列表（分页）
        /// SAM 2017年7月13日17:37:43
        /// </summary>
        /// <param name="token"></param>
        /// <param name="FabricatedMotherID"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <returns></returns>
        public static object Sfc00002GetFabMoOperationRelShipList(string token, string FabricatedMotherID, int page, int rows)
        {
            int count = 0;
            IList<Hashtable> result = SFC_FabMoOperationRelationshipService.Sfc00002GetFabMoOperationRelShipList(FabricatedMotherID, page, rows, ref count);
            return UtilBussinessService.getPaginationModel(result, count);
        }

        /// <summary>
        /// 新增制令制程工序关系
        /// SAM 2017年7月13日17:37:43
        /// </summary>
        /// <param name="Token"></param>
        /// <param name="jArray"></param>
        /// <param name="Type"></param>
        /// <returns></returns>
        public static object Sfc00002FabMoOperationRelShipinsert(string Token, JArray jArray)
        {
            int success = 0;
            int fail = 0;
            string failIDs = null;
            string msg = null;
            string userid = UtilBussinessService.detoken(Token);
            JObject data = null;
            SFC_FabMoOperationRelationship model = null;
            for (int i = 0; i < jArray.Count; i++)
            {
                data = (JObject)jArray[i];

                model = new SFC_FabMoOperationRelationship();
                model.FabMoOperationRelationshipID = UniversalService.GetSerialNumber("SFC_FabMoOperationRelationship");
                model.FabricatedMotherID = data.Value<string>("FabricatedMotherID");
                model.FabMoProcessID = data.Value<string>("FabMoProcessID");
                model.FabMoOperationID = data.Value<string>("FabMoOperationID");
                model.PreFabMoOperationID = data.Value<string>("PreFabMoOperationID");
                model.IsLastOperation = data.Value<bool>("IsLastOperation");
                model.IfMain = data.Value<bool>("IfMain");
                model.Comments = data.Value<string>("Comments");
                model.Status = Framework.SystemID + "0201213000001";


                if (SFC_FabMoOperationRelationshipService.insert(userid, model))
                    success++;
                else
                {
                    failIDs = UtilBussinessService.str(failIDs, data.Value<string>("FabMoOperationRelationshipID"));
                    fail++;
                }
            }
            SFC_FabMoOperationRelationshipService.SetFinishOperation(userid, model.FabMoProcessID);
            return new { success = success, fail = fail, failIDs = failIDs, msg = msg };

        }


        /// <summary>
        /// 更新制令制程工序关系
        /// SAN 2017年7月13日17:37:43
        /// </summary>
        /// <param name="Token"></param>
        /// <param name="jArray"></param>
        /// <returns></returns>
        public static object Sfc00002FabMoOperationRelShipupdate(string Token, JArray jArray)
        {
            int success = 0;
            int fail = 0;
            string failIDs = null;
            string msg = null;
            string userid = UtilBussinessService.detoken(Token);
            JObject data = null;
            SFC_FabMoOperationRelationship model = null;
            for (int i = 0; i < jArray.Count; i++)
            {
                data = (JObject)jArray[i];

                model = SFC_FabMoOperationRelationshipService.get(data.Value<string>("FabMoOperationRelationshipID"));
                model.FabMoOperationID = data.Value<string>("FabMoOperationID");
                model.PreFabMoOperationID = data.Value<string>("PreFabMoOperationID");
                model.IfMain = data.Value<bool>("IfMain");
                model.Comments = data.Value<string>("Comments");
                if (SFC_FabMoOperationRelationshipService.update(userid, model))
                    success++;
                else
                {
                    failIDs = UtilBussinessService.str(failIDs, data.Value<string>("FabMoOperationRelationshipID"));
                    fail++;
                }
            }
            SFC_FabMoOperationRelationshipService.SetFinishOperation(userid, model.FabMoProcessID);
            return new { success = success, fail = fail, failIDs = failIDs, msg = msg };

        }

        /// <summary>
        /// 单个保存制令制程工序关系
        /// SAM 2017年7月13日17:37:43
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static object FabMoOperationRelShipSave(JObject data)
        {
            string Token = data.Value<string>("Token");
            string userid = UtilBussinessService.detoken(Token);

            SFC_FabMoOperationRelationship model = null;
            model = SFC_FabMoOperationRelationshipService.get(data.Value<string>("FabMoOperationRelationshipID"));
            if (model == null)
                return new { status = "410", msg = "流水号错误，不存在的流水号！" };

            model.FabMoOperationID = data.Value<string>("FabMoOperationID");
            model.PreFabMoOperationID = data.Value<string>("PreFabMoOperationID");
            model.IfMain = data.Value<bool>("IfMain");
            model.Comments = data.Value<string>("Comments");

            if (SFC_FabMoOperationRelationshipService.update(userid, model))
            {
                SFC_FabMoOperationRelationshipService.SetFinishOperation(userid, model.FabMoProcessID);
                return new { status = "200", msg = "更新成功！" };
            }
            else
                return new { status = "410", msg = "更新失败！" };
        }


        /// <summary>
        /// 获取制程的工序关系列表(不分页)
        /// SAM 2017年7月13日17:37:43
        /// </summary>
        /// <param name="token"></param>
        /// <param name="FabricatedMotherID"></param>
        /// <returns></returns>
        public static IList<Hashtable> Sfc00002GetFabMoOperationRelShipNoPage(string token, string FabricatedMotherID)
        {
            IList<Hashtable> result = SFC_FabMoOperationRelationshipService.Sfc00002GetFabMoOperationRelShipNoPage(FabricatedMotherID);
            return result;
        }

        /// <summary>
        /// 制令制程工序关系删除
        /// SAM 2017年7月14日15:50:19
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public static object FabMoOperationRelShipDelete(JObject request)
        {
            try
            {
                string Token = request.Value<string>("Token");
                string userID = UniversalService.detoken(Token);

                SFC_FabMoOperationRelationship model = SFC_FabMoOperationRelationshipService.get(request.Value<string>("FabMoOperationRelationshipID"));
                if (model == null)
                    return new { status = "410", msg = "不存在的信息！" };

                model.Status = Framework.SystemID + "0201213000003";

                if (SFC_FabMoOperationRelationshipService.update(userID, model))
                {
                    SFC_FabMoOperationRelationshipService.SetFinishOperation(userID, model.FabMoProcessID);
                    return new { status = "200", msg = "删除成功" };
                }
                else
                    return new { status = "200", msg = "删除成功" };
            }
            catch (Exception ex)
            {
                DataLogerService.writeerrlog(ex);
                return new { status = "400", msg = ex.Message };
            }
        }

        /// <summary>
        /// 获取工序的用料列表
        /// SAM 2017年7月13日17:51:21
        /// </summary>
        /// <param name="token"></param>
        /// <param name="FabMoProcessID"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <returns></returns>
        public static object Sfc00002GetFabMoOperationItem(string token, string FabMoProcessID, int page, int rows)
        {
            int count = 0;
            return UniversalService.getPaginationModel(
                SFC_FabMoItemService.GetOperationItemList(FabMoProcessID, page, rows, ref count),
                count);
        }

        /// <summary>
        /// 新增工序用料
        /// SAM 2017年7月13日20:19:31
        /// </summary>
        /// <param name="Token"></param>
        /// <param name="jArray"></param>
        /// <param name="Type"></param>
        /// <returns></returns>
        public static object Sfc00002FabMoOperationIteminsert(string Token, JArray jArray)
        {
            int success = 0;
            int fail = 0;
            string failIDs = null;
            string msg = null;
            string userid = UtilBussinessService.detoken(Token);
            JObject data = null;
            SFC_FabMoItem model = null;
            for (int i = 0; i < jArray.Count; i++)
            {
                data = (JObject)jArray[i];
                if (SFC_FabMoItemService.CheckOperation(data.Value<string>("ItemID"), data.Value<string>("FabMoOperationID"), null))
                {
                    failIDs = UtilBussinessService.str(failIDs, data.Value<string>("FabMoItemID"));
                    msg = UtilBussinessService.str(msg, "资料重复");
                    fail++;
                    continue;
                }

                model = new SFC_FabMoItem();
                model.FabMoItemID = UniversalService.GetSerialNumber("SFC_FabMoItem");
                model.FabMoOperationID = data.Value<string>("FabMoOperationID");
                model.Sequence = data.Value<string>("Sequence");
                model.ItemID = data.Value<string>("ItemID");
                model.BaseQuantity = data.Value<decimal>("BaseQuantity");
                model.UseQuantity = data.Value<decimal>("UseQuantity");
                model.AttritionRate = data.Value<decimal>("AttritionRate");
                model.Crityn = data.Value<bool>("Crityn");
                model.Comments = data.Value<string>("Comments");
                model.Status = Framework.SystemID + "0201213000001";
                if (SFC_FabMoItemService.insert(userid, model))
                    success++;
                else
                {
                    failIDs = UtilBussinessService.str(failIDs, data.Value<string>("FabMoItemID"));
                    fail++;
                }
            }
            return new { success = success, fail = fail, failIDs = failIDs, msg = msg };
        }


        /// <summary>
        /// 更新工序用料
        /// SAN 2017年7月13日17:37:43
        /// </summary>
        /// <param name="Token"></param>
        /// <param name="jArray"></param>
        /// <returns></returns>
        public static object Sfc00002FabMoOperationItemupdate(string Token, JArray jArray)
        {
            int success = 0;
            int fail = 0;
            string failIDs = null;
            string msg = null;
            string userid = UtilBussinessService.detoken(Token);
            JObject data = null;
            SFC_FabMoItem model = null;
            for (int i = 0; i < jArray.Count; i++)
            {
                data = (JObject)jArray[i];

                if (SFC_FabMoItemService.CheckOperation(data.Value<string>("ItemID"), data.Value<string>("FabMoOperationID"), data.Value<string>("FabMoItemID")))
                {
                    failIDs = UtilBussinessService.str(failIDs, data.Value<string>("FabMoItemID"));
                    msg = UtilBussinessService.str(msg, "资料重复");
                    fail++;
                    continue;
                }
                model = SFC_FabMoItemService.get(data.Value<string>("FabMoItemID"));
                model.Sequence = data.Value<string>("Sequence");
                model.ItemID = data.Value<string>("ItemID");
                model.BaseQuantity = data.Value<decimal>("BaseQuantity");
                model.UseQuantity = data.Value<decimal>("UseQuantity");
                model.AttritionRate = data.Value<decimal>("AttritionRate");
                model.Crityn = data.Value<bool>("Crityn");
                model.Comments = data.Value<string>("Comments");
                if (SFC_FabMoItemService.update(userid, model))
                    success++;
                else
                {
                    failIDs = UtilBussinessService.str(failIDs, data.Value<string>("FabMoItemID"));
                    fail++;
                }
            }

            return new { success = success, fail = fail, failIDs = failIDs, msg = msg };
        }


        /// <summary>
        /// 工序用料的删除
        /// SAM 2017年7月13日20:28:25
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static object Sfc00002FabMoOperationItemDelete(JObject data)
        {
            string Token = data.Value<string>("Token");
            string userid = UtilBussinessService.detoken(Token);

            SFC_FabMoItem model = null;
            model = SFC_FabMoItemService.get(data.Value<string>("FabMoItemID"));
            if (model == null)
                return new { status = "410", msg = "流水号错误，不存在的流水号！" };

            model.Status = Framework.SystemID + "0201213000003";
            if (SFC_FabMoItemService.update(userid, model))
                return new { status = "200", msg = "删除成功！" };
            else
                return new { status = "410", msg = "删除失败！" };
        }


        /// <summary>
        ///  制令制程工序资源
        ///  SAM 2017年7月13日20:38:10
        /// </summary>
        /// <param name="token"></param>
        /// <param name="Type"></param>
        /// <param name="FabMoOperationID"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <returns></returns>
        public static object Sfc00002GetFabricatedOperationResource(string token, string Type, string FabMoOperationID, int page, int rows)
        {
            int count = 0;
            IList<Hashtable> result = SFC_FabMoResourceService.Sfc00002GetFabricatedOperationResource(Type, FabMoOperationID, page, rows, ref count);
            return UniversalService.getPaginationModel(result, count);
        }


        /// <summary>
        /// 判断制程工序关系是否存在错误
        /// SAM 2017年9月10日23:23:04
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static object CheckFabMoOpRelationShip(JObject data)
        {
            string Token = data.Value<string>("Token");
            string userid = UtilBussinessService.detoken(Token);
            string FabMoProcessID = data.Value<string>("FabMoProcessID");

            SFC_FabMoProcess ProModel = SFC_FabMoProcessService.get(FabMoProcessID);
            if (ProModel == null)
                return new { status = "410", msg = "不存在的制令制程信息！" };

            if (SFC_FabMoOperationRelationshipService.CheckOpRelationShip(FabMoProcessID))
                return new { status = "410", msg = "存在没有关系的工序！" };

            //if (SFC_ItemProcessRelationShipService.CheckFinishProcess(ItemID))
            //   return new { status = "410", msg = "已存在最终制程！" };

            return new { status = "200", msg = "检查没问题！" };
        }

        /// <summary>
        /// 判断制令制程关系是否存在错误
        /// SAM 2017年9月10日23:27:45
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static object CheckFabMoProRelationShip(JObject data)
        {
            string Token = data.Value<string>("Token");
            string userid = UtilBussinessService.detoken(Token);
            string FabricatedMotherID = data.Value<string>("FabricatedMotherID");

            SFC_FabricatedMother FabMoModel = SFC_FabricatedMotherService.get(FabricatedMotherID);
            if (FabMoModel == null)
                return new { status = "410", msg = "不存在的制令单信息！" };

            if (SFC_FabMoRelationshipService.CheckProRelationShip(FabricatedMotherID))
                return new { status = "410", msg = "存在没有关系的制程！" };

            //if (SFC_ItemProcessRelationShipService.CheckFinishProcess(ItemID))
            //    return new { status = "410", msg = "已存在最终制程！" };

            return new { status = "200", msg = "检查没问题！" };
        }



        #endregion

        #region SFC00003制令单拆单作业
        ///<summary>
        ///制令单列表
        ///Joint 2017年6月22日17:32:38
        ///</summary>
        ///<param name="Token">授权码</param>
        ///<param name="MoNo">制令单号</param>
        ///<param name="page">页码</param>
        ///<param name"rows">行数</param>

        public static object Sfc00003GetList(string Token, string StartItemCode, string EndItemCode, string StartMoNo, string EndMoNo, int page, int rows)
        {
            int count = 0;
            return UniversalService.getPaginationModel(SFC_FabricatedMotherService.Sfc00003GetList(StartItemCode, EndItemCode, StartMoNo, EndMoNo, page, rows, ref count), count);
        }

        ///<summary>
        ///制令单拆单
        ///Joint 
        ///</summary>
        ///<param name="Token"></param>
        ///<param name="FabricatedMotherID"></param>
        ///<param name="jArray"></param>
        ///<returns></returns>

        public static object Sfc00003DetailSave(string Token, string FabricatedMotherID, JArray jArray)
        {
            int success = 0;
            int fail = 0;
            string failIDs = null;
            string msg = null;
            string userid = UtilBussinessService.detoken(Token);
            JObject data = null;
            SFC_FabricatedMother ModelMother = null;
            SFC_FabMoOperation ModelOperation = null;
            SFC_FabMoItem ModelItem = null;
            SFC_FabMoResource ModelResource = null;
            SFC_FabMoProcess ModelProcess = null;
            //获取制令单在製令母件檔、製令製程檔、製令製程關係檔、製令工序檔、製令工序關係檔、製令用料檔、製令資源檔中的数据
            //制令单        
            SFC_FabricatedMother FabricatedMother = SFC_FabricatedMotherService.get(FabricatedMotherID);
            SFC_FabricatedMother fabricatedmother = SFC_FabricatedMotherService.GetMaxSplitSequence(FabricatedMother.MoNo);

            //制令制程
            List<SFC_FabMoProcess> FabMoProcess = SFC_FabMoProcessService.GetFabMoProcessList(FabricatedMotherID);
            ////串联所有相关的制令制程的流水号
            //String FPID = String.Join(",", FabMoProcess.Select(a => a.FabMoProcessID).ToArray<string>());

            for (int i = 0; i < jArray.Count; i++)
            {
                data = (JObject)jArray[i];
                //制令单插入
                //decimal SplitSequence = Convert.ToDecimal(fabricatedmother.SplitSequence) + i + 1;
                /*
                 * SAM 2017年9月26日15:29:54
                 * 拆分序号从字符串改成了int类型
                 */
                int SplitSequence = fabricatedmother.SplitSequence + i + 1;
                ModelMother = new SFC_FabricatedMother();
                ModelMother.FabricatedMotherID = UniversalService.GetSerialNumber("SFC_FabricatedMother");
                ModelMother.MoNo = FabricatedMother.MoNo;
                ModelMother.Date = FabricatedMother.Date;
                ModelMother.Version = FabricatedMother.Version;
                ModelMother.SplitSequence = SplitSequence;
                ModelMother.BatchNumber = FabricatedMother.BatchNumber;
                ModelMother.ItemID = FabricatedMother.ItemID;
                ModelMother.Quantity = data.Value<decimal>("Quantity");
                ModelMother.UnitID = FabricatedMother.UnitID;
                ModelMother.StartDate = data.Value<DateTime>("StartDate");
                ModelMother.FinishDate = data.Value<DateTime>("FinishDate");
                ModelMother.OrderNo = FabricatedMother.OrderNo;
                ModelMother.OrderQuantity = FabricatedMother.OrderQuantity;
                ModelMother.CustomerID = FabricatedMother.CustomerID;
                ModelMother.MESUserID = FabricatedMother.MESUserID;
                ModelMother.ShipmentDate = FabricatedMother.ShipmentDate;
                ModelMother.OrganizationID = FabricatedMother.OrganizationID;
                ModelMother.Status = FabricatedMother.Status;
                ModelMother.StorageQuantity = FabricatedMother.StorageQuantity;
                ModelMother.OverRate = FabricatedMother.OverRate;
                ModelMother.Source = FabricatedMother.Source;
                ModelMother.OriginalFabricatedMotherID = FabricatedMother.FabricatedMotherID;
                ModelMother.RoutID = FabricatedMother.RoutID;
                ModelMother.SeparateQuantity = 0;

                if (SFC_FabricatedMotherService.insert(userid, ModelMother))
                {
                    //指令单添加成功后
                    //添加该制令单对应的制令制程
                    foreach (SFC_FabMoProcess Process in FabMoProcess)
                    {
                        ModelProcess = new SFC_FabMoProcess();
                        ModelProcess.FabMoProcessID = UniversalService.GetSerialNumber("SFC_FabMoProcess");
                        ModelProcess.FabricatedMotherID = ModelMother.FabricatedMotherID;
                        ModelProcess.ProcessID = Process.ProcessID;
                        ModelProcess.Sequence = Process.Sequence;
                        ModelProcess.WorkCenterID = Process.WorkCenterID;
                        ModelProcess.StartDate = data.Value<DateTime>("StartDate");
                        ModelProcess.FinishDate = data.Value<DateTime>("FinishDate");
                        ModelProcess.Quantity = data.Value<decimal>("Quantity");
                        ModelProcess.SourceID = Process.FabMoProcessID;
                        ModelProcess.OrderQuantity = Process.OrderQuantity;
                        ModelProcess.SeparateQuantity = Process.SeparateQuantity;
                        ModelProcess.FinishDate = Process.FinishDate;
                        ModelProcess.OutProQuantity = Process.OutProQuantity;
                        ModelProcess.ScrappedQuantity = Process.ScrappedQuantity;
                        ModelProcess.DifferenceQuantity = Process.DifferenceQuantity;
                        ModelProcess.PreProQuantity = Process.PreProQuantity;
                        ModelProcess.AssignQuantity = Process.AssignQuantity;
                        ModelProcess.UnitID = Process.UnitID;
                        ModelProcess.UnitRate = Process.UnitRate;
                        ModelProcess.StandardTime = (data.Value<int>("Quantity") * Process.StandardTime / Convert.ToInt32(Process.Quantity));
                        ModelProcess.PrepareTime = (data.Value<int>("Quantity") * Process.PrepareTime / Convert.ToInt32(Process.Quantity));
                        ModelProcess.Status = Process.Status;
                        ModelProcess.OriginalFabMoProcessID = Process.OriginalFabMoProcessID;
                        ModelProcess.IsEnableOperation = Process.IsEnableOperation;
                        ModelProcess.Price = Process.Price;
                        if (SFC_FabMoProcessService.insert(userid, ModelProcess))
                        {
                            //制令制程添加成功后
                            //添加该制程对应的制令制程工序、用料、资源

                            List<SFC_FabMoOperation> FabMoOperation = SFC_FabMoOperationService.GetFabMoOperationList(Process.FabMoProcessID);

                            //串联所有相关的制令制程工序的流水号
                            //String FOID = String.Join(",", FabMoOperation.Select(b => b.FabMoOperationID).ToArray<string>());

                            ModelOperation = new SFC_FabMoOperation();
                            //工序
                            foreach (SFC_FabMoOperation Operation in FabMoOperation)
                            {
                                ModelOperation.FabMoOperationID = UniversalService.GetSerialNumber("SFC_FabMoOperation");
                                ModelOperation.FabricatedMotherID = ModelMother.FabricatedMotherID;
                                ModelOperation.FabMoProcessID = ModelProcess.FabMoProcessID;
                                ModelOperation.OperationID = Operation.OperationID;
                                ModelOperation.UnitID = Operation.UnitID;
                                ModelOperation.UnitRate = Operation.UnitRate;
                                ModelOperation.Sequence = Operation.Sequence;
                                ModelOperation.SourceID = Operation.FabMoOperationID;
                                ModelOperation.StartDate = data.Value<DateTime>("StartDate");
                                ModelOperation.EndDate = data.Value<DateTime>("FinishDate");
                                ModelOperation.Quantity = data.Value<decimal>("Quantity");
                                ModelOperation.ResourceReport = Operation.ResourceReport;
                                ModelOperation.StandardTime = (data.Value<int>("Quantity") * Operation.StandardTime / Convert.ToInt32(Operation.Quantity));
                                ModelOperation.PrepareTime = (data.Value<int>("Quantity") * Operation.PrepareTime / Convert.ToInt32(Operation.Quantity));
                                ModelOperation.TaskNo = Operation.TaskNo;
                                ModelOperation.Status = Operation.Status;
                                if (SFC_FabMoOperationService.insert(userid, ModelOperation))
                                {
                                    //工序用料
                                    //获取所有相关制令制程工序的用料
                                    List<SFC_FabMoItem> FabMoItemOper = SFC_FabMoItemService.GetFabMoItemList(null, Operation.FabMoOperationID);
                                    ModelItem = new SFC_FabMoItem();
                                    foreach (SFC_FabMoItem OperItem in FabMoItemOper)
                                    {
                                        ModelItem.FabMoItemID = UniversalService.GetSerialNumber("SFC_FabMoItem");
                                        ModelItem.FabMoOperationID = ModelOperation.FabMoOperationID;
                                        ModelItem.Sequence = OperItem.Sequence;
                                        ModelItem.ItemID = OperItem.ItemID;
                                        ModelItem.BaseQuantity = OperItem.BaseQuantity;
                                        ModelItem.AttritionRate = OperItem.AttritionRate;
                                        ModelItem.Status = OperItem.Status;

                                        SFC_FabMoItemService.insert(userid, ModelItem);
                                    }
                                    //工序资源
                                    //获取所有相关制令制程工序的资源
                                    List<SFC_FabMoResource> FabMoResourceOper = SFC_FabMoResourceService.GetFabMoResourceList(null, Operation.FabMoOperationID);
                                    ModelResource = new SFC_FabMoResource();
                                    foreach (SFC_FabMoResource OperRosource in FabMoResourceOper)
                                    {
                                        ModelResource.FabMoResourceID = UniversalService.GetSerialNumber("SFC_FabMoResource");
                                        ModelResource.FabMoOperationID = ModelOperation.FabMoOperationID;
                                        ModelResource.ResourceID = OperRosource.ResourceID;
                                        ModelResource.Type = OperRosource.Type;
                                        ModelResource.IfMain = OperRosource.IfMain;
                                        ModelResource.Status = OperRosource.Status;
                                        SFC_FabMoResourceService.insert(userid, ModelResource);
                                    }
                                }

                                else
                                {
                                    return new { status = "400", msg = "添加失败" };
                                }
                            }
                            //获取所有相关制令制程的的用料和资源
                            List<SFC_FabMoItem> FabMoItem = SFC_FabMoItemService.GetFabMoItemList(Process.FabMoProcessID, null);
                            List<SFC_FabMoResource> FabMoResource = SFC_FabMoResourceService.GetFabMoResourceList(Process.FabMoProcessID, null);
                            //制程用料
                            ModelItem = new SFC_FabMoItem();
                            foreach (SFC_FabMoItem ProItem in FabMoItem)
                            {
                                ModelItem.FabMoItemID = UniversalService.GetSerialNumber("SFC_FabMoItem");
                                ModelItem.FabMoProcessID = ModelProcess.FabMoProcessID;
                                ModelItem.Sequence = ProItem.Sequence;
                                ModelItem.ItemID = ProItem.ItemID;
                                ModelItem.BaseQuantity = ProItem.BaseQuantity;
                                ModelItem.AttritionRate = ProItem.AttritionRate;
                                ModelItem.Status = ProItem.Status;

                                SFC_FabMoItemService.insert(userid, ModelItem);
                            }
                            //制程资源
                            ModelResource = new SFC_FabMoResource();
                            foreach (SFC_FabMoResource ProRosource in FabMoResource)
                            {
                                ModelResource.FabMoResourceID = UniversalService.GetSerialNumber("SFC_FabMoResource");
                                ModelResource.FabMoProcessID = ModelProcess.FabMoProcessID;
                                ModelResource.ResourceID = ProRosource.ResourceID;
                                ModelResource.Type = ProRosource.Type;
                                ModelResource.IfMain = ProRosource.IfMain;
                                ModelResource.Status = ProRosource.Status;
                                SFC_FabMoResourceService.insert(userid, ModelResource);
                            }


                        }
                        else
                        {
                            return new { status = "400", msg = "添加失败" };
                        }

                    }
                }
                else
                {
                    return new { status = "400", msg = "添加失败" };
                }
                //添加该制令单对应的制令制程关系
                //制令制程关系
                List<SFC_FabMoRelationship> FabMoProcessRelationship = SFC_FabMoRelationshipService.GetFabMoProcessRelationshipList(FabricatedMotherID);
                SFC_FabMoRelationship ModelProcessRelationship = new SFC_FabMoRelationship();

                foreach (SFC_FabMoRelationship Relationship in FabMoProcessRelationship)
                {
                    SFC_FabMoProcess newID = SFC_FabMoProcessService.get(ModelMother.FabricatedMotherID, Relationship.FabMoProcessID);
                    SFC_FabMoProcess newPreID = SFC_FabMoProcessService.get(ModelMother.FabricatedMotherID, Relationship.PreFabMoProcessID);

                    ModelProcessRelationship.FabMoRelationshipID = UniversalService.GetSerialNumber("SFC_FabMoRelationship");
                    ModelProcessRelationship.FabricatedMotherID = ModelMother.FabricatedMotherID;
                    ModelProcessRelationship.FabMoProcessID = newID.FabMoProcessID;
                    ModelProcessRelationship.PreFabMoProcessID = newPreID.FabMoProcessID;
                    ModelProcessRelationship.IfLastProcess = Relationship.IfLastProcess;
                    ModelProcessRelationship.IfMain = Relationship.IfMain;
                    ModelProcessRelationship.Status = Relationship.Status;
                    SFC_FabMoRelationshipService.insert(userid, ModelProcessRelationship);

                    //制令制程工序关系
                    List<SFC_FabMoOperationRelationship> FabMoOperationRelatonship = SFC_FabMoOperationRelationshipService.GetFabMoOperationRelationShipList(Relationship.FabMoProcessID);
                    SFC_FabMoOperationRelationship ModelOperationRelationship = new SFC_FabMoOperationRelationship();
                    foreach (SFC_FabMoOperationRelationship OperRelationship in FabMoOperationRelatonship)
                    {
                        SFC_FabMoOperation newid = SFC_FabMoOperationService.get(OperRelationship.FabMoProcessID, OperRelationship.FabMoOperationID);
                        SFC_FabMoOperation newPreid = SFC_FabMoOperationService.get(OperRelationship.FabMoProcessID, OperRelationship.PreFabMoOperationID);
                        ModelOperationRelationship.FabMoOperationRelationshipID = UniversalService.GetSerialNumber("SFC_FabMoRelationship");
                        ModelOperationRelationship.FabricatedMotherID = ModelMother.FabricatedMotherID;
                        ModelOperationRelationship.FabMoOperationID = newid.FabMoOperationID;
                        ModelOperationRelationship.PreFabMoOperationID = newPreid.FabMoOperationID;
                        ModelOperationRelationship.IfMain = OperRelationship.IfMain;
                        ModelOperationRelationship.IsLastOperation = OperRelationship.IsLastOperation;
                        ModelOperationRelationship.Status = OperRelationship.Status;
                        SFC_FabMoOperationRelationshipService.insert(userid, ModelOperationRelationship);
                    }

                }
                //原制令的修改
                FabricatedMother.Quantity = FabricatedMother.Quantity - ModelMother.Quantity;
                FabricatedMother.SeparateQuantity += ModelMother.Quantity;
                SFC_FabricatedMother Model = null;
                Model = new SFC_FabricatedMother();
                Model.FabricatedMotherID = FabricatedMotherID;
                Model.SplitSequence = 0;
                Model.MoNo = FabricatedMother.MoNo;
                Model.Date = FabricatedMother.Date;
                Model.Version = FabricatedMother.Version;
                Model.BatchNumber = FabricatedMother.BatchNumber;
                Model.ItemID = FabricatedMother.ItemID;
                Model.Quantity = FabricatedMother.Quantity;
                Model.UnitID = FabricatedMother.UnitID;
                Model.StartDate = FabricatedMother.StartDate;
                Model.FinishDate = FabricatedMother.FinishDate;
                Model.OrderNo = FabricatedMother.OrderNo;
                Model.OrderQuantity = FabricatedMother.OrderQuantity;
                Model.CustomerID = FabricatedMother.CustomerID;
                Model.MESUserID = FabricatedMother.MESUserID;
                Model.ShipmentDate = FabricatedMother.ShipmentDate;
                Model.OrganizationID = FabricatedMother.OrganizationID;
                Model.Status = FabricatedMother.Status;
                Model.StorageQuantity = FabricatedMother.StorageQuantity;
                Model.SeparateQuantity = FabricatedMother.SeparateQuantity;
                Model.OverRate = FabricatedMother.OverRate;
                Model.Source = FabricatedMother.Source;
                Model.RoutID = FabricatedMother.RoutID;

                if (Model.Quantity <= 0)
                {
                    Model.Status = Framework.SystemID + "020121300002A";
                }
                else
                {
                    Model.Status = Framework.SystemID + "0201213000028";
                }

                if (SFC_FabricatedMotherService.update(userid, Model))
                {
                    //如果制令单结案
                    if (Model.Status == Framework.SystemID + "020121300002A")
                    {
                        //同步结案制程和工序
                        SFC_FabMoProcessService.updateByFabMo(userid, Model.FabricatedMotherID, Model.Status);
                        SFC_FabMoOperationService.updateByFabMo(userid, Model.FabricatedMotherID, Model.Status);
                    }
                }
            }

            return new { success = success, fail = fail, failIDs = failIDs, msg = msg };
        }


        #endregion

        #region SFC00004任务单分派与维护
        /// <summary>
        /// 任务单分派与维护主列表
        /// SAM 2017年6月26日18:01:33
        /// </summary>
        /// <param name="token"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <returns></returns>
        public static object Sfc00004GetList(string token, string StartWorkCenterCode, string EndWorkCenterCode,
           string StartProcessCode, string EndProcessCode,
           string StartFabMoCode, string EndFabMoCode,
           string StartDate, string EndDate, int page, int rows)
        {
            int count = 0;
            return UtilBussinessService.getPaginationModel(SFC_FabricatedMotherService.Sfc00004GetList(page, StartWorkCenterCode, EndWorkCenterCode,
            StartProcessCode, EndProcessCode, StartFabMoCode, EndFabMoCode, StartDate, EndDate, rows, ref count), count);
        }

        /// <summary>
        /// 任务单分派列表
        /// SAM 2017年6月29日16:56:14
        /// </summary>
        /// <param name="token"></param>
        /// <param name="ListID"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <returns></returns>
        public static object Sfc00004GetDispatchList(string token, string FabMoOperationID, string FabMoProcessID, int page, int rows)
        {
            if (string.IsNullOrWhiteSpace(FabMoOperationID) && string.IsNullOrWhiteSpace(FabMoProcessID))
                return null;
            int count = 0;
            if (!string.IsNullOrWhiteSpace(FabMoOperationID))
                return UtilBussinessService.getPaginationModel(SFC_TaskDispatchService.Sfc00004GetODispatchList(FabMoOperationID, page, rows, ref count), count);
            else
                return UtilBussinessService.getPaginationModel(SFC_TaskDispatchService.Sfc00004GetDispatchList(FabMoProcessID, page, rows, ref count), count);
        }

        /// <summary>
        /// 任务卡分派新增
        /// SAM 2017年7月3日09:57:19
        /// </summary>
        /// <param name="Token"></param>
        /// <param name="jArray"></param>
        /// <returns></returns>
        public static object Sfc00004insert(string Token, JArray jArray)
        {
            int success = 0;
            int fail = 0;
            string failIDs = null;
            string msg = null;
            string userid = UtilBussinessService.detoken(Token);
            JObject data = null;
            SFC_TaskDispatch model = null;
            SFC_FabricatedMother FabMoModel = null;
            SFC_FabMoProcess FPromodel = null;
            SFC_FabMoOperation FOpemodel = null;
            SFC_ItemProcess Promodel = null;
            SFC_ItemOperation Opemodel = null;
            for (int i = 0; i < jArray.Count; i++)
            {
                data = (JObject)jArray[i];
                model = new SFC_TaskDispatch();
                model.TaskDispatchID = UniversalService.GetSerialNumber("SFC_TaskDispatch");
                //制令单
                FabMoModel = SFC_FabricatedMotherService.get(data.Value<string>("FabricatedMotherID"));
                if (FabMoModel == null)
                {
                    failIDs = UtilBussinessService.str(failIDs, data.Value<string>("TaskDispatchID"));
                    msg = UtilBussinessService.str(msg, "制令单流水号错误，不存在的制令单信息");
                    fail++;
                    continue;
                }
                //制令制程
                FPromodel = SFC_FabMoProcessService.get(data.Value<string>("FabMoProcessID"));
                if (FPromodel == null)
                {
                    failIDs = UtilBussinessService.str(failIDs, data.Value<string>("TaskDispatchID"));
                    msg = UtilBussinessService.str(msg, "制令制程流水号错误，不存在的制令制程信息");
                    fail++;
                    continue;
                }
                if (string.IsNullOrWhiteSpace(data.Value<string>("FabMoOperationID")))
                {
                    Promodel = SFC_ItemProcessService.getByItemProcess(FabMoModel.ItemID, FPromodel.ProcessID);
                    if (Promodel != null)
                    {
                        model.IsFPI = Promodel.IsFPI;
                        model.IsIP = Promodel.IsIP;
                        model.IsOSI = Promodel.IsOSI;
                        model.InspectionGroupID = Promodel.InspectionGroupID;
                    }
                }
                else
                {
                    FOpemodel = SFC_FabMoOperationService.get(data.Value<string>("FabMoOperationID"));
                    if (FOpemodel == null)
                    {
                        failIDs = UtilBussinessService.str(failIDs, data.Value<string>("TaskDispatchID"));
                        msg = UtilBussinessService.str(msg, "制令制程工序流水号错误，不存在的制令制程工序信息");
                        fail++;
                        continue;
                    }

                    Promodel = SFC_ItemProcessService.getByItemProcess(FabMoModel.ItemID, FPromodel.ProcessID);
                    if (Promodel != null)
                    {
                        Opemodel = SFC_ItemOperationService.getByItemProcess(Promodel.ItemProcessID, FOpemodel.OperationID);
                        if (Opemodel != null)
                        {
                            model.IsFPI = Opemodel.IsFPI;
                            model.IsIP = Opemodel.IsIP;
                            model.IsOSI = Opemodel.IsOSI;
                            model.InspectionGroupID = Opemodel.InspectionGroupID;
                        }
                    }
                }

                model.TaskNo = data.Value<string>("TaskNo");
                model.Sequence = data.Value<string>("Sequence");
                model.FabricatedMotherID = data.Value<string>("FabricatedMotherID");
                model.FabMoProcessID = data.Value<string>("FabMoProcessID");
                model.FabMoOperationID = data.Value<string>("FabMoOperationID");

                if (!string.IsNullOrWhiteSpace(model.FabricatedMotherID))
                {
                    SFC_FabricatedMother FMmodel = SFC_FabricatedMotherService.get(model.FabricatedMotherID);
                    model.MoSequence = FMmodel.SplitSequence;
                }

                model.Sequence = data.Value<string>("Sequence");
                model.ItemID = data.Value<string>("ItemID");
                model.ProcessID = data.Value<string>("ProcessID");
                model.OperationID = data.Value<string>("OperationID");
                if (!string.IsNullOrWhiteSpace(data.Value<string>("StartDate")))
                    model.StartDate = data.Value<DateTime>("StartDate");
                if (!string.IsNullOrWhiteSpace(data.Value<string>("FinishDate")))
                    model.FinishDate = data.Value<DateTime>("FinishDate");
                model.IsDispatch = false;
                //model.MESUserID = userid;
                //model.DispatchDate = DateTime.Now;
                model.DispatchQuantity = data.Value<Decimal>("DispatchQuantity");

                model.ClassID = data.Value<string>("ClassID");
                model.Comments = data.Value<string>("Comments");
                model.Status = data.Value<string>("Status");

                if (SFC_TaskDispatchService.Check(model.TaskNo, null))
                {
                    failIDs = UtilBussinessService.str(failIDs, data.Value<string>("TaskDispatchID"));
                    msg = UtilBussinessService.str(msg, model.TaskNo + "单号已存在");
                    fail++;
                    continue;
                }


                if (SFC_TaskDispatchService.insert(userid, model))
                    success++;
                else
                {
                    failIDs = UtilBussinessService.str(failIDs, data.Value<string>("TaskDispatchID"));
                    fail++;
                }

            }
            return new { success = success, fail = fail, failIDs = failIDs, msg = msg };
        }

        /// <summary>
        /// 单个任务卡分派新增
        /// Mouse 2017年9月26日10:00:44
        /// </summary>
        /// <param name="Token"></param>
        /// <param name="jArray"></param>
        /// <returns></returns>
        public static object Sfc00004InsertByAndroid(JObject data)
        {
            string userid = UtilBussinessService.detoken(data.Value<string>("Token"));
            SFC_TaskDispatch model = null;
            SFC_FabricatedMother FabMoModel = null;
            SFC_FabMoProcess FPromodel = null;
            SFC_FabMoOperation FOpemodel = null;
            SFC_ItemProcess Promodel = null;
            SFC_ItemOperation Opemodel = null;
            model = new SFC_TaskDispatch();
            model.TaskDispatchID = UniversalService.GetSerialNumber("SFC_TaskDispatch");
            //制令单
            FabMoModel = SFC_FabricatedMotherService.get(data.Value<string>("FabricatedMotherID"));
            if (FabMoModel == null)
            {
                return new { Status = "410", msg = "制令单流水号错误，不存在的制令单信息" };
            }
            //制令制程
            FPromodel = SFC_FabMoProcessService.get(data.Value<string>("FabMoProcessID"));
            if (FPromodel == null)
            {
                return new { Status = "410", msg = "制令制程流水号错误，不存在的制令制程信息 " };
            }

            if (string.IsNullOrWhiteSpace(data.Value<string>("FabMoOperationID")))
            {
                Promodel = SFC_ItemProcessService.getByItemProcess(FabMoModel.ItemID, FPromodel.ProcessID);
                if (Promodel != null)
                {
                    model.IsFPI = Promodel.IsFPI;
                    model.IsIP = Promodel.IsIP;
                    model.IsOSI = Promodel.IsOSI;
                    model.InspectionGroupID = Promodel.InspectionGroupID;
                }
            }
            else
            {
                FOpemodel = SFC_FabMoOperationService.get(data.Value<string>("FabMoOperationID"));
                if (FOpemodel == null)
                {
                    return new { Status = "410", msg = "制令制程工序流水号错误，不存在的制令制程工序信息" };
                }
                Promodel = SFC_ItemProcessService.getByItemProcess(FabMoModel.ItemID, FPromodel.ProcessID);
                if (Promodel != null)
                {
                    Opemodel = SFC_ItemOperationService.getByItemProcess(Promodel.ItemProcessID, FOpemodel.OperationID);
                    if (Opemodel != null)
                    {
                        model.IsFPI = Opemodel.IsFPI;
                        model.IsIP = Opemodel.IsIP;
                        model.IsOSI = Opemodel.IsOSI;
                        model.InspectionGroupID = Opemodel.InspectionGroupID;
                    }
                }
            }

            model.TaskNo = data.Value<string>("TaskNo");
            model.Sequence = data.Value<string>("Sequence");
            model.FabricatedMotherID = data.Value<string>("FabricatedMotherID");
            model.FabMoProcessID = data.Value<string>("FabMoProcessID");
            model.FabMoOperationID = data.Value<string>("FabMoOperationID");

            if (!string.IsNullOrWhiteSpace(model.FabricatedMotherID))
            {
                SFC_FabricatedMother FMmodel = SFC_FabricatedMotherService.get(model.FabricatedMotherID);
                model.MoSequence = FMmodel.SplitSequence;
            }

            model.Sequence = data.Value<string>("Sequence");
            model.ItemID = data.Value<string>("ItemID");
            model.ProcessID = data.Value<string>("ProcessID");
            model.OperationID = data.Value<string>("OperationID");
            if (!string.IsNullOrWhiteSpace(data.Value<string>("StartDate")))
                model.StartDate = data.Value<DateTime>("StartDate");
            if (!string.IsNullOrWhiteSpace(data.Value<string>("FinishDate")))
                model.FinishDate = data.Value<DateTime>("FinishDate");
            model.IsDispatch = false;
            //model.MESUserID = userid;
            //model.DispatchDate = DateTime.Now;
            model.DispatchQuantity = data.Value<Decimal>("DispatchQuantity");

            model.ClassID = data.Value<string>("ClassID");
            model.Comments = data.Value<string>("Comments");
            model.Status = data.Value<string>("Status");
            if (SFC_TaskDispatchService.Check(model.TaskNo, null))
            {
                return new { Status = "410", msg = model.TaskNo + "单号已存在" };
            }
            if (SFC_TaskDispatchService.insert(userid, model))
                return new { Status = "200", msg = "新增成功！" };
            else
                return new { Status = "410", msg = "新增失败！" };
        }

        /// <summary>
        /// 任务卡分派编辑
        /// SAM 2017年7月3日10:04:43
        /// </summary>
        /// <param name="Token"></param>
        /// <param name="jArray"></param>
        /// <returns></returns>
        public static object Sfc00004update(string Token, JArray jArray)
        {
            int success = 0;
            int fail = 0;
            string failIDs = null;
            string msg = null;
            string userid = UtilBussinessService.detoken(Token);
            JObject data = null;
            SFC_TaskDispatch model = null;
            SFC_FabMoOperation Opmodel = null;
            SFC_FabMoProcess Promodel = null;
            for (int i = 0; i < jArray.Count; i++)
            {
                data = (JObject)jArray[i];
                model = SFC_TaskDispatchService.get(data.Value<string>("TaskDispatchID"));
                model.ItemID = data.Value<string>("ItemID");
                model.ProcessID = data.Value<string>("ProcessID");
                model.OperationID = data.Value<string>("OperationID");
                model.ClassID = data.Value<string>("ClassID");
                if (!string.IsNullOrWhiteSpace(data.Value<string>("StartDate")))
                    model.StartDate = data.Value<DateTime>("StartDate");
                if (!string.IsNullOrWhiteSpace(data.Value<string>("FinishDate")))
                    model.FinishDate = data.Value<DateTime>("FinishDate");

                if (data.Value<string>("Status") == Framework.SystemID + "020121300008C")//如果他是要作废
                {
                    if (model.FinishQuantity > 0)
                    {
                        failIDs = UtilBussinessService.str(failIDs, data.Value<string>("TaskDispatchID"));
                        msg = UtilBussinessService.str(failIDs, "已報工不可作廢");
                        fail++;
                        continue;
                    }
                    if (model.Status == Framework.SystemID + "0201213000087")//如果是从立单作废
                    {

                    }
                    else //如果是其他状态作废，将对应的已分派量扣减
                    {
                        if (!string.IsNullOrWhiteSpace(model.FabMoOperationID))
                        {
                            Opmodel = SFC_FabMoOperationService.get(model.FabMoOperationID);
                            if (Opmodel != null)
                            {
                                Opmodel.AssignQuantity = Opmodel.AssignQuantity - model.DispatchQuantity;
                            }
                        }
                        else if (!string.IsNullOrWhiteSpace(model.FabMoProcessID))
                        {
                            Promodel = SFC_FabMoProcessService.get(model.FabMoProcessID);
                            if (Promodel != null)
                            {
                                Promodel.AssignQuantity = Promodel.AssignQuantity - model.DispatchQuantity;
                            }
                        }
                    }
                    model.Status = data.Value<string>("Status");
                }
                else
                {
                    if (data.Value<bool>("IsDispatch") && !string.IsNullOrWhiteSpace(model.MESUserID)) //如果是分派后的修改
                    {
                        decimal DiffQuantity = 0;
                        if (!string.IsNullOrWhiteSpace(model.FabMoOperationID))
                        {
                            Opmodel = SFC_FabMoOperationService.get(model.FabMoOperationID);
                            if (Opmodel != null)
                            {
                                DiffQuantity = SFC_TaskDispatchService.GetTaskDiffQuantity(null, Opmodel.FabMoOperationID);
                                if (data.Value<Decimal>("DispatchQuantity") + Opmodel.AssignQuantity - model.DispatchQuantity > Opmodel.Quantity + DiffQuantity)
                                {
                                    failIDs = UtilBussinessService.str(failIDs, data.Value<string>("TaskDispatchID"));
                                    msg = UtilBussinessService.str(msg, "分派量超量");
                                    fail++;
                                    continue;
                                }
                                Opmodel.AssignQuantity = Opmodel.AssignQuantity - model.DispatchQuantity + data.Value<Decimal>("DispatchQuantity");
                            }
                        }
                        else if (!string.IsNullOrWhiteSpace(model.FabMoProcessID))
                        {
                            Promodel = SFC_FabMoProcessService.get(model.FabMoProcessID);
                            if (Promodel != null)
                            {
                                DiffQuantity = SFC_TaskDispatchService.GetTaskDiffQuantity(Promodel.FabMoProcessID, null);
                                if (data.Value<Decimal>("DispatchQuantity") + Promodel.AssignQuantity - model.DispatchQuantity > Promodel.Quantity)
                                {
                                    failIDs = UtilBussinessService.str(failIDs, data.Value<string>("TaskDispatchID"));
                                    msg = UtilBussinessService.str(msg, "分派量超量");
                                    fail++;
                                    continue;
                                }
                                Promodel.AssignQuantity = Promodel.AssignQuantity - model.DispatchQuantity + data.Value<Decimal>("DispatchQuantity");
                            }
                        }
                        model.Status = data.Value<string>("Status");
                    }
                    else if (data.Value<bool>("IsDispatch") && string.IsNullOrWhiteSpace(model.MESUserID)) //首次分派
                    {
                        model.IsDispatch = true;
                        model.MESUserID = userid;
                        model.DispatchDate = DateTime.Now;
                        model.Status = Framework.SystemID + "0201213000088";
                        decimal DiffQuantity = 0;
                        if (!string.IsNullOrWhiteSpace(model.FabMoOperationID))
                        {
                            Opmodel = SFC_FabMoOperationService.get(model.FabMoOperationID);
                            if (Opmodel != null)
                            {
                                DiffQuantity = SFC_TaskDispatchService.GetTaskDiffQuantity(null, Opmodel.FabMoOperationID);
                                if (data.Value<Decimal>("DispatchQuantity") + Opmodel.AssignQuantity > Opmodel.Quantity + DiffQuantity)
                                {
                                    failIDs = UtilBussinessService.str(failIDs, data.Value<string>("TaskDispatchID"));
                                    msg = UtilBussinessService.str(msg, "分派量超量");
                                    fail++;
                                    continue;
                                }
                                Opmodel.AssignQuantity = Opmodel.AssignQuantity + data.Value<Decimal>("DispatchQuantity");
                            }
                        }
                        else if (!string.IsNullOrWhiteSpace(model.FabMoProcessID))
                        {
                            Promodel = SFC_FabMoProcessService.get(model.FabMoProcessID);
                            if (Promodel != null)
                            {
                                DiffQuantity = SFC_TaskDispatchService.GetTaskDiffQuantity(Promodel.FabMoProcessID, null);
                                if (data.Value<Decimal>("DispatchQuantity") + Promodel.AssignQuantity > Promodel.Quantity + DiffQuantity)
                                {
                                    failIDs = UtilBussinessService.str(failIDs, data.Value<string>("TaskDispatchID"));
                                    msg = UtilBussinessService.str(msg, "分派量超量");
                                    fail++;
                                    continue;
                                }
                                Promodel.AssignQuantity = Promodel.AssignQuantity + data.Value<Decimal>("DispatchQuantity");
                            }
                        }
                    }
                    model.DispatchQuantity = data.Value<Decimal>("DispatchQuantity");
                }
                model.ClassID = data.Value<string>("ClassID");
                model.Comments = data.Value<string>("Comments");

                if (SFC_TaskDispatchService.update(userid, model))
                {
                    if (Opmodel != null)
                        SFC_FabMoOperationService.update(userid, Opmodel);
                    if (Promodel != null)
                        SFC_FabMoProcessService.update(userid, Promodel);
                    success++;
                }
                else
                {
                    failIDs = UtilBussinessService.str(failIDs, data.Value<string>("TaskDispatchID"));
                    fail++;
                }

            }
            return new { success = success, fail = fail, failIDs = failIDs, msg = msg };
        }

        /// <summary>
        /// 任务卡分派编辑
        /// SAM 2017年7月3日10:04:43
        /// </summary>
        /// <param name="Token"></param>
        /// <param name="jArray"></param>
        /// <returns></returns>
        public static object Sfc00004UpdateByAndroid(JObject data)
        {
            string userid = UtilBussinessService.detoken(data.Value<string>("Token"));
            SFC_TaskDispatch model = null;
            SFC_FabMoOperation Opmodel = null;
            SFC_FabMoProcess Promodel = null;
            model = SFC_TaskDispatchService.get(data.Value<string>("TaskDispatchID"));
            model.ItemID = data.Value<string>("ItemID");
            model.ProcessID = data.Value<string>("ProcessID");
            model.OperationID = data.Value<string>("OperationID");
            model.ClassID = data.Value<string>("ClassID");
            if (!string.IsNullOrWhiteSpace(data.Value<string>("StartDate")))
                model.StartDate = data.Value<DateTime>("StartDate");
            if (!string.IsNullOrWhiteSpace(data.Value<string>("FinishDate")))
                model.FinishDate = data.Value<DateTime>("FinishDate");

            if (data.Value<string>("Status") == Framework.SystemID + "020121300008C")//如果他是要作废
            {
                if (model.FinishQuantity > 0)
                {
                    return new { Status = "410", msg = "已報工不可作廢" };
                }
                if (model.Status == Framework.SystemID + "020121300008F")//如果是从立单作废
                {

                }
                else //如果是其他状态作废，将对应的已分派量扣减
                {
                    if (!string.IsNullOrWhiteSpace(model.FabMoOperationID))
                    {
                        Opmodel = SFC_FabMoOperationService.get(model.FabMoOperationID);
                        if (Opmodel != null)
                        {
                            Opmodel.AssignQuantity = Opmodel.AssignQuantity - model.DispatchQuantity;
                        }
                    }
                    else if (!string.IsNullOrWhiteSpace(model.FabMoProcessID))
                    {
                        Promodel = SFC_FabMoProcessService.get(model.FabMoProcessID);
                        if (Promodel != null)
                        {
                            Promodel.AssignQuantity = Promodel.AssignQuantity - model.DispatchQuantity;
                        }
                    }
                }
                model.Status = data.Value<string>("Status");
            }
            else
            {
                if (data.Value<bool>("IsDispatch") && !string.IsNullOrWhiteSpace(model.MESUserID)) //如果是分派后的修改
                {
                    if (!string.IsNullOrWhiteSpace(model.FabMoOperationID))
                    {
                        Opmodel = SFC_FabMoOperationService.get(model.FabMoOperationID);
                        if (Opmodel != null)
                        {
                            if (data.Value<Decimal>("DispatchQuantity") + Opmodel.AssignQuantity - model.DispatchQuantity > Opmodel.Quantity)
                            {
                                return new { Status = "410", msg = "分派量超量" };
                            }
                            Opmodel.AssignQuantity = Opmodel.AssignQuantity - model.DispatchQuantity + data.Value<Decimal>("DispatchQuantity");
                        }
                    }
                    else if (!string.IsNullOrWhiteSpace(model.FabMoProcessID))
                    {
                        Promodel = SFC_FabMoProcessService.get(model.FabMoProcessID);
                        if (Promodel != null)
                        {
                            if (data.Value<Decimal>("DispatchQuantity") + Promodel.AssignQuantity - model.DispatchQuantity > Promodel.Quantity)
                            {
                                return new { Status = "410", msg = "分派量超量" };
                            }
                            Promodel.AssignQuantity = Promodel.AssignQuantity - model.DispatchQuantity + data.Value<Decimal>("DispatchQuantity");
                        }
                    }
                    model.Status = data.Value<string>("Status");
                }
                else if (data.Value<bool>("IsDispatch") && string.IsNullOrWhiteSpace(model.MESUserID)) //首次分派
                {
                    model.IsDispatch = true;
                    model.MESUserID = userid;
                    model.DispatchDate = DateTime.Now;
                    model.Status = Framework.SystemID + "0201213000088";

                    if (!string.IsNullOrWhiteSpace(model.FabMoOperationID))
                    {
                        Opmodel = SFC_FabMoOperationService.get(model.FabMoOperationID);
                        if (Opmodel != null)
                        {
                            if (data.Value<Decimal>("DispatchQuantity") + Opmodel.AssignQuantity > Opmodel.Quantity)
                            {
                                return new { Status = "410", msg = "分派量超量" };
                            }
                            Opmodel.AssignQuantity = Opmodel.AssignQuantity + data.Value<Decimal>("DispatchQuantity");
                        }
                    }
                    else if (!string.IsNullOrWhiteSpace(model.FabMoProcessID))
                    {
                        Promodel = SFC_FabMoProcessService.get(model.FabMoProcessID);
                        if (Promodel != null)
                        {
                            if (data.Value<Decimal>("DispatchQuantity") + Promodel.AssignQuantity > Promodel.Quantity)
                            {
                                return new { Status = "410", msg = "分派量超量" };
                            }
                            Promodel.AssignQuantity = Promodel.AssignQuantity + data.Value<Decimal>("DispatchQuantity");
                        }
                    }
                }
                model.DispatchQuantity = data.Value<Decimal>("DispatchQuantity");
            }
            model.ClassID = data.Value<string>("ClassID");
            model.Comments = data.Value<string>("Comments");

            if (SFC_TaskDispatchService.update(userid, model))
            {
                if (Opmodel != null)
                    SFC_FabMoOperationService.update(userid, Opmodel);
                if (Promodel != null)
                    SFC_FabMoProcessService.update(userid, Promodel);
                return new { Status = "200", msg = "修改成功！" };
            }
            else
            {
                return new { Status = "410", msg = "修改失败！" };
            }
        }

        /// <summary>
        /// 判断分派量是否已经大于生成量
        /// SAM 2017年7月6日15:23:58
        /// </summary>
        /// <param name="token"></param>
        /// <param name="DispatchQuantity"></param>
        /// <param name="FabMoProcessID"></param>
        /// <param name="FabricatedOPerationID"></param>
        /// <returns></returns>
        public static string Sfc00004CheckDispatchQuantity(string token, string DispatchQuantity, string FabMoProcessID, string FabMoOperationID, string TaskDispatchID)
        {
            //SFC_FabricatedMother model = null;
            decimal NAAssignQuantity = 0;
            if (string.IsNullOrWhiteSpace(FabMoOperationID))
            {
                SFC_FabMoProcess Pmodel = SFC_FabMoProcessService.get(FabMoProcessID);
                if (Pmodel == null)
                    return "并无制令制程信息";

                if (string.IsNullOrWhiteSpace(TaskDispatchID))//如果任务单流水号为空，证明是新增
                {
                    NAAssignQuantity = SFC_TaskDispatchService.GetNAAssignQuantity(Pmodel.FabMoProcessID, null);
                    if (long.Parse(DispatchQuantity) + Pmodel.AssignQuantity + NAAssignQuantity > Pmodel.Quantity)
                        return "分派量超量";
                    else
                        return "OK";
                }
                else
                {
                    SFC_TaskDispatch Taskmodel = SFC_TaskDispatchService.get(TaskDispatchID);
                    if (Taskmodel == null)
                        return "并无任务单信息";

                    NAAssignQuantity = SFC_TaskDispatchService.GetNAAssignQuantity(Pmodel.FabMoProcessID, null);
                    if (long.Parse(DispatchQuantity) + Pmodel.AssignQuantity - Taskmodel.DispatchQuantity + NAAssignQuantity > Pmodel.Quantity)
                        return "分派量超量";
                    else
                        return "OK";
                }
            }
            else
            {
                SFC_FabMoOperation Omodel = SFC_FabMoOperationService.get(FabMoOperationID);
                if (Omodel == null)
                    return "并无制令制程工序信息";

                if (string.IsNullOrWhiteSpace(TaskDispatchID))//如果任务单流水号为空，证明是新增
                {
                    NAAssignQuantity = SFC_TaskDispatchService.GetNAAssignQuantity(null, Omodel.FabMoOperationID);
                    //当前的分派量+工序的已分派量+立单的分派量  >  工序的总制造量
                    if (long.Parse(DispatchQuantity) + Omodel.AssignQuantity + NAAssignQuantity > Omodel.Quantity)
                        return "分派量超量";
                    else
                        return "OK";
                }
                else
                {
                    SFC_TaskDispatch Taskmodel = SFC_TaskDispatchService.get(TaskDispatchID);
                    if (Taskmodel == null)
                        return "并无任务单信息";

                    NAAssignQuantity = SFC_TaskDispatchService.GetNAAssignQuantity(null, Omodel.FabMoOperationID);
                    if (long.Parse(DispatchQuantity) + Omodel.AssignQuantity - Taskmodel.DispatchQuantity + NAAssignQuantity > Omodel.Quantity)
                        return "分派量超量";
                    else
                        return "OK";
                }
            }
        }

        /// <summary>
        /// 判断是否能够转作废
        /// SAM 2017年7月6日15:43:42
        /// </summary>
        /// <param name="token"></param>
        /// <param name="FabMoProcessID"></param>
        /// <param name="FabricatedOPerationID"></param>
        /// <returns></returns>
        public static bool Sfc00004CheckStatus(string token, string FabMoProcessID, string FabMoOperationID)
        {
            if (string.IsNullOrWhiteSpace(FabMoOperationID))
            {
                SFC_FabMoProcess Pmodel = SFC_FabMoProcessService.get(FabMoProcessID);
                if (Pmodel == null)
                    return false;
                if (Pmodel.FinProQuantity > 0)
                    return false;
                else
                    return true;
            }
            else
            {
                SFC_FabMoOperation Omodel = SFC_FabMoOperationService.get(FabMoProcessID);
                if (Omodel == null)
                    return true;
                if (Omodel.FinProQuantity > 0)
                    return false;
                else
                    return true;
            }
        }

        /// <summary>
        /// 根据任务单号获取他的信息
        /// SAM 2017年7月6日16:25:21
        /// </summary>
        /// <param name="token"></param>
        /// <param name="taskDispatchID"></param>
        /// <returns></returns>
        public static Hashtable Sfc00004GetTask(string token, string taskDispatchID)
        {
            Hashtable result = SFC_TaskDispatchService.Sfc00004GetTask(taskDispatchID);
            if (result != null)
            {
                result["NextWorkCenter"] = "";
                result["NextProcess"] = "";
                result["NextOPeration"] = "";
                result["CompletionDate"] = "";
                result["CompletionNo"] = "";
                result["FinProQuantity"] = "";

                SFC_CompletionOrder ComOrder = SFC_CompletionOrderService.GetLastByTaskDispatch(result["TaskDispatchID"].ToString());
                if (ComOrder != null)
                {
                    result["CompletionDate"] = ComOrder.Date;
                    result["CompletionNo"] = ComOrder.CompletionNo;
                    result["FinProQuantity"] = ComOrder.FinProQuantity;
                }


                SYS_Parameters Parmodel = null;
                SYS_WorkCenter WorkcenterModel = null;
                if (!string.IsNullOrWhiteSpace(result["FabMoOperationID"].ToString())) //如果制令制程工序并不空
                {
                    string FabMoOperationID = result["FabMoOperationID"].ToString();//制令制程工序流水号
                    string FabMoProcessID = result["FabMoProcessID"].ToString();//制令制程流水号
                    //首先，根据制令制程工序查询他的下一个工序
                    SFC_FabMoOperationRelationship PreFMOperationship = SFC_FabMoOperationRelationshipService.getByPreFMOperation(FabMoOperationID);
                    if (PreFMOperationship != null)//可以找到下一个工序
                    {
                        //根据下一个工序获取信息
                        SFC_FabMoOperation Operation = SFC_FabMoOperationService.get(PreFMOperationship.FabMoOperationID);
                        if (Operation != null)
                        {
                            SFC_FabMoProcess Process = SFC_FabMoProcessService.get(Operation.FabMoProcessID);
                            WorkcenterModel = SYS_WorkCenterService.get(Process.WorkCenterID);
                            result["NextWorkCenter"] = WorkcenterModel.Code + "_" + WorkcenterModel.Name;
                            Parmodel = SYS_ParameterService.get(Process.ProcessID);
                            result["NextProcess"] = Parmodel.Code + "_" + Parmodel.Name;
                            Parmodel = SYS_ParameterService.get(Operation.OperationID);
                            result["NextOPeration"] = Parmodel.Code + "_" + Parmodel.Name;
                        }
                    }
                    else//找不到下一个工序，那就找他本身的工序关系设定，看看是否为最终工序
                    {
                        SFC_FabMoOperationRelationship FMOperationship = SFC_FabMoOperationRelationshipService.getByFMOperation(FabMoOperationID);
                        if (FMOperationship == null) //没有本身的工序关系设定
                        {
                            //连本站都没有，返回異常資料,非最終工序且無下一站工序資料
                            result["NextWorkCenter"] = "異常資料,非最終工序且無下一站工序資料";
                            result["NextProcess"] = "異常資料,非最終工序且無下一站工序資料";
                            result["NextOPeration"] = "異常資料,非最終工序且無下一站工序資料";
                        }
                        else if (!FMOperationship.IsLastOperation) //存在本身的工序关系设定，但不是最终工序
                        {
                            //返回異常資料,非最終工序且無下一站工序資料
                            result["NextWorkCenter"] = "異常資料,非最終工序且無下一站工序資料";
                            result["NextProcess"] = "異常資料,非最終工序且無下一站工序資料";
                            result["NextOPeration"] = "異常資料,非最終工序且無下一站工序資料";
                        }
                        else//工序是最终工序，那么，去根据制程找下一个制程
                        {
                            SFC_FabMoRelationship PreFMProcessship = SFC_FabMoRelationshipService.getByPreFMProcess(FabMoProcessID);
                            if (PreFMProcessship == null) //找不到下一个制程
                            {
                                //找他本身的制程设定,判断是否最终制程
                                SFC_FabMoRelationship FMProcessship = SFC_FabMoRelationshipService.getByFMProcess(FabMoProcessID);
                                if (FMProcessship == null) //连本身制程设定都没找到
                                {
                                    result["NextWorkCenter"] = "異常資料,非最終製程且無下一站製程資料";
                                    result["NextProcess"] = "異常資料,非最終製程且無下一站製程資料";
                                    result["NextOPeration"] = "異常資料,非最終製程且無下一站製程資料";
                                }
                                else if (!FMProcessship.IfLastProcess)//找到本站的，但是不是最终的
                                {
                                    result["NextWorkCenter"] = "異常資料,非最終製程且無下一站製程資料";
                                    result["NextProcess"] = "異常資料,非最終製程且無下一站製程資料";
                                    result["NextOPeration"] = "異常資料,非最終製程且無下一站製程資料";
                                }
                                else//找到本站的，而且是最终制程
                                {
                                    result["NextWorkCenter"] = "最終站無下站資料";
                                    result["NextProcess"] = "最終站無下站資料";
                                    result["NextOPeration"] = "最終站無下站資料";
                                }
                            }
                            else //可以找到下一个制程
                            {
                                //获取下一个制令制程
                                SFC_FabMoProcess Process = SFC_FabMoProcessService.get(PreFMProcessship.FabMoProcessID);
                                WorkcenterModel = SYS_WorkCenterService.get(Process.WorkCenterID);
                                result["NextWorkCenter"] = WorkcenterModel.Code + "_" + WorkcenterModel.Name;
                                Parmodel = SYS_ParameterService.get(Process.ProcessID);
                                result["NextProcess"] = Parmodel.Code + "_" + Parmodel.Name;
                                //判断制令制程是否存在工序，是则拿取第一个工序
                                //存在则获取工序信息
                                SFC_FabMoOperation Operation = SFC_FabMoOperationService.GetFirstOperation(PreFMProcessship.FabMoProcessID);
                                if (Operation != null)
                                {
                                    Parmodel = SYS_ParameterService.get(Operation.OperationID);
                                    result["NextOPeration"] = Parmodel.Code + "_" + Parmodel.Name;
                                }
                            }
                        }
                    }
                }
                else //如果选的是制程
                {
                    string FabricatedProcessID = result["FabMoProcessID"].ToString();//制令制程流水号
                    //根据制令制程找他的下一个制令制程
                    SFC_FabMoRelationship PreFMProcessship = SFC_FabMoRelationshipService.getByPreFMProcess(FabricatedProcessID);
                    if (PreFMProcessship == null) //找不到下一个制程
                    {
                        //找他本身的制程设定,判断是否最终制程
                        SFC_FabMoRelationship FMProcessship = SFC_FabMoRelationshipService.getByFMProcess(FabricatedProcessID);
                        if (FMProcessship == null) //连本身制程设定都没找到
                        {
                            result["NextWorkCenter"] = "異常資料,非最終製程且無下一站製程資料";
                            result["NextProcess"] = "異常資料,非最終製程且無下一站製程資料";
                            result["NextOPeration"] = "異常資料,非最終製程且無下一站製程資料";
                        }
                        else if (!FMProcessship.IfLastProcess)//找到本站的，但是不是最终的
                        {
                            result["NextWorkCenter"] = "異常資料,非最終製程且無下一站製程資料";
                            result["NextProcess"] = "異常資料,非最終製程且無下一站製程資料";
                            result["NextOPeration"] = "異常資料,非最終製程且無下一站製程資料";
                        }
                        else//找到本站的，而且是最终制程
                        {
                            result["NextWorkCenter"] = "最終站無下站資料";
                            result["NextProcess"] = "最終站無下站資料";
                            result["NextOPeration"] = "最終站無下站資料";
                        }
                    }
                    else //可以找到下一个制程
                    {
                        //获取下一个制令制程
                        SFC_FabMoProcess Process = SFC_FabMoProcessService.get(PreFMProcessship.FabMoProcessID);
                        WorkcenterModel = SYS_WorkCenterService.get(Process.WorkCenterID);
                        result["NextWorkCenter"] = WorkcenterModel.Code + "_" + WorkcenterModel.Name;
                        Parmodel = SYS_ParameterService.get(Process.ProcessID);
                        result["NextProcess"] = Parmodel.Code + "_" + Parmodel.Name;
                        //判断制令制程是否存在工序，是则拿取第一个工序
                        //存在则获取工序信息
                        SFC_FabMoOperation Operation = SFC_FabMoOperationService.GetFirstOperation(Process.FabMoProcessID);
                        if (Operation != null)
                        {
                            Parmodel = SYS_ParameterService.get(Operation.OperationID);
                            result["NextOPeration"] = Parmodel.Code + "_" + Parmodel.Name;
                        }
                    }
                }
            }
            return result;
        }

        /// <summary>
        /// 根据任务单获取他的明细列表
        /// SAM 2017年7月10日17:35:22
        /// </summary>
        /// <param name="token"></param>
        /// <param name="taskDispatchID"></param>
        /// <returns></returns>
        public static List<Hashtable> Sfc00004GetResource(string token, string taskDispatchID)
        {
            List<Hashtable> result = new List<Hashtable>();
            IList<Hashtable> item = null;

            item = SFC_TaskDispatchResourceService.Sfc00004GetResourceList(taskDispatchID, Framework.SystemID + "0201213000084");
            if (item != null)
                result.AddRange(item);

            item = SFC_TaskDispatchResourceService.Sfc00004GetResourceList(taskDispatchID, Framework.SystemID + "0201213000085");
            if (item != null)
                result.AddRange(item);

            item = SFC_TaskDispatchResourceService.Sfc00004GetResourceList(taskDispatchID, Framework.SystemID + "0201213000086");
            if (item != null)
                result.AddRange(item);

            return result;
        }



        /// <summary>
        /// 任务单资源明细列表
        /// SAM 2017年7月3日14:48:30
        /// </summary>
        /// <param name="token"></param>
        /// <param name="TaskDispatchID"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <returns></returns>
        public static object Sfc00004GetResourceList(string token, string TaskDispatchID, string Type, int page, int rows)
        {
            int count = 0;
            IList<Hashtable> result = null;
            if (Type == Framework.SystemID + "0201213000084")//设备
                result = SFC_TaskDispatchResourceService.Sfc00004GetMResourceList(TaskDispatchID, page, rows, ref count);
            else if (Type == Framework.SystemID + "0201213000085")//人工
                result = SFC_TaskDispatchResourceService.Sfc00004GetLResourceList(TaskDispatchID, page, rows, ref count);
            else if (Type == Framework.SystemID + "0201213000086")//其他
                result = SFC_TaskDispatchResourceService.Sfc00004GetResourceList(TaskDispatchID, page, rows, ref count);
            return UtilBussinessService.getPaginationModel(result, count);
        }

        /// <summary>
        /// 删除任务单资源明细
        /// SAM 2017年7月5日16:54:54
        /// </summary>
        /// <param name="Token"></param>
        /// <param name="jArray"></param>
        /// <returns></returns>
        public static object Sfc00004Resourcedelete(string Token, JArray jArray)
        {
            int success = 0;
            int fail = 0;
            string failIDs = null;
            string msg = null;
            string userid = UtilBussinessService.detoken(Token);
            JObject data = null;
            SFC_TaskDispatchResource model = null;
            for (int i = 0; i < jArray.Count; i++)
            {
                data = (JObject)jArray[i];
                model = SFC_TaskDispatchResourceService.get(data.Value<string>("TaskDispatchResourceID"));
                //if (!SYS_ParameterService.USE00001Check(data.Value<string>("ParameterID")))
                //{
                //}
                //else
                //{
                //    failIDs = UtilBussinessService.str(failIDs, data.Value<string>("ParameterID"));
                //    msg = UtilBussinessService.str(msg, model.Code + "已使用，不能删除");
                //    fail++;
                //}
                model.Status = Framework.SystemID + "0201213000003";
                if (SFC_TaskDispatchResourceService.update(userid, model))
                    success++;
                else
                {
                    failIDs = UtilBussinessService.str(failIDs, data.Value<string>("TaskDispatchResourceID"));
                    fail++;
                }

            }
            return new { success = success, fail = fail, failIDs = failIDs, msg = msg };
        }

        /// <summary>
        /// 新增任务单资源明细
        /// SAM 2017年7月5日16:55:10
        /// </summary>
        /// <param name="Token"></param>
        /// <param name="jArray"></param>
        /// <returns></returns>
        public static object Sfc00004Resourceinsert(string Token, JArray jArray)
        {
            int success = 0;
            int fail = 0;
            string failIDs = null;
            string msg = null;
            string userid = UtilBussinessService.detoken(Token);
            JObject data = null;
            SFC_TaskDispatchResource model = null;
            for (int i = 0; i < jArray.Count; i++)
            {
                data = (JObject)jArray[i];
                if (SFC_TaskDispatchResourceService.Check(data.Value<string>("EquipmentID"), data.Value<string>("TaskDispatchID"), null))
                {
                    failIDs = UtilBussinessService.str(failIDs, data.Value<string>("TaskDispatchResourceID"));
                    msg = UtilBussinessService.str(msg, "资料重复");
                    fail++;
                    continue;
                }

                SFC_TaskDispatch Task = SFC_TaskDispatchService.get(data.Value<string>("TaskDispatchID"));
                if (Task == null)
                {
                    failIDs = UtilBussinessService.str(failIDs, data.Value<string>("TaskDispatchResourceID"));
                    msg = UtilBussinessService.str(msg, "不存在的任务单号");
                    fail++;
                    continue;
                }

                model = new SFC_TaskDispatchResource();
                model.TaskDispatchResourceID = UniversalService.GetSerialNumber("SFC_TaskDispatchResource");
                model.TaskDispatchID = data.Value<string>("TaskDispatchID");
                model.FabMoProcessID = Task.FabMoProcessID;
                model.FabMoOperationID = Task.FabMoOperationID;
                model.Type = data.Value<string>("Type");
                model.Sequence = data.Value<string>("Sequence");
                model.ResourceID = data.Value<string>("ResourceID");
                model.ResourceClassID = data.Value<string>("ResourceClassID");
                model.EquipmentID = data.Value<string>("EquipmentID");
                model.IfMain = data.Value<bool>("IfMain");
                model.Status = Framework.SystemID + "0201213000001";

                if (SFC_TaskDispatchResourceService.insert(userid, model))
                    success++;
                else
                {
                    failIDs = UtilBussinessService.str(failIDs, data.Value<string>("TaskDispatchResourceID"));
                    fail++;
                }


            }
            return new { success = success, fail = fail, failIDs = failIDs, msg = msg };
        }

        /// <summary>
        /// 单个新增任务单资源明细
        /// Mouse 2017年9月26日10:15:39
        /// </summary>
        /// <param name="Token"></param>
        /// <param name="jArray"></param>
        /// <returns></returns>
        public static object Sfc00004ResourceInsertByAndroid(JObject data)
        {
            string userid = UtilBussinessService.detoken(data.Value<string>("Token"));
            SFC_TaskDispatchResource model = null;
            if (SFC_TaskDispatchResourceService.Check(data.Value<string>("EquipmentID"), data.Value<string>("TaskDispatchID"), null))
            {
                return new { Status = "410", msg = "资料重复" };
            }

            SFC_TaskDispatch Task = SFC_TaskDispatchService.get(data.Value<string>("TaskDispatchID"));
            if (Task == null)
            {
                return new { Status = "410", msg = "不存在的任务单号" };
            }

            model = new SFC_TaskDispatchResource();
            model.TaskDispatchResourceID = UniversalService.GetSerialNumber("SFC_TaskDispatchResource");
            model.TaskDispatchID = data.Value<string>("TaskDispatchID");
            model.FabMoProcessID = Task.FabMoProcessID;
            model.FabMoOperationID = Task.FabMoOperationID;
            model.Type = data.Value<string>("Type");
            model.Sequence = data.Value<string>("Sequence");
            model.ResourceID = data.Value<string>("ResourceID");
            model.ResourceClassID = data.Value<string>("ResourceClassID");
            model.EquipmentID = data.Value<string>("EquipmentID");
            model.IfMain = data.Value<bool>("IfMain");
            model.Status = Framework.SystemID + "0201213000001";

            if (SFC_TaskDispatchResourceService.insert(userid, model))
                return new { Status = "200", msg = "新增成功！" };
            else
            {
                return new { Status = "410", msg = "新增失败！" };
            }
        }

        /// <summary>
        /// 更新任务单明细
        /// SAM 2017年7月5日17:03:31
        /// </summary>
        /// <param name="Token"></param>
        /// <param name="jArray"></param>
        /// <returns></returns>
        public static object Sfc00004Resourceupdate(string Token, JArray jArray)
        {
            int success = 0;
            int fail = 0;
            string failIDs = null;
            string msg = null;
            string userid = UtilBussinessService.detoken(Token);
            JObject data = null;
            SFC_TaskDispatchResource model = null;
            for (int i = 0; i < jArray.Count; i++)
            {
                data = (JObject)jArray[i];
                if (SFC_TaskDispatchResourceService.Check(data.Value<string>("EquipmentID"), data.Value<string>("TaskDispatchID"), data.Value<string>("TaskDispatchResourceID")))
                {
                    failIDs = UtilBussinessService.str(failIDs, data.Value<string>("ParameterID"));
                    msg = UtilBussinessService.str(msg, "资料重复");
                    fail++;
                    continue;
                }

                model = SFC_TaskDispatchResourceService.get(data.Value<string>("TaskDispatchResourceID"));
                model.EquipmentID = data.Value<string>("EquipmentID");
                model.IfMain = data.Value<bool>("IfMain");
                if (SFC_TaskDispatchResourceService.update(userid, model))
                    success++;
                else
                {
                    failIDs = UtilBussinessService.str(failIDs, data.Value<string>("TaskDispatchResourceID"));
                    fail++;
                }

            }
            return new { success = success, fail = fail, failIDs = failIDs, msg = msg };
        }


        /// <summary>
        /// 单个更新任务单明细
        /// Mouse 2017年9月26日10:21:15
        /// </summary>
        /// <param name="Token"></param>
        /// <param name="jArray"></param>
        /// <returns></returns>
        public static object Sfc00004ResourceUpdateByAndroid(JObject data)
        {
            string userid = UtilBussinessService.detoken(data.Value<string>("Token"));
            SFC_TaskDispatchResource model = null;
            if (SFC_TaskDispatchResourceService.Check(data.Value<string>("EquipmentID"), data.Value<string>("TaskDispatchID"), data.Value<string>("TaskDispatchResourceID")))
            {
                return new { Status = "410", msg = "资料重复" };
            }

            model = SFC_TaskDispatchResourceService.get(data.Value<string>("TaskDispatchResourceID"));
            model.EquipmentID = data.Value<string>("EquipmentID");
            model.IfMain = data.Value<bool>("IfMain");
            if (SFC_TaskDispatchResourceService.update(userid, model))
                return new { Status = "200", msg = "修改成功！" };
            else
            {
                return new { Status = "410", msg = "修改失败！" };
            }
        }

        /// <summary>
        /// 任务单资源明细的删除
        /// SAM 2017年7月10日16:59:05
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static object Sfc00004ResourceDelete(JObject data)
        {
            string Token = data.Value<string>("Token");
            string userid = UtilBussinessService.detoken(Token);

            try
            {
                SFC_TaskDispatchResource model = SFC_TaskDispatchResourceService.get(data.Value<string>("TaskDispatchResourceID"));
                if (model == null)
                    return new { status = "410", msg = "流水号为空！" };

                model.Status = Framework.SystemID + "0201213000003";
                if (SFC_TaskDispatchResourceService.update(userid, model))
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
        /// 根据制令制程流水号获取制令制程信息
        /// SAM 2017年10月24日11:33:04
        /// SFC04表头专用，已分派量=制造量（制程的）-已分派量（所有非删除，非作废的任务单）+差异量（所有非删除，非作废的任务单）
        /// </summary>
        /// <param name="fabMoProcessID"></param>
        /// <returns></returns>
        public static Hashtable Sfc00004GetFabMoProcess(string fabMoProcessID)
        {
            Hashtable result = SFC_FabMoProcessService.Sfc00004GetFabMoProcess(fabMoProcessID);
            //result["ALLAssignQuantity"] = decimal.Parse(result["Quantity"].ToString()) - decimal.Parse(result["TaskAssignQuantity"].ToString()) + decimal.Parse(result["TaskDiffQuantity"].ToString());
            return result;
        }

        /// <summary>
        /// 根据制令制程工序流水号获取制令制程工序信息
        /// SAM 2017年10月24日11:33:04
        /// SFC04表头专用，已分派量=制造量（工序）-已分派量（所有非删除，非作废的任务单）+差异量（所有非删除，非作废的任务单）
        /// </summary>
        /// <param name="fabMoProcessID"></param>
        /// <returns></returns>
        public static Hashtable Sfc00004GetFabMoOperation(string FabMoOperationID)
        {
            Hashtable result = SFC_FabMoOperationService.Sfc00004GetFabMoOperation(FabMoOperationID);
            //result["ALLAssignQuantity"] = decimal.Parse(result["Quantity"].ToString()) - decimal.Parse(result["TaskAssignQuantity"].ToString()) + decimal.Parse(result["TaskDiffQuantity"].ToString());
            return result;
        }

        /// <summary>
        /// 判断分派量是否合格
        /// SAM 2017年10月24日11:56:48
        /// V1版本：分派量的计算公式中加入了差异量，所以可分派量需要重新计算
        /// 
        /// 已分派量=制造量（工序）-已分派量（所有非删除，非作废的任务单）+差异量（所有非删除，非作废的任务单）
        /// </summary>
        /// <param name="token"></param>
        /// <param name="DispatchQuantity"></param>
        /// <param name="FabMoProcessID"></param>
        /// <param name="FabMoOperationID"></param>
        /// <param name="TaskDispatchID"></param>
        /// <returns></returns>
        public static string Sfc00004CheckDispatchQuantityV1(string token, string DispatchQuantity, string FabMoProcessID, string FabMoOperationID, string TaskDispatchID)
        {
            decimal AssignQuantity = 0;
            decimal DiffQuantity = 0;
            if (string.IsNullOrWhiteSpace(FabMoOperationID))
            {
                SFC_FabMoProcess Pmodel = SFC_FabMoProcessService.get(FabMoProcessID);
                if (Pmodel == null)
                    return "并无制令制程信息";
                AssignQuantity = SFC_TaskDispatchService.GetTaskAssignQuantity(Pmodel.FabMoProcessID, null);
                DiffQuantity = SFC_TaskDispatchService.GetTaskDiffQuantity(Pmodel.FabMoProcessID, null);

                if (string.IsNullOrWhiteSpace(TaskDispatchID))//如果任务单流水号为空，证明是新增
                {
                    if (decimal.Parse(DispatchQuantity) + AssignQuantity > Pmodel.Quantity + DiffQuantity)
                        return "分派量超量";
                    else
                        return "OK";
                }
                else
                {
                    SFC_TaskDispatch Taskmodel = SFC_TaskDispatchService.get(TaskDispatchID);
                    if (Taskmodel == null)
                        return "并无任务单信息";

                    if (decimal.Parse(DispatchQuantity) + AssignQuantity - Taskmodel.DispatchQuantity > Pmodel.Quantity + DiffQuantity)
                        return "分派量超量";
                    else
                        return "OK";
                }
            }
            else
            {
                SFC_FabMoOperation Omodel = SFC_FabMoOperationService.get(FabMoOperationID);
                if (Omodel == null)
                    return "并无制令制程工序信息";

                if (string.IsNullOrWhiteSpace(TaskDispatchID))//如果任务单流水号为空，证明是新增
                {
                    AssignQuantity = SFC_TaskDispatchService.GetTaskAssignQuantity(null, Omodel.FabMoOperationID);
                    DiffQuantity = SFC_TaskDispatchService.GetTaskDiffQuantity(null, Omodel.FabMoOperationID);

                    if (decimal.Parse(DispatchQuantity) + AssignQuantity > Omodel.Quantity + DiffQuantity)
                        return "分派量超量";
                    else
                        return "OK";
                }
                else
                {
                    SFC_TaskDispatch Taskmodel = SFC_TaskDispatchService.get(TaskDispatchID);
                    if (Taskmodel == null)
                        return "并无任务单信息";
                    if (decimal.Parse(DispatchQuantity) + AssignQuantity - Taskmodel.DispatchQuantity > Omodel.Quantity + DiffQuantity)
                        return "分派量超量";
                    else
                        return "OK";
                }
            }
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
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <returns></returns>
        public static object Sfc00006GetList(string token, string TaskNo, string MoNo, string WorkcenterCode, string ProcessCode,
            string OperationCode, string ClassCode, string StartDate, string EndDate, string Status, int page, int rows)
        {
            int count = 0;
            IList<Hashtable> result = SFC_TaskDispatchService.Sfc00006GetList(TaskNo, MoNo, WorkcenterCode, ProcessCode, OperationCode,
                ClassCode, StartDate, EndDate, Status, page, rows, ref count);
            foreach (Hashtable item in result)
            {
                item["Next"] = GetNext(item["TaskDispatchID"].ToString());
            }
            return UtilBussinessService.getPaginationModel(result, count);
        }

        /// <summary>
        /// 获取工作站作业列表
        /// SAM 2017年10月24日09:27:32 
        /// 在原来的基础上，加多了班别字段。代号-名称
        /// </summary>
        /// <param name="token"></param>
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
        public static object Sfc00006GetListV1(string token, string TaskNo, string MoNo, string WorkcenterCode, string ProcessCode,
            string OperationCode, string ClassCode, string StartDate, string EndDate, string Status, int page, int rows)
        {
            int count = 0;
            IList<Hashtable> result = SFC_TaskDispatchService.Sfc00006GetListV1(TaskNo, MoNo, WorkcenterCode, ProcessCode, OperationCode,
                ClassCode, StartDate, EndDate, Status, page, rows, ref count);
            foreach (Hashtable item in result)
            {
                item["Next"] = GetNext(item["TaskDispatchID"].ToString());
            }
            return UtilBussinessService.getPaginationModel(result, count);
        }

        /// <summary>
        /// 获取工作站作业列表
        /// SAM 2017年10月26日19:15:04
        /// 在V1的基础上，加多了进站人和出站人的显示
        /// </summary>
        /// <param name="token"></param>
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
        public static object Sfc00006GetListV2(string token, string TaskNo, string MoNo, string WorkcenterCode, string ProcessCode,
           string OperationCode, string ClassCode, string StartDate, string EndDate, string Status, int page, int rows)
        {
            int count = 0;
            IList<Hashtable> result = SFC_TaskDispatchService.Sfc00006GetListV2(TaskNo, MoNo, WorkcenterCode, ProcessCode, OperationCode,
                ClassCode, StartDate, EndDate, Status, page, rows, ref count);
            foreach (Hashtable item in result)
            {
                item["Next"] = GetNext(item["TaskDispatchID"].ToString());
            }
            return UtilBussinessService.getPaginationModel(result, count);
        }


        /// <summary>
        /// 根据任务单拿取他的下一站信息
        /// SAM 2017年8月29日14:10:32
        /// </summary>
        /// <param name="TaskDispatchID"></param>
        /// <returns></returns>
        public static string GetNext(string TaskDispatchID)
        {
            SFC_TaskDispatch Task = SFC_TaskDispatchService.get(TaskDispatchID);
            string result = null;
            SYS_Parameters Parmodel = null;
            if (Task != null)
            {
                if (!string.IsNullOrWhiteSpace(Task.FabMoOperationID)) //如果制令制程工序并不空
                {
                    string FabMoOperationID = Task.FabMoOperationID;//制令制程工序流水号
                    string FabMoProcessID = Task.FabMoProcessID;//制令制程流水号
                    //首先，根据制令制程工序查询他的下一个工序
                    SFC_FabMoOperationRelationship PreFMOperationship = SFC_FabMoOperationRelationshipService.getByPreFMOperation(FabMoOperationID);
                    if (PreFMOperationship != null)//可以找到下一个工序
                    {
                        //根据下一个工序获取信息
                        SFC_FabMoOperation Operation = SFC_FabMoOperationService.get(PreFMOperationship.FabMoOperationID);
                        if (Operation != null)
                        {
                            Parmodel = SYS_ParameterService.get(Operation.OperationID);
                            result = Parmodel.Code + "-" + Parmodel.Name;
                        }
                    }
                    else//找不到下一个工序，那就找他本身的工序关系设定，看看是否为最终工序
                    {
                        SFC_FabMoOperationRelationship FMOperationship = SFC_FabMoOperationRelationshipService.getByFMOperation(FabMoOperationID);
                        if (FMOperationship == null) //没有本身的工序关系设定
                        {
                            //连本站都没有，返回異常資料,非最終工序且無下一站工序資料
                            result = "異常資料,非最終工序且無下一站工序資料";
                        }
                        else if (!FMOperationship.IsLastOperation) //存在本身的工序关系设定，但不是最终工序
                        {
                            //返回異常資料,非最終工序且無下一站工序資料
                            result = "異常資料,非最終工序且無下一站工序資料";
                        }
                        else//工序是最终工序，那么，去根据制程找下一个制程
                        {
                            //首先，根据制程判断是否最终制程
                            if (SFC_FabMoRelationshipService.CheckIfLastProcess(FabMoProcessID)) //是最终制程
                            {
                                //是最终工序也是最终制程了。那么就不存在下一站了
                                result = "最終站無下站資料";
                            }
                            else
                            {
                                //既然对应的制程不是最终制程，那么应该可以找到下一站制程，如果没找到，那就是异常
                                SFC_FabMoRelationship PreFMProcessship = SFC_FabMoRelationshipService.getByPreFMProcess(FabMoProcessID);
                                if (PreFMProcessship == null) //找不到下一个制程
                                    result = "異常資料,非最終制程且無下一站制程資料";
                                else //可以找到下一个制程
                                {
                                    //获取下一个制令制程
                                    SFC_FabMoProcess Process = SFC_FabMoProcessService.get(PreFMProcessship.FabMoProcessID);
                                    //判断制令制程是否存在工序，是则拿取第一个工序
                                    SFC_FabMoOperation FristOperation = SFC_FabMoOperationService.GetFirstOperation(PreFMProcessship.FabMoProcessID);
                                    if (FristOperation != null)
                                    {
                                        Parmodel = SYS_ParameterService.get(FristOperation.OperationID);
                                        result = Parmodel.Code + "-" + Parmodel.Name;
                                    }
                                    else//没有，就拿制程的
                                    {
                                        Parmodel = SYS_ParameterService.get(Process.ProcessID);
                                        result = Parmodel.Code + "-" + Parmodel.Name;
                                    }
                                }
                            }
                        }
                    }
                }
                else //则此任务单对应制程
                {
                    string FabMoProcessID = Task.FabMoProcessID;//制令制程流水号
                    //首先，根据制程判断是否最终制程
                    if (SFC_FabMoRelationshipService.CheckIfLastProcess(FabMoProcessID)) //是最终制程
                    {
                        //是最终工序也是最终制程了。那么就不存在下一站了
                        result = "最終站無下站資料";
                    }
                    else
                    {
                        //根据制令制程找他的下一个制令制程
                        SFC_FabMoRelationship PreFMProcessship = SFC_FabMoRelationshipService.getByPreFMProcess(FabMoProcessID);
                        if (PreFMProcessship == null) //找不到下一个制程
                            result = "異常資料,非最終製程且無下一站製程資料";
                        else //可以找到下一个制程
                        {
                            //获取下一个制令制程
                            SFC_FabMoProcess Process = SFC_FabMoProcessService.get(PreFMProcessship.FabMoProcessID);
                            if (Process != null)
                            {
                                Parmodel = SYS_ParameterService.get(Process.ProcessID);
                                result = Parmodel.Code + "-" + Parmodel.Name;
                            }
                        }
                    }
                }
            }
            return result;
        }

        /// <summary>
        /// 获取料品列表
        /// Joint 2017年7月21日09:39:06
        /// </summary>
        /// <param name="Token"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <returns></returns>
        public static object Sfc00006GetItemList(string Token, string TaskDispatchID, int page, int rows)
        {
            IList<Hashtable> result = null;
            int count = 0;
            SFC_TaskDispatch Task = SFC_TaskDispatchService.get(TaskDispatchID);
            if (Task == null)
                return null;

            SFC_FabricatedMother FabMo = SFC_FabricatedMotherService.get(Task.FabricatedMotherID);
            if (FabMo == null)
                return null;

            if (string.IsNullOrWhiteSpace(Task.FabMoOperationID))
                result = SFC_FabMoItemService.Sfc00006GetItemList(null, Task.FabMoOperationID, page, rows, ref count);
            else
                result = SFC_FabMoItemService.Sfc00006GetItemList(Task.FabMoProcessID, null, page, rows, ref count);

            /*
             * 基本用料=(制程基本用料*分派數量/制造數量)=3000*1000/3000=1000
             * 使用數量=基本用料*(1+耗用率)=1000*(1+3%)=1030
             */
            foreach (Hashtable item in result)
            {
                item["BaseQty"] = decimal.Parse(item["BaseQty"].ToString()) * Task.DispatchQuantity / FabMo.Quantity;
                item["UseQty"] = decimal.Parse(item["BaseQty"].ToString()) * (1 + decimal.Parse(item["ScrapRate"].ToString()));
            }


            return UniversalService.getPaginationModel(result, count);
        }

        /// <summary>
        /// 获取资源异常列表
        /// Tom 2017年7月20日15点20分
        /// </summary>
        /// <param name="Token"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <returns></returns>
        public static object Sfc00006GetExceptionList(string token, string TaskDispatchID, int page, int rows)
        {
            int count = 0;
            return UniversalService.getPaginationModel(SFC_AbnormalDetailsService.GetList(TaskDispatchID, page, rows, ref count), count);
        }

        /// <summary>
        /// 保存资源异常
        /// Tom 2017年7月24日09:56:36
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public static object Sfc00006SaveException(JObject request)
        {
            string Token = request.Value<string>("Token");
            string userID = UniversalService.detoken(Token);
            return UtilBussinessService.Save<SFC_AbnormalDetails>(
                request,
                userID, m => m.AbnormalDetailID,
                m => m.AbnormalDetailID = UniversalService.GetSerialNumber("SFC_AbnormalDetails"),
                SFC_AbnormalDetailsService.insert,
                SFC_AbnormalDetailsService.update,
                null,
                SFC_AbnormalDetailsService.CheckInsertArgs,
                "资源异常重复",
                SFC_AbnormalDetailsService.CheckUpdateArgs,
                "资源异常重复");
        }

        /// <summary>
        /// 删除工作站资源异常
        /// Tom 2017年7月24日10:56:42
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public static object Sfc00006ExceptionDelete(JObject request)
        {
            try
            {
                string Token = request.Value<string>("Token");
                string userID = UniversalService.detoken(Token);
                string AbnormalDetailID = request.Value<string>("AbnormalDetailID");
                if (SFC_AbnormalDetailsService.delete(userID, AbnormalDetailID))
                {
                    return new { status = "200", msg = "删除成功" };
                }
                else
                {
                    return new { status = "400", msg = "删除失败" };
                }
            }
            catch (Exception ex)
            {
                DataLogerService.writeerrlog(ex);
                return new { status = "400", msg = "删除失败" };
            }
        }

        /// <summary>
        /// 进站
        /// Tom 2017年7月24日15:17:19
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public static object Sfc00006Arrival(JObject request)
        {
            string Token = request.Value<string>("Token");
            string userID = UniversalService.detoken(Token);
            string TaskDispatchID = request.Value<string>("TaskDispatchID");
            SFC_TaskDispatch m = SFC_TaskDispatchService.get(TaskDispatchID);
            if (m == null)
            {
                return new { status = "400", msg = "没有找到任务单信息" };
            }

            SFC_FabMoProcess p = SFC_FabMoProcessService.get(m.FabMoProcessID);
            if (p == null)
            {
                return new { status = "400", msg = "没有找到制令制程信息" };
            }
            if (p.BeginDate == null)
            {
                p.BeginDate = DateTime.Now;
                if (!SFC_FabMoProcessService.update(userID, p))
                {
                    return new { status = "400", msg = "更新实际开工时间失败" };
                }
            }

            //判断任务单下对应的设备是否处于保养状态
            if (EMS_EquipmentService.Sfc00006CheckEquipment(TaskDispatchID))
                return new { status = "410", msg = "存在机况为保养状态的机器设备！" };

            m.Status = Framework.SystemID + "0201213000089";
            m.InDateTime = DateTime.Now;
            if (SFC_TaskDispatchService.update(userID, m))
            {
                return new { status = "200", msg = "操作成功" };
            }
            else
            {
                return new { status = "400", msg = "操作失败" };
            }
        }

        /// <summary>
        /// 进站
        /// SAM 2017年10月26日21:44:22 
        /// 增加进站人的记录
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public static object Sfc00006ArrivalV1(JObject request)
        {
            string Token = request.Value<string>("Token");
            string userID = UniversalService.detoken(Token);
            string TaskDispatchID = request.Value<string>("TaskDispatchID");
            SFC_TaskDispatch Task = SFC_TaskDispatchService.get(TaskDispatchID);
            if (Task == null)
                return new { status = "410", msg = "没有找到任务单信息" };
            //if(Task.Status)

            SFC_FabMoProcess FabMoProcess = SFC_FabMoProcessService.get(Task.FabMoProcessID);
            if (FabMoProcess == null)
                return new { status = "410", msg = "没有找到制令制程信息" };

            if (FabMoProcess.BeginDate == null)
            {
                FabMoProcess.BeginDate = DateTime.Now;
                if (!SFC_FabMoProcessService.update(userID, FabMoProcess))
                    return new { status = "410", msg = "更新实际开工时间失败" };
            }

            //判断任务单下对应的设备是否处于保养状态
            if (EMS_EquipmentService.Sfc00006CheckEquipment(TaskDispatchID))
                return new { status = "410", msg = "存在机况为保养状态的机器设备！" };

            Task.Status = Framework.SystemID + "0201213000089";
            Task.InDateTime = DateTime.Now;
            Task.InMESUserID = userID;
            if (SFC_TaskDispatchService.UpdateV1(userID, Task))
            {
                return new { status = "200", msg = "操作成功" };
            }
            else
            {
                return new { status = "400", msg = "操作失败" };
            }
        }

        /// <summary>
        /// 出站
        /// Tom
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public static object Sfc00006Outbound(JObject request)
        {
            string Token = request.Value<string>("Token");
            string userID = UniversalService.detoken(Token);
            string TaskDispatchID = request.Value<string>("TaskDispatchID");
            SFC_TaskDispatch m = SFC_TaskDispatchService.get(TaskDispatchID);
            if (m == null)
                return new { status = "400", msg = "没有找到任务单信息" };

            if (SFC_CompletionOrderService.Sfc06CheckOrder(TaskDispatchID))
                return new { status = "400", msg = "此任务单存在未确认的完工单，不能出站！" };

            /*
             * 2017年9月21日16:54:40 顾问方面QQ提出修改：
             * sfc07中如果有数据未被确认，sfc06不能出站，需要给出相关提醒。
             */
            if (SFC_CompletionOrderService.Sfc00006CheckCompletion(TaskDispatchID))
                return new { status = "400", msg = "此任务单存在未确认的完工单，不能出站！" };

            m.Status = Framework.SystemID + "020121300008B";
            m.OutDateTime = DateTime.Now;

            if (SFC_TaskDispatchService.update(userID, m))
            {
                return new { status = "200", msg = "操作成功" };
            }
            else
            {
                return new { status = "400", msg = "操作失败" };
            }
        }

        /// <summary>
        /// 出站
        /// SAM 2017年10月26日21:45:103
        /// 增加出站人的记录
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public static object Sfc00006OutboundV1(JObject request)
        {
            string Token = request.Value<string>("Token");
            string userID = UniversalService.detoken(Token);
            string TaskDispatchID = request.Value<string>("TaskDispatchID");
            SFC_TaskDispatch m = SFC_TaskDispatchService.get(TaskDispatchID);
            if (m == null)
            {
                return new { status = "400", msg = "没有找到任务单信息" };
            }

            /*
             * 2017年9月21日16:54:40 顾问方面QQ提出修改：
             * sfc07中如果有数据未被确认，sfc06不能出站，需要给出相关提醒。
             */
            if (SFC_CompletionOrderService.Sfc00006CheckCompletion(TaskDispatchID))
                return new { status = "400", msg = "此任务单存在未确认的完工单，不能出站！" };

            /*
             * 情况1：如果从来没有生成过完工单，差异量等于派工量
             * 情况2：来到了这一步，证明此任务单对应的完工单都已经确认完毕，
             *        如果派工量大于完工量，差异量等于派工量-完工量
             *        否则什么都不做，因为此时完工量>=派工量，任务完成
             */
            if (!SFC_CompletionOrderService.Sfc06CheckOrder(TaskDispatchID))//没有产生过完工单
                m.DiffQuantity = m.DispatchQuantity;
            else
            {
                if (m.DispatchQuantity > m.FinishQuantity)
                    m.DiffQuantity = m.DispatchQuantity - m.FinishQuantity;
            }

            m.Status = Framework.SystemID + "020121300008B";
            m.OutDateTime = DateTime.Now;
            m.OutMESUserID = userID;
            if (SFC_TaskDispatchService.UpdateV1(userID, m))
            {
                return new { status = "200", msg = "操作成功" };
            }
            else
            {
                return new { status = "400", msg = "操作失败" };
            }
        }

        /// <summary>
        /// 异常暂停
        /// Tom 2017年7月25日14:11:22
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public static object Sfc00006ExceptionStop(JObject request)
        {
            string Token = request.Value<string>("Token");
            string userID = UniversalService.detoken(Token);
            string TaskDispatchID = request.Value<string>("TaskDispatchID");
            string AbnormalDetailID = request.Value<string>("AbnormalDetailID");
            SFC_TaskDispatch m = SFC_TaskDispatchService.get(TaskDispatchID);
            if (m == null)
                return new { status = "400", msg = "没有找到任务单信息" };
            SFC_AbnormalDetails Demodel = SFC_AbnormalDetailsService.get(AbnormalDetailID);
            if (Demodel == null)
                return new { status = "400", msg = "异常信息" };

            Demodel.StartTime = DateTime.Now;

            m.Status = Framework.SystemID + "020121300008A";
            if (SFC_TaskDispatchService.update(userID, m))
            {
                SFC_AbnormalDetailsService.update(userID, Demodel);
                return new { status = "200", msg = "操作成功" };
            }
            else
            {
                return new { status = "400", msg = "操作失败" };
            }
        }

        /// <summary>
        /// 异常解除
        /// Tom 2017年7月25日14:11:22
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public static object Sfc00006ExceptionRelieve(JObject request)
        {
            string Token = request.Value<string>("Token");
            string userID = UniversalService.detoken(Token);
            string TaskDispatchID = request.Value<string>("TaskDispatchID");
            SFC_TaskDispatch m = SFC_TaskDispatchService.get(TaskDispatchID);
            if (m == null)
            {
                return new { status = "400", msg = "没有找到任务单信息" };
            }

            m.Status = Framework.SystemID + "0201213000089";
            m.OutDateTime = DateTime.Now;
            if (SFC_TaskDispatchService.update(userID, m))
            {
                string AbnormalDetailID = request.Value<string>("AbnormalDetailID");
                SFC_AbnormalDetails am = SFC_AbnormalDetailsService.get(AbnormalDetailID);
                if (am == null)
                {
                    return new { status = "400", msg = "没有找到异常信息" };
                }
                //if (am.EndTime == null)
                //{
                am.EndTime = DateTime.Now;
                if (!SFC_AbnormalDetailsService.update(userID, am))
                {
                    return new { status = "400", msg = "更新异常信息失败" };
                }
                //}

                return new { status = "200", msg = "操作成功" };
            }
            else
            {
                return new { status = "400", msg = "更新任务单信息失败" };
            }
        }

        /// <summary>
        /// 完工新增
        /// SAM 2017年8月22日15:44:14
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public static object Sfc00006CompletionAdd(JObject request)
        {
            string Token = request.Value<string>("Token");
            string userid = UtilBussinessService.detoken(Token);

            SFC_TaskDispatch Task = SFC_TaskDispatchService.get(request.Value<string>("TaskDispatchID"));
            if (Task == null)
                return new { status = "410", msg = "任务单信息不存在！" };


            if (string.IsNullOrWhiteSpace(request.Value<string>("CompletionNo")))
                return new { status = "410", msg = "完工单号不能为空！" };

            SFC_CompletionOrder model = new SFC_CompletionOrder();
            model.CompletionOrderID = UniversalService.GetSerialNumber("SFC_CompletionOrder");

            if (!string.IsNullOrWhiteSpace(request.Value<string>("NextID")))//如果存在一下站制程或者工序
            {
                string NextID = request.Value<string>("NextID");
                if (NextID.StartsWith(Framework.SystemID + "083")) //如果是制程
                {
                    SFC_FabMoProcess FabMoProcess = SFC_FabMoProcessService.get(NextID);
                    if (FabMoProcess != null)
                    {
                        model.NextFabMoProcessID = NextID;
                        model.NextProcessID = FabMoProcess.ProcessID;
                    }
                }
                else if (NextID.StartsWith(Framework.SystemID + "087")) //如果是工序
                {
                    SFC_FabMoOperation FabMoOperation = SFC_FabMoOperationService.get(NextID);
                    if (FabMoOperation != null)
                    {
                        model.NextFabMoProcessID = FabMoOperation.FabMoProcessID;
                        model.NextFabMoOperationID = FabMoOperation.FabMoOperationID;
                        model.NextOperationID = FabMoOperation.OperationID;
                        SFC_FabMoProcess FabMoProcess = SFC_FabMoProcessService.get(FabMoOperation.FabMoProcessID);
                        if (FabMoProcess != null)
                        {
                            model.NextProcessID = FabMoProcess.ProcessID;
                        }
                    }
                }
            }

            model.TaskDispatchID = Task.TaskDispatchID;
            model.FabricatedMotherID = Task.FabricatedMotherID;
            model.FabMoProcessID = Task.FabMoProcessID;
            model.FabMoOperationID = Task.FabMoOperationID;
            model.ItemID = Task.ItemID;
            model.ProcessID = Task.ProcessID;
            model.OperationID = Task.OperationID;
            model.IsIF = Task.IsIP;
            model.Sequence = 1;
            model.CompletionNo = request.Value<string>("CompletionNo");
            model.Date = request.Value<DateTime>("Date");
            model.TaskDispatchID = request.Value<string>("TaskDispatchID");
            model.FinProQuantity = request.Value<decimal>("FinProQuantity");
            model.ScrappedQuantity = request.Value<decimal>("ScrappedQuantity");
            model.DifferenceQuantity = request.Value<decimal>("DifferenceQuantity");
            model.RepairQuantity = request.Value<decimal>("RepairQuantity");
            model.FinProQuantity = request.Value<decimal>("FinProQuantity");
            model.ScrappedQuantity = request.Value<decimal>("ScrappedQuantity");
            model.DifferenceQuantity = request.Value<decimal>("DifferenceQuantity");
            model.RepairQuantity = request.Value<decimal>("RepairQuantity");
            model.LaborHour = UtilBussinessService.StrConversionHour(request.Value<string>("LaborHour"));
            model.MachineHour = UtilBussinessService.StrConversionHour(request.Value<string>("MachineHour"));
            model.UnLaborHour = UtilBussinessService.StrConversionHour(request.Value<string>("UnLaborHour"));
            model.UnMachineHour = UtilBussinessService.StrConversionHour(request.Value<string>("UnMachineHour"));
            if (model.LaborHour == -1)
                return new { status = "410", msg = "人工工时格式异常！" };
            if (model.UnLaborHour == -1)
                return new { status = "410", msg = "无效人工工时格式异常！" };
            if (model.MachineHour == -1)
                return new { status = "410", msg = "机器工时格式异常！" };
            if (model.UnMachineHour == -1)
                return new { status = "410", msg = "无效机器工时格式异常！" };
            model.Status = Framework.SystemID + "0201213000029";
            model.Type = Framework.SystemID + "02012130000A0";
            model.Comments = request.Value<string>("Comments");
            model.DTSID = request.Value<string>("DTSID");
            if (model.ScrappedQuantity > model.FinProQuantity)
                return new { status = "410", msg = "报废量大于完工量！" };

            //if (model.FinProQuantity + Task.FinishQuantity > Task.DispatchQuantity)
            //    return new { status = "410", msg = "累計報工量(數量)> 任務單分派量！" };

            if (model.FinProQuantity + Task.FinishQuantity < 0)
                return new { status = "410", msg = "累計報工量(數量) < 0！" };

            SYS_DocumentTypeSetting Dco = SYS_DocumentTypeSettingService.get(model.DTSID);
            if (Dco == null)
                return new { status = "410", msg = "单据设定流水号错误！" };

            string AutoNumberID = request.Value<string>("AutoNumberID");
            /*当保存时,如果完工单号已存在，则自动获取下一完工单号*/
            /*SAM 2017年9月13日16:46:44*/
            /*
             * 这里的计算逻辑有个弊端就是，存在一个情况是完工单新增进去了但是并没有及时更新流水，造成了无限死循环。
             * 所以目前做了一个机制，循环计数，如果循环超过5次，就更新一次流水,然后重置循环次数
             */
            int Seq = 1;
            while (SFC_CompletionOrderService.CheckCode(model.CompletionNo))
            {
                if (Seq == 5)
                {
                    UtilBussinessService.UpdateDocumentAutoNumber(userid, AutoNumberID);
                    Seq = 1;
                }
                else
                {
                    AutoNumberID = null;
                    model.CompletionNo = UtilBussinessService.GetDocumentAutoNumber(userid, model.DTSID, model.Date.ToString(), ref AutoNumberID);
                    Seq++;
                }
            }

            if (SFC_CompletionOrderService.insert(userid, model))
            {
                UtilBussinessService.UpdateDocumentAutoNumber(userid, AutoNumberID);
                return new { status = "200", msg = "新增成功！" };
            }
            else
                return new { status = "410", msg = "新增失败！" };
        }

        /// <summary>
        /// 检测对应的任务单可否报工
        /// Sam 2017年11月1日14:43:30
        /// </summary>
        /// <param name="Token"></param>
        /// <param name="TaskDispatchID"></param>
        /// <returns></returns>
        public static bool Sfc00006CheckCompletion(string Token,string TaskDispatchID)
        {
            SFC_TaskDispatch Task = SFC_TaskDispatchService.get(TaskDispatchID);
            if (Task == null)
                return false;

            if (Task.IsFPI == false)
                return true;

            if (Task.FPIPass)
                return true;
            else
                return false;
        }

        #endregion

        #region SFC00007完工回报作业
        /// <summary>
        /// 完工單回報作業-列表
        /// SAM 2017年7月19日14:42:51
        /// </summary>
        /// <param name="token"></param>
        /// <param name="FinishNo"></param>
        /// <param name="EndWorkD"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <returns></returns>
        public static object SFC00007GetList(string token, string TaskDispatchID, string FinishNo, string Date, string WorkCenter, string Process, string FabricatedMother, string Status, int page, int rows)
        {
            int count = 0;
            return UtilBussinessService.getPaginationModel(SFC_CompletionOrderService.SFC00007GetList(TaskDispatchID, FinishNo, Date, WorkCenter, Process, FabricatedMother, Status, page, rows, ref count), count);
        }

        /// <summary>
        /// 根据任务单流水号获取完工单
        /// Tom 2017年7月26日15点09分
        /// </summary>
        /// <param name="Token"></param>
        /// <param name="TaskDispatchID"></param>
        /// <returns></returns>
        public static object Sfc00007GetByTaskDispatchID(string token, string taskDispatchID)
        {
            return SFC_CompletionOrderService.GetByTaskDispatchID(taskDispatchID);
        }

        /// <summary>
        /// 获取当前登录用户的类别设定
        /// SAM 2017年7月28日14:49:48
        /// </summary>
        /// <param name="Token"></param>
        /// <returns></returns>
        public static IList<Hashtable> Sfc00007GetTypeList(string Token)
        {
            string userid = UtilBussinessService.detoken(Token);
            return SYS_DocumentTypeSettingService.GetTypeList(userid, Framework.SystemID + "0201213000036");
        }

        /// <summary>
        /// 完工单拿号
        /// SAM 2017年7月28日14:59:19
        /// </summary>
        /// <param name="token"></param>
        /// <param name="dTSID"></param>
        /// <returns></returns>
        public static object Sfc00007GetAutoNumber(string Token, string dTSID, string Date)
        {
            DateTime Now = DateTime.Now;
            if (!string.IsNullOrWhiteSpace(Date))
            {
                try
                {
                    Now = DateTime.Parse(Date);
                }
                catch (Exception ex)
                {
                    DataLogerService.writeerrlog(ex);
                }
            }

            string userid = UtilBussinessService.detoken(Token);
            string Prevchar = null;
            SYS_DocumentTypeSetting number = SYS_DocumentTypeSettingService.get(dTSID);
            if (number == null)
                return null;

            SYS_Parameters ParModel = SYS_ParameterService.get(number.GiveWay);
            if (ParModel.Code == "M")
                Prevchar = number.Code + Now.ToString("yyMM");
            else if (ParModel.Code == "Y")
                Prevchar = number.Code + Now.ToString("yy");
            else if (ParModel.Code == "D")
                Prevchar = number.Code + Now.ToString("yyMMdd");

            string AutoNumber = null;
            string AutoNumberID = null;

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

            AutoNumber = model.DefaultCharacter + (model.Num + 1).ToString().PadLeft(number.CodeLength, '0');
            AutoNumberID = model.DocumentAutoNumberID;

            return new { AutoNumber = AutoNumber, AutoNumberID = AutoNumberID };
        }

        /// <summary>
        ///  判断累計報工量(數量)是否任務單分派量
        ///  SAM 2017年8月23日18:34:16
        /// </summary>
        /// <param name="token"></param>
        /// <param name="TaskDispatchID"></param>
        /// <param name="FinProQuantity"></param>
        /// <returns></returns>
        public static bool Sfc00007CheckFinProQuantity(string token, string TaskDispatchID, string FinProQuantity)
        {
            SFC_TaskDispatch Task = SFC_TaskDispatchService.get(TaskDispatchID);
            if (Task == null)
                return false;

            if ((decimal.Parse(FinProQuantity) + Task.FinishQuantity > Task.DispatchQuantity))
                return false;
            else
                return true;
        }

        /// <summary>
        /// 完工單回報作業-任務單號開窗（状态不为CL和CA）
        /// SAM 2017年7月19日15:14:35
        /// </summary>
        /// <param name="token"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <returns></returns>
        public static object Sfc00007GetTaskList(string token, string TaskCode, string ItemCode, int page, int rows)
        {
            int count = 0;
            return UtilBussinessService.getPaginationModel(SFC_TaskDispatchService.Sfc00007GetTaskList(TaskCode, ItemCode, page, rows, ref count), count);
        }

        /// <summary>
        /// 完工單回報作業-任務單號開窗（状态不为CL和CA）
        /// SAM 2017年10月24日11:20:11
        /// V1版本，加多班别字段显示，然后因为界面并无查询条件，移除查询条件
        /// 只查询 首检为N 或者 首检为Y并且首檢品質判定为Y 的任务单
        /// </summary>
        /// <param name="token"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <returns></returns>
        public static object Sfc00007GetTaskListV1(string token, int page, int rows)
        {
            int count = 0;
            return UtilBussinessService.getPaginationModel(SFC_TaskDispatchService.Sfc00007GetTaskListV1(page, rows, ref count), count);
        }

        /// <summary>
        /// 完工單回報作業-批號屬性—取號开窗
        /// SAM 2017年7月20日14:29:10
        /// </summary>
        /// <param name="token"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <returns></returns>
        public static object Sfc00007GetLotAutoNumber(string token, int page, int rows)
        {
            int count = 0;
            return UtilBussinessService.getPaginationModel(SYS_AutoNumberRecordService.Sfc00007GetLotAutoNumber(page, rows, ref count), count);
        }

        /// <summary>
        /// 根据任务单流水号获取下一站制程（工序）
        /// </summary>
        /// <param name="TaskDispatchID"></param>
        /// <returns></returns>
        public static object Sfc00007GetNextList(string TaskDispatchID)
        {
            SFC_TaskDispatch Task = SFC_TaskDispatchService.get(TaskDispatchID);
            if (Task == null)
                return null;

            if (string.IsNullOrWhiteSpace(Task.FabMoOperationID))//如果制令制程工序为空，则证明这个任务单是对应到制程，拿下一站制程
            {
                return SFC_FabMoRelationshipService.getListByPreFMProcess(Task.FabMoProcessID);
            }
            else//不为空，则拿下一站工序
            {
                return SFC_FabMoOperationRelationshipService.getListByPreFMOperation(Task.FabMoOperationID);
            }
        }

        /// <summary>
        /// 完工单的新增
        /// SAM 2017年7月20日11:14:54
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public static object Sfc00007Add(JObject request)
        {
            string Token = request.Value<string>("Token");
            string userid = UtilBussinessService.detoken(Token);

            SFC_TaskDispatch Task = SFC_TaskDispatchService.get(request.Value<string>("TaskDispatchID"));
            if (Task == null)
                return new { status = "410", msg = "任务单信息不存在！" };


            if (string.IsNullOrWhiteSpace(request.Value<string>("CompletionNo")))
                return new { status = "410", msg = "完工单号不能为空！" };

            SFC_CompletionOrder model = new SFC_CompletionOrder();
            model.CompletionOrderID = UniversalService.GetSerialNumber("SFC_CompletionOrder");

            if (!string.IsNullOrWhiteSpace(request.Value<string>("NextID")))//如果存在一下站制程或者工序
            {
                string NextID = request.Value<string>("NextID");
                if (NextID.StartsWith(Framework.SystemID + "083")) //如果是制程
                {
                    SFC_FabMoProcess FabMoProcess = SFC_FabMoProcessService.get(NextID);
                    if (FabMoProcess != null)
                    {
                        model.NextFabMoProcessID = NextID;
                        model.NextProcessID = FabMoProcess.ProcessID;
                    }
                }
                else if (NextID.StartsWith(Framework.SystemID + "087")) //如果是工序
                {
                    SFC_FabMoOperation FabMoOperation = SFC_FabMoOperationService.get(NextID);
                    if (FabMoOperation != null)
                    {
                        model.NextFabMoProcessID = FabMoOperation.FabMoProcessID;
                        model.NextFabMoOperationID = FabMoOperation.FabMoOperationID;
                        model.NextOperationID = FabMoOperation.OperationID;
                        SFC_FabMoProcess FabMoProcess = SFC_FabMoProcessService.get(FabMoOperation.FabMoProcessID);
                        if (FabMoProcess != null)
                        {
                            model.NextProcessID = FabMoProcess.ProcessID;
                        }
                    }
                }
            }
            model.DTSID = request.Value<string>("DTSID");
            model.TaskDispatchID = Task.TaskDispatchID;
            model.FabricatedMotherID = Task.FabricatedMotherID;
            model.FabMoProcessID = Task.FabMoProcessID;
            model.FabMoOperationID = Task.FabMoOperationID;
            model.ItemID = Task.ItemID;
            model.ProcessID = Task.ProcessID;
            model.OperationID = Task.OperationID;
            model.IsIF = Task.IsIP;
            model.Sequence = 1;
            model.CompletionNo = request.Value<string>("CompletionNo");
            model.Date = request.Value<DateTime>("Date");
            model.TaskDispatchID = request.Value<string>("TaskDispatchID");
            model.FinProQuantity = request.Value<decimal>("FinProQuantity");
            model.ScrappedQuantity = request.Value<decimal>("ScrappedQuantity");
            model.DifferenceQuantity = request.Value<decimal>("DifferenceQuantity");
            model.RepairQuantity = request.Value<decimal>("RepairQuantity");
            model.FinProQuantity = request.Value<decimal>("FinProQuantity");
            model.ScrappedQuantity = request.Value<decimal>("ScrappedQuantity");
            model.DifferenceQuantity = request.Value<decimal>("DifferenceQuantity");
            model.RepairQuantity = request.Value<decimal>("RepairQuantity");
            model.LaborHour = UtilBussinessService.StrConversionHour(request.Value<string>("LaborHour"));
            model.MachineHour = UtilBussinessService.StrConversionHour(request.Value<string>("MachineHour"));
            model.UnLaborHour = UtilBussinessService.StrConversionHour(request.Value<string>("UnLaborHour"));
            model.UnMachineHour = UtilBussinessService.StrConversionHour(request.Value<string>("UnMachineHour"));
            if (model.LaborHour == -1)
                return new { status = "410", msg = "人工工时格式异常！" };
            if (model.UnLaborHour == -1)
                return new { status = "410", msg = "无效人工工时格式异常！" };
            if (model.MachineHour == -1)
                return new { status = "410", msg = "机器工时格式异常！" };
            if (model.UnMachineHour == -1)
                return new { status = "410", msg = "无效机器工时格式异常！" };
            model.Status = Framework.SystemID + "0201213000029";
            model.Type = Framework.SystemID + "02012130000A0";
            model.Comments = request.Value<string>("Comments");

            if (model.ScrappedQuantity > model.FinProQuantity)
                return new { status = "410", msg = "报废量大于完工量！" };

            //if (model.FinProQuantity + Task.FinishQuantity > Task.DispatchQuantity)
            //    return new { status = "410", msg = "累計報工量(數量)> 任務單分派量！" };

            if (model.FinProQuantity + Task.FinishQuantity < 0)
                return new { status = "410", msg = "累計報工量(數量) < 0！" };

            SYS_DocumentTypeSetting Dco = SYS_DocumentTypeSettingService.get(model.DTSID);
            if (Dco == null)
                return new { status = "410", msg = "单据设定流水号错误！" };


            string AutoNumberID = request.Value<string>("AutoNumberID");
            /*当保存时,如果完工单号已存在，则自动获取下一完工单号*/
            /*SAM 2017年9月13日16:46:44*/
            while (SFC_CompletionOrderService.CheckCode(model.CompletionNo))
            {
                AutoNumberID = null;
                model.CompletionNo = UtilBussinessService.GetDocumentAutoNumber(userid, model.DTSID, model.Date.ToString(), ref AutoNumberID);
            }

            if (SFC_CompletionOrderService.insert(userid, model))
            {
                UtilBussinessService.UpdateDocumentAutoNumber(userid, AutoNumberID);
                return new { status = "200", msg = "新增成功！" };
            }
            else
                return new { status = "410", msg = "新增失败！" };
        }

        /// <summary>
        /// 完工单的新增
        /// SAM 2017年7月20日11:24:13
        /// </summary>
        /// <param name="Token"></param>
        /// <param name="jArray"></param>
        /// <returns></returns>
        public static object Sfc00007insert(string Token, JArray jArray)
        {
            int success = 0;
            int fail = 0;
            string failIDs = null;
            string msg = null;
            string userid = UtilBussinessService.detoken(Token);
            JObject data = null;
            SFC_CompletionOrder model = null;
            for (int i = 0; i < jArray.Count; i++)
            {
                data = (JObject)jArray[i];
                SFC_TaskDispatch Task = SFC_TaskDispatchService.get(data.Value<string>("TaskDispatchID"));
                if (Task == null)
                {
                    failIDs = UtilBussinessService.str(failIDs, data.Value<string>("CompletionOrderID"));
                    msg = UtilBussinessService.str(msg, "任务单信息不存在！");
                    fail++;
                    continue;
                }

                if (string.IsNullOrWhiteSpace(data.Value<string>("CompletionNo")))
                {
                    failIDs = UtilBussinessService.str(failIDs, data.Value<string>("CompletionOrderID"));
                    msg = UtilBussinessService.str(msg, "完工单号不能为空！");
                    fail++;
                    continue;
                }


                model = new SFC_CompletionOrder();
                model.CompletionOrderID = UniversalService.GetSerialNumber("SFC_CompletionOrder");

                model.DTSID = data.Value<string>("DTSID");
                if (string.IsNullOrWhiteSpace(model.DTSID))
                {
                    failIDs = UtilBussinessService.str(failIDs, data.Value<string>("CompletionOrderID"));
                    msg = UtilBussinessService.str(msg, "单据类别不能为空！");
                    fail++;
                    continue;
                }
                SYS_DocumentTypeSetting Set = SYS_DocumentTypeSettingService.get(model.DTSID);
                if (Set == null)
                {
                    failIDs = UtilBussinessService.str(failIDs, data.Value<string>("CompletionOrderID"));
                    msg = UtilBussinessService.str(msg, "单据类别错误！");
                    fail++;
                    continue;
                }

                if (!string.IsNullOrWhiteSpace(data.Value<string>("NextID")))//如果存在一下站制程或者工序
                {
                    string NextID = data.Value<string>("NextID");
                    if (NextID.StartsWith(Framework.SystemID + "083")) //如果是制程
                    {
                        SFC_FabMoProcess FabMoProcess = SFC_FabMoProcessService.get(NextID);
                        if (FabMoProcess != null)
                        {
                            model.NextFabMoProcessID = NextID;
                            model.NextProcessID = FabMoProcess.ProcessID;
                        }
                    }
                    else if (NextID.StartsWith(Framework.SystemID + "087")) //如果是工序
                    {
                        SFC_FabMoOperation FabMoOperation = SFC_FabMoOperationService.get(NextID);
                        if (FabMoOperation != null)
                        {
                            model.NextFabMoProcessID = FabMoOperation.FabMoProcessID;
                            model.NextFabMoOperationID = FabMoOperation.FabMoOperationID;
                            model.NextOperationID = FabMoOperation.OperationID;
                            SFC_FabMoProcess FabMoProcess = SFC_FabMoProcessService.get(FabMoOperation.FabMoProcessID);
                            if (FabMoProcess != null)
                            {
                                model.NextProcessID = FabMoProcess.ProcessID;
                            }
                        }
                    }
                }

                model.CompletionNo = data.Value<string>("CompletionNo");
                model.Date = data.Value<DateTime>("Date");
                model.TaskDispatchID = data.Value<string>("TaskDispatchID");
                model.FabricatedMotherID = Task.FabricatedMotherID;
                model.FabMoProcessID = Task.FabMoProcessID;
                model.FabMoOperationID = Task.FabMoOperationID;
                model.ItemID = Task.ItemID;
                model.ProcessID = Task.ProcessID;
                model.OperationID = Task.OperationID;
                model.IsIF = Task.IsFPI;
                model.Sequence = 1;
                model.FinProQuantity = data.Value<decimal>("FinProQuantity");
                model.ScrappedQuantity = data.Value<decimal>("ScrappedQuantity");
                model.DifferenceQuantity = data.Value<decimal>("DifferenceQuantity");
                model.RepairQuantity = data.Value<decimal>("RepairQuantity");
                model.LaborHour = UtilBussinessService.StrConversionHour(data.Value<string>("LaborHour"));
                model.MachineHour = UtilBussinessService.StrConversionHour(data.Value<string>("MachineHour"));
                model.UnLaborHour = UtilBussinessService.StrConversionHour(data.Value<string>("UnLaborHour"));
                model.UnMachineHour = UtilBussinessService.StrConversionHour(data.Value<string>("UnMachineHour"));
                if (model.LaborHour == -1)
                {
                    failIDs = UtilBussinessService.str(failIDs, data.Value<string>("CompletionOrderID"));
                    msg = UtilBussinessService.str(msg, "人工工时格式异常！");
                    fail++;
                    continue;
                }
                if (model.UnLaborHour == -1)
                {
                    failIDs = UtilBussinessService.str(failIDs, data.Value<string>("CompletionOrderID"));
                    msg = UtilBussinessService.str(msg, "无效人工工时格式异常！");
                    fail++;
                    continue;
                }
                if (model.MachineHour == -1)
                {
                    failIDs = UtilBussinessService.str(failIDs, data.Value<string>("CompletionOrderID"));
                    msg = UtilBussinessService.str(msg, "机器工时格式异常！");
                    fail++;
                    continue;
                }
                if (model.UnMachineHour == -1)
                {
                    failIDs = UtilBussinessService.str(failIDs, data.Value<string>("CompletionOrderID"));
                    msg = UtilBussinessService.str(msg, "无效机器工时格式异常！");
                    fail++;
                    continue;
                }

                model.Status = Framework.SystemID + "0201213000029";
                model.Type = Framework.SystemID + "02012130000A0";
                model.Comments = data.Value<string>("Comments");

                if (model.ScrappedQuantity > model.FinProQuantity)
                {
                    failIDs = UtilBussinessService.str(failIDs, data.Value<string>("CompletionOrderID"));
                    msg = UtilBussinessService.str(msg, "报废量大于完工量！");
                    fail++;
                    continue;
                }

                //if (model.FinProQuantity + Task.FinishQuantity > Task.DispatchQuantity)
                //{
                //    failIDs = UtilBussinessService.str(failIDs, data.Value<string>("CompletionOrderID"));
                //    msg = UtilBussinessService.str(msg, "累計報工量(數量)> 任務單分派量！");
                //    fail++;
                //    continue;
                //}

                if (model.FinProQuantity + Task.FinishQuantity < 0)
                {
                    failIDs = UtilBussinessService.str(failIDs, data.Value<string>("CompletionOrderID"));
                    msg = UtilBussinessService.str(msg, "累計報工量(數量) < 0！");
                    fail++;
                    continue;
                }

                SYS_DocumentTypeSetting Dco = SYS_DocumentTypeSettingService.get(model.DTSID);
                if (Dco == null)
                {
                    failIDs = UtilBussinessService.str(failIDs, data.Value<string>("CompletionOrderID"));
                    msg = UtilBussinessService.str(msg, "单据设定流水号错误");
                    fail++;
                    continue;
                }

                string AutoNumberID = data.Value<string>("AutoNumberID");
                /*当保存时,如果完工单号已存在，则自动获取下一完工单号*/
                /*SAM 2017年9月13日16:46:44*/
                while (SFC_CompletionOrderService.CheckCode(model.CompletionNo))
                {
                    AutoNumberID = null;
                    model.CompletionNo = UtilBussinessService.GetDocumentAutoNumber(userid, model.DTSID, model.Date.ToString(), ref AutoNumberID);
                }


                if (SFC_CompletionOrderService.insert(userid, model))
                {
                    UtilBussinessService.UpdateDocumentAutoNumber(userid, AutoNumberID);
                    success++;
                }
                else
                {
                    failIDs = UtilBussinessService.str(failIDs, data.Value<string>("CompletionResourceID"));
                    fail++;
                }

            }
            return new { success = success, fail = fail, failIDs = failIDs, msg = msg };
        }

        /// <summary>
        /// 完工单的更新
        /// SAM 2017年7月20日11:24:18
        /// </summary>
        /// <param name="Token"></param>
        /// <param name="jArray"></param>
        /// <returns></returns>
        public static object Sfc00007update(string Token, JArray jArray)
        {
            int success = 0;
            int fail = 0;
            string failIDs = null;
            string msg = null;
            string userid = UtilBussinessService.detoken(Token);
            JObject data = null;
            SFC_CompletionOrder model = null;
            for (int i = 0; i < jArray.Count; i++)
            {
                data = (JObject)jArray[i];
                model = SFC_CompletionOrderService.get(data.Value<string>("CompletionOrderID"));
                if (model.Status == Framework.SystemID + "020121300002A")
                {
                    failIDs = UtilBussinessService.str(failIDs, data.Value<string>("CompletionResourceID"));
                    msg = UtilBussinessService.str(msg, "完工单处于不可编辑状态！");
                    fail++;
                    continue;
                }

                SFC_TaskDispatch Task = SFC_TaskDispatchService.get(data.Value<string>("TaskDispatchID"));
                if (Task == null)
                {
                    failIDs = UtilBussinessService.str(failIDs, data.Value<string>("CompletionResourceID"));
                    msg = UtilBussinessService.str(msg, "任务单信息不存在！");
                    fail++;
                    continue;
                }

                if (!string.IsNullOrWhiteSpace(data.Value<string>("NextID")))//如果存在一下站制程或者工序
                {
                    string NextID = data.Value<string>("NextID");
                    if (NextID.StartsWith(Framework.SystemID + "083")) //如果是制程
                    {
                        SFC_FabMoProcess FabMoProcess = SFC_FabMoProcessService.get(NextID);
                        if (FabMoProcess != null)
                        {
                            model.NextFabMoProcessID = NextID;
                            model.NextProcessID = FabMoProcess.ProcessID;
                        }
                    }
                    else if (NextID.StartsWith(Framework.SystemID + "087")) //如果是工序
                    {
                        SFC_FabMoOperation FabMoOperation = SFC_FabMoOperationService.get(NextID);
                        if (FabMoOperation != null)
                        {
                            model.NextFabMoProcessID = FabMoOperation.FabMoProcessID;
                            model.NextFabMoOperationID = FabMoOperation.FabMoOperationID;
                            model.NextOperationID = FabMoOperation.OperationID;
                            SFC_FabMoProcess FabMoProcess = SFC_FabMoProcessService.get(FabMoOperation.FabMoProcessID);
                            if (FabMoProcess != null)
                            {
                                model.NextProcessID = FabMoProcess.ProcessID;
                            }
                        }
                    }
                }

                model.Date = data.Value<DateTime>("Date");
                model.TaskDispatchID = data.Value<string>("TaskDispatchID");
                model.FabricatedMotherID = Task.FabricatedMotherID;
                model.FabMoProcessID = Task.FabMoProcessID;
                model.FabMoOperationID = Task.FabMoOperationID;
                model.ItemID = Task.ItemID;
                model.ProcessID = Task.ProcessID;
                model.OperationID = Task.OperationID;
                model.IsIF = Task.IsIP;
                model.FinProQuantity = data.Value<decimal>("FinProQuantity");
                model.ScrappedQuantity = data.Value<decimal>("ScrappedQuantity");
                model.DifferenceQuantity = data.Value<decimal>("DifferenceQuantity");
                model.RepairQuantity = data.Value<decimal>("RepairQuantity");
                model.LaborHour = UtilBussinessService.StrConversionHour(data.Value<string>("LaborHour"));
                model.MachineHour = UtilBussinessService.StrConversionHour(data.Value<string>("MachineHour"));
                model.UnLaborHour = UtilBussinessService.StrConversionHour(data.Value<string>("UnLaborHour"));
                model.UnMachineHour = UtilBussinessService.StrConversionHour(data.Value<string>("UnMachineHour"));
                if (model.LaborHour == -1)
                {
                    failIDs = UtilBussinessService.str(failIDs, data.Value<string>("CompletionOrderID"));
                    msg = UtilBussinessService.str(msg, "人工工时格式异常！");
                    fail++;
                    continue;
                }
                if (model.UnLaborHour == -1)
                {
                    failIDs = UtilBussinessService.str(failIDs, data.Value<string>("CompletionOrderID"));
                    msg = UtilBussinessService.str(msg, "无效人工工时格式异常！");
                    fail++;
                    continue;
                }
                if (model.MachineHour == -1)
                {
                    failIDs = UtilBussinessService.str(failIDs, data.Value<string>("CompletionOrderID"));
                    msg = UtilBussinessService.str(msg, "机器工时格式异常！");
                    fail++;
                    continue;
                }
                if (model.UnMachineHour == -1)
                {
                    failIDs = UtilBussinessService.str(failIDs, data.Value<string>("CompletionOrderID"));
                    msg = UtilBussinessService.str(msg, "无效机器工时格式异常！");
                    fail++;
                    continue;
                }
                model.Comments = data.Value<string>("Comments");


                if (model.ScrappedQuantity > model.FinProQuantity)
                {
                    failIDs = UtilBussinessService.str(failIDs, data.Value<string>("CompletionOrderID"));
                    msg = UtilBussinessService.str(msg, "报废量大于完工量！");
                    fail++;
                    continue;
                }

                //if (model.FinProQuantity + Task.FinishQuantity > Task.DispatchQuantity)
                //{
                //    failIDs = UtilBussinessService.str(failIDs, data.Value<string>("CompletionOrderID"));
                //    msg = UtilBussinessService.str(msg, "累計報工量(數量)> 任務單分派量！");
                //    fail++;
                //    continue;
                //}

                if (model.FinProQuantity + Task.FinishQuantity < 0)
                {
                    failIDs = UtilBussinessService.str(failIDs, data.Value<string>("CompletionOrderID"));
                    msg = UtilBussinessService.str(msg, "累計報工量(數量) < 0！");
                    fail++;
                    continue;
                }

                if (SFC_CompletionOrderService.update(userid, model))
                    success++;
                else
                {
                    failIDs = UtilBussinessService.str(failIDs, data.Value<string>("CompletionOrderID"));
                    fail++;
                }

            }
            return new { success = success, fail = fail, failIDs = failIDs, msg = msg };
        }

        /// <summary>
        /// 完工单的删除
        /// SAM 2017年7月20日11:23:57
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public static object Sfc00007Delete(JObject request)
        {
            try
            {
                string Token = request.Value<string>("Token");
                string userID = UniversalService.detoken(Token);

                SFC_CompletionOrder model = SFC_CompletionOrderService.get(request.Value<string>("CompletionOrderID"));
                if (model == null)
                    return new { status = "410", msg = "不存在完工单信息！" };

                model.Status = Framework.SystemID + "0201213000003";

                if (SFC_CompletionOrderService.update(userID, model))
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
        /// 完工單回報作業-資源報工列表
        /// SAM 2017年7月19日17:34:01
        /// </summary>
        /// <param name="CompletionOrderID"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <returns></returns>
        public static object Sfc00007GetResourceReportingList(string CompletionOrderID, int page, int rows)
        {
            int count = 0;
            return UtilBussinessService.getPaginationModel(SFC_CompletionResourceService.Sfc00007GetResourceReportingList(CompletionOrderID, page, rows, ref count), count);
        }

        /// <summary>
        /// 完工單回報作業-資源報工设备/人工开窗
        /// SAM 2017年7月20日10:37:53
        /// </summary>
        /// <param name="TaskDispatchID"></param>
        /// <param name="ResourceType"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <returns></returns>
        public static object GetMachineOrManList(string TaskDispatchID, string ResourceType, int page, int rows)
        {
            int count = 0;
            IList<Hashtable> result = null;
            if (ResourceType == "L")
                result = SYS_MESUserService.GetMachineOrManList(TaskDispatchID, page, rows, ref count);
            else if (ResourceType == "M")
                result = EMS_EquipmentService.GetMachineOrManList(TaskDispatchID, page, rows, ref count);

            return UtilBussinessService.getPaginationModel(result, count);
        }

        /// <summary>
        /// 完工單回報作業-資源報工新增
        /// SAM 2017年7月19日23:52:22
        /// </summary>
        /// <param name="Token"></param>
        /// <param name="jArray"></param>
        /// <returns></returns>
        public static object Sfc00007ResourceReportinginsert(string Token, JArray jArray)
        {
            int success = 0;
            int fail = 0;
            string failIDs = null;
            string msg = null;
            string userid = UtilBussinessService.detoken(Token);
            JObject data = null;
            int? Hour = null;
            SFC_CompletionResource model = null;
            for (int i = 0; i < jArray.Count; i++)
            {
                data = (JObject)jArray[i];

                model = new SFC_CompletionResource();
                model.CompletionResourceID = UniversalService.GetSerialNumber("SFC_CompletionResource");
                model.CompletionOrderID = data.Value<string>("CompletionOrderID");
                model.Sequence = string.IsNullOrWhiteSpace(data.Value<string>("Sequence")) ? 0 : data.Value<int>("Sequence");
                model.ResourceClassID = data.Value<string>("ResourceClassID");
                model.EquipmentID = data.Value<string>("EquipmentID");
                Hour = UtilBussinessService.StrConversionHour(data.Value<string>("Hour"));
                if (Hour == null)
                {
                    failIDs = UtilBussinessService.str(failIDs, data.Value<string>("CompletionResourceID"));
                    msg = UtilBussinessService.str(msg, "工时格式错误");
                    fail++;
                    continue;
                }
                else
                {
                    model.Hour = (int)Hour;
                }
                model.Status = Framework.SystemID + "0201213000001";
                model.Comments = data.Value<string>("Comments");
                if (SFC_CompletionResourceService.insert(userid, model))
                {
                    int HourNum = SFC_CompletionResourceService.GetSum(model.ResourceClassID, model.CompletionOrderID);
                    SFC_CompletionOrder Com = SFC_CompletionOrderService.get(model.CompletionOrderID);
                    if (Com != null)
                    {
                        if (model.ResourceClassID == Framework.SystemID + "0201213000047")
                            Com.LaborHour = HourNum;
                        else
                            Com.MachineHour = HourNum;
                        SFC_CompletionOrderService.update(userid, Com);
                    }
                    success++;
                }
                else
                {
                    failIDs = UtilBussinessService.str(failIDs, data.Value<string>("CompletionResourceID"));
                    fail++;
                }

            }
            return new { success = success, fail = fail, failIDs = failIDs, msg = msg };
        }

        /// <summary>
        /// 完工單回報作業-资源报工更新
        /// SAM 2017年7月19日23:52:43
        /// </summary>
        /// <param name="Token"></param>
        /// <param name="jArray"></param>
        /// <returns></returns>
        public static object Sfc00007ResourceReportingupdate(string Token, JArray jArray)
        {
            int success = 0;
            int fail = 0;
            string failIDs = null;
            string msg = null;
            string userid = UtilBussinessService.detoken(Token);
            JObject data = null;
            int? Hour = null;
            SFC_CompletionResource model = null;
            for (int i = 0; i < jArray.Count; i++)
            {
                data = (JObject)jArray[i];
                model = SFC_CompletionResourceService.get(data.Value<string>("CompletionResourceID"));
                model.EquipmentID = data.Value<string>("EquipmentID");
                Hour = UtilBussinessService.StrConversionHour(data.Value<string>("Hour"));
                if (Hour == null)
                {
                    failIDs = UtilBussinessService.str(failIDs, data.Value<string>("CompletionResourceID"));
                    msg = UtilBussinessService.str(msg, "工时格式错误");
                    fail++;
                    continue;
                }
                else
                    model.Hour = (int)Hour;
                model.ResourceClassID = data.Value<string>("ResourceClassID");
                model.Comments = data.Value<string>("Comments");
                if (SFC_CompletionResourceService.update(userid, model))
                {
                    int HourNum = SFC_CompletionResourceService.GetSum(model.ResourceClassID, model.CompletionOrderID);
                    SFC_CompletionOrder Com = SFC_CompletionOrderService.get(model.CompletionOrderID);
                    if (Com != null)
                    {
                        if (model.ResourceClassID == Framework.SystemID + "0201213000047")
                            Com.LaborHour = HourNum;
                        else
                            Com.MachineHour = HourNum;
                        SFC_CompletionOrderService.update(userid, Com);
                    }
                    success++;
                }
                else
                {
                    failIDs = UtilBussinessService.str(failIDs, data.Value<string>("CompletionResourceID"));
                    fail++;
                }

            }
            return new { success = success, fail = fail, failIDs = failIDs, msg = msg };
        }

        /// <summary>
        /// 完工單回報作業-資源報工删除
        /// SAM 2017年7月19日23:43:00
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public static object Sfc00007ResourceReportingDelete(JObject request)
        {
            try
            {
                string Token = request.Value<string>("Token");
                string userID = UniversalService.detoken(Token);

                SFC_CompletionResource model = SFC_CompletionResourceService.get(request.Value<string>("CompletionResourceID"));
                if (model == null)
                    return new { status = "410", msg = "不存在资源报工信息！" };

                model.Status = Framework.SystemID + "0201213000003";

                if (SFC_CompletionResourceService.update(userID, model))
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
        /// 完工單回報作業-異常數量列表
        /// SAM 2017年7月19日23:36:12
        /// </summary>
        /// <param name="CompletionOrderID"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <returns></returns>
        public static object Sfc00007GetUnusualQtyList(string CompletionOrderID, int page, int rows)
        {
            int count = 0;
            return UtilBussinessService.getPaginationModel(SFC_AbnormalQuantityService.Sfc00007GetUnusualQtyList(CompletionOrderID, page, rows, ref count), count);
        }

        /// <summary>
        /// 完工單回報作業-異常數量新增
        /// SAM 2017年7月19日23:52:22
        /// </summary>
        /// <param name="Token"></param>
        /// <param name="jArray"></param>
        /// <returns></returns>
        public static object Sfc00007UnusualQtyinsert(string Token, JArray jArray)
        {
            int success = 0;
            int fail = 0;
            string failIDs = null;
            string msg = null;
            string userid = UtilBussinessService.detoken(Token);
            JObject data = null;
            SFC_AbnormalQuantity model = null;

            for (int i = 0; i < jArray.Count; i++)
            {
                data = (JObject)jArray[i];

                SFC_CompletionOrder CompletionOrder = SFC_CompletionOrderService.get(data.Value<string>("CompletionOrderID"));
                if (CompletionOrder == null)
                {
                    failIDs = UtilBussinessService.str(failIDs, data.Value<string>("AbnormalQuantityID"));
                    msg = UtilBussinessService.str(msg, "不存在的完工单信息");
                    fail++;
                    continue;
                }

                model = new SFC_AbnormalQuantity();
                model.AbnormalQuantityID = UniversalService.GetSerialNumber("SFC_AbnormalQuantity");
                model.CompletionOrderID = data.Value<string>("CompletionOrderID");
                model.Sequence = string.IsNullOrWhiteSpace(data.Value<string>("Sequence")) ? 0 : data.Value<int>("Sequence");
                model.Type = data.Value<string>("Type");
                model.ReasonID = data.Value<string>("ReasonID");
                model.Quantity = data.Value<decimal>("Quantity");
                model.Status = Framework.SystemID + "0201213000001";
                model.Comments = data.Value<string>("Comments");

                if (model.Type == Framework.SystemID + "020121300009B")
                {
                    decimal Quantity = SFC_AbnormalQuantityService.GetSum(model.Type, model.CompletionOrderID);
                    if (CompletionOrder.FinProQuantity < Quantity + model.Quantity)
                    {
                        failIDs = UtilBussinessService.str(failIDs, data.Value<string>("AbnormalQuantityID"));
                        msg = UtilBussinessService.str(msg, "报废量大于完工量！");
                        fail++;
                        continue;
                    }
                }

                if (SFC_AbnormalQuantityService.insert(userid, model))
                {
                    //回写完工单
                    decimal Quantity = SFC_AbnormalQuantityService.GetSum(model.Type, model.CompletionOrderID);
                    if (model.Type == Framework.SystemID + "020121300009B")
                        CompletionOrder.ScrappedQuantity = Quantity;
                    else if (model.Type == Framework.SystemID + "020121300009C")
                        CompletionOrder.DifferenceQuantity = Quantity;
                    else if (model.Type == Framework.SystemID + "020121300009D")
                        CompletionOrder.RepairQuantity = Quantity;
                    SFC_CompletionOrderService.update(userid, CompletionOrder);
                    success++;
                }

                else
                {
                    failIDs = UtilBussinessService.str(failIDs, data.Value<string>("AbnormalQuantityID"));
                    fail++;
                }
            }
            return new { success = success, fail = fail, failIDs = failIDs, msg = msg };
        }
        /// <summary>
        ///  完工單回報作業-異常數量更新
        ///  SAM 2017年7月19日23:56:58
        /// </summary>
        /// <param name="Token"></param>
        /// <param name="jArray"></param>
        /// <returns></returns>
        public static object Sfc00007UnusualQtyupdate(string Token, JArray jArray)
        {
            int success = 0;
            int fail = 0;
            string failIDs = null;
            string msg = null;
            string userid = UtilBussinessService.detoken(Token);
            JObject data = null;
            SFC_AbnormalQuantity model = null;
            for (int i = 0; i < jArray.Count; i++)
            {
                data = (JObject)jArray[i];
                model = SFC_AbnormalQuantityService.get(data.Value<string>("AbnormalQuantityID"));

                SFC_CompletionOrder CompletionOrder = SFC_CompletionOrderService.get(data.Value<string>("CompletionOrderID"));
                if (CompletionOrder == null)
                {
                    failIDs = UtilBussinessService.str(failIDs, data.Value<string>("AbnormalQuantityID"));
                    msg = UtilBussinessService.str(msg, "不存在的完工单信息");
                    fail++;
                    continue;
                }

                if (model.Type == Framework.SystemID + "020121300009B")
                {
                    decimal Quantity = SFC_AbnormalQuantityService.GetSum(model.Type, model.CompletionOrderID);
                    if (CompletionOrder.FinProQuantity < Quantity + data.Value<decimal>("Quantity") - model.Quantity)
                    {
                        failIDs = UtilBussinessService.str(failIDs, data.Value<string>("AbnormalQuantityID"));
                        msg = UtilBussinessService.str(msg, "报废量大于完工量！");
                        fail++;
                        continue;
                    }
                }

                model.ReasonID = data.Value<string>("ReasonID");
                model.Quantity = data.Value<decimal>("Quantity");
                model.Comments = data.Value<string>("Comments");

                if (SFC_AbnormalQuantityService.update(userid, model))
                {
                    decimal Quantity = SFC_AbnormalQuantityService.GetSum(model.Type, model.CompletionOrderID);
                    if (model.Type == Framework.SystemID + "020121300009B")
                        CompletionOrder.ScrappedQuantity = Quantity;
                    else if (model.Type == Framework.SystemID + "020121300009C")
                        CompletionOrder.DifferenceQuantity = Quantity;
                    else if (model.Type == Framework.SystemID + "020121300009D")
                        CompletionOrder.RepairQuantity = Quantity;
                    //CompletionOrder.ScrappedQuantity = SFC_AbnormalQuantityService.GetSum(model.Type, model.CompletionOrderID);
                    SFC_CompletionOrderService.update(userid, CompletionOrder);
                    success++;
                }
                else
                {
                    failIDs = UtilBussinessService.str(failIDs, data.Value<string>("AbnormalQuantityID"));
                    fail++;
                }

            }
            return new { success = success, fail = fail, failIDs = failIDs, msg = msg };
        }


        /// <summary>
        /// 完工單回報作業-異常數量删除
        /// SAM 2017年7月19日23:43:52
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public static object Sfc00007UnusualQtyDelete(JObject request)
        {
            try
            {
                string Token = request.Value<string>("Token");
                string userID = UniversalService.detoken(Token);

                SFC_AbnormalQuantity model = SFC_AbnormalQuantityService.get(request.Value<string>("AbnormalQuantityID"));
                if (model == null)
                    return new { status = "410", msg = "不存在異常數量信息！" };

                model.Status = Framework.SystemID + "0201213000003";

                if (SFC_AbnormalQuantityService.update(userID, model))
                {
                    SFC_CompletionOrder CompletionOrder = SFC_CompletionOrderService.get(model.CompletionOrderID);
                    decimal Quantity = SFC_AbnormalQuantityService.GetSum(model.Type, model.CompletionOrderID);
                    if (model.Type == Framework.SystemID + "020121300009B")
                        CompletionOrder.ScrappedQuantity = Quantity;
                    else if (model.Type == Framework.SystemID + "020121300009C")
                        CompletionOrder.DifferenceQuantity = Quantity;
                    else if (model.Type == Framework.SystemID + "020121300009D")
                        CompletionOrder.RepairQuantity = Quantity;
                    CompletionOrder.ScrappedQuantity = SFC_AbnormalQuantityService.GetSum(model.Type, model.CompletionOrderID);
                    return new { status = "200", msg = "删除成功" };
                }
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
        /// 完工單回報作業-無效工時列表
        /// SAM 2017年7月19日23:36:34
        /// </summary>
        /// <param name="CompletionOrderID"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <returns></returns>
        public static object Sfc00007GetUnusualHourList(string CompletionOrderID, int page, int rows)
        {
            int count = 0;
            return UtilBussinessService.getPaginationModel(SFC_AbnormalHourService.Sfc00007GetUnusualHourList(CompletionOrderID, page, rows, ref count), count);
        }
        /// <summary>
        /// 完工單回報作業-無效工時新增
        /// SAM 2017年7月19日23:56:50
        /// </summary>
        /// <param name="Token"></param>
        /// <param name="jArray"></param>
        /// <returns></returns>
        public static object Sfc00007UnusualHourinsert(string Token, JArray jArray)
        {
            int success = 0;
            int fail = 0;
            string failIDs = null;
            string msg = null;
            string userid = UtilBussinessService.detoken(Token);
            JObject data = null;
            int? Hour = null;
            SFC_AbnormalHour model = null;

            for (int i = 0; i < jArray.Count; i++)
            {
                data = (JObject)jArray[i];
                SFC_CompletionOrder CompletionOrder = SFC_CompletionOrderService.get(data.Value<string>("CompletionOrderID"));
                if (CompletionOrder == null)
                {
                    failIDs = UtilBussinessService.str(failIDs, data.Value<string>("AbnormalQuantityID"));
                    msg = UtilBussinessService.str(msg, "不存在的完工单信息");
                    fail++;
                    continue;
                }

                model = new SFC_AbnormalHour();
                model.AbnormalHourID = UniversalService.GetSerialNumber("SFC_AbnormalHour");

                model.CompletionOrderID = data.Value<string>("CompletionOrderID");
                model.Sequence = string.IsNullOrWhiteSpace(data.Value<string>("Sequence")) ? 0 : data.Value<int>("Sequence");
                model.Comments = data.Value<string>("Comments");
                model.Type = data.Value<string>("Type");
                model.ReasonID = data.Value<string>("ReasonID");
                model.GroupID = data.Value<string>("GroupID");
                Hour = UtilBussinessService.StrConversionHour(data.Value<string>("Hour"));
                if (Hour == null)
                {
                    failIDs = UtilBussinessService.str(failIDs, data.Value<string>("AbnormalQuantityID"));
                    msg = UtilBussinessService.str(msg, "工时格式错误");
                    fail++;
                    continue;
                }
                else
                    model.Hour = (int)Hour;
                model.Status = Framework.SystemID + "0201213000001";
                model.Comments = data.Value<string>("Comments");

                if (SFC_AbnormalHourService.insert(userid, model))
                {
                    //回写完工单
                    if (model.Type == Framework.SystemID + "020121300009E")//机器
                        CompletionOrder.UnMachineHour = SFC_AbnormalHourService.GetSum(model.Type, model.CompletionOrderID);
                    else if (model.Type == Framework.SystemID + "020121300009F")//人工
                        CompletionOrder.UnLaborHour = SFC_AbnormalHourService.GetSum(model.Type, model.CompletionOrderID);
                    SFC_CompletionOrderService.update(userid, CompletionOrder);
                    success++;
                }
                else
                {
                    failIDs = UtilBussinessService.str(failIDs, data.Value<string>("AbnormalHourID"));
                    fail++;
                }

            }
            return new { success = success, fail = fail, failIDs = failIDs, msg = msg };
        }

        /// <summary>
        ///  完工單回報作業-無效工時更新
        ///  SAM 2017年7月19日23:56:34
        /// </summary>
        /// <param name="Token"></param>
        /// <param name="jArray"></param>
        /// <returns></returns>
        public static object Sfc00007UnusualHourupdate(string Token, JArray jArray)
        {
            int success = 0;
            int fail = 0;
            string failIDs = null;
            string msg = null;
            string userid = UtilBussinessService.detoken(Token);
            JObject data = null;
            SFC_AbnormalHour model = null;
            int? NewHour = null;
            for (int i = 0; i < jArray.Count; i++)
            {
                data = (JObject)jArray[i];
                SFC_CompletionOrder CompletionOrder = SFC_CompletionOrderService.get(data.Value<string>("CompletionOrderID"));
                if (CompletionOrder == null)
                {
                    failIDs = UtilBussinessService.str(failIDs, data.Value<string>("AbnormalQuantityID"));
                    msg = UtilBussinessService.str(msg, "不存在的完工单信息");
                    fail++;
                    continue;
                }

                model = SFC_AbnormalHourService.get(data.Value<string>("AbnormalHourID"));

                NewHour = UtilBussinessService.StrConversionHour(data.Value<string>("Hour"));
                if (NewHour == null)
                {
                    failIDs = UtilBussinessService.str(failIDs, data.Value<string>("AbnormalQuantityID"));
                    msg = UtilBussinessService.str(msg, "工时格式错误");
                    fail++;
                    continue;
                }

                //if (model.Type == Framework.SystemID + "020121300009E")//机器
                //    CompletionOrder.UnMachineHour = CompletionOrder.UnMachineHour - model.Hour + NewHour;
                //else if (model.Type == Framework.SystemID + "020121300009F")//人工
                //    CompletionOrder.UnLaborHour = CompletionOrder.UnLaborHour - model.Hour + NewHour;

                model.ReasonID = data.Value<string>("ReasonID");
                model.GroupID = data.Value<string>("GroupID");
                model.Hour = (int)NewHour;
                model.Comments = data.Value<string>("Comments");
                if (SFC_AbnormalHourService.update(userid, model))
                {
                    //回写完工单                 
                    if (model.Type == Framework.SystemID + "020121300009E")//机器
                        CompletionOrder.UnMachineHour = SFC_AbnormalHourService.GetSum(model.Type, model.CompletionOrderID);
                    else if (model.Type == Framework.SystemID + "020121300009F")//人工
                        CompletionOrder.UnLaborHour = SFC_AbnormalHourService.GetSum(model.Type, model.CompletionOrderID);
                    SFC_CompletionOrderService.update(userid, CompletionOrder);
                    success++;
                }
                else
                {
                    failIDs = UtilBussinessService.str(failIDs, data.Value<string>("AbnormalHourID"));
                    fail++;
                }

            }
            return new { success = success, fail = fail, failIDs = failIDs, msg = msg };
        }

        /// <summary>
        ///  完工單回報作業-無效工時删除
        ///  SAM 2017年7月19日23:44:18
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public static object Sfc00007UnusualHourDelete(JObject request)
        {
            try
            {
                string Token = request.Value<string>("Token");
                string userID = UniversalService.detoken(Token);

                SFC_AbnormalHour model = SFC_AbnormalHourService.get(request.Value<string>("AbnormalHourID"));
                if (model == null)
                    return new { status = "410", msg = "不存在異常工時信息！" };
                model.Status = Framework.SystemID + "0201213000003";

                if (SFC_AbnormalHourService.update(userID, model))
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
        /// 完工單回報作業-批號列表
        /// SAM 2017年7月19日23:36:45
        /// </summary>
        /// <param name="CompletionOrderID"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <returns></returns>
        public static object Sfc00007GetLotList(string CompletionOrderID, int page, int rows)
        {
            int count = 0;
            return UtilBussinessService.getPaginationModel(SFC_BatchAttributeService.Sfc00007GetLotList(CompletionOrderID, page, rows, ref count), count);
        }

        /// <summary>
        /// 完工單回報作業-批號新增
        /// SAM 2017年7月19日23:56:18
        /// </summary>
        /// <param name="Token"></param>
        /// <param name="jArray"></param>
        /// <returns></returns>
        public static object Sfc00007Lotinsert(string Token, JArray jArray)
        {
            int success = 0;
            int fail = 0;
            string failIDs = null;
            string msg = null;
            string userid = UtilBussinessService.detoken(Token);
            JObject data = null;
            SFC_BatchAttribute model = null;
            for (int i = 0; i < jArray.Count; i++)
            {
                data = (JObject)jArray[i];
                model = new SFC_BatchAttribute();
                model.BatchAttributeID = UniversalService.GetSerialNumber("SFC_BatchAttribute");
                model.CompletionOrderID = data.Value<string>("CompletionOrderID");
                model.Sequence = string.IsNullOrWhiteSpace(data.Value<string>("Sequence")) ? 0 : data.Value<int>("Sequence");
                model.BatchNo = data.Value<string>("BatchNo");
                model.EffectDate = data.Value<DateTime>("EffectDate");
                model.Quantity = data.Value<decimal>("Quantity");
                model.AutoNumberRecordID = data.Value<string>("AutoNumberRecordID");
                model.Status = Framework.SystemID + "0201213000001";
                model.Comments = data.Value<string>("Comments");
                if (SFC_BatchAttributeService.insert(userid, model))
                {
                    //更新批号
                    SYS_AutoNumberRecord Auto = SYS_AutoNumberRecordService.get(model.AutoNumberRecordID);
                    if (Auto != null)
                    {
                        Auto.Num = Auto.Num + 1;
                        SYS_AutoNumberRecordService.update(userid, Auto);
                    }
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
        /// 完工單回報作業-批號更新
        /// SAM 2017年7月19日23:56:09
        /// </summary>
        /// <param name="Token"></param>
        /// <param name="jArray"></param>
        /// <returns></returns>
        public static object Sfc00007Lotupdate(string Token, JArray jArray)
        {
            int success = 0;
            int fail = 0;
            string failIDs = null;
            string msg = null;
            string userid = UtilBussinessService.detoken(Token);
            JObject data = null;
            SFC_BatchAttribute model = null;
            for (int i = 0; i < jArray.Count; i++)
            {
                data = (JObject)jArray[i];
                model = SFC_BatchAttributeService.get(data.Value<string>("BatchAttributeID"));
                model.EffectDate = data.Value<DateTime>("EffectDate");
                model.Quantity = data.Value<decimal>("Quantity");
                model.Comments = data.Value<string>("Comments");
                if (SFC_BatchAttributeService.update(userid, model))
                    success++;
                else
                {
                    failIDs = UtilBussinessService.str(failIDs, data.Value<string>("BatchAttributeID"));
                    fail++;
                }

            }
            return new { success = success, fail = fail, failIDs = failIDs, msg = msg };
        }

        /// <summary>
        ///  完工單回報作業-批號删除
        ///  SAM 2017年7月19日23:44:49
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public static object Sfc00007LotDelete(JObject request)
        {
            try
            {
                string Token = request.Value<string>("Token");
                string userID = UniversalService.detoken(Token);

                SFC_BatchAttribute model = SFC_BatchAttributeService.get(request.Value<string>("BatchAttributeID"));
                if (model == null)
                    return new { status = "410", msg = "不存在批號信息！" };

                model.Status = Framework.SystemID + "0201213000003";

                if (SFC_BatchAttributeService.update(userID, model))
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
        /// 完工單回報作業-批號-屬性列表
        /// SAM 2017年7月20日00:43:11
        /// </summary>
        /// <param name="BatchAttributeID"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <returns></returns>
        public static object Sfc00007GetLotAttributeList(string Token, string BatchAttributeID, int page, int rows)
        {
            string userid = UtilBussinessService.detoken(Token);
            int count = 0;
            IList<Hashtable> result = SFC_BatchAttributeDetailsService.Sfc00007GetLotAttributeList(BatchAttributeID, page, rows, ref count);
            if (result.Count == 0)
            {
                SFC_BatchAttribute batch = SFC_BatchAttributeService.get(BatchAttributeID);
                if (batch == null)
                    return UtilBussinessService.getPaginationModel(result, count);
                SFC_CompletionOrder Commodel = SFC_CompletionOrderService.get(batch.CompletionOrderID);
                if (Commodel == null)
                    return UtilBussinessService.getPaginationModel(result, count);
                SFC_TaskDispatch Task = SFC_TaskDispatchService.get(Commodel.TaskDispatchID);
                if (Task == null)
                    return UtilBussinessService.getPaginationModel(result, count);

                IList<Hashtable> Attribute = SYS_ItemAttributesService.GetAttributeGetList(Task.ItemID);
                if (Attribute != null)
                {
                    int seq = 1;
                    SFC_BatchAttributeDetails model = null;
                    foreach (Hashtable item in Attribute)
                    {
                        model = new SFC_BatchAttributeDetails();
                        model.BatchAttributeDetailID = UniversalService.GetSerialNumber("SFC_BatchAttributeDetails");
                        model.CompletionOrderID = Commodel.CompletionOrderID;
                        model.BatchAttributeID = BatchAttributeID;
                        model.Sequence = seq;
                        model.AttributeID = item["AttributeID"].ToString();
                        model.Status = Framework.SystemID + "0201213000001";
                        model.Comments = item["Comments"].ToString();
                        SFC_BatchAttributeDetailsService.insert(userid, model);
                        seq++;
                    }
                }
                result = SFC_BatchAttributeDetailsService.Sfc00007GetLotAttributeList(BatchAttributeID, page, rows, ref count);
            }
            return UtilBussinessService.getPaginationModel(result, count);
        }

        /// <summary>
        /// 根据料品属性获取料品资料值
        /// SAM 2017年7月20日15:33:16
        /// </summary>
        /// <param name="token"></param>
        /// <param name="AttributeID"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <returns></returns>
        public static object Sfc00007GetAttributeLList(string token, string AttributeID, int page, int rows)
        {
            int count = 0;
            return UtilBussinessService.getPaginationModel(SYS_ParameterService.Sfc00007GetAttributeLList(AttributeID, page, rows, ref count), count);
        }

        /// <summary>
        /// 完工單回報作業-批號-屬性更新
        /// SAM 2017年7月20日00:44:27
        /// </summary>
        /// <param name="Token"></param>
        /// <param name="jArray"></param>
        /// <returns></returns>
        public static object Sfc00007LotAttributeupdate(string Token, JArray jArray)
        {
            int success = 0;
            int fail = 0;
            string failIDs = null;
            string msg = null;
            string userid = UtilBussinessService.detoken(Token);
            JObject data = null;
            SFC_BatchAttributeDetails model = null;
            for (int i = 0; i < jArray.Count; i++)
            {
                data = (JObject)jArray[i];
                model = SFC_BatchAttributeDetailsService.get(data.Value<string>("BatchAttributeDetailID"));
                SFC_CompletionOrder Commodel = SFC_CompletionOrderService.get(model.CompletionOrderID);
                if (Commodel.Status != Framework.SystemID + "0201213000029")
                {
                    failIDs = UtilBussinessService.str(failIDs, data.Value<string>("BatchAttributeDetailID"));
                    msg = UtilBussinessService.str(msg, "完工单处于非OP状态，不能修改！");
                    fail++;
                    continue;
                }
                model.AttributeValue = data.Value<string>("AttributeValue");
                model.Comments = data.Value<string>("Comments");
                if (SFC_BatchAttributeDetailsService.update(userid, model))
                    success++;
                else
                {
                    failIDs = UtilBussinessService.str(failIDs, data.Value<string>("BatchAttributeDetailID"));
                    fail++;
                }
            }
            return new { success = success, fail = fail, failIDs = failIDs, msg = msg };
        }

        /// <summary>
        /// 完工單回報作業-批號屬性—檢查批號
        /// SAM 2017年7月21日10:02:29
        /// </summary>
        /// <param name="token"></param>
        /// <param name="taskID"></param>
        /// <param name="lotNo"></param>
        /// <returns></returns>
        public static object Sfc00007LotNoChecks(string Token, string TaskID, string LotNoStr)
        {
            bool IsFail = true;
            string LotId = null;
            string LotNo = null;
            string Msg = null;
            string userid = UtilBussinessService.detoken(Token);
            SFC_TaskDispatch taskModel = SFC_TaskDispatchService.get(TaskID);
            if (taskModel == null)
            {
                IsFail = false;
                Msg = "任务单流水号错误！";
                return new { IsFail = IsFail, LotId = LotId, LotNo = LotNo, Msg = Msg };
            }

            SFC_FabricatedMother fabMother = SFC_FabricatedMotherService.get(taskModel.FabricatedMotherID);

            if (fabMother.BatchNumber != LotNoStr)
            {
                IsFail = false;
                Msg = "这并不是制令单上的批号";
                return new { IsFail = IsFail, LotId = LotId, LotNo = LotNo, Msg = Msg };
            }
            else
            {
                LotNo = LotNoStr;
                return new { IsFail = IsFail, LotId = LotId, LotNo = LotNo, Msg = Msg };
            }
        }


        /// <summary>
        /// 完工單回報作業-完工單—確認按鍵
        /// SAM 2017年7月21日10:15:07
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public static object Sfc00007CompeletedConfirm(JObject request)
        {
            string Token = request.Value<string>("Token");
            string userid = UtilBussinessService.detoken(Token);
            try
            {
                /*
                單據狀態為OP時顯示,執行確認後
            	完工數量、報廢量、差異量、寫入
            1.	寫入任務單分派資料檔完工數(累加)及制令製程(工序)檔完工量(累加) ——> FinQty 出站量
            2.	寫入任務單分派資料檔報廢量(累加)及制令製程(工序)檔報廢量(累加) ——> ScrappedQty 報廢量
            3.	寫入任務單分派資料檔差異量(累加)及製令製程(工序)檔差異量(累加) ——> DifferenceQty 差異量
            4.	寫入任務單分派資料檔返修量(累加)及製令製程(工序)檔返修量(累加) ——> RepairQty差異量
            5.	任務單單如有工序,制令制程工序檔如是最後一道工序(制程2工序6),將此工序之相關數量寫入該製令制程檔(制程2),其他(制程2工序1~5)不用寫入制程檔(制程2)
            6.	當任務單分派資料檔完工量大於等於分派量時，RC任務單分派單之狀態同時改成CL狀態。
            7.	當製令製程工序檔的完工量大於等於生產量時，制程工序狀態之狀態同時改為CL狀態。
            8.	當製令製程檔的完工量大於等於生產量時，制程狀態之狀態同時改為CL狀態。
            9.	制程檢驗註記IPFlag = N時,自動產生移轉單參考制程移轉單規格
            10.	制程驗註記IPFlag = Y時,自動產生制程檢驗單參考MES規格02 - 完工單自動產生檢驗單邏輯
            11.	如有料品特性資料必需確認料品特性資料值都必需有值
            12.	料品主檔批控參數 = Y時,需檢查批號是否有輸入, 若批號件無批號資料時，顯示訊息”此料件為批號管控件,請檢查批號資料”。
            13.	如是最後一站且沒有制程檢驗,確認後寫入相關資料後結束
                */

                //首先，根据完工单获取对应任务单，料品，制令单，工序，制程的实体，便于计算
                SFC_CompletionOrder model = SFC_CompletionOrderService.get(request.Value<string>("CompletionOrderID"));
                if (model == null)
                    return new { status = "410", msg = "不存在的完工单信息" };

                SFC_TaskDispatch TaskModel = SFC_TaskDispatchService.get(model.TaskDispatchID);             //任务单
                SYS_Items ItemModel = SYS_ItemsService.get(model.ItemID);                                   //料品
                SFC_FabricatedMother FMModel = SFC_FabricatedMotherService.get(model.FabricatedMotherID);   //制令母单
                SFC_FabMoOperation OperationModel = SFC_FabMoOperationService.get(model.FabMoOperationID);  //制令制程工序
                SFC_FabMoProcess ProcessModel = SFC_FabMoProcessService.get(model.FabMoProcessID);          //制令制程

                //11.如有料品特性資料必需確認料品特性資料值都必需有值
                if (SFC_BatchAttributeDetailsService.CheckAttributeValue(model.CompletionOrderID))
                    return new { status = "410", msg = "存在属性值为空的批号属性资料！" };

                //12.料品主檔批控參數 = Y時,需檢查批號是否有輸入, 若批號件無批號資料時，顯示訊息”此料件為批號管控件,請檢查批號資料”。
                if (ItemModel.Lot)
                {
                    //判断是否存在批号资料
                    IList<Hashtable> LotList = SFC_BatchAttributeService.Sfc00007CheckLot(model.CompletionOrderID);
                    if (LotList == null)
                        return new { status = "410", msg = "此料件為批號管控件,請檢查批號資料！" };
                }

                //将完工单状态设置成结案状态
                model.Status = Framework.SystemID + "020121300002A";
                if (SFC_CompletionOrderService.update(userid, model))
                {
                    SFC_TransferOrder Tran = null;
                    //10.制程驗註記IPFlag = Y時,自動產生制程檢驗單參考MES規格02 - 完工單自動產生檢驗單邏輯
                    string AutoNumberID = null;
                    try
                    {
                        if (model.IsIF)
                        {
                            try
                            {
                                DateTime Now = DateTime.Now;
                                QCS_InspectionDocument document = new QCS_InspectionDocument();
                                document.InspectionDocumentID = UniversalService.GetSerialNumber("QCS_InspectionDocument");
                                document.DocumentDate = Now;
                                document.ItemID = model.ItemID;
                                document.InspectionMethod = Framework.SystemID + "020121300007E";
                                document.CompletionOrderID = model.CompletionOrderID;
                                document.TaskDispatchID = model.TaskDispatchID;
                                document.FinQuantity = model.FinProQuantity;
                                document.InspectionQuantity = model.FinProQuantity;
                                document.InspectionDate = Now;
                                document.InspectionUserID = userid;
                                document.QualityControlDecision = Framework.SystemID + "0201213000091";
                                document.ScrappedQuantity = 0;
                                document.NGquantity = 0;
                                document.OKQuantity = 0;
                                document.InspectionFlag = false;
                                document.ConfirmDate = Now;
                                document.ConfirmUserID = userid;
                                document.Status = Framework.SystemID + "020121300008D";
                                document.InspectionNo = UtilBussinessService.SFC07GetDocumentAutoNumber(userid, Now.ToString("yyyy-MM-dd"), ref AutoNumberID);
                                if (!string.IsNullOrWhiteSpace(document.InspectionNo))
                                {
                                    if (QCS_InspectionDocumentService.insert(userid, document))
                                    {
                                        UtilBussinessService.UpdateDocumentAutoNumber(userid, AutoNumberID);
                                        //如果检验单据成功添加那么根据对应的料品号与检验种类获取明细设定列表，规格表说明不足新添加条件，工序与制程
                                        SYS_Items ItemModelV2 = SYS_ItemsService.get(document.ItemID);
                                        if (ItemModelV2 == null)
                                            return new { Status = "400", msg = "对应的料品代号错误" };

                                        List<QCS_StaInsSpeSetting> StaInsSpeSetting = null;
                                        //如果存在检验码，则拿去检验码的明细
                                        if (!string.IsNullOrWhiteSpace(ItemModelV2.GroupID))
                                            StaInsSpeSetting = QCS_StaInsSpeSettingService.GetListByPartID(ItemModelV2.GroupID, document.InspectionMethod, Framework.SystemID + "020121300007C", TaskModel.ProcessID, TaskModel.OperationID);
                                        //如果不存在检验码或者说检验码并没有对应明细
                                        if (StaInsSpeSetting == null)
                                            StaInsSpeSetting = QCS_StaInsSpeSettingService.GetListByPartID(document.ItemID, document.InspectionMethod, Framework.SystemID + "020121300007B", TaskModel.ProcessID, TaskModel.OperationID);
                                        //检验明细新增
                                        if (StaInsSpeSetting != null)
                                        {
                                            foreach (QCS_StaInsSpeSetting Sta in StaInsSpeSetting)
                                            {
                                                QCS_InspectionProject Sampling = QCS_InspectionProjectService.get(Sta.InspectionProjectID);
                                                QCS_InspectionDocumentDetails DetailsModel = new QCS_InspectionDocumentDetails();
                                                DetailsModel.InspectionDocumentDetailID = UniversalService.GetSerialNumber("QCS_InspectionDocumentDetails");
                                                DetailsModel.InspectionDocumentID = document.InspectionDocumentID;
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
                                                        QCS_CheckTestSettingDetails CheckDetail = QCS_CheckTestSettingDetailsService.GetDetails(Check.CheckTestSettingID, TaskModel.DiffQuantity);//获取抽检检验设定明细实体
                                                        if (CheckDetail == null)
                                                        {
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

                                                if (DetailsModel.NGquantity >= DetailsModel.ReQuantity)
                                                    DetailsModel.QualityControlDecision = Framework.SystemID + "0201213000090";
                                                else
                                                    DetailsModel.QualityControlDecision = Framework.SystemID + "0201213000091";

                                                QCS_InspectionDocumentDetailsService.insert(userid, DetailsModel);
                                            }
                                        }
                                        model.InspectionID = document.InspectionDocumentID;
                                        SFC_CompletionOrderService.update(userid, model);
                                    }
                                }
                            }
                            catch (Exception ex)
                            {
                                DataLogerService.writeerrlog(ex);
                            }
                        }
                        else// 9.制程檢驗註記IPFlag = N時,自動產生移轉單參考制程移轉單規格
                        {
                            Tran = new SFC_TransferOrder();
                            Tran.TransferOrderID = UniversalService.GetSerialNumber("SFC_TransferOrder");
                            IList<Hashtable> Type = SYS_DocumentTypeSettingService.GetTypeList(userid, Framework.SystemID + "0201213000037");
                            if (Type.Count != 0)
                                Tran.TransferNo = UtilBussinessService.GetDocumentAutoNumber(userid, Type[0]["value"].ToString(), null, ref AutoNumberID);
                            Tran.Date = DateTime.Now;
                            Tran.Sequence = 1;
                            Tran.Type = Framework.SystemID + "02012130000AE";
                            Tran.CompletionOrderID = model.CompletionOrderID;
                            Tran.TaskDispatchID = model.TaskDispatchID;
                            Tran.FabricatedMotherID = model.FabricatedMotherID;
                            Tran.FabMoProcessID = model.FabMoProcessID;
                            Tran.FabMoOperationID = model.FabMoOperationID;
                            Tran.ItemID = model.ItemID;
                            Tran.ProcessID = model.ProcessID;
                            Tran.OperationID = model.OperationID;
                            Tran.TransferQuantity = model.FinProQuantity;
                            Tran.ActualTransferQuantity = model.FinProQuantity;
                            Tran.Status = Framework.SystemID + "020121300002A";
                            Tran.NextFabMoProcessID = model.NextFabMoProcessID;
                            Tran.NextFabMoOperationID = model.NextFabMoOperationID;
                            Tran.NextProcessID = model.NextProcessID;
                            Tran.NextOperationID = model.NextOperationID;
                            if (SFC_TransferOrderService.insert(userid, Tran))
                                UtilBussinessService.UpdateDocumentAutoNumber(userid, AutoNumberID);
                            else
                                Tran = null;
                        }
                    }
                    catch (Exception ex)
                    {
                        DataLogerService.writeerrlog(ex);
                    }
                    //1.寫入任務單分派資料檔完工數(累加)及制令製程(工序)檔完工量(累加) ——> FinQty 出站量
                    TaskModel.FinishQuantity += model.FinProQuantity;
                    //2.寫入任務單分派資料檔報廢量(累加)及制令製程(工序)檔報廢量(累加) ——> ScrappedQty 報廢量
                    TaskModel.ScrapQuantity += model.ScrappedQuantity;
                    //3.寫入任務單分派資料檔差異量(累加)及製令製程(工序)檔差異量(累加) ——> DifferenceQty 差異量
                    TaskModel.DiffQuantity += model.DifferenceQuantity;
                    //4.寫入任務單分派資料檔返修量(累加)及製令製程(工序)檔返修量(累加) ——> RepairQty差異量
                    TaskModel.RepairQuantity += model.RepairQuantity;

                    //當任務單分派資料檔完工量大於等於分派量時，RC任務單分派單之狀態同時改成CL狀態。
                    if (TaskModel.FinishQuantity >= TaskModel.DispatchQuantity)
                        TaskModel.Status = Framework.SystemID + "020121300008B";

                    SFC_TaskDispatchService.update(userid, TaskModel);
                    //定义转出量
                    decimal OutProQuantity = model.FinProQuantity;

                    if (OperationModel == null)//如果不存在工序,则这个完工单对应的只有制程
                    {
                        /*
                        無工序資料時
                        1.自動寫入制令制程檔欄位OutProQty 轉出量(移轉量)  (MoProcessId	製令製程ID、MoOperationId製令製程工序ID),
                        2.順道寫入下一道制程之PreProQty 前站轉入量
                        3.下一道制程有工序時,需再寫入第一道工序之PreProQty 前站轉入量
                         */

                        //1.自動寫入制令制程檔欄位OutProQty 轉出量
                        ProcessModel.OutProQuantity += OutProQuantity;

                        ProcessModel.FinProQuantity += model.FinProQuantity;
                        ProcessModel.ScrappedQuantity += model.ScrappedQuantity;
                        ProcessModel.DifferenceQuantity += model.DifferenceQuantity;
                        ProcessModel.RepairQuantity += model.RepairQuantity;

                        // 8.當製令製程檔的完工量大於等於生產量時，制程狀態之狀態同時改為CL狀態。
                        if (ProcessModel.FinProQuantity >= ProcessModel.Quantity)
                            ProcessModel.Status = Framework.SystemID + "020121300002A";

                        SFC_FabMoProcessService.update(userid, ProcessModel);

                        SFC_FabMoRelationship Pship = SFC_FabMoRelationshipService.getByFMProcess(model.FabMoProcessID);
                        if (Pship != null && Pship.IfLastProcess)//如果是最后的制程，将相关内容回写到制程中
                        {
                            //當完工單回報之制程(工序)為最終制程(工序)時,應回寫到制令單之製令入庫量;畫面之製令完工量=表之製令入庫量,由完工單之最後一道制程(工序)回寫加總數量
                            FMModel.StorageQuantity = FMModel.StorageQuantity + model.FinProQuantity;

                            /*
                             * Sam 2017年9月29日15:49:17
                             * 经过顾问询问台湾方面，当最终制程完工量回写到制令单时，判断制令单的完工量是否大于等于制造数量，如果是，则结案
                             */
                            if (FMModel.StorageQuantity >= FMModel.Quantity)
                                FMModel.Status = Framework.SystemID + "020121300002A";

                            SFC_FabricatedMotherService.update(userid, FMModel);
                        }
                        else
                        {
                            //2.順道寫入下一道制程之PreProQty 前站轉入量
                            SFC_FabMoProcess NextFabMoProcess = SFC_FabMoProcessService.get(model.NextFabMoProcessID);
                            if (NextFabMoProcess != null)
                            {
                                NextFabMoProcess.PreProQuantity += OutProQuantity;
                                SFC_FabMoProcessService.update(userid, NextFabMoProcess);
                            }
                            //3.下一道制程有工序時,需再寫入第一道工序之PreProQty 前站轉入量
                            IList<Hashtable> NextFabMoOperationList = SFC_FabMoOperationService.GetFirstOperationList(model.NextFabMoProcessID);
                            foreach (Hashtable item in NextFabMoOperationList)
                            {
                                SFC_FabMoOperation NextFabMoOperation = SFC_FabMoOperationService.get(item["FabMoOperationID"].ToString());
                                if (NextFabMoOperation != null)
                                {
                                    NextFabMoOperation.PreProQuantity += OutProQuantity;
                                    SFC_FabMoOperationService.update(userid, NextFabMoOperation);
                                }
                            }
                        }
                    }
                    else //存在工序
                    {
                        /*
                       有工序資料時
                        1.自動寫入制令制程工序檔欄位OutProQty 轉出量(移轉量) (MoProcessId製令製程ID、MoOperationId製令製程工序ID)

                        3.制令制程工序檔如是最後一道工序(制程2工序6),將此工序之完工量(出站量),轉出量(移轉量),報廢量,差異量,
                          寫入該製令制程檔(制程2),其他(制程2工序1~5)不用寫入制程檔(制程2)
                          順道將移轉量寫入下一道制程之PreProQty 前站轉入量
                        4.下一道制程有工序時,需再寫入第一道工序之PreProQty 前站轉入量
                         */

                        //1.自動寫入制令制程工序檔欄位OutProQty 轉出量(移轉量) (MoProcessId製令製程ID、MoOperationId製令製程工序ID)
                        OperationModel.OutProQuantity += OutProQuantity;
                        OperationModel.FinProQuantity += model.FinProQuantity;
                        OperationModel.ScrappedQuantity += model.ScrappedQuantity;
                        OperationModel.DifferenceQuantity += model.DifferenceQuantity;
                        OperationModel.RepairQuantity += model.RepairQuantity;

                        if (OperationModel.FinProQuantity >= OperationModel.Quantity)
                            OperationModel.Status = Framework.SystemID + "020121300002A";

                        SFC_FabMoOperationService.update(userid, OperationModel);

                        //获取工序的工序关系设定
                        SFC_FabMoOperationRelationship ship = SFC_FabMoOperationRelationshipService.getByFMOperation(OperationModel.FabMoOperationID);
                        if (ship != null && ship.IsLastOperation)//如果是最后的工序，将相关内容回写到制程中
                        {
                            ProcessModel.PreProQuantity += OutProQuantity;
                            ProcessModel.FinProQuantity += model.FinProQuantity;
                            ProcessModel.ScrappedQuantity += model.ScrappedQuantity;
                            ProcessModel.DifferenceQuantity += model.DifferenceQuantity;
                            ProcessModel.RepairQuantity += model.RepairQuantity;

                            if (ProcessModel.FinProQuantity >= ProcessModel.Quantity)
                                ProcessModel.Status = Framework.SystemID + "020121300002A";

                            SFC_FabMoProcessService.update(userid, ProcessModel);
                        }
                        else //如果不是最后工序
                        {
                            //3.寫入下一站之制令制程工序PreProQty 前站轉入量
                            SFC_FabMoOperation NextFabMoOperation = SFC_FabMoOperationService.get(model.NextFabMoOperationID);
                            if (NextFabMoOperation != null)
                            {
                                NextFabMoOperation.PreProQuantity = +OutProQuantity;
                                SFC_FabMoOperationService.update(userid, NextFabMoOperation);
                            }
                        }
                    }
                    return new { status = "200", msg = "確認成功!" };
                }
                else
                    return new { status = "410", msg = "確認失败！" };
            }
            catch (Exception ex)
            {
                DataLogerService.writeerrlog(ex);
                return new { status = "410", msg = "信息异常，请记录时间并联系开发人员！" };
            }
        }

        /// <summary>
        /// 判断报废量，差异量，返修量是否可编辑
        /// SAM 2017年7月26日16:41:53
        /// </summary>
        /// <param name="Token"></param>
        /// <param name="CompletionOrderID"></param>
        /// <returns></returns>
        public static object Sfc00007CheckQuantity(string Token, string CompletionOrderID)
        {
            bool IsScrapped = true; //报废
            bool IsDifference = true;//差异
            bool IsRepair = true;//返修
            bool IsLaborHour = true;//人工工时
            bool IsUnLaborHour = true;//无效人工工时
            bool IsMachineHour = true;//机器工时
            bool IsUnMachineHour = true;//无效机器工时

            bool Abnormal = false;// 异常说明是否必输
            MES_Parameter MESParModel = MES_ParameterService.get(Framework.SystemID + "1101213000004");
            if (MESParModel != null)
            {
                try
                {
                    Abnormal = bool.Parse(MESParModel.Value);
                }
                catch (Exception ex)
                {
                    DataLogerService.writeerrlog(ex);
                }
            }

            if (Abnormal) // 如果是必输
            {
                IsScrapped = false; //报废
                IsDifference = false;//差异
                IsRepair = false;//返修
                IsUnLaborHour = false;//无效人工工时
                IsUnMachineHour = false;//无效机器工时
            }
            else
            {
                if (!string.IsNullOrWhiteSpace(CompletionOrderID))//完工单有值，证明是编辑
                {
                    IsScrapped = SFC_AbnormalQuantityService.Check(Framework.SystemID + "020121300009B", CompletionOrderID);
                    IsDifference = SFC_AbnormalQuantityService.Check(Framework.SystemID + "020121300009C", CompletionOrderID);
                    IsRepair = SFC_AbnormalQuantityService.Check(Framework.SystemID + "020121300009D", CompletionOrderID);
                    IsUnLaborHour = SFC_AbnormalHourService.Check(Framework.SystemID + "020121300009F", CompletionOrderID);
                    IsUnMachineHour = SFC_AbnormalHourService.Check(Framework.SystemID + "020121300009E", CompletionOrderID);
                }
            }

            return new
            {
                IsScrapped = IsScrapped,
                IsDifference = IsDifference,
                IsRepair = IsRepair,
                IsLaborHour = IsLaborHour,
                IsUnLaborHour = IsUnLaborHour,
                IsMachineHour = IsMachineHour,
                IsUnMachineHour = IsUnMachineHour
            };
        }

        /// <summary>
        /// 完工单根据料品获取批号
        /// SAM 2017年7月28日10:58:15
        /// </summary>
        /// <param name="Token"></param>
        /// <param name="ItemID"></param>
        /// <returns></returns>
        public static object Sfc00007GetLotNumber(string Token, string ItemID)
        {
            string userid = UtilBussinessService.detoken(Token);
            SYS_Items model = SYS_ItemsService.get(ItemID);
            string BatchNumber = null;
            string Status = "200";
            string Msg = null;
            string AutoNumberRecordID = null;
            if (model == null)
            {
                return new
                {
                    Status = "400",
                    Msg = "料品流水号错误",
                    BatchNumber = BatchNumber,
                    AutoNumberRecordID = AutoNumberRecordID
                };
            }

            if (!model.Lot)
            {
                return new
                {
                    Status = Status,
                    Msg = Msg,
                    BatchNumber = BatchNumber,
                    AutoNumberRecordID = AutoNumberRecordID
                };
            }


            if (string.IsNullOrWhiteSpace(model.LotClassID))
            {
                return new
                {
                    Status = "210",
                    Msg = "料品并不存在批号类别",
                    BatchNumber = BatchNumber,
                    AutoNumberRecordID = AutoNumberRecordID
                };
            }

            SYS_AutoNumber Automodel = SYS_AutoNumberService.get(model.LotClassID);
            if (Automodel == null)
            {
                return new
                {
                    Status = "400",
                    Msg = "批号类别错误",
                    BatchNumber = BatchNumber,
                    AutoNumberRecordID = AutoNumberRecordID
                };

            }

            if (Automodel.Status != Framework.SystemID + "0201213000001")
            {
                return new
                {
                    Status = "210",
                    Msg = "批号类别已被作废",
                    BatchNumber = BatchNumber,
                    AutoNumberRecordID = AutoNumberRecordID
                };
            }

            string Prevchar = null;

            string Year = Automodel.YearLength == 4 ? DateTime.Now.ToString("yy") : null;
            string Month = Automodel.MonthLength == 2 ? DateTime.Now.ToString("MM") : null;
            string day = Automodel.DateLength == 2 ? DateTime.Now.ToString("dd") : null;

            Prevchar = Automodel.DefaultCharacter + Year + Month + day;
            try
            {
                SYS_AutoNumberRecord Recordmodel = SYS_AutoNumberRecordService.getByAutoNumber(Automodel.AutoNumberID, Prevchar);
                if (Recordmodel == null)
                {
                    Recordmodel = new SYS_AutoNumberRecord();
                    Recordmodel.AutoNumberRecordID = UniversalService.GetSerialNumber("SYS_AutoNumberRecord");
                    Recordmodel.AutoNumberID = Automodel.AutoNumberID;
                    Recordmodel.Num = 1;
                    Recordmodel.Prevchar = Prevchar;
                    Recordmodel.Status = Framework.SystemID + "0201213000001";
                    SYS_AutoNumberRecordService.insert(userid, Recordmodel);
                }
                AutoNumberRecordID = Recordmodel.AutoNumberRecordID;
                BatchNumber = Recordmodel.Prevchar + (Recordmodel.Num + 1).ToString().PadLeft(Automodel.NumLength, '0');
            }
            catch (Exception ex)
            {
                Status = "400";
                Msg = ex.ToString();
            }
            return new
            {
                Status = Status,
                Msg = Msg,
                BatchNumber = BatchNumber,
                AutoNumberRecordID = AutoNumberRecordID
            };
        }


        /// <summary>
        /// 根据完工单流水号获取资源报工工时
        /// SAM 2017年9月13日14:55:52
        /// </summary>
        /// <param name="token"></param>
        /// <param name="completionOrderID"></param>
        /// <returns></returns>
        public static string Sfc00007GetReportingHour(string token, string completionOrderID)
        {
            SFC_CompletionOrder Com = SFC_CompletionOrderService.get(completionOrderID);
            if (Com == null)
                return null;

            SFC_TaskDispatch Task = SFC_TaskDispatchService.get(Com.TaskDispatchID);
            if (Task == null)
                return null;

            Com = SFC_CompletionOrderService.GetLastByTaskDispatch(Task.TaskDispatchID);
            if (Com != null)
            {
                if (Com.Date == null)
                    return null;
                TimeSpan Ts = DateTime.Now - DateTime.Parse(Com.Date.ToString());
                return UtilBussinessService.HourConversionStr(Math.Round(Ts.TotalSeconds).ToString());
            }
            else
            {
                if (Task.StartDate == null)
                    return null;
                TimeSpan Ts = DateTime.Now - DateTime.Parse(Task.StartDate.ToString());
                return UtilBussinessService.HourConversionStr(Math.Round(Ts.TotalSeconds).ToString());
            }
        }

        #endregion

        #region SFC00008完工调整作业
        /// <summary>
        /// 查询完工调整作业表
        /// SAM 2017年7月20日16:19:14
        /// </summary>
        /// <param name="token"></param>
        /// <param name="AdjustCode"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <returns></returns>
        public static object SFC00008GetList(string token, string AdjustCode, int page, int rows)
        {
            int count = 0;
            IList<Hashtable> result = SFC_CompletionOrderService.SFC00008GetList(AdjustCode, page, rows, ref count);
            foreach (Hashtable item in result)
            {
                //LaborHour,A.UnLaborHour,A.MachineHour,A.UnMachineHour
                item["LaborHour"] = UtilBussinessService.HourConversionStr(item["LaborHour"].ToString());
                item["UnLaborHour"] = UtilBussinessService.HourConversionStr(item["UnLaborHour"].ToString());
                item["MachineHour"] = UtilBussinessService.HourConversionStr(item["MachineHour"].ToString());
                item["UnMachineHour"] = UtilBussinessService.HourConversionStr(item["UnMachineHour"].ToString());
            }
            return UtilBussinessService.getPaginationModel(result, count);
        }

        /// <summary>
        /// 调整单获取单据类别列表
        /// SAM 2017年8月1日11:58:32
        /// </summary>
        /// <param name="Token"></param>
        /// <returns></returns>
        public static object Sfc00008GetTypeList(string Token)
        {
            string userid = UtilBussinessService.detoken(Token);
            return SYS_DocumentTypeSettingService.GetTypeList(userid, Framework.SystemID + "02012130000AA");
        }

        /// <summary>
        /// 调整单拿号
        /// SAM 2017年8月1日12:01:45
        /// </summary>
        /// <param name="Token"></param>
        /// <param name="dTSID"></param>
        /// <param name="Date"></param>
        /// <returns></returns>
        public static object Sfc00008GetAutoNumber(string Token, string DTSID, string Date)
        {
            DateTime Now = DateTime.Now;
            if (!string.IsNullOrWhiteSpace(Date))
            {
                try
                {
                    Now = DateTime.Parse(Date);
                }
                catch (Exception ex)
                {
                    DataLogerService.writeerrlog(ex);
                }
            }

            string userid = UtilBussinessService.detoken(Token);
            string Prevchar = null;
            SYS_DocumentTypeSetting number = SYS_DocumentTypeSettingService.get(DTSID);
            if (number == null)
                return null;

            SYS_Parameters ParModel = SYS_ParameterService.get(number.GiveWay);
            if (ParModel.Code == "M")
                Prevchar = number.Code + Now.ToString("yyMM");
            else if (ParModel.Code == "Y")
                Prevchar = number.Code + Now.ToString("yy");
            else if (ParModel.Code == "D")
                Prevchar = number.Code + Now.ToString("yyMMdd");

            string AutoNumber = null;
            string AutoNumberID = null;

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

            AutoNumber = model.DefaultCharacter + (model.Num + 1).ToString().PadLeft(number.CodeLength, '0');
            AutoNumberID = model.DocumentAutoNumberID;

            return new { AutoNumber = AutoNumber, AutoNumberID = AutoNumberID };
        }

        /// <summary>
        /// 调整单的新增
        /// SAM 2017年7月20日17:47:11
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public static object Sfc00008Add(JObject request)
        {
            string Token = request.Value<string>("Token");
            string userid = UtilBussinessService.detoken(Token);
            int? Hour = null;
            SFC_CompletionOrder Complention = SFC_CompletionOrderService.get(request.Value<string>("OriginalCompletionOrderID"));
            if (Complention == null)
                return new { status = "200", msg = "原完工单信息不存在！" };

            SFC_CompletionOrder model = new SFC_CompletionOrder();
            model.CompletionOrderID = UniversalService.GetSerialNumber("SFC_CompletionOrder");
            model.CompletionNo = request.Value<string>("CompletionNo");
            model.Date = request.Value<DateTime>("Date");
            model.TaskDispatchID = Complention.TaskDispatchID;
            model.FabricatedMotherID = Complention.FabricatedMotherID;
            model.FabMoProcessID = Complention.FabMoProcessID;
            model.FabMoOperationID = Complention.FabMoOperationID;
            model.ItemID = Complention.ItemID;
            model.ProcessID = Complention.ProcessID;
            model.OperationID = Complention.OperationID;
            model.FinProQuantity = request.Value<decimal>("FinProQuantity");
            model.ScrappedQuantity = request.Value<decimal>("ScrappedQuantity");
            model.DifferenceQuantity = request.Value<decimal>("DifferenceQuantity");
            model.RepairQuantity = request.Value<decimal>("RepairQuantity");
            Hour = UtilBussinessService.StrConversionHour(request.Value<string>("LaborHour"));
            if (Hour == null)
                return new { status = "410", msg = "人工工时格式异常！" };
            else
                model.LaborHour = (int)Hour;

            Hour = UtilBussinessService.StrConversionHour(request.Value<string>("UnLaborHour"));
            if (Hour == null)
                return new { status = "410", msg = "无效人工工时格式异常！" };
            else
                model.UnLaborHour = (int)Hour;

            Hour = UtilBussinessService.StrConversionHour(request.Value<string>("MachineHour"));
            if (Hour == null)
                return new { status = "410", msg = "机器工时格式异常！" };
            else
                model.MachineHour = (int)Hour;

            Hour = UtilBussinessService.StrConversionHour(request.Value<string>("UnMachineHour"));
            if (Hour == null)
                return new { status = "410", msg = "无效机器工时格式异常！" };
            else
                model.UnMachineHour = (int)Hour;

            model.Status = Framework.SystemID + "0201213000029";
            model.Type = Framework.SystemID + "02012130000A1";
            model.Comments = request.Value<string>("Comments");
            model.OriginalCompletionOrderID = request.Value<string>("OriginalCompletionOrderID");
            model.DTSID = request.Value<string>("DTSID");
            //while (SFC_CompletionOrderService.CheckCode(model.CompletionNo, null))
            //{
            //    model.CompletionNo = UtilBussinessService.GetDocumentAutoNumber(userid, request.Value<string>("DTSID"),model.Date.ToString());
            //}

            if (SFC_CompletionOrderService.insert(userid, model))
            {
                UtilBussinessService.UpdateDocumentAutoNumber(userid, request.Value<string>("AutoNumberID"));
                return new { status = "200", msg = "新增成功！" };
            }
            else
                return new { status = "410", msg = "新增失败！" };
        }


        /// <summary>
        /// 原完工单号的开窗查询
        /// SAM 2017年7月20日16:28:55
        /// </summary>
        /// <param name="token"></param>
        /// <param name="OldCompletedNo"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <returns></returns>
        public static object Sfc00008GetOldCompletedNo(string token, string OldCompletedNo, int page, int rows)
        {
            int count = 0;
            return UtilBussinessService.getPaginationModel(SFC_CompletionOrderService.Sfc00008GetOldCompletedNo(OldCompletedNo, page, rows, ref count), count);
        }

        /// <summary>
        /// 调整单的确认
        /// SAM 2017年7月25日16:00:46
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public static object Sfc00008Confirm(JObject request)
        {
            string Token = request.Value<string>("Token");
            string userid = UtilBussinessService.detoken(Token);
            try
            {
                /*
                單據狀態為OP時顯示,執行確認後
             1	完工數量、報廢量、差異量、寫入
             2	寫入RC任務單分派資料檔完工數(累加)及制令製程(工序)檔完工量(累加) 
             3	寫入RC任務單分派資料檔報廢量(累加)及制令製程(工序)檔報廢量(累加)
             4	寫入RC任務單分派資料檔差異量(累加)及製令製程(工序)檔差異量(累加) 
             5	寫入RC任務單分派資料檔返修量(累加)及製令製程(工序)檔返修量(累加) 
             6	RC任務單單如有工序,制令制程工序檔如是最後一道工序(制程2工序6),將此工序之相關數量寫入該製令制程檔(制程2),其他(制程2工序1~5)不用寫入制程檔(制程2)
             7	當RC任務單分派資料檔完工量大於等於分派量時，RC任務單分派單之狀態同時改成CL狀態。
             8	當製令製程工序檔的完工量大於等於生產量時，制程工序狀態之狀態同時改為CL狀態。
             9	當製令製程檔的完工量大於等於生產量時，制程狀態之狀態同時改為CL狀態。
            10	狀態CL時,自動產生移轉單參考制程移轉單規格 (本完工單之已完工量產生移轉單(單據種SFC2))
            11	如有料品特性資料必需確認料品特性資料值都必需有值
            12	料品主檔批控參數=Y時,需檢查批號是否有輸入, 若批號件無批號資料時，顯示訊息”此料件為批號管控件,請檢查批號資料”。

                */

                SFC_CompletionOrder model = SFC_CompletionOrderService.get(request.Value<string>("CompletionOrderID"));
                if (model == null)
                    return new { status = "410", msg = "不存在的完工单信息" };
                SFC_TaskDispatch TaskModel = SFC_TaskDispatchService.get(model.TaskDispatchID);//任务单
                SYS_Items ItemModel = SYS_ItemsService.get(model.ItemID);//料品
                SFC_FabricatedMother FMModel = SFC_FabricatedMotherService.get(model.FabricatedMotherID);//制令母单
                SFC_FabMoOperation OperationModel = SFC_FabMoOperationService.get(model.FabMoOperationID);//工序
                SFC_FabMoProcess ProcessModel = SFC_FabMoProcessService.get(model.FabMoProcessID);//制程


                //11.如有料品特性資料必需確認料品特性資料值都必需有值
                if (SFC_BatchAttributeDetailsService.CheckAttributeValue(model.CompletionOrderID))
                    return new { status = "410", msg = "存在属性值为空的批号属性资料！" };


                //12.料品主檔批控參數 = Y時,需檢查批號是否有輸入, 若批號件無批號資料時，顯示訊息”此料件為批號管控件,請檢查批號資料”。
                if (ItemModel.Lot)
                {
                    //判断是否存在批号数据
                    IList<Hashtable> LotList = SFC_BatchAttributeService.Sfc00007CheckLot(model.CompletionOrderID);
                    if (LotList == null)
                        return new { status = "410", msg = "此料件為批號管控件,請檢查批號資料！" };
                }

                model.Status = Framework.SystemID + "020121300002A";

                if (SFC_CompletionOrderService.update(userid, model))
                {
                    //1-8
                    TaskModel.FinishQuantity += model.FinProQuantity;
                    TaskModel.ScrapQuantity += model.ScrappedQuantity;
                    TaskModel.DiffQuantity += model.DifferenceQuantity;
                    TaskModel.RepairQuantity += model.RepairQuantity;

                    if (TaskModel.FinishQuantity >= TaskModel.DispatchQuantity)
                        TaskModel.Status = Framework.SystemID + "020121300008B";

                    SFC_TaskDispatchService.update(userid, TaskModel);

                    if (OperationModel == null)
                    {
                        ProcessModel.FinProQuantity += model.FinProQuantity;
                        ProcessModel.ScrappedQuantity += model.ScrappedQuantity;
                        ProcessModel.DifferenceQuantity += model.DifferenceQuantity;
                        ProcessModel.RepairQuantity += model.RepairQuantity;
                        if (ProcessModel.FinProQuantity >= ProcessModel.Quantity)
                            ProcessModel.Status = Framework.SystemID + "020121300002A";

                        SFC_FabMoProcessService.update(userid, ProcessModel);
                    }
                    else
                    {

                        OperationModel.FinProQuantity += model.FinProQuantity;
                        OperationModel.ScrappedQuantity += model.ScrappedQuantity;
                        OperationModel.DifferenceQuantity += model.DifferenceQuantity;
                        OperationModel.RepairQuantity += model.RepairQuantity;

                        if (OperationModel.FinProQuantity >= OperationModel.Quantity)
                            OperationModel.Status = Framework.SystemID + "020121300002A";

                        SFC_FabMoOperationService.update(userid, OperationModel);

                        SFC_FabMoOperationRelationship ship = SFC_FabMoOperationRelationshipService.getByFMOperation(OperationModel.FabMoOperationID);
                        if (ship != null && ship.IsLastOperation)
                        {

                            ProcessModel.FinProQuantity += model.FinProQuantity;
                            ProcessModel.ScrappedQuantity += model.ScrappedQuantity;
                            ProcessModel.DifferenceQuantity += model.DifferenceQuantity;
                            ProcessModel.RepairQuantity += model.RepairQuantity;

                            if (ProcessModel.FinProQuantity >= ProcessModel.Quantity)
                                ProcessModel.Status = Framework.SystemID + "020121300002A";

                            SFC_FabMoProcessService.update(userid, ProcessModel);
                        }
                    }

                    return new { status = "200", msg = "確認成功！" };
                }
                else
                    return new { status = "410", msg = "確認失败！" };
            }
            catch (Exception ex)
            {
                DataLogerService.writeerrlog(ex);
                return new { status = "410", msg = "信息异常，请记录时间并联系开发人员！" };
            }
        }

        /// <summary>
        /// 调整单的更新
        /// SAM 2017年7月26日21:53:15
        /// </summary>
        /// <param name="Token"></param>
        /// <param name="jArray"></param>
        /// <returns></returns>
        public static object Sfc00008update(string Token, JArray jArray)
        {
            int success = 0;
            int fail = 0;
            string failIDs = null;
            string msg = null;
            string userid = UtilBussinessService.detoken(Token);
            JObject data = null;
            SFC_CompletionOrder model = null;
            for (int i = 0; i < jArray.Count; i++)
            {
                data = (JObject)jArray[i];
                model = SFC_CompletionOrderService.get(data.Value<string>("CompletionOrderID"));
                if (model.Status == Framework.SystemID + "020121300002A")
                {
                    failIDs = UtilBussinessService.str(failIDs, data.Value<string>("CompletionOrderID"));
                    msg = UtilBussinessService.str(msg, "完工单处于不可编辑状态！");
                    fail++;
                    continue;
                }

                SFC_TaskDispatch Task = SFC_TaskDispatchService.get(data.Value<string>("TaskDispatchID"));
                if (Task == null)
                {
                    failIDs = UtilBussinessService.str(failIDs, data.Value<string>("CompletionOrderID"));
                    msg = UtilBussinessService.str(msg, "任务单信息不存在！");
                    fail++;
                    continue;
                }

                model.Date = data.Value<DateTime>("Date");
                model.FinProQuantity = data.Value<decimal>("FinProQuantity");
                model.ScrappedQuantity = data.Value<decimal>("ScrappedQuantity");
                model.DifferenceQuantity = data.Value<decimal>("DifferenceQuantity");
                model.RepairQuantity = data.Value<decimal>("RepairQuantity");
                model.LaborHour = UtilBussinessService.StrConversionHour(data.Value<string>("LaborHour"));
                model.MachineHour = UtilBussinessService.StrConversionHour(data.Value<string>("MachineHour"));
                model.UnLaborHour = UtilBussinessService.StrConversionHour(data.Value<string>("UnLaborHour"));
                model.UnMachineHour = UtilBussinessService.StrConversionHour(data.Value<string>("UnMachineHour"));
                if (model.LaborHour == null)
                {
                    failIDs = UtilBussinessService.str(failIDs, data.Value<string>("CompletionOrderID"));
                    msg = UtilBussinessService.str(msg, "人工工时格式异常！");
                    fail++;
                    continue;
                }
                if (model.UnLaborHour == null)
                {
                    failIDs = UtilBussinessService.str(failIDs, data.Value<string>("CompletionOrderID"));
                    msg = UtilBussinessService.str(msg, "无效人工工时格式异常！");
                    fail++;
                    continue;
                }
                if (model.MachineHour == null)
                {
                    failIDs = UtilBussinessService.str(failIDs, data.Value<string>("CompletionOrderID"));
                    msg = UtilBussinessService.str(msg, "机器工时格式异常！");
                    fail++;
                    continue;
                }
                if (model.UnMachineHour == null)
                {
                    failIDs = UtilBussinessService.str(failIDs, data.Value<string>("CompletionOrderID"));
                    msg = UtilBussinessService.str(msg, "无效机器工时格式异常！");
                    fail++;
                    continue;
                }
                model.Comments = data.Value<string>("Comments");
                if (SFC_CompletionOrderService.update(userid, model))
                    success++;
                else
                {
                    failIDs = UtilBussinessService.str(failIDs, data.Value<string>("CompletionOrderID"));
                    fail++;
                }

            }
            return new { success = success, fail = fail, failIDs = failIDs, msg = msg };
        }


        #endregion

        #region SFC00010制程完工状况分析（制令）
        /// <summary>
        /// 制程完工状况分析（制令）-主列表
        /// SAM 2017年7月23日01:07:56
        /// </summary>
        /// <param name="token"></param>
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
        public static object Sfc00010GetList(string token, string StartWorkCenterCode, string EndWorkCenterCode,
          string StartFabMoCode, string EndFabMoCode, string StartDate, string EndDate,
          string StartCustCode, string EndCustCode, string StartMESUserCode, string EndMESUserCode,
          int page, int rows)
        {
            int count = 0;
            IList<Hashtable> result = SFC_FabricatedMotherService.Sfc00010GetList(StartWorkCenterCode, EndWorkCenterCode,
                StartFabMoCode, EndFabMoCode, StartDate, EndDate,
                StartCustCode, EndCustCode, StartMESUserCode, EndMESUserCode, page, rows, ref count);
            return UtilBussinessService.getPaginationModel(result, count);
        }

        /// <summary>
        ///  制程完工状况分析（制令）-制程主列表
        ///  SAM 2017年7月23日01:27:58
        /// </summary>
        /// <param name="token"></param>
        /// <param name="FabricatedMotherID"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <returns></returns>
        public static object Sfc00010GetProcessList(string token, string FabricatedMotherID, int page, int rows)
        {
            int count = 0;
            IList<Hashtable> result = SFC_FabMoProcessService.Sfc00010GetProcessList(FabricatedMotherID, page, rows, ref count);
            return UtilBussinessService.getPaginationModel(result, count);
        }

        #endregion

        #region SFC00011制程完工状况分析（工作中心）
        /// <summary>
        /// 制程完工状况分析-主列表
        /// SAM 2017年7月23日00:46:30
        /// </summary>
        /// <param name="token"></param>
        /// <param name="StartWorkCenterCode"></param>
        /// <param name="EndWorkCenterCode"></param>
        /// <param name="StartFabMoCode"></param>
        /// <param name="EndFabMoCode"></param>
        /// <param name="StartDate"></param>
        /// <param name="EndDate"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <returns></returns>
        public static object Sfc00011GetList(string token, string StartWorkCenterCode, string EndWorkCenterCode,
           string StartFabMoCode, string EndFabMoCode, string StartDate, string EndDate, int page, int rows)
        {
            int count = 0;
            IList<Hashtable> result = SFC_FabMoProcessService.Sfc00011GetList(StartWorkCenterCode, EndWorkCenterCode, StartFabMoCode, EndFabMoCode, StartDate, EndDate, page, rows, ref count);
            return UtilBussinessService.getPaginationModel(result, count);
        }

        /// <summary>
        /// 制程完工状况分析-主列表
        /// SAM 2017年7月23日00:46:30
        /// 需求新增，加两个字段
        /// Mouse 2017年11月15日18:01:50
        /// </summary>
        /// <param name="token"></param>
        /// <param name="StartWorkCenterCode"></param>
        /// <param name="EndWorkCenterCode"></param>
        /// <param name="StartFabMoCode"></param>
        /// <param name="EndFabMoCode"></param>
        /// <param name="StartDate"></param>
        /// <param name="EndDate"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <returns></returns>
        public static object Sfc00011GetListV1(string token, string StartWorkCenterCode, string EndWorkCenterCode,
           string StartFabMoCode, string EndFabMoCode, string StartDate, string EndDate, int page, int rows)
        {
            int count = 0;
            IList<Hashtable> result = SFC_FabMoProcessService.Sfc00011GetListV1(StartWorkCenterCode, EndWorkCenterCode, StartFabMoCode, EndFabMoCode, StartDate, EndDate, page, rows, ref count);
            return UtilBussinessService.getPaginationModel(result, count);
        }

        /// <summary>
        /// 制程完工状况分析-工序列表
        /// SAM 2017年7月23日00:59:22
        /// </summary>
        /// <param name="token"></param>
        /// <param name="FabMoProcessID"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <returns></returns>
        public static object Sfc00011GetOperationList(string token, string FabMoProcessID, int page, int rows)
        {
            int count = 0;
            IList<Hashtable> result = SFC_FabMoOperationService.Sfc00011GetOperationList(FabMoProcessID, page, rows, ref count);
            return UtilBussinessService.getPaginationModel(result, count);
        }

        #endregion

        #region SFC00012制程工时分析
        /// <summary>
        /// 制程工时分析-主列表
        /// SAM 2017年7月23日00:15:29
        /// </summary>
        /// <param name="token"></param>
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
        public static object Sfc00012GetList(string token, string StartProcessCode, string EndProcessCode,
            string StartWorkCenterCode, string EndWorkCenterCode,
            string StartFabMoCode, string EndFabMoCode,
            string StartItemCode, string EndItemCode,
            string StartDate, string EndDate, int page, int rows)
        {
            int count = 0;
            IList<Hashtable> result = SFC_CompletionOrderService.Sfc00012GetList(StartProcessCode, EndProcessCode, StartWorkCenterCode, EndWorkCenterCode, StartFabMoCode, EndFabMoCode, StartItemCode, EndItemCode, StartDate, EndDate, page, rows, ref count);
            return UtilBussinessService.getPaginationModel(result, count);
        }

        /// <summary>
        /// 制程工时分析-工序列表
        /// SAM 2017年7月23日01:52:15
        /// </summary>
        /// <param name="token"></param>
        /// <param name="FabMoProcessID"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <returns></returns>
        public static object Sfc00012GetOperationList(string token, string FabMoProcessID, int page, int rows)
        {
            int count = 0;
            IList<Hashtable> result = SFC_CompletionOrderService.Sfc00012GetOperationList(FabMoProcessID, page, rows, ref count);
            return UtilBussinessService.getPaginationModel(result, count);
        }
        #endregion

        #region SFC00013制程完工异常分析
        /// <summary>
        /// 制程完工异常分析-主列表
        /// SAM 2017年7月9日23:15:35
        /// </summary>
        /// <param name="token"></param>
        /// <param name="startProcessCode"></param>
        /// <param name="endProcessCode"></param>
        /// <param name="startItemCode"></param>
        /// <param name="endItemCode"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <returns></returns>
        public static object Sfc00013GetList(string token, string StartProcessCode, string EndProcessCode, string StartItemCode, string EndItemCode, string StartDate, string EndDate, int page, int rows)
        {
            int count = 0;
            IList<Hashtable> result = SFC_CompletionOrderService.Sfc00013GetList(StartProcessCode, EndProcessCode, StartItemCode, EndItemCode, StartDate, EndDate, page, rows, ref count);
            return UtilBussinessService.getPaginationModel(result, count);
        }

        /// <summary>
        /// 制程完工异常分析-異常明細
        /// SAM 2017年7月9日23:36:33
        /// </summary>
        /// <param name="token"></param>
        /// <param name="ItemID"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <returns></returns>
        public static IList<Hashtable> Sfc00013GetDetailList(string token, string ItemID, string ProcessID, string StartDate, string EndDate)
        {
            //根据料品流水号和制程流水号以及日期范围内的获取对应的原因码列表(去重后的)
            IList<Hashtable> result = null;
            IList<Hashtable> Reason = SFC_AbnormalQuantityService.Sfc00013GetReason(ItemID, ProcessID, StartDate, EndDate);
            if (Reason != null)
            {
                result = new List<Hashtable>();
                Hashtable model = null;
                decimal ScrappedQuantity = 0;
                decimal DifferenceQuantity = 0;
                decimal RepairQuantity = 0;
                decimal AllTotal = 0;
                foreach (Hashtable ResItem in Reason)
                {
                    model = new Hashtable();
                    model["ReasonCode"] = ResItem["ReasonCode"];
                    //根据原因码和异常类型，加总3个异常数量
                    model["ScrappedQuantity"] = SFC_AbnormalQuantityService.Sfc00013GetAbnormalQuantity(ResItem["ReasonID"].ToString(), Framework.SystemID + "020121300009B", ItemID, ProcessID, StartDate, EndDate);
                    ScrappedQuantity = ScrappedQuantity + decimal.Parse(model["ScrappedQuantity"].ToString());
                    model["DifferenceQuantity"] = SFC_AbnormalQuantityService.Sfc00013GetAbnormalQuantity(ResItem["ReasonID"].ToString(), Framework.SystemID + "020121300009C", ItemID, ProcessID, StartDate, EndDate);
                    DifferenceQuantity = DifferenceQuantity + decimal.Parse(model["DifferenceQuantity"].ToString());
                    model["RepairQuantity"] = SFC_AbnormalQuantityService.Sfc00013GetAbnormalQuantity(ResItem["ReasonID"].ToString(), Framework.SystemID + "020121300009D", ItemID, ProcessID, StartDate, EndDate);
                    RepairQuantity = RepairQuantity + decimal.Parse(model["RepairQuantity"].ToString());
                    model["Total"] = decimal.Parse(model["ScrappedQuantity"].ToString()) + decimal.Parse(model["DifferenceQuantity"].ToString()) + decimal.Parse(model["RepairQuantity"].ToString());
                    AllTotal += decimal.Parse(model["Total"].ToString());
                    result.Add(model);
                }
                model = new Hashtable();
                model["ReasonCode"] = "合计";
                model["ScrappedQuantity"] = ScrappedQuantity;
                model["DifferenceQuantity"] = DifferenceQuantity;
                model["RepairQuantity"] = RepairQuantity;
                model["Total"] = AllTotal;
                result.Add(model);
            }
            return result;
        }

        #endregion

        #region SFC00014人工时统计分析
        /// <summary>
        /// 人工时统计分析列表
        /// SAM 2017年7月9日23:22:23
        /// </summary>
        /// <param name="token"></param>
        /// <param name="startProcessCode"></param>
        /// <param name="endProcessCode"></param>
        /// <param name="startItemCode"></param>
        /// <param name="endItemCode"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <returns></returns>
        public static object Sfc00014GetList(string token, string StartWorkCenterCode, string EndWorkCenterCode, string StartUserCode, string EndUserCode, string StartDate, string EndDate, int page, int rows)
        {
            int count = 0;
            IList<Hashtable> result = SFC_CompletionResourceService.Sfc00014GetList(StartWorkCenterCode, EndWorkCenterCode, StartUserCode, EndUserCode, StartDate, EndDate, page, rows, ref count);
            return UtilBussinessService.getPaginationModel(result, count);
        }
        #endregion

        #region SFC00015机器工时统计分析
        /// <summary>
        /// 机器工时统计分析
        /// SAM 2017年7月9日23:25:09
        /// </summary>
        /// <param name="token"></param>
        /// <param name="startWorkCenterCode"></param>
        /// <param name="EndWorkCenterCode"></param>
        /// <param name="StartEquipmentCode"></param>
        /// <param name="EndEquipmentCode"></param>
        /// <param name="StartDate"></param>
        /// <param name="EndDate"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <returns></returns>
        public static object Sfc00015GetList(string token, string StartWorkCenterCode, string EndWorkCenterCode, string StartEquipmentCode, string EndEquipmentCode, string StartDate, string EndDate, int page, int rows)
        {
            int count = 0;
            IList<Hashtable> result = SFC_CompletionResourceService.Sfc00015GetList(StartWorkCenterCode, EndWorkCenterCode, StartEquipmentCode, EndEquipmentCode, StartDate, EndDate, page, rows, ref count);
            return UtilBussinessService.getPaginationModel(result, count);

        }

        #endregion

        #region SFC00016无效工时原因分析
        /// <summary>
        /// 无效工时原因分析
        /// SAM 2017年7月9日23:28:56
        /// </summary>
        /// <param name="token"></param>
        /// <param name="StartWorkCenterCode"></param>
        /// <param name="EndWorkCenterCode"></param>
        /// <param name="StartDate"></param>
        /// <param name="EndDate"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <returns></returns>
        public static object Sfc00016GetList(string token, string StartWorkCenterCode, string EndWorkCenterCode, string StartDate, string EndDate, int page, int rows)
        {
            int count = 0;
            IList<Hashtable> result = SYS_WorkCenterService.Sfc00016GetList(StartWorkCenterCode, EndWorkCenterCode, StartDate, EndDate, page, rows, ref count);
            return UtilBussinessService.getPaginationModel(result, count);

        }

        /// <summary>
        ///  根据工作中心获取人工無效原因明細
        ///  SAM 2017年7月9日23:34:08
        /// </summary>
        /// <param name="token"></param>
        /// <param name="WorkCenterID"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <returns></returns>
        public static object Sfc00016GetLDetailList(string token, string WorkCenterID, int page, int rows)
        {
            int count = 0;
            IList<Hashtable> result = SFC_AbnormalHourService.Sfc00016GetDetailList(WorkCenterID, Framework.SystemID + "020121300009F", page, rows, ref count);
            return UtilBussinessService.getPaginationModel(result, count);
        }

        /// <summary>
        /// 据工作中心获取机器無效原因明細
        /// SAM 2017年7月9日23:34:21
        /// </summary>
        /// <param name="token"></param>
        /// <param name="WorkCenterID"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <returns></returns>
        public static object Sfc00016GetMDetailList(string token, string WorkCenterID, int page, int rows)
        {
            int count = 0;
            IList<Hashtable> result = SFC_AbnormalHourService.Sfc00016GetDetailList(WorkCenterID, Framework.SystemID + "020121300009F", page, rows, ref count);
            return UtilBussinessService.getPaginationModel(result, count);
        }

        #endregion

        #region SFC00017制令直通率分析
        /// <summary>
        /// 製令直通率分析
        /// SAM 2017年9月3日21:23:10
        /// </summary>
        /// <param name="token"></param>
        /// <param name="StartFabMoCode"></param>
        /// <param name="EndFabMoCode"></param>
        /// <param name="StartItemCode"></param>
        /// <param name="EndItemCode"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <returns></returns>
        public static object Sfc00017GetList(string token, string StartFabMoCode, string EndFabMoCode, string StartItemCode, string EndItemCode, int page, int rows)
        {
            int count = 0;
            IList<Hashtable> result = SFC_FabricatedMotherService.Sfc00017GetList(StartFabMoCode, EndFabMoCode, StartItemCode, EndItemCode, page, rows, ref count);
            decimal Benign = 0;
            decimal Rework = 0;
            foreach (Hashtable item in result)
            {
                //ThroughRate直通率
                /*
                 * 直通率：每個製程良率相乘的結果。
                 * 抓取該張製令單所有的製令製程的前站轉入量、及報廢數量。［製令單維護sfc00002］程式的製令製程資料。
                 * 匯總計算每一個製令製程的完工單上的返修數量。［完工回報作業sfc00007］程式的返修數量。
                 * 計算每個製程的良率：製令製程的前站轉入量，減製令製程的報廢量及匯總返修數量，再除以前站轉入量，四捨五入取小數4位。
                 * 每一個製程的良率相乘之後乘上100，四捨五入取小數2位。
                 */
                IList<SFC_FabMoProcess> FabMoProcessList = SFC_FabMoProcessService.GetFabMoProcessList(item["FabricatedMotherID"].ToString());
                Benign = 1;
                foreach (SFC_FabMoProcess FabProModel in FabMoProcessList)
                {
                    Rework = 0;
                    //匯總計算每一個製令製程的完工單上的返修數量
                    Rework = SFC_CompletionOrderService.GetFabMoProcessRework(FabProModel.FabMoProcessID);
                    if (SFC_FabMoRelationshipService.CheckFirst(FabProModel.FabMoProcessID))
                    {
                        if (FabProModel.AssignQuantity != 0)
                            Benign = Benign * Math.Round((FabProModel.AssignQuantity - FabProModel.ScrappedQuantity - Rework) / FabProModel.AssignQuantity, 4, MidpointRounding.AwayFromZero);
                    }
                    else if (FabProModel.PreProQuantity != 0)
                    {
                        Benign = Benign * Math.Round((FabProModel.PreProQuantity - FabProModel.ScrappedQuantity - Rework) / FabProModel.PreProQuantity, 4, MidpointRounding.AwayFromZero);
                    }                 
                }
                item["ThroughRate"] = Math.Round(Benign * 100, 2, MidpointRounding.AwayFromZero);
                item["ThroughRate"] = item["ThroughRate"].ToString() + "%";
            }
            return UtilBussinessService.getPaginationModel(result, count);
        }

        /// <summary>
        ///  制令直通率分析-制程明细列表
        ///  SAM 2017年9月3日21:26:41
        /// </summary>
        /// <param name="token"></param>
        /// <param name="FabricatedMotherID"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <returns></returns>
        public static object Sfc00017GetDetailList(string token, string FabricatedMotherID, int page, int rows)
        {
            int count = 0;
            IList<Hashtable> result = SFC_FabMoProcessService.Sfc00017GetDetailList(FabricatedMotherID, page, rows, ref count);
            decimal Unhealthy = 0;
            foreach (Hashtable item in result)
            {
                /*
                 * 不良數量：Unhealthy
                 * 匯總計算每一個製令製程的完工單上的返修數量。［完工回報作業sfc00007］程式的返修數量。
                 * 將製令製程的報廢數量加上匯總的返修數量，即為不良數量。
                 * 良率：(前站轉入量 - 不良數量) / 前站轉入量 *100%，四捨五入取小數2位。Rate

                 */
                Unhealthy = SFC_CompletionOrderService.GetFabMoProcessRework(item["FabMoProcessID"].ToString()) + decimal.Parse(item["ScrappedQuantity"].ToString());
                item["Unhealthy"] = Unhealthy;
                if (SFC_FabMoRelationshipService.CheckFirst(item["FabMoProcessID"].ToString()))
                {
                    if (decimal.Parse(item["AssignQuantity"].ToString()) == 0)
                    {
                        item["Rate"] = "0%";
                    }
                    else
                    {
                        item["Rate"] = Math.Round((decimal.Parse(item["AssignQuantity"].ToString()) - Unhealthy) / decimal.Parse(item["AssignQuantity"].ToString()) * 100, 2, MidpointRounding.AwayFromZero);
                        item["Rate"] = item["Rate"].ToString() + "%";
                    }
                }
                else if (decimal.Parse(item["PreProQuantity"].ToString()) == 0)
                    item["Rate"] = "0%";
                else
                {
                    item["Rate"] = Math.Round((decimal.Parse(item["PreProQuantity"].ToString()) - Unhealthy) / decimal.Parse(item["PreProQuantity"].ToString()) * 100, 2, MidpointRounding.AwayFromZero);
                    item["Rate"] = item["Rate"].ToString() + "%";
                }
            }
            return UtilBussinessService.getPaginationModel(result, count);
        }
        #endregion

        #region SFC00018制令用料耗用分析
        /// <summary>
        /// 制令用料耗用分析
        /// SAM 2017年9月3日22:00:02
        /// </summary>
        /// <param name="token"></param>
        /// <param name="StartFabMoCode"></param>
        /// <param name="EndFabMoCode"></param>
        /// <param name="StartDate"></param>
        /// <param name="EndDate"></param>
        /// <param name="Status"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <returns></returns>
        public static object Sfc00018GetList(string token, string StartFabMoCode, string EndFabMoCode, string StartDate, string EndDate, string Status, int page, int rows)
        {
            int count = 0;
            IList<Hashtable> result = SFC_FabMoItemService.Sfc00018GetList(StartFabMoCode, EndFabMoCode, StartDate, EndDate, Status, page, rows, ref count);
            return UtilBussinessService.getPaginationModel(result, count);
        }
        #endregion

        #region SFC00019制品生产工时分析
        /// <summary>
        /// 製品生產工時分析-制品页签
        /// SAM 2017年9月3日22:12:00
        /// </summary>
        /// <param name="token"></param>
        /// <param name="StartItemCode"></param>
        /// <param name="EndItemCode"></param>
        /// <param name="StartFabMoCode"></param>
        /// <param name="EndFabMoCode"></param>
        /// <param name="StartDate"></param>
        /// <param name="EndDate"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <returns></returns>
        public static object Sfc00019ItemGetList(string token,
            string StartItemCode, string EndItemCode,
            string StartFabMoCode, string EndFabMoCode,
            string StartDate, string EndDate, int page, int rows)
        {
            int count = 0;
            IList<Hashtable> result = SFC_FabricatedMotherService.Sfc00019ItemGetList(StartItemCode, EndItemCode, StartFabMoCode, EndFabMoCode, StartDate, EndDate, page, rows, ref count);
            foreach (Hashtable item in result)
            {
                item["ActualHour"] = SFC_TaskDispatchService.Sfc00019GetHour(item["FabMoProcessID"].ToString());
                item["StandardHour"] = string.IsNullOrWhiteSpace(item["StandardHour"].ToString()) ? "0HR" : int.Parse(item["StandardHour"].ToString()) / 3600 + "HR";
                item["DifferenceHour"] = int.Parse(item["ActualHour"].ToString()) - int.Parse(item["StandardTime"].ToString());
                item["DifferenceHour"] = int.Parse(item["DifferenceHour"].ToString()) / 3600;
                item["DifferenceHour"] = item["DifferenceHour"].ToString() + "HR";
                item["ActualHour"] = int.Parse(item["ActualHour"].ToString()) / 3600;
                item["ActualHour"] = item["ActualHour"].ToString() + "HR";
            }

            return UtilBussinessService.getPaginationModel(result, count);
        }

        /// <summary>
        /// 製品生產工時分析-工作中心页签
        /// SAM 2017年9月3日22:12:07
        /// </summary>
        /// <param name="token"></param>
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
        public static object Sfc00019WorkCenterGetList(string token,
        string StartWorkCenterCode, string EndWorkCenterCode,
        string StartItemCode, string EndItemCode,
        string StartFabMoCode, string EndFabMoCode,
        string StartDate, string EndDate, int page, int rows)
        {
            int count = 0;
            IList<Hashtable> result = SFC_FabMoProcessService.Sfc00019WorkCenterGetList(StartWorkCenterCode, EndWorkCenterCode, StartItemCode, EndItemCode, StartFabMoCode, EndFabMoCode, StartDate, EndDate, page, rows, ref count);
            foreach (Hashtable item in result)
            {
                item["ActualHour"] = SFC_TaskDispatchService.Sfc00019GetHour(item["FabMoProcessID"].ToString());
                item["StandardHour"] = string.IsNullOrWhiteSpace(item["StandardHour"].ToString()) ? "0HR" : int.Parse(item["StandardHour"].ToString()) / 3600 + "HR";
                item["DifferenceHour"] = int.Parse(item["ActualHour"].ToString()) - int.Parse(item["StandardTime"].ToString());
                item["DifferenceHour"] = int.Parse(item["DifferenceHour"].ToString()) / 3600;
                item["DifferenceHour"] = item["DifferenceHour"].ToString() + "HR";
                item["ActualHour"] = int.Parse(item["ActualHour"].ToString()) / 3600;
                item["ActualHour"] = item["ActualHour"].ToString() + "HR";
            }
            return UtilBussinessService.getPaginationModel(result, count);
        }
        #endregion

        #region SFC00020制令生产计划表
        /// <summary>
        /// SFC20列表
        /// Mouse 2017-9-26 15:22:54
        /// </summary>
        /// <param name="Token"></param>
        /// <param name="FabricatedMotherIDStar"></param>
        /// <param name="FabricatedMotherIDEnd"></param>
        /// <param name="StartDate"></param>
        /// <param name="FinishDate"></param>
        /// <param name="Status"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <returns></returns>
        public static object sfc00020GetList(string Token, string MoNoStar, string MoNoEnd, string StartDate, string FinishDate, string Status, int page, int rows)
        {
            int count = 0;
            IList<Hashtable> result = SFC_FabMoProcessService.sfc00020GetList(MoNoStar, MoNoEnd, StartDate, FinishDate, Status, page, rows, ref count);
            return UtilBussinessService.getPaginationModel(result, count);
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
        public static object Sfc00021GetList(string Token, string StartProcessCode, string EndProcessCode,
            string StartDate, string EndDate, int page, int rows)
        {
            int count = 0;
            IList<Hashtable> result = SFC_FabMoProcessService.Sfc00021GetList(StartProcessCode, EndProcessCode, StartDate, EndDate, page, rows, ref count);
            foreach (Hashtable item in result)
            {
                string ProcessCode = SYS_LanguageLibService.GetLan(item["ProcessID"].ToString(), "Code", 20);
                if (!string.IsNullOrWhiteSpace(ProcessCode))
                {
                    item["ProcessCode"] = ProcessCode;
                    item["ProcessName"] = SYS_LanguageLibService.GetLan(item["ProcessID"].ToString(), "Name", 20);
                }
                //InputNum,OutputNum,PlanNum
                //OutputRatio;InputPlanRatio,OutputPlanRatio
                //13.產出投入比：(產出數量 / 投入數量) * 100 %，四捨五入至整數量。
                //14.投入計劃比：(投入數量 / 計劃數量) * 100 %，四捨五入至整數量。
                //15.產出計劃比：(產出數量 / 計劃數量) * 100 %，四捨五入至整數量
                if (item["InputNum"].ToString() == "0")
                    item["OutputRatio"] = "0%";
                else
                {
                    item["OutputRatio"] = Math.Round(decimal.Parse(item["OutputNum"].ToString()) / decimal.Parse(item["InputNum"].ToString()) * 100, MidpointRounding.AwayFromZero);
                    item["OutputRatio"] = item["OutputRatio"].ToString() + "%";
                }
                item["InputPlanRatio"] = Math.Round(decimal.Parse(item["InputNum"].ToString()) / decimal.Parse(item["PlanNum"].ToString()) * 100, MidpointRounding.AwayFromZero);
                item["InputPlanRatio"] = item["InputPlanRatio"].ToString() + "%";
                item["OutputPlanRatio"] = Math.Round(decimal.Parse(item["OutputNum"].ToString()) / decimal.Parse(item["PlanNum"].ToString()) * 100, MidpointRounding.AwayFromZero);
                item["OutputPlanRatio"] = item["OutputPlanRatio"].ToString() + "%";
            }
            return UtilBussinessService.getPaginationModel(result, count);
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
        public static object Sfc00022GetReasonList(string Token, string StartItemCode, string EndItemCode,
           string StartDate, string EndDate, int page, int rows)
        {
            int count = 0;
            IList<Hashtable> result = SFC_CompletionOrderService.Sfc00022GetReasonList(StartItemCode, EndItemCode, StartDate, EndDate, page, rows, ref count);
            foreach (Hashtable item in result)
            {
                //根据语序及参数流水号获取语序设定，不存在不替换，存在则替换
                string TypeName = SYS_LanguageLibService.GetLan(item["Type"].ToString(), "Name", 20);
                if (!string.IsNullOrWhiteSpace(TypeName))
                    item["TypeName"] = TypeName;
            }
            return UtilBussinessService.getPaginationModel(result, count);
        }

        /// <summary>
        /// 制品不良统计分析-原因统计页签-明细列表
        /// SAM 2017年10月11日09:29:59
        /// </summary>
        /// <param name="Token"></param>
        /// <param name="ItemID">料号流水号</param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <returns></returns>
        public static object Sfc00022GetReasonDetailList(string Token, string ItemID)
        {
            int count = 0;
            IList<Hashtable> result = SFC_AbnormalQuantityService.Sfc00022GetReasonDetailList(ItemID);
            if (result != null)
            {
                Hashtable Last = new Hashtable();
                Last["ReasonCode"] = "合计";
                Last["Num"] = 0;
                foreach (Hashtable item in result)
                {
                    Last["Num"] = decimal.Parse(Last["Num"].ToString()) + decimal.Parse(item["Num"].ToString());
                }
                result.Add(Last);
            }
            return UtilBussinessService.getPaginationModel(result, count);
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
        public static object Sfc00022GetItemList(string Token, string StartItemCode, string EndItemCode,
          string StartDate, string EndDate,int page, int rows)
        {
            int count = 0;
            IList<Hashtable> result = SFC_CompletionOrderService.Sfc00022GetReasonList(StartItemCode, EndItemCode, StartDate, EndDate, page, rows, ref count);
            foreach (Hashtable item in result)
            {
                //根据语序及参数流水号获取语序设定，不存在不替换，存在则替换
                string TypeName = SYS_LanguageLibService.GetLan(item["Type"].ToString(), "Name", 20);
                if (!string.IsNullOrWhiteSpace(TypeName))
                    item["TypeName"] = TypeName;
            }
            return UtilBussinessService.getPaginationModel(result, count);
        }


        /// <summary>
        /// 制品不良统计分析-料品統計页签-明细列表
        /// Sam 2017年10月11日14:14:10
        /// </summary>
        /// <param name="Token"></param>
        /// <param name="ItemID"></param>
        /// <param name="StartDate"></param>
        /// <param name="EndDate"></param>
        /// <param name="Interval"></param>
        /// <returns></returns>
        public static object Sfc00022GetItemDetailList(string Token, string ItemID, string StartDate, string EndDate,int Interval)
        {
            IList<Hashtable> result = SFC_AbnormalQuantityService.Sfc00022GetItemDetailList(ItemID,StartDate,EndDate, Interval);
            if (result != null)
            {
                Hashtable Last = new Hashtable();
                Last["Date"] = "合计";
                Last["Num"] = 0;
                foreach (Hashtable item in result)
                {
                    Last["Num"] = decimal.Parse(Last["Num"].ToString()) + decimal.Parse(item["Num"].ToString());
                }
                result.Add(Last);
            }
            return result;
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
        public static object sfc00023GetList(string Token, string BatchNo, string ItemStar, string ItemEnd, string DateStar, string DateEnd, int page, int rows)
        {
            int count = 0;
            IList<Hashtable> result = SFC_CompletionOrderService.sfc00023GetList(BatchNo, ItemStar, ItemEnd, DateStar, DateEnd, page, rows, ref count);
            return UniversalService.getPaginationModel(result, count);
        }
        #endregion
    }
}
