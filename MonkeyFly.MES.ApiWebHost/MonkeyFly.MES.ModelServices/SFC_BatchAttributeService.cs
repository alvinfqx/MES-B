using MonkeyFly.Core;
using MonkeyFly.MES.BasicService;
using MonkeyFly.MES.Models;
using MonkeyFly.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace MonkeyFly.MES.ModelServices
{
    public class SFC_BatchAttributeService : SuperModel<SFC_BatchAttribute>
    {

        public static bool insert(string userId, SFC_BatchAttribute Model)
        {
            try
            {
                string sql = string.Format(@"insert[SFC_BatchAttribute]([BatchAttributeID],[CompletionOrderID],
            [Sequence],[BatchNo],[EffectDate],
            [Quantity],[AutoNumberRecordID],[Status],
            [Comments],[Modifier],[ModifiedTime],
            [ModifiedLocalTime],[Creator],[CreateTime],
            [CreateLocalTime],[SystemID]) values
             (@BatchAttributeID,@CompletionOrderID,
            @Sequence,@BatchNo,@EffectDate,
            @Quantity,@AutoNumberRecordID,@Status,
            @Comments,'{0}','{1}','{2}','{0}','{1}','{2}','{3}')", userId, DateTime.Now, DateTime.Now, Framework.SystemID);

                SqlParameter[] parameters = new SqlParameter[]{
                    new SqlParameter("@BatchAttributeID",SqlDbType.VarChar),
                    new SqlParameter("@CompletionOrderID",SqlDbType.VarChar),
                    new SqlParameter("@Sequence",SqlDbType.Int),
                    new SqlParameter("@BatchNo",SqlDbType.NVarChar),
                    new SqlParameter("@EffectDate",SqlDbType.DateTime),
                    new SqlParameter("@Quantity",SqlDbType.Decimal),
                    new SqlParameter("@AutoNumberRecordID",SqlDbType.VarChar),
                    new SqlParameter("@Status",SqlDbType.VarChar),
                    new SqlParameter("@Comments",SqlDbType.NVarChar),
                    };

                parameters[0].Value = (Object)Model.BatchAttributeID ?? DBNull.Value;
                parameters[1].Value = (Object)Model.CompletionOrderID ?? DBNull.Value;
                parameters[2].Value = (Object)Model.Sequence ?? DBNull.Value;
                parameters[3].Value = (Object)Model.BatchNo ?? DBNull.Value;
                parameters[4].Value = (Object)Model.EffectDate ?? DBNull.Value;
                parameters[5].Value = (Object)Model.Quantity ?? DBNull.Value;
                parameters[6].Value = (Object)Model.AutoNumberRecordID ?? DBNull.Value;
                parameters[7].Value = (Object)Model.Status ?? DBNull.Value;
                parameters[8].Value = (Object)Model.Comments ?? DBNull.Value;

                return SQLHelper.ExecuteNonQuery(sql, CommandType.Text, parameters) > 0;
            }
            catch (Exception ex)
            {
                DataLogerService.writeerrlog(ex);
                return false;
            }
        }

        public static bool update(string userId, SFC_BatchAttribute Model)
        {
            try
            {
                string sql = String.Format(@"update[SFC_BatchAttribute] set {0},
[Sequence]=@Sequence,[BatchNo]=@BatchNo,
[EffectDate]=@EffectDate,[Quantity]=@Quantity,[AutoNumberRecordID]=@AutoNumberRecordID,[Status]=@Status,
[Comments]=@Comments where [BatchAttributeID]=@BatchAttributeID", UniversalService.getUpdateUTC(userId));
                SqlParameter[] parameters = new SqlParameter[]{
                    new SqlParameter("@BatchAttributeID",SqlDbType.VarChar),
                    new SqlParameter("@Sequence",SqlDbType.Int),
                    new SqlParameter("@BatchNo",SqlDbType.NVarChar),
                    new SqlParameter("@EffectDate",SqlDbType.DateTime),
                    new SqlParameter("@Quantity",SqlDbType.Decimal),
                    new SqlParameter("@AutoNumberRecordID",SqlDbType.VarChar),
                    new SqlParameter("@Status",SqlDbType.VarChar),
                    new SqlParameter("@Comments",SqlDbType.NVarChar),
                    };

                parameters[0].Value = Model.BatchAttributeID;
                parameters[1].Value = (Object)Model.Sequence ?? DBNull.Value;
                parameters[2].Value = (Object)Model.BatchNo ?? DBNull.Value;
                parameters[3].Value = (Object)Model.EffectDate ?? DBNull.Value;
                parameters[4].Value = (Object)Model.Quantity ?? DBNull.Value;
                parameters[5].Value = (Object)Model.AutoNumberRecordID ?? DBNull.Value;
                parameters[6].Value = (Object)Model.Status ?? DBNull.Value;
                parameters[7].Value = (Object)Model.Comments ?? DBNull.Value;

                return SQLHelper.ExecuteNonQuery(sql, CommandType.Text, parameters) > 0;
            }
            catch (Exception ex)
            {
                DataLogerService.writeerrlog(ex);
                return false;
            }
        }

        public static SFC_BatchAttribute get(string BatchAttributeID)
        {
            string sql = string.Format(@"select Top 1 * from [SFC_BatchAttribute] where [BatchAttributeID] = '{0}'  and [SystemID] = '{1}' ", BatchAttributeID, Framework.SystemID);

            DataTable dt = SQLHelper.ExecuteDataTable(sql, CommandType.Text);

            if (dt.Rows.Count == 0)
                return null;
            else
                return ToEntity(dt.Rows[0]);
        }

        /// <summary>
        /// 完工單回報作業-批號列表
        /// SAM 2017年7月20日10:05:35
        /// </summary>
        /// <param name="CompletionOrderID"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public static IList<Hashtable> Sfc00007GetLotList(string CompletionOrderID, int page, int rows, ref int count)
        {
            string select = string.Format(
             @"select A.BatchAttributeID,A.CompletionOrderID,A.Sequence,A.BatchNo,A.EffectDate,A.Quantity,A.AutoNumberRecordID,A.Status,
           D.CompletionNo,E.TaskNo,E.ItemID,(Select Code From [SYS_Items] where E.ItemID = ItemID) as ItemCode,
            (Select Lot From [SYS_Items] where E.ItemID = ItemID) as LotControl,
            B.Emplno+'-'+B.UserName as Creator,A.CreateLocalTime as CreateTime,
            (CASE WHEN A.CreateLocalTime=A.ModifiedLocalTime THEN NULL else C.Emplno+'-'+C.UserName END) as Modifier,
            (CASE WHEN A.CreateLocalTime=A.ModifiedLocalTime THEN NULL else A.ModifiedLocalTime END) as ModifiedTime ", Framework.SystemID);
            string sql = string.Format(
                @"from [SFC_BatchAttribute] A               
                left join [SYS_MESUsers] B on A.[Creator] = B.[MESUserID]
                left join [SYS_MESUsers] C on A.[Modifier] =C.[MESUserID]
                left join [SFC_CompletionOrder] D on A.[CompletionOrderID] =D.[CompletionOrderID]
                left join [SFC_TaskDispatch] E on D.[TaskDispatchID] =E.[TaskDispatchID]
                where A.[SystemID] = '{0}' and A.[Status] = '{0}0201213000001' and A.[CompletionOrderID]='{1}'", Framework.SystemID, CompletionOrderID);

            count = UniversalService.getCount(sql, null);

            string orderby = "A.[Sequence] ";

            DataTable dt = UniversalService.getTable(select, sql, orderby, null, page, rows);

            return ToHashtableList(dt);
        }

        /// <summary>
        /// 获取批号记录明细
        /// SAM 2017年7月27日16:35:53
        /// </summary>
        /// <param name="AutoNumberRecordID"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public static IList<Hashtable> Inf00023RecordDetailList(string AutoNumberRecordID, int page, int rows, ref int count)
        {
            string select = string.Format(
             @"select A.BatchAttributeID,A.CompletionOrderID,A.Sequence,A.BatchNo,A.EffectDate,A.Quantity,
            (select Name from [SYS_Parameters] where ParameterID = D.Status) as Status,
           D.CompletionNo,E.TaskNo,E.ItemID,(Select Code From [SYS_Items] where E.ItemID = ItemID) as ItemCode,F.MoNo,F.SplitSequence,
            (Select Lot From [SYS_Items] where E.ItemID = ItemID) as LotControl,
            B.Emplno+'-'+B.UserName as Creator,A.CreateLocalTime as CreateTime,
            (CASE WHEN A.CreateLocalTime=A.ModifiedLocalTime THEN NULL else C.Emplno+'-'+C.UserName END) as Modifier,
            (CASE WHEN A.CreateLocalTime=A.ModifiedLocalTime THEN NULL else A.ModifiedLocalTime END) as ModifiedTime ", Framework.SystemID);
            string sql = string.Format(
                @"from [SFC_BatchAttribute] A               
                left join [SYS_MESUsers] B on A.[Creator] = B.[MESUserID]
                left join [SYS_MESUsers] C on A.[Modifier] =C.[MESUserID]
                left join [SFC_CompletionOrder] D on A.[CompletionOrderID] =D.[CompletionOrderID]
                left join [SFC_TaskDispatch] E on D.[TaskDispatchID] =E.[TaskDispatchID]
                left join [SFC_FabricatedMother] F on D.[FabricatedMotherID] =F.[FabricatedMotherID]
                where A.[SystemID] = '{0}' and A.[Status] = '{0}0201213000001' and A.[AutoNumberRecordID]='{1}'", Framework.SystemID, AutoNumberRecordID);

            count = UniversalService.getCount(sql, null);

            string orderby = "A.[Sequence] ";

            DataTable dt = UniversalService.getTable(select, sql, orderby, null, page, rows);

            return ToHashtableList(dt);
        }


        public static IList<Hashtable> Sfc00007CheckLot(string CompletionOrderID)
        {
            string sql = string.Format(
                @"select A.* from [SFC_BatchAttribute] A               
                where A.[SystemID] = '{0}' 
            and A.[Status] = '{0}0201213000001' and A.[CompletionOrderID]='{1}'", Framework.SystemID, CompletionOrderID);

            DataTable dt = SQLHelper.ExecuteDataTable(sql, CommandType.Text);

            if (dt.Rows.Count == 0)
                return null;

            return ToHashtableList(dt);
        }
    }
}

