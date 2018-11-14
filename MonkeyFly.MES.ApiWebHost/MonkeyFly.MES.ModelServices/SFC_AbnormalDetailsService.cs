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
    public class SFC_AbnormalDetailsService : SuperModel<SFC_AbnormalDetails>
    {

        public static bool insert(string userId, SFC_AbnormalDetails Model)
        {
            try
            {
                Model.Status = Framework.SystemID + "0201213000001";
                string sql = string.Format(@"insert[SFC_AbnormalDetails]([AbnormalDetailID],[FabMoProcessID],
                [FabMoOperationID],[TaskDispatchID],[ReasonID],
                [StartTime],[EndTime],[EquipmentID],
                [Status],[Comments],[Modifier],
                [ModifiedTime],[ModifiedLocalTime],[Creator],
                [CreateTime],[CreateLocalTime],[SystemID]) values
                 (@AbnormalDetailID,@FabMoProcessID,
                @FabMoOperationID,@TaskDispatchID,@ReasonID,
                @StartTime,@EndTime,@EquipmentID,
                @Status,@Comments,'{0}','{1}','{2}','{0}','{1}','{2}','{3}')", userId, DateTime.Now, DateTime.Now, Framework.SystemID);

                SqlParameter[] parameters = new SqlParameter[]{
                    new SqlParameter("@AbnormalDetailID",SqlDbType.VarChar),
                    new SqlParameter("@FabMoProcessID",SqlDbType.VarChar),
                    new SqlParameter("@FabMoOperationID",SqlDbType.VarChar),
                    new SqlParameter("@TaskDispatchID",SqlDbType.VarChar),
                    new SqlParameter("@ReasonID",SqlDbType.VarChar),
                    new SqlParameter("@StartTime",SqlDbType.DateTime),
                    new SqlParameter("@EndTime",SqlDbType.DateTime),
                    new SqlParameter("@EquipmentID",SqlDbType.VarChar),
                    new SqlParameter("@Status",SqlDbType.VarChar),
                    new SqlParameter("@Comments",SqlDbType.NVarChar),
                    };

                parameters[0].Value = (Object)Model.AbnormalDetailID ?? DBNull.Value;
                parameters[1].Value = (Object)Model.FabMoProcessID ?? DBNull.Value;
                parameters[2].Value = (Object)Model.FabMoOperationID ?? DBNull.Value;
                parameters[3].Value = (Object)Model.TaskDispatchID ?? DBNull.Value;
                parameters[4].Value = (Object)Model.ReasonID ?? DBNull.Value;
                parameters[5].Value = (Object)Model.StartTime ?? DBNull.Value;
                parameters[6].Value = (Object)Model.EndTime ?? DBNull.Value;
                parameters[7].Value = (Object)Model.EquipmentID ?? DBNull.Value;
                parameters[8].Value = (Object)Model.Status ?? DBNull.Value;
                parameters[9].Value = (Object)Model.Comments ?? DBNull.Value;

                return SQLHelper.ExecuteNonQuery(sql, CommandType.Text, parameters) > 0;
            }
            catch (Exception ex)
            {
                DataLogerService.writeerrlog(ex);
                return false;
            }
        }

        public static bool update(string userId, SFC_AbnormalDetails Model)
        {
            try
            {
                string sql = String.Format(@"update[SFC_AbnormalDetails] set {0},
                [FabMoProcessID]=@FabMoProcessID,[FabMoOperationID]=@FabMoOperationID,[TaskDispatchID]=@TaskDispatchID,
                [ReasonID]=@ReasonID,[StartTime]=@StartTime,[EndTime]=@EndTime,[EquipmentID]=@EquipmentID,
                [Status]=@Status,[Comments]=@Comments
                where [AbnormalDetailID]=@AbnormalDetailID", UniversalService.getUpdateUTC(userId));
                SqlParameter[] parameters = new SqlParameter[]{
                    new SqlParameter("@AbnormalDetailID",SqlDbType.VarChar),
                    new SqlParameter("@FabMoProcessID",SqlDbType.VarChar),
                    new SqlParameter("@FabMoOperationID",SqlDbType.VarChar),
                    new SqlParameter("@TaskDispatchID",SqlDbType.VarChar),
                    new SqlParameter("@ReasonID",SqlDbType.VarChar),
                    new SqlParameter("@StartTime",SqlDbType.DateTime),
                    new SqlParameter("@EndTime",SqlDbType.DateTime),
                    new SqlParameter("@EquipmentID",SqlDbType.VarChar),
                    new SqlParameter("@Status",SqlDbType.VarChar),
                    new SqlParameter("@Comments",SqlDbType.NVarChar),
                    };

                parameters[0].Value = Model.AbnormalDetailID;
                parameters[1].Value = (Object)Model.FabMoProcessID ?? DBNull.Value;
                parameters[2].Value = (Object)Model.FabMoOperationID ?? DBNull.Value;
                parameters[3].Value = (Object)Model.TaskDispatchID ?? DBNull.Value;
                parameters[4].Value = (Object)Model.ReasonID ?? DBNull.Value;
                parameters[5].Value = (Object)Model.StartTime ?? DBNull.Value;
                parameters[6].Value = (Object)Model.EndTime ?? DBNull.Value;
                parameters[7].Value = (Object)Model.EquipmentID ?? DBNull.Value;
                parameters[8].Value = (Object)Model.Status ?? DBNull.Value;
                parameters[9].Value = (Object)Model.Comments ?? DBNull.Value;

                return SQLHelper.ExecuteNonQuery(sql, CommandType.Text, parameters) > 0;
            }
            catch (Exception ex)
            {
                DataLogerService.writeerrlog(ex);
                return false;
            }
        }

        public static SFC_AbnormalDetails get(string AbnormalDetailID)
        {
            string sql = string.Format(@"select Top 1 * from [SFC_AbnormalDetails] where [AbnormalDetailID] = '{0}'  and [SystemID] = '{1}' ", AbnormalDetailID, Framework.SystemID);

            DataTable dt = SQLHelper.ExecuteDataTable(sql, CommandType.Text);

            if (dt.Rows.Count == 0)
                return null;
            else
                return ToEntity(dt.Rows[0]);
        }

        public static bool delete(string AbnormalDetailID)
        {
            try
            {
                string sql = string.Format(@"delete from [SFC_AbnormalDetails] where [AbnormalDetailID] = '{0}'  and [SystemID] = '{1}' ", AbnormalDetailID, Framework.SystemID);

                return SQLHelper.ExecuteNonQuery(sql, CommandType.Text) > 0;
            }

            catch (Exception ex)
            {

                DataLogerService.writeerrlog(ex);
                return false;
            }
        }
        /// <summary>
        /// 根据任务单流水号获取异常列表
        /// TOM
        /// </summary>
        /// <param name="TaskDispatchID"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public static IList<Hashtable> GetList(string TaskDispatchID, int page, int rows, ref int count)
        {
            string select = string.Format(
              @"select A.[AbnormalDetailID],A.[FabMoProcessID],A.[FabMoOperationID],A.[TaskDispatchID],
                       A.[ReasonID],E.Code as ReasonCode,E.Name as ReasonDesc, 
                       A.EquipmentID, F.Code as EquipmentCode, F.Name as EquipmentName, F.ResourceCategory as ResourceCategoryID, G.Name as ResourceCategory,
                       A.[StartTime],A.[EndTime],A.[Status],A.[Comments],
                       B.Emplno+'-'+B.UserName as Creator,A.CreateLocalTime as CreateTime,
                       (CASE WHEN A.CreateLocalTime=A.ModifiedLocalTime THEN NULL else C.Emplno+'-'+C.UserName END) as Modifier,
                       (CASE WHEN A.CreateLocalTime=A.ModifiedLocalTime THEN NULL else A.ModifiedLocalTime END) as ModifiedTime ", Framework.SystemID);

            string sql = string.Format(
                @"from [SFC_AbnormalDetails] A               
                 left join [SYS_MESUsers] B on A.[Creator] = B.[MESUserID]
                 left join [SYS_MESUsers] C on A.[Modifier] = C.[MESUserID]
                 left join [SFC_TaskDispatchResource] D on A.TaskDispatchID = D.TaskDispatchID and D.EquipmentID = A.EquipmentID and D.Status = '{0}0201213000001'
                 left join [SYS_Parameters] E on A.[ReasonID] = E.[ParameterID]
                 left join [EMS_Equipment] F on A.[EquipmentID] = F.[EquipmentID]
                 left join [SYS_Parameters] G on D.[ResourceClassID] = G.[ParameterID]
                 where A.[SystemID] = '{0}' and A.[Status] <> '{0}0201213000003' and 
                       A.[TaskDispatchID]='{1}'", Framework.SystemID, TaskDispatchID);

            count = UniversalService.getCount(sql, null);

            string orderby = "A.CreateTime desc";

            DataTable dt = UniversalService.getTable(select, sql, orderby, null, page, rows);

            return ToHashtableList(dt);
        }

        public static bool delete(string uid, string AbnormalDetailID)
        {
            try
            {
                string orgID = AbnormalDetailID;
                SFC_AbnormalDetails m = get(orgID);
                if (m != null)
                {
                    m.Status = Framework.SystemID + "0201213000003";
                    return update(uid, m);
                }
                return true;
            }
            catch (Exception ex)
            {
                DataLogerService.writeerrlog(ex);
                return false;
            }
        }

        public static bool CheckInsertArgs(SFC_AbnormalDetails model)
        {
            string sql = string.Format(
                @"from SFC_AbnormalDetails
                  where TaskDispatchID = '{0}' and EquipmentID = '{1}' and ReasonID = '{3}' and Status <> '{2}0201213000003'",
                model.TaskDispatchID, model.EquipmentID, Framework.SystemID, model.ReasonID);

            return UniversalService.getCount(sql, null) <= 0;
        }

        public static bool CheckUpdateArgs(SFC_AbnormalDetails model)
        {
            string sql = string.Format(
                @"from SFC_AbnormalDetails
                  where [TaskDispatchID] = '{0}' and [EquipmentID] = '{1}' and [ReasonID] = '{4}' and 
                        [AbnormalDetailID] <> '{2}' and [Status] <> '{3}0201213000003'",
                model.TaskDispatchID, model.EquipmentID, model.AbnormalDetailID, Framework.SystemID, model.ReasonID);

            return UniversalService.getCount(sql, null) <= 0;
        }
    }
}

