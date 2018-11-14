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
    public class EMS_ServiceReasonLogService : SuperModel<EMS_ServiceReasonLog>
    {
        /// <summary>
        /// 新增
        /// SAM 2017年6月2日11:10:02
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="Model"></param>
        /// <returns></returns>
        public static bool insert(string userId, EMS_ServiceReasonLog Model)
        {
            try
            {
                string sql = string.Format(@"insert[EMS_ServiceReasonLog]([ServiceReasonLogID],[CalledRepairOrderID],[ReasonID],[ReasonDescription],[Status],
                [ReasonGroupID],[Comments],[Modifier],[ModifiedTime],[ModifiedLocalTime],[Creator],[CreateTime],[CreateLocalTime],[SystemID]) values
                (@ServiceReasonLogID,@CalledRepairOrderID,@ReasonID,@ReasonDescription,@Status,
                @ReasonGroupID,@Comments,'{0}','{1}','{2}','{0}','{1}','{2}','{3}')", userId, DateTime.Now, DateTime.Now, Framework.SystemID);

                SqlParameter[] parameters = new SqlParameter[]{
                    new SqlParameter("@ServiceReasonLogID",SqlDbType.VarChar),
                    new SqlParameter("@CalledRepairOrderID",SqlDbType.NVarChar),
                    new SqlParameter("@ReasonID",SqlDbType.NVarChar),
                    new SqlParameter("@ReasonDescription",SqlDbType.NVarChar),
                    new SqlParameter("@Status",SqlDbType.VarChar),
                    new SqlParameter("@Comments",SqlDbType.NVarChar),
                    new SqlParameter("@ReasonGroupID",SqlDbType.VarChar)
                    };

                parameters[0].Value = (Object)Model.ServiceReasonLogID ?? DBNull.Value;
                parameters[1].Value = (Object)Model.CalledRepairOrderID ?? DBNull.Value;
                parameters[2].Value = (Object)Model.ReasonID ?? DBNull.Value;
                parameters[3].Value = (Object)Model.ReasonDescription ?? DBNull.Value;
                parameters[4].Value = (Object)Model.Status ?? DBNull.Value;
                parameters[5].Value = (Object)Model.Comments ?? DBNull.Value;
                parameters[6].Value = (Object)Model.ReasonGroupID ?? DBNull.Value;

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
        /// SAM 2017年6月2日11:10:08
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="Model"></param>
        /// <returns></returns>
        public static bool update(string userId, EMS_ServiceReasonLog Model)
        {
            try
            {
                string sql = String.Format(@"update[EMS_ServiceReasonLog] set {0},
                [ReasonID]=@ReasonID,[ReasonDescription]=@ReasonDescription,[ReasonGroupID]=@ReasonGroupID,
                [Status]=@Status,[Comments]=@Comments 
                where [ServiceReasonLogID]=@ServiceReasonLogID", UniversalService.getUpdateUTC(userId));
                SqlParameter[] parameters = new SqlParameter[]{
                    new SqlParameter("@ServiceReasonLogID",SqlDbType.VarChar),
                    new SqlParameter("@ReasonID",SqlDbType.NVarChar),
                    new SqlParameter("@ReasonDescription",SqlDbType.NVarChar),
                    new SqlParameter("@Status",SqlDbType.VarChar),
                    new SqlParameter("@Comments",SqlDbType.NVarChar),
                    new SqlParameter("@ReasonGroupID",SqlDbType.VarChar)
                    };

                parameters[0].Value = Model.ServiceReasonLogID;
                parameters[1].Value = (Object)Model.ReasonID ?? DBNull.Value;
                parameters[2].Value = (Object)Model.ReasonDescription ?? DBNull.Value;
                parameters[3].Value = (Object)Model.Status ?? DBNull.Value;
                parameters[4].Value = (Object)Model.Comments ?? DBNull.Value;
                parameters[5].Value = (Object)Model.ReasonGroupID ?? DBNull.Value;

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
        /// SAM 2017年6月2日11:10:15
        /// </summary>
        /// <param name="ServiceReasonLogID"></param>
        /// <returns></returns>
        public static EMS_ServiceReasonLog get(string ServiceReasonLogID)
        {
            string sql = string.Format(@"select Top 1 * from [EMS_ServiceReasonLog] where [ServiceReasonLogID] = '{0}'  and [SystemID] = '{1}' ", ServiceReasonLogID, Framework.SystemID);

            DataTable dt = SQLHelper.ExecuteDataTable(sql, CommandType.Text);

            if (dt.Rows.Count == 0)
                return null;
            else
                return ToEntity(dt.Rows[0]);
        }


        /// <summary>
        /// 获取维修记录
        /// SAM 2017年6月2日11:16:08
        /// </summary>
        /// <param name="CalledRepairOrderID"></param>
        /// <param name="page">页码</param>
        /// <param name="rows">行数</param>
        /// <param name="count"></param>
        /// <returns></returns>
        public static IList<Hashtable> Ems00005GetServiceList(string CalledRepairOrderID, int page, int rows, ref int count)
        {
            string select = string.Format(@"select A.ServiceReasonLogID,A.CalledRepairOrderID,A.ReasonID,A.ReasonDescription,
            A.Status,A.Comments,E.Code as ReasonCode,A.ReasonGroupID,
            F.Code as GroupCode,F.Name as GroupName,
            (CASE WHEN B.Emplno is null or B.Emplno = '' THEN B.Account else B.Emplno END)+'-'+B.UserName as Creator,A.CreateLocalTime as CreateTime,
            (CASE WHEN A.CreateLocalTime=A.ModifiedLocalTime THEN NULL else (CASE WHEN C.Emplno is null or C.Emplno = '' THEN C.Account else C.Emplno END)+'-'+C.UserName END) as Modifier,
            (CASE WHEN A.CreateLocalTime=A.ModifiedLocalTime THEN NULL else A.ModifiedLocalTime END) as ModifiedTime ");

            string sql = string.Format(@"  from [EMS_ServiceReasonLog] A 
            left join [SYS_MESUsers] B on A.[Creator] = B.[MESUserID]
            left join [SYS_MESUsers] C on A.[Modifier] = C.[MESUserID]
            left join [EMS_CalledRepairOrder] D on A.[CalledRepairOrderID] = D.[CalledRepairOrderID]
            left join [SYS_Parameters] E on A.[ReasonID] = E.[ParameterID]
            left join [SYS_Parameters] F on A.[ReasonGroupID] = F.[ParameterID]
            where A.[SystemID]='{0}' and A.[Status] <> '{0}0201213000003' and A.[CalledRepairOrderID]='{1}' ", Framework.SystemID, CalledRepairOrderID);

            count = UniversalService.getCount(sql, null);

            string orderby = " A.[Status],D.[Code] ";

            DataTable dt = UniversalService.getTable(select, sql, orderby, null, page, rows);

            return ToHashtableList(dt);
        }

        /// <summary>
        /// 判断重复
        /// SAM 2017年6月2日11:15:57
        /// </summary>
        /// <param name="ReasonID"></param>
        /// <param name="CalledRepairOrderID"></param>
        /// <param name="ServiceReasonLogID"></param>
        /// <returns></returns>
        public static bool CheckReason(string ReasonID, string CalledRepairOrderID, string ServiceReasonLogID)
        {
            string sql = string.Format(@"select Top 1 * from [EMS_ServiceReasonLog] where [SystemID]='{0}' and [Status] <> '{0}0201213000003' ", Framework.SystemID);

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

            /*ServiceReasonLogID（参数流水号）用于在更新时，排除他自己*/
            if (!string.IsNullOrWhiteSpace(ServiceReasonLogID))
                sql = sql + string.Format(@" and [ServiceReasonLogID] <> '{0}' ", ServiceReasonLogID);

            if (!string.IsNullOrWhiteSpace(CalledRepairOrderID))
                sql = sql + string.Format(@" and [CalledRepairOrderID] = '{0}' ", CalledRepairOrderID);

            DataTable dt = SQLHelper.ExecuteDataTable(sql, CommandType.Text, parameters);

            if (dt.Rows.Count == 0)
                return false;
            else
                return true;
        }
    }
}

