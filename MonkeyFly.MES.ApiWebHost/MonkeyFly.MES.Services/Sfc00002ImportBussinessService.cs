using MonkeyFly.Core;
using MonkeyFly.MES.BasicService;
using MonkeyFly.MES.Models;
using MonkeyFly.MES.ModelServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace MonkeyFly.MES.Services
{
    /// <summary>
    /// 制令单导入的专属类
    /// </summary>
    public class Sfc00002ImportBussinessService
    {
        private static IList<SFC_ItemProcess> ItemProcessList = null;
        private static IList<SFC_ItemProcessRelationShip> ItemProcessShipList = null;
        private static IList<SFC_ItemOperation> ItemOperationList = null;
        private static IList<SFC_ItemMaterial> ItemMaterialList = null;
        private static IList<SFC_ItemResource> ItemResourceList = null;
        private static IList<SFC_ProcessOperationRelationShip> ItemOperationShipList = null;
        private static IList<SYS_Parameters> PraLilst = null;

        /// <summary>
        /// 初始化各变量
        /// SAM 2017年10月6日13:53:34
        /// </summary>
        public static void Initialization()
        {
            ItemProcessList = SFC_ItemProcessService.GetList();
            ItemProcessShipList = SFC_ItemProcessRelationShipService.GetList();
            ItemOperationList = SFC_ItemOperationService.GetList();
            ItemMaterialList = SFC_ItemMaterialService.GetList();
            ItemResourceList = SFC_ItemResourceService.GetList();
            ItemOperationShipList = SFC_ProcessOperationRelationShipService.GetList();
            PraLilst = SYS_ParameterService.GetList(Framework.SystemID + "0191213000002','" + Framework.SystemID + "0191213000017");
        }


        /// <summary>
        /// 制令单的导入
        /// SAM 2017年10月8日16:25:24
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="model"></param>
        /// <param name="itemmodel"></param>
        /// <param name="AddReason"></param>
        /// <returns></returns>
        public static bool FabMo(string userId, SFC_FabricatedMother model, SYS_Items itemmodel, ref string AddReason)
        {
            try
            {
                IList<SFC_ItemProcess> FabIPList = ItemProcessList.Where(w => w.ItemID == model.ItemID).ToList();
                if (FabIPList.Count == 0)
                {
                    AddReason = "制令单号" + model.MoNo + "不存在制品制程！";
                    return false;
                }

                using (TransactionScope ts = new TransactionScope())
                {
                    if (SFC_FabricatedMotherService.insert(userId, model))
                    {
                        SFC_FabMoProcess Promodel = null;
                        SFC_FabMoItem Matmodel = null;
                        SFC_FabMoResource Resmodel = null;
                        SFC_FabMoOperation Opmodel = null;
                        SYS_Parameters Parmodel = null;
                        IList<SFC_ItemMaterial> FabIMList = null;
                        IList<SFC_ItemResource> FabIRList = null;
                        IList<SFC_ItemOperation> FabIOList = null;
                        string Sql = null;
                        //IList<SFC_ItemProcess> FabIPList = ItemProcessList.Where(w => w.ItemID == model.ItemID).ToList();
                        if (FabIPList.Count != 0)
                        {
                            List<string> FabMoProcessIDs = UniversalService.GetSerialNumber("SFC_FabMoProcess", FabIPList.Count);
                            for (int P = 0; P < FabIPList.Count; P++)
                            {
                                Promodel = new SFC_FabMoProcess
                                {
                                    FabMoProcessID = FabMoProcessIDs[P],
                                    FabricatedMotherID = model.FabricatedMotherID,
                                    Quantity = model.Quantity,
                                    Sequence = FabIPList[P].Sequence,
                                    Status = model.Status,
                                    ProcessID = FabIPList[P].ProcessID,
                                    WorkCenterID = FabIPList[P].WorkCenterID,
                                    StartDate = model.StartDate,
                                    FinishDate = model.FinishDate,
                                    UnitID = model.UnitID,
                                    UnitRate = 1,
                                    StandardTime = (int)Math.Round((decimal)(FabIPList[P].StandardTime == null ? 0 : FabIPList[P].StandardTime) * model.Quantity, 0, MidpointRounding.AwayFromZero),
                                    PrepareTime = FabIPList[P].PrepareTime,
                                    SourceID = FabIPList[P].ItemProcessID//用于查询关系
                                };
                                try//是否启用工序
                                {
                                    Parmodel = PraLilst.Where(w => w.ParameterID == Promodel.ProcessID && w.ParameterTypeID == Framework.SystemID + "0191213000017").FirstOrDefault();
                                    if (Parmodel != null)
                                        Promodel.IsEnableOperation = Parmodel.IsDefault;
                                    Parmodel = null;
                                }
                                catch (Exception ex)
                                {
                                    DataLogerService.writeerrlog(ex);
                                }
                                Sql += SFC_FabMoProcessService.InsertSQL(userId, Promodel);

                                //根据制程获取制程用料
                                FabIMList = ItemMaterialList.Where(w => w.ItemProcessID == FabIPList[P].ItemProcessID).ToList();
                                if (FabIMList.Count != 0)
                                {
                                    List<string> FabMoItemIDs = UniversalService.GetSerialNumber("SFC_FabMoItem", FabIMList.Count);
                                    for (int i = 0; i < FabIMList.Count; i++)
                                    {
                                        Matmodel = new SFC_FabMoItem
                                        {
                                            FabMoItemID = FabMoItemIDs[i],
                                            FabMoProcessID = Promodel.FabMoProcessID,
                                            Sequence = FabIMList[i].Sequence.ToString(),
                                            ItemID = FabIMList[i].ItemID,
                                            BaseQuantity = model.Quantity * FabIMList[i].BasicQuantity,
                                            AttritionRate = FabIMList[i].AttritionRate,
                                            UseQuantity = FabIMList[i].UseQuantity * model.Quantity,
                                            Status = Framework.SystemID + "0201213000001",
                                            Comments = FabIMList[i].Comments
                                        };
                                        if (itemmodel.IsCutMantissa)//是否切除尾数
                                        {
                                            Parmodel = PraLilst.Where(w => w.ParameterID == itemmodel.CutMantissa && w.ParameterTypeID == Framework.SystemID + "0191213000002").FirstOrDefault();
                                            if (Parmodel != null)
                                            {
                                                Matmodel.BaseQuantity = Math.Round(Matmodel.BaseQuantity, Parmodel.Code == "N" ? 0 : int.Parse(Parmodel.Code));
                                                Matmodel.UseQuantity = Math.Round(Matmodel.UseQuantity, Parmodel.Code == "N" ? 0 : int.Parse(Parmodel.Code));
                                            }
                                        }
                                        Sql += SFC_FabMoItemService.InsertSQL(userId, Matmodel);
                                    }
                                }
                                //根据制程获取制程资源
                                FabIRList = ItemResourceList.Where(w => w.ItemProcessID == FabIPList[P].ItemProcessID).ToList();
                                if (FabIRList.Count != 0)
                                {
                                    List<string> FabMoResourceIDs = UniversalService.GetSerialNumber("SFC_FabMoResource", FabIRList.Count);
                                    for (int i = 0; i < FabIRList.Count; i++)
                                    {
                                        Resmodel = new SFC_FabMoResource
                                        {
                                            FabMoResourceID = FabMoResourceIDs[i],
                                            FabMoProcessID = Promodel.FabMoProcessID,
                                            ResourceID = FabIRList[i].ResourceID,
                                            Type = FabIRList[i].Type,
                                            IfMain = FabIRList[i].IfMain,
                                            Status = Framework.SystemID + "0201213000001",
                                            Comments = FabIRList[i].Comments
                                        };
                                        Sql += SFC_FabMoResourceService.InsertSQL(userId, Resmodel);
                                    }
                                }
                                //根据制程获取制程工序
                                FabIOList = ItemOperationList.Where(w => w.ItemProcessID == FabIPList[P].ItemProcessID).ToList();
                                if (FabIOList.Count != 0)
                                {
                                    List<string> FabMoOperationIDs = UniversalService.GetSerialNumber("SFC_FabMoOperation", FabIOList.Count);
                                    for (int O = 0; O < FabIOList.Count; O++)
                                    {
                                        Opmodel = new SFC_FabMoOperation
                                        {
                                            FabMoOperationID = FabMoOperationIDs[O],
                                            FabricatedMotherID = Promodel.FabricatedMotherID,
                                            FabMoProcessID = Promodel.FabMoProcessID,
                                            OperationID = FabIOList[O].OperationID,
                                            Sequence = FabIOList[O].Sequence,
                                            UnitID = FabIOList[O].Unit,
                                            UnitRate = FabIOList[O].UnitRatio,
                                            Quantity = Promodel.Quantity,
                                            StandardTime = (int)Math.Round(FabIOList[O].StandardTime * model.Quantity, 0, MidpointRounding.AwayFromZero),
                                            PrepareTime = FabIOList[O].PrepareTime,
                                            StartDate = Promodel.StartDate,
                                            FinishDate = Promodel.FinishDate,
                                            Status = model.Status,
                                            Comments = FabIOList[O].Comments,
                                            SourceID = FabIOList[O].ItemOperationID
                                        };
                                        Sql += SFC_FabMoOperationService.InsertSQL(userId, Opmodel);
                                        //根据制程工序获取工序用料
                                        FabIMList = ItemMaterialList.Where(w => w.ItemOperationID == FabIOList[O].ItemOperationID).ToList();
                                        if (FabIMList.Count != 0)
                                        {
                                            List<string> FabMoItemIDs = UniversalService.GetSerialNumber("SFC_FabMoItem", FabIMList.Count);
                                            for (int i = 0; i < FabIMList.Count; i++)
                                            {
                                                Matmodel = new SFC_FabMoItem
                                                {
                                                    FabMoItemID = FabMoItemIDs[i],
                                                    FabMoOperationID = Opmodel.FabMoOperationID,
                                                    Sequence = FabIMList[i].Sequence.ToString(),
                                                    ItemID = FabIMList[i].ItemID,
                                                    BaseQuantity = FabIMList[i].BasicQuantity * model.Quantity,
                                                    AttritionRate = FabIMList[i].AttritionRate,
                                                    UseQuantity = FabIMList[i].UseQuantity * model.Quantity,
                                                    Status = Framework.SystemID + "0201213000001",
                                                    Comments = FabIMList[i].Comments
                                                };
                                                if (itemmodel.IsCutMantissa)//是否切除尾数
                                                {
                                                    Parmodel = PraLilst.Where(w => w.ParameterID == itemmodel.CutMantissa).FirstOrDefault();
                                                    if (Parmodel != null)
                                                    {
                                                        Matmodel.BaseQuantity = Math.Round(Matmodel.BaseQuantity, Parmodel.Code == "N" ? 0 : int.Parse(Parmodel.Code));
                                                        Matmodel.UseQuantity = Math.Round(Matmodel.UseQuantity, Parmodel.Code == "N" ? 0 : int.Parse(Parmodel.Code));
                                                    }
                                                }
                                                Sql += SFC_FabMoItemService.InsertSQL(userId, Matmodel);
                                            }
                                        }
                                        //根据制程工序获取工序资源
                                        FabIRList = ItemResourceList.Where(w => w.ItemOperationID == FabIOList[O].ItemOperationID).ToList();
                                        if (FabIRList.Count != 0)
                                        {
                                            List<string> FabMoResourceIDs = UniversalService.GetSerialNumber("SFC_FabMoResource", FabIRList.Count);
                                            for (int i = 0; i < FabIRList.Count; i++)
                                            {
                                                Resmodel = new SFC_FabMoResource
                                                {
                                                    FabMoResourceID = FabMoResourceIDs[i],
                                                    FabMoOperationID = Opmodel.FabMoOperationID,
                                                    ResourceID = FabIRList[i].ResourceID,
                                                    Type = FabIRList[i].Type,
                                                    IfMain = FabIRList[i].IfMain,
                                                    Status = Framework.SystemID + "0201213000001",
                                                    Comments = FabIRList[i].Comments
                                                };
                                                Sql += SFC_FabMoResourceService.InsertSQL(userId, Resmodel);
                                            }
                                        }
                                    }
                                }
                            }
                        }/*制程添加的循环在这里结束*/
                        if (!string.IsNullOrWhiteSpace(Sql))
                        {
                            if (UtilBussinessService.RunSQL(Sql))
                            {
                                string sql = null;
                                //然后循环制程列表，根据每一道制程对应的制品制程，获取设定好的制品制程关系列表
                                //循环关系列表，再新增成制令单的制程关系
                                SFC_FabMoProcess ProModel = null;
                                SFC_FabMoRelationship FabResmodel = null;
                                IList<SFC_FabMoProcess> FabMoProcessList = SFC_FabMoProcessService.GetList(model.FabricatedMotherID);
                                IList<SFC_FabMoOperation> FabMoOperationList = SFC_FabMoOperationService.GetList(model.FabricatedMotherID);
                                IList<SFC_ItemProcessRelationShip> FIPSList = ItemProcessShipList.Where(w => w.ItemID == model.ItemID).ToList();
                                if (FIPSList.Count != 0)
                                {
                                    List<string> FabMoRelationshipIDs = UniversalService.GetSerialNumber("SFC_FabMoRelationship", FIPSList.Count);
                                    for (int i = 0; i < FIPSList.Count; i++)
                                    {
                                        FabResmodel = new SFC_FabMoRelationship
                                        {
                                            FabMoRelationshipID = FabMoRelationshipIDs[i],
                                            FabricatedMotherID = model.FabricatedMotherID,
                                            IfLastProcess = FIPSList[i].FinishProcess,
                                            IfMain = FIPSList[i].IfMain,
                                            Status = Framework.SystemID + "0201213000001",
                                            Comments = FIPSList[i].Comments
                                        };
                                        ProModel = FabMoProcessList.Where(w => w.FabricatedMotherID == model.FabricatedMotherID && w.SourceID == FIPSList[i].ItemProcessID).FirstOrDefault();
                                        if (ProModel == null)
                                            continue;
                                        FabResmodel.FabMoProcessID = ProModel.FabMoProcessID;
                                        ProModel = FabMoProcessList.Where(w => w.FabricatedMotherID == model.FabricatedMotherID && w.SourceID == FIPSList[i].PreItemProcessID).FirstOrDefault();
                                        if (ProModel == null)
                                            continue;
                                        FabResmodel.PreFabMoProcessID = ProModel.FabMoProcessID;
                                        sql += SFC_FabMoRelationshipService.InsertSQL(userId, FabResmodel);
                                    }
                                }
                                for (int X = 0; X < FabIPList.Count; X++)
                                {
                                    //首先根据制令制程获取刚刚添加好的制令制程工序列表
                                    //然后循环工序列表，根据每一道工序对应的制品制程工序，获取设定好的关系列表
                                    //循环关系列表，再新增成制令单的制程工序关系
                                    SFC_FabMoOperation OpModel = null;
                                    SFC_FabMoOperationRelationship FabOpResmodel = null;
                                    IList<SFC_ProcessOperationRelationShip> FPORSList = ItemOperationShipList.Where(w => w.ItemProcessID == FabIPList[X].ItemProcessID).ToList();
                                    if (FPORSList.Count != 0)
                                    {
                                        List<string> FabMoOperationRelationshipIDs = UniversalService.GetSerialNumber("SFC_FabMoOperationRelationship", FPORSList.Count);
                                        for (int i = 0; i < FPORSList.Count; i++)
                                        {
                                            FabOpResmodel = new SFC_FabMoOperationRelationship
                                            {
                                                FabMoOperationRelationshipID = UniversalService.GetSerialNumber("SFC_FabMoOperationRelationship"),
                                                FabricatedMotherID = Promodel.FabricatedMotherID,
                                                FabMoProcessID = Promodel.FabMoProcessID,
                                                IsLastOperation = FPORSList[i].FinishOperation,
                                                IfMain = FPORSList[i].IfMain,
                                                Status = Framework.SystemID + "0201213000001",
                                                Comments = FPORSList[i].Comments
                                            };

                                            OpModel = FabMoOperationList.Where(w => w.FabMoProcessID == FabOpResmodel.FabMoProcessID && w.SourceID == FPORSList[i].ItemOperationID).FirstOrDefault();
                                            if (OpModel == null)
                                                continue;
                                            FabOpResmodel.FabMoOperationID = OpModel.FabMoOperationID;
                                            OpModel = FabMoOperationList.Where(w => w.FabMoProcessID == FabOpResmodel.FabMoProcessID && w.SourceID == FPORSList[i].PreItemOperationID).FirstOrDefault();
                                            if (OpModel == null)
                                                continue;
                                            FabOpResmodel.PreFabMoOperationID = OpModel.FabMoOperationID;
                                            sql += SFC_FabMoOperationRelationshipService.InsertSQL(userId, FabOpResmodel);
                                        }
                                    }
                                }
                                if (!string.IsNullOrWhiteSpace(sql))
                                    UtilBussinessService.RunSQL(sql);
                            }
                        }
                    }
                    else
                    {
                        AddReason = "制令单添加失败！请研发人员检查对应代码！";
                        return false;
                    }
                    ts.Complete();
                    return true;
                }
            }
            catch (Exception ex)
            {
                DataLogerService.writeerrlog(ex);
                AddReason = ex.ToString();
                return false;
            }
        }
    }
}
