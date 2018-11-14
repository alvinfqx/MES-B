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
    public class EMS_CalledRepairReasonService : SuperModel<EMS_CalledRepairReason>
    {
        /// <summary>
        /// 新增
        /// SAM 2017年5月24日10:52:44
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="Model"></param>
        /// <returns></returns>
        public static bool insert(string userId, EMS_CalledRepairReason Model)
        {
            try
            {
                string sql = string.Format(@"insert[EMS_CalledRepairReason]([CalledRepairReasonID],[CalledRepairOrderID],[ReasonID],[ReasonDescription],[DealWithDescription],
                [Status],[Comments],[Modifier],[ModifiedTime],[ModifiedLocalTime],[Creator],[CreateTime],[CreateLocalTime],[SystemID]) values
                 (@CalledRepairReasonID,@CalledRepairOrderID,
                 @ReasonID,@ReasonDescription,@DealWithDescription,@Status,@Comments,'{0}','{1}','{2}','{0}','{1}','{2}','{3}')", userId, DateTime.Now, DateTime.Now, Framework.SystemID);

                SqlParameter[] parameters = new SqlParameter[]{
                    new SqlParameter("@CalledRepairReasonID",SqlDbType.VarChar),
                    new SqlParameter("@CalledRepairOrderID",SqlDbType.NVarChar),
                    new SqlParameter("@ReasonID",SqlDbType.NVarChar),
                    new SqlParameter("@ReasonDescription",SqlDbType.NVarChar),
                    new SqlParameter("@Status",SqlDbType.VarChar),
                    new SqlParameter("@Comments",SqlDbType.NVarChar),
                    new SqlParameter("@DealWithDescription",SqlDbType.NVarChar)
                    };

                parameters[0].Value = (Object)Model.CalledRepairReasonID ?? DBNull.Value;
                parameters[1].Value = (Object)Model.CalledRepairOrderID ?? DBNull.Value;
                parameters[2].Value = (Object)Model.ReasonID ?? DBNull.Value;
                parameters[3].Value = (Object)Model.ReasonDescription ?? DBNull.Value;
                parameters[4].Value = (Object)Model.Status ?? DBNull.Value;
                parameters[5].Value = (Object)Model.Comments ?? DBNull.Value;
                parameters[6].Value = (Object)Model.DealWithDescription ?? DBNull.Value;

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
        /// SAM 2017年5月24日10:52:51
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="Model"></param>
        /// <returns></returns>
        public static bool update(string userId, EMS_CalledRepairReason Model)
        {
            try
            {
                string sql = string.Format(@"update[EMS_CalledRepairReason] set {0},
                [ReasonID]=@ReasonID,[ReasonDescription]=@ReasonDescription,
                [DealWithDescription]=@DealWithDescription,[Status]=@Status,[Comments]=@Comments 
                where [CalledRepairReasonID]=@CalledRepairReasonID", UniversalService.getUpdateUTC(userId));

                SqlParameter[] parameters = new SqlParameter[]{
                    new SqlParameter("@CalledRepairReasonID",SqlDbType.VarChar),
                    new SqlParameter("@ReasonID",SqlDbType.NVarChar),
                    new SqlParameter("@ReasonDescription",SqlDbType.NVarChar),
                    new SqlParameter("@DealWithDescription",SqlDbType.NVarChar),
                    new SqlParameter("@Status",SqlDbType.VarChar),
                    new SqlParameter("@Comments",SqlDbType.NVarChar),
                    };

                parameters[0].Value = Model.CalledRepairReasonID;
                parameters[1].Value = (Object)Model.ReasonID ?? DBNull.Value;
                parameters[2].Value = (Object)Model.ReasonDescription ?? DBNull.Value;
                parameters[3].Value = (Object)Model.DealWithDescription ?? DBNull.Value;
                parameters[4].Value = (Object)Model.Status ?? DBNull.Value;
                parameters[5].Value = (Object)Model.Comments ?? DBNull.Value;

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
        /// SAM 2017年5月24日10:53:10
        /// </summary>
        /// <param name="CalledRepairReasonID"></param>
        /// <returns></returns>
        public static EMS_CalledRepairReason get(string CalledRepairReasonID)
        {
            string sql = string.Format(@"select Top 1 * from [EMS_CalledRepairReason] where [CalledRepairReasonID] = '{0}'  and [SystemID] = '{1}' ", CalledRepairReasonID, Framework.SystemID);

            DataTable dt = SQLHelper.ExecuteDataTable(sql, CommandType.Text);

            if (dt.Rows.Count == 0)
                return null;
            else
                return ToEntity(dt.Rows[0]);
        }

        /// <summary>
        /// 根据叫修单获取他的叫修原因
        /// SAM 2017年5月24日14:48:42
        /// </summary>
        /// <param name="CalledRepairOrderID"></param>
        /// <param name="page">页码</param>
        /// <param name="rows">行数</param>
        /// <param name="count"></param>
        /// <returns></returns>
        public static IList<Hashtable> Ems00004GetReasonList(string CalledRepairOrderID, int page, int rows, ref int count)
        {
            string select = string.Format(@"select A.CalledRepairReasonID,A.CalledRepairOrderID,A.ReasonID,A.ReasonDescription,
            A.Status,A.DealWithDescription,A.Comments,
            E.Code as ReasonCode,
            (Select Code from SYS_Parameters where ParameterID = E.DescriptionOne) as GroupCode,
            (Select Name from SYS_Parameters where ParameterID = E.DescriptionOne) as GroupName,
            B.UserName as Creator,A.CreateLocalTime as CreateTime,
            (CASE WHEN A.CreateLocalTime=A.ModifiedLocalTime THEN NULL else C.UserName END) as Modifier,
            (CASE WHEN A.CreateLocalTime=A.ModifiedLocalTime THEN NULL else A.ModifiedLocalTime END) as ModifiedTime ");

            string sql = string.Format(@"  from [EMS_CalledRepairReason] A 
            left join [SYS_MESUsers] B on A.[Creator] = B.[MESUserID]
            left join [SYS_MESUsers] C on A.[Modifier] = C.[MESUserID]
            left join [EMS_CalledRepairOrder] D on A.[CalledRepairOrderID] = D.[CalledRepairOrderID]
            left join [SYS_Parameters] E on A.[ReasonID] = E.[ParameterID]
            where A.[SystemID]='{0}' and A.[Status] <> '{0}0201213000003' and A.[CalledRepairOrderID]='{1}' ", Framework.SystemID, CalledRepairOrderID);

            count = UniversalService.getCount(sql, null);

            string orderby = " A.[Status],D.[Code] ";

            DataTable dt = UniversalService.getTable(select, sql, orderby, null, page, rows);

            return ToHashtableList(dt);
        }

        /// <summary>
        /// 判断指定的工单是否已存在原因
        /// SAM 2017年5月29日16:18:12
        /// </summary>
        /// <param name="ReasonID"></param>
        /// <param name="CalledRepairReasonID"></param>
        /// <returns></returns>
        public static bool CheckReason(string ReasonID, string CalledRepairOrderID, string CalledRepairReasonID)
        {
            string sql = string.Format(@"select Top 1 * from [EMS_CalledRepairReason] where [SystemID]='{0}' and [Status] <> '{0}0201213000003' ", Framework.SystemID);

            /*先定义Code，默认给DbNull,以免参数找不到*/
            SqlParameter[] parameters = new SqlParameter[]{
                new SqlParameter("@ReasonID",SqlDbType.VarChar),
            };
            parameters[0].Value = DBNull.Value;

            /*因为Code是通过手动输入的，所以需要用参数的形式去拼SQL*/
            if (!string.IsNullOrWhiteSpace(ReasonID))
            {
                sql = sql + string.Format(@" and [ReasonID] =@ReasonID ");
                parameters[0].Value = ReasonID;
            }

            /*CalledRepairReasonID（参数流水号）用于在更新时，排除他自己*/
            if (!string.IsNullOrWhiteSpace(CalledRepairReasonID))
                sql = sql + string.Format(@" and [CalledRepairReasonID] <> '{0}' ", CalledRepairReasonID);

            if (!string.IsNullOrWhiteSpace(CalledRepairOrderID))
                sql = sql + string.Format(@" and [CalledRepairOrderID] = '{0}' ", CalledRepairOrderID);

            DataTable dt = SQLHelper.ExecuteDataTable(sql, CommandType.Text, parameters);

            if (dt.Rows.Count == 0)
                return false;
            else
                return true;
        }

        /// <summary>
        /// 維修原因統計分析
        /// SAM 2017年8月3日14:01:51   
        /// </summary>
        /// <param name="StartReasonCode"></param>
        /// <param name="EndReasonCode"></param>
        /// <param name="StartDate"></param>
        /// <param name="EndDate"></param>
        /// <param name="StartEquipmentCode"></param>
        /// <param name="EndEquipmentCode"></param>
        /// <param name="Type"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public static IList<Hashtable> Ems00007GetReasonList(string StartReasonCode, string EndReasonCode, string StartDate, string EndDate, string StartEquipmentCode, string EndEquipmentCode, string Type, int page, int rows, ref int count)
        {
            string select = string.Format(@"select distinct ReasonID,B.Code,B.Name,null as [Count] ", Framework.SystemID);

            string sql = string.Format(@" from [EMS_CalledRepairReason]  A
                left Join [SYS_Parameters] B on A.[ReasonID] =B.[ParameterID]
                where [Status]='{0}0201213000001' and A.[SystemID]='{0}' and A.[ReasonID] is not null ", Framework.SystemID);

            SqlParameter[] parameters = new SqlParameter[]{
                new SqlParameter("@StartReasonCode",SqlDbType.VarChar),
                new SqlParameter("@EndReasonCode",SqlDbType.VarChar),
                new SqlParameter("@StartDate",SqlDbType.VarChar),
                new SqlParameter("@EndDate",SqlDbType.VarChar),
                new SqlParameter("@StartEquipmentCode",SqlDbType.VarChar),
                new SqlParameter("@EndEquipmentCode",SqlDbType.VarChar),
                new SqlParameter("@Type",SqlDbType.VarChar)
            };
            parameters[0].Value = DBNull.Value;
            parameters[1].Value = DBNull.Value;
            parameters[2].Value = DBNull.Value;
            parameters[3].Value = DBNull.Value;
            parameters[4].Value = DBNull.Value;
            parameters[5].Value = DBNull.Value;
            parameters[6].Value = DBNull.Value;

            SqlParameter[] Parcount = new SqlParameter[]{
                new SqlParameter("@StartReasonCode",SqlDbType.VarChar),
                new SqlParameter("@EndReasonCode",SqlDbType.VarChar),
                new SqlParameter("@StartDate",SqlDbType.VarChar),
                new SqlParameter("@EndDate",SqlDbType.VarChar),
                new SqlParameter("@StartEquipmentCode",SqlDbType.VarChar),
                new SqlParameter("@EndEquipmentCode",SqlDbType.VarChar),
                new SqlParameter("@Type",SqlDbType.VarChar)
            };
            Parcount[0].Value = DBNull.Value;
            Parcount[1].Value = DBNull.Value;
            Parcount[2].Value = DBNull.Value;
            Parcount[3].Value = DBNull.Value;
            Parcount[4].Value = DBNull.Value;
            Parcount[5].Value = DBNull.Value;
            Parcount[6].Value = DBNull.Value;

            if (!string.IsNullOrWhiteSpace(StartReasonCode))
            {
                sql = sql + string.Format(@" and B.[Code] >= @StartReasonCode ");
                parameters[0].Value = StartReasonCode;
                Parcount[0].Value = StartReasonCode;
            }

            if (!string.IsNullOrWhiteSpace(EndReasonCode))
            {
                sql = sql + string.Format(@" and B.[Code] <= @EndReasonCode ");
                parameters[1].Value = EndReasonCode;
                Parcount[1].Value = EndReasonCode;
            }

            sql += string.Format(@"and A.[CalledRepairOrderID] in (
                select [CalledRepairOrderID] from [EMS_CalledRepairOrder] C
                left Join[EMS_Equipment] D on C.[EquipmentID] = D.[EquipmentID]
                where C.[Status] <> '{0}0201213000003' and C.[SystemID] = '{0}' ", Framework.SystemID);


            if (!string.IsNullOrWhiteSpace(StartDate))
            {
                sql = sql + string.Format(@" and C.[Date] >= @StartDate ");
                parameters[2].Value = StartDate;
                Parcount[2].Value = StartDate;
            }

            if (!string.IsNullOrWhiteSpace(EndDate))
            {
                sql = sql + string.Format(@" and C.[Date] <= @EndDate ");
                parameters[3].Value = EndDate;
                Parcount[3].Value = EndDate;
            }

            if (!string.IsNullOrWhiteSpace(StartEquipmentCode))
            {
                sql = sql + string.Format(@" and D.[Code] >= @StartEquipmentCode ");
                parameters[4].Value = StartEquipmentCode;
                Parcount[4].Value = StartEquipmentCode;
            }


            if (!string.IsNullOrWhiteSpace(EndEquipmentCode))
            {
                sql = sql + string.Format(@" and D.[Code] <= @EndEquipmentCode ");
                parameters[5].Value = EndEquipmentCode;
                Parcount[5].Value = EndEquipmentCode;
            }

            if (!string.IsNullOrWhiteSpace(Type))
            {
                sql = sql + string.Format(@" and D.[ResourceCategory]= (select Top 1 ParameterID from SYS_Parameters where Code collate Chinese_PRC_CI_AS = @Type and ParameterTypeID='{0}0191213000013' and  IsEnable=1) ", Framework.SystemID);
                parameters[6].Value = Type;
                Parcount[6].Value = Type;
            }

            sql += ")";

            DataTable CountList = SQLHelper.ExecuteDataTable(select + sql, CommandType.Text, parameters);

            count = CountList.Rows.Count;

            string orderby = " [Code] ";

            DataTable dt = UniversalService.GetTableDistinct(select, sql, orderby, parameters, page, rows);

            return ToHashtableList(dt);
        }

        /// <summary>
        /// 获取指定原因码在指定范围内的出现次数
        /// SAM 2017年10月10日16:34:33
        /// </summary>
        /// <param name="ReasonID"></param>
        /// <param name="StartReasonCode"></param>
        /// <param name="EndReasonCode"></param>
        /// <param name="StartDate"></param>
        /// <param name="EndDate"></param>
        /// <param name="StartEquipmentCode"></param>
        /// <param name="EndEquipmentCode"></param>
        /// <param name="Type"></param>
        /// <returns></returns>
        public static int Ems00007GetReasonCount(string ReasonID, string StartReasonCode, string EndReasonCode,
          string StartDate, string EndDate, string StartEquipmentCode, string EndEquipmentCode, string Type)
        {
            string sql = string.Format(@"select * from [EMS_CalledRepairReason]  A
                    left Join [SYS_Parameters] B on A.[ReasonID] =B.[ParameterID]
                    where [Status]='{0}0201213000001' and A.[SystemID]='{0}' and A.[ReasonID] ='{1}' ", Framework.SystemID, ReasonID);

            SqlParameter[] parameters = new SqlParameter[]{
                    new SqlParameter("@StartReasonCode",SqlDbType.VarChar),
                    new SqlParameter("@EndReasonCode",SqlDbType.VarChar),
                    new SqlParameter("@StartDate",SqlDbType.VarChar),
                    new SqlParameter("@EndDate",SqlDbType.VarChar),
                    new SqlParameter("@StartEquipmentCode",SqlDbType.VarChar),
                    new SqlParameter("@EndEquipmentCode",SqlDbType.VarChar),
                    new SqlParameter("@Type",SqlDbType.VarChar)
                };
            parameters[0].Value = DBNull.Value;
            parameters[1].Value = DBNull.Value;
            parameters[2].Value = DBNull.Value;
            parameters[3].Value = DBNull.Value;
            parameters[4].Value = DBNull.Value;
            parameters[5].Value = DBNull.Value;
            parameters[6].Value = DBNull.Value;

            SqlParameter[] Parcount = new SqlParameter[]{
                    new SqlParameter("@StartReasonCode",SqlDbType.VarChar),
                    new SqlParameter("@EndReasonCode",SqlDbType.VarChar),
                    new SqlParameter("@StartDate",SqlDbType.VarChar),
                    new SqlParameter("@EndDate",SqlDbType.VarChar),
                    new SqlParameter("@StartEquipmentCode",SqlDbType.VarChar),
                    new SqlParameter("@EndEquipmentCode",SqlDbType.VarChar),
                    new SqlParameter("@Type",SqlDbType.VarChar)
                };
            Parcount[0].Value = DBNull.Value;
            Parcount[1].Value = DBNull.Value;
            Parcount[2].Value = DBNull.Value;
            Parcount[3].Value = DBNull.Value;
            Parcount[4].Value = DBNull.Value;
            Parcount[5].Value = DBNull.Value;
            Parcount[6].Value = DBNull.Value;

            if (!string.IsNullOrWhiteSpace(StartReasonCode))
            {
                sql = sql + string.Format(@" and B.[Code] >= @StartReasonCode ");
                parameters[0].Value = StartReasonCode;
                Parcount[0].Value = StartReasonCode;
            }

            if (!string.IsNullOrWhiteSpace(EndReasonCode))
            {
                sql = sql + string.Format(@" and B.[Code] <= @EndReasonCode ");
                parameters[1].Value = EndReasonCode;
                Parcount[1].Value = EndReasonCode;
            }

            sql += string.Format(@"and A.[CalledRepairOrderID] in (
                    select [CalledRepairOrderID] from [EMS_CalledRepairOrder] C
                    left Join[EMS_Equipment] D on C.[EquipmentID] = D.[EquipmentID]
                    where C.[Status] <> '{0}0201213000003' and C.[SystemID] = '{0}' ", Framework.SystemID);


            if (!string.IsNullOrWhiteSpace(StartDate))
            {
                sql = sql + string.Format(@" and C.[Date] >= @StartDate ");
                parameters[2].Value = StartDate;
                Parcount[2].Value = StartDate;
            }

            if (!string.IsNullOrWhiteSpace(EndDate))
            {
                sql = sql + string.Format(@" and C.[Date] <= @EndDate ");
                parameters[3].Value = EndDate;
                Parcount[3].Value = EndDate;
            }

            if (!string.IsNullOrWhiteSpace(StartEquipmentCode))
            {
                sql = sql + string.Format(@" and D.[Code] >= @StartEquipmentCode ");
                parameters[4].Value = StartEquipmentCode;
                Parcount[4].Value = StartEquipmentCode;
            }

            if (!string.IsNullOrWhiteSpace(EndEquipmentCode))
            {
                sql = sql + string.Format(@" and D.[Code] <= @EndEquipmentCode ");
                parameters[5].Value = EndEquipmentCode;
                Parcount[5].Value = EndEquipmentCode;
            }

            if (!string.IsNullOrWhiteSpace(Type))
            {
                sql = sql + string.Format(@" and D.[ResourceCategory]= (select Top 1 ParameterID from SYS_Parameters where Code collate Chinese_PRC_CI_AS = @Type and ParameterTypeID='{0}0191213000013' and  IsEnable=1) ", Framework.SystemID);
                parameters[6].Value = Type;
                Parcount[6].Value = Type;
            }

            sql += ")";

            //count = UniversalService.getCount(sql, Parcount);

            DataTable CountList = SQLHelper.ExecuteDataTable(sql, CommandType.Text, parameters);

            if (CountList == null)
                return 0;
            else
                return CountList.Rows.Count;
        }

        /// <summary>
        /// 維修原因統計分析-原因码列表(不分页，用于园饼图)
        /// SAM 2017年8月3日17:48:10
        /// </summary>
        /// <param name="StartReasonCode"></param>
        /// <param name="EndReasonCode"></param>
        /// <param name="StartDate"></param>
        /// <param name="EndDate"></param>
        /// <param name="StartEquipmentCode"></param>
        /// <param name="EndEquipmentCode"></param>
        /// <param name="Type"></param>
        /// <returns></returns>
        public static IList<Hashtable> Ems00007GetReason(string StartReasonCode, string EndReasonCode, string StartDate, string EndDate, string StartEquipmentCode, string EndEquipmentCode, string Type)
        {
            string select = string.Format(@"select distinct ReasonID,B.Code,B.Name,null as [Count] ", Framework.SystemID);

            string sql = string.Format(@" from [EMS_CalledRepairReason]  A
                left Join [SYS_Parameters] B on A.[ReasonID] =B.[ParameterID]
                where [Status]='{0}0201213000001' and A.[SystemID]='{0}' and A.[ReasonID] is not null ", Framework.SystemID);

            SqlParameter[] parameters = new SqlParameter[]{
                new SqlParameter("@StartReasonCode",SqlDbType.VarChar),
                new SqlParameter("@EndReasonCode",SqlDbType.VarChar),
                new SqlParameter("@StartDate",SqlDbType.VarChar),
                new SqlParameter("@EndDate",SqlDbType.VarChar),
                new SqlParameter("@StartEquipmentCode",SqlDbType.VarChar),
                new SqlParameter("@EndEquipmentCode",SqlDbType.VarChar),
                new SqlParameter("@Type",SqlDbType.VarChar)
            };
            parameters[0].Value = DBNull.Value;
            parameters[1].Value = DBNull.Value;
            parameters[2].Value = DBNull.Value;
            parameters[3].Value = DBNull.Value;
            parameters[4].Value = DBNull.Value;
            parameters[5].Value = DBNull.Value;
            parameters[6].Value = DBNull.Value;

            if (!string.IsNullOrWhiteSpace(StartReasonCode))
            {
                sql = sql + string.Format(@" and B.[Code] >= @StartReasonCode ");
                parameters[0].Value = StartReasonCode;
            }

            if (!string.IsNullOrWhiteSpace(EndReasonCode))
            {
                sql = sql + string.Format(@" and B.[Code] <= @EndReasonCode ");
                parameters[1].Value = EndReasonCode;
            }

            sql += string.Format(@"and A.[CalledRepairOrderID] in (
                select [CalledRepairOrderID] from [EMS_CalledRepairOrder] C
                left Join[EMS_Equipment] D on C.[EquipmentID] = D.[EquipmentID]
                where C.[Status] <> '{0}0201213000003' and C.[SystemID] = '{0}' ", Framework.SystemID);


            if (!string.IsNullOrWhiteSpace(StartDate))
            {
                sql = sql + string.Format(@" and C.[Date] >= @StartDate ");
                parameters[2].Value = StartDate;
            }

            if (!string.IsNullOrWhiteSpace(EndDate))
            {
                sql = sql + string.Format(@" and C.[Date] <= @EndDate ");
                parameters[3].Value = EndDate;
            }

            if (!string.IsNullOrWhiteSpace(StartEquipmentCode))
            {
                sql = sql + string.Format(@" and D.[Code] >= @StartEquipmentCode ");
                parameters[4].Value = StartEquipmentCode;
            }


            if (!string.IsNullOrWhiteSpace(EndEquipmentCode))
            {
                sql = sql + string.Format(@" and D.[Code] <= @EndEquipmentCode ");
                parameters[5].Value = EndEquipmentCode;
            }

            if (!string.IsNullOrWhiteSpace(Type))
            {
                sql = sql + string.Format(@" and D.[ResourceCategory]= (select Top 1 ParameterID from SYS_Parameters where Code collate Chinese_PRC_CI_AS = @Type and ParameterTypeID='{0}0191213000013' and  IsEnable=1) ", Framework.SystemID);
                parameters[6].Value = Type;
            }

            sql += ")";

            string orderby = "order by B.[Code] ";

            DataTable dt = SQLHelper.ExecuteDataTable(select + sql + orderby, CommandType.Text, parameters);

            return ToHashtableList(dt);
        }

        /// <summary>
        /// 維修原因統計分析-设备列表
        /// SAM  2017年8月3日18:08:18
        /// </summary>
        /// <param name="StartReasonCode"></param>
        /// <param name="EndReasonCode"></param>
        /// <param name="StartDate"></param>
        /// <param name="EndDate"></param>
        /// <param name="StartEquipmentCode"></param>
        /// <param name="EndEquipmentCode"></param>
        /// <param name="Type"></param>
        /// <returns></returns>
        public static IList<Hashtable> Ems00007GetEquipmentList(string StartDate, string EndDate, string StartEquipmentCode, string EndEquipmentCode, string Type)
        {
            string select = string.Format(@"select distinct E.[EquipmentID],
            (select [Code] from [EMS_Equipment] where E.[EquipmentID] =[EquipmentID]) as EquipmentCode,
            (select [Name] from [EMS_Equipment] where E.[EquipmentID] =[EquipmentID]) as EquipmentName,
            ReasonID,B.Code,B.Name,null as [Count] ", Framework.SystemID);

            string sql = string.Format(@" from [EMS_CalledRepairReason]  A
                left Join [SYS_Parameters] B on A.[ReasonID] =B.[ParameterID]
                left Join [EMS_CalledRepairOrder] E on A.[CalledRepairOrderID] =E.[CalledRepairOrderID]
                where A.[Status]='{0}0201213000001' and A.[SystemID]='{0}' and A.[ReasonID] is not null ", Framework.SystemID);

            SqlParameter[] parameters = new SqlParameter[]{
                new SqlParameter("@StartDate",SqlDbType.VarChar),
                new SqlParameter("@EndDate",SqlDbType.VarChar),
                new SqlParameter("@StartEquipmentCode",SqlDbType.VarChar),
                new SqlParameter("@EndEquipmentCode",SqlDbType.VarChar),
                new SqlParameter("@Type",SqlDbType.VarChar)
            };
            parameters[0].Value = DBNull.Value;
            parameters[1].Value = DBNull.Value;
            parameters[2].Value = DBNull.Value;
            parameters[3].Value = DBNull.Value;
            parameters[4].Value = DBNull.Value;

            sql += string.Format(@"and A.[CalledRepairOrderID] in (
                select [CalledRepairOrderID] from [EMS_CalledRepairOrder] C
                left Join [EMS_Equipment] D on C.[EquipmentID] = D.[EquipmentID]
                where C.[Status] <> '{0}0201213000003' and C.[SystemID] = '{0}' ", Framework.SystemID);


            if (!string.IsNullOrWhiteSpace(StartDate))
            {
                sql = sql + string.Format(@" and C.[Date] >= @StartDate ");
                parameters[0].Value = StartDate;
            }

            if (!string.IsNullOrWhiteSpace(EndDate))
            {
                sql = sql + string.Format(@" and C.[Date] <= @EndDate ");
                parameters[1].Value = EndDate;
            }

            if (!string.IsNullOrWhiteSpace(StartEquipmentCode))
            {
                sql = sql + string.Format(@" and D.[Code] >= @StartEquipmentCode ");
                parameters[2].Value = StartEquipmentCode;
            }

            if (!string.IsNullOrWhiteSpace(EndEquipmentCode))
            {
                sql = sql + string.Format(@" and D.[Code] <= @EndEquipmentCode ");
                parameters[3].Value = EndEquipmentCode;
            }

            if (!string.IsNullOrWhiteSpace(Type))
            {
                sql = sql + string.Format(@" and D.[ResourceCategory]= (select Top 1 ParameterID from SYS_Parameters where Code collate Chinese_PRC_CI_AS = @Type and ParameterTypeID='{0}0191213000013' and  IsEnable=1) ", Framework.SystemID);
                parameters[4].Value = Type;
            }

            sql += ")";

            string orderby = "order by B.[Code] ";

            DataTable dt = SQLHelper.ExecuteDataTable(select + sql + orderby, CommandType.Text, parameters);

            return ToHashtableList(dt);
        }

        /// <summary>
        /// 统计指定原因码在一定条件范围内的叫修单次数
        /// SAM 2017年10月11日10:34:18
        /// </summary>
        /// <param name="ReasonID"></param>
        /// <param name="StartDate"></param>
        /// <param name="EndDate"></param>
        /// <param name="EquipmentID"></param>
        /// <param name="Type"></param>
        /// <returns></returns>
        public static int Ems00007GetEquipmentCount(string ReasonID, string EquipmentID,string StartDate, string EndDate, string Type)
        {
            string select = string.Format(@"select distinct A.[CalledRepairOrderID] ", Framework.SystemID);

            string sql = string.Format(@" from [EMS_CalledRepairReason]  A
                left Join [SYS_Parameters] B on A.[ReasonID] =B.[ParameterID]
                left Join [EMS_CalledRepairOrder] E on A.[CalledRepairOrderID] =E.[CalledRepairOrderID] 
                where A.[Status]='{0}0201213000001' and A.[SystemID]='{0}' and A.[ReasonID] ='{1}' ", Framework.SystemID, ReasonID);

            SqlParameter[] parameters = new SqlParameter[]{
                new SqlParameter("@StartDate",SqlDbType.VarChar),
                new SqlParameter("@EndDate",SqlDbType.VarChar),
                new SqlParameter("@Type",SqlDbType.VarChar)
            };
            parameters[0].Value = DBNull.Value;
            parameters[1].Value = DBNull.Value;
            parameters[2].Value = DBNull.Value;

            sql += string.Format(@"and A.[CalledRepairOrderID] in (
                select [CalledRepairOrderID] from [EMS_CalledRepairOrder] C
                left Join [EMS_Equipment] D on C.[EquipmentID] = D.[EquipmentID]
                where C.[Status] <> '{0}0201213000003' and C.[SystemID] = '{0}' and  C.[EquipmentID] ='{1}' ", Framework.SystemID, EquipmentID);


            if (!string.IsNullOrWhiteSpace(StartDate))
            {
                sql = sql + string.Format(@" and C.[Date] >= @StartDate ");
                parameters[0].Value = StartDate;
            }

            if (!string.IsNullOrWhiteSpace(EndDate))
            {
                sql = sql + string.Format(@" and C.[Date] <= @EndDate ");
                parameters[1].Value = EndDate;
            }

            if (!string.IsNullOrWhiteSpace(Type))
            {
                sql = sql + string.Format(@" and D.[ResourceCategory]= (select Top 1 ParameterID from SYS_Parameters where Code collate Chinese_PRC_CI_AS = @Type and ParameterTypeID='{0}0191213000013' and  IsEnable=1) ", Framework.SystemID);
                parameters[2].Value = Type;
            }

            sql += ")";

            DataTable dt = SQLHelper.ExecuteDataTable(select + sql, CommandType.Text, parameters);

            if (dt == null)
                return 0;

            return dt.Rows.Count;
        }

    }
}

