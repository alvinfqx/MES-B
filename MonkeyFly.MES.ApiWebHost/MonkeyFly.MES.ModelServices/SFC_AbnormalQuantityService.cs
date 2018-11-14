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
    public class SFC_AbnormalQuantityService : SuperModel<SFC_AbnormalQuantity>
    {
        /// <summary>
        /// 新增
        /// SAM 2017年10月11日09:33:49
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="Model"></param>
        /// <returns></returns>
        public static bool insert(string userId, SFC_AbnormalQuantity Model)
        {
            try
            {
                string sql = string.Format(@"insert[SFC_AbnormalQuantity]([AbnormalQuantityID],[CompletionOrderID],
                [Sequence],[Type],[ReasonID],
                [Quantity],[Status],[Comments],
                [Modifier],[ModifiedTime],[ModifiedLocalTime],
                [Creator],[CreateTime],[CreateLocalTime],[SystemID]) values
                 (@AbnormalQuantityID,@CompletionOrderID,
                @Sequence,@Type,@ReasonID,
                @Quantity,@Status,@Comments,'{0}','{1}','{2}','{0}','{1}','{2}','{3}')", userId, DateTime.Now, DateTime.Now, Framework.SystemID);

                SqlParameter[] parameters = new SqlParameter[]{
                    new SqlParameter("@AbnormalQuantityID",SqlDbType.VarChar),
                    new SqlParameter("@CompletionOrderID",SqlDbType.VarChar),
                    new SqlParameter("@Sequence",SqlDbType.Int),
                    new SqlParameter("@Type",SqlDbType.VarChar),
                    new SqlParameter("@ReasonID",SqlDbType.VarChar),
                    new SqlParameter("@Quantity",SqlDbType.Decimal),
                    new SqlParameter("@Status",SqlDbType.VarChar),
                    new SqlParameter("@Comments",SqlDbType.NVarChar),
                    };

                parameters[0].Value = (Object)Model.AbnormalQuantityID ?? DBNull.Value;
                parameters[1].Value = (Object)Model.CompletionOrderID ?? DBNull.Value;
                parameters[2].Value = (Object)Model.Sequence ?? DBNull.Value;
                parameters[3].Value = (Object)Model.Type ?? DBNull.Value;
                parameters[4].Value = (Object)Model.ReasonID ?? DBNull.Value;
                parameters[5].Value = (Object)Model.Quantity ?? DBNull.Value;
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

        public static bool update(string userId, SFC_AbnormalQuantity Model)
        {
            try
            {
                string sql = String.Format(@"update[SFC_AbnormalQuantity] set {0},
[Sequence]=@Sequence,[Type]=@Type,
[ReasonID]=@ReasonID,[Quantity]=@Quantity,[Status]=@Status,[Comments]=@Comments where [AbnormalQuantityID]=@AbnormalQuantityID", UniversalService.getUpdateUTC(userId));
                SqlParameter[] parameters = new SqlParameter[]{
                    new SqlParameter("@AbnormalQuantityID",SqlDbType.VarChar),
                    new SqlParameter("@Sequence",SqlDbType.Int),
                    new SqlParameter("@Type",SqlDbType.VarChar),
                    new SqlParameter("@ReasonID",SqlDbType.VarChar),
                    new SqlParameter("@Quantity",SqlDbType.Decimal),
                    new SqlParameter("@Status",SqlDbType.VarChar),
                    new SqlParameter("@Comments",SqlDbType.NVarChar),
                    };

                parameters[0].Value = Model.AbnormalQuantityID;
                parameters[1].Value = (Object)Model.Sequence ?? DBNull.Value;
                parameters[2].Value = (Object)Model.Type ?? DBNull.Value;
                parameters[3].Value = (Object)Model.ReasonID ?? DBNull.Value;
                parameters[4].Value = (Object)Model.Quantity ?? DBNull.Value;
                parameters[5].Value = (Object)Model.Status ?? DBNull.Value;
                parameters[6].Value = (Object)Model.Comments ?? DBNull.Value;

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
        /// SAM 2017年8月22日11:14:40
        /// </summary>
        /// <param name="AbnormalQuantityID"></param>
        /// <returns></returns>
        public static SFC_AbnormalQuantity get(string AbnormalQuantityID)
        {
            string sql = string.Format(@"select Top 1 * from [SFC_AbnormalQuantity] where [AbnormalQuantityID] = '{0}'  and [SystemID] = '{1}' ", AbnormalQuantityID, Framework.SystemID);

            DataTable dt = SQLHelper.ExecuteDataTable(sql, CommandType.Text);

            if (dt.Rows.Count == 0)
                return null;
            else
                return ToEntity(dt.Rows[0]);
        }

        /// <summary>
        /// 完工單回報作業-異常數量列表
        /// SAM 2017年7月20日10:00:53
        /// </summary>
        /// <param name="CompletionOrderID"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public static IList<Hashtable> Sfc00007GetUnusualQtyList(string CompletionOrderID, int page, int rows, ref int count)
        {
            string select = string.Format(
             @"select A.AbnormalQuantityID,A.CompletionOrderID,A.Sequence,A.Type,A.ReasonID,A.Quantity,A.Status,
            D.Code as ReasonCode,D.Name as ReasonDescription,
            B.Emplno+'-'+B.UserName as Creator,A.CreateLocalTime as CreateTime,
            (CASE WHEN A.CreateLocalTime=A.ModifiedLocalTime THEN NULL else C.Emplno+'-'+C.UserName END) as Modifier,
            (CASE WHEN A.CreateLocalTime=A.ModifiedLocalTime THEN NULL else A.ModifiedLocalTime END) as ModifiedTime ", Framework.SystemID);
            string sql = string.Format(
                @"from [SFC_AbnormalQuantity] A               
                left join [SYS_MESUsers] B on A.[Creator] = B.[MESUserID]
                left join [SYS_MESUsers] C on A.[Modifier] =C.[MESUserID]
                left join [SYS_Parameters] D on A.[ReasonID] =D.[ParameterID]
                where A.[SystemID] = '{0}' and A.[Status] = '{0}0201213000001' and A.[CompletionOrderID]='{1}'", Framework.SystemID, CompletionOrderID);

            count = UniversalService.getCount(sql, null);

            string orderby = "A.[Sequence] ";

            DataTable dt = UniversalService.getTable(select, sql, orderby, null, page, rows);

            return ToHashtableList(dt);
        }

        /// <summary>
        /// 根据料品流水号和制程流水号以及日期范围内的获取对应的原因码列表(去重后的)
        /// SAM 2017年7月22日23:24:15
        /// </summary>
        /// <param name="ItemID"></param>
        /// <param name="ProcessID"></param>
        /// <returns></returns>
        public static IList<Hashtable> Sfc00013GetReason(string ItemID, string ProcessID, string StartDate, string EndDate)
        {
            string sql = string.Format(
              @"select distinct A.ReasonID,B.Code as ReasonCode from [SFC_AbnormalQuantity] A 
                left join [SYS_Parameters] B on A.ReasonID = B.ParameterID
                where A.[SystemID] = '{0}' and A.[Status] = '{0}0201213000001' 
            and [CompletionOrderID] in (select [CompletionOrderID] from [SFC_CompletionOrder] where [ItemID] ='{1}' and [ProcessID]='{2}' ", Framework.SystemID, ItemID, ProcessID);

            if (!string.IsNullOrWhiteSpace(StartDate))
                sql += string.Format(@" and [Date] >= '{0}' ", StartDate);

            if (!string.IsNullOrWhiteSpace(EndDate))
                sql += string.Format(@" and [Date] <= '{0}' ", EndDate);

            sql += ") order by B.Code";

            DataTable dt = SQLHelper.ExecuteDataTable(sql, CommandType.Text);

            if (dt.Rows.Count == 0)
                return null;

            return ToHashtableList(dt);
        }

        /// <summary>
        /// 根据原因码和异常类型返回数量
        /// SAM 2017年7月22日23:33:34
        /// </summary>
        /// <param name="ReasonID"></param>
        /// <param name="Type"></param>
        /// <returns></returns>
        public static decimal Sfc00013GetAbnormalQuantity(string ReasonID, string Type, string ItemID, string ProcessID, string StartDate, string EndDate)
        {
            string sql = string.Format(
              @"select ISNULL(SUM(Quantity),0) from [SFC_AbnormalQuantity] A 
                left join [SYS_Parameters] B on A.ReasonID = B.ParameterID
                where A.[SystemID] = '{0}' and A.[Status] = '{0}0201213000001' and [ReasonID]= '{1}' and [Type]='{2}'", Framework.SystemID, ReasonID, Type);


            sql += String.Format(@"and A.[CompletionOrderID] in (select [CompletionOrderID] from [SFC_CompletionOrder] where [ItemID] ='{0}' and [ProcessID]='{1}' ", ItemID, ProcessID);

            if (!string.IsNullOrWhiteSpace(StartDate))
                sql += String.Format(@"and  [Date] >='{0}' ", StartDate);

            if (!string.IsNullOrWhiteSpace(EndDate))
                sql += String.Format(@"and  [Date] <='{0}' ", EndDate);

            sql += ")";
           DataTable dt = SQLHelper.ExecuteDataTable(sql, CommandType.Text);

            if (dt.Rows.Count == 0)
                return 0;

            return decimal.Parse(dt.Rows[0][0].ToString());
        }


        /// <summary>
        /// 判断指定异常类型是否存在数据
        /// SAM 2017年7月26日21:31:09
        /// 不存在true，存在false
        /// </summary>
        /// <param name="Type"></param>
        /// <param name="CompletionOrderID"></param>
        /// <returns></returns>
        public static bool Check(string Type, string CompletionOrderID)
        {
            string sql = string.Format(@"select * from [SFC_AbnormalQuantity] where [Type] = '{0}'  and [SystemID] = '{1}' and [Status] <> '{0}0201213000003' ", Type, Framework.SystemID);

            if (!string.IsNullOrWhiteSpace(CompletionOrderID))
                sql += string.Format(@" and [CompletionOrderID] ='{0}'", CompletionOrderID);

            DataTable dt = SQLHelper.ExecuteDataTable(sql, CommandType.Text);

            if (dt.Rows.Count == 0)
                return true;
            else
                return false;
        }

        /// <summary>
        /// 根据完工单和类型，获取对应异常数量的综合
        /// SAM 2017年8月22日11:17:35
        /// </summary>
        /// <param name="Type"></param>
        /// <param name="CompletionOrderID"></param>
        /// <returns></returns>
        public static decimal GetSum(string Type, string CompletionOrderID)
        {
            string sql = string.Format(@"select isnull(sum(Quantity), 0) from [SFC_AbnormalQuantity] where [Type] = '{0}'  and [SystemID] = '{1}' and [Status] <> '{0}0201213000003' ", Type, Framework.SystemID);

            if (!string.IsNullOrWhiteSpace(CompletionOrderID))
                sql += string.Format(@" and [CompletionOrderID] ='{0}'", CompletionOrderID);

            DataTable dt = SQLHelper.ExecuteDataTable(sql, CommandType.Text);

            return decimal.Parse(dt.Rows[0][0].ToString());
        }


        /// <summary>
        /// 制品不良统计分析-原因统计页签-明细列表
        /// SAM 2017年10月11日09:45:16
        /// </summary>
        /// <param name="ItemID"></param>
        /// <returns></returns>
        public static IList<Hashtable> Sfc00022GetReasonDetailList(string ItemID)
        {
            string sql = string.Format(
              @"select distinct A.ReasonID,B.Code as ReasonCode,
                (select SUM(Quantity) from SFC_AbnormalQuantity where ReasonID=A.ReasonID and [Status] = '{0}0201213000001' and [Type] <> '{0}020121300009C' and 
                [CompletionOrderID] in (select [CompletionOrderID] from [SFC_CompletionOrder] where [ItemID] ='{1}')) as Num
                from [SFC_AbnormalQuantity] A 
                left join [SYS_Parameters] B on A.ReasonID = B.ParameterID
                where A.[SystemID] = '{0}' and A.[Status] = '{0}0201213000001'  and [Type] <> '{0}020121300009C'
                and [CompletionOrderID] in (select [CompletionOrderID] from [SFC_CompletionOrder] where [ItemID] ='{1}') order by Num desc", Framework.SystemID, ItemID);

            DataTable dt = SQLHelper.ExecuteDataTable(sql, CommandType.Text);

            if (dt.Rows.Count == 0)
                return null;

            return ToHashtableList(dt);
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
        public static IList<Hashtable> Sfc00022GetItemDetailList(string ItemID, string StartDate, string EndDate, int Interval)
        {
          string  select = string.Format(@"select distinct convert(varchar({1}),B.Date,120) as Date,
                (select ISNULL(SUM(Quantity),0) from [SFC_AbnormalQuantity] 
                where [Status] = '{0}0201213000001' and [Type] <> '{0}020121300009C' and 
                [CompletionOrderID] in (select [CompletionOrderID] from [SFC_CompletionOrder] 
                where [ItemID] ='{1}' and [Status] <> '{0}0201213000003' and convert(varchar({1}),B.Date,120)=convert(varchar({1}),Date,120)) ", Framework.SystemID,Interval);

            if (!string.IsNullOrWhiteSpace(StartDate))
                select += string.Format(@" and [Date] >='{0}'", StartDate);

            if (!string.IsNullOrWhiteSpace(EndDate))
                select += string.Format(@" and [Date] <='{0}'", EndDate);

            select+= string.Format(@") as Num");

            string sql = string.Format(
              @" from [SFC_AbnormalQuantity] A 
                left join [SFC_CompletionOrder] B on A.CompletionOrderID = B.CompletionOrderID
                where A.[SystemID] = '{0}' and A.[Status] = '{0}0201213000001'  and A.[Type] <> '{0}020121300009C'
                and B.[ItemID] ='{1}' ", Framework.SystemID, ItemID);


            SqlParameter[] parameters = new SqlParameter[] {
                new SqlParameter(@"StartDate",SqlDbType.VarChar),
                new SqlParameter(@"EndDate",SqlDbType.VarChar),
            };
            parameters[0].Value = DBNull.Value;
            parameters[1].Value = DBNull.Value;

            if (!string.IsNullOrWhiteSpace(StartDate))
            {
                sql = sql + string.Format(@" and B.[Date] >= @StartDate");
                parameters[0].Value = StartDate;
            }
            if (!string.IsNullOrWhiteSpace(EndDate))
            {
                sql = sql + string.Format(@" and B.[Date] <=@EndDate");
                parameters[1].Value = EndDate;
            }

            sql += String.Format(@" order by Num desc");

            DataTable dt = SQLHelper.ExecuteDataTable(select+sql, CommandType.Text, parameters);

            if (dt.Rows.Count == 0)
                return null;

            return ToHashtableList(dt);
        }
    }
}

