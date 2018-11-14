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
    public class SYS_ProcessOperationService : SuperModel<SYS_ProcessOperation>
    {
        /// <summary>
        /// 新增
        /// SAM 2017年5月24日17:30:31
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="Model"></param>
        /// <returns></returns>
        public static bool insert(string userId, SYS_ProcessOperation Model)
        {
            try
            {
                string sql = string.Format(@"insert[SYS_ProcessOperation]([ProcessOperationID],[ProcessID],[OperationID],[Status],
                [Comments],[Modifier],[ModifiedTime],[ModifiedLocalTime],[Creator],[CreateTime],[CreateLocalTime],[SystemID]) values
                (@ProcessOperationID,@ProcessID,@OperationID,@Status,@Comments,'{0}','{1}','{2}','{0}','{1}','{2}','{3}')", userId, DateTime.Now, DateTime.Now, Framework.SystemID);

                SqlParameter[] parameters = new SqlParameter[]{
                    new SqlParameter("@ProcessOperationID",SqlDbType.VarChar),
                    new SqlParameter("@ProcessID",SqlDbType.VarChar),
                    new SqlParameter("@OperationID",SqlDbType.VarChar),
                    new SqlParameter("@Status",SqlDbType.VarChar),
                    new SqlParameter("@Comments",SqlDbType.NVarChar),
                    };

                parameters[0].Value = (Object)Model.ProcessOperationID ?? DBNull.Value;
                parameters[1].Value = (Object)Model.ProcessID ?? DBNull.Value;
                parameters[2].Value = (Object)Model.OperationID ?? DBNull.Value;
                parameters[3].Value = (Object)Model.Status ?? DBNull.Value;
                parameters[4].Value = (Object)Model.Comments ?? DBNull.Value;

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
        /// SAM 2017年5月24日17:30:35
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="Model"></param>
        /// <returns></returns>
        public static bool update(string userId, SYS_ProcessOperation Model)
        {
            try
            {
                string sql = String.Format(@"update[SYS_ProcessOperation] set {0},
                [OperationID]=@OperationID,[Status]=@Status,
                [Comments]=@Comments where [ProcessOperationID]=@ProcessOperationID", UniversalService.getUpdateUTC(userId));
                SqlParameter[] parameters = new SqlParameter[]{
                    new SqlParameter("@ProcessOperationID",SqlDbType.VarChar),
                    new SqlParameter("@OperationID",SqlDbType.VarChar),
                    new SqlParameter("@Status",SqlDbType.VarChar),
                    new SqlParameter("@Comments",SqlDbType.NVarChar),
                    };

                parameters[0].Value = Model.ProcessOperationID;
                parameters[1].Value = (Object)Model.OperationID ?? DBNull.Value;
                parameters[2].Value = (Object)Model.Status ?? DBNull.Value;
                parameters[3].Value = (Object)Model.Comments ?? DBNull.Value;

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
        /// SAM 2017年5月24日17:30:42
        /// </summary>
        /// <param name="ProcessOperationID"></param>
        /// <returns></returns>
        public static SYS_ProcessOperation get(string ProcessOperationID)
        {
            string sql = string.Format(@"select Top 1 * from [SYS_ProcessOperation] where [ProcessOperationID] = '{0}'  and [SystemID] = '{1}' ", ProcessOperationID, Framework.SystemID);

            DataTable dt = SQLHelper.ExecuteDataTable(sql, CommandType.Text);

            if (dt.Rows.Count == 0)
                return null;
            else
                return ToEntity(dt.Rows[0]);
        }


        /// <summary>
        /// 判断在同一个制程中是否存在相同的工序
        /// SAM 2017年5月24日17:31:54
        /// </summary>
        /// <param name="OperationID"></param>
        /// <param name="ProcessID"></param>
        /// <param name="ProcessOperationID"></param>
        /// <returns></returns>
        public static bool Check(string OperationID, string ProcessID,string ProcessOperationID)
        {
            string sql = String.Format(@"select Top 1 * from [SYS_ProcessOperation] where [SystemID]='{0}' and [Status] <> '{0}0201213000003' and [ProcessID] = '{1}' ", Framework.SystemID, ProcessID);

            /*先定义Code，默认给DbNull,以免参数找不到*/
            SqlParameter[] parameters = new SqlParameter[]{
                new SqlParameter("@OperationID",SqlDbType.VarChar),
            };
            parameters[0].Value = DBNull.Value;

            /*因为Code是通过手动输入的，所以需要用参数的形式去拼SQL*/
            if (!string.IsNullOrWhiteSpace(OperationID))
            {
                sql = sql + String.Format(@" and [OperationID] =@OperationID ");
                parameters[0].Value = OperationID;
            }

            /*ProcessOperationID（参数流水号）用于在更新时，排除他自己*/
            if (!string.IsNullOrWhiteSpace(ProcessOperationID))
                sql = sql + String.Format(@" and [ProcessOperationID] <> '{0}' ", ProcessOperationID);

            DataTable dt = SQLHelper.ExecuteDataTable(sql, CommandType.Text, parameters);

            if (dt.Rows.Count == 0)
                return false;
            else
                return true;
        }

        /// <summary>
        /// 根据制程获取制程的工序
        /// SAM 2017年5月24日17:42:54
        /// </summary>
        /// <param name="ProcessID"></param>
        /// <param name="page">页码</param>
        /// <param name="rows">行数</param>
        /// <param name="count"></param>
        /// <returns></returns>
        public static IList<Hashtable> Inf00018ProcessGetOperationList(string ProcessID, int page, int rows, ref int count)
        {
            string select = string.Format(@"select A.ProcessOperationID,A.ProcessID,A.OperationID,A.Status,
            D.Code as OperationCode,D.Name as OperationName,D.Comments,
            B.UserName as Creator,A.CreateLocalTime as CreateTime,
            (CASE WHEN A.CreateLocalTime=A.ModifiedLocalTime THEN NULL else C.UserName END) as Modifier,
            (CASE WHEN A.CreateLocalTime=A.ModifiedLocalTime THEN NULL else A.ModifiedLocalTime END) as ModifiedTime ");

            string sql = string.Format(@" from [SYS_ProcessOperation] A 
            left join [SYS_MESUsers] B on A.Creator = B.MESUserID
            left join [SYS_MESUsers] C on A.Modifier = C.MESUserID
            left join [SYS_Parameters] D on A.OperationID = D.ParameterID
            where A.[SystemID]='{0}' and A.Status <> '{0}0201213000003' and A.[ProcessID]='{1}' ", Framework.SystemID, ProcessID);

            count = UniversalService.getCount(sql, null);

            string orderby = "A.[Status],D.[Code] ";

            DataTable dt = UniversalService.getTable(select, sql, orderby, null, page, rows);

            return ToHashtableList(dt);
        }

        /// <summary>
        /// 根据工序流水号获取制程(不分页)
        /// Joint 2017年7月27日10:09:05
        /// </summary>
        /// <param name="OperationID"></param>
        /// <returns></returns>
        public static IList<Hashtable> Inf00018GetProcessList(string OperationID)
        {
            string select = string.Format(@"select A.ProcessOperationID,A.OperationID,A.ProcessID,A.Comments,
            D.Code as ProcessNo,D.Name as ProcessDescription,D.IsDefault as EnableProcess,
            (CASE WHEN D.IsEnable=1 THEN '正常' ELSE '作废' END) as Status,
            B.Emplno+'-'+B.UserName as Creator,A.CreateLocalTime as CreateTime,
            (CASE WHEN A.CreateLocalTime=A.ModifiedLocalTime THEN NULL else C.Emplno+'-'+C.UserName END) as Modifier,
            (CASE WHEN A.CreateLocalTime=A.ModifiedLocalTime THEN NULL else A.ModifiedLocalTime END) as ModifiedTime ");

            string sql = string.Format(@"  from [SYS_ProcessOperation] A 
            left join [SYS_MESUsers] B on A.[Creator] = B.[MESUserID]
            left join [SYS_MESUsers] C on A.[Modifier] = C.[MESUserID]
            left join [SYS_Parameters] D on A.[ProcessID] = D.[ParameterID]
            where A.[SystemID]='{0}' and A.[Status] = '{0}0201213000001' and [OperationID] = '{1}'", Framework.SystemID, OperationID);


            String orderby = "order by D.[Code] ";

            DataTable dt = SQLHelper.ExecuteDataTable(select + sql + orderby, CommandType.Text);

            return ToHashtableList(dt);
        }

        /// <summary>
        /// 根据制程流水号获取工序(不分页)
        /// Joint 2017年7月27日14:54:08
        /// </summary>
        /// <param name="ProcessID"></param>
        /// <returns></returns>
        public static IList<Hashtable> Inf00018GetOperationList(string ProcessID)
        {
            string select = string.Format(@"select A.ProcessOperationID,A.OperationID,A.ProcessID,A.Comments,
            D.Code as WorkOrderNo,D.Name as WorkOrderDescription,
            (CASE WHEN D.IsEnable=1 THEN '正常' ELSE '作废' END) as Status,
            B.Emplno+'-'+B.UserName as Creator,A.CreateLocalTime as CreateTime,
            (CASE WHEN A.CreateLocalTime=A.ModifiedLocalTime THEN NULL else C.Emplno+'-'+C.UserName END) as Modifier,
            (CASE WHEN A.CreateLocalTime=A.ModifiedLocalTime THEN NULL else A.ModifiedLocalTime END) as ModifiedTime ");

            string sql = string.Format(@"  from [SYS_ProcessOperation] A 
            left join [SYS_MESUsers] B on A.[Creator] = B.[MESUserID]
            left join [SYS_MESUsers] C on A.[Modifier] = C.[MESUserID]
            left join [SYS_Parameters] D on A.[OperationID] = D.[ParameterID]
            where A.[SystemID]='{0}' and A.[Status] = '{0}0201213000001' and [ProcessID] = '{1}'", Framework.SystemID, ProcessID);


            String orderby = "order by D.[Code] ";

            DataTable dt = SQLHelper.ExecuteDataTable(select + sql + orderby, CommandType.Text);

            return ToHashtableList(dt);
        }

        /// <summary>
        /// 判断工序是否在制程中使用
        /// SAM 2017年5月25日11:47:51
        /// </summary>
        /// <param name="OperationID"></param>
        /// <returns></returns>
        public static bool CheckOperation(string OperationID)
        {
            string sql = string.Format(@"select * from [SYS_ProcessOperation] where [OperationID] = '{0}' and [SystemID] = '{1}' and [Status] = '{1}0201213000001' ", OperationID, Framework.SystemID);

            DataTable dt = SQLHelper.ExecuteDataTable(sql, CommandType.Text);

            if (dt.Rows.Count == 0)
                return false;
            else
                return true;
        }

        /// <summary>
        /// 删除工序时一并删除制程工序表的数据
        /// Joint 2017年7月27日11:29:44
        /// </summary>
        /// <param name="userid"></param>
        /// <param name="OperationID"></param>
        /// <returns></returns>
        public static bool DeleteOperation(string userid, string OperationID)
        {
            try
            {
                string sql = string.Format(@"update[SYS_ProcessOperation] set {0},
               [Status]='{1}0201213000003' where [OperationID]='{2}' ", UniversalService.getUpdateUTC(userid), Framework.SystemID, OperationID);
                bool Ex = SQLHelper.ExecuteNonQuery(sql, CommandType.Text) > 0;
                return Ex;
            }          
            catch (Exception ex)
            {
                DataLogerService.writeerrlog(ex);
                return false;
            }
        }

        /// <summary>
        /// 删除制程时一并删除制程工序表的数据
        /// Joint 2017年7月27日14:16:05
        /// </summary>
        /// <param name="userid"></param>
        /// <param name="OperationID"></param>
        /// <returns></returns>
        public static bool DeleteProcess(string userid, string ProcessID)
        {
            try
            {
                string sql = string.Format(@"update[SYS_ProcessOperation] set {0},
               [Status]='{1}0201213000003' where [ProcessID]='{2}' ", UniversalService.getUpdateUTC(userid), Framework.SystemID, ProcessID);
                return SQLHelper.ExecuteNonQuery(sql, CommandType.Text) > 0;
            }
            catch (Exception ex)
            {
                DataLogerService.writeerrlog(ex);
                return false;
            }
        }
        /// <summary>
        /// 将不在范围内的工序里的制程都删除了
        /// Joint 2017年7月27日09:30:46
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="ProcessOperationIDs"></param>
        /// <param name="OperationID"></param>
        /// <returns></returns>

        public static bool DeleteOperation(string userId, string ProcessOperationIDs, string OperationID)
        {
            try
            {
                string sql = string.Format(@"update[SYS_ProcessOperation] set {0},
               [Status]='{1}0201213000003' where [OperationID]='{2}' ", UniversalService.getUpdateUTC(userId), Framework.SystemID, OperationID);

                if (!string.IsNullOrWhiteSpace(ProcessOperationIDs))
                    sql += string.Format(@" and [ProcessOperationID] not in ('{0}')", ProcessOperationIDs);

                return SQLHelper.ExecuteNonQuery(sql, CommandType.Text) > 0;
            }
            catch (Exception ex)
            {
                DataLogerService.writeerrlog(ex);
                return false;
            }
        }

        /// <summary>
        /// 将不在范围内的制程里的工序都删除了
        /// Joint 2017年7月27日15:07:36
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="ProcessOperationIDs"></param>
        /// <param name="ProcessID"></param>
        /// <returns></returns>

        public static bool DeleteProcess(string userId, string ProcessOperationIDs, string ProcessID)
        {
            try
            {
                string sql = string.Format(@"update[SYS_ProcessOperation] set {0},
               [Status]='{1}0201213000003' where [ProcessID]='{2}' ", UniversalService.getUpdateUTC(userId), Framework.SystemID, ProcessID);

                if (!string.IsNullOrWhiteSpace(ProcessOperationIDs))
                    sql += string.Format(@" and [ProcessOperationID] not in ('{0}')", ProcessOperationIDs);

                return SQLHelper.ExecuteNonQuery(sql, CommandType.Text) > 0;
            }
            catch (Exception ex)
            {
                DataLogerService.writeerrlog(ex);
                return false;
            }
        }

        /// <summary>
        /// 判断是否已存在工序和制程的映射
        /// </summary>
        /// <param name="ProcessID"></param>
        /// <param name="OperationID"></param>
        /// <returns></returns>
        public static bool CheckProcess(string ProcessID, string OperationID)
        {
            string sql = String.Format(@"select Top 1 * from [SYS_ProcessOperation] where [SystemID]='{0}' and [Status] <> '{0}0201213000003' ", Framework.SystemID);

            if (!string.IsNullOrWhiteSpace(ProcessID))
                sql = sql + String.Format(@" and [ProcessID] = '{0}' ", ProcessID);

            if (!string.IsNullOrWhiteSpace(OperationID))
                sql = sql + String.Format(@" and [OperationID] = '{0}' ", OperationID);

            DataTable dt = SQLHelper.ExecuteDataTable(sql, CommandType.Text);

            if (dt.Rows.Count == 0)
                return false;
            else
                return true;
        }
    }
}

