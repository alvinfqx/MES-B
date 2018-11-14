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
    public class SYS_AlternativeGroupDetailsService : SuperModel<SYS_AlternativeGroupDetails>
    {
        /// <summary>
        /// 新增
        /// SAM 2017年5月23日09:33:26
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="Model"></param>
        /// <returns></returns>
        public static bool insert(string userId, SYS_AlternativeGroupDetails Model)
        {
            try
            {
                string sql = string.Format(@"insert[SYS_AlternativeGroupDetails]([AGDetailID],[GroupID],[DetailID],[Status],
                [Comments],[Modifier],[ModifiedTime],[ModifiedLocalTime],[Creator],[CreateTime],[CreateLocalTime],[SystemID]) values
                (@AGDetailID,@GroupID,@DetailID,@Status,@Comments,'{0}','{1}','{2}','{0}','{1}','{2}','{3}')", userId, DateTime.Now, DateTime.Now, Framework.SystemID);

                SqlParameter[] parameters = new SqlParameter[]{
                    new SqlParameter("@AGDetailID",SqlDbType.VarChar),
                    new SqlParameter("@GroupID",SqlDbType.VarChar),
                    new SqlParameter("@DetailID",SqlDbType.VarChar),
                    new SqlParameter("@Status",SqlDbType.VarChar),
                    new SqlParameter("@Comments",SqlDbType.VarChar),
                    };

                parameters[0].Value = (Object)Model.AGDetailID ?? DBNull.Value;
                parameters[1].Value = (Object)Model.GroupID ?? DBNull.Value;
                parameters[2].Value = (Object)Model.DetailID ?? DBNull.Value;
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
        /// SAM 2017年5月23日09:33:41
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="Model"></param>
        /// <returns></returns>
        public static bool update(string userId, SYS_AlternativeGroupDetails Model)
        {
            try
            {
                string sql = String.Format(@"update[SYS_AlternativeGroupDetails] set {0},
                [DetailID]=@DetailID,[Status]=@Status where [AGDetailID]=@AGDetailID", UniversalService.getUpdateUTC(userId));
                SqlParameter[] parameters = new SqlParameter[]{
                    new SqlParameter("@AGDetailID",SqlDbType.VarChar),
                    new SqlParameter("@DetailID",SqlDbType.VarChar),
                    new SqlParameter("@Status",SqlDbType.VarChar),
                    };

                parameters[0].Value = Model.AGDetailID;
                parameters[1].Value = (Object)Model.DetailID ?? DBNull.Value;
                parameters[2].Value = (Object)Model.Status ?? DBNull.Value;

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
        /// SAM 2017年5月23日09:33:51
        /// </summary>
        /// <param name="AGDetailID"></param>
        /// <returns></returns>
        public static SYS_AlternativeGroupDetails get(string AGDetailID)
        {
            string sql = string.Format(@"select Top 1 * from [SYS_AlternativeGroupDetails] where [AGDetailID] = '{0}'  and [SystemID] = '{1}' ", AGDetailID, Framework.SystemID);

            DataTable dt = SQLHelper.ExecuteDataTable(sql, CommandType.Text);

            if (dt.Rows.Count == 0)
                return null;
            else
                return ToEntity(dt.Rows[0]);
        }

        /// <summary>
        /// 判断指定的制程替代群组是否已存在明细
        /// SAM 2017年5月23日09:35:11
        /// </summary>
        /// <param name="GroupID"></param>
        /// <returns></returns>
        public static bool Check(string GroupID)
        {
            string sql = string.Format(@"select * from [SYS_AlternativeGroupDetails] where [GroupID] = '{0}' and [SystemID] = '{1}' and [Status] <> '{1}0201213000003' ", GroupID, Framework.SystemID);

            DataTable dt = SQLHelper.ExecuteDataTable(sql, CommandType.Text);

            if (dt.Rows.Count == 0)
                return false;
            else
                return true;
        }

        /// <summary>
        /// 根据替代群组获取他的替代制程
        /// SAM 2017年5月25日14:26:41
        /// </summary>
        /// <param name="groupID"></param>
        /// <param name="page">页码</param>
        /// <param name="rows">行数</param>
        /// <param name="count"></param>
        /// <returns></returns>
        public static IList<Hashtable> Inf00021GetDetailsList(string groupID, int page, int rows, ref int count)
        {
            string select = string.Format(@"select A.AGDetailID,A.GroupID,A.DetailID,A.Comments,
            D.Code,D.Name, F.Code as Workcenter, F.Name as WorkcenterName, G.Name as InoutMark,
            (CASE WHEN F.InoutMark='{0}020121300002E' THEN (Select [Code] from [SYS_Organization] where [OrganizationID] = F.DepartmentID) ELSE (Select [Code] from [SYS_Manufacturers] where [ManufacturerID] = F.DepartmentID) END) as Dept,
            (CASE WHEN F.InoutMark='{0}020121300002E' THEN (Select [Code] from [SYS_Organization] where [OrganizationID] = F.DepartmentID) ELSE (Select [Code] from [SYS_Manufacturers] where [ManufacturerID] = F.DepartmentID) END) as Description,
            B.UserName as Creator,A.CreateLocalTime as CreateTime,
            (CASE WHEN A.CreateLocalTime=A.ModifiedLocalTime THEN NULL else C.UserName END) as Modifier,
            (CASE WHEN A.CreateLocalTime=A.ModifiedLocalTime THEN NULL else A.ModifiedLocalTime END) as ModifiedTime ", Framework.SystemID);
  
            string sql = string.Format(@"  from [SYS_AlternativeGroupDetails] A 
            left join [SYS_MESUsers] B on A.[Creator] = B.[MESUserID]
            left join [SYS_MESUsers] C on A.[Modifier] = C.[MESUserID]
            left join [SYS_Parameters] D on A.[DetailID] = D.[ParameterID]
            left join [SYS_WorkCenterProcess] E on A.DetailID = E.ProcessID
            left join [SYS_WorkCenter] F on E.WorkCenterID = F.WorkCenterID
            left join [SYS_Parameters] G on F.[InoutMark] = G.[ParameterID]                    
            where A.[SystemID]='{0}' and A.[Status] = '{0}0201213000001' and [GroupID] = '{1}'", Framework.SystemID, groupID);

            count = UniversalService.getCount(sql, null);

            String orderby = " D.[Code] ";

            DataTable dt = UniversalService.getTable(select, sql, orderby, null, page, rows);

            return ToHashtableList(dt);
        }


        /// <summary>
        /// 根据替代群组获取他的替代制程
        /// SAM 2017年5月25日14:19:16
        /// </summary>
        /// <param name="groupID"></param>
        /// <returns></returns>
        public static IList<Hashtable> Inf00021DetailsList(string groupID)
        {
            string select = string.Format(@"select A.AGDetailID,A.GroupID,A.DetailID,A.Comments,
            D.Code,D.Name,F.Code as Workcenter, F.Name as WorkcenterName,
            B.UserName as Creator,A.CreateLocalTime as CreateTime,
            (CASE WHEN A.CreateLocalTime=A.ModifiedLocalTime THEN NULL else C.UserName END) as Modifier,
            (CASE WHEN A.CreateLocalTime=A.ModifiedLocalTime THEN NULL else A.ModifiedLocalTime END) as ModifiedTime ");

            string sql = string.Format(@"  from [SYS_AlternativeGroupDetails] A 
            left join [SYS_MESUsers] B on A.[Creator] = B.[MESUserID]
            left join [SYS_MESUsers] C on A.[Modifier] = C.[MESUserID]
            left join [SYS_Parameters] D on A.[DetailID] = D.[ParameterID]
            left join [SYS_WorkCenterProcess] E on A.DetailID = E.ProcessID
            left join [SYS_WorkCenter] F on E.WorkCenterID = F.WorkCenterID
            where A.[SystemID]='{0}' and A.[Status] = '{0}0201213000001' and [GroupID] = '{1}'", Framework.SystemID, groupID);

            string orderBy = "order By D.[Code] ";

            DataTable dt = SQLHelper.ExecuteDataTable(select + sql + orderBy, CommandType.Text, null);

            return ToHashtableList(dt);
        }


        /// <summary>
        /// 将不在范围内的替代群组里的制程都删除了
        /// SAM 2017年5月25日15:03:52
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="AGDetailIDs"></param>
        /// <returns></returns>
        public static bool Delete(string userId, string AGDetailIDs,string groupID)
        {
            try
            {
                string sql = String.Format(@"update[SYS_AlternativeGroupDetails] set {0},
               [Status]='{1}0201213000003' where [GroupID]='{2}' ", UniversalService.getUpdateUTC(userId),Framework.SystemID,groupID);

                if(!string.IsNullOrWhiteSpace(AGDetailIDs))
                    sql += string.Format(@" and [AGDetailID] not in ('{0}')", AGDetailIDs);

                return SQLHelper.ExecuteNonQuery(sql, CommandType.Text, null) > 0;
            }
            catch (Exception ex)
            {
                DataLogerService.writeerrlog(ex);
                return false;
            }
        }

        /// <summary>
        /// 判断指定群组是否已有指定制程的对应
        /// SAM 2017年8月14日17:34:45
        /// </summary>
        /// <param name="DetailID"></param>
        /// <param name="GroupID"></param>
        /// <returns></returns>
        public static bool Check(string DetailID, string GroupID)
        {
            string sql = String.Format(@"select Top 1 * from [SYS_AlternativeGroupDetails] where [SystemID]='{0}' and [Status] <> '{0}0201213000003' ", Framework.SystemID);

            if (!string.IsNullOrWhiteSpace(DetailID))
                sql = sql + String.Format(@" and [DetailID] = '{0}' ", DetailID);

            if (!string.IsNullOrWhiteSpace(GroupID))
                sql = sql + String.Format(@" and [GroupID] = '{0}' ", GroupID);

            DataTable dt = SQLHelper.ExecuteDataTable(sql, CommandType.Text);

            if (dt.Rows.Count == 0)
                return false;
            else
                return true;
        }




        /// <summary>
        /// 检查制程是否被使用
        /// Joint 2017年7月27日14:07:35
        /// </summary>
        /// <param name="ProcessID"></param>
        /// <returns></returns>
        public static bool CheckProcess(string ProcessID)
        {
            string sql = string.Format(@"select * from [SYS_AlternativeGroupDetails]
            where [DetailID]='{1}' and [SystemID]='{0}'", Framework.SystemID, ProcessID);
            DataTable dt = SQLHelper.ExecuteDataTable(sql, CommandType.Text);

            if (dt.Rows.Count == 0)
                return false;
            else
                return true;
        }

        /// <summary>
        /// 检查工作中心是否被使用
        /// Joint 2017年7月28日14:41:03
        /// </summary>
        /// <param name="WorkCenterID"></param>
        /// <returns></returns>
        public static bool CheckWorkCenter(string WorkCenterID)
        {
            string sql = string.Format(@"select * from [SYS_AlternativeGroupDetails]
            where [WorkCenterID]='{1}' and [SystemID]='{0}'", Framework.SystemID, WorkCenterID);
            DataTable dt = SQLHelper.ExecuteDataTable(sql, CommandType.Text);

            if (dt.Rows.Count == 0)
                return false;
            else
                return true;
        }
    }
}

