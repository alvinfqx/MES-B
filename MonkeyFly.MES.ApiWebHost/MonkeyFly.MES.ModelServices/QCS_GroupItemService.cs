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
    public class QCS_GroupItemService : SuperModel<QCS_GroupItem>
    {
        /// <summary>
        /// 新增
        /// SAM 2017年5月26日11:54:52
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="Model"></param>
        /// <returns></returns>
        public static bool insert(string userId, QCS_GroupItem Model)
        {
            try
            {
                string sql = string.Format(@"insert[QCS_GroupItem]([GroupItemID],[GroupID],[ItemID],
            [Status],[Comments],[Modifier],[ModifiedTime],[ModifiedLocalTime],[Creator],[CreateTime],[CreateLocalTime],[SystemID]) values
             (@GroupItemID,@GroupID,@ItemID,@Status,@Comments,'{0}','{1}','{2}','{0}','{1}','{2}','{3}')", userId, DateTime.Now, DateTime.Now, Framework.SystemID);

                SqlParameter[] parameters = new SqlParameter[]{
                    new SqlParameter("@GroupItemID",SqlDbType.VarChar),
                    new SqlParameter("@GroupID",SqlDbType.VarChar),
                    new SqlParameter("@ItemID",SqlDbType.VarChar),
                    new SqlParameter("@Status",SqlDbType.VarChar),
                    new SqlParameter("@Comments",SqlDbType.NVarChar),
                    };

                parameters[0].Value = (Object)Model.GroupItemID ?? DBNull.Value;
                parameters[1].Value = (Object)Model.GroupID ?? DBNull.Value;
                parameters[2].Value = (Object)Model.ItemID ?? DBNull.Value;
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
        /// SAM 2017年5月26日11:55:16
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="Model"></param>
        /// <returns></returns>
        public static bool update(string userId, QCS_GroupItem Model)
        {
            try
            {
                string sql = String.Format(@"update[QCS_GroupItem] set {0},
                [ItemID]=@ItemID,[Status]=@Status where [GroupItemID]=@GroupItemID", UniversalService.getUpdateUTC(userId));
                SqlParameter[] parameters = new SqlParameter[]{
                    new SqlParameter("@GroupItemID",SqlDbType.VarChar),
                    new SqlParameter("@ItemID",SqlDbType.VarChar),
                    new SqlParameter("@Status",SqlDbType.VarChar),
                    };

                parameters[0].Value = Model.GroupItemID;
                parameters[1].Value = (Object)Model.ItemID ?? DBNull.Value;
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
        /// SAM 2017年5月26日11:55:28
        /// </summary>
        /// <param name="GroupItemID"></param>
        /// <returns></returns>
        public static QCS_GroupItem get(string GroupItemID)
        {
            string sql = string.Format(@"select Top 1 * from [QCS_GroupItem] where [GroupItemID] = '{0}'  and [SystemID] = '{1}' ", GroupItemID, Framework.SystemID);

            DataTable dt = SQLHelper.ExecuteDataTable(sql, CommandType.Text);

            if (dt.Rows.Count == 0)
                return null;
            else
                return ToEntity(dt.Rows[0]);
        }

        /// <summary>
        /// 根据检验群组码获取他的料品
        /// SAM 2017年5月25日14:26:41
        /// </summary>
        /// <param name="groupID"></param>
        /// <param name="page">页码</param>
        /// <param name="rows">行数</param>
        /// <param name="count"></param>
        /// <returns></returns>
        public static IList<Hashtable> Qcs00003GetDetailsList(string groupID, int page, int rows, ref int count)
        {
            string select = string.Format(@"select A.GroupItemID,A.GroupID,A.ItemID,A.Comments,
            D.Code,D.Name,D.Specification,(Select Name from [SYS_Parameters] where [ParameterID] = D.[Type]) as Type,
            B.UserName as Creator,A.CreateLocalTime as CreateTime,
            (CASE WHEN A.CreateLocalTime=A.ModifiedLocalTime THEN NULL else C.UserName END) as Modifier,
            (CASE WHEN A.CreateLocalTime=A.ModifiedLocalTime THEN NULL else A.ModifiedLocalTime END) as ModifiedTime ");

            string sql = string.Format(@"  from [QCS_GroupItem] A 
            left join [SYS_MESUsers] B on A.[Creator] = B.[MESUserID]
            left join [SYS_MESUsers] C on A.[Modifier] = C.[MESUserID]
            left join [SYS_Items] D on A.[ItemID] = D.[ItemID]
            where A.[SystemID]='{0}' and A.[Status] = '{0}0201213000001' and A.[GroupID] = '{1}'", Framework.SystemID, groupID);

            count = UniversalService.getCount(sql, null);

            String orderby = " D.[Code] ";

            DataTable dt = UniversalService.getTable(select, sql, orderby, null, page, rows);

            return ToHashtableList(dt);
        }

        /// <summary>
        /// 根据检验群组码获取他的料品
        /// SAM 2017年5月25日14:19:16
        /// </summary>
        /// <param name="groupID"></param>
        /// <returns></returns>
        public static IList<Hashtable> Qcs00003DetailsList(string groupID)
        {
            string select = string.Format(@"select A.GroupItemID,A.GroupID,A.ItemID,A.Comments,
            D.Code,D.Name,D.Specification,(Select Name from [SYS_Parameters] where [ParameterID] = D.[Type]) as Type,
            B.UserName as Creator,A.CreateLocalTime as CreateTime,
            (CASE WHEN A.CreateLocalTime=A.ModifiedLocalTime THEN NULL else C.UserName END) as Modifier,
            (CASE WHEN A.CreateLocalTime=A.ModifiedLocalTime THEN NULL else A.ModifiedLocalTime END) as ModifiedTime ");

            string sql = string.Format(@"  from [QCS_GroupItem] A 
            left join [SYS_MESUsers] B on A.[Creator] = B.[MESUserID]
            left join [SYS_MESUsers] C on A.[Modifier] = C.[MESUserID]
            left join [SYS_Items] D on A.[ItemID] = D.[ItemID]
            where A.[SystemID]='{0}' and A.[Status] = '{0}0201213000001' and A.[GroupID] = '{1}'", Framework.SystemID, groupID);

            string orderBy = "order By D.[Code] ";

            DataTable dt = SQLHelper.ExecuteDataTable(select + sql + orderBy, CommandType.Text, null);

            return ToHashtableList(dt);
        }


        /// <summary>
        /// 将不在范围内的检验群组吗里的料品都删除了
        /// SAM 2017年5月25日15:03:52
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="AGDetailIDs"></param>
        /// <returns></returns>
        public static bool Delete(string userId, string GroupItemIDs, string groupID)
        {
            try
            {
                string sql = String.Format(@"update [QCS_GroupItem] set {0},
               [Status]='{1}0201213000003' where [GroupID]='{2}' ", UniversalService.getUpdateUTC(userId), Framework.SystemID, groupID);

                if (!string.IsNullOrWhiteSpace(GroupItemIDs))
                    sql += string.Format(@" and [GroupItemID] not in ('{0}')", GroupItemIDs);

                return SQLHelper.ExecuteNonQuery(sql, CommandType.Text, null) > 0;
            }
            catch (Exception ex)
            {
                DataLogerService.writeerrlog(ex);
                return false;
            }
        }


        /// <summary>
        /// 检查是否已存在料品的映射
        /// SAM 2017年5月27日17:29:22
        /// </summary>
        /// <param name="ItemID"></param>
        /// <param name="GroupID"></param>
        /// <returns></returns>
        public static bool Check(string ItemID, string GroupID)
        {
            string sql = string.Format(@"select Top 1 * from [QCS_GroupItem] where [SystemID]='{0}' and [Status] <> '{0}0201213000003'", Framework.SystemID);

            if (!string.IsNullOrWhiteSpace(ItemID))
                sql = sql + string.Format(@" and [ItemID] = '{0}' ", ItemID);

            if (!string.IsNullOrWhiteSpace(GroupID))
                sql = sql + string.Format(@" and [GroupID] = '{0}' ", GroupID);

            DataTable dt = SQLHelper.ExecuteDataTable(sql, CommandType.Text, null);

            if (dt.Rows.Count == 0)
                return false;
            else
                return true;
        }

        /// <summary>
        /// 检查是否已存在料品的映射（不根据群组查询）
        /// SAM 2017年5月27日17:29:22
        /// </summary>
        /// <param name="ItemID"></param>
        /// <param name="GroupID"></param>
        /// <returns></returns>
        public static bool CheckV2(string ItemID)
        {
            string sql = string.Format(@"select Top 1 * from [QCS_GroupItem] where [SystemID]='{0}' and [Status] <> '{0}0201213000003'", Framework.SystemID);

            if (!string.IsNullOrWhiteSpace(ItemID))
                sql = sql + string.Format(@" and [ItemID] = '{0}' ", ItemID);

            DataTable dt = SQLHelper.ExecuteDataTable(sql, CommandType.Text);

            if (dt.Rows.Count == 0)
                return false;
            else
                return true;
        }

    }
}

