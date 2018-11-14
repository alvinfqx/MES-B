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
    public class SFC_AbnormalHourService : SuperModel<SFC_AbnormalHour>
    {
        /// <summary>
        /// 新增
        /// SAM 2017年7月26日21:27:09
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="Model"></param>
        /// <returns></returns>
        public static bool insert(string userId, SFC_AbnormalHour Model)
        {
            try
            {
                string sql = string.Format(@"insert[SFC_AbnormalHour]([AbnormalHourID],[CompletionOrderID],
                [Sequence],[Type],[ReasonID],
                [Hour],[Status],[GroupID],[Comments],
                [Modifier],[ModifiedTime],[ModifiedLocalTime],
                [Creator],[CreateTime],[CreateLocalTime],[SystemID]) values
                 (@AbnormalHourID,@CompletionOrderID,
                @Sequence,@Type,@ReasonID,
                @Hour,@Status,@GroupID,@Comments,'{0}','{1}','{2}','{0}','{1}','{2}','{3}')", userId, DateTime.Now, DateTime.Now, Framework.SystemID);

                SqlParameter[] parameters = new SqlParameter[]{
                    new SqlParameter("@AbnormalHourID",SqlDbType.VarChar),
                    new SqlParameter("@CompletionOrderID",SqlDbType.VarChar),
                    new SqlParameter("@Sequence",SqlDbType.Int),
                    new SqlParameter("@Type",SqlDbType.VarChar),
                    new SqlParameter("@ReasonID",SqlDbType.VarChar),
                    new SqlParameter("@Hour",SqlDbType.BigInt),
                    new SqlParameter("@Status",SqlDbType.VarChar),
                    new SqlParameter("@Comments",SqlDbType.NVarChar),
                    new SqlParameter("@GroupID",SqlDbType.NVarChar)
                    };

                parameters[0].Value = (Object)Model.AbnormalHourID ?? DBNull.Value;
                parameters[1].Value = (Object)Model.CompletionOrderID ?? DBNull.Value;
                parameters[2].Value = (Object)Model.Sequence ?? DBNull.Value;
                parameters[3].Value = (Object)Model.Type ?? DBNull.Value;
                parameters[4].Value = (Object)Model.ReasonID ?? DBNull.Value;
                parameters[5].Value = (Object)Model.Hour ?? DBNull.Value;
                parameters[6].Value = (Object)Model.Status ?? DBNull.Value;
                parameters[7].Value = (Object)Model.Comments ?? DBNull.Value;
                parameters[8].Value = (Object)Model.GroupID ?? DBNull.Value;

                return SQLHelper.ExecuteNonQuery(sql, CommandType.Text, parameters) > 0;
            }
            catch (Exception ex)
            {
                DataLogerService.writeerrlog(ex);
                return false;
            }
        }

        /// <summary>
        /// 更新
        /// SAM 2017年7月26日21:29:03
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="Model"></param>
        /// <returns></returns>
        public static bool update(string userId, SFC_AbnormalHour Model)
        {
            try
            {
                string sql = String.Format(@"update[SFC_AbnormalHour] set {0},
                [Sequence]=@Sequence,[Type]=@Type,[GroupID] = @GroupID,
                [ReasonID]=@ReasonID,[Hour]=@Hour,[Status]=@Status,[Comments]=@Comments 
                where [AbnormalHourID]=@AbnormalHourID", UniversalService.getUpdateUTC(userId));
                SqlParameter[] parameters = new SqlParameter[]{
                    new SqlParameter("@AbnormalHourID",SqlDbType.VarChar),
                    new SqlParameter("@Sequence",SqlDbType.Int),
                    new SqlParameter("@Type",SqlDbType.VarChar),
                    new SqlParameter("@ReasonID",SqlDbType.VarChar),
                    new SqlParameter("@Hour",SqlDbType.BigInt),
                    new SqlParameter("@Status",SqlDbType.VarChar),
                    new SqlParameter("@Comments",SqlDbType.NVarChar),
                    new SqlParameter("@GroupID",SqlDbType.VarChar)
                    };

                parameters[0].Value = Model.AbnormalHourID;
                parameters[1].Value = (Object)Model.Sequence ?? DBNull.Value;
                parameters[2].Value = (Object)Model.Type ?? DBNull.Value;
                parameters[3].Value = (Object)Model.ReasonID ?? DBNull.Value;
                parameters[4].Value = (Object)Model.Hour ?? DBNull.Value;
                parameters[5].Value = (Object)Model.Status ?? DBNull.Value;
                parameters[6].Value = (Object)Model.Comments ?? DBNull.Value;
                parameters[7].Value = (Object)Model.GroupID ?? DBNull.Value;

                return SQLHelper.ExecuteNonQuery(sql, CommandType.Text, parameters) > 0;
            }
            catch (Exception ex)
            {
                DataLogerService.writeerrlog(ex);
                return false;
            }
        }

        /// <summary>
        /// 获取单一实体
        /// SAM 2017年7月24日14:12:28
        /// </summary>
        /// <param name="AbnormalHourID"></param>
        /// <returns></returns>
        public static SFC_AbnormalHour get(string AbnormalHourID)
        {
            string sql = string.Format(@"select Top 1 * from [SFC_AbnormalHour] where [AbnormalHourID] = '{0}'  and [SystemID] = '{1}' ", AbnormalHourID, Framework.SystemID);

            DataTable dt = SQLHelper.ExecuteDataTable(sql, CommandType.Text);

            if (dt.Rows.Count == 0)
                return null;
            else
                return ToEntity(dt.Rows[0]);
        }

        /// <summary>
        /// 完工單回報作業-無效工時列表
        /// SAM 2017年7月20日10:03:12
        /// </summary>
        /// <param name="CompletionOrderID"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public static IList<Hashtable> Sfc00007GetUnusualHourList(string CompletionOrderID, int page, int rows, ref int count)
        {
            string select = string.Format(
             @"select A.AbnormalHourID,A.CompletionOrderID,A.Sequence,A.Type,A.ReasonID,A.Status,A.GroupID,
            convert(varchar(20),cast(A.Hour/3600 as int))+':'+right('00'+convert(varchar(20),A.Hour%3600/60),2)+':'+right('00'+convert(varchar(20),A.Hour%3600%60),2) as Hour,
            D.Code as ReasonCode,D.Name as ReasonDescription,
            E.Code as GroupCode,E.Name as GroupName,
            B.Emplno+'-'+B.UserName as Creator,A.CreateLocalTime as CreateTime,
            (CASE WHEN A.CreateLocalTime=A.ModifiedLocalTime THEN NULL else C.Emplno+'-'+C.UserName END) as Modifier,
            (CASE WHEN A.CreateLocalTime=A.ModifiedLocalTime THEN NULL else A.ModifiedLocalTime END) as ModifiedTime ", Framework.SystemID);
            string sql = string.Format(
                @"from [SFC_AbnormalHour] A               
                left join [SYS_MESUsers] B on A.[Creator] = B.[MESUserID]
                left join [SYS_MESUsers] C on A.[Modifier] =C.[MESUserID]
                left join [SYS_Parameters] D on A.[ReasonID] =D.[ParameterID]
                left join [SYS_Parameters] E on A.[GroupID] =E.[ParameterID]
                where A.[SystemID] = '{0}' and A.[Status] = '{0}0201213000001' and A.[CompletionOrderID]='{1}'", Framework.SystemID, CompletionOrderID);

            count = UniversalService.getCount(sql, null);

            string orderby = "A.[Sequence] ";

            DataTable dt = UniversalService.getTable(select, sql, orderby, null, page, rows);

            return ToHashtableList(dt);
        }


        /// <summary>
        /// 根据工作中心获取無效原因明細列表
        /// SAM 2017年7月22日20:18:26
        /// </summary>
        /// <param name="WorkCenterID"></param>
        /// <param name="Type">異常類別</param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public static IList<Hashtable> Sfc00016GetDetailList(string WorkCenterID, string Type,int page, int rows, ref int count)
        {
            string select = string.Format(
             @"select distinct 
            D.Code as ReasonCode,D.Name as ReasonDescription,
            E.Code as GroupCode,E.Name as GroupName,
            (select Count(*) from [SFC_AbnormalHour] where A.ReasonID = ReasonID and [Status] = '{0}0201213000001' and 
                        [CompletionOrderID] in 
                        (select [CompletionOrderID] from [SFC_CompletionOrder] where [FabMoProcessID] in 
                        (select [FabMoProcessID] from  [SFC_FabMoProcess] where [WorkCenterID]='{1}' and [Status] <> '{0}0201213000003' )   
                        and [Status] = '{0}020121300002A' )) as Frequency,
                        (select SUM(Hour) from [SFC_AbnormalHour] where A.ReasonID = ReasonID and [Status] = '{0}0201213000001' 
                        and  [CompletionOrderID] in  (select [CompletionOrderID] from [SFC_CompletionOrder] 
                        where [FabMoProcessID] in (select [FabMoProcessID] from  [SFC_FabMoProcess] 
                        where [WorkCenterID]='{1}' and [Status] <> '{0}0201213000003' )   
                        and [Status] = '{0}020121300002A' )) as Hour,
                        (select SUM(Hour) from [SFC_AbnormalHour] where [Status] = '{0}0201213000001' 
                        and [CompletionOrderID] in (select [CompletionOrderID] from [SFC_CompletionOrder] where [FabMoProcessID] in 
                        (select [FabMoProcessID] from  [SFC_FabMoProcess] where [WorkCenterID]='{1}' and [Status] <> '{0}0201213000003' )   
                        and [Status] = '{0}020121300002A' )) as AllHour ", Framework.SystemID, WorkCenterID);
            string sql = string.Format(
                @"from [SFC_AbnormalHour] A               
                left join [SYS_Parameters] D on A.[ReasonID] =D.[ParameterID]
                left join [SYS_Parameters] E on A.[GroupID] =E.[ParameterID]
                where A.[SystemID] = '{0}' and A.[Status] = '{0}0201213000001' and A.[Type]='{1}'", Framework.SystemID, Type);

            sql += string.Format(@"and 
                        [CompletionOrderID] in 
                        (select [CompletionOrderID] 
                        from [SFC_CompletionOrder] 
                        where [FabMoProcessID] in 
                        (select [FabMoProcessID] 
                        from  [SFC_FabMoProcess] 
                        where [WorkCenterID]='{1}' and [Status] <> '{0}0201213000003' )   
                        and [Status] = '{0}020121300002A' )  ", Framework.SystemID, WorkCenterID);

            count = UniversalService.getCount(sql, null);

            string orderby = "A.[Sequence] ";

            DataTable dt = UniversalService.getTable(select, sql, orderby, null, page, rows);

            return ToHashtableList(dt);
        }


        /// <summary>
        /// 判断指定类型的异常工时是否存在
        /// SAM 2017年7月26日21:35:36 
        /// 不存在true,存在false
        /// </summary>
        /// <param name="Type"></param>
        /// <param name="CompletionOrderID"></param>
        /// <returns></returns>
        public static bool Check(string Type, string CompletionOrderID)
        {
            string sql = string.Format(@"select * from [SFC_AbnormalHour] where [Type] = '{0}'  and [SystemID] = '{1}' and [Status] <> '{0}0201213000003' ", Type, Framework.SystemID);

            if (!string.IsNullOrWhiteSpace(CompletionOrderID))
                sql += string.Format(@" and [CompletionOrderID] ='{0}'", CompletionOrderID);

            DataTable dt = SQLHelper.ExecuteDataTable(sql, CommandType.Text);

            if (dt.Rows.Count == 0)
                return true;
            else
                return false;
        }


        /// <summary>
        /// 根据完工单和类型，获取对应无效工时的综合
        /// SAM 2017年8月23日17:09:22
        /// </summary>
        /// <param name="Type"></param>
        /// <param name="CompletionOrderID"></param>
        /// <returns></returns>
        public static int GetSum(string Type, string CompletionOrderID)
        {
            string sql = string.Format(@"select isnull(sum(Hour), 0) from [SFC_AbnormalHour] where [Type] = '{0}'  and [SystemID] = '{1}' and [Status] <> '{0}0201213000003' ", Type, Framework.SystemID);

            if (!string.IsNullOrWhiteSpace(CompletionOrderID))
                sql += string.Format(@" and [CompletionOrderID] ='{0}'", CompletionOrderID);

            DataTable dt = SQLHelper.ExecuteDataTable(sql, CommandType.Text);

            return int.Parse(dt.Rows[0][0].ToString());
        }
    }
}

