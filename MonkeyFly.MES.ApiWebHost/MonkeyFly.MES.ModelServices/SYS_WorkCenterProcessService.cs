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
    public class SYS_WorkCenterProcessService : SuperModel<SYS_WorkCenterProcess>
    {
        /// <summary>
        /// 新增
        /// SAM 2017年5月24日17:33:21
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="Model"></param>
        /// <returns></returns>
        public static bool insert(string userId, SYS_WorkCenterProcess Model)
        {
            try
            {
                string sql = string.Format(@"insert[SYS_WorkCenterProcess]([WorkCenterProcessID],[WorkCenterID],[ProcessID],[Status],
                [Comments],[Modifier],[ModifiedTime],[ModifiedLocalTime],[Creator],[CreateTime],[CreateLocalTime],[SystemID]) values
                 (@WorkCenterProcessID,@WorkCenterID,@ProcessID,@Status,@Comments,'{0}','{1}','{2}','{0}','{1}','{2}','{3}')", userId, DateTime.Now, DateTime.Now, Framework.SystemID);

                SqlParameter[] parameters = new SqlParameter[]{
                    new SqlParameter("@WorkCenterProcessID",SqlDbType.VarChar),
                    new SqlParameter("@WorkCenterID",SqlDbType.VarChar),
                    new SqlParameter("@ProcessID",SqlDbType.VarChar),
                    new SqlParameter("@Status",SqlDbType.VarChar),
                    new SqlParameter("@Comments",SqlDbType.NVarChar),
                    };

                parameters[0].Value = (Object)Model.WorkCenterProcessID ?? DBNull.Value;
                parameters[1].Value = (Object)Model.WorkCenterID ?? DBNull.Value;
                parameters[2].Value = (Object)Model.ProcessID ?? DBNull.Value;
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
        /// SAM 2017年5月24日17:33:26
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="Model"></param>
        /// <returns></returns>
        public static bool update(string userId, SYS_WorkCenterProcess Model)
        {
            try
            {
                string sql = String.Format(@"update[SYS_WorkCenterProcess] set {0},
                [ProcessID]=@ProcessID,[Status]=@Status,
                [Comments]=@Comments where [WorkCenterProcessID]=@WorkCenterProcessID", UniversalService.getUpdateUTC(userId));
                SqlParameter[] parameters = new SqlParameter[]{
                    new SqlParameter("@WorkCenterProcessID",SqlDbType.VarChar),
                    new SqlParameter("@ProcessID",SqlDbType.VarChar),
                    new SqlParameter("@Status",SqlDbType.VarChar),
                    new SqlParameter("@Comments",SqlDbType.NVarChar),
                    };

                parameters[0].Value = Model.WorkCenterProcessID;
                parameters[1].Value = (Object)Model.ProcessID ?? DBNull.Value;
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
        /// SAM 2017年5月24日17:33:35
        /// </summary>
        /// <param name="ProcessOperationID"></param>
        /// <returns></returns>
        public static SYS_WorkCenterProcess get(string WorkCenterProcessID)
        {
            string sql = string.Format(@"select Top 1 * from [SYS_WorkCenterProcess] where [WorkCenterProcessID] = '{0}'  and [SystemID] = '{1}' ", WorkCenterProcessID, Framework.SystemID);

            DataTable dt = SQLHelper.ExecuteDataTable(sql, CommandType.Text);

            if (dt.Rows.Count == 0)
                return null;
            else
                return ToEntity(dt.Rows[0]);
        }

        /// <summary>
        /// 判断唯一性
        /// SAM 2017年5月24日17:37:18
        /// </summary>
        /// <param name="ProcessID"></param>
        /// <param name="WorkCenterID"></param>
        /// <param name="WorkCenterProcessID"></param>
        /// <returns></returns>
        public static bool Check(string ProcessID, string WorkCenterID, string WorkCenterProcessID)
        {
            string sql = String.Format(@"select Top 1 * from [SYS_WorkCenterProcess] where [SystemID]='{0}' and [Status] <> '{0}0201213000003' and [WorkCenterID] = '{1}' ", Framework.SystemID, WorkCenterID);

            /*先定义Code，默认给DbNull,以免参数找不到*/
            SqlParameter[] parameters = new SqlParameter[]{
                new SqlParameter("@ProcessID",SqlDbType.VarChar),
            };
            parameters[0].Value = DBNull.Value;

            /*因为ProcessID是通过手动输入的，所以需要用参数的形式去拼SQL*/
            if (!string.IsNullOrWhiteSpace(ProcessID))
            {
                sql = sql + String.Format(@" and [ProcessID] =@ProcessID ");
                parameters[0].Value = ProcessID;
            }

            /*WorkCenterProcessID（参数流水号）用于在更新时，排除他自己*/
            if (!string.IsNullOrWhiteSpace(WorkCenterProcessID))
                sql = sql + String.Format(@" and [WorkCenterProcessID] <> '{0}' ", WorkCenterProcessID);

            DataTable dt = SQLHelper.ExecuteDataTable(sql, CommandType.Text, parameters);

            if (dt.Rows.Count == 0)
                return false;
            else
                return true;
        }

        /// <summary>
        /// 根据工作中心获取制程列表
        /// SAM 2017年5月25日09:29:28 
        /// </summary>
        /// <param name="WorkCenterID"></param>
        /// <param name="page">页码</param>
        /// <param name="rows">行数</param>
        /// <param name="count"></param>
        /// <returns></returns>
        public static IList<Hashtable> Inf00018WorkCenterGetProcessList(string WorkCenterID, int page, int rows, ref int count)
        {
            string select = string.Format(@"select A.WorkCenterProcessID,A.ProcessID,A.WorkCenterID,A.Status,
            D.Code as ProcessCode,D.Name as ProcessName,D.Comments,
            B.UserName as Creator,A.CreateLocalTime as CreateTime,
            (CASE WHEN A.CreateLocalTime=A.ModifiedLocalTime THEN NULL else C.UserName END) as Modifier,
            (CASE WHEN A.CreateLocalTime=A.ModifiedLocalTime THEN NULL else A.ModifiedLocalTime END) as ModifiedTime ");

            string sql = string.Format(@" from [SYS_WorkCenterProcess] A 
            left join [SYS_MESUsers] B on A.Creator = B.MESUserID
            left join [SYS_MESUsers] C on A.Modifier = C.MESUserID
            left join [SYS_Parameters] D on A.ProcessID = D.ParameterID
            where A.[SystemID]='{0}' and A.Status <> '{0}0201213000003' and A.[WorkCenterID]='{1}' ", Framework.SystemID, WorkCenterID);

            count = UniversalService.getCount(sql, null);

            string orderby = "A.[Status],D.[Code] ";

            DataTable dt = UniversalService.getTable(select, sql, orderby, null, page, rows);

            return ToHashtableList(dt);
        }

        /// <summary>
        /// 判断制程是否在工作重心中使用
        /// SAM 2017年5月25日11:54:04
        /// </summary>
        /// <param name="ProcessID"></param>
        /// <returns></returns>
        public static bool CheckProcess(string ProcessID)
        {
            string sql = string.Format(@"select * from [SYS_WorkCenterProcess] where [ProcessID] = '{0}' and [SystemID] = '{1}' and [Status] = '{1}0201213000001' ", ProcessID, Framework.SystemID);

            DataTable dt = SQLHelper.ExecuteDataTable(sql, CommandType.Text);

            if (dt.Rows.Count == 0)
                return false;
            else
                return true;
        }

        /// <summary>
        /// 删除制程时一并删除制程工作中心表的数据
        /// Joint 2017年7月27日14:18:54
        /// </summary>
        /// <param name="userid"></param>
        /// <param name="OperationID"></param>
        /// <returns></returns>
        public static bool DeleteProcess(string userid, string ProcessID)
        {
            try
            {
                string sql = string.Format(@"update[SYS_WorkCenterProcess] set {0},
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
        /// 根据制程获取工作中心列表（不分页）
        /// Joint 2017年7月27日16:10:04
        /// </summary>
        /// <param name="ProcessID"></param>
        /// <returns></returns>
        public static IList<Hashtable> Inf00018GetWorkCenterProcessList(string ProcessID)
        {
            string select = string.Format(@"select A.WorkCenterProcessID,A.WorkCenterID,A.ProcessID,A.Comments,
            D.Code as WorkCenterNo,D.Name as WorkCenterDescription,D.InoutMark,(select [Name] from [SYS_Parameters] where D.[Status] = [ParameterID]) as Status,
            B.Emplno+'-'+B.UserName as Creator,A.CreateLocalTime as CreateTime,
            (CASE WHEN A.CreateLocalTime=A.ModifiedLocalTime THEN NULL else C.Emplno+'-'+C.UserName END) as Modifier,
            (CASE WHEN A.CreateLocalTime=A.ModifiedLocalTime THEN NULL else A.ModifiedLocalTime END) as ModifiedTime ");

            string sql = string.Format(@"  from [SYS_WorkCenterProcess] A 
            left join [SYS_MESUsers] B on A.[Creator] = B.[MESUserID]
            left join [SYS_MESUsers] C on A.[Modifier] = C.[MESUserID]
            left join [SYS_WorkCenter] D on A.[WorkCenterID] = D.[WorkCenterID]
            where A.[SystemID]='{0}' and A.[Status] = '{0}0201213000001' and [ProcessID] = '{1}'", Framework.SystemID, ProcessID);


            String orderby = "order by D.[Code] ";

            DataTable dt = SQLHelper.ExecuteDataTable(select + sql + orderby, CommandType.Text);

            return ToHashtableList(dt);
        }


        /// <summary>
        /// 根据工作中心获取制程列表（不分页）
        /// Joint 2017年7月28日17:06:46
        /// </summary>
        /// <param name="WorkCenterID"></param>
        /// <returns></returns>
        public static IList<Hashtable> Inf00018GetProcessWorkCenterList(string WorkCenterID)
        {
            string select = string.Format(@"select A.WorkCenterProcessID,A.WorkCenterID,A.ProcessID,A.Comments,
            D.Code as ProcessNo,D.Name as ProcessDescription,(CASE WHEN D.IsEnable=1 THEN '正常' ELSE '作废' END) as Status,
            D.IsDefault as EnableProcess,
            B.Emplno+'-'+B.UserName as Creator,A.CreateLocalTime as CreateTime,
            (CASE WHEN A.CreateLocalTime=A.ModifiedLocalTime THEN NULL else C.Emplno+'-'+C.UserName END) as Modifier,
            (CASE WHEN A.CreateLocalTime=A.ModifiedLocalTime THEN NULL else A.ModifiedLocalTime END) as ModifiedTime ");

            string sql = string.Format(@"  from [SYS_WorkCenterProcess] A 
            left join [SYS_MESUsers] B on A.[Creator] = B.[MESUserID]
            left join [SYS_MESUsers] C on A.[Modifier] = C.[MESUserID]
            left join [SYS_Parameters] D on A.[ProcessID] = D.[ParameterID]
            where A.[SystemID]='{0}' and A.[Status] = '{0}0201213000001' and [WorkCenterID] = '{1}'", Framework.SystemID, WorkCenterID);


            String orderby = "order by D.[Code] ";

            DataTable dt = SQLHelper.ExecuteDataTable(select + sql + orderby, CommandType.Text);

            return ToHashtableList(dt);
        }

        /// <summary>
        /// 将不在范围内的制程里的工作中心都删除了
        /// Joint 2017年7月27日16:54:53
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="WorkCenterProcessIDs"></param>
        /// <param name="ProcessID"></param>
        /// <returns></returns>
        public static bool Delete(string userId, string WorkCenterProcessIDs, string ProcessID)
        {
            try
            {
                string sql = string.Format(@"update[SYS_WorkCenterProcess] set {0},
               [Status]='{1}0201213000003' where [ProcessID]='{2}' ", UniversalService.getUpdateUTC(userId), Framework.SystemID, ProcessID);

                if (!string.IsNullOrWhiteSpace(WorkCenterProcessIDs))
                    sql += string.Format(@" and [WorkCenterProcessID] not in ('{0}')", WorkCenterProcessIDs);

                return SQLHelper.ExecuteNonQuery(sql, CommandType.Text) > 0;
            }
            catch (Exception ex)
            {
                DataLogerService.writeerrlog(ex);
                return false;
            }
        }

        /// <summary>
        /// 删除不在范围内的工作中心里的制程
        /// Joint 2017年7月31日15:08:15
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="WorkCenterProcessIDs"></param>
        /// <param name="ProcessID"></param>
        /// <returns></returns>
        public static bool Deleted(string userId, string WorkCenterProcessIDs, string WorkCenterID)
        {
            try
            {
                string sql = string.Format(@"update[SYS_WorkCenterProcess] set {0},
               [Status]='{1}0201213000003' where [WorkCenterID]='{2}' ", UniversalService.getUpdateUTC(userId), Framework.SystemID, WorkCenterID);

                if (!string.IsNullOrWhiteSpace(WorkCenterProcessIDs))
                    sql += string.Format(@" and [WorkCenterProcessID] not in ('{0}')", WorkCenterProcessIDs);

                return SQLHelper.ExecuteNonQuery(sql, CommandType.Text) > 0;
            }
            catch (Exception ex)
            {
                DataLogerService.writeerrlog(ex);
                return false;
            }
        }
        /// <summary>
        /// 删除工作中心时一并删除制程工作中心表的数据
        /// Joint 2017年7月28日14:51:35
        /// </summary>
        /// <param name="userid"></param>
        /// <param name="WorkCenterID"></param>
        /// <returns></returns>
        public static bool DeleteWorkCenter(string userid, string WorkCenterID)
        {
            try
            {
                string sql = string.Format(@"update[SYS_WorkCenterProcess] set {0},
               [Status]='{1}0201213000003' where [WorkCenterID]='{2}' ", UniversalService.getUpdateUTC(userid), Framework.SystemID, WorkCenterID);
                return SQLHelper.ExecuteNonQuery(sql, CommandType.Text) > 0;
            }
            catch (Exception ex)
            {
                DataLogerService.writeerrlog(ex);
                return false;
            }
        }

        /// <summary>
        /// Iot00003工作中心按钮的制程按钮
        /// Mouse 2017年9月12日16:58:14
        /// </summary>
        /// <param name="Token"></param>
        /// <param name="WorkCenterID"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <returns></returns>
        public static IList<Hashtable> Iot00003GetCenterProcess(string WorkCenterID, int page, int rows, ref int count)
        {
            string select = string.Format(@"select DISTINCT A.WorkCenterProcessID,A.ProcessID,B.Code,B.Name,B.Comments ");

            string sql = string.Format(@"from [SYS_WorkCenterProcess] A
                        left join [SYS_Parameters] B on A.ProcessID=B.ParameterID
                        where A.[WorkCenterID]='{0}' and A.[Status]='{1}0201213000001' and A.[SystemID]='{1}' and B.SystemID='{1}' and  B.IsEnable='1'", WorkCenterID,Framework.SystemID);

            string orderby = " order by A.WorkCenterProcessID";

            DataTable dt = SQLHelper.ExecuteDataTable(select + sql + orderby, CommandType.Text);
            return ToHashtableList(dt);
        }

        /// <summary>
        /// 查询工作中心是否于制程存在关系
        /// Mouse 2017年9月12日18:13:56
        /// </summary>
        /// <param name="Token"></param>
        /// <param name="WorkCenterID"></param>
        /// <returns></returns>
        public static bool Iot00003CheckCenterProcess(string WorkCenterID)
        {
            string sql = string.Format(@"select * from [SYS_WorkCenterProcess] where [Status]='{1}0201213000001' and [SystemID]='{1}' and [WorkCenterID]='{0}'", WorkCenterID, Framework.SystemID);
            DataTable dt = SQLHelper.ExecuteDataTable(sql, CommandType.Text);
            if (dt.Rows.Count == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}

